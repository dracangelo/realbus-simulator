#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;

/// <summary>
/// Forces game-ready FBXs through Unity's Standard material importer. The default
/// Autodesk Interactive path often leaves external PBR maps unassigned even when
/// the files sit beside the FBX.
/// </summary>
public static class ImportedModelImportSettings
{
    public static void EnsureStandard(IEnumerable<string> sourcePaths)
    {
        bool changed = false;
        foreach (string path in sourcePaths)
        {
            var importer = AssetImporter.GetAtPath(path) as ModelImporter;
            if (importer == null) continue;
            if (importer.materialImportMode == ModelImporterMaterialImportMode.ImportStandard &&
                importer.materialLocation == ModelImporterMaterialLocation.InPrefab &&
                importer.materialSearch == ModelImporterMaterialSearch.RecursiveUp)
                continue;

            importer.materialImportMode = ModelImporterMaterialImportMode.ImportStandard;
            importer.materialLocation = ModelImporterMaterialLocation.InPrefab;
            importer.materialSearch = ModelImporterMaterialSearch.RecursiveUp;
            importer.SaveAndReimport();
            changed = true;
        }
        if (changed) AssetDatabase.Refresh();
    }
}
#endif
