#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Imported model packs (FBX, PSD/JPG maps, downloaded asset bundles unpacked into
/// Assets) arrive with their author's texture settings: mipmaps off, anisotropic
/// filtering at 1 and point filtering. A wall or road seen at a grazing angle then
/// shimmers as the camera moves, and a texture stretched over a large surface has no
/// smaller mips to fall back to.
///
/// This pass only touches the two imported-model folders. It turns mipmaps on, adds
/// anisotropic filtering, keeps the filter mode at least trilinear and leaves each
/// texture's Max Size alone so memory use does not jump. The procedural asphalt,
/// facade and roof materials do not use these files; they exist for the props and
/// buses that come from model packs.
/// </summary>
public static class ImportedTextureSharpener
{
    static readonly string[] Roots =
    {
        "Assets/unity models",
        "Assets/_Game/Resources/BussimAssets",
    };

    [MenuItem("Tools/RealBus/Setup/Sharpen Imported Textures")]
    public static void Sharpen()
    {
        var paths = new List<string>();
        foreach (string root in Roots)
        {
            if (!Directory.Exists(root)) continue;
            foreach (string path in Directory.GetFiles(root, "*", SearchOption.AllDirectories))
            {
                string extension = Path.GetExtension(path).ToLowerInvariant();
                if (extension == ".png" || extension == ".jpg" || extension == ".jpeg" ||
                    extension == ".tga" || extension == ".psd" || extension == ".bmp")
                    paths.Add(path.Replace('\\', '/'));
            }
        }

        int changed = 0, scanned = 0;
        try
        {
            for (int i = 0; i < paths.Count; i++)
            {
                if (EditorUtility.DisplayCancelableProgressBar("RealBus", "Sharpening imported textures", i / (float)paths.Count))
                    break;
                var importer = AssetImporter.GetAtPath(paths[i]) as TextureImporter;
                if (importer == null) continue;
                scanned++;

                bool dirty = false;
                // Sprites and UI atlases are sampled 1:1; mipmaps would soften them.
                if (importer.textureType == TextureImporterType.Default && !importer.mipmapEnabled)
                {
                    importer.mipmapEnabled = true;
                    dirty = true;
                }
                if (importer.anisoLevel < 4)
                {
                    importer.anisoLevel = 4;
                    dirty = true;
                }
                if (importer.filterMode == FilterMode.Point)
                {
                    importer.filterMode = FilterMode.Trilinear;
                    dirty = true;
                }
                if (!dirty) continue;

                importer.SaveAndReimport();
                changed++;
            }
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }
        AssetDatabase.Refresh();
        Debug.Log($"[ImportedTextures] Scanned {scanned} imported textures; sharpened {changed} (mipmaps, aniso 4, trilinear). Max Size was left untouched.");
    }
}
#endif
