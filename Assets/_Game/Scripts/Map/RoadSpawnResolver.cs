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
        return leftHandTraffic ? -offset : offset;
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

    public static Vector3 RouteHeading(BusRoute route, Vector3 requested, Vector3 fallback)
    {
        if (route == null) return fallback;
        var geometry = route.geometryLatLonFlat;
        if (GPSManager.Instance != null && geometry != null && geometry.Length >= 4)
        {
            var points = new Vector3[geometry.Length / 2];
            for (int i = 0; i < points.Length; i++) points[i] = GPSManager.Instance.GpsToWorld(geometry[i * 2], geometry[i * 2 + 1]);
            return PathHeading(points, requested, fallback);
        }
        return PathHeading(route.pathPoints, requested, fallback);
    }

    public static Vector3 PathHeading(Vector3[] points, Vector3 requested, Vector3 fallback)
    {
        float nearest = 150f * 150f;
        Vector3 heading = fallback;
        if (points == null) return heading;
        for (int i = 1; i < points.Length; i++)
        {
            Vector3 a = points[i - 1], b = points[i]; a.y = b.y = requested.y;
            if ((b - a).sqrMagnitude < 1f) continue;
            float distance = (ProjectOnSegment(requested, a, b) - requested).sqrMagnitude;
            if (distance >= nearest) continue;
            nearest = distance;
            heading = (b - a).normalized;
        }
        return heading;
    }
}
