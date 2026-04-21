using UnityEngine;

/// <summary>
/// Singleton that converts GPS coordinates to Unity world positions.
/// All gameplay systems use this — never call MapTileLoader directly.
/// </summary>
public class GPSManager : MonoBehaviour
{
    public static GPSManager Instance { get; private set; }

    private MapTileLoader mapTileLoader;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        ResolveMapTileLoader();
    }

    void Start()
    {
        ResolveMapTileLoader();
        if (mapTileLoader == null)
            Debug.LogError("GPSManager: No MapTileLoader found in scene!");
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
        ResolveMapTileLoader();
        if (mapTileLoader == null) return Vector3.zero;
        return mapTileLoader.GpsToWorldPosition(lat, lon);
    }

    /// <summary>
    /// Convert Unity world position back to GPS.
    /// </summary>
    public (double lat, double lon) WorldToGps(Vector3 worldPos)
    {
        ResolveMapTileLoader();
        if (mapTileLoader == null) return (0, 0);
        return mapTileLoader.WorldPositionToGps(worldPos);
    }
}
