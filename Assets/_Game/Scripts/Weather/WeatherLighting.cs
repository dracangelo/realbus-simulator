using UnityEngine;

/// <summary>
/// How much of the daylight survives the cloud.
///
/// Before this rule existed, weather changed the colour of the sun and the fog
/// but not the light itself: a thunderstorm still cast crisp shadows, and glossy
/// paint still mirrored a bright blue sky, because the ambient and reflection
/// passes only ever read the clock. Cloud cover is the single input here - it is
/// already mapped per state by <see cref="SkyEnvironmentController.CloudCoverageFor"/>
/// and smoothed through every transition - so lighting, reflections and shadows
/// can never disagree about how overcast it is.
///
/// All of it is pure, so the "a storm is dark" claim can be checked without a frame.
/// </summary>
public static class WeatherLighting
{
    /// <summary>Cloud cover as 0..1; the only input every rule below shares.</summary>
    public static float Coverage01(float cloudCoverage) => Mathf.Clamp01(cloudCoverage);

    /// <summary>
    /// How much of a cast shadow reaches the ground. Full sun gives a hard shadow,
    /// a solid overcast gives none, but a little is kept: at zero strength every
    /// prop reads as floating, which is the other way a cloudy day looks wrong.
    /// </summary>
    public static float ShadowStrengthScale(float coverage)
        => Mathf.Lerp(1f, 0.12f, Smooth(Coverage01(coverage)));

    /// <summary>
    /// Diffuse sky light under cloud. Overcast is dimmer than clear noon, but far
    /// less dim than the direct sun: cloud scatters light rather than removing it,
    /// so the ambient floor stays high enough for a shaded wall to stay readable.
    /// </summary>
    public static float AmbientScale(float coverage)
        => Mathf.Lerp(1f, 0.55f, Mathf.Pow(Coverage01(coverage), 1.35f));

    /// <summary>
    /// Specular sky reflection under cloud. A grey ceiling reflects much less than
    /// a blue sky and what it does reflect is neutral, so wet asphalt under a storm
    /// is a dull sheen instead of a mirror of the sun.
    /// </summary>
    public static float ReflectionScale(float coverage)
        => Mathf.Lerp(1f, 0.40f, Coverage01(coverage));

    /// <summary>
    /// Pulls a reflection tint toward its own luminance as cloud thickens. Colour
    /// without brightness is what makes a rainy scene look tinted rather than lit.
    /// </summary>
    public static Color Desaturate(Color tint, float coverage, float maxDesaturation = 0.55f)
    {
        float amount = Coverage01(coverage) * Mathf.Clamp01(maxDesaturation);
        float grey = tint.r * 0.299f + tint.g * 0.587f + tint.b * 0.114f;
        var neutral = new Color(grey, grey, grey, tint.a);
        return Color.Lerp(tint, neutral, amount);
    }

    /// <summary>
    /// The sun's direct contribution, as the cloud lets it through. The per-state
    /// palette in <see cref="SkyController"/> already picks a target intensity; this
    /// is the continuous part, so half a sky of cloud dims half the light instead of
    /// snapping at the state boundary.
    /// </summary>
    public static float SunScale(float coverage)
        => Mathf.Lerp(1f, 0.18f, Coverage01(coverage));

    static float Smooth(float t) => t * t * (3f - 2f * t);
}
