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
            peakSpeed = 0f; maxCameraRoll = 0f; highestGear = 0; lastTelemetrySecond = -1;
            sampled = true;
            sampledAt = Time.time;
            Debug.Log($"SMOKE city={CityManager.Instance?.activeCity?.cityCode} gps={GPSManager.Instance != null} roads={OSMLoader.Instance?.dataLoaded}/{OSMRoadMeshBuilder.Instance?.roadsBuilt} buildings={OSMBuildingLoader.Instance?.dataLoaded}/{OSMBuildingMeshBuilder.Instance?.buildingsBuilt} mission={MissionManager.Instance?.missionState}");
            foreach (var b in Object.FindObjectsByType<BusController>(FindObjectsInactive.Include))
                Debug.Log($"SMOKE bus={b.name} active={b.gameObject.activeInHierarchy} engine={b.engineData != null} transmission={b.transmissionData != null} pos={b.transform.position} scale={b.transform.lossyScale} renderers={b.GetComponentsInChildren<Renderer>().Length} interlock={b.ServiceBrakeInterlock} fuelEmpty={b.FuelDepleted} park={b.ParkingBrakeActive}");
            foreach (var cam in Object.FindObjectsByType<Camera>())
                Debug.Log($"SMOKE camera={cam.name} parent={cam.transform.parent?.name} enabled={cam.enabled} local={cam.transform.localPosition} targetTexture={cam.targetTexture != null}");
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
                        if (roadIndex.OverlapsFootprint(footprint, .5f)) blockedStops++;
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
