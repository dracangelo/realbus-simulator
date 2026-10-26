using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Warms the assets the player will see first, before the loading overlay
/// dismisses. The scene's readiness milestones say the systems have started;
/// this coordinator adds the part the player actually notices - the near map
/// tiles, the buildings in the windscreen, the first traffic and the roadside
/// props all being present rather than streaming in after the countdown.
///
/// Every wait has a ceiling. A slow network must never trap the player on a
/// loading screen, so once a stage times out the next one starts and the
/// remaining assets keep streaming in behind the bus as before.
/// </summary>
[DefaultExecutionOrder(-140)]
public class WorldPreloadCoordinator : MonoBehaviour
{
    public static WorldPreloadCoordinator Instance { get; private set; }

    [SerializeField] float coreResourceTimeoutSeconds = 8f;
    [SerializeField] float nearTilesTimeoutSeconds = 9f;
    [SerializeField] float roadTimeoutSeconds = 8f;
    [SerializeField] float buildingTimeoutSeconds = 12f;
    [SerializeField] float trafficTimeoutSeconds = 8f;
    [SerializeField] float sceneryTimeoutSeconds = 9f;
    [SerializeField] float detailTimeoutSeconds = 18f;
    [SerializeField] float overallTimeoutSeconds = 32f;

    public bool IsReady { get; private set; }
    public bool TimedOut { get; private set; }
    public string Status { get; private set; } = "PREPARING ASSETS...";
    public float Progress { get; private set; }

    /// <summary>The map loader gave up on the centre tiles; the card shows the error and a retry.</summary>
    public bool MapUnavailable { get; private set; }
    public string MapFailureMessage { get; private set; }

    readonly List<ResourceRequest> coreRequests = new List<ResourceRequest>();
    readonly List<Object> preloadedAssets = new List<Object>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void RegisterSceneBootstrap()
    {
        // A menu-to-gameplay transition is a scene load inside an already-running
        // play session; AfterSceneLoad only fires at play-mode entry. Hook every
        // scene load so the near-field warm-up starts for the route the player
        // actually picked, not only when GameScene is the first scene played.
        SceneManager.sceneLoaded -= BootstrapOnSceneLoaded;
        SceneManager.sceneLoaded += BootstrapOnSceneLoaded;
    }

    static void BootstrapOnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Instance != null) return;
        if (FindAnyObjectByType<MissionManager>() == null) return;
        new GameObject("WorldPreloadCoordinator").AddComponent<WorldPreloadCoordinator>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    void Start()
    {
        StartCoroutine(Preload());
    }

    IEnumerator Preload()
    {
        yield return null;

        float start = Time.realtimeSinceStartup;

        // Start the near-field detail pass in parallel with the shared catalogs and
        // the map/road stages. It is bounded independently, so a slow optional
        // shelter pack can never extend the whole load past its own budget.
        NearFieldAssetWarmup detail = NearFieldAssetWarmup.EnsureExists();

        RequestCore<BuildingCatalog>("BuildingCatalog");
        RequestCore<TrafficCatalog>("TrafficCatalog");
        RequestCore<LiveCityCatalog>("LiveCityCatalog");
        RequestCore<AudioProductionProfile>("AudioProductionProfile");
        RequestCore<PassengerReactionLibrary>("PassengerReactionLibrary");

        int done = 0;
        while (done < coreRequests.Count)
        {
            done = 0;
            for (int i = 0; i < coreRequests.Count; i++)
                if (coreRequests[i] == null || coreRequests[i].isDone) done++;

            Progress = 0.24f * (coreRequests.Count > 0 ? done / (float)coreRequests.Count : 1f);
            Status = "LOADING SHARED ASSETS...";
            if (Time.realtimeSinceStartup - start >= coreResourceTimeoutSeconds)
                break;
            yield return null;
        }

        // Keep the loaded catalogs alive until the scene systems have picked
        // them up, instead of letting the async handles release immediately.
        // Reading .asset on an incomplete request would block, which would defeat
        // the timeout above, so only completed requests are retained.
        for (int i = 0; i < coreRequests.Count; i++)
        {
            ResourceRequest request = coreRequests[i];
            if (request != null && request.isDone && request.asset != null && !preloadedAssets.Contains(request.asset))
                preloadedAssets.Add(request.asset);
        }

        float worldStart = Time.realtimeSinceStartup;
        MapTileLoader map = null;
        OSMRoadMeshBuilder roads = null;
        OSMBuildingMeshBuilder buildings = null;
        ProceduralBuildingFill infill = null;
        VehiclePool traffic = null;
        LiveCityStreamer scenery = null;
        bool mapReady = false, roadsReady = false, buildingsReady = false;
        bool infillReady = false, trafficReady = false, sceneryReady = false, detailReady = false;
        bool mapComplete = false, roadsComplete = false, buildingsComplete = false;
        bool infillComplete = false, trafficComplete = false, sceneryComplete = false, detailComplete = false;

        while (Time.realtimeSinceStartup - start < overallTimeoutSeconds)
        {
            // Per-stage ceilings start after the shared assets, not at process
            // start: a slow catalog load must not make the road and tile stages
            // time out before they have had a chance to run.
            float elapsed = Time.realtimeSinceStartup - worldStart;

            if (map == null) map = MapTileLoader.Instance;
            if (roads == null) roads = OSMRoadMeshBuilder.Instance;
            if (buildings == null) buildings = OSMBuildingMeshBuilder.Instance;
            if (infill == null && buildings != null) infill = buildings.GetComponent<ProceduralBuildingFill>();
            if (traffic == null) traffic = FindAnyObjectByType<VehiclePool>();
            if (scenery == null) scenery = FindAnyObjectByType<LiveCityStreamer>();
            if (detail == null) detail = NearFieldAssetWarmup.EnsureExists();

            // A failed map is a settled stage: waiting out the rest of the budget
            // would only delay the error card and the retry button. The failure is
            // surfaced, not swallowed.
            MapUnavailable = map != null && map.MapFailed;
            MapFailureMessage = MapUnavailable ? map.LastMapError : null;
            mapComplete = map == null || map.NearTilesLoaded || MapUnavailable;
            roadsComplete = roads == null || roads.roadsBuilt;
            buildingsComplete = buildings == null || buildings.InitialRingReady;
            infillComplete = infill == null || infill.InitialFillComplete;
            trafficComplete = traffic == null || traffic.WarmupComplete;
            sceneryComplete = scenery == null || scenery.InitialPassComplete;
            detailComplete = detail == null || detail.IsReady;

            // A stage that runs past its ceiling still lets the next one start,
            // but it remains a timeout for the warning and diagnostics below.
            mapReady = mapComplete || elapsed >= nearTilesTimeoutSeconds;
            roadsReady = roadsComplete || elapsed >= roadTimeoutSeconds;
            buildingsReady = buildingsComplete || elapsed >= buildingTimeoutSeconds;
            infillReady = infillComplete || elapsed >= buildingTimeoutSeconds;
            trafficReady = trafficComplete || elapsed >= trafficTimeoutSeconds;
            sceneryReady = sceneryComplete || elapsed >= sceneryTimeoutSeconds;
            detailReady = detailComplete || elapsed >= detailTimeoutSeconds;

            float readiness = 0f;
            readiness += mapReady ? 0.16f : 0f;
            readiness += roadsReady ? 0.09f : 0f;
            readiness += buildingsReady ? 0.15f : 0f;
            readiness += infillReady ? 0.11f : 0f;
            readiness += trafficReady ? 0.13f : 0f;
            readiness += sceneryReady ? 0.10f : 0f;
            readiness += detailReady ? 0.26f : 0f;
            Progress = Mathf.Lerp(0.25f, 0.98f, readiness);

            if (!mapReady)
                Status = "DOWNLOADING NEARBY MAP TILES...";
            else if (!roadsReady)
                Status = "BUILDING NEARBY ROADS...";
            else if (!buildingsReady || !infillReady)
                Status = "PLACING BUILDINGS IN VIEW...";
            else if (!trafficReady)
                Status = "SPAWNING TRAFFIC...";
            else if (!sceneryReady)
                Status = "STREAMING STREET DETAIL...";
            else if (!detailReady)
                Status = detail != null && !string.IsNullOrWhiteSpace(detail.Status)
                    ? detail.Status
                    : "PREPARING STOP PROPS & BANNERS...";
            else
                break;

            yield return null;
        }

        // A long synchronous frame (shelter placement, a building ring) can take
        // the loop past its ceiling before the flags are refreshed. Re-read the
        // live systems once so the log and the timeout decision describe the world
        // we actually finished with, not the frame before the stall.
        if (map != null)
        {
            MapUnavailable = map.MapFailed;
            MapFailureMessage = MapUnavailable ? map.LastMapError : null;
            mapComplete = map.NearTilesLoaded || MapUnavailable;
        }
        if (roads != null) roadsComplete = roads.roadsBuilt;
        if (buildings != null) buildingsComplete = buildings.InitialRingReady;
        if (infill != null) infillComplete = infill.InitialFillComplete;
        if (traffic != null) trafficComplete = traffic.WarmupComplete;
        if (scenery != null) sceneryComplete = scenery.InitialPassComplete;
        if (detail != null) detailComplete = detail.IsReady;

        TimedOut = !(mapComplete && roadsComplete && buildingsComplete &&
                     infillComplete && trafficComplete && sceneryComplete && detailComplete);
        if (TimedOut)
        {
            Debug.LogWarning(
                $"[WorldPreload] Near-field warm-up exceeded its budget after " +
                $"{Time.realtimeSinceStartup - start:0.0}s (map={mapComplete}, roads={roadsComplete}, " +
                $"buildings={buildingsComplete}, infill={infillComplete}, traffic={trafficComplete}, " +
                $"scenery={sceneryComplete}, detail={detailComplete}). " +
                "Streaming continues behind the loading screen.");
        }

        if (!TimedOut)
        {
            string coverage = detail != null ? detail.Coverage : "no detail warmer";
            Debug.Log($"[WorldPreload] Near-field ready in {Time.realtimeSinceStartup - start:0.0}s " +
                $"(map={mapComplete}, roads={roadsComplete}, buildings={buildingsComplete}, " +
                $"infill={infillComplete}, traffic={trafficComplete}, scenery={sceneryComplete}, " +
                $"detail={detailComplete}, view={coverage}).");
        }

        Progress = 1f;
        Status = "NEARBY WORLD READY";
        IsReady = true;
    }

    /// <summary>
    /// Drops the loader's own references to the preloaded catalogs once the world is
    /// playable, so the loading pass stops keeping assets alive. Anything a live
    /// scene system still uses stays loaded: this list was only a bridge between
    /// <see cref="Resources.LoadAsync"/> and the systems that read the catalogs.
    /// </summary>
    public void ReleasePreloadedReferences()
    {
        preloadedAssets.Clear();
        coreRequests.Clear();
    }

    void RequestCore<T>(string path) where T : Object
    {
        ResourceRequest request = Resources.LoadAsync<T>(path);
        if (request != null)
            coreRequests.Add(request);
    }
}
