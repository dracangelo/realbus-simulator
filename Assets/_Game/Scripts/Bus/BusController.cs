using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class BusController : MonoBehaviour
{
    [Header("Steering")]
    public WheelCollider[] steerWheels;
    public float maxSteerAngle = 42f;
    public float steerSpeed = 0.1f;
    public float wheelbase = 6.2f;
    public float trackWidth = 2.1f;
    public float steerInput;
    public bool readKeyboard = true;
    public AudioSource engineAudio;
    public AudioSource hornAudio;
    float mobileThrottle, mobileBrake, mobileSteering;
    float shiftCooldown;
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

    float[] originalSuspensionTargets;
    private Rigidbody rb;
    private EngineSystem runtimeEngineInstance;
    private TransmissionSystem runtimeTransmissionInstance;
    private GameObject activeModelInstance;
    private Vector3 modelRootBaseLocalPosition;
    private bool modelRootPositionCaptured;

    public bool ServiceBrakeInterlock { get; set; }
    public bool FuelDepleted { get; set; }
    public float AssistThrottleLimit { get; set; } = 1f;
    public float AssistBrakeFloor { get; set; }
    public Transform dockingReference;
    public bool RetarderActive => retarderActive;
    public bool ParkingBrakeActive => parkingBrakeActive;
    public Rigidbody Rigidbody => rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            return;

        if (rb.mass <= 0.01f)
            rb.mass = 12000f;
        if (runtimeTransmissionInstance == null && transmissionData != null)
            ApplyRuntimeTransmission(transmissionData);
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
        if (readKeyboard)
        {
            var keyboard = Keyboard.current;
            throttleInput = Mathf.Max(mobileThrottle, keyboard != null && keyboard.wKey.isPressed ? 1f : 0f);
            brakeInput = Mathf.Max(mobileBrake, keyboard != null && keyboard.sKey.isPressed ? 1f : 0f);
            float keyboardSteer = keyboard == null ? 0f :
                (keyboard.dKey.isPressed ? 1f : 0f) - (keyboard.aKey.isPressed ? 1f : 0f);
            steerInput = Mathf.Abs(mobileSteering) > 0.01f ? mobileSteering : keyboardSteer;
            if (keyboard != null)
            {
                if (transmissionData != null && keyboard.eKey.wasPressedThisFrame) transmissionData.ShiftUp();
                if (transmissionData != null && keyboard.qKey.wasPressedThisFrame) transmissionData.ShiftDown();
                if (keyboard.rKey.wasPressedThisFrame) ToggleRetarder();
                if (keyboard.pKey.wasPressedThisFrame) SetParkingBrake(!parkingBrakeActive);
                if (keyboard.hKey.wasPressedThisFrame) HonkHorn();
                if (keyboard.xKey.wasPressedThisFrame) SelectReverse(! (transmissionData != null && transmissionData.currentGear < 0));
            }
        }
        if (engineAudio != null && engineData != null)
            engineAudio.pitch = Mathf.Lerp(0.6f, 2f, Mathf.InverseLerp(engineData.idleRPM, engineData.maxRPM, currentRPM));

        UpdateKneelingVisuals(Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (rb == null || engineData == null || transmissionData == null)
            return;

        // A route start owns the interlock during briefing, countdown and stops.
        // It must never remain latched while the mission is actively driving.
        MissionManager activeMission = MissionManager.Instance;
        if (ServiceBrakeInterlock && activeMission != null && activeMission.missionState == MissionState.InProgress)
            ServiceBrakeInterlock = false;

        currentSpeedKmh = rb.linearVelocity.magnitude * 3.6f;
        UpdateRPM();
        shiftCooldown -= Time.fixedDeltaTime;
        if (shiftCooldown <= 0f && transmissionData.UpdateAutomatic(currentRPM, engineData.maxRPM))
        {
            shiftCooldown = 0.75f;
            UpdateRPM();
        }
        UpdateBrakeFade();
        ApplyThrottle();
        ApplySteering();
        ApplyBrakes();
        ApplyRetarder();
        ApplyParkingBrake();
        ApplyAntiRoll();
        UpdateKneelingSuspension();
        ApplyAerodynamics();
    }

    void ApplyThrottle()
    {
        int count = CountWheels(driveWheels);
        if (count == 0) return;
        // WheelCollider takes Nm. Dividing by radius would instead produce force (N).
        float torque = engineData.GetTorque(currentRPM) * transmissionData.GetCurrentRatio()
            * transmissionData.differentialRatio / count;
        float throttle = currentRPM >= engineData.maxRPM ? 0f : Mathf.Min(Mathf.Clamp01(throttleInput), Mathf.Clamp01(AssistThrottleLimit));
        foreach (var wheel in driveWheels)
        {
            if (wheel == null) continue;
            wheel.motorTorque = parkingBrakeActive || ServiceBrakeInterlock || FuelDepleted || brakeInput > 0f ? 0f : torque * throttle;
        }
    }

    public static int CountWheels(WheelCollider[] wheels)
    {
        int count = 0;
        if (wheels != null) foreach (var wheel in wheels) if (wheel != null) count++;
        return count;
    }

    void ApplyBrakes()
    {
        int count = CountWheels(allWheels);
        if (count == 0) return;
        float baseBrake = maxBrakeTorque * (ServiceBrakeInterlock ? 1f : Mathf.Max(Mathf.Clamp01(brakeInput), Mathf.Clamp01(AssistBrakeFloor))) * brakeEffectiveness / count;
        foreach (var wheel in allWheels)
        {
            if (wheel == null) continue;
            bool locked = currentSpeedKmh > 5f && wheel.GetGroundHit(out WheelHit hit)
                && (Mathf.Abs(hit.forwardSlip) > 0.3f || Mathf.Abs(wheel.rpm) < 5f);
            wheel.brakeTorque = baseBrake * (locked ? 0.8f : 1f);
        }
    }

    void ApplyRetarder()
    {
        int count = CountWheels(driveWheels);
        if (count == 0 || throttleInput > 0f) return;
        float resistance = currentRPM * 0.5f;
        if (retarderActive) resistance += retarderStrength * currentSpeedKmh / 80f;
        // Passive brakes oppose wheel rotation in both forward and reverse and cannot propel the bus.
        foreach (var wheel in driveWheels)
            if (wheel != null) wheel.brakeTorque += resistance / count;
    }

    void ApplyParkingBrake()
    {
        if (!parkingBrakeActive || rearWheels == null) return;
        foreach (var wheel in rearWheels)
            if (wheel != null) wheel.brakeTorque = Mathf.Max(wheel.brakeTorque, maxBrakeTorque);
    }

    void UpdateBrakeFade()
    {
        float rate = brakeInput > 0.5f && currentSpeedKmh > 1f
            ? maxBrakeHeat / 60f * Mathf.Clamp01(brakeInput) : -maxBrakeHeat / 60f;
        brakeHeat = Mathf.Clamp(brakeHeat + rate * Time.fixedDeltaTime, 0f, Mathf.Max(1f, maxBrakeHeat));
        brakeEffectiveness = Mathf.Lerp(1f, 0.7f, brakeHeat / Mathf.Max(1f, maxBrakeHeat));
    }

    void UpdateRPM()
    {
        float rpm = 0f;
        int count = CountWheels(driveWheels);
        if (driveWheels != null) foreach (var wheel in driveWheels)
            if (wheel != null) rpm += Mathf.Abs(wheel.rpm);
        currentRPM = Mathf.Clamp(rpm / Mathf.Max(1, count) * Mathf.Abs(transmissionData.GetCurrentRatio())
            * transmissionData.differentialRatio, engineData.idleRPM, engineData.maxRPM);
    }

    public static Vector2 AckermannAngles(float angle, float wheelbase, float track)
    {
        if (Mathf.Abs(angle) < 0.001f) return Vector2.zero;
        float radius = Mathf.Max(0.1f, wheelbase) / Mathf.Tan(Mathf.Abs(angle) * Mathf.Deg2Rad);
        float inner = Mathf.Atan(Mathf.Max(0.1f, wheelbase) / Mathf.Max(0.1f, radius - track * 0.5f)) * Mathf.Rad2Deg;
        float outer = Mathf.Atan(Mathf.Max(0.1f, wheelbase) / (radius + track * 0.5f)) * Mathf.Rad2Deg;
        return angle > 0f ? new Vector2(outer, inner) : new Vector2(-inner, -outer);
    }

    void ApplySteering()
    {
        float limit = Mathf.Lerp(maxSteerAngle, 8f, Mathf.Clamp01(currentSpeedKmh / 80f));
        currentSteerAngle = Mathf.Lerp(currentSteerAngle, limit * Mathf.Clamp(steerInput, -1f, 1f),
            1f - Mathf.Pow(1f - Mathf.Clamp01(steerSpeed), Time.fixedDeltaTime / 0.02f));
        Vector2 angles = AckermannAngles(currentSteerAngle, wheelbase, trackWidth);
        if (steerWheels == null) return;
        for (int i = 0; i < steerWheels.Length; i++)
            if (steerWheels[i] != null) steerWheels[i].steerAngle = i % 2 == 0 ? angles.x : angles.y;
    }

    void ApplyAntiRoll()
    {
        if (allWheels == null) return;
        for (int i = 0; i + 1 < allWheels.Length; i += 2)
            ApplyAntiRollPair(allWheels[i], allWheels[i + 1]);
    }

    void ApplyAntiRollPair(WheelCollider left, WheelCollider right)
    {
        if (left == null || right == null) return;
        WheelHit hitL, hitR;
        float travelL = 1f, travelR = 1f;

        bool groundedL = left.GetGroundHit(out hitL);
        bool groundedR = right.GetGroundHit(out hitR);

        if (groundedL)
            travelL = (-left.transform.InverseTransformPoint(hitL.point).y
                      - left.radius) / Mathf.Max(0.001f, left.suspensionDistance);

        if (groundedR)
            travelR = (-right.transform.InverseTransformPoint(hitR.point).y
                      - right.radius) / Mathf.Max(0.001f, right.suspensionDistance);

        float force = (travelL - travelR) * antiRollForce;

        if (groundedL)
            rb.AddForceAtPosition(left.transform.up * -force, left.transform.position);
        if (groundedR)
            rb.AddForceAtPosition(right.transform.up * force, right.transform.position);
    }

    void ApplyAerodynamics()
    {
        // Dedicated component owns drag when present; retain compatibility with existing prefabs.
        if (TryGetComponent<AerodynamicsSystem>(out var aerodynamics) && aerodynamics.enabled) return;
        rb.AddForce(AerodynamicsSystem.DragForce(rb.linearVelocity, dragCoefficient, frontalArea, airDensity));
    }

    public void SetSteerInput(float value) { mobileSteering = steerInput = Mathf.Clamp(value, -1f, 1f); }
    public void SetMobileInput(float throttle, float brake, float steering)
    {
        mobileThrottle = Mathf.Clamp01(throttle);
        mobileBrake = Mathf.Clamp01(brake);
        SetSteerInput(steering);
        throttleInput = mobileThrottle;
        brakeInput = mobileBrake;
    }
    public void SetParkingBrake(bool active) { parkingBrakeActive = active; }
    public void SelectReverse(bool reverse)
    {
        if (transmissionData != null && currentSpeedKmh < 1f)
            transmissionData.currentGear = reverse ? -1 : 0;
    }

    public void ToggleRetarder()
    {
        retarderActive = !retarderActive;
    }

    public void HonkHorn()
    {
        if (hornAudio != null) hornAudio.Play();
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

        BusUpgradeModifiers modifiers = UpgradeManager.EnsureExists().GetModifiers(spec.busId);
        ApplyUpgradeModifiers(modifiers);

        if (replaceModelFromSpec && spec.modelPrefab != null)
            ReplaceModel(spec.modelPrefab);
    }

    public void ApplyUpgradeModifiers(BusUpgradeModifiers modifiers)
    {
        maxBrakeTorque *= Mathf.Max(0.01f, modifiers.brakeTorque);
        steerSpeed *= Mathf.Max(0.01f, modifiers.steeringResponse);

        if (engineData == null || Mathf.Approximately(modifiers.engineTorque, 1f))
            return;

        AnimationCurve source = engineData.torqueCurve;
        if (source == null)
            return;

        Keyframe[] keys = source.keys;
        float multiplier = Mathf.Max(0.01f, modifiers.engineTorque);
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].value *= multiplier;
            keys[i].inTangent *= multiplier;
            keys[i].outTangent *= multiplier;
        }
        engineData.torqueCurve = new AnimationCurve(keys);
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

    void UpdateKneelingSuspension()
    {
        if (allWheels == null) return;
        if (originalSuspensionTargets == null || originalSuspensionTargets.Length != allWheels.Length)
        {
            originalSuspensionTargets = new float[allWheels.Length];
            for (int i = 0; i < allWheels.Length; i++)
                if (allWheels[i] != null) originalSuspensionTargets[i] = allWheels[i].suspensionSpring.targetPosition;
        }
        for (int i = 0; i < allWheels.Length; i++)
        {
            var wheel = allWheels[i];
            if (wheel == null) continue;
            var spring = wheel.suspensionSpring;
            float drop = kneelingSuspensionActive ? kneelingDropMeters / Mathf.Max(0.01f, wheel.suspensionDistance) : 0f;
            spring.targetPosition = Mathf.Lerp(spring.targetPosition, Mathf.Clamp01(originalSuspensionTargets[i] + drop), Time.fixedDeltaTime * kneelingLerpSpeed);
            wheel.suspensionSpring = spring;
        }
    }

    void UpdateKneelingVisuals(float deltaTime)
    {
        if (modelRoot == null || CountWheels(allWheels) > 0)
            return;

        if (!modelRootPositionCaptured)
            CaptureModelRootBasePosition();

        Vector3 targetLocal = modelRootBaseLocalPosition + Vector3.down * (kneelingSuspensionActive ? kneelingDropMeters : 0f);
        modelRoot.localPosition = Vector3.Lerp(modelRoot.localPosition, targetLocal, deltaTime * kneelingLerpSpeed);
    }
}
