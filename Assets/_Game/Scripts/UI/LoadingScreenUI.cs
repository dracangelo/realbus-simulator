using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// The scene-load half of the loading experience. It draws the same
/// <see cref="CinematicLoadingCard"/> the world warm-up draws, so when the gameplay
/// scene activates and <see cref="WorldReadyLoadingScreen"/> takes over there is no
/// visible change: one title card, letterboxed, from menu to city.
///
/// The canvas persists between scenes, so it is dismissed from the sceneLoaded event
/// as a safety net even when the loading coroutine is interrupted while Unity
/// activates the destination scene.
/// </summary>
public class LoadingScreenUI : MonoBehaviour
{
    static readonly string[] Tips =
    {
        "Brake early and smoothly to protect passenger comfort.",
        "Use the kneeling suspension when serving accessible stops.",
        "A steady throttle saves fuel and reduces engine heat.",
        "Watch the schedule icon as well as its colour.",
        "Press M to toggle the north-up minimap."
    };

    public static LoadingScreenUI Instance { get; private set; }

    /// <summary>True while the card canvas is visible. Used to avoid resetting a card that is already showing a preparation step.</summary>
    public bool IsVisible => canvas != null && canvas.gameObject.activeSelf;

    Canvas canvas;
    CinematicLoadingCard card;

    public static LoadingScreenUI EnsureExists()
    {
        if (Instance != null) return Instance;
        LoadingScreenUI existing = FindAnyObjectByType<LoadingScreenUI>();
        return existing != null ? existing : new GameObject("LoadingScreen").AddComponent<LoadingScreenUI>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Build();
        Hide();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) => Hide();

    public void Show()
    {
        if (canvas == null) return;
        canvas.gameObject.SetActive(true);

        card.SetSubject();
        card.SetEyebrow("LOADING");
        card.SetTip(Tips[Random.Range(0, Tips.Length)]);
        card.SetStatus("PREPARING THE CITY");
        card.SetProgress(0f);
        // Instant: this screen is often only up for a fraction of a second, and a
        // reveal tween that never finishes reads as a flicker.
        card.Reveal(instant: true);
    }

    public void SetProgress(float value)
    {
        if (card != null) card.SetProgress(value);
    }

    /// <summary>
    /// Names the current preparation step while the gameplay scene is still
    /// activating. The card otherwise shows a generic city message, which makes
    /// a route load look stalled when the network is slow.
    /// </summary>
    public void SetStatus(string message)
    {
        if (card != null && !string.IsNullOrWhiteSpace(message))
            card.SetStatus(message);
    }

    public void Hide()
    {
        if (card != null) card.Dismiss(instant: true);
        if (canvas != null) canvas.gameObject.SetActive(false);
    }

    void Build()
    {
        var canvasGo = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler));
        canvasGo.transform.SetParent(transform, false);

        canvas = canvasGo.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 500;

        var scaler = canvasGo.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;

        card = CinematicLoadingCard.Create(canvasGo.transform);
    }
}
