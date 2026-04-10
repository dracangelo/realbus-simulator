using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileStreamManager : MonoBehaviour
{
    [Header("Mapbox")]
    public string mapboxToken = "";
    public string mapStyle = "mapbox/satellite-streets-v12";
    public int zoomLevel = 15;

    [Header("Streaming Radii")]
    public float loadRadiusMeters = 2000f;
    public float unloadRadiusMeters = 3000f;

    [Header("Tile Settings")]
    public float tileWorldSize = 200f;
    public int tileLayer = 0;
    public bool createMeshCollider = false;

    [Header("Performance")]
    public int maxConcurrentDownloads = 6;
    public float refreshEverySeconds = 0.5f;

    [Header("References")]
    public Transform busTransform;
    public CoordinateConverter converter;
    public OfflineCacheManager cache;
    public TileStreamingLoadingUI loadingUI;

    readonly Dictionary<TileKey, GameObject> activeTiles = new Dictionary<TileKey, GameObject>();
    readonly HashSet<TileKey> loadingTiles = new HashSet<TileKey>();
    float t;

    void Start()
    {
        if (converter == null) converter = CoordinateConverter.Instance != null ? CoordinateConverter.Instance : FindFirstObjectByType<CoordinateConverter>();
        if (cache == null) cache = OfflineCacheManager.Instance != null ? OfflineCacheManager.Instance : FindFirstObjectByType<OfflineCacheManager>();
        if (busTransform == null)
        {
            var bus = FindFirstObjectByType<BusController>();
            if (bus != null) busTransform = bus.transform;
        }
    }

    void Update()
    {
        if (busTransform == null || converter == null) return;

        t += Time.deltaTime;
        if (t < refreshEverySeconds) return;
        t = 0f;

        StreamTiles();
    }

    void StreamTiles()
    {
        var gps = converter.WorldToGeoPosition(busTransform.position);
        int centerX = MapTileLoader.LonToTileX(gps.lon, zoomLevel);
        int centerY = MapTileLoader.LatToTileY(gps.lat, zoomLevel);

        int tileRadius = Mathf.CeilToInt(loadRadiusMeters / Mathf.Max(1f, tileWorldSize));

        var needed = new List<(TileKey key, float dist)>(tileRadius * tileRadius);
        for (int dx = -tileRadius; dx <= tileRadius; dx++)
        {
            for (int dy = -tileRadius; dy <= tileRadius; dy++)
            {
                float worldDist = Mathf.Sqrt(dx * dx + dy * dy) * tileWorldSize;
                if (worldDist > loadRadiusMeters) continue;

                var key = new TileKey(zoomLevel, centerX + dx, centerY + dy);
                needed.Add((key, worldDist));
            }
        }

        // Priority: closest first.
        needed.Sort((a, b) => a.dist.CompareTo(b.dist));

        // Unload far tiles.
        var toUnload = new List<TileKey>();
        foreach (var kvp in activeTiles)
        {
            var key = kvp.Key;
            float dist = EstimateTileDistanceMeters(centerX, centerY, key.x, key.y);
            if (dist > unloadRadiusMeters)
                toUnload.Add(key);
        }
        for (int i = 0; i < toUnload.Count; i++)
            UnloadTile(toUnload[i]);

        // Load needed tiles (up to concurrency).
        int capacity = Mathf.Max(0, maxConcurrentDownloads - loadingTiles.Count);
        int started = 0;
        for (int i = 0; i < needed.Count && started < capacity; i++)
        {
            var key = needed[i].key;
            if (activeTiles.ContainsKey(key) || loadingTiles.Contains(key))
                continue;
            StartCoroutine(LoadTileRoutine(key, centerX, centerY));
            started++;
        }

        if (loadingUI != null)
            loadingUI.SetLoading(loadingTiles.Count > 0);
    }

    IEnumerator LoadTileRoutine(TileKey key, int centerX, int centerY)
    {
        loadingTiles.Add(key);

        Texture2D tex = null;
        if (cache != null)
            yield return cache.GetTileTexture(mapboxToken, mapStyle, key.zoom, key.x, key.y, t => tex = t);
        else
            yield return DownloadDirect(key, t => tex = t);

        if (tex != null && !activeTiles.ContainsKey(key))
        {
            int offsetX = key.x - centerX;
            int offsetY = key.y - centerY;
            GameObject tile = CreateTileQuad(tex, offsetX, offsetY, key);
            activeTiles[key] = tile;
        }

        loadingTiles.Remove(key);
        if (loadingUI != null)
            loadingUI.SetLoading(loadingTiles.Count > 0);
    }

    IEnumerator DownloadDirect(TileKey key, System.Action<Texture2D> onDone)
    {
        string url = $"https://api.mapbox.com/styles/v1/{mapStyle}/tiles/512/{key.zoom}/{key.x}/{key.y}@2x?access_token={mapboxToken}";
        using (var request = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();
            if (request.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                onDone?.Invoke(null);
                yield break;
            }
            onDone?.Invoke(UnityEngine.Networking.DownloadHandlerTexture.GetContent(request));
        }
    }

    GameObject CreateTileQuad(Texture2D tex, int offsetX, int offsetY, TileKey key)
    {
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.name = $"StreamTile_{key.zoom}_{key.x}_{key.y}";
        quad.transform.SetParent(transform, false);
        quad.transform.position = new Vector3(offsetX * tileWorldSize, 0.01f, -offsetY * tileWorldSize);
        quad.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        quad.transform.localScale = new Vector3(tileWorldSize, tileWorldSize, 1f);

        var renderer = quad.GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        renderer.material.mainTexture = tex;

        if (!createMeshCollider)
            Destroy(quad.GetComponent<Collider>());

        if (tileLayer > 0)
            quad.layer = tileLayer;

        return quad;
    }

    void UnloadTile(TileKey key)
    {
        if (activeTiles.TryGetValue(key, out var go) && go != null)
            Destroy(go);
        activeTiles.Remove(key);
        loadingTiles.Remove(key);
    }

    float EstimateTileDistanceMeters(int centerX, int centerY, int tileX, int tileY)
    {
        float dx = tileX - centerX;
        float dy = tileY - centerY;
        return Mathf.Sqrt(dx * dx + dy * dy) * tileWorldSize;
    }

    struct TileKey
    {
        public readonly int zoom;
        public readonly int x;
        public readonly int y;

        public TileKey(int zoom, int x, int y)
        {
            this.zoom = zoom;
            this.x = x;
            this.y = y;
        }
    }
}

