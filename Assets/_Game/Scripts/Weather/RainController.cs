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

    private float currentWetness = 0f;
    float lastAppliedWetness = -1f;
    private WeatherState currentRainState = WeatherState.Clear;
    private Coroutine dryRoadRoutine;
    private Coroutine thunderRoutine;
    private MaterialPropertyBlock wetRoadPropertyBlock;

    void Start()
    {
        if (wetRoadPropertyBlock == null)
            wetRoadPropertyBlock = new MaterialPropertyBlock();
        StopAll();
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
                wetRoadPropertyBlock.SetFloat("_Smoothness", Mathf.Lerp(0.15f, 0.85f, wetness));
                wetRoadPropertyBlock.SetColor("_BaseColor", Color.Lerp(new Color(0.18f, 0.19f, 0.21f), new Color(0.09f, 0.10f, 0.12f), wetness));
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
