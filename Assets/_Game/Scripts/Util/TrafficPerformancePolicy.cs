using UnityEngine;

/// <summary>
/// One place for the traffic budgets that decide how many vehicles are alive,
/// how far they keep full physics, and how often a distant one has to think.
///
/// The pool used to warm 160 cars and switch on 92 % of them at rush hour, which
/// is 147 bodies: twenty of those ran full physics, the rest ran the spline
/// controller and its per-frame perception, and every one of them was a separate
/// renderer hierarchy. The active fleet is now roughly half that, the physics
/// ring is smaller, and a car past the detail radius updates at 10-20 Hz instead
/// of every frame. The pool itself is unchanged: bodies are still instantiated
/// once and recycled, never created or destroyed while driving.
/// </summary>
public static class TrafficPerformancePolicy
{
    // ── Pool ────────────────────────────────────────────────────────────────
    // How many bodies are created during the loading card. They are reused for
    // the whole session; only the active count changes with the clock.
    public const int ImportedPoolSize = 120;
    public const int FallbackPoolSize = 64;

    // ── Active fleet ────────────────────────────────────────────────────────
    // The daily curve scales the active count between these fractions. 0.62 at
    // rush hour leaves about 74 of the 120 imported bodies on the road, against
    // 147 before, while a quiet hour still keeps a floor under an empty street.
    public const float MinActiveFraction = 0.24f;
    public const float MaxActiveFraction = 0.62f;
    public const int ImportedMinActiveVehicles = 12;
    public const int FallbackMinActiveVehicles = 8;

    // ── Simulation tiers ────────────────────────────────────────────────────
    // Full physics is the expensive tier: a rigidbody, wheel-free but still a
    // full solver body, and the complete AI controller. Keep it to the cars the
    // player can actually reach, then hand the rest to the kinematic spline.
    public const float FullAiRadiusMeters = 200f;
    public const float SplineRadiusMeters = 400f;
    /// <summary>
    /// The band a density fill is allowed to activate a body in. It is exactly
    /// the spline band on purpose: a body re-homed past <see cref="SplineRadiusMeters"/>
    /// is out of view, so the tier pass retires it on its next tick. That used to
    /// leave the fill activating cars at 400-520 m and the tier pass switching
    /// them straight back off, which is why the active fleet stalled well below
    /// the density target instead of reaching it.
    /// </summary>
    public static float DensitySpawnBandMeters => SplineRadiusMeters;
    public const float HardDespawnDistanceMeters = 600f;
    public const float VisibleDespawnDistanceMeters = 280f;
    public const float RehomeDistanceMeters = 600f;
    public const int MaxFullAiVehicles = 12;
    /// <summary>Global ceiling shared with ambient buses; keeps the physics fleet bounded across owners.</summary>
    public const int FullAiCeiling = 20;

    // ── Distant simulation cadence ──────────────────────────────────────────
    // A spline car still needs perception, but not sixty times a second once it
    // is a few hundred metres away. The step is accumulated, so motion keeps the
    // same average speed; only the update rate drops.
    public const float CoarseSimulationDistanceMeters = 180f;
    public const float FarSimulationDistanceMeters = 320f;
    public const float CoarseUpdateIntervalSeconds = 0.05f;
    public const float FarUpdateIntervalSeconds = 0.1f;

    // ── Presentation ────────────────────────────────────────────────────────
    // Headlights and other per-vehicle presentation stop being readable long
    // before the car does. Only components that actually exist are touched.
    public const float PresentationDistanceMeters = 160f;

    /// <summary>Seconds between spline updates for a vehicle this far from the bus. Zero means every frame.</summary>
    public static float SplineUpdateInterval(float distanceMeters)
    {
        if (distanceMeters >= FarSimulationDistanceMeters)
            return FarUpdateIntervalSeconds;
        if (distanceMeters >= CoarseSimulationDistanceMeters)
            return CoarseUpdateIntervalSeconds;
        return 0f;
    }

    /// <summary>Pool size for the catalog that is actually available this run.</summary>
    public static int PoolSize(bool importedModels)
        => importedModels ? ImportedPoolSize : FallbackPoolSize;

    /// <summary>Floor on active vehicles for the catalog that is actually available.</summary>
    public static int MinActiveVehicles(bool importedModels)
        => importedModels ? ImportedMinActiveVehicles : FallbackMinActiveVehicles;
}
