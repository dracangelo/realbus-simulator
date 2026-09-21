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
    readonly List<Material> runtimeMaterials = new List<Material>();

    public int RepairNow(Transform visualRoot = null)
    {
        Transform root = visualRoot != null ? visualRoot : transform;
        Renderer[] renderers = root.GetComponentsInChildren<Renderer>(true);
        if (renderers.Length == 0)
        {
            Debug.LogError($"ImportedVehicleVisualRepair: '{root.name}' has no renderers. Rerun Tools > RealBus > Wire Imported Models.");
            return 0;
        }

        bool urpActive = GraphicsSettings.currentRenderPipeline != null;
        Shader urpLit = Shader.Find("Universal Render Pipeline/Lit");
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
                if ((!invalid && !legacyInUrp) || urpLit == null) continue;

                if (source != null && replacements.TryGetValue(source, out Material cached))
                {
                    materials[i] = cached;
                    changed = true;
                    continue;
                }

                Material replacement = new Material(urpLit) { name = (source != null ? source.name : renderer.name) + " (URP Runtime)" };
                Color color = ReadColor(source);
                bool glass = renderer.name.ToLowerInvariant().Contains("glass") || (source != null && source.name.ToLowerInvariant().Contains("glass"));
                color.a = glass ? Mathf.Clamp(color.a, 0.22f, 0.55f) : Mathf.Max(color.a, 0.9f);
                replacement.SetColor("_BaseColor", color);
                Texture texture = ReadTexture(source);
                if (texture != null) replacement.SetTexture("_BaseMap", texture);
                replacement.SetFloat("_Smoothness", glass ? 0.82f : 0.34f);
                if (glass)
                {
                    replacement.SetFloat("_Surface", 1f);
                    replacement.SetFloat("_ZWrite", 0f);
                    replacement.renderQueue = 3000;
                }

                runtimeMaterials.Add(replacement);
                if (source != null) replacements[source] = replacement;
                materials[i] = replacement;
                changed = true;
                repaired++;
            }
            if (changed) renderer.sharedMaterials = materials;
        }

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

    static Texture ReadTexture(Material source)
    {
        if (source == null) return null;
        if (source.HasProperty("_BaseMap")) return source.GetTexture("_BaseMap");
        if (source.HasProperty("_MainTex")) return source.GetTexture("_MainTex");
        return null;
    }

    void OnDestroy()
    {
        for (int i = 0; i < runtimeMaterials.Count; i++)
            if (runtimeMaterials[i] != null) Destroy(runtimeMaterials[i]);
    }
}
