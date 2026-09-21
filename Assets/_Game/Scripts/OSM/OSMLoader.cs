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
        string preferredPath = File.Exists(jsonPath) ? jsonPath : xmlPath;
        Debug.Log($"OSM: Loading from {preferredPath}");

        string raw = null;

#if UNITY_ANDROID && !UNITY_EDITOR
        string url = preferredPath;
        using (var request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"OSM load failed: {request.error}");
                Debug.LogError($"Path tried: {preferredPath}");
                yield break;
            }

            raw = request.downloadHandler.text;
        }
#else
        if (!File.Exists(preferredPath))
        {
            Debug.LogError($"OSM load failed: File not found");
            Debug.LogError($"Path tried: {preferredPath}");
            yield break;
        }

        raw = File.ReadAllText(preferredPath);
#endif

        Debug.Log($"OSM: Loaded {raw.Length} bytes from disk");

        osmData = preferredPath.EndsWith(".json")
            ? OSMParser.ParseOverpassJson(raw)
            : OSMParser.Parse(raw);
        dataLoaded = osmData != null && osmData.ways != null && osmData.ways.Count > 0;

        if (!dataLoaded)
        {
            Debug.LogError($"OSM: Parsed data was empty from {preferredPath}");
            yield break;
        }

        Debug.Log("OSM: Data ready!");
    }
}
