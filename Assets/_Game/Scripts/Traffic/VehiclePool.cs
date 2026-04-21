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
    public int poolSizePerScene = 50;
    public VehiclePrefabEntry[] prefabs;
    public bool logSpawnEvents = true;
    public bool allowAutoResolveRoadGraph = true;

    [Header("Debug")]
    public int debugForceActiveCount = 6;
    public bool logBusDistance = true;
    public bool logBusDistanceOnce = true;
    public float logBusDistanceIntervalSeconds = 5f;
    public bool logFirstActivationOnly = true;
    public float roadGraphRetryIntervalSeconds = 1.5f;
    public float maxAutoBindDistanceMeters = 1200f;

    [Header("Tier Split (distance from player bus)")]
    public float fullAiRadiusMeters = 200f;
    public float splineRadiusMeters = 400f;
    public float transitionHysteresisMeters = 25f;
    [Range(15, 20)] public int maxFullAiVehicles = 18;
    public float tierUpdateIntervalSeconds = 0.2f;

    [Header("Density")]
    public float minActiveFraction = 0.25f;
    public float maxActiveFraction = 0.9f;
    public float updateDensityEverySeconds = 4f;

    [Header("Rush Hour")]
    public float morningPeakMinutes = 7.5f * 60f;
    public float eveningPeakMinutes = 17.5f * 60f;
    public float peakWidthMinutes = 80f;
    public float rushMultiplier = 3.5f;

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

    void Start()
    {
        if (IsTrafficGraphDisabledByConfig())
        {
            if (!loggedTrafficDisabled)
            {
                Debug.Log("[VehiclePool] Traffic disabled for this run (no aligned AIRoadGraph configured).");
                loggedTrafficDisabled = true;
            }
            return;
        }

        TryResolveRoadGraph();
        WarmPool();
        ApplyDensityNow();

        if (debugForceActiveCount > 0)
            SetForcedActiveCount(debugForceActiveCount);
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

        if (playerBus == null)
        {
            var bus = FindFirstObjectByType<BusController>();
            if (bus != null) playerBus = bus.transform;
        }

        if (densityTick >= updateDensityEverySeconds)
        {
            densityTick = 0f;
            ApplyDensityNow();
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

        int created = 0;
        for (int i = 0; i < poolSizePerScene; i++)
        {
            var picked = PickWeightedPrefab();
            if (picked == null || picked.prefab == null) continue;

            GameObject go = Instantiate(picked.prefab, transform);
            var ai = go.GetComponent<AIVehicleController>();
            if (ai == null) ai = go.AddComponent<AIVehicleController>();
            ai.vehicleType = picked.type;

            var spline = go.GetComponent<SplineVehicle>();
            if (spline == null) spline = go.AddComponent<SplineVehicle>();

            var rb = go.GetComponent<Rigidbody>();
            if (rb == null) rb = go.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;

            int startNode = Random.Range(0, roadGraph.NodeCount);
            int laneCount = 1;
            if (roadGraph.nodes != null && startNode >= 0 && startNode < roadGraph.nodes.Length && roadGraph.nodes[startNode] != null)
                laneCount = Mathf.Max(1, roadGraph.nodes[startNode].laneCount);
            int lane = Random.Range(0, laneCount);
            ai.Init(roadGraph, startNode, lane, ai.vehicleType);
            spline.Init(roadGraph, startNode, ai.targetNodeIndex, lane, 0f);

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

        if (logSpawnEvents)
            Debug.Log($"[VehiclePool] Warmed pool: {created}/{poolSizePerScene} vehicles created (roadGraphNodes={roadGraph.NodeCount}).");
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

        var graphs = FindObjectsByType<AIRoadGraph>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
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
        }
        else
        {
            float mult = GetTrafficDensityMultiplier();
            float normalized = Mathf.InverseLerp(1f, rushMultiplier, mult);
            float targetFraction = Mathf.Lerp(minActiveFraction, maxActiveFraction, normalized);
            targetActive = Mathf.Clamp(Mathf.RoundToInt(targetFraction * pooled.Count), 0, pooled.Count);
        }

        for (int i = 0; i < pooled.Count; i++)
            pooled[i].enabledByDensity = i < targetActive;
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
        int clampedCap = Mathf.Clamp(maxFullAiVehicles, 15, 20);

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
            bool keepFull = pv.tier == SimulationTier.FullAI && dist <= (fullAiRadiusMeters + transitionHysteresisMeters);
            if (keepFull && fullUsed < clampedCap)
            {
                fullUsed++;
                ApplyTierIfNeeded(pv, SimulationTier.FullAI);
            }
        }

        for (int i = 0; i < pooled.Count; i++)
        {
            var pv = pooled[i];
            if (!pv.enabledByDensity)
            {
                ApplyTierIfNeeded(pv, SimulationTier.Dormant);
                continue;
            }

            float dist = GetDistanceToBus(pv.go.transform.position);
            if (dist <= fullAiRadiusMeters && fullUsed < clampedCap)
            {
                fullUsed++;
                ApplyTierIfNeeded(pv, SimulationTier.FullAI);
            }
            else if (dist <= splineRadiusMeters + transitionHysteresisMeters)
            {
                ApplyTierIfNeeded(pv, SimulationTier.Spline);
            }
            else
            {
                ApplyTierIfNeeded(pv, SimulationTier.Dormant);
            }
        }
    }

    float GetDistanceToBus(Vector3 pos)
    {
        if (playerBus == null) return 9999f;
        return Vector3.Distance(playerBus.position, pos);
    }

    void ApplyTierIfNeeded(PooledVehicle pv, SimulationTier desired)
    {
        if (pv == null || pv.go == null) return;
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

        if (desired == SimulationTier.FullAI)
        {
            // Seamless promotion: preserve pose + spline velocity estimate.
            Vector3 vel = pv.spline != null ? pv.spline.GetVelocityWorld() : Vector3.zero;
            int currentNode = roadGraph.GetNearestNodeIndex(pv.go.transform.position);
            if (!roadGraph.IsValidNode(currentNode))
                currentNode = pv.full != null ? pv.full.currentNodeIndex : 0;
            int targetNode = roadGraph.GetRandomNextNode(currentNode);
            if (!roadGraph.IsValidNode(targetNode) && pv.full != null)
                targetNode = pv.full.targetNodeIndex;

            if (pv.full != null)
            {
                pv.full.SetRuntimeState(roadGraph, currentNode, targetNode, pv.laneIndex, pv.type, vel.magnitude * 3.6f);
                pv.full.enabled = true;
                pv.full.SetVelocityWorld(vel);
            }
            if (pv.spline != null)
                pv.spline.enabled = false;
            if (pv.rb != null)
                pv.rb.isKinematic = false;
        }
        else if (desired == SimulationTier.Spline)
        {
            // Seamless demotion: preserve pose + current rigidbody velocity.
            Vector3 vel = pv.full != null ? pv.full.GetVelocityWorld() : Vector3.zero;
            int currentNode = roadGraph.GetNearestNodeIndex(pv.go.transform.position);
            if (!roadGraph.IsValidNode(currentNode))
                currentNode = pv.spline != null ? pv.spline.currentNodeIndex : 0;
            int targetNode = roadGraph.GetRandomNextNode(currentNode);
            if (pv.spline != null)
            {
                pv.spline.Init(roadGraph, currentNode, targetNode, pv.laneIndex, vel.magnitude * 3.6f);
                pv.spline.enabled = true;
                pv.spline.SetVelocityWorld(vel);
            }
            if (pv.full != null)
                pv.full.enabled = false;
            if (pv.rb != null)
            {
                pv.rb.linearVelocity = Vector3.zero;
                pv.rb.angularVelocity = Vector3.zero;
                pv.rb.isKinematic = true;
            }
        }
        else
        {
            if (pv.full != null) pv.full.enabled = false;
            if (pv.spline != null) pv.spline.enabled = false;
            if (pv.rb != null)
            {
                pv.rb.linearVelocity = Vector3.zero;
                pv.rb.angularVelocity = Vector3.zero;
                pv.rb.isKinematic = true;
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
        if (rb != null) rb.isKinematic = tier != SimulationTier.FullAI;
        if (tier == SimulationTier.Dormant)
            go.SetActive(false);
    }

    public float GetTrafficDensityMultiplier()
    {
        float now = ScheduleManager.Instance != null
            ? ScheduleManager.Instance.currentTimeMinutes
            : 12f * 60f;

        float morning = Gaussian(now, morningPeakMinutes, peakWidthMinutes);
        float evening = Gaussian(now, eveningPeakMinutes, peakWidthMinutes);
        float rushSignal = Mathf.Clamp01(Mathf.Max(morning, evening));
        return Mathf.Lerp(1f, rushMultiplier, rushSignal);
    }

    static float Gaussian(float x, float mean, float sigma)
    {
        if (sigma <= 0.001f) return 0f;
        float d = (x - mean) / sigma;
        return Mathf.Exp(-0.5f * d * d);
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
