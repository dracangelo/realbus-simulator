using System.Collections.Generic;
using UnityEngine;

public static class BusRouteParser
{
    public class ParsedBusRoute
    {
        public string routeRef;
        public string routeName;
        public List<BusStop> stops = new List<BusStop>();
        public List<(double lat, double lon)> geometry = new List<(double, double)>();
    }

    public static List<ParsedBusRoute> ParseRoutes(OverpassResponse response)
    {
        var results = new List<ParsedBusRoute>();
        if (response == null) return results;

        foreach (var elem in response.elements)
        {
            if (elem == null || elem.type != "relation") continue;
            if (elem.tags == null) continue;
            if (!elem.tags.TryGetValue("route", out var routeType) || routeType != "bus") continue;

            var parsed = new ParsedBusRoute();
            parsed.routeRef = elem.tags.TryGetValue("ref", out var r) ? r : "";
            parsed.routeName = elem.tags.TryGetValue("name", out var n) ? n : (string.IsNullOrEmpty(parsed.routeRef) ? "Bus Route" : $"Route {parsed.routeRef}");

            if (elem.geometry != null && elem.geometry.Count > 0)
            {
                for (int i = 0; i < elem.geometry.Count; i++)
                    parsed.geometry.Add((elem.geometry[i].lat, elem.geometry[i].lon));
            }

            // Stops: Overpass relations typically have members with role stop/platform.
            if (elem.members != null)
            {
                foreach (var mem in elem.members)
                {
                    if (mem == null) continue;
                    if (mem.type != "node") continue;
                    if (mem.role != "stop" && mem.role != "platform" && mem.role != "stop_entry_only" && mem.role != "stop_exit_only")
                        continue;

                    // Member geometry is not guaranteed; if missing we skip (requires separate node query).
                    if (mem.geometry == null || mem.geometry.Count == 0) continue;
                    var p = mem.geometry[0];

                    var stop = new BusStop
                    {
                        stopId = mem.@ref.ToString(),
                        stopName = mem.tags != null && mem.tags.TryGetValue("name", out var sn) ? sn : $"Stop {mem.@ref}",
                        latitude = p.lat,
                        longitude = p.lon,
                        headingDegrees = 0f
                    };
                    parsed.stops.Add(stop);
                }
            }

            // Compute stop headings using route geometry when available.
            if (parsed.geometry.Count >= 2 && parsed.stops.Count > 0)
            {
                for (int i = 0; i < parsed.stops.Count; i++)
                    parsed.stops[i].headingDegrees = EstimateHeadingAtStop(parsed.stops[i], parsed.geometry);
            }

            results.Add(parsed);
        }

        return results;
    }

    static float EstimateHeadingAtStop(BusStop stop, List<(double lat, double lon)> geom)
    {
        // Find nearest segment and compute bearing.
        int best = -1;
        double bestSqr = double.MaxValue;
        for (int i = 0; i < geom.Count; i++)
        {
            double dLat = geom[i].lat - stop.latitude;
            double dLon = geom[i].lon - stop.longitude;
            double sqr = dLat * dLat + dLon * dLon;
            if (sqr < bestSqr)
            {
                bestSqr = sqr;
                best = i;
            }
        }

        int a = Mathf.Clamp(best, 0, geom.Count - 2);
        int b = a + 1;
        return (float)BearingDegrees(geom[a].lat, geom[a].lon, geom[b].lat, geom[b].lon);
    }

    static double BearingDegrees(double lat1, double lon1, double lat2, double lon2)
    {
        double phi1 = lat1 * System.Math.PI / 180.0;
        double phi2 = lat2 * System.Math.PI / 180.0;
        double dLon = (lon2 - lon1) * System.Math.PI / 180.0;

        double y = System.Math.Sin(dLon) * System.Math.Cos(phi2);
        double x = System.Math.Cos(phi1) * System.Math.Sin(phi2) - System.Math.Sin(phi1) * System.Math.Cos(phi2) * System.Math.Cos(dLon);
        double brng = System.Math.Atan2(y, x) * 180.0 / System.Math.PI;
        brng = (brng + 360.0) % 360.0;
        return brng;
    }
}

