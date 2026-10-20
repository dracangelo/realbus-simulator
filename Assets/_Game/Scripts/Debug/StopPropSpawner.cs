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
    // Stops are deliberately oversized: the imported shelters/benches read as
    // small props at street distance, and clear, large stop markers matter more
    // to the player than exact real-world scale. A shelter that no longer clears
    // the pavement falls back to the standalone poster totem (see SpawnStopProps).
    public Vector2 randomScaleRange = new Vector2(1.5f, 2.1f);
    public float triggerApproachDistance = 50f;
    public float triggerDockingRadius = 4f;
    public float triggerKerbTolerance = 0.8f;
    public bool spawnOnlyFirstStopAtMissionStart = false;
    public bool logSpawns = true;

    [Header("Imported model fallback")]
    [Tooltip("Applied only to untextured near-white materials on imported stop props.")]
    public Color importedPropFallbackColor = ImportedMaterialPalette.DefaultPropColor;

    void Start()
    {
        StartCoroutine(SpawnStopPropsWhenReady());
    }

    IEnumerator SpawnStopPropsWhenReady()
    {
        while ((MissionManager.Instance == null || MissionManager.Instance.currentRoute == null) || GPSManager.Instance == null)
            yield return null;
        while (OSMRoadMeshBuilder.Instance == null || !OSMRoadMeshBuilder.Instance.roadsBuilt)
            yield return null;

        SpawnStopProps();
    }

    public void SpawnStopProps()
    {
        var mission = FindAnyObjectByType<MissionManager>();
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

        var roads = OSMRoadMeshBuilder.Instance?.SurfaceIndex;
        if (roads == null)
        {
            Debug.LogWarning("[StopPropSpawner] Road placement data is not ready. Waiting prevents props being placed in traffic lanes.");
            return;
        }
        ClearSpawnedProps();
        ImportedMaterialPalette.RegisterImportedRoot(transform, importedPropFallbackColor);
        bool leftTraffic = RoadSpawnResolver.ResolveCountry()?.DrivesOnLeft == true;

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
            Vector3 heading = RoadSpawnResolver.RouteHeading(route, stopPos, routeRotation * Vector3.forward);
            if (!roads.TryRoadside(stopPos, heading, leftTraffic, out Vector3 worldPos, out routeRotation))
            {
                Debug.LogWarning($"[StopPropSpawner] No clear kerb near '{stop.stopName}'; skipping its prop rather than blocking a lane.");
                continue;
            }
            worldPos.y = OSMRoadMeshBuilder.Instance.roadYOffset;
            Vector3 outward = routeRotation * Vector3.right * (leftTraffic ? -1f : 1f);
            // The bus's door target stays just inside the road edge so the
            // chassis never has to climb the physical kerb to finish docking.
            worldPos -= outward * 1.2f;

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
            docking.kerbToleranceMeters = triggerKerbTolerance;
            docking.roadsideDockingSide = leftTraffic ? -1 : 1;

            var model = Instantiate(prefab, stopRoot.transform);
            ImportedVehicleVisualRepair.DisableEmbeddedCameras(model.transform);
            // Street furniture sits in a building's shadow already; adding its own
            // small shadow only multiplies the shadow pass across every stop.
            foreach (var renderer in model.GetComponentsInChildren<Renderer>(true))
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            model.AddComponent<DistanceVisibility>().visibleDistance = 420f;
            // Shelters, benches and signs are imported FBX models whose source
            // textures are missing, so they import flat white. Recolour the
            // untextured slots immediately; the spawner root is registered once
            // (see Start) so a late sweep re-checks every stop prop.
            ImportedMaterialPalette.ApplyFallback(model, importedPropFallbackColor);
            RepairShelterGlass(model);
            model.transform.localPosition = Vector3.up * stopModelVerticalOffset;
            model.transform.localRotation = Quaternion.Euler(0f, Random.Range(randomYawRange.x, randomYawRange.y), 0f);
            float scale = Random.Range(randomScaleRange.x, randomScaleRange.y);
            model.transform.localScale = Vector3.Scale(model.transform.localScale, Vector3.one * scale);

            // Keep the docking anchor at the kerb; move the actual shelter
            // farther onto the pavement until its entire footprint is clear.
            bool clear = false;
            for (int attempt = 0; attempt < 16; attempt++)
            {
                var renderers = model.GetComponentsInChildren<Renderer>();
                if (renderers.Length == 0) break;
                Bounds bounds = renderers[0].bounds;
                foreach (var renderer in renderers) bounds.Encapsulate(renderer.bounds);
                Physics.SyncTransforms();
                foreach (var collider in model.GetComponentsInChildren<Collider>())
                    if (collider.enabled && !collider.isTrigger) bounds.Encapsulate(collider.bounds);
                var footprint = new[] { new Vector3(bounds.min.x, 0, bounds.min.z), new Vector3(bounds.max.x, 0, bounds.min.z),
                    new Vector3(bounds.max.x, 0, bounds.max.z), new Vector3(bounds.min.x, 0, bounds.max.z) };
                if (!roads.OverlapsFootprint(footprint, .8f)) { clear = true; break; }
                model.transform.position += outward;
            }
            if (clear)
            {
                BusStopPosterBuilder.DecorateBusStop(stopRoot, model, i, leftTraffic);
            }
            else
            {
                Destroy(model);
                BusStopPosterBuilder.SpawnStandalonePosterTotem(stopRoot, i, leftTraffic);
                Debug.LogWarning($"[StopPropSpawner] Shelter cannot fit safely at '{stop.stopName}'; retaining kerb docking anchor with advertising totem.");
            }

            if (logSpawns)
                Debug.Log($"[StopPropSpawner] Spawned {propPath} with posters at stop '{stop.stopName}'.");
        }
    }

    /// <summary>
    /// Shelter glazing imports as an opaque white panel. That flat white slab is one
    /// of the most obvious "unfinished asset" tells at a bus stop, so every panel
    /// whose material says glass/window is switched to alpha-blended glass on a
    /// per-renderer copy (the imported material asset itself is left untouched).
    /// </summary>
    static void RepairShelterGlass(GameObject model)
    {
        foreach (var renderer in model.GetComponentsInChildren<Renderer>(true))
        {
            Material[] materials = renderer.sharedMaterials;
            bool changed = false;
            for (int i = 0; i < materials.Length; i++)
            {
                Material source = materials[i];
                if (source == null) continue;
                string name = source.name.ToLowerInvariant();
                bool glass = name.Contains("glass") || name.Contains("kaca") ||
                             name.Contains("window") || name.Contains("transparent");
                if (!glass) continue;

                var material = new Material(source) { name = source.name + " (Stop Glass)" };
                ImportedVehicleVisualRepair.ConfigureGlass(material);
                Color color = material.HasProperty("_Color") ? material.GetColor("_Color") : Color.white;
                color.a = .32f;
                material.color = color;
                if (material.HasProperty("_Color")) material.SetColor("_Color", color);
                if (material.HasProperty("_Glossiness")) material.SetFloat("_Glossiness", .82f);
                if (material.HasProperty("_Smoothness")) material.SetFloat("_Smoothness", .82f);
                materials[i] = material;
                changed = true;
            }
            if (changed) renderer.sharedMaterials = materials;
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
