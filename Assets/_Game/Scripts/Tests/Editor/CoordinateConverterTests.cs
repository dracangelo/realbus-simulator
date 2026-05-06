using NUnit.Framework;
using UnityEngine;

public class CoordinateConverterTests
{
    // Haversine distance in meters.
    static double DistanceMeters(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371000.0;
        double dLat = (lat2 - lat1) * System.Math.PI / 180.0;
        double dLon = (lon2 - lon1) * System.Math.PI / 180.0;
        double a =
            System.Math.Sin(dLat / 2) * System.Math.Sin(dLat / 2) +
            System.Math.Cos(lat1 * System.Math.PI / 180.0) *
            System.Math.Cos(lat2 * System.Math.PI / 180.0) *
            System.Math.Sin(dLon / 2) * System.Math.Sin(dLon / 2);
        double c = 2 * System.Math.Atan2(System.Math.Sqrt(a), System.Math.Sqrt(1 - a));
        return R * c;
    }

    [Test]
    public void LocalOrigin_RoundTrip_GeoToWorldToGeo_IsAccurateWithinPointOneMeters()
    {
        var testCases = new (double originLat, double originLon, (double lat, double lon)[] points)[]
        {
            (
                -1.2864, 36.8172,
                new[]
                {
                    (-1.2864, 36.8172),
                    (-1.2859, 36.8177),
                    (-1.2872, 36.8184),
                    (-1.2844, 36.8157)
                }
            ),
            (
                51.5074, -0.1278,
                new[]
                {
                    (51.5074, -0.1278),
                    (51.5083, -0.1254),
                    (51.5052, -0.1301),
                    (51.5111, -0.1219)
                }
            ),
        };

        for (int caseIndex = 0; caseIndex < testCases.Length; caseIndex++)
        {
            var testCase = testCases[caseIndex];
            for (int pointIndex = 0; pointIndex < testCase.points.Length; pointIndex++)
            {
                var point = testCase.points[pointIndex];
                Vector3 world = CoordinateConverter.LocalOriginGeoToWorld(
                    point.lat, point.lon, testCase.originLat, testCase.originLon);
                var back = CoordinateConverter.LocalOriginWorldToGeo(
                    world, testCase.originLat, testCase.originLon);

                double error = DistanceMeters(point.lat, point.lon, back.lat, back.lon);
                Assert.That(
                    error,
                    Is.LessThanOrEqualTo(0.1),
                    $"Round-trip error too high: {error:0.000}m for origin ({testCase.originLat}, {testCase.originLon}) point {point}");
            }
        }
    }

    [Test]
    public void Instance_UsesAssignedMapOrigin_WhenNoMapboxOrTileLoaderIsPresent()
    {
        var go = new GameObject("CoordinateConverter_Test");
        var converter = go.AddComponent<CoordinateConverter>();
        var origin = ScriptableObject.CreateInstance<MapOrigin>();

        origin.SetOrigin(-1.2864, 36.8172, "Nairobi");
        converter.mapOrigin = origin;

        Vector3 world = converter.GeoToWorldPosition(-1.2859, 36.8177);
        var back = converter.WorldToGeoPosition(world);
        double error = DistanceMeters(-1.2859, 36.8177, back.lat, back.lon);

        Assert.That(error, Is.LessThanOrEqualTo(0.1));

        Object.DestroyImmediate(go);
        Object.DestroyImmediate(origin);
    }
}
