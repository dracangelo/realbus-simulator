using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The sky itself: clouds that answer to weather and drift on the wind, a star
/// field that only shows at night, a moon with its own dim light, and a
/// procedural skybox tinted by the time of day.
///
/// Before this component existed the weather system had nothing to draw the sky
/// with: <see cref="SkyController"/> only retinted the sun and ambient light, and
/// no scene ever assigned <see cref="RenderSettings.skybox"/>, so "overcast",
/// "storm" and "night" were changes in brightness with no weather to look at. The
/// cloud, star and moon textures are generated here rather than shipped, because
/// the project's other scenery (stop posters, traffic fallbacks) is built the same
/// way and the pack folders are known to be empty.
///
/// Everything is driven by pure mapping functions (<see cref="CloudCoverageFor"/>,
/// <see cref="StarVisibilityFor"/>, <see cref="CloudTintFor"/>, ...) so the rules
/// are testable without a rendered frame.
/// </summary>
public class SkyEnvironmentController : MonoBehaviour
{
    public static SkyEnvironmentController Instance { get; private set; }

    [Header("Dome")]
    [Tooltip("Cloud layer radius. Clamped to the camera's far clip plane at run time.")]
    public float cloudDomeRadius = 420f;
    [Tooltip("Star layer radius, slightly outside the clouds.")]
    public float starDomeRadius = 450f;
    public int domeSegments = 48;
    public int domeRings = 18;

    [Header("Clouds")]
    [Range(64, 512)] public int cloudTextureWidth = 256;
    [Tooltip("Grey scale of the noise used for cloud density.")]
    public float cloudNoiseScale = 4f;
    [Tooltip("Offset that turns noise into clear sky: below it there are no clouds at all.")]
    public float cloudNoiseBias = 0.42f;
    [Tooltip("How fast the layer drifts at full wind, in texture units per second.")]
    public float cloudDriftSpeed = 0.006f;
    public float minCloudAlpha = 0.0f;
    public float maxCloudAlpha = 0.95f;
    public Color dayCloudColor = new Color(0.99f, 0.99f, 1.00f);
    public Color nightCloudColor = new Color(0.13f, 0.15f, 0.23f);
    [Tooltip("How much a storm darkens the cloud layer on top of its weather colour.")]
    [Range(0f, 1f)] public float stormDarkening = 0.55f;

    [Header("Stars")]
    [Range(128, 1024)] public int starTextureWidth = 512;
    public int starCount = 900;
    public int starSeed = 20261014;
    [Tooltip("Stars fade in once the sun is this far below the horizon.")]
    public float starFadeSunElevation = 6f;

    [Header("Moon")]
    public float moonIntensity = 0.14f;
    public Color moonLightColor = new Color(0.72f, 0.78f, 0.95f);
    [Tooltip("Moon disc size as a fraction of the dome radius. The real moon is 0.008, so this is already a stylised enlargement.")]
    public float moonDiscAngularSize = 0.012f;

    [Header("Skybox")]
    public bool applySkybox = true;
    public float clearAtmosphereThickness = 0.85f;
    public float stormAtmosphereThickness = 1.85f;
    public float daySkyExposure = 1.15f;
    public float nightSkyExposure = 0.12f;

    [Header("Transition")]
    public float lerpSpeed = 0.6f;

    [Header("Runtime State")]
    [Tooltip("Cloud coverage the layer is fading toward (0 clear, 1 solid).")]
    public float targetCloudCoverage;
    [Tooltip("Cloud coverage currently drawn.")]
    public float cloudCoverage;
    [Tooltip("How dark the weather makes the layer, on top of the time of day.")]
    public float cloudDarkness;
    public float starVisibility;
    public float currentMoonIntensity;
    public float skyExposure;

    Transform domeRoot;
    Transform cloudDome;
    Transform starDome;
    Transform moonDisc;
    MeshRenderer cloudRenderer;
    MeshRenderer starRenderer;
    MeshRenderer moonRenderer;
    Material cloudMaterial;
    Material starMaterial;
    Material moonMaterial;
    Light moonLight;
    Vector2 cloudOffset;
    Camera trackedCamera;

    /// <summary>Wind in metres per second, set by whichever system knows it (weather, or a gust).</summary>
    public Vector2 windXZ = new Vector2(2.4f, 1.1f);

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static SkyEnvironmentController EnsureExists()
    {
        if (Instance != null) return Instance;
        var existing = FindAnyObjectByType<SkyEnvironmentController>();
        return existing != null ? existing : new GameObject("SkyEnvironment").AddComponent<SkyEnvironmentController>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        Build();
    }

    // ── Mapping rules (pure, so they can be tested without a frame) ───

    /// <summary>
    /// How much of the sky the weather covers. Clear sky is nearly cloudless,
    /// overcast is fully covered, and precipitation sits somewhere between the two
    /// with its intensity carried by the storm states.
    /// </summary>
    public static float CloudCoverageFor(WeatherState state, float intensity)
    {
        float baseline;
        switch (state)
        {
            case WeatherState.Clear:        baseline = 0.08f; break;
            case WeatherState.PartlyCloudy: baseline = 0.45f; break;
            case WeatherState.Overcast:     baseline = 0.92f; break;
            case WeatherState.Fog:          baseline = 0.65f; break;
            case WeatherState.Drizzle:      baseline = 0.80f; break;
            case WeatherState.LightRain:    baseline = 0.88f; break;
            case WeatherState.HeavyRain:    baseline = 1.00f; break;
            case WeatherState.Thunderstorm: baseline = 1.00f; break;
            case WeatherState.Snow:         baseline = 0.95f; break;
            case WeatherState.Blizzard:     baseline = 1.00f; break;
            default:                        baseline = 0.10f; break;
        }
        // Intensity only ever thickens the layer: light rain is less covered than
        // heavy rain of the same family, and nothing thins below its own baseline.
        return Mathf.Clamp01(Mathf.Lerp(baseline * 0.85f, baseline, Mathf.Clamp01(intensity)));
    }

    /// <summary>How much darker the weather makes the cloud layer.</summary>
    public static float CloudDarknessFor(WeatherState state, float intensity)
    {
        switch (state)
        {
            case WeatherState.Clear:        return 0f;
            case WeatherState.PartlyCloudy: return 0.10f * Mathf.Clamp01(intensity);
            case WeatherState.Overcast:     return 0.25f;
            case WeatherState.Fog:          return 0.30f;
            case WeatherState.Drizzle:      return 0.30f;
            case WeatherState.LightRain:    return 0.40f;
            case WeatherState.HeavyRain:    return Mathf.Lerp(0.45f, 0.65f, Mathf.Clamp01(intensity));
            case WeatherState.Thunderstorm: return 0.85f;
            case WeatherState.Snow:         return 0.25f;
            case WeatherState.Blizzard:     return 0.75f;
            default:                        return 0f;
        }
    }

    /// <summary>
    /// Star visibility. Stars fade in as the sun sinks past <paramref name="fadeElevation"/>
    /// and are hidden by clouds as well as daylight, which is why an overcast night
    /// stays dark instead of showing the star field the calendar would allow.
    /// </summary>
    public static float StarVisibilityFor(float sunElevationDegrees, float fadeElevation, float cloudCoverage)
    {
        float night = Mathf.InverseLerp(fadeElevation, -fadeElevation * 2f, sunElevationDegrees);
        return Mathf.Clamp01(night * (1f - Mathf.Clamp01(cloudCoverage) * 0.95f));
    }

    /// <summary>Skybox exposure: bright by day, nearly black at night, dimmed by cloud.</summary>
    public static float SkyExposureFor(float daylight01, float cloudCoverage, float dayExposure, float nightExposure)
    {
        float exposure = Mathf.Lerp(nightExposure, dayExposure, Mathf.Clamp01(daylight01));
        return exposure * Mathf.Lerp(1f, 0.55f, Mathf.Clamp01(cloudCoverage));
    }

    /// <summary>Moonlight: only at night, and muted by thick cloud.</summary>
    public static float MoonIntensityFor(bool isNight, float cloudCoverage, float fullIntensity)
        => isNight ? Mathf.Max(0f, fullIntensity) * Mathf.Lerp(1f, 0.15f, Mathf.Clamp01(cloudCoverage)) : 0f;

    /// <summary>
    /// Cloud colour: white by day, blue-grey at night, warmed by the sun during the
    /// twilight band, and darkened by the weather on top of all of it.
    /// </summary>
    public static Color CloudTintFor(float daylight01, Color skyTint, float darkness,
        Color dayColor, Color nightColor, float stormDarkening)
    {
        float daylight = Mathf.Clamp01(daylight01);
        Color tint = Color.Lerp(nightColor, dayColor, daylight);
        // Twilight warmth has to peak at dawn and dusk, not at noon, so it is the
        // product of daylight and darkness rather than either of them alone.
        float twilight = Mathf.Clamp01(daylight * (1f - daylight) * 4f);
        tint = Color.Lerp(tint, skyTint, twilight * 0.5f);
        tint *= 1f - Mathf.Clamp01(darkness) * Mathf.Clamp01(stormDarkening);
        tint.a = 1f;
        return tint;
    }

    /// <summary>Cloud drift for a frame: wind speed and direction turned into a texture offset.</summary>
    public static Vector2 CloudDriftDelta(Vector2 windXZ, float speed, float seconds)
        => windXZ * speed * Mathf.Max(0f, seconds);

    // ── Weather API ──────────────────────────────────────────────────

    /// <summary>
    /// Called by <see cref="WeatherSystem"/> for the state being applied, including
    /// mid-transition, so clouds cross-fade with the same curve as the fog.
    /// </summary>
    public void ApplyEnvironment(WeatherState state, float intensity)
    {
        targetCloudCoverage = CloudCoverageFor(state, intensity);
        cloudDarkness = CloudDarknessFor(state, intensity);
    }

    // ── Construction ─────────────────────────────────────────────────

    void Build()
    {
        if (domeRoot != null) return;

        Shader unlit = ResolveUnlitShader();
        if (unlit == null)
        {
            Debug.LogWarning("[SkyEnvironmentController] No unlit shader is available, so the cloud and star layers were not built.");
            return;
        }

        domeRoot = new GameObject("SkyEnvironment_Domes").transform;
        domeRoot.SetParent(transform, false);

        cloudMaterial = BuildLayerMaterial(unlit, BuildCloudTexture(), "SkyEnvironment_Clouds_Mat");
        cloudDome = BuildDome("SkyEnvironment_Clouds", cloudDomeRadius, cloudMaterial, out cloudRenderer);

        starMaterial = BuildLayerMaterial(unlit, BuildStarTexture(), "SkyEnvironment_Stars_Mat");
        starDome = BuildDome("SkyEnvironment_Stars", starDomeRadius, starMaterial, out starRenderer);
        SetMaterialColor(starMaterial, new Color(1f, 1f, 1f, 0f));

        moonMaterial = BuildLayerMaterial(unlit, BuildMoonTexture(), "SkyEnvironment_Moon_Mat");
        moonDisc = BuildMoonDisc(moonMaterial, out moonRenderer);

        moonLight = BuildMoonLight();
        ApplySkybox();
        ApplyEnvironment(WeatherState.Clear, 0f);
    }

    // Sky layers are drawn over the world, so they use the project's transparent
    // overlay shader: tint in _Color, ZWrite-off and cull-off.
    static Shader ResolveUnlitShader() => BuiltInShaders.Overlay;

    static Material BuildLayerMaterial(Shader shader, Texture2D texture, string name)
    {
        var material = new Material(shader) { name = name };
        if (material.HasProperty("_MainTex")) material.SetTexture("_MainTex", texture);
        SetMaterialColor(material, Color.white);
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        return material;
    }

    static void SetMaterialColor(Material material, Color color)
    {
        if (material == null) return;
        if (material.HasProperty("_Color")) material.SetColor("_Color", color);
    }

    /// <summary>
    /// A dome of triangles that face inwards, so the camera sits inside it. Winding
    /// is checked per triangle rather than assumed: a dome that culls its own
    /// inward faces is an invisible sky.
    /// </summary>
    Transform BuildDome(string name, float radius, Material material, out MeshRenderer renderer)
    {
        int segments = Mathf.Max(8, domeSegments);
        int rings = Mathf.Max(3, domeRings);
        const float skirtDegrees = 12f;

        var vertices = new List<Vector3>();
        var uvs = new List<Vector2>();
        var triangles = new List<int>();

        for (int ring = 0; ring <= rings; ring++)
        {
            float t = ring / (float)rings;
            float elevation = Mathf.Lerp(-skirtDegrees, 90f, t) * Mathf.Deg2Rad;
            float y = Mathf.Sin(elevation);
            float ringRadius = Mathf.Cos(elevation);
            for (int segment = 0; segment <= segments; segment++)
            {
                float u = segment / (float)segments;
                float azimuth = u * Mathf.PI * 2f;
                vertices.Add(new Vector3(Mathf.Cos(azimuth) * ringRadius, y, Mathf.Sin(azimuth) * ringRadius));
                uvs.Add(new Vector2(u, t));
            }
        }

        int stride = segments + 1;
        for (int ring = 0; ring < rings; ring++)
        for (int segment = 0; segment < segments; segment++)
        {
            int a = ring * stride + segment;
            int b = a + 1;
            int c = a + stride;
            int d = c + 1;
            AddInwardTriangle(vertices, triangles, a, c, b);
            AddInwardTriangle(vertices, triangles, b, c, d);
        }

        var mesh = new Mesh { name = name + "_Mesh" };
        mesh.SetVertices(vertices);
        mesh.SetUVs(0, uvs);
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateBounds();

        var go = new GameObject(name);
        go.transform.SetParent(domeRoot, false);
        go.transform.localScale = Vector3.one * radius;
        var filter = go.AddComponent<MeshFilter>();
        filter.sharedMesh = mesh;
        renderer = go.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = material;
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = false;
        return go.transform;
    }

    static void AddInwardTriangle(List<Vector3> vertices, List<int> triangles, int a, int b, int c)
    {
        Vector3 normal = Vector3.Cross(vertices[b] - vertices[a], vertices[c] - vertices[a]);
        Vector3 centroid = (vertices[a] + vertices[b] + vertices[c]) / 3f;
        // Facing the middle of the dome means the visible side is the inside.
        if (Vector3.Dot(normal, centroid) > 0f) { int swap = b; b = c; c = swap; }
        triangles.Add(a); triangles.Add(b); triangles.Add(c);
    }

    Transform BuildMoonDisc(Material material, out MeshRenderer renderer)
    {
        var go = new GameObject("SkyEnvironment_Moon");
        go.transform.SetParent(domeRoot, false);
        var filter = go.AddComponent<MeshFilter>();
        filter.sharedMesh = BuildQuad();
        renderer = go.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = material;
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = false;
        go.transform.localScale = Vector3.one * (moonDiscAngularSize * starDomeRadius * 2f);
        return go.transform;
    }

    /// <summary>Unit quad in the XY plane, facing -Z (the moon turns to face the dome centre).</summary>
    static Mesh BuildQuad()
    {
        var mesh = new Mesh { name = "SkyEnvironment_Quad_Mesh" };
        mesh.SetVertices(new List<Vector3>
        {
            new Vector3(-0.5f, -0.5f, 0f), new Vector3(0.5f, -0.5f, 0f),
            new Vector3(0.5f, 0.5f, 0f), new Vector3(-0.5f, 0.5f, 0f)
        });
        mesh.SetUVs(0, new List<Vector2> { new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(0f, 1f) });
        mesh.SetTriangles(new List<int> { 0, 1, 2, 0, 2, 3 }, 0);
        mesh.RecalculateBounds();
        return mesh;
    }

    Light BuildMoonLight()
    {
        var go = new GameObject("SkyEnvironment_MoonLight");
        go.transform.SetParent(transform, false);
        var light = go.AddComponent<Light>();
        light.type = LightType.Directional;
        light.color = moonLightColor;
        light.intensity = 0f;
        light.shadows = LightShadows.None;
        light.enabled = false;
        return light;
    }

    void ApplySkybox()
    {
        if (!applySkybox) return;
        Shader skyboxShader = BuiltInShaders.Skybox;
        if (skyboxShader == null) return;
        var material = new Material(skyboxShader) { name = "SkyEnvironment_Skybox" };
        if (material.HasProperty("_AtmosphereThickness")) material.SetFloat("_AtmosphereThickness", clearAtmosphereThickness);
        if (material.HasProperty("_SunSize")) material.SetFloat("_SunSize", 0.045f);
        if (material.HasProperty("_SunSizeConvergence")) material.SetFloat("_SunSizeConvergence", 4f);
        RenderSettings.skybox = material;
        if (RenderSettings.sun == null) RenderSettings.sun = FindSun();
    }

    static Light FindSun()
    {
        var lights = FindObjectsByType<Light>(FindObjectsInactive.Include);
        for (int i = 0; i < lights.Length; i++)
            if (lights[i] != null && lights[i].type == LightType.Directional) return lights[i];
        return null;
    }

    // ── Per-frame sky ────────────────────────────────────────────────

    void LateUpdate()
    {
        if (domeRoot == null) return;

        var clock = TimeOfDaySystem.Instance;
        float daylight = clock != null ? clock.GetDaylightFactor() : 0.75f;
        bool isNight = clock != null && clock.IsNight;
        float sunElevation = clock != null ? clock.SunElevationDegrees : 45f;
        Color skyTint = clock != null ? clock.GetSkyTint() : Color.white;
        Vector3 toSun = clock != null ? clock.GetSunDirection() : Vector3.up;

        float dt = Time.deltaTime;
        float blend = Mathf.Clamp01(dt * lerpSpeed);
        cloudCoverage = Mathf.Lerp(cloudCoverage, targetCloudCoverage, blend);
        float darkness = Mathf.Lerp(0f, cloudDarkness, blend);
        currentMoonIntensity = Mathf.Lerp(currentMoonIntensity,
            MoonIntensityFor(isNight, cloudCoverage, moonIntensity), blend);
        starVisibility = Mathf.Lerp(starVisibility,
            StarVisibilityFor(sunElevation, starFadeSunElevation, cloudCoverage), blend);
        skyExposure = Mathf.Lerp(skyExposure,
            SkyExposureFor(daylight, cloudCoverage, daySkyExposure, nightSkyExposure), blend);

        // Clouds drift with the wind; the layer wraps, so the offset never runs out.
        cloudOffset += CloudDriftDelta(windXZ, cloudDriftSpeed, dt);
        cloudOffset.x = Mathf.Repeat(cloudOffset.x, 1f);
        cloudOffset.y = Mathf.Repeat(cloudOffset.y, 1f);
        if (cloudRenderer != null && cloudRenderer.sharedMaterial != null)
            cloudRenderer.sharedMaterial.mainTextureOffset = cloudOffset;

        Color cloudTint = CloudTintFor(daylight, skyTint, cloudDarkness, dayCloudColor, nightCloudColor, stormDarkening);
        cloudTint.a = Mathf.Lerp(minCloudAlpha, maxCloudAlpha, cloudCoverage);
        if (cloudRenderer != null && cloudRenderer.sharedMaterial != null)
            SetMaterialColor(cloudRenderer.sharedMaterial, cloudTint);

        if (starRenderer != null && starRenderer.sharedMaterial != null)
            SetMaterialColor(starRenderer.sharedMaterial, new Color(1f, 1f, 1f, starVisibility));

        PositionMoon(toSun, isNight);
        ApplySkyboxFrame(skyTint, daylight, isNight);
        FollowCamera();
    }

    void PositionMoon(Vector3 toSun, bool isNight)
    {
        if (moonDisc == null) return;
        Vector3 toMoon = -toSun;
        if (toMoon.sqrMagnitude < 0.001f) toMoon = Vector3.up;
        toMoon.Normalize();
        moonDisc.localPosition = toMoon * starDomeRadius * 0.97f;
        // A quad in the XY plane faces -Z, so looking from the dome centre back at
        // the moon means facing the centre.
        moonDisc.localRotation = Quaternion.LookRotation(toMoon, Vector3.up);
        SetMaterialColor(moonMaterial, new Color(1f, 1f, 0.96f, isNight ? Mathf.Lerp(0f, 1f, starVisibility) : 0f));

        if (moonLight != null)
        {
            moonLight.intensity = currentMoonIntensity;
            moonLight.enabled = currentMoonIntensity > 0.001f;
            moonLight.transform.rotation = Quaternion.LookRotation(-toMoon, Vector3.up);
        }
    }

    void ApplySkyboxFrame(Color skyTint, float daylight, bool isNight)
    {
        Material skybox = RenderSettings.skybox;
        if (skybox == null || !applySkybox) return;
        if (skybox.name != "SkyEnvironment_Skybox") return;   // someone else owns the skybox

        Color tint = skyTint;
        if (isNight) tint = Color.Lerp(tint, new Color(0.05f, 0.06f, 0.14f), 0.65f);
        if (skybox.HasProperty("_SkyTint")) skybox.SetColor("_SkyTint", tint);
        if (skybox.HasProperty("_GroundColor"))
            skybox.SetColor("_GroundColor", Color.Lerp(new Color(0.10f, 0.11f, 0.13f), new Color(0.36f, 0.35f, 0.32f), daylight));
        if (skybox.HasProperty("_Exposure")) skybox.SetFloat("_Exposure", Mathf.Max(0.01f, skyExposure));
        if (skybox.HasProperty("_AtmosphereThickness"))
            skybox.SetFloat("_AtmosphereThickness",
                Mathf.Lerp(clearAtmosphereThickness, stormAtmosphereThickness, cloudCoverage));
        if (RenderSettings.sun == null) RenderSettings.sun = FindSun();
    }

    /// <summary>Keeps the dome centred on whoever is looking at it, at a size their camera can see.</summary>
    void FollowCamera()
    {
        if (trackedCamera == null || !trackedCamera.isActiveAndEnabled)
            trackedCamera = Camera.main != null ? Camera.main : FindAnyObjectByType<Camera>();
        if (trackedCamera == null)
        {
            domeRoot.position = Vector3.zero;
            return;
        }

        domeRoot.position = trackedCamera.transform.position;
        float reach = Mathf.Max(50f, trackedCamera.farClipPlane * 0.8f);
        float cloudScale = Mathf.Min(cloudDomeRadius, reach) / Mathf.Max(1f, cloudDomeRadius);
        float starScale = Mathf.Min(starDomeRadius, reach) / Mathf.Max(1f, starDomeRadius);
        if (cloudDome != null) cloudDome.localScale = Vector3.one * cloudDomeRadius * cloudScale;
        if (starDome != null) starDome.localScale = Vector3.one * starDomeRadius * starScale;
    }

    // ── Generated textures ───────────────────────────────────────────

    /// <summary>
    /// Cloud density as an alpha channel over white, so the skybox shows through
    /// the gaps. Tileable in both axes and seeded, so the layer can drift forever
    /// without a seam appearing and looks the same every run.
    /// </summary>
    Texture2D BuildCloudTexture()
    {
        int width = Mathf.Clamp(cloudTextureWidth, 64, 512);
        int height = Mathf.Max(32, width / 2);
        var texture = new Texture2D(width, height, TextureFormat.RGBA32, true)
        {
            name = "SkyEnvironment_Clouds_Tex",
            wrapMode = TextureWrapMode.Repeat,
            filterMode = FilterMode.Bilinear
        };

        var pixels = new Color32[width * height];
        for (int y = 0; y < height; y++)
        for (int x = 0; x < width; x++)
        {
            // Two scrolled samples of the same noise break up the repeat without
            // another lattice.
            float u = x / (float)width;
            float v = y / (float)height;
            float density = FractalNoise(u * cloudNoiseScale, v * cloudNoiseScale * 2f, 4, 8);
            float wisps = FractalNoise(u * cloudNoiseScale * 3.1f + 11.3f, v * cloudNoiseScale * 6.2f + 4.7f, 3, 16);
            float value = Mathf.Clamp01(density * 0.75f + wisps * 0.25f - cloudNoiseBias) / Mathf.Max(0.05f, 1f - cloudNoiseBias);
            byte alpha = (byte)Mathf.RoundToInt(Mathf.Clamp01(value * value * (3f - 2f * value)) * 255f);
            pixels[y * width + x] = new Color32(255, 255, 255, alpha);
        }

        texture.SetPixels32(pixels);
        texture.Apply(true, false);
        return texture;
    }

    /// <summary>A star field on transparent black, denser and dimmer near the horizon.</summary>
    Texture2D BuildStarTexture()
    {
        int width = Mathf.Clamp(starTextureWidth, 128, 1024);
        int height = width / 2;
        var texture = new Texture2D(width, height, TextureFormat.RGBA32, true)
        {
            name = "SkyEnvironment_Stars_Tex",
            wrapMode = TextureWrapMode.Repeat,
            filterMode = FilterMode.Bilinear
        };

        var pixels = new Color32[width * height];
        var random = new System.Random(starSeed);
        int stars = Mathf.Clamp(starCount, 0, width * height / 8);
        for (int i = 0; i < stars; i++)
        {
            float u = (float)random.NextDouble();
            float v = (float)random.NextDouble();
            // Stars thin out toward the horizon (v = 0 is below it), because haze
            // and light pollution do the same thing in reality.
            if (random.NextDouble() > Mathf.Lerp(0.35f, 1f, v)) continue;
            int cx = Mathf.Clamp(Mathf.RoundToInt(u * (width - 1)), 0, width - 1);
            int cy = Mathf.Clamp(Mathf.RoundToInt(v * (height - 1)), 0, height - 1);
            float brightness = Mathf.Lerp(0.45f, 1f, (float)random.NextDouble());
            int size = random.NextDouble() < 0.06f ? 2 : 1;
            for (int oy = 0; oy < size; oy++)
            for (int ox = 0; ox < size; ox++)
            {
                int px = (cx + ox) % width;
                int py = (cy + oy) % height;
                var existing = pixels[py * width + px];
                byte alpha = (byte)Mathf.Max(existing.a, Mathf.RoundToInt(brightness * 255f));
                pixels[py * width + px] = new Color32(255, 255, 255, alpha);
            }
        }

        texture.SetPixels32(pixels);
        texture.Apply(true, false);
        return texture;
    }

    /// <summary>A soft moon disc: bright core, faint halo, transparent outside it.</summary>
    Texture2D BuildMoonTexture()
    {
        const int size = 128;
        var texture = new Texture2D(size, size, TextureFormat.RGBA32, true)
        {
            name = "SkyEnvironment_Moon_Tex",
            wrapMode = TextureWrapMode.Clamp,
            filterMode = FilterMode.Bilinear
        };

        var pixels = new Color32[size * size];
        Vector2 centre = new Vector2((size - 1) * 0.5f, (size - 1) * 0.5f);
        float coreRadius = size * 0.28f;
        for (int y = 0; y < size; y++)
        for (int x = 0; x < size; x++)
        {
            float distance = Vector2.Distance(new Vector2(x, y), centre) / (size * 0.5f);
            float core = Mathf.Clamp01(1f - Mathf.InverseLerp(coreRadius / (size * 0.5f) - 0.12f, coreRadius / (size * 0.5f) + 0.06f, distance));
            float halo = Mathf.Clamp01(1f - Mathf.InverseLerp(0.2f, 1f, distance)) * 0.22f;
            float alpha = Mathf.Clamp01(core + halo);
            byte value = (byte)Mathf.RoundToInt(alpha * 255f);
            // The lit face keeps a hint of maria so it does not read as a flat disc.
            byte shade = (byte)Mathf.RoundToInt(core * Mathf.Lerp(215f, 255f, Mathf.PerlinNoise(x * 0.14f, y * 0.14f)));
            pixels[y * size + x] = new Color32(shade, shade, (byte)Mathf.Min(255, shade + 8), value);
        }

        texture.SetPixels32(pixels);
        texture.Apply(true, false);
        return texture;
    }

    /// <summary>Tileable value noise: the lattice wraps at <paramref name="period"/>, so the texture has no seam.</summary>
    static float FractalNoise(float x, float y, int octaves, int period)
    {
        float sum = 0f, amplitude = 1f, total = 0f;
        int currentPeriod = Mathf.Max(1, period);
        for (int octave = 0; octave < Mathf.Max(1, octaves); octave++)
        {
            sum += SampleNoise(x, y, currentPeriod) * amplitude;
            total += amplitude;
            amplitude *= 0.5f;
            x *= 2f; y *= 2f;
            currentPeriod *= 2;
        }
        return total > 0f ? sum / total : 0f;
    }

    static float SampleNoise(float x, float y, int period)
    {
        int x0 = Mathf.FloorToInt(x);
        int y0 = Mathf.FloorToInt(y);
        float fx = x - x0;
        float fy = y - y0;
        fx = fx * fx * (3f - 2f * fx);
        fy = fy * fy * (3f - 2f * fy);
        float v00 = Lattice(x0, y0, period);
        float v10 = Lattice(x0 + 1, y0, period);
        float v01 = Lattice(x0, y0 + 1, period);
        float v11 = Lattice(x0 + 1, y0 + 1, period);
        return Mathf.Lerp(Mathf.Lerp(v00, v10, fx), Mathf.Lerp(v01, v11, fx), fy);
    }

    static float Lattice(int x, int y, int period)
    {
        int px = ((x % period) + period) % period;
        int py = ((y % period) + period) % period;
        float value = Mathf.Sin(px * 12.9898f + py * 78.233f + 43.758f) * 43758.5453f;
        return value - Mathf.Floor(value);
    }
}
