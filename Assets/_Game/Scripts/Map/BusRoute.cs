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
