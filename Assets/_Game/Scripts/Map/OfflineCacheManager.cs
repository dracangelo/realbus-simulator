using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Tile cache layer that sits between MapTileLoader and Mapbox.
///
/// Responsibilities:
///   • Detect network connectivity on startup (and re-check on each tile miss).
///   • Cache the raw PNG/JPEG response to persistent storage; serve from cache first.
///   • Coalesce duplicate requests for the same tile into one download.
///   • Retry transient failures with a bounded exponential backoff.
///   • Enforce a configurable maximum cache size (LRU eviction via file timestamps).
///   • Report authorisation failures exactly once so the console isn't spammed.
///
/// The cache stores the response bytes exactly as Mapbox sent them. It used to
/// decode every tile into a Texture2D and re-encode it with EncodeToPNG, which
/// allocated a readable 4 MB texture and ran a PNG encoder on the main thread for
/// every tile. Writing the bytes is both cheaper and lossless.
///
/// NOTE: Mapbox caches tiles internally in their SDK. This manager handles the
///       custom offline-first workflow and the pre-download UI flow only.
/// </summary>
public class OfflineCacheManager : MonoBehaviour
{
    static OfflineCacheManager _instance;

    /// <summary>
    /// The live cache, or null.
    ///
    /// A destroyed component keeps its managed wrapper alive for as long as something
    /// references it, and only Unity's overloaded <c>==</c> knows that wrapper is gone. A
    /// plain reference comparison - the download UI, a non-MonoBehaviour helper, a test -
    /// would otherwise be handed a cache whose native object no longer exists. The getter
    /// drops that wrapper the first time it is seen, so "destroyed" reads as null to every
    /// caller rather than only to Unity's operator.
    /// </summary>
    public static OfflineCacheManager Instance
    {
        get
        {
            if (!ReferenceEquals(_instance, null) && _instance == null)
                _instance = null;
            return _instance;
        }
        private set => _instance = value;
    }

    // ── Inspector ──────────────────────────────────────────────────────

    [Header("Cache Settings")]
    public string cacheFolderName = "TileCache";
    [Tooltip("Maximum cache size in MB before LRU eviction runs.")]
    [Min(50f)] public float maxCacheMB = 500f;
    [Tooltip("Tiles older than this are refreshed when online. 0 = never expire. Mapbox raster tiles are commonly cached for up to 30 days.")]
    [Min(0)] public int cacheMaxAgeDays = MapDownloadPolicy.DefaultCacheMaxAgeDays;

    [Header("Network")]
    [Min(5)] public int httpTimeoutSeconds = 12;
    [Tooltip("Seconds between automatic connectivity re-checks.")]
    [Min(10f)] public float connectivityRecheckInterval = 30f;
    [Tooltip("Attempts per tile, including the first. 0 = policy default.")]
    [Range(0, 6)] public int maxDownloadAttempts = MapDownloadPolicy.DefaultMaxAttempts;
    [Tooltip("Simultaneous tile downloads. 0 = platform default (3 mobile, 4 desktop).")]
    [Range(0, 8)] public int maxConcurrentDownloads = 0;

    [Header("State (read-only)")]
    public bool isOnline;
    public bool authFailureDetected;
    public MapTileFailureKind lastFailureKind;
    public string lastFailureMessage;
    public int failedRequestCount;
    public int successfulRequestCount;

    /// <summary>Raised once per tile that could not be fetched after its attempts.</summary>
    public event Action<MapTileFailureKind, string> RequestFailed;

    // ── Private ────────────────────────────────────────────────────────

    string _cacheRoot;
    bool _loggedAuthFailure;
    float _connectivityTimer;
    bool _evicting;
    public bool forceOffline;
    int _concurrentLimit = MapDownloadPolicy.DesktopConcurrentDownloads;
    int _activeDownloads;

    /// <summary>One in-flight or just-finished tile request, shared by every caller asking for it.</summary>
    class TileJob
    {
        public string cachePath;
        public string url;
        public int zoom;
        public int x;
        public int y;
        public bool done;
        public bool success;
        public bool cached;
        public byte[] bytes;
        public MapTileFailureKind failure;
        public string message;
    }

    readonly Dictionary<string, TileJob> _jobs = new Dictionary<string, TileJob>(32);

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
        _jobs.Clear();
    }

    /// <summary>Publishes this manager and gives it a cache root. Idempotent.</summary>
    void Publish()
    {
        if (string.IsNullOrEmpty(_cacheRoot))
            _cacheRoot = Path.Combine(Application.persistentDataPath, cacheFolderName);
        // Outlives the scene the map is loaded from, so a tile cached while driving is still
        // there after a return to the menu.
        if (Application.isPlaying) DontDestroyOnLoad(gameObject);
        RefreshConcurrencyLimit();
    }

    void Start()
    {
        isOnline = !forceOffline && Application.internetReachability != NetworkReachability.NotReachable;
        RefreshConcurrencyLimit();
        EnforceCacheSizeAsync();
        Debug.Log($"[OfflineCacheManager] Online={isOnline}  CacheRoot={_cacheRoot}  " +
                  $"attempts={MaxAttempts}  concurrent={_concurrentLimit}  " +
                  $"pixelRatio={ResolvePixelRatio()}");
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
    /// null texture on failure. The texture is non-readable: no consumer samples tile pixels.
    /// </summary>
    public IEnumerator GetTileTexture(int zoom, int x, int y, Action<Texture2D> onDone)
        => GetTileTexture(zoom, x, y, ResolvePixelRatio(), onDone);

    public IEnumerator GetTileTexture(int zoom, int x, int y, int pixelRatio, Action<Texture2D> onDone)
    {
        MapboxMapSettings settings = MapboxMapSettings.Current;
        string cachePath = GetTileCachePath(settings.Style, zoom, x, y, pixelRatio);

        bool hasCache = File.Exists(cachePath);
        bool expired = hasCache && IsCacheExpired(cachePath);

        // ── Fresh cache hit ────────────────────────────────────────────
        if (hasCache && !expired)
        {
            Texture2D cached = LoadTextureFromDisk(cachePath);
            if (cached != null)
            {
                Touch(cachePath);
                onDone?.Invoke(cached);
                yield break;
            }
            DeleteCorruptCacheEntry(cachePath);
            hasCache = false;
        }

        isOnline = !forceOffline && Application.internetReachability != NetworkReachability.NotReachable;

        // ── Offline or unauthenticated: stale cache is better than a hole ──
        if (!isOnline || !settings.HasToken)
        {
            MapTileFailureKind kind = isOnline ? MapTileFailureKind.NoToken : MapTileFailureKind.Offline;
            if (!isOnline)
                Debug.LogWarning($"[OfflineCacheManager] Offline — no fresh cache for {zoom}/{x}/{y}.");
            else
                Debug.LogWarning($"[OfflineCacheManager] No Mapbox token — skipping tile {zoom}/{x}/{y} " +
                                 $"({settings.TokenSource}).");

            if (hasCache)
            {
                Texture2D stale = LoadTextureFromDisk(cachePath);
                if (stale != null)
                {
                    onDone?.Invoke(stale);
                    yield break;
                }
            }

            RecordFailure(kind, MapDownloadPolicy.Describe(kind), zoom, x, y);
            onDone?.Invoke(null);
            yield break;
        }

        // ── Download (coalesced across callers) ────────────────────────
        TileJob job = null;
        yield return EnsureJob(settings, zoom, x, y, pixelRatio, cachePath, j => job = j);

        if (job != null && job.success)
        {
            Texture2D tex = DecodeTile(job.bytes);
            if (tex != null)
            {
                onDone?.Invoke(tex);
                yield break;
            }
            RecordFailure(MapTileFailureKind.InvalidResponse,
                MapDownloadPolicy.Describe(MapTileFailureKind.InvalidResponse), zoom, x, y);
        }

        // A refresh that failed must not throw away a usable stale tile.
        if (hasCache)
        {
            Texture2D stale = LoadTextureFromDisk(cachePath);
            if (stale != null)
            {
                onDone?.Invoke(stale);
                yield break;
            }
        }

        onDone?.Invoke(null);
    }

    /// <summary>
    /// Downloads and stores a tile without decoding it into a texture. The city
    /// pre-downloader only needs the file on disk; this is what removes a full
    /// 4 MB texture allocation and a PNG encode from every pre-downloaded tile.
    /// </summary>
    public IEnumerator EnsureTileCached(int zoom, int x, int y, int pixelRatio, Action<bool> onDone)
    {
        MapboxMapSettings settings = MapboxMapSettings.Current;
        string cachePath = GetTileCachePath(settings.Style, zoom, x, y, pixelRatio);

        if (File.Exists(cachePath) && !IsCacheExpired(cachePath))
        {
            Touch(cachePath);
            onDone?.Invoke(true);
            yield break;
        }

        isOnline = !forceOffline && Application.internetReachability != NetworkReachability.NotReachable;
        if (!isOnline || !settings.HasToken)
        {
            MapTileFailureKind kind = isOnline ? MapTileFailureKind.NoToken : MapTileFailureKind.Offline;
            if (File.Exists(cachePath))
            {
                onDone?.Invoke(true);
                yield break;
            }
            RecordFailure(kind, MapDownloadPolicy.Describe(kind), zoom, x, y);
            onDone?.Invoke(false);
            yield break;
        }

        TileJob job = null;
        yield return EnsureJob(settings, zoom, x, y, pixelRatio, cachePath, j => job = j);
        onDone?.Invoke(job != null && job.success && File.Exists(cachePath));
    }

    /// <summary>Absolute path where a tile PNG is (or would be) stored.</summary>
    public string GetTileCachePath(string style, int zoom, int x, int y)
        => GetTileCachePath(style, zoom, x, y, 2);

    /// <summary>
    /// Absolute path for a tile at a pixel ratio. The @2x path is unchanged from
    /// the original cache layout, so existing offline downloads keep working; a
    /// 1x tile gets its own sibling folder and can never be served to a 2x loader.
    /// </summary>
    public string GetTileCachePath(string style, int zoom, int x, int y, int pixelRatio)
    {
        string safeStyle = style.Replace('/', '_').Replace(':', '_');
        if (pixelRatio < 2) safeStyle += "@1x";
        return Path.Combine(_cacheRoot, safeStyle, zoom.ToString(), x.ToString(), $"{y}.png");
    }

    /// <summary>True if the tile is already in local cache.</summary>
    public bool IsTileCached(string style, int zoom, int x, int y)
        => IsTileCached(style, zoom, x, y, 2);

    public bool IsTileCached(string style, int zoom, int x, int y, int pixelRatio)
        => File.Exists(GetTileCachePath(style, zoom, x, y, pixelRatio));

    /// <summary>Delete all cached tiles for a given style + zoom level, at every pixel ratio.</summary>
    public void ClearCacheForZoom(string style, int zoom)
    {
        string safeStyle = style.Replace('/', '_').Replace(':', '_');
        DeleteCacheDirectory(Path.Combine(_cacheRoot, safeStyle, zoom.ToString()));
        DeleteCacheDirectory(Path.Combine(_cacheRoot, safeStyle + "@1x", zoom.ToString()));
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

    /// <summary>The pixel ratio this device resolves to, for diagnostics.</summary>
    public int ResolvePixelRatio()
        => MapDownloadPolicy.ResolvePixelRatio(0, Application.isMobilePlatform,
            QualitySettings.GetQualityLevel());

    // ── Shared download ────────────────────────────────────────────────

    IEnumerator EnsureJob(MapboxMapSettings settings, int zoom, int x, int y,
        int pixelRatio, string cachePath, Action<TileJob> onDone)
    {
        if (this == null)
        {
            onDone?.Invoke(null);
            yield break;
        }

        if (_jobs.TryGetValue(cachePath, out TileJob existing) && !existing.done)
        {
            while (existing != null && !existing.done)
            {
                if (this == null) { onDone?.Invoke(null); yield break; }
                yield return null;
            }
            onDone?.Invoke(existing);
            yield break;
        }

        var job = new TileJob
        {
            cachePath = cachePath,
            url = MapboxTileRequest.BuildTileUrl(settings.Style, zoom, x, y, settings.Token, pixelRatio),
            zoom = zoom,
            x = x,
            y = y
        };
        _jobs[cachePath] = job;

        // Run on the persistent cache object, not on the caller. A loader that is
        // destroyed while waiting must not cancel a download another caller is
        // still waiting for.
        StartCoroutine(RunJob(job, settings));

        while (job != null && !job.done)
        {
            if (this == null) { onDone?.Invoke(null); yield break; }
            yield return null;
        }

        if (_jobs.TryGetValue(cachePath, out TileJob current) && ReferenceEquals(current, job))
            _jobs.Remove(cachePath);

        onDone?.Invoke(job);
    }

    IEnumerator RunJob(TileJob job, MapboxMapSettings settings)
    {
        int attempts = MaxAttempts;
        for (int attempt = 1; attempt <= attempts; attempt++)
        {
            while (_activeDownloads >= _concurrentLimit)
                yield return null;

            _activeDownloads++;
            UnityWebRequest request = null;
            string retryAfter = null;
            try
            {
                request = UnityWebRequest.Get(job.url);
                request.timeout = Mathf.Max(1, httpTimeoutSeconds);
                request.SetRequestHeader("Accept", "image/png,image/jpeg;q=0.9,*/*;q=0.1");
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    isOnline = true;
                    // A tile that arrives proves the credential is working again;
                    // a single transient 401 must not keep the whole map halted.
                    if (authFailureDetected)
                    {
                        authFailureDetected = false;
                        _loggedAuthFailure = false;
                    }
                    byte[] bytes = request.downloadHandler != null ? request.downloadHandler.data : null;
                    if (!MapDownloadPolicy.LooksLikeImage(bytes))
                    {
                        job.failure = MapTileFailureKind.InvalidResponse;
                        job.message = $"HTTP {request.responseCode} was not a supported PNG/JPEG image " +
                                      $"(Content-Type: {request.GetResponseHeader("Content-Type") ?? "unknown"}).";
                    }
                    else
                    {
                        job.bytes = bytes;
                        job.success = true;
                        job.cached = WriteBytesToCache(job.cachePath, bytes);
                        if (!job.cached)
                        {
                            job.failure = MapTileFailureKind.CacheWrite;
                            job.message = "The tile arrived but could not be written to the cache.";
                        }
                        successfulRequestCount++;
                        if (job.cached) EnforceCacheSizeAsync();
                        break;
                    }
                }
                else
                {
                    job.failure = MapDownloadPolicy.Classify(request.result, request.responseCode,
                        request.error, !string.IsNullOrEmpty(settings.Token));
                    job.message = DescribeRequestFailure(request, job.failure);
                    retryAfter = request.GetResponseHeader("Retry-After");
                }
            }
            finally
            {
                _activeDownloads--;
                request?.Dispose();
            }

            if (job.success || !MapDownloadPolicy.IsRetryable(job.failure) || attempt >= attempts)
                break;

            float delay = MapDownloadPolicy.RetryDelaySeconds(attempt, retryAfter, UnityEngine.Random.value);
            if (attempt == 1)
                Debug.LogWarning($"[OfflineCacheManager] Tile {job.zoom}/{job.x}/{job.y} " +
                                 $"failed ({job.failure}); retrying in {delay:0.0}s.");
            yield return new WaitForSecondsRealtime(delay);
        }

        if (job.success && job.failure == MapTileFailureKind.CacheWrite)
            RecordFailure(job.failure, job.message, job.zoom, job.x, job.y);
        else if (!job.success)
            RecordFailure(job.failure, job.message, job.zoom, job.x, job.y);

        // Waiting callers hold their own reference to the job and only need it for
        // this frame. Keeping the entry here would pin the tile's response bytes (and
        // its token-bearing URL) for every tile ever downloaded, which for a city
        // pre-download is hundreds of megabytes. The next caller finds the tile on
        // disk instead of in this dictionary.
        job.done = true;
        if (_jobs.TryGetValue(job.cachePath, out TileJob finished) && ReferenceEquals(finished, job))
            _jobs.Remove(job.cachePath);
    }

    string DescribeRequestFailure(UnityWebRequest request, MapTileFailureKind kind)
    {
        long status = request != null ? request.responseCode : 0;
        string body = request?.downloadHandler?.text ?? string.Empty;
        if (body.Length > 160) body = body.Substring(0, 160);
        string detail = MapDownloadPolicy.Describe(kind);
        if (status > 0) detail += $" (HTTP {status})";
        if (!string.IsNullOrWhiteSpace(body)) detail += $" | {body}";
        return detail;
    }

    int MaxAttempts => MapDownloadPolicy.ResolveMaxAttempts(maxDownloadAttempts);

    void RefreshConcurrencyLimit()
    {
        _concurrentLimit = MapDownloadPolicy.ResolveConcurrentDownloads(
            Application.isMobilePlatform, maxConcurrentDownloads);
    }

    void RecordFailure(MapTileFailureKind kind, string message, int zoom, int x, int y)
    {
        lastFailureKind = kind;
        lastFailureMessage = message;
        failedRequestCount++;

        if (kind == MapTileFailureKind.Auth)
        {
            authFailureDetected = true;
            if (!_loggedAuthFailure)
            {
                _loggedAuthFailure = true;
                Debug.LogError("[OfflineCacheManager] Mapbox authorisation failed. Verify your token has " +
                               "'styles:tiles' scope and the style is public or accessible.");
            }
        }
        else if (kind != MapTileFailureKind.Offline && kind != MapTileFailureKind.NoToken)
        {
            Debug.LogWarning($"[OfflineCacheManager] Tile {zoom}/{x}/{y} failed: {message}");
        }

        RequestFailed?.Invoke(kind, message);
    }

    // ── Private helpers ────────────────────────────────────────────────

    bool IsCacheExpired(string path)
    {
        if (cacheMaxAgeDays <= 0) return false;
        try
        {
            return File.GetLastWriteTimeUtc(path) <
                   DateTime.UtcNow.AddDays(-cacheMaxAgeDays);
        }
        catch (IOException) { return false; }
        catch (UnauthorizedAccessException) { return false; }
    }

    static void Touch(string path)
    {
        try { File.SetLastWriteTimeUtc(path, DateTime.UtcNow); } catch (IOException) { }
        catch (UnauthorizedAccessException) { }
    }

    void DeleteCorruptCacheEntry(string path)
    {
        try { File.Delete(path); } catch (IOException) { } catch (UnauthorizedAccessException) { }
        try { File.Delete(path + ".tmp"); } catch (IOException) { } catch (UnauthorizedAccessException) { }
    }

    static Texture2D LoadTextureFromDisk(string path)
    {
        Texture2D tex = null;
        try
        {
            byte[] bytes = File.ReadAllBytes(path);
            if (!MapDownloadPolicy.LooksLikeImage(bytes))
            {
                tex = null;
            }
            else
            {
                tex = new Texture2D(2, 2, TextureFormat.RGBA32, true);
                // No consumer reads map tile pixels; keeping the texture readable
                // doubles its CPU-side memory and prevents Unity from dropping the
                // backing store after upload.
                if (!tex.LoadImage(bytes, true))
                {
                    Destroy(tex);
                    tex = null;
                }
            }
        }
        catch (IOException) { }
        catch (UnauthorizedAccessException) { }

        if (tex != null) tex.name = Path.GetFileName(path);
        return tex;
    }

    static Texture2D DecodeTile(byte[] bytes)
    {
        if (!MapDownloadPolicy.LooksLikeImage(bytes))
            return null;

        var tex = new Texture2D(2, 2, TextureFormat.RGBA32, true);
        if (tex.LoadImage(bytes, true))
            return tex;

        Destroy(tex);
        return null;
    }

    bool WriteBytesToCache(string path, byte[] bytes)
    {
        try
        {
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            string temp = path + ".tmp";
            File.WriteAllBytes(temp, bytes);
            if (File.Exists(path)) File.Delete(path);
            File.Move(temp, path);
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"[OfflineCacheManager] Cache write failed: {ex.Message}");
            return false;
        }
    }

    static void DeleteCacheDirectory(string dir)
    {
        try
        {
            if (Directory.Exists(dir))
                Directory.Delete(dir, recursive: true);
        }
        catch (IOException) { }
        catch (UnauthorizedAccessException) { }
    }

    /// <summary>
    /// Fire-and-forget: if cache is over budget, delete oldest files until under.
    /// Runs as a coroutine to avoid a main-thread stall.
    /// </summary>
    void EnforceCacheSizeAsync()
    {
        if (!_evicting && Application.isPlaying) StartCoroutine(EvictOnce());
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
}
