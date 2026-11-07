using NUnit.Framework;
using UnityEngine;

public class RealismSystemsTests
{
    [TestCase(450f)] [TestCase(1050f)]
    public void RushHourPeaksRepeatAcrossMidnight(float minutes)
    {
        Assert.AreEqual(5f, RealismRules.DailyDensity(minutes), 0.001f);
        Assert.AreEqual(RealismRules.DailyDensity(minutes), RealismRules.DailyDensity(minutes + 1440f), 0.001f);
    }

    [Test]
    public void DailyTrafficVolume_HasNightTroughDaytimeFlowAndCommuterPeaks()
    {
        float night = RealismRules.DailyTrafficVolume(3f * 60f);
        float lunchtime = RealismRules.DailyTrafficVolume(12f * 60f);
        float morningPeak = RealismRules.DailyTrafficVolume(7.5f * 60f);
        float eveningPeak = RealismRules.DailyTrafficVolume(17.5f * 60f);

        Assert.That(night, Is.InRange(0f, 1f));
        Assert.That(morningPeak, Is.InRange(0f, 1f));
        Assert.That(night, Is.LessThan(lunchtime - .2f),
            "03:00 must be visibly quieter than the middle of the day");
        Assert.That(morningPeak, Is.GreaterThan(lunchtime + .15f),
            "the morning commute must be a distinct peak");
        Assert.That(eveningPeak, Is.GreaterThan(lunchtime + .15f),
            "the evening commute must be a distinct peak");
        Assert.AreEqual(morningPeak, RealismRules.DailyTrafficVolume(7.5f * 60f + 1440f), .001f);
    }
    [Test]
    public void FullLoadAndWornTyresUseSpecifiedThresholds()
    {
        Assert.AreEqual(1f, RealismRules.LoadFactor(0, 80));
        Assert.AreEqual(1.18f, RealismRules.LoadFactor(80, 80), 0.001f);
        Assert.AreEqual(1.18f, RealismRules.LoadFactor(100, 80), 0.001f);
        Assert.AreEqual(1f, RealismRules.TyreGrip(0.5f));
        Assert.AreEqual(0.72f, RealismRules.TyreGrip(1f), 0.001f);
    }
    [Test]
    public void DaylightPeaksAtNoonAndIsDarkAtBothEndsOfNight()
    {
        Assert.AreEqual(0f, RealismRules.Daylight(0));
        Assert.AreEqual(1f, RealismRules.Daylight(720), 0.001f);
        Assert.AreEqual(0f, RealismRules.Daylight(1200));
        Assert.AreEqual(0f, RealismRules.Daylight(1440));
    }
    [Test]
    public void BrakingEnvelopeStopsAtClearanceAndTileTiersHonorBoundaries()
    {
        Assert.AreEqual(0f, RealismRules.StoppingSpeedKmh(5, 5, 6));
        Assert.Greater(RealismRules.StoppingSpeedKmh(25, 5, 6), RealismRules.StoppingSpeedKmh(10, 5, 6));
        Assert.AreEqual(0, RealismRules.TileTier(200));
        Assert.AreEqual(1, RealismRules.TileTier(200.01f));
        Assert.AreEqual(1, RealismRules.TileTier(500));
        Assert.AreEqual(2, RealismRules.TileTier(500.01f));
    }

    [Test]
    public void FollowingSpeedKeepsPaceWithAMovingLeaderButStopsForAStationaryOne()
    {
        float stopped = RealismRules.SafeFollowingSpeedKmh(50f, 0f, 20f, 5f, 6f);
        float moving = RealismRules.SafeFollowingSpeedKmh(50f, 30f, 20f, 5f, 6f);

        Assert.Less(stopped, 50f, "a stopped leader must pull the follower below its cruise speed");
        Assert.AreEqual(50f, moving, .5f, "a moving leader should not force unnecessary braking");
        Assert.Greater(moving, stopped);
        Assert.AreEqual(0f, RealismRules.SafeFollowingSpeedKmh(50f, 0f, 5f, 5f, 6f),
            "at the clearance the follower must stop");
        Assert.AreEqual(0f, RealismRules.SafeFollowingSpeedKmh(50f, 30f, 4f, 5f, 6f),
            "a moving leader must not let an overlapping follower keep pace");
    }

    [Test]
    public void JunctionApproachSlowsForTurnsAndRestoresSpeedOnTheOpenRoad()
    {
        Assert.AreEqual(1f, RealismRules.TurnSpeedFactor(0f), .001f);
        Assert.Less(RealismRules.TurnSpeedFactor(90f), .55f,
            "a right-angle turn should need a real speed reduction");
        Assert.Less(RealismRules.TurnSpeedFactor(135f), RealismRules.TurnSpeedFactor(30f));

        Assert.AreEqual(50f, RealismRules.ApproachSpeedKmh(50f, 20f, 40f, 30f), .01f);
        Assert.AreEqual(20f, RealismRules.ApproachSpeedKmh(50f, 20f, 0f, 30f), .01f);
        Assert.That(RealismRules.ApproachSpeedKmh(50f, 20f, 15f, 30f), Is.InRange(20f, 50f));
    }

    [Test]
    public void OvertakeRunwayScalesWithSpeedAndBlocksShortBlocks()
    {
        Assert.IsTrue(RealismRules.HasOvertakeRunway(40f, 30f));
        Assert.IsFalse(RealismRules.HasOvertakeRunway(15f, 30f));
        Assert.IsFalse(RealismRules.HasOvertakeRunway(30f, 70f),
            "a fast car needs more than a short city block before it pulls out");
    }

    [Test]
    public void DriverSpeedVariationIsSmallDeterministicAndNotUniform()
    {
        float first = RealismRules.SpeedFactor(12);
        Assert.AreEqual(first, RealismRules.SpeedFactor(12), .0001f);
        bool sawFast = false;
        bool sawSlow = false;
        for (int i = 0; i < 120; i++)
        {
            float factor = RealismRules.SpeedFactor(i);
            Assert.That(factor, Is.InRange(.92f, 1.06f));
            sawFast |= factor > 1.02f;
            sawSlow |= factor < .96f;
        }
        Assert.IsTrue(sawFast && sawSlow, "the pool should contain both quicker and slower drivers");
    }
    [Test]
    public void PassengerReactionsAreIndependentOfPhysicsStepSize()
    {
        var a = new PassengerAgent(0, 1, 60, false);
        var b = new PassengerAgent(0, 1, 60, false);
        for (int i = 0; i < 50; i++) { a.UpdateStandingSway(-3, 0.02f); a.RegisterHarshAcceleration(0.02f); }
        for (int i = 0; i < 100; i++) { b.UpdateStandingSway(-3, 0.01f); b.RegisterHarshAcceleration(0.01f); }
        Assert.AreEqual(a.satisfaction, b.satisfaction, 0.0001f);
        Assert.IsTrue(new PassengerAgent(0, 1, 60, true).RequiresKneelingForBoarding());
    }
    [Test]
    public void BoardingHonorsCapacityRemovesWaitingAgentsAndUnloadsAtTerminus()
    {
        var go = new GameObject("Passenger service test");
        var spawn = new GameObject("Density test");
        try
        {
            var manager = go.AddComponent<PassengerManager>();
            manager.useCapacityOverride = true; manager.maxBusCapacity = manager.doorOpenCapacityLimit = 2;
            manager.wheelchairChance = 1f;
            manager.logBoardingSummary = false;
            manager.passengerSpawner = spawn.AddComponent<PassengerSpawner>();
            manager.passengerSpawner.baselineSpawnMin = manager.passengerSpawner.baselineSpawnMax = 10;
            manager.HandleStopArrival(new BusStopData { stopName = "Departure" }, 50f, 0, 2);
            Assert.AreEqual(2, manager.currentPassengers);
            Assert.AreEqual(2, manager.OnboardPassengers.Count);
            Assert.IsFalse(manager.CanOpenDoorsForStop(0, 2));
            Assert.IsTrue(manager.CanOpenDoorsForStop(1, 2));
            foreach (var agent in manager.OnboardPassengers) Assert.IsFalse(System.Linq.Enumerable.Contains(manager.WaitingPassengers, agent));
            Assert.IsTrue(manager.hadWheelchairBoarding);
            Assert.GreaterOrEqual(manager.GetRequiredDwellTimeSeconds(), 12f);
            manager.HandleStopArrival(new BusStopData { stopName = "Terminus" }, 50f, 1, 2);
            Assert.AreEqual(0, manager.currentPassengers);
            Assert.AreEqual(2, manager.AlightingPassengers.Count);
            Assert.IsTrue(manager.HasServiceAnimations);
            manager.ResetForFreeDriveMode();
            Assert.AreEqual(0, manager.WaitingPassengers.Count);
            Assert.IsFalse(manager.HasServiceAnimations);
        }
        finally { Object.DestroyImmediate(go); Object.DestroyImmediate(spawn); }
    }

    [Test]
    public void FullBusServesAStopWhereNobodyAlightsWithoutStrandingTheRoute()
    {
        var go = new GameObject("Refused doors service test");
        var spawn = new GameObject("Density test");
        try
        {
            var manager = go.AddComponent<PassengerManager>();
            manager.useCapacityOverride = true;
            manager.maxBusCapacity = manager.doorOpenCapacityLimit = manager.seatedCapacity = 2;
            manager.wheelchairChance = 0f;
            manager.logBoardingSummary = false;
            manager.passengerSpawner = spawn.AddComponent<PassengerSpawner>();
            manager.passengerSpawner.baselineSpawnMin = manager.passengerSpawner.baselineSpawnMax = 6;

            // Full bus, everyone riding past stop 2 to stop 3.
            manager.RestoreCheckpointState(2, 1f, 0, 0f, 0f, 0f, currentStopIndex: 2, routeStopCount: 5);
            Assert.AreEqual(2, manager.currentPassengers);
            Assert.IsFalse(manager.CanOpenDoorsForStop(2, 5),
                "a full bus with nobody alighting here cannot open its doors");

            manager.HandleRefusedDoorsStopArrival(new BusStopData { stopName = "Mid" }, 2, 5);

            Assert.IsFalse(manager.doorsOpen, "the doors must stay shut at a refused stop");
            Assert.AreEqual(2, manager.currentPassengers, "nobody may board a full bus");
            Assert.AreEqual(0, manager.lastBoardingCount);
            Assert.AreEqual(0, manager.lastAlightingCount);
            Assert.AreEqual(manager.WaitingPassengers.Count, manager.waitingAtCurrentStop);
            Assert.Greater(manager.waitingAtCurrentStop, 0, "the queue left behind is still at the stop");
            Assert.LessOrEqual(manager.GetRequiredDwellTimeSeconds(), PassengerManager.RefusedDoorsDwellSeconds + 0.01f,
                "a stop nobody can board at must not charge a boarding dwell");
            Assert.IsFalse(manager.HasServiceAnimations, "nothing was served, so no animation may hold the stop open");
            Assert.IsTrue(manager.CanOpenDoorsForStop(3, 5),
                "a full bus must still let its passengers off at their own stop");
        }
        finally { Object.DestroyImmediate(go); Object.DestroyImmediate(spawn); }
    }

    [Test]
    public void DoorLockingAfterBoardingDisengagesInterlockAndClosesDoors()
    {
        var busGo = new GameObject("Bus");
        var managerGo = new GameObject("Manager");
        var spawnGo = new GameObject("Spawner");
        try
        {
            var bus = busGo.AddComponent<BusController>();
            var manager = managerGo.AddComponent<PassengerManager>();
            manager.busController = bus;
            manager.useCapacityOverride = true;
            manager.maxBusCapacity = 10;
            manager.doorOpenCapacityLimit = 10;
            manager.passengerSpawner = spawnGo.AddComponent<PassengerSpawner>();
            manager.passengerSpawner.baselineSpawnMin = manager.passengerSpawner.baselineSpawnMax = 4;

            manager.HandleStopArrival(new BusStopData { stopName = "Station" }, 25f, 0, 2);
            Assert.IsTrue(manager.doorsOpen, "Doors should be open after stop arrival");

            bus.ServiceBrakeInterlock = true;
            bus.RequestKneelingSuspension(true);
            Assert.IsTrue(bus.ServiceBrakeInterlock);
            Assert.IsTrue(bus.kneelingSuspensionActive);

            bool locked = manager.TryCloseAndLockDoors();
            Assert.IsTrue(locked, "TryCloseAndLockDoors should succeed");
            Assert.IsFalse(manager.doorsOpen, "Doors must be closed and locked");
            Assert.IsFalse(manager.HasServiceAnimations, "All pending animations must be finalized upon lock");
            Assert.IsFalse(bus.ServiceBrakeInterlock, "Service brake interlock must be disengaged");
            Assert.IsFalse(bus.kneelingSuspensionActive, "Kneeling suspension must be retracted");
        }
        finally
        {
            Object.DestroyImmediate(busGo);
            Object.DestroyImmediate(managerGo);
            Object.DestroyImmediate(spawnGo);
        }
    }

    [Test]
    public void BusDepotManager_PlacesYardZoneThatRejectsCityBuildingFootprints()
    {
        var depotGo = new GameObject("DepotZoneTest");
        var busGo = new GameObject("DepotZoneBus");
        BusDepotManager.ResetZones();
        try
        {
            var depotManager = depotGo.AddComponent<BusDepotManager>();
            var bus = busGo.AddComponent<BusController>();
            Assert.IsTrue(depotManager.TrySpawnDepotAndResolveBay(DepotTestRoute(), new Vector3(100f, 0f, 100f), bus,
                out _, out _, out _));

            Vector3 yardCentre = depotManager.currentDepot.transform.position;
            Transform yard = depotManager.currentDepot.transform;
            Assert.That(BusDepotManager.PlacedZoneCount, Is.EqualTo(1), "placing a depot must claim exactly one yard");
            Assert.IsTrue(BusDepotManager.IsInsideDepot(yardCentre), "the yard centre is inside the yard");
            Assert.IsTrue(BusDepotManager.IsInsideDepot(yardCentre + yard.forward * 8f + yard.right * 10f),
                "a point by the bay row is still inside the yard");

            // A city block sized footprint straddling the yard is rejected, and one
            // the same size a block away is not.
            Assert.IsTrue(BusDepotManager.OverlapsDepotFootprint(DepotFootprint(yardCentre, Vector3.right, 14f, 10f)),
                "a building footprint inside the yard must be rejected");
            Assert.IsFalse(BusDepotManager.OverlapsDepotFootprint(DepotFootprint(yardCentre + yard.right * 80f, Vector3.right, 14f, 10f)),
                "a building footprint outside the yard must be allowed");

            // Releasing the yard frees its ground again.
            depotManager.ReleaseCurrentDepot();
            Assert.That(BusDepotManager.PlacedZoneCount, Is.Zero, "releasing a depot must withdraw its yard");
            Assert.IsFalse(BusDepotManager.IsInsideDepot(yardCentre), "a withdrawn yard excludes nothing");
        }
        finally
        {
            Object.DestroyImmediate(depotGo);
            Object.DestroyImmediate(busGo);
            BusDepotManager.ResetZones();
        }
    }

    [Test]
    public void BusDepotManager_BuildsSeparatedEntryAndExitRoadsRegisteredAsRoad()
    {
        var depotGo = new GameObject("DepotRoadTest");
        var busGo = new GameObject("DepotRoadBus");
        var roadGo = new GameObject("DepotRoadBuilder");
        BusDepotManager.ResetZones();
        try
        {
            var roadBuilder = roadGo.AddComponent<OSMRoadMeshBuilder>();
            var depotManager = depotGo.AddComponent<BusDepotManager>();
            var bus = busGo.AddComponent<BusController>();
            Assert.IsTrue(depotManager.TrySpawnDepotAndResolveBay(DepotTestRoute(), new Vector3(100f, 0f, 100f), bus,
                out var bayPos, out _, out var exitPos));

            Transform yard = depotManager.currentDepot.transform;
            Transform entry = yard.Find("Depot_Entry_Road");
            Transform turn = yard.Find("Depot_Turn_Road");
            Transform exit = yard.Find("Depot_Exit_Road");
            string state = $"roads={depotManager.CurrentDepotRoads.Count}, builder={(OSMRoadMeshBuilder.Resolve() == null ? "missing" : "present")}, children={yard.childCount}";
            Assert.IsNotNull(entry, $"the yard must have an entry road ({state})");
            Assert.IsNotNull(exit, $"the yard must have an exit road ({state})");
            Assert.IsNotNull(turn, $"the entry and exit aisles must be joined ({state})");

            foreach (var road in new[] { entry, turn, exit })
            {
                Assert.IsNotNull(road.GetComponent<MeshCollider>(), $"{road.name} must be drivable");
                var filter = road.GetComponent<MeshFilter>();
                Assert.IsNotNull(filter, $"{road.name} must render");
                Assert.That(filter.sharedMesh.vertexCount, Is.GreaterThan(0));
            }

            var lines = depotManager.CurrentDepotRoads;
            Assert.That(lines.Count, Is.EqualTo(3), "entry, U-turn and exit are laid as three lanes");

            // The rest of the world has to see them as road: a yard aisle sits in
            // the clearance index the building infill and street furniture use.
            var index = roadBuilder.SurfaceIndex;
            Assert.IsNotNull(index, "the yard roads must be registered in a road index");
            foreach (var line in lines)
                foreach (var point in line)
                    Assert.IsTrue(index.Contains(point, 0.1f), $"yard road point {point} is not registered as road");

            // Entry and exit are separate aisles, not one wide slab, and both reach
            // the street past the yard edge so the bus can leave without reversing.
            Vector3 entryOuter = yard.InverseTransformPoint(lines[0][0]);
            Vector3 exitOuter = yard.InverseTransformPoint(lines[2][lines[2].Length - 1]);
            Vector3 kerbLocal = yard.InverseTransformPoint(depotManager.currentKerbPosition);
            Assert.That(Mathf.Abs(entryOuter.z - exitOuter.z), Is.GreaterThanOrEqualTo(depotManager.depotRoadWidth + 1f - 0.01f),
                "the entry and exit aisles must not overlap");
            Assert.That(Mathf.Abs(entryOuter.x), Is.GreaterThan(depotManager.yardWidth * 0.5f),
                "the entry driveway must reach past the yard edge to the street");
            Assert.That(Mathf.Abs(exitOuter.x), Is.GreaterThan(depotManager.yardWidth * 0.5f),
                "the exit driveway must reach past the yard edge to the street");
            Assert.That(Mathf.Abs(entryOuter.x), Is.LessThanOrEqualTo(Mathf.Abs(kerbLocal.x) + 0.01f),
                "the entry driveway must stop at the kerb, not lay depot tarmac on the carriageway");
            Assert.That(Mathf.Abs(exitOuter.x), Is.LessThanOrEqualTo(Mathf.Abs(kerbLocal.x) + 0.01f),
                "the exit driveway must stop at the kerb, not lay depot tarmac on the carriageway");

            // The bays stay behind the driving band, the player is dropped in Bay 1,
            // and the departure gate sits on the road at the end of the exit aisle.
            Vector3 bayLocal = yard.InverseTransformPoint(bayPos);
            Assert.That(bayLocal.z, Is.LessThan(entryOuter.z - depotManager.depotRoadWidth * 0.5f),
                "parking bays must sit behind the yard roads");
            Assert.That(Vector3.Distance(exitPos, lines[2][lines[2].Length - 1]), Is.LessThan(0.01f),
                "departure is measured at the exit driveway meeting the road");

            // Nothing the depot builds may stand in its own lanes.
            foreach (Transform child in yard)
                if (child.name.StartsWith("Depot_Floodlight_Pole_"))
                    Assert.IsFalse(index.Contains(child.position, 0.1f), $"{child.name} stands in a yard lane");
        }
        finally
        {
            Object.DestroyImmediate(depotGo);
            Object.DestroyImmediate(busGo);
            Object.DestroyImmediate(roadGo);
            BusDepotManager.ResetZones();
        }
    }

    [Test]
    public void BusDepotManager_EvictsCityBuildingsStandingInTheNewYard()
    {
        var depotGo = new GameObject("DepotEvictionTest");
        var busGo = new GameObject("DepotEvictionBus");
        var buildingsGo = new GameObject("DepotEvictionBuildings");
        var route = DepotTestRoute();
        BusDepotManager.ResetZones();
        try
        {
            var buildings = buildingsGo.AddComponent<OSMBuildingMeshBuilder>();
            var depotManager = depotGo.AddComponent<BusDepotManager>();
            var bus = busGo.AddComponent<BusController>();
            var startPos = new Vector3(100f, 0f, 100f);

            Assert.IsTrue(depotManager.TrySpawnDepotAndResolveBay(route, startPos, bus, out _, out _, out _));
            Vector3 yardCentre = depotManager.currentDepot.transform.position;

            // Withdraw that yard, then let the city build on its ground - which is
            // what really happens, because buildings stream in long before a route
            // picks its start.
            depotManager.ReleaseCurrentDepot();
            GameObject inYard = buildings.CreateSyntheticBuilding(DepotFootprint(yardCentre, Vector3.right, 12f, 9f), 12f, -4242L);
            GameObject elsewhere = buildings.CreateSyntheticBuilding(
                DepotFootprint(yardCentre + new Vector3(320f, 0f, 0f), Vector3.right, 12f, 9f), 12f, -4243L);
            Assert.IsNotNull(inYard, "the stand-in building must exist before the depot is placed");
            Assert.IsNotNull(elsewhere);

            Assert.IsTrue(depotManager.TrySpawnDepotAndResolveBay(route, startPos, bus, out _, out _, out _));

            Assert.That(depotManager.lastEvictedBuildingCount, Is.EqualTo(1), "exactly the building in the yard is cleared");
            Assert.IsTrue(inYard == null, "a building inside the new yard must be destroyed");
            Assert.IsTrue(elsewhere != null, "buildings outside the yard must survive");
            Assert.That(depotManager.EvictCityBuildingsFromDepot(), Is.Zero, "the yard stays clear once cleared");
        }
        finally
        {
            Object.DestroyImmediate(depotGo);
            Object.DestroyImmediate(busGo);
            Object.DestroyImmediate(buildingsGo);
            Object.DestroyImmediate(route);
            BusDepotManager.ResetZones();
        }
    }

    [Test]
    public void BusDepotManager_SpawnsDepotWithAssignedBayAndConnectsExit()
    {
        var depotGo = new GameObject("DepotManagerTest");
        var busGo = new GameObject("BusTest");
        try
        {
            var depotManager = depotGo.AddComponent<BusDepotManager>();
            var bus = busGo.AddComponent<BusController>();
            var route = ScriptableObject.CreateInstance<BusRoute>();
            route.routeName = "Route 101";
            route.stops = new BusStopData[]
            {
                new BusStopData { stopName = "Station Central", latitude = -1.286389, longitude = 36.817223 },
                new BusStopData { stopName = "Terminal", latitude = -1.290000, longitude = 36.820000 }
            };

            Vector3 startPos = new Vector3(100f, 0f, 100f);
            bool spawned = depotManager.TrySpawnDepotAndResolveBay(route, startPos, bus, out var bayPos, out var bayRot, out var exitPos);

            Assert.IsTrue(spawned);
            Assert.IsNotNull(depotManager.currentDepot);
            Assert.AreNotEqual(Vector3.zero, bayPos);
            Assert.AreNotEqual(bayPos, exitPos);
            Assert.IsTrue(depotManager.currentDepot.transform.childCount > 0);
        }
        finally
        {
            Object.DestroyImmediate(depotGo);
            Object.DestroyImmediate(busGo);
            BusDepotManager.ResetZones();
        }
    }

    [Test]
    public void BusDepotManager_RefusesPropsWhoseFootprintWouldStandInItsOwnLanes()
    {
        var depotGo = new GameObject("DepotLaneGateTest");
        var busGo = new GameObject("DepotLaneGateBus");
        var roadGo = new GameObject("DepotLaneGateRoads");
        var propGo = new GameObject("DepotLaneGateProp");
        BusDepotManager.ResetZones();
        try
        {
            var roadBuilder = roadGo.AddComponent<OSMRoadMeshBuilder>();
            var depotManager = depotGo.AddComponent<BusDepotManager>();
            var bus = busGo.AddComponent<BusController>();
            Assert.IsTrue(depotManager.TrySpawnDepotAndResolveBay(DepotTestRoute(), new Vector3(100f, 0f, 100f), bus,
                out _, out _, out _));

            Transform yard = depotManager.currentDepot.transform;
            var lanes = depotManager.CurrentDepotRoads;
            float halfWidth = depotManager.depotRoadWidth * 0.5f;
            Assert.That(lanes.Count, Is.EqualTo(3), "the yard has to have lanes to judge props against");

            // The prop the yard must refuse: a shed-sized box centred on the exit aisle.
            Vector3[] exitLane = lanes[2];
            propGo.transform.position = Vector3.Lerp(exitLane[0], exitLane[1], 0.5f);
            var shed = GameObject.CreatePrimitive(PrimitiveType.Cube);
            shed.name = "Lane blocking shed";
            shed.transform.SetParent(propGo.transform, false);
            shed.transform.localPosition = Vector3.up * 2f;
            shed.transform.localScale = new Vector3(4f, 4f, 5f);
            Assert.IsTrue(depotManager.PropStandsInYardLane(propGo), "a 4x5 m prop in the exit aisle must be refused");

            // The same prop by the bay row is kept: the rule is about the lane, not
            // about standing on the yard.
            propGo.transform.position = yard.TransformPoint(new Vector3(0f, 0f, depotManager.bayRowZ));
            Assert.IsFalse(depotManager.PropStandsInYardLane(propGo),
                "a prop between the bay row and the departure lane is clear of it");

            // And the same question asked of raw footprints: the corridor is the lane
            // plus half its width plus the clearance padding.
            Vector3 lanePoint = Vector3.Lerp(exitLane[0], exitLane[1], 0.5f);
            var straddling = DepotFootprint(lanePoint + yard.forward * 2.9f, yard.right, 4f, 2f);
            var besideLane = DepotFootprint(lanePoint - yard.forward * 8f, yard.right, 4f, 2f);
            Assert.IsTrue(BusDepotManager.FootprintOverlapsLanes(straddling, lanes, halfWidth, 0.3f),
                "a footprint overlapping the lane corridor must be refused");
            Assert.IsFalse(BusDepotManager.FootprintOverlapsLanes(besideLane, lanes, halfWidth, 0.3f),
                "a footprint clear of every corridor must be kept");
            Assert.IsFalse(BusDepotManager.FootprintOverlapsLanes(straddling, null, halfWidth),
                "with no lanes there is nothing to overlap");

            // The yard as built holds the invariant the placement rules exist for:
            // nothing it raised stands in the lanes the player is told to use.
            Assert.That(depotManager.PropsStandingInYardLanes(), Is.Empty,
                $"the depot built a prop in its own lanes: {string.Join(", ", depotManager.PropsStandingInYardLanes())}");
        }
        finally
        {
            Object.DestroyImmediate(depotGo);
            Object.DestroyImmediate(busGo);
            Object.DestroyImmediate(roadGo);
            Object.DestroyImmediate(propGo);
            BusDepotManager.ResetZones();
        }
    }

    [Test]
    public void BusDepotManager_RunsYardTrafficOnItsOwnLanesAndHoldsItForThePlayer()
    {
        var depotGo = new GameObject("DepotTrafficTest");
        var busGo = new GameObject("DepotTrafficBus");
        var roadGo = new GameObject("DepotTrafficRoads");
        BusDepotManager.ResetZones();
        try
        {
            var roadBuilder = roadGo.AddComponent<OSMRoadMeshBuilder>();
            var depotManager = depotGo.AddComponent<BusDepotManager>();
            var bus = busGo.AddComponent<BusController>();
            Assert.IsTrue(depotManager.TrySpawnDepotAndResolveBay(DepotTestRoute(), new Vector3(100f, 0f, 100f), bus,
                out _, out _, out _));

            var traffic = depotManager.currentTraffic;
            Assert.IsNotNull(traffic, "a placed depot runs yard traffic on its lanes");
            Assert.That(traffic.NodeTotal, Is.EqualTo(DepotTrafficSpawner.NodeCount), "the circuit is a closed loop");
            Assert.That(traffic.VehicleCount, Is.GreaterThan(0), "the yard must actually have traffic");
            Assert.That(traffic.VehicleCount, Is.EqualTo(traffic.vehiclesToSpawn));
            Assert.IsTrue(traffic.Graph.privateLaneCircuit,
                "a yard circuit must never be bindable as the city traffic graph");

            for (int i = 0; i < traffic.NodeTotal; i++)
                Assert.That(traffic.Graph.nodes[i].nextNodeIndices[0], Is.EqualTo((i + 1) % DepotTrafficSpawner.NodeCount),
                    $"node {i} must lead to the next node of the circuit");

            // The circuit is the depot's own geometry: the road mouths at the kerb...
            var lanes = depotManager.CurrentDepotRoads;
            Assert.That(FlatDistance(traffic.Graph.GetNodePosition(DepotTrafficSpawner.EntryMouthNode), lanes[0][0]),
                Is.LessThan(0.05f), "the circuit enters through the entry driveway");
            Assert.That(FlatDistance(traffic.Graph.GetNodePosition(DepotTrafficSpawner.ExitMouthNode),
                lanes[2][lanes[2].Length - 1]), Is.LessThan(0.05f), "and leaves through the exit driveway");
            // ...and the street either side of it, so vehicles arrive and depart.
            Vector3 kerb = depotManager.currentKerbPosition;
            Assert.That(FlatDistance(traffic.Graph.GetNodePosition(DepotTrafficSpawner.StreetApproachNode), kerb),
                Is.GreaterThan(traffic.approachDistanceMeters * 0.5f), "traffic must come from the street");
            Assert.That(FlatDistance(traffic.Graph.GetNodePosition(DepotTrafficSpawner.StreetDepartureNode), kerb),
                Is.GreaterThan(traffic.approachDistanceMeters * 0.5f), "and leave onto it");

            // Street traffic has to sit the configured distance off the kerb, not off
            // the driveway mouth (which now reaches onto the carriageway), or the
            // ambient lane would be dragged into the middle of the road.
            Vector3 yardForward = depotManager.currentDepot.transform.forward;
            Vector3 toStreet = kerb - depotManager.currentDepot.transform.position;
            toStreet.y = 0f;
            toStreet = Vector3.ProjectOnPlane(toStreet, yardForward).normalized;
            Vector3 approachNode = traffic.Graph.GetNodePosition(DepotTrafficSpawner.StreetApproachNode);
            float lateral = Vector3.Dot(approachNode - kerb, toStreet);
            Assert.That(lateral, Is.EqualTo(traffic.streetLaneOffsetMeters).Within(0.05f),
                "the street lane must be offset from the kerb, not from the driveway mouth");

            // The gate: traffic is held off the aisles while the player's bus is in
            // the yard, and a vehicle already inside is allowed to finish leaving.
            Assert.IsTrue(DepotTrafficSpawner.ShouldHoldTraffic(true, false), "street traffic must hold for a bus in the yard");
            Assert.IsFalse(DepotTrafficSpawner.ShouldHoldTraffic(true, true), "a vehicle in the yard must be allowed to leave");
            Assert.IsFalse(DepotTrafficSpawner.ShouldHoldTraffic(false, false), "traffic flows once the bus is out");
            Assert.IsTrue(traffic.IsInsideYard(depotManager.currentDepot.transform.position),
                "the yard centre is inside the yard its traffic serves");

            int vehicles = 0;
            foreach (Transform child in traffic.transform)
            {
                var spline = child.GetComponent<SplineVehicle>();
                if (spline == null) continue;   // circuit node markers are children too
                vehicles++;
                Assert.AreSame(traffic.Graph, spline.roadGraph, $"{child.name} must drive the yard circuit");
                Assert.IsNull(child.GetComponent<AIVehicleController>(), $"{child.name} must not drive itself");
                Assert.IsNull(child.GetComponent<BusController>(), $"{child.name} must not be a second player bus");
                var body = child.GetComponent<Rigidbody>();
                if (body != null)
                    Assert.IsTrue(body.isKinematic, $"{child.name} must not fall or fight the spline");
            }
            Assert.That(vehicles, Is.EqualTo(traffic.VehicleCount), "every yard vehicle is a spline vehicle");
        }
        finally
        {
            Object.DestroyImmediate(depotGo);
            Object.DestroyImmediate(busGo);
            Object.DestroyImmediate(roadGo);
            BusDepotManager.ResetZones();
        }
    }

    [Test]
    public void BusController_ThrottleReleasesTheParkingBrakeSoADepotBusCanDriveOut()
    {
        var busGo = new GameObject("DepotDepartureBus");
        try
        {
            var bus = busGo.AddComponent<BusController>();
            bus.SetParkingBrake(true);
            Assert.IsTrue(bus.ParkingBrakeActive, "a bus starts its route parked in its bay");

            Assert.IsTrue(bus.ShouldReleaseParkingBrake(1f, 0f, 0f),
                "throttle from a standstill must release the park brake, or the bus cannot leave the bay");
            Assert.IsFalse(bus.ShouldReleaseParkingBrake(0f, 0f, 0f), "an untouched bus stays parked");
            Assert.IsFalse(bus.ShouldReleaseParkingBrake(1f, 1f, 0f), "the foot brake must not release it");
            Assert.IsFalse(bus.ShouldReleaseParkingBrake(1f, 0f, 12f), "a rolling bus keeps its brake setting");

            bus.ServiceBrakeInterlock = true;
            Assert.IsFalse(bus.ShouldReleaseParkingBrake(1f, 0f, 0f), "the service interlock owns a stopped bus");
            bus.ServiceBrakeInterlock = false;
            bus.FuelDepleted = true;
            Assert.IsFalse(bus.ShouldReleaseParkingBrake(1f, 0f, 0f), "an empty bus is not going anywhere");
            bus.FuelDepleted = false;

            bus.releaseParkingBrakeOnThrottle = false;
            Assert.IsFalse(bus.ShouldReleaseParkingBrake(1f, 0f, 0f), "the release rule can be turned off");
            bus.releaseParkingBrakeOnThrottle = true;
            bus.SetParkingBrake(false);
            Assert.IsFalse(bus.ShouldReleaseParkingBrake(1f, 0f, 0f), "a released brake has nothing to release");
        }
        finally { Object.DestroyImmediate(busGo); }
    }

    [Test]
    public void BusDepotManager_OpensTheStreetKerbWhereTheDrivewaysMeetIt()
    {
        var depotGo = new GameObject("DepotKerbTest");
        var busGo = new GameObject("DepotKerbBus");
        var roadGo = new GameObject("DepotKerbBuilder");
        GameObject kerbRoot = null;
        BusDepotManager.ResetZones();
        try
        {
            roadGo.AddComponent<OSMRoadMeshBuilder>();
            var depotManager = depotGo.AddComponent<BusDepotManager>();
            var bus = busGo.AddComponent<BusController>();

            Assert.IsTrue(depotManager.TrySpawnDepotAndResolveBay(DepotTestRoute(), new Vector3(100f, 0f, 100f), bus,
                out _, out _, out _));

            Vector3 entryMouth = depotManager.CurrentDepotRoads[0][0];
            Vector3 kerb = depotManager.currentKerbPosition;
            Quaternion facing = depotManager.currentDepot.transform.rotation;

            // The city raises these 0.7 m boxes along every street edge before a route
            // exists, so fake one long collider lying across the entry mouth.
            kerbRoot = new GameObject("PhysicalRoadBoundaries");
            var edge = new GameObject("Edge_1_1");
            edge.transform.SetParent(kerbRoot.transform, false);
            edge.transform.SetPositionAndRotation(new Vector3(kerb.x, kerb.y + 0.35f, entryMouth.z), facing);
            var box = edge.AddComponent<BoxCollider>();
            box.size = new Vector3(0.38f, 0.7f, 30f);

            // Re-place the yard: this run has to cut the kerb it now knows about.
            Assert.IsTrue(depotManager.TrySpawnDepotAndResolveBay(DepotTestRoute(), new Vector3(100f, 0f, 100f), bus,
                out _, out _, out _));

            Assert.IsTrue(edge == null,
                "the street kerb collider across the driveway must be removed so the bus can leave");
        }
        finally
        {
            BusDepotManager.ResetZones();
            if (kerbRoot != null) Object.DestroyImmediate(kerbRoot);
            Object.DestroyImmediate(depotGo);
            Object.DestroyImmediate(busGo);
            Object.DestroyImmediate(roadGo);
        }
    }

    [Test]
    public void BusDepotManager_AnchorsTheYardBehindTheFirstStop()
    {
        Vector3 forward = Vector3.forward;
        var stop = new Vector3(40f, 0f, 120f);
        const float setback = 7.5f;   // kerb anchor to the exit driveway mouth
        const float behind = 26f;     // driveway mouth to the first stop

        // A yard already a comfortable way behind the stop keeps its ground.
        Vector3 behindKerb = stop - forward * (behind + setback + 6f);
        Assert.That(BusDepotManager.AnchorExitBehindFirstStop(behindKerb, forward, stop, setback, behind),
            Is.EqualTo(behindKerb), "a yard already behind the stop must not be moved");

        // A yard level with the stop is slid back until its exit driveway opens
        // on the street before the stop: the bus then leaves behind the stop.
        Vector3 level = BusDepotManager.AnchorExitBehindFirstStop(stop, forward, stop, setback, behind);
        Assert.That(Vector3.Dot(level - stop, forward) - setback, Is.EqualTo(-behind).Within(0.01f),
            "the exit driveway must open behind the first stop, not alongside it");

        // A yard placed ahead of the stop - the case where the bus had to reverse
        // back up the street - is pulled behind it instead.
        Vector3 ahead = BusDepotManager.AnchorExitBehindFirstStop(stop + forward * 40f, forward, stop, setback, behind);
        Assert.That(Vector3.Dot(ahead - stop, forward), Is.LessThan(0f),
            "the whole yard must end up behind the first stop");
        Assert.That(Vector3.Dot(ahead - stop, forward) - setback, Is.EqualTo(-behind).Within(0.01f),
            "the exit driveway must open a set distance before the stop");
    }

    static float FlatDistance(Vector3 a, Vector3 b)
    {
        a.y = 0f;
        b.y = 0f;
        return Vector3.Distance(a, b);
    }

    static BusRoute DepotTestRoute()
    {
        var route = ScriptableObject.CreateInstance<BusRoute>();
        route.routeName = "Route 101";
        route.stops = new BusStopData[]
        {
            new BusStopData { stopName = "Station Central", latitude = -1.286389, longitude = 36.817223 },
            new BusStopData { stopName = "Terminal", latitude = -1.290000, longitude = 36.820000 }
        };
        return route;
    }

    /// <summary>Axis-aligned building rectangle centred on <paramref name="centre"/>.</summary>
    static System.Collections.Generic.List<Vector3> DepotFootprint(Vector3 centre, Vector3 right, float width, float depth)
    {
        Vector3 forward = Vector3.Cross(Vector3.up, right).normalized;
        return new System.Collections.Generic.List<Vector3>
        {
            centre - right * (width * .5f) - forward * (depth * .5f),
            centre + right * (width * .5f) - forward * (depth * .5f),
            centre + right * (width * .5f) + forward * (depth * .5f),
            centre - right * (width * .5f) + forward * (depth * .5f)
        };
    }
}
