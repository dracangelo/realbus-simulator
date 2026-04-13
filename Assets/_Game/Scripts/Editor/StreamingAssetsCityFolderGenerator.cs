#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class StreamingAssetsCityFolderGenerator : EditorWindow
{
    bool createJsonPlaceholders = true;
    bool createXmlPlaceholders = true;

    [MenuItem("Tools/RealBus/Cities/Create StreamingAssets Folders")]
    public static void Open()
    {
        GetWindow<StreamingAssetsCityFolderGenerator>("City Folders");
    }

    void OnGUI()
    {
        GUILayout.Label("StreamingAssets City Folders", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "Creates Assets/StreamingAssets/Cities/<CITYCODE>/ for every CityDefinition asset. " +
            "Optional placeholder files help the folders appear immediately in the Project window.",
            MessageType.Info);

        createJsonPlaceholders = EditorGUILayout.Toggle("Create JSON placeholders", createJsonPlaceholders);
        createXmlPlaceholders = EditorGUILayout.Toggle("Create XML placeholders", createXmlPlaceholders);

        EditorGUILayout.Space();
        if (GUILayout.Button("Create Folders For All Cities", GUILayout.Height(36)))
            CreateFolders();
    }

    void CreateFolders()
    {
        string[] guids = AssetDatabase.FindAssets("t:CityDefinition");
        if (guids.Length == 0)
        {
            EditorUtility.DisplayDialog("No Cities", "No CityDefinition assets were found.", "OK");
            return;
        }

        string streamingRoot = Path.Combine(Application.dataPath, "StreamingAssets", "Cities");
        Directory.CreateDirectory(streamingRoot);

        int createdFolders = 0;
        int touchedFiles = 0;
        var seenCodes = new HashSet<string>();

        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            var city = AssetDatabase.LoadAssetAtPath<CityDefinition>(assetPath);
            if (city == null)
                continue;

            string cityCode = SanitizeCityCode(city.cityCode);
            if (string.IsNullOrWhiteSpace(cityCode))
            {
                Debug.LogWarning($"[CityFolders] Skipping '{assetPath}' because cityCode is empty.");
                continue;
            }

            if (!seenCodes.Add(cityCode))
                continue;

            string cityFolder = Path.Combine(streamingRoot, cityCode);
            bool existed = Directory.Exists(cityFolder);
            Directory.CreateDirectory(cityFolder);
            if (!existed)
                createdFolders++;

            if (createJsonPlaceholders)
            {
                touchedFiles += EnsureFile(Path.Combine(cityFolder, "roads.json"), "{\n  \"elements\": []\n}\n");
                touchedFiles += EnsureFile(Path.Combine(cityFolder, "stops.json"), "{\n  \"elements\": []\n}\n");
            }

            if (createXmlPlaceholders)
            {
                touchedFiles += EnsureFile(Path.Combine(cityFolder, city.roadsFileName), "<osm version=\"0.6\"></osm>\n");
                touchedFiles += EnsureFile(Path.Combine(cityFolder, city.buildingsFileName), "<osm version=\"0.6\"></osm>\n");
            }
        }

        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog(
            "StreamingAssets Ready",
            $"Processed {seenCodes.Count} city codes.\nCreated {createdFolders} folders.\nCreated {touchedFiles} placeholder files.",
            "OK");
    }

    static int EnsureFile(string path, string contents)
    {
        if (File.Exists(path))
            return 0;

        File.WriteAllText(path, contents);
        return 1;
    }

    static string SanitizeCityCode(string cityCode)
    {
        if (string.IsNullOrWhiteSpace(cityCode))
            return "";

        return cityCode.Trim().ToUpperInvariant();
    }
}
#endif
