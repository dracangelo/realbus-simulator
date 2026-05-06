using UnityEngine;
using System.Collections;

public class FreeDriveSession : MonoBehaviour
{
    const float MaxSpawnDistanceFromMapCenterMeters = 3500f;
    const float SpawnRaycastHeight = 600f;
    const float SpawnRaycastDistance = 2000f;
    const float SpawnClearanceAboveGround = 1.2f;
    const float SpawnOffsetBehindStartStopMeters = 14f;
    const float SpawnOffsetSideMeters = 5f;
    const float WaitForRoadsTimeoutSeconds = 8f;
    const float SpawnRoadSearchRadiusMeters = 45f;

    public static FreeDriveSession Instance { get; private set; }

    [Header("Session State")]
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
        busController = FindFirstObjectByType<BusController>();
        fuelLitres = ResolveInitialFuelLitres();
        fuelLevel = Mathf.Clamp01(fuelLitres / Mathf.Max(1f, fuelCapacityLitres)) * 100f;
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
        }
    }

    IEnumerator BeginSessionWhenReady()
    {
        while (busController == null)
        {
            busController = FindFirstObjectByType<BusController>();
            yield return null;
        }

        while (GPSManager.Instance == null || CityManager.Instance == null || CityManager.Instance.activeCity == null)
            yield return null;

        yield return WaitForRoadSurfaceIfAvailable();
        PositionBusAtSessionStart();
        StartSession();
    }

    public void StartSession()
    {
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
            if (IsSpawnNearLoadedMap(routeLat, routeLon))
            {
                lat = routeLat;
                lon = routeLon;
            }
            else
            {
                Debug.LogWarning($"FreeDriveSession: Route spawn is outside loaded map. Falling back to city spawn for {city.cityName}.");
            }
        }

        Vector3 startPos = GPSManager.Instance.GpsToWorld(lat, lon);
        startPos = ApplySpawnOffsetFromRouteStart(startPos, selectedRoute);
        startPos = SnapSpawnToNearestRoad(startPos);
        startPos.y = ResolveSpawnHeight(startPos);
        busController.transform.position = startPos;

        var rb = busController.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    bool IsSpawnNearLoadedMap(double lat, double lon)
    {
        var world = GPSManager.Instance.GpsToWorld(lat, lon);
        world.y = 0f;
        return world.sqrMagnitude <= MaxSpawnDistanceFromMapCenterMeters * MaxSpawnDistanceFromMapCenterMeters;
    }

    float ResolveSpawnHeight(Vector3 spawnWorldPos)
    {
        Vector3 castStart = spawnWorldPos + Vector3.up * SpawnRaycastHeight;
        if (Physics.Raycast(castStart, Vector3.down, out RaycastHit hit, SpawnRaycastDistance, ~0, QueryTriggerInteraction.Ignore))
            return hit.point.y + SpawnClearanceAboveGround;

        var mapLoader = FindFirstObjectByType<MapTileLoader>();
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

    Vector3 SnapSpawnToNearestRoad(Vector3 spawnPos)
    {
        var roads = OSMRoadMeshBuilder.Instance;
        if (roads == null || !roads.roadsBuilt)
            return spawnPos;

        Vector3 probe = spawnPos + Vector3.up * 2f;
        Collider[] nearby = Physics.OverlapSphere(probe, SpawnRoadSearchRadiusMeters, ~0, QueryTriggerInteraction.Ignore);
        if (nearby == null || nearby.Length == 0)
            return spawnPos;

        Vector3 best = spawnPos;
        float bestSqr = float.MaxValue;

        for (int i = 0; i < nearby.Length; i++)
        {
            var col = nearby[i];
            if (col == null) continue;
            if (!IsRoadCollider(col)) continue;

            Vector3 p = col.ClosestPoint(probe);
            Vector2 delta = new Vector2(p.x - spawnPos.x, p.z - spawnPos.z);
            float sqr = delta.sqrMagnitude;
            if (sqr < bestSqr)
            {
                bestSqr = sqr;
                best = new Vector3(p.x, spawnPos.y, p.z);
            }
        }

        return bestSqr < float.MaxValue ? best : spawnPos;
    }

    bool IsRoadCollider(Collider col)
    {
        if (col == null) return false;
        var t = col.transform;
        if (t.name.StartsWith("Road_")) return true;
        if (t.parent != null && t.parent.name == "OSM_Roads") return true;
        if (t.root != null && t.root.name == "OSM_Roads") return true;
        return false;
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

        var right = Vector3.Cross(Vector3.up, forward).normalized;
        var offset = (-forward * SpawnOffsetBehindStartStopMeters) + (right * SpawnOffsetSideMeters);
        return startPos + offset;
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
