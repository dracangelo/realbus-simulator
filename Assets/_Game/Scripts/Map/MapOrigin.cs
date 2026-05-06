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
        System.Math.Abs(originLat) > 0.0001 || System.Math.Abs(originLon) > 0.0001;
}
