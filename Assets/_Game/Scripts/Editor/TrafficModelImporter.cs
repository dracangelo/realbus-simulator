#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

/// <summary>
/// Turns the downloaded vehicle FBXs into the normalised traffic prefabs the game
/// spawns, and writes the catalog that lists them.
///
/// The traffic pool used to fall back to four procedural boxes whenever a pack was
/// not pre-wired, because the downloaded models were still inside .zip files and
/// nothing imported them: every Resources path under
/// <c>Assets/_Game/Resources/BussimAssets/aitraffic</c> resolved to nothing. This
/// tool reads the extracted sources under <c>Assets/unity models/traffic</c>,
/// strips what must not reach a running scene (cameras, lights, audio, colliders,
/// rigidbodies), turns the model onto +Z, scales it to its class length, drops it
/// onto the road, converts its materials for the active render pipeline, and saves
/// one prefab per model with its measured bounds and triangle cost.
///
/// It owns <c>Assets/_Game/Resources/TrafficGenerated</c> only: source FBXs and
/// hand-made prefabs are never rewritten. Each saved prefab ends up with exactly the
/// components the traffic pool expects: renderers on the model, and one body collider
/// so a downloaded model collides where the built-in ones do.
/// </summary>
public static class TrafficModelImporter
{
    const string SourceRoot = "Assets/unity models/traffic";
    const string Output = "Assets/_Game/Resources/TrafficGenerated";
    const string CatalogPath = "Assets/_Game/Resources/TrafficCatalog.asset";

    /// <summary>Above this, a model is kept but down-weighted: traffic must stay cheap.</summary>
    const int DenseModelTriangles = 60000;

    [InitializeOnLoadMethod]
    static void QueueInitialImport()
    {
        EditorApplication.delayCall += () =>
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode) return;
            if (File.Exists(CatalogPath) || !Directory.Exists(SourceRoot)) return;
            Build();
        };
    }

    [MenuItem("Tools/RealBus/Setup/Build Traffic Models")]
    public static void Build()
    {
        Directory.CreateDirectory(Output);
        AssetDatabase.Refresh();

        var entries = new List<TrafficCatalog.Entry>();
        List<string> sources = FindSourceModels();
        if (sources.Count == 0)
        {
            Debug.LogWarning($"[Traffic] No vehicle FBX under '{SourceRoot}'. Extract the downloaded packs there and run this tool again.");
            return;
        }

        foreach (string path in sources)
        {
            TrafficCatalog.Entry entry = BuildEntry(path);
            if (entry != null) entries.Add(entry);
        }

        var catalog = AssetDatabase.LoadAssetAtPath<TrafficCatalog>(CatalogPath);
        if (catalog == null)
        {
            catalog = ScriptableObject.CreateInstance<TrafficCatalog>();
            AssetDatabase.CreateAsset(catalog, CatalogPath);
        }
        catalog.entries = entries.ToArray();
        EditorUtility.SetDirty(catalog);
        AssetDatabase.SaveAssets();

        int triangles = 0;
        foreach (var entry in entries) triangles += entry.triangles;
        Debug.Log($"[Traffic] Prepared {entries.Count} traffic models from {sources.Count} sources ({triangles} triangles total). Sources were not modified.");
    }

    static List<string> FindSourceModels()
    {
        var paths = new List<string>();
        if (!Directory.Exists(SourceRoot)) return paths;

        foreach (string path in Directory.GetFiles(SourceRoot, "*", SearchOption.AllDirectories))
            if (Path.GetExtension(path).Equals(".fbx", StringComparison.OrdinalIgnoreCase) ||
                Path.GetExtension(path).Equals(".obj", StringComparison.OrdinalIgnoreCase))
                paths.Add(path.Replace('\\', '/'));
        paths.Sort(StringComparer.Ordinal);
        return paths;
    }

    static TrafficCatalog.Entry BuildEntry(string path)
    {
        GameObject source = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (source == null)
        {
            Debug.LogWarning($"[Traffic] Could not load '{path}'.");
            return null;
        }

        string modelName = Path.GetFileNameWithoutExtension(path);
        AIVehicleController.VehicleType type = TrafficModelRules.ClassifyModel(modelName);
        string guid = AssetDatabase.AssetPathToGUID(path);
        string stem = $"{Sanitise(modelName)}_{(guid.Length >= 8 ? guid.Substring(0, 8) : guid)}";

        var root = new GameObject(modelName);
        try
        {
            GameObject visual = Object.Instantiate(source, root.transform);
            Strip(visual);

            Renderer[] renderers = root.GetComponentsInChildren<Renderer>(true);
            if (renderers.Length == 0)
            {
                Debug.LogWarning($"[Traffic] Skipped '{path}': no renderers.");
                return null;
            }

            Bounds bounds = BoundsOf(renderers);
            if (bounds.size.y < .001f || float.IsNaN(bounds.size.y) || float.IsInfinity(bounds.size.y))
            {
                Debug.LogWarning($"[Traffic] Skipped '{path}': unusable bounds {bounds.size}.");
                return null;
            }

            // A model authored longer across X than along Z would drive sideways.
            if (TrafficModelRules.NeedsQuarterTurn(bounds.size))
                visual.transform.localRotation = Quaternion.Euler(0f, -90f, 0f) * visual.transform.localRotation;
            bounds = BoundsOf(renderers);

            // Nothing in an FBX records which end is the nose, so a model can be
            // normalised perfectly and still drive backwards through the city.
            int facing = FacingFromParts(root, bounds.center.z, TrafficModelRules.LengthOf(bounds.size) * .5f, out string facingDetail);
            if (facing < 0)
            {
                visual.transform.localRotation = Quaternion.Euler(0f, 180f, 0f) * visual.transform.localRotation;
                bounds = BoundsOf(renderers);
            }

            float targetLength = TrafficModelRules.TargetLengthMeters(type);
            float currentLength = Mathf.Max(.01f, TrafficModelRules.LengthOf(bounds.size));
            visual.transform.localScale *= targetLength / currentLength;
            bounds = BoundsOf(renderers);

            // Sit on the road, centred on the pivot, so the pool's lane maths and the
            // model's silhouette agree without per-instance offsets.
            visual.transform.position -= new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);
            bounds = BoundsOf(renderers);

            int triangles = CountTriangles(root);
            if (triangles > DenseModelTriangles)
                Debug.LogWarning($"[Traffic] '{modelName}' has {triangles} triangles, above the {DenseModelTriangles} traffic budget: it is included with a reduced pool weight.");

            // The pool drives these on a rigidbody and turns collisions on once one is
            // near the player, exactly as it does for the built-in vehicles. A downloaded
            // model arrives with its own colliders stripped, so without this it would
            // drive straight through the bus and its neighbours. One box around the
            // body, never a non-convex mesh collider on a mover.
            BoxCollider body = root.AddComponent<BoxCollider>();
            body.center = new Vector3(0f, bounds.size.y * .5f, 0f);
            body.size = new Vector3(bounds.size.x * .94f, bounds.size.y * .96f, bounds.size.z * .96f);

            ApplyMaterials(root, renderers, type, modelName);

            var prefab = PrefabUtility.SaveAsPrefabAsset(root, $"{Output}/{stem}.prefab");
            Debug.Log($"[Traffic] {modelName} -> {type}, length {TrafficModelRules.LengthOf(bounds.size):F1} m, {triangles} triangles, " +
                $"facing {(facing < 0 ? "turned to +Z" : facing > 0 ? "already +Z" : "unknown, left as authored")} ({facingDetail}), " +
                $"weight {TrafficModelRules.PoolWeight(type, triangles):F2}");
            return new TrafficCatalog.Entry
            {
                prefab = prefab,
                vehicleType = type,
                displayName = modelName,
                lengthMeters = TrafficModelRules.LengthOf(bounds.size),
                bounds = bounds,
                triangles = triangles,
                weight = TrafficModelRules.PoolWeight(type, triangles)
            };
        }
        finally { Object.DestroyImmediate(root); }
    }

    /// <summary>Removes everything on a downloaded model that must not reach a scene.</summary>
    static void Strip(GameObject visual)
    {
        foreach (var camera in visual.GetComponentsInChildren<Camera>(true)) Object.DestroyImmediate(camera);
        foreach (var listener in visual.GetComponentsInChildren<AudioListener>(true)) Object.DestroyImmediate(listener);
        foreach (var audio in visual.GetComponentsInChildren<AudioSource>(true)) Object.DestroyImmediate(audio);
        foreach (var light in visual.GetComponentsInChildren<Light>(true)) Object.DestroyImmediate(light);
        foreach (var collider in visual.GetComponentsInChildren<Collider>(true)) Object.DestroyImmediate(collider);
        foreach (var body in visual.GetComponentsInChildren<Rigidbody>(true)) Object.DestroyImmediate(body);
        // Traffic poses are driven by the graph, never by an embedded animation.
        foreach (var animator in visual.GetComponentsInChildren<Animator>(true)) animator.enabled = false;
        // Interiors are the bulk of a scanned vehicle download and are invisible from
        // the street: the Van pack alone spends ~700 000 triangles inside the cabin.
        var parts = visual.GetComponentsInChildren<Transform>(true);
        int stripped = 0;
        foreach (var part in parts)
        {
            if (part == null || part == visual.transform) continue;
            if (!TrafficModelRules.IsInteriorPart(part.name)) continue;
            Object.DestroyImmediate(part.gameObject);
            stripped++;
        }
        if (stripped > 0) Debug.Log($"[Traffic] Stripped {stripped} interior parts from '{visual.name}'.");
    }

    /// <summary>Converts every material slot to a Built-in material, keeping the download's look.</summary>
    static void ApplyMaterials(GameObject root, Renderer[] renderers, AIVehicleController.VehicleType type, string modelName)
    {
        Shader target = BuiltInShaders.Lit;

        var replaced = new Dictionary<Material, Material>();
        int index = 0;
        string stem = Sanitise(modelName);

        foreach (var renderer in renderers)
        {
            Material[] materials = renderer.sharedMaterials;
            for (int i = 0; i < materials.Length; i++)
            {
                Material original = materials[i];
                if (original != null && replaced.TryGetValue(original, out Material cached))
                {
                    materials[i] = cached;
                    continue;
                }

                Material converted = Convert(original, target, type, stem, index++);
                materials[i] = converted;
                if (original != null) replaced[original] = converted;
            }
            renderer.sharedMaterials = materials;
            renderer.shadowCastingMode = ShadowCastingMode.On;
            renderer.receiveShadows = false;
        }
    }

    static Material Convert(Material original, Shader target, AIVehicleController.VehicleType type, string stem, int index)
    {
        var material = original != null ? new Material(original) : new Material(target);
        if (original == null) material.shader = target;
        material.name = $"{stem}_{index}";

        // A downloaded material can arrive with a missing or unsupported shader,
        // which renders magenta, so the shader is always re-resolved.
        if (target != null) material.shader = target;

        Texture texture = ReadTexture(original);
        Color color = ReadColor(original);
        if (texture == null && IsNearNeutral(color))
        {
            // Untextured panels arrive flat white; paint them so a pack without
            // textures still reads as vehicles rather than white shells.
            color = PaintFor(type, stem, index);
        }        material.color = color;
        if (material.HasProperty("_Color")) material.SetColor("_Color", color);

        if (texture != null && material.HasProperty("_MainTex"))
        {
            material.SetTexture("_MainTex", texture);
            if (original != null && original.HasProperty("_MainTex"))
            {
                material.SetTextureScale("_MainTex", original.GetTextureScale("_MainTex"));
                material.SetTextureOffset("_MainTex", original.GetTextureOffset("_MainTex"));
            }
        }

        string name = material.name.ToLowerInvariant();
        bool glass = (original != null && original.name.ToLowerInvariant().Contains("glass")) ||
                     name.Contains("glass") || name.Contains("kaca") || name.Contains("window");
        if (glass)
        {
            color.a = Mathf.Clamp(color.a > .05f ? color.a : .35f, .22f, .55f);
            material.color = color;
            if (material.HasProperty("_Color")) material.SetColor("_Color", color);
            ImportedVehicleVisualRepair.ConfigureGlass(material);
        }
        else
        {
            float smoothness = material.HasProperty("_Metallic") ? .45f : .35f;
            if (material.HasProperty("_Glossiness")) material.SetFloat("_Glossiness", smoothness);
            if (material.HasProperty("_Smoothness")) material.SetFloat("_Smoothness", smoothness);
        }

        material.enableInstancing = true;
        return material;
    }

    // The texture and colour are read from the downloaded material, which can be
    // authored for any pipeline, so both the Built-in (_MainTex/_Color) and the
    // URP (_BaseMap/_BaseColor) slots are probed here. This is reading a foreign
    // asset, not choosing how the project renders: whatever is found is written
    // into a Built-in material by Convert.
    static Texture ReadTexture(Material source)
    {
        if (source == null) return null;
        if (source.HasProperty("_BaseMap") && source.GetTexture("_BaseMap") != null) return source.GetTexture("_BaseMap");
        if (source.HasProperty("_MainTex")) return source.GetTexture("_MainTex");
        return null;
    }

    static Color ReadColor(Material source)
    {
        if (source == null) return Color.white;
        if (source.HasProperty("_BaseColor")) return source.GetColor("_BaseColor");
        if (source.HasProperty("_Color")) return source.GetColor("_Color");
        return Color.white;
    }

    static bool IsNearNeutral(Color color)
        => Mathf.Abs(color.r - color.g) < .06f && Mathf.Abs(color.g - color.b) < .06f && color.r > .5f;

    /// <summary>Deterministic body colour for a model, so a pack without textures still has a palette.</summary>
    static Color PaintFor(AIVehicleController.VehicleType type, string stem, int index)
    {
        Color[] palette =
        {
            new Color(.72f, .74f, .78f), new Color(.16f, .18f, .22f), new Color(.62f, .18f, .16f),
            new Color(.15f, .32f, .55f), new Color(.85f, .86f, .88f), new Color(.22f, .42f, .28f)
        };
        if (type == AIVehicleController.VehicleType.Bus) return new Color(.88f, .86f, .82f);
        if (type == AIVehicleController.VehicleType.Emergency) return new Color(.86f, .16f, .14f);
        int hash = 17;
        for (int i = 0; i < stem.Length; i++) hash = unchecked(hash * 31 + stem[i]);
        return palette[Mathf.Abs(hash + index) % palette.Length];
    }

    /// <summary>
    /// Which way a normalised model faces along Z. Every named part votes with the
    /// evidence its name carries - a headlight, a windscreen, the steering wheel, the
    /// front wheels, the driver's door - for the side of the model centre it sits on, and
    /// the votes are summed. Names are read from the part and from its materials, because
    /// a pack names its parts in one place or the other: these downloads label the wheel
    /// 'FL' but put 'Headlights' on a shared material. A model whose parts say nothing
    /// either way is left exactly as authored.
    /// </summary>
    static int FacingFromParts(GameObject root, float centerZ, float halfLength, out string detail)
    {
        float partVotes = 0f, materialVotes = 0f;
        float deadband = Mathf.Max(.05f, Mathf.Abs(halfLength) * .08f);
        var evidence = new List<string>();

        foreach (var part in root.GetComponentsInChildren<Transform>(true))
        {
            if (part == null || part == root.transform) continue;
            float z = ZOf(part);
            float vote = TrafficModelRules.FacingVote(TrafficModelRules.NoseEvidence(part.name), z, centerZ, deadband);
            if (Mathf.Abs(vote) > .01f)
            {
                partVotes += vote;
                evidence.Add($"{part.name}@{z:F2}{(vote > 0 ? "+" : "-")}");
            }

            var renderer = part.GetComponent<Renderer>();
            if (renderer == null) continue;
            foreach (var material in renderer.sharedMaterials)
            {
                if (material == null) continue;
                float materialVote = TrafficModelRules.FacingVote(TrafficModelRules.NoseEvidence(material.name), z, centerZ, deadband);
                if (Mathf.Abs(materialVote) > .01f) materialVotes += materialVote;
            }
        }

        int facing = TrafficModelRules.FacingFromVotes(partVotes);
        string source = "parts";
        if (facing == 0)
        {
            // Only the materials said anything, and they are shared between parts, so
            // they need a heavier majority before they may turn a model around.
            facing = TrafficModelRules.FacingFromVotes(materialVotes, 1.5f);
            source = "materials";
        }

        if (facing == 0)
        {
            detail = evidence.Count > 0
                ? $"balanced evidence, centre {centerZ:F1} ({string.Join(", ", evidence)})"
                : $"no named end, centre {centerZ:F1}";
            return 0;
        }

        float margin = source == "parts" ? partVotes : materialVotes;
        string seen = evidence.Count > 0 ? string.Join(", ", evidence) : $"{materialVotes:F2} from materials";
        detail = $"{margin.ToString("+0.00;-0.00")} on {source} ({seen})";
        return facing;
    }

    /// <summary>
    /// Centre along Z of a part: its own mesh when it is a renderer, otherwise the combined
    /// bounds of the meshes under it, and finally where the node itself sits. Download packs
    /// nest their named parts either way round.
    /// </summary>
    static float ZOf(Transform part)
    {
        var own = part.GetComponent<Renderer>();
        if (own != null) return own.bounds.center.z;

        var children = part.GetComponentsInChildren<Renderer>(true);
        if (children.Length == 0) return part.position.z;
        Bounds bounds = children[0].bounds;
        for (int i = 1; i < children.Length; i++) bounds.Encapsulate(children[i].bounds);
        return bounds.center.z;
    }

    static Bounds BoundsOf(Renderer[] renderers)
    {
        Bounds bounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++) bounds.Encapsulate(renderers[i].bounds);
        return bounds;
    }

    static int CountTriangles(GameObject root)
    {
        int total = 0;
        foreach (var filter in root.GetComponentsInChildren<MeshFilter>(true)) total += Triangles(filter.sharedMesh);
        foreach (var skin in root.GetComponentsInChildren<SkinnedMeshRenderer>(true)) total += Triangles(skin.sharedMesh);
        return total;
    }

    static int Triangles(Mesh mesh)
    {
        if (mesh == null) return 0;
        int total = 0;
        for (int i = 0; i < mesh.subMeshCount; i++) total += (int)mesh.GetIndexCount(i) / 3;
        return total;
    }

    static string Sanitise(string value)
    {
        var builder = new System.Text.StringBuilder();
        foreach (char c in value)
            builder.Append(char.IsLetterOrDigit(c) ? c : '_');
        string result = builder.ToString().Trim('_');
        return string.IsNullOrEmpty(result) ? "Vehicle" : result;
    }
}
#endif
