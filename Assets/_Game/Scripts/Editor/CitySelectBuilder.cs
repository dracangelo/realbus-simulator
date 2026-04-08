#if UNITY_EDITOR
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public static class CitySelectBuilder
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
    static readonly Color C_ERROR = Hex("#B92902");

    [MenuItem("Tools/Real Bus Sim/Build City Select Scene")]
    public static void Build()
    {
        if (Object.FindObjectOfType<Canvas>() != null)
        {
            bool ok = EditorUtility.DisplayDialog(
                "City Select Builder",
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
        Stretch(contentPanel, 72, -72, -36, 36);

        BuildTopBar(contentPanel.transform);

        GameObject headerSection = MakeRect("HeaderSection", contentPanel.transform);
        SetAnchor(headerSection, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0.5f, 1f));
        SetOffsetMinMax(headerSection, new Vector2(0f, -248f), new Vector2(0f, -124f));

        TextMeshProUGUI titleText = MakeTMPAnchored("Text_Title", headerSection.transform, "SELECT CITY",
            58, FontStyles.Bold, C_TEXT, new Vector2(760f, 72f),
            new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, -6f), TextAlignmentOptions.TopLeft);
        titleText.characterSpacing = 4f;

        TextMeshProUGUI titleAccent = MakeTMPAnchored("Text_TitleAccent", headerSection.transform, "ZONE",
            58, FontStyles.Bold, C_ACCENT, new Vector2(220f, 72f),
            new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(358f, -6f), TextAlignmentOptions.TopLeft);
        titleAccent.characterSpacing = 4f;

        TextMeshProUGUI subtitleText = MakeTMPAnchored("Text_Subtitle", headerSection.transform,
            "Choose the next city in your deployment region.",
            20, FontStyles.Normal, C_TEXT_MUTED, new Vector2(920f, 34f),
            new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), TextAlignmentOptions.BottomLeft);

        Image accentLine = MakePanel("AccentLine", headerSection.transform, C_ACCENT).GetComponent<Image>();
        RectTransform accentRect = accentLine.rectTransform;
        accentRect.anchorMin = new Vector2(0f, 0f);
        accentRect.anchorMax = new Vector2(0f, 0f);
        accentRect.pivot = new Vector2(0f, 0f);
        accentRect.anchoredPosition = new Vector2(0f, 52f);
        accentRect.sizeDelta = new Vector2(96f, 4f);

        GameObject metricsRow = MakeRect("MetricsRow", headerSection.transform);
        SetAnchor(metricsRow, new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f, 0.5f));
        RectOf(metricsRow).anchoredPosition = new Vector2(0f, -4f);
        RectOf(metricsRow).sizeDelta = new Vector2(680f, 116f);
        HorizontalLayoutGroup metricsLayout = metricsRow.AddComponent<HorizontalLayoutGroup>();
        metricsLayout.spacing = 18f;
        metricsLayout.childAlignment = TextAnchor.MiddleRight;
        metricsLayout.childForceExpandWidth = false;
        metricsLayout.childForceExpandHeight = false;
        BuildMetricCard(metricsRow.transform, "NETWORK", "12 ROUTES", C_SECONDARY, "route");
        BuildMetricCard(metricsRow.transform, "STATUS", "24H SERVICE", C_TEXT_MUTED, "schedule");
        BuildMetricCard(metricsRow.transform, "MODE", "DEPLOY READY", C_ACCENT, "bolt");

        GameObject cityShell = MakePanel("CityShell", contentPanel.transform, WithAlpha(C_CARD, 0.96f));
        SetAnchor(cityShell, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector2(0.5f, 0.5f));
        SetOffsetMinMax(cityShell, new Vector2(0f, 124f), new Vector2(0f, -286f));

        GameObject cityHeader = MakeRect("CityHeader", cityShell.transform);
        SetAnchor(cityHeader, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0.5f, 1f));
        SetOffsetMinMax(cityHeader, new Vector2(28f, -56f), new Vector2(-28f, -20f));
        MakeTMP("Text_GridLabel", cityHeader.transform, "DEPLOYMENT CITIES", 14, FontStyles.Bold,
            C_TEXT_MUTED, new Vector2(320f, 36f), TextAlignmentOptions.MidlineLeft).characterSpacing = 4f;
        MakeTMPAnchored("Text_GridHint", cityHeader.transform, "Select a city to continue to routes",
            13, FontStyles.Normal, C_TEXT_DIM, new Vector2(360f, 24f),
            new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0f, 0f), TextAlignmentOptions.MidlineRight);

        GameObject scrollView = MakeRect("CityScrollView", cityShell.transform);
        SetAnchor(scrollView, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector2(0.5f, 0.5f));
        SetOffsetMinMax(scrollView, new Vector2(24f, 24f), new Vector2(-24f, -84f));
        ScrollRect scrollRect = scrollView.AddComponent<ScrollRect>();
        scrollRect.horizontal = false;
        scrollRect.vertical = true;
        scrollRect.movementType = ScrollRect.MovementType.Clamped;
        scrollRect.scrollSensitivity = 28f;
        scrollRect.inertia = true;

        GameObject viewport = MakePanel("Viewport", scrollView.transform, new Color(0f, 0f, 0f, 1f));
        RectTransform viewportRect = RectOf(viewport);
        viewportRect.anchorMin = Vector2.zero;
        viewportRect.anchorMax = Vector2.one;
        viewportRect.offsetMin = Vector2.zero;
        viewportRect.offsetMax = Vector2.zero;
        viewport.AddComponent<Mask>().showMaskGraphic = false;

        GameObject cityGrid = MakeRect("CityGrid", viewport.transform);
        RectTransform gridRect = RectOf(cityGrid);
        gridRect.anchorMin = new Vector2(0f, 1f);
        gridRect.anchorMax = new Vector2(1f, 1f);
        gridRect.pivot = new Vector2(0.5f, 1f);
        gridRect.anchoredPosition = Vector2.zero;
        gridRect.sizeDelta = new Vector2(0f, 0f);

        GridLayoutGroup grid = cityGrid.AddComponent<GridLayoutGroup>();
        // Match CitySelectUI card preferred size so 3 columns fit.
        grid.cellSize = new Vector2(195f, 160f);
        grid.spacing = new Vector2(22f, 22f);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 3;
        grid.childAlignment = TextAnchor.UpperCenter;
        grid.startAxis = GridLayoutGroup.Axis.Horizontal;
        grid.startCorner = GridLayoutGroup.Corner.UpperLeft;

        ContentSizeFitter gridFitter = cityGrid.AddComponent<ContentSizeFitter>();
        gridFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        gridFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        scrollRect.viewport = viewportRect;
        scrollRect.content = gridRect;

        GameObject intelSection = MakeRect("IntelSection", contentPanel.transform);
        SetAnchor(intelSection, new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(0.5f, 0f));
        SetOffsetMinMax(intelSection, new Vector2(0f, 88f), new Vector2(0f, 218f));
        MakeTMPAnchored("Text_Intel", intelSection.transform, "LATEST INTEL",
            18, FontStyles.Bold, C_ACCENT, new Vector2(220f, 26f),
            new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 0f), TextAlignmentOptions.TopLeft).characterSpacing = 6f;

        GameObject intelCards = MakeRect("IntelCards", intelSection.transform);
        SetAnchor(intelCards, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector2(0.5f, 0.5f));
        SetOffsetMinMax(intelCards, new Vector2(0f, 0f), new Vector2(0f, -34f));
        HorizontalLayoutGroup intelLayout = intelCards.AddComponent<HorizontalLayoutGroup>();
        intelLayout.spacing = 18f;
        intelLayout.childAlignment = TextAnchor.MiddleCenter;
        intelLayout.childForceExpandHeight = true;
        intelLayout.childForceExpandWidth = true;

        BuildIntelCard(intelCards.transform, "NETWORK EFFICIENCY", "94.2%", "+2.4% vs LAST WEEK", C_SECONDARY, 2f);
        BuildIntelCard(intelCards.transform, "ACTIVE DRIVERS", "1,402", "LIVE IN-GAME", C_TERTIARY, 1f);
        BuildIntelCard(intelCards.transform, "GLOBAL RANK", "#42", "TOP 1% WORLDWIDE", C_ACCENT, 1f);

        GameObject footerBar = MakeRect("FooterBar", contentPanel.transform);
        SetAnchor(footerBar, new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(0.5f, 0f));
        SetOffsetMinMax(footerBar, new Vector2(0f, 0f), new Vector2(0f, 64f));

        GameObject footerLine = MakePanel("FooterLine", footerBar.transform, WithAlpha(C_OUTLINE, 0.45f));
        SetAnchor(footerLine, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0.5f, 1f));
        RectOf(footerLine).anchoredPosition = Vector2.zero;
        RectOf(footerLine).sizeDelta = new Vector2(0f, 1f);

        GameObject btnBack = MakePanel("Btn_Back", footerBar.transform, WithAlpha(C_SURFACE, 0.92f));
        SetAnchor(btnBack, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 0.5f));
        RectOf(btnBack).anchoredPosition = new Vector2(0f, 30f);
        RectOf(btnBack).sizeDelta = new Vector2(260f, 48f);
        Button backButton = btnBack.AddComponent<Button>();
        backButton.navigation = new Navigation { mode = Navigation.Mode.None };
        TextMeshProUGUI backButtonText = MakeTMP("Text", btnBack.transform, "BACK", 18, FontStyles.Bold,
            C_TEXT_MUTED, new Vector2(260f, 48f), TextAlignmentOptions.Center);
        backButtonText.characterSpacing = 4f;

        if (Object.FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem");
            es.AddComponent<UnityEngine.EventSystems.EventSystem>();
            AddBestInputModule(es);
        }

        CitySelectUI ui = mainPanel.AddComponent<CitySelectUI>();
        ui.canvasGroup = canvasGroup;
        ui.contentPanel = RectOf(contentPanel);
        ui.titleText = titleText;
        ui.subtitleText = subtitleText;
        ui.cityListContainer = cityGrid.transform;
        ui.backButton = backButton;
        ui.backButtonText = backButtonText;
        ui.headerAccentLine = accentLine;
        ui.scrollRect = scrollRect;

        canvasGO.AddComponent<SceneBootstrap>();

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        Debug.Log("[CitySelectBuilder] Scene built. Verify CitySelectUI runtime population with an active country in CityManager.");
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
        SetOffsetMinMax(topBar, new Vector2(0f, -80f), new Vector2(0f, 0f));

        GameObject topBarBg = MakePanel("BarSurface", topBar.transform, C_BG);
        Stretch(topBarBg, 0, 0, 0, 0);

        GameObject brandGroup = MakeRect("BrandGroup", topBar.transform);
        SetAnchor(brandGroup, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 0.5f));
        RectOf(brandGroup).anchoredPosition = new Vector2(0f, -40f);
        RectOf(brandGroup).sizeDelta = new Vector2(360f, 42f);
        MakeTMP("Text_Brand", brandGroup.transform, "REAL BUS SIM", 26, FontStyles.Bold,
            C_ACCENT, new Vector2(360f, 42f), TextAlignmentOptions.MidlineLeft).characterSpacing = 6f;

        GameObject navLinks = MakeRect("NavLinks", topBar.transform);
        SetAnchor(navLinks, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
        RectOf(navLinks).anchoredPosition = new Vector2(0f, -40f);
        RectOf(navLinks).sizeDelta = new Vector2(520f, 36f);
        HorizontalLayoutGroup navLayout = navLinks.AddComponent<HorizontalLayoutGroup>();
        navLayout.spacing = 32f;
        navLayout.childAlignment = TextAnchor.MiddleCenter;
        navLayout.childForceExpandWidth = false;
        navLayout.childForceExpandHeight = false;
        AddNavLabel(navLinks.transform, "DRIVE", C_ACCENT);
        AddNavLabel(navLinks.transform, "GARAGE", WithAlpha(C_TEXT, 0.6f));
        AddNavLabel(navLinks.transform, "ROUTES", WithAlpha(C_TEXT, 0.6f));
        AddNavLabel(navLinks.transform, "MARKET", WithAlpha(C_TEXT, 0.6f));

        GameObject currencyBadge = MakePanel("CurrencyBadge", topBar.transform, C_SURFACE);
        SetAnchor(currencyBadge, new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f, 0.5f));
        RectOf(currencyBadge).anchoredPosition = new Vector2(0f, -40f);
        RectOf(currencyBadge).sizeDelta = new Vector2(220f, 42f);
        MakeTMP("Text_Cash", currencyBadge.transform, "$254,000", 18, FontStyles.Bold,
            C_ACCENT, new Vector2(220f, 42f), TextAlignmentOptions.Center).characterSpacing = 2f;
    }

    static void AddNavLabel(Transform parent, string text, Color color)
    {
        GameObject go = MakeRect($"Text_{text}", parent);
        LayoutElement layout = go.AddComponent<LayoutElement>();
        layout.preferredWidth = 110f;
        layout.preferredHeight = 36f;
        TextMeshProUGUI tmp = go.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = 14;
        tmp.fontStyle = FontStyles.Bold;
        tmp.color = color;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.characterSpacing = 4f;
    }

    static void BuildMetricCard(Transform parent, string label, string value, Color color, string iconText)
    {
        GameObject card = MakePanel($"Metric_{label}", parent, WithAlpha(C_BG_ALT, 0.75f));
        LayoutElement layout = card.AddComponent<LayoutElement>();
        layout.preferredWidth = 210f;
        layout.preferredHeight = 116f;
        MakeTMPAnchored("Text_Label", card.transform, label, 11, FontStyles.Bold, C_TEXT_DIM,
            new Vector2(190f, 18f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f),
            new Vector2(0f, -14f), TextAlignmentOptions.Top);
        MakeTMPAnchored("Text_Value", card.transform, value, 20, FontStyles.Bold, color,
            new Vector2(190f, 34f), new Vector2(0.5f, 0f), new Vector2(0.5f, 0f),
            new Vector2(0f, 18f), TextAlignmentOptions.Bottom);
        MakeTMPAnchored("Text_Icon", card.transform, iconText, 16, FontStyles.Bold, color,
            new Vector2(190f, 24f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
            new Vector2(0f, 0f), TextAlignmentOptions.Center);
    }

    static void BuildIntelCard(Transform parent, string label, string value, string helper, Color accent, float flex)
    {
        GameObject card = MakePanel($"Intel_{label}", parent, WithAlpha(C_CARD_HI, 0.7f));
        LayoutElement layout = card.AddComponent<LayoutElement>();
        layout.flexibleWidth = flex;
        layout.preferredHeight = 96f;
        MakeTMPAnchored("Text_Label", card.transform, label, 11, FontStyles.Bold, C_TEXT_DIM,
            new Vector2(280f, 18f), new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(18f, -14f), TextAlignmentOptions.TopLeft);
        MakeTMPAnchored("Text_Value", card.transform, value, 34, FontStyles.Bold, C_TEXT,
            new Vector2(180f, 40f), new Vector2(0f, 0f), new Vector2(0f, 0f),
            new Vector2(18f, 12f), TextAlignmentOptions.BottomLeft);
        MakeTMPAnchored("Text_Helper", card.transform, helper, 11, FontStyles.Bold, accent,
            new Vector2(220f, 18f), new Vector2(1f, 0f), new Vector2(1f, 0f),
            new Vector2(-18f, 14f), TextAlignmentOptions.BottomRight);
    }

    static GameObject MakeRect(string name, Transform parent)
    {
        GameObject go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        return go;
    }

    static GameObject MakePanel(string name, Transform parent, Color color)
    {
        GameObject go = MakeRect(name, parent);
        Image image = go.AddComponent<Image>();
        image.color = color;
        return go;
    }

    static TextMeshProUGUI MakeTMP(string name, Transform parent, string text,
        float size, FontStyles style, Color color, Vector2 sizeDelta, TextAlignmentOptions align)
    {
        GameObject go = MakeRect(name, parent);
        RectOf(go).sizeDelta = sizeDelta;
        TextMeshProUGUI tmp = go.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = size;
        tmp.fontStyle = style;
        tmp.color = color;
        tmp.alignment = align;
        return tmp;
    }

    static TextMeshProUGUI MakeTMPAnchored(string name, Transform parent, string text,
        float size, FontStyles style, Color color, Vector2 sizeDelta,
        Vector2 anchorMin, Vector2 anchorMax, Vector2 pos, TextAlignmentOptions align)
    {
        TextMeshProUGUI tmp = MakeTMP(name, parent, text, size, style, color, sizeDelta, align);
        RectTransform rt = tmp.rectTransform;
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.pivot = anchorMin;
        rt.anchoredPosition = pos;
        rt.sizeDelta = sizeDelta;
        return tmp;
    }

    static RectTransform RectOf(GameObject go) => go.GetComponent<RectTransform>();

    static void Stretch(GameObject go, float l, float r, float t, float b)
    {
        RectTransform rt = RectOf(go);
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = new Vector2(l, b);
        rt.offsetMax = new Vector2(r, t);
    }

    static void SetAnchor(GameObject go, Vector2 min, Vector2 max, Vector2 pivot)
    {
        RectTransform rt = RectOf(go);
        rt.anchorMin = min;
        rt.anchorMax = max;
        rt.pivot = pivot;
    }

    static void SetOffsetMinMax(GameObject go, Vector2 min, Vector2 max)
    {
        RectTransform rt = RectOf(go);
        rt.offsetMin = min;
        rt.offsetMax = max;
    }

    static Color Hex(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color color);
        return color;
    }

    static Color WithAlpha(Color color, float alpha) => new Color(color.r, color.g, color.b, alpha);
}
#endif
