using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CitySelectUI : MonoBehaviour
{
    [Header("Legacy Scene References")]
    public CanvasGroup canvasGroup;
    public RectTransform contentPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subtitleText;
    public Transform cityListContainer;
    public Button backButton;
    public TextMeshProUGUI backButtonText;
    public Image headerAccentLine;
    public ScrollRect scrollRect;

    CityDefinition[] cities = Array.Empty<CityDefinition>();
    CityDefinition featuredCity;
    TextMeshProUGUI heroCity;
    TextMeshProUGUI heroDetail;
    TextMeshProUGUI driveLabel;
    Button driveButton;
    readonly Dictionary<CityDefinition, Outline> cardFrames = new Dictionary<CityDefinition, Outline>();

    IEnumerator Start()
    {
        UnlockManager.EnsureExists();
        yield return null;
        ResolveCities();
        BuildCinematicScreen();
        SelectInitialCity();
        if (canvasGroup)
        {
            canvasGroup.alpha = 0f;
            yield return StartCoroutine(UIAnimator.FadeIn(canvasGroup, .45f));
        }
    }

    void ResolveCities()
    {
        CountryDefinition country = GameState.Instance?.selectedCountry;
        if (country && country.cities != null)
        {
            var valid = new List<CityDefinition>();
            foreach (CityDefinition city in country.cities) if (city) valid.Add(city);
            cities = valid.ToArray();
        }
    }

    void BuildCinematicScreen()
    {
        if (!contentPanel)
        {
            Debug.LogError("CitySelectUI: contentPanel is not assigned.");
            return;
        }

        SelectionUIStyle.SetRect(contentPanel, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
        for (int i = 0; i < contentPanel.childCount; i++) contentPanel.GetChild(i).gameObject.SetActive(false);

        RectTransform screen = CreateUI("CinematicCityScreen", contentPanel);
        SelectionUIStyle.Stretch(screen, Vector2.zero, Vector2.one);
        Sprite hero = Resources.Load<Sprite>("UI/CountrySelectHero");
        Image background = CreateImage("DestinationHero", screen, Color.white);
        SelectionUIStyle.Stretch(background.rectTransform, Vector2.zero, Vector2.one);
        background.sprite = hero;
        background.preserveAspect = false;
        Image shade = CreateImage("CinematicShade", screen, new Color(.012f, .02f, .024f, .30f));
        SelectionUIStyle.Stretch(shade.rectTransform, Vector2.zero, Vector2.one);
        Image leftShade = CreateImage("CopyShade", screen, new Color(.008f, .014f, .017f, .64f));
        SelectionUIStyle.SetRect(leftShade.rectTransform, Vector2.zero, new Vector2(.50f, 1f), Vector2.zero, Vector2.zero);
        Image bottomShade = CreateImage("CarouselShade", screen, new Color(.008f, .014f, .017f, .76f));
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
        TextMeshProUGUI step = SelectionUIStyle.CreateText("Step", parent, "COUNTRY  ✓    CITY  02    ROUTE  03", 11f,
            UITheme.FontWeight.Bold, UITheme.WithAlpha(UITheme.TextPrimary, .72f), TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(step.rectTransform, new Vector2(.04f, .84f), new Vector2(.45f, .89f), Vector2.zero, Vector2.zero);
        step.characterSpacing = 1.2f;
    }

    void BuildBack(RectTransform parent)
    {
        Image image = CreateImage("BackButton", parent, new Color(.03f, .05f, .055f, .76f));
        SelectionUIStyle.SetRect(image.rectTransform, new Vector2(.855f, .88f), new Vector2(.96f, .95f), Vector2.zero, Vector2.zero);
        Button button = image.gameObject.AddComponent<Button>();
        TextMeshProUGUI label = SelectionUIStyle.CreateText("Label", image.transform, "‹  COUNTRIES", 13f,
            UITheme.FontWeight.Bold, UITheme.TextPrimary, TextAlignmentOptions.Center);
        SelectionUIStyle.Stretch(label.rectTransform, Vector2.zero, Vector2.one);
        button.onClick.AddListener(() => StartCoroutine(Transition(() => SceneLoader.Instance?.LoadCountrySelect())));
    }

    void BuildHeroCopy(RectTransform parent)
    {
        CountryDefinition country = GameState.Instance?.selectedCountry;
        string eyebrowText = country ? $"EXPLORE {country.countryName.ToUpperInvariant()}" : "COUNTRY REQUIRED";
        TextMeshProUGUI eyebrow = SelectionUIStyle.CreateText("Eyebrow", parent, eyebrowText, 14f,
            UITheme.FontWeight.Medium, UITheme.WithAlpha(UITheme.TextPrimary, .82f), TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(eyebrow.rectTransform, new Vector2(.04f, .68f), new Vector2(.46f, .75f), Vector2.zero, Vector2.zero);
        eyebrow.characterSpacing = 4f;
        heroCity = SelectionUIStyle.CreateText("FeaturedCity", parent, country ? "Choose a city" : "Choose a country first", 66f,
            UITheme.FontWeight.Bold, UITheme.TextPrimary, TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(heroCity.rectTransform, new Vector2(.04f, .52f), new Vector2(.48f, .69f), Vector2.zero, Vector2.zero);
        heroCity.enableAutoSizing = true;
        heroCity.fontSizeMin = 34f;
        heroCity.fontSizeMax = 66f;
        heroDetail = SelectionUIStyle.CreateText("FeaturedDetail", parent, "Select an operating area below", 21f,
            UITheme.FontWeight.Regular, UITheme.WithAlpha(UITheme.TextPrimary, .9f), TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(heroDetail.rectTransform, new Vector2(.04f, .46f), new Vector2(.48f, .53f), Vector2.zero, Vector2.zero);
        Image cta = CreateImage("DriveButton", parent, UITheme.Accent);
        SelectionUIStyle.SetRect(cta.rectTransform, new Vector2(.04f, .365f), new Vector2(.34f, .445f), Vector2.zero, Vector2.zero);
        SelectionUIStyle.AddOutline(cta.gameObject, UITheme.WithAlpha(Color.white, .42f));
        driveButton = cta.gameObject.AddComponent<Button>();
        driveLabel = SelectionUIStyle.CreateText("Label", cta.transform, "CHOOSE A CITY", 18f,
            UITheme.FontWeight.Bold, UITheme.Background, TextAlignmentOptions.Center);
        SelectionUIStyle.Stretch(driveLabel.rectTransform, Vector2.zero, Vector2.one);
        driveButton.interactable = false;
        driveButton.onClick.AddListener(ConfirmCity);
    }

    void BuildCarousel(RectTransform parent, Sprite hero)
    {
        Image viewport = CreateImage("CityViewport", parent, Color.clear);
        SelectionUIStyle.SetRect(viewport.rectTransform, new Vector2(.035f, .045f), new Vector2(.965f, .325f), Vector2.zero, Vector2.zero);
        viewport.gameObject.AddComponent<RectMask2D>();
        RectTransform content = CreateUI("CityFilmstrip", viewport.transform);
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

        if (cities.Length == 0)
        {
            TextMeshProUGUI empty = SelectionUIStyle.CreateText("NoCities", content,
                GameState.Instance?.selectedCountry ? "No cities are available for this country yet." : "Return to Countries and choose a destination.",
                20f, UITheme.FontWeight.Medium, UITheme.TextPrimary, TextAlignmentOptions.Left);
            empty.gameObject.AddComponent<LayoutElement>().preferredWidth = 720f;
            return;
        }
        for (int i = 0; i < cities.Length; i++) CreateCityCard(content, cities[i], hero, i);
    }

    void CreateCityCard(RectTransform parent, CityDefinition city, Sprite hero, int index)
    {
        bool unlocked = UnlockManager.Instance == null || UnlockManager.Instance.IsCityUnlocked(city);
        Image card = CreateImage($"City_{city.cityCode}", parent, UITheme.SurfaceContainer);
        LayoutElement layout = card.gameObject.AddComponent<LayoutElement>();
        layout.preferredWidth = 310f;
        layout.minWidth = 280f;
        layout.preferredHeight = 210f;
        Button button = card.gameObject.AddComponent<Button>();
        button.interactable = unlocked;
        Image photo = CreateImage("Photo", card.transform, Color.white);
        SelectionUIStyle.Stretch(photo.rectTransform, Vector2.zero, Vector2.one);
        photo.sprite = hero;
        photo.color = Color.Lerp(Color.white, UITheme.GetContinentColor(city.country), .10f + (index % 4) * .035f);
        photo.raycastTarget = false;
        Image wash = CreateImage("PhotoWash", card.transform, new Color(.01f, .02f, .025f, unlocked ? .34f : .65f));
        SelectionUIStyle.Stretch(wash.rectTransform, Vector2.zero, Vector2.one);
        Image caption = CreateImage("CaptionShade", card.transform, new Color(.01f, .018f, .02f, .76f));
        SelectionUIStyle.SetRect(caption.rectTransform, Vector2.zero, new Vector2(1f, .42f), Vector2.zero, Vector2.zero);
        TextMeshProUGUI code = SelectionUIStyle.CreateText("Code", card.transform,
            unlocked ? $"{city.cityCode}  ·  {city.climateZone}" : "LOCKED", 11f,
            UITheme.FontWeight.Bold, unlocked ? UITheme.Accent : UITheme.TextMuted, TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(code.rectTransform, new Vector2(.06f, .31f), new Vector2(.92f, .47f), Vector2.zero, Vector2.zero);
        code.characterSpacing = 1.2f;
        TextMeshProUGUI name = SelectionUIStyle.CreateText("City", card.transform, city.cityName, 24f,
            UITheme.FontWeight.Bold, unlocked ? UITheme.TextPrimary : UITheme.TextMuted, TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(name.rectTransform, new Vector2(.06f, .06f), new Vector2(.94f, .33f), Vector2.zero, Vector2.zero);
        name.enableAutoSizing = true;
        name.fontSizeMin = 17f;
        name.fontSizeMax = 24f;
        Outline frame = card.gameObject.AddComponent<Outline>();
        frame.effectColor = UITheme.WithAlpha(UITheme.Outline, .75f);
        frame.effectDistance = new Vector2(2f, -2f);
        cardFrames[city] = frame;
        CityDefinition captured = city;
        button.onClick.AddListener(() => FeatureCity(captured));
    }

    void SelectInitialCity()
    {
        CityDefinition initial = GameState.Instance?.selectedCity;
        if (!initial || Array.IndexOf(cities, initial) < 0 || !IsUnlocked(initial)) initial = Array.Find(cities, IsUnlocked);
        FeatureCity(initial);
    }

    bool IsUnlocked(CityDefinition city) => city && (UnlockManager.Instance == null || UnlockManager.Instance.IsCityUnlocked(city));

    void FeatureCity(CityDefinition city)
    {
        if (!IsUnlocked(city) || !heroCity || !heroDetail || !driveLabel || !driveButton) return;
        featuredCity = city;
        int routeCount = city.availableRoutes?.Length ?? 0;
        heroCity.text = city.cityName;
        heroDetail.text = $"{city.country}  ·  {city.climateZone} climate  ·  {routeCount} {(routeCount == 1 ? "route" : "routes")}";
        driveLabel.text = $"DRIVE IN {city.cityName.ToUpperInvariant()}   ›";
        driveButton.interactable = true;
        foreach (var pair in cardFrames)
        {
            bool selected = pair.Key == city;
            pair.Value.effectColor = selected ? UITheme.Accent : UITheme.WithAlpha(UITheme.Outline, .7f);
            pair.Value.effectDistance = selected ? new Vector2(4f, -4f) : new Vector2(2f, -2f);
        }
    }

    void ConfirmCity()
    {
        if (!featuredCity) return;
        GameState.Instance?.SelectCity(featuredCity);
        CityManager.Instance?.SetActiveCity(featuredCity);
        StartCoroutine(Transition(() => SceneLoader.Instance?.LoadRouteSelectChecked()));
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
