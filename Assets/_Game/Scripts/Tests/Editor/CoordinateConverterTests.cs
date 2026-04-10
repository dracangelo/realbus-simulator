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
        double originLat = -1.2864;
        double originLon = 36.8172;

        // Test a few offsets around the origin (small local-area usage).
        var points = new (double lat, double lon)[]
        {
            (originLat, originLon),
            (originLat + 0.0005, originLon + 0.0005),
            (originLat - 0.0008, originLon + 0.0012),
            (originLat + 0.0020, originLon - 0.0015),
        };

        for (int i = 0; i < points.Length; i++)
        {
            var p = points[i];
            Vector3 w = CoordinateConverter.LocalOriginGeoToWorld(p.lat, p.lon, originLat, originLon);
            var back = CoordinateConverter.LocalOriginWorldToGeo(w, originLat, originLon);

            double error = DistanceMeters(p.lat, p.lon, back.lat, back.lon);
            Assert.That(error, Is.LessThanOrEqualTo(0.1), $"Round-trip error too high: {error:0.000}m for point {p}");
        }
    }
}

