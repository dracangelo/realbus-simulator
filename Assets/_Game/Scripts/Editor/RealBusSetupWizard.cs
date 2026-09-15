#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class RealBusSetupWizard : EditorWindow
{
    GameObject busRoot;
    Vector2 scroll;

    [MenuItem("Tools/RealBus/Setup/Beginner Setup Wizard")]
    static void Open()
    {
        RealBusSetupWizard window = GetWindow<RealBusSetupWizard>("RealBus Setup");
        window.minSize = new Vector2(500f, 520f); window.busRoot = Selection.activeGameObject; window.Show();
    }

    [MenuItem("Tools/RealBus/Setup/Auto-wire Selected Bus")]
    static void AutoWireMenu() { AutoWire(Selection.activeGameObject, true); }

    void OnGUI()
    {
        scroll = EditorGUILayout.BeginScrollView(scroll);
        EditorGUILayout.LabelField("RealBus Beginner Setup", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Select the top-level bus object. The wizard adds safe gameplay components and connects WheelColliders that already exist. It never guesses wheel positions from artwork.", MessageType.Info);
        busRoot = (GameObject)EditorGUILayout.ObjectField("Bus root", busRoot, typeof(GameObject), true);
        EditorGUILayout.Space(8f);
        GUI.enabled = busRoot != null;
        if (GUILayout.Button("1. Auto-wire Bus", GUILayout.Height(40f))) AutoWire(busRoot, true);
        if (GUILayout.Button("2. Validate Bus", GUILayout.Height(36f))) ValidateBus(busRoot, true);
        GUI.enabled = true;
        if (GUILayout.Button("3. Prepare Open Gameplay Scene", GUILayout.Height(40f))) PrepareScene(busRoot);
        EditorGUILayout.Space(12f);
        EditorGUILayout.HelpBox("If validation reports missing WheelColliders or a body collider, add and position those visually in the Scene view, then run Auto-wire again.", MessageType.Warning);
        if (GUILayout.Button("Open Beginner Guide"))
        {
            Object guide = AssetDatabase.LoadAssetAtPath<Object>("Assets/_Game/Scripts/BEGINNER_SETUP.md");
            Selection.activeObject = guide; EditorGUIUtility.PingObject(guide);
        }
        EditorGUILayout.EndScrollView();
    }

    static void AutoWire(GameObject root, bool showResult)
    {
        if (root == null) { if (showResult) EditorUtility.DisplayDialog("RealBus Setup", "Select the top-level bus GameObject first.", "OK"); return; }
        Undo.SetCurrentGroupName("Auto-wire RealBus bus");
        Rigidbody body = GetOrAdd<Rigidbody>(root); body.mass = body.mass < 1000f ? 12000f : body.mass; body.interpolation = RigidbodyInterpolation.Interpolate; body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        BusController bus = GetOrAdd<BusController>(root);
        if (bus.engineData == null) bus.engineData = FindFirstAsset<EngineSystem>();
        if (bus.transmissionData == null) bus.transmissionData = FindFirstAsset<TransmissionSystem>();

        WheelCollider[] wheels = root.GetComponentsInChildren<WheelCollider>(true);
        if (wheels.Length > 0) AssignWheels(bus, wheels);
        if (bus.dockingReference == null)
        {
            GameObject marker = new GameObject("DockingReference"); Undo.RegisterCreatedObjectUndo(marker, "Create docking reference"); marker.transform.SetParent(root.transform, false); marker.transform.localPosition = new Vector3(1.05f, 0.45f, -1.5f); bus.dockingReference = marker.transform;
        }

        EngineTemperatureSystem temperature = GetOrAdd<EngineTemperatureSystem>(root); temperature.bus = bus;
        PassengerLoadDynamics load = GetOrAdd<PassengerLoadDynamics>(root);
        RoadSurfaceDetector detector = GetOrAdd<RoadSurfaceDetector>(root); detector.busController = bus;
        RoadSurfaceFrictionController friction = GetOrAdd<RoadSurfaceFrictionController>(root); friction.busController = bus; friction.surfaceDetector = detector;
        MaintenanceSystem maintenance = GetOrAdd<MaintenanceSystem>(root); maintenance.busController = bus;
        FuelSystem fuel = root.GetComponent<FuelSystem>(); BatterySystem battery = root.GetComponent<BatterySystem>();
        if (fuel == null && battery == null) { fuel = Undo.AddComponent<FuelSystem>(root); fuel.busController = bus; }
        GetOrAdd<DrivingAssistSystem>(root).bus = bus;
        GetOrAdd<CosmeticDamageSystem>(root);
        GetOrAdd<VehiclePolishEffects>(root).bus = bus;

        EditorUtility.SetDirty(body); EditorUtility.SetDirty(bus); EditorUtility.SetDirty(load);
        PrefabUtility.RecordPrefabInstancePropertyModifications(body);
        PrefabUtility.RecordPrefabInstancePropertyModifications(bus);
        if (root.scene.IsValid()) EditorSceneManager.MarkSceneDirty(root.scene);
        ValidateBus(root, showResult);
    }

    static void AssignWheels(BusController bus, WheelCollider[] wheels)
    {
        var ordered = new List<WheelCollider>(wheels); ordered.Sort((a, b) => bus.transform.InverseTransformPoint(b.transform.position).z.CompareTo(bus.transform.InverseTransformPoint(a.transform.position).z));
        float frontZ = bus.transform.InverseTransformPoint(ordered[0].transform.position).z;
        var front = new List<WheelCollider>(); var rear = new List<WheelCollider>();
        for (int i = 0; i < ordered.Count; i++)
        {
            float z = bus.transform.InverseTransformPoint(ordered[i].transform.position).z;
            if (frontZ - z <= 1.25f) front.Add(ordered[i]); else rear.Add(ordered[i]);
        }
        if (rear.Count == 0 && ordered.Count >= 4) { front.Clear(); rear.Clear(); front.Add(ordered[0]); front.Add(ordered[1]); for (int i = 2; i < ordered.Count; i++) rear.Add(ordered[i]); }
        bus.allWheels = ordered.ToArray(); bus.steerWheels = front.ToArray(); bus.driveWheels = rear.ToArray(); bus.rearWheels = rear.ToArray();
    }

    static void PrepareScene(GameObject selectedBus)
    {
        FindOrCreateSceneComponent<SceneBootstrap>("SceneBootstrap");
        MissionManager mission = FindOrCreateSceneComponent<MissionManager>("MissionManager");
        FindOrCreateSceneComponent<ScheduleManager>("ScheduleManager");
        FindOrCreateSceneComponent<ScoreTracker>("ScoreTracker");
        BusController bus = selectedBus != null ? selectedBus.GetComponent<BusController>() : FindAnyObjectByType<BusController>();
        if (bus != null) mission.busController = bus;
        EditorUtility.SetDirty(mission); EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        EditorUtility.DisplayDialog("RealBus Setup", "The open scene now has SceneBootstrap, MissionManager, ScheduleManager, and ScoreTracker. Assign a route or use the existing Gameplay scene before pressing Play.", "OK");
    }

    static bool ValidateBus(GameObject root, bool showResult)
    {
        var issues = new List<string>(); if (root == null) issues.Add("No bus root selected.");
        BusController bus = root != null ? root.GetComponent<BusController>() : null;
        if (bus == null) issues.Add("BusController is missing.");
        else
        {
            if (root.GetComponent<Rigidbody>() == null) issues.Add("Rigidbody is missing.");
            if (bus.engineData == null) issues.Add("EngineSystem asset is not assigned.");
            if (bus.transmissionData == null) issues.Add("TransmissionSystem asset is not assigned.");
            if (bus.allWheels == null || bus.allWheels.Length < 4) issues.Add("Add at least four positioned WheelColliders.");
            if (bus.steerWheels == null || bus.steerWheels.Length < 2) issues.Add("At least two front steering WheelColliders are required.");
            if (bus.driveWheels == null || bus.driveWheels.Length < 2) issues.Add("At least two rear drive WheelColliders are required.");
            Collider[] colliders = root.GetComponentsInChildren<Collider>(true); bool hasBody = false;
            for (int i = 0; i < colliders.Length; i++) if (!(colliders[i] is WheelCollider) && !colliders[i].isTrigger) { hasBody = true; break; }
            if (!hasBody) issues.Add("Add a non-trigger BoxCollider or MeshCollider around the bus body.");
            if (root.GetComponentsInChildren<Renderer>(true).Length == 0) issues.Add("No visible model Renderers were found below this root.");
        }
        if (showResult) EditorUtility.DisplayDialog("RealBus Bus Validation", issues.Count == 0 ? "Bus wiring looks ready. Save the scene or prefab, then test at low speed." : "Still needed:\n\n• " + string.Join("\n• ", issues), "OK");
        return issues.Count == 0;
    }

    static T GetOrAdd<T>(GameObject root) where T : Component { T component = root.GetComponent<T>(); return component != null ? component : Undo.AddComponent<T>(root); }
    static T FindOrCreateSceneComponent<T>(string objectName) where T : Component
    {
        T component = FindAnyObjectByType<T>();
        if (component != null) return component;
        GameObject created = new GameObject(objectName);
        Undo.RegisterCreatedObjectUndo(created, "Create " + objectName);
        return Undo.AddComponent<T>(created);
    }

    static T FindFirstAsset<T>() where T : Object
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] { "Assets/_Game" });
        return guids.Length > 0 ? AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guids[0])) : null;
    }
}
#endif
