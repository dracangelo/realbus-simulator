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
    public int generatedCrossingCount = 6;
    public float crossingHalfWidth = 4.5f;
    public float crossingPointHeight = 0.05f;

    [Header("Pedestrians")]
    public GameObject pedestrianPrefab;
    public int maxActivePedestrians = 40;
    public float spawnCheckInterval = 1.25f;
    public float walkSpeed = 1.4f;
    public bool logSpawnEvents = true;

    [Header("Density")]
    public float baselineSpawnChance = 0.2f;
    public float rushMultiplier = 2.0f;

    readonly List<SimplePedestrian> active = new List<SimplePedestrian>();
    readonly Dictionary<int, int> activeByCrossing = new Dictionary<int, int>();
    ZebraCrossing[] runtimeCrossings;
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

        for (int i = active.Count - 1; i >= 0; i--)
        {
            var p = active[i];
            if (p == null) { active.RemoveAt(i); continue; }
            p.Tick(Time.deltaTime, walkSpeed);
            if (p.IsDone)
            {
                Destroy(p.gameObject);
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

        for (int i = 0; i < activeCrossings.Length; i++)
        {
            var x = activeCrossings[i];
            if (x == null || x.spawnA == null || x.spawnB == null) continue;
            if (GetActiveCountForCrossing(i) > 0) continue;

            // Only cross when road traffic is red (pedestrian green assumed).
            if (x.controllingTrafficLight != null && !x.controllingTrafficLight.IsRed())
                continue;

            if (Random.value > spawnChance)
                continue;

            bool dir = Random.value > 0.5f;
            Transform from = dir ? x.spawnA : x.spawnB;
            Transform to = dir ? x.spawnB : x.spawnA;

            GameObject go = Instantiate(pedestrianPrefab, from.position, Quaternion.identity, transform);
            var ped = go.GetComponent<SimplePedestrian>();
            if (ped == null) ped = go.AddComponent<SimplePedestrian>();
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
        var graph = roadGraph != null ? roadGraph : FindFirstObjectByType<AIRoadGraph>();
        if (graph == null || graph.NodeCount < 2)
        {
            Debug.LogWarning("[PedestrianSpawner] Cannot generate crossings because AIRoadGraph is missing or too small.");
            return crossings;
        }

        int[] sequence = graph.BuildRandomNodeSequence(Mathf.Max(2, generatedCrossingCount + 1));
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

    float GetTimeOfDayDensity()
    {
        float now = ScheduleManager.Instance != null ? ScheduleManager.Instance.currentTimeMinutes : 12f * 60f;
        float morning = Gaussian(now, 7.5f * 60f, 90f);
        float evening = Gaussian(now, 17.5f * 60f, 90f);
        float rushSignal = Mathf.Clamp01(Mathf.Max(morning, evening));
        return Mathf.Lerp(1f, rushMultiplier, rushSignal);
    }

    static float Gaussian(float x, float mean, float sigma)
    {
        if (sigma <= 0.001f) return 0f;
        float d = (x - mean) / sigma;
        return Mathf.Exp(-0.5f * d * d);
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
    public bool IsDone { get; private set; }
    public bool IsInsideCrossingZone { get; private set; }

    public void Init(Transform destination, System.Action finished = null, bool insideCrossingZone = false)
    {
        target = destination;
        onFinished = finished;
        released = false;
        IsDone = false;
        IsInsideCrossingZone = insideCrossingZone;
    }

    public void Tick(float dt, float speed)
    {
        if (target == null) { Finish(); return; }
        Vector3 to = target.position - transform.position;
        to.y = 0f;
        float dist = to.magnitude;
        if (dist < 0.25f) { Finish(); return; }
        Vector3 dir = to / Mathf.Max(0.001f, dist);
        transform.position += dir * speed * dt;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), dt * 8f);
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
