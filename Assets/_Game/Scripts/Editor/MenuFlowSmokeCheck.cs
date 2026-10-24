#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Batch-only regression for the menu-to-gameplay path: start in MainMenu, pick a
/// route, call <see cref="SceneLoader.LoadGame"/>, and prove the world phase takes
/// the card over and eventually releases it.
///
/// This exists because the traffic smoke opens GameScene directly, which enters
/// play mode with GameScene already active. That path ran the old
/// RuntimeInitializeOnLoadMethod(AfterSceneLoad) bootstrap and masked the real
/// bug: a menu transition is a scene load inside an already-running session, so
/// no bootstrap ran, WorldReadyLoadingScreen never existed and the loading card
/// stayed on screen for the whole drive.
/// </summary>
[InitializeOnLoad]
public static class MenuFlowSmokeCheck
{
    const string Flag = "RealBus.MenuFlowSmoke";
    const string RoutePathKey = "RealBus.MenuFlowSmoke.Route";
    const string RoutePath =
        "Assets/_Game/Routes/NBO_3rd_Parklands_Avenue_Aga_Kan_Hospital_Junction_-_Trufi_Stop_via_Stima_Plaza 1.asset";

    static double startedAt;
    static double loadRequestedAt = -1;
    static double worldPhaseAt = -1;
    static double deadlineAt = -1;
    static bool loadRequested;
    static bool finished;
    static string lastStatus;

    static MenuFlowSmokeCheck() { EditorApplication.update += Tick; }

    public static void Run()
    {
        if (!Application.isBatchMode)
        {
            Debug.LogError("MenuFlowSmokeCheck is a batch-only check; it exits Unity when finished.");
            return;
        }

        startedAt = 0;
        loadRequestedAt = worldPhaseAt = deadlineAt = -1;
        loadRequested = finished = false;
        lastStatus = null;
        Debug.Log("MENU FLOW: opening MainMenu and entering play mode.");
        SessionState.SetString(RoutePathKey, RoutePath);
        SessionState.SetBool(Flag, true);
        EditorSceneManager.OpenScene("Assets/_Game/Scenes/MainMenu.unity");
        EditorApplication.EnterPlaymode();
    }

    static void Tick()
    {
        if (!SessionState.GetBool(Flag, false)) return;
        if (finished) return;
        // EditorApplication.update also runs outside play mode; the flow only
        // exists once the menu scene is actually playing.
        if (!EditorApplication.isPlaying) return;

        if (startedAt <= 0)
        {
            startedAt = EditorApplication.timeSinceStartup;
            Debug.Log($"MENU FLOW: play mode is up in '{SceneManager.GetActiveScene().name}', driving the menu.");
        }
        double now = EditorApplication.timeSinceStartup;

        if (!loadRequested)
        {
            SceneLoader loader = SceneLoader.Instance;
            GameState state = GameState.Instance;
            if (loader == null)
            {
                SceneLoader[] found = Object.FindObjectsByType<SceneLoader>(
                    FindObjectsInactive.Include, FindObjectsSortMode.None);
                if (found.Length > 0)
                {
                    var names = new System.Text.StringBuilder();
                    foreach (SceneLoader candidate in found)
                        names.Append(candidate != null
                            ? $"{candidate.gameObject.name}(active={candidate.gameObject.activeInHierarchy}) "
                            : "null ");
                    Debug.LogWarning(
                        $"MENU FLOW: SceneLoader.Instance is null but {found.Length} component(s) exist: {names}");
                    loader = found[0];
                }
                else
                {
                    loader = SceneLoader.EnsureExists();
                }
            }

            if (loader == null || state == null)
            {
                if (now - startedAt > 60)
                    Fail($"menu never produced SceneLoader/GameState (loader={loader != null} state={state != null})");
                return;
            }

            BusRoute route = AssetDatabase.LoadAssetAtPath<BusRoute>(SessionState.GetString(RoutePathKey, RoutePath));
            if (route == null)
            {
                Fail($"route asset not found at '{SessionState.GetString(RoutePathKey, RoutePath)}'");
                return;
            }

            state.SelectRoute(route);
            Debug.Log($"MENU FLOW: route selected '{route.routeName}', requesting gameplay load.");
            loader.LoadGame();
            loadRequested = true;
            loadRequestedAt = now;
            deadlineAt = now + 240;
            return;
        }

        Scene active = SceneManager.GetActiveScene();
        bool inGameplay = active.IsValid() && active.name == "GameScene";

        if (inGameplay && worldPhaseAt < 0)
        {
            WorldReadyLoadingScreen world = WorldReadyLoadingScreen.Instance;
            if (world != null)
            {
                worldPhaseAt = now;
                Debug.Log(
                    $"MENU FLOW: WorldReadyLoadingScreen bootstrapped {now - loadRequestedAt:0.0}s after LoadGame(); " +
                    $"card visible={LoadingScreenUI.Instance != null && LoadingScreenUI.Instance.IsVisible}.");
            }
            else if (now - loadRequestedAt > 30)
            {
                Fail("GameScene activated but WorldReadyLoadingScreen was never created - the loading card cannot be released.");
                return;
            }
        }

        if (worldPhaseAt >= 0)
        {
            bool inProgress = LoadingScreenUI.GameplayLoadInProgress;
            bool cardVisible = LoadingScreenUI.Instance != null && LoadingScreenUI.Instance.IsVisible;
            string status = $"inProgress={inProgress} cardVisible={cardVisible} world={WorldReadyLoadingScreen.Instance != null}";
            if (status != lastStatus)
            {
                lastStatus = status;
                Debug.Log($"MENU FLOW: {status} at {now - loadRequestedAt:0.0}s.");
            }

            if (!inProgress && WorldReadyLoadingScreen.Instance == null)
            {
                bool passed = !cardVisible;
                Debug.Log(
                    $"MENU FLOW PASS: card released {now - loadRequestedAt:0.0}s after LoadGame(); " +
                    $"worldPhase={worldPhaseAt - loadRequestedAt:0.0}s scene={active.name} cardVisible={cardVisible} passed={passed}.");
                Finish(passed);
                return;
            }
        }

        if (now >= deadlineAt)
        {
            Fail(
                $"timed out after {now - loadRequestedAt:0}s; scene={active.name} " +
                $"world={WorldReadyLoadingScreen.Instance != null} " +
                $"inProgress={LoadingScreenUI.GameplayLoadInProgress} " +
                $"cardVisible={LoadingScreenUI.Instance != null && LoadingScreenUI.Instance.IsVisible}");
        }
    }

    static void Fail(string reason)
    {
        Debug.LogError($"MENU FLOW FAIL: {reason}");
        Finish(false);
    }

    static void Finish(bool passed)
    {
        finished = true;
        SessionState.SetBool(Flag, false);
        EditorApplication.Exit(passed ? 0 : 2);
    }
}
#endif
