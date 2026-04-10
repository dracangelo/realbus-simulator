using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

public class OfflineCacheManager : MonoBehaviour
{
    public static OfflineCacheManager Instance { get; private set; }

    [Header("Cache")]
    public string cacheFolderName = "TileCache";

    [Header("Connectivity (read-only)")]
    public bool isOnline;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        isOnline = Application.internetReachability != NetworkReachability.NotReachable;
        Debug.Log($"OfflineCacheManager: Online={isOnline}");
    }

    public string GetTileCachePath(string style, int zoom, int x, int y)
    {
        string safeStyle = style.Replace("/", "_");
        return Path.Combine(Application.persistentDataPath, cacheFolderName, safeStyle, zoom.ToString(), x.ToString(), $"{y}.png");
    }

    public IEnumerator GetTileTexture(string mapboxToken, string style, int zoom, int x, int y, System.Action<Texture2D> onDone)
    {
        string cachePath = GetTileCachePath(style, zoom, x, y);

        // Offline: only cache.
        if (!isOnline)
        {
            if (File.Exists(cachePath))
            {
                byte[] bytes = File.ReadAllBytes(cachePath);
                var tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                tex.LoadImage(bytes);
                onDone?.Invoke(tex);
            }
            else
            {
                onDone?.Invoke(null);
            }
            yield break;
        }

        // Online: try cache first, then download and write-through.
        if (File.Exists(cachePath))
        {
            byte[] bytes = File.ReadAllBytes(cachePath);
            var tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            tex.LoadImage(bytes);
            onDone?.Invoke(tex);
            yield break;
        }

        string url = $"https://api.mapbox.com/styles/v1/{style}/tiles/512/{zoom}/{x}/{y}@2x?access_token={mapboxToken}";
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning($"OfflineCacheManager: Tile download failed {zoom}/{x}/{y} — {request.error}");
                onDone?.Invoke(null);
                yield break;
            }

            var tex = DownloadHandlerTexture.GetContent(request);
            TryWriteToCache(cachePath, tex);
            onDone?.Invoke(tex);
        }
    }

    void TryWriteToCache(string path, Texture2D tex)
    {
        try
        {
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            byte[] bytes = tex.EncodeToPNG();
            File.WriteAllBytes(path, bytes);
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning($"OfflineCacheManager: Cache write failed: {ex.Message}");
        }
    }
}

