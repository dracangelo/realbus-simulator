using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DailyChallengeManager : MonoBehaviour
{
    const string LastCompletionKey = "RealBus.DailyChallenge.LastCompletion";
    const string StreakKey = "RealBus.DailyChallenge.Streak";
    public static DailyChallengeManager Instance { get; private set; }
    public CityDefinition ChallengeCity { get; private set; }
    public BusRoute ChallengeRoute { get; private set; }
    public MissionData ChallengeMission { get; private set; }
    public string ChallengeKey { get; private set; }
    public bool IsActive { get; private set; }
    public int CurrentStreak => PlayerPrefs.GetInt(StreakKey, 0);
    MissionManager hookedMission;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static DailyChallengeManager EnsureExists()
    {
        if (Instance != null) return Instance;
        DailyChallengeManager existing = FindFirstObjectByType<DailyChallengeManager>();
        return existing != null ? existing : new GameObject("DailyChallengeManager").AddComponent<DailyChallengeManager>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start() { RefreshChallenge(); StartCoroutine(BindMissionWhenReady()); }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { StartCoroutine(BindMissionWhenReady()); }
    void OnDestroy() { if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded; UnbindMission(); if (ChallengeMission != null) Destroy(ChallengeMission); }

    public bool RefreshChallenge()
    {
        CityDefinition city = FindPlayableCity();
        if (city == null) { ClearChallenge(); return false; }
        List<BusRoute> routes = PlayableRoutes(city);
        if (routes.Count == 0) { ClearChallenge(); return false; }
        string date = System.DateTime.Now.ToString("yyyy-MM-dd");
        string cityCode = string.IsNullOrWhiteSpace(city.cityCode) ? city.cityName : city.cityCode;
        int seed = StableSeed(date + "|" + cityCode);
        ChallengeCity = city; ChallengeRoute = routes[PositiveMod(seed, routes.Count)]; ChallengeKey = date + "|" + cityCode + "|" + ChallengeRoute.GetProgressionId(cityCode);
        BuildMission(seed); return true;
    }

    public bool ActivateChallenge()
    {
        if (ChallengeRoute == null && !RefreshChallenge()) return false;
        GameState state = GameState.Instance;
        if (state == null) return false;
        state.SelectCity(ChallengeCity); state.SelectRoute(ChallengeRoute);
        if (CityManager.Instance != null) CityManager.Instance.SetActiveCity(ChallengeCity);
        IsActive = true; return true;
    }

    public bool AppliesTo(BusRoute route) { return IsActive && route != null && route == ChallengeRoute && ChallengeMission != null; }
    public bool IsCompletedToday => !string.IsNullOrWhiteSpace(ChallengeKey) && PlayerPrefs.GetInt("RealBus.DailyChallenge.Completed." + ChallengeKey, 0) != 0;

    public string GetSummary()
    {
        if (ChallengeRoute == null || ChallengeMission == null) return "Select a city with a downloaded route to unlock today's challenge.";
        string done = IsCompletedToday ? "COMPLETED" : "AVAILABLE";
        return $"{done}  •  {ChallengeCity.cityName}\n{ChallengeRoute.routeName}\n{ChallengeMission.GetDepartureLabel()}  •  {ChallengeMission.GetRequiredWeatherLabel()}\n{ChallengeMission.GetScenarioObjectiveText()}\n+{ChallengeMission.scenarioBonusXP} bonus XP  •  {CurrentStreak} day streak";
    }

    void BuildMission(int seed)
    {
        if (ChallengeMission != null) Destroy(ChallengeMission);
        ChallengeMission = ScriptableObject.CreateInstance<MissionData>(); ChallengeMission.hideFlags = HideFlags.DontSave;
        ChallengeMission.route = ChallengeRoute; ChallengeMission.missionName = "Daily Route Challenge";
        ChallengeMission.description = "A one-day challenge on a real imported route.";
        ChallengeMission.scheduledDepartureTime = ResolveDepartureMinutes(seed);
        ChallengeMission.targetDurationMinutes = Mathf.Max(12f, ChallengeRoute.GetStopCount() * 4f);
        ChallengeMission.minPassengersTarget = Mathf.Max(12, ChallengeRoute.GetStopCount() * 3);
        ChallengeMission.stretchPassengersTarget = ChallengeMission.minPassengersTarget + 8;
        ChallengeMission.punctualityTarget = 0.82f; ChallengeMission.minimumSafetyScore = 0.82f;
        ChallengeMission.scenarioBonusXP = 250; ChallengeMission.timeBonusXP = 75;
        ChallengeMission.starRating = Mathf.Clamp(ChallengeRoute.difficulty + 1, 2, 5);
        ChallengeMission.difficultyMultiplier = 1.15f;
        int mode = PositiveMod(seed / 7, 4);
        ChallengeMission.missionType = mode == 0 ? MissionArchetype.RushHourChaos : mode == 1 ? MissionArchetype.WeatherChallenge : mode == 2 ? MissionArchetype.RuleEnforcement : MissionArchetype.UnexpectedEvents;
        ChallengeMission.requiredWeather = ResolveWeather(seed);
        ChallengeMission.requiredWeatherIntensity = ChallengeMission.requiredWeather == WeatherState.Clear ? 0f : 0.75f;
        ChallengeMission.applyRequiredWeather = true;
        ChallengeMission.minimumDynamicEvents = ChallengeMission.IsUnexpectedEventMission ? 2 : 0;
        ChallengeMission.maximumAllowedViolations = ChallengeMission.IsRuleEnforcementMission ? 0 : 2;
        ChallengeMission.specialInstruction = "Daily conditions use your local date and departure window.";
    }

    void RecordCompletion(MissionResult result)
    {
        if (!IsActive || ChallengeRoute == null || MissionManager.Instance == null || MissionManager.Instance.currentRoute != ChallengeRoute) return;
        IsActive = false;
        if (result == null || !result.scenarioObjectivePassed || IsCompletedToday) return;
        PlayerPrefs.SetInt("RealBus.DailyChallenge.Completed." + ChallengeKey, 1);
        System.DateTime today = System.DateTime.Now.Date; System.DateTime previous;
        string previousRaw = PlayerPrefs.GetString(LastCompletionKey, string.Empty);
        int streak = System.DateTime.TryParseExact(previousRaw, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out previous) && previous.Date == today.AddDays(-1)
            ? CurrentStreak + 1 : 1;
        PlayerPrefs.SetInt(StreakKey, streak); PlayerPrefs.SetString(LastCompletionKey, today.ToString("yyyy-MM-dd")); PlayerPrefs.Save();
        SubtitleManager.EnsureExists().Show("Daily challenge", $"Completed! Your streak is now {streak} days.", 4f);
    }

    IEnumerator BindMissionWhenReady()
    {
        for (int frame = 0; frame < 180; frame++)
        {
            if (MissionManager.Instance != null) { BindMission(MissionManager.Instance); yield break; }
            yield return null;
        }
    }

    void BindMission(MissionManager mission)
    {
        if (hookedMission == mission) return; UnbindMission(); hookedMission = mission; hookedMission.OnMissionCompleted += RecordCompletion;
    }
    void UnbindMission() { if (hookedMission != null) hookedMission.OnMissionCompleted -= RecordCompletion; hookedMission = null; }

    CityDefinition FindPlayableCity()
    {
        CityDefinition selected = GameState.Instance != null ? GameState.Instance.selectedCity : null;
        if (PlayableRoutes(selected).Count > 0) return selected;
        CityDefinition active = CityManager.Instance != null ? CityManager.Instance.activeCity : null;
        if (PlayableRoutes(active).Count > 0) return active;
        CityDefinition[] cities = CityManager.Instance != null ? CityManager.Instance.allCities : null;
        if (cities != null) for (int i = 0; i < cities.Length; i++) if (PlayableRoutes(cities[i]).Count > 0) return cities[i];
        return null;
    }

    static List<BusRoute> PlayableRoutes(CityDefinition city)
    {
        var result = new List<BusRoute>(); if (city == null || city.availableRoutes == null) return result;
        for (int i = 0; i < city.availableRoutes.Length; i++)
        {
            BusRoute route = city.availableRoutes[i]; if (route != null && route.stops != null && route.stops.Length >= 2) result.Add(route);
        }
        return result;
    }

    void ClearChallenge() { ChallengeCity = null; ChallengeRoute = null; ChallengeKey = string.Empty; if (ChallengeMission != null) Destroy(ChallengeMission); ChallengeMission = null; IsActive = false; }
    static WeatherState ResolveWeather(int seed)
    {
        if (WeatherSystem.Instance != null) return WeatherSystem.Instance.currentWeather;
        WeatherState[] weather = { WeatherState.Clear, WeatherState.PartlyCloudy, WeatherState.LightRain, WeatherState.HeavyRain, WeatherState.Fog };
        return weather[PositiveMod(seed / 31, weather.Length)];
    }
    public static float ResolveDepartureMinutes(int seed)
    {
        int[] windows = { 420, 480, 720, 960, 1020 }; return windows[PositiveMod(seed / 13, windows.Length)];
    }
    public static int StableSeed(string value)
    {
        unchecked { uint hash = 2166136261u; string text = value ?? string.Empty; for (int i = 0; i < text.Length; i++) { hash ^= text[i]; hash *= 16777619u; } return (int)(hash & 0x7fffffff); }
    }
    static int PositiveMod(int value, int divisor) { return divisor <= 0 ? 0 : (value & 0x7fffffff) % divisor; }
}
