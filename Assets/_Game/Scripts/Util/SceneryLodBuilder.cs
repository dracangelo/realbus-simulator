using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Adds the missing three-level LOD group to a streamed building or street prop.
///
/// LOD0 keeps the real renderers. LOD1 is a shared box proxy with the lit scene
/// material, and LOD2 is the same box with an unlit material and no shadow pass.
/// The proxy shares one cube mesh and two materials across every instance, so the
/// added memory is a transform and two renderers per object, not a decimated copy
/// of the model. A model that already ships a LODGroup is left alone: rebuilding
/// it would discard the artist's own levels.
/// </summary>
public static class SceneryLodBuilder
{
    static Mesh cubeMesh;
    static Material litProxyMaterial;
    static Material unlitProxyMaterial;

    /// <summary>
    /// Builds a LODGroup on <paramref name="root"/> if it does not already have
    /// one. Returns true when a group was added. <paramref name="localBounds"/>
    /// must be in the root's local space, because the proxy is a child of the root.
    /// </summary>
    public static bool Attach(GameObject root, Bounds localBounds, float lod1ScreenHeight,
        float lod2ScreenHeight, Color tint, bool castShadows = true)
    {
        if (root == null)
            return false;
        if (root.GetComponentInChildren<LODGroup>(true) != null)
            return false;
        if (localBounds.size.sqrMagnitude <= .0001f)
            return false;

        Renderer[] detail = root.GetComponentsInChildren<Renderer>(true);
        if (detail.Length == 0)
            return false;

        Material lit = LitProxyMaterial;
        Material unlit = UnlitProxyMaterial;
        if (lit == null || unlit == null)
            return false;

        // LODGroup requires strictly descending screen heights. Guard the caller
        // rather than silently creating a group Unity will reject.
        float lod2 = Mathf.Clamp(lod2ScreenHeight, .005f, .49f);
        float lod1 = Mathf.Clamp(lod1ScreenHeight, lod2 + .001f, .9f);

        var proxy = new GameObject("LOD_Proxy");
        proxy.transform.SetParent(root.transform, false);
        proxy.transform.localPosition = localBounds.center;
        proxy.transform.localRotation = Quaternion.identity;
        Vector3 size = localBounds.size;
        size.x = Mathf.Max(.05f, size.x);
        size.y = Mathf.Max(.05f, size.y);
        size.z = Mathf.Max(.05f, size.z);
        proxy.transform.localScale = size;

        // A MeshRenderer cannot share a GameObject with another MeshRenderer, so
        // each level gets its own child. Both use the same cube mesh and the same
        // two shared materials; only the transform and the two renderers are new.
        MeshRenderer near = CreateProxy(proxy.transform, "LOD1", lit, tint, castShadows);
        MeshRenderer far = CreateProxy(proxy.transform, "LOD2", unlit, tint, false);

        var lods = new[]
        {
            new LOD(lod1, detail),
            new LOD(lod2, new Renderer[] { near }),
            new LOD(.005f, new Renderer[] { far })
        };
        var group = root.AddComponent<LODGroup>();
        group.SetLODs(lods);
        group.RecalculateBounds();
        group.fadeMode = LODFadeMode.None;
        group.lastLODBillboard = false;
        return true;
    }

    static MeshRenderer CreateProxy(Transform parent, string name, Material material, Color tint, bool castShadows)
    {
        var child = new GameObject(name);
        child.transform.SetParent(parent, false);
        child.transform.localPosition = Vector3.zero;
        child.transform.localRotation = Quaternion.identity;
        child.transform.localScale = Vector3.one;

        var filter = child.AddComponent<MeshFilter>();
        filter.sharedMesh = CubeMesh;

        var renderer = child.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = material;
        renderer.shadowCastingMode = castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off;
        renderer.receiveShadows = castShadows;
        renderer.lightProbeUsage = LightProbeUsage.Off;
        renderer.reflectionProbeUsage = ReflectionProbeUsage.Off;

        var block = new MaterialPropertyBlock();
        block.SetColor("_Color", tint);
        renderer.SetPropertyBlock(block);
        return renderer;
    }

    /// <summary>Triangle count of a generated mesh, without allocating an index array.</summary>
    public static int TriangleCount(Mesh mesh)
    {
        if (mesh == null)
            return 0;
        int total = 0;
        for (int i = 0; i < mesh.subMeshCount; i++)
            total += (int)mesh.GetIndexCount(i) / 3;
        return total;
    }

    static Mesh CubeMesh
    {
        get
        {
            if (cubeMesh == null)
                cubeMesh = Resources.GetBuiltinResource<Mesh>("Cube.fbx");
            if (cubeMesh == null)
            {
                // Player builds can strip the built-in resource; a primitive is a
                // cheap one-off fallback and its mesh is shared, not owned.
                var temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubeMesh = temp.GetComponent<MeshFilter>().sharedMesh;
                DestroySafe(temp);
            }
            return cubeMesh;
        }
    }

    static Material LitProxyMaterial
    {
        get
        {
            if (litProxyMaterial == null)
            {
                Shader shader = BuiltInShaders.Lit;
                if (shader == null)
                    return null;
                litProxyMaterial = new Material(shader)
                {
                    name = "Scenery LOD Lit",
                    hideFlags = HideFlags.DontSave
                };
                if (litProxyMaterial.HasProperty("_Color"))
                    litProxyMaterial.SetColor("_Color", new Color(.72f, .71f, .68f));
            }
            return litProxyMaterial;
        }
    }

    static Material UnlitProxyMaterial
    {
        get
        {
            if (unlitProxyMaterial == null)
            {
                Shader shader = BuiltInShaders.UnlitColor;
                if (shader == null)
                    return null;
                unlitProxyMaterial = new Material(shader)
                {
                    name = "Scenery LOD Unlit",
                    hideFlags = HideFlags.DontSave
                };
                if (unlitProxyMaterial.HasProperty("_Color"))
                    unlitProxyMaterial.SetColor("_Color", new Color(.62f, .62f, .60f));
            }
            return unlitProxyMaterial;
        }
    }

    static void DestroySafe(Object obj)
    {
        if (obj == null)
            return;
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            Object.DestroyImmediate(obj);
            return;
        }
#endif
        Object.Destroy(obj);
    }
}
