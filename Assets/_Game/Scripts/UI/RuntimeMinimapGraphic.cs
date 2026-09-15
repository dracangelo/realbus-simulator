using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuntimeMinimapGraphic : MaskableGraphic
{
    public BusController bus;
    public MissionManager mission;
    public float worldRadiusMeters = 450f;
    public Color routeColor = new Color(1f, 0.45f, 0.15f, 1f);
    public Color stopColor = Color.white;
    public Color busColor = new Color(0.1f, 0.65f, 1f, 1f);

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        Rect r = rectTransform.rect;
        float radius = Mathf.Min(r.width, r.height) * 0.5f;
        AddCircle(vh, Vector2.zero, radius, color, 40);
        if (bus == null) bus = FindFirstObjectByType<BusController>();
        if (mission == null) mission = MissionManager.Instance;
        if (bus == null) return;

        IReadOnlyList<Vector3> path = mission != null ? mission.ActiveGuidancePathPoints : null;
        if (path != null && path.Count > 1)
        {
            for (int i = 1; i < path.Count; i++)
            {
                Vector2 a = WorldToMap(path[i - 1], radius);
                Vector2 b = WorldToMap(path[i], radius);
                if (a.magnitude <= radius || b.magnitude <= radius) AddLine(vh, a, b, 4f, routeColor);
            }
            for (int i = 0; i < path.Count; i += Mathf.Max(1, path.Count / 8))
            {
                Vector2 p = WorldToMap(path[i], radius);
                if (p.magnitude <= radius) AddCircle(vh, p, 4f, stopColor, 10);
            }
        }

        AddTriangle(vh, Vector2.zero, 9f, busColor);
    }

    void Update()
    {
        SetVerticesDirty();
    }

    Vector2 WorldToMap(Vector3 world, float radius)
    {
        Vector3 d = world - bus.transform.position;
        return new Vector2(d.x, d.z) * (radius / Mathf.Max(1f, worldRadiusMeters));
    }

    static void AddCircle(VertexHelper vh, Vector2 center, float radius, Color tint, int segments)
    {
        int start = vh.currentVertCount;
        vh.AddVert(center, tint, Vector2.zero);
        for (int i = 0; i <= segments; i++)
        {
            float a = i * Mathf.PI * 2f / segments;
            vh.AddVert(center + new Vector2(Mathf.Sin(a), Mathf.Cos(a)) * radius, tint, Vector2.zero);
        }
        for (int i = 0; i < segments; i++) vh.AddTriangle(start, start + i + 1, start + i + 2);
    }

    static void AddLine(VertexHelper vh, Vector2 a, Vector2 b, float width, Color tint)
    {
        Vector2 n = new Vector2(-(b - a).y, (b - a).x).normalized * width * 0.5f;
        int s = vh.currentVertCount;
        vh.AddVert(a - n, tint, Vector2.zero); vh.AddVert(a + n, tint, Vector2.zero);
        vh.AddVert(b + n, tint, Vector2.zero); vh.AddVert(b - n, tint, Vector2.zero);
        vh.AddTriangle(s, s + 1, s + 2); vh.AddTriangle(s, s + 2, s + 3);
    }

    static void AddTriangle(VertexHelper vh, Vector2 center, float size, Color tint)
    {
        int s = vh.currentVertCount;
        vh.AddVert(center + Vector2.up * size, tint, Vector2.zero);
        vh.AddVert(center + new Vector2(-size * 0.7f, -size), tint, Vector2.zero);
        vh.AddVert(center + new Vector2(size * 0.7f, -size), tint, Vector2.zero);
        vh.AddTriangle(s, s + 1, s + 2);
    }
}
