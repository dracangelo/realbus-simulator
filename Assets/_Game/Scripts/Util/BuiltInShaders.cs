using UnityEngine;

/// <summary>
/// The shaders this project renders with.
///
/// The project runs the Built-in Render Pipeline, and that is a decision rather
/// than an accident: <c>ProjectSettings/GraphicsSettings.asset</c> assigns no
/// pipeline asset (<c>m_CustomRenderPipeline: {fileID: 0}</c>),
/// <c>Packages/manifest.json</c> lists no URP package, no pipeline asset exists
/// in the project, and every authored material references a Built-in shader. A
/// runtime lookup for a <c>Universal Render Pipeline/...</c> shader could
/// therefore only ever return null, so every branch that preferred URP was dead
/// code - and it was dead in a way that hid live behaviour, because the same
/// branches wrote URP-only properties (<c>_BaseColor</c>, <c>_BaseMap</c>,
/// <c>_Surface</c>, <c>_AlphaClip</c>) onto Built-in materials, where the writes
/// are silently ignored.
///
/// Every runtime material resolves through here, so the render path has one
/// answer instead of a question, and the fallback chain each call site used to
/// repeat lives in a single place.
///
/// Properties a Built-in material created from these shaders actually reads:
/// <c>_Color</c> and <c>_MainTex</c> (albedo and its texture), <c>_Glossiness</c>
/// and <c>_Metallic</c> (Standard only), <c>_Cull</c>, <c>_ZWrite</c>,
/// <c>_Mode</c>, <c>_Cutoff</c>, <c>_SrcBlend</c> and <c>_DstBlend</c>. Callers
/// still guard optional writes with <c>HasProperty</c>, because the legacy
/// fallbacks below do not expose all of them.
/// </summary>
public static class BuiltInShaders
{
    static Shader lit;
    static Shader unlitTexture;
    static Shader unlitColor;
    static Shader overlay;
    static Shader skybox;

    /// <summary>
    /// Lit surface with metallic and smoothness slots - the <c>Standard</c>
    /// shader for buildings, roads, vehicles and props.
    /// </summary>
    public static Shader Lit => Resolve(ref lit, "Standard", "Diffuse");

    /// <summary>
    /// Textured, unlit, opaque surface - for map tiles, which carry their own
    /// lighting in the artwork and must not be shaded twice.
    /// </summary>
    public static Shader UnlitTexture => Resolve(ref unlitTexture, "Unlit/Texture", "Standard");

    /// <summary>Flat unlit colour - for opaque markers and route lines.</summary>
    public static Shader UnlitColor => Resolve(ref unlitColor, "Unlit/Color", "Diffuse");

    /// <summary>
    /// Tinted transparent overlay - for rain, snow, sky layers, route ghosts and
    /// other effects drawn over the world. <c>Sprites/Default</c> is first
    /// because it keeps its tint in <c>_Color</c>, is ZWrite-off and cull-off,
    /// and honours LineRenderer and ParticleSystem vertex colours.
    /// </summary>
    public static Shader Overlay => Resolve(ref overlay, "Sprites/Default", "Unlit/Transparent", "Unlit/Texture");

    /// <summary>
    /// The procedural sky the weather system builds its atmosphere from.
    /// </summary>
    public static Shader Skybox => Resolve(ref skybox, "Skybox/Procedural");

    /// <summary>
    /// Resolves the first available name once and remembers it. A miss is not
    /// cached: the editor can be asked for a shader before every asset is
    /// imported, and a cached null would make the whole render path permanent.
    /// </summary>
    static Shader Resolve(ref Shader cached, params string[] names)
    {
        if (cached != null)
            return cached;

        for (int i = 0; i < names.Length; i++)
        {
            Shader found = Shader.Find(names[i]);
            if (found != null)
                return cached = found;
        }

        return null;
    }
}
