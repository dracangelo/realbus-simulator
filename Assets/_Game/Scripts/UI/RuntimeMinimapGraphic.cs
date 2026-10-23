using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Live route minimap drawn straight into the UI mesh — no second camera, no
/// render texture, no extra draw pass.
///
/// The map rotates with the bus so "up" is always the direction of travel, which
/// is what a driver actually reads at a glance. The route is drawn as a line the
/// player can follow: the distance already covered is dimmed, the road still to
/// drive is bright and carries direction chevrons, and the next stop is pinned.
///
/// Geometry is authored in world metres and projected through <see cref="ProjectWorld"/>
/// so the projection stays a pure function that unit tests can pin down.
/// </summary>
public class RuntimeMinimapGraphic : MaskableGraphic
{
    public BusController bus;
    public MissionManager mission;
    public BusRoute route;

    [Tooltip("World metres from the bus to the edge of the minimap.")]
    public float worldRadiusMeters = 450f;

    [Tooltip("Rotate the map so the bus always faces up. Turn off for a fixed north-up view.")]
    public bool rotateWithHeading = true;

    [Header("Palette")]
    public Color routeColor = new Color(1f, 0.57f, 0.2f, 1f);      // road still to drive
    public Color routeTrailColor = new Color(1f, 0.57f, 0.2f, 0.28f); // road already covered
    public Color chevronColor = new Color(1f, 0.72f, 0.34f, 0.92f);
    public Color stopColor = new Color(0.92f, 0.95f, 1f, 0.95f);
    public Color passedStopColor = new Color(0.55f, 0.6f, 0.66f, 0.5f);
    public Color nextStopColor = new Color(0.2f, 1f, 0.62f, 1f);
    public Color busColor = new Color(0.13f, 0.7f, 1f, 1f);
    public Color ringColor = new Color(1f, 1f, 1f, 0.09f);
    public Color roadColor = new Color(0.62f, 0.68f, 0.74f, 0.34f);
    public Color northColor = new Color(1f, 0.55f, 0.3f, 0.75f);

    [Tooltip("Cap on road segments drawn per frame, so a dense junction cannot turn the map into a full-city rebuild.")]
    public int maxRoadSegments = 1200;
    [Tooltip("Metres the bus must move before the cached road set is re-queried.")]
    public float roadCacheRefreshMeters = 30f;

    readonly System.Collections.Generic.List<RoadSurfaceIndex.Segment> roadCache =
        new System.Collections.Generic.List<RoadSurfaceIndex.Segment>();
    RoadSurfaceIndex roadCacheIndex;
    Vector3 roadCacheOrigin;
    bool roadCacheValid;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        Rect rect = rectTransform.rect;
        float radius = Mathf.Min(rect.width, rect.height) * 0.5f;
        if (radius <= 1f) return;

        AddCircle(vh, Vector2.zero, radius, color, 48);

        // Range rings give the map a readable sense of scale.
        AddRing(vh, radius * 0.5f, 1.5f, ringColor, 40);
        AddRing(vh, radius * 0.78f, 1.5f, ringColor, 48);

        if (bus == null) bus = FindAnyObjectByType<BusController>();
        if (mission == null) mission = MissionManager.Instance;
        if (bus == null) return;

        float yaw = rotateWithHeading ? bus.transform.eulerAngles.y : 0f;
        Vector3 busPosition = bus.transform.position;

        // The streets around the bus come first: a real map in free drive, where
        // there is no route to draw, and a readable backdrop under the route when
        // there is one.
        DrawRoads(vh, busPosition, yaw, radius);
        Vector3[] path = ResolvePath();

        int nearest = -1;
        if (path != null && path.Length >= 2)
        {
            nearest = NearestIndex(path, busPosition);

            // Drive the path in two passes so the covered part can be dimmed while
            // the road ahead stays the visual priority.
            for (int i = 1; i <= path.Length - 1; i++)
            {
                bool covered = i <= nearest;
                Vector2 a = ProjectWorld(path[i - 1], busPosition, yaw, radius, worldRadiusMeters);
                Vector2 b = ProjectWorld(path[i], busPosition, yaw, radius, worldRadiusMeters);
                if (!ClipSegmentToCircle(a, b, radius, out Vector2 ca, out Vector2 cb)) continue;
                AddLine(vh, ca, cb, covered ? 2.2f : 4.2f, covered ? routeTrailColor : routeColor);
            }

            AddChevrons(vh, path, nearest, busPosition, yaw, radius);
        }

        DrawStops(vh, radius, yaw, busPosition);

        // Bus marker: the map rotates, so it always faces up.
        AddBusArrow(vh, busColor);
        AddNorthMarker(vh, radius, yaw);
    }

    /// <summary>
    /// Draws the streets within the map radius, so the dial reads as a map rather
    /// than an empty gauge (especially in free drive, where there is no route).
    /// Segments come from the same <see cref="RoadSurfaceIndex"/> the world is built
    /// from, and are clipped to the dial so nothing spills past its edge.
    /// </summary>
    void DrawRoads(VertexHelper vh, Vector3 busPosition, float yaw, float radius)
    {
        var builder = OSMRoadMeshBuilder.Instance;
        var roads = builder != null ? builder.SurfaceIndex : null;
        if (roads == null) return;

        float metresToPixels = radius / Mathf.Max(1f, worldRadiusMeters);
        int cap = Mathf.Max(1, maxRoadSegments);
        RefreshRoadCache(roads, busPosition, cap);

        int drawn = 0;
        for (int i = 0; i < roadCache.Count; i++)
        {
            if (drawn >= cap) break;
            var segment = roadCache[i];
            Vector2 a = ProjectWorld(segment.a, busPosition, yaw, radius, worldRadiusMeters);
            Vector2 b = ProjectWorld(segment.b, busPosition, yaw, radius, worldRadiusMeters);
            if (!ClipSegmentToCircle(a, b, radius - 1f, out Vector2 ca, out Vector2 cb)) continue;
            float width = Mathf.Clamp(segment.halfWidth * 2f * metresToPixels, 1.2f, 9f);
            AddLine(vh, ca, cb, width, roadColor);
            drawn++;
        }
    }

    /// <summary>
    /// The nearby road set changes slowly compared with the frame rate, so it is
    /// re-queried only when the bus has moved far enough or the world was rebuilt.
    /// Projecting a cached segment each frame is cheap; scanning the index by cell
    /// every frame is not.
    /// </summary>
    void RefreshRoadCache(RoadSurfaceIndex roads, Vector3 busPosition, int cap)
    {
        bool fresh = roadCacheValid && roadCacheIndex == roads &&
            (busPosition - roadCacheOrigin).sqrMagnitude <= roadCacheRefreshMeters * roadCacheRefreshMeters;
        if (fresh) return;

        roadCache.Clear();
        foreach (var segment in roads.Around(busPosition, worldRadiusMeters))
        {
            roadCache.Add(segment);
            if (roadCache.Count >= cap) break;
        }
        roadCacheOrigin = busPosition;
        roadCacheIndex = roads;
        roadCacheValid = true;
    }

    void Update() { SetVerticesDirty(); }

    // ── Path resolution ───────────────────────────────────────────────

    /// <summary>
    /// The live guidance path wins. In free drive there is no mission path, so fall
    /// back to the route the player selected, and only give up when nothing is loaded.
    /// </summary>
    Vector3[] ResolvePath()
    {
        MissionManager activeMission = mission != null ? mission : MissionManager.Instance;

        if (activeMission != null)
        {
            var live = activeMission.ActiveGuidancePathPoints;
            if (live != null && live.Count >= 2)
            {
                var copy = new Vector3[live.Count];
                for (int i = 0; i < live.Count; i++) copy[i] = live[i];
                return copy;
            }

            Vector3[] built = activeMission.GetGuidancePathPoints();
            if (built != null && built.Length >= 2) return built;
        }

        BusRoute activeRoute = (activeMission != null && activeMission.currentRoute != null)
            ? activeMission.currentRoute
            : route != null ? route : (GameState.Instance != null ? GameState.Instance.selectedRoute : null);
        if (activeRoute != null)
        {
            activeRoute.EnsureWorldPathPoints();
            if (activeRoute.HasUsableWorldPath() && activeRoute.pathPoints != null && activeRoute.pathPoints.Length >= 2)
                return activeRoute.pathPoints;
        }

        // Imported routes can arrive with geodata but no pre-built world points (or the
        // world points failed to build for the current map origin). Re-derive the line so
        // the minimap still shows the road ahead instead of an empty dial. The result is
        // cached and decimated: this runs inside OnPopulateMesh, which is rebuilt every
        // frame, and route geometry can carry tens of thousands of points.
        return ResolveFallback(activeRoute);
    }

    BusRoute fallbackRoute;
    Vector3[] fallbackPath;

    Vector3[] ResolveFallback(BusRoute activeRoute)
    {
        if (activeRoute == null || GPSManager.Instance == null) return null;
        if (fallbackPath != null && fallbackRoute == activeRoute) return fallbackPath;

        fallbackRoute = activeRoute;
        Vector3[] points = null;
        if (activeRoute.geometryLatLonFlat != null && activeRoute.geometryLatLonFlat.Length >= 4)
        {
            int count = activeRoute.geometryLatLonFlat.Length / 2;
            points = new Vector3[count];
            for (int i = 0; i < count; i++)
                points[i] = GPSManager.Instance.GpsToWorld(activeRoute.geometryLatLonFlat[i * 2], activeRoute.geometryLatLonFlat[i * 2 + 1]);
        }
        else if (activeRoute.stops != null && activeRoute.stops.Length >= 2)
        {
            points = new Vector3[activeRoute.stops.Length];
            for (int i = 0; i < points.Length; i++)
            {
                var stop = activeRoute.stops[i];
                points[i] = stop != null
                    ? GPSManager.Instance.GpsToWorld(stop.latitude, stop.longitude)
                    : i > 0 ? points[i - 1] : Vector3.zero;
            }
        }

        fallbackPath = Decimate(points, 1200);
        return fallbackPath;
    }

    static Vector3[] Decimate(Vector3[] points, int maxPoints)
    {
        if (points == null || points.Length <= maxPoints) return points;

        int last = points.Length - 1;
        int stride = Mathf.Max(1, Mathf.CeilToInt(last / (float)(maxPoints - 1)));
        var result = new Vector3[maxPoints];
        for (int i = 0; i < maxPoints; i++)
            result[i] = points[Mathf.Min(i * stride, last)];
        result[maxPoints - 1] = points[last];
        return result;
    }

    static int NearestIndex(Vector3[] path, Vector3 position)
    {
        int nearest = 0;
        float best = float.MaxValue;
        for (int i = 0; i < path.Length; i++)
        {
            float sqr = (path[i] - position).sqrMagnitude;
            if (sqr < best) { best = sqr; nearest = i; }
        }
        return nearest;
    }

    // ── Drawing helpers ───────────────────────────────────────────────

    void DrawStops(VertexHelper vh, float radius, float yaw, Vector3 busPosition)
    {
        MissionManager activeMission = mission != null ? mission : MissionManager.Instance;
        BusRoute activeRoute = activeMission != null && activeMission.currentRoute != null ? activeMission.currentRoute : route;
        int nextIndex = activeMission != null ? activeMission.currentStopIndex : 0;

        if (activeRoute != null && activeRoute.stops != null && GPSManager.Instance != null)
        {
            for (int i = 0; i < activeRoute.stops.Length; i++)
            {
                var stop = activeRoute.stops[i];
                if (stop == null) continue;
                Vector3 world = GPSManager.Instance.GpsToWorld(stop.latitude, stop.longitude);
                Vector2 p = ProjectWorld(world, busPosition, yaw, radius, worldRadiusMeters);
                if (p.magnitude > radius - 2f) continue;
                bool next = i == nextIndex;
                bool passed = i < nextIndex;
                float size = next ? 6.5f : 4f;
                Color tint = next ? nextStopColor : passed ? passedStopColor : stopColor;
                if (next) AddRing(vh, size + 3.5f, 2f, WithAlpha(nextStopColor, 0.5f), 20, p);
                AddCircle(vh, p, size, tint, 18);
            }
            return;
        }

        // No geographic stops available: mark the guidance nodes so the line still reads.
        Vector3[] path = ResolvePath();
        if (path == null || path.Length < 2) return;
        int stride = Mathf.Max(1, path.Length / 10);
        for (int i = 0; i < path.Length; i += stride)
        {
            Vector2 p = ProjectWorld(path[i], busPosition, yaw, radius, worldRadiusMeters);
            if (p.magnitude > radius - 2f) continue;
            AddCircle(vh, p, 3f, stopColor, 12);
        }
    }

    void AddChevrons(VertexHelper vh, Vector3[] path, int nearest, Vector3 busPosition, float yaw, float radius)
    {
        if (nearest < 0) return;
        int stride = Mathf.Max(2, path.Length / 14);
        for (int i = Mathf.Max(nearest + 1, 1); i < path.Length; i += stride)
        {
            Vector2 a = ProjectWorld(path[i - 1], busPosition, yaw, radius, worldRadiusMeters);
            Vector2 b = ProjectWorld(path[i], busPosition, yaw, radius, worldRadiusMeters);
            Vector2 delta = b - a;
            if (delta.sqrMagnitude < 0.01f) continue;
            Vector2 center = (a + b) * 0.5f;
            if (center.magnitude > radius - 6f) continue;
            AddChevron(vh, center, delta.normalized, chevronColor);
        }
    }

    void AddNorthMarker(VertexHelper vh, float radius, float yaw)
    {
        // North is +Z in world space; rotate it into the map's current frame.
        Vector2 north = RotateToHeading(new Vector2(0f, 1f), yaw);
        Vector2 tip = north * (radius - 10f);
        Vector2 baseCenter = north * (radius - 22f);
        Vector2 side = new Vector2(-north.y, north.x) * 7f;
        int start = vh.currentVertCount;
        vh.AddVert(tip, northColor, Vector2.zero);
        vh.AddVert(baseCenter - side, northColor, Vector2.zero);
        vh.AddVert(baseCenter + side, northColor, Vector2.zero);
        vh.AddTriangle(start, start + 1, start + 2);
    }

    static void AddBusArrow(VertexHelper vh, Color tint)
    {
        AddCircle(vh, Vector2.zero, 12f, new Color(0.02f, 0.05f, 0.08f, 0.85f), 24);
        int s = vh.currentVertCount;
        vh.AddVert(new Vector2(0f, 12f), tint, Vector2.zero);
        vh.AddVert(new Vector2(-8f, -8f), tint, Vector2.zero);
        vh.AddVert(new Vector2(0f, -3f), tint, Vector2.zero);
        vh.AddVert(new Vector2(8f, -8f), tint, Vector2.zero);
        vh.AddTriangle(s, s + 1, s + 2);
        vh.AddTriangle(s, s + 2, s + 3);
    }

    // ── Pure geometry (unit-testable) ─────────────────────────────────

    /// <summary>Offsets a world XZ offset into map space so the heading points up.</summary>
    public static Vector2 RotateToHeading(Vector2 offset, float headingDegrees)
    {
        float a = headingDegrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(a), sin = Mathf.Sin(a);
        return new Vector2(offset.x * cos - offset.y * sin, offset.x * sin + offset.y * cos);
    }

    /// <summary>Projects a world point into minimap pixels relative to the panel centre.</summary>
    public static Vector2 ProjectWorld(Vector3 world, Vector3 busPosition, float yaw, float radius, float worldRadiusMeters)
    {
        Vector2 offset = new Vector2(world.x - busPosition.x, world.z - busPosition.z);
        Vector2 rotated = RotateToHeading(offset, yaw);
        return rotated * (radius / Mathf.Max(1f, worldRadiusMeters));
    }

    /// <summary>
    /// Clips a segment to the map's circle. Returns false when the whole segment is
    /// outside, so off-map road never spills past the dial edge.
    /// </summary>
    public static bool ClipSegmentToCircle(Vector2 a, Vector2 b, float radius, out Vector2 clippedA, out Vector2 clippedB)
    {
        clippedA = a; clippedB = b;
        Vector2 d = b - a;
        float aa = Vector2.Dot(d, d);
        if (aa < 1e-6f) return a.sqrMagnitude <= radius * radius;

        float bb = 2f * Vector2.Dot(a, d);
        float cc = Vector2.Dot(a, a) - radius * radius;
        float disc = bb * bb - 4f * aa * cc;
        if (disc < 0f) return false;

        float root = Mathf.Sqrt(disc);
        float t0 = (-bb - root) / (2f * aa);
        float t1 = (-bb + root) / (2f * aa);
        t0 = Mathf.Max(t0, 0f);
        t1 = Mathf.Min(t1, 1f);
        if (t1 < t0) return false;
        clippedA = a + d * t0;
        clippedB = a + d * t1;
        return true;
    }

    // ── Mesh primitives ───────────────────────────────────────────────

    static void AddRing(VertexHelper vh, float radius, float width, Color tint, int segments, Vector2 center = default)
    {
        float inner = Mathf.Max(0f, radius - width);
        for (int i = 0; i < segments; i++)
        {
            float a0 = i * Mathf.PI * 2f / segments;
            float a1 = (i + 1) * Mathf.PI * 2f / segments;
            Vector2 o0 = new Vector2(Mathf.Sin(a0), Mathf.Cos(a0));
            Vector2 o1 = new Vector2(Mathf.Sin(a1), Mathf.Cos(a1));
            int s = vh.currentVertCount;
            vh.AddVert(center + o0 * inner, tint, Vector2.zero);
            vh.AddVert(center + o0 * radius, tint, Vector2.zero);
            vh.AddVert(center + o1 * radius, tint, Vector2.zero);
            vh.AddVert(center + o1 * inner, tint, Vector2.zero);
            vh.AddTriangle(s, s + 1, s + 2);
            vh.AddTriangle(s, s + 2, s + 3);
        }
    }

    static void AddChevron(VertexHelper vh, Vector2 center, Vector2 direction, Color tint)
    {
        Vector2 side = new Vector2(-direction.y, direction.x);
        Vector2 tip = center + direction * 6f;
        Vector2 back = center - direction * 3f;
        int s = vh.currentVertCount;
        vh.AddVert(tip, tint, Vector2.zero);
        vh.AddVert(back + side * 5f, tint, Vector2.zero);
        vh.AddVert(back - side * 5f, tint, Vector2.zero);
        vh.AddTriangle(s, s + 1, s + 2);
    }

    static void AddCircle(VertexHelper vh, Vector2 center, float radius, Color tint, int segments)
    {
        int start = vh.currentVertCount;
        vh.AddVert(center, tint, Vector2.zero);
        for (int i = 0; i <= segments; i++)
        {
            float a = i * Mathf.PI * 2f / segments;
            vh.AddVert(center + new Vector2(Mathf.Sin(a), Mathf.Cos(a)) * radius, tint, Vector2.zero);
        }
        for (int i = 0; i < segments; i++) vh.AddTriangle(start, start + i + 1, start + i + 2);
    }

    static void AddLine(VertexHelper vh, Vector2 a, Vector2 b, float width, Color tint)
    {
        Vector2 delta = b - a;
        if (delta.sqrMagnitude < 1e-6f) return;
        Vector2 n = new Vector2(-delta.y, delta.x).normalized * width * 0.5f;
        int s = vh.currentVertCount;
        vh.AddVert(a - n, tint, Vector2.zero); vh.AddVert(a + n, tint, Vector2.zero);
        vh.AddVert(b + n, tint, Vector2.zero); vh.AddVert(b - n, tint, Vector2.zero);
        vh.AddTriangle(s, s + 1, s + 2); vh.AddTriangle(s, s + 2, s + 3);
    }

    static Color WithAlpha(Color c, float a) { c.a = a; return c; }
}
