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

#if UNITY_EDITOR
        SeedCityManagerFromAssetsInEditor();
#endif

        SyncSelectionStateFromManagers();
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
        EnsurePedestrianSpawner();
        EnsureEnvironmentSpawner();
        EnsureStopPropSpawner();
        EnsureGasStationSpawner();

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

        ApplyBusScaleFromMap();
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
        if (FindObjectOfType<PedestrianSpawner>() != null)
            return;

        var go = new GameObject("PedestrianSpawner");
        var spawner = go.AddComponent<PedestrianSpawner>();
        spawner.useRandomGeneratedCrossings = true;
        spawner.roadGraph = FindObjectOfType<AIRoadGraph>();
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
        if (FindObjectOfType<StopPropSpawner>() != null)
            return;

        var go = new GameObject("StopPropSpawner");
        go.AddComponent<StopPropSpawner>();
    }

    void EnsureGasStationSpawner()
    {
        if (FindObjectOfType<RuntimeGasStationSpawner>() != null)
            return;

        var go = new GameObject("RuntimeGasStationSpawner");
        go.AddComponent<RuntimeGasStationSpawner>();
    }

    void ApplyBusScaleFromMap()
    {
        var bus = FindObjectOfType<BusController>();
        var map = FindObjectOfType<MapTileLoader>();
        if (bus == null || map == null) return;

        float scale = map.tileWorldSize > 0f ? map.tileWorldSize / 200f : 1f;
        bus.transform.localScale = Vector3.one * Mathf.Max(0.5f, scale);
    }
}
