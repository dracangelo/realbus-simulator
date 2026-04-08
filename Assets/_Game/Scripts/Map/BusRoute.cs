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
}

[System.Serializable]
public class BusStopData
{
    public string stopName;
    public double latitude;
    public double longitude;
    public float waitTimeSeconds = 10f;
}