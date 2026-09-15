using UnityEngine;

public class TimeOfDaySystem : MonoBehaviour
{
    public static TimeOfDaySystem Instance { get; private set; }

    [Header("References")]
    public Light directionalSun;
    public SkyController skyController;

    [Header("Clock")]
    public bool followScheduleManager = true;
    public float startTimeMinutes = 480f;
    public float currentTimeMinutes = 480f;
    public float secondsPerGameMinute = 60f;

    [Header("Sun")]
    public float sunriseMinutes = 360f;
    public float sunsetMinutes = 1080f;
    public Vector3 midnightSunEuler = new Vector3(340f, -30f, 0f);
    public Vector3 noonSunEuler = new Vector3(68f, 30f, 0f);

    [Header("Lighting")]
    public Gradient skyTintByDayPhase;
    public AnimationCurve ambientIntensityByDayPhase = AnimationCurve.EaseInOut(0f, 0.18f, 1f, 1f);
    public AnimationCurve sunIntensityByDayPhase = AnimationCurve.EaseInOut(0f, 0.05f, 1f, 1f);
    public AnimationCurve shadowStrengthByDayPhase = AnimationCurve.EaseInOut(0f, 0.95f, 1f, 0.7f);

    [Header("Night Lights")]
    public Light[] streetLights;
    public Light[] busHeadLights;
    public Light[] busInteriorLights;
    public GameObject[] streetLightObjects;
    public GameObject[] busHeadLightObjects;
    public GameObject[] busInteriorLightObjects;
    public float nightStreetLightIntensity = 1.15f;
    public float nightHeadlightIntensity = 1.25f;
    public float nightInteriorIntensity = 0.65f;

    public bool IsNight { get; private set; }
    public float DayPhase01 { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        EnsureDefaultGradient();
    }

    void Start()
    {
        if (directionalSun == null && skyController != null)
            directionalSun = skyController.sunLight;
        if (skyController == null)
            skyController = FindFirstObjectByType<SkyController>();

        AutoBindLights();
        currentTimeMinutes = ScheduleManager.Instance != null ? ScheduleManager.Instance.currentTimeMinutes : startTimeMinutes;
        ApplyTimeOfDayImmediate();
    }

    void Update()
    {
        if (followScheduleManager && ScheduleManager.Instance != null)
            currentTimeMinutes = ScheduleManager.Instance.currentTimeMinutes;
        else
            currentTimeMinutes += Time.deltaTime / Mathf.Max(0.01f, secondsPerGameMinute);

        ApplyTimeOfDayImmediate();
    }

    public float GetSunIntensityMultiplier()
    {
        return sunIntensityByDayPhase != null ? Mathf.Max(0f, sunIntensityByDayPhase.Evaluate(RealismRules.Daylight(currentTimeMinutes, sunriseMinutes, sunsetMinutes))) : 1f;
    }

    public float GetAmbientIntensityMultiplier()
    {
        return ambientIntensityByDayPhase != null ? Mathf.Max(0f, ambientIntensityByDayPhase.Evaluate(RealismRules.Daylight(currentTimeMinutes, sunriseMinutes, sunsetMinutes))) : 1f;
    }

    public float GetShadowStrength()
    {
        return shadowStrengthByDayPhase != null ? Mathf.Clamp01(shadowStrengthByDayPhase.Evaluate(DayPhase01)) : 0.8f;
    }

    public Color GetSkyTint()
    {
        return skyTintByDayPhase != null ? skyTintByDayPhase.Evaluate(DayPhase01) : Color.white;
    }

    void ApplyTimeOfDayImmediate()
    {
        float wrappedMinutes = Mathf.Repeat(currentTimeMinutes, 1440f);
        float daylightSpan = Mathf.Max(1f, sunsetMinutes - sunriseMinutes);
        DayPhase01 = Mathf.Clamp01((wrappedMinutes - sunriseMinutes) / daylightSpan);
        IsNight = wrappedMinutes < sunriseMinutes || wrappedMinutes >= sunsetMinutes;

        if (directionalSun != null)
        {
            Vector3 euler = IsNight
                ? midnightSunEuler
                : Vector3.Lerp(noonSunEuler, new Vector3(8f, noonSunEuler.y, noonSunEuler.z), Mathf.Abs(DayPhase01 - 0.5f) * 2f);

            directionalSun.transform.rotation = Quaternion.Euler(euler.x, Mathf.Lerp(-25f, 35f, DayPhase01), euler.z);
            directionalSun.shadowStrength = GetShadowStrength();
        }

        ApplyManagedLights(streetLights, IsNight, nightStreetLightIntensity);
        ApplyManagedLights(busHeadLights, IsNight, nightHeadlightIntensity);
        ApplyManagedLights(busInteriorLights, IsNight, nightInteriorIntensity);
        ApplyManagedObjects(streetLightObjects, IsNight);
        ApplyManagedObjects(busHeadLightObjects, IsNight);
        ApplyManagedObjects(busInteriorLightObjects, IsNight);
    }

    void ApplyManagedLights(Light[] lights, bool active, float targetIntensity)
    {
        if (lights == null)
            return;

        for (int i = 0; i < lights.Length; i++)
        {
            var lightRef = lights[i];
            if (lightRef == null)
                continue;

            lightRef.enabled = active;
            if (active)
                lightRef.intensity = targetIntensity;
        }
    }

    void AutoBindLights()
    {
        if (streetLights == null || streetLights.Length == 0)
            streetLights = FindLightsByName("street", "lamp");
        if (busHeadLights == null || busHeadLights.Length == 0)
            busHeadLights = FindLightsByName("head", "front");
        if (busInteriorLights == null || busInteriorLights.Length == 0)
            busInteriorLights = FindLightsByName("interior", "cabin", "saloon");
        if (streetLightObjects == null || streetLightObjects.Length == 0)
            streetLightObjects = FindObjectsByName("street", "lamp");
        if (busHeadLightObjects == null || busHeadLightObjects.Length == 0)
            busHeadLightObjects = FindObjectsByName("head", "front");
        if (busInteriorLightObjects == null || busInteriorLightObjects.Length == 0)
            busInteriorLightObjects = FindObjectsByName("interior", "cabin", "saloon");
    }

    Light[] FindLightsByName(params string[] nameTokens)
    {
        var allLights = FindObjectsByType<Light>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        var matches = new System.Collections.Generic.List<Light>();

        for (int i = 0; i < allLights.Length; i++)
        {
            var lightRef = allLights[i];
            if (lightRef == null || lightRef == directionalSun)
                continue;

            string lower = lightRef.name.ToLowerInvariant();
            for (int tokenIndex = 0; tokenIndex < nameTokens.Length; tokenIndex++)
            {
                if (lower.Contains(nameTokens[tokenIndex]))
                {
                    matches.Add(lightRef);
                    break;
                }
            }
        }

        return matches.ToArray();
    }

    GameObject[] FindObjectsByName(params string[] nameTokens)
    {
        var allTransforms = FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        var matches = new System.Collections.Generic.List<GameObject>();

        for (int i = 0; i < allTransforms.Length; i++)
        {
            var transformRef = allTransforms[i];
            if (transformRef == null || transformRef == transform)
                continue;

            string lower = transformRef.name.ToLowerInvariant();
            for (int tokenIndex = 0; tokenIndex < nameTokens.Length; tokenIndex++)
            {
                if (lower.Contains(nameTokens[tokenIndex]))
                {
                    matches.Add(transformRef.gameObject);
                    break;
                }
            }
        }

        return matches.ToArray();
    }

    void ApplyManagedObjects(GameObject[] objects, bool active)
    {
        if (objects == null)
            return;

        for (int i = 0; i < objects.Length; i++)
        {
            var target = objects[i];
            if (target == null)
                continue;

            target.SetActive(active);
        }
    }

    void EnsureDefaultGradient()
    {
        if (skyTintByDayPhase != null && skyTintByDayPhase.colorKeys != null && skyTintByDayPhase.colorKeys.Length > 0)
            return;

        skyTintByDayPhase = new Gradient();
        skyTintByDayPhase.SetKeys(
            new[]
            {
                new GradientColorKey(new Color(0.06f, 0.08f, 0.18f), 0f),
                new GradientColorKey(new Color(0.95f, 0.55f, 0.22f), 0.08f),
                new GradientColorKey(new Color(0.52f, 0.74f, 0.96f), 0.5f),
                new GradientColorKey(new Color(0.94f, 0.42f, 0.18f), 0.9f),
                new GradientColorKey(new Color(0.08f, 0.10f, 0.22f), 1f)
            },
            new[]
            {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            });
    }
}
