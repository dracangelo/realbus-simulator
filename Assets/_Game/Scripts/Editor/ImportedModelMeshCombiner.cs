#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Collapses the hundreds of small renderers inside an imported scene into one
/// mesh per material. The city and station packs are authored as whole scenes:
/// a single tower can arrive as 1 300 separate window and trim objects, and each
/// one would otherwise cost a draw call for the rest of the session. Combining is
/// editor-only and keeps the original materials, so the visual result is the same.
/// </summary>
public static class ImportedModelMeshCombiner
{
    /// <summary>
    /// Combines every <see cref="MeshRenderer"/> under <paramref name="root"/> into
    /// one mesh per material. Renderers whose meshes carry several submeshes are
    /// split per submesh first, because the city packs build every window as a
    /// two-material mesh and skipping those would leave the draw count untouched.
    /// The pass is all-or-nothing: if any material group cannot be combined the
    /// original renderers are left untouched rather than half replaced.
    /// Returns the renderer count before and after so the report can show the saving.
    /// </summary>
    public static (int before, int after) CombineByMaterial(GameObject root, string meshFolder, string stem)
    {
        if (root == null) return (0, 0);
        EnsureFolder(meshFolder);
        ClearCombinedMeshes(meshFolder, stem);

        var renderers = root.GetComponentsInChildren<MeshRenderer>(true);
        int before = renderers.Length;
        var groups = new Dictionary<Material, List<(MeshRenderer renderer, int subMesh)>>();
        var slots = new Dictionary<MeshRenderer, Material[]>();
        foreach (var renderer in renderers)
        {
            if (renderer == null) continue;
            var filter = renderer.GetComponent<MeshFilter>();
            if (filter == null || filter.sharedMesh == null || filter.sharedMesh.vertexCount == 0) continue;
            Material[] materials = renderer.sharedMaterials;
            if (materials.Length == 0 || materials.Length > filter.sharedMesh.subMeshCount) continue;

            bool complete = true;
            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i] == null) { complete = false; break; }
            }
            if (!complete) continue;

            slots[renderer] = materials;
            for (int i = 0; i < materials.Length; i++)
            {
                if (!groups.TryGetValue(materials[i], out var list))
                {
                    list = new List<(MeshRenderer, int)>();
                    groups[materials[i]] = list;
                }
                list.Add((renderer, i));
            }
        }

        // A renderer may only be consumed when every one of its submeshes is
        // combined. Groups are pruned until that holds, so a material used by a
        // single renderer can never swallow a renderer whose other submesh would
        // be left behind, and no geometry is dropped or drawn twice.
        var eligible = new HashSet<Material>();
        foreach (var pair in groups)
            if (pair.Value.Count >= 2) eligible.Add(pair.Key);

        bool changed = true;
        while (changed)
        {
            changed = false;
            var drop = new List<Material>();
            foreach (var material in eligible)
            {
                int usable = 0;
                foreach (var entry in groups[material])
                    if (FullyEligible(entry.renderer, slots, eligible)) usable++;
                if (usable >= 2) continue;
                drop.Add(material);
                changed = true;
            }
            foreach (var material in drop) eligible.Remove(material);
        }

        int index = 0;
        bool failed = false;
        var createdMeshes = new List<Mesh>();
        var createdObjects = new List<GameObject>();
        var consumed = new List<MeshRenderer>();
        foreach (var pair in groups)
        {
            if (!eligible.Contains(pair.Key)) continue;
            var entries = new List<(MeshRenderer renderer, int subMesh)>();
            foreach (var entry in pair.Value)
                if (FullyEligible(entry.renderer, slots, eligible)) entries.Add(entry);
            if (entries.Count < 2) continue;
            try
            {
                var combines = new CombineInstance[entries.Count];
                long vertexCount = 0;
                for (int i = 0; i < entries.Count; i++)
                {
                    var filter = entries[i].renderer.GetComponent<MeshFilter>();
                    combines[i].mesh = filter.sharedMesh;
                    combines[i].subMeshIndex = entries[i].subMesh;
                    combines[i].transform = root.transform.worldToLocalMatrix * entries[i].renderer.transform.localToWorldMatrix;
                    // The combined mesh keeps a vertex per source submesh, so the
                    // full mesh count is the safe estimate for the index format.
                    vertexCount += filter.sharedMesh.vertexCount;
                }

                var mesh = new Mesh { name = pair.Key.name + " combined" };
                if (vertexCount > 65535) mesh.indexFormat = IndexFormat.UInt32;
                mesh.CombineMeshes(combines, true, true);
                mesh.RecalculateBounds();

                string meshPath = $"{meshFolder}/{stem}_combined_{index++}.asset";
                if (AssetDatabase.LoadAssetAtPath<Mesh>(meshPath) != null)
                    AssetDatabase.DeleteAsset(meshPath);
                AssetDatabase.CreateAsset(mesh, meshPath);
                createdMeshes.Add(mesh);

                var combined = new GameObject("Combined_" + SafeName(pair.Key.name));
                combined.transform.SetParent(root.transform, false);
                combined.AddComponent<MeshFilter>().sharedMesh = mesh;
                var combinedRenderer = combined.AddComponent<MeshRenderer>();
                combinedRenderer.sharedMaterial = pair.Key;
                combinedRenderer.shadowCastingMode = entries[0].renderer.shadowCastingMode;
                combinedRenderer.receiveShadows = entries[0].renderer.receiveShadows;
                createdObjects.Add(combined);

                foreach (var entry in entries)
                    if (!consumed.Contains(entry.renderer)) consumed.Add(entry.renderer);
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"[MeshCombiner] '{stem}' left unchanged: '{pair.Key.name}' could not be combined ({exception.Message})");
                failed = true;
                break;
            }
        }

        if (failed)
        {
            foreach (var created in createdObjects)
                if (created != null) UnityEngine.Object.DestroyImmediate(created);
            foreach (var mesh in createdMeshes)
            {
                string path = AssetDatabase.GetAssetPath(mesh);
                if (!string.IsNullOrEmpty(path)) AssetDatabase.DeleteAsset(path);
            }
            return (before, before);
        }

        foreach (var renderer in consumed)
        {
            if (renderer == null) continue;
            var filter = renderer.GetComponent<MeshFilter>();
            if (filter != null) UnityEngine.Object.DestroyImmediate(filter);
            UnityEngine.Object.DestroyImmediate(renderer);
        }

        // Combining leaves one empty GameObject per consumed source renderer: a
        // tower authored as 1 300 window objects would still carry 1 300 transforms
        // into the scene. Drop the leaves that no longer draw or hold anything.
        PruneEmptyTransforms(root.transform);

        int after = root.GetComponentsInChildren<MeshRenderer>(true).Length;
        return (before, after);
    }

    static bool PruneEmptyTransforms(Transform transform)
    {
        bool keeps = transform.GetComponents<Component>().Length > 1;
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            if (PruneEmptyTransforms(child)) keeps = true;
        }
        if (keeps || transform.parent == null) return true;
        UnityEngine.Object.DestroyImmediate(transform.gameObject);
        return false;
    }

    static bool FullyEligible(MeshRenderer renderer, Dictionary<MeshRenderer, Material[]> slots, HashSet<Material> eligible)
    {
        if (!slots.TryGetValue(renderer, out var materials)) return false;
        for (int i = 0; i < materials.Length; i++)
            if (!eligible.Contains(materials[i])) return false;
        return true;
    }

    static string SafeName(string value)
    {
        if (string.IsNullOrEmpty(value)) return "Material";
        var builder = new System.Text.StringBuilder(value.Length);
        foreach (char c in value)
            builder.Append(char.IsLetterOrDigit(c) ? c : '_');
        return builder.ToString().Trim('_');
    }

    /// <summary>
    /// Removes the combined meshes a previous run wrote for this prefab. Without
    /// this a rebuild that needs fewer material groups leaves orphaned meshes
    /// behind, and every re-import adds more.
    /// </summary>
    static void ClearCombinedMeshes(string meshFolder, string stem)
    {
        if (!Directory.Exists(meshFolder)) return;
        foreach (string file in Directory.GetFiles(meshFolder, stem + "_combined_*.asset"))
            AssetDatabase.DeleteAsset(file.Replace('\\', '/'));
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
