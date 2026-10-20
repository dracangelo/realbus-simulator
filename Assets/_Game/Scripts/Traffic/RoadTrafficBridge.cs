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
            var laneCounts = new int[source.edges.Count];
            var laneWidths = new float[source.edges.Count];
            var oneWays = new bool[source.edges.Count];
            var sharedLanes = new bool[source.edges.Count];
            float speed = 50f;
            for (int j = 0; j < next.Length; j++)
            {
                var edge = source.edges[j]; next[j] = edge.to; speed = Mathf.Min(speed, edge.speedLimitKmh);
                laneCounts[j] = Mathf.Max(1, edge.laneCount); laneWidths[j] = Mathf.Max(2.6f, edge.laneWidth);
                oneWays[j] = edge.oneWay; sharedLanes[j] = edge.sharedLane;
            }
            Graph.nodes[i] = new AIRoadGraph.RoadNode { id = i.ToString(), point = point,
                laneCount = laneCounts.Length > 0 ? laneCounts[0] : 1, laneWidth = laneWidths.Length > 0 ? laneWidths[0] : 3.1f,
                speedLimitKmh = speed, nextNodeIndices = next, outgoingLaneCounts = laneCounts, outgoingLaneWidths = laneWidths,
                outgoingOneWays = oneWays, outgoingSharedLanes = sharedLanes };
            if (i % 100 == 99) yield return null;
        }
        foreach (var signal in FindObjectsByType<TrafficLight>())
        {
            int index = Graph.GetNearestNodeIndex(signal.transform.position);
            if (index >= 0 && Vector3.Distance(Graph.GetNodePosition(index), signal.transform.position) < 25f)
                Graph.nodes[index].trafficLight = signal;
        }
    }
}
