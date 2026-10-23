using UnityEngine;

/// <summary>
/// The one authority for wind: how hard it blows, which way, and how it gusts,
/// derived from the weather state the HUD is already showing.
///
/// Before this existed the clouds drifted on a constant vector and every rain
/// streak fell along its emitter's own axis, so a still, clear morning and a
/// blizzard moved through the air in exactly the same way. The sky had weather in
/// its colours but not in its motion.
///
/// The mapping rules (<see cref="BaseSpeedFor"/>, <see cref="GustAt"/>,
/// <see cref="WindFor"/>) are pure, so the wind can be checked without a frame.
/// </summary>
public class WindSystem : MonoBehaviour
{
    public static WindSystem Instance { get; private set; }

    [Header("Prevailing Wind")]
    [Tooltip("Compass bearing the wind blows toward, in degrees (0 = north, 90 = east).")]
    public float prevailingDirectionDegrees = 245f;
    [Tooltip("Half-width of the slow swing either side of the prevailing bearing.")]
    public float directionSwingDegrees = 26f;
    [Tooltip("Seconds for one full direction swing. A wind that never veers reads as a fixed fan.")]
    public float directionPeriodSeconds = 430f;

    [Header("Strength (metres per second)")]
    [Tooltip("Clear, still air.")]
    public float calmSpeed = 1.4f;
    [Tooltip("A gentle breeze: partly cloudy sky and drizzle.")]
    public float breezeSpeed = 4.6f;
    [Tooltip("A windy overcast or a steady rain.")]
    public float windySpeed = 9.2f;
    [Tooltip("A thunderstorm or a blizzard: strong enough to slant the rain noticeably.")]
    public float stormSpeed = 17.5f;
    [Tooltip("Seconds the wind takes to settle after the weather changes state.")]
    public float responseSeconds = 7f;

    [Header("Gusts")]
    public bool gustsEnabled = true;
    [Tooltip("Peak gust as a fraction of the base speed (0.55 = 55% above it).")]
    [Range(0f, 1.5f)] public float gustStrength = 0.55f;
    [Tooltip("Seconds between gust peaks.")]
    public float gustPeriodSeconds = 9f;

    [Header("Wiring")]
    public SkyEnvironmentController sky;
    public RainController rain;
    public SnowController snow;

    /// <summary>Wind currently blowing, in metres per second; X = east, Y = north.</summary>
    public Vector2 WindXZ { get; private set; }
    public float SpeedMetersPerSecond { get; private set; }
    public float SpeedKmh => SpeedMetersPerSecond * 3.6f;
    /// <summary>Bearing the wind blows toward, in degrees.</summary>
    public float DirectionDegrees { get; private set; }
    /// <summary>Current gust, 0 at a lull and 1 at the peak of one.</summary>
    public float Gust01 { get; private set; }

    float gustPhase;
    float elapsed;
    float resolveTimer;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static WindSystem EnsureExists()
    {
        if (Instance != null) return Instance;
        WindSystem existing = FindAnyObjectByType<WindSystem>();
        return existing != null ? existing : new GameObject("WindSystem").AddComponent<WindSystem>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        SpeedMetersPerSecond = calmSpeed;
        DirectionDegrees = prevailingDirectionDegrees;
        WindXZ = DirectionVector(DirectionDegrees) * SpeedMetersPerSecond;
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    // ── Mapping rules (pure, so they can be tested without a frame) ───

    /// <summary>
    /// Base wind for a weather state. The state picks the band and the intensity
    /// fills it in, so a light drizzle stays a breeze while a heavy one does not
    /// jump straight to gale: fog is deliberately the stillest thing in the list,
    /// because fog only forms in air that has stopped moving.
    /// </summary>
    public static float BaseSpeedFor(WeatherState state, float intensity,
        float calm, float breeze, float windy, float storm)
    {
        float target;
        switch (state)
        {
            case WeatherState.Clear:        target = calm; break;
            case WeatherState.PartlyCloudy: target = Mathf.Lerp(calm, breeze, 0.65f); break;
            case WeatherState.Overcast:     target = breeze * 1.35f; break;
            case WeatherState.Fog:          target = calm * 1.15f; break;
            case WeatherState.Drizzle:      target = breeze; break;
            case WeatherState.LightRain:    target = Mathf.Lerp(breeze, windy, 0.45f); break;
            case WeatherState.HeavyRain:    target = windy; break;
            case WeatherState.Thunderstorm: target = storm; break;
            case WeatherState.Snow:         target = Mathf.Lerp(breeze, windy, 0.35f); break;
            case WeatherState.Blizzard:     target = storm; break;
            case WeatherState.Windy:        target = Mathf.Lerp(breeze, windy, 0.9f); break;
            default:                        target = calm; break;
        }

        float floor = Mathf.Max(0f, calm);
        return Mathf.Lerp(floor, Mathf.Max(floor, target), Mathf.Lerp(0.55f, 1f, Mathf.Clamp01(intensity)));
    }

    /// <summary>
    /// Gust envelope at a phase of the gust cycle. Two waves of different periods
    /// keep the peaks from arriving on a metronome, which is the difference
    /// between wind and an oscillating fan.
    /// </summary>
    public static float GustAt(float phase01, float strength)
    {
        float phase = Mathf.Repeat(phase01, 1f);
        float slow = Mathf.Sin(phase * Mathf.PI * 2f);
        float fast = Mathf.Sin(phase * Mathf.PI * 6.2f + 1.7f);
        float wave = Mathf.Clamp01(0.5f + 0.5f * (slow * 0.68f + fast * 0.32f));
        return Mathf.Clamp01(Mathf.Max(0f, strength)) * wave;
    }

    /// <summary>Unit vector on the ground plane for a compass bearing.</summary>
    public static Vector2 DirectionVector(float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        return new Vector2(Mathf.Sin(radians), Mathf.Cos(radians));
    }

    /// <summary>
    /// The wind vector: base speed lifted by the current gust, along a bearing.
    /// Kept pure so a caller can predict the wind for a given state.
    /// </summary>
    public static Vector2 WindFor(float speed, float directionDegrees, float gust01, float gustStrength)
    {
        float gusted = Mathf.Max(0f, speed) * (1f + Mathf.Clamp01(gust01) * Mathf.Max(0f, gustStrength));
        return DirectionVector(directionDegrees) * gusted;
    }

    // ── Per-frame ────────────────────────────────────────────────────

    void Start()
    {
        ResolveReferences();
        Apply();
    }

    void Update()
    {
        float dt = Mathf.Max(0f, Time.deltaTime);
        elapsed += dt;
        gustPhase = Mathf.Repeat(gustPhase + dt / Mathf.Max(0.05f, gustPeriodSeconds), 1f);

        WeatherSystem weather = WeatherSystem.Instance;
        WeatherState state = weather != null ? weather.currentWeather : WeatherState.Clear;
        float intensity = weather != null ? weather.weatherIntensity : 0f;
        float target = BaseSpeedFor(state, intensity, calmSpeed, breezeSpeed, windySpeed, stormSpeed);

        // Exponential approach, so a state change ramps the wind instead of
        // stepping it; the gust then rides on whatever the base has reached.
        float response = 1f - Mathf.Exp(-dt / Mathf.Max(0.05f, responseSeconds));
        SpeedMetersPerSecond = Mathf.Lerp(SpeedMetersPerSecond, target, response);

        DirectionDegrees = Mathf.Repeat(prevailingDirectionDegrees +
            directionSwingDegrees * Mathf.Sin(elapsed / Mathf.Max(1f, directionPeriodSeconds) * Mathf.PI * 2f), 360f);

        Gust01 = gustsEnabled ? GustAt(gustPhase, 1f) : 0f;
        WindXZ = WindFor(SpeedMetersPerSecond, DirectionDegrees, Gust01, gustsEnabled ? gustStrength : 0f);

        if (rain == null || snow == null)
        {
            resolveTimer -= dt;
            if (resolveTimer <= 0f) { resolveTimer = 2f; ResolveReferences(); }
        }

        Apply();
    }

    void ResolveReferences()
    {
        if (sky == null) sky = SkyEnvironmentController.Instance;
        if (rain == null) rain = FindAnyObjectByType<RainController>();
        if (snow == null) snow = FindAnyObjectByType<SnowController>();
    }

    void Apply()
    {
        // The sky takes metres per second and turns the vector into cloud drift.
        if (sky != null) sky.windXZ = WindXZ;
        if (rain != null) rain.ApplyWind(WindXZ);
        if (snow != null) snow.ApplyWind(WindXZ);
    }
}
