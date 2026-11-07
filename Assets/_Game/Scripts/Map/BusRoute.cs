using UnityEngine;

[CreateAssetMenu(fileName = "Route_New", menuName = "RealBus/Bus Route")]
public class BusRoute : ScriptableObject
{
    [Header("Route Info")]
    public string routeNumber = "NBO-1";
    public string routeName = "NBO CBD - Westlands";
    public float baseFare = 50f; // KES
    public long osmRelationId;
    public bool isRoadPathValidated;
    public bool isGrandTourRoute = false;
    public string generatedRouteId = "";
    public string sourceCityCode = "";

    [Header("Stops (in order)")]
    public BusStopData[] stops;

    [Header("Stops (new)")]
    public BusStop[] busStops;

    [Header("Route Geometry (optional)")]
    public double[] geometryLatLonFlat;

    [Header("Generated Path (world)")]
    public Vector3[] pathPoints;

    [Header("Generated Metrics")]
    public float distanceKm;
    public float estimatedTimeMinutes;
    [Range(1, 5)] public int difficulty = 3;

    public int GetStopCount()
    {
        if (busStops != null && busStops.Length > 0) return busStops.Length;
        return stops != null ? stops.Length : 0;
    }

    /// <summary>The route number with exactly one NBO prefix, safe to print in any UI.</summary>
    public string DisplayCode => RouteNaming.DisplayCode(this);

    public void EnsureRuntimeData()
    {
        // Routes can arrive from a downloaded OSM pack, an older asset, or a
        // hand-authored test fixture. Normalising here means every consumer -
        // menu, HUD, save file - sees the same NBO name without each one having
        // to remember to add the prefix.
        RouteNaming.Apply(this);

        if ((stops == null || stops.Length == 0) && busStops != null && busStops.Length > 0)
            SyncLegacyStopsFromBusStops();

        if (distanceKm <= 0.001f)
            distanceKm = CalculateDistanceKm();

        if (estimatedTimeMinutes <= 0.001f && distanceKm > 0f)
            estimatedTimeMinutes = distanceKm / 24f * 60f + GetStopCount() * 0.35f;

        // Some imported route assets carry pathPoints generated in a different
        // world origin (Web-Mercator metres, another city's map, an old scene).
        // Rebuild them from WGS84 geometry whenever the current map can provide
        // a trustworthy conversion. Consumers that run before GPS exists leave
        // this alone and retry later.
        EnsureWorldPathPoints();
    }

    /// <summary>
    /// True when the serialized <see cref="pathPoints"/> belong to this route on
    /// the current map. A stale path can be metres away from the city or millions
    /// of metres away; both make the minimap and off-route checks unusable.
    /// </summary>
    public bool HasUsableWorldPath(float maxAnchorErrorMeters = 500f)
    {
        if (pathPoints == null || pathPoints.Length < 2)
            return false;

        // A local route is inside a city, never a thousand kilometres from the
        // world origin. This also catches foreign-origin points before any GPS
        // lookup is available.
        if (pathPoints[0].sqrMagnitude > 1e10f || pathPoints[pathPoints.Length - 1].sqrMagnitude > 1e10f)
            return false;

        if (!TryGetRouteEndpoint(true, out Vector3 expectedStart) ||
            !TryGetRouteEndpoint(false, out Vector3 expectedEnd))
        {
            // Without a converter we cannot prove the path is stale, so keep it.
            return true;
        }

        return IsWorldPathNear(pathPoints, expectedStart, expectedEnd, maxAnchorErrorMeters);
    }

    /// <summary>
    /// Pure endpoint check used by the runtime validator and by tests. A route
    /// path must begin and end where its WGS84 geometry/stops say they should.
    /// </summary>
    public static bool IsWorldPathNear(Vector3[] points, Vector3 expectedStart, Vector3 expectedEnd,
        float toleranceMeters = 500f)
    {
        if (points == null || points.Length < 2)
            return false;

        if (points[0].sqrMagnitude > 1e10f || points[points.Length - 1].sqrMagnitude > 1e10f)
            return false;

        float tolerance = Mathf.Max(50f, toleranceMeters);
        return (points[0] - expectedStart).sqrMagnitude <= tolerance * tolerance &&
               (points[points.Length - 1] - expectedEnd).sqrMagnitude <= tolerance * tolerance;
    }

    /// <summary>
    /// Rebuilds the runtime world path from the route's WGS84 geometry (or stops)
    /// when the serialized one is missing or belongs to another origin. Returns
    /// true when a usable path is available afterwards.
    /// </summary>
    public bool EnsureWorldPathPoints(bool forceRebuild = false)
    {
        if (!forceRebuild && HasUsableWorldPath())
            return true;

        Vector3[] rebuilt = BuildWorldPathFromGeodata();
        if (rebuilt == null || rebuilt.Length < 2)
            return false;

        pathPoints = rebuilt;
        return true;
    }

    Vector3[] BuildWorldPathFromGeodata()
    {
        if (geometryLatLonFlat != null && geometryLatLonFlat.Length >= 4)
        {
            int count = geometryLatLonFlat.Length / 2;
            var points = new Vector3[count];
            for (int i = 0; i < count; i++)
            {
                if (!TryGpsToWorld(geometryLatLonFlat[i * 2], geometryLatLonFlat[i * 2 + 1], out points[i]))
                    return null;
                points[i].y = 0.1f;
            }
            return points;
        }

        if (stops != null && stops.Length >= 2)
        {
            var points = new Vector3[stops.Length];
            for (int i = 0; i < stops.Length; i++)
            {
                BusStopData stop = stops[i];
                if (stop == null || !TryGpsToWorld(stop.latitude, stop.longitude, out points[i]))
                    return null;
                points[i].y = 0.1f;
            }
            return points;
        }

        return null;
    }

    bool TryGetRouteEndpoint(bool start, out Vector3 world)
    {
        world = default;
        if (geometryLatLonFlat != null && geometryLatLonFlat.Length >= 4)
        {
            int pair = start ? 0 : geometryLatLonFlat.Length / 2 - 1;
            return TryGpsToWorld(geometryLatLonFlat[pair * 2], geometryLatLonFlat[pair * 2 + 1], out world);
        }

        if (stops != null && stops.Length > 0)
        {
            BusStopData stop = stops[start ? 0 : stops.Length - 1];
            return stop != null && TryGpsToWorld(stop.latitude, stop.longitude, out world);
        }

        return false;
    }

    static bool TryGpsToWorld(double latitude, double longitude, out Vector3 world)
    {
        world = default;
        if (double.IsNaN(latitude) || double.IsNaN(longitude) ||
            double.IsInfinity(latitude) || double.IsInfinity(longitude) ||
            Mathf.Abs((float)latitude) > 90f || Mathf.Abs((float)longitude) > 180f)
            return false;

        // GPSManager is the gameplay conversion path and matches the road mesh
        // and traffic graph. Fall back to the active city's local origin so the
        // route still resolves during early menu/bootstrap work.
        if (GPSManager.Instance != null)
        {
            world = GPSManager.Instance.GpsToWorld(latitude, longitude);
            return true;
        }

        CoordinateConverter converter = CoordinateConverter.Instance;
        if (converter != null && converter.mapOrigin != null && converter.mapOrigin.IsValid())
        {
            world = converter.GeoToWorldPosition(latitude, longitude);
            return true;
        }

        var city = GameState.Instance != null && GameState.Instance.selectedCity != null
            ? GameState.Instance.selectedCity
            : CityManager.Instance != null ? CityManager.Instance.activeCity : null;
        if (city != null)
        {
            world = CoordinateConverter.LocalOriginGeoToWorld(latitude, longitude, city.centreLat, city.centreLon);
            return true;
        }

        return false;
    }

    float CalculateDistanceKm()
    {
        if (geometryLatLonFlat != null && geometryLatLonFlat.Length >= 4)
        {
            double total = 0d;
            for (int i = 2; i + 1 < geometryLatLonFlat.Length; i += 2)
                total += HaversineKm(geometryLatLonFlat[i - 2], geometryLatLonFlat[i - 1], geometryLatLonFlat[i], geometryLatLonFlat[i + 1]);
            return (float)total;
        }

        if (stops == null || stops.Length < 2) return 0f;
        double stopTotal = 0d;
        for (int i = 1; i < stops.Length; i++)
            stopTotal += HaversineKm(stops[i - 1].latitude, stops[i - 1].longitude, stops[i].latitude, stops[i].longitude);
        return (float)stopTotal;
    }

    static double HaversineKm(double latA, double lonA, double latB, double lonB)
    {
        const double radiusKm = 6371.0088d;
        double lat1 = latA * System.Math.PI / 180d;
        double lat2 = latB * System.Math.PI / 180d;
        double dLat = (latB - latA) * System.Math.PI / 180d;
        double dLon = (lonB - lonA) * System.Math.PI / 180d;
        double sinLat = System.Math.Sin(dLat * 0.5d);
        double sinLon = System.Math.Sin(dLon * 0.5d);
        double value = sinLat * sinLat + System.Math.Cos(lat1) * System.Math.Cos(lat2) * sinLon * sinLon;
        return radiusKm * 2d * System.Math.Atan2(System.Math.Sqrt(value), System.Math.Sqrt(System.Math.Max(0d, 1d - value)));
    }

    public string GetProgressionId(string fallbackCityCode = null)
    {
        if (!string.IsNullOrWhiteSpace(generatedRouteId))
            return generatedRouteId.Trim();

        if (!string.IsNullOrWhiteSpace(routeNumber))
            return routeNumber.Trim();

        if (!string.IsNullOrWhiteSpace(routeName))
            return routeName.Trim();

        if (!string.IsNullOrWhiteSpace(sourceCityCode))
            return sourceCityCode.Trim();

        if (!string.IsNullOrWhiteSpace(fallbackCityCode))
            return fallbackCityCode.Trim();

        return name;
    }

    /// <summary>
    /// Imported routes list the two platforms of a stop pair as separate stops, and
    /// some pairs sit at the same coordinates. Serving the second one serves the stop
    /// the player has just left, which reads as the navigation sending the bus back to
    /// the same station. These radii decide when two entries are the same platform.
    /// </summary>
    public const float SamePlatformRadiusMeters = 6f;
    public const float SamePlatformNamedRadiusMeters = 30f;

    /// <summary>
    /// True when <paramref name="candidate"/> is the platform just served rather than
    /// a new stop: the same place recorded twice, or the opposite-direction platform
    /// of the same named stop on the other side of the road.
    /// </summary>
    public static bool IsSamePlatform(BusStopData served, BusStopData candidate)
    {
        if (served == null || candidate == null) return false;
        float distance = MetresBetween(served, candidate);
        if (distance <= SamePlatformRadiusMeters) return true;
        if (distance > SamePlatformNamedRadiusMeters) return false;
        return SameStopName(served.stopName, candidate.stopName);
    }

    /// <summary>
    /// The index of the next stop worth driving to after <paramref name="servedIndex"/>,
    /// skipping platforms that are the stop just served. Returns
    /// <c>servedIndex + 1</c> when nothing is skipped, and the stop count when the
    /// platform was the last entry, so route completion still fires.
    /// </summary>
    public static int NextDistinctStopIndex(BusStopData[] stops, int servedIndex)
    {
        int next = Mathf.Max(0, servedIndex + 1);
        if (stops == null || stops.Length == 0 || servedIndex < 0 || servedIndex >= stops.Length)
            return next;

        BusStopData served = stops[servedIndex];
        while (next < stops.Length && IsSamePlatform(served, stops[next]))
            next++;
        return next;
    }

    /// <summary>
    /// Marks the stops the mission will actually serve: the first entry of every
    /// consecutive same-platform run. Imported routes list the two platforms of a
    /// roadside pair back to back and the mission skips the second one, so building
    /// furniture for both put a shelter on the oncoming kerb.
    /// </summary>
    public static bool[] ServedStopMask(BusStopData[] stops)
    {
        int count = stops != null ? stops.Length : 0;
        var served = new bool[count];
        int lastServed = -1;
        for (int i = 0; i < count; i++)
        {
            if (lastServed >= 0 && IsSamePlatform(stops[lastServed], stops[i]))
                continue;
            served[i] = true;
            lastServed = i;
        }
        return served;
    }

    /// <summary>
    /// Index of the stop the mission serves immediately before <paramref name="index"/>
    /// in route order, skipping duplicate platform entries. Returns -1 when there is
    /// no earlier served stop. Stop-side placement uses this so the approach vector
    /// never comes from the opposite platform of a pair.
    /// </summary>
    public static int PreviousServedStopIndex(BusStopData[] stops, int index)
    {
        if (stops == null || index <= 0)
            return -1;

        int limit = Mathf.Min(index, stops.Length);
        int previous = -1;
        for (int i = 0; i < limit; i++)
        {
            if (previous >= 0 && IsSamePlatform(stops[previous], stops[i]))
                continue;
            previous = i;
        }
        return previous;
    }

    static bool SameStopName(string a, string b)
    {
        if (string.IsNullOrWhiteSpace(a) || string.IsNullOrWhiteSpace(b)) return false;
        return string.Equals(a.Trim(), b.Trim(), System.StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>Flat-earth metres between two stops. Both are inside one city, so this is exact enough.</summary>
    static float MetresBetween(BusStopData a, BusStopData b)
    {
        double latRadians = a.latitude * System.Math.PI / 180d;
        double northMeters = (b.latitude - a.latitude) * System.Math.PI / 180d * 6371008.8d;
        double eastMeters = (b.longitude - a.longitude) * System.Math.PI / 180d * 6371008.8d *
                            System.Math.Cos(latRadians);
        return (float)System.Math.Sqrt(northMeters * northMeters + eastMeters * eastMeters);
    }

    public void SyncLegacyStopsFromBusStops()
    {
        if (busStops == null || busStops.Length == 0)
        {
            stops = System.Array.Empty<BusStopData>();
            return;
        }

        var syncedStops = new BusStopData[busStops.Length];
        for (int i = 0; i < busStops.Length; i++)
        {
            var stop = busStops[i];
            syncedStops[i] = new BusStopData
            {
                stopName = stop != null ? stop.stopName : "",
                latitude = stop != null ? stop.latitude : 0d,
                longitude = stop != null ? stop.longitude : 0d
            };
        }

        stops = syncedStops;
    }
}

[System.Serializable]
public class BusStopData
{
    public string stopName;
    public double latitude;
    public double longitude;
    public float waitTimeSeconds = 10f;
}
