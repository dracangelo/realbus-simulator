using UnityEngine;

public enum BusEnergyType
{
    Diesel,
    Electric
}

[CreateAssetMenu(fileName = "BusSpec", menuName = "RealBus/Bus Spec")]
public class BusSpec : ScriptableObject
{
    [Header("Identity")]
    public string busId = "fleet.standard";
    public string displayName = "Standard Single-Decker";
    [TextArea(2, 4)] public string description = "Core city fleet bus for dense urban service.";
    public int requiredRank = 1;

    [Header("Visuals")]
    public GameObject modelPrefab;
    public Sprite previewSprite;

    [Header("Capacity")]
    public int seatedCapacity = 36;
    public int standingCapacity = 34;
    public int totalCapacity = 70;
    public int doorOpenCapacityLimit = 70;
    public float bodyLengthMeters = 12f;

    [Header("Energy")]
    public BusEnergyType energyType = BusEnergyType.Diesel;
    public float energyCapacityUnits = 300f;
    public string energyUnitLabel = "L";
    public float cityConsumptionPer100Km = 35f;
    public float motorwayConsumptionPer100Km = 28f;
    public float motorwaySpeedThresholdKmh = 62f;
    public float optimalCruiseRpm = 1350f;
    public float unitPriceKES = 180f;

    [Header("Physics")]
    public float rigidbodyMassKg = 13200f;
    public float maxSteerAngle = 42f;
    public float steerSpeed = 0.1f;
    public float centerOfMassY = 1.6f;
    public float antiRollForce = 8000f;
    public float maxBrakeTorque = 12000f;
    public float retarderStrength = 3000f;
    public float dragCoefficient = 0.65f;
    public float frontalArea = 8.5f;
    public float airDensity = 1.225f;

    [Header("Powertrain")]
    public EngineSystem engineProfile;
    public TransmissionSystem transmissionProfile;

    public int StandingCapacity => Mathf.Max(0, standingCapacity);
    public int SeatedCapacity => Mathf.Max(0, seatedCapacity);
    public int PassengerCapacity => Mathf.Max(totalCapacity, seatedCapacity + standingCapacity);
    public bool IsElectric => energyType == BusEnergyType.Electric;
}
