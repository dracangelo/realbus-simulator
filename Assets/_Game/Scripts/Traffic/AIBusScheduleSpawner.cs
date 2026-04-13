using UnityEngine;
using System.Collections.Generic;

public class AIBusScheduleSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ScheduledAIBusRoute
    {
        public string routeName = "Route";
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

    readonly Dictionary<int, float> nextSpawnTimeByRoute = new Dictionary<int, float>();
    readonly Dictionary<int, int> activeByRoute = new Dictionary<int, int>();

    void Start()
    {
        if (routes == null || routes.Length == 0) return;

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

        float now = ScheduleManager.Instance != null ? ScheduleManager.Instance.currentTimeMinutes : 8f * 60f;
        for (int i = 0; i < routes.Length; i++)
        {
            var route = routes[i];
            int[] resolvedSequence = ResolveNodeSequence(route, i);
            if (resolvedSequence == null || resolvedSequence.Length < 2) continue;
            EnsureRouteState(i, now);

            if (activeByRoute[i] >= Mathf.Max(1, route.maxConcurrentBuses))
                continue;

            if (now >= nextSpawnTimeByRoute[i])
            {
                SpawnBusOnRoute(i, route, resolvedSequence);
                nextSpawnTimeByRoute[i] = now + Mathf.Max(2f, route.headwayMinutes);
            }
        }
    }

    void SpawnBusOnRoute(int routeIndex, ScheduledAIBusRoute route, int[] sequence)
    {
        int startNode = sequence[0];
        if (!route.graph.IsValidNode(startNode)) return;

        GameObject go = Instantiate(aiBusPrefab, route.graph.GetNodePosition(startNode), Quaternion.identity, transform);
        var ai = go.GetComponent<AIVehicleController>();
        if (ai == null) ai = go.AddComponent<AIVehicleController>();
        ai.Init(route.graph, startNode, 0, AIVehicleController.VehicleType.Bus);
        ai.targetNodeIndex = sequence[1];

        var runner = go.GetComponent<AIBusRouteRunner>();
        if (runner == null) runner = go.AddComponent<AIBusRouteRunner>();
        runner.Init(sequence, () => activeByRoute[routeIndex] = Mathf.Max(0, activeByRoute[routeIndex] - 1));

        activeByRoute[routeIndex]++;
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
            Destroy(gameObject);
            return;
        }

        int target = sequence[index + 1];
        if (ai.targetNodeIndex != target)
            ai.targetNodeIndex = target;

        // Advance when controller reaches this target node.
        if (ai.currentNodeIndex == target)
            index++;
    }

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
