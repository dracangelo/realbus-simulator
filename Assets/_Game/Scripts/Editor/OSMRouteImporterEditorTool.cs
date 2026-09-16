#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// =============================================================================
// OSMRouteImporterEditorTool
// Tools → RealBus → OSM → Auto-Generate Bus Routes (Overpass)
//
// Fix summary (vs previous version):
//   - OverpassQueryBuilder.RoadsQuery now uses `out geom` instead of
//     (._;>;) recursive node fetch — eliminates the primary cause of 504s
//   - Highway whitelist reduces payload size and server processing time
//   - Server-side timeout raised to 180 s; Unity request.timeout to 220 s
//     (must exceed server timeout so we get the error body, not a raw drop)
//   - PostOverpass retries each mirror up to 2×, with per-attempt back-off
//   - Stops query also picks up public_transport=stop_position nodes
//   - Return-variant pathfinding extracted into shared helper (DRY)
//   - Minor null-safety + status-string improvements throughout
// =============================================================================

public class OSMRouteImporterEditorTool : EditorWindow
{
    // ── inspector state ───────────────────────────────────────────────────────
    string overpassUrl = "https://overpass-api.de/api/interpreter";
    readonly string[] fallbackOverpassUrls =
    {
        "https://overpass.kumi.systems/api/interpreter",
        "https://lz4.overpass-api.de/api/interpreter"
    };

    double minLat = -1.2900, minLon = 36.8000, maxLat = -1.2630, maxLon = 36.8280;
    float  clusterRadiusMeters = 350f;
    int    minStops      = 6;
    int    maxStops      = 40;
    float  minKm         = 3f;
    float  maxKm         = 25f;
    int    maxRoutesToSave = 20;
    string localRoadsPath  = "";
    string localStopsPath  = "";

    // ── runtime state ─────────────────────────────────────────────────────────
    string            status = "";
    RoadGraph         graph;
    List<BusStop>     importedStops = new List<BusStop>();
    List<RouteDraft>  drafts        = new List<RouteDraft>();
    CoordinateConverter converter;

    // ─────────────────────────────────────────────────────────────────────────
    [MenuItem("Tools/RealBus/OSM/Auto-Generate Bus Routes (Overpass)")]
    public static void Open() => GetWindow<OSMRouteImporterEditorTool>("OSM Route Importer");

    // ── GUI ───────────────────────────────────────────────────────────────────
    void OnGUI()
    {
        GUILayout.Label("Overpass bounding box", EditorStyles.boldLabel);
        minLat      = EditorGUILayout.DoubleField("Min Lat",      minLat);
        minLon      = EditorGUILayout.DoubleField("Min Lon",      minLon);
        maxLat      = EditorGUILayout.DoubleField("Max Lat",      maxLat);
        maxLon      = EditorGUILayout.DoubleField("Max Lon",      maxLon);
        overpassUrl = EditorGUILayout.TextField("Overpass URL",   overpassUrl);

        GUILayout.Space(8);
        GUILayout.Label("Route generation", EditorStyles.boldLabel);
        clusterRadiusMeters = EditorGUILayout.Slider("Cluster radius (m)", clusterRadiusMeters, 200f, 400f);
        minStops        = EditorGUILayout.IntField("Min stops",         minStops);
        maxStops        = EditorGUILayout.IntField("Max stops",         maxStops);
        minKm           = EditorGUILayout.FloatField("Min distance (km)", minKm);
        maxKm           = EditorGUILayout.FloatField("Max distance (km)", maxKm);
        maxRoutesToSave = EditorGUILayout.IntSlider("Max routes to save", maxRoutesToSave, 10, 20);

        GUILayout.Space(8);
        if (GUILayout.Button("1) Download roads + stops"))
            EditorCoroutineUtility.StartCoroutineOwnerless(DownloadRoadsAndStops());
        if (GUILayout.Button("1b) Load local roads + stops (no network)"))
            LoadRoadsAndStopsFromLocalFiles();
        if (GUILayout.Button("2) Cluster stops → draft routes"))
            ClusterStopsIntoDraftRoutes();
        if (GUILayout.Button("3) Generate paths (A*) + filter"))
            GeneratePathsAndFilter();
        if (GUILayout.Button("4) Save BusRoute assets (+ return routes)"))
            SaveAssets();

        GUILayout.Space(10);
        GUILayout.Label("Local data workflow", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "Fetch JSON offline with fetch_osm_city_json.sh, then load here to " +
            "avoid Overpass timeouts entirely. Roads JSON must use `out geom`.",
            MessageType.None);

        using (new EditorGUILayout.HorizontalScope())
        {
            localRoadsPath = EditorGUILayout.TextField("Roads JSON", localRoadsPath);
            if (GUILayout.Button("Browse", GUILayout.Width(70)))
                localRoadsPath = PickJsonFile(localRoadsPath);
        }
        using (new EditorGUILayout.HorizontalScope())
        {
            localStopsPath = EditorGUILayout.TextField("Stops JSON", localStopsPath);
            if (GUILayout.Button("Browse", GUILayout.Width(70)))
                localStopsPath = PickJsonFile(localStopsPath);
        }

        GUILayout.Space(10);
        EditorGUILayout.HelpBox(status, MessageType.Info);

        if (drafts != null && drafts.Count > 0)
        {
            GUILayout.Label($"Draft routes: {drafts.Count}", EditorStyles.boldLabel);
            for (int i = 0; i < Mathf.Min(20, drafts.Count); i++)
            {
                string line = $"{i + 1}. {drafts[i].name}  stops={drafts[i].stops.Count}  km={drafts[i].distanceKm:0.0}  diff={drafts[i].difficulty}";
                if (!string.IsNullOrWhiteSpace(drafts[i].debugStatus))
                    line += $"  [{drafts[i].debugStatus}]";
                GUILayout.Label(line);
            }
        }
    }

    // ── step 1 — download ─────────────────────────────────────────────────────
    IEnumerator DownloadRoadsAndStops()
    {
        status = "Initialising…";
        graph = null;
        importedStops.Clear();
        drafts.Clear();
        EnsureEditorConverter();

        // --- roads ---
        string roadsJson = LoadCache("roads");
        if (!string.IsNullOrEmpty(roadsJson))
        {
            status = "Loaded cached roads. Building graph…";
        }
        else
        {
            status = "Downloading roads (this may take up to 3 min)…";
            string roadsQuery = OverpassQueryBuilder.RoadsQuery(minLat, minLon, maxLat, maxLon);
            yield return PostOverpass(roadsQuery, j => roadsJson = j);
            if (string.IsNullOrEmpty(roadsJson))
            {
                status = "Road download failed. Use '1b) Load local roads + stops' with files " +
                         "fetched via fetch_osm_city_json.sh or overpass-turbo.eu.";
                yield break;
            }
            SaveCache("roads", roadsJson);
        }

        var roadsResponse = OverpassResponse.Deserialize(roadsJson);
        graph = RoadGraph.BuildFromOverpassWays(roadsResponse, converter);
        if (graph == null || graph.nodes.Count == 0)
        {
            status = "Road graph is empty — check that the roads JSON contains way geometry (`out geom`).";
            yield break;
        }

        // --- stops ---
        string stopsJson = LoadCache("stops");
        if (!string.IsNullOrEmpty(stopsJson))
        {
            status = "Loaded cached stops. Parsing…";
        }
        else
        {
            status = $"Road graph built ({graph.nodes.Count} nodes). Downloading bus stops…";
            string stopsQuery = OverpassQueryBuilder.StopsQuery(minLat, minLon, maxLat, maxLon);
            yield return PostOverpass(stopsQuery, j => stopsJson = j);
            if (string.IsNullOrEmpty(stopsJson))
            {
                status = "Stop download failed (road graph is ready — you can load stops locally via 1b).";
                yield break;
            }
            SaveCache("stops", stopsJson);
        }

        var stopsResponse = OverpassResponse.Deserialize(stopsJson);
        PopulateImportedStops(stopsResponse);
        status = $"Ready — roads: {graph.nodes.Count} nodes  |  stops: {importedStops.Count}. Run step 2.";
    }

    // ── step 1b — load local files ────────────────────────────────────────────
    void LoadRoadsAndStopsFromLocalFiles()
    {
        status = "Loading local roads + stops…";
        graph = null;
        importedStops.Clear();
        drafts.Clear();
        EnsureEditorConverter();

        string roadsJson = LoadJsonFromPath(localRoadsPath, "roads");
        if (string.IsNullOrEmpty(roadsJson))
        {
            status = "Roads load failed — pick a valid roads JSON file (must use `out geom`).";
            return;
        }

        var roadsResponse = OverpassResponse.Deserialize(roadsJson);
        graph = RoadGraph.BuildFromOverpassWays(roadsResponse, converter);
        if (graph == null || graph.nodes.Count == 0)
        {
            status = "Roads parsed but no graph nodes built — ensure roads.json was exported with `out geom` not `out body`.";
            return;
        }

        string stopsJson = LoadJsonFromPath(localStopsPath, "stops");
        if (string.IsNullOrEmpty(stopsJson))
        {
            status = $"Roads loaded ({graph.nodes.Count} nodes) but stops file is missing/empty.";
            return;
        }

        var stopsResponse = OverpassResponse.Deserialize(stopsJson);
        PopulateImportedStops(stopsResponse);
        status = $"Local load OK — roads: {graph.nodes.Count} nodes  |  stops: {importedStops.Count}. Run step 2.";
    }

    // ── Overpass HTTP helper ──────────────────────────────────────────────────
    IEnumerator PostOverpass(string query, System.Action<string> onDone)
    {
        string postData = "data=" + UnityWebRequest.EscapeURL(query);

        var endpoints = new List<string> { overpassUrl };
        foreach (var fb in fallbackOverpassUrls)
            if (!string.IsNullOrWhiteSpace(fb) && fb != overpassUrl)
                endpoints.Add(fb);

        for (int ep = 0; ep < endpoints.Count; ep++)
        {
            string endpoint = endpoints[ep];

            // Each mirror gets up to 2 attempts before we move on.
            for (int attempt = 1; attempt <= 2; attempt++)
            {
                status = $"Overpass endpoint {ep + 1}/{endpoints.Count}  attempt {attempt}/2 — {endpoint}";

                using (var req = new UnityWebRequest(endpoint, "POST"))
                {
                    byte[] body = System.Text.Encoding.UTF8.GetBytes(postData);
                    req.uploadHandler   = new UploadHandlerRaw(body);
                    req.downloadHandler = new DownloadHandlerBuffer();
                    req.SetRequestHeader("Content-Type",  "application/x-www-form-urlencoded");
                    req.SetRequestHeader("Accept",        "application/json");
                    req.SetRequestHeader("User-Agent",    "RealBusUnity/1.0");
                    // Must be > server-side [timeout:180] so we receive the error body
                    // rather than the connection being dropped mid-response.
                    req.timeout = 220;

#if UNITY_2020_1_OR_NEWER
                    yield return req.SendWebRequest();
#else
                    yield return req.Send();
#endif

                    string responseText = req.downloadHandler?.text ?? "";
                    bool   isSuccess    = req.result == UnityWebRequest.Result.Success
                                         && !string.IsNullOrWhiteSpace(responseText)
                                         && responseText.TrimStart().StartsWith("{");

                    if (isSuccess)
                    {
                        if (ep > 0 || attempt > 1)
                            Debug.LogWarning($"[OSMImporter] Succeeded via {endpoint} (ep={ep} attempt={attempt})");
                        onDone?.Invoke(responseText);
                        yield break;
                    }

                    string excerpt = string.IsNullOrWhiteSpace(responseText)
                        ? "<empty body>"
                        : responseText.Substring(0, Mathf.Min(400, responseText.Length));

                    Debug.LogError(
                        $"[OSMImporter] Overpass failed: ep={endpoint} " +
                        $"result={req.result} code={req.responseCode} " +
                        $"error={req.error} body={excerpt}");
                }

                // Back-off before retry on the same mirror or next mirror.
                float delay = attempt == 1 ? 3f : 6f * (ep + 1);
                status = $"Waiting {delay:0}s before retry…";
                yield return WaitForEditorSeconds(delay);
            }
        }

        status = "All Overpass endpoints exhausted. See Console. " +
                 "Use fetch_osm_city_json.sh or overpass-turbo.eu to fetch offline, " +
                 "then use '1b) Load local roads + stops'.";
        onDone?.Invoke(null);
    }

    // ── step 2 — cluster ──────────────────────────────────────────────────────
    void ClusterStopsIntoDraftRoutes()
    {
        drafts.Clear();
        if (graph == null || graph.nodes.Count == 0) { status = "No road graph — run step 1 first.";  return; }
        if (importedStops == null || importedStops.Count == 0) { status = "No stops — run step 1 first."; return; }

        status = "Snapping stops to nearest road segment…";
        var snapped = new List<SnappedStop>(importedStops.Count);

        for (int i = 0; i < importedStops.Count; i++)
        {
            var s = importedStops[i];
            Vector3 w = converter.GeoToWorldPosition(s.latitude, s.longitude);
            if (!graph.TryProjectToNearestSegment(w, out Vector3 proj, out int a, out int b, out float _))
                continue;

            var gps = converter.WorldToGeoPosition(proj);
            snapped.Add(new SnappedStop
            {
                stop = new BusStop
                {
                    stopId         = s.stopId,
                    stopName       = s.stopName,
                    latitude       = gps.lat,
                    longitude      = gps.lon,
                    headingDegrees = s.headingDegrees
                },
                snappedWorld = proj,
                nearestNode  = ChooseNearestEndpoint(proj,
                                   graph.nodes[a].world,
                                   graph.nodes[b].world, a, b)
            });
        }

        status = $"Snapped {snapped.Count}/{importedStops.Count} stops. Clustering…";
        var clusters = ClusterByRadius(snapped, clusterRadiusMeters);

        int idx = 0;
        foreach (var cluster in clusters)
        {
            if (cluster.Count < minStops) continue;
            drafts.Add(new RouteDraft
            {
                name  = $"Draft_{idx++}",
                stops = OrderStopsByGreedyPath(cluster)
            });
        }

        status = $"Created {drafts.Count} draft corridors from {clusters.Count} clusters. Run step 3.";
    }

    // ── step 3 — pathfind + filter ────────────────────────────────────────────
    void GeneratePathsAndFilter()
    {
        if (drafts == null || drafts.Count == 0) { status = "No drafts — run step 2 first."; return; }
        if (graph == null)                        { status = "No road graph — run step 1 first."; return; }

        status = "Running A* between stops…";

        var splitDrafts = new List<RouteDraft>();
        foreach (var d in drafts)
            splitDrafts.AddRange(SplitDraftOnDisconnectedPairs(d));

        var evaluatedDrafts = new List<RouteDraft>(splitDrafts.Count);
        foreach (var d in splitDrafts)
        {
            d.pathWorld.Clear();
            d.distanceKm = 0f;
            d.debugStatus = "";
            if (d.stops.Count < 2)
            {
                d.debugStatus = "rejected: fewer than 2 stops";
                evaluatedDrafts.Add(d);
                continue;
            }

            BuildPath(d);

            if (d.pathWorld.Count < 2)
                d.debugStatus = string.IsNullOrWhiteSpace(d.debugStatus) ? "rejected: no valid path" : d.debugStatus;
            else if (d.distanceKm < minKm)
                d.debugStatus = $"rejected: too short ({d.distanceKm:0.0}km)";
            else if (d.distanceKm > maxKm)
                d.debugStatus = $"rejected: too long ({d.distanceKm:0.0}km)";
            else
                d.debugStatus = "usable";

            evaluatedDrafts.Add(d);
        }

        var usableDrafts = evaluatedDrafts
            .Where(d => d.stops.Count  >= minStops && d.stops.Count  <= maxStops)
            .Where(d => d.distanceKm   >= minKm    && d.distanceKm   <= maxKm)
            .Where(d => d.pathWorld.Count >= 2)
            .OrderByDescending(d => d.stops.Count)
            .ThenBy(d => Mathf.Abs(12f - d.distanceKm))   // bias toward mid-length routes
            .Take(Mathf.Clamp(maxRoutesToSave, 10, 20))
            .ToList();

        if (usableDrafts.Count == 0)
        {
            drafts = evaluatedDrafts;
            status = "Filtered to 0 usable routes. Check the rejection reason shown beside each draft. For this bbox, try a smaller cluster radius like 200-250m, or increase max distance if the route is simply too long.";
            return;
        }

        drafts = usableDrafts;
        status = $"Filtered to {drafts.Count} usable routes. Run step 4 to save assets.";
    }

    // ── step 4 — save assets ──────────────────────────────────────────────────
    void SaveAssets()
    {
        if (drafts == null || drafts.Count == 0) { status = "No routes to save — run steps 2–3 first."; return; }

        const string folder = "Assets/_Game/Routes";
        if (!AssetDatabase.IsValidFolder("Assets/_Game"))
            AssetDatabase.CreateFolder("Assets", "_Game");
        if (!AssetDatabase.IsValidFolder(folder))
            AssetDatabase.CreateFolder("Assets/_Game", "Routes");

        int saved = 0;
        foreach (var d in drafts)
        {
            saved += SaveOne(folder, d, isReturn: false);

            var ret = d.MakeReturnVariant();
            BuildPath(ret);

            if (ret.stops.Count  >= minStops && ret.stops.Count  <= maxStops &&
                ret.distanceKm   >= minKm    && ret.distanceKm   <= maxKm    &&
                ret.pathWorld.Count >= 2)
            {
                saved += SaveOne(folder, ret, isReturn: true);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        int linked = RouteAssetRegistry.RepairImportedRoutes(false);
        status = $"Saved {saved} BusRoute assets and linked {linked} routes to their cities.";
    }

    // ── shared pathfinding helper ─────────────────────────────────────────────
    // Fills d.pathWorld, d.distanceKm, d.estimatedTimeMinutes, d.difficulty.
    void BuildPath(RouteDraft d)
    {
        d.pathWorld.Clear();
        if (d.stops.Count < 2) return;

        var nodeIndices = d.stops.Select(s => s.nearestNode).ToList();
        var pathNodes   = new List<int>();

        for (int i = 0; i < nodeIndices.Count - 1; i++)
        {
            var sub = graph.FindPathAStar(nodeIndices[i], nodeIndices[i + 1]);
            if (sub == null || sub.Count == 0)
            {
                d.debugStatus = $"rejected: no path between stops {i + 1} and {i + 2}";
                pathNodes.Clear();
                break;
            }
            // De-duplicate shared node at segment boundary.
            if (pathNodes.Count > 0 && pathNodes[pathNodes.Count - 1] == sub[0])
                sub.RemoveAt(0);
            pathNodes.AddRange(sub);
        }

        for (int i = 0; i < pathNodes.Count; i++)
            d.pathWorld.Add(graph.nodes[pathNodes[i]].world + Vector3.up * 0.1f);

        d.distanceKm          = ComputeDistanceKm(d.pathWorld);
        d.estimatedTimeMinutes = EstimateTimeMinutes(d);
        d.difficulty           = ClassifyDifficulty(d);
    }

    List<RouteDraft> SplitDraftOnDisconnectedPairs(RouteDraft source)
    {
        var result = new List<RouteDraft>();
        if (source == null || source.stops == null || source.stops.Count == 0)
            return result;

        var currentStops = new List<SnappedStop> { source.stops[0] };
        int splitIndex = 0;

        for (int i = 0; i < source.stops.Count - 1; i++)
        {
            var current = source.stops[i];
            var next = source.stops[i + 1];
            var sub = graph.FindPathAStar(current.nearestNode, next.nearestNode);

            if (sub != null && sub.Count > 0)
            {
                currentStops.Add(next);
                continue;
            }

            AddSplitDraftIfUseful(result, source, currentStops, splitIndex++);
            currentStops = new List<SnappedStop> { next };
        }

        AddSplitDraftIfUseful(result, source, currentStops, splitIndex);

        if (result.Count == 0)
        {
            source.debugStatus = "rejected: disconnected stop sequence";
            result.Add(source);
        }

        return result;
    }

    void AddSplitDraftIfUseful(List<RouteDraft> result, RouteDraft source, List<SnappedStop> stopsSlice, int splitIndex)
    {
        if (stopsSlice == null || stopsSlice.Count == 0)
            return;

        var draft = new RouteDraft
        {
            name = splitIndex == 0 ? source.name : $"{source.name}_part{splitIndex + 1}",
            routeRef = source.routeRef
        };
        draft.stops.AddRange(stopsSlice);

        if (draft.stops.Count < 2)
        {
            draft.debugStatus = "rejected: disconnected single stop fragment";
        }
        else if (draft.stops.Count < minStops)
        {
            draft.debugStatus = $"rejected: disconnected fragment too short ({draft.stops.Count} stops)";
        }

        result.Add(draft);
    }

    // ── asset writer ──────────────────────────────────────────────────────────
    int SaveOne(string folder, RouteDraft d, bool isReturn)
    {
        var asset = ScriptableObject.CreateInstance<BusRoute>();
        asset.routeName   = GenerateRouteName(d, isReturn);
        asset.routeNumber = GenerateRouteNumber(d, isReturn);
        asset.sourceCityCode = ResolveRouteCityCode();
        asset.generatedRouteId = $"{asset.sourceCityCode}:{asset.routeNumber}:{d.stops.Count}:{isReturn}";
        asset.isRoadPathValidated = d.pathWorld.Count >= 2;
        asset.busStops    = d.stops.Select(s => s.stop).ToArray();
        asset.stops       = d.stops.Select(s => new BusStopData
        {
            stopName = s.stop.stopName,
            latitude = s.stop.latitude,
            longitude = s.stop.longitude,
            waitTimeSeconds = 10f
        }).ToArray();
        asset.pathPoints  = d.pathWorld.ToArray();
        asset.distanceKm          = d.distanceKm;
        asset.estimatedTimeMinutes = d.estimatedTimeMinutes;
        asset.difficulty           = d.difficulty;

        var flat = new List<double>(d.pathWorld.Count * 2);
        foreach (var pt in d.pathWorld)
        {
            var gps = converter.WorldToGeoPosition(pt);
            flat.Add(gps.lat);
            flat.Add(gps.lon);
        }
        asset.geometryLatLonFlat = flat.ToArray();

        string safe = MakeSafeFileName(asset.routeName);
        string path = AssetDatabase.GenerateUniqueAssetPath($"{folder}/{safe}.asset");
        AssetDatabase.CreateAsset(asset, path);
        return 1;
    }

    string GenerateRouteName(RouteDraft d, bool isReturn)
    {
        string cityCode = ResolveRouteCityCode();
        string routeRef = string.IsNullOrWhiteSpace(d.routeRef) ? "" : d.routeRef.Trim();
        string startName = GetMeaningfulStopName(d, fromStart: true);
        string endName = GetMeaningfulStopName(d, fromStart: false);
        string viaName = GetIntermediateStopName(d, startName, endName);
        bool isLoop = IsLoopRoute(d, startName, endName);

        string baseName;
        if (!string.IsNullOrWhiteSpace(routeRef))
        {
            baseName = isLoop
                ? $"Route {routeRef} {startName} Loop"
                : $"Route {routeRef} {startName} - {endName}";
        }
        else if (!string.IsNullOrWhiteSpace(startName) && !string.IsNullOrWhiteSpace(endName))
        {
            baseName = isLoop
                ? $"{startName} Loop"
                : $"{startName} - {endName}";
        }
        else
        {
            baseName = d.name;
        }

        if (!string.IsNullOrWhiteSpace(viaName))
            baseName += $" via {viaName}";

        if (!string.IsNullOrWhiteSpace(cityCode))
            baseName = $"{cityCode} {baseName}";

        return isReturn && !baseName.EndsWith("(Return)")
            ? $"{baseName} (Return)"
            : baseName;
    }

    string GenerateRouteNumber(RouteDraft d, bool isReturn)
    {
        string cityCode = ResolveRouteCityCode();
        string number = string.IsNullOrWhiteSpace(d.routeRef)
            ? $"{(string.IsNullOrWhiteSpace(cityCode) ? "R" : cityCode)}-{d.name.Replace("Draft_", "").Replace("_part", "P")}"
            : d.routeRef;
        return isReturn ? $"{number}R" : number;
    }

    string ResolveRouteCityCode()
    {
        if (TryFindMatchingCity(out var city))
            return city.cityCode;

        string codeFromLocalPath = ExtractCityCodeFromPath(localRoadsPath);
        if (!string.IsNullOrWhiteSpace(codeFromLocalPath))
            return codeFromLocalPath;

        codeFromLocalPath = ExtractCityCodeFromPath(localStopsPath);
        if (!string.IsNullOrWhiteSpace(codeFromLocalPath))
            return codeFromLocalPath;

        return "";
    }

    bool TryFindMatchingCity(out CityDefinition city)
    {
        city = null;
        string[] guids = AssetDatabase.FindAssets("t:CityDefinition");
        double centerLat = (minLat + maxLat) * 0.5;
        double centerLon = (minLon + maxLon) * 0.5;

        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            var candidate = AssetDatabase.LoadAssetAtPath<CityDefinition>(path);
            if (candidate == null)
                continue;

            bool containsCenter =
                centerLat >= candidate.minLat && centerLat <= candidate.maxLat &&
                centerLon >= candidate.minLon && centerLon <= candidate.maxLon;

            if (!containsCenter)
                continue;

            city = candidate;
            return true;
        }

        return false;
    }

    static string ExtractCityCodeFromPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return "";

        string normalized = path.Replace('\\', '/');
        const string marker = "/Cities/";
        int start = normalized.IndexOf(marker, System.StringComparison.OrdinalIgnoreCase);
        if (start < 0)
            return "";

        start += marker.Length;
        int end = normalized.IndexOf('/', start);
        if (end <= start)
            return "";

        return normalized.Substring(start, end - start).Trim();
    }

    string GetMeaningfulStopName(RouteDraft d, bool fromStart)
    {
        if (d?.stops == null || d.stops.Count == 0)
            return "";

        int index = fromStart ? 0 : d.stops.Count - 1;
        int step = fromStart ? 1 : -1;
        string candidate = CleanStopName(d.stops[index].stop.stopName);

        for (int i = index + step; i >= 0 && i < d.stops.Count; i += step)
        {
            string next = CleanStopName(d.stops[i].stop.stopName);
            if (!string.IsNullOrWhiteSpace(next) &&
                !string.Equals(next, candidate, System.StringComparison.OrdinalIgnoreCase))
                return candidate;
        }

        return candidate;
    }

    string GetIntermediateStopName(RouteDraft d, string startName, string endName)
    {
        if (d?.stops == null || d.stops.Count < 3)
            return "";

        int[] preferredIndices =
        {
            d.stops.Count / 2,
            d.stops.Count / 3,
            (d.stops.Count * 2) / 3
        };

        for (int i = 0; i < preferredIndices.Length; i++)
        {
            int idx = Mathf.Clamp(preferredIndices[i], 1, d.stops.Count - 2);
            string name = CleanStopName(d.stops[idx].stop.stopName);
            if (string.IsNullOrWhiteSpace(name))
                continue;
            if (string.Equals(name, startName, System.StringComparison.OrdinalIgnoreCase))
                continue;
            if (string.Equals(name, endName, System.StringComparison.OrdinalIgnoreCase))
                continue;
            return name;
        }

        return "";
    }

    bool IsLoopRoute(RouteDraft d, string startName, string endName)
    {
        if (!string.IsNullOrWhiteSpace(startName) &&
            !string.IsNullOrWhiteSpace(endName) &&
            string.Equals(startName, endName, System.StringComparison.OrdinalIgnoreCase))
            return true;

        if (d?.stops == null || d.stops.Count < 3)
            return false;

        Vector3 start = d.stops[0].snappedWorld;
        Vector3 end = d.stops[d.stops.Count - 1].snappedWorld;
        return Vector3.Distance(start, end) <= 350f;
    }

    static string CleanStopName(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return "Unnamed Stop";

        string value = raw.Trim();
        value = value.Replace("Bus Stop", "").Replace("bus stop", "").Trim();
        value = value.Replace("Stage", "").Trim();

        while (value.Contains("  "))
            value = value.Replace("  ", " ");

        return string.IsNullOrWhiteSpace(value) ? "Unnamed Stop" : value;
    }

    // ── coordinate converter ──────────────────────────────────────────────────
    void EnsureEditorConverter()
    {
        converter = FindFirstObjectByType<CoordinateConverter>();
        if (converter != null) return;

        var go = new GameObject("CoordinateConverter_EditorTemp");
        converter = go.AddComponent<CoordinateConverter>();
        converter.mapOrigin = ScriptableObject.CreateInstance<MapOrigin>();
        converter.mapOrigin.originLat = (minLat + maxLat) * 0.5;
        converter.mapOrigin.originLon = (minLon + maxLon) * 0.5;
    }

    // ── stop population ───────────────────────────────────────────────────────
    void PopulateImportedStops(OverpassResponse r)
    {
        importedStops.Clear();
        if (r?.elements == null) return;

        foreach (var e in r.elements)
        {
            if (e == null || e.type != "node") continue;
            if (e.lat == 0 && e.lon == 0) continue;
            string name = e.tags != null && e.tags.TryGetValue("name", out var n) ? n : $"Stop {e.id}";
            importedStops.Add(new BusStop
            {
                stopId         = e.id.ToString(),
                stopName       = name,
                latitude       = e.lat,
                longitude      = e.lon,
                headingDegrees = 0f
            });
        }
    }

    // ── disk cache ────────────────────────────────────────────────────────────
    string LoadCache(string kind)
    {
        string path = GetCachePath(kind);
        if (!System.IO.File.Exists(path)) return null;
        try   { return System.IO.File.ReadAllText(path); }
        catch (System.Exception ex) { Debug.LogWarning($"[OSMImporter] Cache read failed '{path}': {ex.Message}"); return null; }
    }

    void SaveCache(string kind, string payload)
    {
        if (string.IsNullOrWhiteSpace(payload)) return;
        string path = GetCachePath(kind);
        try
        {
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
            System.IO.File.WriteAllText(path, payload);
        }
        catch (System.Exception ex) { Debug.LogWarning($"[OSMImporter] Cache write failed '{path}': {ex.Message}"); }
    }

    string GetCachePath(string kind)
    {
        string bbox = $"{minLat:F6}_{minLon:F6}_{maxLat:F6}_{maxLon:F6}"
            .Replace('-', 'm').Replace('.', '_');
        return System.IO.Path.Combine("Temp", "RealBusOSMCache", $"{kind}_{bbox}.json");
    }

    // ── local file loading ────────────────────────────────────────────────────
    string LoadJsonFromPath(string configuredPath, string kind)
    {
        string resolved = ResolvePath(configuredPath);
        if (string.IsNullOrWhiteSpace(resolved))
        {
            Debug.LogWarning($"[OSMImporter] No local {kind} file selected.");
            return null;
        }
        if (!System.IO.File.Exists(resolved))
        {
            Debug.LogWarning($"[OSMImporter] Local {kind} file not found: {resolved}");
            return null;
        }
        try   { return System.IO.File.ReadAllText(resolved); }
        catch (System.Exception ex) { Debug.LogWarning($"[OSMImporter] Read failed '{resolved}': {ex.Message}"); return null; }
    }

    static string ResolvePath(string p)
    {
        if (string.IsNullOrWhiteSpace(p)) return null;
        if (System.IO.Path.IsPathRooted(p)) return p;
        string root = System.IO.Directory.GetParent(Application.dataPath).FullName;
        return System.IO.Path.GetFullPath(System.IO.Path.Combine(root, p));
    }

    static string PickJsonFile(string current)
    {
        string start = GetBrowseStartDir(current);
        string picked = EditorUtility.OpenFilePanel("Select Overpass JSON", start, "json");
        return string.IsNullOrWhiteSpace(picked) ? current : ToProjectRelative(picked);
    }

    static string GetBrowseStartDir(string current)
    {
        string r = ResolvePath(current);
        if (!string.IsNullOrWhiteSpace(r))
        {
            if (System.IO.File.Exists(r))      return System.IO.Path.GetDirectoryName(r);
            if (System.IO.Directory.Exists(r)) return r;
        }
        return System.IO.Directory.GetParent(Application.dataPath).FullName;
    }

    static string ToProjectRelative(string abs)
    {
        if (string.IsNullOrWhiteSpace(abs)) return abs;
        string root = System.IO.Path.GetFullPath(
            System.IO.Directory.GetParent(Application.dataPath).FullName)
            + System.IO.Path.DirectorySeparatorChar;
        string full = System.IO.Path.GetFullPath(abs);
        return full.StartsWith(root) ? full.Substring(root.Length) : full;
    }

    static string MakeSafeFileName(string s)
    {
        foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            s = s.Replace(c, '_');
        return s.Replace(" ", "_");
    }

    // ── maths helpers ─────────────────────────────────────────────────────────
    static int ChooseNearestEndpoint(Vector3 p, Vector3 a, Vector3 b, int ai, int bi)
        => (p - a).sqrMagnitude <= (p - b).sqrMagnitude ? ai : bi;

    static float ComputeDistanceKm(List<Vector3> pts)
    {
        if (pts == null || pts.Count < 2) return 0f;
        float m = 0f;
        for (int i = 0; i < pts.Count - 1; i++)
            m += Vector3.Distance(pts[i], pts[i + 1]);
        return m / 1000f;
    }

    static float EstimateTimeMinutes(RouteDraft d)
    {
        float avgKmh  = d.difficulty >= 4 ? 22f : d.difficulty == 3 ? 28f : 40f;
        float driveMin = (d.distanceKm / Mathf.Max(5f, avgKmh)) * 60f;
        float dwellMin = d.stops.Count * 0.35f;
        return driveMin + dwellMin;
    }

    static int ClassifyDifficulty(RouteDraft d)
    {
        if (d.pathWorld.Count < 2)                              return 3;
        if (d.distanceKm > 18f  && d.stops.Count < 12)         return 2;
        if (d.stops.Count >= 20 && d.distanceKm < 12f)         return 4;
        return 3;
    }

    // ── clustering ────────────────────────────────────────────────────────────
    static List<List<SnappedStop>> ClusterByRadius(List<SnappedStop> stops, float radiusMeters)
    {
        var clusters = new List<List<SnappedStop>>();
        if (stops == null || stops.Count == 0) return clusters;

        float r2      = radiusMeters * radiusMeters;
        var   visited = new bool[stops.Count];

        for (int i = 0; i < stops.Count; i++)
        {
            if (visited[i]) continue;
            visited[i] = true;
            var cluster = new List<SnappedStop> { stops[i] };

            for (int q = 0; q < cluster.Count; q++)
            {
                Vector3 sw = cluster[q].snappedWorld;
                for (int j = 0; j < stops.Count; j++)
                {
                    if (visited[j]) continue;
                    if ((stops[j].snappedWorld - sw).sqrMagnitude <= r2)
                    {
                        visited[j] = true;
                        cluster.Add(stops[j]);
                    }
                }
            }
            clusters.Add(cluster);
        }
        return clusters;
    }

    static List<SnappedStop> OrderStopsByGreedyPath(List<SnappedStop> cluster)
    {
        // Start from the stop farthest from the centroid, then nearest-neighbour chain.
        Vector3 centroid = Vector3.zero;
        for (int i = 0; i < cluster.Count; i++) centroid += cluster[i].snappedWorld;
        centroid /= Mathf.Max(1, cluster.Count);

        int   startIdx = 0;
        float bestDist = -1f;
        for (int i = 0; i < cluster.Count; i++)
        {
            float d = (cluster[i].snappedWorld - centroid).sqrMagnitude;
            if (d > bestDist) { bestDist = d; startIdx = i; }
        }

        var remaining = new List<SnappedStop>(cluster);
        var ordered   = new List<SnappedStop>(cluster.Count);
        var current   = remaining[startIdx];
        ordered.Add(current);
        remaining.RemoveAt(startIdx);

        while (remaining.Count > 0)
        {
            int   bestIdx  = 0;
            float bestSqr  = float.MaxValue;
            for (int i = 0; i < remaining.Count; i++)
            {
                float d = (remaining[i].snappedWorld - current.snappedWorld).sqrMagnitude;
                if (d < bestSqr) { bestSqr = d; bestIdx = i; }
            }
            current = remaining[bestIdx];
            ordered.Add(current);
            remaining.RemoveAt(bestIdx);
        }
        return ordered;
    }

    // ── editor wait ───────────────────────────────────────────────────────────
    static IEnumerator WaitForEditorSeconds(float seconds)
    {
        double end = EditorApplication.timeSinceStartup + seconds;
        while (EditorApplication.timeSinceStartup < end)
            yield return null;
    }

    // ── inner types ───────────────────────────────────────────────────────────
    class RouteDraft
    {
        public string           name;
        public string           routeRef;
        public List<SnappedStop> stops    = new List<SnappedStop>();
        public List<Vector3>    pathWorld = new List<Vector3>();
        public float            distanceKm;
        public float            estimatedTimeMinutes;
        public int              difficulty = 3;
        public string           debugStatus;

        public RouteDraft MakeReturnVariant()
        {
            var r = new RouteDraft { name = name, routeRef = routeRef };
            r.stops.AddRange(stops);
            r.stops.Reverse();
            return r;
        }
    }

    class SnappedStop
    {
        public BusStop stop;
        public Vector3 snappedWorld;
        public int     nearestNode;
    }
}

// =============================================================================
// OverpassQueryBuilder
// Centralises all query strings so they're easy to audit and tweak.
//
// CRITICAL: roads use `out geom` NOT `out body` + (._;>;)
//   `out geom` inlines node lat/lon directly into each way element.
//   (._;>;) recursively fetches every referenced node separately — on a
//   large bbox this can 10× the result set and reliably causes 504s.
// =============================================================================
public static class OverpassQueryBuilder
{
    // Drivable highway classes. Excludes footway, cycleway, path, steps,
    // construction, proposed, etc. — none of which buses use.
    const string HighwayFilter =
        "motorway|motorway_link|trunk|trunk_link|" +
        "primary|primary_link|secondary|secondary_link|" +
        "tertiary|tertiary_link|residential|unclassified|road|living_street";

    public static string RoadsQuery(double minLat, double minLon, double maxLat, double maxLon)
    {
        string bbox = $"{minLat},{minLon},{maxLat},{maxLon}";
        // [timeout:180]  — server-side; Unity's request.timeout is set to 220
        // out geom       — inlines geometry, no recursive fetch needed
        return
            $"[out:json][timeout:180];\n" +
            $"way[highway~\"^({HighwayFilter})$\"]({bbox});\n" +
            $"out geom;";
    }

    public static string StopsQuery(double minLat, double minLon, double maxLat, double maxLon)
    {
        string bbox = $"{minLat},{minLon},{maxLat},{maxLon}";
        // Nodes are cheap — timeout:60 is fine here.
        // stop_position added: marks where a bus actually stops on the carriageway.
        return
            $"[out:json][timeout:60];\n" +
            $"(\n" +
            $"  node[highway=bus_stop]({bbox});\n" +
            $"  node[public_transport=platform]({bbox});\n" +
            $"  node[public_transport=stop_position]({bbox});\n" +
            $");\n" +
            $"out body;";
    }
}

// =============================================================================
// EditorCoroutineUtility — minimal coroutine runner for Editor code.
// Supports nested IEnumerator and AsyncOperation yields.
// =============================================================================
static class EditorCoroutineUtility
{
    class Routine
    {
        readonly Stack<IEnumerator> stack = new Stack<IEnumerator>();
        object pendingAsync;

        public Routine(IEnumerator root) { if (root != null) stack.Push(root); }

        public bool Tick()
        {
            if (pendingAsync is AsyncOperation ao)
            {
                if (!ao.isDone) return true;
                pendingAsync = null;
            }

            while (stack.Count > 0)
            {
                var top = stack.Peek();
                bool moved;
                try   { moved = top.MoveNext(); }
                catch (System.Exception ex) { Debug.LogException(ex); return false; }

                if (!moved) { stack.Pop(); continue; }

                switch (top.Current)
                {
                    case IEnumerator nested:
                        stack.Push(nested);
                        continue;
                    case AsyncOperation asyncOp:
                        pendingAsync = asyncOp;
                        return true;
                    default:
                        return true;
                }
            }
            return false;
        }
    }

    static readonly List<Routine> routines = new List<Routine>();
    static bool subscribed;

    public static void StartCoroutineOwnerless(IEnumerator routine)
    {
        if (routine == null) return;
        routines.Add(new Routine(routine));
        if (subscribed) return;
        EditorApplication.update += Update;
        subscribed = true;
    }

    static void Update()
    {
        for (int i = routines.Count - 1; i >= 0; i--)
            if (!routines[i].Tick())
                routines.RemoveAt(i);

        if (routines.Count > 0 || !subscribed) return;
        EditorApplication.update -= Update;
        subscribed = false;
    }
}

public static class RouteAssetRegistry
{
    const string RouteFolder = "Assets/_Game/Routes";

    [InitializeOnLoadMethod]
    static void QueueRepair() => EditorApplication.delayCall += () => RepairImportedRoutes(false);

    [MenuItem("Tools/RealBus/OSM/Repair Route-City Links")]
    public static void RepairFromMenu()
    {
        int count = RepairImportedRoutes(true);
        EditorUtility.DisplayDialog("RealBus routes", $"Linked {count} valid routes to matching cities.", "OK");
    }

    public static int RepairImportedRoutes(bool logResult)
    {
        CityDefinition[] cities = AssetDatabase.FindAssets("t:CityDefinition", new[] { "Assets/_Game/ScriptableObjects/Cities" })
            .Select(guid => AssetDatabase.LoadAssetAtPath<CityDefinition>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(city => city != null).ToArray();
        if (cities.Length == 0) return 0;

        var discovered = new Dictionary<CityDefinition, List<BusRoute>>();
        foreach (CityDefinition city in cities) discovered[city] = new List<BusRoute>();

        foreach (string guid in AssetDatabase.FindAssets("t:BusRoute", new[] { RouteFolder }))
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (path.IndexOf("/OSMPreviews/", System.StringComparison.OrdinalIgnoreCase) >= 0) continue;
            BusRoute route = AssetDatabase.LoadAssetAtPath<BusRoute>(path);
            if (route == null) continue;
            route.EnsureRuntimeData();
            if (route.GetStopCount() < 2 || route.distanceKm <= .001f) continue;
            CityDefinition city = FindCity(route, cities);
            if (city == null) continue;

            bool changed = false;
            if (!string.Equals(route.sourceCityCode, city.cityCode, System.StringComparison.OrdinalIgnoreCase))
            {
                route.sourceCityCode = city.cityCode;
                changed = true;
            }
            if (string.IsNullOrWhiteSpace(route.generatedRouteId))
            {
                route.generatedRouteId = "asset:" + guid;
                changed = true;
            }
            if (changed) EditorUtility.SetDirty(route);
            discovered[city].Add(route);
        }

        int linked = 0;
        foreach (var pair in discovered)
        {
            var merged = new List<BusRoute>();
            if (pair.Key.availableRoutes != null) merged.AddRange(pair.Key.availableRoutes.Where(IsUsable));
            merged.AddRange(pair.Value);
            BusRoute[] unique = merged.GroupBy(BuildRouteSignature, System.StringComparer.OrdinalIgnoreCase)
                .Select(group => group.OrderBy(route => AssetDatabase.GetAssetPath(route), System.StringComparer.OrdinalIgnoreCase).First())
                .OrderBy(route => route.routeNumber, System.StringComparer.OrdinalIgnoreCase)
                .ThenBy(route => route.routeName, System.StringComparer.OrdinalIgnoreCase).ToArray();
            if (!SameRoutes(pair.Key.availableRoutes, unique))
            {
                pair.Key.availableRoutes = unique;
                EditorUtility.SetDirty(pair.Key);
            }
            linked += unique.Length;
        }

        AssetDatabase.SaveAssets();
        if (logResult) Debug.Log($"[RouteAssetRegistry] Linked {linked} routes across {cities.Length} cities.");
        return linked;
    }

    static CityDefinition FindCity(BusRoute route, CityDefinition[] cities)
    {
        if (route.stops == null || route.stops.Length == 0 || route.stops[0] == null) return null;
        BusStopData anchor = route.stops[0];
        CityDefinition best = null;
        double bestDistance = double.MaxValue;
        foreach (CityDefinition city in cities)
        {
            if (anchor.latitude < city.minLat || anchor.latitude > city.maxLat ||
                anchor.longitude < city.minLon || anchor.longitude > city.maxLon) continue;
            double distance = System.Math.Abs(anchor.latitude - city.centreLat) + System.Math.Abs(anchor.longitude - city.centreLon);
            if (distance < bestDistance) { best = city; bestDistance = distance; }
        }
        return best;
    }

    static bool IsUsable(BusRoute route)
    {
        if (route == null) return false;
        route.EnsureRuntimeData();
        return route.GetStopCount() >= 2 && route.distanceKm > .001f;
    }

    static string BuildRouteSignature(BusRoute route)
    {
        BusStopData first = route.stops != null && route.stops.Length > 0 ? route.stops[0] : null;
        BusStopData last = route.stops != null && route.stops.Length > 0 ? route.stops[route.stops.Length - 1] : null;
        return string.Join("|", route.routeNumber ?? "", route.GetStopCount().ToString(),
            Coordinate(first, true), Coordinate(first, false), Coordinate(last, true), Coordinate(last, false));
    }

    static string Coordinate(BusStopData stop, bool latitude) => stop == null ? "0" :
        (latitude ? stop.latitude : stop.longitude).ToString("F5", System.Globalization.CultureInfo.InvariantCulture);

    static bool SameRoutes(BusRoute[] current, BusRoute[] next)
    {
        if (current == null || current.Length != next.Length) return false;
        for (int i = 0; i < next.Length; i++) if (current[i] != next[i]) return false;
        return true;
    }
}
#endif
