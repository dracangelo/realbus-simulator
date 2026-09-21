using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;

public class OSMBuildingLoader : MonoBehaviour
{
    public static OSMBuildingLoader Instance { get; private set; }

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
        string xmlPath = RuntimeCityContentStore.ResolveReadPath(city, "buildings.xml");
        string jsonPath = RuntimeCityContentStore.ResolveReadPath(city, "buildings.json");
        string preferredPath = SelectPreferredBuildingsPath(xmlPath, jsonPath);

        string url = "file://" + preferredPath;

        Debug.Log($"Buildings: Loading from {preferredPath}");

        using (var request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Buildings load failed: {request.error}");
                Debug.LogError($"Path tried: {preferredPath}");
                yield break;
            }

            string raw = request.downloadHandler.text;
            Debug.Log($"Buildings: Loaded {raw.Length} bytes from disk");

            osmData = preferredPath.EndsWith(".json")
                ? OSMParser.ParseOverpassJson(raw)
                : OSMParser.Parse(raw);
            dataLoaded = osmData != null && osmData.ways != null && osmData.ways.Count > 0;

            if (!dataLoaded)
            {
                Debug.LogWarning($"Buildings: No building ways were found in {preferredPath}. " +
                    "Use Tools > RealBus > OSM > Download Buildings + POIs For Active City; most non-Nairobi city folders currently contain placeholder building files.");
                yield break;
            }

            Debug.Log($"Buildings: Data ready — {osmData.ways.Count} ways");
        }
    }

    string SelectPreferredBuildingsPath(string xmlPath, string jsonPath)
    {
        bool hasJson = File.Exists(jsonPath) && new FileInfo(jsonPath).Length > 32;
        bool hasUsefulXml = File.Exists(xmlPath) && new FileInfo(xmlPath).Length > 64;

        if (hasJson)
            return jsonPath;
        if (hasUsefulXml)
            return xmlPath;

        return File.Exists(jsonPath) ? jsonPath : xmlPath;
    }
}
