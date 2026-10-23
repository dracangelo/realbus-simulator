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
    /// buildings are still streaming in; showing the loading card before the
    /// swap lets <see cref="WorldReadyLoadingScreen"/> take over without a
    /// visible half-built frame.
    /// </summary>
    public void LoadGame()
    {
        if (isLoading) return;
        StartCoroutine(PrepareAndLoadGame());
    }

    System.Collections.IEnumerator PrepareAndLoadGame()
    {
        isLoading = true;

        LoadingScreenUI screen = LoadingScreenUI.EnsureExists();
        screen.Show();
        BusRoute route = GameState.Instance != null ? GameState.Instance.selectedRoute : null;
        screen.SetStatus(route != null
            ? $"PREPARING {RouteNaming.DisplayCode(route)}..."
            : "PREPARING YOUR ROUTE...");

        // Do the wait on unscaled time: pause/UI timescale should never make the
        // loading card disappear before the scene is actually ready.
        float delay = Mathf.Max(0f, gameplayPreparationDelaySeconds);
        float elapsed = 0f;
        while (elapsed < delay)
        {
            elapsed += Time.unscaledDeltaTime;
            screen.SetProgress(delay > 0f ? Mathf.Clamp01(elapsed / delay) * 0.12f : 0.12f);
            yield return null;
        }
        screen.SetProgress(0.12f);

        int fallbackBuildIndex = sceneCatalog != null ? sceneCatalog.gameplayBuildIndex : 4;
        AsyncOperation operation = null;
        if (Application.CanStreamedLevelBeLoaded("GameScene"))
            operation = SceneManager.LoadSceneAsync("GameScene");
        else if (fallbackBuildIndex >= 0)
            operation = SceneManager.LoadSceneAsync(fallbackBuildIndex);

        if (operation == null)
        {
            Debug.LogError("SceneLoader: Could not load GameScene by name or build index.");
            screen.Hide();
            isLoading = false;
            yield break;
        }

        yield return LoadAsync(operation);
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
            // Start above the preparation card's 12% so the bar never jumps
            // backwards between "preparing" and the actual scene load.
            screen.SetProgress(Mathf.Lerp(0.12f, 1f, operation.progress / 0.9f));
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
