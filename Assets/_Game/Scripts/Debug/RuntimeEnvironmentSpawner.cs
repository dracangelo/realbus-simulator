using UnityEngine;

public class RuntimeEnvironmentSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnSpec
    {
        public string resourcePath;
        public Vector3 localOffset;
        public Vector3 rotationEuler;
        public Vector3 localScale;
    }

    public bool spawnOnStart = true;
    public bool logSpawns = true;
    public Transform parentOverride;
    public float globalScale = 1f;
    public SpawnSpec[] spawnList;

    void Start()
    {
        if (!spawnOnStart) return;
        SpawnAll();
    }

    public void SpawnAll()
    {
        var anchor = ResolveAnchor();
        bool usingCustomList = spawnList != null && spawnList.Length > 0;
        var specs = usingCustomList ? spawnList : GetDefaultSpawns();
        var parent = parentOverride != null ? parentOverride : transform;
        int spawned = 0;
        int skipped = 0;

        for (int i = 0; i < specs.Length; i++)
        {
            var spec = specs[i];
            if (string.IsNullOrWhiteSpace(spec.resourcePath)) continue;

            var prefab = ResolvePrefab(spec.resourcePath);
            if (prefab == null)
            {
                skipped++;
                if (logSpawns && usingCustomList)
                    Debug.LogWarning($"[RuntimeEnvironmentSpawner] Missing resource: {spec.resourcePath}");
                continue;
            }

            var worldPos = anchor + spec.localOffset;
            var rot = Quaternion.Euler(spec.rotationEuler);
            var go = Instantiate(prefab, worldPos, rot, parent);
            ImportedVehicleVisualRepair.DisableEmbeddedCameras(go.transform);
            var scale = spec.localScale == Vector3.zero ? Vector3.one : spec.localScale;
            go.transform.localScale = Vector3.Scale(go.transform.localScale, scale * globalScale);
            spawned++;

            if (logSpawns)
                Debug.Log($"[RuntimeEnvironmentSpawner] Spawned {spec.resourcePath} at {worldPos}.");
        }

        if (logSpawns && !usingCustomList)
            Debug.Log($"[RuntimeEnvironmentSpawner] Spawned {spawned} available environment props; skipped {skipped} optional asset categories that are not installed.");
    }

    static GameObject ResolvePrefab(string resourcePath)
    {
        GameObject direct = Resources.Load<GameObject>(resourcePath);
        if (direct != null) return direct;

        string lower = resourcePath.ToLowerInvariant();
        if (lower.Contains("gasstation"))
            return First(Resources.LoadAll<GameObject>("GasStationsGenerated"));
        if (lower.Contains("stops/") || lower.Contains("bus_stand"))
            return First(Resources.LoadAll<GameObject>("BusStopsGenerated"));
        if (lower.Contains("passenger/"))
            return Resources.Load<GameObject>("BussimAssets/passenger/uploads_files_6231172_woman+3d+fbx");

        return null;
    }

    static GameObject First(GameObject[] prefabs)
    {
        return prefabs != null && prefabs.Length > 0 ? prefabs[0] : null;
    }

    Vector3 ResolveAnchor()
    {
        var bus = FindAnyObjectByType<BusController>();
        return bus != null ? bus.transform.position : Vector3.zero;
    }

    SpawnSpec[] GetDefaultSpawns()
    {
        return new[]
        {
            new SpawnSpec
            {
                resourcePath = "BussimAssets/trees/Sour wood_Spring corona",
                localOffset = new Vector3(30f, 0f, 12f),
                rotationEuler = Vector3.zero,
                localScale = Vector3.one * 0.8f
            },
            new SpawnSpec
            {
                resourcePath = "BussimAssets/stops/Bus_Stand",
                localOffset = new Vector3(8f, 0f, 6f),
                rotationEuler = new Vector3(0f, 180f, 0f),
                localScale = Vector3.one
            },
            new SpawnSpec
            {
                resourcePath = "BussimAssets/aitraffic/Van",
                localOffset = new Vector3(18f, 0f, -6f),
                rotationEuler = new Vector3(0f, 90f, 0f),
                localScale = Vector3.one
            },
            new SpawnSpec
            {
                resourcePath = "BussimAssets/aitraffic/uploads_files_4560086_FireEngine",
                localOffset = new Vector3(-22f, 0f, -8f),
                rotationEuler = new Vector3(0f, 40f, 0f),
                localScale = Vector3.one
            },
            new SpawnSpec
            {
                resourcePath = "BussimAssets/GasStation",
                localOffset = new Vector3(-40f, 0f, 25f),
                rotationEuler = new Vector3(0f, 30f, 0f),
                localScale = Vector3.one
            },
            new SpawnSpec
            {
                resourcePath = "BussimAssets/passenger/Realistic man",
                localOffset = new Vector3(6f, 0f, 8f),
                rotationEuler = new Vector3(0f, -90f, 0f),
                localScale = Vector3.one
            }
        };
    }
}
