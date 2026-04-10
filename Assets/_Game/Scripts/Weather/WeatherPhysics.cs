using UnityEngine;

/// <summary>
/// Modifies WheelCollider friction curves based on active weather.
/// Attach to MapSystem alongside WeatherSystem.
/// </summary>
public class WeatherPhysics : MonoBehaviour
{
    public static WeatherPhysics Instance { get; private set; }

    [Header("References")]
    public BusController busController;

    [Header("Grip Multipliers")]
    [Range(0.1f, 1f)] public float clearGrip       = 1.00f;
    [Range(0.1f, 1f)] public float drizzleGrip      = 0.90f;
    [Range(0.1f, 1f)] public float lightRainGrip    = 0.78f;
    [Range(0.1f, 1f)] public float heavyRainGrip    = 0.62f;
    [Range(0.1f, 1f)] public float thunderGrip      = 0.55f;
    [Range(0.1f, 1f)] public float fogGrip          = 0.92f;
    [Range(0.1f, 1f)] public float snowGrip         = 0.45f;
    [Range(0.1f, 1f)] public float blizzardGrip     = 0.30f;
    [Range(0.1f, 1f)] public float overcastGrip     = 0.96f;

    [Header("Base Stiffness Values")]
    public float baseForwardStiffness  = 1.5f;
    public float baseSidewaysStiffness = 1.3f;

    [Header("State")]
    public float currentGripMultiplier = 1f;
    public string currentWeatherLabel  = "Clear";

    private float _targetGrip    = 1f;
    private float _previousGrip  = 1f;

    // Cache original friction curves
    private WheelFrictionCurve[] _originalForward;
    private WheelFrictionCurve[] _originalSideways;
    private bool _initialized = false;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this); return; }
        Instance = this;
    }

    void Start()
    {
        if (busController == null)
            busController = FindFirstObjectByType<BusController>();

        if (busController != null)
            CacheOriginalFriction();
    }

    void Update()
    {
        if (!_initialized) return;
        if (WeatherSystem.Instance == null) return;

        WeatherState state = WeatherSystem.Instance.currentWeather;
        float intensity    = WeatherSystem.Instance.weatherIntensity;

        _targetGrip        = GetGripForState(state, intensity);
        currentWeatherLabel = state.ToString();

        // Smooth transition — grip changes over 10 seconds
        currentGripMultiplier = Mathf.Lerp(
            currentGripMultiplier, _targetGrip, Time.deltaTime * 0.1f);

        // Only update wheels if grip changed meaningfully
        if (Mathf.Abs(currentGripMultiplier - _previousGrip) > 0.005f)
        {
            ApplyFriction(currentGripMultiplier);
            _previousGrip = currentGripMultiplier;
        }
    }

    // ── Grip Calculation ─────────────────────────────────────────────

    float GetGripForState(WeatherState state, float intensity)
    {
        switch (state)
        {
            case WeatherState.Clear:        return clearGrip;
            case WeatherState.PartlyCloudy: return Mathf.Lerp(clearGrip, overcastGrip, 0.5f);
            case WeatherState.Overcast:     return overcastGrip;
            case WeatherState.Fog:          return fogGrip;
            case WeatherState.Drizzle:      return drizzleGrip;

            case WeatherState.LightRain:
                return Mathf.Lerp(drizzleGrip, lightRainGrip, intensity);

            case WeatherState.HeavyRain:
                return Mathf.Lerp(lightRainGrip, heavyRainGrip, intensity);

            case WeatherState.Thunderstorm:
                return Mathf.Lerp(heavyRainGrip, thunderGrip, intensity);

            case WeatherState.Snow:
                return Mathf.Lerp(overcastGrip, snowGrip, intensity);

            case WeatherState.Blizzard:
                return Mathf.Lerp(snowGrip, blizzardGrip, intensity);

            default: return clearGrip;
        }
    }

    // ── Wheel Friction Application ───────────────────────────────────

    void CacheOriginalFriction()
    {
        var wheels = busController.allWheels;
        if (wheels == null || wheels.Length == 0) return;

        _originalForward  = new WheelFrictionCurve[wheels.Length];
        _originalSideways = new WheelFrictionCurve[wheels.Length];

        for (int i = 0; i < wheels.Length; i++)
        {
            if (wheels[i] == null) continue;

            // Set base stiffness first
            var fwd = wheels[i].forwardFriction;
            fwd.stiffness = baseForwardStiffness;
            wheels[i].forwardFriction = fwd;

            var side = wheels[i].sidewaysFriction;
            side.stiffness = baseSidewaysStiffness;
            wheels[i].sidewaysFriction = side;

            _originalForward[i]  = wheels[i].forwardFriction;
            _originalSideways[i] = wheels[i].sidewaysFriction;
        }

        _initialized = true;
        Debug.Log($"WeatherPhysics: Cached friction for {wheels.Length} wheels");
    }

    void ApplyFriction(float gripMultiplier)
    {
        var wheels = busController.allWheels;
        if (wheels == null) return;

        for (int i = 0; i < wheels.Length; i++)
        {
            if (wheels[i] == null) continue;

            var fwd = _originalForward[i];
            fwd.stiffness = baseForwardStiffness * gripMultiplier;
            wheels[i].forwardFriction = fwd;

            var side = _originalSideways[i];
            side.stiffness = baseSidewaysStiffness * gripMultiplier;
            wheels[i].sidewaysFriction = side;
        }
    }

    // ── Public API ───────────────────────────────────────────────────

    /// <summary>
    /// Returns a 0-1 warning level for HUD display.
    /// 0 = fine, 1 = very slippery
    /// </summary>
    public float GetSlipperyWarningLevel()
    {
        return 1f - currentGripMultiplier;
    }

    public bool IsDangerouslySlippery() => currentGripMultiplier < 0.5f;
}