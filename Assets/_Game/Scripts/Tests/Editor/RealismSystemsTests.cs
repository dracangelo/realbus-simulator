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
}
