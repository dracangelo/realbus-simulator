using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GarageProgressionPanel : MonoBehaviour
{
    RectTransform shell;
    RectTransform content;
    TextMeshProUGUI titleText;
    TextMeshProUGUI statusText;
    BusSpec activeBus;
    LiveryEditor liveryEditor;
    RawImage liveryPreview;
    TMP_InputField codeInput;
    LiveryZone selectedZone;
    float hue;
    float saturation = 0.8f;
    float value = 0.9f;
    float opacity = 1f;

    public void ShowUpgrades(BusSpec bus)
    {
        activeBus = bus;
        EnsureBuilt();
        gameObject.SetActive(true);
        titleText.text = $"UPGRADES  /  {(bus != null ? bus.displayName.ToUpper() : "NO BUS")}";
        BuildUpgradeContent();
    }

    public void ShowLivery(BusSpec bus)
    {
        activeBus = bus;
        EnsureBuilt();
        gameObject.SetActive(true);
        titleText.text = $"LIVERY STUDIO  /  {(bus != null ? bus.displayName.ToUpper() : "NO BUS")}";
        BuildLiveryContent();
    }

    public void Hide() => gameObject.SetActive(false);

    void EnsureBuilt()
    {
        if (shell != null)
            return;

        RectTransform root = GetComponent<RectTransform>();
        root.anchorMin = Vector2.zero;
        root.anchorMax = Vector2.one;
        root.offsetMin = Vector2.zero;
        root.offsetMax = Vector2.zero;

        Image dim = CreatePanel("Dim", transform, UITheme.WithAlpha(UITheme.Background, 0.92f));
        Stretch(dim.rectTransform);

        shell = CreatePanel("Shell", transform, UITheme.SurfaceContainer).rectTransform;
        Anchor(shell, new Vector2(0.12f, 0.1f), new Vector2(0.88f, 0.9f), Vector2.zero, Vector2.zero);

        titleText = CreateText("Title", shell, string.Empty, 25f, UITheme.TextPrimary, UITheme.FontWeight.Bold, TextAlignmentOptions.Left);
        Anchor(titleText.rectTransform, new Vector2(0f, 1f), new Vector2(0.75f, 1f), new Vector2(26f, -18f), new Vector2(-12f, -54f));

        Button close = CreateButton("Close", shell, "CLOSE", UITheme.SurfaceHigh);
        close.onClick.AddListener(Hide);
        Anchor(close.GetComponent<RectTransform>(), new Vector2(0.8f, 1f), new Vector2(1f, 1f), new Vector2(10f, -18f), new Vector2(-24f, -56f));

        content = new GameObject("Content", typeof(RectTransform)).GetComponent<RectTransform>();
        content.SetParent(shell, false);
        Anchor(content, Vector2.zero, Vector2.one, new Vector2(26f, 62f), new Vector2(-26f, -70f));

        statusText = CreateText("Status", shell, string.Empty, 14f, UITheme.TextSecondary, UITheme.FontWeight.Medium, TextAlignmentOptions.Left);
        Anchor(statusText.rectTransform, new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(26f, 18f), new Vector2(-26f, 48f));
    }

    void ClearContent()
    {
        for (int i = content.childCount - 1; i >= 0; i--)
            Destroy(content.GetChild(i).gameObject);
        statusText.text = string.Empty;
    }

    void BuildUpgradeContent()
    {
        ClearContent();
        var manager = UpgradeManager.EnsureExists();
        var definitions = manager.Definitions;
        const float rowHeight = 82f;

        for (int i = 0; i < definitions.Count; i++)
        {
            UpgradeDefinition definition = definitions[i];
            float top = -(i * rowHeight);
            RectTransform row = CreatePanel($"Upgrade_{definition.upgradeId}", content, i % 2 == 0 ? UITheme.Surface : UITheme.BackgroundAlt).rectTransform;
            Anchor(row, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0f, top - rowHeight + 4f), new Vector2(0f, top));

            TextMeshProUGUI name = CreateText("Name", row, definition.displayName.ToUpper(), 17f, UITheme.TextPrimary, UITheme.FontWeight.Bold, TextAlignmentOptions.Left);
            Anchor(name.rectTransform, new Vector2(0f, 0.5f), new Vector2(0.28f, 1f), new Vector2(16f, -3f), new Vector2(-8f, -8f));

            TextMeshProUGUI description = CreateText("Description", row, definition.description, 13f, UITheme.TextSecondary, UITheme.FontWeight.Regular, TextAlignmentOptions.Left);
            Anchor(description.rectTransform, new Vector2(0.28f, 0f), new Vector2(0.68f, 1f), new Vector2(8f, 8f), new Vector2(-8f, -8f));

            bool purchased = activeBus != null && manager.IsPurchased(activeBus.busId, definition);
            string buttonLabel = purchased ? "INSTALLED" : $"KES {definition.priceKES:N0}";
            Button buy = CreateButton("Buy", row, buttonLabel, purchased ? UITheme.SurfaceHigh : UITheme.Accent);
            buy.interactable = !purchased && activeBus != null;
            Anchor(buy.GetComponent<RectTransform>(), new Vector2(0.7f, 0.15f), new Vector2(1f, 0.85f), new Vector2(10f, 0f), new Vector2(-14f, 0f));
            buy.onClick.AddListener(() =>
            {
                if (manager.TryPurchase(activeBus, definition, out string message))
                    BuildUpgradeContent();
                statusText.text = message;
            });
        }
    }

    void BuildLiveryContent()
    {
        ClearContent();
        if (activeBus == null)
        {
            statusText.text = "Select a bus before opening the livery studio.";
            return;
        }

        liveryEditor = gameObject.GetComponent<LiveryEditor>();
        if (liveryEditor == null) liveryEditor = gameObject.AddComponent<LiveryEditor>();
        BusController bus = FindFirstObjectByType<BusController>();
        Renderer[] renderers = bus != null ? bus.GetComponentsInChildren<Renderer>(true) : Array.Empty<Renderer>();
        liveryEditor.Initialize(activeBus.busId, renderers);

        liveryPreview = new GameObject("Preview", typeof(RectTransform), typeof(RawImage)).GetComponent<RawImage>();
        liveryPreview.transform.SetParent(content, false);
        liveryPreview.color = Color.white;
        liveryPreview.texture = liveryEditor.LiveryTexture;
        Anchor(liveryPreview.rectTransform, new Vector2(0f, 0.48f), new Vector2(0.36f, 1f), new Vector2(0f, 8f), new Vector2(-14f, 0f));

        string[] zoneNames = { "ROOF", "FRONT", "SIDE A", "SIDE B", "REAR", "ARCHES" };
        for (int i = 0; i < zoneNames.Length; i++)
        {
            int zoneIndex = i;
            Button zoneButton = CreateButton("Zone", content, zoneNames[i], UITheme.SurfaceHigh);
            float colMin = (i % 3) * 0.12f;
            float colMax = colMin + 0.11f;
            float rowTop = i < 3 ? 0.43f : 0.34f;
            Anchor(zoneButton.GetComponent<RectTransform>(), new Vector2(colMin, rowTop - 0.075f), new Vector2(colMax, rowTop), new Vector2(0f, 0f), new Vector2(-8f, -4f));
            zoneButton.onClick.AddListener(() =>
            {
                selectedZone = (LiveryZone)zoneIndex;
                statusText.text = $"Editing {zoneNames[zoneIndex]}.";
            });
        }

        CreateLabeledSlider("HUE", 0.25f, hue, valueChanged => { hue = valueChanged; ApplyPicker(); });
        CreateLabeledSlider("SATURATION", 0.18f, saturation, valueChanged => { saturation = valueChanged; ApplyPicker(); });
        CreateLabeledSlider("VALUE", 0.11f, value, valueChanged => { value = valueChanged; ApplyPicker(); });
        CreateLabeledSlider("OPACITY", 0.04f, opacity, valueChanged => { opacity = valueChanged; ApplyPicker(); });

        var presets = LiveryEditor.Presets;
        for (int i = 0; i < presets.Count; i++)
        {
            int presetIndex = i;
            LiveryPreset preset = presets[i];
            Button presetButton = CreateButton("Preset", content, $"{preset.displayName.ToUpper()}  R{preset.requiredRank}", UITheme.SurfaceHigh);
            int col = i % 2;
            int row = i / 2;
            float top = 1f - row * 0.12f;
            Anchor(presetButton.GetComponent<RectTransform>(), new Vector2(0.39f + col * 0.305f, top - 0.1f), new Vector2(0.68f + col * 0.305f, top), new Vector2(6f, 0f), new Vector2(-6f, -6f));
            presetButton.onClick.AddListener(() =>
            {
                bool applied = liveryEditor.ApplyPreset(presetIndex);
                liveryPreview.texture = liveryEditor.LiveryTexture;
                statusText.text = applied ? $"Applied {preset.displayName}." : $"{preset.displayName} unlocks at Rank {preset.requiredRank}.";
            });
        }

        codeInput = CreateInputField(content);
        Anchor(codeInput.GetComponent<RectTransform>(), new Vector2(0.39f, 0f), new Vector2(0.76f, 0.12f), new Vector2(6f, 0f), new Vector2(-8f, -8f));
        codeInput.text = LiveryCodeCodec.Encode(liveryEditor.CurrentLivery);

        Button applyCode = CreateButton("ApplyCode", content, "APPLY CODE", UITheme.SurfaceHigh);
        Anchor(applyCode.GetComponent<RectTransform>(), new Vector2(0.76f, 0f), new Vector2(1f, 0.12f), new Vector2(6f, 0f), new Vector2(0f, -8f));
        applyCode.onClick.AddListener(() =>
        {
            bool applied = liveryEditor.ApplyCode(codeInput.text);
            liveryPreview.texture = liveryEditor.LiveryTexture;
            statusText.text = applied ? "Shared livery code applied and saved." : "Enter a valid 12-character livery code.";
        });

        Button save = CreateButton("Save", content, "SAVE + COPY CODE", UITheme.Accent);
        Anchor(save.GetComponent<RectTransform>(), new Vector2(0f, 0f), new Vector2(0.36f, 0.12f), new Vector2(0f, 0f), new Vector2(-14f, -8f));
        save.onClick.AddListener(() =>
        {
            string code = liveryEditor.SaveAndGetCode();
            codeInput.text = code;
            GUIUtility.systemCopyBuffer = code;
            statusText.text = $"Saved livery {code}; code copied to clipboard.";
        });
    }

    void ApplyPicker()
    {
        liveryEditor.SetHSV(selectedZone, hue, saturation, value, opacity);
        if (liveryPreview != null) liveryPreview.texture = liveryEditor.LiveryTexture;
        if (codeInput != null) codeInput.text = LiveryCodeCodec.Encode(liveryEditor.CurrentLivery);
    }

    void CreateLabeledSlider(string label, float anchorY, float initial, UnityEngine.Events.UnityAction<float> callback)
    {
        TextMeshProUGUI text = CreateText(label, content, label, 12f, UITheme.TextMuted, UITheme.FontWeight.Bold, TextAlignmentOptions.Left);
        Anchor(text.rectTransform, new Vector2(0f, anchorY), new Vector2(0.12f, anchorY + 0.055f), Vector2.zero, Vector2.zero);

        Slider slider = new GameObject(label + "Slider", typeof(RectTransform), typeof(Slider)).GetComponent<Slider>();
        slider.transform.SetParent(content, false);
        Anchor(slider.GetComponent<RectTransform>(), new Vector2(0.13f, anchorY), new Vector2(0.36f, anchorY + 0.055f), new Vector2(0f, 4f), new Vector2(-14f, -4f));
        Image background = CreatePanel("Background", slider.transform, UITheme.SurfaceHigh);
        Stretch(background.rectTransform, new Vector2(0f, 8f), new Vector2(0f, -8f));
        RectTransform fillArea = new GameObject("Fill Area", typeof(RectTransform)).GetComponent<RectTransform>();
        fillArea.SetParent(slider.transform, false);
        Stretch(fillArea, Vector2.zero, Vector2.zero);
        Image fill = CreatePanel("Fill", fillArea, UITheme.Accent);
        Stretch(fill.rectTransform, Vector2.zero, Vector2.zero);
        slider.fillRect = fill.rectTransform;
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = initial;
        slider.onValueChanged.AddListener(callback);
    }

    TMP_InputField CreateInputField(Transform parent)
    {
        GameObject root = new GameObject("LiveryCode", typeof(RectTransform), typeof(Image), typeof(TMP_InputField));
        root.transform.SetParent(parent, false);
        root.GetComponent<Image>().color = UITheme.BackgroundAlt;

        TextMeshProUGUI text = CreateText("Text", root.transform, string.Empty, 18f, UITheme.TextPrimary, UITheme.FontWeight.Bold, TextAlignmentOptions.Center);
        Stretch(text.rectTransform, new Vector2(10f, 5f), new Vector2(-10f, -5f));
        TextMeshProUGUI placeholder = CreateText("Placeholder", root.transform, "12-CHAR CODE", 15f, UITheme.TextMuted, UITheme.FontWeight.Medium, TextAlignmentOptions.Center);
        Stretch(placeholder.rectTransform, new Vector2(10f, 5f), new Vector2(-10f, -5f));

        TMP_InputField input = root.GetComponent<TMP_InputField>();
        input.textViewport = root.GetComponent<RectTransform>();
        input.textComponent = text;
        input.placeholder = placeholder;
        input.characterLimit = 12;
        input.contentType = TMP_InputField.ContentType.Alphanumeric;
        return input;
    }

    static Image CreatePanel(string name, Transform parent, Color color)
    {
        Image image = new GameObject(name, typeof(RectTransform), typeof(Image)).GetComponent<Image>();
        image.transform.SetParent(parent, false);
        image.color = color;
        return image;
    }

    static Button CreateButton(string name, Transform parent, string label, Color color)
    {
        GameObject root = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
        root.transform.SetParent(parent, false);
        root.GetComponent<Image>().color = color;
        Button button = root.GetComponent<Button>();
        ColorBlock colors = button.colors;
        colors.normalColor = color;
        colors.highlightedColor = UITheme.SurfaceBright;
        colors.pressedColor = UITheme.WithAlpha(color, 0.76f);
        button.colors = colors;
        TextMeshProUGUI text = CreateText("Label", root.transform, label, 13f, UITheme.TextPrimary, UITheme.FontWeight.Bold, TextAlignmentOptions.Center);
        Stretch(text.rectTransform, new Vector2(6f, 4f), new Vector2(-6f, -4f));
        return button;
    }

    static TextMeshProUGUI CreateText(string name, Transform parent, string value, float size, Color color, UITheme.FontWeight weight, TextAlignmentOptions alignment)
    {
        TextMeshProUGUI text = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI)).GetComponent<TextMeshProUGUI>();
        text.transform.SetParent(parent, false);
        text.text = value;
        text.fontSize = size;
        text.color = color;
        text.font = UITheme.GetFont(weight);
        text.alignment = alignment;
        text.enableWordWrapping = true;
        return text;
    }

    static void Anchor(RectTransform rect, Vector2 min, Vector2 max, Vector2 offsetMin, Vector2 offsetMax)
    {
        rect.anchorMin = min;
        rect.anchorMax = max;
        rect.offsetMin = offsetMin;
        rect.offsetMax = offsetMax;
    }

    static void Stretch(RectTransform rect, Vector2 offsetMin = default, Vector2 offsetMax = default)
    {
        Anchor(rect, Vector2.zero, Vector2.one, offsetMin, offsetMax);
    }
}
