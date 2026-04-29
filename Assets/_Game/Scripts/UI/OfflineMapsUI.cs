using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OfflineMapsUI : MonoBehaviour
{
    [Header("UI")]
    public TMP_Dropdown cityDropdown;
    public Button downloadButton;
    public Slider progressBar;
    public TextMeshProUGUI progressLabel;
    public TextMeshProUGUI sizeLabel;

    [Header("Services")]
    public CityPredownloader predownloader;

    void Start()
    {
        if (predownloader == null)
            predownloader = FindFirstObjectByType<CityPredownloader>();

        PopulateCities();

        if (downloadButton != null)
            downloadButton.onClick.AddListener(OnDownloadClicked);

        if (predownloader != null)
        {
            predownloader.OnProgress          += OnProgress;       // was OnProgress01
            predownloader.OnEstimatedMbChanged += OnEstimatedSize; // was OnEstimatedMb
        }

        SetProgress(0f);
    }

    void OnDestroy()
    {
        if (predownloader != null)
        {
            predownloader.OnProgress          -= OnProgress;
            predownloader.OnEstimatedMbChanged -= OnEstimatedSize;
        }
    }

    void PopulateCities()
    {
        if (cityDropdown == null) return;
        cityDropdown.ClearOptions();

        var cm = CityManager.Instance;
        if (cm == null || cm.allCities == null)
        {
            cityDropdown.AddOptions(new System.Collections.Generic.List<string> { "No cities" });
            return;
        }

        var options = new System.Collections.Generic.List<string>();
        for (int i = 0; i < cm.allCities.Length; i++)
            options.Add(cm.allCities[i] != null ? cm.allCities[i].cityName : "Unknown");
        cityDropdown.AddOptions(options);
    }

    void OnDownloadClicked()
    {
        if (predownloader == null) return;
        var cm = CityManager.Instance;
        if (cm == null || cm.allCities == null || cm.allCities.Length == 0) return;
        if (cityDropdown == null) return;

        int idx = Mathf.Clamp(cityDropdown.value, 0, cm.allCities.Length - 1);
        var city = cm.allCities[idx];
        if (city == null) return;

        predownloader.StartDownloadForCity(city);
        SetProgress(0f);
    }

    void OnProgress(float p01)
    {
        SetProgress(p01);
    }

    void OnEstimatedSize(float mb)
    {
        if (sizeLabel != null)
            sizeLabel.text = $"~{mb:0.0} MB";
    }

    void SetProgress(float p01)
    {
        if (progressBar != null)
            progressBar.value = p01;
        if (progressLabel != null)
            progressLabel.text = $"{p01 * 100f:0}%";
    }
}