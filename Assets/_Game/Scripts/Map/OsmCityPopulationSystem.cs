using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OsmCityPopulationSystem : MonoBehaviour
{
    enum PoiCategory
    {
        Unknown,
        School,
        Hospital,
        Shop,
        Fuel,
        Landmark
    }

    [Header("Runtime")]
    public bool populateOnStart = true;
    public bool clearExistingChildrenBeforePopulate = true;
    public bool logSpawns = true;

    [Header("Assets")]
    public string[] treeResourcePaths =
    {
        "BussimAssets/trees/Sour wood_Spring corona",
        "BussimAssets/trees/uploads_files_2986400_FBX+Corona",
        "BussimAssets/trees/uploads_files_6970825_TUALANG"
    };
    public string[] roadsideVehicleResourcePaths =
    {
        "BussimAssets/aitraffic/Van",
        "BussimAssets/aitraffic/uploads_files_1862674_Beetle+-+FBX",
        "BussimAssets/aitraffic/uploads_files_5836120_Fortuner",
        "BussimAssets/aitraffic/uploads_files_5844339_Palisade2024",
        "BussimAssets/aitraffic/uploads_files_5533688_pickup+test2",
        "BussimAssets/aitraffic/uploads_files_4076767_SCANIA"
    };
    public string[] crowdResourcePaths =
    {
        "BussimAssets/passenger/Realistic man",
        "BussimAssets/passenger/Man_in_black_dress_0227",
        "BussimAssets/passenger/uploads_files_2460380_Marina_1276",
        "BussimAssets/passenger/uploads_files_6231172_woman+3d+fbx"
    };
    public string[] benchResourcePaths =
    {
        "BussimAssets/stops/Bench"
    };

    [Header("Trees")]
    public int maxTrees = 320;
    public float treeSpacingMeters = 28f;
    public float treeOffsetFromRoadEdgeMeters = 5f;
    public Vector2 treeScaleRange = new Vector2(0.8f, 1.35f);

    [Header("Roadside Vehicles")]
    public int maxRoadsideVehicles = 48;
    public float roadsideVehicleSpacingMeters = 85f;
    public float roadsideVehicleOffsetFromRoadEdgeMeters = 4.5f;
    public Vector2 roadsideVehicleScaleRange = new Vector2(0.95f, 1.1f);

    [Header("POI Activity")]
    public int maxCrowdActors = 90;
    public int maxBenches = 36;
    public float poiSpacingMeters = 24f;
    public float poiActivityRadiusMeters = 10f;
    public Vector2 crowdScaleRange = new Vector2(0.95f, 1.05f);

    [Header("Placement")]
    public float objectLift = 0f;
    public float raycastHeight = 160f;

    readonly List<Vector3> placedTrees = new List<Vector3>();
    readonly List<Vector3> placedRoadsideVehicles = new List<Vector3>();
    readonly List<Vector3> placedCrowd = new List<Vector3>();
    readonly List<Vector3> placedBenches = new List<Vector3>();

    int spawnedTrees;
    int spawnedRoadsideVehicles;
    int spawnedCrowdActors;
    int spawnedBenches;
    Transform populationRoot;

    void Start()
    {
        if (!populateOnStart)
            return;

        StartCoroutine(PopulateWhenReady());
    }

    IEnumerator PopulateWhenReady()
    {
        while (GPSManager.Instance == null || OSMLoader.Instance == null || !OSMLoader.Instance.dataLoaded)
            yield return null;

        float waitDeadline = Time.time + 8f;
        while (Time.time < waitDeadline)
        {
            if (OSMRoadMeshBuilder.Instance != null && OSMRoadMeshBuilder.Instance.roadsBuilt)
                break;
            yield return null;
        }

        PopulateCity();
    }

    public void PopulateCity()
    {
        PrepareRoot();

        var osm = OSMLoader.Instance != null ? OSMLoader.Instance.osmData : null;
        if (osm != null)
        {
            SpawnRoadsideTrees(osm);
            SpawnRoadsideVehicles(osm);
        }

        SpawnPoiActivity();

        if (logSpawns)
        {
            Debug.Log(
                $"[OsmCityPopulationSystem] Populated city with " +
                $"{spawnedTrees} trees, {spawnedRoadsideVehicles} roadside vehicles, " +
                $"{spawnedCrowdActors} crowd actors, {spawnedBenches} benches.");
        }
    }

    void PrepareRoot()
    {
        if (populationRoot == null)
        {
            populationRoot = new GameObject("City Population").transform;
            populationRoot.SetParent(transform, false);
        }
        if (clearExistingChildrenBeforePopulate)
        {
            // MapSystem also owns fuel stations and other streamed content.
            // Never clear another spawner's children when refreshing population.
            for (int i = populationRoot.childCount - 1; i >= 0; i--)
                Destroy(populationRoot.GetChild(i).gameObject);
        }

        placedTrees.Clear();
        placedRoadsideVehicles.Clear();
        placedCrowd.Clear();
        placedBenches.Clear();
        spawnedTrees = 0;
        spawnedRoadsideVehicles = 0;
        spawnedCrowdActors = 0;
        spawnedBenches = 0;
    }

    void SpawnRoadsideTrees(OSMData osm)
    {
        if (osm == null || osm.ways == null || osm.ways.Count == 0)
            return;

        for (int i = 0; i < osm.ways.Count && spawnedTrees < maxTrees; i++)
        {
            var way = osm.ways[i];
            if (!IsTreeEligibleRoad(way))
                continue;

            SampleWay(way, osm, treeSpacingMeters, (position, forward, right) =>
            {
                if (spawnedTrees >= maxTrees)
                    return;

                float side = Random.value > 0.5f ? 1f : -1f;
                float offset = (way.GetRoadWidth() * 0.5f) + treeOffsetFromRoadEdgeMeters + Random.Range(1f, 5f);
                Vector3 candidate = position + right * side * offset;
                candidate.y = ResolveGroundY(candidate);

                if (!IsFarEnough(candidate, placedTrees, treeSpacingMeters * 0.75f))
                    return;

                var prefab = LoadRandomPrefab(treeResourcePaths);
                if (prefab == null)
                    return;

                var tree = Instantiate(prefab, candidate + Vector3.up * objectLift, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f), populationRoot);
                ImportedVehicleVisualRepair.DisableEmbeddedCameras(tree.transform);
                float scale = Random.Range(treeScaleRange.x, treeScaleRange.y);
                tree.transform.localScale = Vector3.Scale(tree.transform.localScale, Vector3.one * scale);
                placedTrees.Add(candidate);
                spawnedTrees++;
            });
        }
    }

    void SpawnRoadsideVehicles(OSMData osm)
    {
        if (osm == null || osm.ways == null || osm.ways.Count == 0)
            return;

        for (int i = 0; i < osm.ways.Count && spawnedRoadsideVehicles < maxRoadsideVehicles; i++)
        {
            var way = osm.ways[i];
            if (!IsVehicleEligibleRoad(way))
                continue;

            SampleWay(way, osm, roadsideVehicleSpacingMeters, (position, forward, right) =>
            {
                if (spawnedRoadsideVehicles >= maxRoadsideVehicles)
                    return;

                float side = Random.value > 0.5f ? 1f : -1f;
                float offset = (way.GetRoadWidth() * 0.5f) + roadsideVehicleOffsetFromRoadEdgeMeters + Random.Range(0.5f, 2.5f);
                Vector3 candidate = position + right * side * offset;
                candidate.y = ResolveGroundY(candidate);

                if (!IsFarEnough(candidate, placedRoadsideVehicles, roadsideVehicleSpacingMeters * 0.7f))
                    return;

                var prefab = LoadRandomPrefab(roadsideVehicleResourcePaths);
                if (prefab == null)
                    return;

                Quaternion rotation = Quaternion.LookRotation(side > 0f ? forward : -forward, Vector3.up);
                var vehicle = Instantiate(prefab, candidate + Vector3.up * objectLift, rotation, populationRoot);
                ImportedVehicleVisualRepair.DisableEmbeddedCameras(vehicle.transform);
                float scale = Random.Range(roadsideVehicleScaleRange.x, roadsideVehicleScaleRange.y);
                vehicle.transform.localScale = Vector3.Scale(vehicle.transform.localScale, Vector3.one * scale);
                placedRoadsideVehicles.Add(candidate);
                spawnedRoadsideVehicles++;
            });
        }
    }

    void SpawnPoiActivity()
    {
        var city = CityManager.Instance != null ? CityManager.Instance.activeCity : null;
        if (city == null || string.IsNullOrWhiteSpace(city.cityCode))
            return;

        SpawnPoiActivityFromResponse(LoadResponse(RuntimeCityContentStore.ResolveReadPath(city, "poi.json")));
        SpawnPoiActivityFromResponse(LoadResponse(RuntimeCityContentStore.ResolveReadPath(city, "buildings.json")));
    }

    void SpawnPoiActivityFromResponse(OverpassResponse response)
    {
        if (response == null || response.elements == null || response.elements.Count == 0)
            return;

        response.Resolve();

        for (int i = 0; i < response.elements.Count; i++)
        {
            if (spawnedCrowdActors >= maxCrowdActors && spawnedBenches >= maxBenches)
                return;

            var elem = response.elements[i];
            if (!TryResolvePoiCategory(elem, out var category))
                continue;

            if (!TryGetElementWorld(elem, out var center))
                continue;

            if (!IsFarEnough(center, placedCrowd, poiSpacingMeters) &&
                !IsFarEnough(center, placedBenches, poiSpacingMeters))
                continue;

            switch (category)
            {
                case PoiCategory.Shop:
                case PoiCategory.Landmark:
                    SpawnBenchNear(center);
                    SpawnCrowdCluster(center, Random.Range(1, 3));
                    break;
                case PoiCategory.School:
                    SpawnCrowdCluster(center, Random.Range(2, 4));
                    SpawnTreesAroundPoi(center, 2);
                    break;
                case PoiCategory.Hospital:
                    SpawnBenchNear(center);
                    SpawnCrowdCluster(center, Random.Range(1, 3));
                    SpawnTreesAroundPoi(center, 1);
                    break;
                case PoiCategory.Fuel:
                    SpawnCrowdCluster(center, 1);
                    break;
            }
        }
    }

    void SpawnTreesAroundPoi(Vector3 center, int count)
    {
        for (int i = 0; i < count && spawnedTrees < maxTrees; i++)
        {
            Vector2 ring = Random.insideUnitCircle.normalized * Random.Range(6f, poiActivityRadiusMeters);
            Vector3 candidate = new Vector3(center.x + ring.x, 0f, center.z + ring.y);
            candidate.y = ResolveGroundY(candidate);
            if (!IsFarEnough(candidate, placedTrees, 8f))
                continue;

            var prefab = LoadRandomPrefab(treeResourcePaths);
            if (prefab == null)
                return;

            var tree = Instantiate(prefab, candidate + Vector3.up * objectLift, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f), populationRoot);
            ImportedVehicleVisualRepair.DisableEmbeddedCameras(tree.transform);
            float scale = Random.Range(treeScaleRange.x, treeScaleRange.y);
            tree.transform.localScale = Vector3.Scale(tree.transform.localScale, Vector3.one * scale);
            placedTrees.Add(candidate);
            spawnedTrees++;
        }
    }

    void SpawnBenchNear(Vector3 center)
    {
        if (spawnedBenches >= maxBenches)
            return;

        Vector2 ring = Random.insideUnitCircle.normalized * Random.Range(4f, poiActivityRadiusMeters);
        Vector3 candidate = new Vector3(center.x + ring.x, 0f, center.z + ring.y);
        candidate.y = ResolveGroundY(candidate);

        if (!IsFarEnough(candidate, placedBenches, poiSpacingMeters * 0.8f))
            return;

        var prefab = LoadRandomPrefab(benchResourcePaths);
        if (prefab == null)
            return;

        var bench = Instantiate(prefab, candidate + Vector3.up * objectLift, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f), populationRoot);
        ImportedVehicleVisualRepair.DisableEmbeddedCameras(bench.transform);
        placedBenches.Add(candidate);
        spawnedBenches++;
    }

    void SpawnCrowdCluster(Vector3 center, int count)
    {
        for (int i = 0; i < count && spawnedCrowdActors < maxCrowdActors; i++)
        {
            Vector2 ring = Random.insideUnitCircle * poiActivityRadiusMeters;
            Vector3 candidate = new Vector3(center.x + ring.x, 0f, center.z + ring.y);
            candidate.y = ResolveGroundY(candidate);

            if (!IsFarEnough(candidate, placedCrowd, 3.5f))
                continue;

            var prefab = LoadRandomPrefab(crowdResourcePaths);
            if (prefab == null)
                return;

            Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            var actor = Instantiate(prefab, candidate + Vector3.up * objectLift, rotation, populationRoot);
            ImportedVehicleVisualRepair.DisableEmbeddedCameras(actor.transform);
            float scale = Random.Range(crowdScaleRange.x, crowdScaleRange.y);
            actor.transform.localScale = Vector3.Scale(actor.transform.localScale, Vector3.one * scale);
            placedCrowd.Add(candidate);
            spawnedCrowdActors++;
        }
    }

    void SampleWay(OSMWay way, OSMData data, float spacingMeters, System.Action<Vector3, Vector3, Vector3> onSample)
    {
        if (way == null || data == null || way.nodeRefs == null || way.nodeRefs.Count < 2 || onSample == null)
            return;

        Vector3? previous = null;
        float carry = Random.Range(0f, spacingMeters);

        for (int i = 0; i < way.nodeRefs.Count; i++)
        {
            if (!data.nodeMap.TryGetValue(way.nodeRefs[i], out var node))
                continue;

            Vector3 current = GPSManager.Instance.GpsToWorld(node.lat, node.lon);
            current.y = 0f;

            if (previous.HasValue)
            {
                Vector3 start = previous.Value;
                Vector3 end = current;
                Vector3 segment = end - start;
                float length = segment.magnitude;
                if (length > 0.1f)
                {
                    Vector3 forward = segment / length;
                    Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;

                    float travelled = carry;
                    while (travelled < length)
                    {
                        Vector3 sample = start + forward * travelled;
                        onSample(sample, forward, right);
                        travelled += spacingMeters;
                    }

                    carry = travelled - length;
                }
            }

            previous = current;
        }
    }

    bool TryResolvePoiCategory(OverpassResponse.Element elem, out PoiCategory category)
    {
        category = PoiCategory.Unknown;
        var tags = elem != null ? elem.tags : null;
        if (tags == null || tags.Count == 0)
            return false;

        if (tags.TryGetValue("amenity", out var amenity))
        {
            if (amenity == "school" || amenity == "college" || amenity == "university")
            {
                category = PoiCategory.School;
                return true;
            }

            if (amenity == "hospital" || amenity == "clinic" || amenity == "doctors" || amenity == "pharmacy")
            {
                category = PoiCategory.Hospital;
                return true;
            }

            if (amenity == "fuel")
            {
                category = PoiCategory.Fuel;
                return true;
            }
        }

        if (tags.ContainsKey("shop") || tags.ContainsKey("office"))
        {
            category = PoiCategory.Shop;
            return true;
        }

        if (tags.ContainsKey("tourism") || tags.ContainsKey("historic") || tags.ContainsKey("leisure"))
        {
            category = PoiCategory.Landmark;
            return true;
        }

        if (tags.TryGetValue("building", out var building) &&
            (building == "school" || building == "hospital" || building == "commercial" || building == "retail"))
        {
            category = building == "school"
                ? PoiCategory.School
                : building == "hospital"
                    ? PoiCategory.Hospital
                    : PoiCategory.Shop;
            return true;
        }

        return false;
    }

    bool TryGetElementWorld(OverpassResponse.Element elem, out Vector3 world)
    {
        world = Vector3.zero;
        if (GPSManager.Instance == null || elem == null)
            return false;

        if (elem.type == "node")
        {
            world = GPSManager.Instance.GpsToWorld(elem.lat, elem.lon);
            return true;
        }

        if (elem.geometry != null && elem.geometry.Count > 0)
        {
            double latSum = 0d;
            double lonSum = 0d;
            for (int i = 0; i < elem.geometry.Count; i++)
            {
                latSum += elem.geometry[i].lat;
                lonSum += elem.geometry[i].lon;
            }

            world = GPSManager.Instance.GpsToWorld(latSum / elem.geometry.Count, lonSum / elem.geometry.Count);
            return true;
        }

        return false;
    }

    OverpassResponse LoadResponse(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            return null;

        return OverpassResponse.Deserialize(File.ReadAllText(path));
    }

    GameObject LoadRandomPrefab(string[] resourcePaths)
    {
        if (resourcePaths == null || resourcePaths.Length == 0)
            return null;

        int startIndex = Random.Range(0, resourcePaths.Length);
        for (int i = 0; i < resourcePaths.Length; i++)
        {
            string path = resourcePaths[(startIndex + i) % resourcePaths.Length];
            if (string.IsNullOrWhiteSpace(path))
                continue;

            var prefab = Resources.Load<GameObject>(path);
            if (prefab != null)
                return prefab;
        }

        return null;
    }

    float ResolveGroundY(Vector3 candidate)
    {
        Vector3 rayOrigin = candidate + Vector3.up * raycastHeight;
        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, raycastHeight * 2f, ~0, QueryTriggerInteraction.Ignore))
            return hit.point.y;

        var mapLoader = FindFirstObjectByType<MapTileLoader>();
        return mapLoader != null ? mapLoader.tileSurfaceY : candidate.y;
    }

    bool IsFarEnough(Vector3 candidate, List<Vector3> existing, float minSpacingMeters)
    {
        for (int i = 0; i < existing.Count; i++)
        {
            Vector3 delta = existing[i] - candidate;
            delta.y = 0f;
            if (delta.sqrMagnitude < minSpacingMeters * minSpacingMeters)
                return false;
        }

        return true;
    }

    bool IsTreeEligibleRoad(OSMWay way)
    {
        if (way == null || !way.IsRoad())
            return false;

        string type = way.GetRoadType();
        return type == "residential" ||
               type == "tertiary" ||
               type == "secondary" ||
               type == "service" ||
               type == "unclassified";
    }

    bool IsVehicleEligibleRoad(OSMWay way)
    {
        if (way == null || !way.IsRoad())
            return false;

        string type = way.GetRoadType();
        return type == "primary" ||
               type == "secondary" ||
               type == "tertiary" ||
               type == "residential";
    }
}
