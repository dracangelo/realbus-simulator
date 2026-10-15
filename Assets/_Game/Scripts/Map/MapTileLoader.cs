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
    public bool followPlayer = true;
    float nextFollowCheck;

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
    /// <summary>True once the map tile directly beneath the route start / city
    /// centre has been downloaded and displayed.</summary>
    public bool CenterTileLoaded => centerTileLoaded;
    private int tileFailureLogCount;
    private bool loggedAuthFailureHint;
    private bool authFailureDetected;
    private bool tokenResolved;
    private bool loggedMissingToken;

    /// <summary>
    /// The configuration this loader resolved and published. It is the authority for every
    /// request this component makes, so a value can no longer change between a request being
    /// sent and its failure being reported.
    /// </summary>
    MapboxMapSettings mapSettings;

    MapboxMapSettings Settings
    {
        get
        {
            if (mapSettings == null) ResolveTokenOnce();
            return mapSettings;
        }
    }

    public static MapTileLoader Instance { get; private set; }

    /// <summary>
    /// The tile cache, resolved on demand. It used to be resolved once in Awake, so a cache
    /// that appeared later - the offline-maps menu builds one for its download - stayed
    /// invisible for the rest of the session and every tile was fetched from Mapbox even with
    /// the whole city sitting on disk. Same lazy re-resolve the credential got.
    /// </summary>
    OfflineCacheManager Cache
    {
        get
        {
            if (cacheManager == null) cacheManager = OfflineCacheManager.EnsureInstance();
            return cacheManager;
        }
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        // Resolve the credential before any Start can reach LoadMap. Reading the
        // configuration only once an active city existed meant a route started before that
        // asked Mapbox for tiles with an empty access_token and collected 401s.
        ResolveTokenOnce();

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        cacheManager = OfflineCacheManager.EnsureInstance();

        if (loadOnAwake)
            BeginInitialLoad();
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
        ClearTiles();
    }

    void Start()
    {
        if (!loadOnAwake)
            BeginInitialLoad();
    }

    void Update()
    {
        if (!followPlayer || loadVersion == 0 || authFailureDetected || CoordinateConverter.Instance == null ||
            CoordinateConverter.Instance.mapOrigin == null || !CoordinateConverter.Instance.mapOrigin.IsValid() ||
            Time.unscaledTime < nextFollowCheck) return;
        nextFollowCheck = Time.unscaledTime + 2f;
        var bus = MissionManager.Instance != null ? MissionManager.Instance.busController : null;
        if (bus == null) return;
        var location = WorldPositionToGps(bus.transform.position);
        int x = LonToTileX(location.lon, zoomLevel);
        int y = LatToTileY(location.lat, zoomLevel);
        if (Mathf.Abs(x - centerTileX) > Mathf.Max(0, tilesX / 2 - 1) ||
            Mathf.Abs(y - centerTileY) > Mathf.Max(0, tilesY / 2 - 1))
            LoadMap(location.lat, location.lon, zoomLevel);
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
        // Awake may run before SceneBootstrap synchronizes the selected city.
        yield return null;
        while (CityManager.Instance == null || CityManager.Instance.activeCity == null)
            yield return null;
        
        var city = CityManager.Instance.activeCity;
        centreLat = city.centreLat;
        centreLon = city.centreLon;

        // Prioritize center tile directly under the route start / bus location
        if (MissionManager.Instance != null && MissionManager.Instance.currentRoute != null &&
            MissionManager.Instance.currentRoute.stops != null && MissionManager.Instance.currentRoute.stops.Length > 0)
        {
            centreLat = MissionManager.Instance.currentRoute.stops[0].latitude;
            centreLon = MissionManager.Instance.currentRoute.stops[0].longitude;
        }

        Debug.Log($"MapTileLoader: Loading map for {city.cityName} (focus: {centreLat:F5}, {centreLon:F5})");

        ResolveTokenOnce();
        if (!Settings.HasToken)
            yield break;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        LoadMap(centreLat, centreLon, zoomLevel);
        StartCoroutine(WarnIfNoTilesLoaded(loadVersion));
    }

    /// <summary>
    /// Resolves this loader's credential once, synchronously, from the inspector value or
    /// the shipped configuration resource. Every entry point funnels through here so the
    /// request can never be built before the token exists, whichever order the scene, the
    /// city selection and the route boot in.
    /// </summary>
    void ResolveTokenOnce()
    {
        if (tokenResolved)
            return;
        tokenResolved = true;

        // Resolve once, then publish: the loader owns the authored fields, every other tile
        // consumer reads the result, and the fields themselves are left as authored — the
        // label-free preference is part of the resolved style rather than a silent edit of the
        // value the scene was saved with.
        mapSettings = MapboxMapSettings.Resolve(mapboxToken, mapStyle, preferLabelFreeSatellite);
        MapboxMapSettings.Publish(mapSettings);

        if (!mapSettings.HasToken)
        {
            Debug.LogError("MapTileLoader: No Mapbox token set! Assign one in the inspector or add " +
                           "Assets/Resources/Mapbox/MapboxConfiguration.txt ({\"AccessToken\":\"pk.…\"}).");
            return;
        }

        // Record which configuration this run resolved and what its token claims, without
        // printing the credential. A platform build is otherwise impossible to reason about
        // from its log.
        Debug.Log($"MapTileLoader: token from {mapSettings.TokenSource}: " +
                  $"{MapboxTileRequest.DescribeToken(mapSettings.Token)}; every tile consumer now uses " +
                  $"style '{mapSettings.Style}'.");
        if (mapSettings.Token.StartsWith("sk.", System.StringComparison.Ordinal))
            Debug.LogError("MapTileLoader: a Mapbox SECRET (sk.) token is configured. Secret tokens must not " +
                           "ship in a client build; use a public (pk.) token.");
        if (!MapboxTileRequest.IsPublicToken(mapSettings.Token))
            Debug.LogWarning("MapTileLoader: the configured token is not a recognised public Mapbox token; " +
                             "tile requests will very likely be refused with HTTP 401.");
    }

    public void LoadMap(double lat, double lon, int zoom)
    {
        // A route or a city selection can ask for a map before this loader's own startup
        // sequence has seen a city, so the credential is resolved here too.
        ResolveTokenOnce();

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
        if (preferOfflineCache && Cache != null)
        {
            yield return Cache.GetTileTexture(zoom, tileX, tileY, t => tex = t);
        }
        else if (!Settings.HasToken)
        {
            // Nothing can be authorised without a credential, and sending the empty string is
            // worse than sending nothing: Mapbox answers 401 "Direct access not allowed",
            // which used to halt every later download as if the token were bad.
            WarnMissingTokenOnce();
            yield break;
        }
        else
        {
            string url = MapboxTileRequest.BuildTileUrl(Settings.Style, zoom, tileX, tileY, Settings.Token);

            // Buffer the response first, then decode it ourselves. UnityWebRequestTexture
            // can report a DataProcessingError with HTTP 200 for otherwise valid Mapbox
            // PNG/JPEG tiles on some Unity/Linux combinations.
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                request.timeout = Mathf.Max(1, httpTimeoutSeconds);
                request.SetRequestHeader("Accept", "image/png,image/jpeg;q=0.9,*/*;q=0.1");
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    byte[] bytes = request.downloadHandler != null ? request.downloadHandler.data : null;
                    tex = DecodeTile(bytes);
                    if (tex == null)
                        LogTileFailure(tileX, tileY, request, "Mapbox returned HTTP 200, but the response was not a supported PNG/JPEG tile");
                }
                else
                    LogTileFailure(tileX, tileY, request);
            }
        }

        if (version != loadVersion || tex == null)
        {
            if (tex != null) Destroy(tex);
            yield break;
        }

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

    Shader ResolveTileShader() => BuiltInShaders.UnlitTexture;

    void ClearTiles()
    {
        foreach (var tile in activeTiles)
            if (tile != null)
            {
                var renderer = tile.GetComponent<Renderer>();
                if (renderer != null && renderer.sharedMaterial != null)
                {
                    Destroy(renderer.sharedMaterial.mainTexture);
                    Destroy(renderer.sharedMaterial);
                }
                Destroy(tile);
            }
        activeTiles.Clear();
        loggedFirstTile = false;
        centerTileLoaded = false;
        tileFailureLogCount = 0;
        loggedAuthFailureHint = false;
    }


    void WarnMissingTokenOnce()
    {
        if (loggedMissingToken)
            return;

        loggedMissingToken = true;
        Debug.LogWarning(
            $"MapTileLoader: no Mapbox token was resolved ({Settings.TokenSource}), so no tile was requested. " +
            $"Add one to Assets/Resources/{MapboxTileRequest.ConfigResourcePath}.txt or to the loader's " +
            "mapboxToken field.");
    }

    /// <summary>
    /// True when a refusal is a genuine credential problem: Mapbox refused the request and a
    /// token travelled in it. A tokenless 401 means the request was built before the token
    /// was resolved — the loader's own bug, which must not stop the downloads that follow.
    /// </summary>
    public static bool CountsAsAuthFailure(long status, string tokenThatTravelled) =>
        (status == 401 || status == 403) && !string.IsNullOrEmpty(tokenThatTravelled);

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

    static Texture2D DecodeTile(byte[] bytes)
    {
        if (bytes == null || bytes.Length < 16)
            return null;

        var texture = new Texture2D(2, 2, TextureFormat.RGBA32, true);
        if (texture.LoadImage(bytes, false))
            return texture;

        Destroy(texture);
        return null;
    }

    void LogTileFailure(int tileX, int tileY, UnityWebRequest request, string overrideError = null)
    {
        if (request == null)
            return;

        long status = request.responseCode;
        string err = !string.IsNullOrWhiteSpace(overrideError)
            ? overrideError
            : string.IsNullOrWhiteSpace(request.error) ? "unknown error" : request.error;
        string contentType = request.GetResponseHeader("Content-Type") ?? "";
        bool textualResponse = contentType.IndexOf("json", System.StringComparison.OrdinalIgnoreCase) >= 0
            || contentType.IndexOf("text", System.StringComparison.OrdinalIgnoreCase) >= 0;
        string body = textualResponse && request.downloadHandler != null ? request.downloadHandler.text : "";
        if (body != null && body.Length > 160)
            body = body.Substring(0, 160);

        // Report what this request carried, not what the fields hold now: the token is
        // resolved while requests are already in flight, so reading the fields here produced
        // one line that blamed an empty URL and described a live token at the same time.
        string sentStyle = Settings.Style;
        string sentToken = Settings.Token;
        if (MapboxTileRequest.TryReadRequest(request.url, out string requestStyle, out string requestToken))
        {
            sentStyle = requestStyle;
            sentToken = requestToken;
        }

        // Never classify a successful HTTP 200 image/decode problem as an auth
        // failure. Auth responses from the Styles API use 401/403.
        bool accessDenied = status == 401 || status == 403;

        if (accessDenied && !loggedAuthFailureHint)
        {
            loggedAuthFailureHint = true;
            Debug.LogError(
                $"MapTileLoader: Mapbox refused the tile request (HTTP {status}) for style '{sentStyle}': " +
                $"{MapboxTileRequest.ClassifyAuthFailure(status, body)}. " +
                $"The request carried {MapboxTileRequest.DescribeToken(sentToken)}. " +
                "Run RealBus/Diagnostics/Verify Mapbox Token to test the resolved token outside the game.");
        }

        if (CountsAsAuthFailure(status, sentToken) && haltDownloadsAfterAuthFailure)
            authFailureDetected = true;

        if (accessDenied && loggedAuthFailureHint)
            return;

        if (tileFailureLogCount < Mathf.Max(1, maxTileFailureLogs))
        {
            tileFailureLogCount++;
            string detail = string.IsNullOrWhiteSpace(body) ? err : $"{err} | {body}";
            if (!string.IsNullOrWhiteSpace(contentType)) detail += $" | Content-Type: {contentType}";
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
        // Changing the download centre must not move the world's coordinate origin.
        var origin = CoordinateConverter.Instance != null ? CoordinateConverter.Instance.mapOrigin : null;
        if (origin != null && origin.IsValid())
            return CoordinateConverter.LocalOriginGeoToWorld(lat, lon, origin.originLat, origin.originLon);
        double metersPerDegreeLat = 111320.0;
        double metersPerDegreeLon = 111320.0 *
            System.Math.Cos(centreLat * System.Math.PI / 180.0);

        float worldX = (float)((lon - centreLon) * metersPerDegreeLon);
        float worldZ = (float)((lat - centreLat) * metersPerDegreeLat);

        return new Vector3(worldX, 0f, worldZ);
    }

    public (double lat, double lon) WorldPositionToGps(Vector3 worldPos)
    {
        var origin = CoordinateConverter.Instance != null ? CoordinateConverter.Instance.mapOrigin : null;
        if (origin != null && origin.IsValid())
            return CoordinateConverter.LocalOriginWorldToGeo(worldPos, origin.originLat, origin.originLon);
        double metersPerDegreeLat = 111320.0;
        double metersPerDegreeLon = 111320.0 *
            System.Math.Cos(centreLat * System.Math.PI / 180.0);

        double lat = centreLat + (worldPos.z / metersPerDegreeLat);
        double lon = centreLon + (worldPos.x / metersPerDegreeLon);

        return (lat, lon);
    }
}
