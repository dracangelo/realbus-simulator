using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Repairs the materials inside imported bus and vehicle prefabs on the
/// Built-in Render Pipeline: missing source textures leave FBX panels flat
/// white, glass imports opaque, and a material can arrive with a broken shader.
/// It is deliberately runtime-safe so newly generated bus prefabs work without
/// manual material extraction.
/// </summary>
public class ImportedVehicleVisualRepair : MonoBehaviour
{
    [Tooltip("Tint untextured, neutral body panels when the FBX contains no body artwork.")]
    public bool useFallbackBodyColor = true;
    public Color fallbackBodyColor = new Color(0.08f, 0.28f, 0.22f, 1f);
    readonly List<Material> runtimeMaterials = new List<Material>();
    bool repairApplied;

    void Start()
    {
        // This component used to be added to generated buses without ever
        // invoking RepairNow, leaving imported material problems unrepaired.
        RepairNow();
    }

    public int RepairNow(Transform visualRoot = null)
    {
        if (repairApplied && visualRoot == null)
            return GetComponentsInChildren<Renderer>(true).Length;

        Transform root = visualRoot != null ? visualRoot : transform;
        var bus = GetComponent<BusController>();
        DisableEmbeddedCameras(bus != null ? bus.modelRoot : root);
        Renderer[] renderers = root.GetComponentsInChildren<Renderer>(true);
        if (renderers.Length == 0)
        {
            Debug.LogError($"ImportedVehicleVisualRepair: '{root.name}' has no renderers. Rerun Tools > RealBus > Wire Imported Models.");
            return 0;
        }

        Shader targetShader = BuiltInShaders.Lit;
        var replacements = new Dictionary<Material, Material>();
        int repaired = 0;

        foreach (Renderer renderer in renderers)
        {
            if (renderer == null) continue;
            renderer.enabled = true;
            renderer.forceRenderingOff = false;

            Material[] materials = renderer.sharedMaterials;
            bool changed = false;
            for (int i = 0; i < materials.Length; i++)
            {
                Material source = materials[i];
                bool invalid = source == null || source.shader == null || !source.shader.isSupported ||
                    source.shader.name.Contains("InternalErrorShader");
                string materialName = source != null ? source.name.ToLowerInvariant() : "";
                bool glass = materialName.Contains("glass") || materialName.Contains("kaca");
                Color sourceColor = ReadColor(source);
                bool bodyPanel = materialName == "bodi" || materialName == "body" || materialName == "karoseri";
                // Missing source textures leave many imported panels untextured and
                // near-white. Paint those (plus named body panels) with the fallback
                // colour so the bus is not a flat white shell. Glass is excluded by
                // the guard below and keeps its transparency.
                bool untextured = ReadTexture(source) == null;
                // The shared helper also catches the FBX importer's
                // (1,1,1,0) white for missing textures, which an opaque shader
                // renders as flat white.
                bool whiteShell = ImportedMaterialPalette.IsUntexturedWhite(source);
                bool fallbackPaint = useFallbackBodyColor && untextured && (bodyPanel || whiteShell);
                if ((!invalid && !glass && !fallbackPaint) || targetShader == null) continue;

                if (source != null && replacements.TryGetValue(source, out Material cached))
                {
                    materials[i] = cached;
                    changed = true;
                    continue;
                }

                Material replacement = !invalid ? new Material(source) : new Material(targetShader);
                replacement.name = (source != null ? source.name : renderer.name) + " (Runtime)";
                Color color = fallbackPaint ? fallbackBodyColor : sourceColor;
                color.a = glass ? Mathf.Clamp(color.a, 0.22f, 0.55f) : Mathf.Max(color.a, 0.9f);
                // Write the Built-in albedo slot, and set .color as well so a shader
                // that only exposes the property still gets the intended colour.
                replacement.color = color;
                if (replacement.HasProperty("_Color")) replacement.SetColor("_Color", color);
                // A non-glass panel keeps whatever opaque mode it was imported with;
                // glass is switched properly in ConfigureGlass.
                Texture texture = ReadTexture(source);
                if (texture != null && replacement.HasProperty("_MainTex"))
                {
                    replacement.SetTexture("_MainTex", texture);
                    replacement.SetTextureScale("_MainTex", source.GetTextureScale("_MainTex"));
                    replacement.SetTextureOffset("_MainTex", source.GetTextureOffset("_MainTex"));
                }
                if (replacement.HasProperty("_Glossiness")) replacement.SetFloat("_Glossiness", glass ? 0.82f : 0.34f);
                if (replacement.HasProperty("_Smoothness")) replacement.SetFloat("_Smoothness", glass ? 0.82f : 0.34f);
                if (glass) ConfigureGlass(replacement);

                runtimeMaterials.Add(replacement);
                if (source != null) replacements[source] = replacement;
                materials[i] = replacement;
                changed = true;
                repaired++;
            }
            if (changed) renderer.sharedMaterials = materials;
        }

        if (visualRoot == null)
            repairApplied = true;
        // Keep the hierarchy on the palette's late-repair list so the world can
        // be swept again once every model has finished spawning.
        if (repaired > 0)
            ImportedMaterialPalette.RegisterImportedRoot(root, fallbackBodyColor);
        Debug.Log($"ImportedVehicleVisualRepair: '{root.name}' has {renderers.Length} renderers; repaired {repaired} material slots for the Built-in Render Pipeline.");
        return renderers.Length;
    }

    static Color ReadColor(Material source)
    {
        if (source == null) return new Color(0.82f, 0.84f, 0.86f, 1f);
        if (source.HasProperty("_Color")) return source.GetColor("_Color");
        return Color.white;
    }

    /// <summary>
    /// Switches a Built-in material to alpha-blended glass: Fade mode (2), the
    /// SrcAlpha/OneMinusSrcAlpha pair, ZWrite off, the transparent queue and the
    /// keyword the Standard shader keys its blend variants on.
    /// </summary>
    public static void ConfigureGlass(Material material)
    {
        if (material.HasProperty("_Mode")) material.SetFloat("_Mode", 2f);
        if (material.HasProperty("_SrcBlend")) material.SetFloat("_SrcBlend", (float)BlendMode.SrcAlpha);
        if (material.HasProperty("_DstBlend")) material.SetFloat("_DstBlend", (float)BlendMode.OneMinusSrcAlpha);
        if (material.HasProperty("_ZWrite")) material.SetFloat("_ZWrite", 0f);
        material.SetOverrideTag("RenderType", "Transparent");
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.renderQueue = (int)RenderQueue.Transparent;
    }

    static Texture ReadTexture(Material source)
    {
        if (source == null || !source.HasProperty("_MainTex")) return null;
        return source.GetTexture("_MainTex");
    }

    // Only pass an imported visual subtree, never the player root: gameplay
    // cameras and their listener are legitimate siblings of that subtree.
    public static void DisableEmbeddedCameras(Transform visualRoot)
    {
        if (visualRoot == null) return;
        foreach (Camera camera in visualRoot.GetComponentsInChildren<Camera>(true)) camera.enabled = false;
        foreach (AudioListener listener in visualRoot.GetComponentsInChildren<AudioListener>(true)) listener.enabled = false;
    }

    void OnDestroy()
    {
        for (int i = 0; i < runtimeMaterials.Count; i++)
            if (runtimeMaterials[i] != null) Destroy(runtimeMaterials[i]);
    }
}
