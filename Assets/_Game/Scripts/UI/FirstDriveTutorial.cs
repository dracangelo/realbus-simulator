using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstDriveTutorial : MonoBehaviour
{
    const string CompletionKey = "RealBus.FirstDriveTutorialComplete";
    public static FirstDriveTutorial Instance { get; private set; }
    Canvas canvas;
    TextMeshProUGUI title, instruction, progress;
    Button continueButton;
    BusController bus;
    int step;
    bool reachedDrivingSpeed;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static FirstDriveTutorial EnsureExists()
    {
        if (Instance != null) return Instance;
        FirstDriveTutorial existing = FindFirstObjectByType<FirstDriveTutorial>();
        return existing != null ? existing : new GameObject("FirstDriveTutorial").AddComponent<FirstDriveTutorial>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start() { StartCoroutine(BindWhenReady()); }
    void OnDestroy() { if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded; }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { StartCoroutine(BindWhenReady()); }

    IEnumerator BindWhenReady()
    {
        for (int i = 0; i < 120; i++)
        {
            bus = FindFirstObjectByType<BusController>();
            if (bus != null) break;
            yield return null;
        }
        BindScene();
    }

    void BindScene()
    {
        bus = FindFirstObjectByType<BusController>();
        if (bus == null) { Hide(); return; }
        if (bus.GetComponent<DrivingAssistSystem>() == null) bus.gameObject.AddComponent<DrivingAssistSystem>();
        if (PlayerPrefs.GetInt(CompletionKey, 0) != 0) { Hide(); return; }
        if (canvas == null) Build();
        step = 0; reachedDrivingSpeed = false; canvas.gameObject.SetActive(true); Refresh();
    }

    void Update()
    {
        if (canvas == null || !canvas.gameObject.activeSelf || bus == null) return;
        if (bus.currentSpeedKmh >= 8f) reachedDrivingSpeed = true;
        if (step == 1 && reachedDrivingSpeed) Advance();
        else if (step == 2 && reachedDrivingSpeed && bus.brakeInput > 0.2f && bus.currentSpeedKmh < 5f) Advance();
        else if (step == 3 && Mathf.Abs(bus.steerInput) > 0.25f) Advance();
        else if (step == 4)
        {
            MissionManager mission = MissionManager.Instance;
            if (mission == null || !mission.routeActive) Complete();
            else if (mission.distanceToNextStop <= 50f) Advance();
        }
        else if (step == 5 && (DockingZone.CurrentDockedZone != null || MissionManager.Instance == null || !MissionManager.Instance.routeActive)) Complete();
    }

    void Advance() { step++; Refresh(); }
    public void Skip() { Complete(); }
    public static void ResetTutorial() { PlayerPrefs.DeleteKey(CompletionKey); }

    void Complete()
    {
        if (step == 99) return;
        step = 99;
        PlayerPrefs.SetInt(CompletionKey, 1); PlayerPrefs.Save();
        title.text = "YOU'RE READY"; instruction.text = "Drive smoothly, watch the schedule, and enjoy the route."; progress.text = "COMPLETE";
        continueButton.gameObject.SetActive(true); continueButton.GetComponentInChildren<TextMeshProUGUI>().text = "START DRIVING";
        continueButton.onClick.RemoveAllListeners(); continueButton.onClick.AddListener(Hide);
    }

    void Refresh()
    {
        continueButton.gameObject.SetActive(step == 0);
        title.text = step == 0 ? "WELCOME, DRIVER" : "DRIVING SCHOOL";
        if (step == 0) instruction.text = "W/S drive, A/D steer, E/Q shift, X selects reverse, and P toggles the parking brake. Touch controls work the same way.";
        else if (step == 1) instruction.text = "Hold the accelerator and reach 8 km/h.";
        else if (step == 2) instruction.text = "Brake smoothly and stop below 5 km/h.";
        else if (step == 3) instruction.text = "Steer left or right to feel the response curve.";
        else if (step == 4) instruction.text = "Follow the route guidance towards the next stop.";
        else instruction.text = "Enter the marked bay slowly and align with the kerb.";
        progress.text = $"STEP {Mathf.Min(step + 1, 6)} / 6";
    }

    void Build()
    {
        GameObject c = new GameObject("TutorialCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster)); c.transform.SetParent(transform, false);
        canvas = c.GetComponent<Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceOverlay; canvas.sortingOrder = 140;
        CanvasScaler scaler = c.GetComponent<CanvasScaler>(); scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize; scaler.referenceResolution = new Vector2(1920f, 1080f);
        GameObject panel = new GameObject("TutorialCard", typeof(RectTransform), typeof(Image)); panel.transform.SetParent(c.transform, false);
        RectTransform p = panel.GetComponent<RectTransform>(); p.anchorMin = p.anchorMax = p.pivot = new Vector2(0f, 1f); p.sizeDelta = new Vector2(650f, 280f); p.anchoredPosition = new Vector2(34f, -34f); panel.GetComponent<Image>().color = new Color(0.04f, 0.06f, 0.07f, 0.94f);
        title = Text(panel.transform, "WELCOME, DRIVER", 36f, new Vector2(0f, 92f), new Vector2(570f, 52f));
        instruction = Text(panel.transform, "", 28f, new Vector2(0f, 18f), new Vector2(570f, 92f));
        progress = Text(panel.transform, "STEP 1 / 6", 24f, new Vector2(-185f, -94f), new Vector2(200f, 44f));
        continueButton = MakeButton(panel.transform, "CONTINUE", new Vector2(155f, -94f), Advance);
        MakeButton(panel.transform, "SKIP", new Vector2(275f, 110f), Skip, 100f);
    }

    TextMeshProUGUI Text(Transform parent, string value, float size, Vector2 position, Vector2 dimensions)
    {
        GameObject o = new GameObject("Text", typeof(RectTransform), typeof(TextMeshProUGUI)); o.transform.SetParent(parent, false); RectTransform r = o.GetComponent<RectTransform>(); r.sizeDelta = dimensions; r.anchoredPosition = position;
        TextMeshProUGUI t = o.GetComponent<TextMeshProUGUI>(); t.text = value; t.fontSize = Mathf.Max(28f, size); t.color = UITheme.TextPrimary; t.alignment = TextAlignmentOptions.Left; t.font = UITheme.GetFont(size >= 34f ? UITheme.FontWeight.Bold : UITheme.FontWeight.Medium); return t;
    }

    Button MakeButton(Transform parent, string label, Vector2 position, UnityEngine.Events.UnityAction action, float width = 220f)
    {
        GameObject o = new GameObject("Button_" + label, typeof(RectTransform), typeof(Image), typeof(Button)); o.transform.SetParent(parent, false); RectTransform r = o.GetComponent<RectTransform>(); r.sizeDelta = new Vector2(width, 54f); r.anchoredPosition = position; o.GetComponent<Image>().color = UITheme.Accent;
        Button b = o.GetComponent<Button>(); b.onClick.AddListener(action); TextMeshProUGUI t = Text(o.transform, label, 26f, Vector2.zero, r.sizeDelta); t.alignment = TextAlignmentOptions.Center; t.color = UITheme.Background; return b;
    }

    void Hide() { if (canvas != null) canvas.gameObject.SetActive(false); }
}
