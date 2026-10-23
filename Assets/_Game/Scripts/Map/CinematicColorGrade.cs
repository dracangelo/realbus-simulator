using UnityEngine;

/// <summary>
/// The exposure and colour grade for the built-in render pipeline.
///
/// The project deliberately does not depend on URP/HDRP or the post-processing
/// package: every material, sky layer and weather effect is authored for the
/// Built-in pipeline, and switching pipelines would invalidate all of them. That
/// left the brief's "carefully tuned exposure and colour grading" as the one part
/// of the cinematic look with no implementation. This is a single, cheap image
/// pass instead of a package: exposure, a small contrast/saturation grade, a
/// golden-hour warmth, a cool night balance and a soft vignette.
///
/// The grade is driven by the same clock and cloud cover as the sun, ambient and
/// reflections. It never tries to invent a look that disagrees with the weather:
/// an overcast noon stays neutral and slightly softer, a low sun gets the warm
/// highlight bias, and night is cooled rather than simply darkened.
///
/// It is disabled on the Very Low tier, where the game has already turned off
/// shadows and realtime probes. The pass is one full-screen draw with no extra
/// render targets.
/// </summary>
[DisallowMultipleComponent]
public class CinematicColorGrade : MonoBehaviour
{
    [Header("Exposure")]
    [Tooltip("Middle-grey exposure at a clear noon.")]
    [Range(0.75f, 1.35f)] public float baseExposure = 1.00f;
    [Tooltip("A little extra exposure at golden hour so the low sun reads as luminous rather than merely orange.")]
    [Range(0f, 0.25f)] public float goldenExposureBoost = 0.055f;
    [Tooltip("A small lift after dark keeps the cab and the road edge readable without turning night into dusk.")]
    [Range(0f, 0.25f)] public float nightExposureBoost = 0.075f;

    [Header("Colour Grade")]
    [Range(0.8f, 1.25f)] public float baseContrast = 1.06f;
    [Range(0.8f, 1.25f)] public float baseSaturation = 1.08f;
    [Tooltip("Warmth applied to the highlights around sunrise and sunset. Cloud removes most of it.")]
    [Range(0f, 0.25f)] public float goldenWarmth = 0.10f;
    [Range(0f, 0.12f)] public float shadowLift = 0.010f;
    [Range(0f, 0.35f)] public float baseVignette = 0.16f;
    [Tooltip("How quickly the grade follows a weather or time change. The lighting itself is already smoothed.")]
    public float adaptationSpeed = 4f;

    // Runtime state exposed for the smoke check and the pure-rule tests. These
    // are properties rather than Inspector fields: the grade is driven by the
    // clock and the sky, not hand-tuned at runtime.
    public float CurrentExposure { get; private set; } = 1f;
    public float CurrentContrast { get; private set; } = 1.06f;
    public float CurrentSaturation { get; private set; } = 1.08f;
    public float CurrentVignette { get; private set; } = 0.16f;
    public float CurrentShadowLift { get; private set; } = 0.01f;
    public Color CurrentTint { get; private set; } = Color.white;

    static readonly int ExposureId = Shader.PropertyToID("_Exposure");
    static readonly int ContrastId = Shader.PropertyToID("_Contrast");
    static readonly int SaturationId = Shader.PropertyToID("_Saturation");
    static readonly int TintId = Shader.PropertyToID("_Tint");
    static readonly int ShadowLiftId = Shader.PropertyToID("_ShadowLift");
    static readonly int VignetteId = Shader.PropertyToID("_Vignette");
    static readonly int VignetteStartId = Shader.PropertyToID("_VignetteStart");

    Material gradeMaterial;
    bool initialized;

    /// <summary>
    /// Attaches the grade to the active gameplay camera. Reusing the component
    /// keeps quality changes from stacking one image effect per scene load.
    /// </summary>
    public static CinematicColorGrade EnsureOn(Camera camera)
    {
        if (camera == null) return null;
        CinematicColorGrade grade = camera.GetComponent<CinematicColorGrade>();
        if (grade == null) grade = camera.gameObject.AddComponent<CinematicColorGrade>();
        grade.RefreshQuality();
        return grade;
    }

    /// <summary>
    /// The Very Low tier has no shadows, no anisotropic filtering and no realtime
    /// reflection probe. A full-screen pass is the next thing to go, so the cheap
    /// path stays genuinely cheap.
    /// </summary>
    public void RefreshQuality()
    {
        bool supported = QualitySettings.GetQualityLevel() > 0;
        enabled = supported;
        if (supported && !initialized) Initialize();
    }

    void OnEnable()
    {
        if (QualitySettings.GetQualityLevel() <= 0)
        {
            enabled = false;
            return;
        }

        Initialize();
        RefreshFromEnvironment(instant: true);
    }

    void OnDestroy()
    {
        if (gradeMaterial != null)
        {
            if (Application.isPlaying) Destroy(gradeMaterial);
            else DestroyImmediate(gradeMaterial);
            gradeMaterial = null;
        }
    }

    void Initialize()
    {
        if (initialized) return;

        // Resources guarantees the shader is shipped even though no scene material
        // references it directly. The Shader.Find fallback keeps the component
        // usable in a hand-authored test scene.
        Shader shader = Resources.Load<Shader>("Shaders/RealBusCinematicGrade");
        if (shader == null) shader = Shader.Find("Hidden/RealBus/CinematicGrade");
        if (shader == null)
        {
            Debug.LogWarning("[CinematicColorGrade] Cinematic grade shader was not found; the game will render without the grade.");
            enabled = false;
            return;
        }

        gradeMaterial = new Material(shader)
        {
            name = "CinematicGrade_Runtime",
            hideFlags = HideFlags.DontSave
        };
        initialized = true;
    }

    void Update()
    {
        if (!initialized) return;
        RefreshFromEnvironment(instant: false);
    }

    void RefreshFromEnvironment(bool instant)
    {
        TimeOfDaySystem clock = TimeOfDaySystem.Instance;
        SkyEnvironmentController sky = SkyEnvironmentController.Instance;

        float elevation = clock != null ? clock.SunElevationDegrees : 30f;
        float coverage = sky != null ? Mathf.Clamp01(sky.cloudCoverage) : 0f;
        Color skyTint = clock != null ? clock.GetSkyTint() : Color.white;

        float targetExposure = ExposureFor(elevation, coverage, baseExposure, goldenExposureBoost, nightExposureBoost);
        float targetContrast = ContrastFor(elevation, coverage, baseContrast);
        float targetSaturation = SaturationFor(elevation, coverage, baseSaturation);
        Color targetTint = TintFor(elevation, skyTint, coverage, goldenWarmth);
        float targetLift = ShadowLiftFor(elevation, coverage, shadowLift);
        float targetVignette = VignetteFor(elevation, coverage, baseVignette);

        float blend = instant ? 1f : 1f - Mathf.Exp(-Mathf.Max(0f, adaptationSpeed) * Time.unscaledDeltaTime);
        CurrentExposure = Mathf.Lerp(CurrentExposure, targetExposure, blend);
        CurrentContrast = Mathf.Lerp(CurrentContrast, targetContrast, blend);
        CurrentSaturation = Mathf.Lerp(CurrentSaturation, targetSaturation, blend);
        CurrentTint = Color.Lerp(CurrentTint, targetTint, blend);
        CurrentShadowLift = Mathf.Lerp(CurrentShadowLift, targetLift, blend);
        CurrentVignette = Mathf.Lerp(CurrentVignette, targetVignette, blend);

        if (gradeMaterial == null) return;
        gradeMaterial.SetFloat(ExposureId, CurrentExposure);
        gradeMaterial.SetFloat(ContrastId, CurrentContrast);
        gradeMaterial.SetFloat(SaturationId, CurrentSaturation);
        gradeMaterial.SetColor(TintId, CurrentTint);
        gradeMaterial.SetFloat(ShadowLiftId, CurrentShadowLift);
        gradeMaterial.SetFloat(VignetteId, CurrentVignette);
        gradeMaterial.SetFloat(VignetteStartId, 0.42f);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (gradeMaterial == null)
        {
            Graphics.Blit(source, destination);
            return;
        }

        Graphics.Blit(source, destination, gradeMaterial);
    }

    // ── Pure rules (checked without a rendered frame) ────────────────

    /// <summary>
    /// How much of the frame is lifted or held back. The lighting pass already
    /// dims the sun under cloud and at night; this is a gentle camera-side trim,
    /// not a second exposure system. Golden hour gets a little more light because
    /// the low sun is the one time the scene is meant to feel luminous.
    /// </summary>
    public static float ExposureFor(float sunElevation, float cloudCoverage, float baseExposure,
        float goldenBoost, float nightBoost)
    {
        float coverage = Mathf.Clamp01(cloudCoverage);
        float golden = GoldenWeight(sunElevation);
        float night = NightWeight(sunElevation);
        float exposure = Mathf.Max(0f, baseExposure)
            + Mathf.Max(0f, goldenBoost) * golden * Mathf.Lerp(1f, 0.45f, coverage)
            + Mathf.Max(0f, nightBoost) * night;
        // Cloud is already in the lighting. A tiny trim keeps the grade from
        // fighting the sky, while avoiding a second, stronger dimming.
        exposure *= Mathf.Lerp(1f, 0.98f, coverage);
        return Mathf.Clamp(exposure, 0.85f, 1.30f);
    }

    /// <summary>
    /// A low sun wants a touch more contrast and saturation; an overcast day is
    /// softer and more neutral. Night keeps contrast but loses a little colour.
    /// </summary>
    public static float ContrastFor(float sunElevation, float cloudCoverage, float baseContrast)
    {
        float golden = GoldenWeight(sunElevation);
        float coverage = Mathf.Clamp01(cloudCoverage);
        float night = NightWeight(sunElevation);
        return Mathf.Clamp(Mathf.Max(0.5f, baseContrast) + 0.035f * golden - 0.055f * coverage - 0.02f * night,
            0.75f, 1.30f);
    }

    public static float SaturationFor(float sunElevation, float cloudCoverage, float baseSaturation)
    {
        float coverage = Mathf.Clamp01(cloudCoverage);
        float night = NightWeight(sunElevation);
        return Mathf.Clamp(Mathf.Max(0f, baseSaturation)
            * Mathf.Lerp(1f, 0.90f, coverage)
            * Mathf.Lerp(1f, 0.88f, night), 0.60f, 1.35f);
    }

    /// <summary>
    /// A warm highlight bias around sunrise/sunset, neutral at noon, and a cool
    /// blue balance after dark. Cloud desaturates the bias toward white so a
    /// rainy day cannot look like a sepia postcard.
    /// </summary>
    public static Color TintFor(float sunElevation, Color skyTint, float cloudCoverage, float maxWarmth)
    {
        float coverage = Mathf.Clamp01(cloudCoverage);
        float warmth = GoldenWeight(sunElevation) * Mathf.Clamp01(maxWarmth) * Mathf.Lerp(1f, 0.35f, coverage);
        float night = NightWeight(sunElevation);

        // Keep the tint close to white. Multiplying a scene that is already lit
        // warm by a strong orange would clip the red channel on sunlit facades.
        Color warm = new Color(1f + warmth * 0.70f, 1f + warmth * 0.16f, 1f - warmth * 0.48f, 1f);
        Color cool = new Color(0.94f, 0.98f, 1.08f, 1f);
        Color tint = Color.Lerp(warm, cool, night * 0.70f);

        // The sky tint is already present in the sun, ambient and reflections.
        // Only a small amount belongs in the grade, and only while the sun is up.
        float skyInfluence = (1f - night) * 0.10f;
        tint = Color.Lerp(tint, new Color(tint.r * skyTint.r, tint.g * skyTint.g, tint.b * skyTint.b, 1f),
            skyInfluence);

        // Overcast light is grey; let the grade follow it instead of preserving
        // saturated colour that the sky is not providing.
        float grey = tint.r * 0.299f + tint.g * 0.587f + tint.b * 0.114f;
        tint = Color.Lerp(tint, new Color(grey, grey, grey, 1f), coverage * 0.25f);
        tint.a = 1f;
        return tint;
    }

    public static float ShadowLiftFor(float sunElevation, float cloudCoverage, float baseLift)
    {
        float coverage = Mathf.Clamp01(cloudCoverage);
        float night = NightWeight(sunElevation);
        return Mathf.Max(0f, baseLift) * Mathf.Lerp(1f, 1.35f, coverage) * Mathf.Lerp(1f, 0.55f, night);
    }

    public static float VignetteFor(float sunElevation, float cloudCoverage, float baseVignette)
    {
        float coverage = Mathf.Clamp01(cloudCoverage);
        float night = NightWeight(sunElevation);
        return Mathf.Clamp(Mathf.Max(0f, baseVignette) + 0.025f * coverage + 0.035f * night, 0f, 0.45f);
    }

    /// <summary>1 at the horizon, 0 once the sun is well up. Zero below the horizon.</summary>
    static float GoldenWeight(float sunElevation)
    {
        float aboveHorizon = Mathf.Clamp01(Mathf.InverseLerp(-2f, 3f, sunElevation));
        float elevated = Mathf.Clamp01(Mathf.InverseLerp(4f, 42f, Mathf.Max(0f, sunElevation)));
        return (1f - Mathf.SmoothStep(0f, 1f, elevated)) * aboveHorizon;
    }

    /// <summary>1 after dark, 0 once the sun is a few degrees up.</summary>
    static float NightWeight(float sunElevation)
        => 1f - Mathf.Clamp01(Mathf.InverseLerp(-8f, 4f, sunElevation));
}
