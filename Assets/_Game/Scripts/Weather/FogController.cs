using UnityEngine;

public class FogController : MonoBehaviour
{
    public static FogController Instance { get; private set; }

    [Header("Visibility Distances (metres)")]
    public float clearVisibility       = 2000f;
    public float overcastVisibility    = 1200f;
    public float drizzleVisibility     =  800f;
    public float lightRainVisibility   =  500f;
    public float heavyRainVisibility   =  250f;
    public float thunderVisibility     =  150f;
    public float lightFogVisibility    =  120f;
    public float heavyFogVisibility    =   40f;
    public float snowVisibility        =  400f;
    public float blizzardVisibility    =   60f;

    [Header("Clear Fog Start/End (for tests)")]
    public float clearFogStart = 200f;
    public float clearFogEnd = 2000f;

    [Header("Fog Colors")]
    public Color clearFogColor    = new Color(0.78f, 0.84f, 0.92f);
    [Tooltip("Overcast is a grey ceiling, not a dim clear day: the distance has to lose its blue with the sky.")]
    public Color overcastFogColor = new Color(0.62f, 0.64f, 0.66f);
    public Color rainFogColor     = new Color(0.35f, 0.38f, 0.44f);
    public Color thunderFogColor  = new Color(0.15f, 0.15f, 0.20f);
    public Color fogFogColor      = new Color(0.72f, 0.72f, 0.72f);
    public Color snowFogColor     = new Color(0.85f, 0.87f, 0.92f);
    public Color blizzardFogColor = new Color(0.70f, 0.72f, 0.78f);

    [Header("Time of Day")]
    [Tooltip("Fog is lit by the sky, so it darkens at night. Resolved automatically when unset.")]
    public TimeOfDaySystem timeOfDay;
    public Color nightFogTint = new Color(0.03f, 0.04f, 0.09f, 1f);
    [Range(0f, 1f)] public float nightFogStrength = 0.75f;

    [Header("Transition Speed")]
    public float lerpSpeed = 0.4f;
    public float maxFogDensity = 0.03f;

    private float targetFogStart;
    private float targetFogEnd;
    private float targetFogDensity;
    private Color targetFogColor;
    private bool fogActive;
    Coroutine fadeRoutine;
    bool blending;

    /// <summary>
    /// True while weather fog owns <see cref="RenderSettings.fog"/>. The clear-sky
    /// haze asks this before it applies, so the two do not overwrite each other.
    /// </summary>
    public bool WeatherFogActive => fogActive;

    /// <summary>The colour the fog is currently fading toward; also what the smoke check reports.</summary>
    public Color TargetFogColor => targetFogColor;

    /// <summary>The visibility the fog is fading toward, in metres.</summary>
    public float TargetVisibility => targetFogEnd;

    void Awake()
    {
        if (Instance != null && Instance != this) return;
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    void Start()
    {
        RenderSettings.fog     = false;
        RenderSettings.fogMode = FogMode.Linear;

        targetFogStart = clearFogStart;
        targetFogEnd   = clearFogEnd;
        targetFogDensity = 0f;
        targetFogColor = clearFogColor;
    }

    void Update()
    {
        if (!fogActive) return;

        RenderSettings.fogStartDistance = Mathf.Lerp(
            RenderSettings.fogStartDistance, targetFogStart, Time.deltaTime * lerpSpeed);

        RenderSettings.fogEndDistance = Mathf.Lerp(
            RenderSettings.fogEndDistance,   targetFogEnd,   Time.deltaTime * lerpSpeed);

        RenderSettings.fogDensity = Mathf.Lerp(
            RenderSettings.fogDensity, targetFogDensity, Time.deltaTime * lerpSpeed);

        RenderSettings.fogColor = Color.Lerp(
            RenderSettings.fogColor, CurrentTint(), Time.deltaTime * lerpSpeed * 0.5f);
    }

    /// <summary>
    /// The weather's fog colour as it looks at the current time of day. Fog is lit by
    /// the sky: the same fog that reads as haze at noon has to go nearly black at
    /// night, and it picks up the sunrise/sunset tint on the way through.
    /// </summary>
    public static Color TintForDaylight(Color fogColor, Color skyTint, float daylight01, Color nightTint, float nightStrength)
    {
        float daylight = Mathf.Clamp01(daylight01);
        Color night = Color.Lerp(fogColor, nightTint, Mathf.Clamp01(nightStrength));
        Color day = Color.Lerp(fogColor, skyTint, 0.35f);
        return Color.Lerp(night, day, daylight);
    }

    Color CurrentTint()
    {
        if (timeOfDay == null) timeOfDay = TimeOfDaySystem.Instance;
        if (timeOfDay == null) return targetFogColor;
        return TintForDaylight(targetFogColor, timeOfDay.GetSkyTint(), timeOfDay.GetDaylightFactor(), nightFogTint, nightFogStrength);
    }

    public void SetFog(WeatherState state, float intensity)
    {
        if (fadeRoutine != null) { StopCoroutine(fadeRoutine); fadeRoutine = null; }
        switch (state)
        {
            case WeatherState.Clear:
                targetFogStart = clearFogStart;
                targetFogEnd = clearFogEnd;
                targetFogDensity = 0f;
                targetFogColor = clearFogColor;
                if (!blending) fadeRoutine = StartCoroutine(FadeOutFog(3f));
                return;

            case WeatherState.PartlyCloudy:
                SetLinear(clearVisibility * 0.8f, Color.Lerp(clearFogColor, overcastFogColor, 0.45f));
                break;

            case WeatherState.Overcast:
                SetLinear(overcastVisibility, overcastFogColor);
                break;

            case WeatherState.Windy:
                // A gusty dry day is still hazy, but not as closed in as an overcast.
                SetLinear(clearVisibility * 0.72f, Color.Lerp(clearFogColor, overcastFogColor, 0.55f));
                break;

            case WeatherState.Drizzle:
                SetLinear(drizzleVisibility, Color.Lerp(clearFogColor, rainFogColor, 0.4f));
                break;

            case WeatherState.LightRain:
                SetLinear(lightRainVisibility, rainFogColor);
                break;

            case WeatherState.HeavyRain:
                SetLinear(
                    Mathf.Lerp(lightRainVisibility, heavyRainVisibility, intensity),
                    Color.Lerp(rainFogColor, thunderFogColor, intensity * 0.4f));
                break;

            case WeatherState.Thunderstorm:
                SetLinear(thunderVisibility, thunderFogColor);
                break;

            case WeatherState.Fog:
                SetLinear(
                    Mathf.Lerp(lightFogVisibility, heavyFogVisibility, intensity),
                    fogFogColor);
                break;

            case WeatherState.Snow:
                SetLinear(snowVisibility, snowFogColor);
                break;

            case WeatherState.Blizzard:
                SetLinear(blizzardVisibility, blizzardFogColor);
                break;
        }
    }

    public void BlendWeather(WeatherState from, float fromIntensity, WeatherState to, float toIntensity, float t)
    {
        blending = true;
        SetFog(from, fromIntensity);
        float start = targetFogStart, end = targetFogEnd, density = targetFogDensity;
        Color tint = targetFogColor;
        SetFog(to, toIntensity);
        blending = false;
        if (fadeRoutine != null) { StopCoroutine(fadeRoutine); fadeRoutine = null; }
        targetFogStart = Mathf.Lerp(start, targetFogStart, t);
        targetFogEnd = Mathf.Lerp(end, targetFogEnd, t);
        targetFogDensity = Mathf.Lerp(density, targetFogDensity, t);
        targetFogColor = Color.Lerp(tint, targetFogColor, t);
        fogActive = true; RenderSettings.fog = true;
    }

    // ── Helpers ──────────────────────────────────────────────────────

    void SetLinear(float visibility, Color color)
    {
        fogActive = true;
        RenderSettings.fog     = true;
        RenderSettings.fogMode = FogMode.Linear;

        // Fog starts at 10% of visibility, ends at 100%
        targetFogStart = visibility * 0.10f;
        targetFogEnd   = visibility;
        targetFogDensity = Mathf.Lerp(0.001f, maxFogDensity, Mathf.InverseLerp(clearVisibility, heavyFogVisibility, visibility));
        targetFogColor = color;
    }

    System.Collections.IEnumerator FadeOutFog(float duration)
    {
        float elapsed = 0f;
        float startEnd = RenderSettings.fogEndDistance;
        // Clear weather hands the fog over to the city's distance haze, so it fades
        // toward the haze instead of to a plainer 2 km and snapping back at the end.
        var haze = CinematicAtmosphereController.Instance;
        float targetEnd = haze != null && haze.enableAtmosphericHaze ? haze.hazeEndDistance : clearVisibility;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            RenderSettings.fogEndDistance = Mathf.Lerp(startEnd, targetEnd, t);
            yield return null;
        }

        fogActive = false;
        RenderSettings.fogStartDistance = clearFogStart;
        RenderSettings.fogEndDistance = clearFogEnd;
        RenderSettings.fogColor = CurrentTint();
        RenderSettings.fogDensity = 0f;
        RenderSettings.fog = false;

        // Hand the fog back now that the weather has cleared. Without this the first
        // shower removed the city's distance haze for the rest of the session, and
        // the scene kept running on whatever the weather left behind.
        CinematicAtmosphereController.Instance?.ApplyAtmosphericHaze();
    }
}
