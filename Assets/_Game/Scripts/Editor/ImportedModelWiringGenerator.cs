#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class ImportedModelWiringGenerator
{
    const string BusSources = "Assets/unity models/bus";
    const string StopSources = "Assets/unity models/busstop";
    const string GasStationSources = "Assets/unity models/gasstation";
    const string RoadSources = "Assets/unity models/roads";
    const string BusVisuals = "Assets/_Game/Generated/Models/Buses";
    const string BusPrefabs = "Assets/_Game/Generated/Prefabs/Buses";
    const string StopPrefabs = "Assets/_Game/Resources/BusStopsGenerated";
    const string GasStationPrefabs = "Assets/_Game/Resources/GasStationsGenerated";
    const string RoadPrefabs = "Assets/_Game/Resources/RoadsGenerated";
    const string BusSpecs = "Assets/_Game/Resources/BusSpecs/Imported";
    const string ReportPath = "Assets/_Game/Generated/IMPORTED_MODEL_WIRING.md";

    static bool generating;

    [InitializeOnLoadMethod]
    static void QueueMissingModelWiring()
    {
        EditorApplication.delayCall += () =>
        {
            if (!Application.isPlaying && NeedsGeneration()) GenerateAll(false);
        };
    }

    [MenuItem("Tools/RealBus/Setup/Wire All Imported Models")]
    public static void GenerateFromMenu()
    {
        GenerateAll(true);
    }

    public static void GenerateFromBatch()
    {
        GenerateAll(false);
        RouteAssetRegistry.RepairImportedRoutes(true);
    }

    public static void GenerateRoadsFromBatch()
    {
        if (generating) return;
        generating = true;
        try
        {
            EnsureFolders();
            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
            foreach (string roadPath in FindFbxFiles(RoadSources))
            {
                string output = GenerateRoad(roadPath, out string message);
                if (string.IsNullOrEmpty(output))
                    Debug.LogWarning($"[RealBus] Road model '{roadPath}' was skipped: {message}");
                else
                    Debug.Log($"[RealBus] Generated road surface prefab: {output}");
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        finally
        {
            generating = false;
        }
    }

    public static void GenerateAll(bool showDialog)
    {
        if (generating) return;
        generating = true;
        try
        {
            EnsureFolders();
            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);

            string[] busPaths = FindFbxFiles(BusSources);
            string[] stopPaths = FindFbxFiles(StopSources);
            string[] gasStationPaths = FindFbxFiles(GasStationSources);
            string[] roadPaths = FindFbxFiles(RoadSources);
            var report = new StringBuilder("# Imported model wiring\n\n");
            report.AppendLine("Generated from `Assets/unity models`. Re-run `Tools > RealBus > Setup > Wire All Imported Bus and Stop Models` after replacing a source FBX.\n");
            report.AppendLine("## Drivable buses\n");

            for (int i = 0; i < busPaths.Length; i++)
            {
                WiredBusResult result = GenerateBus(busPaths[i], i);
                report.AppendLine(result.success
                    ? $"- **{result.displayName}** — `{result.prefabPath}`; garage spec `{result.specPath}`"
                    : $"- **{Path.GetFileName(busPaths[i])}** — skipped: {result.message}");
            }

            report.AppendLine("\n## Bus-stop props\n");
            for (int i = 0; i < stopPaths.Length; i++)
            {
                string output = GenerateStop(stopPaths[i], out string message);
                report.AppendLine(!string.IsNullOrEmpty(output)
                    ? $"- **{ObjectNames.NicifyVariableName(Path.GetFileNameWithoutExtension(stopPaths[i]))}** — `{output}`"
                    : $"- **{Path.GetFileName(stopPaths[i])}** — skipped: {message}");
            }

            report.AppendLine("\n## Gas-station props\n");
            for (int i = 0; i < gasStationPaths.Length; i++)
            {
                string output = GenerateGasStation(gasStationPaths[i], out string message);
                report.AppendLine(!string.IsNullOrEmpty(output)
                    ? $"- **{ObjectNames.NicifyVariableName(Path.GetFileNameWithoutExtension(gasStationPaths[i]))}** — `{output}`"
                    : $"- **{Path.GetFileName(gasStationPaths[i])}** — skipped: {message}");
            }

            report.AppendLine("\n## Road surfaces\n");
            for (int i = 0; i < roadPaths.Length; i++)
            {
                string output = GenerateRoad(roadPaths[i], out string message);
                report.AppendLine(!string.IsNullOrEmpty(output)
                    ? $"- **{ObjectNames.NicifyVariableName(Path.GetFileNameWithoutExtension(roadPaths[i]))}** — `{output}`"
                    : $"- **{Path.GetFileName(roadPaths[i])}** — skipped: {message}");
            }

            File.WriteAllText(ReportPath, report.ToString());
            AssetDatabase.ImportAsset(ReportPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[RealBus] Wired {busPaths.Length} buses, {stopPaths.Length} bus stops, {gasStationPaths.Length} gas stations, and {roadPaths.Length} road packs. See {ReportPath}.");
            if (showDialog)
                EditorUtility.DisplayDialog("RealBus model wiring", $"Processed {busPaths.Length} buses, {stopPaths.Length} stop props, {gasStationPaths.Length} gas stations, and {roadPaths.Length} road packs.\n\nSee {ReportPath} for the generated asset paths.", "OK");
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
            if (showDialog) EditorUtility.DisplayDialog("RealBus model wiring failed", exception.Message, "OK");
        }
        finally
        {
            generating = false;
        }
    }

    static bool NeedsGeneration()
    {
        if (!Directory.Exists(BusSources) || !Directory.Exists(StopSources)) return false;
        int busCount = FindFbxFiles(BusSources).Length;
        int stopCount = FindFbxFiles(StopSources).Length;
        int gasStationCount = FindFbxFiles(GasStationSources).Length;
        int roadCount = FindFbxFiles(RoadSources).Length;
        int wiredBuses = Directory.Exists(BusPrefabs) ? Directory.GetFiles(BusPrefabs, "*.prefab").Length : 0;
        int wiredStops = Directory.Exists(StopPrefabs) ? Directory.GetFiles(StopPrefabs, "*.prefab").Length : 0;
        int wiredGasStations = Directory.Exists(GasStationPrefabs) ? Directory.GetFiles(GasStationPrefabs, "*.prefab").Length : 0;
        int wiredRoads = Directory.Exists(RoadPrefabs) ? Directory.GetFiles(RoadPrefabs, "*.prefab").Length : 0;
        if (busCount > wiredBuses || stopCount > wiredStops || gasStationCount > wiredGasStations || roadCount > wiredRoads || !File.Exists(ReportPath)) return true;
        foreach (string sourcePath in FindFbxFiles(BusSources))
        {
            string specPath = $"{BusSpecs}/{UniqueStem(sourcePath)}.asset";
            BusSpec spec = AssetDatabase.LoadAssetAtPath<BusSpec>(specPath);
            if (spec == null || spec.drivablePrefab == null) return true;
        }
        return false;
    }

    static WiredBusResult GenerateBus(string sourcePath, int index)
    {
        GameObject source = AssetDatabase.LoadAssetAtPath<GameObject>(sourcePath);
        if (source == null) return WiredBusResult.Failed("Unity did not import this FBX as a GameObject.");

        string displayName = ObjectNames.NicifyVariableName(Path.GetFileNameWithoutExtension(sourcePath));
        string stem = UniqueStem(sourcePath);
        string visualPath = $"{BusVisuals}/{stem}_Visual.prefab";
        string prefabPath = $"{BusPrefabs}/{stem}_Drivable.prefab";
        string specPath = $"{BusSpecs}/{stem}.asset";

        GameObject visualRoot = new GameObject(displayName + " Visual");
        GameObject model = PrefabUtility.InstantiatePrefab(source) as GameObject;
        if (model == null)
        {
            UnityEngine.Object.DestroyImmediate(visualRoot);
            return WiredBusResult.Failed("The model could not be instantiated.");
        }

        model.name = "Model";
        model.transform.SetParent(visualRoot.transform, false);
        StripImportedPhysics(model);
        float targetLength = TargetBusLength(displayName);
        if (!NormalizeBusVisual(visualRoot, model.transform, targetLength, out Bounds visualBounds))
        {
            UnityEngine.Object.DestroyImmediate(visualRoot);
            return WiredBusResult.Failed("No renderable mesh was found.");
        }

        PrefabUtility.SaveAsPrefabAsset(visualRoot, visualPath);
        UnityEngine.Object.DestroyImmediate(visualRoot);
        GameObject visualPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(visualPath);
        if (visualPrefab == null) return WiredBusResult.Failed("The visual prefab could not be saved.");

        GameObject root = new GameObject(displayName + " Drivable");
        Rigidbody body = root.AddComponent<Rigidbody>();
        body.mass = Mathf.Lerp(7600f, 15000f, Mathf.InverseLerp(8f, 14f, visualBounds.size.z));
        body.linearDamping = 0.15f;
        body.angularDamping = 0.5f;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        GameObject mount = new GameObject("ModelRoot");
        mount.transform.SetParent(root.transform, false);
        PrefabUtility.InstantiatePrefab(visualPrefab, mount.transform);
        Bounds bounds = GetBounds(root);
        AddBusBodyCollider(root, bounds);

        float width = Mathf.Max(1.8f, bounds.size.x);
        float length = Mathf.Max(6f, bounds.size.z);
        float radius = Mathf.Clamp(bounds.size.y * 0.15f, 0.32f, 0.58f);
        float leftX = -width * 0.39f;
        float rightX = width * 0.39f;
        float frontZ = bounds.center.z + length * 0.31f;
        float rearZ = bounds.center.z - length * 0.31f;
        GameObject wheelRoot = new GameObject("WheelColliders");
        wheelRoot.transform.SetParent(root.transform, false);
        WheelCollider frontLeft = CreateWheel(wheelRoot.transform, "FrontLeft", new Vector3(leftX, radius, frontZ), radius, body.mass, 4);
        WheelCollider frontRight = CreateWheel(wheelRoot.transform, "FrontRight", new Vector3(rightX, radius, frontZ), radius, body.mass, 4);
        WheelCollider rearLeft = CreateWheel(wheelRoot.transform, "RearLeft", new Vector3(leftX, radius, rearZ), radius, body.mass, 4);
        WheelCollider rearRight = CreateWheel(wheelRoot.transform, "RearRight", new Vector3(rightX, radius, rearZ), radius, body.mass, 4);

        BusController bus = root.AddComponent<BusController>();
        bus.engineData = AssetDatabase.LoadAssetAtPath<EngineSystem>("Assets/_Game/ScriptableObjects/EngineSystem.asset");
        bus.transmissionData = AssetDatabase.LoadAssetAtPath<TransmissionSystem>("Assets/_Game/ScriptableObjects/TransmissionSystem.asset");
        bus.modelRoot = mount.transform;
        bus.allWheels = new[] { frontLeft, frontRight, rearLeft, rearRight };
        bus.steerWheels = new[] { frontLeft, frontRight };
        bus.driveWheels = new[] { rearLeft, rearRight };
        bus.rearWheels = new[] { rearLeft, rearRight };
        bus.wheelbase = frontZ - rearZ;
        bus.trackWidth = rightX - leftX;
        bus.centerOfMassY = Mathf.Clamp(bounds.size.y * 0.38f, 1.1f, 1.8f);

        GameObject docking = new GameObject("DockingReference");
        docking.transform.SetParent(root.transform, false);
        docking.transform.localPosition = new Vector3(width * 0.48f, 0.45f, bounds.center.z - length * 0.15f);
        bus.dockingReference = docking.transform;

        EngineTemperatureSystem temperature = root.AddComponent<EngineTemperatureSystem>(); temperature.bus = bus;
        root.AddComponent<PassengerLoadDynamics>();
        RoadSurfaceDetector detector = root.AddComponent<RoadSurfaceDetector>(); detector.busController = bus;
        RoadSurfaceFrictionController friction = root.AddComponent<RoadSurfaceFrictionController>(); friction.busController = bus; friction.surfaceDetector = detector;
        MaintenanceSystem maintenance = root.AddComponent<MaintenanceSystem>(); maintenance.busController = bus;
        FuelSystem fuel = root.AddComponent<FuelSystem>(); fuel.busController = bus;
        DrivingAssistSystem assists = root.AddComponent<DrivingAssistSystem>(); assists.bus = bus;
        root.AddComponent<CosmeticDamageSystem>();
        VehiclePolishEffects polish = root.AddComponent<VehiclePolishEffects>(); polish.bus = bus;

        float configuredMass = body.mass;
        float configuredCenterOfMassY = bus.centerOfMassY;
        PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
        UnityEngine.Object.DestroyImmediate(root);

        BusSpec spec = AssetDatabase.LoadAssetAtPath<BusSpec>(specPath);
        if (spec == null)
        {
            spec = ScriptableObject.CreateInstance<BusSpec>();
            AssetDatabase.CreateAsset(spec, specPath);
        }
        spec.name = displayName;
        string sourceGuid = AssetDatabase.AssetPathToGUID(sourcePath);
        spec.busId = "fleet.imported." + (string.IsNullOrEmpty(sourceGuid) ? stem.ToLowerInvariant() : sourceGuid.Substring(0, 12).ToLowerInvariant());
        spec.displayName = displayName;
        spec.description = "Imported, game-ready bus model.";
        // Keep one model-backed bus available from the first drive. Later imports
        // still participate in the normal rank progression.
        spec.requiredRank = index == 0 ? 1 : 2 + index / 2;
        spec.modelPrefab = visualPrefab;
        spec.drivablePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        spec.bodyLengthMeters = length;
        spec.rigidbodyMassKg = configuredMass;
        spec.centerOfMassY = configuredCenterOfMassY;
        spec.engineProfile = bus.engineData;
        spec.transmissionProfile = bus.transmissionData;
        EditorUtility.SetDirty(spec);

        return new WiredBusResult(true, displayName, prefabPath, specPath, string.Empty);
    }

    static string GenerateStop(string sourcePath, out string message)
    {
        GameObject source = AssetDatabase.LoadAssetAtPath<GameObject>(sourcePath);
        if (source == null) { message = "Unity did not import this FBX as a GameObject."; return string.Empty; }
        string displayName = ObjectNames.NicifyVariableName(Path.GetFileNameWithoutExtension(sourcePath));
        string output = $"{StopPrefabs}/{UniqueStem(sourcePath)}.prefab";
        GameObject root = new GameObject(displayName);
        GameObject model = PrefabUtility.InstantiatePrefab(source) as GameObject;
        if (model == null) { UnityEngine.Object.DestroyImmediate(root); message = "The model could not be instantiated."; return string.Empty; }
        model.name = "Model";
        model.transform.SetParent(root.transform, false);
        StripImportedPhysics(model);
        if (!NormalizeStopVisual(root, model.transform, displayName, out Bounds bounds))
        {
            UnityEngine.Object.DestroyImmediate(root); message = "No renderable mesh was found."; return string.Empty;
        }
        BoxCollider collider = root.AddComponent<BoxCollider>();
        collider.center = bounds.center;
        collider.size = new Vector3(Mathf.Max(0.08f, bounds.size.x), Mathf.Max(0.08f, bounds.size.y), Mathf.Max(0.08f, bounds.size.z));
        PrefabUtility.SaveAsPrefabAsset(root, output);
        UnityEngine.Object.DestroyImmediate(root);
        message = string.Empty;
        return output;
    }

    static string GenerateGasStation(string sourcePath, out string message)
    {
        GameObject source = AssetDatabase.LoadAssetAtPath<GameObject>(sourcePath);
        if (source == null) { message = "Unity did not import this FBX as a GameObject."; return string.Empty; }
        string displayName = ObjectNames.NicifyVariableName(Path.GetFileNameWithoutExtension(sourcePath));
        string output = $"{GasStationPrefabs}/{UniqueStem(sourcePath)}.prefab";
        GameObject root = new GameObject(displayName);
        GameObject model = PrefabUtility.InstantiatePrefab(source) as GameObject;
        if (model == null) { UnityEngine.Object.DestroyImmediate(root); message = "The model could not be instantiated."; return string.Empty; }
        model.name = "Model";
        model.transform.SetParent(root.transform, false);
        StripImportedPhysics(model);
        if (!NormalizeGasStationVisual(root, model.transform, displayName, out Bounds bounds))
        {
            UnityEngine.Object.DestroyImmediate(root); message = "No renderable mesh was found."; return string.Empty;
        }
        BoxCollider collider = root.AddComponent<BoxCollider>();
        collider.center = bounds.center;
        collider.size = new Vector3(Mathf.Max(.2f, bounds.size.x), Mathf.Max(.2f, bounds.size.y), Mathf.Max(.2f, bounds.size.z));
        PrefabUtility.SaveAsPrefabAsset(root, output);
        UnityEngine.Object.DestroyImmediate(root);
        message = string.Empty;
        return output;
    }

    static string GenerateRoad(string sourcePath, out string message)
    {
        GameObject source = AssetDatabase.LoadAssetAtPath<GameObject>(sourcePath);
        if (source == null) { message = "Unity did not import this FBX as a GameObject."; return string.Empty; }

        GameObject sourceInstance = PrefabUtility.InstantiatePrefab(source) as GameObject;
        if (sourceInstance == null) { message = "The road pack could not be instantiated."; return string.Empty; }

        Renderer best = null;
        float bestScore = float.MinValue;
        foreach (Renderer renderer in sourceInstance.GetComponentsInChildren<Renderer>(true))
        {
            Bounds bounds = renderer.bounds;
            float shortSide = Mathf.Min(bounds.size.x, bounds.size.z);
            float longSide = Mathf.Max(bounds.size.x, bounds.size.z);
            if (shortSide <= .001f || longSide <= .01f) continue;
            float aspect = longSide / shortSide;
            float flatness = longSide / Mathf.Max(.01f, bounds.size.y);
            float score = aspect * 4f + flatness + Mathf.Log10(1f + longSide * shortSide);
            if (score > bestScore) { bestScore = score; best = renderer; }
        }

        if (best == null)
        {
            UnityEngine.Object.DestroyImmediate(sourceInstance);
            message = "No usable road mesh was found.";
            return string.Empty;
        }

        string output = $"{RoadPrefabs}/{UniqueStem(sourcePath)}.prefab";
        GameObject root = new GameObject(ObjectNames.NicifyVariableName(Path.GetFileNameWithoutExtension(sourcePath)) + " Road Surface");
        GameObject model = UnityEngine.Object.Instantiate(best.gameObject);
        model.name = "Model";
        model.transform.SetParent(root.transform, false);
        model.transform.localPosition = Vector3.zero;
        StripImportedPhysics(model);

        Bounds roadBounds = GetBounds(root);
        if (roadBounds.size.x > roadBounds.size.z)
            model.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
        roadBounds = GetBounds(root);
        float widthScale = 7f / Mathf.Max(.01f, roadBounds.size.x);
        model.transform.localScale *= widthScale;
        roadBounds = GetBounds(root);
        model.transform.localPosition += new Vector3(-roadBounds.center.x, -roadBounds.min.y, -roadBounds.center.z);

        PrefabUtility.SaveAsPrefabAsset(root, output);
        UnityEngine.Object.DestroyImmediate(root);
        UnityEngine.Object.DestroyImmediate(sourceInstance);
        message = string.Empty;
        return output;
    }

    static bool NormalizeBusVisual(GameObject root, Transform model, float targetLength, out Bounds bounds)
    {
        bounds = GetBounds(root);
        if (!HasUsableBounds(bounds)) return false;
        if (bounds.size.x > bounds.size.z) model.localRotation = Quaternion.Euler(0f, -90f, 0f);
        bounds = GetBounds(root);
        float scale = targetLength / Mathf.Max(0.001f, bounds.size.z);
        model.localScale *= scale;
        bounds = GetBounds(root);
        model.localPosition += new Vector3(-bounds.center.x, -bounds.min.y, -bounds.center.z);
        bounds = GetBounds(root);
        return HasUsableBounds(bounds);
    }

    static bool NormalizeStopVisual(GameObject root, Transform model, string displayName, out Bounds bounds)
    {
        bounds = GetBounds(root);
        if (!HasUsableBounds(bounds)) return false;
        float target = displayName.IndexOf("bench", StringComparison.OrdinalIgnoreCase) >= 0 ? 2.1f : 3f;
        float sourceMeasure = displayName.IndexOf("bench", StringComparison.OrdinalIgnoreCase) >= 0
            ? Mathf.Max(bounds.size.x, bounds.size.z)
            : bounds.size.y;
        if (sourceMeasure < 0.4f || sourceMeasure > 8f) model.localScale *= target / Mathf.Max(0.001f, sourceMeasure);
        bounds = GetBounds(root);
        model.localPosition += new Vector3(-bounds.center.x, -bounds.min.y, -bounds.center.z);
        bounds = GetBounds(root);
        return HasUsableBounds(bounds);
    }

    static bool NormalizeGasStationVisual(GameObject root, Transform model, string displayName, out Bounds bounds)
    {
        bounds = GetBounds(root);
        if (!HasUsableBounds(bounds)) return false;
        bool pumpOnly = string.Equals(displayName.Replace(" ", ""), "Gas", StringComparison.OrdinalIgnoreCase);
        float sourceMeasure = pumpOnly ? bounds.size.y : Mathf.Max(bounds.size.x, bounds.size.z);
        float targetMeasure = pumpOnly ? 2.2f : 18f;
        model.localScale *= targetMeasure / Mathf.Max(.001f, sourceMeasure);
        bounds = GetBounds(root);
        model.localPosition += new Vector3(-bounds.center.x, -bounds.min.y, -bounds.center.z);
        bounds = GetBounds(root);
        return HasUsableBounds(bounds);
    }

    static void AddBusBodyCollider(GameObject root, Bounds bounds)
    {
        BoxCollider collider = root.AddComponent<BoxCollider>();
        collider.center = new Vector3(bounds.center.x, bounds.min.y + bounds.size.y * 0.54f, bounds.center.z);
        collider.size = new Vector3(bounds.size.x * 0.9f, bounds.size.y * 0.82f, bounds.size.z * 0.92f);
    }

    static WheelCollider CreateWheel(Transform parent, string name, Vector3 position, float radius, float vehicleMass, int wheelCount)
    {
        GameObject wheelObject = new GameObject(name);
        wheelObject.transform.SetParent(parent, false);
        wheelObject.transform.localPosition = position;
        WheelCollider wheel = wheelObject.AddComponent<WheelCollider>();
        wheel.radius = radius;
        wheel.mass = 65f;
        wheel.suspensionDistance = Mathf.Clamp(radius * 0.55f, 0.18f, 0.32f);
        wheel.forceAppPointDistance = Mathf.Clamp(radius * 0.4f, 0.12f, 0.25f);
        JointSpring spring = wheel.suspensionSpring;
        float loadPerWheel = Mathf.Max(1000f, vehicleMass) * Physics.gravity.magnitude / Mathf.Max(1, wheelCount);
        spring.spring = Mathf.Clamp(loadPerWheel / Mathf.Max(0.06f, wheel.suspensionDistance * 0.45f), 60000f, 320000f);
        spring.damper = Mathf.Max(5200f, 2f * Mathf.Sqrt(spring.spring * wheel.mass) * 0.65f);
        spring.targetPosition = 0.5f;
        wheel.suspensionSpring = spring;
        wheel.ConfigureVehicleSubsteps(5f, 12, 15);
        return wheel;
    }

    static void StripImportedPhysics(GameObject model)
    {
        foreach (Camera camera in model.GetComponentsInChildren<Camera>(true)) UnityEngine.Object.DestroyImmediate(camera);
        foreach (AudioListener listener in model.GetComponentsInChildren<AudioListener>(true)) UnityEngine.Object.DestroyImmediate(listener);
        foreach (Collider collider in model.GetComponentsInChildren<Collider>(true)) UnityEngine.Object.DestroyImmediate(collider);
        foreach (Rigidbody body in model.GetComponentsInChildren<Rigidbody>(true)) UnityEngine.Object.DestroyImmediate(body);
    }

    static Bounds GetBounds(GameObject root)
    {
        Renderer[] renderers = root.GetComponentsInChildren<Renderer>(true);
        if (renderers.Length == 0) return new Bounds(Vector3.zero, Vector3.zero);
        Bounds bounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++) bounds.Encapsulate(renderers[i].bounds);
        return bounds;
    }

    static bool HasUsableBounds(Bounds bounds)
    {
        return bounds.size.sqrMagnitude > 0.000001f && IsFinite(bounds.size.x) && IsFinite(bounds.size.y) && IsFinite(bounds.size.z);
    }

    static bool IsFinite(float value) => !float.IsNaN(value) && !float.IsInfinity(value);

    static float TargetBusLength(string displayName)
    {
        if (displayName.IndexOf("toy", StringComparison.OrdinalIgnoreCase) >= 0) return 8.5f;
        if (displayName.IndexOf("cito", StringComparison.OrdinalIgnoreCase) >= 0) return 9.5f;
        return 12f;
    }

    static string[] FindFbxFiles(string folder)
    {
        if (!Directory.Exists(folder)) return Array.Empty<string>();
        return Directory.GetFiles(folder, "*", SearchOption.TopDirectoryOnly)
            .Where(path => string.Equals(Path.GetExtension(path), ".fbx", StringComparison.OrdinalIgnoreCase))
            .Select(path => path.Replace('\\', '/'))
            .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    static string UniqueStem(string sourcePath)
    {
        string safe = string.Concat(Path.GetFileNameWithoutExtension(sourcePath).Select(c => char.IsLetterOrDigit(c) ? c : '_')).Trim('_');
        string guid = AssetDatabase.AssetPathToGUID(sourcePath);
        string suffix = string.IsNullOrEmpty(guid) ? "model" : guid.Substring(0, 8);
        return (string.IsNullOrEmpty(safe) ? "Model" : safe) + "_" + suffix;
    }

    static void EnsureFolders()
    {
        Directory.CreateDirectory(BusVisuals);
        Directory.CreateDirectory(BusPrefabs);
        Directory.CreateDirectory(StopPrefabs);
        Directory.CreateDirectory(GasStationPrefabs);
        Directory.CreateDirectory(RoadPrefabs);
        Directory.CreateDirectory(BusSpecs);
    }

    readonly struct WiredBusResult
    {
        public readonly bool success;
        public readonly string displayName;
        public readonly string prefabPath;
        public readonly string specPath;
        public readonly string message;

        public WiredBusResult(bool success, string displayName, string prefabPath, string specPath, string message)
        {
            this.success = success;
            this.displayName = displayName;
            this.prefabPath = prefabPath;
            this.specPath = specPath;
            this.message = message;
        }

        public static WiredBusResult Failed(string message) => new WiredBusResult(false, string.Empty, string.Empty, string.Empty, message);
    }
}
#endif
