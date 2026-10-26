using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.IO;

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
    [Tooltip("Attempts per endpoint before moving to the next mirror.")]
    [Range(1, 4)] public int attemptsPerEndpoint = OverpassDownloadPolicy.DefaultAttemptsPerEndpoint;
    [Tooltip("Try the public Overpass mirrors when the configured endpoint fails.")]
    public bool useEndpointFallback = true;

    [Header("Stop Snapping (3.7A)")]
    [Tooltip("Maximum distance a stop may be from its snapped road position.")]
    [Range(10f, 200f)] public float maxSnapDistanceMeters = 60f;

    [Header("State (read-only)")]
    public bool  importInProgress;
    public bool  importComplete;
    /// <summary>True when the last attempt failed. Existing routes are kept.</summary>
    public bool  importFailed;
    /// <summary>Why the last attempt failed, for the route UI and diagnostics.</summary>
    public string lastError;
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
    UnityWebRequest activeRequest;
    bool importCancelled;
    string lastImportedCityCode;
    string lastRoutesSignature;

    // ── Lifecycle ──────────────────────────────────────────────────────

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        _converter = CoordinateConverter.Instance ?? FindAnyObjectByType<CoordinateConverter>();
        if (autoImportOnStart)
            StartCoroutine(WaitForCityThenImport());
    }

    // ── Public API ─────────────────────────────────────────────────────

    public void TriggerImport() => TriggerImport(CityManager.Instance?.activeCity);

    public void TriggerImport(CityDefinition city)
    {
        city ??= CityManager.Instance?.activeCity;
        if (city == null)
        {
            SetStatus("No active city — import aborted.");
            return;
        }

        if (importInProgress)
        {
            // Starting a second coroutine would duplicate every Overpass query and
            // race two road graphs into the same city.
            Debug.LogWarning("[OSMRouteImporter] Import already in progress; request ignored.");
            return;
        }

        if (AlreadyImported(city))
        {
            Debug.Log($"[OSMRouteImporter] {city.cityName} already has {importedRoutes.Count} imported routes; " +
                      "skipping the duplicate fetch.");
            OnImportComplete?.Invoke(importedRoutes);
            return;
        }

        CityManager.Instance?.SetActiveCity(city);
        StartCoroutine(ImportRoutesForCity(city));
    }

    /// <summary>
    /// Stops the current import between requests. Completed routes stay in place;
    /// an aborted import never clears the route list the player is already using.
    /// </summary>
    public void CancelImport()
    {
        if (!importInProgress) return;
        importCancelled = true;
        activeRequest?.Abort();
        SetStatus("Route import cancelled.");
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

        if (AlreadyImported(city))
        {
            OnImportComplete?.Invoke(importedRoutes);
            yield break;
        }

        _converter = EnsureConverter(city);
        importInProgress = true;
        importComplete   = false;
        importFailed     = false;
        lastError        = null;
        importCancelled  = false;

        // New routes are built into a local list and only replace the visible
        // list once the import has succeeded. A failed or cancelled fetch must not
        // empty the routes the player is already driving.
        var builtRoutes = new List<BusRoute>();
        try
        {

        // ── Step 1: Fetch road graph data ─────────────────────────────
        SetStatus($"Fetching road network for {city.cityName}…");
        string roadsQuery = OverpassQueryBuilder.RoadsQuery(
            city.minLat, city.minLon, city.maxLat, city.maxLon, roadsTimeoutSec);

        string roadsJson = localRoadsJson != null ? localRoadsJson.text : ReadDownloadedOrBundled(city, "roads.json");
        if (roadsJson == null)
            yield return FetchOverpass(roadsQuery, roadsTimeoutSec, json => roadsJson = json);

        if (importCancelled) { SetStatus("Route import cancelled."); yield break; }
        if (string.IsNullOrEmpty(roadsJson))
        {
            FailImport("Road network fetch failed.", "No usable roads response from any Overpass endpoint.");
            yield break;
        }

        // ── Step 2: Build road graph ──────────────────────────────────
        SetStatus("Building road graph…");
        yield return null; // breathe

        OverpassResponse roadsResponse;
        try
        {
            roadsResponse = OverpassResponse.Deserialize(roadsJson);
            roadsResponse.Resolve();
        }
        catch (System.Exception exception)
        {
            FailImport("Road network data was malformed.", exception.Message);
            yield break;
        }

        var roadGraph = RoadGraph.BuildFromOverpassWays(roadsResponse, _converter);
        Debug.Log($"[OSMRouteImporter] Road graph: {roadGraph.nodes.Count} nodes.");

        // ── Step 3: Fetch bus routes ──────────────────────────────────
        SetStatus("Fetching bus routes…");
        string routesQuery = OverpassQueryBuilder.BusRoutesQuery(
            city.minLat, city.minLon, city.maxLat, city.maxLon, routesTimeoutSec);

        string routesJson = localRoutesJson != null ? localRoutesJson.text : ReadDownloadedOrBundled(city, "routes.json");
        if (routesJson == null)
            yield return FetchOverpass(routesQuery, routesTimeoutSec, json => routesJson = json);

        if (importCancelled) { SetStatus("Route import cancelled."); yield break; }
        if (string.IsNullOrEmpty(routesJson))
        {
            FailImport("Bus routes fetch failed.", "No usable routes response from any Overpass endpoint.");
            yield break;
        }

        // ── Step 4: Parse routes ──────────────────────────────────────
        SetStatus("Parsing routes…");
        yield return null;

        List<BusRouteParser.ParsedBusRoute> parsedRoutes;
        try
        {
            var routesResponse = OverpassResponse.Deserialize(routesJson);
            routesResponse.Resolve();
            parsedRoutes = BusRouteParser.ParseRoutes(routesResponse);
        }
        catch (System.Exception exception)
        {
            FailImport("Bus route data was malformed.", exception.Message);
            yield break;
        }

        routesFound = parsedRoutes.Count;
        Debug.Log($"[OSMRouteImporter] Parsed {routesFound} raw routes.");

        // Validate connectivity before filtering on the actual drivable distance.
        var accepted = new List<BusRouteParser.ParsedBusRoute>();
        foreach (var route in parsedRoutes)
        {
            if (importCancelled) break;
            if (RoadRoutePathBuilder.TryBuild(route, roadGraph, _converter, maxSnapDistanceMeters))
                accepted.Add(route);
            yield return null;
        }
        if (importCancelled) { SetStatus("Route import cancelled."); yield break; }

        bool filterImportedRoutes = applyRouteFilter && !IsExplicitSingleRouteDownload(city);
        parsedRoutes = filterImportedRoutes ? BusRouteParser.FilterRoutes(accepted) : accepted;
        if (generateReturnRoutes)
        {
            var returns = BusRouteParser.GenerateReturnRoutes(parsedRoutes);
            accepted = new List<BusRouteParser.ParsedBusRoute>();
            foreach (var route in returns)
            {
                if (importCancelled) break;
                if (RoadRoutePathBuilder.TryBuild(route, roadGraph, _converter, maxSnapDistanceMeters))
                    accepted.Add(route);
                yield return null;
            }
            if (importCancelled) { SetStatus("Route import cancelled."); yield break; }
            parsedRoutes.AddRange(filterImportedRoutes ? BusRouteParser.FilterRoutes(accepted) : accepted);
        }

        // ── Step 8: Convert to BusRoute ScriptableObjects ─────────────
        SetStatus("Creating route assets…");
        yield return null;

        foreach (var pr in parsedRoutes)
        {
            if (importCancelled) break;
            var route = CreateBusRouteAsset(pr);
            route.sourceCityCode = city.cityCode;
            builtRoutes.Add(route);
        }

        if (importCancelled) { SetStatus("Route import cancelled."); yield break; }

        // ── Step 9: Publish and register with the city ────────────────
        importedRoutes = builtRoutes;
        routesAccepted = importedRoutes.Count;
        MergeIntoCity(city);

        importComplete = true;
        lastImportedCityCode = city.cityCode;
        lastRoutesSignature = RoutesSignature(city);
        SetStatus($"Done — {routesAccepted} routes imported.");
        Debug.Log($"[OSMRouteImporter] Complete: {routesAccepted} routes for {city.cityName}.");
        OnImportComplete?.Invoke(importedRoutes);
        }
        finally
        {
            importInProgress = false;
            activeRequest?.Dispose();
            activeRequest = null;
        }
    }

    /// <summary>
    /// True when this exact city and route source were imported successfully in
    /// this session. CitySelector asks for an import every time a city with a
    /// routes.json is opened; without this check that re-fetched and re-built the
    /// same road graph and route set on every visit.
    /// </summary>
    bool AlreadyImported(CityDefinition city)
    {
        if (!importComplete || importedRoutes == null || importedRoutes.Count == 0)
            return false;
        if (!string.Equals(lastImportedCityCode, city.cityCode, System.StringComparison.OrdinalIgnoreCase))
            return false;

        string signature = RoutesSignature(city);
        return signature == null || string.Equals(signature, lastRoutesSignature, System.StringComparison.Ordinal);
    }

    /// <summary>
    /// Identity of the route source: the downloaded/bundled file's size and write
    /// time, or the authored TextAsset. A new download changes the signature and
    /// correctly forces a re-import.
    /// </summary>
    string RoutesSignature(CityDefinition city)
    {
        if (localRoutesJson != null) return "local:" + localRoutesJson.text.Length;

        string path = RuntimeCityContentStore.ResolveReadPath(city, "routes.json");
        try
        {
            if (File.Exists(path))
            {
                var info = new FileInfo(path);
                return $"{info.Length}:{info.LastWriteTimeUtc.Ticks}";
            }
        }
        catch (IOException) { }
        catch (System.UnauthorizedAccessException) { }
        return null;
    }

    void FailImport(string status, string error)
    {
        importFailed = true;
        lastError = error;
        SetStatus(string.IsNullOrWhiteSpace(error) ? status : $"{status} {error}");
        Debug.LogError($"[OSMRouteImporter] {status} {error}");
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
        RouteNaming.Apply(route);
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

    IEnumerator FetchOverpass(string query, int timeoutSeconds, System.Action<string> onDone)
    {
        List<string> mirrors;
        if (useEndpointFallback)
        {
            mirrors = OverpassDownloadPolicy.BuildEndpointList(overpassEndpoint);
        }
        else
        {
            mirrors = new List<string>(1)
            {
                string.IsNullOrWhiteSpace(overpassEndpoint)
                    ? OverpassDownloadPolicy.PublicEndpoints[0]
                    : overpassEndpoint.Trim()
            };
        }

        int attempts = Mathf.Clamp(attemptsPerEndpoint, 1, 4);
        int timeout = Mathf.Max(30, timeoutSeconds) + 10;

        for (int i = 0; i < mirrors.Count && !importCancelled; i++)
        {
            for (int attempt = 1; attempt <= attempts && !importCancelled; attempt++)
            {
                byte[] raw = System.Text.Encoding.UTF8.GetBytes(
                    "data=" + UnityWebRequest.EscapeURL(query));

                activeRequest = new UnityWebRequest(mirrors[i], "POST")
                {
                    uploadHandler = new UploadHandlerRaw(raw),
                    downloadHandler = new DownloadHandlerBuffer(),
                    timeout = timeout
                };
                activeRequest.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                activeRequest.SetRequestHeader("Accept", "application/json");
                activeRequest.SetRequestHeader("User-Agent", "RealBusSimulator/1.0");

                yield return activeRequest.SendWebRequest();

                string json = activeRequest.downloadHandler != null
                    ? activeRequest.downloadHandler.text
                    : null;
                bool valid = activeRequest.result == UnityWebRequest.Result.Success &&
                             OverpassDownloadPolicy.LooksLikeOverpassJson(json);
                if (valid)
                {
                    Debug.Log($"[OSMRouteImporter] Overpass response via {mirrors[i]}: {json.Length:N0} bytes.");
                    activeRequest.Dispose();
                    activeRequest = null;
                    onDone?.Invoke(json);
                    yield break;
                }

                long status = activeRequest.responseCode;
                string error = activeRequest.error;
                string retryAfter = activeRequest.GetResponseHeader("Retry-After");
                string reason = activeRequest.result == UnityWebRequest.Result.Success
                    ? "the response was not a usable Overpass document"
                    : OverpassDownloadPolicy.Describe(activeRequest.result, status, error);
                Debug.LogWarning($"[OSMRouteImporter] Overpass failed via {mirrors[i]} " +
                                 $"(attempt {attempt}/{attempts}): {reason}");

                bool retryable = activeRequest.result == UnityWebRequest.Result.Success ||
                                 OverpassDownloadPolicy.IsRetryable(activeRequest.result, status, error);
                activeRequest.Dispose();
                activeRequest = null;

                if (!retryable || attempt >= attempts)
                    break;

                yield return new WaitForSecondsRealtime(
                    OverpassDownloadPolicy.RetryDelaySeconds(attempt, retryAfter));
            }

            if (i + 1 < mirrors.Count && !importCancelled)
                yield return new WaitForSecondsRealtime(1f + i);
        }

        if (!importCancelled)
            Debug.LogError($"[OSMRouteImporter] Every Overpass endpoint failed for this query ({mirrors.Count} tried).");
        onDone?.Invoke(null);
    }

    // ── Helpers ────────────────────────────────────────────────────────

    static CoordinateConverter EnsureConverter(CityDefinition city)
    {
        CoordinateConverter converter = CoordinateConverter.Instance ?? FindAnyObjectByType<CoordinateConverter>();
        if (converter == null) converter = new GameObject("CoordinateConverter_RuntimeRoutes").AddComponent<CoordinateConverter>();
        converter.SetCityOrigin(city);
        return converter;
    }

    static string ReadDownloadedOrBundled(CityDefinition city, string fileName)
    {
        string path = RuntimeCityContentStore.ResolveReadPath(city, fileName);
        try { return File.Exists(path) ? File.ReadAllText(path) : null; }
        catch (IOException exception)
        {
            Debug.LogWarning($"[OSMRouteImporter] Could not read {path}: {exception.Message}");
            return null;
        }
    }

    static bool IsExplicitSingleRouteDownload(CityDefinition city)
    {
        string marker = RuntimeCityContentStore.WritablePath(city, "routes.mode");
        try { return File.Exists(marker) && string.Equals(File.ReadAllText(marker).Trim(), "single", System.StringComparison.OrdinalIgnoreCase); }
        catch (IOException) { return false; }
    }

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
