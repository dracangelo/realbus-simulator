using UnityEngine;
using System.Collections;

public class MissionManager : MonoBehaviour
{
    const float MaxSpawnDistanceFromMapCenterMeters = 3500f;
    const float SpawnRaycastHeight = 600f;
    const float SpawnRaycastDistance = 2000f;
    const float SpawnClearanceAboveGround = 1.2f;
    const float SpawnOffsetBehindStartStopMeters = 14f;
    const float SpawnOffsetSideMeters = 5f;
    const float StopApproachDistanceMeters = 120f;
    const float WaitForRoadsTimeoutSeconds = 8f;
    const float SpawnRoadSearchRadiusMeters = 45f;

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
        if (StopApproachUI.Instance != null)
            StopApproachUI.Instance.HideApproach();

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

        if (currentRoute.stops == null || currentRoute.stops.Length == 0)
        {
            Debug.LogError($"MissionManager: Route '{currentRoute.routeName}' has no stops. Assign route stops or enable SceneBootstrap fallback generation.");
            yield break;
        }

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

        var city = CityManager.Instance != null ? CityManager.Instance.activeCity : null;
        double spawnLat = city != null ? city.spawnLat : currentRoute.stops[0].latitude;
        double spawnLon = city != null ? city.spawnLon : currentRoute.stops[0].longitude;
        double routeLat = currentRoute.stops[0].latitude;
        double routeLon = currentRoute.stops[0].longitude;

        if (IsSpawnNearLoadedMap(routeLat, routeLon))
        {
            spawnLat = routeLat;
            spawnLon = routeLon;
        }
        else
        {
            Debug.LogWarning("MissionManager: First route stop is outside loaded map bounds. Using city spawn instead.");
        }

        yield return WaitForRoadSurfaceIfAvailable();

        Vector3 startPos = GPSManager.Instance.GpsToWorld(spawnLat, spawnLon);
        startPos = ApplySpawnOffsetFromRouteStart(startPos);
        startPos = SnapSpawnToNearestRoad(startPos);
        startPos.y = ResolveSpawnHeight(startPos);
        busController.transform.position = startPos;

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

    bool IsSpawnNearLoadedMap(double lat, double lon)
    {
        if (GPSManager.Instance == null)
            return false;

        var world = GPSManager.Instance.GpsToWorld(lat, lon);
        world.y = 0f;
        return world.sqrMagnitude <= MaxSpawnDistanceFromMapCenterMeters * MaxSpawnDistanceFromMapCenterMeters;
    }

    float ResolveSpawnHeight(Vector3 spawnWorldPos)
    {
        Vector3 castStart = spawnWorldPos + Vector3.up * SpawnRaycastHeight;
        if (Physics.Raycast(castStart, Vector3.down, out RaycastHit hit, SpawnRaycastDistance, ~0, QueryTriggerInteraction.Ignore))
            return hit.point.y + SpawnClearanceAboveGround;

        var mapLoader = FindFirstObjectByType<MapTileLoader>();
        if (mapLoader != null)
            return mapLoader.tileSurfaceY + SpawnClearanceAboveGround;

        return 1f;
    }

    IEnumerator WaitForRoadSurfaceIfAvailable()
    {
        float deadline = Time.time + WaitForRoadsTimeoutSeconds;
        while (Time.time < deadline)
        {
            var roads = OSMRoadMeshBuilder.Instance;
            if (roads != null && roads.roadsBuilt)
                yield break;
            yield return null;
        }
    }

    Vector3 SnapSpawnToNearestRoad(Vector3 spawnPos)
    {
        var roads = OSMRoadMeshBuilder.Instance;
        if (roads == null || !roads.roadsBuilt)
            return spawnPos;

        Vector3 probe = spawnPos + Vector3.up * 2f;
        Collider[] nearby = Physics.OverlapSphere(probe, SpawnRoadSearchRadiusMeters, ~0, QueryTriggerInteraction.Ignore);
        if (nearby == null || nearby.Length == 0)
            return spawnPos;

        Vector3 best = spawnPos;
        float bestSqr = float.MaxValue;

        for (int i = 0; i < nearby.Length; i++)
        {
            var col = nearby[i];
            if (col == null) continue;
            if (!IsRoadCollider(col)) continue;

            Vector3 p = col.ClosestPoint(probe);
            Vector2 delta = new Vector2(p.x - spawnPos.x, p.z - spawnPos.z);
            float sqr = delta.sqrMagnitude;
            if (sqr < bestSqr)
            {
                bestSqr = sqr;
                best = new Vector3(p.x, spawnPos.y, p.z);
            }
        }

        return bestSqr < float.MaxValue ? best : spawnPos;
    }

    bool IsRoadCollider(Collider col)
    {
        if (col == null) return false;
        var t = col.transform;
        if (t.name.StartsWith("Road_")) return true;
        if (t.parent != null && t.parent.name == "OSM_Roads") return true;
        if (t.root != null && t.root.name == "OSM_Roads") return true;
        return false;
    }

    Vector3 ApplySpawnOffsetFromRouteStart(Vector3 startPos)
    {
        if (currentRoute == null || currentRoute.stops == null || currentRoute.stops.Length == 0 || GPSManager.Instance == null)
            return startPos;

        Vector3 forward = busController != null ? busController.transform.forward : Vector3.forward;
        if (currentRoute.stops.Length > 1)
        {
            var secondStopWorld = GPSManager.Instance.GpsToWorld(
                currentRoute.stops[1].latitude,
                currentRoute.stops[1].longitude);
            var routeDirection = new Vector3(
                secondStopWorld.x - startPos.x,
                0f,
                secondStopWorld.z - startPos.z);
            if (routeDirection.sqrMagnitude > 1f)
                forward = routeDirection.normalized;
        }

        var right = Vector3.Cross(Vector3.up, forward).normalized;
        var offset = (-forward * SpawnOffsetBehindStartStopMeters) + (right * SpawnOffsetSideMeters);
        return startPos + offset;
    }

    void Update()
    {
        if (!routeActive || currentRoute == null) return;

        distanceToNextStop = Vector3.Distance(
            busController.transform.position, nextStopWorldPos);

        UpdateStopApproachUI();

        if (distanceToNextStop < 15f)
            ArrivedAtStop();
    }

    void UpdateStopApproachUI()
    {
        if (StopApproachUI.Instance == null)
            return;

        if (currentStopIndex < 0 || currentRoute == null || currentRoute.stops == null || currentStopIndex >= currentRoute.stops.Length)
        {
            StopApproachUI.Instance.HideApproach();
            return;
        }

        if (distanceToNextStop <= StopApproachDistanceMeters && distanceToNextStop > 15f && !isProcessingStop)
        {
            var stop = currentRoute.stops[currentStopIndex];
            StopApproachUI.Instance.ShowApproach(stop.stopName, distanceToNextStop);
            StopApproachUI.Instance.UpdateDistance(distanceToNextStop);
        }
        else if (!isProcessingStop)
        {
            StopApproachUI.Instance.HideApproach();
        }
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
        if (StopApproachUI.Instance != null)
            StopApproachUI.Instance.HideApproach();
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
        if (StopApproachUI.Instance != null)
            StopApproachUI.Instance.ShowDocked(stop.stopName);

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
        if (StopApproachUI.Instance != null)
            StopApproachUI.Instance.HideApproach();
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
