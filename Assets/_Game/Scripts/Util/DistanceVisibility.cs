using UnityEngine;

/// <summary>
/// Hides the renderers of a static prop once it is far from the bus. Gas stations
/// and shelters are imported models with dozens of parts each; they stay alive so
/// their docking triggers and colliders keep working, but their renderers leave the
/// culling, draw and shadow work as soon as the player drives away. Only the
/// renderers are toggled, so this component keeps running to bring them back.
/// </summary>
public class DistanceVisibility : MonoBehaviour
{
    public float visibleDistance = 320f;
    public float checkInterval = 0.5f;

    Renderer[] renderers;
    float tick;
    bool visible = true;

    void Awake() { Cache(); }

    void Cache()
    {
        if (renderers != null && renderers.Length > 0) return;
        renderers = GetComponentsInChildren<Renderer>(true);
    }

    void Update()
    {
        tick += Time.unscaledDeltaTime;
        if (tick < Mathf.Max(0.1f, checkInterval)) return;
        tick = 0f;

        Transform target = MissionManager.Instance != null && MissionManager.Instance.busController != null
            ? MissionManager.Instance.busController.transform
            : Camera.main != null ? Camera.main.transform : null;
        if (target == null) return;

        bool shouldShow = (transform.position - target.position).sqrMagnitude <=
            Mathf.Max(10f, visibleDistance) * Mathf.Max(10f, visibleDistance);
        if (shouldShow == visible) return;

        visible = shouldShow;
        Cache();
        for (int i = 0; i < renderers.Length; i++)
            if (renderers[i] != null) renderers[i].enabled = visible;
    }
}
