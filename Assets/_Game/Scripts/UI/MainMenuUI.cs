using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MainMenuUI : MonoBehaviour
{
    [Header("Layout")]
    [SerializeField] bool landscapeOnly = true;
    [SerializeField] float compactLandscapeAspect = 1.85f;
    [SerializeField] float compactLandscapeShortSide = 800f;
    [SerializeField] float scrollableLandscapeShortSide = 760f;
    [SerializeField] float scrollableLandscapeAspect = 2.05f;

    [Header("References — assign in Inspector")]
    public CanvasGroup canvasGroup;
    public RectTransform rootPanel;
    public RectTransform topBar;
    public RectTransform heroCard;
    public RectTransform statsGrid;

    [Header("Background")]
    public Image backgroundPanel;
    public Image heroCardBackground;
    public Image heroGradientOverlay;

    [Header("Top Bar")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI navDriveText;
    public TextMeshProUGUI navGarageText;
    public TextMeshProUGUI navRoutesText;
    public TextMeshProUGUI navMarketText;
    public Image currencyBadge;
    public TextMeshProUGUI currencyText;

    [Header("Hero")]
    public Image heroBusImage;
    public TextMeshProUGUI statusChipText;
    public Image statusChipBackground;
    public TextMeshProUGUI heroTitleText;
    public TextMeshProUGUI heroDescriptionText;
    public Button playButton;
    public TextMeshProUGUI playButtonText;
    public Button customizeButton;
    public TextMeshProUGUI customizeButtonText;

    [Header("Garage Panel")]
    public Image garagePanel;
    public TextMeshProUGUI garageTitleText;
    public TextMeshProUGUI garageSubtitleText;
    public TextMeshProUGUI batteryValueText;
    public Image batteryFill;
    public TextMeshProUGUI capacityValueText;
    public Image capacityFill;
    public TextMeshProUGUI wearValueText;
    public Image wearFill;
    public TextMeshProUGUI speedValueText;
    public Image speedFill;

    TextMeshProUGUI batteryLabelText;
    TextMeshProUGUI capacityLabelText;
    TextMeshProUGUI wearLabelText;
    TextMeshProUGUI speedLabelText;

    RectTransform maintenanceDetailRoot;
    TextMeshProUGUI[] maintenanceRowLabels;
    TextMeshProUGUI[] maintenanceRowValues;
    Image[] maintenanceRowFills;

    [Header("Career Panel")]
    public Image careerPanel;
    public TextMeshProUGUI careerTitleText;
    public TextMeshProUGUI careerSubtitleText;
    public TextMeshProUGUI levelValueText;
    public TextMeshProUGUI xpValueText;
    public Image xpFill;
    public TextMeshProUGUI versionText;

    RectTransform careerDetailRoot;
    TextMeshProUGUI[] careerDetailLabels;
    TextMeshProUGUI[] careerDetailValues;
    Image[] careerDetailFills;

    Vector2Int lastScreenSize;
    RectTransform statsViewport;
    ScrollRect statsScrollRect;
    Image statsViewportImage;
    Mask statsViewportMask;
    bool statsScrollInitialized;

    // Cache original editor/layout RectTransform values so Play Mode can match
    // the static scene when scrollable stats aren't needed.
    bool cachedStaticStatsLayout;
    Vector2 statsAnchorMinStatic;
    Vector2 statsAnchorMaxStatic;
    Vector2 statsPivotStatic;
    Vector2 statsAnchoredPositionStatic;
    Vector2 statsSizeDeltaStatic;
    Vector2 statsOffsetMinStatic;
    Vector2 statsOffsetMaxStatic;

    Vector2 garageAnchorMinStatic;
    Vector2 garageAnchorMaxStatic;
    Vector2 garagePivotStatic;
    Vector2 garageAnchoredPositionStatic;
    Vector2 garageSizeDeltaStatic;
    Vector2 garageOffsetMinStatic;
    Vector2 garageOffsetMaxStatic;

    Vector2 careerAnchorMinStatic;
    Vector2 careerAnchorMaxStatic;
    Vector2 careerPivotStatic;
    Vector2 careerAnchoredPositionStatic;
    Vector2 careerSizeDeltaStatic;
    Vector2 careerOffsetMinStatic;
    Vector2 careerOffsetMaxStatic;
    GarageScreenUI garageScreen;
    bool garageScreenVisible;

    void Start()
    {
        BusFleetManager.EnsureExists();
        XPSystem.EnsureExists();
        AutoBindLayoutReferences();
        CacheGarageStatLabels();
        EnsureMaintenanceDetailUI();
        EnsureCareerDetailUI();

        CacheStaticStatsLayout();

        ApplyTheme();
        EnsureGarageScreen();
        RefreshGarageAndFleetPresentation();
        Canvas.ForceUpdateCanvases();
        ApplyResponsiveLayout(force: true);
        SetupButtons();
        StartCoroutine(AnimateIn());
    }

    void Update()
    {
        ApplyResponsiveLayout();
        RefreshGarageMetrics();
        RefreshFleetPresentation();
        RefreshCareerProgress();
    }

    void ApplyTheme()
    {
        if (backgroundPanel)
            backgroundPanel.color = UITheme.Background;

        if (heroCardBackground)
            heroCardBackground.color = UITheme.SurfaceContainer;

        if (heroGradientOverlay)
            heroGradientOverlay.color = UITheme.WithAlpha(UITheme.Background, 0.58f);

        if (heroBusImage)
            heroBusImage.preserveAspect = false;

        if (titleText)
        {
            titleText.text = "REAL BUS SIM";
            titleText.color = UITheme.Accent;
            titleText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            titleText.characterSpacing = 14f;
            titleText.fontStyle = FontStyles.Bold;
        }

        ApplyNavLabel(navDriveText, "DRIVE", true);
        ApplyNavLabel(navGarageText, "GARAGE", false);
        ApplyNavLabel(navRoutesText, "START PLAYING", false);
        ApplyNavLabel(navMarketText, "MARKET", false);

        if (currencyBadge)
            currencyBadge.color = UITheme.Surface;

        if (currencyText)
        {
            float balance = GameState.Instance != null && GameState.Instance.economy != null
                ? GameState.Instance.economy.balanceKES
                : 254000f;
            currencyText.text = $"KES {balance:N0}";
            currencyText.color = UITheme.Accent;
            currencyText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            currencyText.characterSpacing = 1f;
        }

        if (statusChipBackground)
            statusChipBackground.color = UITheme.WithAlpha(UITheme.Accent, 0.18f);

        if (statusChipText)
        {
            statusChipText.text = "CURRENTLY EQUIPPED";
            statusChipText.color = UITheme.Accent;
            statusChipText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            statusChipText.characterSpacing = 6f;
            statusChipText.fontStyle = FontStyles.Bold;
        }

        if (heroTitleText)
        {
            heroTitleText.text = "FLEET READY";
            heroTitleText.color = UITheme.TextPrimary;
            heroTitleText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            heroTitleText.fontStyle = FontStyles.Bold | FontStyles.Italic;
        }

        if (heroDescriptionText)
        {
            heroDescriptionText.text = "Choose the right vehicle for the route, the crowd, and the shift.";
            heroDescriptionText.color = UITheme.TextSecondary;
            heroDescriptionText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
        }

        if (playButton)
        {
            var img = playButton.GetComponent<Image>();
            if (img) img.color = UITheme.Accent;

            var cols = playButton.colors;
            cols.normalColor = UITheme.Accent;
            cols.highlightedColor = UITheme.AccentDim;
            cols.pressedColor = UITheme.WithAlpha(UITheme.Accent, 0.7f);
            playButton.colors = cols;
        }

        if (playButtonText)
        {
            playButtonText.text = "START DRIVING";
            playButtonText.color = UITheme.Background;
            playButtonText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            playButtonText.characterSpacing = 4f;
            playButtonText.fontStyle = FontStyles.Bold;
        }

        if (customizeButton)
        {
            var img = customizeButton.GetComponent<Image>();
            if (img) img.color = UITheme.WithAlpha(UITheme.SurfaceHigh, 0.88f);

            var cols = customizeButton.colors;
            cols.normalColor = UITheme.WithAlpha(UITheme.SurfaceHigh, 0.88f);
            cols.highlightedColor = UITheme.SurfaceBright;
            cols.pressedColor = UITheme.WithAlpha(UITheme.SurfaceHigh, 0.7f);
            customizeButton.colors = cols;
        }

        if (customizeButtonText)
        {
            customizeButtonText.text = "OPEN GARAGE";
            customizeButtonText.color = UITheme.TextPrimary;
            customizeButtonText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            customizeButtonText.characterSpacing = 4f;
        }

        ApplyPanel(garagePanel);
        ApplyPanel(careerPanel);

        ApplySectionHeading(garageTitleText, "FLEET GARAGE");
        ApplySectionSubtitle(garageSubtitleText, "Depot fuel, wear, and service overview");
        ApplySectionHeading(careerTitleText, "CAREER");
        ApplySectionSubtitle(careerSubtitleText, "Driver level and dispatch readiness");

        ApplyStatLabel(batteryLabelText, "FUEL RESERVE");
        ApplyStatLabel(capacityLabelText, "BRAKES");
        ApplyStatLabel(wearLabelText, "TYRES AVG");
        ApplyStatLabel(speedLabelText, "ENGINE");

        ApplyStatValue(batteryValueText, "300 / 300L", UITheme.TextPrimary);
        ApplyStatValue(capacityValueText, "92%", UITheme.TextPrimary);
        ApplyStatValue(wearValueText, "91%", UITheme.TextPrimary);
        ApplyStatValue(speedValueText, "95%", UITheme.TextPrimary);

        ApplyFill(batteryFill, UITheme.Secondary, 1f);
        ApplyFill(capacityFill, UITheme.TertiaryDim, 0.92f);
        ApplyFill(wearFill, UITheme.Accent, 0.91f);
        ApplyFill(speedFill, UITheme.Success, 0.95f);

        if (levelValueText)
        {
            levelValueText.text = "1";
            levelValueText.color = UITheme.TextPrimary;
            levelValueText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }

        if (xpValueText)
        {
            xpValueText.text = "0 / 500 XP  •  500 TO NEXT";
            xpValueText.color = UITheme.TextSecondary;
            xpValueText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
        }

        ApplyFill(xpFill, UITheme.Accent, 0f);

        if (versionText)
        {
            versionText.text = "v0.1.0 - Early Access";
            versionText.color = UITheme.TextMuted;
            versionText.font = UITheme.GetFont(UITheme.FontWeight.Regular);
        }
    }

    void AutoBindLayoutReferences()
    {
        RectTransform selfRect = transform as RectTransform;
        if (!rootPanel && selfRect)
            rootPanel = FindRect(selfRect, "RootPanel");

        if (!topBar && rootPanel)
            topBar = FindRect(rootPanel, "TopBar");

        if (!heroCard && rootPanel)
            heroCard = FindRect(rootPanel, "HeroCard");

        if (!statsGrid && rootPanel)
            statsGrid = FindRect(rootPanel, "StatsGrid");

        if (!backgroundPanel)
            backgroundPanel = GetComponent<Image>();

        if (!heroCardBackground && heroCard)
            heroCardBackground = heroCard.GetComponent<Image>();

        if (!heroGradientOverlay && heroCard)
            heroGradientOverlay = FindImage(heroCard, "HeroOverlay");

        if (!garagePanel && statsGrid)
            garagePanel = FindImage(statsGrid, "GaragePanel");

        if (!careerPanel && statsGrid)
            careerPanel = FindImage(statsGrid, "CareerPanel");

        if (!titleText && topBar)
            titleText = FindChildText(topBar, "BrandGroup/Text_Title");

        if (!navDriveText && topBar)
            navDriveText = FindChildText(topBar, "NavLinks/Text_Drive");
        if (!navGarageText && topBar)
            navGarageText = FindChildText(topBar, "NavLinks/Text_Garage");
        if (!navRoutesText && topBar)
            navRoutesText = FindChildText(topBar, "NavLinks/Text_Routes");
        if (!navMarketText && topBar)
            navMarketText = FindChildText(topBar, "NavLinks/Text_Market");

        if (!currencyBadge && topBar)
            currencyBadge = FindImage(topBar, "CurrencyBadge");
        if (!currencyText && topBar)
            currencyText = FindChildText(topBar, "CurrencyBadge/Text_Cash");

        if (!heroBusImage && heroCard)
            heroBusImage = FindImage(heroCard, "HeroBusImage");
        if (!statusChipBackground && heroCard)
            statusChipBackground = FindImage(heroCard, "HeroContent/StatusChip");
        if (!statusChipText && heroCard)
            statusChipText = FindChildText(heroCard, "HeroContent/StatusChip/Text_Status");
        if (!heroTitleText && heroCard)
            heroTitleText = FindChildText(heroCard, "HeroContent/Text_HeroTitle");
        if (!heroDescriptionText && heroCard)
            heroDescriptionText = FindChildText(heroCard, "HeroContent/Text_HeroDesc");
        if (!playButton && heroCard)
            playButton = FindButton(heroCard, "HeroContent/Btn_Play");
        if (!playButtonText && heroCard)
            playButtonText = FindChildText(heroCard, "HeroContent/Btn_Play/Text");
        if (!customizeButton && heroCard)
            customizeButton = FindButton(heroCard, "HeroContent/Btn_Customize");
        if (!customizeButtonText && heroCard)
            customizeButtonText = FindChildText(heroCard, "HeroContent/Btn_Customize/Text");

        if (!garageTitleText && garagePanel)
            garageTitleText = FindChildText(garagePanel.transform, "Text_GarageTitle");
        if (!garageSubtitleText && garagePanel)
            garageSubtitleText = FindChildText(garagePanel.transform, "Text_GarageSubtitle");
        if (!batteryValueText && garagePanel)
            batteryValueText = FindChildText(garagePanel.transform, "Stat_Battery/Text_Value");
        if (!batteryFill && garagePanel)
            batteryFill = FindChildImage(garagePanel.transform, "Stat_Battery/Bar_BG/Bar_Fill");
        if (!capacityValueText && garagePanel)
            capacityValueText = FindChildText(garagePanel.transform, "Stat_Capacity/Text_Value");
        if (!capacityFill && garagePanel)
            capacityFill = FindChildImage(garagePanel.transform, "Stat_Capacity/Bar_BG/Bar_Fill");
        if (!wearValueText && garagePanel)
            wearValueText = FindChildText(garagePanel.transform, "Stat_Wear/Text_Value");
        if (!wearFill && garagePanel)
            wearFill = FindChildImage(garagePanel.transform, "Stat_Wear/Bar_BG/Bar_Fill");
        if (!speedValueText && garagePanel)
            speedValueText = FindChildText(garagePanel.transform, "Stat_Speed/Text_Value");
        if (!speedFill && garagePanel)
            speedFill = FindChildImage(garagePanel.transform, "Stat_Speed/Bar_BG/Bar_Fill");

        if (!careerTitleText && careerPanel)
            careerTitleText = FindChildText(careerPanel.transform, "Text_CareerTitle");
        if (!careerSubtitleText && careerPanel)
            careerSubtitleText = FindChildText(careerPanel.transform, "Text_CareerSubtitle");
        if (!levelValueText && careerPanel)
            levelValueText = FindChildText(careerPanel.transform, "Text_LevelValue");
        if (!xpValueText && careerPanel)
            xpValueText = FindChildText(careerPanel.transform, "Text_XPValue");
        if (!xpFill && careerPanel)
            xpFill = FindChildImage(careerPanel.transform, "XPBar_BG/XPBar_Fill");
        if (!versionText && rootPanel)
            versionText = FindChildText(rootPanel, "Text_Version");
    }

    void CacheStaticStatsLayout()
    {
        if (cachedStaticStatsLayout) return;
        if (!statsGrid || !garagePanel || !careerPanel) return;

        cachedStaticStatsLayout = true;

        statsAnchorMinStatic = statsGrid.anchorMin;
        statsAnchorMaxStatic = statsGrid.anchorMax;
        statsPivotStatic = statsGrid.pivot;
        statsAnchoredPositionStatic = statsGrid.anchoredPosition;
        statsSizeDeltaStatic = statsGrid.sizeDelta;
        statsOffsetMinStatic = statsGrid.offsetMin;
        statsOffsetMaxStatic = statsGrid.offsetMax;

        RectTransform garageRt = garagePanel.rectTransform;
        RectTransform careerRt = careerPanel.rectTransform;

        garageAnchorMinStatic = garageRt.anchorMin;
        garageAnchorMaxStatic = garageRt.anchorMax;
        garagePivotStatic = garageRt.pivot;
        garageAnchoredPositionStatic = garageRt.anchoredPosition;
        garageSizeDeltaStatic = garageRt.sizeDelta;
        garageOffsetMinStatic = garageRt.offsetMin;
        garageOffsetMaxStatic = garageRt.offsetMax;

        careerAnchorMinStatic = careerRt.anchorMin;
        careerAnchorMaxStatic = careerRt.anchorMax;
        careerPivotStatic = careerRt.pivot;
        careerAnchoredPositionStatic = careerRt.anchoredPosition;
        careerSizeDeltaStatic = careerRt.sizeDelta;
        careerOffsetMinStatic = careerRt.offsetMin;
        careerOffsetMaxStatic = careerRt.offsetMax;
    }

    void RestoreStaticStatsLayout()
    {
        if (!cachedStaticStatsLayout) return;
        if (!statsGrid || !garagePanel || !careerPanel) return;

        statsGrid.anchorMin = statsAnchorMinStatic;
        statsGrid.anchorMax = statsAnchorMaxStatic;
        statsGrid.pivot = statsPivotStatic;
        statsGrid.anchoredPosition = statsAnchoredPositionStatic;
        statsGrid.sizeDelta = statsSizeDeltaStatic;
        statsGrid.offsetMin = statsOffsetMinStatic;
        statsGrid.offsetMax = statsOffsetMaxStatic;

        RectTransform garageRt = garagePanel.rectTransform;
        RectTransform careerRt = careerPanel.rectTransform;

        garageRt.anchorMin = garageAnchorMinStatic;
        garageRt.anchorMax = garageAnchorMaxStatic;
        garageRt.pivot = garagePivotStatic;
        garageRt.anchoredPosition = garageAnchoredPositionStatic;
        garageRt.sizeDelta = garageSizeDeltaStatic;
        garageRt.offsetMin = garageOffsetMinStatic;
        garageRt.offsetMax = garageOffsetMaxStatic;

        careerRt.anchorMin = careerAnchorMinStatic;
        careerRt.anchorMax = careerAnchorMaxStatic;
        careerRt.pivot = careerPivotStatic;
        careerRt.anchoredPosition = careerAnchoredPositionStatic;
        careerRt.sizeDelta = careerSizeDeltaStatic;
        careerRt.offsetMin = careerOffsetMinStatic;
        careerRt.offsetMax = careerOffsetMaxStatic;
    }

    void ApplyResponsiveLayout(bool force = false)
    {
        Vector2Int currentScreenSize = new Vector2Int(Screen.width, Screen.height);
        bool layoutNotReady = IsLayoutNotReady();
        if (!force && !layoutNotReady && currentScreenSize == lastScreenSize)
            return;

        lastScreenSize = currentScreenSize;

        bool isLandscape = Screen.width >= Screen.height;
        if (landscapeOnly && !isLandscape)
            return;

        float shortSide = Mathf.Min(Screen.width, Screen.height);
        float aspect = Screen.height <= 0 ? 1f : (float)Screen.width / Screen.height;
        bool useCompactLandscape = aspect >= compactLandscapeAspect || shortSide <= compactLandscapeShortSide;

        float horizontalPadding = useCompactLandscape ? 28f : 56f;
        float topPadding = useCompactLandscape ? 20f : 32f;
        float bottomPadding = useCompactLandscape ? 20f : 32f;

        // Derive major block sizes from current screen height so compact landscape
        // devices (e.g. phones in landscape) still retain visible space for stats.
        float topBarHeight = Mathf.Clamp(shortSide * 0.14f, 56f, 88f);
        float heroGap = useCompactLandscape ? 14f : 24f;
        float heroTopInset = topPadding + topBarHeight + heroGap;
        float heroHeight = Mathf.Clamp(shortSide * (useCompactLandscape ? 0.40f : 0.52f), 150f, 430f);
        float statsTopInset = heroTopInset + heroHeight + (useCompactLandscape ? 14f : 24f);

        // Keep a minimum visible stats viewport.
        float maxStatsTopInset = Mathf.Max(120f, Screen.height - 170f);
        if (statsTopInset > maxStatsTopInset)
        {
            float overflow = statsTopInset - maxStatsTopInset;
            heroHeight = Mathf.Max(130f, heroHeight - overflow);
            statsTopInset = heroTopInset + heroHeight + (useCompactLandscape ? 14f : 24f);
        }

        // Only switch to scrollable stats when remaining vertical space is truly low.
        float remainingStatsHeight = Screen.height - (statsTopInset + bottomPadding);
        bool useScrollableStats =
            remainingStatsHeight < 220f ||
            (shortSide <= scrollableLandscapeShortSide && aspect >= scrollableLandscapeAspect);

        ApplyRootSafeArea(horizontalPadding, topPadding, bottomPadding);

        // Safe-area offsets change rect sizes; ensure they apply before we compute insets
        // for the stats viewport.
        Canvas.ForceUpdateCanvases();
        if (rootPanel)
            LayoutRebuilder.ForceRebuildLayoutImmediate(rootPanel);

        if (topBar)
            topBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, topBarHeight);

        if (heroCard)
            heroCard.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, heroTopInset, heroHeight);

        // Only create the masked scroll wrapper when we actually need it.
        if (useScrollableStats && !statsScrollInitialized)
            EnsureStatsScrollInfrastructure();

        ApplyStatsLayout(statsTopInset, useScrollableStats);

        ApplyLandscapeTypography(useCompactLandscape);

        // Rebuild after we mutate anchors/sizes so masked content updates same frame.
        Canvas.ForceUpdateCanvases();
        if (statsViewport)
            LayoutRebuilder.ForceRebuildLayoutImmediate(statsViewport);
        if (statsGrid)
            LayoutRebuilder.ForceRebuildLayoutImmediate(statsGrid);
    }

    bool IsLayoutNotReady()
    {
        if (!rootPanel || !statsGrid)
            return true;

        if (rootPanel.rect.width <= 1f || rootPanel.rect.height <= 1f)
            return true;

        RectTransform viewport = statsViewport ? statsViewport : statsGrid;
        return viewport.rect.width <= 1f || viewport.rect.height <= 1f;
    }

    void EnsureStatsScrollInfrastructure()
    {
        if (statsScrollInitialized || !statsGrid)
            return;

        statsScrollInitialized = true;

        if (statsGrid.parent == null)
            return;

        var originalParent = statsGrid.parent as RectTransform;
        if (!originalParent)
            return;

        GameObject viewportObject = new GameObject("StatsViewport", typeof(RectTransform), typeof(Image), typeof(Mask), typeof(ScrollRect));
        viewportObject.transform.SetParent(originalParent, false);
        viewportObject.transform.SetSiblingIndex(statsGrid.GetSiblingIndex());

        statsViewport = viewportObject.GetComponent<RectTransform>();
        statsViewport.anchorMin = new Vector2(0f, 0f);
        statsViewport.anchorMax = new Vector2(1f, 1f);
        statsViewport.offsetMin = Vector2.zero;
        statsViewport.offsetMax = Vector2.zero;
        statsViewport.pivot = new Vector2(0.5f, 0.5f);

        statsViewportImage = viewportObject.GetComponent<Image>();
        // Mask uses the graphic's alpha to generate the stencil.
        // If alpha is too close to 0, children can end up fully masked (invisible) in Play Mode.
        statsViewportImage.color = UITheme.WithAlpha(Color.black, 1f);
        statsViewportImage.raycastTarget = true;

        statsViewportMask = viewportObject.GetComponent<Mask>();
        statsViewportMask.showMaskGraphic = false;

        statsScrollRect = viewportObject.GetComponent<ScrollRect>();
        statsScrollRect.horizontal = false;
        statsScrollRect.vertical = true;
        statsScrollRect.movementType = ScrollRect.MovementType.Clamped;
        statsScrollRect.scrollSensitivity = 24f;
        statsScrollRect.inertia = true;
        statsScrollRect.viewport = statsViewport;

        statsGrid.SetParent(statsViewport, false);
        statsScrollRect.content = statsGrid;

        // Force a rect rebuild after we create/re-parent the masked viewport.
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(statsGrid);
    }

    void ApplyStatsLayout(float statsTopInset, bool useScrollableStats)
    {
        if (!statsGrid)
            return;

        // To keep Play Mode visually identical to the static (Play Off) layout,
        // only mutate StatsGrid when scrollable mode is actually required.
        if (!useScrollableStats)
        {
            if (statsScrollRect)
                statsScrollRect.enabled = false;
            RestoreStaticStatsLayout();
            return;
        }

        RectTransform viewport = statsViewport ? statsViewport : statsGrid;
        // Prevent a negative/near-zero viewport height on short landscape screens.
        RectTransform parentRt = viewport.parent as RectTransform;
        if (parentRt && parentRt.rect.height > 1f)
        {
            float maxInset = Mathf.Max(0f, parentRt.rect.height - 140f);
            statsTopInset = Mathf.Clamp(statsTopInset, 0f, maxInset);
        }
        viewport.anchorMin = new Vector2(0f, 0f);
        viewport.anchorMax = new Vector2(1f, 1f);
        viewport.offsetMin = new Vector2(0f, 0f);
        viewport.offsetMax = new Vector2(0f, -statsTopInset);

        RectTransform garageRect = garagePanel ? garagePanel.rectTransform : null;
        RectTransform careerRect = careerPanel ? careerPanel.rectTransform : null;
        if (!garageRect || !careerRect)
            return;

        if (useScrollableStats)
        {
            if (statsScrollRect)
            {
                statsScrollRect.enabled = true;
                // NormalizedPosition direction is commonly "0 = top, 1 = bottom".
                // Using 0 here prevents the content from starting fully scrolled out.
                statsScrollRect.verticalNormalizedPosition = 0f;
            }

            if (statsViewportImage)
                statsViewportImage.raycastTarget = true;

            statsGrid.anchorMin = new Vector2(0f, 1f);
            statsGrid.anchorMax = new Vector2(1f, 1f);
            statsGrid.pivot = new Vector2(0.5f, 1f);
            // Reset offsets as well (with bottom-anchored rects, offsets map to "Pos Y"
            // and can push the grid out of the mask).
            statsGrid.offsetMin = Vector2.zero;
            statsGrid.offsetMax = Vector2.zero;
            statsGrid.anchoredPosition = Vector2.zero;

            float width = viewport.rect.width;
            float garageHeight = 500f;
            float careerHeight = 240f;
            float gap = 18f;
            float contentHeight = garageHeight + gap + careerHeight;

            statsGrid.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            statsGrid.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentHeight);
            statsGrid.offsetMin = new Vector2(0f, statsGrid.offsetMin.y);
            statsGrid.offsetMax = new Vector2(0f, statsGrid.offsetMax.y);
            statsGrid.anchoredPosition = Vector2.zero;

            SetPanelAsStackItem(garageRect, 0f, garageHeight);
            SetPanelAsStackItem(careerRect, garageHeight + gap, careerHeight);
        }
        else
        {
            if (statsScrollRect)
                statsScrollRect.enabled = false;

            if (statsViewportImage)
                statsViewportImage.raycastTarget = false;

            statsGrid.anchorMin = new Vector2(0f, 0f);
            statsGrid.anchorMax = new Vector2(1f, 1f);
            statsGrid.offsetMin = Vector2.zero;
            statsGrid.offsetMax = Vector2.zero;
            statsGrid.pivot = new Vector2(0.5f, 0.5f);
            statsGrid.anchoredPosition = Vector2.zero;

            float width = Mathf.Max(viewport.rect.width, 0f);
            float height = Mathf.Max(viewport.rect.height, 0f);
            statsGrid.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            statsGrid.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

            float gap = 24f;
            float totalWidth = Mathf.Max(width, 0f);
            float careerWidth = Mathf.Clamp(totalWidth * 0.32f, 420f, 572f);
            float garageWidth = Mathf.Max(0f, totalWidth - careerWidth - gap);

            SetPanelSideBySide(garageRect, 0f, garageWidth, true);
            SetPanelSideBySide(careerRect, 0f, careerWidth, false);
        }
    }

    void SetPanelAsStackItem(RectTransform panel, float topInset, float height)
    {
        if (!panel)
            return;

        panel.anchorMin = new Vector2(0f, 1f);
        panel.anchorMax = new Vector2(1f, 1f);
        panel.pivot = new Vector2(0.5f, 1f);
        panel.offsetMin = new Vector2(0f, 0f);
        panel.offsetMax = new Vector2(0f, 0f);
        panel.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, topInset, height);
    }

    void SetPanelSideBySide(RectTransform panel, float topInset, float width, bool alignLeft)
    {
        if (!panel)
            return;

        float pivotX = alignLeft ? 0f : 1f;
        panel.anchorMin = new Vector2(pivotX, 0f);
        panel.anchorMax = new Vector2(pivotX, 1f);
        panel.pivot = new Vector2(pivotX, 0.5f);
        panel.SetInsetAndSizeFromParentEdge(alignLeft ? RectTransform.Edge.Left : RectTransform.Edge.Right, 0f, width);
        panel.offsetMin = new Vector2(panel.offsetMin.x, 0f);
        panel.offsetMax = new Vector2(panel.offsetMax.x, -topInset);
    }

    void ApplyRootSafeArea(float horizontalPadding, float topPadding, float bottomPadding)
    {
        if (!rootPanel)
            return;

        RectTransform canvasRect = rootPanel.parent as RectTransform;
        if (!canvasRect || Screen.width <= 0 || Screen.height <= 0)
        {
            rootPanel.offsetMin = new Vector2(horizontalPadding, bottomPadding);
            rootPanel.offsetMax = new Vector2(-horizontalPadding, -topPadding);
            return;
        }

        Rect safeArea = Screen.safeArea;
        float scaleX = canvasRect.rect.width / Screen.width;
        float scaleY = canvasRect.rect.height / Screen.height;

        float leftInset = safeArea.xMin * scaleX;
        float rightInset = (Screen.width - safeArea.xMax) * scaleX;
        float bottomInset = safeArea.yMin * scaleY;
        float topInset = (Screen.height - safeArea.yMax) * scaleY;

        rootPanel.offsetMin = new Vector2(horizontalPadding + leftInset, bottomPadding + bottomInset);
        rootPanel.offsetMax = new Vector2(-(horizontalPadding + rightInset), -(topPadding + topInset));
    }

    void ApplyLandscapeTypography(bool compact)
    {
        if (titleText)
            titleText.fontSize = compact ? 24f : 32f;

        ApplyNavFontSize(navDriveText, compact ? 15f : 18f);
        ApplyNavFontSize(navGarageText, compact ? 15f : 18f);
        ApplyNavFontSize(navRoutesText, compact ? 15f : 18f);
        ApplyNavFontSize(navMarketText, compact ? 15f : 18f);

        if (currencyText)
            currencyText.fontSize = compact ? 18f : 22f;

        if (statusChipText)
            statusChipText.fontSize = compact ? 11f : 13f;

        if (heroTitleText)
            heroTitleText.fontSize = compact ? 52f : 64f;

        if (heroDescriptionText)
            heroDescriptionText.fontSize = compact ? 17f : 21f;

        if (playButtonText)
            playButtonText.fontSize = compact ? 18f : 22f;

        if (customizeButtonText)
            customizeButtonText.fontSize = compact ? 18f : 22f;

        if (garageTitleText)
            garageTitleText.fontSize = compact ? 22f : 26f;

        if (garageSubtitleText)
            garageSubtitleText.fontSize = compact ? 14f : 17f;

        if (careerTitleText)
            careerTitleText.fontSize = compact ? 22f : 26f;

        if (careerSubtitleText)
            careerSubtitleText.fontSize = compact ? 14f : 17f;

        if (batteryValueText)
            batteryValueText.fontSize = compact ? 28f : 34f;

        if (capacityValueText)
            capacityValueText.fontSize = compact ? 28f : 34f;

        if (wearValueText)
            wearValueText.fontSize = compact ? 28f : 34f;

        if (speedValueText)
            speedValueText.fontSize = compact ? 28f : 34f;

        if (levelValueText)
            levelValueText.fontSize = compact ? 52f : 68f;

        if (xpValueText)
            xpValueText.fontSize = compact ? 15f : 18f;

        if (versionText)
            versionText.fontSize = compact ? 13f : 16f;

        if (maintenanceRowLabels != null)
        {
            float labelSize = compact ? 12f : 13f;
            float valueSize = compact ? 12f : 13f;
            for (int i = 0; i < maintenanceRowLabels.Length; i++)
            {
                if (maintenanceRowLabels[i])
                    maintenanceRowLabels[i].fontSize = labelSize;
                if (maintenanceRowValues != null && i < maintenanceRowValues.Length && maintenanceRowValues[i])
                    maintenanceRowValues[i].fontSize = valueSize;
            }
        }

        if (careerDetailLabels != null)
        {
            float labelSize = compact ? 11f : 12f;
            float valueSize = compact ? 12f : 13f;
            for (int i = 0; i < careerDetailLabels.Length; i++)
            {
                if (careerDetailLabels[i])
                    careerDetailLabels[i].fontSize = labelSize;
                if (careerDetailValues != null && i < careerDetailValues.Length && careerDetailValues[i])
                    careerDetailValues[i].fontSize = valueSize;
            }
        }
    }

    void SetupButtons()
    {
        playButton?.onClick.RemoveListener(OnPlay);
        customizeButton?.onClick.RemoveListener(OnCustomize);
        playButton?.onClick.AddListener(OnPlay);
        customizeButton?.onClick.AddListener(OnCustomize);
    }

    void OnPlay()
    {
        StartCoroutine(TransitionOut());
    }

    void OnCustomize()
    {
        ToggleGarage();
    }

    void LoadPrimaryAction()
    {
        var loader = SceneLoader.Instance;
        if (loader == null)
            return;

        var gameState = GameState.Instance;
        if (gameState != null && gameState.selectedRoute != null)
        {
            loader.LoadGameChecked();
            return;
        }

        var selectedCity = gameState != null ? gameState.selectedCity : null;
        var activeCity = CityManager.Instance != null ? CityManager.Instance.activeCity : null;
        if (selectedCity != null || activeCity != null)
        {
            loader.LoadRouteSelectChecked();
            return;
        }

        var selectedCountry = gameState != null ? gameState.selectedCountry : null;
        var activeCountry = CityManager.Instance != null ? CityManager.Instance.activeCountry : null;
        if (selectedCountry != null || activeCountry != null)
        {
            loader.LoadCitySelectChecked();
            return;
        }

        loader.LoadCountrySelect();
    }

    IEnumerator TransitionOut()
    {
        if (playButton) playButton.interactable = false;
        StartCoroutine(UIAnimator.PunchScale(playButton.transform));
        yield return new WaitForSeconds(0.15f);

        if (canvasGroup)
            yield return StartCoroutine(UIAnimator.FadeOut(canvasGroup, 0.3f));

        LoadPrimaryAction();
    }

    IEnumerator AnimateIn()
    {
        if (canvasGroup) canvasGroup.alpha = 0f;
        yield return new WaitForSeconds(0.1f);

        if (canvasGroup)
            yield return StartCoroutine(UIAnimator.FadeIn(canvasGroup, 0.5f));

        if (rootPanel)
            yield return StartCoroutine(
                UIAnimator.SlideInFromBottom(rootPanel, 0.4f, 40f));
    }

    void ApplyNavLabel(TextMeshProUGUI label, string text, bool isActive)
    {
        if (!label) return;

        label.text = text;
        label.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        label.fontStyle = FontStyles.Bold;
        label.characterSpacing = 6f;
        label.color = isActive ? UITheme.Accent : UITheme.WithAlpha(UITheme.TextPrimary, 0.6f);
    }

    void ApplyNavFontSize(TextMeshProUGUI label, float fontSize)
    {
        if (!label) return;
        label.fontSize = fontSize;
    }

    void ApplyPanel(Image panel)
    {
        if (!panel) return;
        panel.color = UITheme.SurfaceContainer;
    }

    void ApplySectionHeading(TextMeshProUGUI label, string text)
    {
        if (!label) return;
        label.text = text;
        label.color = UITheme.TextPrimary;
        label.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        label.characterSpacing = 3f;
        label.fontStyle = FontStyles.Bold;
    }

    void ApplySectionSubtitle(TextMeshProUGUI label, string text)
    {
        if (!label) return;
        label.text = text;
        label.color = UITheme.TextSecondary;
        label.font = UITheme.GetFont(UITheme.FontWeight.Medium);
    }

    void ApplyStatValue(TextMeshProUGUI label, string text, Color color)
    {
        if (!label) return;
        label.text = text;
        label.color = color;
        label.font = UITheme.GetFont(UITheme.FontWeight.Bold);
    }

    void ApplyStatLabel(TextMeshProUGUI label, string text)
    {
        if (!label) return;
        label.text = text;
        label.color = UITheme.TextMuted;
        label.font = UITheme.GetFont(UITheme.FontWeight.Bold);
    }

    void ApplyFill(Image fill, Color color, float widthScale)
    {
        if (!fill) return;
        fill.color = color;
        var rect = fill.rectTransform;
        rect.anchorMin = new Vector2(0f, rect.anchorMin.y);
        rect.anchorMax = new Vector2(widthScale, rect.anchorMax.y);
    }

    RectTransform FindRect(RectTransform parent, string childName)
    {
        Transform t = parent.Find(childName);
        return t ? t as RectTransform : null;
    }

    Image FindImage(RectTransform parent, string childName)
    {
        Transform t = parent.Find(childName);
        return t ? t.GetComponent<Image>() : null;
    }

    Button FindButton(Transform parent, string path)
    {
        Transform child = parent.Find(path);
        return child ? child.GetComponent<Button>() : null;
    }

    Image FindChildImage(Transform parent, string path)
    {
        Transform child = parent.Find(path);
        return child ? child.GetComponent<Image>() : null;
    }

    void CacheGarageStatLabels()
    {
        if (garagePanel == null)
            return;

        batteryLabelText = FindChildText(garagePanel.transform, "Stat_Battery/Text_Label");
        capacityLabelText = FindChildText(garagePanel.transform, "Stat_Capacity/Text_Label");
        wearLabelText = FindChildText(garagePanel.transform, "Stat_Wear/Text_Label");
        speedLabelText = FindChildText(garagePanel.transform, "Stat_Speed/Text_Label");
    }

    void EnsureGarageScreen()
    {
        if (garageScreen != null)
            return;

        var screenObject = new GameObject("GarageScreenUI", typeof(RectTransform), typeof(CanvasGroup), typeof(GarageScreenUI));
        screenObject.transform.SetParent(transform, false);
        var rect = screenObject.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        garageScreen = screenObject.GetComponent<GarageScreenUI>();
        garageScreen.Initialize(this);
    }

    void ToggleGarage()
    {
        EnsureGarageScreen();
        garageScreenVisible = !garageScreenVisible;

        if (garageScreenVisible)
            garageScreen.Show();
        else
            garageScreen.Hide();

        RefreshFleetPresentation();
    }

    public void HandleGarageVisibilityChanged(bool visible)
    {
        garageScreenVisible = visible;
        RefreshFleetPresentation();
    }

    public void RefreshGarageAndFleetPresentation()
    {
        RefreshFleetPresentation();
        RefreshGarageMetrics();
        RefreshCareerProgress();
        if (garageScreen != null && garageScreenVisible)
            garageScreen.Refresh();
    }

    void RefreshFleetPresentation()
    {
        var fleet = BusFleetManager.Instance != null ? BusFleetManager.Instance : BusFleetManager.EnsureExists();
        var spec = fleet != null ? fleet.GetSelectedBusSpec() : null;
        if (spec == null)
            return;

        ApplyNavLabel(navDriveText, "DRIVE", !garageScreenVisible);
        ApplyNavLabel(navGarageText, "GARAGE", garageScreenVisible);
        ApplyNavLabel(navRoutesText, "START PLAYING", false);
        ApplyNavLabel(navMarketText, "MARKET", false);

        var gameState = GameState.Instance;
        var city = gameState != null && gameState.selectedCity != null
            ? gameState.selectedCity
            : CityManager.Instance != null
                ? CityManager.Instance.activeCity
                : null;
        var country = gameState != null && gameState.selectedCountry != null
            ? gameState.selectedCountry
            : CityManager.Instance != null
                ? CityManager.Instance.activeCountry
                : null;
        var route = gameState != null ? gameState.selectedRoute : null;
        int availableRoutes = city != null && city.availableRoutes != null ? city.availableRoutes.Length : 0;

        if (statusChipText)
            statusChipText.text = BuildStatusLabel(spec, route, city);

        if (heroTitleText)
            heroTitleText.text = spec.displayName.ToUpper();

        if (heroDescriptionText)
            heroDescriptionText.text = BuildHeroDescription(spec, city, route, availableRoutes);

        if (heroBusImage && spec.previewSprite != null)
        {
            heroBusImage.sprite = spec.previewSprite;
            heroBusImage.color = Color.white;
            heroBusImage.preserveAspect = true;
        }

        if (garageTitleText)
            garageTitleText.text = "FLEET GARAGE";

        if (garageSubtitleText)
            garageSubtitleText.text = $"{spec.displayName}  •  {spec.PassengerCapacity} pax  •  Rank {spec.requiredRank}+";

        if (customizeButtonText)
            customizeButtonText.text = garageScreenVisible ? "CLOSE GARAGE" : "OPEN GARAGE";

        if (playButtonText)
            playButtonText.text = GetPrimaryActionLabel(route, city, country);

        ApplyStatLabel(batteryLabelText, spec.IsElectric ? "CHARGE RESERVE" : "FUEL RESERVE");
    }

    void EnsureMaintenanceDetailUI()
    {
        if (garagePanel == null || maintenanceDetailRoot != null)
            return;

        var existing = garagePanel.transform.Find("MaintenanceDetailPanel");
        if (existing != null)
        {
            maintenanceDetailRoot = existing as RectTransform;
            if (TryBindDetailRows(maintenanceDetailRoot, 5, out maintenanceRowLabels, out maintenanceRowValues, out maintenanceRowFills))
                return;
        }
        else
        {
            GameObject root = new GameObject("MaintenanceDetailPanel", typeof(RectTransform));
            root.transform.SetParent(garagePanel.transform, false);
            maintenanceDetailRoot = root.GetComponent<RectTransform>();
            maintenanceDetailRoot.anchorMin = new Vector2(0f, 1f);
            maintenanceDetailRoot.anchorMax = new Vector2(1f, 1f);
            maintenanceDetailRoot.pivot = new Vector2(0.5f, 1f);
            maintenanceDetailRoot.anchoredPosition = new Vector2(0f, -392f);
            maintenanceDetailRoot.offsetMin = new Vector2(32f, -110f);
            maintenanceDetailRoot.offsetMax = new Vector2(-32f, 0f);
        }

        string[] labels = { "Brakes", "Steer axle tyres", "Middle axle tyres", "Rear axle tyres", "Engine service" };
        CreateDetailRows(maintenanceDetailRoot, labels, 24f, 0.42f, 0.88f, out maintenanceRowLabels, out maintenanceRowValues, out maintenanceRowFills);
    }

    void EnsureCareerDetailUI()
    {
        if (careerPanel == null || careerDetailRoot != null)
            return;

        var existing = careerPanel.transform.Find("CareerDataPanel");
        if (existing != null)
        {
            careerDetailRoot = existing as RectTransform;
            if (TryBindDetailRows(careerDetailRoot, 5, out careerDetailLabels, out careerDetailValues, out careerDetailFills))
                return;
        }
        else
        {
            GameObject root = new GameObject("CareerDataPanel", typeof(RectTransform));
            root.transform.SetParent(careerPanel.transform, false);
            careerDetailRoot = root.GetComponent<RectTransform>();
            careerDetailRoot.anchorMin = new Vector2(0f, 0f);
            careerDetailRoot.anchorMax = new Vector2(1f, 0f);
            careerDetailRoot.pivot = new Vector2(0.5f, 0f);
            careerDetailRoot.anchoredPosition = new Vector2(0f, 24f);
            careerDetailRoot.sizeDelta = new Vector2(0f, 190f);
            careerDetailRoot.offsetMin = new Vector2(28f, 24f);
            careerDetailRoot.offsetMax = new Vector2(-28f, 214f);
        }

        string[] labels =
        {
            "CURRENT CITY",
            "ACTIVE ROUTE",
            "LAST PAYOUT",
            "REPUTATION",
            "NEXT UNLOCK"
        };
        CreateDetailRows(careerDetailRoot, labels, 36f, 0.42f, 0.78f, out careerDetailLabels, out careerDetailValues, out careerDetailFills);
    }

    void RefreshGarageMetrics()
    {
        var gameState = GameState.Instance;
        if (gameState == null || gameState.vehicleState == null)
            return;

        var vehicle = gameState.vehicleState;
        var fleet = BusFleetManager.Instance != null ? BusFleetManager.Instance : BusFleetManager.EnsureExists();
        var spec = fleet != null ? fleet.GetSelectedBusSpec() : null;
        string unitLabel = !string.IsNullOrWhiteSpace(vehicle.energyUnitLabel)
            ? vehicle.energyUnitLabel
            : spec != null && !string.IsNullOrWhiteSpace(spec.energyUnitLabel)
                ? spec.energyUnitLabel
                : "L";
        float fuelCapacity = Mathf.Max(1f, vehicle.fuelCapacityLitres);
        float fuelPercent = Mathf.Clamp01(vehicle.fuelLitres / fuelCapacity);
        Color fuelColor = fuelPercent <= 0.05f
            ? UITheme.Error
            : fuelPercent <= 0.20f
                ? UITheme.Tertiary
                : UITheme.Secondary;

        float brakeCondition = Mathf.Clamp01(1f - vehicle.brakeWearNormalized);
        float tyreAverageCondition = 1f;
        if (vehicle.axleTyreWearNormalized != null && vehicle.axleTyreWearNormalized.Length > 0)
        {
            float tyreWearTotal = 0f;
            for (int i = 0; i < vehicle.axleTyreWearNormalized.Length; i++)
                tyreWearTotal += vehicle.axleTyreWearNormalized[i];
            tyreAverageCondition = 1f - (tyreWearTotal / vehicle.axleTyreWearNormalized.Length);
        }

        float engineCondition = Mathf.Clamp01(1f - (vehicle.engineHours / 250f));

        if (currencyText && gameState.economy != null)
            currencyText.text = $"KES {gameState.economy.balanceKES:N0}";

        ApplyStatValue(batteryValueText, $"{vehicle.fuelLitres:F0} / {fuelCapacity:F0}{unitLabel}", fuelColor);
        ApplyFill(batteryFill, fuelColor, fuelPercent);

        ApplyStatValue(capacityValueText, $"{brakeCondition * 100f:F0}%", GetConditionColor(brakeCondition));
        ApplyFill(capacityFill, GetConditionColor(brakeCondition), brakeCondition);

        ApplyStatValue(wearValueText, $"{tyreAverageCondition * 100f:F0}%", GetConditionColor(tyreAverageCondition));
        ApplyFill(wearFill, GetConditionColor(tyreAverageCondition), tyreAverageCondition);

        ApplyStatValue(speedValueText, $"{engineCondition * 100f:F0}%", GetConditionColor(engineCondition));
        ApplyFill(speedFill, GetConditionColor(engineCondition), engineCondition);

        if (maintenanceRowLabels == null || maintenanceRowValues == null || maintenanceRowFills == null)
            return;

        SetMaintenanceRow(0, "Brakes", $"{brakeCondition * 100f:F0}%", brakeCondition);
        SetMaintenanceRow(1, "Steer axle tyres", $"{GetAxleCondition(vehicle, 0):F0}%", GetAxleCondition(vehicle, 0) / 100f);
        SetMaintenanceRow(2, "Middle axle tyres", $"{GetAxleCondition(vehicle, 1):F0}%", GetAxleCondition(vehicle, 1) / 100f);
        SetMaintenanceRow(3, "Rear axle tyres", $"{GetAxleCondition(vehicle, 2):F0}%", GetAxleCondition(vehicle, 2) / 100f);
        SetMaintenanceRow(4, "Engine service", $"{engineCondition * 100f:F0}%  •  {vehicle.engineHours:F1}h", engineCondition);
    }

    void RefreshCareerProgress()
    {
        var xpSystem = XPSystem.Instance != null ? XPSystem.Instance : XPSystem.EnsureExists();
        var snapshot = xpSystem != null ? xpSystem.GetProgressSnapshot() : default;
        var gameState = GameState.Instance;
        var city = gameState != null && gameState.selectedCity != null
            ? gameState.selectedCity
            : CityManager.Instance != null
                ? CityManager.Instance.activeCity
                : null;
        var country = gameState != null && gameState.selectedCountry != null
            ? gameState.selectedCountry
            : CityManager.Instance != null
                ? CityManager.Instance.activeCountry
                : null;
        var route = gameState != null ? gameState.selectedRoute : null;
        float reputation = gameState != null && gameState.economy != null
            ? gameState.economy.driverReputationRating
            : 0f;
        float payout = gameState != null && gameState.lastMissionSettlement != null
            ? gameState.lastMissionSettlement.netEarningsKES
            : 0f;
        int availableRoutes = city != null && city.availableRoutes != null ? city.availableRoutes.Length : 0;

        if (careerSubtitleText)
            careerSubtitleText.text = string.IsNullOrWhiteSpace(snapshot.currentRankTitle)
                ? "Driver level and dispatch readiness"
                : snapshot.currentRankTitle;

        if (levelValueText)
        {
            levelValueText.text = snapshot.currentRank > 0 ? snapshot.currentRank.ToString() : "1";
            levelValueText.color = UITheme.TextPrimary;
            levelValueText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }

        if (xpValueText)
        {
            xpValueText.text = snapshot.isMaxRank
                ? $"MAX RANK  •  {snapshot.totalXP:N0} XP"
                : $"{snapshot.xpIntoCurrentRank:N0} / {snapshot.xpRequiredForNextRank:N0} XP  •  {snapshot.xpToNextRank:N0} TO RANK {snapshot.currentRank + 1}";
            xpValueText.color = UITheme.TextSecondary;
            xpValueText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
        }

        ApplyFill(xpFill, UITheme.Accent, snapshot.currentRank > 0 ? Mathf.Clamp01(snapshot.progress01) : 0f);

        if (careerDetailLabels == null || careerDetailValues == null || careerDetailFills == null)
            return;

        SetDetailRow(0, "CURRENT CITY", BuildCitySummary(city, country), city != null ? UITheme.Accent : UITheme.TextMuted, city != null ? 1f : 0.08f);
        SetDetailRow(1, "ACTIVE ROUTE", BuildRouteSummary(route, availableRoutes), route != null ? UITheme.Secondary : UITheme.TextSecondary, route != null ? 1f : Mathf.Clamp01(availableRoutes / 8f));
        SetDetailRow(2, "LAST PAYOUT", BuildPayoutSummary(payout), payout > 0f ? UITheme.Success : UITheme.TextSecondary, Mathf.Clamp01(Mathf.Abs(payout) / 50000f));
        SetDetailRow(3, "REPUTATION", $"{reputation:F0} / 100", GetConditionColor(Mathf.Clamp01(reputation / 100f)), Mathf.Clamp01(reputation / 100f));
        SetDetailRow(4, "NEXT UNLOCK", BuildNextUnlockSummary(snapshot), snapshot.isMaxRank ? UITheme.Success : UITheme.Secondary, snapshot.currentRank > 0 ? Mathf.Clamp01(snapshot.progress01) : 0.1f);
    }

    void SetMaintenanceRow(int index, string label, string value, float normalized)
    {
        if (maintenanceRowLabels == null || index < 0 || index >= maintenanceRowLabels.Length)
            return;

        if (maintenanceRowLabels[index])
        {
            maintenanceRowLabels[index].text = label;
            maintenanceRowLabels[index].color = UITheme.TextSecondary;
            maintenanceRowLabels[index].font = UITheme.GetFont(UITheme.FontWeight.Medium);
        }

        if (maintenanceRowValues != null && index < maintenanceRowValues.Length && maintenanceRowValues[index])
        {
            maintenanceRowValues[index].text = value;
            maintenanceRowValues[index].color = GetConditionColor(normalized);
            maintenanceRowValues[index].font = UITheme.GetFont(UITheme.FontWeight.Medium);
        }

        if (maintenanceRowFills != null && index < maintenanceRowFills.Length && maintenanceRowFills[index])
        {
            maintenanceRowFills[index].color = GetConditionColor(normalized);
            var rect = maintenanceRowFills[index].rectTransform;
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(Mathf.Clamp01(normalized), 1f);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }
    }

    void SetDetailRow(int index, string label, string value, Color color, float normalized)
    {
        if (careerDetailLabels == null || careerDetailValues == null || careerDetailFills == null)
            return;
        if (index < 0 || index >= careerDetailLabels.Length)
            return;

        if (careerDetailLabels[index])
        {
            careerDetailLabels[index].text = label;
            careerDetailLabels[index].color = UITheme.TextMuted;
            careerDetailLabels[index].font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }

        if (careerDetailValues[index])
        {
            careerDetailValues[index].text = value;
            careerDetailValues[index].color = color;
            careerDetailValues[index].font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }

        if (careerDetailFills[index])
        {
            careerDetailFills[index].color = color;
            RectTransform rect = careerDetailFills[index].rectTransform;
            rect.anchorMin = new Vector2(0f, 0f);
            rect.anchorMax = new Vector2(Mathf.Clamp01(normalized), 1f);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }
    }

    float GetAxleCondition(VehiclePersistentState vehicle, int axleIndex)
    {
        if (vehicle.axleTyreWearNormalized == null || axleIndex < 0 || axleIndex >= vehicle.axleTyreWearNormalized.Length)
            return 100f;
        return (1f - Mathf.Clamp01(vehicle.axleTyreWearNormalized[axleIndex])) * 100f;
    }

    Color GetConditionColor(float normalized)
    {
        if (normalized <= 0.15f) return UITheme.Error;
        if (normalized <= 0.5f) return UITheme.Tertiary;
        return UITheme.Success;
    }

    bool TryBindDetailRows(RectTransform root, int rowCount, out TextMeshProUGUI[] labels, out TextMeshProUGUI[] values, out Image[] fills)
    {
        labels = new TextMeshProUGUI[rowCount];
        values = new TextMeshProUGUI[rowCount];
        fills = new Image[rowCount];

        for (int i = 0; i < rowCount; i++)
        {
            labels[i] = FindChildText(root, $"Row_{i}/Text_Label");
            values[i] = FindChildText(root, $"Row_{i}/Text_Value");
            fills[i] = FindChildImage(root, $"Row_{i}/Bar_BG/Bar_Fill");

            if (!labels[i] || !values[i] || !fills[i])
                return false;
        }

        return true;
    }

    void CreateDetailRows(RectTransform root, string[] labels, float verticalSpacing, float barMinAnchor, float barMaxAnchor, out TextMeshProUGUI[] labelTexts, out TextMeshProUGUI[] valueTexts, out Image[] fillImages)
    {
        labelTexts = new TextMeshProUGUI[labels.Length];
        valueTexts = new TextMeshProUGUI[labels.Length];
        fillImages = new Image[labels.Length];

        for (int i = 0; i < labels.Length; i++)
        {
            float top = -i * verticalSpacing;
            GameObject row = new GameObject($"Row_{i}", typeof(RectTransform));
            row.transform.SetParent(root, false);
            RectTransform rowRect = row.GetComponent<RectTransform>();
            rowRect.anchorMin = new Vector2(0f, 1f);
            rowRect.anchorMax = new Vector2(1f, 1f);
            rowRect.pivot = new Vector2(0.5f, 1f);
            rowRect.anchoredPosition = new Vector2(0f, top);
            rowRect.sizeDelta = new Vector2(0f, 28f);

            labelTexts[i] = CreateText("Text_Label", row.transform, labels[i], new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 0f), 180f, TextAlignmentOptions.Left);
            valueTexts[i] = CreateText("Text_Value", row.transform, "-", new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(0f, 0f), 180f, TextAlignmentOptions.Right);

            GameObject barBg = new GameObject("Bar_BG", typeof(RectTransform), typeof(Image));
            barBg.transform.SetParent(row.transform, false);
            RectTransform bgRect = barBg.GetComponent<RectTransform>();
            bgRect.anchorMin = new Vector2(barMinAnchor, 0.5f);
            bgRect.anchorMax = new Vector2(barMaxAnchor, 0.5f);
            bgRect.pivot = new Vector2(0.5f, 0.5f);
            bgRect.sizeDelta = new Vector2(0f, 8f);
            bgRect.anchoredPosition = Vector2.zero;
            barBg.GetComponent<Image>().color = UITheme.Surface;

            GameObject fill = new GameObject("Bar_Fill", typeof(RectTransform), typeof(Image));
            fill.transform.SetParent(barBg.transform, false);
            RectTransform fillRect = fill.GetComponent<RectTransform>();
            fillRect.anchorMin = new Vector2(0f, 0f);
            fillRect.anchorMax = new Vector2(0f, 1f);
            fillRect.offsetMin = Vector2.zero;
            fillRect.offsetMax = Vector2.zero;
            fillImages[i] = fill.GetComponent<Image>();
            fillImages[i].color = UITheme.Accent;
        }
    }

    TextMeshProUGUI FindChildText(Transform parent, string path)
    {
        var child = parent.Find(path);
        return child ? child.GetComponent<TextMeshProUGUI>() : null;
    }

    string BuildStatusLabel(BusSpec spec, BusRoute route, CityDefinition city)
    {
        string energy = spec != null && spec.IsElectric ? "ELECTRIC" : "DIESEL";
        if (route != null)
            return $"CURRENTLY EQUIPPED • {energy} • {route.routeName.ToUpper()}";
        if (city != null)
            return $"CURRENTLY EQUIPPED • {energy} • {city.cityName.ToUpper()}";
        return $"CURRENTLY EQUIPPED • {energy}";
    }

    string BuildHeroDescription(BusSpec spec, CityDefinition city, BusRoute route, int availableRoutes)
    {
        if (spec == null)
            return "Choose the right vehicle for the route, the crowd, and the shift.";

        if (route != null)
        {
            string routeNumber = string.IsNullOrWhiteSpace(route.routeNumber) ? "Route" : route.routeNumber;
            return $"{spec.description} Assigned to {routeNumber} • {route.routeName} for the next dispatch.";
        }

        if (city != null)
            return $"{spec.description} {city.cityName} is live with {availableRoutes} routes ready for dispatch.";

        return spec.description;
    }

    string GetPrimaryActionLabel(BusRoute route, CityDefinition city, CountryDefinition country)
    {
        if (route != null)
            return "CONTINUE ROUTE";
        if (city != null)
            return "SELECT ROUTE";
        if (country != null)
            return "SELECT CITY";
        return "START DRIVING";
    }

    string BuildCitySummary(CityDefinition city, CountryDefinition country)
    {
        if (city != null)
            return $"{city.cityName} • {city.country}";
        if (country != null)
            return $"{country.countryName} • Pick a city";
        return "No deployment selected";
    }

    string BuildRouteSummary(BusRoute route, int availableRoutes)
    {
        if (route != null)
        {
            string routeNumber = string.IsNullOrWhiteSpace(route.routeNumber) ? "LINE" : route.routeNumber;
            return $"{routeNumber} • {route.routeName}";
        }

        if (availableRoutes > 0)
            return $"{availableRoutes} routes ready";

        return "No route selected";
    }

    string BuildPayoutSummary(float payout)
    {
        if (Mathf.Abs(payout) < 0.01f)
            return "No shift payout yet";
        return $"KES {payout:N0} net";
    }

    string BuildNextUnlockSummary(RankProgressSnapshot snapshot)
    {
        if (snapshot.isMaxRank)
            return "Master operator status reached";
        if (!string.IsNullOrWhiteSpace(snapshot.nextRankUnlockReveal))
            return $"Rank {snapshot.currentRank + 1} • {snapshot.nextRankUnlockReveal}";
        return $"Rank {snapshot.currentRank + 1} incoming";
    }

    TextMeshProUGUI CreateText(string name, Transform parent, string text, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot, Vector2 anchoredPosition, float width, TextAlignmentOptions alignment)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI));
        go.transform.SetParent(parent, false);
        RectTransform rect = go.GetComponent<RectTransform>();
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.pivot = pivot;
        rect.anchoredPosition = anchoredPosition;
        rect.sizeDelta = new Vector2(width, 18f);

        var tmp = go.GetComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.alignment = alignment;
        tmp.font = UITheme.GetFont(UITheme.FontWeight.Medium);
        tmp.color = UITheme.TextSecondary;
        tmp.fontSize = 13f;
        return tmp;
    }
}
