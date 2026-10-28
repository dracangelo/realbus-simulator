using UnityEngine;

/// <summary>
/// One place for the distances and screen sizes that decide how much of the road
/// environment is drawn. Kept pure and static so the policy can be checked without
/// a scene, and so the camera, the streamers and the LOD builder cannot disagree.
///
/// The camera used to clip at 1.5-2.5 km (4 km in the authored scene) while the
/// city streams buildings only 600-800 m around the bus. Everything past the last
/// streamed block was empty ground drawn at full cost, and every building in that
/// band paid for its full mesh. These values put the far plane inside the haze and
/// let the LOD groups drop detail before the fog does.
/// </summary>
public static class SceneryLodPolicy
{
    // The gameplay camera never needs to see further than the weather can show.
    // Clear haze is opaque by 950 m and overcast fog by 1200 m, so a 1 km far
    // plane removes the empty ring beyond the streamed city without a visible cut.
    public const float GameplayFarClip = 1000f;
    public const float MinFarClip = 600f;
    public const float MaxFarClip = GameplayFarClip;

    // Screen-relative heights at which a LOD group changes level. LODGroup
    // measures the group's bounds against the screen, so a tall tower keeps its
    // detail further out than a two-storey house at the same distance.
    public const float BuildingLod1ScreenHeight = 0.06f;
    public const float BuildingLod2ScreenHeight = 0.02f;
    public const float PropLod1ScreenHeight = 0.05f;
    public const float PropLod2ScreenHeight = 0.018f;

    // A mesh below this cost is already as cheap as its own box proxy. Adding a
    // LODGroup to a few dozen procedural quads would cost more than it saves.
    public const int BuildingLodTriangleThreshold = 1500;
    public const int PropLodTriangleThreshold = 2000;

    // Tiny street furniture stops being readable long before a tree does. These
    // are the distances at which the streamer deactivates the renderer; the
    // object itself stays alive so walking back toward it is instant.
    public const float VegetationRenderDistance = 240f;
    public const float TinyPropRenderDistance = 130f;
    public const float PedestrianRenderDistance = 120f;

    /// <summary>How far a street prop of this kind is drawn around the bus.</summary>
    public static float PropRenderDistance(LiveCityCatalog.Kind kind)
    {
        switch (kind)
        {
            case LiveCityCatalog.Kind.Pedestrian:
                return PedestrianRenderDistance;
            case LiveCityCatalog.Kind.Bench:
            case LiveCityCatalog.Kind.Billboard:
            case LiveCityCatalog.Kind.Rock:
            case LiveCityCatalog.Kind.StreetLight:
            case LiveCityCatalog.Kind.RoadSign:
                return TinyPropRenderDistance;
            default:
                return VegetationRenderDistance;
        }
    }

    /// <summary>
    /// Flat colour used by a street prop's box proxy. Sampling the imported
    /// material would usually return white (the detail lives in its texture), so
    /// the proxy gets the colour the prop reads as at a distance instead.
    /// </summary>
    public static Color PropTint(LiveCityCatalog.Kind kind)
    {
        switch (kind)
        {
            case LiveCityCatalog.Kind.Tree: return new Color(.20f, .32f, .16f);
            case LiveCityCatalog.Kind.Plant: return new Color(.24f, .36f, .18f);
            case LiveCityCatalog.Kind.Rock: return new Color(.38f, .36f, .33f);
            case LiveCityCatalog.Kind.Bench: return new Color(.32f, .24f, .16f);
            case LiveCityCatalog.Kind.Billboard: return new Color(.55f, .57f, .60f);
            case LiveCityCatalog.Kind.StreetLight: return new Color(.26f, .27f, .28f);
            case LiveCityCatalog.Kind.RoadSign: return new Color(.72f, .74f, .76f);
            default: return new Color(.30f, .32f, .35f);
        }
    }

    /// <summary>True when a mesh is expensive enough to be worth a three-level LOD group.</summary>
    public static bool NeedsLod(int triangles, int threshold)
        => triangles >= Mathf.Max(1, threshold);

    /// <summary>Keeps a camera that was authored for the old 4 km world inside the new budget.</summary>
    public static float ClampFarClip(float farClip)
        => Mathf.Clamp(farClip <= 0f ? GameplayFarClip : farClip, MinFarClip, MaxFarClip);
}
