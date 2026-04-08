using UnityEngine;
using System.Collections;
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
        // Only run in GameScene
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "GameScene")
            return;
        StartCoroutine(LoadFromStreamingAssets());
    }

    IEnumerator LoadFromStreamingAssets()
    {
        string path = CityManager.Instance != null
            ? CityManager.Instance.activeCity.GetRoadsPath()
            : System.IO.Path.Combine(Application.streamingAssetsPath, "Cities/NBO/roads.xml");

        string url = "file://" + path;

        // On Android use UnityWebRequest, on desktop file:// works
        #if UNITY_ANDROID
        url = path; // Android uses direct path
        #endif

        Debug.Log($"OSM: Loading from {path}");

        using (var request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"OSM load failed: {request.error}");
                Debug.LogError($"Path tried: {path}");
                yield break;
            }

            string xml = request.downloadHandler.text;
            Debug.Log($"OSM: Loaded {xml.Length} bytes from disk");

            osmData = OSMParser.Parse(xml);
            dataLoaded = true;

            Debug.Log("OSM: Data ready!");
        }
    }
}