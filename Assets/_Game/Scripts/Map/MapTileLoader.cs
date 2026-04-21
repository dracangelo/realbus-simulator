using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Downloads and displays Mapbox raster tiles as flat mesh quads in the scene.
/// Reads centre coordinates from CityManager.activeCity.
/// </summary>
public class MapTileLoader : MonoBehaviour
{
    [Header("Mapbox Settings")]
    public string mapboxToken = "";
    public string mapStyle = "mapbox/satellite-streets-v12";
    public bool preferLabelFreeSatellite = true;

    [Header("Map Centre — set automatically from CityDefinition")]
    public double centreLat = -1.2864;
    public double centreLon = 36.8172;
    public int zoomLevel = 15;

    [Header("Tile Grid")]
    public int tilesX = 5;
    public int tilesY = 5;
    public float tileWorldSize = 200f;
    public float tileSurfaceY = 0.15f;
    public bool keepTileColliders = true;

    [Header("Tile Layer")]
    public int tileLayer = 0;

    [Header("Startup")]
    public bool loadOnAwake = true;
    public bool preferOfflineCache = true;
    public bool hideFallbackPlaneWhenTilesLoad = true;
    public string fallbackPlaneObjectName = "Plane";
    public int httpTimeoutSeconds = 12;
    public int highPriorityRing = 1;
    public float deferredTileStartDelay = 0.03f;
    public int maxTileFailureLogs = 8;
    public bool haltDownloadsAfterAuthFailure = true;

    [Header("Tile Visual Quality")]
    public bool forceSharpTileFiltering = true;
    [Range(-2f, 2f)] public float tileMipMapBias = -0.75f;
    [Range(1, 16)] public int tileAnisoLevel = 16;

    private List<GameObject> activeTiles = new List<GameObject>();
    private OfflineCacheManager cacheManager;
    private int centerTileX, centerTileY;
    private int loadVersion;
    private bool initStarted;
    private bool loggedFirstTile;
    private bool centerTileLoaded;
    private int tileFailureLogCount;
    private bool loggedAuthFailureHint;
    private bool authFailureDetected;

    void Awake()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        cacheManager = OfflineCacheManager.Instance != null
            ? OfflineCacheManager.Instance
            : FindFirstObjectByType<OfflineCacheManager>();

        if (loadOnAwake)
            BeginInitialLoad();
    }

    void Start()
    {
        if (!loadOnAwake)
            BeginInitialLoad();
    }

    public void BeginInitialLoad()
    {
        if (initStarted)
            return;

        initStarted = true;
        StartCoroutine(InitWhenReady());
    }

    IEnumerator InitWhenReady()
    {
        while (CityManager.Instance == null || CityManager.Instance.activeCity == null)
            yield return null;
        
        var city = CityManager.Instance.activeCity;
        centreLat = city.centreLat;
        centreLon = city.centreLon;
        Debug.Log($"MapTileLoader: Loading map for {city.cityName}");

        if (preferLabelFreeSatellite && mapStyle == "mapbox/satellite-streets-v12")
            mapStyle = "mapbox/satellite-v9";
        
        mapboxToken = string.IsNullOrWhiteSpace(mapboxToken) ? "" : mapboxToken.Trim();
        if (string.IsNullOrEmpty(mapboxToken))
        {
            Debug.LogError("MapTileLoader: No Mapbox token set!");
            yield break;
        }

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        LoadMap(centreLat, centreLon, zoomLevel);
        StartCoroutine(WarnIfNoTilesLoaded(loadVersion));
    }

    public void LoadMap(double lat, double lon, int zoom)
    {
        ClearTiles();
        loadVersion++;
        centerTileLoaded = false;
        authFailureDetected = false;

        centreLat = lat;
        centreLon = lon;
        zoomLevel = zoom;

        centerTileX = LonToTileX(lon, zoom);
        centerTileY = LatToTileY(lat, zoom);

        int halfX = tilesX / 2;
        int halfY = tilesY / 2;
        int localVersion = loadVersion;

        var highPriority = new List<(int x, int y)>();
        var deferred = new List<(int x, int y, int ring)>();

        for (int x = -halfX; x <= halfX; x++)
        {
            for (int y = -halfY; y <= halfY; y++)
            {
                int ring = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                if (ring <= Mathf.Max(0, highPriorityRing))
                    highPriority.Add((x, y));
                else
                    deferred.Add((x, y, ring));
            }
        }

        highPriority.Sort((a, b) => (a.x * a.x + a.y * a.y).CompareTo(b.x * b.x + b.y * b.y));
        deferred.Sort((a, b) => a.ring != b.ring
            ? a.ring.CompareTo(b.ring)
            : (a.x * a.x + a.y * a.y).CompareTo(b.x * b.x + b.y * b.y));

        for (int i = 0; i < highPriority.Count; i++)
        {
            var item = highPriority[i];
            StartCoroutine(DownloadTile(centerTileX + item.x, centerTileY + item.y, item.x, item.y, zoom, localVersion));
        }

        for (int i = 0; i < deferred.Count; i++)
        {
            var item = deferred[i];
            float delay = Mathf.Max(0f, deferredTileStartDelay) * (i + 1);
            StartCoroutine(DownloadTileDelayed(centerTileX + item.x, centerTileY + item.y, item.x, item.y, zoom, localVersion, delay));
        }
    }

    IEnumerator DownloadTileDelayed(int tileX, int tileY, int offsetX, int offsetY, int zoom, int version, float delay)
    {
        if (delay > 0f)
            yield return new WaitForSeconds(delay);

        if (version != loadVersion)
            yield break;

        yield return DownloadTile(tileX, tileY, offsetX, offsetY, zoom, version);
    }

    IEnumerator DownloadTile(int tileX, int tileY, int offsetX, int offsetY, int zoom, int version)
    {
        if (haltDownloadsAfterAuthFailure && authFailureDetected)
            yield break;

        Texture2D tex = null;
        if (preferOfflineCache && cacheManager != null)
        {
            yield return cacheManager.GetTileTexture(mapboxToken, mapStyle, zoom, tileX, tileY, t => tex = t);
        }
        else
        {
            string url = $"https://api.mapbox.com/styles/v1/{mapStyle}/tiles/512/" +
                         $"{zoom}/{tileX}/{tileY}@2x?access_token={mapboxToken}";

            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                request.timeout = Mathf.Max(1, httpTimeoutSeconds);
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                    tex = DownloadHandlerTexture.GetContent(request);
                else
                    LogTileFailure(tileX, tileY, request);
            }
        }

        if (version != loadVersion || tex == null)
            yield break;

        CreateTileQuad(tex, offsetX, offsetY, tileX, tileY);
    }

    void CreateTileQuad(Texture2D tex, int offsetX, int offsetY, int tileX, int tileY)
    {
        ApplyTileTextureSettings(tex);

        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.name = $"Tile_{tileX}_{tileY}";
        quad.transform.SetParent(transform, false);

        double centerLon = TileXToLon(tileX + 0.5, zoomLevel);
        double centerLat = TileYToLat(tileY + 0.5, zoomLevel);
        
        double leftLon = TileXToLon(tileX, zoomLevel);
        double rightLon = TileXToLon(tileX + 1.0, zoomLevel);
        double topLat = TileYToLat(tileY, zoomLevel);
        double bottomLat = TileYToLat(tileY + 1.0, zoomLevel);

        Vector3 centerPos = GpsToWorldPosition(centerLat, centerLon);
        centerPos.y = tileSurfaceY;

        Vector3 leftPos = GpsToWorldPosition(centerLat, leftLon);
        Vector3 rightPos = GpsToWorldPosition(centerLat, rightLon);
        Vector3 topPos = GpsToWorldPosition(topLat, centerLon);
        Vector3 bottomPos = GpsToWorldPosition(bottomLat, centerLon);

        float width = Vector3.Distance(leftPos, rightPos);
        float height = Vector3.Distance(topPos, bottomPos);

        quad.transform.localPosition = centerPos;
        quad.transform.localRotation = Quaternion.Euler(90f, 0f, 0f); // Changed from -90f to 90f to make it face UP
        quad.transform.localScale = new Vector3(width, height, 1f);

        var renderer = quad.GetComponent<Renderer>();

        renderer.material = new Material(ResolveTileShader());
        renderer.material.mainTexture = tex;
        renderer.material.color = Color.white;
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = false;

        if (renderer.material.HasProperty("_Cull"))
            renderer.material.SetFloat("_Cull", (float)UnityEngine.Rendering.CullMode.Off);
        if (renderer.material.HasProperty("_Surface"))
            renderer.material.SetFloat("_Surface", 0f);
        if (renderer.material.HasProperty("_ZWrite"))
            renderer.material.SetFloat("_ZWrite", 1f);

        if (!keepTileColliders)
            Destroy(quad.GetComponent<Collider>());
        if (tileLayer > 0) quad.layer = tileLayer;
        activeTiles.Add(quad);

        if (tileX == centerTileX && tileY == centerTileY)
        {
            centerTileLoaded = true;
            HideFallbackPlane();
            Debug.Log("MapTileLoader: Center tile loaded. Fallback plane hidden.");
        }

        if (!loggedFirstTile)
        {
            loggedFirstTile = true;
            Debug.Log($"MapTileLoader: First tile created ({tileX},{tileY}) at local {quad.transform.localPosition} world {quad.transform.position}.");
        }
    }

    void ApplyTileTextureSettings(Texture2D tex)
    {
        if (tex == null || !forceSharpTileFiltering)
            return;

        tex.wrapMode = TextureWrapMode.Clamp;
        tex.filterMode = FilterMode.Trilinear;
        tex.anisoLevel = Mathf.Clamp(tileAnisoLevel, 1, 16);
        tex.mipMapBias = tileMipMapBias;
    }

    Shader ResolveTileShader()
    {
        Shader shader = Shader.Find("Universal Render Pipeline/Unlit");
        if (shader == null) shader = Shader.Find("Unlit/Texture");
        if (shader == null) shader = Shader.Find("Standard");
        return shader;
    }

    void ClearTiles()
    {
        foreach (var tile in activeTiles)
            if (tile != null) Destroy(tile);
        activeTiles.Clear();
        loggedFirstTile = false;
        centerTileLoaded = false;
        tileFailureLogCount = 0;
        loggedAuthFailureHint = false;
    }

    IEnumerator WarnIfNoTilesLoaded(int version)
    {
        yield return new WaitForSeconds(5f);
        if (version != loadVersion)
            yield break;

        if (!centerTileLoaded)
            Debug.LogWarning("MapTileLoader: Center tile not loaded within 5 seconds. Keeping fallback plane active.");
    }

    void HideFallbackPlane()
    {
        if (!hideFallbackPlaneWhenTilesLoad)
            return;

        var plane = GameObject.Find(fallbackPlaneObjectName);
        if (plane == null)
            return;

        plane.SetActive(false);
    }

    void LogTileFailure(int tileX, int tileY, UnityWebRequest request)
    {
        if (request == null)
            return;

        long status = request.responseCode;
        string err = string.IsNullOrWhiteSpace(request.error) ? "unknown error" : request.error;
        string body = request.downloadHandler != null ? request.downloadHandler.text : "";
        if (body != null && body.Length > 160)
            body = body.Substring(0, 160);

        bool accessDenied = status == 401 || status == 403
            || err.IndexOf("access denied", System.StringComparison.OrdinalIgnoreCase) >= 0
            || (body != null && body.IndexOf("access denied", System.StringComparison.OrdinalIgnoreCase) >= 0)
            || (body != null && body.IndexOf("not authorized", System.StringComparison.OrdinalIgnoreCase) >= 0);

        if (accessDenied && !loggedAuthFailureHint)
        {
            loggedAuthFailureHint = true;
            Debug.LogError(
                $"MapTileLoader: Mapbox authorization failed (HTTP {status}). " +
                "Verify token is valid, has styles:read scope, style access, and no restrictive URL/domain rules for Unity requests.");
        }

        if (accessDenied && haltDownloadsAfterAuthFailure)
            authFailureDetected = true;

        if (accessDenied && loggedAuthFailureHint)
            return;

        if (tileFailureLogCount < Mathf.Max(1, maxTileFailureLogs))
        {
            tileFailureLogCount++;
            string detail = string.IsNullOrWhiteSpace(body) ? err : $"{err} | {body}";
            Debug.LogWarning($"MapTileLoader: Failed tile {tileX},{tileY} (HTTP {status}) — {detail}");
        }
    }

    public static int LonToTileX(double lon, int zoom)
        => (int)System.Math.Floor((lon + 180.0) / 360.0 * (1 << zoom));

    public static int LatToTileY(double lat, int zoom)
    {
        double latRad = lat * System.Math.PI / 180.0;
        return (int)System.Math.Floor(
            (1.0 - System.Math.Log(
                System.Math.Tan(latRad) + 1.0 / System.Math.Cos(latRad))
            / System.Math.PI) / 2.0 * (1 << zoom));
    }

    
    public static double TileXToLon(double x, int zoom)
    {
        return x / (double)(1 << zoom) * 360.0 - 180.0;
    }

    public static double TileYToLat(double y, int zoom)
    {
        double n = System.Math.PI - 2.0 * System.Math.PI * y / (double)(1 << zoom);
        return 180.0 / System.Math.PI * System.Math.Atan(0.5 * (System.Math.Exp(n) - System.Math.Exp(-n)));
    }

    public Vector3 GpsToWorldPosition(double lat, double lon)
    {
        double metersPerDegreeLat = 111320.0;
        double metersPerDegreeLon = 111320.0 *
            System.Math.Cos(centreLat * System.Math.PI / 180.0);

        float worldX = (float)((lon - centreLon) * metersPerDegreeLon);
        float worldZ = (float)((lat - centreLat) * metersPerDegreeLat);

        return new Vector3(worldX, 0f, worldZ);
    }

    public (double lat, double lon) WorldPositionToGps(Vector3 worldPos)
    {
        double metersPerDegreeLat = 111320.0;
        double metersPerDegreeLon = 111320.0 *
            System.Math.Cos(centreLat * System.Math.PI / 180.0);

        double lat = centreLat + (worldPos.z / metersPerDegreeLat);
        double lon = centreLon + (worldPos.x / metersPerDegreeLon);

        return (lat, lon);
    }
}
