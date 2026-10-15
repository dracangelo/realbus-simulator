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

    Material flakeMaterial;

    void Start()
    {
        if (buildEffectsWhenMissing) EnsureEffects();
        StopAll();
    }

    void Update()
    {
        FollowTarget();
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
        main.startSpeed = fallSpeed;
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
        if (flakeMaterial.HasProperty("_Color")) flakeMaterial.SetColor("_Color", new Color(0.97f, 0.98f, 1f, 0.9f));
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
