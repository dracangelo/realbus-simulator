using UnityEngine;

/// <summary>
/// Switches a traffic body's presentation lights off once it is far from the bus.
///
/// Only attached by <see cref="TrafficBodyOptimizer"/> when the prefab actually
/// has lights, so the common car with no light rig pays nothing. The body stays
/// active and keeps driving; its lamps simply leave the lighting and shadow
/// passes until it comes back into range. Colliders and simulation are untouched.
/// </summary>
public class TrafficPresentationCull : MonoBehaviour
{
    public float visibleDistance = TrafficPerformancePolicy.PresentationDistanceMeters;
    public float checkInterval = 0.5f;

    Light[] lights;
    float tick;
    bool visible = true;

    void Awake() { Cache(); }
    void OnEnable() { tick = checkInterval; }

    void Cache()
    {
        if (lights != null && lights.Length > 0)
            return;
        lights = GetComponentsInChildren<Light>(true);
    }

    void Update()
    {
        tick += Time.unscaledDeltaTime;
        if (tick < Mathf.Max(0.1f, checkInterval))
            return;
        tick = 0f;

        Transform target = MissionManager.Instance != null && MissionManager.Instance.busController != null
            ? MissionManager.Instance.busController.transform
            : Camera.main != null ? Camera.main.transform : null;
        if (target == null)
            return;

        float distance = Vector3.Distance(target.position, transform.position);
        bool shouldShow = distance <= Mathf.Max(10f, visibleDistance);
        if (shouldShow == visible)
            return;

        visible = shouldShow;
        Cache();
        foreach (Light light in lights)
            if (light != null)
                light.enabled = visible;
    }
}
