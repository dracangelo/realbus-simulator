using UnityEngine;

public class SnowController : MonoBehaviour
{
    [Header("Snow Particles")]
    public ParticleSystem snowParticles;
    public ParticleSystem blizzardParticles;

    [Header("Snow Accumulation")]
    public Material groundMaterial;
    public string snowBlendProperty = "_SnowBlend";
    private float currentSnowAccumulation = 0f;

    [Header("Snow Audio")]
    public AudioSource snowAudio;
    public AudioClip snowWindClip;

    [Header("Generated Effects")]
    [Tooltip("Build the snow systems at run time when the scene provides none. No scene ships snow particles, so snow was previously only a state in the weather system.")]
    public bool buildEffectsWhenMissing = true;
    [Tooltip("The snowfall follows this transform so the bus is always inside it.")]
    public Transform followTarget;
    public float followHeight = 20f;
    public float effectFieldSize = 55f;

    [Header("Wind")]
    [Tooltip("Wind in metres per second (X = east, Y = north), pushed in by the wind system. Snow drifts with it.")]
    public Vector2 windXZ;

    [Header("Night Light")]
    [Tooltip("Flake colour in daylight.")]
    public Color dayFlakeColor = new Color(0.97f, 0.98f, 1f, 0.9f);
    [Tooltip("Flake colour after dark. Flakes are drawn unlit, so night needs its own floor or the snow glows.")]
    public Color nightFlakeColor = new Color(0.40f, 0.45f, 0.56f, 0.72f);

    Material flakeMaterial;
    Color lastFlakeTint = Color.clear;

    // Fall speed per generated system, kept out of startSpeed so world-space wind
    // can carry the flakes sideways.
    readonly System.Collections.Generic.Dictionary<ParticleSystem, float> fallSpeedBySystem =
        new System.Collections.Generic.Dictionary<ParticleSystem, float>();

    void Start()
    {
        if (buildEffectsWhenMissing) EnsureEffects();
        StopAll();
    }

    void Update()
    {
        FollowTarget();
        UpdateTint();
    }

    /// <summary>Fades the flakes with the sky, so snow does not glow at midnight.</summary>
    void UpdateTint()
    {
        if (flakeMaterial == null) return;

        TimeOfDaySystem clock = TimeOfDaySystem.Instance;
        float light = clock != null ? SkyEnvironmentController.SkyLightLevelFor(clock.SunElevationDegrees) : 1f;
        Color tint = RainController.TintForLight(dayFlakeColor, nightFlakeColor, light);
        if (tint == lastFlakeTint) return;

        lastFlakeTint = tint;
        if (flakeMaterial.HasProperty("_Color")) flakeMaterial.SetColor("_Color", tint);
    }

    /// <summary>Creates the snow and blizzard systems when the scene authored none.</summary>
    public void EnsureEffects()
    {
        if (!buildEffectsWhenMissing) return;
        if (snowParticles == null) snowParticles = BuildSnowSystem("SnowController_Snow", 0.14f, 1.4f, 0.25f);
        if (blizzardParticles == null) blizzardParticles = BuildSnowSystem("SnowController_Blizzard", 0.22f, 3.2f, 0.6f);
    }

    ParticleSystem BuildSnowSystem(string name, float flakeSize, float fallSpeed, float wobble)
    {
        var go = new GameObject(name);
        go.transform.SetParent(transform, false);
        var ps = go.AddComponent<ParticleSystem>();

        var main = ps.main;
        main.loop = true;
        main.playOnAwake = false;
        main.startLifetime = 8f;
        // Box emitters throw along their own forward axis, so the fall lives in
        // world-space velocity instead; the wind then drifts it sideways.
        main.startSpeed = 0f;
        main.startSize = new ParticleSystem.MinMaxCurve(flakeSize * 0.5f, flakeSize);
        main.gravityModifier = 0.01f;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.maxParticles = QualitySettings.GetQualityLevel() == 0 ? 1200 : 4000;

        var emission = ps.emission;
        emission.enabled = true;
        emission.rateOverTime = 0f;

        var shape = ps.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = new Vector3(Mathf.Max(10f, effectFieldSize), 1f, Mathf.Max(10f, effectFieldSize));
        shape.position = new Vector3(0f, followHeight, 0f);

        // Falling straight down reads as rain; snow needs the wobble a noise module
        // gives it, and a blizzard needs more of it.
        var noise = ps.noise;
        noise.enabled = true;
        noise.strength = wobble;
        noise.frequency = 0.4f;
        noise.scrollSpeed = 0.25f;

        var velocity = ps.velocityOverLifetime;
        velocity.enabled = true;
        velocity.space = ParticleSystemSimulationSpace.World;
        velocity.x = new ParticleSystem.MinMaxCurve(windXZ.x);
        velocity.y = new ParticleSystem.MinMaxCurve(-fallSpeed);
        velocity.z = new ParticleSystem.MinMaxCurve(windXZ.y);
        fallSpeedBySystem[ps] = fallSpeed;

        var renderer = ps.GetComponent<ParticleSystemRenderer>();
        renderer.renderMode = ParticleSystemRenderMode.Billboard;
        renderer.sharedMaterial = FlakeMaterial();
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = false;

        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        return ps;
    }

    Material FlakeMaterial()
    {
        if (flakeMaterial != null) return flakeMaterial;
        Shader shader = BuiltInShaders.Overlay;
        if (shader == null) return null;
        flakeMaterial = new Material(shader) { name = "SnowController_Flake_Mat" };
        if (flakeMaterial.HasProperty("_MainTex")) flakeMaterial.SetTexture("_MainTex", BuildFlakeTexture());
        if (flakeMaterial.HasProperty("_Color")) flakeMaterial.SetColor("_Color", dayFlakeColor);
        return flakeMaterial;
    }

    /// <summary>A soft round flake: solid in the middle, feathered to nothing at the edge.</summary>
    static Texture2D BuildFlakeTexture()
    {
        const int size = 32;
        var texture = new Texture2D(size, size, TextureFormat.RGBA32, true)
        {
            name = "SnowController_Flake_Tex",
            wrapMode = TextureWrapMode.Clamp,
            filterMode = FilterMode.Bilinear
        };
        var pixels = new Color32[size * size];
        float centre = (size - 1) * 0.5f;
        for (int y = 0; y < size; y++)
        for (int x = 0; x < size; x++)
        {
            float distance = Vector2.Distance(new Vector2(x, y), new Vector2(centre, centre)) / (size * 0.5f);
            float alpha = Mathf.Clamp01(1f - distance * distance);
            pixels[y * size + x] = new Color32(255, 255, 255, (byte)Mathf.RoundToInt(alpha * 255f));
        }
        texture.SetPixels32(pixels);
        texture.Apply(true, false);
        return texture;
    }

    void FollowTarget()
    {
        if (followTarget == null)
        {
            var bus = FindAnyObjectByType<BusController>();
            if (bus != null) followTarget = bus.transform;
            else if (Camera.main != null) followTarget = Camera.main.transform;
        }
        if (followTarget == null) return;
        Vector3 anchor = followTarget.position;
        if (snowParticles != null) snowParticles.transform.position = anchor;
        if (blizzardParticles != null) blizzardParticles.transform.position = anchor;
    }

    public void SetSnow(WeatherState state, float intensity)
    {
        StopAll();

        switch (state)
        {
            case WeatherState.Snow:
                SetParticles(snowParticles, intensity * 0.7f);
                SetAudio(snowWindClip, intensity * 0.4f);
                AccumulateSnow(intensity * 0.3f);
                break;

            case WeatherState.Blizzard:
                SetParticles(blizzardParticles ?? snowParticles, 1f);
                SetAudio(snowWindClip, 1f);
                AccumulateSnow(1f);
                break;

            default:
                // Melt snow gradually
                StartCoroutine(MeltSnow());
                break;
        }
    }

    void SetParticles(ParticleSystem ps, float intensity)
    {
        if (ps == null) return;
        var emission = ps.emission;
        emission.rateOverTime = Mathf.Lerp(0, 300, intensity);
        ps.Play();
    }

    /// <summary>
    /// Carries the snowfall with the wind. Snow is the lightest thing in the scene,
    /// so it drifts further than rain for the same speed and is the clearest sign of
    /// a blizzard rather than a calm fall.
    /// </summary>
    public void ApplyWind(Vector2 wind)
    {
        if ((wind - windXZ).sqrMagnitude < 0.0004f) return;
        windXZ = wind;
        ApplyWindTo(snowParticles);
        ApplyWindTo(blizzardParticles);
    }

    void EnsureWindDriven(ParticleSystem ps)
    {
        if (ps == null || fallSpeedBySystem.ContainsKey(ps)) return;

        float fall = ps.main.startSpeed.constant;
        if (fall <= 0f) fall = 1.4f;

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
        // Flakes are much lighter than drops, so they lean further for the same air.
        velocity.x = new ParticleSystem.MinMaxCurve(windXZ.x * 1.35f);
        velocity.z = new ParticleSystem.MinMaxCurve(windXZ.y * 1.35f);
    }

    void SetAudio(AudioClip clip, float volume)
    {
        if (snowAudio == null || clip == null) return;
        snowAudio.clip = clip;
        snowAudio.volume = volume;
        snowAudio.loop = true;
        snowAudio.Play();
    }

    void AccumulateSnow(float amount)
    {
        currentSnowAccumulation = Mathf.Clamp01(
            currentSnowAccumulation + amount * 0.1f);
        ApplySnowToGround(currentSnowAccumulation);
    }

    void ApplySnowToGround(float amount)
    {
        if (groundMaterial != null &&
            groundMaterial.HasProperty(snowBlendProperty))
            groundMaterial.SetFloat(snowBlendProperty, amount);
    }

    void StopAll()
    {
        if (snowParticles != null) snowParticles.Stop();
        if (blizzardParticles != null) blizzardParticles.Stop();
        if (snowAudio != null) snowAudio.Stop();
    }

    System.Collections.IEnumerator MeltSnow()
    {
        float start = currentSnowAccumulation;
        float elapsed = 0f;
        float duration = 300f; // 5 minutes to melt

        while (elapsed < duration && currentSnowAccumulation > 0f)
        {
            elapsed += Time.deltaTime;
            currentSnowAccumulation = Mathf.Lerp(start, 0f, elapsed / duration);
            ApplySnowToGround(currentSnowAccumulation);
            yield return null;
        }
    }
}
