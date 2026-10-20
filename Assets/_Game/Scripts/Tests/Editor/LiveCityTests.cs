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
    public void ImportedBuildingFit_RespectsLotAndRowHeightBand()
    {
        var house = new Bounds(new Vector3(0, 10, 0), new Vector3(10, 20, 10));
        Assert.That(ProceduralBuildingFill.TryFitImportedBuilding(house, 12f, 15f, 7f, out float houseScale), Is.True);
        Assert.That(house.size.x * houseScale, Is.LessThanOrEqualTo(12.01f));
        Assert.That(house.size.z * houseScale, Is.LessThanOrEqualTo(15.01f));
        Assert.That(house.size.y * houseScale, Is.InRange(7f * .55f - .01f, 7f * 3.5f + .01f));

        var tower = new Bounds(new Vector3(0, 50, 0), new Vector3(20, 100, 20));
        Assert.That(ProceduralBuildingFill.TryFitImportedBuilding(tower, 12f, 15f, 7f, out _), Is.False,
            "A hundred-metre tower must not be squeezed into a two-storey terrace.");
        Assert.That(ProceduralBuildingFill.TryFitImportedBuilding(tower, 12f, 15f, 28f, out _), Is.True,
            "The same tower belongs in a taller apartment or commercial row.");

        var shed = new Bounds(new Vector3(0, 1, 0), new Vector3(10, 2, 10));
        Assert.That(ProceduralBuildingFill.TryFitImportedBuilding(shed, 12f, 15f, 7f, out _), Is.False);
    }

    [Test]
    public void MappedFootprintFrame_UsesLongestEdgeAndMeasuresLot()
    {
        Vector3 center = new Vector3(5f, .2f, 7f);
        Vector3 along = new Vector3(.866f, 0f, .5f);
        Vector3 across = new Vector3(-.5f, 0f, .866f);
        var footprint = new System.Collections.Generic.List<Vector3>
        {
            center - along * 10f - across * 5f,
            center + along * 10f - across * 5f,
            center + along * 10f + across * 5f,
            center - along * 10f + across * 5f
        };

        Assert.That(OSMBuildingMeshBuilder.TryFootprintFrame(footprint, out var frameCenter, out _, out _, out float width, out float depth), Is.True);
        Assert.That(width, Is.EqualTo(20f).Within(.05f));
        Assert.That(depth, Is.EqualTo(10f).Within(.05f));
        Assert.That(frameCenter.x, Is.EqualTo(center.x).Within(.05f));
        Assert.That(frameCenter.z, Is.EqualTo(center.z).Within(.05f));
        Assert.That(frameCenter.y, Is.EqualTo(center.y).Within(.01f));
    }

    [Test]
    public void MappedBuildingFit_RespectsFootprintAndMappedHeight()
    {
        var tower = new Bounds(new Vector3(0, 35, 0), new Vector3(20, 70, 20));
        Assert.That(OSMBuildingMeshBuilder.TryFitMappedBuilding(tower, 20f, 20f, 40f, out float towerScale), Is.True);
        Assert.That(tower.size.x * towerScale, Is.LessThanOrEqualTo(20.01f));
        Assert.That(tower.size.z * towerScale, Is.LessThanOrEqualTo(20.01f));
        Assert.That(OSMBuildingMeshBuilder.TryFitMappedBuilding(tower, 20f, 20f, 10f, out _), Is.False,
            "A 70 m facade should not be placed on a 10 m mapped shop.");

        var low = new Bounds(new Vector3(0, 5, 0), new Vector3(10, 10, 10));
        Assert.That(OSMBuildingMeshBuilder.TryFitMappedBuilding(low, 20f, 20f, 30f, out float lowScale), Is.True);
        Assert.That(low.size.x * lowScale, Is.LessThanOrEqualTo(20.01f));
        Assert.That(low.size.z * lowScale, Is.LessThanOrEqualTo(20.01f));
    }

    [Test]
    public void GeneratedBuildingCatalog_ContainsTexturedNormalizedPrefabs()
    {
        var catalog = Resources.Load<BuildingCatalog>("BuildingCatalog");
        Assert.That(catalog, Is.Not.Null, "Run Tools > RealBus > Setup > Build Building Models before these integration tests.");
        Assert.That(catalog.entries.Length, Is.GreaterThanOrEqualTo(8));
        foreach (var entry in catalog.entries)
        {
            Assert.That(entry.prefab, Is.Not.Null);
            Assert.That(entry.triangles, Is.InRange(300, 500000));
            Assert.That(entry.bounds.min.y, Is.EqualTo(0).Within(.02f));
            Assert.That(entry.bounds.center.x, Is.EqualTo(0).Within(.05f));
            Assert.That(entry.bounds.center.z, Is.EqualTo(0).Within(.05f));
            Assert.That(entry.prefab.GetComponentsInChildren<Camera>(true), Is.Empty);
            Assert.That(entry.prefab.GetComponentsInChildren<Rigidbody>(true), Is.Empty);
            Assert.That(entry.prefab.GetComponent<BoxCollider>(), Is.Not.Null, entry.sourceName);
            bool textured = false;
            foreach (var renderer in entry.prefab.GetComponentsInChildren<Renderer>(true))
                foreach (var material in renderer.sharedMaterials)
                {
                    Assert.That(material, Is.Not.Null, entry.sourceName);
                    if (material.mainTexture != null) textured = true;
                }
            Assert.That(textured, Is.True, entry.sourceName);
        }
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
