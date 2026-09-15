using System.Collections;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class BalanceTelemetrySummary
{
    public int completedMissions;
    public float averageScore;
    public float averageNetEarnings;
    public float averageMinutes;
    public float averagePassengers;
    public float earningsPerHour;
}

public class BalanceTelemetryRecorder : MonoBehaviour
{
    const string SummaryKey = "RealBus.BalanceTelemetry.v1";
    public static BalanceTelemetryRecorder Instance { get; private set; }
    public BalanceTelemetrySummary Summary { get; private set; } = new BalanceTelemetrySummary();
    MissionManager hookedMission;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static BalanceTelemetryRecorder EnsureExists()
    {
        if (Instance != null) return Instance;
        BalanceTelemetryRecorder existing = FindFirstObjectByType<BalanceTelemetryRecorder>();
        return existing != null ? existing : new GameObject("BalanceTelemetryRecorder").AddComponent<BalanceTelemetryRecorder>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); SceneManager.sceneLoaded += OnSceneLoaded;
        string json = PlayerPrefs.GetString(SummaryKey, string.Empty);
        if (!string.IsNullOrWhiteSpace(json)) { try { JsonUtility.FromJsonOverwrite(json, Summary); } catch (System.Exception) { } }
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
        if (hookedMission == mission) return;
        Unbind(); hookedMission = mission;
        if (hookedMission != null) hookedMission.OnMissionCompleted += RecordMission;
    }

    void Unbind()
    {
        if (hookedMission != null) hookedMission.OnMissionCompleted -= RecordMission;
        hookedMission = null;
    }

    void RecordMission(MissionResult result)
    {
        if (result == null) return;
        int oldCount = Summary.completedMissions; int count = oldCount + 1;
        Summary.averageScore = RollingAverage(Summary.averageScore, oldCount, result.totalScore);
        Summary.averageNetEarnings = RollingAverage(Summary.averageNetEarnings, oldCount, result.netEarningsKES);
        Summary.averageMinutes = RollingAverage(Summary.averageMinutes, oldCount, result.totalTimeMinutes);
        Summary.averagePassengers = RollingAverage(Summary.averagePassengers, oldCount, result.totalPassengers);
        Summary.completedMissions = count;
        Summary.earningsPerHour = Summary.averageMinutes > 0.1f ? Summary.averageNetEarnings * 60f / Summary.averageMinutes : 0f;
        PlayerPrefs.SetString(SummaryKey, JsonUtility.ToJson(Summary)); PlayerPrefs.Save();
        AppendCsv(result);
    }

    void AppendCsv(MissionResult result)
    {
        try
        {
            string path = Path.Combine(Application.persistentDataPath, "realbus_balance_telemetry.csv");
            bool header = !File.Exists(path);
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                if (header) writer.WriteLine("utc,route,score,stars,minutes,distance_km,passengers,fare_kes,net_kes,collisions,red_lights,upgrades");
                string upgrades = UpgradeManager.Instance != null ? string.Join(";", UpgradeManager.Instance.GetPurchasedKeys()) : string.Empty;
                writer.WriteLine(string.Join(",", System.DateTime.UtcNow.ToString("O"), Csv(result.routeName), F(result.totalScore), result.starRating,
                    F(result.totalTimeMinutes), F(result.totalDistanceKm), result.totalPassengers, result.totalFareKES, result.netEarningsKES,
                    result.collisions, result.redLightViolations, Csv(upgrades)));
            }
        }
        catch (System.Exception ex) { Debug.LogWarning("Balance telemetry could not be written: " + ex.Message); }
    }

    public static float RollingAverage(float average, int count, float value) { return count <= 0 ? value : average + (value - average) / (count + 1); }
    static string F(float value) { return value.ToString("0.###", CultureInfo.InvariantCulture); }
    static string Csv(string value) { return "\"" + (value ?? string.Empty).Replace("\"", "\"\"") + "\""; }
}
