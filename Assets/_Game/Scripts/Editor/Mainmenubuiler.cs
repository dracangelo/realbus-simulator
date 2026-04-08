// MainMenuBuilder.cs
// Place this file in any Editor/ folder in your project.
// Usage: Unity menu → Tools → Real Bus Sim → Build Main Menu Scene

#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;

public static class MainMenuBuilder
{
    // ─── Palette ────────────────────────────────────────────────────────────────
    static readonly Color C_BG          = Hex("#0C0F10");
    static readonly Color C_CARD        = Hex("#1C2023");
    static readonly Color C_STAT        = Hex("#222629");
    static readonly Color C_CURRENCY_BG = Hex("#161A1C");
    static readonly Color C_ACCENT      = Hex("#FF9159");
    static readonly Color C_TEXT_LIGHT  = Hex("#F8F9FC");
    static readonly Color C_TEXT_MUTED  = Hex("#A9ABAE");
    static readonly Color C_TEXT_DIM    = Hex("#737678");
    static readonly Color C_XP_TRACK    = Hex("#282D30");

    // ─── Entry point ────────────────────────────────────────────────────────────
    [MenuItem("Tools/Real Bus Sim/Build Main Menu Scene")]
    public static void Build()
    {
        // Warn if the scene already has a Canvas
        if (Object.FindObjectOfType<Canvas>() != null)
        {
            bool ok = EditorUtility.DisplayDialog(
                "Main Menu Builder",
                "A Canvas already exists in the scene.\nProceed and add the hierarchy anyway?",
                "Yes, add it", "Cancel");
            if (!ok) return;
        }

        // ── GameManagers root ─────────────────────────────────────────────────
        GameObject managers = new GameObject("GameManagers");
        AddDontDestroy<SceneLoaderStub>(managers);   // swap with your real type
        AddDontDestroy<GameStateStub>(managers);
        AddDontDestroy<CityManagerStub>(managers);
        AddDontDestroy<UIFontsStub>(managers);

        // ── Canvas ────────────────────────────────────────────────────────────
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 0;

        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode            = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution    = new Vector2(1920, 1080);
        scaler.screenMatchMode        = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight     = 0.5f;

        canvasGO.AddComponent<GraphicRaycaster>();

        // ── MainMenuPanel ─────────────────────────────────────────────────────
        GameObject mainMenuPanel = MakePanel("MainMenuPanel", canvasGO.transform, C_BG);
        mainMenuPanel.AddComponent<CanvasGroup>();
        Stretch(mainMenuPanel, 0, 0, 0, 0);

        // ── RootPanel ─────────────────────────────────────────────────────────
        GameObject rootPanel = MakeRect("RootPanel", mainMenuPanel.transform);
        Stretch(rootPanel, 56, -56, -32, 32);

        // ── TopBar ────────────────────────────────────────────────────────────
        GameObject topBar = MakeRect("TopBar", rootPanel.transform);
        SetAnchor(topBar, new Vector2(0, 1), new Vector2(1, 1), new Vector2(0.5f, 1));
        SetOffsetMinMax(topBar, new Vector2(0, -88), new Vector2(0, 0));

        // BrandGroup
        GameObject brandGroup = MakeRect("BrandGroup", topBar.transform);
        SetAnchor(brandGroup, new Vector2(0, 0.5f), new Vector2(0, 0.5f), new Vector2(0, 0.5f));
        RectOf(brandGroup).anchoredPosition = new Vector2(0, -44);
        RectOf(brandGroup).sizeDelta        = new Vector2(460, 64);

        TextMeshProUGUI titleText = MakeTMP("Text_Title", brandGroup.transform, "REAL BUS SIM", 32, FontStyles.Bold,
                C_ACCENT, new Vector2(460, 64), TextAlignmentOptions.MidlineLeft);

        // NavLinks
        GameObject navLinks = MakeRect("NavLinks", topBar.transform);
        SetAnchor(navLinks, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
        RectOf(navLinks).anchoredPosition = new Vector2(0, -44);
        RectOf(navLinks).sizeDelta        = new Vector2(760, 56);
        HorizontalLayoutGroup hlg = navLinks.AddComponent<HorizontalLayoutGroup>();
        hlg.spacing            = 38;
        hlg.childAlignment     = TextAnchor.MiddleCenter;
        hlg.childForceExpandWidth  = false;
        hlg.childForceExpandHeight = false;

        TextMeshProUGUI navDriveText = AddNavLink(navLinks.transform, "Text_Drive",  "DRIVE",  130, C_ACCENT);
        TextMeshProUGUI navGarageText = AddNavLink(navLinks.transform, "Text_Garage", "GARAGE", 160, WithAlpha(C_TEXT_LIGHT, 0.6f));
        TextMeshProUGUI navRoutesText = AddNavLink(navLinks.transform, "Text_Routes", "ROUTES", 160, WithAlpha(C_TEXT_LIGHT, 0.6f));
        TextMeshProUGUI navMarketText = AddNavLink(navLinks.transform, "Text_Market", "MARKET", 160, WithAlpha(C_TEXT_LIGHT, 0.6f));

        // CurrencyBadge
        GameObject currencyBadge = MakePanel("CurrencyBadge", topBar.transform, C_CURRENCY_BG);
        SetAnchor(currencyBadge, new Vector2(1, 0.5f), new Vector2(1, 0.5f), new Vector2(1, 0.5f));
        RectOf(currencyBadge).anchoredPosition = new Vector2(0, -44);
        RectOf(currencyBadge).sizeDelta        = new Vector2(240, 54);
        TextMeshProUGUI currencyText = MakeTMP("Text_Cash", currencyBadge.transform, "$254,000", 22, FontStyles.Bold,
                C_ACCENT, new Vector2(240, 54), TextAlignmentOptions.Center);

        // ── HeroCard ─────────────────────────────────────────────────────────
        GameObject heroCard = MakePanel("HeroCard", rootPanel.transform, C_CARD);
        SetAnchor(heroCard, new Vector2(0, 1), new Vector2(1, 1), new Vector2(0.5f, 1));
        SetOffsetMinMax(heroCard, new Vector2(0, -542), new Vector2(0, -112));

        // HeroBusImage — assign your bus artwork in the Inspector
        GameObject heroBusImage = MakePanel("HeroBusImage", heroCard.transform, Color.clear);
        Stretch(heroBusImage, 0, 0, 0, 0);
        Image busImg = heroBusImage.GetComponent<Image>();
        busImg.color = Color.white;     // tint white so artwork shows true colour

        // HeroOverlay
        GameObject heroOverlay = MakePanel("HeroOverlay", heroCard.transform, WithAlpha(C_BG, 0.58f));
        Stretch(heroOverlay, 0, 0, 0, 0);

        // HeroContent
        GameObject heroContent = MakeRect("HeroContent", heroCard.transform);
        Stretch(heroContent, 44, -44, -40, 40);

        // StatusChip
        GameObject statusChip = MakePanel("StatusChip", heroContent.transform,
                                          WithAlpha(C_ACCENT, 0.18f));
        SetAnchor(statusChip, new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1));
        RectOf(statusChip).anchoredPosition = Vector2.zero;
        RectOf(statusChip).sizeDelta        = new Vector2(280, 40);
        TextMeshProUGUI statusChipText = MakeTMP("Text_Status", statusChip.transform, "CURRENTLY EQUIPPED", 13, FontStyles.Bold,
                C_ACCENT, new Vector2(280, 40), TextAlignmentOptions.Center);

        // HeroTitle
        TextMeshProUGUI heroTitleText = MakeTMPAnchored("Text_HeroTitle", heroContent.transform, "VOLTA S-SERIES",
                         64, FontStyles.Bold | FontStyles.Italic, C_TEXT_LIGHT,
                         new Vector2(760, 90), new Vector2(0, 0), new Vector2(0, 0),
                         new Vector2(0, 122), TextAlignmentOptions.BottomLeft);

        // HeroDesc
        TextMeshProUGUI heroDescText = MakeTMPAnchored("Text_HeroDesc", heroContent.transform, "Electric articulated city bus · 420 km range",
                         21, FontStyles.Normal, C_TEXT_MUTED,
                         new Vector2(700, 52), new Vector2(0, 0), new Vector2(0, 0),
                         new Vector2(0, 66), TextAlignmentOptions.BottomLeft);

        // Btn_Play
        GameObject btnPlay = MakeButton("Btn_Play", heroContent.transform,
                                        "START DRIVING", C_ACCENT, C_BG,
                                        new Vector2(340, 72), new Vector2(0, 0),
                                        new Vector2(0, 0), new Vector2(0, 0));

        // Btn_Customize
        GameObject btnCustomize = MakeButton("Btn_Customize", heroContent.transform,
                                             "CUSTOMIZE", WithAlpha(Hex("#222629"), 0.88f), C_TEXT_LIGHT,
                                             new Vector2(270, 72), new Vector2(0, 0),
                                             new Vector2(0, 0), new Vector2(360, 0));

        // ── StatsGrid ─────────────────────────────────────────────────────────
        GameObject statsGrid = MakeRect("StatsGrid", rootPanel.transform);
        SetAnchor(statsGrid, new Vector2(0, 0), new Vector2(1, 1), new Vector2(0.5f, 0.5f));
        SetOffsetMinMax(statsGrid, new Vector2(0, 0), new Vector2(0, -566));

        // GaragePanel
        GameObject garagePanel = MakePanel("GaragePanel", statsGrid.transform, C_CARD);
        SetAnchor(garagePanel, new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 0.5f));
        RectOf(garagePanel).anchoredPosition = Vector2.zero;
        RectOf(garagePanel).sizeDelta        = new Vector2(1180, 0);

        MakeTMPAnchored("Text_GarageTitle", garagePanel.transform, "MY GARAGE",
                         26, FontStyles.Bold, C_TEXT_LIGHT,
                         new Vector2(340, 36), new Vector2(0, 1), new Vector2(0, 1),
                         new Vector2(32, -28), TextAlignmentOptions.Left);

        MakeTMPAnchored("Text_GarageSubtitle", garagePanel.transform, "Technical Performance Analysis",
                         17, FontStyles.Normal, C_TEXT_MUTED,
                         new Vector2(460, 26), new Vector2(0, 1), new Vector2(0, 1),
                         new Vector2(32, -64), TextAlignmentOptions.Left);

        // Stat cards  (name, label, value, fillPct, fillColor, anchorPivot, pos)
        BuildStatCard(garagePanel, "Stat_Battery",  "BATTERY RANGE", "420 KM", 0.85f, Hex("#4A9EFF"),
                      true,  new Vector2(32, -116));
        BuildStatCard(garagePanel, "Stat_Capacity", "MAX CAPACITY",  "85 PAX", 0.70f, Hex("#FFD84A"),
                      false, new Vector2(-32, -116));
        BuildStatCard(garagePanel, "Stat_Wear",     "WEAR LEVEL",    "12%",    0.12f, Hex("#FF4A4A"),
                      true,  new Vector2(32, -252));
        BuildStatCard(garagePanel, "Stat_Speed",    "TOP SPEED",     "115 KPH",0.92f, C_ACCENT,
                      false, new Vector2(-32, -252));

        // CareerPanel
        GameObject careerPanel = MakePanel("CareerPanel", statsGrid.transform, C_CARD);
        SetAnchor(careerPanel, new Vector2(1, 0), new Vector2(1, 1), new Vector2(1, 0.5f));
        RectOf(careerPanel).anchoredPosition = Vector2.zero;
        RectOf(careerPanel).sizeDelta        = new Vector2(572, 0);

        MakeTMPAnchored("Text_CareerTitle", careerPanel.transform, "CAREER",
                         26, FontStyles.Bold, C_TEXT_LIGHT,
                         new Vector2(220, 36), new Vector2(0, 1), new Vector2(0, 1),
                         new Vector2(32, -28), TextAlignmentOptions.Left);

        MakeTMPAnchored("Text_CareerSubtitle", careerPanel.transform, "Driver Level & Experience",
                         17, FontStyles.Normal, C_TEXT_MUTED,
                         new Vector2(300, 48), new Vector2(0, 1), new Vector2(0, 1),
                         new Vector2(32, -64), TextAlignmentOptions.Left);

        // Level number
        MakeTMPAnchored("Text_LevelValue", careerPanel.transform, "24",
                         68, FontStyles.Bold, C_TEXT_LIGHT,
                         new Vector2(180, 82), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
                         new Vector2(0, 8), TextAlignmentOptions.Center);

        // XP bar
        GameObject xpBg = MakePanel("XPBar_BG", careerPanel.transform, C_XP_TRACK);
        SetAnchor(xpBg, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
        RectOf(xpBg).anchoredPosition = new Vector2(0, -62);
        RectOf(xpBg).sizeDelta        = new Vector2(300, 10);

        GameObject xpFill = MakePanel("XPBar_Fill", xpBg.transform, C_ACCENT);
        RectTransform xpFillRT = RectOf(xpFill);
        xpFillRT.anchorMin = new Vector2(0, 0);
        xpFillRT.anchorMax = new Vector2(0.84f, 1);
        xpFillRT.offsetMin = Vector2.zero;
        xpFillRT.offsetMax = Vector2.zero;

        MakeTMPAnchored("Text_XPValue", careerPanel.transform, "8,420 / 10,000 XP",
                         18, FontStyles.Normal, C_TEXT_MUTED,
                         new Vector2(300, 28), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
                         new Vector2(0, -92), TextAlignmentOptions.Center);

        // ── Version label ─────────────────────────────────────────────────────
        MakeTMPAnchored("Text_Version", rootPanel.transform, "v0.1.0 — Early Access",
                         16, FontStyles.Normal, C_TEXT_DIM,
                         new Vector2(280, 24), new Vector2(1, 0), new Vector2(1, 0),
                         new Vector2(0, 0), TextAlignmentOptions.BottomRight);

        // ── EventSystem ───────────────────────────────────────────────────────
        if (Object.FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem");
            es.AddComponent<UnityEngine.EventSystems.EventSystem>();
            AddBestInputModule(es);
        }

        // Wire responsive runtime controller so UI adapts to phone/tablet screens.
        MainMenuUI ui = mainMenuPanel.AddComponent<MainMenuUI>();
        ui.canvasGroup = mainMenuPanel.GetComponent<CanvasGroup>();
        ui.rootPanel = RectOf(rootPanel);
        ui.topBar = RectOf(topBar);
        ui.heroCard = RectOf(heroCard);
        ui.statsGrid = RectOf(statsGrid);
        ui.backgroundPanel = mainMenuPanel.GetComponent<Image>();
        ui.heroCardBackground = heroCard.GetComponent<Image>();
        ui.heroGradientOverlay = heroOverlay.GetComponent<Image>();
        ui.titleText = titleText;
        ui.navDriveText = navDriveText;
        ui.navGarageText = navGarageText;
        ui.navRoutesText = navRoutesText;
        ui.navMarketText = navMarketText;
        ui.currencyBadge = currencyBadge.GetComponent<Image>();
        ui.currencyText = currencyText;
        ui.statusChipText = statusChipText;
        ui.statusChipBackground = statusChip.GetComponent<Image>();
        ui.heroTitleText = heroTitleText;
        ui.heroDescriptionText = heroDescText;
        ui.playButton = btnPlay.GetComponent<Button>();
        ui.playButtonText = FindTMP(btnPlay.transform, "Text");
        ui.customizeButton = btnCustomize.GetComponent<Button>();
        ui.customizeButtonText = FindTMP(btnCustomize.transform, "Text");
        ui.garagePanel = garagePanel.GetComponent<Image>();
        ui.careerPanel = careerPanel.GetComponent<Image>();
        ui.versionText = FindTMP(rootPanel.transform, "Text_Version");

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        Debug.Log("[MainMenuBuilder] Scene built. Wire up MainMenuUI component references manually, then assign your bus artwork to HeroBusImage.");
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

    // ─── Stat card helper ────────────────────────────────────────────────────────
    static void BuildStatCard(GameObject parent, string id, string label, string value,
                               float fillPct, Color fillColor, bool anchorLeft, Vector2 pos)
    {
        GameObject card = MakePanel(id, parent.transform, C_STAT);
        Vector2 anchorX = anchorLeft ? new Vector2(0, 1) : new Vector2(1, 1);
        SetAnchor(card, anchorX, anchorX, anchorX);
        RectOf(card).anchoredPosition = pos;
        RectOf(card).sizeDelta        = new Vector2(532, 120);

        MakeTMPAnchored("Text_Label", card.transform, label,
                         12, FontStyles.Bold, C_TEXT_MUTED,
                         new Vector2(420, 22), new Vector2(0, 1), new Vector2(0, 1),
                         new Vector2(24, -20), TextAlignmentOptions.Left);

        MakeTMPAnchored("Text_Value", card.transform, value,
                         34, FontStyles.Bold, C_TEXT_LIGHT,
                         new Vector2(420, 48), new Vector2(0, 1), new Vector2(0, 1),
                         new Vector2(24, -46), TextAlignmentOptions.Left);

        GameObject barBg = MakePanel("Bar_BG", card.transform, C_XP_TRACK);
        SetAnchor(barBg, new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1));
        RectOf(barBg).anchoredPosition = new Vector2(24, -100);
        RectOf(barBg).sizeDelta        = new Vector2(420, 10);

        GameObject barFill = MakePanel("Bar_Fill", barBg.transform, fillColor);
        RectTransform fillRT = RectOf(barFill);
        fillRT.anchorMin = new Vector2(0, 0);
        fillRT.anchorMax = new Vector2(fillPct, 1);
        fillRT.offsetMin = Vector2.zero;
        fillRT.offsetMax = Vector2.zero;
    }

    // ─── Nav link helper ─────────────────────────────────────────────────────────
    static TextMeshProUGUI AddNavLink(Transform parent, string goName, string text, float width, Color color)
    {
        GameObject go = new GameObject(goName);
        go.transform.SetParent(parent, false);
        TextMeshProUGUI tmp = go.AddComponent<TextMeshProUGUI>();
        tmp.text      = text;
        tmp.fontSize  = 18;
        tmp.fontStyle = FontStyles.Bold;
        tmp.color     = color;
        tmp.alignment = TextAlignmentOptions.Center;
        LayoutElement le = go.AddComponent<LayoutElement>();
        le.preferredWidth  = width;
        le.preferredHeight = 56;
        return tmp;
    }

    static TextMeshProUGUI FindTMP(Transform parent, string childName)
    {
        Transform child = parent.Find(childName);
        if (!child) return null;
        return child.GetComponent<TextMeshProUGUI>();
    }

    // ─── Button helper ───────────────────────────────────────────────────────────
    static GameObject MakeButton(string name, Transform parent, string label,
                                  Color bgColor, Color textColor, Vector2 size,
                                  Vector2 anchorMin, Vector2 anchorMax, Vector2 pos)
    {
        GameObject go = MakePanel(name, parent, bgColor);
        SetAnchor(go, anchorMin, anchorMax, new Vector2(0, 0));
        RectOf(go).anchoredPosition = pos;
        RectOf(go).sizeDelta        = size;
        go.AddComponent<Button>().navigation = new Navigation { mode = Navigation.Mode.None };
        MakeTMP("Text", go.transform, label, 22, FontStyles.Bold,
                textColor, size, TextAlignmentOptions.Center);
        return go;
    }

    // ─── Primitive factories ─────────────────────────────────────────────────────
    static GameObject MakeRect(string name, Transform parent)
    {
        GameObject go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        return go;
    }

    static GameObject MakePanel(string name, Transform parent, Color color)
    {
        GameObject go = MakeRect(name, parent);
        Image img = go.AddComponent<Image>();
        img.color = color;
        return go;
    }

    static TextMeshProUGUI MakeTMP(string name, Transform parent, string text,
                                    float size, FontStyles style, Color color,
                                    Vector2 sizeDelta, TextAlignmentOptions align)
    {
        GameObject go = MakeRect(name, parent);
        RectOf(go).sizeDelta = sizeDelta;
        TextMeshProUGUI tmp = go.AddComponent<TextMeshProUGUI>();
        tmp.text      = text;
        tmp.fontSize  = size;
        tmp.fontStyle = style;
        tmp.color     = color;
        tmp.alignment = align;
        return tmp;
    }

    static TextMeshProUGUI MakeTMPAnchored(string name, Transform parent, string text,
                                            float size, FontStyles style, Color color,
                                            Vector2 sizeDelta, Vector2 anchorMin, Vector2 anchorMax,
                                            Vector2 pos, TextAlignmentOptions align)
    {
        TextMeshProUGUI tmp = MakeTMP(name, parent, text, size, style, color, sizeDelta, align);
        RectTransform rt = tmp.rectTransform;
        rt.anchorMin        = anchorMin;
        rt.anchorMax        = anchorMax;
        rt.pivot            = anchorMin;     // pivot matches anchor by convention
        rt.anchoredPosition = pos;
        rt.sizeDelta        = sizeDelta;
        return tmp;
    }

    // ─── RectTransform helpers ───────────────────────────────────────────────────
    static RectTransform RectOf(GameObject go) => go.GetComponent<RectTransform>();

    /// <summary>Full stretch with offsets (left, right, top, bottom).</summary>
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
        rt.pivot     = pivot;
    }

    static void SetOffsetMinMax(GameObject go, Vector2 min, Vector2 max)
    {
        RectTransform rt = RectOf(go);
        rt.offsetMin = min;
        rt.offsetMax = max;
    }

    // ─── Color helpers ───────────────────────────────────────────────────────────
    static Color Hex(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color c);
        return c;
    }

    static Color WithAlpha(Color c, float a) => new Color(c.r, c.g, c.b, a);

    // ─── Stub component adder (swappable with your real types) ───────────────────
    static T AddDontDestroy<T>(GameObject go) where T : MonoBehaviour
    {
        T comp = go.AddComponent<T>();
        // DontDestroyOnLoad is runtime-only; the flag is set by each manager itself at Awake.
        return comp;
    }
}

// ── Stub types — DELETE these once you point the builder at your real scripts ──
// These exist only so the file compiles before your actual managers are present.

public class SceneLoaderStub  : MonoBehaviour {}
public class GameStateStub    : MonoBehaviour {}
public class CityManagerStub  : MonoBehaviour {}
public class UIFontsStub      : MonoBehaviour {}
#endif
