using UnityEngine;

/// <summary>
/// Keeps the asphalt at least as wide as the vehicles that use it.
///
/// OSM lane tags describe the real street, but they are frequently narrower than
/// the imported bus models. The traffic graph can space its lane centres out, but
/// unless the rendered ribbon and kerbs move with it the outer wheels still hang
/// over the pavement. This policy publishes the required per-lane width to the
/// OSM width and marking code before roads are generated.
/// </summary>
public static class RoadWidthPolicy
{
    /// <summary>Extra room around the widest traffic model, matching VehiclePool's default.</summary>
    public const float ModelPaddingMeters = 0.55f;

    /// <summary>Upper bound for automatic widening; protects against a bad model measurement.</summary>
    public const float MaxLaneWidthMeters = 5.2f;

    /// <summary>A sensible floor even when no imported traffic catalog was built.</summary>
    public const float BaselineLaneWidthMeters = 3.3f;

    /// <summary>The per-lane width the rendered road and the traffic graph should share.</summary>
    public static float MinimumLaneWidth { get; private set; } = BaselineLaneWidthMeters;

    /// <summary>
    /// Reads the traffic catalog once, before the road mesh is generated, and
    /// raises the lane floor to the widest model that pool can spawn. The same
    /// measurement is reused by VehiclePool for the graph, so the painted lanes
    /// and the lane lines cannot disagree.
    /// </summary>
    public static void ConfigureFromTrafficCatalog()
    {
        var catalog = Resources.Load<TrafficCatalog>("TrafficCatalog");
        if (catalog == null || catalog.entries == null)
            return;

        float widest = 0f;
        for (int i = 0; i < catalog.entries.Length; i++)
        {
            var entry = catalog.entries[i];
            if (entry == null || entry.prefab == null) continue;
            widest = Mathf.Max(widest, VehiclePool.MeasurePrefabSize(entry.prefab).x);
        }

        if (widest > 0f)
            Request(widest + ModelPaddingMeters);
    }

    /// <summary>Raises (never lowers) the rendered lane width for this run.</summary>
    public static void Request(float laneWidth)
    {
        float clamped = Mathf.Clamp(laneWidth, BaselineLaneWidthMeters, MaxLaneWidthMeters);
        if (clamped > MinimumLaneWidth)
            MinimumLaneWidth = clamped;
    }

    /// <summary>Returns the larger of an OSM lane width and the traffic fit floor.</summary>
    public static float Fit(float osmLaneWidth)
    {
        return Mathf.Max(Mathf.Max(0f, osmLaneWidth), MinimumLaneWidth);
    }

    /// <summary>Test hook; production code never narrows the policy after startup.</summary>
    public static void ResetForTests()
    {
        MinimumLaneWidth = BaselineLaneWidthMeters;
    }
}
