using UnityEngine;

[System.Serializable]
public class BusStop
{
    public string stopId = "";
    public string stopName = "Stop";
    public double latitude;
    public double longitude;
    public float headingDegrees;

    public Vector3 WorldPosition(CoordinateConverter converter)
    {
        return converter != null ? converter.GeoToWorldPosition(latitude, longitude) : Vector3.zero;
    }
}