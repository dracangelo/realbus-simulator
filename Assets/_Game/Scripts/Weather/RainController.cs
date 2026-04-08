using UnityEngine;

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
    public string wetBlendProperty = "_WetBlend";

    [Header("Windscreen")]
    public GameObject windscreenWipers;

    private float currentWetness = 0f;

    void Start()
    {
        StopAll();
    }

    public void SetRain(WeatherState state, float intensity)
    {
        StopAll();

        switch (state)
        {
            case WeatherState.Drizzle:
                SetParticles(drizzleParticles, intensity * 0.5f);
                SetAudio(lightRainClip, intensity * 0.3f);
                SetWetRoad(intensity * 0.3f);
                SetWipers(false);
                break;

            case WeatherState.LightRain:
                SetParticles(rainParticles, intensity * 0.6f);
                SetAudio(lightRainClip, intensity * 0.6f);
                SetWetRoad(intensity * 0.6f);
                SetWipers(true);
                break;

            case WeatherState.HeavyRain:
                SetParticles(rainParticles, intensity);
                SetAudio(heavyRainClip, intensity * 0.8f);
                SetWetRoad(intensity);
                SetWipers(true);
                break;

            case WeatherState.Thunderstorm:
                SetParticles(thunderstormParticles ?? rainParticles, 1f);
                SetAudio(heavyRainClip, 1f);
                SetWetRoad(1f);
                SetWipers(true);
                StartCoroutine(ThunderEffect());
                break;

            default:
                // Gradually dry the road
                StartCoroutine(DryRoad());
                break;
        }
    }

    void SetParticles(ParticleSystem ps, float intensity)
    {
        if (ps == null) return;
        var emission = ps.emission;
        emission.rateOverTime = Mathf.Lerp(0, 500, intensity);
        ps.Play();
    }

    void SetAudio(AudioClip clip, float volume)
    {
        if (rainAudio == null || clip == null) return;
        rainAudio.clip = clip;
        rainAudio.volume = volume;
        rainAudio.loop = true;
        rainAudio.Play();
    }

    void SetWetRoad(float wetness)
    {
        currentWetness = wetness;
        if (roadMaterial != null &&
            roadMaterial.HasProperty(wetBlendProperty))
            roadMaterial.SetFloat(wetBlendProperty, wetness);
    }

    void SetWipers(bool active)
    {
        if (windscreenWipers != null)
            windscreenWipers.SetActive(active);
    }

    void StopAll()
    {
        if (rainParticles != null) rainParticles.Stop();
        if (drizzleParticles != null) drizzleParticles.Stop();
        if (thunderstormParticles != null) thunderstormParticles.Stop();
        if (rainAudio != null) rainAudio.Stop();
        SetWipers(false);
    }

    System.Collections.IEnumerator DryRoad()
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
    }

    System.Collections.IEnumerator ThunderEffect()
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
    }
}
