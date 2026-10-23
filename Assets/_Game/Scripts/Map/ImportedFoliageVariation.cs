using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stable per-tree and per-plant colour variation for imported foliage.
///
/// The plant packs contain only a handful of prefabs, so a street planted with the
/// same tree at the same green reads as a copy-paste even when the model is good.
/// This helper selects one of four shared material variants and swaps it onto the
/// renderer instead of setting a per-renderer MaterialPropertyBlock. The variants
/// are shared and keep GPU instancing enabled, so varying every instance does not
/// silently turn thousands of trees into individual draws.
/// </summary>
public static class ImportedFoliageVariation
{
    public const int VariantCount = 4;

    static readonly Color[] Tints =
    {
        new Color(.88f, .99f, .84f, 1f), // cooler, younger leaves
        new Color(.97f, 1.05f, .90f, 1f), // yellow-green, sunlit
        new Color(1.05f, 1.01f, .86f, 1f), // warmer, drier
        new Color(.84f, .94f, .80f, 1f)  // deep shade
    };

    static readonly Dictionary<Material, Material[]> Variants = new Dictionary<Material, Material[]>();

    /// <summary>Deterministic variant for a placement seed.</summary>
    public static int VariantFor(uint seed)
    {
        unchecked
        {
            seed ^= seed >> 16;
            seed *= 0x7feb352d;
            seed ^= seed >> 15;
            seed *= 0x846ca68b;
            seed ^= seed >> 16;
            return (int)(seed % VariantCount);
        }
    }

    /// <summary>The multiplier applied to a leaf material's albedo.</summary>
    public static Color TintFor(uint seed) => Tints[VariantFor(seed)];

    /// <summary>
    /// Applies a shared colour variant to every foliage renderer under
    /// <paramref name="root"/>. Trunks, bark, branches and helper ground meshes keep
    /// their original material; only leaves and plant cards are tinted.
    /// </summary>
    public static void Apply(GameObject root, LiveCityCatalog.Kind kind, uint seed)
    {
        if (root == null || (kind != LiveCityCatalog.Kind.Tree && kind != LiveCityCatalog.Kind.Plant)) return;

        int variant = VariantFor(seed);
        foreach (Renderer renderer in root.GetComponentsInChildren<Renderer>(true))
        {
            if (renderer == null) continue;
            Material[] materials = renderer.sharedMaterials;
            bool changed = false;
            for (int i = 0; i < materials.Length; i++)
            {
                Material source = materials[i];
                if (!IsFoliage(source, renderer.name)) continue;
                Material replacement = GetVariant(source, variant);
                if (replacement == null || replacement == source) continue;
                materials[i] = replacement;
                changed = true;
            }
            if (changed) renderer.sharedMaterials = materials;
        }
    }

    static bool IsFoliage(Material material, string rendererName)
    {
        if (material == null) return false;
        string name = ((material.name ?? string.Empty) + " " + (rendererName ?? string.Empty)).ToLowerInvariant();
        if (name.Contains("bark") || name.Contains("trunk") || name.Contains("branch") ||
            name.Contains("stem") || name.Contains("ground"))
            return false;
        return true;
    }

    static Material GetVariant(Material source, int variant)
    {
        if (!Variants.TryGetValue(source, out Material[] set))
        {
            set = new Material[VariantCount];
            Variants[source] = set;
        }

        if (set[variant] != null) return set[variant];

        Material clone = Object.Instantiate(source);
        clone.name = source.name + " (foliage " + variant + ")";
        clone.hideFlags = HideFlags.DontSave;
        clone.enableInstancing = true;
        Color tint = Tints[variant];
        if (clone.HasProperty("_Color")) clone.SetColor("_Color", tint);
        if (clone.HasProperty("_BaseColor")) clone.SetColor("_BaseColor", tint);
        set[variant] = clone;
        return clone;
    }
}
