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
    public Image xpFillImage;

    [Header("Buttons")]
    public Button playAgainButton;
    public TextMeshProUGUI playAgainButtonText;
    public Button mainMenuButton;
    public TextMeshProUGUI mainMenuButtonText;

    Coroutine revealRoutine;
    XPBar runtimeXpBar;
    RankUpSequence rankUpSequence;

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

    public void ShowResult(MissionResult result, XPAwardResult awardResult = null)
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
                    ? $"{city.cityName.ToUpper()} — {result.routeName} • {result.missionTypeLabel.ToUpper()}"
                    : $"{result.routeName} • {result.missionTypeLabel.ToUpper()}";
            cityRouteText.color = UITheme.TextSecondary;
            cityRouteText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
        }

        // Total score — large
        if (totalScoreText)
        {
            totalScoreText.text = "0%";
            totalScoreText.color = GetScoreColor(result.totalScore);
            totalScoreText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }

        // Star rating
        if (starRatingText)
        {
            starRatingText.text = "☆☆☆☆☆";
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
                : $"{result.totalDistanceKm:F1} km • V{result.totalViolations} • E{result.dynamicEventsTriggered}";
            distanceText.color = UITheme.TextPrimary;
            distanceText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }
        if (xpText)
        {
            xpText.text = result.isShiftSummary
                ? $"Incidents {result.shiftIncidentCount}  •  Bay {(result.returnedToCorrectBay ? "OK" : "MISSED")}"
                : $"{(result.scenarioObjectivePassed ? "OBJ OK" : "OBJ MISSED")}  •  {result.scenarioObjectiveStatus}  •  +{result.xpEarned} XP  •  {result.violationSummary}  •  Rep {result.driverReputationRating:F0}";
            xpText.color = UITheme.TertiaryDim;
            xpText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }

        EnsureProgressUi();
        if (runtimeXpBar != null)
        {
            if (awardResult != null)
                runtimeXpBar.AnimateAward(awardResult);
            else
                runtimeXpBar.RefreshCurrent();
        }

        if (revealRoutine != null)
            StopCoroutine(revealRoutine);
        revealRoutine = StartCoroutine(AnimateReveal(result));

        if (awardResult != null && awardResult.RankedUp && rankUpSequence != null)
            StartCoroutine(rankUpSequence.PlaySequence(awardResult.rankUps));
    }

    void EnsureProgressUi()
    {
        if (resultPanel == null || runtimeXpBar != null)
            return;

        var root = new GameObject("XPProgressRoot", typeof(RectTransform));
        root.transform.SetParent(resultPanel.transform, false);
        var rootRect = root.GetComponent<RectTransform>();
        rootRect.anchorMin = new Vector2(0.08f, 0.02f);
        rootRect.anchorMax = new Vector2(0.92f, 0.16f);
        rootRect.offsetMin = Vector2.zero;
        rootRect.offsetMax = Vector2.zero;

        runtimeXpBar = XPBar.CreateRuntimeBar(root.transform, "MissionResultXPBar");
        var xpRect = runtimeXpBar.GetComponent<RectTransform>();
        xpRect.anchorMin = Vector2.zero;
        xpRect.anchorMax = Vector2.one;
        xpRect.offsetMin = Vector2.zero;
        xpRect.offsetMax = Vector2.zero;

        rankUpSequence = gameObject.GetComponent<RankUpSequence>();
        if (rankUpSequence == null)
            rankUpSequence = gameObject.AddComponent<RankUpSequence>();
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

    IEnumerator AnimateReveal(MissionResult result)
    {
        float duration = 1.1f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float eased = 1f - Mathf.Pow(1f - t, 3f);

            if (totalScoreText != null)
                totalScoreText.text = $"{Mathf.RoundToInt(Mathf.Lerp(0f, result.totalScore, eased))}%";

            if (xpFillImage != null)
                xpFillImage.fillAmount = eased;

            if (starRatingText != null)
            {
                int starsVisible = Mathf.Clamp(Mathf.FloorToInt(eased * (result.starRating + 0.25f)), 0, result.starRating);
                starRatingText.text = new string('★', starsVisible) + new string('☆', Mathf.Max(0, 5 - starsVisible));
            }

            yield return null;
        }

        if (totalScoreText != null)
            totalScoreText.text = $"{result.totalScore:F0}%";
        if (starRatingText != null)
            starRatingText.text = new string('★', result.starRating) + new string('☆', Mathf.Max(0, 5 - result.starRating));
        if (xpFillImage != null)
            xpFillImage.fillAmount = 1f;

        revealRoutine = null;
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
