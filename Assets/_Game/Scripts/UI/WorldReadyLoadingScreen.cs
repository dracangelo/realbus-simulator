using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Drives the second half of the loading bar inside the gameplay scene: the world is
/// still warming up (map tiles downloading, OSM roads building, traffic and props
/// streaming in, the bus being configured) long after the Unity scene activates.
/// This owns only the wait. The visible card is the same
/// <see cref="CinematicLoadingCard"/> the scene load used, held by
/// <see cref="LoadingScreenUI"/>, so the player sees one continuous bar from menu to
/// cockpit instead of a 100% scene load followed by a second screen starting at 10%.
///
/// It also owns the playable gate: drive input is locked and
/// <see cref="WaitUntilPlayable"/> holds the mission countdown until the world is
/// actually ready (or the ceiling expires), so no countdown second and no mission
/// clock is spent behind the card.
///
/// Auto-bootstrapped via RuntimeInitializeOnLoadMethod so it works even when the
/// gameplay scene is played directly from the Editor.
/// </summary>
[DefaultExecutionOrder(-150)]
public class WorldReadyLoadingScreen : MonoBehaviour
{
    public static WorldReadyLoadingScreen Instance { get; private set; }

    [SerializeField] float holdAfterReadySeconds = 0.6f;

    // Even a direct scene play or an extremely fast warm-up should show the card
    // as a finished title, never as a one-frame flash between the scene loader
    // disappearing and the cockpit appearing.
    [SerializeField] float minimumVisibleSeconds = 0.9f;

    // The overlay has to outlast the async world warm-up (tiles, road meshes,
    // near-ring buildings, depot placement). Hiding early is what made the game
    // look frozen after loading, so the ceiling is generous.
    [SerializeField] float timeoutSeconds = 60f;

    LoadingScreenUI screen;
    BusController lockedBus;
    float shownAt;
    int tipIndex = -1;
    string lastStatus;

    /// <summary>
    /// True once the world phase is over, whether the world reached readiness or the
    /// ceiling expired. Anything waiting to hand control to the player watches this.
    /// </summary>
    public bool Playable { get; private set; }

    static readonly string[] Tips =
    {
        "Map tiles are streaming from Mapbox — stay close to the start line!",
        "OSM road geometry is being laid out. The city loads chunk by chunk.",
        "Brake early and smoothly to protect passenger comfort.",
        "Watch the schedule icon — colours show punctuality at a glance.",
        "Press M at any time to toggle the minimap.",
        "The bus depot is placed near your route start. Drive out when ready.",
    };

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void RegisterSceneBootstrap()
    {
        // The gameplay scene is normally reached from the menu, which is a scene
        // load inside an already-running play session. AfterSceneLoad only fires
        // once, at play-mode entry, so the old bootstrap never ran for that path
        // and the menu's loading card was never handed to this component. The
        // card then stayed up for the whole session. Register a hook that runs on
        // every scene load instead, including a direct GameScene play.
        SceneManager.sceneLoaded -= BootstrapOnSceneLoaded;
        SceneManager.sceneLoaded += BootstrapOnSceneLoaded;
    }

    static void BootstrapOnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Instance != null) return;
        if (FindAnyObjectByType<MissionManager>() == null) return;
        new GameObject("WorldReadyLoadingScreen").AddComponent<WorldReadyLoadingScreen>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
        // Never leave the bus locked if this component is torn down mid-wait
        // (a scene reload, a failed load, exiting play mode).
        ReleaseInput();
        // Same for the shared transition flag: a teardown mid-wait must not block
        // the next gameplay load.
        if (LoadingScreenUI.GameplayLoadInProgress) LoadingScreenUI.ReleaseGameplayLoad();
    }

    void Start()
    {
        shownAt = Time.unscaledTime;
        // Reuse the scene-load card if there is one; a direct GameScene play has no
        // session, so this creates (and reveals) the same card.
        screen = LoadingScreenUI.EnsureExists();
        screen.BeginWorldPhase();
        if (screen != null) screen.TipFaded += NextTip;
        NextTip();
        StartCoroutine(WaitForWorldReady());
    }

    /// <summary>
    /// Coroutine callers can hold gameplay until the overlay has handed the world
    /// over. Returns immediately when no overlay exists (tests, headless probes) and
    /// after the overlay's own ceiling, so it can never wedge a caller forever.
    /// </summary>
    public static IEnumerator WaitUntilPlayable()
    {
        WorldReadyLoadingScreen gate = Instance;
        if (gate == null) yield break;

        float deadline = Time.realtimeSinceStartup + gate.timeoutSeconds + 10f;
        while (gate != null && !gate.Playable && Time.realtimeSinceStartup < deadline)
            yield return null;
    }

    IEnumerator WaitForWorldReady()
    {
        float deadline = Time.unscaledTime + timeoutSeconds;
        bool worldReady = false;

        while (Time.unscaledTime < deadline)
        {
            LockInput();

            WorldReadiness.Snapshot snapshot = WorldReadiness.Evaluate(WorldReadiness.Capture());
            screen.SetStatus(snapshot.status);
            // One line per ladder step: when a load is slow, the log names the
            // milestone it is sitting on instead of leaving a silent gap.
            if (snapshot.status != lastStatus)
            {
                lastStatus = snapshot.status;
                Debug.Log($"WorldReadyLoadingScreen: {snapshot.status} at {Time.unscaledTime - shownAt:0.0}s.");
            }
            // The card clamps to a monotonic value, so a milestone that flickers
            // back (a re-bind, a streaming flag) cannot make the bar retreat.
            screen.SetWorldProgress(snapshot.progress);

            if (snapshot.ready)
            {
                worldReady = true;
                break;
            }

            yield return null;
        }

        if (worldReady)
        {
            // 100% is only ever written here, after every milestone is true.
            screen.SetStatus("WORLD READY");
            screen.SetWorldProgress(1f);
            Debug.Log($"WorldReadyLoadingScreen: world ready in {Time.unscaledTime - shownAt:0.0}s.");

            float visibleFor = Time.unscaledTime - shownAt;
            float hold = Mathf.Max(0f, minimumVisibleSeconds - visibleFor) + holdAfterReadySeconds;
            yield return new WaitForSecondsRealtime(hold);

            // Last chance to fix imported models that spawned after their own
            // ApplyFallback call - the depot, stop props, gas stations and the
            // fleet buses are all in place by now.
            int repainted = ImportedMaterialPalette.RepairRegisteredRoots();
            if (repainted > 0)
                Debug.Log($"WorldReadyLoadingScreen: repaired {repainted} untextured material slots on imported models.");
        }
        else
        {
            // The ceiling is a safety valve, not a success: the bar keeps the last
            // honest value and the card says what is still streaming, so the player
            // is never told 100% while work is outstanding.
            Debug.LogWarning(
                $"WorldReadyLoadingScreen: world not ready after {timeoutSeconds:0}s, still waiting on " +
                $"{WorldReadiness.DescribePending(WorldReadiness.Capture())}. Showing the scene anyway.");
            screen.SetStatus("STILL STREAMING — CONTINUING IN THE CITY");
            yield return new WaitForSecondsRealtime(holdAfterReadySeconds);
        }

        // The preloaded catalogs were only held so the scene systems could read them
        // from Resources. By now every one of them is either referenced by a live
        // system or no longer needed, so let the loading-only copies go while the
        // card still covers the world: unloading after the card is gone would show
        // up as a hitch on the first frame of gameplay.
        if (WorldPreloadCoordinator.Instance != null)
            WorldPreloadCoordinator.Instance.ReleasePreloadedReferences();
        yield return Resources.UnloadUnusedAssets();

        Playable = true;
        ReleaseInput();
        Finish();
    }

    void Finish()
    {
        if (screen != null) screen.TipFaded -= NextTip;

        // One owner for the transition: SceneLoader closes the session (which
        // releases the card) when it exists; a direct scene play closes it here.
        if (SceneLoader.Instance != null)
            SceneLoader.Instance.CompleteGameplayLoad();
        else
            LoadingScreenUI.ReleaseGameplayLoad();

        Destroy(gameObject);
    }

    /// <summary>Holds every drive input at zero for whichever bus is current.</summary>
    void LockInput()
    {
        BusController bus = MissionManager.Instance != null ? MissionManager.Instance.busController : null;
        if (bus == null) bus = FindAnyObjectByType<BusController>();
        if (bus == null) return;

        if (lockedBus != null && lockedBus != bus)
            lockedBus.inputLocked = false;
        lockedBus = bus;
        if (!bus.inputLocked) bus.inputLocked = true;
    }

    void ReleaseInput()
    {
        if (lockedBus != null) lockedBus.inputLocked = false;
        lockedBus = null;
        // A SceneBootstrap prefab swap can leave a second body behind; unlock every
        // one of them rather than only the one this component happened to track.
        foreach (BusController bus in FindObjectsByType<BusController>(FindObjectsInactive.Exclude))
            if (bus != null) bus.inputLocked = false;
    }

    void NextTip()
    {
        if (screen == null || Tips.Length == 0) return;
        tipIndex = (tipIndex + 1) % Tips.Length;
        screen.SetTip(Tips[tipIndex]);
    }
}
