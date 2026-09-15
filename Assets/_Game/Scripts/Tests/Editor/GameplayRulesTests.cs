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
        Assert.IsTrue(GameplayRules.IsDocked(0.5f, 3f, 25f, 1.5f));
        Assert.IsFalse(GameplayRules.IsDocked(0.501f, 0, 0, 0));
        Assert.IsFalse(GameplayRules.IsDocked(0, 3.01f, 0, 0));
        Assert.IsFalse(GameplayRules.IsDocked(0, 0, 26, 0));
        Assert.IsFalse(GameplayRules.IsDocked(0, 0, 0, 2));
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
    public void ScoresResetAndIgnoreCollisionsOutsideMissions()
    {
        var go = new GameObject("Score test");
        try
        {
            var score = go.AddComponent<ScoreTracker>();
            score.RecordCollision(); Assert.AreEqual(0, score.CollisionCount);
            score.BeginMission(null); score.RecordRedLight();
            Assert.AreEqual(500, score.pointDeductions);
            Assert.AreEqual(95f, score.totalScore, 0.001f);
            for (int i = 0; i < 5; i++) score.RecordRedLight();
            Assert.AreEqual(70f, score.totalScore, 0.001f);
            Assert.AreEqual(3000, score.pointDeductions);
            score.BeginMission(null);
            Assert.AreEqual(0, score.pointDeductions);
            Assert.AreEqual(100f, score.totalScore);
            score.EndMission(); score.RecordCollision();
            Assert.AreEqual(0, score.CollisionCount);
        }
        finally { Object.DestroyImmediate(go); }
    }
}
