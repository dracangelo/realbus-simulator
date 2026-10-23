using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Warms the small, player-facing asset set that used to arrive after the loading
/// card disappeared: stop shelters and their generated posters, fuel stations,
/// roadside environment props, road model prefabs and the generated asphalt/banner
/// textures.
///
/// The heavy city systems already have their own streaming rings and view-aware
/// ordering. This pass is deliberately the opposite: it does not build the whole
/// city, only the handful of things the player can see from the first stop. Prefabs
/// are requested with <see cref="Resources.LoadAsync{T}"/> so the card keeps drawing
/// while the loader works, and procedural textures are generated once, behind the
/// card, instead of hitching on the first stop the bus reaches.
///
/// Every wait has a ceiling and every missing optional pack is treated as ready.
/// A city without imported shelters must not hold the player on a loading screen.
/// </summary>
[DefaultExecutionOrder(-135)]
public class NearFieldAssetWarmup : MonoBehaviour
{
    public static NearFieldAssetWarmup Instance { get; private set; }

    [Header("Near-field coverage")]
    [Tooltip("Radius used to count the renderers the player can see immediately.")]
    [SerializeField] float nearRadiusMeters = 180f;
    [Tooltip("Longer, narrower probe used for the first-frame field of view.")]
    [SerializeField] float fovRadiusMeters = 260f;

    [Header("Budgets")]
    [SerializeField] float resourceTimeoutSeconds = 8f;
    [SerializeField] float spawnerTimeoutSeconds = 15f;
    [Tooltip("How long a spawner may take to appear before a scene without it is treated as complete.")]
    [SerializeField] float spawnerDiscoverySeconds = 2f;
    [SerializeField] float overallTimeoutSeconds = 20f;

    public bool IsReady { get; private set; }
    public bool TimedOut { get; private set; }
    public float Progress { get; private set; }
    public string Status { get; private set; } = "WARMING NEARBY ASSETS...";

    /// <summary>Short diagnostic for the smoke check and the preload log.</summary>
    public string Coverage { get; private set; } = "not measured";

    public int NearRenderers { get; private set; }
    public int NearViewRenderers { get; private set; }
    public int MissingMaterialRenderers { get; private set; }

    static readonly string[] DefaultStopPropPaths =
    {
        "BussimAssets/stops/Bench",
        "BussimAssets/stops/Bus_Stand",
        "BussimAssets/stops/StopSign",
        "BussimAssets/stops/bus_stopEU"
    };

    readonly List<ResourceRequest> requests = new List<ResourceRequest>();
    readonly List<Object> retained = new List<Object>();
    readonly HashSet<string> queued = new HashSet<string>();
    readonly HashSet<string> folders = new HashSet<string>();

    StopPropSpawner stopSpawner;
    RuntimeEnvironmentSpawner environmentSpawner;
    RuntimeGasStationSpawner gasSpawner;
    bool materialsWarmed;
    bool stopResourcesWarmed;

    public static NearFieldAssetWarmup EnsureExists()
    {
        if (Instance != null) return Instance;
        var existing = FindAnyObjectByType<NearFieldAssetWarmup>();
        return existing != null ? existing : new GameObject("NearFieldAssetWarmup").AddComponent<NearFieldAssetWarmup>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    void Start()
    {
        StartCoroutine(WarmNearField());
    }

    IEnumerator WarmNearField()
    {
        // Let the scene bootstrap finish adding its spawners and let the loading
        // card draw one frame before any procedural texture work starts.
        yield return null;

        float start = Time.realtimeSinceStartup;
        ResolveSpawners();
        QueueResourceRequests();

        Status = "GENERATING ROAD & BANNER TEXTURES...";
        Progress = 0.02f;
        yield return null;

        // These are procedural and synchronous by nature. Doing them here, while
        // the card is up, moves the cost off the first stop the player reaches.
        UrbanMaterials.Warmup();
        BusStopPosterBuilder.Prewarm();
        StreetTextureLibrary.Prewarm();
        materialsWarmed = true;

        while (Time.realtimeSinceStartup - start < overallTimeoutSeconds)
        {
            ResolveSpawners();
            QueueResourceRequests();

            float elapsed = Time.realtimeSinceStartup - start;
            bool resourcesComplete = ResourcesComplete();
            bool resourcesReady = resourcesComplete || elapsed >= resourceTimeoutSeconds;

            // The scene bootstrap adds its spawners a frame or two after this
            // component starts. Treat a missing spawner as complete only after a
            // short discovery window, otherwise the warm-up would declare success
            // before the stop props it is supposed to wait for even exist.
            bool discovered = elapsed >= spawnerDiscoverySeconds;
            bool propsComplete = stopSpawner != null ? stopSpawner.NearFieldComplete : discovered;
            bool environmentComplete = environmentSpawner != null
                ? (!environmentSpawner.spawnOnStart || environmentSpawner.SpawnComplete)
                : discovered;
            bool gasComplete = gasSpawner != null ? gasSpawner.SpawnComplete : discovered;

            bool propsReady = propsComplete || elapsed >= spawnerTimeoutSeconds;
            bool environmentReady = environmentComplete || elapsed >= spawnerTimeoutSeconds;
            bool gasReady = gasComplete || elapsed >= spawnerTimeoutSeconds;

            // Shared resources are more than a third of the warm-up because a
            // missing prefab is what produced the white, untextured props.
            float readiness = 0f;
            readiness += resourcesReady ? 0.32f : 0f;
            readiness += materialsWarmed ? 0.12f : 0f;
            readiness += propsReady ? 0.30f : 0f;
            readiness += environmentReady ? 0.10f : 0f;
            readiness += gasReady ? 0.08f : 0f;
            readiness += AllWarm(resourcesReady, propsReady, environmentReady, gasReady) ? 0.08f : 0f;
            Progress = Mathf.Clamp01(readiness);

            if (!resourcesReady)
                Status = "PRELOADING NEARBY PREFABS...";
            else if (!propsReady)
                Status = "PLACING STOP PROPS & BANNERS...";
            else if (!environmentReady || !gasReady)
                Status = "PLACING STREET DETAIL...";
            else
            {
                Status = "CHECKING THE VIEW AHEAD...";
                break;
            }

            yield return null;
        }

        // Retain completed assets so the scene systems can use the exact instances
        // the card loaded instead of releasing them and loading them again.
        RetainCompletedAssets();

        if (!ResourcesComplete() || (stopSpawner != null && !stopSpawner.NearFieldComplete) ||
            (environmentSpawner != null && environmentSpawner.spawnOnStart && !environmentSpawner.SpawnComplete) ||
            (gasSpawner != null && !gasSpawner.SpawnComplete))
        {
            TimedOut = true;
            Debug.LogWarning(
                $"[NearFieldWarmup] Finished its {Time.realtimeSinceStartup - start:0.0}s budget with optional detail still streaming " +
                $"(resources={ResourcesComplete()}, nearProps={stopSpawner == null || stopSpawner.NearFieldComplete}, " +
                $"environment={environmentSpawner == null || !environmentSpawner.spawnOnStart || environmentSpawner.SpawnComplete}, " +
                $"fuel={gasSpawner == null || gasSpawner.SpawnComplete}).");
        }

        MeasureCoverage();
        Progress = 1f;
        Status = "NEARBY ASSETS READY";
        IsReady = true;

        Debug.Log($"[NearFieldWarmup] Near/FOV assets ready in {Time.realtimeSinceStartup - start:0.0}s " +
                  $"({Coverage}, materialsWarmed={materialsWarmed}).");
    }

    bool AllWarm(bool resourcesReady, bool propsReady, bool environmentReady, bool gasReady)
        => resourcesReady && propsReady && environmentReady && gasReady;

    void ResolveSpawners()
    {
        if (stopSpawner == null) stopSpawner = FindAnyObjectByType<StopPropSpawner>();
        if (environmentSpawner == null) environmentSpawner = FindAnyObjectByType<RuntimeEnvironmentSpawner>();
        if (gasSpawner == null) gasSpawner = FindAnyObjectByType<RuntimeGasStationSpawner>();

        // Generated shelter packs are found with LoadAll rather than LoadAsync, so
        // ask the spawner to cache them only once the card is already covering the
        // scene. Its own spawn pass then reuses the cache.
        if (!stopResourcesWarmed && stopSpawner != null)
        {
            stopResourcesWarmed = true;
            stopSpawner.PrewarmResources();
        }
    }

    void QueueResourceRequests()
    {
        if (stopSpawner != null && stopSpawner.stopPropResourcePaths != null)
        {
            for (int i = 0; i < stopSpawner.stopPropResourcePaths.Length; i++)
                QueuePrefab(stopSpawner.stopPropResourcePaths[i]);
        }
        else
        {
            for (int i = 0; i < DefaultStopPropPaths.Length; i++)
                QueuePrefab(DefaultStopPropPaths[i]);
        }

        if (gasSpawner != null)
            QueuePrefab(gasSpawner.gasStationResourcePath);

        if (environmentSpawner != null && environmentSpawner.spawnList != null && environmentSpawner.spawnList.Length > 0)
        {
            for (int i = 0; i < environmentSpawner.spawnList.Length; i++)
                QueuePrefab(environmentSpawner.spawnList[i].resourcePath);
        }
        else
        {
            string[] defaults = RuntimeEnvironmentSpawner.DefaultResourcePaths();
            for (int i = 0; i < defaults.Length; i++)
                QueuePrefab(defaults[i]);
        }

        OSMRoadMeshBuilder roads = OSMRoadMeshBuilder.Instance;
        if (roads != null)
        {
            QueuePrefab(roads.roadModelResourcePath);
            QueuePrefab(roads.medianBarrierResourcePath);
        }

        PrewarmFolder("BusStopsGenerated");
        PrewarmFolder("GasStationsGenerated");
    }

    void QueuePrefab(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !queued.Add(path))
            return;

        ResourceRequest request = Resources.LoadAsync<GameObject>(path);
        if (request != null)
            requests.Add(request);
    }

    void PrewarmFolder(string folder)
    {
        if (string.IsNullOrWhiteSpace(folder) || !folders.Add(folder))
            return;

        // LoadAll has no async form. These packs are small optional imports and
        // the call happens under the loading card, never during driving.
        GameObject[] assets = Resources.LoadAll<GameObject>(folder);
        if (assets == null) return;
        for (int i = 0; i < assets.Length; i++)
            Retain(assets[i]);
    }

    bool ResourcesComplete()
    {
        for (int i = 0; i < requests.Count; i++)
        {
            ResourceRequest request = requests[i];
            if (request != null && !request.isDone)
                return false;
        }
        return true;
    }

    void RetainCompletedAssets()
    {
        for (int i = 0; i < requests.Count; i++)
        {
            ResourceRequest request = requests[i];
            if (request == null || !request.isDone)
                continue;
            Retain(request.asset);
        }
    }

    void Retain(Object asset)
    {
        if (asset == null || retained.Contains(asset)) return;
        retained.Add(asset);
    }

    void MeasureCoverage()
    {
        NearRenderers = NearViewRenderers = MissingMaterialRenderers = 0;

        var mission = MissionManager.Instance;
        BusController bus = mission != null ? mission.busController : null;
        if (bus == null) bus = FindAnyObjectByType<BusController>();
        if (bus == null)
        {
            Coverage = "no player focus";
            return;
        }

        Vector3 focus = bus.transform.position;
        Vector3 forward = ViewPriority.ResolveForward(bus.transform);
        float nearSqr = nearRadiusMeters * nearRadiusMeters;
        float viewSqr = fovRadiusMeters * fovRadiusMeters;

        Renderer[] renderers = FindObjectsByType<Renderer>(FindObjectsInactive.Exclude);
        for (int i = 0; i < renderers.Length; i++)
        {
            Renderer renderer = renderers[i];
            if (renderer == null || !renderer.enabled || !renderer.gameObject.activeInHierarchy)
                continue;

            Vector3 center = renderer.bounds.center;
            Vector3 delta = center - focus;
            delta.y = 0f;
            float distanceSqr = delta.sqrMagnitude;

            bool near = distanceSqr <= nearSqr;
            bool inView = distanceSqr <= viewSqr &&
                ViewPriority.IsInsideView(focus, forward, ViewPriority.DefaultFovDegrees + 12f, fovRadiusMeters, center);
            if (!near && !inView) continue;

            if (near) NearRenderers++;
            if (inView) NearViewRenderers++;

            Material[] materials = renderer.sharedMaterials;
            if (materials == null || materials.Length == 0)
            {
                MissingMaterialRenderers++;
                continue;
            }

            for (int m = 0; m < materials.Length; m++)
            {
                if (materials[m] == null)
                {
                    MissingMaterialRenderers++;
                    break;
                }
            }
        }

        int stopCount = stopSpawner != null ? stopSpawner.SpawnedStopCount : 0;
        bool stopPropsComplete = stopSpawner == null || stopSpawner.NearFieldComplete;
        Coverage = $"{NearViewRenderers} in view / {NearRenderers} nearby, " +
                   $"{MissingMaterialRenderers} missing materials, " +
                   $"stopProps={stopCount}{(stopPropsComplete ? string.Empty : " (streaming)")}";
    }
}
