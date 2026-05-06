using System.Collections.Generic;
using UnityEngine;

public class UnlockManager : MonoBehaviour
{
    public const string RouteCompletionPrefsPrefix = "progress.route_complete";

    public static UnlockManager Instance { get; private set; }

    [SerializeField] string[] featuredCityCodes = { "NBO", "MBA", "KSM", "NKR", "KLA" };
    [SerializeField] int[] featuredCityRankRequirements = { 1, 3, 5, 6, 8 };

    readonly Dictionary<string, BusRoute> grandTourCache = new Dictionary<string, BusRoute>();
    readonly List<CityDefinition> featuredCities = new List<CityDefinition>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap()
    {
        EnsureExists();
    }

    public static UnlockManager EnsureExists()
    {
        if (Instance != null)
            return Instance;

        var existing = FindFirstObjectByType<UnlockManager>();
        if (existing != null)
            return existing;

        var root = new GameObject("UnlockManager");
        return root.AddComponent<UnlockManager>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        XPSystem.EnsureExists();
    }

    public IReadOnlyList<CityDefinition> GetFeaturedCities()
    {
        RefreshFeaturedCities();
        return featuredCities;
    }

    public int GetRequiredRank(CityDefinition city)
    {
        if (city == null)
            return int.MaxValue;

        RefreshFeaturedCities();

        for (int i = 0; i < featuredCities.Count; i++)
        {
            if (featuredCities[i] == city || CodesMatch(featuredCities[i], city.cityCode))
                return GetRequiredRankByIndex(i);
        }

        return 1;
    }

    public bool IsCityUnlocked(CityDefinition city)
    {
        if (city == null)
            return false;

        int currentRank = XPSystem.Instance != null
            ? XPSystem.Instance.GetProgressSnapshot().currentRank
            : 1;

        return currentRank >= GetRequiredRank(city);
    }

    public string GetCityLockTooltip(CityDefinition city)
    {
        if (city == null)
            return string.Empty;

        int requiredRank = GetRequiredRank(city);
        return IsCityUnlocked(city)
            ? "Unlocked"
            : $"Unlock at Rank {requiredRank}";
    }

    public List<BusRoute> GetRoutesForCity(CityDefinition city)
    {
        var routes = GetBaseRoutesForCity(city);
        if (city == null)
            return routes;

        if (IsGrandTourUnlocked(city))
        {
            var grandTour = GetGrandTourRoute(city);
            if (grandTour != null)
                routes.Add(grandTour);
        }

        return routes;
    }

    public bool IsRouteCompleted(BusRoute route, CityDefinition city = null)
    {
        if (route == null)
            return false;

        var resolvedCity = ResolveCityForRoute(route, city);
        if (resolvedCity == null)
            return false;

        return PlayerPrefs.GetInt(BuildRouteCompletionKey(resolvedCity, route), 0) == 1;
    }

    public void RecordRouteCompletion(BusRoute route, CityDefinition city = null)
    {
        if (route == null || route.isGrandTourRoute)
            return;

        var resolvedCity = ResolveCityForRoute(route, city);
        if (resolvedCity == null)
            return;

        PlayerPrefs.SetInt(BuildRouteCompletionKey(resolvedCity, route), 1);
        PlayerPrefs.Save();
        grandTourCache.Remove(resolvedCity.cityCode);
    }

    public string[] GetUnlockedCityCodes()
    {
        RefreshFeaturedCities();

        var unlocked = new List<string>();
        for (int i = 0; i < featuredCities.Count; i++)
        {
            var city = featuredCities[i];
            if (city != null && IsCityUnlocked(city) && !string.IsNullOrWhiteSpace(city.cityCode))
                unlocked.Add(city.cityCode);
        }

        return unlocked.ToArray();
    }

    public string[] GetCompletedRouteKeys()
    {
        var completed = new List<string>();
        var cityManager = CityManager.Instance;
        if (cityManager?.allCities == null)
            return completed.ToArray();

        for (int i = 0; i < cityManager.allCities.Length; i++)
        {
            var city = cityManager.allCities[i];
            var routes = GetBaseRoutesForCity(city);
            for (int j = 0; j < routes.Count; j++)
            {
                var route = routes[j];
                if (route != null && IsRouteCompleted(route, city))
                    completed.Add(BuildRouteCompletionKey(city, route));
            }
        }

        return completed.ToArray();
    }

    public void ApplyCompletedRouteKeys(IEnumerable<string> completionKeys)
    {
        grandTourCache.Clear();
        ClearStoredRouteCompletions();

        if (completionKeys == null)
        {
            PlayerPrefs.Save();
            return;
        }

        foreach (string key in completionKeys)
        {
            if (!string.IsNullOrWhiteSpace(key))
                PlayerPrefs.SetInt(key, 1);
        }

        PlayerPrefs.Save();
    }

    public float GetCityCompletion01(CityDefinition city)
    {
        var routes = GetBaseRoutesForCity(city);
        if (routes.Count == 0)
            return 0f;

        int completedCount = 0;
        for (int i = 0; i < routes.Count; i++)
        {
            if (IsRouteCompleted(routes[i], city))
                completedCount++;
        }

        return (float)completedCount / routes.Count;
    }

    public bool IsGrandTourUnlocked(CityDefinition city)
    {
        var routes = GetBaseRoutesForCity(city);
        return routes.Count > 0 && GetCityCompletion01(city) >= 0.999f;
    }

    public BusRoute GetGrandTourRoute(CityDefinition city)
    {
        if (city == null || !IsGrandTourUnlocked(city))
            return null;

        if (grandTourCache.TryGetValue(city.cityCode, out var cachedRoute) && cachedRoute != null)
            return cachedRoute;

        var createdRoute = BuildGrandTourRoute(city);
        if (createdRoute != null)
            grandTourCache[city.cityCode] = createdRoute;

        return createdRoute;
    }

    void RefreshFeaturedCities()
    {
        featuredCities.Clear();

        var cityManager = CityManager.Instance;
        if (cityManager == null || cityManager.allCities == null)
            return;

        for (int i = 0; i < featuredCityCodes.Length; i++)
        {
            var city = cityManager.GetCityByCode(featuredCityCodes[i]);
            if (city != null && !featuredCities.Contains(city))
                featuredCities.Add(city);
        }

        for (int i = 0; i < cityManager.allCities.Length && featuredCities.Count < featuredCityRankRequirements.Length; i++)
        {
            var city = cityManager.allCities[i];
            if (city != null && !featuredCities.Contains(city))
                featuredCities.Add(city);
        }
    }

    int GetRequiredRankByIndex(int index)
    {
        if (featuredCityRankRequirements == null || featuredCityRankRequirements.Length == 0)
            return 1;

        index = Mathf.Clamp(index, 0, featuredCityRankRequirements.Length - 1);
        return Mathf.Max(1, featuredCityRankRequirements[index]);
    }

    CityDefinition ResolveCityForRoute(BusRoute route, CityDefinition explicitCity)
    {
        if (explicitCity != null)
            return explicitCity;

        var cityManager = CityManager.Instance;
        if (cityManager?.allCities == null)
            return null;

        for (int i = 0; i < cityManager.allCities.Length; i++)
        {
            var city = cityManager.allCities[i];
            if (city == null || city.availableRoutes == null)
                continue;

            for (int j = 0; j < city.availableRoutes.Length; j++)
            {
                if (city.availableRoutes[j] == route)
                    return city;
            }
        }

        return null;
    }

    List<BusRoute> GetBaseRoutesForCity(CityDefinition city)
    {
        var routes = new List<BusRoute>();
        if (city == null || city.availableRoutes == null)
            return routes;

        var seen = new HashSet<string>();
        for (int i = 0; i < city.availableRoutes.Length; i++)
        {
            var route = city.availableRoutes[i];
            if (route == null || route.isGrandTourRoute)
                continue;

            route.sourceCityCode = city.cityCode;
            string routeId = route.GetProgressionId(city.cityCode);
            if (seen.Add(routeId))
                routes.Add(route);
        }

        return routes;
    }

    BusRoute BuildGrandTourRoute(CityDefinition city)
    {
        var baseRoutes = GetBaseRoutesForCity(city);
        if (baseRoutes.Count == 0)
            return null;

        var combinedStops = new List<BusStopData>();
        var combinedPath = new List<Vector3>();
        var combinedGeometry = new List<double>();
        float totalDistance = 0f;
        float totalTime = 0f;
        float totalFare = 0f;
        float totalDifficulty = 0f;
        int maxDifficulty = 1;

        for (int i = 0; i < baseRoutes.Count; i++)
        {
            var route = baseRoutes[i];
            totalDistance += Mathf.Max(0f, route.distanceKm);
            totalTime += Mathf.Max(0f, route.estimatedTimeMinutes);
            totalFare += Mathf.Max(0f, route.baseFare);
            totalDifficulty += Mathf.Clamp(route.difficulty, 1, 5);
            maxDifficulty = Mathf.Max(maxDifficulty, Mathf.Clamp(route.difficulty, 1, 5));

            AppendStops(route, combinedStops);
            AppendPath(route, combinedPath);
            AppendGeometry(route, combinedGeometry);
        }

        if (combinedStops.Count == 0)
            return null;

        var grandTour = ScriptableObject.CreateInstance<BusRoute>();
        grandTour.name = $"{city.cityCode}_GrandTour";
        grandTour.hideFlags = HideFlags.DontUnloadUnusedAsset;
        grandTour.routeNumber = "GT";
        grandTour.routeName = $"{city.cityName} Grand Tour";
        grandTour.baseFare = totalFare / Mathf.Max(1, baseRoutes.Count);
        grandTour.stops = combinedStops.ToArray();
        grandTour.pathPoints = combinedPath.Count > 1 ? combinedPath.ToArray() : null;
        grandTour.geometryLatLonFlat = combinedGeometry.Count > 1 ? combinedGeometry.ToArray() : null;
        grandTour.distanceKm = totalDistance;
        grandTour.estimatedTimeMinutes = totalTime;
        grandTour.difficulty = Mathf.Clamp(
            Mathf.RoundToInt((totalDifficulty / Mathf.Max(1, baseRoutes.Count) + maxDifficulty) * 0.5f),
            1, 5);
        grandTour.isGrandTourRoute = true;
        grandTour.generatedRouteId = $"{city.cityCode}_GRAND_TOUR";
        grandTour.sourceCityCode = city.cityCode;
        return grandTour;
    }

    void AppendStops(BusRoute route, List<BusStopData> target)
    {
        if (route.stops == null)
            return;

        for (int i = 0; i < route.stops.Length; i++)
        {
            var stop = route.stops[i];
            if (stop == null)
                continue;

            if (target.Count > 0)
            {
                var previous = target[target.Count - 1];
                if (previous != null &&
                    previous.stopName == stop.stopName &&
                    Mathf.Abs((float)(previous.latitude - stop.latitude)) < 0.00001f &&
                    Mathf.Abs((float)(previous.longitude - stop.longitude)) < 0.00001f)
                {
                    continue;
                }
            }

            target.Add(new BusStopData
            {
                stopName = stop.stopName,
                latitude = stop.latitude,
                longitude = stop.longitude,
                waitTimeSeconds = stop.waitTimeSeconds
            });
        }
    }

    void AppendPath(BusRoute route, List<Vector3> target)
    {
        if (route.pathPoints == null || route.pathPoints.Length == 0)
            return;

        for (int i = 0; i < route.pathPoints.Length; i++)
        {
            if (target.Count > 0 && target[target.Count - 1] == route.pathPoints[i])
                continue;

            target.Add(route.pathPoints[i]);
        }
    }

    void AppendGeometry(BusRoute route, List<double> target)
    {
        if (route.geometryLatLonFlat == null || route.geometryLatLonFlat.Length == 0)
            return;

        for (int i = 0; i < route.geometryLatLonFlat.Length; i += 2)
        {
            if (i + 1 >= route.geometryLatLonFlat.Length)
                break;

            double lat = route.geometryLatLonFlat[i];
            double lon = route.geometryLatLonFlat[i + 1];

            if (target.Count >= 2)
            {
                double previousLat = target[target.Count - 2];
                double previousLon = target[target.Count - 1];
                if (Mathf.Abs((float)(previousLat - lat)) < 0.00001f &&
                    Mathf.Abs((float)(previousLon - lon)) < 0.00001f)
                {
                    continue;
                }
            }

            target.Add(lat);
            target.Add(lon);
        }
    }

    string BuildRouteCompletionKey(CityDefinition city, BusRoute route)
    {
        return $"{RouteCompletionPrefsPrefix}.{city.cityCode}.{route.GetProgressionId(city.cityCode)}";
    }

    void ClearStoredRouteCompletions()
    {
        var cityManager = CityManager.Instance;
        if (cityManager?.allCities == null)
            return;

        for (int i = 0; i < cityManager.allCities.Length; i++)
        {
            var city = cityManager.allCities[i];
            var routes = GetBaseRoutesForCity(city);
            for (int j = 0; j < routes.Count; j++)
            {
                var route = routes[j];
                if (route == null)
                    continue;

                PlayerPrefs.DeleteKey(BuildRouteCompletionKey(city, route));
            }
        }
    }

    bool CodesMatch(CityDefinition city, string cityCode)
    {
        return city != null &&
               !string.IsNullOrWhiteSpace(cityCode) &&
               string.Equals(city.cityCode, cityCode, System.StringComparison.OrdinalIgnoreCase);
    }
}
