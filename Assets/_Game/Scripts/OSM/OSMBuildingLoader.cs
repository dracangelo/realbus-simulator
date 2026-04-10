using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

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
        string path = CityManager.Instance != null
            ? CityManager.Instance.activeCity.GetBuildingsPath()
            : System.IO.Path.Combine(Application.streamingAssetsPath, "Cities/NBO/buildings.xml");

        string url = "file://" + path;

        Debug.Log($"Buildings: Loading from {path}");

        using (var request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Buildings load failed: {request.error}");
                yield break;
            }

            string xml = request.downloadHandler.text;
            Debug.Log($"Buildings: Loaded {xml.Length} bytes from disk");

            osmData = OSMParser.Parse(xml);
            dataLoaded = true;

            Debug.Log($"Buildings: Data ready — {osmData.ways.Count} ways");
        }
    }
}