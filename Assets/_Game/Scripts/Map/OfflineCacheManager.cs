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
    public int httpTimeoutSeconds = 12;

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

    bool loggedAuthFailureHint;

    public string GetTileCachePath(string style, int zoom, int x, int y)
    {
        string safeStyle = style.Replace("/", "_");
        return Path.Combine(Application.persistentDataPath, cacheFolderName, safeStyle, zoom.ToString(), x.ToString(), $"{y}.png");
    }

    public IEnumerator GetTileTexture(string mapboxToken, string style, int zoom, int x, int y, System.Action<Texture2D> onDone)
    {
        string cachePath = GetTileCachePath(style, zoom, x, y);

        // Always prefer cache first.
        if (File.Exists(cachePath))
        {
            var tex = LoadCachedTexture(cachePath);
            onDone?.Invoke(tex);
            yield break;
        }

        if (string.IsNullOrWhiteSpace(mapboxToken))
        {
            Debug.LogWarning($"OfflineCacheManager: No Mapbox token available for tile {zoom}/{x}/{y}.");
            onDone?.Invoke(null);
            yield break;
        }

        string url = $"https://api.mapbox.com/styles/v1/{style}/tiles/512/{zoom}/{x}/{y}@2x?access_token={mapboxToken}";
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            request.timeout = Mathf.Max(1, httpTimeoutSeconds);
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                isOnline = false;
                long status = request.responseCode;
                string body = request.downloadHandler != null ? request.downloadHandler.text : "";
                if (body != null && body.Length > 160)
                    body = body.Substring(0, 160);

                bool accessDenied = status == 401 || status == 403
                    || (!string.IsNullOrWhiteSpace(request.error) && request.error.IndexOf("access denied", System.StringComparison.OrdinalIgnoreCase) >= 0)
                    || (!string.IsNullOrWhiteSpace(body) && body.IndexOf("access denied", System.StringComparison.OrdinalIgnoreCase) >= 0);

                if (accessDenied && !loggedAuthFailureHint)
                {
                    loggedAuthFailureHint = true;
                    Debug.LogError(
                        $"OfflineCacheManager: Mapbox authorization failed (HTTP {status}). " +
                        "Check token permissions and style access.");
                }

                string detail = string.IsNullOrWhiteSpace(body) ? request.error : $"{request.error} | {body}";
                Debug.LogWarning($"OfflineCacheManager: Tile download failed {zoom}/{x}/{y} (HTTP {status}) — {detail}");
                onDone?.Invoke(null);
                yield break;
            }

            isOnline = true;
            var tex = DownloadHandlerTexture.GetContent(request);
            TryWriteToCache(cachePath, tex);
            onDone?.Invoke(tex);
        }
    }

    Texture2D LoadCachedTexture(string cachePath)
    {
        byte[] bytes = File.ReadAllBytes(cachePath);
        var tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        tex.LoadImage(bytes);
        return tex;
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
