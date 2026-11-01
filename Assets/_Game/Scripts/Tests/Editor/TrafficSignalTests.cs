using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Checks the junction signal wiring that the city rebuild now performs: every
/// approach of a crossing junction gets its own head bound to its own directed
/// link, the heads are driven by one controller, and the two axes are never
/// released at the same time.
/// </summary>
public class TrafficSignalTests
{
    readonly List<GameObject> created = new List<GameObject>();
    AIRoadGraph graph;

    // Node indices in BuildCrossJunction.
    const int Centre = 0;
    const int North = 1;
    const int East = 2;
    const int South = 3;
    const int West = 4;

    [TearDown]
    public void TearDown()
    {
        foreach (var gameObject in created)
            if (gameObject != null) Object.DestroyImmediate(gameObject);
        created.Clear();
        graph = null;
    }

    GameObject Track(GameObject gameObject)
    {
        created.Add(gameObject);
        return gameObject;
    }

    AIRoadGraph BuildCrossJunction()
    {
        var root = Track(new GameObject("TestGraph"));
        graph = root.AddComponent<AIRoadGraph>();
        graph.drivesOnLeft = true;

        graph.nodes = new[]
        {
            Node(root, "Centre", new Vector3(0f, .1f, 0f), North, East, South, West),
            Node(root, "North", new Vector3(0f, .1f, 60f), Centre),
            Node(root, "East", new Vector3(60f, .1f, 0f), Centre),
            Node(root, "South", new Vector3(0f, .1f, -60f), Centre),
            Node(root, "West", new Vector3(-60f, .1f, 0f), Centre)
        };
        return graph;
    }

    AIRoadGraph.RoadNode Node(GameObject root, string name, Vector3 position, params int[] next)
    {
        var point = Track(new GameObject(name));
        point.transform.SetParent(root.transform, false);
        point.transform.position = position;

        return new AIRoadGraph.RoadNode
        {
            id = name,
            point = point.transform,
            laneCount = 2,
            laneWidth = 3.2f,
            speedLimitKmh = 40f,
            trafficLight = null,
            nextNodeIndices = next,
            outgoingLaneCounts = new int[next.Length],
            outgoingLaneWidths = new float[next.Length],
            outgoingOneWays = new bool[next.Length],
            outgoingSharedLanes = new bool[next.Length],
            outgoingSpeedLimits = new float[next.Length]
        };
    }

    [Test]
    public void AxisForHeading_PutsOppositeDirectionsOnOneAxisAndCrossTrafficOnTheOther()
    {
        Assert.That(TrafficSignalJunction.AxisForHeading(Vector3.forward),
            Is.EqualTo(TrafficSignalJunction.AxisForHeading(Vector3.back)),
            "the two directions of one street must share a phase");
        Assert.That(TrafficSignalJunction.AxisForHeading(Vector3.forward),
            Is.Not.EqualTo(TrafficSignalJunction.AxisForHeading(Vector3.right)),
            "a crossing street must not share the phase");
        Assert.That(TrafficSignalJunction.AxisForHeading(Vector3.zero), Is.EqualTo(0),
            "a degenerate direction must not throw");
    }

    [Test]
    public void PhaseAt_NeverReleasesBothAxesTogether()
    {
        const float green = 16f, amber = 4f, allRed = 2f;
        float cycle = TrafficSignalJunction.CycleLength(green, amber, allRed);
        for (float t = 0f; t <= cycle; t += .1f)
        {
            var phase = TrafficSignalJunction.PhaseAt(t, green, amber, allRed);
            Assert.That(TrafficSignalJunction.AllowsAxis(phase, 0) && TrafficSignalJunction.AllowsAxis(phase, 1),
                Is.False, $"both axes were released at t={t:F1}s ({phase})");
        }
    }

    [Test]
    public void PhaseAt_HoldsAnAllRedGapBeforeReleasingTheCrossingAxis()
    {
        var phase = TrafficSignalJunction.PhaseAt(16f + 4f + 1f, 16f, 4f, 2f);
        Assert.That(phase, Is.EqualTo(TrafficSignalJunction.Phase.AllRedAfterA),
            "an all-red gap must follow the amber before the crossing street is released");
    }

    [Test]
    public void BuildAt_SignalsEveryApproach_AndBindsEachToItsOwnLink()
    {
        BuildCrossJunction();
        int built = CityTrafficSignals.BuildAt(graph, new[] { Centre }, importedHousing: false);

        Assert.That(built, Is.EqualTo(4), "a four-approach junction needs one head per approach");
        Assert.That(graph.GetTrafficLight(North, Centre), Is.Not.Null);
        Assert.That(graph.GetTrafficLight(South, Centre), Is.Not.Null);
        Assert.That(graph.GetTrafficLight(East, Centre), Is.Not.Null);
        Assert.That(graph.GetTrafficLight(West, Centre), Is.Not.Null);

        var controller = graph.GetComponentInChildren<TrafficSignalJunction>(true);
        Assert.That(controller, Is.Not.Null, "the junction needs one controller");
        Assert.That(controller.axisA.Count, Is.EqualTo(2), "opposing approaches share an axis");
        Assert.That(controller.axisB.Count, Is.EqualTo(2), "the crossing approaches share the other axis");
        Assert.That(controller.SignalCount, Is.EqualTo(4));

        foreach (var light in graph.GetComponentsInChildren<TrafficLight>(true))
            Assert.That(light.externallyControlled, Is.True,
                "a junction lamp must not advance a private cycle beside the controller");
    }

    [Test]
    public void JunctionPhases_StopTheCrossingApproachAndReleaseTheOther()
    {
        BuildCrossJunction();
        CityTrafficSignals.BuildAt(graph, new[] { Centre }, importedHousing: false);
        var controller = graph.GetComponentInChildren<TrafficSignalJunction>(true);

        controller.SetPhase(TrafficSignalJunction.Phase.AxisAGreen);
        Assert.That(graph.IsRedSignal(North, Centre), Is.False, "the north approach is released on axis A");
        Assert.That(graph.IsRedSignal(South, Centre), Is.False, "the south approach runs with the north");
        Assert.That(graph.IsRedSignal(East, Centre), Is.True, "the crossing approach must hold at red");

        controller.SetPhase(TrafficSignalJunction.Phase.AxisBGreen);
        Assert.That(graph.IsRedSignal(North, Centre), Is.True, "the first axis must hold while the second runs");
        Assert.That(graph.IsRedSignal(East, Centre), Is.False);
    }

    [Test]
    public void BuildAt_IsIdempotent_SoARebuildCannotStackDuplicateSignals()
    {
        BuildCrossJunction();
        CityTrafficSignals.BuildAt(graph, new[] { Centre }, importedHousing: false);
        int afterFirst = graph.GetComponentsInChildren<TrafficLight>(true).Length;

        int second = CityTrafficSignals.BuildAt(graph, new[] { Centre }, importedHousing: false);

        Assert.That(second, Is.EqualTo(0), "a signalled junction must not be signalled again");
        Assert.That(graph.GetComponentsInChildren<TrafficLight>(true).Length, Is.EqualTo(afterFirst));
    }

    [Test]
    public void FindJunctions_SkipsThroughRoads_AndStaysWithinTheRouteCorridor()
    {
        BuildCrossJunction();

        var junctions = CityTrafficSignals.FindJunctions(graph,
            new List<Vector3> { new Vector3(0f, .1f, 10f) }, 10, 120f, 0f);
        Assert.That(junctions, Does.Contain(Centre), "a crossing junction on the route must be picked");

        var chain = Track(new GameObject("ChainGraph")).AddComponent<AIRoadGraph>();
        chain.nodes = new[]
        {
            Node(chain.gameObject, "A", Vector3.zero, 1),
            Node(chain.gameObject, "B", new Vector3(0f, .1f, 20f), 2),
            Node(chain.gameObject, "C", new Vector3(0f, .1f, 40f), 1)
        };
        var throughRoads = CityTrafficSignals.FindJunctions(chain,
            new List<Vector3> { Vector3.zero }, 10, 120f, 0f);
        Assert.That(throughRoads, Is.Empty, "a node connecting two roads is not a junction");
    }

    [Test]
    public void FindJunctions_RespectsTheRouteCorridor()
    {
        BuildCrossJunction();
        var farAway = CityTrafficSignals.FindJunctions(graph,
            new List<Vector3> { new Vector3(5000f, 0f, 5000f) }, 10, 120f, 0f);
        Assert.That(farAway, Is.Empty, "a junction far from the driven route is not worth a signal");
    }
}
