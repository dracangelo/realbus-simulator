using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneBootstrap : MonoBehaviour
{
    [SerializeField] bool redirectIfNoManagers = true;
    [SerializeField] SceneCatalog sceneCatalog;
    [Header("Direct Play Runtime")]
    [SerializeField] bool autoPopulateCityFromOsm = true;
    [SerializeField] bool spawnSupplementalWorldProps = false;
    [SerializeField] bool spawnDebugPoiMarkers = false;
    [SerializeField] bool spawnOnlyFirstStopPropsAtMissionStart = false;
    [SerializeField] bool attachMainCameraToBusForDirectPlay = true;
    [SerializeField] bool disableCinemachineVirtualCamerasForDirectPlay = true;
    [SerializeField] bool disableCinemachineBrainForDirectPlay = true;
    [SerializeField] bool forceClearVisibilityForDirectPlay = true;
    [SerializeField] bool suppressBuildingsForCurrentPhase = false;
    [SerializeField] Vector3 directPlayCameraLocalPosition = new Vector3(0f, 3.2f, -7.5f);
    [SerializeField] Vector3 directPlayCameraLocalEuler = new Vector3(14f, 0f, 0f);
    [Header("Gameplay Fallbacks")]
    [SerializeField] BusRoute fallbackRoute;
    [SerializeField] MissionData fallbackMissionData;
    [SerializeField] MissionData[] knownMissionData;

    void Awake()
    {
        if (redirectIfNoManagers && SceneLoader.Instance == null)
        {
            Debug.LogWarning("SceneBootstrap: No SceneLoader — creating one for direct scene testing.");
            var loader = new GameObject("SceneLoader");
            var sl = loader.AddComponent<SceneLoader>();
            if (sceneCatalog != null)
                sl.sceneCatalog = sceneCatalog;
        }

        EnsureCoreManagersForDirectPlay();
        ConfigureGameplaySceneForDirectPlay();
    }

    void EnsureCoreManagersForDirectPlay()
    {
        // Directly playing Country/City/Route scenes often skips MainMenu bootstrap.
        // Create essential managers so UI scenes can run standalone.
        if (GameState.Instance == null)
        {
            var go = new GameObject("GameState");
            go.AddComponent<GameState>();
        }

        if (CityManager.Instance == null)
        {
            var go = new GameObject("CityManager");
            go.AddComponent<CityManager>();
        }

        EnsureDriverShiftSystem();

#if UNITY_EDITOR
        SeedCityManagerFromAssetsInEditor();
#endif

        SyncSelectionStateFromManagers();
    }

    void EnsureDriverShiftSystem()
    {
        if (GameState.Instance == null)
            return;

        if (GameState.Instance.GetComponent<DriverShiftSystem>() == null)
            GameState.Instance.gameObject.AddComponent<DriverShiftSystem>();
    }

    void SyncSelectionStateFromManagers()
    {
        var gameState = GameState.Instance;
        var cityManager = CityManager.Instance;

        if (gameState == null || cityManager == null) return;

        if (cityManager.activeCountry == null && cityManager.allCountries != null && cityManager.allCountries.Length > 0)
            cityManager.activeCountry = cityManager.allCountries[0];

        if (cityManager.activeCity == null)
        {
            if (gameState.selectedCity != null)
            {
                cityManager.activeCity = gameState.selectedCity;
            }
            else if (cityManager.activeCountry != null && cityManager.activeCountry.cities != null && cityManager.activeCountry.cities.Length > 0)
            {
                cityManager.activeCity = cityManager.activeCountry.cities[0];
            }
            else if (cityManager.allCities != null && cityManager.allCities.Length > 0)
            {
                cityManager.activeCity = cityManager.allCities[0];
            }
        }

        if (gameState.selectedCountry == null)
            gameState.selectedCountry = cityManager.activeCountry;

        if (gameState.selectedCity == null)
            gameState.selectedCity = cityManager.activeCity;
    }

    void ConfigureGameplaySceneForDirectPlay()
    {
        var missionManager = FindObjectOfType<MissionManager>();
        if (missionManager == null) return;

        EnsureRuntimeLogger();

        var selectedRoute = GameState.Instance?.selectedRoute;
        var routeToUse = selectedRoute != null ? selectedRoute : fallbackRoute;
        if (routeToUse == null && missionManager.currentRoute != null)
            routeToUse = missionManager.currentRoute;

        routeToUse = EnsureRouteHasStops(routeToUse);

        if (routeToUse != null)
        {
            missionManager.currentRoute = routeToUse;

            if (GameState.Instance != null)
                GameState.Instance.selectedRoute = routeToUse;
        }

        DailyChallengeManager daily = DailyChallengeManager.Instance;
        var missionToUse = daily != null && daily.AppliesTo(routeToUse)
            ? daily.ChallengeMission
            : ResolveMissionDataForRoute(routeToUse) ?? fallbackMissionData ?? missionManager.missionData;
        if (missionToUse != null)
            missionManager.missionData = missionToUse;

        if (missionManager.busController == null)
            missionManager.busController = FindObjectOfType<BusController>();

        EnsureVehicleSupportSystems(missionManager.busController);

        EnsureGpsManager();
        EnsureGpsTracking(missionManager.busController);
        EnsurePhotoMode(missionManager.busController);
        DisableCinemachineVirtualCamerasForDirectPlay();
        EnsureMainCameraForDirectPlay(missionManager.busController);
        ConfigureRoadGraphBackedSystems(routeToUse);
        EnsurePedestrianSpawner();
        EnsureStopPropSpawner();
        EnsurePassengerSystems();
        ApplySelectedBusSpec(missionManager.busController);
        EnsureTarmacApplier();
        EnsureWeatherSystems();
        EnsureRuntimeRoadSystems();
        EnsureRoadSurfaceRuntime(missionManager.busController);
        ConfigureOptionalTileStreaming(missionManager.busController);
        EnsureBuildingSystems();
        EnsureOsmCityPopulation();
        EnsurePoiVisualizer();
        EnsureTrafficViolationSystem();
        EnsureDynamicEventSystem();
        EnsureAiTrafficSystems();

        if (spawnSupplementalWorldProps)
        {
            EnsureEnvironmentSpawner();
        }

        if (autoPopulateCityFromOsm)
            EnsureGasStationSpawner();

        EnsureClearVisibility();
        ApplyBusScaleFromMap();
    }

    void EnsureVehicleSupportSystems(BusController busController)
    {
        if (busController == null)
            return;

        if (busController.GetComponent<MaintenanceSystem>() == null)
            busController.gameObject.AddComponent<MaintenanceSystem>();
        if (busController.GetComponent<FuelSystem>() == null && busController.GetComponent<BatterySystem>() == null)
            busController.gameObject.AddComponent<FuelSystem>();
    }

    void ApplySelectedBusSpec(BusController busController)
    {
        if (busController == null)
            return;

        var fleet = BusFleetManager.EnsureExists();
        var spec = fleet != null ? fleet.GetSelectedBusSpec() : null;
        if (spec == null)
            return;

        busController.ApplyBusSpec(spec);
        GameState.Instance?.vehicleState?.ApplyBusSpec(spec, preserveEnergyPercent: true);

        var passengerManager = FindObjectOfType<PassengerManager>();
        if (passengerManager != null)
            passengerManager.ApplyBusSpec(spec);

        var fuelSystem = busController.GetComponent<FuelSystem>();
        var batterySystem = busController.GetComponent<BatterySystem>();

        if (spec.IsElectric)
        {
            if (fuelSystem != null)
                Destroy(fuelSystem);

            if (batterySystem == null)
                batterySystem = busController.gameObject.AddComponent<BatterySystem>();

            batterySystem.ApplyBusSpec(spec);
        }
        else
        {
            if (batterySystem != null)
                Destroy(batterySystem);

            if (fuelSystem == null)
                fuelSystem = busController.gameObject.AddComponent<FuelSystem>();

            fuelSystem.ApplyBusSpec(spec);
        }

        var liveryEditor = busController.GetComponent<LiveryEditor>();
        if (liveryEditor == null)
            liveryEditor = busController.gameObject.AddComponent<LiveryEditor>();

        Transform visualRoot = busController.modelRoot != null ? busController.modelRoot : busController.transform;
        liveryEditor.Initialize(spec.busId, visualRoot.GetComponentsInChildren<Renderer>(true));
    }

    void EnsureGpsManager()
    {
        if (FindObjectOfType<GPSManager>() != null)
            return;

        var mapLoader = FindObjectOfType<MapTileLoader>();
        if (mapLoader == null)
        {
            Debug.LogWarning("SceneBootstrap: No MapTileLoader found, so GPSManager cannot resolve map coordinates yet.");
            return;
        }

        var go = new GameObject("GPSManager");
        go.AddComponent<GPSManager>();
    }

    void EnsureGpsTracking(BusController busController)
    {
        if (busController == null)
            return;

        var tracker = FindObjectOfType<GpsTracker>();
        if (tracker == null)
        {
            var go = new GameObject("GpsTracker");
            tracker = go.AddComponent<GpsTracker>();
        }

        tracker.busTransform = busController.transform;
        if (tracker.converter == null)
            tracker.converter = CoordinateConverter.Instance ?? FindFirstObjectByType<CoordinateConverter>();
    }

    void EnsurePhotoMode(BusController busController)
    {
        var photoMode = FindObjectOfType<PhotoModeController>();
        if (photoMode == null)
        {
            var go = new GameObject("PhotoModeController");
            photoMode = go.AddComponent<PhotoModeController>();
        }

        if (photoMode.targetCamera == null)
            photoMode.targetCamera = Camera.main != null ? Camera.main : FindObjectOfType<Camera>();
        if (photoMode.busTransform == null && busController != null)
            photoMode.busTransform = busController.transform;
    }

    void EnsureRuntimeRoadSystems()
    {
        var osmLoader = FindObjectOfType<OSMLoader>();
        if (osmLoader == null)
        {
            var loaderGo = new GameObject("OSMLoader");
            osmLoader = loaderGo.AddComponent<OSMLoader>();
        }

        var roadBuilder = FindObjectOfType<OSMRoadMeshBuilder>();
        if (roadBuilder == null)
        {
            var roadsGo = new GameObject("OSMRoadMeshBuilder");
            roadBuilder = roadsGo.AddComponent<OSMRoadMeshBuilder>();
        }

        // Always render + collide roads in direct-play so the player bus can drive on geometry.
        roadBuilder.renderRoadSurface = true;
        roadBuilder.drawCenterLines = true;
        roadBuilder.maxColliderSegmentLength = Mathf.Clamp(roadBuilder.maxColliderSegmentLength, 10f, 60f);
        roadBuilder.SetRoadSurfaceVisible(true);

        var mapLoader = FindObjectOfType<MapTileLoader>();
        if (mapLoader != null)
        {
            roadBuilder.roadYOffset = Mathf.Max(mapLoader.tileSurfaceY + 0.03f, 0.02f);
            mapLoader.zoomLevel = Mathf.Max(mapLoader.zoomLevel, 17);
            mapLoader.forceSharpTileFiltering = true;
            mapLoader.tileAnisoLevel = 16;
            mapLoader.tileMipMapBias = -0.75f;
            mapLoader.preferLabelFreeSatellite = true;
        }
    }

    void EnsureRoadSurfaceRuntime(BusController busController)
    {
        if (busController == null)
            return;

        var detector = busController.GetComponent<RoadSurfaceDetector>();
        if (detector == null)
            detector = busController.gameObject.AddComponent<RoadSurfaceDetector>();
        detector.busController = busController;

        var friction = busController.GetComponent<RoadSurfaceFrictionController>();
        if (friction == null)
            friction = busController.gameObject.AddComponent<RoadSurfaceFrictionController>();
        friction.busController = busController;
        friction.surfaceDetector = detector;
    }

    void ConfigureOptionalTileStreaming(BusController busController)
    {
        var streaming = FindObjectOfType<TileStreamManager>();
        if (streaming == null)
            return;

        streaming.busController = busController;
        streaming.busTransform = busController != null ? busController.transform : null;
        if (streaming.converter == null)
            streaming.converter = CoordinateConverter.Instance ?? FindFirstObjectByType<CoordinateConverter>();
        if (streaming.cache == null)
            streaming.cache = OfflineCacheManager.Instance ?? FindFirstObjectByType<OfflineCacheManager>();
        if (streaming.gpsTracker == null)
            streaming.gpsTracker = GpsTracker.Instance ?? FindFirstObjectByType<GpsTracker>();
        if (streaming.mapTileLoader == null)
            streaming.mapTileLoader = FindObjectOfType<MapTileLoader>();
    }

    void EnsureTrafficViolationSystem()
    {
        if (FindObjectOfType<ExtendedTrafficViolationSystem>() != null)
            return;

        var bus = FindObjectOfType<BusController>();
        if (bus == null)
            return;

        var violationSystem = bus.gameObject.AddComponent<ExtendedTrafficViolationSystem>();
        violationSystem.roadGraph = FindUsableRoadGraph();
    }

    void EnsureDynamicEventSystem()
    {
        if (FindObjectOfType<DynamicEventSystem>() != null)
            return;

        var go = new GameObject("DynamicEventSystem");
        var system = go.AddComponent<DynamicEventSystem>();
        system.busController = FindObjectOfType<BusController>();
    }

    void EnsurePassengerSystems()
    {
        if (FindObjectOfType<PassengerSpawner>() == null)
        {
            var spawnerGo = new GameObject("PassengerSpawner");
            spawnerGo.AddComponent<PassengerSpawner>();
        }

        var passengerManager = FindObjectOfType<PassengerManager>();
        if (passengerManager == null)
        {
            var managerGo = new GameObject("PassengerManager");
            passengerManager = managerGo.AddComponent<PassengerManager>();
        }

        if (passengerManager.passengerSpawner == null)
            passengerManager.passengerSpawner = FindObjectOfType<PassengerSpawner>();
    }

    void EnsureAiTrafficSystems()
    {
        var graph = FindUsableRoadGraph();
        if (graph == null)
            return;

        var pool = FindObjectOfType<VehiclePool>();
        if (pool == null)
        {
            var go = new GameObject("VehiclePool");
            pool = go.AddComponent<VehiclePool>();
        }

        pool.roadGraph = graph;
        pool.allowAutoResolveRoadGraph = true;
        ConfigureVehiclePoolPrefabs(pool);

        var busSpawner = FindObjectOfType<AIBusScheduleSpawner>();
        if (busSpawner == null)
        {
            var go = new GameObject("AIBusScheduleSpawner");
            busSpawner = go.AddComponent<AIBusScheduleSpawner>();
        }

        if (busSpawner.aiBusPrefab == null)
            busSpawner.aiBusPrefab = LoadFirstResourcePrefab(
                "BussimAssets/bus-models/White Modern Coach Bus",
                "BussimAssets/bus-models/Laksana",
                "BussimAssets/bus-models/LUXURY BUS",
                "BussimAssets/bus-models/Indonesian Bus AdiPutro JetBus");

        if (busSpawner.routes == null || busSpawner.routes.Length == 0)
        {
            busSpawner.routes = new[]
            {
                new AIBusScheduleSpawner.ScheduledAIBusRoute
                {
                    routeName = "Ambient Service",
                    graph = graph,
                    useRandomGeneratedNodes = true,
                    randomNodeCount = 10,
                    headwayMinutes = 14f,
                    maxConcurrentBuses = 2
                }
            };
        }
        else
        {
            for (int i = 0; i < busSpawner.routes.Length; i++)
            {
                if (busSpawner.routes[i] != null && busSpawner.routes[i].graph == null)
                    busSpawner.routes[i].graph = graph;
            }
        }
    }

    void EnsureWeatherSystems()
    {
        var skyController = FindObjectOfType<SkyController>();
        if (skyController == null)
        {
            var skyGo = new GameObject("SkyController");
            skyController = skyGo.AddComponent<SkyController>();
            skyController.sunLight = RenderSettings.sun;
        }

        var rainController = FindObjectOfType<RainController>();
        if (rainController == null)
            rainController = new GameObject("RainController").AddComponent<RainController>();

        var fogController = FindObjectOfType<FogController>();
        if (fogController == null)
            fogController = new GameObject("FogController").AddComponent<FogController>();

        var weatherSystem = FindObjectOfType<WeatherSystem>();
        if (weatherSystem == null)
        {
            var go = new GameObject("WeatherSystem");
            weatherSystem = go.AddComponent<WeatherSystem>();
        }
        weatherSystem.skyController = skyController;
        weatherSystem.rainController = rainController;
        weatherSystem.fogController = fogController;

        if (FindObjectOfType<TimeOfDaySystem>() == null)
        {
            var go = new GameObject("TimeOfDaySystem");
            var timeSystem = go.AddComponent<TimeOfDaySystem>();
            timeSystem.skyController = skyController;
            timeSystem.directionalSun = RenderSettings.sun;
        }
    }

#if UNITY_EDITOR
    void SeedCityManagerFromAssetsInEditor()
    {
        var cityManager = CityManager.Instance;
        if (cityManager == null) return;
        if (cityManager.allCountries != null && cityManager.allCountries.Length > 0) return;

        string[] countryGuids = AssetDatabase.FindAssets("t:CountryDefinition");
        if (countryGuids == null || countryGuids.Length == 0) return;

        var countries = new List<CountryDefinition>();
        var allCities = new List<CityDefinition>();

        foreach (string guid in countryGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var country = AssetDatabase.LoadAssetAtPath<CountryDefinition>(path);
            if (!country) continue;
            countries.Add(country);

            if (country.cities == null) continue;
            foreach (var city in country.cities)
            {
                if (city != null)
                    allCities.Add(city);
            }
        }

        if (countries.Count == 0) return;

        cityManager.allCountries = countries.ToArray();
        cityManager.allCities = allCities.ToArray();
        cityManager.activeCountry = cityManager.allCountries[0];
        cityManager.activeCity = (cityManager.activeCountry.cities != null && cityManager.activeCountry.cities.Length > 0)
            ? cityManager.activeCountry.cities[0]
            : null;

        Debug.Log($"SceneBootstrap: Seeded CityManager with {cityManager.allCountries.Length} countries for direct scene play.");
    }
#endif

    MissionData ResolveMissionDataForRoute(BusRoute route)
    {
        if (route == null) return null;

        if (knownMissionData != null)
        {
            for (int i = 0; i < knownMissionData.Length; i++)
            {
                var mission = knownMissionData[i];
                if (mission != null && mission.route == route)
                    return mission;
            }
        }

        if (fallbackMissionData != null && fallbackMissionData.route == route)
            return fallbackMissionData;

#if UNITY_EDITOR
        string[] missionGuids = AssetDatabase.FindAssets("t:MissionData");
        for (int i = 0; i < missionGuids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(missionGuids[i]);
            var mission = AssetDatabase.LoadAssetAtPath<MissionData>(path);
            if (mission != null && mission.route == route)
                return mission;
        }
#endif

        return null;
    }

    BusRoute EnsureRouteHasStops(BusRoute route)
    {
        if (route != null && route.stops != null && route.stops.Length > 0)
            return route;

        var city = GameState.Instance?.selectedCity ?? CityManager.Instance?.activeCity;
        if (city == null)
            return route;

        string stopsPath = Path.Combine(Application.streamingAssetsPath, "Cities", city.cityCode, "stops.json");
        if (!File.Exists(stopsPath))
            return route;

        var stopsJson = File.ReadAllText(stopsPath);
        var parsedStops = OSMParser.ParseStopsJson(stopsJson, city.spawnLat, city.spawnLon, 12);
        if (parsedStops == null || parsedStops.Length < 2)
            return route;

        var runtimeRoute = route != null ? Instantiate(route) : ScriptableObject.CreateInstance<BusRoute>();
        runtimeRoute.routeName = route != null && !string.IsNullOrWhiteSpace(route.routeName)
            ? route.routeName
            : $"{city.cityName} OSM Service";
        runtimeRoute.routeNumber = route != null && !string.IsNullOrWhiteSpace(route.routeNumber)
            ? route.routeNumber
            : city.cityCode;
        runtimeRoute.baseFare = route != null ? route.baseFare : 50f;
        runtimeRoute.difficulty = route != null ? route.difficulty : 3;
        runtimeRoute.stops = parsedStops;

        Debug.Log($"SceneBootstrap: Built runtime fallback route '{runtimeRoute.routeName}' from {parsedStops.Length} downloaded OSM stops.");
        return runtimeRoute;
    }

    void EnsureRuntimeLogger()
    {
        if (FindObjectOfType<RuntimeSpawnLogger>() != null)
            return;

        var loggerGo = new GameObject("RuntimeSpawnLogger");
        loggerGo.AddComponent<RuntimeSpawnLogger>();
    }

    void EnsurePedestrianSpawner()
    {
        var existing = FindObjectOfType<PedestrianSpawner>();
        if (existing != null)
        {
            if (existing.pedestrianPrefab == null)
                existing.pedestrianPrefab = LoadFirstResourcePrefab(
                    "BussimAssets/passenger/Realistic man",
                    "BussimAssets/passenger/uploads_files_2460380_Marina_1276",
                    "BussimAssets/passenger/uploads_files_6231172_woman+3d+fbx",
                    "BussimAssets/passenger/Man_in_black_dress_0227");
            if (existing.roadGraph == null)
                existing.useRandomGeneratedCrossings = false;
            return;
        }

        var go = new GameObject("PedestrianSpawner");
        var spawner = go.AddComponent<PedestrianSpawner>();
        spawner.roadGraph = FindUsableRoadGraph();
        spawner.useRandomGeneratedCrossings = spawner.roadGraph != null;
        spawner.pedestrianPrefab = LoadFirstResourcePrefab(
            "BussimAssets/passenger/Realistic man",
            "BussimAssets/passenger/uploads_files_2460380_Marina_1276",
            "BussimAssets/passenger/uploads_files_6231172_woman+3d+fbx",
            "BussimAssets/passenger/Man_in_black_dress_0227");
    }

    void EnsureEnvironmentSpawner()
    {
        if (FindObjectOfType<RuntimeEnvironmentSpawner>() != null)
            return;

        var go = new GameObject("RuntimeEnvironmentSpawner");
        go.AddComponent<RuntimeEnvironmentSpawner>();
    }

    void EnsureStopPropSpawner()
    {
        var existing = FindObjectOfType<StopPropSpawner>();
        if (existing != null)
        {
            existing.spawnOnlyFirstStopAtMissionStart = spawnOnlyFirstStopPropsAtMissionStart;
            return;
        }

        var go = new GameObject("StopPropSpawner");
        var spawner = go.AddComponent<StopPropSpawner>();
        spawner.spawnOnlyFirstStopAtMissionStart = spawnOnlyFirstStopPropsAtMissionStart;
    }

    void EnsureGasStationSpawner()
    {
        if (FindObjectOfType<RuntimeGasStationSpawner>() != null)
            return;

        var go = new GameObject("RuntimeGasStationSpawner");
        go.AddComponent<RuntimeGasStationSpawner>();
    }

    void EnsureTarmacApplier()
    {
        if (FindObjectOfType<RuntimeTarmacApplier>() != null)
            return;

        var go = new GameObject("RuntimeTarmacApplier");
        go.AddComponent<RuntimeTarmacApplier>();
    }

    void EnsureBuildingSystems()
    {
        if (suppressBuildingsForCurrentPhase && !autoPopulateCityFromOsm)
        {
            var buildingLoader = FindObjectOfType<OSMBuildingLoader>();
            if (buildingLoader != null)
                buildingLoader.enabled = false;

            var buildingBuilder = FindObjectOfType<OSMBuildingMeshBuilder>();
            if (buildingBuilder != null)
                buildingBuilder.enabled = false;

            var existingBuildings = GameObject.Find("OSM_Buildings");
            if (existingBuildings != null)
                existingBuildings.SetActive(false);

            return;
        }

        if (FindObjectOfType<OSMBuildingLoader>() == null)
        {
            var loaderGo = new GameObject("OSMBuildingLoader");
            loaderGo.AddComponent<OSMBuildingLoader>();
        }
        else
        {
            FindObjectOfType<OSMBuildingLoader>().enabled = true;
        }

        var builder = FindObjectOfType<OSMBuildingMeshBuilder>();
        if (builder == null)
        {
            var builderGo = new GameObject("OSMBuildingMeshBuilder");
            builder = builderGo.AddComponent<OSMBuildingMeshBuilder>();
        }
        else
        {
            builder.enabled = true;
        }

        var mapLoader = FindObjectOfType<MapTileLoader>();
        if (mapLoader != null)
            builder.buildingBaseYOffset = Mathf.Max(mapLoader.tileSurfaceY + 0.03f, 0.2f);

        builder.maxBuildingsPerFrame = Mathf.Max(builder.maxBuildingsPerFrame, 500);
        builder.enableDistanceCulling = false;
        builder.drawFootprints = false;

        var activeBuildingsRoot = GameObject.Find("OSM_Buildings");
        if (activeBuildingsRoot != null)
            activeBuildingsRoot.SetActive(true);
    }

    void EnsureOsmCityPopulation()
    {
        if (!autoPopulateCityFromOsm)
            return;

        var populator = FindObjectOfType<OsmCityPopulationSystem>();
        if (populator == null)
        {
            var go = new GameObject("OsmCityPopulationSystem");
            populator = go.AddComponent<OsmCityPopulationSystem>();
        }

        populator.populateOnStart = true;
        populator.logSpawns = true;
    }

    void EnsurePoiVisualizer()
    {
        if (!spawnDebugPoiMarkers)
            return;

        if (FindObjectOfType<RuntimeOsmPoiVisualizer>() != null)
            return;

        var go = new GameObject("RuntimeOsmPoiVisualizer");
        go.AddComponent<RuntimeOsmPoiVisualizer>();
    }

    void ConfigureVehiclePoolPrefabs(VehiclePool pool)
    {
        if (pool == null)
            return;

        if (pool.prefabs != null && pool.prefabs.Length > 0)
            return;

        var entries = new List<VehiclePool.VehiclePrefabEntry>();
        AddVehiclePrefab(entries, AIVehicleController.VehicleType.Car, 0.16f, "BussimAssets/aitraffic/Van");
        AddVehiclePrefab(entries, AIVehicleController.VehicleType.Car, 0.12f, "BussimAssets/aitraffic/uploads_files_1862674_Beetle+-+FBX");
        AddVehiclePrefab(entries, AIVehicleController.VehicleType.Car, 0.12f, "BussimAssets/aitraffic/uploads_files_5836120_Fortuner");
        AddVehiclePrefab(entries, AIVehicleController.VehicleType.Car, 0.12f, "BussimAssets/aitraffic/uploads_files_5844339_Palisade2024");
        AddVehiclePrefab(entries, AIVehicleController.VehicleType.Truck, 0.08f, "BussimAssets/aitraffic/uploads_files_5533688_pickup+test2");
        AddVehiclePrefab(entries, AIVehicleController.VehicleType.Truck, 0.08f, "BussimAssets/aitraffic/uploads_files_4076767_SCANIA");
        AddVehiclePrefab(entries, AIVehicleController.VehicleType.Motorcycle, 0.07f, "BussimAssets/aitraffic/uploads_files_901612_bike");
        AddVehiclePrefab(entries, AIVehicleController.VehicleType.Emergency, 0.05f, "BussimAssets/aitraffic/uploads_files_4560086_FireEngine");
        AddVehiclePrefab(entries, AIVehicleController.VehicleType.Emergency, 0.05f, "BussimAssets/aitraffic/uploads_files_6375847_ambulance");
        AddVehiclePrefab(entries, AIVehicleController.VehicleType.Bus, 0.15f, "BussimAssets/bus-models/White Modern Coach Bus");

        if (entries.Count == 0)
            return;

        pool.prefabs = entries.ToArray();
        pool.poolSizePerScene = Mathf.Max(pool.poolSizePerScene, 64);
        pool.debugForceActiveCount = Mathf.Max(pool.debugForceActiveCount, 10);
        pool.logSpawnEvents = false;
    }

    void AddVehiclePrefab(List<VehiclePool.VehiclePrefabEntry> entries, AIVehicleController.VehicleType type, float weight, string resourcePath)
    {
        var prefab = LoadFirstResourcePrefab(resourcePath);
        if (prefab == null)
            return;

        entries.Add(new VehiclePool.VehiclePrefabEntry
        {
            type = type,
            prefab = prefab,
            weight = weight
        });
    }

    GameObject LoadFirstResourcePrefab(params string[] resourcePaths)
    {
        if (resourcePaths == null)
            return null;

        for (int i = 0; i < resourcePaths.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(resourcePaths[i]))
                continue;

            var prefab = Resources.Load<GameObject>(resourcePaths[i]);
            if (prefab != null)
                return prefab;
        }

        return null;
    }

    void EnsureClearVisibility()
    {
        if (!forceClearVisibilityForDirectPlay)
            return;

        RenderSettings.fog = false;
        RenderSettings.fogStartDistance = 5000f;
        RenderSettings.fogEndDistance = 10000f;

        var fogController = FindObjectOfType<FogController>();
        if (fogController != null)
            fogController.enabled = false;

        var weatherSystem = FindObjectOfType<WeatherSystem>();
        if (weatherSystem != null)
        {
            weatherSystem.forceWeather = true;
            weatherSystem.forcedWeatherState = WeatherState.Clear;
            weatherSystem.ApplyWeather(WeatherState.Clear, 0f, weatherSystem.temperature);
            weatherSystem.enabled = false;
        }

        var skyController = FindObjectOfType<SkyController>();
        if (skyController != null)
            skyController.SetSky(WeatherState.Clear, 0f, 24f);
    }

    void ApplyBusScaleFromMap()
    {
        var bus = FindObjectOfType<BusController>();
        var map = FindObjectOfType<MapTileLoader>();
        if (bus == null || map == null) return;

        float scale = map.tileWorldSize > 0f ? map.tileWorldSize / 200f : 1f;
        bus.transform.localScale = Vector3.one * Mathf.Max(0.5f, scale);
    }

    void EnsureMainCameraForDirectPlay(BusController bus)
    {
        if (!attachMainCameraToBusForDirectPlay || bus == null)
            return;

        var cam = Camera.main;
        if (cam == null)
            cam = FindObjectOfType<Camera>();
        if (cam == null)
            return;

        DisableCinemachineBrain(cam);

        Transform camTransform = cam.transform;
        if (camTransform.parent == bus.transform)
            return;

        camTransform.SetParent(bus.transform, false);
        camTransform.localPosition = directPlayCameraLocalPosition;
        camTransform.localRotation = Quaternion.Euler(directPlayCameraLocalEuler);
    }

    void DisableCinemachineVirtualCamerasForDirectPlay()
    {
        if (!disableCinemachineVirtualCamerasForDirectPlay)
            return;

        var behaviours = FindObjectsOfType<MonoBehaviour>(true);
        for (int i = 0; i < behaviours.Length; i++)
        {
            var behaviour = behaviours[i];
            if (behaviour == null)
                continue;

            var type = behaviour.GetType();
            if (type == null)
                continue;

            string fullName = type.FullName ?? string.Empty;
            if (fullName.Contains("CinemachineVirtualCamera") ||
                fullName.Contains("CinemachineFreeLook"))
            {
                behaviour.gameObject.SetActive(false);
            }
        }
    }

    void DisableCinemachineBrain(Camera cam)
    {
        if (!disableCinemachineBrainForDirectPlay || cam == null)
            return;

        var behaviours = cam.GetComponents<MonoBehaviour>();
        for (int i = 0; i < behaviours.Length; i++)
        {
            var behaviour = behaviours[i];
            if (behaviour == null)
                continue;

            var type = behaviour.GetType();
            string fullName = type != null ? type.FullName ?? string.Empty : string.Empty;
            if (fullName.Contains("CinemachineBrain"))
                behaviour.enabled = false;
        }
    }

    void ConfigureRoadGraphBackedSystems(BusRoute route)
    {
        var usableGraph = FindUsableRoadGraph(route);
        if (usableGraph == null)
            usableGraph = TryRebuildAlignedRoadGraph(route);

        var vehiclePool = FindObjectOfType<VehiclePool>();
        if (vehiclePool != null)
        {
            vehiclePool.roadGraph = usableGraph;
            vehiclePool.allowAutoResolveRoadGraph = usableGraph != null;
            if (usableGraph == null)
                Debug.LogWarning("SceneBootstrap: VehiclePool AIRoadGraph is misaligned with the active city. Traffic graph usage disabled for this direct-play run.");
        }

        var pedestrianSpawner = FindObjectOfType<PedestrianSpawner>();
        if (pedestrianSpawner != null)
        {
            pedestrianSpawner.roadGraph = usableGraph;
            pedestrianSpawner.useRandomGeneratedCrossings = usableGraph != null;
            if (usableGraph == null)
                Debug.LogWarning("SceneBootstrap: PedestrianSpawner AIRoadGraph is misaligned with the active city. Random crossings disabled for this direct-play run.");
        }
    }

    AIRoadGraph TryRebuildAlignedRoadGraph(BusRoute route)
    {
        var city = GameState.Instance?.selectedCity ?? CityManager.Instance?.activeCity;
        if (city == null)
            return null;

        string roadsPath = ResolveRoadsJsonPath(city);
        if (string.IsNullOrWhiteSpace(roadsPath) || !File.Exists(roadsPath))
            return null;

        try
        {
            string roadsJson = File.ReadAllText(roadsPath);
            var response = OverpassResponse.Deserialize(roadsJson);
            if (response == null || response.elements == null || response.elements.Count == 0)
                return null;

            var converterGo = new GameObject("CoordinateConverter_RuntimeGraphTemp");
            var converter = converterGo.AddComponent<CoordinateConverter>();
            converter.mapOrigin = ScriptableObject.CreateInstance<MapOrigin>();
            converter.mapOrigin.SetFromCity(city);

            var roadGraph = RoadGraph.BuildFromOverpassWays(response, converter);
            Destroy(converterGo);

            if (roadGraph == null || roadGraph.nodes == null || roadGraph.nodes.Count == 0)
                return null;

            var airGraph = FindFirstObjectByType<AIRoadGraph>();
            if (airGraph == null)
            {
                var graphGo = new GameObject("AIRoadGraph");
                airGraph = graphGo.AddComponent<AIRoadGraph>();
            }

            RebuildAirRoadGraph(airGraph, roadGraph);

            var alignedGraph = FindUsableRoadGraph(route);
            if (alignedGraph != null)
            {
                Debug.Log(
                    $"SceneBootstrap: Rebuilt AIRoadGraph for {city.cityName} from {Path.GetFileName(roadsPath)} " +
                    $"({alignedGraph.NodeCount} nodes).");
            }

            return alignedGraph;
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning($"SceneBootstrap: Failed to rebuild AIRoadGraph for {city.cityName}. {ex.Message}");
            return null;
        }
    }

    string ResolveRoadsJsonPath(CityDefinition city)
    {
        if (city == null || string.IsNullOrWhiteSpace(city.cityCode))
            return null;

        string cityDir = Path.Combine(Application.streamingAssetsPath, "Cities", city.cityCode);
        string preferredJson = Path.Combine(cityDir, "roads.json");
        if (File.Exists(preferredJson))
            return preferredJson;

        string configuredPath = city.GetRoadsPath();
        if (!string.IsNullOrWhiteSpace(configuredPath) && File.Exists(configuredPath))
            return configuredPath;

        if (!string.IsNullOrWhiteSpace(configuredPath))
        {
            string configuredJson = Path.ChangeExtension(configuredPath, "json");
            if (File.Exists(configuredJson))
                return configuredJson;
        }

        return null;
    }

    void RebuildAirRoadGraph(AIRoadGraph target, RoadGraph source)
    {
        if (target == null || source == null || source.nodes == null)
            return;

        for (int i = target.transform.childCount - 1; i >= 0; i--)
            Destroy(target.transform.GetChild(i).gameObject);

        var nodes = new AIRoadGraph.RoadNode[source.nodes.Count];
        for (int i = 0; i < source.nodes.Count; i++)
        {
            var sourceNode = source.nodes[i];
            var pointGo = new GameObject($"Node_{i:0000}");
            pointGo.transform.SetParent(target.transform, false);
            pointGo.transform.position = new Vector3(sourceNode.world.x, 0.1f, sourceNode.world.z);

            float speedLimit = 30f;
            if (sourceNode.edges != null)
            {
                for (int edgeIndex = 0; edgeIndex < sourceNode.edges.Count; edgeIndex++)
                    speedLimit = Mathf.Max(speedLimit, sourceNode.edges[edgeIndex].speedLimitKmh);
            }

            int[] nextNodeIndices = sourceNode.edges != null
                ? new int[sourceNode.edges.Count]
                : System.Array.Empty<int>();

            if (sourceNode.edges != null)
            {
                for (int edgeIndex = 0; edgeIndex < sourceNode.edges.Count; edgeIndex++)
                    nextNodeIndices[edgeIndex] = sourceNode.edges[edgeIndex].to;
            }

            nodes[i] = new AIRoadGraph.RoadNode
            {
                id = $"N{i:0000}",
                point = pointGo.transform,
                laneCount = 2,
                laneWidth = 3.3f,
                speedLimitKmh = speedLimit,
                trafficLight = null,
                nextNodeIndices = nextNodeIndices
            };
        }

        target.nodes = nodes;
    }

    AIRoadGraph FindUsableRoadGraph(BusRoute route = null)
    {
        var graphs = FindObjectsByType<AIRoadGraph>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        if (graphs == null || graphs.Length == 0)
            return null;

        var city = GameState.Instance?.selectedCity ?? CityManager.Instance?.activeCity;
        if (city == null)
        {
            for (int i = 0; i < graphs.Length; i++)
            {
                if (graphs[i] != null && graphs[i].NodeCount > 0)
                    return graphs[i];
            }
            return null;
        }

        BusStopData referenceStop = null;
        if (route != null && route.stops != null && route.stops.Length > 0)
            referenceStop = route.stops[0];

        Vector3 referenceWorld = referenceStop != null
            ? CoordinateConverter.LocalOriginGeoToWorld(referenceStop.latitude, referenceStop.longitude, city.centreLat, city.centreLon)
            : CoordinateConverter.LocalOriginGeoToWorld(city.spawnLat, city.spawnLon, city.centreLat, city.centreLon);

        AIRoadGraph bestGraph = null;
        float bestDistance = float.MaxValue;

        for (int i = 0; i < graphs.Length; i++)
        {
            var graph = graphs[i];
            if (graph == null || graph.NodeCount == 0)
                continue;

            int nearest = graph.GetNearestNodeIndex(referenceWorld);
            if (!graph.IsValidNode(nearest))
                continue;

            float distance = Vector3.Distance(referenceWorld, graph.GetNodePosition(nearest));
            if (distance < bestDistance)
            {
                bestDistance = distance;
                bestGraph = graph;
            }
        }

        return bestDistance <= 500f ? bestGraph : null;
    }
}
