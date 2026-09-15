using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Shared, mobile-first styling for the country/city/route flow.</summary>
public static class SelectionUIStyle
{
    public const float CardGap = 16f;

    public static void BuildScreen(
        RectTransform root,
        RectTransform header,
        RectTransform listShell,
        RectTransform scrollView,
        RectTransform footer,
        TextMeshProUGUI title,
        TextMeshProUGUI subtitle,
        Image accentLine,
        Button backButton,
        int currentStep)
    {
        if (!root || !header || !listShell || !scrollView || !footer) return;

        SetRect(root, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
        SetRect(header, new Vector2(.055f, .73f), new Vector2(.945f, .94f), Vector2.zero, Vector2.zero);
        SetRect(listShell, new Vector2(.055f, .14f), new Vector2(.945f, .69f), Vector2.zero, Vector2.zero);
        SetRect(footer, new Vector2(.055f, .025f), new Vector2(.945f, .115f), Vector2.zero, Vector2.zero);
        SetRect(scrollView, new Vector2(.018f, .045f), new Vector2(.982f, .955f), Vector2.zero, Vector2.zero);

        if (title)
        {
            title.transform.SetParent(header, false);
            SetRect(title.rectTransform, new Vector2(0f, .34f), new Vector2(.62f, 1f), Vector2.zero, Vector2.zero);
            title.alignment = TextAlignmentOptions.BottomLeft;
        }
        if (subtitle)
        {
            subtitle.transform.SetParent(header, false);
            SetRect(subtitle.rectTransform, new Vector2(0f, 0f), new Vector2(.62f, .34f), Vector2.zero, Vector2.zero);
            subtitle.alignment = TextAlignmentOptions.TopLeft;
        }
        if (accentLine)
        {
            accentLine.transform.SetParent(header, false);
            SetRect(accentLine.rectTransform, new Vector2(0f, 0f), new Vector2(.008f, 1f), Vector2.zero, Vector2.zero);
            accentLine.color = UITheme.Accent;
            accentLine.raycastTarget = false;
            if (title) title.rectTransform.offsetMin = new Vector2(20f, 0f);
            if (subtitle) subtitle.rectTransform.offsetMin = new Vector2(20f, 0f);
        }

        var shellImage = listShell.GetComponent<Image>();
        if (shellImage)
        {
            shellImage.color = UITheme.WithAlpha(UITheme.Background, .78f);
            AddOutlineOnce(listShell.gameObject, UITheme.WithAlpha(UITheme.OutlineVariant, .65f));
        }

        var headerImage = header.GetComponent<Image>();
        if (headerImage) headerImage.color = Color.clear;
        var footerImage = footer.GetComponent<Image>();
        if (footerImage) footerImage.color = Color.clear;

        if (backButton)
        {
            var backRect = backButton.transform as RectTransform;
            SetRect(backRect, new Vector2(0f, .08f), new Vector2(.23f, .92f), Vector2.zero, Vector2.zero);
        }

        BuildProgressRail(header, currentStep);
        BuildBackdrop(root, currentStep);
    }

    public static Transform FindDeep(Transform root, string name)
    {
        if (!root) return null;
        if (root.name == name) return root;
        for (int i = 0; i < root.childCount; i++)
        {
            Transform found = FindDeep(root.GetChild(i), name);
            if (found) return found;
        }
        return null;
    }

    public static void Hide(Transform root, params string[] names)
    {
        if (!root || names == null) return;
        foreach (string name in names)
        {
            Transform item = FindDeep(root, name);
            if (item) item.gameObject.SetActive(false);
        }
    }

    public static void SetRect(RectTransform rect, Vector2 anchorMin, Vector2 anchorMax, Vector2 offsetMin, Vector2 offsetMax)
    {
        if (!rect) return;
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.pivot = new Vector2(.5f, .5f);
        rect.anchoredPosition = Vector2.zero;
        rect.sizeDelta = Vector2.zero;
        rect.offsetMin = offsetMin;
        rect.offsetMax = offsetMax;
        rect.localScale = Vector3.one;
    }

    static void BuildProgressRail(RectTransform header, int currentStep)
    {
        Transform old = header.Find("DriveSetupProgress");
        if (old) Object.Destroy(old.gameObject);

        var rail = new GameObject("DriveSetupProgress");
        rail.transform.SetParent(header, false);
        var rect = rail.AddComponent<RectTransform>();
        SetRect(rect, new Vector2(.66f, .34f), new Vector2(1f, .78f), Vector2.zero, Vector2.zero);
        var horizontal = rail.AddComponent<HorizontalLayoutGroup>();
        horizontal.spacing = 8f;
        horizontal.childAlignment = TextAnchor.MiddleRight;
        horizontal.childControlWidth = true;
        horizontal.childControlHeight = true;
        horizontal.childForceExpandWidth = true;
        horizontal.childForceExpandHeight = true;

        string[] labels = { "1  COUNTRY", "2  CITY", "3  ROUTE" };
        for (int i = 0; i < labels.Length; i++)
        {
            int step = i + 1;
            var chip = new GameObject($"Step_{step}");
            chip.transform.SetParent(rail.transform, false);
            var image = chip.AddComponent<Image>();
            image.color = step == currentStep
                ? UITheme.Accent
                : step < currentStep ? UITheme.SurfaceBright : UITheme.SurfaceContainer;
            image.raycastTarget = false;
            var text = CreateText("Label", chip.transform, labels[i], 12f, UITheme.FontWeight.Bold,
                step == currentStep ? UITheme.Background : step < currentStep ? UITheme.TextPrimary : UITheme.TextMuted,
                TextAlignmentOptions.Center);
            Stretch(text.rectTransform, Vector2.zero, Vector2.one);
        }
    }

    static void BuildBackdrop(RectTransform root, int currentStep)
    {
        Transform old = root.Find("ModernCinematicBackdrop");
        if (old) Object.Destroy(old.gameObject);

        var backdrop = new GameObject("ModernCinematicBackdrop", typeof(RectTransform), typeof(Image));
        backdrop.transform.SetParent(root, false);
        backdrop.transform.SetAsFirstSibling();
        var backdropRect = backdrop.GetComponent<RectTransform>();
        Stretch(backdropRect, Vector2.zero, Vector2.one);
        var backdropImage = backdrop.GetComponent<Image>();
        backdropImage.sprite = Resources.Load<Sprite>("UI/CountrySelectHero");
        backdropImage.color = Color.white;
        backdropImage.raycastTarget = false;

        var shade = new GameObject("BackdropShade", typeof(RectTransform), typeof(Image));
        shade.transform.SetParent(root, false);
        shade.transform.SetSiblingIndex(1);
        Stretch(shade.GetComponent<RectTransform>(), Vector2.zero, Vector2.one);
        var shadeImage = shade.GetComponent<Image>();
        shadeImage.color = new Color(.01f, .018f, .02f, .55f);
        shadeImage.raycastTarget = false;
    }

    public static void StyleScreenTitle(TextMeshProUGUI text, string value)
    {
        if (!text) return;
        text.text = value;
        text.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        text.fontStyle = FontStyles.Bold;
        text.fontSize = 38f;
        text.enableAutoSizing = true;
        text.fontSizeMin = 26f;
        text.fontSizeMax = 38f;
        text.characterSpacing = 0f;
        text.color = UITheme.TextPrimary;
        text.textWrappingMode = TextWrappingModes.NoWrap;
        text.overflowMode = TextOverflowModes.Ellipsis;
        text.raycastTarget = false;
    }

    public static void StyleSubtitle(TextMeshProUGUI text, string value)
    {
        if (!text) return;
        text.text = value;
        text.font = UITheme.GetFont(UITheme.FontWeight.Regular);
        text.fontStyle = FontStyles.Normal;
        text.fontSize = 16f;
        text.characterSpacing = 0f;
        text.color = UITheme.TextSecondary;
        text.textWrappingMode = TextWrappingModes.NoWrap;
        text.overflowMode = TextOverflowModes.Ellipsis;
        text.raycastTarget = false;
    }

    public static void StyleBackButton(Button button, TextMeshProUGUI label, string value)
    {
        if (button)
        {
            var image = button.GetComponent<Image>();
            if (image) image.color = UITheme.SurfaceHigh;
            var colors = button.colors;
            colors.normalColor = UITheme.SurfaceHigh;
            colors.highlightedColor = UITheme.SurfaceBright;
            colors.pressedColor = UITheme.SurfaceContainer;
            colors.selectedColor = UITheme.SurfaceBright;
            button.colors = colors;
        }

        if (!label) return;
        label.text = value;
        label.font = UITheme.GetFont(UITheme.FontWeight.Medium);
        label.fontSize = 14f;
        label.characterSpacing = 0f;
        label.color = UITheme.TextPrimary;
        label.alignment = TextAlignmentOptions.Center;
        label.raycastTarget = false;
    }

    public static TextMeshProUGUI CreateText(
        string name, Transform parent, string value, float size,
        UITheme.FontWeight weight, Color color, TextAlignmentOptions alignment)
    {
        var obj = new GameObject(name);
        obj.transform.SetParent(parent, false);
        var text = obj.AddComponent<TextMeshProUGUI>();
        text.text = value;
        text.font = UITheme.GetFont(weight);
        text.fontSize = size;
        text.color = color;
        text.alignment = alignment;
        text.textWrappingMode = TextWrappingModes.NoWrap;
        text.overflowMode = TextOverflowModes.Ellipsis;
        text.raycastTarget = false;
        if (weight == UITheme.FontWeight.Bold)
            text.fontStyle = FontStyles.Bold;
        return text;
    }

    public static void Stretch(RectTransform rect, Vector2 min, Vector2 max)
    {
        rect.anchorMin = min;
        rect.anchorMax = max;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    public static void AddOutline(GameObject target, Color color)
    {
        var outline = target.AddComponent<Outline>();
        outline.effectColor = color;
        outline.effectDistance = new Vector2(1f, -1f);
        outline.useGraphicAlpha = true;
    }

    static void AddOutlineOnce(GameObject target, Color color)
    {
        var outline = target.GetComponent<Outline>();
        if (!outline) outline = target.AddComponent<Outline>();
        outline.effectColor = color;
        outline.effectDistance = new Vector2(1f, -1f);
        outline.useGraphicAlpha = true;
    }
}
