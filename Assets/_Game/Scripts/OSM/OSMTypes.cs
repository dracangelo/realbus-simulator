using System.Collections.Generic;

[System.Serializable]
public class OSMNode
{
    public long id;
    public double lat;
    public double lon;
}

[System.Serializable]
public class OSMWay
{
    public long id;
    public List<long> nodeRefs = new List<long>();
    public Dictionary<string, string> tags = new Dictionary<string, string>();

    public bool IsRoad()
    {
        return tags.ContainsKey("highway");
    }

    public float GetRoadWidth()
    {
        if (!tags.ContainsKey("highway")) return 6f;

        switch (tags["highway"])
        {
            case "trunk":
            case "primary":       return 20f;
            case "secondary":     return 15f;
            case "tertiary":      return 10f;
            case "residential":   return 7f;
            case "service":       return 5f;
            default:              return 8f;
        }
    }

    public bool IsBuilding()
    {
        return tags.ContainsKey("building");
    }
}

[System.Serializable]
public class OSMData
{
    public List<OSMNode> nodes = new List<OSMNode>();
    public List<OSMWay> ways = new List<OSMWay>();
    public Dictionary<long, OSMNode> nodeMap = new Dictionary<long, OSMNode>();

    public void BuildNodeMap()
    {
        nodeMap.Clear();
        foreach (var node in nodes)
            nodeMap[node.id] = node;
    }
}