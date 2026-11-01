using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Gameplay contracts added for the city, sign, light, road and fuel-station
/// packs. These are pure logic checks so they run without the imported assets.
/// </summary>
public class NewModelPackGameplayTests
{
    readonly List<GameObject> created = new List<GameObject>();

    [TearDown]
    public void TearDown()
    {
        foreach (var gameObject in created)
            if (gameObject != null) Object.DestroyImmediate(gameObject);
        created.Clear();
        ResetSingleton<GameState>();
        // A failed session test must not leave a station owning the global session
        // slot for the next test.
        var session = typeof(GasStationServicePoint).GetProperty("ActiveSession",
            BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        session?.SetValue(null, null);
    }

    T Create<T>(string name) where T : Component
    {
        var gameObject = new GameObject(name);
        created.Add(gameObject);
        return gameObject.AddComponent<T>();
    }

    [Test]
    public void FuelRefuel_AcceptsOnlyWhatFits_AndPricesTheAcceptedAmount()
    {
        var fuel = Create<FuelSystem>("Fuel");
        fuel.tankCapacityLitres = 300f;
        fuel.dieselPricePerLitreKES = 180f;
        SetPrivate(fuel, "currentFuelLitres", 250f);

        Assert.That(fuel.GetMissingFuelLitres(), Is.EqualTo(50f).Within(0.001f));
        Assert.That(fuel.Refuel(40f), Is.EqualTo(40f).Within(0.001f));
        Assert.That(fuel.CurrentFuelLitres, Is.EqualTo(290f).Within(0.001f));

        Assert.That(fuel.Refuel(40f), Is.EqualTo(10f).Within(0.001f),
            "A part fill must not accept more than the tank can hold.");
        Assert.That(fuel.CurrentFuelLitres, Is.EqualTo(300f).Within(0.001f));
        Assert.That(fuel.Refuel(5f), Is.EqualTo(0f).Within(0.001f), "A full tank accepts nothing.");

        Assert.That(fuel.PriceForLitres(12.5f), Is.EqualTo(2250f).Within(0.01f));
        Assert.That(fuel.PriceForLitres(-3f), Is.EqualTo(0f), "A negative amount must never become a credit.");
    }

    [Test]
    public void BatteryRecharge_AcceptsOnlyWhatFits_AndPricesTheAcceptedAmount()
    {
        var battery = Create<BatterySystem>("Battery");
        battery.batteryCapacityKWh = 420f;
        battery.electricityPricePerKWhKES = 64f;
        SetPrivate(battery, "currentChargeKWh", 400f);

        Assert.That(battery.GetMissingChargeKWh(), Is.EqualTo(20f).Within(0.001f));
        Assert.That(battery.Recharge(15f), Is.EqualTo(15f).Within(0.001f));
        Assert.That(battery.CurrentChargeKWh, Is.EqualTo(415f).Within(0.001f));

        Assert.That(battery.Recharge(15f), Is.EqualTo(5f).Within(0.001f),
            "A part charge must not accept more than the battery can hold.");
        Assert.That(battery.CurrentChargeKWh, Is.EqualTo(420f).Within(0.001f));
        Assert.That(battery.Recharge(1f), Is.EqualTo(0f).Within(0.001f), "A full battery accepts nothing.");

        Assert.That(battery.PriceForKWh(7.5f), Is.EqualTo(480f).Within(0.01f));
        Assert.That(battery.PriceForKWh(-1f), Is.EqualTo(0f), "A negative amount must never become a credit.");
    }

    [Test]
    public void GasStationService_AmountAndPriceAreClampedAndMonotonic()
    {
        Assert.That(GasStationServicePoint.AmountFor(22f, .5f), Is.EqualTo(11f).Within(0.001f));
        Assert.That(GasStationServicePoint.AmountFor(-22f, .5f), Is.EqualTo(0f));
        Assert.That(GasStationServicePoint.AmountFor(22f, -1f), Is.EqualTo(0f));
        Assert.That(GasStationServicePoint.AmountFor(0f, 10f), Is.EqualTo(0f));

        Assert.That(GasStationServicePoint.PriceFor(11f, 180f), Is.EqualTo(1980f).Within(0.01f));
        Assert.That(GasStationServicePoint.PriceFor(-1f, 180f), Is.EqualTo(0f));
        Assert.That(GasStationServicePoint.PriceFor(11f, 0f), Is.EqualTo(0f));
    }

    [Test]
    public void GasStationService_AffordableAmountFollowsThePlayerBalance()
    {
        // Awake does not run in Edit Mode, so the test publishes the singleton
        // itself. Other suites also leave a GameState behind, hence the reset.
        ResetSingleton<GameState>();
        var state = Create<GameState>("GameState");
        SetSingleton(state);
        state.economy.balanceKES = 900f;

        Assert.That(GasStationServicePoint.AffordableAmount(180f), Is.EqualTo(5f).Within(0.001f));
        Assert.That(GasStationServicePoint.AffordableAmount(0f), Is.EqualTo(float.MaxValue),
            "A free unit price is never limited by the balance.");

        state.economy.balanceKES = 0f;
        Assert.That(GasStationServicePoint.AffordableAmount(180f), Is.EqualTo(0f));
    }

    [Test]
    public void RoadModuleCatalog_PicksCheapestTileAndLongestRoadPiece()
    {
        var catalog = ScriptableObject.CreateInstance<RoadModuleCatalog>();
        try
        {
            catalog.entries = new[]
            {
                Module(RoadModuleCatalog.Kind.RoadTile, 2, new Vector3(7f, 0f, 15f)),
                Module(RoadModuleCatalog.Kind.RoadTileLong, 10, new Vector3(7.4f, 0f, 24.25f)),
                Module(RoadModuleCatalog.Kind.Sidewalk, 1, new Vector3(2.5f, 0f, 5f))
            };

            Assert.That(catalog.Pick(RoadModuleCatalog.Kind.RoadTile).triangles, Is.EqualTo(2),
                "The cheapest tile keeps a forecourt apron cheap.");
            Assert.That(catalog.PickLongestRoadTile().bounds.size.z, Is.EqualTo(24.25f).Within(0.001f),
                "The longest tile covers the apron with fewer instances.");
            Assert.That(catalog.Pick(RoadModuleCatalog.Kind.Kerb), Is.Null,
                "A missing kind returns null rather than an unrelated module.");
        }
        finally
        {
            Object.DestroyImmediate(catalog);
        }
    }

    [Test]
    public void GeneratedGasStations_NeverBlockTheBusWithASolidCollider()
    {
        var prefabs = Resources.LoadAll<GameObject>("GasStationsGenerated");
        Assert.That(prefabs, Is.Not.Empty, "The fuel-station prefabs are generated assets and must ship with the project.");
        foreach (var prefab in prefabs)
        {
            if (prefab == null) continue;
            foreach (var collider in prefab.GetComponentsInChildren<Collider>(true))
                Assert.That(collider.isTrigger, Is.True,
                    $"'{prefab.name}' has a solid collider, so the bus could not drive in.");
        }
    }

    [Test]
    public void GasStationForecourt_PavesTheApronWithImportedModulesAndNoPhysics()
    {
        Assert.That(GasStationForecourt.IsAvailable, Is.True,
            "The low-poly road pack must provide at least one road tile.");

        var station = new GameObject("station");
        created.Add(station);
        station.transform.position = new Vector3(0f, .28f, 0f);
        var apron = GasStationForecourt.Build(station.transform,
            new Bounds(new Vector3(0f, 2.45f, 0f), new Vector3(33.9f, 4.9f, 35f)));
        Assert.That(apron, Is.Not.Null);
        Assert.That(apron.GetComponentsInChildren<MeshRenderer>().Length, Is.GreaterThan(0));

        Assert.That(apron.GetComponentsInChildren<Collider>(true), Is.Empty,
            "The apron must never add physics over the road ribbon.");

        foreach (var renderer in apron.GetComponentsInChildren<MeshRenderer>())
            Assert.That(renderer.bounds.center.y, Is.EqualTo(GasStationForecourt.SurfaceY).Within(0.05f),
                "The apron sits above the map tiles and just below the generated road.");
    }

    [Test]
    public void GasStationService_AddsFuelAndChargesTheBalanceForExactlyThatFuel()
    {
        var state = NewGameState(10000f);
        var bus = CreateBus();
        var fuel = bus.GetComponent<FuelSystem>();
        fuel.tankCapacityLitres = 300f;
        fuel.dieselPricePerLitreKES = 180f;
        SetPrivate(fuel, "currentFuelLitres", 250f);

        var point = CreateServicePoint();
        point.serviceBattery = false;
        point.logService = false;

        Assert.That(point.BindBus(bus), Is.True);
        Assert.That(point.Service(1f), Is.True, "A diesel bus inside the trigger must be refuelled.");

        Assert.That(fuel.CurrentFuelLitres, Is.EqualTo(272f).Within(.01f),
            "One second at 22 L/s adds 22 L.");
        Assert.That(point.SessionAmount, Is.EqualTo(22f).Within(.01f));
        Assert.That(point.SessionCostKES, Is.EqualTo(3960f).Within(.5f));
        Assert.That(state.economy.balanceKES, Is.EqualTo(6040f).Within(.5f),
            "Only the fuel actually delivered is charged.");
        Assert.That(point.IsServicing, Is.True);
    }

    [Test]
    public void GasStationService_StopsAtTheBalanceAndNeverCreditsThePlayer()
    {
        var state = NewGameState(900f);
        var bus = CreateBus();
        var fuel = bus.GetComponent<FuelSystem>();
        fuel.tankCapacityLitres = 300f;
        fuel.dieselPricePerLitreKES = 180f;
        SetPrivate(fuel, "currentFuelLitres", 100f);

        var point = CreateServicePoint();
        point.serviceBattery = false;
        point.logService = false;
        point.BindBus(bus);

        Assert.That(point.Service(1f), Is.True);
        Assert.That(fuel.CurrentFuelLitres, Is.EqualTo(105f).Within(.01f),
            "KES 900 buys exactly 5 L at KES 180.");
        Assert.That(state.economy.balanceKES, Is.EqualTo(0f).Within(.01f));

        Assert.That(point.Service(1f), Is.False, "An empty balance stops service.");
        Assert.That(fuel.CurrentFuelLitres, Is.EqualTo(105f).Within(.01f));
        Assert.That(state.economy.balanceKES, Is.EqualTo(0f).Within(.01f),
            "A stopped service must never push the balance below zero.");
    }

    [Test]
    public void GasStationService_ChargesAnElectricBusInsteadOfRefuellingIt()
    {
        var state = NewGameState(10000f);
        var bus = CreateBus();
        var fuel = bus.GetComponent<FuelSystem>();
        var battery = bus.GetComponent<BatterySystem>();
        fuel.tankCapacityLitres = 300f;
        fuel.dieselPricePerLitreKES = 180f;
        SetPrivate(fuel, "currentFuelLitres", 120f);
        battery.batteryCapacityKWh = 420f;
        battery.electricityPricePerKWhKES = 64f;
        SetPrivate(battery, "currentChargeKWh", 300f);

        var point = CreateServicePoint();
        point.logService = false;
        point.BindBus(bus);

        Assert.That(point.Service(1f), Is.True);
        Assert.That(battery.CurrentChargeKWh, Is.EqualTo(355f).Within(.01f),
            "A bus with a battery is charged at 55 kWh/s.");
        Assert.That(fuel.CurrentFuelLitres, Is.EqualTo(120f).Within(.01f),
            "The diesel tank must be left alone.");
        Assert.That(point.SessionCostKES, Is.EqualTo(3520f).Within(.5f));
        Assert.That(state.economy.balanceKES, Is.EqualTo(6480f).Within(.5f));
    }

    [Test]
    public void GasStationService_LeavesAFullTankAloneAndFree()
    {
        var state = NewGameState(5000f);
        var bus = CreateBus();
        var fuel = bus.GetComponent<FuelSystem>();
        fuel.tankCapacityLitres = 300f;
        fuel.dieselPricePerLitreKES = 180f;
        SetPrivate(fuel, "currentFuelLitres", 300f);

        var point = CreateServicePoint();
        point.serviceBattery = false;
        point.logService = false;
        point.BindBus(bus);

        Assert.That(point.Service(1f), Is.False);
        Assert.That(point.SessionCostKES, Is.EqualTo(0f));
        Assert.That(state.economy.balanceKES, Is.EqualTo(5000f).Within(.01f));
    }

    [Test]
    public void GasStationSession_RequiresAStoppedBusAndStartsOnlyOnce()
    {
        NewGameState(100000f);
        var bus = CreateBus();
        var fuel = bus.GetComponent<FuelSystem>();
        fuel.tankCapacityLitres = 300f;
        fuel.dieselPricePerLitreKES = 180f;
        SetPrivate(fuel, "currentFuelLitres", 100f);

        var point = CreateServicePoint();
        point.serviceBattery = false;
        point.logService = false;
        point.BindBus(bus);

        bus.currentSpeedKmh = 12f;
        Assert.That(point.BeginSession(50f, out string reason), Is.False,
            "A moving bus must not open a paid session.");
        Assert.That(reason, Is.EqualTo("station.tooFast"));

        bus.currentSpeedKmh = 0f;
        Assert.That(point.BeginSession(50f, out reason), Is.True);
        Assert.That(reason, Is.Null);
        Assert.That(point.SessionActive, Is.True);
        Assert.That(point.TargetAmount, Is.EqualTo(50f).Within(.01f));

        Assert.That(point.BeginSession(20f, out reason), Is.False,
            "A second start must not open a duplicate session.");
        Assert.That(reason, Is.EqualTo("station.active"));
        Assert.That(point.TargetAmount, Is.EqualTo(50f).Within(.01f));
    }

    [Test]
    public void GasStationSession_StopsAtTheTargetAndChargesOnlyDeliveredEnergy()
    {
        var state = NewGameState(100000f);
        var bus = CreateBus();
        var fuel = bus.GetComponent<FuelSystem>();
        fuel.tankCapacityLitres = 300f;
        fuel.dieselPricePerLitreKES = 180f;
        SetPrivate(fuel, "currentFuelLitres", 100f);

        var point = CreateServicePoint();
        point.serviceBattery = false;
        point.logService = false;
        point.BindBus(bus);

        Assert.That(point.BeginSession(30f), Is.True);
        Assert.That(point.Service(1f), Is.True);
        Assert.That(fuel.CurrentFuelLitres, Is.EqualTo(122f).Within(.01f));
        Assert.That(point.SessionAmount, Is.EqualTo(22f).Within(.01f));
        Assert.That(point.Progress01, Is.EqualTo(22f / 30f).Within(.001f));
        Assert.That(point.SessionActive, Is.True);

        Assert.That(point.Service(1f), Is.True, "The final tick buys only the remaining 8 L.");
        Assert.That(fuel.CurrentFuelLitres, Is.EqualTo(130f).Within(.01f));
        Assert.That(point.SessionActive, Is.False, "The session closes when the target is reached.");
        Assert.That(state.economy.balanceKES, Is.EqualTo(100000f - 30f * 180f).Within(.5f),
            "Thirty litres must be charged once, at the station price.");
        Assert.That(point.LastSessionAmount, Is.EqualTo(30f).Within(.01f));
    }

    [Test]
    public void GasStationSession_CancelsWhenTheBusDrivesAwayAndKeepsOnlyWhatItBought()
    {
        var state = NewGameState(100000f);
        var bus = CreateBus();
        var fuel = bus.GetComponent<FuelSystem>();
        fuel.tankCapacityLitres = 300f;
        fuel.dieselPricePerLitreKES = 180f;
        SetPrivate(fuel, "currentFuelLitres", 100f);

        var point = CreateServicePoint();
        point.serviceBattery = false;
        point.logService = false;
        point.BindBus(bus);
        Assert.That(point.BeginSession(100f), Is.True);

        Assert.That(point.Service(.5f), Is.True);
        Assert.That(fuel.CurrentFuelLitres, Is.EqualTo(111f).Within(.01f));

        bus.currentSpeedKmh = 8f;
        Assert.That(point.Service(.5f), Is.False, "A moving bus must end the session.");
        Assert.That(point.SessionActive, Is.False);
        Assert.That(fuel.CurrentFuelLitres, Is.EqualTo(111f).Within(.01f),
            "Cancelling must not deliver or charge for any extra fuel.");
        Assert.That(state.economy.balanceKES, Is.EqualTo(100000f - 11f * 180f).Within(.5f));
    }

    [Test]
    public void GasStationSession_RejectsFullTanksAndEmptyBalancesWithReasons()
    {
        var state = NewGameState(0f);
        var bus = CreateBus();
        var fuel = bus.GetComponent<FuelSystem>();
        fuel.tankCapacityLitres = 300f;
        fuel.dieselPricePerLitreKES = 180f;
        SetPrivate(fuel, "currentFuelLitres", 300f);

        var point = CreateServicePoint();
        point.serviceBattery = false;
        point.logService = false;
        point.BindBus(bus);

        Assert.That(point.BeginSession(10f, out string reason), Is.False);
        Assert.That(reason, Is.EqualTo("station.full"));

        SetPrivate(fuel, "currentFuelLitres", 100f);
        Assert.That(point.BeginSession(10f, out reason), Is.False);
        Assert.That(reason, Is.EqualTo("station.noFunds"));
        Assert.That(state.economy.balanceKES, Is.EqualTo(0f));
    }

    [Test]
    public void GasStationSession_OnlyOneStationCanOwnAPaidSession()
    {
        NewGameState(100000f);
        var bus = CreateBus();
        var fuel = bus.GetComponent<FuelSystem>();
        fuel.tankCapacityLitres = 300f;
        fuel.dieselPricePerLitreKES = 180f;
        SetPrivate(fuel, "currentFuelLitres", 100f);

        var first = CreateServicePoint();
        first.serviceBattery = false;
        first.logService = false;
        first.BindBus(bus);

        var second = CreateServicePoint();
        second.serviceBattery = false;
        second.logService = false;
        second.BindBus(bus);

        Assert.That(first.BeginSession(40f), Is.True);
        Assert.That(second.BeginSession(40f, out string reason), Is.False);
        Assert.That(reason, Is.EqualTo("station.busy"));
        Assert.That(GasStationServicePoint.ActiveSession, Is.SameAs(first));
        first.StopSession(cancelled: true);
    }

    [Test]
    public void GasStationSession_RefusesAnIncompatibleStation()
    {
        NewGameState(100000f);
        var bus = CreateBus();
        var battery = bus.GetComponent<BatterySystem>();
        battery.batteryCapacityKWh = 420f;
        battery.electricityPricePerKWhKES = 64f;
        SetPrivate(battery, "currentChargeKWh", 100f);

        // An electric bus carries no tank, so a fuel-only site cannot serve it.
        Object.DestroyImmediate(bus.GetComponent<FuelSystem>());

        var fuelOnly = CreateServicePoint();
        fuelOnly.supportsFuel = true;
        fuelOnly.supportsCharging = false;
        fuelOnly.logService = false;
        fuelOnly.BindBus(bus);

        Assert.That(fuelOnly.Mode, Is.EqualTo(GasStationServicePoint.ServiceMode.None));
        Assert.That(fuelOnly.BeginSession(10f, out string reason), Is.False);
        Assert.That(reason, Is.EqualTo("station.unsupported"));

        var dualService = CreateServicePoint();
        dualService.supportsFuel = true;
        dualService.supportsCharging = true;
        dualService.logService = false;
        dualService.BindBus(bus);
        Assert.That(dualService.Mode, Is.EqualTo(GasStationServicePoint.ServiceMode.Charge),
            "A dual-service site must charge the electric bus instead of trying the tank.");
        Assert.That(dualService.BeginSession(10f), Is.True);
        dualService.StopSession();
    }

    [Test]
    public void GasStationSession_ChargesAnElectricBusAndPersistsTheBatteryState()
    {
        var state = NewGameState(100000f);
        var bus = CreateBus();
        var fuel = bus.GetComponent<FuelSystem>();
        var battery = bus.GetComponent<BatterySystem>();
        fuel.tankCapacityLitres = 300f;
        SetPrivate(fuel, "currentFuelLitres", 120f);
        battery.batteryCapacityKWh = 420f;
        battery.electricityPricePerKWhKES = 64f;
        SetPrivate(battery, "currentChargeKWh", 300f);

        var point = CreateServicePoint();
        point.logService = false;
        point.BindBus(bus);
        Assert.That(point.Mode, Is.EqualTo(GasStationServicePoint.ServiceMode.Charge));

        Assert.That(point.BeginSession(50f), Is.True);
        Assert.That(point.Service(1f), Is.True);
        Assert.That(battery.CurrentChargeKWh, Is.EqualTo(350f).Within(.01f));
        Assert.That(fuel.CurrentFuelLitres, Is.EqualTo(120f).Within(.01f),
            "Charging must not touch the diesel tank.");
        Assert.That(state.economy.balanceKES, Is.EqualTo(100000f - 50f * 64f).Within(.5f));

        Assert.That(state.vehicleState.isElectricBus, Is.True);
        Assert.That(state.vehicleState.fuelLitres, Is.EqualTo(350f).Within(.01f));
        Assert.That(state.vehicleState.fuelCapacityLitres, Is.EqualTo(420f).Within(.01f));
    }

    [Test]
    public void GasStationSession_PersistsFuelThroughTheExistingVehicleState()
    {
        var state = NewGameState(100000f);
        var bus = CreateBus();
        var fuel = bus.GetComponent<FuelSystem>();
        fuel.tankCapacityLitres = 300f;
        fuel.dieselPricePerLitreKES = 180f;
        SetPrivate(fuel, "currentFuelLitres", 100f);

        var point = CreateServicePoint();
        point.serviceBattery = false;
        point.logService = false;
        point.BindBus(bus);
        point.BeginSession(25f);
        point.Service(1f);

        Assert.That(state.vehicleState.isElectricBus, Is.False);
        Assert.That(state.vehicleState.fuelLitres, Is.EqualTo(122f).Within(.01f));
        Assert.That(state.vehicleState.fuelCapacityLitres, Is.EqualTo(300f).Within(.01f));

        // A reload constructs the system again and reads the same state.
        var reloaded = Create<FuelSystem>("ReloadedFuel");
        reloaded.tankCapacityLitres = 1f;
        SetPrivate(reloaded, "currentFuelLitres", 0f);
        var load = typeof(FuelSystem).GetMethod("LoadFromGameState",
            BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.That(load, Is.Not.Null);
        load.Invoke(reloaded, null);
        Assert.That(reloaded.CurrentFuelLitres, Is.EqualTo(122f).Within(.01f));
        Assert.That(reloaded.tankCapacityLitres, Is.EqualTo(300f).Within(.01f));
    }

    [Test]
    public void GasStationTargetAmountFor_ClampsFractionsToTheMissingCapacity()
    {
        Assert.That(GasStationServicePoint.TargetAmountFor(1f, 80f), Is.EqualTo(80f).Within(.001f));
        Assert.That(GasStationServicePoint.TargetAmountFor(.5f, 80f), Is.EqualTo(40f).Within(.001f));
        Assert.That(GasStationServicePoint.TargetAmountFor(-1f, 80f), Is.EqualTo(0f));
        Assert.That(GasStationServicePoint.TargetAmountFor(2f, 80f), Is.EqualTo(80f).Within(.001f));
        Assert.That(GasStationServicePoint.TargetAmountFor(.5f, -10f), Is.EqualTo(0f));
    }

    GameState NewGameState(float balance)
    {
        // Awake does not run in Edit Mode, so the test publishes the singleton
        // itself. Other suites also leave a GameState behind, hence the reset.
        ResetSingleton<GameState>();
        var state = Create<GameState>("GameState");
        SetSingleton(state);
        state.economy.balanceKES = balance;
        return state;
    }

    BusController CreateBus()
    {
        var gameObject = new GameObject("Bus");
        created.Add(gameObject);
        var bus = gameObject.AddComponent<BusController>();
        gameObject.AddComponent<FuelSystem>();
        gameObject.AddComponent<BatterySystem>();
        return bus;
    }

    GasStationServicePoint CreateServicePoint()
    {
        var gameObject = new GameObject("Fuel stop");
        created.Add(gameObject);
        gameObject.AddComponent<BoxCollider>();
        return gameObject.AddComponent<GasStationServicePoint>();
    }

    [Test]
    public void ShippedCatalogs_ContainTheImportedCityFurniture()
    {
        var live = Resources.Load<LiveCityCatalog>("LiveCityCatalog");
        Assert.That(live, Is.Not.Null, "The shipped scenery catalog is missing.");
        Assert.That(live.entries, Is.Not.Empty);

        bool hasStreetLight = false, hasRoadSign = false;
        foreach (var entry in live.entries)
        {
            Assert.That(entry.prefab, Is.Not.Null, "A catalog entry lost its prefab.");
            Assert.That(entry.bounds.size.y, Is.GreaterThan(0f), $"'{entry.prefab.name}' has no height.");
            hasStreetLight |= entry.kind == LiveCityCatalog.Kind.StreetLight;
            hasRoadSign |= entry.kind == LiveCityCatalog.Kind.RoadSign;
        }
        Assert.That(hasStreetLight, Is.True, "The street-light pack must reach the streamer.");
        Assert.That(hasRoadSign, Is.True, "The road-sign pack must reach the streamer.");

        var buildings = Resources.Load<BuildingCatalog>("BuildingCatalog");
        Assert.That(buildings, Is.Not.Null, "The shipped building catalog is missing.");
        bool hasTower = false;
        foreach (var entry in buildings.entries)
        {
            if (entry.prefab == null) continue;
            if (entry.bounds.size.y > 80f) hasTower = true;
        }
        Assert.That(hasTower, Is.True, "The city pack's towers must reach the infill.");

        var roads = Resources.Load<RoadModuleCatalog>("RoadModuleCatalog");
        Assert.That(roads, Is.Not.Null, "The shipped road-module catalog is missing.");
        Assert.That(roads.PickLongestRoadTile(), Is.Not.Null);
        Assert.That(roads.Pick(RoadModuleCatalog.Kind.Sidewalk), Is.Not.Null);
    }

    [Test]
    public void LiveCityCatalog_AppendsNewKindsWithoutRenumberingTheOldOnes()
    {
        Assert.That((int)LiveCityCatalog.Kind.Tree, Is.EqualTo(0));
        Assert.That((int)LiveCityCatalog.Kind.Pedestrian, Is.EqualTo(5));
        Assert.That((int)LiveCityCatalog.Kind.StreetLight, Is.EqualTo(6));
        Assert.That((int)LiveCityCatalog.Kind.RoadSign, Is.EqualTo(7));

        Assert.That(SceneryLodPolicy.PropRenderDistance(LiveCityCatalog.Kind.StreetLight),
            Is.EqualTo(SceneryLodPolicy.TinyPropRenderDistance));
        Assert.That(SceneryLodPolicy.PropRenderDistance(LiveCityCatalog.Kind.RoadSign),
            Is.EqualTo(SceneryLodPolicy.TinyPropRenderDistance));
    }

    RoadModuleCatalog.Entry Module(RoadModuleCatalog.Kind kind, int triangles, Vector3 size)
    {
        var prefab = new GameObject("module");
        created.Add(prefab);
        return new RoadModuleCatalog.Entry
        {
            prefab = prefab,
            kind = kind,
            triangles = triangles,
            bounds = new Bounds(Vector3.zero, size),
            sourceName = prefab.name
        };
    }

    static void SetPrivate(object target, string fieldName, object value)
    {
        var field = target.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.That(field, Is.Not.Null, $"Expected private field '{fieldName}' to exist.");
        field.SetValue(target, value);
    }

    static void SetSingleton<T>(T instance) where T : MonoBehaviour
    {
        var field = typeof(T).GetField("<Instance>k__BackingField", BindingFlags.Static | BindingFlags.NonPublic);
        Assert.That(field, Is.Not.Null, $"Expected singleton backing field for '{typeof(T).Name}' to exist.");
        field.SetValue(null, instance);
    }

    static void ResetSingleton<T>() where T : MonoBehaviour
    {
        var field = typeof(T).GetField("<Instance>k__BackingField", BindingFlags.Static | BindingFlags.NonPublic);
        field?.SetValue(null, null);
    }
}
