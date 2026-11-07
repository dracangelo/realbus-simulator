using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Shared, XZ road-clearance queries. Does not rely on shared OSM node IDs.</summary>
public sealed class RoadSurfaceIndex
{
    public sealed class Segment
    {
        public Vector3 a, b;
        public float halfWidth;
        public long wayId;
        public int oneWay;
    }
    const float CellSize = 64f;
    readonly Dictionary<Vector2Int, List<Segment>> cells = new Dictionary<Vector2Int, List<Segment>>();
    // Cells touched by each way id, so a runtime surface (a depot apron, for
    // example) can be withdrawn again without rescanning the whole city.
    readonly Dictionary<long, HashSet<Vector2Int>> wayCells = new Dictionary<long, HashSet<Vector2Int>>();

    public void Add(Vector3 a, Vector3 b, float width, long wayId, int oneWay = 0)
    {
        a.y = b.y = 0;
        if ((b - a).sqrMagnitude < .01f) return;
        var segment = new Segment { a = a, b = b, halfWidth = width * .5f, wayId = wayId, oneWay = oneWay };
        Vector3 margin = Vector3.one * (segment.halfWidth + 1f);
        var min = Cell(Vector3.Min(a, b) - margin); var max = Cell(Vector3.Max(a, b) + margin);
        if (!wayCells.TryGetValue(wayId, out var tracked)) wayCells[wayId] = tracked = new HashSet<Vector2Int>();
        for (int x = min.x; x <= max.x; x++)
        for (int z = min.y; z <= max.y; z++)
        {
            var key = new Vector2Int(x, z);
            if (!cells.TryGetValue(key, out var list)) cells[key] = list = new List<Segment>();
            list.Add(segment);
            tracked.Add(key);
        }
    }

    /// <summary>
    /// Withdraws every segment belonging to <paramref name="wayId"/>. Used when a
    /// runtime-built surface (a depot driveway) is torn down, so the index cannot
    /// keep advertising road that no longer exists.
    /// </summary>
    public int Remove(long wayId)
    {
        if (!wayCells.TryGetValue(wayId, out var tracked)) return 0;
        int removed = 0;
        foreach (var key in tracked)
        {
            if (!cells.TryGetValue(key, out var list)) continue;
            removed += list.RemoveAll(segment => segment.wayId == wayId);
            if (list.Count == 0) cells.Remove(key);
        }
        wayCells.Remove(wayId);
        return removed;
    }

    public static RoadSurfaceIndex Build(OSMData data, Func<double, double, Vector3> toWorld)
    {
        var index = new RoadSurfaceIndex();
        foreach (var way in data.ways)
        {
            if (!way.IsRoad()) continue;
            for (int i = 1; i < way.nodeRefs.Count; i++)
                if (data.nodeMap.TryGetValue(way.nodeRefs[i - 1], out var a) && data.nodeMap.TryGetValue(way.nodeRefs[i], out var b))
                    index.Add(toWorld(a.lat, a.lon), toWorld(b.lat, b.lon), way.GetRoadWidth(), way.id, way.GetOneWayDirectionSign());
        }
        return index;
    }

    static Vector2Int Cell(Vector3 p) => new Vector2Int(Mathf.FloorToInt(p.x / CellSize), Mathf.FloorToInt(p.z / CellSize));
    public IEnumerable<Segment> Around(Vector3 point, float radius)
        => Nearby(point - Vector3.one * radius, point + Vector3.one * radius);

    /// <summary>True when no road has been registered: a test fixture, or a city whose mesh is not built yet.</summary>
    public bool IsEmpty => cells.Count == 0;

    /// <summary>
    /// Nearest point on the drawn asphalt within <paramref name="radius"/> of
    /// <paramref name="point"/>. The traffic graph is built from OSM node positions
    /// while the ribbon is drawn from the way's own width, and the two can disagree
    /// (a node welded across a dual carriageway, a way whose centreline runs down a
    /// median). The asphalt the player can see is the authority, so traffic rails
    /// against this.
    /// </summary>
    public bool TryGetNearestSurface(Vector3 point, float radius, out Vector3 centre, out float halfWidth, out float distance)
    {
        centre = point;
        halfWidth = 0f;
        distance = float.MaxValue;
        if (cells.Count == 0) return false;

        point.y = 0f;
        float limit = Mathf.Max(0f, radius);
        foreach (var segment in Around(point, limit))
        {
            Vector3 projected = RoadSpawnResolver.ProjectOnSegment(point, segment.a, segment.b);
            float candidate = Vector3.Distance(point, projected);
            if (candidate > limit || candidate >= distance) continue;
            distance = candidate;
            centre = projected;
            halfWidth = segment.halfWidth;
        }

        return distance < float.MaxValue;
    }
    IEnumerable<Segment> Nearby(Vector3 min, Vector3 max)
    {
        var seen = new HashSet<Segment>(); var lo = Cell(min); var hi = Cell(max);
        for (int x = lo.x; x <= hi.x; x++)
        for (int z = lo.y; z <= hi.y; z++)
            if (cells.TryGetValue(new Vector2Int(x, z), out var list))
                foreach (var segment in list) if (seen.Add(segment)) yield return segment;
    }

    public bool Contains(Vector3 point, float clearance = 0f, long excludeWay = long.MinValue)
    {
        point.y = 0;
        foreach (var segment in Nearby(point - Vector3.one * clearance, point + Vector3.one * clearance))
            if (segment.wayId != excludeWay && (point - RoadSpawnResolver.ProjectOnSegment(point, segment.a, segment.b)).sqrMagnitude <=
                (segment.halfWidth + clearance) * (segment.halfWidth + clearance)) return true;
        return false;
    }

    public bool OverlapsFootprint(IList<Vector3> polygon, float clearance = .5f, long excludeWay = long.MinValue)
    {
        if (polygon == null || polygon.Count < 3) return false;
        Vector3 min = polygon[0], max = min;
        foreach (var p in polygon) { min = Vector3.Min(min, p); max = Vector3.Max(max, p); }
        foreach (var segment in Nearby(min - Vector3.one * clearance, max + Vector3.one * clearance))
        {
            if (segment.wayId == excludeWay) continue;
            if (Inside(segment.a, polygon) || Inside(segment.b, polygon)) return true;
            float radius = segment.halfWidth + clearance;
            for (int i = 0; i < polygon.Count; i++)
                if (SegmentDistanceSquared(polygon[i], polygon[(i + 1) % polygon.Count], segment.a, segment.b) <= radius * radius) return true;
        }
        return false;
    }

    static bool Inside(Vector3 p, IList<Vector3> polygon)
    {
        bool inside = false;
        for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
        {
            var a = polygon[i]; var b = polygon[j];
            if ((a.z > p.z) != (b.z > p.z) && p.x < (b.x - a.x) * (p.z - a.z) / (b.z - a.z) + a.x) inside = !inside;
        }
        return inside;
    }

    public static float SegmentDistanceSquared(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        a.y = b.y = c.y = d.y = 0;
        Vector3 ab = b - a, cd = d - c;
        float cross = ab.x * cd.z - ab.z * cd.x;
        if (Mathf.Abs(cross) > .00001f)
        {
            Vector3 ac = c - a;
            float t = (ac.x * cd.z - ac.z * cd.x) / cross;
            float u = (ac.x * ab.z - ac.z * ab.x) / cross;
            if (t >= 0 && t <= 1 && u >= 0 && u <= 1) return 0;
        }
        return Mathf.Min((a - RoadSpawnResolver.ProjectOnSegment(a, c, d)).sqrMagnitude,
            (b - RoadSpawnResolver.ProjectOnSegment(b, c, d)).sqrMagnitude,
            (c - RoadSpawnResolver.ProjectOnSegment(c, a, b)).sqrMagnitude,
            (d - RoadSpawnResolver.ProjectOnSegment(d, a, b)).sqrMagnitude);
    }

    // Fractions along a kerb/marking where no other road crosses. Expand by
    // half a sample so the resulting collider cannot bridge a missed crossing.
    public IEnumerable<Vector2> ClearRuns(Vector3 from, Vector3 to, long wayId, float clearance)
    {
        int steps = Mathf.Max(1, Mathf.CeilToInt(Vector3.Distance(from, to)));
        float start = -1;
        for (int i = 0; i <= steps; i++)
        {
            bool clear = i < steps && !Contains(Vector3.Lerp(from, to, (i + .5f) / steps), clearance + .5f, wayId);
            if (clear && start < 0) start = (float)i / steps;
            if (!clear && start >= 0) { yield return new Vector2(start, (float)i / steps); start = -1; }
        }
    }

    public bool TryRoadside(Vector3 requested, Vector3 heading, bool leftTraffic, out Vector3 kerb, out Quaternion rotation)
    {
        kerb = requested; rotation = Quaternion.identity;
        float best = float.MaxValue; bool found = false;
        heading = Vector3.ProjectOnPlane(heading, Vector3.up).normalized;

        Vector3 fallbackKerb = requested;
        Quaternion fallbackRotation = Quaternion.identity;
        float bestFallbackScore = float.MaxValue;
        bool hasFallback = false;

        foreach (var segment in Nearby(requested - Vector3.one * 60, requested + Vector3.one * 60))
        {
            Vector3 direction = (segment.b - segment.a).normalized;
            if (segment.oneWay < 0) direction = -direction;
            if (segment.oneWay == 0 && Vector3.Dot(direction, heading) < 0) direction = -direction;
            float alignment = heading.sqrMagnitude > .1f ? Vector3.Dot(direction, heading) : 1;
            if (alignment < 0) continue;
            Vector3 center = RoadSpawnResolver.ProjectOnSegment(new Vector3(requested.x, 0, requested.z), segment.a, segment.b);
            for (int step = -3; step <= 3; step++)
            {
                Vector3 along = RoadSpawnResolver.ProjectOnSegment(center + direction * step * 8f, segment.a, segment.b);
                float distance = (along - new Vector3(requested.x, 0, requested.z)).sqrMagnitude;
                if (distance > 3600f) continue;
                float score = distance + (1 - alignment) * 900f;
                Vector3 candidate = along + RoadSpawnResolver.KerbSide(direction, leftTraffic) * (segment.halfWidth + .6f);
                if (score < bestFallbackScore)
                {
                    bestFallbackScore = score;
                    fallbackKerb = candidate;
                    fallbackRotation = Quaternion.LookRotation(direction, Vector3.up);
                    hasFallback = true;
                }
                if (score >= best || Contains(candidate, .3f)) continue;
                best = score; found = true; kerb = candidate; rotation = Quaternion.LookRotation(direction, Vector3.up);
            }
        }

        if (!found && hasFallback)
        {
            kerb = fallbackKerb;
            rotation = fallbackRotation;
            return true;
        }

        return found;
    }
}
