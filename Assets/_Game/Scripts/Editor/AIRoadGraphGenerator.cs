#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AIRoadGraphGenerator : EditorWindow
{
    string roadsJsonPath = "Assets/StreamingAssets/Cities/NBO/roads.json";
    string graphObjectName = "AIRoadGraph";
    string pointsParentName = "GeneratedNodes";
    float generatedY = 0.1f;
    int defaultLaneCount = 2;
    float defaultLaneWidth = 3.3f;

    [MenuItem("Tools/RealBus/Traffic/Generate AIRoadGraph From Roads JSON")]
    public static void Open()
    {
        GetWindow<AIRoadGraphGenerator>("AIRoadGraph Generator");
    }

    void OnGUI()
    {
        GUILayout.Label("Generate AIRoadGraph", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "Builds an AIRoadGraph from local Overpass roads JSON (`out geom`). " +
            "It creates waypoint transforms and fills nextNodeIndices automatically.",
            MessageType.Info);

        using (new EditorGUILayout.HorizontalScope())
        {
            roadsJsonPath = EditorGUILayout.TextField("Roads JSON", roadsJsonPath);
            if (GUILayout.Button("Browse", GUILayout.Width(70)))
                roadsJsonPath = PickJsonFile(roadsJsonPath);
        }

        graphObjectName = EditorGUILayout.TextField("Graph Object", graphObjectName);
        pointsParentName = EditorGUILayout.TextField("Points Parent", pointsParentName);
        generatedY = EditorGUILayout.FloatField("Point Y", generatedY);
        defaultLaneCount = EditorGUILayout.IntSlider("Default Lane Count", defaultLaneCount, 1, 4);
        defaultLaneWidth = EditorGUILayout.Slider("Default Lane Width", defaultLaneWidth, 2.5f, 4.5f);

        EditorGUILayout.Space();
        if (GUILayout.Button("Generate / Replace AIRoadGraph", GUILayout.Height(36)))
            GenerateGraph();
    }

    void GenerateGraph()
    {
        string resolved = ResolvePath(roadsJsonPath);
        if (string.IsNullOrWhiteSpace(resolved) || !System.IO.File.Exists(resolved))
        {
            EditorUtility.DisplayDialog("Missing Roads JSON", "Pick a valid local roads.json file first.", "OK");
            return;
        }

        string json = System.IO.File.ReadAllText(resolved);
        var response = OverpassResponse.Deserialize(json);
        if (response == null || response.elements == null || response.elements.Count == 0)
        {
            EditorUtility.DisplayDialog("Invalid Roads JSON", "The selected file did not contain Overpass elements.", "OK");
            return;
        }

        CoordinateConverter converter = FindOrCreateConverter(response);
        var graph = RoadGraph.BuildFromOverpassWays(response, converter);
        if (graph == null || graph.nodes.Count == 0)
        {
            EditorUtility.DisplayDialog("Graph Build Failed", "No road nodes were built from the selected roads.json.", "OK");
            return;
        }

        var nodeMetadata = BuildNodeMetadata(response);
        var root = FindOrCreateRoot(graphObjectName);
        var airGraph = root.GetComponent<AIRoadGraph>();
        if (airGraph == null)
            airGraph = root.AddComponent<AIRoadGraph>();

        var existing = root.transform.Find(pointsParentName);
        if (existing != null)
            DestroyImmediate(existing.gameObject);

        var pointsParent = new GameObject(pointsParentName).transform;
        pointsParent.SetParent(root.transform, false);

        var nodes = new AIRoadGraph.RoadNode[graph.nodes.Count];
        var pointTransforms = new Transform[graph.nodes.Count];

        for (int i = 0; i < graph.nodes.Count; i++)
        {
            var graphNode = graph.nodes[i];
            var point = new GameObject($"Node_{i:0000}").transform;
            point.SetParent(pointsParent, false);
            point.position = new Vector3(graphNode.world.x, generatedY, graphNode.world.z);
            pointTransforms[i] = point;
        }

        for (int i = 0; i < graph.nodes.Count; i++)
        {
            var graphNode = graph.nodes[i];
            var key = Quantize(graphNode.lat, graphNode.lon);
            nodeMetadata.TryGetValue(key, out var meta);

            nodes[i] = new AIRoadGraph.RoadNode
            {
                id = $"N{i:0000}",
                point = pointTransforms[i],
                laneCount = Mathf.Max(1, meta.laneCount > 0 ? meta.laneCount : defaultLaneCount),
                laneWidth = meta.laneWidth > 0f ? meta.laneWidth : defaultLaneWidth,
                speedLimitKmh = meta.speedLimitKmh > 0f ? meta.speedLimitKmh : GuessNodeSpeed(graphNode),
                trafficLight = null,
                nextNodeIndices = graphNode.edges.Select(e => e.to).Distinct().Where(idx => idx >= 0 && idx < graph.nodes.Count).ToArray()
            };
        }

        airGraph.nodes = nodes;
        EditorUtility.SetDirty(airGraph);
        Selection.activeGameObject = root;

        EditorUtility.DisplayDialog(
            "AIRoadGraph Generated",
            $"Created {nodes.Length} nodes on '{root.name}'.\n" +
            $"Next step: assign this AIRoadGraph to VehiclePool and validate a few nodes/intersections.",
            "OK");
    }

    Dictionary<(long, long), NodeMeta> BuildNodeMetadata(OverpassResponse response)
    {
        var map = new Dictionary<(long, long), NodeMeta>();
        foreach (var elem in response.elements)
        {
            if (elem == null || elem.type != "way" || elem.geometry == null || elem.geometry.Count == 0)
                continue;

            int laneCount = ParseLaneCount(elem.tags);
            float speed = ParseSpeedLimit(elem.tags);

            for (int i = 0; i < elem.geometry.Count; i++)
            {
                var p = elem.geometry[i];
                var key = Quantize(p.lat, p.lon);
                if (!map.TryGetValue(key, out var meta))
                    meta = new NodeMeta();

                meta.laneCount = Mathf.Max(meta.laneCount, laneCount);
                meta.laneWidth = defaultLaneWidth;
                meta.speedLimitKmh = Mathf.Max(meta.speedLimitKmh, speed);
                map[key] = meta;
            }
        }
        return map;
    }

    static int ParseLaneCount(Dictionary<string, string> tags)
    {
        if (tags != null && tags.TryGetValue("lanes", out var lanesRaw) && int.TryParse(lanesRaw, out int lanes))
            return Mathf.Clamp(lanes, 1, 6);
        return 2;
    }

    static float ParseSpeedLimit(Dictionary<string, string> tags)
    {
        if (tags != null && tags.TryGetValue("maxspeed", out var ms) && !string.IsNullOrWhiteSpace(ms))
        {
            string s = ms.ToLowerInvariant().Trim();
            bool mph = s.Contains("mph");
            s = s.Replace("km/h", "").Replace("kph", "").Replace("mph", "").Trim();
            if (float.TryParse(s, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float value))
                return mph ? value * 1.60934f : value;
        }
        return 0f;
    }

    static float GuessNodeSpeed(RoadGraph.Node node)
    {
        if (node == null || node.edges == null || node.edges.Count == 0)
            return 30f;
        return Mathf.Max(20f, node.edges.Max(e => e.speedLimitKmh));
    }

    CoordinateConverter FindOrCreateConverter(OverpassResponse response)
    {
        var converter = FindFirstObjectByType<CoordinateConverter>();
        if (converter != null)
            return converter;

        double minLat = double.MaxValue;
        double minLon = double.MaxValue;
        double maxLat = double.MinValue;
        double maxLon = double.MinValue;

        foreach (var elem in response.elements)
        {
            if (elem?.geometry == null) continue;
            for (int i = 0; i < elem.geometry.Count; i++)
            {
                var p = elem.geometry[i];
                minLat = System.Math.Min(minLat, p.lat);
                minLon = System.Math.Min(minLon, p.lon);
                maxLat = System.Math.Max(maxLat, p.lat);
                maxLon = System.Math.Max(maxLon, p.lon);
            }
        }

        var go = new GameObject("CoordinateConverter_EditorTemp");
        converter = go.AddComponent<CoordinateConverter>();
        converter.mapOrigin = ScriptableObject.CreateInstance<MapOrigin>();
        converter.mapOrigin.originLat = (minLat + maxLat) * 0.5;
        converter.mapOrigin.originLon = (minLon + maxLon) * 0.5;
        return converter;
    }

    static GameObject FindOrCreateRoot(string name)
    {
        var root = GameObject.Find(name);
        return root != null ? root : new GameObject(name);
    }

    static (long, long) Quantize(double lat, double lon)
    {
        return ((long)System.Math.Round(lat * 1e7), (long)System.Math.Round(lon * 1e7));
    }

    static string ResolvePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return null;
        if (System.IO.Path.IsPathRooted(path))
            return path;
        string projectRoot = System.IO.Directory.GetParent(Application.dataPath).FullName;
        return System.IO.Path.GetFullPath(System.IO.Path.Combine(projectRoot, path));
    }

    static string PickJsonFile(string current)
    {
        string startDir = System.IO.Directory.GetParent(Application.dataPath).FullName;
        string resolved = ResolvePath(current);
        if (!string.IsNullOrWhiteSpace(resolved) && System.IO.File.Exists(resolved))
            startDir = System.IO.Path.GetDirectoryName(resolved);

        string picked = EditorUtility.OpenFilePanel("Select roads.json", startDir, "json");
        if (string.IsNullOrWhiteSpace(picked))
            return current;

        string projectRoot = System.IO.Directory.GetParent(Application.dataPath).FullName + System.IO.Path.DirectorySeparatorChar;
        string full = System.IO.Path.GetFullPath(picked);
        return full.StartsWith(projectRoot) ? full.Substring(projectRoot.Length) : full;
    }

    struct NodeMeta
    {
        public int laneCount;
        public float laneWidth;
        public float speedLimitKmh;
    }
}
#endif
