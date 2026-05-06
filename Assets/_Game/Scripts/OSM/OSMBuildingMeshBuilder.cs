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

    // ── PERF: if a single frame takes longer than this threshold the coroutine
    //          yields immediately, regardless of maxBuildingsPerFrame.
    [Header("Frame-time Budget")]
    [Tooltip("Max milliseconds to spend on buildings per frame (0 = disabled)")]
    public float maxMsPerFrame = 0f;

    [Header("State")]
    public bool buildingsBuilt = false;
    private GameObject buildingsParent;
    readonly Dictionary<BuildingKind, Material> materialCache = new Dictionary<BuildingKind, Material>();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(WaitForDataThenBuild());
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

            float height = ResolveBuildingHeight(way);
            var buildingKind = ResolveBuildingKind(way);

            Mesh mesh = BuildExtrudedMesh(footprint, height);
            if (mesh == null)
            {
                skippedInvalidFootprint++;
                continue;
            }

            string buildingLabel = ResolveBuildingLabel(way, buildingKind);
            GameObject buildingObj = new GameObject($"Building_{buildingLabel}_{way.id}");
            buildingObj.transform.parent = buildingsParent.transform;

            var mf = buildingObj.AddComponent<MeshFilter>();
            var mr = buildingObj.AddComponent<MeshRenderer>();
            mf.mesh = mesh;
            mr.material = ResolveBuildingMaterial(buildingKind);
            mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            mr.receiveShadows = false;

            if (drawFootprints)
                BuildFootprintOutline(footprint, buildingObj.transform, ResolveBuildingColor(buildingKind));

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
        Debug.Log(
            $"Buildings: Built {buildingCount} buildings! " +
            $"(skipped {skippedDistance} by distance, {skippedInvalidFootprint} invalid footprints, {skippedMissingNodes} missing nodes)");
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

    float ResolveBuildingHeight(OSMWay way)
    {
        float height = defaultBuildingHeight;

        if (way.tags.TryGetValue("building:levels", out var levelsRaw) && int.TryParse(levelsRaw, out int levels))
            height = Mathf.Max(floorHeight, levels * floorHeight);
        else if (way.tags.TryGetValue("height", out var heightRaw))
        {
            string cleaned = heightRaw.Replace("m", "").Trim();
            if (float.TryParse(cleaned, NumberStyles.Float, CultureInfo.InvariantCulture, out float parsedHeight))
                height = Mathf.Max(floorHeight, parsedHeight);
        }

        return height;
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
        if (mat.HasProperty("_Glossiness")) mat.SetFloat("_Glossiness", 0.05f);
        if (mat.HasProperty("_Metallic")) mat.SetFloat("_Metallic", 0f);
        if (mat.HasProperty("_Smoothness")) mat.SetFloat("_Smoothness", 0.08f);
        materialCache[kind] = mat;
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
        List<int> tris = new List<int>();
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

            tris.Add(idx); tris.Add(idx + 2); tris.Add(idx + 1);
            tris.Add(idx + 1); tris.Add(idx + 2); tris.Add(idx + 3);
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

            tris.Add(ci); tris.Add(ai); tris.Add(bi);
        }

        Mesh mesh = new Mesh();
        mesh.name = "Building";
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }
}
