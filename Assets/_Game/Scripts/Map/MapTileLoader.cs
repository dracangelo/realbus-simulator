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

    [Header("Download Quality")]
    [Tooltip("1 = 512 px tiles, 2 = 1024 px @2x. 0 = choose from platform and quality tier.")]
    [Range(0, 2)] public int tilePixelRatio = 0;
    [Tooltip("Attempts for the direct (cache-disabled) path. The cache retries its own downloads.")]
    [Range(1, 6)] public int maxDownloadAttempts = MapDownloadPolicy.DefaultMaxAttempts;

    [Header("Tile Visual Quality")]
    public bool forceSharpTileFiltering = true;
    [Tooltip("Negative values sharpen the ground but make distant rows crawl. Kept near zero so the imagery stays stable while the bus moves.")]
    [Range(-2f, 2f)] public float tileMipMapBias = 0f;
    [Range(1, 16)] public int tileAnisoLevel = 16;

    // Tiles are kept, not rebuilt: a follow update used to destroy all 25 quads and
    // download them again every time the bus crossed half a tile. That made the
    // ground blink out and back, and it spent a frame or two uploading 25 textures
    // while the bus was moving. The window now slides: tiles that stay in range are
    // left untouched, only the newly exposed ring is fetched, and only tiles that
    // left the window are destroyed.
    private readonly Dictionary<Vector2Int, GameObject> activeTiles = new Dictionary<Vector2Int, GameObject>();
    private readonly HashSet<Vector2Int> requestedTiles = new HashSet<Vector2Int>();
    private OfflineCacheManager cacheManager;
    private int centerTileX, centerTileY;
    private int activeZoom = -1;
    private int loadVersion;
    private bool initStarted;
    private bool loggedFirstTile;
    private bool centerTileLoaded;
    private int activePixelRatio = 2;
    private readonly HashSet<Vector2Int> failedTiles = new HashSet<Vector2Int>();

    /// <summary>True once the map tile directly beneath the route start / city
    /// centre has been downloaded and displayed.</summary>
    public bool CenterTileLoaded => centerTileLoaded;
    public int LoadedTileCount => activeTiles.Count;
    public int RequestedTileCount => requestedTiles.Count;

    /// <summary>True when the tile under the bus could not be loaded after every retry.</summary>
    public bool MapFailed { get; private set; }
    /// <summary>Why the last tile failure happened, for the loading card and diagnostics.</summary>
    public string LastMapError { get; private set; }
    /// <summary>Tiles that failed and would be retried by <see cref="RetryFailedTiles"/>.</summary>
    public int FailedTileCount => failedTiles.Count;
    /// <summary>The raster ratio this run is actually using (1 or 2).</summary>
    public int TilePixelRatio => activePixelRatio;
    /// <summary>Raised when the map's failure or recovery state changes.</summary>
    public event System.Action MapStateChanged;

    /// <summary>
    /// True once the high-priority tile ring around the centre is on screen.
    /// The loading overlay uses this instead of the single centre tile: the
    /// player steps out of a bus that fills the middle of the screen, so a
    /// one-tile world still showed bare fallback plane down both sides of the road.
    /// </summary>
    public bool NearTilesLoaded
    {
        get
        {
            if (!centerTileLoaded) return false;
            // A 5x5 window is already the mobile memory budget. Waiting for all
            // 25 tiles before handing control to the player is unnecessary: the
            // centre 3x3 covers the street the bus starts on, and the outer ring
            // streams behind the card.
            int ring = Mathf.Min(tilesX, tilesY) <= 5 ? 1 : 2;
            for (int dx = -ring; dx <= ring; dx++)
            for (int dy = -ring; dy <= ring; dy++)
                if (!activeTiles.ContainsKey(new Vector2Int(centerTileX + dx, centerTileY + dy)))
                    return false;
            return true;
        }
    }

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
        {
            // A missing credential is a settled failure, not a slow download. The
            // loading screen must be able to say so immediately instead of waiting
            // out its whole ceiling on a request that will never be sent.
            LastMapError = MapDownloadPolicy.Describe(MapTileFailureKind.NoToken) +
                           ". Add one in Assets/Resources/Mapbox/MapboxConfiguration.txt.";
            MapFailed = true;
            MapStateChanged?.Invoke();
            yield break;
        }

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

        loadVersion++;
        // In-flight requests belong to the previous window. They abandon themselves on
        // the version check, so their coordinates are released rather than leaked.
        requestedTiles.Clear();
        authFailureDetected = false;
        failedTiles.Clear();
        MapFailed = false;
        LastMapError = null;

        int desiredPixelRatio = MapDownloadPolicy.ResolvePixelRatio(
            tilePixelRatio, Application.isMobilePlatform, QualitySettings.GetQualityLevel());

        // A tile coordinate means nothing without its zoom: z15/100,200 and
        // z17/100,200 are different pieces of ground. Changing zoom (city switch,
        // quality change) has to start from a clean window or the old imagery would
        // be considered already loaded. A pixel-ratio change is the same: the
        // cached 1x file and the 2x file are different images.
        if (zoom != activeZoom || desiredPixelRatio != activePixelRatio)
        {
            ClearTiles();
            activeZoom = zoom;
            activePixelRatio = desiredPixelRatio;
        }

        centreLat = lat;
        centreLon = lon;
        zoomLevel = zoom;

        centerTileX = LonToTileX(lon, zoom);
        centerTileY = LatToTileY(lat, zoom);

        var window = SurroundingTiles(centerTileX, centerTileY, tilesX, tilesY);
        var wanted = new HashSet<Vector2Int>(window);

        // Drop only what left the window. Everything else keeps its quad, material
        // and texture, so the ground never has a hole and no texture is re-uploaded.
        var stale = new List<Vector2Int>();
        foreach (var pair in activeTiles)
            if (!wanted.Contains(pair.Key)) stale.Add(pair.Key);
        for (int i = 0; i < stale.Count; i++) DestroyTile(stale[i]);

        centerTileLoaded = activeTiles.ContainsKey(new Vector2Int(centerTileX, centerTileY));
        if (centerTileLoaded) HideFallbackPlane();

        var highPriority = new List<Vector2Int>();
        var deferred = new List<(Vector2Int tile, int ring)>();

        for (int i = 0; i < window.Count; i++)
        {
            Vector2Int tile = window[i];
            if (activeTiles.ContainsKey(tile) || !requestedTiles.Add(tile)) continue;

            int dx = tile.x - centerTileX;
            int dy = tile.y - centerTileY;
            int ring = Mathf.Max(Mathf.Abs(dx), Mathf.Abs(dy));
            if (ring <= Mathf.Max(0, highPriorityRing))
                highPriority.Add(tile);
            else
                deferred.Add((tile, ring));
        }

        Transform viewer = MissionManager.Instance != null && MissionManager.Instance.busController != null
            ? MissionManager.Instance.busController.transform
            : null;
        if (viewer == null)
        {
            var bus = FindAnyObjectByType<BusController>();
            if (bus != null) viewer = bus.transform;
        }
        Vector3 viewOrigin = viewer != null ? viewer.position : transform.position;
        Vector3 viewForward = viewer != null ? ViewPriority.ResolveForward(viewer) : Vector3.forward;

        highPriority.Sort((a, b) =>
        {
            float scoreA = ViewPriority.Score(viewOrigin, viewForward, ViewPriority.DefaultFovDegrees, TileCenterWorld(a));
            float scoreB = ViewPriority.Score(viewOrigin, viewForward, ViewPriority.DefaultFovDegrees, TileCenterWorld(b));
            return scoreA.CompareTo(scoreB);
        });
        deferred.Sort((a, b) => a.ring != b.ring
            ? a.ring.CompareTo(b.ring)
            : ViewPriority.Score(viewOrigin, viewForward, ViewPriority.DefaultFovDegrees, TileCenterWorld(a.tile))
                .CompareTo(ViewPriority.Score(viewOrigin, viewForward, ViewPriority.DefaultFovDegrees, TileCenterWorld(b.tile))));

        int localVersion = loadVersion;
        for (int i = 0; i < highPriority.Count; i++)
        {
            Vector2Int tile = highPriority[i];
            StartCoroutine(DownloadTile(tile.x, tile.y, tile.x - centerTileX, tile.y - centerTileY, zoom, localVersion));
        }

        for (int i = 0; i < deferred.Count; i++)
        {
            Vector2Int tile = deferred[i].tile;
            float delay = Mathf.Max(0f, deferredTileStartDelay) * (i + 1);
            StartCoroutine(DownloadTileDelayed(tile.x, tile.y, tile.x - centerTileX, tile.y - centerTileY, zoom, localVersion, delay));
        }
    }

    /// <summary>
    /// The tile coordinates a loader of this size wants centred on a tile, ordered
    /// centre-out. Kept pure so the window can be checked without a network or a scene.
    /// </summary>
    public static List<Vector2Int> SurroundingTiles(int centerX, int centerY, int sizeX, int sizeY)
    {
        int halfX = Mathf.Max(0, sizeX) / 2;
        int halfY = Mathf.Max(0, sizeY) / 2;
        var tiles = new List<Vector2Int>(Mathf.Max(1, sizeX * sizeY));
        for (int x = -halfX; x <= halfX; x++)
            for (int y = -halfY; y <= halfY; y++)
                tiles.Add(new Vector2Int(centerX + x, centerY + y));

        tiles.Sort((a, b) => FlatDistanceSq(a, centerX, centerY).CompareTo(FlatDistanceSq(b, centerX, centerY)));
        return tiles;
    }

    static int FlatDistanceSq(Vector2Int tile, int centerX, int centerY)
    {
        int dx = tile.x - centerX;
        int dy = tile.y - centerY;
        return dx * dx + dy * dy;
    }

    Vector3 TileCenterWorld(Vector2Int tile)
    {
        double lat = TileYToLat(tile.y + 0.5, zoomLevel);
        double lon = TileXToLon(tile.x + 0.5, zoomLevel);
        return GpsToWorldPosition(lat, lon);
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
            yield return Cache.GetTileTexture(zoom, tileX, tileY, activePixelRatio, t =>
            {
                // The loader can be destroyed while a shared cache download is in
                // flight. The callback still runs on the cache object, so a texture
                // handed to a dead loader has to be released here.
                if (this != null) tex = t;
                else if (t != null) Destroy(t);
            });
        }
        else if (!Settings.HasToken)
        {
            // Nothing can be authorised without a credential, and sending the empty string is
            // worse than sending nothing: Mapbox answers 401 "Direct access not allowed",
            // which used to halt every later download as if the token were bad.
            WarnMissingTokenOnce();
            NoteTileFailure(tileX, tileY, MapDownloadPolicy.Describe(MapTileFailureKind.NoToken));
            yield break;
        }
        else
        {
            tex = null;
            int attempts = MapDownloadPolicy.ResolveMaxAttempts(maxDownloadAttempts);
            for (int attempt = 1; attempt <= attempts; attempt++)
            {
                string url = MapboxTileRequest.BuildTileUrl(
                    Settings.Style, zoom, tileX, tileY, Settings.Token, activePixelRatio);

                // Buffer the response first, then decode it ourselves. UnityWebRequestTexture
                // can report a DataProcessingError with HTTP 200 for otherwise valid Mapbox
                // PNG/JPEG tiles on some Unity/Linux combinations.
                MapTileFailureKind failure = MapTileFailureKind.None;
                string retryAfter = null;
                using (UnityWebRequest request = UnityWebRequest.Get(url))
                {
                    request.timeout = Mathf.Max(1, httpTimeoutSeconds);
                    request.SetRequestHeader("Accept", "image/png,image/jpeg;q=0.9,*/*;q=0.1");
                    yield return request.SendWebRequest();

                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        byte[] bytes = request.downloadHandler != null ? request.downloadHandler.data : null;
                        if (MapDownloadPolicy.LooksLikeImage(bytes))
                        {
                            tex = DecodeTile(bytes);
                            if (tex != null) break;
                        }
                        failure = MapTileFailureKind.InvalidResponse;
                        LogTileFailure(tileX, tileY, request,
                            "Mapbox returned HTTP 200, but the response was not a supported PNG/JPEG tile");
                    }
                    else
                    {
                        failure = MapDownloadPolicy.Classify(request.result, request.responseCode,
                            request.error, !string.IsNullOrEmpty(Settings.Token));
                        retryAfter = request.GetResponseHeader("Retry-After");
                        LogTileFailure(tileX, tileY, request);
                    }
                }

                if (attempt >= attempts || !MapDownloadPolicy.IsRetryable(failure))
                {
                    NoteTileFailure(tileX, tileY, MapDownloadPolicy.Describe(failure));
                    break;
                }

                yield return new WaitForSecondsRealtime(
                    MapDownloadPolicy.RetryDelaySeconds(attempt, retryAfter, UnityEngine.Random.value));
            }
        }

        if (preferOfflineCache && Cache != null && tex == null && version == loadVersion)
        {
            if (Cache.authFailureDetected) authFailureDetected = true;
            NoteTileFailure(tileX, tileY, string.IsNullOrWhiteSpace(Cache.lastFailureMessage)
                ? "Map tiles are unavailable"
                : Cache.lastFailureMessage);
        }

        if (version != loadVersion || tex == null)
        {
            if (tex != null) Destroy(tex);
            yield break;
        }

        CreateTileQuad(tex, offsetX, offsetY, tileX, tileY);
    }

    /// <summary>
    /// Records a tile that could not be shown. The tile under the bus is the one
    /// that decides whether the map has failed: an outer-ring tile failing is a
    /// gap the player may never see, but a missing centre tile is the loading
    /// screen's business.
    /// </summary>
    void NoteTileFailure(int tileX, int tileY, string reason)
    {
        var key = new Vector2Int(tileX, tileY);
        failedTiles.Add(key);
        requestedTiles.Remove(key);
        LastMapError = string.IsNullOrWhiteSpace(reason) ? "Map tiles are unavailable" : reason;

        bool wasFailed = MapFailed;
        bool isCentre = tileX == centerTileX && tileY == centerTileY;
        if (isCentre || authFailureDetected)
            MapFailed = true;

        if (MapFailed != wasFailed || isCentre)
            MapStateChanged?.Invoke();
    }

    /// <summary>
    /// Re-requests the tiles that failed, from the current centre. Called by the
    /// loading card's retry action and safe to call when nothing failed.
    /// </summary>
    public bool RetryFailedTiles()
    {
        if (failedTiles.Count == 0 && !MapFailed)
            return false;

        failedTiles.Clear();
        MapFailed = false;
        LastMapError = null;
        requestedTiles.Clear();
        authFailureDetected = false;
        loggedAuthFailureHint = false;
        tileFailureLogCount = 0;
        MapStateChanged?.Invoke();
        LoadMap(centreLat, centreLon, zoomLevel);
        return true;
    }

    void CreateTileQuad(Texture2D tex, int offsetX, int offsetY, int tileX, int tileY)
    {
        ApplyTileTextureSettings(tex);

        // A displayed tile proves the credential and network are working; clear a
        // halt caused by an earlier transient 401 so following tiles keep loading.
        authFailureDetected = false;

        var key = new Vector2Int(tileX, tileY);
        requestedTiles.Remove(key);
        failedTiles.Remove(key);
        if (tileX == centerTileX && tileY == centerTileY)
        {
            bool wasFailed = MapFailed;
            MapFailed = false;
            LastMapError = null;
            if (wasFailed) MapStateChanged?.Invoke();
        }
        if (activeTiles.ContainsKey(key))
        {
            // A second response for the same tile can arrive if the window moved while
            // the request was in flight. Keep the first quad rather than stacking a
            // coplanar duplicate that would z-fight with it.
            Destroy(tex);
            return;
        }

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

        // A lit ground material, not Unlit/Texture. The imagery already carries its
        // own baked shading, but an unlit quad ignores every shadow the buildings and
        // trees cast, which is most of why the street read as flat. Standard keeps
        // the imagery, receives the sun's cascaded shadows and takes fog, so the
        // ground also fades into the haze instead of ending at a hard edge.
        renderer.material = CreateGroundMaterial(tex);
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = true;

        if (renderer.material.HasProperty("_Cull"))
            renderer.material.SetFloat("_Cull", (float)UnityEngine.Rendering.CullMode.Off);
        if (renderer.material.HasProperty("_ZWrite"))
            renderer.material.SetFloat("_ZWrite", 1f);

        if (!keepTileColliders)
            Destroy(quad.GetComponent<Collider>());
        if (tileLayer > 0) quad.layer = tileLayer;
        activeTiles[key] = quad;

        if (tileX == centerTileX && tileY == centerTileY)
        {
            centerTileLoaded = true;
            HideFallbackPlane();
            Debug.Log("MapTileLoader: Center tile loaded. Fallback plane hidden.");
        }
        else if (MapFailed && centerTileLoaded)
        {
            // The centre is on screen and a later tile arrived, so a transient
            // refusal that marked the whole map failed has cleared. Do not keep the
            // loading card blocked on an error the map has already recovered from.
            MapFailed = false;
            LastMapError = null;
            MapStateChanged?.Invoke();
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
        // A negative bias sharpens the near ground but makes the far rows crawl as the
        // bus moves; the old -0.75 was the shimmer. Zero keeps mip selection honest.
        tex.mipMapBias = Mathf.Clamp(tileMipMapBias, -0.35f, 0f);
    }

    Material CreateGroundMaterial(Texture2D tex)
    {
        // Legacy Diffuse with fullforwardshadows: a shadow-receiving, fog-aware
        // ground for a fraction of Standard's cost. Ground is the largest surface on
        // screen, so the cheapest lit shader that still takes building shadows is the
        // right one here. Standard is the fallback if a build strips the legacy shader.
        Shader shader = Shader.Find("Legacy Shaders/Diffuse");
        if (shader == null) shader = Shader.Find("Standard");
        if (shader == null) shader = BuiltInShaders.UnlitTexture;

        var material = new Material(shader) { name = "Mapbox Ground Tile" };
        material.mainTexture = tex;
        material.color = new Color(0.82f, 0.84f, 0.82f, 1f);
        if (material.HasProperty("_Glossiness")) material.SetFloat("_Glossiness", 0.03f);
        if (material.HasProperty("_Smoothness")) material.SetFloat("_Smoothness", 0.03f);
        if (material.HasProperty("_Metallic")) material.SetFloat("_Metallic", 0f);
        if (material.HasProperty("_Cull"))
            material.SetFloat("_Cull", (float)UnityEngine.Rendering.CullMode.Off);
        return material;
    }

    Shader ResolveTileShader() => BuiltInShaders.UnlitTexture;

    void DestroyTile(Vector2Int key)
    {
        if (!activeTiles.TryGetValue(key, out GameObject tile)) return;
        activeTiles.Remove(key);
        if (tile == null) return;

        var renderer = tile.GetComponent<Renderer>();
        if (renderer != null && renderer.sharedMaterial != null)
        {
            if (renderer.sharedMaterial.mainTexture != null) Destroy(renderer.sharedMaterial.mainTexture);
            Destroy(renderer.sharedMaterial);
        }
        Destroy(tile);
    }

    void ClearTiles()
    {
        var keys = new List<Vector2Int>(activeTiles.Keys);
        for (int i = 0; i < keys.Count; i++) DestroyTile(keys[i]);
        activeTiles.Clear();
        requestedTiles.Clear();
        failedTiles.Clear();
        loggedFirstTile = false;
        centerTileLoaded = false;
        MapFailed = false;
        LastMapError = null;
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

        // The scene builder names its temporary ground "Fallback_Test_Plane",
        // while older scenes used "Plane". Looking only for the serialized name
        // left the 1.6 km fallback plane alive under the loaded tiles forever.
        GameObject plane = GameObject.Find(fallbackPlaneObjectName);
        if (plane == null) plane = GameObject.Find("Fallback_Test_Plane");
        if (plane == null) plane = GameObject.Find("Plane");
        if (plane == null)
            return;

        plane.SetActive(false);
    }

    static Texture2D DecodeTile(byte[] bytes)
    {
        if (!MapDownloadPolicy.LooksLikeImage(bytes))
            return null;

        var texture = new Texture2D(2, 2, TextureFormat.RGBA32, true);
        // Map tiles are sampled by the ground material only; no gameplay code reads
        // their pixels. A non-readable texture halves the CPU-side copy and lets
        // Unity release the backing store after upload.
        if (texture.LoadImage(bytes, true))
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
