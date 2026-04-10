using System.Collections.Generic;
using UnityEngine;

public class RoadGraph
{
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
        public int index;
        public double lat;
        public double lon;
        public Vector3 world;
        public List<Edge> edges = new List<Edge>();
    }

    public class Edge
    {
        public int from;
        public int to;
        public float speedLimitKmh;
        public RoadType roadType;
    }

    public readonly List<Node> nodes = new List<Node>();
    readonly Dictionary<(long quantLat, long quantLon), int> coordToNodeIndex = new Dictionary<(long, long), int>();

    public int FindNearestNodeIndex(Vector3 worldPos)
    {
        int best = -1;
        float bestSqr = float.MaxValue;
        for (int i = 0; i < nodes.Count; i++)
        {
            float sqr = (nodes[i].world - worldPos).sqrMagnitude;
            if (sqr < bestSqr)
            {
                bestSqr = sqr;
                best = i;
            }
        }
        return best;
    }

    public bool TryProjectToNearestSegment(Vector3 worldPos, out Vector3 projectedWorld, out int segA, out int segB, out float distanceMeters)
    {
        projectedWorld = Vector3.zero;
        segA = -1;
        segB = -1;
        distanceMeters = float.MaxValue;
        if (nodes.Count < 2) return false;

        // Iterate unique undirected segments (from < to) to avoid duplicates.
        for (int i = 0; i < nodes.Count; i++)
        {
            var edges = nodes[i].edges;
            for (int e = 0; e < edges.Count; e++)
            {
                int a = edges[e].from;
                int b = edges[e].to;
                if (a >= b) continue;
                Vector3 p = nodes[a].world;
                Vector3 q = nodes[b].world;
                Vector3 proj = ClosestPointOnSegment(worldPos, p, q);
                float d = Vector3.Distance(worldPos, proj);
                if (d < distanceMeters)
                {
                    distanceMeters = d;
                    projectedWorld = proj;
                    segA = a;
                    segB = b;
                }
            }
        }

        return segA >= 0 && segB >= 0;
    }

    static Vector3 ClosestPointOnSegment(Vector3 p, Vector3 a, Vector3 b)
    {
        Vector3 ab = b - a;
        float t = Vector3.Dot(p - a, ab) / Mathf.Max(0.0001f, Vector3.Dot(ab, ab));
        t = Mathf.Clamp01(t);
        return a + ab * t;
    }

    public List<int> FindPathAStar(int startNode, int goalNode)
    {
        var path = new List<int>();
        if (startNode < 0 || goalNode < 0 || startNode >= nodes.Count || goalNode >= nodes.Count) return path;
        if (startNode == goalNode) { path.Add(startNode); return path; }

        var open = new MinHeap();
        var cameFrom = new int[nodes.Count];
        var gScore = new float[nodes.Count];
        var fScore = new float[nodes.Count];
        var inOpen = new bool[nodes.Count];
        var closed = new bool[nodes.Count];

        for (int i = 0; i < nodes.Count; i++)
        {
            cameFrom[i] = -1;
            gScore[i] = float.PositiveInfinity;
            fScore[i] = float.PositiveInfinity;
        }

        gScore[startNode] = 0f;
        fScore[startNode] = Heuristic(startNode, goalNode);
        open.Push(startNode, fScore[startNode]);
        inOpen[startNode] = true;

        while (open.Count > 0)
        {
            int current = open.PopMin();
            inOpen[current] = false;
            if (current == goalNode)
                return Reconstruct(cameFrom, current);

            closed[current] = true;

            var edges = nodes[current].edges;
            for (int i = 0; i < edges.Count; i++)
            {
                int neighbor = edges[i].to;
                if (neighbor < 0 || neighbor >= nodes.Count) continue;
                if (closed[neighbor]) continue;

                float tentative = gScore[current] + Vector3.Distance(nodes[current].world, nodes[neighbor].world);
                if (tentative < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentative;
                    fScore[neighbor] = tentative + Heuristic(neighbor, goalNode);
                    if (!inOpen[neighbor])
                    {
                        open.Push(neighbor, fScore[neighbor]);
                        inOpen[neighbor] = true;
                    }
                    else
                    {
                        open.DecreaseKey(neighbor, fScore[neighbor]);
                    }
                }
            }
        }

        return path;
    }

    float Heuristic(int a, int b)
    {
        return Vector3.Distance(nodes[a].world, nodes[b].world);
    }

    static List<int> Reconstruct(int[] cameFrom, int current)
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

    class MinHeap
    {
        struct Item { public int node; public float priority; }
        readonly List<Item> heap = new List<Item>();
        readonly Dictionary<int, int> positions = new Dictionary<int, int>();
        public int Count => heap.Count;

        public void Push(int node, float priority)
        {
            var it = new Item { node = node, priority = priority };
            heap.Add(it);
            int i = heap.Count - 1;
            positions[node] = i;
            SiftUp(i);
        }

        public int PopMin()
        {
            int minNode = heap[0].node;
            Swap(0, heap.Count - 1);
            heap.RemoveAt(heap.Count - 1);
            positions.Remove(minNode);
            if (heap.Count > 0) SiftDown(0);
            return minNode;
        }

        public void DecreaseKey(int node, float newPriority)
        {
            if (!positions.TryGetValue(node, out int i)) return;
            if (newPriority >= heap[i].priority) return;
            heap[i] = new Item { node = node, priority = newPriority };
            SiftUp(i);
        }

        void SiftUp(int i)
        {
            while (i > 0)
            {
                int p = (i - 1) / 2;
                if (heap[p].priority <= heap[i].priority) break;
                Swap(p, i);
                i = p;
            }
        }

        void SiftDown(int i)
        {
            while (true)
            {
                int l = i * 2 + 1;
                int r = l + 1;
                int smallest = i;
                if (l < heap.Count && heap[l].priority < heap[smallest].priority) smallest = l;
                if (r < heap.Count && heap[r].priority < heap[smallest].priority) smallest = r;
                if (smallest == i) break;
                Swap(i, smallest);
                i = smallest;
            }
        }

        void Swap(int a, int b)
        {
            var tmp = heap[a];
            heap[a] = heap[b];
            heap[b] = tmp;
            positions[heap[a].node] = a;
            positions[heap[b].node] = b;
        }
    }

    public static RoadGraph BuildFromOverpassWays(OverpassResponse response, CoordinateConverter converter)
    {
        var graph = new RoadGraph();
        if (response == null) return graph;

        foreach (var elem in response.elements)
        {
            if (elem == null) continue;
            if (elem.type != "way") continue;
            if (elem.geometry == null || elem.geometry.Count < 2) continue;

            var roadType = ParseRoadType(elem.tags);
            float speed = ParseSpeedLimit(elem.tags, roadType);

            int prev = -1;
            for (int i = 0; i < elem.geometry.Count; i++)
            {
                var p = elem.geometry[i];
                int n = graph.GetOrCreateNode(p.lat, p.lon, converter);
                if (prev >= 0 && n != prev)
                {
                    graph.AddBidirectional(prev, n, speed, roadType);
                }
                prev = n;
            }
        }

        return graph;
    }

    int GetOrCreateNode(double lat, double lon, CoordinateConverter converter)
    {
        // Quantize to reduce duplicates from float formatting differences.
        long qLat = (long)System.Math.Round(lat * 1e7);
        long qLon = (long)System.Math.Round(lon * 1e7);
        var key = (qLat, qLon);

        if (coordToNodeIndex.TryGetValue(key, out int idx))
            return idx;

        var node = new Node
        {
            index = nodes.Count,
            lat = lat,
            lon = lon,
            world = converter != null ? converter.GeoToWorldPosition(lat, lon) : Vector3.zero
        };
        nodes.Add(node);
        coordToNodeIndex[key] = node.index;
        return node.index;
    }

    void AddBidirectional(int a, int b, float speedLimit, RoadType type)
    {
        AddEdge(a, b, speedLimit, type);
        AddEdge(b, a, speedLimit, type);
    }

    void AddEdge(int from, int to, float speedLimit, RoadType type)
    {
        if (from < 0 || to < 0 || from >= nodes.Count || to >= nodes.Count) return;
        nodes[from].edges.Add(new Edge { from = from, to = to, speedLimitKmh = speedLimit, roadType = type });
    }

    static RoadType ParseRoadType(Dictionary<string, string> tags)
    {
        if (tags == null || !tags.TryGetValue("highway", out var hwy) || string.IsNullOrEmpty(hwy))
            return RoadType.Other;

        switch (hwy)
        {
            case "motorway": return RoadType.Motorway;
            case "trunk": return RoadType.Trunk;
            case "primary": return RoadType.Primary;
            case "secondary": return RoadType.Secondary;
            case "tertiary": return RoadType.Tertiary;
            case "residential":
            case "unclassified": return RoadType.Residential;
            case "service": return RoadType.Service;
            default: return RoadType.Other;
        }
    }

    static float ParseSpeedLimit(Dictionary<string, string> tags, RoadType type)
    {
        if (tags != null && tags.TryGetValue("maxspeed", out var ms) && !string.IsNullOrEmpty(ms))
        {
            // Common formats: "50", "50 km/h", "30 mph"
            string s = ms.ToLowerInvariant().Trim();
            bool mph = s.Contains("mph");
            s = s.Replace("km/h", "").Replace("kph", "").Replace("mph", "").Trim();
            if (float.TryParse(s, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float v))
                return mph ? v * 1.60934f : v;
        }

        // Fallback defaults by type.
        switch (type)
        {
            case RoadType.Motorway: return 90f;
            case RoadType.Trunk: return 80f;
            case RoadType.Primary: return 60f;
            case RoadType.Secondary: return 50f;
            case RoadType.Tertiary: return 40f;
            case RoadType.Residential: return 30f;
            case RoadType.Service: return 20f;
            default: return 35f;
        }
    }
}

