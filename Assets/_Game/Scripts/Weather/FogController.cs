using UnityEngine;

public class FogController : MonoBehaviour
{
    [Header("Visibility Distances (metres)")]
    public float clearVisibility       = 2000f;
    public float overcastVisibility    = 1200f;
    public float drizzleVisibility     =  800f;
    public float lightRainVisibility   =  500f;
    public float heavyRainVisibility   =  250f;
    public float thunderVisibility     =  150f;
    public float lightFogVisibility    =  120f;
    public float heavyFogVisibility    =   40f;
    public float snowVisibility        =  400f;
    public float blizzardVisibility    =   60f;

    [Header("Clear Fog Start/End (for tests)")]
    public float clearFogStart = 200f;
    public float clearFogEnd = 2000f;

    [Header("Fog Colors")]
    public Color clearFogColor    = new Color(0.78f, 0.84f, 0.92f);
    public Color rainFogColor     = new Color(0.35f, 0.38f, 0.44f);
    public Color thunderFogColor  = new Color(0.15f, 0.15f, 0.20f);
    public Color fogFogColor      = new Color(0.72f, 0.72f, 0.72f);
    public Color snowFogColor     = new Color(0.85f, 0.87f, 0.92f);
    public Color blizzardFogColor = new Color(0.70f, 0.72f, 0.78f);

    [Header("Transition Speed")]
    public float lerpSpeed = 0.4f;

    private float targetFogStart;
    private float targetFogEnd;
    private Color targetFogColor;
    private bool  _fogShouldBeOn;

    void Start()
    {
        RenderSettings.fog     = false;
        RenderSettings.fogMode = FogMode.Linear;

        targetFogStart = 0f;
        targetFogEnd   = clearVisibility;
        targetFogColor = clearFogColor;
    }

    void Update()
    {
        if (!_fogShouldBeOn) return;

        RenderSettings.fogStartDistance = Mathf.Lerp(
            RenderSettings.fogStartDistance, targetFogStart, Time.deltaTime * lerpSpeed);

        RenderSettings.fogEndDistance = Mathf.Lerp(
            RenderSettings.fogEndDistance,   targetFogEnd,   Time.deltaTime * lerpSpeed);

        RenderSettings.fogColor = Color.Lerp(
            RenderSettings.fogColor, targetFogColor, Time.deltaTime * lerpSpeed * 0.5f);
    }

    public void SetFog(WeatherState state, float intensity)
    {
        switch (state)
        {
            case WeatherState.Clear:
                StartCoroutine(FadeOutFog(3f));
                return;

            case WeatherState.PartlyCloudy:
                SetLinear(clearVisibility * 0.8f, clearFogColor);
                break;

            case WeatherState.Overcast:
                SetLinear(overcastVisibility, clearFogColor);
                break;

            case WeatherState.Drizzle:
                SetLinear(drizzleVisibility, Color.Lerp(clearFogColor, rainFogColor, 0.4f));
                break;

            case WeatherState.LightRain:
                SetLinear(lightRainVisibility, rainFogColor);
                break;

            case WeatherState.HeavyRain:
                SetLinear(
                    Mathf.Lerp(lightRainVisibility, heavyRainVisibility, intensity),
                    Color.Lerp(rainFogColor, thunderFogColor, intensity * 0.4f));
                break;

            case WeatherState.Thunderstorm:
                SetLinear(thunderVisibility, thunderFogColor);
                break;

            case WeatherState.Fog:
                SetLinear(
                    Mathf.Lerp(lightFogVisibility, heavyFogVisibility, intensity),
                    fogFogColor);
                break;

            case WeatherState.Snow:
                SetLinear(snowVisibility, snowFogColor);
                break;

            case WeatherState.Blizzard:
                SetLinear(blizzardVisibility, blizzardFogColor);
                break;
        }
    }

    // ── Helpers ──────────────────────────────────────────────────────

    void SetLinear(float visibility, Color color)
    {
        _fogShouldBeOn = true;
        RenderSettings.fog     = true;
        RenderSettings.fogMode = FogMode.Linear;

        // Fog starts at 10% of visibility, ends at 100%
        targetFogStart = visibility * 0.10f;
        targetFogEnd   = visibility;
        targetFogColor = color;
    }

    System.Collections.IEnumerator FadeOutFog(float duration)
    {
        float elapsed = 0f;
        float startEnd = RenderSettings.fogEndDistance;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            RenderSettings.fogEndDistance = Mathf.Lerp(startEnd, clearVisibility, t);
            yield return null;
        }

        _fogShouldBeOn = false;
        RenderSettings.fog = false;
    }
}