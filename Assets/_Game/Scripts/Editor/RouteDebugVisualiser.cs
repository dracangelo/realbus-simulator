#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class RouteDebugVisualiser : MonoBehaviour
{
    public BusRoute route;
    public CoordinateConverter converter;
    public Color lineColor = new Color(0.2f, 1f, 0.6f, 0.9f);
    public float lineWidth = 3f;
    public bool drawStops = true;
    public float stopSphereRadius = 3f;

    void OnDrawGizmos()
    {
        if (route == null) return;
        if (converter == null) converter = CoordinateConverter.Instance != null ? CoordinateConverter.Instance : FindFirstObjectByType<CoordinateConverter>();

        var points = GetRouteWorldPoints();
        if (points == null || points.Length < 2) return;

        Handles.color = lineColor;
        for (int i = 0; i < points.Length - 1; i++)
            Handles.DrawAAPolyLine(lineWidth, points[i], points[i + 1]);

        if (drawStops)
        {
            var stops = route.busStops;
            if (stops != null && stops.Length > 0)
            {
                Gizmos.color = lineColor;
                for (int i = 0; i < stops.Length; i++)
                {
                    Vector3 p = stops[i].WorldPosition(converter);
                    Gizmos.DrawSphere(p + Vector3.up * 0.5f, stopSphereRadius);
                }
            }
        }
    }

    Vector3[] GetRouteWorldPoints()
    {
        if (route.geometryLatLonFlat != null && route.geometryLatLonFlat.Length >= 4 && converter != null)
        {
            int n = route.geometryLatLonFlat.Length / 2;
            var pts = new Vector3[n];
            for (int i = 0; i < n; i++)
            {
                double lat = route.geometryLatLonFlat[i * 2];
                double lon = route.geometryLatLonFlat[i * 2 + 1];
                pts[i] = converter.GeoToWorldPosition(lat, lon) + Vector3.up * 0.1f;
            }
            return pts;
        }

        // Fallback: connect stops.
        if (route.busStops != null && route.busStops.Length >= 2 && converter != null)
        {
            var pts = new Vector3[route.busStops.Length];
            for (int i = 0; i < pts.Length; i++)
                pts[i] = route.busStops[i].WorldPosition(converter) + Vector3.up * 0.1f;
            return pts;
        }

        return null;
    }
}
#endif

