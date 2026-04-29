using UnityEngine;

/// <summary>
/// Tracks the bus's GPS position in WGS84 by back-projecting its Unity world
/// position through <see cref="CoordinateConverter"/>.
///
/// Fires <see cref="OnGpsUpdated"/> at a configurable interval (default 5 Hz)
/// so downstream systems (minimap, tile streaming, schedule) aren't polled
/// every frame.
///
/// Also exposes <see cref="HeadingDegrees"/> (0 = North, clockwise) so the
/// minimap icon can be oriented correctly.
/// </summary>
public class GpsTracker : MonoBehaviour
{
    public static GpsTracker Instance { get; private set; }

    // ── Inspector ─────────────────────────────────────────────────────

    [Header("References")]
    public Transform busTransform;
    public CoordinateConverter converter;

    [Header("Update Rate")]
    [Tooltip("How often to fire OnGpsUpdated (seconds). 0.2 = 5 Hz.")]
    [Range(0.05f, 2f)]
    public float updateInterval = 0.2f;

    // ── Read-only state ───────────────────────────────────────────────

    [Header("State (read-only)")]
    [SerializeField] double _currentLat;
    [SerializeField] double _currentLon;
    [SerializeField] float  _headingDeg;
    [SerializeField] float  _speedKmh;

    public double CurrentLat    => _currentLat;
    public double CurrentLon    => _currentLon;
    /// <summary>Compass bearing in degrees (0 = North, 90 = East).</summary>
    public float  HeadingDegrees => _headingDeg;
    /// <summary>Estimated speed in km/h based on world-space displacement.</summary>
    public float  SpeedKmh      => _speedKmh;

    // ── Events ────────────────────────────────────────────────────────

    /// <summary>Fired after each GPS position update.</summary>
    public event System.Action<double, double> OnGpsUpdated;
    /// <summary>Fired when heading changes by more than <see cref="headingChangeTolerance"/> degrees.</summary>
    public event System.Action<float> OnHeadingChanged;

    [Tooltip("Minimum heading change (degrees) that fires OnHeadingChanged.")]
    public float headingChangeTolerance = 2f;

    // ── Private ───────────────────────────────────────────────────────

    float   _timer;
    Vector3 _prevWorldPos;
    bool    _firstUpdate = true;

    // ── Lifecycle ─────────────────────────────────────────────────────

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        if (converter == null)
            converter = CoordinateConverter.Instance ?? FindFirstObjectByType<CoordinateConverter>();

        if (busTransform == null)
        {
            var bus = FindFirstObjectByType<BusController>();
            if (bus != null) busTransform = bus.transform;
        }

        if (busTransform != null)
            _prevWorldPos = busTransform.position;
    }

    void Update()
    {
        if (converter == null || busTransform == null) return;

        _timer += Time.deltaTime;
        if (_timer < updateInterval) return;

        float dt = _timer;
        _timer = 0f;

        Vector3 currentWorldPos = busTransform.position;

        // ── GPS position ──────────────────────────────────────────
        var gps = converter.WorldToGeoPosition(currentWorldPos);
        _currentLat = gps.lat;
        _currentLon = gps.lon;

        // ── Speed (km/h) ──────────────────────────────────────────
        float distanceMeters = Vector3.Distance(currentWorldPos, _prevWorldPos);
        _speedKmh = (distanceMeters / dt) * 3.6f;

        // ── Heading ───────────────────────────────────────────────
        if (!_firstUpdate && distanceMeters > 0.05f)
        {
            Vector3 dir = currentWorldPos - _prevWorldPos;
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.001f)
            {
                float newHeading = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                if (newHeading < 0f) newHeading += 360f;

                float delta = Mathf.Abs(Mathf.DeltaAngle(_headingDeg, newHeading));
                _headingDeg = newHeading;
                if (delta >= headingChangeTolerance)
                    OnHeadingChanged?.Invoke(_headingDeg);
            }
        }

        _prevWorldPos = currentWorldPos;
        _firstUpdate  = false;

        OnGpsUpdated?.Invoke(_currentLat, _currentLon);
    }

    // ── Public helpers ─────────────────────────────────────────────────

    /// <summary>Snapshot of current GPS as a value tuple.</summary>
    public (double lat, double lon) GetGpsPosition() => (_currentLat, _currentLon);

    /// <summary>
    /// True if the bus is within <paramref name="radiusMeters"/> of the
    /// given GPS coordinate (fast equirectangular approximation).
    /// </summary>
    public bool IsWithinRadius(double lat, double lon, float radiusMeters)
    {
        double dLat = (_currentLat - lat) * CoordinateConverter.MetersPerDegreeLat;
        double cosLat = System.Math.Cos(lat * System.Math.PI / 180.0);
        double dLon = (_currentLon - lon) * CoordinateConverter.MetersPerDegreeLat * cosLat;
        double distSqr = dLat * dLat + dLon * dLon;
        return distSqr <= (double)(radiusMeters * radiusMeters);
    }
}
