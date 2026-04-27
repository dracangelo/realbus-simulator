using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    private Vector3[] activeGuidancePathPoints;
    private int guidancePathVersion = 0;
    private bool diversionActive = false;
    private string diversionLabel = string.Empty;
    private string diversionReason = string.Empty;
    private HashSet<int> blockedDiversionNodes = new HashSet<int>();

    public IReadOnlyList<Vector3> ActiveGuidancePathPoints => activeGuidancePathPoints;
    public int GuidancePathVersion => guidancePathVersion;
    public bool HasActiveDiversion => diversionActive;
    public string CurrentDiversionLabel => diversionActive ? diversionLabel : string.Empty;
    public string CurrentDiversionReason => diversionActive ? diversionReason : string.Empty;

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
        ResetGuidancePath();
        StartCoroutine(MissionStartSequence());
    }

    IEnumerator MissionStartSequence()
    {
        missionState = MissionState.Briefing;
        busController.throttleInput = 0f;
        busController.brakeInput = 1f;
        ExtendedTrafficViolationSystem.Instance?.ResetViolationLog();
        DynamicEventSystem.Instance?.StartRoute(currentRoute);

        DriverShiftSystem.Instance?.PrepareRoute(currentRoute, missionData);

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
        PassengerManager.Instance?.SetUpcomingStopIndex(currentStopIndex);
        UpdateGuidancePathForCurrentState();

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

        PunctualityStatus status = PunctualityStatus.OnTime;
        if (ScheduleManager.Instance != null)
        {
            status = ScheduleManager.Instance.RecordArrival(currentStopIndex);

            if (status == PunctualityStatus.Early)
            {
                float waitSeconds = ScheduleManager.Instance.GetEarlyWaitSecondsRequired(currentStopIndex);
                while (waitSeconds > 0.05f)
                {
                    busController.brakeInput = 1f;
                    if (StopApproachUI.Instance != null)
                        StopApproachUI.Instance.ShowWaiting(stop.stopName, waitSeconds);

                    yield return null;
                    waitSeconds = ScheduleManager.Instance.GetEarlyWaitSecondsRequired(currentStopIndex);
                }

                if (StopApproachUI.Instance != null)
                    StopApproachUI.Instance.ShowDocked(stop.stopName);
            }
        }

        // Update score
        if (ScoreTracker.Instance != null)
            ScoreTracker.Instance.RecordStopArrival(status);

        // Handle passengers
        if (PassengerManager.Instance != null)
            PassengerManager.Instance.HandleStopArrival(
                stop, currentRoute.baseFare, currentStopIndex, currentRoute.stops.Length);

        currentStopIndex++;
        if (diversionActive)
            ClearTemporaryDiversion();
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

        ApplyMissionSettlement();
        DynamicEventSystem.Instance?.CompleteRoute();

        // Generate and show result
        var result = MissionResult.Generate(currentRoute);
        DriverShiftSystem.Instance?.CompleteRoute(currentRoute, result, MissionEndedAtDepot());
        DriverShiftSystem.Instance?.TryApplyShiftSummary(result);
        if (MissionResultUI.Instance != null)
            MissionResultUI.Instance.ShowResult(result);
    }

    void ApplyMissionSettlement()
    {
        if (GameState.Instance == null)
            return;

        float grossEarnings = PassengerManager.Instance != null
            ? PassengerManager.Instance.sessionIncome
            : 0f;

        float fuelRefuelCost = 0f;
        var fuelSystem = FindFirstObjectByType<FuelSystem>();
        if (fuelSystem != null && MissionEndedAtDepot())
            fuelRefuelCost = fuelSystem.AutoRefuelAtDepot();

        float maintenanceCost = 0f;
        var maintenanceSystem = FindFirstObjectByType<MaintenanceSystem>();
        if (maintenanceSystem != null)
            maintenanceCost = maintenanceSystem.AutoServiceAndGetCost();

        float netEarnings = grossEarnings - fuelRefuelCost - maintenanceCost;

        GameState.Instance.economy.balanceKES += netEarnings;
        GameState.Instance.lastMissionSettlement.grossEarningsKES = grossEarnings;
        GameState.Instance.lastMissionSettlement.fuelRefuelCostKES = fuelRefuelCost;
        GameState.Instance.lastMissionSettlement.maintenanceCostKES = maintenanceCost;
        GameState.Instance.lastMissionSettlement.netEarningsKES = netEarnings;
        GameState.Instance.lastMissionSettlement.autoRefuelApplied = fuelRefuelCost > 0f;
    }

    bool MissionEndedAtDepot()
    {
        if (currentRoute == null || currentRoute.stops == null || currentRoute.stops.Length == 0)
            return false;

        string stopName = currentRoute.stops[currentRoute.stops.Length - 1].stopName;
        return !string.IsNullOrWhiteSpace(stopName) &&
               stopName.ToLowerInvariant().Contains("depot");
    }

    void OnDrawGizmos()
    {
        if (!routeActive) return;
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(nextStopWorldPos, 3f);
        Gizmos.DrawLine(busController.transform.position, nextStopWorldPos);
    }

    public Vector3[] GetGuidancePathPoints()
    {
        if (activeGuidancePathPoints == null || activeGuidancePathPoints.Length < 2)
            activeGuidancePathPoints = BuildBaseGuidancePath();
        return activeGuidancePathPoints;
    }

    public float GetDistanceToGuidancePath(Vector3 worldPosition)
    {
        var path = GetGuidancePathPoints();
        if (path == null || path.Length < 2)
            return 0f;

        float best = float.MaxValue;
        for (int i = 1; i < path.Length; i++)
        {
            Vector3 projected = ClosestPointOnSegment(worldPosition, path[i - 1], path[i]);
            best = Mathf.Min(best, Vector3.Distance(worldPosition, projected));
        }

        return best < float.MaxValue ? best : 0f;
    }

    public bool TryGetGuidancePointAhead(float aheadDistanceMeters, out Vector3 point)
    {
        point = Vector3.zero;
        var path = GetGuidancePathPoints();
        if (path == null || path.Length < 2 || busController == null)
            return false;

        int closestSegment = 0;
        float bestSqr = float.MaxValue;
        Vector3 busPos = busController.transform.position;
        for (int i = 1; i < path.Length; i++)
        {
            Vector3 projected = ClosestPointOnSegment(busPos, path[i - 1], path[i]);
            float sqr = (projected - busPos).sqrMagnitude;
            if (sqr < bestSqr)
            {
                bestSqr = sqr;
                closestSegment = i - 1;
                point = projected;
            }
        }

        float remaining = Mathf.Max(0f, aheadDistanceMeters);
        Vector3 cursor = point;
        for (int i = closestSegment + 1; i < path.Length; i++)
        {
            Vector3 target = path[i];
            float distance = Vector3.Distance(cursor, target);
            if (distance >= remaining)
            {
                point = Vector3.Lerp(cursor, target, remaining / Mathf.Max(0.001f, distance));
                return true;
            }

            remaining -= distance;
            cursor = target;
            point = cursor;
        }

        return true;
    }

    public bool TryApplyTemporaryDiversion(string title, string reason, Vector3 blockedWorldPosition, float blockRadiusMeters = 40f)
    {
        if (currentRoute == null || currentRoute.stops == null || currentRoute.stops.Length == 0 || busController == null)
            return false;

        var graph = FindFirstObjectByType<AIRoadGraph>();
        if (graph == null || graph.NodeCount == 0 || GPSManager.Instance == null)
            return false;

        var blockedNodes = new HashSet<int>(graph.GetNodesWithinRadius(blockedWorldPosition, blockRadiusMeters));
        if (blockedNodes.Count == 0)
            return false;

        if (!TryBuildGraphGuidancePath(graph, busController.transform.position, currentStopIndex, blockedNodes, out var diversionPath))
            return false;

        blockedDiversionNodes = blockedNodes;
        diversionActive = true;
        diversionLabel = string.IsNullOrWhiteSpace(title) ? "DETOUR" : title;
        diversionReason = reason ?? string.Empty;
        SetGuidancePath(diversionPath);
        return true;
    }

    public void ClearTemporaryDiversion()
    {
        diversionActive = false;
        diversionLabel = string.Empty;
        diversionReason = string.Empty;
        blockedDiversionNodes.Clear();
        SetGuidancePath(BuildBaseGuidancePath());
    }

    void ResetGuidancePath()
    {
        diversionActive = false;
        diversionLabel = string.Empty;
        diversionReason = string.Empty;
        blockedDiversionNodes.Clear();
        SetGuidancePath(BuildBaseGuidancePath());
    }

    void UpdateGuidancePathForCurrentState()
    {
        if (!diversionActive)
        {
            SetGuidancePath(BuildBaseGuidancePath());
            return;
        }

        var graph = FindFirstObjectByType<AIRoadGraph>();
        if (graph == null || busController == null || !TryBuildGraphGuidancePath(graph, busController.transform.position, currentStopIndex, blockedDiversionNodes, out var diversionPath))
        {
            ClearTemporaryDiversion();
            return;
        }

        SetGuidancePath(diversionPath);
    }

    bool TryBuildGraphGuidancePath(AIRoadGraph graph, Vector3 startWorld, int startStop, ISet<int> blockedNodes, out Vector3[] path)
    {
        path = null;
        if (graph == null || GPSManager.Instance == null || currentRoute == null || currentRoute.stops == null || currentRoute.stops.Length == 0)
            return false;

        var points = new List<Vector3> { startWorld };
        Vector3 cursor = startWorld;
        int firstStop = Mathf.Clamp(startStop, 0, currentRoute.stops.Length - 1);

        for (int stopIndex = firstStop; stopIndex < currentRoute.stops.Length; stopIndex++)
        {
            var stop = currentRoute.stops[stopIndex];
            Vector3 stopWorld = GPSManager.Instance.GpsToWorld(stop.latitude, stop.longitude);
            stopWorld.y = cursor.y;

            if (!graph.TryFindPathWorld(cursor, stopWorld, out var leg, blockedNodes) || leg == null || leg.Count < 2)
                return false;

            AppendPath(points, leg);
            cursor = stopWorld;
        }

        if (points.Count < 2)
            return false;

        path = points.ToArray();
        return true;
    }

    Vector3[] BuildBaseGuidancePath()
    {
        if (currentRoute == null)
            return System.Array.Empty<Vector3>();

        if (currentRoute.pathPoints != null && currentRoute.pathPoints.Length >= 2)
            return (Vector3[])currentRoute.pathPoints.Clone();

        if (currentRoute.stops == null || currentRoute.stops.Length == 0 || GPSManager.Instance == null)
            return System.Array.Empty<Vector3>();

        var points = new Vector3[currentRoute.stops.Length];
        for (int i = 0; i < currentRoute.stops.Length; i++)
            points[i] = GPSManager.Instance.GpsToWorld(currentRoute.stops[i].latitude, currentRoute.stops[i].longitude);

        return points;
    }

    void SetGuidancePath(Vector3[] newPath)
    {
        activeGuidancePathPoints = newPath ?? System.Array.Empty<Vector3>();
        guidancePathVersion++;
    }

    static void AppendPath(List<Vector3> points, List<Vector3> leg)
    {
        if (leg == null || leg.Count == 0)
            return;

        for (int i = 0; i < leg.Count; i++)
        {
            if (points.Count == 0 || (leg[i] - points[points.Count - 1]).sqrMagnitude > 1f)
                points.Add(leg[i]);
        }
    }

    static Vector3 ClosestPointOnSegment(Vector3 point, Vector3 start, Vector3 end)
    {
        Vector3 segment = end - start;
        float t = Vector3.Dot(point - start, segment) / Mathf.Max(0.0001f, Vector3.Dot(segment, segment));
        return start + segment * Mathf.Clamp01(t);
    }
}
