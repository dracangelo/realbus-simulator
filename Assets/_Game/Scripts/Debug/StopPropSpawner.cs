using UnityEngine;

public class StopPropSpawner : MonoBehaviour
{
    [Header("Resources (relative to Assets/_Game/Resources)")]
    public string[] stopPropResourcePaths =
    {
        "BussimAssets/stops/Bench",
        "BussimAssets/stops/Bus_Stand",
        "BussimAssets/stops/StopSign",
        "BussimAssets/stops/bus_stopEU"
    };

    [Header("Placement")]
    public float lateralOffset = 3f;
    public float forwardOffset = 1.5f;
    public bool alignToRouteDirection = true;
    public Vector2 randomYawRange = new Vector2(-10f, 10f);
    public Vector2 randomScaleRange = new Vector2(0.8f, 1.2f);
    public bool logSpawns = true;

    void Start()
    {
        SpawnStopProps();
    }

    public void SpawnStopProps()
    {
        var mission = FindFirstObjectByType<MissionManager>();
        var route = mission != null ? mission.currentRoute : null;
        if (route == null || route.stops == null || route.stops.Length == 0)
        {
            if (logSpawns)
                Debug.LogWarning("[StopPropSpawner] No route stops found. Skipping stop props.");
            return;
        }

        if (GPSManager.Instance == null)
        {
            if (logSpawns)
                Debug.LogWarning("[StopPropSpawner] GPSManager missing. Skipping stop props.");
            return;
        }

        if (stopPropResourcePaths == null || stopPropResourcePaths.Length == 0)
        {
            if (logSpawns)
                Debug.LogWarning("[StopPropSpawner] No stop prop resource paths configured.");
            return;
        }

        for (int i = 0; i < route.stops.Length; i++)
        {
            var stop = route.stops[i];
            Vector3 stopPos = GPSManager.Instance.GpsToWorld(stop.latitude, stop.longitude);
            stopPos.y = 0f;

            var propPath = stopPropResourcePaths[Random.Range(0, stopPropResourcePaths.Length)];
            var prefab = Resources.Load<GameObject>(propPath);
            if (prefab == null)
            {
                if (logSpawns)
                    Debug.LogWarning($"[StopPropSpawner] Missing resource: {propPath}");
                continue;
            }

            Vector3 offset = new Vector3(Random.Range(-lateralOffset, lateralOffset), 0f, forwardOffset);
            Vector3 worldPos = stopPos + offset;
            float yaw = alignToRouteDirection
                ? GetRouteYaw(route, i)
                : 0f;
            yaw += Random.Range(randomYawRange.x, randomYawRange.y);

            var go = Instantiate(prefab, worldPos, Quaternion.Euler(0f, yaw, 0f), transform);
            float scale = Random.Range(randomScaleRange.x, randomScaleRange.y);
            go.transform.localScale = Vector3.Scale(go.transform.localScale, Vector3.one * scale);

            if (logSpawns)
                Debug.Log($"[StopPropSpawner] Spawned {propPath} at stop '{stop.stopName}'.");
        }
    }

    float GetRouteYaw(BusRoute route, int index)
    {
        int a = Mathf.Clamp(index, 0, route.stops.Length - 1);
        int b = Mathf.Clamp(index + 1, 0, route.stops.Length - 1);
        if (a == b && a > 0) a -= 1;

        var pA = route.stops[a];
        var pB = route.stops[b];
        Vector3 worldA = GPSManager.Instance.GpsToWorld(pA.latitude, pA.longitude);
        Vector3 worldB = GPSManager.Instance.GpsToWorld(pB.latitude, pB.longitude);
        Vector3 dir = worldB - worldA;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.001f)
            return 0f;

        return Quaternion.LookRotation(dir, Vector3.up).eulerAngles.y;
    }
}
