using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Manages physical transit depots where player buses start routes and shifts
/// instead of randomly spawning in the middle of traffic.
/// Integrates assets from Assets/unity models/parking lot.
///
/// A placed depot owns three things the rest of the world has to respect:
///   1. A YARD ZONE. Building spawners ask <see cref="IsInsideDepot"/> /
///      <see cref="OverlapsDepotFootprint"/> before raising anything, and whatever
///      was already standing inside the yard is evicted when the depot is placed -
///      the yard is where the player parks and manoeuvres, so it stays empty.
///   2. Its own ROAD NETWORK. A one-way entry aisle, a U-turn and a one-way exit
///      aisle that both reach the street kerb, built as real road meshes with
///      colliders and registered in <see cref="RoadSurfaceIndex"/>, so building
///      infill, street furniture and spawn clearing all treat them as road.
///   3. Props and imported structures placed out of those lanes, so the entry and
///      exit the player is told to use are actually drivable. A prop is judged by
///      its footprint against the lanes themselves, not only by its size.
///   4. Yard traffic that uses those lanes (see <see cref="DepotTrafficSpawner"/>),
///      held off the aisles whenever the player's bus is inside the yard.
/// </summary>
public class BusDepotManager : MonoBehaviour
{
    public static BusDepotManager Instance { get; private set; }

    [Header("Depot Configuration")]
    public float depotOffsetFromRoad = 22f;
    public float depotAlongRoadOffset = -12f;
    public int totalBays = 4;
    public float bayWidth = 4.6f;
    public float bayLength = 14f;

    [Header("Yard Layout")]
    [Tooltip("Yard width, measured parallel to the kerb the depot serves.")]
    public float yardWidth = 34f;
    [Tooltip("Yard depth, measured away from the kerb.")]
    public float yardDepth = 28f;
    [Tooltip("Depth of the parking bay row from the yard centre. Must stay behind the yard roads.")]
    public float bayRowZ = -6f;
    [Tooltip("Width of each one-way yard aisle: wide enough to swing a bus out of a bay.")]
    public float depotRoadWidth = 5f;
    [Tooltip("Largest imported depot building the yard will accept, in metres per axis.")]
    public float maxDepotStructureSize = 20f;
    [Tooltip("Ambient traffic that drives the yard's entry and exit lanes.")]
    public bool spawnDepotTraffic = true;

    [Header("Runtime State")]
    public GameObject currentDepot;
    public Vector3 currentBay1Position;
    public Quaternion currentBay1Rotation;
    public Vector3 currentExitPosition;
    public bool hasDepartedDepot;
    [Tooltip("Zone handle registered for the current yard; -1 when no yard is placed.")]
    public int currentZoneHandle = -1;
    [Tooltip("City buildings removed from the yard when it was last placed.")]
    public int lastEvictedBuildingCount;
    [Tooltip("Where the yard's driveways meet the street, kept for the yard traffic circuit.")]
    public Vector3 currentKerbPosition;
    /// <summary>The yard's ambient traffic, while a depot owns the ground.</summary>
    public DepotTrafficSpawner currentTraffic;

    /// <summary>Centre lines of the current yard's roads, in build order (world space).</summary>
    public IList<Vector3[]> CurrentDepotRoads => depotRoadLines;

    private BusController activeBus;
    private Material tarmacMaterial;
    private Material stripeMaterial;
    private Material curbMaterial;
    private long currentRoadWayId = long.MinValue;
    private readonly List<Vector3[]> depotRoadLines = new List<Vector3[]>();
    private static long nextDepotRoadWayId = -910000L;

    // Yard surfaces sit a hair below the road plane (roadYOffset) so a yard road
    // can be drawn on top of the apron without the two z-fighting, while still
    // clearing the map tiles underneath.
    private const float ApronTopOffset = -0.03f;
    private const float ApronThickness = 0.1f;
    // The yard aisles overlap the kerb and a little of the street. Both surfaces sit
    // at roadYOffset, so without a hair of lift the two coplanar meshes z-fight in the
    // overlap; 12 mm is invisible on a bus wheel but settles the depth test.
    private const float DrivewayLift = 0.012f;

    [Header("Imported model fallback")]
    [Tooltip("Applied only to untextured near-white materials on imported depot props (concrete/steel tone).")]
    public Color importedPropFallbackColor = new Color(0.66f, 0.66f, 0.63f, 1f);

    #region Yard zones

    /// <summary>
    /// World-space rectangle of a placed depot yard. Stored rather than derived
    /// because the yard follows the route start, which is only known at run time.
    /// </summary>
    public struct DepotZone
    {
        public Vector3 center;
        public Quaternion rotation;
        /// <summary>Half extents along the zone's local X (width) and Z (depth).</summary>
        public Vector2 halfExtents;
        public bool active;
    }

    static readonly List<DepotZone> zones = new List<DepotZone>();

    /// <summary>Number of yards currently placed. Used by the depot tests.</summary>
    public static int PlacedZoneCount
    {
        get
        {
            int count = 0;
            for (int i = 0; i < zones.Count; i++) if (zones[i].active) count++;
            return count;
        }
    }

    /// <summary>Forgets every yard. Test isolation only; the scene places its own.</summary>
    public static void ResetZones() => zones.Clear();

    public static int RegisterZone(Vector3 center, Quaternion rotation, Vector2 halfExtents)
    {
        var zone = new DepotZone { center = center, rotation = rotation, halfExtents = halfExtents, active = true };
        for (int i = 0; i < zones.Count; i++)
            if (!zones[i].active)
            {
                zones[i] = zone;
                return i;
            }
        zones.Add(zone);
        return zones.Count - 1;
    }

    public static void UnregisterZone(int handle)
    {
        if (handle < 0 || handle >= zones.Count) return;
        var zone = zones[handle];
        zone.active = false;
        zones[handle] = zone;
    }

    /// <summary>
    /// True when a world position falls inside a placed depot yard. Every building
    /// spawner checks this so nothing is raised in the yard the bus drives in.
    /// </summary>
    public static bool IsInsideDepot(Vector3 worldPosition, float padding = 0f)
    {
        for (int i = 0; i < zones.Count; i++)
        {
            if (!zones[i].active) continue;
            Vector3 local = ToZoneLocal(zones[i], worldPosition);
            if (Mathf.Abs(local.x) <= zones[i].halfExtents.x + padding &&
                Mathf.Abs(local.z) <= zones[i].halfExtents.y + padding) return true;
        }
        return false;
    }

    /// <summary>
    /// True when a building footprint overlaps a yard at all - a corner inside,
    /// a yard corner inside the footprint, or an edge crossing between the two.
    /// </summary>
    public static bool OverlapsDepotFootprint(IList<Vector3> polygon, float padding = 0f)
    {
        if (polygon == null || polygon.Count < 3) return false;
        for (int i = 0; i < zones.Count; i++)
        {
            if (!zones[i].active) continue;
            var zone = zones[i];
            for (int p = 0; p < polygon.Count; p++)
            {
                Vector3 local = ToZoneLocal(zone, polygon[p]);
                if (Mathf.Abs(local.x) <= zone.halfExtents.x + padding &&
                    Mathf.Abs(local.z) <= zone.halfExtents.y + padding) return true;
            }

            Vector3[] corners = ZoneCorners(zone, padding);
            for (int c = 0; c < corners.Length; c++)
                if (LiveCityStreamer.OverlapsPolygon(corners[c], 0f, polygon)) return true;

            for (int c = 0; c < corners.Length; c++)
            for (int p = 0; p < polygon.Count; p++)
            {
                Vector3 edgeStart = corners[c];
                Vector3 edgeEnd = corners[(c + 1) % corners.Length];
                Vector3 footprintStart = polygon[p];
                Vector3 footprintEnd = polygon[(p + 1) % polygon.Count];
                if (RoadSurfaceIndex.SegmentDistanceSquared(edgeStart, edgeEnd, footprintStart, footprintEnd) < 0.0001f)
                    return true;
            }
        }
        return false;
    }

    static Vector3 ToZoneLocal(DepotZone zone, Vector3 world) => Quaternion.Inverse(zone.rotation) * (world - zone.center);

    static Vector3[] ZoneCorners(DepotZone zone, float padding)
    {
        float x = zone.halfExtents.x + padding;
        float z = zone.halfExtents.y + padding;
        return new[]
        {
            zone.center + zone.rotation * new Vector3(-x, 0f, -z),
            zone.center + zone.rotation * new Vector3(-x, 0f, z),
            zone.center + zone.rotation * new Vector3(x, 0f, z),
            zone.center + zone.rotation * new Vector3(x, 0f, -z)
        };
    }

    #endregion

    #region Land clearance

    /// <summary>
    /// True when a world-space footprint crosses one of the yard's driving lanes.
    ///
    /// A prop can fit any size budget and still stand in the exit aisle, so size is
    /// not the question a depot prop has to answer - this is. Clearance is measured
    /// in plan view against the lane corridor, deliberately: the bus drives at
    /// ground level, so a structure standing over an aisle blocks it however tall
    /// the structure is.
    /// </summary>
    public static bool FootprintOverlapsLanes(IList<Vector3> footprint, IList<Vector3[]> lanes,
        float laneHalfWidth, float padding = 0f)
    {
        if (footprint == null || footprint.Count < 3 || lanes == null) return false;
        float clearance = Mathf.Max(0.05f, laneHalfWidth + padding);
        float clearanceSquared = clearance * clearance;

        for (int lane = 0; lane < lanes.Count; lane++)
        {
            var line = lanes[lane];
            if (line == null) continue;
            for (int i = 1; i < line.Length; i++)
            {
                Vector3 from = line[i - 1];
                Vector3 to = line[i];

                for (int p = 0; p < footprint.Count; p++)
                {
                    Vector3 nearest = RoadSpawnResolver.ProjectOnSegment(footprint[p], from, to);
                    if (XZDistanceSquared(footprint[p], nearest) <= clearanceSquared) return true;
                }

                // A lane that ends inside the footprint - a driveway running under a
                // building, say - is an overlap even when no corner is anywhere near it.
                if (LiveCityStreamer.OverlapsPolygon(from, 0f, footprint) ||
                    LiveCityStreamer.OverlapsPolygon(to, 0f, footprint)) return true;

                for (int p = 0; p < footprint.Count; p++)
                    if (RoadSurfaceIndex.SegmentDistanceSquared(from, to, footprint[p], footprint[(p + 1) % footprint.Count]) < clearanceSquared)
                        return true;
            }
        }
        return false;
    }

    static float XZDistanceSquared(Vector3 a, Vector3 b)
    {
        float dx = a.x - b.x, dz = a.z - b.z;
        return dx * dx + dz * dz;
    }

    /// <summary>
    /// Plan-view footprint of a placed prop, taken from what it actually renders and
    /// collides with, so a scaled or rotated imported model is judged as it stands
    /// rather than as it was authored. The world box is a superset of the prop, which
    /// is the safe direction to be wrong in.
    /// </summary>
    void PropFootprint(GameObject prop, List<Vector3> footprint)
    {
        footprint.Clear();
        if (prop == null) return;
        // Collider bounds only follow a transform after a sync, and this runs while a
        // depot is being built rather than from a physics step.
        Physics.SyncTransforms();

        Bounds bounds = new Bounds();
        bool measured = false;
        foreach (var renderer in prop.GetComponentsInChildren<Renderer>(true))
        {
            if (!measured) { bounds = renderer.bounds; measured = true; }
            else bounds.Encapsulate(renderer.bounds);
        }
        foreach (var collider in prop.GetComponentsInChildren<Collider>(true))
        {
            if (collider == null || !collider.enabled || collider.isTrigger) continue;
            if (!measured) { bounds = collider.bounds; measured = true; }
            else bounds.Encapsulate(collider.bounds);
        }
        if (!measured) return;

        footprint.Add(new Vector3(bounds.min.x, 0f, bounds.min.z));
        footprint.Add(new Vector3(bounds.max.x, 0f, bounds.min.z));
        footprint.Add(new Vector3(bounds.max.x, 0f, bounds.max.z));
        footprint.Add(new Vector3(bounds.min.x, 0f, bounds.max.z));
    }

    /// <summary>
    /// True when a placed yard prop would stand in one of the yard's own lanes.
    /// This is what the placement rules ask before they keep a prop: the depot tells
    /// the player to leave through those lanes, so nothing may sit in them.
    /// </summary>
    public bool PropStandsInYardLane(GameObject prop, float padding = 0.3f)
    {
        if (prop == null || depotRoadLines.Count == 0) return false;
        var footprint = new List<Vector3>(4);
        PropFootprint(prop, footprint);
        if (footprint.Count < 3) return false;
        return FootprintOverlapsLanes(footprint, depotRoadLines, depotRoadWidth * 0.5f, padding);
    }

    /// <summary>
    /// True when a placed prop's footprint reaches the street carriageway. The
    /// yard's own roads are excluded - those driveways are meant to meet the road.
    /// This is the street-side twin of <see cref="PropStandsInYardLane"/>: a prop
    /// must stand on the yard, not on the city it was built beside.
    /// </summary>
    public bool PropReachesStreet(GameObject prop, float clearance = 0.2f)
    {
        var roads = OSMRoadMeshBuilder.Resolve()?.SurfaceIndex;
        if (roads == null || prop == null) return false;
        var footprint = new List<Vector3>(4);
        PropFootprint(prop, footprint);
        if (footprint.Count < 3) return false;
        long exclude = currentRoadWayId != long.MinValue ? currentRoadWayId : long.MinValue;
        return roads.OverlapsFootprint(footprint, clearance, exclude);
    }

    /// <summary>
    /// Street-clearance audit for the acceptance run: the yard props whose footprint
    /// reaches the carriageway. Healthy is empty. The yard's own asphalt, driveways,
    /// dropped kerbs and ambient traffic are the deliberate street join and are
    /// skipped; the apron is the yard surface itself, so its bounding box is the yard,
    /// not a prop that could be moved off the road.
    /// </summary>
    public List<string> DepotPropsReachingStreet()
    {
        var offenders = new List<string>();
        if (currentDepot == null) return offenders;
        foreach (Transform child in currentDepot.transform)
        {
            string name = child.name;
            if (name.StartsWith("Depot_Entry_Road") || name.StartsWith("Depot_Turn_Road") ||
                name.StartsWith("Depot_Exit_Road") || name.StartsWith("Depot_Apron") ||
                name.Contains("Ramp") || name.Contains("DroppedKerb") ||
                name.StartsWith("Depot_Traffic")) continue;
            if (PropReachesStreet(child.gameObject))
                offenders.Add($"{name}@{child.position.x:F1},{child.position.z:F1}");
        }
        return offenders;
    }

    /// <summary>
    /// Slides the yard away from the kerb until its whole apron footprint clears
    /// every street segment. The yard is wide enough that too shallow an offset
    /// would otherwise lay the apron, bays and wheel stops across the carriageway.
    /// </summary>
    Vector3 PushYardClearOfStreet(RoadSurfaceIndex roads, Vector3 center, Vector3 outward,
        Quaternion facing, float groundY)
    {
        if (roads == null) return center;
        var footprint = new List<Vector3>(4);
        for (int attempt = 0; attempt < 16; attempt++)
        {
            YardFootprint(center, facing, footprint);
            if (!roads.OverlapsFootprint(footprint, 0.4f)) return center;
            center += outward * 2f;
            center.y = groundY;
        }
        return center;
    }

    /// <summary>Plan-view corners of the yard apron in world space.</summary>
    void YardFootprint(Vector3 center, Quaternion facing, List<Vector3> footprint)
    {
        float hw = yardWidth * 0.5f;
        float hd = yardDepth * 0.5f;
        footprint.Clear();
        footprint.Add(center + facing * new Vector3(-hw, 0f, -hd));
        footprint.Add(center + facing * new Vector3(hw, 0f, -hd));
        footprint.Add(center + facing * new Vector3(hw, 0f, hd));
        footprint.Add(center + facing * new Vector3(-hw, 0f, hd));
    }

    /// <summary>
    /// Names of the yard props currently standing in a lane. Healthy is empty: it is
    /// the invariant the depot's own placement rules are meant to hold, so the tests
    /// and the headless smoke run assert on it directly.
    /// </summary>
    public List<string> PropsStandingInYardLanes(float padding = 0.3f)
    {
        var offenders = new List<string>();
        if (currentDepot == null) return offenders;
        foreach (Transform child in currentDepot.transform)
            if (IsYardProp(child.name) && PropStandsInYardLane(child.gameObject, padding))
                offenders.Add(child.name);
        return offenders;
    }

    /// <summary>Yard children that are structure or scenery rather than the road surface itself.</summary>
    static bool IsYardProp(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        if (name.StartsWith("Depot_Entry_Road") || name.StartsWith("Depot_Turn_Road") || name.StartsWith("Depot_Exit_Road")) return false;
        if (name.StartsWith("Depot_Entry_Ramp") || name.StartsWith("Depot_Exit_Ramp")) return false;
        if (name.StartsWith("Depot_Apron")) return false;
        if (name.StartsWith("Depot_Traffic")) return false;   // yard traffic belongs on the lanes
        if (name.StartsWith("Bay_Stripe")) return false;      // paint, not structure
        return true;
    }

    #endregion

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap()
    {
        EnsureExists();
    }

    public static BusDepotManager EnsureExists()
    {
        if (Instance != null) return Instance;
        BusDepotManager existing = FindAnyObjectByType<BusDepotManager>();
        if (existing != null) return existing;
        GameObject go = new GameObject("BusDepotManager");
        return go.AddComponent<BusDepotManager>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroySafe(gameObject);
            return;
        }
        Instance = this;
        CreateMaterials();
    }

    /// <summary>
    /// Destroys an object in both play and edit mode. Unity logs an error if
    /// <see cref="Object.Destroy"/> is called from edit mode (for example in an
    /// EditMode test that builds a depot), so fall back to DestroyImmediate.
    /// </summary>
    public static void DestroySafe(Object obj)
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

    void CreateMaterials()
    {
        Shader standardShader = BuiltInShaders.Lit;

        tarmacMaterial = new Material(standardShader) { name = "Depot_Tarmac_Mat" };
        tarmacMaterial.color = new Color(0.12f, 0.13f, 0.14f, 1f);
        if (tarmacMaterial.HasProperty("_Smoothness")) tarmacMaterial.SetFloat("_Smoothness", 0.15f);

        stripeMaterial = new Material(standardShader) { name = "Depot_Stripe_Yellow" };
        stripeMaterial.color = new Color(0.96f, 0.78f, 0.12f, 1f);
        if (stripeMaterial.HasProperty("_Smoothness")) stripeMaterial.SetFloat("_Smoothness", 0.3f);

        curbMaterial = new Material(standardShader) { name = "Depot_Curb_Concrete" };
        curbMaterial.color = new Color(0.55f, 0.55f, 0.53f, 1f);
    }

    /// <summary>
    /// Spawns a depot adjacent to the route's starting point and resolves the spawn transform for Bay 1.
    /// </summary>
    public bool TrySpawnDepotAndResolveBay(BusRoute route, Vector3 requestedStartPos, BusController bus,
        out Vector3 bayPosition, out Quaternion bayRotation, out Vector3 exitPos)
    {
        bayPosition = requestedStartPos;
        bayRotation = bus != null ? bus.transform.rotation : Quaternion.identity;
        exitPos = requestedStartPos;
        activeBus = bus;
        hasDepartedDepot = false;

        if (route == null) return false;

        // Resolve road heading & roadside kerb
        Vector3 heading = bus != null ? bus.transform.forward : Vector3.forward;
        if (route.stops != null && route.stops.Length > 1 && GPSManager.Instance != null)
        {
            Vector3 nextStop = GPSManager.Instance.GpsToWorld(route.stops[1].latitude, route.stops[1].longitude);
            Vector3 diff = nextStop - requestedStartPos;
            diff.y = 0f;
            if (diff.sqrMagnitude > 1f) heading = diff.normalized;
        }

        bool leftTraffic = RoadSpawnResolver.ResolveCountry()?.DrivesOnLeft ?? true;
        Vector3 kerbPos = requestedStartPos;
        Quaternion roadFacing = Quaternion.LookRotation(heading, Vector3.up);

        var roadBuilder = OSMRoadMeshBuilder.Resolve();
        var roads = roadBuilder?.SurfaceIndex;
        if (roads != null && roads.TryRoadside(requestedStartPos, heading, leftTraffic, out var resolvedKerb, out var resolvedFacing))
        {
            kerbPos = resolvedKerb;
            roadFacing = resolvedFacing;
        }

        float groundY = roadBuilder != null ? roadBuilder.roadYOffset : kerbPos.y;
        kerbPos.y = groundY;

        Vector3 roadFwd = roadFacing * Vector3.forward;
        Vector3 outward = roadFacing * (leftTraffic ? Vector3.left : Vector3.right);
        // +1 when the street lies on the yard's local +X, matching the aisle
        // builder. Yard structures are placed on the far side of the depot from it.
        float streetSide = Mathf.Sign(Vector3.Dot(roadFacing * Vector3.right, -outward));
        if (streetSide == 0f) streetSide = -1f;

        // Position depot yard adjacent to kerb
        Vector3 depotCenter = kerbPos + outward * depotOffsetFromRoad + roadFwd * depotAlongRoadOffset;
        depotCenter.y = groundY;
        // A yard pushed too close would lay its apron and bays across the
        // carriageway. Walk it out until its footprint misses every street
        // segment; the yard's own roads are laid later, so nothing needs
        // excluding yet.
        depotCenter = PushYardClearOfStreet(roads, depotCenter, outward, roadFacing, groundY);

        // Clean up any old depot, its zone and its road registrations first, so a
        // re-placed depot cannot leave the previous yard's exclusion behind.
        ReleaseCurrentDepot();

        // Claim the yard before building anything: every building spawner that runs
        // from here on - mapped, lazy or fictional - sees the zone and stays out.
        currentZoneHandle = RegisterZone(depotCenter, roadFacing,
            new Vector2(yardWidth * 0.5f + 2f, yardDepth * 0.5f + 2f));

        currentDepot = new GameObject("TransitDepot_Origin");
        currentDepot.transform.position = depotCenter;
        currentDepot.transform.rotation = roadFacing;

        // 1. Depot paved apron
        BuildDepotApron(currentDepot.transform);

        // 2. Yard roads: one-way entry and exit aisles meeting the street kerb
        BuildDepotRoadNetwork(currentDepot.transform, kerbPos, roadFacing);

        // Open the street kerb where the driveways now meet it. The city raised its
        // physical kerbs before this depot existed, so the junction is cut here.
        ClearStreetKerbAcrossDriveways();

        // 3. Parking bays (Bay 1 to 4)
        BuildBaysAndStops(currentDepot.transform, out bayPosition, out bayRotation);

        // 4. Place Garage & Parking Lot Models from Assets/unity models/parking lot
        PlaceDepot3DAssets(currentDepot.transform, streetSide);

        // 5. Depot lighting & signage
        BuildDepotProps(currentDepot.transform);

        // 6. Parked fleet buses in other bays for depot realism
        SpawnParkedFleetBuses(currentDepot.transform);

        // 7. Ambient traffic driving the yard's entry and exit lanes
        BuildDepotTraffic(currentDepot.transform, bus);

        // 8. Nothing the city already built may remain in the yard
        EvictCityBuildingsFromDepot();

        // The whole depot is imported scenery: keep it on the palette's
        // late-repair list so white FBX materials are caught even if a spawn
        // path above did not call ApplyFallback itself.
        ImportedMaterialPalette.RegisterImportedRoot(currentDepot.transform, importedPropFallbackColor);

        currentBay1Position = bayPosition;
        currentBay1Rotation = bayRotation;
        exitPos = currentExitPosition;

        if (DriverShiftSystem.Instance != null)
            DriverShiftSystem.Instance.assignedDepotBayId = "BAY-1";

        Debug.Log($"[BusDepotManager] Constructed depot at {depotCenter}. Assigned Bay 1: {bayPosition}, Exit: {exitPos}, Roads: {depotRoadLines.Count}.");
        return true;
    }

    /// <summary>
    /// Tears the current yard down: its zone, its road registrations and its
    /// geometry. Called before a depot is placed somewhere else, so the world is
    /// never asked to respect two yards at once.
    /// </summary>
    public void ReleaseCurrentDepot()
    {
        if (currentZoneHandle >= 0)
        {
            UnregisterZone(currentZoneHandle);
            currentZoneHandle = -1;
        }

        if (currentRoadWayId != long.MinValue)
        {
            var index = OSMRoadMeshBuilder.Resolve()?.SurfaceIndex;
            if (index != null) index.Remove(currentRoadWayId);
            currentRoadWayId = long.MinValue;
        }

        depotRoadLines.Clear();
        currentTraffic = null;
        currentKerbPosition = Vector3.zero;

        if (currentDepot != null)
        {
            DestroySafe(currentDepot);
            currentDepot = null;
        }
    }

    void OnDestroy()
    {
        if (Instance != this) return;
        ReleaseCurrentDepot();
    }

    void BuildDepotApron(Transform parent)
    {
        GameObject apron = GameObject.CreatePrimitive(PrimitiveType.Cube);
        apron.name = "Depot_Apron_Ground";
        apron.transform.SetParent(parent, false);
        apron.transform.localPosition = new Vector3(0f, ApronTopOffset - ApronThickness * 0.5f, 0f);
        apron.transform.localRotation = Quaternion.identity;
        apron.transform.localScale = new Vector3(yardWidth, ApronThickness, yardDepth);

        Renderer rend = apron.GetComponent<Renderer>();
        if (rend != null && tarmacMaterial != null) rend.material = tarmacMaterial;
    }

    /// <summary>
    /// Lays the yard's own asphalt: a one-way entry aisle, a U-turn and a one-way
    /// exit aisle, each reaching the kerb so the player has a real road in and a
    /// real road out instead of open concrete. Both driveways cross the yard edge
    /// and stop on the street, which is what makes them junctions rather than a
    /// ramp lying next to the depot.
    /// </summary>
    void BuildDepotRoadNetwork(Transform parent, Vector3 kerbPos, Quaternion roadFacing)
    {
        // Kept so the yard traffic can find the street the driveways meet.
        currentKerbPosition = kerbPos;
        Vector3 localKerb = Quaternion.Inverse(roadFacing) * (kerbPos - parent.position);
        float side = localKerb.x >= 0f ? 1f : -1f;
        float halfWidth = yardWidth * 0.5f;
        float entryZ = localKerb.z - 1.5f;
        // The two aisles must not touch, or the U-turn would read as one wide slab
        // and neither lane could be marked or reasoned about on its own.
        float exitZ = entryZ - Mathf.Max(depotRoadWidth + 1f, 5.5f);
        float loopEndX = -side * (halfWidth - 6f);
        float turnX = -side * (halfWidth - 3f);
        // The aisle runs at right angles to the kerb, so its last point is its end
        // cap. It stops ON the kerb line: crossing it used to leave about a metre of
        // the depot's own tarmac on the carriageway, which read as the depot spilling
        // into the street. The dropped kerb built below bridges the last 0.6 m of
        // pavement and finishes flush with the asphalt edge, so the bus still has
        // continuous tarmac while the street keeps its full width.
        float outerX = localKerb.x;

        Vector3 Local(float x, float z) => parent.TransformPoint(new Vector3(x, DrivewayLift, z));

        var entryPoints = new List<Vector3> { Local(outerX, entryZ), Local(loopEndX, entryZ) };
        var turnPoints = new List<Vector3> { Local(loopEndX, entryZ), Local(turnX, entryZ), Local(turnX, exitZ), Local(loopEndX, exitZ) };
        var exitPoints = new List<Vector3> { Local(loopEndX, exitZ), Local(outerX, exitZ) };

        GameObject entry = BuildDepotRoad(parent, "Depot_Entry_Road", entryPoints);
        GameObject turn = BuildDepotRoad(parent, "Depot_Turn_Road", turnPoints);
        GameObject exit = BuildDepotRoad(parent, "Depot_Exit_Road", exitPoints);

        if (entry != null) depotRoadLines.Add(entryPoints.ToArray());
        if (turn != null) depotRoadLines.Add(turnPoints.ToArray());
        if (exit != null) depotRoadLines.Add(exitPoints.ToArray());

        if (depotRoadLines.Count == 0) BuildFallbackDriveway(parent, outerX, entryZ, exitZ);
        else BuildDroppedKerbs(parent, localKerb, side, entryZ, exitZ);

        // Departure is reported when the bus reaches the road at the end of the
        // exit aisle, which is the point the yard actually funnels it towards.
        currentExitPosition = exit != null ? exitPoints[exitPoints.Count - 1] : kerbPos;
    }

    /// <summary>
    /// A driveway is a dropped kerb, not asphalt butting into a raised one. The city
    /// raises a kerb strip (and a physical collider) along every street edge before a
    /// route ever picks its start, so the depot cannot rely on the junction being cut
    /// for it. These two little ramps bridge the kerb at the mouth of each aisle: they
    /// carry the driveway surface over the kerb strip so the join reads as a real
    /// driveway, and the matching colliders are dropped in
    /// <see cref="ClearStreetKerbAcrossDriveways"/> so nothing invisible blocks the bus.
    /// </summary>
    void BuildDroppedKerbs(Transform parent, Vector3 localKerb, float side, float entryZ, float exitZ)
    {
        var builder = OSMRoadMeshBuilder.Resolve();
        if (builder == null) return;

        float streetX = localKerb.x + side * 0.6f;   // flush with the asphalt edge, not on it
        float yardX = localKerb.x - side * 0.5f;     // back onto the driveway
        float kerbLift = builder.boundaryHeight + 0.01f;
        BuildDroppedKerb(parent, builder, streetX, yardX, kerbLift, entryZ);
        BuildDroppedKerb(parent, builder, streetX, yardX, kerbLift, exitZ);
    }

    void BuildDroppedKerb(Transform parent, OSMRoadMeshBuilder builder, float streetX, float yardX, float kerbLift, float laneZ)
    {
        var points = new List<Vector3>
        {
            parent.TransformPoint(new Vector3(streetX, DrivewayLift, laneZ)),
            parent.TransformPoint(new Vector3((streetX + yardX) * 0.5f, kerbLift, laneZ)),
            parent.TransformPoint(new Vector3(yardX, DrivewayLift, laneZ))
        };

        Mesh mesh = builder.BuildRoadSegmentMesh(points, depotRoadWidth, 1, false);
        if (mesh == null) return;

        var ramp = new GameObject("Depot_DroppedKerb");
        ramp.transform.SetParent(parent, false);
        ramp.AddComponent<MeshFilter>().sharedMesh = mesh;
        var surface = ramp.AddComponent<MeshRenderer>();
        surface.sharedMaterial = builder.RoadSurfaceMaterial();
        surface.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        surface.receiveShadows = false;
    }

    /// <summary>
    /// Removes the city's physical kerb colliders where the depot's driveways meet the
    /// street. Those 0.7 m boxes are raised when the city is built, from every junction
    /// that existed then; a depot placed afterwards is not one of them, so without this
    /// the bus is walled into its own yard. The visual kerb is bridged by
    /// <see cref="BuildDroppedKerbs"/>, so only the colliders need removing.
    /// </summary>
    void ClearStreetKerbAcrossDriveways()
    {
        if (depotRoadLines.Count == 0) return;

        float corridor = depotRoadWidth * 0.5f + 0.8f;
        var colliders = Object.FindObjectsByType<BoxCollider>(FindObjectsSortMode.None);
        for (int i = 0; i < colliders.Length; i++)
        {
            BoxCollider collider = colliders[i];
            if (collider == null) continue;
            Transform owner = collider.transform.parent;
            if (owner == null || owner.name != "PhysicalRoadBoundaries") continue;

            // Kerb colliders are long segments, not points: one can run the length of
            // a block and still cross a driveway with its centre far away. Compare the
            // collider's full segment (its long local axis) against the lanes.
            Transform t = collider.transform;
            Vector3 halfLength = t.forward * (collider.size.z * 0.5f * Mathf.Abs(t.lossyScale.z));
            Vector3 a = t.position - halfLength;
            Vector3 b = t.position + halfLength;
            if (!BoundaryCrossesDepotLane(a, b, corridor)) continue;
            DestroySafe(collider.gameObject);
        }
    }

    bool BoundaryCrossesDepotLane(Vector3 a, Vector3 b, float corridor)
    {
        float sqrCorridor = corridor * corridor;
        for (int l = 0; l < depotRoadLines.Count; l++)
        {
            Vector3[] line = depotRoadLines[l];
            if (line == null) continue;
            for (int i = 1; i < line.Length; i++)
                if (RoadSurfaceIndex.SegmentDistanceSquared(a, b, line[i - 1], line[i]) <= sqrCorridor)
                    return true;
        }
        return false;
    }

    /// <summary>
    /// Builds one yard aisle as real road: mesh, collider, lane markings, and a
    /// registration in the road clearance index, so the rest of the world agrees
    /// this is road. Returns null when no road builder exists yet (early boot, or
    /// an EditMode test without one) - the caller then falls back to a plain slab.
    /// </summary>
    GameObject BuildDepotRoad(Transform parent, string name, List<Vector3> points)
    {
        var builder = OSMRoadMeshBuilder.Resolve();
        if (builder == null) return null;

        Mesh mesh = builder.BuildRoadSegmentMesh(points, depotRoadWidth, 1, true);
        if (mesh == null) return null;

        if (currentRoadWayId == long.MinValue) currentRoadWayId = nextDepotRoadWayId--;
        long wayId = currentRoadWayId;

        var index = builder.EnsureSurfaceIndex();
        for (int i = 1; i < points.Count; i++)
            index.Add(points[i - 1], points[i], depotRoadWidth, wayId, 1);

        var road = new GameObject(name);
        road.transform.SetParent(parent, true);
        var collider = road.AddComponent<MeshCollider>();
        collider.sharedMesh = mesh;
        var physicsMaterial = builder.RoadColliderMaterial;
        if (physicsMaterial != null) collider.sharedMaterial = physicsMaterial;

        var filter = road.AddComponent<MeshFilter>();
        filter.sharedMesh = mesh;
        var surface = road.AddComponent<MeshRenderer>();
        surface.sharedMaterial = builder.RoadSurfaceMaterial();
        surface.enabled = builder.renderRoadSurface;
        surface.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        surface.receiveShadows = false;

        if (builder.renderRoadSurface && builder.generateRealisticLaneMarkings)
            builder.BuildLaneMarkings(road.transform, points, DepotRoadWay(wayId), depotRoadWidth);

        return road;
    }

    /// <summary>A "service, one lane, one way" description for the yard aisles.</summary>
    static OSMWay DepotRoadWay(long wayId)
    {
        var way = new OSMWay { id = wayId, tags = new Dictionary<string, string>() };
        way.tags["highway"] = "service";
        way.tags["lanes"] = "1";
        way.tags["oneway"] = "yes";
        way.tags["name"] = "Depot Access";
        return way;
    }

    /// <summary>
    /// Plain tarmac slabs used only when no road builder is available. Keeping the
    /// driveways drivable in every boot order matters more than their looks.
    /// </summary>
    void BuildFallbackDriveway(Transform parent, float outerX, float entryZ, float exitZ)
    {
        BuildFallbackLane(parent, "Depot_Entry_Ramp", outerX, entryZ);
        BuildFallbackLane(parent, "Depot_Exit_Ramp", outerX, exitZ);
    }

    void BuildFallbackLane(Transform parent, string name, float outerX, float laneZ)
    {
        GameObject ramp = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ramp.name = name;
        ramp.transform.SetParent(parent, false);
        ramp.transform.localPosition = new Vector3(outerX * 0.5f, ApronTopOffset - 0.04f, laneZ);
        ramp.transform.localRotation = Quaternion.identity;
        ramp.transform.localScale = new Vector3(Mathf.Abs(outerX), 0.08f, depotRoadWidth);

        Renderer rend = ramp.GetComponent<Renderer>();
        if (rend != null && tarmacMaterial != null) rend.material = tarmacMaterial;
    }

    void BuildBaysAndStops(Transform parent, out Vector3 bay1WorldPos, out Quaternion bay1WorldRot)
    {
        // 4 bus parking bays side-by-side, behind the yard roads so a bus can pull
        // forward out of its bay straight into the exit aisle.
        float startX = -(totalBays - 1) * 0.5f * bayWidth;
        bay1WorldPos = parent.position;
        bay1WorldRot = parent.rotation;

        GameObject stopFbxPrefab = LoadDepotModel("Cocnrete+Parking+Stop");

        for (int i = 0; i < totalBays; i++)
        {
            float x = startX + i * bayWidth;
            Vector3 bayLocal = new Vector3(x, 0.01f, bayRowZ);

            // Paint bay yellow lines
            CreateBayStripes(parent, x, bayWidth, bayLength, bayRowZ);

            // Concrete wheel stop at the head of the bay
            Vector3 wheelStopLocal = new Vector3(x, ApronTopOffset, bayRowZ - bayLength * 0.45f);
            if (stopFbxPrefab != null)
            {
                GameObject stopInstance = Instantiate(stopFbxPrefab, parent);
                stopInstance.name = $"WheelStop_Bay_{i + 1}";
                stopInstance.transform.localPosition = wheelStopLocal;
                stopInstance.transform.localRotation = Quaternion.identity;
                stopInstance.transform.localScale = Vector3.one * 1.6f;
                ImportedMaterialPalette.ApplyFallback(stopInstance, importedPropFallbackColor);
            }
            else
            {
                GameObject fallbackStop = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fallbackStop.name = $"WheelStop_Bay_{i + 1}";
                fallbackStop.transform.SetParent(parent, false);
                fallbackStop.transform.localPosition = wheelStopLocal + Vector3.up * 0.08f;
                fallbackStop.transform.localScale = new Vector3(2.4f, 0.16f, 0.28f);
                if (curbMaterial != null) fallbackStop.GetComponent<Renderer>().material = curbMaterial;
            }

            // Bay 1 is player's assigned departure bay
            if (i == 0)
            {
                bay1WorldPos = parent.TransformPoint(new Vector3(x, 0.4f, bayRowZ));
                bay1WorldRot = parent.rotation;
            }
        }
    }

    void CreateBayStripes(Transform parent, float xCenter, float width, float length, float zCenter)
    {
        float halfW = width * 0.48f;
        float halfL = length * 0.5f;
        float y = ApronTopOffset + 0.005f;

        CreateStripe(parent, new Vector3(xCenter - halfW, y, zCenter), new Vector3(0.18f, 0.02f, length));
        CreateStripe(parent, new Vector3(xCenter + halfW, y, zCenter), new Vector3(0.18f, 0.02f, length));
        CreateStripe(parent, new Vector3(xCenter, y, zCenter - halfL), new Vector3(width * 0.96f, 0.02f, 0.18f));
    }

    void CreateStripe(Transform parent, Vector3 localPos, Vector3 scale)
    {
        GameObject stripe = GameObject.CreatePrimitive(PrimitiveType.Cube);
        stripe.name = "Bay_Stripe";
        stripe.transform.SetParent(parent, false);
        stripe.transform.localPosition = localPos;
        stripe.transform.localRotation = Quaternion.identity;
        stripe.transform.localScale = scale;
        DestroySafe(stripe.GetComponent<Collider>());
        Renderer rend = stripe.GetComponent<Renderer>();
        if (rend != null && stripeMaterial != null) rend.material = stripeMaterial;
    }

    /// <summary>
    /// Places the imported garage and parking structure at the ends of the bay row,
    /// outside the yard roads and outside the apron edge, so however large they are
    /// they cannot stand in an aisle: a depot building blocking its own lanes is
    /// exactly the kind of blockage this layout exists to prevent.
    /// </summary>
    void PlaceDepot3DAssets(Transform parent, float streetSide)
    {
        // Structures live on the far side of the yard from the street, so a
        // building can never reach the carriageway even in a right-hand-traffic
        // city, where the depot is mirrored across the kerb.
        float away = -streetSide;
        Vector3 garageLocal = new Vector3(away * (yardWidth * 0.5f + 3f), ApronTopOffset, bayRowZ);
        Vector3 parkingLocal = new Vector3(away * (yardWidth * 0.5f + 3f), ApronTopOffset, bayRowZ + bayLength + 5f);

        // 1. Maintenance garage: the imported model when it is yard sized, and the
        // yard's own workshop when it is not (the shipped prop is a 200 m parking
        // structure, which would swallow the yard and the whole block around it).
        GameObject garagePrefab = LoadDepotModel("Parking+Garage");
        GameObject garage = garagePrefab != null
            ? PlaceDepotStructure(parent, garagePrefab, "Depot_Service_Garage", garageLocal, Quaternion.Euler(0f, 90f, 0f), 1.5f, away)
            : null;
        if (garage == null) BuildProceduralGarageBuilding(parent, garageLocal, away);

        // 2. Parking structure prop, same size rule.
        GameObject parkingPrefab = LoadDepotModel("parking");
        if (parkingPrefab != null)
            PlaceDepotStructure(parent, parkingPrefab, "Depot_Parking_Structure", parkingLocal, Quaternion.identity, 0.8f, away);
    }

    /// <summary>
    /// Instantiates an imported depot building and keeps it only when it fits the
    /// yard budget. Returns null when the prop is too large to stand beside a yard
    /// this size, which is the caller's cue to build its own.
    ///
    /// When the placed prop overlaps the street, it is slid further in the
    /// away-from-street direction (up to 8 steps of 1.5 m) before being refused,
    /// so a prop that barely clips the kerb is recovered rather than dropped.
    /// </summary>
    GameObject PlaceDepotStructure(Transform parent, GameObject prefab, string name, Vector3 localPosition,
        Quaternion localRotation, float scale, float awayLocalX = 0f)
    {
        GameObject instance = Instantiate(prefab, parent);
        instance.name = name;
        instance.transform.localPosition = localPosition;
        instance.transform.localRotation = localRotation;
        instance.transform.localScale = Vector3.one * scale;

        Vector3 size = LocalVisualSize(instance);
        if (size.x > maxDepotStructureSize || size.y > maxDepotStructureSize || size.z > maxDepotStructureSize)
        {
            Debug.Log($"[BusDepotManager] Refused imported depot prop '{prefab.name}' ({size.x:F1}x{size.y:F1}x{size.z:F1} m): " +
                $"a depot building has to fit the {maxDepotStructureSize:F0} m yard budget.");
            DestroySafe(instance);
            return null;
        }

        // Fitting the budget is not the same as fitting the yard: a prop can be
        // small and still stand in the entry or exit aisle, which is the one place
        // the player has been told to drive.
        if (PropStandsInYardLane(instance))
        {
            Debug.Log($"[BusDepotManager] Refused imported depot prop '{prefab.name}' ({size.x:F1}x{size.y:F1}x{size.z:F1} m): " +
                "its footprint would stand in the yard's entry/exit lane.");
            DestroySafe(instance);
            return null;
        }

        // Fitting the yard is not the same as staying off the street. Try sliding
        // the structure away from the street (in the local-X away direction) before
        // refusing it, so a prop that barely clips the kerb is recovered.
        if (PropReachesStreet(instance) && awayLocalX != 0f)
        {
            Vector3 slideWorld = parent.TransformDirection(new Vector3(awayLocalX, 0f, 0f)).normalized;
            for (int slide = 0; slide < 8 && PropReachesStreet(instance); slide++)
            {
                instance.transform.position += slideWorld * 1.5f;
                if (PropStandsInYardLane(instance)) break;
            }
        }
        if (PropReachesStreet(instance))
        {
            Debug.Log($"[BusDepotManager] Refused imported depot prop '{prefab.name}' ({size.x:F1}x{size.y:F1}x{size.z:F1} m): " +
                "its footprint would reach the street carriageway even after sliding.");
            DestroySafe(instance);
            return null;
        }

        ImportedMaterialPalette.ApplyFallback(instance, importedPropFallbackColor);
        EnsureColliders(instance);
        return instance;
    }

    /// <summary>
    /// Footprint of an instance's renderers measured in the instance's own axes
    /// (with its scale applied), so a depot heading cannot swap or inflate it the
    /// way a world-space axis-aligned box would.
    /// </summary>
    static Vector3 LocalVisualSize(GameObject instance)
    {
        var renderers = instance.GetComponentsInChildren<Renderer>(true);
        if (renderers.Length == 0) return Vector3.zero;

        Bounds world = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++) world.Encapsulate(renderers[i].bounds);

        Transform frame = instance.transform;
        var local = new Bounds(frame.InverseTransformPoint(world.center), Vector3.zero);
        for (int corner = 0; corner < 8; corner++)
            local.Encapsulate(frame.InverseTransformPoint(new Vector3(
                (corner & 1) == 0 ? world.min.x : world.max.x,
                (corner & 2) == 0 ? world.min.y : world.max.y,
                (corner & 4) == 0 ? world.min.z : world.max.z)));

        Vector3 scale = frame.lossyScale;
        return new Vector3(Mathf.Abs(local.size.x * scale.x), Mathf.Abs(local.size.y * scale.y), Mathf.Abs(local.size.z * scale.z));
    }

    /// <summary>
    /// The workshop the yard raises when the imported garage is refused. It answers
    /// the same lane question as every other prop: the shed is walked outward until
    /// it clears the aisles, and dropped rather than left standing in one. An
    /// additional sweep walks it in the away-from-street direction when the
    /// previous-cleared position still overlaps the carriageway.
    /// </summary>
    void BuildProceduralGarageBuilding(Transform parent, Vector3 localPos, float awayLocalX = 0f)
    {
        GameObject garage = null;
        // Primary walk: clear the yard's own lanes.
        float outward = Mathf.Sign(localPos.x == 0f ? 1f : localPos.x);
        for (int step = 0; step <= 8; step++)
        {
            garage = GameObject.CreatePrimitive(PrimitiveType.Cube);
            garage.name = "Depot_Workshop_Building";
            garage.transform.SetParent(parent, false);
            garage.transform.localPosition = localPos + new Vector3(outward * step, 0f, 0f) + Vector3.up * 3.5f;
            garage.transform.localScale = new Vector3(12f, 7f, 8f);
            if (!PropStandsInYardLane(garage) && !PropReachesStreet(garage))
            {
                Renderer rend = garage.GetComponent<Renderer>();
                if (rend != null && curbMaterial != null) rend.material = curbMaterial;
                return;
            }
            Debug.Log($"[BusDepotManager] Workshop moved {step + 1} m outward: the first spot stood in a yard lane.");
            DestroySafe(garage);
            garage = null;
        }

        // Secondary walk: lane-cleared position still overlaps the street. Walk
        // in the away-from-street direction instead, which is always safe because
        // it leads deeper into the yard / away from the carriageway.
        if (awayLocalX != 0f)
        {
            float awaySign = Mathf.Sign(awayLocalX);
            for (int step = 1; step <= 12; step++)
            {
                garage = GameObject.CreatePrimitive(PrimitiveType.Cube);
                garage.name = "Depot_Workshop_Building";
                garage.transform.SetParent(parent, false);
                garage.transform.localPosition = localPos + new Vector3(awaySign * step * 1.5f, 0f, 0f) + Vector3.up * 3.5f;
                garage.transform.localScale = new Vector3(12f, 7f, 8f);
                if (!PropStandsInYardLane(garage) && !PropReachesStreet(garage))
                {
                    Renderer rend = garage.GetComponent<Renderer>();
                    if (rend != null && curbMaterial != null) rend.material = curbMaterial;
                    return;
                }
                DestroySafe(garage);
                garage = null;
            }
        }

        Debug.LogWarning("[BusDepotManager] The yard's lanes leave no clear ground for a workshop; the shed was dropped rather than left in an aisle.");
    }

    void BuildDepotProps(Transform parent)
    {
        float halfWidth = yardWidth * 0.5f;
        float halfDepth = yardDepth * 0.5f;

        // Yard perimeter floodlights
        Vector3[] lightCorners = new Vector3[]
        {
            new Vector3(-halfWidth + 0.8f, ApronTopOffset, -halfDepth + 0.8f),
            new Vector3(halfWidth - 0.8f, ApronTopOffset, -halfDepth + 0.8f),
            new Vector3(-halfWidth + 0.8f, ApronTopOffset, halfDepth - 0.8f),
            new Vector3(halfWidth - 0.8f, ApronTopOffset, halfDepth - 0.8f)
        };

        for (int i = 0; i < lightCorners.Length; i++)
        {
            if (!TryClearYardProp(lightCorners[i], 0.35f, out var clearedLocal))
            {
                Debug.Log($"[BusDepotManager] Skipped floodlight {i + 1}: the yard roads leave it no clear footing.");
                continue;
            }

            GameObject pole = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            pole.name = $"Depot_Floodlight_Pole_{i + 1}";
            pole.transform.SetParent(parent, false);
            pole.transform.localPosition = clearedLocal + Vector3.up * 4f;
            pole.transform.localScale = new Vector3(0.35f, 4f, 0.35f);
            DestroySafe(pole.GetComponent<Collider>());

            GameObject lightGo = new GameObject("Light");
            lightGo.transform.SetParent(pole.transform, false);
            lightGo.transform.localPosition = Vector3.up * 0.95f;
            Light lt = lightGo.AddComponent<Light>();
            lt.type = LightType.Point;
            lt.range = 28f;
            lt.intensity = 1.4f;
            lt.color = new Color(1f, 0.95f, 0.85f);
        }

        // 3D Signboard for Bay 1 Dispatch, lifted clear of a bus roof so it reads as
        // a gantry over the bay rather than something the bus has to drive around.
        GameObject signBoard = GameObject.CreatePrimitive(PrimitiveType.Cube);
        signBoard.name = "Depot_Dispatch_Sign";
        signBoard.transform.SetParent(parent, false);
        signBoard.transform.localPosition = new Vector3(-(totalBays - 1) * 0.5f * bayWidth, 5f, bayRowZ);
        signBoard.transform.localScale = new Vector3(4.2f, 0.9f, 0.15f);
        DestroySafe(signBoard.GetComponent<Collider>());
        Renderer signRend = signBoard.GetComponent<Renderer>();
        if (signRend != null && stripeMaterial != null) signRend.material = stripeMaterial;
    }

    /// <summary>
    /// Moves a yard prop until it stands clear of every yard road, and reports
    /// whether it could. The roads are the point of the layout, so a lamp post is
    /// nudged (or dropped) rather than left standing in an aisle.
    /// </summary>
    bool TryClearYardProp(Vector3 localPoint, float radius, out Vector3 clearedLocal)
    {
        clearedLocal = localPoint;
        if (currentDepot == null || depotRoadLines.Count == 0) return true;

        Vector3 world = currentDepot.transform.TransformPoint(localPoint);
        float required = depotRoadWidth * 0.5f + radius + 0.35f;

        for (int attempt = 0; attempt <= 3; attempt++)
        {
            float nearest = float.MaxValue;
            Vector3 away = Vector3.zero;
            foreach (var line in depotRoadLines)
                for (int i = 1; i < line.Length; i++)
                {
                    Vector3 projected = RoadSpawnResolver.ProjectOnSegment(world, line[i - 1], line[i]);
                    float distance = Vector3.Distance(
                        new Vector3(world.x, 0f, world.z), new Vector3(projected.x, 0f, projected.z));
                    if (distance >= nearest) continue;
                    nearest = distance;
                    away = world - projected;
                }

            if (nearest >= required)
            {
                clearedLocal = currentDepot.transform.InverseTransformPoint(world);
                clearedLocal.y = localPoint.y;
                return true;
            }

            away.y = 0f;
            if (away.sqrMagnitude < 0.0001f) away = world - currentDepot.transform.position;
            away.y = 0f;
            if (away.sqrMagnitude < 0.0001f) return false;

            world += away.normalized * (required - nearest + 0.25f);
        }

        return false;
    }

    void SpawnParkedFleetBuses(Transform parent)
    {
        // Place parked static fleet buses in Bay 2 and Bay 3
        float startX = -(totalBays - 1) * 0.5f * bayWidth;

        GameObject[] availableVisuals = LoadAvailableBusVisuals();
        if (availableVisuals == null || availableVisuals.Length == 0) return;

        for (int bay = 1; bay <= 2; bay++)
        {
            float x = startX + bay * bayWidth;
            GameObject visualPrefab = availableVisuals[bay % availableVisuals.Length];
            if (visualPrefab == null) continue;

            GameObject parked = Instantiate(visualPrefab, parent);
            parked.name = $"Fleet_Bus_Bay_{bay + 1}";
            parked.transform.localPosition = new Vector3(x, 0.4f, bayRowZ);
            parked.transform.localRotation = Quaternion.identity;

            // Strip any scripts or audio
            foreach (var audio in parked.GetComponentsInChildren<AudioSource>(true)) DestroySafe(audio);
            foreach (var cam in parked.GetComponentsInChildren<Camera>(true)) DestroySafe(cam);

            // Parked fleet buses are imported FBX models too. They used to be
            // left as flat white shells because nothing repaired them, so give
            // them the same material treatment as the player's bus.
            ImportedVehicleVisualRepair repair = parked.GetComponent<ImportedVehicleVisualRepair>();
            if (repair == null)
                repair = parked.AddComponent<ImportedVehicleVisualRepair>();
            repair.RepairNow(parked.transform);

            // A parked bus is the worst thing to leave in an aisle, so it gets the
            // same footprint test as the buildings before it is kept.
            if (PropStandsInYardLane(parked))
            {
                Debug.Log($"[BusDepotManager] Dropped the parked fleet bus for bay {bay + 1}: its footprint would stand in the yard's entry/exit lane.");
                DestroySafe(parked);
                continue;
            }

            if (PropReachesStreet(parked))
            {
                Debug.Log($"[BusDepotManager] Dropped the parked fleet bus for bay {bay + 1}: its footprint would reach the street carriageway.");
                DestroySafe(parked);
                continue;
            }

            ImportedMaterialPalette.RegisterImportedRoot(parked, repair.fallbackBodyColor);
        }
    }

    /// <summary>
    /// Puts ambient traffic on the yard's own circuit (see <see cref="DepotTrafficSpawner"/>).
    /// It is a child of the depot, so releasing the yard takes its traffic with it.
    /// </summary>
    void BuildDepotTraffic(Transform parent, BusController bus)
    {
        currentTraffic = null;
        if (!spawnDepotTraffic || depotRoadLines.Count < 3) return;

        var trafficGo = new GameObject("Depot_Traffic");
        trafficGo.transform.SetParent(parent, false);
        var traffic = trafficGo.AddComponent<DepotTrafficSpawner>();
        traffic.Init(parent, depotRoadLines, currentKerbPosition,
            new Vector2(yardWidth * 0.5f, yardDepth * 0.5f), bus, currentKerbPosition.y);
        currentTraffic = traffic;
    }

    /// <summary>
    /// Removes whatever the city already built inside the new yard, and drops the
    /// building records behind it so the streamer cannot rebuild it the next time
    /// the player drives past. The zone alone is not enough: mapped buildings and
    /// fictional infill are usually raised long before a route picks its start.
    /// </summary>
    public int EvictCityBuildingsFromDepot()
    {
        var builder = OSMBuildingMeshBuilder.Resolve();
        if (builder == null)
        {
            lastEvictedBuildingCount = 0;
            return 0;
        }

        int removed = builder.ClearDepotOverlaps(1f);
        var fill = builder.GetComponent<ProceduralBuildingFill>();
        if (fill != null) removed += fill.ClearDepotOverlaps(1f);

        lastEvictedBuildingCount = removed;
        if (removed > 0)
            Debug.Log($"[BusDepotManager] Cleared {removed} building(s) that stood inside the depot yard.");
        return removed;
    }

    GameObject[] LoadAvailableBusVisuals()
    {
        var list = new List<GameObject>();
#if UNITY_EDITOR
        string folder = "Assets/_Game/Generated/Models/Buses";
        if (Directory.Exists(folder))
        {
            foreach (var file in Directory.GetFiles(folder, "*_Visual.prefab"))
            {
                var go = AssetDatabase.LoadAssetAtPath<GameObject>(file.Replace('\\', '/'));
                if (go != null) list.Add(go);
            }
        }
#endif
        return list.ToArray();
    }

    GameObject LoadDepotModel(string nameFilter)
    {
        // 1. Check generated resources
        GameObject[] resources = Resources.LoadAll<GameObject>("BusDepotsGenerated");
        if (resources != null && resources.Length > 0)
        {
            foreach (var r in resources)
            {
                if (r != null && r.name.IndexOf(nameFilter, System.StringComparison.OrdinalIgnoreCase) >= 0)
                    return r;
            }
        }

#if UNITY_EDITOR
        // 2. Check source FBX in Assets/unity models/parking lot
        string folder = "Assets/unity models/parking lot";
        if (Directory.Exists(folder))
        {
            foreach (var file in Directory.GetFiles(folder, "*.fbx"))
            {
                if (file.IndexOf(nameFilter, System.StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    var loaded = AssetDatabase.LoadAssetAtPath<GameObject>(file.Replace('\\', '/'));
                    if (loaded != null) return loaded;
                }
            }
        }
#endif
        return null;
    }

    void EnsureColliders(GameObject go)
    {
        if (go.GetComponentInChildren<Collider>() == null)
        {
            var box = go.AddComponent<BoxCollider>();
            var renderers = go.GetComponentsInChildren<Renderer>();
            if (renderers.Length > 0)
            {
                Bounds b = renderers[0].bounds;
                for (int i = 1; i < renderers.Length; i++) b.Encapsulate(renderers[i].bounds);
                box.center = go.transform.InverseTransformPoint(b.center);
                box.size = b.size;
            }
        }
    }

    void Update()
    {
        if (hasDepartedDepot || activeBus == null || currentDepot == null)
            return;

        float distToExit = Vector3.Distance(activeBus.transform.position, currentExitPosition);
        if (distToExit < 14f && activeBus.currentSpeedKmh > 5f)
        {
            hasDepartedDepot = true;
            if (StopApproachUI.Instance != null && MissionManager.Instance != null && MissionManager.Instance.currentRoute != null)
            {
                var stops = MissionManager.Instance.currentRoute.stops;
                if (stops != null && stops.Length > 0)
                    SubtitleManager.EnsureExists().Show("Departed Depot", $"En route to {stops[0].stopName}.", 3.5f);
            }
        }
    }
}
