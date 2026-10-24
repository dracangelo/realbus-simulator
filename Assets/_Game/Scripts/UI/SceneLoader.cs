using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    [Header("Scene Catalog")]
    public SceneCatalog sceneCatalog;

    [Header("Gameplay Transition")]
    [Tooltip("A short deliberate beat after the route is confirmed. It gives the loading card time to appear before the scene swap, so the player never sees a half-built street flash past.")]
    [SerializeField, Min(0f)] float gameplayPreparationDelaySeconds = 0.8f;

    [Tooltip("Ceiling for the async scene load. A local scene should never take this long; if it does, the transition is aborted instead of leaving the player on a frozen card.")]
    [SerializeField, Min(10f)] float sceneLoadTimeoutSeconds = 90f;

    bool isLoading;

    public static SceneLoader EnsureExists()
    {
        if (Instance != null) return Instance;
        SceneLoader existing = FindAnyObjectByType<SceneLoader>();
        return existing != null ? existing : new GameObject("SceneLoader").AddComponent<SceneLoader>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        ConfigureLandscapeOrientation();
        if (transform.parent != null)
            transform.SetParent(null, true);
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // A gameplay load is not finished when the scene activates: the world phase
        // still owns the same card and bar. CompleteGameplayLoad releases both.
        if (LoadingScreenUI.GameplayLoadInProgress) return;
        isLoading = false;
        if (LoadingScreenUI.Instance != null) LoadingScreenUI.Instance.Hide();
    }

    void ConfigureLandscapeOrientation()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.orientation = ScreenOrientation.AutoRotation;
#endif
    }

    public void LoadMainMenu() => LoadScene("MainMenu", sceneCatalog != null ? sceneCatalog.mainMenuBuildIndex : 0);
    public void LoadCountrySelect() => LoadScene("CountrySelect", sceneCatalog != null ? sceneCatalog.countrySelectBuildIndex : 1);
    public void LoadCitySelect() => LoadScene("CitySelect", sceneCatalog != null ? sceneCatalog.citySelectBuildIndex : 2);
    public void LoadRouteSelect() => LoadScene("RouteSelect", sceneCatalog != null ? sceneCatalog.routeSelectBuildIndex : 3);

    /// <summary>
    /// Route selection goes through a deliberate preparation beat. Unity can
    /// activate the gameplay scene in a few frames, but the map, roads and
    /// buildings are still streaming in; the bar therefore only fills to
    /// <see cref="LoadingScreenUI.SceneLoadEnd"/> here and
    /// <see cref="WorldReadyLoadingScreen"/> continues from that value while it
    /// waits for the world. The card is never dismissed at scene activation.
    /// </summary>
    public void LoadGame()
    {
        if (isLoading || LoadingScreenUI.GameplayLoadInProgress) return;
        StartCoroutine(PrepareAndLoadGame());
    }

    System.Collections.IEnumerator PrepareAndLoadGame()
    {
        isLoading = true;

        LoadingScreenUI screen = LoadingScreenUI.EnsureExists();
        BusRoute route = GameState.Instance != null ? GameState.Instance.selectedRoute : null;
        screen.BeginGameplayLoad(route != null
            ? $"PREPARING {RouteNaming.DisplayCode(route)}..."
            : "PREPARING YOUR ROUTE...");

        // Do the wait on unscaled time: pause/UI timescale should never make the
        // loading card disappear before the scene is actually ready.
        float delay = Mathf.Max(0f, gameplayPreparationDelaySeconds);
        float elapsed = 0f;
        while (elapsed < delay)
        {
            elapsed += Time.unscaledDeltaTime;
            screen.SetProgress(delay > 0f
                ? Mathf.Clamp01(elapsed / delay) * LoadingScreenUI.SceneLoadStart
                : LoadingScreenUI.SceneLoadStart);
            yield return null;
        }
        screen.SetProgress(LoadingScreenUI.SceneLoadStart);

        int fallbackBuildIndex = sceneCatalog != null ? sceneCatalog.gameplayBuildIndex : 4;
        AsyncOperation operation = null;
        if (Application.CanStreamedLevelBeLoaded("GameScene"))
            operation = SceneManager.LoadSceneAsync("GameScene");
        else if (fallbackBuildIndex >= 0)
            operation = SceneManager.LoadSceneAsync(fallbackBuildIndex);

        if (operation == null)
        {
            Debug.LogError("SceneLoader: Could not load GameScene by name or build index.");
            CancelGameplayLoad();
            yield break;
        }

        screen.SetStatus(route != null
            ? $"LOADING {RouteNaming.DisplayCode(route)}..."
            : "LOADING THE CITY...");
        operation.allowSceneActivation = false;
        float loadDeadline = Time.unscaledTime + Mathf.Max(10f, sceneLoadTimeoutSeconds);
        while (operation.progress < 0.9f)
        {
            // A local scene load should finish in a few frames. If it does not, the
            // transition is aborted rather than leaving the player pinned to a card
            // whose bar can never advance; the menu is still behind it and usable.
            if (Time.unscaledTime >= loadDeadline)
            {
                Debug.LogError(
                    $"SceneLoader: GameScene did not finish loading within {sceneLoadTimeoutSeconds:0}s " +
                    $"(progress {operation.progress:P0}). Aborting the transition.");
                CancelGameplayLoad();
                yield break;
            }

            screen.SetProgress(Mathf.Lerp(LoadingScreenUI.SceneLoadStart,
                LoadingScreenUI.SceneLoadEnd, operation.progress / 0.9f));
            yield return null;
        }
        screen.SetProgress(LoadingScreenUI.SceneLoadEnd);

        // Activation is the hand-off, not the finish: the card stays up and
        // WorldReadyLoadingScreen takes the same bar from here.
        operation.allowSceneActivation = true;
        loadDeadline = Time.unscaledTime + Mathf.Max(10f, sceneLoadTimeoutSeconds);
        while (!operation.isDone)
        {
            if (Time.unscaledTime >= loadDeadline)
            {
                // Activation itself is stalled (a scene that never finishes
                // activating). Same rule: release the card instead of freezing on it.
                Debug.LogError(
                    $"SceneLoader: GameScene activation did not complete within {sceneLoadTimeoutSeconds:0}s. " +
                    "Aborting the transition.");
                CancelGameplayLoad();
                yield break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Called by <see cref="WorldReadyLoadingScreen"/> once the world is playable (or
    /// its ceiling expired). Only here does the gameplay transition stop accepting
    /// input and release the card.
    /// </summary>
    public void CompleteGameplayLoad()
    {
        isLoading = false;
        LoadingScreenUI.ReleaseGameplayLoad();
    }

    /// <summary>Aborts a transition that could not even start, without stranding the card.</summary>
    public void CancelGameplayLoad()
    {
        isLoading = false;
        LoadingScreenUI.ReleaseGameplayLoad();
    }

    public void LoadCitySelectChecked()
    {
        if (HasCountrySelection())
        {
            LoadCitySelect();
            return;
        }

        Debug.LogWarning("SceneLoader: Cannot open CitySelect before selecting a country.");
        LoadCountrySelect();
    }

    public void LoadRouteSelectChecked()
    {
        if (HasCitySelection())
        {
            LoadRouteSelect();
            return;
        }

        Debug.LogWarning("SceneLoader: Cannot open RouteSelect before selecting a city.");
        if (HasCountrySelection()) LoadCitySelect();
        else LoadCountrySelect();
    }

    public void LoadGameChecked()
    {
        if (HasRouteSelection())
        {
            LoadGame();
            return;
        }

        Debug.LogWarning("SceneLoader: Cannot open GameScene before selecting a route.");
        if (HasCitySelection()) LoadRouteSelect();
        else if (HasCountrySelection()) LoadCitySelect();
        else LoadCountrySelect();
    }

    public void LoadWithDelay(string sceneName, float delay)
    {
        StartCoroutine(DelayedLoad(sceneName, delay));
    }

    System.Collections.IEnumerator DelayedLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return LoadAsync(SceneManager.LoadSceneAsync(sceneName));
    }

    void LoadByIndex(int buildIndex)
    {
        if (buildIndex < 0)
        {
            Debug.LogError("SceneLoader: Invalid build index.");
            return;
        }
        if (!isLoading) StartCoroutine(LoadAsync(SceneManager.LoadSceneAsync(buildIndex)));
    }

    void LoadScene(string sceneName, int fallbackBuildIndex)
    {
        if (isLoading) return;
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            StartCoroutine(LoadAsync(SceneManager.LoadSceneAsync(sceneName)));
            return;
        }

        Debug.LogWarning($"SceneLoader: Scene '{sceneName}' was not found by name; trying build index {fallbackBuildIndex}.");
        LoadByIndex(fallbackBuildIndex);
    }

    System.Collections.IEnumerator LoadAsync(AsyncOperation operation)
    {
        if (operation == null) yield break;
        isLoading = true;
        LoadingScreenUI screen = LoadingScreenUI.EnsureExists();
        if (!screen.IsVisible) screen.Show();
        BusRoute route = GameState.Instance != null ? GameState.Instance.selectedRoute : null;
        screen.SetStatus(route != null
            ? $"LOADING {RouteNaming.DisplayCode(route)}..."
            : "LOADING THE CITY...");
        operation.allowSceneActivation = false;
        while (operation.progress < 0.9f)
        {
            // Menu navigation is the whole wait, so it may use the whole bar. It
            // starts above zero so it never jumps back from the reveal.
            screen.SetProgress(Mathf.Lerp(LoadingScreenUI.SceneLoadStart, 1f,
                operation.progress / 0.9f));
            yield return null;
        }
        screen.SetProgress(1f);
        yield return new WaitForSecondsRealtime(0.2f);
        operation.allowSceneActivation = true;
        while (!operation.isDone) yield return null;
        screen.Hide();
        isLoading = false;
    }

    bool HasCountrySelection()
    {
        return GameState.Instance?.selectedCountry != null;
    }

    bool HasCitySelection()
    {
        return HasCountrySelection() && GameState.Instance?.selectedCity != null;
    }

    bool HasRouteSelection()
    {
        return HasCitySelection() && GameState.Instance?.selectedRoute != null;
    }
}
