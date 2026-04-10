using UnityEngine;

/// <summary>
/// Single conversion API for Geo <-> World used across gameplay systems.
/// Uses Mapbox AbstractMap if present; otherwise falls back to local origin math.
/// </summary>
public class CoordinateConverter : MonoBehaviour
{
    public static CoordinateConverter Instance { get; private set; }

    [Header("Origin")]
    public MapOrigin mapOrigin;

    [Header("Optional fallback source")]
    public MapTileLoader mapTileLoader;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        if (mapTileLoader == null)
            mapTileLoader = FindFirstObjectByType<MapTileLoader>();

        if (mapOrigin == null && CityManager.Instance?.activeCity != null)
        {
            // If user didn't create an asset yet, seed from active city to avoid zero origin.
            // (This is runtime-only; for persistence create a MapOrigin asset.)
            mapOrigin = ScriptableObject.CreateInstance<MapOrigin>();
            mapOrigin.originLat = CityManager.Instance.activeCity.centreLat;
            mapOrigin.originLon = CityManager.Instance.activeCity.centreLon;
            mapOrigin.originLabel = CityManager.Instance.activeCity.cityName;
        }
    }

    public Vector3 GeoToWorldPosition(double lat, double lon)
    {
        // Mapbox integration (optional) — only if package is present at compile time.
#if MAPBOX_SDK
        var map = FindFirstObjectByType<Mapbox.Unity.Map.AbstractMap>();
        if (map != null)
        {
            var v2 = new Mapbox.Utils.Vector2d(lat, lon);
            return map.GeoToWorldPosition(v2, true);
        }
#endif

        if (mapTileLoader != null)
            return mapTileLoader.GpsToWorldPosition(lat, lon);

        if (mapOrigin == null)
            return Vector3.zero;

        return LocalOriginGeoToWorld(lat, lon, mapOrigin.originLat, mapOrigin.originLon);
    }

    public (double lat, double lon) WorldToGeoPosition(Vector3 worldPos)
    {
#if MAPBOX_SDK
        var map = FindFirstObjectByType<Mapbox.Unity.Map.AbstractMap>();
        if (map != null)
        {
            var v2 = map.WorldToGeoPosition(worldPos);
            return (v2.x, v2.y);
        }
#endif

        if (mapTileLoader != null)
            return mapTileLoader.WorldPositionToGps(worldPos);

        if (mapOrigin == null)
            return (0, 0);

        return LocalOriginWorldToGeo(worldPos, mapOrigin.originLat, mapOrigin.originLon);
    }

    // --- Pure math fallbacks (deterministic, testable) ---

    public static Vector3 LocalOriginGeoToWorld(double lat, double lon, double originLat, double originLon)
    {
        // Same approximation used by MapTileLoader (good for small areas; consistent and invertible).
        double metersPerDegreeLat = 111320.0;
        double metersPerDegreeLon = 111320.0 * System.Math.Cos(originLat * System.Math.PI / 180.0);

        float worldX = (float)((lon - originLon) * metersPerDegreeLon);
        float worldZ = (float)((lat - originLat) * metersPerDegreeLat);
        return new Vector3(worldX, 0f, worldZ);
    }

    public static (double lat, double lon) LocalOriginWorldToGeo(Vector3 worldPos, double originLat, double originLon)
    {
        double metersPerDegreeLat = 111320.0;
        double metersPerDegreeLon = 111320.0 * System.Math.Cos(originLat * System.Math.PI / 180.0);

        double lat = originLat + (worldPos.z / metersPerDegreeLat);
        double lon = originLon + (worldPos.x / metersPerDegreeLon);
        return (lat, lon);
    }
}

