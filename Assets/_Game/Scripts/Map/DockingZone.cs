using UnityEngine;
using System.Collections;

public class DockingZone : MonoBehaviour
{
    [Header("Settings")]
    public float dockingRadius = 12f;
    public string stopName;
    public int stopIndex = -1;

    private BusController busController;
    public bool isDocked = false;

    void Start()
    {
        StartCoroutine(InitNextFrame());
    }

    IEnumerator InitNextFrame()
    {
        yield return null; // wait for BusStop.cs to position this GameObject
        busController = FindFirstObjectByType<BusController>();
        Debug.Log($"DockingZone '{stopName}' ready at position: {transform.position}");
    }

    void Update()
    {
        if (busController == null) return;
        if (!IsRelevantStop())
        {
            if (isDocked)
            {
                isDocked = false;
                OnUndocked();
            }
            return;
        }

        float distance = Vector3.Distance(
            transform.position, busController.transform.position);

        bool wasDockedBefore = isDocked;
        isDocked = distance < dockingRadius
                && busController.currentSpeedKmh < 3f;

        if (isDocked && !wasDockedBefore) OnDocked();
        if (!isDocked && wasDockedBefore) OnUndocked();
    }

    void OnDocked()
    {
        Debug.Log($"Docked at: {stopName}");
        if (StopApproachUI.Instance != null)
            StopApproachUI.Instance.ShowDocked(stopName);
    }

    void OnUndocked()
    {
        Debug.Log($"Departed: {stopName}");
        if (StopApproachUI.Instance != null)
            StopApproachUI.Instance.HideApproach();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.4f);
        Gizmos.DrawWireSphere(transform.position, dockingRadius);
    }

    bool IsRelevantStop()
    {
        var mission = MissionManager.Instance;
        if (mission != null && mission.routeActive && mission.currentRoute != null)
        {
            int currentIndex = Mathf.Clamp(mission.currentStopIndex, 0, mission.currentRoute.stops.Length - 1);
            return stopIndex >= 0 ? stopIndex == currentIndex : stopName == mission.currentRoute.stops[currentIndex].stopName;
        }

        var freeDrive = FreeDriveSession.Instance;
        if (freeDrive != null && freeDrive.sessionActive)
            return true;

        return false;
    }
}
