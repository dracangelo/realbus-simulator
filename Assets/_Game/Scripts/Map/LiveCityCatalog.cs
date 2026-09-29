using System;
using UnityEngine;

/// <summary>Build-safe references to normalized imported scenery, not drivable vehicles.</summary>
public class LiveCityCatalog : ScriptableObject
{
    public enum Kind { Tree, Plant, Rock, Billboard, Bench, Pedestrian }
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
