using UnityEngine;
using System.Collections;

public class FreeDriveSession : MonoBehaviour
{
    const float SpawnRaycastHeight = 600f;
    const float SpawnRaycastDistance = 2000f;
    const float SpawnClearanceAboveGround = 1.2f;
    const float SpawnOffsetBehindStartStopMeters = 14f;
    const float WaitForRoadsTimeoutSeconds = 8f;
    const float SpawnRoadSearchRadiusMeters = 45f;

    public static FreeDriveSession Instance { get; private set; }

    [Header("Session State")]
    public bool autoStart = true;
    public bool sessionActive = false;
    public float distanceDrivenKm = 0f;
    public float sessionTimeSeconds = 0f;
    public float fuelLevel = 100f; // percentage

    [Header("Fuel Settings")]
    public float fuelConsumptionPer100km = 35f; // litres per 100km (diesel bus)
    public float fuelCapacityLitres = 300f;
    private float fuelLitres;

    public event System.Action<bool> OnSessionActiveChanged;

    private BusController busController;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        busController = FindAnyObjectByType<BusController>();
        fuelLitres = ResolveInitialFuelLitres();
        fuelLevel = Mathf.Clamp01(fuelLitres / Mathf.Max(1f, fuelCapacityLitres)) * 100f;

        BusRoute selectedRoute = GameState.Instance != null ? GameState.Instance.selectedRoute : null;
        if (selectedRoute != null)
        {
            selectedRoute.EnsureRuntimeData();
            selectedRoute.EnsureWorldPathPoints();
        }

        if (selectedRoute != null && selectedRoute.GetStopCount() >= 2)
            StartCoroutine(BeginSelectedRouteWhenReady(selectedRoute));
        else if (autoStart)
            StartCoroutine(BeginSessionWhenReady());
    }

    void Update()
    {
        if (!sessionActive) return;

        sessionTimeSeconds += Time.deltaTime;

        // Track distance
        if (busController != null)
        {
            float speedKmh = busController.currentSpeedKmh;
            float distanceDelta = (speedKmh / 3600f) * Time.deltaTime;
            distanceDrivenKm += distanceDelta;
            if (busController.GetComponent<FuelSystem>() == null && busController.GetComponent<BatterySystem>() == null)
            {
                fuelLitres = Mathf.Max(0f, fuelLitres - distanceDelta * fuelConsumptionPer100km / 100f);
                fuelLevel = fuelLitres / Mathf.Max(1f, fuelCapacityLitres) * 100f;
                busController.FuelDepleted = fuelLitres <= 0f;
            }
        }
    }

    IEnumerator BeginSessionWhenReady()
    {
        while (busController == null)
        {
            busController = FindAnyObjectByType<BusController>();
            yield return null;
        }

        while (GPSManager.Instance == null || CityManager.Instance == null || CityManager.Instance.activeCity == null)
            yield return null;

        yield return WaitForRoadSurfaceIfAvailable();
        PositionBusAtSessionStart();
        StartSession();
    }

    IEnumerator BeginSelectedRouteWhenReady(BusRoute route)
    {
        while (busController == null)
        {
            busController = FindAnyObjectByType<BusController>();
            yield return null;
        }

        while (MissionManager.Instance == null || GPSManager.Instance == null)
            yield return null;

        MissionManager mission = MissionManager.Instance;
        if (mission.busController == null)
            mission.busController = busController;

        mission.currentRoute = route;
        mission.StartRoute(route);
        Debug.Log($"FreeDriveSession: Selected route '{route.routeName}' detected; starting scheduled service instead of free drive.");
    }

    public void StartSession()
    {
        if (sessionActive) return;
        if (busController == null) busController = FindAnyObjectByType<BusController>();
        MissionManager.Instance?.AbortMissionForFreeDrive();
        PassengerManager.Instance?.ResetForFreeDriveMode();

        sessionActive = true;
        distanceDrivenKm = 0f;
        sessionTimeSeconds = 0f;
        fuelLitres = ResolveInitialFuelLitres();
        fuelLevel = Mathf.Clamp01(fuelLitres / Mathf.Max(1f, fuelCapacityLitres)) * 100f;
        OnSessionActiveChanged?.Invoke(true);
        Debug.Log("Free drive session started!");
    }

    void PositionBusAtSessionStart()
    {
        if (busController == null || GPSManager.Instance == null || CityManager.Instance == null || CityManager.Instance.activeCity == null)
            return;

        var city = CityManager.Instance.activeCity;
        double lat = city.spawnLat;
        double lon = city.spawnLon;

        var selectedRoute = GameState.Instance != null ? GameState.Instance.selectedRoute : null;
        if (selectedRoute != null && selectedRoute.stops != null && selectedRoute.stops.Length > 0)
        {
            double routeLat = selectedRoute.stops[0].latitude;
            double routeLon = selectedRoute.stops[0].longitude;
            if (IsPlausibleRouteStart(routeLat, routeLon))
            {
                lat = routeLat;
                lon = routeLon;
            }
            else
            {
                Debug.LogWarning($"FreeDriveSession: Route spawn has invalid coordinates. Falling back to city spawn for {city.cityName}.");
            }
        }

        Vector3 startPos = GPSManager.Instance.GpsToWorld(lat, lon);
        startPos = ApplySpawnOffsetFromRouteStart(startPos, selectedRoute);
        Vector3 heading = busController.transform.forward;
        if (selectedRoute != null && selectedRoute.stops != null && selectedRoute.stops.Length > 1)
            heading = GPSManager.Instance.GpsToWorld(selectedRoute.stops[1].latitude, selectedRoute.stops[1].longitude) - startPos;
        heading = RoadSpawnResolver.RouteHeading(selectedRoute, startPos, heading);
        if (selectedRoute != null && BusDepotManager.EnsureExists().TrySpawnDepotAndResolveBay(selectedRoute, startPos, busController, out var depotBayPos, out var depotBayRot, out var depotExitPos))
        {
            busController.Teleport(depotBayPos, depotBayRot);
            busController.SetParkingBrake(true);
        }
        else if (RoadSpawnResolver.TryResolve(busController, startPos, heading, SpawnRoadSearchRadiusMeters, out var roadPosition, out var roadRotation))
            busController.Teleport(roadPosition, roadRotation);
        else
        {
            startPos.y = ResolveSpawnHeight(startPos);
            busController.Teleport(startPos, busController.transform.rotation);
        }

        var rb = busController.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    static bool IsPlausibleRouteStart(double lat, double lon)
    {
        if (double.IsNaN(lat) || double.IsNaN(lon) ||
            double.IsInfinity(lat) || double.IsInfinity(lon))
            return false;
        if (System.Math.Abs(lat) < 0.000001d && System.Math.Abs(lon) < 0.000001d)
            return false;
        return System.Math.Abs(lat) <= 90d && System.Math.Abs(lon) <= 180d;
    }

    float ResolveSpawnHeight(Vector3 spawnWorldPos)
    {
        Vector3 castStart = spawnWorldPos + Vector3.up * SpawnRaycastHeight;
        if (Physics.Raycast(castStart, Vector3.down, out RaycastHit hit, SpawnRaycastDistance, ~0, QueryTriggerInteraction.Ignore))
            return hit.point.y + SpawnClearanceAboveGround;

        var mapLoader = FindAnyObjectByType<MapTileLoader>();
        if (mapLoader != null)
            return mapLoader.tileSurfaceY + SpawnClearanceAboveGround;

        return busController.transform.position.y > 0.1f ? busController.transform.position.y : 1f;
    }

    IEnumerator WaitForRoadSurfaceIfAvailable()
    {
        float deadline = Time.time + WaitForRoadsTimeoutSeconds;
        while (Time.time < deadline)
        {
            var roads = OSMRoadMeshBuilder.Instance;
            if (roads != null && roads.roadsBuilt)
                yield break;
            yield return null;
        }
    }

    Vector3 ApplySpawnOffsetFromRouteStart(Vector3 startPos, BusRoute route)
    {
        if (route == null || route.stops == null || route.stops.Length == 0 || GPSManager.Instance == null)
            return startPos;

        Vector3 forward = busController != null ? busController.transform.forward : Vector3.forward;
        if (route.stops.Length > 1)
        {
            var secondStopWorld = GPSManager.Instance.GpsToWorld(
                route.stops[1].latitude,
                route.stops[1].longitude);
            var routeDirection = new Vector3(
                secondStopWorld.x - startPos.x,
                0f,
                secondStopWorld.z - startPos.z);
            if (routeDirection.sqrMagnitude > 1f)
                forward = routeDirection.normalized;
        }

        forward = RoadSpawnResolver.RouteHeading(route, startPos, forward).normalized;
        return startPos - forward * SpawnOffsetBehindStartStopMeters;
    }

    public void EndSession()
    {
        sessionActive = false;
        OnSessionActiveChanged?.Invoke(false);
        Debug.Log($"Session ended — Distance: {distanceDrivenKm:F2} km, Time: {sessionTimeSeconds:F0}s");
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(sessionTimeSeconds / 60f);
        int seconds = Mathf.FloorToInt(sessionTimeSeconds % 60f);
        return $"{minutes:00}:{seconds:00}";
    }

    public string GetCurrentGPSString()
    {
        if (busController == null || GPSManager.Instance == null) return "0.0000, 0.0000";
        var (lat, lon) = GPSManager.Instance.WorldToGps(busController.transform.position);
        return $"{lat:F4}, {lon:F4}";
    }

    float ResolveInitialFuelLitres()
    {
        if (GameState.Instance != null && GameState.Instance.vehicleState != null)
        {
            fuelCapacityLitres = Mathf.Max(1f, GameState.Instance.vehicleState.fuelCapacityLitres);
            return Mathf.Clamp(GameState.Instance.vehicleState.fuelLitres, 0f, fuelCapacityLitres);
        }

        return fuelCapacityLitres;
    }
}
