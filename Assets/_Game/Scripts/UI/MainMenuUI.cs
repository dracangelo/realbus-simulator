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

    [Header("Career Panel")]
    public Image careerPanel;
    public TextMeshProUGUI careerTitleText;
    public TextMeshProUGUI careerSubtitleText;
    public TextMeshProUGUI levelValueText;
    public TextMeshProUGUI xpValueText;
    public Image xpFill;
    public TextMeshProUGUI versionText;

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

    void Start()
    {
        AutoBindLayoutReferences();

        CacheStaticStatsLayout();

        ApplyTheme();
        Canvas.ForceUpdateCanvases();
        ApplyResponsiveLayout(force: true);
        SetupButtons();
        StartCoroutine(AnimateIn());
    }

    void Update()
    {
        ApplyResponsiveLayout();
    }

    void ApplyTheme()
    {
        if (backgroundPanel)
            backgroundPanel.color = UITheme.Background;

        if (heroCardBackground)
            heroCardBackground.color = UITheme.SurfaceContainer;

        if (heroGradientOverlay)
            heroGradientOverlay.color = UITheme.WithAlpha(UITheme.Background, 0.58f);

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
        ApplyNavLabel(navRoutesText, "ROUTES", false);
        ApplyNavLabel(navMarketText, "MARKET", false);

        if (currencyBadge)
            currencyBadge.color = UITheme.Surface;

        if (currencyText)
        {
            currencyText.text = "$254,000";
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
            heroTitleText.text = "VOLTA S-SERIES";
            heroTitleText.color = UITheme.TextPrimary;
            heroTitleText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            heroTitleText.fontStyle = FontStyles.Bold | FontStyles.Italic;
        }

        if (heroDescriptionText)
        {
            heroDescriptionText.text = "Next-gen electric propulsion. Optimized for high-density metropolitan routes with adaptive air suspension.";
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
            customizeButtonText.text = "CUSTOMIZE";
            customizeButtonText.color = UITheme.TextPrimary;
            customizeButtonText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            customizeButtonText.characterSpacing = 4f;
        }

        ApplyPanel(garagePanel);
        ApplyPanel(careerPanel);

        ApplySectionHeading(garageTitleText, "MY GARAGE");
        ApplySectionSubtitle(garageSubtitleText, "Technical Performance Analysis");
        ApplySectionHeading(careerTitleText, "CAREER");
        ApplySectionSubtitle(careerSubtitleText, "Driver Level & Experience");

        ApplyStatValue(batteryValueText, "420 KM", UITheme.TextPrimary);
        ApplyStatValue(capacityValueText, "85 PAX", UITheme.TextPrimary);
        ApplyStatValue(wearValueText, "12%", UITheme.Error);
        ApplyStatValue(speedValueText, "115 KPH", UITheme.TextPrimary);

        ApplyFill(batteryFill, UITheme.Secondary, 0.85f);
        ApplyFill(capacityFill, UITheme.TertiaryDim, 0.70f);
        ApplyFill(wearFill, UITheme.Error, 0.12f);
        ApplyFill(speedFill, UITheme.Accent, 0.92f);

        if (levelValueText)
        {
            levelValueText.text = "24";
            levelValueText.color = UITheme.TextPrimary;
            levelValueText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }

        if (xpValueText)
        {
            xpValueText.text = "8,420 / 10,000 XP";
            xpValueText.color = UITheme.TextSecondary;
            xpValueText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
        }

        ApplyFill(xpFill, UITheme.Accent, 0.84f);

        if (versionText)
        {
            versionText.text = "v0.1.0 — Early Access";
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

        if (!garagePanel && statsGrid)
            garagePanel = FindImage(statsGrid, "GaragePanel");

        if (!careerPanel && statsGrid)
            careerPanel = FindImage(statsGrid, "CareerPanel");
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
            float garageHeight = 344f;
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
    }

    void SetupButtons()
    {
        playButton?.onClick.AddListener(OnPlay);
        customizeButton?.onClick.AddListener(OnCustomize);
    }

    void OnPlay()
    {
        StartCoroutine(TransitionOut());
    }

    void OnCustomize()
    {
        Debug.Log("Customize bus — Phase 7");
    }

    IEnumerator TransitionOut()
    {
        if (playButton) playButton.interactable = false;
        StartCoroutine(UIAnimator.PunchScale(playButton.transform));
        yield return new WaitForSeconds(0.15f);

        if (canvasGroup)
            yield return StartCoroutine(UIAnimator.FadeOut(canvasGroup, 0.3f));

        SceneLoader.Instance?.LoadCountrySelect();
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
}
