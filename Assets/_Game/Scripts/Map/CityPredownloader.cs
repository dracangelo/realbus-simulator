using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CityPredownloader : MonoBehaviour
{
    [Header("Mapbox")]
    public string mapboxToken = "";
    public string mapStyle = "mapbox/satellite-streets-v12";
    public int zoomLevel = 15;

    [Header("Download Area")]
    public float radiusMeters = 2000f;
    public float tileWorldSize = 200f;
    public int maxConcurrent = 4;

    [Header("State (read-only)")]
    public bool downloading;
    public int totalTiles;
    public int completedTiles;
    public float estimatedMegabytes;

    OfflineCacheManager cache;

    public System.Action<float> OnProgress01;
    public System.Action<float> OnEstimatedMb;

    void Start()
    {
        cache = OfflineCacheManager.Instance != null ? OfflineCacheManager.Instance : FindFirstObjectByType<OfflineCacheManager>();
    }

    public void StartDownloadForCity(CityDefinition city)
    {
        if (downloading || city == null) return;
        StartCoroutine(DownloadRoutine(city));
    }

    IEnumerator DownloadRoutine(CityDefinition city)
    {
        downloading = true;
        completedTiles = 0;

        int centerX = MapTileLoader.LonToTileX(city.centreLon, zoomLevel);
        int centerY = MapTileLoader.LatToTileY(city.centreLat, zoomLevel);
        int tileRadius = Mathf.CeilToInt(radiusMeters / Mathf.Max(1f, tileWorldSize));

        var keys = new List<(int x, int y, float dist)>();
        for (int dx = -tileRadius; dx <= tileRadius; dx++)
        {
            for (int dy = -tileRadius; dy <= tileRadius; dy++)
            {
                float d = Mathf.Sqrt(dx * dx + dy * dy) * tileWorldSize;
                if (d > radiusMeters) continue;
                keys.Add((centerX + dx, centerY + dy, d));
            }
        }
        keys.Sort((a, b) => a.dist.CompareTo(b.dist));

        totalTiles = keys.Count;
        estimatedMegabytes = (totalTiles * 0.22f); // rough default for @2x PNG tiles; update after first real downloads if desired.
        OnEstimatedMb?.Invoke(estimatedMegabytes);

        int inFlight = 0;
        int index = 0;

        while (completedTiles < totalTiles)
        {
            while (inFlight < maxConcurrent && index < keys.Count)
            {
                var k = keys[index++];
                inFlight++;
                StartCoroutine(DownloadOne(k.x, k.y, () =>
                {
                    completedTiles++;
                    inFlight--;
                    float p = totalTiles > 0 ? (float)completedTiles / totalTiles : 1f;
                    OnProgress01?.Invoke(p);
                }));
            }
            yield return null;
        }

        downloading = false;
    }

    IEnumerator DownloadOne(int x, int y, System.Action onDone)
    {
        if (cache != null)
        {
            Texture2D tex = null;
            yield return cache.GetTileTexture(mapboxToken, mapStyle, zoomLevel, x, y, t => tex = t);
            // texture may be null if offline and not cached; still count as done so UI doesn't hang forever
        }
        onDone?.Invoke();
    }
}

