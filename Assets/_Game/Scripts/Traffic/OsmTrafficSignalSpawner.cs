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
    [System.NonSerialized] public RoadGraph graph;
    [Tooltip("AI graph the spawned signals control. When unset, the largest city graph is used.")]
    public AIRoadGraph aiGraph;
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
            var signal = CreateSignal(transform, projected - direction * 4f, Quaternion.LookRotation(direction), node.id % 85);
            BindToAiGraph(signal);
            yield return null;
        }
    }

    void BindToAiGraph(TrafficLight signal)
    {
        if (signal == null) return;
        if (aiGraph == null) aiGraph = AIRoadGraph.FindCityGraph();
        if (aiGraph != null) aiGraph.TryBindTrafficSignal(signal, 30f);
    }

    const string ImportedSignalResourcePath = "TrafficLightsGenerated/TrafficLightModel";
    static GameObject importedSignalPrefab;
    static bool attemptedImportedSignal;
    static Material poleMaterial;

    static GameObject LoadImportedSignal()
    {
        if (attemptedImportedSignal) return importedSignalPrefab;
        attemptedImportedSignal = true;
        importedSignalPrefab = Resources.Load<GameObject>(ImportedSignalResourcePath);
        return importedSignalPrefab;
    }

    public static TrafficLight CreateSignal(Transform parent, Vector3 position, Quaternion rotation, float phase)
    {
        var root = new GameObject("Traffic signal");
        root.transform.SetParent(parent, false); root.transform.position = position; root.transform.rotation = rotation;
        var light = root.AddComponent<TrafficLight>(); light.phaseOffset = phase;
        var detector = root.AddComponent<SignalViolationDetector>(); detector.trafficLight = light;

        GameObject housing = LoadImportedSignal();
        Renderer[] lenses = new Renderer[3];
        if (housing != null)
        {
            // The imported retro signal is the housing and its short mast. Raise it
            // on a procedural pole so the lenses sit at a normal signal height. The
            // root's +Z points along the road, so the housing is turned to face the
            // traffic approaching from the other direction.
            var instance = Object.Instantiate(housing, root.transform);
            instance.name = "Signal housing";
            instance.transform.localPosition = new Vector3(4f, 2.15f, 0f);
            // The model's face points along its local +Z. Turning it to root -Z
            // points the face back along the road, at the traffic approaching the
            // junction from behind the signal.
            instance.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            AddPole(root.transform, 4f);
            // The housing's three lens openings sit in its top 0.68 m, one per
            // 0.23 m slice, and its face is 0.17 m in front of the pivot line. The
            // emissive spheres go on those openings rather than on the mast.
            for (int i = 0; i < 3; i++)
            {
                var lens = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Object.Destroy(lens.GetComponent<Collider>());
                lens.transform.SetParent(root.transform, false);
                lens.transform.localPosition = new Vector3(4f, 4.76f - i * 0.23f, -0.17f);
                lens.transform.localScale = Vector3.one * 0.20f;
                lenses[i] = lens.GetComponent<Renderer>();
            }
        }
        else
        {
            // Procedural fallback when the pack has not been imported yet. Keeps the
            // old placement so a missing optional pack cannot break a signal node.
            for (int i = 0; i < 3; i++)
            {
                var lens = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Object.Destroy(lens.GetComponent<Collider>());
                lens.transform.SetParent(root.transform, false);
                lens.transform.localPosition = new Vector3(4f, 4f - i * 0.6f, 0f);
                lens.transform.localScale = Vector3.one * 0.45f;
                lenses[i] = lens.GetComponent<Renderer>();
            }
        }

        light.redLight = lenses[0]; light.amberLight = lenses[1]; light.greenLight = lenses[2];
        return light;
    }

    static void AddPole(Transform parent, float localX)
    {
        var pole = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Object.Destroy(pole.GetComponent<Collider>());
        pole.name = "Signal pole";
        pole.transform.SetParent(parent, false);
        pole.transform.localPosition = new Vector3(localX, 1.075f, 0f);
        pole.transform.localScale = new Vector3(0.22f, 1.075f, 0.22f);
        var renderer = pole.GetComponent<Renderer>();
        if (renderer == null) return;
        renderer.sharedMaterial = PoleMaterial();
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    static Material PoleMaterial()
    {
        if (poleMaterial != null) return poleMaterial;
        Shader shader = BuiltInShaders.Lit;
        if (shader == null) return null;
        poleMaterial = new Material(shader) { name = "TrafficSignalPole" };
        poleMaterial.color = new Color(.20f, .21f, .22f, 1f);
        if (poleMaterial.HasProperty("_Metallic")) poleMaterial.SetFloat("_Metallic", .45f);
        if (poleMaterial.HasProperty("_Glossiness")) poleMaterial.SetFloat("_Glossiness", .35f);
        return poleMaterial;
    }
}
