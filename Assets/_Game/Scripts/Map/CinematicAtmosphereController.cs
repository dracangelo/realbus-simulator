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
    public Color warmSunColor = new Color(1.0f, 0.955f, 0.885f, 1.0f);
    public float sunIntensity = 1.25f;
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

    [Header("Ambient Trilight")]
    public Color ambientSky = new Color(0.64f, 0.72f, 0.84f, 1.0f);
    public Color ambientEquator = new Color(0.52f, 0.48f, 0.43f, 1.0f);
    public Color ambientGround = new Color(0.18f, 0.18f, 0.19f, 1.0f);

    Light directionalSun;

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
        ConfigureAmbientLighting();
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

        // Cinematic afternoon angle (~34° elevation, ~135° azimuth) casts beautiful
        // long building and tree shadows across the asphalt roads. It is only a
        // fallback: the time-of-day clock owns the sun's arc whenever it is running,
        // and this pose used to fight it for the first frame of every scene load.
        Vector3 rot = directionalSun.transform.rotation.eulerAngles;
        if (TimeOfDaySystem.Instance == null && (rot.x > 60f || rot.x < 15f))
        {
            directionalSun.transform.rotation = Quaternion.Euler(34f, 135f, 0f);
        }
    }

    void ConfigureAmbientLighting()
    {
        RenderSettings.ambientMode = AmbientMode.Trilight;
        RenderSettings.ambientSkyColor = ambientSky;
        RenderSettings.ambientEquatorColor = ambientEquator;
        RenderSettings.ambientGroundColor = ambientGround;
        RenderSettings.ambientIntensity = 1.05f;
    }

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
        if (clock == null) return hazeColor;
        return FogController.TintForDaylight(hazeColor, clock.GetSkyTint(), clock.GetDaylightFactor(), nightHazeTint, nightHazeStrength);
    }

    void Update()
    {
        // The haze is applied on scene load, so its colour has to be refreshed while
        // the sun moves: otherwise dawn would keep the midnight haze and vice versa.
        if (!enableAtmosphericHaze || !HazeMayOwnFogNow()) return;
        Color tint = TintedHazeColor();
        if (RenderSettings.fogColor != tint) RenderSettings.fogColor = tint;
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
            // so 0.3 m is still comfortably ahead of the nearest bodywork, and the
            // fog closes well before 2.5 km.
            cam.nearClipPlane = Mathf.Clamp(cam.nearClipPlane, 0.3f, 0.5f);
            cam.farClipPlane = Mathf.Clamp(cam.farClipPlane, 1500f, 2500f);
            // The adaptive resolution used by MobilePerformanceManager only takes
            // effect when the camera opts in; without this the buffer resize was a
            // no-op and a thermally throttled phone kept rendering at full size.
            if (Application.isMobilePlatform) cam.allowDynamicResolution = true;
        }
    }
}
