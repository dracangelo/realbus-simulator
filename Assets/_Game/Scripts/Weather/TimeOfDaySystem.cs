using UnityEngine;

public class TimeOfDaySystem : MonoBehaviour
{
    public static TimeOfDaySystem Instance { get; private set; }

    [Header("References")]
    public Light directionalSun;
    public SkyController skyController;

    [Header("Clock")]
    [Tooltip("Follow the mission timetable. When off, the world clock keeps its own compressed day cycle: the timetable is a running sheet for stops and punctuality, not a lighting timer.")]
    public bool followScheduleManager = false;
    [Tooltip("Advance the world clock. Turning this off pins the sky to currentTimeMinutes without touching the mission schedule.")]
    public bool advanceTimeAutomatically = true;
    [Tooltip("Real minutes for one full 24-hour world day when not following the mission schedule. 30 minutes gives a sunrise-to-sunrise cycle a player can actually see; 0 falls back to secondsPerGameMinute.")]
    public float dayLengthRealMinutes = 30f;
    public float startTimeMinutes = 480f;
    public float currentTimeMinutes = 480f;
    [Tooltip("Legacy 1 game minute = N real seconds rate. Only used when dayLengthRealMinutes is 0.")]
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

    [Header("Sun Colour")]
    [Tooltip("The sun on the horizon: deep orange, the light that makes golden hour read as gold.")]
    public Color horizonSunColor = new Color(1.00f, 0.46f, 0.20f, 1f);
    [Tooltip("A few degrees up: still warm, already bright.")]
    public Color lowSunColor = new Color(1.00f, 0.80f, 0.58f, 1f);
    [Tooltip("Overhead: nearly neutral, so noon is not tinted.")]
    public Color noonSunColor = new Color(1.00f, 0.985f, 0.960f, 1f);

    [Header("Lighting")]
    public Gradient skyTintByDayPhase;
    public AnimationCurve ambientIntensityByDayPhase = AnimationCurve.EaseInOut(0f, 0.18f, 1f, 1f);
    public AnimationCurve sunIntensityByDayPhase = AnimationCurve.EaseInOut(0f, 0.05f, 1f, 1f);
    // Keep the clear-sky shadow in the brief's 0.8–0.9 band all day. The old
    // curve fell to 0.7 at sunset, which made a clear late afternoon look hazier
    // than the weather actually was. Cloud still multiplies this down.
    public AnimationCurve shadowStrengthByDayPhase = AnimationCurve.EaseInOut(0f, 0.92f, 1f, 0.82f);

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
    /// <summary>How much of the day's shadow survives the cloud (1 clear, 0.12 overcast).</summary>
    public float WeatherShadowScale { get; private set; } = 1f;
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
        // Start the world at the timetable's opening hour, then let the compressed
        // day cycle take over. The two clocks answer different questions: one is the
        // running board, the other is how high the sun is.
        currentTimeMinutes = Mathf.Repeat(
            ScheduleManager.Instance != null ? ScheduleManager.Instance.currentTimeMinutes : startTimeMinutes,
            1440f);
        ApplyTimeOfDayImmediate();
    }

    void Update()
    {
        if (followScheduleManager && ScheduleManager.Instance != null)
        {
            currentTimeMinutes = ScheduleManager.Instance.currentTimeMinutes;
        }
        else if (advanceTimeAutomatically)
        {
            currentTimeMinutes = AdvanceMinutes(
                currentTimeMinutes, Time.deltaTime, dayLengthRealMinutes, secondsPerGameMinute);
        }

        ApplyTimeOfDayImmediate();
    }

    /// <summary>
    /// Real seconds per game minute for a configured day length. A 30 minute day
    /// runs 1.25 seconds per game minute; falling back to 0 keeps the old explicit
    /// rate for scenes and tests that authored one.
    /// </summary>
    public static float SecondsPerGameMinuteFor(float dayLengthRealMinutes, float fallbackSecondsPerGameMinute)
    {
        if (dayLengthRealMinutes > 0f)
            return Mathf.Max(0.01f, dayLengthRealMinutes * 60f / 1440f);
        return Mathf.Max(0.01f, fallbackSecondsPerGameMinute);
    }

    /// <summary>
    /// Advances and wraps the clock. Pure, so the visible day cycle can be checked
    /// without starting a frame.
    /// </summary>
    public static float AdvanceMinutes(float currentMinutes, float realSeconds,
        float dayLengthRealMinutes, float fallbackSecondsPerGameMinute)
    {
        float rate = SecondsPerGameMinuteFor(dayLengthRealMinutes, fallbackSecondsPerGameMinute);
        return Mathf.Repeat(currentMinutes + Mathf.Max(0f, realSeconds) / rate, 1440f);
    }

    /// <summary>Pins the world clock to an hour and updates the sky immediately.</summary>
    public void SetTimeOfDay(float minutes, bool applyImmediately = true)
    {
        currentTimeMinutes = Mathf.Repeat(minutes, 1440f);
        if (applyImmediately) ApplyTimeOfDayImmediate();
    }

    /// <summary>Shifts the world clock by a number of game minutes.</summary>
    public void ShiftTimeOfDay(float minutes)
        => SetTimeOfDay(currentTimeMinutes + minutes);

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
    /// How warm the sunlight is at the current sun height. Weather sets the colour
    /// of the light (grey under cloud, blue in snow); the sun's height sets its
    /// temperature, and without this every state was equally white at dawn and at
    /// noon - the single biggest reason a low sun read as a dimmer noon.
    /// </summary>
    public Color GetSunTint()
        => SunTintFor(SunElevationDegrees, horizonSunColor, lowSunColor, noonSunColor);

    /// <summary>
    /// Sunlight colour for a solar elevation: deep orange on the horizon, gold a few
    /// degrees up, neutral by mid-morning. Pure, because this is the rule that makes
    /// sunrise look like sunrise.
    /// </summary>
    public static Color SunTintFor(float elevationDegrees, Color horizon, Color low, Color noon)
    {
        if (elevationDegrees <= 0f) return horizon;
        if (elevationDegrees < 8f) return Color.Lerp(horizon, low, elevationDegrees / 8f);
        if (elevationDegrees < 25f) return Color.Lerp(low, noon, (elevationDegrees - 8f) / 17f);
        return noon;
    }

    /// <summary>
    /// How much of the direct sun reaches the ground, from its elevation. A sun that
    /// is already below the horizon still had a full-strength directional light in
    /// the scene, which lit rooftops from underneath at night; this fades it out over
    /// the same few degrees where the real thing disappears into the horizon haze.
    /// </summary>
    public float GetSunVisibilityFactor() => SunVisibilityFor(SunElevationDegrees);

    public static float SunVisibilityFor(float elevationDegrees)
        => Mathf.SmoothStep(0f, 1f, Mathf.InverseLerp(-6f, 2f, elevationDegrees));

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
            // The clock says how long the shadows should be; the weather says whether
            // they should be there at all.
            WeatherShadowScale = SkyEnvironmentController.Instance != null
                ? WeatherLighting.ShadowStrengthScale(SkyEnvironmentController.Instance.cloudCoverage)
                : 1f;
            directionalSun.shadowStrength = GetShadowStrength() * WeatherShadowScale;
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

    /// <summary>
    /// True when the gradient was actually authored. A component that was added at
    /// run time arrives with Unity's built-in two-key white gradient rather than a
    /// null one, so a length check alone would keep a colourless sky.
    /// </summary>
    public bool HasAuthoredSkyGradient() => IsAuthoredSkyGradient(skyTintByDayPhase);

    public static bool IsAuthoredSkyGradient(Gradient gradient)
    {
        if (gradient == null) return false;
        GradientColorKey[] keys = gradient.colorKeys;
        if (keys == null || keys.Length < 3) return false;

        for (int i = 0; i < keys.Length; i++)
        {
            Color color = keys[i].color;
            if (Mathf.Abs(color.r - 1f) + Mathf.Abs(color.g - 1f) + Mathf.Abs(color.b - 1f) > 0.15f)
                return true;
        }
        return false;
    }

    /// <summary>
    /// Warmth is confined to the hour around sunrise and sunset. The old keys held
    /// orange through the first fifth of the day, so the sky at 08:00 - a 34 degree
    /// sun - was a brown-purple that no real morning has.
    /// </summary>
    public static Gradient CreateDefaultSkyGradient()
    {
        // Unity's Gradient takes at most eight colour keys; nine are rejected whole,
        // leaving the built-in white gradient in place and a colourless sky.
        var gradient = new Gradient();
        gradient.SetKeys(
            new[]
            {
                new GradientColorKey(new Color(0.05f, 0.07f, 0.16f), 0f),
                new GradientColorKey(new Color(0.85f, 0.42f, 0.24f), 0.035f),
                new GradientColorKey(new Color(0.98f, 0.68f, 0.42f), 0.09f),
                new GradientColorKey(new Color(0.60f, 0.76f, 0.96f), 0.14f),
                new GradientColorKey(new Color(0.50f, 0.72f, 0.98f), 0.5f),
                new GradientColorKey(new Color(0.60f, 0.74f, 0.94f), 0.78f),
                new GradientColorKey(new Color(0.98f, 0.60f, 0.32f), 0.92f),
                new GradientColorKey(new Color(0.06f, 0.08f, 0.18f), 1f)
            },
            new[]
            {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            });
        return gradient;
    }

    void EnsureDefaultGradient()
    {
        if (HasAuthoredSkyGradient()) return;
        skyTintByDayPhase = CreateDefaultSkyGradient();
    }
}
