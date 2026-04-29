using UnityEngine;

public class BatterySystem : MonoBehaviour
{
    public const float ChargeWarningPercent = 20f;
    public const float CriticalWarningPercent = 5f;

    [Header("References")]
    public BusController busController;
    public PassengerManager passengerManager;
    public FreeDriveSession freeDriveSession;

    [Header("Battery Profile")]
    public float batteryCapacityKWh = 420f;
    public float cityBaseKWhPer100Km = 120f;
    public float motorwayBaseKWhPer100Km = 102f;
    public float motorwaySpeedThresholdKmh = 66f;
    public float optimalCruiseRpm = 900f;
    public float electricityPricePerKWhKES = 64f;

    [Header("Runtime")]
    [SerializeField] float currentChargeKWh = 420f;
    [SerializeField] float lastConsumptionKWhPer100Km = 0f;

    public float CurrentChargeKWh => currentChargeKWh;
    public float CurrentChargePercent => Mathf.Clamp01(currentChargeKWh / Mathf.Max(1f, batteryCapacityKWh)) * 100f;
    public float LastConsumptionKWhPer100Km => lastConsumptionKWhPer100Km;
    public bool IsWarningActive => CurrentChargePercent <= ChargeWarningPercent;
    public bool IsCriticalActive => CurrentChargePercent <= CriticalWarningPercent;

    void Awake()
    {
        if (busController == null)
            busController = GetComponent<BusController>() ?? FindFirstObjectByType<BusController>();
        if (passengerManager == null)
            passengerManager = FindFirstObjectByType<PassengerManager>();
        if (freeDriveSession == null)
            freeDriveSession = FindFirstObjectByType<FreeDriveSession>();

        LoadFromGameState();
        SyncSessionDisplay();
    }

    void FixedUpdate()
    {
        if (!IsDriveActive() || busController == null)
            return;

        float distanceKm = (busController.currentSpeedKmh / 3600f) * Time.fixedDeltaTime;
        if (distanceKm <= 0f)
        {
            SyncSessionDisplay();
            return;
        }

        float baseKWhPer100Km = busController.currentSpeedKmh >= motorwaySpeedThresholdKmh
            ? motorwayBaseKWhPer100Km
            : cityBaseKWhPer100Km;

        float loadFactor = GetLoadFactor();
        float rpmEfficiencyFactor = GetRpmEfficiencyFactor();
        float roadGradientFactor = GetRoadGradientFactor();

        lastConsumptionKWhPer100Km = baseKWhPer100Km * loadFactor * rpmEfficiencyFactor * roadGradientFactor;
        float chargeUsed = (lastConsumptionKWhPer100Km / 100f) * distanceKm;

        currentChargeKWh = Mathf.Max(0f, currentChargeKWh - chargeUsed);
        PersistUsage(chargeUsed);
        SyncSessionDisplay();

        if (currentChargeKWh <= 0f)
            busController.throttleInput = 0f;
    }

    public void ApplyBusSpec(BusSpec spec)
    {
        if (spec == null)
            return;

        float preservedPercent = CurrentChargePercent / 100f;
        batteryCapacityKWh = Mathf.Max(1f, spec.energyCapacityUnits);
        cityBaseKWhPer100Km = Mathf.Max(1f, spec.cityConsumptionPer100Km);
        motorwayBaseKWhPer100Km = Mathf.Max(1f, spec.motorwayConsumptionPer100Km);
        motorwaySpeedThresholdKmh = Mathf.Max(1f, spec.motorwaySpeedThresholdKmh);
        optimalCruiseRpm = Mathf.Max(1f, spec.optimalCruiseRpm);
        electricityPricePerKWhKES = Mathf.Max(0f, spec.unitPriceKES);
        currentChargeKWh = Mathf.Clamp(preservedPercent * batteryCapacityKWh, 0f, batteryCapacityKWh);
        PersistUsage(0f);
        SyncSessionDisplay();
    }

    public float AutoRechargeAtDepot()
    {
        float kWhNeeded = Mathf.Max(0f, batteryCapacityKWh - currentChargeKWh);
        if (kWhNeeded <= 0.01f)
            return 0f;

        float cost = kWhNeeded * electricityPricePerKWhKES;
        currentChargeKWh = batteryCapacityKWh;

        if (GameState.Instance != null)
        {
            var state = GameState.Instance.vehicleState;
            state.fuelCapacityLitres = batteryCapacityKWh;
            state.fuelLitres = currentChargeKWh;
            state.energyUnitLabel = "kWh";
            state.isElectricBus = true;
            state.lowFuelWarningTriggered = false;
            state.criticalFuelWarningTriggered = false;
        }

        SyncSessionDisplay();
        return cost;
    }

    float GetLoadFactor()
    {
        if (passengerManager == null)
            passengerManager = FindFirstObjectByType<PassengerManager>();

        if (passengerManager == null)
            return 1f;

        float capacity = Mathf.Max(1f, passengerManager.GetMaxCapacity());
        float occupancy = Mathf.Clamp01(passengerManager.currentPassengers / capacity);
        return Mathf.Lerp(0.92f, 1.12f, occupancy);
    }

    float GetRpmEfficiencyFactor()
    {
        if (busController == null)
            return 1f;

        float rpmDeltaRatio = Mathf.Abs(busController.currentRPM - optimalCruiseRpm) / Mathf.Max(1f, optimalCruiseRpm);
        return Mathf.Clamp(0.9f + (rpmDeltaRatio * 0.22f), 0.86f, 1.18f);
    }

    float GetRoadGradientFactor()
    {
        if (busController == null)
            return 1f;

        float slope = Mathf.Clamp(busController.transform.forward.y, -0.18f, 0.18f);
        return Mathf.Clamp(1f + (slope * 1.9f), 0.84f, 1.34f);
    }

    void LoadFromGameState()
    {
        if (GameState.Instance == null || GameState.Instance.vehicleState == null)
        {
            currentChargeKWh = batteryCapacityKWh;
            return;
        }

        var state = GameState.Instance.vehicleState;
        batteryCapacityKWh = Mathf.Max(1f, state.fuelCapacityLitres);
        currentChargeKWh = Mathf.Clamp(state.fuelLitres, 0f, batteryCapacityKWh);
        state.energyUnitLabel = "kWh";
        state.isElectricBus = true;
    }

    void PersistUsage(float chargeUsed)
    {
        if (GameState.Instance == null || GameState.Instance.vehicleState == null)
            return;

        var state = GameState.Instance.vehicleState;
        state.fuelCapacityLitres = batteryCapacityKWh;
        state.fuelLitres = currentChargeKWh;
        state.totalFuelConsumedLitres += Mathf.Max(0f, chargeUsed);
        state.energyUnitLabel = "kWh";
        state.isElectricBus = true;
        state.lowFuelWarningTriggered = IsWarningActive;
        state.criticalFuelWarningTriggered = IsCriticalActive;
    }

    void SyncSessionDisplay()
    {
        if (freeDriveSession == null)
            freeDriveSession = FindFirstObjectByType<FreeDriveSession>();
        if (freeDriveSession == null)
            return;

        freeDriveSession.fuelCapacityLitres = batteryCapacityKWh;
        freeDriveSession.fuelLevel = CurrentChargePercent;
    }

    bool IsDriveActive()
    {
        if (freeDriveSession != null && freeDriveSession.sessionActive)
            return true;
        if (MissionManager.Instance != null && MissionManager.Instance.routeActive)
            return true;
        return false;
    }
}
