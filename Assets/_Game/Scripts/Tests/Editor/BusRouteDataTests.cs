using NUnit.Framework;

public class BusRouteDataTests
{
    [Test]
    public void BusRouteParser_ParseRoutes_ExtractsOrderedStopsGeometryAndDestination()
    {
        const string json =
            "{ \"elements\": [" +
            "{ \"type\":\"relation\",\"id\":101,\"tags\":{\"route\":\"bus\",\"ref\":\"23\",\"name\":\"23 CBD - Westlands\",\"to\":\"Westlands\"}," +
            "\"members\":[" +
            "{\"type\":\"node\",\"ref\":1,\"role\":\"stop\",\"lat\":-1.0,\"lon\":36.0,\"tags\":{\"name\":\"CBD Terminal\"}}," +
            "{\"type\":\"way\",\"ref\":11,\"role\":\"\",\"geometry\":[{\"lat\":-1.0,\"lon\":36.0},{\"lat\":-1.0,\"lon\":36.001}]}," +
            "{\"type\":\"node\",\"ref\":2,\"role\":\"platform\",\"lat\":-1.0,\"lon\":36.001,\"tags\":{\"name\":\"Museum\"}}," +
            "{\"type\":\"way\",\"ref\":12,\"role\":\"backward\",\"geometry\":[{\"lat\":-1.0,\"lon\":36.003},{\"lat\":-1.0,\"lon\":36.001}]}," +
            "{\"type\":\"node\",\"ref\":3,\"role\":\"stop\",\"lat\":-1.0,\"lon\":36.003,\"tags\":{\"name\":\"Westlands\"}}" +
            "] } ] }";

        var response = OverpassResponse.Deserialize(json);
        var routes = BusRouteParser.ParseRoutes(response);

        Assert.That(routes.Count, Is.EqualTo(1));
        Assert.That(routes[0].routeRef, Is.EqualTo("23"));
        Assert.That(routes[0].destinationName, Is.EqualTo("Westlands"));
        Assert.That(routes[0].stops.Count, Is.EqualTo(3));
        Assert.That(routes[0].stops[0].stopName, Is.EqualTo("CBD Terminal"));
        Assert.That(routes[0].stops[2].stopName, Is.EqualTo("Westlands"));
        Assert.That(routes[0].geometry.Count, Is.EqualTo(3));
        Assert.That(routes[0].geometry[0].lon, Is.EqualTo(36.0).Within(0.000001));
        Assert.That(routes[0].geometry[2].lon, Is.EqualTo(36.003).Within(0.000001));
        Assert.That(routes[0].stops[0].headingDegrees, Is.InRange(80f, 100f));
    }

    [Test]
    public void BusRoute_SyncLegacyStopsFromBusStops_CopiesOrderedStopData()
    {
        var route = UnityEngine.ScriptableObject.CreateInstance<BusRoute>();
        route.busStops = new[]
        {
            new BusStop { stopId = "A", stopName = "Alpha", latitude = -1.0, longitude = 36.0 },
            new BusStop { stopId = "B", stopName = "Bravo", latitude = -1.1, longitude = 36.1 }
        };

        route.SyncLegacyStopsFromBusStops();

        Assert.That(route.stops, Is.Not.Null);
        Assert.That(route.stops.Length, Is.EqualTo(2));
        Assert.That(route.stops[0].stopName, Is.EqualTo("Alpha"));
        Assert.That(route.stops[1].longitude, Is.EqualTo(36.1d).Within(0.000001d));
    }

    [Test]
    public void BusRoute_EnsureRuntimeData_RepairsMissingMetrics()
    {
        var route = UnityEngine.ScriptableObject.CreateInstance<BusRoute>();
        route.stops = new[]
        {
            new BusStopData { stopName = "A", latitude = -1.2864, longitude = 36.8172 },
            new BusStopData { stopName = "B", latitude = -1.2864, longitude = 36.8272 }
        };

        route.EnsureRuntimeData();

        Assert.That(route.distanceKm, Is.GreaterThan(1f));
        Assert.That(route.estimatedTimeMinutes, Is.GreaterThan(0f));
        Assert.That(route.GetStopCount(), Is.EqualTo(2));
    }

    [Test]
    public void CityConfig_CreateRuntimeCityDefinition_ComputesBoundingBoxAndCopiesRoutes()
    {
        var route = UnityEngine.ScriptableObject.CreateInstance<BusRoute>();
        route.routeName = "Tutorial Loop";

        var config = UnityEngine.ScriptableObject.CreateInstance<CityConfig>();
        config.cityName = "Tutorial City";
        config.countryName = "Sandbox";
        config.cityCode = "TUT1";
        config.gpsCenter = new GeoCoordinate(-1.2864, 36.8172);
        config.queryHalfExtentMeters = 1000f;
        config.defaultZoom = 16;
        config.availableRoutes = new[] { route };

        var runtimeCity = config.CreateRuntimeCityDefinition();

        Assert.That(runtimeCity.cityName, Is.EqualTo("Tutorial City"));
        Assert.That(runtimeCity.cityCode, Is.EqualTo("TUT1"));
        Assert.That(runtimeCity.centreLat, Is.EqualTo(-1.2864d).Within(0.000001d));
        Assert.That(runtimeCity.maxLat, Is.GreaterThan(runtimeCity.minLat));
        Assert.That(runtimeCity.maxLon, Is.GreaterThan(runtimeCity.minLon));
        Assert.That(runtimeCity.availableRoutes, Has.Length.EqualTo(1));
        Assert.That(runtimeCity.availableRoutes[0], Is.EqualTo(route));
    }
}
