using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages cinematic lighting, soft directional shadows, warm daylight color grading,
/// ambient trilight bounce, and distance atmospheric haze for RealBus.
/// </summary>
public class CinematicAtmosphereController : MonoBehaviour
{
    public static CinematicAtmosphereController Instance { get; private set; }

    [Header("Cinematic Sun")]
    public Color warmSunColor = new Color(1.0f, 0.970f, 0.930f, 1.0f);
    public float sunIntensity = 1.20f;
    public LightShadows shadowType = LightShadows.Soft;
    public float shadowStrength = 0.88f;
    public float shadowBias = 0.035f;
    public float shadowNormalBias = 0.22f;

    [Header("Cinematic Atmosphere / Fog")]
    public bool enableAtmosphericHaze = true;
    public Color hazeColor = new Color(0.74f, 0.80f, 0.88f, 1.0f);
    public float hazeStartDistance = 140f;
    public float hazeEndDistance = 950f;
    [Tooltip("Haze colour once the sun is down. Daylight haze at midnight looks like milk, so the haze follows the same rule as the weather fog.")]
    public Color nightHazeTint = new Color(0.03f, 0.04f, 0.09f, 1.0f);
    [Range(0f, 1f)] public float nightHazeStrength = 0.75f;
    [Tooltip("Haze under thick cloud. A cloudy day is grey in the distance; a warm blue haze only belongs to a clear sky.")]
    public Color overcastHazeColor = new Color(0.62f, 0.64f, 0.66f, 1.0f);
    [Range(0f, 1f)] public float overcastHazeWeight = 0.85f;

    [Header("Ambient Trilight")]
    public Color ambientSky = new Color(0.64f, 0.72f, 0.84f, 1.0f);
    public Color ambientEquator = new Color(0.52f, 0.48f, 0.43f, 1.0f);
    public Color ambientGround = new Color(0.18f, 0.18f, 0.19f, 1.0f);

    [Tooltip("Ambient after dark. A night city is moonlit blue, not the noon trilight dimmed - dimming a daytime sky turns a street orange-brown and swallows the headlights.")]
    public Color nightAmbientSky = new Color(0.055f, 0.075f, 0.140f, 1.0f);
    public Color nightAmbientEquator = new Color(0.040f, 0.050f, 0.090f, 1.0f);
    public Color nightAmbientGround = new Color(0.015f, 0.020f, 0.030f, 1.0f);
    [Range(0f, 2f)] public float dayAmbientIntensity = 1.05f;
    [Range(0f, 1f)] public float nightAmbientIntensity = 0.28f;

    [Header("Reflections")]
    [Tooltip("How much of the sky reaches glossy surfaces at noon: bus clear-coat, glass, wet asphalt.")]
    [Range(0f, 1f)] public float dayReflectionIntensity = 0.55f;
    [Range(0f, 1f)] public float nightReflectionIntensity = 0.20f;
    [Tooltip("Seconds between lighting look-ups. The reflection cubemap itself is only rebuilt when the sky tint or the daylight has actually moved.")]
    public float lookRefreshSeconds = 0.25f;

    Light directionalSun;
    bool skyOwnsAmbientSky;

    /// <summary>How overcast the sky is right now (0 clear, 1 solid), smoothed by the sky controller.</summary>
    static float CloudCoverage()
        => SkyEnvironmentController.Instance != null ? SkyEnvironmentController.Instance.cloudCoverage : 0f;
    Color lastReflectionTint = Color.clear;
    float lastReflectionDaylight = -1f;
    float lookRefreshTimer;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static CinematicAtmosphereController EnsureExists()
    {
        if (Instance != null) return Instance;
        CinematicAtmosphereController existing = FindAnyObjectByType<CinematicAtmosphereController>();
        return existing != null ? existing : new GameObject("CinematicAtmosphereController").AddComponent<CinematicAtmosphereController>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        ApplyCinematicAtmosphere();
    }

    void OnDestroy()
    {
        if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        directionalSun = null;
        ApplyCinematicAtmosphere();
    }

    public void ApplyCinematicAtmosphere()
    {
        ConfigureSunLight();
        // RenderSettings belong to the scene, so a reload resets ambient mode, the
        // reflection source and the intensity. Everything has to be written again.
        skyOwnsAmbientSky = FindAnyObjectByType<SkyController>() != null;
        ConfigureAmbientLighting();
        ConfigureReflectionEnvironment(force: true);
        ConfigureAtmosphericHaze();
        ConfigureCameraFeatures();
    }

    void ConfigureSunLight()
    {
        if (directionalSun == null)
        {
            if (RenderSettings.sun != null)
                directionalSun = RenderSettings.sun;
            else
            {
                Light[] lights = FindObjectsByType<Light>(FindObjectsInactive.Include);
                for (int i = 0; i < lights.Length; i++)
                {
                    if (lights[i].type == LightType.Directional)
                    {
                        directionalSun = lights[i];
                        break;
                    }
                }
            }
        }

        if (directionalSun == null) return;

        RenderSettings.sun = directionalSun;
        directionalSun.color = warmSunColor;
        directionalSun.intensity = sunIntensity;
        directionalSun.shadows = shadowType;
        directionalSun.shadowResolution = LightShadowResolution.VeryHigh;
        directionalSun.shadowStrength = shadowStrength;
        directionalSun.shadowBias = shadowBias;
        directionalSun.shadowNormalBias = shadowNormalBias;

        // Cinematic afternoon angle: a 34° sun, which is a light X rotation of
        // 56° in Unity's convention. It casts long building and tree shadows across
        // the asphalt. This is only a fallback: the time-of-day clock owns the sun's
        // arc whenever it is running, and this pose used to fight it for the first
        // frame of every scene load.
        Vector3 rot = directionalSun.transform.rotation.eulerAngles;
        if (TimeOfDaySystem.Instance == null && (rot.x > 70f || rot.x < 15f))
        {
            directionalSun.transform.rotation = Quaternion.Euler(56f, 135f, 0f);
        }
    }

    void ConfigureAmbientLighting()
    {
        RenderSettings.ambientMode = AmbientMode.Trilight;
        ApplyAmbientLook(force: true);
    }

    /// <summary>
    /// How much light the sky is bouncing, from the sun's height rather than from the
    /// fraction of the day. Ambient does not fall with the sun's sine: golden hour is
    /// still bright, and the real fall happens once the sun is below the horizon.
    /// Using the daylight fraction instead put dusk at a quarter of noon, which is
    /// why an evening depot looked like midnight.
    /// </summary>
    public static float AmbientLightLevel(float sunElevationDegrees)
        => Mathf.SmoothStep(0f, 1f, Mathf.InverseLerp(-8f, 12f, sunElevationDegrees));

    /// <summary>
    /// Ambient intensity for a daylight level. Pure, so the night floor can be
    /// checked without a running clock.
    /// </summary>
    public static float AmbientIntensityFor(float daylight, float dayIntensity, float nightIntensity)
        // Ambient intensity is a multiplier and Unity allows it above one, so the
        // endpoints are floored at zero rather than clamped to one.
        => Mathf.Lerp(Mathf.Max(0f, nightIntensity), Mathf.Max(0f, dayIntensity), Mathf.Clamp01(daylight));

    /// <summary>
    /// Writes the ambient trilight as the clock lights it. The sky term is left to
    /// the weather controller while it is present: it owns that one channel and
    /// fights for it every frame, so writing it here too would only make both of
    /// them lerp against each other.
    /// </summary>
    /// <summary>The one light level every pass below should agree on.</summary>
    static float LightingLevel()
    {
        TimeOfDaySystem clock = TimeOfDaySystem.Instance;
        return clock != null ? AmbientLightLevel(clock.SunElevationDegrees) : 1f;
    }

    void ApplyAmbientLook(bool force = false)
    {
        TimeOfDaySystem clock = TimeOfDaySystem.Instance;
        float daylight = LightingLevel();
        Color skyTint = clock != null ? clock.GetSkyTint() : Color.white;

        // Clock first, weather second: an overcast noon is dimmer than a clear one,
        // and a storm at night is darker still, without either system overwriting
        // the other's work.
        RenderSettings.ambientIntensity = AmbientIntensityFor(daylight, dayAmbientIntensity, nightAmbientIntensity)
            * WeatherLighting.AmbientScale(CloudCoverage());

        Color equator = SkyReflectionEnvironment.AmbientFor(ambientEquator, nightAmbientEquator, skyTint, daylight);
        Color ground = SkyReflectionEnvironment.AmbientFor(ambientGround, nightAmbientGround, skyTint, daylight);
        if (force || ColorDistance(RenderSettings.ambientEquatorColor, equator) > 0.002f)
        {
            RenderSettings.ambientEquatorColor = equator;
            RenderSettings.ambientGroundColor = ground;
        }

        if (force || !skyOwnsAmbientSky)
            RenderSettings.ambientSkyColor = SkyReflectionEnvironment.AmbientFor(ambientSky, nightAmbientSky, skyTint, daylight);
    }

    /// <summary>
    /// Builds a reflection environment from the current sky and hands it to the
    /// renderer, so clear-coat, glass and wet asphalt mirror the sky that is actually
    /// overhead instead of the daylight probe Unity baked at startup.
    /// </summary>
    void ConfigureReflectionEnvironment(bool force = false)
    {
        TimeOfDaySystem clock = TimeOfDaySystem.Instance;
        // One level for the tint, the sun lobe and the intensity: a reflection that
        // is warm on one axis and night-dark on another is what a half-converted
        // day/night pass looks like.
        float level = LightingLevel();
        Color skyTint = clock != null ? clock.GetSkyTint() : Color.white;
        float coverage = CloudCoverage();
        // A cloudy sky is both dimmer and greyer to a glossy surface, so the tint is
        // desaturated by the same cover that dims the intensity.
        Color tint = WeatherLighting.Desaturate(SkyReflectionEnvironment.ReflectionTint(skyTint, level), coverage);
        float intensity = SkyReflectionEnvironment.IntensityFor(level, dayReflectionIntensity, nightReflectionIntensity)
            * WeatherLighting.ReflectionScale(coverage);

        if (force || SkyReflectionEnvironment.ShouldRebuild(lastReflectionTint, lastReflectionDaylight, tint, level))
        {
            Vector3 sunDirection = clock != null
                ? clock.GetSunDirection()
                : RenderSettings.sun != null ? -RenderSettings.sun.transform.forward : Vector3.up;

            Cubemap cubemap = SkyReflectionEnvironment.Build(tint, level, sunDirection);
            SkyReflectionEnvironment.Adopt(cubemap);
            lastReflectionTint = tint;
            lastReflectionDaylight = level;
        }

        // The intensity is a plain render setting, so it is written every refresh even
        // when the cube itself is unchanged: weather moves it without moving the tint
        // enough to justify rebuilding 32 pixels per face.
        SkyReflectionEnvironment.Apply(SkyReflectionEnvironment.Active, intensity);
    }

    static float ColorDistance(Color a, Color b)
        => Mathf.Abs(a.r - b.r) + Mathf.Abs(a.g - b.g) + Mathf.Abs(a.b - b.b);

    void ConfigureAtmosphericHaze()
    {
        ApplyAtmosphericHaze();
    }

    /// <summary>
    /// Distance haze for a clear sky. Weather fog owns <see cref="RenderSettings.fog"/>
    /// while it is active, so the haze stands down then and is applied again once the
    /// weather clears. Without that hand-off the haze cancelled storm fog, and the
    /// first shower cancelled the haze for the rest of the session.
    /// </summary>
    public void ApplyAtmosphericHaze()
    {
        if (!enableAtmosphericHaze) return;
        if (!HazeMayOwnFogNow()) return;

        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = hazeStartDistance;
        RenderSettings.fogEndDistance = hazeEndDistance;
        RenderSettings.fogColor = TintedHazeColor();
    }

    public bool HazeMayOwnFogNow()
    {
        FogController weatherFog = FogController.Instance;
        WeatherState weather = WeatherSystem.Instance != null ? WeatherSystem.Instance.currentWeather : WeatherState.Clear;
        return HazeMayOwnFog(weatherFog != null && weatherFog.WeatherFogActive, weather);
    }

    /// <summary>
    /// The hand-off rule: the haze belongs to clear, dry weather and to nobody else's
    /// fog. Pure, so the rule can be tested without a frame.
    /// </summary>
    public static bool HazeMayOwnFog(bool weatherFogActive, WeatherState weather)
        => !weatherFogActive && (weather == WeatherState.Clear || weather == WeatherState.PartlyCloudy);

    /// <summary>
    /// The haze as the sky lights it, so the same haze reads as distance at noon and
    /// near-black at midnight. Shares the weather fog's rule, because fog is fog.
    /// </summary>
    public Color TintedHazeColor()
    {
        TimeOfDaySystem clock = TimeOfDaySystem.Instance;
        float coverage = CloudCoverage();
        Color baseHaze = Color.Lerp(hazeColor, overcastHazeColor, coverage * overcastHazeWeight);
        if (clock == null) return baseHaze;
        return FogController.TintForDaylight(baseHaze, clock.GetSkyTint(), clock.GetDaylightFactor(), nightHazeTint, nightHazeStrength);
    }

    void Update()
    {
        // The haze is applied on scene load, so its colour has to be refreshed while
        // the sun moves: otherwise dawn would keep the midnight haze and vice versa.
        if (enableAtmosphericHaze && HazeMayOwnFogNow())
        {
            Color tint = TintedHazeColor();
            if (RenderSettings.fogColor != tint) RenderSettings.fogColor = tint;
        }

        // Ambient and reflections follow the clock at a deliberately low rate. The
        // sun moves over hours of play; per-frame updates would only churn render
        // settings the GPU has already consumed.
        lookRefreshTimer -= Time.unscaledDeltaTime;
        if (lookRefreshTimer > 0f) return;
        lookRefreshTimer = Mathf.Max(0.05f, lookRefreshSeconds);

        ApplyAmbientLook();
        ConfigureReflectionEnvironment();
    }

    void ConfigureCameraFeatures()
    {
        Camera cam = Camera.main != null ? Camera.main : FindAnyObjectByType<Camera>();
        if (cam != null)
        {
            cam.depthTextureMode |= DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
            // Depth precision is set by the near/far ratio. The old 0.05–4000 m pair
            // left so little precision at road distance that the asphalt and the
            // ground tile underneath it could swap depth from one frame to the next
            // - the ground flicker. The gameplay camera sits 12.5 m behind the bus,
            // so 0.3 m is still comfortably ahead of the nearest bodywork. The far
            // plane is now the streaming envelope: the city exists to 600-800 m,
            // clear haze is opaque by 950 m, and drawing the empty ring beyond it
            // only paid for culled ground and distant facades.
            cam.nearClipPlane = Mathf.Clamp(cam.nearClipPlane, 0.3f, 0.5f);
            cam.farClipPlane = SceneryLodPolicy.ClampFarClip(cam.farClipPlane);
            // The adaptive resolution used by MobilePerformanceManager only takes
            // effect when the camera opts in; without this the buffer resize was a
            // no-op and a thermally throttled phone kept rendering at full size.
            if (Application.isMobilePlatform) cam.allowDynamicResolution = true;

            // Exposure and colour grading are the one part of the cinematic brief
            // the built-in pipeline does not provide on its own. The pass reads the
            // same clock and cloud cover as the sun, ambient and reflections, so it
            // cannot disagree with the weather. It is disabled on the Very Low tier.
            CinematicColorGrade.EnsureOn(cam);
        }
    }
}
