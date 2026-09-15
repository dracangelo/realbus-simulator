using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OfflineMapsUI : MonoBehaviour
{
    [Header("UI")]
    public TMP_Dropdown cityDropdown;
    public Button downloadButton;
    public Button cancelButton;
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
        if (cancelButton != null) cancelButton.onClick.AddListener(() => predownloader?.CancelDownload());

        if (downloadButton != null)
            downloadButton.onClick.AddListener(OnDownloadClicked);

        if (predownloader != null)
        {
            predownloader.OnComplete += OnDownloadComplete;
            predownloader.OnProgress          += OnProgress;       // was OnProgress01
            predownloader.OnEstimatedMbChanged += OnEstimatedSize; // was OnEstimatedMb
        }

        RefreshSelectedCityStatus();
    }

    void OnDestroy()
    {
        if (predownloader != null)
        {
            predownloader.OnComplete -= OnDownloadComplete;
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
        cityDropdown.onValueChanged.AddListener(_ => RefreshSelectedCityStatus());
        RefreshSelectedCityStatus();
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

        CityPredownloader.CityDownloadStatus status = predownloader.GetStatus(city);
        float progress = status != null && status.total > 0 ? status.cached / (float)status.total : 0f;
        float remainingMb = status != null ? status.estimatedMb * (1f - progress) : 0f;
        long freeBytes = CityPredownloader.GetAvailableStorageBytes();
        long requiredBytes = (long)((remainingMb + 32f) * 1024f * 1024f);
        if (freeBytes >= 0 && freeBytes < requiredBytes)
        {
            if (progressLabel != null) progressLabel.text = $"Need ~{remainingMb + 32f:0} MB free";
            SubtitleManager.EnsureExists().Show("Offline maps", "Not enough storage for this city.", 3f);
            return;
        }

        predownloader.StartDownloadForCity(city);
        SetProgress(progress);
    }

    void OnProgress(float p01)
    {
        SetProgress(p01);
    }

    void OnDownloadComplete(bool cancelled)
    {
        if (progressLabel != null)
            progressLabel.text = cancelled ? "Cancelled" : predownloader.failedTiles > 0
                ? $"Incomplete: {predownloader.failedTiles} tiles unavailable" : "Ready offline";
        RefreshSelectedCityStatus();
    }

    void RefreshSelectedCityStatus()
    {
        if (predownloader == null || cityDropdown == null || CityManager.Instance == null || CityManager.Instance.allCities == null || CityManager.Instance.allCities.Length == 0) return;
        int index = Mathf.Clamp(cityDropdown.value, 0, CityManager.Instance.allCities.Length - 1);
        CityDefinition city = CityManager.Instance.allCities[index]; if (city == null) return;
        CityPredownloader.CityDownloadStatus status = predownloader.GetStatus(city);
        if (status == null) return;
        float progress = status.total > 0 ? status.cached / (float)status.total : 0f;
        SetProgress(progress);
        long freeBytes = CityPredownloader.GetAvailableStorageBytes();
        string free = freeBytes >= 0 ? $" • {(freeBytes / (1024f * 1024f)):N0} MB free" : string.Empty;
        if (sizeLabel != null) sizeLabel.text = $"~{status.estimatedMb:0.0} MB • {status.cached}/{status.total} tiles{free}";
        TextMeshProUGUI buttonLabel = downloadButton != null ? downloadButton.GetComponentInChildren<TextMeshProUGUI>() : null;
        if (buttonLabel != null) buttonLabel.text = status.complete ? "VERIFY OFFLINE MAP" : status.cached > 0 ? "RESUME DOWNLOAD" : "DOWNLOAD";
        if (progressLabel != null) progressLabel.text = status.complete ? "Ready offline" : status.cached > 0 ? $"Paused • {progress * 100f:0}% cached" : "Not downloaded";
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
