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
            if (route == null || route.graph == null) continue;
            if (route.nodeSequence == null || route.nodeSequence.Length < 2) continue;

            if (activeByRoute[i] >= Mathf.Max(1, route.maxConcurrentBuses))
                continue;

            if (now >= nextSpawnTimeByRoute[i])
            {
                SpawnBusOnRoute(i, route);
                nextSpawnTimeByRoute[i] = now + Mathf.Max(2f, route.headwayMinutes);
            }
        }
    }

    void SpawnBusOnRoute(int routeIndex, ScheduledAIBusRoute route)
    {
        int startNode = route.nodeSequence[0];
        if (!route.graph.IsValidNode(startNode)) return;

        GameObject go = Instantiate(aiBusPrefab, route.graph.GetNodePosition(startNode), Quaternion.identity, transform);
        var ai = go.GetComponent<AIVehicleController>();
        if (ai == null) ai = go.AddComponent<AIVehicleController>();
        ai.Init(route.graph, startNode, 0, AIVehicleController.VehicleType.Bus);

        var runner = go.GetComponent<AIBusRouteRunner>();
        if (runner == null) runner = go.AddComponent<AIBusRouteRunner>();
        runner.Init(route.nodeSequence, () => activeByRoute[routeIndex] = Mathf.Max(0, activeByRoute[routeIndex] - 1));

        activeByRoute[routeIndex]++;
    }
}

public class AIBusRouteRunner : MonoBehaviour
{
    int[] sequence;
    int index;
    System.Action onComplete;
    AIVehicleController ai;

    public void Init(int[] nodeSequence, System.Action complete)
    {
        sequence = nodeSequence;
        onComplete = complete;
        ai = GetComponent<AIVehicleController>();
        index = 0;
    }

    void Update()
    {
        if (ai == null || sequence == null || sequence.Length < 2) return;
        if (index >= sequence.Length - 1)
        {
            onComplete?.Invoke();
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
}
