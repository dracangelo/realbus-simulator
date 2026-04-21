using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

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
    [Range(0f, 1f)] public float averageSatisfaction = 1f;

    [Header("Capacity Policy")]
    public int maxBusCapacity = 80;
    public int doorOpenCapacityLimit = 80;

    [Header("Economy")]
    public float fuelCostPerKm = 12f; // KES
    public float totalDistanceKm = 0f;

    [Header("Passenger Simulation")]
    public PassengerSpawner passengerSpawner;
    public bool useAdvancedPassengerSimulation = true;
    [Range(0f, 1f)] public float wheelchairChance = 0.05f;
    public float extraWheelchairDwellSeconds = 10f;
    public int seatedCapacity = 40;

    [Header("Realtime Stats")]
    public int waitingAtCurrentStop = 0;
    public int lastAlightingCount = 0;
    public int lastBoardingCount = 0;
    public bool hadWheelchairBoarding = false;
    public bool doorsOpen = true;
    public float latestRequiredDwellSeconds = 2f;
    public bool stopRequestActive = false;
    public bool stopRequestAcknowledged = false;
    public int upcomingStopIndex = -1;
    public int missedRequestedStops = 0;

    [Header("Debug Logs")]
    public bool logBoardingSummary = true;
    public bool logEachPassengerBoarding = false;

    private BusController busController;
    private readonly List<PassengerAgent> onboardPassengers = new List<PassengerAgent>();
    private readonly List<PassengerAgent> waitingPassengers = new List<PassengerAgent>();
    private float lastSpeedKmh = 0f;
    private float longitudinalAcceleration = 0f;
    private float currentLongitudinalG = 0f;
    private bool hadElderlyBoarding = false;

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
        TrackBusAcceleration();
        UpdateOnboardPassengerDynamics();
        UpdatePatience();
        HandleStopRequestAcknowledgeInput();
    }

    void TrackDistance()
    {
        if (busController == null) return;
        float speedKmh = busController.currentSpeedKmh;
        totalDistanceKm += (speedKmh / 3600f) * Time.deltaTime;
    }

    void TrackBusAcceleration()
    {
        if (busController == null) return;
        float speedMs = busController.currentSpeedKmh / 3.6f;
        float lastSpeedMs = lastSpeedKmh / 3.6f;
        if (Time.deltaTime > 0f)
            longitudinalAcceleration = (speedMs - lastSpeedMs) / Time.deltaTime;
        currentLongitudinalG = Mathf.Abs(longitudinalAcceleration) / 9.81f;
        lastSpeedKmh = busController.currentSpeedKmh;
    }

    void UpdateOnboardPassengerDynamics()
    {
        if (onboardPassengers.Count == 0) return;

        // Standing passengers sway opposite bus acceleration/deceleration.
        foreach (var passenger in onboardPassengers)
        {
            if (passenger == null) continue;
            if (!passenger.isSeated)
                passenger.UpdateStandingSway(-longitudinalAcceleration);

            if (currentLongitudinalG > 0.5f)
            {
                bool alreadyComplained = passenger.hasComplainedAboutBraking;
                passenger.RegisterHarshBrakingComplaint();
                if (!alreadyComplained)
                    Debug.Log("[Passenger] Audible complaint after harsh braking.");
            }
        }
    }

    void UpdatePatience()
    {
        if (waitingPassengers.Count == 0) return;

        float dt = Time.deltaTime;
        for (int i = 0; i < waitingPassengers.Count; i++)
            waitingPassengers[i].TickPatience(dt);
    }

    public int GetMaxCapacity()
    {
        if (passengerData != null && passengerData.totalCapacity > 0)
            return passengerData.totalCapacity;
        return maxBusCapacity;
    }

    public bool CanOpenDoors()
    {
        return currentPassengers < Mathf.Max(1, doorOpenCapacityLimit);
    }

    public float GetRequiredDwellTimeSeconds()
    {
        return latestRequiredDwellSeconds;
    }

    /// <summary>
    /// Called by MissionManager when bus arrives at a stop.
    /// </summary>
    public void HandleStopArrival(BusStopData stop, float fare)
    {
        HandleStopArrival(stop, fare, -1, -1);
    }

    public void HandleStopArrival(BusStopData stop, float fare, int stopIndex, int totalStops)
    {
        if (stop == null) return;
        if (stopRequestActive && !stopRequestAcknowledged)
        {
            missedRequestedStops++;
            ScoreTracker.Instance?.ApplyPassengerServicePenalty(6f, 2f);
            for (int i = 0; i < onboardPassengers.Count; i++)
            {
                if (onboardPassengers[i] != null && onboardPassengers[i].stopRequested)
                    onboardPassengers[i].satisfaction = Mathf.Clamp01(onboardPassengers[i].satisfaction - 0.12f);
            }
        }

        // Alighting first.
        int alighting = ProcessAlighting(stopIndex);
        lastAlightingCount = alighting;
        currentPassengers = Mathf.Max(0, onboardPassengers.Count);

        // Capacity policy: refuse door open at full cap.
        doorsOpen = CanOpenDoors();
        int boarding = 0;
        hadWheelchairBoarding = false;
        waitingAtCurrentStop = 0;

        if (doorsOpen)
        {
            waitingPassengers.Clear();
            SpawnWaitingPassengers(stop, stopIndex, totalStops);
            waitingAtCurrentStop = waitingPassengers.Count;

            int canBoard = Mathf.Max(0, GetMaxCapacity() - currentPassengers);
            boarding = Mathf.Min(waitingAtCurrentStop, canBoard);
            ProcessBoarding(boarding, stopIndex, totalStops);
        }

        lastBoardingCount = boarding;
        currentPassengers = onboardPassengers.Count;
        ClearStopRequestsAtStop(stopIndex);

        // Collect fares from boarding passengers.
        float fareCollected = boarding * fare;
        totalFaresCollected += fareCollected;
        sessionIncome += fareCollected;
        totalPassengersServed += boarding;
        totalFareCollected += fareCollected;

        RecalculateSatisfaction();
        RecalculateDwellTime();

        if (logBoardingSummary)
        {
            int leftBehind = Mathf.Max(0, waitingAtCurrentStop - boarding);
            string stopLabel = stopIndex >= 0 && totalStops > 0
                ? $"Stop {stopIndex + 1}/{totalStops}"
                : "Stop";
            Debug.Log(
                $"[PassengerBoarding] {stopLabel}: {stop.stopName} | Boarded={boarding}, Alighted={alighting}, LeftBehind={leftBehind}, " +
                $"Onboard={currentPassengers}/{GetMaxCapacity()}, Fare+={fareCollected:F0}, Dwell={latestRequiredDwellSeconds:F1}s, WheelchairBoarding={hadWheelchairBoarding}");
        }
    }

    public float GetProfit()
    {
        float fuelCost = totalDistanceKm * fuelCostPerKm;
        return sessionIncome - fuelCost;
    }

    public void SetUpcomingStopIndex(int stopIndex)
    {
        upcomingStopIndex = stopIndex;
        for (int i = 0; i < onboardPassengers.Count; i++)
        {
            var passenger = onboardPassengers[i];
            if (passenger == null)
                continue;

            if (passenger.destinationStopIndex == stopIndex)
            {
                passenger.PrepareForUpcomingStop();
                if (!passenger.stopRequested)
                    RegisterStopRequest(passenger);
            }
        }
    }

    public void AcknowledgeStopRequest()
    {
        if (!stopRequestActive)
            return;

        stopRequestAcknowledged = true;
    }

    int ProcessAlighting(int stopIndex)
    {
        if (!useAdvancedPassengerSimulation || onboardPassengers.Count == 0 || stopIndex < 0)
        {
            int alightingFallback = Mathf.Min(
                currentPassengers,
                Random.Range(
                    Mathf.RoundToInt(currentPassengers * 0.2f),
                    Mathf.RoundToInt(currentPassengers * 0.6f) + 1));
            for (int i = 0; i < alightingFallback && onboardPassengers.Count > 0; i++)
                onboardPassengers.RemoveAt(onboardPassengers.Count - 1);
            return alightingFallback;
        }

        int alighting = 0;
        for (int i = onboardPassengers.Count - 1; i >= 0; i--)
        {
            var p = onboardPassengers[i];
            if (p == null) continue;
            if (p.destinationStopIndex == stopIndex)
            {
                p.MarkAlightingToDoor();
                onboardPassengers.RemoveAt(i);
                alighting++;
            }
        }
        return alighting;
    }

    void SpawnWaitingPassengers(BusStopData stop, int stopIndex, int totalStops)
    {
        int spawnCount;
        if (passengerSpawner != null)
            spawnCount = passengerSpawner.GetSpawnCountForStop(stop);
        else
            spawnCount = Random.Range(3, 15);

        for (int i = 0; i < spawnCount; i++)
        {
            int destination = ResolveDestinationStop(stopIndex, totalStops);
            bool wheelchair = Random.value < wheelchairChance;

            var p = new PassengerAgent(
                originStop: Mathf.Max(0, stopIndex),
                destinationStop: destination,
                initialPatience: Random.Range(45f, 120f),
                wheelchair: wheelchair,
                passengerArchetype: ResolveArchetype()
            );
            ApplyArchetypeTuning(p);
            waitingPassengers.Add(p);

            if (p.archetype == PassengerArchetype.Student && Random.value < 0.45f)
            {
                int extraStudents = Random.Range(1, 3);
                for (int groupIndex = 0; groupIndex < extraStudents; groupIndex++)
                {
                    var student = new PassengerAgent(
                        Mathf.Max(0, stopIndex),
                        destination,
                        Random.Range(45f, 110f),
                        false,
                        PassengerArchetype.Student);
                    ApplyArchetypeTuning(student);
                    waitingPassengers.Add(student);
                }
            }
        }
    }

    void ProcessBoarding(int boardingCount, int stopIndex, int totalStops)
    {
        for (int i = 0; i < boardingCount && i < waitingPassengers.Count; i++)
        {
            var p = waitingPassengers[i];
            if (p == null) continue;

            p.BeginBoarding();
            p.isSeated = onboardPassengers.Count < seatedCapacity;
            p.AssignBusSlot(onboardPassengers.Count);
            p.MarkBoarded();
            onboardPassengers.Add(p);

            if (p.isWheelchairPassenger)
                hadWheelchairBoarding = true;
            if (p.archetype == PassengerArchetype.Elderly)
            {
                hadElderlyBoarding = true;
                if (busController != null && !busController.kneelingSuspensionActive)
                    p.satisfaction = Mathf.Clamp01(p.satisfaction - 0.08f);
            }
            if (p.archetype == PassengerArchetype.Tourist && logBoardingSummary)
                Debug.Log($"[PassengerBoarding] Tourist asking for stop names near stop index {p.destinationStopIndex}.");

            if (logEachPassengerBoarding)
            {
                Debug.Log(
                    $"[PassengerBoarding] Passenger boarded | DestStopIndex={p.destinationStopIndex}, " +
                    $"Wheelchair={p.isWheelchairPassenger}, Seated={p.isSeated}, Slot={p.assignedSlotIndex}");
            }
        }
    }

    int ResolveDestinationStop(int stopIndex, int totalStops)
    {
        if (stopIndex < 0 || totalStops <= 1)
            return -1;
        int from = Mathf.Clamp(stopIndex + 1, 1, totalStops - 1);
        int to = Mathf.Max(from + 1, totalStops);
        return Random.Range(from, to);
    }

    void RecalculateSatisfaction()
    {
        if (onboardPassengers.Count == 0 && waitingPassengers.Count == 0)
        {
            averageSatisfaction = 1f;
            return;
        }

        float sum = 0f;
        int count = 0;
        foreach (var p in onboardPassengers)
        {
            if (p == null) continue;
            sum += p.satisfaction;
            count++;
        }
        foreach (var p in waitingPassengers)
        {
            if (p == null) continue;
            sum += p.satisfaction;
            count++;
        }

        averageSatisfaction = count > 0 ? Mathf.Clamp01(sum / count) : 1f;
    }

    void RecalculateDwellTime()
    {
        float boardingTimePerPassenger = passengerData != null ? passengerData.boardingTimePerPassenger : 1.5f;
        float alightingTimePerPassenger = passengerData != null ? passengerData.alightingTimePerPassenger : 1.2f;

        float boardingTime = 0f;
        for (int i = Mathf.Max(0, onboardPassengers.Count - lastBoardingCount); i < onboardPassengers.Count; i++)
        {
            var passenger = onboardPassengers[i];
            if (passenger == null) continue;
            boardingTime += boardingTimePerPassenger * passenger.GetBoardingTimeMultiplier();
        }

        float alightingTime = alightingTimePerPassenger * lastAlightingCount;
        latestRequiredDwellSeconds = Mathf.Max(2f, boardingTime + alightingTime);

        if (hadWheelchairBoarding)
        {
            latestRequiredDwellSeconds += extraWheelchairDwellSeconds;
            if (busController != null)
                busController.RequestKneelingSuspension(true);
        }
        if (hadElderlyBoarding)
        {
            latestRequiredDwellSeconds = Mathf.Max(latestRequiredDwellSeconds, 10f);
            if (busController != null)
                busController.RequestKneelingSuspension(true);
        }
        else if (busController != null)
        {
            busController.RequestKneelingSuspension(false);
        }

        hadElderlyBoarding = false;
    }

    PassengerArchetype ResolveArchetype()
    {
        float roll = Random.value;
        if (roll < 0.38f) return PassengerArchetype.Commuter;
        if (roll < 0.56f) return PassengerArchetype.Student;
        if (roll < 0.78f) return PassengerArchetype.Tourist;
        return PassengerArchetype.Elderly;
    }

    void ApplyArchetypeTuning(PassengerAgent passenger)
    {
        if (passenger == null)
            return;

        switch (passenger.archetype)
        {
            case PassengerArchetype.Commuter:
                passenger.patienceSeconds *= 0.75f;
                break;
            case PassengerArchetype.Tourist:
                passenger.patienceSeconds *= 1.1f;
                break;
            case PassengerArchetype.Elderly:
                passenger.patienceSeconds *= 1.25f;
                break;
        }
    }

    void RegisterStopRequest(PassengerAgent passenger)
    {
        if (passenger == null)
            return;

        passenger.PressStopRequest();
        stopRequestActive = true;
        stopRequestAcknowledged = false;
        Debug.Log("[Passenger] Stop request bell pressed.");
    }

    void ClearStopRequestsAtStop(int stopIndex)
    {
        bool fulfilled = false;
        for (int i = 0; i < onboardPassengers.Count; i++)
        {
            var passenger = onboardPassengers[i];
            if (passenger == null)
                continue;

            if (passenger.destinationStopIndex == stopIndex)
            {
                passenger.ClearStopRequest();
                fulfilled = true;
            }
        }

        if (fulfilled || currentPassengers == 0)
        {
            stopRequestActive = false;
            stopRequestAcknowledged = false;
        }
    }

    void HandleStopRequestAcknowledgeInput()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null && keyboard.bKey.wasPressedThisFrame)
            AcknowledgeStopRequest();
    }
}
