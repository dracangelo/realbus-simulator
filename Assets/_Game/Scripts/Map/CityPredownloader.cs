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
    public string mapboxToken = "";
    public string mapStyle    = "mapbox/satellite-streets-v12";
    public int    zoomLevel   = 15;

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
        _cache = OfflineCacheManager.Instance ?? FindFirstObjectByType<OfflineCacheManager>();
    }

    // ── Public API ─────────────────────────────────────────────────────

    public void StartDownloadForCity(CityDefinition city)
    {
        if (isDownloading || city == null) return;
        StartCoroutine(DownloadRoutine(city));
    }

    public void CancelDownload()
    {
        if (!isDownloading) return;
        isCancelled = true;
        Debug.Log("[CityPredownloader] Download cancelled by user.");
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
        _cache = OfflineCacheManager.Instance ?? FindFirstObjectByType<OfflineCacheManager>();
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
        estimatedMB = totalTiles * 0.22f;
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
                if (_cache != null && _cache.IsTileCached(mapStyle, zoomLevel, tile.x, tile.y))
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
                if (_cache.IsTileCached(mapStyle, zoomLevel, tile.x, tile.y)) completedTiles++;
            failedTiles = totalTiles - completedTiles;
            FireProgress();
        }
        isDownloading = false;
        SaveStatus(city, !isCancelled && failedTiles == 0 && completedTiles == totalTiles);
        Debug.Log($"[CityPredownloader] Done — {completedTiles} OK, {failedTiles} failed, " +
                  $"cancelled={isCancelled}.  Total ≈ {estimatedMB:F1} MB.");
        OnComplete?.Invoke(isCancelled);
    }

    IEnumerator DownloadOneTile(int x, int y, System.Action<bool, long> onDone)
    {
        if (_cache == null) { onDone?.Invoke(false, 0); yield break; }

        bool  success   = false;
        long  byteCount = 0;

        for (int attempt = 0; attempt <= retryAttempts && !success && !isCancelled; attempt++)
        {
            if (attempt > 0)
                yield return new WaitForSeconds(0.5f * attempt); // back-off

            Texture2D tex = null;
            yield return _cache.GetTileTexture(
                mapboxToken, mapStyle, zoomLevel, x, y, t => tex = t);

            if (tex != null)
            {
                // Rough byte estimate from texture dimensions (@2x PNG).
                byteCount = (long)(tex.width * tex.height * 4 * 0.6f); // ~60 % PNG compression
                success = _cache.IsTileCached(mapStyle, zoomLevel, x, y);
                if (success) byteCount = new System.IO.FileInfo(_cache.GetTileCachePath(mapStyle, zoomLevel, x, y)).Length;
                Destroy(tex);
            }
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
        CityDownloadStatus status = new CityDownloadStatus { cityCode = CityKey(city), mapStyle = mapStyle, zoom = zoomLevel };
        string json = PlayerPrefs.GetString(key, string.Empty);
        if (!string.IsNullOrWhiteSpace(json))
        {
            try { JsonUtility.FromJsonOverwrite(json, status); } catch (System.Exception) { }
        }
        if (status.total <= 0 || status.zoom != zoomLevel || status.mapStyle != mapStyle)
        {
            status.mapStyle = mapStyle; status.zoom = zoomLevel;
            status.total = GetTilesInRadius(city.centreLat, city.centreLon, zoomLevel, radiusMeters, tileWorldSize).Count;
            status.estimatedMb = status.total * 0.22f;
        }
        if (verifyCache)
        {
            _cache = OfflineCacheManager.Instance ?? FindFirstObjectByType<OfflineCacheManager>();
            if (_cache != null)
            {
                int cached = 0;
                var tiles = GetTilesInRadius(city.centreLat, city.centreLon, zoomLevel, radiusMeters, tileWorldSize);
                for (int i = 0; i < tiles.Count; i++) if (_cache.IsTileCached(mapStyle, zoomLevel, tiles[i].x, tiles[i].y)) cached++;
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
            cityCode = CityKey(city), mapStyle = mapStyle, zoom = zoomLevel, total = totalTiles,
            cached = completedTiles, failed = failedTiles, estimatedMb = estimatedMB, complete = complete,
            updatedAt = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };
        PlayerPrefs.SetString(StatusKey(city), JsonUtility.ToJson(status)); PlayerPrefs.Save();
    }

    static string CityKey(CityDefinition city) { return !string.IsNullOrWhiteSpace(city.cityCode) ? city.cityCode : city.cityName; }
    static string StatusKey(CityDefinition city) { return "RealBus.CityDownload.v1." + CityKey(city); }
}
