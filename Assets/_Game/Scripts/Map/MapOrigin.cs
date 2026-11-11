using System;
using UnityEngine;

/// <summary>
/// Stores the GPS anchor point for the current map session.
/// Create via: Assets → RealBus → Map → Map Origin
/// One asset per deployment; origin is overwritten at runtime when a city loads.
/// </summary>
[CreateAssetMenu(fileName = "MapOrigin", menuName = "RealBus/Map/Map Origin")]
public class MapOrigin : ScriptableObject
{
    [Header("Origin GPS (WGS84)")]
    public double originLat = -1.2864;   // Nairobi CBD default
    public double originLon = 36.8172;

    [Header("Notes")]
    public string originLabel = "Default (Nairobi CBD)";

    // ── Runtime helpers ──────────────────────────────────────────────

    /// <summary>True after the origin has been set from a loaded city this session.</summary>
    [System.NonSerialized] public bool isInitialised;

    public void SetFromCity(CityDefinition city)
    {
        if (city == null) return;
        SetOrigin(city.centreLat, city.centreLon, city.cityName);
    }

    public void SetOrigin(double latitude, double longitude, string label = null)
    {
        originLat = latitude;
        originLon = longitude;
        originLabel = string.IsNullOrWhiteSpace(label) ? "Runtime Origin" : label;
        isInitialised = true;
    }

    public bool IsValid() =>
        !double.IsNaN(originLat) && !double.IsNaN(originLon) &&
        System.Math.Abs(originLat) <= 90d && System.Math.Abs(originLon) <= 180d;

    /// <summary>
    /// Shifts the origin by the given world-space displacement, used by the
    /// floating-origin recentring (section 3.3).
    ///
    /// GPS positions of existing world content are unchanged by this call:
    /// callers must shift every world object by <c>-worldShiftMeters</c>
    /// themselves so that world positions stay tied to the same GPS coordinates.
    /// Only the horizontal (X/Z) displacement affects the origin; Y is ignored.
    /// </summary>
    public void ShiftOrigin(Vector3 worldShiftMeters)
    {
        if (worldShiftMeters.sqrMagnitude < 1e-6f) return;

        double metersPerDegreeLon = CoordinateConverter.MetersPerDegreeLat *
            System.Math.Cos(System.Math.PI * originLat / 180.0);
        if (Math.Abs(metersPerDegreeLon) < 1e-6) metersPerDegreeLon = CoordinateConverter.MetersPerDegreeLat;

        originLat += worldShiftMeters.z / CoordinateConverter.MetersPerDegreeLat;
        originLon += worldShiftMeters.x / metersPerDegreeLon;

        originLabel = $"Runtime origin (shifted {worldShiftMeters.magnitude:F1} m)";
        isInitialised = true;
    }
}
