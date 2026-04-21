#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class BuildingsAndPoiDownloader : EditorWindow
{
    [MenuItem("Tools/RealBus/OSM/Download Buildings + POIs For Active City")]
    public static void Open()
    {
        GetWindow<BuildingsAndPoiDownloader>("Buildings + POIs");
    }

    string status = "Idle";
    bool isDownloading;
    string[] cityCodes = System.Array.Empty<string>();
    int selectedCityIndex;
    CityDefinition[] cityDefinitions = System.Array.Empty<CityDefinition>();

    void OnGUI()
    {
        GUILayout.Label("Download Buildings + POIs", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "Downloads building footprints into buildings.json and places of interest into poi.json " +
            "under StreamingAssets/Cities/<CITY>/.",
            MessageType.Info);

        EnsureCityList();
        DrawCityPicker();

        using (new EditorGUI.DisabledScope(isDownloading))
        {
            if (GUILayout.Button("Download Buildings + POIs", GUILayout.Height(36)))
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
                EditorGUILayout.LabelField("No cities found in StreamingAssets/Cities");
            else
                selectedCityIndex = EditorGUILayout.Popup(Mathf.Clamp(selectedCityIndex, 0, cityCodes.Length - 1), cityCodes);
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

        string buildingQuery =
            $"[out:json][timeout:120];" +
            $"(" +
            $"way[\"building\"]({city.minLat},{city.minLon},{city.maxLat},{city.maxLon});" +
            $");" +
            $"out geom;";

        string poiQuery =
            $"[out:json][timeout:120];" +
            $"(" +
            $"node[\"amenity\"~\"school|college|university|hospital|clinic|doctors|pharmacy|fuel\"]({city.minLat},{city.minLon},{city.maxLat},{city.maxLon});" +
            $"way[\"amenity\"~\"school|college|university|hospital|clinic|doctors|pharmacy|fuel\"]({city.minLat},{city.minLon},{city.maxLat},{city.maxLon});" +
            $"relation[\"amenity\"~\"school|college|university|hospital|clinic|doctors|pharmacy|fuel\"]({city.minLat},{city.minLon},{city.maxLat},{city.maxLon});" +
            $"node[\"shop\"]({city.minLat},{city.minLon},{city.maxLat},{city.maxLon});" +
            $"way[\"shop\"]({city.minLat},{city.minLon},{city.maxLat},{city.maxLon});" +
            $"relation[\"shop\"]({city.minLat},{city.minLon},{city.maxLat},{city.maxLon});" +
            $"node[\"tourism\"]({city.minLat},{city.minLon},{city.maxLat},{city.maxLon});" +
            $"way[\"tourism\"]({city.minLat},{city.minLon},{city.maxLat},{city.maxLon});" +
            $"relation[\"tourism\"]({city.minLat},{city.minLon},{city.maxLat},{city.maxLon});" +
            $"node[\"historic\"]({city.minLat},{city.minLon},{city.maxLat},{city.maxLon});" +
            $"way[\"historic\"]({city.minLat},{city.minLon},{city.maxLat},{city.maxLon});" +
            $"relation[\"historic\"]({city.minLat},{city.minLon},{city.maxLat},{city.maxLon});" +
            $");" +
            $"out geom;";

        isDownloading = true;
        status = $"Downloading buildings and POIs for {city.cityName}...";
        Repaint();

        StartDownloads(city, buildingQuery, poiQuery);
    }

    async void StartDownloads(CityDefinition city, string buildingQuery, string poiQuery)
    {
        string buildingsJson = await PostOverpass(buildingQuery);
        if (string.IsNullOrEmpty(buildingsJson))
        {
            isDownloading = false;
            status = "Building download failed. See Console.";
            Repaint();
            return;
        }

        string poiJson = await PostOverpass(poiQuery);
        if (string.IsNullOrEmpty(poiJson))
        {
            isDownloading = false;
            status = "POI download failed. See Console.";
            Repaint();
            return;
        }

        string dir = Path.Combine(Application.streamingAssetsPath, "Cities", city.cityCode);
        Directory.CreateDirectory(dir);
        File.WriteAllText(Path.Combine(dir, "buildings.json"), buildingsJson);
        File.WriteAllText(Path.Combine(dir, "poi.json"), poiJson);
        AssetDatabase.Refresh();

        isDownloading = false;
        status = $"Saved buildings.json ({buildingsJson.Length} bytes) and poi.json ({poiJson.Length} bytes) for {city.cityCode}.";
        Repaint();
    }

    async System.Threading.Tasks.Task<string> PostOverpass(string query)
    {
        string[] endpoints =
        {
            "https://overpass-api.de/api/interpreter",
            "https://overpass.kumi.systems/api/interpreter",
            "https://lz4.overpass-api.de/api/interpreter"
        };

        foreach (string endpoint in endpoints)
        {
            using (var request = new UnityWebRequest(endpoint, "POST"))
            {
                string postData = "data=" + UnityWebRequest.EscapeURL(query);
                request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(postData));
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                request.timeout = 120;

                var op = request.SendWebRequest();
                while (!op.isDone)
                    await System.Threading.Tasks.Task.Yield();

                if (request.result == UnityWebRequest.Result.Success)
                    return request.downloadHandler.text;

                Debug.LogWarning($"BuildingsAndPoiDownloader: request failed via {endpoint}: {request.error}");
            }
        }

        return null;
    }

    void EnsureCityList()
    {
        if (cityCodes.Length > 0)
            return;

        string citiesRoot = Path.Combine(Application.streamingAssetsPath, "Cities");
        if (!Directory.Exists(citiesRoot))
        {
            cityCodes = System.Array.Empty<string>();
            return;
        }

        var dirs = Directory.GetDirectories(citiesRoot);
        var codes = new List<string>();
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
            if (idx >= 0)
                selectedCityIndex = idx;
        }
    }

    CityDefinition[] LoadCityDefinitions()
    {
        string[] guids = AssetDatabase.FindAssets("t:CityDefinition");
        var list = new List<CityDefinition>(guids.Length);
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
        if (string.IsNullOrWhiteSpace(cityCode))
            return null;

        if (cityDefinitions == null || cityDefinitions.Length == 0)
            cityDefinitions = LoadCityDefinitions();

        for (int i = 0; i < cityDefinitions.Length; i++)
        {
            var city = cityDefinitions[i];
            if (city != null && city.cityCode == cityCode)
                return city;
        }

        return null;
    }
}
#endif
