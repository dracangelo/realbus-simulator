using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Guards the streaming order and the road-graph geometry the traffic pool
/// depends on: visible work first, and lane offsets that keep a car on its own
/// carriageway instead of the centre line.
/// </summary>
public class StreamingPriorityTests
{
    [Test]
    public void ViewPriority_PrefersTheConeAhead()
    {
        Vector3 viewer = Vector3.zero;
        Vector3 forward = Vector3.forward;
        Vector3 ahead = new Vector3(0f, 0f, 100f);
        Vector3 behind = new Vector3(0f, 0f, -100f);
        Vector3 side = new Vector3(100f, 0f, 0f);

        Assert.That(ViewPriority.IsInsideView(viewer, forward, 90f, 200f, ahead), Is.True);
        Assert.That(ViewPriority.IsInsideView(viewer, forward, 90f, 200f, behind), Is.False);
        Assert.That(ViewPriority.IsInsideView(viewer, forward, 90f, 80f, ahead), Is.False);

        Assert.That(ViewPriority.Score(viewer, forward, 90f, ahead), Is.LessThan(ViewPriority.Score(viewer, forward, 90f, side)));
        Assert.That(ViewPriority.Score(viewer, forward, 90f, side), Is.LessThan(ViewPriority.Score(viewer, forward, 90f, behind)));
    }

    [Test]
    public void RoadGraph_CollectNodesWithin_ReturnsTheLocalStreetsOnly()
    {
        var root = new GameObject("RoadGraphTest");
        var graph = root.AddComponent<AIRoadGraph>();
        try
        {
            graph.nodes = new[]
            {
                Node(root.transform, Vector3.zero, 1),
                Node(root.transform, new Vector3(100f, 0f, 0f), 0),
                Node(root.transform, new Vector3(1000f, 0f, 0f), 0)
            };
            var results = new List<int>();
            graph.CollectNodesWithin(Vector3.zero, 150f, results);

            Assert.That(results, Is.EquivalentTo(new[] { 0, 1 }));
        }
        finally
        {
            Object.DestroyImmediate(root);
        }
    }

    [Test]
    public void LaneOffset_KeepsTwoWayTrafficOnItsOwnSide()
    {
        var root = new GameObject("LaneOffsetTest");
        var graph = root.AddComponent<AIRoadGraph>();
        try
        {
            graph.drivesOnLeft = true;
            graph.nodes = new[]
            {
                Node(root.transform, Vector3.zero, 1, laneWidth: 3.1f, oneWay: false),
                Node(root.transform, new Vector3(50f, 0f, 0f), 0, laneWidth: 3.1f, oneWay: false)
            };

            // The offset is measured from the middle of each direction's own
            // carriageway, so both directions use the same scalar offset while
            // the world-space lateral vector flips with the link direction.
            Assert.That(graph.GetLaneOffset(0, 1, 0), Is.EqualTo(-1.55f).Within(0.001f));
            Assert.That(graph.GetLaneOffset(1, 0, 0), Is.EqualTo(-1.55f).Within(0.001f));
        }
        finally
        {
            Object.DestroyImmediate(root);
        }
    }

    [Test]
    public void LaneOffset_CentresSharedAndSpreadsOneWayLanes()
    {
        var root = new GameObject("LaneOffsetKindsTest");
        var graph = root.AddComponent<AIRoadGraph>();
        try
        {
            graph.drivesOnLeft = true;
            graph.nodes = new[]
            {
                Node(root.transform, Vector3.zero, 1, laneWidth: 3.2f, oneWay: true, lanes: 2),
                Node(root.transform, new Vector3(50f, 0f, 0f), 0, laneWidth: 3.2f, oneWay: true, lanes: 2)
            };

            Assert.That(graph.GetLaneOffset(0, 1, 0), Is.EqualTo(-1.6f).Within(0.001f));
            Assert.That(graph.GetLaneOffset(0, 1, 1), Is.EqualTo(1.6f).Within(0.001f));

            graph.nodes[0].outgoingSharedLanes[0] = true;
            Assert.That(graph.GetLaneOffset(0, 1, 0), Is.EqualTo(0f).Within(0.001f));
        }
        finally
        {
            Object.DestroyImmediate(root);
        }
    }

    static AIRoadGraph.RoadNode Node(Transform parent, Vector3 position, int next,
        float laneWidth = 3.1f, bool oneWay = false, int lanes = 1)
    {
        var point = new GameObject("node").transform;
        point.SetParent(parent, false);
        point.position = position;
        return new AIRoadGraph.RoadNode
        {
            point = point,
            laneCount = lanes,
            laneWidth = laneWidth,
            nextNodeIndices = next >= 0 ? new[] { next } : new int[0],
            outgoingLaneCounts = next >= 0 ? new[] { lanes } : new int[0],
            outgoingLaneWidths = next >= 0 ? new[] { laneWidth } : new float[0],
            outgoingOneWays = next >= 0 ? new[] { oneWay } : new bool[0],
            outgoingSharedLanes = next >= 0 ? new bool[1] : new bool[0]
        };
    }
}
