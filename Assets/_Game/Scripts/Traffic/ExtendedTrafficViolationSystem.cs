using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurnRestrictionDefinition
{
    public string fromNodeId;
    public string viaNodeId;
    public string toNodeId;
}

public enum TrafficViolationType
{
    Speeding,
    IllegalTurn,
    RedLight,
    YellowLight,
    PedestrianConflict,
    JunctionBlocking,
    OffRouteDeviation
}

[System.Serializable]
public class TrafficViolationRecord
{
    public TrafficViolationType type;
    public float penalty;
    public string description;
}

public class ExtendedTrafficViolationSystem : MonoBehaviour
{
    public static ExtendedTrafficViolationSystem Instance { get; private set; }

    [Header("References")]
    public BusController busController;
    public AIRoadGraph roadGraph;

    [Header("Optional OSM Turn Restrictions")]
    public TurnRestrictionDefinition[] turnRestrictions;

    [Header("Thresholds")]
    public float speedingGraceKmh = 3f;
    public float pedestrianConflictDistanceMeters = 4f;
    public float junctionBlockingRadiusMeters = 12f;
    public float offRouteDistanceMeters = 50f;
    public float offRouteDurationSeconds = 10f;

    readonly List<TrafficViolationRecord> violations = new List<TrafficViolationRecord>();
    readonly Dictionary<TrafficViolationType, float> lastViolationTime = new Dictionary<TrafficViolationType, float>();

    float speedingTimer;
    float stationaryIntersectionTimer;
    float offRouteTimer;
    int lastNearestNode = -1;
    int previousNearestNode = -1;

    public IReadOnlyList<TrafficViolationRecord> Violations => violations;
    public int TotalViolationCount => violations.Count;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this); return; }
        Instance = this;
    }

    void Start()
    {
        if (busController == null)
            busController = FindFirstObjectByType<BusController>();
        if (roadGraph == null)
            roadGraph = FindFirstObjectByType<AIRoadGraph>();
    }

    void Update()
    {
        if (busController == null)
            return;

        TrackSpeeding();
        TrackIllegalTurns();
        TrackPedestrianConflicts();
        TrackJunctionBlocking();
        TrackOffRouteDeviation();
    }

    public void RecordSignalViolation(bool redLight)
    {
        if (redLight)
        {
            RecordViolation(TrafficViolationType.RedLight, 500f, "Red light violation (-500 points)");
            ScoreTracker.Instance?.RecordRedLight();
        }
        else
        {
            RecordViolation(TrafficViolationType.YellowLight, 4f, "Crossed stop line during amber");
            ScoreTracker.Instance?.ApplyTrafficPenalty(4f, 1f, 0f);
        }
    }

    public int GetViolationCount(TrafficViolationType type)
    {
        int count = 0;
        for (int i = 0; i < violations.Count; i++)
            if (violations[i].type == type)
                count++;
        return count;
    }

    public void ResetViolationLog()
    {
        violations.Clear();
        lastViolationTime.Clear();
        speedingTimer = 0f;
        stationaryIntersectionTimer = 0f;
        offRouteTimer = 0f;
        lastNearestNode = -1;
        previousNearestNode = -1;
    }

    public string GetViolationSummary()
    {
        if (violations.Count == 0)
            return "No violations";

        return
            $"SPD {GetViolationCount(TrafficViolationType.Speeding)} | " +
            $"TURN {GetViolationCount(TrafficViolationType.IllegalTurn)} | " +
            $"SIG {GetViolationCount(TrafficViolationType.RedLight) + GetViolationCount(TrafficViolationType.YellowLight)} | " +
            $"PED {GetViolationCount(TrafficViolationType.PedestrianConflict)} | " +
            $"JCT {GetViolationCount(TrafficViolationType.JunctionBlocking)} | " +
            $"OFF {GetViolationCount(TrafficViolationType.OffRouteDeviation)}";
    }

    void TrackSpeeding()
    {
        if (roadGraph == null)
            return;

        int nearestNode = roadGraph.GetNearestNodeIndex(busController.transform.position);
        if (!roadGraph.IsValidNode(nearestNode))
            return;

        float limit = roadGraph.GetSpeedLimitKmh(nearestNode);
        float margin = busController.currentSpeedKmh - limit;
        if (margin > speedingGraceKmh)
        {
            speedingTimer += Time.deltaTime;
            if (speedingTimer >= 2f)
            {
                float penalty = Mathf.Clamp(2f + (margin * 0.45f), 2f, 14f);
                RecordViolation(TrafficViolationType.Speeding, penalty, $"Speeding by {margin:F0} km/h");
                ScoreTracker.Instance?.ApplyTrafficPenalty(penalty * 0.6f, penalty * 0.4f, 0f);
                speedingTimer = 0f;
            }
        }
        else
        {
            speedingTimer = 0f;
        }
    }

    void TrackIllegalTurns()
    {
        if (roadGraph == null || turnRestrictions == null || turnRestrictions.Length == 0)
            return;

        int nearestNode = roadGraph.GetNearestNodeIndex(busController.transform.position);
        if (!roadGraph.IsValidNode(nearestNode) || nearestNode == lastNearestNode)
            return;

        previousNearestNode = lastNearestNode;
        lastNearestNode = nearestNode;

        if (!roadGraph.IsValidNode(previousNearestNode))
            return;

        for (int i = 0; i < turnRestrictions.Length; i++)
        {
            var restriction = turnRestrictions[i];
            if (restriction == null)
                continue;

            string previousId = roadGraph.nodes[previousNearestNode].id;
            string currentId = roadGraph.nodes[lastNearestNode].id;
            if (previousId == restriction.viaNodeId || currentId == restriction.viaNodeId)
            {
                if (previousId == restriction.fromNodeId && currentId == restriction.toNodeId)
                {
                    RecordViolation(TrafficViolationType.IllegalTurn, 10f, $"Illegal turn {restriction.fromNodeId}->{restriction.toNodeId}");
                    ScoreTracker.Instance?.ApplyTrafficPenalty(8f, 0f, 3f);
                }
            }
        }
    }

    void TrackPedestrianConflicts()
    {
        if (PedestrianSpawner.Instance == null)
            return;

        var activePedestrians = PedestrianSpawner.Instance.GetActivePedestrians();
        for (int i = 0; i < activePedestrians.Count; i++)
        {
            var pedestrian = activePedestrians[i];
            if (pedestrian == null)
                continue;

            float distance = Vector3.Distance(busController.transform.position, pedestrian.transform.position);
            if (distance <= pedestrianConflictDistanceMeters && pedestrian.IsInsideCrossingZone)
            {
                RecordViolation(TrafficViolationType.PedestrianConflict, 15f, "Pedestrian conflict in crossing zone");
                ScoreTracker.Instance?.ApplyTrafficPenalty(15f, 0f, 4f);
                return;
            }
        }
    }

    void TrackJunctionBlocking()
    {
        if (roadGraph == null)
            return;

        int nearestNode = roadGraph.GetNearestNodeIndex(busController.transform.position);
        if (!roadGraph.IsValidNode(nearestNode))
            return;

        bool atIntersection = roadGraph.nodes[nearestNode].nextNodeIndices != null &&
                              roadGraph.nodes[nearestNode].nextNodeIndices.Length >= 3 &&
                              Vector3.Distance(busController.transform.position, roadGraph.GetNodePosition(nearestNode)) <= junctionBlockingRadiusMeters;

        if (atIntersection && busController.currentSpeedKmh < 1f)
        {
            stationaryIntersectionTimer += Time.deltaTime;
            if (stationaryIntersectionTimer >= 3f)
            {
                RecordViolation(TrafficViolationType.JunctionBlocking, 8f, "Blocking junction while stationary");
                ScoreTracker.Instance?.ApplyTrafficPenalty(7f, 0f, 2f);
                stationaryIntersectionTimer = 0f;
            }
        }
        else
        {
            stationaryIntersectionTimer = 0f;
        }
    }

    void TrackOffRouteDeviation()
    {
        BusRoute route = MissionManager.Instance != null ? MissionManager.Instance.currentRoute : GameState.Instance?.selectedRoute;
        if (route == null)
            return;

        float distance = MissionManager.Instance != null
            ? MissionManager.Instance.GetDistanceToGuidancePath(busController.transform.position)
            : GetDistanceToRoute(route, busController.transform.position);
        if (distance > offRouteDistanceMeters)
        {
            offRouteTimer += Time.deltaTime;
            if (offRouteTimer >= offRouteDurationSeconds)
            {
                RecordViolation(TrafficViolationType.OffRouteDeviation, 12f, $"Off-route deviation {distance:F0}m");
                ScoreTracker.Instance?.ApplyTrafficPenalty(5f, 5f, 6f);
                offRouteTimer = 0f;
            }
        }
        else
        {
            offRouteTimer = 0f;
        }
    }

    float GetDistanceToRoute(BusRoute route, Vector3 worldPosition)
    {
        float best = float.MaxValue;

        if (route.geometryLatLonFlat != null && route.geometryLatLonFlat.Length >= 4 && CoordinateConverter.Instance != null)
        {
            int pointCount = route.geometryLatLonFlat.Length / 2;
            Vector3 previous = CoordinateConverter.Instance.GeoToWorldPosition(route.geometryLatLonFlat[0], route.geometryLatLonFlat[1]);

            for (int i = 1; i < pointCount; i++)
            {
                Vector3 current = CoordinateConverter.Instance.GeoToWorldPosition(route.geometryLatLonFlat[i * 2], route.geometryLatLonFlat[i * 2 + 1]);
                Vector3 projected = ClosestPointOnSegment(worldPosition, previous, current);
                best = Mathf.Min(best, Vector3.Distance(worldPosition, projected));
                previous = current;
            }
        }
        else if (route.stops != null && route.stops.Length > 1 && GPSManager.Instance != null)
        {
            Vector3 previous = GPSManager.Instance.GpsToWorld(route.stops[0].latitude, route.stops[0].longitude);
            for (int i = 1; i < route.stops.Length; i++)
            {
                Vector3 current = GPSManager.Instance.GpsToWorld(route.stops[i].latitude, route.stops[i].longitude);
                Vector3 projected = ClosestPointOnSegment(worldPosition, previous, current);
                best = Mathf.Min(best, Vector3.Distance(worldPosition, projected));
                previous = current;
            }
        }

        return best < float.MaxValue ? best : 0f;
    }

    Vector3 ClosestPointOnSegment(Vector3 point, Vector3 start, Vector3 end)
    {
        Vector3 segment = end - start;
        float t = Vector3.Dot(point - start, segment) / Mathf.Max(0.0001f, Vector3.Dot(segment, segment));
        return start + segment * Mathf.Clamp01(t);
    }

    void RecordViolation(TrafficViolationType type, float penalty, string description)
    {
        if (IsOnCooldown(type))
            return;

        violations.Add(new TrafficViolationRecord
        {
            type = type,
            penalty = penalty,
            description = description
        });

        lastViolationTime[type] = Time.time;
        Debug.Log($"[TrafficViolation] {description}");
    }

    bool IsOnCooldown(TrafficViolationType type)
    {
        if (!lastViolationTime.TryGetValue(type, out float lastTime))
            return false;

        float cooldown = type == TrafficViolationType.Speeding ? 2f : 4f;
        return Time.time - lastTime < cooldown;
    }
}
