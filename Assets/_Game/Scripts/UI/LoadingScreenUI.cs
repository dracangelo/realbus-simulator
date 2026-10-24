using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// The scene-load half of the loading experience, and the owner of the single card
/// the whole transition draws.
///
/// The bar is one continuous run from menu to cockpit: the scene load fills
/// <see cref="SceneLoadStart"/>..<see cref="SceneLoadEnd"/>, then
/// <see cref="WorldReadyLoadingScreen"/> continues from exactly that value while the
/// map, roads, buildings, traffic and cockpit are warmed. The card is deliberately
/// not hidden when the gameplay scene activates - hiding it there was what made the
/// bar reach 100% and then start again from zero while the world was still building.
///
/// The canvas persists between scenes, so it is dismissed from the sceneLoaded event
/// only for ordinary menu navigation, and by <see cref="EndGameplayLoad"/> when the
/// world phase is actually finished.
/// </summary>
public class LoadingScreenUI : MonoBehaviour
{
    /// <summary>Where the bar starts once the deliberate preparation beat is over.</summary>
    public const float SceneLoadStart = 0.04f;

    /// <summary>Where the async scene load hands the bar to the world warm-up.</summary>
    public const float SceneLoadEnd = 0.44f;

    static readonly string[] Tips =
    {
        "Brake early and smoothly to protect passenger comfort.",
        "Use the kneeling suspension when serving accessible stops.",
        "A steady throttle saves fuel and reduces engine heat.",
        "Watch the schedule icon as well as its colour.",
        "Press M to toggle the north-up minimap."
    };

    public static LoadingScreenUI Instance { get; private set; }

    /// <summary>Raised by the card at the dark point of its tip cycle, so the owner can swap the copy unnoticed.</summary>
    public event System.Action TipFaded;

    /// <summary>
    /// True from the moment a gameplay load starts until the world phase ends. The
    /// scene-loaded event must not dismiss the card while this is set, and a second
    /// load request is refused instead of stacking two operations.
    /// </summary>
    public static bool GameplayLoadInProgress { get; private set; }

    /// <summary>True while the card canvas is visible. Used to avoid resetting a card that is already showing a preparation step.</summary>
    public bool IsVisible => canvas != null && canvas.gameObject.activeSelf;

    /// <summary>
    /// Maps the world warm-up's 0..1 progress into the second half of the one bar, so
    /// the value shown on screen is continuous across the scene activation.
    /// </summary>
    public static float WorldProgressToScreen(float world01) =>
        Mathf.Lerp(SceneLoadEnd, 1f, Mathf.Clamp01(world01));

    Canvas canvas;
    CinematicLoadingCard card;
    float shownProgress;

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
        // A play session with domain reload disabled keeps statics from the previous
        // run. A brand-new loader object is by definition not mid-transition.
        GameplayLoadInProgress = false;
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ordinary menu navigation is done as soon as the scene is up. A gameplay
        // load is not: the scene activating only means the world can start building,
        // so the world phase keeps this same card and bar.
        if (!GameplayLoadInProgress) Hide();
    }

    /// <summary>
    /// Starts the gameplay transition on a fresh card. Sets the shared flag that
    /// prevents a second load and keeps the card alive across the scene swap.
    /// </summary>
    public void BeginGameplayLoad(string status)
    {
        GameplayLoadInProgress = true;
        Show();
        SetStatus(status);
        SetProgress(0f);
    }

    /// <summary>
    /// Hands the visible card to the world warm-up at exactly the value the scene
    /// load finished on. Does not reveal or reset anything: the player sees one bar
    /// that keeps filling, not two screens.
    /// </summary>
    public void BeginWorldPhase()
    {
        if (!IsVisible) Show();
        if (card == null) return;
        card.SetSubject();
        card.SetEyebrow("PREPARING YOUR WORLD");
        card.SetStatus("INITIALISING...");
        card.SetTip(Tips[Random.Range(0, Tips.Length)]);
        SetProgress(SceneLoadEnd);
    }

    /// <summary>
    /// Drives the bar through the second half. The value is clamped to be monotonic:
    /// a milestone that flickers back (a re-bind, a streaming flag) must never make
    /// the player's progress retreat.
    /// </summary>
    public void SetWorldProgress(float world01)
    {
        SetProgress(WorldProgressToScreen(world01));
    }

    /// <summary>Finishes the gameplay transition: the card is dismissed for good.</summary>
    public void EndGameplayLoad()
    {
        GameplayLoadInProgress = false;
        Hide();
    }

    /// <summary>
    /// Clears the shared gameplay-load flag even when the card is already gone, so a
    /// failed or late transition can never leave the next load blocked.
    /// </summary>
    public static void ReleaseGameplayLoad()
    {
        GameplayLoadInProgress = false;
        if (Instance != null) Instance.Hide();
    }

    public void Show()
    {
        if (canvas == null) return;
        shownProgress = 0f;
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
        if (card == null) return;
        // A full canvas rebuild per frame is wasted work; the card only needs a
        // value that actually moved (and never one that moves backwards).
        float next = Mathf.Max(shownProgress, Mathf.Clamp01(value));
        if (Mathf.Approximately(next, shownProgress)) return;
        shownProgress = next;
        card.SetProgress(next);
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

    public void SetTip(string message)
    {
        if (card != null && !string.IsNullOrWhiteSpace(message))
            card.SetTip(message);
    }

    public void Hide()
    {
        if (card != null) card.Dismiss(instant: true);
        if (canvas != null) canvas.gameObject.SetActive(false);
        shownProgress = 0f;
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
        // Match both axes so the card keeps its proportions on tall phone screens
        // and on wide tablets instead of hugging one edge.
        scaler.matchWidthOrHeight = 0.5f;

        card = CinematicLoadingCard.Create(canvasGo.transform);
        card.TipFadedOut += () => TipFaded?.Invoke();
    }
}
