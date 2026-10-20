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
        VisualQualityController existing = FindAnyObjectByType<VisualQualityController>();
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
        CinematicAtmosphereController.EnsureExists().ApplyCinematicAtmosphere();
        BusController bus = FindAnyObjectByType<BusController>();
        if (bus == null) return;
        if (bus.GetComponent<CosmeticDamageSystem>() == null) bus.gameObject.AddComponent<CosmeticDamageSystem>();
        if (bus.GetComponent<VehiclePolishEffects>() == null) bus.gameObject.AddComponent<VehiclePolishEffects>();
        ConfigureGlass(bus);
        ConfigureInteriorLight(bus);
        ConfigureReflection(bus);
        ConfigureSharpness();
        DisableMotionBlurOnLow();
    }

    /// <summary>
    /// Keeps the high tiers sharp. A texture importing at the default aniso level 1,
    /// or a quality tier that quietly halves texture resolution, makes asphalt and
    /// facades crawl at a grazing angle no matter what the render settings promise.
    /// Only the tiers that already ship 4x MSAA and real shadows are touched, so a
    /// player who chose Low still gets the cheap path.
    /// </summary>
    void ConfigureSharpness()
    {
        int tier = QualitySettings.GetQualityLevel();
        int sharpTier = Application.isMobilePlatform ? 3 : 2;
        if (tier < sharpTier) return;

        // 0 = full resolution. The old master-texture limit path could still be left
        // at a fraction from a previous session, which halves every facade and road.
        if (QualitySettings.globalTextureMipmapLimit != 0)
            QualitySettings.globalTextureMipmapLimit = 0;

        bool forceAniso = !Application.isMobilePlatform || tier >= 4;
        if (forceAniso && QualitySettings.anisotropicFiltering != AnisotropicFiltering.ForceEnable)
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;

        // Medium and up already pay for real shadows; a desktop GPU can also afford
        // the 4x MSAA those tiers ask for. Without this, choosing Medium left every
        // kerb and window edge stair-stepped while the rest of the tier looked fine.
        if (!Application.isMobilePlatform && QualitySettings.antiAliasing < 4)
            QualitySettings.antiAliasing = 4;
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
                // The same glass setup the importers use: Fade mode, alpha blending,
                // ZWrite off and the transparent queue, then a capped alpha so the
                // cabin reads as glass rather than as a hole.
                ImportedVehicleVisualRepair.ConfigureGlass(material);
                if (material.HasProperty("_Glossiness")) material.SetFloat("_Glossiness", 0.86f);
                if (material.HasProperty("_Smoothness")) material.SetFloat("_Smoothness", 0.86f);
                if (material.HasProperty("_Color")) { Color c = material.GetColor("_Color"); c.a = Mathf.Min(c.a, 0.38f); material.SetColor("_Color", c); }
            }
        }
    }

    /// <summary>
    /// The cabin is a closed box under a workshop's worth of dark trim, so with only
    /// the sun and sky ambient it reads as a black hole from the driver's seat. A
    /// shadowless warm fill inside the cabin makes the dashboard, wheel and seats
    /// legible without lighting the street through the windows.
    /// </summary>
    void ConfigureInteriorLight(BusController bus)
    {
        Transform existing = bus.transform.Find("Cabin Fill Light");
        GameObject lightObject = existing != null ? existing.gameObject : new GameObject("Cabin Fill Light");
        lightObject.transform.SetParent(bus.transform, false);
        lightObject.transform.localPosition = new Vector3(0f, 2.15f, 2.2f);

        Light fill = lightObject.GetComponent<Light>();
        if (fill == null) fill = lightObject.AddComponent<Light>();
        fill.type = LightType.Point;
        fill.range = 9f;
        fill.intensity = 0.7f;
        fill.color = new Color(1f, 0.93f, 0.82f);
        fill.shadows = LightShadows.None;
        fill.renderMode = LightRenderMode.Auto;
        fill.enabled = true;
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
        Behaviour[] behaviours = FindObjectsByType<Behaviour>(FindObjectsInactive.Include);
        for (int i = 0; i < behaviours.Length; i++)
            if (behaviours[i] != null && behaviours[i].GetType().Name.IndexOf("MotionBlur", System.StringComparison.OrdinalIgnoreCase) >= 0)
                behaviours[i].enabled = false;
    }
}
