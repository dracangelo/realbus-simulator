using System.Globalization;
using NUnit.Framework;
using UnityEngine;

public class Phase3MapTests
{
    [Test]
    public void DirectedPathFollowsSnappedSegmentAndRejectsWrongWayReturn()
    {
        var go = new GameObject("Phase 3 converter");
        var origin = ScriptableObject.CreateInstance<MapOrigin>();
        try
        {
            origin.SetOrigin(1, 1);
            var converter = go.AddComponent<CoordinateConverter>();
            converter.mapOrigin = origin;
            var response = new OverpassResponse();
            var way = new OverpassResponse.Element { type = "way", id = 10 };
            way.tags["highway"] = "residential";
            way.tags["oneway"] = "yes";
            way.geometry.Add(new OverpassResponse.GeoPoint { lat = 1, lon = 1 });
            way.geometry.Add(new OverpassResponse.GeoPoint { lat = 1, lon = 1.001 });
            response.elements.Add(way);
            var graph = RoadGraph.BuildFromOverpassWays(response, converter);
            var route = new BusRouteParser.ParsedBusRoute();
            route.stops.Add(new BusStop { latitude = 1, longitude = 1.0002 });
            route.stops.Add(new BusStop { latitude = 1, longitude = 1.0008 });
            Assert.IsTrue(RoadRoutePathBuilder.TryBuild(route, graph, converter, 60f));
            Assert.IsTrue(route.isRoadPathValidated);
            Assert.That(route.lengthKm, Is.InRange(0.06f, 0.07f));
            var returns = BusRouteParser.GenerateReturnRoutes(new System.Collections.Generic.List<BusRouteParser.ParsedBusRoute> { route });
            Assert.IsFalse(RoadRoutePathBuilder.TryBuild(returns[0], graph, converter, 60f));
            Assert.IsFalse(returns[0].isRoadPathValidated);
        }
        finally { Object.DestroyImmediate(go); Object.DestroyImmediate(origin); }
    }

    [Test]
    public void TileCoverageWrapsAndClampsAtDatelineAndPoles()
    {
        foreach (var tile in SlippyTileCoverage.InRadius(85.05, 179.999, 10, 2000f))
        {
            Assert.That(tile.x, Is.InRange(0, 1023));
            Assert.That(tile.y, Is.InRange(0, 1023));
        }
        var coverage = SlippyTileCoverage.InRadius(0, 179.999, 16, 2000f);
        Assert.IsTrue(coverage.Exists(tile => tile.x == 0));
        Assert.IsTrue(coverage.Exists(tile => tile.x == 65535));
    }

    [Test]
    public void CoverageUsesLatitudeAndZoomNotLegacyTileSize()
    {
        var equator = CityPredownloader.GetTilesInRadius(0, 0, 16, 2000f, 200f);
        var legacy = CityPredownloader.GetTilesInRadius(0, 0, 16, 2000f, 999f);
        var north = CityPredownloader.GetTilesInRadius(60, 0, 16, 2000f, 200f);
        Assert.AreEqual(equator.Count, legacy.Count);
        Assert.Greater(north.Count, equator.Count);
        for (int i = 1; i < equator.Count; i++) Assert.LessOrEqual(equator[i - 1].dist, equator[i].dist);
    }

    [Test]
    public void OverpassBboxUsesInvariantDecimalsAndStopsUseUnion()
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("fr-FR");
            Assert.AreEqual("-1.2000000,36.1000000,-1.1000000,36.2000000", OverpassQueryBuilder.FormatBbox(-1.2, 36.1, -1.1, 36.2));
            Assert.That(OverpassQueryBuilder.BusStopsQuery(-1.2, 36.1, -1.1, 36.2), Does.Contain("(node[highway=bus_stop]"));
        }
        finally { CultureInfo.CurrentCulture = previous; }
    }

    [Test]
    public void ResolveJoinsAllRelationWaysAndPreservesStopNames()
    {
        const string json = "{\"elements\":[" +
            "{\"type\":\"node\",\"id\":1,\"lat\":1,\"lon\":1,\"tags\":{\"name\":\"First\"}}," +
            "{\"type\":\"node\",\"id\":2,\"lat\":1,\"lon\":1.001}," +
            "{\"type\":\"node\",\"id\":3,\"lat\":1,\"lon\":1.002}," +
            "{\"type\":\"way\",\"id\":10,\"nodes\":[1,2]}," +
            "{\"type\":\"way\",\"id\":11,\"nodes\":[2,3]}," +
            "{\"type\":\"relation\",\"id\":20,\"tags\":{\"route\":\"bus\"},\"members\":[" +
            "{\"type\":\"node\",\"ref\":1,\"role\":\"stop\"}," +
            "{\"type\":\"way\",\"ref\":10,\"role\":\"\"}," +
            "{\"type\":\"way\",\"ref\":11,\"role\":\"\"}]}]}";
        var response = OverpassResponse.Deserialize(json);
        response.Resolve();
        var routes = BusRouteParser.ParseRoutes(response);
        Assert.AreEqual(1, routes.Count);
        Assert.AreEqual(3, routes[0].geometry.Count);
        Assert.AreEqual("First", routes[0].stops[0].stopName);
    }

    [Test]
    public void ReturnRouteDoesNotMutateOutboundStopHeadings()
    {
        var route = new BusRouteParser.ParsedBusRoute();
        route.stops.Add(new BusStop { latitude = 1, longitude = 1, headingDegrees = 90 });
        route.stops.Add(new BusStop { latitude = 1, longitude = 1.001, headingDegrees = 90 });
        route.geometry.Add((1, 1)); route.geometry.Add((1, 1.001));
        var reversed = BusRouteParser.GenerateReturnRoutes(new System.Collections.Generic.List<BusRouteParser.ParsedBusRoute> { route });
        Assert.AreEqual(90f, route.stops[0].headingDegrees);
        Assert.AreNotSame(route.stops[0], reversed[0].stops[1]);
    }

    [Test]
    public void ReverseOneWayAllowsOnlyReverseTravelAndCanBeSnapped()
    {
        var response = OverpassResponse.Deserialize("{\"elements\":[{\"type\":\"way\",\"id\":1,\"tags\":{\"highway\":\"residential\",\"oneway\":\"-1\"},\"geometry\":[{\"lat\":0,\"lon\":0},{\"lat\":0,\"lon\":0.001}]}]}");
        var graph = RoadGraph.BuildFromOverpassWays(response, null);
        Assert.AreEqual(0, graph.FindPathAStar(0, 1).Count);
        Assert.AreEqual(2, graph.FindPathAStar(1, 0).Count);
        Assert.IsTrue(graph.TryProjectToNearestSegment(new Vector3(50f, 0f, 2f), out var point, out _, out _, out float distance));
        Assert.AreEqual(0f, point.z, 0.001f);
        Assert.AreEqual(2f, distance, 0.001f);
    }
}
