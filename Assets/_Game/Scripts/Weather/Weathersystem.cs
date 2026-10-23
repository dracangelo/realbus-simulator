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
    Blizzard,
    /// <summary>
    /// Dry, gusty cloud: the sky is mostly covered but it is not raining. This is
    /// the state the old weather set could not describe, so a windy overcast had to
    /// be called "Overcast" even while the clouds raced across the sky.
    /// </summary>
    Windy
}

public class WeatherSystem : MonoBehaviour
{
    public static WeatherSystem Instance { get; private set; }

    [Header("Current Weather")]
    [SerializeField] WeatherState currentWeatherState = WeatherState.Clear;
    public float temperature = 20f;
    public float weatherIntensity = 0f; // 0-1
    [Tooltip("Wind reported by the weather service, in km/h. Zero when the simulated fallback is in use.")]
    public float realWindSpeedKmh = 0f;
    [Tooltip("Cloud cover reported by the weather service, 0-100. Negative when unknown.")]
    public float realCloudCover = -1f;

    [Header("Transition")]
    public float transitionSpeed = 0.5f; // legacy tuning value
    public float transitionMinSeconds = 45f;
    public float transitionMaxSeconds = 90f;

    [Header("Weather Cycle")]
    [Tooltip("Keep the weather moving after the first state. Off pins it until something calls ApplyWeather/TransitionWeather.")]
    public bool advanceWeatherAutomatically = true;
    [Tooltip("How long a state holds before the next change, in real seconds.")]
    public float weatherHoldMinSeconds = 90f;
    public float weatherHoldMaxSeconds = 240f;
    [Tooltip("The first change after an offline/simulated start. Short enough that a normal drive sees the sky move.")]
    public float simulatedFirstChangeSeconds = 45f;

    [Header("Controllers — assign in Inspector")]
    public RainController rainController;
    public FogController fogController;
    public SnowController snowController;
    public SkyController skyController;
    [Tooltip("Clouds, stars, moon and skybox. Without it the weather only changed light levels.")]
    public SkyEnvironmentController skyEnvironmentController;

    [Header("Debug")]
    public bool forceWeather = false;
    public WeatherState forcedWeatherState = WeatherState.Clear;

    private CityDefinition activeCity;
    private bool weatherLoaded = false;
    private Coroutine weatherTransitionRoutine;
    float blendedGrip = 1f;

    /// <summary>
    /// Active weather state. Assigning it refreshes the road-grip multiplier so
    /// callers (and tests) observe the new state's grip immediately instead of
    /// waiting for the next transition tick.
    /// </summary>
    public WeatherState currentWeather
    {
        get => currentWeatherState;
        set
        {
            currentWeatherState = value;
            blendedGrip = GripFor(value, 1f);
        }
    }

    public WeatherState TargetWeather { get; private set; } = WeatherState.Clear;
    public float TransitionProgress01 { get; private set; } = 1f;
    public bool IsNightCompositeActive => TimeOfDaySystem.Instance != null && TimeOfDaySystem.Instance.IsNight;
    public string CurrentCompositeWeatherLabel => IsNightCompositeActive ? $"{currentWeather} Night" : currentWeather.ToString();

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
        if (forceWeather) yield break;

        bool simulatedStart = !weatherLoaded;
        if (simulatedStart)
        {
            Debug.Log("WeatherSystem: Using simulated weather");
            ApplySimulatedWeather();
        }

        // Start gradual weather change cycle
        if (advanceWeatherAutomatically)
            StartCoroutine(WeatherChangeCycle(simulatedStart ? simulatedFirstChangeSeconds : -1f));
    }

    // ── Real weather via Open-Meteo ──────────────────────────────────

    IEnumerator TryFetchRealWeather()
    {
        if (activeCity == null) yield break;

        string url = $"https://api.open-meteo.com/v1/forecast" +
                     $"?latitude={activeCity.centreLat:F4}" +
                     $"&longitude={activeCity.centreLon:F4}" +
                     $"&current=weathercode,temperature_2m,wind_speed_10m,cloud_cover" +
                     $"&wind_speed_unit=kmh" +
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
        // A forced state (smoke run, event, debug panel) must not be overwritten by
        // a weather request that was already in flight when the state was forced.
        if (forceWeather)
            return;

        // Simple JSON parsing without external library
        int wmoCode = ExtractInt(json, "weathercode");
        float temp = ExtractFloat(json, "temperature_2m");
        realWindSpeedKmh = ExtractFloat(json, "wind_speed_10m");
        realCloudCover = ExtractFloat(json, "cloud_cover");

        temperature = temp;
        WeatherState state = StateForWmoCode(wmoCode);

        // The WMO code says what is falling, not how hard the air is moving. A dry
        // but gusty overcast is a real and very visible state, so promote a cloudy
        // code to Windy when the service reports a strong breeze.
        state = ApplyObservedConditions(state, realWindSpeedKmh,
            activeCity == null || activeCity.IsSnowSeason());

        ApplyWeather(state, GetIntensityForState(state), temp);
    }

    /// <summary>
    /// WMO weather code to the state the simulator understands. Static and pure so
    /// the mapping can be tested without a network call.
    /// </summary>
    public static WeatherState StateForWmoCode(int code)
    {
        if (code == 0)                          return WeatherState.Clear;
        if (code <= 2)                          return WeatherState.PartlyCloudy;
        if (code == 3)                          return WeatherState.Overcast;
        if (code == 45 || code == 48)           return WeatherState.Fog;
        if (code == 51 || code == 53 || code == 55) return WeatherState.Drizzle;
        if (code == 56 || code == 57)           return WeatherState.Drizzle;
        if (code >= 61 && code <= 63)           return WeatherState.LightRain;
        if (code == 65)                         return WeatherState.HeavyRain;
        if (code == 66 || code == 67)           return WeatherState.LightRain;
        if (code >= 71 && code <= 75)           return WeatherState.Snow;
        if (code == 77)                         return WeatherState.Snow;
        if (code >= 80 && code <= 82)           return WeatherState.LightRain;
        if (code == 85 || code == 86)           return WeatherState.Snow;
        if (code >= 95 && code <= 99)           return WeatherState.Thunderstorm;
        return WeatherState.Clear;
    }

    /// <summary>
    /// Applies the two observations the weather code cannot carry: a strong dry
    /// wind makes a cloudy sky Windy, and snow is downgraded to rain outside a city's
    /// snow season.
    /// </summary>
    public static WeatherState ApplyObservedConditions(WeatherState state, float windSpeedKmh, bool snowSeason)
    {
        if (windSpeedKmh >= 30f &&
            (state == WeatherState.Clear || state == WeatherState.PartlyCloudy || state == WeatherState.Overcast))
            state = WeatherState.Windy;

        if ((state == WeatherState.Snow || state == WeatherState.Blizzard) && !snowSeason)
            state = WeatherState.HeavyRain;

        return state;
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
            case WeatherState.Windy:        return 0.45f;
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
                         : roll < 0.45f ? WeatherState.Windy
                         : WeatherState.Clear;

            case ClimateZone.Desert:
                return roll < 0.05f ? WeatherState.LightRain
                     : roll < 0.15f ? WeatherState.Fog
                     : roll < 0.35f ? WeatherState.PartlyCloudy
                     : roll < 0.55f ? WeatherState.Windy
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
                     : roll < 0.72f ? WeatherState.Fog
                     : roll < 0.84f ? WeatherState.Windy
                     : WeatherState.Clear;

            case ClimateZone.Continental:
                if (isSnow)
                    return roll < 0.5f ? WeatherState.Snow
                         : roll < 0.7f ? WeatherState.Blizzard
                         : WeatherState.Overcast;
                return roll < 0.3f ? WeatherState.LightRain
                     : roll < 0.5f ? WeatherState.Overcast
                     : roll < 0.65f ? WeatherState.Windy
                     : WeatherState.Clear;

            case ClimateZone.Mediterranean:
                if (isRainy)
                    return roll < 0.4f ? WeatherState.LightRain
                         : roll < 0.6f ? WeatherState.Overcast
                         : WeatherState.Clear;
                return roll < 0.05f ? WeatherState.LightRain
                     : roll < 0.2f ? WeatherState.PartlyCloudy
                     : roll < 0.38f ? WeatherState.Windy
                     : WeatherState.Clear;

            case ClimateZone.Arid:
            case ClimateZone.Subtropical:
                if (city.hasMonsoon && isRainy)
                    return roll < 0.5f ? WeatherState.HeavyRain
                         : roll < 0.7f ? WeatherState.Thunderstorm
                         : WeatherState.Overcast;
                return roll < 0.1f ? WeatherState.Drizzle
                     : roll < 0.3f ? WeatherState.PartlyCloudy
                     : roll < 0.45f ? WeatherState.Windy
                     : WeatherState.Clear;

            default:
                return WeatherState.Clear;
        }
    }

    // ── Apply weather to all controllers ────────────────────────────

    public void ApplyWeather(WeatherState state, float intensity, float temp)
    {
        if (weatherTransitionRoutine != null) { StopCoroutine(weatherTransitionRoutine); weatherTransitionRoutine = null; }
        // Assign the state first (its setter refreshes the base grip), then apply
        // the intensity-scaled grip so the blended value stays authoritative.
        currentWeather = state;
        blendedGrip = GripFor(state, intensity);
        TargetWeather = state;
        TransitionProgress01 = 1f;
        weatherIntensity = intensity;
        temperature = temp;

        Debug.Log($"WeatherSystem: {state} | Intensity: {intensity:F2} | Temp: {temp:F1}°C");

        rainController?.SetRain(state, intensity);
        fogController?.SetFog(state, intensity);
        snowController?.SetSnow(state, intensity);
        skyController?.SetSky(state, intensity, temp);
        ResolveSkyEnvironment()?.ApplyEnvironment(state, intensity);
    }

    /// <summary>
    /// The sky environment can be created after the weather system (a fixture may add
    /// the weather first), so it is resolved on demand rather than only at wiring time.
    /// </summary>
    SkyEnvironmentController ResolveSkyEnvironment()
    {
        if (skyEnvironmentController == null) skyEnvironmentController = SkyEnvironmentController.Instance;
        return skyEnvironmentController;
    }

    // ── Gradual weather change cycle ─────────────────────────────────

    IEnumerator WeatherChangeCycle(float firstHoldSeconds = -1f)
    {
        float minHold = Mathf.Max(1f, Mathf.Min(weatherHoldMinSeconds, weatherHoldMaxSeconds));
        float maxHold = Mathf.Max(minHold, Mathf.Max(weatherHoldMinSeconds, weatherHoldMaxSeconds));
        float firstHold = firstHoldSeconds >= 0f
            ? firstHoldSeconds
            : Random.Range(minHold, maxHold);

        yield return new WaitForSeconds(Mathf.Max(1f, firstHold));

        while (true)
        {
            if (!forceWeather && advanceWeatherAutomatically && activeCity != null)
            {
                WeatherState newState = PickNextWeather();

                if (weatherTransitionRoutine != null)
                    StopCoroutine(weatherTransitionRoutine);

                weatherTransitionRoutine = StartCoroutine(TransitionToWeather(newState));
                yield return weatherTransitionRoutine;
                weatherTransitionRoutine = null;
            }

            yield return new WaitForSeconds(Random.Range(minHold, maxHold));
        }
    }

    WeatherState PickNextWeather()
    {
        WeatherState suggestion = activeCity != null
            ? SimulateWeatherForCity(activeCity)
            : WeatherState.PartlyCloudy;
        return ChooseNextWeather(currentWeather, suggestion, Random.value);
    }

    /// <summary>
    /// The next state has to look like weather rather than a dice roll. The local
    /// climate suggests a state; a little over half the time we follow that, and the
    /// rest of the time we step to a neighbouring state so a clear morning can become
    /// windy, then overcast, then rain, instead of teleporting between extremes.
    /// Pure, so the sequence rules can be checked without waiting minutes for them.
    /// </summary>
    public static WeatherState ChooseNextWeather(WeatherState current, WeatherState climateSuggestion, float roll)
    {
        roll = Mathf.Repeat(roll, 1f);
        WeatherState candidate = roll < 0.55f
            ? climateSuggestion
            : AdjacentWeather(current, roll);

        WeatherState result = SoftenTransition(current, candidate);
        if (result == current)
            result = AdjacentWeather(current, Mathf.Repeat(roll + 0.37f, 1f));
        return result;
    }

    /// <summary>A plausible one-step neighbour for a state, weighted so the common move is the gentlest one.</summary>
    public static WeatherState AdjacentWeather(WeatherState current, float roll)
    {
        roll = Mathf.Repeat(roll, 1f);
        switch (current)
        {
            case WeatherState.Clear:
                return roll < 0.65f ? WeatherState.PartlyCloudy
                     : roll < 0.90f ? WeatherState.Windy
                     : WeatherState.Overcast;

            case WeatherState.PartlyCloudy:
                return roll < 0.40f ? WeatherState.Clear
                     : roll < 0.70f ? WeatherState.Windy
                     : WeatherState.Overcast;

            case WeatherState.Windy:
                return roll < 0.45f ? WeatherState.PartlyCloudy
                     : roll < 0.80f ? WeatherState.Overcast
                     : WeatherState.Clear;

            case WeatherState.Overcast:
                return roll < 0.30f ? WeatherState.Windy
                     : roll < 0.55f ? WeatherState.Drizzle
                     : roll < 0.80f ? WeatherState.LightRain
                     : WeatherState.Fog;

            case WeatherState.Fog:
                return roll < 0.60f ? WeatherState.Overcast : WeatherState.PartlyCloudy;

            case WeatherState.Drizzle:
                return roll < 0.45f ? WeatherState.Overcast
                     : roll < 0.85f ? WeatherState.LightRain
                     : WeatherState.PartlyCloudy;

            case WeatherState.LightRain:
                return roll < 0.30f ? WeatherState.Drizzle
                     : roll < 0.80f ? WeatherState.HeavyRain
                     : WeatherState.Overcast;

            case WeatherState.HeavyRain:
                return roll < 0.40f ? WeatherState.LightRain
                     : roll < 0.80f ? WeatherState.Thunderstorm
                     : WeatherState.Overcast;

            case WeatherState.Thunderstorm:
                return roll < 0.60f ? WeatherState.HeavyRain : WeatherState.LightRain;

            case WeatherState.Snow:
                return roll < 0.40f ? WeatherState.Blizzard
                     : roll < 0.75f ? WeatherState.Overcast
                     : WeatherState.LightRain;

            case WeatherState.Blizzard:
                return roll < 0.60f ? WeatherState.Snow : WeatherState.Overcast;

            default:
                return WeatherState.PartlyCloudy;
        }
    }

    /// <summary>
    /// Removes the jumps that make procedural weather feel like a switch: no storm
    /// out of a clear sky and no blizzard without snow already falling. The sky can
    /// still reach every state, it just has to pass through the state next door.
    /// </summary>
    public static WeatherState SoftenTransition(WeatherState from, WeatherState to)
    {
        bool dryAndCalm = from == WeatherState.Clear || from == WeatherState.PartlyCloudy || from == WeatherState.Windy;

        if (to == WeatherState.Thunderstorm && from != WeatherState.HeavyRain &&
            from != WeatherState.LightRain && from != WeatherState.Thunderstorm)
            return WeatherState.HeavyRain;

        if (to == WeatherState.Blizzard && from != WeatherState.Snow && from != WeatherState.Blizzard)
            return WeatherState.Snow;

        if (to == WeatherState.HeavyRain && dryAndCalm)
            return WeatherState.Overcast;

        if ((to == WeatherState.Drizzle || to == WeatherState.LightRain) && dryAndCalm)
            return WeatherState.Overcast;

        if (to == WeatherState.Snow && dryAndCalm)
            return WeatherState.Overcast;

        if (to == WeatherState.Fog && dryAndCalm)
            return WeatherState.Overcast;

        if (to == WeatherState.Clear && (from == WeatherState.Thunderstorm || from == WeatherState.Blizzard))
            return WeatherState.Overcast;

        return to;
    }

    public void TransitionWeather(WeatherState state)
    {
        if (weatherTransitionRoutine != null) StopCoroutine(weatherTransitionRoutine);
        weatherTransitionRoutine = StartCoroutine(TransitionToWeather(state));
    }

    IEnumerator TransitionToWeather(WeatherState newState)
    {
        float targetInt = GetIntensityForState(newState);
        float minDuration = Mathf.Max(1f, Mathf.Min(transitionMinSeconds, transitionMaxSeconds));
        float maxDuration = Mathf.Max(minDuration, Mathf.Max(transitionMinSeconds, transitionMaxSeconds));
        float elapsed = 0f;
        float duration = Random.Range(minDuration, maxDuration);
        WeatherState oldState = currentWeather;
        float oldIntensity = weatherIntensity;
        float oldGrip = blendedGrip;
        TargetWeather = newState;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            TransitionProgress01 = t;
            // Fade the outgoing precipitation out before bringing the incoming state in.
            currentWeather = t < 0.5f ? oldState : newState;
            weatherIntensity = t < 0.5f ? oldIntensity * (1f - t * 2f) : targetInt * (t * 2f - 1f);
            blendedGrip = Mathf.Lerp(oldGrip, GripFor(newState, targetInt), t);
            rainController?.SetRain(currentWeather, weatherIntensity);
            fogController?.BlendWeather(oldState, oldIntensity, newState, targetInt, t);
            snowController?.SetSnow(currentWeather, weatherIntensity);
            skyController?.SetSky(currentWeather, weatherIntensity, temperature);
            ResolveSkyEnvironment()?.ApplyEnvironment(currentWeather, weatherIntensity);
            yield return null;
        }
        currentWeather = newState;
        weatherIntensity = targetInt;
        blendedGrip = GripFor(newState, targetInt);
        TransitionProgress01 = 1f;

        // Land the controllers exactly on the destination state. The last frame of
        // the loop is only as close as that frame's delta, and Clear needs its fog
        // hand-back to start now rather than on a later state change.
        rainController?.SetRain(newState, targetInt);
        fogController?.SetFog(newState, targetInt);
        snowController?.SetSnow(newState, targetInt);
        skyController?.SetSky(newState, targetInt, temperature);
        ResolveSkyEnvironment()?.ApplyEnvironment(newState, targetInt);
        weatherTransitionRoutine = null;
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

    public float GetRoadGripMultiplier() => blendedGrip;

    static float GripFor(WeatherState state, float intensity)
    {
        float grip;
        switch (state)
        {
            case WeatherState.LightRain:    grip = 0.85f; break;
            case WeatherState.HeavyRain:    grip = 0.70f; break;
            case WeatherState.Thunderstorm: grip = 0.65f; break;
            case WeatherState.Snow:         grip = 0.50f; break;
            case WeatherState.Blizzard:     grip = 0.35f; break;
            case WeatherState.Fog:          grip = 0.90f; break;
            case WeatherState.Windy:        grip = 1.00f; break;
            default:                        grip = 1.0f; break;
        }
        return Mathf.Lerp(1f, grip, Mathf.Clamp01(intensity));
    }
}
