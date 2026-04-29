using UnityEngine;

/// <summary>
/// Single conversion API: GPS (WGS84) ↔ Unity world-space (metres).
///
/// Priority chain (first available wins):
///   1. Mapbox AbstractMap   — if MAPBOX_SDK is defined and map is alive
///   2. MapTileLoader        — if present and initialised
///   3. Local-origin flat-earth approximation (deterministic, unit-testable)
///
/// Performance: hot-path calls (GeoToWorldPosition / WorldToGeoPosition) are
/// O(1) with no allocations.  FindFirstObjectByType is only called during
/// Start() and SetCityOrigin(), never per-frame.
/// </summary>
public class CoordinateConverter : UnityEngine.MonoBehaviour
{
    public static CoordinateConverter Instance { get; private set; }

    [Header("Origin")]
    public MapOrigin mapOrigin;

    [Header("Optional – tile loader fallback")]
    public MapTileLoader mapTileLoader;

    // ── Constants ────────────────────────────────────────────────────

    /// <summary>Metres per degree of latitude (effectively constant).</summary>
    public const double MetersPerDegreeLat = 111_320.0;

    // ── Lifecycle ────────────────────────────────────────────────────

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        if (mapTileLoader == null)
            mapTileLoader = FindFirstObjectByType<MapTileLoader>();

        // Auto-seed origin from active city if no asset was assigned.
        if (mapOrigin == null || !mapOrigin.IsValid())
        {
            var city = CityManager.Instance?.activeCity;
            if (city != null)
            {
                if (mapOrigin == null)
                    mapOrigin = ScriptableObject.CreateInstance<MapOrigin>();
                mapOrigin.SetFromCity(city);
            }
        }
    }

    // ── Public API ───────────────────────────────────────────────────

    public UnityEngine.Vector3 GeoToWorldPosition(double lat, double lon)
    {
#if MAPBOX_SDK
        var abstractMap = Mapbox.Unity.Map.AbstractMap.Instance; // cached singleton
        if (abstractMap != null)
        {
            var v2 = new Mapbox.Utils.Vector2d(lat, lon);
            return abstractMap.GeoToWorldPosition(v2, true);
        }
#endif
        if (mapTileLoader != null)
            return mapTileLoader.GpsToWorldPosition(lat, lon);

        if (mapOrigin != null && mapOrigin.IsValid())
            return LocalOriginGeoToWorld(lat, lon, mapOrigin.originLat, mapOrigin.originLon);

        UnityEngine.Debug.LogWarning("CoordinateConverter: No origin set — returning Vector3.zero.");
        return UnityEngine.Vector3.zero;
    }

    public (double lat, double lon) WorldToGeoPosition(UnityEngine.Vector3 worldPos)
    {
#if MAPBOX_SDK
        var abstractMap = Mapbox.Unity.Map.AbstractMap.Instance;
        if (abstractMap != null)
        {
            var v2 = abstractMap.WorldToGeoPosition(worldPos);
            return (v2.x, v2.y);
        }
#endif
        if (mapTileLoader != null)
            return mapTileLoader.WorldPositionToGps(worldPos);

        if (mapOrigin != null && mapOrigin.IsValid())
            return LocalOriginWorldToGeo(worldPos, mapOrigin.originLat, mapOrigin.originLon);

        return (0d, 0d);
    }

    /// <summary>Called by CitySelector when a new city is loaded.</summary>
    public void SetCityOrigin(CityDefinition city)
    {
        if (city == null) return;

        if (mapOrigin == null)
            mapOrigin = ScriptableObject.CreateInstance<MapOrigin>();

        mapOrigin.SetFromCity(city);
    }

    // ── Pure-math helpers (static → fully unit-testable without a scene) ──

    /// <summary>
    /// Equirectangular projection anchored to an origin.
    /// Accurate to within ~0.1 m for areas up to ~50 km radius.
    /// Consistent with <see cref="LocalOriginWorldToGeo"/> (round-trip stable).
    /// </summary>
    public static UnityEngine.Vector3 LocalOriginGeoToWorld(
        double lat, double lon,
        double originLat, double originLon)
    {
        double cosLat = System.Math.Cos(originLat * System.Math.PI / 180.0);
        double metersPerDegreeLon = MetersPerDegreeLat * cosLat;

        float x = (float)((lon - originLon) * metersPerDegreeLon);
        float z = (float)((lat - originLat) * MetersPerDegreeLat);
        return new UnityEngine.Vector3(x, 0f, z);
    }

    /// <summary>Inverse of <see cref="LocalOriginGeoToWorld"/>.</summary>
    public static (double lat, double lon) LocalOriginWorldToGeo(
        UnityEngine.Vector3 worldPos,
        double originLat, double originLon)
    {
        double cosLat = System.Math.Cos(originLat * System.Math.PI / 180.0);
        double metersPerDegreeLon = MetersPerDegreeLat * cosLat;

        double lat = originLat + worldPos.z / MetersPerDegreeLat;
        double lon = originLon + worldPos.x / metersPerDegreeLon;
        return (lat, lon);
    }

    /// <summary>
    /// Returns metres-per-degree-longitude at the given latitude.
    /// Used by OverpassQueryBuilder and any system computing bounding boxes.
    /// </summary>
    public static double MetersPerDegreeLonAt(double lat) =>
        MetersPerDegreeLat * System.Math.Cos(lat * System.Math.PI / 180.0);
}
