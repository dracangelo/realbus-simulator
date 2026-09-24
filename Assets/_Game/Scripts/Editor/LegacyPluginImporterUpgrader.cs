#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class LegacyPluginImporterUpgrader
{
    const string PluginRoot = "Assets/Mapbox/Core/Plugins";

    [MenuItem("Tools/RealBus/Maintenance/Upgrade Legacy Mapbox Plugin Metadata")]
    public static void UpgradeFromMenu()
    {
        int upgraded = UpgradeAll();
        EditorUtility.DisplayDialog("Mapbox plugin metadata", $"Re-saved {upgraded} plugin importers using Unity's current metadata format.", "OK");
    }

    public static void UpgradeFromBatch()
    {
        UpgradeAll();
    }

    [MenuItem("Tools/RealBus/Maintenance/Re-save Legacy Physics Settings")]
    public static void UpgradePhysicsSettingsFromBatch()
    {
        UnityEngine.Object[] settings = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/DynamicsManager.asset");
        for (int i = 0; i < settings.Length; i++)
            if (settings[i] != null) EditorUtility.SetDirty(settings[i]);
        AssetDatabase.SaveAssets();
        Debug.Log("[RealBus] Re-saved physics settings using Unity's current format.");
    }

    public static void UpgradeLegacyMapboxPrefabFromBatch()
    {
        const string path = "Assets/Mapbox/Core/Plugins/Android/UniAndroidPermission/UniAndroidPermission.prefab";
        GameObject root = PrefabUtility.LoadPrefabContents(path);
        try
        {
            PrefabUtility.SaveAsPrefabAsset(root, path);
        }
        finally
        {
            PrefabUtility.UnloadPrefabContents(root);
        }
        AssetDatabase.SaveAssets();
        Debug.Log("[RealBus] Re-saved legacy Mapbox Android permission prefab.");
    }

    static int UpgradeAll()
    {
        if (!Directory.Exists(PluginRoot))
        {
            Debug.LogWarning($"[RealBus] Mapbox plugin directory was not found: {PluginRoot}");
            return 0;
        }

        string[] files = Directory.GetFiles(PluginRoot, "*", SearchOption.AllDirectories);
        int upgraded = 0;
        foreach (string rawPath in files)
        {
            if (rawPath.EndsWith(".meta", StringComparison.OrdinalIgnoreCase))
                continue;

            string path = rawPath.Replace('\\', '/');
            if (AssetImporter.GetAtPath(path) is not PluginImporter importer)
                continue;

            // Writing importer settings through Unity upgrades the serialized
            // PluginImporter schema while preserving platform compatibility.
            EditorUtility.SetDirty(importer);
            importer.SaveAndReimport();
            upgraded++;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
        Debug.Log($"[RealBus] Upgraded {upgraded} Mapbox plugin importers to Unity's current metadata format.");
        return upgraded;
    }
}
#endif
