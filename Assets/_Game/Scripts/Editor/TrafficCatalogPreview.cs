#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// Renders every prefab in the traffic catalog to a single image.
///
/// Traffic was shipped pink and untextured twice without anyone noticing, because
/// the pool is only observable by driving around. This tool puts the imported models
/// side by side in an empty scene, lit by a known light, and writes one PNG, so a
/// broken import (null materials, wrong scale, backwards facing, a missing atlas) is
/// visible in a second:
///
/// <code>
/// Unity -batchmode -nographics -projectPath . -executeMethod TrafficCatalogPreview.Capture
/// </code>
///
/// It touches nothing in the project: the scene it builds is unsaved and discarded
/// when the editor exits.
/// </summary>
public static class TrafficCatalogPreview
{
    const string CatalogPath = "Assets/_Game/Resources/TrafficCatalog.asset";
    const string OutputPath = "/tmp/realbus-traffic-models.png";

    [MenuItem("Tools/RealBus/Diagnostics/Capture Traffic Models")]
    public static void Capture()
    {
        if (!Application.isBatchMode)
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        var catalog = AssetDatabase.LoadAssetAtPath<TrafficCatalog>(CatalogPath);
        if (catalog == null || catalog.UsableCount == 0)
        {
            Debug.LogError($"[TrafficPreview] No usable catalog at '{CatalogPath}'. Run Tools > RealBus > Setup > Build Traffic Models first.");
            return;
        }

        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        var lightObject = new GameObject("Preview sun");
        var light = lightObject.AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1.1f;
        lightObject.transform.rotation = Quaternion.Euler(42f, 138f, 0f);

        float cursor = 0f;
        var placed = new System.Collections.Generic.List<GameObject>();
        foreach (var entry in catalog.entries)
        {
            if (entry == null || entry.prefab == null) continue;
            var instance = (GameObject)PrefabUtility.InstantiatePrefab(entry.prefab);
            float width = Mathf.Max(1f, entry.bounds.size.x);
            float length = Mathf.Max(1f, entry.lengthMeters);
            cursor += width * .5f + (placed.Count > 0 ? .6f : 0f);
            instance.transform.position = new Vector3(cursor, 0f, 0f);
            instance.transform.rotation = Quaternion.Euler(0f, 205f, 0f);
            cursor += Mathf.Max(width, length) * .5f;
            placed.Add(instance);
        }

        if (placed.Count == 0)
        {
            Debug.LogError("[TrafficPreview] The catalog has entries but none could be instantiated.");
            return;
        }

        var cameraObject = new GameObject("Preview camera");
        var camera = cameraObject.AddComponent<Camera>();
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(.22f, .24f, .27f);
        float middle = cursor * .5f;
        float distance = Mathf.Max(10f, cursor * .75f);
        camera.transform.position = new Vector3(middle, distance * .35f, -distance * .8f);
        camera.transform.LookAt(new Vector3(middle, .9f, 0f));

        const int widthPixels = 1600;
        const int heightPixels = 600;
        var target = RenderTexture.GetTemporary(widthPixels, heightPixels, 24);
        var previous = RenderTexture.active;
        var image = new Texture2D(widthPixels, heightPixels, TextureFormat.RGB24, false);
        try
        {
            camera.targetTexture = target;
            camera.Render();
            RenderTexture.active = target;
            image.ReadPixels(new Rect(0, 0, widthPixels, heightPixels), 0, 0);
            image.Apply();
            System.IO.File.WriteAllBytes(OutputPath, image.EncodeToPNG());
        }
        finally
        {
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(target);
            Object.DestroyImmediate(image);
        }

        foreach (var instance in placed)
        {
            if (instance == null) continue;
            foreach (var renderer in instance.GetComponentsInChildren<Renderer>(true))
            {
                var material = renderer.sharedMaterial;
                if (material == null)
                    Debug.LogWarning($"[TrafficPreview] '{instance.name}/{renderer.name}' has no material.");
                else if (material.mainTexture == null)
                    Debug.Log($"[TrafficPreview] '{instance.name}/{renderer.name}' uses '{material.name}' without an albedo texture (painted).");
            }
        }

        Debug.Log($"[TrafficPreview] Wrote {OutputPath} with {placed.Count} models: {string.Join(", ", placed.ConvertAll(p => p.name))}.");
        if (Application.isBatchMode) EditorApplication.Exit(0);
    }
}
#endif
