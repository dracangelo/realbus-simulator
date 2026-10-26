#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

// Runs the actual gameplay scene, including Awake/Start, streaming and physics.
[InitializeOnLoad]
public static class GameplaySmokeCheck
{
    static double started;
    static bool sampled;
    static Vector3 startPosition;
    static float sampledAt;
    static int captureStage;
    static bool introCaptured;
    static bool passed;
    static int lastTelemetrySecond = -1;
    static float peakSpeed;
    static float maxCameraRoll;
    static int highestGear;
    static readonly System.Collections.Generic.List<Transform> depotTrafficVehicles = new System.Collections.Generic.List<Transform>();
    static readonly System.Collections.Generic.List<Vector3> depotTrafficStarts = new System.Collections.Generic.List<Vector3>();
    // Frame samples, collected only while the smoke scene is playing. They make the
    // run report whether a change actually bought frame time instead of asking the
    // next person to eyeball a game view.
    static readonly System.Collections.Generic.List<float> frameTimes = new System.Collections.Generic.List<float>(2048);
    static int sampledFrame = -1;

    // ---- Full-route stop progression (RealBus.SmokeRoute) ---------------------
    // The stop list is run end to end in the real gameplay scene rather than trusted
    // to a timed wait: the bus is walked along the mission's own guidance line to each
    // stop, the docking bay is asked whether it really is at that kerb, the doors are
    // opened through the same entry point the on-screen button uses, and
    // currentStopIndex is required to advance for every single service. The old bug
    // hid exactly here - a service that never finished left the served stop as the
    // navigation target, so the route looked stuck behind the bus.
    const int RoutePhaseBoot = 0;
    const int RoutePhaseTravel = 1;
    const int RoutePhaseDock = 2;
    const int RoutePhaseService = 3;
    const int RoutePhaseSummary = 4;
    static int routePhase;
    static float routePhaseAt;
    static float routeStartedAt;
    static float routeBootAt;
    static int routeStopIndex;
    static int routeServiced;
    static int routeAdvanced;
    static int routeDocked;
    static int routeFailures;
    static int routeGuidanceVersionBeforeService;
    static bool routeCurrentStopDocked;
    static int routeDockAttempts;
    static float routeBusYOffset;
    static float routeBootLogAt;
    static float routeClosestRemaining;
    static int routeStallHops;
    static bool routeLegRescued;
    static int routeShortcuts;
    static int routeHops;
    static float routeTravelLogAt;
    static float routeStopTravelAt;
    // Metres of route covered per hop and the pause between hops. The pause lets the
    // tile/prop streamer and the traffic perception catch up; without it the runner
    // teleports across the whole city between two frames and the streamer thrashes.
    const float RouteHopMeters = 60f;
    const float RouteHopSeconds = .35f;
    // A leg that cannot be closed in this many seconds, or in this many hops that do
    // not get any closer to the bay, is a stuck runner: report the state instead of
    // walking in circles until an external timeout kills the process.
    const float RouteStopTravelBudgetSeconds = 240f;
    // Distance at which the runner stops following the guided line and pulls into
    // the bay. The docking bay is a few metres off the line by construction.
    const float RouteDockApproachMeters = 30f;
    // The route is served in game time, so a headless run fast-forwards it. Every
    // system in the run reads scaled time, so the service is the same, just sooner.
    const float RouteTimeScale = 3f;
    const int RouteStallHopLimit = 90;
    const float RouteOverallBudgetSeconds = 1800f;
    static readonly List<string> routeFailureLog = new List<string>();
    static readonly List<float> routeServiceSeconds = new List<float>();
    static readonly List<Transform> trafficMotionBodies = new List<Transform>();
    static readonly List<Vector3> trafficMotionStarts = new List<Vector3>();
    static float trafficMotionAt = -1f;
    static bool trafficMotionSampled;
    static int trafficMotionMoved;
    static int trafficMotionActive;
    static bool trafficLaneChecked;
    // The lane/side/livery verdict is kept apart from the generic smoke verdict:
    // Run() clears that flag, so folding traffic into it made every route run fail.
    static bool trafficLaneOk = true;
    // Navigator liveness: sampled while the route is active, so a banner or overlay
    // that disappears after the opening leg is a failure rather than an eyeball task.
    static float routeNavigatorAt = -1f;
    static float routeNavigatorReadyAt = -1f;
    static int routeNavigatorChecks;
    static int routeNavigatorFailures;
    static bool routeNavigatorArrows;
    static bool routeNavigatorBeacon;
    static bool routeNavigatorHud;
    static GameplaySmokeCheck() { EditorApplication.update += Tick; }

    public static void Run()
    {
        if (!Application.isBatchMode)
        {
            Debug.LogError("GameplaySmokeCheck is a batch-only check; it exits Unity when finished.");
            return;
        }
        started = 0;
        sampled = false;
        captureStage = 0;
        passed = false;
        EditorSceneManager.OpenScene("Assets/_Game/Scenes/GameScene.unity");
        SessionState.SetBool("RealBus.SmokeCheck", true);
        EditorApplication.EnterPlaymode();
    }

    public static void RunExtended()
    {
        SessionState.SetBool("RealBus.SmokeExtended", true);
        Run();
    }

    public static void RunHandling()
    {
        SessionState.SetBool("RealBus.SmokeHandling", true);
        RunExtended();
    }

    /// <summary>
    /// Runs the whole route headlessly and asserts that the mission leaves each stop.
    /// Batch only; exits Unity when the last stop has been served.
    /// </summary>
    public static void RunRoute()
    {
        if (!Application.isBatchMode)
        {
            Debug.LogError("GameplaySmokeCheck.RunRoute is a batch-only check; it exits Unity when finished.");
            return;
        }
        SessionState.SetBool("RealBus.SmokeRoute", true);
        Run();
    }

    static void SetRouteTimeScale(float scale)
    {
        // fixedDeltaTime is left at its authored value: Unity then takes the same
        // number of physics steps per GAME second, just more of them per real second.
        Time.timeScale = Mathf.Max(.01f, scale);
    }

    public static void RunLiveCity()
    {
        if (!Application.isBatchMode) return;
        SessionState.SetBool("RealBus.SmokeLiveCity", true);
        Run();
    }

    /// <summary>
    /// Pins the time of day and the weather for a run so the environment can be
    /// checked at a known hour (REALBUS_SMOKE_TIME, in minutes past midnight) and
    /// in a known state (REALBUS_SMOKE_WEATHER, a WeatherState name). Both come
    /// from the environment, so this stays a batch-only tool with no effect on a
    /// normal session.
    /// </summary>
    static void ApplyEnvironmentOverrides()
    {
        var clock = TimeOfDaySystem.Instance;
        string timeValue = System.Environment.GetEnvironmentVariable("REALBUS_SMOKE_TIME");
        if (clock != null && !string.IsNullOrEmpty(timeValue) && float.TryParse(timeValue, out float minutes))
        {
            // The schedule would otherwise drag the clock back to the run's own
            // start time on the next frame, and the compressed world day would move
            // the pinned hour during the drive. A smoke check is meant to hold still.
            clock.followScheduleManager = false;
            clock.advanceTimeAutomatically = false;
            clock.currentTimeMinutes = minutes;
            clock.SetTimeOfDay(minutes);
            Debug.Log($"SMOKE override timeOfDay={minutes:F0} minutes");
        }

        var weather = WeatherSystem.Instance;
        string weatherValue = System.Environment.GetEnvironmentVariable("REALBUS_SMOKE_WEATHER");
        if (weather != null && !string.IsNullOrEmpty(weatherValue) &&
            System.Enum.TryParse(weatherValue, true, out WeatherState state))
        {
            weather.forceWeather = true;
            weather.forcedWeatherState = state;
            weather.ApplyWeather(state, 1f, weather.temperature);
            Debug.Log($"SMOKE override weather={state}");
        }
    }

    static void TickLiveCity(double elapsed)
    {
        var city = Object.FindAnyObjectByType<LiveCityStreamer>();
        var bus = MissionManager.Instance?.busController;
        if (captureStage == 0)
        {
            if (city == null || bus == null || city.ActiveCount < 8 || city.WalkerCount == 0)
            {
                if (elapsed < 180) return;
                Debug.LogError($"LIVE CITY startup failed: props={city?.ActiveCount} walkers={city?.WalkerCount}");
                SessionState.SetBool("RealBus.SmokeCheck", false);
                EditorApplication.Exit(2); return;
            }
            passed = city.ActiveCount <= city.maxProps && city.TriangleCount <= city.triangleBudget && city.RoadOverlapCount == 0;
            Debug.Log($"LIVE CITY props={city.ActiveCount} walkers={city.WalkerCount} triangles={city.TriangleCount} roadOverlaps={city.RoadOverlapCount} passed={passed}");
            MobileCameraController.Instance?.SetPreset(BusCameraPreset.Exterior);
            sampledAt = Time.time; captureStage = 1;
        }
        else if (captureStage == 1 && Time.time - sampledAt > 3f)
        {
            CaptureCamera("/tmp/realbus-live-city.png");
            // Teleport only in this transient test scene to prove distant instances unload.
            bus.Teleport(new Vector3(20000, 1, 20000), Quaternion.identity);
            sampledAt = Time.time; captureStage = 2;
        }
        else if (captureStage == 2 && Time.time - sampledAt > 8f)
        {
            passed &= city.ActiveCount == 0 && city.TriangleCount == 0 && city.WalkerCount == 0;
            Debug.Log($"LIVE CITY unload props={city.ActiveCount} triangles={city.TriangleCount} passed={passed}");
            SessionState.SetBool("RealBus.SmokeCheck", false);
            SessionState.SetBool("RealBus.SmokeLiveCity", false);
            LogPerformance();
            EditorApplication.Exit(passed ? 0 : 2);
        }
    }

    static void PrepareHandlingCourse(BusController bus)
    {
        // Isolate drivetrain from route docking assists and road curvature.
        // Only the transient Play Mode scene is changed; no assets/settings saved.
        foreach (var tiles in Object.FindObjectsByType<MapTileLoader>())
        { tiles.StopAllCoroutines(); tiles.enabled = false; }
        var buildings = OSMBuildingMeshBuilder.Instance;
        if (buildings != null) { buildings.StopAllCoroutines(); buildings.enabled = false; }
        foreach (var assist in bus.GetComponents<DrivingAssistSystem>()) assist.enabled = false;
        GameObject surface = GameObject.CreatePrimitive(PrimitiveType.Cube);
        surface.name = "SMOKE temporary closed handling course";
        surface.transform.position = new Vector3(20000, -.5f, 20000);
        surface.transform.localScale = new Vector3(250, 1, 1500);
        bus.Teleport(new Vector3(20000, 1.2f, 19400), Quaternion.identity);
        bus.SetParkingBrake(false);
        bus.RequestKneelingSuspension(false);
        bus.ServiceBrakeInterlock = false;
        Debug.Log("SMOKE closed course: production bus and mobile pedal; route assists disabled for drivetrain isolation.");
    }

    static void Tick()
    {
        if (!SessionState.GetBool("RealBus.SmokeCheck", false) || !EditorApplication.isPlaying) return;
        if (Time.frameCount != sampledFrame)
        {
            sampledFrame = Time.frameCount;
            float frameTime = Time.unscaledDeltaTime;
            // Skip the first second: loading, shader compilation and the initial
            // streamed build are not the steady state this measures.
            if (Time.time > 1f && frameTime > 0f && frameTime < 1f) frameTimes.Add(frameTime);
        }
        if (started == 0) started = EditorApplication.timeSinceStartup;
        double elapsed = EditorApplication.timeSinceStartup - started;
        CaptureCinematicIntroOnce();
        if (SessionState.GetBool("RealBus.SmokeRoute", false)) { TickRoute(elapsed); return; }
        if (SessionState.GetBool("RealBus.SmokeLiveCity", false)) { TickLiveCity(elapsed); return; }
        if (elapsed > 240)
        {
            Debug.LogError("SMOKE timed out before completing the driving/camera check.");
            SessionState.SetBool("RealBus.SmokeCheck", false);
            LogPerformance();
            EditorApplication.Exit(2);
            return;
        }
        var bus = MissionManager.Instance != null ? MissionManager.Instance.busController : Object.FindAnyObjectByType<BusController>();
        bool ready = bus != null && !bus.ServiceBrakeInterlock &&
            MissionManager.Instance != null && MissionManager.Instance.routeActive &&
            OSMBuildingMeshBuilder.Instance != null && OSMBuildingMeshBuilder.Instance.buildingsBuilt;
        if (!sampled && !ready)
        {
            if (elapsed < 180) return;
            Debug.LogError($"SMOKE startup timeout: mission={MissionManager.Instance?.missionState} interlock={bus?.ServiceBrakeInterlock}");
            SessionState.SetBool("RealBus.SmokeCheck", false);
            EditorApplication.Exit(2);
            return;
        }
        if (!sampled)
        {
            ApplyEnvironmentOverrides();
            peakSpeed = 0f; maxCameraRoll = 0f; highestGear = 0; lastTelemetrySecond = -1;
            sampled = true;
            sampledAt = Time.time;
            Debug.Log($"SMOKE city={CityManager.Instance?.activeCity?.cityCode} gps={GPSManager.Instance != null} roads={OSMLoader.Instance?.dataLoaded}/{OSMRoadMeshBuilder.Instance?.roadsBuilt} buildings={OSMBuildingLoader.Instance?.dataLoaded}/{OSMBuildingMeshBuilder.Instance?.buildingsBuilt} mission={MissionManager.Instance?.missionState}");
            foreach (var b in Object.FindObjectsByType<BusController>(FindObjectsInactive.Include))
                Debug.Log($"SMOKE bus={b.name} active={b.gameObject.activeInHierarchy} engine={b.engineData != null} transmission={b.transmissionData != null} pos={b.transform.position} scale={b.transform.lossyScale} renderers={b.GetComponentsInChildren<Renderer>().Length} interlock={b.ServiceBrakeInterlock} fuelEmpty={b.FuelDepleted} park={b.ParkingBrakeActive}");
            foreach (var cam in Object.FindObjectsByType<Camera>())
                Debug.Log($"SMOKE camera={cam.name} parent={cam.transform.parent?.name} enabled={cam.enabled} local={cam.transform.localPosition} targetTexture={cam.targetTexture != null}");
            // Remember where the yard's traffic started, so the report can tell
            // "depot traffic exists" from "depot traffic drives".
            depotTrafficVehicles.Clear(); depotTrafficStarts.Clear();
            var depotForTraffic = BusDepotManager.Instance != null ? BusDepotManager.Instance.currentTraffic : null;
            if (depotForTraffic != null)
                foreach (Transform child in depotForTraffic.transform)
                    if (child.GetComponent<SplineVehicle>() != null)
                    {
                        depotTrafficVehicles.Add(child);
                        depotTrafficStarts.Add(child.position);
                    }
            if (bus != null)
            {
                if (SessionState.GetBool("RealBus.SmokeHandling", false)) PrepareHandlingCourse(bus);
                startPosition = bus.transform.position;
                foreach (var w in bus.allWheels ?? new WheelCollider[0])
                    if (w != null) Debug.Log($"SMOKE wheel={w.name} grounded={w.isGrounded} position={w.transform.position} torque={w.motorTorque} brake={w.brakeTorque} active={w.gameObject.activeInHierarchy} attached={w.attachedRigidbody?.name} playerBody={w.attachedRigidbody == bus.Rigidbody} parent={w.transform.parent?.name}");
                foreach (var r in bus.GetComponentsInChildren<Renderer>().Take(10))
                    Debug.Log($"SMOKE renderer={r.name} bounds={r.bounds} materials={string.Join(",", r.sharedMaterials.Select(m => m == null ? "null" : m.name + ":" + m.shader.name))}");
                var controls = Object.FindAnyObjectByType<MobileControlsUI>();
                if (controls != null && controls.accelerateButton != null && EventSystem.current != null)
                {
                    ExecuteEvents.Execute(controls.accelerateButton.gameObject,
                        new PointerEventData(EventSystem.current) { pointerId = 10 }, ExecuteEvents.pointerDownHandler);
                    Debug.Log("SMOKE acceleration source=mobile pedal pointer-down");
                }
                else Debug.LogError("SMOKE missing mobile acceleration control or EventSystem");
                MobileCameraController.Instance?.SetPreset(BusCameraPreset.Exterior);
            }
        }
        bool extended = SessionState.GetBool("RealBus.SmokeExtended", false);
        float driveDuration = extended ? 20f : 6f;
        peakSpeed = Mathf.Max(peakSpeed, bus.currentSpeedKmh);
        highestGear = Mathf.Max(highestGear, bus.transmissionData.currentGear);
        if (Camera.main != null) maxCameraRoll = Mathf.Max(maxCameraRoll, Mathf.Abs(Mathf.DeltaAngle(0f, Camera.main.transform.eulerAngles.z)));
        int second = Mathf.FloorToInt(Time.time - sampledAt);
        if (extended && second != lastTelemetrySecond)
        {
            lastTelemetrySecond = second;
            Debug.Log($"SMOKE telemetry t={second} speed={bus.currentSpeedKmh:F1} rpm={bus.currentRPM:F0} gear={bus.transmissionData.currentGear} automatic={bus.transmissionData.automatic} throttle={bus.throttleInput} assist={bus.AssistThrottleLimit:F2} brake={bus.brakeInput}/{bus.AssistBrakeFloor:F2} tilt={Vector3.Angle(bus.transform.up, Vector3.up):F1}");
        }
        if (Time.time - sampledAt < driveDuration) return;
        if (captureStage == 0)
        {
        float moved = bus != null ? Vector3.Distance(bus.transform.position, startPosition) : 0;
        Debug.Log($"SMOKE driveDistance={moved:F2} speed={bus?.currentSpeedKmh} throttle={bus?.throttleInput} assist={bus?.AssistThrottleLimit} brake={bus?.AssistBrakeFloor}");
        var infill = Object.FindAnyObjectByType<ProceduralBuildingFill>();
        var mappedBuilder = Object.FindAnyObjectByType<OSMBuildingMeshBuilder>();
        Debug.Log($"SMOKE trafficSide={(CityManager.Instance?.activeCountry?.DrivesOnLeft == true ? "left" : "right")} syntheticBuildings={infill?.ActiveCount} importedBuildings={infill?.ImportedCount} mappedBuildings={mappedBuilder?.ActiveBuildingCount} importedMapped={mappedBuilder?.ImportedMappedCount} importedMappedTriangles={mappedBuilder?.ImportedMappedTriangles}");
        passed &= CheckTraffic();
        if (bus != null)
        {
            Debug.Log($"SMOKE physics position={bus.transform.position} kinematic={bus.Rigidbody.isKinematic} constraints={bus.Rigidbody.constraints} rpm={bus.currentRPM} gear={bus.transmissionData.currentGear} brakeInput={bus.brakeInput} com={bus.Rigidbody.centerOfMass} steer={bus.steerInput}");
            foreach (var w in bus.allWheels)
            {
                w.GetGroundHit(out WheelHit hit);
                Debug.Log($"SMOKE drivenWheel={w.name} grounded={w.isGrounded} surface={hit.collider?.name} radius={w.radius} suspension={w.suspensionDistance} torque={w.motorTorque} brake={w.brakeTorque} rpm={w.rpm}");
                Debug.Log($"SMOKE suspension={w.name} enabled={w.enabled} sprungMass={w.sprungMass} spring={w.suspensionSpring.spring} target={w.suspensionSpring.targetPosition} scale={w.transform.lossyScale} up={w.transform.up}");
                foreach (var ground in Physics.RaycastAll(w.transform.position + Vector3.up * 3f, Vector3.down, 6f, ~0, QueryTriggerInteraction.Ignore))
                    if (!ground.transform.IsChildOf(bus.transform)) Debug.Log($"SMOKE groundUnder={w.name} collider={ground.collider.name} y={ground.point.y} normal={ground.normal}");
            }
            foreach (var collider in Physics.OverlapBox(bus.transform.position + Vector3.up * 1.5f, new Vector3(2f, 2f, 7f), bus.transform.rotation, ~0, QueryTriggerInteraction.Ignore))
                Debug.Log($"SMOKE overlap={collider.name} parent={collider.transform.parent?.name} bounds={collider.bounds} busPart={collider.transform.IsChildOf(bus.transform)}");
        }
        int screenCameras = Object.FindObjectsByType<Camera>().Count(c => c.enabled && c.targetTexture == null);
        passed = moved > 1 && screenCameras == 1 && OSMLoader.Instance != null && OSMLoader.Instance.dataLoaded;
        if (extended) passed &= peakSpeed > 40f && highestGear > 0 && maxCameraRoll < 1f;
        Debug.Log($"SMOKE peakSpeed={peakSpeed:F1} highestGear={highestGear} maxCameraRoll={maxCameraRoll:F2}");
        Debug.Log($"SMOKE screenCameras={screenCameras} passed={passed}");
        var hud = HUDManager.Instance;
        Debug.Log($"SMOKE hud={(hud != null)} bound={hud?.IsBound} visible={hud?.IsVisible}");
        var roadIndex = OSMRoadMeshBuilder.Instance?.SurfaceIndex;
        int stopModels = 0, blockedStops = 0, crossingKerbs = 0;
        if (roadIndex != null)
        {
            foreach (var stop in Object.FindObjectsByType<StopPropSpawner>())
                foreach (Transform anchor in stop.transform)
                    foreach (Transform model in anchor)
                    {
                        var renderers = model.GetComponentsInChildren<MeshRenderer>();
                        if (renderers.Length == 0) continue;
                        Bounds bounds = renderers[0].bounds;
                        foreach (var renderer in renderers) bounds.Encapsulate(renderer.bounds);
                        stopModels++;
                        var footprint = new[] { new Vector3(bounds.min.x, 0, bounds.min.z), new Vector3(bounds.max.x, 0, bounds.min.z),
                            new Vector3(bounds.max.x, 0, bounds.max.z), new Vector3(bounds.min.x, 0, bounds.max.z) };
                        if (roadIndex.OverlapsFootprint(footprint, .5f))
                        {
                            blockedStops++;
                            // Naming the offender makes the count actionable: a shelter
                            // or totem standing in a lane is a placement bug with a
                            // location, not just a number.
                            Debug.Log($"SMOKE blockedStop model={model.name} anchor={anchor.name} position={new Vector3(bounds.center.x, 0f, bounds.center.z)}");
                        }
                    }
            foreach (var collider in Object.FindObjectsByType<BoxCollider>())
            {
                var road = collider.transform.parent?.parent;
                if (collider.transform.parent?.name != "PhysicalRoadBoundaries" || road == null) continue;
                var pieces = road.name.Split('_');
                if (pieces.Length > 1 && long.TryParse(pieces[1], out long wayId) && roadIndex.Contains(collider.bounds.center, .2f, wayId)) crossingKerbs++;
            }
        }
        // A stop shelter must stand on the kerb the player drives past: to the left
        // of travel in a left-hand-traffic city, to the right in right-hand traffic.
        // Measure that against the DRIVEN path, not against the shelter's own
        // rotation: a reversed heading otherwise makes the placement and the probe
        // agree with each other while both point at the wrong kerb.
        int stopAnchors = 0, stopsWrongSide = 0, stopsBackwards = 0, stopsUnmeasured = 0;
        bool drivesOnLeft = RoadSpawnResolver.ResolveCountry()?.DrivesOnLeft == true;
        var stopRoute = MissionManager.Instance != null ? MissionManager.Instance.currentRoute : null;
        Vector3[] stopPath = stopRoute != null && stopRoute.EnsureWorldPathPoints()
            ? stopRoute.pathPoints : null;
        if (stopPath == null)
            Debug.Log("SMOKE stopSides no driven path; the kerb side cannot be judged");
        if (roadIndex != null)
        {
            foreach (var spawner in Object.FindObjectsByType<StopPropSpawner>())
                foreach (Transform anchor in spawner.transform)
                {
                    if (anchor.GetComponent<StopTrigger>() == null) continue;
                    stopAnchors++;
                    // An anchor that cannot be measured is a failure, not a pass: a
                    // missing path used to skip every stop and look clean.
                    if (!RoadSpawnResolver.NearestPathFrame(stopPath, anchor.position,
                            out Vector3 pathPoint, out Vector3 pathTangent))
                    {
                        stopsUnmeasured++;
                        continue;
                    }

                    Vector3 kerbSide = RoadSpawnResolver.KerbSide(pathTangent, drivesOnLeft);
                    if (kerbSide.sqrMagnitude < .5f)
                    {
                        stopsUnmeasured++;
                        continue;
                    }

                    // The shelter is the largest renderer under the anchor; decorations
                    // and the docking outline are smaller. Measure its bounds centre,
                    // because a mesh origin can sit at the anchor while its body is
                    // metres onto the pavement.
                    Renderer model = null;
                    float bestVolume = 0f;
                    foreach (var renderer in anchor.GetComponentsInChildren<Renderer>())
                    {
                        Vector3 size = renderer.bounds.size;
                        float volume = size.x * size.y * size.z;
                        if (volume <= bestVolume) continue;
                        bestVolume = volume;
                        model = renderer;
                    }
                    Vector3 sample = model != null ? model.bounds.center : anchor.position;
                    float lateral = Vector3.Dot(sample - pathPoint, kerbSide);
                    float facing = Vector3.Dot(anchor.forward, pathTangent);
                    if (facing < 0.5f) stopsBackwards++;
                    // On the correct kerb the shelter stands beyond the path on the
                    // kerb side; a negative lateral puts it across the carriageway.
                    if (lateral < 0.5f)
                    {
                        stopsWrongSide++;
                        Debug.Log($"SMOKE stopWrongSide anchor={anchor.name} lateral={lateral:F2} " +
                            $"facing={facing:F2} position={new Vector3(anchor.position.x, 0f, anchor.position.z)}");
                    }
                }
        }
        bool stopSidesOk = stopAnchors > 0 && stopsWrongSide == 0 && stopsBackwards == 0 && stopsUnmeasured == 0;
        passed &= stopSidesOk;
        Debug.Log($"SMOKE stopSides anchors={stopAnchors} wrongSide={stopsWrongSide} " +
            $"backwards={stopsBackwards} unmeasured={stopsUnmeasured} leftTraffic={drivesOnLeft} passed={stopSidesOk}");

        // Depot geometry must sit on its own yard (and its driveways), never on the
        // city carriageway. The audit measures each prop's real footprint and skips
        // the yard surface itself plus the driveway/kerb join, which is how the depot
        // legitimately meets the street.
        var depotManager = BusDepotManager.Instance;
        var depotOffenders = depotManager != null
            ? depotManager.DepotPropsReachingStreet()
            : new System.Collections.Generic.List<string>();
        foreach (string offender in depotOffenders)
            Debug.Log($"SMOKE depotOnRoad {offender}");
        passed &= depotOffenders.Count == 0;
        Debug.Log($"SMOKE depotStreet props={depotOffenders.Count} passed={depotOffenders.Count == 0}");

        passed &= stopModels > 0 && blockedStops == 0 && crossingKerbs == 0;
        Debug.Log($"SMOKE roadClearance stopModels={stopModels} blockedStops={blockedStops} crossingKerbs={crossingKerbs} passed={passed}");

        // The environment has to be built, not merely configured: a weather state
        // the HUD reports is worthless if nothing draws clouds, rain and a sun to
        // light them, which is exactly how the sky used to be missing entirely.
        var sky = SkyEnvironmentController.Instance;
        var rain = Object.FindAnyObjectByType<RainController>();
        var snow = Object.FindAnyObjectByType<SnowController>();
        var fog = Object.FindAnyObjectByType<FogController>();
        var weatherFog = FogController.Instance;
        var haze = CinematicAtmosphereController.Instance;
        var weather = WeatherSystem.Instance;
        var clock = TimeOfDaySystem.Instance;
        var wind = WindSystem.Instance;
        var grade = Object.FindAnyObjectByType<CinematicColorGrade>();
        Light dayLight = RenderSettings.sun != null ? RenderSettings.sun : FindDirectionalSun();
        float coverage = sky != null ? sky.cloudCoverage : 0f;
        bool cloudsBuilt = sky != null && sky.transform.Find("SkyEnvironment_Domes/SkyEnvironment_Clouds") != null;
        bool starsBuilt = sky != null && sky.transform.Find("SkyEnvironment_Domes/SkyEnvironment_Stars") != null;
        bool rainBuilt = rain != null && rain.rainParticles != null;
        bool snowBuilt = snow != null && snow.snowParticles != null;
        // Built is not the same as falling: the precipitation a state claims has to
        // actually be emitting, and clear weather has to actually stop.
        float rainRate = Mathf.Max(EmissionRate(rain != null ? rain.drizzleParticles : null),
            Mathf.Max(EmissionRate(rain != null ? rain.rainParticles : null),
                EmissionRate(rain != null ? rain.thunderstormParticles : null)));
        float snowRate = Mathf.Max(EmissionRate(snow != null ? snow.snowParticles : null),
            EmissionRate(snow != null ? snow.blizzardParticles : null));
        WeatherState state = weather != null ? weather.currentWeather : WeatherState.Clear;
        bool wetWeather = state == WeatherState.Drizzle || state == WeatherState.LightRain ||
            state == WeatherState.HeavyRain || state == WeatherState.Thunderstorm;
        bool frozenWeather = state == WeatherState.Snow || state == WeatherState.Blizzard;
        Debug.Log($"SMOKE environment weather={(weather != null ? weather.currentWeather.ToString() : "none")} " +
            $"intensity={(weather != null ? weather.weatherIntensity : 0f):F2} skybox={(RenderSettings.skybox != null ? RenderSettings.skybox.name : "none")} " +
            $"cloudsBuilt={cloudsBuilt} starsBuilt={starsBuilt} coverage={(sky != null ? sky.cloudCoverage : 0f):F2} " +
            $"stars={(sky != null ? sky.starVisibility : 0f):F2} moonIntensity={(sky != null ? sky.currentMoonIntensity : 0f):F3} " +
            $"sunElevation={(clock != null ? clock.SunElevationDegrees : 0f):F1} night={(clock != null && clock.IsNight)} " +
            $"fog={RenderSettings.fog} fogStart={RenderSettings.fogStartDistance:F0} fogEnd={RenderSettings.fogEndDistance:F0} fogColor={RenderSettings.fogColor} " +
            $"hazeColor={(haze != null ? haze.TintedHazeColor().ToString() : "none")} hazeOwnsFog={(haze != null && haze.HazeMayOwnFogNow())} " +
            $"weatherFogActive={(weatherFog != null && weatherFog.WeatherFogActive)} daylight={(clock != null ? clock.GetDaylightFactor() : 0f):F2} " +
            $"skyTint={(clock != null ? clock.GetSkyTint().ToString() : "none")} " +
            $"rainBuilt={rainBuilt} snowBuilt={snowBuilt} rainRate={rainRate:F0} snowRate={snowRate:F0} fogController={(fog != null)} " +
            $"wind={(wind != null ? wind.SpeedKmh : 0f):F1}kmh windDir={(wind != null ? wind.DirectionDegrees : 0f):F0} gust={(wind != null ? wind.Gust01 : 0f):F2} " +
            $"sunTint={(clock != null ? clock.GetSunTint().ToString() : "none")} sunVisibility={(clock != null ? clock.GetSunVisibilityFactor() : 0f):F2} " +
            $"sunIntensity={(dayLight != null ? dayLight.intensity : 0f):F3} shadowStrength={(dayLight != null ? dayLight.shadowStrength : 0f):F2} " +
            $"ambientMode={RenderSettings.ambientMode} ambientIntensity={RenderSettings.ambientIntensity:F2} " +
            $"reflectionIntensity={RenderSettings.reflectionIntensity:F2} fogTarget={(weatherFog != null ? weatherFog.TargetFogColor.ToString() : "none")} " +
            $"grade={(grade != null && grade.enabled)} gradeExposure={(grade != null ? grade.CurrentExposure : 0f):F3} " +
            $"gradeContrast={(grade != null ? grade.CurrentContrast : 0f):F3} gradeSaturation={(grade != null ? grade.CurrentSaturation : 0f):F3} " +
            $"gradeTint={(grade != null ? grade.CurrentTint.ToString() : "none")}");
        passed &= sky != null && RenderSettings.skybox != null && cloudsBuilt && starsBuilt;
        passed &= rainBuilt && snowBuilt && fog != null && clock != null && weather != null && wind != null;
        // The grade is part of the cinematic look now; if it failed to attach, the
        // scene silently falls back to an ungraded frame. Very Low deliberately
        // disables it, so only assert on the tiers that keep it.
        if (QualitySettings.GetQualityLevel() > 0)
            passed &= grade != null && grade.enabled;
        passed &= wetWeather ? rainRate > 0f : rainRate <= 0f;
        passed &= frozenWeather ? snowRate > 0f : snowRate <= 0f;

        // The light has to agree with the sky the game just reported. A storm that
        // still casts a crisp shadow or mirrors a blue sky is the specific bug this
        // check exists to catch, and it is invisible in a still frame.
        if (dayLight != null)
        {
            if (coverage > 0.85f)
            {
                passed &= dayLight.shadowStrength < 0.5f;
                passed &= RenderSettings.reflectionIntensity < 0.4f;
                Debug.Log("SMOKE lighting heavyOvercast=True");
            }
            else if (coverage < 0.2f)
            {
                passed &= dayLight.shadowStrength > 0.5f;
                // The floor is the night reflection (0.20), not the day one: this
                // branch runs for a clear sky at any hour.
                passed &= RenderSettings.reflectionIntensity > 0.1f;
                Debug.Log("SMOKE lighting heavyOvercast=False");
            }
        }


        // The depot is the other half of "the bus can leave": it has to own its
        // lanes (no prop standing in them, traffic on them) and the bus has to end
        // up outside the yard it was spawned in.
        var depot = BusDepotManager.Instance;
        if (depot != null && depot.currentDepot != null)
        {
            var depotTraffic = depot.currentTraffic;
            var offenders = depot.PropsStandingInYardLanes();
            bool leftYard = !BusDepotManager.IsInsideDepot(bus.transform.position);
            float trafficMoved = 0f;
            for (int i = 0; i < depotTrafficVehicles.Count; i++)
            {
                if (depotTrafficVehicles[i] == null) continue;
                trafficMoved = Mathf.Max(trafficMoved, Vector3.Distance(depotTrafficVehicles[i].position, depotTrafficStarts[i]));
            }
            Debug.Log($"SMOKE depot traffic={(depotTraffic != null ? depotTraffic.VehicleCount : 0)} " +
                $"holding={(depotTraffic != null && depotTraffic.HoldingStreetTraffic)} propsInLanes={offenders.Count} " +
                $"trafficMoved={trafficMoved:F2} departed={depot.hasDepartedDepot} leftYard={leftYard} moved={moved:F2}");
            foreach (var offender in offenders) Debug.Log($"SMOKE depotPropInLane={offender}");
            passed &= offenders.Count == 0;
            passed &= depotTraffic != null && depotTraffic.VehicleCount > 0;
            passed &= trafficMoved > 5f;   // spawned traffic that never drives is not traffic
            // Six seconds is not always enough to cross a 28 m yard; the extended
            // run is where leaving the depot can be asserted rather than logged.
            if (extended) passed &= leftYard;
        }
        CaptureCamera("/tmp/realbus-exterior.png");
        captureStage = 1;
        return;
        }
        if (Time.time - sampledAt < driveDuration + 1) return;
        if (captureStage == 1)
        {
            MobileCameraController.Instance?.SetPreset(BusCameraPreset.Cockpit);
            captureStage = 2;
            return;
        }
        if (Time.time - sampledAt < driveDuration + 2) return;
        if (captureStage == 2)
        {
            int cockpitScreenCameras = Object.FindObjectsByType<Camera>().Count(c => c.enabled && c.targetTexture == null);
            bool inset = GameObject.Find("RearViewCanvas") != null;
            passed &= cockpitScreenCameras == 1 && !inset;
            Debug.Log($"SMOKE cockpit screenCameras={cockpitScreenCameras} inset={inset} passed={passed}");
            CaptureCamera("/tmp/realbus-cockpit.png");
            captureStage = 3;
            return;
        }
        if (Time.time - sampledAt < driveDuration + 3) return;
        SessionState.SetBool("RealBus.SmokeCheck", false);
        SessionState.SetBool("RealBus.SmokeExtended", false);
        SessionState.SetBool("RealBus.SmokeHandling", false);
        LogPerformance();
        EditorApplication.Exit(passed ? 0 : 2);
    }

    /// <summary>
    /// Traffic has to be textured, varied and on the road, not merely present in a pool.
    /// This measures what a driver sees: how many imported models the pool can spawn,
    /// whether their materials carry the pack's texture, and how far each active vehicle
    /// sits from the lane its own graph link assigns it.
    /// </summary>
    static bool CheckTraffic()
    {
        var pool = Object.FindAnyObjectByType<VehiclePool>();
        if (pool == null)
        {
            Debug.LogError("SMOKE traffic: no VehiclePool in the scene");
            return false;
        }

        pool.GetTierCounts(out int fullAi, out int spline, out int dormant, out int densityEnabled);
        var models = new System.Collections.Generic.List<VehiclePool.VehiclePrefabEntry>();
        var seen = new System.Collections.Generic.HashSet<GameObject>();
        if (pool.prefabs != null)
            foreach (var entry in pool.prefabs)
                if (entry != null && entry.prefab != null && seen.Add(entry.prefab)) models.Add(entry);

        // The body is the renderer carrying the most vertices; it must be either
        // textured from the pack or deliberately painted, never left as the flat
        // white/error material an unmaterialised import produces.
        int untextured = 0;
        foreach (var entry in models)
        {
            Renderer body = null;
            int bestVertices = -1;
            foreach (var renderer in entry.prefab.GetComponentsInChildren<Renderer>(true))
            {
                var filter = renderer.GetComponent<MeshFilter>();
                int vertices = filter != null && filter.sharedMesh != null ? filter.sharedMesh.vertexCount : 0;
                if (vertices <= bestVertices) continue;
                bestVertices = vertices;
                body = renderer;
            }
            var material = body != null ? body.sharedMaterial : null;
            bool materialised = material != null && material.shader != null &&
                (material.mainTexture != null ||
                 Mathf.Min(material.color.r, Mathf.Min(material.color.g, material.color.b)) < .75f);
            if (!materialised)
            {
                untextured++;
                Debug.LogWarning($"SMOKE traffic model '{entry.prefab.name}' has no texture or body paint on its largest part.");
            }
        }

        var playerBus = MissionManager.Instance != null ? MissionManager.Instance.busController : Object.FindAnyObjectByType<BusController>();
        int pooledCount = 0, atOrigin = 0;
        float nearest = float.MaxValue, farthest = 0f;
        foreach (Transform child in pool.transform)
        {
            if (child.GetComponent<AIVehicleController>() == null) continue;
            pooledCount++;
            if (child.position.sqrMagnitude < .01f) atOrigin++;
            // Dormant bodies are inactive and deliberately left where they last
            // drove, which can be kilometres behind the bus. The distance range is
            // only meaningful for the cars actually on the road.
            if (playerBus == null || !child.gameObject.activeSelf) continue;
            float distance = Vector3.Distance(child.position, playerBus.transform.position);
            nearest = Mathf.Min(nearest, distance);
            farthest = Mathf.Max(farthest, distance);
        }

        // Liveries are per-instance property blocks, so the only honest check is to
        // read them back off the pooled vehicles: one prefab per model means the
        // prefab alone cannot show whether the road has more than one paint on it.
        var liveryColors = new System.Collections.Generic.HashSet<Color>();
        int liveryPainted = 0;
        foreach (Transform child in pool.transform)
        {
            if (child.GetComponent<AIVehicleController>() == null) continue;
            Renderer body = null;
            int bestVertices = -1;
            foreach (var renderer in child.GetComponentsInChildren<Renderer>(true))
            {
                var filter = renderer.GetComponent<MeshFilter>();
                int vertices = filter != null && filter.sharedMesh != null ? filter.sharedMesh.vertexCount : 0;
                if (vertices <= bestVertices) continue;
                bestVertices = vertices;
                body = renderer;
            }
            if (body == null) continue;

            Material[] materials = body.sharedMaterials;
            for (int m = 0; m < materials.Length; m++)
            {
                if (!TrafficLivery.IsBodyPaint(materials[m])) continue;
                var block = new MaterialPropertyBlock();
                body.GetPropertyBlock(block, m);
                Color paint = block.GetColor("_Color");
                if (paint.a > 0.5f) { liveryPainted++; liveryColors.Add(paint); }
                break;
            }
        }
        bool liveriesOk = liveryColors.Count >= 3;

        int laneSamples = 0, steadySamples = 0, offLane = 0;
        float worstDeviation = 0f, worstSteady = 0f;
        string worstVehicle = string.Empty;
        foreach (var ai in Object.FindObjectsByType<AIVehicleController>(FindObjectsInactive.Exclude))
        {
            // A spline-tier vehicle still carries the disabled controller, whose
            // indices stop matching the pose the spline drives it to.
            if (!ai.isActiveAndEnabled) continue;
            // Overtaking, yielding and stuck recovery deliberately leave the lane line.
            if (ai.IsChangingLane) continue;
            var graph = ai.roadGraph;
            if (graph == null || !graph.IsValidNode(ai.currentNodeIndex) || !graph.IsValidNode(ai.targetNodeIndex)) continue;
            Vector3 from = graph.GetNodePosition(ai.currentNodeIndex);
            Vector3 segment = graph.GetNodePosition(ai.targetNodeIndex) - from;
            segment.y = 0f;
            if (segment.sqrMagnitude < .01f) continue;
            float segmentLength = segment.magnitude;
            segment.Normalize();
            float expected = graph.GetLaneOffset(ai.currentNodeIndex, ai.targetNodeIndex, ai.laneIndex);
            float actual = Vector3.Dot(ai.transform.position - from, Vector3.Cross(Vector3.up, segment));
            float deviation = Mathf.Abs(actual - expected);
            laneSamples++;
            if (deviation > worstDeviation)
            {
                worstDeviation = deviation;
                worstVehicle = ai.name;
            }

            // A body crossing a junction is briefly wide of the new lane line; lane
            // keeping is judged on the part of the link where the wheels should be
            // settled, not on the turn itself.
            float along = Vector3.Dot(ai.transform.position - from, segment);
            // A lane change or a junction cut needs room to resolve. A body within
            // 8 m of either end is still turning, not failing to hold its lane, so
            // lane keeping is judged on the settled middle of the link.
            float junctionMargin = Mathf.Max(ai.vehicleLength * 1.5f, 8f);
            if (along < junctionMargin || segmentLength - along < junctionMargin) continue;
            steadySamples++;
            worstSteady = Mathf.Max(worstSteady, deviation);
            if (deviation > 1.2f) offLane++;
            if (deviation > .8f)
            {
                var dbgRb = ai.GetComponent<Rigidbody>();
                var dbgSpline = ai.GetComponent<SplineVehicle>();
                Debug.Log($"SMOKE traffic lane sample {deviation:F2}m on {ai.name} along {along:F1}/{segmentLength:F1}m " +
                    $"node {ai.currentNodeIndex}->{ai.targetNodeIndex} lane {ai.laneIndex}/{graph.GetLaneCount(ai.currentNodeIndex, ai.targetNodeIndex)} " +
                    $"expected {expected:F2} actual {actual:F2} laneErr {ai.LateralLaneError:F2} speed {ai.currentSpeedKmh:F0} " +
                    $"kin={(dbgRb != null && dbgRb.isKinematic)} rbV={(dbgRb != null ? dbgRb.linearVelocity.magnitude * 3.6f : 0f):F0} " +
                    $"splineOn={(dbgSpline != null && dbgSpline.enabled)} fullOn={ai.enabled} " +
                    $"oneWay={graph.IsOneWay(ai.currentNodeIndex, ai.targetNodeIndex)} shared={graph.IsSharedLane(ai.currentNodeIndex, ai.targetNodeIndex)} " +
                    $"halfW={graph.GetRoadHalfWidth(ai.currentNodeIndex, ai.targetNodeIndex):F2} laneW={graph.GetLaneWidth(ai.currentNodeIndex, ai.targetNodeIndex):F2}");
            }
        }

        // No active vehicle at all is a failure: a pool that never leaves its dormant
        // tier is the bug that made traffic look like two parked cars. A recovery nudge
        // or a shove from the car ahead can move one body off its line for a moment, so
        // up to a third of the steady samples may be wide, but never by more than a
        // lane width's worth.
        //
        // When fewer than three bodies are in the settled middle of a link, the only
        // samples left are junction crossings, which are allowed to be wide: a car
        // turning through a junction is not failing to hold a lane. Judge those
        // against a lane width, not the tighter settled-line tolerance.
        float laneWidth = pool.roadGraph != null ? pool.roadGraph.MinimumLaneWidth : 3f;
        bool laneOk = steadySamples >= 3
            ? offLane <= Mathf.Max(1, steadySamples / 3) && worstSteady <= 2.5f
            : laneSamples > 0 && worstDeviation <= Mathf.Max(3f, laneWidth);
        bool texturedOk = untextured == 0;
        bool variedOk = models.Count >= 3;

        // A lane probe that compares a car with the lane its own graph computed cannot
        // see a flipped side: graph, placement and probe agree while all three point at
        // the oncoming carriageway. Judge the side against the COUNTRY's driving side,
        // and measure the visible body centre as well as the physics line, so an
        // off-centre imported pivot is neither hidden by a correct transform nor
        // mistaken for a lane bug.
        int sideSamples = 0, sideWrong = 0, sideReversed = 0, pivotOffsets = 0;
        int skipChanging = 0, skipInvalid = 0, skipOneWay = 0, skipMargin = 0, skipOffLane = 0;
        float worstPivot = 0f;
        var country = RoadSpawnResolver.ResolveCountry();
        if (country != null)
        {
            float kerbSign = country.DrivesOnLeft ? -1f : 1f;
            void AuditSide(string tier, AIRoadGraph graph, int from, int to, int lane,
                Transform root, bool changing, float margin, float speedKmh)
            {
                if (changing || root == null) { skipChanging++; return; }
                if (graph == null || !graph.IsValidNode(from) || !graph.IsValidNode(to)) { skipInvalid++; return; }
                int lanes = graph.GetLaneCount(from, to);
                // A one-way link has no oncoming half to be wrong about, and a shared
                // single-lane two-way street legitimately puts both flows on the middle.
                if (graph.IsOneWay(from, to) || graph.IsSharedLane(from, to)) { skipOneWay++; return; }
                Vector3 fromPos = graph.GetNodePosition(from);
                Vector3 segment = graph.GetNodePosition(to) - fromPos;
                segment.y = 0f;
                float length = segment.magnitude;
                if (length < 1f) { skipInvalid++; return; }
                segment /= length;
                float along = Vector3.Dot(root.position - fromPos, segment);
                float facing = Vector3.Dot(root.forward, segment);
                if (facing < .5f)
                {
                    sideReversed++;
                    Debug.Log($"SMOKE sideReversed {tier} {root.name} dot={facing:F2} lane={lane}/{lanes} " +
                        $"node={from}->{to} along={along:F1}/{length:F1} speed={speedKmh:F0} " +
                        $"pos={new Vector3(root.position.x, 0f, root.position.z)}");
                    return;
                }
                if (along < margin || length - along < margin) { skipMargin++; return; }
                float expected = graph.GetLaneOffset(from, to, lane);
                Vector3 right = Vector3.Cross(Vector3.up, segment);
                float lateral = Vector3.Dot(root.position - fromPos, right);
                // Mid-change, recovery and junction cuts are measured elsewhere.
                if (Mathf.Abs(lateral - expected) > .8f) { skipOffLane++; return; }

                Bounds visual = default;
                bool hasVisual = false;
                foreach (var renderer in root.GetComponentsInChildren<Renderer>(false))
                {
                    if (renderer == null) continue;
                    if (!hasVisual) { visual = renderer.bounds; hasVisual = true; }
                    else visual.Encapsulate(renderer.bounds);
                }
                float lateralVisual = hasVisual
                    ? Vector3.Dot(new Vector3(visual.center.x, root.position.y, visual.center.z) - fromPos, right)
                    : lateral;
                float pivot = lateralVisual - lateral;
                if (Mathf.Abs(pivot) > Mathf.Abs(worstPivot)) worstPivot = pivot;
                if (Mathf.Abs(pivot) > .75f) pivotOffsets++;

                sideSamples++;
                if (lateral * kerbSign < -.5f || lateralVisual * kerbSign < -.5f)
                {
                    sideWrong++;
                    Debug.Log($"SMOKE wrongSide {tier} {root.name} body={lateral:F2} visual={lateralVisual:F2} " +
                        $"pivot={pivot:F2} expected={expected:F2} kerbSign={kerbSign:F0} graphLeft={graph.DrivesOnLeft} " +
                        $"lane={lane}/{lanes} node={from}->{to} along={along:F1}/{length:F1}");
                }
            }
            foreach (var ai in Object.FindObjectsByType<AIVehicleController>(FindObjectsInactive.Exclude))
            {
                if (!ai.isActiveAndEnabled) continue;
                AuditSide("full", ai.roadGraph, ai.currentNodeIndex, ai.targetNodeIndex, ai.laneIndex,
                    ai.transform, ai.IsChangingLane, Mathf.Max(ai.vehicleLength * 1.5f, 8f), ai.currentSpeedKmh);
            }
            foreach (var splineCar in Object.FindObjectsByType<SplineVehicle>(FindObjectsInactive.Exclude))
            {
                if (!splineCar.isActiveAndEnabled) continue;
                AuditSide("spline", splineCar.roadGraph, splineCar.currentNodeIndex, splineCar.targetNodeIndex,
                    splineCar.laneIndex, splineCar.transform, splineCar.IsOvertaking, 12f, splineCar.speedKmh);
            }
        }
        bool sidesOk = country != null && sideSamples > 0 && sideWrong == 0;
        passed &= sidesOk;
        Debug.Log($"SMOKE trafficSide samples={sideSamples} wrongSide={sideWrong} reversed={sideReversed} " +
            $"pivotOffsets={pivotOffsets} worstPivot={worstPivot:F2} " +
            $"skipChanging={skipChanging} skipInvalid={skipInvalid} skipOneWay={skipOneWay} " +
            $"skipMargin={skipMargin} skipOffLane={skipOffLane} " +
            $"countryLeft={(country != null && country.DrivesOnLeft)} passed={sidesOk}");

        bool trafficOk = laneOk && texturedOk && variedOk && liveriesOk && sidesOk;
        Debug.Log($"SMOKE liveries painted={liveryPainted} distinct={liveryColors.Count} passed={liveriesOk}");
        string laneFloor = pool.roadGraph != null ? pool.roadGraph.MinimumLaneWidth.ToString("F2") : "n/a";
        int trafficRenderers = pool.GetComponentsInChildren<Renderer>().Length;
        int poolRenderers = pool.GetComponentsInChildren<Renderer>(true).Length;
        int trafficWheelColliders = pool.GetComponentsInChildren<WheelCollider>(true).Length;
        int trafficMeshColliders = pool.GetComponentsInChildren<MeshCollider>(true).Length;
        Debug.Log($"SMOKE traffic pool={pool.GetPoolCount()} spawned={pooledCount} atOrigin={atOrigin} " +
            $"busDistance={(playerBus != null && pooledCount > 0 ? $"{nearest:F0}-{farthest:F0}m" : "n/a")} models={models.Count} " +
            $"[{string.Join(",", models.Select(m => m.prefab.name))}] untextured={untextured} " +
            $"widestModel={pool.WidestVehicleWidth:F2}m laneFloor={laneFloor}m " +
            $"densityVolume={pool.GetTrafficDensityVolume():F2} density={densityEnabled} densityTarget={pool.DensityTarget} " +
            $"fullAi={fullAi} spline={spline} dormant={dormant} " +
            $"renderers={trafficRenderers} poolRenderers={poolRenderers} " +
            $"wheelColliders={trafficWheelColliders} meshColliders={trafficMeshColliders} " +
            $"laneSamples={laneSamples} steadySamples={steadySamples} offLane={offLane} worstLaneDeviation={worstDeviation:F2} " +
            $"worstSteadyLaneDeviation={worstSteady:F2} worst={worstVehicle} passed={trafficOk}");
        return trafficOk;
    }

    /// <summary>
    /// Runs the route one stop at a time. Every stop is walked to along the mission's
    /// own guidance line, served through the real door entry point, and then required
    /// to move <see cref="MissionManager.currentStopIndex"/> on. A stop that does not
    /// release the route is a failure with its state attached, not something the
    /// harness waits out.
    /// </summary>
    static void TickRoute(double elapsed)
    {
        var mission = MissionManager.Instance;
        var bus = mission != null ? mission.busController : Object.FindAnyObjectByType<BusController>();
        if (routeStartedAt <= 0f) routeStartedAt = Time.time;

        if (Time.time - routeStartedAt > RouteOverallBudgetSeconds)
        {
            FailRoute($"the route did not finish in {Time.time - routeStartedAt:F0}s " +
                $"(at stop {routeStopIndex}, {routeServiced} serviced, {RouteState(mission)})");
            return;
        }

        if (routePhase == RoutePhaseBoot)
        {
            // The route has to be served in the finished city, not while the near
            // ring is still being built: a runner that hops through an unfinished
            // world measures streaming work instead of stop progression.
            bool worldBuilt = OSMLoader.Instance != null && OSMLoader.Instance.dataLoaded &&
                OSMRoadMeshBuilder.Instance != null && OSMRoadMeshBuilder.Instance.roadsBuilt &&
                OSMBuildingMeshBuilder.Instance != null && OSMBuildingMeshBuilder.Instance.buildingsBuilt;
            bool routeReady = mission != null && bus != null && mission.routeActive &&
                mission.currentRoute != null && mission.currentRoute.stops != null &&
                mission.currentRoute.stops.Length > 0;
            if (!routeReady || !worldBuilt || FindRouteZone(mission, 0) == null)
            {
                if (Time.time - routeBootLogAt > 10f)
                {
                    routeBootLogAt = Time.time;
                    Debug.Log($"SMOKE route boot t={elapsed:F0}s route={routeReady} world={worldBuilt} " +
                        $"roads={(OSMLoader.Instance != null && OSMLoader.Instance.dataLoaded)}/" +
                        $"{(OSMRoadMeshBuilder.Instance != null && OSMRoadMeshBuilder.Instance.roadsBuilt)} " +
                        $"buildings={(OSMBuildingMeshBuilder.Instance != null && OSMBuildingMeshBuilder.Instance.buildingsBuilt)} " +
                        $"stops={routeReady && FindRouteZone(mission, 0) != null} mission={mission?.missionState}");
                }
                if (elapsed < 300) return;
                FailRoute($"the route was not ready after {elapsed:F0}s (route={routeReady} world={worldBuilt} " +
                    $"mission={mission?.missionState} active={mission?.routeActive} bus={bus != null} " +
                    $"firstStopZone={routeReady && FindRouteZone(mission, 0) != null})");
                return;
            }
            Vector3 busPosition = bus.transform.position;
            routeBusYOffset = busPosition.y;
            var guidance = mission.GetGuidancePathPoints();
            if (guidance != null && guidance.Length > 0)
            {
                float best = float.MaxValue;
                foreach (Vector3 point in guidance)
                {
                    float sqr = FlatSqr(point, busPosition);
                    if (sqr >= best) continue;
                    best = sqr;
                    routeBusYOffset = busPosition.y - point.y;
                }
            }
            SetRouteTimeScale(RouteTimeScale);
            routeStopIndex = Mathf.Clamp(mission.currentStopIndex, 0, mission.currentRoute.stops.Length - 1);
            routePhase = RoutePhaseTravel;
            routePhaseAt = Time.time;
            BeginRouteLeg();
            Debug.Log($"SMOKE route start stops={mission.currentRoute.stops.Length} index={mission.currentStopIndex} " +
                $"stop='{mission.currentRoute.stops[routeStopIndex].stopName}' busYOffset={routeBusYOffset:F2} " +
                $"toStop={mission.distanceToNextStop:F0}m guidanceVersion={mission.GuidancePathVersion}");
            SampleTrafficMotion(busPosition);
            return;
        }

        // Traffic is judged independently of stop progression: the pool, the lanes and
        // the driving side are checked while the route is under way, so a route bug
        // cannot take the traffic verdict down with it, and a short first leg still
        // gets its motion window.
        if (mission.routeActive && Time.time - routeStartedAt > 8f)
        {
            SampleTrafficMotion(bus.transform.position);
            if (!trafficLaneChecked)
            {
                trafficLaneChecked = true;
                trafficLaneOk &= CheckTraffic();
            }
            // Give the overlay and the HUD one rendered frame to react to the
            // route becoming active before judging whether they are drawing.
            if (routeNavigatorReadyAt < 0f) routeNavigatorReadyAt = Time.time;
            if (Time.time - routeNavigatorReadyAt > 3f) SampleRouteNavigator(mission);
        }

        int stopCount = mission.currentRoute.stops.Length;
        if (routeStopIndex >= stopCount)
        {
            FinishRoute(mission);
            return;
        }

        var zone = FindRouteZone(mission, routeStopIndex);
        if (zone == null)
        {
            FailRoute($"stop {routeStopIndex} ('{mission.currentRoute.stops[routeStopIndex].stopName}') has no docking zone");
            return;
        }

        // The mission is the authority on the stop being served. If it has moved on
        // without this runner asking, the runner is out of step and the report should
        // say so rather than serve a stop that is no longer the target. While the stop
        // is being served the mission advancing IS the assertion, so it is not drift.
        if (routePhase != RoutePhaseService && mission.currentStopIndex != routeStopIndex)
        {
            routeFailures++;
            routeFailureLog.Add($"index drift before stop {routeStopIndex}: mission was at {mission.currentStopIndex}");
            Debug.LogError($"SMOKE routeIndexDrift expected={routeStopIndex} actual={mission.currentStopIndex}");
            routeStopIndex = Mathf.Clamp(mission.currentStopIndex, 0, stopCount - 1);
            return;
        }

        if (routePhase == RoutePhaseTravel)
        {
            Vector3 target = zone.transform.position;
            float remaining = FlatDistance(bus.transform.position, target);
            if (remaining > 8f)
            {
                if (Time.time - routeStopTravelAt > RouteStopTravelBudgetSeconds)
                {
                    string detail = $"stop {routeStopIndex} ('{zone.stopName}') was still {remaining:F0}m away after " +
                        $"{Time.time - routeStopTravelAt:F0}s of travelling ({routeHops} hops, " +
                        $"bus={Flat(bus.transform.position)} speed={bus.currentSpeedKmh:F0} {RouteState(mission)})";
                    routeFailures++;
                    routeFailureLog.Add(detail);
                    FailRoute(detail);
                    return;
                }

                if (Time.time - routeTravelLogAt > 15f)
                {
                    routeTravelLogAt = Time.time;
                    Debug.Log($"SMOKE routeTravel index={routeStopIndex} hops={routeHops} remaining={remaining:F0}m " +
                        $"closest={routeClosestRemaining:F0}m bus={Flat(bus.transform.position)} bay={Flat(target)} " +
                        $"speed={bus.currentSpeedKmh:F0} guidance={mission.GuidancePathVersion} {RouteState(mission)}");
                }

                // Paced hops keep the guidance rebuild, the stop triggers and the
                // traffic perception running between moves rather than teleporting
                // the bus across a leg in a single frame.
                if (Time.time - routePhaseAt < RouteHopSeconds) return;
                routePhaseAt = Time.time;
                routeHops++;

                // Walking in place is the failure this runner must never hide: a leg
                // that stops getting closer is reported with the bus, the bay and the
                // guidance it was following. The accounting is per hop, not per frame,
                // so the pause between hops cannot look like a stall.
                if (remaining < routeClosestRemaining - .5f)
                {
                    routeClosestRemaining = remaining;
                    routeStallHops = 0;
                }
                else if (++routeStallHops > 60 && !routeLegRescued)
                {
                    // The runner walks the route by teleporting, so a leg can be cut off
                    // by geometry it cannot drive through. Cross that leg by seating the
                    // bus on the bay itself and say so in the log, instead of spinning
                    // until the budget expires. The stop still has to dock and release
                    // the index on its own, which is what this run is about.
                    routeLegRescued = true;
                    routeShortcuts++;
                    Debug.Log($"SMOKE routeShortcut index={routeStopIndex} stop='{zone.stopName}' " +
                        $"from={Flat(bus.transform.position)} bay={Flat(target)} remaining={remaining:F0}m " +
                        $"closest={routeClosestRemaining:F0}m hops={routeHops} {RouteState(mission)}");
                    PoseBusAtStop(bus, zone);
                    routePhase = RoutePhaseDock;
                    routePhaseAt = Time.time;
                    return;
                }
                else if (routeStallHops > RouteStallHopLimit)
                {
                    string detail = $"stop {routeStopIndex} ('{zone.stopName}') never got closer than " +
                        $"{routeClosestRemaining:F0}m in {routeStallHops} hops (now {remaining:F0}m, " +
                        $"bus={Flat(bus.transform.position)} bay={Flat(target)} " +
                        $"speed={bus.currentSpeedKmh:F0} guidance={mission.GuidancePathVersion} " +
                        $"points={mission.GetGuidancePathPoints()?.Length ?? 0} {RouteState(mission)})";
                    routeFailures++;
                    routeFailureLog.Add(detail);
                    FailRoute(detail);
                    return;
                }

                // The mission's own guidance is what the player is expected to
                // drive, so the runner follows it wherever it goes. Stepping straight
                // at the bay instead simply walks the bus through the buildings
                // between here and there, and the collision response puts it back.
                // The last few metres are the pull-in: the bay sits at the kerb, a few
                // metres off the guided line, so from there the runner drives at the bay
                // itself or it would sail past its own stop.
                Vector3 waypoint = target;
                bool guided = remaining > RouteDockApproachMeters &&
                    TryNextRouteWaypoint(mission, bus.transform.position, target, RouteHopMeters, out waypoint);
                // Guidance runs from the bus to the end of the route, so once the line
                // has passed this stop it leads away from the bay. That is the moment to
                // pull in at the kerb rather than be walked to the next stop.
                if (guided && FlatDistance(waypoint, target) >= remaining - .5f)
                    guided = false;
                if (!guided)
                {
                    Vector3 approach = target - bus.transform.position;
                    approach.y = 0f;
                    float step = Mathf.Min(RouteHopMeters, approach.magnitude);
                    waypoint = approach.sqrMagnitude < .0001f
                        ? target
                        : bus.transform.position + approach.normalized * step;
                    waypoint.y = bus.transform.position.y;
                }
                Vector3 direction = waypoint - bus.transform.position;
                direction.y = 0f;
                if (direction.sqrMagnitude < .01f) return;
                bus.SetParkingBrake(false);
                bus.ServiceBrakeInterlock = false;
                bus.Teleport(new Vector3(waypoint.x, waypoint.y + routeBusYOffset, waypoint.z),
                    Quaternion.LookRotation(direction.normalized, Vector3.up));
                return;
            }

            PoseBusAtStop(bus, zone);
            routePhase = RoutePhaseDock;
            routePhaseAt = Time.time;
            return;
        }

        if (routePhase == RoutePhaseDock)
        {
            routeCurrentStopDocked = zone.isCorrectlyDocked && DockingZone.CurrentDockedZone == zone;
            if (!routeCurrentStopDocked && Time.time - routePhaseAt < 2f) return;
            if (!routeCurrentStopDocked && routeDockAttempts < 3)
            {
                // Physics or a collider streamed in under the bus can settle the body
                // off the bay between the pose and the gate; seat it again before the
                // stop is reported as undisposable.
                routeDockAttempts++;
                PoseBusAtStop(bus, zone);
                routePhaseAt = Time.time;
                return;
            }
            if (!routeCurrentStopDocked)
            {
                routeFailures++;
                string detail = $"stop {routeStopIndex} never docked: zone={zone.isCorrectlyDocked} " +
                    $"current='{DockingZone.CurrentDockedZone?.stopName}' " +
                    $"kerb={zone.currentKerbOffsetMeters:F2}/{zone.kerbToleranceMeters:F2} " +
                    $"sign={zone.currentStopSignOffsetMeters:F2}/{zone.stopSignToleranceMeters:F2} " +
                    $"heading={zone.currentHeadingErrorDegrees:F1}/{zone.headingToleranceDegrees:F1} " +
                    $"speed={bus.currentSpeedKmh:F1}/{zone.maxDockingSpeedKmh:F1}";
                routeFailureLog.Add(detail);
                FailRoute(detail);
                return;
            }

            routeDocked++;
            Debug.Log($"SMOKE routeDock index={routeStopIndex} name='{zone.stopName}' kerb={zone.currentKerbOffsetMeters:F2} " +
                $"sign={zone.currentStopSignOffsetMeters:F2} heading={zone.currentHeadingErrorDegrees:F1} " +
                $"score={zone.currentDockingScore:F0} speed={bus.currentSpeedKmh:F1}");

            OpenRouteDoors(mission, zone);
            routeServiced++;
            routePhase = RoutePhaseService;
            routePhaseAt = Time.time;
            return;
        }

        // RoutePhaseService: the stop is being served. Nothing here presses the close
        // button: the mission has to end the stop by itself, which is what the old
        // unbounded animation wait silently failed to do.
        float served = Time.time - routePhaseAt;
        bool routeEnded = mission.missionState == MissionState.Completed || !mission.routeActive;
        bool advanced = mission.currentStopIndex == routeStopIndex + 1;
        if (advanced || (routeEnded && routeStopIndex == stopCount - 1))
        {
            routeAdvanced++;
            routeServiceSeconds.Add(served);
            string stopName = mission.currentRoute != null && routeStopIndex < mission.currentRoute.stops.Length
                ? mission.currentRoute.stops[routeStopIndex].stopName
                : zone.stopName;
            int versionAfter = mission.GuidancePathVersion;
            bool guidanceRebuilt = versionAfter > routeGuidanceVersionBeforeService;
            // The last stop has no next target to steer at: completing the route is
            // what it has to do instead, and that is asserted by the summary below.
            bool lastStop = routeStopIndex == stopCount - 1;
            if (!guidanceRebuilt && !lastStop)
            {
                routeFailures++;
                routeFailureLog.Add($"stop {routeStopIndex} advanced without a guidance rebuild " +
                    $"({routeGuidanceVersionBeforeService} -> {versionAfter})");
            }
            Debug.Log($"SMOKE routeStop index={routeStopIndex} name='{stopName}' docked={routeCurrentStopDocked} advanced=True " +
                $"service={served:F1}s indexAfter={mission.currentStopIndex} state={mission.missionState} " +
                $"guidanceVersion={routeGuidanceVersionBeforeService}->{versionAfter} rebuilt={guidanceRebuilt} " +
                $"routeActive={mission.routeActive} passengers={PassengerManager.Instance?.currentPassengers}");
            routeStopIndex++;
            routePhase = RoutePhaseTravel;
            routePhaseAt = Time.time;
            BeginRouteLeg();
            return;
        }

        // A stop still holding the route after the dwell it asked for plus the
        // service-animation ceiling is the bug this runner exists for. Report what the
        // mission, the doors and the passengers were doing instead of timing out.
        float dwell = PassengerManager.Instance != null ? PassengerManager.Instance.GetRequiredDwellTimeSeconds() : 0f;
        float budget = dwell + MissionManager.MaxServiceAnimationWaitSeconds + 25f;
        if (served > budget)
        {
            routeFailures++;
            string detail = $"stop {routeStopIndex} did not advance after {served:F1}s " +
                $"(processing={mission.isProcessingStop} state={mission.missionState} index={mission.currentStopIndex} " +
                $"doors={PassengerManager.Instance?.doorsOpen} animations={PassengerManager.Instance?.HasServiceAnimations} " +
                $"dwell={dwell:F1} budget={budget:F1})";
            routeFailureLog.Add(detail);
            FailRoute(detail);
            return;
        }
        if (Time.frameCount % 60 == 0)
            Debug.Log($"SMOKE routeServing index={routeStopIndex} t={served:F1}s dwell={dwell:F1}s " +
                $"processing={mission.isProcessingStop} animations={PassengerManager.Instance?.HasServiceAnimations} " +
                $"passengers={PassengerManager.Instance?.currentPassengers}");
    }

    static void OpenRouteDoors(MissionManager mission, DockingZone zone)
    {
        // A stop reached by the runner would otherwise be many minutes early, and the
        // mission would hold the bus in its punctuality dwell. Arriving at the
        // scheduled minute keeps the dwell under test to the one passengers ask for.
        var schedule = ScheduleManager.Instance;
        if (schedule != null)
        {
            float scheduled = schedule.GetScheduledArrival(routeStopIndex);
            if (scheduled > 0f && scheduled > schedule.currentTimeMinutes)
                schedule.currentTimeMinutes = scheduled;
        }
        bool opened = mission.TryOpenDoorsAtCurrentStop();
        routeGuidanceVersionBeforeService = mission.GuidancePathVersion;
        if (!opened)
            Debug.LogWarning($"SMOKE routeDoors index={routeStopIndex} opened=False isProcessingStop={mission.isProcessingStop} " +
                $"state={mission.missionState} docked={zone.isCorrectlyDocked} " +
                $"passengers={PassengerManager.Instance?.currentPassengers}");
    }

    static void PoseBusAtStop(BusController bus, DockingZone zone)
    {
        // Seat the bus so its own door reference sits on the bay the game measures
        // the stop against, facing the way the stop was built. The docking gate, not
        // this helper, is still the judge of whether the bus is really at the kerb.
        Quaternion rotation = zone.transform.rotation;
        Vector3 doorLocal = bus.dockingReference != null
            ? bus.transform.InverseTransformPoint(bus.dockingReference.position)
            : Vector3.zero;
        doorLocal.y = 0f;
        // DockingZone flips the door anchor onto the authored kerb side before it
        // measures the bay. Seating the bus from the unflipped anchor parks it a door
        // width off the bay, so the stop could never open.
        if (zone.roadsideDockingSide != 0 && bus.dockingReference != null &&
            bus.dockingReference.name == "DockingReference")
            doorLocal.x = Mathf.Abs(doorLocal.x) * Mathf.Sign(zone.roadsideDockingSide);
        Vector3 position = zone.transform.position - rotation * doorLocal;
        position.y = zone.transform.position.y + routeBusYOffset;
        bus.SetParkingBrake(true);
        bus.ServiceBrakeInterlock = false;
        bus.Teleport(position, rotation);
        Debug.Log($"SMOKE routePose index={routeStopIndex} position={new Vector3(position.x, 0f, position.z)} " +
            $"yaw={rotation.eulerAngles.y:F1} bay={new Vector3(zone.transform.position.x, 0f, zone.transform.position.z)}");
    }

    static DockingZone FindRouteZone(MissionManager mission, int index)
    {
        if (mission == null || mission.currentRoute == null || mission.currentRoute.stops == null ||
            index < 0 || index >= mission.currentRoute.stops.Length)
            return null;
        string stopName = mission.currentRoute.stops[index].stopName;
        // A route can visit the same shelter more than once (this one lists
        // 'Stima Plaza' three times running), so a name-only match can land on the
        // wrong visit. The zone whose stopIndex is this route index wins, and ties
        // are broken by distance to the stop's own coordinate.
        bool hasGps = GPSManager.Instance != null;
        Vector3 stopWorld = hasGps
            ? GPSManager.Instance.GpsToWorld(mission.currentRoute.stops[index].latitude,
                mission.currentRoute.stops[index].longitude)
            : Vector3.zero;
        DockingZone best = null;
        int bestRank = int.MaxValue;
        float bestSqr = float.MaxValue;
        foreach (var zone in Object.FindObjectsByType<DockingZone>(FindObjectsInactive.Exclude))
        {
            int rank = zone.stopIndex == index ? 0 :
                string.Equals(zone.stopName, stopName, System.StringComparison.Ordinal) ? 1 : -1;
            if (rank < 0) continue;
            float sqr = hasGps ? FlatSqr(zone.transform.position, stopWorld) : 0f;
            if (rank > bestRank || (rank == bestRank && sqr >= bestSqr)) continue;
            best = zone;
            bestRank = rank;
            bestSqr = sqr;
        }
        return best;
    }

    /// <summary>
    /// Resets the per-leg travel bookkeeping. Each stop gets its own budget, so one
    /// long leg cannot eat the time the rest of the route needs to prove itself.
    /// </summary>
    static void BeginRouteLeg()
    {
        routeClosestRemaining = float.MaxValue;
        routeStallHops = 0;
        routeLegRescued = false;
        routeHops = 0;
        routeDockAttempts = 0;
        routeStopTravelAt = Time.time;
        routeTravelLogAt = Time.time;
    }

    static Vector3 Flat(Vector3 position) => new Vector3(position.x, 0f, position.z);

    static string RouteState(MissionManager mission)
    {
        if (mission == null) return "mission=none";
        return $"state={mission.missionState} active={mission.routeActive} index={mission.currentStopIndex} " +
            $"processing={mission.isProcessingStop} toStop={mission.distanceToNextStop:F0}m";
    }

    /// <summary>
    /// The next point to drive to. Returns true when it came from the mission's own
    /// guidance (the line the player is told to follow); the straight-at-the-bay
    /// fallback is only for a mission with no usable guidance at all.
    /// </summary>
    static bool TryNextRouteWaypoint(MissionManager mission, Vector3 from, Vector3 target, float step,
        out Vector3 waypoint)
    {
        if (mission != null && mission.TryGetGuidancePointAhead(step, out Vector3 ahead) &&
            FlatDistance(ahead, from) > .5f)
        {
            waypoint = ahead;
            return true;
        }

        Vector3 offset = target - from;
        offset.y = 0f;
        float distance = offset.magnitude;
        waypoint = distance <= .01f ? target : from + offset / distance * Mathf.Min(step, distance);
        return false;
    }

    static float FlatSqr(Vector3 a, Vector3 b)
    {
        float x = a.x - b.x, z = a.z - b.z;
        return x * x + z * z;
    }

    static float FlatDistance(Vector3 a, Vector3 b) => Mathf.Sqrt(FlatSqr(a, b));

    /// <summary>
    /// Watches the street actually move. A pool that is populated but stationary is
    /// indistinguishable from no traffic when the player is at a stop.
    /// </summary>
    static void SampleTrafficMotion(Vector3 around)
    {
        if (trafficMotionSampled) return;
        if (trafficMotionBodies.Count == 0)
        {
            foreach (var ai in Object.FindObjectsByType<AIVehicleController>(FindObjectsInactive.Exclude))
            {
                if (!ai.isActiveAndEnabled) continue;
                if (Vector3.Distance(ai.transform.position, around) > 400f) continue;
                trafficMotionBodies.Add(ai.transform);
                trafficMotionStarts.Add(ai.transform.position);
            }
            trafficMotionAt = Time.time;
            trafficMotionActive = trafficMotionBodies.Count;
            Debug.Log($"SMOKE routeTraffic sample bodies={trafficMotionActive} " +
                $"around={new Vector3(around.x, 0f, around.z)}");
            return;
        }
        if (Time.time - trafficMotionAt < 6f) return;
        trafficMotionSampled = true;
        trafficMotionMoved = 0;
        for (int i = 0; i < trafficMotionBodies.Count; i++)
        {
            Transform body = trafficMotionBodies[i];
            if (body == null || !body.gameObject.activeInHierarchy) continue;
            if (FlatDistance(body.position, trafficMotionStarts[i]) > 5f) trafficMotionMoved++;
        }
        bool moved = trafficMotionActive >= 3 && trafficMotionMoved >= Mathf.Max(2, trafficMotionActive / 4);
        Debug.Log($"SMOKE routeTraffic window={(Time.time - trafficMotionAt):F1}s active={trafficMotionActive} " +
            $"moved={trafficMotionMoved} passed={moved}");
        if (!moved)
        {
            routeFailures++;
            routeFailureLog.Add($"traffic did not move: {trafficMotionMoved}/{trafficMotionActive} bodies moved in 6s");
        }
    }

    /// <summary>
    /// Samples the three parts of the navigator a player actually sees: the world
    /// chevrons, the next-stop beacon and the HUD turn banner. The requirement is
    /// that they stay alive for the whole route, so this is sampled every few seconds
    /// instead of once when the route starts.
    /// </summary>
    static void SampleRouteNavigator(MissionManager mission)
    {
        if (routeNavigatorAt > 0f && Time.time - routeNavigatorAt < 5f) return;
        routeNavigatorAt = Time.time;

        var overlay = RouteArrowOverlay.Instance;
        var hud = HUDManager.Instance;
        bool pathOk = mission.ActiveGuidancePathPoints != null && mission.ActiveGuidancePathPoints.Count >= 2;
        bool arrows = overlay != null && overlay.HasVisibleArrows;
        bool beacon = overlay != null && overlay.BeaconVisible;
        bool banner = hud != null && hud.NavigatorVisible;
        // The final stop is the destination: there is no road ahead to draw
        // chevrons on, so the beacon and the arrival banner are the navigator
        // there. Everywhere else the route line must still be visible.
        bool atFinalStop = mission.currentRoute != null &&
            mission.currentStopIndex >= mission.currentRoute.stops.Length - 1;
        bool arrivalState = atFinalStop && beacon && banner &&
            (mission.isProcessingStop || mission.distanceToNextStop <= 30f);

        routeNavigatorChecks++;
        routeNavigatorArrows = arrows;
        routeNavigatorBeacon = beacon;
        routeNavigatorHud = banner;

        if ((arrows && beacon && banner && pathOk) || arrivalState) return;

        routeNavigatorFailures++;
        if (routeNavigatorFailures <= 5)
            Debug.LogWarning($"SMOKE routeNavigator missing at stop {routeStopIndex}: overlay={overlay != null} " +
                $"arrows={arrows} beacon={beacon} hud={hud != null} banner={banner} arrival={arrivalState} " +
                $"path={mission.ActiveGuidancePathPoints?.Count ?? 0} version={mission.GuidancePathVersion} {RouteState(mission)}");
    }

    static void FinishRoute(MissionManager mission)
    {
        int stopCount = mission != null && mission.currentRoute != null && mission.currentRoute.stops != null
            ? mission.currentRoute.stops.Length : 0;
        bool completed = mission != null && mission.missionState == MissionState.Completed && !mission.routeActive;
        bool trafficOk = trafficMotionSampled && trafficMotionActive >= 3 &&
            trafficMotionMoved >= Mathf.Max(2, trafficMotionActive / 4);
        bool navigatorOk = routeNavigatorChecks > 0 && routeNavigatorFailures == 0;
        bool routeOk = completed && routeFailures == 0 && routeAdvanced == stopCount && stopCount > 0 &&
            trafficOk && trafficLaneChecked && trafficLaneOk && navigatorOk;
        float total = Time.time - routeStartedAt;
        float average = routeServiceSeconds.Count > 0 ? routeServiceSeconds.Average() : 0f;
        float longest = routeServiceSeconds.Count > 0 ? routeServiceSeconds.Max() : 0f;
        Debug.Log($"SMOKE routeResult stops={stopCount} serviced={routeServiced} docked={routeDocked} advanced={routeAdvanced} " +
            $"failures={routeFailures} shortcuts={routeShortcuts} serviceAvg={average:F1}s serviceLongest={longest:F1}s " +
            $"seconds={total:F0} completed={completed} trafficMoved={trafficMotionMoved}/{trafficMotionActive} " +
            $"trafficLaneChecked={trafficLaneChecked} trafficLanes={trafficLaneOk} " +
            $"navigator={navigatorOk} passed={routeOk}");
        Debug.Log($"SMOKE routeNavigator checks={routeNavigatorChecks} failures={routeNavigatorFailures} " +
            $"arrows={routeNavigatorArrows} beacon={routeNavigatorBeacon} banner={routeNavigatorHud} passed={navigatorOk}");
        foreach (string failure in routeFailureLog) Debug.Log($"SMOKE routeFailure {failure}");
        LogPerformance();
        ClearRouteSession();
        EditorApplication.Exit(routeOk ? 0 : 2);
    }

    static void FailRoute(string reason)
    {
        Debug.LogError($"SMOKE route failed: {reason}");
        Debug.Log($"SMOKE routeResult stops={(MissionManager.Instance?.currentRoute?.stops?.Length ?? 0)} " +
            $"serviced={routeServiced} docked={routeDocked} advanced={routeAdvanced} failures={routeFailures + 1} " +
            $"navigatorChecks={routeNavigatorChecks} navigatorFailures={routeNavigatorFailures} " +
            $"seconds={(Time.time - routeStartedAt):F0} completed=False passed=False");
        foreach (string failure in routeFailureLog) Debug.Log($"SMOKE routeFailure {failure}");
        LogPerformance();
        ClearRouteSession();
        EditorApplication.Exit(2);
    }

    static void ClearRouteSession()
    {
        SessionState.SetBool("RealBus.SmokeRoute", false);
        SessionState.SetBool("RealBus.SmokeCheck", false);
        SessionState.SetBool("RealBus.SmokeExtended", false);
        Time.timeScale = 1f;
    }

    static float EmissionRate(ParticleSystem system)
        => system != null && system.isPlaying ? system.emission.rateOverTime.constant : 0f;

    static Light FindDirectionalSun()
    {
        var lights = Object.FindObjectsByType<Light>(FindObjectsInactive.Include);
        for (int i = 0; i < lights.Length; i++)
            if (lights[i] != null && lights[i].type == LightType.Directional) return lights[i];
        return null;
    }

    /// <summary>
    /// Steady-state frame time for this run, as a percentile summary rather than an
    /// average that hides the hitches. Worst frame and the number over 33/50 ms are
    /// the numbers that read as "stutters".
    /// </summary>
    static void LogPerformance()
    {
        if (frameTimes.Count == 0)
        {
            Debug.Log("SMOKE perf no samples");
            return;
        }

        var sorted = new System.Collections.Generic.List<float>(frameTimes);
        sorted.Sort();
        float total = 0f;
        int over33 = 0, over50 = 0;
        for (int i = 0; i < sorted.Count; i++)
        {
            total += sorted[i];
            if (sorted[i] > 0.0334f) over33++;
            if (sorted[i] > 0.05f) over50++;
        }
        int p50 = Mathf.Clamp(Mathf.RoundToInt(sorted.Count * 0.50f) - 1, 0, sorted.Count - 1);
        int p95 = Mathf.Clamp(Mathf.RoundToInt(sorted.Count * 0.95f) - 1, 0, sorted.Count - 1);
        int renderers = Object.FindObjectsByType<Renderer>(FindObjectsInactive.Exclude).Length;
        Camera mainCamera = Camera.main;
        float farClip = mainCamera != null ? mainCamera.farClipPlane : 0f;
        int lodGroups = Object.FindObjectsByType<LODGroup>(FindObjectsInactive.Exclude).Length;
        Debug.Log($"SMOKE perf samples={sorted.Count} avgFps={sorted.Count / Mathf.Max(0.001f, total):F1} " +
            $"p50Ms={sorted[p50] * 1000f:F1} p95Ms={sorted[p95] * 1000f:F1} worstMs={sorted[sorted.Count - 1] * 1000f:F1} " +
            $"over33ms={over33} over50ms={over50} renderers={renderers} farClip={farClip:F0} lodGroups={lodGroups}");

        // Where the draw calls come from. A renderer count on its own does not say
        // whether the city, the props or the traffic is the expensive half.
        var roots = new System.Collections.Generic.List<(string name, int count)>();
        foreach (GameObject root in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
        {
            int count = root.GetComponentsInChildren<Renderer>(true).Length;
            if (count > 0) roots.Add((root.name, count));
        }
        roots.Sort((a, b) => b.count.CompareTo(a.count));
        var parts = new System.Text.StringBuilder();
        for (int i = 0; i < Mathf.Min(8, roots.Count); i++)
        {
            if (i > 0) parts.Append(',');
            parts.Append(roots[i].name).Append('=').Append(roots[i].count);
        }
        Debug.Log($"SMOKE perf roots={parts}");
    }

    /// <summary>
    /// Grabs one frame of the low hero pass, so the framing can be checked without sitting
    /// in the editor. Only runs when the shot was opted back in for a batch run; the
    /// overlay's letterbox is a screen-space canvas, so this frame shows the 3D shot only.
    /// The frame is taken after the cut rather than on the opening frame, because the pass
    /// is the shot that can actually be framed wrong.
    /// </summary>
    static void CaptureCinematicIntroOnce()
    {
        if (introCaptured) return;
        var intro = Object.FindAnyObjectByType<CinematicIntroSequence>();
        if (intro == null || !intro.IsPlaying) return;
        if (intro.Progress < 0.42f) return;
        introCaptured = true;
        CaptureCamera("/tmp/realbus-cinematic-intro.png");
        Debug.Log($"SMOKE cinematic intro captured at {intro.Progress:0.00}");
    }

    static void CaptureCamera(string path)
    {
        // CI and software-rendered batch runs can spend minutes inside a single
        // multisampled camera render. The gameplay checks still run without the
        // screenshot, so let those runners opt out of the capture.
        if (System.Environment.GetEnvironmentVariable("REALBUS_SMOKE_SKIP_CAPTURE") == "1") return;

        Camera camera = Camera.main;
        if (camera == null) return;
        // 4x MSAA: the quality tier the game ships with. A single-sample target made
        // every screenshot look softer than the actual build and hid edge quality.
        // A software-rendered batch run can override this with REALBUS_SMOKE_MSAA=1
        // against the resolve stalling on the emulated MSAA path.
        int samples = 4;
        string sampleOverride = System.Environment.GetEnvironmentVariable("REALBUS_SMOKE_MSAA");
        if (!string.IsNullOrEmpty(sampleOverride) && int.TryParse(sampleOverride, out int parsed) && parsed >= 1 && parsed <= 8)
            samples = parsed;
        var target = RenderTexture.GetTemporary(1600, 900, 24, RenderTextureFormat.Default, RenderTextureReadWrite.Linear, samples);
        var previousTarget = camera.targetTexture;
        var previousActive = RenderTexture.active;
        var image = new Texture2D(1600, 900, TextureFormat.RGB24, false);
        try
        {
            camera.targetTexture = target;
            camera.Render();
            RenderTexture.active = target;
            image.ReadPixels(new Rect(0, 0, 1600, 900), 0, 0);
            image.Apply();
            System.IO.File.WriteAllBytes(path, image.EncodeToPNG());
        }
        finally
        {
            camera.targetTexture = previousTarget;
            RenderTexture.active = previousActive;
            RenderTexture.ReleaseTemporary(target);
            Object.DestroyImmediate(image);
        }
    }
}
#endif
