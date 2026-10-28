using System;
using UnityEngine;

/// <summary>
/// Normalized road pieces from the low-poly road pack. The generated OSM ribbon
/// still owns the carriageway and its physics; these modules are used where a
/// flat, textured apron is needed next to gameplay space, such as a fuel-station
/// forecourt. Pivots are bottom-centre and the prefabs carry no colliders.
/// </summary>
public class RoadModuleCatalog : ScriptableObject
{
    public enum Kind { RoadTile, RoadTileLong, Sidewalk, Kerb }

    [Serializable]
    public class Entry
    {
        public GameObject prefab;
        public Kind kind;
        [Tooltip("Local bounds of the normalized prefab, with the pivot at the bottom centre.")]
        public Bounds bounds;
        public int triangles;
        public string sourceName;
    }

    public Entry[] entries = Array.Empty<Entry>();

    /// <summary>Cheapest module of a kind. Deterministic, so a rebuilt catalog keeps the same pick.</summary>
    public Entry Pick(Kind kind)
    {
        Entry best = null;
        if (entries == null) return null;
        for (int i = 0; i < entries.Length; i++)
        {
            var entry = entries[i];
            if (entry == null || entry.prefab == null || entry.kind != kind) continue;
            if (best == null || entry.triangles < best.triangles) best = entry;
        }
        return best;
    }

    /// <summary>Longest road module, used to cover a forecourt with fewer instances.</summary>
    public Entry PickLongestRoadTile()
    {
        Entry best = null;
        if (entries == null) return null;
        for (int i = 0; i < entries.Length; i++)
        {
            var entry = entries[i];
            if (entry == null || entry.prefab == null) continue;
            if (entry.kind != Kind.RoadTile && entry.kind != Kind.RoadTileLong) continue;
            if (best == null || entry.bounds.size.z > best.bounds.size.z) best = entry;
        }
        return best;
    }
}
