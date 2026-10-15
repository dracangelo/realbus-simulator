using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

public class OSMBuildingMeshBuilder : MonoBehaviour
{
    /// <summary>Palette and height family for a building. Public so the infill streamer can pick one.</summary>
    public enum BuildingKind
    {
        Generic,
        House,
        Commercial,
        School,
        Hospital,
        Apartment
    }

    /// <summary>
    /// The silhouette of a building's roof. A city where every roof is a flat cap
    /// reads as a box model however well the facades are tiled, so houses and
    /// apartments get pitched roofs and the rest stay flat.
    /// </summary>
    public enum RoofShape
    {
        Flat,
        Hip
    }

    sealed class BuildingRecord
    {
        public long id;
        public string label;
        public BuildingKind kind;
        public List<Vector3> footprint;
        public Vector3 center;
        public float height;
    }

    public static OSMBuildingMeshBuilder Instance { get; private set; }

    [Header("Materials")]
    public Material buildingMaterial;
    public Material roofMaterial;

    [Header("Settings")]
    public float defaultBuildingHeight = 10f;
    public float floorHeight = 3f;

    // Build aggressively enough that full-city datasets appear in a reasonable
    // time instead of trickling in over thousands of frames.
    public int maxBuildingsPerFrame = 500;

    public float buildingBaseYOffset = 0.2f;

    // ── PERF: footprints create one LineRenderer + one Material per building.
    //          On 43 k buildings that is ~43 k extra draw calls.  Off by default.
    public bool drawFootprints = false;

    public float footprintWidth = 0.35f;
    public float footprintYOffset = 0.05f;

    // Optional distance culling for low-memory scenes. Disabled by default so
    // the full OSM building set is displayed unless the project opts back in.
    [Header("Distance Culling")]
    public bool enableDistanceCulling = false;
    public float maxBuildingDistance = 1500f;

    [Header("Proximity Streaming")]
    public bool streamAroundVehicle = true;
    [Min(100f)] public float buildingLoadDistance = 600f;
    [Min(120f)] public float buildingUnloadDistance = 800f;
    [Min(50f)] public float streamingCellSize = 200f;
    [Min(0.1f)] public float streamingRefreshSeconds = 0.65f;
    [Min(10)] public int maxBuildingLoadsPerRefresh = 240;

    // Millisecond budget for one slice of lazy district indexing and for mesh
    // creation inside a streaming refresh. Both used to run unbounded inside a
    // single frame, which is what made the world appear to freeze after loading.
    [Min(0.5f)] public float meshBuildBudgetMs = 6f;

    // ── PERF: if a single frame takes longer than this threshold the coroutine
    //          yields immediately, regardless of maxBuildingsPerFrame.
    [Header("Frame-time Budget")]
    [Tooltip("Max milliseconds to spend on buildings per frame (0 = disabled)")]
    public float maxMsPerFrame = 0f;

    [Header("State")]
    public bool buildingsBuilt = false;

    /// <summary>
    /// True once the first streaming ring around the vehicle has been indexed and
    /// its buildings created. The world is playable at this point even though
    /// distant districts are still waiting for lazy indexing.
    /// </summary>
    public bool InitialRingReady { get; private set; }

    /// <summary>Number of background stream cells still waiting for lazy indexing.</summary>
    public int PendingCellCount
    {
        get
        {
            int pending = 0;
            foreach (var cell in deferredWaysByCell.Keys)
                if (!indexedCells.Contains(cell)) pending++;
            return pending;
        }
    }
    private GameObject buildingsParent;
    readonly Dictionary<BuildingKind, Material> materialCache = new Dictionary<BuildingKind, Material>();
    readonly Dictionary<BuildingKind, Material> roofMaterialCache = new Dictionary<BuildingKind, Material>();
    Texture2D defaultFacadeTexture;
    Texture2D defaultRoofTexture;
    // Background buildings are bucketed by stream cell and only resolved into
    // footprints when the vehicle's streaming radius reaches them. Indexing a
    // whole 60k-building city up front could not finish inside a play session.
    readonly Dictionary<Vector2Int, List<OSMWay>> deferredWaysByCell = new Dictionary<Vector2Int, List<OSMWay>>();
    readonly HashSet<Vector2Int> indexedCells = new HashSet<Vector2Int>();
    OSMData activeData;
    float activeMaxDistSq;
    int activeSkippedMissingNodes, activeSkippedInvalidFootprint, activeSkippedRoadOverlap, activeSkippedDistance;
    readonly Dictionary<Vector2Int, List<BuildingRecord>> buildingBuckets = new Dictionary<Vector2Int, List<BuildingRecord>>();
    readonly Dictionary<long, BuildingRecord> recordsById = new Dictionary<long, BuildingRecord>();
    readonly Dictionary<long, GameObject> activeBuildings = new Dictionary<long, GameObject>();
    Transform streamingTarget;
    float streamingTimer;
    bool streamRefreshRunning;
    bool loggedLazyRoadOverlap;
    // City buildings that fell inside a depot yard. Counted rather than logged one
    // line per case: a busy block can straddle a yard edge many times.
    int skippedDepotOverlap;
    bool loggedDepotOverlap;
    [Header("Unmapped Areas")]
    public bool enableProceduralFill = true;
    readonly HashSet<Vector2Int> mappedOccupancy = new HashSet<Vector2Int>();
    readonly Dictionary<Vector2Int, List<List<Vector3>>> mappedPolygons = new Dictionary<Vector2Int, List<List<Vector3>>>();
    readonly Dictionary<GameObject, List<Vector3>> syntheticPolygons = new Dictionary<GameObject, List<Vector3>>();
    const float OccupancyCellSize = 20f;

    /// <summary>
    /// The active building builder, finding it in the scene when nothing has
    /// claimed the role yet. A depot can evict buildings before this component's
    /// Awake has run, and a stale builder must not hide the live one.
    /// </summary>
    public static OSMBuildingMeshBuilder Resolve()
    {
        if (Instance != null) return Instance;
        Instance = FindAnyObjectByType<OSMBuildingMeshBuilder>();
        return Instance;
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        if (enableProceduralFill && GetComponent<ProceduralBuildingFill>() == null)
            gameObject.AddComponent<ProceduralBuildingFill>();
        StartCoroutine(WaitForDataThenBuild());
    }

    void Update()
    {
        if (!streamAroundVehicle || !buildingsBuilt || streamRefreshRunning)
            return;

        streamingTimer -= Time.deltaTime;
        if (streamingTimer > 0f)
            return;

        streamingTimer = Mathf.Max(.1f, streamingRefreshSeconds);
        StartCoroutine(RefreshStreamedBuildings());
    }

    IEnumerator WaitForDataThenBuild()
    {
        while (OSMBuildingLoader.Instance == null || !OSMBuildingLoader.Instance.loadCompleted || GPSManager.Instance == null)
            yield return new WaitForSeconds(0.5f);
        while (OSMRoadMeshBuilder.Instance != null && !OSMRoadMeshBuilder.Instance.roadsBuilt)
            yield return null;

        Debug.Log(
            $"Buildings: Building meshes... " +
            $"(maxBuildingsPerFrame={maxBuildingsPerFrame}, distanceCulling={enableDistanceCulling})");
        yield return StartCoroutine(BuildBuildingsAsync(OSMBuildingLoader.Instance.osmData));
    }

    IEnumerator BuildBuildingsAsync(OSMData data)
    {
        if (buildingsParent != null)
            Destroy(buildingsParent);

        buildingsParent = new GameObject("OSM_Buildings");
        buildingsParent.transform.position = Vector3.zero;
        buildingBuckets.Clear();
        recordsById.Clear();
        activeBuildings.Clear();
        mappedOccupancy.Clear();
        mappedPolygons.Clear();
        syntheticPolygons.Clear();
        buildingsBuilt = false;

        int buildingCount = 0;
        int skippedDistance = 0;
        int skippedInvalidFootprint = 0;
        int skippedMissingNodes = 0;
        int skippedRoadOverlap = 0;
        int frameCount = 0;
        float maxDistSq = maxBuildingDistance * maxBuildingDistance;
        float frameStart = Time.realtimeSinceStartup;

        ResolveStreamingTarget();
        Vector3 focus = streamingTarget != null ? streamingTarget.position : Vector3.zero;
        if (streamingTarget == null && MissionManager.Instance != null && MissionManager.Instance.currentRoute != null &&
            MissionManager.Instance.currentRoute.stops != null && MissionManager.Instance.currentRoute.stops.Length > 0 &&
            GPSManager.Instance != null)
        {
            focus = GPSManager.Instance.GpsToWorld(
                MissionManager.Instance.currentRoute.stops[0].latitude,
                MissionManager.Instance.currentRoute.stops[0].longitude);
        }

        float immediateDistSq = 350f * 350f;
        var immediateWays = new List<OSMWay>();
        var deferredWays = new List<OSMWay>();

        foreach (var way in data.ways)
        {
            if (!way.IsBuilding()) continue;
            if (way.nodeRefs.Count > 0 && data.nodeMap.TryGetValue(way.nodeRefs[0], out var node))
            {
                Vector3 pos = GPSManager.Instance.GpsToWorld(node.lat, node.lon);
                float dx = pos.x - focus.x;
                float dz = pos.z - focus.z;
                if (dx * dx + dz * dz <= immediateDistSq)
                    immediateWays.Add(way);
                else
                    deferredWays.Add(way);
            }
            else
            {
                deferredWays.Add(way);
            }
        }

        // 1. Process and instantiate immediate buildings around the player's bus (Frame 1)
        for (int i = 0; i < immediateWays.Count; i++)
        {
            var record = TryProcessBuildingWay(immediateWays[i], data, maxDistSq, ref skippedMissingNodes, ref skippedInvalidFootprint, ref skippedRoadOverlap, ref skippedDistance);
            if (record == null) continue;

            recordsById[record.id] = record;
            MarkMappedFootprint(record.footprint);
            Vector2Int cell = WorldToStreamingCell(record.center);
            if (!buildingBuckets.TryGetValue(cell, out var bucket))
                buildingBuckets[cell] = bucket = new List<BuildingRecord>();
            bucket.Add(record);

            GameObject instance = CreateBuildingObject(record);
            if (instance != null) activeBuildings[record.id] = instance;
            buildingCount++;
        }

        // Unblock live scenery, pedestrian streaming, and city lifecycle immediately!
        buildingsBuilt = true;
        Debug.Log($"Buildings: Immediate spawn buildings ready ({activeBuildings.Count} loaded around bus). Bus active! Indexing {deferredWays.Count} city buildings in background...");

        activeData = data;
        activeMaxDistSq = maxDistSq;
        activeSkippedMissingNodes = activeSkippedInvalidFootprint = 0;
        activeSkippedRoadOverlap = activeSkippedDistance = 0;
        skippedDepotOverlap = 0;
        loggedDepotOverlap = false;

        if (!streamAroundVehicle)
        {
            // Without streaming the whole dataset has to exist, so keep the
            // original budgeted full build and create every instance.
            for (int i = 0; i < deferredWays.Count; i++)
            {
                var record = TryProcessBuildingWay(deferredWays[i], data, maxDistSq, ref skippedMissingNodes, ref skippedInvalidFootprint, ref skippedRoadOverlap, ref skippedDistance);
                if (record != null)
                {
                    RegisterRecord(record);
                    CreateBuildingObject(record);
                    buildingCount++;
                }

                frameCount++;
                bool batchFull = frameCount >= maxBuildingsPerFrame;
                bool overBudget = (Time.realtimeSinceStartup - frameStart) * 1000f >= Mathf.Max(3f, maxMsPerFrame > 0f ? maxMsPerFrame : 4f);
                if (batchFull || overBudget)
                {
                    frameCount = 0;
                    frameStart = Time.realtimeSinceStartup;
                    yield return null;
                }
            }

            if (skippedRoadOverlap > 0) Debug.Log($"Buildings: Omitted {skippedRoadOverlap} footprints overlapping playable roads; source files unchanged.");
            LogDepotOverlapOnce();
            Debug.Log($"Buildings: Indexed {buildingCount} buildings; {activeBuildings.Count} loaded near the vehicle.");
            yield break;
        }

        // 2. Bucket the remaining city buildings by stream cell. Their footprints
        //    stay unresolved until the vehicle's streaming radius reaches them
        //    (see IndexCellsAround), so distant districts cost nothing until the
        //    player actually drives towards them.
        deferredWaysByCell.Clear();
        indexedCells.Clear();
        int unmappedWays = 0;
        foreach (var way in deferredWays)
        {
            if (way == null || way.nodeRefs == null || way.nodeRefs.Count == 0)
                continue;
            if (!data.nodeMap.TryGetValue(way.nodeRefs[0], out var firstNode))
            {
                unmappedWays++;
                continue;
            }

            Vector3 firstPos = GPSManager.Instance.GpsToWorld(firstNode.lat, firstNode.lon);
            Vector2Int cell = WorldToStreamingCell(firstPos);
            if (!deferredWaysByCell.TryGetValue(cell, out var cellWays))
                deferredWaysByCell[cell] = cellWays = new List<OSMWay>();
            cellWays.Add(way);
        }

        if (skippedRoadOverlap > 0) Debug.Log($"Buildings: Omitted {skippedRoadOverlap} footprints overlapping playable roads; source files unchanged.");
        LogDepotOverlapOnce();
        yield return StartCoroutine(RefreshStreamedBuildings());
        Debug.Log(
            $"Buildings: Near ring ready ({activeBuildings.Count} loaded around the vehicle); " +
            $"{deferredWaysByCell.Count} background cells queue for lazy indexing" +
            (unmappedWays > 0 ? $" ({unmappedWays} ways lack a usable first node)." : "."));
    }

    void RegisterRecord(BuildingRecord record)
    {
        recordsById[record.id] = record;
        MarkMappedFootprint(record.footprint);
        Vector2Int cell = WorldToStreamingCell(record.center);
        if (!buildingBuckets.TryGetValue(cell, out var bucket))
            buildingBuckets[cell] = bucket = new List<BuildingRecord>();
        bucket.Add(record);
    }

    BuildingRecord TryProcessBuildingWay(OSMWay way, OSMData data, float maxDistSq,
        ref int skippedMissingNodes, ref int skippedInvalidFootprint, ref int skippedRoadOverlap, ref int skippedDistance)
    {
        List<Vector3> footprint = new List<Vector3>();

        foreach (long nodeRef in way.nodeRefs)
        {
            if (!data.nodeMap.TryGetValue(nodeRef, out var node))
            {
                skippedMissingNodes++;
                continue;
            }

            Vector3 worldPos = GPSManager.Instance.GpsToWorld(node.lat, node.lon);
            worldPos.y = buildingBaseYOffset;
            footprint.Add(worldPos);
        }

        if (footprint.Count < 3)
        {
            skippedInvalidFootprint++;
            return null;
        }

        // Remove duplicate last point if closed polygon
        if (footprint.Count > 1 && Vector3.Distance(footprint[0], footprint[footprint.Count - 1]) < 0.1f)
            footprint.RemoveAt(footprint.Count - 1);

        if (footprint.Count < 3)
        {
            skippedInvalidFootprint++;
            return null;
        }

        float signedArea = GetSignedAreaXZ(footprint);
        if (Mathf.Abs(signedArea) < 0.01f)
        {
            skippedInvalidFootprint++;
            return null;
        }

        if (signedArea < 0f)
            footprint.Reverse();

        if (OSMRoadMeshBuilder.Instance?.SurfaceIndex?.OverlapsFootprint(footprint, .75f) == true)
        {
            skippedRoadOverlap++;
            return null;
        }

        // A depot yard is a place the player parks and manoeuvres in. No city
        // building may stand there, so the whole footprint is rejected - the same
        // rule as a playable road, and applied before the record is ever filed.
        if (BusDepotManager.OverlapsDepotFootprint(footprint, .75f))
        {
            skippedDepotOverlap++;
            return null;
        }

        if (enableDistanceCulling)
        {
            Vector3 centroid = Vector3.zero;
            foreach (var p in footprint) centroid += p;
            centroid /= footprint.Count;
            float distSq = centroid.x * centroid.x + centroid.z * centroid.z;
            if (distSq > maxDistSq)
            {
                skippedDistance++;
                return null;
            }
        }

        var buildingKind = ResolveBuildingKind(way);
        Vector3 center = Vector3.zero;
        for (int i = 0; i < footprint.Count; i++) center += footprint[i];
        center /= footprint.Count;

        return new BuildingRecord
        {
            id = way.id,
            label = ResolveBuildingLabel(way, buildingKind),
            kind = buildingKind,
            footprint = footprint,
            center = center,
            height = ResolveBuildingHeight(way, buildingKind)
        };
    }

    IEnumerator RefreshStreamedBuildings()
    {
        streamRefreshRunning = true;
        ResolveStreamingTarget();
        if (streamingTarget == null)
        {
            streamRefreshRunning = false;
            yield break;
        }

        Vector3 target = streamingTarget.position;
        target.y = 0f;
        float unloadSq = Mathf.Max(buildingLoadDistance + 50f, buildingUnloadDistance);
        unloadSq *= unloadSq;
        var unloadIds = new List<long>();
        foreach (var pair in activeBuildings)
        {
            if (!recordsById.TryGetValue(pair.Key, out var record) || HorizontalSqrDistance(record.center, target) > unloadSq)
            {
                DestroyBuildingObject(pair.Value);
                unloadIds.Add(pair.Key);
            }
        }
        for (int i = 0; i < unloadIds.Count; i++) activeBuildings.Remove(unloadIds[i]);

        float cellSize = Mathf.Max(50f, streamingCellSize);
        int radius = Mathf.CeilToInt(buildingLoadDistance / cellSize);
        Vector2Int centerCell = WorldToStreamingCell(target);

        // Lazily resolve the districts the streaming radius now covers. Indexing
        // is sliced by a frame budget, so driving into a dense district cannot
        // stall the main thread the way a whole-city index did.
        yield return StartCoroutine(IndexCellsAround(centerCell, radius + 1));

        float loadSq = buildingLoadDistance * buildingLoadDistance;
        var candidates = new List<BuildingRecord>();

        for (int dz = -radius; dz <= radius; dz++)
        for (int dx = -radius; dx <= radius; dx++)
        {
            Vector2Int cell = new Vector2Int(centerCell.x + dx, centerCell.y + dz);
            if (!buildingBuckets.TryGetValue(cell, out var bucket)) continue;
            for (int i = 0; i < bucket.Count; i++)
            {
                BuildingRecord record = bucket[i];
                if (activeBuildings.ContainsKey(record.id) || HorizontalSqrDistance(record.center, target) > loadSq)
                    continue;
                candidates.Add(record);
            }
        }

        candidates.Sort((a, b) => HorizontalSqrDistance(a.center, target).CompareTo(HorizontalSqrDistance(b.center, target)));

        // Create the nearest missing buildings, but never more than a slice per
        // frame. Producing every candidate in one frame was the visible hitch
        // that repeated every streamingRefreshSeconds.
        int perFrameCap = Mathf.Max(1, maxBuildingLoadsPerRefresh / 6);
        int builtThisFrame = 0;
        float sliceDeadline = Time.realtimeSinceStartup + Mathf.Max(0.001f, meshBuildBudgetMs / 1000f);
        for (int i = 0; i < candidates.Count; i++)
        {
            BuildingRecord record = candidates[i];
            GameObject instance = CreateBuildingObject(record);
            if (instance != null) activeBuildings[record.id] = instance;

            builtThisFrame++;
            if (builtThisFrame >= perFrameCap || Time.realtimeSinceStartup >= sliceDeadline)
            {
                builtThisFrame = 0;
                sliceDeadline = Time.realtimeSinceStartup + Mathf.Max(0.001f, meshBuildBudgetMs / 1000f);
                yield return null;
            }
        }

        InitialRingReady = true;
        streamRefreshRunning = false;
        yield break;
    }

    /// <summary>
    /// Resolves footprints for background cells inside the streaming radius.
    /// Each cell is indexed at most once and the work is sliced by a frame
    /// budget, so the cost tracks how far the player has actually driven rather
    /// than the size of the source dataset.
    /// </summary>
    IEnumerator IndexCellsAround(Vector2Int centerCell, int radius)
    {
        if (activeData == null || deferredWaysByCell.Count == 0)
            yield break;

        int processed = 0;
        float sliceDeadline = Time.realtimeSinceStartup + Mathf.Max(0.001f, meshBuildBudgetMs / 1000f);
        for (int dz = -radius; dz <= radius; dz++)
        for (int dx = -radius; dx <= radius; dx++)
        {
            var cell = new Vector2Int(centerCell.x + dx, centerCell.y + dz);
            if (!indexedCells.Add(cell))
                continue;
            if (!deferredWaysByCell.TryGetValue(cell, out var cellWays))
                continue;

            for (int i = 0; i < cellWays.Count; i++)
            {
                var record = TryProcessBuildingWay(cellWays[i], activeData, activeMaxDistSq,
                    ref activeSkippedMissingNodes, ref activeSkippedInvalidFootprint,
                    ref activeSkippedRoadOverlap, ref activeSkippedDistance);
                if (record == null)
                    continue;

                RegisterRecord(record);
            }

            processed++;
            if (Time.realtimeSinceStartup >= sliceDeadline)
            {
                sliceDeadline = Time.realtimeSinceStartup + Mathf.Max(0.001f, meshBuildBudgetMs / 1000f);
                yield return null;
            }
        }

        if (processed > 0 && activeSkippedRoadOverlap > 0 && !loggedLazyRoadOverlap)
        {
            loggedLazyRoadOverlap = true;
            Debug.Log($"Buildings: Omitted {activeSkippedRoadOverlap} footprints overlapping playable roads; source files unchanged.");
        }
        LogDepotOverlapOnce();
    }

    /// <summary>
    /// Reports city buildings dropped because a depot yard occupies their ground,
    /// once per session so the streamer's log stays readable.
    /// </summary>
    void LogDepotOverlapOnce()
    {
        if (loggedDepotOverlap || skippedDepotOverlap <= 0) return;
        loggedDepotOverlap = true;
        Debug.Log($"Buildings: Omitted {skippedDepotOverlap} footprints inside a depot yard; the yard roads stay clear.");
    }

    GameObject CreateBuildingObject(BuildingRecord record)
    {
        Mesh mesh = BuildExtrudedMesh(record.footprint, record.height, FacadeTilesFor(record.height, floorHeight),
            RoofShapeFor(record.kind, record.id));
        if (mesh == null) return null;
        // Synthetic buildings (fictional infill, and the depot's own clearing test
        // path) can be requested before the mapped build pass has made its parent.
        if (buildingsParent == null)
        {
            buildingsParent = new GameObject("OSM_Buildings");
            buildingsParent.transform.position = Vector3.zero;
        }
        GameObject buildingObj = new GameObject($"Building_{record.label}_{record.id}");
        buildingObj.transform.SetParent(buildingsParent.transform, false);
        var mf = buildingObj.AddComponent<MeshFilter>();
        var mr = buildingObj.AddComponent<MeshRenderer>();
        mf.sharedMesh = mesh;
        mr.sharedMaterials = new[] { ResolveBuildingMaterial(record.kind), ResolveRoofMaterial(record.kind) };
        mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        mr.receiveShadows = true;
        // A per-building tint in a property block: a street of identical facades is the
        // other half of "every building looks the same". Shared materials keep the
        // renderer batched, and the tint is stable for a given building id.
        var block = new MaterialPropertyBlock();
        Color tint = TintFor(PaletteFor(record.kind, record.id), record.id);
        block.SetColor("_Color", tint);
        mr.SetPropertyBlock(block);
        if (drawFootprints)
            BuildFootprintOutline(record.footprint, buildingObj.transform, PaletteFor(record.kind, record.id));
        return buildingObj;
    }

    void MarkMappedFootprint(List<Vector3> footprint)
    {
        Vector3 min = footprint[0], max = min;
        foreach (var point in footprint) { min = Vector3.Min(min, point); max = Vector3.Max(max, point); }
        for (int x = Mathf.FloorToInt(min.x / OccupancyCellSize); x <= Mathf.FloorToInt(max.x / OccupancyCellSize); x++)
        for (int z = Mathf.FloorToInt(min.z / OccupancyCellSize); z <= Mathf.FloorToInt(max.z / OccupancyCellSize); z++)
        {
            var key = new Vector2Int(x, z);
            mappedOccupancy.Add(key);
            if (!mappedPolygons.TryGetValue(key, out var polygons)) mappedPolygons[key] = polygons = new List<List<Vector3>>();
            polygons.Add(footprint);
        }
    }

    // Precise clearance for street furniture; coarse 20m occupancy is intentionally
    // retained for fictional building infill, but would reject whole sidewalks.
    public bool OverlapsBuilding(Vector3 center, float radius)
    {
        for (int x = Mathf.FloorToInt((center.x - radius) / OccupancyCellSize); x <= Mathf.FloorToInt((center.x + radius) / OccupancyCellSize); x++)
        for (int z = Mathf.FloorToInt((center.z - radius) / OccupancyCellSize); z <= Mathf.FloorToInt((center.z + radius) / OccupancyCellSize); z++)
            if (mappedPolygons.TryGetValue(new Vector2Int(x, z), out var polygons))
                foreach (var polygon in polygons)
                    if (LiveCityStreamer.OverlapsPolygon(center, radius, polygon)) return true;
        foreach (var polygon in syntheticPolygons.Values)
            if (LiveCityStreamer.OverlapsPolygon(center, radius, polygon)) return true;
        return false;
    }

    public bool HasMappedFootprint(Vector3 center, float radius)
    {
        for (int x = Mathf.FloorToInt((center.x - radius) / OccupancyCellSize); x <= Mathf.FloorToInt((center.x + radius) / OccupancyCellSize); x++)
        for (int z = Mathf.FloorToInt((center.z - radius) / OccupancyCellSize); z <= Mathf.FloorToInt((center.z + radius) / OccupancyCellSize); z++)
            if (mappedOccupancy.Contains(new Vector2Int(x, z))) return true;
        return false;
    }

    public GameObject CreateSyntheticBuilding(List<Vector3> footprint, float height, long id, BuildingKind kind = BuildingKind.House)
    {
        if (OSMRoadMeshBuilder.Instance?.SurfaceIndex?.OverlapsFootprint(footprint, .75f) == true) return null;
        if (BusDepotManager.OverlapsDepotFootprint(footprint, .75f)) return null;
        if (GetSignedAreaXZ(footprint) < 0f) footprint.Reverse();
        var instance = CreateBuildingObject(new BuildingRecord {
            id = id, label = "Synthetic", kind = kind, footprint = footprint, height = height
        });
        if (instance != null) syntheticPolygons[instance] = footprint;
        return instance;
    }

    public void ReleaseSyntheticBuilding(GameObject building) { DestroyBuildingObject(building); }

    /// <summary>
    /// Destroys mapped and synthetic buildings standing inside any registered
    /// depot yard and drops their records, so the streaming pass cannot put them
    /// back when the player next drives past the depot.
    /// </summary>
    public int ClearDepotOverlaps(float padding = 1f)
    {
        int removed = 0;
        var doomed = new List<long>();
        foreach (var pair in recordsById)
            if (pair.Value != null && BusDepotManager.OverlapsDepotFootprint(pair.Value.footprint, padding))
                doomed.Add(pair.Key);

        foreach (long id in doomed)
        {
            if (activeBuildings.TryGetValue(id, out var instance))
            {
                DestroyBuildingObject(instance);
                activeBuildings.Remove(id);
                removed++;
            }
            recordsById.Remove(id);
        }

        foreach (var bucket in buildingBuckets.Values)
            bucket.RemoveAll(record => record == null || BusDepotManager.OverlapsDepotFootprint(record.footprint, padding));

        var synthetic = new List<GameObject>();
        foreach (var pair in syntheticPolygons)
            if (BusDepotManager.OverlapsDepotFootprint(pair.Value, padding)) synthetic.Add(pair.Key);
        foreach (var building in synthetic)
        {
            DestroyBuildingObject(building);
            removed++;
        }

        return removed;
    }

    void DestroyBuildingObject(GameObject building)
    {
        if (building == null) return;
        syntheticPolygons.Remove(building);
        MeshFilter filter = building.GetComponent<MeshFilter>();
        if (filter != null && filter.sharedMesh != null) DestroySafe(filter.sharedMesh);
        DestroySafe(building);
    }

    /// <summary>
    /// Destroys in both play and edit mode. The depot clears buildings while a
    /// yard is placed, and an EditMode test does exactly that outside play mode,
    /// where <see cref="Object.Destroy"/> is not legal.
    /// </summary>
    static void DestroySafe(Object obj)
    {
        if (obj == null) return;
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            Object.DestroyImmediate(obj);
            return;
        }
#endif
        Object.Destroy(obj);
    }

    Vector2Int WorldToStreamingCell(Vector3 position)
    {
        float size = Mathf.Max(50f, streamingCellSize);
        return new Vector2Int(Mathf.FloorToInt(position.x / size), Mathf.FloorToInt(position.z / size));
    }

    static float HorizontalSqrDistance(Vector3 a, Vector3 b)
    {
        float x = a.x - b.x;
        float z = a.z - b.z;
        return x * x + z * z;
    }

    void ResolveStreamingTarget()
    {
        if (streamingTarget != null && streamingTarget.gameObject.activeInHierarchy)
            return;
        BusController bus = FindAnyObjectByType<BusController>();
        streamingTarget = bus != null ? bus.transform : Camera.main != null ? Camera.main.transform : null;
    }

    float GetSignedAreaXZ(List<Vector3> footprint)
    {
        if (footprint == null || footprint.Count < 3)
            return 0f;

        float areaTwice = 0f;
        for (int i = 0; i < footprint.Count; i++)
        {
            int next = (i + 1) % footprint.Count;
            areaTwice += footprint[i].x * footprint[next].z;
            areaTwice -= footprint[next].x * footprint[i].z;
        }

        return areaTwice * 0.5f;
    }

    float ResolveBuildingHeight(OSMWay way, BuildingKind kind)
    {
        float height;

        if (way.tags.TryGetValue("building:levels", out var levelsRaw) && int.TryParse(levelsRaw, out int levels))
            height = Mathf.Max(floorHeight, levels * floorHeight);
        else if (way.tags.TryGetValue("height", out var heightRaw))
        {
            string cleaned = heightRaw.Replace("m", "").Trim();
            if (float.TryParse(cleaned, NumberStyles.Float, CultureInfo.InvariantCulture, out float parsedHeight))
                height = Mathf.Max(floorHeight, parsedHeight);
            else
                height = EstimateUntaggedHeight(way, kind);
        }
        else
            height = EstimateUntaggedHeight(way, kind);

        return height;
    }

    float EstimateUntaggedHeight(OSMWay way, BuildingKind kind)
    {
        // Most OSM footprints do not include levels. Stable variation avoids an
        // artificial city where every building is exactly ten metres tall.
        float variation = Mathf.Abs((way.id * 1103515245L + 12345L) % 1000L) / 999f;
        switch (kind)
        {
            case BuildingKind.House: return Mathf.Lerp(5.5f, 13f, variation);
            case BuildingKind.Apartment: return Mathf.Lerp(11f, 27f, variation);
            // A minority of footprints are towers: without them a mapped city reads as
            // one flat skyline band, because most OSM ways carry no height tag.
            case BuildingKind.Commercial:
                return variation > .82f
                    ? Mathf.Lerp(34f, 58f, (variation - .82f) / .18f)
                    : Mathf.Lerp(10f, 30f, variation);
            case BuildingKind.School: return Mathf.Lerp(7f, 16f, variation);
            case BuildingKind.Hospital: return Mathf.Lerp(12f, 26f, variation);
            default: return Mathf.Lerp(Mathf.Max(6f, defaultBuildingHeight * .7f), defaultBuildingHeight * 1.8f, variation);
        }
    }

    BuildingKind ResolveBuildingKind(OSMWay way)
    {
        if (way.tags.TryGetValue("amenity", out var amenity))
        {
            if (amenity == "school" || amenity == "college" || amenity == "university")
                return BuildingKind.School;
            if (amenity == "hospital" || amenity == "clinic" || amenity == "doctors")
                return BuildingKind.Hospital;
        }

        if (way.tags.TryGetValue("building", out var building))
        {
            switch (building)
            {
                case "house":
                case "detached":
                case "residential":
                case "bungalow":
                case "terrace":
                    return BuildingKind.House;
                case "apartments":
                case "apartment":
                case "dormitory":
                case "residential_building":
                    return BuildingKind.Apartment;
                case "commercial":
                case "retail":
                case "office":
                    return BuildingKind.Commercial;
                case "school":
                    return BuildingKind.School;
                case "hospital":
                    return BuildingKind.Hospital;
            }
        }

        if (way.tags.ContainsKey("shop") || way.tags.ContainsKey("office"))
            return BuildingKind.Commercial;

        return BuildingKind.Generic;
    }

    string ResolveBuildingLabel(OSMWay way, BuildingKind buildingKind)
    {
        if (way.tags != null)
        {
            if (way.tags.TryGetValue("name", out var name) && !string.IsNullOrWhiteSpace(name))
                return SanitizeName(name);

            if (way.tags.TryGetValue("building", out var building) && !string.IsNullOrWhiteSpace(building))
                return SanitizeName(building);
        }

        return buildingKind.ToString();
    }

    string SanitizeName(string raw)
    {
        return raw.Replace(" ", "_").Replace("/", "_");
    }

    Shader ResolveDefaultBuildingShader() => BuiltInShaders.Lit;

    Material ResolveBuildingMaterial(BuildingKind kind)
    {
        if (buildingMaterial != null)
            return buildingMaterial;

        if (materialCache.TryGetValue(kind, out var cached) && cached != null)
            return cached;

        var mat = new Material(ResolveDefaultBuildingShader());
        mat.color = ResolveBuildingColor(kind);
        if (defaultFacadeTexture == null)
            defaultFacadeTexture = Resources.Load<Texture2D>("MapboxStyles/Styles/MapboxSampleStyles/Realistic/Assets/Textures/RealisticSideAlbedo");
        if (defaultFacadeTexture != null)
        {
            defaultFacadeTexture.wrapMode = TextureWrapMode.Repeat;
            mat.mainTexture = defaultFacadeTexture;
            mat.mainTextureScale = new Vector2(1f, 1.2f);
        }
        if (mat.HasProperty("_Glossiness")) mat.SetFloat("_Glossiness", 0.05f);
        if (mat.HasProperty("_Metallic")) mat.SetFloat("_Metallic", 0f);
        if (mat.HasProperty("_Smoothness")) mat.SetFloat("_Smoothness", 0.08f);
        materialCache[kind] = mat;
        return mat;
    }

    Material ResolveRoofMaterial(BuildingKind kind)
    {
        if (roofMaterial != null)
            return roofMaterial;
        if (roofMaterialCache.TryGetValue(kind, out var cached) && cached != null)
            return cached;

        var mat = new Material(ResolveDefaultBuildingShader());
        mat.color = Color.Lerp(ResolveBuildingColor(kind), new Color(.16f, .17f, .18f, 1f), .48f);
        if (defaultRoofTexture == null)
            defaultRoofTexture = Resources.Load<Texture2D>("MapboxStyles/Styles/MapboxSampleStyles/Realistic/Assets/Textures/RealisticTopAlbedo");
        if (defaultRoofTexture != null)
        {
            defaultRoofTexture.wrapMode = TextureWrapMode.Repeat;
            mat.mainTexture = defaultRoofTexture;
        }
        if (mat.HasProperty("_Smoothness")) mat.SetFloat("_Smoothness", .04f);
        roofMaterialCache[kind] = mat;
        return mat;
    }

    Color ResolveBuildingColor(BuildingKind kind) => PaletteFor(kind, 0L);

    // Several base colours per kind. One colour per kind meant that every house in
    // the city shared a wall tone and the per-building tint could only nudge its
    // brightness, which is what made whole districts look like one building.
    static readonly Color[] HousePalette =
    {
        new Color(0.86f, 0.75f, 0.62f, 1f), new Color(0.78f, 0.63f, 0.50f, 1f),
        new Color(0.90f, 0.87f, 0.80f, 1f), new Color(0.70f, 0.72f, 0.66f, 1f),
        new Color(0.82f, 0.66f, 0.52f, 1f)
    };
    static readonly Color[] ApartmentPalette =
    {
        new Color(0.80f, 0.76f, 0.70f, 1f), new Color(0.66f, 0.69f, 0.72f, 1f),
        new Color(0.85f, 0.80f, 0.72f, 1f), new Color(0.72f, 0.68f, 0.62f, 1f)
    };
    static readonly Color[] CommercialPalette =
    {
        new Color(0.59f, 0.74f, 0.9f, 1f), new Color(0.66f, 0.80f, 0.86f, 1f),
        new Color(0.47f, 0.58f, 0.70f, 1f), new Color(0.78f, 0.78f, 0.76f, 1f),
        new Color(0.62f, 0.62f, 0.60f, 1f)
    };
    static readonly Color[] GenericPalette =
    {
        new Color(0.76f, 0.79f, 0.84f, 1f), new Color(0.68f, 0.71f, 0.72f, 1f),
        new Color(0.83f, 0.81f, 0.78f, 1f), new Color(0.74f, 0.70f, 0.66f, 1f)
    };

    /// <summary>
    /// The wall colour a building draws from its kind's palette. Stable for an id, so
    /// a building keeps its look between loads, and public so tests can check that a
    /// street is not one repeated tone.
    /// </summary>
    public static Color PaletteFor(BuildingKind kind, long id)
    {
        switch (kind)
        {
            case BuildingKind.House: return Pick(HousePalette, id);
            case BuildingKind.Apartment: return Pick(ApartmentPalette, id);
            case BuildingKind.Commercial: return Pick(CommercialPalette, id);
            case BuildingKind.School: return new Color(0.95f, 0.72f, 0.26f, 1f);
            case BuildingKind.Hospital: return new Color(0.87f, 0.35f, 0.38f, 1f);
            default: return Pick(GenericPalette, id);
        }
    }

    static Color Pick(Color[] palette, long id)
    {
        long mixed = id * 6364136223846793005L + 1442695040888963407L;
        long index = mixed % palette.Length;
        if (index < 0) index += palette.Length;
        return palette[(int)index];
    }

    /// <summary>
    /// The roof a building gets. Houses and apartments are pitched often enough to
    /// break the roofline, while shops, schools and the generic fill stay flat
    /// because that is what their real counterparts look like.
    /// </summary>
    public static RoofShape RoofShapeFor(BuildingKind kind, long id)
    {
        int roll = (int)Mathf.Abs((id * 2246822519L + 374761393L) % 100L);
        switch (kind)
        {
            case BuildingKind.House: return roll < 70 ? RoofShape.Hip : RoofShape.Flat;
            case BuildingKind.Apartment: return roll < 45 ? RoofShape.Hip : RoofShape.Flat;
            default: return RoofShape.Flat;
        }
    }

    void BuildFootprintOutline(List<Vector3> footprint, Transform parent, Color color)
    {
        if (footprint == null || footprint.Count < 2)
            return;

        var go = new GameObject("Footprint");
        go.transform.SetParent(parent, false);

        var lr = go.AddComponent<LineRenderer>();
        lr.useWorldSpace = true;
        lr.loop = true;
        lr.positionCount = footprint.Count;
        lr.startWidth = footprintWidth;
        lr.endWidth = footprintWidth;
        lr.numCapVertices = 2;
        lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lr.receiveShadows = false;

        var mat = new Material(ResolveDefaultBuildingShader());
        mat.color = color;
        mat.renderQueue = 2450;
        lr.material = mat;

        var positions = new Vector3[footprint.Count];
        for (int i = 0; i < footprint.Count; i++)
        {
            positions[i] = footprint[i] + Vector3.up * footprintYOffset;
        }

        lr.SetPositions(positions);
    }

    /// <summary>
    /// How many facade tiles a wall carries vertically, one per floor. The old
    /// mapping stretched a single window band over the whole wall, so a 30 m block
    /// and a 6 m house looked identical from the street.
    /// </summary>
    public static int FacadeTilesFor(float height, float floorHeight)
        => Mathf.Clamp(Mathf.RoundToInt(Mathf.Max(1f, height) / Mathf.Max(1.5f, floorHeight)), 1, 16);

    /// <summary>
    /// A stable per-building variation of the material's own colour: same palette,
    /// different building. Brightness and a slight warm/cool shift are enough to
    /// break up a street without looking painted at random.
    /// </summary>
    public static Color TintFor(Color baseColour, long id)
    {
        float variation = Mathf.Abs((id * 2654435761L + 1013904223L) % 1000L) / 999f;
        float warmth = Mathf.Abs((id * 40503L + 7L) % 997L) / 996f;
        float brightness = Mathf.Lerp(0.84f, 1.14f, variation);
        float shift = Mathf.Lerp(-0.035f, 0.045f, warmth);
        return new Color(
            Mathf.Clamp01(baseColour.r * brightness + shift),
            Mathf.Clamp01(baseColour.g * brightness),
            Mathf.Clamp01(baseColour.b * brightness - shift),
            baseColour.a);
    }

    /// <summary>How far a roof is pulled in from the wall top, and how far it drops: the parapet.</summary>
    const float RoofLedgeInset = 0.7f;
    const float RoofLedgeDrop = 0.35f;

    /// <summary>Pulls a roof vertex toward the footprint centroid, so the wall top reads as a parapet.</summary>
    static Vector3 InsetRoofPoint(Vector3 point, Vector3 center, float y, float inset)
    {
        Vector3 flat = new Vector3(center.x - point.x, 0f, center.z - point.z);
        float distance = flat.magnitude;
        if (distance > 0.001f) point += flat / distance * Mathf.Min(inset, distance * 0.25f);
        point.y = y;
        return point;
    }

    Mesh BuildExtrudedMesh(List<Vector3> footprint, float height, int facadeTiles = 1, RoofShape roofShape = RoofShape.Flat)
    {
        int n = footprint.Count;
        if (n < 3) return null;
        facadeTiles = Mathf.Clamp(facadeTiles, 1, 16);

        List<Vector3> verts = new List<Vector3>();
        List<int> wallTris = new List<int>();
        List<int> roofTris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        // Walls
        for (int i = 0; i < n; i++)
        {
            int next = (i + 1) % n;

            Vector3 bl = footprint[i];
            Vector3 br = footprint[next];
            Vector3 tl = footprint[i] + Vector3.up * height;
            Vector3 tr = footprint[next] + Vector3.up * height;

            int idx = verts.Count;
            verts.Add(bl); verts.Add(br);
            verts.Add(tl); verts.Add(tr);

            // One tile per floor vertically, and one every ~3 m along the wall, so the
            // window rhythm stays the same whatever the building's size.
            float segLen = Vector3.Distance(bl, br) / Mathf.Max(1.5f, floorHeight);
            uvs.Add(new Vector2(0, 0));
            uvs.Add(new Vector2(segLen, 0));
            uvs.Add(new Vector2(0, facadeTiles));
            uvs.Add(new Vector2(segLen, facadeTiles));

            wallTris.Add(idx); wallTris.Add(idx + 2); wallTris.Add(idx + 1);
            wallTris.Add(idx + 1); wallTris.Add(idx + 2); wallTris.Add(idx + 3);
        }

        // Pitched roofs where the footprint allows one, flat parapets everywhere else.
        if (roofShape != RoofShape.Hip || !AddHipRoof(footprint, height, verts, uvs, roofTris))
            AddFlatRoof(footprint, height, verts, uvs, roofTris);

        Mesh mesh = new Mesh();
        mesh.name = "Building";
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.vertices = verts.ToArray();
        mesh.subMeshCount = 2;
        mesh.SetTriangles(wallTris, 0);
        mesh.SetTriangles(roofTris, 1);
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }

    /// <summary>
    /// Flat roof using fan triangulation, sitting inset and just below the wall top
    /// so every building shows a parapet edge instead of a cap flush with the facade.
    /// </summary>
    static void AddFlatRoof(List<Vector3> footprint, float height, List<Vector3> verts, List<Vector2> uvs, List<int> tris)
    {
        int n = footprint.Count;
        int roofBase = verts.Count;
        Vector3 center = Vector3.zero;
        foreach (var p in footprint) center += p;
        center /= n;
        float roofY = Mathf.Max(0.5f, height - RoofLedgeDrop);
        center.y = roofY;

        verts.Add(center);
        uvs.Add(new Vector2(0.5f, 0.5f));

        for (int i = 0; i < n; i++)
        {
            int next = (i + 1) % n;
            verts.Add(InsetRoofPoint(footprint[i], center, roofY, RoofLedgeInset));
            verts.Add(InsetRoofPoint(footprint[next], center, roofY, RoofLedgeInset));
            uvs.Add(new Vector2(0, 0));
            uvs.Add(new Vector2(1, 0));

            int ci = roofBase;
            int ai = roofBase + 1 + i * 2;
            int bi = roofBase + 1 + i * 2 + 1;

            tris.Add(ci); tris.Add(ai); tris.Add(bi);
        }
    }

    /// <summary>
    /// Adds a pitched roof over a rectangular footprint: two sloped planes meeting at
    /// a ridge, with hipped ends. This is the single biggest thing separating a street
    /// of houses from a row of flat-topped boxes.
    ///
    /// Returns false, leaving the mesh untouched, for any footprint that is not a
    /// rectangle, so an irregular mapped building keeps its flat roof instead of
    /// growing a lopsided one.
    /// </summary>
    static bool AddHipRoof(List<Vector3> footprint, float height, List<Vector3> verts, List<Vector2> uvs, List<int> tris)
    {
        if (footprint == null || footprint.Count != 4) return false;

        Vector3 center = Vector3.zero;
        foreach (var p in footprint) center += p;
        center /= 4f;

        Vector3 e0 = footprint[1] - footprint[0];
        Vector3 e1 = footprint[2] - footprint[1];
        e0.y = e1.y = 0f;
        float len0 = e0.magnitude, len1 = e1.magnitude;
        if (len0 < .01f || len1 < .01f) return false;

        Vector3 along = (len0 >= len1 ? e0 : e1).normalized;
        Vector3 across = (len0 >= len1 ? e1 : e0).normalized;
        float spanAlong = len0 >= len1 ? len0 : len1;
        float spanAcross = len0 >= len1 ? len1 : len0;

        // Only a rectangle has axes a ridge can follow, and only a believable roof
        // pitch fits on a shape at least as deep as it is wide.
        if (Mathf.Abs(Vector3.Dot(along, across)) > .08f) return false;
        if (spanAcross < 3f || spanAlong < spanAcross * .95f) return false;
        // Keep the handedness fixed so the roof faces wind the way the walls do.
        if (Vector3.Cross(along, across).y > 0f) across = -across;

        float eaveY = Mathf.Max(1f, height);
        float rise = Mathf.Clamp(spanAcross * .42f, 1f, 3.6f);
        float halfAlong = spanAlong * .5f, halfAcross = spanAcross * .5f;
        // Hipping the ends: the ridge is shorter than the eaves by half the depth.
        float ridgeHalf = Mathf.Max(.05f, halfAlong - halfAcross);

        Vector3 low = center - along * halfAlong, high = center + along * halfAlong;
        Vector3 lowFront = low - across * halfAcross, lowBack = low + across * halfAcross;
        Vector3 highBack = high + across * halfAcross, highFront = high - across * halfAcross;
        Vector3 ridgeLow = center - along * ridgeHalf + Vector3.up * (eaveY + rise);
        Vector3 ridgeHigh = center + along * ridgeHalf + Vector3.up * (eaveY + rise);

        int baseIndex = verts.Count;
        Add(baseIndex + 0, lowFront, new Vector2(0, 0));
        Add(baseIndex + 1, lowBack, new Vector2(1, 0));
        Add(baseIndex + 2, highBack, new Vector2(1, 1));
        Add(baseIndex + 3, highFront, new Vector2(0, 1));
        Add(baseIndex + 4, ridgeLow, new Vector2(.5f, .5f));
        Add(baseIndex + 5, ridgeHigh, new Vector2(.5f, .5f));

        Face(1, 2, 5); Face(1, 5, 4);   // back plane, eave to ridge
        Face(0, 4, 5); Face(0, 5, 3);   // front plane
        Face(2, 3, 5);                  // hip at the high end
        Face(0, 1, 4);                  // hip at the low end
        return true;

        void Add(int index, Vector3 position, Vector2 uv)
        {
            // The eave ring is lifted to the wall top; the ridge already carries its rise.
            float y = index >= baseIndex + 4 ? position.y : eaveY;
            verts.Add(new Vector3(position.x, y, position.z));
            uvs.Add(uv);
        }

        void Face(int a, int b, int c)
        {
            tris.Add(baseIndex + a); tris.Add(baseIndex + b); tris.Add(baseIndex + c);
        }
    }
}
