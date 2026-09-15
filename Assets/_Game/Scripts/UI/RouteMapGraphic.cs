using UnityEngine;
using UnityEngine.UI;

/// <summary>Lightweight route preview for a briefing: geometry and ordered stop markers.</summary>
public class RouteMapGraphic : MaskableGraphic
{
    public BusRoute route;
    public float lineWidth = 3f;
    public float markerSize = 8f;
    public Color stopColor = Color.yellow;
    Vector2 minimum, maximum;

    public void SetRoute(BusRoute value) { route = value; SetVerticesDirty(); }

    protected override void OnPopulateMesh(VertexHelper helper)
    {
        helper.Clear();
        if (route == null || route.geometryLatLonFlat == null || route.geometryLatLonFlat.Length < 4) return;
        var geometry = route.geometryLatLonFlat;
        double latitude = geometry[0], longitude = geometry[1];
        var points = new Vector2[geometry.Length / 2];
        minimum = new Vector2(float.MaxValue, float.MaxValue);
        maximum = new Vector2(float.MinValue, float.MinValue);
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 point = CoordinateConverter.LocalOriginGeoToWorld(geometry[i * 2], geometry[i * 2 + 1], latitude, longitude);
            points[i] = new Vector2(point.x, point.z);
            minimum = Vector2.Min(minimum, points[i]); maximum = Vector2.Max(maximum, points[i]);
        }
        for (int i = 1; i < points.Length; i++)
        {
            Vector2 a = Project(points[i - 1]), b = Project(points[i]);
            Vector2 delta = b - a;
            if (delta.sqrMagnitude < 0.0001f) continue;
            Vector2 normal = new Vector2(-delta.y, delta.x).normalized * lineWidth * 0.5f;
            Quad(helper, a - normal, a + normal, b + normal, b - normal, color);
        }
        if (route.busStops == null) return;
        for (int i = 0; i < route.busStops.Length; i++)
        {
            var stop = route.busStops[i]; if (stop == null) continue;
            Vector3 world = CoordinateConverter.LocalOriginGeoToWorld(stop.latitude, stop.longitude, latitude, longitude);
            Vector2 p = Project(new Vector2(world.x, world.z));
            Vector2 offset = Vector2.one * markerSize * 0.5f;
            Color marker = i == 0 ? Color.green : i == route.busStops.Length - 1 ? Color.red : stopColor;
            Quad(helper, p - offset, p + new Vector2(-offset.x, offset.y), p + offset, p + new Vector2(offset.x, -offset.y), marker);
        }
    }

    Vector2 Project(Vector2 point)
    {
        Rect rect = rectTransform.rect;
        Vector2 span = maximum - minimum;
        float scale = Mathf.Min(Mathf.Max(1f, rect.width - 20f) / Mathf.Max(1f, span.x), Mathf.Max(1f, rect.height - 20f) / Mathf.Max(1f, span.y));
        return rect.center + (point - (minimum + maximum) * 0.5f) * scale;
    }
    static void Quad(VertexHelper helper, Vector2 a, Vector2 b, Vector2 c, Vector2 d, Color tint)
    {
        int start = helper.currentVertCount;
        helper.AddVert(a, tint, Vector2.zero); helper.AddVert(b, tint, Vector2.zero);
        helper.AddVert(c, tint, Vector2.zero); helper.AddVert(d, tint, Vector2.zero);
        helper.AddTriangle(start, start + 1, start + 2); helper.AddTriangle(start, start + 2, start + 3);
    }
}
