using NUnit.Framework;
using UnityEngine;

public class RoadGraphTests
{
    [Test]
    public void OverpassQueryBuilder_RoadsQuery_IncludesBboxAndHighwayFilter()
    {
        var q = OverpassQueryBuilder.RoadsQuery(-1, 36, -0.9, 36.1);
        Assert.That(q, Does.Contain("way[highway"));
        Assert.That(q, Does.Contain("(-1.0000000,36.0000000,-0.9000000,36.1000000)"));
        Assert.That(q, Does.Contain("out geom"));
    }

    [Test]
    public void RoadGraph_BuildFromOverpassWays_CreatesConnectivityForSimpleWay()
    {
        const string json =
            "{ \"elements\": [" +
            "{ \"type\":\"way\",\"id\":1,\"tags\":{\"highway\":\"residential\",\"maxspeed\":\"30\"}," +
            "\"geometry\":[{\"lat\":-1.0,\"lon\":36.0},{\"lat\":-1.0,\"lon\":36.001},{\"lat\":-1.0,\"lon\":36.002}] }" +
            "] }";

        var resp = OverpassResponse.Deserialize(json);
        var go = new GameObject("Converter");
        var conv = go.AddComponent<CoordinateConverter>();
        conv.mapOrigin = ScriptableObject.CreateInstance<MapOrigin>();
        conv.mapOrigin.originLat = -1.0;
        conv.mapOrigin.originLon = 36.0;

        var graph = RoadGraph.BuildFromOverpassWays(resp, conv);
        Assert.That(graph.nodes.Count, Is.EqualTo(3));
        Assert.That(graph.nodes[0].edges.Count, Is.GreaterThan(0));
        Assert.That(graph.nodes[1].edges.Count, Is.GreaterThan(0));
        Assert.That(graph.nodes[2].edges.Count, Is.GreaterThan(0));
    }

    [Test]
    public void RoadGraph_PreservesOsmLaneMetadataForBothTravelDirections()
    {
        const string json = "{\"elements\":[{\"type\":\"way\",\"id\":5,\"tags\":{\"highway\":\"primary\",\"lanes\":\"4\",\"width\":\"14\"},\"geometry\":[{\"lat\":-1,\"lon\":36},{\"lat\":-1,\"lon\":36.001}]}]}";
        var go = new GameObject("Lane converter");
        var converter = go.AddComponent<CoordinateConverter>();
        converter.mapOrigin = ScriptableObject.CreateInstance<MapOrigin>();
        converter.mapOrigin.originLat = -1; converter.mapOrigin.originLon = 36;
        var graph = RoadGraph.BuildFromOverpassWays(OverpassResponse.Deserialize(json), converter);
        Assert.That(graph.nodes[0].edges[0].laneCount, Is.EqualTo(2));
        Assert.That(graph.nodes[0].edges[0].laneWidth, Is.EqualTo(3.5f).Within(.01f));
        Assert.That(graph.nodes[0].edges[0].oneWay, Is.False);
        Assert.That(graph.nodes[1].edges[0].laneCount, Is.EqualTo(2));
    }

    [Test]
    public void AirRoadGraph_UsesDifferentRoadHalvesForTwoWayTraffic()
    {
        var root = new GameObject("Lane graph");
        var graph = root.AddComponent<AIRoadGraph>();
        var a = new GameObject("A").transform; a.position = Vector3.zero;
        var b = new GameObject("B").transform; b.position = Vector3.forward * 20;
        graph.nodes = new[]
        {
            new AIRoadGraph.RoadNode { point = a, nextNodeIndices = new[] { 1 }, outgoingLaneCounts = new[] { 2 }, outgoingLaneWidths = new[] { 3.5f }, outgoingOneWays = new[] { false } },
            new AIRoadGraph.RoadNode { point = b, nextNodeIndices = new[] { 0 }, outgoingLaneCounts = new[] { 2 }, outgoingLaneWidths = new[] { 3.5f }, outgoingOneWays = new[] { false } }
        };
        float forward = graph.GetLaneOffset(0, 1, 0);
        float reverse = graph.GetLaneOffset(1, 0, 0);
        Assert.That(Mathf.Abs(forward), Is.GreaterThan(1f));
        Assert.That(Mathf.Abs(reverse), Is.GreaterThan(1f));
        // Offsets are measured from each direction's right vector, so their signs match
        // while their world-space positions occupy opposite carriageway halves.
        Assert.That(forward, Is.EqualTo(reverse).Within(.01f));
        Object.DestroyImmediate(root); Object.DestroyImmediate(a.gameObject); Object.DestroyImmediate(b.gameObject);
    }

    [Test]
    public void PlayerCityLibrary_PersistsFavouriteStateByCityCode()
    {
        var city = ScriptableObject.CreateInstance<CityDefinition>();
        city.cityCode = "TESTLANE";
        PlayerCityLibrary.SetFavourite(city, true);
        Assert.That(PlayerCityLibrary.IsFavourite(city), Is.True);
        PlayerCityLibrary.SetFavourite(city, false);
        Assert.That(PlayerCityLibrary.IsFavourite(city), Is.False);
        Object.DestroyImmediate(city);
    }
}
