#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
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

    [InitializeOnLoadMethod]
    static void QueueInitialImport()
    {
        EditorApplication.delayCall += () =>
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode && !File.Exists(CatalogPath)) Build();
        };
    }

    [MenuItem("Tools/RealBus/Setup/Build Live City Models")]
    public static void Build()
    {
        Directory.CreateDirectory(Output);
        AssetDatabase.Refresh();
        var entries = new List<LiveCityCatalog.Entry>();
        var paths = new List<string>();
        foreach (string folder in new[] { "Assets/unity models/environment", "Assets/unity models/billboard", "Assets/unity models/busstop" })
            if (Directory.Exists(folder))
                foreach (string path in Directory.GetFiles(folder, "*", SearchOption.AllDirectories))
                    if (Path.GetExtension(path).Equals(".fbx", StringComparison.OrdinalIgnoreCase)) paths.Add(path.Replace('\\', '/'));
        if (File.Exists(Person)) paths.Add(Person);
        paths.Sort(StringComparer.Ordinal);
        foreach (string path in paths)
        {
            if (!Classify(path, out var kind, out float height)) continue;
            GameObject source = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (source == null) continue;
            string stem = AssetDatabase.AssetPathToGUID(path);
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
                var renderers = root.GetComponentsInChildren<Renderer>(true);
                if (renderers.Length == 0) continue;
                Bounds bounds = BoundsOf(renderers);
                if (bounds.size.y < .001f || float.IsNaN(bounds.size.y) || float.IsInfinity(bounds.size.y)) continue;
                if (kind == LiveCityCatalog.Kind.Tree && Mathf.Max(bounds.size.x, bounds.size.z) > bounds.size.y * 2f)
                {
                    visual.transform.localRotation = Quaternion.Euler(bounds.size.z > bounds.size.x ? -90 : 0, 0, bounds.size.x >= bounds.size.z ? 90 : 0) * visual.transform.localRotation;
                    bounds = BoundsOf(renderers);
                }
                visual.transform.localScale *= height / bounds.size.y;
                bounds = BoundsOf(renderers);
                visual.transform.position -= new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);
                bounds = BoundsOf(renderers);
                int triangles = 0;
                foreach (var filter in root.GetComponentsInChildren<MeshFilter>(true)) triangles += Triangles(filter.sharedMesh);
                foreach (var skin in root.GetComponentsInChildren<SkinnedMeshRenderer>(true)) triangles += Triangles(skin.sharedMesh);
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
                        Material converted = MakeMaterial(original, kind);
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

    static bool Classify(string path, out LiveCityCatalog.Kind kind, out float height)
    {
        string name = Path.GetFileNameWithoutExtension(path).ToLowerInvariant();
        kind = LiveCityCatalog.Kind.Plant; height = 1f;
        if (path == Person) { kind = LiveCityCatalog.Kind.Pedestrian; height = 1.7f; }
        else if (name.Contains("barrier")) return false; // Road builder owns junction-safe barriers.
        else if (path.Contains("/billboard/")) { kind = LiveCityCatalog.Kind.Billboard; height = name.Contains("sign") ? 2.6f : 5f; }
        else if (name.Contains("bench")) { kind = LiveCityCatalog.Kind.Bench; height = .9f; }
        else if (path.Contains("/busstop/")) return false; // Mission spawner owns actual stops.
        else if (name.Contains("tree") || name.Contains("cocos")) { kind = LiveCityCatalog.Kind.Tree; height = 7f; }
        else if (name.Contains("rock")) { kind = LiveCityCatalog.Kind.Rock; height = name.Contains("cliff") ? 2f : .8f; }
        else { height = name.Contains("banana") ? 2.7f : name.Contains("grass") ? .35f : 1.2f; }
        return true;
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

    static Material MakeMaterial(Material source, LiveCityCatalog.Kind kind)
    {
        var result = new Material(BuiltInShaders.Lit);
        result.name = source != null ? source.name : kind.ToString();
        Texture texture = source != null && source.HasProperty("_MainTex") ? source.GetTexture("_MainTex") : null;
        Color color = source != null && source.HasProperty("_Color") ? source.color : Color.white;
        bool neutral = Mathf.Abs(color.r - color.g) < .03f && Mathf.Abs(color.g - color.b) < .03f;
        if (texture == null && neutral && kind != LiveCityCatalog.Kind.Pedestrian)
        {
            string name = result.name.ToLowerInvariant();
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
        if (source != null && source.HasProperty("_BumpMap"))
        { result.SetTexture("_BumpMap", source.GetTexture("_BumpMap")); if (source.GetTexture("_BumpMap") != null) result.EnableKeyword("_NORMALMAP"); }
        if (kind == LiveCityCatalog.Kind.Tree || kind == LiveCityCatalog.Kind.Plant)
        {
            result.SetFloat("_Cutoff", .4f);
            result.EnableKeyword("_ALPHATEST_ON");
            result.SetOverrideTag("RenderType", "TransparentCutout");
            result.renderQueue = (int)RenderQueue.AlphaTest;
            result.SetFloat("_Mode", 1);
        }
        result.enableInstancing = true;
        return result;
    }
}
#endif
