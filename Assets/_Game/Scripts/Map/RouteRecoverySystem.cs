using System.Collections.Generic;
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
        // A stop that is still being served has, by definition, not been left
        // behind yet. Nagging "turn back" while the doors are open or the stop is
        // mid-service told the player they had missed a stop they were standing at.
        if (mission.isProcessingStop || mission.missionState != MissionState.InProgress)
        {
            ResetTracking();
            return;
        }
        cooldown = Mathf.Max(0f, cooldown - Time.deltaTime);
        if (trackedStop != mission.currentStopIndex) { trackedStop = mission.currentStopIndex; closestDistance = float.MaxValue; movingAwaySeconds = 0f; }
        float distance = mission.distanceToNextStop;
        if (distance < closestDistance) { closestDistance = distance; movingAwaySeconds = 0f; }
        else if (closestDistance < detectionRadiusMeters && distance > closestDistance + 8f && StopIsBehindBus()) movingAwaySeconds += Time.deltaTime;
        else movingAwaySeconds = Mathf.Max(0f, movingAwaySeconds - Time.deltaTime * 2f);
        if (movingAwaySeconds < 2.5f || cooldown > 0f) return;
        cooldown = recoveryCooldownSeconds; movingAwaySeconds = 0f;
        // A stop can sit behind the bus on a perfectly correct route - a loop, a
        // bend that wraps around a block - so "behind" alone is not a missed stop.
        // Only warn when the bus is actually driving against the guidance line it
        // was given.
        if (!DrivingAgainstGuidance())
        {
            movingAwaySeconds = 0f;
            return;
        }

        bool rerouted = mission.RecalculateGuidanceFromBus();
        SubtitleManager.EnsureExists().Show("Navigation", rerouted ? "Stop missed. Recalculating a safe return route." : "Stop is behind you. Turn back when safe.", 3.5f);
    }

    /// <summary>
    /// True when the bus faces against the nearest segment of the guidance path.
    /// That is the one reliable signal that the player is driving away from the
    /// route rather than around a corner it legitimately bends round.
    /// </summary>
    bool DrivingAgainstGuidance()
    {
        if (mission.busController == null) return true;
        return IsDrivingAgainstPath(mission.ActiveGuidancePathPoints,
            mission.busController.transform.position, mission.busController.transform.forward);
    }

    /// <summary>
    /// True when <paramref name="forward"/> points against the nearest segment of
    /// <paramref name="path"/>. Pure so the warning rule can be checked without a
    /// mission, a bus or a frame.
    /// </summary>
    public static bool IsDrivingAgainstPath(IReadOnlyList<Vector3> path, Vector3 busPos, Vector3 forward)
    {
        if (path == null || path.Count < 2) return true;
        forward.y = 0f;
        if (forward.sqrMagnitude < 0.001f) return false;

        int best = -1;
        float bestSqr = float.MaxValue;
        for (int i = 0; i < path.Count - 1; i++)
        {
            Vector3 a = path[i]; a.y = busPos.y;
            Vector3 b = path[i + 1]; b.y = busPos.y;
            Vector3 ab = b - a;
            if (ab.sqrMagnitude < 1f) continue;
            float t = Mathf.Clamp01(Vector3.Dot(busPos - a, ab) / ab.sqrMagnitude);
            float sqr = (a + ab * t - busPos).sqrMagnitude;
            if (sqr >= bestSqr) continue;
            bestSqr = sqr;
            best = i;
        }

        if (best < 0) return true;
        Vector3 direction = path[best + 1] - path[best];
        direction.y = 0f;
        if (direction.sqrMagnitude < 0.01f) return false;
        return Vector3.Dot(forward.normalized, direction.normalized) < -0.35f;
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
