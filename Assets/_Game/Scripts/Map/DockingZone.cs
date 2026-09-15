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
    public float currentHeadingErrorDegrees;
    public float currentDockingScore;
    LineRenderer bayOutline;
    Material bayMaterial;

    void Start()
    {
        SyncLegacyDockingRadius();
        BuildBayOutline();
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
            if (bayOutline != null) bayOutline.enabled = false;
            isCorrectlyDocked = false;
            if (isDocked)
            {
                isDocked = false;
                OnUndocked();
            }
            return;
        }

        bool wasDockedBefore = isDocked;
        EvaluateDockingState();
        UpdateBayFeedback();

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
            StopApproachUI.Instance.ShowDocked(stopName, currentDockingScore, DockingGrade(currentDockingScore));
        RealBusAudioManager.EnsureExists().PlayDocking();
        AccessibilityManager.EnsureExists().Pulse(HapticCue.Docking);
        SubtitleManager.EnsureExists().Show("Conductor", $"Arrived at {stopName}.", 2f);
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

    void OnDisable()
    {
        if (CurrentDockedZone == this) OnUndocked();
        isDocked = isCorrectlyDocked = false;
    }

    void OnDestroy()
    {
        if (bayMaterial != null) Destroy(bayMaterial);
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

        return false;
    }

    void EvaluateDockingState()
    {
        Vector3 local = transform.InverseTransformPoint(busController.dockingReference != null
            ? busController.dockingReference.position : busController.transform.position);
        currentKerbOffsetMeters = Mathf.Abs(local.x);
        currentStopSignOffsetMeters = Mathf.Abs(local.z);

        float headingError = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, busController.transform.eulerAngles.y));
        currentHeadingErrorDegrees = headingError;
        currentDockingScore = CalculateDockingScore(currentKerbOffsetMeters, currentStopSignOffsetMeters,
            currentHeadingErrorDegrees, busController.currentSpeedKmh, kerbToleranceMeters,
            stopSignToleranceMeters, headingToleranceDegrees, maxDockingSpeedKmh);
        isCorrectlyDocked = GameplayRules.IsDocked(currentKerbOffsetMeters, currentStopSignOffsetMeters,
            headingError, busController.currentSpeedKmh, kerbToleranceMeters, stopSignToleranceMeters,
            headingToleranceDegrees, maxDockingSpeedKmh);

        isDocked = isCorrectlyDocked;
    }

    void UpdateBayFeedback()
    {
        if (bayOutline != null)
        {
            bayOutline.enabled = true;
            Color color = isDocked ? UITheme.Success : currentDockingScore >= 60f ? UITheme.TertiaryDim : UITheme.Error;
            bayOutline.startColor = bayOutline.endColor = color;
        }
        if (!isDocked && StopApproachUI.Instance != null && currentStopSignOffsetMeters <= Mathf.Max(12f, stopSignToleranceMeters * 3f))
            StopApproachUI.Instance.ShowDockingAlignment(stopName, currentKerbOffsetMeters, currentStopSignOffsetMeters,
                currentHeadingErrorDegrees, busController.currentSpeedKmh, currentDockingScore,
                kerbToleranceMeters, stopSignToleranceMeters, headingToleranceDegrees, maxDockingSpeedKmh);
    }

    void BuildBayOutline()
    {
        bayOutline = GetComponent<LineRenderer>();
        if (bayOutline == null) bayOutline = gameObject.AddComponent<LineRenderer>();
        bayOutline.useWorldSpace = false; bayOutline.loop = false; bayOutline.positionCount = 5; bayOutline.widthMultiplier = 0.09f;
        bayOutline.SetPositions(new[]
        {
            new Vector3(-kerbToleranceMeters, 0.04f, -stopSignToleranceMeters),
            new Vector3(kerbToleranceMeters, 0.04f, -stopSignToleranceMeters),
            new Vector3(kerbToleranceMeters, 0.04f, stopSignToleranceMeters),
            new Vector3(-kerbToleranceMeters, 0.04f, stopSignToleranceMeters),
            new Vector3(-kerbToleranceMeters, 0.04f, -stopSignToleranceMeters)
        });
        Shader shader = Shader.Find("Sprites/Default");
        if (shader != null) { bayMaterial = new Material(shader); bayOutline.material = bayMaterial; }
        bayOutline.enabled = false;
    }

    public static float CalculateDockingScore(float kerb, float sign, float heading, float speed,
        float kerbTolerance, float signTolerance, float headingTolerance, float speedTolerance)
    {
        float error = Mathf.Clamp01(kerb / Mathf.Max(0.01f, kerbTolerance)) * 0.4f
            + Mathf.Clamp01(sign / Mathf.Max(0.01f, signTolerance)) * 0.3f
            + Mathf.Clamp01(heading / Mathf.Max(0.01f, headingTolerance)) * 0.2f
            + Mathf.Clamp01(speed / Mathf.Max(0.01f, speedTolerance)) * 0.1f;
        return Mathf.Round((1f - error) * 100f);
    }

    public static string DockingGrade(float score)
    {
        if (score >= 85f) return "GOLD";
        if (score >= 65f) return "SILVER";
        return "BRONZE";
    }

    void SyncLegacyDockingRadius()
    {
        if (dockingRadius > 0f)
            stopSignToleranceMeters = dockingRadius;
        else
            dockingRadius = stopSignToleranceMeters;
    }
}
