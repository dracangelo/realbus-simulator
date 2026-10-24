using NUnit.Framework;
using UnityEngine;

public class GameplayRulesTests
{
    [TestCase(60f, 0f)]
    [TestCase(89f, 0f)]
    [TestCase(90f, 2f)]
    [TestCase(180f, 8f)]
    [TestCase(-120f, 0f)]
    public void LatePenaltyHonorsGraceAndThirtySecondIntervals(float seconds, float expected)
    { Assert.AreEqual(expected, GameplayRules.LatePenaltyPercent(seconds)); }

    [Test]
    public void ScoreAndRewardsUseTimelineWeights()
    {
        Assert.AreEqual(100f, GameplayRules.WeightedScore(100, 100, 100, 100), 0.001f);
        Assert.AreEqual(30f, GameplayRules.WeightedScore(100, 0, 0, 0), 0.001f);
        Assert.AreEqual(200, GameplayRules.BaseXP(1));
        Assert.AreEqual(1000, GameplayRules.BaseXP(5));
        Assert.AreEqual(750, GameplayRules.EarnedXP(1000, 50, 1.5f));
    }

    [Test]
    public void DockingRequiresAllTolerances()
    {
        Assert.IsTrue(GameplayRules.IsDocked(0.8f, 4f, 25f, 1.5f));
        Assert.IsTrue(GameplayRules.IsDocked(0.5f, 3f, 25f, 1.5f));
        Assert.IsFalse(GameplayRules.IsDocked(0.801f, 0, 0, 0));
        Assert.IsFalse(GameplayRules.IsDocked(0, 4.01f, 0, 0));
        Assert.IsFalse(GameplayRules.IsDocked(0, 0, 26, 0));
        Assert.IsFalse(GameplayRules.IsDocked(0, 0, 0, 2));
    }

    [Test]
    public void DockingZoneGateAllowsARealKerbGapButStillRequiresTheKerb()
    {
        // GameplayRules.IsDocked keeps the strict defaults above as the "perfect
        // stop" reference. A runtime stop passes its own wider gate: the anchor sits
        // only ~0.6 m inside the asphalt edge, so a 0.8 m envelope demanded a door
        // pressed onto the kerb and a correct stop could miss by centimetres.
        var go = new GameObject("DockingZoneTest");
        try
        {
            var zone = go.AddComponent<DockingZone>();
            Assert.That(zone.kerbToleranceMeters, Is.GreaterThanOrEqualTo(1.8f));
            Assert.That(zone.stopSignToleranceMeters, Is.GreaterThanOrEqualTo(6f));
            Assert.That(zone.headingToleranceDegrees, Is.GreaterThanOrEqualTo(35f));
            Assert.That(zone.maxDockingSpeedKmh, Is.GreaterThanOrEqualTo(3f));

            Assert.IsTrue(GameplayRules.IsDocked(1.5f, 6f, 35f, 2.5f,
                zone.kerbToleranceMeters, zone.stopSignToleranceMeters,
                zone.headingToleranceDegrees, zone.maxDockingSpeedKmh));
            // Still not docked from the middle of a wide road.
            Assert.IsFalse(GameplayRules.IsDocked(4f, 6f, 35f, 2.5f,
                zone.kerbToleranceMeters, zone.stopSignToleranceMeters,
                zone.headingToleranceDegrees, zone.maxDockingSpeedKmh));
        }
        finally
        {
            Object.DestroyImmediate(go);
        }
    }

    [Test]
    public void SignalCrossingDetectsHighAndLowSpeedsButNotReverseOrSideways()
    {
        Assert.IsTrue(GameplayRules.CrossedStopLine(0, -20, 0, 20, 8));
        Assert.IsTrue(GameplayRules.CrossedStopLine(0, -0.01f, 0, 0.01f, 8));
        Assert.IsFalse(GameplayRules.CrossedStopLine(0, 1, 0, -1, 8));
        Assert.IsFalse(GameplayRules.CrossedStopLine(10, -1, 10, 1, 8));
        Assert.IsFalse(GameplayRules.CrossedStopLine(0, -1, 0, -1, 8));
    }

    [Test]
    public void SignalCycleRetainsOvershootAndHandlesZeroDurations()
    {
        var go = new GameObject("Signal test");
        try
        {
            var light = go.AddComponent<TrafficLight>();
            light.AdvanceTime(46f);
            Assert.AreEqual(TrafficLight.LightState.Green, light.currentState);
            Assert.AreEqual(1f, light.timeInState, 0.001f);
            light.AdvanceTime(40f);
            Assert.AreEqual(TrafficLight.LightState.Red, light.currentState);
            Assert.AreEqual(1f, light.timeInState, 0.001f);
            light.redDuration = light.greenDuration = light.amberDuration = 0f;
            Assert.DoesNotThrow(() => light.AdvanceTime(1000f));
        }
        finally { Object.DestroyImmediate(go); }
    }

    [Test]
    public void ScoresApplyPenaltiesOutsideMissionsAndResetOnBeginMission()
    {
        var go = new GameObject("Score test");
        try
        {
            var score = go.AddComponent<ScoreTracker>();

            // Penalties apply outside a mission (e.g. free drive).
            score.RecordCollision();
            Assert.AreEqual(1, score.CollisionCount);
            Assert.AreEqual(85f, score.safetyScore, 0.001f);
            Assert.AreEqual(96.25f, score.totalScore, 0.001f);

            // BeginMission resets the running totals.
            score.BeginMission(null);
            Assert.AreEqual(0, score.CollisionCount);
            Assert.AreEqual(100f, score.totalScore, 0.001f);

            score.RecordRedLight();
            Assert.AreEqual(500, score.pointDeductions);
            Assert.AreEqual(90f, score.safetyScore, 0.001f);
            Assert.AreEqual(97.5f, score.totalScore, 0.001f);

            for (int i = 0; i < 5; i++) score.RecordRedLight();
            Assert.AreEqual(40f, score.safetyScore, 0.001f);
            Assert.AreEqual(3000, score.pointDeductions);
            Assert.AreEqual(85f, score.totalScore, 0.001f);

            score.BeginMission(null);
            Assert.AreEqual(0, score.pointDeductions);
            Assert.AreEqual(100f, score.totalScore, 0.001f);

            // Still applies after EndMission.
            score.EndMission();
            score.RecordCollision();
            Assert.AreEqual(1, score.CollisionCount);
        }
        finally { Object.DestroyImmediate(go); }
    }
}
