using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Navigable road graph built from OSM way data (section 3.6).
///
/// Improvements vs original:
///   • Spatial grid index — <see cref="FindNearestNodeIndex"/> and
///     <see cref="TryProjectToNearestSegment"/> run in sub-linear time for
///     large graphs (Nairobi = ~40 000 nodes).
///   • A* cost uses travel-time (distance / speed) so routing prefers fast
///     roads over slow residential streets.
///   • One-way road support via <c>oneway</c> OSM tag.
///   • <see cref="BuildFromOverpassWays"/> calls <see cref="OverpassResponse.Resolve"/>
///     so it works with both <c>out geom</c> and <c>out body;>;out skel</c>
///     responses.
/// </summary>
public class RoadGraph
{
    // ── Enums & data classes ───────────────────────────────────────────

    public enum RoadType
    {
        Motorway,
        Trunk,
        Primary,
        Secondary,
        Tertiary,
        Residential,
        Service,
        Other
    }

    public class Node
    {
        public int     index;
        public double  lat;
        public double  lon;
        public Vector3 world;
        public List<Edge> edges = new(4);
    }

    public class Edge
    {
        public int      from;
        public int      to;
        public float    speedLimitKmh;
        public RoadType roadType;
        /// <summary>Pre-computed travel cost in seconds.</summary>
        public float    travelTimeSec;
        // OSM lane data is carried to runtime traffic instead of being guessed.
        public int      laneCount;
        public float    laneWidth;
        public bool     oneWay;
        /// <summary>True when the way has a single lane in total, so both directions
        /// use the same centred lane instead of inventing a half each.</summary>
        public bool     sharedLane;
        /// <summary>True when the OSM way is tagged <c>bridge=yes</c>. The deck
        /// elevation must be interpolated between structure ends, not sampled
        /// from the DEM (which sees the ground underneath).</summary>
        public bool     isBridge;
        /// <summary>True when the OSM way is tagged <c>tunnel=yes</c>. Same
        /// handling as bridges: interpolate between the portal elevations.</summary>
        public bool     isTunnel;
        /// <summary>Horizontal length of the edge in metres.</summary>
        public float    lengthMeters;
        /// <summary>Baked grade profile (section 3.3): world-space Y elevations
        /// sampled from <c>from</c> to <c>to</c> at
        /// <see cref="GradeProfile.intervalMeters"/>, smoothed and clamped.
        /// Null until <see cref="RoadGradeBaker.Bake"/> has run.</summary>
        public GradeProfile gradeProfile;
    }

    // ── Storage ────────────────────────────────────────────────────────

    public readonly List<Node> nodes = new();
    float fastestSpeedMs = 25f;
    /// <summary>Baked elevation per node (world-space Y), aligned with <see cref="nodes"/>.</summary>
    public float[] nodeElevation;
    readonly Dictionary<long, int> osmNodeIndex = new();

    // Deduplication key: quantised lat/lon → node index.
    readonly Dictionary<(long, long), int> _coordIndex = new();

    // Spatial grid for fast nearest-node lookups.
    readonly Dictionary<(int, int), List<int>> _spatialGrid = new();
    const float GridCellSize = 100f; // metres per grid cell

    // ── Public API ─────────────────────────────────────────────────────

    /// <summary>
    /// Finds the nearest graph node to <paramref name="worldPos"/>.
    /// Uses spatial grid — O(1) average for uniform distributions.
    /// </summary>
    public int FindNearestNodeIndex(Vector3 worldPos)
    {
        if (nodes.Count == 0) return -1;

        // Search expanding rings of grid cells until we find a candidate.
        int cx = Mathf.FloorToInt(worldPos.x / GridCellSize);
        int cz = Mathf.FloorToInt(worldPos.z / GridCellSize);

        int best    = -1;
        float bestSqr = float.MaxValue;

        for (int radius = 0; radius <= 8; radius++)
        {
            SearchGridRing(cx, cz, radius, worldPos, ref best, ref bestSqr);
            // Stop expanding once we've checked at least one full ring beyond a hit.
            if (best >= 0 && radius > 0)
            {
                float ringEdgeDist = (radius - 1) * GridCellSize;
                if (bestSqr <= ringEdgeDist * ringEdgeDist)
                    break;
            }
        }

        return best;
    }

    /// <summary>
    /// Projects <paramref name="worldPos"/> onto the nearest road segment.
    /// Returns false if the graph has fewer than 2 nodes.
    /// </summary>
    public bool TryProjectToNearestSegment(
        Vector3 worldPos,
        out Vector3 projected,
        out int segA, out int segB,
        out float distanceMeters)
    {
        projected      = Vector3.zero;
        segA = segB    = -1;
        distanceMeters = float.MaxValue;

        if (nodes.Count < 2) return false;

        // Inspect directed edges, including incoming-only segments and long segments
        // whose endpoints are not the nearest node to the stop.
        for (int a = 0; a < nodes.Count; a++)
        foreach (var edge in nodes[a].edges)
        {
            int b = edge.to;
            Vector3 proj = ClosestPointOnSegment(worldPos, nodes[a].world, nodes[b].world);
            float distance = Vector3.Distance(worldPos, proj);
            if (distance < distanceMeters)
            {
                distanceMeters = distance;
                projected = proj;
                segA = a;
                segB = b;
            }
        }

        return segA >= 0;
    }

    /// <summary>
    /// A* shortest path using travel-time as the cost (not raw distance).
    /// This routes the bus onto faster roads when available.
    /// </summary>
    public List<int> FindPathAStar(int startNode, int goalNode)
    {
        var path = new List<int>();
        if (!ValidNode(startNode) || !ValidNode(goalNode)) return path;
        if (startNode == goalNode) { path.Add(startNode); return path; }

        int n = nodes.Count;

        var cameFrom = new int[n];
        var gScore   = new float[n];
        var fScore   = new float[n];
        var inOpen   = new bool[n];
        var closed   = new bool[n];

        for (int i = 0; i < n; i++)
        {
            cameFrom[i] = -1;
            gScore[i]   = float.PositiveInfinity;
            fScore[i]   = float.PositiveInfinity;
        }

        var open = new MinHeap();
        gScore[startNode] = 0f;
        fScore[startNode] = Heuristic(startNode, goalNode);
        open.Push(startNode, fScore[startNode]);
        inOpen[startNode] = true;

        while (open.Count > 0)
        {
            int cur = open.PopMin();
            inOpen[cur] = false;

            if (cur == goalNode)
                return ReconstructPath(cameFrom, cur);

            closed[cur] = true;

            foreach (var edge in nodes[cur].edges)
            {
                int nb = edge.to;
                if (!ValidNode(nb) || closed[nb]) continue;

                float tentG = gScore[cur] + edge.travelTimeSec;
                if (tentG >= gScore[nb]) continue;

                cameFrom[nb] = cur;
                gScore[nb]   = tentG;
                fScore[nb]   = tentG + Heuristic(nb, goalNode);

                if (!inOpen[nb])
                {
                    open.Push(nb, fScore[nb]);
                    inOpen[nb] = true;
                }
                else
                {
                    open.DecreaseKey(nb, fScore[nb]);
                }
            }
        }

        return path; // empty = no path found
    }

    // ── Build from Overpass ────────────────────────────────────────────

    public static RoadGraph BuildFromOverpassWays(
        OverpassResponse response,
        CoordinateConverter converter)
    {
        var graph = new RoadGraph();
        if (response == null) return graph;

        // Resolve node positions if this is a body+skel response.
        response.Resolve();

        foreach (var elem in response.elements)
        {
            if (elem?.type != "way" || !elem.tags.ContainsKey("highway")) continue;
            if (elem.geometry == null || elem.geometry.Count < 2) continue;

            var  roadType = ParseRoadType(elem.tags);
            float speed   = ParseSpeedLimit(elem.tags, roadType);
            bool oneWay   = IsOneWay(elem.tags, roadType);
            int totalLanes = ParseLaneCount(elem.tags, roadType, oneWay);
            float laneWidth = ParseLaneWidth(elem.tags, totalLanes, roadType);
            // Each directed link carries the lane count of its own direction. Using
            // half the total for both directions was only right on a symmetric road:
            // a 3-lane way has 1 lane forward and 2 backward, and the extra lane made
            // the two flows overlap on the backward half.
            int forwardLanes = ParseForwardLaneCount(elem.tags, totalLanes, oneWay);
            int backwardLanes = oneWay ? totalLanes : Mathf.Max(1, totalLanes - forwardLanes);
            // A two-way way tagged as a single lane has nowhere to put two
            // carriageways, so its traffic stays centred rather than being pushed
            // half a lane off the asphalt.
            bool sharedLane = !oneWay && totalLanes <= 1;

            int prev = -1;
            for (int pointIndex = 0; pointIndex < elem.geometry.Count; pointIndex++)
            {
                var p = elem.geometry[pointIndex];
                long nodeId = elem.nodeRefs.Count == elem.geometry.Count ? elem.nodeRefs[pointIndex] : 0;
                int n = graph.GetOrCreateNode(p.lat, p.lon, converter, nodeId);
                if (prev >= 0 && n != prev)
                {
                    bool isBridge = elem.tags.TryGetValue("bridge", out var bridge) && bridge == "yes";
                    bool isTunnel = elem.tags.TryGetValue("tunnel", out var tunnel) && tunnel == "yes";
                    bool reverse = elem.tags.TryGetValue("oneway", out var direction) && direction == "-1";
                    graph.AddEdge(reverse ? n : prev, reverse ? prev : n, speed, roadType,
                        oneWay ? totalLanes : forwardLanes, laneWidth, oneWay, sharedLane, isBridge, isTunnel);
                    if (!oneWay)
                        graph.AddEdge(n, prev, speed, roadType, backwardLanes, laneWidth, false, sharedLane, isBridge, isTunnel);
                }
                prev = n;
            }
        }

        return graph;
    }

    // ── Private: node management ───────────────────────────────────────

    int GetOrCreateNode(double lat, double lon, CoordinateConverter converter, long osmId = 0)
    {
        long qLat = (long)System.Math.Round(lat * 1e7);
        long qLon = (long)System.Math.Round(lon * 1e7);
        var  key  = (qLat, qLon);

        if (osmId != 0 && osmNodeIndex.TryGetValue(osmId, out int existing)) return existing;
        if (osmId == 0 && _coordIndex.TryGetValue(key, out int idx)) return idx;

        var world = converter != null
            ? converter.GeoToWorldPosition(lat, lon)
            : CoordinateConverter.LocalOriginGeoToWorld(lat, lon, 0, 0);

        var node = new Node
        {
            index = nodes.Count,
            lat   = lat,
            lon   = lon,
            world = world
        };
        nodes.Add(node);
        if (osmId != 0) osmNodeIndex[osmId] = node.index;
        else _coordIndex[key] = node.index;
        AddToSpatialGrid(node);
        return node.index;
    }

    void AddToSpatialGrid(Node node)
    {
        int cx = Mathf.FloorToInt(node.world.x / GridCellSize);
        int cz = Mathf.FloorToInt(node.world.z / GridCellSize);
        var cell = (cx, cz);
        if (!_spatialGrid.TryGetValue(cell, out var list))
        {
            list = new List<int>(4);
            _spatialGrid[cell] = list;
        }
        list.Add(node.index);
    }

    void SearchGridRing(int cx, int cz, int radius, Vector3 target,
                        ref int bestIdx, ref float bestSqr)
    {
        int lo = -radius, hi = radius;
        for (int dx = lo; dx <= hi; dx++)
        {
            for (int dz = lo; dz <= hi; dz++)
            {
                if (Mathf.Abs(dx) != radius && Mathf.Abs(dz) != radius) continue; // ring only
                if (!_spatialGrid.TryGetValue((cx + dx, cz + dz), out var list)) continue;
                foreach (int ni in list)
                {
                    float sqr = (nodes[ni].world - target).sqrMagnitude;
                    if (sqr < bestSqr) { bestSqr = sqr; bestIdx = ni; }
                }
            }
        }
    }

    // ── Private: edge management ───────────────────────────────────────

    void AddEdge(int from, int to, float speedKmh, RoadType type, int laneCount, float laneWidth,
        bool oneWay, bool sharedLane = false, bool isBridge = false, bool isTunnel = false)
    {
        if (!ValidNode(from) || !ValidNode(to)) return;

        float distM       = Vector3.Distance(nodes[from].world, nodes[to].world);
        float speedMs     = Mathf.Max(1f, speedKmh) / 3.6f;
        fastestSpeedMs = Mathf.Max(fastestSpeedMs, speedMs);
        float travelSec   = distM / speedMs;

        nodes[from].edges.Add(new Edge
        {
            from          = from,
            to            = to,
            speedLimitKmh = speedKmh,
            roadType      = type,
            travelTimeSec = travelSec,
            laneCount     = Mathf.Clamp(laneCount, 1, 4),
            laneWidth     = Mathf.Clamp(laneWidth, 2.6f, 4.2f),
            oneWay        = oneWay,
            sharedLane    = sharedLane,
            isBridge      = isBridge,
            isTunnel      = isTunnel,
            lengthMeters  = distM
        });
    }

    bool ValidNode(int i) => i >= 0 && i < nodes.Count;

    // ── Private: OSM tag parsing ───────────────────────────────────────

    static RoadType ParseRoadType(Dictionary<string, string> tags)
    {
        if (tags == null || !tags.TryGetValue("highway", out var hwy) || string.IsNullOrEmpty(hwy))
            return RoadType.Other;

        return hwy switch
        {
            "motorway"      => RoadType.Motorway,
            "trunk"         => RoadType.Trunk,
            "primary"       => RoadType.Primary,
            "secondary"     => RoadType.Secondary,
            "tertiary"      => RoadType.Tertiary,
            "residential"   => RoadType.Residential,
            "unclassified"  => RoadType.Residential,
            "service"       => RoadType.Service,
            _               => RoadType.Other
        };
    }

    static float ParseSpeedLimit(Dictionary<string, string> tags, RoadType type)
    {
        if (tags != null && tags.TryGetValue("maxspeed", out var ms) && !string.IsNullOrEmpty(ms))
        {
            string s   = ms.Trim().ToLowerInvariant();
            bool   mph = s.Contains("mph");
            s = s.Replace("mph", "").Replace("km/h", "").Replace("kph", "").Trim();
            if (float.TryParse(s,
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out float v))
                return mph ? v * 1.60934f : v;
        }

        return type switch
        {
            RoadType.Motorway    => 90f,
            RoadType.Trunk       => 80f,
            RoadType.Primary     => 60f,
            RoadType.Secondary   => 50f,
            RoadType.Tertiary    => 40f,
            RoadType.Residential => 30f,
            RoadType.Service     => 20f,
            _                    => 35f
        };
    }

    static bool IsOneWay(Dictionary<string, string> tags, RoadType type)
    {
        if (tags == null) return false;
        if (tags.TryGetValue("oneway", out var ow))
            return ow == "yes" || ow == "1" || ow == "true" || ow == "-1";
        // Motorways and trunk roads are implicitly one-way per carriageway in OSM.
        return type == RoadType.Motorway || (tags.TryGetValue("junction", out var junction) && junction == "roundabout");
    }

    static int ParseLaneCount(Dictionary<string, string> tags, RoadType type, bool oneWay)
    {
        if (tags != null)
        {
            if (tags.TryGetValue("lanes", out string raw) && int.TryParse(raw, out int lanes))
                return Mathf.Clamp(lanes, 1, 8);

            int forward = 0;
            int backward = 0;
            bool hasForward = tags.TryGetValue("lanes:forward", out var fwdRaw) && int.TryParse(fwdRaw, out forward);
            bool hasBackward = tags.TryGetValue("lanes:backward", out var bwdRaw) && int.TryParse(bwdRaw, out backward);
            if (hasForward || hasBackward)
            {
                int total = Mathf.Max(0, forward) + Mathf.Max(0, backward);
                if (total > 0)
                    return Mathf.Clamp(total, 1, 8);
            }
        }

        return type switch
        {
            RoadType.Motorway or RoadType.Trunk => oneWay ? 2 : 4,
            RoadType.Primary or RoadType.Secondary => 2,
            RoadType.Service => 1,
            _ => 2
        };
    }

    /// <summary>
    /// Lanes available in the way's authored direction. Mirrors OSMWay so the road
    /// mesh's markings and the traffic graph place the same number of lanes on the
    /// same side of the centre line.
    /// </summary>
    static int ParseForwardLaneCount(Dictionary<string, string> tags, int totalLanes, bool oneWay)
    {
        if (oneWay) return Mathf.Max(1, totalLanes);
        if (tags != null && tags.TryGetValue("lanes:forward", out var raw) && int.TryParse(raw, out int forward))
            return Mathf.Clamp(forward, 1, Mathf.Max(1, totalLanes));
        return Mathf.Max(1, totalLanes / 2);
    }

    static float ParseLaneWidth(Dictionary<string, string> tags, int lanes, RoadType type)
    {
        float laneWidth;
        if (tags != null && tags.TryGetValue("width", out string raw) &&
            float.TryParse(raw.ToLowerInvariant().Replace("m", "").Trim(), System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture, out float width))
            laneWidth = Mathf.Clamp(width / Mathf.Max(1, lanes), 2.6f, 4.2f);
        else
            laneWidth = type is RoadType.Motorway or RoadType.Trunk ? 3.6f : type is RoadType.Primary or RoadType.Secondary ? 3.3f : 3.1f;

        // Keep graph lane spacing equal to the asphalt the road builder draws.
        return RoadWidthPolicy.Fit(laneWidth);
    }

    // ── Private: A* helpers ────────────────────────────────────────────

    float Heuristic(int a, int b)
    {
        // Admissible: straight-line distance / fastest road speed (90 km/h = 25 m/s).
        float dist = Vector3.Distance(nodes[a].world, nodes[b].world);
        return dist / fastestSpeedMs;
    }

    static List<int> ReconstructPath(int[] cameFrom, int current)
    {
        var result = new List<int> { current };
        while (cameFrom[current] >= 0)
        {
            current = cameFrom[current];
            result.Add(current);
        }
        result.Reverse();
        return result;
    }

    static Vector3 ClosestPointOnSegment(Vector3 p, Vector3 a, Vector3 b)
    {
        Vector3 ab = b - a;
        float   t  = Vector3.Dot(p - a, ab) / Mathf.Max(1e-6f, ab.sqrMagnitude);
        return a + ab * Mathf.Clamp01(t);
    }

    // ── Inner: binary min-heap ─────────────────────────────────────────

    class MinHeap
    {
        struct Item { public int node; public float priority; }
        readonly List<Item>             _heap = new();
        readonly Dictionary<int, int>   _pos  = new();

        public int Count => _heap.Count;

        public void Push(int node, float priority)
        {
            _heap.Add(new Item { node = node, priority = priority });
            int i = _heap.Count - 1;
            _pos[node] = i;
            SiftUp(i);
        }

        public int PopMin()
        {
            int root = _heap[0].node;
            Swap(0, _heap.Count - 1);
            _heap.RemoveAt(_heap.Count - 1);
            _pos.Remove(root);
            if (_heap.Count > 0) SiftDown(0);
            return root;
        }

        public void DecreaseKey(int node, float p)
        {
            if (!_pos.TryGetValue(node, out int i)) return;
            if (p >= _heap[i].priority) return;
            _heap[i] = new Item { node = node, priority = p };
            SiftUp(i);
        }

        void SiftUp(int i)
        {
            while (i > 0)
            {
                int p = (i - 1) >> 1;
                if (_heap[p].priority <= _heap[i].priority) break;
                Swap(p, i); i = p;
            }
        }

        void SiftDown(int i)
        {
            while (true)
            {
                int l = (i << 1) + 1, r = l + 1, s = i;
                if (l < _heap.Count && _heap[l].priority < _heap[s].priority) s = l;
                if (r < _heap.Count && _heap[r].priority < _heap[s].priority) s = r;
                if (s == i) break;
                Swap(i, s); i = s;
            }
        }

        void Swap(int a, int b)
        {
            (_heap[a], _heap[b]) = (_heap[b], _heap[a]);
            _pos[_heap[a].node] = a;
            _pos[_heap[b].node] = b;
        }
    }
    // ── Section 3.3: baked grade profiles and city-bundle persistence ──────

    const string BundleFileName = "roads.graph.json";

    /// <summary>
    /// Writes nodes, edges and baked grade profiles to the city bundle so the
    /// runtime can rebuild the graph (and its elevations) without ever calling
    /// a DEM service.
    /// </summary>
    public bool SaveToCityBundle(CityDefinition city)
    {
        if (city == null)
        {
            Debug.LogWarning("[RoadGraph] SaveToCityBundle: no city.");
            return false;
        }

        var bundle = new RoadGraphBundle { cityCode = city.cityCode ?? "UNKNOWN" };

        for (int i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];
            bundle.nodes.Add(new BundleNode
            {
                index = i,
                lat = (float)node.lat,
                lon = (float)node.lon,
                worldX = node.world.x,
                worldY = node.world.y,
                worldZ = node.world.z,
                elevation = nodeElevation != null && i < nodeElevation.Length ? nodeElevation[i] : 0f,
            });
        }

        foreach (var node in nodes)
        foreach (var edge in node.edges)
        {
            bundle.edges.Add(new BundleEdge
            {
                from = edge.from,
                to = edge.to,
                isBridge = edge.isBridge,
                isTunnel = edge.isTunnel,
                lengthMeters = edge.lengthMeters,
                gradeProfile = edge.gradeProfile,
            });
        }

        try
        {
            RuntimeCityContentStore.Write(city, BundleFileName, JsonUtility.ToJson(bundle, true));
            Debug.Log($"[RoadGraph] Saved city bundle: {bundle.nodes.Count} nodes, {bundle.edges.Count} edges.");
            return true;
        }
        catch (Exception exception)
        {
            Debug.LogError($"[RoadGraph] Failed to save city bundle: {exception.Message}");
            return false;
        }
    }

    /// <summary>Reads the previously saved road graph bundle, or null when none exists.</summary>
    public static RoadGraphBundle LoadCityBundle(CityDefinition city)
    {
        if (city == null) return null;

        string path = RuntimeCityContentStore.ResolveReadPath(city, BundleFileName);
        if (string.IsNullOrEmpty(path) || !File.Exists(path)) return null;

        string json;
        try { json = File.ReadAllText(path); }
        catch (IOException exception)
        {
            Debug.LogWarning($"[RoadGraph] Could not read city bundle {path}: {exception.Message}");
            return null;
        }
        if (string.IsNullOrWhiteSpace(json)) return null;

        try { return JsonUtility.FromJson<RoadGraphBundle>(json); }
        catch (Exception exception)
        {
            Debug.LogWarning($"[RoadGraph] Malformed city bundle {path}: {exception.Message}");
            return null;
        }
    }

    /// <summary>
    /// Rehydrates a <see cref="RoadGraph"/> (nodes, edges, grade profiles and
    /// node elevations) from a previously saved city bundle. The result is
    /// suitable for elevation lookups; travel costs are re-estimated.
    /// </summary>
    public static RoadGraph LoadFromCityBundle(CityDefinition city, CoordinateConverter converter)
    {
        var bundle = LoadCityBundle(city);
        if (bundle == null || bundle.nodes == null || bundle.nodes.Count == 0) return null;

        var graph = new RoadGraph();
        var nodes = new Node[bundle.nodes.Count];

        for (int i = 0; i < bundle.nodes.Count; i++)
        {
            var source = bundle.nodes[i];
            Vector3 world = converter != null
                ? converter.GeoToWorldPosition(source.lat, source.lon)
                : new Vector3(source.worldX, source.worldY, source.worldZ);

            var node = new Node { index = i, lat = source.lat, lon = source.lon, world = world };
            nodes[i] = node;
            graph.nodes.Add(node);
            graph._coordIndex[((long)System.Math.Round(source.lat * 1e7),
                              (long)System.Math.Round(source.lon * 1e7))] = i;
        }

        foreach (var source in bundle.edges)
        {
            if (source.from < 0 || source.from >= nodes.Length ||
                source.to < 0 || source.to >= nodes.Length) continue;

            float distance = Vector3.Distance(nodes[source.from].world, nodes[source.to].world);
            var edge = new Edge
            {
                from = source.from,
                to = source.to,
                speedLimitKmh = 35f,
                roadType = RoadType.Other,
                travelTimeSec = distance / 10f,
                laneCount = 1,
                laneWidth = 3.5f,
                oneWay = true,
                sharedLane = false,
                isBridge = source.isBridge,
                isTunnel = source.isTunnel,
                lengthMeters = source.lengthMeters,
                gradeProfile = source.gradeProfile,
            };
            nodes[source.from].edges.Add(edge);
        }

        graph.nodeElevation = new float[bundle.nodes.Count];
        for (int i = 0; i < bundle.nodes.Count; i++)
            graph.nodeElevation[i] = bundle.nodes[i].elevation;

        graph.RebuildSpatialIndex();
        Debug.Log($"[RoadGraph] Loaded city bundle: {graph.nodes.Count} nodes, {CountDirectedEdges(graph)} edges.");
        return graph;
    }

    /// <summary>Rebuilds the spatial grid after nodes were rehydrated from a bundle.</summary>
    public void RebuildSpatialIndex()
    {
        _spatialGrid.Clear();
        for (int i = 0; i < nodes.Count; i++)
            AddToSpatialGrid(nodes[i]);
    }

    static int CountDirectedEdges(RoadGraph graph)
    {
        int count = 0;
        for (int i = 0; i < graph.nodes.Count; i++)
            count += graph.nodes[i].edges.Count;
        return count;
    }

    /// <summary>Baked grade profile for a single edge (section 3.3).</summary>
    [Serializable]
    public class GradeProfile
    {
        /// <summary>Metres between consecutive elevation samples.</summary>
        public float intervalMeters;
        /// <summary>World-space Y elevation at each sample, ordered from-node → to-node.</summary>
        public float[] elevations;
    }

    /// <summary>Serialisable snapshot of a road graph saved to the city bundle.</summary>
    [Serializable]
    public class RoadGraphBundle
    {
        public string cityCode;
        public List<BundleNode> nodes = new();
        public List<BundleEdge> edges = new();
    }

    [Serializable]
    public class BundleNode
    {
        public int index;
        // Stored as float (JsonUtility does not reliably round-trip double).
        public float lat;
        public float lon;
        public float worldX;
        public float worldY;
        public float worldZ;
        public float elevation;
    }

    [Serializable]
    public class BundleEdge
    {
        public int from;
        public int to;
        public bool isBridge;
        public bool isTunnel;
        public float lengthMeters;
        public GradeProfile gradeProfile;
    }
}