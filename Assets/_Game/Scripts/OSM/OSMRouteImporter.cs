using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class OSMRouteImporter : MonoBehaviour
{
    public static OSMRouteImporter Instance { get; private set; }

    [Header("Settings")]
    public bool autoImportOnStart = false;

    [Header("State")]
    public bool importComplete = false;
    public List<BusRoute> importedRoutes = new List<BusRoute>();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        if (autoImportOnStart)
            StartCoroutine(WaitThenImport());
    }
    IEnumerator WaitThenImport()
    {
        // Wait until CityManager has an active city
        while (CityManager.Instance == null || CityManager.Instance.activeCity == null)
            yield return null;
        yield return StartCoroutine(ImportRoutesForActiveCity());
    }

    public void TriggerImport()
    {
        StartCoroutine(ImportRoutesForActiveCity());
    }

    IEnumerator ImportRoutesForActiveCity()
    {
        if (CityManager.Instance == null || CityManager.Instance.activeCity == null)
        {
            Debug.LogError("OSMRouteImporter: No active city!");
            yield break;
        }

        var city = CityManager.Instance.activeCity;
        Debug.Log($"OSMRouteImporter: Fetching routes for {city.cityName}...");

        string query = $"[out:json][timeout:60];" +
                       $"relation[\"route\"=\"bus\"]" +
                       $"({city.minLat},{city.minLon},{city.maxLat},{city.maxLon});" +
                       $"out body;>;out skel qt;";

        string url = "https://overpass-api.de/api/interpreter";
        string postData = "data=" + UnityWebRequest.EscapeURL(query);

        using (var request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(postData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type",
                "application/x-www-form-urlencoded");
            request.timeout = 60;

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"OSMRouteImporter: Fetch failed — {request.error}");
                yield break;
            }

            string json = request.downloadHandler.text;
            Debug.Log($"OSMRouteImporter: Received {json.Length} bytes");
            yield return StartCoroutine(ParseAndCreateRoutes(json));
        }
    }

    IEnumerator ParseAndCreateRoutes(string json)
    {
        // Parse JSON manually — Unity's JsonUtility doesn't handle OSM format
        var parsed = ParseOSMJson(json);

        Debug.Log($"OSMRouteImporter: Found {parsed.relations.Count} routes, " +
                  $"{parsed.nodes.Count} nodes");

        int created = 0;

        foreach (var relation in parsed.relations)
        {
            // Build stop list from member nodes
            List<BusStopData> stops = new List<BusStopData>();

            foreach (var member in relation.members)
            {
                if (member.role != "stop" && member.role != "stop_entry_only"
                    && member.role != "stop_exit_only") continue;

                if (!parsed.nodes.ContainsKey(member.nodeId)) continue;

                var node = parsed.nodes[member.nodeId];
                var stop = new BusStopData();
                stop.stopName = node.name ?? $"Stop {member.nodeId}";
                stop.latitude = node.lat;
                stop.longitude = node.lon;
                stops.Add(stop);
            }

            if (stops.Count < 2) continue;

            // Create BusRoute object in memory
            BusRoute route = ScriptableObject.CreateInstance<BusRoute>();
            route.name = $"Route_{relation.tags.GetValueOrDefault("ref", relation.id.ToString())}";
            route.routeName = relation.tags.GetValueOrDefault("name",
                $"Route {relation.tags.GetValueOrDefault("ref", "?")}");
            route.baseFare = 50f;
            route.stops = stops.ToArray();

            importedRoutes.Add(route);
            created++;

            Debug.Log($"OSMRouteImporter: Created {route.routeName} " +
                      $"({stops.Count} stops)");

            // Yield every 10 routes to avoid hitching
            if (created % 10 == 0)
                yield return null;
        }

        importComplete = true;
        Debug.Log($"OSMRouteImporter: Done — {created} routes imported!");

        // Add imported routes to active city
        if (CityManager.Instance?.activeCity != null)
        {
            var existingRoutes = CityManager.Instance.activeCity.availableRoutes ?? 
                                 new BusRoute[0];
            var allRoutes = new BusRoute[existingRoutes.Length + importedRoutes.Count];
            existingRoutes.CopyTo(allRoutes, 0);
            importedRoutes.CopyTo(allRoutes, existingRoutes.Length);
            CityManager.Instance.activeCity.availableRoutes = allRoutes;
            Debug.Log($"City routes updated: {allRoutes.Length} total");
        }
    }

    // ── Minimal OSM JSON parser ──────────────────────────────────────

    class OSMNodeData
    {
        public long id;
        public double lat, lon;
        public string name;
    }

    class OSMMember
    {
        public long nodeId;
        public string role;
    }

    class OSMRelation
    {
        public long id;
        public List<OSMMember> members = new List<OSMMember>();
        public Dictionary<string, string> tags = new Dictionary<string, string>();
    }

    class OSMParsed
    {
        public Dictionary<long, OSMNodeData> nodes = new Dictionary<long, OSMNodeData>();
        public List<OSMRelation> relations = new List<OSMRelation>();
    }

    OSMParsed ParseOSMJson(string json)
    {
        var result = new OSMParsed();

        try
        {
            // Use SimpleJSON-style manual parsing via Unity's built-in approach
            var root = Json.Deserialize(json) as Dictionary<string, object>;
            if (root == null) return result;

            var elements = root["elements"] as List<object>;
            if (elements == null) return result;

            foreach (var elem in elements)
            {
                var e = elem as Dictionary<string, object>;
                if (e == null) continue;

                string type = e["type"] as string;

                if (type == "node")
                {
                    var node = new OSMNodeData();
                    node.id = System.Convert.ToInt64(e["id"]);
                    node.lat = System.Convert.ToDouble(e["lat"]);
                    node.lon = System.Convert.ToDouble(e["lon"]);

                    if (e.ContainsKey("tags"))
                    {
                        var tags = e["tags"] as Dictionary<string, object>;
                        if (tags != null && tags.ContainsKey("name"))
                            node.name = tags["name"] as string;
                    }

                    result.nodes[node.id] = node;
                }
                else if (type == "relation")
                {
                    var relation = new OSMRelation();
                    relation.id = System.Convert.ToInt64(e["id"]);

                    if (e.ContainsKey("members"))
                    {
                        var members = e["members"] as List<object>;
                        if (members != null)
                        {
                            foreach (var m in members)
                            {
                                var md = m as Dictionary<string, object>;
                                if (md == null) continue;
                                if (md["type"] as string != "node") continue;

                                var member = new OSMMember();
                                member.nodeId = System.Convert.ToInt64(md["ref"]);
                                member.role = md["role"] as string ?? "";
                                relation.members.Add(member);
                            }
                        }
                    }

                    if (e.ContainsKey("tags"))
                    {
                        var tags = e["tags"] as Dictionary<string, object>;
                        if (tags != null)
                            foreach (var kvp in tags)
                                relation.tags[kvp.Key] = kvp.Value as string ?? "";
                    }

                    result.relations.Add(relation);
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"OSMRouteImporter parse error: {ex.Message}");
        }

        return result;
    }
}
