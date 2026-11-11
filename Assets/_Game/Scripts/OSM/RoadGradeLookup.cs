using System;
using UnityEngine;

/// <summary>
/// Runtime road-grade lookup (section 3.4).
///
/// Provides the current road grade (rise / run, -1..1) at any world position,
/// backed by the baked <see cref="RoadElevationLookup"/> data. Runtime never
/// calls a DEM service — all grades come from the grade bake that ran at import
/// time and live in the city bundle.
///
/// Every system that needs grade (fuel, retarder, brake fade, hill start, HUD,
/// sway, AI speed, route difficulty) reads from here so they all agree.
/// </summary>
public static class RoadGradeLookup
{
    /// <summary>Local road grade as rise / run in [-1, 1] at the given world
    /// position, or 0 when no baked data covers this position.</summary>
    public static float GetGrade(Vector3 worldPosition)
    {
        var converter = CoordinateConverter.Instance;
        if (converter == null) return 0f;

        var (lat, lon) = converter.WorldToGeoPosition(worldPosition);

        var elevA = RoadElevationLookup.GetElevation(lat, lon);
        var elevB = RoadElevationLookup.GetElevation(
            lat + 1e-5d,
            lon + 1e-5d);

        if (elevA == null || elevB == null) return 0f;

        float rise = elevB.Value - elevA.Value;
        // ~1.113 m per 1e-5 degree of lat/lon at this latitude scale.
        float run = 1.113f;
        return Mathf.Clamp(rise / run, -1f, 1f);
    }

    /// <summary>Grade at the player bus's current position.</summary>
    public static float GetGradeAtBus()
    {
        var bus = FindBus();
        if (bus == null) return 0f;
        return GetGrade(bus.transform.position);
    }

    static BusController FindBus()
    {
        if (MissionManager.Instance != null && MissionManager.Instance.busController != null)
            return MissionManager.Instance.busController;
        return UnityEngine.Object.FindAnyObjectByType<BusController>();
    }
}