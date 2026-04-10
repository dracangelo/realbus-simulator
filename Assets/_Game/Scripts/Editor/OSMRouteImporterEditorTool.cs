#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class OSMRouteImporterEditorTool : EditorWindow
{
    string overpassUrl = "https://overpass-api.de/api/interpreter";
    double minLat = -1.2900, minLon = 36.8000, maxLat = -1.2630, maxLon = 36.8280;
    float clusterRadiusMeters = 350f;
    int minStops = 6;
    int maxStops = 40;
    float minKm = 3f;
    float maxKm = 25f;
    int maxRoutesToSave = 20;

    string status = "";
    RoadGraph graph;
    List<BusStop> importedStops = new List<BusStop>();
    List<RouteDraft> drafts = new List<RouteDraft>();
    CoordinateConverter converter;

    [MenuItem("Tools/RealBus/OSM/Auto-Generate Bus Routes (Overpass)")]
    public static void Open()
    {
        GetWindow<OSMRouteImporterEditorTool>("OSM Route Importer");
    }

    void OnGUI()
    {
        GUILayout.Label("Overpass bounding box", EditorStyles.boldLabel);
        minLat = EditorGUILayout.DoubleField("Min Lat", minLat);
        minLon = EditorGUILayout.DoubleField("Min Lon", minLon);
        maxLat = EditorGUILayout.DoubleField("Max Lat", maxLat);
        maxLon = EditorGUILayout.DoubleField("Max Lon", maxLon);
        overpassUrl = EditorGUILayout.TextField("Overpass URL", overpassUrl);

        GUILayout.Space(8);
        GUILayout.Label("Route generation", EditorStyles.boldLabel);
        clusterRadiusMeters = EditorGUILayout.Slider("Cluster radius (m)", clusterRadiusMeters, 200f, 400f);
        minStops = EditorGUILayout.IntField("Min stops", minStops);
        maxStops = EditorGUILayout.IntField("Max stops", maxStops);
        minKm = EditorGUILayout.FloatField("Min distance (km)", minKm);
        maxKm = EditorGUILayout.FloatField("Max distance (km)", maxKm);
        maxRoutesToSave = EditorGUILayout.IntSlider("Max routes to save", maxRoutesToSave, 10, 20);

        GUILayout.Space(8);
        if (GUILayout.Button("1) Download roads + stops"))
        {
            EditorCoroutineUtility.StartCoroutineOwnerless(DownloadRoadsAndStops());
        }
        if (GUILayout.Button("2) Cluster stops → draft routes"))
        {
            ClusterStopsIntoDraftRoutes();
        }
        if (GUILayout.Button("3) Generate paths (A*) + filter"))
        {
            GeneratePathsAndFilter();
        }
        if (GUILayout.Button("4) Save BusRoute assets (+ return routes)"))
        {
            SaveAssets();
        }

        GUILayout.Space(10);
        EditorGUILayout.HelpBox(status, MessageType.Info);

        if (drafts != null && drafts.Count > 0)
        {
            GUILayout.Label($"Draft routes: {drafts.Count}", EditorStyles.boldLabel);
            for (int i = 0; i < Mathf.Min(20, drafts.Count); i++)
            {
                GUILayout.Label($"{i + 1}. {drafts[i].name} stops={drafts[i].stops.Count} km={drafts[i].distanceKm:0.0} diff={drafts[i].difficulty}");
            }
        }
    }

    IEnumerator DownloadRoadsAndStops()
    {
        status = "Downloading roads...";
        graph = null;
        importedStops.Clear();
        drafts.Clear();

        // Coordinate converter for world projections in Editor.
        converter = FindFirstObjectByType<CoordinateConverter>();
        if (converter == null)
        {
            var go = new GameObject("CoordinateConverter_EditorTemp");
            converter = go.AddComponent<CoordinateConverter>();
            converter.mapOrigin = ScriptableObject.CreateInstance<MapOrigin>();
            converter.mapOrigin.originLat = (minLat + maxLat) * 0.5;
            converter.mapOrigin.originLon = (minLon + maxLon) * 0.5;
        }

        string roadsQuery = OverpassQueryBuilder.RoadsQuery(minLat, minLon, maxLat, maxLon);
        string roadsJson = null;
        yield return PostOverpass(roadsQuery, j => roadsJson = j);
        if (string.IsNullOrEmpty(roadsJson)) { status = "Road download failed."; yield break; }

        var roadsResponse = OverpassResponse.Deserialize(roadsJson);
        graph = RoadGraph.BuildFromOverpassWays(roadsResponse, converter);

        status = $"Road graph built: nodes={graph.nodes.Count}. Downloading bus stops...";

        // Stops query (nodes).
        string bbox = $"{minLat},{minLon},{maxLat},{maxLon}";
        string stopsQuery =
            "[out:json][timeout:60];(" +
            $"node[highway=bus_stop]({bbox});" +
            $"node[public_transport=platform]({bbox});" +
            ");out body;";

        string stopsJson = null;
        yield return PostOverpass(stopsQuery, j => stopsJson = j);
        if (string.IsNullOrEmpty(stopsJson)) { status = "Stop download failed."; yield break; }

        var stopsResponse = OverpassResponse.Deserialize(stopsJson);
        foreach (var e in stopsResponse.elements)
        {
            if (e == null || e.type != "node") continue;
            if (e.lat == 0 && e.lon == 0) continue;
            string name = e.tags != null && e.tags.TryGetValue("name", out var n) ? n : $"Stop {e.id}";
            importedStops.Add(new BusStop
            {
                stopId = e.id.ToString(),
                stopName = name,
                latitude = e.lat,
                longitude = e.lon,
                headingDegrees = 0f
            });
        }

        status = $"Downloaded stops={importedStops.Count}. Ready to cluster.";
    }

    IEnumerator PostOverpass(string query, System.Action<string> onDone)
    {
        string postData = "data=" + UnityWebRequest.EscapeURL(query);
        using (var request = new UnityWebRequest(overpassUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(postData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            request.timeout = 60;
#if UNITY_2020_1_OR_NEWER
            yield return request.SendWebRequest();
#else
            yield return request.Send();
#endif
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Overpass failed: {request.error}");
                onDone?.Invoke(null);
                yield break;
            }
            onDone?.Invoke(request.downloadHandler.text);
        }
    }

    void ClusterStopsIntoDraftRoutes()
    {
        drafts.Clear();
        if (graph == null || graph.nodes.Count == 0) { status = "No RoadGraph. Download roads first."; return; }
        if (importedStops == null || importedStops.Count == 0) { status = "No stops. Download stops first."; return; }

        status = "Snapping stops to nearest road segment...";
        var snapped = new List<SnappedStop>();
        for (int i = 0; i < importedStops.Count; i++)
        {
            var s = importedStops[i];
            Vector3 w = converter.GeoToWorldPosition(s.latitude, s.longitude);
            if (graph.TryProjectToNearestSegment(w, out var proj, out int a, out int b, out float dist))
            {
                // Update stop GPS to the projected location (invisible alignment fix).
                var gps = converter.WorldToGeoPosition(proj);
                snapped.Add(new SnappedStop
                {
                    stop = new BusStop
                    {
                        stopId = s.stopId,
                        stopName = s.stopName,
                        latitude = gps.lat,
                        longitude = gps.lon,
                        headingDegrees = s.headingDegrees
                    },
                    snappedWorld = proj,
                    nearestNode = ChooseNearestEndpoint(proj, graph.nodes[a].world, graph.nodes[b].world, a, b)
                });
            }
        }

        status = $"Snapped stops={snapped.Count}. Clustering...";
        var clusters = ClusterByRadius(snapped, clusterRadiusMeters);
        int clusterIndex = 0;
        foreach (var cluster in clusters)
        {
            if (cluster.Count < minStops) continue;
            var ordered = OrderStopsByGreedyPath(cluster);
            drafts.Add(new RouteDraft
            {
                name = $"Draft_{clusterIndex++}",
                stops = ordered
            });
        }

        status = $"Created draft corridors={drafts.Count}. Generate paths next.";
    }

    void GeneratePathsAndFilter()
    {
        if (drafts == null || drafts.Count == 0) { status = "No drafts. Cluster first."; return; }
        if (graph == null) { status = "No RoadGraph."; return; }

        status = "Pathfinding (A*) between stops...";

        foreach (var d in drafts)
        {
            d.pathWorld.Clear();
            d.distanceKm = 0f;

            if (d.stops.Count < 2) continue;

            var nodeIndices = d.stops.Select(s => s.nearestNode).ToList();
            var pathNodes = new List<int>();

            for (int i = 0; i < nodeIndices.Count - 1; i++)
            {
                var sub = graph.FindPathAStar(nodeIndices[i], nodeIndices[i + 1]);
                if (sub == null || sub.Count == 0) { pathNodes.Clear(); break; }
                if (pathNodes.Count > 0 && sub.Count > 0 && pathNodes[pathNodes.Count - 1] == sub[0])
                    sub.RemoveAt(0);
                pathNodes.AddRange(sub);
            }

            for (int i = 0; i < pathNodes.Count; i++)
                d.pathWorld.Add(graph.nodes[pathNodes[i]].world + Vector3.up * 0.1f);

            d.distanceKm = ComputeDistanceKm(d.pathWorld);
            d.estimatedTimeMinutes = EstimateTimeMinutes(d);
            d.difficulty = ClassifyDifficulty(d);
        }

        // Filter pass.
        drafts = drafts
            .Where(d => d.stops.Count >= minStops && d.stops.Count <= maxStops)
            .Where(d => d.distanceKm >= minKm && d.distanceKm <= maxKm)
            .Where(d => d.pathWorld.Count >= 2)
            .OrderByDescending(d => d.stops.Count)
            .ThenBy(d => Mathf.Abs(12f - d.distanceKm)) // bias mid-length
            .Take(Mathf.Clamp(maxRoutesToSave, 10, 20))
            .ToList();

        status = $"Filtered usable routes={drafts.Count}. Ready to save (+return variants).";
    }

    void SaveAssets()
    {
        if (drafts == null || drafts.Count == 0) { status = "No routes to save."; return; }

        const string folder = "Assets/_Game/Routes";
        if (!AssetDatabase.IsValidFolder("Assets/_Game"))
            AssetDatabase.CreateFolder("Assets", "_Game");
        if (!AssetDatabase.IsValidFolder(folder))
            AssetDatabase.CreateFolder("Assets/_Game", "Routes");

        int saved = 0;
        foreach (var d in drafts)
        {
            saved += SaveOne(folder, d, isReturn: false);

            // Return variant.
            var ret = d.MakeReturnVariant();
            // Re-run pathfinding for return variant.
            ret.pathWorld.Clear();
            var nodeIndices = ret.stops.Select(s => s.nearestNode).ToList();
            var pathNodes = new List<int>();
            for (int i = 0; i < nodeIndices.Count - 1; i++)
            {
                var sub = graph.FindPathAStar(nodeIndices[i], nodeIndices[i + 1]);
                if (sub == null || sub.Count == 0) { pathNodes.Clear(); break; }
                if (pathNodes.Count > 0 && pathNodes[pathNodes.Count - 1] == sub[0])
                    sub.RemoveAt(0);
                pathNodes.AddRange(sub);
            }
            for (int i = 0; i < pathNodes.Count; i++)
                ret.pathWorld.Add(graph.nodes[pathNodes[i]].world + Vector3.up * 0.1f);
            ret.distanceKm = ComputeDistanceKm(ret.pathWorld);
            ret.estimatedTimeMinutes = EstimateTimeMinutes(ret);
            ret.difficulty = ClassifyDifficulty(ret);

            // Filter return too.
            if (ret.stops.Count >= minStops && ret.stops.Count <= maxStops && ret.distanceKm >= minKm && ret.distanceKm <= maxKm && ret.pathWorld.Count >= 2)
                saved += SaveOne(folder, ret, isReturn: true);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        status = $"Saved BusRoute assets: {saved}";
    }

    int SaveOne(string folder, RouteDraft d, bool isReturn)
    {
        var asset = ScriptableObject.CreateInstance<BusRoute>();
        asset.routeName = isReturn ? $"{d.name} (Return)" : d.name;
        asset.routeNumber = d.routeRef ?? "";

        asset.busStops = d.stops.Select(s => s.stop).ToArray();
        asset.pathPoints = d.pathWorld.ToArray();
        asset.distanceKm = d.distanceKm;
        asset.estimatedTimeMinutes = d.estimatedTimeMinutes;
        asset.difficulty = d.difficulty;

        // Save geometry lat/lon flat too (optional).
        var flat = new List<double>();
        for (int i = 0; i < d.pathWorld.Count; i++)
        {
            var gps = converter.WorldToGeoPosition(d.pathWorld[i]);
            flat.Add(gps.lat);
            flat.Add(gps.lon);
        }
        asset.geometryLatLonFlat = flat.ToArray();

        string safe = MakeSafeFileName(asset.routeName);
        string path = AssetDatabase.GenerateUniqueAssetPath($"{folder}/{safe}.asset");
        AssetDatabase.CreateAsset(asset, path);
        return 1;
    }

    static string MakeSafeFileName(string s)
    {
        foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            s = s.Replace(c, '_');
        return s.Replace(" ", "_");
    }

    // --- helpers ---

    static int ChooseNearestEndpoint(Vector3 p, Vector3 a, Vector3 b, int ai, int bi)
    {
        return (p - a).sqrMagnitude <= (p - b).sqrMagnitude ? ai : bi;
    }

    static float ComputeDistanceKm(List<Vector3> pts)
    {
        if (pts == null || pts.Count < 2) return 0f;
        float meters = 0f;
        for (int i = 0; i < pts.Count - 1; i++)
            meters += Vector3.Distance(pts[i], pts[i + 1]);
        return meters / 1000f;
    }

    float EstimateTimeMinutes(RouteDraft d)
    {
        // Conservative estimate: use 28km/h urban average and add dwell.
        float avgKmh = d.difficulty >= 4 ? 22f : d.difficulty == 3 ? 28f : 40f;
        float driveMin = (d.distanceKm / Mathf.Max(5f, avgKmh)) * 60f;
        float dwellMin = d.stops.Count * 0.35f;
        return driveMin + dwellMin;
    }

    int ClassifyDifficulty(RouteDraft d)
    {
        // Heuristic:
        // - highway-heavy => 2
        // - dense short segments / many stops => 4
        // - otherwise => 3
        if (d.pathWorld.Count < 2) return 3;
        if (d.distanceKm > 18f && d.stops.Count < 12) return 2;
        if (d.stops.Count >= 20 && d.distanceKm < 12f) return 4;
        return 3;
    }

    static List<List<SnappedStop>> ClusterByRadius(List<SnappedStop> stops, float radiusMeters)
    {
        var clusters = new List<List<SnappedStop>>();
        if (stops == null || stops.Count == 0) return clusters;

        float r2 = radiusMeters * radiusMeters;
        var visited = new bool[stops.Count];

        for (int i = 0; i < stops.Count; i++)
        {
            if (visited[i]) continue;
            visited[i] = true;
            var cluster = new List<SnappedStop> { stops[i] };

            // BFS expansion.
            for (int q = 0; q < cluster.Count; q++)
            {
                var s = cluster[q];
                for (int j = 0; j < stops.Count; j++)
                {
                    if (visited[j]) continue;
                    if ((stops[j].snappedWorld - s.snappedWorld).sqrMagnitude <= r2)
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
        // Simple ordering: start from the most isolated (farthest from centroid), then nearest-neighbor chain.
        Vector3 centroid = Vector3.zero;
        for (int i = 0; i < cluster.Count; i++) centroid += cluster[i].snappedWorld;
        centroid /= Mathf.Max(1, cluster.Count);

        int start = 0;
        float best = -1f;
        for (int i = 0; i < cluster.Count; i++)
        {
            float d = (cluster[i].snappedWorld - centroid).sqrMagnitude;
            if (d > best) { best = d; start = i; }
        }

        var remaining = new List<SnappedStop>(cluster);
        var ordered = new List<SnappedStop>();
        var current = remaining[start];
        ordered.Add(current);
        remaining.RemoveAt(start);

        while (remaining.Count > 0)
        {
            int bestIdx = 0;
            float bestD = float.MaxValue;
            for (int i = 0; i < remaining.Count; i++)
            {
                float d = (remaining[i].snappedWorld - current.snappedWorld).sqrMagnitude;
                if (d < bestD) { bestD = d; bestIdx = i; }
            }
            current = remaining[bestIdx];
            ordered.Add(current);
            remaining.RemoveAt(bestIdx);
        }

        return ordered;
    }

    class RouteDraft
    {
        public string name;
        public string routeRef;
        public List<SnappedStop> stops = new List<SnappedStop>();
        public List<Vector3> pathWorld = new List<Vector3>();
        public float distanceKm;
        public float estimatedTimeMinutes;
        public int difficulty = 3;

        public RouteDraft MakeReturnVariant()
        {
            var r = new RouteDraft();
            r.name = name;
            r.routeRef = routeRef;
            r.stops = new List<SnappedStop>(stops);
            r.stops.Reverse();
            return r;
        }
    }

    class SnappedStop
    {
        public BusStop stop;
        public Vector3 snappedWorld;
        public int nearestNode;
    }
}

// Minimal editor coroutine wrapper for compatibility.
static class EditorCoroutineUtility
{
    class Runner : EditorWindow
    {
        readonly List<IEnumerator> routines = new List<IEnumerator>();
        void Update()
        {
            for (int i = routines.Count - 1; i >= 0; i--)
            {
                if (!routines[i].MoveNext())
                    routines.RemoveAt(i);
            }
        }
        public void StartRoutine(IEnumerator r) => routines.Add(r);
    }

    static Runner runner;
    public static void StartCoroutineOwnerless(IEnumerator routine)
    {
        if (runner == null)
            runner = ScriptableObject.CreateInstance<Runner>();
        runner.StartRoutine(routine);
    }
}
#endif

