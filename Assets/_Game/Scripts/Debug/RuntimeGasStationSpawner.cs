using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RuntimeGasStationSpawner : MonoBehaviour
{
    [Header("Resources (relative to Assets/_Game/Resources)")]
    public string gasStationResourcePath = "BussimAssets/GasStation";
    public string generatedGasStationResourcesPath = "GasStationsGenerated";

    [Header("Placement")]
    public int maxStations = 25;
    public float minSpacingMeters = 120f;
    public float yOffset = 0f;
    public bool logSpawns = true;

    [Header("Drive-in service")]
    [Tooltip("Lifts the station above the map tiles so its apron is not buried. Map tiles sit at 0.15 m and generated roads at 0.25 m.")]
    public float groundClearance = 0.28f;
    [Tooltip("Maximum radius of the trigger the bus has to enter to refuel or charge. Clamped to the station footprint.")]
    public float serviceRadius = 11f;
    public float refuelLitresPerSecond = 22f;
    public float rechargeKWhPerSecond = 55f;
    [Tooltip("Paves a drive-in apron around each station with the imported low-poly road modules.")]
    public bool addForecourt = true;
    [Tooltip("Station colliders become triggers so the bus can drive under the canopy instead of hitting a solid box.")]
    public bool makeCollidersTriggers = true;

    [Header("Imported model fallback")]
    [Tooltip("Applied only to untextured near-white materials on imported station models.")]
    public Color importedFallbackColor = new Color(0.72f, 0.73f, 0.70f, 1f);

    /// <summary>True once the fuel-tag pass has finished, including the no-data case.</summary>
    public bool SpawnComplete { get; private set; }

    void Start()
    {
        StartCoroutine(SpawnWhenReady());
    }

    IEnumerator SpawnWhenReady()
    {
        while (OSMLoader.Instance == null || !OSMLoader.Instance.dataLoaded || GPSManager.Instance == null)
            yield return null;

        SpawnFromOsmFuelTags();
        SpawnComplete = true;
    }

    void SpawnFromOsmFuelTags()
    {
        var fuelResponse = LoadFuelResponse();
        if (fuelResponse != null && fuelResponse.elements != null && fuelResponse.elements.Count > 0)
        {
            SpawnFromFuelResponse(fuelResponse);
            return;
        }

        var data = OSMLoader.Instance.osmData;
        if (data == null || data.ways == null || data.ways.Count == 0)
        {
            if (logSpawns)
                Debug.LogWarning("[RuntimeGasStationSpawner] OSM data missing. No gas stations spawned.");
            return;
        }

        GameObject[] prefabs = LoadStationPrefabs();
        if (prefabs.Length == 0)
        {
            if (logSpawns)
                Debug.LogWarning($"[RuntimeGasStationSpawner] Missing resource: {gasStationResourcePath}");
            return;
        }

        var placed = new List<Vector3>();
        int spawned = 0;

        foreach (var way in data.ways)
        {
            if (spawned >= maxStations) break;
            if (!IsFuelWay(way)) continue;

            if (!TryGetWayCentroidWorld(way, data, out var world))
                continue;

            if (!IsFarEnough(world, placed))
                continue;

            SpawnStation(prefabs[spawned % prefabs.Length], world);
            placed.Add(world);
            spawned++;

            if (logSpawns)
                Debug.Log($"[RuntimeGasStationSpawner] Spawned gas station at {world}.");
        }

        if (logSpawns)
            Debug.Log($"[RuntimeGasStationSpawner] Gas stations spawned: {spawned}.");
    }

    OverpassResponse LoadFuelResponse()
    {
        var city = CityManager.Instance != null ? CityManager.Instance.activeCity : null;
        if (city == null) return null;

        string path = RuntimeCityContentStore.ResolveReadPath(city, "fuel.json");
        if (!File.Exists(path)) return null;

        string json = File.ReadAllText(path);
        return OverpassResponse.Deserialize(json);
    }

    void SpawnFromFuelResponse(OverpassResponse response)
    {
        GameObject[] prefabs = LoadStationPrefabs();
        if (prefabs.Length == 0)
        {
            if (logSpawns)
                Debug.LogWarning($"[RuntimeGasStationSpawner] Missing resource: {gasStationResourcePath}");
            return;
        }

        var placed = new List<Vector3>();
        int spawned = 0;

        foreach (var elem in response.elements)
        {
            if (spawned >= maxStations) break;
            if (!IsFuelElement(elem)) continue;

            if (!TryGetElementWorld(elem, out var world))
                continue;

            if (!IsFarEnough(world, placed))
                continue;

            SpawnStation(prefabs[spawned % prefabs.Length], world);
            placed.Add(world);
            spawned++;
        }

        if (logSpawns)
            Debug.Log($"[RuntimeGasStationSpawner] Gas stations spawned from fuel.json: {spawned}.");
    }

    void SpawnStation(GameObject prefab, Vector3 world)
    {
        if (prefab == null) return;

        world.y = Mathf.Max(0f, world.y) + yOffset + groundClearance;
        var station = Instantiate(prefab, world, Quaternion.identity, transform);
        ImportedVehicleVisualRepair.DisableEmbeddedCameras(station.transform);
        ImportedMaterialPalette.ApplyFallback(station, importedFallbackColor);
        ImportedMaterialPalette.RegisterImportedRoot(transform, importedFallbackColor);

        if (makeCollidersTriggers)
        {
            foreach (var collider in station.GetComponentsInChildren<Collider>(true))
                collider.isTrigger = true;
        }

        Bounds localBounds = LocalBounds(station);
        if (localBounds.size.sqrMagnitude <= .0001f)
            localBounds = new Bounds(new Vector3(0f, 1.5f, 0f), new Vector3(8f, 3f, 8f));

        // The station is a 130k-triangle hero prop. Put its full mesh in LOD0 and
        // let the shared box proxies take over before the player reaches it.
        int triangles = TriangleCount(station);
        if (SceneryLodPolicy.NeedsLod(triangles, SceneryLodPolicy.BuildingLodTriangleThreshold))
        {
            SceneryLodBuilder.Attach(station, localBounds,
                SceneryLodPolicy.BuildingLod1ScreenHeight,
                SceneryLodPolicy.BuildingLod2ScreenHeight,
                new Color(.55f, .56f, .58f), castShadows: false);
        }

        if (addForecourt)
            GasStationForecourt.Build(station.transform, localBounds);

        AddServicePoint(station, localBounds);

        // Caches every renderer, including the forecourt, and toggles only those,
        // so the trigger keeps working while the station is off screen.
        station.AddComponent<DistanceVisibility>().visibleDistance = 340f;
    }

    void AddServicePoint(GameObject station, Bounds localBounds)
    {
        var serviceObject = new GameObject("ServicePoint");
        serviceObject.transform.SetParent(station.transform, false);
        serviceObject.transform.localPosition = new Vector3(
            localBounds.center.x,
            Mathf.Max(1f, localBounds.center.y),
            localBounds.center.z);

        var trigger = serviceObject.AddComponent<SphereCollider>();
        trigger.isTrigger = true;
        // Keep the trigger inside the station footprint. A fixed 11 m sphere is
        // right for the 34 m forecourt but would let the bus refuel while passing
        // a small roadside pump.
        float footprint = Mathf.Max(localBounds.extents.x, localBounds.extents.z);
        trigger.radius = Mathf.Clamp(serviceRadius, 2f, Mathf.Max(2f, footprint));

        var point = serviceObject.AddComponent<GasStationServicePoint>();
        point.refuelLitresPerSecond = Mathf.Max(0f, refuelLitresPerSecond);
        point.rechargeKWhPerSecond = Mathf.Max(0f, rechargeKWhPerSecond);
    }

    static Bounds LocalBounds(GameObject root)
    {
        var renderers = root.GetComponentsInChildren<Renderer>(true);
        Bounds bounds = new Bounds();
        bool has = false;
        foreach (var renderer in renderers)
        {
            if (renderer == null) continue;
            Bounds world = renderer.bounds;
            Vector3 center = world.center;
            Vector3 extents = world.extents;
            for (int corner = 0; corner < 8; corner++)
            {
                Vector3 point = center + new Vector3(
                    (corner & 1) == 0 ? -extents.x : extents.x,
                    (corner & 2) == 0 ? -extents.y : extents.y,
                    (corner & 4) == 0 ? -extents.z : extents.z);
                Vector3 local = root.transform.InverseTransformPoint(point);
                if (!has) { bounds = new Bounds(local, Vector3.zero); has = true; }
                else bounds.Encapsulate(local);
            }
        }
        return bounds;
    }

    static int TriangleCount(GameObject root)
    {
        int total = 0;
        foreach (var filter in root.GetComponentsInChildren<MeshFilter>(true))
        {
            Mesh mesh = filter.sharedMesh;
            if (mesh == null) continue;
            for (int i = 0; i < mesh.subMeshCount; i++)
                total += (int)mesh.GetIndexCount(i) / 3;
        }
        return total;
    }

    bool IsFuelWay(OSMWay way)
    {
        if (way == null || way.tags == null || way.tags.Count == 0)
            return false;

        if (way.tags.TryGetValue("amenity", out var amenity) && amenity == "fuel")
            return true;

        if (way.tags.TryGetValue("building", out var building) && building == "fuel")
            return true;

        if (way.tags.TryGetValue("shop", out var shop) && shop == "fuel")
            return true;

        return false;
    }

    GameObject[] LoadStationPrefabs()
    {
        GameObject[] generated = Resources.LoadAll<GameObject>(generatedGasStationResourcesPath);
        if (generated != null && generated.Length > 0)
        {
            // Resources.LoadAll order is not guaranteed. Sorting by name keeps the
            // prefab chosen for each OSM station stable between sessions.
            System.Array.Sort(generated, (a, b) => string.CompareOrdinal(a != null ? a.name : string.Empty, b != null ? b.name : string.Empty));
            return generated;
        }
        GameObject fallback = Resources.Load<GameObject>(gasStationResourcePath);
        return fallback != null ? new[] { fallback } : System.Array.Empty<GameObject>();
    }

    bool IsFuelElement(OverpassResponse.Element elem)
    {
        if (elem == null || elem.tags == null || elem.tags.Count == 0)
            return false;

        if (elem.tags.TryGetValue("amenity", out var amenity) && amenity == "fuel")
            return true;

        return false;
    }

    bool TryGetElementWorld(OverpassResponse.Element elem, out Vector3 world)
    {
        world = Vector3.zero;
        if (GPSManager.Instance == null || elem == null) return false;

        if (elem.type == "node")
        {
            world = GPSManager.Instance.GpsToWorld(elem.lat, elem.lon);
            return true;
        }

        if (elem.geometry != null && elem.geometry.Count > 0)
        {
            double latSum = 0;
            double lonSum = 0;
            for (int i = 0; i < elem.geometry.Count; i++)
            {
                latSum += elem.geometry[i].lat;
                lonSum += elem.geometry[i].lon;
            }
            double lat = latSum / elem.geometry.Count;
            double lon = lonSum / elem.geometry.Count;
            world = GPSManager.Instance.GpsToWorld(lat, lon);
            return true;
        }

        return false;
    }

    bool TryGetWayCentroidWorld(OSMWay way, OSMData data, out Vector3 world)
    {
        world = Vector3.zero;
        if (way.nodeRefs == null || way.nodeRefs.Count == 0) return false;

        int count = 0;
        double latSum = 0;
        double lonSum = 0;

        for (int i = 0; i < way.nodeRefs.Count; i++)
        {
            if (!data.nodeMap.TryGetValue(way.nodeRefs[i], out var node))
                continue;

            latSum += node.lat;
            lonSum += node.lon;
            count++;
        }

        if (count == 0) return false;

        double lat = latSum / count;
        double lon = lonSum / count;
        world = GPSManager.Instance.GpsToWorld(lat, lon);
        return true;
    }

    bool IsFarEnough(Vector3 candidate, List<Vector3> placed)
    {
        for (int i = 0; i < placed.Count; i++)
        {
            if (Vector3.Distance(candidate, placed[i]) < minSpacingMeters)
                return false;
        }
        return true;
    }
}
