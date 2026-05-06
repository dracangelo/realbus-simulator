using System.Text;
using UnityEngine;

/// <summary>
/// Builds Overpass QL query strings for the systems in sections 3.6–3.7.
///
/// All methods are pure/static — no MonoBehaviour, fully unit-testable.
/// </summary>
public static class OverpassQueryBuilder
{
    // ── Shared header/footer ───────────────────────────────────────────

    const int DefaultTimeoutSeconds = 120;

    /// <summary>Standard Overpass header that all queries share.</summary>
    static string Header(int timeoutSeconds = DefaultTimeoutSeconds) =>
        $"[out:json][timeout:{timeoutSeconds}];";

    // ── Bounding-box helpers ───────────────────────────────────────────

    /// <summary>
    /// Computes a lat/lon bounding box from a centre point and half-extent in
    /// metres.  Accurate to within 0.1 % for radii up to 50 km.
    /// </summary>
    public static void BoundingBoxFromCenter(
        double centerLat, double centerLon,
        float halfExtentMeters,
        out double minLat, out double minLon,
        out double maxLat, out double maxLon)
    {
        double dLat = halfExtentMeters / CoordinateConverter.MetersPerDegreeLat;
        double dLon = halfExtentMeters / CoordinateConverter.MetersPerDegreeLonAt(centerLat);

        minLat = centerLat - dLat;
        maxLat = centerLat + dLat;
        minLon = centerLon - dLon;
        maxLon = centerLon + dLon;
    }

    /// <summary>Formats a bbox string in the Overpass order (S,W,N,E).</summary>
    public static string FormatBbox(double minLat, double minLon, double maxLat, double maxLon) =>
        $"{minLat:F7},{minLon:F7},{maxLat:F7},{maxLon:F7}";

    // ── Section 3.6: Road network ──────────────────────────────────────

    /// <summary>
    /// Queries the road network within a bounding box.
    /// Returns ways with inline geometry so RoadGraph can build without a
    /// second node-lookup pass.
    /// </summary>
    public static string RoadsQuery(
        double minLat, double minLon,
        double maxLat, double maxLon,
        int timeoutSeconds = DefaultTimeoutSeconds)
    {
        string bbox = FormatBbox(minLat, minLon, maxLat, maxLon);
        return Header(timeoutSeconds) +
               "way[highway~\"motorway|trunk|primary|secondary|tertiary|" +
               "residential|unclassified|service\"]" +
               $"({bbox});" +
               "out geom;";
    }

    // ── Section 3.7: Bus routes ────────────────────────────────────────

    /// <summary>
    /// Queries OSM bus route relations with full member geometry.
    ///
    /// Uses <c>out body;>;out skel qt;</c> to resolve all member nodes so
    /// BusRouteParser can build stop positions without separate node queries.
    /// </summary>
    public static string BusRoutesQuery(
        double minLat, double minLon,
        double maxLat, double maxLon,
        int timeoutSeconds = DefaultTimeoutSeconds)
    {
        string bbox = FormatBbox(minLat, minLon, maxLat, maxLon);
        // "out geom" on relations doesn't always embed member node positions;
        // the body+skel approach is more reliable across Overpass instances.
        return Header(timeoutSeconds) +
               $"relation[route=bus]({bbox});" +
               "out body;>;out skel qt;";
    }

    /// <summary>
    /// Variant that requests inline geometry (faster, but some Overpass mirrors
    /// truncate large responses).  Use for small bounding boxes (&lt;5 km²).
    /// </summary>
    public static string BusRoutesQueryInline(
        double minLat, double minLon,
        double maxLat, double maxLon,
        int timeoutSeconds = DefaultTimeoutSeconds)
    {
        string bbox = FormatBbox(minLat, minLon, maxLat, maxLon);
        return Header(timeoutSeconds) +
               $"relation[route=bus]({bbox});" +
               "out geom;";
    }

    // ── Section 3.7: Bus stops (standalone) ───────────────────────────

    /// <summary>
    /// Queries individual bus stop nodes.  Useful for snapping imported stops
    /// to the road graph independently of route relations.
    /// </summary>
    public static string BusStopsQuery(
        double minLat, double minLon,
        double maxLat, double maxLon,
        int timeoutSeconds = 60)
    {
        string bbox = FormatBbox(minLat, minLon, maxLat, maxLon);
        return Header(timeoutSeconds) +
               $"node[highway=bus_stop]({bbox});" +
               $"node[public_transport=stop_position]({bbox});" +
               "out body;";
    }

    // ── Section 3.5 / Fuel amenity (FuelAmenityDownloader) ────────────

    /// <summary>Queries fuel stations (petrol + EV charging) in the bbox.</summary>
    public static string FuelAmenitiesQuery(
        double minLat, double minLon,
        double maxLat, double maxLon,
        int timeoutSeconds = 60)
    {
        string bbox = FormatBbox(minLat, minLon, maxLat, maxLon);
        return Header(timeoutSeconds) +
               $"node[amenity=fuel]({bbox});" +
               $"node[amenity=charging_station]({bbox});" +
               "out body;";
    }

    /// <summary>Queries traffic signal nodes in the bbox.</summary>
    public static string TrafficSignalsQuery(
        double minLat, double minLon,
        double maxLat, double maxLon,
        int timeoutSeconds = 60)
    {
        string bbox = FormatBbox(minLat, minLon, maxLat, maxLon);
        return Header(timeoutSeconds) +
               $"node[highway=traffic_signals]({bbox});" +
               "out body;";
    }

    // ── Convenience overloads ──────────────────────────────────────────

    /// <summary>Road query centred on a GPS point with half-extent in metres.</summary>
    public static string RoadsQueryFromCenter(
        double lat, double lon, float halfExtentMeters,
        int timeoutSeconds = DefaultTimeoutSeconds)
    {
        BoundingBoxFromCenter(lat, lon, halfExtentMeters,
            out var s, out var w, out var n, out var e);
        return RoadsQuery(s, w, n, e, timeoutSeconds);
    }

    /// <summary>Bus-routes query centred on a GPS point with half-extent in metres.</summary>
    public static string BusRoutesQueryFromCenter(
        double lat, double lon, float halfExtentMeters,
        int timeoutSeconds = DefaultTimeoutSeconds)
    {
        BoundingBoxFromCenter(lat, lon, halfExtentMeters,
            out var s, out var w, out var n, out var e);
        return BusRoutesQuery(s, w, n, e, timeoutSeconds);
    }
}
