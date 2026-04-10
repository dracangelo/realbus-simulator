using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;

public class CountrySelectUI : MonoBehaviour
{
    [Header("UI References")]
    public CanvasGroup canvasGroup;
    public RectTransform contentPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subtitleText;
    public Image accentLine;
    public Transform countryListContainer;
    public Button backButton;
    public TextMeshProUGUI backButtonText;
    public Image backgroundPanel;

    // Continent order
    private readonly string[] continents = {
        "Africa", "Europe", "Asia", "Americas", "Oceania", "Other"
    };

    IEnumerator Start()
    {
        ApplyTheme();
        SetupButtons();
        yield return StartCoroutine(EnsureCountryGridLayoutReady());
        PopulateCountryList();
        yield return StartCoroutine(AnimateIn());
    }

    void ApplyTheme()
    {
        if (backgroundPanel)
            backgroundPanel.color = UITheme.Background;

        if (titleText)
        {
            titleText.text = "SELECT COUNTRY";
            titleText.color = UITheme.TextPrimary;
            titleText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            titleText.characterSpacing = 6f;
        }

        if (subtitleText)
        {
            subtitleText.text = "where do you want to drive?";
            subtitleText.color = UITheme.TextSecondary;
            subtitleText.font = UITheme.GetFont(UITheme.FontWeight.Regular);
        }

        if (accentLine)
            accentLine.color = UITheme.Accent;

        if (backButtonText)
        {
            backButtonText.text = "BACK";
            backButtonText.color = UITheme.TextSecondary;
            backButtonText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
            backButtonText.characterSpacing = 2f;
        }

        var backImg = backButton?.GetComponent<Image>();
        if (backImg) backImg.color = UITheme.SurfaceContainer;
    }

    void SetupButtons()
    {
        backButton?.onClick.AddListener(OnBack);
    }

    IEnumerator EnsureCountryGridLayoutReady()
    {
        if (!countryListContainer)
            yield break;

        var listRect = countryListContainer as RectTransform;
        if (!listRect)
        {
            Debug.LogWarning("CountrySelectUI: countryListContainer is not a RectTransform. Skipping grid setup.");
            yield break;
        }

        // If scene still has VerticalLayoutGroup, remove it first and wait one frame
        // before adding GridLayoutGroup (Unity only destroys components end-of-frame).
        var vertical = listRect.GetComponent<VerticalLayoutGroup>();
        var grid = listRect.GetComponent<GridLayoutGroup>();
        if (vertical && !grid)
        {
            Destroy(vertical);
            yield return null;
        }

        grid = listRect.GetComponent<GridLayoutGroup>();
        if (!grid)
            grid = listRect.gameObject.AddComponent<GridLayoutGroup>();
        if (!grid)
            yield break;

        // Small cards: 3 columns across.
        grid.cellSize = new Vector2(220f, 300f);
        grid.spacing = new Vector2(16f, 16f);
        grid.constraint = GridLayoutGroup.Constraint.Flexible;
        grid.constraintCount = 3;
        grid.startAxis = GridLayoutGroup.Axis.Horizontal;
        grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
        grid.childAlignment = TextAnchor.UpperCenter;
        grid.padding = new RectOffset(0, 0, 4, 12);
    }

    void PopulateCountryList()
    {
        if (countryListContainer == null)
        {
            Debug.LogError("CountrySelectUI: countryListContainer is not assigned.");
            return;
        }

        foreach (Transform child in countryListContainer)
            Destroy(child.gameObject);

        if (CityManager.Instance == null || CityManager.Instance.allCountries == null
            || CityManager.Instance.allCountries.Length == 0)
        {
            Debug.LogError("CountrySelectUI: No countries in CityManager!");
            CreateEmptyState("No countries available. Populate CityManager.allCountries first.");
            return;
        }

        int renderedCards = 0;

        // Group by continent
        foreach (string continent in continents)
        {
            var countries = GetCountriesByContinentNormalized(continent);
            if (countries.Length == 0) continue;

            // Country cards
            foreach (var country in countries)
                if (country != null)
                {
                    CreateCountryCard(country);
                    renderedCards++;
                }
        }

        if (renderedCards == 0)
        {
            Debug.LogWarning("CountrySelectUI: Countries found but none matched known continent names. Rendering uncategorized list.");
            foreach (var country in CityManager.Instance.allCountries)
            {
                if (country == null) continue;
                CreateCountryCard(country);
                renderedCards++;
            }
        }

        Debug.Log($"CountrySelectUI: Rendered {renderedCards} country cards (source countries: {CityManager.Instance.allCountries.Length}).");
        RefreshListLayout();

        // Layout groups + ContentSizeFitter often need one extra frame in Play Mode.
        StartCoroutine(RebuildNextFrame());
    }

    IEnumerator RebuildNextFrame()
    {
        yield return null;
        RefreshListLayout();
    }

    CountryDefinition[] GetCountriesByContinentNormalized(string continent)
    {
        var all = CityManager.Instance?.allCountries;
        if (all == null || all.Length == 0) return Array.Empty<CountryDefinition>();

        var result = new List<CountryDefinition>();
        foreach (var c in all)
        {
            if (c == null) continue;
            if (SameLabel(c.continent, continent))
                result.Add(c);
        }
        return result.ToArray();
    }

    bool SameLabel(string a, string b)
    {
        if (string.IsNullOrWhiteSpace(a) || string.IsNullOrWhiteSpace(b)) return false;
        return string.Equals(a.Trim(), b.Trim(), StringComparison.OrdinalIgnoreCase);
    }

    void CreateContinentHeader(string continent)
    {
        GameObject headerObj = new GameObject($"Header_{continent}");
        headerObj.transform.SetParent(countryListContainer, false);

        var rect = headerObj.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(0, 50);
        var layout = headerObj.AddComponent<LayoutElement>();
        layout.preferredHeight = 50f;
        layout.flexibleWidth = 1f;

        // Bottom border line
        GameObject line = new GameObject("Line");
        line.transform.SetParent(headerObj.transform, false);
        var lineRect = line.AddComponent<RectTransform>();
        lineRect.anchorMin = new Vector2(0f, 0f);
        lineRect.anchorMax = new Vector2(1f, 0f);
        lineRect.sizeDelta = new Vector2(0f, 1f);
        lineRect.anchoredPosition = new Vector2(0f, 8f);
        var lineImg = line.AddComponent<Image>();
        lineImg.color = UITheme.OutlineVariant;

        // Continent label
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(headerObj.transform, false);
        var tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = continent.ToUpper();
        tmp.fontSize = 13;
        tmp.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        tmp.color = GetContinentAccent(continent);
        tmp.characterSpacing = 4f;
        tmp.alignment = TextAlignmentOptions.Left;
        var textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = new Vector2(4f, 10f);
        textRect.offsetMax = new Vector2(0f, 0f);
    }

    void CreateCountryCard(CountryDefinition country)
    {
        // Neon-inspired compact card (3-across friendly).
        GameObject card = new GameObject($"CountryCard_{country.countryCode}");
        card.transform.SetParent(countryListContainer, false);
        card.transform.localScale = Vector3.one;

        var cardRect = card.AddComponent<RectTransform>();
        cardRect.anchorMin = new Vector2(0f, 1f);
        cardRect.anchorMax = new Vector2(1f, 1f);
        cardRect.pivot = new Vector2(0.5f, 1f);
        cardRect.anchoredPosition = Vector2.zero;
        cardRect.sizeDelta = new Vector2(220f, 300f);

        var cardLayout = card.AddComponent<LayoutElement>();
        cardLayout.preferredHeight = 300f;
        cardLayout.preferredWidth = 220f;
        cardLayout.flexibleWidth = 0f;

        var bg = card.AddComponent<Image>();
        bg.color = UITheme.SurfaceHigh;

        int cityCount = country.cities?.Length ?? 0;
        string difficulty = cityCount <= 4 ? "HARD" : cityCount <= 7 ? "MEDIUM" : "EASY";
        int unlockPct = Mathf.Clamp(40 + cityCount * 5, 0, 100);
        bool isActive = CityManager.Instance != null && CityManager.Instance.activeCountry == country;
        Color difficultyColor = difficulty == "HARD" ? UITheme.Accent
            : difficulty == "MEDIUM" ? UITheme.Secondary
            : UITheme.TertiaryDim;

        // Hero image area (currently color-based by continent).
        GameObject hero = new GameObject("HeroImage");
        hero.transform.SetParent(card.transform, false);
        var heroImg = hero.AddComponent<Image>();
        heroImg.color = UITheme.WithAlpha(GetContinentAccent(country.continent), 0.85f);
        var heroRt = hero.GetComponent<RectTransform>();
        heroRt.anchorMin = Vector2.zero;
        heroRt.anchorMax = Vector2.one;
        heroRt.offsetMin = Vector2.zero;
        heroRt.offsetMax = Vector2.zero;

        // Full-screen gradient overlay (dark at bottom).
        GameObject overlay = new GameObject("HeroOverlay");
        overlay.transform.SetParent(hero.transform, false);
        var overlayImg = overlay.AddComponent<Image>();
        overlayImg.color = UITheme.WithAlpha(Color.black, 0.42f);

        var overlayRect = overlay.GetComponent<RectTransform>();
        overlayRect.anchorMin = Vector2.zero;
        overlayRect.anchorMax = Vector2.one;
        overlayRect.offsetMin = Vector2.zero;
        overlayRect.offsetMax = Vector2.zero;

        // Content block anchored to bottom.
        GameObject content = new GameObject("Content");
        content.transform.SetParent(card.transform, false);
        var contentRt = content.AddComponent<RectTransform>();
        contentRt.anchorMin = Vector2.zero;
        contentRt.anchorMax = Vector2.one;
        contentRt.offsetMin = new Vector2(12f, 10f);
        contentRt.offsetMax = new Vector2(-12f, -10f);

        // Status badge background + label (separate objects to avoid Graphic conflicts).
        GameObject badgeObj = new GameObject("Badge");
        badgeObj.transform.SetParent(content.transform, false);
        var badgeBg = badgeObj.AddComponent<Image>();
        badgeBg.color = UITheme.WithAlpha(isActive ? UITheme.Accent : UITheme.Secondary, 0.18f);
        var badgeRt = badgeObj.GetComponent<RectTransform>();
        badgeRt.anchorMin = new Vector2(0f, 0f);
        badgeRt.anchorMax = new Vector2(0f, 0f);
        badgeRt.pivot = new Vector2(0f, 0f);
        badgeRt.anchoredPosition = new Vector2(0f, 250f);
        badgeRt.sizeDelta = new Vector2(78f, 16f);

        GameObject badgeTextObj = new GameObject("Text_Badge");
        badgeTextObj.transform.SetParent(badgeObj.transform, false);
        var badgeTmp = badgeTextObj.AddComponent<TextMeshProUGUI>();
        badgeTmp.text = isActive ? "CURRENT ACTIVE" : "ELITE CLASS";
        badgeTmp.fontSize = 9f;
        badgeTmp.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        badgeTmp.color = isActive ? UITheme.Accent : UITheme.Secondary;
        badgeTmp.alignment = TextAlignmentOptions.Center;
        var badgeTextRt = badgeTextObj.GetComponent<RectTransform>();
        badgeTextRt.anchorMin = Vector2.zero;
        badgeTextRt.anchorMax = Vector2.one;
        badgeTextRt.offsetMin = Vector2.zero;
        badgeTextRt.offsetMax = Vector2.zero;

        // Country name.
        GameObject nameObj = new GameObject("Text_Country");
        nameObj.transform.SetParent(content.transform, false);
        var nameTmp = nameObj.AddComponent<TextMeshProUGUI>();
        nameTmp.text = country.countryName.ToUpper();
        nameTmp.fontSize = 16f;
        nameTmp.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        nameTmp.color = UITheme.TextPrimary;
        nameTmp.alignment = TextAlignmentOptions.Left;
        nameTmp.overflowMode = TextOverflowModes.Ellipsis;
        var nameRt = nameObj.GetComponent<RectTransform>();
        nameRt.anchorMin = new Vector2(0f, 0f);
        nameRt.anchorMax = new Vector2(1f, 0f);
        nameRt.pivot = new Vector2(0f, 0f);
        nameRt.anchoredPosition = new Vector2(0f, 100f);
        nameRt.sizeDelta = new Vector2(0f, 24f);

        // Three-column metadata strip.
        GameObject statsRow = new GameObject("StatsRow");
        statsRow.transform.SetParent(content.transform, false);
        var statsBg = statsRow.AddComponent<Image>();
        statsBg.color = UITheme.WithAlpha(UITheme.BackgroundAlt, 0.62f);

        var rowLayout = statsRow.AddComponent<HorizontalLayoutGroup>();
        rowLayout.spacing = 0f;
        rowLayout.childAlignment = TextAnchor.MiddleCenter;
        rowLayout.childForceExpandWidth = true;
        rowLayout.childForceExpandHeight = true;
        rowLayout.padding = new RectOffset(4, 4, 4, 4);
        var statsRt = statsRow.GetComponent<RectTransform>();
        statsRt.anchorMin = new Vector2(0f, 0f);
        statsRt.anchorMax = new Vector2(1f, 0f);
        statsRt.pivot = new Vector2(0.5f, 0f);
        statsRt.anchoredPosition = new Vector2(0f, 50f);
        statsRt.sizeDelta = new Vector2(0f, 40f);

        CreateMetaCell(statsRow.transform, "DIFFICULTY", difficulty, difficultyColor);
        CreateMetaCell(statsRow.transform, "CITIES", cityCount.ToString(), UITheme.TextPrimary);
        CreateMetaCell(statsRow.transform, "UNLOCK", $"{unlockPct}%", UITheme.Accent);

        // Select button
        GameObject btnObj = new GameObject("Btn_SelectCountry");
        btnObj.transform.SetParent(content.transform, false);
        var btnRt = btnObj.AddComponent<RectTransform>();
        btnRt.anchorMin = new Vector2(0f, 0f);
        btnRt.anchorMax = new Vector2(1f, 0f);
        btnRt.pivot = new Vector2(0.5f, 0f);
        btnRt.anchoredPosition = new Vector2(0f, 0f);
        btnRt.sizeDelta = new Vector2(0f, 26f);

        var btnImg = btnObj.AddComponent<Image>();
        btnImg.color = isActive ? UITheme.SurfaceBright : UITheme.Accent;
        var btn = btnObj.AddComponent<Button>();

        var cols = btn.colors;
        cols.normalColor = btnImg.color;
        cols.highlightedColor = isActive ? UITheme.SurfaceHigh : UITheme.AccentDim;
        cols.pressedColor = isActive ? UITheme.WithAlpha(UITheme.SurfaceBright, 0.7f) : UITheme.WithAlpha(UITheme.Accent, 0.7f);
        btn.colors = cols;

        var btnText = new GameObject("Text").AddComponent<TextMeshProUGUI>();
        btnText.transform.SetParent(btnObj.transform, false);
        btnText.text = "SELECT COUNTRY";
        btnText.fontSize = 11f;
        btnText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        btnText.color = isActive ? UITheme.TextPrimary : UITheme.Background;
        btnText.alignment = TextAlignmentOptions.Center;

        var btnTextRt = btnText.GetComponent<RectTransform>();
        btnTextRt.anchorMin = Vector2.zero;
        btnTextRt.anchorMax = Vector2.one;
        btnTextRt.offsetMin = Vector2.zero;
        btnTextRt.offsetMax = Vector2.zero;

        var capturedCountry = country;
        btn.onClick.AddListener(() => OnCountrySelected(capturedCountry));
    }

    void CreateMetaCell(Transform parent, string label, string value, Color valueColor)
    {
        GameObject cell = new GameObject($"Meta_{label}");
        cell.transform.SetParent(parent, false);
        cell.AddComponent<RectTransform>();
        var layout = cell.AddComponent<LayoutElement>();
        layout.flexibleWidth = 1f;

        var vertical = cell.AddComponent<VerticalLayoutGroup>();
        vertical.spacing = 0f;
        vertical.childAlignment = TextAnchor.MiddleCenter;
        vertical.childControlHeight = false;
        vertical.childControlWidth = true;
        vertical.childForceExpandHeight = false;
        vertical.childForceExpandWidth = true;

        var labelObj = new GameObject("Label");
        labelObj.transform.SetParent(cell.transform, false);
        var labelTmp = labelObj.AddComponent<TextMeshProUGUI>();
        labelTmp.text = label;
        labelTmp.fontSize = 8f;
        labelTmp.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        labelTmp.color = UITheme.TextMuted;
        labelTmp.alignment = TextAlignmentOptions.Center;
        var labelLayout = labelObj.AddComponent<LayoutElement>();
        labelLayout.preferredHeight = 14f;

        var valueObj = new GameObject("Value");
        valueObj.transform.SetParent(cell.transform, false);
        var valueTmp = valueObj.AddComponent<TextMeshProUGUI>();
        valueTmp.text = value;
        valueTmp.fontSize = 12f;
        valueTmp.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        valueTmp.color = valueColor;
        valueTmp.alignment = TextAlignmentOptions.Center;
        var valueLayout = valueObj.AddComponent<LayoutElement>();
        valueLayout.preferredHeight = 18f;
    }

    void CreateEmptyState(string message)
    {
        GameObject msgObj = new GameObject("Text_Empty");
        msgObj.transform.SetParent(countryListContainer, false);
        var layout = msgObj.AddComponent<LayoutElement>();
        layout.preferredHeight = 120f;
        layout.flexibleWidth = 1f;

        var tmp = msgObj.AddComponent<TextMeshProUGUI>();
        tmp.text = message;
        tmp.fontSize = 18;
        tmp.font = UITheme.GetFont(UITheme.FontWeight.Regular);
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = UITheme.TextMuted;

        var rect = msgObj.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(0f, 120f);
    }

    Color GetContinentAccent(string continent)
    {
        switch (continent)
        {
            case "Africa":   return UITheme.Africa;
            case "Europe":   return UITheme.Europe;
            case "Asia":     return UITheme.Asia;
            case "Americas": return UITheme.Americas;
            case "Oceania":  return UITheme.Oceania;
            default:         return UITheme.Accent;
        }
    }

    void OnCountrySelected(CountryDefinition country)
    {
        Debug.Log($"Country selected: {country.countryName}");

        if (GameState.Instance != null)
            GameState.Instance.SelectCountry(country);

        if (CityManager.Instance != null)
            CityManager.Instance.SetActiveCountry(country);

        StartCoroutine(TransitionToCitySelect());
    }

    void OnBack()
    {
        StartCoroutine(TransitionToMainMenu());
    }

    IEnumerator AnimateIn()
    {
        if (canvasGroup) canvasGroup.alpha = 0f;
        yield return new WaitForSeconds(0.05f);
        if (canvasGroup)
            yield return StartCoroutine(UIAnimator.FadeIn(canvasGroup, 0.4f));
        if (contentPanel)
            yield return StartCoroutine(
                UIAnimator.SlideInFromBottom(contentPanel, 0.35f, 30f));
    }

    IEnumerator TransitionToMainMenu()
    {
        if (canvasGroup)
            yield return StartCoroutine(UIAnimator.FadeOut(canvasGroup, 0.25f));

        SceneLoader.Instance?.LoadMainMenu();
    }

    IEnumerator TransitionToCitySelect()
    {
        if (canvasGroup)
            yield return StartCoroutine(UIAnimator.FadeOut(canvasGroup, 0.25f));

        SceneLoader.Instance?.LoadCitySelect();
    }

    void RefreshListLayout()
    {
        var listRect = countryListContainer as RectTransform;
        if (listRect == null) return;

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(listRect);
        Canvas.ForceUpdateCanvases();
    }
}
