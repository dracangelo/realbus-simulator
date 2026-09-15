using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountrySelectUI : MonoBehaviour
{
    [Header("Legacy Scene References")]
    public CanvasGroup canvasGroup;
    public RectTransform contentPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subtitleText;
    public Image accentLine;
    public Transform countryListContainer;
    public Button backButton;
    public TextMeshProUGUI backButtonText;
    public Image backgroundPanel;

    [Header("Reliable Runtime Catalog")]
    [Tooltip("Used when this scene starts without the Main Menu manager.")]
    public CountryDefinition[] fallbackCountries = Array.Empty<CountryDefinition>();

    CountryDefinition[] countries = Array.Empty<CountryDefinition>();
    CountryDefinition featuredCountry;
    TextMeshProUGUI heroCountry;
    TextMeshProUGUI heroDetail;
    TextMeshProUGUI driveLabel;
    Button driveButton;
    readonly Dictionary<CountryDefinition, Image> cardFrames = new Dictionary<CountryDefinition, Image>();

    IEnumerator Start()
    {
        yield return null;
        countries = ResolveCountries();
        BuildCinematicScreen();
        SelectInitialCountry();
        if (canvasGroup)
        {
            canvasGroup.alpha = 0f;
            yield return StartCoroutine(UIAnimator.FadeIn(canvasGroup, .45f));
        }
    }

    CountryDefinition[] ResolveCountries()
    {
        var manager = CityManager.Instance;
        if (manager != null && manager.allCountries != null && manager.allCountries.Length > 0)
            return RemoveNulls(manager.allCountries);

        CountryDefinition[] fallback = RemoveNulls(fallbackCountries);
        if (manager != null && fallback.Length > 0)
        {
            manager.allCountries = fallback;
            var cityList = new List<CityDefinition>();
            foreach (var country in fallback)
                if (country.cities != null)
                    foreach (var city in country.cities)
                        if (city && !cityList.Contains(city)) cityList.Add(city);
            manager.allCities = cityList.ToArray();
        }
        return fallback;
    }

    static CountryDefinition[] RemoveNulls(CountryDefinition[] source)
    {
        if (source == null) return Array.Empty<CountryDefinition>();
        var result = new List<CountryDefinition>(source.Length);
        foreach (var item in source) if (item) result.Add(item);
        return result.ToArray();
    }

    void BuildCinematicScreen()
    {
        if (!contentPanel) return;
        SelectionUIStyle.SetRect(contentPanel, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
        for (int i = 0; i < contentPanel.childCount; i++)
            contentPanel.GetChild(i).gameObject.SetActive(false);

        var screen = CreateUI("CinematicCountryScreen", contentPanel);
        SelectionUIStyle.Stretch(screen, Vector2.zero, Vector2.one);

        Sprite hero = Resources.Load<Sprite>("UI/CountrySelectHero");
        var background = CreateImage("DestinationHero", screen, Color.white);
        SelectionUIStyle.Stretch(background.rectTransform, Vector2.zero, Vector2.one);
        background.sprite = hero;
        background.preserveAspect = false;

        var shade = CreateImage("CinematicShade", screen, new Color(.015f, .025f, .028f, .24f));
        SelectionUIStyle.Stretch(shade.rectTransform, Vector2.zero, Vector2.one);
        var leftShade = CreateImage("CopyShade", screen, new Color(.01f, .018f, .02f, .60f));
        SelectionUIStyle.SetRect(leftShade.rectTransform, Vector2.zero, new Vector2(.48f, 1f), Vector2.zero, Vector2.zero);
        var bottomShade = CreateImage("CarouselShade", screen, new Color(.01f, .018f, .02f, .72f));
        SelectionUIStyle.SetRect(bottomShade.rectTransform, new Vector2(0f, 0f), new Vector2(1f, .36f), Vector2.zero, Vector2.zero);

        BuildBrand(screen);
        BuildBackButton(screen);
        BuildHeroCopy(screen);
        BuildCarousel(screen, hero);
    }

    void BuildBrand(RectTransform parent)
    {
        var brand = SelectionUIStyle.CreateText("Brand", parent, "REAL BUS  /  SIMULATOR", 22f,
            UITheme.FontWeight.Bold, UITheme.TextPrimary, TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(brand.rectTransform, new Vector2(.04f, .88f), new Vector2(.38f, .96f), Vector2.zero, Vector2.zero);
        brand.characterSpacing = 1.5f;

        var tagline = SelectionUIStyle.CreateText("BrandTagline", parent, "EXPLORE  ·  DRIVE  ·  BELONG", 10f,
            UITheme.FontWeight.Medium, UITheme.WithAlpha(UITheme.TextPrimary, .72f), TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(tagline.rectTransform, new Vector2(.04f, .845f), new Vector2(.38f, .89f), Vector2.zero, Vector2.zero);
        tagline.characterSpacing = 2f;
    }

    void BuildBackButton(RectTransform parent)
    {
        var buttonObject = CreateImage("BackButton", parent, new Color(.03f, .05f, .055f, .72f));
        SelectionUIStyle.SetRect(buttonObject.rectTransform, new Vector2(.875f, .88f), new Vector2(.96f, .95f), Vector2.zero, Vector2.zero);
        var button = buttonObject.gameObject.AddComponent<Button>();
        var label = SelectionUIStyle.CreateText("Label", buttonObject.transform, "‹  MENU", 13f,
            UITheme.FontWeight.Bold, UITheme.TextPrimary, TextAlignmentOptions.Center);
        SelectionUIStyle.Stretch(label.rectTransform, Vector2.zero, Vector2.one);
        button.onClick.AddListener(() => StartCoroutine(Transition(() => SceneLoader.Instance?.LoadMainMenu())));
    }

    void BuildHeroCopy(RectTransform parent)
    {
        var eyebrow = SelectionUIStyle.CreateText("Eyebrow", parent, "CHOOSE YOUR JOURNEY", 14f,
            UITheme.FontWeight.Medium, UITheme.WithAlpha(UITheme.TextPrimary, .82f), TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(eyebrow.rectTransform, new Vector2(.04f, .68f), new Vector2(.43f, .75f), Vector2.zero, Vector2.zero);
        eyebrow.characterSpacing = 4f;

        heroCountry = SelectionUIStyle.CreateText("FeaturedCountry", parent, "Choose a country", 72f,
            UITheme.FontWeight.Bold, UITheme.TextPrimary, TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(heroCountry.rectTransform, new Vector2(.04f, .52f), new Vector2(.45f, .69f), Vector2.zero, Vector2.zero);
        heroCountry.enableAutoSizing = true;
        heroCountry.fontSizeMin = 40f;
        heroCountry.fontSizeMax = 72f;

        heroDetail = SelectionUIStyle.CreateText("FeaturedDetail", parent, "Select a destination below", 22f,
            UITheme.FontWeight.Regular, UITheme.WithAlpha(UITheme.TextPrimary, .9f), TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(heroDetail.rectTransform, new Vector2(.04f, .46f), new Vector2(.45f, .53f), Vector2.zero, Vector2.zero);

        var cta = CreateImage("DriveButton", parent, UITheme.Accent);
        SelectionUIStyle.SetRect(cta.rectTransform, new Vector2(.04f, .365f), new Vector2(.34f, .445f), Vector2.zero, Vector2.zero);
        SelectionUIStyle.AddOutline(cta.gameObject, UITheme.WithAlpha(Color.white, .42f));
        driveButton = cta.gameObject.AddComponent<Button>();
        driveLabel = SelectionUIStyle.CreateText("Label", cta.transform, "CHOOSE A COUNTRY", 18f,
            UITheme.FontWeight.Bold, UITheme.Background, TextAlignmentOptions.Center);
        SelectionUIStyle.Stretch(driveLabel.rectTransform, Vector2.zero, Vector2.one);
        driveButton.onClick.AddListener(ConfirmCountry);
        driveButton.interactable = false;
    }

    void BuildCarousel(RectTransform parent, Sprite hero)
    {
        var viewport = CreateImage("CountryViewport", parent, Color.clear);
        SelectionUIStyle.SetRect(viewport.rectTransform, new Vector2(.035f, .045f), new Vector2(.965f, .325f), Vector2.zero, Vector2.zero);
        var mask = viewport.gameObject.AddComponent<RectMask2D>();

        var content = CreateUI("CountryFilmstrip", viewport.transform);
        content.anchorMin = new Vector2(0f, 0f);
        content.anchorMax = new Vector2(0f, 1f);
        content.pivot = new Vector2(0f, .5f);
        content.anchoredPosition = Vector2.zero;
        content.sizeDelta = Vector2.zero;
        var row = content.gameObject.AddComponent<HorizontalLayoutGroup>();
        row.padding = new RectOffset(4, 18, 4, 4);
        row.spacing = 16f;
        row.childAlignment = TextAnchor.MiddleLeft;
        row.childControlWidth = true;
        row.childControlHeight = true;
        row.childForceExpandWidth = false;
        row.childForceExpandHeight = true;
        var fitter = content.gameObject.AddComponent<ContentSizeFitter>();
        fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        fitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

        var scroll = viewport.gameObject.AddComponent<ScrollRect>();
        scroll.viewport = viewport.rectTransform;
        scroll.content = content;
        scroll.horizontal = true;
        scroll.vertical = false;
        scroll.movementType = ScrollRect.MovementType.Elastic;
        scroll.inertia = true;
        scroll.decelerationRate = .12f;
        scroll.scrollSensitivity = 32f;

        if (countries.Length == 0)
        {
            var empty = SelectionUIStyle.CreateText("NoCountries", content, "No countries found — return to Main Menu and try again.", 20f,
                UITheme.FontWeight.Medium, UITheme.TextPrimary, TextAlignmentOptions.Left);
            var emptyLayout = empty.gameObject.AddComponent<LayoutElement>();
            emptyLayout.preferredWidth = 720f;
            return;
        }

        for (int i = 0; i < countries.Length; i++)
            CreateCountryCard(content, countries[i], hero, i);
    }

    void CreateCountryCard(RectTransform parent, CountryDefinition country, Sprite hero, int index)
    {
        var card = CreateImage($"Country_{country.countryCode}", parent, UITheme.SurfaceContainer);
        var layout = card.gameObject.AddComponent<LayoutElement>();
        layout.preferredWidth = 310f;
        layout.minWidth = 280f;
        layout.preferredHeight = 210f;
        var button = card.gameObject.AddComponent<Button>();

        var photo = CreateImage("Photo", card.transform, Color.white);
        SelectionUIStyle.Stretch(photo.rectTransform, Vector2.zero, Vector2.one);
        photo.sprite = hero;
        photo.preserveAspect = false;
        photo.color = Color.Lerp(Color.white, UITheme.GetContinentColor(country.countryName), .13f + (index % 3) * .035f);
        photo.raycastTarget = false;

        var wash = CreateImage("PhotoWash", card.transform, new Color(.01f, .02f, .025f, .32f));
        SelectionUIStyle.Stretch(wash.rectTransform, Vector2.zero, Vector2.one);
        var caption = CreateImage("CaptionShade", card.transform, new Color(.01f, .018f, .02f, .70f));
        SelectionUIStyle.SetRect(caption.rectTransform, Vector2.zero, new Vector2(1f, .38f), Vector2.zero, Vector2.zero);

        var code = SelectionUIStyle.CreateText("Code", card.transform, country.countryCode, 12f,
            UITheme.FontWeight.Bold, UITheme.Accent, TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(code.rectTransform, new Vector2(.06f, .29f), new Vector2(.35f, .46f), Vector2.zero, Vector2.zero);
        code.characterSpacing = 1.5f;

        var name = SelectionUIStyle.CreateText("Country", card.transform, country.countryName, 24f,
            UITheme.FontWeight.Bold, UITheme.TextPrimary, TextAlignmentOptions.Left);
        SelectionUIStyle.SetRect(name.rectTransform, new Vector2(.06f, .05f), new Vector2(.94f, .31f), Vector2.zero, Vector2.zero);
        name.enableAutoSizing = true;
        name.fontSizeMin = 17f;
        name.fontSizeMax = 24f;

        var frame = card.gameObject.AddComponent<Outline>();
        frame.effectDistance = new Vector2(3f, -3f);
        frame.effectColor = UITheme.WithAlpha(UITheme.Outline, .75f);
        frame.useGraphicAlpha = true;
        cardFrames[country] = card;

        var captured = country;
        button.onClick.AddListener(() => FeatureCountry(captured));
    }

    void SelectInitialCountry()
    {
        CountryDefinition initial = GameState.Instance?.selectedCountry;
        if (!initial)
            initial = Array.Find(countries, c => c && string.Equals(c.countryCode, "KE", StringComparison.OrdinalIgnoreCase));
        if (!initial && countries.Length > 0) initial = countries[0];
        FeatureCountry(initial);
    }

    void FeatureCountry(CountryDefinition country)
    {
        if (!country) return;
        featuredCountry = country;
        int cityCount = country.cities?.Length ?? 0;
        heroCountry.text = country.countryName;
        heroDetail.text = $"{country.continent}  ·  {cityCount} {(cityCount == 1 ? "city" : "cities")}  ·  Real-world routes";
        driveLabel.text = $"DRIVE IN {country.countryName.ToUpperInvariant()}   ›";
        driveButton.interactable = true;

        foreach (var pair in cardFrames)
        {
            var outline = pair.Value.GetComponent<Outline>();
            bool selected = pair.Key == country;
            outline.effectColor = selected ? UITheme.Accent : UITheme.WithAlpha(UITheme.Outline, .7f);
            outline.effectDistance = selected ? new Vector2(4f, -4f) : new Vector2(2f, -2f);
        }
    }

    void ConfirmCountry()
    {
        if (!featuredCountry) return;
        GameState.Instance?.SelectCountry(featuredCountry);
        CityManager.Instance?.SetActiveCountry(featuredCountry);
        StartCoroutine(Transition(() => SceneLoader.Instance?.LoadCitySelectChecked()));
    }

    IEnumerator Transition(Action load)
    {
        if (canvasGroup) yield return StartCoroutine(UIAnimator.FadeOut(canvasGroup, .25f));
        load?.Invoke();
    }

    static RectTransform CreateUI(string name, Transform parent)
    {
        var obj = new GameObject(name, typeof(RectTransform));
        obj.transform.SetParent(parent, false);
        return obj.GetComponent<RectTransform>();
    }

    static Image CreateImage(string name, Transform parent, Color color)
    {
        var obj = new GameObject(name, typeof(RectTransform), typeof(Image));
        obj.transform.SetParent(parent, false);
        var image = obj.GetComponent<Image>();
        image.color = color;
        return image;
    }
}
