using UnityEngine;

[DefaultExecutionOrder(-50)]
[DisallowMultipleComponent]
public class DrivingAssistSystem : MonoBehaviour
{
    public BusController bus;
    public float assistedSpeedLimitKmh = 50f;
    public float dockingApproachMeters = 14f;
    MissionDrivingMonitor drivingMonitor;

    void Awake() { if (bus == null) bus = GetComponent<BusController>(); drivingMonitor = GetComponent<MissionDrivingMonitor>(); }
    void OnDisable() { ResetAssists(); }

    void FixedUpdate()
    {
        if (bus == null) return;
        RealBusSettings settings = SettingsManager.EnsureExists().Current;
        if (!settings.drivingAssists) { ResetAssists(); return; }

        if (drivingMonitor == null) drivingMonitor = GetComponent<MissionDrivingMonitor>();
        float activeLimit = MissionManager.Instance != null && MissionManager.Instance.routeActive
            ? (drivingMonitor != null ? drivingMonitor.CurrentSpeedLimitKmh : assistedSpeedLimitKmh)
            : 80f;
        float speedOver = bus.currentSpeedKmh - activeLimit;
        bus.AssistThrottleLimit = speedOver <= 0f ? 1f : Mathf.Clamp01(1f - speedOver / 5f);
        bus.AssistBrakeFloor = speedOver > 7f ? Mathf.InverseLerp(7f, 18f, speedOver) * 0.16f : 0f;

        MissionManager mission = MissionManager.Instance;
        if (mission != null && mission.routeActive && mission.distanceToNextStop < dockingApproachMeters)
        {
            float safeApproachSpeed = Mathf.Lerp(1.2f, 7f, mission.distanceToNextStop / dockingApproachMeters);
            float excess = bus.currentSpeedKmh - safeApproachSpeed;
            if (excess > 0f)
            {
                bus.AssistThrottleLimit = 0f;
                bus.AssistBrakeFloor = Mathf.Max(bus.AssistBrakeFloor, Mathf.Clamp01(excess / 8f) * 0.28f);
            }
        }
    }

    void ResetAssists()
    {
        if (bus == null) return;
        bus.AssistThrottleLimit = 1f; bus.AssistBrakeFloor = 0f;
    }
}
