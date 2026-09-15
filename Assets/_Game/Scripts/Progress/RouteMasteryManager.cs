using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class RouteMasteryRecord
{
    public string routeId;
    public float smoothness;
    public float punctuality;
    public float safety;
    public float efficiency;
    public int completedRuns;
    public long updatedAtUnixSeconds;
}

public class RouteMasteryManager : MonoBehaviour
{
    const string KeyPrefix = "RealBus.RouteMastery.v1.";
    public static RouteMasteryManager Instance { get; private set; }
    MissionManager hookedMission;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static RouteMasteryManager EnsureExists()
    {
        if (Instance != null) return Instance;
        RouteMasteryManager existing = FindFirstObjectByType<RouteMasteryManager>();
        return existing != null ? existing : new GameObject("RouteMasteryManager").AddComponent<RouteMasteryManager>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start() { StartCoroutine(BindWhenReady()); }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { StartCoroutine(BindWhenReady()); }
    void OnDestroy() { if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded; Unbind(); }

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
        if (hookedMission == mission) return; Unbind(); hookedMission = mission; hookedMission.OnMissionCompleted += RecordResult;
    }
    void Unbind() { if (hookedMission != null) hookedMission.OnMissionCompleted -= RecordResult; hookedMission = null; }

    void RecordResult(MissionResult result)
    {
        MissionManager mission = MissionManager.Instance; if (result == null || mission == null || mission.currentRoute == null) return;
        CityDefinition city = GameState.Instance != null ? GameState.Instance.selectedCity : CityManager.Instance?.activeCity;
        string routeId = mission.currentRoute.GetProgressionId(city != null ? city.cityCode : null);
        RouteMasteryRecord record = GetRecord(routeId) ?? new RouteMasteryRecord { routeId = routeId };
        record.smoothness = Mathf.Max(record.smoothness, result.satisfactionScore);
        record.punctuality = Mathf.Max(record.punctuality, result.punctualityScore);
        record.safety = Mathf.Max(record.safety, result.safetyScore);
        record.efficiency = Mathf.Max(record.efficiency, result.efficiencyScore);
        record.completedRuns++; record.updatedAtUnixSeconds = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        PlayerPrefs.SetString(Key(routeId), JsonUtility.ToJson(record)); PlayerPrefs.Save();
    }

    public RouteMasteryRecord GetRecord(string routeId)
    {
        if (string.IsNullOrWhiteSpace(routeId)) return null; string json = PlayerPrefs.GetString(Key(routeId), string.Empty);
        if (string.IsNullOrWhiteSpace(json)) return null;
        try { return JsonUtility.FromJson<RouteMasteryRecord>(json); } catch (System.Exception) { return null; }
    }

    public string GetCompactLabel(string routeId)
    {
        RouteMasteryRecord record = GetRecord(routeId); if (record == null || record.completedRuns <= 0) return "MASTERY —";
        float overall = Mathf.Min(Mathf.Min(record.smoothness, record.punctuality), Mathf.Min(record.safety, record.efficiency));
        return "MASTERY " + Medal(overall).ToUpperInvariant();
    }

    public static string Medal(float score)
    {
        if (score >= 95f) return "Platinum";
        if (score >= 85f) return "Gold";
        if (score >= 75f) return "Silver";
        if (score >= 60f) return "Bronze";
        return "Developing";
    }

    static string Key(string routeId) { return KeyPrefix + routeId; }
}
