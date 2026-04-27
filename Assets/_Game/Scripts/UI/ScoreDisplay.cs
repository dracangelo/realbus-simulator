using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup panel;
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI bodyText;

    [Header("Behaviour")]
    public KeyCode toggleKey = KeyCode.Tab;
    public bool startExpanded = true;

    bool isExpanded = true;

    void Start()
    {
        isExpanded = startExpanded;
        ApplyVisibility();
        Refresh();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isExpanded = !isExpanded;
            ApplyVisibility();
        }

        Refresh();
    }

    void ApplyVisibility()
    {
        if (panel == null)
            return;

        panel.alpha = isExpanded ? 1f : 0.35f;
        panel.interactable = isExpanded;
        panel.blocksRaycasts = false;
    }

    void Refresh()
    {
        if (headerText != null)
            headerText.text = isExpanded ? "MISSION SCORE" : "SCORE";

        if (bodyText == null || ScoreTracker.Instance == null)
            return;

        if (!isExpanded)
        {
            bodyText.text = $"{ScoreTracker.Instance.totalScore:F0}%";
            return;
        }

        bodyText.text =
            $"Total {ScoreTracker.Instance.totalScore:F0}%\n" +
            $"Punctuality {ScoreTracker.Instance.punctualityScore:F0}%\n" +
            $"Satisfaction {ScoreTracker.Instance.satisfactionScore:F0}%\n" +
            $"Safety {ScoreTracker.Instance.safetyScore:F0}%\n" +
            $"Efficiency {ScoreTracker.Instance.efficiencyScore:F0}%";
    }
}
