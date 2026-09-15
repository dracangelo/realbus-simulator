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
    public bool useCapacityOverride = false;
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
    public bool doorsOpen = false;
    public float latestRequiredDwellSeconds = 2f;
    public bool stopRequestActive = false;
    public bool stopRequestAcknowledged = false;
    public int upcomingStopIndex = -1;
    public int missedRequestedStops = 0;

    [Header("Debug Logs")]
    public bool logBoardingSummary = true;
    public bool logEachPassengerBoarding = false;

    [Header("Animation Proxies")]
    public float boardingAnimationSpeed = 2.8f;
    public float alightingAnimationSpeed = 3.2f;
    public float standingRowSpacing = 0.55f;
    public float standingColumnSpacing = 0.75f;
    public Vector3 seatAreaOrigin = new Vector3(0f, 1.15f, 1.8f);
    public Vector3 standingAreaOrigin = new Vector3(0f, 1.05f, -0.8f);
    public Vector3 doorLocalPosition = new Vector3(0.9f, 1.05f, -2.4f);
    public Vector3 platformLocalPosition = new Vector3(1.7f, 0f, -2.8f);

    [Header("Door & Bell")]
    public AudioSource bellAudioSource;
    public AudioClip bellClip;
    public float bellVolume = 1f;

    private BusController busController;
    private readonly List<PassengerAgent> onboardPassengers = new List<PassengerAgent>();
    private readonly List<PassengerAgent> waitingPassengers = new List<PassengerAgent>();
    private readonly List<PassengerAgent> alightingPassengers = new List<PassengerAgent>();
    public IReadOnlyList<PassengerAgent> OnboardPassengers => onboardPassengers;
    public IReadOnlyList<PassengerAgent> AlightingPassengers => alightingPassengers;
    public IReadOnlyList<PassengerAgent> WaitingPassengers => waitingPassengers;
    public Vector3 WaitingPlatformWorld { get; private set; }
    int preparedStop = -1;
    float completedSatisfaction;
    int completedPassengerCount;
    private float lastSpeedKmh = 0f;
    private float longitudinalAcceleration = 0f;
    private float currentLongitudinalG = 0f;
    private bool hadElderlyBoarding = false;
    private float accessibilityResetTimer = -1f;
    private float lastAlightingTimeUnits = 0f;

    public void RestoreCheckpointState(int passengerCount, float satisfaction, int served, float fares,
        float income, float distanceKm, int currentStopIndex, int routeStopCount)
    {
        onboardPassengers.Clear(); waitingPassengers.Clear(); alightingPassengers.Clear();
        int restoredCount = Mathf.Clamp(passengerCount, 0, GetMaxCapacity());
        int destination = Mathf.Clamp(currentStopIndex + 1, 0, Mathf.Max(0, routeStopCount - 1));
        for (int i = 0; i < restoredCount; i++)
        {
            PassengerAgent passenger = new PassengerAgent(Mathf.Max(0, currentStopIndex - 1), destination, 60f, false);
            passenger.satisfaction = Mathf.Clamp01(satisfaction); passenger.AssignBusSlot(i); passenger.MarkBoarded();
            onboardPassengers.Add(passenger);
        }
        currentPassengers = restoredCount;
        averageSatisfaction = Mathf.Clamp01(satisfaction);
        totalPassengersServed = Mathf.Max(0, served);
        totalFareCollected = totalFaresCollected = Mathf.Max(0f, fares);
        sessionIncome = Mathf.Max(0f, income);
        totalDistanceKm = Mathf.Max(0f, distanceKm);
        preparedStop = -1; waitingAtCurrentStop = lastAlightingCount = lastBoardingCount = 0;
        doorsOpen = stopRequestActive = stopRequestAcknowledged = false;
    }

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

        UpdatePassengerAnimationState();
        UpdateAccessibilityReset();
        PrepareApproachingStop();
        UpdatePatience();
        RecalculateSatisfaction();
        HandleStopRequestAcknowledgeInput();
        HandleDoorOpenInput();
    }

    void FixedUpdate()
    {
        TrackBusAcceleration();
        UpdateOnboardPassengerDynamics();
    }

    void PrepareApproachingStop()
    {
        var mission = MissionManager.Instance;
        if (mission == null || !mission.routeActive || mission.currentRoute == null || mission.distanceToNextStop > 120f) return;
        int index = mission.currentStopIndex;
        if (index == preparedStop || index < 0 || index >= mission.currentRoute.stops.Length - 1) return;
        PrepareWaiting(mission.currentRoute.stops[index], index, mission.currentRoute.stops.Length);
    }

    void PrepareWaiting(BusStopData stop, int index, int count)
    {
        waitingPassengers.Clear();
        preparedStop = index;
        WaitingPlatformWorld = GPSManager.Instance != null ? GPSManager.Instance.GpsToWorld(stop.latitude, stop.longitude) : transform.position;
        SpawnWaitingPassengers(stop, index, count);
        waitingAtCurrentStop = waitingPassengers.Count;
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
        if (Time.fixedDeltaTime > 0f)
            longitudinalAcceleration = (speedMs - lastSpeedMs) / Time.fixedDeltaTime;
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
                passenger.UpdateStandingSway(-longitudinalAcceleration, Time.fixedDeltaTime);

            if (longitudinalAcceleration < -0.5f * 9.81f)
            {
                bool alreadyComplained = passenger.hasComplainedAboutBraking;
                passenger.RegisterHarshBrakingComplaint();
                if (!alreadyComplained)
                    Debug.Log("[Passenger] Audible complaint after harsh braking.");
            }

            float accelerationG = longitudinalAcceleration / 9.81f;
            if (accelerationG > 0.35f)
                passenger.RegisterHarshAcceleration(Time.fixedDeltaTime);
        }
    }

    void UpdatePassengerAnimationState()
    {
        for (int i = 0; i < onboardPassengers.Count; i++)
        {
            var passenger = onboardPassengers[i];
            if (passenger == null)
                continue;

            Vector3 target = ResolveBusSlotLocalPosition(passenger, i);
            passenger.TickBoardingAnimation(Time.deltaTime, target, boardingAnimationSpeed);
            passenger.TickAlightingAnimation(Time.deltaTime, doorLocalPosition, alightingAnimationSpeed);

            if (!passenger.isBoarding && !passenger.isAlighting)
                passenger.busLocalPosition = Vector3.Lerp(passenger.busLocalPosition, target, Time.deltaTime * 4f);
        }

        for (int i = alightingPassengers.Count - 1; i >= 0; i--)
        {
            var passenger = alightingPassengers[i];
            if (passenger == null)
            {
                alightingPassengers.RemoveAt(i);
                continue;
            }

            passenger.TickAlightingAnimation(Time.deltaTime, doorLocalPosition, alightingAnimationSpeed);
            if (!passenger.HasReachedExit(doorLocalPosition))
                continue;

            completedSatisfaction += passenger.satisfaction;
            completedPassengerCount++;
            passenger.MarkExited();
            alightingPassengers.RemoveAt(i);
        }
    }

    void UpdatePatience()
    {
        if (waitingPassengers.Count == 0) return;

        float dt = Time.deltaTime;
        for (int i = 0; i < waitingPassengers.Count; i++)
            waitingPassengers[i].TickPatience(dt);
    }

    void UpdateAccessibilityReset()
    {
        if (MissionManager.Instance != null && MissionManager.Instance.missionState == MissionState.AtStop) return;
        if (accessibilityResetTimer < 0f)
            return;

        accessibilityResetTimer -= Time.deltaTime;
        if (accessibilityResetTimer > 0f)
            return;

        accessibilityResetTimer = -1f;
        if (busController != null)
            busController.RequestKneelingSuspension(false);
    }

    public int GetMaxCapacity()
    {
        if (useCapacityOverride)
            return Mathf.Max(1, maxBusCapacity);
        if (passengerData != null && passengerData.totalCapacity > 0)
            return passengerData.totalCapacity;
        return maxBusCapacity;
    }

    public void ApplyBusSpec(BusSpec spec)
    {
        if (spec == null)
            return;

        useCapacityOverride = true;
        maxBusCapacity = Mathf.Max(1, spec.PassengerCapacity);
        doorOpenCapacityLimit = Mathf.Clamp(spec.doorOpenCapacityLimit, 1, maxBusCapacity);
        seatedCapacity = Mathf.Clamp(spec.SeatedCapacity, 0, maxBusCapacity);
    }

    public bool CanOpenDoors()
    {
        return currentPassengers < Mathf.Min(GetMaxCapacity(), Mathf.Max(1, doorOpenCapacityLimit));
    }

    public bool CanOpenDoorsForStop(int stopIndex, int totalStops)
    {
        if (CanOpenDoors()) return true;
        foreach (var passenger in onboardPassengers)
            if (passenger.destinationStopIndex == stopIndex || stopIndex == totalStops - 1) return true;
        return false;
    }

    public bool HasServiceAnimations
    {
        get
        {
            if (alightingPassengers.Count > 0) return true;
            foreach (var passenger in onboardPassengers) if (passenger.isBoarding) return true;
            return false;
        }
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
        int alighting = ProcessAlighting(stopIndex, totalStops > 0 && stopIndex == totalStops - 1);
        lastAlightingCount = alighting;
        currentPassengers = Mathf.Max(0, onboardPassengers.Count);

        // Service alighting first, then admit only the remaining capacity.
        doorsOpen = true; // Full occupancy must never prevent alighting.
        int boarding = 0;
        hadWheelchairBoarding = false;
        waitingAtCurrentStop = 0;

        if (doorsOpen && (totalStops <= 0 || stopIndex < totalStops - 1))
        {
            if (preparedStop != stopIndex || stopIndex < 0) PrepareWaiting(stop, stopIndex, totalStops);
            waitingAtCurrentStop = waitingPassengers.Count;
            PrepareAccessibilityBoarding();

            int canBoard = Mathf.Max(0, GetMaxCapacity() - currentPassengers);
            boarding = Mathf.Min(waitingAtCurrentStop, canBoard);
            ProcessBoarding(boarding, stopIndex, totalStops);
            waitingPassengers.RemoveRange(0, boarding);
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

    public void ResetForFreeDriveMode()
    {
        preparedStop = -1;
        completedSatisfaction = 0f; completedPassengerCount = 0;
        averageSatisfaction = 1f; missedRequestedStops = 0;
        lastSpeedKmh = busController != null ? busController.currentSpeedKmh : 0f;
        totalPassengersServed = 0;
        totalFareCollected = totalFaresCollected = sessionIncome = totalDistanceKm = 0f;
        onboardPassengers.Clear();
        waitingPassengers.Clear();
        alightingPassengers.Clear();
        currentPassengers = 0;
        waitingAtCurrentStop = 0;
        lastAlightingCount = 0;
        lastBoardingCount = 0;
        doorsOpen = false;
        latestRequiredDwellSeconds = 0f;
        stopRequestActive = false;
        stopRequestAcknowledged = false;
        upcomingStopIndex = -1;
        hadWheelchairBoarding = false;
        accessibilityResetTimer = -1f;
        lastAlightingTimeUnits = 0f;
    }

    int ProcessAlighting(int stopIndex, bool unloadAll = false)
    {
        if (!useAdvancedPassengerSimulation || onboardPassengers.Count == 0 || stopIndex < 0)
        {
            int alightingFallback = unloadAll ? currentPassengers : Mathf.Min(
                currentPassengers,
                Random.Range(
                    Mathf.RoundToInt(currentPassengers * 0.2f),
                    Mathf.RoundToInt(currentPassengers * 0.6f) + 1));
            for (int i = 0; i < alightingFallback && onboardPassengers.Count > 0; i++)
                onboardPassengers.RemoveAt(onboardPassengers.Count - 1);
            return alightingFallback;
        }

        int alighting = 0;
        lastAlightingTimeUnits = 0f;
        for (int i = onboardPassengers.Count - 1; i >= 0; i--)
        {
            var p = onboardPassengers[i];
            if (p == null) continue;
            if (unloadAll || p.destinationStopIndex == stopIndex)
            {
                p.ClearStopRequest();
                p.MarkAlightingToDoor();
                if (!alightingPassengers.Contains(p))
                    alightingPassengers.Add(p);
                onboardPassengers.RemoveAt(i);
                lastAlightingTimeUnits += p.GetAlightingTimeMultiplier();
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
            p.platformPosition = platformLocalPosition + new Vector3(Random.Range(-0.25f, 0.25f), 0f, Random.Range(-0.4f, 0.4f));
            p.isSeated = onboardPassengers.Count < seatedCapacity;
            p.AssignBusSlot(onboardPassengers.Count);
            p.busLocalPosition = p.platformPosition;
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
        if (completedPassengerCount == 0 && onboardPassengers.Count == 0 && waitingPassengers.Count == 0 && alightingPassengers.Count == 0)
        {
            averageSatisfaction = 1f;
            return;
        }

        float sum = completedSatisfaction;
        int count = completedPassengerCount;
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
        foreach (var p in alightingPassengers)
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

        float alightingTime = alightingTimePerPassenger * Mathf.Max(lastAlightingCount, lastAlightingTimeUnits);
        latestRequiredDwellSeconds = Mathf.Max(2f, boardingTime + alightingTime);

        if (hadWheelchairBoarding)
        {
            latestRequiredDwellSeconds += extraWheelchairDwellSeconds;
            if (busController != null)
                busController.RequestKneelingSuspension(true);
            ArmAccessibilityReset(latestRequiredDwellSeconds);
        }
        if (hadElderlyBoarding)
        {
            latestRequiredDwellSeconds = Mathf.Max(latestRequiredDwellSeconds, 10f);
            if (busController != null)
                busController.RequestKneelingSuspension(true);
            ArmAccessibilityReset(latestRequiredDwellSeconds);
        }
        else if (!hadWheelchairBoarding && busController != null)
        {
            accessibilityResetTimer = -1f;
            busController.RequestKneelingSuspension(false);
        }

        hadElderlyBoarding = false;
    }

    void ArmAccessibilityReset(float seconds)
    {
        accessibilityResetTimer = Mathf.Max(accessibilityResetTimer, Mathf.Max(0.1f, seconds));
    }

    void PrepareAccessibilityBoarding()
    {
        bool needsKneel = false;
        for (int i = 0; i < waitingPassengers.Count; i++)
        {
            var passenger = waitingPassengers[i];
            if (passenger == null)
                continue;

            if (passenger.isWheelchairPassenger || passenger.RequiresKneelingForBoarding())
            {
                needsKneel = true;
                break;
            }
        }

        if (busController != null)
            busController.RequestKneelingSuspension(needsKneel);
    }

    Vector3 ResolveBusSlotLocalPosition(PassengerAgent passenger, int onboardIndex)
    {
        if (passenger != null && passenger.isSeated)
        {
            int seatsPerRow = 4;
            int row = Mathf.Max(0, onboardIndex / seatsPerRow);
            int column = onboardIndex % seatsPerRow;
            float side = column < 2 ? -0.5f : 0.5f;
            float lane = (column % 2 == 0) ? -0.35f : 0.35f;
            return seatAreaOrigin + new Vector3((side + lane) * standingColumnSpacing, 0f, -row * standingRowSpacing);
        }

        int standingIndex = Mathf.Max(0, onboardIndex - seatedCapacity);
        int standingColumns = 3;
        int standingRow = standingIndex / standingColumns;
        int standingColumn = standingIndex % standingColumns;
        float lateral = (standingColumn - 1) * standingColumnSpacing;
        return standingAreaOrigin + new Vector3(lateral, 0f, -standingRow * standingRowSpacing);
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
        RingBell();
        Debug.Log("[Passenger] Stop request bell pressed.");
    }

    void ClearStopRequestsAtStop(int stopIndex)
    {
        for (int i = 0; i < onboardPassengers.Count; i++)
        {
            var passenger = onboardPassengers[i];
            if (passenger == null)
                continue;

            if (passenger.destinationStopIndex == stopIndex)
                passenger.ClearStopRequest();
        }

        bool anyOutstandingRequest = false;
        for (int i = 0; i < onboardPassengers.Count; i++)
        {
            var passenger = onboardPassengers[i];
            if (passenger != null && passenger.stopRequested)
            {
                anyOutstandingRequest = true;
                break;
            }
        }

        stopRequestActive = anyOutstandingRequest;
        if (!anyOutstandingRequest || currentPassengers == 0)
            stopRequestAcknowledged = false;
    }

    void HandleStopRequestAcknowledgeInput()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null && keyboard.bKey.wasPressedThisFrame)
            AcknowledgeStopRequest();
    }

    void HandleDoorOpenInput()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null || !keyboard.oKey.wasPressedThisFrame)
            return;

        RingBell();

        if (MissionManager.Instance != null && MissionManager.Instance.routeActive)
            MissionManager.Instance.TryOpenDoorsAtCurrentStop();
    }

    public void RequestDoors()
    {
        RingBell();
        MissionManager.Instance?.TryOpenDoorsAtCurrentStop();
    }

    void RingBell()
    {
        if (bellAudioSource != null && bellClip != null)
            bellAudioSource.PlayOneShot(bellClip, bellVolume);
    }
}
