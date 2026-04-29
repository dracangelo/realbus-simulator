using System.Collections.Generic;
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
    }

    // ── Storage ────────────────────────────────────────────────────────

    public readonly List<Node> nodes = new();

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

        // Start with the nearest node; only check its edges (and two rings of
        // neighbours) rather than the entire graph — fast enough for real-time use.
        int nearest = FindNearestNodeIndex(worldPos);
        if (nearest < 0) return false;

        var candidates = new HashSet<int> { nearest };
        foreach (var e in nodes[nearest].edges)
        {
            candidates.Add(e.to);
            foreach (var e2 in nodes[e.to].edges)
                candidates.Add(e2.to);
        }

        foreach (int a in candidates)
        {
            foreach (var edge in nodes[a].edges)
            {
                int b = edge.to;
                if (b <= a) continue; // undirected: check each pair once

                Vector3 p    = nodes[a].world;
                Vector3 q    = nodes[b].world;
                Vector3 proj = ClosestPointOnSegment(worldPos, p, q);
                float d      = Vector3.Distance(worldPos, proj);

                if (d < distanceMeters)
                {
                    distanceMeters = d;
                    projected      = proj;
                    segA           = a;
                    segB           = b;
                }
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
            if (elem?.type != "way") continue;
            if (elem.geometry == null || elem.geometry.Count < 2) continue;

            var  roadType = ParseRoadType(elem.tags);
            float speed   = ParseSpeedLimit(elem.tags, roadType);
            bool oneWay   = IsOneWay(elem.tags, roadType);

            int prev = -1;
            foreach (var p in elem.geometry)
            {
                int n = graph.GetOrCreateNode(p.lat, p.lon, converter);
                if (prev >= 0 && n != prev)
                {
                    graph.AddEdge(prev, n, speed, roadType);
                    if (!oneWay)
                        graph.AddEdge(n, prev, speed, roadType);
                }
                prev = n;
            }
        }

        return graph;
    }

    // ── Private: node management ───────────────────────────────────────

    int GetOrCreateNode(double lat, double lon, CoordinateConverter converter)
    {
        long qLat = (long)System.Math.Round(lat * 1e7);
        long qLon = (long)System.Math.Round(lon * 1e7);
        var  key  = (qLat, qLon);

        if (_coordIndex.TryGetValue(key, out int idx))
            return idx;

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
        _coordIndex[key] = node.index;
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

    void AddEdge(int from, int to, float speedKmh, RoadType type)
    {
        if (!ValidNode(from) || !ValidNode(to)) return;

        float distM       = Vector3.Distance(nodes[from].world, nodes[to].world);
        float speedMs     = Mathf.Max(1f, speedKmh) / 3.6f;
        float travelSec   = distM / speedMs;

        nodes[from].edges.Add(new Edge
        {
            from          = from,
            to            = to,
            speedLimitKmh = speedKmh,
            roadType      = type,
            travelTimeSec = travelSec
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
            return ow == "yes" || ow == "1" || ow == "true";
        // Motorways and trunk roads are implicitly one-way per carriageway in OSM.
        return type == RoadType.Motorway || type == RoadType.Trunk;
    }

    // ── Private: A* helpers ────────────────────────────────────────────

    float Heuristic(int a, int b)
    {
        // Admissible: straight-line distance / fastest road speed (90 km/h = 25 m/s).
        float dist = Vector3.Distance(nodes[a].world, nodes[b].world);
        return dist / 25f;
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
}
