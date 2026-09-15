using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>OSM signal-node adapter. Signal timing is simulated; OSM does not provide live phases.</summary>
public class OsmTrafficSignalSpawner : MonoBehaviour
{
    public CoordinateConverter converter;
    public TextAsset localSignalJson;
    public string endpoint = "https://overpass-api.de/api/interpreter";
    public float radiusMeters = 2000f;
    public RoadGraph graph;
    readonly HashSet<long> spawned = new HashSet<long>();

    [ContextMenu("Load traffic signals")]
    public void LoadSignals() { StartCoroutine(Load()); }
    IEnumerator Load()
    {
        if (converter == null) converter = CoordinateConverter.Instance;
        if (converter == null || converter.mapOrigin == null || graph == null) yield break;
        string json = localSignalJson != null ? localSignalJson.text : null;
        if (json == null)
        {
            var origin = converter.mapOrigin;
            OverpassQueryBuilder.BoundingBoxFromCenter(origin.originLat, origin.originLon, radiusMeters, out var s, out var w, out var n, out var e);
            var form = new WWWForm(); form.AddField("data", OverpassQueryBuilder.TrafficSignalsQuery(s, w, n, e));
            using (var request = UnityWebRequest.Post(endpoint, form))
            {
                request.timeout = 130;
                yield return request.SendWebRequest();
                if (request.result != UnityWebRequest.Result.Success) { Debug.LogWarning("Signal query failed: " + request.error); yield break; }
                json = request.downloadHandler.text;
            }
        }
        foreach (var node in OverpassResponse.Deserialize(json).elements)
        {
            if (node.type != "node" || !node.tags.TryGetValue("highway", out var highway) || highway != "traffic_signals" || !spawned.Add(node.id)) continue;
            var position = converter.GeoToWorldPosition(node.lat, node.lon);
            if (!graph.TryProjectToNearestSegment(position, out var projected, out int a, out int b, out float distance) || distance > 30f) continue;
            var direction = (graph.nodes[b].world - graph.nodes[a].world).normalized;
            CreateSignal(transform, projected - direction * 4f, Quaternion.LookRotation(direction), node.id % 85);
            yield return null;
        }
    }

    public static TrafficLight CreateSignal(Transform parent, Vector3 position, Quaternion rotation, float phase)
    {
        var root = new GameObject("Traffic signal");
        root.transform.SetParent(parent, false); root.transform.position = position; root.transform.rotation = rotation;
        var light = root.AddComponent<TrafficLight>(); light.phaseOffset = phase;
        var detector = root.AddComponent<SignalViolationDetector>(); detector.trafficLight = light;
        Renderer[] lenses = new Renderer[3];
        for (int i = 0; i < 3; i++)
        {
            var lens = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Object.Destroy(lens.GetComponent<Collider>());
            lens.transform.SetParent(root.transform, false);
            lens.transform.localPosition = new Vector3(4f, 4f - i * 0.6f, 0f);
            lens.transform.localScale = Vector3.one * 0.45f;
            lenses[i] = lens.GetComponent<Renderer>();
        }
        light.redLight = lenses[0]; light.amberLight = lenses[1]; light.greenLight = lenses[2];
        return light;
    }
}
