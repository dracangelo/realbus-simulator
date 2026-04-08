using System.Collections.Generic;
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
}