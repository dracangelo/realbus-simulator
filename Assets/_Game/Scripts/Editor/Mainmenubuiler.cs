// MainMenuBuilder.cs
// Usage: Unity menu -> Tools -> Real Bus Sim -> Build Main Menu Scene

#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public static class MainMenuBuilder
{
    static readonly Color C_BG        = Hex("#0C0F10");
    static readonly Color C_CARD      = Hex("#1C2023");
    static readonly Color C_SURFACE   = Hex("#161A1C");
    static readonly Color C_STAT      = Hex("#222629");
    static readonly Color C_ACCENT    = Hex("#FF9159");
    static readonly Color C_SECONDARY = Hex("#15A4FF");
    static readonly Color C_SUCCESS   = Hex("#34D399");
    static readonly Color C_TEXT_LIGHT = Hex("#F8F9FC");
    static readonly Color C_TEXT_MUTED = Hex("#A9ABAE");
    static readonly Color C_TEXT_DIM   = Hex("#737678");
    static readonly Color C_XP_TRACK   = Hex("#282D30");
    static readonly Color C_DIVIDER    = Hex("#282D30");

    [MenuItem("Tools/Real Bus Sim/Build Main Menu Scene")]
    public static void Build()
    {
        if (Object.FindObjectOfType<Canvas>() != null)
        {
            bool ok = EditorUtility.DisplayDialog(
                "Main Menu Builder",
                "A Canvas already exists in the scene.\nProceed and add the hierarchy anyway?",
                "Yes, add it", "Cancel");
            if (!ok) return;
        }

        GameObject managersRoot = FindOrCreateRoot("GameManagers");
        SceneLoader  sceneLoader  = FindOrCreateManager<SceneLoader>(managersRoot.transform,  "SceneLoader");
        GameState    gameState    = FindOrCreateManager<GameState>(managersRoot.transform,    "GameState");
        CityManager  cityManager  = FindOrCreateManager<CityManager>(managersRoot.transform, "CityManager");
        UIFonts      uiFonts      = FindOrCreateManager<UIFonts>(managersRoot.transform,     "UIFonts");

        AssignFonts(uiFonts);
        SeedCityAndSelectionData(cityManager, gameState);

        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode  = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 0;

        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode        = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.screenMatchMode     = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight  = 0.5f;

        canvasGO.AddComponent<GraphicRaycaster>();

        GameObject mainMenuPanel = MakePanel("MainMenuPanel", canvasGO.transform, C_BG);
        mainMenuPanel.AddComponent<CanvasGroup>();
        Stretch(mainMenuPanel, 0f, 0f, 0f, 0f);

        GameObject rootPanel = MakeRect("RootPanel", mainMenuPanel.transform);
        Stretch(rootPanel, 56f, -56f, -32f, 32f);

        // ── Top Bar ──────────────────────────────────────────────────────────
        GameObject topBar = MakeRect("TopBar", rootPanel.transform);
        SetAnchor(topBar, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0.5f, 1f));
        SetOffsetMinMax(topBar, new Vector2(0f, -88f), new Vector2(0f, 0f));

        GameObject brandGroup = MakeRect("BrandGroup", topBar.transform);
        SetAnchor(brandGroup, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 0.5f));
        RectOf(brandGroup).anchoredPosition = new Vector2(0f, -44f);
        RectOf(brandGroup).sizeDelta        = new Vector2(460f, 64f);
        TextMeshProUGUI titleText = MakeTMP(
            "Text_Title", brandGroup.transform, "REAL BUS SIM", 32f, FontStyles.Bold,
            C_ACCENT, new Vector2(460f, 64f), TextAlignmentOptions.MidlineLeft);

        GameObject navLinks = MakeRect("NavLinks", topBar.transform);
        SetAnchor(navLinks, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
        RectOf(navLinks).anchoredPosition = new Vector2(0f, -44f);
        RectOf(navLinks).sizeDelta        = new Vector2(760f, 56f);
        HorizontalLayoutGroup navLayout = navLinks.AddComponent<HorizontalLayoutGroup>();
        navLayout.spacing              = 38f;
        navLayout.childAlignment       = TextAnchor.MiddleCenter;
        navLayout.childForceExpandWidth  = false;
        navLayout.childForceExpandHeight = false;

        TextMeshProUGUI navDriveText   = AddNavLink(navLinks.transform, "Text_Drive",  "DRIVE",  130f, C_ACCENT);
        TextMeshProUGUI navGarageText  = AddNavLink(navLinks.transform, "Text_Garage", "GARAGE", 160f, WithAlpha(C_TEXT_LIGHT, 0.6f));
        TextMeshProUGUI navRoutesText  = AddNavLink(navLinks.transform, "Text_Routes", "COUNTRY", 160f, WithAlpha(C_TEXT_LIGHT, 0.6f));
        TextMeshProUGUI navMarketText  = AddNavLink(navLinks.transform, "Text_Market", "MARKET", 160f, WithAlpha(C_TEXT_LIGHT, 0.6f));

        GameObject currencyBadge = MakePanel("CurrencyBadge", topBar.transform, C_SURFACE);
        SetAnchor(currencyBadge, new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f, 0.5f));
        RectOf(currencyBadge).anchoredPosition = new Vector2(0f, -44f);
        RectOf(currencyBadge).sizeDelta        = new Vector2(260f, 54f);
        TextMeshProUGUI currencyText = MakeTMP(
            "Text_Cash", currencyBadge.transform, "KES 254,000", 22f, FontStyles.Bold,
            C_ACCENT, new Vector2(260f, 54f), TextAlignmentOptions.Center);

        // ── Hero Card ────────────────────────────────────────────────────────
        GameObject heroCard = MakePanel("HeroCard", rootPanel.transform, C_CARD);
        SetAnchor(heroCard, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0.5f, 1f));
        SetOffsetMinMax(heroCard, new Vector2(0f, -542f), new Vector2(0f, -112f));

        GameObject heroBusImage = MakePanel("HeroBusImage", heroCard.transform, Color.clear);
        Stretch(heroBusImage, 0f, 0f, 0f, 0f);
        Image heroBusArtwork = heroBusImage.GetComponent<Image>();
        heroBusArtwork.color          = Color.white;
        heroBusArtwork.preserveAspect = false;

        GameObject heroOverlay = MakePanel("HeroOverlay", heroCard.transform, WithAlpha(C_BG, 0.58f));
        Stretch(heroOverlay, 0f, 0f, 0f, 0f);

        GameObject heroContent = MakeRect("HeroContent", heroCard.transform);
        Stretch(heroContent, 44f, -44f, -40f, 40f);

        GameObject statusChip = MakePanel("StatusChip", heroContent.transform, WithAlpha(C_ACCENT, 0.18f));
        SetAnchor(statusChip, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f));
        RectOf(statusChip).anchoredPosition = Vector2.zero;
        RectOf(statusChip).sizeDelta        = new Vector2(340f, 40f);
        TextMeshProUGUI statusChipText = MakeTMP(
            "Text_Status", statusChip.transform, "CURRENTLY EQUIPPED", 13f, FontStyles.Bold,
            C_ACCENT, new Vector2(340f, 40f), TextAlignmentOptions.Center);

        TextMeshProUGUI heroTitleText = MakeTMPAnchored(
            "Text_HeroTitle", heroContent.transform, "FLEET READY", 64f, FontStyles.Bold | FontStyles.Italic, C_TEXT_LIGHT,
            new Vector2(760f, 90f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 122f), TextAlignmentOptions.BottomLeft);

        TextMeshProUGUI heroDescText = MakeTMPAnchored(
            "Text_HeroDesc", heroContent.transform, "Choose the right vehicle for the route, the crowd, and the shift.",
            21f, FontStyles.Normal, C_TEXT_MUTED, new Vector2(820f, 60f),
            new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 66f), TextAlignmentOptions.BottomLeft);

        GameObject btnPlay = MakeButton(
            "Btn_Play", heroContent.transform, "START DRIVING", C_ACCENT, C_BG,
            new Vector2(340f, 72f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f));

        GameObject btnCustomize = MakeButton(
            "Btn_Customize", heroContent.transform, "OPEN GARAGE", WithAlpha(C_STAT, 0.88f), C_TEXT_LIGHT,
            new Vector2(270f, 72f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(360f, 0f));

        // ── Stats Grid ───────────────────────────────────────────────────────
        GameObject statsGrid = MakeRect("StatsGrid", rootPanel.transform);
        SetAnchor(statsGrid, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector2(0.5f, 0.5f));
        SetOffsetMinMax(statsGrid, new Vector2(0f, 0f), new Vector2(0f, -566f));

        // Garage Panel
        GameObject garagePanel = MakePanel("GaragePanel", statsGrid.transform, C_CARD);
        SetAnchor(garagePanel, new Vector2(0f, 0f), new Vector2(0f, 1f), new Vector2(0f, 0.5f));
        RectOf(garagePanel).anchoredPosition = Vector2.zero;
        RectOf(garagePanel).sizeDelta        = new Vector2(1180f, 0f);

        MakeTMPAnchored(
            "Text_GarageTitle", garagePanel.transform, "FLEET GARAGE", 26f, FontStyles.Bold, C_TEXT_LIGHT,
            new Vector2(340f, 36f), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(32f, -28f), TextAlignmentOptions.Left);
        MakeTMPAnchored(
            "Text_GarageSubtitle", garagePanel.transform, "Depot fuel, wear, and service overview", 17f, FontStyles.Normal, C_TEXT_MUTED,
            new Vector2(560f, 26f), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(32f, -64f), TextAlignmentOptions.Left);

        BuildStatCard(garagePanel, "Stat_Battery",  "FUEL RESERVE", "300 / 300L", 1f,    C_SECONDARY, true,  new Vector2(32f,  -116f));
        BuildStatCard(garagePanel, "Stat_Capacity", "BRAKES",       "92%",        0.92f, C_ACCENT,    false, new Vector2(-32f, -116f));
        BuildStatCard(garagePanel, "Stat_Wear",     "TYRES AVG",    "91%",        0.91f, C_ACCENT,    true,  new Vector2(32f,  -252f));
        BuildStatCard(garagePanel, "Stat_Speed",    "ENGINE",       "95%",        0.95f, C_SUCCESS,   false, new Vector2(-32f, -252f));
        BuildMaintenanceDetailPanel(garagePanel.transform);

        // Career Panel
        GameObject careerPanel = MakePanel("CareerPanel", statsGrid.transform, C_CARD);
        SetAnchor(careerPanel, new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(1f, 0.5f));
        RectOf(careerPanel).anchoredPosition = Vector2.zero;
        RectOf(careerPanel).sizeDelta        = new Vector2(572f, 0f);

        BuildCareerPanel(careerPanel);

        // Version label
        MakeTMPAnchored(
            "Text_Version", rootPanel.transform, "v0.1.0 - Early Access", 16f, FontStyles.Normal, C_TEXT_DIM,
            new Vector2(280f, 24f), new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(0f, 0f), TextAlignmentOptions.BottomRight);

        // Event System
        if (Object.FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem");
            es.AddComponent<UnityEngine.EventSystems.EventSystem>();
            AddBestInputModule(es);
        }

        // Wire MainMenuUI
        MainMenuUI ui = mainMenuPanel.AddComponent<MainMenuUI>();
        ui.canvasGroup          = mainMenuPanel.GetComponent<CanvasGroup>();
        ui.rootPanel            = RectOf(rootPanel);
        ui.topBar               = RectOf(topBar);
        ui.heroCard             = RectOf(heroCard);
        ui.statsGrid            = RectOf(statsGrid);
        ui.backgroundPanel      = mainMenuPanel.GetComponent<Image>();
        ui.heroCardBackground   = heroCard.GetComponent<Image>();
        ui.heroGradientOverlay  = heroOverlay.GetComponent<Image>();
        ui.heroBusImage         = heroBusArtwork;
        ui.titleText            = titleText;
        ui.navDriveText         = navDriveText;
        ui.navGarageText        = navGarageText;
        ui.navRoutesText        = navRoutesText;
        ui.navMarketText        = navMarketText;
        ui.currencyBadge        = currencyBadge.GetComponent<Image>();
        ui.currencyText         = currencyText;
        ui.statusChipText       = statusChipText;
        ui.statusChipBackground = statusChip.GetComponent<Image>();
        ui.heroTitleText        = heroTitleText;
        ui.heroDescriptionText  = heroDescText;
        ui.playButton           = btnPlay.GetComponent<Button>();
        ui.playButtonText       = FindTMP(btnPlay.transform, "Text");
        ui.customizeButton      = btnCustomize.GetComponent<Button>();
        ui.customizeButtonText  = FindTMP(btnCustomize.transform, "Text");
        ui.garagePanel          = garagePanel.GetComponent<Image>();
        ui.garageTitleText      = FindTMP(garagePanel.transform, "Text_GarageTitle");
        ui.garageSubtitleText   = FindTMP(garagePanel.transform, "Text_GarageSubtitle");
        ui.batteryValueText     = FindTMP(garagePanel.transform,  "Stat_Battery/Text_Value");
        ui.batteryFill          = FindImage(garagePanel.transform, "Stat_Battery/Bar_BG/Bar_Fill");
        ui.capacityValueText    = FindTMP(garagePanel.transform,  "Stat_Capacity/Text_Value");
        ui.capacityFill         = FindImage(garagePanel.transform, "Stat_Capacity/Bar_BG/Bar_Fill");
        ui.wearValueText        = FindTMP(garagePanel.transform,  "Stat_Wear/Text_Value");
        ui.wearFill             = FindImage(garagePanel.transform, "Stat_Wear/Bar_BG/Bar_Fill");
        ui.speedValueText       = FindTMP(garagePanel.transform,  "Stat_Speed/Text_Value");
        ui.speedFill            = FindImage(garagePanel.transform, "Stat_Speed/Bar_BG/Bar_Fill");
        ui.careerPanel          = careerPanel.GetComponent<Image>();
        ui.careerTitleText      = FindTMP(careerPanel.transform, "Text_CareerTitle");
        ui.careerSubtitleText   = FindTMP(careerPanel.transform, "Text_CareerSubtitle");
        ui.levelValueText       = FindTMP(careerPanel.transform, "Text_LevelValue");
        ui.xpValueText          = FindTMP(careerPanel.transform, "Text_XPValue");
        ui.xpFill               = FindImage(careerPanel.transform, "XPBar_BG/XPBar_Fill");
        ui.versionText          = FindTMP(rootPanel.transform, "Text_Version");

        EditorUtility.SetDirty(sceneLoader);
        EditorUtility.SetDirty(gameState);
        EditorUtility.SetDirty(cityManager);
        EditorUtility.SetDirty(uiFonts);
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        Debug.Log("[MainMenuBuilder] Scene built with live manager components, seeded city data, and a fully wired main menu.");
    }

    // =========================================================================
    //  CAREER PANEL  (full rebuild matching the screenshot)
    // =========================================================================

    static void BuildCareerPanel(GameObject panel)
    {
        // ── Section header ────────────────────────────────────────────────────
        // "CAREER" label (small, muted, uppercase)
        MakeTMPAnchored(
            "Text_CareerTitle", panel.transform,
            "CAREER", 12f, FontStyles.Bold, C_TEXT_MUTED,
            new Vector2(340f, 20f),
            new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(32f, -28f),
            TextAlignmentOptions.Left);

        // "Depot Rookie" rank name (large, light)
        MakeTMPAnchored(
            "Text_CareerSubtitle", panel.transform,
            "Depot Rookie", 20f, FontStyles.Bold, C_TEXT_LIGHT,
            new Vector2(400f, 34f),
            new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(32f, -52f),
            TextAlignmentOptions.Left);

        // Hidden level value kept for MainMenuUI wiring
        TextMeshProUGUI levelHidden = MakeTMPAnchored(
            "Text_LevelValue", panel.transform,
            "1", 1f, FontStyles.Normal, Color.clear,
            new Vector2(1f, 1f),
            new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(0f, -1f),
            TextAlignmentOptions.Left);

        // ── Data rows  (top offset relative to panel top) ────────────────────
        // Starting Y = -90 so rows sit below the header text.
        float rowY   = -90f;
        float rowH   = 40f;  // height of each standard row
        float repH   = 64f;  // taller row for reputation (has progress bar below)

        // Top divider
        BuildHRule(panel.transform, "Divider_Top", rowY);

        // Row 1 – Current City (label + orange fill bar + accent city name)
        rowY -= 2f; // sit just below divider
        BuildCityRow(panel.transform, "Row_City",
            "CURRENT CITY", "Nairobi • Kenya", rowY, rowH);

        rowY -= rowH;
        // No divider here based on the image

        // Row 2 – Active Country
        rowY -= 2f;
        BuildEmptyRow(panel.transform, "Row_Route",
            "ACTIVE COUNTRY", "No country selected", rowY, rowH);

        // rowY -= rowH;
        // // Row 3 – Last Payout
        // BuildEmptyRow(panel.transform, "Row_Payout",
        //     "LAST PAYOUT", "No shift payout yet", rowY, rowH);

        // rowY -= rowH;
        // BuildHRule(panel.transform, "Divider_1", rowY);

        // // Row 4 – Reputation (XP label left, score right, progress bar below)
        // rowY -= 2f;
        // BuildReputationRow(panel.transform, "Row_Reputation",
        //     "REPUTATION", "Rank 1 — 0 / 500 XP", "100 / 100",
        //     rowY, repH);

        // // XP sub-label ("500 XP to Rank 2") sits below the bar — BuildReputationRow places it
        // rowY -= repH;
        // // No divider here based on the image

        // // Row 5 – Next Unlock (blue dot + blue text)
        // rowY -= 2f;
        // BuildUnlockRow(panel.transform, "Row_Unlock",
        //     "NEXT UNLOCK", "Rank 2 — Longer shift chains unlocked", rowY, rowH);

        // ── XP bar wiring stubs (kept for MainMenuUI field wiring) ────────────
        // The real XP bar lives inside BuildReputationRow above.
        // We add hidden anchors so FindImage/FindTMP still resolve.
        GameObject xpBgStub = MakePanel("XPBar_BG", panel.transform, Color.clear);
        RectTransform xpBgR = RectOf(xpBgStub);
        xpBgR.anchorMin        = new Vector2(0f, 1f);
        xpBgR.anchorMax        = new Vector2(0f, 1f);
        xpBgR.anchoredPosition = new Vector2(0f, -1f);
        xpBgR.sizeDelta        = new Vector2(1f, 1f);

        GameObject xpFillStub = MakePanel("XPBar_Fill", xpBgStub.transform, Color.clear);
        RectTransform xpFillR = RectOf(xpFillStub);
        xpFillR.anchorMin  = Vector2.zero;
        xpFillR.anchorMax  = Vector2.zero;
        xpFillR.offsetMin  = Vector2.zero;
        xpFillR.offsetMax  = Vector2.zero;

        MakeTMPAnchored(
            "Text_XPValue", panel.transform,
            "0 / 500 XP  •  500 TO NEXT", 1f, FontStyles.Normal, Color.clear,
            new Vector2(1f, 1f),
            new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(0f, -2f),
            TextAlignmentOptions.Left);
    }

    // ── Row builders ─────────────────────────────────────────────────────────

    /// <summary>
    /// Current City row: [LABEL]  [━━━━━━━━━━]  [City · Country]
    /// The orange fill bar stretches between label and value.
    /// </summary>
    static void BuildCityRow(Transform parent, string id,
        string label, string value, float topY, float height)
    {
        GameObject row = MakeRowRect(id, parent, topY, height);

        // Label
        MakeTMPAnchored("Text_Label", row.transform,
            label, 11f, FontStyles.Bold, C_TEXT_MUTED,
            new Vector2(140f, height),
            new Vector2(0f, 0f), new Vector2(0f, 1f),
            new Vector2(32f, 0f),
            TextAlignmentOptions.MidlineLeft);

        // Bar background – stretches from after label to before value
        GameObject barBg = MakePanel("Bar_BG", row.transform, C_XP_TRACK);
        RectTransform bbR = RectOf(barBg);
        bbR.anchorMin        = new Vector2(0f, 0.5f);
        bbR.anchorMax        = new Vector2(1f, 0.5f);
        bbR.offsetMin        = new Vector2(140f, -2f);
        bbR.offsetMax        = new Vector2(-120f, 2f);

        // Bar fill (100% = city is active)
        GameObject barFill = MakePanel("Bar_Fill", barBg.transform, C_ACCENT);
        RectTransform bfR = RectOf(barFill);
        bfR.anchorMin = Vector2.zero;
        bfR.anchorMax = Vector2.one;
        bfR.offsetMin = Vector2.zero;
        bfR.offsetMax = Vector2.zero;

        // Value (right-aligned, orange)
        MakeTMPAnchored("Text_Value", row.transform,
            value, 13f, FontStyles.Bold, C_ACCENT,
            new Vector2(120f, height),
            new Vector2(1f, 0f), new Vector2(1f, 1f),
            new Vector2(-32f, 0f),
            TextAlignmentOptions.MidlineRight);
    }

    /// <summary>
    /// Empty/placeholder row: [LABEL]  [italic dim value]
    /// Used for Active Country and Last Payout when no data is available.
    /// </summary>
    static void BuildEmptyRow(Transform parent, string id,
        string label, string value, float topY, float height)
    {
        GameObject row = MakeRowRect(id, parent, topY, height);

        MakeTMPAnchored("Text_Label", row.transform,
            label, 11f, FontStyles.Bold, C_TEXT_MUTED,
            new Vector2(140f, height),
            new Vector2(0f, 0f), new Vector2(0f, 1f),
            new Vector2(32f, 0f),
            TextAlignmentOptions.MidlineLeft);

        MakeTMPAnchored("Text_Value", row.transform,
            value, 13f, FontStyles.Italic, C_TEXT_MUTED,
            new Vector2(300f, height),
            new Vector2(0f, 0f), new Vector2(0f, 1f),
            new Vector2(140f, 0f),
            TextAlignmentOptions.MidlineLeft);
    }

    /// <summary>
    /// Reputation row:
    ///   Top line: [REPUTATION]  [Rank 1 — 0 / 500 XP]  [100 / 100 (green)]
    ///   Progress bar spanning full width
    ///   Sub-label: "500 XP to Rank 2"
    /// </summary>
    static void BuildReputationRow(Transform parent, string id,
        string label, string xpText, string scoreText,
        float topY, float height)
    {
        GameObject row = MakeRowRect(id, parent, topY, height);

        // Row label
        MakeTMPAnchored("Text_Label", row.transform,
            label, 11f, FontStyles.Bold, C_TEXT_MUTED,
            new Vector2(140f, 20f),
            new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(32f, -12f),
            TextAlignmentOptions.Left);

        // XP progress text (left of centre)
        MakeTMPAnchored("Text_Value", row.transform,
            xpText, 13f, FontStyles.Bold, C_TEXT_LIGHT,
            new Vector2(200f, 20f),
            new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(140f, -12f),
            TextAlignmentOptions.Left);

        // Score text (far right, green)
        MakeTMPAnchored("Text_Score", row.transform,
            scoreText, 13f, FontStyles.Bold, C_SUCCESS,
            new Vector2(100f, 20f),
            new Vector2(1f, 1f), new Vector2(1f, 1f),
            new Vector2(-32f, -12f),
            TextAlignmentOptions.Right);

        // Progress bar track
        GameObject barBg = MakePanel("Bar_BG", row.transform, C_XP_TRACK);
        RectTransform bbR = RectOf(barBg);
        bbR.anchorMin = new Vector2(0f, 1f);
        bbR.anchorMax = new Vector2(1f, 1f);
        bbR.offsetMin = new Vector2(32f, -38f);
        bbR.offsetMax = new Vector2(-32f, -32f);

        // Progress bar fill (0 % at start)
        GameObject barFill = MakePanel("Bar_Fill", barBg.transform, C_SUCCESS);
        RectTransform bfR = RectOf(barFill);
        bfR.anchorMin = Vector2.zero;
        bfR.anchorMax = new Vector2(0f, 1f);  // 0 % – runtime will update
        bfR.offsetMin = Vector2.zero;
        bfR.offsetMax = Vector2.zero;

        // Sub-label below bar
        MakeTMPAnchored("Text_SubLabel", row.transform,
            "500 XP to Rank 2", 11f, FontStyles.Normal, C_TEXT_MUTED,
            new Vector2(300f, 18f),
            new Vector2(0f, 1f), new Vector2(0f, 1f),
            new Vector2(32f, -46f),
            TextAlignmentOptions.Left);
    }

    /// <summary>
    /// Next Unlock row: [NEXT UNLOCK]  [● Rank 2 — description]
    /// The dot is a small panel (circle sprite required at runtime).
    /// </summary>
    static void BuildUnlockRow(Transform parent, string id,
        string label, string value, float topY, float height)
    {
        GameObject row = MakeRowRect(id, parent, topY, height);

        MakeTMPAnchored("Text_Label", row.transform,
            label, 11f, FontStyles.Bold, C_TEXT_MUTED,
            new Vector2(140f, height),
            new Vector2(0f, 0f), new Vector2(0f, 1f),
            new Vector2(32f, 0f),
            TextAlignmentOptions.MidlineLeft);

        // Dot indicator
        GameObject dot = MakePanel("Dot", row.transform, C_SECONDARY);
        RectTransform dotR = RectOf(dot);
        dotR.anchorMin        = new Vector2(0f, 0.5f);
        dotR.anchorMax        = new Vector2(0f, 0.5f);
        dotR.pivot            = new Vector2(0.5f, 0.5f);
        dotR.anchoredPosition = new Vector2(140f, 0f);
        dotR.sizeDelta        = new Vector2(6f, 6f);
        // Assign a circle sprite at runtime via MainMenuUI, or use a Mask component here.

        MakeTMPAnchored("Text_Value", row.transform,
            value, 13f, FontStyles.Bold, C_SECONDARY,
            new Vector2(280f, height),
            new Vector2(0f, 0f), new Vector2(0f, 1f),
            new Vector2(154f, 0f),
            TextAlignmentOptions.MidlineLeft);
    }

    /// <summary>Creates a full-width 1 px horizontal rule.</summary>
    static void BuildHRule(Transform parent, string id, float topY)
    {
        GameObject div = MakePanel(id, parent, C_DIVIDER);
        RectTransform r = RectOf(div);
        r.anchorMin        = new Vector2(0f, 1f);
        r.anchorMax        = new Vector2(1f, 1f);
        r.pivot            = new Vector2(0.5f, 1f);
        r.anchoredPosition = new Vector2(0f, topY);
        r.offsetMin        = new Vector2(32f, topY - 1f);
        r.offsetMax        = new Vector2(-32f, topY);
        // Simpler: use sizeDelta for the 1 px height
        r.anchorMin        = new Vector2(0f, 1f);
        r.anchorMax        = new Vector2(1f, 1f);
        r.pivot            = new Vector2(0.5f, 1f);
        r.anchoredPosition = new Vector2(0f, topY);
        r.sizeDelta        = new Vector2(-64f, 1f);
    }

    /// <summary>Creates a RectTransform row anchored to the top of its parent.</summary>
    static GameObject MakeRowRect(string id, Transform parent, float topY, float height)
    {
        GameObject row = MakeRect(id, parent);
        RectTransform r = RectOf(row);
        r.anchorMin        = new Vector2(0f, 1f);
        r.anchorMax        = new Vector2(1f, 1f);
        r.pivot            = new Vector2(0.5f, 1f);
        r.anchoredPosition = new Vector2(0f, topY);
        r.sizeDelta        = new Vector2(0f, height);
        return row;
    }

    // =========================================================================
    //  GARAGE helpers (unchanged)
    // =========================================================================

    static void BuildStatCard(GameObject parent, string id, string label, string value,
        float fillPct, Color fillColor, bool anchorLeft, Vector2 pos)
    {
        GameObject card = MakePanel(id, parent.transform, C_STAT);
        Vector2 anchor  = anchorLeft ? new Vector2(0f, 1f) : new Vector2(1f, 1f);
        SetAnchor(card, anchor, anchor, anchor);
        RectOf(card).anchoredPosition = pos;
        RectOf(card).sizeDelta        = new Vector2(532f, 120f);

        MakeTMPAnchored("Text_Label", card.transform, label, 12f, FontStyles.Bold, C_TEXT_MUTED,
            new Vector2(420f, 22f), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(24f, -20f), TextAlignmentOptions.Left);
        MakeTMPAnchored("Text_Value", card.transform, value, 34f, FontStyles.Bold, C_TEXT_LIGHT,
            new Vector2(420f, 48f), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(24f, -46f), TextAlignmentOptions.Left);

        GameObject barBg = MakePanel("Bar_BG", card.transform, C_XP_TRACK);
        SetAnchor(barBg, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f));
        RectOf(barBg).anchoredPosition = new Vector2(24f, -100f);
        RectOf(barBg).sizeDelta        = new Vector2(420f, 10f);

        GameObject barFill = MakePanel("Bar_Fill", barBg.transform, fillColor);
        RectTransform fillRT = RectOf(barFill);
        fillRT.anchorMin = Vector2.zero;
        fillRT.anchorMax = new Vector2(fillPct, 1f);
        fillRT.offsetMin = Vector2.zero;
        fillRT.offsetMax = Vector2.zero;
    }

    static void BuildMaintenanceDetailPanel(Transform parent)
    {
        GameObject root = MakeRect("MaintenanceDetailPanel", parent);
        RectTransform rect = RectOf(root);
        rect.anchorMin        = new Vector2(0f, 1f);
        rect.anchorMax        = new Vector2(1f, 1f);
        rect.pivot            = new Vector2(0.5f, 1f);
        rect.anchoredPosition = new Vector2(0f, 0f);
        rect.offsetMin        = new Vector2(350f, -110f);
        rect.offsetMax        = new Vector2(-32f, 0f);

        string[] labels = { "Brakes", "Steer axle tyres", "Middle axle tyres", "Rear axle tyres", "Engine service" };
        for (int i = 0; i < labels.Length; i++)
            BuildInfoRow(root.transform, $"Row_{i}", labels[i], "100%", C_ACCENT, -i * 24f, 0.42f, 0.88f);
    }

    static void BuildInfoRow(Transform parent, string rowName, string label, string value,
        Color fillColor, float topOffset, float barMinAnchor, float barMaxAnchor)
    {
        GameObject row = MakeRect(rowName, parent);
        RectTransform rowRect = RectOf(row);
        rowRect.anchorMin        = new Vector2(0f, 1f);
        rowRect.anchorMax        = new Vector2(1f, 1f);
        rowRect.pivot            = new Vector2(0.5f, 1f);
        rowRect.anchoredPosition = new Vector2(0f, topOffset);
        rowRect.sizeDelta        = new Vector2(0f, 28f);

        MakeTMPAnchored("Text_Label", row.transform, label.ToUpper(), 12f, FontStyles.Bold, C_TEXT_MUTED,
            new Vector2(180f, 18f), new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 0f), TextAlignmentOptions.Left);
        MakeTMPAnchored("Text_Value", row.transform, value, 13f, FontStyles.Bold, C_TEXT_LIGHT,
            new Vector2(180f, 18f), new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0f, 0f), TextAlignmentOptions.Right);

        GameObject barBg = MakePanel("Bar_BG", row.transform, C_SURFACE);
        RectTransform bgRect = RectOf(barBg);
        bgRect.anchorMin        = new Vector2(barMinAnchor, 0.5f);
        bgRect.anchorMax        = new Vector2(barMaxAnchor, 0.5f);
        bgRect.pivot            = new Vector2(0.5f, 0.5f);
        bgRect.sizeDelta        = new Vector2(0f, 8f);
        bgRect.anchoredPosition = Vector2.zero;

        GameObject barFill = MakePanel("Bar_Fill", barBg.transform, fillColor);
        RectTransform fillRect = RectOf(barFill);
        fillRect.anchorMin = Vector2.zero;
        fillRect.anchorMax = new Vector2(0.72f, 1f);
        fillRect.offsetMin = Vector2.zero;
        fillRect.offsetMax = Vector2.zero;
    }

    // =========================================================================
    //  Shared helpers
    // =========================================================================

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

    static TextMeshProUGUI AddNavLink(Transform parent, string goName, string text, float width, Color color)
    {
        GameObject go = new GameObject(goName);
        go.transform.SetParent(parent, false);
        TextMeshProUGUI tmp = go.AddComponent<TextMeshProUGUI>();
        tmp.text      = text;
        tmp.fontSize  = 18f;
        tmp.fontStyle = FontStyles.Bold;
        tmp.color     = color;
        tmp.alignment = TextAlignmentOptions.Center;
        LayoutElement layout = go.AddComponent<LayoutElement>();
        layout.preferredWidth  = width;
        layout.preferredHeight = 56f;
        return tmp;
    }

    static GameObject MakeButton(string name, Transform parent, string label,
        Color bgColor, Color textColor, Vector2 size, Vector2 anchorMin, Vector2 anchorMax, Vector2 pos)
    {
        GameObject go = MakePanel(name, parent, bgColor);
        SetAnchor(go, anchorMin, anchorMax, new Vector2(0f, 0f));
        RectOf(go).anchoredPosition = pos;
        RectOf(go).sizeDelta        = size;
        go.AddComponent<Button>().navigation = new Navigation { mode = Navigation.Mode.None };
        MakeTMP("Text", go.transform, label, 22f, FontStyles.Bold, textColor, size, TextAlignmentOptions.Center);
        return go;
    }

    static GameObject MakeRect(string name, Transform parent)
    {
        GameObject go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        return go;
    }

    static GameObject MakePanel(string name, Transform parent, Color color)
    {
        GameObject go    = MakeRect(name, parent);
        Image      image = go.AddComponent<Image>();
        image.color = color;
        return go;
    }

    static TextMeshProUGUI MakeTMP(string name, Transform parent, string text,
        float size, FontStyles style, Color color, Vector2 sizeDelta, TextAlignmentOptions align)
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
        float size, FontStyles style, Color color, Vector2 sizeDelta,
        Vector2 anchorMin, Vector2 anchorMax, Vector2 pos, TextAlignmentOptions align)
    {
        TextMeshProUGUI tmp  = MakeTMP(name, parent, text, size, style, color, sizeDelta, align);
        RectTransform   rect = tmp.rectTransform;
        rect.anchorMin        = anchorMin;
        rect.anchorMax        = anchorMax;
        rect.pivot            = anchorMin;
        rect.anchoredPosition = pos;
        rect.sizeDelta        = sizeDelta;
        return tmp;
    }

    static RectTransform RectOf(GameObject go) => go.GetComponent<RectTransform>();

    static void Stretch(GameObject go, float left, float right, float top, float bottom)
    {
        RectTransform rect = RectOf(go);
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = new Vector2(left,  bottom);
        rect.offsetMax = new Vector2(right, top);
    }

    static void SetAnchor(GameObject go, Vector2 min, Vector2 max, Vector2 pivot)
    {
        RectTransform rect = RectOf(go);
        rect.anchorMin = min;
        rect.anchorMax = max;
        rect.pivot     = pivot;
    }

    static void SetOffsetMinMax(GameObject go, Vector2 min, Vector2 max)
    {
        RectTransform rect = RectOf(go);
        rect.offsetMin = min;
        rect.offsetMax = max;
    }

    static GameObject FindOrCreateRoot(string name)
    {
        GameObject existing = GameObject.Find(name);
        return existing != null ? existing : new GameObject(name);
    }

    static T FindOrCreateManager<T>(Transform parent, string name) where T : MonoBehaviour
    {
        T existing = Object.FindObjectOfType<T>();
        if (existing != null)
        {
            existing.name = name;
            if (existing.transform.parent != parent)
                existing.transform.SetParent(parent, false);
            return existing;
        }
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);
        return go.AddComponent<T>();
    }

    static void AssignFonts(UIFonts fonts)
    {
        if (fonts == null) return;
        fonts.bold      = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/_Game/Scripts/Fonts/SpaceGrotesk-Bold SDF.asset");
        fonts.medium    = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/_Game/Scripts/Fonts/SpaceGrotesk-Medium SDF.asset");
        fonts.regular   = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/_Game/Scripts/Fonts/SpaceGrotesk-Regular SDF.asset");
        fonts.lightFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/_Game/Scripts/Fonts/SpaceGrotesk-Light SDF.asset");
    }

    static void SeedCityAndSelectionData(CityManager cityManager, GameState gameState)
    {
        if (cityManager == null || gameState == null) return;

        CountryDefinition[] countries = LoadAllAssets<CountryDefinition>("t:CountryDefinition")
            .OrderBy(c => c.countryName).ToArray();
        CityDefinition[] cities = LoadAllAssets<CityDefinition>("t:CityDefinition")
            .OrderBy(c => c.country).ThenBy(c => c.cityName).ToArray();

        cityManager.allCountries = countries;
        cityManager.allCities    = cities;

        if (cityManager.activeCountry == null && countries.Length > 0)
            cityManager.activeCountry = countries[0];
        if (cityManager.activeCity == null && cities.Length > 0)
            cityManager.activeCity = cities[0];

        if (gameState.selectedCountry == null) gameState.selectedCountry = cityManager.activeCountry;
        if (gameState.selectedCity    == null) gameState.selectedCity    = cityManager.activeCity;
    }

    static List<T> LoadAllAssets<T>(string filter) where T : Object
    {
        List<T> result = new List<T>();
        string[] guids = AssetDatabase.FindAssets(filter);
        foreach (string guid in guids)
        {
            string path  = AssetDatabase.GUIDToAssetPath(guid);
            T      asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset != null) result.Add(asset);
        }
        return result;
    }

    static TextMeshProUGUI FindTMP(Transform parent, string path)
    {
        Transform child = parent.Find(path);
        return child != null ? child.GetComponent<TextMeshProUGUI>() : null;
    }

    static Image FindImage(Transform parent, string path)
    {
        Transform child = parent.Find(path);
        return child != null ? child.GetComponent<Image>() : null;
    }

    static Color Hex(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color color);
        return color;
    }

    static Color WithAlpha(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }
}
#endif