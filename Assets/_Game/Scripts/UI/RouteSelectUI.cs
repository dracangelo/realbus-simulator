using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class RouteSelectUI : MonoBehaviour
{
    [Header("UI References")]
    public CanvasGroup canvasGroup;
    public RectTransform contentPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI cityNameText;
    public Image accentLine;
    public Transform routeListContainer;
    public Button backButton;
    public TextMeshProUGUI backButtonText;
    public Image backgroundPanel;

    CityDefinition activeCity;

    void Start()
    {
        ApplyTheme();
        SetupButtons();
        PopulateRouteList();
        StartCoroutine(AnimateIn());
    }

    void ApplyTheme()
    {
        if (backgroundPanel)
            backgroundPanel.color = UITheme.Background;

        if (titleText)
        {
            titleText.text = "ROUTE BOARD";
            titleText.color = UITheme.TextPrimary;
            titleText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            titleText.characterSpacing = 6f;
        }

        if (accentLine)
            accentLine.color = UITheme.Accent;

        // City name from selection
        var city = ResolveActiveCity();

        if (cityNameText)
        {
            cityNameText.text = city != null
                ? $"CITY OPERATIONS — {city.cityName}, {city.country}"
                : "Select a city first";
            cityNameText.color = UITheme.TextSecondary;
            cityNameText.font = UITheme.GetFont(UITheme.FontWeight.Regular);
        }

        if (backButtonText)
        {
            backButtonText.text = "BACK TO CITY";
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

    void PopulateRouteList()
    {
        foreach (Transform child in routeListContainer)
            Destroy(child.gameObject);

        var city = ResolveActiveCity();

        Debug.Log($"RouteSelect: city={city?.cityName}, routes={city?.availableRoutes?.Length ?? 0}");

        if (city == null)
        {
            CreateEmptyState("No city selected.");
            return;
        }

        activeCity = city;
        var routes = city.availableRoutes;

        if (routes == null || routes.Length == 0)
        {
            CreateEmptyState($"No routes available for {city.cityName} yet.");
            return;
        }

        foreach (var route in routes)
            if (route != null)
                CreateRouteCard(route);
    }

    CityDefinition ResolveActiveCity()
    {
        var cityManager = CityManager.Instance;
        var gameState = GameState.Instance;

        var city = gameState?.selectedCity ?? cityManager?.activeCity;
        if (HasRoutes(city))
            return city;

        if (cityManager?.activeCountry?.cities != null)
        {
            for (int i = 0; i < cityManager.activeCountry.cities.Length; i++)
            {
                var candidate = cityManager.activeCountry.cities[i];
                if (!HasRoutes(candidate)) continue;
                ApplyResolvedCity(candidate);
                return candidate;
            }
        }

        if (cityManager?.allCities != null)
        {
            for (int i = 0; i < cityManager.allCities.Length; i++)
            {
                var candidate = cityManager.allCities[i];
                if (!HasRoutes(candidate)) continue;
                ApplyResolvedCity(candidate);
                return candidate;
            }
        }

        return city;
    }

    bool HasRoutes(CityDefinition city)
    {
        return city != null && city.availableRoutes != null && city.availableRoutes.Length > 0;
    }

    void ApplyResolvedCity(CityDefinition city)
    {
        if (city == null) return;

        activeCity = city;

        if (CityManager.Instance != null)
            CityManager.Instance.activeCity = city;

        if (GameState.Instance != null && GameState.Instance.selectedCity == null)
            GameState.Instance.selectedCity = city;
    }

    void CreateRouteCard(BusRoute route)
    {
        GameObject card = new GameObject($"Card_{route.name}");
        card.transform.SetParent(routeListContainer, false);

        var rect = card.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(400, 170);

        var img = card.AddComponent<Image>();
        img.color = UITheme.SurfaceContainer;

        var btn = card.AddComponent<Button>();
        var cols = btn.colors;
        cols.normalColor = UITheme.SurfaceContainer;
        cols.highlightedColor = UITheme.SurfaceHigh;
        cols.pressedColor = UITheme.WithAlpha(UITheme.SurfaceHigh, 0.7f);
        btn.colors = cols;

        int stopCount = route.GetStopCount();
        string routeCode = BuildRouteCode(route);
        string serviceClass = GetServiceClass(route.difficulty);
        Color serviceColor = GetServiceColor(route.difficulty);
        float distanceKm = Mathf.Max(0f, route.distanceKm);
        float etaMinutes = Mathf.Max(0f, route.estimatedTimeMinutes);
        float headwayMinutes = GetHeadwayMinutes(route.difficulty);
        int cycleMinutes = Mathf.RoundToInt(etaMinutes * 2f);
        int revenue = Mathf.RoundToInt(route.baseFare * Mathf.Max(1, stopCount) * 0.9f);

        // Left service bar
        GameObject bar = new GameObject("AccentBar");
        bar.transform.SetParent(card.transform, false);
        var barRect = bar.AddComponent<RectTransform>();
        barRect.anchorMin = new Vector2(0f, 0f);
        barRect.anchorMax = new Vector2(0f, 1f);
        barRect.sizeDelta = new Vector2(6f, 0f);
        barRect.anchoredPosition = new Vector2(3f, 0f);
        var barImg = bar.AddComponent<Image>();
        barImg.color = serviceColor;

        var headerTmp = CreateText("Text_Header", card.transform,
            $"LINE {routeCode}  •  {stopCount} STOPS", 12,
            UITheme.GetFont(UITheme.FontWeight.Medium), UITheme.TextSecondary,
            TextAlignmentOptions.Left);
        var headerRect = headerTmp.GetComponent<RectTransform>();
        headerRect.anchorMin = new Vector2(0f, 1f);
        headerRect.anchorMax = new Vector2(0.72f, 1f);
        headerRect.pivot = new Vector2(0f, 1f);
        headerRect.anchoredPosition = new Vector2(20f, -10f);
        headerRect.sizeDelta = new Vector2(0f, 18f);

        var nameTmp = CreateText("Text_Name", card.transform,
            route.routeName, 22,
            UITheme.GetFont(UITheme.FontWeight.Bold), UITheme.TextPrimary,
            TextAlignmentOptions.Left);
        nameTmp.overflowMode = TextOverflowModes.Ellipsis;
        var nameRect = nameTmp.GetComponent<RectTransform>();
        nameRect.anchorMin = new Vector2(0f, 0.45f);
        nameRect.anchorMax = new Vector2(0.72f, 0.9f);
        nameRect.offsetMin = new Vector2(20f, 0f);
        nameRect.offsetMax = new Vector2(0f, -24f);

        string metaLine = $"Distance {distanceKm:F1} km  •  Est. time {etaMinutes:F0} min  •  Cycle {cycleMinutes} min  •  Headway {headwayMinutes:F0} min";
        var metaTmp = CreateText("Text_Meta", card.transform,
            metaLine, 13,
            UITheme.GetFont(UITheme.FontWeight.Regular), UITheme.TextSecondary,
            TextAlignmentOptions.Left);
        metaTmp.overflowMode = TextOverflowModes.Ellipsis;
        var metaRect = metaTmp.GetComponent<RectTransform>();
        metaRect.anchorMin = new Vector2(0f, 0.12f);
        metaRect.anchorMax = new Vector2(0.72f, 0.45f);
        metaRect.offsetMin = new Vector2(20f, 0f);
        metaRect.offsetMax = new Vector2(0f, 0f);

        var revenueLabel = CreateText("Text_RevenueLabel", card.transform,
            "REVENUE / TRIP", 11,
            UITheme.GetFont(UITheme.FontWeight.Medium), UITheme.TextMuted,
            TextAlignmentOptions.Right);
        var revenueLabelRect = revenueLabel.GetComponent<RectTransform>();
        revenueLabelRect.anchorMin = new Vector2(0.72f, 0.55f);
        revenueLabelRect.anchorMax = new Vector2(1f, 0.9f);
        revenueLabelRect.offsetMin = new Vector2(0f, 0f);
        revenueLabelRect.offsetMax = new Vector2(-18f, 0f);

        var revenueValue = CreateText("Text_RevenueValue", card.transform,
            $"KES {revenue:N0}", 20,
            UITheme.GetFont(UITheme.FontWeight.Bold), UITheme.Accent,
            TextAlignmentOptions.Right);
        var revenueValueRect = revenueValue.GetComponent<RectTransform>();
        revenueValueRect.anchorMin = new Vector2(0.72f, 0.2f);
        revenueValueRect.anchorMax = new Vector2(1f, 0.55f);
        revenueValueRect.offsetMin = new Vector2(0f, 0f);
        revenueValueRect.offsetMax = new Vector2(-18f, 0f);

        var chip = new GameObject("ServiceChip");
        chip.transform.SetParent(card.transform, false);
        var chipRect = chip.AddComponent<RectTransform>();
        chipRect.anchorMin = new Vector2(1f, 1f);
        chipRect.anchorMax = new Vector2(1f, 1f);
        chipRect.pivot = new Vector2(1f, 1f);
        chipRect.sizeDelta = new Vector2(120f, 24f);
        chipRect.anchoredPosition = new Vector2(-18f, -10f);
        var chipImg = chip.AddComponent<Image>();
        chipImg.color = UITheme.WithAlpha(serviceColor, 0.18f);

        var chipText = CreateText("Text", chip.transform,
            serviceClass.ToUpperInvariant(), 11,
            UITheme.GetFont(UITheme.FontWeight.Bold), serviceColor,
            TextAlignmentOptions.Center);
        var chipTextRect = chipText.GetComponent<RectTransform>();
        chipTextRect.anchorMin = new Vector2(0f, 0f);
        chipTextRect.anchorMax = new Vector2(1f, 1f);
        chipTextRect.offsetMin = Vector2.zero;
        chipTextRect.offsetMax = Vector2.zero;

        var capturedRoute = route;
        btn.onClick.AddListener(() => OnRouteSelected(capturedRoute));
    }

    TextMeshProUGUI CreateText(string name, Transform parent, string text, int fontSize,
        TMP_FontAsset font, Color color, TextAlignmentOptions alignment)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(parent, false);
        var tmp = obj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = fontSize;
        tmp.font = font;
        tmp.color = color;
        tmp.alignment = alignment;
        return tmp;
    }

    string BuildRouteCode(BusRoute route)
    {
        string cityCode = activeCity != null ? activeCity.cityCode : "CITY";
        string number = string.IsNullOrWhiteSpace(route.routeNumber)
            ? "R"
            : route.routeNumber.Trim().ToUpperInvariant();

        if (number == "R")
            number = BuildFallbackCode(route.routeName);

        return $"{cityCode}-{number}";
    }

    string BuildFallbackCode(string routeName)
    {
        if (string.IsNullOrWhiteSpace(routeName)) return "R1";
        string[] parts = routeName.Split(' ');
        string code = "";
        for (int i = 0; i < parts.Length && code.Length < 3; i++)
        {
            if (string.IsNullOrWhiteSpace(parts[i])) continue;
            char c = char.ToUpperInvariant(parts[i][0]);
            if (char.IsLetterOrDigit(c)) code += c;
        }
        if (code.Length == 0) code = "R";
        return code;
    }

    string GetServiceClass(int difficulty)
    {
        if (difficulty <= 1) return "Local";
        if (difficulty == 2) return "Standard";
        if (difficulty == 3) return "Rapid";
        if (difficulty == 4) return "Express";
        return "Priority";
    }

    Color GetServiceColor(int difficulty)
    {
        if (difficulty <= 1) return UITheme.Secondary;
        if (difficulty == 2) return UITheme.Tertiary;
        if (difficulty == 3) return UITheme.Accent;
        if (difficulty == 4) return UITheme.AccentDim;
        return UITheme.Error;
    }

    float GetHeadwayMinutes(int difficulty)
    {
        float t = Mathf.InverseLerp(1f, 5f, Mathf.Clamp(difficulty, 1, 5));
        return Mathf.Lerp(14f, 6f, t);
    }

    void CreateEmptyState(string message)
    {
        GameObject msgObj = new GameObject("Text_Empty");
        msgObj.transform.SetParent(routeListContainer, false);
        var tmp = msgObj.AddComponent<TextMeshProUGUI>();
        tmp.text = message;
        tmp.fontSize = 18;
        tmp.font = UITheme.GetFont(UITheme.FontWeight.Regular);
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = UITheme.TextMuted;
        var rect = msgObj.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(0, 120);
    }

    void OnRouteSelected(BusRoute route)
    {
        Debug.Log($"Route selected: {route.routeName}");
        if (GameState.Instance != null) GameState.Instance.SelectRoute(route);
        StartCoroutine(TransitionToGame());
    }

    void OnBack()
    {
        Debug.Log($"Back pressed — SceneLoader: {SceneLoader.Instance != null}");
        StartCoroutine(TransitionToCitySelect());
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

    IEnumerator TransitionToCitySelect()
    {
        if (canvasGroup)
            yield return StartCoroutine(UIAnimator.FadeOut(canvasGroup, 0.25f));

        SceneLoader.Instance?.LoadCitySelect();
    }

    IEnumerator TransitionToGame()
    {
        if (canvasGroup)
            yield return StartCoroutine(UIAnimator.FadeOut(canvasGroup, 0.25f));

        SceneLoader.Instance?.LoadGame();
    }
}
