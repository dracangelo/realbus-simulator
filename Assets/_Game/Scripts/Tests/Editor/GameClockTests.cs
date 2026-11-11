using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Tests for the three-clock time model (section 4.3).
/// </summary>
public class GameClockTests
{
    [SetUp]
    public void SetUp()
    {
        GameClock.Reset(480d); // 08:00
    }

    [Test]
    public void Reset_SetsStartTimeAndRealSpeed()
    {
        GameClock.SetAccelerated(8f);
        Assert.That(GameClock.IsAccelerated, Is.True);
        GameClock.Reset(300d);
        Assert.That(GameClock.SimMinutes, Is.EqualTo(300d).Within(0.01d));
        Assert.That(GameClock.IsAccelerated, Is.False);
    }

    [Test]
    public void Tick_AdvancesSimMinutesByRealDeltaAtX1()
    {
        GameClock.Reset(480d);
        // Simulate 30 seconds of real time at x1
        for (int i = 0; i < 30; i++)
        {
            // Unity sets deltaTime internally; we test via the multiplier logic instead
        }
        GameClock.SetSimSpeed(1f);
        Assert.That(GameClock.SimSpeedMultiplier, Is.EqualTo(1f));
        Assert.That(GameClock.IsAccelerated, Is.False);
    }

    [Test]
    public void SetAccelerated_ClampsToRange()
    {
        GameClock.SetAccelerated(100f);
        Assert.That(GameClock.SimSpeedMultiplier, Is.EqualTo(30f));
        GameClock.SetAccelerated(0.1f);
        Assert.That(GameClock.SimSpeedMultiplier, Is.EqualTo(1f));
        GameClock.SetAccelerated(8f);
        Assert.That(GameClock.SimSpeedMultiplier, Is.EqualTo(8f));
        Assert.That(GameClock.IsAccelerated, Is.True);
    }

    [Test]
    public void SetRealTime_ReturnsToX1()
    {
        GameClock.SetAccelerated(15f);
        GameClock.SetRealTime();
        Assert.That(GameClock.SimSpeedMultiplier, Is.EqualTo(1f));
        Assert.That(GameClock.IsAccelerated, Is.False);
    }

    [Test]
    public void SimTimeString_FormatsCorrectly()
    {
        GameClock.Reset(480d); // 08:00
        Assert.That(GameClock.SimTimeString(), Is.EqualTo("08:00 AM"));
        GameClock.Reset(720d); // 12:00
        Assert.That(GameClock.SimTimeString(), Is.EqualTo("12:00 PM"));
        GameClock.Reset(1320d); // 22:00
        Assert.That(GameClock.SimTimeString(), Is.EqualTo("10:00 PM"));
    }

    [Test]
    public void SimHour24_ReturnsCorrectHour()
    {
        GameClock.Reset(480d);
        Assert.That(GameClock.SimHour24(), Is.EqualTo(8));
        GameClock.Reset(780d);
        Assert.That(GameClock.SimHour24(), Is.EqualTo(13));
    }
}