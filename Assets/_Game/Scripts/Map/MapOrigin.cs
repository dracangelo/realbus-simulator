using UnityEngine;

[CreateAssetMenu(fileName = "MapOrigin", menuName = "RealBus/Map/Map Origin")]
public class MapOrigin : ScriptableObject
{
    [Header("Origin (GPS)")]
    public double originLat = -1.2864;
    public double originLon = 36.8172;

    [Header("Notes")]
    public string originLabel = "Default";
}

