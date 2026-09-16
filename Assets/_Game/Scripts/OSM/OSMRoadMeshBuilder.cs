using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Builds road mesh geometry from OSMData and places it in the scene.
///
/// Bug fixes vs previous version:
///   1. WINDING ORDER — triangles were wound CW from above, so normals pointed
///      DOWN into the ground. The mesh was technically there but back-face culled
///      from the camera. Fixed to CCW (left-tl-br / left-tr-tl pattern) so
///      normals point UP.
///   2. TWO-SIDED MATERIAL — even with correct winding, a single-sided shader
///      will vanish if the camera ever dips below the road plane. CreateDefault-
///      RoadMaterial now sets _Cull = Off.
///   3. Y-OFFSET — map tiles sit at y=0.15 (MapTileLoader.tileSurfaceY). Roads
///      were at y=0.02, so they rendered *under* the tile quads. Default roadY-
///      Offset bumped to 0.25 so roads are always on top of tiles.
///   4. CENTERLINE LOCAL-SPACE BUG — LineRenderer was set to useWorldSpace=false
///      but was given world-space positions converted via InverseTransformPoint on
///      a parent that sits at world origin, making them correct only when the
///      parent has no transform (fragile). Switched to useWorldSpace=true and
///      pass world positions directly, which is simpler and always correct.
///   5. RUNTIME MATERIAL OVERRIDE — RuntimeTarmacApplier runs with a 12-second
///      timeout and replaces whatever material OSMRoadMeshBuilder assigned, so
///      both need to agree. CreateDefaultRoadMaterial and RuntimeTarmacApplier
///      now use the same shader preference chain.
/// </summary>
public class OSMRoadMeshBuilder : MonoBehaviour
{
    public enum RoadModelForwardAxis
    {
        ZAxis,
        XAxis
    }

    public static OSMRoadMeshBuilder Instance { get; private set; }

    [Header("Materials")]
    public Material roadMaterial;
    public Material pavementMaterial;

    [Header("Road Model Visuals")]
    public bool useRoadModelInstances = true;
    public string roadModelResourcePath = "RoadsGenerated";
    public RoadModelForwardAxis roadModelForwardAxis = RoadModelForwardAxis.ZAxis;
    public float roadModelSurfaceLift = 0.01f;
    public bool overrideRoadModelMaterials = false;

    [Header("Median Barriers")]
    public bool autoGenerateMedianBarriers = true;
    public string medianBarrierResourcePath = "BussimAssets/road_fbarrier1";
    public float medianBarrierSpacingMeters = 4.5f;
    public float medianBarrierLift = 0.02f;

    [Header("Settings")]
    // Must be > MapTileLoader.tileSurfaceY (0.15) so roads render on top of map tiles.
    public float roadYOffset = 0.25f;
    public int roadLayer = 0;
    public string roadTag = "Road";
    public PhysicsMaterial roadPhysicMaterial;
    public float roadStaticFriction = 0.7f;
    public float roadDynamicFriction = 0.6f;
    public float maxColliderSegmentLength = 100f;
    public bool renderRoadSurface = true;
    public bool drawCenterLines = true;
    public float centerLineWidth = 0.3f;
    public Color centerLineColor = new Color(1f, 0.85f, 0.2f, 1f);

    [Header("Road Boundaries")]
    public bool generateRoadBoundaries = true;
    public bool includeServiceRoadBoundaries = false;
    [Min(0.1f)] public float boundaryWidth = 0.38f;
    [Min(0f)] public float boundaryOffsetFromRoad = 0.18f;
    [Min(0.01f)] public float boundaryHeight = 0.1f;
    public Color boundaryColor = new Color(0.72f, 0.74f, 0.72f, 1f);
    public bool addPhysicalBoundaryColliders = true;
    [Min(0.2f)] public float physicalBoundaryHeight = 0.7f;
    [Min(0f)] public float junctionOpeningMeters = 7f;

    [Header("State")]
    public bool roadsBuilt = false;
    private GameObject roadsParent;
    private GameObject roadVisualPrefab;
    private bool roadVisualPrefabLoadAttempted;
    private Bounds roadVisualBounds;
    private bool hasRoadVisualBounds;
    private GameObject medianBarrierPrefab;
    private bool medianBarrierLoadAttempted;
    private PhysicsMaterial runtimeRoadPhysicMaterial;
    private Material runtimeBoundaryMaterial;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(WaitForOSMThenBuild());
    }

    IEnumerator WaitForOSMThenBuild()
    {
        while (OSMLoader.Instance == null || !OSMLoader.Instance.dataLoaded || GPSManager.Instance == null)
            yield return new WaitForSeconds(0.5f);

        Debug.Log("OSM: Building road meshes...");
        EnsureRoadVisualPrefabLoaded();
        BuildRoads(OSMLoader.Instance.osmData);
    }

    void BuildRoads(OSMData data)
    {
        var colliderMaterial = ResolveRoadPhysicsMaterial();

        if (roadsParent != null)
            Destroy(roadsParent);

        roadsParent = new GameObject("OSM_Roads");
        roadsParent.transform.position = Vector3.zero;

        int roadCount = 0;

        foreach (var way in data.ways)
        {
            if (!way.IsRoad()) continue;

            var points = new List<Vector3>(way.nodeRefs.Count);
            var sourceNodeIds = new List<long>(way.nodeRefs.Count);
            foreach (long nodeRef in way.nodeRefs)
            {
                if (!data.nodeMap.TryGetValue(nodeRef, out var node)) continue;
                Vector3 worldPos = GPSManager.Instance.GpsToWorld(node.lat, node.lon);
                worldPos.y = roadYOffset;
                points.Add(worldPos);
                sourceNodeIds.Add(nodeRef);
            }

            if (points.Count < 2) continue;

            var sourcePoints = new List<Vector3>(points);
            points = DensifyPoints(points);

            float width = way.GetRoadWidth();
            Mesh mesh = BuildRoadSegmentMesh(points, width);
            if (mesh == null) continue;

            string roadLabel = way.GetRoadType();
            string trafficMode = way.IsOneWay() ? "OneWay" : "TwoWay";
            var roadObj = new GameObject($"Road_{way.id}_{roadLabel}_{trafficMode}_{way.GetDisplayName()}");
            roadObj.transform.parent = roadsParent.transform;
            if (roadLayer > 0)
                roadObj.layer = roadLayer;

            var mc = roadObj.AddComponent<MeshCollider>();
            mc.sharedMesh = mesh;
            if (colliderMaterial != null)
                mc.sharedMaterial = colliderMaterial;

            if (ShouldUseRoadModelVisuals())
            {
                BuildRoadModelVisuals(roadObj.transform, points, width);
            }
            else
            {
                var mf = roadObj.AddComponent<MeshFilter>();
                var mr = roadObj.AddComponent<MeshRenderer>();
                mf.mesh = mesh;
                mr.material = roadMaterial != null ? roadMaterial : CreateDefaultRoadMaterial();
                mr.enabled = renderRoadSurface;
                mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                mr.receiveShadows = false;
            }

            if (!string.IsNullOrEmpty(roadTag) && IsValidTag(roadTag))
                roadObj.tag = roadTag;

            if (renderRoadSurface && drawCenterLines && way.ShouldDrawCenterLine())
                BuildCenterLine(points);

            if (generateRoadBoundaries && (includeServiceRoadBoundaries || way.GetRoadType() != "service"))
            {
                BuildRoadBoundaries(points, width);
                if (addPhysicalBoundaryColliders)
                    BuildPhysicalRoadBoundaries(roadObj.transform, sourcePoints, sourceNodeIds, data, width);
            }

            if (autoGenerateMedianBarriers && way.ShouldGenerateMedianBarrier())
                BuildMedianBarrier(roadObj.transform, points, way.GetRoadWidth());

            roadCount++;
        }

        roadsBuilt = true;
        Debug.Log($"OSM: Built {roadCount} road segments!");
    }

    PhysicsMaterial ResolveRoadPhysicsMaterial()
    {
        if (roadPhysicMaterial != null)
            return roadPhysicMaterial;

        if (runtimeRoadPhysicMaterial == null)
        {
            runtimeRoadPhysicMaterial = new PhysicsMaterial("RoadPhysics_Runtime")
            {
                staticFriction = roadStaticFriction,
                dynamicFriction = roadDynamicFriction,
                bounciness = 0f,
                frictionCombine = PhysicsMaterialCombine.Average,
                bounceCombine = PhysicsMaterialCombine.Minimum
            };
        }

        return runtimeRoadPhysicMaterial;
    }

    public void SetRoadSurfaceVisible(bool visible)
    {
        renderRoadSurface = visible;
        if (roadsParent == null)
            roadsParent = GameObject.Find("OSM_Roads");
        if (roadsParent == null) return;

        foreach (var mr in roadsParent.GetComponentsInChildren<MeshRenderer>(true))
            if (mr != null) mr.enabled = visible;
    }

    // ── Mesh construction ─────────────────────────────────────────────────────

    Mesh BuildRoadSegmentMesh(List<Vector3> points, float width)
    {
        if (points.Count < 2) return null;

        var verts = new List<Vector3>(points.Count * 2);
        var tris  = new List<int>((points.Count - 1) * 6);
        var uvs   = new List<Vector2>(points.Count * 2);

        float halfWidth  = width * 0.5f;
        float uvProgress = 0f;

        for (int i = 0; i < points.Count; i++)
        {
            Vector3 fwd;
            if      (i == 0)               fwd = (points[1]     - points[0]).normalized;
            else if (i == points.Count - 1) fwd = (points[i]     - points[i - 1]).normalized;
            else                            fwd = (points[i + 1] - points[i - 1]).normalized;

            // right = Cross(up, forward) → points to road's right side
            Vector3 right = Vector3.Cross(Vector3.up, fwd).normalized;

            verts.Add(points[i] - right * halfWidth);   // left  (even index)
            verts.Add(points[i] + right * halfWidth);   // right (odd index)

            if (i > 0)
                uvProgress += Vector3.Distance(points[i], points[i - 1]) / Mathf.Max(0.01f, width);

            uvs.Add(new Vector2(0f, uvProgress));
            uvs.Add(new Vector2(1f, uvProgress));

            if (i > 0)
            {
                int bl = (i - 1) * 2;       // bottom-left  (prev left)
                int br = bl + 1;             // bottom-right (prev right)
                int tl = i * 2;              // top-left     (curr left)
                int tr = tl + 1;             // top-right    (curr right)

                // FIX: CCW winding from above so normals point UP (were CW before)
                tris.Add(bl); tris.Add(tr); tris.Add(br);   // tri 1
                tris.Add(bl); tris.Add(tl); tris.Add(tr);   // tri 2
            }
        }

        var mesh = new Mesh
        {
            name        = "RoadSegment",
            indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
        };
        mesh.SetVertices(verts);
        mesh.SetTriangles(tris, 0);
        mesh.SetUVs(0, uvs);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }

    // ── Centre line ───────────────────────────────────────────────────────────

    // Note: LineRenderer lives in its own GameObject as a sibling under roadsParent,
    // NOT as a child of a road segment. This avoids the local-space conversion bug
    // where InverseTransformPoint was used on a parent at world origin (worked by
    // accident, broke when parent had any transform offset).
    void BuildCenterLine(List<Vector3> points)
    {
        if (points == null || points.Count < 2) return;

        var go = new GameObject("CenterLine");
        go.transform.SetParent(roadsParent.transform, false);

        var lr = go.AddComponent<LineRenderer>();
        lr.positionCount  = points.Count;
        lr.useWorldSpace  = true;   // FIX: world-space positions, no transform math needed
        lr.startWidth     = centerLineWidth;
        lr.endWidth       = centerLineWidth;
        lr.numCapVertices = 2;
        lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lr.receiveShadows    = false;

        var mat = new Material(ResolveShader());
        mat.color = centerLineColor;
        // Ensure the line draws on top of the road mesh
        mat.renderQueue = 2450;
        lr.material = mat;

        var worldPts = new Vector3[points.Count];
        for (int i = 0; i < points.Count; i++)
        {
            var p = points[i];
            p.y = roadYOffset + 0.03f;   // just above the road surface
            worldPts[i] = p;
        }
        lr.SetPositions(worldPts);
    }

    void BuildRoadBoundaries(List<Vector3> points, float roadWidth)
    {
        if (points == null || points.Count < 2) return;

        Vector3[] left = new Vector3[points.Count];
        Vector3[] right = new Vector3[points.Count];
        float offset = roadWidth * .5f + Mathf.Max(0f, boundaryOffsetFromRoad);
        for (int i = 0; i < points.Count; i++)
        {
            Vector3 forward = i == 0
                ? points[1] - points[0]
                : i == points.Count - 1 ? points[i] - points[i - 1] : points[i + 1] - points[i - 1];
            forward.y = 0f;
            if (forward.sqrMagnitude < .001f) forward = Vector3.forward;
            Vector3 side = Vector3.Cross(Vector3.up, forward.normalized);
            Vector3 basePoint = points[i];
            basePoint.y = roadYOffset + boundaryHeight;
            left[i] = basePoint - side * offset;
            right[i] = basePoint + side * offset;
        }

        Mesh leftMesh = BuildRoadSegmentMesh(new List<Vector3>(left), Mathf.Max(.1f, boundaryWidth));
        Mesh rightMesh = BuildRoadSegmentMesh(new List<Vector3>(right), Mathf.Max(.1f, boundaryWidth));
        if (leftMesh == null || rightMesh == null) return;

        var combine = new[]
        {
            new CombineInstance { mesh = leftMesh, transform = Matrix4x4.identity },
            new CombineInstance { mesh = rightMesh, transform = Matrix4x4.identity }
        };
        Mesh combined = new Mesh { name = "RoadBoundaries", indexFormat = UnityEngine.Rendering.IndexFormat.UInt32 };
        combined.CombineMeshes(combine, true, false);
        Destroy(leftMesh);
        Destroy(rightMesh);

        GameObject boundary = new GameObject("RoadBoundaries");
        boundary.transform.SetParent(roadsParent.transform, false);
        MeshFilter filter = boundary.AddComponent<MeshFilter>();
        MeshRenderer renderer = boundary.AddComponent<MeshRenderer>();
        filter.sharedMesh = combined;
        renderer.sharedMaterial = ResolveBoundaryMaterial();
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = true;
    }

    Material ResolveBoundaryMaterial()
    {
        if (pavementMaterial != null) return pavementMaterial;
        if (runtimeBoundaryMaterial != null) return runtimeBoundaryMaterial;
        runtimeBoundaryMaterial = new Material(ResolveShader()) { color = boundaryColor, renderQueue = 2120 };
        if (runtimeBoundaryMaterial.HasProperty("_Smoothness")) runtimeBoundaryMaterial.SetFloat("_Smoothness", .08f);
        if (runtimeBoundaryMaterial.HasProperty("_Metallic")) runtimeBoundaryMaterial.SetFloat("_Metallic", 0f);
        return runtimeBoundaryMaterial;
    }

    void BuildPhysicalRoadBoundaries(Transform parent, List<Vector3> points, List<long> nodeIds, OSMData data, float roadWidth)
    {
        if (parent == null || points == null || nodeIds == null || points.Count < 2 || nodeIds.Count != points.Count)
            return;

        GameObject root = new GameObject("PhysicalRoadBoundaries");
        root.transform.SetParent(parent, false);
        float sideOffset = roadWidth * .5f + Mathf.Max(.05f, boundaryOffsetFromRoad);
        float colliderWidth = Mathf.Max(.2f, boundaryWidth);
        float colliderHeight = Mathf.Max(.2f, physicalBoundaryHeight);

        for (int i = 1; i < points.Count; i++)
        {
            Vector3 from = points[i - 1];
            Vector3 to = points[i];
            Vector3 segment = to - from;
            segment.y = 0f;
            float length = segment.magnitude;
            if (length < 1f) continue;

            Vector3 direction = segment / length;
            float startGap = data != null && data.IsIntersectionNode(nodeIds[i - 1]) ? junctionOpeningMeters : 0f;
            float endGap = data != null && data.IsIntersectionNode(nodeIds[i]) ? junctionOpeningMeters : 0f;
            float usableLength = length - startGap - endGap;
            if (usableLength < 1f) continue;

            Vector3 right = Vector3.Cross(Vector3.up, direction);
            Vector3 center = from + direction * (startGap + usableLength * .5f);
            center.y = roadYOffset + colliderHeight * .5f;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            CreateBoundaryCollider(root.transform, $"Left_{i}", center - right * sideOffset, rotation, colliderWidth, colliderHeight, usableLength);
            CreateBoundaryCollider(root.transform, $"Right_{i}", center + right * sideOffset, rotation, colliderWidth, colliderHeight, usableLength);
        }
    }

    void CreateBoundaryCollider(Transform parent, string name, Vector3 position, Quaternion rotation, float width, float height, float length)
    {
        GameObject edge = new GameObject(name);
        edge.transform.SetParent(parent, false);
        edge.transform.SetPositionAndRotation(position, rotation);
        BoxCollider collider = edge.AddComponent<BoxCollider>();
        collider.size = new Vector3(width, height, length);
        collider.isTrigger = false;
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    bool ShouldUseRoadModelVisuals()
    {
        EnsureRoadVisualPrefabLoaded();
        return useRoadModelInstances && roadVisualPrefab != null && hasRoadVisualBounds;
    }

    void EnsureRoadVisualPrefabLoaded()
    {
        if (roadVisualPrefabLoadAttempted)
            return;

        roadVisualPrefabLoadAttempted = true;

        if (!useRoadModelInstances || string.IsNullOrWhiteSpace(roadModelResourcePath))
            return;

        roadVisualPrefab = Resources.Load<GameObject>(roadModelResourcePath);
        if (roadVisualPrefab == null)
        {
            GameObject[] generatedRoads = Resources.LoadAll<GameObject>(roadModelResourcePath);
            if (generatedRoads != null && generatedRoads.Length > 0)
                roadVisualPrefab = generatedRoads[0];
        }
        if (roadVisualPrefab == null)
        {
            Debug.LogWarning($"OSM: Road model not found at Resources path '{roadModelResourcePath}'. Falling back to generated mesh roads.");
            return;
        }

        hasRoadVisualBounds = TryMeasureRoadVisualBounds(out roadVisualBounds);
        if (!hasRoadVisualBounds)
            Debug.LogWarning($"OSM: Failed to measure road model bounds for '{roadVisualPrefab.name}'. Falling back to generated mesh roads.");
    }

    void EnsureMedianBarrierPrefabLoaded()
    {
        if (medianBarrierLoadAttempted)
            return;

        medianBarrierLoadAttempted = true;
        if (!autoGenerateMedianBarriers || string.IsNullOrWhiteSpace(medianBarrierResourcePath))
            return;

        medianBarrierPrefab = Resources.Load<GameObject>(medianBarrierResourcePath);
        if (medianBarrierPrefab == null)
            Debug.LogWarning($"OSM: Median barrier model not found at Resources path '{medianBarrierResourcePath}'.");
    }

    bool TryMeasureRoadVisualBounds(out Bounds bounds)
    {
        bounds = new Bounds();

        if (roadVisualPrefab == null)
            return false;

        var sample = Instantiate(roadVisualPrefab);
        sample.name = "__RoadVisualBounds";
        sample.hideFlags = HideFlags.HideAndDontSave;
        sample.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        sample.transform.localScale = Vector3.one;

        var renderers = sample.GetComponentsInChildren<Renderer>(true);
        if (renderers.Length == 0)
        {
            Destroy(sample);
            return false;
        }

        bounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
            bounds.Encapsulate(renderers[i].bounds);

        Destroy(sample);
        return bounds.size.sqrMagnitude > 0.0001f;
    }

    void BuildRoadModelVisuals(Transform parent, List<Vector3> points, float width)
    {
        float sourceLength = GetRoadModelLength();
        float sourceWidth = GetRoadModelWidth();
        if (sourceLength <= 0.01f || sourceWidth <= 0.01f)
            return;

        float modelBottomOffset = roadYOffset - roadVisualBounds.min.y + roadModelSurfaceLift;

        for (int i = 1; i < points.Count; i++)
        {
            Vector3 from = points[i - 1];
            Vector3 to = points[i];
            Vector3 segment = to - from;
            float segmentLength = segment.magnitude;
            if (segmentLength <= 0.05f)
                continue;

            Vector3 direction = segment / segmentLength;
            int pieceCount = Mathf.Max(1, Mathf.CeilToInt(segmentLength / sourceLength));
            float pieceLength = segmentLength / pieceCount;
            float widthScale = width / sourceWidth;
            float lengthScale = pieceLength / sourceLength;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up) * GetRoadModelAxisRotation();

            for (int pieceIndex = 0; pieceIndex < pieceCount; pieceIndex++)
            {
                float distance = pieceLength * (pieceIndex + 0.5f);
                Vector3 position = from + direction * distance;
                position.y = modelBottomOffset;

                var visual = Instantiate(roadVisualPrefab, position, rotation, parent);
                visual.name = $"RoadVisual_{i}_{pieceIndex}";
                visual.transform.localScale = GetScaledRoadVisualScale(visual.transform.localScale, widthScale, lengthScale);
                ApplyLayerRecursively(visual, parent.gameObject.layer);

                foreach (var renderer in visual.GetComponentsInChildren<Renderer>(true))
                {
                    renderer.enabled = renderRoadSurface;
                    renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    renderer.receiveShadows = false;

                    if (overrideRoadModelMaterials)
                        renderer.material = roadMaterial != null ? roadMaterial : CreateDefaultRoadMaterial();
                }
            }
        }
    }

    void BuildMedianBarrier(Transform parent, List<Vector3> points, float roadWidth)
    {
        EnsureMedianBarrierPrefabLoaded();
        if (medianBarrierPrefab == null || points == null || points.Count < 2)
            return;

        for (int i = 1; i < points.Count; i++)
        {
            Vector3 from = points[i - 1];
            Vector3 to = points[i];
            Vector3 segment = to - from;
            float segmentLength = segment.magnitude;
            if (segmentLength < 1f)
                continue;

            Vector3 direction = segment / segmentLength;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            int pieceCount = Mathf.Max(1, Mathf.FloorToInt(segmentLength / Mathf.Max(1f, medianBarrierSpacingMeters)));

            for (int pieceIndex = 0; pieceIndex <= pieceCount; pieceIndex++)
            {
                float distance = Mathf.Min(segmentLength, pieceIndex * medianBarrierSpacingMeters);
                Vector3 position = from + direction * distance;
                position.y = roadYOffset + medianBarrierLift;

                var barrier = Instantiate(medianBarrierPrefab, position, rotation, parent);
                barrier.name = $"MedianBarrier_{i}_{pieceIndex}";
                float lateralScale = Mathf.Clamp(roadWidth / 8f, 0.85f, 1.2f);
                barrier.transform.localScale = new Vector3(
                    barrier.transform.localScale.x * lateralScale,
                    barrier.transform.localScale.y,
                    barrier.transform.localScale.z);
                ApplyLayerRecursively(barrier, parent.gameObject.layer);
            }
        }
    }

    Quaternion GetRoadModelAxisRotation()
    {
        return roadModelForwardAxis == RoadModelForwardAxis.XAxis
            ? Quaternion.Euler(0f, -90f, 0f)
            : Quaternion.identity;
    }

    float GetRoadModelLength()
    {
        return roadModelForwardAxis == RoadModelForwardAxis.XAxis
            ? roadVisualBounds.size.x
            : roadVisualBounds.size.z;
    }

    float GetRoadModelWidth()
    {
        return roadModelForwardAxis == RoadModelForwardAxis.XAxis
            ? roadVisualBounds.size.z
            : roadVisualBounds.size.x;
    }

    Vector3 GetScaledRoadVisualScale(Vector3 currentScale, float widthScale, float lengthScale)
    {
        if (roadModelForwardAxis == RoadModelForwardAxis.XAxis)
            return new Vector3(currentScale.x * lengthScale, currentScale.y, currentScale.z * widthScale);

        return new Vector3(currentScale.x * widthScale, currentScale.y, currentScale.z * lengthScale);
    }

    void ApplyLayerRecursively(GameObject root, int layer)
    {
        if (root == null)
            return;

        root.layer = layer;
        foreach (Transform child in root.transform)
            ApplyLayerRecursively(child.gameObject, layer);
    }

    List<Vector3> DensifyPoints(List<Vector3> points)
    {
        if (points == null || points.Count < 2 || maxColliderSegmentLength <= 0f)
            return points;

        var result = new List<Vector3>(points.Count);
        result.Add(points[0]);

        for (int i = 1; i < points.Count; i++)
        {
            Vector3 from = points[i - 1];
            Vector3 to   = points[i];
            float dist   = Vector3.Distance(from, to);
            int   steps  = Mathf.Max(1, Mathf.CeilToInt(dist / maxColliderSegmentLength));

            for (int s = 1; s <= steps; s++)
                result.Add(Vector3.Lerp(from, to, s / (float)steps));
        }

        return result;
    }

    bool IsValidTag(string tag)
    {
        if (string.IsNullOrEmpty(tag)) return false;
        try { GameObject.FindWithTag(tag); return true; }
        catch { return false; }
    }

    Shader ResolveShader()
    {
        return Shader.Find("Universal Render Pipeline/Lit")
            ?? Shader.Find("Universal Render Pipeline/Unlit")
            ?? Shader.Find("Standard")
            ?? Shader.Find("Unlit/Color");
    }

    Material CreateDefaultRoadMaterial()
    {
        var mat = new Material(ResolveShader())
        {
            color      = new Color(0.12f, 0.12f, 0.12f, 1f),
            // Render on top of map tile quads (default queue 2000) but below UI
            renderQueue = 2100
        };

        // Matte, non-reflective asphalt look
        if (mat.HasProperty("_Glossiness")) mat.SetFloat("_Glossiness", 0f);
        if (mat.HasProperty("_Metallic"))   mat.SetFloat("_Metallic",   0f);
        if (mat.HasProperty("_Smoothness")) mat.SetFloat("_Smoothness", 0.05f);

        // FIX: disable back-face culling so the mesh is visible regardless of
        // winding order issues and from below (debug camera angles, etc.)
        if (mat.HasProperty("_Cull"))
            mat.SetFloat("_Cull", (float)UnityEngine.Rendering.CullMode.Off);

        // URP Lit — ensure opaque mode
        if (mat.HasProperty("_Surface")) mat.SetFloat("_Surface", 0f);  // 0 = Opaque
        if (mat.HasProperty("_ZWrite"))  mat.SetFloat("_ZWrite",  1f);

        return mat;
    }
}
