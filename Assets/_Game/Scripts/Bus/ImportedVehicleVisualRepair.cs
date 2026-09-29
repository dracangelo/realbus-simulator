using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Makes embedded FBX materials usable after moving the project from the
/// Built-in Render Pipeline to URP. It is deliberately runtime-safe so newly
/// generated bus prefabs work without manual material extraction.
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
        // invoking RepairNow, leaving Built-in pipeline materials grey in URP.
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

        bool urpActive = GraphicsSettings.currentRenderPipeline != null;
        Shader targetShader = Shader.Find(urpActive ? "Universal Render Pipeline/Lit" : "Standard");
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
                bool legacyInUrp = urpActive && source != null && source.shader != null &&
                    !source.shader.name.StartsWith("Universal Render Pipeline/");
                string materialName = source != null ? source.name.ToLowerInvariant() : "";
                bool glass = materialName.Contains("glass") || materialName.Contains("kaca");
                Color sourceColor = ReadColor(source);
                bool bodyPanel = materialName == "bodi" || materialName == "body" || materialName == "karoseri";
                bool fallbackPaint = useFallbackBodyColor && bodyPanel && ReadTexture(source) == null &&
                    Mathf.Abs(sourceColor.r - sourceColor.g) < .03f && Mathf.Abs(sourceColor.g - sourceColor.b) < .03f && sourceColor.r > .2f;
                if ((!invalid && !legacyInUrp && !glass && !fallbackPaint) || targetShader == null) continue;

                if (source != null && replacements.TryGetValue(source, out Material cached))
                {
                    materials[i] = cached;
                    changed = true;
                    continue;
                }

                Material replacement = !invalid && !legacyInUrp ? new Material(source) : new Material(targetShader);
                replacement.name = (source != null ? source.name : renderer.name) + " (Runtime)";
                Color color = fallbackPaint ? fallbackBodyColor : sourceColor;
                color.a = glass ? Mathf.Clamp(color.a, 0.22f, 0.55f) : Mathf.Max(color.a, 0.9f);
                replacement.SetColor(replacement.HasProperty("_BaseColor") ? "_BaseColor" : "_Color", color);
                Texture texture = ReadTexture(source);
                if (texture != null)
                {
                    string destination = replacement.HasProperty("_BaseMap") ? "_BaseMap" : "_MainTex";
                    string original = source.HasProperty("_BaseMap") && source.GetTexture("_BaseMap") != null ? "_BaseMap" : "_MainTex";
                    replacement.SetTexture(destination, texture);
                    replacement.SetTextureScale(destination, source.GetTextureScale(original));
                    replacement.SetTextureOffset(destination, source.GetTextureOffset(original));
                }
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
        Debug.Log($"ImportedVehicleVisualRepair: '{root.name}' has {renderers.Length} renderers; repaired {repaired} material slots for the active render pipeline.");
        return renderers.Length;
    }

    static Color ReadColor(Material source)
    {
        if (source == null) return new Color(0.82f, 0.84f, 0.86f, 1f);
        if (source.HasProperty("_BaseColor")) return source.GetColor("_BaseColor");
        if (source.HasProperty("_Color")) return source.GetColor("_Color");
        return Color.white;
    }

    public static void ConfigureGlass(Material material)
    {
        if (material.HasProperty("_Surface")) material.SetFloat("_Surface", 1f);
        if (material.HasProperty("_Mode")) material.SetFloat("_Mode", 2f);
        if (material.HasProperty("_SrcBlend")) material.SetFloat("_SrcBlend", (float)BlendMode.SrcAlpha);
        if (material.HasProperty("_DstBlend")) material.SetFloat("_DstBlend", (float)BlendMode.OneMinusSrcAlpha);
        if (material.HasProperty("_ZWrite")) material.SetFloat("_ZWrite", 0f);
        material.SetOverrideTag("RenderType", "Transparent");
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.EnableKeyword(material.HasProperty("_Surface") ? "_SURFACE_TYPE_TRANSPARENT" : "_ALPHABLEND_ON");
        material.renderQueue = (int)RenderQueue.Transparent;
    }

    static Texture ReadTexture(Material source)
    {
        if (source == null) return null;
        if (source.HasProperty("_BaseMap") && source.GetTexture("_BaseMap") != null) return source.GetTexture("_BaseMap");
        if (source.HasProperty("_MainTex")) return source.GetTexture("_MainTex");
        return null;
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
