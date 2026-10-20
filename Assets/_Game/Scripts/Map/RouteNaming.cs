using System;

/// <summary>
/// One spelling for every route in the game. Nairobi's code is <c>NBO</c>, and the
/// route picker, the importer and the download screens all have to agree on that:
/// the old flow let an OSM relation keep its raw <c>ref</c> ("R0", "8") while the
/// city screen prepended the city code, so the same service appeared as
/// "NBO-R0" on one screen and "NBO-NBO-8" on another.
///
/// The rules are deliberately tolerant rather than clever. They never remove
/// information from a code; they only make sure the result begins with NBO and
/// that the separators are stable, so a route can be normalised repeatedly.
/// </summary>
public static class RouteNaming
{
    public const string CityPrefix = "NBO";

    /// <summary>Normalises a route number to the NBO namespace.</summary>
    public static string NormalizeCode(string raw, string fallback = null)
    {
        string code = Clean(raw);
        if (string.IsNullOrEmpty(code))
            code = Clean(fallback);
        if (string.IsNullOrEmpty(code))
            code = "R";

        code = code.ToUpperInvariant();
        string tail;
        if (code.StartsWith(CityPrefix, StringComparison.Ordinal))
        {
            tail = code.Substring(CityPrefix.Length).TrimStart('-', '_', ' ', ':', '/');
        }
        else
        {
            tail = code.TrimStart('-', '_', ' ', ':', '/');
        }

        // A second prefix can survive an older importer run. Strip it here so
        // NormalizeCode(NormalizeCode("NBO-NBO-8")) still ends at NBO-8.
        if (tail.StartsWith(CityPrefix, StringComparison.Ordinal))
            tail = tail.Substring(CityPrefix.Length).TrimStart('-', '_', ' ', ':', '/');

        return string.IsNullOrEmpty(tail) ? CityPrefix + "-R" : CityPrefix + "-" + tail;
    }

    /// <summary>Normalises a display name so every route starts with NBO.</summary>
    public static string NormalizeName(string raw)
    {
        string name = Clean(raw);
        if (string.IsNullOrEmpty(name))
            return CityPrefix + " Route";

        if (name.Equals(CityPrefix, StringComparison.OrdinalIgnoreCase) ||
            name.StartsWith(CityPrefix + " ", StringComparison.OrdinalIgnoreCase) ||
            name.StartsWith(CityPrefix + "-", StringComparison.OrdinalIgnoreCase) ||
            name.StartsWith(CityPrefix + "_", StringComparison.OrdinalIgnoreCase))
            return name;

        return CityPrefix + " " + name;
    }

    /// <summary>Applies both rules to an asset, including legacy assets loaded at runtime.</summary>
    public static void Apply(BusRoute route)
    {
        if (route == null)
            return;

        string fallback = route.osmRelationId > 0
            ? "OSM" + route.osmRelationId
            : route.generatedRouteId;
        route.routeNumber = NormalizeCode(route.routeNumber, fallback);
        route.routeName = NormalizeName(route.routeName);
    }

    /// <summary>The code the UI prints for a route, already carrying one NBO prefix.</summary>
    public static string DisplayCode(BusRoute route)
    {
        if (route == null)
            return CityPrefix + "-R";
        return NormalizeCode(route.routeNumber,
            route.osmRelationId > 0 ? "OSM" + route.osmRelationId : route.generatedRouteId);
    }

    static string Clean(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
    }
}
