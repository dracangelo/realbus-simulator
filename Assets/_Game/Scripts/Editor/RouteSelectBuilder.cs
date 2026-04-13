#if UNITY_EDITOR
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public static class RouteSelectBuilder
{
    static readonly Color C_BG = Hex("#0C0F10");
    static readonly Color C_BG_ALT = Hex("#101416");
    static readonly Color C_SURFACE = Hex("#161A1C");
    static readonly Color C_CARD = Hex("#1C2023");
    static readonly Color C_CARD_HI = Hex("#222629");
    static readonly Color C_ACCENT = Hex("#FF9159");
    static readonly Color C_SECONDARY = Hex("#15A4FF");
    static readonly Color C_TERTIARY = Hex("#FDD400");
    static readonly Color C_TEXT = Hex("#F8F9FC");
    static readonly Color C_TEXT_MUTED = Hex("#A9ABAE");
    static readonly Color C_TEXT_DIM = Hex("#737678");
    static readonly Color C_OUTLINE = Hex("#45484A");

    [MenuItem("Tools/Real Bus Sim/Build Route Select Scene")]
    public static void Build()
    {
        if (Object.FindObjectOfType<Canvas>() != null)
        {
            bool ok = EditorUtility.DisplayDialog(
                "Route Select Builder",
                "A Canvas already exists in the scene.\nProceed and add the hierarchy anyway?",
                "Yes, add it", "Cancel");
            if (!ok) return;
        }

        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 0;

        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = 0.5f;

        canvasGO.AddComponent<GraphicRaycaster>();

        GameObject mainPanel = MakePanel("MainPanel", canvasGO.transform, C_BG);
        CanvasGroup canvasGroup = mainPanel.AddComponent<CanvasGroup>();
        Stretch(mainPanel, 0, 0, 0, 0);

        GameObject contentPanel = MakeRect("ContentPanel", mainPanel.transform);
        Stretch(contentPanel, 64, -64, -48, 48);

        BuildTopBar(contentPanel.transform);

        GameObject headerSection = MakeRect("HeaderSection", contentPanel.transform);
        SetAnchor(headerSection, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0.5f, 1f));
        SetOffsetMinMax(headerSection, new Vector2(0f, -220f), new Vector2(0f, -96f));

        TextMeshProUGUI titleText = MakeTMPAnchored("Text_Title", headerSection.transform, "ROUTE BOARD",
            52, FontStyles.Bold, C_TEXT, new Vector2(720f, 64f),
            new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, -2f), TextAlignmentOptions.TopLeft);
        titleText.characterSpacing = 4f;

        Image accentLine = MakePanel("AccentLine", headerSection.transform, C_ACCENT).GetComponent<Image>();
        RectTransform accentRect = accentLine.rectTransform;
        accentRect.anchorMin = new Vector2(0f, 0f);
        accentRect.anchorMax = new Vector2(0f, 0f);
        accentRect.pivot = new Vector2(0f, 0f);
        accentRect.anchoredPosition = new Vector2(0f, 36f);
        accentRect.sizeDelta = new Vector2(120f, 4f);

        TextMeshProUGUI cityNameText = MakeTMPAnchored("Text_CityName", headerSection.transform,
            "CITY OPERATIONS — CITY, COUNTRY", 18, FontStyles.Normal, C_TEXT_MUTED, new Vector2(760f, 28f),
            new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 6f), TextAlignmentOptions.BottomLeft);

        GameObject metricsRow = MakeRect("MetricsRow", headerSection.transform);
        SetAnchor(metricsRow, new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f, 0.5f));
        RectOf(metricsRow).anchoredPosition = new Vector2(0f, -6f);
        RectOf(metricsRow).sizeDelta = new Vector2(720f, 92f);
        HorizontalLayoutGroup metricsLayout = metricsRow.AddComponent<HorizontalLayoutGroup>();
        metricsLayout.spacing = 16f;
        metricsLayout.childAlignment = TextAnchor.MiddleRight;
        metricsLayout.childForceExpandWidth = false;
        metricsLayout.childForceExpandHeight = false;
        BuildMetricCard(metricsRow.transform, "LINES", "8 ACTIVE", C_SECONDARY);
        BuildMetricCard(metricsRow.transform, "HEADWAY", "8-14 MIN", C_TERTIARY);
        BuildMetricCard(metricsRow.transform, "ON TIME", "96%", C_ACCENT);

        GameObject boardShell = MakePanel("BoardShell", contentPanel.transform, WithAlpha(C_CARD, 0.96f));
        SetAnchor(boardShell, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector2(0.5f, 0.5f));
        SetOffsetMinMax(boardShell, new Vector2(0f, 120f), new Vector2(0f, -180f));

        GameObject listHeader = MakeRect("ListHeader", boardShell.transform);
        SetAnchor(listHeader, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0.5f, 1f));
        SetOffsetMinMax(listHeader, new Vector2(28f, -48f), new Vector2(-28f, -16f));
        MakeTMP("Text_ListLabel", listHeader.transform, "ACTIVE SERVICE LINES", 14, FontStyles.Bold,
            C_TEXT_MUTED, new Vector2(300f, 28f), TextAlignmentOptions.MidlineLeft).characterSpacing = 4f;
        MakeTMPAnchored("Text_ListHint", listHeader.transform, "Select a line to dispatch",
            12, FontStyles.Normal, C_TEXT_DIM, new Vector2(280f, 22f),
            new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0f, 0f), TextAlignmentOptions.MidlineRight);

        GameObject scrollView = MakeRect("RouteScrollView", boardShell.transform);
        SetAnchor(scrollView, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector2(0.5f, 0.5f));
        SetOffsetMinMax(scrollView, new Vector2(24f, 24f), new Vector2(-24f, -76f));
        ScrollRect scrollRect = scrollView.AddComponent<ScrollRect>();
        scrollRect.horizontal = false;
        scrollRect.vertical = true;
        scrollRect.movementType = ScrollRect.MovementType.Clamped;
        scrollRect.scrollSensitivity = 26f;
        scrollRect.inertia = true;

        GameObject viewport = MakePanel("Viewport", scrollView.transform, new Color(0f, 0f, 0f, 1f));
        RectTransform viewportRect = RectOf(viewport);
        viewportRect.anchorMin = Vector2.zero;
        viewportRect.anchorMax = Vector2.one;
        viewportRect.offsetMin = Vector2.zero;
        viewportRect.offsetMax = Vector2.zero;
        viewport.AddComponent<Mask>().showMaskGraphic = false;

        GameObject routeContainer = MakeRect("RouteContainer", viewport.transform);
        RectTransform routeRect = RectOf(routeContainer);
        routeRect.anchorMin = new Vector2(0f, 1f);
        routeRect.anchorMax = new Vector2(1f, 1f);
        routeRect.pivot = new Vector2(0.5f, 1f);
        routeRect.anchoredPosition = Vector2.zero;
        routeRect.sizeDelta = new Vector2(0f, 0f);
        VerticalLayoutGroup layout = routeContainer.AddComponent<VerticalLayoutGroup>();
        layout.spacing = 16f;
        layout.padding = new RectOffset(10, 10, 6, 10);
        layout.childAlignment = TextAnchor.UpperCenter;
        layout.childForceExpandHeight = false;
        layout.childForceExpandWidth = true;

        ContentSizeFitter fitter = routeContainer.AddComponent<ContentSizeFitter>();
        fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        scrollRect.viewport = viewportRect;
        scrollRect.content = routeRect;

        GameObject footerBar = MakeRect("FooterBar", contentPanel.transform);
        SetAnchor(footerBar, new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(0.5f, 0f));
        SetOffsetMinMax(footerBar, new Vector2(0f, 0f), new Vector2(0f, 72f));

        GameObject footerLine = MakePanel("FooterLine", footerBar.transform, WithAlpha(C_OUTLINE, 0.45f));
        SetAnchor(footerLine, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0.5f, 1f));
        RectOf(footerLine).anchoredPosition = Vector2.zero;
        RectOf(footerLine).sizeDelta = new Vector2(0f, 1f);

        GameObject btnBack = MakePanel("Btn_Back", footerBar.transform, WithAlpha(C_SURFACE, 0.92f));
        SetAnchor(btnBack, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 0.5f));
        RectOf(btnBack).anchoredPosition = new Vector2(0f, 34f);
        RectOf(btnBack).sizeDelta = new Vector2(280f, 52f);
        Button backButton = btnBack.AddComponent<Button>();
        backButton.navigation = new Navigation { mode = Navigation.Mode.None };
        TextMeshProUGUI backButtonText = MakeTMP("Text", btnBack.transform, "BACK TO CITY", 18, FontStyles.Bold,
            C_TEXT_MUTED, new Vector2(280f, 52f), TextAlignmentOptions.Center);
        backButtonText.characterSpacing = 3f;

        if (Object.FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem");
            es.AddComponent<UnityEngine.EventSystems.EventSystem>();
            AddBestInputModule(es);
        }

        RouteSelectUI ui = mainPanel.AddComponent<RouteSelectUI>();
        ui.canvasGroup = canvasGroup;
        ui.contentPanel = RectOf(contentPanel);
        ui.titleText = titleText;
        ui.cityNameText = cityNameText;
        ui.accentLine = accentLine;
        ui.routeListContainer = routeContainer.transform;
        ui.backButton = backButton;
        ui.backButtonText = backButtonText;
        ui.backgroundPanel = mainPanel.GetComponent<Image>();

        canvasGO.AddComponent<SceneBootstrap>();

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        Debug.Log("[RouteSelectBuilder] Scene built. Verify RouteSelectUI runtime population with an active city in GameState.");
    }

    static void AddBestInputModule(GameObject eventSystemObject)
    {
        var inputSystemModuleType = System.Type.GetType(
            "UnityEngine.InputSystem.UI.InputSystemUIInputModule, Unity.InputSystem");

        if (inputSystemModuleType != null)
        {
            eventSystemObject.AddComponent(inputSystemModuleType);
            return;
        }

        eventSystemObject.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
    }

    static void BuildTopBar(Transform parent)
    {
        GameObject topBar = MakeRect("TopBar", parent);
        SetAnchor(topBar, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0.5f, 1f));
        SetOffsetMinMax(topBar, new Vector2(0f, -72f), new Vector2(0f, 0f));

        GameObject topBarBg = MakePanel("BarSurface", topBar.transform, C_BG_ALT);
        Stretch(topBarBg, 0, 0, 0, 0);

        GameObject brandGroup = MakeRect("BrandGroup", topBar.transform);
        SetAnchor(brandGroup, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 0.5f));
        RectOf(brandGroup).anchoredPosition = new Vector2(0f, -36f);
        RectOf(brandGroup).sizeDelta = new Vector2(360f, 36f);
        MakeTMP("Text_Brand", brandGroup.transform, "REAL BUS SIM", 22, FontStyles.Bold,
            C_ACCENT, new Vector2(360f, 36f), TextAlignmentOptions.MidlineLeft).characterSpacing = 5f;

        GameObject navLinks = MakeRect("NavLinks", topBar.transform);
        SetAnchor(navLinks, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
        RectOf(navLinks).anchoredPosition = new Vector2(0f, -36f);
        RectOf(navLinks).sizeDelta = new Vector2(520f, 32f);
        HorizontalLayoutGroup navLayout = navLinks.AddComponent<HorizontalLayoutGroup>();
        navLayout.spacing = 28f;
        navLayout.childAlignment = TextAnchor.MiddleCenter;
        navLayout.childForceExpandWidth = false;
        navLayout.childForceExpandHeight = false;
        AddNavLabel(navLinks.transform, "DRIVE", C_TEXT_MUTED);
        AddNavLabel(navLinks.transform, "GARAGE", C_TEXT_MUTED);
        AddNavLabel(navLinks.transform, "ROUTES", C_ACCENT);
        AddNavLabel(navLinks.transform, "MARKET", C_TEXT_MUTED);
    }

    static void AddNavLabel(Transform parent, string text, Color color)
    {
        MakeTMP("Text_" + text, parent, text, 14, FontStyles.Bold, color,
            new Vector2(120f, 28f), TextAlignmentOptions.Midline).characterSpacing = 4f;
    }

    static void BuildMetricCard(Transform parent, string label, string value, Color accent)
    {
        GameObject card = MakePanel("Metric_" + label, parent, WithAlpha(C_SURFACE, 0.86f));
        RectTransform rect = RectOf(card);
        rect.sizeDelta = new Vector2(200f, 72f);

        GameObject accentBar = MakePanel("Accent", card.transform, WithAlpha(accent, 0.28f));
        RectTransform barRect = RectOf(accentBar);
        barRect.anchorMin = new Vector2(0f, 0f);
        barRect.anchorMax = new Vector2(0f, 1f);
        barRect.pivot = new Vector2(0f, 0.5f);
        barRect.anchoredPosition = new Vector2(0f, 0f);
        barRect.sizeDelta = new Vector2(4f, 0f);

        MakeTMPAnchored("Text_Label", card.transform, label, 11, FontStyles.Bold, C_TEXT_MUTED,
            new Vector2(160f, 20f), new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(12f, -8f), TextAlignmentOptions.TopLeft).characterSpacing = 3f;

        MakeTMPAnchored("Text_Value", card.transform, value, 18, FontStyles.Bold, C_TEXT,
            new Vector2(160f, 28f), new Vector2(0f, 0f), new Vector2(0f, 0f),
            new Vector2(12f, 8f), TextAlignmentOptions.BottomLeft);
    }

    static GameObject MakePanel(string name, Transform parent, Color color)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);
        Image img = go.AddComponent<Image>();
        img.color = color;
        return go;
    }

    static GameObject MakeRect(string name, Transform parent)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);
        go.AddComponent<RectTransform>();
        return go;
    }

    static TextMeshProUGUI MakeTMP(string name, Transform parent, string text, int size, FontStyles style,
        Color color, Vector2 sizeDelta, TextAlignmentOptions align)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);
        TextMeshProUGUI tmp = go.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = size;
        tmp.fontStyle = style;
        tmp.color = color;
        tmp.alignment = align;
        RectTransform rect = tmp.rectTransform;
        rect.sizeDelta = sizeDelta;
        return tmp;
    }

    static TextMeshProUGUI MakeTMPAnchored(string name, Transform parent, string text, int size, FontStyles style,
        Color color, Vector2 sizeDelta, Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPos,
        TextAlignmentOptions align)
    {
        TextMeshProUGUI tmp = MakeTMP(name, parent, text, size, style, color, sizeDelta, align);
        RectTransform rect = tmp.rectTransform;
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.pivot = anchorMin;
        rect.anchoredPosition = anchoredPos;
        return tmp;
    }

    static void Stretch(GameObject go, float left, float right, float bottom, float top)
    {
        RectTransform rect = RectOf(go);
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = new Vector2(left, bottom);
        rect.offsetMax = new Vector2(right, top);
    }

    static void SetAnchor(GameObject go, Vector2 min, Vector2 max, Vector2 pivot)
    {
        RectTransform rect = RectOf(go);
        rect.anchorMin = min;
        rect.anchorMax = max;
        rect.pivot = pivot;
    }

    static void SetOffsetMinMax(GameObject go, Vector2 offsetMin, Vector2 offsetMax)
    {
        RectTransform rect = RectOf(go);
        rect.offsetMin = offsetMin;
        rect.offsetMax = offsetMax;
    }

    static RectTransform RectOf(GameObject go)
    {
        return go.GetComponent<RectTransform>();
    }

    static Color Hex(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color c);
        return c;
    }

    static Color WithAlpha(Color c, float a)
    {
        c.a = a;
        return c;
    }
}
#endif
