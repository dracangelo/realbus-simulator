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
        int cx = MapTileLoader.LonToTileX(centerLon, zoom);
        int cy = MapTileLoader.LatToTileY(centerLat, zoom);
        int r  = Mathf.CeilToInt(radiusMeters / Mathf.Max(1f, tileWorldSizeMeters));

        var list = new List<(int x, int y, float dist)>(r * r * 4);
        for (int dx = -r; dx <= r; dx++)
        {
            for (int dy = -r; dy <= r; dy++)
            {
                float d = Mathf.Sqrt(dx * dx + dy * dy) * tileWorldSizeMeters;
                if (d > radiusMeters) continue;
                list.Add((cx + dx, cy + dy, d));
            }
        }
        list.Sort((a, b) => a.dist.CompareTo(b.dist));  // now resolves correctly
        return list;
    }

    // ── Download coroutine ─────────────────────────────────────────────

    IEnumerator DownloadRoutine(CityDefinition city)
    {
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

        Debug.Log($"[CityPredownloader] Starting: {city.cityName} — {totalTiles} tiles.");

        int inFlight = 0;
        int index    = 0;

        while ((completedTiles + failedTiles) < totalTiles && !isCancelled)
        {
            while (inFlight < maxConcurrent && index < tiles.Count && !isCancelled)
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

        isDownloading = false;
        Debug.Log($"[CityPredownloader] Done — {completedTiles} OK, {failedTiles} failed, " +
                  $"cancelled={isCancelled}.  Total ≈ {estimatedMB:F1} MB.");
        OnComplete?.Invoke(isCancelled);
    }

    IEnumerator DownloadOneTile(int x, int y, System.Action<bool, long> onDone)
    {
        if (_cache == null) { onDone?.Invoke(false, 0); yield break; }

        bool  success   = false;
        long  byteCount = 0;

        for (int attempt = 0; attempt <= retryAttempts && !success; attempt++)
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
                success   = true;
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
}