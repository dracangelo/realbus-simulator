using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TileStreamManager : MonoBehaviour
{
    enum TileLodTier
    {
        Tier1,
        Tier2,
        Tier3
    }

    sealed class TileRuntime
    {
        public TileKey key;
        public GameObject root;
        public MeshFilter meshFilter;
        public MeshRenderer meshRenderer;
        public MeshCollider meshCollider;
        public Vector3 centerPosition;
        public Vector3 scale;
        public Texture2D sourceTexture;
        public Color32[] sourcePixels;
        public int sourceWidth;
        public int sourceHeight;
        public TileLodTier currentLod = TileLodTier.Tier3;
        public TileLodTier requestedLod = TileLodTier.Tier3;
        public int buildVersion = 0;
        public bool isBuilding = false;
    }

    struct MeshBuildData
    {
        public Vector3[] vertices;
        public int[] triangles;
        public Vector2[] uvs;
    }

    struct TileBuildResult
    {
        public TileKey key;
        public int buildVersion;
        public TileLodTier lod;
        public MeshBuildData mesh;
        public Color32[] texturePixels;
        public int textureWidth;
        public int textureHeight;
        public bool colliderEnabled;
    }

    [Header("Mapbox")]
    public string mapboxToken = "";
    public string mapStyle = "mapbox/satellite-streets-v12";
    public int zoomLevel = 15;

    [Header("Streaming Radii")]
    public float loadRadiusMeters = 2000f;
    public float unloadRadiusMeters = 3000f;

    [Header("LOD Thresholds")]
    public float tier1DistanceMeters = 200f;
    public float tier2DistanceMeters = 500f;
    public float tier3DistanceMeters = 2000f;

    [Header("Tile Settings")]
    public float tileWorldSize = 200f;
    public int tileLayer = 0;
    public bool createMeshCollider = true;
    public float tileSurfaceY = 0.01f;

    [Header("LOD Geometry")]
    public int tier1GridSegments = 10;
    public int tier2GridSegments = 6;

    [Header("Performance")]
    public int maxConcurrentDownloads = 6;
    public float refreshEverySeconds = 0.5f;
    public int httpTimeoutSeconds = 12;
    public int maxConcurrentLodBuilds = 2;
    public int maxLodAppliesPerFrame = 2;

    [Header("References")]
    public Transform busTransform;
    public CoordinateConverter converter;
    public OfflineCacheManager cache;
    public TileStreamingLoadingUI loadingUI;

    readonly Dictionary<TileKey, TileRuntime> activeTiles = new Dictionary<TileKey, TileRuntime>();
    readonly HashSet<TileKey> loadingTiles = new HashSet<TileKey>();
    readonly ConcurrentQueue<TileBuildResult> completedBuilds = new ConcurrentQueue<TileBuildResult>();

    int activeBuildJobs;
    float t;

    void Start()
    {
        if (converter == null)
            converter = CoordinateConverter.Instance != null ? CoordinateConverter.Instance : FindFirstObjectByType<CoordinateConverter>();
        if (cache == null)
            cache = OfflineCacheManager.Instance != null ? OfflineCacheManager.Instance : FindFirstObjectByType<OfflineCacheManager>();
        if (busTransform == null)
        {
            var bus = FindFirstObjectByType<BusController>();
            if (bus != null)
                busTransform = bus.transform;
        }
    }

    void Update()
    {
        ApplyCompletedLodBuilds();

        if (busTransform == null || converter == null)
            return;

        t += Time.deltaTime;
        if (t < refreshEverySeconds)
            return;
        t = 0f;

        StreamTiles();
        RefreshTileLods();
    }

    void OnDestroy()
    {
        var keys = new List<TileKey>(activeTiles.Keys);
        for (int i = 0; i < keys.Count; i++)
            UnloadTile(keys[i]);
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
                if (worldDist > loadRadiusMeters)
                    continue;

                needed.Add((new TileKey(zoomLevel, centerX + dx, centerY + dy), worldDist));
            }
        }

        needed.Sort((a, b) => a.dist.CompareTo(b.dist));

        var toUnload = new List<TileKey>();
        foreach (var kvp in activeTiles)
        {
            float dist = EstimateTileDistanceMeters(centerX, centerY, kvp.Key.x, kvp.Key.y);
            if (dist > unloadRadiusMeters)
                toUnload.Add(kvp.Key);
        }

        for (int i = 0; i < toUnload.Count; i++)
            UnloadTile(toUnload[i]);

        int capacity = Mathf.Max(0, maxConcurrentDownloads - loadingTiles.Count);
        int started = 0;
        for (int i = 0; i < needed.Count && started < capacity; i++)
        {
            var key = needed[i].key;
            if (activeTiles.ContainsKey(key) || loadingTiles.Contains(key))
                continue;

            StartCoroutine(LoadTileRoutine(key));
            started++;
        }

        if (loadingUI != null)
            loadingUI.SetLoading(loadingTiles.Count > 0 || activeBuildJobs > 0);
    }

    void RefreshTileLods()
    {
        foreach (var kvp in activeTiles)
        {
            var runtime = kvp.Value;
            if (runtime == null || runtime.root == null)
                continue;

            float distance = Vector3.Distance(busTransform.position, runtime.centerPosition);
            TileLodTier desiredLod = ResolveLod(distance);
            if (desiredLod != runtime.currentLod && desiredLod != runtime.requestedLod)
                QueueLodBuild(runtime, desiredLod);
        }
    }

    void QueueLodBuild(TileRuntime runtime, TileLodTier lod)
    {
        if (runtime == null || runtime.sourcePixels == null)
            return;
        if (runtime.isBuilding)
            return;
        if (activeBuildJobs >= Mathf.Max(1, maxConcurrentLodBuilds))
            return;

        runtime.isBuilding = true;
        runtime.requestedLod = lod;
        runtime.buildVersion++;
        int version = runtime.buildVersion;
        Interlocked.Increment(ref activeBuildJobs);

        Task.Run(() =>
        {
            var result = new TileBuildResult
            {
                key = runtime.key,
                buildVersion = version,
                lod = lod,
                mesh = BuildMeshData(GetSegmentsForLod(lod)),
                texturePixels = BuildTexturePixels(runtime.sourcePixels, runtime.sourceWidth, runtime.sourceHeight, lod, out int texWidth, out int texHeight),
                textureWidth = texWidth,
                textureHeight = texHeight,
                colliderEnabled = createMeshCollider && lod == TileLodTier.Tier1
            };

            completedBuilds.Enqueue(result);
        });
    }

    void ApplyCompletedLodBuilds()
    {
        int applied = 0;
        while (applied < Mathf.Max(1, maxLodAppliesPerFrame) && completedBuilds.TryDequeue(out TileBuildResult result))
        {
            applied++;
            Interlocked.Decrement(ref activeBuildJobs);

            if (!activeTiles.TryGetValue(result.key, out TileRuntime runtime) || runtime == null || runtime.root == null)
                continue;
            if (result.buildVersion != runtime.buildVersion)
                continue;

            ApplyLodResult(runtime, result);
            runtime.isBuilding = false;
            runtime.currentLod = result.lod;
        }

        if (loadingUI != null)
            loadingUI.SetLoading(loadingTiles.Count > 0 || activeBuildJobs > 0);
    }

    void ApplyLodResult(TileRuntime runtime, TileBuildResult result)
    {
        var previousMesh = runtime.meshFilter.sharedMesh;
        var previousMaterial = runtime.meshRenderer.sharedMaterial;
        var previousTexture = previousMaterial != null ? previousMaterial.mainTexture as Texture2D : null;

        var mesh = new Mesh();
        mesh.name = $"TileMesh_{runtime.key.zoom}_{runtime.key.x}_{runtime.key.y}_{result.lod}";
        mesh.vertices = result.mesh.vertices;
        mesh.triangles = result.mesh.triangles;
        mesh.uv = result.mesh.uvs;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        runtime.meshFilter.sharedMesh = mesh;

        if (runtime.meshCollider == null && createMeshCollider)
            runtime.meshCollider = runtime.root.AddComponent<MeshCollider>();

        if (runtime.meshCollider != null)
        {
            runtime.meshCollider.enabled = result.colliderEnabled;
            runtime.meshCollider.sharedMesh = result.colliderEnabled ? mesh : null;
        }

        Texture2D texture = new Texture2D(
            result.textureWidth,
            result.textureHeight,
            result.lod == TileLodTier.Tier1 ? TextureFormat.RGBA32 : TextureFormat.RGB24,
            false);
        texture.SetPixels32(result.texturePixels);
        texture.Apply(false, false);
        ApplyTextureSettings(texture, result.lod);

        Material tileMaterial = previousMaterial;
        if (tileMaterial == null)
            tileMaterial = new Material(ResolveTileShader());

        tileMaterial.mainTexture = texture;
        tileMaterial.color = Color.white;
        runtime.meshRenderer.sharedMaterial = tileMaterial;
        runtime.meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        runtime.meshRenderer.receiveShadows = false;

        runtime.root.transform.position = runtime.centerPosition;
        runtime.root.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        runtime.root.transform.localScale = runtime.scale;

        if (previousMesh != null)
            Destroy(previousMesh);
        if (previousTexture != null && previousTexture != runtime.sourceTexture)
            Destroy(previousTexture);

        runtime.requestedLod = result.lod;
    }

    IEnumerator LoadTileRoutine(TileKey key)
    {
        loadingTiles.Add(key);

        Texture2D tex = null;
        if (cache != null)
            yield return cache.GetTileTexture(mapboxToken, mapStyle, key.zoom, key.x, key.y, t2 => tex = t2);
        else
            yield return DownloadDirect(key, t2 => tex = t2);

        if (tex != null && !activeTiles.ContainsKey(key))
            CreateTileRuntime(tex, key);

        loadingTiles.Remove(key);
        if (loadingUI != null)
            loadingUI.SetLoading(loadingTiles.Count > 0 || activeBuildJobs > 0);
    }

    IEnumerator DownloadDirect(TileKey key, System.Action<Texture2D> onDone)
    {
        string url = $"https://api.mapbox.com/styles/v1/{mapStyle}/tiles/512/{key.zoom}/{key.x}/{key.y}@2x?access_token={mapboxToken}";
        using (var request = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url))
        {
            request.timeout = Mathf.Max(1, httpTimeoutSeconds);
            yield return request.SendWebRequest();
            if (request.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                onDone?.Invoke(null);
                yield break;
            }

            onDone?.Invoke(UnityEngine.Networking.DownloadHandlerTexture.GetContent(request));
        }
    }

    void CreateTileRuntime(Texture2D tex, TileKey key)
    {
        Texture2D readable = EnsureReadableTexture(tex);
        Color32[] pixels = readable.GetPixels32();

        double centerLon = MapTileLoader.TileXToLon(key.x + 0.5, key.zoom);
        double centerLat = MapTileLoader.TileYToLat(key.y + 0.5, key.zoom);
        double leftLon = MapTileLoader.TileXToLon(key.x, key.zoom);
        double rightLon = MapTileLoader.TileXToLon(key.x + 1.0, key.zoom);
        double topLat = MapTileLoader.TileYToLat(key.y, key.zoom);
        double bottomLat = MapTileLoader.TileYToLat(key.y + 1.0, key.zoom);

        Vector3 centerPos = converter.GeoToWorldPosition(centerLat, centerLon);
        centerPos.y = tileSurfaceY;

        Vector3 leftPos = converter.GeoToWorldPosition(centerLat, leftLon);
        Vector3 rightPos = converter.GeoToWorldPosition(centerLat, rightLon);
        Vector3 topPos = converter.GeoToWorldPosition(topLat, centerLon);
        Vector3 bottomPos = converter.GeoToWorldPosition(bottomLat, centerLon);

        float width = Vector3.Distance(leftPos, rightPos);
        float height = Vector3.Distance(topPos, bottomPos);

        GameObject tileRoot = new GameObject($"StreamTile_{key.zoom}_{key.x}_{key.y}");
        tileRoot.transform.SetParent(transform, false);
        tileRoot.layer = tileLayer > 0 ? tileLayer : tileRoot.layer;

        var runtime = new TileRuntime
        {
            key = key,
            root = tileRoot,
            meshFilter = tileRoot.AddComponent<MeshFilter>(),
            meshRenderer = tileRoot.AddComponent<MeshRenderer>(),
            centerPosition = centerPos,
            scale = new Vector3(width, 1f, height),
            sourceTexture = readable,
            sourcePixels = pixels,
            sourceWidth = readable.width,
            sourceHeight = readable.height,
            currentLod = TileLodTier.Tier3,
            requestedLod = TileLodTier.Tier3
        };

        if (createMeshCollider)
            runtime.meshCollider = tileRoot.AddComponent<MeshCollider>();

        activeTiles[key] = runtime;
        QueueLodBuild(runtime, ResolveLod(Vector3.Distance(busTransform.position, centerPos)));
    }

    Texture2D EnsureReadableTexture(Texture2D source)
    {
        if (source == null)
            return null;

        var copy = new Texture2D(source.width, source.height, TextureFormat.RGBA32, false);
        copy.SetPixels32(source.GetPixels32());
        copy.Apply(false, false);
        return copy;
    }

    void ApplyTextureSettings(Texture2D tex, TileLodTier lod)
    {
        if (tex == null)
            return;

        tex.wrapMode = TextureWrapMode.Clamp;
        tex.anisoLevel = lod == TileLodTier.Tier1 ? 8 : 1;
        tex.filterMode = lod == TileLodTier.Tier3 ? FilterMode.Bilinear : FilterMode.Trilinear;
        tex.mipMapBias = lod == TileLodTier.Tier1 ? -0.5f : 0.1f;
    }

    Shader ResolveTileShader()
    {
        Shader shader = Shader.Find("Universal Render Pipeline/Unlit");
        if (shader == null) shader = Shader.Find("Unlit/Texture");
        if (shader == null) shader = Shader.Find("Standard");
        return shader;
    }

    TileLodTier ResolveLod(float distanceMeters)
    {
        if (distanceMeters <= tier1DistanceMeters)
            return TileLodTier.Tier1;
        if (distanceMeters <= tier2DistanceMeters)
            return TileLodTier.Tier2;
        return TileLodTier.Tier3;
    }

    int GetSegmentsForLod(TileLodTier lod)
    {
        switch (lod)
        {
            case TileLodTier.Tier1: return Mathf.Max(1, tier1GridSegments);
            case TileLodTier.Tier2: return Mathf.Max(1, tier2GridSegments);
            default: return 1;
        }
    }

    static MeshBuildData BuildMeshData(int segments)
    {
        segments = Mathf.Max(1, segments);
        int vertCountX = segments + 1;
        int vertCountZ = segments + 1;

        Vector3[] vertices = new Vector3[vertCountX * vertCountZ];
        Vector2[] uvs = new Vector2[vertices.Length];
        int[] triangles = new int[segments * segments * 6];

        int v = 0;
        for (int z = 0; z <= segments; z++)
        {
            float vz = (z / (float)segments) - 0.5f;
            for (int x = 0; x <= segments; x++)
            {
                float vx = (x / (float)segments) - 0.5f;
                vertices[v] = new Vector3(vx, 0f, vz);
                uvs[v] = new Vector2(x / (float)segments, 1f - (z / (float)segments));
                v++;
            }
        }

        int t = 0;
        for (int z = 0; z < segments; z++)
        {
            for (int x = 0; x < segments; x++)
            {
                int i = z * (segments + 1) + x;
                triangles[t++] = i;
                triangles[t++] = i + segments + 1;
                triangles[t++] = i + 1;
                triangles[t++] = i + 1;
                triangles[t++] = i + segments + 1;
                triangles[t++] = i + segments + 2;
            }
        }

        return new MeshBuildData
        {
            vertices = vertices,
            triangles = triangles,
            uvs = uvs
        };
    }

    static Color32[] BuildTexturePixels(Color32[] sourcePixels, int sourceWidth, int sourceHeight, TileLodTier lod, out int width, out int height)
    {
        float scale = lod == TileLodTier.Tier1 ? 1f : lod == TileLodTier.Tier2 ? 0.5f : 0.25f;
        width = Mathf.Max(32, Mathf.RoundToInt(sourceWidth * scale));
        height = Mathf.Max(32, Mathf.RoundToInt(sourceHeight * scale));

        if (width == sourceWidth && height == sourceHeight)
        {
            Color32[] full = new Color32[sourcePixels.Length];
            sourcePixels.CopyTo(full, 0);
            return full;
        }

        Color32[] resized = new Color32[width * height];
        float xRatio = (sourceWidth - 1f) / Mathf.Max(1f, width - 1f);
        float yRatio = (sourceHeight - 1f) / Mathf.Max(1f, height - 1f);

        for (int y = 0; y < height; y++)
        {
            int sampleY = Mathf.RoundToInt(y * yRatio);
            for (int x = 0; x < width; x++)
            {
                int sampleX = Mathf.RoundToInt(x * xRatio);
                resized[(y * width) + x] = sourcePixels[(sampleY * sourceWidth) + sampleX];
            }
        }

        return resized;
    }

    void UnloadTile(TileKey key)
    {
        if (activeTiles.TryGetValue(key, out TileRuntime runtime) && runtime != null)
        {
            ReleaseRuntimeAssets(runtime);
            if (runtime.sourceTexture != null)
                Destroy(runtime.sourceTexture);
            if (runtime.root != null)
                Destroy(runtime.root);
        }
        activeTiles.Remove(key);
        loadingTiles.Remove(key);
    }

    void ReleaseRuntimeAssets(TileRuntime runtime)
    {
        if (runtime == null)
            return;

        if (runtime.meshCollider != null && runtime.meshCollider.sharedMesh != null)
            runtime.meshCollider.sharedMesh = null;

        if (runtime.meshFilter != null && runtime.meshFilter.sharedMesh != null)
        {
            Destroy(runtime.meshFilter.sharedMesh);
            runtime.meshFilter.sharedMesh = null;
        }

        if (runtime.meshRenderer != null && runtime.meshRenderer.sharedMaterial != null)
        {
            var material = runtime.meshRenderer.sharedMaterial;
            var texture = material.mainTexture as Texture2D;
            runtime.meshRenderer.sharedMaterial = null;

            if (texture != null && texture != runtime.sourceTexture)
                Destroy(texture);

            Destroy(material);
        }
    }

    float EstimateTileDistanceMeters(int centerX, int centerY, int tileX, int tileY)
    {
        float dx = tileX - centerX;
        float dy = tileY - centerY;
        return Mathf.Sqrt(dx * dx + dy * dy) * tileWorldSize;
    }

    readonly struct TileKey
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
