using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;

public class OSMLoader : MonoBehaviour
{
    public static OSMLoader Instance { get; private set; }

    [Header("State")]
    public bool dataLoaded = false;
    public OSMData osmData;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(LoadFromStreamingAssets());
    }

    IEnumerator LoadFromStreamingAssets()
    {
        CityDefinition city = CityManager.Instance != null ? CityManager.Instance.activeCity : null;
        string jsonPath = RuntimeCityContentStore.ResolveReadPath(city, "roads.json");
        string xmlPath = RuntimeCityContentStore.ResolveReadPath(city, "roads.xml");
        string preferredPath = SelectPreferredRoadsPath(xmlPath, jsonPath);
        string alternatePath = preferredPath == jsonPath ? xmlPath : jsonPath;

        foreach (string path in new[] { preferredPath, alternatePath })
        {
            Debug.Log($"OSM: Loading roads from {path}");
            string raw = null;
            yield return RuntimeCityContentStore.ReadText(path, text => raw = text);

            if (string.IsNullOrWhiteSpace(raw))
            {
                Debug.LogWarning($"OSM: No data loaded from {path}");
                continue;
            }

            Debug.Log($"OSM: Loaded {raw.Length} bytes from {path}");

            OSMData parsed = null;
            yield return OSMParser.ParseInBackground(raw, path.EndsWith(".json"), result => parsed = result);
            if (parsed != null && parsed.ways != null && parsed.ways.Count > 0)
            {
                osmData = parsed;
                dataLoaded = true;
                Debug.Log($"OSM: Data ready — {osmData.ways.Count} ways from {path}!");
                yield break;
            }

            Debug.LogWarning($"OSM: Parsed data had no ways from {path}, trying fallback...");
        }

        osmData = new OSMData();
        Debug.LogError($"OSM: Failed to load road data for {city?.cityName ?? "active city"}.");
    }

    public static string SelectPreferredRoadsPath(string xmlPath, string jsonPath)
    {
        bool hasJson = File.Exists(jsonPath) && new FileInfo(jsonPath).Length > 32;
        bool hasUsefulXml = File.Exists(xmlPath) && new FileInfo(xmlPath).Length > 64;

        if (hasJson)
            return jsonPath;
        if (hasUsefulXml)
            return xmlPath;

        // Inside an Android APK, File.Exists cannot inspect StreamingAssets.
        // Bundled roads are shipped as roads.json; try JSON first and let the loader fall back to XML.
        return jsonPath;
    }
}
