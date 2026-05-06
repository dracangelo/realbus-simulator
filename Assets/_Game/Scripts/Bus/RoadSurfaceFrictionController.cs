using UnityEngine;

/// <summary>
/// Applies WheelCollider friction stiffness based on:
/// - weather grip (from WeatherSystem)
/// - road type (from RoadSurfaceDetector)
/// </summary>
public class RoadSurfaceFrictionController : MonoBehaviour
{
    [Header("References")]
    public BusController busController;
    public RoadSurfaceDetector surfaceDetector;
    MaintenanceSystem maintenanceSystem;

    [Header("Base stiffness")]
    public float baseForwardStiffness = 1.5f;
    public float baseSidewaysStiffness = 1.3f;

    [Header("Weather")]
    public bool useWeatherSystemForNonRainConditions = true;
    public float rainGripMultiplier = 0.7f;

    [Header("Smoothing")]
    public float lerpSpeed = 6f;

    float currentGrip = 1f;

    void Start()
    {
        if (busController == null)
            busController = FindFirstObjectByType<BusController>();
        if (surfaceDetector == null)
            surfaceDetector = FindFirstObjectByType<RoadSurfaceDetector>();
        if (maintenanceSystem == null)
            maintenanceSystem = FindFirstObjectByType<MaintenanceSystem>();
    }

    void Update()
    {
        if (busController == null || busController.allWheels == null) return;

        float weatherGrip = ResolveWeatherGripMultiplier();

        currentGrip = Mathf.Lerp(currentGrip, weatherGrip, Time.deltaTime * lerpSpeed);

        var wheels = busController.allWheels;
        for (int i = 0; i < wheels.Length; i++)
        {
            var wc = wheels[i];
            if (wc == null) continue;

            float surfaceMult = 1f;
            if (surfaceDetector != null &&
                surfaceDetector.perWheelSurfaceMultiplier != null &&
                i < surfaceDetector.perWheelSurfaceMultiplier.Length)
            {
                surfaceMult = surfaceDetector.perWheelSurfaceMultiplier[i];
            }

            float grip = Mathf.Clamp(surfaceMult * currentGrip, 0.05f, 2.0f);
            if (maintenanceSystem != null)
                grip *= maintenanceSystem.GetWheelGripMultiplier(i);

            var fwd = wc.forwardFriction;
            fwd.stiffness = baseForwardStiffness * grip;
            wc.forwardFriction = fwd;

            var side = wc.sidewaysFriction;
            side.stiffness = baseSidewaysStiffness * grip;
            wc.sidewaysFriction = side;
        }
    }

    float ResolveWeatherGripMultiplier()
    {
        if (WeatherSystem.Instance == null)
            return 1f;

        if (WeatherSystem.Instance.IsRaining())
        {
            if (surfaceDetector != null)
                return Mathf.Clamp(surfaceDetector.wetMultiplier, 0.05f, 1f);

            return Mathf.Clamp(rainGripMultiplier, 0.05f, 1f);
        }

        if (!useWeatherSystemForNonRainConditions)
            return 1f;

        return Mathf.Clamp(WeatherSystem.Instance.GetRoadGripMultiplier(), 0.05f, 1f);
    }
}
