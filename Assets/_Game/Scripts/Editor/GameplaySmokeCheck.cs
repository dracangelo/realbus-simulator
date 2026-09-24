#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

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

    static void Tick()
    {
        if (!SessionState.GetBool("RealBus.SmokeCheck", false) || !EditorApplication.isPlaying) return;
        if (started == 0) started = EditorApplication.timeSinceStartup;
        double elapsed = EditorApplication.timeSinceStartup - started;
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
            sampled = true;
            sampledAt = Time.time;
            Debug.Log($"SMOKE city={CityManager.Instance?.activeCity?.cityCode} gps={GPSManager.Instance != null} roads={OSMLoader.Instance?.dataLoaded}/{OSMRoadMeshBuilder.Instance?.roadsBuilt} buildings={OSMBuildingLoader.Instance?.dataLoaded}/{OSMBuildingMeshBuilder.Instance?.buildingsBuilt} mission={MissionManager.Instance?.missionState}");
            foreach (var b in Object.FindObjectsByType<BusController>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                Debug.Log($"SMOKE bus={b.name} active={b.gameObject.activeInHierarchy} engine={b.engineData != null} transmission={b.transmissionData != null} pos={b.transform.position} scale={b.transform.lossyScale} renderers={b.GetComponentsInChildren<Renderer>().Length} interlock={b.ServiceBrakeInterlock} fuelEmpty={b.FuelDepleted} park={b.ParkingBrakeActive}");
            foreach (var cam in Object.FindObjectsByType<Camera>(FindObjectsSortMode.None))
                Debug.Log($"SMOKE camera={cam.name} parent={cam.transform.parent?.name} enabled={cam.enabled} local={cam.transform.localPosition} targetTexture={cam.targetTexture != null}");
            if (bus != null)
            {
                startPosition = bus.transform.position;
                foreach (var w in bus.allWheels ?? new WheelCollider[0])
                    if (w != null) Debug.Log($"SMOKE wheel={w.name} grounded={w.isGrounded} position={w.transform.position} torque={w.motorTorque} brake={w.brakeTorque}");
                foreach (var r in bus.GetComponentsInChildren<Renderer>().Take(10))
                    Debug.Log($"SMOKE renderer={r.name} bounds={r.bounds} materials={string.Join(",", r.sharedMaterials.Select(m => m == null ? "null" : m.name + ":" + m.shader.name))}");
                foreach (var ui in Object.FindObjectsByType<MobileControlsUI>(FindObjectsSortMode.None)) ui.enabled = false;
                bus.readKeyboard = false;
                bus.SetMobileInput(1, 0, 0);
                MobileCameraController.Instance?.SetPreset(BusCameraPreset.Exterior);
            }
        }
        if (Time.time - sampledAt < 6) return;
        if (captureStage == 0)
        {
        float moved = bus != null ? Vector3.Distance(bus.transform.position, startPosition) : 0;
        Debug.Log($"SMOKE driveDistance={moved:F2} speed={bus?.currentSpeedKmh} throttle={bus?.throttleInput} assist={bus?.AssistThrottleLimit} brake={bus?.AssistBrakeFloor}");
        if (bus != null)
        {
            Debug.Log($"SMOKE physics position={bus.transform.position} kinematic={bus.Rigidbody.isKinematic} constraints={bus.Rigidbody.constraints} rpm={bus.currentRPM} gear={bus.transmissionData.currentGear} brakeInput={bus.brakeInput}");
            foreach (var w in bus.allWheels)
            {
                w.GetGroundHit(out WheelHit hit);
                Debug.Log($"SMOKE drivenWheel={w.name} grounded={w.isGrounded} surface={hit.collider?.name} radius={w.radius} suspension={w.suspensionDistance} torque={w.motorTorque} brake={w.brakeTorque} rpm={w.rpm}");
            }
            foreach (var collider in Physics.OverlapBox(bus.transform.position + Vector3.up * 1.5f, new Vector3(2f, 2f, 7f), bus.transform.rotation, ~0, QueryTriggerInteraction.Ignore))
                Debug.Log($"SMOKE overlap={collider.name} parent={collider.transform.parent?.name} bounds={collider.bounds} busPart={collider.transform.IsChildOf(bus.transform)}");
        }
        int screenCameras = Object.FindObjectsByType<Camera>(FindObjectsSortMode.None).Count(c => c.enabled && c.targetTexture == null);
        passed = moved > 1 && screenCameras == 1 && OSMLoader.Instance != null && OSMLoader.Instance.dataLoaded;
        Debug.Log($"SMOKE screenCameras={screenCameras} passed={passed}");
        CaptureCamera("/tmp/realbus-exterior.png");
        captureStage = 1;
        return;
        }
        if (Time.time - sampledAt < 7) return;
        if (captureStage == 1)
        {
            MobileCameraController.Instance?.SetPreset(BusCameraPreset.Cockpit);
            captureStage = 2;
            return;
        }
        if (Time.time - sampledAt < 8) return;
        if (captureStage == 2)
        {
            CaptureCamera("/tmp/realbus-cockpit.png");
            captureStage = 3;
            return;
        }
        if (Time.time - sampledAt < 9) return;
        SessionState.SetBool("RealBus.SmokeCheck", false);
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
