using UnityEngine;

/// <summary>
/// Keeps a 12-ton bus planted without turning it into an arcade car.
///
/// The WheelCollider setup gives the chassis strong springs and anti-roll, but a
/// heavy, tall body still rocks on its suspension and can gather yaw after a kerb
/// strike or a hard lane change. This component adds saturated angular damping on
/// the yaw, roll and pitch axes only once the bus is actually rolling, so parking
/// and low-speed manoeuvring keep their raw feel while cruising stays settled.
///
/// Torque is capped against the vehicle mass, so the assist can slow a rotation but
/// can never overpower the road and flip the bus on its own.
/// </summary>
[DefaultExecutionOrder(50)]
[RequireComponent(typeof(Rigidbody))]
public class BusStabilityAssist : MonoBehaviour
{
    [Header("Reference")]
    public BusController bus;

    [Header("Enable")]
    [Tooltip("Damping only ramps in above this speed; a stationary bus is left alone.")]
    public float fullEffectSpeedKmh = 25f;

    [Header("Angular Damping (1/s time constants)")]
    [Tooltip("Resists yaw (spinning/fishtailing).")]
    public float yawDamping = 2.4f;
    [Tooltip("Resists body roll — the slow rocking that makes a bus feel top-heavy.")]
    public float rollDamping = 4.5f;
    [Tooltip("Resists pitch (dive/squat oscillation after braking or bumps).")]
    public float pitchDamping = 3.2f;

    [Header("Torque Caps (fraction of mass × gravity)")]
    public float yawTorqueFactor = 0.55f;
    public float rollTorqueFactor = 0.7f;
    public float pitchTorqueFactor = 0.45f;

    [Header("Hard Limit")]
    [Tooltip("Angular speed ceiling; a bus should never corkscrew after a collision.")]
    public float maxAngularVelocityDegPerSec = 140f;

    Rigidbody rb;

    public void Bind(BusController controller, Rigidbody body = null)
    {
        bus = controller;
        rb = body != null ? body : (controller != null ? controller.Rigidbody : GetComponent<Rigidbody>());
    }

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (bus == null) bus = GetComponent<BusController>();
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (rb == null || rb.isKinematic) return;

        float speed = bus != null ? bus.currentSpeedKmh : rb.linearVelocity.magnitude * 3.6f;
        float ramp = SpeedDampingFactor(speed, fullEffectSpeedKmh);
        if (ramp <= 0.001f) return;

        Vector3 angular = rb.angularVelocity;
        float yawRate = Vector3.Dot(angular, transform.up) * Mathf.Rad2Deg;
        float rollRate = Vector3.Dot(angular, transform.forward) * Mathf.Rad2Deg;
        float pitchRate = Vector3.Dot(angular, transform.right) * Mathf.Rad2Deg;

        Vector3 inertia = rb.inertiaTensor;
        float gravity = Physics.gravity.magnitude;

        float yawTorque = AngularDamperTorque(yawRate, inertia.y, yawDamping * ramp, MaxCorrectionTorque(rb.mass, gravity, yawTorqueFactor));
        float rollTorque = AngularDamperTorque(rollRate, inertia.z, rollDamping * ramp, MaxCorrectionTorque(rb.mass, gravity, rollTorqueFactor));
        float pitchTorque = AngularDamperTorque(pitchRate, inertia.x, pitchDamping * ramp, MaxCorrectionTorque(rb.mass, gravity, pitchTorqueFactor));

        rb.AddTorque(transform.up * yawTorque + transform.forward * rollTorque + transform.right * pitchTorque, ForceMode.Force);

        float ceiling = Mathf.Abs(maxAngularVelocityDegPerSec) * Mathf.Deg2Rad;
        if (ceiling > 0.01f && angular.sqrMagnitude > ceiling * ceiling)
            rb.angularVelocity = angular.normalized * ceiling;
    }

    /// <summary>0 at a standstill, 1 once the bus reaches full-effect speed.</summary>
    public static float SpeedDampingFactor(float speedKmh, float fullEffectSpeedKmh)
    {
        float full = Mathf.Max(1f, fullEffectSpeedKmh);
        return Mathf.Clamp01(Mathf.Abs(speedKmh) / full);
    }

    /// <summary>Largest corrective torque the assist may apply for a given mass.</summary>
    public static float MaxCorrectionTorque(float mass, float gravity, float factor)
    {
        return Mathf.Max(0f, mass) * Mathf.Max(0f, gravity) * Mathf.Max(0f, factor);
    }

    /// <summary>
    /// Torque that opposes an angular rate, capped so the assist stays a damper and
    /// never a thruster. Returns zero for zero rate.
    /// </summary>
    public static float AngularDamperTorque(float rateDegreesPerSecond, float inertia, float gainPerSecond, float maxTorque)
    {
        if (rateDegreesPerSecond == 0f) return 0f;
        float rateRadians = rateDegreesPerSecond * Mathf.Deg2Rad;
        float torque = -rateRadians * Mathf.Max(0f, inertia) * Mathf.Max(0f, gainPerSecond);
        return Mathf.Clamp(torque, -maxTorque, maxTorque);
    }
}
