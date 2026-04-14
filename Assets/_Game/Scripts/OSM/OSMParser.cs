using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public static class OSMParser
{
    public static OSMData Parse(string xml)
    {
        var data = new OSMData();

        try
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            // Parse nodes
            foreach (XmlNode node in doc.SelectNodes("//node"))
            {
                var osmNode = new OSMNode();
                osmNode.id = long.Parse(node.Attributes["id"].Value);
                osmNode.lat = double.Parse(node.Attributes["lat"].Value,
                    System.Globalization.CultureInfo.InvariantCulture);
                osmNode.lon = double.Parse(node.Attributes["lon"].Value,
                    System.Globalization.CultureInfo.InvariantCulture);
                data.nodes.Add(osmNode);
            }

            // Parse ways
            foreach (XmlNode wayNode in doc.SelectNodes("//way"))
            {
                var way = new OSMWay();
                way.id = long.Parse(wayNode.Attributes["id"].Value);

                foreach (XmlNode nd in wayNode.SelectNodes("nd"))
                    way.nodeRefs.Add(long.Parse(nd.Attributes["ref"].Value));

                foreach (XmlNode tag in wayNode.SelectNodes("tag"))
                {
                    string k = tag.Attributes["k"].Value;
                    string v = tag.Attributes["v"].Value;
                    way.tags[k] = v;
                }

                data.ways.Add(way);
            }

            data.BuildNodeMap();
            Debug.Log($"OSM parsed: {data.nodes.Count} nodes, {data.ways.Count} ways");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"OSM parse error: {e.Message}");
        }

        return data;
    }

    public static OSMData ParseOverpassJson(string json)
    {
        var data = new OSMData();

        try
        {
            var response = OverpassResponse.Deserialize(json);
            if (response?.elements == null)
                return data;

            long syntheticNodeId = 1;

            for (int i = 0; i < response.elements.Count; i++)
            {
                var elem = response.elements[i];
                if (elem == null || elem.type != "way" || elem.geometry == null || elem.geometry.Count < 2)
                    continue;

                var way = new OSMWay();
                way.id = elem.id;

                if (elem.tags != null)
                {
                    foreach (var kvp in elem.tags)
                        way.tags[kvp.Key] = kvp.Value;
                }

                for (int p = 0; p < elem.geometry.Count; p++)
                {
                    var point = elem.geometry[p];
                    var node = new OSMNode
                    {
                        id = syntheticNodeId++,
                        lat = point.lat,
                        lon = point.lon
                    };

                    data.nodes.Add(node);
                    way.nodeRefs.Add(node.id);
                }

                data.ways.Add(way);
            }

            data.BuildNodeMap();
            Debug.Log($"OSM JSON parsed: {data.nodes.Count} nodes, {data.ways.Count} ways");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"OSM JSON parse error: {e.Message}");
        }

        return data;
    }

    public static BusStopData[] ParseStopsJson(string json, double originLat, double originLon, int maxStops = 12)
    {
        var result = new List<BusStopData>();

        try
        {
            var response = OverpassResponse.Deserialize(json);
            if (response?.elements == null)
                return result.ToArray();

            for (int i = 0; i < response.elements.Count; i++)
            {
                var elem = response.elements[i];
                if (elem == null || elem.type != "node")
                    continue;

                string stopName = ResolveStopName(elem.tags, elem.id);
                result.Add(new BusStopData
                {
                    stopName = stopName,
                    latitude = elem.lat,
                    longitude = elem.lon,
                    waitTimeSeconds = 8f
                });
            }

            result = result
                .GroupBy(stop => $"{Mathf.RoundToInt((float)(stop.latitude * 100000f))}:{Mathf.RoundToInt((float)(stop.longitude * 100000f))}:{stop.stopName}")
                .Select(group => group.First())
                .OrderBy(stop => ApproxDistanceSquared(originLat, originLon, stop.latitude, stop.longitude))
                .ToList();

            if (maxStops > 1 && result.Count > maxStops)
                result = result.Take(maxStops).ToList();

            OrderStopsByNearestNeighbor(result, originLat, originLon);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Stops JSON parse error: {e.Message}");
        }

        return result.ToArray();
    }

    static string ResolveStopName(Dictionary<string, string> tags, long id)
    {
        if (tags != null)
        {
            if (tags.TryGetValue("name", out var name) && !string.IsNullOrWhiteSpace(name))
                return name.Trim();
            if (tags.TryGetValue("local_ref", out var localRef) && !string.IsNullOrWhiteSpace(localRef))
                return localRef.Trim();
            if (tags.TryGetValue("ref", out var stopRef) && !string.IsNullOrWhiteSpace(stopRef))
                return $"Stop {stopRef.Trim()}";
        }

        return $"Stop {id}";
    }

    static void OrderStopsByNearestNeighbor(List<BusStopData> stops, double originLat, double originLon)
    {
        if (stops == null || stops.Count < 3)
            return;

        var ordered = new List<BusStopData>(stops.Count);
        var remaining = new List<BusStopData>(stops);
        double cursorLat = originLat;
        double cursorLon = originLon;

        while (remaining.Count > 0)
        {
            int bestIndex = 0;
            double bestDistance = double.MaxValue;

            for (int i = 0; i < remaining.Count; i++)
            {
                double candidate = ApproxDistanceSquared(cursorLat, cursorLon, remaining[i].latitude, remaining[i].longitude);
                if (candidate < bestDistance)
                {
                    bestDistance = candidate;
                    bestIndex = i;
                }
            }

            var next = remaining[bestIndex];
            ordered.Add(next);
            cursorLat = next.latitude;
            cursorLon = next.longitude;
            remaining.RemoveAt(bestIndex);
        }

        stops.Clear();
        stops.AddRange(ordered);
    }

    static double ApproxDistanceSquared(double latA, double lonA, double latB, double lonB)
    {
        double dLat = (latB - latA) * 111320.0;
        double dLon = (lonB - lonA) * 111320.0 * System.Math.Cos((latA + latB) * 0.5 * System.Math.PI / 180.0);
        return dLat * dLat + dLon * dLon;
    }
}
