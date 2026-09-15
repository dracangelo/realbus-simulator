using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class RouteGhostFrame
{
    public float time;
    public Vector3 position;
    public Quaternion rotation;
}

[System.Serializable]
public class RouteGhostReplay
{
    public int schemaVersion = 1;
    public string routeId;
    public string cityCode;
    public float score;
    public float durationSeconds;
    public List<RouteGhostFrame> frames = new List<RouteGhostFrame>();
}

public class RouteGhostRecorder : MonoBehaviour
{
    public static RouteGhostRecorder Instance { get; private set; }
    public float sampleIntervalSeconds = 0.25f;
    public int maximumSamples = 12000;
    MissionManager mission;
    BusController bus;
    RouteGhostReplay recording;
    RouteGhostReplay playback;
    GameObject ghost;
    Material ghostMaterial;
    float nextSample;
    int playbackIndex;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static RouteGhostRecorder EnsureExists()
    {
        if (Instance != null) return Instance;
        RouteGhostRecorder existing = FindFirstObjectByType<RouteGhostRecorder>();
        return existing != null ? existing : new GameObject("RouteGhostRecorder").AddComponent<RouteGhostRecorder>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start() { StartCoroutine(BindWhenReady()); }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { ResetSession(); StartCoroutine(BindWhenReady()); }
    void OnDestroy() { if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded; Unbind(); if (ghostMaterial != null) Destroy(ghostMaterial); }

    IEnumerator BindWhenReady()
    {
        for (int frame = 0; frame < 180; frame++)
        {
            MissionManager candidate = MissionManager.Instance; BusController candidateBus = FindFirstObjectByType<BusController>();
            if (candidate != null && candidateBus != null) { Bind(candidate, candidateBus); yield break; }
            yield return null;
        }
    }

    void Bind(MissionManager nextMission, BusController nextBus)
    {
        Unbind(); mission = nextMission; bus = nextBus; mission.OnMissionCompleted += CompleteRecording;
    }

    void Unbind() { if (mission != null) mission.OnMissionCompleted -= CompleteRecording; mission = null; bus = null; }

    void Update()
    {
        if (mission == null || bus == null) return;
        if (mission.routeActive && recording == null) BeginRecording();
        if (!mission.routeActive && recording != null) { recording = null; HideGhost(); return; }
        if (!mission.routeActive || recording == null) return;
        if (Time.unscaledTime >= nextSample && recording.frames.Count < maximumSamples)
        {
            nextSample = Time.unscaledTime + Mathf.Max(0.1f, sampleIntervalSeconds);
            recording.frames.Add(new RouteGhostFrame { time = mission.ElapsedSeconds, position = bus.transform.position, rotation = bus.transform.rotation });
        }
        UpdateGhost(mission.ElapsedSeconds);
    }

    void BeginRecording()
    {
        CityDefinition city = GameState.Instance != null ? GameState.Instance.selectedCity : CityManager.Instance?.activeCity;
        string cityCode = city != null ? city.cityCode : "CITY";
        string routeId = mission.currentRoute.GetProgressionId(cityCode);
        recording = new RouteGhostReplay { routeId = routeId, cityCode = cityCode }; nextSample = 0f;
        playback = TryLoadBest(routeId); playbackIndex = 0;
        if (playback != null && playback.frames != null && playback.frames.Count > 1) EnsureGhost(); else HideGhost();
    }

    void CompleteRecording(MissionResult result)
    {
        if (recording == null || result == null) return;
        recording.score = result.totalScore; recording.durationSeconds = result.totalTimeMinutes * 60f;
        float previousScore = PlayerPrefs.GetFloat(ScoreKey(recording.routeId), -1f);
        if (IsBetterReplay(recording.score, previousScore) && recording.frames.Count > 1)
        {
            try
            {
                Directory.CreateDirectory(ReplayDirectory()); File.WriteAllText(ReplayPath(recording.routeId), JsonUtility.ToJson(recording));
                PlayerPrefs.SetFloat(ScoreKey(recording.routeId), recording.score); PlayerPrefs.Save();
            }
            catch (System.Exception ex) { Debug.LogWarning("Route ghost could not be saved: " + ex.Message); }
        }
        recording = null; HideGhost();
    }

    public RouteGhostReplay TryLoadBest(string routeId)
    {
        try
        {
            string path = ReplayPath(routeId); if (!File.Exists(path)) return null;
            RouteGhostReplay replay = JsonUtility.FromJson<RouteGhostReplay>(File.ReadAllText(path));
            return replay != null && replay.schemaVersion == 1 && replay.routeId == routeId && replay.frames != null && replay.frames.Count > 1 ? replay : null;
        }
        catch (System.Exception) { return null; }
    }

    void UpdateGhost(float elapsed)
    {
        if (ghost == null || playback == null || playback.frames == null || playback.frames.Count < 2) return;
        while (playbackIndex < playback.frames.Count - 2 && playback.frames[playbackIndex + 1].time <= elapsed) playbackIndex++;
        RouteGhostFrame a = playback.frames[playbackIndex], b = playback.frames[Mathf.Min(playbackIndex + 1, playback.frames.Count - 1)];
        float t = Mathf.InverseLerp(a.time, b.time, elapsed); ghost.transform.SetPositionAndRotation(Vector3.Lerp(a.position, b.position, t), Quaternion.Slerp(a.rotation, b.rotation, t));
        ghost.SetActive(elapsed <= playback.frames[playback.frames.Count - 1].time + 1f);
    }

    void EnsureGhost()
    {
        if (ghost == null)
        {
            ghost = GameObject.CreatePrimitive(PrimitiveType.Cube); ghost.name = "Best Route Ghost"; ghost.transform.localScale = new Vector3(2.45f, 3.1f, 10.5f); Destroy(ghost.GetComponent<Collider>());
            Shader shader = Shader.Find("Sprites/Default") ?? Shader.Find("Universal Render Pipeline/Unlit") ?? Shader.Find("Unlit/Color");
            if (shader != null) { ghostMaterial = new Material(shader); ghostMaterial.color = new Color(0.1f, 0.9f, 1f, 0.28f); ghostMaterial.renderQueue = 3000; ghost.GetComponent<Renderer>().material = ghostMaterial; }
        }
        ghost.SetActive(true);
    }

    void HideGhost() { if (ghost != null) ghost.SetActive(false); playback = null; playbackIndex = 0; }
    void ResetSession() { recording = null; playback = null; playbackIndex = 0; HideGhost(); Unbind(); }
    static string ReplayDirectory() { return Path.Combine(Application.persistentDataPath, "RouteGhosts"); }
    static string ReplayPath(string routeId) { return Path.Combine(ReplayDirectory(), DailyChallengeManager.StableSeed(routeId).ToString("X8") + ".json"); }
    static string ScoreKey(string routeId) { return "RealBus.RouteGhost.BestScore." + routeId; }
    public static bool IsBetterReplay(float candidateScore, float previousScore) { return candidateScore > previousScore + 0.001f; }
}
