#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Repairs texture slots that Unity leaves empty when an FBX material name does
/// not match the artwork filename. Only missing slots are touched; materials that
/// already carry a texture are left on their original asset.
/// </summary>
public static class ImportedModelTextureBinder
{
    static readonly Dictionary<string, Dictionary<string, string>> EmbeddedSets =
        new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

    public static void Bind(GameObject root, string sourcePath, string materialFolder, string stem)
    {
        EnsureFolder(materialFolder);
        var converted = new Dictionary<Material, Material>();
        int index = 0;
        foreach (Renderer renderer in root.GetComponentsInChildren<Renderer>(true))
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

                bool skipBase = IsGlass(original.name) || IsGlass(renderer.name);
                Texture baseTexture = original.HasProperty("_MainTex") ? original.GetTexture("_MainTex") : null;
                Texture normalTexture = original.HasProperty("_BumpMap") ? original.GetTexture("_BumpMap") : null;
                bool needsBase = !skipBase && baseTexture == null;
                bool needsNormal = !skipBase && normalTexture == null;
                if (!needsBase && !needsNormal) continue;

                if (needsBase) baseTexture = FindBaseTexture(sourcePath, original.name, renderer.name);
                if (needsNormal) normalTexture = FindNormalTexture(sourcePath, original.name);
                if (baseTexture == null && normalTexture == null) continue;

                Material copy = new Material(original);
                copy.name = original.name;
                if (baseTexture != null)
                {
                    copy.SetTexture("_MainTex", baseTexture);
                    if (copy.HasProperty("_Color")) copy.SetColor("_Color", Color.white);
                }
                if (normalTexture != null)
                {
                    copy.SetTexture("_BumpMap", normalTexture);
                    copy.EnableKeyword("_NORMALMAP");
                }
                if (copy.shader == null || copy.shader.name.IndexOf("Autodesk", StringComparison.OrdinalIgnoreCase) >= 0)
                    copy.shader = BuiltInShaders.Lit;

                string materialPath = $"{materialFolder}/{stem}_{index++}.mat";
                Material saved = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
                if (saved == null)
                {
                    AssetDatabase.CreateAsset(copy, materialPath);
                    saved = copy;
                }
                else
                {
                    EditorUtility.CopySerialized(copy, saved);
                    UnityEngine.Object.DestroyImmediate(copy);
                    EditorUtility.SetDirty(saved);
                }
                materials[i] = saved;
                converted[original] = saved;
                changed = true;
            }
            if (changed) renderer.sharedMaterials = materials;
        }
    }

    static Texture FindBaseTexture(string sourcePath, string materialName, string rendererName)
    {
        Texture found = LiveCityModelImporter.FindTexture(sourcePath, materialName, false);
        if (found != null) return found;

        // The old Travego body has no UV-named artwork in the FBX. Its folder
        // carries alternative livery wraps, so use the first one for the body.
        string lowerMaterial = (materialName ?? string.Empty).ToLowerInvariant();
        string lowerRenderer = (rendererName ?? string.Empty).ToLowerInvariant();
        if (lowerMaterial.Contains("bodi") || lowerMaterial.Contains("body") ||
            lowerRenderer == "body" || lowerRenderer.Contains("bodi"))
        {
            found = FindNamedTexture(sourcePath, "livery");
            if (found != null) return found;
        }

        // Mirrors and some trim parts are named semantically rather than by their
        // texture set. Chrome is the closest safe match for a mirror surface.
        if (lowerMaterial.Contains("spion") || lowerMaterial.Contains("mirror"))
            return FindNamedTexture(sourcePath, "chrome");

        // Bus-stop frames use a generic "BusStop" material for bolts, pillars and
        // bench supports. A neutral metal map reads correctly on all of them.
        if (lowerMaterial.Replace(" ", string.Empty).Contains("busstop"))
            return FindNamedTexture(sourcePath, "metal");

        // Some FBX exports keep generic aiStandardSurface material names while the
        // texture set names live only in the embedded FBX paths. Pair the material
        // list with the base-colour path list in file order when the counts match.
        if (TryEmbeddedTextureSet(sourcePath, materialName, out string embeddedBase, out _))
            return AssetDatabase.LoadAssetAtPath<Texture>(embeddedBase);
        return null;
    }

    static Texture FindNormalTexture(string sourcePath, string materialName)
    {
        Texture found = LiveCityModelImporter.FindTexture(sourcePath, materialName, true);
        if (found != null) return found;
        if (TryEmbeddedTextureSet(sourcePath, materialName, out _, out string normalPath) && normalPath != null)
            return AssetDatabase.LoadAssetAtPath<Texture>(normalPath);
        return null;
    }

    static Texture FindNamedTexture(string sourcePath, string token)
    {
        foreach (string file in LiveCityModelImporter.TextureFiles(sourcePath)
                     .OrderBy(file => file, StringComparer.OrdinalIgnoreCase))
        {
            if (Path.GetFileNameWithoutExtension(file).IndexOf(token, StringComparison.OrdinalIgnoreCase) >= 0)
                return AssetDatabase.LoadAssetAtPath<Texture>(file.Replace('\\', '/'));
        }
        return null;
    }

    static bool TryEmbeddedTextureSet(string sourcePath, string materialName, out string basePath, out string normalPath)
    {
        basePath = null;
        normalPath = null;
        if (string.IsNullOrEmpty(materialName)) return false;
        Dictionary<string, string> map = EmbeddedMap(sourcePath);
        if (!map.TryGetValue(materialName, out basePath)) return false;

        string normalCandidate = basePath.Replace("_BaseColor", "_Normal");
        if (File.Exists(normalCandidate)) normalPath = normalCandidate;
        return true;
    }

    static Dictionary<string, string> EmbeddedMap(string sourcePath)
    {
        if (EmbeddedSets.TryGetValue(sourcePath, out var cached)) return cached;
        var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        EmbeddedSets[sourcePath] = map;
        if (!File.Exists(sourcePath)) return map;

        var materials = new List<string>();
        var textures = new List<string>();
        foreach (string text in AsciiStrings(File.ReadAllBytes(sourcePath)))
        {
            Match material = Regex.Match(text, @"\baiStandardSurface\d+\b");
            if (material.Success && !materials.Contains(material.Value))
                materials.Add(material.Value);

            Match texture = Regex.Match(text, @"BUSTEXTURE[\\/].*?_BaseColor\.(?:png|jpg|jpeg|tga|exr)", RegexOptions.IgnoreCase);
            if (!texture.Success) continue;
            string path = ResolveTexturePath(sourcePath, texture.Value);
            if (path != null && !textures.Contains(path)) textures.Add(path);
        }

        if (materials.Count == 0 || materials.Count != textures.Count) return map;
        for (int i = 0; i < materials.Count; i++) map[materials[i]] = textures[i];
        return map;
    }

    static string ResolveTexturePath(string sourcePath, string embeddedPath)
    {
        string fileName = Path.GetFileName(embeddedPath.Replace('\\', '/'));
        string folder = Path.GetDirectoryName(sourcePath);
        if (string.IsNullOrEmpty(folder) || !Directory.Exists(folder)) return null;
        foreach (string file in Directory.GetFiles(folder, fileName, SearchOption.AllDirectories))
            return file.Replace('\\', '/');
        return null;
    }

    static IEnumerable<string> AsciiStrings(byte[] bytes)
    {
        var current = new StringBuilder();
        foreach (byte value in bytes)
        {
            if (value >= 32 && value <= 126)
            {
                current.Append((char)value);
                continue;
            }
            if (current.Length >= 6)
            {
                string text = current.ToString();
                if (text.IndexOf("aiStandardSurface", StringComparison.Ordinal) >= 0 ||
                    text.IndexOf("BUSTEXTURE", StringComparison.Ordinal) >= 0)
                    yield return text;
            }
            current.Clear();
        }
        if (current.Length >= 6)
        {
            string text = current.ToString();
            if (text.IndexOf("aiStandardSurface", StringComparison.Ordinal) >= 0 ||
                text.IndexOf("BUSTEXTURE", StringComparison.Ordinal) >= 0)
                yield return text;
        }
    }

    static bool IsGlass(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        string lower = name.ToLowerInvariant();
        return lower.Contains("glass") || lower.Contains("kaca") || lower.Contains("window") ||
               lower.Contains("windshield");
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
