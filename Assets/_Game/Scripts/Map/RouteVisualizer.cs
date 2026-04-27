using UnityEngine;
using System.Collections;

public class RouteVisualizer : MonoBehaviour
{
    [Header("Route")]
    public BusRoute route;

    [Header("Line Settings")]
    public Color routeColor = new Color(1f, 0.5f, 0f);
    public float lineWidth = 0.1f;
    public float lineHeightY = 0.2f;

    private LineRenderer lineRenderer;
    private int lastGuidanceVersion = -1;
    private BusRoute lastRoute;

    void Start()
    {

        // Use selected route from GameState if available
        if (GameState.Instance?.selectedRoute != null)
            route = GameState.Instance.selectedRoute;
        // Get existing or add new LineRenderer
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
            lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        lineRenderer.material.color = routeColor;
        lineRenderer.startColor = routeColor;
        lineRenderer.endColor = routeColor;
        lineRenderer.startWidth = 3f;
        lineRenderer.endWidth = 3f;
        lineRenderer.useWorldSpace = true;

        StartCoroutine(DrawRouteNextFrame());
    }

    void Update()
    {
        var mission = MissionManager.Instance;
        int version = mission != null ? mission.GuidancePathVersion : -1;
        BusRoute activeRoute = mission != null && mission.currentRoute != null ? mission.currentRoute : route;
        if (version != lastGuidanceVersion || activeRoute != lastRoute)
            RefreshRouteLine();
    }

    IEnumerator DrawRouteNextFrame()
    {
        float timeout = 5f;
        while (GPSManager.Instance == null && timeout > 0f)
        {
            timeout -= Time.deltaTime;
            yield return null;
        }

        if (GPSManager.Instance == null)
        {
            Debug.LogError("RouteVisualizer: GPSManager not found after timeout!");
            yield break;
        }

        if (route == null && (MissionManager.Instance == null || MissionManager.Instance.currentRoute == null))
        {
            Debug.LogError("RouteVisualizer: No route assigned!");
            yield break;
        }

        RefreshRouteLine();
    }

    void RefreshRouteLine()
    {
        var mission = MissionManager.Instance;
        if (mission != null && mission.currentRoute != null)
            route = mission.currentRoute;

        Vector3[] points = mission != null ? mission.GetGuidancePathPoints() : null;
        if ((points == null || points.Length < 2) && route != null)
            points = BuildFallbackPoints(route);

        if (points == null || points.Length < 2)
        {
            lineRenderer.positionCount = 0;
            return;
        }

        lineRenderer.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 worldPos = points[i];
            worldPos.y = lineHeightY;
            lineRenderer.SetPosition(i, worldPos);
        }

        lastGuidanceVersion = mission != null ? mission.GuidancePathVersion : -1;
        lastRoute = route;
    }

    Vector3[] BuildFallbackPoints(BusRoute activeRoute)
    {
        if (activeRoute == null)
            return null;
        if (activeRoute.pathPoints != null && activeRoute.pathPoints.Length >= 2)
            return activeRoute.pathPoints;
        if (activeRoute.stops == null || activeRoute.stops.Length < 2 || GPSManager.Instance == null)
            return null;

        var points = new Vector3[activeRoute.stops.Length];
        for (int i = 0; i < activeRoute.stops.Length; i++)
            points[i] = GPSManager.Instance.GpsToWorld(activeRoute.stops[i].latitude, activeRoute.stops[i].longitude);
        return points;
    }
}
