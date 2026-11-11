using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Runtime elevation lookup for road nodes, backed by the baked city bundle
/// ({PersistentDataPath}/DownloadedCities/{cityCode}/roads.graph.json).
///
/// Runtime never calls a DEM service: elevations come from the grade bake that
/// ran at import time. Returns null when no bundle exists, in which case the
/// road builder falls back to its flat road offset.
/// </summary>
public static class RoadElevationLookup
{
    const string BundleFileName = "roads.graph.json";

    static readonly Dictionary<(long, long), float> _elevationByNode = new();
    static bool _loaded;

    /// <summary>Loads the baked city bundle for <paramref name="city"/> once.</summary>
    public static void LoadBundle(CityDefinition city)
    {
        _elevationByNode.Clear();
        _loaded = true;
        if (city == null) return;

        string path = RuntimeCityContentStore.ResolveReadPath(city, BundleFileName);
        if (string.IsNullOrEmpty(path) || !File.Exists(path)) return;

        string json;
        try { json = File.ReadAllText(path); }
        catch (IOException exception)
        {
            Debug.LogWarning($"[RoadElevationLookup] Could not read bundle {path}: {exception.Message}");
            return;
        }
        if (string.IsNullOrWhiteSpace(json)) return;

        var bundle = JsonUtility.FromJson<RoadGraph.RoadGraphBundle>(json);
        if (bundle == null || bundle.nodes == null) return;

        foreach (var node in bundle.nodes)
        {
            var key = Quantize(node.lat, node.lon);
            _elevationByNode[key] = node.elevation;
        }

        Debug.Log($"[RoadElevationLookup] Loaded {_elevationByNode.Count} baked node elevations for {city.cityCode}.");
    }

    /// <summary>Baked elevation in world-space metres, or null when unknown.</summary>
    public static float? GetElevation(double latitude, double longitude)
    {
        if (!_loaded) LoadBundle(CityManager.Instance != null ? CityManager.Instance.activeCity : null);
        var key = Quantize(latitude, longitude);
        return _elevationByNode.TryGetValue(key, out var elevation) ? (float?)elevation : null;
    }

    static (long, long) Quantize(double latitude, double longitude)
        => ((long)System.Math.Round(latitude * 1e7), (long)System.Math.Round(longitude * 1e7));
}