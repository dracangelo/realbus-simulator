using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouteSelectUI : MonoBehaviour
{
    [Header("Legacy Scene References")]
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
    readonly List<BusRoute> routes = new List<BusRoute>();
    readonly Dictionary<BusRoute, Outline> cardFrames = new Dictionary<BusRoute, Outline>();
    BusRoute featuredRoute;
    TextMeshProUGUI heroRoute;
    TextMeshProUGUI heroDetail;
    TextMeshProUGUI routeMetrics;
    TextMeshProUGUI driveLabel;
    Button driveButton;

    IEnumerator Start()
    {
        UnlockManager.EnsureExists();
        yield return null;
        ResolveRoutes();
        BuildCinematicScreen();
        SelectInitialRoute();
        if (canvasGroup)
        {
            canvasGroup.alpha = 0f;
            yield return StartCoroutine(UIAnimator.FadeIn(canvasGroup, .45f));
        }
    }

    void ResolveRoutes()
    {
        activeCity = GameState.Instance?.selectedCity;
        if (!activeCity) return;
        List<BusRoute> available = UnlockManager.Instance != null
            ? UnlockManager.Instance.GetRoutesForCity(activeCity)
            : new List<BusRoute>(activeCity.availableRoutes ?? Array.Empty<BusRoute>());
        if (available == null) return;
        foreach (BusRoute route in available)
        {
            if (!route) continue;
            route.EnsureRuntimeData();
            if (route.GetStopCount() >= 2) routes.Add(route);
        }
    }

    void BuildCinematicScreen()
    {
        if (!contentPanel)
        {
            Debug.LogError("RouteSelectUI: contentPanel is not assigned.");
            return;
        }
        SelectionUIStyle.SetRect(contentPanel, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
        for (int i = 0; i < contentPanel.childCount; i++) contentPanel.GetChild(i).gameObject.SetActive(false);

        RectTransform screen = CreateUI("CinematicRouteScreen", contentPanel);
        SelectionUIStyle.Stretch(screen, Vector2.zero, Vector2.one);
        Sprite hero = Resources.Load<Sprite>("UI/CountrySelectHero");
        Image background = CreateImage("DestinationHero", screen, Color.white);
        SelectionUIStyle.Stretch(background.rectTransform, Vector2.zero, Vector2.one);
        background.sprite = hero;
        background.preserveAspect = false;
        Image shade = CreateImage("CinematicShade", screen, new Color(.012f, .02f, .024f, .34f));
        SelectionUIStyle.Stretch(shade.rectTransform, Vector2.zero, Vector2.one);
        Image leftShade = CreateImage("CopyShade", screen, new Color(.008f, .014f, .017f, .68f));
        SelectionUIStyle.SetRect(leftShade.rectTransform, Vector2.zero, new Vector2(.52f, 1f), Vector2.zero, Vector2.zero);
        Image bottomShade = CreateImage("CarouselShade", screen, new Color(.008f, .014f, .017f, .78f));
        SelectionUIStyle.SetRect(bottomShade.rectTransform, Vector2.zero, new Vector2(1f, .36f), Vector2.zero, Vector2.zero);
        BuildBrand(screen);
        BuildBack(screen);
        BuildHeroCopy(screen);
        BuildCarousel(screen, hero);
    }

    void BuildBrand(RectTransform parent)
    {
        TextMeshProUGUI brand = SelectionUIStyle.CreateText("Brand", parent, "REAL BUS  /  SIMULATOR", 22f,
            UITheme.FontWeight.Bold, UITheme.TextPrimary, TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(brand.rectTransform, new Vector2(.04f, .88f), new Vector2(.38f, .96f), Vector2.zero, Vector2.zero);
        brand.characterSpacing = 1.5f;
        TextMeshProUGUI step = SelectionUIStyle.CreateText("Step", parent, "COUNTRY  ✓    CITY  ✓    ROUTE  03", 11f,
            UITheme.FontWeight.Bold, UITheme.WithAlpha(UITheme.TextPrimary, .72f), TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(step.rectTransform, new Vector2(.04f, .84f), new Vector2(.45f, .89f), Vector2.zero, Vector2.zero);
        step.characterSpacing = 1.2f;
    }

    void BuildBack(RectTransform parent)
    {
        Image image = CreateImage("BackButton", parent, new Color(.03f, .05f, .055f, .76f));
        SelectionUIStyle.SetRect(image.rectTransform, new Vector2(.875f, .88f), new Vector2(.96f, .95f), Vector2.zero, Vector2.zero);
        Button button = image.gameObject.AddComponent<Button>();
        TextMeshProUGUI label = SelectionUIStyle.CreateText("Label", image.transform, "‹  CITIES", 13f,
            UITheme.FontWeight.Bold, UITheme.TextPrimary, TextAlignmentOptions.Center);
        SelectionUIStyle.Stretch(label.rectTransform, Vector2.zero, Vector2.one);
        button.onClick.AddListener(() => StartCoroutine(Transition(() => SceneLoader.Instance?.LoadCitySelectChecked())));
    }

    void BuildHeroCopy(RectTransform parent)
    {
        string eyebrowText = activeCity ? $"SERVICES IN {activeCity.cityName.ToUpperInvariant()}" : "CITY REQUIRED";
        TextMeshProUGUI eyebrow = SelectionUIStyle.CreateText("Eyebrow", parent, eyebrowText, 14f,
            UITheme.FontWeight.Medium, UITheme.WithAlpha(UITheme.TextPrimary, .82f), TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(eyebrow.rectTransform, new Vector2(.04f, .69f), new Vector2(.49f, .76f), Vector2.zero, Vector2.zero);
        eyebrow.characterSpacing = 3.5f;
        heroRoute = SelectionUIStyle.CreateText("FeaturedRoute", parent, activeCity ? "Choose a route" : "Choose a city first", 52f,
            UITheme.FontWeight.Bold, UITheme.TextPrimary, TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(heroRoute.rectTransform, new Vector2(.04f, .55f), new Vector2(.50f, .70f), Vector2.zero, Vector2.zero);
        heroRoute.enableAutoSizing = true;
        heroRoute.fontSizeMin = 28f;
        heroRoute.fontSizeMax = 52f;
        heroDetail = SelectionUIStyle.CreateText("FeaturedDetail", parent, "Select a service below", 20f,
            UITheme.FontWeight.Regular, UITheme.WithAlpha(UITheme.TextPrimary, .9f), TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(heroDetail.rectTransform, new Vector2(.04f, .49f), new Vector2(.50f, .56f), Vector2.zero, Vector2.zero);
        routeMetrics = SelectionUIStyle.CreateText("RouteMetrics", parent, "DISTANCE  —    TIME  —    STOPS  —", 13f,
            UITheme.FontWeight.Bold, UITheme.Accent, TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(routeMetrics.rectTransform, new Vector2(.04f, .445f), new Vector2(.50f, .50f), Vector2.zero, Vector2.zero);
        routeMetrics.characterSpacing = 1f;
        Image cta = CreateImage("DriveButton", parent, UITheme.Accent);
        SelectionUIStyle.SetRect(cta.rectTransform, new Vector2(.04f, .35f), new Vector2(.34f, .43f), Vector2.zero, Vector2.zero);
        SelectionUIStyle.AddOutline(cta.gameObject, UITheme.WithAlpha(Color.white, .42f));
        driveButton = cta.gameObject.AddComponent<Button>();
        driveLabel = SelectionUIStyle.CreateText("Label", cta.transform, "CHOOSE A ROUTE", 18f,
            UITheme.FontWeight.Bold, UITheme.Background, TextAlignmentOptions.Center);
        SelectionUIStyle.Stretch(driveLabel.rectTransform, Vector2.zero, Vector2.one);
        driveButton.interactable = false;
        driveButton.onClick.AddListener(ConfirmRoute);
    }

    void BuildCarousel(RectTransform parent, Sprite hero)
    {
        Image viewport = CreateImage("RouteViewport", parent, Color.clear);
        SelectionUIStyle.SetRect(viewport.rectTransform, new Vector2(.035f, .045f), new Vector2(.965f, .325f), Vector2.zero, Vector2.zero);
        viewport.gameObject.AddComponent<RectMask2D>();
        RectTransform content = CreateUI("RouteFilmstrip", viewport.transform);
        content.anchorMin = new Vector2(0f, 0f);
        content.anchorMax = new Vector2(0f, 1f);
        content.pivot = new Vector2(0f, .5f);
        content.anchoredPosition = Vector2.zero;
        content.sizeDelta = Vector2.zero;
        HorizontalLayoutGroup row = content.gameObject.AddComponent<HorizontalLayoutGroup>();
        row.padding = new RectOffset(4, 18, 4, 4);
        row.spacing = 16f;
        row.childAlignment = TextAnchor.MiddleLeft;
        row.childControlWidth = true;
        row.childControlHeight = true;
        row.childForceExpandWidth = false;
        row.childForceExpandHeight = true;
        ContentSizeFitter fitter = content.gameObject.AddComponent<ContentSizeFitter>();
        fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        fitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        ScrollRect scroll = viewport.gameObject.AddComponent<ScrollRect>();
        scroll.viewport = viewport.rectTransform;
        scroll.content = content;
        scroll.horizontal = true;
        scroll.vertical = false;
        scroll.movementType = ScrollRect.MovementType.Elastic;
        scroll.inertia = true;

        if (routes.Count == 0)
        {
            TextMeshProUGUI empty = SelectionUIStyle.CreateText("NoRoutes", content,
                activeCity ? $"No routes are available for {activeCity.cityName} yet." : "Return to Cities and choose an operating area.",
                20f, UITheme.FontWeight.Medium, UITheme.TextPrimary, TextAlignmentOptions.Left);
            empty.gameObject.AddComponent<LayoutElement>().preferredWidth = 720f;
            return;
        }
        for (int i = 0; i < routes.Count; i++) CreateRouteCard(content, routes[i], hero, i);
    }

    void CreateRouteCard(RectTransform parent, BusRoute route, Sprite hero, int index)
    {
        int difficulty = Mathf.Clamp(route.difficulty, 1, 5);
        Color accent = GetServiceColor(difficulty);
        Image card = CreateImage($"Route_{route.GetProgressionId(activeCity ? activeCity.cityCode : null)}", parent, UITheme.SurfaceContainer);
        LayoutElement layout = card.gameObject.AddComponent<LayoutElement>();
        layout.preferredWidth = 345f;
        layout.minWidth = 310f;
        layout.preferredHeight = 210f;
        Button button = card.gameObject.AddComponent<Button>();
        Image photo = CreateImage("Photo", card.transform, Color.Lerp(Color.white, accent, .08f + (index % 3) * .03f));
        SelectionUIStyle.Stretch(photo.rectTransform, Vector2.zero, Vector2.one);
        photo.sprite = hero;
        photo.raycastTarget = false;
        Image wash = CreateImage("PhotoWash", card.transform, new Color(.01f, .02f, .025f, .40f));
        SelectionUIStyle.Stretch(wash.rectTransform, Vector2.zero, Vector2.one);
        Image caption = CreateImage("CaptionShade", card.transform, new Color(.01f, .018f, .02f, .80f));
        SelectionUIStyle.SetRect(caption.rectTransform, Vector2.zero, new Vector2(1f, .49f), Vector2.zero, Vector2.zero);
        TextMeshProUGUI code = SelectionUIStyle.CreateText("Code", card.transform,
            $"LINE {BuildRouteCode(route)}  ·  {GetServiceClass(difficulty).ToUpperInvariant()}", 11f,
            UITheme.FontWeight.Bold, accent, TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(code.rectTransform, new Vector2(.055f, .38f), new Vector2(.94f, .52f), Vector2.zero, Vector2.zero);
        code.characterSpacing = 1f;
        TextMeshProUGUI name = SelectionUIStyle.CreateText("Route", card.transform, route.routeName, 21f,
            UITheme.FontWeight.Bold, UITheme.TextPrimary, TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(name.rectTransform, new Vector2(.055f, .15f), new Vector2(.94f, .40f), Vector2.zero, Vector2.zero);
        name.enableAutoSizing = true;
        name.fontSizeMin = 15f;
        name.fontSizeMax = 21f;
        TextMeshProUGUI stats = SelectionUIStyle.CreateText("Stats", card.transform,
            $"{route.distanceKm:F1} KM  ·  {route.estimatedTimeMinutes:F0} MIN  ·  {route.GetStopCount()} STOPS", 10f,
            UITheme.FontWeight.Medium, UITheme.TextSecondary, TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(stats.rectTransform, new Vector2(.055f, .025f), new Vector2(.94f, .17f), Vector2.zero, Vector2.zero);
        Outline frame = card.gameObject.AddComponent<Outline>();
        frame.effectColor = UITheme.WithAlpha(UITheme.Outline, .75f);
        frame.effectDistance = new Vector2(2f, -2f);
        cardFrames[route] = frame;
        BusRoute captured = route;
        button.onClick.AddListener(() => FeatureRoute(captured));
    }

    void SelectInitialRoute()
    {
        BusRoute initial = GameState.Instance?.selectedRoute;
        if (!initial || !routes.Contains(initial)) initial = routes.Count > 0 ? routes[0] : null;
        FeatureRoute(initial);
    }

    void FeatureRoute(BusRoute route)
    {
        if (!route || !heroRoute || !heroDetail || !routeMetrics || !driveLabel || !driveButton) return;
        featuredRoute = route;
        int difficulty = Mathf.Clamp(route.difficulty, 1, 5);
        heroRoute.text = route.routeName;
        heroDetail.text = $"Line {BuildRouteCode(route)}  ·  {GetServiceClass(difficulty)} service  ·  Difficulty {difficulty}/5";
        routeMetrics.text = $"DISTANCE  {route.distanceKm:F1} KM    TIME  {route.estimatedTimeMinutes:F0} MIN    STOPS  {route.GetStopCount()}";
        driveLabel.text = $"START LINE {BuildRouteCode(route)}   ›";
        driveButton.interactable = true;
        foreach (var pair in cardFrames)
        {
            bool selected = pair.Key == route;
            pair.Value.effectColor = selected ? UITheme.Accent : UITheme.WithAlpha(UITheme.Outline, .7f);
            pair.Value.effectDistance = selected ? new Vector2(4f, -4f) : new Vector2(2f, -2f);
        }
    }

    string BuildRouteCode(BusRoute route)
    {
        string code = string.IsNullOrWhiteSpace(route.routeNumber) ? "R1" : route.routeNumber.Trim().ToUpperInvariant();
        return activeCity ? $"{activeCity.cityCode}-{code}" : code;
    }

    static string GetServiceClass(int difficulty)
    {
        if (difficulty <= 1) return "Local";
        if (difficulty == 2) return "Standard";
        if (difficulty == 3) return "Rapid";
        if (difficulty == 4) return "Express";
        return "Priority";
    }

    static Color GetServiceColor(int difficulty)
    {
        if (difficulty <= 1) return UITheme.Secondary;
        if (difficulty == 2) return UITheme.Tertiary;
        if (difficulty == 3) return UITheme.Accent;
        if (difficulty == 4) return UITheme.AccentDim;
        return UITheme.Error;
    }

    void ConfirmRoute()
    {
        if (!featuredRoute) return;
        GameState.Instance?.SelectRoute(featuredRoute);
        StartCoroutine(Transition(() => SceneLoader.Instance?.LoadGameChecked()));
    }

    IEnumerator Transition(Action load)
    {
        if (canvasGroup) yield return StartCoroutine(UIAnimator.FadeOut(canvasGroup, .25f));
        load?.Invoke();
    }

    static RectTransform CreateUI(string name, Transform parent)
    {
        GameObject obj = new GameObject(name, typeof(RectTransform));
        obj.transform.SetParent(parent, false);
        return obj.GetComponent<RectTransform>();
    }

    static Image CreateImage(string name, Transform parent, Color color)
    {
        GameObject obj = new GameObject(name, typeof(RectTransform), typeof(Image));
        obj.transform.SetParent(parent, false);
        Image image = obj.GetComponent<Image>();
        image.color = color;
        return image;
    }
}
