using UnityEngine;

/// <summary>
/// How many AI buses the city should contain, where they may appear, and how a
/// bus has to drive.
///
/// A bus is not a slow car. It accelerates gently, brakes early, turns wide and
/// needs a much longer following gap; giving it the car defaults made it tailgate,
/// dive into junctions and change lanes like a hatchback. The same numbers are
/// applied to the pooled mixed traffic and to the scheduled ambient buses, so a
/// bus behaves the same whichever spawner produced it.
///
/// The rules live here rather than in the controllers so they are testable and so
/// the two spawners cannot drift apart.
/// </summary>
public static class TrafficBusPolicy
{
    /// <summary>Share of the mixed-traffic pool that may be buses.</summary>
    public const float DefaultBusShare = 0.12f;

    /// <summary>Hard ceiling for buses alive at once, across both spawners.</summary>
    public const int DefaultMaxActiveBuses = 12;
    public const int MaxActiveBusesCeiling = 24;

    /// <summary>Probability that a scheduled route spawns a bus when its headway is due.</summary>
    public const float DefaultSpawnProbability = 0.9f;

    public const float MinSpawnDistanceMeters = 90f;
    public const float MaxSpawnDistanceMeters = 420f;

    // Full-physics tier: a 12 m bus with passengers.
    public const float BusMaxAccel = 3.0f;
    public const float BusMaxBrake = 6.5f;
    public const float BusTurnRate = 2.8f;
    public const float BusStopDistance = 6f;
    public const float BusLaneChangeMetersPerSecond = 2.2f;
    public const float BusOvertakeSpeedMarginKmh = 8f;
    public const float BusMinimumOvertakeSpeedKmh = 18f;
    public const float BusOvertakeScanDistanceMeters = 55f;
    public const float BusMaximumOvertakeSeconds = 12f;
    public const float BusOvertakeCooldownSeconds = 6f;
    public const float BusLaneChangeSettleSeconds = 3f;
    public const float BusLaneChangeRetrySeconds = 5f;

    // Distant/spline tier.
    public const float BusSplineAccelKmhPerSec = 8f;
    public const float BusSplineBrakeKmhPerSec = 14f;
    public const float BusSplineTurnRate = 4.5f;
    public const float BusSplineMinHeadwayMeters = 16f;
    public const float BusRedLightStopDistance = 22f;
    public const float BusSplineLaneChangeMetersPerSecond = 2.4f;

    public static int ResolveMaxActiveBuses(int authored)
        => Mathf.Clamp(authored <= 0 ? DefaultMaxActiveBuses : authored, 0, MaxActiveBusesCeiling);

    /// <summary>
    /// Size of the scheduled-bus pool: the sum of the per-route caps, but never
    /// more than the global ceiling. Per-route limits alone could add up to dozens
    /// of instantiated buses on a multi-route city.
    /// </summary>
    public static int ScheduledPoolCapacity(int sumOfPerRouteCaps, int maxActiveBuses)
        => Mathf.Clamp(sumOfPerRouteCaps, 0, ResolveMaxActiveBuses(maxActiveBuses));

    /// <summary>
    /// A negative authored share means "not configured" and gets the default; zero
    /// is a real request for no buses, so it stays zero.
    /// </summary>
    public static float ResolveBusShare(float authored)
        => Mathf.Clamp01(authored < 0f ? DefaultBusShare : authored);

    /// <summary>
    /// How many bus prefabs the mixed pool should contain. The pool is created
    /// once, so this is both the visible share and the hard cap: a bus cannot be
    /// activated if it was never instantiated.
    /// </summary>
    public static int PoolBusCount(int poolSize, float share, int maxActiveBuses)
    {
        int size = Mathf.Max(0, poolSize);
        int cap = ResolveMaxActiveBuses(maxActiveBuses);
        int target = Mathf.RoundToInt(size * ResolveBusShare(share));
        return Mathf.Clamp(target, 0, Mathf.Min(cap, size));
    }

    /// <summary>
    /// Probability for one scheduled spawn attempt. Zero at the cap, otherwise the
    /// authored chance when the fleet is empty, easing down as it fills so routes
    /// do not all fire on the same frame.
    /// </summary>
    public static float SpawnProbability(float authored, int activeBuses, int maxActiveBuses)
    {
        int cap = ResolveMaxActiveBuses(maxActiveBuses);
        if (cap <= 0 || activeBuses >= cap) return 0f;
        float occupancy = Mathf.Clamp01(activeBuses / (float)cap);
        float chance = authored < 0f ? DefaultSpawnProbability : authored;
        return Mathf.Clamp01(chance) * Mathf.Lerp(1f, 0.55f, occupancy);
    }

    /// <summary>True when a spawn point is inside the configured band around the player.</summary>
    public static bool WithinSpawnBand(float distanceMeters, float minMeters, float maxMeters)
    {
        float min = Mathf.Max(0f, minMeters);
        float max = Mathf.Max(min, maxMeters);
        return distanceMeters >= min && distanceMeters <= max;
    }

    /// <summary>
    /// Applies the bus's motion envelope to the full-physics controller. The
    /// measured length and width are kept: a minibus and a coach must not share
    /// the same bumper gap.
    /// </summary>
    public static void ApplyHandling(AIVehicleController ai)
    {
        if (ai == null) return;

        ai.maxAccel = BusMaxAccel;
        ai.maxBrake = BusMaxBrake;
        ai.turnRate = BusTurnRate;
        ai.stopDistance = BusStopDistance;
        ai.safeFollowingDistance = Mathf.Max(12f, ai.vehicleLength * 1.1f);
        ai.laneChangeMetersPerSecond = BusLaneChangeMetersPerSecond;
        ai.overtakeSpeedMarginKmh = BusOvertakeSpeedMarginKmh;
        ai.minimumOvertakeSpeedKmh = BusMinimumOvertakeSpeedKmh;
        ai.overtakeScanDistanceMeters = BusOvertakeScanDistanceMeters;
        ai.maximumOvertakeSeconds = BusMaximumOvertakeSeconds;
        ai.overtakeCooldownSeconds = BusOvertakeCooldownSeconds;
        ai.laneChangeSettleSeconds = BusLaneChangeSettleSeconds;
        ai.laneChangeRetrySeconds = BusLaneChangeRetrySeconds;
    }

    /// <summary>
    /// Applies the same envelope to the distant spline tier. Its speed is in km/h
    /// per second rather than m/s², so the acceleration and braking numbers are
    /// converted rather than copied.
    /// </summary>
    public static void ApplyHandling(SplineVehicle spline)
    {
        if (spline == null) return;

        spline.accelKmhPerSec = BusSplineAccelKmhPerSec;
        spline.brakeKmhPerSec = BusSplineBrakeKmhPerSec;
        spline.turnRate = BusSplineTurnRate;
        spline.minHeadway = BusSplineMinHeadwayMeters;
        spline.redLightStopDistance = BusRedLightStopDistance;
        spline.laneChangeMetersPerSecond = BusSplineLaneChangeMetersPerSecond;
        spline.overtakeSpeedMarginKmh = BusOvertakeSpeedMarginKmh;
        spline.minimumOvertakeSpeedKmh = BusMinimumOvertakeSpeedKmh;
        spline.overtakeScanDistanceMeters = BusOvertakeScanDistanceMeters;
        spline.maximumOvertakeSeconds = BusMaximumOvertakeSeconds;
        spline.overtakeCooldownSeconds = BusOvertakeCooldownSeconds;
        // The spline tier has no vehicle-type speed factor, so the bus's slower
        // cruise has to be written here.
        spline.desiredSpeedFactor = Mathf.Min(spline.desiredSpeedFactor, 0.82f);
    }

    public static void Apply(AIVehicleController ai, SplineVehicle spline)
    {
        ApplyHandling(ai);
        ApplyHandling(spline);
    }
}
