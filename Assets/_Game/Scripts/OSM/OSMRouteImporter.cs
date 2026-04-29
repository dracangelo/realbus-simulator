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

        importInProgress = true;
        importComplete   = false;
        importedRoutes.Clear();

        // ── Step 1: Fetch road graph data ─────────────────────────────
        SetStatus($"Fetching road network for {city.cityName}…");
        string roadsQuery = OverpassQueryBuilder.RoadsQuery(
            city.minLat, city.minLon, city.maxLat, city.maxLon, roadsTimeoutSec);

        string roadsJson = null;
        yield return FetchOverpass(roadsQuery, json => roadsJson = json);

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

        string routesJson = null;
        yield return FetchOverpass(routesQuery, json => routesJson = json);

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

        // ── Step 5: Filter ────────────────────────────────────────────
        if (applyRouteFilter)
            parsedRoutes = BusRouteParser.FilterRoutes(parsedRoutes);

        // ── Step 6: Stop snap + A* path per route ─────────────────────
        SetStatus($"Processing {parsedRoutes.Count} routes…");
        int processed = 0;

        foreach (var pr in parsedRoutes)
        {
            SnapStopsToRoadGraph(pr, roadGraph);
            RefineGeometryWithAStar(pr, roadGraph);
            processed++;
            if (processed % 5 == 0)
            {
                SetStatus($"Processing routes… {processed}/{parsedRoutes.Count}");
                yield return null;
            }
        }

        // ── Step 7: Generate return routes ────────────────────────────
        if (generateReturnRoutes)
        {
            var returns = BusRouteParser.GenerateReturnRoutes(parsedRoutes);
            parsedRoutes.AddRange(returns);
        }

        // ── Step 8: Convert to BusRoute ScriptableObjects ─────────────
        SetStatus("Creating route assets…");
        yield return null;

        foreach (var pr in parsedRoutes)
        {
            var route = CreateBusRouteAsset(pr);
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

    // ── Section 3.7A: Stop snapping ───────────────────────────────────

    void SnapStopsToRoadGraph(BusRouteParser.ParsedBusRoute route, RoadGraph graph)
    {
        if (graph.nodes.Count < 2 || _converter == null) return;

        foreach (var stop in route.stops)
        {
            Vector3 stopWorld = _converter.GeoToWorldPosition(stop.latitude, stop.longitude);

            if (!graph.TryProjectToNearestSegment(
                    stopWorld,
                    out Vector3 snapped,
                    out _, out _,
                    out float dist))
                continue;

            if (dist > maxSnapDistanceMeters) continue;

            var (lat, lon) = _converter.WorldToGeoPosition(snapped);
            stop.latitude  = lat;
            stop.longitude = lon;
        }
    }

    // ── Section 3.7A: A* geometry refinement ─────────────────────────

    void RefineGeometryWithAStar(BusRouteParser.ParsedBusRoute route, RoadGraph graph)
    {
        if (route.stops.Count < 2 || graph.nodes.Count < 2 || _converter == null) return;

        var refined = new List<(double lat, double lon)>();

        for (int i = 0; i < route.stops.Count - 1; i++)
        {
            Vector3 aWorld = _converter.GeoToWorldPosition(
                route.stops[i].latitude, route.stops[i].longitude);
            Vector3 bWorld = _converter.GeoToWorldPosition(
                route.stops[i + 1].latitude, route.stops[i + 1].longitude);

            int startNode = graph.FindNearestNodeIndex(aWorld);
            int goalNode  = graph.FindNearestNodeIndex(bWorld);

            var path = graph.FindPathAStar(startNode, goalNode);

            if (path.Count >= 2)
            {
                foreach (int ni in path)
                    refined.Add((graph.nodes[ni].lat, graph.nodes[ni].lon));
            }
            else
            {
                // Fallback: straight line between stops.
                refined.Add((route.stops[i].latitude,     route.stops[i].longitude));
                refined.Add((route.stops[i + 1].latitude, route.stops[i + 1].longitude));
            }
        }

        if (refined.Count > 0)
            route.geometry = refined;
    }

    // ── BusRoute asset creation ────────────────────────────────────────

    static BusRoute CreateBusRouteAsset(BusRouteParser.ParsedBusRoute pr)
    {
        var route      = ScriptableObject.CreateInstance<BusRoute>();
        route.name     = SanitiseName($"Route_{pr.routeRef}_{pr.destinationName}");
        route.routeName = pr.routeName;
        route.baseFare  = 50f;

        var stops = new BusStopData[pr.stops.Count];
        for (int i = 0; i < pr.stops.Count; i++)
        {
            stops[i] = new BusStopData
            {
                stopName  = pr.stops[i].stopName,
                latitude  = pr.stops[i].latitude,
                longitude = pr.stops[i].longitude,
            };
        }
        route.stops = stops;

        return route;
    }

    void MergeIntoCity(CityDefinition city)
    {
        if (city == null || importedRoutes.Count == 0) return;

        var existing  = city.availableRoutes ?? System.Array.Empty<BusRoute>();
        var all       = new BusRoute[existing.Length + importedRoutes.Count];
        existing.CopyTo(all, 0);
        importedRoutes.CopyTo(all, existing.Length);
        city.availableRoutes = all;

        Debug.Log($"[OSMRouteImporter] City '{city.cityName}' now has {all.Length} routes.");
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
