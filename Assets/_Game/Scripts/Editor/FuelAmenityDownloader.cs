#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class FuelAmenityDownloader : EditorWindow
{
    [MenuItem("Tools/RealBus/OSM/Download Fuel Amenities For Active City")]
    public static void Open()
    {
        GetWindow<FuelAmenityDownloader>("Fuel Amenities");
    }

    string status = "Idle";
    bool isDownloading;
    string[] cityCodes = System.Array.Empty<string>();
    int selectedCityIndex;
    CityDefinition[] cityDefinitions = System.Array.Empty<CityDefinition>();

    void OnGUI()
    {
        GUILayout.Label("Download Fuel Amenities", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "Downloads OSM amenities tagged as fuel for the active city bounds and saves them to " +
            "StreamingAssets/Cities/<CITY>/fuel.json.",
            MessageType.Info);

        EnsureCityList();
        DrawCityPicker();

        using (new EditorGUI.DisabledScope(isDownloading))
        {
            if (GUILayout.Button("Download Fuel Amenities", GUILayout.Height(36)))
                DownloadForSelectedCity();
        }

        GUILayout.Space(8f);
        EditorGUILayout.LabelField("Status", status);
    }

    void DrawCityPicker()
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField("City", GUILayout.Width(60f));
            if (cityCodes.Length == 0)
            {
                EditorGUILayout.LabelField("No cities found in StreamingAssets/Cities");
            }
            else
            {
                selectedCityIndex = Mathf.Clamp(selectedCityIndex, 0, cityCodes.Length - 1);
                selectedCityIndex = EditorGUILayout.Popup(selectedCityIndex, cityCodes);
            }
        }
    }

    void DownloadForSelectedCity()
    {
        if (cityCodes.Length == 0)
        {
            status = "No city folders found in StreamingAssets/Cities.";
            Repaint();
            return;
        }

        string cityCode = cityCodes[Mathf.Clamp(selectedCityIndex, 0, cityCodes.Length - 1)];
        var city = FindCityDefinitionByCode(cityCode);
        if (city == null)
        {
            status = $"No CityDefinition asset found for city code '{cityCode}'.";
            Repaint();
            return;
        }

        string query =
            $"[out:json][timeout:60];" +
            $"(" +
            $"node[\"amenity\"=\"fuel\"]({city.minLat},{city.minLon},{city.maxLat},{city.maxLon});" +
            $"way[\"amenity\"=\"fuel\"]({city.minLat},{city.minLon},{city.maxLat},{city.maxLon});" +
            $"relation[\"amenity\"=\"fuel\"]({city.minLat},{city.minLon},{city.maxLat},{city.maxLon});" +
            $");" +
            $"out body;>;out skel qt;";

        string url = "https://overpass-api.de/api/interpreter";
        string postData = "data=" + UnityWebRequest.EscapeURL(query);

        var request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(postData));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        request.timeout = 60;

        isDownloading = true;
        status = $"Downloading fuel amenities for {city.cityName}...";
        Repaint();

        var op = request.SendWebRequest();
        op.completed += _ =>
        {
            isDownloading = false;
            if (request.result != UnityWebRequest.Result.Success)
            {
                status = $"Download failed: {request.error}";
                Repaint();
                request.Dispose();
                return;
            }

            string json = request.downloadHandler.text;
            string dir = Path.Combine(Application.streamingAssetsPath, "Cities", city.cityCode);
            Directory.CreateDirectory(dir);
            string path = Path.Combine(dir, "fuel.json");
            File.WriteAllText(path, json);

            status = $"Saved {json.Length} bytes to {path}";
            Repaint();
            request.Dispose();
        };
    }

    void EnsureCityList()
    {
        if (cityCodes.Length > 0) return;

        string citiesRoot = Path.Combine(Application.streamingAssetsPath, "Cities");
        if (!Directory.Exists(citiesRoot))
        {
            cityCodes = System.Array.Empty<string>();
            return;
        }

        var dirs = Directory.GetDirectories(citiesRoot);
        var codes = new System.Collections.Generic.List<string>();
        foreach (var dir in dirs)
        {
            var code = Path.GetFileName(dir);
            if (!string.IsNullOrWhiteSpace(code))
                codes.Add(code);
        }
        codes.Sort();
        cityCodes = codes.ToArray();

        cityDefinitions = LoadCityDefinitions();

        var active = CityManager.Instance != null ? CityManager.Instance.activeCity : null;
        if (active != null)
        {
            int idx = System.Array.IndexOf(cityCodes, active.cityCode);
            if (idx >= 0) selectedCityIndex = idx;
        }
    }

    CityDefinition[] LoadCityDefinitions()
    {
        string[] guids = AssetDatabase.FindAssets("t:CityDefinition");
        var list = new System.Collections.Generic.List<CityDefinition>(guids.Length);
        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<CityDefinition>(path);
            if (asset != null)
                list.Add(asset);
        }
        return list.ToArray();
    }

    CityDefinition FindCityDefinitionByCode(string cityCode)
    {
        if (string.IsNullOrWhiteSpace(cityCode)) return null;
        if (cityDefinitions == null || cityDefinitions.Length == 0)
            cityDefinitions = LoadCityDefinitions();

        for (int i = 0; i < cityDefinitions.Length; i++)
        {
            var c = cityDefinitions[i];
            if (c != null && c.cityCode == cityCode)
                return c;
        }
        return null;
    }
}
#endif
