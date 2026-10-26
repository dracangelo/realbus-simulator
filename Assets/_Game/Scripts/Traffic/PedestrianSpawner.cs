using UnityEngine;
using System.Collections.Generic;

public class PedestrianSpawner : MonoBehaviour
{
    public static PedestrianSpawner Instance { get; private set; }

    [System.Serializable]
    public class ZebraCrossing
    {
        public string crossingId = "X0";
        public Transform spawnA;
        public Transform spawnB;
        public TrafficLight controllingTrafficLight;
    }

    [Header("Crossings")]
    public ZebraCrossing[] crossings;

    [Header("Optional Random Crossing Generation")]
    public bool useRandomGeneratedCrossings = false;
    public AIRoadGraph roadGraph;
    public int generatedCrossingCount = 18;
    public float crossingHalfWidth = 4.5f;
    public float crossingPointHeight = 0.05f;

    [Tooltip("Node the generated crossings start from. -1 picks a random start; set a node index to author a repeatable layout.")]
    public int generatedCrossingStartNodeIndex = -1;

    [Header("Pedestrians")]
    public GameObject pedestrianPrefab;
    // The imported pedestrian is a multi-part model (about ten renderers each),
    // so the crowd budget is deliberately modest: only crossings near the bus
    // are populated, and the policy clamps this value. 48 fills the crossings
    // around the player without paying for people a few pixels tall at the far
    // end of the street.
    public int maxActivePedestrians = CrowdPerformancePolicy.MaxCrossingPedestrians;
    public float spawnCheckInterval = 0.5f;
    public float walkSpeed = 1.4f;
    public bool logSpawnEvents = false;

    [Header("Density")]
    public float baselineSpawnChance = 0.85f;
    public float rushMultiplier = 2.0f;

    readonly Stack<SimplePedestrian> spare = new Stack<SimplePedestrian>();
    readonly List<SimplePedestrian> active = new List<SimplePedestrian>();
    readonly Dictionary<int, int> activeByCrossing = new Dictionary<int, int>();
    ZebraCrossing[] runtimeCrossings;
    BusController playerBus;
    float timer;
    bool loggedCrossings;

    public IReadOnlyList<SimplePedestrian> GetActivePedestrians() => active;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("[PedestrianSpawner] Duplicate spawner detected. Destroying extra instance.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        if (Instance != this)
            return;

        EnsurePedestrianPrefab();
        EnsureRuntimeCrossings();
        // The quality tier is the budget, not just the ceiling: a Low preset or a
        // phone should see a thinner pavement, and the bodies are instantiated up
        // front, so an oversized crowd is paid for at load time as well.
        int tierBudget = CrowdPerformancePolicy.BudgetForQuality(
            QualitySettings.GetQualityLevel(), Application.isMobilePlatform,
            CrowdPerformancePolicy.MaxCrossingPedestrians);
        maxActivePedestrians = CrowdPerformancePolicy.ClampWalkerBudget(
            Mathf.Min(maxActivePedestrians <= 0 ? tierBudget : maxActivePedestrians, tierBudget),
            CrowdPerformancePolicy.MaxCrossingPedestrians);
        for (int i = 0; i < maxActivePedestrians; i++)
        {
            var go = Instantiate(pedestrianPrefab, transform);
            // Pedestrians are too small for their shadows to read as anything but
            // extra draw calls; the street keeps the building shadows.
            foreach (var renderer in go.GetComponentsInChildren<Renderer>(true))
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            var pedestrian = go.GetComponent<SimplePedestrian>() ?? go.AddComponent<SimplePedestrian>();
            go.SetActive(false); spare.Push(pedestrian);
        }
    }

    void Update()
    {
        if (Instance != this)
            return;

        EnsureRuntimeCrossings();

        timer += Time.deltaTime;
        if (timer >= spawnCheckInterval)
        {
            timer = 0f;
            TrySpawnPedestrians();
        }

        bool havePlayer = TryResolvePlayerPosition(out Vector3 player);
        for (int i = active.Count - 1; i >= 0; i--)
        {
            var p = active[i];
            if (p == null) { active.RemoveAt(i); continue; }

            float distance = havePlayer ? FlatDistance(p.transform.position, player) : 0f;
            // A walker that has drifted beyond the drawn band is decoration the
            // player cannot see. Retire it instead of letting it finish a crossing
            // that is already out of view, and hand the body back to the pool.
            if (havePlayer && distance > CrowdPerformancePolicy.WalkerRetireDistanceMeters)
            {
                p.Retire();
                p.gameObject.SetActive(false);
                spare.Push(p);
                active.RemoveAt(i);
                continue;
            }

            p.Tick(Time.deltaTime, walkSpeed, distance);
            if (p.IsDone)
            {
                p.gameObject.SetActive(false);
                spare.Push(p);
                active.RemoveAt(i);
            }
        }
    }

    void TrySpawnPedestrians()
    {
        var activeCrossings = runtimeCrossings;
        if (pedestrianPrefab == null || activeCrossings == null || activeCrossings.Length == 0) return;
        if (active.Count >= maxActivePedestrians) return;

        float density = GetTimeOfDayDensity();
        float spawnChance = Mathf.Clamp01(baselineSpawnChance * density);
        bool havePlayer = TryResolvePlayerPosition(out Vector3 player);
        float spawnRadiusSqr = CrowdPerformancePolicy.CrossingSpawnRadiusMeters * CrowdPerformancePolicy.CrossingSpawnRadiusMeters;

        for (int i = 0; i < activeCrossings.Length; i++)
        {
            var x = activeCrossings[i];
            if (x == null || x.spawnA == null || x.spawnB == null) continue;
            if (GetActiveCountForCrossing(i) > 0) continue;

            // Crossings all over the city are generated, but a walker on a
            // crossing beyond the drawn band can never be seen. Populating only
            // the nearby ones keeps the crowd budget where the player is.
            if (havePlayer && FlatDistanceSquared((x.spawnA.position + x.spawnB.position) * 0.5f, player) > spawnRadiusSqr)
                continue;

            // Only cross when road traffic is red (pedestrian green assumed).
            if (x.controllingTrafficLight != null && !x.controllingTrafficLight.IsRed())
                continue;

            if (Random.value > spawnChance)
                continue;

            bool dir = Random.value > 0.5f;
            Transform from = dir ? x.spawnA : x.spawnB;
            Transform to = dir ? x.spawnB : x.spawnA;

            if (spare.Count == 0) break;
            var ped = spare.Pop();
            ped.transform.position = from.position;
            ped.transform.rotation = Quaternion.identity;
            ped.gameObject.SetActive(true);
            IncrementCrossing(i);
            ped.Init(to, () => DecrementCrossing(i), true);
            active.Add(ped);

            if (logSpawnEvents)
                Debug.Log($"[PedestrianSpawner] Spawned pedestrian at crossing {x.crossingId} (active={active.Count}/{maxActivePedestrians}).");

            if (active.Count >= maxActivePedestrians)
                break;
        }
    }

    void EnsureRuntimeCrossings()
    {
        if (!useRandomGeneratedCrossings)
        {
            runtimeCrossings = crossings;
            return;
        }

        if (runtimeCrossings == null || runtimeCrossings.Length == 0)
            runtimeCrossings = BuildRandomCrossingsFromRoadGraph();

        if (logSpawnEvents && runtimeCrossings != null && !loggedCrossings)
        {
            Debug.Log($"[PedestrianSpawner] Runtime crossings ready: {runtimeCrossings.Length} (random={useRandomGeneratedCrossings}).");
            loggedCrossings = true;
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    void EnsurePedestrianPrefab()
    {
        if (pedestrianPrefab != null) return;

        var catalog = Resources.Load<LiveCityCatalog>("LiveCityCatalog");
        if (catalog != null)
        {
            foreach (var e in catalog.entries)
            {
                if (e != null && e.kind == LiveCityCatalog.Kind.Pedestrian && e.prefab != null)
                {
                    pedestrianPrefab = e.prefab;
                    break;
                }
            }
        }
        if (pedestrianPrefab == null)
            pedestrianPrefab = Resources.Load<GameObject>("LiveCityGenerated/ab834887507b6c0fba84796be9f2c3ea");

        if (pedestrianPrefab != null) return;

        var fallback = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        fallback.name = "Pedestrian_Fallback";
        fallback.transform.localScale = new Vector3(0.4f, 0.9f, 0.4f);
        if (fallback.TryGetComponent<Collider>(out var col))
            Destroy(col);
        fallback.SetActive(false);
        pedestrianPrefab = fallback;

        if (logSpawnEvents)
            Debug.Log("[PedestrianSpawner] Using fallback capsule pedestrian prefab.");
    }

    ZebraCrossing[] BuildRandomCrossingsFromRoadGraph()
    {
        var graph = roadGraph != null && !roadGraph.privateLaneCircuit
            ? roadGraph
            : AIRoadGraph.FindCityGraph();
        if (graph == null || graph.NodeCount < 2)
        {
            Debug.LogWarning("[PedestrianSpawner] Cannot generate crossings because AIRoadGraph is missing or too small.");
            return crossings;
        }

        // The walk is random by default; pinning the start node keeps authored
        // layouts (and tests) repeatable without removing the variety.
        int[] sequence = graph.BuildRandomNodeSequence(Mathf.Max(2, generatedCrossingCount + 1), generatedCrossingStartNodeIndex);
        if (sequence == null || sequence.Length < 2)
        {
            Debug.LogWarning("[PedestrianSpawner] Failed to generate random crossings from AIRoadGraph.");
            return crossings;
        }

        Transform generatedRoot = GetOrCreateGeneratedRoot();
        ClearGeneratedChildren(generatedRoot);

        int count = Mathf.Min(generatedCrossingCount, sequence.Length - 1);
        var generated = new List<ZebraCrossing>(count);

        for (int i = 0; i < count; i++)
        {
            int fromIndex = sequence[i];
            int toIndex = sequence[i + 1];
            if (!graph.IsValidNode(fromIndex) || !graph.IsValidNode(toIndex))
                continue;

            Vector3 from = graph.GetNodePosition(fromIndex);
            Vector3 to = graph.GetNodePosition(toIndex);
            Vector3 roadDir = to - from;
            roadDir.y = 0f;
            if (roadDir.sqrMagnitude < 0.001f)
                continue;

            Vector3 center = Vector3.Lerp(from, to, 0.5f);
            Vector3 side = Vector3.Cross(Vector3.up, roadDir.normalized);
            float laneWidth = Mathf.Max(2.5f, graph.nodes[fromIndex].laneWidth);
            float halfWidth = Mathf.Max(crossingHalfWidth, laneWidth * Mathf.Max(1, graph.nodes[fromIndex].laneCount) * 0.5f + 1f);

            var crossingRoot = new GameObject($"GeneratedCrossing_{i:00}").transform;
            crossingRoot.SetParent(generatedRoot, false);

            var spawnA = new GameObject("SpawnA").transform;
            spawnA.SetParent(crossingRoot, false);
            spawnA.position = center + side * halfWidth + Vector3.up * crossingPointHeight;

            var spawnB = new GameObject("SpawnB").transform;
            spawnB.SetParent(crossingRoot, false);
            spawnB.position = center - side * halfWidth + Vector3.up * crossingPointHeight;

            generated.Add(new ZebraCrossing
            {
                crossingId = $"RGX_{fromIndex}_{toIndex}",
                spawnA = spawnA,
                spawnB = spawnB,
                controllingTrafficLight = graph.nodes[toIndex].trafficLight != null
                    ? graph.nodes[toIndex].trafficLight
                    : graph.nodes[fromIndex].trafficLight
            });
        }

        return generated.Count > 0 ? generated.ToArray() : crossings;
    }

    Transform GetOrCreateGeneratedRoot()
    {
        const string rootName = "GeneratedCrossings";
        var existing = transform.Find(rootName);
        if (existing != null)
            return existing;

        var root = new GameObject(rootName).transform;
        root.SetParent(transform, false);
        return root;
    }

    void ClearGeneratedChildren(Transform root)
    {
        for (int i = root.childCount - 1; i >= 0; i--)
            Destroy(root.GetChild(i).gameObject);
    }

    bool TryResolvePlayerPosition(out Vector3 position)
    {
        if (playerBus == null)
            playerBus = MissionManager.Instance != null
                ? MissionManager.Instance.busController
                : FindAnyObjectByType<BusController>();
        if (playerBus == null)
        {
            position = default;
            return false;
        }

        position = playerBus.transform.position;
        return true;
    }

    static float FlatDistance(Vector3 a, Vector3 b) => Mathf.Sqrt(FlatDistanceSquared(a, b));

    static float FlatDistanceSquared(Vector3 a, Vector3 b)
    {
        a.y = b.y = 0f;
        return (a - b).sqrMagnitude;
    }

    float GetTimeOfDayDensity()
    {
        float now = ScheduleManager.Instance != null ? ScheduleManager.Instance.currentTimeMinutes : 12f * 60f;
        return RealismRules.DailyDensity(now, peak: rushMultiplier);
    }

    int GetActiveCountForCrossing(int index)
    {
        return activeByCrossing.TryGetValue(index, out int count) ? count : 0;
    }

    void IncrementCrossing(int index)
    {
        activeByCrossing[index] = GetActiveCountForCrossing(index) + 1;
    }

    void DecrementCrossing(int index)
    {
        int count = Mathf.Max(0, GetActiveCountForCrossing(index) - 1);
        if (count == 0)
        {
            activeByCrossing.Remove(index);
            return;
        }

        activeByCrossing[index] = count;
    }
}

public class SimplePedestrian : MonoBehaviour
{
    Transform target;
    System.Action onFinished;
    bool released;
    float animationTimer;
    public bool IsDone { get; private set; }
    public bool IsInsideCrossingZone { get; private set; }

    public void Init(Transform destination, System.Action finished = null, bool insideCrossingZone = false)
    {
        target = destination;
        onFinished = finished;
        released = false;
        animationTimer = 0f;
        IsDone = false;
        IsInsideCrossingZone = insideCrossingZone;
    }

    /// <summary>Ends the walk early (for example when the walker leaves the drawn band) and returns it to the pool.</summary>
    public void Retire() => Finish();

    public void Tick(float dt, float speed, float distanceMeters)
    {
        if (target == null) { Finish(); return; }
        Vector3 to = target.position - transform.position;
        to.y = 0f;
        float dist = to.magnitude;
        if (dist < 0.25f) { Finish(); return; }
        Vector3 dir = to / Mathf.Max(0.001f, dist);
        // Position advances every frame so a crossing takes the same time at every
        // animation tier. Only the decorative facing and sway are thinned, and
        // past the cutoff the pose is frozen entirely.
        transform.position += dir * speed * dt;

        if (!CrowdPerformancePolicy.AnimatesAt(distanceMeters))
            return;

        float interval = CrowdPerformancePolicy.AnimationInterval(distanceMeters);
        if (interval > 0f)
        {
            animationTimer += dt;
            if (animationTimer < interval)
                return;
            dt = animationTimer;
            animationTimer = 0f;
        }
        else
        {
            animationTimer = 0f;
        }

        float sway = CrowdPerformancePolicy.UsesFullAnimation(distanceMeters)
            ? Mathf.Sin(Time.time * 7f) * 1.5f
            : 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(dir, Vector3.up) * Quaternion.Euler(0, 0, sway),
            Mathf.Clamp01(dt * 8f));
    }

    void Finish()
    {
        IsDone = true;
        IsInsideCrossingZone = false;
        Release();
    }

    void OnDestroy()
    {
        Release();
    }

    void Release()
    {
        if (released) return;
        released = true;
        onFinished?.Invoke();
    }
}
