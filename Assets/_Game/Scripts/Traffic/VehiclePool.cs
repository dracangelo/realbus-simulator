using UnityEngine;
using System.Collections.Generic;

public class VehiclePool : MonoBehaviour
{
    enum SimulationTier
    {
        Dormant,
        Spline,
        FullAI
    }

    class PooledVehicle
    {
        public GameObject go;
        public Rigidbody rb;
        public AIVehicleController full;
        public SplineVehicle spline;
        public AIVehicleController.VehicleType type;
        public int laneIndex;
        public bool enabledByDensity;
        public SimulationTier tier;
    }

    [System.Serializable]
    public class VehiclePrefabEntry
    {
        public AIVehicleController.VehicleType type;
        public GameObject prefab;
        [Range(0f, 1f)] public float weight = 1f;
    }

    [Header("Pool")]
    public AIRoadGraph roadGraph;
    /// <summary>
    /// Bodies created once during the loading card and reused for the whole
    /// session. The active count is the density envelope below; this is the
    /// ceiling. <see cref="TrafficPerformancePolicy"/> keeps both in one place.
    /// </summary>
    public int poolSizePerScene = TrafficPerformancePolicy.FallbackPoolSize;
    public VehiclePrefabEntry[] prefabs;
    public bool logSpawnEvents = true;
    public bool allowAutoResolveRoadGraph = true;

    [Header("Debug")]
    public int debugForceActiveCount = 0;
    public bool logBusDistance = true;
    public bool logBusDistanceOnce = true;
    public float logBusDistanceIntervalSeconds = 5f;
    public bool logFirstActivationOnly = true;
    public float roadGraphRetryIntervalSeconds = 1.5f;
    public float maxAutoBindDistanceMeters = 1200f;

    [Header("Re-homing")]
    /// <summary>
    /// A dormant vehicle beyond this distance from the bus is moved to a node near it.
    /// The bus is teleported to the chosen route or depot after the pool is warmed, and
    /// it drives away from wherever it started: without this, the whole fleet is left
    /// behind beyond the spline radius and every vehicle stays dormant, which is why
    /// only a couple of cars ever appeared.
    /// </summary>
    public float rehomeDistanceMeters = TrafficPerformancePolicy.RehomeDistanceMeters;
    public int maxRehomesPerTick = 40;
    public float rehomeIntervalSeconds = 0.5f;

    [Header("Visibility")]
    /// <summary>
    /// Cars inside this range and inside the camera cone are never deactivated.
    /// Density falls by retiring vehicles that have already driven out of view,
    /// not by switching off whichever pool indices happen to be close.
    /// </summary>
    public float visibleDespawnDistanceMeters = TrafficPerformancePolicy.VisibleDespawnDistanceMeters;
    /// <summary>Hard limit for a visible vehicle. Beyond this it is far enough away to recycle safely.</summary>
    public float hardDespawnDistanceMeters = TrafficPerformancePolicy.HardDespawnDistanceMeters;
    /// <summary>Extra cone width used when choosing a re-home/activation point, so a car never appears at the edge of the screen.</summary>
    [Range(0f, 60f)] public float offscreenSpawnFovPaddingDegrees = 30f;

    [Header("Model Fit")]
    [Tooltip("Extra clearance added to the widest traffic model before it is used as the lane-spacing floor.")]
    public float laneWidthPaddingMeters = 0.55f;
    [Tooltip("Upper bound for automatic lane expansion. Beyond this a city's lane tags are more likely wrong than the models are wide.")]
    public float maxLaneWidthMeters = 5.2f;

    /// <summary>The widest traffic prefab measured during warm-up, in metres.</summary>
    public float WidestVehicleWidth { get; private set; }

    [Header("Spawn Safety")]
    [Tooltip("No vehicle is placed or re-homed closer than this to the player's bus.")]
    public float minSpawnDistanceFromPlayerMeters = 80f;
    [Tooltip("No vehicle is placed in the player's forward corridor within this distance, so traffic cannot pop into the bonnet.")]
    public float spawnAheadClearanceMeters = 120f;
    public float spawnAheadHalfWidthMeters = 9f;
    [Tooltip("Minimum gap between a new/re-homed vehicle and any other traffic body.")]
    public float vehicleSpawnClearanceMeters = 14f;
    public int spawnClearanceAttempts = 512;
    [Tooltip("Share of spawn attempts drawn from the streets in the forward view, so the traffic the player sees first exists first.")]
    [Range(0f, 1f)] public float spawnFovShare = 0.65f;

    [Header("Tier Split (distance from player bus)")]
    public float fullAiRadiusMeters = TrafficPerformancePolicy.FullAiRadiusMeters;
    public float splineRadiusMeters = TrafficPerformancePolicy.SplineRadiusMeters;
    public float transitionHysteresisMeters = 30f;
    [Range(4, 20)] public int maxFullAiVehicles = TrafficPerformancePolicy.MaxFullAiVehicles;
    public float tierUpdateIntervalSeconds = 0.2f;

    [Header("Density")]
    /// <summary>
    /// Vehicles kept on the road whatever the hour. The daily curve scales the pool
    /// between the two fractions, and a floor under it stops a quiet road at 03:00 from
    /// being empty - which is what a fixed count of ten vehicles bought before, at every
    /// hour of the day.
    /// </summary>
    public int minActiveVehicles = TrafficPerformancePolicy.FallbackMinActiveVehicles;
    public float minActiveFraction = TrafficPerformancePolicy.MinActiveFraction;
    public float maxActiveFraction = TrafficPerformancePolicy.MaxActiveFraction;
    public float updateDensityEverySeconds = 3f;

    [Header("Rush Hour")]
    public float morningPeakMinutes = 7.5f * 60f;
    public float eveningPeakMinutes = 17.5f * 60f;
    public float peakWidthMinutes = 80f;
    public float rushMultiplier = 5f;

    [Header("Debug Gizmos")]
    public bool drawTierGizmos = true;
    public bool drawTierRings = true;
    public float vehicleGizmoRadius = 1.2f;
    public Color fullAiGizmoColor = new Color(0.2f, 1f, 0.2f, 0.9f);
    public Color splineGizmoColor = new Color(0.2f, 0.9f, 1f, 0.9f);
    public Color dormantGizmoColor = new Color(0.55f, 0.55f, 0.55f, 0.8f);
    public Color fullRingColor = new Color(0.2f, 1f, 0.2f, 0.35f);
    public Color splineRingColor = new Color(0.2f, 0.9f, 1f, 0.3f);

    readonly List<PooledVehicle> pooled = new List<PooledVehicle>();
    readonly HashSet<PooledVehicle> reservedFull = new HashSet<PooledVehicle>();
    readonly List<(PooledVehicle vehicle, float distance)> candidates = new List<(PooledVehicle, float)>(50);
    readonly List<int> spawnNodeCandidates = new List<int>(512);
    // x = width, y = length, measured once from the prefab bounds.
    readonly Dictionary<GameObject, Vector2> prefabSizeCache = new Dictionary<GameObject, Vector2>();
    float spawnCandidateRefreshAt;
    float densityTick;
    float tierTick;
    float distanceLogTick;
    Transform playerBus;
    int forcedActiveCount = -1;
    bool loggedBusDistance;
    bool loggedFirstActivation;
    bool loggedMissingRoadGraph;
    bool loggedTrafficDisabled;
    float roadGraphRetryTick;
    float rehomeTick;
    bool initialDensityFillPending = true;
    // The warm-up fill is allowed to switch on in front of the loading card even
    // though those bodies are inside the camera cone. Every later activation must
    // happen off-camera instead.
    bool initialTierFillPending = true;

    /// <summary>True once the initial pool has been placed, or traffic is deliberately off.</summary>
    public bool WarmupComplete { get; private set; }

    void Start()
    {
        if (IsTrafficGraphDisabledByConfig())
        {
            if (!loggedTrafficDisabled)
            {
                Debug.Log("[VehiclePool] Traffic disabled for this run (no aligned AIRoadGraph configured).");
                loggedTrafficDisabled = true;
            }
            WarmupComplete = true;
            return;
        }

        ResolvePlayerBus();
        TryResolveRoadGraph();
        if (roadGraph != null) roadGraph.SyncDrivingSide();
        WarmPool();
        ApplyDensityNow();

        if (debugForceActiveCount > 0)
            SetForcedActiveCount(debugForceActiveCount);
        WarmupComplete = true;
    }

    void Update()
    {
        if (IsTrafficGraphDisabledByConfig())
            return;

        if (roadGraph == null || roadGraph.NodeCount == 0)
        {
            roadGraphRetryTick += Time.deltaTime;
            if (roadGraphRetryTick >= Mathf.Max(0.25f, roadGraphRetryIntervalSeconds))
            {
                roadGraphRetryTick = 0f;
                if (TryResolveRoadGraph())
                {
                    if (pooled.Count == 0)
                    {
                        WarmPool();
                        ApplyDensityNow();
                    }
                }
            }

            if (logBusDistance && !loggedMissingRoadGraph)
            {
                Debug.LogWarning("[VehiclePool] Bus distance check skipped: roadGraph missing.");
                loggedMissingRoadGraph = true;
            }
            return;
        }

        densityTick += Time.deltaTime;
        tierTick += Time.deltaTime;

        ResolvePlayerBus();

        if (densityTick >= updateDensityEverySeconds)
        {
            densityTick = 0f;
            if (roadGraph != null && roadGraph.SyncDrivingSide())
                RealignVehiclesToLanes();
            ApplyDensityNow();
        }

        rehomeTick += Time.deltaTime;
        if (rehomeTick >= Mathf.Max(.25f, rehomeIntervalSeconds))
        {
            rehomeTick = 0f;
            RehomeDistantVehicles();
        }

        if (tierTick >= tierUpdateIntervalSeconds)
        {
            tierTick = 0f;
            UpdateSimulationTiers();
        }

        if (logBusDistance)
        {
            distanceLogTick += Time.deltaTime;
            if (logBusDistanceOnce)
            {
                if (!loggedBusDistance && distanceLogTick >= Mathf.Max(0.5f, logBusDistanceIntervalSeconds))
                {
                    distanceLogTick = 0f;
                    loggedBusDistance = LogBusDistance();
                }
            }
            else if (distanceLogTick >= logBusDistanceIntervalSeconds)
            {
                distanceLogTick = 0f;
                LogBusDistance();
            }
        }
    }

    /// <summary>
    /// The bus the traffic is organised around: the one the mission actually drives,
    /// not whichever BusController a scene search happens to return first. The scene
    /// bus is replaced when the route starts, and a stale reference leaves the pool
    /// measuring every distance against a bus the player has left behind.
    /// </summary>
    void ResolvePlayerBus()
    {
        var mission = MissionManager.Instance;
        if (mission != null && mission.busController != null && mission.busController.gameObject.activeInHierarchy)
        {
            playerBus = mission.busController.transform;
            return;
        }

        if (playerBus != null) return;
        var bus = FindAnyObjectByType<BusController>();
        if (bus != null) playerBus = bus.transform;
    }

    /// <summary>
    /// Moves dormant vehicles that the bus has left behind to nodes around it. Only
    /// dormant bodies are touched, and they are invisible, so this recycles the pool
    /// without a visible teleport.
    /// </summary>
    void RehomeDistantVehicles()
    {
        if (playerBus == null || roadGraph == null || roadGraph.NodeCount == 0) return;

        float radius = Mathf.Max(splineRadiusMeters, rehomeDistanceMeters);
        float radiusSqr = radius * radius;
        int moved = 0;
        for (int i = 0; i < pooled.Count && moved < Mathf.Max(1, maxRehomesPerTick); i++)
        {
            var pv = pooled[i];
            if (pv == null || pv.go == null || pv.full == null || pv.spline == null) continue;
            if (pv.tier != SimulationTier.Dormant) continue;
            // Only vehicles the density curve currently wants are worth recycling;
            // the rest are part of the pool that stays off the road.
            if (!pv.enabledByDensity) continue;

            bool far = (pv.go.transform.position - playerBus.position).sqrMagnitude > radiusSqr;
            // A dormant body that is still in the player's view must not be
            // activated in place. Move it somewhere outside the camera cone first;
            // the tier update can then safely switch it on.
            bool visible = IsPositionInView(pv.go.transform.position,
                visibleDespawnDistanceMeters, offscreenSpawnFovPaddingDegrees);
            if (!far && !visible) continue;

            if (!TryRehomeVehicle(pv, true))
                continue;
            moved++;
        }

        if (moved > 0 && logSpawnEvents)
            Debug.Log($"[VehiclePool] Re-homed {moved} dormant vehicles near the bus.");
    }

    bool TryRehomeVehicle(PooledVehicle pv, bool avoidPlayerView)
    {
        if (pv == null || pv.go == null || pv.full == null || pv.spline == null)
            return false;
        if (!TryPickSpawnLink(pv, out int startNode, out int targetNode, out int lane,
                out float along01, avoidPlayerView))
            return false;

        pv.full.PlaceOnLane(startNode, targetNode, lane, -1, along01);
        pv.full.ResetLaneChange();
        pv.spline.Init(roadGraph, startNode, targetNode, lane, 0f);
        pv.laneIndex = lane;
        return true;
    }


    /// <summary>
    /// The selected city (and therefore the driving side) can become known a
    /// frame after the pool first warms. Re-seat every live body onto its lane
    /// when that happens; otherwise the early cars keep driving on the wrong
    /// side of a two-way road while their lane index says otherwise.
    /// </summary>
    void RealignVehiclesToLanes()
    {
        if (roadGraph == null) return;
        for (int i = 0; i < pooled.Count; i++)
        {
            var pv = pooled[i];
            if (pv == null || pv.go == null || pv.full == null || pv.spline == null) continue;

            var ai = pv.full;
            if (!roadGraph.IsValidNode(ai.currentNodeIndex) || !roadGraph.IsValidNode(ai.targetNodeIndex))
                continue;

            Vector3 from = roadGraph.GetNodePosition(ai.currentNodeIndex);
            Vector3 to = roadGraph.GetNodePosition(ai.targetNodeIndex);
            Vector3 segment = to - from;
            segment.y = 0f;
            float along = segment.sqrMagnitude > .0001f
                ? Mathf.Clamp01(Vector3.Dot(pv.go.transform.position - from, segment) / segment.sqrMagnitude)
                : 0.5f;

            float speedKmh = pv.tier == SimulationTier.Spline ? pv.spline.speedKmh : ai.currentSpeedKmh;
            // The spline tier owns laneIndex while it is active; the disabled full
            // controller's copy can be one junction behind after a turn.
            int lane = pv.tier == SimulationTier.Spline ? pv.spline.laneIndex : ai.laneIndex;
            // Dormant bodies are inactive, so re-seating them is invisible and
            // saves them from activating later on the old carriageway.
            ai.PlaceOnLane(ai.currentNodeIndex, ai.targetNodeIndex, lane, ai.previousNodeIndex, along);
            ai.ResetLaneChange();
            pv.spline.Init(roadGraph, ai.currentNodeIndex, ai.targetNodeIndex, lane, speedKmh);
            ai.SetVelocityWorld(ai.transform.forward * (speedKmh / 3.6f));
            pv.laneIndex = lane;
        }
    }

    void WarmPool()
    {
        if (IsTrafficGraphDisabledByConfig())
            return;

        if (roadGraph == null || roadGraph.NodeCount == 0)
            TryResolveRoadGraph();

        if (roadGraph == null || roadGraph.NodeCount == 0)
        {
            Debug.LogWarning("VehiclePool: Missing AIRoadGraph.");
            loggedMissingRoadGraph = true;
            return;
        }

        ApplyTrafficLaneWidth(roadGraph);

        int created = 0;
        int wheelCollidersRemoved = 0;
        int meshCollidersRemoved = 0;
        int animatorsDisabled = 0;
        for (int i = 0; i < poolSizePerScene; i++)
        {
            var picked = PickWeightedPrefab();
            if (picked == null || picked.prefab == null) continue;

            GameObject go = Instantiate(picked.prefab, transform);
            ImportedVehicleVisualRepair.DisableEmbeddedCameras(go.transform);
            // The prefab is a drivable vehicle in some packs and a static shell in
            // others. Traffic only ever needs the shell: wheel colliders, mesh
            // colliders, driver animators and per-car audio are removed once here
            // instead of running for the life of the pool.
            TrafficBodyOptimizer.Report optimized = TrafficBodyOptimizer.Prepare(go);
            wheelCollidersRemoved += optimized.wheelCollidersRemoved;
            meshCollidersRemoved += optimized.meshCollidersRemoved;
            animatorsDisabled += optimized.animatorsDisabled;
            // One prefab per model would otherwise put the same colour on every
            // instance of that model. The paint is a property block on the body
            // slot, so the shared material stays batched.
            TrafficLivery.Apply(go, picked.prefab.name, i);
            // A car casts a tiny shadow, but there are 160 of them and every renderer
            // part is submitted to the shadow pass again. Dropping traffic shadows
            // halves the work these vehicles cost while the buildings still shade
            // the street. The bodies still receive shadows and ambient light.
            foreach (var carRenderer in go.GetComponentsInChildren<Renderer>(true))
                carRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            Vector2 modelSize = GetPrefabSize(picked.prefab);
            float modelWidth = modelSize.x;

            var ai = go.GetComponent<AIVehicleController>();
            if (ai == null) ai = go.AddComponent<AIVehicleController>();
            ai.vehicleType = picked.type;
            ai.vehicleWidth = modelWidth;
            ai.vehicleLength = modelSize.y;

            var spline = go.GetComponent<SplineVehicle>();
            if (spline == null) spline = go.AddComponent<SplineVehicle>();
            float driverFactor = RealismRules.SpeedFactor(i * 41 + (int)picked.type);
            ai.desiredSpeedFactor = driverFactor;
            spline.desiredSpeedFactor = driverFactor;
            spline.vehicleWidth = modelWidth;

            var participant = go.GetComponent<TrafficParticipant>();
            if (participant == null) participant = go.AddComponent<TrafficParticipant>();
            participant.width = modelWidth;
            participant.length = modelSize.y;
            var rb = go.GetComponent<Rigidbody>();
            if (rb == null) rb = go.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;

            int startNode;
            int targetNode;
            int lane;
            float along01;
            if (!TryPickSpawnLink(null, out startNode, out targetNode, out lane, out along01))
            {
                // No clear link anywhere in the pool's band. Keep the body dormant at
                // a legal link rather than putting it on top of the player; Rehome
                // will place it once there is room.
                startNode = roadGraph.GetRandomNodeIndex(requireOutgoingConnection: true);
                targetNode = roadGraph.GetRandomNextNode(startNode, -1);
                lane = 0;
                along01 = 0.5f;
                if (!roadGraph.IsValidNode(startNode) || !roadGraph.IsValidNode(targetNode))
                    continue;
            }

            // Resolve a legal directed road link before choosing its lane.
            ai.Init(roadGraph, startNode, lane, ai.vehicleType);
            // The lane is chosen after the link exists, so the body is placed on that
            // lane's line rather than on the road's centre line. Spawning partway
            // along the link also stops every car in a district appearing at the
            // same junction at the same time.
            ai.PlaceOnLane(startNode, targetNode, lane, -1, along01);
            spline.Init(roadGraph, startNode, targetNode, lane, 0f);

            SetTier(go, ai, spline, rb, SimulationTier.Dormant, true);

            pooled.Add(new PooledVehicle
            {
                go = go,
                rb = rb,
                full = ai,
                spline = spline,
                type = ai.vehicleType,
                laneIndex = lane,
                enabledByDensity = false,
                tier = SimulationTier.Dormant
            });
            created++;
        }

        initialDensityFillPending = true;
        initialTierFillPending = true;
        if (logSpawnEvents)
            Debug.Log($"[VehiclePool] Warmed pool: {created}/{poolSizePerScene} vehicles created " +
                      $"(roadGraphNodes={roadGraph.NodeCount}, laneWidth={roadGraph.MinimumLaneWidth:0.00}m, " +
                      $"wheelCollidersRemoved={wheelCollidersRemoved}, meshCollidersRemoved={meshCollidersRemoved}, " +
                      $"animatorsDisabled={animatorsDisabled}).");
    }

    /// <summary>
    /// Measures the traffic models this pool will actually spawn and raises the
    /// graph's lane spacing until the widest one fits. OSM lane tags are advisory:
    /// a 2.75 m residential lane is common in the downloaded cities, and a bus or
    /// a wide van placed in it visually overhangs the kerb and the centre line.
    /// The measurement is cached per prefab, so the cost is paid once during the
    /// loading card rather than every spawn.
    /// </summary>
    void ApplyTrafficLaneWidth(AIRoadGraph graph)
    {
        if (graph == null || prefabs == null || prefabs.Length == 0)
            return;

        float widest = 0f;
        for (int i = 0; i < prefabs.Length; i++)
        {
            var entry = prefabs[i];
            if (entry == null || entry.prefab == null)
                continue;
            widest = Mathf.Max(widest, GetPrefabSize(entry.prefab).x);
        }

        if (widest <= 0f)
            return;

        WidestVehicleWidth = widest;
        float required = Mathf.Clamp(
            widest + Mathf.Max(0f, laneWidthPaddingMeters),
            AIRoadGraph.MinLaneWidth,
            Mathf.Max(AIRoadGraph.MinLaneWidth, maxLaneWidthMeters));
        graph.SetMinimumLaneWidth(required);
        // Keep the rendered ribbon in step if a custom pool or a late catalog
        // changes the graph floor before a road rebuild.
        RoadWidthPolicy.Request(required);

        if (logSpawnEvents)
        {
            Debug.Log($"[VehiclePool] Widest traffic model {widest:0.00}m; lane spacing floor is now " +
                      $"{graph.MinimumLaneWidth:0.00}m (padding {laneWidthPaddingMeters:0.00}m).");
        }
    }

    Vector2 GetPrefabSize(GameObject prefab)
    {
        if (prefab == null) return new Vector2(2f, 4.2f);
        if (prefabSizeCache.TryGetValue(prefab, out Vector2 cached))
            return cached;

        Vector2 measured = MeasurePrefabSize(prefab);
        prefabSizeCache[prefab] = measured;
        return measured;
    }

    public static Vector2 MeasurePrefabSize(GameObject prefab)
    {
        if (prefab == null) return new Vector2(2f, 4.2f);

        Bounds combined = default;
        bool hasBounds = false;

        var renderers = prefab.GetComponentsInChildren<Renderer>(true);
        if (renderers != null)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i] == null) continue;
                if (!hasBounds) { combined = renderers[i].bounds; hasBounds = true; }
                else combined.Encapsulate(renderers[i].bounds);
            }
        }

        if (!hasBounds || (combined.size.x < .01f && combined.size.z < .01f))
        {
            var colliders = prefab.GetComponentsInChildren<Collider>(true);
            if (colliders != null)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i] == null) continue;
                    if (!hasBounds) { combined = colliders[i].bounds; hasBounds = true; }
                    else combined.Encapsulate(colliders[i].bounds);
                }
            }
        }

        if (!hasBounds)
            return new Vector2(2f, 4.2f);

        // Vehicles are modelled with their length along X or Z depending on the
        // export. The smaller horizontal extent is the width in both cases; the
        // larger one is the body length used for following gaps.
        Vector2 size = new Vector2(
            Mathf.Clamp(Mathf.Min(combined.size.x, combined.size.z), 1.2f, 6f),
            Mathf.Clamp(Mathf.Max(combined.size.x, combined.size.z), 2f, 18f));

        // A bounds box that is still effectively a point (an unloaded skinned
        // mesh, for example) is not a useful model measurement.
        if (size.x <= 1.21f && size.y <= 2.01f)
            return new Vector2(2f, 4.2f);

        return size;
    }

    /// <summary>
    /// Chooses a directed link and lane whose spawn point is clear of the player and
    /// of every other traffic body. Looping the pool around the bus is what populates
    /// the street; placing one of those loops on the bus's own bonnet is what made
    /// traffic appear to spawn on top of the player, so a candidate is only accepted
    /// when it is far enough away and not in the player's forward corridor.
    /// </summary>
    bool TryPickSpawnLink(PooledVehicle ignore, out int startNode, out int targetNode, out int lane)
    {
        return TryPickSpawnLink(ignore, out startNode, out targetNode, out lane, out _);
    }

    bool TryPickSpawnLink(PooledVehicle ignore, out int startNode, out int targetNode, out int lane, out float along01, bool avoidPlayerView = false)
    {
        startNode = -1;
        targetNode = -1;
        lane = 0;
        along01 = 0.5f;
        if (roadGraph == null || roadGraph.NodeCount == 0)
            return false;

        RefreshSpawnNodeCandidates();
        int localCount = spawnNodeCandidates.Count;
        int attempts = Mathf.Max(16, spawnClearanceAttempts);
        for (int i = 0; i < attempts; i++)
        {
            // Sample from the streets around the bus. Choosing from the whole
            // city graph meant a 40k-node city needed thousands of tries before
            // one node happened to fall inside the 520 m band, so the pool
            // warmed with almost no traffic in sight.
            int node;
            if (localCount <= 0)
            {
                node = roadGraph.GetRandomNodeIndex(requireOutgoingConnection: true);
            }
            else if (avoidPlayerView)
            {
                // Mid-drive re-homes must appear outside the windscreen. Sample the
                // whole local band rather than the forward-biased warm-up slice.
                node = spawnNodeCandidates[Random.Range(0, localCount)];
            }
            else
            {
                // Alternate between the forward-view slice and the whole local
                // band. The visible slice fills first, while the alternating
                // attempts still let traffic appear behind the bus instead of
                // making the world one-sided.
                int frontCount = Mathf.Max(1, Mathf.CeilToInt(localCount * Mathf.Clamp01(spawnFovShare)));
                int pick = i % 2 == 0 ? Random.Range(0, frontCount) : Random.Range(0, localCount);
                node = spawnNodeCandidates[pick];
            }
            if (!roadGraph.IsValidNode(node)) continue;

            int next = roadGraph.GetRandomNextNode(node, -1);
            if (!roadGraph.IsValidNode(next)) continue;

            int laneCount = Mathf.Max(1, roadGraph.GetLaneCount(node, next));
            int candidateLane = Random.Range(0, laneCount);
            // Spread bodies along the link. Junction-only spawning created visible
            // clumps and made re-homed traffic look like it had teleported.
            float candidateAlong = Random.Range(0.12f, 0.88f);
            Vector3 point = GetSpawnPoint(node, next, candidateLane, candidateAlong);
            if (!IsSpawnPointClear(point, ignore)) continue;

            // Keep the warm pool in the same 45-520 m band as before. The band is
            // only applied once there is a player to measure from.
            if (playerBus != null)
            {
                float distance = Vector3.Distance(playerBus.position, point);
                // Re-home inside the spline tier so the body actually drives in
                // view after it is enabled. A point beyond splineRadius would be
                // switched straight back to dormant and quietly consume the
                // density budget without putting a car on the road.
                float maxDistance = avoidPlayerView
                    ? Mathf.Max(520f, splineRadiusMeters)
                    : 520f;
                if (distance < minSpawnDistanceFromPlayerMeters || distance > maxDistance) continue;
                if (avoidPlayerView && IsPositionInView(point,
                        Mathf.Max(hardDespawnDistanceMeters, visibleDespawnDistanceMeters),
                        offscreenSpawnFovPaddingDegrees))
                    continue;

                // Bias the first half of the attempts towards links that lead
                // back towards the bus. A re-home on a road heading away only
                // drives out of range and gets recycled without ever entering
                // view; the second half still accepts any cleared street so a
                // one-way or sparse band can always be filled.
                if (avoidPlayerView && i < attempts / 2)
                {
                    Vector3 linkDirection = roadGraph.GetNodePosition(next) -
                                            roadGraph.GetNodePosition(node);
                    linkDirection.y = 0f;
                    Vector3 towardsBus = playerBus.position - point;
                    towardsBus.y = 0f;
                    if (linkDirection.sqrMagnitude > .0001f && towardsBus.sqrMagnitude > .0001f &&
                        Vector3.Dot(linkDirection.normalized, towardsBus.normalized) < -.35f)
                        continue;
                }
            }

            startNode = node;
            targetNode = next;
            lane = candidateLane;
            along01 = candidateAlong;
            return true;
        }

        return false;
    }

    void RefreshSpawnNodeCandidates()
    {
        if (roadGraph == null || roadGraph.NodeCount == 0)
            return;

        // A full scan costs one pass over the graph. Refreshing it once a second
        // is cheaper than the old loop's rejected random attempts on a big city
        // and keeps the candidate band centred on a moving bus.
        float now = Time.unscaledTime;
        if (spawnNodeCandidates.Count > 0 && now < spawnCandidateRefreshAt)
            return;

        ResolvePlayerBus();
        Vector3 center = playerBus != null ? playerBus.position : transform.position;
        float radius = Mathf.Max(splineRadiusMeters, rehomeDistanceMeters) + 80f;
        spawnNodeCandidates.Clear();
        roadGraph.CollectNodesWithin(center, radius, spawnNodeCandidates);

        if (spawnNodeCandidates.Count > 1 && playerBus != null)
        {
            Vector3 forward = ViewPriority.ResolveForward(playerBus);
            spawnNodeCandidates.Sort((a, b) =>
                ViewPriority.Score(center, forward, ViewPriority.DefaultFovDegrees,
                    roadGraph.GetNodePosition(a)).CompareTo(
                ViewPriority.Score(center, forward, ViewPriority.DefaultFovDegrees,
                    roadGraph.GetNodePosition(b))));
        }
        spawnCandidateRefreshAt = now + 1.25f;
    }

    Vector3 GetSpawnPoint(int startNode, int targetNode, int lane, float along01 = 0f)
    {
        Vector3 start = roadGraph.GetNodePosition(startNode);
        Vector3 end = roadGraph.GetNodePosition(targetNode);
        Vector3 segment = end - start;
        segment.y = 0f;
        if (segment.sqrMagnitude < .0001f) return start;
        segment.Normalize();
        Vector3 point = Vector3.Lerp(start, end, Mathf.Clamp(along01, 0.02f, 0.98f));
        return point + Vector3.Cross(Vector3.up, segment) * roadGraph.GetLaneOffset(startNode, targetNode, lane);
    }

    bool IsSpawnPointClear(Vector3 point, PooledVehicle ignore)
    {
        if (playerBus != null)
        {
            Vector3 offset = point - playerBus.position;
            offset.y = 0f;
            float distance = offset.magnitude;
            if (distance < minSpawnDistanceFromPlayerMeters) return false;

            // Directly in front of the bus is the one place a spawn is guaranteed to
            // be driven into at speed, even when the straight-line distance is legal.
            float ahead = Vector3.Dot(playerBus.forward, offset);
            float lateral = Mathf.Abs(Vector3.Dot(playerBus.right, offset));
            if (ahead > 0f && ahead < spawnAheadClearanceMeters && lateral < spawnAheadHalfWidthMeters)
                return false;
        }

        float clearSqr = Mathf.Max(2f, vehicleSpawnClearanceMeters) * Mathf.Max(2f, vehicleSpawnClearanceMeters);
        for (int i = 0; i < pooled.Count; i++)
        {
            var other = pooled[i];
            if (other == null || other == ignore || other.go == null) continue;
            Vector3 offset = other.go.transform.position - point;
            offset.y = 0f;
            if (offset.sqrMagnitude < clearSqr) return false;
        }

        return true;
    }

    bool IsTrafficGraphDisabledByConfig()
    {
        return roadGraph == null && !allowAutoResolveRoadGraph;
    }

    bool TryResolveRoadGraph()
    {
        if (!allowAutoResolveRoadGraph)
            return false;

        if (roadGraph != null && roadGraph.NodeCount > 0)
            return true;

        var graphs = FindObjectsByType<AIRoadGraph>(FindObjectsInactive.Exclude);
        if (graphs == null || graphs.Length == 0)
            return false;

        AIRoadGraph best = null;
        int bestNearestNode = -1;
        float bestSqr = float.MaxValue;
        Vector3 origin = playerBus != null ? playerBus.position : transform.position;

        for (int i = 0; i < graphs.Length; i++)
        {
            var g = graphs[i];
            if (g == null || g.NodeCount <= 0)
                continue;
            // A depot's lane circuit is a private loop, not a city graph, and it is
            // nearest the player exactly when the player is at that depot.
            if (g.privateLaneCircuit)
                continue;

            int nearest = g.GetNearestNodeIndex(origin);
            if (!g.IsValidNode(nearest))
                continue;

            float sqr = (g.GetNodePosition(nearest) - origin).sqrMagnitude;
            if (best == null || sqr < bestSqr)
            {
                best = g;
                bestNearestNode = nearest;
                bestSqr = sqr;
            }
        }

        if (best == null)
            return false;

        float bindDistance = Mathf.Sqrt(bestSqr);
        if (bindDistance > Mathf.Max(50f, maxAutoBindDistanceMeters))
        {
            if (!loggedMissingRoadGraph)
            {
                Debug.LogWarning(
                    $"[VehiclePool] Found AIRoadGraph '{best.name}' but nearest node is {bindDistance:0.0}m away. " +
                    "Skipping auto-bind to avoid misaligned traffic graph.");
            }
            loggedMissingRoadGraph = true;
            return false;
        }

        roadGraph = best;
        roadGraph.SyncDrivingSide();
        loggedMissingRoadGraph = false;
        Debug.Log($"[VehiclePool] Auto-bound AIRoadGraph '{best.name}' ({best.NodeCount} nodes, nearestNode={bestNearestNode}, distance={bindDistance:0.0}m).");
        return true;
    }

    void ApplyDensityNow()
    {
        if (pooled.Count == 0) return;

        int targetActive;
        if (forcedActiveCount >= 0)
        {
            targetActive = Mathf.Clamp(forcedActiveCount, 0, pooled.Count);
            // Stress/debug runs ask for an exact count. Keep the historic
            // deterministic selection for that path.
            for (int i = 0; i < pooled.Count; i++)
                pooled[i].enabledByDensity = i < targetActive;
            initialDensityFillPending = false;
            return;
        }

        // Use the shaped daily volume (night trough, daytime flow, two peaks),
        // not the raw rush multiplier, which was flat for every non-peak hour.
        float volume = GetTrafficDensityVolume();
        float targetFraction = Mathf.Lerp(minActiveFraction, maxActiveFraction, volume);
        targetActive = Mathf.Clamp(
            Mathf.Max(minActiveVehicles, Mathf.RoundToInt(targetFraction * pooled.Count)),
            0, pooled.Count);

        // The warm-up fill already sits in the streets ahead of the bus under the
        // loading card. Keep that visible first impression instead of immediately
        // recycling it behind the camera. From then on every change is off-camera.
        if (initialDensityFillPending)
        {
            for (int i = 0; i < pooled.Count; i++)
                pooled[i].enabledByDensity = i < targetActive;
            initialDensityFillPending = false;
            return;
        }

        if (playerBus == null)
        {
            for (int i = 0; i < pooled.Count; i++)
                pooled[i].enabledByDensity = i < targetActive;
            return;
        }

        // Count the vehicles the curve already wants. Anything still visible in
        // the camera cone is kept; the density target is met by retiring cars
        // that are already out of view and by recycling dormant bodies outside
        // the cone. That removes the fixed-index pop on/off at the target
        // boundary that made traffic appear and disappear in front of the bus.
        int active = 0;
        for (int i = 0; i < pooled.Count; i++)
        {
            var pv = pooled[i];
            if (pv == null || pv.go == null || !pv.enabledByDensity) continue;
            float distance = GetDistanceToBus(pv.go.transform.position);
            bool visible = IsVehicleVisible(pv);
            if (distance > hardDespawnDistanceMeters && !visible)
            {
                pv.enabledByDensity = false;
                continue;
            }
            active++;
        }

        // A dormant body that is still visible would pop when the tier update
        // switches it back on. Move it out of the cone now; if there is no clear
        // off-camera street, it simply stays dormant for this tick.
        for (int i = 0; i < pooled.Count; i++)
        {
            var pv = pooled[i];
            if (pv == null || pv.go == null || !pv.enabledByDensity ||
                pv.tier != SimulationTier.Dormant)
                continue;
            if (IsVehicleVisible(pv))
                TryRehomeVehicle(pv, true);
        }

        if (active > targetActive)
        {
            candidates.Clear();
            for (int i = 0; i < pooled.Count; i++)
            {
                var pv = pooled[i];
                if (pv == null || pv.go == null || !pv.enabledByDensity) continue;
                if (IsVehicleVisible(pv)) continue;
                float distance = GetDistanceToBus(pv.go.transform.position);
                if (distance < visibleDespawnDistanceMeters) continue;
                candidates.Add((pv, distance));
            }
            candidates.Sort((a, b) => b.distance.CompareTo(a.distance));
            for (int i = 0; i < candidates.Count && active > targetActive; i++)
            {
                candidates[i].vehicle.enabledByDensity = false;
                active--;
            }
        }
        else if (active < targetActive)
        {
            // Retire the out-of-view bodies the previous tick disabled before
            // choosing replacements. Otherwise a density rise (for example
            // immediately after the route teleports the bus) has to wait for the
            // next tier pass before the freed bodies become candidates, and the
            // new street stays sparse for a few seconds.
            for (int i = 0; i < pooled.Count; i++)
            {
                var pv = pooled[i];
                if (pv == null || pv.go == null || pv.enabledByDensity ||
                    pv.tier == SimulationTier.Dormant)
                    continue;
                ApplyTierIfNeeded(pv, SimulationTier.Dormant);
            }

            candidates.Clear();
            for (int i = 0; i < pooled.Count; i++)
            {
                var pv = pooled[i];
                if (pv == null || pv.go == null || pv.enabledByDensity ||
                    pv.tier != SimulationTier.Dormant)
                    continue;
                candidates.Add((pv, GetDistanceToBus(pv.go.transform.position)));
            }
            // Prefer already-near bodies, then recycle farther ones into an
            // off-camera street. This keeps the same pool distributed around the
            // bus instead of clustering every car at its initial spawn point.
            candidates.Sort((a, b) => a.distance.CompareTo(b.distance));
            for (int i = 0; i < candidates.Count && active < targetActive; i++)
            {
                var pv = candidates[i].vehicle;
                bool visible = IsVehicleVisible(pv);
                bool tooFar = candidates[i].distance >
                              Mathf.Max(splineRadiusMeters, rehomeDistanceMeters);
                if (visible || tooFar)
                {
                    if (!TryRehomeVehicle(pv, true))
                        continue;
                }
                pv.enabledByDensity = true;
                active++;
            }
        }
    }

    public float GetTrafficDensityVolume()
    {
        // Traffic follows the clock the player can actually see. TimeOfDaySystem
        // tracks ScheduleManager during play, but it is also the clock a pinned
        // (smoke) run holds still; reading the schedule alone locked density to
        // whatever minute the schedule happened to start on, so the day curve
        // never actually moved.
        float now;
        if (TimeOfDaySystem.Instance != null) now = TimeOfDaySystem.Instance.currentTimeMinutes;
        else if (ScheduleManager.Instance != null) now = ScheduleManager.Instance.currentTimeMinutes;
        else now = 12f * 60f;
        return RealismRules.DailyTrafficVolume(now, morningPeakMinutes,
            eveningPeakMinutes, peakWidthMinutes);
    }

    bool IsVehicleVisible(PooledVehicle pv)
    {
        if (pv == null || pv.go == null || playerBus == null) return false;
        return IsPositionInView(pv.go.transform.position, hardDespawnDistanceMeters,
            offscreenSpawnFovPaddingDegrees);
    }

    bool IsPositionInView(Vector3 point, float maxDistance, float extraFovDegrees = 0f)
    {
        if (playerBus == null) return false;
        Vector3 forward = ViewPriority.ResolveForward(playerBus);
        return ViewPriority.IsInsideView(playerBus.position, forward,
            ViewPriority.DefaultFovDegrees + Mathf.Max(0f, extraFovDegrees),
            Mathf.Max(0f, maxDistance), point);
    }

    public void SetForcedActiveCount(int count)
    {
        forcedActiveCount = Mathf.Clamp(count, -1, pooled.Count);
        ApplyDensityNow();
        UpdateSimulationTiers();
    }

    public int GetPoolCount()
    {
        return pooled.Count;
    }

    public void GetTierCounts(out int fullAi, out int spline, out int dormant, out int densityEnabled)
    {
        fullAi = 0;
        spline = 0;
        dormant = 0;
        densityEnabled = 0;
        for (int i = 0; i < pooled.Count; i++)
        {
            if (pooled[i].enabledByDensity) densityEnabled++;
            switch (pooled[i].tier)
            {
                case SimulationTier.FullAI: fullAi++; break;
                case SimulationTier.Spline: spline++; break;
                default: dormant++; break;
            }
        }
    }

    void UpdateSimulationTiers()
    {
        int fullUsed = 0;
        int ownFull = 0;
        foreach (var vehicle in pooled) if (vehicle.tier == SimulationTier.FullAI) ownFull++;
        int externalFull = Mathf.Max(0, AIVehicleController.ActiveCount - ownFull);
        int clampedCap = Mathf.Max(0, Mathf.Min(maxFullAiVehicles, TrafficPerformancePolicy.FullAiCeiling - externalFull));
        reservedFull.Clear(); candidates.Clear();

        // Keep existing full-AI vehicles if still within hysteresis, then fill remaining budget.
        for (int i = 0; i < pooled.Count; i++)
        {
            var pv = pooled[i];
            if (!pv.enabledByDensity)
            {
                ApplyTierIfNeeded(pv, SimulationTier.Dormant);
                continue;
            }

            float dist = GetDistanceToBus(pv.go.transform.position);
            // A body outside the simulation band but still inside the camera
            // cone would visibly blink out. Keep it on the cheap tier until it
            // has driven out of view; the next density tick can then retire it.
            bool visible = dist <= hardDespawnDistanceMeters && IsVehicleVisible(pv);
            if (dist > hardDespawnDistanceMeters && !visible)
            {
                pv.enabledByDensity = false;
                ApplyTierIfNeeded(pv, SimulationTier.Dormant);
                continue;
            }

            // Beyond the cheap tier and out of sight there is no simulation to
            // run. Retire it from the density count instead of leaving it dormant
            // but still counted as an active car; ApplyDensityNow will recycle it
            // back into the visible band on a later tick.
            if (dist > splineRadiusMeters && !visible)
            {
                pv.enabledByDensity = false;
                ApplyTierIfNeeded(pv, SimulationTier.Dormant);
                continue;
            }

            if (pv.tier == SimulationTier.Spline)
                ApplySplineCadence(pv, dist);

            bool keepFull = pv.tier == SimulationTier.FullAI && dist <= (fullAiRadiusMeters + transitionHysteresisMeters);
            if (keepFull && fullUsed < clampedCap)
            {
                fullUsed++;
                reservedFull.Add(pv);
                ApplyTierIfNeeded(pv, SimulationTier.FullAI);
                continue;
            }

            candidates.Add((pv, dist));
        }

        candidates.Sort((a, b) => a.distance.CompareTo(b.distance));

        for (int i = 0; i < candidates.Count; i++)
        {
            var pv = candidates[i].vehicle;
            float dist = candidates[i].distance;
            if (reservedFull.Contains(pv))
                continue;

            if (dist <= fullAiRadiusMeters && fullUsed < clampedCap)
            {
                fullUsed++;
                ApplyTierIfNeeded(pv, SimulationTier.FullAI);
            }
            else if (dist <= splineRadiusMeters)
            {
                ApplyTierIfNeeded(pv, SimulationTier.Spline);
                ApplySplineCadence(pv, dist);
            }
            else if (dist <= hardDespawnDistanceMeters && IsVehicleVisible(pv))
            {
                ApplyTierIfNeeded(pv, SimulationTier.Spline);
                ApplySplineCadence(pv, dist);
            }
            else
            {
                ApplyTierIfNeeded(pv, SimulationTier.Dormant);
            }
        }

        initialTierFillPending = false;
    }

    float GetDistanceToBus(Vector3 pos)
    {
        // Before the bus exists (scene load) a sentinel distance would make the
        // tier pass retire every warmed body on its first frame. Measuring from
        // the pool origin instead keeps the warm fill alive until the bus appears.
        if (playerBus == null) return 0f;
        return Vector3.Distance(playerBus.position, pos);
    }

    /// <summary>
    /// A spline car still has to perceive the road, but not sixty times a second
    /// when it is a few hundred metres away. The step is accumulated inside the
    /// controller, so the car keeps its average speed and only the update rate
    /// drops. Near traffic stays per-frame.
    /// </summary>
    static void ApplySplineCadence(PooledVehicle pv, float distance)
    {
        if (pv == null || pv.spline == null)
            return;
        float interval = TrafficPerformancePolicy.SplineUpdateInterval(distance);
        if (!Mathf.Approximately(pv.spline.updateIntervalSeconds, interval))
            pv.spline.updateIntervalSeconds = interval;
    }

    void ApplyTierIfNeeded(PooledVehicle pv, SimulationTier desired)
    {
        if (pv == null || pv.go == null) return;

        // Never switch a visible body on or off once the warm-up is over. A
        // dormant body in the camera cone must first be re-homed outside it;
        // RehomeDistantVehicles does that on its own cadence, so waiting a tick
        // here is cheap and invisible. During the initial fill the loading card
        // covers the scene, so the warmed bodies are intentionally switched on in
        // their placed view.
        if (!initialTierFillPending && pv.tier == SimulationTier.Dormant &&
            desired != SimulationTier.Dormant && IsVehicleVisible(pv))
            return;

        // The inverse case: density asked for a retirement, but the car is still
        // on screen. Keep the cheap spline tier alive until it drives out of
        // view instead of popping it off the road.
        if (desired == SimulationTier.Dormant && pv.tier != SimulationTier.Dormant &&
            IsVehicleVisible(pv))
            desired = SimulationTier.Spline;

        if (pv.tier == desired)
        {
            // Keep object enabled while active tiers are running.
            if (desired == SimulationTier.Dormant)
                pv.go.SetActive(false);
            else if (!pv.go.activeSelf)
                pv.go.SetActive(true);
            return;
        }

        bool wasDormant = pv.tier == SimulationTier.Dormant;
        pv.go.SetActive(true);

        // The outgoing tier owns the live lane index. Carrying the cached pool
        // value into a promotion/demotion can drop a car back onto the lane it
        // used before the last turn, which reads as a lane change at a junction.
        int sourceLane = pv.laneIndex;
        if (pv.tier == SimulationTier.Spline && pv.spline != null)
            sourceLane = pv.spline.laneIndex;
        else if (pv.tier == SimulationTier.FullAI && pv.full != null)
            sourceLane = pv.full.laneIndex;
        pv.laneIndex = Mathf.Max(0, sourceLane);

        if (desired == SimulationTier.FullAI)
        {
            // Seamless promotion: preserve pose + spline velocity estimate.
            Vector3 vel = pv.spline != null ? pv.spline.GetVelocityWorld() : Vector3.zero;
            int currentNode = pv.spline != null ? pv.spline.currentNodeIndex : pv.full.currentNodeIndex;
            int targetNode = pv.spline != null ? pv.spline.targetNodeIndex : pv.full.targetNodeIndex;
            int previousNode = pv.spline != null ? pv.spline.previousNodeIndex : pv.full.previousNodeIndex;
            if (pv.rb != null)
            {
                pv.rb.isKinematic = false;
                // Physics interpolation was switched off while this body was a
                // kinematic spline vehicle. Leaving it off on promotion makes the
                // rigidbody step at the physics rate, which is what read as jumpy
                // traffic near the player.
                pv.rb.interpolation = RigidbodyInterpolation.Interpolate;
                pv.rb.angularVelocity = Vector3.zero;
            }

            if (pv.full != null)
            {
                pv.full.SetRuntimeState(roadGraph, currentNode, targetNode, pv.laneIndex, pv.type, vel.magnitude * 3.6f, previousNode);
                pv.full.enabled = true;
                pv.full.SetVelocityWorld(vel);
            }
            if (pv.spline != null)
                pv.spline.enabled = false;
        }
        else if (desired == SimulationTier.Spline)
        {
            // Seamless demotion: preserve pose + current rigidbody velocity.
            Vector3 vel = pv.full != null ? pv.full.GetVelocityWorld() : Vector3.zero;
            int currentNode = pv.full != null ? pv.full.currentNodeIndex : pv.spline.currentNodeIndex;
            int targetNode = pv.full != null ? pv.full.targetNodeIndex : pv.spline.targetNodeIndex;
            int previousNode = pv.full != null ? pv.full.previousNodeIndex : pv.spline.previousNodeIndex;
            if (pv.spline != null)
            {
                pv.spline.Init(roadGraph, currentNode, targetNode, pv.laneIndex, vel.magnitude * 3.6f, previousNode);
                pv.spline.enabled = true;
                pv.spline.SetVelocityWorld(vel);
            }
            if (pv.full != null)
                pv.full.enabled = false;
            if (pv.rb != null)
            {
                if (!pv.rb.isKinematic) { pv.rb.linearVelocity = Vector3.zero; pv.rb.angularVelocity = Vector3.zero; }
                pv.rb.isKinematic = true;
                // A kinematic body moved through transform.position must not be
                // rendered with physics interpolation, or it visibly stutters.
                pv.rb.interpolation = RigidbodyInterpolation.None;
            }
        }
        else
        {
            if (pv.full != null) pv.full.enabled = false;
            if (pv.spline != null) pv.spline.enabled = false;
            if (pv.rb != null)
            {
                if (!pv.rb.isKinematic) { pv.rb.linearVelocity = Vector3.zero; pv.rb.angularVelocity = Vector3.zero; }
                pv.rb.isKinematic = true;
                pv.rb.interpolation = RigidbodyInterpolation.None;
            }
            pv.go.SetActive(false);
        }

        if (logSpawnEvents && wasDormant && desired != SimulationTier.Dormant)
        {
            if (!logFirstActivationOnly || !loggedFirstActivation)
            {
                Debug.Log($"[VehiclePool] Activated vehicle '{pv.go.name}' tier={desired} type={pv.type} lane={pv.laneIndex}.");
                loggedFirstActivation = true;
            }
        }

        if (pv.rb != null) pv.rb.detectCollisions = desired == SimulationTier.FullAI;
        // A body that leaves the spline tier goes back to a per-frame update, so a
        // later promotion or re-activation never inherits a stale coarse cadence.
        if (desired != SimulationTier.Spline && pv.spline != null)
            pv.spline.updateIntervalSeconds = 0f;
        pv.tier = desired;
    }

    bool LogBusDistance()
    {
        if (playerBus == null) return false;
        if (roadGraph == null || roadGraph.NodeCount == 0)
        {
            return false;
        }

        int nearest = roadGraph.GetNearestNodeIndex(playerBus.position);
        if (!roadGraph.IsValidNode(nearest))
        {
            Debug.LogWarning("[VehiclePool] Bus distance check skipped: no valid road node found.");
            return false;
        }

        float dist = Vector3.Distance(playerBus.position, roadGraph.GetNodePosition(nearest));
        Debug.Log($"[VehiclePool] Bus distance to nearest road node: {dist:0.0}m (node={nearest}).");
        return true;
    }

    void SetTier(GameObject go, AIVehicleController full, SplineVehicle spline, Rigidbody rb, SimulationTier tier, bool initial = false)
    {
        if (initial)
            go.SetActive(tier != SimulationTier.Dormant);

        if (full != null) full.enabled = tier == SimulationTier.FullAI;
        if (spline != null) spline.enabled = tier == SimulationTier.Spline;
        if (rb != null)
        {
            rb.isKinematic = tier != SimulationTier.FullAI;
            rb.interpolation = tier == SimulationTier.FullAI
                ? RigidbodyInterpolation.Interpolate
                : RigidbodyInterpolation.None;
        }
        if (tier == SimulationTier.Dormant)
            go.SetActive(false);
    }

    public float GetTrafficDensityMultiplier()
    {
        float now = ScheduleManager.Instance != null
            ? ScheduleManager.Instance.currentTimeMinutes
            : 12f * 60f;

        return RealismRules.DailyDensity(now, morningPeakMinutes, eveningPeakMinutes, peakWidthMinutes, rushMultiplier);
    }

    VehiclePrefabEntry PickWeightedPrefab()
    {
        if (prefabs == null || prefabs.Length == 0) return null;

        float sum = 0f;
        for (int i = 0; i < prefabs.Length; i++)
            sum += Mathf.Max(0.01f, prefabs[i].weight);

        float roll = Random.Range(0f, sum);
        float acc = 0f;
        for (int i = 0; i < prefabs.Length; i++)
        {
            acc += Mathf.Max(0.01f, prefabs[i].weight);
            if (roll <= acc) return prefabs[i];
        }
        return prefabs[prefabs.Length - 1];
    }

    void OnDrawGizmos()
    {
        if (!drawTierGizmos) return;

        if (drawTierRings && playerBus != null)
        {
            Gizmos.color = fullRingColor;
            Gizmos.DrawWireSphere(playerBus.position, Mathf.Max(0f, fullAiRadiusMeters));
            Gizmos.color = splineRingColor;
            Gizmos.DrawWireSphere(playerBus.position, Mathf.Max(0f, splineRadiusMeters));
        }

        for (int i = 0; i < pooled.Count; i++)
        {
            var pv = pooled[i];
            if (pv == null || pv.go == null) continue;

            switch (pv.tier)
            {
                case SimulationTier.FullAI:
                    Gizmos.color = fullAiGizmoColor;
                    break;
                case SimulationTier.Spline:
                    Gizmos.color = splineGizmoColor;
                    break;
                default:
                    Gizmos.color = dormantGizmoColor;
                    break;
            }

            Vector3 p = pv.go.transform.position + Vector3.up * 0.6f;
            Gizmos.DrawSphere(p, vehicleGizmoRadius);
        }
    }
}
