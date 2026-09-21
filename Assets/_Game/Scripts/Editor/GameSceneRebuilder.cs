#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class GameSceneRebuilder
{
    const string ScenePath = "Assets/_Game/Scenes/GameScene.unity";
    const string OriginAssetPath = "Assets/_Game/ScriptableObjects/Map/GameSceneMapOrigin.asset";

    [MenuItem("Tools/RealBus/Setup/Rebuild GameScene Diagnostics")]
    public static void RebuildGameSceneMenu()
    {
        if (!EditorUtility.DisplayDialog(
            "Rebuild GameScene",
            "This will recreate Assets/_Game/Scenes/GameScene.unity with a gameplay scaffold and imported-asset diagnostics.",
            "Rebuild",
            "Cancel"))
        {
            return;
        }

        RebuildGameScene();
    }

    public static void RebuildGameSceneFromBatch()
    {
        RebuildGameScene();
    }

    public static void RebuildGameScene()
    {
        ImportedModelWiringGenerator.GenerateAll(false);

        Directory.CreateDirectory(Path.GetDirectoryName(ScenePath));
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        scene.name = "GameScene";

        var city = LoadPreferredCity();
        var country = LoadCountryForCity(city);
        var origin = EnsureMapOrigin(city);

        CreateGlobalManagers(country, city);
        var mapSystem = CreateMapSystem(city, origin);
        var bus = CreatePlayerBus();
        CreateFallbackGround();
        CreateCameraAndLighting(bus);
        CreateGameplayCanvas(bus, mapSystem);
        CreateDiagnosticShowcase();

        EditorSceneManager.SaveScene(scene, ScenePath);
        EnsureSceneInBuildSettings(ScenePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("[RealBus] Rebuilt GameScene with imported bus, stop, gas station, road, OSM, and gameplay diagnostics.");
    }

    static void CreateGlobalManagers(CountryDefinition country, CityDefinition city)
    {
        var gameState = new GameObject("GameState").AddComponent<GameState>();
        gameState.selectedCountry = country;
        gameState.selectedCity = city;
        gameState.selectedRoute = FirstRoute(city);

        var cityManager = new GameObject("CityManager").AddComponent<CityManager>();
        cityManager.allCountries = LoadAssets<CountryDefinition>("Assets/_Game/ScriptableObjects");
        cityManager.allCities = LoadAssets<CityDefinition>("Assets/_Game/ScriptableObjects");
        cityManager.activeCountry = country ?? cityManager.allCountries.FirstOrDefault();
        cityManager.activeCity = city ?? cityManager.allCities.FirstOrDefault();
    }

    static GameObject CreateMapSystem(CityDefinition city, MapOrigin origin)
    {
        var root = new GameObject("MapSystem");

        var converter = root.AddComponent<CoordinateConverter>();
        converter.mapOrigin = origin;

        var tiles = root.AddComponent<MapTileLoader>();
        tiles.loadOnAwake = false;
        tiles.centreLat = city != null ? city.centreLat : -1.2864d;
        tiles.centreLon = city != null ? city.centreLon : 36.8172d;
        tiles.zoomLevel = 17;
        tiles.tilesX = 7;
        tiles.tilesY = 7;
        tiles.tileWorldSize = 220f;
        tiles.tileSurfaceY = 0.12f;
        tiles.forceSharpTileFiltering = true;
        converter.mapTileLoader = tiles;

        root.AddComponent<GPSManager>();
        root.AddComponent<PassengerManager>().passengerData = FindAsset<PassengerData>("PassengerData");

        var mission = root.AddComponent<MissionManager>();
        mission.currentRoute = FirstRoute(city);
        mission.missionData = FindAsset<MissionData>("MissionData");
        mission.countdownSeconds = 3;

        var freeDrive = root.AddComponent<FreeDriveSession>();
        freeDrive.autoStart = true;
        freeDrive.fuelCapacityLitres = 300f;
        freeDrive.fuelConsumptionPer100km = 35f;

        root.AddComponent<ScheduleManager>();
        root.AddComponent<ScoreTracker>();
        root.AddComponent<OSMLoader>();
        root.AddComponent<OSMBuildingLoader>();

        var roads = root.AddComponent<OSMRoadMeshBuilder>();
        roads.renderRoadSurface = true;
        roads.drawCenterLines = true;
        roads.useRoadModelInstances = true;
        roads.roadModelResourcePath = "RoadsGenerated";
        roads.overrideRoadModelMaterials = true;
        roads.generateRoadBoundaries = true;
        roads.addPhysicalBoundaryColliders = true;
        roads.roadYOffset = 0.25f;
        roads.maxColliderSegmentLength = 40f;

        var buildings = root.AddComponent<OSMBuildingMeshBuilder>();
        buildings.streamAroundVehicle = true;
        buildings.buildingLoadDistance = 650f;
        buildings.buildingUnloadDistance = 850f;
        buildings.maxBuildingsPerFrame = 500;

        root.AddComponent<OSMRouteImporter>();
        root.AddComponent<OsmCityPopulationSystem>();
        root.AddComponent<RuntimeGasStationSpawner>();
        root.AddComponent<RuntimeOsmPoiVisualizer>();
        root.AddComponent<RuntimeTarmacApplier>();

        var bootstrap = root.AddComponent<SceneBootstrap>();
        SetSerializedBool(bootstrap, "redirectIfNoManagers", false);
        SetSerializedBool(bootstrap, "spawnSupplementalWorldProps", true);
        SetSerializedBool(bootstrap, "spawnDebugPoiMarkers", true);
        SetSerializedBool(bootstrap, "suppressBuildingsForCurrentPhase", false);
        SetSerializedBool(bootstrap, "autoStartSelectedRoute", true);

        return root;
    }

    static BusController CreatePlayerBus()
    {
        var prefab = FindFirstImportedDrivablePrefab();
        GameObject instance = prefab != null
            ? PrefabUtility.InstantiatePrefab(prefab) as GameObject
            : CreateFallbackBusObject();

        if (instance == null)
            instance = CreateFallbackBusObject();

        instance.name = "PlayerBus_ImportedPrefabTest";
        instance.transform.position = new Vector3(0f, 1.25f, 0f);
        instance.transform.rotation = Quaternion.identity;

        var bus = instance.GetComponent<BusController>();
        if (bus == null)
            bus = instance.AddComponent<BusController>();

        if (bus.engineData == null)
            bus.engineData = FindAsset<EngineSystem>("EngineSystem") ?? FindAsset<EngineSystem>("Phase2EngineSystem");
        if (bus.transmissionData == null)
            bus.transmissionData = FindAsset<TransmissionSystem>("TransmissionSystem") ?? FindAsset<TransmissionSystem>("Phase2TransmissionSystem");

        var body = instance.GetComponent<Rigidbody>();
        if (body == null)
            body = instance.AddComponent<Rigidbody>();
        body.mass = Mathf.Max(9000f, body.mass);
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        if (bus.dockingReference == null)
        {
            var dock = new GameObject("DockingReference");
            dock.transform.SetParent(instance.transform, false);
            dock.transform.localPosition = new Vector3(1.2f, 0.45f, -1.5f);
            bus.dockingReference = dock.transform;
        }

        if (UnityEngine.Object.FindAnyObjectByType<MissionManager>() is MissionManager mission)
            mission.busController = bus;

        EnsureVehicleSupport(bus);
        return bus;
    }

    static GameObject CreateFallbackBusObject()
    {
        var root = new GameObject("Fallback Drivable Bus");
        var body = root.AddComponent<Rigidbody>();
        body.mass = 12000f;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        var visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
        visual.name = "Fallback Body";
        visual.transform.SetParent(root.transform, false);
        visual.transform.localPosition = new Vector3(0f, 1.35f, 0f);
        visual.transform.localScale = new Vector3(2.4f, 2.2f, 9f);
        visual.GetComponent<Renderer>().sharedMaterial = CreateRuntimeMaterial("FallbackBusBody", new Color(0.1f, 0.45f, 0.22f));

        var wheelsRoot = new GameObject("WheelColliders");
        wheelsRoot.transform.SetParent(root.transform, false);
        var fl = CreateWheel(wheelsRoot.transform, "FrontLeft", new Vector3(-0.95f, 0.48f, 2.8f));
        var fr = CreateWheel(wheelsRoot.transform, "FrontRight", new Vector3(0.95f, 0.48f, 2.8f));
        var rl = CreateWheel(wheelsRoot.transform, "RearLeft", new Vector3(-0.95f, 0.48f, -2.8f));
        var rr = CreateWheel(wheelsRoot.transform, "RearRight", new Vector3(0.95f, 0.48f, -2.8f));

        var bus = root.AddComponent<BusController>();
        bus.modelRoot = visual.transform;
        bus.allWheels = new[] { fl, fr, rl, rr };
        bus.steerWheels = new[] { fl, fr };
        bus.driveWheels = new[] { rl, rr };
        bus.rearWheels = new[] { rl, rr };
        bus.wheelbase = 5.6f;
        bus.trackWidth = 1.9f;
        return root;
    }

    static WheelCollider CreateWheel(Transform parent, string name, Vector3 localPosition)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        go.transform.localPosition = localPosition;
        var wheel = go.AddComponent<WheelCollider>();
        wheel.radius = 0.45f;
        wheel.suspensionDistance = 0.28f;
        return wheel;
    }

    static void EnsureVehicleSupport(BusController bus)
    {
        if (bus == null) return;
        if (bus.GetComponent<EngineTemperatureSystem>() == null) bus.gameObject.AddComponent<EngineTemperatureSystem>().bus = bus;
        if (bus.GetComponent<PassengerLoadDynamics>() == null) bus.gameObject.AddComponent<PassengerLoadDynamics>();
        if (bus.GetComponent<RoadSurfaceDetector>() == null) bus.gameObject.AddComponent<RoadSurfaceDetector>().busController = bus;
        if (bus.GetComponent<RoadSurfaceFrictionController>() == null)
        {
            var friction = bus.gameObject.AddComponent<RoadSurfaceFrictionController>();
            friction.busController = bus;
            friction.surfaceDetector = bus.GetComponent<RoadSurfaceDetector>();
        }
        if (bus.GetComponent<FuelSystem>() == null && bus.GetComponent<BatterySystem>() == null) bus.gameObject.AddComponent<FuelSystem>().busController = bus;
        if (bus.GetComponent<DrivingAssistSystem>() == null) bus.gameObject.AddComponent<DrivingAssistSystem>().bus = bus;
        if (bus.GetComponent<ImportedVehicleVisualRepair>() == null) bus.gameObject.AddComponent<ImportedVehicleVisualRepair>();
    }

    static void CreateFallbackGround()
    {
        var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.name = "Fallback_Test_Plane";
        plane.transform.localScale = new Vector3(160f, 1f, 160f);
        var renderer = plane.GetComponent<Renderer>();
        renderer.sharedMaterial = CreateRuntimeMaterial("FallbackGround", new Color(0.18f, 0.2f, 0.18f));
        var collider = plane.GetComponent<Collider>();
        if (collider != null)
            collider.enabled = true;
    }

    static void CreateCameraAndLighting(BusController bus)
    {
        var sun = new GameObject("Directional Light").AddComponent<Light>();
        sun.type = LightType.Directional;
        sun.intensity = 1.2f;
        sun.transform.rotation = Quaternion.Euler(45f, -35f, 0f);
        RenderSettings.sun = sun;
        RenderSettings.ambientLight = new Color(0.62f, 0.66f, 0.72f);

        var cameraGo = new GameObject("Main Camera");
        var camera = cameraGo.AddComponent<Camera>();
        camera.tag = "MainCamera";
        camera.fieldOfView = 62f;
        camera.nearClipPlane = 0.05f;
        camera.farClipPlane = 4000f;
        cameraGo.AddComponent<AudioListener>();

        if (bus != null)
        {
            cameraGo.transform.SetParent(bus.transform, false);
            cameraGo.transform.localPosition = new Vector3(0f, 4.0f, -9.0f);
            cameraGo.transform.localRotation = Quaternion.Euler(14f, 0f, 0f);
        }
        else
        {
            cameraGo.transform.SetPositionAndRotation(new Vector3(0f, 6f, -12f), Quaternion.Euler(18f, 0f, 0f));
        }
    }

    static void CreateGameplayCanvas(BusController bus, GameObject mapSystem)
    {
        var canvasGo = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        var canvas = canvasGo.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGo.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasGo.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920f, 1080f);

        var controlsRoot = new GameObject("MobileControlsPanel", typeof(RectTransform), typeof(MobileControlsUI));
        controlsRoot.transform.SetParent(canvasGo.transform, false);
        Stretch(controlsRoot.GetComponent<RectTransform>());
        var controls = controlsRoot.GetComponent<MobileControlsUI>();
        controls.busController = bus;
        controls.accelerateButton = CreateUiButton(controlsRoot.transform, "Btn_Accelerate", "ACCEL").GetComponent<Button>();
        controls.brakeButton = CreateUiButton(controlsRoot.transform, "Btn_Brake", "BRAKE").GetComponent<Button>();
        controls.steerLeftButton = CreateUiButton(controlsRoot.transform, "Btn_SteerLeft", "<").GetComponent<Button>();
        controls.steerRightButton = CreateUiButton(controlsRoot.transform, "Btn_SteerRight", ">").GetComponent<Button>();
        controls.shiftUpButton = CreateUiButton(controlsRoot.transform, "Btn_ShiftUp", "^").GetComponent<Button>();
        controls.shiftDownButton = CreateUiButton(controlsRoot.transform, "Btn_ShiftDown", "v").GetComponent<Button>();
        controls.retarderButton = CreateUiButton(controlsRoot.transform, "Btn_Retarder", "R").GetComponent<Button>();
        controls.hornButton = CreateUiButton(controlsRoot.transform, "Btn_Horn", "HORN").GetComponent<Button>();

        var eventSystem = new GameObject("EventSystem", typeof(EventSystem));
        AddBestInputModule(eventSystem);
        eventSystem.transform.SetParent(canvasGo.transform, false);
    }

    static GameObject CreateUiButton(Transform parent, string name, string label)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
        go.transform.SetParent(parent, false);
        go.GetComponent<Image>().color = new Color(0.04f, 0.045f, 0.05f, 0.82f);
        var text = new GameObject("Label", typeof(RectTransform), typeof(TextMeshProUGUI)).GetComponent<TextMeshProUGUI>();
        text.transform.SetParent(go.transform, false);
        Stretch(text.rectTransform);
        text.text = label;
        text.fontSize = 22f;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.white;
        text.raycastTarget = false;
        return go;
    }

    static void AddBestInputModule(GameObject eventSystemObject)
    {
        var inputSystemModuleType = Type.GetType("UnityEngine.InputSystem.UI.InputSystemUIInputModule, Unity.InputSystem");
        if (inputSystemModuleType != null)
        {
            eventSystemObject.AddComponent(inputSystemModuleType);
            return;
        }

        eventSystemObject.AddComponent<StandaloneInputModule>();
    }

    static void CreateDiagnosticShowcase()
    {
        var root = new GameObject("Imported Asset Diagnostics");
        CreateWorldLabel(root.transform, "Imported Asset Diagnostics: buses, stops, fuel, roads", new Vector3(0f, 5.5f, 18f), 2.2f);

        float x = -24f;
        foreach (var path in FindAssetPaths("Assets/_Game/Generated/Models/Buses", "t:Prefab").Take(5))
        {
            PlacePrefab(path, root.transform, new Vector3(x, 0f, 18f), Quaternion.Euler(0f, 180f, 0f), 1f);
            CreateWorldLabel(root.transform, Path.GetFileNameWithoutExtension(path).Replace("_Visual", ""), new Vector3(x, 3.5f, 18f), 0.85f);
            x += 12f;
        }

        x = -18f;
        foreach (var path in FindAssetPaths("Assets/_Game/Resources/BusStopsGenerated", "t:Prefab").Take(4))
        {
            PlacePrefab(path, root.transform, new Vector3(x, 0f, 34f), Quaternion.identity, 1f);
            CreateWorldLabel(root.transform, Path.GetFileNameWithoutExtension(path), new Vector3(x, 2.8f, 34f), 0.7f);
            x += 10f;
        }

        x = -10f;
        foreach (var path in FindAssetPaths("Assets/_Game/Resources/GasStationsGenerated", "t:Prefab").Take(2))
        {
            PlacePrefab(path, root.transform, new Vector3(x, 0f, 50f), Quaternion.identity, 1f);
            CreateWorldLabel(root.transform, Path.GetFileNameWithoutExtension(path), new Vector3(x, 4f, 50f), 0.85f);
            x += 20f;
        }

        foreach (var path in FindAssetPaths("Assets/_Game/Resources/RoadsGenerated", "t:Prefab").Take(3))
        {
            var road = PlacePrefab(path, root.transform, new Vector3(0f, 0.03f, -18f), Quaternion.identity, 1f);
            if (road != null)
            {
                road.name = "RoadPrefab_Diagnostic_" + Path.GetFileNameWithoutExtension(path);
                CreateWorldLabel(root.transform, "Road prefab: " + Path.GetFileNameWithoutExtension(path), new Vector3(0f, 1.2f, -18f), 0.9f);
            }
        }
    }

    static GameObject PlacePrefab(string path, Transform parent, Vector3 position, Quaternion rotation, float scale)
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (prefab == null) return null;
        var instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        if (instance == null) return null;
        instance.transform.SetParent(parent, true);
        instance.transform.SetPositionAndRotation(position, rotation);
        instance.transform.localScale *= scale;

        foreach (var body in instance.GetComponentsInChildren<Rigidbody>(true))
            body.isKinematic = true;
        foreach (var bus in instance.GetComponentsInChildren<BusController>(true))
            bus.enabled = false;

        return instance;
    }

    static void CreateWorldLabel(Transform parent, string text, Vector3 position, float fontSize)
    {
        var go = new GameObject("Label_" + text.Take(24).Aggregate("", (a, c) => a + (char.IsLetterOrDigit(c) ? c : '_')), typeof(TextMeshPro));
        go.transform.SetParent(parent, false);
        go.transform.position = position;
        go.transform.rotation = Quaternion.Euler(65f, 0f, 0f);
        var label = go.GetComponent<TextMeshPro>();
        label.text = text;
        label.fontSize = fontSize;
        label.alignment = TextAlignmentOptions.Center;
        label.color = Color.white;
    }

    static CityDefinition LoadPreferredCity()
    {
        var cities = LoadAssets<CityDefinition>("Assets/_Game/ScriptableObjects/Cities");
        return cities.FirstOrDefault(c => c != null && string.Equals(c.cityCode, "NBO", StringComparison.OrdinalIgnoreCase))
            ?? cities.FirstOrDefault(c => c != null);
    }

    static CountryDefinition LoadCountryForCity(CityDefinition city)
    {
        var countries = LoadAssets<CountryDefinition>("Assets/_Game/ScriptableObjects");
        if (city == null) return countries.FirstOrDefault();
        return countries.FirstOrDefault(country => country != null && country.cities != null && country.cities.Contains(city))
            ?? countries.FirstOrDefault(country => country != null && string.Equals(country.countryName, city.country, StringComparison.OrdinalIgnoreCase))
            ?? countries.FirstOrDefault();
    }

    static MapOrigin EnsureMapOrigin(CityDefinition city)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(OriginAssetPath));
        var origin = AssetDatabase.LoadAssetAtPath<MapOrigin>(OriginAssetPath);
        if (origin == null)
        {
            origin = ScriptableObject.CreateInstance<MapOrigin>();
            AssetDatabase.CreateAsset(origin, OriginAssetPath);
        }

        if (city != null)
            origin.SetFromCity(city);
        else
            origin.SetOrigin(-1.2864d, 36.8172d, "Nairobi fallback");

        EditorUtility.SetDirty(origin);
        return origin;
    }

    static BusRoute FirstRoute(CityDefinition city)
    {
        if (city != null && city.availableRoutes != null)
        {
            foreach (var route in city.availableRoutes)
                if (route != null)
                    return route;
        }

        return LoadAssets<BusRoute>("Assets/_Game/Routes").FirstOrDefault(route => route != null);
    }

    static GameObject FindFirstImportedDrivablePrefab()
    {
        var spec = LoadAssets<BusSpec>("Assets/_Game/Resources/BusSpecs")
            .Where(s => s != null && s.drivablePrefab != null)
            .OrderBy(s => s.requiredRank)
            .ThenBy(s => s.displayName)
            .FirstOrDefault();
        if (spec != null)
            return spec.drivablePrefab;

        string path = FindAssetPaths("Assets/_Game/Generated/Prefabs/Buses", "t:Prefab").FirstOrDefault();
        return string.IsNullOrEmpty(path) ? null : AssetDatabase.LoadAssetAtPath<GameObject>(path);
    }

    static T FindAsset<T>(string nameContains) where T : UnityEngine.Object
    {
        return LoadAssets<T>("Assets/_Game")
            .FirstOrDefault(asset => asset != null && asset.name.IndexOf(nameContains, StringComparison.OrdinalIgnoreCase) >= 0);
    }

    static T[] LoadAssets<T>(string root) where T : UnityEngine.Object
    {
        if (!Directory.Exists(root))
            return Array.Empty<T>();

        return AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] { root })
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<T>)
            .Where(asset => asset != null)
            .OrderBy(asset => asset.name)
            .ToArray();
    }

    static IEnumerable<string> FindAssetPaths(string root, string filter)
    {
        if (!Directory.Exists(root))
            return Enumerable.Empty<string>();

        return AssetDatabase.FindAssets(filter, new[] { root })
            .Select(AssetDatabase.GUIDToAssetPath)
            .OrderBy(path => path, StringComparer.OrdinalIgnoreCase);
    }

    static Material CreateRuntimeMaterial(string name, Color color)
    {
        var shader = Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard");
        var material = new Material(shader) { name = name, color = color };
        return material;
    }

    static void Stretch(RectTransform rect)
    {
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    static void SetSerializedBool(UnityEngine.Object target, string propertyName, bool value)
    {
        var serialized = new SerializedObject(target);
        var property = serialized.FindProperty(propertyName);
        if (property != null)
        {
            property.boolValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }
    }

    static void EnsureSceneInBuildSettings(string path)
    {
        var scenes = EditorBuildSettings.scenes.ToList();
        if (scenes.Any(scene => scene.path == path))
            return;

        scenes.Add(new EditorBuildSettingsScene(path, true));
        EditorBuildSettings.scenes = scenes.ToArray();
    }
}
#endif
