using UnityEngine;

[DisallowMultipleComponent]
public class RouteRecoverySystem : MonoBehaviour
{
    public MissionManager mission;
    public float detectionRadiusMeters = 80f;
    public float recoveryCooldownSeconds = 12f;
    float closestDistance = float.MaxValue;
    float movingAwaySeconds;
    float cooldown;
    int trackedStop = -1;

    void Awake() { if (mission == null) mission = GetComponent<MissionManager>(); }

    void Update()
    {
        if (mission == null || !mission.routeActive || mission.currentRoute == null || mission.busController == null) { ResetTracking(); return; }
        cooldown = Mathf.Max(0f, cooldown - Time.deltaTime);
        if (trackedStop != mission.currentStopIndex) { trackedStop = mission.currentStopIndex; closestDistance = float.MaxValue; movingAwaySeconds = 0f; }
        float distance = mission.distanceToNextStop;
        if (distance < closestDistance) { closestDistance = distance; movingAwaySeconds = 0f; }
        else if (closestDistance < detectionRadiusMeters && distance > closestDistance + 8f && StopIsBehindBus()) movingAwaySeconds += Time.deltaTime;
        else movingAwaySeconds = Mathf.Max(0f, movingAwaySeconds - Time.deltaTime * 2f);
        if (movingAwaySeconds < 2.5f || cooldown > 0f) return;
        cooldown = recoveryCooldownSeconds; movingAwaySeconds = 0f;
        bool rerouted = mission.RecalculateGuidanceFromBus();
        SubtitleManager.EnsureExists().Show("Navigation", rerouted ? "Stop missed. Recalculating a safe return route." : "Stop is behind you. Turn back when safe.", 3.5f);
    }

    bool StopIsBehindBus()
    {
        if (GPSManager.Instance == null || mission.currentRoute.stops == null || mission.currentStopIndex < 0 || mission.currentStopIndex >= mission.currentRoute.stops.Length) return false;
        BusStopData stop = mission.currentRoute.stops[mission.currentStopIndex];
        Vector3 target = GPSManager.Instance.GpsToWorld(stop.latitude, stop.longitude);
        Vector3 toStop = target - mission.busController.transform.position; toStop.y = 0f;
        return toStop.sqrMagnitude > 1f && Vector3.Dot(mission.busController.transform.forward, toStop.normalized) < -0.25f;
    }

    void ResetTracking() { trackedStop = -1; closestDistance = float.MaxValue; movingAwaySeconds = 0f; cooldown = 0f; }
}
