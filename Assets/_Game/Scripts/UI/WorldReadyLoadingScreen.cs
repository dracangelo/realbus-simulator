using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows a full-screen title card inside the gameplay scene while the world is still
/// warming up (map tiles downloading, OSM roads building, bus spawning). The scene
/// loading bar only covers Unity's scene activation; this covers the async
/// asset-streaming phase that follows it, which is by far the longer wait.
///
/// The card itself is <see cref="CinematicLoadingCard"/>, shared with the scene-load
/// screen so the two waits are visually the same shot rather than two different
/// panels. This component owns only the wait: what to say, when the world is ready,
/// and the dismiss.
///
/// Auto-bootstrapped via RuntimeInitializeOnLoadMethod so it
/// works even when the gameplay scene is played directly from the Editor.
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

    Canvas canvas;
    CinematicLoadingCard card;
    // -1 so the first NextTip() lands on Tips[0] rather than skipping it: the card
    // swaps its copy at the dark point of the cycle, which is the first frame.
    int tipIndex = -1;

    static readonly string[] Tips =
    {
        "Map tiles are streaming from Mapbox — stay close to the start line!",
        "OSM road geometry is being laid out. The city loads chunk by chunk.",
        "Brake early and smoothly to protect passenger comfort.",
        "Watch the schedule icon — colours show punctuality at a glance.",
        "Press M at any time to toggle the minimap.",
        "The bus depot is placed near your route start. Drive out when ready.",
    };

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap()
    {
        if (FindAnyObjectByType<MissionManager>() == null) return;
        if (Instance != null) return;
        new GameObject("WorldReadyLoadingScreen").AddComponent<WorldReadyLoadingScreen>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Build();
    }

    void Start()
    {
        // Show in Start rather than after a couple of frames. Start runs before
        // Unity renders the first gameplay frame, so the scene-load card and the
        // world-warm-up card overlap instead of leaving a visible empty frame.
        Show();
        if (card != null)
        {
            card.SetSubject();
            card.SetEyebrow("PREPARING YOUR WORLD");
            NextTip();
            card.TipFadedOut += NextTip;
        }
        StartCoroutine(WaitForWorldReady());
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void Show()
    {
        if (canvas != null) canvas.gameObject.SetActive(true);
        // The scene-load card was already fully revealed; animating the world
        // card in again would read as a second screen rather than a hand-off.
        if (card != null) card.Reveal(instant: true);
    }

    /// <summary>Closes the card and hides the canvas once the bars have retracted.</summary>
    public void Hide()
    {
        if (card == null || !isActiveAndEnabled) { HideImmediate(); return; }
        card.Dismiss();
        StartCoroutine(DeactivateWhenClosed());
    }

    void HideImmediate()
    {
        if (card != null) card.Dismiss(instant: true);
        if (canvas != null) canvas.gameObject.SetActive(false);
    }

    IEnumerator DeactivateWhenClosed()
    {
        float waited = 0f;
        while (!card.IsDismissed && waited < 2f)
        {
            waited += Time.unscaledDeltaTime;
            yield return null;
        }
        if (canvas != null) canvas.gameObject.SetActive(false);
    }

    public void SetStatus(string message, float progress01)
    {
        if (card == null) return;
        card.SetStatus(message);
        card.SetProgress(progress01);
    }

    IEnumerator WaitForWorldReady()
    {
        float elapsed = 0f;
        bool worldReady = false;

        while (elapsed < timeoutSeconds)
        {
            elapsed += Time.unscaledDeltaTime;

            var snapshot = WorldReadiness.Evaluate(WorldReadiness.Capture());
            SetStatus(snapshot.status, snapshot.progress);

            if (snapshot.ready)
            {
                worldReady = true;
                SetStatus("WORLD READY", 1f);

                // Hold the finished card for a deliberate beat. A short minimum
                // duration also protects the no-streaming / editor direct-play
                // case, where readiness can become true on the first frame.
                if (elapsed < minimumVisibleSeconds)
                {
                    yield return null;
                    continue;
                }

                yield return new WaitForSecondsRealtime(holdAfterReadySeconds);
                break;
            }

            yield return null;
        }

        if (worldReady)
        {
            // Last chance to fix imported models that spawned after their own
            // ApplyFallback call - the depot, stop props, gas stations and the
            // fleet buses are all in place by now.
            int repainted = ImportedMaterialPalette.RepairRegisteredRoots();
            if (repainted > 0)
                Debug.Log($"WorldReadyLoadingScreen: repaired {repainted} untextured material slots on imported models.");
        }
        else
        {
            Debug.LogWarning(
                $"WorldReadyLoadingScreen: world not ready after {timeoutSeconds:0}s, still waiting on " +
                $"{WorldReadiness.DescribePending(WorldReadiness.Capture())}. Showing the scene anyway.");
        }

        if (card != null)
        {
            card.TipFadedOut -= NextTip;
            card.Dismiss();
        }

        float waited = 0f;
        while ((card == null || !card.IsDismissed) && waited < 2f)
        {
            waited += Time.unscaledDeltaTime;
            yield return null;
        }

        if (canvas != null) canvas.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void NextTip()
    {
        if (card == null || Tips.Length == 0) return;
        tipIndex = (tipIndex + 1) % Tips.Length;
        card.SetTip(Tips[tipIndex]);
    }

    void Build()
    {
        var canvasGo = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(CanvasGroup));
        canvasGo.transform.SetParent(transform, false);
        canvas = canvasGo.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        // Above the HUD and above the intro's letterbox, so the title card owns the
        // frame until the world is up.
        canvas.sortingOrder = 490;

        var scaler = canvasGo.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;

        card = CinematicLoadingCard.Create(canvasGo.transform);
        card.SetSubject();
        card.SetStatus("INITIALISING...");
        card.SetProgress(0f);
        canvas.gameObject.SetActive(false);
    }
}
