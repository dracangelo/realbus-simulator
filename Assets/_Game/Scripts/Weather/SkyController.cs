using UnityEngine;

public class SkyController : MonoBehaviour
{
    [Header("Sun / Directional Light")]
    public Light sunLight;

    [Header("Sun Colors")]
    public Color clearSunColor     = new Color(1.00f, 0.96f, 0.84f); // warm daylight
    public Color overcastSunColor  = new Color(0.80f, 0.82f, 0.85f); // cool grey
    public Color rainSunColor      = new Color(0.55f, 0.58f, 0.65f); // dark blue-grey
    public Color thunderSunColor   = new Color(0.25f, 0.25f, 0.30f); // near black
    public Color snowSunColor      = new Color(0.90f, 0.93f, 1.00f); // ice-blue white

    [Header("Sky Colors (for tests)")]
    public Color rainSkyColor = new Color(0.55f, 0.58f, 0.65f);
    public Color snowSkyColor = new Color(0.90f, 0.93f, 1.00f);

    [Header("Sun Intensity")]
    public float clearIntensity     = 1.20f;
    public float overcastIntensity  = 0.55f;
    public float drizzleIntensity   = 0.40f;
    public float lightRainIntensity = 0.30f;
    public float heavyRainIntensity = 0.18f;
    public float thunderIntensity   = 0.08f;
    public float fogIntensity       = 0.45f;
    public float snowIntensity      = 0.50f;
    public float blizzardIntensity  = 0.20f;

    [Header("Thunder Intensity (for tests)")]
    public float thunderSunIntensity = 0.08f;
    public float thunderAmbientIntensity = 0.10f;

    [Header("Ambient (Environment Lighting)")]
    public Color clearAmbient      = new Color(0.50f, 0.65f, 0.90f);
    public Color overcastAmbient   = new Color(0.45f, 0.47f, 0.52f);
    public Color rainAmbient       = new Color(0.25f, 0.28f, 0.35f);
    public Color thunderAmbient    = new Color(0.10f, 0.10f, 0.14f);
    public Color snowAmbient       = new Color(0.70f, 0.72f, 0.80f);

    [Header("Clear Sky (for tests)")]
    public float clearSunIntensity = 1.20f;
    public float clearAmbientIntensity = 0.50f;
    public Color clearSkyColor = new Color(0.50f, 0.65f, 0.90f);

    [Header("Transition Speed")]
    public float lerpSpeed = 0.8f; // units per second

    // targets
    private Color  _targetSunColor;
    private float  targetSunIntensity;
    private Color  _targetAmbient;
    private float  targetAmbientIntensity;
    private Color  targetSkyColor;

    // lightning state
    private bool  _isFlashing;
    private float _preLightningIntensity;
    private Color _preLightningAmbient;

    void Start()
    {
        _targetSunColor       = clearSunColor;
        targetSunIntensity   = clearIntensity;
        _targetAmbient        = clearAmbient;
        targetAmbientIntensity = clearAmbientIntensity;
        targetSkyColor        = clearSkyColor;

        Apply(_targetSunColor, targetSunIntensity, _targetAmbient);
    }

    void Update()
    {
        if (_isFlashing) return;

        if (sunLight != null)
        {
            sunLight.color     = Color.Lerp(sunLight.color,     _targetSunColor,      Time.deltaTime * lerpSpeed);
            sunLight.intensity = Mathf.Lerp(sunLight.intensity, targetSunIntensity,  Time.deltaTime * lerpSpeed);
        }

        RenderSettings.ambientLight = Color.Lerp(
            RenderSettings.ambientLight, _targetAmbient, Time.deltaTime * lerpSpeed * 0.5f);
    }

    // ── Public API ───────────────────────────────────────────────────

    public void SetSky(WeatherState state, float intensity, float temperature)
    {
        switch (state)
        {
            case WeatherState.Clear:
                _targetSunColor       = clearSunColor;
                targetSunIntensity   = clearIntensity;
                _targetAmbient        = clearAmbient;
                targetAmbientIntensity = clearAmbientIntensity;
                targetSkyColor        = clearSkyColor;
                break;

            case WeatherState.PartlyCloudy:
                _targetSunColor       = Color.Lerp(clearSunColor,    overcastSunColor,  0.35f);
                targetSunIntensity   = Mathf.Lerp(clearIntensity,   overcastIntensity, 0.35f);
                _targetAmbient        = Color.Lerp(clearAmbient,      overcastAmbient,   0.35f);
                targetAmbientIntensity = Mathf.Lerp(clearAmbientIntensity, 0.40f, 0.35f);
                targetSkyColor        = Color.Lerp(clearSkyColor,    overcastSunColor,  0.35f);
                break;

            case WeatherState.Overcast:
                _targetSunColor       = overcastSunColor;
                targetSunIntensity   = overcastIntensity;
                _targetAmbient        = overcastAmbient;
                targetAmbientIntensity = 0.40f;
                targetSkyColor        = overcastSunColor;
                break;

            case WeatherState.Fog:
                _targetSunColor       = Color.Lerp(overcastSunColor, rainSunColor,  intensity * 0.6f);
                targetSunIntensity   = Mathf.Lerp(fogIntensity,     lightRainIntensity, intensity * 0.5f);
                _targetAmbient        = Color.Lerp(overcastAmbient,  rainAmbient,   intensity * 0.6f);
                targetAmbientIntensity = Mathf.Lerp(0.40f, 0.35f, intensity * 0.6f);
                targetSkyColor        = Color.Lerp(overcastSunColor, rainSkyColor, intensity * 0.6f);
                break;

            case WeatherState.Drizzle:
                _targetSunColor       = Color.Lerp(overcastSunColor, rainSunColor,  0.4f);
                targetSunIntensity   = drizzleIntensity;
                _targetAmbient        = Color.Lerp(overcastAmbient,  rainAmbient,   0.4f);
                targetAmbientIntensity = 0.35f;
                targetSkyColor        = Color.Lerp(overcastSunColor, rainSkyColor, 0.4f);
                break;

            case WeatherState.LightRain:
                _targetSunColor       = rainSunColor;
                targetSunIntensity   = lightRainIntensity;
                _targetAmbient        = rainAmbient;
                targetAmbientIntensity = 0.30f;
                targetSkyColor        = rainSkyColor;
                break;

            case WeatherState.HeavyRain:
                _targetSunColor       = Color.Lerp(rainSunColor,    thunderSunColor, intensity * 0.5f);
                targetSunIntensity   = heavyRainIntensity;
                _targetAmbient        = Color.Lerp(rainAmbient,     thunderAmbient,  intensity * 0.5f);
                targetAmbientIntensity = Mathf.Lerp(0.30f, thunderAmbientIntensity, intensity * 0.5f);
                targetSkyColor        = Color.Lerp(rainSkyColor, thunderSunColor, intensity * 0.5f);
                break;

            case WeatherState.Thunderstorm:
                _targetSunColor       = thunderSunColor;
                targetSunIntensity   = thunderIntensity;
                _targetAmbient        = thunderAmbient;
                targetAmbientIntensity = thunderAmbientIntensity;
                targetSkyColor        = thunderSunColor;
                break;

            case WeatherState.Snow:
                _targetSunColor       = snowSunColor;
                targetSunIntensity   = snowIntensity;
                _targetAmbient        = snowAmbient;
                targetAmbientIntensity = 0.45f;
                targetSkyColor        = snowSkyColor;
                break;

            case WeatherState.Blizzard:
                _targetSunColor       = Color.Lerp(snowSunColor, thunderSunColor, 0.4f);
                targetSunIntensity   = blizzardIntensity;
                _targetAmbient        = Color.Lerp(snowAmbient,  thunderAmbient,  0.4f);
                targetAmbientIntensity = thunderAmbientIntensity;
                targetSkyColor        = Color.Lerp(snowSkyColor, rainSkyColor, 0.5f);
                break;
        }
    }

    public void TriggerLightningFlash()
    {
        if (!_isFlashing)
            StartCoroutine(LightningFlash());
    }

    // ── Helpers ──────────────────────────────────────────────────────

    void Apply(Color sunCol, float sunInt, Color ambient)
    {
        if (sunLight != null)
        {
            sunLight.color     = sunCol;
            sunLight.intensity = sunInt;
        }
        RenderSettings.ambientLight = ambient;
    }

    System.Collections.IEnumerator LightningFlash()
    {
        _isFlashing = true;
        _preLightningIntensity = sunLight != null ? sunLight.intensity : 0f;
        _preLightningAmbient   = RenderSettings.ambientLight;

        // Flash 1 — very bright
        Apply(Color.white, 6f, Color.white * 2.5f);
        yield return new WaitForSeconds(0.04f);

        // Back to dark
        Apply(_targetSunColor, targetSunIntensity, _targetAmbient);
        yield return new WaitForSeconds(0.06f);

        // Flash 2 — slightly dimmer
        Apply(Color.white, 4f, Color.white * 1.8f);
        yield return new WaitForSeconds(0.03f);

        // Restore
        Apply(_targetSunColor, targetSunIntensity, _targetAmbient);

        _isFlashing = false;
    }
}