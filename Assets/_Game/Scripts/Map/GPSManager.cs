using UnityEngine;

/// <summary>
/// Singleton that converts GPS coordinates to Unity world positions.
/// All gameplay systems use this — never call MapTileLoader directly.
/// </summary>
public class GPSManager : MonoBehaviour
{
    public static GPSManager Instance { get; private set; }

    private CoordinateConverter coordinateConverter;
    private MapTileLoader mapTileLoader;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        ResolveCoordinateConverter();
        ResolveMapTileLoader();
    }

    void Start()
    {
        ResolveCoordinateConverter();
        ResolveMapTileLoader();
        if (coordinateConverter == null && mapTileLoader == null)
            Debug.LogError("GPSManager: No CoordinateConverter or MapTileLoader found in scene!");
    }

    void ResolveCoordinateConverter()
    {
        if (coordinateConverter == null)
            coordinateConverter = CoordinateConverter.Instance ?? FindFirstObjectByType<CoordinateConverter>();
    }

    void ResolveMapTileLoader()
    {
        if (mapTileLoader == null)
            mapTileLoader = FindFirstObjectByType<MapTileLoader>();
    }

    /// <summary>
    /// Convert GPS to Unity world position. Use this everywhere.
    /// </summary>
    public Vector3 GpsToWorld(double lat, double lon)
    {
        ResolveCoordinateConverter();
        if (coordinateConverter != null)
            return coordinateConverter.GeoToWorldPosition(lat, lon);

        ResolveMapTileLoader();
        if (mapTileLoader == null) return Vector3.zero;
        return mapTileLoader.GpsToWorldPosition(lat, lon);
    }

    /// <summary>
    /// Convert Unity world position back to GPS.
    /// </summary>
    public (double lat, double lon) WorldToGps(Vector3 worldPos)
    {
        ResolveCoordinateConverter();
        if (coordinateConverter != null)
            return coordinateConverter.WorldToGeoPosition(worldPos);

        ResolveMapTileLoader();
        if (mapTileLoader == null) return (0, 0);
        return mapTileLoader.WorldPositionToGps(worldPos);
    }
}
