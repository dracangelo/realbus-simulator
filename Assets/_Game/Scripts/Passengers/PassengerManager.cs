using UnityEngine;

public class PassengerManager : MonoBehaviour
{
    public static PassengerManager Instance { get; private set; }

    [Header("Totals")]
    public int totalPassengersServed = 0;
    public float totalFareCollected = 0f;
    
    [Header("Data")]
    public PassengerData passengerData;

    [Header("State")]
    public int currentPassengers = 0;
    public float totalFaresCollected = 0f;
    public float sessionIncome = 0f;

    [Header("Economy")]
    public float fuelCostPerKm = 12f; // KES
    public float totalDistanceKm = 0f;

    private BusController busController;
    //private float lastSpeedKmh = 0f;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        busController = FindFirstObjectByType<BusController>();
    }

    void Update()
    {
        TrackDistance();
    }

    void TrackDistance()
    {
        if (busController == null) return;
        float speedKmh = busController.currentSpeedKmh;
        totalDistanceKm += (speedKmh / 3600f) * Time.deltaTime;
    }

    /// <summary>
    /// Called by MissionManager when bus arrives at a stop.
    /// </summary>
    public void HandleStopArrival(BusStopData stop, float fare)
    {
        // Passengers alighting (random 20-60% of current)
        int alighting = Mathf.Min(
            currentPassengers,
            Random.Range(
                Mathf.RoundToInt(currentPassengers * 0.2f),
                Mathf.RoundToInt(currentPassengers * 0.6f) + 1));

        currentPassengers -= alighting;

        // Passengers boarding (random, up to capacity)
        int waiting = Random.Range(3, 15);
        int canBoard = passengerData.totalCapacity - currentPassengers;
        int boarding = Mathf.Min(waiting, canBoard);
        currentPassengers += boarding;

        // Collect fares from boarding passengers
        float fareCollected = boarding * fare;
        totalFaresCollected += fareCollected;
        sessionIncome += fareCollected;

        totalPassengersServed += boarding;
        totalFareCollected += fareCollected;

        Debug.Log($"Stop: {stop.stopName} | -{alighting} +{boarding} | Passengers: {currentPassengers}/{passengerData.totalCapacity} | Fare: KES {fareCollected} | Total: KES {totalFaresCollected:F0}");
    }

    public float GetProfit()
    {
        float fuelCost = totalDistanceKm * fuelCostPerKm;
        return sessionIncome - fuelCost;
    }
}