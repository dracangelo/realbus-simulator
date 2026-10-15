using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows a full-screen loading overlay inside the gameplay scene while
/// the world is still warming up (map tiles downloading, OSM roads building,
/// bus spawning). The SceneLoader loading bar only covers the
/// Unity scene-load step; this covers the async asset-streaming phase that follows it.
///
/// Auto-bootstrapped via RuntimeInitializeOnLoadMethod so it
/// works even when the gameplay scene is played directly from the Editor.
/// </summary>
[DefaultExecutionOrder(-150)]
public class WorldReadyLoadingScreen : MonoBehaviour
{
    public static WorldReadyLoadingScreen Instance { get; private set; }

    [SerializeField] float holdAfterReadySeconds = 0.6f;

    // The overlay has to outlast the async world warm-up (tiles, road meshes,
    // near-ring buildings, depot placement). Hiding early is what made the game
    // look frozen after loading, so the ceiling is generous.
    [SerializeField] float timeoutSeconds = 60f;

    Canvas canvas;
    Image background, progressBar, spinner;
    TextMeshProUGUI statusText, cityLabel, tipText, pctText;
    float spinAngle;

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
        StartCoroutine(WaitForWorldReady());
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void Show()
    {
        if (canvas != null) canvas.gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (canvas != null) canvas.gameObject.SetActive(false);
    }

    public void SetStatus(string message, float progress01)
    {
        if (statusText != null) statusText.text = message;
        if (progressBar != null) progressBar.fillAmount = Mathf.Clamp01(progress01);
        if (pctText != null) pctText.text = Mathf.RoundToInt(progress01 * 100f) + "%";
    }

    IEnumerator WaitForWorldReady()
    {
        yield return null;
        yield return null;

        Show();

        var city = GameState.Instance != null ? GameState.Instance.selectedCity : null;
        if (cityLabel != null)
            cityLabel.text = city != null ? city.cityName.ToUpperInvariant() : "REALBUS";
        if (tipText != null)
            tipText.text = Tips[Random.Range(0, Tips.Length)];

        float elapsed = 0f;
        bool worldReady = false;

        while (elapsed < timeoutSeconds)
        {
            elapsed += Time.unscaledDeltaTime;

            spinAngle -= 220f * Time.unscaledDeltaTime;
            if (spinner != null)
                spinner.rectTransform.localEulerAngles = new Vector3(0f, 0f, spinAngle);

            var snapshot = WorldReadiness.Evaluate(WorldReadiness.Capture());
            SetStatus(snapshot.status, snapshot.progress);

            if (snapshot.ready)
            {
                worldReady = true;
                SetStatus("WORLD READY", 1f);
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

        yield return FadeOut();
        Hide();
        Destroy(gameObject);
    }

    IEnumerator FadeOut()
    {
        if (canvas == null) yield break;
        var group = canvas.GetComponent<CanvasGroup>();
        if (group == null) group = canvas.gameObject.AddComponent<CanvasGroup>();
        float t = 0f;
        while (t < 0.35f)
        {
            t += Time.unscaledDeltaTime;
            group.alpha = 1f - Mathf.Clamp01(t / 0.35f);
            yield return null;
        }
        group.alpha = 0f;
    }

    void Build()
    {
        var canvasGo = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(CanvasGroup));
        canvasGo.transform.SetParent(transform, false);
        canvas = canvasGo.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 490;

        var scaler = canvasGo.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;

        background = MakeImage("Background", canvasGo.transform, UITheme.Background);
        StretchFull(background.rectTransform);

        var topStrip = MakeImage("TopStrip", background.transform, UITheme.WithAlpha(UITheme.BusAmber, 0.22f));
        topStrip.rectTransform.anchorMin = new Vector2(0f, 1f);
        topStrip.rectTransform.anchorMax = new Vector2(1f, 1f);
        topStrip.rectTransform.pivot = new Vector2(0.5f, 1f);
        topStrip.rectTransform.sizeDelta = new Vector2(0f, 6f);

        cityLabel = MakeText("CityLabel", background.transform, "REALBUS", 80f,
            new Vector2(0f, 100f), new Vector2(1200f, 120f), UITheme.FontWeight.Bold);
        cityLabel.color = UITheme.TextPrimary;

        var subLabel = MakeText("SubLabel", background.transform, "PREPARING YOUR WORLD", 22f,
            new Vector2(0f, 42f), new Vector2(900f, 36f), UITheme.FontWeight.Medium);
        subLabel.color = UITheme.WithAlpha(UITheme.Accent, 0.85f);

        tipText = MakeText("Tip", background.transform, "", 24f,
            new Vector2(0f, -40f), new Vector2(1100f, 72f), UITheme.FontWeight.Regular);
        tipText.color = UITheme.TextSecondary;

        var track = MakeImage("ProgressTrack", background.transform, UITheme.SurfaceBright);
        track.rectTransform.anchoredPosition = new Vector2(0f, -140f);
        track.rectTransform.sizeDelta = new Vector2(860f, 10f);
        RuntimeUiShapes.Rounded(track);

        progressBar = MakeImage("ProgressFill", track.transform, UITheme.Accent);
        StretchFull(progressBar.rectTransform);
        progressBar.type = Image.Type.Filled;
        progressBar.fillMethod = Image.FillMethod.Horizontal;
        progressBar.fillOrigin = 0;
        progressBar.fillAmount = 0f;
        RuntimeUiShapes.Rounded(progressBar);

        pctText = MakeText("Percent", background.transform, "0%", 26f,
            new Vector2(0f, -172f), new Vector2(200f, 40f), UITheme.FontWeight.Medium);
        pctText.color = UITheme.TextSecondary;

        statusText = MakeText("Status", background.transform, "INITIALISING...", 20f,
            new Vector2(0f, -208f), new Vector2(900f, 32f), UITheme.FontWeight.Regular);
        statusText.color = UITheme.WithAlpha(UITheme.TextMuted, 0.8f);

        spinner = MakeImage("Spinner", background.transform, UITheme.WithAlpha(UITheme.Accent, 0.7f));
        spinner.rectTransform.anchoredPosition = new Vector2(0f, -290f);
        spinner.rectTransform.sizeDelta = new Vector2(48f, 48f);
        RuntimeUiShapes.Circle(spinner);

        var hub = MakeImage("Hub", spinner.transform, UITheme.Background);
        hub.rectTransform.anchorMin = hub.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        hub.rectTransform.sizeDelta = new Vector2(34f, 34f);
        RuntimeUiShapes.Circle(hub);
    }

    static Image MakeImage(string name, Transform parent, Color color)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(Image));
        go.transform.SetParent(parent, false);
        var img = go.GetComponent<Image>();
        img.color = color;
        return img;
    }

    static TextMeshProUGUI MakeText(string name, Transform parent, string value,
        float size, Vector2 position, Vector2 dimensions, UITheme.FontWeight weight)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI));
        go.transform.SetParent(parent, false);
        var rt = go.GetComponent<RectTransform>();
        rt.sizeDelta = dimensions;
        rt.anchoredPosition = position;
        var tmp = go.GetComponent<TextMeshProUGUI>();
        tmp.text = value;
        tmp.fontSize = size;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = UITheme.TextPrimary;
        tmp.font = UITheme.GetFont(weight);
        tmp.raycastTarget = false;
        return tmp;
    }

    static void StretchFull(RectTransform r)
    {
        r.anchorMin = Vector2.zero;
        r.anchorMax = Vector2.one;
        r.offsetMin = r.offsetMax = Vector2.zero;
    }
}
