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

    void Start()
    {
        ApplyTheme();
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
    }

    void Update()
    {
        if (busController == null || session == null) return;

        if (speedText)
            speedText.text = $"{busController.currentSpeedKmh:F0}";

        if (gearText)
        {
            int gear = transmissionData != null ? transmissionData.currentGear + 1 : 1;
            gearText.text = $"G{gear}";
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
            distanceText.text = $"{session.distanceDrivenKm:F2} km";

        if (timeText)
            timeText.text = session.GetFormattedTime();

        if (gpsText)
        {
            string eventSuffix = DynamicEventSystem.Instance != null && DynamicEventSystem.Instance.ActiveEvent != null
                ? $" • {DynamicEventSystem.Instance.ActiveEvent.title}"
                : "";
            string diversionSuffix = MissionManager.Instance != null && MissionManager.Instance.HasActiveDiversion
                ? " • DETOUR"
                : "";
            gpsText.text = $"{session.GetCurrentGPSString()}{eventSuffix}{diversionSuffix}";
        }

        if (passengerText && PassengerManager.Instance != null)
        {
            string stopRequestSuffix = PassengerManager.Instance.stopRequestActive
                ? PassengerManager.Instance.stopRequestAcknowledged ? " • STOP ACK" : " • STOP REQ"
                : "";
            passengerText.text =
                $"{PassengerManager.Instance.currentPassengers} PAX{stopRequestSuffix}";
        }

        if (clockText && ScheduleManager.Instance != null)
            clockText.text = ScheduleManager.Instance.currentTimeString;

        if (scoreText && ScoreTracker.Instance != null)
        {
            string violationSuffix = ExtendedTrafficViolationSystem.Instance != null && ExtendedTrafficViolationSystem.Instance.TotalViolationCount > 0
                ? $" • V{ExtendedTrafficViolationSystem.Instance.TotalViolationCount}"
                : "";
            string eventSuffix = DynamicEventSystem.Instance != null && DynamicEventSystem.Instance.ActiveEvent != null
                ? $" • {DynamicEventSystem.Instance.ActiveEvent.title}"
                : "";
            scoreText.text = $"{ScoreTracker.Instance.totalScore:F0}%{violationSuffix}{eventSuffix}";
        }
    }

    void OnEnable()
    {
        if (fuelSystem == null)
            fuelSystem = FindFirstObjectByType<FuelSystem>();
    }
}
