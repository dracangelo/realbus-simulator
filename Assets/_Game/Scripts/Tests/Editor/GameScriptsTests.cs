using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameScriptsTests
{
    [TearDown]
    public void TearDown()
    {
        ResetSingleton<ScheduleManager>();
        ResetSingleton<ScoreTracker>();
        ResetSingleton<PassengerManager>();
        ResetSingleton<FreeDriveSession>();
        ResetSingleton<GameState>();
        ResetSingleton<DriverShiftSystem>();
        ResetSingleton<ExtendedTrafficViolationSystem>();
        ResetSingleton<DynamicEventSystem>();
        ResetSingleton<WeatherSystem>();
        ResetSingleton<XPSystem>();
        ResetSingleton<UnlockManager>();
        ResetSingleton<BusFleetManager>();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        foreach (var gameObject in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            Object.DestroyImmediate(gameObject);
        }
    }

    [Test]
    public void SetupSchedule_SpacesStopsEvenly_AndArrivalThresholdsMatchExpectedBands()
    {
        var scheduleManager = CreateComponent<ScheduleManager>("ScheduleManager");
        var route = ScriptableObject.CreateInstance<BusRoute>();
        route.stops = new[]
        {
            new BusStopData { stopName = "A" },
            new BusStopData { stopName = "B" },
            new BusStopData { stopName = "C" },
            new BusStopData { stopName = "D" }
        };

        scheduleManager.SetupSchedule(route, 480f, 30f);

        Assert.That(scheduleManager.GetScheduledArrival(0), Is.EqualTo(480f));
        Assert.That(scheduleManager.GetScheduledArrival(1), Is.EqualTo(490f));
        Assert.That(scheduleManager.GetScheduledArrival(2), Is.EqualTo(500f));
        Assert.That(scheduleManager.GetScheduledArrival(3), Is.EqualTo(510f));

        scheduleManager.currentTimeMinutes = 488.5f;
        Assert.That(scheduleManager.RecordArrival(1), Is.EqualTo(PunctualityStatus.Early));

        scheduleManager.currentTimeMinutes = 500.5f;
        Assert.That(scheduleManager.RecordArrival(2), Is.EqualTo(PunctualityStatus.OnTime));

        scheduleManager.currentTimeMinutes = 512.5f;
        Assert.That(scheduleManager.RecordArrival(3), Is.EqualTo(PunctualityStatus.Late));

        scheduleManager.currentTimeMinutes = 515f;
        Assert.That(scheduleManager.RecordArrival(0), Is.EqualTo(PunctualityStatus.SeverelyLate));
    }

    [Test]
    public void SetupSchedule_ReplacesPreviousSchedule_AndUnknownStopDefaultsToOnTime()
    {
        var scheduleManager = CreateComponent<ScheduleManager>("ScheduleManager");
        var firstRoute = ScriptableObject.CreateInstance<BusRoute>();
        firstRoute.stops = new[]
        {
            new BusStopData { stopName = "A" },
            new BusStopData { stopName = "B" },
            new BusStopData { stopName = "C" }
        };

        var secondRoute = ScriptableObject.CreateInstance<BusRoute>();
        secondRoute.stops = new[]
        {
            new BusStopData { stopName = "X" },
            new BusStopData { stopName = "Y" }
        };

        scheduleManager.SetupSchedule(firstRoute, 480f, 20f);
        Assert.That(scheduleManager.GetScheduledArrival(2), Is.EqualTo(500f));

        scheduleManager.SetupSchedule(secondRoute, 600f, 15f);

        Assert.That(scheduleManager.GetScheduledArrival(0), Is.EqualTo(600f));
        Assert.That(scheduleManager.GetScheduledArrival(1), Is.EqualTo(615f));
        Assert.That(scheduleManager.GetScheduledArrival(2), Is.EqualTo(0f));

        scheduleManager.currentTimeMinutes = 700f;
        Assert.That(scheduleManager.RecordArrival(99), Is.EqualTo(PunctualityStatus.OnTime));
    }

    [Test]
    public void ScoreTracker_AggregatesCategoryScores_AndReturnsExpectedStarRating()
    {
        var scoreTracker = CreateComponent<ScoreTracker>("ScoreTracker");

        scoreTracker.RecordStopArrival(PunctualityStatus.Early);
        scoreTracker.RecordCollision();
        scoreTracker.RecordRedLight();
        scoreTracker.UpdateEfficiencyScore(8f, 10f);
        scoreTracker.satisfactionScore = 80f;

        InvokePrivateMethod(scoreTracker, "UpdateTotalScore");

        Assert.That(scoreTracker.stopsCompleted, Is.EqualTo(1));
        Assert.That(scoreTracker.punctualityScore, Is.EqualTo(95f));
        Assert.That(scoreTracker.safetyScore, Is.EqualTo(75f));
        Assert.That(scoreTracker.efficiencyScore, Is.EqualTo(100f));
        Assert.That(scoreTracker.totalScore, Is.EqualTo(86.25f).Within(0.001f));
        Assert.That(scoreTracker.GetStarRating(), Is.EqualTo(4));
        Assert.That(scoreTracker.GetScoreSummary(), Does.Contain("Total: 86%"));
    }

    [Test]
    [TestCase(90f, 5)]
    [TestCase(89.99f, 4)]
    [TestCase(75f, 4)]
    [TestCase(74.99f, 3)]
    [TestCase(60f, 3)]
    [TestCase(59.99f, 2)]
    [TestCase(40f, 2)]
    [TestCase(39.99f, 1)]
    public void ScoreTracker_GetStarRating_UsesExpectedThresholds(float totalScore, int expectedStars)
    {
        var scoreTracker = CreateComponent<ScoreTracker>("ScoreTracker");
        scoreTracker.totalScore = totalScore;

        Assert.That(scoreTracker.GetStarRating(), Is.EqualTo(expectedStars));
    }

    [Test]
    public void ScoreTracker_UpdateEfficiencyScore_ClampsToExpectedRange()
    {
        var scoreTracker = CreateComponent<ScoreTracker>("ScoreTracker");

        scoreTracker.UpdateEfficiencyScore(0f, 10f);
        Assert.That(scoreTracker.efficiencyScore, Is.EqualTo(100f));

        scoreTracker.UpdateEfficiencyScore(20f, 10f);
        Assert.That(scoreTracker.efficiencyScore, Is.EqualTo(50f));

        scoreTracker.UpdateEfficiencyScore(1000f, 10f);
        Assert.That(scoreTracker.efficiencyScore, Is.EqualTo(1f).Within(0.001f));
    }

    [Test]
    public void ScoreTracker_GetScoreSummary_FormatsRoundedCategoryValues()
    {
        var scoreTracker = CreateComponent<ScoreTracker>("ScoreTracker");
        scoreTracker.totalScore = 84.6f;
        scoreTracker.punctualityScore = 94.2f;
        scoreTracker.satisfactionScore = 79.5f;
        scoreTracker.safetyScore = 74.6f;
        scoreTracker.efficiencyScore = 88.9f;

        Assert.That(
            scoreTracker.GetScoreSummary(),
            Is.EqualTo("Total: 85% | Punctuality: 94% | Satisfaction: 80% | Safety: 75% | Efficiency: 89%"));
    }

    [Test]
    public void ScoreTracker_Penalties_DoNotDropBelowZero()
    {
        var scoreTracker = CreateComponent<ScoreTracker>("ScoreTracker");

        for (int i = 0; i < 10; i++)
        {
            scoreTracker.RecordStopArrival(PunctualityStatus.SeverelyLate);
            scoreTracker.RecordCollision();
            scoreTracker.RecordRedLight();
        }

        Assert.That(scoreTracker.punctualityScore, Is.EqualTo(0f));
        Assert.That(scoreTracker.safetyScore, Is.EqualTo(0f));
    }

    [Test]
    public void MissionResult_Generate_UsesFallbackValues_WhenManagersAreMissing()
    {
        var result = MissionResult.Generate(null);

        Assert.That(result.routeName, Is.EqualTo("Unknown"));
        Assert.That(result.totalScore, Is.EqualTo(0f));
        Assert.That(result.starRating, Is.EqualTo(1));
        Assert.That(result.punctualityScore, Is.EqualTo(0f));
        Assert.That(result.satisfactionScore, Is.EqualTo(0f));
        Assert.That(result.safetyScore, Is.EqualTo(0f));
        Assert.That(result.efficiencyScore, Is.EqualTo(0f));
        Assert.That(result.totalPassengers, Is.EqualTo(0));
        Assert.That(result.totalFareKES, Is.EqualTo(0));
        Assert.That(result.totalDistanceKm, Is.EqualTo(0f));
        Assert.That(result.totalTimeMinutes, Is.EqualTo(0f));
        Assert.That(result.xpEarned, Is.EqualTo(0));
    }

    [Test]
    public void MissionResult_Generate_UsesLiveSessionState_AndCalculatesXp()
    {
        var route = ScriptableObject.CreateInstance<BusRoute>();
        route.routeName = "CBD - Westlands";

        var scoreTracker = CreateComponent<ScoreTracker>("ScoreTracker");
        SetSingleton(scoreTracker);
        scoreTracker.totalScore = 85f;
        scoreTracker.punctualityScore = 90f;
        scoreTracker.satisfactionScore = 80f;
        scoreTracker.safetyScore = 70f;
        scoreTracker.efficiencyScore = 75f;

        var passengerManager = CreateComponent<PassengerManager>("PassengerManager");
        SetSingleton(passengerManager);
        passengerManager.totalPassengersServed = 42;
        passengerManager.totalFareCollected = 123.6f;

        var freeDriveSession = CreateComponent<FreeDriveSession>("FreeDriveSession");
        SetSingleton(freeDriveSession);
        freeDriveSession.distanceDrivenKm = 12.5f;
        freeDriveSession.sessionTimeSeconds = 930f;

        var result = MissionResult.Generate(route);

        Assert.That(result.routeName, Is.EqualTo("CBD - Westlands"));
        Assert.That(result.totalScore, Is.EqualTo(85f));
        Assert.That(result.starRating, Is.EqualTo(4));
        Assert.That(result.punctualityScore, Is.EqualTo(90f));
        Assert.That(result.satisfactionScore, Is.EqualTo(80f));
        Assert.That(result.safetyScore, Is.EqualTo(70f));
        Assert.That(result.efficiencyScore, Is.EqualTo(75f));
        Assert.That(result.totalPassengers, Is.EqualTo(42));
        Assert.That(result.totalFareKES, Is.EqualTo(124));
        Assert.That(result.totalDistanceKm, Is.EqualTo(12.5f));
        Assert.That(result.totalTimeMinutes, Is.EqualTo(15.5f));
        Assert.That(result.xpEarned, Is.EqualTo(510));
    }

    [Test]
    public void MissionResult_Generate_UsesMissionSettlementBreakdown_WhenAvailable()
    {
        var route = ScriptableObject.CreateInstance<BusRoute>();
        route.routeName = "Depot Loop";

        var gameState = CreateComponent<GameState>("GameState");
        SetSingleton(gameState);
        gameState.lastMissionSettlement.grossEarningsKES = 5000f;
        gameState.lastMissionSettlement.fuelRefuelCostKES = 1200f;
        gameState.lastMissionSettlement.maintenanceCostKES = 800f;
        gameState.lastMissionSettlement.netEarningsKES = 3000f;

        var passengerManager = CreateComponent<PassengerManager>("PassengerManager");
        SetSingleton(passengerManager);
        passengerManager.totalFareCollected = 5000f;

        var result = MissionResult.Generate(route);

        Assert.That(result.totalFareKES, Is.EqualTo(5000));
        Assert.That(result.fuelRefuelCostKES, Is.EqualTo(1200));
        Assert.That(result.maintenanceCostKES, Is.EqualTo(800));
        Assert.That(result.netEarningsKES, Is.EqualTo(3000));
    }

    [Test]
    public void MissionResult_Generate_IncludesDynamicEventSummary_WhenAvailable()
    {
        var route = ScriptableObject.CreateInstance<BusRoute>();
        route.routeName = "Eventful Route";

        var eventSystem = CreateComponent<DynamicEventSystem>("DynamicEventSystem");
        SetSingleton(eventSystem);

        var completed = GetPrivateField<List<DynamicEventRecord>>(eventSystem, "completedEvents");
        completed.Add(new DynamicEventRecord
        {
            type = DynamicEventType.PoliceCheckpoint,
            title = "CHECKPOINT",
            description = "Mandatory slow zone",
            triggered = true,
            resolved = true,
            complianceObserved = true
        });

        var result = MissionResult.Generate(route);

        Assert.That(result.dynamicEventsTriggered, Is.EqualTo(1));
        Assert.That(result.dynamicEventSummary, Does.Contain("CHECKPOINT OK"));
    }

    [Test]
    public void ScoreTracker_ApplyDynamicEventImpact_ReducesExpectedCategories()
    {
        var scoreTracker = CreateComponent<ScoreTracker>("ScoreTracker");

        scoreTracker.ApplyDynamicEventImpact(6f, 4f, "CHECKPOINT", true, 3f);

        Assert.That(scoreTracker.safetyScore, Is.EqualTo(94f));
        Assert.That(scoreTracker.punctualityScore, Is.EqualTo(96f));
        Assert.That(scoreTracker.efficiencyScore, Is.EqualTo(97f));
        Assert.That(scoreTracker.totalScore, Is.LessThan(100f));
    }

    [Test]
    public void FreeDriveSession_StartAndEndSession_ResetAndToggleState()
    {
        var session = CreateComponent<FreeDriveSession>("FreeDriveSession");
        session.sessionActive = false;
        session.distanceDrivenKm = 42f;
        session.sessionTimeSeconds = 99f;
        session.fuelLevel = 10f;

        session.StartSession();

        Assert.That(session.sessionActive, Is.True);
        Assert.That(session.distanceDrivenKm, Is.EqualTo(0f));
        Assert.That(session.sessionTimeSeconds, Is.EqualTo(0f));
        Assert.That(session.fuelLevel, Is.EqualTo(100f));

        session.EndSession();

        Assert.That(session.sessionActive, Is.False);
    }

    [Test]
    public void FreeDriveSession_FormatsElapsedTime_AsMinutesAndSeconds()
    {
        var session = CreateComponent<FreeDriveSession>("FreeDriveSession");
        session.sessionTimeSeconds = 125f;

        Assert.That(session.GetFormattedTime(), Is.EqualTo("02:05"));
    }

    [Test]
    public void FreeDriveSession_ReturnsDefaultGpsString_WhenDependenciesAreMissing()
    {
        var session = CreateComponent<FreeDriveSession>("FreeDriveSession");

        Assert.That(session.GetCurrentGPSString(), Is.EqualTo("0.0000, 0.0000"));
    }

    [Test]
    public void PassengerData_TotalCapacity_AddsSeatedAndStandingCapacity()
    {
        var passengerData = ScriptableObject.CreateInstance<PassengerData>();
        passengerData.seatedCapacity = 40;
        passengerData.standingCapacity = 30;

        Assert.That(passengerData.totalCapacity, Is.EqualTo(70));
    }

    [Test]
    public void PassengerManager_GetProfit_SubtractsFuelCostFromIncome()
    {
        var passengerManager = CreateComponent<PassengerManager>("PassengerManager");
        passengerManager.sessionIncome = 1500f;
        passengerManager.totalDistanceKm = 20f;
        passengerManager.fuelCostPerKm = 12f;

        Assert.That(passengerManager.GetProfit(), Is.EqualTo(1260f));
    }

    [Test]
    public void GameState_SelectCountry_ResetsCityAndRoute()
    {
        var gameState = CreateComponent<GameState>("GameState");
        var country = ScriptableObject.CreateInstance<CountryDefinition>();
        country.countryName = "Kenya";
        gameState.selectedCity = ScriptableObject.CreateInstance<CityDefinition>();
        gameState.selectedRoute = ScriptableObject.CreateInstance<BusRoute>();

        gameState.SelectCountry(country);

        Assert.That(gameState.selectedCountry, Is.SameAs(country));
        Assert.That(gameState.selectedCity, Is.Null);
        Assert.That(gameState.selectedRoute, Is.Null);
    }

    [Test]
    public void GameState_SelectCity_SetsCityAndClearsRoute()
    {
        var gameState = CreateComponent<GameState>("GameState");
        var city = ScriptableObject.CreateInstance<CityDefinition>();
        city.cityName = "Nairobi";
        gameState.selectedRoute = ScriptableObject.CreateInstance<BusRoute>();

        gameState.SelectCity(city);

        Assert.That(gameState.selectedCity, Is.SameAs(city));
        Assert.That(gameState.selectedRoute, Is.Null);
    }

    [Test]
    public void GameState_SelectRoute_SetsSelectedRoute()
    {
        var gameState = CreateComponent<GameState>("GameState");
        var route = ScriptableObject.CreateInstance<BusRoute>();
        route.routeName = "CBD Loop";

        gameState.SelectRoute(route);

        Assert.That(gameState.selectedRoute, Is.SameAs(route));
    }

    [Test]
    public void GameState_GetFuelPercent_UsesPersistentVehicleState()
    {
        var gameState = CreateComponent<GameState>("GameState");
        gameState.vehicleState.fuelCapacityLitres = 300f;
        gameState.vehicleState.fuelLitres = 60f;

        Assert.That(gameState.GetFuelPercent(), Is.EqualTo(20f));
    }

    [Test]
    public void MaintenanceSystem_GetWheelGripMultiplier_DropsAfterHalfWear()
    {
        var gameState = CreateComponent<GameState>("GameState");
        SetSingleton(gameState);
        gameState.vehicleState.axleTyreWearNormalized = new[] { 0.25f, 0.5f, 0.8f };

        var maintenanceSystem = CreateComponent<MaintenanceSystem>("MaintenanceSystem");

        Assert.That(maintenanceSystem.GetWheelGripMultiplier(0), Is.EqualTo(1f));
        Assert.That(maintenanceSystem.GetWheelGripMultiplier(4), Is.LessThan(1f));
    }

    [Test]
    public void DriverShiftSystem_PrepareRoute_StartsShift_Inspection_AndRota()
    {
        var gameState = CreateComponent<GameState>("GameState");
        SetSingleton(gameState);
        var shiftSystem = gameState.gameObject.AddComponent<DriverShiftSystem>();

        var route = ScriptableObject.CreateInstance<BusRoute>();
        route.estimatedTimeMinutes = 25f;

        shiftSystem.PrepareRoute(route, null);

        Assert.That(shiftSystem.shiftActive, Is.True);
        Assert.That(shiftSystem.inspectionCompleted, Is.True);
        Assert.That(shiftSystem.rotaSignedIn, Is.True);
        Assert.That(shiftSystem.plannedRouteCount, Is.InRange(2, 4));
    }

    [Test]
    public void DriverShiftSystem_CompleteRoute_BuildsShiftSummary_WhenShiftEnds()
    {
        var gameState = CreateComponent<GameState>("GameState");
        SetSingleton(gameState);
        var shiftSystem = gameState.gameObject.AddComponent<DriverShiftSystem>();

        var route = ScriptableObject.CreateInstance<BusRoute>();
        route.estimatedTimeMinutes = 20f;

        shiftSystem.PrepareRoute(route, null);
        shiftSystem.plannedRouteCount = 1;
        gameState.vehicleState.totalFuelConsumedLitres = 6f;

        var result = new MissionResult
        {
            routeName = "Depot Loop",
            punctualityScore = 88f,
            satisfactionScore = 84f,
            safetyScore = 92f,
            efficiencyScore = 81f,
            totalPassengers = 54,
            totalFareKES = 4200,
            netEarningsKES = 3100,
            totalDistanceKm = 18f,
            totalTimeMinutes = 23f
        };

        shiftSystem.CompleteRoute(route, result, true);
        bool applied = shiftSystem.TryApplyShiftSummary(result);

        Assert.That(shiftSystem.shiftCompleted, Is.True);
        Assert.That(applied, Is.True);
        Assert.That(result.isShiftSummary, Is.True);
        Assert.That(result.shiftRoutesCompleted, Is.EqualTo(1));
        Assert.That(result.shiftFuelConsumedLitres, Is.EqualTo(6f));
    }

    [Test]
    public void MissionResult_Generate_AppliesViolationPenalty_AndLowSatisfactionStarCap()
    {
        var gameState = CreateComponent<GameState>("GameState");
        SetSingleton(gameState);

        var route = ScriptableObject.CreateInstance<BusRoute>();
        route.routeName = "CBD";

        var scoreTracker = CreateComponent<ScoreTracker>("ScoreTracker");
        SetSingleton(scoreTracker);
        scoreTracker.totalScore = 92f;
        scoreTracker.punctualityScore = 90f;
        scoreTracker.satisfactionScore = 55f;
        scoreTracker.safetyScore = 92f;
        scoreTracker.efficiencyScore = 91f;

        var violationSystem = CreateComponent<ExtendedTrafficViolationSystem>("ViolationSystem");
        SetSingleton(violationSystem);
        violationSystem.RecordSignalViolation(false);

        var result = MissionResult.Generate(route);

        Assert.That(result.totalViolations, Is.GreaterThanOrEqualTo(1));
        Assert.That(result.starRating, Is.LessThanOrEqualTo(2));
        Assert.That(result.driverReputationRating, Is.LessThan(100f));
    }

    [Test]
    public void CityManager_GetCitiesForCountry_ReturnsCountryCities_OrEmptyArray()
    {
        var cityManager = CreateComponent<CityManager>("CityManager");
        var cityA = ScriptableObject.CreateInstance<CityDefinition>();
        var cityB = ScriptableObject.CreateInstance<CityDefinition>();
        var country = ScriptableObject.CreateInstance<CountryDefinition>();
        country.cities = new[] { cityA, cityB };

        var cities = cityManager.GetCitiesForCountry(country);
        var noCities = cityManager.GetCitiesForCountry(null);

        Assert.That(cities, Is.EqualTo(new[] { cityA, cityB }));
        Assert.That(noCities, Is.Empty);
    }

    [Test]
    public void CityManager_GetCountriesByContinent_FiltersAndIgnoresNullEntries()
    {
        var cityManager = CreateComponent<CityManager>("CityManager");
        var kenya = ScriptableObject.CreateInstance<CountryDefinition>();
        kenya.countryName = "Kenya";
        kenya.continent = "Africa";
        var japan = ScriptableObject.CreateInstance<CountryDefinition>();
        japan.countryName = "Japan";
        japan.continent = "Asia";
        var ghana = ScriptableObject.CreateInstance<CountryDefinition>();
        ghana.countryName = "Ghana";
        ghana.continent = "Africa";
        cityManager.allCountries = new CountryDefinition[] { kenya, null, japan, ghana };

        var africanCountries = cityManager.GetCountriesByContinent("Africa");
        var europeanCountries = cityManager.GetCountriesByContinent("Europe");

        Assert.That(africanCountries, Is.EqualTo(new[] { kenya, ghana }));
        Assert.That(europeanCountries, Is.Empty);
    }

    [Test]
    public void CityDefinition_BuildsStreamingAssetsPaths_FromConfiguredValues()
    {
        var city = ScriptableObject.CreateInstance<CityDefinition>();
        city.cityCode = "NBO";
        city.roadsFileName = "roads.xml";
        city.buildingsFileName = "buildings.xml";

        var expectedRoads = System.IO.Path.Combine(Application.streamingAssetsPath, "Cities", "NBO", "roads.xml");
        var expectedBuildings = System.IO.Path.Combine(Application.streamingAssetsPath, "Cities", "NBO", "buildings.xml");

        Assert.That(city.GetRoadsPath(), Is.EqualTo(expectedRoads));
        Assert.That(city.GetBuildingsPath(), Is.EqualTo(expectedBuildings));
    }

    [Test]
    public void CityDefinition_SeasonChecks_UseConfiguredMonthLists()
    {
        var city = ScriptableObject.CreateInstance<CityDefinition>();
        int currentMonth = System.DateTime.Now.Month;
        int nextMonth = currentMonth == 12 ? 1 : currentMonth + 1;

        city.rainySeasonMonths = new[] { currentMonth };
        city.snowSeasonMonths = new[] { nextMonth };

        Assert.That(city.IsRainySeason(), Is.True);
        Assert.That(city.IsSnowSeason(), Is.False);

        city.snowSeasonMonths = new[] { currentMonth };
        Assert.That(city.IsSnowSeason(), Is.True);
    }

    [Test]
    public void CityDefinition_SeasonChecks_ReturnFalse_ForNullOrEmptyMonths()
    {
        var city = ScriptableObject.CreateInstance<CityDefinition>();
        city.rainySeasonMonths = null;
        city.snowSeasonMonths = new int[0];

        Assert.That(city.IsRainySeason(), Is.False);
        Assert.That(city.IsSnowSeason(), Is.False);
    }

    [Test]
    public void AIRoadGraph_GetRandomNodeIndex_SkipsDeadEnds_WhenOutgoingConnectionRequired()
    {
        var graph = CreateLinearAirRoadGraph();
        graph.nodes[2].nextNodeIndices = new int[0];

        int picked = graph.GetRandomNodeIndex(requireOutgoingConnection: true, maxAttempts: 32);

        Assert.That(picked, Is.EqualTo(0).Or.EqualTo(1));
        Assert.That(graph.HasOutgoingConnection(picked), Is.True);
    }

    [Test]
    public void AIRoadGraph_BuildRandomNodeSequence_ReturnsConnectedValidNodes()
    {
        var graph = CreateLinearAirRoadGraph();

        int[] sequence = graph.BuildRandomNodeSequence(4, 0);

        Assert.That(sequence, Has.Length.EqualTo(4));
        Assert.That(sequence[0], Is.EqualTo(0));
        for (int i = 0; i < sequence.Length; i++)
            Assert.That(graph.IsValidNode(sequence[i]), Is.True);
        for (int i = 0; i < sequence.Length - 1; i++)
            Assert.That(graph.nodes[sequence[i]].nextNodeIndices, Does.Contain(sequence[i + 1]));
    }

    [Test]
    public void AIRoadGraph_TryFindNodePath_AvoidsBlockedNodes_WhenAlternateExists()
    {
        var graph = CreateComponent<AIRoadGraph>("AIRoadGraph");
        graph.nodes = new AIRoadGraph.RoadNode[4];

        for (int i = 0; i < graph.nodes.Length; i++)
        {
            var point = new GameObject($"GraphNode_{i}").transform;
            point.position = i switch
            {
                0 => new Vector3(0f, 0f, 0f),
                1 => new Vector3(10f, 0f, 0f),
                2 => new Vector3(0f, 0f, 10f),
                _ => new Vector3(10f, 0f, 10f)
            };

            graph.nodes[i] = new AIRoadGraph.RoadNode
            {
                id = $"N{i}",
                point = point,
                nextNodeIndices = i switch
                {
                    0 => new[] { 1, 2 },
                    1 => new[] { 3 },
                    2 => new[] { 3 },
                    _ => System.Array.Empty<int>()
                }
            };
        }

        bool found = graph.TryFindNodePath(0, 3, out var path, new HashSet<int> { 1 });

        Assert.That(found, Is.True);
        Assert.That(path, Is.EqualTo(new[] { 0, 2, 3 }));
    }

    [Test]
    public void PassengerSpawner_BuildRandomStopsFromRoadGraph_CreatesSyntheticStopsFromGraphNodes()
    {
        var graph = CreateLinearAirRoadGraph();
        var converter = CreateComponent<CoordinateConverter>("CoordinateConverter");
        converter.mapOrigin = ScriptableObject.CreateInstance<MapOrigin>();
        converter.mapOrigin.originLat = -1.2864;
        converter.mapOrigin.originLon = 36.8172;

        var passengerSpawner = CreateComponent<PassengerSpawner>("PassengerSpawner");
        passengerSpawner.roadGraph = graph;
        passengerSpawner.coordinateConverter = converter;
        passengerSpawner.generatedStopNamePrefix = "Synthetic";
        passengerSpawner.generatedStopWaitTimeSeconds = 12f;

        BusStopData[] stops = passengerSpawner.BuildRandomStopsFromRoadGraph(3, 0);

        Assert.That(stops, Has.Length.EqualTo(3));
        Assert.That(stops[0].stopName, Is.EqualTo("Synthetic 0000"));
        Assert.That(stops[0].waitTimeSeconds, Is.EqualTo(12f));
        for (int i = 0; i < stops.Length; i++)
        {
            Assert.That(stops[i].stopName, Does.StartWith("Synthetic "));
            Assert.That(stops[i].latitude, Is.Not.EqualTo(0d));
            Assert.That(stops[i].longitude, Is.Not.EqualTo(0d));
        }
    }

    [Test]
    public void PedestrianSpawner_BuildRandomCrossingsFromRoadGraph_GeneratesRuntimeCrossings()
    {
        var graph = CreateLinearAirRoadGraph();
        var trafficLight = CreateComponent<TrafficLight>("TrafficLight");
        graph.nodes[1].trafficLight = trafficLight;

        var pedestrianSpawner = CreateComponent<PedestrianSpawner>("PedestrianSpawner");
        pedestrianSpawner.roadGraph = graph;
        pedestrianSpawner.generatedCrossingCount = 2;
        pedestrianSpawner.crossingHalfWidth = 3f;
        pedestrianSpawner.useRandomGeneratedCrossings = true;

        var generated = (PedestrianSpawner.ZebraCrossing[])InvokePrivateMethodWithResult(
            pedestrianSpawner, "BuildRandomCrossingsFromRoadGraph");

        Assert.That(generated, Has.Length.EqualTo(2));
        for (int i = 0; i < generated.Length; i++)
        {
            Assert.That(generated[i].crossingId, Does.StartWith("RGX_"));
            Assert.That(generated[i].spawnA, Is.Not.Null);
            Assert.That(generated[i].spawnB, Is.Not.Null);
            Assert.That(Vector3.Distance(generated[i].spawnA.position, generated[i].spawnB.position), Is.GreaterThan(0.1f));
        }
        Assert.That(generated[0].controllingTrafficLight, Is.SameAs(trafficLight));
    }

    [Test]
    public void MissionData_DefaultValues_AreInitializedAsExpected()
    {
        var missionData = ScriptableObject.CreateInstance<MissionData>();

        Assert.That(missionData.scheduledDepartureTime, Is.EqualTo(480f));
        Assert.That(missionData.targetDurationMinutes, Is.EqualTo(25f));
        Assert.That(missionData.minPassengersTarget, Is.EqualTo(30));
        Assert.That(missionData.stretchPassengersTarget, Is.EqualTo(45));
        Assert.That(missionData.punctualityTarget, Is.EqualTo(0.8f));
        Assert.That(missionData.baseXP, Is.EqualTo(200));
        Assert.That(missionData.difficultyMultiplier, Is.EqualTo(1f));
        Assert.That(missionData.timeBonusXP, Is.EqualTo(50));
        Assert.That(missionData.missionName, Is.EqualTo("CBD to Westlands"));
        Assert.That(missionData.description, Is.EqualTo("Complete the full route on time."));
        Assert.That(missionData.starRating, Is.EqualTo(1));
    }

    [Test]
    public void BusRoute_AndBusStopData_Defaults_AreInitialized()
    {
        var route = ScriptableObject.CreateInstance<BusRoute>();
        var stop = new BusStopData();

        Assert.That(route.routeNumber, Is.EqualTo("1"));
        Assert.That(route.routeName, Is.EqualTo("CBD - Westlands"));
        Assert.That(route.baseFare, Is.EqualTo(50f));
        Assert.That(stop.waitTimeSeconds, Is.EqualTo(10f));
    }

    [Test]
    public void TrafficLight_AdvanceState_CyclesRedGreenAmberAndQueriesMatch()
    {
        var trafficLight = CreateComponent<TrafficLight>("TrafficLight");

        Assert.That(trafficLight.IsRed(), Is.True);
        Assert.That(trafficLight.IsGreen(), Is.False);

        InvokePrivateMethod(trafficLight, "AdvanceState");
        Assert.That(trafficLight.currentState, Is.EqualTo(TrafficLight.LightState.Green));
        Assert.That(trafficLight.IsGreen(), Is.True);
        Assert.That(trafficLight.IsRed(), Is.False);

        InvokePrivateMethod(trafficLight, "AdvanceState");
        Assert.That(trafficLight.currentState, Is.EqualTo(TrafficLight.LightState.Amber));

        InvokePrivateMethod(trafficLight, "AdvanceState");
        Assert.That(trafficLight.currentState, Is.EqualTo(TrafficLight.LightState.Red));
    }

    [Test]
    [TestCase(TrafficLight.LightState.Red, 45f)]
    [TestCase(TrafficLight.LightState.Green, 35f)]
    [TestCase(TrafficLight.LightState.Amber, 5f)]
    public void TrafficLight_GetStateDuration_ReturnsConfiguredDuration(TrafficLight.LightState state, float expectedDuration)
    {
        var trafficLight = CreateComponent<TrafficLight>("TrafficLight");
        trafficLight.redDuration = 45f;
        trafficLight.greenDuration = 35f;
        trafficLight.amberDuration = 5f;

        var duration = (float)InvokePrivateMethodWithResult(trafficLight, "GetStateDuration", state);

        Assert.That(duration, Is.EqualTo(expectedDuration));
    }

    [Test]
    public void TrafficLight_UpdateVisuals_LightsOnlyTheActiveRenderer()
    {
        var trafficLight = CreateComponent<TrafficLight>("TrafficLight");
        trafficLight.redLight = CreateRenderer("RedLight");
        trafficLight.greenLight = CreateRenderer("GreenLight");
        trafficLight.amberLight = CreateRenderer("AmberLight");
        trafficLight.currentState = TrafficLight.LightState.Green;
        const string materialInstantiationMessage =
            "Instantiating material due to calling renderer.material during edit mode. This will leak materials into the scene. You most likely want to use renderer.sharedMaterial instead.";

        LogAssert.Expect(LogType.Error, materialInstantiationMessage);
        LogAssert.Expect(LogType.Error, materialInstantiationMessage);
        LogAssert.Expect(LogType.Error, materialInstantiationMessage);

        InvokePrivateMethod(trafficLight, "UpdateVisuals");

        AssertColorApproximately(trafficLight.redLight.material.color, TrafficLight.Unlit);
        AssertColorApproximately(trafficLight.greenLight.material.color, TrafficLight.LitGreen);
        AssertColorApproximately(trafficLight.amberLight.material.color, TrafficLight.Unlit);
    }

    [Test]
    public void Json_Deserialize_ParsesNestedDataAndEscapedText()
    {
        const string json = "{\"name\":\"Nairobi\",\"active\":true,\"count\":3,\"ratio\":1.5,\"items\":[1,\"two\",null],\"nested\":{\"escaped\":\"Line\\nBreak\",\"unicode\":\"\\u263a\"}}";

        var result = Json.Deserialize(json) as Dictionary<string, object>;

        Assert.That(result, Is.Not.Null);
        Assert.That(result["name"], Is.EqualTo("Nairobi"));
        Assert.That(result["active"], Is.EqualTo(true));
        Assert.That(result["count"], Is.EqualTo(3L));
        Assert.That(result["ratio"], Is.EqualTo(1.5d));

        var items = result["items"] as List<object>;
        Assert.That(items, Is.Not.Null);
        Assert.That(items.Count, Is.EqualTo(3));
        Assert.That(items[0], Is.EqualTo(1L));
        Assert.That(items[1], Is.EqualTo("two"));
        Assert.That(items[2], Is.Null);

        var nested = result["nested"] as Dictionary<string, object>;
        Assert.That(nested, Is.Not.Null);
        Assert.That(nested["escaped"], Is.EqualTo("Line\nBreak"));
        Assert.That(nested["unicode"], Is.EqualTo("\u263a"));
    }

    [Test]
    public void Json_Deserialize_ReturnsNull_ForNullOrInvalidJson()
    {
        Assert.That(Json.Deserialize(null), Is.Null);
        Assert.That(Json.Deserialize("{invalid json"), Is.Null);
    }

    [Test]
    public void Json_Serialize_ProducesCompactJson_ForCollectionsAndObjects()
    {
        var payload = new Dictionary<string, object>
        {
            ["text"] = "Hello\nWorld",
            ["number"] = 2.5f,
            ["items"] = new ArrayList { 1, true, null },
            ["object"] = new SerializablePayload { Name = "Bus", Count = 2 }
        };

        var serialized = Json.Serialize(payload);

        Assert.That(serialized, Does.Contain("\"text\":\"Hello\\nWorld\""));
        Assert.That(serialized, Does.Contain("\"number\":2.5"));
        Assert.That(serialized, Does.Contain("\"items\":[1,true,null]"));
        Assert.That(serialized, Does.Contain("\"object\":{\"Name\":\"Bus\",\"Count\":2}"));
    }

    [Test]
    public void Json_Serialize_PrettyFormatsWithCustomIndent_AndHandlesEmptyCollections()
    {
        var payload = new Dictionary<string, object>
        {
            ["emptyArray"] = new ArrayList(),
            ["emptyObject"] = new Dictionary<string, object>(),
            ["value"] = "x"
        };

        var serialized = Json.Serialize(payload, true, "    ");

        Assert.That(serialized, Does.Contain("\n    \"emptyArray\": []"));
        Assert.That(serialized, Does.Contain("\n    \"emptyObject\": {}"));
        Assert.That(serialized, Does.Contain("\n    \"value\": \"x\""));
    }

    [Test]
    public void UITheme_Helpers_ReturnExpectedColorsAndFontFallback()
    {
        var alphaColor = UITheme.WithAlpha(UITheme.Accent, 0.25f);
        var parsed = UITheme.HEX("#FF9159");

        Assert.That(alphaColor.a, Is.EqualTo(0.25f).Within(0.001f));
        Assert.That(parsed, Is.EqualTo(UITheme.Accent));
        Assert.That(UITheme.GetContinentColor("Kenya"), Is.EqualTo(UITheme.Africa));
        Assert.That(UITheme.GetContinentColor("Japan"), Is.EqualTo(UITheme.Asia));
        Assert.That(UITheme.GetContinentColor("Unknown"), Is.EqualTo(UITheme.Accent));
        Assert.That(UITheme.GetFont(UITheme.FontWeight.Bold), Is.Null);
    }

    [Test]
    [TestCase(0, WeatherState.Clear)]
    [TestCase(2, WeatherState.PartlyCloudy)]
    [TestCase(3, WeatherState.Overcast)]
    [TestCase(45, WeatherState.Fog)]
    [TestCase(53, WeatherState.Drizzle)]
    [TestCase(62, WeatherState.LightRain)]
    [TestCase(65, WeatherState.HeavyRain)]
    [TestCase(73, WeatherState.Snow)]
    [TestCase(81, WeatherState.LightRain)]
    [TestCase(96, WeatherState.Thunderstorm)]
    [TestCase(999, WeatherState.Clear)]
    public void WeatherSystem_WmoCodeMapping_ReturnsExpectedState(int code, WeatherState expected)
    {
        var weatherSystem = CreateComponent<WeatherSystem>("WeatherSystem");

        var result = (WeatherState)InvokePrivateMethodWithResult(weatherSystem, "WMOCodeToWeatherState", code);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    [TestCase(WeatherState.Clear, 0f)]
    [TestCase(WeatherState.PartlyCloudy, 0.2f)]
    [TestCase(WeatherState.Overcast, 0.4f)]
    [TestCase(WeatherState.Fog, 0.7f)]
    [TestCase(WeatherState.Drizzle, 0.3f)]
    [TestCase(WeatherState.LightRain, 0.5f)]
    [TestCase(WeatherState.HeavyRain, 0.85f)]
    [TestCase(WeatherState.Thunderstorm, 1f)]
    [TestCase(WeatherState.Snow, 0.6f)]
    [TestCase(WeatherState.Blizzard, 1f)]
    public void WeatherSystem_IntensityMapping_ReturnsExpectedValues(WeatherState state, float expected)
    {
        var weatherSystem = CreateComponent<WeatherSystem>("WeatherSystem");

        var result = (float)InvokePrivateMethodWithResult(weatherSystem, "GetIntensityForState", state);

        Assert.That(result, Is.EqualTo(expected).Within(0.001f));
    }

    [Test]
    public void WeatherSystem_ApplyWeather_UpdatesCurrentStateFields()
    {
        var weatherSystem = CreateComponent<WeatherSystem>("WeatherSystem");

        weatherSystem.ApplyWeather(WeatherState.HeavyRain, 0.85f, 14f);

        Assert.That(weatherSystem.currentWeather, Is.EqualTo(WeatherState.HeavyRain));
        Assert.That(weatherSystem.weatherIntensity, Is.EqualTo(0.85f));
        Assert.That(weatherSystem.temperature, Is.EqualTo(14f));
    }

    [Test]
    public void WeatherSystem_ApplySimulatedWeather_FallsBackToClear_WhenCityMissing()
    {
        var weatherSystem = CreateComponent<WeatherSystem>("WeatherSystem");
        SetPrivateField(weatherSystem, "activeCity", null);

        InvokePrivateMethod(weatherSystem, "ApplySimulatedWeather");

        Assert.That(weatherSystem.currentWeather, Is.EqualTo(WeatherState.Clear));
        Assert.That(weatherSystem.weatherIntensity, Is.EqualTo(0f));
        Assert.That(weatherSystem.temperature, Is.EqualTo(20f));
    }

    [Test]
    [TestCase(ClimateZone.Desert, false, false, false)]
    [TestCase(ClimateZone.Temperate, false, true, false)]
    [TestCase(ClimateZone.Tropical, true, false, false)]
    [TestCase(ClimateZone.Tropical, false, false, false)]
    [TestCase(ClimateZone.Subtropical, true, false, true)]
    public void WeatherSystem_SimulateWeatherForCity_ReturnsOnlyAllowedStatesForItsClimateRules(
        ClimateZone climateZone,
        bool rainySeason,
        bool snowSeason,
        bool hasMonsoon)
    {
        var weatherSystem = CreateComponent<WeatherSystem>("WeatherSystem");
        var city = ScriptableObject.CreateInstance<CityDefinition>();
        city.climateZone = climateZone;
        city.hasMonsoon = hasMonsoon;
        int currentMonth = System.DateTime.Now.Month;
        int otherMonth = currentMonth == 12 ? 1 : currentMonth + 1;
        city.rainySeasonMonths = rainySeason ? new[] { currentMonth } : new[] { otherMonth };
        city.snowSeasonMonths = snowSeason ? new[] { currentMonth } : new[] { otherMonth };

        var result = (WeatherState)InvokePrivateMethodWithResult(weatherSystem, "SimulateWeatherForCity", city);

        switch (climateZone)
        {
            case ClimateZone.Desert:
                Assert.That(
                    result == WeatherState.LightRain ||
                    result == WeatherState.Fog ||
                    result == WeatherState.PartlyCloudy ||
                    result == WeatherState.Clear,
                    Is.True);
                break;

            case ClimateZone.Temperate:
                Assert.That(
                    result == WeatherState.Snow ||
                    result == WeatherState.Overcast ||
                    result == WeatherState.LightRain ||
                    result == WeatherState.Clear,
                    Is.True);
                break;

            case ClimateZone.Tropical:
                if (rainySeason)
                {
                    Assert.That(
                        result == WeatherState.HeavyRain ||
                        result == WeatherState.LightRain ||
                        result == WeatherState.Overcast ||
                        result == WeatherState.PartlyCloudy,
                        Is.True);
                }
                else
                {
                    Assert.That(
                        result == WeatherState.LightRain ||
                        result == WeatherState.PartlyCloudy ||
                        result == WeatherState.Clear,
                        Is.True);
                    Assert.That(result, Is.Not.EqualTo(WeatherState.HeavyRain));
                }
                break;

            case ClimateZone.Subtropical:
                Assert.That(
                    result == WeatherState.HeavyRain ||
                    result == WeatherState.Thunderstorm ||
                    result == WeatherState.Overcast,
                    Is.True);
                break;
        }
    }

    [Test]
    public void WeatherSystem_ParseAndApplyWeather_DowngradesSnow_WhenCityOutOfSeason()
    {
        var weatherSystem = CreateComponent<WeatherSystem>("WeatherSystem");
        var city = ScriptableObject.CreateInstance<CityDefinition>();
        int currentMonth = System.DateTime.Now.Month;
        int otherMonth = currentMonth == 12 ? 1 : currentMonth + 1;
        city.snowSeasonMonths = new[] { otherMonth };
        SetPrivateField(weatherSystem, "activeCity", city);

        InvokePrivateMethod(weatherSystem, "ParseAndApplyWeather", "{\"weathercode\":73,\"temperature_2m\":5}");

        Assert.That(weatherSystem.currentWeather, Is.EqualTo(WeatherState.HeavyRain));
        Assert.That(weatherSystem.temperature, Is.EqualTo(5f).Within(0.001f));
        Assert.That(weatherSystem.weatherIntensity, Is.EqualTo(0.85f).Within(0.001f));
    }

    [Test]
    public void WeatherSystem_ParseAndApplyWeather_KeepsSnow_WhenCityInSeason()
    {
        var weatherSystem = CreateComponent<WeatherSystem>("WeatherSystem");
        var city = ScriptableObject.CreateInstance<CityDefinition>();
        int currentMonth = System.DateTime.Now.Month;
        city.snowSeasonMonths = new[] { currentMonth };
        SetPrivateField(weatherSystem, "activeCity", city);

        InvokePrivateMethod(weatherSystem, "ParseAndApplyWeather", "{\"weathercode\":73,\"temperature_2m\":-2.5}");

        Assert.That(weatherSystem.currentWeather, Is.EqualTo(WeatherState.Snow));
        Assert.That(weatherSystem.temperature, Is.EqualTo(-2.5f).Within(0.001f));
        Assert.That(weatherSystem.weatherIntensity, Is.EqualTo(0.6f).Within(0.001f));
    }

    [Test]
    public void WeatherSystem_ExtractHelpers_ParseNumbersAndFallbackToZero()
    {
        var weatherSystem = CreateComponent<WeatherSystem>("WeatherSystem");
        const string json = "{\"weathercode\":-45,\"temperature_2m\":23.75}";

        var parsedInt = (int)InvokePrivateMethodWithResult(weatherSystem, "ExtractInt", json, "weathercode");
        var parsedFloat = (float)InvokePrivateMethodWithResult(weatherSystem, "ExtractFloat", json, "temperature_2m");
        var missingInt = (int)InvokePrivateMethodWithResult(weatherSystem, "ExtractInt", json, "missing");
        var missingFloat = (float)InvokePrivateMethodWithResult(weatherSystem, "ExtractFloat", json, "missing");

        Assert.That(parsedInt, Is.EqualTo(-45));
        Assert.That(parsedFloat, Is.EqualTo(23.75f).Within(0.001f));
        Assert.That(missingInt, Is.EqualTo(0));
        Assert.That(missingFloat, Is.EqualTo(0f));
    }

    [Test]
    [TestCase(WeatherState.Drizzle, true, false)]
    [TestCase(WeatherState.LightRain, true, false)]
    [TestCase(WeatherState.HeavyRain, true, false)]
    [TestCase(WeatherState.Thunderstorm, true, false)]
    [TestCase(WeatherState.Snow, false, true)]
    [TestCase(WeatherState.Blizzard, false, true)]
    [TestCase(WeatherState.Clear, false, false)]
    public void WeatherSystem_PrecipitationFlags_MatchCurrentWeather(WeatherState state, bool isRaining, bool isSnowing)
    {
        var weatherSystem = CreateComponent<WeatherSystem>("WeatherSystem");
        weatherSystem.currentWeather = state;

        Assert.That(weatherSystem.IsRaining(), Is.EqualTo(isRaining));
        Assert.That(weatherSystem.IsSnowing(), Is.EqualTo(isSnowing));
    }

    [Test]
    [TestCase(WeatherState.LightRain, 0.85f)]
    [TestCase(WeatherState.HeavyRain, 0.70f)]
    [TestCase(WeatherState.Thunderstorm, 0.65f)]
    [TestCase(WeatherState.Snow, 0.50f)]
    [TestCase(WeatherState.Blizzard, 0.35f)]
    [TestCase(WeatherState.Fog, 0.90f)]
    [TestCase(WeatherState.Clear, 1.0f)]
    public void WeatherSystem_GetRoadGripMultiplier_ReturnsExpectedValues(WeatherState state, float expectedGrip)
    {
        var weatherSystem = CreateComponent<WeatherSystem>("WeatherSystem");
        weatherSystem.currentWeather = state;

        Assert.That(weatherSystem.GetRoadGripMultiplier(), Is.EqualTo(expectedGrip).Within(0.001f));
    }

    [Test]
    public void RainController_SetRain_LightRain_UpdatesAudioWetnessAndWipers()
    {
        var controller = CreateComponent<RainController>("RainController");
        controller.rainParticles = CreateParticleSystem("RainParticles");
        controller.rainAudio = controller.gameObject.AddComponent<AudioSource>();
        controller.lightRainClip = AudioClip.Create("light", 4410, 1, 44100, false);
        controller.roadMaterial = new Material(Shader.Find("Standard"));
        controller.windscreenWipers = new GameObject("Wipers");

        controller.SetRain(WeatherState.LightRain, 0.5f);

        Assert.That(controller.rainAudio.clip, Is.SameAs(controller.lightRainClip));
        Assert.That(controller.rainAudio.volume, Is.EqualTo(0.3f).Within(0.001f));
        Assert.That(controller.rainAudio.loop, Is.True);
        Assert.That(GetPrivateField<float>(controller, "currentWetness"), Is.EqualTo(0.3f).Within(0.001f));
        Assert.That(controller.windscreenWipers.activeSelf, Is.True);
    }

    [Test]
    public void RainController_SetRain_Thunderstorm_UsesFallbackParticlesAndMaxWetness()
    {
        var controller = CreateComponent<RainController>("RainController");
        controller.rainParticles = CreateParticleSystem("FallbackRainParticles");
        controller.rainAudio = controller.gameObject.AddComponent<AudioSource>();
        controller.heavyRainClip = AudioClip.Create("heavy", 4410, 1, 44100, false);
        controller.roadMaterial = new Material(Shader.Find("Standard"));
        controller.windscreenWipers = new GameObject("Wipers");

        controller.SetRain(WeatherState.Thunderstorm, 0.2f);

        Assert.That(controller.rainAudio.clip, Is.SameAs(controller.heavyRainClip));
        Assert.That(controller.rainAudio.volume, Is.EqualTo(1f).Within(0.001f));
        Assert.That(GetPrivateField<float>(controller, "currentWetness"), Is.EqualTo(1f).Within(0.001f));
        Assert.That(controller.windscreenWipers.activeSelf, Is.True);

        var emission = controller.rainParticles.emission;
        Assert.That(emission.rateOverTime.constant, Is.EqualTo(500f).Within(0.001f));
    }

    [Test]
    public void RainController_SetRain_Clear_DisablesWipersAndStopsAudio()
    {
        var controller = CreateComponent<RainController>("RainController");
        controller.rainAudio = controller.gameObject.AddComponent<AudioSource>();
        controller.lightRainClip = AudioClip.Create("light", 4410, 1, 44100, false);
        controller.roadMaterial = new Material(Shader.Find("Standard"));
        controller.windscreenWipers = new GameObject("Wipers");

        controller.SetRain(WeatherState.LightRain, 0.5f);
        controller.SetRain(WeatherState.Clear, 0f);

        Assert.That(controller.rainAudio.isPlaying, Is.False);
        Assert.That(controller.windscreenWipers.activeSelf, Is.False);
    }

    [Test]
    public void SnowController_SetSnow_Blizzard_AccumulatesSnowAndSetsAudio()
    {
        var controller = CreateComponent<SnowController>("SnowController");
        controller.snowParticles = CreateParticleSystem("SnowParticles");
        controller.snowAudio = controller.gameObject.AddComponent<AudioSource>();
        controller.snowWindClip = AudioClip.Create("snow", 4410, 1, 44100, false);
        controller.groundMaterial = new Material(Shader.Find("Standard"));

        controller.SetSnow(WeatherState.Blizzard, 0.4f);

        Assert.That(controller.snowAudio.clip, Is.SameAs(controller.snowWindClip));
        Assert.That(controller.snowAudio.volume, Is.EqualTo(1f).Within(0.001f));
        Assert.That(GetPrivateField<float>(controller, "currentSnowAccumulation"), Is.EqualTo(0.1f).Within(0.001f));

        var emission = controller.snowParticles.emission;
        Assert.That(emission.rateOverTime.constant, Is.EqualTo(300f).Within(0.001f));
    }

    [Test]
    public void SnowController_SetSnow_Clear_StopsAudio()
    {
        var controller = CreateComponent<SnowController>("SnowController");
        controller.snowAudio = controller.gameObject.AddComponent<AudioSource>();
        controller.snowWindClip = AudioClip.Create("snow", 4410, 1, 44100, false);
        controller.groundMaterial = new Material(Shader.Find("Standard"));

        controller.SetSnow(WeatherState.Snow, 0.5f);
        controller.SetSnow(WeatherState.Clear, 0f);

        Assert.That(controller.snowAudio.isPlaying, Is.False);
    }

    [Test]
    public void FogController_SetFog_FogState_ConfiguresLinearFogTargets()
    {
        var controller = CreateComponent<FogController>("FogController");

        controller.SetFog(WeatherState.Fog, 0.5f);

        Assert.That(RenderSettings.fog, Is.True);
        Assert.That(RenderSettings.fogMode, Is.EqualTo(FogMode.Linear));
        Assert.That(GetPrivateField<float>(controller, "targetFogStart"), Is.EqualTo(255f).Within(0.001f));
        Assert.That(GetPrivateField<float>(controller, "targetFogEnd"), Is.EqualTo(1075f).Within(0.001f));
        Assert.That(GetPrivateField<bool>(controller, "fogActive"), Is.True);
    }

    [Test]
    public void FogController_SetFog_Clear_SetsTargetsBackToClearValues()
    {
        var controller = CreateComponent<FogController>("FogController");

        controller.SetFog(WeatherState.Fog, 1f);
        controller.SetFog(WeatherState.Clear, 0f);

        Assert.That(GetPrivateField<float>(controller, "targetFogStart"), Is.EqualTo(controller.clearFogStart).Within(0.001f));
        Assert.That(GetPrivateField<float>(controller, "targetFogEnd"), Is.EqualTo(controller.clearFogEnd).Within(0.001f));
        Assert.That(GetPrivateField<Color>(controller, "targetFogColor"), Is.EqualTo(controller.clearFogColor));
    }

    [Test]
    public void SkyController_SetSky_Blizzard_UpdatesPrivateTargets()
    {
        var controller = CreateComponent<SkyController>("SkyController");

        controller.SetSky(WeatherState.Blizzard, 0.7f, -5f);

        Assert.That(GetPrivateField<float>(controller, "targetSunIntensity"), Is.EqualTo(controller.thunderSunIntensity).Within(0.001f));
        Assert.That(GetPrivateField<float>(controller, "targetAmbientIntensity"), Is.EqualTo(controller.thunderAmbientIntensity).Within(0.001f));
        Assert.That(GetPrivateField<Color>(controller, "targetSkyColor"), Is.EqualTo(Color.Lerp(controller.snowSkyColor, controller.rainSkyColor, 0.5f)));
    }

    [Test]
    public void SkyController_SetSky_Clear_UsesClearTargets()
    {
        var controller = CreateComponent<SkyController>("SkyController");

        controller.SetSky(WeatherState.Clear, 0f, 20f);

        Assert.That(GetPrivateField<float>(controller, "targetSunIntensity"), Is.EqualTo(controller.clearSunIntensity).Within(0.001f));
        Assert.That(GetPrivateField<float>(controller, "targetAmbientIntensity"), Is.EqualTo(controller.clearAmbientIntensity).Within(0.001f));
        Assert.That(GetPrivateField<Color>(controller, "targetSkyColor"), Is.EqualTo(controller.clearSkyColor));
    }

    [Test]
    public void BusFleetManager_DefaultFleet_IncludesFiveTypes_AndStartsWithStandardBusSelected()
    {
        var gameState = CreateComponent<GameState>("GameState");
        SetSingleton(gameState);

        var xpSystem = CreateComponent<XPSystem>("XPSystem");
        SetSingleton(xpSystem);
        xpSystem.EnsureInitialized();

        var fleetManager = CreateComponent<BusFleetManager>("BusFleetManager");
        SetSingleton(fleetManager);
        fleetManager.EnsureInitialized();

        var specs = fleetManager.GetAllBusSpecs();

        Assert.That(specs.Count, Is.GreaterThanOrEqualTo(5));
        Assert.That(specs[0].displayName, Does.Contain("Single-Decker"));
        Assert.That(fleetManager.IsBusOwned(specs[0]), Is.True);
        Assert.That(fleetManager.GetSelectedBusSpec(), Is.EqualTo(specs[0]));
        Assert.That(gameState.vehicleState.activeBusId, Is.EqualTo(specs[0].busId));
    }

    [Test]
    public void BusFleetManager_UnlocksElectricBus_ByRank_AndPersistsSelectionIntoGameState()
    {
        var gameState = CreateComponent<GameState>("GameState");
        SetSingleton(gameState);

        var xpSystem = CreateComponent<XPSystem>("XPSystem");
        SetSingleton(xpSystem);
        xpSystem.EnsureInitialized();
        xpSystem.AwardXP(4000, "Fleet Progress");

        var fleetManager = CreateComponent<BusFleetManager>("BusFleetManager");
        SetSingleton(fleetManager);
        fleetManager.EnsureInitialized();

        BusSpec electricSpec = null;
        var specs = fleetManager.GetAllBusSpecs();
        for (int i = 0; i < specs.Count; i++)
        {
            if (specs[i] != null && specs[i].IsElectric)
            {
                electricSpec = specs[i];
                break;
            }
        }

        Assert.That(electricSpec, Is.Not.Null);
        Assert.That(fleetManager.IsBusOwned(electricSpec), Is.True);
        Assert.That(fleetManager.TrySelectBus(electricSpec), Is.True);
        Assert.That(gameState.vehicleState.activeBusId, Is.EqualTo(electricSpec.busId));
        Assert.That(gameState.vehicleState.isElectricBus, Is.True);
        Assert.That(gameState.vehicleState.energyUnitLabel, Is.EqualTo("kWh"));
    }

    private static T CreateComponent<T>(string name) where T : Component
    {
        var gameObject = new GameObject(name);
        return gameObject.AddComponent<T>();
    }

    private static AIRoadGraph CreateLinearAirRoadGraph()
    {
        var graph = CreateComponent<AIRoadGraph>("AIRoadGraph");
        graph.nodes = new AIRoadGraph.RoadNode[4];

        for (int i = 0; i < graph.nodes.Length; i++)
        {
            var point = new GameObject($"Node_{i}").transform;
            point.position = new Vector3(i * 10f, 0f, 0f);

            graph.nodes[i] = new AIRoadGraph.RoadNode
            {
                id = $"N{i}",
                point = point,
                laneCount = 2,
                laneWidth = 3.3f,
                speedLimitKmh = 40f,
                nextNodeIndices = i < graph.nodes.Length - 1 ? new[] { i + 1 } : new[] { i - 1 }
            };
        }

        return graph;
    }

    private static ParticleSystem CreateParticleSystem(string name)
    {
        var gameObject = new GameObject(name);
        return gameObject.AddComponent<ParticleSystem>();
    }

    private static Renderer CreateRenderer(string name)
    {
        var gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        gameObject.name = name;
        return gameObject.GetComponent<Renderer>();
    }

    private static void InvokePrivateMethod(object target, string methodName)
    {
        var method = target.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.That(method, Is.Not.Null, $"Expected private method '{methodName}' to exist.");
        method.Invoke(target, null);
    }

    private static void InvokePrivateMethod(object target, string methodName, params object[] args)
    {
        var method = target.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.That(method, Is.Not.Null, $"Expected private method '{methodName}' to exist.");
        method.Invoke(target, args);
    }

    private static object InvokePrivateMethodWithResult(object target, string methodName, params object[] args)
    {
        var method = target.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.That(method, Is.Not.Null, $"Expected private method '{methodName}' to exist.");
        return method.Invoke(target, args);
    }

    private static void SetPrivateField(object target, string fieldName, object value)
    {
        var field = target.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.That(field, Is.Not.Null, $"Expected private field '{fieldName}' to exist.");
        field.SetValue(target, value);
    }

    private static T GetPrivateField<T>(object target, string fieldName)
    {
        var field = target.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.That(field, Is.Not.Null, $"Expected private field '{fieldName}' to exist.");
        return (T)field.GetValue(target);
    }

    private static void SetSingleton<T>(T instance) where T : MonoBehaviour
    {
        var field = typeof(T).GetField("<Instance>k__BackingField", BindingFlags.Static | BindingFlags.NonPublic);
        Assert.That(field, Is.Not.Null, $"Expected singleton backing field for '{typeof(T).Name}' to exist.");
        field.SetValue(null, instance);
    }

    private static void ResetSingleton<T>() where T : MonoBehaviour
    {
        var field = typeof(T).GetField("<Instance>k__BackingField", BindingFlags.Static | BindingFlags.NonPublic);
        field?.SetValue(null, null);
    }

    private static void AssertColorApproximately(Color actual, Color expected, float tolerance = 0.001f)
    {
        Assert.That(actual.r, Is.EqualTo(expected.r).Within(tolerance));
        Assert.That(actual.g, Is.EqualTo(expected.g).Within(tolerance));
        Assert.That(actual.b, Is.EqualTo(expected.b).Within(tolerance));
        Assert.That(actual.a, Is.EqualTo(expected.a).Within(tolerance));
    }

    private class SerializablePayload
    {
        public string Name;
        public int Count { get; set; }
    }
}
