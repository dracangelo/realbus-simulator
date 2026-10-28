using System;
using UnityEngine;

/// <summary>Build-safe references to normalized imported scenery, not drivable vehicles.</summary>
public class LiveCityCatalog : ScriptableObject
{
    // Values are serialized as integers in the catalog asset, so new kinds must
    // be appended rather than inserted. StreetLight and RoadSign come from the
    // city and road-sign packs and are placed by the same kerb streamer.
    public enum Kind { Tree, Plant, Rock, Billboard, Bench, Pedestrian, StreetLight, RoadSign }
    [Serializable]
    public class Entry
    {
        public GameObject prefab;
        public Kind kind;
        public Bounds bounds;
        public int triangles;
    }
    public Entry[] entries = Array.Empty<Entry>();
}
