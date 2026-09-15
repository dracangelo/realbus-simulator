using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StopApproachUI : MonoBehaviour
{
    public static StopApproachUI Instance { get; private set; }

    [Header("UI Elements")]
    public GameObject approachPanel;
    public TextMeshProUGUI stopNameText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI guidanceText;
    public Image panelBackground;
    public RectTransform directionArrow;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        HideApproach();
    }

    public void ShowApproach(string stopName, float distance, float signedAngleDeg = 0f)
    {
        if (approachPanel != null)
            approachPanel.SetActive(true);
        if (stopNameText != null)
            stopNameText.text = stopName;
        if (distanceText != null)
            distanceText.text = $"{distance:F0}m";
        if (statusText != null)
            statusText.text = "APPROACHING";
        SetGuidanceHint(signedAngleDeg);
        if (panelBackground != null)
            panelBackground.color = new Color(1f, 0.5f, 0f, 0.85f); // orange
    }

    public void ShowDocked(string stopName)
    {
        ShowDocked(stopName, 100f, "GOLD");
    }

    public void ShowDocked(string stopName, float score, string grade)
    {
        if (approachPanel != null)
            approachPanel.SetActive(true);
        if (stopNameText != null)
            stopNameText.text = stopName;
        if (distanceText != null)
            distanceText.text = $"{score:0}%";
        if (statusText != null)
            statusText.text = "DOCKED";
        if (guidanceText != null)
            guidanceText.text = grade + " DOCKING";
        if (directionArrow != null)
            directionArrow.localRotation = Quaternion.identity;
        if (panelBackground != null)
            panelBackground.color = new Color(0f, 0.8f, 0f, 0.85f); // green
    }

    public void ShowDockingAlignment(string stopName, float kerb, float sign, float heading, float speed,
        float score, float kerbTolerance, float signTolerance, float headingTolerance, float speedTolerance)
    {
        if (approachPanel != null) approachPanel.SetActive(true);
        if (stopNameText != null) stopNameText.text = stopName;
        if (distanceText != null) distanceText.text = $"{score:0}%";
        if (statusText != null) statusText.text = "ALIGNING";
        if (guidanceText != null)
        {
            if (speed > speedTolerance) guidanceText.text = $"BRAKE TO {speedTolerance:0.0} KM/H";
            else if (kerb > kerbTolerance) guidanceText.text = $"KERB {kerb:0.00} M — MOVE CLOSER";
            else if (sign > signTolerance) guidanceText.text = $"STOP MARK {sign:0.0} M";
            else if (heading > headingTolerance) guidanceText.text = $"STRAIGHTEN {heading:0}°";
            else guidanceText.text = "HOLD POSITION • OPEN DOORS";
        }
        if (directionArrow != null) directionArrow.localRotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(-heading, -45f, 45f));
        if (panelBackground != null) panelBackground.color = score >= 85f ? UITheme.Success : score >= 60f ? UITheme.TertiaryDim : UITheme.Error;
    }

    public void ShowWaiting(string stopName, float waitSeconds)
    {
        if (approachPanel != null)
            approachPanel.SetActive(true);
        if (stopNameText != null)
            stopNameText.text = stopName;
        if (distanceText != null)
            distanceText.text = $"{Mathf.CeilToInt(waitSeconds)}s";
        if (statusText != null)
            statusText.text = "WAIT FOR SCHEDULE";
        if (guidanceText != null)
            guidanceText.text = "HOLD POSITION";
        if (panelBackground != null)
            panelBackground.color = new Color(0.15f, 0.45f, 1f, 0.88f);
    }

    public void HideApproach()
    {
        if (approachPanel != null)
            approachPanel.SetActive(false);
    }

    // Update distance live while approaching
    public void UpdateDistance(float distance)
    {
        if (approachPanel != null && approachPanel.activeSelf && distanceText != null)
            distanceText.text = $"{distance:F0}m";
    }

    public void UpdateGuidance(float distance, float signedAngleDeg)
    {
        UpdateDistance(distance);
        SetGuidanceHint(signedAngleDeg);
    }

    void SetGuidanceHint(float signedAngleDeg)
    {
        if (guidanceText != null)
        {
            if (signedAngleDeg > 8f)
                guidanceText.text = "STEER RIGHT";
            else if (signedAngleDeg < -8f)
                guidanceText.text = "STEER LEFT";
            else
                guidanceText.text = "STRAIGHT AHEAD";
        }

        if (directionArrow != null)
            directionArrow.localRotation = Quaternion.Euler(0f, 0f, -signedAngleDeg);
    }
}
