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
            titleText.text = "SELECT ROUTE";
            titleText.color = UITheme.TextPrimary;
            titleText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            titleText.characterSpacing = 6f;
        }

        if (accentLine)
            accentLine.color = UITheme.Accent;

        // City name from selection
        var city = GameState.Instance?.selectedCity
                ?? CityManager.Instance?.activeCity;

        if (cityNameText)
        {
            cityNameText.text = city != null
                ? $"{city.cityName}, {city.country}"
                : "Select a city first";
            cityNameText.color = UITheme.TextSecondary;
            cityNameText.font = UITheme.GetFont(UITheme.FontWeight.Regular);
        }

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

    void PopulateRouteList()
    {
        foreach (Transform child in routeListContainer)
            Destroy(child.gameObject);

        var city = GameState.Instance?.selectedCity
                ?? CityManager.Instance?.activeCity;

        Debug.Log($"RouteSelect: city={city?.cityName}, routes={city?.availableRoutes?.Length ?? 0}");

        // Fallback for testing — use first available city
        if (city == null && CityManager.Instance?.allCities?.Length > 0)
            city = CityManager.Instance.allCities[0];

        if (city == null)
        {
            CreateEmptyState("No city selected.");
            return;
        }

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

    void CreateRouteCard(BusRoute route)
    {
        GameObject card = new GameObject($"Card_{route.name}");
        card.transform.SetParent(routeListContainer, false);

        var rect = card.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(0, 100);

        var img = card.AddComponent<Image>();
        img.color = UITheme.Surface;

        var btn = card.AddComponent<Button>();
        var cols = btn.colors;
        cols.normalColor = UITheme.Surface;
        cols.highlightedColor = UITheme.SurfaceContainer;
        cols.pressedColor = UITheme.WithAlpha(UITheme.Surface, 0.6f);
        btn.colors = cols;

        // Left accent bar — green for routes
        GameObject bar = new GameObject("AccentBar");
        bar.transform.SetParent(card.transform, false);
        var barRect = bar.AddComponent<RectTransform>();
        barRect.anchorMin = new Vector2(0f, 0f);
        barRect.anchorMax = new Vector2(0f, 1f);
        barRect.sizeDelta = new Vector2(4f, 0f);
        barRect.anchoredPosition = new Vector2(2f, 0f);
        var barImg = bar.AddComponent<Image>();
        barImg.color = UITheme.Success;

        // Route name
        GameObject nameObj = new GameObject("Text_Name");
        nameObj.transform.SetParent(card.transform, false);
        var nameTmp = nameObj.AddComponent<TextMeshProUGUI>();
        nameTmp.text = route.routeName;
        nameTmp.fontSize = 20;
        nameTmp.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        nameTmp.color = UITheme.TextPrimary;
        nameTmp.alignment = TextAlignmentOptions.Left;
        nameTmp.overflowMode = TextOverflowModes.Ellipsis;
        var nameRect = nameObj.GetComponent<RectTransform>();
        nameRect.anchorMin = new Vector2(0f, 0.5f);
        nameRect.anchorMax = new Vector2(0.8f, 1f);
        nameRect.offsetMin = new Vector2(20f, 0f);
        nameRect.offsetMax = new Vector2(0f, -8f);

        // Stop count + fare
        GameObject infoObj = new GameObject("Text_Info");
        infoObj.transform.SetParent(card.transform, false);
        var infoTmp = infoObj.AddComponent<TextMeshProUGUI>();
        infoTmp.text = $"{route.stops.Length} stops  •  KES {route.baseFare:F0} fare";
        infoTmp.fontSize = 14;
        infoTmp.font = UITheme.GetFont(UITheme.FontWeight.Regular);
        infoTmp.color = UITheme.TextSecondary;
        infoTmp.alignment = TextAlignmentOptions.Left;
        var infoRect = infoObj.GetComponent<RectTransform>();
        infoRect.anchorMin = new Vector2(0f, 0f);
        infoRect.anchorMax = new Vector2(0.8f, 0.5f);
        infoRect.offsetMin = new Vector2(20f, 8f);
        infoRect.offsetMax = Vector2.zero;

        // Arrow
        GameObject arrowObj = new GameObject("Text_Arrow");
        arrowObj.transform.SetParent(card.transform, false);
        var arrowTmp = arrowObj.AddComponent<TextMeshProUGUI>();
        arrowTmp.text = "→";
        arrowTmp.fontSize = 24;
        arrowTmp.font = UITheme.GetFont(UITheme.FontWeight.Light);
        arrowTmp.color = UITheme.WithAlpha(UITheme.Success, 0.7f);
        arrowTmp.alignment = TextAlignmentOptions.Right;
        var arrowRect = arrowObj.GetComponent<RectTransform>();
        arrowRect.anchorMin = new Vector2(0.8f, 0f);
        arrowRect.anchorMax = new Vector2(1f, 1f);
        arrowRect.offsetMin = Vector2.zero;
        arrowRect.offsetMax = new Vector2(-20f, 0f);

        var capturedRoute = route;
        btn.onClick.AddListener(() => OnRouteSelected(capturedRoute));
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