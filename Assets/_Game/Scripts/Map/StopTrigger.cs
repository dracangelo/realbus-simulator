using UnityEngine;
using System.Collections;

public class StopTrigger : MonoBehaviour
{
    [Header("Stop Data")]
    public string stopName;
    public int stopIndex;

    [Header("Trigger Settings")]
    public float approachDistance = 50f;

    private bool approachTriggered = false;
    private Transform busTransform;

    void Start()
    {
        StartCoroutine(InitNextFrame());
    }

    IEnumerator InitNextFrame()
    {
        yield return null; // wait for BusStop.cs to position this GameObject

        var bus = FindFirstObjectByType<BusController>();
        if (bus != null) busTransform = bus.transform;

        Debug.Log($"StopTrigger '{stopName}' ready at position: {transform.position}");
    }

    void Update()
    {
        if (busTransform == null) return;
        if (!IsRelevantStop())
        {
            if (approachTriggered)
            {
                approachTriggered = false;
                if (StopApproachUI.Instance != null)
                    StopApproachUI.Instance.HideApproach();
            }
            return;
        }

        float distance = Vector3.Distance(transform.position, busTransform.position);

        // Approaching
        if (!approachTriggered && distance < approachDistance)
        {
            approachTriggered = true;
            OnApproach(distance);
        }

        // Update distance live while in approach zone
        if (approachTriggered && distance < approachDistance)
        {
            if (StopApproachUI.Instance != null)
                StopApproachUI.Instance.UpdateGuidance(distance, CalculateSignedAngleToStop());
        }

        // Reset when bus moves away
        if (approachTriggered && distance > approachDistance + 20f)
        {
            approachTriggered = false;
            if (StopApproachUI.Instance != null)
                StopApproachUI.Instance.HideApproach();
        }
    }

    void OnApproach(float distance)
    {
        Debug.Log($"Approaching: {stopName} — {distance:F0}m");
        if (StopApproachUI.Instance != null)
            StopApproachUI.Instance.ShowApproach(stopName, distance, CalculateSignedAngleToStop());
    }

    float CalculateSignedAngleToStop()
    {
        if (busTransform == null)
            return 0f;

        Vector3 toStop = transform.position - busTransform.position;
        toStop.y = 0f;
        Vector3 forward = busTransform.forward;
        forward.y = 0f;
        if (toStop.sqrMagnitude <= 0.001f || forward.sqrMagnitude <= 0.001f)
            return 0f;

        return Vector3.SignedAngle(forward.normalized, toStop.normalized, Vector3.up);
    }

    bool IsRelevantStop()
    {
        var mission = MissionManager.Instance;
        if (mission != null && mission.routeActive && mission.currentRoute != null)
            return stopIndex == Mathf.Clamp(mission.currentStopIndex, 0, mission.currentRoute.stops.Length - 1);

        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
        Gizmos.DrawSphere(transform.position, approachDistance);
        Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, 12f);
    }
}
