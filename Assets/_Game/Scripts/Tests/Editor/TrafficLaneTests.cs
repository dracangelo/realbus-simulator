using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Locks the lane and traffic-side rules that the rewired traffic stack shares.
/// Every one of these used to be re-derived locally, so a single mirrored sign put
/// traffic, stops or queues on the wrong carriageway while each file looked correct.
/// </summary>
public class TrafficLaneTests
{
    GameObject root;
    AIRoadGraph graph;
    GameObject nodeA;
    GameObject nodeB;
    GameObject nodeC;
    GameObject nodeD;
    GameObject nodeE;

    /// <summary>A two-way link A(0,0,0) -> B(0,0,40) with the given lanes each way.</summary>
    void BuildTwoWay(int lanes, float laneWidth = 3.3f)
    {
        root = new GameObject("Traffic lane graph");
        graph = root.AddComponent<AIRoadGraph>();
        nodeA = new GameObject("A");
        nodeB = new GameObject("B");
        nodeA.transform.position = Vector3.zero;
        nodeB.transform.position = Vector3.forward * 40f;
        graph.nodes = new[]
        {
            new AIRoadGraph.RoadNode
            {
                point = nodeA.transform, nextNodeIndices = new[] { 1 },
                outgoingLaneCounts = new[] { lanes }, outgoingLaneWidths = new[] { laneWidth },
                outgoingOneWays = new[] { false }, outgoingSharedLanes = new[] { false }
            },
            new AIRoadGraph.RoadNode
            {
                point = nodeB.transform, nextNodeIndices = new[] { 0 },
                outgoingLaneCounts = new[] { lanes }, outgoingLaneWidths = new[] { laneWidth },
                outgoingOneWays = new[] { false }, outgoingSharedLanes = new[] { false }
            }
        };
    }

    /// <summary>
    /// Creates a 4-way intersection centered at nodeB (0,0,40):
    /// nodeA (0,0,0) -> nodeB (0,0,40) [incoming from south]
    /// nodeB -> nodeC (-40,0,40) [west / left turn]
    /// nodeB -> nodeD (40,0,40)  [east / right turn]
    /// nodeB -> nodeE (0,0,80)   [north / straight ahead]
    /// </summary>
    void BuildIntersection(int incomingLanes, int outgoingLanes, float laneWidth = 3.3f)
    {
        root = new GameObject("Intersection graph");
        graph = root.AddComponent<AIRoadGraph>();
        nodeA = new GameObject("A_South");
        nodeB = new GameObject("B_Junction");
        nodeC = new GameObject("C_West");
        nodeD = new GameObject("D_East");
        nodeE = new GameObject("E_North");

        nodeA.transform.position = Vector3.zero;
        nodeB.transform.position = new Vector3(0f, 0f, 40f);
        nodeC.transform.position = new Vector3(-40f, 0f, 40f);
        nodeD.transform.position = new Vector3(40f, 0f, 40f);
        nodeE.transform.position = new Vector3(0f, 0f, 80f);

        graph.nodes = new[]
        {
            new AIRoadGraph.RoadNode
            {
                point = nodeA.transform, nextNodeIndices = new[] { 1 },
                outgoingLaneCounts = new[] { incomingLanes }, outgoingLaneWidths = new[] { laneWidth },
                outgoingOneWays = new[] { false }, outgoingSharedLanes = new[] { false }
            },
            new AIRoadGraph.RoadNode
            {
                point = nodeB.transform, nextNodeIndices = new[] { 2, 3, 4 },
                outgoingLaneCounts = new[] { outgoingLanes, outgoingLanes, outgoingLanes },
                outgoingLaneWidths = new[] { laneWidth, laneWidth, laneWidth },
                outgoingOneWays = new[] { false, false, false },
                outgoingSharedLanes = new[] { false, false, false }
            },
            new AIRoadGraph.RoadNode { point = nodeC.transform, nextNodeIndices = new int[0] },
            new AIRoadGraph.RoadNode { point = nodeD.transform, nextNodeIndices = new int[0] },
            new AIRoadGraph.RoadNode { point = nodeE.transform, nextNodeIndices = new int[0] }
        };
    }

    [TearDown]
    public void TearDown()
    {
        if (root != null) Object.DestroyImmediate(root);
        if (nodeA != null) Object.DestroyImmediate(nodeA);
        if (nodeB != null) Object.DestroyImmediate(nodeB);
        if (nodeC != null) Object.DestroyImmediate(nodeC);
        if (nodeD != null) Object.DestroyImmediate(nodeD);
        if (nodeE != null) Object.DestroyImmediate(nodeE);
    }

    [Test]
    public void CruiseLaneIsTheKerbLaneOnBothDrivingSides()
    {
        BuildTwoWay(3);

        graph.drivesOnLeft = true;
        Assert.That(TrafficLane.CruiseLane(graph, 0, 1), Is.EqualTo(2),
            "left-hand traffic keeps to the lane nearest the left kerb, the highest index");
        GraphSideAssertions(0, 1, 2, leftHandTraffic: true);

        graph.drivesOnLeft = false;
        Assert.That(TrafficLane.CruiseLane(graph, 0, 1), Is.EqualTo(2),
            "a two-way direction is indexed from the centre line outwards on both sides");
        GraphSideAssertions(0, 1, 2, leftHandTraffic: false);
    }

    /// <summary>The cruise lane line must be the one nearest the kerb, not just an index.</summary>
    void GraphSideAssertions(int from, int to, int cruiseLane, bool leftHandTraffic)
    {
        LaneFrame frame = TrafficLane.Frame(graph, from, to, cruiseLane);
        Assert.That(frame.IsValid, Is.True);
        Vector3 kerb = TrafficLane.KerbDirection(frame.Direction, leftHandTraffic);
        Assert.That(Vector3.Dot(frame.Right * frame.Offset, kerb), Is.GreaterThan(0f),
            "the cruise lane line must lie on the kerb side of the centre line");
    }

    [Test]
    public void PassMovesTowardTheCentreLineAndNeverTowardTheKerb()
    {
        BuildTwoWay(3);
        graph.drivesOnLeft = true;

        Assert.That(TrafficLane.PassingLane(graph, 0, 1, 2), Is.EqualTo(1));
        Assert.That(TrafficLane.PassingLane(graph, 0, 1, 1), Is.EqualTo(0));
        Assert.That(TrafficLane.PassingLane(graph, 0, 1, 0), Is.EqualTo(-1),
            "the centre-line lane has nowhere legal to pass into on a two-way link");

        graph.drivesOnLeft = false;
        Assert.That(TrafficLane.PassingLane(graph, 0, 1, 2), Is.EqualTo(1),
            "the pass follows the same centre-ward rule in right-hand traffic");
    }

    [Test]
    public void KerbYieldMovesTowardTheKerbAndStopsThere()
    {
        BuildTwoWay(3);

        Assert.That(TrafficLane.KerbYieldLane(graph, 0, 1, 0), Is.EqualTo(1));
        Assert.That(TrafficLane.KerbYieldLane(graph, 0, 1, 2), Is.EqualTo(-1),
            "already in the kerb lane: there is no further lane to yield into");
    }

    [Test]
    public void OneWayLanesKeepOrderAcrossTheWholeCarriageway()
    {
        BuildTwoWay(2);
        graph.nodes[0].outgoingOneWays = new[] { true };

        graph.drivesOnLeft = true;
        // Offsets are centred on the way line: lane 0 is the left/kerb lane.
        Assert.That(TrafficLane.Offset(graph, 0, 1, 0), Is.LessThan(0f));
        Assert.That(TrafficLane.Offset(graph, 0, 1, 1), Is.GreaterThan(0f));
        Assert.That(TrafficLane.CruiseLane(graph, 0, 1), Is.EqualTo(0));
        Assert.That(TrafficLane.KerbwardLane(graph, 0, 1, 1), Is.EqualTo(0));

        graph.drivesOnLeft = false;
        Assert.That(TrafficLane.CruiseLane(graph, 0, 1), Is.EqualTo(1),
            "right-hand traffic on a one-way road keeps to the right-hand lane");
        Assert.That(TrafficLane.KerbwardLane(graph, 0, 1, 0), Is.EqualTo(1));
    }

    [Test]
    public void AOneWayCarriagewayStillAllowsAPassInEitherDirection()
    {
        BuildTwoWay(2);
        graph.nodes[0].outgoingOneWays = new[] { true };

        // Both lanes are travelling the same way, so the far lane is a legal pass
        // even when it is on the kerb side of the way line.
        Assert.That(TrafficLane.PassingLane(graph, 0, 1, 0), Is.EqualTo(1));
        Assert.That(TrafficLane.PassingLane(graph, 0, 1, 1), Is.EqualTo(0));
    }

    [Test]
    public void SharedSingleLaneKeepsBothDirectionsOnTheMiddle()
    {
        BuildTwoWay(1);
        graph.nodes[0].outgoingSharedLanes = new[] { true };

        Assert.That(TrafficLane.Offset(graph, 0, 1, 0), Is.EqualTo(0f).Within(.001f));
        Assert.That(TrafficLane.CruiseLane(graph, 0, 1), Is.EqualTo(0));
        Assert.That(TrafficLane.PassingLane(graph, 0, 1, 0), Is.EqualTo(-1));
        Assert.That(TrafficLane.KerbYieldLane(graph, 0, 1, 0), Is.EqualTo(-1));
    }

    [Test]
    public void OppositeDirectionsOccupyOppositeHalvesOfTheSameRoad()
    {
        BuildTwoWay(2);

        LaneFrame forward = TrafficLane.Frame(graph, 0, 1, TrafficLane.CruiseLane(graph, 0, 1));
        LaneFrame reverse = TrafficLane.Frame(graph, 1, 0, TrafficLane.CruiseLane(graph, 1, 0));

        Assert.That(forward.IsValid && reverse.IsValid, Is.True);
        Vector3 forwardPoint = forward.PointAt(.5f);
        Vector3 reversePoint = reverse.PointAt(.5f);

        // Both lanes are half a carriageway away from the centre line, on opposite
        // sides of the world centreline: the two directions never share a line.
        Assert.That(Vector3.Dot(forwardPoint, Vector3.right), Is.LessThan(-.5f),
            "left-hand traffic drives on the negative side of a forward link");
        Assert.That(Vector3.Dot(reversePoint, Vector3.right), Is.GreaterThan(.5f));
        Assert.That(Vector3.Distance(forwardPoint, reversePoint), Is.GreaterThan(2f));

        graph.drivesOnLeft = false;
        LaneFrame rightHandForward = TrafficLane.Frame(graph, 0, 1, TrafficLane.CruiseLane(graph, 0, 1));
        Assert.That(Vector3.Dot(rightHandForward.PointAt(.5f), Vector3.right), Is.GreaterThan(.5f),
            "switching the driving side mirrors every lane onto the other half");
    }

    [Test]
    public void LaneFrameRoundTripsAlongAndLateralPosition()
    {
        BuildTwoWay(2);
        LaneFrame frame = TrafficLane.Frame(graph, 0, 1, 1);

        Assert.That(frame.Length, Is.EqualTo(40f).Within(.01f));
        Assert.That(frame.PointAt(.25f).z, Is.EqualTo(10f).Within(.01f));
        Assert.That(frame.PointAt(2f).z, Is.EqualTo(40f).Within(.01f),
            "a point past the end clamps to the link, never past the junction");

        Vector3 sample = frame.PointAt(.6f) + frame.Right * 1.4f;
        Assert.That(frame.AlongOf(sample), Is.EqualTo(.6f).Within(.01f));
        Assert.That(frame.LateralOf(sample), Is.EqualTo(1.4f).Within(.01f),
            "another consumer offsetting from the frame lands where it expects");
    }

    [Test]
    public void InvalidLinksNeverProduceAPlausibleLookingFrame()
    {
        BuildTwoWay(2);

        Assert.That(TrafficLane.Frame(graph, 0, 99, 0).IsValid, Is.False);
        Assert.That(TrafficLane.Frame(null, 0, 1, 0).IsValid, Is.False);
        Assert.That(TrafficLane.Offset(null, 0, 1, 0), Is.EqualTo(0f));
        Assert.That(TrafficLane.CruiseLane(null, 0, 1), Is.EqualTo(0));
        Assert.That(TrafficLane.PassingLane(graph, 0, 99, 0), Is.EqualTo(-1));
    }

    [Test]
    public void KerbSideAuthorityAgreesWithTheLaneGeometry()
    {
        BuildTwoWay(2);

        // The kerb vector of the travel direction and the lane offset must point the
        // same way. A stop, a queue, a signal head and a spawned car all read these
        // two values, so a disagreement is the wrong-side bug itself.
        foreach (bool drivesOnLeft in new[] { true, false })
        {
            graph.drivesOnLeft = drivesOnLeft;
            LaneFrame frame = TrafficLane.Frame(graph, 0, 1, TrafficLane.CruiseLane(graph, 0, 1));
            Vector3 kerb = RoadSpawnResolver.KerbSide(frame.Direction, drivesOnLeft);

            Assert.That(Vector3.Dot(kerb, frame.Right * frame.Offset), Is.GreaterThan(0f),
                $"kerb side and lane offset must agree (drivesOnLeft={drivesOnLeft})");
            Assert.That(Vector3.Dot(kerb, frame.Right), Is.EqualTo(
                TrafficLane.SideSign(drivesOnLeft)).Within(.01f));
        }
    }

    [Test]
    public void NearestLaneResolvesABodyThatHasDriftedBackToItsOwnLine()
    {
        BuildTwoWay(3);
        graph.drivesOnLeft = true;

        float laneOne = TrafficLane.Offset(graph, 0, 1, 1);
        Assert.That(TrafficLane.NearestLane(graph, 0, 1, laneOne + .2f), Is.EqualTo(1));
        Assert.That(TrafficLane.NearestLane(graph, 0, 1, laneOne - .2f), Is.EqualTo(1),
            "the committed lane is recovered from either side of the line");
        // Lane lines sit on the negative side of a left-hand-traffic link, so a body
        // far out on the kerb resolves to the highest index and a body at the centre
        // line resolves to the lowest.
        Assert.That(TrafficLane.NearestLane(graph, 0, 1, -999f), Is.EqualTo(2));
        Assert.That(TrafficLane.NearestLane(graph, 0, 1, 999f), Is.EqualTo(0));
    }

    [Test]
    public void KerbOffsetStaysInsideTheRenderedAsphalt()
    {
        BuildTwoWay(2, 3.5f);

        float half = graph.GetRoadHalfWidth(0, 1);
        float kerb = TrafficLane.KerbOffsetFromCentreline(graph, 0, 1, 2.6f);

        Assert.That(kerb, Is.GreaterThan(0f));
        Assert.That(kerb + 2.6f * .5f, Is.LessThanOrEqualTo(half + .001f),
            "a stopped bus must still fit inside the carriageway it docks against");
    }

    [Test]
    public void FastestLaneIsInnermostOnTwoWayAndOffsideOnOneWay()
    {
        BuildTwoWay(3);

        // On two-way roads, lane 0 is next to the centre line (fastest / innermost) on both sides.
        graph.drivesOnLeft = true;
        Assert.That(TrafficLane.FastestLane(graph, 0, 1), Is.EqualTo(0));
        graph.drivesOnLeft = false;
        Assert.That(TrafficLane.FastestLane(graph, 0, 1), Is.EqualTo(0));

        // On one-way roads:
        graph.nodes[0].outgoingOneWays = new[] { true };
        graph.drivesOnLeft = true;
        // In left-hand drive, offside overtaking lane is the rightmost lane (highest index).
        Assert.That(TrafficLane.FastestLane(graph, 0, 1), Is.EqualTo(2));
        graph.drivesOnLeft = false;
        // In right-hand drive, offside overtaking lane is the leftmost lane (lane 0).
        Assert.That(TrafficLane.FastestLane(graph, 0, 1), Is.EqualTo(0));
    }

    [Test]
    public void PickLaneForVehicleAssignsLanesByVehicleTypeAndSpeed()
    {
        BuildTwoWay(3);
        graph.drivesOnLeft = true;

        // Buses and trucks must stick to kerb lane (lane 2 in 3-lane two-way)
        int busLane = TrafficLane.PickLaneForVehicle(graph, 0, 1, AIVehicleController.VehicleType.Bus, 1.0f);
        int truckLane = TrafficLane.PickLaneForVehicle(graph, 0, 1, AIVehicleController.VehicleType.Truck, 1.0f);
        Assert.That(busLane, Is.EqualTo(2));
        Assert.That(truckLane, Is.EqualTo(2));

        // Emergency vehicle and high-speed drivers prefer fastest lane (lane 0)
        int emergLane = TrafficLane.PickLaneForVehicle(graph, 0, 1, AIVehicleController.VehicleType.Emergency, 1.0f);
        int fastCarLane = TrafficLane.PickLaneForVehicle(graph, 0, 1, AIVehicleController.VehicleType.Car, 1.25f);
        Assert.That(emergLane, Is.EqualTo(0));
        Assert.That(fastCarLane, Is.EqualTo(0));

        // Normal/slow car stays kerbward
        int slowCarLane = TrafficLane.PickLaneForVehicle(graph, 0, 1, AIVehicleController.VehicleType.Car, 0.88f);
        Assert.That(slowCarLane, Is.EqualTo(2));
    }

    [Test]
    public void ASteeredBodyIsPulledBackInsideTheAsphalt()
    {
        BuildTwoWay(1, 3.5f);
        float half = graph.GetRoadHalfWidth(0, 1);
        const float bodyHalf = 1.1f;

        // A body shoved past the kerb by a collision comes back onto the road.
        Vector3 centre = new Vector3(0f, 0f, 20f);
        Vector3 offRoad = centre + Vector3.right * (half + 2.5f);
        Vector3 clamped = TrafficLane.ClampToRoad(graph, 0, 1, -1, offRoad, bodyHalf);
        Assert.That(Vector3.Dot(clamped - centre, Vector3.right),
            Is.LessThanOrEqualTo(half - bodyHalf + .001f),
            "a body must sit fully inside the carriageway, never on the kerb");
        Assert.That(Vector3.Dot(clamped - centre, Vector3.right), Is.GreaterThan(0f),
            "the rail pulls the body back in, it does not mirror it across the road");

        // A body already in its lane is not moved at all.
        Vector3 inLane = centre + Vector3.right * 1.2f;
        Assert.That(Vector3.Distance(TrafficLane.ClampToRoad(graph, 0, 1, -1, inLane, bodyHalf), inLane),
            Is.LessThan(.001f), "lane keeping already satisfies the rail");
    }

    [Test]
    public void ACornerIsJudgedAgainstTheRoadItTurnsInto()
    {
        BuildIntersection(incomingLanes: 1, outgoingLanes: 1);

        // Turning east at the junction: the body is wide of the incoming centreline
        // but already on the asphalt of the link it is joining. Judging it only
        // against the line it left behind would drag it back across the junction.
        Vector3 corner = new Vector3(6f, 0f, 41.5f);
        Assert.That(Vector3.Distance(TrafficLane.ClampToRoad(graph, 0, 1, 3, corner, 1.1f), corner),
            Is.LessThan(.001f), "a body inside the junction mouth of the exit link is on the road");

        // Once the body is genuinely off both roads, it is pulled back.
        Vector3 pavement = new Vector3(6f, 0f, 55f);
        Vector3 clamped = TrafficLane.ClampToRoad(graph, 0, 1, 3, pavement, 1.1f);
        Assert.That(Vector3.Distance(clamped, pavement), Is.GreaterThan(.5f),
            "a body beyond the asphalt of both links must be corrected");
        Assert.That(Vector3.Distance(clamped, new Vector3(6f, 0f, 40f)),
            Is.LessThan(Vector3.Distance(pavement, new Vector3(6f, 0f, 40f))),
            "the correction brings the body back towards the road, not further out");
    }

    [Test]
    public void TurnAngleReportsTheExitTheVehicleActuallyTakes()
    {
        BuildIntersection(incomingLanes: 3, outgoingLanes: 3);

        Assert.That(graph.GetTurnAngle(0, 1, 2), Is.EqualTo(90f).Within(1f),
            "the west exit is a right-angle turn, so the car must slow for it");
        Assert.That(graph.GetTurnAngle(0, 1, 4), Is.EqualTo(0f).Within(1f),
            "the straight exit is not a turn");
        Assert.That(graph.GetTurnAngle(0, 1, 99), Is.EqualTo(0f),
            "an unusable exit reports no turn instead of guessing");
    }

    [Test]
    public void APassStartsOnlyWhenItCanFinishBeforeTheJunction()
    {
        Assert.That(AIVehicleController.ShouldStartOvertake(50f, 20f, 18f, 9f, true, 200f), Is.True);
        Assert.That(AIVehicleController.ShouldStartOvertake(50f, 20f, 18f, 9f, true, 12f), Is.False,
            "a lane change it cannot complete before the junction is not worth starting");
    }

    [Test]
    public void ResolveTurnLanePreselectsKerbForNearsideAndOffsideForOffside()
    {
        BuildIntersection(incomingLanes: 3, outgoingLanes: 3);

        // nodeA (0,0,0) -> nodeB (0,0,40): heading North (0, 0, 1)
        // nodeC (-40,0,40): West (Left turn)
        // nodeD (40,0,40): East (Right turn)

        // Left-hand drive:
        // Left turn is nearside -> kerb lane (lane 2)
        // Right turn is offside -> inner lane (lane 0)
        graph.drivesOnLeft = true;
        int lhdLeftTurn = TrafficLane.ResolveTurnLane(graph, 0, 1, 2);
        int lhdRightTurn = TrafficLane.ResolveTurnLane(graph, 0, 1, 3);
        Assert.That(lhdLeftTurn, Is.EqualTo(2), "LHD left turn is near-side: use kerb lane");
        Assert.That(lhdRightTurn, Is.EqualTo(0), "LHD right turn is off-side: use inner lane");

        // Right-hand drive:
        // Right turn is nearside -> kerb lane (lane 2)
        // Left turn is offside -> inner lane (lane 0)
        graph.drivesOnLeft = false;
        int rhdLeftTurn = TrafficLane.ResolveTurnLane(graph, 0, 1, 2);
        int rhdRightTurn = TrafficLane.ResolveTurnLane(graph, 0, 1, 3);
        Assert.That(rhdRightTurn, Is.EqualTo(2), "RHD right turn is near-side: use kerb lane");
        Assert.That(rhdLeftTurn, Is.EqualTo(0), "RHD left turn is off-side: use inner lane");
    }

    [Test]
    public void ResolveExitLaneMapsVehiclesAccuratelyAcrossIntersectionsAndTransitions()
    {
        // 1. Intersection turns:
        BuildIntersection(incomingLanes: 3, outgoingLanes: 3);
        graph.drivesOnLeft = true;

        // Nearside left turn in LHD exits into kerb lane (lane 2)
        int exitNearside = TrafficLane.ResolveExitLane(graph, 0, 1, 2, incomingLane: 2);
        Assert.That(exitNearside, Is.EqualTo(2));

        // Offside right turn in LHD exits into fastest lane (lane 0)
        int exitOffside = TrafficLane.ResolveExitLane(graph, 0, 1, 3, incomingLane: 0);
        Assert.That(exitOffside, Is.EqualTo(0));

        // 2. Straight transition where lane count expands (2 lanes -> 4 lanes):
        // Node 0 -> Node 1 (2 lanes), Node 1 -> Node 4 (4 lanes)
        graph.nodes[0].outgoingLaneCounts = new[] { 2 };
        graph.nodes[1].outgoingLaneCounts = new[] { 3, 3, 4 };

        // Straight ahead (North -> nodeE index 4):
        // Incoming lane 0 (innermost of 2) maps to lane 0 (innermost of 4)
        int exitStraightInner = TrafficLane.ResolveExitLane(graph, 0, 1, 4, incomingLane: 0);
        Assert.That(exitStraightInner, Is.EqualTo(0));

        // Incoming lane 1 (kerb lane of 2) maps to lane 3 (kerb lane of 4)
        int exitStraightKerb = TrafficLane.ResolveExitLane(graph, 0, 1, 4, incomingLane: 1);
        Assert.That(exitStraightKerb, Is.EqualTo(3));
    }

    [Test]
    public void ABodyOffTheDrawnRoadIsPulledBackOntoTheAsphalt()
    {
        var roads = new RoadSurfaceIndex();
        roads.Add(new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 80f), 7f, 42L);
        const float bodyHalf = 1.1f;

        // Already on the carriageway: the index never moves it.
        Vector3 onRoad = new Vector3(2f, 0f, 30f);
        Assert.That(Vector3.Distance(TrafficLane.ClampToSurface(roads, onRoad, bodyHalf), onRoad),
            Is.LessThan(.001f), "a body on the drawn asphalt must not be nudged");

        // A graph centreline running down a median leaves the body legal on the graph
        // and visibly off the road. The drawn asphalt is the authority.
        Vector3 verge = new Vector3(14f, 0f, 30f);
        Vector3 pulled = TrafficLane.ClampToSurface(roads, verge, bodyHalf);
        Assert.That(Vector3.Distance(pulled, verge), Is.GreaterThan(1f), "a body on the verge is corrected");
        Assert.That(Vector3.Distance(pulled, new Vector3(0f, 0f, 30f)), Is.LessThanOrEqualTo(2.21f),
            "the correction leaves the body fully inside the 7 m asphalt");
    }

    [Test]
    public void TheDrawnRoadRailIsInertWithoutARoadIndex()
    {
        Vector3 position = new Vector3(50f, 0f, 12f);
        Assert.That(Vector3.Distance(TrafficLane.ClampToSurface(null, position, 1.1f), position), Is.LessThan(.001f));

        var empty = new RoadSurfaceIndex();
        Assert.That(empty.IsEmpty, Is.True);
        Assert.That(Vector3.Distance(TrafficLane.ClampToSurface(empty, position, 1.1f), position), Is.LessThan(.001f),
            "a scene with no road mesh keeps the graph rail as its only authority");
    }
}
