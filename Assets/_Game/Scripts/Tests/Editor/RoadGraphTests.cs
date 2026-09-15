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
}

