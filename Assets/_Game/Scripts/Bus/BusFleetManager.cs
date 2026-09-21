using System;
using System.Collections.Generic;
using UnityEngine;

public class BusFleetManager : MonoBehaviour
{
    const string SelectedBusPlayerPrefsKey = "fleet.selected_bus_id";
    const string OwnedBusPlayerPrefsKey = "fleet.owned_bus_ids";
    const string ModelBackedDefaultMigrationKey = "fleet.model_backed_default.v2";

    public static BusFleetManager Instance { get; private set; }

    public event Action FleetChanged;
    public event Action<BusSpec> SelectedBusChanged;

    readonly List<BusSpec> busSpecs = new List<BusSpec>();
    readonly HashSet<string> ownedBusIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

    bool initialized;
    string selectedBusId;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap()
    {
        EnsureExists();
    }

    public static BusFleetManager EnsureExists()
    {
        if (Instance != null)
            return Instance;

        var existing = FindFirstObjectByType<BusFleetManager>();
        if (existing != null)
            return existing;

        var root = new GameObject("BusFleetManager");
        return root.AddComponent<BusFleetManager>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        EnsureInitialized();
    }

    public void EnsureInitialized()
    {
        if (initialized)
            return;

        initialized = true;
        LoadSpecs();
        LoadOwnedState();
        SyncUnlockOwnership();
        EnsureSelectedBusIsValid();
        MigratePlaceholderSelectionToModelBackedBus();
        ApplySelectedBusToGameState();
    }

    public IReadOnlyList<BusSpec> GetAllBusSpecs()
    {
        EnsureInitialized();
        return busSpecs;
    }

    public BusSpec GetSelectedBusSpec()
    {
        EnsureInitialized();
        SyncUnlockOwnership();
        return GetBusSpecById(selectedBusId) ?? GetFirstOwnedBus();
    }

    public BusSpec GetSelectedDrivableBusSpec()
    {
        EnsureInitialized();
        SyncUnlockOwnership();
        BusSpec selected = GetBusSpecById(selectedBusId);
        if (selected != null && selected.drivablePrefab != null)
            return selected;

        BusSpec fallback = GetFirstOwnedBus();
        if (fallback != null && fallback.drivablePrefab != null)
        {
            selectedBusId = fallback.busId;
            SaveOwnedState();
            ApplySelectedBusToGameState();
            SelectedBusChanged?.Invoke(fallback);
            Debug.LogWarning($"BusFleetManager: Replaced placeholder fleet selection with model-backed bus '{fallback.displayName}'.");
            return fallback;
        }

        return selected;
    }

    public BusSpec GetBusSpecById(string busId)
    {
        EnsureInitialized();
        if (string.IsNullOrWhiteSpace(busId))
            return null;

        for (int i = 0; i < busSpecs.Count; i++)
        {
            var spec = busSpecs[i];
            if (spec != null && string.Equals(spec.busId, busId, StringComparison.OrdinalIgnoreCase))
                return spec;
        }

        return null;
    }

    public bool IsBusUnlocked(BusSpec spec)
    {
        if (spec == null)
            return false;

        int currentRank = XPSystem.Instance != null
            ? XPSystem.Instance.GetProgressSnapshot().currentRank
            : 1;

        return currentRank >= Mathf.Max(1, spec.requiredRank);
    }

    public bool IsBusOwned(BusSpec spec)
    {
        if (spec == null)
            return false;

        EnsureInitialized();
        SyncUnlockOwnership();
        return ownedBusIds.Contains(spec.busId);
    }

    public bool IsSelected(BusSpec spec)
    {
        return spec != null && string.Equals(selectedBusId, spec.busId, StringComparison.OrdinalIgnoreCase);
    }

    public string[] GetOwnedBusIds()
    {
        EnsureInitialized();
        SyncUnlockOwnership();

        string[] ids = new string[ownedBusIds.Count];
        ownedBusIds.CopyTo(ids);
        Array.Sort(ids, StringComparer.OrdinalIgnoreCase);
        return ids;
    }

    public string GetSelectedBusId()
    {
        EnsureInitialized();
        SyncUnlockOwnership();
        return selectedBusId ?? string.Empty;
    }

    public bool TrySelectBus(BusSpec spec)
    {
        EnsureInitialized();
        SyncUnlockOwnership();
        if (spec == null || !IsBusOwned(spec))
            return false;
        if (string.Equals(selectedBusId, spec.busId, StringComparison.OrdinalIgnoreCase))
            return true;

        selectedBusId = spec.busId;
        SaveOwnedState();
        ApplySelectedBusToGameState();
        FleetChanged?.Invoke();
        SelectedBusChanged?.Invoke(spec);
        return true;
    }

    /// <summary>Registers a vehicle supplied by an optional runtime content pack.</summary>
    public bool RegisterRuntimeBusSpec(BusSpec spec, bool grantOwnership = true)
    {
        EnsureInitialized();
        if (spec == null || string.IsNullOrWhiteSpace(spec.busId) || spec.drivablePrefab == null)
            return false;

        int existingIndex = GetSpecIndex(spec.busId);
        if (existingIndex >= 0)
            busSpecs[existingIndex] = spec;
        else
            busSpecs.Add(spec);

        busSpecs.Sort(CompareBusSpecs);
        if (grantOwnership)
            ownedBusIds.Add(spec.busId);
        EnsureSelectedBusIsValid();
        SaveOwnedState();
        FleetChanged?.Invoke();
        return true;
    }

    public void ApplySaveState(IEnumerable<string> ownedIds, string selectedId)
    {
        EnsureInitialized();

        ownedBusIds.Clear();
        if (ownedIds != null)
        {
            foreach (string ownedId in ownedIds)
            {
                if (!string.IsNullOrWhiteSpace(ownedId))
                    ownedBusIds.Add(ownedId);
            }
        }

        selectedBusId = selectedId ?? string.Empty;
        SyncUnlockOwnership();
        EnsureSelectedBusIsValid();
        ApplySelectedBusToGameState();
        FleetChanged?.Invoke();
        SelectedBusChanged?.Invoke(GetSelectedBusSpec());
    }

    public int GetRequiredRank(BusSpec spec)
    {
        return spec != null ? Mathf.Max(1, spec.requiredRank) : int.MaxValue;
    }

    public string GetLockedReason(BusSpec spec)
    {
        if (spec == null)
            return string.Empty;

        if (IsBusUnlocked(spec))
            return "Owned";

        return $"Unlocks at rank {GetRequiredRank(spec)}";
    }

    void LoadSpecs()
    {
        busSpecs.Clear();

        // Keep the balanced built-in fleet even when artists add model-backed specs.
        // Older behavior replaced the entire fleet as soon as one Resources asset existed.
        busSpecs.AddRange(CreateDefaultSpecs());

        var resourceSpecs = Resources.LoadAll<BusSpec>("BusSpecs");
        if (resourceSpecs != null && resourceSpecs.Length > 0)
        {
            for (int i = 0; i < resourceSpecs.Length; i++)
            {
                BusSpec candidate = resourceSpecs[i];
                if (candidate == null || GetSpecIndex(candidate.busId) >= 0)
                    continue;
                busSpecs.Add(candidate);
            }
        }

        busSpecs.Sort(CompareBusSpecs);
    }

    int GetSpecIndex(string busId)
    {
        if (string.IsNullOrWhiteSpace(busId)) return -1;
        for (int i = 0; i < busSpecs.Count; i++)
            if (busSpecs[i] != null && string.Equals(busSpecs[i].busId, busId, StringComparison.OrdinalIgnoreCase))
                return i;
        return -1;
    }

    void LoadOwnedState()
    {
        ownedBusIds.Clear();
        selectedBusId = PlayerPrefs.GetString(SelectedBusPlayerPrefsKey, string.Empty);

        string rawOwned = PlayerPrefs.GetString(OwnedBusPlayerPrefsKey, string.Empty);
        if (string.IsNullOrWhiteSpace(rawOwned))
            return;

        string[] ids = rawOwned.Split('|');
        for (int i = 0; i < ids.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(ids[i]))
                ownedBusIds.Add(ids[i]);
        }
    }

    void SaveOwnedState()
    {
        string[] ids = new string[ownedBusIds.Count];
        ownedBusIds.CopyTo(ids);
        Array.Sort(ids, StringComparer.OrdinalIgnoreCase);

        PlayerPrefs.SetString(OwnedBusPlayerPrefsKey, string.Join("|", ids));
        PlayerPrefs.SetString(SelectedBusPlayerPrefsKey, selectedBusId ?? string.Empty);
        PlayerPrefs.Save();
    }

    void SyncUnlockOwnership()
    {
        bool changed = false;
        for (int i = 0; i < busSpecs.Count; i++)
        {
            var spec = busSpecs[i];
            if (spec == null)
                continue;

            bool shouldOwn = i == 0 || IsBusUnlocked(spec);
            if (shouldOwn && ownedBusIds.Add(spec.busId))
                changed = true;
        }

        if (!ownedBusIds.Contains(selectedBusId))
        {
            selectedBusId = GetFirstOwnedBus()?.busId ?? string.Empty;
            changed = true;
        }

        if (changed)
            SaveOwnedState();
    }

    void EnsureSelectedBusIsValid()
    {
        if (!string.IsNullOrWhiteSpace(selectedBusId) && GetBusSpecById(selectedBusId) != null && ownedBusIds.Contains(selectedBusId))
            return;

        selectedBusId = GetFirstOwnedBus()?.busId ?? string.Empty;
        SaveOwnedState();
    }

    void MigratePlaceholderSelectionToModelBackedBus()
    {
        if (PlayerPrefs.GetInt(ModelBackedDefaultMigrationKey, 0) != 0)
            return;

        BusSpec current = GetBusSpecById(selectedBusId);
        BusSpec modelBacked = GetFirstOwnedBus();
        if (modelBacked == null || modelBacked.drivablePrefab == null)
            return; // Model imports have not finished yet; retry on the next launch.

        if (current == null || current.drivablePrefab == null)
        {
            selectedBusId = modelBacked.busId;
            SaveOwnedState();
        }

        PlayerPrefs.SetInt(ModelBackedDefaultMigrationKey, 1);
        PlayerPrefs.Save();
    }

    BusSpec GetFirstOwnedBus()
    {
        // Prefer a complete model-backed vehicle for new saves and invalid legacy
        // selections so gameplay does not silently fall back to the grey test bus.
        for (int i = 0; i < busSpecs.Count; i++)
        {
            var spec = busSpecs[i];
            if (spec != null && spec.drivablePrefab != null && ownedBusIds.Contains(spec.busId))
                return spec;
        }

        for (int i = 0; i < busSpecs.Count; i++)
        {
            var spec = busSpecs[i];
            if (spec != null && ownedBusIds.Contains(spec.busId))
                return spec;
        }

        return busSpecs.Count > 0 ? busSpecs[0] : null;
    }

    void ApplySelectedBusToGameState()
    {
        var spec = GetSelectedBusSpec();
        if (spec == null || GameState.Instance == null)
            return;

        GameState.Instance.vehicleState.ApplyBusSpec(spec, preserveEnergyPercent: true);
    }

    static int CompareBusSpecs(BusSpec a, BusSpec b)
    {
        if (a == null && b == null) return 0;
        if (a == null) return 1;
        if (b == null) return -1;

        int rankCompare = a.requiredRank.CompareTo(b.requiredRank);
        if (rankCompare != 0)
            return rankCompare;

        return string.Compare(a.displayName, b.displayName, StringComparison.OrdinalIgnoreCase);
    }

    static List<BusSpec> CreateDefaultSpecs()
    {
        return new List<BusSpec>
        {
            CreateSpec(
                "fleet.standard_single_decker",
                "Kifaru Standard 12",
                "Standard Single-Decker",
                "Balanced diesel workhorse for all-day city duty.",
                requiredRank: 1,
                energyType: BusEnergyType.Diesel,
                energyCapacityUnits: 300f,
                energyUnitLabel: "L",
                cityConsumption: 35f,
                motorwayConsumption: 28f,
                speedThreshold: 62f,
                optimalCruiseRpm: 1350f,
                unitPriceKES: 180f,
                seatedCapacity: 36,
                standingCapacity: 34,
                totalCapacity: 70,
                doorOpenCapacityLimit: 70,
                bodyLengthMeters: 12f,
                rigidbodyMassKg: 13200f,
                maxSteerAngle: 42f,
                steerSpeed: 0.1f,
                centerOfMassY: 1.6f,
                antiRollForce: 8000f,
                maxBrakeTorque: 12000f,
                retarderStrength: 3000f,
                dragCoefficient: 0.65f,
                frontalArea: 8.5f,
                airDensity: 1.225f,
                engine: CreateDieselEngine(1650f, 1150f, 2500f),
                transmission: CreateTransmission(new[] { 3.5f, 2.0f, 1.35f, 1.0f, 0.82f, 0.68f }, 4.1f, new[] { 12f, 25f, 40f, 58f, 75f, 95f })),
            CreateSpec(
                "fleet.minibus",
                "Jambo Shuttle 8.5",
                "Minibus",
                "Short-wheelbase city shuttle with tight steering and low running cost.",
                requiredRank: 2,
                energyType: BusEnergyType.Diesel,
                energyCapacityUnits: 160f,
                energyUnitLabel: "L",
                cityConsumption: 22f,
                motorwayConsumption: 18f,
                speedThreshold: 68f,
                optimalCruiseRpm: 1450f,
                unitPriceKES: 175f,
                seatedCapacity: 18,
                standingCapacity: 10,
                totalCapacity: 28,
                doorOpenCapacityLimit: 28,
                bodyLengthMeters: 8.5f,
                rigidbodyMassKg: 7600f,
                maxSteerAngle: 48f,
                steerSpeed: 0.14f,
                centerOfMassY: 1.3f,
                antiRollForce: 5400f,
                maxBrakeTorque: 9200f,
                retarderStrength: 1800f,
                dragCoefficient: 0.58f,
                frontalArea: 6.2f,
                airDensity: 1.225f,
                engine: CreateDieselEngine(980f, 1250f, 2800f),
                transmission: CreateTransmission(new[] { 3.8f, 2.3f, 1.55f, 1.12f, 0.84f }, 4.4f, new[] { 14f, 28f, 46f, 66f, 92f })),
            CreateSpec(
                "fleet.articulated_18m",
                "Safari Link 18",
                "Articulated 18m",
                "High-capacity articulated diesel built for heavy trunk corridors.",
                requiredRank: 4,
                energyType: BusEnergyType.Diesel,
                energyCapacityUnits: 420f,
                energyUnitLabel: "L",
                cityConsumption: 46f,
                motorwayConsumption: 38f,
                speedThreshold: 60f,
                optimalCruiseRpm: 1280f,
                unitPriceKES: 192f,
                seatedCapacity: 52,
                standingCapacity: 68,
                totalCapacity: 120,
                doorOpenCapacityLimit: 120,
                bodyLengthMeters: 18f,
                rigidbodyMassKg: 20800f,
                maxSteerAngle: 36f,
                steerSpeed: 0.085f,
                centerOfMassY: 1.75f,
                antiRollForce: 10400f,
                maxBrakeTorque: 16800f,
                retarderStrength: 3600f,
                dragCoefficient: 0.72f,
                frontalArea: 9.8f,
                airDensity: 1.225f,
                engine: CreateDieselEngine(2100f, 1100f, 2300f),
                transmission: CreateTransmission(new[] { 3.7f, 2.2f, 1.52f, 1.08f, 0.82f, 0.64f }, 4.35f, new[] { 10f, 22f, 36f, 52f, 70f, 88f })),
            CreateSpec(
                "fleet.electric",
                "Volta E-City",
                "Electric",
                "Quiet battery-electric fleet leader with strong launch torque and zero diesel burn.",
                requiredRank: 5,
                energyType: BusEnergyType.Electric,
                energyCapacityUnits: 420f,
                energyUnitLabel: "kWh",
                cityConsumption: 120f,
                motorwayConsumption: 102f,
                speedThreshold: 66f,
                optimalCruiseRpm: 900f,
                unitPriceKES: 64f,
                seatedCapacity: 38,
                standingCapacity: 42,
                totalCapacity: 80,
                doorOpenCapacityLimit: 80,
                bodyLengthMeters: 12.2f,
                rigidbodyMassKg: 14800f,
                maxSteerAngle: 41f,
                steerSpeed: 0.1f,
                centerOfMassY: 1.55f,
                antiRollForce: 8400f,
                maxBrakeTorque: 12800f,
                retarderStrength: 4200f,
                dragCoefficient: 0.6f,
                frontalArea: 8.4f,
                airDensity: 1.225f,
                engine: CreateElectricEngine(2400f, 700f, 2400f),
                transmission: CreateTransmission(new[] { 2.8f, 1.8f, 1.15f, 0.82f }, 3.6f, new[] { 22f, 44f, 72f, 102f })),
            CreateSpec(
                "fleet.double_decker",
                "Mara Vista DD",
                "Double-Decker",
                "Tall premium-capacity bus tuned for long busy urban feeders.",
                requiredRank: 6,
                energyType: BusEnergyType.Diesel,
                energyCapacityUnits: 360f,
                energyUnitLabel: "L",
                cityConsumption: 40f,
                motorwayConsumption: 32f,
                speedThreshold: 58f,
                optimalCruiseRpm: 1300f,
                unitPriceKES: 186f,
                seatedCapacity: 74,
                standingCapacity: 22,
                totalCapacity: 96,
                doorOpenCapacityLimit: 96,
                bodyLengthMeters: 13.4f,
                rigidbodyMassKg: 16800f,
                maxSteerAngle: 35f,
                steerSpeed: 0.09f,
                centerOfMassY: 2.15f,
                antiRollForce: 11800f,
                maxBrakeTorque: 15200f,
                retarderStrength: 3300f,
                dragCoefficient: 0.78f,
                frontalArea: 10.6f,
                airDensity: 1.225f,
                engine: CreateDieselEngine(1880f, 1120f, 2350f),
                transmission: CreateTransmission(new[] { 3.6f, 2.1f, 1.45f, 1.04f, 0.8f, 0.62f }, 4.2f, new[] { 11f, 23f, 37f, 54f, 72f, 90f }))
        };
    }

    static BusSpec CreateSpec(
        string busId,
        string name,
        string displayName,
        string description,
        int requiredRank,
        BusEnergyType energyType,
        float energyCapacityUnits,
        string energyUnitLabel,
        float cityConsumption,
        float motorwayConsumption,
        float speedThreshold,
        float optimalCruiseRpm,
        float unitPriceKES,
        int seatedCapacity,
        int standingCapacity,
        int totalCapacity,
        int doorOpenCapacityLimit,
        float bodyLengthMeters,
        float rigidbodyMassKg,
        float maxSteerAngle,
        float steerSpeed,
        float centerOfMassY,
        float antiRollForce,
        float maxBrakeTorque,
        float retarderStrength,
        float dragCoefficient,
        float frontalArea,
        float airDensity,
        EngineSystem engine,
        TransmissionSystem transmission)
    {
        var spec = ScriptableObject.CreateInstance<BusSpec>();
        spec.name = name;
        spec.busId = busId;
        spec.displayName = displayName;
        spec.description = description;
        spec.requiredRank = requiredRank;
        spec.energyType = energyType;
        spec.energyCapacityUnits = energyCapacityUnits;
        spec.energyUnitLabel = energyUnitLabel;
        spec.cityConsumptionPer100Km = cityConsumption;
        spec.motorwayConsumptionPer100Km = motorwayConsumption;
        spec.motorwaySpeedThresholdKmh = speedThreshold;
        spec.optimalCruiseRpm = optimalCruiseRpm;
        spec.unitPriceKES = unitPriceKES;
        spec.seatedCapacity = seatedCapacity;
        spec.standingCapacity = standingCapacity;
        spec.totalCapacity = totalCapacity;
        spec.doorOpenCapacityLimit = doorOpenCapacityLimit;
        spec.bodyLengthMeters = bodyLengthMeters;
        spec.rigidbodyMassKg = rigidbodyMassKg;
        spec.maxSteerAngle = maxSteerAngle;
        spec.steerSpeed = steerSpeed;
        spec.centerOfMassY = centerOfMassY;
        spec.antiRollForce = antiRollForce;
        spec.maxBrakeTorque = maxBrakeTorque;
        spec.retarderStrength = retarderStrength;
        spec.dragCoefficient = dragCoefficient;
        spec.frontalArea = frontalArea;
        spec.airDensity = airDensity;
        spec.engineProfile = engine;
        spec.transmissionProfile = transmission;
        return spec;
    }

    static EngineSystem CreateDieselEngine(float peakTorque, float peakTorqueRpm, float maxRpm)
    {
        var engine = ScriptableObject.CreateInstance<EngineSystem>();
        engine.name = "RuntimeDieselEngine";
        engine.idleRPM = 500f;
        engine.maxRPM = maxRpm;
        engine.torqueCurve = new AnimationCurve(
            new Keyframe(engine.idleRPM, peakTorque * 0.58f),
            new Keyframe(peakTorqueRpm * 0.7f, peakTorque * 0.92f),
            new Keyframe(peakTorqueRpm, peakTorque),
            new Keyframe(Mathf.Lerp(peakTorqueRpm, maxRpm, 0.55f), peakTorque * 0.88f),
            new Keyframe(maxRpm, peakTorque * 0.45f));
        return engine;
    }

    static EngineSystem CreateElectricEngine(float peakTorque, float plateauRpm, float maxRpm)
    {
        var engine = ScriptableObject.CreateInstance<EngineSystem>();
        engine.name = "RuntimeElectricMotor";
        engine.idleRPM = 350f;
        engine.maxRPM = maxRpm;
        engine.torqueCurve = new AnimationCurve(
            new Keyframe(engine.idleRPM, peakTorque * 0.96f),
            new Keyframe(plateauRpm, peakTorque),
            new Keyframe(maxRpm * 0.6f, peakTorque * 0.86f),
            new Keyframe(maxRpm, peakTorque * 0.38f));
        return engine;
    }

    static TransmissionSystem CreateTransmission(float[] ratios, float differentialRatio, float[] maxSpeeds)
    {
        var transmission = ScriptableObject.CreateInstance<TransmissionSystem>();
        transmission.name = "RuntimeTransmission";
        transmission.gearRatios = ratios;
        transmission.reverseGearRatio = -4.5f;
        transmission.differentialRatio = differentialRatio;
        transmission.gearMaxSpeeds = maxSpeeds;
        transmission.currentGear = 0;
        return transmission;
    }
}
