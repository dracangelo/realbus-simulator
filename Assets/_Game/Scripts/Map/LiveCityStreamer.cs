using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Bounded, deterministic ambient scenery along loaded roads. Never owns road physics.</summary>
public class LiveCityStreamer : MonoBehaviour
{
    public LiveCityCatalog catalog;
    public float loadRadius = 180f;
    public float unloadRadius = 250f;
    public int maxProps = 64;
    public int triangleBudget = 360000;
    public int maxWalkers = 2;
    const float Spacing = 28f;
    struct Candidate
    {
        public RoadSurfaceIndex.Segment segment;
        public int sample, side;
        public Vector3 center, direction;
        public uint seed;
    }
    sealed class Spawned
    {
        public GameObject go;
        public LiveCityCatalog.Entry entry;
        public Vector3 center, direction;
        public Quaternion rotation;
        public float radius, phase;
    }
    readonly Dictionary<(RoadSurfaceIndex.Segment, int, int), Spawned> active = new Dictionary<(RoadSurfaceIndex.Segment, int, int), Spawned>();
    readonly Collider[] overlaps = new Collider[16];
    RoadSurfaceIndex index;
    int triangles, walkers;
    public int ActiveCount => active.Count;
    public int WalkerCount => walkers;
    public int TriangleCount => triangles;
    public int RoadOverlapCount
    {
        get
        {
            int count = 0;
            foreach (var item in active.Values) if (index != null && index.Contains(item.center, item.radius)) count++;
            return count;
        }
    }

    void OnEnable() { StartCoroutine(Stream()); }
    void OnDisable()
    {
        StopAllCoroutines();
        foreach (var item in active.Values) if (item.go != null) Destroy(item.go);
        active.Clear(); triangles = walkers = 0; index = null;
    }

    IEnumerator Stream()
    {
        if (catalog == null) catalog = Resources.Load<LiveCityCatalog>("LiveCityCatalog");
        if (catalog == null || catalog.entries.Length == 0)
        { Debug.LogWarning("[LiveCity] No catalog. Run Tools > RealBus > Setup > Build Live City Models."); yield break; }
        while (enabled)
        {
            var roads = OSMRoadMeshBuilder.Instance;
            var buildings = OSMBuildingMeshBuilder.Instance;
            if (roads == null || !roads.roadsBuilt || buildings == null || !buildings.buildingsBuilt)
            { yield return null; continue; }
            if (index != roads.SurfaceIndex)
            {
                foreach (var item in active.Values) if (item.go != null) Destroy(item.go);
                active.Clear(); triangles = walkers = 0;
                index = roads.SurfaceIndex;
            }
            var bus = MissionManager.Instance != null ? MissionManager.Instance.busController : FindAnyObjectByType<BusController>();
            if (bus != null && index != null) yield return Refresh(bus.transform, buildings);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Refresh(Transform viewer, OSMBuildingMeshBuilder buildings)
    {
        Vector3 player = viewer.position;
        var remove = new List<(RoadSurfaceIndex.Segment, int, int)>();
        foreach (var pair in active)
            if (FlatDistanceSquared(pair.Value.center, player) > Mathf.Max(loadRadius + 30f, unloadRadius) * Mathf.Max(loadRadius + 30f, unloadRadius)
                || (pair.Value.entry.kind == LiveCityCatalog.Kind.Pedestrian && FlatDistanceSquared(pair.Value.center, player) > 100f * 100f)
                || buildings.OverlapsBuilding(pair.Value.center, pair.Value.radius))
                remove.Add(pair.Key);
        foreach (var key in remove)
        {
            var item = active[key];
            triangles -= item.entry.triangles;
            if (item.entry.kind == LiveCityCatalog.Kind.Pedestrian) walkers--;
            Destroy(item.go); active.Remove(key);
        }
        var candidates = new List<Candidate>();
        foreach (var segment in index.Around(player, loadRadius))
        {
            Vector3 delta = segment.b - segment.a;
            float length = delta.magnitude;
            if (length < 20f) continue;
            Vector3 direction = delta / length;
            float projection = Vector3.Dot(player - segment.a, direction);
            int from = Mathf.Max(0, Mathf.FloorToInt((projection - loadRadius) / Spacing));
            int to = Mathf.Min(Mathf.FloorToInt((length - 12f) / Spacing), Mathf.CeilToInt((projection + loadRadius) / Spacing));
            for (int sample = from; sample <= to; sample++)
            for (int side = -1; side <= 1; side += 2)
            {
                Vector3 center = segment.a + direction * (12f + sample * Spacing);
                if (FlatDistanceSquared(center, player) > loadRadius * loadRadius || active.ContainsKey((segment, sample, side))) continue;
                candidates.Add(new Candidate { segment = segment, sample = sample, side = side, center = center, direction = direction,
                    seed = PlacementSeed(segment.wayId, center, side) });
            }
        }
        candidates.Sort((a, b) => FlatDistanceSquared(a.center, player).CompareTo(FlatDistanceSquared(b.center, player)));
        int examined = 0;
        foreach (var candidate in candidates)
        {
            if (viewer == null || FlatDistanceSquared(viewer.position, player) > 30f * 30f) yield break;
            if (active.Count >= Mathf.Max(0, maxProps)) break;
            if (++examined % 8 == 0) yield return null; // Bound rejection/clearance work as well as instantiation.
            var entry = SelectEntry(candidate.seed);
            if (entry == null || entry.prefab == null || triangles + entry.triangles > triangleBudget) continue;
            bool walking = entry.kind == LiveCityCatalog.Kind.Pedestrian;
            if (walking && (walkers >= maxWalkers || FlatDistanceSquared(candidate.center, player) > 75f * 75f)) continue;
            // Keep room for one nearby character even when trees are encountered first.
            if (!walking && walkers == 0 && maxWalkers > 0 && triangles + entry.triangles > triangleBudget - 100000) continue;
            float radius = Mathf.Max(.4f, new Vector2(entry.bounds.extents.x, entry.bounds.extents.z).magnitude);
            Vector3 right = Vector3.Cross(Vector3.up, candidate.direction);
            Vector3 position = candidate.center + right * candidate.side * (candidate.segment.halfWidth + radius + 1.8f);
            position.y = .08f;
            if (!Clear(position, radius, buildings)) continue;
            if (walking)
            {
                bool clear = true;
                for (int step = -5; step <= 5; step++)
                    if (!Clear(position + candidate.direction * step, radius, buildings)) { clear = false; break; }
                if (!clear) continue;
            }
            bool space = true;
            foreach (var item in active.Values)
                if (FlatDistanceSquared(position, item.center) < Mathf.Pow(radius + item.radius + (walking || item.entry.kind == LiveCityCatalog.Kind.Pedestrian ? 6f : 1f), 2)) { space = false; break; }
            if (!space) continue;
            Quaternion rotation = Quaternion.LookRotation(-right * candidate.side);
            if (entry.kind == LiveCityCatalog.Kind.Tree || entry.kind == LiveCityCatalog.Kind.Plant || entry.kind == LiveCityCatalog.Kind.Rock)
                rotation = Quaternion.Euler(0, candidate.seed % 360, 0);
            var go = Instantiate(entry.prefab, position, rotation, transform);
            active[(candidate.segment, candidate.sample, candidate.side)] = new Spawned { go = go, entry = entry, center = position,
                direction = candidate.direction, rotation = rotation, radius = radius, phase = (candidate.seed % 1000) * .01f };
            triangles += entry.triangles;
            if (walking) walkers++;
            yield return null; // At most one new model instance per frame.
        }
    }

    LiveCityCatalog.Entry SelectEntry(uint seed)
    {
        int roll = (int)(seed % 10);
        var kind = roll < 3 ? LiveCityCatalog.Kind.Tree : roll < 5 ? LiveCityCatalog.Kind.Plant : roll == 5 ? LiveCityCatalog.Kind.Rock
            : roll == 6 ? LiveCityCatalog.Kind.Billboard : roll == 7 ? LiveCityCatalog.Kind.Bench : LiveCityCatalog.Kind.Pedestrian;
        int count = 0;
        foreach (var entry in catalog.entries) if (entry != null && entry.kind == kind) count++;
        if (count == 0) return null;
        int chosen = (int)((seed / 10) % (uint)count);
        foreach (var entry in catalog.entries) if (entry != null && entry.kind == kind && chosen-- == 0) return entry;
        return null;
    }

    bool Clear(Vector3 position, float radius, OSMBuildingMeshBuilder buildings)
    {
        if (index.Contains(position, radius + .8f) || buildings.OverlapsBuilding(position, radius + .5f)) return false;
        // Probe above the flat road ground; includes existing shelters and fuel props, not ground slabs.
        return Physics.OverlapBoxNonAlloc(position + Vector3.up * 1.5f, new Vector3(radius, 1f, radius), overlaps,
            Quaternion.identity, ~0, QueryTriggerInteraction.Ignore) == 0;
    }

    void Update()
    {
        foreach (var item in active.Values)
        {
            if (item.go == null) continue;
            float t = Time.time + item.phase;
            if (item.entry.kind == LiveCityCatalog.Kind.Pedestrian)
            {
                float along = Mathf.PingPong(t * 1.1f, 10f) - 5f;
                Vector3 direction = (Mathf.Repeat(t * 1.1f, 20f) < 10f ? 1f : -1f) * item.direction;
                item.go.transform.SetPositionAndRotation(item.center + item.direction * along, Quaternion.LookRotation(direction));
            }
            else if (item.entry.kind == LiveCityCatalog.Kind.Tree || item.entry.kind == LiveCityCatalog.Kind.Plant)
                item.go.transform.rotation = item.rotation * Quaternion.Euler(Mathf.Sin(t * .8f) * .35f, 0, Mathf.Sin(t * .6f) * .3f);
        }
    }

    public static uint PlacementSeed(long way, Vector3 point, int side)
    {
        unchecked
        {
            uint hash = (uint)way ^ (uint)(way >> 32);
            hash = (hash ^ (uint)Mathf.RoundToInt(point.x * 10)) * 16777619;
            hash = (hash ^ (uint)Mathf.RoundToInt(point.z * 10)) * 16777619;
            return (hash ^ (uint)side) * 16777619;
        }
    }
    static float FlatDistanceSquared(Vector3 a, Vector3 b) { a.y = b.y = 0; return (a - b).sqrMagnitude; }
    public static bool OverlapsPolygon(Vector3 point, float radius, IList<Vector3> polygon)
    {
        if (polygon == null || polygon.Count < 3) return false;
        point.y = 0;
        bool inside = false;
        for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
        {
            Vector3 a = polygon[i], b = polygon[j]; a.y = b.y = 0;
            if ((point - RoadSpawnResolver.ProjectOnSegment(point, a, b)).sqrMagnitude <= radius * radius) return true;
            if ((a.z > point.z) != (b.z > point.z) && point.x < (b.x - a.x) * (point.z - a.z) / (b.z - a.z) + a.x) inside = !inside;
        }
        return inside;
    }
}
