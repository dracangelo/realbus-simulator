using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Phase7MenuController : MonoBehaviour
{
    public static Phase7MenuController Instance { get; private set; }
    Canvas canvas;
    GameObject settingsPanel, mobileSettingsPanel, creditsPanel, pausePanel, dailyPanel;
    TextMeshProUGUI pauseScore;
    TextMeshProUGUI dailySummary;
    bool paused;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static Phase7MenuController EnsureExists()
    {
        if (Instance != null) return Instance;
        Phase7MenuController existing = FindFirstObjectByType<Phase7MenuController>();
        return existing != null ? existing : new GameObject("Phase7Menus").AddComponent<Phase7MenuController>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start() { StartCoroutine(BuildWhenReady()); }
    void OnDestroy() { if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded; }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { Resume(); StopAllCoroutines(); StartCoroutine(BuildWhenReady()); }

    System.Collections.IEnumerator BuildWhenReady()
    {
        for (int i = 0; i < 120; i++)
        {
            if (FindFirstObjectByType<MainMenuUI>() != null || FindFirstObjectByType<BusController>() != null) break;
            yield return null;
        }
        BuildForScene();
    }

    void Update()
    {
        if (FindFirstObjectByType<BusController>() == null) return;
        if (UnityEngine.InputSystem.Keyboard.current != null && UnityEngine.InputSystem.Keyboard.current.escapeKey.wasPressedThisFrame)
            TogglePause();
    }

    void BuildForScene()
    {
        if (canvas != null) Destroy(canvas.gameObject);
        canvas = CreateCanvas();
        bool mainMenu = FindFirstObjectByType<MainMenuUI>() != null;
        bool gameplay = FindFirstObjectByType<BusController>() != null;
        if (mainMenu)
        {
            CreateButton(canvas.transform, "SETTINGS", new Vector2(-160f, -48f), new Vector2(210f, 60f), ToggleSettings, new Vector2(1f, 1f));
        }
        if (gameplay)
        {
            CreateButton(canvas.transform, "Ⅱ", new Vector2(-28f, -210f), new Vector2(78f, 78f), TogglePause, Vector2.one, Vector2.one);
            CreateButton(canvas.transform, "CAM", new Vector2(-120f, -210f), new Vector2(78f, 78f), () => MobileCameraController.EnsureExists().CycleCamera(), Vector2.one, Vector2.one);
        }
        BuildSettings(); BuildMobileSettings(); BuildCredits(); BuildPause(); BuildDaily();
    }

    void BuildSettings()
    {
        settingsPanel = Modal("SettingsPanel", new Vector2(820f, 980f));
        CreateText(settingsPanel.transform, "SETTINGS", 42f, new Vector2(0f, 430f), new Vector2(700f, 60f), TextAlignmentOptions.Center);
        SettingsManager settings = SettingsManager.EnsureExists();
        float y = 340f;
        AddSetting("Controls", () => SettingsManager.ControlLabel(settings.Current.controlScheme), settings.CycleControl, ref y);
        AddSetting("Quality", () => QualitySettings.names.Length > 0 ? QualitySettings.names[settings.Current.qualityLevel] : "Default", settings.CycleQuality, ref y);
        AddSetting("Language", () => settings.Current.language.ToString(), settings.CycleLanguage, ref y);
        AddSetting("Master volume", () => Mathf.RoundToInt(settings.Current.masterVolume * 100f) + "%", () => { settings.Current.masterVolume = CycleVolume(settings.Current.masterVolume); settings.SaveAndApply(); }, ref y);
        AddSetting("Music volume", () => Mathf.RoundToInt(settings.Current.musicVolume * 100f) + "%", () => { settings.Current.musicVolume = CycleVolume(settings.Current.musicVolume); settings.SaveAndApply(); }, ref y);
        AddSetting("Effects volume", () => Mathf.RoundToInt(settings.Current.effectsVolume * 100f) + "%", () => { settings.Current.effectsVolume = CycleVolume(settings.Current.effectsVolume); settings.SaveAndApply(); }, ref y);
        AddSetting("Large text", () => OnOff(settings.Current.largeText), () => { settings.Current.largeText = !settings.Current.largeText; settings.SaveAndApply(); }, ref y);
        AddSetting("High contrast", () => OnOff(settings.Current.highContrast), () => { settings.Current.highContrast = !settings.Current.highContrast; settings.SaveAndApply(); }, ref y);
        AddSetting("Colour-blind icons", () => OnOff(settings.Current.colourBlindIcons), () => { settings.Current.colourBlindIcons = !settings.Current.colourBlindIcons; settings.SaveAndApply(); }, ref y);
        AddSetting("Haptics", () => OnOff(settings.Current.haptics), () => { settings.Current.haptics = !settings.Current.haptics; settings.SaveAndApply(); }, ref y);
        AddSetting("Subtitles", () => OnOff(settings.Current.subtitles), () => { settings.Current.subtitles = !settings.Current.subtitles; settings.SaveAndApply(); }, ref y);
        CreateButton(settingsPanel.transform, "MOBILE & PERFORMANCE", new Vector2(-230f, -440f), new Vector2(300f, 58f), ToggleMobileSettings);
        CreateButton(settingsPanel.transform, "CREDITS", new Vector2(100f, -440f), new Vector2(200f, 58f), OpenCreditsFromSettings);
        CreateButton(settingsPanel.transform, "CLOSE", new Vector2(300f, -440f), new Vector2(160f, 58f), ToggleSettings);
        settingsPanel.SetActive(false);
    }

    void BuildMobileSettings()
    {
        mobileSettingsPanel = Modal("MobileSettingsPanel", new Vector2(820f, 820f));
        CreateText(mobileSettingsPanel.transform, "MOBILE & PERFORMANCE", 40f, new Vector2(0f, 350f), new Vector2(720f, 60f), TextAlignmentOptions.Center);
        SettingsManager settings = SettingsManager.EnsureExists(); float y = 270f;
        AddSettingTo(mobileSettingsPanel, "Frame rate", () => settings.Current.targetFrameRate + " FPS", settings.CycleFrameRate, ref y);
        AddSettingTo(mobileSettingsPanel, "Adaptive scaling", () => OnOff(settings.Current.adaptivePerformance), () => { settings.Current.adaptivePerformance = !settings.Current.adaptivePerformance; settings.SaveAndApply(); }, ref y);
        AddSettingTo(mobileSettingsPanel, "Driving assists", () => OnOff(settings.Current.drivingAssists), () => { settings.Current.drivingAssists = !settings.Current.drivingAssists; settings.SaveAndApply(); }, ref y);
        AddSettingTo(mobileSettingsPanel, "Left-handed layout", () => OnOff(settings.Current.leftHandedControls), () => { settings.Current.leftHandedControls = !settings.Current.leftHandedControls; settings.SaveAndApply(); }, ref y);
        AddSettingTo(mobileSettingsPanel, "Control size", () => Mathf.RoundToInt(settings.Current.touchControlScale * 100f) + "%", () => { settings.Current.touchControlScale = CycleRange(settings.Current.touchControlScale, 0.75f, 1.25f, 0.125f); settings.SaveAndApply(); }, ref y);
        AddSettingTo(mobileSettingsPanel, "Steering response", () => Mathf.RoundToInt(settings.Current.steeringSensitivity * 100f) + "%", () => { settings.Current.steeringSensitivity = CycleRange(settings.Current.steeringSensitivity, 0.5f, 1.5f, 0.25f); settings.SaveAndApply(); }, ref y);
        AddSettingTo(mobileSettingsPanel, "Steering dead zone", () => Mathf.RoundToInt(settings.Current.steeringDeadZone * 100f) + "%", () => { settings.Current.steeringDeadZone = CycleRange(settings.Current.steeringDeadZone, 0f, 0.2f, 0.05f); settings.SaveAndApply(); }, ref y);
        AddSettingTo(mobileSettingsPanel, "Reduced motion", () => OnOff(settings.Current.reducedMotion), () => { settings.Current.reducedMotion = !settings.Current.reducedMotion; settings.SaveAndApply(); }, ref y);
        if (FindFirstObjectByType<MobileControlsUI>() != null)
            CreateButton(mobileSettingsPanel.transform, "EDIT CONTROL LAYOUT", new Vector2(-170f, -320f), new Vector2(320f, 58f), BeginControlLayoutEdit);
        CreateButton(mobileSettingsPanel.transform, "BACK", new Vector2(210f, -320f), new Vector2(260f, 58f), ToggleMobileSettings);
        mobileSettingsPanel.SetActive(false);
    }

    void AddSetting(string label, Func<string> value, Action action, ref float y)
    {
        AddSettingTo(settingsPanel, label, value, action, ref y);
    }

    void AddSettingTo(GameObject targetPanel, string label, Func<string> value, Action action, ref float y)
    {
        CreateText(targetPanel.transform, label, 28f, new Vector2(-215f, y), new Vector2(320f, 48f), TextAlignmentOptions.Left);
        TextMeshProUGUI valueText = null;
        Button button = CreateButton(targetPanel.transform, value(), new Vector2(220f, y), new Vector2(300f, 52f), null);
        valueText = button.GetComponentInChildren<TextMeshProUGUI>();
        button.onClick.AddListener(() => { action(); valueText.text = value(); });
        y -= 67f;
    }

    void BuildCredits()
    {
        creditsPanel = Modal("CreditsPanel", new Vector2(760f, 440f));
        CreateText(creditsPanel.transform, "REALBUS", 48f, new Vector2(0f, 145f), new Vector2(650f, 70f), TextAlignmentOptions.Center);
        CreateText(creditsPanel.transform, "Design, engineering and simulation team\nOpen map and weather data providers\nBuilt with Unity", 29f, new Vector2(0f, 25f), new Vector2(650f, 180f), TextAlignmentOptions.Center);
        CreateButton(creditsPanel.transform, "BACK TO SETTINGS", new Vector2(0f, -150f), new Vector2(320f, 58f), CloseCreditsToSettings);
        creditsPanel.SetActive(false);
    }

    void BuildPause()
    {
        pausePanel = Modal("PausePanel", new Vector2(760f, 640f));
        CreateText(pausePanel.transform, "PAUSED", 48f, new Vector2(0f, 250f), new Vector2(650f, 70f), TextAlignmentOptions.Center);
        pauseScore = CreateText(pausePanel.transform, "Score unavailable\n\nW/S DRIVE  •  A/D STEER  •  E/Q SHIFT\nX REVERSE  •  P PARKING BRAKE", 26f, new Vector2(0f, 115f), new Vector2(650f, 220f), TextAlignmentOptions.Center);
        CreateButton(pausePanel.transform, "RESUME", new Vector2(0f, -30f), new Vector2(330f, 58f), TogglePause);
        CreateButton(pausePanel.transform, "SETTINGS", new Vector2(0f, -105f), new Vector2(330f, 58f), ToggleSettings);
        CreateButton(pausePanel.transform, "CONTROL LAYOUT", new Vector2(0f, -180f), new Vector2(330f, 58f), BeginControlLayoutEdit);
        CreateButton(pausePanel.transform, "MAIN MENU", new Vector2(0f, -255f), new Vector2(330f, 58f), () => { Resume(); SceneLoader.Instance?.LoadMainMenu(); });
        pausePanel.SetActive(false);
    }

    void BuildDaily()
    {
        dailyPanel = Modal("DailyChallengePanel", new Vector2(820f, 650f));
        CreateText(dailyPanel.transform, "DAILY ROUTE CHALLENGE", 42f, new Vector2(0f, 250f), new Vector2(700f, 64f), TextAlignmentOptions.Center);
        dailySummary = CreateText(dailyPanel.transform, "Loading today's route…", 29f, new Vector2(0f, 45f), new Vector2(680f, 300f), TextAlignmentOptions.Center);
        CreateButton(dailyPanel.transform, "DRIVE CHALLENGE", new Vector2(-170f, -245f), new Vector2(320f, 60f), StartDailyChallenge);
        CreateButton(dailyPanel.transform, "CLOSE", new Vector2(210f, -245f), new Vector2(260f, 60f), ToggleDaily);
        dailyPanel.SetActive(false);
    }

    public void TogglePause()
    {
        if (pausePanel == null) return;
        paused = !paused; pausePanel.SetActive(paused); Time.timeScale = paused ? 0f : 1f; AudioListener.pause = paused;
        if (paused && pauseScore != null)
        {
            ScoreTracker score = ScoreTracker.Instance;
            string summary = score != null
                ? $"TOTAL {score.totalScore:0}%\nPunctuality {score.punctualityScore:0}  •  Satisfaction {score.satisfactionScore:0}\nSafety {score.safetyScore:0}  •  Efficiency {score.efficiencyScore:0}"
                : "Free drive session";
            pauseScore.text = summary + "\n\nW/S DRIVE  •  A/D STEER  •  E/Q SHIFT\nX REVERSE  •  P PARKING BRAKE";
        }
    }

    public void Resume() { paused = false; Time.timeScale = 1f; AudioListener.pause = false; if (pausePanel != null) pausePanel.SetActive(false); }
    void ToggleSettings() { if (settingsPanel != null) settingsPanel.SetActive(!settingsPanel.activeSelf); }
    void ToggleMobileSettings()
    {
        if (mobileSettingsPanel == null) return;
        bool show = !mobileSettingsPanel.activeSelf; mobileSettingsPanel.SetActive(show);
        if (settingsPanel != null) settingsPanel.SetActive(!show);
    }
    void BeginControlLayoutEdit()
    {
        MobileControlsUI mobile = FindFirstObjectByType<MobileControlsUI>(); if (mobile == null) return;
        MobileControlLayoutEditor editor = mobile.GetComponent<MobileControlLayoutEditor>(); if (editor == null) editor = mobile.gameObject.AddComponent<MobileControlLayoutEditor>();
        editor.Bind(mobile); if (settingsPanel != null) settingsPanel.SetActive(false); if (mobileSettingsPanel != null) mobileSettingsPanel.SetActive(false); if (pausePanel != null) pausePanel.SetActive(false); editor.SetEditing(true);
    }
    public void ReturnFromControlLayout()
    {
        if (paused && pausePanel != null) pausePanel.SetActive(true);
    }
    void OpenCreditsFromSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(true);
    }

    void CloseCreditsToSettings()
    {
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void ShowDailyChallenge()
    {
        if (dailyPanel == null) BuildForScene();
        if (dailyPanel == null) return;
        dailyPanel.SetActive(true);
        DailyChallengeManager daily = DailyChallengeManager.EnsureExists();
        daily.RefreshChallenge();
        if (dailySummary != null) dailySummary.text = daily.GetSummary();
    }

    void ToggleDaily()
    {
        if (dailyPanel == null) return;
        if (dailyPanel.activeSelf) dailyPanel.SetActive(false);
        else ShowDailyChallenge();
    }
    void StartDailyChallenge()
    {
        DailyChallengeManager daily = DailyChallengeManager.EnsureExists();
        if (!daily.ActivateChallenge())
        {
            if (dailySummary != null) dailySummary.text = daily.GetSummary();
            SubtitleManager.EnsureExists().Show("Daily challenge", "Download or select a city route first.", 3f); return;
        }
        if (dailyPanel != null) dailyPanel.SetActive(false);
        SceneLoader.Instance?.LoadGameChecked();
    }
    static float CycleVolume(float value) { int step = (Mathf.RoundToInt(value * 4f) + 1) % 5; return step / 4f; }
    static float CycleRange(float value, float minimum, float maximum, float step) { float next = value + step; return next > maximum + 0.001f ? minimum : next; }
    static string OnOff(bool value) { return value ? "On" : "Off"; }

    Canvas CreateCanvas()
    {
        GameObject o = new GameObject("Phase7MenuCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster)); o.transform.SetParent(transform, false);
        Canvas c = o.GetComponent<Canvas>(); c.renderMode = RenderMode.ScreenSpaceOverlay; c.sortingOrder = 120;
        CanvasScaler s = o.GetComponent<CanvasScaler>(); s.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize; s.referenceResolution = new Vector2(1920f, 1080f); s.matchWidthOrHeight = 0.5f;
        return c;
    }

    GameObject Modal(string name, Vector2 size)
    {
        GameObject shade = new GameObject(name, typeof(RectTransform), typeof(Image)); shade.transform.SetParent(canvas.transform, false);
        RectTransform sr = shade.GetComponent<RectTransform>(); sr.anchorMin = Vector2.zero; sr.anchorMax = Vector2.one; sr.offsetMin = sr.offsetMax = Vector2.zero;
        shade.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.76f);
        GameObject panel = new GameObject("Panel", typeof(RectTransform), typeof(Image)); panel.transform.SetParent(shade.transform, false);
        RectTransform pr = panel.GetComponent<RectTransform>(); pr.anchorMin = pr.anchorMax = pr.pivot = new Vector2(0.5f, 0.5f); pr.sizeDelta = size;
        Image panelImage = panel.GetComponent<Image>(); panelImage.color = UITheme.SurfaceContainer; RuntimeUiShapes.Rounded(panelImage); RuntimeUiShapes.SoftShadow(panelImage, .6f, 12f);
        return shade;
    }

    Button CreateButton(Transform parent, string label, Vector2 position, Vector2 size, Action action, Vector2? anchor = null, Vector2? pivot = null)
    {
        GameObject o = new GameObject("Button_" + label.Replace(" ", ""), typeof(RectTransform), typeof(Image), typeof(Button)); o.transform.SetParent(parent, false);
        RectTransform r = o.GetComponent<RectTransform>(); Vector2 a = anchor ?? new Vector2(0.5f, 0.5f); r.anchorMin = r.anchorMax = a; r.pivot = pivot ?? new Vector2(0.5f, 0.5f); r.sizeDelta = size; r.anchoredPosition = position;
        Image image = o.GetComponent<Image>(); image.color = UITheme.Accent;
        bool circular = Mathf.Abs(size.x - size.y) < 10f;
        if (circular) RuntimeUiShapes.Circle(image); else RuntimeUiShapes.Rounded(image);
        RuntimeUiShapes.SoftShadow(image, .42f, 7f);
        Button b = o.GetComponent<Button>(); if (action != null) b.onClick.AddListener(() => action());
        TextMeshProUGUI t = CreateText(o.transform, label, 28f, Vector2.zero, size, TextAlignmentOptions.Center); t.color = UITheme.Background;
        return b;
    }

    TextMeshProUGUI CreateText(Transform parent, string value, float fontSize, Vector2 position, Vector2 size, TextAlignmentOptions alignment)
    {
        GameObject o = new GameObject("Text", typeof(RectTransform), typeof(TextMeshProUGUI)); o.transform.SetParent(parent, false);
        RectTransform r = o.GetComponent<RectTransform>(); r.sizeDelta = size; r.anchoredPosition = position;
        TextMeshProUGUI t = o.GetComponent<TextMeshProUGUI>(); t.text = value; t.fontSize = Mathf.Max(28f, fontSize); t.alignment = alignment; t.color = UITheme.TextPrimary; t.font = UITheme.GetFont(fontSize >= 40f ? UITheme.FontWeight.Bold : UITheme.FontWeight.Medium);
        return t;
    }
}
