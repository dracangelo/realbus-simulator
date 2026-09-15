using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public const string PurchasedUpgradePrefsKey = "progress.purchased_upgrades";

    public static UpgradeManager Instance { get; private set; }
    public event Action<string, UpgradeDefinition> UpgradePurchased;

    [SerializeField] List<UpgradeDefinition> definitions = new List<UpgradeDefinition>();
    readonly HashSet<string> purchasedKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    bool initialized;

    public IReadOnlyList<UpgradeDefinition> Definitions
    {
        get { EnsureInitialized(); return definitions; }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap() => EnsureExists();

    public static UpgradeManager EnsureExists()
    {
        if (Instance != null)
            return Instance;

        var existing = FindFirstObjectByType<UpgradeManager>();
        if (existing != null)
            return existing;

        return new GameObject("UpgradeManager").AddComponent<UpgradeManager>();
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
        if (definitions == null || definitions.Count == 0)
        {
            definitions = new List<UpgradeDefinition>(Resources.LoadAll<UpgradeDefinition>("Progression/Upgrades"));
            if (definitions.Count == 0)
                definitions = BuildDefaultDefinitions();
        }

        definitions.Sort((left, right) => string.Compare(left?.upgradeId, right?.upgradeId, StringComparison.OrdinalIgnoreCase));
        LoadLocalBackup();
    }

    public bool IsPurchased(string busId, UpgradeDefinition definition)
    {
        EnsureInitialized();
        return definition != null && purchasedKeys.Contains(BuildPurchaseKey(busId, definition.upgradeId));
    }

    public bool TryPurchase(BusSpec bus, UpgradeDefinition definition, out string message)
    {
        EnsureInitialized();
        if (bus == null || definition == null)
        {
            message = "Select a bus and upgrade first.";
            return false;
        }

        string key = BuildPurchaseKey(bus.busId, definition.upgradeId);
        if (purchasedKeys.Contains(key))
        {
            message = "Already installed.";
            return false;
        }

        int rank = XPSystem.Instance != null ? XPSystem.Instance.CurrentRank : 1;
        if (rank < definition.requiredRank)
        {
            message = $"Unlocks at Rank {definition.requiredRank}.";
            return false;
        }

        var economy = GameState.Instance != null ? GameState.Instance.economy : null;
        if (economy == null || economy.balanceKES < definition.priceKES)
        {
            message = $"Requires KES {definition.priceKES:N0}.";
            return false;
        }

        economy.balanceKES -= definition.priceKES;
        purchasedKeys.Add(key);
        SaveLocalBackup();
        SaveManager.Instance?.RegisterUpgradePurchase(key, definition.multiplier.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
        ApplyToActiveVehicle(bus);
        UpgradePurchased?.Invoke(bus.busId, definition);
        message = $"{definition.displayName} installed.";
        return true;
    }

    public BusUpgradeModifiers GetModifiers(string busId)
    {
        EnsureInitialized();
        var modifiers = BusUpgradeModifiers.Identity;
        for (int i = 0; i < definitions.Count; i++)
        {
            UpgradeDefinition definition = definitions[i];
            if (definition != null && purchasedKeys.Contains(BuildPurchaseKey(busId, definition.upgradeId)))
                modifiers.Apply(definition);
        }
        return modifiers;
    }

    public string[] GetPurchasedKeys()
    {
        EnsureInitialized();
        var values = new List<string>(purchasedKeys);
        values.Sort(StringComparer.OrdinalIgnoreCase);
        return values.ToArray();
    }

    public void ApplySaveEntries(IEnumerable<SaveKeyValueEntry> entries)
    {
        EnsureInitialized();
        purchasedKeys.Clear();
        if (entries != null)
        {
            foreach (SaveKeyValueEntry entry in entries)
            {
                if (entry != null && IsPurchaseKey(entry.key))
                    purchasedKeys.Add(entry.key);
            }
        }
        SaveLocalBackup();
    }

    public static string BuildPurchaseKey(string busId, string upgradeId)
    {
        string safeBus = string.IsNullOrWhiteSpace(busId) ? "fleet.unknown" : busId.Trim();
        string safeUpgrade = string.IsNullOrWhiteSpace(upgradeId) ? "upgrade.unknown" : upgradeId.Trim();
        return $"{safeBus}|{safeUpgrade}";
    }

    static bool IsPurchaseKey(string key) => !string.IsNullOrWhiteSpace(key) && key.Contains("|upgrade.");

    void ApplyToActiveVehicle(BusSpec bus)
    {
        BusController controller = FindFirstObjectByType<BusController>();
        if (controller != null) controller.ApplyBusSpec(bus);

        FuelSystem fuel = FindFirstObjectByType<FuelSystem>();
        BatterySystem battery = FindFirstObjectByType<BatterySystem>();
        if (bus.IsElectric)
        {
            if (fuel != null) fuel.enabled = false;
            if (battery != null)
            {
                battery.enabled = true;
                battery.ApplyBusSpec(bus);
            }
        }
        else
        {
            if (battery != null) battery.enabled = false;
            if (fuel != null)
            {
                fuel.enabled = true;
                fuel.ApplyBusSpec(bus);
            }
        }

        GameState.Instance?.vehicleState?.ApplyBusSpec(bus, preserveEnergyPercent: true);
    }

    void LoadLocalBackup()
    {
        purchasedKeys.Clear();
        string raw = PlayerPrefs.GetString(PurchasedUpgradePrefsKey, string.Empty);
        if (string.IsNullOrWhiteSpace(raw))
            return;
        string[] values = raw.Split(';');
        for (int i = 0; i < values.Length; i++)
            if (IsPurchaseKey(values[i])) purchasedKeys.Add(values[i]);
    }

    void SaveLocalBackup()
    {
        PlayerPrefs.SetString(PurchasedUpgradePrefsKey, string.Join(";", GetPurchasedKeysWithoutInitialization()));
        PlayerPrefs.Save();
    }

    string[] GetPurchasedKeysWithoutInitialization()
    {
        var values = new List<string>(purchasedKeys);
        values.Sort(StringComparer.OrdinalIgnoreCase);
        return values.ToArray();
    }

    public static List<UpgradeDefinition> BuildDefaultDefinitions()
    {
        return new List<UpgradeDefinition>
        {
            Create("upgrade.engine_tune", "Engine Tune", "+12% torque • +4% energy use", BusUpgradeTarget.EngineTorque, 1.12f, BusUpgradeTarget.EnergyEfficiency, 1.04f, 32000, 2),
            Create("upgrade.brake_package", "Heavy-Duty Brakes", "+15% braking • -3% steering response", BusUpgradeTarget.BrakeTorque, 1.15f, BusUpgradeTarget.SteeringResponse, 0.97f, 28000, 2),
            Create("upgrade.eco_calibration", "Eco Calibration", "-10% energy use • -4% torque", BusUpgradeTarget.EnergyEfficiency, 0.90f, BusUpgradeTarget.EngineTorque, 0.96f, 40000, 3),
            Create("upgrade.steering_rack", "Quick Steering Rack", "+12% response • +3% energy use", BusUpgradeTarget.SteeringResponse, 1.12f, BusUpgradeTarget.EnergyEfficiency, 1.03f, 26000, 3),
            Create("upgrade.extended_range", "Extended Range", "+15% capacity • -3% torque", BusUpgradeTarget.EnergyCapacity, 1.15f, BusUpgradeTarget.EngineTorque, 0.97f, 54000, 5)
        };
    }

    static UpgradeDefinition Create(string id, string title, string description, BusUpgradeTarget target, float multiplier,
        BusUpgradeTarget secondaryTarget, float secondaryMultiplier, int price, int rank)
    {
        var definition = ScriptableObject.CreateInstance<UpgradeDefinition>();
        definition.name = title;
        definition.upgradeId = id;
        definition.displayName = title;
        definition.description = description;
        definition.target = target;
        definition.multiplier = multiplier;
        definition.secondaryTarget = secondaryTarget;
        definition.secondaryMultiplier = secondaryMultiplier;
        definition.priceKES = price;
        definition.requiredRank = rank;
        return definition;
    }
}
