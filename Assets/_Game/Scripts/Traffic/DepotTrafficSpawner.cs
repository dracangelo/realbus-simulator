using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ambient depot traffic: vehicles that come down the street, turn in through the
/// depot's entry driveway, loop the yard and leave again by the exit driveway.
///
/// The depot's three lanes already form a circuit (street, entry driveway, entry
/// aisle, U-turn, exit aisle, exit driveway, street), so the traffic is driven
/// along an <see cref="AIRoadGraph"/> built from those lane points rather than from
/// the city graph, which knows nothing about the yard. Movement is spline-based
/// (<see cref="SplineVehicle"/>): the vehicles are scenery with a schedule, not
/// physics actors that could shove the player's bus.
///
/// The circuit is driven in ONE direction - street, yard, street - and a vehicle
/// that reaches the far end is paused out of sight and re-enters from the top of
/// the street, the way the traffic pool activates dormant vehicles. Looping back
/// along the street instead would put both directions in one lane, because the
/// depot only knows the one lane the street presents at its kerb.
///
/// The gate is the part that matters for playability. While the player's bus is
/// inside the yard, every vehicle still on the street is held at its node, so
/// nothing can drive into the aisles the player has been told to leave through; a
/// vehicle already inside the yard is allowed to finish leaving. That is
/// <see cref="ShouldHoldTraffic"/>, kept as a pure function so it can be tested
/// without a running yard.
/// </summary>
public class DepotTrafficSpawner : MonoBehaviour
{
    [Header("Traffic")]
    [Tooltip("How many yard vehicles to place. One starts on the street, the rest inside the yard.")]
    public int vehiclesToSpawn = 2;
    [Tooltip("How far up and down the street the approach and departure nodes sit from the depot.")]
    public float approachDistanceMeters = 26f;
    [Tooltip("Cruise speed for yard traffic. Yard aisles are tight, so this stays a crawl.")]
    public float cruiseSpeedKmh = 22f;
    [Tooltip("Lateral offset from the kerb onto the street lane the traffic uses.")]
    public float streetLaneOffsetMeters = 1.9f;
    [Tooltip("Seconds a vehicle waits out of sight at the far end before re-entering from the top of the street.")]
    public float respawnPauseSeconds = 9f;
    [Tooltip("Freezes street traffic while the player's bus is inside the yard.")]
    public bool holdWhilePlayerBusInYard = true;
    public bool logSpawns = true;

    [Header("Runtime State")]
    public BusController playerBus;
    public int activeVehicleCount;

    /// <summary>Circuit nodes, in driving order.</summary>
    public const int StreetApproachNode = 0;
    public const int EntryMouthNode = 1;
    public const int EntryDeepNode = 2;
    public const int TurnInNode = 3;
    public const int TurnOutNode = 4;
    public const int ExitDeepNode = 5;
    public const int ExitMouthNode = 6;
    public const int StreetDepartureNode = 7;
    public const int NodeCount = 8;

    /// <summary>One yard vehicle and where it is in its schedule.</summary>
    class YardVehicle
    {
        public SplineVehicle spline;
        /// <summary>Time to re-enter from the top of the street, or -1 while driving.</summary>
        public float reenterAt = -1f;
        /// <summary>Stagger so two vehicles do not re-enter together.</summary>
        public float pause;
    }

    readonly List<YardVehicle> vehicles = new List<YardVehicle>();
    Material bodyMaterial;
    Material glassMaterial;
    Transform yard;
    AIRoadGraph graph;
    Vector2 yardHalfExtents;
    Vector3 approachPoint;
    Vector3 approachFacing;
    bool initialized;

    public AIRoadGraph Graph => graph;
    public int NodeTotal => graph != null ? graph.NodeCount : 0;
    public bool HoldingStreetTraffic { get; private set; }

    /// <summary>Number of vehicles placed under this root. Used by the depot tests.</summary>
    public int VehicleCount => vehicles.Count;

    /// <summary>
    /// Builds the yard circuit from the depot's own lane points and places the
    /// traffic. <paramref name="lanes"/> is the depot's road list in build order:
    /// entry, U-turn, exit.
    /// </summary>
    public void Init(Transform yardTransform, IList<Vector3[]> lanes, Vector3 kerbWorld, Vector2 halfExtents,
        BusController bus, float groundY)
    {
        yard = yardTransform;
        playerBus = bus;
        yardHalfExtents = halfExtents;
        if (lanes == null || lanes.Count < 3 || lanes[0] == null || lanes[0].Length < 2 ||
            lanes[1] == null || lanes[1].Length < 3 || lanes[2] == null || lanes[2].Length < 2)
        {
            Debug.LogWarning("[DepotTrafficSpawner] The yard has no usable lane circuit; depot traffic skipped.");
            return;
        }

        Vector3[] entry = lanes[0];
        Vector3[] turn = lanes[1];
        Vector3[] exit = lanes[2];

        // The road lies beyond the kerb, away from the yard centre. The depot sits
        // slightly along the road from the kerb point, so the straight line to it is
        // diagonal; take the component across the road only, or the ambient lane is
        // dragged up the street instead of sitting in the near lane.
        Vector3 toKerb = kerbWorld - yardTransform.position;
        toKerb.y = 0f;
        Vector3 streetAxis = yardTransform.forward;
        Vector3 outward = Vector3.ProjectOnPlane(toKerb, streetAxis);
        if (outward.sqrMagnitude < 0.01f) outward = yardTransform.right;
        Vector3 streetLane = outward.normalized * streetLaneOffsetMeters;

        Vector3 entryMouth = Flat(entry[0], groundY);
        Vector3 exitMouth = Flat(exit[exit.Length - 1], groundY);

        // The street lane is measured from the KERB, not from the driveway mouth.
        // The mouths now reach onto the carriageway, so measuring from them would
        // push the approach and departure nodes up to a lane too far across the road.
        Vector3 streetBase = Flat(kerbWorld, groundY) + streetLane;

        var positions = new Vector3[NodeCount];
        positions[StreetApproachNode] = streetBase + streetAxis * approachDistanceMeters;
        positions[EntryMouthNode] = entryMouth;
        positions[EntryDeepNode] = Flat(entry[entry.Length - 1], groundY);
        positions[TurnInNode] = Flat(turn[1], groundY);
        positions[TurnOutNode] = Flat(turn[2], groundY);
        positions[ExitDeepNode] = Flat(exit[0], groundY);
        positions[ExitMouthNode] = exitMouth;
        positions[StreetDepartureNode] = streetBase - streetAxis * approachDistanceMeters;

        graph = gameObject.GetComponent<AIRoadGraph>();
        if (graph == null) graph = gameObject.AddComponent<AIRoadGraph>();
        // Private to the yard: the city traffic pool must never bind this loop and
        // drive the whole city around a depot.
        graph.privateLaneCircuit = true;
        graph.nodes = new AIRoadGraph.RoadNode[NodeCount];
        for (int i = 0; i < NodeCount; i++)
            graph.nodes[i] = BuildNode(i, positions[i], (i + 1) % NodeCount);

        approachPoint = positions[StreetApproachNode];
        approachFacing = SafeDirection(positions[EntryMouthNode] - positions[StreetApproachNode], yardTransform);
        initialized = true;
        SpawnVehicles(groundY);

        if (logSpawns)
            Debug.Log($"[DepotTrafficSpawner] Depot traffic ready: {VehicleCount} vehicle(s) on a {NodeCount}-node yard circuit " +
                $"({approachDistanceMeters:F0} m of street either side of the depot).");
    }

    static Vector3 Flat(Vector3 point, float y) => new Vector3(point.x, y, point.z);

    AIRoadGraph.RoadNode BuildNode(int index, Vector3 position, int next)
    {
        var point = new GameObject($"Depot traffic node {index}").transform;
        point.SetParent(transform, false);
        point.position = position;
        // One lane, one way per link: the circuit is driven as laid, and a centred
        // lane offset keeps the vehicle on the lane the depot actually built.
        return new AIRoadGraph.RoadNode
        {
            id = $"DEPOT{index}",
            point = point,
            laneCount = 1,
            laneWidth = 3.4f,
            speedLimitKmh = cruiseSpeedKmh,
            nextNodeIndices = new[] { next },
            outgoingLaneCounts = new[] { 1 },
            outgoingLaneWidths = new[] { 3.4f },
            outgoingOneWays = new[] { true }
        };
    }

    int NextNode(int index) => (index + 1) % NodeCount;

    void SpawnVehicles(float groundY)
    {
        int count = Mathf.Max(0, vehiclesToSpawn);
        for (int i = 0; i < count; i++)
        {
            GameObject vehicle = BuildVehicle(i == 1);
            if (vehicle == null) continue;
            vehicle.transform.SetParent(transform, true);

            var spline = vehicle.GetComponent<SplineVehicle>();
            var entry = new YardVehicle
            {
                spline = spline,
                pause = respawnPauseSeconds + i * respawnPauseSeconds * 0.5f
            };
            vehicles.Add(entry);

            // One vehicle arrives from the street, the next is already in the yard
            // and drives out, so the depot reads as working from the first frame.
            EnterCircuit(entry, i == 0 ? StreetApproachNode : EntryDeepNode, groundY);
        }
        activeVehicleCount = vehicles.Count;
    }

    void EnterCircuit(YardVehicle vehicle, int node, float groundY)
    {
        if (vehicle == null || vehicle.spline == null || graph == null) return;
        int next = NextNode(node);
        Vector3 from = graph.GetNodePosition(node);
        Vector3 to = graph.GetNodePosition(next);
        vehicle.spline.transform.position = new Vector3(from.x, groundY, from.z);
        vehicle.spline.transform.rotation = Quaternion.LookRotation(SafeDirection(to - from, yard), Vector3.up);
        vehicle.spline.Init(graph, node, next, 0, cruiseSpeedKmh);
        vehicle.spline.enabled = true;
        vehicle.reenterAt = -1f;
    }

    static Vector3 SafeDirection(Vector3 delta, Transform fallback)
    {
        delta.y = 0f;
        if (delta.sqrMagnitude > 0.01f) return delta.normalized;
        return fallback != null ? fallback.forward : Vector3.forward;
    }

    /// <summary>
    /// A yard vehicle. Traffic prefabs configured on the scene's <see cref="VehiclePool"/>
    /// are preferred so the depot matches the city's traffic; when the pool has no
    /// prefabs (an EditMode build, or an unconfigured scene) a low-poly vehicle is
    /// built here, because depot traffic must never depend on optional packs.
    /// </summary>
    GameObject BuildVehicle(bool asBus)
    {
        GameObject prefab = ResolveTrafficPrefab(asBus ? AIVehicleController.VehicleType.Bus : AIVehicleController.VehicleType.Car);
        GameObject vehicle = prefab != null ? Instantiate(prefab) : BuildFallbackVehicle(asBus);
        if (vehicle == null) return null;
        vehicle.name = asBus ? "Depot_Traffic_Bus" : "Depot_Traffic_Vehicle";

        // Yard traffic is moved along its circuit: anything that would drive,
        // fall, or film itself is removed rather than fighting the spline.
        foreach (var ai in vehicle.GetComponentsInChildren<AIVehicleController>(true)) BusDepotManager.DestroySafe(ai);
        foreach (var bus in vehicle.GetComponentsInChildren<BusController>(true)) BusDepotManager.DestroySafe(bus);
        foreach (var camera in vehicle.GetComponentsInChildren<Camera>(true)) BusDepotManager.DestroySafe(camera);
        foreach (var body in vehicle.GetComponentsInChildren<Rigidbody>(true)) body.isKinematic = true;
        // Yard traffic is scenery on a circuit, so the same player-only physics
        // and presentation costs the city pool strips apply here too.
        TrafficBodyOptimizer.Prepare(vehicle);

        var spline = vehicle.GetComponent<SplineVehicle>();
        if (spline == null) spline = vehicle.AddComponent<SplineVehicle>();
        spline.accelKmhPerSec = 10f;
        spline.brakeKmhPerSec = 16f;
        spline.minHeadway = 9f;
        spline.lookaheadNodes = NodeCount;
        // Registered after the spline exists so the perception component sees it.
        if (vehicle.GetComponent<TrafficParticipant>() == null) vehicle.AddComponent<TrafficParticipant>();
        return vehicle;
    }

    GameObject ResolveTrafficPrefab(AIVehicleController.VehicleType type)
    {
        var pool = FindAnyObjectByType<VehiclePool>();
        if (pool == null || pool.prefabs == null) return null;
        for (int i = 0; i < pool.prefabs.Length; i++)
            if (pool.prefabs[i] != null && pool.prefabs[i].prefab != null && pool.prefabs[i].type == type)
                return pool.prefabs[i].prefab;
        return null;
    }

    GameObject BuildFallbackVehicle(bool asBus)
    {
        EnsureVehicleMaterials();
        var root = new GameObject(asBus ? "Depot traffic bus (fallback)" : "Depot traffic car (fallback)");
        root.hideFlags = HideFlags.DontSave;

        var body = GameObject.CreatePrimitive(PrimitiveType.Cube);
        body.name = "Body";
        body.transform.SetParent(root.transform, false);
        body.transform.localPosition = asBus ? new Vector3(0f, 1.6f, 0f) : new Vector3(0f, 0.55f, 0f);
        body.transform.localScale = asBus ? new Vector3(2.5f, 2.9f, 10.5f) : new Vector3(1.8f, 0.75f, 4.2f);
        Paint(body, bodyMaterial);

        var cabin = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cabin.name = "Cabin";
        cabin.transform.SetParent(root.transform, false);
        cabin.transform.localPosition = asBus ? new Vector3(0f, 2.9f, 3.4f) : new Vector3(0f, 1.2f, -0.2f);
        cabin.transform.localScale = asBus ? new Vector3(2.4f, 0.6f, 2.2f) : new Vector3(1.55f, 0.6f, 1.7f);
        BusDepotManager.DestroySafe(cabin.GetComponent<Collider>());
        Paint(cabin, glassMaterial);

        Vector3 wheelOffset = asBus ? new Vector3(1.3f, 0.5f, 3.2f) : new Vector3(0.85f, 0.33f, 1.4f);
        float wheelRadius = asBus ? 0.5f : 0.33f;
        foreach (var corner in new[] { new Vector2(-1f, -1f), new Vector2(1f, -1f), new Vector2(-1f, 1f), new Vector2(1f, 1f) })
        {
            var wheel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            wheel.name = "Wheel";
            wheel.transform.SetParent(root.transform, false);
            wheel.transform.localPosition = new Vector3(wheelOffset.x * corner.x, wheelOffset.y, wheelOffset.z * corner.y);
            wheel.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            wheel.transform.localScale = new Vector3(wheelRadius * 2f, wheelRadius * 0.4f, wheelRadius * 2f);
            BusDepotManager.DestroySafe(wheel.GetComponent<Collider>());
            Paint(wheel, glassMaterial);
        }

        var kinematic = root.AddComponent<Rigidbody>();
        kinematic.isKinematic = true;
        kinematic.useGravity = false;
        return root;
    }

    void EnsureVehicleMaterials()
    {
        if (bodyMaterial != null) return;
        Shader shader = BuiltInShaders.Lit;
        // A test build may ship none of those. A vehicle with unpainted primitives
        // still drives its circuit, so this is not worth failing over.
        if (shader == null) return;
        bodyMaterial = new Material(shader) { name = "Depot_Traffic_Body" };
        bodyMaterial.color = new Color(0.72f, 0.24f, 0.2f, 1f);
        glassMaterial = new Material(shader) { name = "Depot_Traffic_Dark" };
        glassMaterial.color = new Color(0.16f, 0.18f, 0.2f, 1f);
    }

    static void Paint(GameObject target, Material material)
    {
        var renderer = target.GetComponent<Renderer>();
        if (renderer != null && material != null) renderer.material = material;
    }

    void Update()
    {
        if (!initialized || vehicles.Count == 0) return;
        bool busInYard = IsInsideYard(PlayerBusPosition(), 3f);
        HoldingStreetTraffic = holdWhilePlayerBusInYard && busInYard;

        for (int i = 0; i < vehicles.Count; i++)
        {
            var vehicle = vehicles[i];
            if (vehicle == null || vehicle.spline == null) continue;

            // Between trips: wait out of sight, then come in from the top of the
            // street like the next bus on the timetable.
            if (vehicle.reenterAt >= 0f)
            {
                if (Time.time >= vehicle.reenterAt)
                    EnterCircuit(vehicle, StreetApproachNode, vehicle.spline.transform.position.y);
                continue;
            }

            if (vehicle.spline.currentNodeIndex == StreetDepartureNode)
            {
                vehicle.spline.enabled = false;
                vehicle.reenterAt = Time.time + vehicle.pause;
                continue;
            }

            bool hold = ShouldHoldTraffic(HoldingStreetTraffic, IsInsideYard(vehicle.spline.transform.position, 0f));
            if (vehicle.spline.enabled == hold) vehicle.spline.enabled = !hold;
        }
    }

    Vector3 PlayerBusPosition()
    {
        if (playerBus == null) playerBus = FindAnyObjectByType<BusController>();
        return playerBus != null ? playerBus.transform.position : transform.position;
    }

    /// <summary>True when a world position falls inside the yard this traffic serves.</summary>
    public bool IsInsideYard(Vector3 worldPosition, float padding = 0f)
    {
        if (yard == null) return false;
        Vector3 local = yard.InverseTransformPoint(worldPosition);
        return Mathf.Abs(local.x) <= yardHalfExtents.x + padding && Mathf.Abs(local.z) <= yardHalfExtents.y + padding;
    }

    /// <summary>
    /// The gate, as a pure rule: traffic is held only when the player's bus is in
    /// the yard and the vehicle is not - a vehicle already inside finishes leaving,
    /// and nothing new drives in until the player is out.
    /// </summary>
    public static bool ShouldHoldTraffic(bool playerBusInYard, bool vehicleInYard)
        => playerBusInYard && !vehicleInYard;
}
