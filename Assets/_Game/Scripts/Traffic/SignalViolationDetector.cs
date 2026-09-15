using UnityEngine;

public class SignalViolationDetector : MonoBehaviour
{
    public TrafficLight trafficLight;
    public float detectionWidth = 8f;
    public float detectionDepth = 3f;
    public float frontBumperOffsetMeters = 6f;
    BusController busController;
    Vector3 previous;
    bool sampled;

    void Start() { busController = FindFirstObjectByType<BusController>(); }
    void FixedUpdate()
    {
        if (busController == null || trafficLight == null) return;
        Vector3 point = busController.transform.position + busController.transform.forward * frontBumperOffsetMeters;
        Vector3 current = transform.InverseTransformPoint(point);
        bool active = MissionManager.Instance != null && MissionManager.Instance.routeActive;
        if (sampled && active && Mathf.Abs(current.y) < 4f &&
            GameplayRules.CrossedStopLine(previous.x, previous.z, current.x, current.z, detectionWidth) && trafficLight.IsRed())
        {
            if (ExtendedTrafficViolationSystem.Instance != null)
                ExtendedTrafficViolationSystem.Instance.RecordSignalViolation(true);
            else ScoreTracker.Instance?.RecordRedLight();
        }
        previous = current;
        sampled = true;
    }
    void OnDisable() { sampled = false; }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(detectionWidth, 1f, 0.1f));
        Gizmos.matrix = Matrix4x4.identity;
    }
}
