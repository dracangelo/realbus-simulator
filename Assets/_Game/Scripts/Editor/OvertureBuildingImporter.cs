using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class OvertureBuildingImporter : EditorWindow
{
    CityDefinition city;
    string status = "Select a city, download its GeoJSON, then import it here.";

    [MenuItem("Tools/RealBus/OSM/Import Overture Buildings")]
    static void Open() => GetWindow<OvertureBuildingImporter>("Overture Buildings");

    void OnEnable() { city = Selection.activeObject as CityDefinition; }

    void OnGUI()
    {
        city = (CityDefinition)EditorGUILayout.ObjectField("City", city, typeof(CityDefinition), false);
        EditorGUILayout.HelpBox("Optional alternate building source. OSM files are retained. Import a complete city-area export; Overture replaces the OSM building layer for this city, not roads or POIs.", MessageType.Info);
        using (new EditorGUI.DisabledScope(city == null || EditorApplication.isPlaying))
        {
            if (GUILayout.Button("Copy city download command"))
            {
                GUIUtility.systemCopyBuffer = string.Format(CultureInfo.InvariantCulture,
                    "overturemaps download --bbox={0},{1},{2},{3} -f geojson --type=building -o overture-buildings.geojson",
                    city.minLon, city.minLat, city.maxLon, city.maxLat);
                status = "Command copied. Requires the official overturemaps CLI. No download has been started.";
            }
            if (GUILayout.Button("Import downloaded GeoJSON…")) Import();
        }
        EditorGUILayout.HelpBox(status, MessageType.None);
        if (GUILayout.Button("Download instructions / attribution"))
            Application.OpenURL("https://docs.overturemaps.org/getting-data/overturemaps-py/");
    }

    void Import()
    {
        string source = EditorUtility.OpenFilePanel("Overture building GeoJSON", "", "geojson,json");
        if (string.IsNullOrEmpty(source)) return;
        try
        {
            if (!Regex.IsMatch(city.cityCode ?? "", @"^[A-Za-z0-9_-]+$"))
                throw new InvalidOperationException("City code must contain only letters, numbers, _ or -.");
            if (new FileInfo(source).Length > 128L * 1024 * 1024)
                throw new InvalidOperationException("Export exceeds 128 MB. Download a smaller city area.");
            string converted = OvertureBuildingConverter.ConvertGeoJson(File.ReadAllText(source), city, out int imported, out int skipped);
            string directory = Path.Combine(Application.streamingAssetsPath, "Cities", city.cityCode);
            string target = Path.Combine(directory, "overture-buildings.json");
            if (File.Exists(target) && !EditorUtility.DisplayDialog("Replace Overture pack?", "The previous Overture pack will be kept as a .bak file. OSM files are unchanged.", "Replace", "Cancel")) return;
            Directory.CreateDirectory(directory);
            string temporary = target + ".import";
            File.WriteAllText(temporary, converted);
            if (File.Exists(target)) File.Replace(temporary, target, target + ".bak");
            else File.Move(temporary, target);
            AssetDatabase.Refresh();
            status = $"Imported {imported} polygons; skipped {skipped} invalid, holed, duplicate or out-of-bounds polygons. Restart Play Mode to load.";
            if (RuntimeCityContentStore.IsDownloaded(city, "overture-buildings.json"))
                status += " A player-downloaded Overture pack overrides this bundled pack; remove that city's downloaded copy to use this import.";
            Debug.Log(status);
        }
        catch (Exception ex) { status = "Import failed: " + ex.Message; Debug.LogWarning(status); }
    }
}
