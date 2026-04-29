using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps static world chunks active only when they are close enough to the viewer
/// and, optionally, inside the current camera frustum.
/// </summary>
public sealed class RuntimeDistanceCuller : MonoBehaviour
{
    sealed class Entry
    {
        public Transform root;
        public Bounds worldBounds;
        public bool isVisible = true;
    }

    [Header("Viewer")]
    public Transform viewerTransform;
    public Camera viewerCamera;

    [Header("Distance")]
    public float visibleDistanceMeters = 500f;
    public float spawnBufferMeters = 100f;
    public float alwaysActiveDistanceMeters = 120f;
    public bool useHorizontalDistanceOnly = true;

    [Header("Refresh")]
    public float refreshIntervalSeconds = 0.2f;
    public bool requireCameraView = true;
    public float boundsPaddingMeters = 8f;

    readonly List<Entry> entries = new List<Entry>();
    Plane[] frustumPlanes;
    float refreshTimer;

    public void Register(GameObject root)
    {
        if (root == null)
            return;

        Bounds bounds = CalculateWorldBounds(root);
        bounds = ExpandBounds(bounds, boundsPaddingMeters);

        entries.Add(new Entry
        {
            root = root.transform,
            worldBounds = bounds,
            isVisible = root.activeSelf
        });
    }

    public void RefreshNow()
    {
        refreshTimer = 0f;
        RefreshVisibility();
    }

    void LateUpdate()
    {
        refreshTimer -= Time.deltaTime;
        if (refreshTimer > 0f)
            return;

        refreshTimer = Mathf.Max(0.02f, refreshIntervalSeconds);
        RefreshVisibility();
    }

    void RefreshVisibility()
    {
        ResolveViewerReferences();
        if (viewerTransform == null)
            return;

        bool canUseFrustum = requireCameraView && viewerCamera != null;
        if (canUseFrustum)
            frustumPlanes = GeometryUtility.CalculateFrustumPlanes(viewerCamera);

        Vector3 viewerPosition = viewerTransform.position;
        float activationDistance = Mathf.Max(alwaysActiveDistanceMeters, visibleDistanceMeters + spawnBufferMeters);

        for (int i = entries.Count - 1; i >= 0; i--)
        {
            var entry = entries[i];
            if (entry == null || entry.root == null)
            {
                entries.RemoveAt(i);
                continue;
            }

            Vector3 center = entry.worldBounds.center;
            float distance = useHorizontalDistanceOnly
                ? Vector2.Distance(new Vector2(center.x, center.z), new Vector2(viewerPosition.x, viewerPosition.z))
                : Vector3.Distance(center, viewerPosition);

            bool isNearby = distance <= Mathf.Max(0f, alwaysActiveDistanceMeters);
            bool isWithinDistance = distance <= activationDistance;
            bool isWithinView = !canUseFrustum || GeometryUtility.TestPlanesAABB(frustumPlanes, entry.worldBounds);
            bool shouldBeVisible = isNearby || (isWithinDistance && isWithinView);

            if (entry.isVisible == shouldBeVisible)
                continue;

            entry.isVisible = shouldBeVisible;
            entry.root.gameObject.SetActive(shouldBeVisible);
        }
    }

    void ResolveViewerReferences()
    {
        if (viewerCamera == null)
            viewerCamera = Camera.main != null ? Camera.main : FindFirstObjectByType<Camera>();

        if (viewerTransform != null)
            return;

        if (viewerCamera != null)
        {
            viewerTransform = viewerCamera.transform;
            return;
        }

        var bus = FindFirstObjectByType<BusController>();
        if (bus != null)
            viewerTransform = bus.transform;
    }

    static Bounds CalculateWorldBounds(GameObject root)
    {
        bool hasBounds = false;
        Bounds bounds = new Bounds(root.transform.position, Vector3.one * 2f);

        var renderers = root.GetComponentsInChildren<Renderer>(true);
        for (int i = 0; i < renderers.Length; i++)
        {
            if (!hasBounds)
            {
                bounds = renderers[i].bounds;
                hasBounds = true;
            }
            else
            {
                bounds.Encapsulate(renderers[i].bounds);
            }
        }

        var colliders = root.GetComponentsInChildren<Collider>(true);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (!hasBounds)
            {
                bounds = colliders[i].bounds;
                hasBounds = true;
            }
            else
            {
                bounds.Encapsulate(colliders[i].bounds);
            }
        }

        return bounds;
    }

    static Bounds ExpandBounds(Bounds bounds, float padding)
    {
        if (padding <= 0f)
            return bounds;

        bounds.Expand(padding * 2f);
        return bounds;
    }
}
