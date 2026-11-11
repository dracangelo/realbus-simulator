using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Tests for the road grade bake pipeline (section 3.3). The baker is pure
/// math over a fake DEM, so these tests run without any scene or network.
/// </summary>
public class RoadGradeBakerTests
{
    sealed class FakeDem : IDemSource
    {
        public string Name => "fake";
        public Func<double, double, float> Sample = (_, _) => 0f;
        public float SampleHeight(double latitude, double longitude) => Sample(latitude, longitude);
    }

    static RoadGraph.Node Node(double lat, double lon, Vector3 world)
    {
        return new RoadGraph.Node { lat = lat, lon = lon, world = world };
    }

    static RoadGraph.Edge Edge(int from, int to, bool isBridge = false, bool isTunnel = false)
    {
        return new RoadGraph.Edge
        {
            from = from, to = to, isBridge = isBridge, isTunnel = isTunnel,
            speedLimitKmh = 50f, laneCount = 1, laneWidth = 3.5f, oneWay = false,
            travelTimeSec = 1f, sharedLane = false
        };
    }

    static RoadGraph BuildChain(double[] lats, Vector3[] worlds, bool[] bridges = null)
    {
        var graph = new RoadGraph();
        int n = lats.Length;
        for (int i = 0; i < n; i++)
        {
            var node = Node(lats[i], 36d, worlds[i]);
            node.index = i;
            graph.nodes.Add(node);
        }
        for (int i = 0; i < n - 1; i++)
        {
            var edge = Edge(i, i + 1,
                bridges != null && bridges[i], bridges != null && bridges[i + 1]);
            graph.nodes[i].edges.Add(edge);
        }
        return graph;
    }

    static float ConsecutiveGrade(float[] elevations, int i, float step)
    {
        return (elevations[i + 1] - elevations[i]) / step;
    }

    [Test]
    public void Bake_FlatDem_EverySampleAtDemElevation()
    {
        var dem = new FakeDem { Sample = (_, _) => 127f };
        var graph = BuildChain(new[] { -1d, -1.0001d },
            new[] { new Vector3(0, 0, 0), new Vector3(11.132f, 0, 0) });

        RoadGradeBaker.Bake(graph, dem);

        Assert.That(graph.nodes[0].edges[0].gradeProfile, Is.Not.Null);
        foreach (var e in graph.nodes[0].edges[0].gradeProfile.elevations)
            Assert.That(e, Is.EqualTo(127f).Within(1e-4f));
    }

    [Test]
    public void Bake_SteepDem_GradesAreClampedToPlusMinus12Percent()
    {
        // 100 m of elevation over ~11 m = ~900 % grade; must be clamped to 12 %.
        var dem = new FakeDem { Sample = (lat, _) => (float)((lat + 1d) * 1e6) };
        var graph = BuildChain(new[] { -1d, -1.0001d },
            new[] { new Vector3(0, 0, 0), new Vector3(11.132f, 0, 0) });

        RoadGradeBaker.Bake(graph, dem);

        var profile = graph.nodes[0].edges[0].gradeProfile;
        float step = profile.intervalMeters;
        for (int i = 0; i < profile.elevations.Length - 1; i++)
        {
            float grade = ConsecutiveGrade(profile.elevations, i, step);
            Assert.That(grade, Is.LessThanOrEqualTo(RoadGradeBaker.MaxGrade + 1e-4f),
                "positive grade must be clamped");
            Assert.That(grade, Is.GreaterThanOrEqualTo(-RoadGradeBaker.MaxGrade - 1e-4f),
                "negative grade must be clamped");
        }
    }

    [Test]
    public void Bake_BridgeEdge_InterpolatesBetweenStructureEndsIgnoringDem()
    {
        // DEM has a bump under the bridge; the bridge deck must ignore it.
        var dem = new FakeDem
        {
            Sample = (lat, _) => (float)(Math.Abs(lat + 1.00005d) < 1e-6 ? 50f : 0f)
        };
        var graph = BuildChain(new[] { -1d, -1.00005d, -1.0001d },
            new[] { new Vector3(0, 0, 0), new Vector3(5.566f, 0, 0), new Vector3(11.132f, 0, 0) },
            bridges: new[] { true, true });

        RoadGradeBaker.Bake(graph, dem, sampleIntervalMeters: 5f);

        var profile = graph.nodes[0].edges[0].gradeProfile;
        Assert.That(profile.elevations.Length, Is.GreaterThanOrEqualTo(2));
        // Both ends are at 0 elevation, so the whole deck must be at 0 even
        // though the DEM shows 50 m at mid-span.
        foreach (var e in profile.elevations)
            Assert.That(e, Is.EqualTo(0f).Within(1e-4f), "bridge deck must follow end elevations, not the DEM");
    }

    [Test]
    public void Bake_NonBridgeEdge_SamplesTheDemBump()
    {
        var dem = new FakeDem
        {
            Sample = (lat, _) => (float)(Math.Abs(lat + 1.00005d) < 1e-6 ? 50f : 0f)
        };
        var graph = BuildChain(new[] { -1d, -1.00005d, -1.0001d },
            new[] { new Vector3(0, 0, 0), new Vector3(5.566f, 0, 0), new Vector3(11.132f, 0, 0) });

        RoadGradeBaker.Bake(graph, dem, sampleIntervalMeters: 5f);

        var middle = graph.nodes[1].edges[0].gradeProfile.elevations; // edge 1->2 starts at the bump
        Assert.That(middle[0], Is.EqualTo(50f).Within(1f), "non-bridge edges sample the DEM");
    }

    [Test]
    public void Bake_AbruptGradeChange_RateOfGradeChangeIsLimited()
    {
        // Grades jump from +12 % to -12 % over one step: vertical curve must limit the kink.
        var dem = new FakeDem { Sample = (lat, _) => (float)(lat < -1.00005d ? -10f : 10f) };
        var graph = BuildChain(new[] { -1d, -1.00005d, -1.0001d },
            new[] { new Vector3(0, 0, 0), new Vector3(5.566f, 0, 0), new Vector3(11.132f, 0, 0) });

        RoadGradeBaker.Bake(graph, dem, sampleIntervalMeters: 5f);

        float step = 5.566f;
        float maxDelta = RoadGradeBaker.MaxGradeChangePerMeter * step;
        var profile = graph.nodes[0].edges[0].gradeProfile.elevations;
        for (int i = 1; i < profile.Length - 1; i++)
        {
            float g0 = ConsecutiveGrade(profile, i - 1, step);
            float g1 = ConsecutiveGrade(profile, i, step);
            Assert.That(Mathf.Abs(g1 - g0), Is.LessThanOrEqualTo(maxDelta + 1e-4f),
                "grade change must be limited to form a vertical curve");
        }
    }

    [Test]
    public void Bake_FillsNodeElevationsFromProfileEndpoints()
    {
        var dem = new FakeDem { Sample = (_, _) => 42f };
        var graph = BuildChain(new[] { -1d, -1.0001d },
            new[] { new Vector3(0, 0, 0), new Vector3(11.132f, 0, 0) });

        RoadGradeBaker.Bake(graph, dem);

        Assert.That(graph.nodeElevation, Is.Not.Null);
        Assert.That(graph.nodeElevation.Length, Is.EqualTo(graph.nodes.Count));
        foreach (var e in graph.nodeElevation)
            Assert.That(e, Is.EqualTo(42f).Within(1e-4f));
    }

    [Test]
    public void SaveAndLoadCityBundle_PreservesGradeProfiles()
    {
        var dem = new FakeDem { Sample = (lat, _) => (float)(lat * 1000f) };
        var graph = BuildChain(new[] { -1d, -1.0001d },
            new[] { new Vector3(0, 0, 0), new Vector3(11.132f, 0, 0) });
        RoadGradeBaker.Bake(graph, dem);

        var city = ScriptableObject.CreateInstance<CityDefinition>();
        Assert.True(graph.SaveToCityBundle(city), "bundle should write");

        var loaded = RoadGraph.LoadCityBundle(city);
        Assert.That(loaded, Is.Not.Null);
        Assert.That(loaded.nodes.Count, Is.EqualTo(2));

        var rehydrated = RoadGraph.LoadFromCityBundle(city, null);
        Assert.That(rehydrated, Is.Not.Null);
        Assert.That(rehydrated.nodeElevation, Is.Not.Null);
        Assert.That(rehydrated.nodeElevation.Length, Is.EqualTo(2));

        var edge = rehydrated.nodes[0].edges[0];
        Assert.That(edge.gradeProfile, Is.Not.Null);
        Assert.That(edge.gradeProfile.intervalMeters, Is.GreaterThan(0f));
        Assert.That(edge.gradeProfile.elevations, Is.Not.Empty);

        // Clean up the downloaded city content.
        string directory = RuntimeCityContentStore.CityDirectory(city != null ? city.cityCode : null);
        if (Directory.Exists(directory)) Directory.Delete(directory, true);
    }
}