using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class MissionCheckpoint
{
    public int schemaVersion;
    public string routeId;
    public int stopIndex;
    public Vector3 busPosition;
    public Quaternion busRotation;
    public float speedKmh;
    public float elapsedSeconds;
    public float distanceDrivenKm;
    public float scheduleTimeMinutes;
    public int passengerCount;
    public float passengerSatisfaction;
    public int passengersServed;
    public float faresCollected;
    public float sessionIncome;
    public float passengerDistanceKm;
    public float punctualityScore;
    public float satisfactionScore;
    public float safetyScore;
    public float efficiencyScore;
    public int stopsCompleted;
    public int pointDeductions;
    public int collisions;
    public int redLights;
    public long capturedAtUnixSeconds;
}

public class MobileResilienceManager : MonoBehaviour
{
    const string CheckpointKey = "RealBus.MissionCheckpoint.v1";
    public static MobileResilienceManager Instance { get; private set; }
    public float autosaveIntervalSeconds = 90f;
    float nextAutosave;
    Canvas recoveryCanvas;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static MobileResilienceManager EnsureExists()
    {
        if (Instance != null) return Instance;
        MobileResilienceManager existing = FindFirstObjectByType<MobileResilienceManager>();
        return existing != null ? existing : new GameObject("MobileResilienceManager").AddComponent<MobileResilienceManager>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); Application.lowMemory += HandleLowMemory;
        SceneManager.sceneLoaded += HandleSceneLoaded;
        nextAutosave = Time.unscaledTime + autosaveIntervalSeconds;
    }

    void Start() { StartCoroutine(OfferCheckpointWhenReady()); }
    void OnDestroy() { if (Instance == this) { Application.lowMemory -= HandleLowMemory; SceneManager.sceneLoaded -= HandleSceneLoaded; } }
    void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (recoveryCanvas != null) Destroy(recoveryCanvas.gameObject);
        recoveryCanvas = null; StartCoroutine(OfferCheckpointWhenReady());
    }

    void Update()
    {
        MissionManager mission = MissionManager.Instance;
        if (mission != null && (mission.missionState == MissionState.Completed || mission.missionState == MissionState.Failed)) ClearCheckpoint();
        if (Time.unscaledTime < nextAutosave) return;
        nextAutosave = Time.unscaledTime + Mathf.Max(30f, autosaveIntervalSeconds);
        SaveRecoveryState("periodic_mobile_autosave");
    }

    void OnApplicationPause(bool paused) { if (paused) SaveRecoveryState("mobile_suspend"); }
    void OnApplicationFocus(bool focused) { if (!focused) SaveRecoveryState("focus_lost"); }
    void OnApplicationQuit() { SaveRecoveryState("mobile_quit"); }

    public void SaveRecoveryState(string reason)
    {
        SaveManager.EnsureExists().SaveNow(reason);
        CaptureCheckpoint();
        PlayerPrefs.Save();
    }

    public void CaptureCheckpoint()
    {
        MissionManager mission = MissionManager.Instance;
        if (mission == null || !mission.routeActive || mission.currentRoute == null || mission.busController == null) return;
        CityDefinition city = GameState.Instance != null ? GameState.Instance.selectedCity : null;
        MissionCheckpoint checkpoint = new MissionCheckpoint
        {
            schemaVersion = 2,
            routeId = mission.currentRoute.GetProgressionId(city != null ? city.cityCode : null),
            stopIndex = mission.currentStopIndex,
            busPosition = mission.busController.transform.position,
            busRotation = mission.busController.transform.rotation,
            speedKmh = mission.busController.currentSpeedKmh,
            elapsedSeconds = mission.ElapsedSeconds,
            distanceDrivenKm = mission.DistanceDrivenKm,
            scheduleTimeMinutes = ScheduleManager.Instance != null ? ScheduleManager.Instance.currentTimeMinutes : 0f,
            capturedAtUnixSeconds = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };
        PassengerManager passengers = PassengerManager.Instance;
        if (passengers != null)
        {
            checkpoint.passengerCount = passengers.currentPassengers;
            checkpoint.passengerSatisfaction = passengers.averageSatisfaction;
            checkpoint.passengersServed = passengers.totalPassengersServed;
            checkpoint.faresCollected = passengers.totalFaresCollected;
            checkpoint.sessionIncome = passengers.sessionIncome;
            checkpoint.passengerDistanceKm = passengers.totalDistanceKm;
        }
        ScoreTracker score = ScoreTracker.Instance;
        if (score != null)
        {
            checkpoint.punctualityScore = score.punctualityScore; checkpoint.satisfactionScore = score.satisfactionScore;
            checkpoint.safetyScore = score.safetyScore; checkpoint.efficiencyScore = score.efficiencyScore;
            checkpoint.stopsCompleted = score.stopsCompleted; checkpoint.pointDeductions = score.pointDeductions;
            checkpoint.collisions = score.CollisionCount; checkpoint.redLights = score.RedLightViolationCount;
        }
        PlayerPrefs.SetString(CheckpointKey, JsonUtility.ToJson(checkpoint));
    }

    public static bool TryGetRecentCheckpoint(string routeId, out MissionCheckpoint checkpoint, long maxAgeSeconds = 21600)
    {
        checkpoint = null; string json = PlayerPrefs.GetString(CheckpointKey, string.Empty);
        if (string.IsNullOrWhiteSpace(json)) return false;
        try { checkpoint = JsonUtility.FromJson<MissionCheckpoint>(json); } catch (System.Exception) { return false; }
        if (checkpoint == null || checkpoint.schemaVersion < 2 || !string.Equals(checkpoint.routeId, routeId, System.StringComparison.OrdinalIgnoreCase)) return false;
        long age = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds() - checkpoint.capturedAtUnixSeconds;
        return age >= 0 && age <= Mathf.Max(60, (int)maxAgeSeconds);
    }

    public static void ClearCheckpoint() { PlayerPrefs.DeleteKey(CheckpointKey); }

    IEnumerator OfferCheckpointWhenReady()
    {
        for (int frame = 0; frame < 180; frame++)
        {
            MissionManager mission = MissionManager.Instance;
            BusRoute route = GameState.Instance != null && GameState.Instance.selectedRoute != null
                ? GameState.Instance.selectedRoute : mission != null ? mission.currentRoute : null;
            if (mission != null && route != null && mission.busController != null && GPSManager.Instance != null)
            {
                CityDefinition city = GameState.Instance != null ? GameState.Instance.selectedCity : null;
                string routeId = route.GetProgressionId(city != null ? city.cityCode : null);
                if (!mission.routeActive && TryGetRecentCheckpoint(routeId, out MissionCheckpoint checkpoint))
                    ShowRecoveryPrompt(mission, route, checkpoint);
                yield break;
            }
            yield return null;
        }
    }

    void ShowRecoveryPrompt(MissionManager mission, BusRoute route, MissionCheckpoint checkpoint)
    {
        if (recoveryCanvas == null) BuildRecoveryPrompt();
        recoveryCanvas.gameObject.SetActive(true);
        TextMeshProUGUI detail = recoveryCanvas.transform.Find("Shade/Card/Detail")?.GetComponent<TextMeshProUGUI>();
        if (detail != null)
        {
            string stop = route.stops != null && route.stops.Length > 0
                ? route.stops[Mathf.Clamp(checkpoint.stopIndex, 0, route.stops.Length - 1)].stopName : "saved stop";
            detail.text = $"Resume {route.routeName} near {stop}?\nSaved {FormatAge(checkpoint.capturedAtUnixSeconds)} ago.";
        }
        Button resume = recoveryCanvas.transform.Find("Shade/Card/Resume")?.GetComponent<Button>();
        Button restart = recoveryCanvas.transform.Find("Shade/Card/Restart")?.GetComponent<Button>();
        resume?.onClick.RemoveAllListeners(); restart?.onClick.RemoveAllListeners();
        resume?.onClick.AddListener(() => { recoveryCanvas.gameObject.SetActive(false); MissionBriefingUI briefing = MissionBriefingUI.Instance; if (briefing != null && briefing.briefingPanel != null) briefing.briefingPanel.SetActive(false); mission.ResumeRoute(route, checkpoint); });
        restart?.onClick.AddListener(() => { ClearCheckpoint(); PlayerPrefs.Save(); recoveryCanvas.gameObject.SetActive(false); });
    }

    void BuildRecoveryPrompt()
    {
        GameObject root = new GameObject("MissionRecoveryCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster)); root.transform.SetParent(transform, false);
        recoveryCanvas = root.GetComponent<Canvas>(); recoveryCanvas.renderMode = RenderMode.ScreenSpaceOverlay; recoveryCanvas.sortingOrder = 180;
        CanvasScaler scaler = root.GetComponent<CanvasScaler>(); scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize; scaler.referenceResolution = new Vector2(1920f, 1080f);
        GameObject shade = Panel(root.transform, "Shade", Vector2.zero, Vector2.zero, new Color(0f, 0f, 0f, 0.82f)); RectTransform shadeRect = shade.GetComponent<RectTransform>(); shadeRect.anchorMin = Vector2.zero; shadeRect.anchorMax = Vector2.one; shadeRect.offsetMin = shadeRect.offsetMax = Vector2.zero;
        GameObject card = Panel(shade.transform, "Card", new Vector2(760f, 390f), Vector2.zero, UITheme.SurfaceContainer);
        MakeText(card.transform, "Title", "CONTINUE YOUR ROUTE?", 40f, new Vector2(0f, 125f), new Vector2(650f, 60f));
        MakeText(card.transform, "Detail", "", 29f, new Vector2(0f, 30f), new Vector2(650f, 110f));
        MakeButton(card.transform, "Resume", "RESUME", new Vector2(-175f, -125f), UITheme.Accent);
        MakeButton(card.transform, "Restart", "START OVER", new Vector2(175f, -125f), UITheme.SurfaceHigh);
    }

    static GameObject Panel(Transform parent, string name, Vector2 size, Vector2 position, Color color)
    {
        GameObject o = new GameObject(name, typeof(RectTransform), typeof(Image)); o.transform.SetParent(parent, false); RectTransform r = o.GetComponent<RectTransform>(); r.sizeDelta = size; r.anchoredPosition = position; o.GetComponent<Image>().color = color; return o;
    }

    static TextMeshProUGUI MakeText(Transform parent, string name, string value, float size, Vector2 position, Vector2 dimensions)
    {
        GameObject o = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI)); o.transform.SetParent(parent, false); RectTransform r = o.GetComponent<RectTransform>(); r.sizeDelta = dimensions; r.anchoredPosition = position;
        TextMeshProUGUI text = o.GetComponent<TextMeshProUGUI>(); text.text = value; text.fontSize = size; text.alignment = TextAlignmentOptions.Center; text.color = UITheme.TextPrimary; text.font = UITheme.GetFont(size >= 36f ? UITheme.FontWeight.Bold : UITheme.FontWeight.Medium); return text;
    }

    static Button MakeButton(Transform parent, string name, string label, Vector2 position, Color color)
    {
        GameObject o = Panel(parent, name, new Vector2(290f, 62f), position, color); Button button = o.AddComponent<Button>(); TextMeshProUGUI text = MakeText(o.transform, "Label", label, 28f, Vector2.zero, new Vector2(290f, 62f)); text.color = name == "Resume" ? UITheme.Background : UITheme.TextPrimary; return button;
    }

    static string FormatAge(long capturedAt)
    {
        long seconds = System.Math.Max(0, System.DateTimeOffset.UtcNow.ToUnixTimeSeconds() - capturedAt);
        if (seconds < 60) return "less than a minute";
        if (seconds < 3600) return (seconds / 60) + " min";
        return (seconds / 3600) + " hr";
    }

    void HandleLowMemory()
    {
        SaveRecoveryState("low_memory");
        if (MobilePerformanceManager.Instance != null) MobilePerformanceManager.Instance.EnterLowMemoryMode();
        StartCoroutine(ReleaseUnusedAssets());
    }

    IEnumerator ReleaseUnusedAssets()
    {
        yield return Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
}
