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
    [SerializeField] bool spawnSupplementalWorldProps = false;
    [SerializeField] bool spawnOnlyFirstStopPropsAtMissionStart = false;
    [SerializeField] bool attachMainCameraToBusForDirectPlay = true;
    [SerializeField] bool disableCinemachineVirtualCamerasForDirectPlay = true;
    [SerializeField] bool disableCinemachineBrainForDirectPlay = true;
    [SerializeField] bool forceClearVisibilityForDirectPlay = true;
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

        var missionToUse = ResolveMissionDataForRoute(routeToUse) ?? fallbackMissionData ?? missionManager.missionData;
        if (missionToUse != null)
            missionManager.missionData = missionToUse;

        if (missionManager.busController == null)
            missionManager.busController = FindObjectOfType<BusController>();

        EnsureVehicleSupportSystems(missionManager.busController);

        EnsureGpsManager();
        DisableCinemachineVirtualCamerasForDirectPlay();
        EnsureMainCameraForDirectPlay(missionManager.busController);
        ConfigureRoadGraphBackedSystems(routeToUse);
        EnsurePedestrianSpawner();
        EnsureStopPropSpawner();
        EnsurePassengerSystems();
        EnsureTarmacApplier();
        EnsureWeatherSystems();
        EnsureRuntimeRoadSystems();
        EnsureBuildingSystems();
        EnsurePoiVisualizer();
        EnsureTrafficViolationSystem();
        EnsureDynamicEventSystem();
        EnsureAiTrafficSystems();

        if (spawnSupplementalWorldProps)
        {
            EnsureEnvironmentSpawner();
            EnsureGasStationSpawner();
        }

        EnsureClearVisibility();
        ApplyBusScaleFromMap();
    }

    void EnsureVehicleSupportSystems(BusController busController)
    {
        if (busController == null)
            return;

        if (busController.GetComponent<FuelSystem>() == null)
            busController.gameObject.AddComponent<FuelSystem>();

        if (busController.GetComponent<MaintenanceSystem>() == null)
            busController.gameObject.AddComponent<MaintenanceSystem>();
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

        var busSpawner = FindObjectOfType<AIBusScheduleSpawner>();
        if (busSpawner == null)
        {
            var go = new GameObject("AIBusScheduleSpawner");
            busSpawner = go.AddComponent<AIBusScheduleSpawner>();
        }

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
        var weatherSystem = FindObjectOfType<WeatherSystem>();
        if (weatherSystem == null)
        {
            var go = new GameObject("WeatherSystem");
            weatherSystem = go.AddComponent<WeatherSystem>();
        }

        if (FindObjectOfType<TimeOfDaySystem>() == null)
        {
            var go = new GameObject("TimeOfDaySystem");
            var timeSystem = go.AddComponent<TimeOfDaySystem>();
            timeSystem.skyController = FindObjectOfType<SkyController>();
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
            if (existing.roadGraph == null)
                existing.useRandomGeneratedCrossings = false;
            return;
        }

        var go = new GameObject("PedestrianSpawner");
        var spawner = go.AddComponent<PedestrianSpawner>();
        spawner.roadGraph = FindUsableRoadGraph();
        spawner.useRandomGeneratedCrossings = spawner.roadGraph != null;
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
        if (FindObjectOfType<OSMBuildingLoader>() == null)
        {
            var loaderGo = new GameObject("OSMBuildingLoader");
            loaderGo.AddComponent<OSMBuildingLoader>();
        }

        var builder = FindObjectOfType<OSMBuildingMeshBuilder>();
        if (builder == null)
        {
            var builderGo = new GameObject("OSMBuildingMeshBuilder");
            builder = builderGo.AddComponent<OSMBuildingMeshBuilder>();
        }

        var mapLoader = FindObjectOfType<MapTileLoader>();
        if (mapLoader != null)
            builder.buildingBaseYOffset = Mathf.Max(mapLoader.tileSurfaceY + 0.03f, 0.2f);
    }

    void EnsurePoiVisualizer()
    {
        if (FindObjectOfType<RuntimeOsmPoiVisualizer>() != null)
            return;

        var go = new GameObject("RuntimeOsmPoiVisualizer");
        go.AddComponent<RuntimeOsmPoiVisualizer>();
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

    AIRoadGraph FindUsableRoadGraph(BusRoute route = null)
    {
        var graph = FindObjectOfType<AIRoadGraph>();
        if (graph == null || graph.NodeCount == 0)
            return null;

        var city = GameState.Instance?.selectedCity ?? CityManager.Instance?.activeCity;
        if (city == null)
            return graph;

        BusStopData referenceStop = null;
        if (route != null && route.stops != null && route.stops.Length > 0)
            referenceStop = route.stops[0];

        Vector3 referenceWorld = referenceStop != null
            ? CoordinateConverter.LocalOriginGeoToWorld(referenceStop.latitude, referenceStop.longitude, city.centreLat, city.centreLon)
            : CoordinateConverter.LocalOriginGeoToWorld(city.spawnLat, city.spawnLon, city.centreLat, city.centreLon);

        int nearest = graph.GetNearestNodeIndex(referenceWorld);
        if (!graph.IsValidNode(nearest))
            return null;

        float distance = Vector3.Distance(referenceWorld, graph.GetNodePosition(nearest));
        return distance <= 500f ? graph : null;
    }
}
