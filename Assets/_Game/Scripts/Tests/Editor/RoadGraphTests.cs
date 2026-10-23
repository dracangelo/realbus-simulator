using NUnit.Framework;
using UnityEngine;

public class RoadGraphTests
{
    [Test]
    public void OverpassQueryBuilder_RoadsQuery_IncludesBboxAndHighwayFilter()
    {
        var q = OverpassQueryBuilder.RoadsQuery(-1, 36, -0.9, 36.1);
        Assert.That(q, Does.Contain("way[highway"));
        Assert.That(q, Does.Contain("(-1.0000000,36.0000000,-0.9000000,36.1000000)"));
        Assert.That(q, Does.Contain("out geom"));
    }

    [Test]
    public void RoadGraph_BuildFromOverpassWays_CreatesConnectivityForSimpleWay()
    {
        const string json =
            "{ \"elements\": [" +
            "{ \"type\":\"way\",\"id\":1,\"tags\":{\"highway\":\"residential\",\"maxspeed\":\"30\"}," +
            "\"geometry\":[{\"lat\":-1.0,\"lon\":36.0},{\"lat\":-1.0,\"lon\":36.001},{\"lat\":-1.0,\"lon\":36.002}] }" +
            "] }";

        var resp = OverpassResponse.Deserialize(json);
        var go = new GameObject("Converter");
        var conv = go.AddComponent<CoordinateConverter>();
        conv.mapOrigin = ScriptableObject.CreateInstance<MapOrigin>();
        conv.mapOrigin.originLat = -1.0;
        conv.mapOrigin.originLon = 36.0;

        var graph = RoadGraph.BuildFromOverpassWays(resp, conv);
        Assert.That(graph.nodes.Count, Is.EqualTo(3));
        Assert.That(graph.nodes[0].edges.Count, Is.GreaterThan(0));
        Assert.That(graph.nodes[1].edges.Count, Is.GreaterThan(0));
        Assert.That(graph.nodes[2].edges.Count, Is.GreaterThan(0));
    }

    [Test]
    public void RoadGraph_PreservesOsmLaneMetadataForBothTravelDirections()
    {
        const string json = "{\"elements\":[{\"type\":\"way\",\"id\":5,\"tags\":{\"highway\":\"primary\",\"lanes\":\"4\",\"width\":\"14\"},\"geometry\":[{\"lat\":-1,\"lon\":36},{\"lat\":-1,\"lon\":36.001}]}]}";
        var go = new GameObject("Lane converter");
        var converter = go.AddComponent<CoordinateConverter>();
        converter.mapOrigin = ScriptableObject.CreateInstance<MapOrigin>();
        converter.mapOrigin.originLat = -1; converter.mapOrigin.originLon = 36;
        var graph = RoadGraph.BuildFromOverpassWays(OverpassResponse.Deserialize(json), converter);
        Assert.That(graph.nodes[0].edges[0].laneCount, Is.EqualTo(2));
        Assert.That(graph.nodes[0].edges[0].laneWidth, Is.EqualTo(3.5f).Within(.01f));
        Assert.That(graph.nodes[0].edges[0].oneWay, Is.False);
        Assert.That(graph.nodes[1].edges[0].laneCount, Is.EqualTo(2));
    }

    [Test]
    public void AirRoadGraph_UsesDifferentRoadHalvesForTwoWayTraffic()
    {
        var root = new GameObject("Lane graph");
        var graph = root.AddComponent<AIRoadGraph>();
        var a = new GameObject("A").transform; a.position = Vector3.zero;
        var b = new GameObject("B").transform; b.position = Vector3.forward * 20;
        graph.nodes = new[]
        {
            new AIRoadGraph.RoadNode { point = a, nextNodeIndices = new[] { 1 }, outgoingLaneCounts = new[] { 2 }, outgoingLaneWidths = new[] { 3.5f }, outgoingOneWays = new[] { false } },
            new AIRoadGraph.RoadNode { point = b, nextNodeIndices = new[] { 0 }, outgoingLaneCounts = new[] { 2 }, outgoingLaneWidths = new[] { 3.5f }, outgoingOneWays = new[] { false } }
        };
        float forward = graph.GetLaneOffset(0, 1, 0);
        float reverse = graph.GetLaneOffset(1, 0, 0);
        Assert.That(Mathf.Abs(forward), Is.GreaterThan(1f));
        Assert.That(Mathf.Abs(reverse), Is.GreaterThan(1f));
        // Offsets are measured from each direction's right vector, so their signs match
        // while their world-space positions occupy opposite carriageway halves.
        Assert.That(forward, Is.EqualTo(reverse).Within(.01f));
        Object.DestroyImmediate(root); Object.DestroyImmediate(a.gameObject); Object.DestroyImmediate(b.gameObject);
    }

    [Test]
    public void RoadGraph_AsymmetricLaneTags_GiveEachDirectionItsOwnCount()
    {
        const string json = "{\"elements\":[{\"type\":\"way\",\"id\":6,\"tags\":{\"highway\":\"primary\",\"lanes:forward\":\"2\",\"lanes:backward\":\"1\"},\"geometry\":[{\"lat\":-1,\"lon\":36},{\"lat\":-1,\"lon\":36.001}]}]}";
        var go = new GameObject("Asymmetric lane converter");
        var converter = go.AddComponent<CoordinateConverter>();
        converter.mapOrigin = ScriptableObject.CreateInstance<MapOrigin>();
        converter.mapOrigin.originLat = -1; converter.mapOrigin.originLon = 36;
        var graph = RoadGraph.BuildFromOverpassWays(OverpassResponse.Deserialize(json), converter);

        // The forward directed link has two lanes, the reverse link only one. Sharing
        // half the total between both directions made the flows overlap.
        Assert.That(graph.nodes[0].edges[0].laneCount, Is.EqualTo(2));
        Assert.That(graph.nodes[1].edges[0].laneCount, Is.EqualTo(1));
    }

    [Test]
    public void AirRoadGraph_DrivingSide_MirrorsLaneOffsetForRightHandTraffic()
    {
        var root = new GameObject("Driving side graph");
        var graph = root.AddComponent<AIRoadGraph>();
        var a = new GameObject("A").transform;
        var b = new GameObject("B").transform; b.position = Vector3.forward * 20f;
        graph.nodes = new[]
        {
            new AIRoadGraph.RoadNode { point = a, nextNodeIndices = new[] { 1 }, outgoingLaneCounts = new[] { 1 }, outgoingLaneWidths = new[] { 3.1f }, outgoingOneWays = new[] { false } },
            new AIRoadGraph.RoadNode { point = b, nextNodeIndices = new[] { 0 }, outgoingLaneCounts = new[] { 1 }, outgoingLaneWidths = new[] { 3.1f }, outgoingOneWays = new[] { false } }
        };

        graph.drivesOnLeft = true;
        float leftHand = graph.GetLaneOffset(0, 1, 0);
        graph.drivesOnLeft = false;
        float rightHand = graph.GetLaneOffset(0, 1, 0);

        Assert.That(leftHand, Is.EqualTo(-1.55f).Within(.01f));
        Assert.That(rightHand, Is.EqualTo(1.55f).Within(.01f));

        Object.DestroyImmediate(root);
        Object.DestroyImmediate(a.gameObject);
        Object.DestroyImmediate(b.gameObject);
    }

    [Test]
    public void PlayerCityLibrary_PersistsFavouriteStateByCityCode()
    {
        var city = ScriptableObject.CreateInstance<CityDefinition>();
        city.cityCode = "TESTLANE";
        PlayerCityLibrary.SetFavourite(city, true);
        Assert.That(PlayerCityLibrary.IsFavourite(city), Is.True);
        PlayerCityLibrary.SetFavourite(city, false);
        Assert.That(PlayerCityLibrary.IsFavourite(city), Is.False);
        Object.DestroyImmediate(city);
    }

    [Test]
    public void OsmWay_GetLaneCount_ParsesSingleAndMultipleLanesCorrectly()
    {
        var way4 = new OSMWay { id = 101, tags = new System.Collections.Generic.Dictionary<string, string> { { "highway", "primary" }, { "lanes", "4" } } };
        Assert.That(way4.GetLaneCount(), Is.EqualTo(4));
        Assert.That(way4.GetForwardLaneCount(), Is.EqualTo(2));
        Assert.That(way4.GetBackwardLaneCount(), Is.EqualTo(2));

        var way2 = new OSMWay { id = 102, tags = new System.Collections.Generic.Dictionary<string, string> { { "highway", "secondary" }, { "lanes", "2" } } };
        Assert.That(way2.GetLaneCount(), Is.EqualTo(2));

        var way1 = new OSMWay { id = 103, tags = new System.Collections.Generic.Dictionary<string, string> { { "highway", "service" } } };
        Assert.That(way1.GetLaneCount(), Is.EqualTo(1));
    }

    [Test]
    public void OsmWay_GetLaneCount_SumsForwardAndBackwardLanes()
    {
        var wayAsymmetric = new OSMWay
        {
            id = 201,
            tags = new System.Collections.Generic.Dictionary<string, string>
            {
                { "highway", "primary" },
                { "lanes:forward", "2" },
                { "lanes:backward", "1" }
            }
        };
        Assert.That(wayAsymmetric.GetLaneCount(), Is.EqualTo(3));
        Assert.That(wayAsymmetric.GetForwardLaneCount(), Is.EqualTo(2));
        Assert.That(wayAsymmetric.GetBackwardLaneCount(), Is.EqualTo(1));
    }

    [Test]
    public void RoadWidthPolicy_WidensAsphaltToFitTheWidestDirectionalLaneEnvelope()
    {
        RoadWidthPolicy.ResetForTests();
        try
        {
            RoadWidthPolicy.Request(4.32f);

            var way = new OSMWay
            {
                id = 250,
                tags = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "highway", "primary" },
                    { "lanes:forward", "2" },
                    { "lanes:backward", "1" }
                }
            };

            float laneWidth = way.GetLaneWidthMeters();
            float roadWidth = way.GetRoadWidth();
            Assert.That(laneWidth, Is.EqualTo(4.32f).Within(.01f));
            Assert.That(roadWidth, Is.GreaterThanOrEqualTo(laneWidth * 4f - .01f),
                "the two-lane direction needs two full lanes either side of the centreline");

            // The outermost forward lane centre plus half a wide imported bus must
            // still be inside the rendered asphalt.
            float widestBodyHalf = 3.77f * .5f;
            Assert.That(1.5f * laneWidth + widestBodyHalf,
                Is.LessThanOrEqualTo(roadWidth * .5f + .001f));

            var narrow = new OSMWay
            {
                id = 251,
                tags = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "highway", "secondary" },
                    { "lanes", "2" },
                    { "width", "8" }
                }
            };
            Assert.That(narrow.GetLaneWidthMeters(), Is.EqualTo(4.32f).Within(.01f),
                "a narrow OSM width tag must not leave traffic hanging over the kerb");
            Assert.That(narrow.GetRoadWidth(), Is.EqualTo(8.64f).Within(.01f));

            var shared = new OSMWay
            {
                id = 252,
                tags = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "highway", "service" },
                    { "lanes", "1" }
                }
            };
            Assert.That(shared.GetRoadWidth(), Is.GreaterThanOrEqualTo(4f),
                "a shared single lane still needs a drivable ribbon");
        }
        finally
        {
            RoadWidthPolicy.ResetForTests();
        }
    }

    [Test]
    public void AirRoadGraph_LikelyTurnAngleIgnoresTheReverseLinkAndFindsTheTurn()
    {
        var root = new GameObject("Turn angle graph");
        var graph = root.AddComponent<AIRoadGraph>();
        var a = new GameObject("A").transform;
        var b = new GameObject("B").transform; b.position = Vector3.forward * 20f;
        var c = new GameObject("C").transform; c.position = new Vector3(20f, 0f, 20f);
        graph.nodes = new[]
        {
            new AIRoadGraph.RoadNode { point = a },
            new AIRoadGraph.RoadNode { point = b, nextNodeIndices = new[] { 0, 2 } },
            new AIRoadGraph.RoadNode { point = c }
        };

        // Coming from A towards B, the reverse link to A is excluded and the
        // right-angle exit to C is the only legal continuation.
        Assert.That(graph.GetLikelyTurnAngle(1, Vector3.forward, 0),
            Is.EqualTo(90f).Within(.5f));

        Object.DestroyImmediate(root);
        Object.DestroyImmediate(a.gameObject);
        Object.DestroyImmediate(b.gameObject);
        Object.DestroyImmediate(c.gameObject);
    }

    [Test]
    public void AirRoadGraph_RoadHalfWidthUsesTheWiderDirectionalEnvelopeAndSharedLane()
    {
        var root = new GameObject("Road half-width graph");
        var graph = root.AddComponent<AIRoadGraph>();
        var a = new GameObject("A").transform;
        var b = new GameObject("B").transform; b.position = Vector3.forward * 20f;
        graph.nodes = new[]
        {
            new AIRoadGraph.RoadNode
            {
                point = a, nextNodeIndices = new[] { 1 },
                outgoingLaneCounts = new[] { 2 }, outgoingLaneWidths = new[] { 4.32f },
                outgoingOneWays = new[] { false }, outgoingSharedLanes = new[] { false }
            },
            new AIRoadGraph.RoadNode
            {
                point = b, nextNodeIndices = new[] { 0 },
                outgoingLaneCounts = new[] { 1 }, outgoingLaneWidths = new[] { 4.32f },
                outgoingOneWays = new[] { false }, outgoingSharedLanes = new[] { false }
            }
        };

        // A 2+1 road is rendered with the two-lane envelope on both sides.
        Assert.That(graph.GetRoadHalfWidth(0, 1), Is.EqualTo(8.64f).Within(.01f));
        Assert.That(graph.GetRoadHalfWidth(1, 0), Is.EqualTo(8.64f).Within(.01f));

        // A one-way carriageway only needs its own lanes.
        graph.nodes[0].outgoingOneWays = new[] { true };
        Assert.That(graph.GetRoadHalfWidth(0, 1), Is.EqualTo(4.32f).Within(.01f));

        // A shared single lane is exactly one lane wide.
        graph.nodes[0].outgoingOneWays = new[] { false };
        graph.nodes[0].outgoingSharedLanes = new[] { true };
        Assert.That(graph.GetRoadHalfWidth(0, 1), Is.EqualTo(2.16f).Within(.01f));

        Object.DestroyImmediate(root);
        Object.DestroyImmediate(a.gameObject);
        Object.DestroyImmediate(b.gameObject);
    }

    [Test]
    public void OSMRoadMeshBuilder_BuildRoadSegmentMesh_GeneratesCorrectColumnsForLanes()
    {
        var go = new GameObject("MeshBuilderHolder");
        var builder = go.AddComponent<OSMRoadMeshBuilder>();

        var points = new System.Collections.Generic.List<Vector3>
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 20),
            new Vector3(0, 0, 40)
        };

        // 4 lanes with camber -> 5 lateral columns per longitudinal point (3 * 5 = 15 vertices)
        Mesh mesh4 = builder.BuildRoadSegmentMesh(points, 14f, 4, true);
        Assert.That(mesh4, Is.Not.Null);
        Assert.That(mesh4.vertexCount, Is.EqualTo(15));
        Assert.That(mesh4.triangles.Length, Is.GreaterThan(0));

        // 2 lanes with camber -> 3 lateral columns per longitudinal point (3 * 3 = 9 vertices)
        Mesh mesh2 = builder.BuildRoadSegmentMesh(points, 7f, 2, true);
        Assert.That(mesh2, Is.Not.Null);
        Assert.That(mesh2.vertexCount, Is.EqualTo(9));

        // 1 lane -> 2 lateral columns per longitudinal point (3 * 2 = 6 vertices)
        Mesh mesh1 = builder.BuildRoadSegmentMesh(points, 4f, 1, false);
        Assert.That(mesh1, Is.Not.Null);
        Assert.That(mesh1.vertexCount, Is.EqualTo(6));

        Object.DestroyImmediate(go);
    }

    [Test]
    public void OSMRoadMeshBuilder_BuildLaneMarkings_CreatesRealisticMarkingsForFourAndTwoLaneRoads()
    {
        var go = new GameObject("MarkingHolder");
        var builder = go.AddComponent<OSMRoadMeshBuilder>();
        builder.generateRealisticLaneMarkings = true;
        builder.drawOuterEdgeLines = true;

        var points = new System.Collections.Generic.List<Vector3>
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 30)
        };

        var way4 = new OSMWay
        {
            id = 401,
            tags = new System.Collections.Generic.Dictionary<string, string>
            {
                { "highway", "primary" },
                { "lanes", "4" }
            }
        };

        var roadObj4 = new GameObject("Road4");
        var markings4 = builder.BuildLaneMarkings(roadObj4.transform, points, way4, 14f);
        Assert.That(markings4, Is.Not.Null);
        var mf4 = markings4.GetComponent<MeshFilter>();
        Assert.That(mf4, Is.Not.Null);
        Assert.That(mf4.sharedMesh.vertexCount, Is.GreaterThan(0));
        var mr4 = markings4.GetComponent<MeshRenderer>();
        // 4 lanes two-way should have both white and yellow marking materials (2 submeshes: dashed white + edge lines and double yellow center)
        Assert.That(mr4.sharedMaterials.Length, Is.EqualTo(2));

        var way2 = new OSMWay
        {
            id = 402,
            tags = new System.Collections.Generic.Dictionary<string, string>
            {
                { "highway", "secondary" },
                { "lanes", "2" }
            }
        };
        var roadObj2 = new GameObject("Road2");
        var markings2 = builder.BuildLaneMarkings(roadObj2.transform, points, way2, 7f);
        Assert.That(markings2, Is.Not.Null);
        var mf2 = markings2.GetComponent<MeshFilter>();
        Assert.That(mf2.sharedMesh.vertexCount, Is.GreaterThan(0));

        Object.DestroyImmediate(roadObj4);
        Object.DestroyImmediate(roadObj2);
        Object.DestroyImmediate(go);
    }

    [Test]
    public void AirRoadGraph_PlacesEachDirectionInItsOwnLaneInsteadOfOnTheCentreLine()
    {
        var root = new GameObject("Lane geometry graph");
        var graph = root.AddComponent<AIRoadGraph>();
        var a = new GameObject("A").transform;
        var b = new GameObject("B").transform; b.position = Vector3.forward * 20f;
        graph.nodes = new[]
        {
            new AIRoadGraph.RoadNode { point = a, nextNodeIndices = new[] { 1 }, outgoingLaneCounts = new[] { 1 }, outgoingLaneWidths = new[] { 3.1f }, outgoingOneWays = new[] { false } },
            new AIRoadGraph.RoadNode { point = b, nextNodeIndices = new[] { 0 }, outgoingLaneCounts = new[] { 1 }, outgoingLaneWidths = new[] { 3.1f }, outgoingOneWays = new[] { false } }
        };

        // A two-lane street has one lane each way, half a lane width out from the
        // centre line. The old formula returned 0 here, which is why opposite
        // directions met head-on in the middle of the road. The offset is measured
        // from each direction's own right vector, so the numbers match while the
        // world positions sit on opposite sides of the centre line.
        Assert.That(graph.GetLaneOffset(0, 1, 0), Is.EqualTo(-1.55f).Within(.01f));
        Assert.That(graph.GetLaneOffset(1, 0, 0), Is.EqualTo(-1.55f).Within(.01f));
        Assert.That(graph.GetLaneOffset(1, 0, 9), Is.EqualTo(graph.GetLaneOffset(1, 0, 0)).Within(.001f));
        Vector3 forwardLane = Vector3.Cross(Vector3.up, Vector3.forward) * graph.GetLaneOffset(0, 1, 0);
        Vector3 reverseLane = Vector3.Cross(Vector3.up, Vector3.back) * graph.GetLaneOffset(1, 0, 0);
        Assert.That(forwardLane.x, Is.LessThan(-1f));
        Assert.That(reverseLane.x, Is.GreaterThan(1f));

        // A way tagged as a single lane has no second carriageway to sit in, so both
        // directions keep to the middle of the one lane they share.
        graph.nodes[0].outgoingSharedLanes = new[] { true };
        Assert.That(graph.GetLaneOffset(0, 1, 0), Is.EqualTo(0f).Within(.001f));

        Object.DestroyImmediate(root);
        Object.DestroyImmediate(a.gameObject);
        Object.DestroyImmediate(b.gameObject);
    }

    [Test]
    public void AirRoadGraph_CentresLanesOnOneWayCarriageways()
    {
        var root = new GameObject("One-way lane graph");
        var graph = root.AddComponent<AIRoadGraph>();
        var a = new GameObject("A").transform;
        var b = new GameObject("B").transform; b.position = Vector3.forward * 20f;
        graph.nodes = new[]
        {
            new AIRoadGraph.RoadNode { point = a, nextNodeIndices = new[] { 1 }, outgoingLaneCounts = new[] { 3 }, outgoingLaneWidths = new[] { 3.3f }, outgoingOneWays = new[] { true } },
            new AIRoadGraph.RoadNode { point = b }
        };

        // Three lanes of a one-way road are centred on the carriageway: -3.3, 0, +3.3.
        Assert.That(graph.GetLaneOffset(0, 1, 0), Is.EqualTo(-3.3f).Within(.01f));
        Assert.That(graph.GetLaneOffset(0, 1, 1), Is.EqualTo(0f).Within(.01f));
        Assert.That(graph.GetLaneOffset(0, 1, 2), Is.EqualTo(3.3f).Within(.01f));

        Object.DestroyImmediate(root);
        Object.DestroyImmediate(a.gameObject);
        Object.DestroyImmediate(b.gameObject);
    }

    [Test]
    public void AirRoadGraph_SetMinimumLaneWidth_ExpandsNarrowLinksForWideVehicles()
    {
        var root = new GameObject("Wide vehicle lane graph");
        var graph = root.AddComponent<AIRoadGraph>();
        var a = new GameObject("A").transform;
        var b = new GameObject("B").transform; b.position = Vector3.forward * 20f;
        graph.nodes = new[]
        {
            new AIRoadGraph.RoadNode { point = a, nextNodeIndices = new[] { 1 }, laneWidth = 2.4f, outgoingLaneCounts = new[] { 1 }, outgoingLaneWidths = new[] { 2.4f }, outgoingOneWays = new[] { false } },
            new AIRoadGraph.RoadNode { point = b, nextNodeIndices = new[] { 0 }, laneWidth = 2.4f, outgoingLaneCounts = new[] { 1 }, outgoingLaneWidths = new[] { 2.4f }, outgoingOneWays = new[] { false } }
        };

        // A narrow OSM lane is still driven, but a wide traffic model would
        // overhang it. The pool raises the floor before spawning anything.
        Assert.That(graph.GetLaneWidth(0, 1), Is.EqualTo(AIRoadGraph.MinLaneWidth).Within(.01f));
        Assert.That(Mathf.Abs(graph.GetLaneOffset(0, 1, 0)), Is.EqualTo(AIRoadGraph.MinLaneWidth * .5f).Within(.01f));

        graph.SetMinimumLaneWidth(4.1f);

        Assert.That(graph.MinimumLaneWidth, Is.EqualTo(4.1f).Within(.01f));
        Assert.That(graph.GetLaneWidth(0, 1), Is.EqualTo(4.1f).Within(.01f));
        Assert.That(Mathf.Abs(graph.GetLaneOffset(0, 1, 0)), Is.EqualTo(2.05f).Within(.01f));
        Assert.That(graph.nodes[0].laneWidth, Is.EqualTo(4.1f).Within(.01f));
        Assert.That(graph.nodes[0].outgoingLaneWidths[0], Is.EqualTo(4.1f).Within(.01f),
            "the per-link width must be raised too, not just the node fallback");
        Assert.That(graph.nodes[1].outgoingLaneWidths[0], Is.EqualTo(4.1f).Within(.01f));

        Object.DestroyImmediate(root);
        Object.DestroyImmediate(a.gameObject);
        Object.DestroyImmediate(b.gameObject);
    }

    [Test]
    public void AIVehicleController_OvertakeDecisionRequiresASlowerLeaderAndAClearLane()
    {
        Assert.That(AIVehicleController.ShouldStartOvertake(50f, 20f, 18f, 9f, true), Is.True);
        Assert.That(AIVehicleController.ShouldStartOvertake(50f, 20f, 18f, 9f, false), Is.False,
            "a single-lane link has nowhere to pass");
        Assert.That(AIVehicleController.ShouldStartOvertake(50f, 48f, 18f, 9f, true), Is.False,
            "do not pull out for a leader that is already keeping up");
        Assert.That(AIVehicleController.ShouldStartOvertake(50f, 0f, 60f, 9f, true), Is.False,
            "a leader sixty metres ahead is not a pass yet");
    }

    [Test]
    public void AIVehicleController_OvertakeSpeedIsQuickButNeverOverTheLimit()
    {
        Assert.That(AIVehicleController.OvertakeSpeedKmh(50f, 0f), Is.EqualTo(24f).Within(.01f));
        Assert.That(AIVehicleController.OvertakeSpeedKmh(50f, 30f), Is.EqualTo(42f).Within(.01f));
        Assert.That(AIVehicleController.OvertakeSpeedKmh(30f, 0f), Is.EqualTo(24f).Within(.01f),
            "the pass still respects a low road limit");
        Assert.That(AIVehicleController.OvertakeSpeedKmh(80f, 50f), Is.EqualTo(62f).Within(.01f));
        Assert.That(AIVehicleController.OvertakeSpeedKmh(80f, 75f), Is.EqualTo(80f).Within(.01f),
            "a pass must not clamp below the leader when the limit is generous");
    }

    [Test]
    public void AIVehicleController_LaneChangeCrossesAWidenedLaneInFiniteTime()
    {
        float quarter = AIVehicleController.StepLaneChange(0f, 4.32f, 3f, .25f);
        float half = AIVehicleController.StepLaneChange(quarter, 4.32f, 3f, .5f);
        float done = AIVehicleController.StepLaneChange(half, 4.32f, 3f, 1f);

        Assert.That(quarter, Is.EqualTo(.75f).Within(.01f));
        Assert.That(half, Is.EqualTo(2.25f).Within(.01f));
        Assert.That(done, Is.EqualTo(4.32f).Within(.01f),
            "a 4.32 m lane change should complete in about 1.4 s at 3 m/s");
        Assert.That(AIVehicleController.StepLaneChange(4.32f, 0f, 3f, .25f),
            Is.EqualTo(3.57f).Within(.01f), "returning to lane must also ease, not snap");
    }

    [Test]
    public void TrafficParticipant_LateralClearanceUsesBothBodyWidths()
    {
        var self = new GameObject("self");
        var other = new GameObject("other");
        try
        {
            self.AddComponent<TrafficParticipant>().width = 3.77f;
            other.AddComponent<TrafficParticipant>().width = 1.8f;
            other.transform.position = new Vector3(3f, 0f, 0f);
            Assert.That(TrafficParticipant.IsLateralClear(self.transform, other.transform), Is.False,
                "a wide body still overlaps its own lane at 3 m of separation");
            other.transform.position = new Vector3(3.2f, 0f, 0f);
            Assert.That(TrafficParticipant.IsLateralClear(self.transform, other.transform), Is.True,
                "once both full widths and the mirror gap clear, the pass can release the target");
        }
        finally
        {
            Object.DestroyImmediate(self);
            Object.DestroyImmediate(other);
        }
    }
}
