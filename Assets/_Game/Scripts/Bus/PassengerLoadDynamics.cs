using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BusController))]
public class PassengerLoadDynamics : MonoBehaviour
{
    public PassengerManager passengerManager;
    public float passengerMassKg = 75f;
    public Vector3 passengerCenter = new Vector3(0f, 2f, 0f);
    public int testPassengerCount;
    public float emptyMassKg = 12000f;
    Rigidbody body;
    BusController bus;
    float previousTotal;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        bus = GetComponent<BusController>();
        if (passengerManager == null)
            passengerManager = PassengerManager.Instance != null
                ? PassengerManager.Instance
                : FindAnyObjectByType<PassengerManager>();
        emptyMassKg = body.mass;
        previousTotal = body.mass;
    }

    public static Vector3 CombinedCenter(float emptyMass, Vector3 emptyCenter, float loadMass, Vector3 loadCenter)
    {
        return (emptyMass * emptyCenter + loadMass * loadCenter) / Mathf.Max(1f, emptyMass + loadMass);
    }

    void FixedUpdate()
    {
        // A fleet change can replace the base mass during play.
        if (!Mathf.Approximately(body.mass, previousTotal)) emptyMassKg = body.mass;
        int count = passengerManager != null ? passengerManager.currentPassengers : testPassengerCount;
        float load = Mathf.Max(0, count) * Mathf.Max(0f, passengerMassKg);
        previousTotal = Mathf.Max(1f, emptyMassKg) + load;
        body.mass = previousTotal;
        body.centerOfMass = CombinedCenter(emptyMassKg, new Vector3(0f, bus.centerOfMassY, 0f), load, passengerCenter);
    }
}
