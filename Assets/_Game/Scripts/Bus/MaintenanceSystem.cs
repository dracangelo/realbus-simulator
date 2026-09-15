using UnityEngine;

public class MaintenanceSystem : MonoBehaviour
{
    [Header("References")]
    public BusController busController;
    public PassengerManager passengerManager;

    [Header("Wear Rates")]
    public float brakeWearPerHeavyBrakeSecond = 0.0001f;
    public float brakeWearPerRetarderSecond = 0.000018f;
    public float tyreWearPerKm = 0.00055f;
    public float tyreSlipWearMultiplier = 0.0004f;
    public float engineHoursPerSecond = 1f / 3600f;

    [Header("Service Costs")]
    public float brakeServiceCostKES = 28000f;
    public float tyreServiceCostPerAxleKES = 22000f;
    public float engineServiceCostPerHourKES = 380f;

    [Header("Runtime")]
    [SerializeField, Range(0f, 1f)] float brakeWearNormalized = 0.08f;
    [SerializeField] float[] axleTyreWearNormalized = new float[] { 0.06f, 0.08f, 0.08f };
    [SerializeField] float engineHours = 12f;

    float[] initialForwardStiffness;
    float[] initialSidewaysStiffness;

    public float BrakeWearNormalized => brakeWearNormalized;
    public float EngineHours => engineHours;
    public float BrakeConditionPercent => (1f - brakeWearNormalized) * 100f;
    public float EngineConditionPercent => Mathf.Clamp01(1f - (engineHours / 250f)) * 100f;
    public int AxleCount => axleTyreWearNormalized != null ? axleTyreWearNormalized.Length : 0;

    void Awake()
    {
        if (busController == null)
            busController = GetComponent<BusController>() ?? FindFirstObjectByType<BusController>();
        if (passengerManager == null)
            passengerManager = FindFirstObjectByType<PassengerManager>();

        EnsureAxleArray();
        LoadFromGameState();
        CacheWheelBaseFriction();
        ApplyTyreGripPenalty();
    }

    void FixedUpdate()
    {
        if (!IsDriveActive() || busController == null)
            return;

        TrackBrakeWear();
        TrackTyreWear();
        TrackEngineHours();
        ApplyTyreGripPenalty();
        PersistState();
    }

    public float GetAxleWearNormalized(int axleIndex)
    {
        EnsureAxleArray();
        if (axleIndex < 0 || axleIndex >= axleTyreWearNormalized.Length)
            return 0f;
        return axleTyreWearNormalized[axleIndex];
    }

    public float GetAxleConditionPercent(int axleIndex)
    {
        return (1f - GetAxleWearNormalized(axleIndex)) * 100f;
    }

    public float GetWheelGripMultiplier(int wheelIndex)
    {
        int axleIndex = GetAxleIndexForWheel(wheelIndex);
        float wear = GetAxleWearNormalized(axleIndex);
        return RealismRules.TyreGrip(wear);
    }

    public float AutoServiceAndGetCost()
    {
        EnsureAxleArray();

        float tyreWearSum = 0f;
        for (int i = 0; i < axleTyreWearNormalized.Length; i++)
            tyreWearSum += axleTyreWearNormalized[i];

        float cost =
            (brakeWearNormalized * brakeServiceCostKES) +
            (tyreWearSum * tyreServiceCostPerAxleKES) +
            (engineHours * engineServiceCostPerHourKES * 0.02f);

        brakeWearNormalized = 0f;
        for (int i = 0; i < axleTyreWearNormalized.Length; i++)
            axleTyreWearNormalized[i] = 0f;
        engineHours = Mathf.Max(0f, engineHours - 25f);

        ApplyTyreGripPenalty();
        PersistState();
        return cost;
    }

    void TrackBrakeWear()
    {
        if (busController.currentSpeedKmh < 0.1f) return;
        float heavyBrakeFactor = Mathf.Pow(Mathf.Clamp01(busController.brakeInput), 1.4f);
        float speedFactor = Mathf.Clamp(busController.currentSpeedKmh / 80f, 0.15f, 1.35f);
        float heatFactor = 1f + Mathf.Clamp01(busController.brakeHeat / Mathf.Max(1f, busController.maxBrakeHeat));

        brakeWearNormalized += Time.fixedDeltaTime * heavyBrakeFactor * speedFactor * heatFactor * brakeWearPerHeavyBrakeSecond;

        if (busController.RetarderActive && busController.brakeInput < 0.2f)
            brakeWearNormalized += Time.fixedDeltaTime * speedFactor * brakeWearPerRetarderSecond;

        brakeWearNormalized = Mathf.Clamp01(brakeWearNormalized);
    }

    void TrackTyreWear()
    {
        EnsureAxleArray();
        if (busController.allWheels == null)
            return;

        float distanceKm = (busController.currentSpeedKmh / 3600f) * Time.fixedDeltaTime;

        for (int axle = 0; axle < axleTyreWearNormalized.Length; axle++)
        {
            int startWheel = axle * 2;
            float slipWear = 0f;
            int wheelCount = 0;

            for (int wheelOffset = 0; wheelOffset < 2; wheelOffset++)
            {
                int wheelIndex = startWheel + wheelOffset;
                if (wheelIndex >= busController.allWheels.Length)
                    continue;

                WheelCollider wheel = busController.allWheels[wheelIndex];
                if (wheel == null)
                    continue;

                if (wheel.GetGroundHit(out WheelHit hit))
                    slipWear += Mathf.Abs(hit.forwardSlip) + Mathf.Abs(hit.sidewaysSlip);

                wheelCount++;
            }

            float averageSlip = wheelCount > 0 ? slipWear / wheelCount : 0f;
            axleTyreWearNormalized[axle] += (distanceKm * tyreWearPerKm) + (averageSlip * tyreSlipWearMultiplier * Time.fixedDeltaTime);
            axleTyreWearNormalized[axle] = Mathf.Clamp01(axleTyreWearNormalized[axle]);
        }
    }

    void TrackEngineHours()
    {
        if (!busController.FuelDepleted && busController.currentRPM > 0f)
            engineHours += Time.fixedDeltaTime * engineHoursPerSecond;
    }

    void CacheWheelBaseFriction()
    {
        if (busController == null || busController.allWheels == null)
            return;

        initialForwardStiffness = new float[busController.allWheels.Length];
        initialSidewaysStiffness = new float[busController.allWheels.Length];

        for (int i = 0; i < busController.allWheels.Length; i++)
        {
            var wheel = busController.allWheels[i];
            if (wheel == null)
                continue;

            initialForwardStiffness[i] = wheel.forwardFriction.stiffness;
            initialSidewaysStiffness[i] = wheel.sidewaysFriction.stiffness;
        }
    }

    void ApplyTyreGripPenalty()
    {
        // The surface controller is the sole friction writer when composed systems are present.
        if (busController != null && busController.GetComponent<RoadSurfaceFrictionController>() != null) return;
        if (busController == null || busController.allWheels == null)
            return;

        if (initialForwardStiffness == null || initialForwardStiffness.Length != busController.allWheels.Length)
            CacheWheelBaseFriction();

        for (int i = 0; i < busController.allWheels.Length; i++)
        {
            var wheel = busController.allWheels[i];
            if (wheel == null)
                continue;

            float gripMultiplier = GetWheelGripMultiplier(i);

            var forward = wheel.forwardFriction;
            forward.stiffness = initialForwardStiffness[i] * gripMultiplier;
            wheel.forwardFriction = forward;

            var sideways = wheel.sidewaysFriction;
            sideways.stiffness = initialSidewaysStiffness[i] * gripMultiplier;
            wheel.sidewaysFriction = sideways;
        }
    }

    void LoadFromGameState()
    {
        if (GameState.Instance == null || GameState.Instance.vehicleState == null)
            return;

        var state = GameState.Instance.vehicleState;
        brakeWearNormalized = Mathf.Clamp01(state.brakeWearNormalized);
        engineHours = Mathf.Max(0f, state.engineHours);

        if (state.axleTyreWearNormalized != null && state.axleTyreWearNormalized.Length == axleTyreWearNormalized.Length)
        {
            for (int i = 0; i < axleTyreWearNormalized.Length; i++)
                axleTyreWearNormalized[i] = Mathf.Clamp01(state.axleTyreWearNormalized[i]);
        }
    }

    void PersistState()
    {
        if (GameState.Instance == null || GameState.Instance.vehicleState == null)
            return;

        var state = GameState.Instance.vehicleState;
        state.brakeWearNormalized = brakeWearNormalized;
        state.engineHours = engineHours;

        if (state.axleTyreWearNormalized == null || state.axleTyreWearNormalized.Length != axleTyreWearNormalized.Length)
            state.axleTyreWearNormalized = new float[axleTyreWearNormalized.Length];

        for (int i = 0; i < axleTyreWearNormalized.Length; i++)
            state.axleTyreWearNormalized[i] = axleTyreWearNormalized[i];
    }

    void EnsureAxleArray()
    {
        if (axleTyreWearNormalized == null || axleTyreWearNormalized.Length != 3)
            axleTyreWearNormalized = new float[] { 0.06f, 0.08f, 0.08f };
    }

    int GetAxleIndexForWheel(int wheelIndex)
    {
        if (wheelIndex <= 1) return 0;
        if (wheelIndex <= 3) return 1;
        return 2;
    }

    bool IsDriveActive()
    {
        if (FreeDriveSession.Instance != null && FreeDriveSession.Instance.sessionActive)
            return true;
        if (MissionManager.Instance != null && MissionManager.Instance.routeActive)
            return true;
        return false;
    }
}
