using UnityEngine;

public static class OverpassQueryBuilder
{
    public static string RoadsQuery(double minLat, double minLon, double maxLat, double maxLon)
    {
        // Sample: [out:json]; way[highway~'motorway|primary|secondary|residential'](<bbox>); out geom;
        string bbox = $"{minLat},{minLon},{maxLat},{maxLon}";
        return "[out:json][timeout:120];" +
               "way[highway~\"motorway|trunk|primary|secondary|tertiary|residential|unclassified\"]" +
               $"({bbox});" +
               "out geom;";
    }

    public static string BusRoutesQuery(double minLat, double minLon, double maxLat, double maxLon)
    {
        // Sample: [out:json]; relation[route=bus](<bbox>); out geom;
        string bbox = $"{minLat},{minLon},{maxLat},{maxLon}";
        return "[out:json][timeout:120];" +
               "relation[route=bus]" +
               $"({bbox});" +
               "out geom;";
    }

    public static void BoundingBoxFromCenter(double centerLat, double centerLon, float halfExtentMeters, out double minLat, out double minLon, out double maxLat, out double maxLon)
    {
        // Approx meters->degrees conversion. Good for small areas.
        double metersPerDegreeLat = 111320.0;
        double metersPerDegreeLon = 111320.0 * System.Math.Cos(centerLat * System.Math.PI / 180.0);
        double dLat = halfExtentMeters / metersPerDegreeLat;
        double dLon = halfExtentMeters / metersPerDegreeLon;

        minLat = centerLat - dLat;
        maxLat = centerLat + dLat;
        minLon = centerLon - dLon;
        maxLon = centerLon + dLon;
    }
}
