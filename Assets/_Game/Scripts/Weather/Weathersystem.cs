using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public enum WeatherState
{
    Clear,
    PartlyCloudy,
    Overcast,
    Fog,
    Drizzle,
    LightRain,
    HeavyRain,
    Thunderstorm,
    Snow,
    Blizzard
}

public class WeatherSystem : MonoBehaviour
{
    public static WeatherSystem Instance { get; private set; }

    [Header("Current Weather")]
    public WeatherState currentWeather = WeatherState.Clear;
    public float temperature = 20f;
    public float weatherIntensity = 0f; // 0-1

    [Header("Transition")]
    public float transitionSpeed = 0.5f; // seconds per unit intensity

    [Header("Controllers — assign in Inspector")]
    public RainController rainController;
    public FogController fogController;
    public SnowController snowController;
    public SkyController skyController;

    [Header("Debug")]
    public bool forceWeather = false;
    public WeatherState forcedWeatherState = WeatherState.Clear;

    private CityDefinition activeCity;
    private bool weatherLoaded = false;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        activeCity = CityManager.Instance?.activeCity;
        StartCoroutine(InitialiseWeather());
    }

    IEnumerator InitialiseWeather()
    {
        if (forceWeather)
        {
            ApplyWeather(forcedWeatherState, 0.8f, temperature);
            weatherLoaded = true;
            yield break;
        }

        // Option C — try real weather first, fall back to simulated
        yield return StartCoroutine(TryFetchRealWeather());

        if (!weatherLoaded)
        {
            Debug.Log("WeatherSystem: Using simulated weather");
            ApplySimulatedWeather();
        }

        // Start gradual weather change cycle
        StartCoroutine(WeatherChangeCycle());
    }

    // ── Real weather via Open-Meteo ──────────────────────────────────

    IEnumerator TryFetchRealWeather()
    {
        if (activeCity == null) yield break;

        string url = $"https://api.open-meteo.com/v1/forecast" +
                     $"?latitude={activeCity.centreLat:F4}" +
                     $"&longitude={activeCity.centreLon:F4}" +
                     $"&current=weathercode,temperature_2m" +
                     $"&timezone=auto";

        Debug.Log($"WeatherSystem: Fetching real weather for {activeCity.cityName}...");

        using (var request = UnityWebRequest.Get(url))
        {
            request.timeout = 8;
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning($"WeatherSystem: Real weather fetch failed — {request.error}");
                yield break;
            }

            string json = request.downloadHandler.text;
            ParseAndApplyWeather(json);
            weatherLoaded = true;
            Debug.Log($"WeatherSystem: Real weather applied — {currentWeather} {temperature:F1}°C");
        }
    }

    void ParseAndApplyWeather(string json)
    {
        // Simple JSON parsing without external library
        int wmoCode = ExtractInt(json, "weathercode");
        float temp = ExtractFloat(json, "temperature_2m");

        temperature = temp;
        WeatherState state = WMOCodeToWeatherState(wmoCode);

        // Override snow if city doesn't have it
        if (state == WeatherState.Snow || state == WeatherState.Blizzard)
            if (activeCity != null && !activeCity.IsSnowSeason())
                state = WeatherState.HeavyRain;

        ApplyWeather(state, GetIntensityForState(state), temp);
    }

    WeatherState WMOCodeToWeatherState(int code)
    {
        if (code == 0)                          return WeatherState.Clear;
        if (code <= 2)                          return WeatherState.PartlyCloudy;
        if (code == 3)                          return WeatherState.Overcast;
        if (code == 45 || code == 48)           return WeatherState.Fog;
        if (code >= 51 && code <= 55)           return WeatherState.Drizzle;
        if (code >= 61 && code <= 63)           return WeatherState.LightRain;
        if (code == 65)                         return WeatherState.HeavyRain;
        if (code >= 71 && code <= 75)           return WeatherState.Snow;
        if (code >= 80 && code <= 82)           return WeatherState.LightRain;
        if (code >= 95 && code <= 99)           return WeatherState.Thunderstorm;
        return WeatherState.Clear;
    }

    float GetIntensityForState(WeatherState state)
    {
        switch (state)
        {
            case WeatherState.Clear:        return 0f;
            case WeatherState.PartlyCloudy: return 0.2f;
            case WeatherState.Overcast:     return 0.4f;
            case WeatherState.Fog:          return 0.7f;
            case WeatherState.Drizzle:      return 0.3f;
            case WeatherState.LightRain:    return 0.5f;
            case WeatherState.HeavyRain:    return 0.85f;
            case WeatherState.Thunderstorm: return 1f;
            case WeatherState.Snow:         return 0.6f;
            case WeatherState.Blizzard:     return 1f;
            default:                        return 0f;
        }
    }

    // ── Simulated weather fallback ───────────────────────────────────

    void ApplySimulatedWeather()
    {
        if (activeCity == null)
        {
            ApplyWeather(WeatherState.Clear, 0f, 20f);
            return;
        }

        WeatherState state = SimulateWeatherForCity(activeCity);
        float intensity = GetIntensityForState(state);
        ApplyWeather(state, intensity, activeCity.avgTemperatureCelsius);
    }

    WeatherState SimulateWeatherForCity(CityDefinition city)
    {
        bool isRainy = city.IsRainySeason();
        bool isSnow = city.IsSnowSeason();

        // Weighted random based on climate and season
        float roll = Random.value;

        switch (city.climateZone)
        {
            case ClimateZone.Tropical:
                if (isRainy)
                    return roll < 0.4f ? WeatherState.HeavyRain
                         : roll < 0.6f ? WeatherState.LightRain
                         : roll < 0.8f ? WeatherState.Overcast
                         : WeatherState.PartlyCloudy;
                else
                    return roll < 0.1f ? WeatherState.LightRain
                         : roll < 0.3f ? WeatherState.PartlyCloudy
                         : WeatherState.Clear;

            case ClimateZone.Desert:
                return roll < 0.05f ? WeatherState.LightRain
                     : roll < 0.15f ? WeatherState.Fog
                     : roll < 0.35f ? WeatherState.PartlyCloudy
                     : WeatherState.Clear;

            case ClimateZone.Temperate:
                if (isSnow)
                    return roll < 0.3f ? WeatherState.Snow
                         : roll < 0.5f ? WeatherState.Overcast
                         : roll < 0.7f ? WeatherState.LightRain
                         : WeatherState.Clear;
                return roll < 0.2f ? WeatherState.HeavyRain
                     : roll < 0.4f ? WeatherState.LightRain
                     : roll < 0.6f ? WeatherState.Overcast
                     : roll < 0.75f ? WeatherState.Fog
                     : WeatherState.Clear;

            case ClimateZone.Continental:
                if (isSnow)
                    return roll < 0.5f ? WeatherState.Snow
                         : roll < 0.7f ? WeatherState.Blizzard
                         : WeatherState.Overcast;
                return roll < 0.3f ? WeatherState.LightRain
                     : roll < 0.5f ? WeatherState.Overcast
                     : WeatherState.Clear;

            case ClimateZone.Mediterranean:
                if (isRainy)
                    return roll < 0.4f ? WeatherState.LightRain
                         : roll < 0.6f ? WeatherState.Overcast
                         : WeatherState.Clear;
                return roll < 0.05f ? WeatherState.LightRain
                     : roll < 0.2f ? WeatherState.PartlyCloudy
                     : WeatherState.Clear;

            case ClimateZone.Arid:
            case ClimateZone.Subtropical:
                if (city.hasMonsoon && isRainy)
                    return roll < 0.5f ? WeatherState.HeavyRain
                         : roll < 0.7f ? WeatherState.Thunderstorm
                         : WeatherState.Overcast;
                return roll < 0.1f ? WeatherState.Drizzle
                     : roll < 0.3f ? WeatherState.PartlyCloudy
                     : WeatherState.Clear;

            default:
                return WeatherState.Clear;
        }
    }

    // ── Apply weather to all controllers ────────────────────────────

    public void ApplyWeather(WeatherState state, float intensity, float temp)
    {
        currentWeather = state;
        weatherIntensity = intensity;
        temperature = temp;

        Debug.Log($"WeatherSystem: {state} | Intensity: {intensity:F2} | Temp: {temp:F1}°C");

        rainController?.SetRain(state, intensity);
        fogController?.SetFog(state, intensity);
        snowController?.SetSnow(state, intensity);
        skyController?.SetSky(state, intensity, temp);
    }

    // ── Gradual weather change cycle ─────────────────────────────────

    IEnumerator WeatherChangeCycle()
    {
        while (true)
        {
            // Wait 3-8 real minutes between changes
            yield return new WaitForSeconds(Random.Range(180f, 480f));

            if (forceWeather) continue;
            if (activeCity == null) continue;

            WeatherState newState = SimulateWeatherForCity(activeCity);

            // Don't jump too dramatically
            if (newState == WeatherState.Blizzard && currentWeather == WeatherState.Clear)
                newState = WeatherState.Snow;
            if (newState == WeatherState.Thunderstorm && currentWeather == WeatherState.Clear)
                newState = WeatherState.HeavyRain;

            yield return StartCoroutine(TransitionToWeather(newState));
        }
    }

    IEnumerator TransitionToWeather(WeatherState newState)
    {
        float targetInt = GetIntensityForState(newState);
        float elapsed = 0f;
        float duration = Random.Range(180f, 300f); // 3-5 minute transition

        WeatherState oldState = currentWeather;
        float oldIntensity = weatherIntensity;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            float blendedIntensity = Mathf.Lerp(oldIntensity, targetInt, t);

            rainController?.SetRain(newState, blendedIntensity * t);
            fogController?.SetFog(newState, blendedIntensity * t);
            snowController?.SetSnow(newState, blendedIntensity * t);
            skyController?.SetSky(newState, blendedIntensity * t, temperature);

            yield return null;
        }

        currentWeather = newState;
        weatherIntensity = targetInt;
        Debug.Log($"WeatherSystem: Transitioned to {newState}");
    }

    // ── Helpers ──────────────────────────────────────────────────────

    int ExtractInt(string json, string key)
    {
        string search = $"\"{key}\":";
        int idx = json.IndexOf(search);
        if (idx < 0) return 0;
        idx += search.Length;
        int end = idx;
        while (end < json.Length && (char.IsDigit(json[end]) || json[end] == '-'))
            end++;
        if (int.TryParse(json.Substring(idx, end - idx), out int val))
            return val;
        return 0;
    }

    float ExtractFloat(string json, string key)
    {
        string search = $"\"{key}\":";
        int idx = json.IndexOf(search);
        if (idx < 0) return 0f;
        idx += search.Length;
        int end = idx;
        while (end < json.Length && (char.IsDigit(json[end]) ||
               json[end] == '.' || json[end] == '-'))
            end++;
        if (float.TryParse(json.Substring(idx, end - idx),
            System.Globalization.NumberStyles.Float,
            System.Globalization.CultureInfo.InvariantCulture,
            out float val))
            return val;
        return 0f;
    }

    // ── Public API ───────────────────────────────────────────────────

    public bool IsRaining() =>
        currentWeather == WeatherState.LightRain ||
        currentWeather == WeatherState.HeavyRain ||
        currentWeather == WeatherState.Thunderstorm ||
        currentWeather == WeatherState.Drizzle;

    public bool IsSnowing() =>
        currentWeather == WeatherState.Snow ||
        currentWeather == WeatherState.Blizzard;

    public float GetRoadGripMultiplier()
    {
        switch (currentWeather)
        {
            case WeatherState.LightRain:    return 0.85f;
            case WeatherState.HeavyRain:    return 0.70f;
            case WeatherState.Thunderstorm: return 0.65f;
            case WeatherState.Snow:         return 0.50f;
            case WeatherState.Blizzard:     return 0.35f;
            case WeatherState.Fog:          return 0.90f;
            default:                        return 1.0f;
        }
    }
}
