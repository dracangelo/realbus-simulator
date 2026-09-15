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
    const float StopApproachDistanceMeters = 50f;
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
    public bool repositionBusOnStart = true;
    public bool autoInitialize = true;
    public float ElapsedSeconds { get; private set; }
    public float DistanceDrivenKm { get; private set; }
    public MissionResult LastResult { get; private set; }
    public event System.Action<MissionResult> OnMissionCompleted;
    public int countdownSeconds = 5;
    [SerializeField] int currentCountdownValue;

    private Vector3 nextStopWorldPos;
    bool missionStarting;
    private bool isProcessingStop = false;
    private bool pendingDoorOpenRequest = false;
    private Vector3[] activeGuidancePathPoints;
    private int guidancePathVersion = 0;
    private bool diversionActive = false;
    private string diversionLabel = string.Empty;
    private string diversionReason = string.Empty;
    private HashSet<int> blockedDiversionNodes = new HashSet<int>();
    MissionCheckpoint pendingCheckpoint;

    public IReadOnlyList<Vector3> ActiveGuidancePathPoints => activeGuidancePathPoints;
    public int GuidancePathVersion => guidancePathVersion;
    public bool HasActiveDiversion => diversionActive;
    public string CurrentDiversionLabel => diversionActive ? diversionLabel : string.Empty;
    public string CurrentDiversionReason => diversionActive ? diversionReason : string.Empty;
    public int CurrentCountdownValue => currentCountdownValue;

    public event System.Action<MissionState> OnMissionStateChanged;
    public event System.Action<int> OnCountdownTick;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        if (GetComponent<RouteRecoverySystem>() == null) gameObject.AddComponent<RouteRecoverySystem>();
        if (StopApproachUI.Instance != null)
            StopApproachUI.Instance.HideApproach();

        if (autoInitialize) StartCoroutine(InitNextFrame());
    }

    public bool RecalculateGuidanceFromBus()
    {
        if (busController == null || currentRoute == null) return false;
        AIRoadGraph graph = FindFirstObjectByType<AIRoadGraph>();
        if (graph != null && TryBuildGraphGuidancePath(graph, busController.transform.position, currentStopIndex,
            diversionActive ? blockedDiversionNodes : null, out Vector3[] recalculated))
        {
            SetGuidancePath(recalculated); return true;
        }
        SetGuidancePath(BuildBaseGuidancePath()); return false;
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

        SetMissionState(MissionState.Briefing);
        Debug.Log("MissionManager: Ready — waiting for mission start.");
    }

    public void StartRoute(BusRoute route)
    {
        if (routeActive || missionStarting || currentCountdownValue > 0 || busController == null || GPSManager.Instance == null || route == null) return;
        if (route.busStops != null && route.busStops.Length > 0) route.SyncLegacyStopsFromBusStops();
        if (route.stops == null || route.stops.Length < 2) return;
        StopAllCoroutines();
        FreeDriveSession.Instance?.EndSession();
        PassengerManager.Instance?.ResetForFreeDriveMode();
        LastResult = null;
        ElapsedSeconds = DistanceDrivenKm = 0f;
        currentStopIndex = 0;
        isProcessingStop = pendingDoorOpenRequest = false;
        currentRoute = route;
        ResetGuidancePath();
        missionStarting = true;
        StartCoroutine(MissionStartSequence());
    }

    public void ResumeRoute(BusRoute route, MissionCheckpoint checkpoint)
    {
        if (checkpoint == null || route == null) return;
        pendingCheckpoint = checkpoint;
        StartRoute(route);
        if (!missionStarting) pendingCheckpoint = null;
    }

    IEnumerator MissionStartSequence()
    {
        SetMissionState(MissionState.Briefing);
        busController.ServiceBrakeInterlock = true;
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

        if (repositionBusOnStart) yield return WaitForRoadSurfaceIfAvailable();

        ApplyMissionConditions();

        Vector3 startPos = GPSManager.Instance.GpsToWorld(spawnLat, spawnLon);
        startPos = ApplySpawnOffsetFromRouteStart(startPos);
        startPos = SnapSpawnToNearestRoad(startPos);
        startPos.y = ResolveSpawnHeight(startPos);
        if (repositionBusOnStart)
        {
            if (pendingCheckpoint != null)
                busController.transform.SetPositionAndRotation(pendingCheckpoint.busPosition, pendingCheckpoint.busRotation);
            else
                busController.transform.position = startPos;
            var body = busController.GetComponent<Rigidbody>();
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }

        Debug.Log("Mission starting...");

        for (int i = countdownSeconds; i > 0; i--)
        {
            currentCountdownValue = i;
            OnCountdownTick?.Invoke(i);
            Debug.Log($"Departing in {i}...");
            yield return new WaitForSeconds(1f);
        }

        currentCountdownValue = 0;
        missionStarting = false;
        busController.brakeInput = 0f;
        busController.ServiceBrakeInterlock = false;
        ScoreTracker.Instance?.BeginMission(busController);
        SetMissionState(MissionState.InProgress);
        routeActive = true;

        // Setup schedule
        if (ScheduleManager.Instance != null && missionData != null)
            ScheduleManager.Instance.SetupSchedule(
                currentRoute,
                missionData.scheduledDepartureTime,
                missionData.targetDurationMinutes);

        if (pendingCheckpoint != null)
            RestoreMissionCheckpoint(pendingCheckpoint);
        else
        {
            // Every stop, including departure, must be docked and serviced.
            currentStopIndex = 0;
            SetNextStop();
        }

        Debug.Log($"Route started: {currentRoute.routeName}");
    }

    void RestoreMissionCheckpoint(MissionCheckpoint checkpoint)
    {
        currentStopIndex = Mathf.Clamp(checkpoint.stopIndex, 0, currentRoute.stops.Length - 1);
        ElapsedSeconds = Mathf.Max(0f, checkpoint.elapsedSeconds);
        DistanceDrivenKm = Mathf.Max(0f, checkpoint.distanceDrivenKm);
        busController.transform.SetPositionAndRotation(checkpoint.busPosition, checkpoint.busRotation);
        Rigidbody body = busController.GetComponent<Rigidbody>();
        if (body != null)
        {
            body.linearVelocity = busController.transform.forward * (Mathf.Max(0f, checkpoint.speedKmh) / 3.6f);
            body.angularVelocity = Vector3.zero;
        }
        if (ScheduleManager.Instance != null && checkpoint.scheduleTimeMinutes > 0f)
            ScheduleManager.Instance.currentTimeMinutes = checkpoint.scheduleTimeMinutes;
        ScoreTracker.Instance?.RestoreCheckpoint(checkpoint.punctualityScore, checkpoint.satisfactionScore,
            checkpoint.safetyScore, checkpoint.efficiencyScore, checkpoint.stopsCompleted,
            checkpoint.pointDeductions, checkpoint.collisions, checkpoint.redLights);
        PassengerManager.Instance?.RestoreCheckpointState(checkpoint.passengerCount, checkpoint.passengerSatisfaction,
            checkpoint.passengersServed, checkpoint.faresCollected, checkpoint.sessionIncome,
            checkpoint.passengerDistanceKm, currentStopIndex, currentRoute.stops.Length);
        pendingCheckpoint = null;
        SetNextStop();
        RecalculateGuidanceFromBus();
        SubtitleManager.EnsureExists().Show("Route restored", "Continue to the highlighted stop when ready.", 3f);
    }

    void ApplyMissionConditions()
    {
        if (missionData == null)
            return;

        if ((missionData.IsWeatherChallengeMission || missionData.applyRequiredWeather) && WeatherSystem.Instance != null)
        {
            WeatherSystem.Instance.ApplyWeather(
                missionData.requiredWeather,
                Mathf.Clamp01(missionData.requiredWeatherIntensity),
                WeatherSystem.Instance.temperature);

            Debug.Log(
                $"MissionManager: Applied mission weather {missionData.requiredWeather} " +
                $"({missionData.requiredWeatherIntensity:0.00}) for {missionData.GetMissionTypeLabel()}.");
        }
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

        ElapsedSeconds += Time.deltaTime;
        DistanceDrivenKm += busController.currentSpeedKmh / 3600f * Time.deltaTime;
        distanceToNextStop = Vector3.Distance(
            busController.transform.position, nextStopWorldPos);

        UpdateStopApproachUI();
        HandleDoorOpenRequest();
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
            StopApproachUI.Instance.UpdateGuidance(distanceToNextStop, GetSignedAngleToNextStop());
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

        SetMissionState(MissionState.InProgress);
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
        busController.ServiceBrakeInterlock = true;
        busController.RequestKneelingSuspension(true);

        var stop = currentRoute.stops[currentStopIndex];
        SetMissionState(MissionState.AtStop);
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
            ScoreTracker.Instance.RecordStopArrival(status, ScheduleManager.Instance != null
                ? ScheduleManager.Instance.GetArrivalDeltaSeconds(currentStopIndex)
                : 0f);

        // Handle passengers
        if (PassengerManager.Instance != null)
            PassengerManager.Instance.HandleStopArrival(
                stop, currentRoute.baseFare, currentStopIndex, currentRoute.stops.Length);

        float dwell = PassengerManager.Instance != null
            ? PassengerManager.Instance.GetRequiredDwellTimeSeconds() : 2f;
        yield return new WaitForSeconds(Mathf.Max(2f, dwell));
        while (PassengerManager.Instance != null && PassengerManager.Instance.HasServiceAnimations) yield return null;
        if (PassengerManager.Instance != null) PassengerManager.Instance.doorsOpen = false;
        busController.RequestKneelingSuspension(false);
        busController.ServiceBrakeInterlock = false;
        currentStopIndex++;
        pendingDoorOpenRequest = false;
        isProcessingStop = false;
        if (diversionActive) ClearTemporaryDiversion();
        SetNextStop();
    }

    void RouteComplete()
    {
        if (!routeActive) return;
        ScoreTracker.Instance?.EndMission();
        busController.ServiceBrakeInterlock = true;
        routeActive = false;
        SetMissionState(MissionState.Completed);
        if (StopApproachUI.Instance != null)
            StopApproachUI.Instance.HideApproach();
        Debug.Log($"Route complete: {currentRoute.routeName}");

        ApplyMissionSettlement();
        DynamicEventSystem.Instance?.CompleteRoute();

        // Generate and show result
        var result = MissionResult.Generate(currentRoute);
        LastResult = result;
        UnlockManager.Instance?.RecordRouteCompletion(currentRoute, GameState.Instance != null ? GameState.Instance.selectedCity : CityManager.Instance?.activeCity);
        result.SaveToPlayerPrefs();
        XPAwardResult xpAwardResult = null;
        if (XPSystem.Instance != null && result.xpEarned > 0)
            xpAwardResult = XPSystem.Instance.AwardXP(result.xpEarned, result.routeName);
        SaveManager.Instance?.RecordMissionResult(result, currentRoute, GameState.Instance != null ? GameState.Instance.selectedCity : CityManager.Instance?.activeCity);
        DriverShiftSystem.Instance?.CompleteRoute(currentRoute, result, MissionEndedAtDepot());
        DriverShiftSystem.Instance?.TryApplyShiftSummary(result);
        if (MissionResultUI.Instance != null)
            MissionResultUI.Instance.ShowResult(result, xpAwardResult);
        RealBusAudioManager.EnsureExists().PlayMissionComplete();
        AccessibilityManager.EnsureExists().Pulse(HapticCue.MissionComplete);
        SubtitleManager.EnsureExists().Show("Control", "Route complete. Excellent work.", 4f);
        OnMissionCompleted?.Invoke(result);
    }

    public void AbortMissionForFreeDrive()
    {
        StopAllCoroutines();
        missionStarting = false;
        currentCountdownValue = 0;
        ScoreTracker.Instance?.EndMission();
        if (busController != null)
        {
            busController.ServiceBrakeInterlock = false;
            busController.RequestKneelingSuspension(false);
        }
        if (PassengerManager.Instance != null) PassengerManager.Instance.doorsOpen = false;
        routeActive = false;
        isProcessingStop = false;
        pendingDoorOpenRequest = false;
        ResetGuidancePath();
        SetMissionState(MissionState.NotStarted);
    }

    public bool TryOpenDoorsAtCurrentStop()
    {
        if (!routeActive || currentRoute == null || isProcessingStop)
            return false;

        if (!IsReadyToProcessDockedStop(out _)) return false;
        if (PassengerManager.Instance != null && !PassengerManager.Instance.CanOpenDoorsForStop(currentStopIndex, currentRoute.stops.Length)) return false;
        pendingDoorOpenRequest = true;
        PassengerManager.Instance?.AcknowledgeStopRequest();

        ArrivedAtStop();
        return true;
    }

    public void FailMission(string reason)
    {
        if (!routeActive && currentCountdownValue == 0) return;
        AbortMissionForFreeDrive();
        SetMissionState(MissionState.Failed);
        Debug.LogWarning("Mission failed: " + reason);
    }

    void OnDisable()
    {
        if (routeActive || missionStarting || currentCountdownValue > 0) AbortMissionForFreeDrive();
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
        var batterySystem = FindFirstObjectByType<BatterySystem>();
        if (MissionEndedAtDepot())
        {
            if (batterySystem != null)
                fuelRefuelCost = batterySystem.AutoRechargeAtDepot();
            else if (fuelSystem != null)
                fuelRefuelCost = fuelSystem.AutoRefuelAtDepot();
        }

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

    void HandleDoorOpenRequest()
    {
        if (!pendingDoorOpenRequest)
            return;

        if (IsReadyToProcessDockedStop(out _))
            ArrivedAtStop();
    }

    bool IsReadyToProcessDockedStop(out DockingZone dockingZone)
    {
        dockingZone = DockingZone.CurrentDockedZone;
        if (dockingZone != null && dockingZone.isCorrectlyDocked)
        {
            if (dockingZone.stopIndex >= 0)
                return dockingZone.stopIndex == Mathf.Clamp(currentStopIndex, 0, currentRoute.stops.Length - 1);

            return string.Equals(dockingZone.stopName, currentRoute.stops[currentStopIndex].stopName, System.StringComparison.Ordinal);
        }

        return false; // Correct kerb, sign and heading alignment is mandatory.
    }

    float GetSignedAngleToNextStop()
    {
        Vector3 toStop = nextStopWorldPos - busController.transform.position;
        toStop.y = 0f;
        Vector3 forward = busController.transform.forward;
        forward.y = 0f;
        if (toStop.sqrMagnitude <= 0.001f || forward.sqrMagnitude <= 0.001f)
            return 0f;

        return Vector3.SignedAngle(forward.normalized, toStop.normalized, Vector3.up);
    }

    void SetMissionState(MissionState nextState)
    {
        if (missionState == nextState)
            return;

        missionState = nextState;
        OnMissionStateChanged?.Invoke(missionState);
    }
}
