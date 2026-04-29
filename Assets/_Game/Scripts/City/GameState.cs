using UnityEngine;

[System.Serializable]
public class PlayerEconomyData
{
    public float balanceKES = 254000f;
    public float driverReputationRating = 100f;
}

[System.Serializable]
public class VehiclePersistentState
{
    public string activeBusId = "fleet.standard_single_decker";
    public bool isElectricBus = false;
    public string energyUnitLabel = "L";
    public int passengerCapacity = 70;
    public int seatedCapacity = 36;

    public float fuelCapacityLitres = 300f;
    public float fuelLitres = 300f;
    public float totalFuelConsumedLitres = 0f;
    public bool lowFuelWarningTriggered = false;
    public bool criticalFuelWarningTriggered = false;

    public float brakeWearNormalized = 0.08f;
    public float[] axleTyreWearNormalized = new float[] { 0.06f, 0.08f, 0.08f };
    public float engineHours = 12f;
    public bool doorFunctionOperational = true;
    public bool exteriorLightsOperational = true;

    public void ApplyBusSpec(BusSpec spec, bool preserveEnergyPercent = true)
    {
        if (spec == null)
            return;

        float preservedEnergyPercent = preserveEnergyPercent && fuelCapacityLitres > 0.01f
            ? Mathf.Clamp01(fuelLitres / fuelCapacityLitres)
            : 1f;

        activeBusId = spec.busId;
        isElectricBus = spec.IsElectric;
        energyUnitLabel = string.IsNullOrWhiteSpace(spec.energyUnitLabel)
            ? (spec.IsElectric ? "kWh" : "L")
            : spec.energyUnitLabel;
        passengerCapacity = spec.PassengerCapacity;
        seatedCapacity = spec.SeatedCapacity;
        fuelCapacityLitres = Mathf.Max(1f, spec.energyCapacityUnits);
        fuelLitres = Mathf.Clamp(preservedEnergyPercent * fuelCapacityLitres, 0f, fuelCapacityLitres);
    }
}

[System.Serializable]
public class MissionSettlementData
{
    public float grossEarningsKES = 0f;
    public float fuelRefuelCostKES = 0f;
    public float maintenanceCostKES = 0f;
    public float netEarningsKES = 0f;
    public bool autoRefuelApplied = false;
}

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    [Header("Player Selections")]
    public CountryDefinition selectedCountry;
    public CityDefinition selectedCity;
    public BusRoute selectedRoute;

    [Header("Persistent Progress")]
    public PlayerEconomyData economy = new PlayerEconomyData();
    public VehiclePersistentState vehicleState = new VehiclePersistentState();
    public MissionSettlementData lastMissionSettlement = new MissionSettlementData();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (economy == null)
            economy = new PlayerEconomyData();
        if (vehicleState == null)
            vehicleState = new VehiclePersistentState();
        if (vehicleState.axleTyreWearNormalized == null || vehicleState.axleTyreWearNormalized.Length != 3)
            vehicleState.axleTyreWearNormalized = new float[] { 0.06f, 0.08f, 0.08f };
        if (lastMissionSettlement == null)
            lastMissionSettlement = new MissionSettlementData();

        if (BusFleetManager.Instance != null)
            vehicleState.ApplyBusSpec(BusFleetManager.Instance.GetSelectedBusSpec(), preserveEnergyPercent: true);
    }

    public void SelectCountry(CountryDefinition country)
    {
        selectedCountry = country;
        selectedCity = null; // reset city when country changes
        selectedRoute = null;
        Debug.Log($"GameState: Country selected — {country.countryName}");
    }

    public void SelectCity(CityDefinition city)
    {
        selectedCity = city;
        selectedRoute = null;
        Debug.Log($"GameState: City selected — {city.cityName}");
    }

    public void SelectRoute(BusRoute route)
    {
        selectedRoute = route;
        Debug.Log($"GameState: Route selected — {route.routeName}");
    }

    public float GetFuelPercent()
    {
        float capacity = Mathf.Max(1f, vehicleState != null ? vehicleState.fuelCapacityLitres : 300f);
        float litres = vehicleState != null ? vehicleState.fuelLitres : capacity;
        return Mathf.Clamp01(litres / capacity) * 100f;
    }
}
