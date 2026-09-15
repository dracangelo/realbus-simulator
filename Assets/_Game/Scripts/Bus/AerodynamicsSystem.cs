using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AerodynamicsSystem : MonoBehaviour
{
    public float dragCoefficient = 0.65f;
    public float frontalArea = 8.5f;
    public float airDensity = 1.225f;
    public float sideArea = 36f;
    public Vector3 windVelocity;
    Rigidbody body;
    BusController bus;

    void Awake() { body = GetComponent<Rigidbody>(); bus = GetComponent<BusController>(); }

    public static Vector3 DragForce(Vector3 velocity, float coefficient, float area, float density)
    {
        return -0.5f * Mathf.Max(0f, coefficient) * Mathf.Max(0f, area)
            * Mathf.Max(0f, density) * velocity.magnitude * velocity;
    }

    void FixedUpdate()
    {
        body.AddForce(DragForce(body.linearVelocity, bus != null ? bus.dragCoefficient : dragCoefficient,
            bus != null ? bus.frontalArea : frontalArea, bus != null ? bus.airDensity : airDensity));
        float crosswind = Vector3.Dot(windVelocity, transform.right);
        body.AddForce(transform.right * (0.5f * airDensity * sideArea * crosswind * Mathf.Abs(crosswind)));
    }
}
