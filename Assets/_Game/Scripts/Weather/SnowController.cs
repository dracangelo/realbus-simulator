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

    void Start()
    {
        StopAll();
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
