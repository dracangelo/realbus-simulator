using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TrafficParticipant))]
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
    [Tooltip("Low-speed torque-converter assistance used to get a heavy bus moving from rest.")]
    [Min(0f)] public float launchAssistAcceleration = 0.85f;

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
    [Tooltip("Releases the parking brake when the driver applies throttle from a standstill. Route starts drop the bus in a depot bay with the brake on, so without this the bus cannot be driven out until the park-brake key is found.")]
    public bool releaseParkingBrakeOnThrottle = true;

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
    /// <summary>
    /// Set while a loading overlay is still covering the world. Every drive input is
    /// held at zero, so a held key or a touch that was already down cannot roll the
    /// bus out from behind the title card.
    /// </summary>
    public bool inputLocked = false;

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

    public void Teleport(Vector3 position, Quaternion rotation)
    {
        // Update the physics pose too: interpolation can otherwise restore the
        // old rigidbody pose after a Transform-only route-start teleport.
        var body = rb != null ? rb : GetComponent<Rigidbody>();
        if (body != null)
        {
            body.position = position;
            body.rotation = rotation;
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            body.WakeUp();
        }
        transform.SetPositionAndRotation(position, rotation);
        Physics.SyncTransforms();
    }

    void Awake()
    {
        // The player's bus has to be visible to the same perception rules as AI
        // traffic, or cars only react to each other and drive straight through it.
        if (!TryGetComponent(out TrafficParticipant participant))
            participant = gameObject.AddComponent<TrafficParticipant>();
        participant.RefreshOwner();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            return;

        RepairWheelReferences();

        if (rb.mass <= 0.01f)
            rb.mass = 12000f;
        if (runtimeTransmissionInstance == null && transmissionData != null)
            ApplyRuntimeTransmission(transmissionData);
        // AerodynamicsSystem (or ApplyAerodynamics below) already owns drag.
        // The old 0.15/s damping adds ~15 kN resistance at 30 km/h to a 12 t
        // bus, enough to stall acceleration just after the first upshift.
        rb.linearDamping = 0f;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        // A heavier bus needs a couple more solver passes to keep the wheel contacts
        // from jittering under load, and a raised angular ceiling so a kerb strike
        // does not get clipped into a hard spin.
        rb.solverIterations = Mathf.Max(rb.solverIterations, 10);
        rb.maxAngularVelocity = Mathf.Max(rb.maxAngularVelocity, 10f);
        rb.centerOfMass = new Vector3(0f, centerOfMassY, 0f);
        ConfigureSuspensionForVehicleMass();

        if (!TryGetComponent(out BusStabilityAssist stability))
            stability = gameObject.AddComponent<BusStabilityAssist>();
        stability.Bind(this, rb);

        CaptureModelRootBasePosition();
    }

    void RepairWheelReferences()
    {
        WheelCollider[] discovered = GetComponentsInChildren<WheelCollider>(true);
        if (CountWheels(allWheels) == 0) allWheels = discovered;
        if (CountWheels(steerWheels) == 0 && discovered.Length >= 2)
            steerWheels = new[] { discovered[0], discovered[1] };
        if (CountWheels(driveWheels) == 0 && discovered.Length >= 2)
            driveWheels = new[] { discovered[discovered.Length - 2], discovered[discovered.Length - 1] };
        if (CountWheels(rearWheels) == 0)
            rearWheels = driveWheels;
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
        // A loading overlay owns the bus until the world is playable. This has to be
        // checked before the keyboard read: the overlay covers the touch pedals but
        // a physical keyboard (or a held touch that was already down) would still
        // send input straight through the title card.
        if (inputLocked)
        {
            mobileThrottle = mobileBrake = mobileSteering = 0f;
            throttleInput = brakeInput = steerInput = 0f;
        }
        else if (readKeyboard)
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

        // Enforced here as well as in Update: a mobile control that writes the input
        // fields directly must not be able to drive the bus while the loading card
        // still covers the world. FixedUpdate runs after every Update, so this is the
        // last word before the drive maths reads the pedals.
        if (inputLocked)
        {
            mobileThrottle = mobileBrake = mobileSteering = 0f;
            throttleInput = brakeInput = steerInput = 0f;
        }

        // A route start owns the interlock during briefing, countdown and stops.
        // It must never remain latched while the mission is actively driving.
        MissionManager activeMission = MissionManager.Instance;
        if (ServiceBrakeInterlock && ((activeMission != null && activeMission.missionState == MissionState.InProgress) ||
            (FreeDriveSession.Instance != null && FreeDriveSession.Instance.sessionActive)))
            ServiceBrakeInterlock = false;

        currentSpeedKmh = rb.linearVelocity.magnitude * 3.6f;
        UpdateParkingBrakeRelease();
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
        bool driveAllowed = !parkingBrakeActive && !ServiceBrakeInterlock && !FuelDepleted && brakeInput <= 0f;
        foreach (var wheel in driveWheels)
        {
            if (wheel == null) continue;
            wheel.motorTorque = driveAllowed ? torque * throttle : 0f;
        }

        // Heavy buses use a torque converter to provide strong launch torque.
        // This also prevents WheelCollider static-friction stalls at route spawn.
        // Gear index 0 is first/Drive in TransmissionSystem; negative is Reverse.
        if (driveAllowed && throttle > .01f && currentSpeedKmh < 6f && HasGroundedDriveWheel())
        {
            float fade = 1f - currentSpeedKmh / 6f;
            float direction = transmissionData.currentGear < 0 ? -1f : 1f;
            rb.AddForce(transform.forward * (direction * rb.mass * launchAssistAcceleration * throttle * fade), ForceMode.Force);
        }
    }

    /// <summary>
    /// Sizes suspension for the actual vehicle mass. The imported four-wheel
    /// prefabs previously used car-strength springs, allowing a 12-ton chassis
    /// to bottom out on its BoxCollider and stall against the road.
    /// </summary>
    public void ConfigureSuspensionForVehicleMass()
    {
        int wheelCount = CountWheels(allWheels);
        if (rb == null || wheelCount == 0) return;

        float loadPerWheel = rb.mass * Physics.gravity.magnitude / wheelCount;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel == null) continue;
            float desiredCompression = Mathf.Max(0.06f, wheel.suspensionDistance * 0.45f);
            float requiredSpring = Mathf.Clamp(loadPerWheel / desiredCompression, 60000f, 320000f);
            JointSpring spring = wheel.suspensionSpring;
            spring.spring = Mathf.Max(spring.spring, requiredSpring);
            // Damp the supported chassis mass, not the much lighter wheel.
            spring.damper = Mathf.Max(spring.damper, 2f * Mathf.Sqrt(spring.spring * rb.mass / wheelCount) * 0.65f);
            spring.targetPosition = Mathf.Clamp(spring.targetPosition, 0.45f, 0.6f);
            wheel.suspensionSpring = spring;
        }
        // Configure substeps once and recalculate loads after mass/COM changes.
        foreach (WheelCollider wheel in allWheels)
            if (wheel != null) { wheel.ConfigureVehicleSubsteps(5f, 12, 15); wheel.ResetSprungMasses(); break; }
    }

    bool HasGroundedDriveWheel()
    {
        if (driveWheels == null) return false;
        foreach (WheelCollider wheel in driveWheels)
            if (wheel != null && wheel.isGrounded) return true;
        return false;
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

    /// <summary>
    /// Modern bus behaviour: asking for drive releases the park brake. A route
    /// starts with the bus parked in a depot bay ("release park brake and proceed"),
    /// and the brake also gates throttle completely, so a player who never finds
    /// the park-brake control was simply stuck in the bay. The manual P toggle still
    /// works, and the brake is never released while the service brake is applied or
    /// the bus is already rolling.
    /// </summary>
    void UpdateParkingBrakeRelease()
    {
        if (ShouldReleaseParkingBrake(throttleInput, brakeInput, currentSpeedKmh)) SetParkingBrake(false);
    }

    /// <summary>
    /// The release rule on its own, so it can be stated and tested without a
    /// physics step: the driver is asking for drive, is not braking, is not already
    /// rolling, and nothing else (fuel, service interlock, the option itself) holds
    /// the bus.
    /// </summary>
    public bool ShouldReleaseParkingBrake(float throttle, float brake, float speedKmh)
        => releaseParkingBrakeOnThrottle && parkingBrakeActive
           && throttle > 0.05f && brake <= 0.05f && speedKmh <= 1f
           && !FuelDepleted && !ServiceBrakeInterlock;

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
        float limit = SafeSteeringLimit(maxSteerAngle, wheelbase, currentSpeedKmh);
        currentSteerAngle = Mathf.Lerp(currentSteerAngle, limit * Mathf.Clamp(steerInput, -1f, 1f),
            1f - Mathf.Pow(1f - Mathf.Clamp01(steerSpeed), Time.fixedDeltaTime / 0.02f));
        Vector2 angles = AckermannAngles(currentSteerAngle, wheelbase, trackWidth);
        if (steerWheels == null) return;
        for (int i = 0; i < steerWheels.Length; i++)
            if (steerWheels[i] != null) steerWheels[i].steerAngle = i % 2 == 0 ? angles.x : angles.y;
    }

    public static float SafeSteeringLimit(float parkingAngle, float axleDistance, float speedKmh)
    {
        // Limit commanded lateral acceleration to 0.3 g. Retain the full
        // parking angle at walking speed instead of allowing 30-degree turns
        // at urban traffic speeds, which can overturn a tall bus.
        float speed = Mathf.Abs(speedKmh) / 3.6f;
        float dynamicLimit = Mathf.Atan(3f * Mathf.Max(1f, axleDistance) / Mathf.Max(1f, speed * speed)) * Mathf.Rad2Deg;
        return Mathf.Min(Mathf.Abs(parkingAngle), dynamicLimit);
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

        float force = (Mathf.Clamp(travelL, 0f, 1f) - Mathf.Clamp(travelR, 0f, 1f)) * antiRollForce;

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

    public void SetSteerInput(float value)
    {
        mobileSteering = inputLocked ? 0f : Mathf.Clamp(value, -1f, 1f);
        steerInput = mobileSteering;
    }

    public void SetMobileInput(float throttle, float brake, float steering)
    {
        // Refuse at the source as well as in Update/FixedUpdate: a control cluster
        // that writes after the bus's own Update must not be able to push the
        // pedals while the loading card is up.
        mobileThrottle = inputLocked ? 0f : Mathf.Clamp01(throttle);
        mobileBrake = inputLocked ? 0f : Mathf.Clamp01(brake);
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
            ConfigureSuspensionForVehicleMass();
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
        ImportedVehicleVisualRepair.DisableEmbeddedCameras(activeModelInstance.transform);
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
            float target = Mathf.Lerp(spring.targetPosition, Mathf.Clamp01(originalSuspensionTargets[i] + drop), Time.fixedDeltaTime * kneelingLerpSpeed);
            if (!Mathf.Approximately(target, spring.targetPosition))
            {
                spring.targetPosition = target;
                wheel.suspensionSpring = spring;
            }
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
