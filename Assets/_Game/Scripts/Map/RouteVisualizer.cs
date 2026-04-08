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

        if (route == null)
        {
            Debug.LogError("RouteVisualizer: No route assigned!");
            yield break;
        }

        lineRenderer.positionCount = route.stops.Length;

        for (int i = 0; i < route.stops.Length; i++)
        {
            Vector3 worldPos = GPSManager.Instance.GpsToWorld(
                route.stops[i].latitude,
                route.stops[i].longitude);
            worldPos.y = lineHeightY;
            lineRenderer.SetPosition(i, worldPos);
            Debug.Log($"Stop {i} — {route.stops[i].stopName} — world pos: {worldPos}");
        }

        Debug.Log("Route drawn!");
    }
}