using System;
using System.Collections.Generic;
using System.Globalization;

/// <summary>Converts a city-scoped Overture GeoJSON export to our footprint format.</summary>
public static class OvertureBuildingConverter
{
    public static string ConvertGeoJson(string json, CityDefinition city, out int imported, out int skipped)
    {
        if (city == null || city.minLat >= city.maxLat || city.minLon >= city.maxLon)
            throw new ArgumentException("Select a city with a valid bounding box.");
        var root = Json.Deserialize(json) as Dictionary<string, object>;
        if (root == null || Text(root, "type") != "FeatureCollection" ||
            !root.TryGetValue("features", out var featuresValue) || !(featuresValue is List<object> features))
            throw new FormatException("Expected a GeoJSON FeatureCollection (export with -f geojson).");
        var elements = new List<object>();
        var ids = new HashSet<string>();
        imported = skipped = 0;
        foreach (var value in features)
        {
            if (!(value is Dictionary<string, object> feature) ||
                !feature.TryGetValue("geometry", out var geometryValue) || !(geometryValue is Dictionary<string, object> geometry) ||
                !geometry.TryGetValue("coordinates", out var coordsValue) || !(coordsValue is List<object> coords))
            { skipped++; continue; }
            var properties = feature.TryGetValue("properties", out var p) ? p as Dictionary<string, object> : null;
            string type = Text(geometry, "type");
            var polygons = type == "Polygon" ? new List<object> { coords } : type == "MultiPolygon" ? coords : null;
            if (polygons == null) { skipped++; continue; }
            string sourceId = Text(properties, "id");
            if (string.IsNullOrEmpty(sourceId)) sourceId = Text(feature, "id");
            for (int part = 0; part < polygons.Count; part++)
            {
                // The current extrusion mesher cannot preserve courtyards. Skip
                // holed or malformed polygons rather than covering their holes.
                if (!(polygons[part] is List<object> rings) || rings.Count != 1 ||
                    !(rings[0] is List<object> ring) || !TryRing(ring, city, out var points))
                { skipped++; continue; }
                string identity = string.IsNullOrEmpty(sourceId) ? Json.Serialize(points) : sourceId + ":" + part;
                if (!ids.Add(identity)) { skipped++; continue; }
                var tags = new Dictionary<string, object> { ["building"] = "yes", ["source"] = "Overture Maps", ["overture:id"] = sourceId };
                if (properties != null && properties.TryGetValue("height", out var h) && Number(h, out double height) && height > 0 && height <= 1000)
                    tags["height"] = height.ToString(CultureInfo.InvariantCulture);
                var element = new Dictionary<string, object> {
                    ["type"] = "way", ["id"] = -(long)(++imported), ["tags"] = tags, ["geometry"] = points
                };
                if (properties != null && properties.TryGetValue("sources", out var sources)) element["overture_sources"] = sources;
                elements.Add(element);
            }
        }
        if (imported == 0) throw new FormatException("No usable footprints inside the selected city's bounds. Existing data was not changed.");
        return Json.Serialize(new Dictionary<string, object> {
            ["generator"] = "RealBus Overture import", ["cityCode"] = city.cityCode,
            ["attribution"] = "Overture Maps Foundation and contributing sources; see overture_sources per building.",
            ["attribution_url"] = "https://docs.overturemaps.org/attribution/",
            ["elements"] = elements
        });
    }

    static string Text(Dictionary<string, object> obj, string key) =>
        obj != null && obj.TryGetValue(key, out var value) ? value as string : null;

    static bool Number(object value, out double number) =>
        double.TryParse(System.Convert.ToString(value, CultureInfo.InvariantCulture), NumberStyles.Float,
            CultureInfo.InvariantCulture, out number) && !double.IsNaN(number) && !double.IsInfinity(number);

    static bool TryRing(List<object> ring, CityDefinition city, out List<object> points)
    {
        points = new List<object>();
        if (ring.Count < 4 || ring.Count > 2048) return false;
        double minX = double.MaxValue, maxX = double.MinValue, minY = double.MaxValue, maxY = double.MinValue;
        double firstX = 0, firstY = 0, previousX = 0, previousY = 0, area = 0;
        for (int i = 0; i < ring.Count; i++)
        {
            if (!(ring[i] is List<object> coordinate) || coordinate.Count < 2 ||
                !Number(coordinate[0], out double x) || !Number(coordinate[1], out double y) ||
                x < -180 || x > 180 || y < -90 || y > 90) return false;
            if (i == 0) { firstX = x; firstY = y; }
            else area += (previousX - firstX) * (y - firstY) - (x - firstX) * (previousY - firstY);
            previousX = x; previousY = y;
            minX = Math.Min(minX, x); maxX = Math.Max(maxX, x);
            minY = Math.Min(minY, y); maxY = Math.Max(maxY, y);
            points.Add(new Dictionary<string, object> { ["lon"] = x, ["lat"] = y });
        }
        // Bound polygon size to protect the mobile mesh/spatial index budget.
        if (previousX != firstX || previousY != firstY || Math.Abs(area) < 1e-12 || maxX - minX > .02 || maxY - minY > .02) return false;
        double cx = (minX + maxX) * .5, cy = (minY + maxY) * .5;
        return cx >= city.minLon && cx <= city.maxLon && cy >= city.minLat && cy <= city.maxLat;
    }
}
