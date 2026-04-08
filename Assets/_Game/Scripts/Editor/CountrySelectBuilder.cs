#if UNITY_EDITOR
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public static class CountrySelectBuilder
{
    static readonly Color C_BG = Hex("#0C0F10");
    static readonly Color C_BG_ALT = Hex("#101416");
    static readonly Color C_SURFACE = Hex("#161A1C");
    static readonly Color C_CARD = Hex("#1C2023");
    static readonly Color C_CARD_HI = Hex("#222629");
    static readonly Color C_ACCENT = Hex("#FF9159");
    static readonly Color C_SECONDARY = Hex("#15A4FF");
    static readonly Color C_TEXT = Hex("#F8F9FC");
    static readonly Color C_TEXT_MUTED = Hex("#A9ABAE");
    static readonly Color C_TEXT_DIM = Hex("#737678");
    static readonly Color C_OUTLINE = Hex("#45484A");

    [MenuItem("Tools/Real Bus Sim/Build Country Select Scene")]
    public static void Build()
    {
        if (Object.FindObjectOfType<Canvas>() != null)
        {
            bool ok = EditorUtility.DisplayDialog(
                "Country Select Builder",
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

        GameObject topGlow = MakePanel("TopGlow", mainPanel.transform, WithAlpha(C_ACCENT, 0.08f));
        SetAnchor(topGlow, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f));
        RectOf(topGlow).anchoredPosition = new Vector2(0f, -12f);
        RectOf(topGlow).sizeDelta = new Vector2(780f, 84f);

        GameObject contentPanel = MakeRect("ContentPanel", mainPanel.transform);
        Stretch(contentPanel, 72, -72, -36, 36);

        GameObject topBar = MakeRect("TopBar", contentPanel.transform);
        SetAnchor(topBar, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0.5f, 1f));
        SetOffsetMinMax(topBar, new Vector2(0f, -92f), new Vector2(0f, 0f));

        GameObject topBarBg = MakePanel("BarSurface", topBar.transform, WithAlpha(C_BG_ALT, 0.82f));
        Stretch(topBarBg, 0, 0, 0, 0);

        GameObject brandGroup = MakeRect("BrandGroup", topBar.transform);
        SetAnchor(brandGroup, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 0.5f));
        RectOf(brandGroup).anchoredPosition = new Vector2(0f, -46f);
        RectOf(brandGroup).sizeDelta = new Vector2(480f, 54f);
        MakeTMP("Text_Brand", brandGroup.transform, "REAL BUS SIM", 28, FontStyles.Bold | FontStyles.Italic,
            C_ACCENT, new Vector2(360f, 54f), TextAlignmentOptions.MidlineLeft).characterSpacing = 8f;

        GameObject rightCluster = MakeRect("RightCluster", topBar.transform);
        SetAnchor(rightCluster, new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f, 0.5f));
        RectOf(rightCluster).anchoredPosition = new Vector2(0f, -46f);
        RectOf(rightCluster).sizeDelta = new Vector2(470f, 58f);
        HorizontalLayoutGroup clusterLayout = rightCluster.AddComponent<HorizontalLayoutGroup>();
        clusterLayout.spacing = 18f;
        clusterLayout.childAlignment = TextAnchor.MiddleRight;
        clusterLayout.childForceExpandHeight = false;
        clusterLayout.childForceExpandWidth = false;

        GameObject rankChip = MakeRect("RankChip", rightCluster.transform);
        LayoutElement rankLayout = rankChip.AddComponent<LayoutElement>();
        rankLayout.preferredWidth = 180f;
        rankLayout.preferredHeight = 58f;
        MakeTMP("Text_RankLabel", rankChip.transform, "GLOBAL RANK", 10, FontStyles.Bold,
            C_SECONDARY, new Vector2(180f, 20f), TextAlignmentOptions.TopRight).characterSpacing = 5f;
        MakeTMPAnchored("Text_RankValue", rankChip.transform, "#12", 22, FontStyles.Bold, C_ACCENT,
            new Vector2(180f, 30f), new Vector2(0f, 0f), new Vector2(1f, 0f),
            new Vector2(0f, 0f), TextAlignmentOptions.BottomRight).characterSpacing = 4f;

        GameObject currencyBadge = MakePanel("CurrencyBadge", rightCluster.transform, C_SURFACE);
        LayoutElement currencyLayout = currencyBadge.AddComponent<LayoutElement>();
        currencyLayout.preferredWidth = 220f;
        currencyLayout.preferredHeight = 48f;
        MakeTMP("Text_Cash", currencyBadge.transform, "$254,000", 20, FontStyles.Bold,
            C_ACCENT, new Vector2(220f, 48f), TextAlignmentOptions.Center).characterSpacing = 2f;

        GameObject heroBand = MakePanel("HeroBand", contentPanel.transform, C_CARD);
        SetAnchor(heroBand, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0.5f, 1f));
        SetOffsetMinMax(heroBand, new Vector2(0f, -272f), new Vector2(0f, -120f));

        GameObject heroTint = MakePanel("HeroTint", heroBand.transform, WithAlpha(C_BG, 0.34f));
        Stretch(heroTint, 0, 0, 0, 0);

        GameObject heroContent = MakeRect("HeroContent", heroBand.transform);
        Stretch(heroContent, 36, -36, -28, 28);

        GameObject badge = MakePanel("ModeBadge", heroContent.transform, WithAlpha(C_SECONDARY, 0.14f));
        SetAnchor(badge, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f));
        RectOf(badge).anchoredPosition = Vector2.zero;
        RectOf(badge).sizeDelta = new Vector2(190f, 32f);
        MakeTMP("Text_Mode", badge.transform, "REGION NETWORK", 11, FontStyles.Bold,
            C_SECONDARY, new Vector2(190f, 32f), TextAlignmentOptions.Center).characterSpacing = 4f;

        TextMeshProUGUI titleText = MakeTMPAnchored("Text_Title", heroContent.transform, "SELECT COUNTRY",
            42, FontStyles.Bold, C_TEXT, new Vector2(720f, 54f),
            new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 54f), TextAlignmentOptions.BottomLeft);
        titleText.characterSpacing = 8f;

        Image accentLine = MakePanel("AccentLine", heroContent.transform, C_ACCENT).GetComponent<Image>();
        RectTransform accentRect = accentLine.rectTransform;
        accentRect.anchorMin = new Vector2(0f, 0f);
        accentRect.anchorMax = new Vector2(0f, 0f);
        accentRect.pivot = new Vector2(0f, 0f);
        accentRect.anchoredPosition = new Vector2(0f, 34f);
        accentRect.sizeDelta = new Vector2(84f, 4f);

        TextMeshProUGUI subtitleText = MakeTMPAnchored("Text_Subtitle", heroContent.transform,
            "Choose a region and discover its city network.",
            20, FontStyles.Normal, C_TEXT_MUTED, new Vector2(760f, 32f),
            new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), TextAlignmentOptions.BottomLeft);

        GameObject infoRail = MakeRect("InfoRail", heroContent.transform);
        SetAnchor(infoRail, new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f, 0.5f));
        RectOf(infoRail).anchoredPosition = new Vector2(0f, 0f);
        RectOf(infoRail).sizeDelta = new Vector2(430f, 82f);
        HorizontalLayoutGroup infoLayout = infoRail.AddComponent<HorizontalLayoutGroup>();
        infoLayout.spacing = 18f;
        infoLayout.childAlignment = TextAnchor.MiddleRight;
        infoLayout.childForceExpandHeight = false;
        infoLayout.childForceExpandWidth = false;

        CreateInfoTile(infoRail.transform, "DISCOVER", "11 COUNTRIES", C_TEXT);
        CreateInfoTile(infoRail.transform, "EXPLORE", "24+ CITIES", C_SECONDARY);
        CreateInfoTile(infoRail.transform, "STATUS", "READY", C_ACCENT);

        GameObject listShell = MakePanel("ListShell", contentPanel.transform, WithAlpha(C_CARD, 0.96f));
        SetAnchor(listShell, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector2(0.5f, 0.5f));
        SetOffsetMinMax(listShell, new Vector2(0f, 112f), new Vector2(0f, -304f));

        GameObject listHeader = MakeRect("ListHeader", listShell.transform);
        SetAnchor(listHeader, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0.5f, 1f));
        SetOffsetMinMax(listHeader, new Vector2(28f, -54f), new Vector2(-28f, -16f));
        MakeTMP("Text_ListHeader", listHeader.transform, "AVAILABLE REGIONS", 14, FontStyles.Bold,
            C_TEXT_MUTED, new Vector2(300f, 38f), TextAlignmentOptions.MidlineLeft).characterSpacing = 4f;
        MakeTMPAnchored("Text_ListHint", listHeader.transform, "Tap a country card to continue",
            13, FontStyles.Normal, C_TEXT_DIM, new Vector2(340f, 28f),
            new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0f, 0f), TextAlignmentOptions.MidlineRight);

        GameObject scrollView = MakeRect("CountryScrollView", listShell.transform);
        SetAnchor(scrollView, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector2(0.5f, 0.5f));
        SetOffsetMinMax(scrollView, new Vector2(24f, 24f), new Vector2(-24f, -72f));
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

        GameObject content = MakeRect("Content", viewport.transform);
        RectTransform contentRect = RectOf(content);
        contentRect.anchorMin = new Vector2(0f, 1f);
        contentRect.anchorMax = new Vector2(1f, 1f);
        contentRect.pivot = new Vector2(0.5f, 1f);
        contentRect.anchoredPosition = Vector2.zero;
        contentRect.sizeDelta = new Vector2(0f, 0f);

        GridLayoutGroup grid = content.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(195f, 248f);
        grid.spacing = new Vector2(22f, 22f);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 3;
        grid.childAlignment = TextAnchor.UpperCenter;
        grid.startAxis = GridLayoutGroup.Axis.Horizontal;
        grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
        grid.padding = new RectOffset(0, 0, 4, 12);

        ContentSizeFitter fitter = content.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

        scrollRect.viewport = viewportRect;
        scrollRect.content = contentRect;

        GameObject footerBar = MakeRect("FooterBar", contentPanel.transform);
        SetAnchor(footerBar, new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(0.5f, 0f));
        SetOffsetMinMax(footerBar, new Vector2(0f, 0f), new Vector2(0f, 88f));

        GameObject footerLine = MakePanel("FooterLine", footerBar.transform, WithAlpha(C_OUTLINE, 0.45f));
        SetAnchor(footerLine, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0.5f, 1f));
        RectOf(footerLine).anchoredPosition = Vector2.zero;
        RectOf(footerLine).sizeDelta = new Vector2(0f, 1f);

        GameObject btnBack = MakePanel("Btn_Back", footerBar.transform, C_SURFACE);
        SetAnchor(btnBack, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 0.5f));
        RectOf(btnBack).anchoredPosition = new Vector2(0f, 42f);
        RectOf(btnBack).sizeDelta = new Vector2(260f, 54f);
        Button backButton = btnBack.AddComponent<Button>();
        backButton.navigation = new Navigation { mode = Navigation.Mode.None };
        TextMeshProUGUI backButtonText = MakeTMP("Text", btnBack.transform, "BACK", 18, FontStyles.Bold,
            C_TEXT_MUTED, new Vector2(260f, 54f), TextAlignmentOptions.Center);
        backButtonText.characterSpacing = 4f;

        MakeTMPAnchored("Text_FooterHint", footerBar.transform, "SceneBootstrap returns to MainMenu if managers are missing",
            13, FontStyles.Normal, C_TEXT_DIM, new Vector2(540f, 24f),
            new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0f, 42f), TextAlignmentOptions.MidlineRight);

        if (Object.FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem");
            es.AddComponent<UnityEngine.EventSystems.EventSystem>();
            AddBestInputModule(es);
        }

        CountrySelectUI ui = canvasGO.AddComponent<CountrySelectUI>();
        ui.canvasGroup = canvasGroup;
        ui.contentPanel = RectOf(contentPanel);
        ui.titleText = titleText;
        ui.subtitleText = subtitleText;
        ui.accentLine = accentLine;
        ui.countryListContainer = content.transform;
        ui.backButton = backButton;
        ui.backButtonText = backButtonText;
        ui.backgroundPanel = mainPanel.GetComponent<Image>();

        canvasGO.AddComponent<SceneBootstrap>();

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        Debug.Log("[CountrySelectBuilder] Scene built. Assign the scene name to CountrySelect and verify CountrySelectUI runtime styling in Play Mode.");
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

    static void CreateInfoTile(Transform parent, string label, string value, Color color)
    {
        GameObject tile = MakePanel($"Info_{label}", parent, WithAlpha(C_BG, 0.3f));
        LayoutElement layout = tile.AddComponent<LayoutElement>();
        layout.preferredWidth = 132f;
        layout.preferredHeight = 82f;
        MakeTMPAnchored("Text_Label", tile.transform, label, 10, FontStyles.Bold, C_TEXT_DIM,
            new Vector2(120f, 18f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f),
            new Vector2(0f, -10f), TextAlignmentOptions.Top);
        MakeTMPAnchored("Text_Value", tile.transform, value, 18, FontStyles.Bold, color,
            new Vector2(120f, 34f), new Vector2(0.5f, 0f), new Vector2(0.5f, 0f),
            new Vector2(0f, 10f), TextAlignmentOptions.Bottom);
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
