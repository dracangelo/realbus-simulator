using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Road-only collider fixture for Overpass geometry; no Mapbox or mission dependencies.</summary>
public class RoadNetworkSurface : MonoBehaviour
{
    public float surfaceY = 0.2f;
    public float defaultWidth = 7f;
    public int waysPerFrame = 8;
    public bool IsReady { get; private set; }
    public int RoadCount { get; private set; }
    readonly List<Mesh> meshes = new List<Mesh>();
    Material material;
    PhysicsMaterial friction;

    public IEnumerator Build(OverpassResponse response, CoordinateConverter converter)
    {
        Clear();
        material = new Material(Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard"));
        material.color = new Color(0.18f, 0.19f, 0.21f);
        friction = new PhysicsMaterial("Road 0.7 / 0.6") { staticFriction = 0.7f, dynamicFriction = 0.6f };
        response.Resolve();
        int processed = 0;
        foreach (var way in response.elements)
        {
            if (way.type != "way" || !way.tags.ContainsKey("highway") || way.geometry.Count < 2) continue;
            var points = new List<Vector3>();
            foreach (var point in way.geometry)
            {
                Vector3 world = converter.GeoToWorldPosition(point.lat, point.lon);
                world.y = surfaceY;
                if (points.Count == 0 || (world - points[points.Count - 1]).sqrMagnitude > 0.0001f) points.Add(world);
            }
            if (points.Count < 2) continue;
            float width = defaultWidth;
            if (way.tags.TryGetValue("width", out var widthText) && float.TryParse(widthText,
                System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float parsed))
                width = Mathf.Clamp(parsed, 3f, 30f);
            var vertices = new Vector3[points.Count * 2];
            var triangles = new int[(points.Count - 1) * 6];
            for (int i = 0; i < points.Count; i++)
            {
                Vector3 tangent = points[Mathf.Min(i + 1, points.Count - 1)] - points[Mathf.Max(0, i - 1)];
                Vector3 side = Vector3.Cross(Vector3.up, tangent.normalized) * width * 0.5f;
                vertices[i * 2] = points[i] - side;
                vertices[i * 2 + 1] = points[i] + side;
                if (i == points.Count - 1) continue;
                int t = i * 6, v = i * 2;
                triangles[t] = v; triangles[t + 1] = v + 2; triangles[t + 2] = v + 1;
                triangles[t + 3] = v + 1; triangles[t + 4] = v + 2; triangles[t + 5] = v + 3;
            }
            var mesh = new Mesh { name = "OSM road " + way.id, indexFormat = UnityEngine.Rendering.IndexFormat.UInt32 };
            mesh.vertices = vertices; mesh.triangles = triangles;
            mesh.RecalculateNormals(); mesh.RecalculateBounds();
            meshes.Add(mesh);
            var road = new GameObject(mesh.name, typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider), typeof(RoadSurfaceType));
            road.transform.SetParent(transform, false);
            road.GetComponent<MeshFilter>().sharedMesh = mesh;
            road.GetComponent<MeshRenderer>().sharedMaterial = material;
            road.GetComponent<MeshCollider>().sharedMesh = mesh;
            road.GetComponent<MeshCollider>().sharedMaterial = friction;
            way.tags.TryGetValue("surface", out string surface);
            road.GetComponent<RoadSurfaceType>().roadType = RoadSurfaceType.FromOsm(surface);
            RoadCount++;
            if (++processed % Mathf.Max(1, waysPerFrame) == 0) yield return null;
        }
        IsReady = RoadCount > 0;
    }

    void Clear()
    {
        IsReady = false; RoadCount = 0;
        foreach (Transform child in transform) Destroy(child.gameObject);
        foreach (var mesh in meshes) Destroy(mesh);
        meshes.Clear();
        if (material != null) Destroy(material);
        if (friction != null) Destroy(friction);
    }
    void OnDestroy() { Clear(); }
}
