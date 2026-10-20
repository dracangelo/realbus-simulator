using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(-200)]
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
    [Tooltip("Open the scene on clear weather with a clear view. The weather stays dynamic after that: this only clears the start, it does not disable the weather or fog systems.")]
    [SerializeField] bool forceClearVisibilityForDirectPlay = true;
    [SerializeField] bool suppressBuildingsForCurrentPhase = false;
    [SerializeField] bool autoStartSelectedRoute = true;
    [SerializeField] Vector3 directPlayCameraLocalPosition = new Vector3(0f, 3.2f, -7.5f);
    [SerializeField] Vector3 directPlayCameraLocalEuler = new Vector3(14f, 0f, 0f);
    [Header("Gameplay Fallbacks")]
    [SerializeField] BusRoute fallbackRoute;
    [SerializeField] MissionData fallbackMissionData;
    [SerializeField] MissionData[] knownMissionData;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void EnsureGameplaySceneHasBootstrap()
    {
        if (FindAnyObjectByType<MissionManager>() == null || FindAnyObjectByType<SceneBootstrap>() != null)
            return;

        new GameObject("SceneBootstrap (Runtime)").AddComponent<SceneBootstrap>();
    }

    // All scene singletons must finish Awake before setup can resolve them.
    // Run before loader/controller Start methods, but never create competing
    // managers or temporary singleton converters during scene deserialization.
    void Start()
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
        var city = CityManager.Instance.activeCity;
        if (city != null)
        {
            var converter = FindAnyObjectByType<CoordinateConverter>();
            if (converter != null) converter.SetCityOrigin(city);
            var tiles = FindAnyObjectByType<MapTileLoader>();
            if (tiles != null) { tiles.centreLat = city.centreLat; tiles.centreLon = city.centreLon; }
        }
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

        // The explicit player selection is authoritative. CityManager may still
        // contain the country/city that was active on the main menu.
        if (gameState.selectedCountry != null && cityManager.activeCountry != gameState.selectedCountry)
            cityManager.SetActiveCountry(gameState.selectedCountry);

        if (cityManager.activeCountry == null && cityManager.allCountries != null && cityManager.allCountries.Length > 0)
            cityManager.activeCountry = cityManager.allCountries[0];

        if (gameState.selectedCity != null)
            cityManager.SetActiveCity(gameState.selectedCity);

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

        if (gameState.selectedCity == null && cityManager.activeCity != null)
            gameState.selectedCity = cityManager.activeCity;
    }

    void ConfigureGameplaySceneForDirectPlay()
    {
        var missionManager = FindAnyObjectByType<MissionManager>();
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
            missionManager.busController = FindAnyObjectByType<BusController>();

        missionManager.busController = InstantiateSelectedDrivableBus(missionManager.busController);

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

        EnsureBusDepotManager();
        EnsureClearVisibility();
        ApplyBusScaleFromMap();
        ConfigureSelectedRouteStart(missionManager, selectedRoute != null ? routeToUse : null);
    }

    void ConfigureSelectedRouteStart(MissionManager missionManager, BusRoute selectedRoute)
    {
        FreeDriveSession freeDrive = FindAnyObjectByType<FreeDriveSession>();
        bool hasSelectedMission = autoStartSelectedRoute && selectedRoute != null && selectedRoute.GetStopCount() >= 2;
        if (freeDrive != null)
        {
            freeDrive.autoStart = !hasSelectedMission;
            if (hasSelectedMission && freeDrive.sessionActive)
                freeDrive.EndSession();
        }

        if (hasSelectedMission)
            StartCoroutine(StartSelectedRouteWhenReady(missionManager, selectedRoute));
    }

    IEnumerator StartSelectedRouteWhenReady(MissionManager missionManager, BusRoute route)
    {
        route.EnsureRuntimeData();
        for (int frame = 0; frame < 300; frame++)
        {
            if (missionManager == null || missionManager.routeActive || missionManager.CurrentCountdownValue > 0)
                yield break;

            if (missionManager.busController != null && GPSManager.Instance != null)
            {
                missionManager.StartRoute(route);
                Debug.Log($"SceneBootstrap: Starting selected route '{route.routeName}' instead of free drive.");
                yield break;
            }
            yield return null;
        }

        Debug.LogError($"SceneBootstrap: Timed out starting selected route '{route.routeName}'.");
    }

    BusController InstantiateSelectedDrivableBus(BusController sceneBus)
    {
        BusSpec spec = BusFleetManager.EnsureExists().GetSelectedDrivableBusSpec();
        if (sceneBus == null || spec == null || spec.drivablePrefab == null)
            return sceneBus;

        Vector3 position = sceneBus.transform.position;
        Quaternion rotation = sceneBus.transform.rotation;
        Camera gameplayCamera = Camera.main != null ? Camera.main : FindAnyObjectByType<Camera>();
        if (gameplayCamera != null && gameplayCamera.transform.IsChildOf(sceneBus.transform))
            gameplayCamera.transform.SetParent(null, true);

        GameObject instance = Instantiate(spec.drivablePrefab, position, rotation);
        instance.name = spec.displayName + " (Player Bus)";
        BusController replacement = instance.GetComponent<BusController>();
        if (replacement == null)
        {
            Debug.LogError($"SceneBootstrap: Drivable prefab for '{spec.displayName}' has no BusController. Keeping scene bus.");
            Destroy(instance);
            return sceneBus;
        }

        foreach (MobileControlsUI controls in FindObjectsByType<MobileControlsUI>(FindObjectsInactive.Include))
            controls.busController = replacement;

        sceneBus.gameObject.SetActive(false);
        Destroy(sceneBus.gameObject);
        Debug.Log($"SceneBootstrap: Using real drivable prefab '{spec.drivablePrefab.name}' for physics and docking.");
        return replacement;
    }

    void EnsureVehicleSupportSystems(BusController busController)
    {
        if (busController == null)
            return;

        if (busController.GetComponent<MaintenanceSystem>() == null)
            busController.gameObject.AddComponent<MaintenanceSystem>();
        if (busController.GetComponent<FuelSystem>() == null && busController.GetComponent<BatterySystem>() == null)
            busController.gameObject.AddComponent<FuelSystem>();

        var visualRepair = busController.GetComponent<ImportedVehicleVisualRepair>();
        if (visualRepair == null)
            visualRepair = busController.gameObject.AddComponent<ImportedVehicleVisualRepair>();
        visualRepair.RepairNow(busController.modelRoot != null ? busController.modelRoot : busController.transform);
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

        var passengerManager = FindAnyObjectByType<PassengerManager>();
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
        // ApplyBusSpec can swap in a different imported model after the support
        // systems ran. Repair that model before the livery asks which slots are
        // bodywork, otherwise the new shell is left flat white.
        var visualRepair = busController.GetComponent<ImportedVehicleVisualRepair>();
        if (visualRepair != null)
            visualRepair.RepairNow(visualRoot);
        liveryEditor.Initialize(spec.busId, visualRoot.GetComponentsInChildren<Renderer>(true), null, visualRoot);
    }

    void EnsureGpsManager()
    {
        if (FindAnyObjectByType<GPSManager>() != null)
            return;

        var mapLoader = FindAnyObjectByType<MapTileLoader>();
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

        var tracker = FindAnyObjectByType<GpsTracker>();
        if (tracker == null)
        {
            var go = new GameObject("GpsTracker");
            tracker = go.AddComponent<GpsTracker>();
        }

        tracker.busTransform = busController.transform;
        if (tracker.converter == null)
            tracker.converter = CoordinateConverter.Instance ?? FindAnyObjectByType<CoordinateConverter>();
    }

    void EnsurePhotoMode(BusController busController)
    {
        var photoMode = FindAnyObjectByType<PhotoModeController>();
        if (photoMode == null)
        {
            var go = new GameObject("PhotoModeController");
            photoMode = go.AddComponent<PhotoModeController>();
        }

        if (photoMode.targetCamera == null)
            photoMode.targetCamera = Camera.main != null ? Camera.main : FindAnyObjectByType<Camera>();
        if (photoMode.busTransform == null && busController != null)
            photoMode.busTransform = busController.transform;
    }

    void EnsureRuntimeRoadSystems()
    {
        var osmLoader = FindAnyObjectByType<OSMLoader>();
        if (osmLoader == null)
        {
            var loaderGo = new GameObject("OSMLoader");
            osmLoader = loaderGo.AddComponent<OSMLoader>();
        }

        var roadBuilder = FindAnyObjectByType<OSMRoadMeshBuilder>();
        if (roadBuilder == null)
        {
            var roadsGo = new GameObject("OSMRoadMeshBuilder");
            roadBuilder = roadsGo.AddComponent<OSMRoadMeshBuilder>();
        }

        // Always render + collide roads in direct-play so the player bus can drive on geometry.
        roadBuilder.renderRoadSurface = true;
        roadBuilder.drawCenterLines = true;
        roadBuilder.useRoadModelInstances = true;
        roadBuilder.roadModelResourcePath = "RoadsGenerated";
        roadBuilder.overrideRoadModelMaterials = true;
        roadBuilder.generateRoadBoundaries = true;
        roadBuilder.addPhysicalBoundaryColliders = true;
        roadBuilder.maxColliderSegmentLength = Mathf.Clamp(roadBuilder.maxColliderSegmentLength, 10f, 60f);
        roadBuilder.SetRoadSurfaceVisible(true);

        var mapLoader = FindAnyObjectByType<MapTileLoader>();
        if (mapLoader != null)
        {
            // Twelve centimetres, not three. The ground tile and the road ribbon
            // are coplanar enough at distance that the depth buffer swapped them
            // from frame to frame; at 0.3 m near clip and 12 cm separation the
            // order stays stable out past the 950 m fog wall.
            roadBuilder.roadYOffset = Mathf.Max(mapLoader.tileSurfaceY + 0.12f, 0.12f);
            mapLoader.zoomLevel = Mathf.Max(mapLoader.zoomLevel, 17);
            mapLoader.tilesX = Mathf.Max(mapLoader.tilesX, 7);
            mapLoader.tilesY = Mathf.Max(mapLoader.tilesY, 7);
            // Load the 5x5 window first, not just the centre cross: the
            // loading overlay now waits for the near ring and the player
            // should not land on a half-tiled street.
            mapLoader.highPriorityRing = Mathf.Max(mapLoader.highPriorityRing, 2);
            mapLoader.forceSharpTileFiltering = true;
            mapLoader.tileAnisoLevel = 16;
            // A negative mip bias sharpened the near ground but made the far
            // rows crawl as the bus moved. Zero keeps mip selection honest.
            mapLoader.tileMipMapBias = 0f;
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

    void EnsureTrafficViolationSystem()
    {
        if (FindAnyObjectByType<ExtendedTrafficViolationSystem>() != null)
            return;

        var bus = FindAnyObjectByType<BusController>();
        if (bus == null)
            return;

        var violationSystem = bus.gameObject.AddComponent<ExtendedTrafficViolationSystem>();
        violationSystem.roadGraph = FindUsableRoadGraph();
    }

    void EnsureDynamicEventSystem()
    {
        if (FindAnyObjectByType<DynamicEventSystem>() != null)
            return;

        var go = new GameObject("DynamicEventSystem");
        var system = go.AddComponent<DynamicEventSystem>();
        system.busController = FindAnyObjectByType<BusController>();
    }

    void EnsurePassengerSystems()
    {
        if (FindAnyObjectByType<PassengerSpawner>() == null)
        {
            var spawnerGo = new GameObject("PassengerSpawner");
            spawnerGo.AddComponent<PassengerSpawner>();
        }

        var passengerManager = FindAnyObjectByType<PassengerManager>();
        if (passengerManager == null)
        {
            var managerGo = new GameObject("PassengerManager");
            passengerManager = managerGo.AddComponent<PassengerManager>();
        }

        if (passengerManager.passengerSpawner == null)
            passengerManager.passengerSpawner = FindAnyObjectByType<PassengerSpawner>();
    }

    void EnsureAiTrafficSystems()
    {
        var graph = FindUsableRoadGraph();
        if (graph == null)
            return;

        var pool = FindAnyObjectByType<VehiclePool>();
        if (pool == null)
        {
            var go = new GameObject("VehiclePool");
            pool = go.AddComponent<VehiclePool>();
        }

        pool.roadGraph = graph;
        pool.allowAutoResolveRoadGraph = true;
        ConfigureVehiclePoolPrefabs(pool);

        var busSpawner = FindAnyObjectByType<AIBusScheduleSpawner>();
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
        var skyController = FindAnyObjectByType<SkyController>();
        if (skyController == null)
        {
            var skyGo = new GameObject("SkyController");
            skyController = skyGo.AddComponent<SkyController>();
            skyController.sunLight = RenderSettings.sun;
        }

        var rainController = FindAnyObjectByType<RainController>();
        if (rainController == null)
            rainController = new GameObject("RainController").AddComponent<RainController>();

        var fogController = FindAnyObjectByType<FogController>();
        if (fogController == null)
            fogController = new GameObject("FogController").AddComponent<FogController>();

        // Snow and the sky visuals were both missing here: WeatherSystem.snowController
        // was never assigned, so a snow state could only change the light, and nothing
        // ever built clouds or a skybox.
        var snowController = FindAnyObjectByType<SnowController>();
        if (snowController == null)
            snowController = new GameObject("SnowController").AddComponent<SnowController>();

        var timeSystem = FindAnyObjectByType<TimeOfDaySystem>();
        if (timeSystem == null)
        {
            var go = new GameObject("TimeOfDaySystem");
            timeSystem = go.AddComponent<TimeOfDaySystem>();
            timeSystem.skyController = skyController;
            timeSystem.directionalSun = RenderSettings.sun != null ? RenderSettings.sun : FindDirectionalSun();
        }
        fogController.timeOfDay = timeSystem;

        var skyEnvironment = SkyEnvironmentController.EnsureExists();

        var weatherSystem = FindAnyObjectByType<WeatherSystem>();
        if (weatherSystem == null)
        {
            var go = new GameObject("WeatherSystem");
            weatherSystem = go.AddComponent<WeatherSystem>();
        }
        weatherSystem.skyController = skyController;
        weatherSystem.rainController = rainController;
        weatherSystem.fogController = fogController;
        weatherSystem.snowController = snowController;
        weatherSystem.skyEnvironmentController = skyEnvironment;

        // Precipitation has to exist before a state can show it, and it has to follow
        // the bus rather than sit over one patch of city. The shower resolves the bus
        // itself when it is created later, so this is only the first chance.
        var bus = FindAnyObjectByType<BusController>();
        Transform follow = bus != null ? bus.transform : (Camera.main != null ? Camera.main.transform : null);
        rainController.followTarget = follow;
        rainController.EnsureEffects();
        snowController.followTarget = follow;
        snowController.EnsureEffects();
    }

    static Light FindDirectionalSun()
    {
        var lights = FindObjectsByType<Light>(FindObjectsInactive.Include);
        for (int i = 0; i < lights.Length; i++)
            if (lights[i] != null && lights[i].type == LightType.Directional) return lights[i];
        return null;
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
        if (route != null)
        {
            route.EnsureRuntimeData();
            if (route.stops != null && route.stops.Length > 0)
                return route;
        }

        var city = GameState.Instance?.selectedCity ?? CityManager.Instance?.activeCity;
        if (city == null)
            return route;

        string stopsPath = RuntimeCityContentStore.ResolveReadPath(city, "stops.json");
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
        runtimeRoute.EnsureRuntimeData();

        Debug.Log($"SceneBootstrap: Built runtime fallback route '{runtimeRoute.routeName}' from {parsedStops.Length} downloaded OSM stops.");
        return runtimeRoute;
    }

    void EnsureRuntimeLogger()
    {
        if (FindAnyObjectByType<RuntimeSpawnLogger>() != null)
            return;

        var loggerGo = new GameObject("RuntimeSpawnLogger");
        loggerGo.AddComponent<RuntimeSpawnLogger>();
    }

    void EnsurePedestrianSpawner()
    {
        var existing = FindAnyObjectByType<PedestrianSpawner>();
        if (existing != null)
        {
            if (existing.pedestrianPrefab == null)
                existing.pedestrianPrefab = LoadFirstResourcePrefab(
                    "LiveCityGenerated/ab834887507b6c0fba84796be9f2c3ea",
                    "BussimAssets/passenger/uploads_files_6231172_woman+3d+fbx",
                    "BussimAssets/passenger/Realistic man",
                    "BussimAssets/passenger/uploads_files_2460380_Marina_1276",
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
            "LiveCityGenerated/ab834887507b6c0fba84796be9f2c3ea",
            "BussimAssets/passenger/uploads_files_6231172_woman+3d+fbx",
            "BussimAssets/passenger/Realistic man",
            "BussimAssets/passenger/uploads_files_2460380_Marina_1276",
            "BussimAssets/passenger/Man_in_black_dress_0227");
    }

    void EnsureEnvironmentSpawner()
    {
        if (FindAnyObjectByType<RuntimeEnvironmentSpawner>() != null)
            return;

        var go = new GameObject("RuntimeEnvironmentSpawner");
        go.AddComponent<RuntimeEnvironmentSpawner>();
    }

    void EnsureStopPropSpawner()
    {
        var existing = FindAnyObjectByType<StopPropSpawner>();
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
        if (FindAnyObjectByType<RuntimeGasStationSpawner>() != null)
            return;

        var go = new GameObject("RuntimeGasStationSpawner");
        go.AddComponent<RuntimeGasStationSpawner>();
    }

    void EnsureBusDepotManager()
    {
        BusDepotManager.EnsureExists();
    }

    void EnsureTarmacApplier()
    {
        if (FindAnyObjectByType<RuntimeTarmacApplier>() != null)
            return;

        var go = new GameObject("RuntimeTarmacApplier");
        go.AddComponent<RuntimeTarmacApplier>();
    }

    void EnsureBuildingSystems()
    {
        if (suppressBuildingsForCurrentPhase && !autoPopulateCityFromOsm)
        {
            var buildingLoader = FindAnyObjectByType<OSMBuildingLoader>();
            if (buildingLoader != null)
                buildingLoader.enabled = false;

            var buildingBuilder = FindAnyObjectByType<OSMBuildingMeshBuilder>();
            if (buildingBuilder != null)
                buildingBuilder.enabled = false;

            var existingBuildings = GameObject.Find("OSM_Buildings");
            if (existingBuildings != null)
                existingBuildings.SetActive(false);

            return;
        }

        if (FindAnyObjectByType<OSMBuildingLoader>() == null)
        {
            var loaderGo = new GameObject("OSMBuildingLoader");
            loaderGo.AddComponent<OSMBuildingLoader>();
        }
        else
        {
            FindAnyObjectByType<OSMBuildingLoader>().enabled = true;
        }

        var builder = FindAnyObjectByType<OSMBuildingMeshBuilder>();
        if (builder == null)
        {
            var builderGo = new GameObject("OSMBuildingMeshBuilder");
            builder = builderGo.AddComponent<OSMBuildingMeshBuilder>();
        }
        else
        {
            builder.enabled = true;
        }

        var mapLoader = FindAnyObjectByType<MapTileLoader>();
        if (mapLoader != null)
            builder.buildingBaseYOffset = Mathf.Max(mapLoader.tileSurfaceY + 0.12f, 0.24f);

        builder.maxBuildingsPerFrame = Mathf.Max(builder.maxBuildingsPerFrame, 500);
        builder.enableDistanceCulling = false;
        builder.streamAroundVehicle = true;
        builder.buildingLoadDistance = Mathf.Max(700f, builder.buildingLoadDistance);
        builder.buildingUnloadDistance = Mathf.Max(builder.buildingLoadDistance + 200f, builder.buildingUnloadDistance);
        builder.maxBuildingLoadsPerRefresh = Mathf.Max(400, builder.maxBuildingLoadsPerRefresh);
        builder.drawFootprints = false;

        // Fill the blocks as well as the mapped footprints. The infill is what
        // makes unmapped Nairobi streets read as a city instead of a road
        // network in a field.
        var infill = builder.GetComponent<ProceduralBuildingFill>();
        if (infill == null)
            infill = builder.gameObject.AddComponent<ProceduralBuildingFill>();
        infill.loadRadius = Mathf.Max(infill.loadRadius, 560f);
        infill.unloadRadius = Mathf.Max(infill.unloadRadius, 760f);
        infill.maxBuildings = Mathf.Max(infill.maxBuildings, 3200);
        infill.maxBuildingsPerFrame = Mathf.Max(infill.maxBuildingsPerFrame, 40);
        infill.rowsPerSide = Mathf.Max(infill.rowsPerSide, 7);
        infill.maxImportedBuildings = Mathf.Max(infill.maxImportedBuildings, 240);

        var activeBuildingsRoot = GameObject.Find("OSM_Buildings");
        if (activeBuildingsRoot != null)
            activeBuildingsRoot.SetActive(true);
    }

    void EnsureOsmCityPopulation()
    {
        if (!autoPopulateCityFromOsm)
            return;

        // Stream the imported scenery instead of spawning the old whole-city scatter.
        if (Resources.Load<LiveCityCatalog>("LiveCityCatalog") != null)
        {
            foreach (var legacy in FindObjectsByType<OsmCityPopulationSystem>())
            { legacy.populateOnStart = false; legacy.StopAllCoroutines(); legacy.enabled = false; }
            if (FindAnyObjectByType<LiveCityStreamer>() == null)
                new GameObject("Live City").AddComponent<LiveCityStreamer>();
            return;
        }

        var populator = FindAnyObjectByType<OsmCityPopulationSystem>();
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
        {
            foreach (var visualizer in FindObjectsByType<RuntimeOsmPoiVisualizer>())
                visualizer.enabled = false;
            return;
        }

        if (FindAnyObjectByType<RuntimeOsmPoiVisualizer>() != null)
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
        int importedModels = AddCatalogTraffic(entries);
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
        {
            // Optional traffic packs should improve variety, not be a requirement
            // for a populated city. Nothing under Assets/_Game/Resources/BussimAssets/aitraffic
            // is a loadable prefab (the packs ship as .zip files), and no traffic
            // catalog was built, so these local low-poly variants are what traffic
            // runs on: several shapes and colours beat one repeated box.
            entries.Add(FallbackTrafficEntry(AIVehicleController.VehicleType.Car, 1.0f, FallbackVehicleKind.Sedan));
            entries.Add(FallbackTrafficEntry(AIVehicleController.VehicleType.Car, 0.8f, FallbackVehicleKind.Hatchback));
            entries.Add(FallbackTrafficEntry(AIVehicleController.VehicleType.Car, 0.6f, FallbackVehicleKind.Van));
            entries.Add(FallbackTrafficEntry(AIVehicleController.VehicleType.Truck, 0.4f, FallbackVehicleKind.Pickup));
            Debug.Log("SceneBootstrap: No imported traffic catalog. Run Tools > RealBus > Setup > Build Traffic Models to use the downloaded vehicle FBXs; using 4 built-in low-poly variants for now.");
        }

        pool.prefabs = entries.ToArray();
        // The downloaded FBX models (Stylized Vehicles + Van) are cheap geometry that
        // drives on the correct side of the road via CityManager.activeCountry.DrivesOnLeft.
        // A larger pool at higher density fractions is what puts them visibly on the streets.
        pool.poolSizePerScene = Mathf.Max(pool.poolSizePerScene, importedModels > 0 ? 160 : 80);
        pool.minActiveVehicles = importedModels > 0 ? 32 : 12;
        if (importedModels > 0)
            pool.maxActiveFraction = Mathf.Clamp(pool.maxActiveFraction, 0.45f, 0.75f);
        else
            pool.debugForceActiveCount = Mathf.Max(pool.debugForceActiveCount, 14);
        pool.logSpawnEvents = false;
    }

    /// <summary>
    /// The downloaded vehicle models, as normalised by the traffic importer. They
    /// are the point of the download: without a built catalog the pool can only
    /// spawn procedural boxes, because the packs ship as .zip files rather than as
    /// prefabs. Weights come from the catalog, where denser models ask for less.
    /// </summary>
    int AddCatalogTraffic(List<VehiclePool.VehiclePrefabEntry> entries)
    {
        var catalog = Resources.Load<TrafficCatalog>("TrafficCatalog");
        if (catalog == null || catalog.UsableCount == 0)
            return 0;

        var names = new List<string>();
        foreach (var entry in catalog.entries)
        {
            if (entry == null || entry.prefab == null)
                continue;
            entries.Add(new VehiclePool.VehiclePrefabEntry
            {
                type = entry.vehicleType,
                prefab = entry.prefab,
                weight = Mathf.Clamp(entry.weight > 0f ? entry.weight : 0.2f, 0.02f, 1f)
            });
            names.Add($"{entry.displayName} ({entry.vehicleType}, {entry.triangles} tris)");
        }

        Debug.Log($"SceneBootstrap: Traffic catalog supplies {names.Count} imported models: {string.Join(", ", names)}.");
        return names.Count;
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

    enum FallbackVehicleKind
    {
        Sedan,
        Hatchback,
        Van,
        Pickup
    }

    VehiclePool.VehiclePrefabEntry FallbackTrafficEntry(AIVehicleController.VehicleType type, float weight, FallbackVehicleKind kind)
        => new VehiclePool.VehiclePrefabEntry { type = type, prefab = CreateFallbackTrafficVehicle(kind), weight = weight };

    GameObject CreateFallbackTrafficVehicle(FallbackVehicleKind kind = FallbackVehicleKind.Sedan)
    {
        var root = new GameObject($"Runtime Traffic {kind} (Fallback)");
        root.hideFlags = HideFlags.DontSave;

        // Body, cabin, paint and wheel size per kind, so vehicles are tellable
        // apart at a glance even though they are all primitives.
        Vector3 bodySize, bodyOffset, cabinSize, cabinOffset, wheelOffset;
        Color paint;
        float wheelRadius;

        switch (kind)
        {
            case FallbackVehicleKind.Hatchback:
                bodySize = new Vector3(1.75f, .8f, 3.6f); bodyOffset = new Vector3(0f, .6f, 0f);
                cabinSize = new Vector3(1.5f, .62f, 1.9f); cabinOffset = new Vector3(0f, 1.25f, -.35f);
                wheelOffset = new Vector3(1f, .35f, 1.15f); wheelRadius = .46f;
                paint = new Color(.62f, .2f, .2f);
                break;
            case FallbackVehicleKind.Van:
                bodySize = new Vector3(2f, 1.5f, 4.8f); bodyOffset = new Vector3(0f, .95f, 0f);
                cabinSize = new Vector3(1.85f, .5f, 1.4f); cabinOffset = new Vector3(0f, 1.85f, 1.35f);
                wheelOffset = new Vector3(1.05f, .4f, 1.6f); wheelRadius = .52f;
                paint = new Color(.85f, .85f, .86f);
                break;
            case FallbackVehicleKind.Pickup:
                bodySize = new Vector3(1.9f, .7f, 4.6f); bodyOffset = new Vector3(0f, .58f, 0f);
                cabinSize = new Vector3(1.7f, .7f, 1.6f); cabinOffset = new Vector3(0f, 1.15f, .5f);
                wheelOffset = new Vector3(1.05f, .38f, 1.5f); wheelRadius = .5f;
                paint = new Color(.16f, .4f, .3f);
                break;
            default:
                bodySize = new Vector3(1.8f, .75f, 4.2f); bodyOffset = new Vector3(0f, .55f, 0f);
                cabinSize = new Vector3(1.55f, .55f, 2.15f); cabinOffset = new Vector3(0f, 1.15f, -.25f);
                wheelOffset = new Vector3(1f, .35f, 1.35f); wheelRadius = .48f;
                paint = new Color(.12f, .35f, .68f);
                break;
        }

        var body = GameObject.CreatePrimitive(PrimitiveType.Cube);
        body.name = "Body";
        body.transform.SetParent(root.transform, false);
        body.transform.localPosition = bodyOffset;
        body.transform.localScale = bodySize;
        body.GetComponent<Renderer>().sharedMaterial = CreateFallbackTrafficMaterial("TrafficBody", paint);

        var cabin = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cabin.name = "Cabin";
        cabin.transform.SetParent(root.transform, false);
        cabin.transform.localPosition = cabinOffset;
        cabin.transform.localScale = cabinSize;
        var cabinCollider = cabin.GetComponent<Collider>();
        if (cabinCollider != null) Destroy(cabinCollider);
        cabin.GetComponent<Renderer>().sharedMaterial = CreateFallbackTrafficMaterial("TrafficGlass", new Color(.06f, .12f, .18f));

        for (int side = -1; side <= 1; side += 2)
        for (int axle = -1; axle <= 1; axle += 2)
        {
            var wheel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            wheel.name = "Wheel";
            wheel.transform.SetParent(root.transform, false);
            wheel.transform.localPosition = new Vector3(side * wheelOffset.x, wheelOffset.y, axle * wheelOffset.z);
            wheel.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            wheel.transform.localScale = Vector3.one * wheelRadius;
            var wheelCollider = wheel.GetComponent<Collider>();
            if (wheelCollider != null) Destroy(wheelCollider);
            wheel.GetComponent<Renderer>().sharedMaterial = CreateFallbackTrafficMaterial("TrafficTyre", new Color(.035f, .035f, .04f));
        }

        root.SetActive(false);
        return root;
    }

    Material CreateFallbackTrafficMaterial(string name, Color color)
    {
        return new Material(BuiltInShaders.Lit) { name = name + " (Runtime)", color = color };
    }

    /// <summary>
    /// Opens the scene on clear weather with an unobstructed view. The flag asks for
    /// a clear <em>start</em>, not for the environment to be switched off: pinning the
    /// weather and disabling the weather and fog controllers here meant clouds, rain
    /// and weather fog could never appear during a real drive.
    /// </summary>
    void EnsureClearVisibility()
    {
        if (!forceClearVisibilityForDirectPlay)
            return;

        var weatherSystem = FindAnyObjectByType<WeatherSystem>();
        if (weatherSystem != null)
        {
            weatherSystem.forceWeather = false;
            weatherSystem.forcedWeatherState = WeatherState.Clear;
            weatherSystem.currentWeather = WeatherState.Clear;
        }

        var skyController = FindAnyObjectByType<SkyController>();
        if (skyController != null)
            skyController.SetSky(WeatherState.Clear, 0f, 24f);

        // Clear weather means no weather fog: the city's distance haze owns the fog
        // until real weather arrives, and it hands the fog straight back when it does.
        CinematicAtmosphereController.EnsureExists().ApplyAtmosphericHaze();
    }

    void ApplyBusScaleFromMap()
    {
        var bus = FindAnyObjectByType<BusController>();
        if (bus == null) return;

        // GPSToWorld already returns metre-scaled coordinates. Scaling a Rigidbody
        // hierarchy also scales its WheelColliders, which left imported buses tiny,
        // visually broken and unable to put usable torque into the road.
        if (bus.transform.localScale != Vector3.one)
            Debug.LogWarning($"SceneBootstrap: Reset invalid vehicle scale {bus.transform.localScale} to 1:1 world metres.");
        bus.transform.localScale = Vector3.one;
    }

    void EnsureMainCameraForDirectPlay(BusController bus)
    {
        if (!attachMainCameraToBusForDirectPlay || bus == null)
            return;

        var cam = Camera.main;
        if (cam == null)
            cam = FindAnyObjectByType<Camera>();
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

        var behaviours = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include);
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

        // Fix which side this graph's traffic keeps to before any vehicle is placed.
        if (usableGraph != null)
            usableGraph.SyncDrivingSide();

        var vehiclePool = FindAnyObjectByType<VehiclePool>();
        if (vehiclePool != null)
        {
            vehiclePool.roadGraph = usableGraph;
            vehiclePool.allowAutoResolveRoadGraph = usableGraph != null;
            if (usableGraph == null)
                Debug.LogWarning("SceneBootstrap: VehiclePool AIRoadGraph is misaligned with the active city. Traffic graph usage disabled for this direct-play run.");
        }

        var pedestrianSpawner = FindAnyObjectByType<PedestrianSpawner>();
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

            var converter = CoordinateConverter.Instance ?? FindAnyObjectByType<CoordinateConverter>();
            if (converter == null) return null;
            var roadGraph = RoadGraph.BuildFromOverpassWays(response, converter);

            if (roadGraph == null || roadGraph.nodes == null || roadGraph.nodes.Count == 0)
                return null;

            var airGraph = FindAnyObjectByType<AIRoadGraph>();
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

        string preferredJson = RuntimeCityContentStore.ResolveReadPath(city, "roads.json");
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
            int[] laneCounts = sourceNode.edges != null ? new int[sourceNode.edges.Count] : System.Array.Empty<int>();
            float[] laneWidths = sourceNode.edges != null ? new float[sourceNode.edges.Count] : System.Array.Empty<float>();
            bool[] oneWays = sourceNode.edges != null ? new bool[sourceNode.edges.Count] : System.Array.Empty<bool>();
            bool[] sharedLanes = sourceNode.edges != null ? new bool[sourceNode.edges.Count] : System.Array.Empty<bool>();

            if (sourceNode.edges != null)
            {
                for (int edgeIndex = 0; edgeIndex < sourceNode.edges.Count; edgeIndex++)
                {
                    var edge = sourceNode.edges[edgeIndex];
                    nextNodeIndices[edgeIndex] = edge.to;
                    laneCounts[edgeIndex] = Mathf.Max(1, edge.laneCount);
                    laneWidths[edgeIndex] = Mathf.Max(2.6f, edge.laneWidth);
                    oneWays[edgeIndex] = edge.oneWay;
                    sharedLanes[edgeIndex] = edge.sharedLane;
                }
            }

            nodes[i] = new AIRoadGraph.RoadNode
            {
                id = $"N{i:0000}",
                point = pointGo.transform,
                laneCount = laneCounts.Length > 0 ? laneCounts[0] : 1,
                laneWidth = laneWidths.Length > 0 ? laneWidths[0] : 3.1f,
                speedLimitKmh = speedLimit,
                trafficLight = null,
                nextNodeIndices = nextNodeIndices,
                outgoingLaneCounts = laneCounts,
                outgoingLaneWidths = laneWidths,
                outgoingOneWays = oneWays,
                outgoingSharedLanes = sharedLanes
            };
        }

        target.nodes = nodes;
    }

    AIRoadGraph FindUsableRoadGraph(BusRoute route = null)
    {
        var graphs = FindObjectsByType<AIRoadGraph>(FindObjectsInactive.Exclude);
        if (graphs == null || graphs.Length == 0)
            return null;

        var city = GameState.Instance?.selectedCity ?? CityManager.Instance?.activeCity;
        if (city == null)
        {
            for (int i = 0; i < graphs.Length; i++)
            {
                // A depot yard's own lane circuit is private to that depot: binding
                // it as the city graph would put all city traffic on a yard loop.
                if (graphs[i] != null && graphs[i].NodeCount > 0 && !graphs[i].privateLaneCircuit)
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
            if (graph == null || graph.NodeCount == 0 || graph.privateLaneCircuit)
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
