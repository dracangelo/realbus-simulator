using System.Collections;
using UnityEngine;

/// <summary>Converts the provider-neutral directed road graph into AI waypoints in bounded batches.</summary>
public class RoadTrafficBridge : MonoBehaviour
{
    public AIRoadGraph Graph { get; private set; }
    public IEnumerator Build(RoadGraph roads)
    {
        Graph = gameObject.AddComponent<AIRoadGraph>();
        Graph.nodes = new AIRoadGraph.RoadNode[roads.nodes.Count];
        for (int i = 0; i < roads.nodes.Count; i++)
        {
            var source = roads.nodes[i];
            var point = new GameObject("Road node " + i).transform;
            point.SetParent(transform, false); point.position = source.world + Vector3.up * 0.25f;
            var next = new int[source.edges.Count];
            float speed = 50f;
            for (int j = 0; j < next.Length; j++) { next[j] = source.edges[j].to; speed = Mathf.Min(speed, source.edges[j].speedLimitKmh); }
            // Lane counts aren't retained in RoadGraph. A single lane prevents invented passing permissions.
            Graph.nodes[i] = new AIRoadGraph.RoadNode { id = i.ToString(), point = point, laneCount = 1, speedLimitKmh = speed, nextNodeIndices = next };
            if (i % 100 == 99) yield return null;
        }
        foreach (var signal in FindObjectsByType<TrafficLight>(FindObjectsSortMode.None))
        {
            int index = Graph.GetNearestNodeIndex(signal.transform.position);
            if (index >= 0 && Vector3.Distance(Graph.GetNodePosition(index), signal.transform.position) < 25f)
                Graph.nodes[index].trafficLight = signal;
        }
    }
}
