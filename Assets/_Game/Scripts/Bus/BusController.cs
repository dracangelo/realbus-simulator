using UnityEngine;
using UnityEngine.InputSystem;

public class BusController : MonoBehaviour
{
    [Header("Steering")]
    public WheelCollider[] steerWheels;
    public float maxSteerAngle = 42f;
    public float steerSpeed = 0.1f;
    private float currentSteerAngle = 0f;

    [Header("Weight & Dynamics")]
    public float centerOfMassY = 1.6f;
    public float antiRollForce = 8000f;

    [Header("Data")]
    public EngineSystem engineData;
    public TransmissionSystem transmissionData;

    [Header("Wheels")]
    public WheelCollider[] driveWheels;   // rear 4
    public WheelCollider[] allWheels;     // all 6
    public WheelCollider[] rearWheels;    // rear 4 for parking brake

    [Header("Braking")]
    public float maxBrakeTorque = 12000f;
    public float retarderStrength = 3000f;
    private bool retarderActive = false;
    private bool parkingBrakeActive = false;

    [Header("Brake Fade")]
    public float brakeHeat = 0f;
    public float maxBrakeHeat = 100f;
    private float brakeEffectiveness = 1f;

    [Header("Aerodynamics")]
    public float dragCoefficient = 0.65f;
    public float frontalArea = 8.5f;
    public float airDensity = 1.225f;

    [Header("State")]
    public float currentRPM = 800f;
    public float throttleInput = 0f;
    public float brakeInput = 0f;
    public float currentSpeedKmh = 0f;
    public bool kneelingSuspensionActive = false;

    private Rigidbody rb;

    public bool RetarderActive => retarderActive;
    public bool ParkingBrakeActive => parkingBrakeActive;
    public Rigidbody Rigidbody => rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, centerOfMassY, 0f);
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        throttleInput = keyboard.wKey.isPressed ? 1f : 0f;
        brakeInput = keyboard.sKey.isPressed ? 1f : 0f;

        if (keyboard.eKey.wasPressedThisFrame) transmissionData.ShiftUp();
        if (keyboard.qKey.wasPressedThisFrame) transmissionData.ShiftDown();
        if (keyboard.rKey.wasPressedThisFrame) retarderActive = !retarderActive;
        if (keyboard.pKey.wasPressedThisFrame) parkingBrakeActive = !parkingBrakeActive;
    }

    void FixedUpdate()
    {
        ApplyThrottle();
        ApplySteering();
        ApplyBrakes();
        ApplyRetarder();
        ApplyParkingBrake();
        ApplyAntiRoll();
        UpdateRPM();
        UpdateBrakeFade();
        ApplyAerodynamics();
    }

    void ApplyThrottle()
    {
        float torque = engineData.GetTorque(currentRPM);
        float wheelTorque = torque * transmissionData.GetCurrentRatio()
                          * transmissionData.differentialRatio
                          / 0.5f;

        // Cut throttle if this gear's max speed is reached
        float maxSpeed = transmissionData.GetCurrentMaxSpeed();
        float throttle = (currentSpeedKmh >= maxSpeed) ? 0f : throttleInput;

        foreach (var wheel in driveWheels)
            wheel.motorTorque = wheelTorque * throttle;
    }

    void ApplyBrakes()
    {
        if (parkingBrakeActive) return;

        float effectiveBrake = maxBrakeTorque * brakeInput * brakeEffectiveness;

        foreach (var wheel in allWheels)
        {
            WheelHit hit;
            if (wheel.GetGroundHit(out hit))
            {
                if (hit.forwardSlip < -0.3f && brakeInput > 0f)
                    effectiveBrake *= 0.8f;
            }
            wheel.brakeTorque = effectiveBrake / allWheels.Length;
        }

        if (brakeInput > 0.5f)
            brakeHeat += Time.fixedDeltaTime * 5f * brakeInput;
    }

    void ApplyRetarder()
    {
        if (!retarderActive || throttleInput > 0f) return;

        float resistance = retarderStrength * (currentSpeedKmh / 80f);
        foreach (var wheel in driveWheels)
            wheel.motorTorque = -resistance;
    }

    void ApplyParkingBrake()
    {
        if (parkingBrakeActive)
        {
            foreach (var wheel in rearWheels)
                wheel.brakeTorque = maxBrakeTorque;
            foreach (var wheel in steerWheels)
                wheel.brakeTorque = 0f;
        }
        else if (brakeInput == 0f)
        {
            foreach (var wheel in rearWheels)
                wheel.brakeTorque = 0f;
        }
    }

    void UpdateBrakeFade()
    {
        brakeHeat = Mathf.Max(0f, brakeHeat - Time.fixedDeltaTime * 2f);
        brakeEffectiveness = Mathf.Lerp(1f, 0.7f, brakeHeat / maxBrakeHeat);
    }

    void UpdateRPM()
    {
        currentSpeedKmh = rb.linearVelocity.magnitude * 3.6f;
        currentRPM = Mathf.Clamp(
            500f + currentSpeedKmh * transmissionData.GetCurrentRatio() * 20f,
            500f, 2500f);

        if (throttleInput == 0f && !retarderActive)
        {
            foreach (var wheel in driveWheels)
                wheel.motorTorque = -currentRPM * 0.5f;
        }
    }

    void ApplySteering()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        float steerInput = 0f;
        if (keyboard.aKey.isPressed) steerInput = -1f;
        if (keyboard.dKey.isPressed) steerInput = 1f;

        float speedFactor = Mathf.Clamp01(1f - (currentSpeedKmh / 80f) * 0.85f);
        float targetAngle = maxSteerAngle * speedFactor * steerInput;

        currentSteerAngle = Mathf.Lerp(currentSteerAngle, targetAngle, steerSpeed);

        foreach (var wheel in steerWheels)
            wheel.steerAngle = currentSteerAngle;
    }

    void ApplyAntiRoll()
    {
        ApplyAntiRollPair(allWheels[0], allWheels[1]); // FL, FR
        ApplyAntiRollPair(allWheels[2], allWheels[3]); // ML, MR
        ApplyAntiRollPair(allWheels[4], allWheels[5]); // RL, RR
    }

    void ApplyAntiRollPair(WheelCollider left, WheelCollider right)
    {
        WheelHit hitL, hitR;
        float travelL = 1f, travelR = 1f;

        bool groundedL = left.GetGroundHit(out hitL);
        bool groundedR = right.GetGroundHit(out hitR);

        if (groundedL)
            travelL = (-left.transform.InverseTransformPoint(hitL.point).y
                      - left.radius) / left.suspensionDistance;

        if (groundedR)
            travelR = (-right.transform.InverseTransformPoint(hitR.point).y
                      - right.radius) / right.suspensionDistance;

        float force = (travelL - travelR) * antiRollForce;

        if (groundedL)
            rb.AddForceAtPosition(left.transform.up * -force, left.transform.position);
        if (groundedR)
            rb.AddForceAtPosition(right.transform.up * force, right.transform.position);
    }

    void ApplyAerodynamics()
    {
        float speedMs = rb.linearVelocity.magnitude;
        float dragForce = 0.5f * dragCoefficient * frontalArea
                        * airDensity * speedMs * speedMs;

        if (speedMs > 0.1f)
            rb.AddForce(-rb.linearVelocity.normalized * dragForce);
    }

    public void SetSteerInput(float value)
    {
        currentSteerAngle = Mathf.Lerp(currentSteerAngle,
                            maxSteerAngle * value, steerSpeed);
        foreach (var wheel in steerWheels)
            wheel.steerAngle = currentSteerAngle;
    }

    public void ToggleRetarder()
    {
        retarderActive = !retarderActive;
    }

    public void HonkHorn()
    {
        Debug.Log("HONK!");
    }

    public void RequestKneelingSuspension(bool active)
    {
        kneelingSuspensionActive = active;
    }
}
