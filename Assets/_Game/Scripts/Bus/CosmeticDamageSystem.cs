using UnityEngine;

[DisallowMultipleComponent]
public class CosmeticDamageSystem : MonoBehaviour
{
    public Renderer[] bodyRenderers;
    public float damageAmount;
    public float impactThreshold = 2f;
    public Vector3 lastImpactLocalPosition;
    public float lastImpactSeverity;
    public int impactCount;
    MaterialPropertyBlock properties;

    void Awake()
    {
        if (bodyRenderers == null || bodyRenderers.Length == 0) bodyRenderers = GetComponentsInChildren<Renderer>();
        properties = new MaterialPropertyBlock();
    }

    void OnCollisionEnter(Collision collision)
    {
        float impact = collision.relativeVelocity.magnitude;
        if (impact < impactThreshold) return;
        Vector3 impactPoint = collision.contactCount > 0 ? collision.GetContact(0).point : transform.position;
        lastImpactLocalPosition = transform.InverseTransformPoint(impactPoint);
        lastImpactSeverity = Mathf.InverseLerp(impactThreshold, 18f, impact);
        impactCount++;
        damageAmount = Mathf.Clamp01(damageAmount + impact * 0.012f);
        ApplyVisuals();
    }

    public void RepairCosmetics() { damageAmount = lastImpactSeverity = 0f; impactCount = 0; ApplyVisuals(); }

    void ApplyVisuals()
    {
        Color scuff = Color.Lerp(Color.white, new Color(0.42f, 0.36f, 0.31f), damageAmount * 0.45f);
        for (int i = 0; i < bodyRenderers.Length; i++)
        {
            Renderer renderer = bodyRenderers[i]; if (renderer == null) continue;
            renderer.GetPropertyBlock(properties); properties.SetFloat("_DamageAmount", damageAmount); properties.SetColor("_DamageTint", scuff);
            properties.SetVector("_DamagePoint", lastImpactLocalPosition); properties.SetFloat("_ImpactSeverity", lastImpactSeverity);
            if (renderer.name.IndexOf("glass", System.StringComparison.OrdinalIgnoreCase) >= 0) properties.SetFloat("_CrackAmount", damageAmount);
            renderer.SetPropertyBlock(properties);
        }
    }
}
