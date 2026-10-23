using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// The reflection environment for the city: what glossy surfaces actually mirror.
///
/// Nothing used to set one, so every material with any smoothness - the bus
/// clear-coat, the glass, the polished wheel paths in the asphalt - reflected the
/// default sky probe that Unity bakes once at startup at 16 pixels. That probe is
/// daylight blue at midnight and cloudless at noon, which is why reflections used to
/// read as a flat grey sheen instead of as sky.
///
/// This builds a tiny cubemap from the same tint the sky and the fog are using, adds
/// a warm lobe where the sun is, and hands it to <see cref="RenderSettings"/>. It is
/// 32 pixels a face, rebuilt only when the tint moves, so the cost is a few thousand
/// colour writes a few times a minute - not a render pass. The extra resolution over
/// the old 16 px cube keeps the sun lobe and horizon band from smearing across the
/// whole bonnet while still being far cheaper than a real reflection probe.
/// </summary>
public static class SkyReflectionEnvironment
{
    /// <summary>Face size in pixels. Small on purpose: a reflection is a blur.</summary>
    public const int Resolution = 32;

    static Cubemap active;

    /// <summary>The cubemap currently assigned to the render settings, if any.</summary>
    public static Cubemap Active => active;

    /// <summary>
    /// How much the sky contributes to specular reflections at a given daylight
    /// level. Night reflections are dim but not gone: a wet street at 2 a.m. still
    /// picks up the moon and the streetlights.
    /// </summary>
    public static float IntensityFor(float daylight, float dayIntensity, float nightIntensity)
        => Mathf.Lerp(Mathf.Clamp01(nightIntensity), Mathf.Clamp01(dayIntensity), Mathf.Clamp01(daylight));

    /// <summary>
    /// The colour of the reflected sky. Daytime follows the sky tint, so a golden
    /// hour warms paint and glass; night pulls toward a cold moonlit blue rather than
    /// simply scaling the daylight colour down, because a scaled-down orange sky
    /// reads as brown.
    /// </summary>
    public static Color ReflectionTint(Color skyTint, float daylight)
    {
        Color day = Color.Lerp(Color.white, skyTint, 0.55f);
        day.a = 1f;
        Color night = new Color(0.075f, 0.095f, 0.170f, 1f);
        return Color.Lerp(night, day, Mathf.Clamp01(daylight));
    }

    /// <summary>
    /// Ambient trilight as the clock lights it: the authored daytime colour tinted by
    /// the sky and blended down to a night floor. Ambient is what keeps a shaded wall
    /// readable, so the night floor is a colour rather than black.
    /// </summary>
    public static Color AmbientFor(Color authoredDay, Color nightFloor, Color skyTint, float daylight)
    {
        Color tint = Color.Lerp(Color.white, skyTint, 0.35f);
        tint.a = 1f;
        Color day = new Color(authoredDay.r * tint.r, authoredDay.g * tint.g, authoredDay.b * tint.b, 1f);
        return Color.Lerp(nightFloor, day, Mathf.Clamp01(daylight));
    }

    /// <summary>
    /// True when the environment has moved enough to be worth rebuilding. Rebuilding
    /// on every frame would be pointless work on a 16-pixel cube the player cannot
    /// see change; rebuilding never would leave a daylight reflection at midnight.
    /// </summary>
    public static bool ShouldRebuild(Color previousTint, float previousDaylight, Color tint, float daylight,
        float tolerance = 0.015f)
    {
        if (active == null) return true;
        if (Mathf.Abs(daylight - previousDaylight) > tolerance) return true;
        return Mathf.Abs(previousTint.r - tint.r) > tolerance
            || Mathf.Abs(previousTint.g - tint.g) > tolerance
            || Mathf.Abs(previousTint.b - tint.b) > tolerance;
    }

    /// <summary>
    /// Builds the cubemap: sky overhead, hazier band at the horizon, dark ground, and
    /// a warm lobe in the direction of the sun. Caller owns the result.
    /// </summary>
    public static Cubemap Build(Color tint, float daylight, Vector3 sunDirection, int resolution = Resolution)
    {
        resolution = Mathf.Clamp(resolution, 4, 64);

        var cube = new Cubemap(resolution, TextureFormat.RGBA32, false)
        {
            name = "Sky Reflection (runtime)",
            hideFlags = HideFlags.DontSave,
            wrapMode = TextureWrapMode.Clamp,
            filterMode = FilterMode.Trilinear
        };

        float day = Mathf.Clamp01(daylight);
        Vector3 sun = sunDirection.sqrMagnitude > 0.0001f ? sunDirection.normalized : Vector3.up;

        Color zenith = Scaled(tint, 1f);
        Color horizon = Scaled(tint, 0.62f);
        Color ground = Scaled(tint, 0.10f);
        // The lobe is the sun's own colour: reflections of a low sun are the most
        // obvious sign that a glossy surface is reflecting anything at all.
        Color sunLobe = Scaled(new Color(1f, 0.86f, 0.68f), 1.1f * day);

        var pixels = new Color[resolution * resolution];
        for (int face = 0; face < 6; face++)
        {
            for (int y = 0; y < resolution; y++)
            for (int x = 0; x < resolution; x++)
            {
                float u = (x + 0.5f) / resolution * 2f - 1f;
                float v = (y + 0.5f) / resolution * 2f - 1f;
                Vector3 direction = FaceDirection(face, u, v).normalized;

                Color color = direction.y >= 0f
                    ? Color.Lerp(horizon, zenith, Mathf.SmoothStep(0f, 1f, direction.y))
                    : Color.Lerp(horizon, ground, Mathf.SmoothStep(0f, 1f, -direction.y));

                float sunAmount = Mathf.Pow(Mathf.Max(0f, Vector3.Dot(direction, sun)), 8f);
                color.r = Mathf.Clamp01(color.r + sunLobe.r * sunAmount);
                color.g = Mathf.Clamp01(color.g + sunLobe.g * sunAmount);
                color.b = Mathf.Clamp01(color.b + sunLobe.b * sunAmount);
                color.a = 1f;

                pixels[y * resolution + x] = color;
            }

            cube.SetPixels(pixels, (CubemapFace)face);
        }

        cube.Apply(false, false);
        return cube;
    }

    /// <summary>Assigns a cubemap as the scene's reflection source.</summary>
    public static void Apply(Cubemap cubemap, float intensity)
    {
        if (cubemap == null) return;
        RenderSettings.defaultReflectionMode = DefaultReflectionMode.Custom;
        RenderSettings.customReflectionTexture = cubemap;
        RenderSettings.reflectionIntensity = Mathf.Clamp01(intensity);
    }

    /// <summary>
    /// Makes a cubemap the active environment and destroys the one it replaces. The
    /// old cube is a runtime asset with DontSave, so nothing else will collect it.
    /// </summary>
    public static void Adopt(Cubemap cubemap)
    {
        if (cubemap == null) return;
        if (active != null && active != cubemap)
        {
            if (RenderSettings.customReflectionTexture == active)
                RenderSettings.customReflectionTexture = null;
            if (Application.isPlaying) Object.Destroy(active);
            else Object.DestroyImmediate(active);
        }
        active = cubemap;
    }

    /// <summary>Drops the runtime environment and hands reflections back to the default probe.</summary>
    public static void Release()
    {
        if (RenderSettings.customReflectionTexture == active)
            RenderSettings.customReflectionTexture = null;
        if (active != null)
        {
            if (Application.isPlaying) Object.Destroy(active);
            else Object.DestroyImmediate(active);
        }
        active = null;
    }

    /// <summary>Unit cube-face direction for normalised face coordinates.</summary>
    static Vector3 FaceDirection(int face, float u, float v)
    {
        switch (face)
        {
            case 0: return new Vector3(1f, -v, -u);    // +X
            case 1: return new Vector3(-1f, -v, u);    // -X
            case 2: return new Vector3(u, 1f, v);      // +Y
            case 3: return new Vector3(u, -1f, -v);    // -Y
            case 4: return new Vector3(u, -v, 1f);     // +Z
            default: return new Vector3(-u, -v, -1f);  // -Z
        }
    }

    static Color Scaled(Color color, float factor)
        => new Color(color.r * factor, color.g * factor, color.b * factor, 1f);
}
