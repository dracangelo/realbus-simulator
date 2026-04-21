using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RuntimeOsmPoiVisualizer : MonoBehaviour
{
    enum PoiCategory
    {
        School,
        Hospital,
        Shop,
        Fuel,
        Landmark
    }

    [Header("Placement")]
    public float markerHeight = 18f;
    public float markerRadius = 3f;
    public float yOffset = 0.5f;
    public float minSpacingMeters = 40f;
    public int maxPerCategory = 40;

    [Header("Debug")]
    public bool logSpawns = true;

    void Start()
    {
        StartCoroutine(SpawnWhenReady());
    }

    IEnumerator SpawnWhenReady()
    {
        while (GPSManager.Instance == null)
            yield return null;

        SpawnPoiMarkers();
    }

    void SpawnPoiMarkers()
    {
        var city = CityManager.Instance != null ? CityManager.Instance.activeCity : null;
        if (city == null)
        {
            if (logSpawns)
                Debug.LogWarning("[RuntimeOsmPoiVisualizer] No active city. Skipping POI markers.");
            return;
        }

        string cityDir = Path.Combine(Application.streamingAssetsPath, "Cities", city.cityCode);
        string poiPath = Path.Combine(cityDir, "poi.json");
        string fuelPath = Path.Combine(cityDir, "fuel.json");
        string buildingsJsonPath = Path.ChangeExtension(city.GetBuildingsPath(), ".json");

        var parent = new GameObject("OSM_POIs");
        parent.transform.SetParent(transform, false);

        var placedByCategory = new Dictionary<PoiCategory, List<Vector3>>();
        int total = 0;

        if (File.Exists(poiPath))
            total += SpawnFromResponse(OverpassResponse.Deserialize(File.ReadAllText(poiPath)), parent.transform, placedByCategory);
        else if (File.Exists(buildingsJsonPath))
            total += SpawnFromResponse(OverpassResponse.Deserialize(File.ReadAllText(buildingsJsonPath)), parent.transform, placedByCategory);
        else if (logSpawns)
            Debug.LogWarning($"[RuntimeOsmPoiVisualizer] Missing poi.json and buildings.json for city {city.cityCode}. Download OSM place data first.");

        if (File.Exists(fuelPath))
            total += SpawnFromResponse(OverpassResponse.Deserialize(File.ReadAllText(fuelPath)), parent.transform, placedByCategory);

        if (logSpawns)
            Debug.Log($"[RuntimeOsmPoiVisualizer] Spawned {total} POI markers.");
    }

    int SpawnFromResponse(OverpassResponse response, Transform parent, Dictionary<PoiCategory, List<Vector3>> placedByCategory)
    {
        if (response?.elements == null)
            return 0;

        int spawned = 0;

        for (int i = 0; i < response.elements.Count; i++)
        {
            var elem = response.elements[i];
            if (!TryResolveCategory(elem, out var category))
                continue;

            if (!TryGetElementWorld(elem, out var world))
                continue;

            if (!placedByCategory.TryGetValue(category, out var placed))
            {
                placed = new List<Vector3>();
                placedByCategory[category] = placed;
            }

            if (placed.Count >= maxPerCategory || !IsFarEnough(world, placed))
                continue;

            world.y += yOffset + markerHeight * 0.5f;
            var marker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            marker.name = $"POI_{category}_{ResolveName(elem)}";
            marker.transform.SetParent(parent, true);
            marker.transform.position = world;
            marker.transform.localScale = new Vector3(markerRadius, markerHeight * 0.5f, markerRadius);

            var renderer = marker.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial = CreateMarkerMaterial(category);
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                renderer.receiveShadows = false;
            }

            placed.Add(world);
            spawned++;
        }

        return spawned;
    }

    bool TryResolveCategory(OverpassResponse.Element elem, out PoiCategory category)
    {
        category = PoiCategory.Landmark;
        var tags = elem?.tags;
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

        if (tags.ContainsKey("shop"))
        {
            category = PoiCategory.Shop;
            return true;
        }

        if (tags.ContainsKey("tourism") || tags.ContainsKey("historic") || tags.ContainsKey("leisure"))
        {
            category = PoiCategory.Landmark;
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
            double latSum = 0;
            double lonSum = 0;
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

    string ResolveName(OverpassResponse.Element elem)
    {
        if (elem?.tags != null && elem.tags.TryGetValue("name", out var name) && !string.IsNullOrWhiteSpace(name))
            return name.Replace(" ", "_");

        return elem != null ? elem.id.ToString() : "Unknown";
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

    Material CreateMarkerMaterial(PoiCategory category)
    {
        var shader = Shader.Find("Universal Render Pipeline/Lit")
            ?? Shader.Find("Standard")
            ?? Shader.Find("Unlit/Color");
        var material = new Material(shader);
        material.color = ResolveColor(category);
        if (material.HasProperty("_Glossiness")) material.SetFloat("_Glossiness", 0.05f);
        if (material.HasProperty("_Metallic")) material.SetFloat("_Metallic", 0f);
        if (material.HasProperty("_Smoothness")) material.SetFloat("_Smoothness", 0.08f);
        return material;
    }

    Color ResolveColor(PoiCategory category)
    {
        switch (category)
        {
            case PoiCategory.School: return new Color(0.96f, 0.74f, 0.22f, 1f);
            case PoiCategory.Hospital: return new Color(0.87f, 0.28f, 0.32f, 1f);
            case PoiCategory.Shop: return new Color(0.33f, 0.72f, 0.52f, 1f);
            case PoiCategory.Fuel: return new Color(0.21f, 0.65f, 0.91f, 1f);
            default: return new Color(0.74f, 0.48f, 0.87f, 1f);
        }
    }
}
