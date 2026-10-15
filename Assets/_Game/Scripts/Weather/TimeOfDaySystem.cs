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
    [Tooltip("Sun elevation at solar noon, in degrees above the horizon.")]
    public float noonSunElevation = 68f;
    [Tooltip("How far below the horizon the sun travels at solar midnight. A real arc keeps sunrise and the star fade continuous.")]
    public float midnightSunElevation = -34f;
    public float sunriseAzimuth = 85f;
    public float sunsetAzimuth = 275f;

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
    /// <summary>Sun height above the horizon in degrees; negative when it has set.</summary>
    public float SunElevationDegrees { get; private set; }
    /// <summary>Compass bearing of the sun in degrees, sweeping east to west across the day.</summary>
    public float SunAzimuthDegrees { get; private set; }

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
            skyController = FindAnyObjectByType<SkyController>();

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

    /// <summary>
    /// Daylight strength (0 at night, 1 at noon). Shares <see cref="RealismRules.Daylight"/>
    /// with the sky and fog so a time of day cannot mean two different brightnesses.
    /// </summary>
    public float GetDaylightFactor()
        => RealismRules.Daylight(currentTimeMinutes, sunriseMinutes, sunsetMinutes);

    /// <summary>
    /// Unit vector from the world toward the sun, derived from the arc rather than
    /// from the light's transform, so the sky, the moon and anything else asking can
    /// agree on where the sun is even when no directional light exists.
    /// </summary>
    public Vector3 GetSunDirection()
    {
        float elevation = SunElevationDegrees * Mathf.Deg2Rad;
        float azimuth = SunAzimuthDegrees * Mathf.Deg2Rad;
        return new Vector3(
            Mathf.Cos(elevation) * Mathf.Sin(azimuth),
            Mathf.Sin(elevation),
            Mathf.Cos(elevation) * Mathf.Cos(azimuth));
    }

    /// <summary>
    /// Sun elevation for a clock reading, as a continuous arc: half a sine above the
    /// horizon between sunrise and sunset, and half a trough below it through the
    /// night. The old step between a noon pose and a midnight pose made dawn and
    /// dusk jump, which the clouds and stars then followed.
    /// </summary>
    public static float SunElevationAt(float minutes, float sunrise, float sunset, float noonElevation, float midnightElevation)
    {
        float wrapped = Mathf.Repeat(minutes, 1440f);
        float daySpan = Mathf.Max(1f, sunset - sunrise);
        float nightSpan = Mathf.Max(1f, 1440f - daySpan);

        if (wrapped >= sunrise && wrapped < sunset)
            return noonElevation * Mathf.Sin((wrapped - sunrise) / daySpan * Mathf.PI);

        float intoNight = wrapped >= sunset ? wrapped - sunset : wrapped + 1440f - sunset;
        return midnightElevation * Mathf.Sin(intoNight / nightSpan * Mathf.PI);
    }

    void ApplyTimeOfDayImmediate()
    {
        float wrappedMinutes = Mathf.Repeat(currentTimeMinutes, 1440f);
        float daylightSpan = Mathf.Max(1f, sunsetMinutes - sunriseMinutes);
        DayPhase01 = Mathf.Clamp01((wrappedMinutes - sunriseMinutes) / daylightSpan);
        IsNight = wrappedMinutes < sunriseMinutes || wrappedMinutes >= sunsetMinutes;
        SunElevationDegrees = SunElevationAt(wrappedMinutes, sunriseMinutes, sunsetMinutes, noonSunElevation, midnightSunElevation);

        // Azimuth keeps travelling after sunset instead of snapping back east, so the
        // moon (which sits opposite the sun) crosses the sky in one sweep too.
        float nightSpan = Mathf.Max(1f, 1440f - daylightSpan);
        float nightProgress = wrappedMinutes >= sunsetMinutes
            ? (wrappedMinutes - sunsetMinutes) / nightSpan
            : (wrappedMinutes + 1440f - sunsetMinutes) / nightSpan;
        SunAzimuthDegrees = IsNight
            ? Mathf.Lerp(sunsetAzimuth, sunriseAzimuth + 360f, Mathf.Clamp01(nightProgress))
            : Mathf.Lerp(sunriseAzimuth, sunsetAzimuth, Mathf.Clamp01((wrappedMinutes - sunriseMinutes) / daylightSpan));

        if (directionalSun != null)
        {
            // A directional light shines along its forward axis, so x = 90 is level
            // with the horizon and x = 0 points straight down: this is the elevation.
            directionalSun.transform.rotation = Quaternion.Euler(90f - SunElevationDegrees, SunAzimuthDegrees, 0f);
            if (directionalSun.shadows == LightShadows.None) directionalSun.shadows = LightShadows.Soft;
            directionalSun.shadowResolution = UnityEngine.Rendering.LightShadowResolution.VeryHigh;
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
        // GameObject arrays are explicit visual-only assignments. Guessing them
        // by name disabled FrontLeft/FrontRight WheelColliders during daylight.
        // Automatic discovery must only ever bind actual Light components.
    }

    Light[] FindLightsByName(params string[] nameTokens)
    {
        var allLights = FindObjectsByType<Light>(FindObjectsInactive.Include);
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

    void ApplyManagedObjects(GameObject[] objects, bool active)
    {
        if (objects == null)
            return;

        for (int i = 0; i < objects.Length; i++)
        {
            var target = objects[i];
            if (target == null)
                continue;

            // Protect stale serialized assignments too. A lighting toggle must
            // never deactivate vehicle physics, a camera, or their ancestors.
            if (target.GetComponentInChildren<Collider>(true) != null ||
                target.GetComponentInChildren<Rigidbody>(true) != null ||
                target.GetComponentInChildren<Camera>(true) != null)
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
