using System.Collections.Generic;
using UnityEngine;

/// <summary>Places vehicles along OSM centerlines, never on collider bounds or kerbs.</summary>
public static class RoadSpawnResolver
{
    struct Candidate
    {
        public Vector3 point;
        public Quaternion rotation;
        public float distance;
        public long wayId;
    }

    public static bool TryResolve(BusController bus, Vector3 requested, Vector3 heading,
        float radius, out Vector3 position, out Quaternion rotation)
    {
        position = requested;
        rotation = bus != null ? bus.transform.rotation : Quaternion.identity;
        var data = OSMLoader.Instance != null ? OSMLoader.Instance.osmData : null;
        var roads = OSMRoadMeshBuilder.Instance;
        if (bus == null || data == null || roads == null || !roads.roadsBuilt || GPSManager.Instance == null)
            return false;

        var body = bus.GetComponent<BoxCollider>();
        Vector3 size = body != null ? Vector3.Scale(body.size, bus.transform.lossyScale) : new Vector3(2.6f, 3.2f, 12f);
        Vector3 center = body != null ? Vector3.Scale(body.center, bus.transform.lossyScale) : new Vector3(0, 2, 0);
        var candidates = new List<Candidate>();
        CountryDefinition country = ResolveCountry();
        bool driveOnLeft = country != null && country.DrivesOnLeft;
        heading = Vector3.ProjectOnPlane(heading, Vector3.up).normalized;
        foreach (var way in data.ways)
        {
            if (!way.IsRoad() || way.GetRoadWidth() < size.x + 0.6f ||
                way.GetRoadWidth() / way.GetLaneCount() < size.x + .2f) continue;
            for (int i = 1; i < way.nodeRefs.Count; i++)
            {
                if (!data.nodeMap.TryGetValue(way.nodeRefs[i - 1], out var a) ||
                    !data.nodeMap.TryGetValue(way.nodeRefs[i], out var b)) continue;
                Vector3 from = GPSManager.Instance.GpsToWorld(a.lat, a.lon);
                Vector3 to = GPSManager.Instance.GpsToWorld(b.lat, b.lon);
                from.y = to.y = roads.roadYOffset;
                Vector3 segment = to - from;
                float length = segment.magnitude;
                // Avoid placing a long chassis across a tight bend or junction.
                float endClearance = size.z * .5f + .4f;
                if (length < endClearance * 2f) continue;
                Vector3 direction = segment / length;
                Vector3 projected = ProjectOnSegment(requested, from, to);
                if ((new Vector2(projected.x - requested.x, projected.z - requested.z)).sqrMagnitude > radius * radius) continue;
                Vector3 forward = direction;
                if (way.GetOneWayDirectionSign() < 0) forward = -forward;
                else if (!way.IsOneWay() && Vector3.Dot(forward, heading) < 0f) forward = -forward;
                float alignment = heading.sqrMagnitude > .1f ? Vector3.Dot(forward, heading) : 1f;
                // Never select the adjacent opposing one-way carriageway.
                if (way.IsOneWay() && alignment < 0f) continue;
                Quaternion facing = Quaternion.LookRotation(forward, Vector3.up);
                Vector3 laneOffset = Vector3.Cross(Vector3.up, forward) * LaneOffset(
                    way.GetRoadWidth(), way.GetLaneCount(), way.IsOneWay(), driveOnLeft, size.x);
                float along = Vector3.Dot(projected - from, direction);
                // Alternative positions let long vehicles clear junction kerbs.
                for (int step = -2; step <= 2; step++)
                {
                    Vector3 point = from + direction * Mathf.Clamp(along + step * size.z, endClearance, length - endClearance) + laneOffset;
                    float distance = new Vector2(point.x - requested.x, point.z - requested.z).sqrMagnitude;
                    if (distance <= radius * radius)
                        candidates.Add(new Candidate { point = point, rotation = facing,
                            distance = distance + (1f - alignment) * radius * radius, wayId = way.id });
                }
            }
        }
        candidates.Sort((a, b) => a.distance.CompareTo(b.distance));
        Physics.SyncTransforms();
        foreach (var candidate in candidates)
        {
            // Check the settled chassis, not a high airborne spawn that can
            // subsequently drop onto a boundary and lift the wheels off-road.
            Vector3 settled = candidate.point + Vector3.up * 0.05f;
            bool obstructed = false;
            foreach (var collider in Physics.OverlapBox(settled + candidate.rotation * center,
                size * 0.5f + new Vector3(0.15f, 0f, 0.3f), candidate.rotation, ~0, QueryTriggerInteraction.Ignore))
            {
                if (collider.transform.IsChildOf(bus.transform) || collider.bounds.max.y <= candidate.point.y + 0.15f) continue;
                obstructed = true;
                break;
            }
            if (obstructed) continue;
            position = candidate.point + Vector3.up * 1.2f;
            rotation = candidate.rotation;
            Debug.Log($"RoadSpawnResolver: way={candidate.wayId} traffic={(driveOnLeft ? "left" : "right")} position={position} heading={rotation.eulerAngles.y:F1}");
            return true;
        }
        Debug.LogWarning("RoadSpawnResolver: No clear road spawn nearby; retaining the requested location.");
        return false;
    }

    public static Vector3 ProjectOnSegment(Vector3 point, Vector3 from, Vector3 to)
    {
        Vector3 segment = to - from;
        return segment.sqrMagnitude < 0.0001f ? from :
            from + segment * Mathf.Clamp01(Vector3.Dot(point - from, segment) / segment.sqrMagnitude);
    }

    public static float LaneOffset(float width, int lanes, bool oneWay, bool leftHandTraffic, float busWidth)
    {
        // Choose the outer travelling lane, not the line between lanes on a
        // four-lane road. A shared single-lane road has no separate left/right lane.
        float offset = lanes <= 1 ? 0f : width * .5f - width / lanes * .5f;
        offset = Mathf.Clamp(offset, 0f, Mathf.Max(0f, (width - busWidth) * .5f - .35f));
        // The sign comes from the shared traffic-side authority, so OSM-data road
        // placement and graph lane placement cannot end up on opposite sides.
        return TrafficLane.SideSign(leftHandTraffic) * offset;
    }

    /// <summary>
    /// Unit vector from the carriageway centreline toward the kerb a bus stops at,
    /// for a given direction of travel. In left-hand traffic the bus keeps left and
    /// docks at the left kerb, so the sign flips with the traffic side. Placement and
    /// the smoke audit both use this, so they cannot disagree about which kerb is
    /// "correct".
    /// </summary>
    public static Vector3 KerbSide(Vector3 heading, bool leftHandTraffic)
    {
        return TrafficLane.KerbDirection(heading, leftHandTraffic);
    }

    public static CountryDefinition ResolveCountry()
    {
        var manager = CityManager.Instance;
        if (manager == null) return null;
        var city = manager.activeCity;
        if (city == null) return manager.activeCountry;
        foreach (var country in manager.Countries)
            if (country != null && (System.String.Equals(country.countryName, city.country, System.StringComparison.OrdinalIgnoreCase) ||
                (country.cities != null && System.Array.IndexOf(country.cities, city) >= 0))) return country;
        return manager.activeCountry;
    }

    public static Vector3 RouteHeading(BusRoute route, Vector3 requested, Vector3 fallback,
        Vector3 approachHint = default)
    {
        if (route == null) return fallback;
        // The driven path is the authority on travel order: it is the line the
        // minimap draws and the mission scores the bus against. Raw OSM geometry (and
        // the stop list) can be stored in an order that does not match the drive, and
        // a reversed tangent puts a kerb-side prop on the opposite side of the road.
        if (route.EnsureWorldPathPoints() && route.pathPoints != null && route.pathPoints.Length >= 2)
            return PathHeading(route.pathPoints, requested, fallback, approachHint);

        var geometry = route.geometryLatLonFlat;
        if (GPSManager.Instance != null && geometry != null && geometry.Length >= 4)
        {
            var points = new Vector3[geometry.Length / 2];
            for (int i = 0; i < points.Length; i++) points[i] = GPSManager.Instance.GpsToWorld(geometry[i * 2], geometry[i * 2 + 1]);
            return PathHeading(points, requested, fallback, approachHint);
        }
        return fallback;
    }

    /// <summary>
    /// The direction the player actually arrives at a stop from. A loop or
    /// out-and-back route passes the same kerb twice, so the nearest path segment
    /// is ambiguous and the first one found can point back along the return leg;
    /// that mirrors the shelter onto the oncoming side. The road-graph path from
    /// the previous stop is the same approach the mission drives, so it breaks the
    /// tie in favour of the pass the bus actually uses.
    /// </summary>
    public static Vector3 StopApproachHeading(BusRoute route, int stopIndex, Vector3 stopWorld,
        Vector3 fallback, AIRoadGraph graph)
    {
        if (route == null || route.stops == null || GPSManager.Instance == null)
            return RouteHeading(route, stopWorld, fallback);

        Vector3 hint = Vector3.zero;
        if (graph != null)
        {
            if (stopIndex > 0 && stopIndex < route.stops.Length)
            {
                Vector3 previous = GpsToWorld(route.stops[stopIndex - 1]);
                hint = LastPathDirection(graph, previous, stopWorld);
            }
            else if (stopIndex == 0 && route.stops.Length > 1)
            {
                Vector3 next = GpsToWorld(route.stops[1]);
                hint = FirstPathDirection(graph, stopWorld, next);
            }
        }

        // If road graph gave no directional hint, derive forward travel flow directly
        // from sequential stop progression so return legs / oncoming lanes cannot hijack orientation
        if (hint.sqrMagnitude < 0.01f)
        {
            if (stopIndex < route.stops.Length - 1)
            {
                for (int nextIdx = stopIndex + 1; nextIdx < route.stops.Length; nextIdx++)
                {
                    Vector3 next = GpsToWorld(route.stops[nextIdx]);
                    Vector3 diff = Vector3.ProjectOnPlane(next - stopWorld, Vector3.up);
                    if (diff.sqrMagnitude >= 4f)
                    {
                        hint = diff.normalized;
                        break;
                    }
                }
            }
            if (hint.sqrMagnitude < 0.01f && stopIndex > 0)
            {
                for (int prevIdx = stopIndex - 1; prevIdx >= 0; prevIdx--)
                {
                    Vector3 prev = GpsToWorld(route.stops[prevIdx]);
                    Vector3 diff = Vector3.ProjectOnPlane(stopWorld - prev, Vector3.up);
                    if (diff.sqrMagnitude >= 4f)
                    {
                        hint = diff.normalized;
                        break;
                    }
                }
            }
        }

        if (hint.sqrMagnitude < 0.01f && fallback.sqrMagnitude >= 0.01f)
            hint = Vector3.ProjectOnPlane(fallback, Vector3.up).normalized;

        return RouteHeading(route, stopWorld, fallback, hint);
    }

    static Vector3 GpsToWorld(BusStopData stop)
    {
        if (stop == null || GPSManager.Instance == null) return Vector3.zero;
        return GPSManager.Instance.GpsToWorld(stop.latitude, stop.longitude);
    }

    static Vector3 LastPathDirection(AIRoadGraph graph, Vector3 from, Vector3 to)
    {
        if (graph == null || !graph.TryFindPathWorld(from, to, out var path) || path == null)
            return Vector3.zero;

        // The graph appends the exact stop point, which can leave a 2 m stub at the
        // end. Walk back to the last segment long enough to carry a real bearing.
        for (int i = path.Count - 1; i > 0; i--)
        {
            Vector3 segment = Vector3.ProjectOnPlane(path[i] - path[i - 1], Vector3.up);
            if (segment.sqrMagnitude >= 9f)
                return segment.normalized;
        }
        return Vector3.zero;
    }

    static Vector3 FirstPathDirection(AIRoadGraph graph, Vector3 from, Vector3 to)
    {
        if (graph == null || !graph.TryFindPathWorld(from, to, out var path) || path == null)
            return Vector3.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector3 segment = Vector3.ProjectOnPlane(path[i] - path[i - 1], Vector3.up);
            if (segment.sqrMagnitude >= 9f)
                return segment.normalized;
        }
        return Vector3.zero;
    }

    /// <summary>
    /// Nearest point on a polyline and that segment's tangent in the polyline's own
    /// stored order. For a route path that order is the direction the player drives,
    /// which is what the kerb side has to be measured against.
    /// </summary>
    public static bool NearestPathFrame(Vector3[] points, Vector3 requested,
        out Vector3 point, out Vector3 tangent)
    {
        point = requested; tangent = Vector3.forward;
        if (points == null || points.Length < 2) return false;
        float best = float.MaxValue;
        bool found = false;
        for (int i = 1; i < points.Length; i++)
        {
            Vector3 a = points[i - 1], b = points[i];
            a.y = b.y = requested.y;
            Vector3 segment = b - a;
            if (segment.sqrMagnitude < 1f) continue;
            Vector3 projected = ProjectOnSegment(requested, a, b);
            float distance = (projected - requested).sqrMagnitude;
            if (distance >= best) continue;
            best = distance;
            point = projected;
            tangent = segment.normalized;
            found = true;
        }
        return found;
    }

    public static Vector3 PathHeading(Vector3[] points, Vector3 requested, Vector3 fallback,
        Vector3 approachHint = default)
    {
        if (points == null || points.Length < 2) return fallback;

        Vector3 expectedFlow = approachHint.sqrMagnitude >= 0.01f
            ? Vector3.ProjectOnPlane(approachHint, Vector3.up).normalized
            : Vector3.zero;

        float bestScore = float.MaxValue;
        Vector3 bestHeading = fallback;
        bool found = false;

        for (int i = 1; i < points.Length; i++)
        {
            Vector3 a = points[i - 1], b = points[i];
            a.y = b.y = requested.y;
            Vector3 segment = b - a;
            if (segment.sqrMagnitude < 0.5f) continue;

            Vector3 tangent = segment.normalized;

            // Reject any segment running backwards against expected route flow
            float alignment = expectedFlow.sqrMagnitude > 0.01f ? Vector3.Dot(tangent, expectedFlow) : 1f;
            if (expectedFlow.sqrMagnitude > 0.01f && alignment < -0.1f)
                continue; // Opposing / oncoming segment from return leg or parallel lane

            Vector3 projected = ProjectOnSegment(requested, a, b);
            float distSq = (projected - requested).sqrMagnitude;
            if (distSq > 150f * 150f) continue;

            // Score penalizes distance and misalignment with expected travel flow
            float alignmentPenalty = expectedFlow.sqrMagnitude > 0.01f ? (1f - alignment) * 400f : 0f;
            float score = distSq + alignmentPenalty;

            if (score < bestScore)
            {
                bestScore = score;
                bestHeading = tangent;
                found = true;
            }
        }

        if (found) return bestHeading;

        // Fallback: If no aligned segment was found within range, use NearestPathFrame
        // but ensure tangent points along expectedFlow
        if (NearestPathFrame(points, requested, out _, out Vector3 nearestTangent))
        {
            if (expectedFlow.sqrMagnitude > 0.01f && Vector3.Dot(nearestTangent, expectedFlow) < 0f)
                nearestTangent = -nearestTangent;
            return nearestTangent;
        }

        return fallback;
    }
}
