#if UNITY_EDITOR
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CityJsonBulkDownloader : EditorWindow
{
    readonly Dictionary<string, bool> selectedByPath = new Dictionary<string, bool>();
    string searchQuery = "";
    bool skipIfJsonExists = true;
    Vector2 scroll;

    [MenuItem("Tools/RealBus/Cities/Bulk Download Roads + Stops JSON")]
    public static void Open()
    {
        GetWindow<CityJsonBulkDownloader>("City JSON Bulk");
    }

    void OnGUI()
    {
        GUILayout.Label("Bulk Download Roads + Stops JSON", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "Downloads tiled roads.json and stops.json for selected CityDefinition assets " +
            "into Assets/StreamingAssets/Cities/<CITYCODE>/ using the local fetch script.",
            MessageType.Info);

        searchQuery = EditorGUILayout.TextField("Search", searchQuery);
        skipIfJsonExists = EditorGUILayout.Toggle("Skip cities with existing JSON", skipIfJsonExists);

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Select Visible"))
                SetVisibleSelection(true);
            if (GUILayout.Button("Clear Visible"))
                SetVisibleSelection(false);
            if (GUILayout.Button("Select All"))
                SetAllSelection(true);
            if (GUILayout.Button("Clear All"))
                SetAllSelection(false);
        }

        var cities = GetFilteredCities();
        EditorGUILayout.Space();
        GUILayout.Label($"Visible cities: {cities.Count}", EditorStyles.boldLabel);

        scroll = EditorGUILayout.BeginScrollView(scroll);
        foreach (var city in cities)
        {
            string path = AssetDatabase.GetAssetPath(city);
            bool selected = selectedByPath.TryGetValue(path, out var value) && value;

            using (new EditorGUILayout.HorizontalScope())
            {
                bool next = EditorGUILayout.Toggle(selected, GUILayout.Width(18));
                if (next != selected)
                    selectedByPath[path] = next;

                string jsonStatus = GetJsonStatus(city);
                EditorGUILayout.LabelField($"{city.country} / {city.cityName} ({city.cityCode})", GUILayout.Width(260));
                EditorGUILayout.LabelField(jsonStatus);
            }
        }
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();
        if (GUILayout.Button("Download Selected Cities", GUILayout.Height(36)))
            DownloadSelectedCities();
    }

    void DownloadSelectedCities()
    {
        var selectedCities = GetAllCities()
            .Where(c =>
            {
                string path = AssetDatabase.GetAssetPath(c);
                return selectedByPath.TryGetValue(path, out var selected) && selected;
            })
            .ToList();

        if (selectedCities.Count == 0)
        {
            EditorUtility.DisplayDialog("No Cities Selected", "Select at least one city first.", "OK");
            return;
        }

        string scriptPath = Path.Combine(ProjectRoot(), "scripts", "fetch_osm_city_json.sh");
        if (!File.Exists(scriptPath))
        {
            EditorUtility.DisplayDialog("Missing Script", $"Could not find helper script:\n{scriptPath}", "OK");
            return;
        }

        int completed = 0;
        int skipped = 0;
        int failed = 0;

        try
        {
            for (int i = 0; i < selectedCities.Count; i++)
            {
                var city = selectedCities[i];
                string title = $"Downloading {city.cityCode} ({i + 1}/{selectedCities.Count})";
                string info = $"{city.cityName}, {city.country}";
                if (EditorUtility.DisplayCancelableProgressBar("City JSON Bulk Download", $"{title}\n{info}", (float)i / selectedCities.Count))
                    break;

                if (skipIfJsonExists && CityJsonExists(city))
                {
                    skipped++;
                    continue;
                }

                bool ok = RunFetchScript(scriptPath, city, out string output);
                if (ok)
                {
                    completed++;
                    Debug.Log($"[CityJsonBulk] Downloaded {city.cityCode}\n{output}");
                }
                else
                {
                    failed++;
                    Debug.LogError($"[CityJsonBulk] Failed for {city.cityCode}\n{output}");
                }
            }
        }
        finally
        {
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

        EditorUtility.DisplayDialog(
            "Bulk Download Complete",
            $"Downloaded: {completed}\nSkipped: {skipped}\nFailed: {failed}",
            "OK");
    }

    bool RunFetchScript(string scriptPath, CityDefinition city, out string combinedOutput)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments =
                $"{Quote(scriptPath)} " +
                $"{Quote(city.cityCode)} " +
                $"{city.minLat.ToString(CultureInfo.InvariantCulture)} " +
                $"{city.minLon.ToString(CultureInfo.InvariantCulture)} " +
                $"{city.maxLat.ToString(CultureInfo.InvariantCulture)} " +
                $"{city.maxLon.ToString(CultureInfo.InvariantCulture)}",
            WorkingDirectory = ProjectRoot(),
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (var process = new Process { StartInfo = psi })
        {
            process.Start();
            string stdout = process.StandardOutput.ReadToEnd();
            string stderr = process.StandardError.ReadToEnd();
            process.WaitForExit();

            combinedOutput = string.IsNullOrWhiteSpace(stderr) ? stdout : $"{stdout}\n{stderr}".Trim();
            return process.ExitCode == 0;
        }
    }

    List<CityDefinition> GetFilteredCities()
    {
        string query = searchQuery?.Trim().ToLowerInvariant();
        return GetAllCities()
            .Where(c =>
                string.IsNullOrWhiteSpace(query) ||
                c.cityName.ToLowerInvariant().Contains(query) ||
                c.country.ToLowerInvariant().Contains(query) ||
                c.cityCode.ToLowerInvariant().Contains(query))
            .OrderBy(c => c.country)
            .ThenBy(c => c.cityName)
            .ToList();
    }

    List<CityDefinition> GetAllCities()
    {
        var result = new List<CityDefinition>();
        string[] guids = AssetDatabase.FindAssets("t:CityDefinition");
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            var city = AssetDatabase.LoadAssetAtPath<CityDefinition>(path);
            if (city != null)
                result.Add(city);
        }
        return result;
    }

    void SetVisibleSelection(bool selected)
    {
        foreach (var city in GetFilteredCities())
            selectedByPath[AssetDatabase.GetAssetPath(city)] = selected;
    }

    void SetAllSelection(bool selected)
    {
        foreach (var city in GetAllCities())
            selectedByPath[AssetDatabase.GetAssetPath(city)] = selected;
    }

    string GetJsonStatus(CityDefinition city)
    {
        return CityJsonExists(city) ? "JSON ready" : "JSON missing";
    }

    bool CityJsonExists(CityDefinition city)
    {
        string baseDir = Path.Combine(ProjectRoot(), "Assets", "StreamingAssets", "Cities", city.cityCode.ToUpperInvariant());
        return File.Exists(Path.Combine(baseDir, "roads.json")) &&
               File.Exists(Path.Combine(baseDir, "stops.json"));
    }

    static string ProjectRoot()
    {
        return Directory.GetParent(Application.dataPath).FullName;
    }

    static string Quote(string value)
    {
        return "\"" + value.Replace("\"", "\\\"") + "\"";
    }
}
#endif
