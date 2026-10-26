using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Pre-downloads map tiles for a city so the player can drive offline.
/// Exposed via Settings → Offline Maps → [City] → Download.
///
/// Features vs original:
///   • Cancel support (<see cref="CancelDownload"/>).
///   • Per-tile retry (up to <see cref="retryAttempts"/> times).
///   • Running MB estimate updated as real tile sizes are observed.
///   • Progress events carry both ratio (0–1) and completed/total counts.
/// </summary>
public class CityPredownloader : MonoBehaviour
{
    [System.Serializable]
    public class CityDownloadStatus
    {
        public string cityCode;
        /// <summary>The resolved style these tiles were written for (see <see cref="MapboxMapSettings"/>).</summary>
        public string mapStyle;
        public int zoom;
        public int total;
        public int cached;
        public int failed;
        public float estimatedMb;
        public bool complete;
        public long updatedAt;
    }
    // ── Inspector ──────────────────────────────────────────────────────

    [Header("Mapbox")]
    public int    zoomLevel   = 15;
    [Tooltip("1 = 512 px tiles, 2 = 1024 px @2x. 0 = copy the loader / platform policy.")]
    [Range(0, 2)] public int tilePixelRatio = 0;

    /// <summary>
    /// The credential and the style are not authored here: they come from the one resolved
    /// configuration the scene's loader publishes, so a pre-download writes the same tiles the
    /// game will later read, and a saved status cannot name a style nothing else is using.
    /// </summary>
    static MapboxMapSettings Settings => MapboxMapSettings.Current;

    [Header("Download Area")]
    [Tooltip("Radius around the city centre to pre-download (metres).")]
    [Min(500f)] public float radiusMeters = 2000f;
    [Tooltip("World-space size of one tile (metres). Must match MapTileLoader.")]
    [Min(50f)]  public float tileWorldSize = 200f;

    [Header("Concurrency & Retry")]
    [Range(1, 8)] public int maxConcurrent = 4;
    [Range(0, 3)] public int retryAttempts = 1;

    [Header("State (read-only)")]
    public bool  isDownloading;
    public bool  isCancelled;
    public int   totalTiles;
    public int   completedTiles;
    public int   failedTiles;
    public float estimatedMB;

    // ── Events ─────────────────────────────────────────────────────────

    /// <summary>Progress ratio 0–1.</summary>
    public event System.Action<float> OnProgress;
    /// <summary>Updated MB estimate as real tile sizes come in.</summary>
    public event System.Action<float> OnEstimatedMbChanged;
    /// <summary>Fired on completion (success or cancel). bool = wasCancelled.</summary>
    public event System.Action<bool>  OnComplete;

    // ── Private ────────────────────────────────────────────────────────

    OfflineCacheManager _cache;
    long  _totalBytesObserved;
    int   _tilesWithSizeData;
    CityDefinition _activeCity;

    // ── Lifecycle ──────────────────────────────────────────────────────

    void Start()
    {
        _cache = OfflineCacheManager.EnsureInstance();
        SyncTileGeometryFromLoader();
    }

    /// <summary>
    /// True only when every tile that was asked for is on disk afterwards. Nothing asked for
    /// is not a success either, and neither is a run in which one tile came back missing: the
    /// menu printed "Offline map ready." for a download where every single tile had failed,
    /// because the only thing it knew was that the user had not cancelled.
    /// </summary>
    public static bool DownloadCoveredEveryTile(int completed, int failed, int total)
        => total > 0 && failed <= 0 && completed >= total;

    /// <summary>Whether the last download left the whole requested area cached. Read by the menu.</summary>
    public bool LastDownloadCoveredEveryTile { get; private set; }

    /// <summary>Why the last attempt could not start. Null after a successful start.</summary>
    public string LastFailure { get; private set; }

    // ── Public API ─────────────────────────────────────────────────────

    /// <summary>
    /// Starts a map pre-download. Returns false with <see cref="LastFailure"/>
    /// filled when the map cannot be fetched; the caller can then tell the
    /// player that the built-in city layout is still playable.
    /// </summary>
    public bool StartDownloadForCity(CityDefinition city)
    {
        if (city == null)
        {
            LastFailure = "Choose a city first.";
            return false;
        }
        if (isDownloading)
        {
            LastFailure = "A map download is already running.";
            return false;
        }
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            LastFailure = "No network connection. The built-in city layout is still available.";
            return false;
        }

        SyncTileGeometryFromLoader();
        if (!Settings.HasToken)
        {
            LastFailure = "No Mapbox token is configured. The built-in city layout is still available.";
            Debug.LogWarning($"[CityPredownloader] Map download needs the token the rest of the map stack uses, " +
                             $"and none was resolved ({Settings.TokenSource}).");
            return false;
        }

        // A download without a cache cannot store a single tile, so it would run to the end
        // and report failure for reasons that have nothing to do with the network.
        _cache = OfflineCacheManager.EnsureInstance();
        if (_cache == null)
        {
            LastFailure = "The tile cache could not be created. The built-in city layout is still available.";
            Debug.LogWarning("[CityPredownloader] No tile cache could be created, so nothing can be stored offline.");
            return false;
        }

        LastFailure = null;
        StartCoroutine(DownloadRoutine(city));
        return true;
    }

    /// <summary>
    /// Copies the tile geometry from the scene's loader so a pre-download covers the zoom and
    /// footprint the game will actually read back. The token and style are not copied: they are
    /// read from the shared resolved configuration wherever they are needed.
    /// </summary>
    void SyncTileGeometryFromLoader()
    {
        var loader = FindAnyObjectByType<MapTileLoader>();
        if (loader == null) return;

        zoomLevel = loader.zoomLevel;
        tileWorldSize = loader.tileWorldSize;
        // The pre-download has to write the same cache folder the loader will
        // read. Copy the authored choice, not the loader's resolved value: the
        // loader may not have run its first LoadMap yet, and both sides resolve
        // platform/quality the same way when they actually use the ratio.
        tilePixelRatio = loader.tilePixelRatio;
    }

    /// <summary>The raster ratio this download is writing (1 or 2).</summary>
    public int ActivePixelRatio => MapDownloadPolicy.ResolvePixelRatio(
        tilePixelRatio, Application.isMobilePlatform, QualitySettings.GetQualityLevel());

    float EstimatedMBPerTile => ActivePixelRatio >= 2 ? 0.22f : 0.06f;

    public void CancelDownload()
    {
        if (!isDownloading) return;
        isCancelled = true;
        Debug.Log("[CityPredownloader] Download cancelled by user.");
    }

    /// <summary>Removes only tiles covered by this city's saved offline area.</summary>
    public void DeleteDownloadedMap(CityDefinition city)
    {
        if (city == null || isDownloading) return;
        _cache = OfflineCacheManager.EnsureInstance();
        if (_cache != null)
            foreach (var tile in GetTilesInRadius(city.centreLat, city.centreLon, zoomLevel, radiusMeters, tileWorldSize))
            {
                string path = _cache.GetTileCachePath(Settings.Style, zoomLevel, tile.x, tile.y, ActivePixelRatio);
                try { if (System.IO.File.Exists(path)) System.IO.File.Delete(path); } catch (System.IO.IOException) { }
            }
        PlayerPrefs.DeleteKey(StatusKey(city)); PlayerPrefs.Save();
        Debug.Log($"[CityPredownloader] Removed cached map tiles for {city.cityName}.");
    }

    // ── Tile key helpers (static so MapTileLoader can share the same math) ──

    /// <summary>Returns all tile (x,y) pairs within radius, sorted closest-first.</summary>
    public static List<(int x, int y, float dist)> GetTilesInRadius(  // named tuple fields
        double centerLat, double centerLon,
        int zoom, float radiusMeters, float tileWorldSizeMeters)
    {
        // Retain the legacy parameter for callers; tile size is determined by latitude and zoom.
        return SlippyTileCoverage.InRadius(centerLat, centerLon, zoom, radiusMeters);
    }

    // ── Download coroutine ─────────────────────────────────────────────

    IEnumerator DownloadRoutine(CityDefinition city)
    {
        _cache = OfflineCacheManager.EnsureInstance();
        _activeCity = city;
        isDownloading   = true;
        isCancelled     = false;
        completedTiles  = 0;
        failedTiles     = 0;
        _totalBytesObserved = 0;
        _tilesWithSizeData  = 0;

        var tiles  = GetTilesInRadius(city.centreLat, city.centreLon,
                                      zoomLevel, radiusMeters, tileWorldSize);
        totalTiles = tiles.Count;

        // Initial estimate: 0.22 MB per @2x PNG tile (conservative)
        estimatedMB = totalTiles * EstimatedMBPerTile;
        OnEstimatedMbChanged?.Invoke(estimatedMB);
        SaveStatus(city, false);

        Debug.Log($"[CityPredownloader] Starting: {city.cityName} — {totalTiles} tiles.");

        int inFlight = 0;
        int index    = 0;

        while ((completedTiles + failedTiles) < totalTiles && !isCancelled)
        {
            while (inFlight < Mathf.Max(1, maxConcurrent) && index < tiles.Count && !isCancelled)
            {
                var tile = tiles[index++];
                inFlight++;

                // Skip already-cached tiles immediately.
                if (_cache != null && _cache.IsTileCached(Settings.Style, zoomLevel, tile.x, tile.y, ActivePixelRatio))
                {
                    inFlight--;
                    completedTiles++;
                    FireProgress();
                    continue;
                }

                StartCoroutine(DownloadOneTile(tile.x, tile.y, (success, bytes) =>
                {
                    inFlight--;
                    if (success)
                    {
                        completedTiles++;
                        UpdateSizeEstimate(bytes);
                    }
                    else
                    {
                        failedTiles++;
                    }
                    FireProgress();
                }));
            }
            yield return null;
        }

        // Drain workers before allowing a new job to reuse the counters/settings.
        while (inFlight > 0) yield return null;
        if (!isCancelled && _cache != null)
        {
            // A successful response is not proof of offline availability after LRU eviction.
            completedTiles = 0;
            foreach (var tile in tiles)
                if (_cache.IsTileCached(Settings.Style, zoomLevel, tile.x, tile.y, ActivePixelRatio)) completedTiles++;
            failedTiles = totalTiles - completedTiles;
            FireProgress();
        }
        isDownloading = false;
        LastDownloadCoveredEveryTile = !isCancelled && DownloadCoveredEveryTile(completedTiles, failedTiles, totalTiles);
        SaveStatus(city, LastDownloadCoveredEveryTile);
        Debug.Log($"[CityPredownloader] Done — {completedTiles} OK, {failedTiles} failed, " +
                  $"cancelled={isCancelled}, offlineReady={LastDownloadCoveredEveryTile}.  Total ≈ {estimatedMB:F1} MB.");
        OnComplete?.Invoke(isCancelled);
    }

    IEnumerator DownloadOneTile(int x, int y, System.Action<bool, long> onDone)
    {
        if (_cache == null) { onDone?.Invoke(false, 0); yield break; }

        bool success = false;
        long byteCount = 0;
        int ratio = ActivePixelRatio;

        // The cache coalesces duplicate requests and retries transient failures
        // itself. The outer loop only exists for a caller that wants one more
        // attempt after the cache has given up.
        for (int attempt = 0; attempt <= retryAttempts && !success && !isCancelled; attempt++)
        {
            if (attempt > 0)
                yield return new WaitForSecondsRealtime(
                    MapDownloadPolicy.RetryDelaySeconds(attempt, UnityEngine.Random.value));

            bool cached = false;
            yield return _cache.EnsureTileCached(zoomLevel, x, y, ratio, c => cached = c);
            if (!cached) continue;

            success = true;
            try
            {
                byteCount = new System.IO.FileInfo(
                    _cache.GetTileCachePath(Settings.Style, zoomLevel, x, y, ratio)).Length;
            }
            catch (System.IO.IOException) { }
            catch (System.UnauthorizedAccessException) { }
        }

        onDone?.Invoke(success, byteCount);
    }

    // ── Helpers ────────────────────────────────────────────────────────

    void FireProgress()
    {
        int done  = completedTiles + failedTiles;
        float p   = totalTiles > 0 ? (float)done / totalTiles : 1f;
        OnProgress?.Invoke(p);
        if (_activeCity != null && (done == totalTiles || done % 8 == 0)) SaveStatus(_activeCity, false);
    }

    void UpdateSizeEstimate(long observedBytes)
    {
        if (observedBytes <= 0) return;
        _totalBytesObserved += observedBytes;
        _tilesWithSizeData++;
        float avgMB   = (_totalBytesObserved / (float)_tilesWithSizeData) / (1024f * 1024f);
        estimatedMB   = avgMB * totalTiles;
        OnEstimatedMbChanged?.Invoke(estimatedMB);
    }

    public CityDownloadStatus GetStatus(CityDefinition city, bool verifyCache = true)
    {
        if (city == null) return null;
        string key = StatusKey(city);
        CityDownloadStatus status = new CityDownloadStatus { cityCode = CityKey(city), mapStyle = Settings.Style, zoom = zoomLevel };
        string json = PlayerPrefs.GetString(key, string.Empty);
        if (!string.IsNullOrWhiteSpace(json))
        {
            try { JsonUtility.FromJsonOverwrite(json, status); } catch (System.Exception) { }
        }
        if (status.total <= 0 || status.zoom != zoomLevel || status.mapStyle != Settings.Style)
        {
            status.mapStyle = Settings.Style; status.zoom = zoomLevel;
            status.total = GetTilesInRadius(city.centreLat, city.centreLon, zoomLevel, radiusMeters, tileWorldSize).Count;
            status.estimatedMb = status.total * EstimatedMBPerTile;
        }
        if (verifyCache)
        {
            _cache = OfflineCacheManager.EnsureInstance();
            if (_cache != null)
            {
                int cached = 0;
                var tiles = GetTilesInRadius(city.centreLat, city.centreLon, zoomLevel, radiusMeters, tileWorldSize);
                for (int i = 0; i < tiles.Count; i++) if (_cache.IsTileCached(Settings.Style, zoomLevel, tiles[i].x, tiles[i].y, ActivePixelRatio)) cached++;
                status.cached = cached; status.total = tiles.Count; status.complete = cached == tiles.Count && tiles.Count > 0;
            }
        }
        return status;
    }

    public static long GetAvailableStorageBytes()
    {
        try
        {
            string root = System.IO.Path.GetPathRoot(Application.persistentDataPath);
            return string.IsNullOrWhiteSpace(root) ? -1 : new System.IO.DriveInfo(root).AvailableFreeSpace;
        }
        catch (System.Exception) { return -1; }
    }

    void SaveStatus(CityDefinition city, bool complete)
    {
        if (city == null) return;
        CityDownloadStatus status = new CityDownloadStatus
        {
            cityCode = CityKey(city), mapStyle = Settings.Style, zoom = zoomLevel, total = totalTiles,
            cached = completedTiles, failed = failedTiles, estimatedMb = estimatedMB, complete = complete,
            updatedAt = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };
        PlayerPrefs.SetString(StatusKey(city), JsonUtility.ToJson(status)); PlayerPrefs.Save();
    }

    static string CityKey(CityDefinition city) { return !string.IsNullOrWhiteSpace(city.cityCode) ? city.cityCode : city.cityName; }
    static string StatusKey(CityDefinition city) { return "RealBus.CityDownload.v1." + CityKey(city); }
}
