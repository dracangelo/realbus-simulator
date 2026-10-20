#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Renders every imported building prefab to one contact sheet, so a missing
/// texture, a wrong scale or a sideways facade is visible without driving the
/// route. The scene is unsaved and thrown away when the editor exits.
/// </summary>
public static class BuildingCatalogPreview
{
    const string CatalogPath = "Assets/_Game/Resources/BuildingCatalog.asset";
    const string OutputPath = "/tmp/realbus-building-models.png";
    const int TileSize = 512;
    const int Columns = 4;

    [MenuItem("Tools/RealBus/Diagnostics/Capture Building Models")]
    public static void Capture()
    {
        if (!Application.isBatchMode)
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        var catalog = AssetDatabase.LoadAssetAtPath<BuildingCatalog>(CatalogPath);
        if (catalog == null || catalog.entries == null || catalog.entries.Length == 0)
        {
            Debug.LogError($"[BuildingPreview] No catalog at '{CatalogPath}'. Run Tools > RealBus > Setup > Build Building Models first.");
            return;
        }

        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        RenderSettings.ambientMode = AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(.42f, .44f, .47f);

        var keyObject = new GameObject("Preview key");
        var key = keyObject.AddComponent<Light>();
        key.type = LightType.Directional;
        key.intensity = 1.2f;
        key.shadows = LightShadows.None;
        keyObject.transform.rotation = Quaternion.Euler(38f, 132f, 0f);

        var fillObject = new GameObject("Preview fill");
        var fill = fillObject.AddComponent<Light>();
        fill.type = LightType.Directional;
        fill.intensity = .35f;
        fill.shadows = LightShadows.None;
        fillObject.transform.rotation = Quaternion.Euler(18f, -48f, 0f);

        var cameraObject = new GameObject("Preview camera");
        var camera = cameraObject.AddComponent<Camera>();
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(.17f, .19f, .22f);
        camera.orthographic = true;
        camera.nearClipPlane = .1f;
        camera.aspect = 1f;
        camera.allowHDR = false;
        camera.allowMSAA = true;

        int rows = Mathf.CeilToInt(catalog.entries.Length / (float)Columns);
        var sheet = new Texture2D(TileSize * Columns, TileSize * rows, TextureFormat.RGB24, false);
        var target = RenderTexture.GetTemporary(TileSize, TileSize, 24, RenderTextureFormat.ARGB32);
        var previous = RenderTexture.active;
        var names = new List<string>();
        try
        {
            camera.targetTexture = target;
            for (int i = 0; i < catalog.entries.Length; i++)
            {
                var entry = catalog.entries[i];
                if (entry == null || entry.prefab == null) continue;
                var instance = (GameObject)PrefabUtility.InstantiatePrefab(entry.prefab);
                instance.transform.position = Vector3.zero;
                instance.transform.rotation = Quaternion.identity;

                float height = Mathf.Max(1f, entry.bounds.size.y);
                float width = Mathf.Max(1f, Mathf.Max(entry.bounds.size.x, entry.bounds.size.z));
                float extent = Mathf.Max(height, width);
                Vector3 center = new Vector3(0f, height * .5f, 0f);
                Quaternion look = Quaternion.Euler(18f, 215f, 0f);
                float distance = extent * 3f;
                camera.transform.position = center - look * Vector3.forward * distance;
                camera.transform.rotation = look;
                camera.orthographicSize = extent * .72f;
                camera.farClipPlane = distance + extent * 3f + 100f;

                camera.Render();
                RenderTexture.active = target;
                int column = i % Columns;
                int row = i / Columns;
                sheet.ReadPixels(new Rect(0, 0, TileSize, TileSize),
                    column * TileSize, (rows - 1 - row) * TileSize);
                RenderTexture.active = null;

                foreach (var renderer in instance.GetComponentsInChildren<Renderer>(true))
                    foreach (var material in renderer.sharedMaterials)
                    {
                        if (material == null) Debug.LogWarning($"[BuildingPreview] {entry.sourceName}/{renderer.name} has a null material.");
                        else if (material.mainTexture == null)
                            Debug.Log($"[BuildingPreview] {entry.sourceName}/{renderer.name} uses painted material '{material.name}'.");
                    }
                names.Add(entry.sourceName);
                Object.DestroyImmediate(instance);
            }
            sheet.Apply();
            File.WriteAllBytes(OutputPath, sheet.EncodeToPNG());
        }
        finally
        {
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(target);
            camera.targetTexture = null;
            Object.DestroyImmediate(sheet);
        }

        Debug.Log($"[BuildingPreview] Wrote {OutputPath} with {names.Count} buildings: {string.Join(", ", names)}.");
        if (Application.isBatchMode) EditorApplication.Exit(0);
    }
}
#endif
