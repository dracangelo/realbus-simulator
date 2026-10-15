using System.Reflection;
using NUnit.Framework;
using UnityEngine;

public class Phase7PolishTests
{
    [TestCase(-1.01f, PunctualityStatus.Early)]
    [TestCase(-1f, PunctualityStatus.OnTime)]
    [TestCase(1f, PunctualityStatus.OnTime)]
    [TestCase(1.01f, PunctualityStatus.Late)]
    [TestCase(3.01f, PunctualityStatus.SeverelyLate)]
    public void ScheduleStatus_UsesPublishedTimingBands(float delta, PunctualityStatus expected)
    {
        Assert.That(ScheduleManager.ClassifyDeltaMinutes(delta), Is.EqualTo(expected));
    }

    [TestCase(-1, "R")]
    [TestCase(0, "D")]
    [TestCase(1, "2")]
    [TestCase(4, "5")]
    public void HudGearLabel_IsDriverFriendly(int gear, string expected)
    {
        Assert.That(HUDManager.FormatGear(gear), Is.EqualTo(expected));
    }

    [TestCase(20f, "<1 min")]
    [TestCase(60f, "1 min")]
    [TestCase(61f, "2 min")]
    public void HudEta_RoundsUpWithoutFalsePrecision(float seconds, string expected)
    {
        Assert.That(HUDManager.FormatEta(seconds), Is.EqualTo(expected));
    }

    [Test]
    public void ScheduleStatuses_HaveNonColourIcons()
    {
        Assert.That(HUDManager.StatusIcon(PunctualityStatus.OnTime), Is.EqualTo("✓"));
        Assert.That(HUDManager.StatusIcon(PunctualityStatus.Early), Is.Not.Empty);
        Assert.That(HUDManager.StatusIcon(PunctualityStatus.Late), Is.Not.Empty);
        Assert.That(HUDManager.StatusIcon(PunctualityStatus.SeverelyLate), Is.Not.Empty);
    }

    [Test]
    public void SteeringCurve_AppliesDeadZoneAndPreservesDirection()
    {
        Assert.That(MobileControlsUI.ApplySteeringCurve(0.04f, 0.06f, 1f), Is.Zero);
        Assert.That(MobileControlsUI.ApplySteeringCurve(0.7f, 0.06f, 1f), Is.GreaterThan(0f));
        Assert.That(MobileControlsUI.ApplySteeringCurve(-0.7f, 0.06f, 1f), Is.LessThan(0f));
    }

    [TestCase(29, 30)]
    [TestCase(44, 45)]
    [TestCase(59, 60)]
    public void FrameRateSetting_SnapsToMobilePresets(int requested, int expected)
    {
        Assert.That(SettingsManager.NormalizeFrameRate(requested), Is.EqualTo(expected));
    }

    [Test]
    public void AdaptiveResolution_ReducesLoadAndRecoversGradually()
    {
        Assert.That(MobilePerformanceManager.RecommendScale(1f, 30f, 20f, MobileThermalState.Nominal), Is.EqualTo(0.95f).Within(0.001f));
        Assert.That(MobilePerformanceManager.RecommendScale(0.8f, 12f, 20f, MobileThermalState.Nominal), Is.EqualTo(0.85f).Within(0.001f));
        Assert.That(MobilePerformanceManager.RecommendScale(0.7f, 20f, 20f, MobileThermalState.Critical), Is.EqualTo(0.65f).Within(0.001f));
    }

    [Test]
    public void DockingScore_RewardsPreciseAlignment()
    {
        float perfect = DockingZone.CalculateDockingScore(0f, 0f, 0f, 0f, 0.5f, 3f, 25f, 1.5f);
        float rough = DockingZone.CalculateDockingScore(0.4f, 2f, 20f, 1.2f, 0.5f, 3f, 25f, 1.5f);
        Assert.That(perfect, Is.EqualTo(100f));
        Assert.That(rough, Is.LessThan(perfect));
        Assert.That(DockingZone.DockingGrade(perfect), Is.EqualTo("GOLD"));
    }

    [TestCase(0.9f, 0)]
    [TestCase(0.7f, 1)]
    [TestCase(0.45f, 2)]
    [TestCase(0.2f, 3)]
    public void PassengerMood_UsesStableReadableBands(float satisfaction, int expected)
    {
        Assert.That(PassengerFeedbackSystem.MoodBand(satisfaction), Is.EqualTo(expected));
        Assert.That(PassengerFeedbackSystem.MoodIcon(satisfaction), Is.Not.Empty);
    }

    [Test]
    public void DefaultUpgrades_ExposeBenefitsAndTradeoffs()
    {
        var definitions = UpgradeManager.BuildDefaultDefinitions();
        try
        {
            Assert.That(definitions, Has.Count.EqualTo(5));
            for (int i = 0; i < definitions.Count; i++)
            {
                Assert.That(definitions[i].secondaryTarget, Is.Not.EqualTo(BusUpgradeTarget.None));
                Assert.That(definitions[i].secondaryMultiplier, Is.Not.EqualTo(1f));
            }
            BusUpgradeModifiers engineTune = BusUpgradeModifiers.Identity;
            engineTune.Apply(definitions.Find(item => item.upgradeId == "upgrade.engine_tune"));
            Assert.That(engineTune.engineTorque, Is.GreaterThan(1f));
            Assert.That(engineTune.energyConsumption, Is.GreaterThan(1f));
        }
        finally
        {
            for (int i = 0; i < definitions.Count; i++) Object.DestroyImmediate(definitions[i]);
        }
    }

    [Test]
    public void BalanceTelemetry_RollingAverageUsesEveryMission()
    {
        float average = BalanceTelemetryRecorder.RollingAverage(100f, 2, 40f);
        Assert.That(average, Is.EqualTo(80f).Within(0.001f));
    }

    [Test]
    public void MissionCheckpoint_RequiresCurrentSchemaAndMatchingRoute()
    {
        const string key = "RealBus.MissionCheckpoint.v1";
        try
        {
            var checkpoint = new MissionCheckpoint
            {
                schemaVersion = 2,
                routeId = "NBO|route.1",
                capturedAtUnixSeconds = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };
            PlayerPrefs.SetString(key, JsonUtility.ToJson(checkpoint));
            Assert.That(MobileResilienceManager.TryGetRecentCheckpoint("NBO|route.1", out MissionCheckpoint restored), Is.True);
            Assert.That(restored.schemaVersion, Is.EqualTo(2));
            Assert.That(MobileResilienceManager.TryGetRecentCheckpoint("NBO|route.2", out _), Is.False);
        }
        finally { PlayerPrefs.DeleteKey(key); }
    }

    [Test]
    public void DailyChallengeSeed_IsStableAndDepartureIsPublishedWindow()
    {
        int first = DailyChallengeManager.StableSeed("2026-09-15|NBO");
        int second = DailyChallengeManager.StableSeed("2026-09-15|NBO");
        Assert.That(second, Is.EqualTo(first));
        CollectionAssert.Contains(new[] { 420f, 480f, 720f, 960f, 1020f }, DailyChallengeManager.ResolveDepartureMinutes(first));
    }

    [TestCase(96f, "Platinum")]
    [TestCase(90f, "Gold")]
    [TestCase(80f, "Silver")]
    [TestCase(65f, "Bronze")]
    [TestCase(40f, "Developing")]
    public void RouteMastery_UsesClearMedalThresholds(float score, string expected)
    {
        Assert.That(RouteMasteryManager.Medal(score), Is.EqualTo(expected));
    }

    [Test]
    public void RouteGhost_OnlyReplacesAWeakerRun()
    {
        Assert.That(RouteGhostRecorder.IsBetterReplay(86f, 82f), Is.True);
        Assert.That(RouteGhostRecorder.IsBetterReplay(82f, 86f), Is.False);
        Assert.That(RouteGhostRecorder.IsBetterReplay(86f, 86f), Is.False);
    }

    [TestCase(1, 90f, 0)]
    [TestCase(2, 55f, 1)]
    [TestCase(5, 68f, 2)]
    [TestCase(8, 80f, 3)]
    public void PassengerStories_AdvanceFromTrustAndRepeatRides(int rides, float affinity, int expected)
    {
        Assert.That(PassengerStoryManager.CalculateStoryStage(rides, affinity), Is.EqualTo(expected));
    }

    [Test]
    public void UITheme_BusTokens_HaveHighContrastAndTransitAesthetics()
    {
        Assert.That(UITheme.BusAmber.r, Is.GreaterThan(0.8f));
        Assert.That(UITheme.BusStopRed.r, Is.GreaterThan(0.8f));
        Assert.That(UITheme.TransitNavy.r, Is.LessThan(0.15f));
        Assert.That(UITheme.TransitCard.r, Is.LessThan(0.2f));
        Assert.That(UITheme.TransitLedGreen.g, Is.GreaterThan(0.6f));
    }

    [Test]
    public void CinematicAtmosphereController_ConfiguresSoftShadowsAndAtmosphericHaze()
    {
        GameObject lightObj = new GameObject("TestSun", typeof(Light));
        Light light = lightObj.GetComponent<Light>();
        light.type = LightType.Directional;
        light.shadows = LightShadows.None;

        GameObject ctrlObj = new GameObject("TestCinematicAtmosphere", typeof(CinematicAtmosphereController));
        CinematicAtmosphereController controller = ctrlObj.GetComponent<CinematicAtmosphereController>();

        try
        {
            controller.ApplyCinematicAtmosphere();
            Assert.That(light.shadows, Is.EqualTo(LightShadows.Soft));
            Assert.That(light.color.r, Is.GreaterThan(0.9f));
            Assert.That(RenderSettings.fog, Is.True);
            Assert.That(RenderSettings.fogMode, Is.EqualTo(FogMode.Linear));
            Assert.That(RenderSettings.ambientMode, Is.EqualTo(UnityEngine.Rendering.AmbientMode.Trilight));
        }
        finally
        {
            Object.DestroyImmediate(lightObj);
            Object.DestroyImmediate(ctrlObj);
        }
    }

    [Test]
    public void MobileCameraController_PresetsAndHorizonRotation_AreConsistent()
    {
        Quaternion horizontal = MobileCameraController.HorizonRotation(Quaternion.Euler(15f, 90f, -10f));
        Vector3 forward = horizontal * Vector3.forward;
        Assert.That(forward.y, Is.EqualTo(0f).Within(0.001f));
        Assert.That(forward.x, Is.EqualTo(1f).Within(0.01f));

        Assert.That((int)BusCameraPreset.Exterior, Is.EqualTo(0));
        Assert.That((int)BusCameraPreset.Cockpit, Is.EqualTo(1));
        Assert.That((int)BusCameraPreset.WideChase, Is.EqualTo(2));
        Assert.That((int)BusCameraPreset.DoorView, Is.EqualTo(3));
    }

    [Test]
    public void MapTileLoader_InstanceStartsEmptyAndStaysReadOnly()
    {
        // The loader publishes this static from Awake, and Awake is exactly what an EditMode
        // test cannot exercise: Unity does not run it for a component a test creates, and
        // invoking it by hand stops the run with Unity's own
        // "[Assert] Assertion failed on expression: 'ShouldRunBehaviour()'" instead of
        // executing it. What can be pinned here is the half the consumers depend on — that
        // the accessor is reachable, starts empty, and cannot be reassigned.
        //
        // An eagerly created instance would be worse than a missing one: it would leak a
        // hidden GameObject and, because WorldReadiness reads it, mark the map as initialised
        // before any tile could ever load.
        Assert.IsTrue(MapTileLoader.Instance == null,
            "MapTileLoader.Instance must be empty until a loader in the scene has awoken.");

        var property = typeof(MapTileLoader).GetProperty(
            "Instance", BindingFlags.Public | BindingFlags.Static);
        Assert.IsNotNull(property, "Consumers read MapTileLoader.Instance, so it must stay public.");
        Assert.IsNull(property.GetSetMethod(),
            "Instance must stay read-only; a public setter would let any caller swap the loader " +
            "the map and the readiness gate read from.");
    }
}
