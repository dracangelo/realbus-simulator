using UnityEngine;
using System.Collections;

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
    public string generatedStopResourceFolder = "BusStopsGenerated";

    [Header("Placement")]
    public float lateralOffset = 3f;
    [Range(-1f, 1f)] public float stopSide = -1f;
    public float forwardOffset = 1.5f;
    public float stopModelVerticalOffset = 0f;
    public bool alignToRouteDirection = true;
    public Vector2 randomYawRange = new Vector2(-10f, 10f);
    public Vector2 randomScaleRange = new Vector2(0.8f, 1.2f);
    public float triggerApproachDistance = 50f;
    public float triggerDockingRadius = 3f;
    public bool spawnOnlyFirstStopAtMissionStart = false;
    public bool logSpawns = true;

    void Start()
    {
        StartCoroutine(SpawnStopPropsWhenReady());
    }

    IEnumerator SpawnStopPropsWhenReady()
    {
        while ((MissionManager.Instance == null || MissionManager.Instance.currentRoute == null) || GPSManager.Instance == null)
            yield return null;

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

        GameObject[] generatedStopPrefabs = string.IsNullOrWhiteSpace(generatedStopResourceFolder)
            ? System.Array.Empty<GameObject>()
            : Resources.LoadAll<GameObject>(generatedStopResourceFolder);
        if (generatedStopPrefabs.Length == 0 && (stopPropResourcePaths == null || stopPropResourcePaths.Length == 0))
        {
            if (logSpawns)
                Debug.LogWarning("[StopPropSpawner] No stop prop resource paths configured.");
            return;
        }

        ClearSpawnedProps();

        int stopCount = spawnOnlyFirstStopAtMissionStart ? Mathf.Min(1, route.stops.Length) : route.stops.Length;
        for (int i = 0; i < stopCount; i++)
        {
            var stop = route.stops[i];
            Vector3 stopPos = GPSManager.Instance.GpsToWorld(stop.latitude, stop.longitude);
            stopPos.y = 0f;

            string propPath = string.Empty;
            GameObject prefab;
            if (generatedStopPrefabs.Length > 0)
            {
                prefab = generatedStopPrefabs[Random.Range(0, generatedStopPrefabs.Length)];
                propPath = generatedStopResourceFolder + "/" + prefab.name;
            }
            else
            {
                propPath = stopPropResourcePaths[Random.Range(0, stopPropResourcePaths.Length)];
                prefab = Resources.Load<GameObject>(propPath);
            }
            if (prefab == null)
            {
                if (logSpawns)
                    Debug.LogWarning($"[StopPropSpawner] Missing resource: {propPath}");
                continue;
            }

            float yaw = alignToRouteDirection
                ? GetRouteYaw(route, i)
                : 0f;
            Quaternion routeRotation = Quaternion.Euler(0f, yaw, 0f);
            Vector3 offset = routeRotation * new Vector3(lateralOffset * Mathf.Sign(stopSide), 0f, forwardOffset);
            Vector3 worldPos = stopPos + offset;

            var stopRoot = new GameObject($"RuntimeStop_{i:00}_{SanitizeName(stop.stopName)}");
            stopRoot.transform.SetParent(transform, false);
            stopRoot.transform.SetPositionAndRotation(worldPos, routeRotation);

            var trigger = stopRoot.AddComponent<StopTrigger>();
            trigger.stopName = stop.stopName;
            trigger.stopIndex = i;
            trigger.approachDistance = triggerApproachDistance;

            var docking = stopRoot.AddComponent<DockingZone>();
            docking.stopName = stop.stopName;
            docking.stopIndex = i;
            docking.dockingRadius = triggerDockingRadius;

            var model = Instantiate(prefab, stopRoot.transform);
            ImportedVehicleVisualRepair.DisableEmbeddedCameras(model.transform);
            model.transform.localPosition = Vector3.up * stopModelVerticalOffset;
            model.transform.localRotation = Quaternion.Euler(0f, Random.Range(randomYawRange.x, randomYawRange.y), 0f);
            float scale = Random.Range(randomScaleRange.x, randomScaleRange.y);
            model.transform.localScale = Vector3.Scale(model.transform.localScale, Vector3.one * scale);

            if (logSpawns)
                Debug.Log($"[StopPropSpawner] Spawned {propPath} at stop '{stop.stopName}'.");
        }
    }

    void ClearSpawnedProps()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            Destroy(transform.GetChild(i).gameObject);
    }

    static string SanitizeName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return "Stop";

        return value.Replace(" ", "_").Replace("/", "_");
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
