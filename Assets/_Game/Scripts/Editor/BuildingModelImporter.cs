#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

/// <summary>
/// Turns the downloaded building FBX packs into normalised, textured prefabs.
///
/// The packs are a mixture of Sketchfab exports: some ship a full PBR bake next
/// to the model, some use material names that match loose textures in the pack
/// root, and several are whole-city scenes or untextured dummy shells. This
/// importer keeps only plausible single buildings, resolves their textures by
/// name, saves the materials as real assets and puts a bottom-centre pivot plus
/// a box collider on every prefab. Nothing under <c>Assets/unity models</c> is
/// modified apart from texture import settings.
/// </summary>
public static class BuildingModelImporter
{
    const string Source = "Assets/unity models/buildings";
    const string Output = "Assets/_Game/Resources/BuildingGenerated";
    const string MaterialOutput = Output + "/Materials";
    const string CatalogPath = "Assets/_Game/Resources/BuildingCatalog.asset";

    // The source packs contain 4K bakes. 2K keeps the facades crisp at the
    // distance the player sees them while keeping the streaming budget sane.
    const int MaxTextureSize = 2048;
    // One hero building can afford to be dense; skyscraper-02, the city-scene
    // dumps and the 1M-triangle high-rise are deliberately outside this band.
    const int MaxTriangles = 500000;

    [InitializeOnLoadMethod]
    static void QueueInitialImport()
    {
        EditorApplication.delayCall += () =>
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode && !File.Exists(CatalogPath)) Build();
        };
    }

    [MenuItem("Tools/RealBus/Setup/Build Building Models")]
    public static void Build()
    {
        if (!Directory.Exists(Source))
        {
            Debug.LogWarning($"[Buildings] No model folder at '{Source}'.");
            return;
        }

        EnsureFolder(Output);
        EnsureFolder(MaterialOutput);
        AssetDatabase.Refresh();

        var textures = IndexTextures(Source);
        ConfigureTextureImporters(textures);

        var entries = new List<BuildingCatalog.Entry>();
        var usedPrefabs = new HashSet<string>(StringComparer.Ordinal);
        var usedMaterials = new HashSet<string>(StringComparer.Ordinal);
        var fingerprints = new HashSet<string>(StringComparer.Ordinal);

        var paths = new List<string>();
        foreach (string path in Directory.GetFiles(Source, "*", SearchOption.TopDirectoryOnly))
            if (Path.GetExtension(path).Equals(".fbx", StringComparison.OrdinalIgnoreCase))
                paths.Add(path.Replace('\\', '/'));
        paths.Sort(StringComparer.Ordinal);

        foreach (string path in paths)
        {
            GameObject source = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (source == null) continue;

            string sourceName = Path.GetFileNameWithoutExtension(path);
            if (sourceName.Equals("10_f_buildind", StringComparison.OrdinalIgnoreCase))
            {
                // Same geometry as 10_f_building with every material slot doubled.
                Debug.Log("[Buildings] Skipped '10_f_buildind': duplicate of 10_f_building.");
                continue;
            }
            string fileStem = SafeName(sourceName);
            GameObject root = new GameObject(sourceName);
            root.name = fileStem;
            try
            {
                GameObject visual = Object.Instantiate(source, root.transform, false);
                visual.name = "Visual";
                StripNonBuildingComponents(visual);

                var renderers = root.GetComponentsInChildren<Renderer>(true);
                if (renderers.Length == 0) continue;

                Bounds bounds = BoundsOf(renderers);
                int triangles = TrianglesOf(root);
                if (!IsPlausibleBuilding(bounds, triangles, path)) continue;

                var materialNames = new List<string>();
                foreach (var renderer in renderers)
                    foreach (var material in renderer.sharedMaterials)
                        materialNames.Add(material != null ? material.name : "NULL");
                materialNames.Sort(StringComparer.Ordinal);
                string fingerprint = Fingerprint(bounds, triangles, materialNames);
                if (!fingerprints.Add(fingerprint))
                {
                    Debug.Log($"[Buildings] Skipped duplicate model '{sourceName}'.");
                    continue;
                }

                // The packs store their pivots wherever the artist left them, often
                // far from the geometry. Put the bottom centre at the origin so the
                // infill can place the prefab exactly in a lot.
                visual.transform.position -= new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);
                bounds = BoundsOf(renderers);

                var materialMap = new Dictionary<Material, Material>();
                var modelMaterials = new List<string>();
                Material fallback = null;
                int materialIndex = 0;
                int texturedMaterials = 0;
                int totalMaterials = 0;
                foreach (var renderer in renderers)
                {
                    var materials = renderer.sharedMaterials;
                    for (int i = 0; i < materials.Length; i++)
                    {
                        Material original = materials[i];
                        Material converted;
                        if (original != null && materialMap.TryGetValue(original, out var existing))
                        {
                            materials[i] = existing;
                            continue;
                        }
                        if (original == null && fallback != null)
                        {
                            materials[i] = fallback;
                            continue;
                        }

                        converted = MakeMaterial(original, sourceName, textures);
                        totalMaterials++;
                        if (converted.mainTexture != null) texturedMaterials++;

                        string materialPath = $"{MaterialOutput}/{fileStem}_{materialIndex++}.mat";
                        string assetName = Path.GetFileNameWithoutExtension(materialPath);
                        // Unity warns when a saved object's name does not match the
                        // asset filename. Keep both in sync so an import run is quiet.
                        converted.name = assetName;
                        var saved = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
                        if (saved == null)
                        {
                            AssetDatabase.CreateAsset(converted, materialPath);
                            saved = converted;
                        }
                        else
                        {
                            EditorUtility.CopySerialized(converted, saved);
                            saved.name = assetName;
                            Object.DestroyImmediate(converted);
                            EditorUtility.SetDirty(saved);
                        }
                        modelMaterials.Add(materialPath);
                        materials[i] = saved;
                        if (original != null) materialMap[original] = saved;
                        else fallback = saved;
                    }
                    renderer.sharedMaterials = materials;
                    renderer.shadowCastingMode = ShadowCastingMode.On;
                    renderer.receiveShadows = true;
                }

                // A building with no albedo anywhere is one of the untextured dummy
                // shells (Apartment Building, Only_Building, the fbx1 scene). A model
                // with only a couple of correct materials among many is the same
                // problem with a few defaults left behind: it adds grey boxes, not
                // life, so leave those out too.
                if (totalMaterials == 0 || texturedMaterials * 2 < totalMaterials)
                {
                    foreach (string materialPath in modelMaterials) AssetDatabase.DeleteAsset(materialPath);
                    Debug.Log($"[Buildings] Skipped under-textured model '{sourceName}' ({texturedMaterials}/{totalMaterials} materials).");
                    continue;
                }
                usedMaterials.UnionWith(modelMaterials);

                var collider = root.GetComponent<BoxCollider>();
                if (collider == null) collider = root.AddComponent<BoxCollider>();
                collider.center = bounds.center;
                collider.size = bounds.size;
                collider.isTrigger = false;

                string prefabPath = $"{Output}/{fileStem}.prefab";
                var prefab = PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
                if (prefab == null) continue;
                usedPrefabs.Add(prefabPath);
                entries.Add(new BuildingCatalog.Entry
                {
                    prefab = prefab,
                    bounds = bounds,
                    triangles = triangles,
                    sourceName = sourceName
                });
                Debug.Log($"[Buildings] Imported '{sourceName}': {bounds.size.x:F1}x{bounds.size.y:F1}x{bounds.size.z:F1} m, " +
                          $"{triangles} triangles, {texturedMaterials}/{totalMaterials} textured materials.");
            }
            finally
            {
                Object.DestroyImmediate(root);
            }
        }

        DeleteStaleAssets(Output, ".prefab", usedPrefabs);
        DeleteStaleAssets(MaterialOutput, ".mat", usedMaterials);

        entries.Sort((a, b) => string.CompareOrdinal(a.sourceName, b.sourceName));
        var catalog = AssetDatabase.LoadAssetAtPath<BuildingCatalog>(CatalogPath);
        if (catalog == null)
        {
            catalog = ScriptableObject.CreateInstance<BuildingCatalog>();
            AssetDatabase.CreateAsset(catalog, CatalogPath);
        }
        // The city pack appends its buildings to this catalog from another output
        // folder. Rebuilding the original building pack must not delete them.
        var preserved = new List<BuildingCatalog.Entry>();
        if (catalog.entries != null)
        {
            foreach (var entry in catalog.entries)
            {
                if (entry == null || entry.prefab == null) continue;
                string path = AssetDatabase.GetAssetPath(entry.prefab);
                if (path.StartsWith(Output + "/", StringComparison.OrdinalIgnoreCase)) continue;
                preserved.Add(entry);
            }
        }
        preserved.AddRange(entries);
        preserved.Sort((a, b) => string.CompareOrdinal(a.sourceName, b.sourceName));
        catalog.entries = preserved.ToArray();
        EditorUtility.SetDirty(catalog);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"[Buildings] Prepared {entries.Count} textured building models in '{Output}'.");
    }

    static void StripNonBuildingComponents(GameObject visual)
    {
        foreach (var component in visual.GetComponentsInChildren<Camera>(true)) Object.DestroyImmediate(component);
        foreach (var component in visual.GetComponentsInChildren<AudioSource>(true)) Object.DestroyImmediate(component);
        foreach (var component in visual.GetComponentsInChildren<AudioListener>(true)) Object.DestroyImmediate(component);
        foreach (var component in visual.GetComponentsInChildren<Collider>(true)) Object.DestroyImmediate(component);
        foreach (var component in visual.GetComponentsInChildren<Rigidbody>(true)) Object.DestroyImmediate(component);
        foreach (var component in visual.GetComponentsInChildren<Light>(true)) Object.DestroyImmediate(component);
        foreach (var component in visual.GetComponentsInChildren<Animator>(true)) Object.DestroyImmediate(component);
        foreach (var component in visual.GetComponentsInChildren<Animation>(true)) Object.DestroyImmediate(component);
    }

    static bool IsPlausibleBuilding(Bounds bounds, int triangles, string path)
    {
        Vector3 size = bounds.size;
        string name = Path.GetFileNameWithoutExtension(path);
        string reason = null;
        if (size.y < 5f || size.y > 260f) reason = $"height {size.y:F1} m";
        else if (Mathf.Min(size.x, size.z) < 3f) reason = $"footprint {size.x:F1}x{size.z:F1} m";
        else if (Mathf.Max(size.x, size.z) > 130f) reason = $"oversized footprint {size.x:F1}x{size.z:F1} m";
        else if (triangles < 300 || triangles > MaxTriangles) reason = $"{triangles} triangles";
        if (reason == null) return true;
        Debug.Log($"[Buildings] Skipped '{name}': {reason}.");
        return false;
    }

    static string Fingerprint(Bounds bounds, int triangles, List<string> materials)
    {
        return $"{Mathf.RoundToInt(bounds.size.x)}x{Mathf.RoundToInt(bounds.size.y)}x{Mathf.RoundToInt(bounds.size.z)}|" +
               $"{Mathf.RoundToInt(triangles / 1000f)}|{string.Join(",", materials)}";
    }

    static int TrianglesOf(GameObject root)
    {
        int total = 0;
        foreach (var filter in root.GetComponentsInChildren<MeshFilter>(true)) total += Triangles(filter.sharedMesh);
        foreach (var renderer in root.GetComponentsInChildren<SkinnedMeshRenderer>(true)) total += Triangles(renderer.sharedMesh);
        return total;
    }

    static int Triangles(Mesh mesh)
    {
        if (mesh == null) return 0;
        int total = 0;
        for (int i = 0; i < mesh.subMeshCount; i++) total += (int)mesh.GetIndexCount(i) / 3;
        return total;
    }

    static Bounds BoundsOf(Renderer[] renderers)
    {
        Bounds result = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++) result.Encapsulate(renderers[i].bounds);
        return result;
    }

    // ── Textures and materials ───────────────────────────────────────────────

    static Dictionary<string, string> IndexTextures(string root)
    {
        var map = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (string path in Directory.GetFiles(root, "*", SearchOption.AllDirectories))
        {
            string extension = Path.GetExtension(path).ToLowerInvariant();
            if (extension != ".png" && extension != ".jpg" && extension != ".jpeg" &&
                extension != ".tif" && extension != ".tga") continue;
            string key = Key(Path.GetFileNameWithoutExtension(path));
            if (key.Length == 0) continue;
            string normalized = path.Replace('\\', '/');
            if (!map.TryGetValue(key, out var existing))
            {
                map[key] = normalized;
                continue;
            }
            // A PNG with the same stem wins over a JPG/TIF duplicate.
            if (Path.GetExtension(existing).Equals(".png", StringComparison.OrdinalIgnoreCase) &&
                !extension.Equals(".png", StringComparison.Ordinal)) continue;
            if (extension.Equals(".png", StringComparison.Ordinal) ||
                !Path.GetExtension(existing).Equals(".png", StringComparison.OrdinalIgnoreCase))
                map[key] = normalized;
        }
        return map;
    }

    static void ConfigureTextureImporters(Dictionary<string, string> textures)
    {
        foreach (var path in textures.Values)
        {
            var importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer == null) continue;
            string key = Key(Path.GetFileNameWithoutExtension(path));
            bool normal = key.Contains("normal");
            bool linear = key.Contains("roughness") || key.Contains("metal") ||
                          key.Contains("ambientocclusion") || key.Contains("ao") ||
                          key.Contains("height") || key.Contains("displacement") ||
                          key.Contains("transmission") || key.Contains("opacity") ||
                          key.Contains("alpha") || key.Contains("mask");
            bool sRgb = !normal && !linear;

            bool changed = false;
            var type = normal ? TextureImporterType.NormalMap : TextureImporterType.Default;
            if (importer.textureType != type) { importer.textureType = type; changed = true; }
            if (importer.sRGBTexture != sRgb) { importer.sRGBTexture = sRgb; changed = true; }
            if (importer.maxTextureSize != MaxTextureSize) { importer.maxTextureSize = MaxTextureSize; changed = true; }
            if (!importer.mipmapEnabled) { importer.mipmapEnabled = true; changed = true; }
            if (importer.textureCompression != TextureImporterCompression.CompressedHQ)
            {
                importer.textureCompression = TextureImporterCompression.CompressedHQ;
                changed = true;
            }
            if (sRgb && !importer.alphaIsTransparency)
            {
                importer.alphaIsTransparency = true;
                changed = true;
            }
            if (changed) importer.SaveAndReimport();
        }
    }

    static Material MakeMaterial(Material source, string modelStem, Dictionary<string, string> textures)
    {
        var result = new Material(BuiltInShaders.Lit);
        string materialName = source != null ? source.name : "Building";
        string lower = materialName.ToLowerInvariant();
        result.name = materialName;

        string diffusePath = FindTexture(textures, modelStem, materialName, TextureChannel.Diffuse);
        string normalPath = FindTexture(textures, modelStem, materialName, TextureChannel.Normal);
        string occlusionPath = FindTexture(textures, modelStem, materialName, TextureChannel.Occlusion);
        string roughnessPath = FindTexture(textures, modelStem, materialName, TextureChannel.Roughness);

        Texture sourceDiffuse = source != null && source.HasProperty("_MainTex") ? source.GetTexture("_MainTex") : null;
        Texture sourceNormal = source != null && source.HasProperty("_BumpMap") ? source.GetTexture("_BumpMap") : null;
        Texture sourceOcclusion = source != null && source.HasProperty("_OcclusionMap") ? source.GetTexture("_OcclusionMap") : null;

        Texture diffuse = LoadTexture(diffusePath) ?? sourceDiffuse;
        Texture normal = LoadTexture(normalPath) ?? sourceNormal;
        Texture occlusion = LoadTexture(occlusionPath) ?? sourceOcclusion;

        Color color = source != null && source.HasProperty("_Color") ? source.color : Color.white;
        color.a = 1f;
        if (diffuse != null)
        {
            result.SetTexture("_MainTex", diffuse);
            result.SetColor("_Color", Color.white);
            if (source != null && source.HasProperty("_MainTex"))
            {
                result.SetTextureScale("_MainTex", source.GetTextureScale("_MainTex"));
                result.SetTextureOffset("_MainTex", source.GetTextureOffset("_MainTex"));
            }
        }
        else
        {
            if (IsNeutral(color)) color = FallbackColor(lower);
            result.SetColor("_Color", color);
        }

        if (normal != null)
        {
            result.SetTexture("_BumpMap", normal);
            result.SetFloat("_BumpScale", 1f);
            result.EnableKeyword("_NORMALMAP");
        }
        if (occlusion != null)
        {
            result.SetTexture("_OcclusionMap", occlusion);
            result.SetFloat("_OcclusionStrength", .88f);
            result.EnableKeyword("_OCCLUSIONMAP");
        }

        // Imported packs mix materials named "concrete", "plaster", "brick",
        // "glass", "steel" and generic placeholders. Treat the name as a surface
        // class instead of giving every wall the same 0.32 gloss: concrete should
        // be matte, painted frames slightly smoother, metal reflective, and glass
        // the only surface that mirrors the sky strongly.
        SurfaceClass surface = ClassifySurface(lower);
        float smoothness = SmoothnessFor(surface);
        // A measured roughness map should not push clear glass below the 0.8
        // smoothness floor that makes a window read as glazed rather than dusty.
        if (roughnessPath != null && surface != SurfaceClass.Glass)
            smoothness = Mathf.Clamp(smoothness * .92f, .08f, .9f);
        result.SetFloat("_Metallic", MetallicFor(surface));
        result.SetFloat("_Glossiness", smoothness);
        if (result.HasProperty("_GlossyReflections"))
            result.SetFloat("_GlossyReflections", 1f);
        if (result.HasProperty("_BumpScale"))
            result.SetFloat("_BumpScale", .9f);
        result.enableInstancing = true;
        return result;
    }

    enum TextureChannel { Diffuse, Normal, Occlusion, Roughness }
    enum SurfaceClass { Masonry, Glass, Metal, Wood, Generic }

    static SurfaceClass ClassifySurface(string materialName)
    {
        if (materialName.Contains("glass") || materialName.Contains("window") ||
            materialName.Contains("transmission") || materialName.Contains("translucent"))
            return SurfaceClass.Glass;
        if (materialName.Contains("metal") || materialName.Contains("steel") ||
            materialName.Contains("aluminium") || materialName.Contains("aluminum"))
            return SurfaceClass.Metal;
        if (materialName.Contains("wood") || materialName.Contains("timber") ||
            materialName.Contains("door") || materialName.Contains("shutter"))
            return SurfaceClass.Wood;
        if (materialName.Contains("concrete") || materialName.Contains("plaster") ||
            materialName.Contains("render") || materialName.Contains("stucco") ||
            materialName.Contains("brick") || materialName.Contains("stone") ||
            materialName.Contains("masonry") || materialName.Contains("wall") ||
            materialName.Contains("tile"))
            return SurfaceClass.Masonry;
        return SurfaceClass.Generic;
    }

    static float MetallicFor(SurfaceClass surface)
        => surface == SurfaceClass.Metal ? .65f : 0f;

    static float SmoothnessFor(SurfaceClass surface)
    {
        switch (surface)
        {
            case SurfaceClass.Glass: return .86f;   // clear sky reflection, not a mirror
            case SurfaceClass.Metal: return .55f;   // galvanised/painted metal, not chrome
            case SurfaceClass.Wood: return .34f;
            case SurfaceClass.Masonry: return .22f; // concrete, brick and plaster
            default: return .28f;
        }
    }

    static string FindTexture(Dictionary<string, string> textures, string modelStem, string materialName, TextureChannel channel)
    {
        switch (channel)
        {
            case TextureChannel.Diffuse:
                return First(textures,
                    $"{modelStem}_Bake1_PBR_Diffuse",
                    $"{materialName}_BaseColor", $"{materialName}_Base_Color",
                    $"{materialName}_base_color", $"{materialName}_albedo",
                    $"{materialName}_Diffuse", $"{materialName}_diffuse",
                    materialName, modelStem);
            case TextureChannel.Normal:
                return First(textures,
                    $"{modelStem}_Bake1_PBR_Normal",
                    $"{materialName}_Normal", $"{materialName}_normal",
                    $"{materialName}normalmap", $"{materialName}_normalmap",
                    $"{modelStem}normalmap", $"{modelStem}_normalmap",
                    $"{modelStem}_Normal");
            case TextureChannel.Occlusion:
                return First(textures,
                    $"{modelStem}_Bake1_PBR_Ambient Occlusion",
                    $"{materialName}_AmbientOcclusion", $"{materialName}_AO",
                    $"{modelStem}_AmbientOcclusion", $"{modelStem}_AO");
            default:
                return First(textures,
                    $"{modelStem}_Bake1_PBR_Roughness",
                    $"{materialName}_Roughness", $"{materialName}_roughness",
                    $"{modelStem}_Roughness", $"{modelStem}_roughness");
        }
    }

    static string First(Dictionary<string, string> textures, params string[] candidates)
    {
        foreach (string candidate in candidates)
        {
            if (string.IsNullOrWhiteSpace(candidate)) continue;
            string key = Key(candidate);
            if (key.Length > 0 && textures.TryGetValue(key, out var path)) return path;
        }
        return null;
    }

    static Texture2D LoadTexture(string path)
        => string.IsNullOrEmpty(path) ? null : AssetDatabase.LoadAssetAtPath<Texture2D>(path);

    static bool IsNeutral(Color color)
        => Mathf.Abs(color.r - color.g) < .03f && Mathf.Abs(color.g - color.b) < .03f;

    static Color FallbackColor(string materialName)
    {
        if (materialName.Contains("glass") || materialName.Contains("window")) return new Color(.28f, .38f, .46f);
        if (materialName.Contains("metal") || materialName.Contains("steel")) return new Color(.34f, .35f, .36f);
        if (materialName.Contains("brick")) return new Color(.50f, .28f, .22f);
        if (materialName.Contains("wood")) return new Color(.43f, .30f, .20f);
        if (materialName.Contains("concrete") || materialName.Contains("plaster") || materialName.Contains("stucco"))
            return new Color(.60f, .58f, .54f);
        return new Color(.50f, .49f, .46f);
    }

    static string Key(string name)
    {
        var builder = new StringBuilder(name.Length);
        foreach (char c in name)
            if (char.IsLetterOrDigit(c)) builder.Append(char.ToLowerInvariant(c));
        return builder.ToString();
    }

    static string SafeName(string name)
    {
        var chars = name.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
            if (!char.IsLetterOrDigit(chars[i]) && chars[i] != '-' && chars[i] != '_') chars[i] = '_';
        return new string(chars).Trim('_');
    }

    static void EnsureFolder(string path)
    {
        if (AssetDatabase.IsValidFolder(path)) return;
        string parent = Path.GetDirectoryName(path);
        if (string.IsNullOrEmpty(parent)) return;
        parent = parent.Replace('\\', '/');
        EnsureFolder(parent);
        AssetDatabase.CreateFolder(parent, Path.GetFileName(path));
    }

    static void DeleteStaleAssets(string folder, string extension, HashSet<string> used)
    {
        if (!Directory.Exists(folder)) return;
        foreach (string path in Directory.GetFiles(folder, "*" + extension, SearchOption.TopDirectoryOnly))
        {
            string assetPath = path.Replace('\\', '/');
            if (!used.Contains(assetPath)) AssetDatabase.DeleteAsset(assetPath);
        }
    }
}
#endif
