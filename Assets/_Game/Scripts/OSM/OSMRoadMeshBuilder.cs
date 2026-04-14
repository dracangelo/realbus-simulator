using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OSMRoadMeshBuilder : MonoBehaviour
{
    public static OSMRoadMeshBuilder Instance { get; private set; }

    [Header("Materials")]
    public Material roadMaterial;
    public Material pavementMaterial;

    [Header("Settings")]
    public float roadYOffset = 0.02f;
    public int roadLayer = 0;
    public string roadTag = "Road";
    public PhysicsMaterial roadPhysicMaterial;

    [Header("State")]
    public bool roadsBuilt = false;
    private GameObject roadsParent;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(WaitForOSMThenBuild());
    }

    IEnumerator WaitForOSMThenBuild()
    {
        // Wait for OSM data
        while (OSMLoader.Instance == null || !OSMLoader.Instance.dataLoaded)
            yield return new WaitForSeconds(0.5f);

        Debug.Log("OSM: Building road meshes...");
        BuildRoads(OSMLoader.Instance.osmData);
    }

    void BuildRoads(OSMData data)
    {
        roadsParent = new GameObject("OSM_Roads");
        roadsParent.transform.position = Vector3.zero;

        int roadCount = 0;

        foreach (var way in data.ways)
        {
            if (!way.IsRoad()) continue;

            List<Vector3> points = new List<Vector3>();

            foreach (long nodeRef in way.nodeRefs)
            {
                if (!data.nodeMap.ContainsKey(nodeRef)) continue;
                var node = data.nodeMap[nodeRef];
                Vector3 worldPos = GPSManager.Instance.GpsToWorld(node.lat, node.lon);
                worldPos.y = roadYOffset;
                points.Add(worldPos);
            }

            if (points.Count < 2) continue;

            float width = way.GetRoadWidth();
            Mesh mesh = BuildRoadSegmentMesh(points, width);

            if (mesh == null) continue;

            GameObject roadObj = new GameObject($"Road_{way.id}");
            roadObj.transform.parent = roadsParent.transform;

            var mf = roadObj.AddComponent<MeshFilter>();
            var mr = roadObj.AddComponent<MeshRenderer>();

            mf.mesh = mesh;
            mr.material = roadMaterial != null ? roadMaterial : CreateDefaultRoadMaterial();

            // Add mesh collider for driving on
            var mc = roadObj.AddComponent<MeshCollider>();
            mc.sharedMesh = mesh;
            if (roadPhysicMaterial != null)
                mc.sharedMaterial = roadPhysicMaterial;

            if (roadLayer > 0)
                roadObj.layer = roadLayer;
            if (!string.IsNullOrEmpty(roadTag) && IsValidTag(roadTag))
                roadObj.tag = roadTag;

            roadCount++;
        }

        roadsBuilt = true;
        Debug.Log($"OSM: Built {roadCount} road segments!");
    }

    bool IsValidTag(string tag)
    {
        if (string.IsNullOrEmpty(tag)) return false;
        try
        {
            GameObject.FindWithTag(tag);
            return true;
        }
        catch
        {
            return false;
        }
    }

    Shader ResolveDefaultRoadShader()
    {
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null) shader = Shader.Find("Standard");
        if (shader == null) shader = Shader.Find("Unlit/Color");
        return shader;
    }

    Material CreateDefaultRoadMaterial()
    {
        var shader = ResolveDefaultRoadShader();
        var mat = new Material(shader);
        mat.color = new Color(0.1f, 0.1f, 0.1f, 1f);
        if (mat.HasProperty("_Glossiness"))
            mat.SetFloat("_Glossiness", 0.0f);
        if (mat.HasProperty("_Metallic"))
            mat.SetFloat("_Metallic", 0.0f);
        return mat;
    }

    Mesh BuildRoadSegmentMesh(List<Vector3> points, float width)
    {
        if (points.Count < 2) return null;

        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        float halfWidth = width * 0.5f;
        float uvProgress = 0f;

        for (int i = 0; i < points.Count; i++)
        {
            Vector3 forward;

            if (i == 0)
                forward = (points[1] - points[0]).normalized;
            else if (i == points.Count - 1)
                forward = (points[i] - points[i - 1]).normalized;
            else
                forward = (points[i + 1] - points[i - 1]).normalized;

            Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;

            Vector3 left3D = points[i] - right * halfWidth;
            Vector3 right3D = points[i] + right * halfWidth;

            verts.Add(left3D);
            verts.Add(right3D);

            if (i > 0)
            {
                float segLen = Vector3.Distance(points[i], points[i - 1]);
                uvProgress += segLen / width;
            }

            uvs.Add(new Vector2(0f, uvProgress));
            uvs.Add(new Vector2(1f, uvProgress));

            if (i > 0)
            {
                int bl = (i - 1) * 2;
                int br = bl + 1;
                int tl = i * 2;
                int tr = tl + 1;

                tris.Add(bl); tris.Add(tl); tris.Add(br);
                tris.Add(br); tris.Add(tl); tris.Add(tr);
            }
        }

        Mesh mesh = new Mesh();
        mesh.name = "RoadSegment";
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}
