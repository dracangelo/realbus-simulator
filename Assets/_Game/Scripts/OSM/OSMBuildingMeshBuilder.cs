using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

public class OSMBuildingMeshBuilder : MonoBehaviour
{
    enum BuildingKind
    {
        Generic,
        House,
        Commercial,
        School,
        Hospital
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

    // ── PERF: if a single frame takes longer than this threshold the coroutine
    //          yields immediately, regardless of maxBuildingsPerFrame.
    [Header("Frame-time Budget")]
    [Tooltip("Max milliseconds to spend on buildings per frame (0 = disabled)")]
    public float maxMsPerFrame = 0f;

    [Header("State")]
    public bool buildingsBuilt = false;
    private GameObject buildingsParent;
    readonly Dictionary<BuildingKind, Material> materialCache = new Dictionary<BuildingKind, Material>();
    readonly Dictionary<BuildingKind, Material> roofMaterialCache = new Dictionary<BuildingKind, Material>();
    Texture2D defaultFacadeTexture;
    Texture2D defaultRoofTexture;
    readonly Dictionary<Vector2Int, List<BuildingRecord>> buildingBuckets = new Dictionary<Vector2Int, List<BuildingRecord>>();
    readonly Dictionary<long, BuildingRecord> recordsById = new Dictionary<long, BuildingRecord>();
    readonly Dictionary<long, GameObject> activeBuildings = new Dictionary<long, GameObject>();
    Transform streamingTarget;
    float streamingTimer;
    bool streamRefreshRunning;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
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
        while (OSMBuildingLoader.Instance == null || !OSMBuildingLoader.Instance.dataLoaded || GPSManager.Instance == null)
            yield return new WaitForSeconds(0.5f);

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
        buildingsBuilt = false;

        int buildingCount = 0;
        int skippedDistance = 0;
        int skippedInvalidFootprint = 0;
        int skippedMissingNodes = 0;
        int frameCount = 0;
        float maxDistSq = maxBuildingDistance * maxBuildingDistance;
        float frameStart = Time.realtimeSinceStartup;

        foreach (var way in data.ways)
        {
            if (!way.IsBuilding()) continue;

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
                continue;
            }

            // Remove duplicate last point if closed polygon
            if (footprint.Count > 1 &&
                Vector3.Distance(footprint[0], footprint[footprint.Count - 1]) < 0.1f)
                footprint.RemoveAt(footprint.Count - 1);

            if (footprint.Count < 3)
            {
                skippedInvalidFootprint++;
                continue;
            }

            float signedArea = GetSignedAreaXZ(footprint);
            if (Mathf.Abs(signedArea) < 0.01f)
            {
                skippedInvalidFootprint++;
                continue;
            }

            // Normalize winding so roof triangles face upward and wall faces
            // point outward instead of being culled from the common camera view.
            if (signedArea < 0f)
                footprint.Reverse();

            // ── Distance culling: compute centroid XZ, skip if too far away
            if (enableDistanceCulling)
            {
                Vector3 centroid = Vector3.zero;
                foreach (var p in footprint) centroid += p;
                centroid /= footprint.Count;
                float distSq = centroid.x * centroid.x + centroid.z * centroid.z;
                if (distSq > maxDistSq)
                {
                    skippedDistance++;
                    continue;
                }
            }

            var buildingKind = ResolveBuildingKind(way);
            Vector3 center = Vector3.zero;
            for (int i = 0; i < footprint.Count; i++) center += footprint[i];
            center /= footprint.Count;

            var record = new BuildingRecord
            {
                id = way.id,
                label = ResolveBuildingLabel(way, buildingKind),
                kind = buildingKind,
                footprint = footprint,
                center = center,
                height = ResolveBuildingHeight(way, buildingKind)
            };

            recordsById[record.id] = record;
            Vector2Int cell = WorldToStreamingCell(record.center);
            if (!buildingBuckets.TryGetValue(cell, out var bucket))
                buildingBuckets[cell] = bucket = new List<BuildingRecord>();
            bucket.Add(record);

            if (!streamAroundVehicle)
                CreateBuildingObject(record);

            buildingCount++;
            frameCount++;

            // ── Yield when batch limit OR frame-time budget is exceeded
            bool batchFull = frameCount >= maxBuildingsPerFrame;
            bool overBudget = maxMsPerFrame > 0f &&
                              (Time.realtimeSinceStartup - frameStart) * 1000f >= maxMsPerFrame;

            if (batchFull || overBudget)
            {
                frameCount = 0;
                frameStart = Time.realtimeSinceStartup;
                yield return null;
            }
        }

        buildingsBuilt = true;
        if (streamAroundVehicle)
            yield return StartCoroutine(RefreshStreamedBuildings());
        Debug.Log(
            streamAroundVehicle
            ? $"Buildings: Indexed {buildingCount} buildings; {activeBuildings.Count} loaded near the vehicle. " +
              $"Streaming range {buildingLoadDistance:0}m/{buildingUnloadDistance:0}m. " +
              $"(skipped {skippedDistance} by distance, {skippedInvalidFootprint} invalid footprints, {skippedMissingNodes} missing nodes)"
            : $"Buildings: Built {buildingCount} buildings! " +
            $"(skipped {skippedDistance} by distance, {skippedInvalidFootprint} invalid footprints, {skippedMissingNodes} missing nodes)");
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
        int loadCount = Mathf.Min(Mathf.Max(1, maxBuildingLoadsPerRefresh), candidates.Count);
        for (int i = 0; i < loadCount; i++)
        {
            BuildingRecord record = candidates[i];
            GameObject instance = CreateBuildingObject(record);
            if (instance != null) activeBuildings[record.id] = instance;
        }

        streamRefreshRunning = false;
        yield break;
    }

    GameObject CreateBuildingObject(BuildingRecord record)
    {
        Mesh mesh = BuildExtrudedMesh(record.footprint, record.height);
        if (mesh == null) return null;
        GameObject buildingObj = new GameObject($"Building_{record.label}_{record.id}");
        buildingObj.transform.SetParent(buildingsParent.transform, false);
        var mf = buildingObj.AddComponent<MeshFilter>();
        var mr = buildingObj.AddComponent<MeshRenderer>();
        mf.sharedMesh = mesh;
        mr.sharedMaterials = new[] { ResolveBuildingMaterial(record.kind), ResolveRoofMaterial(record.kind) };
        mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        mr.receiveShadows = true;
        if (drawFootprints)
            BuildFootprintOutline(record.footprint, buildingObj.transform, ResolveBuildingColor(record.kind));
        return buildingObj;
    }

    void DestroyBuildingObject(GameObject building)
    {
        if (building == null) return;
        MeshFilter filter = building.GetComponent<MeshFilter>();
        if (filter != null && filter.sharedMesh != null) Destroy(filter.sharedMesh);
        Destroy(building);
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
        BusController bus = FindFirstObjectByType<BusController>();
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
            case BuildingKind.Commercial: return Mathf.Lerp(10f, 30f, variation);
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
                case "apartments":
                    return BuildingKind.House;
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

    Shader ResolveDefaultBuildingShader()
    {
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null) shader = Shader.Find("Standard");
        if (shader == null) shader = Shader.Find("Unlit/Color");
        return shader;
    }

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

    Color ResolveBuildingColor(BuildingKind kind)
    {
        switch (kind)
        {
            case BuildingKind.House: return new Color(0.86f, 0.75f, 0.62f, 1f);
            case BuildingKind.Commercial: return new Color(0.59f, 0.74f, 0.9f, 1f);
            case BuildingKind.School: return new Color(0.95f, 0.72f, 0.26f, 1f);
            case BuildingKind.Hospital: return new Color(0.87f, 0.35f, 0.38f, 1f);
            default: return new Color(0.76f, 0.79f, 0.84f, 1f);
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

    Mesh BuildExtrudedMesh(List<Vector3> footprint, float height)
    {
        int n = footprint.Count;
        if (n < 3) return null;

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

            float segLen = Vector3.Distance(bl, br) / height;
            uvs.Add(new Vector2(0, 0));
            uvs.Add(new Vector2(segLen, 0));
            uvs.Add(new Vector2(0, 1));
            uvs.Add(new Vector2(segLen, 1));

            wallTris.Add(idx); wallTris.Add(idx + 2); wallTris.Add(idx + 1);
            wallTris.Add(idx + 1); wallTris.Add(idx + 2); wallTris.Add(idx + 3);
        }

        // Flat roof using fan triangulation
        int roofBase = verts.Count;
        Vector3 center = Vector3.zero;
        foreach (var p in footprint) center += p;
        center /= n;
        center.y = height;

        verts.Add(center);
        uvs.Add(new Vector2(0.5f, 0.5f));

        for (int i = 0; i < n; i++)
        {
            int next = (i + 1) % n;
            verts.Add(footprint[i] + Vector3.up * height);
            verts.Add(footprint[next] + Vector3.up * height);
            uvs.Add(new Vector2(0, 0));
            uvs.Add(new Vector2(1, 0));

            int ci = roofBase;
            int ai = roofBase + 1 + i * 2;
            int bi = roofBase + 1 + i * 2 + 1;

            roofTris.Add(ci); roofTris.Add(ai); roofTris.Add(bi);
        }

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
}
