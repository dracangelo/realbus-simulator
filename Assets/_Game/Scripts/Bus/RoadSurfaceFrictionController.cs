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

    [Header("Base stiffness")]
    public float baseForwardStiffness = 1.5f;
    public float baseSidewaysStiffness = 1.3f;

    [Header("Smoothing")]
    public float lerpSpeed = 6f;

    float currentGrip = 1f;

    void Start()
    {
        if (busController == null)
            busController = FindFirstObjectByType<BusController>();
        if (surfaceDetector == null)
            surfaceDetector = FindFirstObjectByType<RoadSurfaceDetector>();
    }

    void Update()
    {
        if (busController == null || busController.allWheels == null) return;

        float weatherGrip = 1f;
        if (WeatherSystem.Instance != null)
            weatherGrip = Mathf.Clamp01(WeatherSystem.Instance.GetRoadGripMultiplier());

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

            var fwd = wc.forwardFriction;
            fwd.stiffness = baseForwardStiffness * grip;
            wc.forwardFriction = fwd;

            var side = wc.sidewaysFriction;
            side.stiffness = baseSidewaysStiffness * grip;
            wc.sidewaysFriction = side;
        }
    }
}

