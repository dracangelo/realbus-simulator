using UnityEngine;

/// <summary>
/// One budget for the people on the street: how many walkers exist, how often
/// they animate, and how far a character can be before its animation stops.
///
/// The two pedestrian owners (the pooled crossing spawner and the ambient live
/// city streamer) and the passenger visual pool used to have independent
/// numbers, and every walker animated every frame regardless of distance. A
/// 40-walker crowd plus the crossing pool is small in body count but each body
/// is a multi-renderer imported model, so the per-frame cost was the animation
/// update rather than the renderer count. This policy keeps the near street
/// fully animated, halves the rate at street range and freezes the pose once a
/// character is past the distance its renderer is drawn at.
/// </summary>
public static class CrowdPerformancePolicy
{
    // ── Active budgets ──────────────────────────────────────────────────────
    /// <summary>Crossing pedestrians alive at once. Only crossings near the bus are used.</summary>
    public const int MaxCrossingPedestrians = 48;
    /// <summary>Ambient walkers the live city may keep. They are dressed as scenery, not agents.</summary>
    public const int MaxAmbientWalkers = 32;
    /// <summary>Visual passenger proxies; a full bus plus the platform still fits.</summary>
    public const int MaxPassengerProxies = 96;

    // ── Animation distance bands ────────────────────────────────────────────
    /// <summary>Inside this radius a character keeps its full walk cycle and body sway.</summary>
    public const float FullAnimationDistanceMeters = 35f;
    /// <summary>Between the two bands the walk cycle is simplified (no sway) and thinned.</summary>
    public const float ReducedAnimationDistanceMeters = 80f;
    /// <summary>Past this radius the pose is frozen; the renderer is usually culled here too.</summary>
    public const float AnimationCutoffMeters = SceneryLodPolicy.PedestrianRenderDistance;

    // ── Update cadence ──────────────────────────────────────────────────────
    public const float NearIntervalSeconds = 0f;
    public const float ReducedIntervalSeconds = 0.1f;
    public const float DistantIntervalSeconds = 0.25f;
    /// <summary>Ambient tree sway is decoration; it does not need a per-frame update.</summary>
    public const float SwayIntervalSeconds = 0.2f;

    // ── Spawning and retirement ─────────────────────────────────────────────
    /// <summary>A crossing farther than this from the bus is not worth a walker.</summary>
    public const float CrossingSpawnRadiusMeters = 140f;
    /// <summary>A walker past this radius is retired and returned to the pool.</summary>
    public const float WalkerRetireDistanceMeters = AnimationCutoffMeters * 1.25f;

    /// <summary>True while a character at this distance should animate at all.</summary>
    public static bool AnimatesAt(float distanceMeters)
        => distanceMeters < AnimationCutoffMeters;

    /// <summary>True while a character is close enough for the full walk cycle and sway.</summary>
    public static bool UsesFullAnimation(float distanceMeters)
        => distanceMeters < FullAnimationDistanceMeters;

    /// <summary>
    /// Seconds between animation steps for a character this far away. Zero keeps
    /// the per-frame update; the step is accumulated by the caller, so motion
    /// keeps its average speed instead of slowing down.
    /// </summary>
    public static float AnimationInterval(float distanceMeters)
    {
        if (distanceMeters >= ReducedAnimationDistanceMeters)
            return DistantIntervalSeconds;
        if (distanceMeters >= FullAnimationDistanceMeters)
            return ReducedIntervalSeconds;
        return NearIntervalSeconds;
    }

    /// <summary>Budget for a walker owner, clamped so an authored value cannot exceed the policy.</summary>
    public static int ClampWalkerBudget(int authored, int policyMax)
        => Mathf.Clamp(authored <= 0 ? policyMax : authored, 1, policyMax);

    /// <summary>
    /// The crowd a quality tier can afford. Every walker is a multi-renderer
    /// imported model, so the Low tier and a phone do not get the desktop crowd:
    /// half the budget there, three quarters on Medium, the full policy from High
    /// up. This is the same ladder the render settings use, so lowering the
    /// quality preset visibly thins the pavement instead of only softening it.
    /// </summary>
    public static int BudgetForQuality(int qualityLevel, bool mobilePlatform, int policyMax)
    {
        int ceiling = Mathf.Max(1, policyMax);
        float fraction;
        if (qualityLevel <= 0) fraction = .5f;
        else if (qualityLevel == 1) fraction = .75f;
        else fraction = 1f;
        // A phone runs at High here only because the tier names are shared; the
        // smaller screen still cannot justify a desktop-sized crowd.
        if (mobilePlatform) fraction = Mathf.Min(fraction, .6f);
        return Mathf.Clamp(Mathf.CeilToInt(ceiling * fraction), 1, ceiling);
    }
}
