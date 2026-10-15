#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Configures ModelImporter settings for parking lot and depot assets.
/// Automatically calculates missing normals/tangents and disables empty animations.
/// </summary>
public class ParkingLotModelPostprocessor : AssetPostprocessor
{
    const string TargetFolder = "Assets/unity models/parking lot";

    void OnPreprocessModel()
    {
        if (!assetPath.Replace('\\', '/').Contains(TargetFolder))
            return;

        ModelImporter importer = assetImporter as ModelImporter;
        if (importer == null) return;

        ApplyCleanImportSettings(importer);
    }

    static void ApplyCleanImportSettings(ModelImporter importer)
    {
        // Calculate normals for all submeshes (fixing meshes that lack authored normals)
        importer.importNormals = ModelImporterNormals.Calculate;
        importer.normalCalculationMode = ModelImporterNormalCalculationMode.AreaAndAngleWeighted;
        importer.importTangents = ModelImporterTangents.CalculateMikk;

        // Turn off animations for static environment / architecture props (fixing 0-length anim clip warnings)
        importer.importAnimation = false;

        // Geometry cleanup
        importer.weldVertices = true;
        importer.optimizeMeshVertices = true;
        importer.optimizeMeshPolygons = true;
    }

    [InitializeOnLoadMethod]
    static void CheckAndApplyImportSettings()
    {
        EditorApplication.delayCall += () =>
        {
            if (Application.isPlaying || !Directory.Exists(TargetFolder)) return;

            bool anyChanged = false;
            foreach (string file in Directory.GetFiles(TargetFolder, "*.fbx"))
            {
                string path = file.Replace('\\', '/');
                ModelImporter importer = AssetImporter.GetAtPath(path) as ModelImporter;
                if (importer == null) continue;

                if (importer.importNormals != ModelImporterNormals.Calculate || importer.importAnimation)
                {
                    ApplyCleanImportSettings(importer);
                    importer.SaveAndReimport();
                    anyChanged = true;
                }
            }

            if (anyChanged)
            {
                Debug.Log("[ParkingLotModelPostprocessor] Cleaned up parking lot FBX import settings (calculated normals, removed empty animations).");
            }
        };
    }
}
#endif
