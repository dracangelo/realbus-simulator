using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Bounded, deterministic ambient scenery along loaded roads. Never owns road physics.</summary>
public class LiveCityStreamer : MonoBehaviour
{
    public LiveCityCatalog catalog;
    public float loadRadius = 250f;
    public float unloadRadius = 300f;

    // Budgets and spacing drive how much street dressing exists. They are raised
    // well above the original values so the city reads as populated and planted:
    // placement is bounded per frame, so denser dressing costs frame time
    // gradually rather than all at once.
    public int maxProps = 2400;
    public int triangleBudget = 9000000;
    public int maxWalkers = 40;
    [Tooltip("Distance between placement samples along a road. Smaller = denser.")]
    [Min(2f)] public float spacing = 3.4f;
    [Tooltip("Models placed per frame while a street fills. Higher fills faster and costs more frame time.")]
    [Min(1)] public int instancesPerFrame = 10;
    [Tooltip("Chance a kerb sample plants the first row of greenery behind it, so streets have depth instead of a single line of trees.")]
    [Range(0f, 1f)] public float secondRowChance = 0.9f;
    [Tooltip("Extra distance behind the kerb row for the first planting row, in metres.")]
    public float secondRowOffset = 3.6f;
    [Tooltip("How many planting rows may stand behind the kerb row, each half as likely as the one in front of it.")]
    [Range(0, 4)] public int deepRowCount = 4;

    [Header("Open ground")]
    [Tooltip("Side of one open-ground cell, in metres. The scatter plants inside these, not along a road.")]
    [Min(6f)] public float scatterCellSize = 26f;
    [Tooltip("Greenery spots offered per open-ground cell.")]
    [Range(1, 6)] public int scatterPerCell = 2;
    [Tooltip("Chance an offered spot is actually planted, so greenery clumps like parks and verges instead of covering the ground evenly.")]
    [Range(0f, 1f)] public float scatterChance = 0.5f;
    [Tooltip("Most greenery props standing away from any road at once.")]
    public int maxScatterProps = 700;

    const float FirstSampleOffset = 12f;
    /// <summary>Key offset that keeps a deeper-row prop distinct from the kerb sample it grew from.</summary>
    const int SecondRowKeyOffset = 1000000;
    /// <summary>Side value reserved for scattered greenery, so it cannot collide with a kerb sample.</summary>
    const int ScatterSide = 0;
    /// <summary>Triangles at which a scenery model still carries its full share of the planting.</summary>
    public const int GreeneryTriangleBudget = 6000;

    /// <summary>
    /// A stable per-instance size multiplier for greenery. The packs contain only a
    /// handful of tree and shrub models, and a street planted with the same tree at
    /// the same size reads as a copy-paste however many are placed; the same four
    /// models at different sizes read as an avenue.
    /// </summary>
    public static float ScaleFor(uint seed, LiveCityCatalog.Kind kind)
    {
        switch (kind)
        {
            case LiveCityCatalog.Kind.Tree: return 0.78f + seed % 55 * 0.01f;
            case LiveCityCatalog.Kind.Plant: return 0.80f + seed % 70 * 0.01f;
            case LiveCityCatalog.Kind.Rock: return 0.70f + seed % 90 * 0.01f;
            default: return 1f;
        }
    }
    /// <summary>
    /// Share of the planting one scenery model asks for. The packs hold three trees of
    /// 40 000-55 000 triangles and one of 5 000; planted evenly, the first three spend the
    /// whole scenery budget on a couple of hundred trees, which is why a street looked bare
    /// however high the prop count went. Cheap models carry the planting, dense ones appear.
    /// </summary>
    public static float ModelWeight(int triangles)
        => Mathf.Clamp(GreeneryTriangleBudget / (float)Mathf.Max(1, triangles), .05f, 1f);

    /// <summary>
    /// Deterministic weighted pick among the models of one kind. Pure, so the choice can be
    /// checked without a scene.
    /// </summary>
    public static LiveCityCatalog.Entry PickWeighed(IList<LiveCityCatalog.Entry> entries, LiveCityCatalog.Kind kind, uint seed)
    {
        if (entries == null) return null;
        float total = 0f;
        LiveCityCatalog.Entry last = null;
        for (int i = 0; i < entries.Count; i++)
        {
            var entry = entries[i];
            if (entry == null || entry.prefab == null || entry.kind != kind) continue;
            total += ModelWeight(entry.triangles);
            last = entry;
        }
        if (last == null || total <= 0f) return null;

        float roll = seed % 1000 / 1000f * total;
        float accumulated = 0f;
        for (int i = 0; i < entries.Count; i++)
        {
            var entry = entries[i];
            if (entry == null || entry.prefab == null || entry.kind != kind) continue;
            accumulated += ModelWeight(entry.triangles);
            if (roll <= accumulated) return entry;
        }
        return last;
    }

    /// <summary>
    /// Scrambles a spot's seed. Consecutive seeds differ in their low bits only, and both
    /// rules below divide by constants, so without this every spot in a run of cells lands
    /// on the same point and every one of them is planted.
    /// </summary>
    public static uint MixScatterSeed(uint seed)
    {
        seed ^= seed >> 16; seed *= 0x7feb352du;
        seed ^= seed >> 15; seed *= 0x846ca68bu;
        seed ^= seed >> 16;
        return seed;
    }

    /// <summary>Where inside its open-ground cell a spot sits, as a fraction of the cell. Pure.</summary>
    public static Vector2 ScatterOffset(uint seed)
    {
        uint mixed = MixScatterSeed(seed);
        return new Vector2(mixed % 977 / 977f, mixed / 977 % 977 / 977f);
    }

    /// <summary>True when an open-ground spot is planted rather than left as bare ground. Pure.</summary>
    public static bool ScatterPlants(uint seed, float chance)
        => MixScatterSeed(seed) / 13 % 100 < Mathf.Clamp01(chance) * 100f;

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
        public bool scattered;
    }
    readonly Dictionary<(RoadSurfaceIndex.Segment, int, int), Spawned> active = new Dictionary<(RoadSurfaceIndex.Segment, int, int), Spawned>();
    /// <summary>One shared placement key per open-ground cell, so a scattered prop knows where it came from.</summary>
    readonly Dictionary<Vector2Int, RoadSurfaceIndex.Segment> scatterCells = new Dictionary<Vector2Int, RoadSurfaceIndex.Segment>();
    /// <summary>Way id reserved for the open-ground keys; no real road can own it.</summary>
    const long ScatterWayId = long.MinValue;
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
        active.Clear(); scatterCells.Clear(); triangles = walkers = 0; index = null;
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
                active.Clear(); scatterCells.Clear(); triangles = walkers = 0;
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
                || (pair.Value.entry.kind == LiveCityCatalog.Kind.Pedestrian && FlatDistanceSquared(pair.Value.center, player) > 140f * 140f)
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
            int from = Mathf.Max(0, Mathf.FloorToInt((projection - loadRadius) / spacing));
            int to = Mathf.Min(Mathf.FloorToInt((length - FirstSampleOffset) / spacing), Mathf.CeilToInt((projection + loadRadius) / spacing));
            for (int sample = from; sample <= to; sample++)
            for (int side = -1; side <= 1; side += 2)
            {
                Vector3 center = segment.a + direction * (FirstSampleOffset + sample * spacing);
                if (FlatDistanceSquared(center, player) > loadRadius * loadRadius || active.ContainsKey((segment, sample, side))) continue;
                candidates.Add(new Candidate { segment = segment, sample = sample, side = side, center = center, direction = direction,
                    seed = PlacementSeed(segment.wayId, center, side) });
            }
        }
        candidates.Sort((a, b) => FlatDistanceSquared(a.center, player).CompareTo(FlatDistanceSquared(b.center, player)));
        int examined = 0;
        int placedThisFrame = 0;
        foreach (var candidate in candidates)
        {
            if (viewer == null || FlatDistanceSquared(viewer.position, player) > 30f * 30f) yield break;
            if (active.Count >= Mathf.Max(0, maxProps)) break;
            if (++examined % 4 == 0) yield return null; // Bound rejection/clearance work as well as instantiation.
            var entry = SelectEntry(candidate.seed);
            if (entry == null || entry.prefab == null || triangles + entry.triangles > triangleBudget) continue;
            bool walking = entry.kind == LiveCityCatalog.Kind.Pedestrian;
            if (walking && (walkers >= maxWalkers || FlatDistanceSquared(candidate.center, player) > 120f * 120f))
            {
                entry = SelectVegetationEntry(candidate.seed);
                if (entry == null || entry.prefab == null || triangles + entry.triangles > triangleBudget) continue;
                walking = false;
            }
            float scale = ScaleFor(candidate.seed, entry.kind);
            float radius = Mathf.Max(.4f, new Vector2(entry.bounds.extents.x, entry.bounds.extents.z).magnitude) * scale;
            Vector3 right = Vector3.Cross(Vector3.up, candidate.direction);
            Vector3 position = candidate.center + right * candidate.side * (candidate.segment.halfWidth + radius + 1.8f);
            position.y = .08f;
            if (!Clear(position, radius, buildings)) continue;
            if (walking)
            {
                bool clear = true;
                for (int step = -5; step <= 5; step++)
                    if (!Clear(position + candidate.direction * step, radius, buildings)) { clear = false; break; }
                if (!clear)
                {
                    entry = SelectVegetationEntry(candidate.seed);
                    if (entry == null || entry.prefab == null || triangles + entry.triangles > triangleBudget) continue;
                    walking = false;
                    scale = ScaleFor(candidate.seed, entry.kind);
                    radius = Mathf.Max(.4f, new Vector2(entry.bounds.extents.x, entry.bounds.extents.z).magnitude) * scale;
                }
            }
            bool space = true;
            foreach (var item in active.Values)
                if (FlatDistanceSquared(position, item.center) < Mathf.Pow(radius + item.radius + (walking || item.entry.kind == LiveCityCatalog.Kind.Pedestrian ? 5f : 0.8f), 2)) { space = false; break; }
            if (!space) continue;
            Quaternion rotation = Quaternion.LookRotation(-right * candidate.side);
            if (entry.kind == LiveCityCatalog.Kind.Tree || entry.kind == LiveCityCatalog.Kind.Plant || entry.kind == LiveCityCatalog.Kind.Rock)
                rotation = Quaternion.Euler(0, candidate.seed % 360, 0);
            var go = Instantiate(entry.prefab, position, rotation, transform);
            go.transform.localScale = Vector3.one * scale;
            active[(candidate.segment, candidate.sample, candidate.side)] = new Spawned { go = go, entry = entry, center = position,
                direction = candidate.direction, rotation = rotation, radius = radius, phase = (candidate.seed % 1000) * .01f };
            triangles += entry.triangles;
            if (walking) walkers++;
            if (++placedThisFrame >= Mathf.Max(1, instancesPerFrame)) { placedThisFrame = 0; yield return null; }

            // Planting rows behind the kerb make a street read as planted rather than
            // as a single line of trees, and they fill the strip between the road and
            // the building line that used to stay bare. Each row back is half as
            // likely as the one in front of it, so a verge thins out toward the walls.
            float depth = secondRowOffset;
            for (int deepRow = 0; deepRow < Mathf.Clamp(deepRowCount, 0, 4); deepRow++)
            {
                if (candidate.seed % 100 >= secondRowChance * 100f * Mathf.Pow(.5f, deepRow)) break;
                uint deepSeed = candidate.seed + 991u * (uint)(deepRow + 1);
                var deep = SelectVegetationEntry(deepSeed);
                if (deep == null || deep.prefab == null || triangles + deep.triangles > triangleBudget) break;
                float deepScale = ScaleFor(deepSeed, deep.kind);
                float deepRadius = Mathf.Max(.4f, new Vector2(deep.bounds.extents.x, deep.bounds.extents.z).magnitude) * deepScale;
                Vector3 deepPosition = candidate.center + right * candidate.side * (candidate.segment.halfWidth + deepRadius + 1.8f + depth);
                deepPosition.y = .08f;
                depth += secondRowOffset + deepRadius * .5f;
                int deepKey = candidate.sample + SecondRowKeyOffset * (deepRow + 1);
                if (active.ContainsKey((candidate.segment, deepKey, candidate.side)) || !Clear(deepPosition, deepRadius, buildings)) break;
                bool deepSpace = true;
                foreach (var item in active.Values)
                    if (FlatDistanceSquared(deepPosition, item.center) < Mathf.Pow(deepRadius + item.radius + .8f, 2)) { deepSpace = false; break; }
                if (!deepSpace) break;
                Quaternion deepRotation = Quaternion.Euler(0, deepSeed % 360, 0);
                var deepGo = Instantiate(deep.prefab, deepPosition, deepRotation, transform);
                deepGo.transform.localScale = Vector3.one * deepScale;
                active[(candidate.segment, deepKey, candidate.side)] = new Spawned { go = deepGo, entry = deep, center = deepPosition,
                    direction = candidate.direction, rotation = deepRotation, radius = deepRadius, phase = (deepSeed % 1000) * .01f };
                triangles += deep.triangles;
                if (++placedThisFrame >= Mathf.Max(1, instancesPerFrame)) { placedThisFrame = 0; yield return null; }
            }
        }

        PruneScatterCells(player);
        yield return Scatter(player, viewer, buildings);
    }

    /// <summary>Forgets open-ground keys the player has left far enough behind that none of their props can return.</summary>
    void PruneScatterCells(Vector3 player)
    {
        if (scatterCells.Count < 64) return;
        var stale = new List<Vector2Int>();
        float limit = Mathf.Max(unloadRadius + scatterCellSize * 2f, loadRadius + 60f);
        foreach (var pair in scatterCells)
        {
            Vector3 center = new Vector3(pair.Key.x * scatterCellSize, 0f, pair.Key.y * scatterCellSize) + Vector3.one * scatterCellSize * .5f;
            if (FlatDistanceSquared(center, player) > limit * limit) stale.Add(pair.Key);
        }
        foreach (var key in stale) scatterCells.Remove(key);
    }

    LiveCityCatalog.Entry SelectEntry(uint seed)
    {
        int roll = (int)(seed % 100);
        LiveCityCatalog.Kind kind;
        // Vegetation first, because it is what makes a street look lived in and it is
        // what the user asked to see more of: 62% Trees, 31% Plants/Shrubs, then the
        // street furniture that keeps a pavement from being only greenery.
        if (roll < 62) kind = LiveCityCatalog.Kind.Tree;
        else if (roll < 93) kind = LiveCityCatalog.Kind.Plant;
        else if (roll < 94) kind = LiveCityCatalog.Kind.Pedestrian;
        else if (roll < 97) kind = LiveCityCatalog.Kind.Bench;
        else if (roll < 99) kind = LiveCityCatalog.Kind.Billboard;
        else kind = LiveCityCatalog.Kind.Rock;

        var picked = PickWeighed(catalog.entries, kind, seed / 10);
        return picked != null ? picked : SelectVegetationEntry(seed);
    }

    LiveCityCatalog.Entry SelectVegetationEntry(uint seed)
    {
        LiveCityCatalog.Kind kind = (seed % 2 == 0) ? LiveCityCatalog.Kind.Tree : LiveCityCatalog.Kind.Plant;
        return PickWeighed(catalog.entries, kind, seed / 13);
    }

    /// <summary>
    /// Plants the ground between the roads: parks, verges, back gardens and the empty
    /// ground at the edge of the built-up area. Roadside planting cannot reach most of it,
    /// because a canopy needs metres of clearance on every side and the infill fills the
    /// pavement right up to the kerb, so a tree offered to the kerb row is rejected by the
    /// building it would grow into.
    /// </summary>
    IEnumerator Scatter(Vector3 player, Transform viewer, OSMBuildingMeshBuilder buildings)
    {
        var origin = ScatterCell(player);
        int ringCount = Mathf.CeilToInt(loadRadius / scatterCellSize);
        int plantedThisFrame = 0;
        int scattered = ScatterCount();
        for (int ring = 0; ring <= ringCount; ring++)
        for (int dx = -ring; dx <= ring; dx++)
        for (int dz = -ring; dz <= ring; dz++)
        {
            if (Mathf.Max(Mathf.Abs(dx), Mathf.Abs(dz)) != ring) continue;
            if (scattered >= maxScatterProps || active.Count >= Mathf.Max(0, maxProps)) yield break;
            var cell = origin + new Vector2Int(dx, dz);
            Vector3 corner = new Vector3(cell.x * scatterCellSize, 0f, cell.y * scatterCellSize);
            if ((corner + Vector3.one * scatterCellSize * .5f - player).sqrMagnitude > loadRadius * loadRadius) continue;
            var segment = ScatterSegment(cell);
            for (int slot = 0; slot < Mathf.Max(1, scatterPerCell); slot++)
            {
                if (viewer == null || FlatDistanceSquared(viewer.position, player) > 30f * 30f) yield break;
                if (active.ContainsKey((segment, slot, ScatterSide))) continue;
                if (scattered >= maxScatterProps || active.Count >= Mathf.Max(0, maxProps)) yield break;
                Vector2 offset = ScatterOffset(unchecked((uint)(cell.x * 73856093 + cell.y * 19349663 + slot * 83492791)));
                Vector3 position = corner + new Vector3(offset.x * scatterCellSize, .08f, offset.y * scatterCellSize);
                uint seed = PlacementSeed(unchecked((long)cell.x * 73856093L ^ (long)cell.y * 19349663L), position, slot);
                if (!ScatterPlants(seed, scatterChance)) continue;
                var entry = SelectVegetationEntry(seed);
                if (entry == null || entry.prefab == null || triangles + entry.triangles > triangleBudget) continue;
                float scale = ScaleFor(seed, entry.kind);
                float radius = Mathf.Max(.4f, new Vector2(entry.bounds.extents.x, entry.bounds.extents.z).magnitude) * scale;
                // A scattered prop belongs on open ground, never on asphalt or in a building.
                if (!Clear(position, radius, buildings)) continue;
                bool space = true;
                foreach (var item in active.Values)
                    if (FlatDistanceSquared(position, item.center) < Mathf.Pow(radius + item.radius + .8f, 2)) { space = false; break; }
                if (!space) continue;

                Quaternion rotation = Quaternion.Euler(0, seed % 360, 0);
                var go = Instantiate(entry.prefab, position, rotation, transform);
                go.transform.localScale = Vector3.one * scale;
                active[(segment, slot, ScatterSide)] = new Spawned { go = go, entry = entry, center = position, direction = Vector3.forward,
                    rotation = rotation, radius = radius, phase = (seed % 1000) * .01f, scattered = true };
                triangles += entry.triangles;
                scattered++;
                if (++plantedThisFrame >= Mathf.Max(1, instancesPerFrame) / 2) { plantedThisFrame = 0; yield return null; }
            }
        }
    }

    int ScatterCount()
    {
        int count = 0;
        foreach (var item in active.Values) if (item.scattered) count++;
        return count;
    }

    /// <summary>One shared index segment per open-ground cell, so the placement keys stay stable.</summary>
    RoadSurfaceIndex.Segment ScatterSegment(Vector2Int cell)
    {
        if (scatterCells.TryGetValue(cell, out var cached)) return cached;
        Vector3 corner = new Vector3(cell.x * scatterCellSize, 0f, cell.y * scatterCellSize);
        var segment = new RoadSurfaceIndex.Segment { a = corner, b = corner + Vector3.forward * scatterCellSize, halfWidth = 0f, wayId = ScatterWayId };
        scatterCells[cell] = segment;
        return segment;
    }

    Vector2Int ScatterCell(Vector3 point)
        => new Vector2Int(Mathf.FloorToInt(point.x / scatterCellSize), Mathf.FloorToInt(point.z / scatterCellSize));

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
                float along = Mathf.PingPong(t * 1.1f, 12f) - 6f;
                Vector3 direction = (Mathf.Repeat(t * 1.1f, 24f) < 12f ? 1f : -1f) * item.direction;
                Quaternion walkRot = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 0, Mathf.Sin(t * 7f) * 1.5f);
                item.go.transform.SetPositionAndRotation(item.center + item.direction * along, walkRot);
            }
            else if (item.entry.kind == LiveCityCatalog.Kind.Tree || item.entry.kind == LiveCityCatalog.Kind.Plant)
                item.go.transform.rotation = item.rotation * Quaternion.Euler(Mathf.Sin(t * .8f) * .4f, 0, Mathf.Sin(t * .6f) * .35f);
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
