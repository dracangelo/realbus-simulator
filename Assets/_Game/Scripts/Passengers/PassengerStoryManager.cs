using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PassengerStoryProfile
{
    public string passengerId;
    public string displayName;
    public int rides;
    public float affinity = 50f;
    public int storyStage;
    public string lastRouteId;
}

[System.Serializable]
class PassengerStorySave
{
    public List<PassengerStoryProfile> profiles = new List<PassengerStoryProfile>();
    public int completedStoryRoutes;
}

public class PassengerStoryManager : MonoBehaviour
{
    const string SaveKey = "RealBus.PassengerStories.v1";
    public static PassengerStoryManager Instance { get; private set; }
    PassengerStorySave data = new PassengerStorySave();
    MissionManager hookedMission;
    bool routeWasActive;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static PassengerStoryManager EnsureExists()
    {
        if (Instance != null) return Instance;
        PassengerStoryManager existing = FindFirstObjectByType<PassengerStoryManager>();
        return existing != null ? existing : new GameObject("PassengerStoryManager").AddComponent<PassengerStoryManager>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); SceneManager.sceneLoaded += OnSceneLoaded; Load();
    }

    void Start() { StartCoroutine(BindWhenReady()); }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { routeWasActive = false; StartCoroutine(BindWhenReady()); }
    void OnDestroy() { if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded; Unbind(); }

    void Update()
    {
        bool active = hookedMission != null && hookedMission.routeActive;
        if (active && !routeWasActive) StartCoroutine(ShowRegularPassengerMoment());
        routeWasActive = active;
    }

    IEnumerator BindWhenReady()
    {
        for (int frame = 0; frame < 180; frame++)
        {
            if (MissionManager.Instance != null) { Bind(MissionManager.Instance); yield break; }
            yield return null;
        }
    }

    void Bind(MissionManager mission)
    {
        if (hookedMission == mission) return; Unbind(); hookedMission = mission; hookedMission.OnMissionCompleted += RecordRoute;
    }
    void Unbind() { if (hookedMission != null) hookedMission.OnMissionCompleted -= RecordRoute; hookedMission = null; }

    IEnumerator ShowRegularPassengerMoment()
    {
        yield return new WaitForSeconds(8f);
        if (hookedMission == null || !hookedMission.routeActive || data.profiles.Count == 0) yield break;
        PassengerStoryProfile profile = data.profiles[PositiveMod(data.completedStoryRoutes, data.profiles.Count)];
        if (profile.rides <= 0) yield break;
        SubtitleManager.EnsureExists().Show(profile.displayName, Greeting(profile), 4f);
    }

    void RecordRoute(MissionResult result)
    {
        if (result == null || hookedMission == null || hookedMission.currentRoute == null || data.profiles.Count == 0) return;
        CityDefinition city = GameState.Instance != null ? GameState.Instance.selectedCity : CityManager.Instance?.activeCity;
        string routeId = hookedMission.currentRoute.GetProgressionId(city != null ? city.cityCode : null);
        PassengerStoryProfile profile = data.profiles[PositiveMod(DailyChallengeManager.StableSeed(routeId) + data.completedStoryRoutes, data.profiles.Count)];
        int oldStage = profile.storyStage; profile.rides++;
        float quality = (result.satisfactionScore + result.safetyScore) * 0.5f;
        profile.affinity = Mathf.Clamp(profile.affinity + (quality - 65f) * 0.12f, 0f, 100f);
        profile.storyStage = CalculateStoryStage(profile.rides, profile.affinity); profile.lastRouteId = routeId;
        data.completedStoryRoutes++; Save();
        string line = profile.storyStage > oldStage ? StoryMilestone(profile) : Farewell(profile, quality);
        SubtitleManager.EnsureExists().Show(profile.displayName, line, 4f);
    }

    void Load()
    {
        string json = PlayerPrefs.GetString(SaveKey, string.Empty);
        if (!string.IsNullOrWhiteSpace(json)) { try { data = JsonUtility.FromJson<PassengerStorySave>(json); } catch (System.Exception) { data = null; } }
        if (data == null) data = new PassengerStorySave();
        if (data.profiles == null) data.profiles = new List<PassengerStoryProfile>();
        if (data.profiles.Count == 0)
        {
            data.profiles.Add(Profile("regular.amina", "Amina")); data.profiles.Add(Profile("regular.kamau", "Kamau"));
            data.profiles.Add(Profile("regular.maya", "Maya")); data.profiles.Add(Profile("regular.leo", "Leo")); Save();
        }
    }

    void Save() { PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(data)); PlayerPrefs.Save(); }
    static PassengerStoryProfile Profile(string id, string name) { return new PassengerStoryProfile { passengerId = id, displayName = name }; }
    static string Greeting(PassengerStoryProfile profile) { return profile.storyStage >= 3 ? "Good to see my favorite driver again." : profile.storyStage >= 1 ? "Nice to see a familiar driver." : "Hello again."; }
    static string Farewell(PassengerStoryProfile profile, float quality) { return quality >= 85f ? "Smooth trip. I'll look for your bus next time." : quality >= 65f ? "Thanks. See you on another route." : "I hope the next journey is gentler."; }
    static string StoryMilestone(PassengerStoryProfile profile) { return profile.storyStage >= 3 ? "You remembered my stop—this route feels like home now." : profile.storyStage == 2 ? "Your reliable driving makes my commute easier." : "I'm starting to recognize your bus."; }
    public static int CalculateStoryStage(int rides, float affinity) { return rides >= 8 && affinity >= 80f ? 3 : rides >= 5 && affinity >= 68f ? 2 : rides >= 2 && affinity >= 55f ? 1 : 0; }
    static int PositiveMod(int value, int divisor) { return divisor <= 0 ? 0 : (value & 0x7fffffff) % divisor; }
}
