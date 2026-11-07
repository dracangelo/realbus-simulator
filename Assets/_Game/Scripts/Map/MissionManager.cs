using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
    const float SpawnRaycastHeight = 600f;
    const float SpawnRaycastDistance = 2000f;
    const float SpawnClearanceAboveGround = 1.2f;
    const float SpawnOffsetBehindStartStopMeters = 14f;
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
    public bool startAtDepot = true;
    public bool autoInitialize = true;
    /// <summary>
    /// True when this mission start did not place a depot (placement failed, the
    /// start was a checkpoint resume, or the scene starts on the street). The
    /// loading gate reads it so it never waits for a yard that is not coming.
    /// </summary>
    public bool DepotPlacementFailed { get; private set; }
    public float ElapsedSeconds { get; private set; }
    public float DistanceDrivenKm { get; private set; }
    public MissionResult LastResult { get; private set; }
    public event System.Action<MissionResult> OnMissionCompleted;
    public int countdownSeconds = 5;
    [SerializeField] int currentCountdownValue;

    private Vector3 nextStopWorldPos;
    bool missionStarting;
    public bool isProcessingStop { get; private set; } = false;

    /// <summary>
    /// Upper bound on how long a stop waits for boarding/alighting animations
    /// before it completes anyway. The same anti-stuck reasoning as the door-lock
    /// ceiling: a stuck animation must never strand the route at a stop.
    /// </summary>
    public const float MaxServiceAnimationWaitSeconds = 10f;
    public bool earlyDepartureRequested { get; private set; } = false;
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
        AIRoadGraph graph = AIRoadGraph.ResolveCityGraph(busController.transform.position, 2500f);
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
        route.EnsureRuntimeData();
        route.EnsureWorldPathPoints();
        StopAllCoroutines();
        FreeDriveSession.Instance?.EndSession();
        PassengerManager.Instance?.ResetForFreeDriveMode();
        LastResult = null;
        ElapsedSeconds = DistanceDrivenKm = 0f;
        DepotPlacementFailed = false;
        currentStopIndex = 0;
        isProcessingStop = pendingDoorOpenRequest = false;
        currentRoute = route;
        if (route.stops != null && route.stops.Length > 0 && MapTileLoader.Instance != null)
        {
            MapTileLoader.Instance.LoadMap(route.stops[0].latitude, route.stops[0].longitude, MapTileLoader.Instance.zoomLevel);
        }
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
        double spawnLat = currentRoute.stops[0].latitude;
        double spawnLon = currentRoute.stops[0].longitude;

        // The map loader has already been asked to centre on this route start.
        // The old 3.5 km-from-origin guard rejected perfectly valid outlying
        // terminals (Kigali's Kwa Mironko is 3.55 km from the city centre) and
        // dropped the bus at the city spawn, leaving the minimap blank and the
        // first stop three kilometres away. Only reject coordinates that are
        // not actually a place on Earth.
        if (city != null && !IsPlausibleRouteStart(spawnLat, spawnLon))
        {
            Debug.LogWarning("MissionManager: Route start is not a valid coordinate. Using city spawn instead.");
            spawnLat = city.spawnLat;
            spawnLon = city.spawnLon;
        }

        if (repositionBusOnStart) yield return WaitForRoadSurfaceIfAvailable();

        ApplyMissionConditions();

        Vector3 startPos = GPSManager.Instance.GpsToWorld(spawnLat, spawnLon);
        startPos = ApplySpawnOffsetFromRouteStart(startPos);
        bool spawnedAtDepot = false;
        if (repositionBusOnStart)
        {
            if (pendingCheckpoint != null)
                busController.Teleport(pendingCheckpoint.busPosition, pendingCheckpoint.busRotation);
            else if (startAtDepot && BusDepotManager.EnsureExists().TrySpawnDepotAndResolveBay(currentRoute, startPos, busController, out var depotBayPos, out var depotBayRot, out var depotExitPos))
            {
                spawnedAtDepot = true;
                busController.Teleport(depotBayPos, depotBayRot);
                busController.SetParkingBrake(true);
                // Throttle releases the park brake (BusController), and the yard's own
                // traffic is held off the aisles until the bus is clear of the yard, so
                // the instruction can be about where to drive rather than which key to find.
                SubtitleManager.EnsureExists().Show("Depot Dispatch",
                    $"Route '{currentRoute.routeName}' assigned. Pull out of Bay 1 and follow the exit lane; yard traffic holds until you are clear.", 5f);
                Debug.Log($"MissionManager: Player spawned at Depot Bay 1: {depotBayPos}, heading {depotBayRot.eulerAngles.y:F1} deg.");
            }
            else
            {
                Vector3 heading = busController.transform.forward;
                if (currentRoute.stops.Length > 1)
                    heading = GPSManager.Instance.GpsToWorld(currentRoute.stops[1].latitude, currentRoute.stops[1].longitude) - startPos;
                heading = RoadSpawnResolver.RouteHeading(currentRoute, startPos, heading);
                if (RoadSpawnResolver.TryResolve(busController, startPos, heading, SpawnRoadSearchRadiusMeters, out var roadPosition, out var roadRotation))
                    busController.Teleport(roadPosition, roadRotation);
                else
                {
                    startPos.y = ResolveSpawnHeight(startPos);
                    busController.Teleport(startPos, busController.transform.rotation);
                }
            }
            var body = busController.GetComponent<Rigidbody>();
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            MobileCameraController.EnsureExists().SetPreset(MobileCameraController.Instance.CurrentPreset);
            Debug.Log($"MissionManager: Player spawned at {body.position}, heading {body.rotation.eulerAngles.y:F1} degrees.");
        }

        // A depot that could not be placed (or a checkpoint resume, or a scene that
        // starts on the street) must not hold the loading screen open waiting for a
        // yard that is never coming. The bus is on the road either way.
        DepotPlacementFailed = pendingCheckpoint != null || (startAtDepot && !spawnedAtDepot);

        // The loading card is still up while the map, roads, traffic and cockpit
        // warm. Do not burn the countdown - or start the mission clock - behind it:
        // wait for the overlay to hand the world over. Bounded by the overlay's own
        // ceiling, and a no-op when the scene is played without an overlay.
        yield return WorldReadyLoadingScreen.WaitUntilPlayable();

        Debug.Log("Mission starting...");

        // Responsive fast-start: driver can press throttle to depart immediately
        int totalCountdown = Mathf.Min(countdownSeconds, 2);
        for (int i = totalCountdown; i > 0; i--)
        {
            currentCountdownValue = i;
            OnCountdownTick?.Invoke(i);
            Debug.Log($"Departing in {i}...");
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime;
                var keyboard = UnityEngine.InputSystem.Keyboard.current;
                bool accel = (busController != null && busController.throttleInput > 0.05f) ||
                             (keyboard != null && (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed));
                if (accel) { i = 0; break; }
                yield return null;
            }
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
        // A checkpoint saved on the duplicate platform of the stop just served would
        // otherwise resume with the navigation aiming back at that station.
        currentStopIndex = BusRoute.NextDistinctStopIndex(currentRoute.stops, currentStopIndex - 1);
        ElapsedSeconds = Mathf.Max(0f, checkpoint.elapsedSeconds);
        DistanceDrivenKm = Mathf.Max(0f, checkpoint.distanceDrivenKm);
        busController.Teleport(checkpoint.busPosition, checkpoint.busRotation);
        Rigidbody body = busController.GetComponent<Rigidbody>();
        if (body != null)
        {
            body.linearVelocity = busController.transform.forward * (Mathf.Max(0f, checkpoint.speedKmh) / 3.6f);
            body.angularVelocity = Vector3.zero;
        }
        if (ScheduleManager.Instance != null && checkpoint.scheduleTimeMinutes > 0f)
            ScheduleManager.Instance.currentTimeMinutes = checkpoint.scheduleTimeMinutes;
        // The world clock normally runs its own compressed day, but a checkpoint is
        // an explicit point in the mission's day: restore the sky to the same hour so
        // a reloaded afternoon route does not start under a morning sun.
        if (TimeOfDaySystem.Instance != null && checkpoint.scheduleTimeMinutes > 0f)
            TimeOfDaySystem.Instance.SetTimeOfDay(checkpoint.scheduleTimeMinutes);
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

    static bool IsPlausibleRouteStart(double lat, double lon)
    {
        if (double.IsNaN(lat) || double.IsNaN(lon) ||
            double.IsInfinity(lat) || double.IsInfinity(lon))
            return false;

        // (0, 0) is the default value of an unfilled stop, not a terminal in
        // the Gulf of Guinea.
        if (System.Math.Abs(lat) < 0.000001d && System.Math.Abs(lon) < 0.000001d)
            return false;

        return System.Math.Abs(lat) <= 90d && System.Math.Abs(lon) <= 180d;
    }

    float ResolveSpawnHeight(Vector3 spawnWorldPos)
    {
        Vector3 castStart = spawnWorldPos + Vector3.up * SpawnRaycastHeight;
        if (Physics.Raycast(castStart, Vector3.down, out RaycastHit hit, SpawnRaycastDistance, ~0, QueryTriggerInteraction.Ignore))
            return hit.point.y + SpawnClearanceAboveGround;

        var mapLoader = FindAnyObjectByType<MapTileLoader>();
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

        forward = RoadSpawnResolver.RouteHeading(currentRoute, startPos, forward).normalized;
        // RoadSpawnResolver owns the lateral lane offset. A second, hard-coded
        // right offset biases the search onto the opposing carriageway.
        return startPos - forward * SpawnOffsetBehindStartStopMeters;
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

    void ArrivedAtStop(bool doorsRefused = false)
    {
        if (isProcessingStop) return;
        StartCoroutine(ProcessStopArrival(doorsRefused));
    }

    IEnumerator ProcessStopArrival(bool doorsRefused)
    {
        isProcessingStop = true;
        earlyDepartureRequested = false;
        busController.ServiceBrakeInterlock = true;
        // No doors, no step: kneeling is part of the door cycle.
        busController.RequestKneelingSuspension(!doorsRefused);

        var stop = currentRoute.stops[currentStopIndex];
        SetMissionState(MissionState.AtStop);
        Debug.Log($"Arrived at: {stop.stopName}{(doorsRefused ? " (doors refused: bus full)" : string.Empty)}");
        if (StopApproachUI.Instance != null)
        {
            if (doorsRefused)
                StopApproachUI.Instance.ShowDoorsRefused(stop.stopName);
            else
                StopApproachUI.Instance.ShowDocked(stop.stopName);
        }
        if (doorsRefused)
            SubtitleManager.EnsureExists().Show("Doors",
                "Bus full — no room to board. Stopping, then continuing to the next stop.", 4f);

        PunctualityStatus status = PunctualityStatus.OnTime;
        if (ScheduleManager.Instance != null)
        {
            status = ScheduleManager.Instance.RecordArrival(currentStopIndex);
        }

        // Handle passenger boarding and alighting immediately on door open
        if (PassengerManager.Instance != null)
        {
            if (doorsRefused)
                PassengerManager.Instance.HandleRefusedDoorsStopArrival(
                    stop, currentStopIndex, currentRoute.stops.Length);
            else
                PassengerManager.Instance.HandleStopArrival(
                    stop, currentRoute.baseFare, currentStopIndex, currentRoute.stops.Length);
        }

        // Update score
        if (ScoreTracker.Instance != null)
            ScoreTracker.Instance.RecordStopArrival(status, ScheduleManager.Instance != null
                ? ScheduleManager.Instance.GetArrivalDeltaSeconds(currentStopIndex)
                : 0f);

        float dwell = PassengerManager.Instance != null
            ? PassengerManager.Instance.GetRequiredDwellTimeSeconds() : 2f;

        if (status == PunctualityStatus.Early && ScheduleManager.Instance != null)
        {
            float waitSeconds = ScheduleManager.Instance.GetEarlyWaitSecondsRequired(currentStopIndex);
            if (waitSeconds > dwell)
                dwell = waitSeconds;
        }

        float dwellTimer = 0f;
        float maxDwell = Mathf.Max(1.5f, dwell);
        while (dwellTimer < maxDwell && !earlyDepartureRequested)
        {
            dwellTimer += Time.deltaTime;
            if (!doorsRefused && status == PunctualityStatus.Early && ScheduleManager.Instance != null && StopApproachUI.Instance != null)
            {
                float remainingEarlyWait = ScheduleManager.Instance.GetEarlyWaitSecondsRequired(currentStopIndex);
                if (remainingEarlyWait > 0.5f)
                    StopApproachUI.Instance.ShowWaiting(stop.stopName, remainingEarlyWait);
                else
                    StopApproachUI.Instance.ShowDocked(stop.stopName);
            }
            yield return null;
        }

        // A boarding or alighting animation can outlive its useful window (a
        // passenger waiting behind a door that was shut, a model whose exit point
        // is never reached). Waiting on it with no ceiling left the mission in
        // AtStop with the served stop still the navigation target, so however far
        // the player drove the game behaved as if the stop had not been left. The
        // wait is bounded, and the stop always completes.
        float serviceWait = 0f;
        while (!earlyDepartureRequested && PassengerManager.Instance != null &&
               PassengerManager.Instance.HasServiceAnimations)
        {
            serviceWait += Time.deltaTime;
            if (serviceWait >= MaxServiceAnimationWaitSeconds)
            {
                Debug.LogWarning($"[MissionManager] Service animations at '{stop.stopName}' did not finish " +
                    $"within {MaxServiceAnimationWaitSeconds:F0}s. Completing the stop so the route can continue.");
                break;
            }
            yield return null;
        }

        if (PassengerManager.Instance != null)
        {
            PassengerManager.Instance.CompletePendingBoardingAndAlighting();
            PassengerManager.Instance.doorsOpen = false;
        }

        busController.RequestKneelingSuspension(false);
        busController.ServiceBrakeInterlock = false;
        busController.brakeInput = 0f;
        // A route can list the same platform twice (the two directions of a stop
        // pair). Advancing onto it made the navigation point back at the stop the bus
        // had just served, so the duplicates are skipped here instead.
        int servedStopIndex = currentStopIndex;
        currentStopIndex = BusRoute.NextDistinctStopIndex(currentRoute.stops, servedStopIndex);
        int skippedPlatforms = currentStopIndex - servedStopIndex - 1;
        if (skippedPlatforms > 0)
        {
            string servedName = servedStopIndex >= 0 && servedStopIndex < currentRoute.stops.Length
                ? currentRoute.stops[servedStopIndex].stopName : servedStopIndex.ToString();
            Debug.Log($"[MissionManager] Skipped {skippedPlatforms} stop(s) recorded at the same platform as '{servedName}'.");
        }
        pendingDoorOpenRequest = false;
        isProcessingStop = false;
        earlyDepartureRequested = false;
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
            busController.brakeInput = 0f;
        }
        if (PassengerManager.Instance != null) PassengerManager.Instance.doorsOpen = false;
        routeActive = false;
        isProcessingStop = false;
        earlyDepartureRequested = false;
        pendingDoorOpenRequest = false;
        ResetGuidancePath();
        SetMissionState(MissionState.NotStarted);
    }

    public bool TryOpenDoorsAtCurrentStop()
    {
        if (!routeActive || currentRoute == null || isProcessingStop)
            return false;

        if (!IsReadyToProcessDockedStop(out _)) return false;

        // A bus at its doorway limit cannot open its doors. That is a door state,
        // not a reason to skip the stop: returning false here left the mission
        // waiting at a stop it had already reached, so currentStopIndex never
        // advanced and every guidance rebuild steered the player back to the stop
        // just left. The stop is still served, with the doors shut.
        bool doorsRefused = PassengerManager.Instance != null &&
            !PassengerManager.Instance.CanOpenDoorsForStop(currentStopIndex, currentRoute.stops.Length);
        pendingDoorOpenRequest = true;
        PassengerManager.Instance?.AcknowledgeStopRequest();

        ArrivedAtStop(doorsRefused);
        return true;
    }

    public bool TryCloseAndLockDoors()
    {
        if (!isProcessingStop && (PassengerManager.Instance == null || !PassengerManager.Instance.doorsOpen))
            return false;

        earlyDepartureRequested = true;

        if (PassengerManager.Instance != null)
        {
            PassengerManager.Instance.CompletePendingBoardingAndAlighting();
            PassengerManager.Instance.doorsOpen = false;
        }

        if (busController != null)
        {
            busController.RequestKneelingSuspension(false);
            busController.ServiceBrakeInterlock = false;
            busController.brakeInput = 0f;
        }

        if (StopApproachUI.Instance != null && currentRoute != null && currentStopIndex < currentRoute.stops.Length)
        {
            StopApproachUI.Instance.ShowDoorsLocked(currentRoute.stops[currentStopIndex].stopName);
        }

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
        var fuelSystem = FindAnyObjectByType<FuelSystem>();
        var batterySystem = FindAnyObjectByType<BatterySystem>();
        if (MissionEndedAtDepot())
        {
            if (batterySystem != null)
                fuelRefuelCost = batterySystem.AutoRechargeAtDepot();
            else if (fuelSystem != null)
                fuelRefuelCost = fuelSystem.AutoRefuelAtDepot();
        }

        float maintenanceCost = 0f;
        var maintenanceSystem = FindAnyObjectByType<MaintenanceSystem>();
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
        if (BusDepotManager.Instance != null && BusDepotManager.Instance.currentDepot != null && busController != null)
        {
            float dist = Vector3.Distance(busController.transform.position, BusDepotManager.Instance.currentBay1Position);
            if (dist < 30f) return true;
        }

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

        var graph = AIRoadGraph.ResolveCityGraph(busController.transform.position, 2500f);
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

        var graph = busController != null
            ? AIRoadGraph.ResolveCityGraph(busController.transform.position, 2500f)
            : AIRoadGraph.FindCityGraph();
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

        // Never trust serialized pathPoints until they have been checked against
        // the active map. A stale foreign-origin path makes the minimap blank and
        // reports a 13,000 km off-route deviation.
        currentRoute.EnsureWorldPathPoints();
        if (currentRoute.pathPoints != null && currentRoute.pathPoints.Length >= 2 &&
            currentRoute.HasUsableWorldPath())
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
