using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GarageScreenUI : MonoBehaviour
{
    MainMenuUI owner;
    CanvasGroup canvasGroup;
    RectTransform shell;
    RectTransform listContent;
    Button equipButton;
    TextMeshProUGUI equipButtonText;
    TextMeshProUGUI subtitleText;
    TextMeshProUGUI rankText;
    TextMeshProUGUI currentNameText;
    TextMeshProUGUI compareNameText;
    TextMeshProUGUI comparisonHintText;
    GarageProgressionPanel progressionPanel;

    readonly List<GameObject> spawnedCards = new List<GameObject>();

    BusSpec focusedSpec;
    bool built;
    bool isVisible;

    public bool IsVisible => isVisible;

    public void Initialize(MainMenuUI menuOwner)
    {
        owner = menuOwner;
        EnsureBuilt();
        Refresh();
        HideImmediate();
    }

    public void Show()
    {
        EnsureBuilt();
        gameObject.SetActive(true);
        Refresh();
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        isVisible = true;
    }

    public void Hide()
    {
        if (!built)
            return;

        HideImmediate();
        owner?.HandleGarageVisibilityChanged(false);
    }

    public void Refresh()
    {
        EnsureBuilt();
        var fleet = BusFleetManager.EnsureExists();
        var equipped = fleet.GetSelectedBusSpec();
        focusedSpec = focusedSpec != null ? focusedSpec : equipped;

        if (focusedSpec == null)
            return;

        if (NeedsSelectionFallback(fleet, equipped))
            focusedSpec = equipped;

        PopulateList(fleet);
        RefreshComparison(fleet, equipped, focusedSpec);
    }

    bool NeedsSelectionFallback(BusFleetManager fleet, BusSpec equipped)
    {
        return focusedSpec == null || fleet.GetBusSpecById(focusedSpec.busId) == null || equipped == null;
    }

    void EnsureBuilt()
    {
        if (built)
            return;

        built = true;
        BuildUi();
    }

    void BuildUi()
    {
        var rootRect = gameObject.GetComponent<RectTransform>();
        if (rootRect == null)
            rootRect = gameObject.AddComponent<RectTransform>();

        rootRect.anchorMin = Vector2.zero;
        rootRect.anchorMax = Vector2.one;
        rootRect.offsetMin = Vector2.zero;
        rootRect.offsetMax = Vector2.zero;

        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        var background = CreatePanel("Dim", transform, UITheme.WithAlpha(UITheme.Background, 0.84f));
        Stretch(background.rectTransform);

        shell = CreatePanel("Shell", transform, UITheme.SurfaceContainer).rectTransform;
        shell.anchorMin = new Vector2(0.06f, 0.08f);
        shell.anchorMax = new Vector2(0.94f, 0.92f);
        shell.offsetMin = Vector2.zero;
        shell.offsetMax = Vector2.zero;

        CreateHeader();
        CreateBody();
        CreateFooter();
    }

    void CreateHeader()
    {
        var title = CreateText("Title", shell, "GARAGE", 30f, UITheme.TextPrimary, UITheme.FontWeight.Bold, TextAlignmentOptions.Left);
        Anchor(title.rectTransform, new Vector2(0f, 1f), new Vector2(0.4f, 1f), new Vector2(28f, -18f), new Vector2(-12f, -54f));

        subtitleText = CreateText("Subtitle", shell, string.Empty, 15f, UITheme.TextSecondary, UITheme.FontWeight.Medium, TextAlignmentOptions.Left);
        Anchor(subtitleText.rectTransform, new Vector2(0f, 1f), new Vector2(0.56f, 1f), new Vector2(28f, -58f), new Vector2(-12f, -88f));

        rankText = CreateText("Rank", shell, string.Empty, 14f, UITheme.Accent, UITheme.FontWeight.Bold, TextAlignmentOptions.Right);
        Anchor(rankText.rectTransform, new Vector2(0.62f, 1f), new Vector2(0.86f, 1f), new Vector2(0f, -26f), new Vector2(0f, -54f));

        var closeButtonObj = CreateButton("Close", shell, UITheme.SurfaceHigh, out var closeLabel);
        closeLabel.text = "CLOSE";
        closeLabel.color = UITheme.TextPrimary;
        closeLabel.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        closeButtonObj.onClick.AddListener(Hide);
        Anchor(closeButtonObj.GetComponent<RectTransform>(), new Vector2(0.86f, 1f), new Vector2(1f, 1f), new Vector2(-132f, -20f), new Vector2(-28f, -58f));
    }

    void CreateBody()
    {
        var listPanel = CreatePanel("ListPanel", shell, UITheme.Surface).rectTransform;
        listPanel.anchorMin = new Vector2(0f, 0f);
        listPanel.anchorMax = new Vector2(0.34f, 1f);
        listPanel.offsetMin = new Vector2(28f, 86f);
        listPanel.offsetMax = new Vector2(-14f, -86f);

        var listTitle = CreateText("ListTitle", listPanel, "FLEET", 16f, UITheme.TextMuted, UITheme.FontWeight.Bold, TextAlignmentOptions.Left);
        Anchor(listTitle.rectTransform, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(18f, -16f), new Vector2(-18f, -40f));

        CreateScrollList(listPanel);

        var comparePanel = CreatePanel("ComparePanel", shell, UITheme.Surface).rectTransform;
        comparePanel.anchorMin = new Vector2(0.34f, 0f);
        comparePanel.anchorMax = new Vector2(1f, 1f);
        comparePanel.offsetMin = new Vector2(14f, 86f);
        comparePanel.offsetMax = new Vector2(-28f, -86f);

        var compareTitle = CreateText("CompareTitle", comparePanel, "SPEC COMPARISON", 16f, UITheme.TextMuted, UITheme.FontWeight.Bold, TextAlignmentOptions.Left);
        Anchor(compareTitle.rectTransform, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(24f, -16f), new Vector2(-24f, -40f));

        currentNameText = CreateText("CurrentName", comparePanel, string.Empty, 26f, UITheme.TextPrimary, UITheme.FontWeight.Bold, TextAlignmentOptions.Left);
        Anchor(currentNameText.rectTransform, new Vector2(0f, 1f), new Vector2(0.48f, 1f), new Vector2(24f, -58f), new Vector2(-12f, -96f));

        compareNameText = CreateText("CompareName", comparePanel, string.Empty, 26f, UITheme.Accent, UITheme.FontWeight.Bold, TextAlignmentOptions.Left);
        Anchor(compareNameText.rectTransform, new Vector2(0.52f, 1f), new Vector2(1f, 1f), new Vector2(12f, -58f), new Vector2(-24f, -96f));

        comparisonHintText = CreateText("Hint", comparePanel, string.Empty, 14f, UITheme.TextSecondary, UITheme.FontWeight.Regular, TextAlignmentOptions.Left);
        Anchor(comparisonHintText.rectTransform, new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(24f, 18f), new Vector2(-24f, 48f));

        CreateSpecTable(comparePanel);
    }

    void CreateScrollList(RectTransform parent)
    {
        var viewportObj = new GameObject("Viewport", typeof(RectTransform), typeof(Image), typeof(Mask), typeof(ScrollRect));
        viewportObj.transform.SetParent(parent, false);
        var viewport = viewportObj.GetComponent<RectTransform>();
        viewport.anchorMin = new Vector2(0f, 0f);
        viewport.anchorMax = new Vector2(1f, 1f);
        viewport.offsetMin = new Vector2(14f, 14f);
        viewport.offsetMax = new Vector2(-14f, -52f);

        var viewportImage = viewportObj.GetComponent<Image>();
        viewportImage.color = UITheme.WithAlpha(UITheme.Background, 1f);
        viewportImage.raycastTarget = true;
        viewportObj.GetComponent<Mask>().showMaskGraphic = false;

        var contentObj = new GameObject("Content", typeof(RectTransform), typeof(VerticalLayoutGroup), typeof(ContentSizeFitter));
        contentObj.transform.SetParent(viewport, false);
        listContent = contentObj.GetComponent<RectTransform>();
        listContent.anchorMin = new Vector2(0f, 1f);
        listContent.anchorMax = new Vector2(1f, 1f);
        listContent.pivot = new Vector2(0.5f, 1f);
        listContent.offsetMin = Vector2.zero;
        listContent.offsetMax = Vector2.zero;

        var layout = contentObj.GetComponent<VerticalLayoutGroup>();
        layout.spacing = 10f;
        layout.padding = new RectOffset(0, 0, 0, 0);
        layout.childControlHeight = true;
        layout.childControlWidth = true;
        layout.childForceExpandHeight = false;
        layout.childForceExpandWidth = true;

        var fitter = contentObj.GetComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

        var scrollRect = viewportObj.GetComponent<ScrollRect>();
        scrollRect.viewport = viewport;
        scrollRect.content = listContent;
        scrollRect.horizontal = false;
        scrollRect.vertical = true;
        scrollRect.movementType = ScrollRect.MovementType.Clamped;
        scrollRect.scrollSensitivity = 28f;
    }

    void CreateSpecTable(RectTransform parent)
    {
        string[] labels =
        {
            "Unlock rank",
            "Energy",
            "Capacity",
            "Seated / standing",
            "Body length",
            "Cruise reserve",
            "Urban consumption",
            "Mass",
            "Steering",
            "Brake torque"
        };

        for (int i = 0; i < labels.Length; i++)
        {
            float top = -112f - (i * 44f);

            var label = CreateText($"RowLabel_{i}", parent, labels[i].ToUpper(), 13f, UITheme.TextMuted, UITheme.FontWeight.Bold, TextAlignmentOptions.Left);
            Anchor(label.rectTransform, new Vector2(0f, 1f), new Vector2(0.28f, 1f), new Vector2(24f, top), new Vector2(-10f, top - 28f));

            var left = CreateText($"RowLeft_{i}", parent, string.Empty, 16f, UITheme.TextPrimary, UITheme.FontWeight.Medium, TextAlignmentOptions.Left);
            Anchor(left.rectTransform, new Vector2(0.28f, 1f), new Vector2(0.52f, 1f), new Vector2(6f, top), new Vector2(-12f, top - 28f));

            var right = CreateText($"RowRight_{i}", parent, string.Empty, 16f, UITheme.Accent, UITheme.FontWeight.Bold, TextAlignmentOptions.Left);
            Anchor(right.rectTransform, new Vector2(0.52f, 1f), new Vector2(1f, 1f), new Vector2(12f, top), new Vector2(-24f, top - 28f));
        }
    }

    void CreateFooter()
    {
        var footer = CreatePanel("Footer", shell, UITheme.BackgroundAlt).rectTransform;
        footer.anchorMin = new Vector2(0f, 0f);
        footer.anchorMax = new Vector2(1f, 0f);
        footer.offsetMin = new Vector2(28f, 22f);
        footer.offsetMax = new Vector2(-28f, 70f);

        var upgradesButton = CreateButton("Upgrades", footer, UITheme.SurfaceHigh, out var upgradesLabel);
        upgradesLabel.text = "UPGRADES";
        upgradesButton.onClick.AddListener(ShowUpgrades);
        Anchor(upgradesButton.GetComponent<RectTransform>(), new Vector2(0f, 0f), new Vector2(0.22f, 1f), new Vector2(16f, 10f), new Vector2(-8f, -10f));

        var liveryButton = CreateButton("Livery", footer, UITheme.SurfaceHigh, out var liveryLabel);
        liveryLabel.text = "LIVERY";
        liveryButton.onClick.AddListener(ShowLivery);
        Anchor(liveryButton.GetComponent<RectTransform>(), new Vector2(0.22f, 0f), new Vector2(0.44f, 1f), new Vector2(8f, 10f), new Vector2(-8f, -10f));

        equipButton = CreateButton("Equip", footer, UITheme.Accent, out equipButtonText);
        equipButton.onClick.AddListener(HandleEquipPressed);
        Anchor(equipButton.GetComponent<RectTransform>(), new Vector2(0.72f, 0f), new Vector2(1f, 1f), new Vector2(8f, 10f), new Vector2(-16f, -10f));
    }

    void PopulateList(BusFleetManager fleet)
    {
        for (int i = 0; i < spawnedCards.Count; i++)
        {
            if (spawnedCards[i] != null)
                Destroy(spawnedCards[i]);
        }
        spawnedCards.Clear();

        var specs = fleet.GetAllBusSpecs();
        for (int i = 0; i < specs.Count; i++)
        {
            var spec = specs[i];
            if (spec == null)
                continue;

            var card = CreateBusCard(fleet, spec);
            spawnedCards.Add(card);
        }
    }

    GameObject CreateBusCard(BusFleetManager fleet, BusSpec spec)
    {
        var card = new GameObject($"Card_{spec.busId}", typeof(RectTransform), typeof(Image), typeof(Button), typeof(LayoutElement));
        card.transform.SetParent(listContent, false);

        var image = card.GetComponent<Image>();
        image.color = fleet.IsSelected(spec)
            ? UITheme.WithAlpha(UITheme.Accent, 0.18f)
            : UITheme.SurfaceContainer;

        var layout = card.GetComponent<LayoutElement>();
        layout.preferredHeight = 106f;

        var button = card.GetComponent<Button>();
        var colors = button.colors;
        colors.normalColor = image.color;
        colors.highlightedColor = UITheme.SurfaceBright;
        colors.pressedColor = UITheme.WithAlpha(UITheme.SurfaceHigh, 0.82f);
        button.colors = colors;
        button.onClick.AddListener(() =>
        {
            focusedSpec = spec;
            Refresh();
        });

        var accent = CreatePanel("Accent", card.transform, fleet.IsBusOwned(spec) ? UITheme.Success : UITheme.Outline).rectTransform;
        accent.anchorMin = new Vector2(0f, 0f);
        accent.anchorMax = new Vector2(0f, 1f);
        accent.sizeDelta = new Vector2(6f, 0f);
        accent.anchoredPosition = Vector2.zero;

        var name = CreateText("Name", card.transform, spec.displayName, 20f, UITheme.TextPrimary, UITheme.FontWeight.Bold, TextAlignmentOptions.Left);
        Anchor(name.rectTransform, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(22f, -16f), new Vector2(-16f, -42f));

        string energy = spec.IsElectric ? "Electric" : "Diesel";
        var meta = CreateText("Meta", card.transform, $"{energy}  •  {spec.PassengerCapacity} pax  •  {spec.bodyLengthMeters:0.#}m", 13f, UITheme.TextSecondary, UITheme.FontWeight.Medium, TextAlignmentOptions.Left);
        Anchor(meta.rectTransform, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(22f, -46f), new Vector2(-16f, -66f));

        string status = fleet.IsSelected(spec)
            ? "Equipped"
            : fleet.IsBusOwned(spec)
                ? "Owned"
                : $"Locked • Rank {fleet.GetRequiredRank(spec)}";
        Color statusColor = fleet.IsSelected(spec) ? UITheme.Accent : fleet.IsBusOwned(spec) ? UITheme.Success : UITheme.TextMuted;
        var statusText = CreateText("Status", card.transform, status.ToUpper(), 12f, statusColor, UITheme.FontWeight.Bold, TextAlignmentOptions.Left);
        Anchor(statusText.rectTransform, new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(22f, 14f), new Vector2(-16f, 34f));

        return card;
    }

    void RefreshComparison(BusFleetManager fleet, BusSpec equipped, BusSpec candidate)
    {
        int currentRank = XPSystem.Instance != null ? XPSystem.Instance.GetProgressSnapshot().currentRank : 1;
        subtitleText.text = "Fleet selection and role fit";
        rankText.text = $"RANK {currentRank}";

        currentNameText.text = equipped != null ? equipped.displayName : "No bus selected";
        compareNameText.text = candidate != null ? candidate.displayName : "No bus focused";

        comparisonHintText.text = candidate != null && fleet.IsBusOwned(candidate)
            ? "Select an owned bus to equip it for the next drive."
            : candidate != null
                ? $"Locked until rank {fleet.GetRequiredRank(candidate)}."
                : string.Empty;

        if (equipped == null || candidate == null)
            return;

        SetComparisonRow(0, equipped.requiredRank.ToString(), candidate.requiredRank.ToString());
        SetComparisonRow(1, equipped.IsElectric ? "Electric" : "Diesel", candidate.IsElectric ? "Electric" : "Diesel");
        SetComparisonRow(2, $"{equipped.PassengerCapacity} pax", $"{candidate.PassengerCapacity} pax");
        SetComparisonRow(3, $"{equipped.SeatedCapacity} / {equipped.StandingCapacity}", $"{candidate.SeatedCapacity} / {candidate.StandingCapacity}");
        SetComparisonRow(4, $"{equipped.bodyLengthMeters:0.#} m", $"{candidate.bodyLengthMeters:0.#} m");
        SetComparisonRow(5, $"{equipped.energyCapacityUnits:0} {equipped.energyUnitLabel}", $"{candidate.energyCapacityUnits:0} {candidate.energyUnitLabel}");
        SetComparisonRow(6, $"{equipped.cityConsumptionPer100Km:0} /100km", $"{candidate.cityConsumptionPer100Km:0} /100km");
        SetComparisonRow(7, $"{equipped.rigidbodyMassKg / 1000f:0.0} t", $"{candidate.rigidbodyMassKg / 1000f:0.0} t");
        SetComparisonRow(8, $"{equipped.maxSteerAngle:0.#} deg", $"{candidate.maxSteerAngle:0.#} deg");
        SetComparisonRow(9, $"{equipped.maxBrakeTorque:0} Nm", $"{candidate.maxBrakeTorque:0} Nm");

        bool owned = fleet.IsBusOwned(candidate);
        bool selected = fleet.IsSelected(candidate);
        equipButton.interactable = owned && !selected;
        equipButtonText.text = selected
            ? "EQUIPPED"
            : owned
                ? "EQUIP BUS"
                : $"UNLOCK AT RANK {fleet.GetRequiredRank(candidate)}";
        equipButton.GetComponent<Image>().color = owned && !selected ? UITheme.Accent : UITheme.SurfaceHigh;
        equipButtonText.color = owned && !selected ? UITheme.Background : UITheme.TextMuted;
    }

    void SetComparisonRow(int rowIndex, string leftValue, string rightValue)
    {
        var left = shell.Find($"ComparePanel/RowLeft_{rowIndex}")?.GetComponent<TextMeshProUGUI>();
        var right = shell.Find($"ComparePanel/RowRight_{rowIndex}")?.GetComponent<TextMeshProUGUI>();
        if (left != null)
            left.text = leftValue;
        if (right != null)
            right.text = rightValue;
    }

    void HandleEquipPressed()
    {
        var fleet = BusFleetManager.EnsureExists();
        if (focusedSpec == null || !fleet.TrySelectBus(focusedSpec))
            return;

        Refresh();
        owner?.RefreshGarageAndFleetPresentation();
    }

    void ShowUpgrades()
    {
        if (focusedSpec == null)
            return;
        EnsureProgressionPanel();
        progressionPanel.ShowUpgrades(focusedSpec);
    }

    void ShowLivery()
    {
        if (focusedSpec == null)
            return;
        EnsureProgressionPanel();
        progressionPanel.ShowLivery(focusedSpec);
    }

    void EnsureProgressionPanel()
    {
        if (progressionPanel != null)
            return;
        var panelObject = new GameObject("GarageProgressionPanel", typeof(RectTransform), typeof(GarageProgressionPanel));
        panelObject.transform.SetParent(transform, false);
        progressionPanel = panelObject.GetComponent<GarageProgressionPanel>();
        panelObject.transform.SetAsLastSibling();
        panelObject.SetActive(false);
    }

    void HideImmediate()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        isVisible = false;
        gameObject.SetActive(false);
    }

    Image CreatePanel(string name, Transform parent, Color color)
    {
        var panel = new GameObject(name, typeof(RectTransform), typeof(Image)).GetComponent<Image>();
        panel.transform.SetParent(parent, false);
        panel.color = color;
        return panel;
    }

    Button CreateButton(string name, Transform parent, Color color, out TextMeshProUGUI label)
    {
        var buttonObj = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
        buttonObj.transform.SetParent(parent, false);

        var image = buttonObj.GetComponent<Image>();
        image.color = color;

        var button = buttonObj.GetComponent<Button>();
        var colors = button.colors;
        colors.normalColor = color;
        colors.highlightedColor = UITheme.SurfaceBright;
        colors.pressedColor = UITheme.WithAlpha(color, 0.75f);
        button.colors = colors;

        label = CreateText("Label", buttonObj.transform, name, 14f, UITheme.TextPrimary, UITheme.FontWeight.Bold, TextAlignmentOptions.Center);
        Stretch(label.rectTransform, new Vector2(10f, 6f), new Vector2(-10f, -6f));
        label.characterSpacing = 3f;
        return button;
    }

    TextMeshProUGUI CreateText(string name, Transform parent, string value, float fontSize, Color color, UITheme.FontWeight weight, TextAlignmentOptions alignment)
    {
        var textObj = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI));
        textObj.transform.SetParent(parent, false);

        var text = textObj.GetComponent<TextMeshProUGUI>();
        text.text = value;
        text.fontSize = fontSize;
        text.color = color;
        text.font = UITheme.GetFont(weight);
        text.alignment = alignment;
        text.textWrappingMode = TextWrappingModes.Normal;
        return text;
    }

    static void Anchor(RectTransform rect, Vector2 anchorMin, Vector2 anchorMax, Vector2 offsetMin, Vector2 offsetMax)
    {
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.offsetMin = offsetMin;
        rect.offsetMax = offsetMax;
    }

    static void Stretch(RectTransform rect)
    {
        Stretch(rect, Vector2.zero, Vector2.zero);
    }

    static void Stretch(RectTransform rect, Vector2 offsetMin, Vector2 offsetMax)
    {
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = offsetMin;
        rect.offsetMax = offsetMax;
    }
}
