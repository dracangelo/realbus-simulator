using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class VisualQualityController : MonoBehaviour
{
    public static VisualQualityController Instance { get; private set; }
    ReflectionProbe busReflectionProbe;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static VisualQualityController EnsureExists()
    {
        if (Instance != null) return Instance;
        VisualQualityController existing = FindFirstObjectByType<VisualQualityController>();
        return existing != null ? existing : new GameObject("VisualQualityController").AddComponent<VisualQualityController>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start() { ApplyScenePolish(); }
    void OnDestroy() { if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded; }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { ApplyScenePolish(); }

    public void ApplyScenePolish()
    {
        BusController bus = FindFirstObjectByType<BusController>();
        if (bus == null) return;
        if (bus.GetComponent<CosmeticDamageSystem>() == null) bus.gameObject.AddComponent<CosmeticDamageSystem>();
        if (bus.GetComponent<VehiclePolishEffects>() == null) bus.gameObject.AddComponent<VehiclePolishEffects>();
        ConfigureGlass(bus);
        ConfigureReflection(bus);
        DisableMotionBlurOnLow();
    }

    void ConfigureGlass(BusController bus)
    {
        Renderer[] renderers = bus.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            Renderer renderer = renderers[i];
            if (renderer == null || renderer.name.IndexOf("glass", System.StringComparison.OrdinalIgnoreCase) < 0) continue;
            renderer.shadowCastingMode = ShadowCastingMode.Off;
            Material[] materials = renderer.materials;
            for (int m = 0; m < materials.Length; m++)
            {
                Material material = materials[m]; if (material == null) continue;
                if (material.HasProperty("_Surface")) material.SetFloat("_Surface", 1f);
                if (material.HasProperty("_Smoothness")) material.SetFloat("_Smoothness", 0.86f);
                if (material.HasProperty("_BaseColor")) { Color c = material.GetColor("_BaseColor"); c.a = Mathf.Min(c.a, 0.38f); material.SetColor("_BaseColor", c); }
                material.renderQueue = 3000;
            }
        }
    }

    void ConfigureReflection(BusController bus)
    {
        Transform existing = bus.transform.Find("Bus Reflection Probe");
        if (QualitySettings.GetQualityLevel() <= 0) { if (existing != null) existing.gameObject.SetActive(false); return; }
        GameObject probeObject = existing != null ? existing.gameObject : new GameObject("Bus Reflection Probe"); probeObject.transform.SetParent(bus.transform, false); probeObject.transform.localPosition = new Vector3(0f, 2f, 0f); probeObject.SetActive(true);
        busReflectionProbe = probeObject.GetComponent<ReflectionProbe>(); if (busReflectionProbe == null) busReflectionProbe = probeObject.AddComponent<ReflectionProbe>(); busReflectionProbe.mode = ReflectionProbeMode.Realtime; busReflectionProbe.refreshMode = ReflectionProbeRefreshMode.ViaScripting; busReflectionProbe.resolution = QualitySettings.GetQualityLevel() >= 2 ? 128 : 64; busReflectionProbe.size = new Vector3(24f, 12f, 24f); busReflectionProbe.intensity = 0.65f;
        busReflectionProbe.RenderProbe();
    }

    void DisableMotionBlurOnLow()
    {
        if (QualitySettings.GetQualityLevel() > 0) return;
        Behaviour[] behaviours = FindObjectsByType<Behaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        for (int i = 0; i < behaviours.Length; i++)
            if (behaviours[i] != null && behaviours[i].GetType().Name.IndexOf("MotionBlur", System.StringComparison.OrdinalIgnoreCase) >= 0)
                behaviours[i].enabled = false;
    }
}
