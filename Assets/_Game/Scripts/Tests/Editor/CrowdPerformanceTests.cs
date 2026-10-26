using NUnit.Framework;
using UnityEngine;

public class CrowdPerformanceTests
{
    [Test]
    public void CrowdBudgetsStayBoundedAndOrdered()
    {
        Assert.That(CrowdPerformancePolicy.MaxCrossingPedestrians, Is.InRange(1, 48),
            "the crossing crowd must stay inside the budget the streamer was tuned for");
        Assert.That(CrowdPerformancePolicy.MaxAmbientWalkers, Is.LessThan(CrowdPerformancePolicy.MaxCrossingPedestrians),
            "ambient scenery walkers should be cheaper than the interactive crossing crowd");
        Assert.That(CrowdPerformancePolicy.MaxPassengerProxies, Is.InRange(1, 96));
    }

    [Test]
    public void AnimationBandsThinWithDistance()
    {
        Assert.That(CrowdPerformancePolicy.FullAnimationDistanceMeters,
            Is.LessThan(CrowdPerformancePolicy.ReducedAnimationDistanceMeters));
        Assert.That(CrowdPerformancePolicy.ReducedAnimationDistanceMeters,
            Is.LessThanOrEqualTo(CrowdPerformancePolicy.AnimationCutoffMeters));

        Assert.That(CrowdPerformancePolicy.AnimationInterval(0f), Is.EqualTo(CrowdPerformancePolicy.NearIntervalSeconds));
        Assert.That(CrowdPerformancePolicy.AnimationInterval(CrowdPerformancePolicy.FullAnimationDistanceMeters),
            Is.EqualTo(CrowdPerformancePolicy.ReducedIntervalSeconds));
        Assert.That(CrowdPerformancePolicy.AnimationInterval(CrowdPerformancePolicy.ReducedAnimationDistanceMeters),
            Is.EqualTo(CrowdPerformancePolicy.DistantIntervalSeconds));
        Assert.That(CrowdPerformancePolicy.AnimationInterval(CrowdPerformancePolicy.ReducedAnimationDistanceMeters),
            Is.GreaterThan(CrowdPerformancePolicy.AnimationInterval(CrowdPerformancePolicy.FullAnimationDistanceMeters)),
            "a distant walker must update less often than a street-range one");

        Assert.That(CrowdPerformancePolicy.UsesFullAnimation(CrowdPerformancePolicy.FullAnimationDistanceMeters - .01f), Is.True);
        Assert.That(CrowdPerformancePolicy.UsesFullAnimation(CrowdPerformancePolicy.FullAnimationDistanceMeters), Is.False);
        Assert.That(CrowdPerformancePolicy.AnimatesAt(CrowdPerformancePolicy.AnimationCutoffMeters), Is.False,
            "past the cutoff the pose is frozen, not merely thinned");
    }

    [Test]
    public void QualityTierCapsTheCrowdBudget()
    {
        int max = CrowdPerformancePolicy.MaxCrossingPedestrians;
        int high = CrowdPerformancePolicy.BudgetForQuality(3, false, max);
        int medium = CrowdPerformancePolicy.BudgetForQuality(1, false, max);
        int low = CrowdPerformancePolicy.BudgetForQuality(0, false, max);
        int phone = CrowdPerformancePolicy.BudgetForQuality(3, true, max);

        Assert.That(high, Is.EqualTo(max), "the top tiers keep the full policy crowd");
        Assert.That(medium, Is.LessThan(high), "Medium must cost less than High");
        Assert.That(low, Is.LessThan(medium), "Low must cost less than Medium");
        Assert.That(phone, Is.LessThanOrEqualTo(high), "a phone never gets more walkers than a desktop");
        Assert.That(low, Is.GreaterThanOrEqualTo(1), "a low tier still has a street");
        Assert.That(CrowdPerformancePolicy.BudgetForQuality(3, false, 0), Is.EqualTo(1),
            "a degenerate ceiling still resolves to one walker, not zero");
    }

    [Test]
    public void WalkerBudgetClampsAuthoredValues()
    {
        Assert.That(CrowdPerformancePolicy.ClampWalkerBudget(8, 48), Is.EqualTo(8));
        Assert.That(CrowdPerformancePolicy.ClampWalkerBudget(999, 48), Is.EqualTo(48));
        Assert.That(CrowdPerformancePolicy.ClampWalkerBudget(0, 48), Is.EqualTo(48),
            "an unset budget falls back to the policy default");
        Assert.That(CrowdPerformancePolicy.ClampWalkerBudget(-5, 48), Is.EqualTo(48));
    }

    [Test]
    public void PassengerResetForReuseClearsEveryStateFlag()
    {
        var passenger = new PassengerAgent(1, 2, 30f, true, PassengerArchetype.Elderly);
        passenger.satisfaction = .2f;
        passenger.isSeated = true;
        passenger.isBoarding = true;
        passenger.isAlighting = true;
        passenger.assignedSlotIndex = 5;
        passenger.stopRequested = true;
        passenger.preparingToAlight = true;
        passenger.hasComplainedAboutBraking = true;
        passenger.platformPosition = Vector3.one;
        passenger.busLocalPosition = Vector3.one;
        passenger.boardingLerp = .5f;
        passenger.standingSway = .2f;

        passenger.ResetForReuse(3, 4, 90f, false, PassengerArchetype.Student);

        Assert.That(passenger.originStopIndex, Is.EqualTo(3));
        Assert.That(passenger.destinationStopIndex, Is.EqualTo(4));
        Assert.That(passenger.patienceSeconds, Is.EqualTo(90f));
        Assert.That(passenger.archetype, Is.EqualTo(PassengerArchetype.Student));
        Assert.That(passenger.satisfaction, Is.EqualTo(1f));
        Assert.That(passenger.isWheelchairPassenger, Is.False);
        Assert.That(passenger.isSeated, Is.False);
        Assert.That(passenger.isBoarding, Is.False);
        Assert.That(passenger.isAlighting, Is.False);
        Assert.That(passenger.assignedSlotIndex, Is.EqualTo(-1));
        Assert.That(passenger.stopRequested, Is.False);
        Assert.That(passenger.preparingToAlight, Is.False);
        Assert.That(passenger.hasComplainedAboutBraking, Is.False);
        Assert.That(passenger.platformPosition, Is.EqualTo(Vector3.zero));
        Assert.That(passenger.busLocalPosition, Is.EqualTo(Vector3.zero));
        Assert.That(passenger.boardingLerp, Is.EqualTo(0f));
        Assert.That(passenger.standingSway, Is.EqualTo(0f));
    }

    [Test]
    public void CruiseLaneStaysOnTheKerbsideOfBothDrivingSides()
    {
        foreach (bool drivesOnLeft in new[] { true, false })
        {
            var graph = BuildTwoWayGraph(drivesOnLeft, 2);
            try
            {
                Assert.That(AIVehicleController.CruiseLaneIndex(graph, 0, 1), Is.EqualTo(1),
                    $"drivesOnLeft={drivesOnLeft}: forward traffic should spawn in the kerbside lane");
                Assert.That(AIVehicleController.CruiseLaneIndex(graph, 1, 0), Is.EqualTo(1),
                    $"drivesOnLeft={drivesOnLeft}: the opposite direction also keeps to its own kerb");
            }
            finally
            {
                Object.DestroyImmediate(graph.gameObject);
            }
        }
    }

    [Test]
    public void TwoWayLanesPlaceEachDirectionOnItsOwnSide()
    {
        foreach (bool drivesOnLeft in new[] { true, false })
        {
            var graph = BuildTwoWayGraph(drivesOnLeft, 2);
            try
            {
                // The offset is a scalar along each link's own right vector, so the
                // sign alone is not the side. Compare the resulting world points:
                // the reverse link's right vector is flipped with its direction.
                Vector3 forward = LanePoint(graph, 0, 1, 1);
                Vector3 backward = LanePoint(graph, 1, 0, 1);
                Assert.That(forward.x * backward.x, Is.LessThan(0f),
                    $"drivesOnLeft={drivesOnLeft}: the two directions must not share a carriageway");
                Assert.That(Mathf.Abs(forward.x), Is.EqualTo(1.5f * 3.3f).Within(.01f),
                    "the kerbside lane is one full lane outboard of the centre-line lane");
            }
            finally
            {
                Object.DestroyImmediate(graph.gameObject);
            }
        }
    }

    [Test]
    public void OneWayCruiseLaneFollowsTheDrivingSide()
    {
        var left = BuildOneWayGraph(true, 3);
        var right = BuildOneWayGraph(false, 3);
        try
        {
            Assert.That(AIVehicleController.CruiseLaneIndex(left, 0, 1), Is.EqualTo(0));
            Assert.That(AIVehicleController.CruiseLaneIndex(right, 0, 1), Is.EqualTo(2));
        }
        finally
        {
            Object.DestroyImmediate(left.gameObject);
            Object.DestroyImmediate(right.gameObject);
        }
    }

    static Vector3 LanePoint(AIRoadGraph graph, int from, int to, int lane)
    {
        Vector3 a = graph.GetNodePosition(from);
        Vector3 segment = graph.GetNodePosition(to) - a;
        segment.y = 0f;
        if (segment.sqrMagnitude < .0001f) return a;
        segment.Normalize();
        return a + Vector3.Cross(Vector3.up, segment) * graph.GetLaneOffset(from, to, lane);
    }

    static AIRoadGraph BuildTwoWayGraph(bool drivesOnLeft, int lanes)
    {
        var graph = NewGraph(drivesOnLeft, 2);
        for (int i = 0; i < 2; i++)
        {
            graph.nodes[i].laneCount = lanes;
            graph.nodes[i].nextNodeIndices = new[] { 1 - i };
            graph.nodes[i].outgoingLaneCounts = new[] { lanes };
            graph.nodes[i].outgoingLaneWidths = new[] { 3.3f };
            graph.nodes[i].outgoingOneWays = new[] { false };
        }
        return graph;
    }

    static AIRoadGraph BuildOneWayGraph(bool drivesOnLeft, int lanes)
    {
        var graph = NewGraph(drivesOnLeft, 2);
        graph.nodes[0].laneCount = lanes;
        graph.nodes[0].nextNodeIndices = new[] { 1 };
        graph.nodes[0].outgoingLaneCounts = new[] { lanes };
        graph.nodes[0].outgoingLaneWidths = new[] { 3.3f };
        graph.nodes[0].outgoingOneWays = new[] { true };
        graph.nodes[1].laneCount = lanes;
        return graph;
    }

    static AIRoadGraph NewGraph(bool drivesOnLeft, int nodeCount)
    {
        var root = new GameObject("Crowd lane graph");
        var graph = root.AddComponent<AIRoadGraph>();
        graph.drivesOnLeft = drivesOnLeft;
        graph.nodes = new AIRoadGraph.RoadNode[nodeCount];
        for (int i = 0; i < nodeCount; i++)
        {
            var point = new GameObject($"Node_{i}").transform;
            point.SetParent(root.transform, false);
            point.localPosition = new Vector3(0f, 0f, i * 10f);
            graph.nodes[i] = new AIRoadGraph.RoadNode { point = point };
        }
        return graph;
    }
}
