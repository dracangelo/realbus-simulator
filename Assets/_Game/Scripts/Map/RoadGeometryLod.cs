using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>Far-road simplification for RoadNetworkSurface ribbon meshes. Keeps collider geometry intact.</summary>
public class RoadGeometryLod : MonoBehaviour
{
    public Transform viewer;
    public float simplifiedDistance = 500f;
    class Entry { public MeshFilter filter; public Mesh full, reduced; public Bounds bounds; public bool far; }
    readonly List<Entry> entries = new List<Entry>();
    bool stopping;
    float nextRefresh;
    IEnumerator Start()
    {
        var surface = GetComponent<RoadNetworkSurface>();
        while (surface != null && !surface.IsReady) yield return null;
        foreach (var filter in GetComponentsInChildren<MeshFilter>())
        {
            if (filter.sharedMesh == null) continue;
            var source = filter.sharedMesh.vertices;
            if (source.Length < 6 || source.Length % 2 != 0) continue;
            var job = Task.Run(() => Simplify(source));
            while (!job.IsCompleted) yield return null;
            if (stopping) yield break;
            if (job.IsFaulted) { Debug.LogWarning("Road LOD failed: " + job.Exception.GetBaseException().Message); continue; }
            var vertices = job.Result;
            var triangles = new int[(vertices.Length / 2 - 1) * 6];
            for (int i = 0; i < vertices.Length / 2 - 1; i++)
            {
                int t = i * 6, v = i * 2;
                triangles[t] = v; triangles[t + 1] = v + 2; triangles[t + 2] = v + 1;
                triangles[t + 3] = v + 1; triangles[t + 4] = v + 2; triangles[t + 5] = v + 3;
            }
            var mesh = new Mesh { name = "Far road ribbon" }; mesh.vertices = vertices; mesh.triangles = triangles; mesh.RecalculateNormals(); mesh.RecalculateBounds();
            entries.Add(new Entry { filter = filter, full = filter.sharedMesh, reduced = mesh, bounds = filter.GetComponent<Renderer>().bounds });
            yield return null;
        }
    }
    static Vector3[] Simplify(Vector3[] source)
    {
        var result = new List<Vector3>(); int pairs = source.Length / 2;
        for (int i = 0; i < pairs; i += 3) { result.Add(source[i * 2]); result.Add(source[i * 2 + 1]); }
        if ((pairs - 1) % 3 != 0) { result.Add(source[source.Length - 2]); result.Add(source[source.Length - 1]); }
        return result.ToArray();
    }
    void Update()
    {
        if (viewer == null || Time.time < nextRefresh) return;
        nextRefresh = Time.time + 0.25f; int budget = 4;
        foreach (var entry in entries)
        {
            bool far = entry.bounds.SqrDistance(viewer.position) > simplifiedDistance * simplifiedDistance;
            if (entry.filter == null || far == entry.far) continue;
            entry.filter.sharedMesh = far ? entry.reduced : entry.full; entry.far = far;
            if (--budget == 0) break;
        }
    }
    void OnDestroy() { stopping = true; foreach (var entry in entries) if (entry.reduced != null) Destroy(entry.reduced); }
}
