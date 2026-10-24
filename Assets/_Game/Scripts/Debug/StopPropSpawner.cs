using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    // Runtime stops take their docking envelope from here. These match
    // DockingZone's defaults: forgiving enough for a correctly parked bus, strict
    // enough that the bus still has to reach the kerb.
    public float triggerDockingRadius = 7f;
    public float triggerKerbTolerance = 2f;
    public bool spawnOnlyFirstStopAtMissionStart = false;
    public bool logSpawns = true;

    [Header("Imported model fallback")]
    [Tooltip("Applied only to untextured near-white materials on imported stop props.")]
    public Color importedPropFallbackColor = ImportedMaterialPalette.DefaultPropColor;

    /// <summary>True once every stop prop this spawner can place has been placed.</summary>
    public bool SpawnComplete { get; private set; }

    /// <summary>
    /// True once the first, view-sorted stops are placed. The loading gate waits
    /// for this rather than the whole route: the stops behind the depot can stream
    /// in after the player has control, but the shelter in the windscreen cannot.
    /// </summary>
    public bool NearFieldComplete { get; private set; }

    [Tooltip("View-sorted stops that must exist before the loading card may lift.")]
    public int nearFieldStopsBeforeReady = 6;

    public bool ResourcesWarmed { get; private set; }
    public int SpawnedStopCount { get; private set; }

    GameObject[] cachedGeneratedStops;
    readonly Dictionary<string, GameObject> cachedPropPrefabs = new Dictionary<string, GameObject>();
    bool spawnRunning;

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

        yield return SpawnStopPropsStreamed();
    }

    /// <summary>
    /// Loads the stop packs once, behind the loading card. The spawn pass may run
    /// several frames later; without this it paid for eight poster textures and
    /// every imported shelter the first time the bus approached a stop.
    ///
    /// <see cref="Resources.LoadAll{T}"/> has no asynchronous form. The folder is a
    /// small optional pack, and this is called only while the overlay is covering
    /// the scene, so the one synchronous folder scan is acceptable.
    /// </summary>
    public void PrewarmResources()
    {
        if (ResourcesWarmed) return;
        ResourcesWarmed = true;

        cachedGeneratedStops = string.IsNullOrWhiteSpace(generatedStopResourceFolder)
            ? System.Array.Empty<GameObject>()
            : Resources.LoadAll<GameObject>(generatedStopResourceFolder);

        if (stopPropResourcePaths != null)
        {
            for (int i = 0; i < stopPropResourcePaths.Length; i++)
            {
                string path = stopPropResourcePaths[i];
                if (string.IsNullOrWhiteSpace(path) || cachedPropPrefabs.ContainsKey(path))
                    continue;
                cachedPropPrefabs[path] = Resources.Load<GameObject>(path);
            }
        }

        BusStopPosterBuilder.Prewarm();
    }

    /// <summary>Starts the streamed spawn pass, if it is not already running.</summary>
    public void SpawnStopProps()
    {
        if (!spawnRunning) StartCoroutine(SpawnStopPropsStreamed());
    }

    /// <summary>
    /// Places stops in bounded batches. The old pass created every shelter, poster
    /// totem, collider and docking trigger in one synchronous call; on a long route
    /// that froze the loading card for tens of seconds. One stop per frame keeps the
    /// card responsive while the same work completes behind it.
    /// </summary>
    IEnumerator SpawnStopPropsStreamed()
    {
        if (spawnRunning) yield break;
        spawnRunning = true;

        var mission = FindAnyObjectByType<MissionManager>();
        var route = mission != null ? mission.currentRoute : null;
        if (route == null || route.stops == null || route.stops.Length == 0)
        {
            SpawnComplete = true;
            NearFieldComplete = true;
            spawnRunning = false;
            if (logSpawns)
                Debug.LogWarning("[StopPropSpawner] No route stops found. Skipping stop props.");
            yield break;
        }

        if (GPSManager.Instance == null)
        {
            SpawnComplete = true;
            NearFieldComplete = true;
            spawnRunning = false;
            if (logSpawns)
                Debug.LogWarning("[StopPropSpawner] GPSManager missing. Skipping stop props.");
            yield break;
        }

        PrewarmResources();
        GameObject[] generatedStopPrefabs = cachedGeneratedStops ?? System.Array.Empty<GameObject>();
        if (generatedStopPrefabs.Length == 0 && (stopPropResourcePaths == null || stopPropResourcePaths.Length == 0))
        {
            SpawnComplete = true;
            NearFieldComplete = true;
            spawnRunning = false;
            if (logSpawns)
                Debug.LogWarning("[StopPropSpawner] No stop prop resource paths configured.");
            yield break;
        }

        var roads = OSMRoadMeshBuilder.Instance?.SurfaceIndex;
        if (roads == null)
        {
            Debug.LogWarning("[StopPropSpawner] Road placement data is not ready. Waiting prevents props being placed in traffic lanes.");
            spawnRunning = false;
            yield break;
        }
        ClearSpawnedProps();
        ImportedMaterialPalette.RegisterImportedRoot(transform, importedPropFallbackColor);
        bool leftTraffic = RoadSpawnResolver.ResolveCountry()?.DrivesOnLeft ?? true;

        int stopCount = spawnOnlyFirstStopAtMissionStart ? Mathf.Min(1, route.stops.Length) : route.stops.Length;
        List<int> stopOrder = ResolveStopOrder(route, stopCount);
        SpawnedStopCount = 0;
        for (int orderIndex = 0; orderIndex < stopOrder.Count; orderIndex++)
        {
            int i = stopOrder[orderIndex];
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
                if (!cachedPropPrefabs.TryGetValue(propPath, out prefab) || prefab == null)
                {
                    prefab = Resources.Load<GameObject>(propPath);
                    cachedPropPrefabs[propPath] = prefab;
                }
            }
            if (prefab == null)
            {
                if (logSpawns)
                    Debug.LogWarning($"[StopPropSpawner] Missing resource: {propPath}");
                continue;
            }

            Quaternion routeRotation = alignToRouteDirection
                ? Quaternion.Euler(0f, GetRouteYaw(route, i), 0f)
                : Quaternion.identity;
            // The kerb side is chosen from the heading, so the heading has to be the
            // direction the player actually drives. RouteHeading follows the driven
            // path; the stop list is not a safe substitute, because many imported
            // routes list stops out of path order and folding a correct tangent onto
            // that chord mirrors the shelter onto the opposite kerb for those stops.
            Vector3 heading = RoadSpawnResolver.RouteHeading(route, stopPos,
                routeRotation * Vector3.forward);
            if (!roads.TryRoadside(stopPos, heading, leftTraffic, out Vector3 worldPos, out routeRotation))
            {
                Debug.LogWarning($"[StopPropSpawner] No clear kerb near '{stop.stopName}'; skipping its prop rather than blocking a lane.");
                continue;
            }
            worldPos.y = OSMRoadMeshBuilder.Instance.roadYOffset;
            Vector3 outward = RoadSpawnResolver.KerbSide(routeRotation * Vector3.forward, leftTraffic);
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
            bool clear = MoveClearOfRoad(model, roads, outward, .8f, 1f, 16);
            if (clear)
            {
                BusStopPosterBuilder.DecorateBusStop(stopRoot, model, i, leftTraffic);

                // Decoration adds a wall poster to the shelter, so the prop that
                // finally stands on the pavement is not the one that was measured
                // above. Re-check it, then push any poster, totem or commuter that
                // ended up over the kerb back onto the pavement. The smoke check
                // measures the finished hierarchy; placement has to do the same.
                Vector3 verifiedPosition = model.transform.position;
                if (!MoveClearOfRoad(model, roads, outward, .8f, .5f, 20))
                {
                    // The added poster was the only renderer that appeared after
                    // the first clearance pass. Losing it is better than losing the
                    // shelter, and restores the already-verified footprint. Start
                    // from that verified position instead of the far end of the
                    // failed search so the shelter does not drift down the road.
                    model.transform.position = verifiedPosition;
                    foreach (Transform child in model.transform)
                    {
                        if (child.name != "Shelter_WallPoster") continue;
                        // Destroy is deferred to the end of the frame; deactivate
                        // now so the retry measures the shelter without the pane.
                        child.gameObject.SetActive(false);
                        Destroy(child.gameObject);
                    }
                    clear = MoveClearOfRoad(model, roads, outward, .8f, .5f, 16);
                }

                if (clear)
                {
                    foreach (Transform prop in stopRoot.transform)
                    {
                        if (prop == null || prop == model.transform) continue;
                        if (!MoveClearOfRoad(prop.gameObject, roads, outward, .65f, .5f, 12))
                        {
                            Debug.LogWarning($"[StopPropSpawner] Removing '{prop.name}' at '{stop.stopName}' because it cannot clear the carriageway.");
                            Destroy(prop.gameObject);
                        }
                    }
                }
            }
            if (!clear)
            {
                // Remove the rejected shelter and any decoration created for it
                // before substituting the compact marker, otherwise its totem
                // and commuters would remain beside the fallback.
                foreach (Transform prop in stopRoot.transform)
                    if (prop != null) Destroy(prop.gameObject);
                BusStopPosterBuilder.SpawnStandalonePosterTotem(stopRoot, i, leftTraffic);
                Debug.LogWarning($"[StopPropSpawner] Shelter cannot fit safely at '{stop.stopName}'; retaining kerb docking anchor with advertising totem.");
            }

            SpawnedStopCount++;
            if (!NearFieldComplete && SpawnedStopCount >= Mathf.Min(nearFieldStopsBeforeReady, stopOrder.Count))
                NearFieldComplete = true;
            if (logSpawns)
                Debug.Log($"[StopPropSpawner] Spawned {propPath} with posters at stop '{stop.stopName}'.");

            // One stop per frame: the next shelter is built while the loading
            // card keeps animating and the camera can already see the previous
            // ones in the windscreen.
            yield return null;
        }

        SpawnComplete = true;
        NearFieldComplete = true;
        spawnRunning = false;
    }

    /// <summary>
    /// Spawns the stops the player can see first. Route order is how the service is
    /// driven, not how a street is warmed: a stop on the road behind the depot must
    /// not delay the shelter directly in the windscreen.
    /// </summary>
    List<int> ResolveStopOrder(BusRoute route, int stopCount)
    {
        var order = new List<int>(stopCount);
        for (int i = 0; i < stopCount; i++) order.Add(i);
        if (stopCount < 2) return order;

        BusController bus = MissionManager.Instance != null ? MissionManager.Instance.busController : null;
        if (bus == null) bus = FindAnyObjectByType<BusController>();
        Vector3 focus = bus != null ? bus.transform.position : GPSManager.Instance.GpsToWorld(
            route.stops[0].latitude, route.stops[0].longitude);
        Vector3 forward = ViewPriority.ResolveForward(bus != null ? bus.transform : null);

        order.Sort((a, b) =>
        {
            Vector3 pa = GPSManager.Instance.GpsToWorld(route.stops[a].latitude, route.stops[a].longitude);
            Vector3 pb = GPSManager.Instance.GpsToWorld(route.stops[b].latitude, route.stops[b].longitude);
            float scoreA = ViewPriority.Score(focus, forward, ViewPriority.DefaultFovDegrees, pa);
            float scoreB = ViewPriority.Score(focus, forward, ViewPriority.DefaultFovDegrees, pb);
            if (!Mathf.Approximately(scoreA, scoreB)) return scoreA.CompareTo(scoreB);
            return a.CompareTo(b);
        });
        return order;
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

    /// <summary>
    /// Slides a finished prop away from the kerb until its whole footprint clears
    /// the asphalt. The docking trigger stays at the kerb; only the visual prop
    /// moves. Returns false when even the last attempt still overlaps, which lets
    /// the caller fall back to a smaller marker instead of leaving an obstacle in
    /// the running lane.
    /// </summary>
    static bool MoveClearOfRoad(GameObject prop, RoadSurfaceIndex roads, Vector3 outward, float clearance, float step, int attempts)
    {
        if (prop == null || roads == null) return false;
        for (int attempt = 0; attempt < attempts; attempt++)
        {
            Physics.SyncTransforms();
            var renderers = prop.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0) return false;
            Bounds bounds = renderers[0].bounds;
            foreach (var renderer in renderers) bounds.Encapsulate(renderer.bounds);
            foreach (var collider in prop.GetComponentsInChildren<Collider>())
                if (collider.enabled && !collider.isTrigger) bounds.Encapsulate(collider.bounds);

            var footprint = new[] { new Vector3(bounds.min.x, 0, bounds.min.z), new Vector3(bounds.max.x, 0, bounds.min.z),
                new Vector3(bounds.max.x, 0, bounds.max.z), new Vector3(bounds.min.x, 0, bounds.max.z) };
            if (!roads.OverlapsFootprint(footprint, clearance)) return true;
            prop.transform.position += outward * step;
        }
        return false;
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
