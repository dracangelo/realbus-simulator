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
    static bool passed;
    static int lastTelemetrySecond = -1;
    static float peakSpeed;
    static float maxCameraRoll;
    static int highestGear;
    static readonly System.Collections.Generic.List<Transform> depotTrafficVehicles = new System.Collections.Generic.List<Transform>();
    static readonly System.Collections.Generic.List<Vector3> depotTrafficStarts = new System.Collections.Generic.List<Vector3>();
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
            // start time on the next frame.
            clock.followScheduleManager = false;
            clock.currentTimeMinutes = minutes;
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
        if (started == 0) started = EditorApplication.timeSinceStartup;
        double elapsed = EditorApplication.timeSinceStartup - started;
        if (SessionState.GetBool("RealBus.SmokeLiveCity", false)) { TickLiveCity(elapsed); return; }
        if (elapsed > 240)
        {
            Debug.LogError("SMOKE timed out before completing the driving/camera check.");
            SessionState.SetBool("RealBus.SmokeCheck", false);
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
        Debug.Log($"SMOKE trafficSide={(CityManager.Instance?.activeCountry?.DrivesOnLeft == true ? "left" : "right")} syntheticBuildings={Object.FindAnyObjectByType<ProceduralBuildingFill>()?.ActiveCount}");
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
            $"rainBuilt={rainBuilt} snowBuilt={snowBuilt} rainRate={rainRate:F0} snowRate={snowRate:F0} fogController={(fog != null)}");
        passed &= sky != null && RenderSettings.skybox != null && cloudsBuilt && starsBuilt;
        passed &= rainBuilt && snowBuilt && fog != null && clock != null && weather != null;
        passed &= wetWeather ? rainRate > 0f : rainRate <= 0f;
        passed &= frozenWeather ? snowRate > 0f : snowRate <= 0f;


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
        EditorApplication.Exit(passed ? 0 : 2);
    }

    static float EmissionRate(ParticleSystem system)
        => system != null && system.isPlaying ? system.emission.rateOverTime.constant : 0f;

    static void CaptureCamera(string path)
    {
        Camera camera = Camera.main;
        if (camera == null) return;
        var target = RenderTexture.GetTemporary(1280, 720, 24);
        var previousTarget = camera.targetTexture;
        var previousActive = RenderTexture.active;
        var image = new Texture2D(1280, 720, TextureFormat.RGB24, false);
        try
        {
            camera.targetTexture = target;
            camera.Render();
            RenderTexture.active = target;
            image.ReadPixels(new Rect(0, 0, 1280, 720), 0, 0);
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
