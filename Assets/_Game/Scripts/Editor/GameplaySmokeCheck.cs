#if UNITY_EDITOR
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
            if (playerBus == null) continue;
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
        bool laneOk = steadySamples >= 3
            ? offLane <= Mathf.Max(1, steadySamples / 3) && worstSteady <= 2.5f
            : laneSamples > 0 && worstDeviation <= 3f;
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
        Debug.Log($"SMOKE traffic pool={pool.GetPoolCount()} spawned={pooledCount} atOrigin={atOrigin} " +
            $"busDistance={(playerBus != null && pooledCount > 0 ? $"{nearest:F0}-{farthest:F0}m" : "n/a")} models={models.Count} " +
            $"[{string.Join(",", models.Select(m => m.prefab.name))}] untextured={untextured} " +
            $"widestModel={pool.WidestVehicleWidth:F2}m laneFloor={laneFloor}m " +
            $"densityVolume={pool.GetTrafficDensityVolume():F2} density={densityEnabled} " +
            $"fullAi={fullAi} spline={spline} dormant={dormant} " +
            $"laneSamples={laneSamples} steadySamples={steadySamples} offLane={offLane} worstLaneDeviation={worstDeviation:F2} " +
            $"worstSteadyLaneDeviation={worstSteady:F2} worst={worstVehicle} passed={trafficOk}");
        return trafficOk;
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
        Debug.Log($"SMOKE perf samples={sorted.Count} avgFps={sorted.Count / Mathf.Max(0.001f, total):F1} " +
            $"p50Ms={sorted[p50] * 1000f:F1} p95Ms={sorted[p95] * 1000f:F1} worstMs={sorted[sorted.Count - 1] * 1000f:F1} " +
            $"over33ms={over33} over50ms={over50} renderers={renderers}");

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
