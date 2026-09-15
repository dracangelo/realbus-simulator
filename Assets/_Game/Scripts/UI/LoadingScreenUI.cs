using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    Canvas canvas;
    TextMeshProUGUI cityText, tipText, progressText;
    Image progress;

    public static LoadingScreenUI EnsureExists()
    {
        if (Instance != null) return Instance;
        LoadingScreenUI existing = FindFirstObjectByType<LoadingScreenUI>();
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

    // The loading canvas persists between scenes. Always dismiss it from the
    // sceneLoaded event as a safety net, even if the loading coroutine is
    // interrupted while Unity activates the destination scene.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) => Hide();

    public void Show()
    {
        CityDefinition city = GameState.Instance != null ? GameState.Instance.selectedCity : null;
        cityText.text = city != null ? city.cityName.ToUpperInvariant() : "REALBUS";
        tipText.text = "DRIVING TIP\n" + Tips[Random.Range(0, Tips.Length)];
        SetProgress(0f); canvas.gameObject.SetActive(true);
    }

    public void SetProgress(float value)
    {
        value = Mathf.Clamp01(value); progress.fillAmount = value; progressText.text = Mathf.RoundToInt(value * 100f) + "%";
    }

    public void Hide() { if (canvas != null) canvas.gameObject.SetActive(false); }

    void Build()
    {
        GameObject c = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler)); c.transform.SetParent(transform, false);
        canvas = c.GetComponent<Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceOverlay; canvas.sortingOrder = 500;
        CanvasScaler scaler = c.GetComponent<CanvasScaler>(); scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize; scaler.referenceResolution = new Vector2(1920f, 1080f);
        Image background = CreateImage("Background", c.transform, UITheme.Background); Stretch(background.rectTransform);
        cityText = CreateText("City", background.transform, "REALBUS", 72f, new Vector2(0f, 110f), new Vector2(1100f, 110f));
        tipText = CreateText("Tip", background.transform, "", 30f, new Vector2(0f, -20f), new Vector2(1100f, 150f));
        Image track = CreateImage("ProgressTrack", background.transform, UITheme.SurfaceBright); track.rectTransform.sizeDelta = new Vector2(820f, 16f); track.rectTransform.anchoredPosition = new Vector2(0f, -155f);
        progress = CreateImage("Progress", track.transform, UITheme.Accent); Stretch(progress.rectTransform); progress.type = Image.Type.Filled; progress.fillMethod = Image.FillMethod.Horizontal; progress.fillOrigin = 0;
        progressText = CreateText("ProgressText", background.transform, "0%", 28f, new Vector2(0f, -205f), new Vector2(300f, 50f));
    }

    static Image CreateImage(string name, Transform parent, Color color)
    {
        GameObject o = new GameObject(name, typeof(RectTransform), typeof(Image)); o.transform.SetParent(parent, false); Image i = o.GetComponent<Image>(); i.color = color; return i;
    }

    static TextMeshProUGUI CreateText(string name, Transform parent, string value, float size, Vector2 position, Vector2 dimensions)
    {
        GameObject o = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI)); o.transform.SetParent(parent, false);
        RectTransform r = o.GetComponent<RectTransform>(); r.sizeDelta = dimensions; r.anchoredPosition = position;
        TextMeshProUGUI t = o.GetComponent<TextMeshProUGUI>(); t.text = value; t.fontSize = size; t.alignment = TextAlignmentOptions.Center; t.color = UITheme.TextPrimary; t.font = UITheme.GetFont(size >= 40f ? UITheme.FontWeight.Bold : UITheme.FontWeight.Medium); return t;
    }

    static void Stretch(RectTransform r) { r.anchorMin = Vector2.zero; r.anchorMax = Vector2.one; r.offsetMin = r.offsetMax = Vector2.zero; }
}
