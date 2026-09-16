using UnityEngine;

[CreateAssetMenu(fileName = "Route_New", menuName = "RealBus/Bus Route")]
public class BusRoute : ScriptableObject
{
    [Header("Route Info")]
    public string routeNumber = "1";
    public string routeName = "CBD - Westlands";
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

    public void EnsureRuntimeData()
    {
        if ((stops == null || stops.Length == 0) && busStops != null && busStops.Length > 0)
            SyncLegacyStopsFromBusStops();

        if (distanceKm <= 0.001f)
            distanceKm = CalculateDistanceKm();

        if (estimatedTimeMinutes <= 0.001f && distanceKm > 0f)
            estimatedTimeMinutes = distanceKm / 24f * 60f + GetStopCount() * 0.35f;
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
