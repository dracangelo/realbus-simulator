using System.Collections.Generic;
using UnityEngine;

/// <summary>Routes snapped stops along directed graph edges. Rejects disconnected legs.</summary>
public static class RoadRoutePathBuilder
{
    struct Snap
    {
        public Vector3 point;
        public int a, b;
        public float t;
    }

    public static bool TryBuild(BusRouteParser.ParsedBusRoute route, RoadGraph graph,
        CoordinateConverter converter, float maxSnapDistance)
    {
        if (route == null || graph == null || converter == null || route.stops.Count < 2) return false;
        var snaps = new List<Snap>();
        foreach (var stop in route.stops)
        {
            Vector3 position = converter.GeoToWorldPosition(stop.latitude, stop.longitude);
            if (!graph.TryProjectToNearestSegment(position, out var projected, out int a, out int b, out float distance)
                || distance > maxSnapDistance) return false;
            Vector3 delta = graph.nodes[b].world - graph.nodes[a].world;
            snaps.Add(new Snap { point = projected, a = a, b = b,
                t = Vector3.Dot(projected - graph.nodes[a].world, delta) / Mathf.Max(0.0001f, delta.sqrMagnitude) });
        }
        var points = new List<Vector3>();
        for (int i = 1; i < snaps.Count; i++)
        {
            Snap from = snaps[i - 1], to = snaps[i];
            List<Vector3> best = null;
            float bestLength = float.MaxValue;
            // Direct travel along the same oriented edge requires no detour to its endpoints.
            if (from.a == to.a && from.b == to.b && (to.t >= from.t || HasEdge(graph, from.b, from.a)))
                Consider(new List<Vector3> { from.point, to.point }, ref best, ref bestLength);
            int[] exits = HasEdge(graph, from.b, from.a) ? new[] { from.a, from.b } :
                from.t <= 0.0001f ? new[] { from.a, from.b } : new[] { from.b };
            int[] entries = HasEdge(graph, to.b, to.a) ? new[] { to.a, to.b } :
                to.t >= 0.9999f ? new[] { to.a, to.b } : new[] { to.a };
            foreach (int exit in exits)
            foreach (int entry in entries)
            {
                List<int> path = graph.FindPathAStar(exit, entry);
                if (path.Count == 0) continue;
                var candidate = new List<Vector3> { from.point };
                foreach (int node in path) candidate.Add(graph.nodes[node].world);
                candidate.Add(to.point);
                Consider(candidate, ref best, ref bestLength);
            }
            if (best == null) return false;
            foreach (var point in best)
                if (points.Count == 0 || (points[points.Count - 1] - point).sqrMagnitude > 0.0001f) points.Add(point);
        }
        if (points.Count < 2) return false;
        route.geometry.Clear();
        foreach (var point in points) route.geometry.Add(converter.WorldToGeoPosition(point));
        for (int i = 0; i < snaps.Count; i++)
        {
            var gps = converter.WorldToGeoPosition(snaps[i].point);
            route.stops[i].latitude = gps.lat;
            route.stops[i].longitude = gps.lon;
        }
        route.isRoadPathValidated = true;
        BusRouteParser.RefreshMetrics(route);
        return true;
    }

    static bool HasEdge(RoadGraph graph, int from, int to)
    {
        foreach (var edge in graph.nodes[from].edges) if (edge.to == to) return true;
        return false;
    }
    static void Consider(List<Vector3> candidate, ref List<Vector3> best, ref float bestLength)
    {
        float length = 0f;
        for (int i = 1; i < candidate.Count; i++) length += Vector3.Distance(candidate[i - 1], candidate[i]);
        if (length < bestLength) { bestLength = length; best = candidate; }
    }
}
