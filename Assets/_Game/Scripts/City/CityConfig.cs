using UnityEngine;

[System.Serializable]
public struct GeoCoordinate
{
    public double latitude;
    public double longitude;

    public GeoCoordinate(double lat, double lon)
    { latitude = lat; longitude = lon; }

    public bool IsValid() =>
        System.Math.Abs(latitude) > 0.0001 || System.Math.Abs(longitude) > 0.0001;

    public override string ToString() =>
        $"({latitude:F6}, {longitude:F6})";
}

/// <summary>
/// Serialised city configuration asset (section 3.8).
///
/// One CityConfig per city.  At runtime <see cref="CreateRuntimeCityDefinition"/>
/// creates a transient <see cref="CityDefinition"/> from this data so the asset
/// itself is never mutated in a build.
///
/// The bounding box is derived from <see cref="gpsCenter"/> and
/// <see cref="queryHalfExtentMeters"/> — never edit it directly on CityDefinition.
/// </summary>
[CreateAssetMenu(fileName = "CityConfig", menuName = "RealBus/City Config")]
public class CityConfig : ScriptableObject
{
    // ── Identity ───────────────────────────────────────────────────────

    [Header("Identity")]
    public string cityName    = "Tutorial City";
    public string countryName = "Kenya";
    [Tooltip("2–4 uppercase letters. Used as a stable key throughout the save system.")]
    public string cityCode    = "NBI";

    // ── Location ───────────────────────────────────────────────────────

    [Header("Location")]
    public GeoCoordinate gpsCenter = new(-1.2864, 36.8172);

    [Tooltip("Overpass query half-extent in metres. 1800 m ≈ 3.6 km² — good for a city centre.")]
    [Min(500f)]  public float queryHalfExtentMeters = 1800f;

    [Tooltip("Mapbox zoom level used when the city loads.")]
    [Range(10, 18)] public int defaultZoom = 15;

    // ── City pack ──────────────────────────────────────────────────────

    [Header("City Pack")]
    [Tooltip("Estimated download size shown in Offline Maps UI.")]
    [Min(0f)] public float predownloadSizeMB = 48f;

    [Tooltip("Fetch routes from OSM the first time this city is selected.")]
    public bool loadRoutesOnDemand = true;

    [Tooltip("Routes baked at build time (optional — importer will append more).")]
    public BusRoute[] availableRoutes = System.Array.Empty<BusRoute>();

    // ── Validation ─────────────────────────────────────────────────────

    public bool IsValid(out string reason)
    {
        if (string.IsNullOrWhiteSpace(cityName))
        { reason = "cityName is empty"; return false; }

        if (!gpsCenter.IsValid())
        { reason = "gpsCenter is (0,0)"; return false; }

        if (queryHalfExtentMeters < 200f)
        { reason = "queryHalfExtentMeters < 200 m — too small"; return false; }

        reason = "";
        return true;
    }

    // ── Runtime factory ────────────────────────────────────────────────

    /// <summary>
    /// Creates a transient <see cref="CityDefinition"/> (not saved to disk)
    /// from this config.  Safe to call from any thread-like coroutine.
    /// </summary>
    public CityDefinition CreateRuntimeCityDefinition()
    {
        var city = ScriptableObject.CreateInstance<CityDefinition>();
        city.hideFlags = HideFlags.DontSave;
        ApplyTo(city, preserveExistingRoutes: false);
        return city;
    }

    /// <summary>
    /// Applies (or re-applies) this config's data to an existing
    /// <see cref="CityDefinition"/>, preserving any runtime-fetched routes
    /// when <paramref name="preserveExistingRoutes"/> is true.
    /// </summary>
    public void ApplyTo(CityDefinition city, bool preserveExistingRoutes)
    {
        if (city == null) return;

        city.cityName  = cityName.Trim();
        city.country   = countryName.Trim();
        city.cityCode  = NormaliseCityCode(cityCode, cityName);
        city.centreLat = gpsCenter.latitude;
        city.centreLon = gpsCenter.longitude;
        city.spawnLat  = gpsCenter.latitude;
        city.spawnLon  = gpsCenter.longitude;

        OverpassQueryBuilder.BoundingBoxFromCenter(
            gpsCenter.latitude, gpsCenter.longitude,
            Mathf.Max(200f, queryHalfExtentMeters),
            out city.minLat, out city.minLon,
            out city.maxLat, out city.maxLon);

        city.roadsFileName     = "roads.json";
        city.buildingsFileName = "buildings.json";
        city.isDownloaded      = true;
        city.previewImagePath  = "";

        if (!preserveExistingRoutes
            || city.availableRoutes == null
            || city.availableRoutes.Length == 0)
        {
            city.availableRoutes = availableRoutes != null
                ? (BusRoute[])availableRoutes.Clone()
                : System.Array.Empty<BusRoute>();
        }
    }

    // ── Helpers ────────────────────────────────────────────────────────

    static string NormaliseCityCode(string code, string fallback)
    {
        string src = string.IsNullOrWhiteSpace(code) ? fallback : code;
        var sb = new System.Text.StringBuilder(4);
        foreach (char c in src)
        {
            if (char.IsLetterOrDigit(c))
                sb.Append(char.ToUpperInvariant(c));
            if (sb.Length >= 4) break;
        }
        return sb.Length == 0 ? "CITY" : sb.ToString();
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        // Surface validation errors in the Inspector immediately.
        if (!IsValid(out string reason))
            Debug.LogWarning($"[CityConfig] '{name}': {reason}", this);
    }
#endif
}
