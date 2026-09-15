using UnityEngine;

public class CityPinPulse : MonoBehaviour
{
    Vector3 baseScale;
    void Awake() { baseScale = transform.localScale; }
    void OnEnable() { if (baseScale == Vector3.zero) baseScale = Vector3.one; }
    void Update() { transform.localScale = baseScale * (1f + Mathf.Sin(Time.unscaledTime * 2.4f) * 0.025f); }
    void OnDisable() { transform.localScale = baseScale; }
}
