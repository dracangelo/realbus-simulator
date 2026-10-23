using System;

/// <summary>Deterministic realism rules shared by simulation and acceptance tests.</summary>
public static class RealismRules
{
    public static float DailyDensity(float minutes, float morning = 450f, float evening = 1050f, float width = 90f, float peak = 5f)
    {
        minutes = (minutes % 1440f + 1440f) % 1440f;
        double a = CircularDistance(minutes, morning) / Math.Max(1f, width);
        double b = CircularDistance(minutes, evening) / Math.Max(1f, width);
        return 1f + (Math.Max(1f, peak) - 1f) * (float)Math.Max(Math.Exp(-0.5 * a * a), Math.Exp(-0.5 * b * b));
    }

    /// <summary>
    /// Normalised road-traffic volume for the time of day, in the range 0..1.
    /// <see cref="DailyDensity"/> only scales the two rush-hour humps, so every
    /// hour outside them used the same baseline. That meant 03:00 and 13:00 had
    /// the same number of cars. This profile adds the missing day/night shape:
    /// a quiet night, a broad daytime flow, and two commuter peaks. VehiclePool
    /// maps it between its minimum and maximum active fractions.
    /// </summary>
    public static float DailyTrafficVolume(float minutes, float morning = 450f,
        float evening = 1050f, float width = 80f)
    {
        minutes = (minutes % 1440f + 1440f) % 1440f;
        double morningDistance = CircularDistance(minutes, morning) / Math.Max(1f, width);
        double eveningDistance = CircularDistance(minutes, evening) / Math.Max(1f, width);
        float rush = (float)Math.Max(Math.Exp(-0.5 * morningDistance * morningDistance),
            Math.Exp(-0.5 * eveningDistance * eveningDistance));

        // The daylight term is deliberately wider than the rush term. It makes
        // midday busier than the small hours without flattening the two peaks.
        float daylight = Daylight(minutes, 330f, 1140f);
        daylight = daylight * daylight * (3f - 2f * daylight);
        const float nightVolume = 0.12f;
        const float daytimeVolume = 0.58f;
        float baseVolume = Math.Max(0f, Math.Min(1f,
            nightVolume + (daytimeVolume - nightVolume) * daylight));
        return Math.Max(0f, Math.Min(1f, baseVolume + (1f - baseVolume) * rush));
    }
    static float CircularDistance(float a, float b) { float d = Math.Abs(a - b) % 1440f; return Math.Min(d, 1440f - d); }
    public static float LoadFactor(int passengers, int capacity) => 1f + 0.18f * Clamp01((float)passengers / Math.Max(1, capacity));
    public static float TyreGrip(float wear) => 1f - 0.28f * Clamp01((wear - 0.5f) / 0.5f);
    public static float Daylight(float minutes, float sunrise = 360f, float sunset = 1080f)
    {
        float phase = ((minutes % 1440f + 1440f) % 1440f - sunrise) / Math.Max(1f, sunset - sunrise);
        return phase <= 0f || phase >= 1f ? 0f : (float)Math.Sin(phase * Math.PI);
    }
    public static float StoppingSpeedKmh(float distance, float clearance, float deceleration) =>
        (float)Math.Sqrt(2f * Math.Max(0.1f, deceleration) * Math.Max(0f, distance - clearance)) * 3.6f;

    /// <summary>
    /// Speed a follower may carry behind a moving leader. Unlike the old
    /// gap-only clamp, a leader travelling at speed does not make a follower
    /// brake to the leader's stopping distance; the follower may close at the
    /// speed that can still be shed inside the remaining gap, after a short
    /// reaction delay. This keeps traffic flowing while remaining conservative
    /// enough for a stopped leader.
    /// </summary>
    public static float SafeFollowingSpeedKmh(float desiredKmh, float leaderKmh, float gap,
        float clearance, float deceleration, float reactionSeconds = 0.55f)
    {
        float desired = Math.Max(0f, desiredKmh);
        float leader = Math.Max(0f, leaderKmh);
        float available = gap - clearance;
        if (available <= 0f) return 0f;
        float reaction = desired / 3.6f * Math.Max(0f, reactionSeconds);
        float room = Math.Max(0f, available - reaction);
        float closingBudget = (float)Math.Sqrt(2f * Math.Max(0.1f, deceleration) * room) * 3.6f;
        return Math.Min(desired, leader + closingBudget);
    }

    /// <summary>
    /// Fraction of the approach speed a driver may carry through a junction.
    /// A ninety-degree turn is roughly 40%, while a shallow bend stays close to
    /// the limit. Angles are in degrees.
    /// </summary>
    public static float TurnSpeedFactor(float angleDegrees)
    {
        float normalized = Clamp01(Math.Abs(angleDegrees) / 120f);
        float eased = normalized * normalized * (3f - 2f * normalized);
        return 1f - 0.72f * eased;
    }

    /// <summary>
    /// Blends from the open-road speed down to a junction or turn cap as the
    /// vehicle approaches it. At the junction the cap applies; one approach
    /// length away the original speed is restored.
    /// </summary>
    public static float ApproachSpeedKmh(float desiredKmh, float capKmh, float distance, float approachDistance)
    {
        float desired = Math.Max(0f, desiredKmh);
        float cap = Math.Min(desired, Math.Max(0f, capKmh));
        float blend = Clamp01(distance / Math.Max(1f, approachDistance));
        return cap + (desired - cap) * blend;
    }

    /// <summary>
    /// A pass needs a minimum runway before the next junction, scaled with
    /// speed, so a vehicle does not change lanes just as the road ends.
    /// </summary>
    public static bool HasOvertakeRunway(float metersToJunction, float speedKmh,
        float minimumSeconds = 1.8f, float minimumMeters = 18f)
    {
        float required = Math.Max(minimumMeters,
            Math.Max(0f, speedKmh) / 3.6f * Math.Max(0f, minimumSeconds) + 8f);
        return metersToJunction > required;
    }

    /// <summary>
    /// Small deterministic per-driver cruise variation. It prevents the whole
    /// pool from travelling as one rigid convoy and gives overtaking a real
    /// reason to happen without making any car exceed the road limit.
    /// </summary>
    public static float SpeedFactor(int seed)
    {
        uint value = unchecked((uint)seed + 0x9E3779B9u);
        value ^= value >> 16;
        value *= 0x7FEB352Du;
        value ^= value >> 15;
        return 0.92f + (value & 0xFFFF) / 65535f * 0.14f;
    }

    public static int TileTier(float distance, float near = 200f, float medium = 500f) => distance <= near ? 0 : distance <= medium ? 1 : 2;
    static float Clamp01(float value) => Math.Max(0f, Math.Min(1f, value));
}
