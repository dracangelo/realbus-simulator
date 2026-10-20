using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Responsive bus-themed settings and overlay menus for RealBus.
/// Includes LED matrix destination sign headers, transit equipment ticket cards,
/// responsive scrollable tabs, and iconic bus-stop bell close buttons.
/// </summary>
public class Phase7MenuController : MonoBehaviour
{
    public static Phase7MenuController Instance { get; private set; }
    Canvas canvas;
    GameObject settingsPanel, mobileSettingsPanel, contentDownloadsPanel, vehiclePacksPanel, creditsPanel, pausePanel, dailyPanel;
    TextMeshProUGUI pauseScore;
    TextMeshProUGUI dailySummary;
    TextMeshProUGUI contentCityLabel, contentStatusLabel, routePickerLabel;
    TMP_InputField routeRelationInput;
    TMP_InputField vehiclePackUrlInput;
    Slider contentProgress;
    Slider vehiclePackProgress;
    TextMeshProUGUI vehiclePackStatusLabel;
    CityDefinition[] downloadableCities = Array.Empty<CityDefinition>();
    BusRoute[] downloadableRoutes = Array.Empty<BusRoute>();
    int contentCityIndex;
    int contentRouteIndex;
    CityDefinition pendingMapCity;
    bool paused;

    // Tabs inside settings
    readonly List<GameObject> settingsCategoryPages = new List<GameObject>();
    readonly List<Image> settingsTabImages = new List<Image>();
    readonly List<TextMeshProUGUI> settingsTabTexts = new List<TextMeshProUGUI>();
    int currentSettingsTab = 0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static Phase7MenuController EnsureExists()
    {
        if (Instance != null) return Instance;
        Phase7MenuController existing = FindAnyObjectByType<Phase7MenuController>();
        return existing != null ? existing : new GameObject("Phase7Menus").AddComponent<Phase7MenuController>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start() { StartCoroutine(BuildWhenReady()); }

    void OnDestroy()
    {
        if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded;
        UnsubscribeContentDownloader();
        UnsubscribeVehiclePackDownloader();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Resume();
        StopAllCoroutines();
        StartCoroutine(BuildWhenReady());
    }

    System.Collections.IEnumerator BuildWhenReady()
    {
        for (int i = 0; i < 120; i++)
        {
            if (FindAnyObjectByType<MainMenuUI>() != null || FindAnyObjectByType<BusController>() != null) break;
            yield return null;
        }
        BuildForScene();
    }

    void Update()
    {
        if (FindAnyObjectByType<BusController>() == null) return;
        var keyboard = UnityEngine.InputSystem.Keyboard.current;
        if (keyboard != null && keyboard.escapeKey.wasPressedThisFrame)
            TogglePause();
    }

    void BuildForScene()
    {
        UnsubscribeContentDownloader();
        if (canvas != null) Destroy(canvas.gameObject);
        canvas = CreateCanvas();
        bool mainMenu = FindAnyObjectByType<MainMenuUI>() != null;
        bool gameplay = FindAnyObjectByType<BusController>() != null;

        if (mainMenu)
        {
            CreateButton(canvas.transform, "⚙ SETTINGS", new Vector2(-160f, -48f), new Vector2(210f, 60f), ToggleSettings, new Vector2(1f, 1f), null, "btn.settings");
        }
        if (gameplay)
        {
            // Camera and pause used to sit at the top-right, on top of the speed dial,
            // the indicator lamps and the vitals panel. They now live in the clear strip
            // under the top-left minimap card.
            Color glass = new Color(.03f, .045f, .055f, .92f);
            Button pauseButton = CreateStyledButton(canvas.transform, "II", new Vector2(20f, -340f), new Vector2(60f, 60f), TogglePause, glass, UITheme.BusAmber, new Vector2(0f, 1f), new Vector2(0f, 1f));
            Button cameraButton = CreateStyledButton(canvas.transform, "CAM", new Vector2(90f, -340f), new Vector2(60f, 60f), () => MobileCameraController.EnsureExists().CycleCamera(), glass, UITheme.BusAmber, new Vector2(0f, 1f), new Vector2(0f, 1f));
            StyleRoundActionButton(pauseButton);
            StyleRoundActionButton(cameraButton);
        }

        BuildSettings();
        BuildMobileSettings();
        BuildContentDownloads();
        BuildVehiclePacks();
        BuildCredits();
        BuildPause();
        BuildDaily();
    }

    // ── BUS-THEMED RESPONSIVE SETTINGS ──────────────────────────────

    void BuildSettings()
    {
        settingsPanel = Modal("SettingsPanel", new Vector2(960f, 880f), out RectTransform panel);
        settingsCategoryPages.Clear();
        settingsTabImages.Clear();
        settingsTabTexts.Clear();

        // 1. LED Destination Header
        BuildLedHeader(panel, "hdr.settings.title", "hdr.settings.subtitle", "badge.depotOnline");

        // 2. Responsive Transit Category Tabs
        string[] tabNames = { "tab.controls", "tab.audio", "tab.display", "tab.accessibility" };
        GameObject tabBar = new GameObject("TabBar", typeof(RectTransform));
        tabBar.transform.SetParent(panel, false);
        RectTransform tbr = tabBar.GetComponent<RectTransform>();
        tbr.anchorMin = new Vector2(0f, 1f);
        tbr.anchorMax = new Vector2(1f, 1f);
        tbr.pivot = new Vector2(0.5f, 1f);
        tbr.anchoredPosition = new Vector2(0f, -95f);
        tbr.sizeDelta = new Vector2(-32f, 50f);

        float tabWidth = (960f - 32f - (tabNames.Length - 1) * 8f) / tabNames.Length;
        for (int i = 0; i < tabNames.Length; i++)
        {
            int index = i;
            GameObject tabBtn = new GameObject("Tab_" + tabNames[i], typeof(RectTransform), typeof(Image), typeof(Button));
            tabBtn.transform.SetParent(tabBar.transform, false);
            RectTransform tr = tabBtn.GetComponent<RectTransform>();
            tr.anchorMin = new Vector2(0f, 0.5f);
            tr.anchorMax = new Vector2(0f, 0.5f);
            tr.pivot = new Vector2(0f, 0.5f);
            tr.sizeDelta = new Vector2(tabWidth, 46f);
            tr.anchoredPosition = new Vector2(i * (tabWidth + 8f), 0f);

            Image img = tabBtn.GetComponent<Image>();
            img.color = i == 0 ? UITheme.BusAmber : UITheme.TransitCard;
            RuntimeUiShapes.Rounded(img);
            settingsTabImages.Add(img);

            TextMeshProUGUI txt = CreateLocalizedText(tabBtn.transform, tabNames[i], 22f, Vector2.zero, tr.sizeDelta, TextAlignmentOptions.Center);
            txt.color = i == 0 ? UITheme.Background : UITheme.TextSecondary;
            settingsTabTexts.Add(txt);

            Button btn = tabBtn.GetComponent<Button>();
            btn.onClick.AddListener(() => SwitchSettingsTab(index));
        }

        // 3. Scrollable Content Area
        GameObject scrollObj = new GameObject("SettingsScrollView", typeof(RectTransform), typeof(ScrollRect));
        scrollObj.transform.SetParent(panel, false);
        RectTransform sr = scrollObj.GetComponent<RectTransform>();
        sr.anchorMin = Vector2.zero;
        sr.anchorMax = Vector2.one;
        sr.offsetMin = new Vector2(16f, 82f);
        sr.offsetMax = new Vector2(-16f, -155f);

        GameObject viewport = new GameObject("Viewport", typeof(RectTransform), typeof(RectMask2D));
        viewport.transform.SetParent(scrollObj.transform, false);
        RectTransform vr = viewport.GetComponent<RectTransform>();
        vr.anchorMin = Vector2.zero;
        vr.anchorMax = Vector2.one;
        vr.offsetMin = vr.offsetMax = Vector2.zero;

        ScrollRect scrollRect = scrollObj.GetComponent<ScrollRect>();
        scrollRect.horizontal = false;
        scrollRect.vertical = true;
        scrollRect.movementType = ScrollRect.MovementType.Clamped;
        scrollRect.scrollSensitivity = 28f;
        scrollRect.viewport = vr;

        SettingsManager settings = SettingsManager.EnsureExists();

        // Page 0: Controls
        GameObject pageControls = CreateScrollPage(viewport.transform, "Page_Controls");
        settingsCategoryPages.Add(pageControls);
        AddTransitSettingCard(pageControls.transform, "set.steeringScheme", "set.steeringScheme.desc",
            () => SettingsManager.ControlLabel(settings.Current.controlScheme), settings.CycleControl);
        AddTransitSettingCard(pageControls.transform, "set.drivingAssists", "set.drivingAssists.desc",
            () => OnOff(settings.Current.drivingAssists), () => { settings.Current.drivingAssists = !settings.Current.drivingAssists; settings.SaveAndApply(); });
        AddTransitSettingCard(pageControls.transform, "set.leftHanded", "set.leftHanded.desc",
            () => OnOff(settings.Current.leftHandedControls), () => { settings.Current.leftHandedControls = !settings.Current.leftHandedControls; settings.SaveAndApply(); });
        AddTransitSettingCard(pageControls.transform, "set.touchSize", "set.touchSize.desc",
            () => Mathf.RoundToInt(settings.Current.touchControlScale * 100f) + "%", () => { settings.Current.touchControlScale = CycleRange(settings.Current.touchControlScale, 0.75f, 1.25f, 0.125f); settings.SaveAndApply(); });
        AddTransitSettingCard(pageControls.transform, "set.steeringResponse", "set.steeringResponse.desc",
            () => Mathf.RoundToInt(settings.Current.steeringSensitivity * 100f) + "%", () => { settings.Current.steeringSensitivity = CycleRange(settings.Current.steeringSensitivity, 0.5f, 1.5f, 0.25f); settings.SaveAndApply(); });
        AddTransitSettingCard(pageControls.transform, "set.steeringDeadZone", "set.steeringDeadZone.desc",
            () => Mathf.RoundToInt(settings.Current.steeringDeadZone * 100f) + "%", () => { settings.Current.steeringDeadZone = CycleRange(settings.Current.steeringDeadZone, 0f, 0.2f, 0.05f); settings.SaveAndApply(); });
        if (FindAnyObjectByType<MobileControlsUI>() != null)
        {
            AddTransitActionCard(pageControls.transform, "set.customLayout", "set.customLayout.desc",
                "set.customLayout.action", BeginControlLayoutEdit);
        }

        // Page 1: Audio
        GameObject pageAudio = CreateScrollPage(viewport.transform, "Page_Audio");
        settingsCategoryPages.Add(pageAudio);
        AddTransitSettingCard(pageAudio.transform, "set.masterVolume", "set.masterVolume.desc",
            () => Mathf.RoundToInt(settings.Current.masterVolume * 100f) + "%", () => { settings.Current.masterVolume = CycleVolume(settings.Current.masterVolume); settings.SaveAndApply(); });
        AddTransitSettingCard(pageAudio.transform, "set.sfx", "set.sfx.desc",
            () => Mathf.RoundToInt(settings.Current.effectsVolume * 100f) + "%", () => { settings.Current.effectsVolume = CycleVolume(settings.Current.effectsVolume); settings.SaveAndApply(); });
        AddTransitSettingCard(pageAudio.transform, "set.music", "set.music.desc",
            () => Mathf.RoundToInt(settings.Current.musicVolume * 100f) + "%", () => { settings.Current.musicVolume = CycleVolume(settings.Current.musicVolume); settings.SaveAndApply(); });
        AddTransitSettingCard(pageAudio.transform, "set.subtitles", "set.subtitles.desc",
            () => OnOff(settings.Current.subtitles), () => { settings.Current.subtitles = !settings.Current.subtitles; settings.SaveAndApply(); });
        AddTransitSettingCard(pageAudio.transform, "set.haptics", "set.haptics.desc",
            () => OnOff(settings.Current.haptics), () => { settings.Current.haptics = !settings.Current.haptics; settings.SaveAndApply(); });

        // Page 2: Display
        GameObject pageDisplay = CreateScrollPage(viewport.transform, "Page_Display");
        settingsCategoryPages.Add(pageDisplay);
        AddTransitSettingCard(pageDisplay.transform, "set.quality", "set.quality.desc",
            () => QualitySettings.names.Length > 0 ? QualitySettings.names[settings.Current.qualityLevel] : "Default", settings.CycleQuality);
        AddTransitSettingCard(pageDisplay.transform, "set.framerate", "set.framerate.desc",
            () => settings.Current.targetFrameRate + " FPS", settings.CycleFrameRate);
        AddTransitSettingCard(pageDisplay.transform, "set.adaptive", "set.adaptive.desc",
            () => OnOff(settings.Current.adaptivePerformance), () => { settings.Current.adaptivePerformance = !settings.Current.adaptivePerformance; settings.SaveAndApply(); });
        AddTransitSettingCard(pageDisplay.transform, "set.reducedMotion", "set.reducedMotion.desc",
            () => OnOff(settings.Current.reducedMotion), () => { settings.Current.reducedMotion = !settings.Current.reducedMotion; settings.SaveAndApply(); });

        // Page 3: Accessibility & Depot
        GameObject pageAccess = CreateScrollPage(viewport.transform, "Page_Access");
        settingsCategoryPages.Add(pageAccess);
        AddTransitSettingCard(pageAccess.transform, "set.language", "set.language.desc",
            () => settings.Current.language.ToString(), settings.CycleLanguage);
        AddTransitSettingCard(pageAccess.transform, "set.largeText", "set.largeText.desc",
            () => OnOff(settings.Current.largeText), () => { settings.Current.largeText = !settings.Current.largeText; settings.SaveAndApply(); });
        AddTransitSettingCard(pageAccess.transform, "set.highContrast", "set.highContrast.desc",
            () => OnOff(settings.Current.highContrast), () => { settings.Current.highContrast = !settings.Current.highContrast; settings.SaveAndApply(); });
        AddTransitSettingCard(pageAccess.transform, "set.colourBlind", "set.colourBlind.desc",
            () => OnOff(settings.Current.colourBlindIcons), () => { settings.Current.colourBlindIcons = !settings.Current.colourBlindIcons; settings.SaveAndApply(); });
        AddTransitActionCard(pageAccess.transform, "set.depot", "set.depot.desc",
            "set.depot.action", ToggleContentDownloads);
        AddTransitActionCard(pageAccess.transform, "set.credits", "set.credits.desc",
            "set.credits.action", OpenCreditsFromSettings);

        SwitchSettingsTab(0);

        // 4. Pinned Transit Footer Bar
        GameObject footer = new GameObject("FooterBar", typeof(RectTransform));
        footer.transform.SetParent(panel, false);
        RectTransform fr = footer.GetComponent<RectTransform>();
        fr.anchorMin = Vector2.zero;
        fr.anchorMax = new Vector2(1f, 0f);
        fr.pivot = new Vector2(0.5f, 0f);
        fr.anchoredPosition = Vector2.zero;
        fr.sizeDelta = new Vector2(0f, 75f);

        // Iconic Bus Stop Bell Close Button (Red)
        CreateStyledButton(footer.transform, "⯀ STOP / CLOSE", new Vector2(-300f, 38f), new Vector2(260f, 52f),
            ToggleSettings, UITheme.BusStopRed, UITheme.TextPrimary, null, null, "btn.stopClose");

        CreateStyledButton(footer.transform, "DEPOT DOWNLOADS", new Vector2(10f, 38f), new Vector2(240f, 52f),
            ToggleContentDownloads, UITheme.TransitCard, UITheme.BusAmber, null, null, "btn.depotDownloads");

        CreateStyledButton(footer.transform, "MOBILE OPTIMIZE", new Vector2(280f, 38f), new Vector2(220f, 52f),
            ToggleMobileSettings, UITheme.TransitCard, UITheme.TextPrimary, null, null, "btn.mobileOptimize");

        settingsPanel.SetActive(false);
    }

    void SwitchSettingsTab(int tabIndex)
    {
        currentSettingsTab = tabIndex;
        for (int i = 0; i < settingsCategoryPages.Count; i++)
        {
            bool active = (i == tabIndex);
            settingsCategoryPages[i].SetActive(active);
            if (i < settingsTabImages.Count && settingsTabImages[i] != null)
                settingsTabImages[i].color = active ? UITheme.BusAmber : UITheme.TransitCard;
            if (i < settingsTabTexts.Count && settingsTabTexts[i] != null)
                settingsTabTexts[i].color = active ? UITheme.Background : UITheme.TextSecondary;
        }

        ScrollRect sr = settingsPanel.GetComponentInChildren<ScrollRect>();
        if (sr != null && tabIndex < settingsCategoryPages.Count)
        {
            sr.content = settingsCategoryPages[tabIndex].GetComponent<RectTransform>();
            sr.verticalNormalizedPosition = 1f;
        }
    }

    GameObject CreateScrollPage(Transform parent, string name)
    {
        GameObject page = new GameObject(name, typeof(RectTransform), typeof(VerticalLayoutGroup), typeof(ContentSizeFitter));
        page.transform.SetParent(parent, false);
        RectTransform pr = page.GetComponent<RectTransform>();
        pr.anchorMin = new Vector2(0f, 1f);
        pr.anchorMax = new Vector2(1f, 1f);
        pr.pivot = new Vector2(0.5f, 1f);
        pr.anchoredPosition = Vector2.zero;
        pr.sizeDelta = Vector2.zero;

        VerticalLayoutGroup vlg = page.GetComponent<VerticalLayoutGroup>();
        vlg.padding = new RectOffset(8, 8, 8, 12);
        vlg.spacing = 8f;
        vlg.childAlignment = TextAnchor.UpperCenter;
        vlg.childControlWidth = true;
        vlg.childControlHeight = false;
        vlg.childForceExpandWidth = true;
        vlg.childForceExpandHeight = false;

        ContentSizeFitter csf = page.GetComponent<ContentSizeFitter>();
        csf.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        return page;
    }

    void AddTransitSettingCard(Transform parent, string titleKey, string descriptionKey, Func<string> getValue, Action onClick)
    {
        GameObject card = new GameObject("Card_" + titleKey.Replace(".", "_"), typeof(RectTransform), typeof(Image), typeof(LayoutElement));
        card.transform.SetParent(parent, false);
        LayoutElement le = card.GetComponent<LayoutElement>();
        le.preferredHeight = 70f;
        le.minHeight = 70f;

        Image cardImg = card.GetComponent<Image>();
        cardImg.color = UITheme.TransitCard;
        RuntimeUiShapes.Rounded(cardImg);

        // Left LED Amber Accent Strip
        GameObject strip = new GameObject("AccentStrip", typeof(RectTransform), typeof(Image));
        strip.transform.SetParent(card.transform, false);
        RectTransform str = strip.GetComponent<RectTransform>();
        str.anchorMin = new Vector2(0f, 0f);
        str.anchorMax = new Vector2(0f, 1f);
        str.pivot = new Vector2(0f, 0.5f);
        str.sizeDelta = new Vector2(5f, 0f);
        str.anchoredPosition = Vector2.zero;
        strip.GetComponent<Image>().color = UITheme.BusAmber;

        // Label and description
        GameObject labelObj = new GameObject("Labels", typeof(RectTransform));
        labelObj.transform.SetParent(card.transform, false);
        RectTransform lr = labelObj.GetComponent<RectTransform>();
        lr.anchorMin = new Vector2(0f, 0f);
        lr.anchorMax = new Vector2(0.68f, 1f);
        lr.offsetMin = new Vector2(20f, 6f);
        lr.offsetMax = new Vector2(-8f, -6f);

        TextMeshProUGUI titleTxt = CreateLocalizedText(labelObj.transform, titleKey, 24f, new Vector2(0f, 14f), new Vector2(520f, 28f), TextAlignmentOptions.Left);
        titleTxt.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        titleTxt.color = UITheme.TextPrimary;

        TextMeshProUGUI descTxt = CreateLocalizedText(labelObj.transform, descriptionKey, 17f, new Vector2(0f, -14f), new Vector2(520f, 24f), TextAlignmentOptions.Left);
        descTxt.color = UITheme.TextSecondary;

        // Interactive Button on Right
        GameObject btnObj = new GameObject("Btn_Value", typeof(RectTransform), typeof(Image), typeof(Button));
        btnObj.transform.SetParent(card.transform, false);
        RectTransform br = btnObj.GetComponent<RectTransform>();
        br.anchorMin = new Vector2(1f, 0.5f);
        br.anchorMax = new Vector2(1f, 0.5f);
        br.pivot = new Vector2(1f, 0.5f);
        br.sizeDelta = new Vector2(240f, 50f);
        br.anchoredPosition = new Vector2(-16f, 0f);

        Image btnImg = btnObj.GetComponent<Image>();
        btnImg.color = UITheme.TransitPanel;
        RuntimeUiShapes.Rounded(btnImg);
        RuntimeUiShapes.SoftShadow(btnImg, 0.35f, 5f);

        TextMeshProUGUI valTxt = CreateText(btnObj.transform, getValue(), 23f, Vector2.zero, br.sizeDelta, TextAlignmentOptions.Center);
        valTxt.color = UITheme.BusAmber;
        valTxt.font = UITheme.GetFont(UITheme.FontWeight.Bold);

        Button btn = btnObj.GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            onClick();
            valTxt.text = getValue();
        });
    }

    void AddTransitActionCard(Transform parent, string titleKey, string descriptionKey, string actionKey, Action onClick)
    {
        GameObject card = new GameObject("CardAction_" + titleKey.Replace(".", "_"), typeof(RectTransform), typeof(Image), typeof(LayoutElement));
        card.transform.SetParent(parent, false);
        LayoutElement le = card.GetComponent<LayoutElement>();
        le.preferredHeight = 70f;
        le.minHeight = 70f;

        Image cardImg = card.GetComponent<Image>();
        cardImg.color = UITheme.TransitCard;
        RuntimeUiShapes.Rounded(cardImg);

        // Labels
        GameObject labelObj = new GameObject("Labels", typeof(RectTransform));
        labelObj.transform.SetParent(card.transform, false);
        RectTransform lr = labelObj.GetComponent<RectTransform>();
        lr.anchorMin = new Vector2(0f, 0f);
        lr.anchorMax = new Vector2(0.68f, 1f);
        lr.offsetMin = new Vector2(20f, 6f);
        lr.offsetMax = new Vector2(-8f, -6f);

        TextMeshProUGUI titleTxt = CreateLocalizedText(labelObj.transform, titleKey, 24f, new Vector2(0f, 14f), new Vector2(520f, 28f), TextAlignmentOptions.Left);
        titleTxt.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        titleTxt.color = UITheme.TextPrimary;

        TextMeshProUGUI descTxt = CreateLocalizedText(labelObj.transform, descriptionKey, 17f, new Vector2(0f, -14f), new Vector2(520f, 24f), TextAlignmentOptions.Left);
        descTxt.color = UITheme.TextSecondary;

        // Button
        CreateStyledButton(card.transform, Localization.Get(actionKey), new Vector2(-16f, 0f), new Vector2(240f, 50f),
            onClick, UITheme.Accent, UITheme.Background, new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), actionKey);
    }

    void BuildLedHeader(Transform parent, string titleKey, string subtitleKey, string badgeKey = null)
    {
        GameObject header = new GameObject("LedHeader", typeof(RectTransform), typeof(Image));
        header.transform.SetParent(parent, false);
        RectTransform hr = header.GetComponent<RectTransform>();
        hr.anchorMin = new Vector2(0f, 1f);
        hr.anchorMax = new Vector2(1f, 1f);
        hr.pivot = new Vector2(0.5f, 1f);
        hr.sizeDelta = new Vector2(0f, 85f);
        hr.anchoredPosition = Vector2.zero;

        Image img = header.GetComponent<Image>();
        img.color = UITheme.TransitNavy;
        RuntimeUiShapes.Rounded(img);

        // LED Matrix amber title
        TextMeshProUGUI t1 = CreateLocalizedText(header.transform, titleKey, 30f, new Vector2(24f, 14f), new Vector2(650f, 36f), TextAlignmentOptions.Left);
        t1.color = UITheme.BusAmber;
        t1.font = UITheme.GetFont(UITheme.FontWeight.Bold);

        TextMeshProUGUI t2 = CreateLocalizedText(header.transform, subtitleKey, 19f, new Vector2(24f, -18f), new Vector2(650f, 28f), TextAlignmentOptions.Left);
        t2.color = UITheme.TextSecondary;

        if (!string.IsNullOrEmpty(badgeKey))
        {
            GameObject badgeObj = new GameObject("Badge", typeof(RectTransform), typeof(Image));
            badgeObj.transform.SetParent(header.transform, false);
            RectTransform br = badgeObj.GetComponent<RectTransform>();
            br.anchorMin = new Vector2(1f, 0.5f);
            br.anchorMax = new Vector2(1f, 0.5f);
            br.pivot = new Vector2(1f, 0.5f);
            br.sizeDelta = new Vector2(160f, 36f);
            br.anchoredPosition = new Vector2(-24f, 0f);
            Image bImg = badgeObj.GetComponent<Image>();
            bImg.color = UITheme.TransitPanel;
            RuntimeUiShapes.Rounded(bImg);

            TextMeshProUGUI bt = CreateLocalizedText(badgeObj.transform, badgeKey, 18f, Vector2.zero, br.sizeDelta, TextAlignmentOptions.Center);
            bt.color = UITheme.TransitLedGreen;
            bt.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }
    }

    // ── MOBILE SETTINGS MODAL ───────────────────────────────────────

    void BuildMobileSettings()
    {
        mobileSettingsPanel = Modal("MobileSettingsPanel", new Vector2(880f, 780f), out RectTransform panel);
        BuildLedHeader(panel, "hdr.mobile.title", "hdr.mobile.subtitle", "badge.optimized");

        GameObject scrollObj = new GameObject("MobileScrollView", typeof(RectTransform), typeof(ScrollRect));
        scrollObj.transform.SetParent(panel, false);
        RectTransform sr = scrollObj.GetComponent<RectTransform>();
        sr.anchorMin = Vector2.zero;
        sr.anchorMax = Vector2.one;
        sr.offsetMin = new Vector2(16f, 82f);
        sr.offsetMax = new Vector2(-16f, -95f);

        GameObject viewport = new GameObject("Viewport", typeof(RectTransform), typeof(RectMask2D));
        viewport.transform.SetParent(scrollObj.transform, false);
        RectTransform vr = viewport.GetComponent<RectTransform>();
        vr.anchorMin = Vector2.zero;
        vr.anchorMax = Vector2.one;
        vr.offsetMin = vr.offsetMax = Vector2.zero;

        ScrollRect scrollRect = scrollObj.GetComponent<ScrollRect>();
        scrollRect.horizontal = false;
        scrollRect.vertical = true;
        scrollRect.viewport = vr;

        GameObject page = CreateScrollPage(viewport.transform, "MobilePage");
        scrollRect.content = page.GetComponent<RectTransform>();

        SettingsManager settings = SettingsManager.EnsureExists();
        AddTransitSettingCard(page.transform, "set.framerate", "set.framerate.desc",
            () => settings.Current.targetFrameRate + " FPS", settings.CycleFrameRate);
        AddTransitSettingCard(page.transform, "set.adaptive", "set.adaptive.desc",
            () => OnOff(settings.Current.adaptivePerformance), () => { settings.Current.adaptivePerformance = !settings.Current.adaptivePerformance; settings.SaveAndApply(); });
        AddTransitSettingCard(page.transform, "set.drivingAssists", "set.drivingAssists.desc",
            () => OnOff(settings.Current.drivingAssists), () => { settings.Current.drivingAssists = !settings.Current.drivingAssists; settings.SaveAndApply(); });
        AddTransitSettingCard(page.transform, "set.leftHanded", "set.leftHanded.desc",
            () => OnOff(settings.Current.leftHandedControls), () => { settings.Current.leftHandedControls = !settings.Current.leftHandedControls; settings.SaveAndApply(); });
        AddTransitSettingCard(page.transform, "set.touchSize", "set.touchSize.desc",
            () => Mathf.RoundToInt(settings.Current.touchControlScale * 100f) + "%", () => { settings.Current.touchControlScale = CycleRange(settings.Current.touchControlScale, 0.75f, 1.25f, 0.125f); settings.SaveAndApply(); });
        AddTransitSettingCard(page.transform, "set.steeringResponse", "set.steeringResponse.desc",
            () => Mathf.RoundToInt(settings.Current.steeringSensitivity * 100f) + "%", () => { settings.Current.steeringSensitivity = CycleRange(settings.Current.steeringSensitivity, 0.5f, 1.5f, 0.25f); settings.SaveAndApply(); });
        AddTransitSettingCard(page.transform, "set.steeringDeadZone", "set.steeringDeadZone.desc",
            () => Mathf.RoundToInt(settings.Current.steeringDeadZone * 100f) + "%", () => { settings.Current.steeringDeadZone = CycleRange(settings.Current.steeringDeadZone, 0f, 0.2f, 0.05f); settings.SaveAndApply(); });
        AddTransitSettingCard(page.transform, "set.reducedMotion", "set.reducedMotion.desc",
            () => OnOff(settings.Current.reducedMotion), () => { settings.Current.reducedMotion = !settings.Current.reducedMotion; settings.SaveAndApply(); });

        if (FindAnyObjectByType<MobileControlsUI>() != null)
            AddTransitActionCard(page.transform, "set.customLayout", "set.customLayout.desc",
                "set.customLayout.action", BeginControlLayoutEdit);

        // Footer
        GameObject footer = new GameObject("Footer", typeof(RectTransform));
        footer.transform.SetParent(panel, false);
        RectTransform fr = footer.GetComponent<RectTransform>();
        fr.anchorMin = Vector2.zero;
        fr.anchorMax = new Vector2(1f, 0f);
        fr.pivot = new Vector2(0.5f, 0f);
        fr.sizeDelta = new Vector2(0f, 75f);
        fr.anchoredPosition = Vector2.zero;

        CreateStyledButton(footer.transform, "⯀ STOP / BACK", new Vector2(0f, 38f), new Vector2(280f, 52f),
            ToggleMobileSettings, UITheme.BusStopRed, UITheme.TextPrimary, null, null, "btn.stopBack");

        mobileSettingsPanel.SetActive(false);
    }

    // ── BUS-THEMED PAUSE MENU ───────────────────────────────────────

    void BuildPause()
    {
        pausePanel = Modal("PausePanel", new Vector2(860f, 680f), out RectTransform panel);
        BuildLedHeader(panel, "hdr.pause.title", "hdr.pause.subtitle", "badge.standby");

        // Passenger info card
        GameObject card = new GameObject("ScoreCard", typeof(RectTransform), typeof(Image));
        card.transform.SetParent(panel, false);
        RectTransform cr = card.GetComponent<RectTransform>();
        cr.anchorMin = new Vector2(0.5f, 0.5f);
        cr.anchorMax = new Vector2(0.5f, 0.5f);
        cr.pivot = new Vector2(0.5f, 0.5f);
        cr.sizeDelta = new Vector2(800f, 190f);
        cr.anchoredPosition = new Vector2(0f, 130f);
        Image cardImg = card.GetComponent<Image>();
        cardImg.color = UITheme.TransitCard;
        RuntimeUiShapes.Rounded(cardImg);

        pauseScore = CreateText(card.transform, "Free drive session\n\nW/S DRIVE  •  A/D STEER  •  E/Q GEAR\nX REVERSE  •  P PARKING BRAKE", 24f,
            Vector2.zero, new Vector2(760f, 160f), TextAlignmentOptions.Center);
        pauseScore.color = UITheme.BusAmber;

        // Big transit action buttons
        CreateStyledButton(panel, "⯈ RESUME SERVICE", new Vector2(0f, -20f), new Vector2(400f, 60f),
            TogglePause, UITheme.TransitLedGreen, UITheme.Background, null, null, "btn.resumeService");

        CreateStyledButton(panel, "⚙ OPERATOR SETTINGS", new Vector2(0f, -95f), new Vector2(400f, 56f),
            ToggleSettings, UITheme.TransitCard, UITheme.BusAmber, null, null, "btn.operatorSettings");

        CreateStyledButton(panel, "🎮 EDIT CONTROLS", new Vector2(0f, -165f), new Vector2(400f, 56f),
            BeginControlLayoutEdit, UITheme.TransitCard, UITheme.TextPrimary, null, null, "btn.editControls");

        CreateStyledButton(panel, "🚏 DEPOT / MAIN MENU", new Vector2(0f, -235f), new Vector2(400f, 56f),
            () => { Resume(); SceneLoader.Instance?.LoadMainMenu(); }, UITheme.TransitNavy, UITheme.TextSecondary, null, null, "btn.depotMainMenu");

        pausePanel.SetActive(false);
    }

    // ── CONTENT DOWNLOADS MODAL ─────────────────────────────────────

    void BuildContentDownloads()
    {
        contentDownloadsPanel = Modal("ContentDownloadsPanel", new Vector2(1080f, 900f), out RectTransform panel);
        BuildLedHeader(panel, "hdr.downloads.title", "hdr.downloads.subtitle", "hdr.downloads.badge");

        downloadableCities = CityManager.Instance != null && CityManager.Instance.allCities != null
            ? Array.FindAll(CityManager.Instance.allCities, city => city != null)
            : Array.Empty<CityDefinition>();
        CityDefinition selected = GameState.Instance?.selectedCity;
        contentCityIndex = selected != null ? Array.IndexOf(downloadableCities, selected) : 0;
        if (contentCityIndex < 0) contentCityIndex = 0;

        CreateButton(panel, "‹", new Vector2(-420f, 255f), new Vector2(70f, 62f), () => CycleContentCity(-1));
        contentCityLabel = CreateText(panel, Localization.Get("status.noCities"), 29f, new Vector2(0f, 255f), new Vector2(720f, 62f), TextAlignmentOptions.Center);
        contentCityLabel.color = UITheme.BusAmber;
        CreateButton(panel, "›", new Vector2(420f, 255f), new Vector2(70f, 62f), () => CycleContentCity(1));

        CreateStyledButton(panel, "OFFLINE MAP", new Vector2(-310f, 150f), new Vector2(270f, 64f), DownloadOfflineMap, UITheme.TransitCard, UITheme.BusAmber, null, null, "btn.offlineMap");
        CreateStyledButton(panel, "WORLD DATA", new Vector2(0f, 150f), new Vector2(270f, 64f), () => DownloadContent(CityContentDownloadKind.World), UITheme.TransitCard, UITheme.TextPrimary, null, null, "btn.worldData");
        CreateStyledButton(panel, "ALL ROUTES", new Vector2(310f, 150f), new Vector2(270f, 64f), () => DownloadContent(CityContentDownloadKind.Routes), UITheme.TransitCard, UITheme.TextPrimary, null, null, "btn.allRoutes");
        CreateStyledButton(panel, "★ FAVOURITE", new Vector2(-180f, 80f), new Vector2(250f, 54f), ToggleFavouriteCity, UITheme.TransitCard, UITheme.Accent, null, null, "btn.favourite");
        CreateStyledButton(panel, "COMPLETE CITY", new Vector2(180f, 80f), new Vector2(250f, 54f), DownloadCompleteCity, UITheme.TransitLedGreen, UITheme.Background, null, null, "btn.completeCity");

        CreateText(panel, "Complete City downloads the map, world and routes without extra steps.\nBuilt-in data is used automatically when a request cannot reach the network.", 18f,
            new Vector2(0f, 22f), new Vector2(930f, 52f), TextAlignmentOptions.Center);

        CreateText(panel, "ROUTE", 22f, new Vector2(-430f, -45f), new Vector2(110f, 52f), TextAlignmentOptions.Left);
        CreateButton(panel, "‹", new Vector2(-360f, -45f), new Vector2(50f, 54f), () => CycleDownloadRoute(-1));
        routePickerLabel = CreateText(panel, "Loading routes…", 19f, new Vector2(10f, -45f), new Vector2(680f, 54f), TextAlignmentOptions.Center);
        routePickerLabel.color = UITheme.BusAmber;
        CreateButton(panel, "›", new Vector2(380f, -45f), new Vector2(50f, 54f), () => CycleDownloadRoute(1));

        CreateStyledButton(panel, "VEHICLE PACKS", new Vector2(-330f, -115f), new Vector2(250f, 58f), OpenVehiclePacks, UITheme.TransitCard, UITheme.TextPrimary, null, null, "btn.vehiclePacks");
        CreateStyledButton(panel, "DOWNLOAD ROUTE", new Vector2(-30f, -115f), new Vector2(250f, 58f), DownloadSingleRoute, UITheme.Accent, UITheme.Background, null, null, "btn.downloadRoute");
        routeRelationInput = CreateInput(panel, "or OSM ID", new Vector2(300f, -115f), new Vector2(260f, 54f));

        contentProgress = CreateSlider(panel, new Vector2(0f, -185f), new Vector2(850f, 24f));
        contentStatusLabel = CreateText(panel, Localization.Get("status.ready"), 22f, new Vector2(0f, -240f), new Vector2(900f, 70f), TextAlignmentOptions.Center);
        contentStatusLabel.color = UITheme.BusAmber;

        CreateStyledButton(panel, "CANCEL", new Vector2(-330f, -330f), new Vector2(210f, 56f),
            () => { RuntimeCityContentDownloader.EnsureExists().Cancel(); FindAnyObjectByType<CityPredownloader>()?.CancelDownload(); }, UITheme.TransitCard, UITheme.TextSecondary, null, null, "btn.cancel");
        CreateStyledButton(panel, "REMOVE CITY", new Vector2(-70f, -330f), new Vector2(270f, 56f),
            RemoveSelectedCityContent, UITheme.TransitCard, UITheme.Error, null, null, "btn.removeCity");
        CreateStyledButton(panel, "⯀ STOP / BACK", new Vector2(300f, -330f), new Vector2(240f, 56f),
            ToggleContentDownloads, UITheme.BusStopRed, UITheme.TextPrimary, null, null, "btn.stopBack");

        RuntimeCityContentDownloader downloader = RuntimeCityContentDownloader.EnsureExists();
        downloader.Changed += HandleContentDownloadChanged;
        downloader.Completed += HandleContentDownloadCompleted;
        RefreshContentCity();
        contentDownloadsPanel.SetActive(false);
    }

    // ── VEHICLE PACKS MODAL ─────────────────────────────────────────

    void BuildVehiclePacks()
    {
        vehiclePacksPanel = Modal("VehiclePacksPanel", new Vector2(1080f, 700f), out RectTransform panel);
        BuildLedHeader(panel, "hdr.fleet.title", "hdr.fleet.subtitle", "hdr.fleet.badge");

        CreateText(panel, "VEHICLE PACK HTTPS URL", 22f, new Vector2(-260f, 90f), new Vector2(420f, 52f), TextAlignmentOptions.Left);
        vehiclePackUrlInput = CreateInput(panel, "https://cdn.example.com/kenya-buses.bundle",
            new Vector2(120f, 90f), new Vector2(570f, 56f), TMP_InputField.ContentType.Standard);

        CreateStyledButton(panel, "DOWNLOAD & INSTALL PACK", new Vector2(0f, 5f), new Vector2(360f, 60f), DownloadVehiclePack, UITheme.Accent, UITheme.Background, null, null, "btn.downloadInstall");
        vehiclePackProgress = CreateSlider(panel, new Vector2(0f, -70f), new Vector2(850f, 24f));

        RuntimeVehiclePackDownloader downloader = RuntimeVehiclePackDownloader.EnsureExists();
        vehiclePackStatusLabel = CreateText(panel,
            $"Installed storage: {RuntimeCityContentDownloader.FormatBytes(downloader.GetDownloadedBytes())}", 22f,
            new Vector2(0f, -130f), new Vector2(900f, 70f), TextAlignmentOptions.Center);
        vehiclePackStatusLabel.color = UITheme.BusAmber;

        CreateStyledButton(panel, "CANCEL", new Vector2(-330f, -245f), new Vector2(210f, 56f), downloader.Cancel, UITheme.TransitCard, UITheme.TextSecondary, null, null, "btn.cancel");
        CreateStyledButton(panel, "REMOVE PACKS", new Vector2(-50f, -245f), new Vector2(260f, 56f), downloader.RemoveDownloadedPacks, UITheme.TransitCard, UITheme.Error, null, null, "btn.removePacks");
        CreateStyledButton(panel, "⯀ STOP / BACK", new Vector2(300f, -245f), new Vector2(240f, 56f), CloseVehiclePacks, UITheme.BusStopRed, UITheme.TextPrimary, null, null, "btn.stopBack");

        downloader.Changed += HandleVehiclePackChanged;
        vehiclePacksPanel.SetActive(false);
    }

    // ── CREDITS MODAL ───────────────────────────────────────────────

    void BuildCredits()
    {
        creditsPanel = Modal("CreditsPanel", new Vector2(780f, 480f), out RectTransform panel);
        BuildLedHeader(panel, "hdr.credits.title", "hdr.credits.subtitle", "hdr.credits.badge");

        CreateText(panel, "RealBus Simulator\nMaps: Mapbox / OpenStreetMap contributors\n3D Buildings: Overture Maps Foundation\nProcedural city assets are fictional transit simulations.", 22f,
            new Vector2(0f, 25f), new Vector2(680f, 180f), TextAlignmentOptions.Center);

        CreateStyledButton(panel, "OVERTURE DATA DOCS", new Vector2(-165f, -150f), new Vector2(290f, 56f),
            () => Application.OpenURL("https://docs.overturemaps.org/attribution/"), UITheme.TransitCard, UITheme.BusAmber, null, null, "btn.overtureDocs");
        CreateStyledButton(panel, "⯀ STOP / BACK", new Vector2(165f, -150f), new Vector2(290f, 56f),
            CloseCreditsToSettings, UITheme.BusStopRed, UITheme.TextPrimary, null, null, "btn.stopBack");

        creditsPanel.SetActive(false);
    }

    // ── DAILY CHALLENGE MODAL ───────────────────────────────────────

    void BuildDaily()
    {
        dailyPanel = Modal("DailyChallengePanel", new Vector2(840f, 660f), out RectTransform panel);
        BuildLedHeader(panel, "hdr.daily.title", "hdr.daily.subtitle", "hdr.daily.badge");

        dailySummary = CreateText(panel, Localization.Get("status.loadingDaily"), 28f, new Vector2(0f, 45f), new Vector2(720f, 280f), TextAlignmentOptions.Center);
        dailySummary.color = UITheme.BusAmber;

        CreateStyledButton(panel, "⯈ DRIVE CHALLENGE", new Vector2(-170f, -245f), new Vector2(320f, 60f),
            StartDailyChallenge, UITheme.TransitLedGreen, UITheme.Background, null, null, "btn.driveChallenge");
        CreateStyledButton(panel, "⯀ STOP / CLOSE", new Vector2(210f, -245f), new Vector2(260f, 60f),
            ToggleDaily, UITheme.BusStopRed, UITheme.TextPrimary, null, null, "btn.stopClose");

        dailyPanel.SetActive(false);
    }

    // ── PUBLIC ACTIONS & CONTROLS ───────────────────────────────────

    public void ToggleSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void TogglePause()
    {
        if (pausePanel == null) return;
        paused = !paused;
        pausePanel.SetActive(paused);
        Time.timeScale = paused ? 0f : 1f;
        AudioListener.pause = paused;

        if (paused && pauseScore != null)
        {
            ScoreTracker score = ScoreTracker.Instance;
            string summary = score != null
                ? $"TRIP TOTAL {score.totalScore:0}%\nPunctuality {score.punctualityScore:0}  •  Satisfaction {score.satisfactionScore:0}\nSafety {score.safetyScore:0}  •  Efficiency {score.efficiencyScore:0}"
                : "Free drive session";
            pauseScore.text = summary + "\n\nW/S DRIVE  •  A/D STEER  •  E/Q GEAR\nX REVERSE  •  P PARKING BRAKE";
        }
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        if (pausePanel != null) pausePanel.SetActive(false);
    }

    void ToggleMobileSettings()
    {
        if (mobileSettingsPanel == null) return;
        bool show = !mobileSettingsPanel.activeSelf;
        mobileSettingsPanel.SetActive(show);
        if (settingsPanel != null) settingsPanel.SetActive(!show);
    }

    void ToggleContentDownloads()
    {
        if (contentDownloadsPanel == null) return;
        bool show = !contentDownloadsPanel.activeSelf;
        contentDownloadsPanel.SetActive(show);
        if (settingsPanel != null) settingsPanel.SetActive(!show);
        if (show) RefreshContentCity();
    }

    void OpenVehiclePacks()
    {
        if (contentDownloadsPanel != null) contentDownloadsPanel.SetActive(false);
        if (vehiclePacksPanel != null) vehiclePacksPanel.SetActive(true);
    }

    void CloseVehiclePacks()
    {
        if (vehiclePacksPanel != null) vehiclePacksPanel.SetActive(false);
        if (contentDownloadsPanel != null) contentDownloadsPanel.SetActive(true);
    }

    void DownloadVehiclePack()
    {
        RuntimeVehiclePackDownloader.EnsureExists().Download(vehiclePackUrlInput != null ? vehiclePackUrlInput.text : string.Empty);
    }

    void HandleVehiclePackChanged(string status, float progress)
    {
        if (vehiclePackStatusLabel != null)
        {
            long bytes = RuntimeVehiclePackDownloader.EnsureExists().GetDownloadedBytes();
            vehiclePackStatusLabel.text = status + $"\n<size=19>Installed storage: {RuntimeCityContentDownloader.FormatBytes(bytes)}</size>";
        }
        if (vehiclePackProgress != null) vehiclePackProgress.value = progress;
    }

    void UnsubscribeVehiclePackDownloader()
    {
        RuntimeVehiclePackDownloader downloader = RuntimeVehiclePackDownloader.Instance;
        if (downloader != null) downloader.Changed -= HandleVehiclePackChanged;
    }

    CityDefinition SelectedDownloadCity()
    {
        if (downloadableCities == null || downloadableCities.Length == 0) return null;
        contentCityIndex = (contentCityIndex % downloadableCities.Length + downloadableCities.Length) % downloadableCities.Length;
        return downloadableCities[contentCityIndex];
    }

    void CycleContentCity(int direction)
    {
        if (downloadableCities == null || downloadableCities.Length == 0) return;
        contentCityIndex = (contentCityIndex + direction + downloadableCities.Length) % downloadableCities.Length;
        RefreshContentCity();
    }

    void RefreshContentCity()
    {
        CityDefinition city = SelectedDownloadCity();
        if (contentCityLabel == null) return;
        if (city == null) { contentCityLabel.text = "No cities available"; return; }
        long bytes = RuntimeCityContentStore.GetDownloadedBytes(city);
        CityPredownloader maps = FindAnyObjectByType<CityPredownloader>();
        if (maps == null) maps = new GameObject("CityPredownloader").AddComponent<CityPredownloader>();
        var mapStatus = maps.GetStatus(city, true);
        bool hasWorld = RuntimeCityContentStore.IsAvailable(city, "buildings.json") ||
                        RuntimeCityContentStore.IsAvailable(city, "buildings.xml");
        bool hasRoutes = RuntimeCityContentStore.HasRouteData(city);
        string files = $"{(PlayerCityLibrary.IsFavourite(city) ? "★ FAVOURITE" : "☆ NOT FAVOURITE")}   " +
            $"{(mapStatus != null && mapStatus.cached > 0 ? "MAP ✓" : "MAP —")}   " +
            $"{(hasWorld ? "WORLD ✓" : "WORLD —")}   " +
            $"{(hasRoutes ? "ROUTES ✓" : "ROUTES —")}";
        contentCityLabel.text = $"{city.country.ToUpperInvariant()}  /  {city.cityName.ToUpperInvariant()}\n<size=18>{files}  •  {RuntimeCityContentDownloader.FormatBytes(bytes)}</size>";
        RefreshDownloadRoutes(city);
    }

    void RefreshDownloadRoutes(CityDefinition city)
    {
        var routes = new List<BusRoute>();
        if (city != null && city.availableRoutes != null)
        {
            foreach (BusRoute route in city.availableRoutes)
            {
                if (route == null || route.osmRelationId <= 0)
                    continue;
                route.EnsureRuntimeData();
                routes.Add(route);
            }
        }
        routes.Sort((a, b) =>
        {
            int code = string.CompareOrdinal(RouteNaming.DisplayCode(a), RouteNaming.DisplayCode(b));
            return code != 0 ? code : string.CompareOrdinal(a.routeName, b.routeName);
        });
        downloadableRoutes = routes.ToArray();
        contentRouteIndex = 0;
        UpdateRoutePickerLabel();
    }

    void CycleDownloadRoute(int direction)
    {
        if (downloadableRoutes == null || downloadableRoutes.Length == 0)
        {
            HandleContentDownloadChanged("This city uses its built-in route pack. Enter an OSM relation ID only for a new route.", 0f);
            return;
        }
        contentRouteIndex = (contentRouteIndex + direction + downloadableRoutes.Length) % downloadableRoutes.Length;
        UpdateRoutePickerLabel();
    }

    void UpdateRoutePickerLabel()
    {
        if (routePickerLabel == null) return;
        if (downloadableRoutes == null || downloadableRoutes.Length == 0)
        {
            routePickerLabel.text = "Built-in routes ready • no network needed";
            return;
        }
        BusRoute route = downloadableRoutes[contentRouteIndex];
        routePickerLabel.text = $"{contentRouteIndex + 1}/{downloadableRoutes.Length}   {RouteNaming.DisplayCode(route)}  ·  {route.routeName}";
    }

    void ToggleFavouriteCity()
    {
        CityDefinition city = SelectedDownloadCity(); if (city == null) return;
        PlayerCityLibrary.SetFavourite(city, !PlayerCityLibrary.IsFavourite(city));
        RefreshContentCity();
    }

    void DownloadOfflineMap()
    {
        DownloadOfflineMap(SelectedDownloadCity());
    }

    void DownloadOfflineMap(CityDefinition city)
    {
        if (city == null) return;
        CityPredownloader maps = FindAnyObjectByType<CityPredownloader>();
        if (maps == null) maps = new GameObject("CityPredownloader").AddComponent<CityPredownloader>();
        maps.OnProgress -= HandleContentDownloadProgress; maps.OnProgress += HandleContentDownloadProgress;
        maps.OnComplete -= HandleMapDownloadComplete; maps.OnComplete += HandleMapDownloadComplete;
        if (!maps.StartDownloadForCity(city))
            HandleContentDownloadChanged(maps.LastFailure ?? "Offline map could not start.", 0f);
    }

    void HandleContentDownloadProgress(float progress) => HandleContentDownloadChanged("Downloading offline map…", progress);
    void HandleMapDownloadComplete(bool cancelled)
    {
        // "Not cancelled" is not the same as "downloaded". The menu used to say "Offline map
        // ready." for a run in which every tile had failed, which is how a broken cache looked
        // like a finished download.
        CityPredownloader maps = FindAnyObjectByType<CityPredownloader>();
        string message = cancelled
            ? "Map download cancelled."
            : maps == null || maps.LastDownloadCoveredEveryTile
                ? "Offline map ready."
                : $"Offline map incomplete — {maps.failedTiles} of {maps.totalTiles} tiles unavailable.";
        HandleContentDownloadChanged(message, 1f);
        RefreshContentCity();
    }

    void DownloadCompleteCity()
    {
        CityDefinition city = SelectedDownloadCity();
        if (city == null) return;

        // One button for the whole city: map tiles, OSM world data and routes.
        // The map starts only after the content pass finishes so the two
        // downloaders do not compete for the same connection.
        RuntimeCityContentDownloader downloader = RuntimeCityContentDownloader.EnsureExists();
        if (downloader.IsDownloading)
        {
            HandleContentDownloadChanged("A city download is already running. Wait for it to finish or cancel it.", 0f);
            return;
        }
        pendingMapCity = city;
        downloader.Download(city, CityContentDownloadKind.CompleteCity);
    }

    void DownloadContent(CityContentDownloadKind kind)
    {
        CityDefinition city = SelectedDownloadCity();
        if (city == null) return;
        RuntimeCityContentDownloader.EnsureExists().Download(city, kind);
    }

    void DownloadSingleRoute()
    {
        CityDefinition city = SelectedDownloadCity();
        if (city == null) return;

        string manual = routeRelationInput != null ? routeRelationInput.text.Trim() : string.Empty;
        if (!string.IsNullOrEmpty(manual))
        {
            if (!long.TryParse(manual, out long entered) || entered <= 0)
            {
                HandleContentDownloadChanged("Enter the numeric OSM relation ID shown on openstreetmap.org.", 0f);
                return;
            }
            RuntimeCityContentDownloader.EnsureExists().Download(city, CityContentDownloadKind.SingleRoute, entered);
            return;
        }

        if (downloadableRoutes == null || downloadableRoutes.Length == 0)
        {
            HandleContentDownloadChanged("No OSM route is listed for this city. Use ALL ROUTES or the built-in pack.", 0f);
            return;
        }

        BusRoute selected = downloadableRoutes[contentRouteIndex];
        if (selected == null || selected.osmRelationId <= 0)
        {
            HandleContentDownloadChanged("The selected route has no OpenStreetMap relation ID.", 0f);
            return;
        }
        RuntimeCityContentDownloader.EnsureExists().Download(city, CityContentDownloadKind.SingleRoute, selected.osmRelationId);
    }

    void RemoveSelectedCityContent()
    {
        CityDefinition city = SelectedDownloadCity();
        if (city == null) return;
        RuntimeCityContentDownloader.EnsureExists().Delete(city);
        FindAnyObjectByType<CityPredownloader>()?.DeleteDownloadedMap(city);
        PlayerCityLibrary.SetFavourite(city, false);
        RefreshContentCity();
    }

    void HandleContentDownloadChanged(string status, float progress)
    {
        if (contentStatusLabel != null) contentStatusLabel.text = status;
        if (contentProgress != null) contentProgress.value = progress;
    }

    void HandleContentDownloadCompleted(CityDefinition city, bool success)
    {
        RefreshContentCity();
        if (pendingMapCity == city)
        {
            pendingMapCity = null;
            if (success)
                DownloadOfflineMap(city);
        }

        if (!success || city == null || !RuntimeCityContentStore.IsDownloaded(city, "routes.json")) return;
        if (GameState.Instance != null && GameState.Instance.selectedCity == city)
        {
            OSMRouteImporter importer = OSMRouteImporter.Instance;
            if (importer == null) importer = new GameObject("OSMRouteImporter").AddComponent<OSMRouteImporter>();
            importer.TriggerImport(city);
        }
    }

    void UnsubscribeContentDownloader()
    {
        RuntimeCityContentDownloader downloader = RuntimeCityContentDownloader.Instance;
        if (downloader == null) return;
        downloader.Changed -= HandleContentDownloadChanged;
        downloader.Completed -= HandleContentDownloadCompleted;
    }

    void BeginControlLayoutEdit()
    {
        MobileControlsUI mobile = FindAnyObjectByType<MobileControlsUI>(); if (mobile == null) return;
        MobileControlLayoutEditor editor = mobile.GetComponent<MobileControlLayoutEditor>(); if (editor == null) editor = mobile.gameObject.AddComponent<MobileControlLayoutEditor>();
        editor.Bind(mobile);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (mobileSettingsPanel != null) mobileSettingsPanel.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(false);
        editor.SetEditing(true);
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
            SubtitleManager.EnsureExists().Show("Daily challenge", "Download or select a city route first.", 3f);
            return;
        }
        if (dailyPanel != null) dailyPanel.SetActive(false);
        SceneLoader.Instance?.LoadGameChecked();
    }

    static float CycleVolume(float value) { int step = (Mathf.RoundToInt(value * 4f) + 1) % 5; return step / 4f; }
    static float CycleRange(float value, float minimum, float maximum, float step) { float next = value + step; return next > maximum + 0.001f ? minimum : next; }
    static string OnOff(bool value) { return Localization.Get(value ? "value.on" : "value.off"); }

    // ── UI BUILDER HELPERS ──────────────────────────────────────────

    Canvas CreateCanvas()
    {
        GameObject o = new GameObject("Phase7MenuCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        o.transform.SetParent(transform, false);
        Canvas c = o.GetComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        c.sortingOrder = 120;
        CanvasScaler s = o.GetComponent<CanvasScaler>();
        s.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        s.referenceResolution = new Vector2(1920f, 1080f);
        s.matchWidthOrHeight = 0.5f;
        return c;
    }

    GameObject Modal(string name, Vector2 size)
    {
        return Modal(name, size, out _);
    }

    GameObject Modal(string name, Vector2 size, out RectTransform panelRect)
    {
        GameObject shade = new GameObject(name, typeof(RectTransform), typeof(Image));
        shade.transform.SetParent(canvas.transform, false);
        RectTransform sr = shade.GetComponent<RectTransform>();
        sr.anchorMin = Vector2.zero;
        sr.anchorMax = Vector2.one;
        sr.offsetMin = sr.offsetMax = Vector2.zero;
        shade.GetComponent<Image>().color = new Color(0.04f, 0.06f, 0.08f, 0.85f);

        GameObject panel = new GameObject("Panel", typeof(RectTransform), typeof(Image));
        panel.transform.SetParent(shade.transform, false);
        panelRect = panel.GetComponent<RectTransform>();
        panelRect.anchorMin = panelRect.anchorMax = panelRect.pivot = new Vector2(0.5f, 0.5f);
        panelRect.sizeDelta = size;

        Image panelImage = panel.GetComponent<Image>();
        panelImage.color = UITheme.TransitNavy;
        RuntimeUiShapes.Rounded(panelImage);
        RuntimeUiShapes.SoftShadow(panelImage, 0.7f, 16f);

        // The panel is authored for a 1920x1080 landscape canvas, so on a phone or
        // a notched display it would run off the screen and take its own footer
        // buttons with it. The fitter insets it inside the safe area and scales it
        // down when the visible canvas is smaller than the authored size.
        ResponsiveModalFitter fitter = panel.AddComponent<ResponsiveModalFitter>();
        fitter.designSize = size;

        return shade;
    }

    Button CreateButton(Transform parent, string label, Vector2 position, Vector2 size, Action action, Vector2? anchor = null, Vector2? pivot = null, string localizationKey = null)
    {
        return CreateStyledButton(parent, label, position, size, action, UITheme.Accent, UITheme.Background, anchor, pivot, localizationKey);
    }

    Button CreateStyledButton(Transform parent, string label, Vector2 position, Vector2 size, Action action,
        Color bgColor, Color textColor, Vector2? anchor = null, Vector2? pivot = null, string localizationKey = null)
    {
        GameObject o = new GameObject("Button_" + label.Replace(" ", ""), typeof(RectTransform), typeof(Image), typeof(Button));
        o.transform.SetParent(parent, false);
        RectTransform r = o.GetComponent<RectTransform>();
        Vector2 a = anchor ?? new Vector2(0.5f, 0.5f);
        r.anchorMin = r.anchorMax = a;
        r.pivot = pivot ?? new Vector2(0.5f, 0.5f);
        r.sizeDelta = size;
        r.anchoredPosition = position;

        Image image = o.GetComponent<Image>();
        image.color = bgColor;
        bool circular = Mathf.Abs(size.x - size.y) < 10f;
        if (circular) RuntimeUiShapes.Circle(image); else RuntimeUiShapes.Rounded(image);
        RuntimeUiShapes.SoftShadow(image, 0.42f, 7f);

        Button b = o.GetComponent<Button>();
        if (action != null) b.onClick.AddListener(() => action());

        TextMeshProUGUI t = CreateText(o.transform, label, 24f, Vector2.zero, size, TextAlignmentOptions.Center);
        t.color = textColor;
        t.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        if (!string.IsNullOrEmpty(localizationKey)) LocalizedText.Bind(t.gameObject, localizationKey);
        return b;
    }

    // Small round HUD verbs (camera, pause) need a tighter label than the big modal
    // buttons and a ring that reads against the world behind them.
    static void StyleRoundActionButton(Button button)
    {
        if (button == null) return;
        Outline outline = button.GetComponent<Outline>();
        if (outline == null) outline = button.gameObject.AddComponent<Outline>();
        outline.effectColor = new Color(1f, 1f, 1f, .28f);
        outline.effectDistance = new Vector2(2f, -2f);

        TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>(true);
        if (text != null)
        {
            text.fontSize = 22f;
            text.textWrappingMode = TextWrappingModes.NoWrap;
            text.overflowMode = TextOverflowModes.Ellipsis;
        }
    }

    TMP_InputField CreateInput(Transform parent, string placeholder, Vector2 position, Vector2 size,
        TMP_InputField.ContentType contentType = TMP_InputField.ContentType.IntegerNumber)
    {
        GameObject root = new GameObject("RouteRelationInput", typeof(RectTransform), typeof(Image), typeof(TMP_InputField));
        root.transform.SetParent(parent, false);
        RectTransform rect = root.GetComponent<RectTransform>();
        rect.sizeDelta = size;
        rect.anchoredPosition = position;

        Image image = root.GetComponent<Image>();
        image.color = UITheme.TransitPanel;
        RuntimeUiShapes.Rounded(image);

        TextMeshProUGUI text = CreateText(root.transform, string.Empty, 23f, Vector2.zero, size - new Vector2(28f, 0f), TextAlignmentOptions.Left);
        TextMeshProUGUI hint = CreateText(root.transform, placeholder, 20f, Vector2.zero, size - new Vector2(28f, 0f), TextAlignmentOptions.Left);
        hint.color = UITheme.TextMuted;

        TMP_InputField input = root.GetComponent<TMP_InputField>();
        input.textComponent = text;
        input.placeholder = hint;
        input.contentType = contentType;
        return input;
    }

    Slider CreateSlider(Transform parent, Vector2 position, Vector2 size)
    {
        GameObject root = new GameObject("DownloadProgress", typeof(RectTransform), typeof(Slider));
        root.transform.SetParent(parent, false);
        RectTransform rect = root.GetComponent<RectTransform>();
        rect.sizeDelta = size;
        rect.anchoredPosition = position;

        Image background = new GameObject("Background", typeof(RectTransform), typeof(Image)).GetComponent<Image>();
        background.transform.SetParent(root.transform, false);
        RectTransform br = background.rectTransform;
        br.anchorMin = Vector2.zero;
        br.anchorMax = Vector2.one;
        br.offsetMin = br.offsetMax = Vector2.zero;
        background.color = UITheme.TransitPanel;
        RuntimeUiShapes.Rounded(background);

        Image fill = new GameObject("Fill", typeof(RectTransform), typeof(Image)).GetComponent<Image>();
        fill.transform.SetParent(root.transform, false);
        RectTransform fr = fill.rectTransform;
        fr.anchorMin = Vector2.zero;
        fr.anchorMax = Vector2.one;
        fr.offsetMin = fr.offsetMax = Vector2.zero;
        fill.color = UITheme.BusAmber;
        RuntimeUiShapes.Rounded(fill);

        Slider slider = root.GetComponent<Slider>();
        slider.fillRect = fr;
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = 0f;
        slider.interactable = false;
        return slider;
    }

    /// <summary>
    /// Creates a label bound to a localization key, so it re-renders when the player
    /// changes language instead of freezing on whatever was authored at build time.
    /// </summary>
    TextMeshProUGUI CreateLocalizedText(Transform parent, string key, float fontSize, Vector2 position, Vector2 size, TextAlignmentOptions alignment)
    {
        TextMeshProUGUI label = CreateText(parent, Localization.Get(key), fontSize, position, size, alignment);
        LocalizedText.Bind(label.gameObject, key);
        return label;
    }

    TextMeshProUGUI CreateText(Transform parent, string value, float fontSize, Vector2 position, Vector2 size, TextAlignmentOptions alignment)
    {
        GameObject o = new GameObject("Text", typeof(RectTransform), typeof(TextMeshProUGUI));
        o.transform.SetParent(parent, false);
        RectTransform r = o.GetComponent<RectTransform>();
        r.sizeDelta = size;
        r.anchoredPosition = position;

        TextMeshProUGUI t = o.GetComponent<TextMeshProUGUI>();
        t.text = value;
        t.fontSize = fontSize;
        t.alignment = alignment;
        t.color = UITheme.TextPrimary;
        t.font = UITheme.GetFont(fontSize >= 32f ? UITheme.FontWeight.Bold : UITheme.FontWeight.Medium);
        return t;
    }
}
