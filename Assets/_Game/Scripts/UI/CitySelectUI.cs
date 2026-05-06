using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CitySelectUI : MonoBehaviour
{
    [Header("UI References")]
    public CanvasGroup canvasGroup;
    public RectTransform contentPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subtitleText;
    public Transform cityListContainer;
    public Button backButton;
    public TextMeshProUGUI backButtonText;
    public Image headerAccentLine;
    public ScrollRect scrollRect;

    void Start()
    {
        UnlockManager.EnsureExists();
        ApplyTheme();
        EnsureCityGridLayout();
        SetupButtons();
        PopulateCityList();
        StartCoroutine(AnimateIn());
    }

    void ApplyTheme()
    {
        var bg = GetComponent<Image>();
        if (bg) bg.color = UITheme.Background;

        if (titleText)
        {
            titleText.text = "SELECT CITY";
            titleText.color = UITheme.TextPrimary;
            titleText.characterSpacing = 6f;
        }

        if (subtitleText)
        {
            subtitleText.text = "where do you want to drive?";
            subtitleText.color = UITheme.TextSecondary;
        }

        if (headerAccentLine)
            headerAccentLine.color = UITheme.Accent;

        if (backButtonText)
        {
            backButtonText.text = "BACK";
            backButtonText.color = UITheme.TextSecondary;
            backButtonText.characterSpacing = 2f;
        }

        var backImg = backButton?.GetComponent<Image>();
        if (backImg) backImg.color = UITheme.WithAlpha(UITheme.Surface, 0.8f);
    }

    void SetupButtons()
    {
        backButton?.onClick.AddListener(OnBack);
    }

    void EnsureCityGridLayout()
    {
        if (!cityListContainer)
            return;

        var listRect = cityListContainer as RectTransform;
        if (!listRect)
        {
            Debug.LogWarning("CitySelectUI: cityListContainer is not a RectTransform. Skipping grid setup.");
            return;
        }

        var vertical = listRect.GetComponent<VerticalLayoutGroup>();
        if (vertical)
            Destroy(vertical);

        var grid = listRect.GetComponent<GridLayoutGroup>();
        if (!grid)
            grid = listRect.gameObject.AddComponent<GridLayoutGroup>();
        if (!grid)
            return;

        // Match CreateCityCard() preferred size so the grid stays 3-across.
        grid.cellSize = new Vector2(195f, 160f);
        grid.spacing = new Vector2(22f, 22f);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 3;
        grid.startAxis = GridLayoutGroup.Axis.Horizontal;
        grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
        grid.childAlignment = TextAnchor.UpperCenter;
        grid.padding = new RectOffset(0, 0, 0, 0);
    }

    void PopulateCityList()
    {
        if (cityListContainer == null)
        {
            Debug.LogError("CitySelectUI: cityListContainer is not assigned.");
            return;
        }

        foreach (Transform child in cityListContainer)
            Destroy(child.gameObject);

        // Get cities from selected country
        var country = CityManager.Instance?.activeCountry;
        if (country == null)
        {
            Debug.LogError("CitySelectUI: No active country!");
            return;
        }

        var cities = country.cities;
        if (cities == null || cities.Length == 0)
        {
            CreateEmptyState($"No cities available for {country.countryName} yet.");
            return;
        }

        // Update subtitle with country name
        if (subtitleText)
        {
            int unlockedCount = 0;
            for (int i = 0; i < cities.Length; i++)
            {
                if (cities[i] != null && (UnlockManager.Instance == null || UnlockManager.Instance.IsCityUnlocked(cities[i])))
                    unlockedCount++;
            }

            subtitleText.text = $"{country.countryName} — {unlockedCount}/{cities.Length} unlocked";
        }

        foreach (var city in cities)
            if (city != null)
                CreateCityCard(city);

        Debug.Log($"CitySelectUI: Created {cities.Length} city cards for {country.countryName}.");
        RefreshListLayout();

        // Layout groups + ContentSizeFitter often need one extra frame in Play Mode.
        StartCoroutine(RebuildNextFrame());
    }

    IEnumerator RebuildNextFrame()
    {
        yield return null;
        RefreshListLayout();
    }

    void CreateEmptyState(string message)
    {
        GameObject msgObj = new GameObject("Text_Empty");
        msgObj.transform.SetParent(cityListContainer, false);
        var tmp = msgObj.AddComponent<TextMeshProUGUI>();
        tmp.text = message;
        tmp.fontSize = 18;
        tmp.font = UITheme.GetFont(UITheme.FontWeight.Regular);
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = UITheme.TextMuted;
        var rect = msgObj.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(0, 120);
    }

    void CreateCityCard(CityDefinition city)
    {
        bool unlocked = UnlockManager.Instance == null || UnlockManager.Instance.IsCityUnlocked(city);
        int requiredRank = UnlockManager.Instance != null ? UnlockManager.Instance.GetRequiredRank(city) : 1;
        string tooltip = UnlockManager.Instance != null ? UnlockManager.Instance.GetCityLockTooltip(city) : "Unlocked";

        // Card root
        GameObject card = new GameObject($"Card_{city.cityCode}");
        card.transform.SetParent(cityListContainer, false);
        card.transform.localScale = Vector3.one;

        var rect = card.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(195, 160);
        var layout = card.AddComponent<LayoutElement>();
        layout.preferredWidth = 195f;
        layout.preferredHeight = 160f;

        var img = card.AddComponent<Image>();
        img.color = unlocked ? UITheme.Surface : UITheme.WithAlpha(UITheme.SurfaceHigh, 0.92f);

        var btn = card.AddComponent<Button>();
        var cols = btn.colors;
        cols.normalColor = img.color;
        cols.highlightedColor = unlocked ? UITheme.SurfaceHigh : img.color;
        cols.pressedColor = unlocked ? UITheme.WithAlpha(UITheme.Surface, 0.6f) : img.color;
        btn.colors = cols;
        btn.interactable = unlocked;

        // Top accent bar (full width, 5px tall at top)
        GameObject accentBar = new GameObject("AccentBar");
        accentBar.transform.SetParent(card.transform, false);
        var barRect = accentBar.AddComponent<RectTransform>();
        barRect.anchorMin = new Vector2(0f, 1f);
        barRect.anchorMax = new Vector2(1f, 1f);
        barRect.sizeDelta = new Vector2(0f, 5f);
        barRect.anchoredPosition = new Vector2(0f, -2.5f);
        var barImg = accentBar.AddComponent<Image>();
        barImg.color = unlocked ? UITheme.GetContinentColor(city.country) : UITheme.Outline;

        // City name — upper center
        GameObject nameObj = new GameObject("Text_CityName");
        nameObj.transform.SetParent(card.transform, false);
        var nameTmp = nameObj.AddComponent<TextMeshProUGUI>();
        nameTmp.text = city.cityName;
        nameTmp.fontSize = 18;
        nameTmp.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        nameTmp.fontStyle = FontStyles.Bold;
        nameTmp.color = unlocked ? UITheme.TextPrimary : UITheme.TextMuted;
        nameTmp.alignment = TextAlignmentOptions.Center;
        nameTmp.overflowMode = TextOverflowModes.Ellipsis;
        nameTmp.textWrappingMode = TextWrappingModes.Normal;
        var nameRect = nameObj.GetComponent<RectTransform>();
        nameRect.anchorMin = new Vector2(0f, 0.45f);
        nameRect.anchorMax = new Vector2(1f, 0.9f);
        nameRect.offsetMin = new Vector2(8f, 0f);
        nameRect.offsetMax = new Vector2(-8f, 0f);

        // Country — lower center
        GameObject countryObj = new GameObject("Text_Country");
        countryObj.transform.SetParent(card.transform, false);
        var countryTmp = countryObj.AddComponent<TextMeshProUGUI>();
        countryTmp.text = city.country;
        countryTmp.fontSize = 13;
        countryTmp.font = UITheme.GetFont(UITheme.FontWeight.Regular);
        countryTmp.color = unlocked ? UITheme.TextSecondary : UITheme.TextMuted;
        countryTmp.alignment = TextAlignmentOptions.Center;
        countryTmp.overflowMode = TextOverflowModes.Ellipsis;
        countryTmp.textWrappingMode = TextWrappingModes.Normal;
        var countryRect = countryObj.GetComponent<RectTransform>();
        countryRect.anchorMin = new Vector2(0f, 0.1f);
        countryRect.anchorMax = new Vector2(1f, 0.45f);
        countryRect.offsetMin = new Vector2(8f, 0f);
        countryRect.offsetMax = new Vector2(-8f, 0f);

        if (!unlocked)
        {
            GameObject lockObj = new GameObject("Text_Lock");
            lockObj.transform.SetParent(card.transform, false);
            var lockTmp = lockObj.AddComponent<TextMeshProUGUI>();
            lockTmp.text = "LOCKED";
            lockTmp.fontSize = 12f;
            lockTmp.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            lockTmp.color = UITheme.Error;
            lockTmp.alignment = TextAlignmentOptions.Center;
            var lockRect = lockObj.GetComponent<RectTransform>();
            lockRect.anchorMin = new Vector2(0f, 0.74f);
            lockRect.anchorMax = new Vector2(1f, 0.92f);
            lockRect.offsetMin = new Vector2(8f, 0f);
            lockRect.offsetMax = new Vector2(-8f, 0f);

            GameObject tooltipObj = new GameObject("Text_Tooltip");
            tooltipObj.transform.SetParent(card.transform, false);
            var tooltipTmp = tooltipObj.AddComponent<TextMeshProUGUI>();
            tooltipTmp.text = $"{tooltip.ToUpper()}  •  LOCKED";
            tooltipTmp.fontSize = 11f;
            tooltipTmp.font = UITheme.GetFont(UITheme.FontWeight.Medium);
            tooltipTmp.color = UITheme.TextSecondary;
            tooltipTmp.alignment = TextAlignmentOptions.Center;
            tooltipTmp.enableWordWrapping = true;
            var tooltipRect = tooltipObj.GetComponent<RectTransform>();
            tooltipRect.anchorMin = new Vector2(0f, 0f);
            tooltipRect.anchorMax = new Vector2(1f, 0.2f);
            tooltipRect.offsetMin = new Vector2(10f, 8f);
            tooltipRect.offsetMax = new Vector2(-10f, -6f);
        }
        else
        {
            float completion = UnlockManager.Instance != null ? UnlockManager.Instance.GetCityCompletion01(city) : 0f;
            GameObject statusObj = new GameObject("Text_Status");
            statusObj.transform.SetParent(card.transform, false);
            var statusTmp = statusObj.AddComponent<TextMeshProUGUI>();
            statusTmp.text = completion >= 0.999f ? "GRAND TOUR READY" : $"{Mathf.RoundToInt(completion * 100f)}% ROUTES COMPLETE";
            statusTmp.fontSize = 11f;
            statusTmp.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            statusTmp.color = completion >= 0.999f ? UITheme.Accent : UITheme.TextSecondary;
            statusTmp.alignment = TextAlignmentOptions.Center;
            var statusRect = statusObj.GetComponent<RectTransform>();
            statusRect.anchorMin = new Vector2(0f, 0f);
            statusRect.anchorMax = new Vector2(1f, 0.2f);
            statusRect.offsetMin = new Vector2(10f, 8f);
            statusRect.offsetMax = new Vector2(-10f, -6f);
        }

        var capturedCity = city;
        btn.onClick.AddListener(() => OnCitySelected(capturedCity));
    }

    void OnCitySelected(CityDefinition city)
    {
        if (city == null || (UnlockManager.Instance != null && !UnlockManager.Instance.IsCityUnlocked(city)))
            return;

        Debug.Log($"City selected: {city.cityName}");
        if (GameState.Instance != null) GameState.Instance.SelectCity(city);
        if (CityManager.Instance != null) CityManager.Instance.SetActiveCity(city);
        StartCoroutine(TransitionToRouteSelect());
    }

    void OnBack()
    {
        StartCoroutine(TransitionToCountrySelect());
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

        
    IEnumerator TransitionToCountrySelect()
    {
        if (canvasGroup)
            yield return StartCoroutine(UIAnimator.FadeOut(canvasGroup, 0.25f));

        SceneLoader.Instance?.LoadCountrySelect();
    }

    IEnumerator TransitionToRouteSelect()
    {
        if (canvasGroup)
            yield return StartCoroutine(UIAnimator.FadeOut(canvasGroup, 0.25f));

        SceneLoader.Instance?.LoadRouteSelect();
    }

    void RefreshListLayout()
    {
        var listRect = cityListContainer as RectTransform;
        if (listRect == null) return;

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(listRect);
        Canvas.ForceUpdateCanvases();
    }
}
