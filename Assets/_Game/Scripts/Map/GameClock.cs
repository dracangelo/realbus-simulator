using UnityEngine;

/// <summary>
/// Three-clock time model for realistic route times (section 4).
///
/// • <b>PhysicsTime</b> — real time, fixed timestep, never accelerated.
///   <see cref="UnityEngine.Time.timeScale"/> is never raised above 1, so
///   WheelColliders stay stable and physics cost stays bounded.
/// • <b>SimClock</b> — drives the timetable, scoring, passenger density,
///   day/night, signals and events. Equals real time during manual driving.
///   May run at x4–x30 only during autopilot, layover and dwell, so the player
///   doesn't have to sit through empty road or stopped-bus time.
/// • <b>Real time</b> — menus, ads, haptics and indicator blink. Always runs
///   at 1:1 so pausing and skipping never desync them.
///
/// The SimClock is the single source of truth for ScheduleManager, MissionData
/// deadlines, punctuality scoring and TimeOfDaySystem sky phase. Physics and
/// input always read PhysicsTime.
/// </summary>
public static class GameClock
{
    /// <summary>Real-world seconds elapsed since the clock was created.</summary>
    public static double PhysicsTime { get; private set; }

    /// <summary>Simulated minutes since midnight, driven by SimClock.</summary>
    public static double SimMinutes { get; private set; }

    /// <summary>Current SimClock speed multiplier (1.0 = real time).</summary>
    public static float SimSpeedMultiplier { get; private set; } = 1f;

    /// <summary>True when SimClock is running faster than real time.</summary>
    public static bool IsAccelerated => SimSpeedMultiplier > 1.01f;

    /// <summary>Seconds of real time elapsed this frame (PhysicsTime delta).</summary>
    public static float PhysicsDeltaTime => Time.unscaledDeltaTime;

    /// <summary>Simulated seconds elapsed this frame (SimClock delta).</summary>
    public static float SimDeltaTime => Time.unscaledDeltaTime * SimSpeedMultiplier;

    static GameClock() { Reset(); }

    /// <summary>Call once at bootstrap. SimMinutes starts at the game start time.</summary>
    public static void Reset(double startSimMinutes = 480d)
    {
        PhysicsTime = 0d;
        SimMinutes = startSimMinutes;
        SimSpeedMultiplier = 1f;
    }

    /// <summary>Call every frame before any clock consumer.</summary>
    public static void Tick()
    {
        PhysicsTime += Time.unscaledDeltaTime;
        SimMinutes += SimDeltaTime / 60d;
        if (SimMinutes >= 1440d) SimMinutes -= 1440d;
        else if (SimMinutes < 0d) SimMinutes += 1440d;
    }

    /// <summary>Set SimClock multiplier. Clamped to [1, 30].</summary>
    public static void SetSimSpeed(float multiplier)
    {
        SimSpeedMultiplier = Mathf.Clamp(multiplier, 1f, 30f);
    }

    /// <summary>Return SimClock to real time (manual driving).</summary>
    public static void SetRealTime() => SimSpeedMultiplier = 1f;

    /// <summary>Enter accelerated mode for autopilot / layover / dwell.</summary>
    public static void SetAccelerated(float multiplier)
    {
        SimSpeedMultiplier = Mathf.Clamp(multiplier, 1f, 30f);
    }

    /// <summary>Convert SimMinutes to a HH:mm AM/PM string.</summary>
    public static string SimTimeString()
    {
        int total = Mathf.FloorToInt((float)SimMinutes);
        int hours24 = (total / 60) % 24;
        int mins = total % 60;
        string ampm = hours24 >= 12 ? "PM" : "AM";
        int hours12 = hours24 % 12;
        if (hours12 == 0) hours12 = 12;
        return $"{hours12:00}:{mins:00} {ampm}";
    }

    /// <summary>Convert SimMinutes to hours24 (0-23).</summary>
    public static int SimHour24() => Mathf.FloorToInt((float)SimMinutes / 60) % 24;
}