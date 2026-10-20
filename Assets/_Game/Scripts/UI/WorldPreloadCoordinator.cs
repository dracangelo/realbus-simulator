using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] float overallTimeoutSeconds = 24f;

    public bool IsReady { get; private set; }
    public bool TimedOut { get; private set; }
    public string Status { get; private set; } = "PREPARING ASSETS...";
    public float Progress { get; private set; }

    readonly List<ResourceRequest> coreRequests = new List<ResourceRequest>();
    readonly List<Object> preloadedAssets = new List<Object>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap()
    {
        if (FindAnyObjectByType<MissionManager>() == null) return;
        if (Instance != null) return;
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
        bool infillReady = false, trafficReady = false, sceneryReady = false;
        bool mapComplete = false, roadsComplete = false, buildingsComplete = false;
        bool infillComplete = false, trafficComplete = false, sceneryComplete = false;

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

            mapComplete = map == null || map.NearTilesLoaded;
            roadsComplete = roads == null || roads.roadsBuilt;
            buildingsComplete = buildings == null || buildings.InitialRingReady;
            infillComplete = infill == null || infill.InitialFillComplete;
            trafficComplete = traffic == null || traffic.WarmupComplete;
            sceneryComplete = scenery == null || scenery.InitialPassComplete;

            // A stage that runs past its ceiling still lets the next one start,
            // but it remains a timeout for the warning and diagnostics below.
            mapReady = mapComplete || elapsed >= nearTilesTimeoutSeconds;
            roadsReady = roadsComplete || elapsed >= roadTimeoutSeconds;
            buildingsReady = buildingsComplete || elapsed >= buildingTimeoutSeconds;
            infillReady = infillComplete || elapsed >= buildingTimeoutSeconds;
            trafficReady = trafficComplete || elapsed >= trafficTimeoutSeconds;
            sceneryReady = sceneryComplete || elapsed >= sceneryTimeoutSeconds;

            float readiness = 0f;
            readiness += mapReady ? 0.20f : 0f;
            readiness += roadsReady ? 0.12f : 0f;
            readiness += buildingsReady ? 0.20f : 0f;
            readiness += infillReady ? 0.16f : 0f;
            readiness += trafficReady ? 0.18f : 0f;
            readiness += sceneryReady ? 0.14f : 0f;
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
            else
                break;

            yield return null;
        }

        TimedOut = !(mapComplete && roadsComplete && buildingsComplete &&
                     infillComplete && trafficComplete && sceneryComplete);
        if (TimedOut)
        {
            Debug.LogWarning(
                $"[WorldPreload] Near-field warm-up exceeded its budget after " +
                $"{Time.realtimeSinceStartup - start:0.0}s (map={mapComplete}, roads={roadsComplete}, " +
                $"buildings={buildingsComplete}, infill={infillComplete}, traffic={trafficComplete}, scenery={sceneryComplete}). " +
                "Streaming continues behind the loading screen.");
        }

        if (!TimedOut)
        {
            Debug.Log($"[WorldPreload] Near-field ready in {Time.realtimeSinceStartup - start:0.0}s " +
                $"(map={mapComplete}, roads={roadsComplete}, buildings={buildingsComplete}, " +
                $"infill={infillComplete}, traffic={trafficComplete}, scenery={sceneryComplete}).");
        }

        Progress = 1f;
        Status = "NEARBY WORLD READY";
        IsReady = true;
    }

    void RequestCore<T>(string path) where T : Object
    {
        ResourceRequest request = Resources.LoadAsync<T>(path);
        if (request != null)
            coreRequests.Add(request);
    }
}
