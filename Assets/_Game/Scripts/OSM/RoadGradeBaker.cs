using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Road grade bake pipeline (section 3.3).
///
/// Runs once at city import time (bake, do not stream):
///   1. Sample the DEM along every road edge at 5–10 m intervals.
///   2. For bridge=yes / tunnel=yes edges, interpolate between the elevations
///      at the structure ends instead of sampling the DEM (the DEM shows the
///      ground under a flyover, not the deck).
///   3. Smooth with a 30–50 m moving average, clamp grade to ±12 %, and limit
///      the rate of grade change so vertical curves exist.
///   4. Store the result as <see cref="RoadGraph.Edge.gradeProfile"/> per edge;
///      <see cref="RoadGraph.SaveToCityBundle"/> persists it to the city bundle.
///
/// Runtime never calls a DEM service.
/// </summary>
public static class RoadGradeBaker
{
    /// <summary>Sample spacing along each edge: within the 5–10 m spec.</summary>
    public const float DefaultSampleIntervalMeters = 7.5f;
    /// <summary>Moving-average window: within the 30–50 m spec.</summary>
    public const float SmoothWindowMeters = 40f;
    /// <summary>Maximum absolute grade (rise / run).</summary>
    public const float MaxGrade = 0.12f;
    /// <summary>Maximum grade change per metre travelled — enforces vertical curves.</summary>
    public const float MaxGradeChangePerMeter = 0.002f;

    /// <summary>Bakes every edge of <paramref name="graph"/>, filling
    /// <see cref="RoadGraph.Edge.gradeProfile"/>, <see cref="RoadGraph.Edge.lengthMeters"/>
    /// and <see cref="RoadGraph.nodeElevation"/>.</summary>
    public static void Bake(RoadGraph graph, IDemSource dem,
        float sampleIntervalMeters = DefaultSampleIntervalMeters,
        float smoothWindowMeters = SmoothWindowMeters)
    {
        if (graph == null) throw new ArgumentNullException(nameof(graph));
        if (dem == null) throw new ArgumentNullException(nameof(dem));
        if (graph.nodes.Count == 0) return;

        float interval = Mathf.Clamp(sampleIntervalMeters, 1f, 50f);
        float window = Mathf.Max(3f, smoothWindowMeters);

        var nodeElevationSum = new float[graph.nodes.Count];
        var nodeElevationCount = new int[graph.nodes.Count];
        int edgeCount = 0;

        for (int i = 0; i < graph.nodes.Count; i++)
        {
            foreach (var edge in graph.nodes[i].edges)
            {
                var profile = BakeEdge(graph, edge, dem, interval, window);
                edge.gradeProfile = profile;
                edge.lengthMeters = Vector3.Distance(graph.nodes[edge.from].world,
                                                     graph.nodes[edge.to].world);

                // Each node's elevation is the average of the baked profile
                // endpoints incident to it, so shared nodes stay consistent.
                AddEndpoint(nodeElevationSum, nodeElevationCount, edge.from, profile.elevations[0]);
                AddEndpoint(nodeElevationSum, nodeElevationCount, edge.to,
                    profile.elevations[profile.elevations.Length - 1]);
                edgeCount++;
            }
        }

        graph.nodeElevation = new float[graph.nodes.Count];
        for (int i = 0; i < graph.nodes.Count; i++)
            graph.nodeElevation[i] = nodeElevationCount[i] > 0
                ? nodeElevationSum[i] / nodeElevationCount[i]
                : 0f;

        Debug.Log($"[RoadGradeBaker] Baked {graph.nodes.Count} nodes / {edgeCount} directed edges " +
                  $"(interval {interval:F1} m, smoothing window {window:F0} m, source: {dem.Name}).");
    }

    static RoadGraph.GradeProfile BakeEdge(RoadGraph graph, RoadGraph.Edge edge, IDemSource dem,
        float interval, float windowMeters)
    {
        var from = graph.nodes[edge.from];
        var to = graph.nodes[edge.to];

        float length = Vector3.Distance(from.world, to.world);
        int segments = Mathf.Max(1, Mathf.CeilToInt(length / Mathf.Max(1e-3f, interval)));
        int sampleCount = segments + 1;
        float invSegments = 1f / segments;

        var raw = new float[sampleCount];
        for (int i = 0; i < sampleCount; i++)
        {
            float t = i * invSegments;
            double lat = from.lat + (to.lat - from.lat) * t;
            double lon = from.lon + (to.lon - from.lon) * t;

            float elevation;
            if (edge.isBridge || edge.isTunnel)
            {
                // The DEM shows the ground under a flyover / tunnel bore, not
                // the deck or portal elevation: interpolate between the
                // elevations at the structure ends.
                float endA = dem.SampleHeight(from.lat, from.lon);
                float endB = dem.SampleHeight(to.lat, to.lon);
                elevation = Mathf.Lerp(endA, endB, t);
            }
            else
            {
                elevation = dem.SampleHeight(lat, lon);
            }
            raw[i] = elevation;
        }

        var smoothed = MovingAverage(raw, windowMeters / Mathf.Max(1f, interval));
        return ClampGradesAndReintegrate(smoothed, length, segments);
    }

    /// <summary>Centered moving-average over <paramref name="windowSamples"/> samples.</summary>
    static float[] MovingAverage(float[] input, float windowSamples)
    {
        int n = input.Length;
        if (n < 3) return (float[])input.Clone();

        int half = Mathf.Max(1, Mathf.FloorToInt(windowSamples * 0.5f));
        var output = new float[n];
        for (int i = 0; i < n; i++)
        {
            int lo = Mathf.Max(0, i - half);
            int hi = Mathf.Min(n - 1, i + half);
            float sum = 0f;
            for (int j = lo; j <= hi; j++) sum += input[j];
            output[i] = sum / (hi - lo + 1);
        }
        return output;
    }

    /// <summary>
    /// Clamps consecutive-sample grades to ±12 %, limits the rate of grade
    /// change (vertical curves), then re-integrates elevations from the first
    /// sample so the stored profile is exactly consistent with its grades.
    /// </summary>
    static RoadGraph.GradeProfile ClampGradesAndReintegrate(float[] elevations, float length, int segments)
    {
        int n = elevations.Length;
        int gradeCount = Mathf.Max(1, n - 1);
        float step = length / Mathf.Max(1, segments);

        var grades = new float[gradeCount];
        for (int i = 0; i < gradeCount; i++)
        {
            float grade = (elevations[i + 1] - elevations[i]) / Mathf.Max(1e-3f, step);
            grades[i] = Mathf.Clamp(grade, -MaxGrade, MaxGrade);
        }

        // Vertical curves: |grade[i + 1] - grade[i]| may not exceed the allowed
        // change over the distance between the samples.
        for (int i = 0; i + 1 < gradeCount; i++)
        {
            float maxDelta = MaxGradeChangePerMeter * step;
            float delta = grades[i + 1] - grades[i];
            if (Mathf.Abs(delta) > maxDelta)
                grades[i + 1] = grades[i] + Mathf.Sign(delta) * maxDelta;
        }

        var result = new float[n];
        result[0] = elevations[0];
        for (int i = 0; i < gradeCount; i++)
            result[i + 1] = result[i] + grades[i] * step;

        return new RoadGraph.GradeProfile { intervalMeters = step, elevations = result };
    }

    static void AddEndpoint(float[] sums, int[] counts, int nodeIndex, float elevation)
    {
        sums[nodeIndex] += elevation;
        counts[nodeIndex]++;
    }
}