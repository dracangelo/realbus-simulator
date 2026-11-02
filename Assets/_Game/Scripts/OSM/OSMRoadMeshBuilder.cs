using UnityEngine;
using System;
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
///   3. Y-OFFSET — map tiles sit at tileSurfaceY (0.12 in the generated scene).
///      Roads were at y=0.02, so they rendered *under* the tile quads. The scene
///      now sets roadYOffset to tileSurfaceY + 0.12, which is far enough apart
///      that the depth buffer keeps the asphalt in front out to the fog wall.
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
    public Material shoulderMaterial;

    [Header("Road Model Visuals")]
    public bool useRoadModelInstances = true;
    public string roadModelResourcePath = "RoadsGenerated";
    public RoadModelForwardAxis roadModelForwardAxis = RoadModelForwardAxis.ZAxis;
    [Tooltip("Metres the imported road piece sits above the generated ribbon. The old 1 cm left the two coplanar surfaces fighting for depth at any distance, which made the asphalt flicker as the bus moved.")]
    public float roadModelSurfaceLift = 0.05f;
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

    [Header("Lane Markings")]
    public bool generateRealisticLaneMarkings = true;
    public float laneMarkingWidth = 0.15f;
    public float dashLengthMeters = 3.0f;
    public float dashGapMeters = 3.0f;
    // Weathered paint: fresh white is the brightest thing in a street and makes the
    // asphalt around it read even darker.
    public Color laneMarkingWhiteColor = new Color(0.70f, 0.70f, 0.67f, 1f);
    public Color laneMarkingYellowColor = new Color(0.72f, 0.58f, 0.16f, 1f);
    public bool drawOuterEdgeLines = true;
    public float outerEdgeLineOffset = 0.22f;

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

    [Header("Road Shoulders")]
    [Tooltip("A gravel/dirt verge between the asphalt edge and the kerb. It hides the razor-sharp seam where the ribbon meets the map tile.")]
    public bool generateRoadShoulders = true;
    [Min(0f)] public float shoulderWidth = 0.9f;
    public Color shoulderColor = new Color(0.32f, 0.28f, 0.23f, 1f);

    [Header("State")]
    public bool roadsBuilt = false;
    public RoadSurfaceIndex SurfaceIndex { get; private set; }

    /// <summary>
    /// Friction material for road colliders built outside the OSM pass (the depot
    /// driveways). Sharing it keeps depot tarmac gripped like the street it joins.
    /// </summary>
    public PhysicsMaterial RoadColliderMaterial => ResolveRoadPhysicsMaterial();

    /// <summary>Asphalt material for a runtime-built road surface (depot driveway).</summary>
    public Material RoadSurfaceMaterial() => ResolveRoadMaterial();

    /// <summary>
    /// Returns the road-side clearance index, creating an empty one before the OSM
    /// pass has run. Runtime surfaces (depot driveways) register into it so
    /// building infill, street furniture and spawn clearing all treat them as road,
    /// even when they exist before the city road build completes.
    /// </summary>
    public RoadSurfaceIndex EnsureSurfaceIndex()
    {
        if (SurfaceIndex == null) SurfaceIndex = new RoadSurfaceIndex();
        return SurfaceIndex;
    }
    private GameObject roadsParent;
    private GameObject roadVisualPrefab;
    private bool roadVisualPrefabLoadAttempted;
    private Bounds roadVisualBounds;
    private bool hasRoadVisualBounds;
    private GameObject medianBarrierPrefab;
    private bool medianBarrierLoadAttempted;
    private PhysicsMaterial runtimeRoadPhysicMaterial;
    private Material runtimeBoundaryMaterial;
    private Material runtimeShoulderMaterial;
    private static Texture2D runtimeAsphaltTexture;
    static bool loggedRoadMaterial;

    /// <summary>
    /// The active road builder, finding it in the scene when nothing has claimed
    /// the role yet. Runtime helpers (depot driveways) can run before Awake has,
    /// and a second builder left behind in edit mode must not hide the live one.
    /// </summary>
    public static OSMRoadMeshBuilder Resolve()
    {
        if (Instance != null) return Instance;
        Instance = FindAnyObjectByType<OSMRoadMeshBuilder>();
        return Instance;
    }

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

    Coroutine streamRoadsRoutine;

    void BuildRoads(OSMData data)
    {
        // Scenes without SceneBootstrap still need the same envelope. The policy
        // only ever widens, so a second call after bootstrap is harmless.
        RoadWidthPolicy.ConfigureFromTrafficCatalog();

        if (streamRoadsRoutine != null) StopCoroutine(streamRoadsRoutine);
        roadsBuilt = false;
        SurfaceIndex = RoadSurfaceIndex.Build(data, GPSManager.Instance.GpsToWorld);
        var colliderMaterial = ResolveRoadPhysicsMaterial();

        if (roadsParent != null)
            Destroy(roadsParent);

        roadsParent = new GameObject("OSM_Roads");
        roadsParent.transform.position = Vector3.zero;

        Vector3 spawnFocus = ResolveSpawnFocus(data);
        float immediateRadiusSq = 350f * 350f;

        var immediateRoads = new List<OSMWay>();
        var deferredRoads = new List<OSMWay>();

        foreach (var way in data.ways)
        {
            if (!way.IsRoad()) continue;
            float distSq = ApproximateWayDistanceSq(way, data, spawnFocus);
            if (distSq <= immediateRadiusSq)
                immediateRoads.Add(way);
            else
                deferredRoads.Add(way);
        }

        // Both the immediate batch and the background stream build what the camera
        // can see first. A nearby road behind the depot still loads; it simply waits
        // for the street in the windscreen to exist.
        BusController playerBus = MissionManager.Instance != null
            ? MissionManager.Instance.busController
            : FindAnyObjectByType<BusController>();
        Vector3 viewForward = ViewPriority.ResolveForward(playerBus != null ? playerBus.transform : null);
        Comparison<OSMWay> byView = (a, b) =>
        {
            float scoreA = ApproximateWayViewScore(a, data, spawnFocus, viewForward);
            float scoreB = ApproximateWayViewScore(b, data, spawnFocus, viewForward);
            return scoreA.CompareTo(scoreB);
        };
        immediateRoads.Sort(byView);
        deferredRoads.Sort(byView);

        int roadCount = 0;
        // 1. Build immediate roads around bus spawn instantly (Frame 1)
        for (int i = 0; i < immediateRoads.Count; i++)
        {
            if (BuildSingleRoadWay(immediateRoads[i], data, colliderMaterial))
                roadCount++;
        }

        // UNBLOCK THE BUS, CAMERA, AND DEPENDENT SYSTEMS IMMEDIATELY!
        roadsBuilt = true;
        Debug.Log($"OSM: Immediate spawn roads ready ({roadCount} segments). Bus active! Streaming {deferredRoads.Count} remaining roads in background...");

        // 2. Stream remaining city roads smoothly in background without dropping frames
        streamRoadsRoutine = StartCoroutine(StreamDeferredRoads(deferredRoads, data, colliderMaterial, roadCount));
    }

    IEnumerator StreamDeferredRoads(List<OSMWay> deferredRoads, OSMData data, PhysicsMaterial colliderMaterial, int initialCount)
    {
        int count = initialCount;
        int index = 0;
        while (index < deferredRoads.Count)
        {
            float frameStart = Time.realtimeSinceStartup;
            while (index < deferredRoads.Count && (Time.realtimeSinceStartup - frameStart) * 1000f < 4f)
            {
                if (BuildSingleRoadWay(deferredRoads[index], data, colliderMaterial))
                    count++;
                index++;
            }
            yield return null;
        }
        Debug.Log($"OSM: All {count} road segments built and streamed successfully!");
    }

    bool BuildSingleRoadWay(OSMWay way, OSMData data, PhysicsMaterial colliderMaterial)
    {
        if (!way.IsRoad()) return false;

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

        if (points.Count < 2) return false;

        var sourcePoints = new List<Vector3>(points);
        points = DensifyPoints(points);

        int laneCount = way.GetLaneCount();
        float width = way.GetRoadWidth();
        Mesh mesh = BuildRoadSegmentMesh(points, width, laneCount, true);
        if (mesh == null) return false;

        string roadLabel = way.GetRoadType();
        string trafficMode = way.IsOneWay() ? "OneWay" : "TwoWay";
        var roadObj = new GameObject($"Road_{way.id}_{roadLabel}_{trafficMode}_{way.GetDisplayName()}_Lanes{laneCount}");
        if (roadsParent != null) roadObj.transform.parent = roadsParent.transform;
        if (roadLayer > 0)
            roadObj.layer = roadLayer;

        var mc = roadObj.AddComponent<MeshCollider>();
        mc.sharedMesh = mesh;
        if (colliderMaterial != null)
            mc.sharedMaterial = colliderMaterial;

        // The generated surface is always retained as a reliable base. Imported
        // road packs are decorative overlays and must never make roads vanish.
        var mf = roadObj.AddComponent<MeshFilter>();
        var mr = roadObj.AddComponent<MeshRenderer>();
        mf.mesh = mesh;
        // One shared asphalt material for every ribbon: the shader tiles in world
        // space, so a per-renderer material bought nothing and broke batching.
        mr.sharedMaterial = ResolveRoadMaterial();
        if (!loggedRoadMaterial && mr.sharedMaterial != null)
        {
            loggedRoadMaterial = true;
            Material road = mr.sharedMaterial;
            Debug.Log($"Asphalt surface: shader={road.shader.name} " +
                $"tone={(road.HasProperty("_Color") ? road.GetColor("_Color").ToString() : "n/a")} queue={road.renderQueue}");
        }
        mr.enabled = renderRoadSurface;
        mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        // Building and tree shadows have to fall across the road; without this the
        // street is lit uniformly and reads as a flat ribbon.
        mr.receiveShadows = true;

        if (ShouldUseRoadModelVisuals())
            BuildRoadModelVisuals(roadObj.transform, points, width, way.id);

        if (!string.IsNullOrEmpty(roadTag) && IsValidTag(roadTag))
            roadObj.tag = roadTag;

        if (renderRoadSurface && (drawCenterLines || generateRealisticLaneMarkings))
            BuildLaneMarkings(roadObj.transform, points, way, width);

        if (generateRoadBoundaries && (includeServiceRoadBoundaries || way.GetRoadType() != "service"))
        {
            BuildRoadBoundaries(points, width, way.id);
            if (addPhysicalBoundaryColliders)
                BuildPhysicalRoadBoundaries(roadObj.transform, sourcePoints, sourceNodeIds, data, width, way.id);
        }

        // A gravel verge under the kerb, drawn before the boundary so the raised kerb
        // sits on top. This is the roadside dirt/gravel detail between the asphalt and
        // whatever the map tile shows.
        if (generateRoadShoulders && shoulderWidth > 0.05f)
            BuildRoadShoulders(points, width, way.id);

        if (autoGenerateMedianBarriers && way.ShouldGenerateMedianBarrier())
            BuildMedianBarrier(roadObj.transform, points, way.GetRoadWidth(), way.GetOneWayDirectionSign(), way.id);

        return true;
    }

    Vector3 ResolveSpawnFocus(OSMData data)
    {
        if (MissionManager.Instance != null && MissionManager.Instance.currentRoute != null &&
            MissionManager.Instance.currentRoute.stops != null && MissionManager.Instance.currentRoute.stops.Length > 0 &&
            GPSManager.Instance != null)
        {
            return GPSManager.Instance.GpsToWorld(
                MissionManager.Instance.currentRoute.stops[0].latitude,
                MissionManager.Instance.currentRoute.stops[0].longitude);
        }

        if (CityManager.Instance != null && CityManager.Instance.activeCity != null && GPSManager.Instance != null)
        {
            return GPSManager.Instance.GpsToWorld(
                CityManager.Instance.activeCity.spawnLat,
                CityManager.Instance.activeCity.spawnLon);
        }

        var bus = FindAnyObjectByType<BusController>();
        if (bus != null) return bus.transform.position;

        return Vector3.zero;
    }

    float ApproximateWayDistanceSq(OSMWay way, OSMData data, Vector3 focus)
    {
        if (way == null || way.nodeRefs == null || way.nodeRefs.Count == 0 || GPSManager.Instance == null)
            return float.MaxValue;

        long nodeRef = way.nodeRefs[way.nodeRefs.Count / 2];
        if (!data.nodeMap.TryGetValue(nodeRef, out var node))
            nodeRef = way.nodeRefs[0];
        if (!data.nodeMap.TryGetValue(nodeRef, out node))
            return float.MaxValue;

        Vector3 pos = GPSManager.Instance.GpsToWorld(node.lat, node.lon);
        float dx = pos.x - focus.x;
        float dz = pos.z - focus.z;
        return dx * dx + dz * dz;
    }

    float ApproximateWayViewScore(OSMWay way, OSMData data, Vector3 focus, Vector3 viewForward)
    {
        if (way == null || way.nodeRefs == null || way.nodeRefs.Count == 0 || GPSManager.Instance == null)
            return float.MaxValue;

        long nodeRef = way.nodeRefs[way.nodeRefs.Count / 2];
        if (!data.nodeMap.TryGetValue(nodeRef, out var node))
            nodeRef = way.nodeRefs[0];
        if (!data.nodeMap.TryGetValue(nodeRef, out node))
            return float.MaxValue;

        Vector3 point = GPSManager.Instance.GpsToWorld(node.lat, node.lon);
        return ViewPriority.Score(focus, viewForward, ViewPriority.DefaultFovDegrees, point);
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

    public Mesh BuildRoadSegmentMesh(List<Vector3> points, float width, int laneCount = 2, bool applyCamber = true)
    {
        if (points == null || points.Count < 2) return null;

        int lateralCols = Mathf.Max(2, (applyCamber && width > 4f) ? laneCount + 1 : 2);
        var verts = new List<Vector3>(points.Count * lateralCols);
        var tris  = new List<int>((points.Count - 1) * (lateralCols - 1) * 6);
        var uvs   = new List<Vector2>(points.Count * lateralCols);
        var uv2s  = new List<Vector2>(points.Count * lateralCols);
        var uv3s  = new List<Vector2>(points.Count * lateralCols);

        float halfWidth  = width * 0.5f;
        float alongDistance = 0f;

        for (int i = 0; i < points.Count; i++)
        {
            Vector3 offset = RoadJoinOffset(points, i, halfWidth);

            if (i > 0)
                alongDistance += Vector3.Distance(points[i], points[i - 1]);

            for (int col = 0; col < lateralCols; col++)
            {
                float tNorm = (float)col / (lateralCols - 1); // 0 (left) to 1 (right)
                float tSide = -1f + 2f * tNorm;             // -1 to +1

                Vector3 vPos = points[i] + offset * tSide;

                if (applyCamber && width > 4f)
                {
                    // Realistic cross-slope / road crown (highest in middle, slopes to gutters)
                    float crown = 0.025f * (1f - tSide * tSide);
                    vPos.y += crown;
                }

                verts.Add(vPos);
                uvs.Add(new Vector2(vPos.x, vPos.z) * 0.1f);
                // UV2 is the asphalt shader's road frame: x is the lane coordinate
                // (0 at the left kerb, laneCount at the right) and y is distance in
                // metres along the ribbon. The shader uses it for wheel paths, edge
                // dirt and markings that must not stretch with the way length.
                uv2s.Add(new Vector2(tNorm * laneCount, alongDistance));
                uv3s.Add(new Vector2(laneCount, width));
            }

            if (i > 0)
            {
                for (int col = 0; col < lateralCols - 1; col++)
                {
                    int bl = (i - 1) * lateralCols + col;
                    int br = (i - 1) * lateralCols + (col + 1);
                    int tl = i * lateralCols + col;
                    int tr = i * lateralCols + (col + 1);

                    // CCW winding from above so normals point UP
                    tris.Add(bl); tris.Add(tr); tris.Add(br);
                    tris.Add(bl); tris.Add(tl); tris.Add(tr);
                }
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
        mesh.SetUVs(1, uv2s);
        mesh.SetUVs(2, uv3s);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }

    // ── Road Geometry & Marking Utilities ───────────────────────────────────────

    public static Vector3 RoadJoinOffset(IList<Vector3> points, int i, float halfWidth)
    {
        Vector3 incoming = i > 0 ? points[i] - points[i - 1] : points[1] - points[0];
        Vector3 outgoing = i + 1 < points.Count ? points[i + 1] - points[i] : incoming;
        incoming.y = outgoing.y = 0;
        Vector3 normal = Vector3.Cross(Vector3.up, incoming.normalized);
        Vector3 miter = (normal + Vector3.Cross(Vector3.up, outgoing.normalized)).normalized;
        if (miter.sqrMagnitude < .01f) return normal * halfWidth;
        return miter * Mathf.Min(halfWidth * 2f, halfWidth / Mathf.Max(.5f, Vector3.Dot(miter, normal)));
    }

    public static Vector3 RoadJoinOffsetSigned(IList<Vector3> points, int i, float lateralOffset)
    {
        if (Mathf.Abs(lateralOffset) < 0.001f) return Vector3.zero;
        float absOffset = Mathf.Abs(lateralOffset);
        Vector3 miter = RoadJoinOffset(points, i, absOffset);
        return lateralOffset < 0f ? -miter : miter;
    }

    public static List<Vector3> GetShiftedPoints(IList<Vector3> points, float lateralOffset)
    {
        var shifted = new List<Vector3>(points.Count);
        for (int i = 0; i < points.Count; i++)
        {
            shifted.Add(points[i] + RoadJoinOffsetSigned(points, i, lateralOffset));
        }
        return shifted;
    }

    public class MarkingMeshBuilder
    {
        public List<Vector3> vertices = new List<Vector3>();
        public List<int> triangles = new List<int>();
        public List<Vector2> uvs = new List<Vector2>();
        public List<Vector3> normals = new List<Vector3>();

        static bool warnedUnadvanceableRun;

        /// <summary>
        /// Number of dashed runs that had to be stopped because a float at this
        /// magnitude could not move the walk forward. Zero on healthy city data.
        /// </summary>
        public static int UnadvanceableRunCount { get; private set; }

        public static void ResetStallDiagnostics()
        {
            UnadvanceableRunCount = 0;
            warnedUnadvanceableRun = false;
        }

        /// <summary>
        /// True when <paramref name="step"/> can still move the walk at
        /// <paramref name="t"/> forward. At the end of a run the remaining dash or
        /// gap residual can be half an ulp of <paramref name="t"/>, which rounds
        /// straight back to <paramref name="t"/>: the dash walk then repeated the
        /// same iteration forever and froze whichever road first hit it.
        /// </summary>
        public static bool TryAdvance(float t, float step, out float next)
        {
            next = t + step;
            return next > t;
        }

        public void AddQuad(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
        {
            int baseIdx = vertices.Count;
            vertices.Add(v0);
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);

            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
            normals.Add(Vector3.up);

            uvs.Add(new Vector2(0f, 0f));
            uvs.Add(new Vector2(1f, 0f));
            uvs.Add(new Vector2(0f, 1f));
            uvs.Add(new Vector2(1f, 1f));

            // CCW winding from above: (v0, v3, v1) and (v0, v2, v3)
            triangles.Add(baseIdx + 0);
            triangles.Add(baseIdx + 3);
            triangles.Add(baseIdx + 1);

            triangles.Add(baseIdx + 0);
            triangles.Add(baseIdx + 2);
            triangles.Add(baseIdx + 3);
        }

        public void AddSolidStrip(IList<Vector3> linePoints, float lineWidth, RoadSurfaceIndex surfaceIndex, long wayId, float clearance, float yOffset)
        {
            if (linePoints == null || linePoints.Count < 2) return;
            float halfWidth = lineWidth * 0.5f;

            for (int i = 1; i < linePoints.Count; i++)
            {
                Vector3 pA = linePoints[i - 1];
                Vector3 pB = linePoints[i];
                var runs = surfaceIndex != null
                    ? surfaceIndex.ClearRuns(pA, pB, wayId, clearance)
                    : new List<Vector2> { new Vector2(0f, 1f) };

                foreach (var run in runs)
                {
                    Vector3 start = Vector3.Lerp(pA, pB, run.x);
                    Vector3 end = Vector3.Lerp(pA, pB, run.y);
                    Vector3 delta = end - start;
                    delta.y = 0f;
                    float len = delta.magnitude;
                    if (len < 0.1f) continue;

                    Vector3 dir = delta / len;
                    Vector3 norm = Vector3.Cross(Vector3.up, dir).normalized * halfWidth;

                    Vector3 v0 = start - norm; v0.y = yOffset;
                    Vector3 v1 = start + norm; v1.y = yOffset;
                    Vector3 v2 = end - norm;   v2.y = yOffset;
                    Vector3 v3 = end + norm;   v3.y = yOffset;

                    AddQuad(v0, v1, v2, v3);
                }
            }
        }

        public void AddDashedStrip(IList<Vector3> linePoints, float lineWidth, float dashLen, float gapLen, RoadSurfaceIndex surfaceIndex, long wayId, float clearance, float yOffset)
        {
            if (linePoints == null || linePoints.Count < 2) return;
            float halfWidth = lineWidth * 0.5f;
            float cycle = Mathf.Max(0.5f, dashLen + gapLen);
            float accumDist = 0f;

            for (int i = 1; i < linePoints.Count; i++)
            {
                Vector3 pA = linePoints[i - 1];
                Vector3 pB = linePoints[i];
                var runs = surfaceIndex != null
                    ? surfaceIndex.ClearRuns(pA, pB, wayId, clearance)
                    : new List<Vector2> { new Vector2(0f, 1f) };

                foreach (var run in runs)
                {
                    Vector3 start = Vector3.Lerp(pA, pB, run.x);
                    Vector3 end = Vector3.Lerp(pA, pB, run.y);
                    Vector3 delta = end - start;
                    delta.y = 0f;
                    float len = delta.magnitude;
                    if (len < 0.1f) continue;

                    Vector3 dir = delta / len;
                    Vector3 norm = Vector3.Cross(Vector3.up, dir).normalized * halfWidth;

                    float t = 0f;
                    while (t < len)
                    {
                        float phase = (accumDist + t) % cycle;
                        float step;
                        if (phase < dashLen)
                        {
                            step = Mathf.Min(dashLen - phase, len - t);
                            if (step > 0.05f)
                            {
                                Vector3 d0 = start + dir * t;
                                Vector3 d1 = start + dir * (t + step);

                                Vector3 v0 = d0 - norm; v0.y = yOffset;
                                Vector3 v1 = d0 + norm; v1.y = yOffset;
                                Vector3 v2 = d1 - norm; v2.y = yOffset;
                                Vector3 v3 = d1 + norm; v3.y = yOffset;

                                AddQuad(v0, v1, v2, v3);
                            }
                        }
                        else
                        {
                            // Inside a gap: skip ahead to the next dash.
                            step = Mathf.Min(cycle - phase, len - t);
                        }

                        // A run whose length lands just past a dash boundary leaves a
                        // remainder smaller than a float at this magnitude can add, so
                        // `t + step` returned `t` and this walk spun forever (the
                        // editor froze on the first secondary road that hit it).
                        // Stop the run rather than stall the frame.
                        if (!TryAdvance(t, step, out float advanced))
                        {
                            UnadvanceableRunCount++;
                            if (!warnedUnadvanceableRun)
                            {
                                warnedUnadvanceableRun = true;
                                Debug.LogWarning($"[OSMRoadMeshBuilder] Dashed marking run could not advance: t={t:F6} len={len:F6} step={step} cycle={cycle:F3} dash={dashLen:F3} gap={gapLen:F3} points={linePoints.Count}. Stopping the run.");
                            }
                            break;
                        }

                        t = advanced;
                    }
                    accumDist += len;
                }
            }
        }
    }

    public GameObject BuildLaneMarkings(Transform roadTransform, List<Vector3> points, OSMWay way, float roadWidth)
    {
        if (points == null || points.Count < 2) return null;

        int lanes = Mathf.Max(1, way.GetLaneCount());
        bool isOneWay = way.IsOneWay();
        int fwdLanes = Mathf.Max(1, way.GetForwardLaneCount());
        int bwdLanes = Mathf.Max(1, way.GetBackwardLaneCount());
        bool sharedLane = !isOneWay && lanes <= 1;
        int maxSideLanes = Mathf.Max(1, Mathf.Max(fwdLanes, bwdLanes));
        float laneWidth = way.GetLaneWidthMeters();
        if (isOneWay)
            laneWidth = Mathf.Min(laneWidth, roadWidth / lanes);
        else if (!sharedLane)
            laneWidth = Mathf.Min(laneWidth, roadWidth / (2f * maxSideLanes));
        // Keep edge lines on the traffic envelope rather than out on a shoulder
        // or median, so the painted lanes match the graph's lane centres.
        float halfWidth = sharedLane ? roadWidth * 0.5f
            : isOneWay ? lanes * laneWidth * 0.5f
            : maxSideLanes * laneWidth;
        float markingsY = roadYOffset + (useRoadModelInstances ? roadModelSurfaceLift : 0f) + 0.06f;

        // The graph lays traffic on the country's driving side. Markings have to use
        // the same side, or a right-hand-drive city paints its forward lanes where
        // the traffic does not drive. For a two-way ribbon the centreline is at the
        // road's geometric centre; asymmetric forward/backward counts simply leave
        // more unused width on the side with fewer lanes.
        bool leftTraffic = RoadSpawnResolver.ResolveCountry()?.DrivesOnLeft ?? true;
        // Same side authority the graph and the vehicles use, so the paint cannot
        // end up on the approaching carriageway.
        float forwardSide = TrafficLane.SideSign(leftTraffic);

        var whiteBuilder = new MarkingMeshBuilder();
        var yellowBuilder = new MarkingMeshBuilder();

        // 1. Outer edge lines (fog lines) marking roadway borders
        if (drawOuterEdgeLines && roadWidth >= 4f)
        {
            float edgeInset = Mathf.Clamp(outerEdgeLineOffset, 0.1f, halfWidth * 0.3f);
            var leftEdge = GetShiftedPoints(points, -halfWidth + edgeInset);
            var rightEdge = GetShiftedPoints(points, halfWidth - edgeInset);

            whiteBuilder.AddSolidStrip(leftEdge, laneMarkingWidth, SurfaceIndex, way.id, 0.4f, markingsY);
            whiteBuilder.AddSolidStrip(rightEdge, laneMarkingWidth, SurfaceIndex, way.id, 0.4f, markingsY);
        }

        // 2. Centre markings. A shared single-lane street has no centreline.
        if (!isOneWay && !sharedLane)
        {
            if (maxSideLanes >= 2)
            {
                // Double solid yellow line for a multi-lane carriageway.
                float stripeOffset = 0.12f;
                var centerLeft = GetShiftedPoints(points, -stripeOffset);
                var centerRight = GetShiftedPoints(points, stripeOffset);
                yellowBuilder.AddSolidStrip(centerLeft, 0.12f, SurfaceIndex, way.id, 0.35f, markingsY);
                yellowBuilder.AddSolidStrip(centerRight, 0.12f, SurfaceIndex, way.id, 0.35f, markingsY);
            }
            else
            {
                var center = GetShiftedPoints(points, 0f);
                bool isMainRoad = way.GetRoadType() == "primary" || way.GetRoadType() == "trunk" || way.GetRoadType() == "motorway";
                if (isMainRoad)
                    yellowBuilder.AddSolidStrip(center, 0.16f, SurfaceIndex, way.id, 0.35f, markingsY);
                else
                    yellowBuilder.AddDashedStrip(center, 0.16f, dashLengthMeters, dashGapMeters, SurfaceIndex, way.id, 0.35f, markingsY);
            }
        }

        // 3. Same-direction lane dividers. These follow the traffic graph's lane
        // centres exactly, including asymmetric 2+1 roads and the empty shoulder
        // left on the side with fewer lanes.
        if (!sharedLane)
        {
            if (isOneWay)
            {
                for (int divider = 1; divider < lanes; divider++)
                {
                    float dividerOffset = -halfWidth + laneWidth * divider;
                    var lanePts = GetShiftedPoints(points, dividerOffset);
                    whiteBuilder.AddDashedStrip(lanePts, laneMarkingWidth, dashLengthMeters, dashGapMeters,
                        SurfaceIndex, way.id, 0.35f, markingsY);
                }
            }
            else
            {
                AddLaneDividers(whiteBuilder, points, way.id, forwardSide, laneWidth,
                    fwdLanes, markingsY);
                AddLaneDividers(whiteBuilder, points, way.id, -forwardSide, laneWidth,
                    bwdLanes, markingsY);
            }
        }

        bool hasWhite = whiteBuilder.vertices.Count > 0;
        bool hasYellow = yellowBuilder.vertices.Count > 0;
        if (!hasWhite && !hasYellow) return null;

        var markingsObj = new GameObject("RoadMarkings");
        if (roadTransform != null)
            markingsObj.transform.SetParent(roadTransform, false);
        else if (roadsParent != null)
            markingsObj.transform.SetParent(roadsParent.transform, false);

        if (roadLayer > 0)
            markingsObj.layer = roadLayer;

        var mesh = new Mesh
        {
            name = $"RoadMarkings_{way.id}",
            indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
        };

        var mf = markingsObj.AddComponent<MeshFilter>();
        var mr = markingsObj.AddComponent<MeshRenderer>();
        mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        mr.receiveShadows = false;

        if (hasWhite && hasYellow)
        {
            var combinedVerts = new List<Vector3>(whiteBuilder.vertices.Count + yellowBuilder.vertices.Count);
            combinedVerts.AddRange(whiteBuilder.vertices);
            combinedVerts.AddRange(yellowBuilder.vertices);

            var combinedNorms = new List<Vector3>(whiteBuilder.normals.Count + yellowBuilder.normals.Count);
            combinedNorms.AddRange(whiteBuilder.normals);
            combinedNorms.AddRange(yellowBuilder.normals);

            var combinedUvs = new List<Vector2>(whiteBuilder.uvs.Count + yellowBuilder.uvs.Count);
            combinedUvs.AddRange(whiteBuilder.uvs);
            combinedUvs.AddRange(yellowBuilder.uvs);

            int whiteOffset = whiteBuilder.vertices.Count;
            var yellowTris = new List<int>(yellowBuilder.triangles.Count);
            for (int i = 0; i < yellowBuilder.triangles.Count; i++)
                yellowTris.Add(yellowBuilder.triangles[i] + whiteOffset);

            mesh.SetVertices(combinedVerts);
            mesh.SetNormals(combinedNorms);
            mesh.SetUVs(0, combinedUvs);
            mesh.subMeshCount = 2;
            mesh.SetTriangles(whiteBuilder.triangles, 0);
            mesh.SetTriangles(yellowTris, 1);

            mr.sharedMaterials = new Material[] { ResolveWhiteMarkingMaterial(), ResolveYellowMarkingMaterial() };
        }
        else if (hasWhite)
        {
            mesh.SetVertices(whiteBuilder.vertices);
            mesh.SetNormals(whiteBuilder.normals);
            mesh.SetUVs(0, whiteBuilder.uvs);
            mesh.subMeshCount = 1;
            mesh.SetTriangles(whiteBuilder.triangles, 0);
            mr.sharedMaterial = ResolveWhiteMarkingMaterial();
        }
        else
        {
            mesh.SetVertices(yellowBuilder.vertices);
            mesh.SetNormals(yellowBuilder.normals);
            mesh.SetUVs(0, yellowBuilder.uvs);
            mesh.subMeshCount = 1;
            mesh.SetTriangles(yellowBuilder.triangles, 0);
            mr.sharedMaterial = ResolveYellowMarkingMaterial();
        }

        mesh.RecalculateBounds();
        mf.sharedMesh = mesh;
        return markingsObj;
    }

    void AddLaneDividers(MarkingMeshBuilder builder, List<Vector3> points, long wayId,
        float side, float laneWidth, int lanes, float markingsY)
    {
        for (int divider = 1; divider < lanes; divider++)
        {
            float offset = side * divider * laneWidth;
            var lanePoints = GetShiftedPoints(points, offset);
            builder.AddDashedStrip(lanePoints, laneMarkingWidth, dashLengthMeters, dashGapMeters,
                SurfaceIndex, wayId, 0.35f, markingsY);
        }
    }

    private Material runtimeWhiteMarkingMaterial;
    private Material runtimeYellowMarkingMaterial;

    Material ResolveWhiteMarkingMaterial()
    {
        if (runtimeWhiteMarkingMaterial != null) return runtimeWhiteMarkingMaterial;
        runtimeWhiteMarkingMaterial = new Material(ResolveShader())
        {
            name = "RoadMarking_White",
            color = laneMarkingWhiteColor,
            renderQueue = 2450
        };
        if (runtimeWhiteMarkingMaterial.HasProperty("_Smoothness")) runtimeWhiteMarkingMaterial.SetFloat("_Smoothness", 0.25f);
        if (runtimeWhiteMarkingMaterial.HasProperty("_Metallic")) runtimeWhiteMarkingMaterial.SetFloat("_Metallic", 0f);
        if (runtimeWhiteMarkingMaterial.HasProperty("_Cull")) runtimeWhiteMarkingMaterial.SetFloat("_Cull", (float)UnityEngine.Rendering.CullMode.Off);
        return runtimeWhiteMarkingMaterial;
    }

    Material ResolveYellowMarkingMaterial()
    {
        if (runtimeYellowMarkingMaterial != null) return runtimeYellowMarkingMaterial;
        runtimeYellowMarkingMaterial = new Material(ResolveShader())
        {
            name = "RoadMarking_Yellow",
            color = laneMarkingYellowColor,
            renderQueue = 2450
        };
        if (runtimeYellowMarkingMaterial.HasProperty("_Smoothness")) runtimeYellowMarkingMaterial.SetFloat("_Smoothness", 0.25f);
        if (runtimeYellowMarkingMaterial.HasProperty("_Metallic")) runtimeYellowMarkingMaterial.SetFloat("_Metallic", 0f);
        if (runtimeYellowMarkingMaterial.HasProperty("_Cull")) runtimeYellowMarkingMaterial.SetFloat("_Cull", (float)UnityEngine.Rendering.CullMode.Off);
        return runtimeYellowMarkingMaterial;
    }

    void BuildCenterLine(List<Vector3> points, long wayId)
    {
        for (int i = 1; i < points.Count; i++)
            foreach (var run in SurfaceIndex.ClearRuns(points[i - 1], points[i], wayId, 1f))
                BuildCenterLinePiece(new List<Vector3> { Vector3.Lerp(points[i - 1], points[i], run.x), Vector3.Lerp(points[i - 1], points[i], run.y) });
    }

    void BuildCenterLinePiece(List<Vector3> points, Color? overrideColor = null, float? overrideWidth = null)
    {
        if (points == null || points.Count < 2) return;

        var go = new GameObject("CenterLine");
        go.transform.SetParent(roadsParent != null ? roadsParent.transform : transform, false);

        var lr = go.AddComponent<LineRenderer>();
        lr.positionCount  = points.Count;
        lr.useWorldSpace  = true;
        lr.startWidth     = overrideWidth ?? centerLineWidth;
        lr.endWidth       = overrideWidth ?? centerLineWidth;
        lr.numCapVertices = 2;
        lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lr.receiveShadows    = false;

        var mat = new Material(ResolveShader());
        mat.color = overrideColor ?? centerLineColor;
        mat.renderQueue = 2450;
        lr.material = mat;

        var worldPts = new Vector3[points.Count];
        for (int i = 0; i < points.Count; i++)
        {
            var p = points[i];
            p.y = roadYOffset + (useRoadModelInstances ? roadModelSurfaceLift : 0f) + 0.06f;
            worldPts[i] = p;
        }
        lr.SetPositions(worldPts);
    }

    void BuildRoadBoundaries(List<Vector3> points, float roadWidth, long wayId)
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
            Vector3 side = RoadJoinOffset(points, i, offset);
            Vector3 basePoint = points[i];
            basePoint.y = roadYOffset + boundaryHeight;
            left[i] = basePoint - side;
            right[i] = basePoint + side;
        }

        var combine = new List<CombineInstance>();
        foreach (var edge in new[] { left, right })
            for (int i = 1; i < edge.Length; i++)
                foreach (var run in SurfaceIndex.ClearRuns(edge[i - 1], edge[i], wayId, Mathf.Max(2f, junctionOpeningMeters * .5f)))
                {
                    var mesh = BuildRoadSegmentMesh(new List<Vector3> { Vector3.Lerp(edge[i - 1], edge[i], run.x), Vector3.Lerp(edge[i - 1], edge[i], run.y) }, Mathf.Max(.1f, boundaryWidth));
                    if (mesh != null) combine.Add(new CombineInstance { mesh = mesh, transform = Matrix4x4.identity });
                }
        if (combine.Count == 0) return;
        Mesh combined = new Mesh { name = "RoadBoundaries", indexFormat = UnityEngine.Rendering.IndexFormat.UInt32 };
        combined.CombineMeshes(combine.ToArray(), true, false);
        foreach (var item in combine) Destroy(item.mesh);

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

    /// <summary>
    /// A gravel/dirt verge strip either side of the asphalt, between the road edge
    /// and the kerb. It is drawn just below the ribbon so the seam where a road meets
    /// the map tile is broken up by a rough, dirtier surface rather than a hard line.
    /// No collider: the driving surface stays the road width.
    /// </summary>
    void BuildRoadShoulders(List<Vector3> points, float roadWidth, long wayId)
    {
        if (points == null || points.Count < 2) return;

        Vector3[] left = new Vector3[points.Count];
        Vector3[] right = new Vector3[points.Count];
        float offset = roadWidth * .5f + shoulderWidth * .5f;
        for (int i = 0; i < points.Count; i++)
        {
            Vector3 side = RoadJoinOffset(points, i, offset);
            Vector3 basePoint = points[i];
            // One centimetre below the ribbon, still eleven centimetres above
            // the map tile. The old 5 mm left the verge fighting the tile at
            // distance, which showed as flickering dirt beside the road, while
            // dropping it further would open a visible step at the kerb.
            basePoint.y = roadYOffset - 0.01f;
            left[i] = basePoint - side;
            right[i] = basePoint + side;
        }

        var combine = new List<CombineInstance>();
        foreach (var edge in new[] { left, right })
            for (int i = 1; i < edge.Length; i++)
                foreach (var run in SurfaceIndex.ClearRuns(edge[i - 1], edge[i], wayId, Mathf.Max(2f, junctionOpeningMeters * .5f)))
                {
                    var mesh = BuildRoadSegmentMesh(
                        new List<Vector3> { Vector3.Lerp(edge[i - 1], edge[i], run.x), Vector3.Lerp(edge[i - 1], edge[i], run.y) },
                        Mathf.Max(.1f, shoulderWidth));
                    if (mesh != null) combine.Add(new CombineInstance { mesh = mesh, transform = Matrix4x4.identity });
                }
        if (combine.Count == 0) return;

        Mesh combined = new Mesh { name = "RoadShoulders", indexFormat = UnityEngine.Rendering.IndexFormat.UInt32 };
        combined.CombineMeshes(combine.ToArray(), true, false);
        foreach (var item in combine) Destroy(item.mesh);

        GameObject shoulder = new GameObject("RoadShoulders");
        shoulder.transform.SetParent(roadsParent.transform, false);
        MeshFilter filter = shoulder.AddComponent<MeshFilter>();
        MeshRenderer renderer = shoulder.AddComponent<MeshRenderer>();
        filter.sharedMesh = combined;
        renderer.sharedMaterial = ResolveShoulderMaterial();
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = true;
    }

    Material ResolveShoulderMaterial()
    {
        if (shoulderMaterial != null) return shoulderMaterial;
        if (runtimeShoulderMaterial != null) return runtimeShoulderMaterial;
        runtimeShoulderMaterial = new Material(ResolveShader()) { color = shoulderColor, renderQueue = 2105 };
        // The asphalt grain atlas doubles as loose-gravel mottling: the UVs from
        // BuildRoadSegmentMesh are world-space, so the patches never stretch.
        if (runtimeShoulderMaterial.HasProperty("_MainTex"))
            runtimeShoulderMaterial.SetTexture("_MainTex", UrbanMaterials.AsphaltAlbedo);
        if (runtimeShoulderMaterial.HasProperty("_Glossiness")) runtimeShoulderMaterial.SetFloat("_Glossiness", .03f);
        if (runtimeShoulderMaterial.HasProperty("_Smoothness")) runtimeShoulderMaterial.SetFloat("_Smoothness", .03f);
        if (runtimeShoulderMaterial.HasProperty("_Metallic")) runtimeShoulderMaterial.SetFloat("_Metallic", 0f);
        return runtimeShoulderMaterial;
    }

    void BuildPhysicalRoadBoundaries(Transform parent, List<Vector3> points, List<long> nodeIds, OSMData data, float roadWidth, long wayId)
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
            Vector3 right = Vector3.Cross(Vector3.up, direction);
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            for (int side = -1; side <= 1; side += 2)
            {
                Vector3 a = from + RoadJoinOffset(points, i - 1, sideOffset) * side;
                Vector3 b = to + RoadJoinOffset(points, i, sideOffset) * side;
                Quaternion edgeRotation = Quaternion.LookRotation((b - a).normalized, Vector3.up);
                foreach (var run in SurfaceIndex.ClearRuns(a, b, wayId, Mathf.Max(2f, junctionOpeningMeters * .5f)))
                {
                    float usableLength = Vector3.Distance(a, b) * (run.y - run.x);
                    if (usableLength < .5f) continue;
                    Vector3 center = Vector3.Lerp(a, b, (run.x + run.y) * .5f);
                    center.y = roadYOffset + colliderHeight * .5f;
                    CreateBoundaryCollider(root.transform, $"Edge_{side}_{i}", center, edgeRotation, colliderWidth, colliderHeight, usableLength);
                }
            }
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
            roadVisualPrefab = Resources.Load<GameObject>("RoadsGenerated/radtypesremastered_297a0871");
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
        ImportedVehicleVisualRepair.DisableEmbeddedCameras(sample.transform);
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

    void BuildRoadModelVisuals(Transform parent, List<Vector3> points, float width, long wayId)
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
            // Rectangular imported pieces cannot bend. Use the continuous
            // generated mesh at corners instead of overlapping rigid tiles.
            if ((i > 1 && Vector3.Angle(direction, from - points[i - 2]) > 5f) ||
                (i + 1 < points.Count && Vector3.Angle(direction, points[i + 1] - to) > 5f)) continue;
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
                Vector3 side = Vector3.Cross(Vector3.up, direction) * width * .5f;
                Vector3 along = direction * pieceLength * .5f;
                var footprint = new[] { position - along - side, position + along - side, position + along + side, position - along + side };
                if (SurfaceIndex.OverlapsFootprint(footprint, 2f, wayId)) continue;

                var visual = Instantiate(roadVisualPrefab, position, rotation, parent);
                ImportedVehicleVisualRepair.DisableEmbeddedCameras(visual.transform);
                // Only the generated road surface/kerbs own driveable physics.
                foreach (var collider in visual.GetComponentsInChildren<Collider>(true)) collider.enabled = false;
                foreach (var body in visual.GetComponentsInChildren<Rigidbody>(true)) { body.isKinematic = true; body.detectCollisions = false; }
                visual.name = $"RoadVisual_{i}_{pieceIndex}";
                visual.transform.localScale = GetScaledRoadVisualScale(visual.transform.localScale, widthScale, lengthScale);
                ApplyLayerRecursively(visual, parent.gameObject.layer);

                foreach (var renderer in visual.GetComponentsInChildren<Renderer>(true))
                {
                    renderer.enabled = renderRoadSurface;
                    renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    // The imported piece covers the generated ribbon, so if it did not
                    // receive shadows the street would lose every building shadow.
                    renderer.receiveShadows = true;

                    if (overrideRoadModelMaterials)
                        renderer.material = roadMaterial != null ? roadMaterial : CreateDefaultRoadMaterial();
                }
            }
        }
    }

    void BuildMedianBarrier(Transform parent, List<Vector3> points, float roadWidth, int directionSign, long wayId)
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
                // An OSM one-way way represents an entire carriageway. Its
                // centreline is NOT the median between opposing traffic.
                bool leftTraffic = RoadSpawnResolver.ResolveCountry()?.DrivesOnLeft == true;
                // The median sits on the far side of the centreline from the driven
                // carriageway, which is the negation of the shared kerb-side sign.
                float medianSide = -TrafficLane.SideSign(leftTraffic) *
                    (directionSign < 0 ? -1f : 1f);
                Vector3 position = from + direction * distance + Vector3.Cross(Vector3.up, direction) * medianSide * (roadWidth * .5f + .4f);
                position.y = roadYOffset + medianBarrierLift;
                if (SurfaceIndex.Contains(position, Mathf.Max(3f, medianBarrierSpacingMeters), wayId)) continue;

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

    bool IsModelForwardAlongX()
    {
        if (roadModelForwardAxis == RoadModelForwardAxis.XAxis) return true;
        if (hasRoadVisualBounds && roadVisualBounds.size.x > roadVisualBounds.size.z * 1.35f)
            return true;
        return false;
    }

    Quaternion GetRoadModelAxisRotation()
    {
        return IsModelForwardAlongX()
            ? Quaternion.Euler(0f, -90f, 0f)
            : Quaternion.identity;
    }

    float GetRoadModelLength()
    {
        return IsModelForwardAlongX()
            ? roadVisualBounds.size.x
            : roadVisualBounds.size.z;
    }

    float GetRoadModelWidth()
    {
        return IsModelForwardAlongX()
            ? roadVisualBounds.size.z
            : roadVisualBounds.size.x;
    }

    Vector3 GetScaledRoadVisualScale(Vector3 currentScale, float widthScale, float lengthScale)
    {
        if (IsModelForwardAlongX())
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

    Shader ResolveShader() => BuiltInShaders.Lit;

    Material CreateDefaultRoadMaterial() => UrbanMaterials.Road;

    /// <summary>
    /// The shared asphalt material. An explicit material in the inspector still wins,
    /// but the default keeps one shader and one texture for every road in the city.
    /// </summary>
    Material ResolveRoadMaterial() => roadMaterial != null ? roadMaterial : UrbanMaterials.Road;

    public static Texture2D GetOrCreateAsphaltTexture()
    {
        if (runtimeAsphaltTexture != null) return runtimeAsphaltTexture;
        const int size = 128;
        runtimeAsphaltTexture = new Texture2D(size, size, TextureFormat.RGB24, true)
        {
            name = "Generated Asphalt",
            wrapMode = TextureWrapMode.Repeat,
            filterMode = FilterMode.Bilinear,
            hideFlags = HideFlags.DontSave
        };
        var pixels = new Color32[size * size];
        for (int y = 0; y < size; y++)
        for (int x = 0; x < size; x++)
        {
            uint hash = (uint)(x * 374761393 + y * 668265263); hash = (hash ^ (hash >> 13)) * 1274126177u;
            byte grain = (byte)(43 + (hash & 19));
            if ((hash & 255) < 5) grain = (byte)Mathf.Max(24, grain - 16);
            pixels[y * size + x] = new Color32(grain, grain, (byte)Mathf.Max(0, grain - 2), 255);
        }
        runtimeAsphaltTexture.SetPixels32(pixels);
        runtimeAsphaltTexture.Apply(true, true);
        return runtimeAsphaltTexture;
    }
}
