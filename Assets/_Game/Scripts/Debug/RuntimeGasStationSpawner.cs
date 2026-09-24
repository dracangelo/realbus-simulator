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

    void Start()
    {
        StartCoroutine(SpawnWhenReady());
    }

    IEnumerator SpawnWhenReady()
    {
        while (OSMLoader.Instance == null || !OSMLoader.Instance.dataLoaded || GPSManager.Instance == null)
            yield return null;

        SpawnFromOsmFuelTags();
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

            world.y += yOffset;
            var station = Instantiate(prefabs[spawned % prefabs.Length], world, Quaternion.identity, transform);
            ImportedVehicleVisualRepair.DisableEmbeddedCameras(station.transform);
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

            world.y += yOffset;
            var station = Instantiate(prefabs[spawned % prefabs.Length], world, Quaternion.identity, transform);
            ImportedVehicleVisualRepair.DisableEmbeddedCameras(station.transform);
            placed.Add(world);
            spawned++;
        }

        if (logSpawns)
            Debug.Log($"[RuntimeGasStationSpawner] Gas stations spawned from fuel.json: {spawned}.");
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
        if (generated != null && generated.Length > 0) return generated;
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
