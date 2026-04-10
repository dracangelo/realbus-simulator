using UnityEngine;

[CreateAssetMenu(fileName = "Route_New", menuName = "RealBus/Bus Route")]
public class BusRoute : ScriptableObject
{
    [Header("Route Info")]
    public string routeNumber = "1";
    public string routeName = "CBD - Westlands";
    public float baseFare = 50f; // KES

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
}

[System.Serializable]
public class BusStopData
{
    public string stopName;
    public double latitude;
    public double longitude;
    public float waitTimeSeconds = 10f;
}