using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

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
        if (!tags.ContainsKey("highway"))
            return false;

        string highway = tags["highway"];
        return highway != "footway"
            && highway != "cycleway"
            && highway != "path"
            && highway != "steps"
            && highway != "pedestrian"
            && highway != "construction"
            && highway != "bridleway";
    }

    public string GetRoadType()
    {
        return tags != null && tags.TryGetValue("highway", out var type) ? type : "unknown";
    }

    public bool IsOneWay()
    {
        if (tags == null)
            return false;

        if (tags.TryGetValue("oneway", out var oneway))
        {
            oneway = oneway.Trim().ToLowerInvariant();
            return oneway == "yes" || oneway == "1" || oneway == "true" || oneway == "-1";
        }

        string highway = GetRoadType();
        return highway == "motorway" || highway == "motorway_link";
    }

    public int GetOneWayDirectionSign()
    {
        if (tags == null || !tags.TryGetValue("oneway", out var oneway))
            return IsOneWay() ? 1 : 0;

        oneway = oneway.Trim().ToLowerInvariant();
        if (oneway == "-1")
            return -1;
        if (oneway == "yes" || oneway == "1" || oneway == "true")
            return 1;
        return 0;
    }

    public int GetLaneCount()
    {
        if (tags != null && tags.TryGetValue("lanes", out var lanesRaw) && int.TryParse(lanesRaw, NumberStyles.Integer, CultureInfo.InvariantCulture, out int lanes))
            return Mathf.Clamp(lanes, 1, 8);

        string highway = GetRoadType();
        switch (highway)
        {
            case "motorway":
            case "trunk":
                return IsOneWay() ? 2 : 4;
            case "primary":
            case "secondary":
                return 2;
            case "service":
                return 1;
            default:
                return 2;
        }
    }

    public float GetLaneWidthMeters()
    {
        if (tags != null && tags.TryGetValue("width", out var widthRaw))
        {
            string cleaned = widthRaw.ToLowerInvariant().Replace("m", "").Trim();
            if (float.TryParse(cleaned, NumberStyles.Float, CultureInfo.InvariantCulture, out float width))
                return Mathf.Clamp(width / Mathf.Max(1, GetLaneCount()), 2.6f, 4.2f);
        }

        string highway = GetRoadType();
        switch (highway)
        {
            case "motorway":
            case "trunk":
                return 3.6f;
            case "primary":
            case "secondary":
                return 3.3f;
            case "service":
                return 2.8f;
            default:
                return 3.1f;
        }
    }

    public bool IsJunction()
    {
        return tags != null && tags.TryGetValue("junction", out var junction) && !string.IsNullOrWhiteSpace(junction);
    }

    public bool TryGetSpeedLimitKmh(out float speedLimitKmh)
    {
        speedLimitKmh = 0f;
        if (tags == null || !tags.TryGetValue("maxspeed", out var maxSpeedRaw) || string.IsNullOrWhiteSpace(maxSpeedRaw))
            return false;

        string cleaned = maxSpeedRaw.Trim().ToLowerInvariant();
        bool isMph = cleaned.Contains("mph");
        cleaned = cleaned.Replace("km/h", "").Replace("kph", "").Replace("mph", "").Trim();
        if (!float.TryParse(cleaned, NumberStyles.Float, CultureInfo.InvariantCulture, out float parsed))
            return false;

        speedLimitKmh = isMph ? parsed * 1.60934f : parsed;
        return speedLimitKmh > 0f;
    }

    public string GetDisplayName()
    {
        if (tags != null && tags.TryGetValue("name", out var name) && !string.IsNullOrWhiteSpace(name))
            return name.Trim();

        return $"{GetRoadType()} #{id}";
    }

    public bool ShouldDrawCenterLine()
    {
        if (IsOneWay())
            return false;

        string highway = GetRoadType();
        return highway != "service" && highway != "track";
    }

    public bool ShouldGenerateMedianBarrier()
    {
        if (!IsOneWay())
            return false;

        int lanes = GetLaneCount();
        string highway = GetRoadType();
        return lanes >= 2 && (
            highway == "motorway" ||
            highway == "motorway_link" ||
            highway == "trunk" ||
            highway == "primary");
    }

    public float GetRoadWidth()
    {
        if (!IsRoad())
            return 6f;

        float laneWidth = GetLaneWidthMeters();
        float width = GetLaneCount() * laneWidth;

        if (tags != null && tags.TryGetValue("shoulder", out var shoulder) && (shoulder == "yes" || shoulder == "both"))
            width += 1.5f;
        if (ShouldGenerateMedianBarrier())
            width += 0.75f;

        return Mathf.Clamp(width, 4.5f, 24f);
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
    public Dictionary<long, int> roadNodeUseCounts = new Dictionary<long, int>();
    public HashSet<long> intersectionNodeIds = new HashSet<long>();

    public void BuildNodeMap()
    {
        nodeMap.Clear();
        foreach (var node in nodes)
            nodeMap[node.id] = node;

        BuildRoadTopology();
    }

    public bool IsIntersectionNode(long nodeId)
    {
        return intersectionNodeIds.Contains(nodeId);
    }

    public int GetConnectedRoadCount(long nodeId)
    {
        return roadNodeUseCounts.TryGetValue(nodeId, out int count) ? count : 0;
    }

    void BuildRoadTopology()
    {
        roadNodeUseCounts.Clear();
        intersectionNodeIds.Clear();

        foreach (var way in ways)
        {
            if (way == null || !way.IsRoad() || way.nodeRefs == null)
                continue;

            for (int i = 0; i < way.nodeRefs.Count; i++)
            {
                long nodeId = way.nodeRefs[i];
                if (roadNodeUseCounts.TryGetValue(nodeId, out int count))
                    roadNodeUseCounts[nodeId] = count + 1;
                else
                    roadNodeUseCounts[nodeId] = 1;
            }
        }

        foreach (var kvp in roadNodeUseCounts)
        {
            if (kvp.Value > 1)
                intersectionNodeIds.Add(kvp.Key);
        }
    }
}
