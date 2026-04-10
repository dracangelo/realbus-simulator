using UnityEngine;
using System.Collections;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }

    [Header("Active Route")]
    public BusRoute currentRoute;
    public BusController busController;

    [Header("Mission Data")]
    public MissionData missionData;

    [Header("State")]
    public MissionState missionState = MissionState.NotStarted;
    public int currentStopIndex = 0;
    public float distanceToNextStop = 0f;
    public bool routeActive = false;

    [Header("Countdown")]
    public int countdownSeconds = 5;

    private Vector3 nextStopWorldPos;
    private bool isProcessingStop = false;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(InitNextFrame());
    }

    IEnumerator InitNextFrame()
    {
        yield return null;

        if (currentRoute == null || GPSManager.Instance == null || busController == null)
        {
            Debug.LogError("MissionManager: Missing references — check Inspector!");
            yield break;
        }

        Vector3 startPos = GPSManager.Instance.GpsToWorld(
            currentRoute.stops[0].latitude,
            currentRoute.stops[0].longitude);
        startPos.y = 1f;
        busController.transform.position = startPos;

        missionState = MissionState.Briefing;
        Debug.Log("MissionManager: Ready — waiting for mission start.");
    }

    public void StartRoute(BusRoute route)
    {
        currentRoute = route;
        StartCoroutine(MissionStartSequence());
    }

    IEnumerator MissionStartSequence()
    {
        missionState = MissionState.Briefing;
        busController.throttleInput = 0f;
        busController.brakeInput = 1f;

        Debug.Log("Mission starting...");

        for (int i = countdownSeconds; i > 0; i--)
        {
            Debug.Log($"Departing in {i}...");
            yield return new WaitForSeconds(1f);
        }

        busController.brakeInput = 0f;
        missionState = MissionState.InProgress;
        routeActive = true;

        // Setup schedule
        if (ScheduleManager.Instance != null && missionData != null)
            ScheduleManager.Instance.SetupSchedule(
                currentRoute,
                missionData.scheduledDepartureTime,
                missionData.targetDurationMinutes);

        // Board first stop passengers
        currentStopIndex = 0;
        if (PassengerManager.Instance != null)
            PassengerManager.Instance.HandleStopArrival(
                currentRoute.stops[0], currentRoute.baseFare, 0, currentRoute.stops.Length);

        // Record departure stop punctuality
        if (ScheduleManager.Instance != null)
            ScheduleManager.Instance.RecordArrival(0);

        currentStopIndex = 1;
        SetNextStop();

        Debug.Log($"Route started: {currentRoute.routeName}");
    }

    void Update()
    {
        if (!routeActive || currentRoute == null) return;

        distanceToNextStop = Vector3.Distance(
            busController.transform.position, nextStopWorldPos);

        if (distanceToNextStop < 15f)
            ArrivedAtStop();
    }

    void SetNextStop()
    {
        if (currentStopIndex >= currentRoute.stops.Length)
        {
            RouteComplete();
            return;
        }

        var stop = currentRoute.stops[currentStopIndex];
        nextStopWorldPos = GPSManager.Instance.GpsToWorld(stop.latitude, stop.longitude);
        nextStopWorldPos.y = busController.transform.position.y;

        missionState = MissionState.InProgress;
        Debug.Log($"Next stop: {stop.stopName} — {distanceToNextStop:F0}m away");
    }

    void ArrivedAtStop()
    {
        if (isProcessingStop) return;
        StartCoroutine(ProcessStopArrival());
    }

    IEnumerator ProcessStopArrival()
    {
        isProcessingStop = true;

        var stop = currentRoute.stops[currentStopIndex];
        missionState = MissionState.AtStop;
        Debug.Log($"Arrived at: {stop.stopName}");

        // Record punctuality BEFORE incrementing index
        if (ScheduleManager.Instance != null)
            ScheduleManager.Instance.RecordArrival(currentStopIndex);

        // Update score
        if (ScoreTracker.Instance != null)
        {
            var status = ScheduleManager.Instance != null 
                ? ScheduleManager.Instance.RecordArrival(currentStopIndex)
                : PunctualityStatus.OnTime;
            ScoreTracker.Instance.RecordStopArrival(status);
        }

        // Handle passengers
        if (PassengerManager.Instance != null)
            PassengerManager.Instance.HandleStopArrival(
                stop, currentRoute.baseFare, currentStopIndex, currentRoute.stops.Length);

        currentStopIndex++;
        SetNextStop();

        float dwell = PassengerManager.Instance != null
            ? PassengerManager.Instance.GetRequiredDwellTimeSeconds()
            : 2f;
        yield return new WaitForSeconds(Mathf.Max(2f, dwell));
        isProcessingStop = false;
    }

    void RouteComplete()
    {
        routeActive = false;
        missionState = MissionState.Completed;
        Debug.Log($"Route complete: {currentRoute.routeName}");

        // Generate and show result
        var result = MissionResult.Generate(currentRoute);
        if (MissionResultUI.Instance != null)
            MissionResultUI.Instance.ShowResult(result);
    }

    void OnDrawGizmos()
    {
        if (!routeActive) return;
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(nextStopWorldPos, 3f);
        Gizmos.DrawLine(busController.transform.position, nextStopWorldPos);
    }
}