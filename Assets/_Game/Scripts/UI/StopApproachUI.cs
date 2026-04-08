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
    public Image panelBackground;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        HideApproach();
    }

    public void ShowApproach(string stopName, float distance)
    {
        approachPanel.SetActive(true);
        stopNameText.text = stopName;
        distanceText.text = $"{distance:F0}m";
        statusText.text = "APPROACHING";
        if (panelBackground != null)
            panelBackground.color = new Color(1f, 0.5f, 0f, 0.85f); // orange
    }

    public void ShowDocked(string stopName)
    {
        approachPanel.SetActive(true);
        stopNameText.text = stopName;
        distanceText.text = "STOP";
        statusText.text = "DOCKED";
        if (panelBackground != null)
            panelBackground.color = new Color(0f, 0.8f, 0f, 0.85f); // green
    }

    public void HideApproach()
    {
        if (approachPanel != null)
            approachPanel.SetActive(false);
    }

    // Update distance live while approaching
    public void UpdateDistance(float distance)
    {
        if (approachPanel != null && approachPanel.activeSelf)
            distanceText.text = $"{distance:F0}m";
    }
}