using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>Standalone Phase 3 integration scene using local OSM and optional Mapbox raster tiles.</summary>
public class MapTestBootstrap : MonoBehaviour
{
    public CityConfig tutorialCity;
    public CityConfig[] futureCities;
    public EngineSystem engineProfile;
    public TransmissionSystem transmissionProfile;
    [Tooltip("Use a local routes.json export or import real relations through the button during Play mode.")]
    public bool loadLocalRoutes = true;
    public bool showDebugUI = true;
    public bool IsReady { get; private set; }
    public BusController Bus => vehicleFixture != null ? vehicleFixture.Bus : null;
    public CoordinateConverter Converter => converter;
    public RoadGraph Graph { get; private set; }
    public string Status { get; private set; } = "Preparing map...";
    CoordinateConverter converter;
    MapOrigin origin;
    CityDefinition city;
    OfflineCacheManager cache;
    CityPredownloader downloader;
    TileStreamManager streaming;
    RoadNetworkSurface surface;
    OSMRouteImporter importer;
    PhysicsTestTrack vehicleFixture;
    readonly List<BusRoute> localRoutes = new List<BusRoute>();
    GameObject stopRoot;
    string runtimeToken = "";
    Material stopMaterial;
    bool ownsCache;

    IEnumerator Start()
    {
        if (tutorialCity == null || !tutorialCity.IsValid(out _))
        { Status = "Assign a valid tutorial CityConfig."; yield break; }
        city = tutorialCity.CreateRuntimeCityDefinition();
        origin = ScriptableObject.CreateInstance<MapOrigin>();
        origin.SetFromCity(city);
        converter = new GameObject("Coordinate bridge").AddComponent<CoordinateConverter>();
        converter.mapOrigin = origin;
        cache = OfflineCacheManager.Instance;
        if (cache == null) { cache = new GameObject("Offline tile cache").AddComponent<OfflineCacheManager>(); ownsCache = true; }
        cache.maxCacheMB = 500f;
        runtimeToken = System.Environment.GetEnvironmentVariable("REALBUS_MAPBOX_TOKEN") ?? "";
        downloader = new GameObject("City predownloader").AddComponent<CityPredownloader>();
        downloader.mapboxToken = runtimeToken;
        downloader.mapStyle = "mapbox/streets-v12";
        downloader.zoomLevel = tutorialCity.defaultZoom;
        downloader.radiusMeters = 2000f;
        importer = new GameObject("OSM route importer").AddComponent<OSMRouteImporter>();
        importer.OnImportComplete += ShowRoutes;
        vehicleFixture = new GameObject("Test bus fixture").AddComponent<PhysicsTestTrack>();
        vehicleFixture.createTestSurface = false;
        vehicleFixture.engineProfile = engineProfile;
        vehicleFixture.transmissionProfile = transmissionProfile;
        while (vehicleFixture.Bus == null) yield return null;
        var bus = vehicleFixture.Bus;
        var body = bus.GetComponent<Rigidbody>();
        body.isKinematic = true;
        Status = "Loading local OSM roads...";
        string roadsJson = null;
        yield return ReadCityFile("roads.json", json => roadsJson = json);
        if (string.IsNullOrEmpty(roadsJson))
        { Status = "Missing city roads.json. Export roads with the OSM importer before driving."; yield break; }
        var response = OverpassResponse.Deserialize(roadsJson);
        string routesJson = null;
        if (loadLocalRoutes) yield return ReadCityFile("routes.json", text => routesJson = text);
        OverpassResponse relations = null;
        if (!string.IsNullOrEmpty(routesJson))
        {
            relations = OverpassResponse.Deserialize(routesJson);
            relations.Resolve();
            var wayIds = new HashSet<long>();
            foreach (var element in response.elements) if (element.type == "way") wayIds.Add(element.id);
            foreach (var element in relations.elements)
                if (element.type == "way" && wayIds.Add(element.id)) response.elements.Add(element);
        }
        surface = new GameObject("Road surfaces").AddComponent<RoadNetworkSurface>();
        yield return surface.Build(response, converter);
        var graph = RoadGraph.BuildFromOverpassWays(response, converter);
        Graph = graph;
        if (!surface.IsReady || !graph.TryProjectToNearestSegment(Vector3.zero, out var spawn, out int a, out int b, out _))
        { Status = "No drivable roads in local data."; yield break; }
        body.position = spawn + Vector3.up * (surface.surfaceY + 0.2f);
        body.rotation = Quaternion.LookRotation(graph.nodes[b].world - graph.nodes[a].world);
        Physics.SyncTransforms();
        body.isKinematic = false;
        bus.SetParkingBrake(true);
        var detector = bus.gameObject.AddComponent<RoadSurfaceDetector>();
        detector.busController = bus;
        var grip = bus.gameObject.AddComponent<RoadSurfaceFrictionController>();
        grip.busController = bus; grip.surfaceDetector = detector;
        grip.baseForwardStiffness = 1.2f; grip.baseSidewaysStiffness = 1f;
        var gps = bus.gameObject.AddComponent<GpsTracker>();
        gps.busTransform = bus.transform; gps.converter = converter;
        streaming = new GameObject("Tile streaming").AddComponent<TileStreamManager>();
        streaming.converter = converter; streaming.cache = cache;
        streaming.busTransform = bus.transform; streaming.busController = bus; streaming.gpsTracker = gps;
        streaming.mapboxToken = runtimeToken; streaming.mapStyle = downloader.mapStyle;
        streaming.zoomLevel = tutorialCity.defaultZoom;
        streaming.createMeshCollider = false; // OSM road geometry owns collisions.
        streaming.maxConcurrentDownloads = 2;
        streaming.enabled = !string.IsNullOrWhiteSpace(runtimeToken) || cache.forceOffline;
        string stopsJson = null;
        yield return ReadCityFile("stops.json", json => stopsJson = json);
        int stopCount = ShowStops(stopsJson);
        if (relations != null)
        {
            foreach (var parsed in BusRouteParser.ParseRoutes(relations))
            {
                if (!RoadRoutePathBuilder.TryBuild(parsed, graph, converter, importer.maxSnapDistanceMeters))
                    parsed.routeName += " [OSM preview: disconnected]";
                localRoutes.Add(OSMRouteImporter.CreateRouteAsset(parsed, converter));
            }
            ShowRoutes(localRoutes);
        }
        IsReady = true;
        Status = $"Nairobi: {surface.RoadCount} roads, {stopCount} stops, {localRoutes.Count} OSM route previews. Release parking brake (P) to drive.";
    }

    IEnumerator ReadCityFile(string name, System.Action<string> result)
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Cities", city.cityCode, name);
        string url = path.Contains("://") ? path : new System.Uri(path).AbsoluteUri;
        using (var request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            result(request.result == UnityWebRequest.Result.Success ? request.downloadHandler.text : null);
        }
    }

    int ShowStops(string json)
    {
        if (string.IsNullOrEmpty(json)) return 0;
        stopRoot = new GameObject("GPS bus stop markers");
        int count = 0;
        foreach (var element in OverpassResponse.Deserialize(json).elements)
        {
            if (element.type != "node") continue;
            var marker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            Destroy(marker.GetComponent<Collider>());
            marker.name = element.tags.TryGetValue("name", out var name) ? name : "Stop " + element.id;
            marker.transform.SetParent(stopRoot.transform, false);
            marker.transform.position = converter.GeoToWorldPosition(element.lat, element.lon) + Vector3.up * 1.5f;
            marker.transform.localScale = new Vector3(0.35f, 1.5f, 0.35f);
            count++;
        }
        return count;
    }

    void ShowRoutes(List<BusRoute> routes)
    {
        var validated = new List<BusRoute>();
        foreach (var route in routes) if (route.isRoadPathValidated) validated.Add(route);
        city.availableRoutes = validated.ToArray();
        Status = $"Imported {routes.Count} connected OSM routes.";
        foreach (var route in routes)
        {
            var go = new GameObject(route.routeName);
            go.transform.SetParent(transform, false);
            var line = go.AddComponent<LineRenderer>();
            line.sharedMaterial = RouteMaterial;
            line.startWidth = line.endWidth = 0.4f;
            line.positionCount = route.pathPoints.Length;
            var points = (Vector3[])route.pathPoints.Clone();
            for (int i = 0; i < points.Length; i++) points[i].y = 0.3f;
            line.SetPositions(points);
        }
    }
    Material RouteMaterial
    {
        get
        {
            if (stopMaterial == null)
            {
                stopMaterial = new Material(Shader.Find("Universal Render Pipeline/Unlit") ?? Shader.Find("Sprites/Default"));
                stopMaterial.color = Color.cyan;
            }
            return stopMaterial;
        }
    }

    void OnGUI()
    {
        if (!showDebugUI) return;
        GUI.Box(new Rect(10, 160, 600, 245), "Phase 3 — Real map integration");
        GUI.Label(new Rect(20, 185, 580, 45), Status);
        if (streaming == null) return;
        runtimeToken = GUI.PasswordField(new Rect(20, 235, 370, 25), runtimeToken, '*');
        if (GUI.Button(new Rect(400, 235, 190, 25), "Enable map tiles"))
        {
            streaming.mapboxToken = downloader.mapboxToken = runtimeToken.Trim();
            streaming.ResetStreaming();
            streaming.enabled = true;
        }
        cache.forceOffline = GUI.Toggle(new Rect(20, 270, 180, 25), cache.forceOffline, "Offline test mode");
        if (GUI.Button(new Rect(200, 270, 190, 25), "Download 2 km area")) downloader.StartDownloadForCity(city);
        if (GUI.Button(new Rect(400, 270, 190, 25), "Cancel download")) downloader.CancelDownload();
        if (GUI.Button(new Rect(20, 305, 220, 25), "Import real OSM routes")) importer.TriggerImport(city);
        GUI.Label(new Rect(250, 305, 340, 25), importer.statusMessage);
        GUI.Label(new Rect(20, 340, 580, 25), $"Tiles: {streaming.ActiveTileCount} loaded / {streaming.PendingTileCount} pending. Offline download: {downloader.completedTiles}/{downloader.totalTiles}, failed {downloader.failedTiles}, ~{downloader.estimatedMB:F1} MB");
        if (GUI.Button(new Rect(20, 370, 100, 25), "© Mapbox")) Application.OpenURL("https://www.mapbox.com/about/maps/");
        if (GUI.Button(new Rect(130, 370, 260, 25), "© OpenStreetMap contributors")) Application.OpenURL("https://www.openstreetmap.org/copyright");
        if (GUI.Button(new Rect(400, 370, 190, 25), "Improve this map")) Application.OpenURL("https://apps.mapbox.com/feedback/");
    }

    void OnDestroy()
    {
        if (importer != null) importer.OnImportComplete -= ShowRoutes;
        foreach (var route in localRoutes) if (route != null) Destroy(route);
        if (origin != null) Destroy(origin);
        if (city != null) Destroy(city);
        if (stopMaterial != null) Destroy(stopMaterial);
        if (ownsCache && cache != null) Destroy(cache.gameObject);
    }
}
