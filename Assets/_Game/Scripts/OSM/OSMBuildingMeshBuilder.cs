using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OSMBuildingMeshBuilder : MonoBehaviour
{
    public static OSMBuildingMeshBuilder Instance { get; private set; }

    [Header("Materials")]
    public Material buildingMaterial;
    public Material roofMaterial;

    [Header("Settings")]
    public float defaultBuildingHeight = 10f;
    public float floorHeight = 3f;
    public int maxBuildingsPerFrame = 50;

    [Header("State")]
    public bool buildingsBuilt = false;
    private GameObject buildingsParent;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(WaitForDataThenBuild());
    }

    IEnumerator WaitForDataThenBuild()
    {
        while (OSMBuildingLoader.Instance == null || !OSMBuildingLoader.Instance.dataLoaded)
            yield return new WaitForSeconds(0.5f);

        Debug.Log("Buildings: Building meshes...");
        yield return StartCoroutine(BuildBuildingsAsync(OSMBuildingLoader.Instance.osmData));
    }

    IEnumerator BuildBuildingsAsync(OSMData data)
    {
        buildingsParent = new GameObject("OSM_Buildings");
        buildingsParent.transform.position = Vector3.zero;

        int buildingCount = 0;
        int frameCount = 0;

        foreach (var way in data.ways)
        {
            if (!way.IsBuilding()) continue;

            List<Vector3> footprint = new List<Vector3>();

            foreach (long nodeRef in way.nodeRefs)
            {
                if (!data.nodeMap.ContainsKey(nodeRef)) continue;
                var node = data.nodeMap[nodeRef];
                Vector3 worldPos = GPSManager.Instance.GpsToWorld(node.lat, node.lon);
                worldPos.y = 0f;
                footprint.Add(worldPos);
            }

            // Need at least 3 points for a building
            if (footprint.Count < 3) continue;

            // Remove duplicate last point if closed polygon
            if (footprint.Count > 1 &&
                Vector3.Distance(footprint[0], footprint[footprint.Count - 1]) < 0.1f)
                footprint.RemoveAt(footprint.Count - 1);

            if (footprint.Count < 3) continue;

            // Get building height
            float height = defaultBuildingHeight;
            if (way.tags.ContainsKey("building:levels"))
            {
                if (int.TryParse(way.tags["building:levels"], out int levels))
                    height = levels * floorHeight;
            }
            else if (way.tags.ContainsKey("height"))
            {
                if (float.TryParse(way.tags["height"],
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out float h))
                    height = h;
            }

            Mesh mesh = BuildExtrudedMesh(footprint, height);
            if (mesh == null) continue;

            GameObject buildingObj = new GameObject($"Building_{way.id}");
            buildingObj.transform.parent = buildingsParent.transform;

            var mf = buildingObj.AddComponent<MeshFilter>();
            var mr = buildingObj.AddComponent<MeshRenderer>();
            mf.mesh = mesh;
            mr.material = buildingMaterial != null ? buildingMaterial :
                new Material(Shader.Find("Universal Render Pipeline/Lit"));

            buildingCount++;
            frameCount++;

            // Spread across frames to avoid hitching
            if (frameCount >= maxBuildingsPerFrame)
            {
                frameCount = 0;
                yield return null;
            }
        }

        buildingsBuilt = true;
        Debug.Log($"Buildings: Built {buildingCount} buildings!");
    }

    Mesh BuildExtrudedMesh(List<Vector3> footprint, float height)
    {
        int n = footprint.Count;
        if (n < 3) return null;

        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        // Walls
        for (int i = 0; i < n; i++)
        {
            int next = (i + 1) % n;

            Vector3 bl = footprint[i];
            Vector3 br = footprint[next];
            Vector3 tl = footprint[i] + Vector3.up * height;
            Vector3 tr = footprint[next] + Vector3.up * height;

            int idx = verts.Count;
            verts.Add(bl); verts.Add(br);
            verts.Add(tl); verts.Add(tr);

            float segLen = Vector3.Distance(bl, br) / height;
            uvs.Add(new Vector2(0, 0));
            uvs.Add(new Vector2(segLen, 0));
            uvs.Add(new Vector2(0, 1));
            uvs.Add(new Vector2(segLen, 1));

            tris.Add(idx); tris.Add(idx + 2); tris.Add(idx + 1);
            tris.Add(idx + 1); tris.Add(idx + 2); tris.Add(idx + 3);
        }

        // Flat roof using fan triangulation
        int roofBase = verts.Count;
        Vector3 center = Vector3.zero;
        foreach (var p in footprint) center += p;
        center /= n;
        center.y = height;

        verts.Add(center);
        uvs.Add(new Vector2(0.5f, 0.5f));

        for (int i = 0; i < n; i++)
        {
            int next = (i + 1) % n;
            verts.Add(footprint[i] + Vector3.up * height);
            verts.Add(footprint[next] + Vector3.up * height);
            uvs.Add(new Vector2(0, 0));
            uvs.Add(new Vector2(1, 0));

            int ci = roofBase;
            int ai = roofBase + 1 + i * 2;
            int bi = roofBase + 1 + i * 2 + 1;

            tris.Add(ci); tris.Add(ai); tris.Add(bi);
        }

        Mesh mesh = new Mesh();
        mesh.name = "Building";
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }
}