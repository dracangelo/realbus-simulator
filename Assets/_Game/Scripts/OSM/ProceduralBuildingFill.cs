using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Fictional roadside infill; kept separate from real map footprints.</summary>
public class ProceduralBuildingFill : MonoBehaviour
{
    public float loadRadius = 380f;
    public float unloadRadius = 520f;
    /// <summary>
    /// How many fictional buildings may stand at once. A block is filled until this runs
    /// out, and every cell that was cut short is walked again, so the number decides how
    /// far the built-up area reaches rather than where it stops.
    /// </summary>
    public int maxBuildings = 1000;
    /// <summary>
    /// Milliseconds of infill work one frame may spend placing buildings. The walk used to
    /// raise a single building per frame, which leaves a district looking half-built for the
    /// first ten seconds after it streams in - and a half-built street is a gap in it.
    /// </summary>
    public float millisecondsPerFrame = 6f;
    /// <summary>Ceiling on buildings raised in one frame, so a fast machine cannot spike.</summary>
    [Min(1)] public int maxBuildingsPerFrame = 24;
    const float CellSize = 100f;
    /// <summary>Grid used only to keep the neighbour-spacing test near constant time as a block fills.</summary>
    const float SpacingCellSize = 25f;
    /// <summary>Where along a segment the first front-row building sits.</summary>
    public const float firstSampleOffset = 8f;

    /// <summary>
    /// Distance from the road centre to the back of the last row with the current
    /// settings: the number that decides whether a block is covered or merely
    /// outlined along the kerb.
    /// </summary>
    public float BlockCoverageMeters(float roadWidth = 8f, float averageDepth = 15f)
        => roadWidth * .5f + kerbGap + Mathf.Max(1, rowsPerSide) * (averageDepth + Mathf.Max(0f, rowGap));

    /// <summary>
    /// Scrambles a candidate hash. Consecutive sample hashes differ only in their low
    /// bits, so dividing them by a constant collapsed the block to one palette; mixing
    /// first makes every rule below depend on the whole id.
    /// </summary>
    public static uint Mix(uint value)
    {
        value ^= value >> 16; value *= 0x7feb352du;
        value ^= value >> 15; value *= 0x846ca68bu;
        value ^= value >> 16;
        return value;
    }

    /// <summary>Width along the street: 9-17 m, so a block has differently sized units.</summary>
    public static float InfillWidth(uint hash) => 9f + Mix(hash) % 9;

    /// <summary>Depth behind the street: 9-21 m.</summary>
    public static float InfillDepth(uint hash) => 9f + Mix(hash * 3u + 7u) % 13;

    /// <summary>
    /// What an infill building looks like. Housing dominates with a strong minority of
    /// apartments, shops and offices, so the infill matches the palette of the mapped
    /// city instead of being a street of identical houses.
    /// </summary>
    public static OSMBuildingMeshBuilder.BuildingKind InfillKind(uint hash)
    {
        uint roll = Mix(hash * 11u + 29u) % 100;
        if (roll < 56) return OSMBuildingMeshBuilder.BuildingKind.House;
        if (roll < 74) return OSMBuildingMeshBuilder.BuildingKind.Apartment;
        if (roll < 92) return OSMBuildingMeshBuilder.BuildingKind.Commercial;
        return OSMBuildingMeshBuilder.BuildingKind.Generic;
    }

    /// <summary>
    /// Height for an infill building, in metres, from its kind and hash: two to three
    /// storeys of housing, four to nine of apartments, three to eight of commercial,
    /// and a short generic band in between. The old flat 4-13 m range gave every block
    /// the same silhouette.
    /// </summary>
    public static float InfillHeight(uint hash, OSMBuildingMeshBuilder.BuildingKind kind)
    {
        uint mixed = Mix(hash * 5u + 17u);
        int storeys;
        switch (kind)
        {
            case OSMBuildingMeshBuilder.BuildingKind.Commercial: storeys = 3 + (int)(mixed % 6); break;
            case OSMBuildingMeshBuilder.BuildingKind.Apartment:  storeys = 4 + (int)(mixed % 6); break;
            case OSMBuildingMeshBuilder.BuildingKind.House:      storeys = 2 + (int)(mixed % 2); break;
            default:                                            storeys = 2 + (int)(mixed % 4); break;
        }
        float floorHeight = kind == OSMBuildingMeshBuilder.BuildingKind.Commercial ? 3.8f : 3.2f;
        return storeys * floorHeight;
    }

    [Header("Block shape")]
    [Tooltip("Gap between neighbours along the street. Frontages nearly touch, so a street reads as a continuous row instead of detached boxes.")]
    public float frontGap = 0.8f;
    [Tooltip("Distance from the road edge to the first row's front face. The front row belongs on the pavement, not behind a yard.")]
    public float kerbGap = 1.2f;
    [Tooltip("How many rows deep a block is filled. Filling stops early at the next street, a depot or a mapped footprint.")]
    [Range(1, 8)] public int rowsPerSide = 6;
    [Tooltip("Gap left behind each row for courtyards and service lanes.")]
    public float rowGap = 0.5f;
    [Tooltip("Smallest distance between two infill buildings.")]
    public float minBuildingSpacing = 5f;
    [Tooltip("Clearance kept around a mapped OSM footprint, so infill does not cut into real buildings.")]
    public float mappedClearance = 6f;

    struct Segment { public Vector3 a, b; public float width; public long id; public bool fill; }
    readonly Dictionary<Vector2Int, List<Segment>> roads = new Dictionary<Vector2Int, List<Segment>>();
    readonly Dictionary<Vector2Int, List<GameObject>> cells = new Dictionary<Vector2Int, List<GameObject>>();
    readonly Dictionary<GameObject, Vector3> centers = new Dictionary<GameObject, Vector3>();
    readonly Dictionary<Vector2Int, List<GameObject>> placedPerCell = new Dictionary<Vector2Int, List<GameObject>>();
    /// <summary>Cells the building budget cut short, so a later pass can finish them.</summary>
    readonly HashSet<Vector2Int> incomplete = new HashSet<Vector2Int>();
    readonly List<Vector3> neighbours = new List<Vector3>();
    OSMBuildingMeshBuilder builder;
    public int ActiveCount => centers.Count;

    void OnEnable() { StartCoroutine(Stream()); }

    void OnDisable()
    {
        StopAllCoroutines();
        if (builder != null)
            foreach (var item in centers.Keys) builder.ReleaseSyntheticBuilding(item);
        centers.Clear(); cells.Clear(); roads.Clear(); placedPerCell.Clear(); incomplete.Clear();
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
        centers.Clear(); cells.Clear(); placedPerCell.Clear(); incomplete.Clear();
    }

    static Vector2Int Cell(Vector3 p) => new Vector2Int(Mathf.FloorToInt(p.x / CellSize), Mathf.FloorToInt(p.z / CellSize));
    public static bool CanFillRoad(string type) => type == "residential" || type == "living_street" || type == "unclassified" ||
        type == "tertiary" || type == "secondary" || type == "primary";

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
            foreach (var item in pair.Value) { Forget(item); builder.ReleaseSyntheticBuilding(item); }
            remove.Add(pair.Key);
        }
        foreach (var key in remove) { cells.Remove(key); incomplete.Remove(key); }
        var current = Cell(player);
        int radius = Mathf.CeilToInt(loadRadius / CellSize);
        float deadline = Time.realtimeSinceStartup + Mathf.Max(1f, millisecondsPerFrame) / 1000f;
        int placedThisFrame = 0;
        for (int ring = 0; ring <= radius; ring++)
        for (int dx = -ring; dx <= ring; dx++)
        for (int dz = -ring; dz <= ring; dz++)
        {
            if (Mathf.Max(Mathf.Abs(dx), Mathf.Abs(dz)) != ring) continue;
            var key = current + new Vector2Int(dx, dz);
            Vector3 cellCenter = new Vector3((key.x + .5f) * CellSize, player.y, (key.y + .5f) * CellSize);
            if ((cellCenter - player).sqrMagnitude > loadRadius * loadRadius) continue;
            if (!roads.TryGetValue(key, out var segments)) continue;
            // A cell that ran out of budget is finished on a later pass, nearest first,
            // because a cell marked done while it was half-built stays a hole in that
            // block for the whole session however long the player stands next to it.
            bool waiting = incomplete.Contains(key);
            if (cells.ContainsKey(key) && !waiting) continue;
            if (centers.Count >= maxBuildings) { incomplete.Add(key); continue; }
            if (!cells.TryGetValue(key, out var created)) cells[key] = created = new List<GameObject>();
            bool filled = true;
            foreach (var segment in segments)
            {
                if (!segment.fill) continue;
                Vector3 direction = segment.b - segment.a; direction.y = 0;
                float length = direction.magnitude;
                if (length < 18f) continue;
                direction /= length;
                Vector3 right = Vector3.Cross(Vector3.up, direction);
                uint segmentSeed = unchecked((uint)(segment.id * 131));
                for (int side = -1; side <= 1; side += 2)
                {
                    // A block is filled row by row walking outward from the kerb. The walk
                    // stops as soon as a row meets the next street, a depot yard or a
                    // mapped footprint, because nothing behind that point can fit either.
                    float rowOffset = segment.width * .5f + kerbGap + .05f;
                    for (int row = 0; row < Mathf.Max(1, rowsPerSide); row++)
                    {
                        float rowDepth = 0f;
                        // Rows start at different points along the street, so their joints
                        // do not line up into a visible grid.
                        float along = firstSampleOffset + (row % 2 == 0 ? 0f : 3.2f);
                        int slot = 0;
                        while (along < length - firstSampleOffset)
                        {
                            if (centers.Count >= maxBuildings) { filled = false; break; }
                            uint hash = unchecked(segmentSeed + (uint)(slot * 17) + (uint)(slot * slot * 3) + (uint)(side * 7) + (uint)(row * 101));
                            slot++;
                            float width = InfillWidth(hash), depth = InfillDepth(hash);
                            Vector3 center = segment.a + direction * (along + width * .5f) + right * side * (rowOffset + depth * .5f);
                            along += width + frontGap;
                            center.y = builder.buildingBaseYOffset;
                            // A building whose centre falls just over the cell line is built
                            // here instead of being dropped, which is what otherwise left a
                            // one-building hole in a row every hundred metres. If the owning
                            // cell is already filled, that cell owns the spot and this pass
                            // leaves it alone.
                            if ((Cell(center) != key && cells.ContainsKey(Cell(center))) || builder.HasMappedFootprint(center, mappedClearance)) continue;
                            if (!Clear(center, direction, width, depth, segments, out bool blockEnded))
                            {
                                // A street crossing the row ends the block; the row's own
                                // street, or one parallel to it, only rules out this spot.
                                if (blockEnded) break;
                                continue;
                            }
                            var footprint = new List<Vector3> { center - direction * width * .5f - right * depth * .5f,
                                center + direction * width * .5f - right * depth * .5f, center + direction * width * .5f + right * depth * .5f,
                                center - direction * width * .5f + right * depth * .5f };
                            var kind = InfillKind(hash);
                            var instance = builder.CreateSyntheticBuilding(footprint, InfillHeight(hash, kind), -(long)hash - 1, kind);
                            if (instance != null)
                            {
                                created.Add(instance);
                                centers[instance] = center;
                                Index(instance, center);
                                rowDepth = Mathf.Max(rowDepth, depth);
                                if (++placedThisFrame >= Mathf.Max(1, maxBuildingsPerFrame) ||
                                    Time.realtimeSinceStartup >= deadline)
                                {
                                    placedThisFrame = 0;
                                    deadline = Time.realtimeSinceStartup + Mathf.Max(1f, millisecondsPerFrame) / 1000f;
                                    yield return null;
                                }
                            }
                        }
                        if (!filled) break;
                        // A row that placed nothing has hit the far side of the block.
                        if (rowDepth <= 0f) break;
                        rowOffset += rowDepth + Mathf.Max(0f, rowGap);
                    }
                    if (!filled) break;
                }
                if (!filled) break;
            }
            if (filled) incomplete.Remove(key); else incomplete.Add(key);
        }
    }

    /// <summary>
    /// True when a spot can hold a building. The clearance test measures the building's
    /// own footprint against every nearby road, not its centre: the old centre test
    /// compared the distance to the road with the footprint's half diagonal, which
    /// rejected every candidate and left the blocks empty.
    /// </summary>
    bool Clear(Vector3 center, Vector3 along, float width, float depth, List<Segment> segments, out bool blockEnded)
    {
        blockEnded = false;
        float radius = Mathf.Sqrt(width * width + depth * depth) * .5f;
        // A depot is a workplace, not a block: no infill may stand in the yard.
        if (BusDepotManager.IsInsideDepot(center, radius + 2f)) { blockEnded = true; return false; }

        float halfAlong = width * .5f, halfAcross = depth * .5f;
        foreach (var segment in segments)
        {
            float clearance = segment.width * .5f + kerbGap;
            if (RectangleDistanceToSegment(center, along, halfAlong, halfAcross, segment.a, segment.b) >= clearance) continue;

            // A street running across the row is the end of the block, and nothing behind
            // it can fit either. A street parallel to the row - the row's own street, or the
            // next one over - only rules out the spot the walk is standing on. Treating the
            // second case as the end of the block is what abandoned the rest of a frontage
            // the moment the walk grazed a kerb line, leaving holes in the row.
            if (!EndsBlock(along, segment.a, segment.b)) continue;

            blockEnded = true;
            return false;
        }

        // Neighbours are read from a small grid rather than from every building placed
        // so far, which keeps the test cheap now that blocks are filled tightly.
        float spacing = Mathf.Max(3f, minBuildingSpacing);
        foreach (var existing in Neighbours(center))
            if ((existing - center).sqrMagnitude < spacing * spacing) return false;
        return !Physics.CheckBox(center + Vector3.up * 5f, new Vector3(radius, 4f, radius), Quaternion.identity, ~0, QueryTriggerInteraction.Ignore);
    }

    /// <summary>
    /// True when a street that blocks one building spot ends the whole block rather than
    /// just that spot: a street running across the row does, and one parallel to it does not.
    /// </summary>
    public static bool EndsBlock(Vector3 rowDirection, Vector3 segmentStart, Vector3 segmentEnd)
    {
        Vector3 blocking = segmentEnd - segmentStart;
        blocking.y = 0f;
        if (blocking.sqrMagnitude <= .01f) return true;
        return Mathf.Abs(Vector3.Dot(blocking.normalized, rowDirection)) <= .75f;
    }

    void Index(GameObject instance, Vector3 center)
    {
        var key = SpacingCell(center);
        if (!placedPerCell.TryGetValue(key, out var list)) placedPerCell[key] = list = new List<GameObject>();
        list.Add(instance);
    }

    void Forget(GameObject instance)
    {
        if (!centers.TryGetValue(instance, out var center)) { centers.Remove(instance); return; }
        centers.Remove(instance);
        if (placedPerCell.TryGetValue(SpacingCell(center), out var list)) list.Remove(instance);
    }

    static Vector2Int SpacingCell(Vector3 p) => new Vector2Int(Mathf.FloorToInt(p.x / SpacingCellSize), Mathf.FloorToInt(p.z / SpacingCellSize));

    /// <summary>Centres already placed within one grid cell of the candidate.</summary>
    List<Vector3> Neighbours(Vector3 center)
    {
        neighbours.Clear();
        var key = SpacingCell(center);
        for (int dx = -1; dx <= 1; dx++)
        for (int dz = -1; dz <= 1; dz++)
        {
            if (!placedPerCell.TryGetValue(new Vector2Int(key.x + dx, key.y + dz), out var list)) continue;
            foreach (var item in list)
                if (item != null && centers.TryGetValue(item, out var position)) neighbours.Add(position);
        }
        return neighbours;
    }

    /// <summary>
    /// Ground-plane distance between an oriented rectangle and a road segment. The
    /// closest approach of two disjoint convex shapes is between a vertex of one and
    /// an edge of the other, so testing the four corners against the segment and the
    /// segment's ends against the rectangle covers it exactly. A segment that runs
    /// right through the footprint is the one case those tests cannot see, and there
    /// the distance is zero.
    /// </summary>
    public static float RectangleDistanceToSegment(Vector3 center, Vector3 along, float halfAlong, float halfAcross, Vector3 a, Vector3 b)
    {
        if (SegmentCrossesRectangle(center, along, halfAlong, halfAcross, a, b)) return 0f;
        Vector3 across = Vector3.Cross(Vector3.up, along);
        float best = Mathf.Min(PointToSegmentDistance(center + along * halfAlong + across * halfAcross, a, b),
                               PointToSegmentDistance(center + along * halfAlong - across * halfAcross, a, b));
        best = Mathf.Min(best, PointToSegmentDistance(center - along * halfAlong + across * halfAcross, a, b));
        best = Mathf.Min(best, PointToSegmentDistance(center - along * halfAlong - across * halfAcross, a, b));
        best = Mathf.Min(best, PointToRectangleDistance(a, center, along, halfAlong, halfAcross));
        best = Mathf.Min(best, PointToRectangleDistance(b, center, along, halfAlong, halfAcross));
        return best;
    }

    /// <summary>Ground-plane distance from a point to a segment.</summary>
    public static float PointToSegmentDistance(Vector3 point, Vector3 a, Vector3 b)
    {
        Vector3 nearest = RoadSpawnResolver.ProjectOnSegment(point, a, b);
        return new Vector2(point.x - nearest.x, point.z - nearest.z).magnitude;
    }

    /// <summary>
    /// True when a segment passes through an oriented rectangle. Both of the shapes'
    /// vertices can be far outside the other while the road still cuts straight across
    /// the building, so the segment is clipped against the rectangle's four sides.
    /// </summary>
    public static bool SegmentCrossesRectangle(Vector3 center, Vector3 along, float halfAlong, float halfAcross, Vector3 a, Vector3 b)
    {
        Vector2 start = Local(a), end = Local(b);
        Vector2 delta = end - start;
        float enter = 0f, exit = 1f;
        return Clip(-delta.x, start.x + halfAlong, ref enter, ref exit)
            && Clip(delta.x, halfAlong - start.x, ref enter, ref exit)
            && Clip(-delta.y, start.y + halfAcross, ref enter, ref exit)
            && Clip(delta.y, halfAcross - start.y, ref enter, ref exit);

        Vector2 Local(Vector3 point)
        {
            Vector3 across = Vector3.Cross(Vector3.up, along);
            Vector3 offset = new Vector3(point.x - center.x, 0f, point.z - center.z);
            return new Vector2(Vector3.Dot(offset, along), Vector3.Dot(offset, across));
        }
    }

    /// <summary>Liang-Barsky: narrows the surviving parameter range by one rectangle edge.</summary>
    static bool Clip(float slope, float limit, ref float enter, ref float exit)
    {
        if (Mathf.Abs(slope) < 1e-5f) return limit >= 0f;
        float crossing = limit / slope;
        if (slope < 0f)
        {
            if (crossing > exit) return false;
            if (crossing > enter) enter = crossing;
        }
        else
        {
            if (crossing < enter) return false;
            if (crossing < exit) exit = crossing;
        }
        return true;
    }

    /// <summary>Ground-plane distance from a point to an oriented rectangle; zero inside it.</summary>
    public static float PointToRectangleDistance(Vector3 point, Vector3 center, Vector3 along, float halfAlong, float halfAcross)
    {
        Vector3 across = Vector3.Cross(Vector3.up, along);
        Vector3 delta = new Vector3(point.x - center.x, 0f, point.z - center.z);
        float u = Mathf.Abs(Vector3.Dot(delta, along)) - halfAlong;
        float v = Mathf.Abs(Vector3.Dot(delta, across)) - halfAcross;
        if (u <= 0f && v <= 0f) return 0f;
        return new Vector2(Mathf.Max(0f, u), Mathf.Max(0f, v)).magnitude;
    }

    /// <summary>
    /// Destroys the fictional buildings this streamer already raised inside a
    /// depot yard and forgets them, so the yard is genuinely empty instead of
    /// relying on the new clearance rule alone. Each emptied cell keeps its key
    /// and is not refilled, exactly like every other visited cell.
    /// </summary>
    public int ClearDepotOverlaps(float padding = 1f)
    {
        var doomed = new List<GameObject>();
        foreach (var pair in centers)
            if (pair.Key != null && BusDepotManager.IsInsideDepot(pair.Value, padding)) doomed.Add(pair.Key);

        int removed = 0;
        foreach (var building in doomed)
        {
            Forget(building);
            if (building == null) continue;
            if (builder != null) builder.ReleaseSyntheticBuilding(building); else DestroySafe(building);
            removed++;
        }

        foreach (var list in cells.Values) list.RemoveAll(item => item == null);
        return removed;
    }

    /// <summary>Destroys in play and edit mode; a depot can be cleared outside play mode.</summary>
    static void DestroySafe(GameObject obj)
    {
        if (obj == null) return;
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            DestroyImmediate(obj);
            return;
        }
#endif
        Destroy(obj);
    }
}
