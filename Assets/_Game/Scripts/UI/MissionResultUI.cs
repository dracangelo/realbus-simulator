using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MissionResultUI : MonoBehaviour
{
    public static MissionResultUI Instance { get; private set; }

    [Header("Panel")]
    public CanvasGroup resultCanvasGroup;
    public GameObject resultPanel;
    public Image backgroundOverlay;

    [Header("Header")]
    public TextMeshProUGUI cityRouteText;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI starRatingText;
    public Image accentLine;

    [Header("Score Breakdown")]
    public TextMeshProUGUI punctualityText;
    public TextMeshProUGUI satisfactionText;
    public TextMeshProUGUI safetyText;
    public TextMeshProUGUI efficiencyText;

    [Header("Trip Summary")]
    public TextMeshProUGUI passengersText;
    public TextMeshProUGUI fareText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI xpText;

    [Header("Buttons")]
    public Button playAgainButton;
    public TextMeshProUGUI playAgainButtonText;
    public Button mainMenuButton;
    public TextMeshProUGUI mainMenuButtonText;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        // Hide on start
        if (resultPanel) resultPanel.SetActive(false);
    }

    void Start()
    {
        ApplyTheme();
        SetupButtons();
    }

    void ApplyTheme()
    {
        if (backgroundOverlay)
            backgroundOverlay.color = UITheme.WithAlpha(UITheme.Background, 0.97f);

        if (accentLine)
            accentLine.color = UITheme.Accent;

        // Play Again button
        if (playAgainButton)
        {
            var img = playAgainButton.GetComponent<Image>();
            if (img) img.color = UITheme.Accent;
            var cols = playAgainButton.colors;
            cols.normalColor = UITheme.Accent;
            cols.highlightedColor = UITheme.AccentDim;
            playAgainButton.colors = cols;
        }
        if (playAgainButtonText)
        {
            playAgainButtonText.text = "PLAY AGAIN";
            playAgainButtonText.color = UITheme.Background;
            playAgainButtonText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            playAgainButtonText.characterSpacing = 3f;
        }

        // Main Menu button
        if (mainMenuButton)
        {
            var img = mainMenuButton.GetComponent<Image>();
            if (img) img.color = UITheme.SurfaceContainer;
            var cols = mainMenuButton.colors;
            cols.normalColor = UITheme.SurfaceContainer;
            cols.highlightedColor = UITheme.SurfaceHigh;
            mainMenuButton.colors = cols;
        }
        if (mainMenuButtonText)
        {
            mainMenuButtonText.text = "MAIN MENU";
            mainMenuButtonText.color = UITheme.TextSecondary;
            mainMenuButtonText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
            mainMenuButtonText.characterSpacing = 2f;
        }
    }

    void SetupButtons()
    {
        playAgainButton?.onClick.AddListener(OnPlayAgain);
        mainMenuButton?.onClick.AddListener(OnMainMenu);
    }

    public void ShowResult(MissionResult result)
    {
        if (resultPanel) resultPanel.SetActive(true);
        if (resultCanvasGroup)
            StartCoroutine(UIAnimator.FadeIn(resultCanvasGroup, 0.5f));

        // Header
        if (cityRouteText)
        {
            var city = GameState.Instance?.selectedCity ?? CityManager.Instance?.activeCity;
            cityRouteText.text = result.isShiftSummary
                ? $"SHIFT SUMMARY — {result.routeName}"
                : city != null
                    ? $"{city.cityName.ToUpper()} — {result.routeName}"
                    : result.routeName;
            cityRouteText.color = UITheme.TextSecondary;
            cityRouteText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
        }

        // Total score — large
        if (totalScoreText)
        {
            totalScoreText.text = $"{result.totalScore:F0}%";
            totalScoreText.color = GetScoreColor(result.totalScore);
            totalScoreText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }

        // Star rating
        if (starRatingText)
        {
            string stars = "";
            for (int i = 0; i < 5; i++)
                stars += i < result.starRating ? "★" : "☆";
            starRatingText.text = stars;
            starRatingText.color = UITheme.TertiaryDim;
            starRatingText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }

        // Score breakdown
        SetBreakdown(punctualityText, "PUNCTUALITY",
            result.punctualityScore, UITheme.Accent);
        SetBreakdown(satisfactionText, "SATISFACTION",
            result.satisfactionScore, UITheme.Secondary);
        SetBreakdown(safetyText, "SAFETY",
            result.safetyScore, UITheme.Success);
        SetBreakdown(efficiencyText, "EFFICIENCY",
            result.efficiencyScore, UITheme.Tertiary);

        // Trip summary
        if (passengersText)
        {
            passengersText.text = result.isShiftSummary
                ? $"{result.shiftRoutesCompleted}/{result.shiftPlannedRoutes}"
                : $"{result.totalPassengers}";
            passengersText.color = UITheme.TextPrimary;
            passengersText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }
        if (fareText)
        {
            fareText.text = result.isShiftSummary
                ? $"REP {result.driverReputationRating:F0}"
                : $"KES {result.netEarningsKES:N0}";
            fareText.color = UITheme.Success;
            fareText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }
        if (distanceText)
        {
            distanceText.text = result.isShiftSummary
                ? $"{result.shiftFuelConsumedLitres:F1} L"
                : $"{result.totalDistanceKm:F1} km • V{result.totalViolations}";
            distanceText.color = UITheme.TextPrimary;
            distanceText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }
        if (xpText)
        {
            xpText.text = result.isShiftSummary
                ? $"Incidents {result.shiftIncidentCount}  •  Bay {(result.returnedToCorrectBay ? "OK" : "MISSED")}"
                : $"+{result.xpEarned} XP  •  {result.violationSummary}  •  Rep {result.driverReputationRating:F0}";
            xpText.color = UITheme.TertiaryDim;
            xpText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }
    }

    void SetBreakdown(TextMeshProUGUI tmp, string label,
        float score, Color color)
    {
        if (tmp == null) return;
        tmp.text = $"{label}\n<size=24><color=#{ColorUtility.ToHtmlStringRGB(color)}>" +
                   $"{score:F0}%</color></size>";
        tmp.color = UITheme.TextSecondary;
        tmp.font = UITheme.GetFont(UITheme.FontWeight.Regular);
    }

    Color GetScoreColor(float score)
    {
        if (score >= 80f) return UITheme.Success;
        if (score >= 60f) return UITheme.Accent;
        return UITheme.Error;
    }

    void OnPlayAgain()
    {
        StartCoroutine(TransitionToRouteSelect());
    }

    void OnMainMenu()
    {
        StartCoroutine(TransitionToMainMenu());
    }

    IEnumerator TransitionToMainMenu()
    {
        if (resultCanvasGroup)
            yield return StartCoroutine(UIAnimator.FadeOut(resultCanvasGroup, 0.3f));

        if (resultPanel) resultPanel.SetActive(false);

        SceneLoader.Instance?.LoadMainMenu();
    }

    IEnumerator TransitionToRouteSelect()
    {
        if (resultCanvasGroup)
            yield return StartCoroutine(UIAnimator.FadeOut(resultCanvasGroup, 0.3f));

        if (resultPanel) resultPanel.SetActive(false);

        SceneLoader.Instance?.LoadRouteSelect();
    }
}
