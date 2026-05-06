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
    public Transform modelRoot;
    public bool replaceModelFromSpec = false;

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

    [Header("Kneeling Suspension")]
    public float kneelingDropMeters = 0.08f;
    public float kneelingLerpSpeed = 4f;

    private Rigidbody rb;
    private EngineSystem runtimeEngineInstance;
    private TransmissionSystem runtimeTransmissionInstance;
    private GameObject activeModelInstance;
    private Vector3 modelRootBaseLocalPosition;
    private bool modelRootPositionCaptured;

    public bool RetarderActive => retarderActive;
    public bool ParkingBrakeActive => parkingBrakeActive;
    public Rigidbody Rigidbody => rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            return;

        if (rb.mass <= 0.01f)
            rb.mass = 13200f;
        rb.centerOfMass = new Vector3(0f, centerOfMassY, 0f);

        CaptureModelRootBasePosition();
    }

    void OnDestroy()
    {
        if (runtimeEngineInstance != null)
            Destroy(runtimeEngineInstance);
        if (runtimeTransmissionInstance != null)
            Destroy(runtimeTransmissionInstance);
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            throttleInput = keyboard.wKey.isPressed ? 1f : 0f;
            brakeInput = keyboard.sKey.isPressed ? 1f : 0f;

            if (transmissionData != null && keyboard.eKey.wasPressedThisFrame) transmissionData.ShiftUp();
            if (transmissionData != null && keyboard.qKey.wasPressedThisFrame) transmissionData.ShiftDown();
            if (keyboard.rKey.wasPressedThisFrame) retarderActive = !retarderActive;
            if (keyboard.pKey.wasPressedThisFrame) parkingBrakeActive = !parkingBrakeActive;
        }

        UpdateKneelingVisuals(Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (rb == null || engineData == null || transmissionData == null)
            return;

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
        float minRpm = engineData != null ? Mathf.Max(350f, engineData.idleRPM) : 500f;
        float maxRpm = engineData != null ? Mathf.Max(minRpm + 100f, engineData.maxRPM) : 2500f;
        currentRPM = Mathf.Clamp(
            minRpm + currentSpeedKmh * transmissionData.GetCurrentRatio() * 20f,
            minRpm, maxRpm);

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

    public void ApplyBusSpec(BusSpec spec)
    {
        if (spec == null)
            return;

        maxSteerAngle = Mathf.Max(1f, spec.maxSteerAngle);
        steerSpeed = Mathf.Max(0.01f, spec.steerSpeed);
        centerOfMassY = spec.centerOfMassY;
        antiRollForce = Mathf.Max(0f, spec.antiRollForce);
        maxBrakeTorque = Mathf.Max(0f, spec.maxBrakeTorque);
        retarderStrength = Mathf.Max(0f, spec.retarderStrength);
        dragCoefficient = Mathf.Max(0f, spec.dragCoefficient);
        frontalArea = Mathf.Max(0.1f, spec.frontalArea);
        airDensity = Mathf.Max(0.1f, spec.airDensity);

        if (rb == null)
            rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.mass = Mathf.Max(1f, spec.rigidbodyMassKg);
            rb.centerOfMass = new Vector3(0f, centerOfMassY, 0f);
        }

        ApplyRuntimeEngine(spec.engineProfile);
        ApplyRuntimeTransmission(spec.transmissionProfile);

        if (replaceModelFromSpec && spec.modelPrefab != null)
            ReplaceModel(spec.modelPrefab);
    }

    void ApplyRuntimeEngine(EngineSystem source)
    {
        if (runtimeEngineInstance != null)
            Destroy(runtimeEngineInstance);

        runtimeEngineInstance = source != null ? Instantiate(source) : null;
        engineData = runtimeEngineInstance != null ? runtimeEngineInstance : source;
        if (engineData != null)
            currentRPM = Mathf.Max(350f, engineData.idleRPM);
    }

    void ApplyRuntimeTransmission(TransmissionSystem source)
    {
        if (runtimeTransmissionInstance != null)
            Destroy(runtimeTransmissionInstance);

        runtimeTransmissionInstance = source != null ? Instantiate(source) : null;
        transmissionData = runtimeTransmissionInstance != null ? runtimeTransmissionInstance : source;
        if (transmissionData != null)
            transmissionData.currentGear = 0;
    }

    void ReplaceModel(GameObject modelPrefab)
    {
        Transform parent = modelRoot != null ? modelRoot : transform;
        if (activeModelInstance != null)
            Destroy(activeModelInstance);

        activeModelInstance = Instantiate(modelPrefab, parent);
        activeModelInstance.transform.localPosition = Vector3.zero;
        activeModelInstance.transform.localRotation = Quaternion.identity;
        activeModelInstance.transform.localScale = Vector3.one;
    }

    void CaptureModelRootBasePosition()
    {
        if (modelRoot != null)
        {
            modelRootBaseLocalPosition = modelRoot.localPosition;
            modelRootPositionCaptured = true;
        }
    }

    void UpdateKneelingVisuals(float deltaTime)
    {
        if (modelRoot == null)
            return;

        if (!modelRootPositionCaptured)
            CaptureModelRootBasePosition();

        Vector3 targetLocal = modelRootBaseLocalPosition + Vector3.down * (kneelingSuspensionActive ? kneelingDropMeters : 0f);
        modelRoot.localPosition = Vector3.Lerp(modelRoot.localPosition, targetLocal, deltaTime * kneelingLerpSpeed);
    }
}
