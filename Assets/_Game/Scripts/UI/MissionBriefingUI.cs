using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MissionBriefingUI : MonoBehaviour
{
    public static MissionBriefingUI Instance { get; private set; }

    [Header("Panel")]
    public CanvasGroup briefingCanvasGroup;
    public GameObject briefingPanel;
    public Image backgroundOverlay;

    [Header("Header")]
    public TextMeshProUGUI cityNameText;
    public TextMeshProUGUI routeNameText;
    public Image accentLine;

    [Header("Route Stats")]
    public TextMeshProUGUI stopsCountText;
    public TextMeshProUGUI fareText;
    public TextMeshProUGUI departureText;
    public TextMeshProUGUI durationText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI targetText;

    [Header("Stops List")]
    public TextMeshProUGUI stopsListText;

    [Header("Buttons")]
    public Button startButton;
    public TextMeshProUGUI startButtonText;
    public Button cancelButton;
    public TextMeshProUGUI cancelButtonText;

    [Header("Mission Data — fallback if GameState empty")]
    public MissionData missionData;

    private BusRoute activeRoute;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        Debug.Log($"BriefingUI Start — MissionManager: {MissionManager.Instance != null}");
        Debug.Log($"BriefingUI Start — MapSystem active: {GameObject.Find("MapSystem") != null}");
    
        ApplyTheme();
        SetupButtons();

        // Get route from GameState or fallback to MissionData
        if (GameState.Instance?.selectedRoute != null)
            activeRoute = GameState.Instance.selectedRoute;
        else if (missionData?.route != null)
            activeRoute = missionData.route;

        ShowBriefing();
    }

    void ApplyTheme()
    {
        if (backgroundOverlay)
            backgroundOverlay.color = UITheme.WithAlpha(UITheme.Background, 0.95f);

        if (accentLine)
            accentLine.color = UITheme.Accent;

        // Start button
        if (startButton)
        {
            var img = startButton.GetComponent<Image>();
            if (img) img.color = UITheme.Accent;
            var cols = startButton.colors;
            cols.normalColor = UITheme.Accent;
            cols.highlightedColor = UITheme.AccentDim;
            cols.pressedColor = UITheme.WithAlpha(UITheme.Accent, 0.7f);
            startButton.colors = cols;
        }

        if (startButtonText)
        {
            startButtonText.text = "START MISSION";
            startButtonText.color = UITheme.Background;
            startButtonText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            startButtonText.characterSpacing = 3f;
        }

        // Cancel button
        if (cancelButton)
        {
            var img = cancelButton.GetComponent<Image>();
            if (img) img.color = UITheme.SurfaceContainer;
            var cols = cancelButton.colors;
            cols.normalColor = UITheme.SurfaceContainer;
            cols.highlightedColor = UITheme.SurfaceHigh;
            cancelButton.colors = cols;
        }

        if (cancelButtonText)
        {
            cancelButtonText.text = "CANCEL";
            cancelButtonText.color = UITheme.TextSecondary;
            cancelButtonText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
            cancelButtonText.characterSpacing = 2f;
        }

        // Stat labels color
        ApplyStatTheme(stopsCountText, "— stops");
        ApplyStatTheme(fareText, "KES —");
        ApplyStatTheme(departureText, "——:——");
        ApplyStatTheme(durationText, "— min");
    }

    void ApplyStatTheme(TextMeshProUGUI tmp, string placeholder)
    {
        if (tmp == null) return;
        tmp.text = placeholder;
        tmp.color = UITheme.TextPrimary;
        tmp.font = UITheme.GetFont(UITheme.FontWeight.Bold);
    }

    void SetupButtons()
    {
        startButton?.onClick.AddListener(OnStartMission);
        cancelButton?.onClick.AddListener(OnCancel);
    }

    public void ShowBriefing()
    {
        if (briefingPanel) briefingPanel.SetActive(true);
        if (briefingCanvasGroup)
            StartCoroutine(UIAnimator.FadeIn(briefingCanvasGroup, 0.3f));

        if (activeRoute == null)
        {
            if (routeNameText) routeNameText.text = "No route selected";
            return;
        }

        // City name
        var city = GameState.Instance?.selectedCity ?? CityManager.Instance?.activeCity;
        if (cityNameText)
        {
            cityNameText.text = city != null
                ? $"{city.cityName}, {city.country}".ToUpper()
                : "UNKNOWN CITY";
            cityNameText.color = UITheme.TextSecondary;
            cityNameText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
            cityNameText.characterSpacing = 3f;
        }

        // Route name
        if (routeNameText)
        {
            routeNameText.text = activeRoute.routeName;
            routeNameText.color = UITheme.TextPrimary;
            routeNameText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }

        // Stats
        if (stopsCountText)
        {
            stopsCountText.text = $"{activeRoute.stops.Length}";
            stopsCountText.color = UITheme.Accent;
            stopsCountText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }

        if (fareText)
        {
            fareText.text = $"KES {activeRoute.baseFare:F0}";
            fareText.color = UITheme.Success;
            fareText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }

        // Departure from MissionData if available
        if (departureText)
        {
            if (missionData != null)
            {
                int h = Mathf.FloorToInt(missionData.scheduledDepartureTime / 60f);
                int m = Mathf.FloorToInt(missionData.scheduledDepartureTime % 60f);
                string ampm = h >= 12 ? "PM" : "AM";
                int h12 = h % 12; if (h12 == 0) h12 = 12;
                departureText.text = $"{h12:00}:{m:00} {ampm}";
            }
            else
                departureText.text = "08:00 AM";

            departureText.color = UITheme.TextPrimary;
            departureText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }

        if (durationText)
        {
            durationText.text = missionData != null
                ? $"{missionData.targetDurationMinutes:F0} min"
                : $"~{activeRoute.stops.Length * 5} min";
            durationText.color = UITheme.TextPrimary;
            durationText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }

        if (difficultyText)
        {
            int difficulty = missionData != null ? Mathf.Clamp(missionData.starRating, 1, 5) : Mathf.Clamp(activeRoute.difficulty, 1, 5);
            difficultyText.text = new string('★', difficulty) + new string('☆', Mathf.Max(0, 5 - difficulty));
            difficultyText.color = UITheme.TertiaryDim;
            difficultyText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }

        if (targetText)
        {
            targetText.text = missionData != null
                ? $"Target {missionData.minPassengersTarget}+ pax"
                : $"Target {Mathf.Max(12, activeRoute.GetStopCount() * 4)}+ pax";
            targetText.color = UITheme.TextSecondary;
            targetText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
        }

        // Stops list
        if (stopsListText)
        {
            string stops = "";
            for (int i = 0; i < activeRoute.stops.Length; i++)
            {
                bool isFirst = i == 0;
                bool isLast = i == activeRoute.stops.Length - 1;
                string prefix = isFirst ? "◉  " : isLast ? "◉  " : "○  ";
                stops += $"{prefix}{activeRoute.stops[i].stopName}\n";
            }
            stopsListText.text = stops.TrimEnd();
            stopsListText.color = UITheme.TextSecondary;
            stopsListText.font = UITheme.GetFont(UITheme.FontWeight.Regular);
        }
    }

    void OnStartMission()
    {
        if (briefingPanel) briefingPanel.SetActive(false);
        LaunchMission();
    }

    IEnumerator StartAfterFade()
    {
        if (startButton) startButton.interactable = false;
        yield return StartCoroutine(UIAnimator.FadeOut(briefingCanvasGroup, 0.3f));
        LaunchMission();
    }

    void LaunchMission()
    {
        var route = activeRoute ?? missionData?.route;
        if (route == null)
        {
            Debug.LogError("MissionBriefingUI: No route to launch!");
            return;
        }

        var mm = MissionManager.Instance;
        if (mm == null)
        {
            Debug.LogError("MissionBriefingUI: MissionManager is null!");
            return;
        }

        mm.StartRoute(route);
        Debug.Log($"Mission started: {route.routeName}");
    }

    void OnCancel()
    {
        StartCoroutine(CancelAndGoBack());
    }

    IEnumerator CancelAndGoBack()
    {
        if (briefingCanvasGroup)
            yield return StartCoroutine(UIAnimator.FadeOut(briefingCanvasGroup, 0.25f));

        if (briefingPanel) briefingPanel.SetActive(false);
        SceneLoader.Instance?.LoadRouteSelect();
    }
}
