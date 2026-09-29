using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Fictional roadside infill; kept separate from real map footprints.</summary>
public class ProceduralBuildingFill : MonoBehaviour
{
    public float loadRadius = 280f;
    public float unloadRadius = 420f;
    public int maxBuildings = 160;
    const float CellSize = 100f;
    struct Segment { public Vector3 a, b; public float width; public long id; public bool fill; }
    readonly Dictionary<Vector2Int, List<Segment>> roads = new Dictionary<Vector2Int, List<Segment>>();
    readonly Dictionary<Vector2Int, List<GameObject>> cells = new Dictionary<Vector2Int, List<GameObject>>();
    readonly Dictionary<GameObject, Vector3> centers = new Dictionary<GameObject, Vector3>();
    OSMBuildingMeshBuilder builder;
    public int ActiveCount => centers.Count;

    void OnEnable() { StartCoroutine(Stream()); }

    void OnDisable()
    {
        StopAllCoroutines();
        if (builder != null)
            foreach (var item in centers.Keys) builder.ReleaseSyntheticBuilding(item);
        centers.Clear(); cells.Clear(); roads.Clear();
    }

    IEnumerator Stream()
    {
        builder = GetComponent<OSMBuildingMeshBuilder>();
        while (builder != null && (!builder.buildingsBuilt || GPSManager.Instance == null || OSMLoader.Instance == null || !OSMLoader.Instance.dataLoaded))
            yield return null;
        if (builder == null || !builder.enableProceduralFill) yield break;
        IndexRoads(OSMLoader.Instance.osmData);
        while (enabled && builder != null && builder.enableProceduralFill)
        {
            var bus = MissionManager.Instance != null ? MissionManager.Instance.busController : FindAnyObjectByType<BusController>();
            if (bus != null) yield return Refresh(bus.transform.position);
            yield return new WaitForSeconds(1f);
        }
        if (builder != null)
            foreach (var item in centers.Keys) builder.ReleaseSyntheticBuilding(item);
        centers.Clear(); cells.Clear();
    }

    static Vector2Int Cell(Vector3 p) => new Vector2Int(Mathf.FloorToInt(p.x / CellSize), Mathf.FloorToInt(p.z / CellSize));
    public static bool CanFillRoad(string type) => type == "residential" || type == "living_street" || type == "tertiary" || type == "secondary";

    void IndexRoads(OSMData data)
    {
        foreach (var way in data.ways)
        {
            if (!way.IsRoad()) continue;
            for (int i = 1; i < way.nodeRefs.Count; i++)
            {
                if (!data.nodeMap.TryGetValue(way.nodeRefs[i - 1], out var a) || !data.nodeMap.TryGetValue(way.nodeRefs[i], out var b)) continue;
                var segment = new Segment { a = GPSManager.Instance.GpsToWorld(a.lat, a.lon), b = GPSManager.Instance.GpsToWorld(b.lat, b.lon),
                    id = unchecked(way.id * 397 + i), width = way.GetRoadWidth(), fill = CanFillRoad(way.GetRoadType()) };
                var min = Cell(Vector3.Min(segment.a, segment.b) - Vector3.one * 40f);
                var max = Cell(Vector3.Max(segment.a, segment.b) + Vector3.one * 40f);
                for (int x = min.x; x <= max.x; x++)
                for (int z = min.y; z <= max.y; z++)
                {
                    var cell = new Vector2Int(x, z);
                    if (!roads.TryGetValue(cell, out var list)) roads[cell] = list = new List<Segment>();
                    list.Add(segment);
                }
            }
        }
    }

    IEnumerator Refresh(Vector3 player)
    {
        var remove = new List<Vector2Int>();
        foreach (var pair in cells)
        {
            Vector3 center = new Vector3((pair.Key.x + .5f) * CellSize, player.y, (pair.Key.y + .5f) * CellSize);
            if (Vector3.Distance(player, center) <= unloadRadius + CellSize) continue;
            foreach (var item in pair.Value) { centers.Remove(item); builder.ReleaseSyntheticBuilding(item); }
            remove.Add(pair.Key);
        }
        foreach (var key in remove) cells.Remove(key);
        var current = Cell(player);
        int radius = Mathf.CeilToInt(loadRadius / CellSize);
        for (int ring = 0; ring <= radius; ring++)
        for (int dx = -ring; dx <= ring; dx++)
        for (int dz = -ring; dz <= ring; dz++)
        {
            if (Mathf.Max(Mathf.Abs(dx), Mathf.Abs(dz)) != ring) continue;
            var key = current + new Vector2Int(dx, dz);
            Vector3 cellCenter = new Vector3((key.x + .5f) * CellSize, player.y, (key.y + .5f) * CellSize);
            if ((cellCenter - player).sqrMagnitude > loadRadius * loadRadius) continue;
            if (cells.ContainsKey(key) || centers.Count >= maxBuildings || !roads.TryGetValue(key, out var segments)) continue;
            var created = new List<GameObject>();
            cells[key] = created;
            foreach (var segment in segments)
            {
                if (!segment.fill) continue;
                Vector3 direction = segment.b - segment.a; direction.y = 0;
                float length = direction.magnitude;
                if (length < 18f) continue;
                direction /= length;
                Vector3 right = Vector3.Cross(Vector3.up, direction);
                for (int sample = 0; sample * 32f + 12f < length; sample++)
                for (int side = -1; side <= 1; side += 2)
                {
                    if (centers.Count >= maxBuildings) break;
                    uint hash = unchecked((uint)(segment.id * 31 + sample * 17 + side * 7));
                    float width = 7f + hash % 7, depth = 8f + (hash / 7) % 7;
                    Vector3 center = segment.a + direction * (sample * 32f + 12f) + right * side * (segment.width * .5f + 8f + depth * .5f);
                    center.y = builder.buildingBaseYOffset;
                    if (Cell(center) != key || builder.HasMappedFootprint(center, 18f) || !Clear(center, segments, Mathf.Sqrt(width * width + depth * depth) * .5f)) continue;
                    var footprint = new List<Vector3> { center - direction * width * .5f - right * depth * .5f,
                        center + direction * width * .5f - right * depth * .5f, center + direction * width * .5f + right * depth * .5f,
                        center - direction * width * .5f + right * depth * .5f };
                    var instance = builder.CreateSyntheticBuilding(footprint, 4f + (hash / 49) % 4 * 3f, -(long)hash - 1);
                    if (instance != null) { created.Add(instance); centers[instance] = center; }
                    yield return null; // At most one synthetic mesh per frame.
                }
            }
            yield return null;
        }
    }

    bool Clear(Vector3 center, List<Segment> segments, float radius)
    {
        foreach (var segment in segments)
        {
            Vector3 nearest = RoadSpawnResolver.ProjectOnSegment(center, segment.a, segment.b);
            nearest.y = center.y;
            if (Vector3.Distance(center, nearest) < segment.width * .5f + radius + 3f) return false;
        }
        foreach (var existing in centers.Values)
            if ((existing - center).sqrMagnitude < 25f * 25f) return false;
        return !Physics.CheckBox(center + Vector3.up * 5f, new Vector3(radius, 4f, radius), Quaternion.identity, ~0, QueryTriggerInteraction.Ignore);
    }
}
