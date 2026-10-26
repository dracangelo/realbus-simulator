using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Pins the bus rules: how many buses may exist, how often a scheduled one may
/// appear, where it may appear, and the slower envelope it has to drive with.
/// These are the values that stop "add buses" from turning the city into a
/// bus-only traffic jam with cars tailgating twelve-metre bodies.
/// </summary>
public class TrafficBusPolicyTests
{
    // ── Fleet size ────────────────────────────────────────────────────────

    [Test]
    public void MaxActiveBuses_DefaultsWhenUnsetAndCapsRunawayValues()
    {
        Assert.AreEqual(TrafficBusPolicy.DefaultMaxActiveBuses, TrafficBusPolicy.ResolveMaxActiveBuses(0));
        Assert.AreEqual(TrafficBusPolicy.DefaultMaxActiveBuses, TrafficBusPolicy.ResolveMaxActiveBuses(-8));
        Assert.AreEqual(5, TrafficBusPolicy.ResolveMaxActiveBuses(5));
        Assert.AreEqual(TrafficBusPolicy.MaxActiveBusesCeiling, TrafficBusPolicy.ResolveMaxActiveBuses(500),
            "a typo in the inspector must not spawn a hundred buses");
    }

    [Test]
    public void PoolBusCount_IsTheShareOfThePoolButNeverAboveTheCeiling()
    {
        Assert.AreEqual(4, TrafficBusPolicy.PoolBusCount(30, 0.12f, 12),
            "30 pooled vehicles at 12% is roughly four buses");
        Assert.AreEqual(5, TrafficBusPolicy.PoolBusCount(30, 0.5f, 5),
            "the ceiling wins over the share");
        Assert.AreEqual(0, TrafficBusPolicy.PoolBusCount(0, 0.12f, 12));
        Assert.AreEqual(0, TrafficBusPolicy.PoolBusCount(30, 0f, 12),
            "a share of zero is a real request for no buses");
        Assert.AreEqual(4, TrafficBusPolicy.PoolBusCount(30, -1f, 12),
            "a negative share is an unset field and gets the default");
    }

    [Test]
    public void ScheduledPoolCapacity_IsTheSumOfRoutesCappedOnce()
    {
        Assert.AreEqual(6, TrafficBusPolicy.ScheduledPoolCapacity(6, 12));
        Assert.AreEqual(12, TrafficBusPolicy.ScheduledPoolCapacity(40, 12),
            "a multi-route city must not instantiate forty buses because each route asked for some");
        Assert.AreEqual(3, TrafficBusPolicy.ScheduledPoolCapacity(40, 3));
        Assert.AreEqual(0, TrafficBusPolicy.ScheduledPoolCapacity(0, 12));
    }

    // ── Spawn chance and band ─────────────────────────────────────────────

    [Test]
    public void SpawnProbability_IsZeroAtTheCeilingAndFullWhenEmpty()
    {
        Assert.AreEqual(0.9f, TrafficBusPolicy.SpawnProbability(0.9f, 0, 12), 0.0001f);
        Assert.AreEqual(0f, TrafficBusPolicy.SpawnProbability(0.9f, 12, 12), 0.0001f);
        Assert.AreEqual(0f, TrafficBusPolicy.SpawnProbability(0.9f, 20, 12), 0.0001f,
            "an over-budget fleet must never be made larger");
        Assert.AreEqual(0f, TrafficBusPolicy.SpawnProbability(0f, 0, 12), 0.0001f,
            "zero probability disables scheduled service");
    }

    [Test]
    public void SpawnProbability_EasesOffAsTheFleetFills()
    {
        float empty = TrafficBusPolicy.SpawnProbability(0.9f, 0, 12);
        float half = TrafficBusPolicy.SpawnProbability(0.9f, 6, 12);
        float almostFull = TrafficBusPolicy.SpawnProbability(0.9f, 11, 12);

        Assert.Less(half, empty);
        Assert.Less(almostFull, half);
        Assert.Greater(almostFull, 0f, "the last slot is still allowed to be used");
    }

    [Test]
    public void SpawnBand_IncludesBothEdgesAndRejectsOutside()
    {
        Assert.IsFalse(TrafficBusPolicy.WithinSpawnBand(89.9f, 90f, 420f));
        Assert.IsTrue(TrafficBusPolicy.WithinSpawnBand(90f, 90f, 420f));
        Assert.IsTrue(TrafficBusPolicy.WithinSpawnBand(420f, 90f, 420f));
        Assert.IsFalse(TrafficBusPolicy.WithinSpawnBand(420.1f, 90f, 420f));
    }

    [Test]
    public void SpawnBand_ToleratesAReversedOrDegenerateRange()
    {
        Assert.IsFalse(TrafficBusPolicy.WithinSpawnBand(50f, 90f, 40f),
            "a reversed range collapses to its minimum; it does not silently accept everything");
        Assert.IsTrue(TrafficBusPolicy.WithinSpawnBand(90f, 90f, 40f));
        Assert.IsTrue(TrafficBusPolicy.WithinSpawnBand(0f, 0f, 0f));
        Assert.IsFalse(TrafficBusPolicy.WithinSpawnBand(1f, 0f, 0f));
    }

    // ── Handling ──────────────────────────────────────────────────────────

    [Test]
    public void Handling_MakesABusSlowerAndLongerGappedThanACar()
    {
        var go = new GameObject("BusPolicyTest");
        try
        {
            var ai = go.AddComponent<AIVehicleController>();
            var spline = go.AddComponent<SplineVehicle>();
            ai.vehicleLength = 12f;

            // Car defaults, for the comparison.
            float carAccel = ai.maxAccel;
            float carBrake = ai.maxBrake;
            float carTurn = ai.turnRate;

            TrafficBusPolicy.Apply(ai, spline);

            Assert.Less(ai.maxAccel, carAccel, "a bus cannot launch like a hatchback");
            Assert.Less(ai.maxBrake, carBrake, "and it cannot stop like one either");
            Assert.Less(ai.turnRate, carTurn, "a long wheelbase turns wider");
            Assert.GreaterOrEqual(ai.safeFollowingDistance, 12f,
                "the gap must fit a twelve-metre body");
            Assert.AreEqual(Mathf.Max(12f, ai.vehicleLength * 1.1f), ai.safeFollowingDistance, 0.0001f);

            Assert.Less(spline.accelKmhPerSec, 16f);
            Assert.Less(spline.brakeKmhPerSec, 25f);
            Assert.GreaterOrEqual(spline.minHeadway, 16f);
            Assert.LessOrEqual(spline.desiredSpeedFactor, 0.82f,
                "the distant tier has no vehicle-type factor, so its cruise is capped here");
        }
        finally
        {
            Object.DestroyImmediate(go);
        }
    }

    [Test]
    public void Handling_KeepsTheMeasuredLengthSoAMinibusAndACoachDiffer()
    {
        var minibus = new GameObject("MinibusPolicyTest");
        var coach = new GameObject("CoachPolicyTest");
        try
        {
            var minibusAi = minibus.AddComponent<AIVehicleController>();
            minibusAi.vehicleLength = 7f;
            TrafficBusPolicy.Apply(minibusAi, minibus.AddComponent<SplineVehicle>());

            var coachAi = coach.AddComponent<AIVehicleController>();
            coachAi.vehicleLength = 15f;
            TrafficBusPolicy.Apply(coachAi, coach.AddComponent<SplineVehicle>());

            Assert.Less(minibusAi.safeFollowingDistance, coachAi.safeFollowingDistance,
                "a coach needs more room than a minibus");
            Assert.AreEqual(12f, minibusAi.safeFollowingDistance, 0.0001f);
            Assert.AreEqual(15f * 1.1f, coachAi.safeFollowingDistance, 0.001f);
        }
        finally
        {
            Object.DestroyImmediate(minibus);
            Object.DestroyImmediate(coach);
        }
    }

    [Test]
    public void Handling_IgnoresMissingComponents()
    {
        Assert.DoesNotThrow(() => TrafficBusPolicy.Apply(null, null));
        Assert.DoesNotThrow(() => TrafficBusPolicy.ApplyHandling((AIVehicleController)null));
        Assert.DoesNotThrow(() => TrafficBusPolicy.ApplyHandling((SplineVehicle)null));
    }
}
