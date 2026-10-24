using UnityEngine;
using System.Collections.Generic;

public class AIBusScheduleSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ScheduledAIBusRoute
    {
        public string routeName = "NBO Ambient Service";
        public AIRoadGraph graph;
        public int[] nodeSequence;
        public bool useRandomGeneratedNodes = false;
        public int randomNodeCount = 8;
        public int startNodeIndex = -1;
        public float headwayMinutes = 12f;
        public int maxConcurrentBuses = 3;
    }

    [Header("Prefabs")]
    public GameObject aiBusPrefab;
    public ScheduledAIBusRoute[] routes;

    [Header("Spawn Safety")]
    [Tooltip("An ambient bus is never placed closer than this to the player's bus.")]
    public float minPlayerSpawnDistanceMeters = 80f;
    [Tooltip("An ambient bus is never placed in the player's forward corridor within this distance.")]
    public float playerAheadClearanceMeters = 120f;
    public float playerAheadHalfWidthMeters = 10f;
    [Tooltip("Minimum gap between a spawned bus and any other active traffic body.")]
    public float vehicleSpawnClearanceMeters = 18f;
    [Tooltip("Real-time seconds to wait before retrying a route whose spawn point was blocked.")]
    public float blockedRetrySeconds = 5f;

    readonly Dictionary<int, int[]> routeSequences = new Dictionary<int, int[]>();
    readonly Stack<GameObject> spare = new Stack<GameObject>();
    readonly Dictionary<int, float> nextSpawnTimeByRoute = new Dictionary<int, float>();
    readonly Dictionary<int, float> retryAfterRealtime = new Dictionary<int, float>();
    readonly Dictionary<int, int> activeByRoute = new Dictionary<int, int>();
    Transform playerBus;
    Vector2 aiBusSize = new Vector2(2.5f, 12f);

    void Start()
    {
        if (routes == null || routes.Length == 0) return;

        if (aiBusPrefab != null)
            aiBusSize = VehiclePool.MeasurePrefabSize(aiBusPrefab);

        // Freeze the side of the road before any bus is placed.
        for (int i = 0; i < routes.Length; i++)
        {
            if (routes[i] == null) continue;
            routes[i].routeName = RouteNaming.NormalizeName(routes[i].routeName);
            if (routes[i].graph != null)
                routes[i].graph.SyncDrivingSide();
        }

        ResolvePlayerBus();

        int capacity = 0;
        foreach (var route in routes) if (route != null) capacity += Mathf.Max(1, route.maxConcurrentBuses);
        if (aiBusPrefab != null) for (int i = 0; i < capacity; i++)
        {
            var go = Instantiate(aiBusPrefab, transform);
            // An ambient bus is not drivable: no wheel colliders, no cooked mesh
            // collider and no per-bus audio are needed for a body on a schedule.
            TrafficBodyOptimizer.Prepare(go);
            go.SetActive(false);
            spare.Push(go);
        }
        float now = ScheduleManager.Instance != null ? ScheduleManager.Instance.currentTimeMinutes : 8f * 60f;
        for (int i = 0; i < routes.Length; i++)
        {
            nextSpawnTimeByRoute[i] = now + Random.Range(0f, 3f);
            activeByRoute[i] = 0;
        }
    }

    void Update()
    {
        if (aiBusPrefab == null || routes == null) return;

        ResolvePlayerBus();
        float now = ScheduleManager.Instance != null ? ScheduleManager.Instance.currentTimeMinutes : 8f * 60f;
        for (int i = 0; i < routes.Length; i++)
        {
            var route = routes[i];
            int[] resolvedSequence = ResolveNodeSequence(route, i);
            if (resolvedSequence == null || resolvedSequence.Length < 2) continue;
            EnsureRouteState(i, now);

            if (activeByRoute[i] >= Mathf.Max(1, route.maxConcurrentBuses))
                continue;

            if (now >= nextSpawnTimeByRoute[i] && spare.Count > 0 &&
                AIVehicleController.ActiveCount < TrafficPerformancePolicy.FullAiCeiling)
            {
                // A blocked spawn is retried on a real-time timer rather than a whole
                // headway: ambient buses should fill a gap in the street, not skip it.
                retryAfterRealtime.TryGetValue(i, out float retryAt);
                if (Time.time < retryAt) continue;

                bool spawned = SpawnBusOnRoute(i, route, resolvedSequence);
                if (spawned)
                {
                    nextSpawnTimeByRoute[i] = now + Mathf.Max(2f, route.headwayMinutes);
                    retryAfterRealtime[i] = 0f;
                }
                else
                {
                    retryAfterRealtime[i] = Time.time + Mathf.Max(1f, blockedRetrySeconds);
                }
            }
        }
    }

    bool SpawnBusOnRoute(int routeIndex, ScheduledAIBusRoute route, int[] sequence)
    {
        if (route == null || route.graph == null || sequence == null || sequence.Length < 2 || spare.Count == 0)
            return false;

        if (!TryPickSpawnLink(route.graph, sequence, out int startIndex, out int lane, out _))
            return false;

        GameObject go = spare.Pop();
        if (go == null) return false;

        var ai = go.GetComponent<AIVehicleController>();
        if (ai == null) ai = go.AddComponent<AIVehicleController>();
        ai.vehicleWidth = aiBusSize.x;
        ai.vehicleLength = aiBusSize.y;
        var spline = go.GetComponent<SplineVehicle>();
        if (spline == null) spline = go.AddComponent<SplineVehicle>();
        spline.vehicleWidth = aiBusSize.x;
        var rb = go.GetComponent<Rigidbody>();
        if (rb == null) rb = go.AddComponent<Rigidbody>();
        var participant = go.GetComponent<TrafficParticipant>();
        if (participant == null) participant = go.AddComponent<TrafficParticipant>();
        participant.width = aiBusSize.x;
        participant.length = aiBusSize.y;
        if (go.GetComponent<ScheduledTrafficTier>() == null) go.AddComponent<ScheduledTrafficTier>();

        int startNode = sequence[startIndex];
        int targetNode = sequence[startIndex + 1];

        // Place the body on its own lane while it is still inactive, so the first
        // visible frame is already at the spawn point.
        ai.Init(route.graph, startNode, lane, AIVehicleController.VehicleType.Bus);
        ai.PlaceOnLane(startNode, targetNode, lane);
        ai.targetNodeIndex = targetNode;
        ai.currentSpeedKmh = 0f;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.detectCollisions = true;
            rb.constraints |= RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Starting mid-route is fine for ambient traffic, and it means a blocked
        // stop near the player does not permanently stall the whole route.
        int[] runSequence = sequence;
        if (startIndex > 0)
        {
            runSequence = new int[sequence.Length - startIndex];
            System.Array.Copy(sequence, startIndex, runSequence, 0, runSequence.Length);
        }

        var runner = go.GetComponent<AIBusRouteRunner>();
        if (runner == null) runner = go.AddComponent<AIBusRouteRunner>();
        runner.Init(runSequence, () =>
        {
            activeByRoute[routeIndex] = Mathf.Max(0, activeByRoute[routeIndex] - 1);
            if (go != null) { go.SetActive(false); spare.Push(go); }
        });

        go.SetActive(true);
        activeByRoute[routeIndex]++;
        return true;
    }

    /// <summary>
    /// Chooses a link on the route whose lane point is clear of the player and of
    /// every other traffic body. Tries links in a random order so two buses of the
    /// same route do not always appear at the same junction.
    /// </summary>
    bool TryPickSpawnLink(AIRoadGraph graph, int[] sequence, out int startIndex, out int lane, out Vector3 point)
    {
        startIndex = -1;
        lane = 0;
        point = Vector3.zero;
        if (graph == null || sequence == null || sequence.Length < 2)
            return false;

        int linkCount = sequence.Length - 1;
        int first = linkCount > 1 ? Random.Range(0, linkCount) : 0;
        for (int offset = 0; offset < linkCount; offset++)
        {
            int index = (first + offset) % linkCount;
            int from = sequence[index];
            int to = sequence[index + 1];
            if (!graph.IsValidNode(from) || !graph.IsValidNode(to))
                continue;

            // A bus is 10-12 m long; a link shorter than that has no room for it.
            if (Vector3.Distance(graph.GetNodePosition(from), graph.GetNodePosition(to)) < 8f)
                continue;

            int laneCount = Mathf.Max(1, graph.GetLaneCount(from, to));
            int candidateLane = Random.Range(0, laneCount);
            Vector3 candidatePoint = GetLanePoint(graph, from, to, candidateLane);
            if (!IsSpawnPointClear(candidatePoint))
                continue;

            startIndex = index;
            lane = candidateLane;
            point = candidatePoint;
            return true;
        }

        return false;
    }

    static Vector3 GetLanePoint(AIRoadGraph graph, int fromNode, int toNode, int lane)
    {
        Vector3 point = graph.GetNodePosition(fromNode);
        Vector3 segment = graph.GetNodePosition(toNode) - point;
        segment.y = 0f;
        if (segment.sqrMagnitude < .0001f)
            return point;

        segment.Normalize();
        return point + Vector3.Cross(Vector3.up, segment) * graph.GetLaneOffset(fromNode, toNode, lane);
    }

    bool IsSpawnPointClear(Vector3 point)
    {
        if (playerBus != null)
        {
            Vector3 offset = point - playerBus.position;
            offset.y = 0f;
            if (offset.magnitude < minPlayerSpawnDistanceMeters)
                return false;

            // A legal straight-line distance is not enough when the point is dead
            // ahead: at road speed the bus would reach it before it could react.
            float ahead = Vector3.Dot(playerBus.forward, offset);
            float lateral = Mathf.Abs(Vector3.Dot(playerBus.right, offset));
            if (ahead > 0f && ahead < playerAheadClearanceMeters && lateral < playerAheadHalfWidthMeters)
                return false;
        }

        return TrafficParticipant.IsSpawnAreaClear(point, vehicleSpawnClearanceMeters);
    }

    void ResolvePlayerBus()
    {
        var mission = MissionManager.Instance;
        if (mission != null && mission.busController != null && mission.busController.gameObject.activeInHierarchy)
        {
            playerBus = mission.busController.transform;
            return;
        }

        if (playerBus != null) return;
        var bus = FindAnyObjectByType<BusController>();
        if (bus != null) playerBus = bus.transform;
    }

    void EnsureRouteState(int routeIndex, float now)
    {
        if (!nextSpawnTimeByRoute.ContainsKey(routeIndex))
            nextSpawnTimeByRoute[routeIndex] = now + Random.Range(0f, 3f);
        if (!activeByRoute.ContainsKey(routeIndex))
            activeByRoute[routeIndex] = 0;
    }

    int[] ResolveNodeSequence(ScheduledAIBusRoute route, int routeIndex)
    {
        if (route == null || route.graph == null)
            return null;

        if (routeSequences.TryGetValue(routeIndex, out var cached)) return cached;
        int[] sequence = route.nodeSequence;
        if (route.useRandomGeneratedNodes)
            sequence = route.graph.BuildRandomNodeSequence(route.randomNodeCount, route.startNodeIndex);

        if (sequence == null || sequence.Length < 2)
        {
            if (route.useRandomGeneratedNodes)
                Debug.LogWarning($"[AIBusScheduleSpawner] Route {routeIndex} ({route.routeName}) could not generate a valid random node sequence.");
            return null;
        }

        for (int i = 0; i < sequence.Length; i++)
        {
            if (!route.graph.IsValidNode(sequence[i]))
            {
                Debug.LogWarning($"[AIBusScheduleSpawner] Route {routeIndex} ({route.routeName}) has invalid node index {sequence[i]}.");
                return null;
            }
        }

        for (int i = 0; i < sequence.Length - 1; i++)
        {
            if (!IsConnected(route.graph, sequence[i], sequence[i + 1]))
            {
                Debug.LogWarning($"[AIBusScheduleSpawner] Route {routeIndex} ({route.routeName}) contains an unlinked node hop {sequence[i]} -> {sequence[i + 1]}.");
                return null;
            }
        }

        routeSequences[routeIndex] = sequence;
        return sequence;
    }

    bool IsConnected(AIRoadGraph graph, int fromNode, int toNode)
    {
        if (!graph.IsValidNode(fromNode) || !graph.IsValidNode(toNode))
            return false;

        int[] next = graph.nodes[fromNode].nextNodeIndices;
        if (next == null) return false;
        for (int i = 0; i < next.Length; i++)
            if (next[i] == toNode)
                return true;

        return false;
    }
}

public class AIBusRouteRunner : MonoBehaviour
{
    int[] sequence;
    int index;
    System.Action onComplete;
    AIVehicleController ai;
    bool released;

    public void Init(int[] nodeSequence, System.Action complete)
    {
        sequence = nodeSequence;
        onComplete = complete;
        ai = GetComponent<AIVehicleController>();
        index = 0;
        released = false;

        if (ai != null && sequence != null && sequence.Length > 1)
            ai.targetNodeIndex = sequence[1];
    }

    void Update()
    {
        if (ai == null || sequence == null || sequence.Length < 2) return;
        if (index >= sequence.Length - 1)
        {
            Release();
            gameObject.SetActive(false);
            return;
        }

        var spline = GetComponent<SplineVehicle>();
        int current = ai.enabled ? ai.currentNodeIndex : spline != null ? spline.currentNodeIndex : ai.currentNodeIndex;
        int target = sequence[index + 1];
        if (current == target) { index++; if (index >= sequence.Length - 1) return; target = sequence[index + 1]; }
        ai.targetNodeIndex = target;
        if (spline != null) spline.targetNodeIndex = target;
    }

    public void Despawn() { Release(); gameObject.SetActive(false); }

    void OnDestroy()
    {
        Release();
    }

    void Release()
    {
        if (released) return;
        released = true;
        onComplete?.Invoke();
    }
}
