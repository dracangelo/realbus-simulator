using UnityEngine;
using System.Collections;

public class DockingZone : MonoBehaviour
{
    [Header("Settings")]
    public float dockingRadius = 3f;
    public float kerbToleranceMeters = 0.5f;
    public float stopSignToleranceMeters = 3f;
    public float maxDockingSpeedKmh = 1.5f;
    public float headingToleranceDegrees = 25f;
    public string stopName;
    public int stopIndex = -1;

    public static DockingZone CurrentDockedZone { get; private set; }

    private BusController busController;
    public bool isDocked = false;
    public bool isCorrectlyDocked = false;
    public float currentKerbOffsetMeters;
    public float currentStopSignOffsetMeters;

    void Start()
    {
        SyncLegacyDockingRadius();
        StartCoroutine(InitNextFrame());
    }

    void OnValidate()
    {
        SyncLegacyDockingRadius();
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

        bool wasDockedBefore = isDocked;
        EvaluateDockingState();

        if (isDocked && !wasDockedBefore) OnDocked();
        if (!isDocked && wasDockedBefore) OnUndocked();
    }

    void OnDocked()
    {
        Debug.Log($"Docked at: {stopName}");
        CurrentDockedZone = this;
        if (busController != null)
            busController.RequestKneelingSuspension(true);
        if (StopApproachUI.Instance != null)
            StopApproachUI.Instance.ShowDocked(stopName);
    }

    void OnUndocked()
    {
        Debug.Log($"Departed: {stopName}");
        if (CurrentDockedZone == this)
            CurrentDockedZone = null;
        if (busController != null)
            busController.RequestKneelingSuspension(false);
        if (StopApproachUI.Instance != null)
            StopApproachUI.Instance.HideApproach();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.4f);
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(kerbToleranceMeters * 2f, 1f, stopSignToleranceMeters * 2f));
        Gizmos.matrix = Matrix4x4.identity;
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

    void EvaluateDockingState()
    {
        Vector3 local = transform.InverseTransformPoint(busController.transform.position);
        currentKerbOffsetMeters = Mathf.Abs(local.x);
        currentStopSignOffsetMeters = Mathf.Abs(local.z);

        float headingError = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, busController.transform.eulerAngles.y));
        isCorrectlyDocked =
            currentKerbOffsetMeters <= kerbToleranceMeters &&
            currentStopSignOffsetMeters <= stopSignToleranceMeters &&
            headingError <= headingToleranceDegrees &&
            busController.currentSpeedKmh <= maxDockingSpeedKmh;

        isDocked = isCorrectlyDocked;
    }

    void SyncLegacyDockingRadius()
    {
        if (dockingRadius > 0f)
            stopSignToleranceMeters = dockingRadius;
        else
            dockingRadius = stopSignToleranceMeters;
    }
}
