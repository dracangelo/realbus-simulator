using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

/// <summary>
/// Runtime component that fetches OSM bus route data and converts it into
/// <see cref="BusRoute"/> ScriptableObjects (section 3.7 / 3.7A).
///
/// Pipeline (section 3.7A):
///   1. Fetch roads + routes from Overpass.
///   2. Build <see cref="RoadGraph"/> from road ways.
///   3. Parse raw routes via <see cref="BusRouteParser"/>.
///   4. Snap each stop to the nearest road-graph segment.
///   5. Group stops into corridor clusters (200–400 m proximity).
///   6. Connect ordered stops with A* on the RoadGraph.
///   7. Apply route filter (6–40 stops, 3–25 km).
///   8. Classify difficulty; generate return routes.
///   9. Emit <see cref="BusRoute"/> ScriptableObject instances.
///
/// In Editor builds the Editor tool (RouteEditorWindow) can also call
/// <see cref="ImportForCity"/> directly and save assets to disk.
/// </summary>
public class OSMRouteImporter : MonoBehaviour
{
    public static OSMRouteImporter Instance { get; private set; }

    // ── Inspector ──────────────────────────────────────────────────────

    [Header("Settings")]
    [Tooltip("Automatically fetch routes when a city is loaded.")]
    public TextAsset localRoadsJson;
    public TextAsset localRoutesJson;
    public bool autoImportOnStart = false;
    [Tooltip("Include synthetic return routes in output.")]
    public bool generateReturnRoutes = true;
    [Tooltip("Apply stop-count / length filter from spec 3.7A.")]
    public bool applyRouteFilter = true;

    [Header("Overpass")]
    public string overpassEndpoint = "https://overpass-api.de/api/interpreter";
    [Tooltip("Timeout for road graph query (seconds).")]
    [Range(30, 300)] public int roadsTimeoutSec   = 120;
    [Tooltip("Timeout for bus routes query (seconds).")]
    [Range(30, 300)] public int routesTimeoutSec  = 120;

    [Header("Stop Snapping (3.7A)")]
    [Tooltip("Maximum distance a stop may be from its snapped road position.")]
    [Range(10f, 200f)] public float maxSnapDistanceMeters = 60f;

    [Header("State (read-only)")]
    public bool  importInProgress;
    public bool  importComplete;
    public int   routesFound;
    public int   routesAccepted;
    public string statusMessage = "Idle";

    [Header("Output")]
    public List<BusRoute> importedRoutes = new();

    // ── Events ─────────────────────────────────────────────────────────

    public event System.Action<string>         OnStatusChanged;
    public event System.Action<List<BusRoute>> OnImportComplete;

    // ── Private ────────────────────────────────────────────────────────

    CoordinateConverter _converter;

    // ── Lifecycle ──────────────────────────────────────────────────────

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        _converter = CoordinateConverter.Instance ?? FindFirstObjectByType<CoordinateConverter>();
        if (autoImportOnStart)
            StartCoroutine(WaitForCityThenImport());
    }

    // ── Public API ─────────────────────────────────────────────────────

    public void TriggerImport() =>
        StartCoroutine(ImportRoutesForCity(CityManager.Instance?.activeCity));

    public void TriggerImport(CityDefinition city)
    {
        if (city != null)
            CityManager.Instance?.SetActiveCity(city);
        StartCoroutine(ImportRoutesForCity(city ?? CityManager.Instance?.activeCity));
    }

    // ── Main coroutine ─────────────────────────────────────────────────

    IEnumerator WaitForCityThenImport()
    {
        while (CityManager.Instance == null || CityManager.Instance.activeCity == null)
            yield return null;
        yield return ImportRoutesForCity(CityManager.Instance.activeCity);
    }

    IEnumerator ImportRoutesForCity(CityDefinition city)
    {
        if (city == null)
        {
            SetStatus("No active city — import aborted.");
            yield break;
        }

        if (importInProgress)
        {
            Debug.LogWarning("[OSMRouteImporter] Import already in progress.");
            yield break;
        }

        _converter = CoordinateConverter.Instance ?? FindFirstObjectByType<CoordinateConverter>();
        importInProgress = true;
        importComplete   = false;
        importedRoutes.Clear();
        try
        {

        // ── Step 1: Fetch road graph data ─────────────────────────────
        SetStatus($"Fetching road network for {city.cityName}…");
        string roadsQuery = OverpassQueryBuilder.RoadsQuery(
            city.minLat, city.minLon, city.maxLat, city.maxLon, roadsTimeoutSec);

        string roadsJson = localRoadsJson != null ? localRoadsJson.text : null;
        if (roadsJson == null) yield return FetchOverpass(roadsQuery, json => roadsJson = json);

        if (string.IsNullOrEmpty(roadsJson))
        {
            SetStatus("Road network fetch failed.");
            importInProgress = false;
            yield break;
        }

        // ── Step 2: Build road graph ──────────────────────────────────
        SetStatus("Building road graph…");
        yield return null; // breathe

        var roadsResponse = OverpassResponse.Deserialize(roadsJson);
        roadsResponse.Resolve();
        var roadGraph = RoadGraph.BuildFromOverpassWays(roadsResponse, _converter);
        Debug.Log($"[OSMRouteImporter] Road graph: {roadGraph.nodes.Count} nodes.");

        // ── Step 3: Fetch bus routes ──────────────────────────────────
        SetStatus("Fetching bus routes…");
        string routesQuery = OverpassQueryBuilder.BusRoutesQuery(
            city.minLat, city.minLon, city.maxLat, city.maxLon, routesTimeoutSec);

        string routesJson = localRoutesJson != null ? localRoutesJson.text : null;
        if (routesJson == null) yield return FetchOverpass(routesQuery, json => routesJson = json);

        if (string.IsNullOrEmpty(routesJson))
        {
            SetStatus("Bus routes fetch failed.");
            importInProgress = false;
            yield break;
        }

        // ── Step 4: Parse routes ──────────────────────────────────────
        SetStatus("Parsing routes…");
        yield return null;

        var routesResponse = OverpassResponse.Deserialize(routesJson);
        routesResponse.Resolve();
        var parsedRoutes = BusRouteParser.ParseRoutes(routesResponse);
        routesFound = parsedRoutes.Count;
        Debug.Log($"[OSMRouteImporter] Parsed {routesFound} raw routes.");

        // Validate connectivity before filtering on the actual drivable distance.
        var accepted = new List<BusRouteParser.ParsedBusRoute>();
        foreach (var route in parsedRoutes)
        {
            if (RoadRoutePathBuilder.TryBuild(route, roadGraph, _converter, maxSnapDistanceMeters))
                accepted.Add(route);
            yield return null;
        }
        parsedRoutes = applyRouteFilter ? BusRouteParser.FilterRoutes(accepted) : accepted;
        if (generateReturnRoutes)
        {
            var returns = BusRouteParser.GenerateReturnRoutes(parsedRoutes);
            accepted = new List<BusRouteParser.ParsedBusRoute>();
            foreach (var route in returns)
            {
                if (RoadRoutePathBuilder.TryBuild(route, roadGraph, _converter, maxSnapDistanceMeters))
                    accepted.Add(route);
                yield return null;
            }
            parsedRoutes.AddRange(applyRouteFilter ? BusRouteParser.FilterRoutes(accepted) : accepted);
        }

        // ── Step 8: Convert to BusRoute ScriptableObjects ─────────────
        SetStatus("Creating route assets…");
        yield return null;

        foreach (var pr in parsedRoutes)
        {
            var route = CreateBusRouteAsset(pr);
            route.sourceCityCode = city.cityCode;
            importedRoutes.Add(route);
        }

        routesAccepted = importedRoutes.Count;

        // ── Step 9: Register with city ────────────────────────────────
        MergeIntoCity(city);

        importInProgress = false;
        importComplete   = true;
        SetStatus($"Done — {routesAccepted} routes imported.");
        Debug.Log($"[OSMRouteImporter] Complete: {routesAccepted} routes for {city.cityName}.");
        OnImportComplete?.Invoke(importedRoutes);
        }
        finally { importInProgress = false; }
    }

    public BusRoute CreateBusRouteAsset(BusRouteParser.ParsedBusRoute parsed)
    {
        return CreateRouteAsset(parsed, _converter);
    }

    public static BusRoute CreateRouteAsset(BusRouteParser.ParsedBusRoute parsed, CoordinateConverter converter)
    {
        var route = ScriptableObject.CreateInstance<BusRoute>();
        route.name = SanitiseName($"Route_{parsed.routeRef}_{parsed.destinationName}");
        route.generatedRouteId = parsed.osmRelationId > 0 ? "osm:" + parsed.osmRelationId + (parsed.isReturn ? ":return" : "") : parsed.routeRef;
        route.osmRelationId = parsed.osmRelationId;
        route.isRoadPathValidated = parsed.isRoadPathValidated;
        route.routeNumber = parsed.routeRef;
        route.routeName = parsed.routeName;
        route.busStops = parsed.stops.ToArray();
        route.SyncLegacyStopsFromBusStops();
        route.distanceKm = parsed.lengthKm;
        route.estimatedTimeMinutes = parsed.estimatedMinutes;
        route.difficulty = parsed.difficulty;
        route.geometryLatLonFlat = new double[parsed.geometry.Count * 2];
        route.pathPoints = new Vector3[parsed.geometry.Count];
        for (int i = 0; i < parsed.geometry.Count; i++)
        {
            var point = parsed.geometry[i];
            route.geometryLatLonFlat[i * 2] = point.lat;
            route.geometryLatLonFlat[i * 2 + 1] = point.lon;
            if (converter != null) route.pathPoints[i] = converter.GeoToWorldPosition(point.lat, point.lon);
        }
        return route;
    }

    void MergeIntoCity(CityDefinition city)
    {
        if (city == null || importedRoutes.Count == 0) return;

        var merged = new Dictionary<string, BusRoute>();
        foreach (var route in city.availableRoutes ?? System.Array.Empty<BusRoute>())
            if (route != null) merged[route.GetProgressionId(city.cityCode)] = route;
        foreach (var route in importedRoutes) merged[route.GetProgressionId(city.cityCode)] = route;
        city.availableRoutes = new List<BusRoute>(merged.Values).ToArray();
    }

    // ── Overpass HTTP helper ───────────────────────────────────────────

    IEnumerator FetchOverpass(string query, System.Action<string> onDone)
    {
        string body = "data=" + UnityWebRequest.EscapeURL(query);
        byte[] raw  = System.Text.Encoding.UTF8.GetBytes(body);

        using var req = new UnityWebRequest(overpassEndpoint, "POST");
        req.uploadHandler   = new UploadHandlerRaw(raw);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        req.timeout         = Mathf.Max(roadsTimeoutSec, routesTimeoutSec) + 10;

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"[OSMRouteImporter] Overpass request failed: {req.error}");
            onDone?.Invoke(null);
            yield break;
        }

        string json = req.downloadHandler.text;
        Debug.Log($"[OSMRouteImporter] Overpass response: {json.Length:N0} bytes.");
        onDone?.Invoke(json);
    }

    // ── Helpers ────────────────────────────────────────────────────────

    void SetStatus(string msg)
    {
        statusMessage = msg;
        OnStatusChanged?.Invoke(msg);
        Debug.Log($"[OSMRouteImporter] {msg}");
    }

    static string SanitiseName(string raw)
    {
        var sb = new System.Text.StringBuilder(raw.Length);
        foreach (char c in raw)
            sb.Append(char.IsLetterOrDigit(c) || c == '_' ? c : '_');
        return sb.ToString();
    }
}
