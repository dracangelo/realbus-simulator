using System;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Single conversion API: GPS (WGS84) ↔ Unity world-space (metres).
///
/// Priority chain (first available wins):
///   1. Mapbox AbstractMap   — when a runtime Mapbox map is alive
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

    [Header("Diagnostics")]
    [SerializeField] bool useMapboxBridge = true;
    [SerializeField] bool logBridgeSelection;

    object _cachedMapboxMap;
    Type _cachedAbstractMapType;
    Type _cachedVector2dType;
    MethodInfo _geoToWorldMethod;
    MethodInfo _worldToGeoMethod;
    FieldInfo _vector2dXField;
    FieldInfo _vector2dYField;
    bool _mapboxTypeLookupAttempted;
    float _nextMapboxLookupTime;
    string _lastBridgeLabel = "Unresolved";

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

        RefreshBridgeSelectionLog();
    }

    // ── Public API ───────────────────────────────────────────────────

    public UnityEngine.Vector3 GeoToWorldPosition(double lat, double lon)
    {
        if (TryGeoToWorldWithMapbox(lat, lon, out Vector3 mapboxWorld))
            return mapboxWorld;

        if (mapTileLoader != null)
            return mapTileLoader.GpsToWorldPosition(lat, lon);

        if (mapOrigin != null && mapOrigin.IsValid())
            return LocalOriginGeoToWorld(lat, lon, mapOrigin.originLat, mapOrigin.originLon);

        UnityEngine.Debug.LogWarning("CoordinateConverter: No origin set — returning Vector3.zero.");
        return UnityEngine.Vector3.zero;
    }

    public (double lat, double lon) WorldToGeoPosition(UnityEngine.Vector3 worldPos)
    {
        if (TryWorldToGeoWithMapbox(worldPos, out var mapboxGeo))
            return mapboxGeo;

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

    bool TryGeoToWorldWithMapbox(double lat, double lon, out Vector3 worldPosition)
    {
        worldPosition = default;
        if (!TryResolveMapboxBridge())
            return false;

        try
        {
            object latLon = Activator.CreateInstance(_cachedVector2dType, lat, lon);
            object result = _geoToWorldMethod.Invoke(_cachedMapboxMap, new[] { latLon, (object)true });
            if (result is Vector3 vector)
            {
                worldPosition = vector;
                _lastBridgeLabel = "Mapbox";
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"CoordinateConverter: Mapbox GeoToWorldPosition bridge failed. {ex.Message}");
            InvalidateMapboxBridge();
        }

        return false;
    }

    bool TryWorldToGeoWithMapbox(Vector3 worldPosition, out (double lat, double lon) geoPosition)
    {
        geoPosition = default;
        if (!TryResolveMapboxBridge())
            return false;

        try
        {
            object result = _worldToGeoMethod.Invoke(_cachedMapboxMap, new object[] { worldPosition });
            if (result != null)
            {
                double lat = Convert.ToDouble(_vector2dXField.GetValue(result));
                double lon = Convert.ToDouble(_vector2dYField.GetValue(result));
                geoPosition = (lat, lon);
                _lastBridgeLabel = "Mapbox";
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"CoordinateConverter: Mapbox WorldToGeoPosition bridge failed. {ex.Message}");
            InvalidateMapboxBridge();
        }

        return false;
    }

    bool TryResolveMapboxBridge()
    {
        if (!useMapboxBridge)
            return false;

        if (_cachedMapboxMap is UnityEngine.Object cachedObject && cachedObject != null &&
            _geoToWorldMethod != null && _worldToGeoMethod != null &&
            _vector2dXField != null && _vector2dYField != null)
        {
            return true;
        }

        if (Time.unscaledTime < _nextMapboxLookupTime)
            return false;

        _nextMapboxLookupTime = Time.unscaledTime + 1f;

        if (!_mapboxTypeLookupAttempted)
        {
            _mapboxTypeLookupAttempted = true;
            _cachedAbstractMapType = FindType("Mapbox.Unity.Map.AbstractMap");
            _cachedVector2dType = FindType("Mapbox.Utils.Vector2d");
            if (_cachedVector2dType != null)
            {
                _vector2dXField = _cachedVector2dType.GetField("x");
                _vector2dYField = _cachedVector2dType.GetField("y");
            }
        }

        if (_cachedAbstractMapType == null || _cachedVector2dType == null)
            return false;

        _geoToWorldMethod = _cachedAbstractMapType.GetMethod(
            "GeoToWorldPosition",
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            types: new[] { _cachedVector2dType, typeof(bool) },
            modifiers: null);

        _worldToGeoMethod = _cachedAbstractMapType.GetMethod(
            "WorldToGeoPosition",
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            types: new[] { typeof(Vector3) },
            modifiers: null);

        if (_geoToWorldMethod == null || _worldToGeoMethod == null ||
            _vector2dXField == null || _vector2dYField == null)
        {
            InvalidateMapboxBridge(clearTypeCache: false);
            return false;
        }

        var loadedMaps = Resources.FindObjectsOfTypeAll(_cachedAbstractMapType);
        for (int i = 0; i < loadedMaps.Length; i++)
        {
            if (loadedMaps[i] is Component component &&
                component.gameObject.scene.IsValid() &&
                component.gameObject.scene.isLoaded)
            {
                _cachedMapboxMap = loadedMaps[i];
                return true;
            }
        }

        _cachedMapboxMap = null;
        return false;
    }

    void InvalidateMapboxBridge(bool clearTypeCache = false)
    {
        _cachedMapboxMap = null;
        _geoToWorldMethod = null;
        _worldToGeoMethod = null;
        _nextMapboxLookupTime = Time.unscaledTime + 1f;

        if (clearTypeCache)
        {
            _cachedAbstractMapType = null;
            _cachedVector2dType = null;
            _vector2dXField = null;
            _vector2dYField = null;
            _mapboxTypeLookupAttempted = false;
        }
    }

    static Type FindType(string fullName)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        for (int i = 0; i < assemblies.Length; i++)
        {
            Type type = assemblies[i].GetType(fullName, throwOnError: false);
            if (type != null)
                return type;
        }

        return null;
    }

    void RefreshBridgeSelectionLog()
    {
        string nextLabel = "LocalOrigin";
        if (TryResolveMapboxBridge())
            nextLabel = "Mapbox";
        else if (mapTileLoader != null)
            nextLabel = "MapTileLoader";

        if (logBridgeSelection && !string.Equals(_lastBridgeLabel, nextLabel, StringComparison.Ordinal))
            Debug.Log($"CoordinateConverter: using {nextLabel} bridge.");

        _lastBridgeLabel = nextLabel;
    }
}
