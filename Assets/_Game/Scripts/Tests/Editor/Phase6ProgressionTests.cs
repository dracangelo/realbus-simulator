using System.Reflection;
using NUnit.Framework;
using UnityEngine;

public class Phase6ProgressionTests
{
    [TearDown]
    public void TearDown()
    {
        ResetSingleton<XPSystem>();
        ResetSingleton<UnlockManager>();
        ResetSingleton<BusFleetManager>();
        ResetSingleton<UpgradeManager>();
        ResetSingleton<SaveManager>();
        ResetSingleton<GameState>();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        foreach (GameObject gameObject in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
            Object.DestroyImmediate(gameObject);
    }

    [Test]
    public void LiveryCodeCodec_RoundTripsSixZones_InTwelveCharacters()
    {
        var source = new LiveryData
        {
            zoneColors = new[]
            {
                new Color(1f, 0f, 0f, 1f),
                new Color(0f, 1f, 0f, 0.66f),
                new Color(0f, 0f, 1f, 0.33f),
                new Color(1f, 1f, 0f, 1f),
                new Color(0f, 1f, 1f, 1f),
                new Color(1f, 0f, 1f, 0f)
            }
        };

        string code = LiveryCodeCodec.Encode(source);
        Assert.That(LiveryCodeCodec.TryDecode(code, out LiveryData restored), Is.True);
        Assert.That(code, Has.Length.EqualTo(12));
        Assert.That(restored.zoneColors, Has.Length.EqualTo(6));
        AssertColorApproximately(restored.zoneColors[0], source.zoneColors[0], 0.01f);
        AssertColorApproximately(restored.zoneColors[1], source.zoneColors[1], 0.18f);
        AssertColorApproximately(restored.zoneColors[2], source.zoneColors[2], 0.18f);
    }

    [Test]
    public void UpgradeManager_PurchaseDeductsFunds_PersistsAndChangesConsumption()
    {
        GameState gameState = CreateComponent<GameState>("GameState");
        gameState.economy.balanceKES = 200000f;
        XPSystem xpSystem = CreateComponent<XPSystem>("XPSystem");
        xpSystem.EnsureInitialized();
        xpSystem.SetTotalXP(5400, saveLocalBackup: false);
        UpgradeManager manager = CreateComponent<UpgradeManager>("UpgradeManager");
        manager.EnsureInitialized();

        UpgradeDefinition eco = null;
        for (int i = 0; i < manager.Definitions.Count; i++)
            if (manager.Definitions[i].target == BusUpgradeTarget.EnergyEfficiency) eco = manager.Definitions[i];

        BusSpec bus = ScriptableObject.CreateInstance<BusSpec>();
        bus.busId = "fleet.test";
        bus.cityConsumptionPer100Km = 40f;
        bus.motorwayConsumptionPer100Km = 30f;
        bus.energyCapacityUnits = 300f;

        Assert.That(eco, Is.Not.Null);
        Assert.That(manager.TryPurchase(bus, eco, out string message), Is.True, message);
        Assert.That(gameState.economy.balanceKES, Is.EqualTo(200000f - eco.priceKES));
        Assert.That(manager.GetModifiers(bus.busId).energyConsumption, Is.EqualTo(eco.multiplier).Within(0.001f));
        Assert.That(PlayerPrefs.GetString(UpgradeManager.PurchasedUpgradePrefsKey), Does.Contain(eco.upgradeId));

        FuelSystem fuel = CreateComponent<FuelSystem>("FuelSystem");
        fuel.ApplyBusSpec(bus);
        Assert.That(fuel.cityBaseLPer100Km, Is.EqualTo(40f * eco.multiplier).Within(0.001f));
    }

    [Test]
    public void SaveManager_LiverySelection_IsCapturedAndRestoredPerBus()
    {
        CreateComponent<GameState>("GameState");
        XPSystem xpSystem = CreateComponent<XPSystem>("XPSystem");
        xpSystem.EnsureInitialized();
        CreateComponent<UnlockManager>("UnlockManager");
        BusFleetManager fleetManager = CreateComponent<BusFleetManager>("BusFleetManager");
        fleetManager.EnsureInitialized();
        UpgradeManager upgradeManager = CreateComponent<UpgradeManager>("UpgradeManager");
        upgradeManager.EnsureInitialized();
        SaveManager saveManager = CreateComponent<SaveManager>("SaveManager");
        saveManager.EnsureInitialized();

        string code = LiveryCodeCodec.Encode(new LiveryData());
        saveManager.RegisterLivery("fleet.standard_single_decker", code);
        SaveData captured = saveManager.CaptureCurrentData();
        saveManager.ApplySaveData(captured, saveLocalBackup: false, pushCloud: false);

        Assert.That(saveManager.TryGetLiveryCode("fleet.standard_single_decker", out string restored), Is.True);
        Assert.That(restored, Is.EqualTo(code));
        Assert.That(captured.liveries, Does.Contain("fleet.standard_single_decker=" + code));
    }

    static T CreateComponent<T>(string name) where T : Component => new GameObject(name).AddComponent<T>();

    static void ResetSingleton<T>() where T : MonoBehaviour
    {
        FieldInfo field = typeof(T).GetField("<Instance>k__BackingField", BindingFlags.Static | BindingFlags.NonPublic);
        field?.SetValue(null, null);
    }

    static void AssertColorApproximately(Color actual, Color expected, float tolerance)
    {
        Assert.That(actual.r, Is.EqualTo(expected.r).Within(tolerance));
        Assert.That(actual.g, Is.EqualTo(expected.g).Within(tolerance));
        Assert.That(actual.b, Is.EqualTo(expected.b).Within(tolerance));
        Assert.That(actual.a, Is.EqualTo(expected.a).Within(tolerance));
    }
}
