using UnityEngine;
using System.Collections;

public class RainController : MonoBehaviour
{
    [Header("Rain Particles")]
    public ParticleSystem rainParticles;
    public ParticleSystem drizzleParticles;
    public ParticleSystem thunderstormParticles;

    [Header("Rain Sound")]
    public AudioSource rainAudio;
    public AudioClip lightRainClip;
    public AudioClip heavyRainClip;
    public AudioClip thunderClip;

    [Header("Wet Road")]
    public Material roadMaterial;
    public Material[] roadMaterials;
    public Renderer[] wetRoadRenderers;
    public Material wetSpecularMaterial;
    public string wetBlendProperty = "_WetBlend";

    [Header("Windscreen")]
    public GameObject windscreenWipers;
    public Animator wiperAnimator;
    public string wiperSpeedParameter = "WiperSpeed";
    public float minWiperSpeed = 0.75f;
    public float maxWiperSpeed = 2.25f;

    [Header("Generated Effects")]
    [Tooltip("Build the rain systems at run time when the scene provides none. No scene ships rain particles, so without this the weather system could only offer fog and a wet road.")]
    public bool buildEffectsWhenMissing = true;
    [Tooltip("The shower follows this transform so the bus is always inside it. The bus or the main camera is resolved when unset.")]
    public Transform followTarget;
    [Tooltip("Height of the emitting box above the followed transform.")]
    public float followHeight = 18f;
    [Tooltip("Side of the square the rain falls through, in metres, centred on the emitter.")]
    public float effectFieldSize = 60f;

    [Header("Wind")]
    [Tooltip("Wind in metres per second (X = east, Y = north), pushed in by the wind system. Rain leans with it.")]
    public Vector2 windXZ;

    [Header("Night Light")]
    [Tooltip("Streak colour in daylight.")]
    public Color dayRainColor = new Color(0.72f, 0.78f, 0.86f, 0.55f);
    [Tooltip("Streak colour after dark. Rain is drawn unlit, so without this the same streaks glow white at midnight.")]
    public Color nightRainColor = new Color(0.28f, 0.33f, 0.44f, 0.42f);

    Texture2D streakTexture;
    Material rainMaterial;

    // Fall speed per generated system. It is no longer the emitter's start speed:
    // fall is applied as world-space velocity so the wind can lean it.
    readonly System.Collections.Generic.Dictionary<ParticleSystem, float> fallSpeedBySystem =
        new System.Collections.Generic.Dictionary<ParticleSystem, float>();

    private float currentWetness = 0f;
    float lastAppliedWetness = -1f;
    Color lastRainTint = Color.clear;
    private WeatherState currentRainState = WeatherState.Clear;
    private Coroutine dryRoadRoutine;
    private Coroutine thunderRoutine;
    private MaterialPropertyBlock wetRoadPropertyBlock;

    void Start()
    {
        if (wetRoadPropertyBlock == null)
            wetRoadPropertyBlock = new MaterialPropertyBlock();
        if (buildEffectsWhenMissing) EnsureEffects();
        StopAll();
    }

    void Update()
    {
        FollowTarget();
        UpdateTint();
    }

    /// <summary>
    /// Fades the shower with the sky. The streak material is unlit - it has to be,
    /// or rain would be invisible at night - so it needs to be tinted by the light
    /// that is actually available instead of always being drawn at noon brightness.
    /// </summary>
    void UpdateTint()
    {
        if (rainMaterial == null) return;

        TimeOfDaySystem clock = TimeOfDaySystem.Instance;
        float light = clock != null ? SkyEnvironmentController.SkyLightLevelFor(clock.SunElevationDegrees) : 1f;
        Color tint = TintForLight(dayRainColor, nightRainColor, light);
        if (tint == lastRainTint) return;

        lastRainTint = tint;
        if (rainMaterial.HasProperty("_Color")) rainMaterial.SetColor("_Color", tint);
    }

    /// <summary>Precipitation colour for a light level. Pure, so the night floor is testable.</summary>
    public static Color TintForLight(Color day, Color night, float lightLevel01)
        => Color.Lerp(night, day, Mathf.Clamp01(lightLevel01));

    /// <summary>
    /// Creates the drizzle, rain and thunderstorm systems when they were not
    /// authored. Every scene in this project builds its weather from an empty
    /// RainController, so this is the difference between rain being a state in the
    /// HUD and rain being visible out of the windscreen.
    /// </summary>
    public void EnsureEffects()
    {
        if (!buildEffectsWhenMissing) return;
        if (drizzleParticles == null) drizzleParticles = BuildRainSystem("RainController_Drizzle", 0.5f, 14f);
        if (rainParticles == null) rainParticles = BuildRainSystem("RainController_Rain", 0.8f, 22f);
        if (thunderstormParticles == null) thunderstormParticles = BuildRainSystem("RainController_Thunderstorm", 1f, 32f);
    }

    ParticleSystem BuildRainSystem(string name, float densityScale, float fallSpeed)
    {
        var go = new GameObject(name);
        go.transform.SetParent(transform, false);
        var ps = go.AddComponent<ParticleSystem>();

        var main = ps.main;
        main.loop = true;
        main.playOnAwake = false;
        main.startLifetime = Mathf.Lerp(0.8f, 1.5f, densityScale);
        // A box emitter throws its particles along its own forward axis, so a start
        // speed here sent the rain sideways. Fall is applied as world-space velocity
        // instead, which also lets the wind lean the streaks without touching the
        // emitter's rotation.
        main.startSpeed = 0f;
        main.startSize = new ParticleSystem.MinMaxCurve(0.012f, 0.03f);
        main.gravityModifier = 0f;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        // Rain has to look like streaks, so consecutive particles are stretched along
        // their own velocity instead of being round dots.
        main.startRotation = new ParticleSystem.MinMaxCurve(0f, Mathf.PI * 2f);
        main.maxParticles = LowQuality ? 1500 : 6000;

        var emission = ps.emission;
        emission.enabled = true;
        emission.rateOverTime = 0f;

        var shape = ps.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = new Vector3(Mathf.Max(10f, effectFieldSize), 1f, Mathf.Max(10f, effectFieldSize));
        shape.position = new Vector3(0f, followHeight, 0f);

        var velocity = ps.velocityOverLifetime;
        velocity.enabled = true;
        velocity.space = ParticleSystemSimulationSpace.World;
        velocity.x = new ParticleSystem.MinMaxCurve(windXZ.x);
        velocity.y = new ParticleSystem.MinMaxCurve(-fallSpeed);
        velocity.z = new ParticleSystem.MinMaxCurve(windXZ.y);
        fallSpeedBySystem[ps] = fallSpeed;

        var renderer = ps.GetComponent<ParticleSystemRenderer>();
        renderer.renderMode = ParticleSystemRenderMode.Stretch;
        renderer.lengthScale = Mathf.Lerp(2f, 3.5f, densityScale);
        renderer.velocityScale = 0.08f;
        renderer.sharedMaterial = RainMaterial();
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = false;
        renderer.sortingOrder = 1;

        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        return ps;
    }

    static bool LowQuality => QualitySettings.GetQualityLevel() == 0;

    /// <summary>
    /// Leans the shower with the wind. Rain is a light particle inside a moving air
    /// mass, so the streak angle is the wind vector against the fall speed; without
    /// this every state fell dead vertical and the wind only moved the clouds.
    /// </summary>
    public void ApplyWind(Vector2 wind)
    {
        if ((wind - windXZ).sqrMagnitude < 0.0004f) return;
        windXZ = wind;
        ApplyWindTo(drizzleParticles);
        ApplyWindTo(rainParticles);
        ApplyWindTo(thunderstormParticles);
    }

    /// <summary>
    /// Converts an authored system to world-space fall the first time the wind
    /// touches it, so a scene-built shower leans just like a generated one.
    /// </summary>
    void EnsureWindDriven(ParticleSystem ps)
    {
        if (ps == null || fallSpeedBySystem.ContainsKey(ps)) return;

        float fall = ps.main.startSpeed.constant;
        if (fall <= 0f) fall = 18f;

        var main = ps.main;
        main.startSpeed = 0f;

        var velocity = ps.velocityOverLifetime;
        velocity.enabled = true;
        velocity.space = ParticleSystemSimulationSpace.World;
        velocity.y = new ParticleSystem.MinMaxCurve(-fall);
        fallSpeedBySystem[ps] = fall;
    }

    void ApplyWindTo(ParticleSystem ps)
    {
        if (ps == null) return;
        EnsureWindDriven(ps);

        var velocity = ps.velocityOverLifetime;
        if (!velocity.enabled) return;
        velocity.x = new ParticleSystem.MinMaxCurve(windXZ.x);
        velocity.z = new ParticleSystem.MinMaxCurve(windXZ.y);
    }

    Material RainMaterial()
    {
        if (rainMaterial != null) return rainMaterial;
        Shader shader = BuiltInShaders.Overlay;
        if (shader == null) return null;
        if (streakTexture == null) streakTexture = BuildStreakTexture();
        rainMaterial = new Material(shader) { name = "RainController_Streak_Mat" };
        if (rainMaterial.HasProperty("_MainTex")) rainMaterial.SetTexture("_MainTex", streakTexture);
        if (rainMaterial.HasProperty("_Color")) rainMaterial.SetColor("_Color", dayRainColor);
        return rainMaterial;
    }

    /// <summary>A soft vertical streak: bright in the middle, faded at both ends and the sides.</summary>
    Texture2D BuildStreakTexture()
    {
        const int width = 8;
        const int height = 32;
        var texture = new Texture2D(width, height, TextureFormat.RGBA32, true)
        {
            name = "RainController_Streak_Tex",
            wrapMode = TextureWrapMode.Clamp,
            filterMode = FilterMode.Bilinear
        };
        var pixels = new Color32[width * height];
        for (int y = 0; y < height; y++)
        for (int x = 0; x < width; x++)
        {
            float across = 1f - Mathf.Abs((x + 0.5f) / width * 2f - 1f);
            float along = Mathf.Sin((y + 0.5f) / height * Mathf.PI);
            byte alpha = (byte)Mathf.RoundToInt(Mathf.Clamp01(across * across * along) * 255f);
            pixels[y * width + x] = new Color32(255, 255, 255, alpha);
        }
        texture.SetPixels32(pixels);
        texture.Apply(true, false);
        return texture;
    }

    /// <summary>
    /// Keeps the shower over whoever is being watched. Rain authored in one place
    /// falls on one patch of city; the emitters follow so the bus is always inside it.
    /// </summary>
    void FollowTarget()
    {
        if (followTarget == null) followTarget = ResolveFollowTarget();
        if (followTarget == null) return;
        Vector3 anchor = followTarget.position;
        MoveEmitter(drizzleParticles, anchor);
        MoveEmitter(rainParticles, anchor);
        MoveEmitter(thunderstormParticles, anchor);
    }

    static void MoveEmitter(ParticleSystem ps, Vector3 anchor)
    {
        if (ps == null) return;
        var t = ps.transform;
        t.position = new Vector3(anchor.x, anchor.y, anchor.z);
    }

    Transform ResolveFollowTarget()
    {
        var bus = FindAnyObjectByType<BusController>();
        if (bus != null) return bus.transform;
        if (Camera.main != null) return Camera.main.transform;
        var camera = FindAnyObjectByType<Camera>();
        return camera != null ? camera.transform : null;
    }

    public void SetRain(WeatherState state, float intensity)
    {
        intensity = Mathf.Clamp01(intensity);
        currentRainState = state;

        if (dryRoadRoutine != null && IsRainState(state))
        {
            StopCoroutine(dryRoadRoutine);
            dryRoadRoutine = null;
        }

        StopThunderIfInactive(state);

        switch (state)
        {
            case WeatherState.Drizzle:
                StopInactiveParticles(drizzleParticles);
                SetParticles(drizzleParticles, intensity * 0.5f);
                SetAudio(lightRainClip, intensity * 0.3f);
                SetWetRoad(intensity * 0.3f);
                SetWipers(intensity > 0.15f, intensity * 0.6f);
                break;

            case WeatherState.LightRain:
                StopInactiveParticles(rainParticles);
                SetParticles(rainParticles, intensity * 0.6f);
                SetAudio(lightRainClip, intensity * 0.6f);
                SetWetRoad(intensity * 0.6f);
                SetWipers(true, intensity);
                break;

            case WeatherState.HeavyRain:
                StopInactiveParticles(rainParticles);
                SetParticles(rainParticles, intensity);
                SetAudio(heavyRainClip, intensity * 0.8f);
                SetWetRoad(intensity);
                SetWipers(true, intensity);
                break;

            case WeatherState.Thunderstorm:
                StopInactiveParticles(thunderstormParticles ?? rainParticles);
                SetParticles(thunderstormParticles ?? rainParticles, 1f);
                SetAudio(heavyRainClip, 1f);
                SetWetRoad(1f);
                SetWipers(true, 1f);
                EnsureThunderEffect();
                break;

            default:
                StopAllParticles();
                StopLoopingRainAudio();
                SetWipers(false, 0f);
                if (dryRoadRoutine == null) dryRoadRoutine = StartCoroutine(DryRoad());
                break;
        }
    }

    void SetParticles(ParticleSystem ps, float intensity)
    {
        if (ps == null) return;
        StopParticlesExcept(ps);
        var emission = ps.emission;
        emission.rateOverTime = Mathf.Lerp(0, QualitySettings.GetQualityLevel() == 0 ? 150 : 500, intensity);
        if (intensity > 0.001f)
        {
            if (!ps.isPlaying)
                ps.Play();
        }
        else
        {
            ps.Stop();
        }
    }

    void SetAudio(AudioClip clip, float volume)
    {
        if (rainAudio == null || clip == null) return;
        if (rainAudio.clip != clip)
            rainAudio.clip = clip;
        rainAudio.volume = Mathf.Clamp01(volume);
        rainAudio.loop = true;
        if (!rainAudio.isPlaying)
            rainAudio.Play();
    }

    void SetWetRoad(float wetness)
    {
        currentWetness = wetness;
        if (Mathf.Abs(wetness - lastAppliedWetness) < 0.01f && wetness > 0f && wetness < 1f) return;
        if (Mathf.Approximately(wetness, lastAppliedWetness)) return;
        lastAppliedWetness = wetness;
        ApplyWetnessToMaterial(roadMaterial, wetness);

        if (roadMaterials != null)
        {
            for (int i = 0; i < roadMaterials.Length; i++)
                ApplyWetnessToMaterial(roadMaterials[i], wetness);
        }

        if (wetRoadRenderers != null)
        {
            if (wetRoadPropertyBlock == null)
                wetRoadPropertyBlock = new MaterialPropertyBlock();

            for (int i = 0; i < wetRoadRenderers.Length; i++)
            {
                var rendererRef = wetRoadRenderers[i];
                if (rendererRef == null)
                    continue;

                rendererRef.GetPropertyBlock(wetRoadPropertyBlock);
                wetRoadPropertyBlock.SetFloat(wetBlendProperty, wetness);
                // Built-in road materials read _Glossiness for smoothness and _Color
                // for albedo. The dry colour below is the one the road surfaces are
                // built with, so the ramp darkens them as it was always meant to.
                // wetBlendProperty, _WetColor and _WetGlossiness belong to the
                // optional custom wet shader, which supplies the blend the built-in
                // road materials do not have.
                wetRoadPropertyBlock.SetFloat("_Glossiness", Mathf.Lerp(0.15f, 0.85f, wetness));
                wetRoadPropertyBlock.SetFloat("_Smoothness", Mathf.Lerp(0.15f, 0.85f, wetness));
                wetRoadPropertyBlock.SetColor("_Color", Color.Lerp(new Color(0.18f, 0.19f, 0.21f), new Color(0.09f, 0.10f, 0.12f), wetness));
                if (wetSpecularMaterial != null)
                {
                    if (wetSpecularMaterial.HasProperty("_Color"))
                        wetRoadPropertyBlock.SetColor("_WetColor", wetSpecularMaterial.color);
                    if (wetSpecularMaterial.HasProperty("_Glossiness"))
                        wetRoadPropertyBlock.SetFloat("_WetGlossiness", wetSpecularMaterial.GetFloat("_Glossiness"));
                }
                rendererRef.SetPropertyBlock(wetRoadPropertyBlock);
            }
        }
    }

    void SetWipers(bool active, float intensity)
    {
        if (windscreenWipers != null)
            windscreenWipers.SetActive(active);
        if (wiperAnimator != null)
        {
            wiperAnimator.enabled = active;
            wiperAnimator.speed = active
                ? Mathf.Lerp(minWiperSpeed, maxWiperSpeed, Mathf.Clamp01(intensity))
                : 0f;
            if (!string.IsNullOrWhiteSpace(wiperSpeedParameter))
                wiperAnimator.SetFloat(wiperSpeedParameter, wiperAnimator.speed);
        }
    }

    void StopAll()
    {
        StopAllParticles();
        StopLoopingRainAudio();
        SetWipers(false, 0f);
        StopThunderIfInactive(WeatherState.Clear);
    }

    IEnumerator DryRoad()
    {
        float start = currentWetness;
        float elapsed = 0f;
        float duration = 120f; // 2 minutes to dry

        while (elapsed < duration && currentWetness > 0f)
        {
            elapsed += Time.deltaTime;
            float wetness = Mathf.Lerp(start, 0f, elapsed / duration);
            SetWetRoad(wetness);
            yield return null;
        }

        SetWetRoad(0f);
        dryRoadRoutine = null;
    }

    IEnumerator ThunderEffect()
    {
        while (WeatherSystem.Instance != null &&
               WeatherSystem.Instance.currentWeather == WeatherState.Thunderstorm)
        {
            yield return new WaitForSeconds(Random.Range(5f, 20f));

            // Flash
            if (WeatherSystem.Instance?.skyController != null)
                WeatherSystem.Instance.skyController.TriggerLightningFlash();

            // Thunder sound after delay
            float dist = Random.Range(0.5f, 3f);
            yield return new WaitForSeconds(dist);
            if (rainAudio != null && thunderClip != null)
            {
                rainAudio.PlayOneShot(thunderClip, 0.8f);
            }
        }

        thunderRoutine = null;
    }

    void EnsureThunderEffect()
    {
        if (thunderRoutine == null)
            thunderRoutine = StartCoroutine(ThunderEffect());
    }

    void StopThunderIfInactive(WeatherState state)
    {
        if (state == WeatherState.Thunderstorm)
            return;

        if (thunderRoutine != null)
        {
            StopCoroutine(thunderRoutine);
            thunderRoutine = null;
        }
    }

    void StopLoopingRainAudio()
    {
        if (rainAudio != null && rainAudio.isPlaying && rainAudio.loop)
            rainAudio.Stop();
    }

    void StopAllParticles()
    {
        if (rainParticles != null) rainParticles.Stop();
        if (drizzleParticles != null) drizzleParticles.Stop();
        if (thunderstormParticles != null) thunderstormParticles.Stop();
    }

    void StopParticlesExcept(ParticleSystem keep)
    {
        if (rainParticles != null && rainParticles != keep) rainParticles.Stop();
        if (drizzleParticles != null && drizzleParticles != keep) drizzleParticles.Stop();
        if (thunderstormParticles != null && thunderstormParticles != keep) thunderstormParticles.Stop();
    }

    void StopInactiveParticles(ParticleSystem activeParticles)
    {
        StopParticlesExcept(activeParticles);
    }

    void ApplyWetnessToMaterial(Material material, float wetness)
    {
        if (material == null || !material.HasProperty(wetBlendProperty))
            return;

        material.SetFloat(wetBlendProperty, wetness);
    }

    bool IsRainState(WeatherState state)
    {
        switch (state)
        {
            case WeatherState.Drizzle:
            case WeatherState.LightRain:
            case WeatherState.HeavyRain:
            case WeatherState.Thunderstorm:
                return true;
            default:
                return false;
        }
    }
}
