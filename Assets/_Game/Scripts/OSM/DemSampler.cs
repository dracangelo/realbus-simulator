using System;
using UnityEngine;

/// <summary>
/// Digital elevation model sampling used by the road grade bake (section 3.3).
///
/// The bake samples the DEM once per city import. Runtime never queries a DEM
/// service: it reads the baked <see cref="RoadGraph.GradeProfile"/> data and
/// per-node elevations from the city bundle instead.
///
/// A real DEM (SRTM, GeoTIFF, ...) can be plugged in by implementing
/// <see cref="IDemSource"/> and assigning a <see cref="TextureDemSource"/> backed
/// by a readable heightmap texture. When no heightmap is configured, a
/// deterministic procedural terrain is used so the pipeline is self-contained
/// and testable.
/// </summary>
public interface IDemSource
{
    /// <summary>Human-readable source name for diagnostics.</summary>
    string Name { get; }

    /// <summary>Elevation in world-space metres (above sea level) at the given GPS position.</summary>
    float SampleHeight(double latitude, double longitude);
}

/// <summary>Central (optional) DEM source used by the bake.</summary>
public static class DemSampler
{
    public static IDemSource Source { get; set; }

    public static float SampleHeight(double latitude, double longitude)
        => Source != null ? Source.SampleHeight(latitude, longitude) : 0f;
}

/// <summary>
/// Samples a 2D heightmap texture whose pixels encode elevation.
/// Pixel grayscale 0..1 maps to elevMin .. elevMin + elevRange.
/// The texture must be readable (TextureImporter.isReadable = true).
/// </summary>
public class TextureDemSource : IDemSource
{
    readonly Texture2D _texture;
    readonly double _latMin, _latMax, _lonMin, _lonMax;
    readonly float _elevMin;
    readonly float _elevRange;

    public string Name => $"TextureDemSource({_texture?.name})";

    public TextureDemSource(Texture2D texture, double latMin, double latMax,
        double lonMin, double lonMax, float elevMinMeters, float elevRangeMeters)
    {
        _texture = texture;
        _latMin = latMin; _latMax = latMax;
        _lonMin = lonMin; _lonMax = lonMax;
        _elevMin = elevMinMeters;
        _elevRange = elevRangeMeters;
    }

    public float SampleHeight(double latitude, double longitude)
    {
        if (_texture == null) return 0f;
        float u = Mathf.Clamp01((float)((longitude - _lonMin) / Mathf.Max(1e-9f, (float)(_lonMax - _lonMin))));
        float v = Mathf.Clamp01((float)((latitude - _latMin) / Mathf.Max(1e-9f, (float)(_latMax - _latMin))));
        // Texture row 0 is the top (north); flip V so V=0 is the south edge.
        Color sample = _texture.GetPixelBilinear(u, 1f - v);
        return _elevMin + sample.grayscale * _elevRange;
    }
}

/// <summary>
/// Deterministic procedural terrain used when no real DEM is available.
/// Rolling low-frequency hills derived purely from lat/lon, so every bake of
/// the same city produces the same profile without any external data.
/// </summary>
public class ProceduralDemSource : IDemSource
{
    readonly float _baseElevationMeters;
    readonly float _amplitudeMeters;
    readonly int _seed;

    public string Name => $"ProceduralDemSource(seed={_seed})";

    public ProceduralDemSource(float baseElevationMeters, float amplitudeMeters, int seed)
    {
        _baseElevationMeters = baseElevationMeters;
        _amplitudeMeters = amplitudeMeters;
        _seed = seed;
    }

    public float SampleHeight(double latitude, double longitude)
    {
        float height = _baseElevationMeters;
        float frequency = 1f / 4000f; // cycles per degree
        float amplitude = _amplitudeMeters;
        float seedX = _seed * 73856093;
        float seedY = _seed * 19349663;
        for (int octave = 0; octave < 4; octave++)
        {
            height += Mathf.PerlinNoise(
                (float)(longitude * frequency) + seedX,
                (float)(latitude * frequency) + seedY) * amplitude;
            frequency *= 2f;
            amplitude *= 0.5f;
        }
        return height;
    }
}