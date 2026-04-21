using UnityEngine;

public class FuelSystem : MonoBehaviour
{
    public const float TankCapacityLitres = 300f;
    public const float FuelWarningPercent = 20f;
    public const float CriticalWarningPercent = 5f;

    [Header("References")]
    public BusController busController;
    public PassengerManager passengerManager;
    public FreeDriveSession freeDriveSession;

    [Header("Consumption")]
    public float cityBaseLPer100Km = 35f;
    public float motorwayBaseLPer100Km = 28f;
    public float motorwaySpeedThresholdKmh = 62f;
    public float optimalCruiseRpm = 1350f;

    [Header("Economy")]
    public float dieselPricePerLitreKES = 180f;

    [Header("Runtime")]
    [SerializeField] float currentFuelLitres = TankCapacityLitres;
    [SerializeField] float lastConsumptionLPer100Km = 0f;

    public float CurrentFuelLitres => currentFuelLitres;
    public float CurrentFuelPercent => Mathf.Clamp01(currentFuelLitres / TankCapacityLitres) * 100f;
    public float LastConsumptionLPer100Km => lastConsumptionLPer100Km;
    public bool IsWarningActive => CurrentFuelPercent <= FuelWarningPercent;
    public bool IsCriticalActive => CurrentFuelPercent <= CriticalWarningPercent;

    void Awake()
    {
        if (busController == null)
            busController = GetComponent<BusController>() ?? FindFirstObjectByType<BusController>();
        if (passengerManager == null)
            passengerManager = FindFirstObjectByType<PassengerManager>();
        if (freeDriveSession == null)
            freeDriveSession = FindFirstObjectByType<FreeDriveSession>();

        LoadFromGameState();
        SyncSessionFuelDisplay();
    }

    void FixedUpdate()
    {
        if (!IsDriveActive() || busController == null)
            return;

        float distanceKm = (busController.currentSpeedKmh / 3600f) * Time.fixedDeltaTime;
        if (distanceKm <= 0f)
        {
            SyncSessionFuelDisplay();
            return;
        }

        float baseLPer100Km = busController.currentSpeedKmh >= motorwaySpeedThresholdKmh
            ? motorwayBaseLPer100Km
            : cityBaseLPer100Km;

        float loadFactor = GetLoadFactor();
        float rpmEfficiencyFactor = GetRpmEfficiencyFactor();
        float roadGradientFactor = GetRoadGradientFactor();

        lastConsumptionLPer100Km = baseLPer100Km * loadFactor * rpmEfficiencyFactor * roadGradientFactor;
        float fuelUsed = (lastConsumptionLPer100Km / 100f) * distanceKm;

        currentFuelLitres = Mathf.Max(0f, currentFuelLitres - fuelUsed);
        PersistFuelUsage(fuelUsed);
        SyncSessionFuelDisplay();

        if (currentFuelLitres <= 0f)
            busController.throttleInput = 0f;
    }

    public float GetLoadFactor()
    {
        if (passengerManager == null)
            passengerManager = FindFirstObjectByType<PassengerManager>();

        if (passengerManager == null)
            return 1f;

        float capacity = Mathf.Max(1f, passengerManager.GetMaxCapacity());
        float occupancy = Mathf.Clamp01(passengerManager.currentPassengers / capacity);
        return Mathf.Lerp(1f, 1.18f, occupancy);
    }

    public float GetRpmEfficiencyFactor()
    {
        if (busController == null)
            return 1f;

        float rpmDeltaRatio = Mathf.Abs(busController.currentRPM - optimalCruiseRpm) / Mathf.Max(1f, optimalCruiseRpm);
        return Mathf.Clamp(0.92f + (rpmDeltaRatio * 0.38f), 0.88f, 1.35f);
    }

    public float GetRoadGradientFactor()
    {
        if (busController == null)
            return 1f;

        float slope = Mathf.Clamp(busController.transform.forward.y, -0.18f, 0.18f);
        return Mathf.Clamp(1f + (slope * 2.1f), 0.82f, 1.38f);
    }

    public float GetMissingFuelLitres()
    {
        return Mathf.Max(0f, TankCapacityLitres - currentFuelLitres);
    }

    public float AutoRefuelAtDepot()
    {
        float litresNeeded = GetMissingFuelLitres();
        if (litresNeeded <= 0.01f)
            return 0f;

        float cost = litresNeeded * dieselPricePerLitreKES;
        currentFuelLitres = TankCapacityLitres;

        if (GameState.Instance != null)
        {
            GameState.Instance.vehicleState.fuelCapacityLitres = TankCapacityLitres;
            GameState.Instance.vehicleState.fuelLitres = currentFuelLitres;
            GameState.Instance.vehicleState.lowFuelWarningTriggered = false;
            GameState.Instance.vehicleState.criticalFuelWarningTriggered = false;
        }

        SyncSessionFuelDisplay();
        return cost;
    }

    void LoadFromGameState()
    {
        if (GameState.Instance == null || GameState.Instance.vehicleState == null)
        {
            currentFuelLitres = TankCapacityLitres;
            return;
        }

        var state = GameState.Instance.vehicleState;
        state.fuelCapacityLitres = TankCapacityLitres;
        currentFuelLitres = Mathf.Clamp(state.fuelLitres, 0f, TankCapacityLitres);
    }

    void PersistFuelUsage(float fuelUsed)
    {
        if (GameState.Instance == null || GameState.Instance.vehicleState == null)
            return;

        var state = GameState.Instance.vehicleState;
        state.fuelCapacityLitres = TankCapacityLitres;
        state.fuelLitres = currentFuelLitres;
        state.totalFuelConsumedLitres += Mathf.Max(0f, fuelUsed);
        state.lowFuelWarningTriggered = CurrentFuelPercent <= FuelWarningPercent;
        state.criticalFuelWarningTriggered = CurrentFuelPercent <= CriticalWarningPercent;
    }

    void SyncSessionFuelDisplay()
    {
        if (freeDriveSession == null)
            freeDriveSession = FindFirstObjectByType<FreeDriveSession>();
        if (freeDriveSession == null)
            return;

        freeDriveSession.fuelCapacityLitres = TankCapacityLitres;
        freeDriveSession.fuelLevel = CurrentFuelPercent;
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
