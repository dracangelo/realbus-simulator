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
    bool _evicting;
    public bool forceOffline;

    /// <summary>Where cached tiles are written, for diagnostics and tests.</summary>
    public string CacheRoot => _cacheRoot;

    /// <summary>
    /// The one tile cache, created on demand.
    ///
    /// Nothing in the shipped game used to create a cache: no scene contained one and no code
    /// added one, so every consumer's reference was null. The offline-maps download therefore
    /// wrote nothing (every tile "failed") while the menu still called the result "Offline map
    /// ready.", and the loader's own cache branch could never run, so a fully downloaded city
    /// was still fetched from Mapbox tile by tile. The loader and the predownloader now both
    /// ask for the cache here, which is what makes it exist wherever the map runs.
    /// </summary>
    public static OfflineCacheManager EnsureInstance()
    {
        if (Instance != null) return Instance;

        Instance = FindAnyObjectByType<OfflineCacheManager>();
        if (Instance != null) return Instance;

        var host = new GameObject("OfflineCacheManager");
        Instance = host.AddComponent<OfflineCacheManager>();
        // Awake does not run for a component added by an edit-mode caller or a test, so the
        // instance publishes itself here rather than waiting to be found later.
        Instance.Publish();
        return Instance;
    }

    // ── Lifecycle ──────────────────────────────────────────────────────

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) { DestroyImmediate(gameObject); return; }
#endif
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Publish();
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    /// <summary>Publishes this manager and gives it a cache root. Idempotent.</summary>
    void Publish()
    {
        if (string.IsNullOrEmpty(_cacheRoot))
            _cacheRoot = Path.Combine(Application.persistentDataPath, cacheFolderName);
        // Outlives the scene the map is loaded from, so a tile cached while driving is still
        // there after a return to the menu.
        if (Application.isPlaying) DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        isOnline = !forceOffline && Application.internetReachability != NetworkReachability.NotReachable;
        EnforceCacheSizeAsync();
        Debug.Log($"[OfflineCacheManager] Online={isOnline}  CacheRoot={_cacheRoot}");
    }

    void Update()
    {
        // Lightweight re-check (Unity's NetworkReachability is a flag, not a web request).
        _connectivityTimer += Time.unscaledDeltaTime;
        if (_connectivityTimer >= connectivityRecheckInterval)
        {
            _connectivityTimer = 0f;
            isOnline = !forceOffline && Application.internetReachability != NetworkReachability.NotReachable;
        }
    }

    // ── Public API ─────────────────────────────────────────────────────

    /// <summary>
    /// Resolves a tile — from cache if available, otherwise downloaded and cached.
    ///
    /// The credential and the style are not parameters: they are read from the one resolved
    /// configuration (<see cref="MapboxMapSettings"/>), so no caller can ask for a different
    /// style, or send a different token, than the rest of the map stack is using. Returns a
    /// null texture on failure.
    /// </summary>
    public IEnumerator GetTileTexture(int zoom, int x, int y, System.Action<Texture2D> onDone)
    {
        MapboxMapSettings settings = MapboxMapSettings.Current;
        string cachePath = GetTileCachePath(settings.Style, zoom, x, y);

        // ── Cache hit ──────────────────────────────────────────────────
        if (File.Exists(cachePath))
        {
            Texture2D cached = LoadTextureFromDisk(cachePath);
            if (cached != null)
            {
                try { File.SetLastWriteTimeUtc(cachePath, System.DateTime.UtcNow); } catch (IOException) { }
                onDone?.Invoke(cached);
                yield break;
            }
            try { File.Delete(cachePath); } catch (IOException) { }
        }

        isOnline = !forceOffline && Application.internetReachability != NetworkReachability.NotReachable;

        // ── Offline and no cache ────────────────────────────────────────
        if (!isOnline)
        {
            Debug.LogWarning($"[OfflineCacheManager] Offline — no cache for {zoom}/{x}/{y}.");
            onDone?.Invoke(null);
            yield break;
        }

        if (!settings.HasToken)
        {
            Debug.LogWarning($"[OfflineCacheManager] No Mapbox token — skipping tile {zoom}/{x}/{y} " +
                             $"({settings.TokenSource}).");
            onDone?.Invoke(null);
            yield break;
        }

        // ── Download ───────────────────────────────────────────────────
        string url = MapboxTileRequest.BuildTileUrl(settings.Style, zoom, x, y, settings.Token);
        using (var req = UnityWebRequest.Get(url))
        {
            req.timeout = Mathf.Max(1, httpTimeoutSeconds);
            req.SetRequestHeader("Accept", "image/png,image/jpeg;q=0.9,*/*;q=0.1");
            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
            {
                HandleDownloadFailure(req, zoom, x, y);
                onDone?.Invoke(null);
                yield break;
            }

            isOnline = true;
            byte[] bytes = req.downloadHandler != null ? req.downloadHandler.data : null;
            var tex = new Texture2D(2, 2, TextureFormat.RGBA32, true);
            if (bytes == null || bytes.Length < 16 || !tex.LoadImage(bytes, false))
            {
                Destroy(tex);
                Debug.LogWarning($"[OfflineCacheManager] Mapbox returned HTTP {req.responseCode}, but tile {zoom}/{x}/{y} was not a supported PNG/JPEG image (Content-Type: {req.GetResponseHeader("Content-Type") ?? "unknown"}).");
                onDone?.Invoke(null);
                yield break;
            }
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

    static Texture2D LoadTextureFromDisk(string path)
    {
        Texture2D tex = null;
        try
        {
            byte[] bytes = File.ReadAllBytes(path);
            tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            if (tex.LoadImage(bytes, false)) return tex; // Streaming LOD builder needs readable pixels.
        }
        catch (IOException) { }
        catch (System.UnauthorizedAccessException) { }
        if (tex != null) Destroy(tex);
        return null;
    }

    void WriteToCache(string path, Texture2D tex)
    {
        try
        {
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            File.WriteAllBytes(path + ".tmp", tex.EncodeToPNG());
            if (File.Exists(path)) File.Delete(path);
            File.Move(path + ".tmp", path);
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
    void EnforceCacheSizeAsync()
    {
        if (!_evicting) StartCoroutine(EvictOnce());
    }
    IEnumerator EvictOnce()
    {
        _evicting = true;
        try { yield return EvictOldestTiles(); }
        finally { _evicting = false; }
    }

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
        isOnline = !forceOffline && Application.internetReachability != NetworkReachability.NotReachable;
        long status   = req.responseCode;
        string body   = req.downloadHandler?.text ?? "";
        if (body.Length > 200) body = body.Substring(0, 200);

        bool authFail = status == 401 || status == 403;

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
