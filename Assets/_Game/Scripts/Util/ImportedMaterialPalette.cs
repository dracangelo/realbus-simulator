using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Conservative palette fallback for imported models whose source textures are
/// missing from the project. Several bundled FBX files reference external
/// texture files that are not committed (for example the bus
/// <c>BUSTEXTURE\*.png</c> set or the gas-station artwork), so their materials
/// import with no texture and render flat white.
///
/// This helper only replaces material slots that have <b>no main texture</b>
/// and a <b>near-white albedo</b>. Any material that carries real artwork (a
/// main texture, or a colour that is not near-white) is left untouched, so the
/// fallback never overrides textures once they are restored.
///
/// The project runs the Built-in Render Pipeline, so the fallback resolves the
/// Standard shader through <see cref="BuiltInShaders"/>, the one place that
/// decides which shaders the project renders with.
/// </summary>
public static class ImportedMaterialPalette
{
    // Shared across calls so repeated spawns do not leak one material per prop.
    static readonly Dictionary<Color, Material> Cache = new Dictionary<Color, Material>();

    // Hierarchies that have been repaired once but may be re-checked after the
    // world finishes loading (late spawns and spawn paths that forget to call
    // ApplyFallback are covered by RepairRegisteredRoots).
    static readonly List<KeyValuePair<Transform, Color>> RegisteredRoots = new List<KeyValuePair<Transform, Color>>();
    const int MaxRegisteredRoots = 512;

    /// <summary>Default concrete/steel tone used for untextured props.</summary>
    public static readonly Color DefaultPropColor = new Color(0.66f, 0.66f, 0.63f, 1f);

    /// <summary>Number of live hierarchies waiting for a post-load repair pass.</summary>
    public static int RegisteredRootCount
    {
        get { PruneRegisteredRoots(); return RegisteredRoots.Count; }
    }

    /// <summary>
    /// Recolours untextured near-white materials under <paramref name="root"/>.
    /// Returns the number of material slots changed.
    /// </summary>
    public static int ApplyFallback(Transform root, Color color, float nearWhiteThreshold = 0.8f)
    {
        if (root == null)
            return 0;

        Shader shader = ResolveShader();
        if (shader == null)
            return 0;

        int changed = 0;
        foreach (Renderer renderer in root.GetComponentsInChildren<Renderer>(true))
        {
            if (renderer == null)
                continue;

            Material[] materials = renderer.sharedMaterials;
            bool dirty = false;
            for (int i = 0; i < materials.Length; i++)
            {
                Material source = materials[i];

                // Keep any material that already shows real artwork, and any
                // material that is deliberately coloured (not white).
                if (!IsUntexturedWhite(source, nearWhiteThreshold))
                    continue;

                materials[i] = GetOrCreate(shader, color);
                dirty = true;
                changed++;
            }

            if (dirty)
                renderer.sharedMaterials = materials;
        }

        return changed;
    }

    public static int ApplyFallback(GameObject root, Color color, float nearWhiteThreshold = 0.8f)
        => root == null ? 0 : ApplyFallback(root.transform, color, nearWhiteThreshold);

    /// <summary>
    /// Remembers a spawned imported hierarchy so <see cref="RepairRegisteredRoots"/>
    /// can recolour it once the world is fully loaded. Imported models are built
    /// by several systems, and this keeps the white-material fix from depending
    /// on every one of them calling <see cref="ApplyFallback"/> itself.
    /// </summary>
    public static void RegisterImportedRoot(Transform root, Color color)
    {
        if (root == null)
            return;

        PruneRegisteredRoots();
        for (int i = 0; i < RegisteredRoots.Count; i++)
        {
            if (RegisteredRoots[i].Key == root)
                return;
        }

        if (RegisteredRoots.Count >= MaxRegisteredRoots)
            RegisteredRoots.RemoveAt(0);

        RegisteredRoots.Add(new KeyValuePair<Transform, Color>(root, color));
    }

    public static void RegisterImportedRoot(GameObject root, Color color)
        => RegisterImportedRoot(root != null ? root.transform : null, color);

    /// <summary>Re-applies the fallback to every registered hierarchy. Returns slots changed.</summary>
    public static int RepairRegisteredRoots(float nearWhiteThreshold = 0.8f)
    {
        PruneRegisteredRoots();

        int changed = 0;
        for (int i = 0; i < RegisteredRoots.Count; i++)
            changed += ApplyFallback(RegisteredRoots[i].Key, RegisteredRoots[i].Value, nearWhiteThreshold);

        return changed;
    }

    /// <summary>Forgets all registered hierarchies (used by tests).</summary>
    public static void ClearRegisteredRoots() => RegisteredRoots.Clear();

    /// <summary>
    /// True when a material shows no artwork: it carries no main texture and its
    /// albedo is neutral white.
    ///
    /// The alpha channel is deliberately ignored for the near-white test. The
    /// FBX importer writes an albedo of <c>(1, 1, 1, 0)</c> for materials whose
    /// texture file is missing, and an opaque shader ignores that alpha and
    /// renders flat white - which is exactly the failure this palette repairs.
    /// Mid-range alpha is treated as intentional transparency (glass) and left
    /// alone.
    /// </summary>
    public static bool IsUntexturedWhite(Material material, float nearWhiteThreshold = 0.8f)
    {
        if (material == null)
            return true;
        // The player-bus livery shader intentionally has no albedo texture; its
        // colours are per-zone material properties. Without this guard the late
        // world-ready sweep would mistake it for an unpainted white import and
        // replace the whole livery with the fallback shell colour.
        if (material.shader != null && material.shader.name == "RealBus/BusPaint")
            return false;
        if (HasMainTexture(material))
            return false;

        Color c = ReadColor(material);
        if (c.a > 0.05f && c.a < 0.95f)
            return false;

        return c.r >= nearWhiteThreshold && c.g >= nearWhiteThreshold && c.b >= nearWhiteThreshold;
    }

    static void PruneRegisteredRoots()
    {
        for (int i = RegisteredRoots.Count - 1; i >= 0; i--)
        {
            if (RegisteredRoots[i].Key == null)
                RegisteredRoots.RemoveAt(i);
        }
    }

    static Shader ResolveShader() => BuiltInShaders.Lit;

    static Material GetOrCreate(Shader shader, Color color)
    {
        // The palette is always an opaque replacement; an imported alpha of 0
        // must never leak into the material we create.
        color.a = 1f;

        if (Cache.TryGetValue(color, out Material existing) && existing != null)
            return existing;

        var material = new Material(shader)
        {
            name = "ImportedFallback_" + ColorUtility.ToHtmlStringRGB(color),
            // Explicitly set the legacy color slot so Built-in shaders work too.
            color = color
        };
        // The color property above writes _Color, the albedo slot of every
        // Built-in lit shader; the same value is written again for shaders that
        // only expose the property, never URP's _BaseColor.
        if (material.HasProperty("_Color")) material.SetColor("_Color", color);
        if (material.HasProperty("_Glossiness")) material.SetFloat("_Glossiness", 0.25f);
        if (material.HasProperty("_Smoothness")) material.SetFloat("_Smoothness", 0.25f);
        // Ensure the material renders as opaque (workflow = Metallic, mode = Opaque)
        if (material.HasProperty("_WorkflowMode")) material.SetFloat("_WorkflowMode", 1f);
        if (material.HasProperty("_Mode"))         material.SetFloat("_Mode",         0f);
        if (material.HasProperty("_ZWrite"))       material.SetFloat("_ZWrite",       1f);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)RenderQueue.Geometry;

        Cache[color] = material;
        return material;
    }

    static bool HasMainTexture(Material material)
        => material.HasProperty("_MainTex") && material.GetTexture("_MainTex") != null;

    static Color ReadColor(Material material)
    {
        if (material == null) return Color.white;
        if (material.HasProperty("_Color")) return material.GetColor("_Color");
        return Color.white;
    }
}
