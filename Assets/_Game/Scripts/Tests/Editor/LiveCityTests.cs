using NUnit.Framework;
using UnityEngine;

public class LiveCityTests
{
    [Test]
    public void RoadQueries_DeduplicateLongSegments()
    {
        var index = new RoadSurfaceIndex();
        index.Add(Vector3.zero, Vector3.forward * 400, 8, 1);
        int count = 0;
        foreach (var segment in index.Around(Vector3.forward * 200, 180)) count++;
        Assert.That(count, Is.EqualTo(1));
    }

    [Test]
    public void FootprintClearance_RejectsInsideAndEdgeButAllowsSidewalk()
    {
        var polygon = new[] { new Vector3(0, 0, 0), new Vector3(10, 0, 0), new Vector3(10, 0, 10), new Vector3(0, 0, 10) };
        Assert.That(LiveCityStreamer.OverlapsPolygon(new Vector3(5, 0, 5), 1, polygon), Is.True);
        Assert.That(LiveCityStreamer.OverlapsPolygon(new Vector3(10.5f, 0, 5), 1, polygon), Is.True);
        Assert.That(LiveCityStreamer.OverlapsPolygon(new Vector3(12, 0, 5), 1, polygon), Is.False);
    }

    [Test]
    public void Placement_DeterministicAndDoesNotConsumeGameplayRandom()
    {
        var before = Random.state;
        uint first = LiveCityStreamer.PlacementSeed(42, new Vector3(25, 0, 12), -1);
        Assert.That(LiveCityStreamer.PlacementSeed(42, new Vector3(25, 0, 12), -1), Is.EqualTo(first));
        Assert.That(LiveCityStreamer.PlacementSeed(42, new Vector3(25, 0, 12), 1), Is.Not.EqualTo(first));
        Assert.That(Random.state, Is.EqualTo(before));
    }

    [Test]
    public void GeneratedCatalog_ContainsRealNormalizedBuildSafeModels()
    {
        var catalog = Resources.Load<LiveCityCatalog>("LiveCityCatalog");
        Assert.That(catalog, Is.Not.Null, "Run Build Live City Models before these integration tests.");
        Assert.That(catalog.entries.Length, Is.GreaterThan(3));
        bool trees = false, signs = false, benches = false;
        foreach (var entry in catalog.entries)
        {
            Assert.That(entry.prefab, Is.Not.Null);
            Assert.That(entry.triangles, Is.InRange(1, entry.kind == LiveCityCatalog.Kind.Pedestrian ? 100000 : 65000));
            Assert.That(entry.bounds.min.y, Is.EqualTo(0).Within(.01));
            Assert.That(entry.prefab.GetComponentsInChildren<Camera>(true), Is.Empty);
            Assert.That(entry.prefab.GetComponentsInChildren<Collider>(true), Is.Empty);
            Assert.That(entry.prefab.GetComponentsInChildren<Rigidbody>(true), Is.Empty);
            foreach (var renderer in entry.prefab.GetComponentsInChildren<Renderer>(true))
                foreach (var material in renderer.sharedMaterials) Assert.That(material, Is.Not.Null);
            trees |= entry.kind == LiveCityCatalog.Kind.Tree;
            signs |= entry.kind == LiveCityCatalog.Kind.Billboard;
            benches |= entry.kind == LiveCityCatalog.Kind.Bench;
        }
        Assert.That(trees && signs && benches, Is.True);
    }
}
