using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FreeDriveUI : MonoBehaviour
{
    [Header("References")]
    public FreeDriveSession session;
    public BusController busController;
    public TransmissionSystem transmissionData;
    public FuelSystem fuelSystem;

    [Header("Speed Display")]
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI speedLabelText;
    public TextMeshProUGUI gearText;

    [Header("Mission Info")]
    public TextMeshProUGUI clockText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI passengerText;

    [Header("Vehicle Info")]
    public TextMeshProUGUI fuelText;
    public Image fuelBar;
    public Image fuelBarBackground;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI timeText;

    [Header("GPS")]
    public TextMeshProUGUI gpsText;

    [Header("Photo Mode")]
    public Button photoModeButton;
    public TextMeshProUGUI photoModeButtonText;
    public Button screenshotButton;
    public TextMeshProUGUI screenshotButtonText;
    public TextMeshProUGUI photoModeStatusText;

    void Start()
    {
        ApplyTheme();
        SetupButtons();
    }

    void ApplyTheme()
    {
        if (speedText)
        {
            speedText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            speedText.color = UITheme.TextPrimary;
        }
        if (speedLabelText)
        {
            speedLabelText.text = "km/h";
            speedLabelText.font = UITheme.GetFont(UITheme.FontWeight.Regular);
            speedLabelText.color = UITheme.TextSecondary;
        }
        if (gearText)
        {
            gearText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
            gearText.color = UITheme.Accent;
        }
        if (clockText)
        {
            clockText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            clockText.color = UITheme.TextPrimary;
        }
        if (scoreText)
        {
            scoreText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
            scoreText.color = UITheme.Success;
        }
        if (passengerText)
        {
            passengerText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
            passengerText.color = UITheme.Secondary;
        }
        if (fuelText)
        {
            fuelText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
            fuelText.color = UITheme.TextSecondary;
        }
        if (fuelBarBackground)
            fuelBarBackground.color = UITheme.SurfaceContainer;
        if (fuelBar)
            fuelBar.color = UITheme.Success;
        if (distanceText)
        {
            distanceText.font = UITheme.GetFont(UITheme.FontWeight.Regular);
            distanceText.color = UITheme.TextSecondary;
        }
        if (timeText)
        {
            timeText.font = UITheme.GetFont(UITheme.FontWeight.Regular);
            timeText.color = UITheme.TextSecondary;
        }
        if (gpsText)
        {
            gpsText.font = UITheme.GetFont(UITheme.FontWeight.Regular);
            gpsText.color = UITheme.TextMuted;
            gpsText.fontSize = 14;
        }
        if (photoModeButtonText)
        {
            photoModeButtonText.text = "PHOTO MODE";
            photoModeButtonText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
            photoModeButtonText.color = UITheme.TextPrimary;
        }
        if (screenshotButtonText)
        {
            screenshotButtonText.text = "SCREENSHOT";
            screenshotButtonText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
            screenshotButtonText.color = UITheme.TextPrimary;
        }
        if (photoModeStatusText)
        {
            photoModeStatusText.font = UITheme.GetFont(UITheme.FontWeight.Regular);
            photoModeStatusText.color = UITheme.TextMuted;
        }
    }

    void SetupButtons()
    {
        if (photoModeButton != null)
            photoModeButton.onClick.AddListener(TogglePhotoMode);
        if (screenshotButton != null)
            screenshotButton.onClick.AddListener(CaptureScreenshot);
    }

    void Update()
    {
        if (busController == null || session == null) return;

        if (speedText)
            speedText.text = $"{busController.currentSpeedKmh:F0}";

        if (gearText)
        {
            int gear = transmissionData != null ? transmissionData.currentGear + 1 : 1;
            gearText.text = HUDManager.FormatGear(gear);
        }

        if (fuelText)
        {
            string suffix = "";
            if (fuelSystem != null)
            {
                if (fuelSystem.IsCriticalActive) suffix = " CRITICAL";
                else if (fuelSystem.IsWarningActive) suffix = " LOW";
            }

            fuelText.text = $"{session.fuelLevel:F0}%{suffix}";
            fuelText.color = fuelSystem != null && fuelSystem.IsWarningActive
                ? UITheme.Error : UITheme.TextSecondary;
        }

        if (fuelBar != null)
        {
            fuelBar.fillAmount = session.fuelLevel / 100f;
            fuelBar.color = fuelSystem != null && fuelSystem.IsCriticalActive
                ? UITheme.Error
                : fuelSystem != null && fuelSystem.IsWarningActive
                    ? UITheme.Tertiary
                    : UITheme.Success;
        }

        if (distanceText)
            distanceText.text = $"ODO {session.distanceDrivenKm:F2} km";

        if (timeText)
            timeText.text = PhotoModeController.Instance != null && PhotoModeController.Instance.IsPhotoModeActive
                ? "PHOTO MODE"
                : string.Empty;

        if (gpsText)
            gpsText.text = $"GPS {session.GetCurrentGPSString()}";

        if (passengerText)
            passengerText.text = string.Empty;

        if (clockText)
            clockText.text = string.Empty;

        if (scoreText)
            scoreText.text = string.Empty;

        if (photoModeStatusText != null)
        {
            if (PhotoModeController.Instance != null && PhotoModeController.Instance.IsPhotoModeActive)
                photoModeStatusText.text = "WASD MOVE • RMB LOOK • F12 SCREENSHOT • F10 EXIT";
            else
                photoModeStatusText.text = "F10 PHOTO MODE • F12 QUICK SHOT";
        }
    }

    void OnEnable()
    {
        if (fuelSystem == null)
            fuelSystem = FindFirstObjectByType<FuelSystem>();
    }

    void TogglePhotoMode()
    {
        if (PhotoModeController.Instance == null)
            return;

        if (PhotoModeController.Instance.IsPhotoModeActive)
            PhotoModeController.Instance.ExitPhotoMode();
        else
            PhotoModeController.Instance.EnterPhotoMode();
    }

    void CaptureScreenshot()
    {
        PhotoModeController.Instance?.CaptureScreenshot();
    }
}
