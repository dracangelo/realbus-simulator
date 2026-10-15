using System;
using UnityEngine;

/// <summary>
/// Build-safe references to normalised AI traffic models, built from the imported
/// vehicle FBX sources by <c>Tools &gt; RealBus &gt; Setup &gt; Build Traffic Models</c>.
///
/// The catalog is what lets the shipped game use real downloaded models: the pool
/// keeps working from a build with no editor tooling, and falls back to the
/// procedural variants only when no catalog was ever built.
/// </summary>
public class TrafficCatalog : ScriptableObject
{
    [Serializable]
    public class Entry
    {
        public GameObject prefab;
        public AIVehicleController.VehicleType vehicleType;
        public string displayName;
        [Tooltip("Length along the facing axis, in metres, after normalisation.")]
        public float lengthMeters;
        public Bounds bounds;
        public int triangles;
        [Tooltip("Share of the pool this model asks for. Falls as the model gets denser.")]
        public float weight = 1f;
    }

    public Entry[] entries = Array.Empty<Entry>();

    /// <summary>Entries that can actually be spawned, so an unbuilt catalog is never used.</summary>
    public int UsableCount
    {
        get
        {
            int count = 0;
            for (int i = 0; i < entries.Length; i++)
                if (entries[i] != null && entries[i].prefab != null) count++;
            return count;
        }
    }
}
