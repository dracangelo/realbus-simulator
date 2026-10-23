#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

/// <summary>Owns only LiveCityGenerated; never rewrites source FBXs or bus prefabs.</summary>
public static class LiveCityModelImporter
{
    const string Output = "Assets/_Game/Resources/LiveCityGenerated";
    const string CatalogPath = "Assets/_Game/Resources/LiveCityCatalog.asset";
    const string Person = "Assets/_Game/Resources/BussimAssets/passenger/uploads_files_6231172_woman+3d+fbx.Fbx";

    static readonly string[] SourceRoots =
    {
        "Assets/unity models/environment",
        "Assets/unity models/billboard",
        "Assets/unity models/busstop",
    };

    static readonly string[] TextureExtensions =
    {
        ".png", ".jpg", ".jpeg", ".tga", ".bmp", ".psd", ".exr", ".tif", ".tiff"
    };

    [InitializeOnLoadMethod]
    static void QueueInitialImport()
    {
        EditorApplication.delayCall += () =>
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode && NeedsBuild()) Build();
        };
    }

    [MenuItem("Tools/RealBus/Setup/Build Live City Models")]
    public static void Build()
    {
        Directory.CreateDirectory(Output);
        AssetDatabase.Refresh();
        List<SourceModel> models = CollectSources();
        ImportedModelImportSettings.EnsureStandard(models.Select(model => model.assetPath));
        var entries = new List<LiveCityCatalog.Entry>();
        foreach (SourceModel model in models)
        {
            string path = model.assetPath;
            if (!Classify(path, out var kind, out float height)) continue;
            GameObject source = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (source == null) continue;
            // Keep the original top-level GUID when a textured nested copy replaces
            // an older untextured model. Existing catalog entries and prefabs then
            // update in place instead of leaving duplicate scenery behind.
            string stem = AssetDatabase.AssetPathToGUID(model.identityPath);
            GameObject root = new GameObject(Path.GetFileNameWithoutExtension(path));
            try
            {
                GameObject visual = Object.Instantiate(source, root.transform);
                foreach (var cam in visual.GetComponentsInChildren<Camera>(true)) Object.DestroyImmediate(cam);
                foreach (var audio in visual.GetComponentsInChildren<AudioSource>(true)) Object.DestroyImmediate(audio);
                foreach (var listener in visual.GetComponentsInChildren<AudioListener>(true)) Object.DestroyImmediate(listener);
                foreach (var collider in visual.GetComponentsInChildren<Collider>(true)) Object.DestroyImmediate(collider);
                foreach (var body in visual.GetComponentsInChildren<Rigidbody>(true)) Object.DestroyImmediate(body);
                foreach (var light in visual.GetComponentsInChildren<Light>(true)) Object.DestroyImmediate(light);
                foreach (var animator in visual.GetComponentsInChildren<Animator>(true)) animator.enabled = animator.runtimeAnimatorController != null;
                StripHelperGeometry(visual, kind);
                BuildLodGroup(visual, kind);
                var renderers = root.GetComponentsInChildren<Renderer>(true);
                if (renderers.Length == 0) continue;
                Renderer[] lod0 = Lod0Renderers(renderers);
                Bounds bounds = BoundsOf(lod0);
                if (bounds.size.y < .001f || float.IsNaN(bounds.size.y) || float.IsInfinity(bounds.size.y)) continue;
                if (kind == LiveCityCatalog.Kind.Tree && Mathf.Max(bounds.size.x, bounds.size.z) > bounds.size.y * 2f)
                {
                    visual.transform.localRotation = Quaternion.Euler(bounds.size.z > bounds.size.x ? -90 : 0, 0, bounds.size.x >= bounds.size.z ? 90 : 0) * visual.transform.localRotation;
                    bounds = BoundsOf(lod0);
                }
                visual.transform.localScale *= height / bounds.size.y;
                bounds = BoundsOf(lod0);
                visual.transform.position -= new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);
                bounds = BoundsOf(lod0);
                int triangles = TrianglesOf(lod0);
                // Dense hero/scanned assets remain untouched and are excluded from repeated scenery.
                if (triangles > (kind == LiveCityCatalog.Kind.Pedestrian ? 100000 : 65000) || Mathf.Max(bounds.size.x, bounds.size.z) > 22f)
                { Debug.Log($"[LiveCity] Skipped dense/oversized repeated prop: {path} ({triangles} triangles)"); continue; }
                var materialMap = new Dictionary<Material, Material>();
                int materialIndex = 0;
                foreach (var renderer in renderers)
                {
                    var materials = renderer.sharedMaterials;
                    for (int i = 0; i < materials.Length; i++)
                    {
                        Material original = materials[i];
                        if (original != null && materialMap.TryGetValue(original, out var existing)) { materials[i] = existing; continue; }
                        Material converted = MakeMaterial(original, kind, path);
                        string materialPath = $"{Output}/{stem}_{materialIndex++}.mat";
                        var saved = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
                        if (saved == null) { AssetDatabase.CreateAsset(converted, materialPath); saved = converted; }
                        else { EditorUtility.CopySerialized(converted, saved); Object.DestroyImmediate(converted); EditorUtility.SetDirty(saved); }
                        materials[i] = saved;
                        if (original != null) materialMap[original] = saved;
                    }
                    renderer.sharedMaterials = materials;
                    renderer.shadowCastingMode = kind == LiveCityCatalog.Kind.Plant ? ShadowCastingMode.Off : ShadowCastingMode.On;
                }
                var prefab = PrefabUtility.SaveAsPrefabAsset(root, $"{Output}/{stem}.prefab");
                entries.Add(new LiveCityCatalog.Entry { prefab = prefab, kind = kind, bounds = bounds, triangles = triangles });
            }
            finally { Object.DestroyImmediate(root); }
        }
        var catalog = AssetDatabase.LoadAssetAtPath<LiveCityCatalog>(CatalogPath);
        if (catalog == null) { catalog = ScriptableObject.CreateInstance<LiveCityCatalog>(); AssetDatabase.CreateAsset(catalog, CatalogPath); }
        catalog.entries = entries.ToArray();
        EditorUtility.SetDirty(catalog);
        AssetDatabase.SaveAssets();
        Debug.Log($"[LiveCity] Prepared {entries.Count} imported scenery models. Sources and bus prefabs were not modified.");
    }

    static bool NeedsBuild()
    {
        if (!File.Exists(CatalogPath)) return true;
        DateTime builtAt = File.GetLastWriteTimeUtc(CatalogPath);
        foreach (string path in EnumerateSourceFiles())
            if (File.GetLastWriteTimeUtc(path) > builtAt) return true;
        return false;
    }

    static IEnumerable<string> EnumerateSourceFiles()
    {
        foreach (string folder in SourceRoots)
        {
            if (!Directory.Exists(folder)) continue;
            foreach (string path in Directory.GetFiles(folder, "*", SearchOption.AllDirectories))
            {
                string extension = Path.GetExtension(path);
                if (extension.Equals(".fbx", StringComparison.OrdinalIgnoreCase) ||
                    TextureExtensions.Any(ext => ext.Equals(extension, StringComparison.OrdinalIgnoreCase)))
                    yield return path.Replace('\\', '/');
            }
        }
        if (File.Exists(Person)) yield return Person;
    }

    /// <summary>
    /// One source per logical model. The same tree or plant is often shipped at
    /// the pack root and again inside a folder with its maps; prefer the copy with
    /// more nearby textures but keep the root GUID as the output identity.
    /// </summary>
    static List<SourceModel> CollectSources()
    {
        var groups = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        foreach (string path in EnumerateSourceFiles())
        {
            if (!Path.GetExtension(path).Equals(".fbx", StringComparison.OrdinalIgnoreCase)) continue;
            string key = CanonicalModelKey(path);
            if (!groups.TryGetValue(key, out var list))
            {
                list = new List<string>();
                groups[key] = list;
            }
            list.Add(path);
        }

        var sources = new List<SourceModel>(groups.Count);
        foreach (var pair in groups)
        {
            string chosen = pair.Value
                .OrderByDescending(TextureCount)
                .ThenByDescending(PathDepth)
                .ThenBy(path => path, StringComparer.OrdinalIgnoreCase)
                .First();
            sources.Add(new SourceModel(chosen, IdentityPath(chosen)));
        }
        sources.Sort((a, b) => string.Compare(a.identityPath, b.identityPath, StringComparison.OrdinalIgnoreCase));
        return sources;
    }

    static string IdentityPath(string sourcePath)
    {
        foreach (string root in SourceRoots)
        {
            string rootPath = root.Replace('\\', '/');
            if (!sourcePath.StartsWith(rootPath + "/", StringComparison.OrdinalIgnoreCase)) continue;
            string candidate = $"{rootPath}/{Path.GetFileName(sourcePath)}";
            if (File.Exists(candidate)) return candidate;
        }
        return sourcePath;
    }

    static int TextureCount(string sourcePath)
    {
        string directory = Path.GetDirectoryName(sourcePath);
        if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory)) return 0;

        int count = CountTextures(directory, SearchOption.TopDirectoryOnly);
        foreach (string child in Directory.GetDirectories(directory))
        {
            if (Directory.GetFiles(child, "*.fbx", SearchOption.AllDirectories).Length > 0) continue;
            count += CountTextures(child, SearchOption.AllDirectories);
        }
        return count;
    }

    static int CountTextures(string folder, SearchOption searchOption)
    {
        int count = 0;
        foreach (string path in Directory.GetFiles(folder, "*", searchOption))
        {
            string extension = Path.GetExtension(path);
            if (TextureExtensions.Any(ext => ext.Equals(extension, StringComparison.OrdinalIgnoreCase)))
                count++;
        }
        return count;
    }

    static int PathDepth(string path)
    {
        int depth = 0;
        foreach (char c in path) if (c == '/' || c == '\\') depth++;
        return depth;
    }

    static string CanonicalModelKey(string path)
    {
        string name = Path.GetFileNameWithoutExtension(path).ToLowerInvariant();
        foreach (string suffix in new[] { " corona", " vray", " standard", " obj mtl", " obj" })
        {
            if (!name.EndsWith(suffix, StringComparison.Ordinal)) continue;
            name = name.Substring(0, name.Length - suffix.Length);
            break;
        }
        return new string(name.Where(char.IsLetterOrDigit).ToArray());
    }

    static bool Classify(string path, out LiveCityCatalog.Kind kind, out float height)
    {
        string name = Path.GetFileNameWithoutExtension(path).ToLowerInvariant();
        kind = LiveCityCatalog.Kind.Plant; height = 1f;
        if (path == Person) { kind = LiveCityCatalog.Kind.Pedestrian; height = 1.7f; }
        else if (name.Contains("forestpack")) return false; // Multi-tree pack, not a single scenery prop.
        else if (name.Contains("barrier")) return false; // Road builder owns junction-safe barriers.
        else if (path.Contains("/billboard/")) { kind = LiveCityCatalog.Kind.Billboard; height = name.Contains("sign") ? 2.6f : 5f; }
        else if (name.Contains("bench")) { kind = LiveCityCatalog.Kind.Bench; height = .9f; }
        else if (path.Contains("/busstop/")) return false; // Mission spawner owns actual stops.
        else if (name.Contains("tree") || name.Contains("cocos") || name.Contains("carpinus"))
        {
            kind = LiveCityCatalog.Kind.Tree;
            height = name.Contains("carpinus") ? 8f : 7f;
        }
        else if (name.Contains("rock")) { kind = LiveCityCatalog.Kind.Rock; height = name.Contains("cliff") ? 2f : .8f; }
        else if (name.Contains("banana")) { kind = LiveCityCatalog.Kind.Plant; height = 2.7f; }
        else if (name.Contains("pyrrosia")) { kind = LiveCityCatalog.Kind.Plant; height = .6f; }
        else if (name.Contains("dipteris") || name.Contains("plant")) { kind = LiveCityCatalog.Kind.Plant; height = 1.2f; }
        else { height = name.Contains("grass") ? .35f : 1.2f; }
        return true;
    }

    readonly struct SourceModel
    {
        public readonly string assetPath;
        public readonly string identityPath;

        public SourceModel(string assetPath, string identityPath)
        {
            this.assetPath = assetPath;
            this.identityPath = identityPath;
        }
    }

    static readonly Color[] ClothingPalette =
    {
        new Color(.20f, .29f, .42f), new Color(.45f, .15f, .14f), new Color(.16f, .34f, .26f),
        new Color(.56f, .47f, .30f), new Color(.30f, .30f, .35f), new Color(.62f, .58f, .52f)
    };

    static Color PedestrianTone(string materialName)
    {
        if (materialName.Contains("skin") || materialName.Contains("head") || materialName.Contains("face"))
            return new Color(.60f, .43f, .32f);
        if (materialName.Contains("hair")) return new Color(.09f, .07f, .06f);
        if (materialName.Contains("heel") || materialName.Contains("shoe") || materialName.Contains("boot"))
            return new Color(.10f, .10f, .11f);
        if (materialName.Contains("eye") || materialName.Contains("brow")) return new Color(.12f, .10f, .09f);
        int hash = 0;
        foreach (char c in materialName) hash = unchecked(hash * 31 + c);
        return ClothingPalette[Mathf.Abs(hash) % ClothingPalette.Length];
    }

    static int Triangles(Mesh mesh)
    {
        if (mesh == null) return 0;
        int total = 0;
        for (int i = 0; i < mesh.subMeshCount; i++) total += (int)mesh.GetIndexCount(i) / 3;
        return total;
    }
    static Bounds BoundsOf(Renderer[] renderers)
    { Bounds result = renderers[0].bounds; foreach (var r in renderers) result.Encapsulate(r.bounds); return result; }

    /// <summary>
    /// Game-ready plant packs ship LOD1-3, UCX collision meshes and camera-path or
    /// ground helper renderers beside LOD0. Helpers are always removed. For trees
    /// and plants the higher-detail meshes become a real LODGroup instead of being
    /// deleted: keeping every copy visible caused overdraw, but discarding them
    /// made the whole city pay for LOD0 at every distance. Other prop kinds keep
    /// only their lowest LOD because their extra levels are not reliable.
    /// </summary>
    static void StripHelperGeometry(GameObject root, LiveCityCatalog.Kind kind)
    {
        Renderer[] renderers = root.GetComponentsInChildren<Renderer>(true);
        int lowestLod = int.MaxValue;
        foreach (Renderer renderer in renderers)
        {
            int lod = LodIndex(renderer.name);
            if (lod >= 0) lowestLod = Mathf.Min(lowestLod, lod);
        }

        bool keepsLods = kind == LiveCityCatalog.Kind.Tree || kind == LiveCityCatalog.Kind.Plant;
        foreach (Renderer renderer in renderers)
        {
            string name = renderer.name.ToLowerInvariant();
            if (name.StartsWith("ucx_") || name.Contains("_ucx_") || name.Contains("camera_path") || name.Contains("camerapath"))
            {
                Object.DestroyImmediate(renderer.gameObject);
                continue;
            }
            if ((kind == LiveCityCatalog.Kind.Tree || kind == LiveCityCatalog.Kind.Plant) &&
                (name == "ground" || name.StartsWith("ground ") || name.Contains("_ground")))
            {
                Object.DestroyImmediate(renderer.gameObject);
                continue;
            }
            int lod = LodIndex(renderer.name);
            if (!keepsLods && lod >= 0 && lowestLod != int.MaxValue && lod > lowestLod)
                Object.DestroyImmediate(renderer.gameObject);
        }
    }

    /// <summary>
    /// Builds a LODGroup from the LOD-named renderers a plant pack ships. Only the
    /// highest-detail level is used for catalog bounds and the triangle budget; the
    /// lower levels are only drawn when the group's screen size falls.
    /// </summary>
    static void BuildLodGroup(GameObject visual, LiveCityCatalog.Kind kind)
    {
        if (kind != LiveCityCatalog.Kind.Tree && kind != LiveCityCatalog.Kind.Plant) return;

        // Some source FBXs already ship a LODGroup whose levels reference helper or
        // stripped objects. Rebuild it from the surviving renderers so the prefab
        // cannot keep dangling LOD entries or gain a second group.
        foreach (LODGroup old in visual.GetComponentsInChildren<LODGroup>(true))
            Object.DestroyImmediate(old);

        var levels = new SortedDictionary<int, List<Renderer>>();
        foreach (Renderer renderer in visual.GetComponentsInChildren<Renderer>(true))
        {
            if (renderer == null) continue;
            int lod = Mathf.Max(0, LodIndex(renderer.name));
            if (!levels.TryGetValue(lod, out var list)) levels[lod] = list = new List<Renderer>();
            list.Add(renderer);
        }
        if (levels.Count < 2) return;

        var lods = new List<LOD>(levels.Count);
        int index = 0;
        foreach (var pair in levels)
        {
            // 50% screen height is full detail; each coarser level takes over at
            // roughly half the remaining screen coverage. The exact height is not
            // critical for street props, but a descending order is.
            float screenHeight = index == 0 ? .5f : Mathf.Clamp(.5f * Mathf.Pow(.52f, index), .02f, .45f);
            lods.Add(new LOD(screenHeight, pair.Value.ToArray()));
            index++;
        }

        var group = visual.AddComponent<LODGroup>();
        group.SetLODs(lods.ToArray());
        group.RecalculateBounds();
        group.fadeMode = LODFadeMode.None;
        group.lastLODBillboard = false;
    }

    /// <summary>Renderers that make up the highest-detail level, used for bounds and budget.</summary>
    static Renderer[] Lod0Renderers(Renderer[] renderers)
    {
        int lowest = int.MaxValue;
        foreach (Renderer renderer in renderers)
        {
            int lod = LodIndex(renderer.name);
            if (lod >= 0) lowest = Mathf.Min(lowest, lod);
        }
        if (lowest == int.MaxValue) return renderers;

        var result = new List<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            int lod = LodIndex(renderer.name);
            if (lod < 0 || lod == lowest) result.Add(renderer);
        }
        return result.Count > 0 ? result.ToArray() : renderers;
    }

    static int TrianglesOf(Renderer[] renderers)
    {
        int total = 0;
        foreach (Renderer renderer in renderers)
        {
            if (renderer is MeshRenderer)
            {
                var filter = renderer.GetComponent<MeshFilter>();
                if (filter != null) total += Triangles(filter.sharedMesh);
            }
            else if (renderer is SkinnedMeshRenderer skin)
            {
                total += Triangles(skin.sharedMesh);
            }
        }
        return total;
    }

    static int LodIndex(string name)
    {
        string lower = name.ToLowerInvariant();
        int index = lower.IndexOf("lod", StringComparison.Ordinal);
        while (index >= 0)
        {
            int cursor = index + 3;
            if (cursor < lower.Length && (lower[cursor] == '_' || lower[cursor] == ' ' || lower[cursor] == '-')) cursor++;
            if (cursor < lower.Length && char.IsDigit(lower[cursor])) return lower[cursor] - '0';
            index = lower.IndexOf("lod", index + 3, StringComparison.Ordinal);
        }
        return -1;
    }

    static Material MakeMaterial(Material source, LiveCityCatalog.Kind kind, string sourcePath)
    {
        var result = new Material(BuiltInShaders.Lit);
        result.name = source != null ? source.name : kind.ToString();
        string materialName = result.name;
        Texture texture = source != null && source.HasProperty("_MainTex") ? source.GetTexture("_MainTex") : null;
        Texture bump = source != null && source.HasProperty("_BumpMap") ? source.GetTexture("_BumpMap") : null;
        if (texture == null) texture = FindTexture(sourcePath, materialName, false);
        if (bump == null) bump = FindTexture(sourcePath, materialName, true);
        Color color = source != null && source.HasProperty("_Color") ? source.color : Color.white;
        bool neutral = Mathf.Abs(color.r - color.g) < .03f && Mathf.Abs(color.g - color.b) < .03f;
        if (texture != null)
        {
            // The artwork already carries its own tone. Keep the material white so
            // the source's placeholder grey does not darken the texture.
            color = Color.white;
        }
        else if (neutral)
        {
            string name = materialName.ToLowerInvariant();
            // Pedestrians used to be skipped here, which left the untextured walker
            // model as a white mannequin. Skin, hair, shoes and clothing each get a
            // tone derived from the material name instead.
            if (kind == LiveCityCatalog.Kind.Pedestrian)
                color = PedestrianTone(name);
            else
                color = kind == LiveCityCatalog.Kind.Tree || kind == LiveCityCatalog.Kind.Plant
                    ? (name.Contains("trunk") || name.Contains("bark") ? new Color(.3f, .21f, .12f) : new Color(.2f, .42f, .12f))
                    : kind == LiveCityCatalog.Kind.Rock ? new Color(.4f, .38f, .32f) : new Color(.26f, .3f, .31f);
        }
        result.SetColor("_Color", color);
        result.SetTexture("_MainTex", texture);
        if (source != null && texture != null && source.HasProperty("_MainTex"))
        {
            result.SetTextureScale("_MainTex", source.GetTextureScale("_MainTex"));
            result.SetTextureOffset("_MainTex", source.GetTextureOffset("_MainTex"));
        }
        result.SetTexture("_BumpMap", bump);
        if (bump != null)
        {
            result.EnableKeyword("_NORMALMAP");
            result.SetFloat("_BumpScale", kind == LiveCityCatalog.Kind.Tree || kind == LiveCityCatalog.Kind.Plant ? .8f : 1f);
        }
        result.SetFloat("_Metallic", 0f);
        result.SetFloat("_Glossiness", SurfaceSmoothness(kind, materialName));
        if (result.HasProperty("_GlossyReflections")) result.SetFloat("_GlossyReflections", 1f);
        if (kind == LiveCityCatalog.Kind.Tree || kind == LiveCityCatalog.Kind.Plant)
        {
            result.SetFloat("_Cutoff", .42f);
            result.EnableKeyword("_ALPHATEST_ON");
            result.DisableKeyword("_ALPHABLEND_ON");
            result.SetOverrideTag("RenderType", "TransparentCutout");
            result.renderQueue = (int)RenderQueue.AlphaTest;
            result.SetFloat("_Mode", 1);
        }
        result.enableInstancing = true;
        return result;
    }

    /// <summary>
    /// Leaf cards and bark are matte; a high smoothness on foliage is what makes
    /// imported trees look like plastic. Rocks and painted street furniture keep a
    /// little more response, but remain below a varnished surface.
    /// </summary>
    static float SurfaceSmoothness(LiveCityCatalog.Kind kind, string materialName)
    {
        string name = (materialName ?? string.Empty).ToLowerInvariant();
        if (kind == LiveCityCatalog.Kind.Tree || kind == LiveCityCatalog.Kind.Plant)
        {
            if (name.Contains("bark") || name.Contains("trunk") || name.Contains("branch"))
                return .3f;
            return .22f;
        }
        if (kind == LiveCityCatalog.Kind.Rock) return .34f;
        if (kind == LiveCityCatalog.Kind.Bench) return .38f;
        if (kind == LiveCityCatalog.Kind.Billboard) return .3f;
        return .4f;
    }

    internal static Texture FindTexture(string sourcePath, string materialName, bool normal)
    {
        string folder = Path.GetDirectoryName(sourcePath);
        if (string.IsNullOrEmpty(folder) || !Directory.Exists(folder)) return null;

        string modelName = Path.GetFileNameWithoutExtension(sourcePath);
        string best = null;
        int bestScore = int.MinValue;
        int candidates = 0;
        foreach (string file in TextureFiles(sourcePath))
        {
            if (!IsTextureFile(file)) continue;
            string fileName = Path.GetFileNameWithoutExtension(file);
            if (normal != LooksNormal(fileName)) continue;
            if (!normal && !LooksAlbedo(fileName)) continue;
            candidates++;
            int score = TextureScore(fileName, materialName, modelName, normal);
            if (score <= bestScore) continue;
            bestScore = score;
            best = file;
        }

        bool defaultName = string.IsNullOrEmpty(materialName) ||
            materialName.IndexOf("default", StringComparison.OrdinalIgnoreCase) >= 0 ||
            materialName.IndexOf("no name", StringComparison.OrdinalIgnoreCase) >= 0;
        if (best == null || (bestScore <= 0 && !(defaultName && candidates == 1))) return null;
        return AssetDatabase.LoadAssetAtPath<Texture>(best.Replace('\\', '/'));
    }

    /// <summary>
    /// Textures that belong to this source model. A nested folder that contains
    /// another FBX is a different model pack and must not leak its maps into a
    /// top-level bus or tree.
    /// </summary>
    internal static IEnumerable<string> TextureFiles(string sourcePath)
    {
        string folder = Path.GetDirectoryName(sourcePath);
        if (string.IsNullOrEmpty(folder) || !Directory.Exists(folder)) yield break;
        foreach (string file in Directory.GetFiles(folder, "*", SearchOption.TopDirectoryOnly))
            if (IsTextureFile(file)) yield return file;
        foreach (string child in Directory.GetDirectories(folder))
        {
            if (Directory.GetFiles(child, "*.fbx", SearchOption.AllDirectories).Length > 0) continue;
            foreach (string file in Directory.GetFiles(child, "*", SearchOption.AllDirectories))
                if (IsTextureFile(file)) yield return file;
        }

        // Some packs keep the FBX in a "FBX" subfolder and the maps in a sibling
        // "textures" folder. Include sibling folders that do not contain another
        // model, so that layout works without borrowing another bus or tree.
        string parent = Path.GetDirectoryName(folder);
        if (string.IsNullOrEmpty(parent) || !Directory.Exists(parent)) yield break;
        string sourceFolder = Path.GetFullPath(folder);
        foreach (string sibling in Directory.GetDirectories(parent))
        {
            if (string.Equals(Path.GetFullPath(sibling), sourceFolder, StringComparison.OrdinalIgnoreCase)) continue;
            if (Directory.GetFiles(sibling, "*.fbx", SearchOption.AllDirectories).Length > 0) continue;
            foreach (string file in Directory.GetFiles(sibling, "*", SearchOption.AllDirectories))
                if (IsTextureFile(file)) yield return file;
        }
    }

    internal static bool IsTextureFile(string path)
    {
        if (Path.GetFileName(path).StartsWith("._")) return false;
        string extension = Path.GetExtension(path);
        return TextureExtensions.Any(ext => ext.Equals(extension, StringComparison.OrdinalIgnoreCase));
    }

    static bool LooksNormal(string name)
    {
        string lower = name.ToLowerInvariant();
        return lower.Contains("normal") || lower.Contains("_nor_") || lower.Contains("_nrm") || lower.Contains("bump");
    }

    static bool LooksAlbedo(string name)
    {
        string lower = name.ToLowerInvariant();
        return !(lower.Contains("normal") || lower.Contains("_nor_") || lower.Contains("_nrm") ||
                 lower.Contains("bump") || lower.Contains("rough") || lower.Contains("metal") ||
                 lower.Contains("height") || lower.Contains("disp") || lower.Contains("opacity") ||
                 lower.Contains("alpha") || lower.Contains("_sss") || lower.Contains("emissive") ||
                 lower.Contains("emission") || lower.Contains("mask") || lower.Contains("_ao") ||
                 lower.Contains("ambient") || lower.Contains("specular") || lower.Contains("gloss"));
    }

    static int TextureScore(string fileName, string materialName, string modelName, bool normal)
    {
        string texture = Compact(fileName);
        string material = Compact(materialName);
        string model = Compact(modelName);
        int score = 0;
        bool matched = false;
        if (material.Length >= 4 && texture.Contains(material)) { score += 100; matched = true; }
        else if (texture.Length >= 4 && material.Contains(texture)) { score += 60; matched = true; }
        if (model.Length >= 4 && texture.Contains(model)) { score += 30; matched = true; }
        foreach (string token in Tokens(materialName))
            if (token.Length >= 3 && texture.Contains(token)) { score += 8; matched = true; }
        foreach (string token in Tokens(modelName))
            if (token.Length >= 3 && texture.Contains(token)) { score += 3; matched = true; }
        if (!matched) return 0;
        if (normal)
        {
            if (texture.Contains("opengl")) score += 4;
            if (texture.Contains("directx")) score -= 4;
        }
        else
        {
            if (texture.Contains("basecolor") || texture.Contains("albedo") ||
                texture.Contains("diffuse") || texture.Contains("diff")) score += 6;
            if (texture.Contains("billboard") && !material.Contains("billboard")) score -= 40;
        }
        return score;
    }

    static string Compact(string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        return new string(value.Where(char.IsLetterOrDigit).Select(char.ToLowerInvariant).ToArray());
    }

    static IEnumerable<string> Tokens(string value)
    {
        if (string.IsNullOrEmpty(value)) yield break;
        var token = new System.Text.StringBuilder();
        foreach (char c in value)
        {
            if (char.IsLetterOrDigit(c)) token.Append(char.ToLowerInvariant(c));
            else if (token.Length > 0) { yield return token.ToString(); token.Clear(); }
        }
        if (token.Length > 0) yield return token.ToString();
    }
}
#endif
