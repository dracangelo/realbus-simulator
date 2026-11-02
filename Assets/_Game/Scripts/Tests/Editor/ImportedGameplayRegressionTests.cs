using System.Reflection;
using NUnit.Framework;
using UnityEngine;

public class ImportedGameplayRegressionTests
{
    static System.Type LoadedType(string name)
    {
        foreach (var assembly in UnityEngine.Assemblies.CurrentAssemblies.GetLoadedAssemblies())
        {
            var type = assembly.GetType(name, false);
            if (type != null) return type;
        }
        Assert.Fail("Expected loaded type: " + name);
        return null;
    }

    [TestCase("Mapbox.Unity.Map.MapboxDataProperty")]
    [TestCase("Mapbox.Unity.Map.ExtentOptions")]
    [TestCase("Mapbox.Unity.Map.LayerProperties")]
    [TestCase("Mapbox.Unity.Map.AbstractLayer")]
    [TestCase("Mapbox.MapMatching.Tracepoint")]
    [TestCase("Mapbox.MapMatching.MatchObject")]
    public void MapboxConfiguration_HasSerializableBaseHierarchy(string name)
    {
        for (var type = LoadedType(name); type != typeof(object); type = type.BaseType)
            Assert.That(type.IsDefined(typeof(System.SerializableAttribute), false), Is.True, type.FullName);
    }

    [Test]
    public void MapMatchingJson_PreservesTracepointsAndMatchings()
    {
        // Reflection keeps tests independent of the optional Mapbox Assembly-CSharp bridge.
        var jsonType = LoadedType("Mapbox.Json.JsonConvert");
        var responseType = LoadedType("Mapbox.MapMatching.MapMatchingResponse");
        var deserialize = jsonType.GetMethod("DeserializeObject", new[] { typeof(string), typeof(System.Type) });
        const string json = "{\"code\":\"Ok\",\"tracepoints\":[{\"name\":\"Test stop\",\"location\":[36.8,-1.3],\"waypoint_index\":2}],\"matchings\":[{\"confidence\":0.95,\"distance\":1200}]}";
        object response = deserialize.Invoke(null, new object[] { json, responseType });
        var points = (System.Array)responseType.GetField("Tracepoints").GetValue(response);
        var matches = (System.Array)responseType.GetField("Matchings").GetValue(response);
        Assert.That(points.Length, Is.EqualTo(1));
        Assert.That(matches.Length, Is.EqualTo(1));
        object point = points.GetValue(0), match = matches.GetValue(0);
        Assert.That(point.GetType().GetProperty("Name").GetValue(point), Is.EqualTo("Test stop"));
        Assert.That(point.GetType().GetProperty("WaypointIndex").GetValue(point), Is.EqualTo(2));
        Assert.That(match.GetType().GetProperty("Distance").GetValue(match), Is.EqualTo(1200d));
        Assert.That((float)match.GetType().GetProperty("Confidence").GetValue(match), Is.EqualTo(0.95f).Within(0.001f));
    }

    [Test]
    public void OsmRuntimeCaches_StillPopulateFromSourceData()
    {
        const string xml = "<osm><node id='1' lat='-1.3' lon='36.8'/><node id='2' lat='-1.31' lon='36.81'/><way id='10'><nd ref='1'/><nd ref='2'/><tag k='highway' v='residential'/><tag k='lanes' v='2'/></way></osm>";
        var data = OSMParser.Parse(xml);
        Assert.That(data.nodeMap.ContainsKey(1), Is.True);
        Assert.That(data.ways[0].IsRoad(), Is.True);
        Assert.That(data.ways[0].tags["lanes"], Is.EqualTo("2"));
        Assert.That(typeof(OSMWay).GetField("tags").IsNotSerialized, Is.True);
        Assert.That(typeof(OSMData).GetField("nodeMap").IsNotSerialized, Is.True);
    }

    [Test]
    public void LaneAwareTraffic_ClampsAChangedRoadToItsAvailableLanes()
    {
        var graph = Child("Lane graph").AddComponent<AIRoadGraph>();
        var a = Child("A").transform; var b = Child("B").transform;
        b.position = Vector3.forward * 20f;
        graph.nodes = new[]
        {
            new AIRoadGraph.RoadNode { point = a, nextNodeIndices = new[] { 1 }, outgoingLaneCounts = new[] { 3 }, outgoingLaneWidths = new[] { 3.3f }, outgoingOneWays = new[] { true } },
            new AIRoadGraph.RoadNode { point = b, nextNodeIndices = new[] { 0 }, outgoingLaneCounts = new[] { 1 }, outgoingLaneWidths = new[] { 3.1f }, outgoingOneWays = new[] { false } }
        };
        Assert.That(graph.GetLaneCount(0, 1), Is.EqualTo(3));
        Assert.That(graph.GetLaneCount(1, 0), Is.EqualTo(1));
        Assert.That(graph.GetLaneOffset(1, 0, 99), Is.EqualTo(graph.GetLaneOffset(1, 0, 0)).Within(.001f));
    }

    /// <summary>
    /// A junction edge that points back down the road must never be picked while a
    /// forward edge exists. Reversing the directed link mirrors the lane offset onto
    /// the opposite carriageway, and a car still facing its old way then slides
    /// across the centre line: that was the traffic seen driving on the wrong side.
    /// </summary>
    [Test]
    public void RoadGraph_RefusesABackwardEdgeWhileAForwardEdgeExists()
    {
        var graph = Child("Wrong-side graph").AddComponent<AIRoadGraph>();
        var current = Child("Junction").transform;
        var ahead = Child("Ahead").transform;
        var stray = Child("Backwards link").transform;
        var behind = Child("Behind").transform;
        ahead.position = new Vector3(0f, 0f, 22f);
        stray.position = new Vector3(.6f, 0f, -18f);
        behind.position = new Vector3(0f, 0f, -15f);
        graph.nodes = new[]
        {
            new AIRoadGraph.RoadNode { point = current, nextNodeIndices = new[] { 1, 2, 3 } },
            new AIRoadGraph.RoadNode { point = ahead, nextNodeIndices = new[] { 0 } },
            new AIRoadGraph.RoadNode { point = stray, nextNodeIndices = new[] { 0 } },
            new AIRoadGraph.RoadNode { point = behind, nextNodeIndices = new[] { 0 } }
        };

        for (int i = 0; i < 200; i++)
            Assert.That(graph.GetRandomNextNode(0, 3), Is.EqualTo(1),
                "a backwards edge at a junction must never be chosen over a forward one");
    }

    /// <summary>
    /// The forward-only rule must not trap traffic: a cul-de-sac still turns round,
    /// because the edge it arrived on is the only way out.
    /// </summary>
    [Test]
    public void RoadGraph_StillReversesAtADeadEnd()
    {
        var graph = Child("Dead-end graph").AddComponent<AIRoadGraph>();
        var end = Child("End").transform;
        var entry = Child("Entry").transform;
        entry.position = new Vector3(0f, 0f, 12f);
        graph.nodes = new[]
        {
            new AIRoadGraph.RoadNode { point = end, nextNodeIndices = new[] { 1 } },
            new AIRoadGraph.RoadNode { point = entry, nextNodeIndices = new[] { 0 } }
        };

        Assert.That(graph.GetRandomNextNode(0, 1), Is.EqualTo(1));
    }

    /// <summary>
    /// Lane offsets are laid out per directed link, so the same lane index sits on
    /// opposite sides of the centre line once a link is read backwards. This is the
    /// geometry that punished a reversed car, and it has to stay that way: the fix
    /// belongs in the edge picker, not in the offset.
    /// </summary>
    [Test]
    public void LaneOffset_MirrorsAcrossTheCentreLineWhenALinkIsReversed()
    {
        var graph = Child("Lane graph").AddComponent<AIRoadGraph>();
        var a = Child("A").transform;
        var b = Child("B").transform;
        b.position = new Vector3(0f, 0f, 20f);
        graph.nodes = new[]
        {
            new AIRoadGraph.RoadNode { point = a, laneWidth = 3.5f, nextNodeIndices = new[] { 1 },
                outgoingLaneCounts = new[] { 2 }, outgoingLaneWidths = new[] { 3.5f } },
            new AIRoadGraph.RoadNode { point = b, laneWidth = 3.5f, nextNodeIndices = new[] { 0 },
                outgoingLaneCounts = new[] { 2 }, outgoingLaneWidths = new[] { 3.5f } }
        };

        Vector3 forwardRight = Vector3.Cross(Vector3.up, (b.position - a.position).normalized);
        Vector3 backwardRight = Vector3.Cross(Vector3.up, (a.position - b.position).normalized);
        float forwardLateral = forwardRight.x * graph.GetLaneOffset(0, 1, 0);
        float backwardLateral = backwardRight.x * graph.GetLaneOffset(1, 0, 0);

        Assert.That(forwardLateral, Is.EqualTo(-1.75f).Within(.01f));
        Assert.That(forwardLateral * backwardLateral, Is.LessThan(0f),
            "both links must put the kerb lane on their own driving side");
    }

    /// <summary>
    /// A body whose steering target is behind it must slow to a crawl instead of
    /// holding cruise, so a bad turn cannot carry it sideways across the road. The
    /// rule is shared by both simulation tiers.
    /// </summary>
    [Test]
    public void HardTurnLimit_CreepsInsteadOfHoldingCruise()
    {
        Assert.That(AIVehicleController.HardTurnSpeedLimitKmh(50f, 1f), Is.EqualTo(50f).Within(.001f));
        Assert.That(AIVehicleController.HardTurnSpeedLimitKmh(50f, .34f), Is.LessThan(14f));
        Assert.That(AIVehicleController.HardTurnSpeedLimitKmh(50f, 0f), Is.EqualTo(10.37f).Within(.05f));
        Assert.That(AIVehicleController.HardTurnSpeedLimitKmh(50f, -1f), Is.EqualTo(0f).Within(.001f));
        Assert.That(AIVehicleController.HardTurnSpeedLimitKmh(4f, -1f), Is.EqualTo(0f).Within(.001f));
    }

    GameObject root;

    [SetUp] public void Setup() { root = new GameObject("Regression fixture"); }
    [TearDown] public void Cleanup() { Object.DestroyImmediate(root); }

    GameObject Child(string name)
    {
        var child = new GameObject(name);
        child.transform.SetParent(root.transform, false);
        return child;
    }

    [Test]
    public void DaylightAutoBinding_DoesNotDisableFrontWheels()
    {
        var body = Child("Bus");
        body.AddComponent<Rigidbody>();
        var front = new GameObject("FrontLeft", typeof(WheelCollider));
        front.transform.SetParent(body.transform);
        var headlight = Child("Front headlight").AddComponent<Light>();
        var clock = Child("Clock").AddComponent<TimeOfDaySystem>();
        clock.startTimeMinutes = 720f;
        typeof(TimeOfDaySystem).GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(clock, null);
        Assert.That(front.activeInHierarchy, Is.True);
        Assert.That(headlight.enabled, Is.False);
    }

    [Test]
    public void Daylight_ProtectsPhysicsInStaleExplicitLightingAssignments()
    {
        var body = Child("Bus");
        body.AddComponent<Rigidbody>();
        var front = new GameObject("FrontRight", typeof(WheelCollider));
        front.transform.SetParent(body.transform);
        var clock = Child("Clock").AddComponent<TimeOfDaySystem>();
        clock.currentTimeMinutes = 720f;
        clock.busHeadLightObjects = new[] { front, body };
        typeof(TimeOfDaySystem).GetMethod("ApplyTimeOfDayImmediate", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(clock, null);
        Assert.That(body.activeSelf && front.activeSelf, Is.True);
    }

    [Test]
    public void ImportedCameraIsolation_LeavesGameplayCameraEnabled()
    {
        var main = Child("Main Camera").AddComponent<Camera>();
        var visual = Child("Model");
        var embedded = new GameObject("CINEMA_4D_Editor", typeof(Camera), typeof(AudioListener));
        embedded.transform.SetParent(visual.transform);
        ImportedVehicleVisualRepair.DisableEmbeddedCameras(visual.transform);
        Assert.That(main.enabled, Is.True);
        Assert.That(embedded.GetComponent<Camera>().enabled, Is.False);
        Assert.That(embedded.GetComponent<AudioListener>().enabled, Is.False);
    }

    [TestCase(-5f, 0f)]
    [TestCase(5f, 5f)]
    [TestCase(15f, 10f)]
    public void RoadProjection_ClampsToSegment(float z, float expected)
    {
        var point = RoadSpawnResolver.ProjectOnSegment(new Vector3(4, 0, z), Vector3.zero, Vector3.forward * 10);
        Assert.That(point, Is.EqualTo(new Vector3(0, 0, expected)));
    }

    [Test]
    public void Teleport_UpdatesInterpolatedPhysicsPose()
    {
        var bus = Child("Bus").AddComponent<BusController>();
        var body = bus.GetComponent<Rigidbody>();
        body.interpolation = RigidbodyInterpolation.Interpolate;
        var destination = new Vector3(100, 2, 50);
        bus.Teleport(destination, Quaternion.Euler(0, 45, 0));
        Assert.That(body.position, Is.EqualTo(destination));
        Assert.That(Quaternion.Angle(body.rotation, bus.transform.rotation), Is.LessThan(.01f));
    }

    [TestCase("KE", true)]
    [TestCase("UG", true)]
    [TestCase("TZ", true)]
    [TestCase("RW", false)]
    [TestCase("DE", false)]
    public void CountryTrafficSide_UsesCountryCode(string code, bool left)
    {
        Assert.That(CountryDefinition.DefaultDrivesOnLeft(code), Is.EqualTo(left));
    }

    [Test]
    public void BusStartup_RemovesDuplicateDragAndEnablesInterpolation()
    {
        var bus = Child("Bus").AddComponent<BusController>();
        var body = bus.GetComponent<Rigidbody>();
        body.linearDamping = .15f;
        typeof(BusController).GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(bus, null);
        Assert.That(body.linearDamping, Is.Zero);
        Assert.That(body.interpolation, Is.EqualTo(RigidbodyInterpolation.Interpolate));
    }

    [TestCase(0f)]
    [TestCase(30f)]
    [TestCase(80f)]
    public void SteeringLimit_RetainsParkingAndLimitsHighSpeedLateralForce(float speed)
    {
        float angle = BusController.SafeSteeringLimit(42f, 6.2f, speed);
        if (speed == 0f) Assert.That(angle, Is.EqualTo(42f));
        float acceleration = speed / 3.6f * (speed / 3.6f) * Mathf.Tan(angle * Mathf.Deg2Rad) / 6.2f;
        Assert.That(acceleration, Is.LessThanOrEqualTo(3.001f));
    }

    [TestCase(20f, 60f, 30f)]
    [TestCase(-10f, 170f, -40f)]
    public void CameraHorizon_DoesNotInheritChassisRoll(float pitch, float yaw, float roll)
    {
        Quaternion rotation = MobileCameraController.HorizonRotation(Quaternion.Euler(pitch, yaw, roll));
        Assert.That(Vector3.Angle(rotation * Vector3.up, Vector3.up), Is.LessThan(.01f));
        Assert.That(Mathf.Abs(Mathf.DeltaAngle(rotation.eulerAngles.y, yaw)), Is.LessThan(.01f));
    }

    [Test]
    public void FourLaneSpawn_UsesOuterLaneCenterAndSingleLaneStaysCentered()
    {
        Assert.That(RoadSpawnResolver.LaneOffset(14f, 4, false, true, 2.6f), Is.EqualTo(-5.25f));
        Assert.That(RoadSpawnResolver.LaneOffset(14f, 4, false, false, 2.6f), Is.EqualTo(5.25f));
        Assert.That(RoadSpawnResolver.LaneOffset(4.5f, 1, false, true, 2.6f), Is.Zero);
    }

    [Test]
    public void SpawnHeading_FollowsLocalRouteTangentInsteadOfDistantStopBearing()
    {
        var path = new[] { Vector3.zero, new Vector3(0, 0, 20), new Vector3(30, 0, 20) };
        Assert.That(RoadSpawnResolver.PathHeading(path, new Vector3(2, 0, 4), Vector3.right), Is.EqualTo(Vector3.forward));
    }

    [Test]
    public void RouteHeading_FollowsDrivenPathEvenWhenStopOrderIsReversed()
    {
        var route = ScriptableObject.CreateInstance<BusRoute>();
        try
        {
            // The bus drives north along this path. An imported route can still list
            // its stops in the opposite order; folding the tangent onto that chord is
            // what put shelters on the far kerb, so the driven path has to win.
            route.pathPoints = new[] { new Vector3(0, 0, 0), new Vector3(0, 0, 20), new Vector3(0, 0, 40) };
            Vector3 heading = RoadSpawnResolver.RouteHeading(route, new Vector3(0, 0, 10), Vector3.back);
            Assert.That(heading, Is.EqualTo(Vector3.forward));
        }
        finally
        {
            Object.DestroyImmediate(route);
        }
    }

    [Test]
    public void NearestPathFrame_ReportsTheTangentInPathOrder()
    {
        var path = new[] { new Vector3(0, 0, 0), new Vector3(0, 0, 20) };
        Assert.That(RoadSpawnResolver.NearestPathFrame(path, new Vector3(1, 0, 5),
            out Vector3 point, out Vector3 tangent), Is.True);
        Assert.That(tangent, Is.EqualTo(Vector3.forward));
        Assert.That(point, Is.EqualTo(new Vector3(0, 0, 5)));
    }

    [Test]
    public void KerbSide_AgreesWithTheSideTryRoadsideOffsetsTo()
    {
        // Facing +Z, the left kerb is -X. In left-hand traffic the bus keeps left and
        // docks there; in right-hand traffic it docks at +X. TryRoadside offsets the
        // same vector by the same sign, so placement and the smoke audit share it.
        Assert.That(RoadSpawnResolver.KerbSide(Vector3.forward, true), Is.EqualTo(Vector3.left));
        Assert.That(RoadSpawnResolver.KerbSide(Vector3.forward, false), Is.EqualTo(Vector3.right));
        Assert.That(RoadSpawnResolver.KerbSide(Vector3.zero, true), Is.EqualTo(Vector3.zero));
    }

    static RoadSurfaceIndex Crossroads()
    {
        var roads = new RoadSurfaceIndex();
        roads.Add(new Vector3(0, 0, -40), new Vector3(0, 0, 40), 8, 10);
        roads.Add(new Vector3(-40, 0, 0), new Vector3(40, 0, 0), 8, 20);
        return roads;
    }

    [Test]
    public void JunctionClearance_DoesNotRequireSharedNodeIds()
    {
        var roads = Crossroads();
        Assert.That(roads.Contains(new Vector3(4.2f, 0, 0), 2, 10), Is.True);
        Assert.That(roads.Contains(new Vector3(4.2f, 0, 20), 2, 10), Is.False);
        var runs = new System.Collections.Generic.List<Vector2>(roads.ClearRuns(new Vector3(4.2f, 0, -30), new Vector3(4.2f, 0, 30), 10, 3));
        Assert.That(runs.Count, Is.EqualTo(2));
        Assert.That(runs[0].y, Is.LessThan(.4f));
        Assert.That(runs[1].x, Is.GreaterThan(.6f));
    }

    [TestCase(2f, true)]
    [TestCase(12f, false)]
    public void BuildingFootprintClearance_UsesFullRoadWidth(float x, bool overlaps)
    {
        var roads = new RoadSurfaceIndex();
        roads.Add(new Vector3(0, 0, -40), new Vector3(0, 0, 40), 8, 10);
        var footprint = new[] { new Vector3(x, 0, 8), new Vector3(x + 5, 0, 8), new Vector3(x + 5, 0, 15), new Vector3(x, 0, 15) };
        Assert.That(roads.OverlapsFootprint(footprint), Is.EqualTo(overlaps));
    }

    [Test]
    public void RoadInsideLargeFootprint_IsStillRejected()
    {
        var roads = new RoadSurfaceIndex();
        roads.Add(new Vector3(-2, 0, 0), new Vector3(2, 0, 0), 4, 1);
        Assert.That(roads.OverlapsFootprint(new[] { new Vector3(-10, 0, -10), new Vector3(10, 0, -10),
            new Vector3(10, 0, 10), new Vector3(-10, 0, 10) }), Is.True);
    }

    [TestCase(true, -4.6f)]
    [TestCase(false, 4.6f)]
    public void StopPlacement_UsesKerbAndAvoidsCrossingRoad(bool left, float expectedX)
    {
        var roads = Crossroads();
        Assert.That(roads.TryRoadside(Vector3.zero, Vector3.forward, left, out var kerb, out var rotation), Is.True);
        Assert.That(kerb.x, Is.EqualTo(expectedX).Within(.01f));
        Assert.That(Mathf.Abs(kerb.z), Is.GreaterThan(4f));
        Assert.That(roads.Contains(kerb, .3f), Is.False);
        Assert.That(Vector3.Dot(rotation * Vector3.forward, Vector3.forward), Is.GreaterThan(.99f));
    }

    [Test]
    public void PathHeading_BreaksATieWithTheDrivenApproach()
    {
        // An out-and-back route has two segments at the same distance from a stop,
        // pointing opposite ways. The old first-match rule could pick the return
        // leg and mirror the shelter onto the oncoming kerb.
        var path = new[] { new Vector3(0, 0, 0), new Vector3(0, 0, 20), new Vector3(0, 0, 0) };
        Vector3 requested = new Vector3(2, 0, 10);

        Assert.That(RoadSpawnResolver.PathHeading(path, requested, Vector3.back, Vector3.forward),
            Is.EqualTo(Vector3.forward));
        Assert.That(RoadSpawnResolver.PathHeading(path, requested, Vector3.forward, Vector3.back),
            Is.EqualTo(Vector3.back));
        // With no approach hint the first segment stays the deterministic fallback.
        Assert.That(RoadSpawnResolver.PathHeading(path, requested, Vector3.back),
            Is.EqualTo(Vector3.forward));
    }

    [Test]
    public void PathHeading_DisqualifiesOpposingFlowEvenIfCloser()
    {
        var path = new[]
        {
            new Vector3(2, 0, 0), new Vector3(2, 0, 50),
            new Vector3(1, 0, 50), new Vector3(1, 0, 0)
        };
        Vector3 requested = new Vector3(0, 0, 25);
        Vector3 heading = RoadSpawnResolver.PathHeading(path, requested, Vector3.forward, Vector3.forward);
        Assert.That(heading, Is.EqualTo(Vector3.forward));
    }

    [Test]
    public void StopApproachHeading_InfersFromConsecutiveStopsWhenGraphNull()
    {
        var route = ScriptableObject.CreateInstance<BusRoute>();
        route.stops = new[]
        {
            new BusStopData { stopName = "A", latitude = -1.280, longitude = 36.820 },
            new BusStopData { stopName = "B", latitude = -1.285, longitude = 36.820 },
            new BusStopData { stopName = "C", latitude = -1.290, longitude = 36.820 }
        };
        Vector3 stopPos = new Vector3(0, 0, 100);
        Vector3 fallback = Vector3.forward;
        Vector3 heading = RoadSpawnResolver.StopApproachHeading(route, 1, stopPos, fallback, null);
        Assert.That(heading.sqrMagnitude, Is.GreaterThan(0.01f));
        Object.DestroyImmediate(route);
    }

    [Test]
    public void TryRoadside_FindsFallbackKerbWhenClearanceCheckOverlaps()
    {
        var roads = new RoadSurfaceIndex();
        roads.Add(new Vector3(0, 0, -20), new Vector3(0, 0, 20), 8, 10);
        Assert.That(roads.TryRoadside(new Vector3(0, 0, 5), Vector3.forward, true, out var kerb, out var rot), Is.True);
        Assert.That(kerb.x, Is.LessThan(0f));
        Assert.That(Vector3.Dot(rot * Vector3.forward, Vector3.forward), Is.GreaterThan(0.9f));
    }

    [Test]
    public void WaitingQueue_IsLaidOutOnThePavementSideOfTheKerb()
    {
        var anchor = new Vector3(10, 0, -4);
        var outward = new Vector3(0, 0, 1);
        var along = new Vector3(1, 0, 0);
        var seen = new System.Collections.Generic.HashSet<Vector3>();

        for (int i = 0; i < 15; i++)
        {
            Vector3 slot = BusStopPlatformRegistry.LayoutSlot(anchor, outward, along, i);
            Assert.That(Vector3.Dot(slot - anchor, outward), Is.GreaterThanOrEqualTo(0f),
                $"passenger {i} must never stand on the carriageway side of the kerb");
            Assert.That(seen.Add(slot), Is.True, $"passenger {i} shares a slot with another passenger");
        }

        // The queue also spreads along the kerb instead of stacking on one point.
        Vector3 first = BusStopPlatformRegistry.LayoutSlot(anchor, outward, along, 0);
        Vector3 last = BusStopPlatformRegistry.LayoutSlot(anchor, outward, along, 4);
        Assert.That(Vector3.Dot(last - first, along), Is.GreaterThan(1f));
    }

    [Test]
    public void RightAngleRoadJoin_MaintainsBothRoadWidths()
    {
        var points = new[] { Vector3.back * 10, Vector3.zero, Vector3.right * 10 };
        Vector3 offset = OSMRoadMeshBuilder.RoadJoinOffset(points, 1, 4);
        Assert.That(offset.x, Is.EqualTo(4).Within(.01f));
        Assert.That(offset.z, Is.EqualTo(-4).Within(.01f));
    }

    // ── First-run tutorial ───────────────────────────────────────────────────

    [TestCase(0, 0, true)]
    [TestCase(1, 0, false)]
    [TestCase(0, 1, false)]
    [TestCase(1, 1, false)]
    public void FirstDriveTutorial_OpensOnlyOncePerInstall(int completionFlag, int presentedFlag, bool expected)
    {
        Assert.That(FirstDriveTutorial.ShouldOpenForFirstRun(completionFlag, presentedFlag), Is.EqualTo(expected));
    }

    // ── Dashed lane markings ─────────────────────────────────────────────────
    // The dash walk advances a distance along each clear run. When the run's
    // residual was smaller than a float at that distance can represent,
    // `t + step` rounded straight back to `t`, so the walk repeated the same
    // iteration forever and froze the gameplay scene while the streamer was
    // still drawing roads (Limuru Road stalled at t = 10.64229 m with a
    // 4.768372e-07 m step, which is half an ulp there).

    /// <summary>Spacing between <paramref name="value"/> and the next float above it.</summary>
    static float Ulp(float value)
    {
        int bits = System.BitConverter.ToInt32(System.BitConverter.GetBytes(value), 0);
        return System.BitConverter.ToSingle(System.BitConverter.GetBytes(bits + 1), 0) - value;
    }

    [TestCase(10.642290f)]
    [TestCase(6f)]
    [TestCase(1f)]
    [TestCase(47.217f)]
    [TestCase(1000f)]
    public void DashedMarkingAdvance_RejectsStepsTooSmallToMoveTheWalk(float t)
    {
        // A quarter of an ulp can never round up, whatever the tie-breaking.
        Assert.That(OSMRoadMeshBuilder.MarkingMeshBuilder.TryAdvance(t, Ulp(t) * .25f, out float next), Is.False);
        Assert.That(next, Is.EqualTo(t), "a rejected step must not move the walk");

        Assert.That(OSMRoadMeshBuilder.MarkingMeshBuilder.TryAdvance(t, Ulp(t), out float advanced), Is.True,
            "a whole ulp always moves the walk");
        Assert.That(advanced, Is.GreaterThan(t));
    }

    [Test]
    public void DashedMarkingAdvance_RejectsNonPositiveAndNonFiniteSteps()
    {
        Assert.That(OSMRoadMeshBuilder.MarkingMeshBuilder.TryAdvance(12f, 0f, out _), Is.False);
        Assert.That(OSMRoadMeshBuilder.MarkingMeshBuilder.TryAdvance(12f, -1f, out _), Is.False);
        Assert.That(OSMRoadMeshBuilder.MarkingMeshBuilder.TryAdvance(12f, float.NaN, out _), Is.False);
        Assert.That(OSMRoadMeshBuilder.MarkingMeshBuilder.TryAdvance(12f, 3f, out float next), Is.True);
        Assert.That(next, Is.EqualTo(15f));
    }

    [Test]
    public void DashedMarkingWalk_ProducesDashesForEveryRunLengthAndStaysFinite()
    {
        var builder = new OSMRoadMeshBuilder.MarkingMeshBuilder();
        OSMRoadMeshBuilder.MarkingMeshBuilder.ResetStallDiagnostics();

        for (float length = 3f; length < 48f; length += .007f)
        {
            int before = builder.vertices.Count;
            var line = new System.Collections.Generic.List<Vector3> { Vector3.zero, new Vector3(length, 0f, 0f) };
            builder.AddDashedStrip(line, .15f, 3f, 3f, null, 1, .35f, .02f);
            Assert.That(builder.vertices.Count, Is.GreaterThan(before), $"a {length:F3} m run drew no dash");
        }

        foreach (var vertex in builder.vertices)
            Assert.That(float.IsNaN(vertex.x) || float.IsInfinity(vertex.x) ||
                        float.IsNaN(vertex.z) || float.IsInfinity(vertex.z), Is.False,
                "marking geometry must stay finite");
    }

    [Test]
    public void DashedMarkingWalk_KeepsGoingWhenTheSurfaceIndexClearsTheRun()
    {
        var roads = new RoadSurfaceIndex();
        roads.Add(new Vector3(0, 0, -40), new Vector3(0, 0, 40), 8, 10);
        var builder = new OSMRoadMeshBuilder.MarkingMeshBuilder();
        OSMRoadMeshBuilder.MarkingMeshBuilder.ResetStallDiagnostics();

        builder.AddDashedStrip(new System.Collections.Generic.List<Vector3> { new Vector3(0, 0, -20), new Vector3(0, 0, 20) }, .15f, 3f, 3f, roads, 10, .35f, .02f);

        Assert.That(builder.vertices.Count, Is.GreaterThan(0));
        Assert.That(OSMRoadMeshBuilder.MarkingMeshBuilder.UnadvanceableRunCount, Is.Zero,
            "a run that can advance must never be reported as stalled");
    }

    [TestCase(true, -2f)]
    [TestCase(false, 2f)]
    public void LaneOffset_UsesTrafficSideAndClearsRoadEdge(bool left, float expected)
    {
        Assert.That(RoadSpawnResolver.LaneOffset(8, 2, false, left, 2.6f), Is.EqualTo(expected));
        Assert.That(RoadSpawnResolver.LaneOffset(4, 1, true, left, 2.6f), Is.Zero);
        Assert.That(Mathf.Abs(RoadSpawnResolver.LaneOffset(4, 2, false, left, 2.6f)), Is.LessThan(.36f));
    }

    [TestCase("residential", true)]
    [TestCase("motorway", false)]
    [TestCase("trunk", false)]
    [TestCase("footway", false)]
    public void ProceduralFill_OnlyUsesUrbanRoadTypes(string road, bool expected)
    {
        Assert.That(ProceduralBuildingFill.CanFillRoad(road), Is.EqualTo(expected));
    }

    const string Polygon = "[[[36.810,-1.280],[36.811,-1.280],[36.811,-1.281],[36.810,-1.280]]]";

    [Test]
    public void AndroidPackagedBuildings_TryJsonBeforeXml()
    {
        const string folder = "jar:file:///example.apk!/assets/Cities/NBO/";
        Assert.That(OSMBuildingLoader.SelectPreferredBuildingsPath(folder + "buildings.xml", folder + "buildings.json"),
            Is.EqualTo(folder + "buildings.json"));
    }

    [TestCase("Polygon", Polygon, 1)]
    [TestCase("MultiPolygon", "[" + Polygon + "," + Polygon + "]", 2)]
    public void OvertureConversion_ProducesLoadableFootprints(string type, string coordinates, int expected)
    {
        var city = ScriptableObject.CreateInstance<CityDefinition>();
        try
        {
            string feature = "{\"type\":\"Feature\",\"properties\":{\"id\":\"example\",\"height\":12,\"sources\":[{\"dataset\":\"example-source\"}]},\"geometry\":{\"type\":\"" + type + "\",\"coordinates\":" + coordinates + "}}";
            string json = "{\"type\":\"FeatureCollection\",\"features\":[" + feature + "," + feature + "]}";
            string converted = OvertureBuildingConverter.ConvertGeoJson(json, city, out int count, out int skipped);
            Assert.That(count, Is.EqualTo(expected));
            Assert.That(skipped, Is.EqualTo(expected));
            var data = OSMParser.ParseOverpassJson(converted);
            Assert.That(data.ways.Count, Is.EqualTo(expected));
            Assert.That(data.ways[0].tags["height"], Is.EqualTo("12"));
            Assert.That(converted, Does.Contain("example-source"));
        }
        finally { Object.DestroyImmediate(city); }
    }

    [TestCase("[]")]
    [TestCase("[[[200,0],[201,0],[201,1],[200,0]]]")]
    [TestCase("[[[0,0],[1,0],[1,1],[0,0]]]")]
    [TestCase("[[[36.81,-1.28],[36.811,-1.28],[36.811,-1.281],[36.81,-1.28]],[[36.81,-1.28]]]")]
    public void OvertureConversion_RejectsEmptyInvalidOutsideAndHoledGeometry(string coordinates)
    {
        var city = ScriptableObject.CreateInstance<CityDefinition>();
        try
        {
            string json = "{\"type\":\"FeatureCollection\",\"features\":[{\"geometry\":{\"type\":\"Polygon\",\"coordinates\":" + coordinates + "}}]}";
            Assert.Throws<System.FormatException>(() => OvertureBuildingConverter.ConvertGeoJson(json, city, out _, out _));
        }
        finally { Object.DestroyImmediate(city); }
    }
}
