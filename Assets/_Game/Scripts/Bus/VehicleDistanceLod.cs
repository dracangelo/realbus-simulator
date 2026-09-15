using UnityEngine;

/// <summary>Explicit distance LOD for production bus models. Colliders and simulation are unaffected.</summary>
public class VehicleDistanceLod : MonoBehaviour
{
    public Transform viewer;
    public Renderer[] detailed;
    public Renderer[] reduced;
    public Renderer[] billboard;
    public float reducedDistance = 100f, billboardDistance = 300f, cullDistance = 400f;
    float nextUpdate;
    int previousTier = -1;
    void Update()
    {
        if (Time.time < nextUpdate) return;
        nextUpdate = Time.time + 0.2f;
        if (viewer == null && Camera.main != null) viewer = Camera.main.transform;
        if (viewer == null) return;
        float distance = Vector3.Distance(viewer.position, transform.position);
        int tier = distance > cullDistance ? 3 : distance > billboardDistance && billboard != null && billboard.Length > 0 ? 2 : distance > reducedDistance && reduced != null && reduced.Length > 0 ? 1 : 0;
        if (tier != previousTier)
        {
            Show(detailed, tier == 0); Show(reduced, tier == 1); Show(billboard, tier == 2); previousTier = tier;
        }
        if (tier == 2) foreach (var renderer in billboard)
            if (renderer != null) renderer.transform.rotation = Quaternion.LookRotation(renderer.transform.position - viewer.position, Vector3.up);
    }
    static void Show(Renderer[] renderers, bool show) { if (renderers != null) foreach (var renderer in renderers) if (renderer != null) renderer.enabled = show; }
}
