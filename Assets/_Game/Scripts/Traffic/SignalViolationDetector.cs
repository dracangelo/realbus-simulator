using UnityEngine;

public class SignalViolationDetector : MonoBehaviour
{
    [Header("References")]
    public TrafficLight trafficLight;

    [Header("Settings")]
    public float detectionWidth = 8f;
    public float detectionDepth = 3f;

    private BusController busController;
    private bool busInZone = false;

    void Start()
    {
        busController = FindFirstObjectByType<BusController>();
    }

    void Update()
    {
        if (busController == null || trafficLight == null) return;

        Vector3 diff = busController.transform.position - transform.position;
        bool inZone = Mathf.Abs(diff.x) < detectionWidth * 0.5f
                   && Mathf.Abs(diff.z) < detectionDepth * 0.5f;

        if (inZone && !busInZone)
        {
            busInZone = true;

            if (trafficLight.IsRed() && busController.currentSpeedKmh > 5f)
            {
                Debug.Log("RED LIGHT VIOLATION!");
                if (ExtendedTrafficViolationSystem.Instance != null)
                    ExtendedTrafficViolationSystem.Instance.RecordSignalViolation(redLight: true);
                else
                    ScoreTracker.Instance?.RecordRedLight();
            }
            else if (trafficLight.currentState == TrafficLight.LightState.Amber && busController.currentSpeedKmh > 5f)
            {
                Debug.Log("YELLOW LIGHT VIOLATION!");
                ExtendedTrafficViolationSystem.Instance?.RecordSignalViolation(redLight: false);
            }
        }

        if (!inZone && busInZone)
        {
            busInZone = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.4f);
        Gizmos.DrawCube(transform.position,
            new Vector3(detectionWidth, 1f, detectionDepth));
    }
}
