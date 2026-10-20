using System;
using UnityEngine;

/// <summary>
/// Detailed building models imported from the downloaded packs. Generated infill
/// picks from this catalog to put real, textured facades into the city without
/// touching the mapped OSM buildings or the existing procedural fill.
/// </summary>
public class BuildingCatalog : ScriptableObject
{
    [Serializable]
    public class Entry
    {
        [Tooltip("Prefab with its pivot at the bottom centre, so it can be dropped straight onto the ground.")]
        public GameObject prefab;
        [Tooltip("Local bounds of the normalised prefab, used to fit it into an infill lot.")]
        public Bounds bounds;
        [Tooltip("Triangle cost of one instance, used to keep the mix cheap enough to stream.")]
        public int triangles;
        [Tooltip("Source FBX name, kept for diagnostics and deterministic ordering.")]
        public string sourceName;
    }

    public Entry[] entries = Array.Empty<Entry>();
}
