using UnityEngine;

/// <summary>Bus-side telemetry when the mission services live on separate GameObjects.</summary>
public class MissionDrivingMonitor : MonoBehaviour
{
    public RoadGraph roadGraph;
    public float defaultSpeedLimitKmh = 50f;
    BusController bus;
    float speedingSeconds;
    float nextRoadSample;
    float speedLimit;
    public float CurrentSpeedLimitKmh => speedLimit;
    void Awake() { bus = GetComponent<BusController>(); speedLimit = defaultSpeedLimitKmh; }
    void FixedUpdate()
    {
        if (bus == null || MissionManager.Instance == null || !MissionManager.Instance.routeActive)
        { speedingSeconds = 0f; return; }
        if (ExtendedTrafficViolationSystem.Instance != null) return;
        if (roadGraph != null && Time.time >= nextRoadSample)
        {
            nextRoadSample = Time.time + 0.5f;
            int index = roadGraph.FindNearestNodeIndex(bus.transform.position);
            if (index >= 0 && roadGraph.nodes[index].edges.Count > 0)
                speedLimit = roadGraph.nodes[index].edges[0].speedLimitKmh;
        }
        speedingSeconds = bus.currentSpeedKmh > speedLimit + 3f ? speedingSeconds + Time.fixedDeltaTime : 0f;
        if (speedingSeconds >= 2f)
        {
            ScoreTracker.Instance?.ApplyTrafficPenalty(2f);
            speedingSeconds = 0f;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.sqrMagnitude < 4f) return;
        if (GetComponent<ScoreTracker>() == null) ScoreTracker.Instance?.RecordCollision();
        RealBusAudioManager.EnsureExists().PlayCollision(collision.relativeVelocity.magnitude);
        AccessibilityManager.EnsureExists().Pulse(HapticCue.Collision);
    }
}
