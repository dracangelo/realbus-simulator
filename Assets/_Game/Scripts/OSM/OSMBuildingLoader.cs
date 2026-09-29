using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;

public class OSMBuildingLoader : MonoBehaviour
{
    public static OSMBuildingLoader Instance { get; private set; }

    [Header("State")]
    public bool dataLoaded = false;
    public bool loadCompleted { get; private set; }
    public OSMData osmData;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    IEnumerator Start()
    {
        yield return LoadFromStreamingAssets();
        if (osmData == null) osmData = new OSMData();
        loadCompleted = true;
    }

    IEnumerator LoadFromStreamingAssets()
    {
        CityDefinition city = CityManager.Instance != null ? CityManager.Instance.activeCity : null;
        string xmlPath = RuntimeCityContentStore.ResolveReadPath(city, "buildings.xml");
        string jsonPath = RuntimeCityContentStore.ResolveReadPath(city, "buildings.json");
        string preferredPath = SelectPreferredBuildingsPath(xmlPath, jsonPath);

        // An optional, converted Overture pack replaces OSM (Overture already
        // contains OSM footprints). Never render both sources on top of each other.
        string overturePath = RuntimeCityContentStore.ResolveReadPath(city, "overture-buildings.json");
        string overtureRaw = null;
        yield return ReadText(overturePath, text => overtureRaw = text);
        if (!string.IsNullOrWhiteSpace(overtureRaw))
        {
            osmData = OSMParser.ParseOverpassJson(overtureRaw);
            if (HasBuildings(osmData))
            {
                dataLoaded = true;
                Debug.Log($"Buildings: Using Overture pack with {osmData.ways.Count} footprints.");
                yield break;
            }
            Debug.LogWarning("Buildings: Optional Overture pack is empty; falling back to OSM.");
        }

        // Android packaged assets use jar:file URLs, whereas downloaded city
        // files are ordinary disk paths. Do not send disk paths to a web request.
        string alternatePath = preferredPath == jsonPath ? xmlPath : jsonPath;
        foreach (string path in new[] { preferredPath, alternatePath })
        {
            string raw = null;
            yield return ReadText(path, text => raw = text);
            if (string.IsNullOrWhiteSpace(raw)) continue;
            Debug.Log($"Buildings: Loaded {raw.Length} bytes from {path}");
            yield return null;
            osmData = path.EndsWith(".json")
                ? OSMParser.ParseOverpassJson(raw)
                : OSMParser.Parse(raw);
            dataLoaded = HasBuildings(osmData);
            if (!dataLoaded) continue;
            Debug.Log($"Buildings: Data ready — {osmData.ways.Count} ways");
            yield break;
        }
        osmData = new OSMData();
        Debug.LogWarning("Buildings: No usable footprint pack found; procedural fill can use the road network. Download Buildings + POIs or import Overture for mapped buildings.");
    }

    public static string SelectPreferredBuildingsPath(string xmlPath, string jsonPath)
    {
        bool hasJson = File.Exists(jsonPath) && new FileInfo(jsonPath).Length > 32;
        bool hasUsefulXml = File.Exists(xmlPath) && new FileInfo(xmlPath).Length > 64;

        if (hasJson)
            return jsonPath;
        if (hasUsefulXml)
            return xmlPath;

        // File.Exists cannot inspect assets inside an Android APK. Try JSON
        // first and let the loader fall back to XML if absent or empty.
        return jsonPath;
    }

    static bool HasBuildings(OSMData data) => data?.ways != null && data.ways.Exists(way => way.IsBuilding());

    static IEnumerator ReadText(string path, System.Action<string> completed)
    {
        if (!path.Contains("://"))
        {
            completed(File.Exists(path) ? ReadLocalFile(path) : null);
            yield break;
        }
        using (var request = UnityWebRequest.Get(path))
        {
            request.timeout = 30;
            yield return request.SendWebRequest();
            completed(request.result == UnityWebRequest.Result.Success ? request.downloadHandler.text : null);
        }
    }

    static string ReadLocalFile(string path)
    {
        try { return File.ReadAllText(path); }
        catch (System.Exception error) when (error is IOException || error is System.UnauthorizedAccessException)
        {
            Debug.LogWarning($"Buildings: Cannot read {path}: {error.Message}. Continuing with available fallback data.");
            return null;
        }
    }
}
