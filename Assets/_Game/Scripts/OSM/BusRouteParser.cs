using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parses <see cref="OverpassResponse"/> data into <see cref="ParsedBusRoute"/>
/// objects (section 3.7).
///
/// Also implements the route filter and difficulty classifier from section
/// 3.7A so the results can feed directly into OSMRouteImporter.
/// </summary>
public static class BusRouteParser
{
    // ── Filter thresholds (section 3.7A) ──────────────────────────────

    public const int   MinStops   = 6;
    public const int   MaxStops   = 40;
    public const float MinKm      = 3f;
    public const float MaxKm      = 25f;

    // ── Output model ──────────────────────────────────────────────────

    public class ParsedBusRoute
    {
        public string routeRef;
        public string routeName;
        public string destinationName;
        public string originName;

        /// <summary>Stops in order of travel.</summary>
        public List<BusStop> stops = new();

        /// <summary>Dense polyline path in GPS coordinates.</summary>
        public List<(double lat, double lon)> geometry = new();

        /// <summary>Approximate route length in km (computed from geometry).</summary>
        public float lengthKm;

        /// <summary>Estimated travel time in minutes (from speed limits / average speed).</summary>
        public float estimatedMinutes;

        /// <summary>Difficulty 1–5 (see classifier notes in section 3.7A).</summary>
        public int difficulty;

        /// <summary>True if this is a synthetic return route.</summary>
        public bool isReturn;
    }

    // ── Main entry point ──────────────────────────────────────────────

    /// <summary>
    /// Parses all bus routes from an Overpass response.
    /// Call <see cref="OverpassResponse.Resolve"/> before passing the response
    /// if you used <c>out body;>;out skel</c>.
    /// </summary>
    public static List<ParsedBusRoute> ParseRoutes(OverpassResponse response)
    {
        var results = new List<ParsedBusRoute>();
        if (response == null) return results;

        foreach (var elem in response.elements)
        {
            if (elem?.type != "relation") continue;
            if (elem.tags == null) continue;
            if (!elem.tags.TryGetValue("route", out var routeType) || routeType != "bus") continue;

            var parsed = ParseSingleRoute(elem);
            if (parsed == null) continue;

            ComputeLength(parsed);
            ClassifyDifficulty(parsed);
            results.Add(parsed);
        }

        return results;
    }

    /// <summary>
    /// Applies the route filter from section 3.7A:
    /// drops routes outside the stop/length bounds.
    /// </summary>
    public static List<ParsedBusRoute> FilterRoutes(List<ParsedBusRoute> routes)
    {
        var filtered = new List<ParsedBusRoute>(routes.Count);
        foreach (var r in routes)
        {
            if (r.stops.Count < MinStops || r.stops.Count > MaxStops) continue;
            if (r.lengthKm < MinKm      || r.lengthKm > MaxKm)        continue;
            filtered.Add(r);
        }
        return filtered;
    }

    /// <summary>
    /// Generates return routes by reversing stop order and geometry.
    /// The return route shares the same routeRef with " (Return)" appended.
    /// </summary>
    public static List<ParsedBusRoute> GenerateReturnRoutes(List<ParsedBusRoute> routes)
    {
        var returns = new List<ParsedBusRoute>(routes.Count);
        foreach (var r in routes)
        {
            if (r.stops.Count < 2) continue;

            var ret = new ParsedBusRoute
            {
                routeRef        = r.routeRef + "-R",
                routeName       = r.routeName + " (Return)",
                destinationName = r.originName,
                originName      = r.destinationName,
                lengthKm        = r.lengthKm,
                estimatedMinutes= r.estimatedMinutes,
                difficulty      = r.difficulty,
                isReturn        = true,
            };

            // Reverse stops.
            ret.stops.AddRange(r.stops);
            ret.stops.Reverse();

            // Reverse geometry.
            ret.geometry.AddRange(r.geometry);
            ret.geometry.Reverse();

            // Recompute headings for reversed stop order.
            if (ret.geometry.Count >= 2)
                RecomputeStopHeadings(ret);

            returns.Add(ret);
        }
        return returns;
    }

    // ── Private parsing ────────────────────────────────────────────────

    static ParsedBusRoute ParseSingleRoute(OverpassResponse.Element elem)
    {
        var parsed = new ParsedBusRoute
        {
            routeRef        = elem.tags.GetValueOrDefault("ref", ""),
            destinationName = elem.tags.GetValueOrDefault("to",  ""),
            originName      = elem.tags.GetValueOrDefault("from",""),
        };

        string name = elem.tags.GetValueOrDefault("name", "");
        parsed.routeName = !string.IsNullOrWhiteSpace(name)
            ? name
            : (string.IsNullOrEmpty(parsed.routeRef) ? "Bus Route" : $"Route {parsed.routeRef}");

        // ── Geometry (prefer inline, fall back to member ways) ─────────
        if (elem.geometry != null && elem.geometry.Count > 0)
        {
            foreach (var gp in elem.geometry)
                AppendPoint(parsed.geometry, gp.lat, gp.lon);
        }

        // ── Members ────────────────────────────────────────────────────
        if (elem.members != null)
        {
            foreach (var mem in elem.members)
            {
                if (mem == null) continue;

                // Stops.
                if (mem.type == "node" && IsStopRole(mem.role))
                {
                    double lat = mem.lat, lon = mem.lon;
                    if (lat == 0d && lon == 0d && mem.geometry?.Count > 0)
                    { lat = mem.geometry[0].lat; lon = mem.geometry[0].lon; }

                    if (lat == 0d && lon == 0d) continue;

                    string stopName = "";
                    mem.tags?.TryGetValue("name", out stopName);

                    parsed.stops.Add(new BusStop
                    {
                        stopId         = mem.@ref.ToString(),
                        stopName       = !string.IsNullOrWhiteSpace(stopName)
                                         ? stopName : $"Stop {mem.@ref}",
                        latitude       = lat,
                        longitude      = lon,
                        headingDegrees = 0f,
                    });
                    continue;
                }

                // Way geometry (only needed when inline geometry absent).
                if (mem.type == "way" && parsed.geometry.Count == 0
                    && mem.geometry != null && mem.geometry.Count > 0)
                {
                    bool reverse = mem.role == "backward" || mem.role == "reverse";
                    if (reverse)
                        for (int j = mem.geometry.Count - 1; j >= 0; j--)
                            AppendPoint(parsed.geometry, mem.geometry[j].lat, mem.geometry[j].lon);
                    else
                        for (int j = 0; j < mem.geometry.Count; j++)
                            AppendPoint(parsed.geometry, mem.geometry[j].lat, mem.geometry[j].lon);
                }
            }
        }

        if (string.IsNullOrWhiteSpace(parsed.destinationName) && parsed.stops.Count > 0)
            parsed.destinationName = parsed.stops[^1].stopName;

        if (string.IsNullOrWhiteSpace(parsed.originName) && parsed.stops.Count > 0)
            parsed.originName = parsed.stops[0].stopName;

        if (parsed.geometry.Count >= 2 && parsed.stops.Count > 0)
            RecomputeStopHeadings(parsed);

        return parsed;
    }

    static bool IsStopRole(string role) =>
        role == "stop"           || role == "platform"         ||
        role == "stop_entry_only"|| role == "stop_exit_only";

    // ── Geometry helpers ───────────────────────────────────────────────

    static void AppendPoint(List<(double lat, double lon)> list, double lat, double lon)
    {
        if (list.Count > 0 && list[^1].lat == lat && list[^1].lon == lon) return;
        list.Add((lat, lon));
    }

    // ── Length computation ─────────────────────────────────────────────

    static void ComputeLength(ParsedBusRoute route)
    {
        double totalMeters = 0;
        for (int i = 1; i < route.geometry.Count; i++)
        {
            totalMeters += HaversineMeters(
                route.geometry[i - 1].lat, route.geometry[i - 1].lon,
                route.geometry[i].lat,     route.geometry[i].lon);
        }
        route.lengthKm       = (float)(totalMeters / 1000.0);
        // Estimate travel time at 25 km/h average (urban bus incl. stops).
        route.estimatedMinutes = route.lengthKm / 25f * 60f;
    }

    static double HaversineMeters(double lat1, double lon1, double lat2, double lon2)
    {
        const double R  = 6_371_000.0;
        double phi1     = lat1 * System.Math.PI / 180.0;
        double phi2     = lat2 * System.Math.PI / 180.0;
        double dPhi     = (lat2 - lat1) * System.Math.PI / 180.0;
        double dLambda  = (lon2 - lon1) * System.Math.PI / 180.0;
        double a        = System.Math.Sin(dPhi / 2) * System.Math.Sin(dPhi / 2)
                        + System.Math.Cos(phi1) * System.Math.Cos(phi2)
                        * System.Math.Sin(dLambda / 2) * System.Math.Sin(dLambda / 2);
        return R * 2 * System.Math.Atan2(System.Math.Sqrt(a), System.Math.Sqrt(1 - a));
    }

    // ── Difficulty classifier (section 3.7A) ──────────────────────────

    /// <summary>
    /// 1 = easiest, 5 = hardest.
    /// Logic: urban dense (many stops, short gaps) = harder.
    /// Highway routes (fewer stops, high speed) = easier.
    /// </summary>
    static void ClassifyDifficulty(ParsedBusRoute route)
    {
        if (route.stops.Count == 0) { route.difficulty = 3; return; }

        float avgGapKm = route.stops.Count > 1
            ? route.lengthKm / (route.stops.Count - 1)
            : route.lengthKm;

        // Dense urban: tight stops, short total route.
        if (avgGapKm < 0.4f && route.lengthKm < 8f)
        { route.difficulty = 4; return; }

        // Suburban: moderate gap.
        if (avgGapKm < 0.8f)
        { route.difficulty = 3; return; }

        // Highway express: wide gaps, long route.
        if (avgGapKm > 1.5f && route.lengthKm > 12f)
        { route.difficulty = 2; return; }

        route.difficulty = 3; // default suburban
    }

    // ── Stop heading ──────────────────────────────────────────────────

    static void RecomputeStopHeadings(ParsedBusRoute route)
    {
        for (int i = 0; i < route.stops.Count; i++)
            route.stops[i].headingDegrees = EstimateHeading(route.stops[i], route.geometry);
    }

    static float EstimateHeading(BusStop stop, List<(double lat, double lon)> geom)
    {
        int best    = 0;
        double bestSqr = double.MaxValue;

        for (int i = 0; i < geom.Count; i++)
        {
            double dLat = geom[i].lat - stop.latitude;
            double dLon = geom[i].lon - stop.longitude;
            double sqr  = dLat * dLat + dLon * dLon;
            if (sqr < bestSqr) { bestSqr = sqr; best = i; }
        }

        int a = Mathf.Clamp(best, 0, geom.Count - 2);
        return (float)BearingDeg(geom[a].lat, geom[a].lon, geom[a + 1].lat, geom[a + 1].lon);
    }

    static double BearingDeg(double lat1, double lon1, double lat2, double lon2)
    {
        double phi1   = lat1 * System.Math.PI / 180.0;
        double phi2   = lat2 * System.Math.PI / 180.0;
        double dLon   = (lon2 - lon1) * System.Math.PI / 180.0;
        double y      = System.Math.Sin(dLon) * System.Math.Cos(phi2);
        double x      = System.Math.Cos(phi1) * System.Math.Sin(phi2)
                      - System.Math.Sin(phi1) * System.Math.Cos(phi2) * System.Math.Cos(dLon);
        return (System.Math.Atan2(y, x) * 180.0 / System.Math.PI + 360.0) % 360.0;
    }
}
