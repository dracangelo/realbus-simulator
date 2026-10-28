#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// The gas-station FBX ships Autodesk Interactive materials with no texture
/// slots. Its maps sit beside the model in per-material folders, so this binder
/// maps the Turkish material names onto those maps and converts the result to
/// the Built-in Standard shader the project renders with.
/// </summary>
public static class GasStationMaterialBinder
{
    const string SourceFolder = "Assets/unity models/gasstation/gas+station+fbx";
    const string AsphaltAlbedo = SourceFolder + "/Asphalt/Road012A_1K-JPG_Color.jpg";
    const string AsphaltNormal = SourceFolder + "/Asphalt/Road012A_1K-JPG_NormalGL.jpg";
    const string MetalAlbedo = SourceFolder + "/Boyalı Metal/Metal028_1K-JPG_Color.jpg";
    const string MetalNormal = SourceFolder + "/Boyalı Metal/Metal028_1K-JPG_NormalGL.jpg";
    const string RubberAlbedo = SourceFolder + "/Kauçuk/Rubber004_1K-JPG_Color.jpg";
    const string RubberNormal = SourceFolder + "/Kauçuk/Rubber004_1K-JPG_NormalGL.jpg";

    public static void Bind(GameObject root, string materialFolder, string stem)
    {
        if (root == null) return;
        EnsureFolder(materialFolder);
        EnsureNormalMap(AsphaltNormal);
        EnsureNormalMap(MetalNormal);
        EnsureNormalMap(RubberNormal);

        var converted = new Dictionary<Material, Material>();
        int index = 0;
        foreach (var renderer in root.GetComponentsInChildren<Renderer>(true))
        {
            Material[] materials = renderer.sharedMaterials;
            bool changed = false;
            for (int i = 0; i < materials.Length; i++)
            {
                Material original = materials[i];
                if (original == null) continue;
                if (converted.TryGetValue(original, out Material cached))
                {
                    materials[i] = cached;
                    changed = true;
                    continue;
                }

                var copy = new Material(original) { name = original.name };
                if (copy.shader == null || copy.shader.name.IndexOf("Autodesk", System.StringComparison.OrdinalIgnoreCase) >= 0)
                    copy.shader = BuiltInShaders.Lit;
                copy.enableInstancing = true;
                ApplyMaps(copy);

                string path = $"{materialFolder}/{stem}_{index++}.mat";
                if (AssetDatabase.LoadAssetAtPath<Material>(path) != null)
                    AssetDatabase.DeleteAsset(path);
                AssetDatabase.CreateAsset(copy, path);
                converted[original] = copy;
                materials[i] = copy;
                changed = true;
            }
            if (changed) renderer.sharedMaterials = materials;
        }
    }

    static void ApplyMaps(Material material)
    {
        string name = (material.name ?? string.Empty).ToLowerInvariant();
        if (name.Contains("asfalt") || name.Contains("asphalt") || name.Contains("road"))
        {
            SetAlbedo(material, AsphaltAlbedo, AsphaltNormal, resetColor: true);
            SetSmoothness(material, .16f, 0f);
        }
        else if (name.Contains("kauçuk") || name.Contains("kaucuk") || name.Contains("rubber"))
        {
            SetAlbedo(material, RubberAlbedo, RubberNormal, resetColor: true);
            SetSmoothness(material, .12f, 0f);
        }
        else if (name.Contains("metal") && !name.Contains("siyah"))
        {
            // Keep the artist's paint tint: Standard multiplies the map by _Color.
            SetAlbedo(material, MetalAlbedo, MetalNormal, resetColor: false);
            SetSmoothness(material, .42f, .65f);
        }
        else if (name.Contains("cam") || name.Contains("glass"))
        {
            MakeTransparent(material);
        }
        else
        {
            SetSmoothness(material, .30f, .05f);
        }
    }

    static void SetAlbedo(Material material, string albedoPath, string normalPath, bool resetColor)
    {
        var albedo = AssetDatabase.LoadAssetAtPath<Texture2D>(albedoPath);
        if (albedo != null)
        {
            material.SetTexture("_MainTex", albedo);
            if (resetColor && material.HasProperty("_Color")) material.SetColor("_Color", Color.white);
        }
        var normal = AssetDatabase.LoadAssetAtPath<Texture2D>(normalPath);
        if (normal != null)
        {
            material.SetTexture("_BumpMap", normal);
            material.EnableKeyword("_NORMALMAP");
        }
    }

    static void SetSmoothness(Material material, float smoothness, float metallic)
    {
        if (material.HasProperty("_Glossiness")) material.SetFloat("_Glossiness", smoothness);
        if (material.HasProperty("_Metallic")) material.SetFloat("_Metallic", metallic);
    }

    static void MakeTransparent(Material material)
    {
        if (material.HasProperty("_Mode")) material.SetFloat("_Mode", 3f);
        if (material.HasProperty("_SrcBlend")) material.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
        if (material.HasProperty("_DstBlend")) material.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
        if (material.HasProperty("_ZWrite")) material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)RenderQueue.Transparent;
    }

    static void EnsureNormalMap(string path)
    {
        var importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if (importer == null || importer.textureType == TextureImporterType.NormalMap) return;
        importer.textureType = TextureImporterType.NormalMap;
        importer.SaveAndReimport();
    }

    static void EnsureFolder(string folder)
    {
        folder = folder.Replace('\\', '/');
        if (AssetDatabase.IsValidFolder(folder)) return;
        string parent = Path.GetDirectoryName(folder)?.Replace('\\', '/');
        if (!string.IsNullOrEmpty(parent)) EnsureFolder(parent);
        AssetDatabase.CreateFolder(parent, Path.GetFileName(folder));
    }
}
#endif
