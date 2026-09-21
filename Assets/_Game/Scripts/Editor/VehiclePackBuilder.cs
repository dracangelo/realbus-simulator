#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class VehiclePackBuilder
{
    const string ImportedSpecsFolder = "Assets/_Game/Resources/BusSpecs/Imported";

    [MenuItem("Tools/RealBus/Content/Build Selected Vehicle Pack")]
    static void BuildSelectedPack()
    {
        BusSpec[] selected = Selection.GetFiltered<BusSpec>(SelectionMode.DeepAssets);
        BuildPack(selected, "selected-buses");
    }

    [MenuItem("Tools/RealBus/Content/Build All Imported Buses Pack")]
    static void BuildImportedPack()
    {
        string[] guids = AssetDatabase.FindAssets("t:BusSpec", new[] { ImportedSpecsFolder });
        var specs = new List<BusSpec>();
        foreach (string guid in guids)
        {
            BusSpec spec = AssetDatabase.LoadAssetAtPath<BusSpec>(AssetDatabase.GUIDToAssetPath(guid));
            if (spec != null) specs.Add(spec);
        }
        BuildPack(specs.ToArray(), "imported-buses");
    }

    static void BuildPack(BusSpec[] specs, string packName)
    {
        if (specs == null || specs.Length == 0)
        {
            EditorUtility.DisplayDialog("RealBus Vehicle Pack", "Select one or more BusSpec assets first.", "OK");
            return;
        }

        var assetPaths = new List<string>();
        foreach (BusSpec spec in specs)
        {
            if (spec == null || spec.drivablePrefab == null)
            {
                Debug.LogWarning($"VehiclePackBuilder: skipped '{spec?.name ?? "missing asset"}' because it has no drivable prefab.");
                continue;
            }
            string path = AssetDatabase.GetAssetPath(spec);
            if (!string.IsNullOrWhiteSpace(path)) assetPaths.Add(path);
        }

        if (assetPaths.Count == 0)
        {
            EditorUtility.DisplayDialog("RealBus Vehicle Pack", "No selected BusSpec has a drivable prefab.", "OK");
            return;
        }

        BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
        string outputDirectory = Path.GetFullPath(Path.Combine("Builds", "VehiclePacks", target.ToString()));
        Directory.CreateDirectory(outputDirectory);
        string safeName = Sanitise(packName) + "-" + target.ToString().ToLowerInvariant() + ".bundle";
        var build = new AssetBundleBuild { assetBundleName = safeName, assetNames = assetPaths.ToArray() };
        AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(outputDirectory, new[] { build },
            BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.ForceRebuildAssetBundle, target);

        if (manifest == null)
        {
            EditorUtility.DisplayDialog("RealBus Vehicle Pack", "Bundle build failed. Check the Console.", "OK");
            return;
        }

        string bundlePath = Path.Combine(outputDirectory, safeName);
        Debug.Log($"VehiclePackBuilder: built {assetPaths.Count} buses for {target}: {bundlePath}");
        EditorUtility.RevealInFinder(bundlePath);
        EditorUtility.DisplayDialog("RealBus Vehicle Pack",
            $"Built {assetPaths.Count} vehicle(s) for {target}.\n\nUpload this .bundle file, then paste its HTTPS URL into Settings > Downloads > Vehicle Packs.", "OK");
    }

    static string Sanitise(string value)
    {
        foreach (char invalid in Path.GetInvalidFileNameChars()) value = value.Replace(invalid, '-');
        return string.IsNullOrWhiteSpace(value) ? "realbus-vehicles" : value.ToLowerInvariant();
    }
}
#endif
