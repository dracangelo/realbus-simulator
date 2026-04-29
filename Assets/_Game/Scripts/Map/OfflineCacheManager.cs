using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Tile cache layer that sits between MapTileLoader and Mapbox.
///
/// Responsibilities:
///   • Detect network connectivity on startup (and re-check on each tile miss).
///   • Cache PNG tiles to persistent storage; serve from cache first.
///   • Enforce a configurable maximum cache size (LRU eviction via file timestamps).
///   • Report authorisation failures exactly once so the console isn't spammed.
///
/// NOTE: Mapbox caches tiles internally in their SDK.  This manager handles
///       the custom offline-first workflow and the pre-download UI flow only.
/// </summary>
public class OfflineCacheManager : MonoBehaviour
{
    public static OfflineCacheManager Instance { get; private set; }

    // ── Inspector ──────────────────────────────────────────────────────

    [Header("Cache Settings")]
    public string cacheFolderName  = "TileCache";
    [Tooltip("Maximum cache size in MB before LRU eviction runs.")]
    [Min(50f)] public float maxCacheMB = 500f;

    [Header("Network")]
    [Min(5)] public int httpTimeoutSeconds = 12;
    [Tooltip("Seconds between automatic connectivity re-checks.")]
    [Min(10f)] public float connectivityRecheckInterval = 30f;

    [Header("State (read-only)")]
    public bool isOnline;

    // ── Private ────────────────────────────────────────────────────────

    string _cacheRoot;
    bool   _loggedAuthFailure;
    float  _connectivityTimer;

    // ── Lifecycle ──────────────────────────────────────────────────────

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _cacheRoot = Path.Combine(Application.persistentDataPath, cacheFolderName);
    }

    void Start()
    {
        isOnline = Application.internetReachability != NetworkReachability.NotReachable;
        Debug.Log($"[OfflineCacheManager] Online={isOnline}  CacheRoot={_cacheRoot}");
    }

    void Update()
    {
        // Lightweight re-check (Unity's NetworkReachability is a flag, not a web request).
        _connectivityTimer += Time.unscaledDeltaTime;
        if (_connectivityTimer >= connectivityRecheckInterval)
        {
            _connectivityTimer = 0f;
            isOnline = Application.internetReachability != NetworkReachability.NotReachable;
        }
    }

    // ── Public API ─────────────────────────────────────────────────────

    /// <summary>
    /// Resolves a tile — from cache if available, otherwise downloaded and cached.
    /// Returns null texture on failure.
    /// </summary>
    public IEnumerator GetTileTexture(
        string mapboxToken,
        string style,
        int zoom, int x, int y,
        System.Action<Texture2D> onDone)
    {
        string cachePath = GetTileCachePath(style, zoom, x, y);

        // ── Cache hit ──────────────────────────────────────────────────
        if (File.Exists(cachePath))
        {
            File.SetLastWriteTimeUtc(cachePath, System.DateTime.UtcNow); // LRU touch
            onDone?.Invoke(LoadTextureFromDisk(cachePath));
            yield break;
        }

        // ── Offline and no cache ────────────────────────────────────────
        if (!isOnline)
        {
            Debug.LogWarning($"[OfflineCacheManager] Offline — no cache for {zoom}/{x}/{y}.");
            onDone?.Invoke(null);
            yield break;
        }

        if (string.IsNullOrWhiteSpace(mapboxToken))
        {
            Debug.LogWarning($"[OfflineCacheManager] No Mapbox token — skipping tile {zoom}/{x}/{y}.");
            onDone?.Invoke(null);
            yield break;
        }

        // ── Download ───────────────────────────────────────────────────
        string url = BuildTileUrl(mapboxToken, style, zoom, x, y);
        using (var req = UnityWebRequestTexture.GetTexture(url))
        {
            req.timeout = Mathf.Max(1, httpTimeoutSeconds);
            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
            {
                HandleDownloadFailure(req, zoom, x, y);
                onDone?.Invoke(null);
                yield break;
            }

            isOnline = true;
            var tex = DownloadHandlerTexture.GetContent(req);
            WriteToCache(cachePath, tex);
            EnforceCacheSizeAsync();
            onDone?.Invoke(tex);
        }
    }

    /// <summary>Absolute path where a tile PNG is (or would be) stored.</summary>
    public string GetTileCachePath(string style, int zoom, int x, int y)
    {
        string safeStyle = style.Replace('/', '_').Replace(':', '_');
        return Path.Combine(_cacheRoot, safeStyle, zoom.ToString(), x.ToString(), $"{y}.png");
    }

    /// <summary>True if the tile is already in local cache.</summary>
    public bool IsTileCached(string style, int zoom, int x, int y) =>
        File.Exists(GetTileCachePath(style, zoom, x, y));

    /// <summary>Delete all cached tiles for a given style + zoom level.</summary>
    public void ClearCacheForZoom(string style, int zoom)
    {
        string safeStyle = style.Replace('/', '_').Replace(':', '_');
        string dir = Path.Combine(_cacheRoot, safeStyle, zoom.ToString());
        if (Directory.Exists(dir))
        {
            Directory.Delete(dir, recursive: true);
            Debug.Log($"[OfflineCacheManager] Cleared cache: {dir}");
        }
    }

    /// <summary>Approximate total size of the cache folder in MB.</summary>
    public float GetCacheSizeMB()
    {
        if (!Directory.Exists(_cacheRoot)) return 0f;
        long bytes = 0;
        foreach (var f in Directory.GetFiles(_cacheRoot, "*", SearchOption.AllDirectories))
            bytes += new FileInfo(f).Length;
        return bytes / (1024f * 1024f);
    }

    // ── Private helpers ────────────────────────────────────────────────

    static string BuildTileUrl(string token, string style, int zoom, int x, int y) =>
        $"https://api.mapbox.com/styles/v1/{style}/tiles/512/{zoom}/{x}/{y}@2x?access_token={token}";

    static Texture2D LoadTextureFromDisk(string path)
    {
        byte[] bytes = File.ReadAllBytes(path);
        var tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        tex.LoadImage(bytes);          // resizes and fills
        tex.Apply(false, true);        // mark non-readable to save VRAM copies
        return tex;
    }

    void WriteToCache(string path, Texture2D tex)
    {
        try
        {
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            File.WriteAllBytes(path, tex.EncodeToPNG());
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning($"[OfflineCacheManager] Cache write failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Fire-and-forget: if cache is over budget, delete oldest files until under.
    /// Runs as a coroutine to avoid a main-thread stall.
    /// </summary>
    void EnforceCacheSizeAsync() => StartCoroutine(EvictOldestTiles());

    IEnumerator EvictOldestTiles()
    {
        yield return null; // defer one frame so tile delivery isn't blocked

        if (!Directory.Exists(_cacheRoot)) yield break;

        float sizeMB = GetCacheSizeMB();
        if (sizeMB <= maxCacheMB) yield break;

        var files = new List<FileInfo>();
        foreach (var f in Directory.GetFiles(_cacheRoot, "*.png", SearchOption.AllDirectories))
            files.Add(new FileInfo(f));

        // Oldest last-write-time first (LRU).
        files.Sort((a, b) => a.LastWriteTimeUtc.CompareTo(b.LastWriteTimeUtc));

        int deleted = 0;
        foreach (var fi in files)
        {
            if (sizeMB <= maxCacheMB * 0.85f) break; // free to 85 % to reduce churn
            try
            {
                sizeMB -= fi.Length / (1024f * 1024f);
                fi.Delete();
                deleted++;
            }
            catch { /* ignore – file may be in use */ }

            if (deleted % 50 == 0) yield return null; // breathe every 50 deletions
        }

        if (deleted > 0)
            Debug.Log($"[OfflineCacheManager] LRU eviction: removed {deleted} tiles. Cache ≈ {sizeMB:F1} MB.");
    }

    void HandleDownloadFailure(UnityWebRequest req, int zoom, int x, int y)
    {
        isOnline = false;
        long status   = req.responseCode;
        string body   = req.downloadHandler?.text ?? "";
        if (body.Length > 200) body = body.Substring(0, 200);

        bool authFail = status == 401 || status == 403
            || body.IndexOf("access denied",  System.StringComparison.OrdinalIgnoreCase) >= 0
            || body.IndexOf("Unauthorized",   System.StringComparison.OrdinalIgnoreCase) >= 0;

        if (authFail && !_loggedAuthFailure)
        {
            _loggedAuthFailure = true;
            Debug.LogError(
                $"[OfflineCacheManager] Mapbox authorisation failed (HTTP {status}). " +
                "Verify your token has 'styles:tiles' scope and the style is public or accessible.");
        }
        else if (!authFail)
        {
            string detail = string.IsNullOrWhiteSpace(body) ? req.error : $"{req.error} | {body}";
            Debug.LogWarning($"[OfflineCacheManager] Tile download failed {zoom}/{x}/{y} (HTTP {status}): {detail}");
        }
    }
}
