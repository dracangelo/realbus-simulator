using System.Reflection;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Covers the environment the sky and precipitation systems present: cloud cover
/// and darkness per weather state, stars that need both darkness and clear sky,
/// moonlight, skybox exposure, day/night fog tint, the sun's arc, and the fact that
/// rain and snow effects are built when nothing authored them.
/// </summary>
public class EnvironmentSystemsTests
{
    [TestCase(WeatherState.Clear, 0f)]
    [TestCase(WeatherState.PartlyCloudy, 0.2f)]
    [TestCase(WeatherState.Overcast, 0.4f)]
    [TestCase(WeatherState.HeavyRain, 0.85f)]
    [TestCase(WeatherState.Thunderstorm, 1f)]
    public void SkyEnvironment_CloudCoverageRisesWithWeatherUntilTheSkyIsSolid(WeatherState state, float intensity)
    {
        float coverage = SkyEnvironmentController.CloudCoverageFor(state, intensity);
        Assert.That(coverage, Is.InRange(0f, 1f));
        if (state != WeatherState.Clear)
            Assert.That(coverage, Is.GreaterThan(SkyEnvironmentController.CloudCoverageFor(WeatherState.Clear, 0f)),
                $"{state} must be cloudier than a clear sky");
        if (state == WeatherState.Overcast)
            Assert.That(coverage, Is.GreaterThan(0.6f), "overcast is a covered sky, not a few clouds");
        if (state == WeatherState.Thunderstorm)
            Assert.That(coverage, Is.GreaterThan(0.9f), "a storm has no blue sky left");
    }

    [Test]
    public void SkyEnvironment_LightRainIsLessCoveredThanHeavyRainAndStormsDarkenTheLayer()
    {
        Assert.That(SkyEnvironmentController.CloudCoverageFor(WeatherState.LightRain, 0.5f),
            Is.LessThanOrEqualTo(SkyEnvironmentController.CloudCoverageFor(WeatherState.HeavyRain, 0.85f)));

        Assert.That(SkyEnvironmentController.CloudDarknessFor(WeatherState.Clear, 0f), Is.EqualTo(0f));
        Assert.That(SkyEnvironmentController.CloudDarknessFor(WeatherState.Thunderstorm, 1f),
            Is.GreaterThan(SkyEnvironmentController.CloudDarknessFor(WeatherState.Overcast, 0.4f)),
            "a storm is darker than a plain overcast");
    }

    [Test]
    public void SkyEnvironment_StarsNeedNightAndClearSky()
    {
        const float fade = 6f;
        Assert.That(SkyEnvironmentController.StarVisibilityFor(40f, fade, 0f), Is.EqualTo(0f),
            "daylight shows no stars");
        Assert.That(SkyEnvironmentController.StarVisibilityFor(0f, fade, 0f), Is.EqualTo(0f).Within(0.35f),
            "stars start to appear as the sun reaches the horizon");
        Assert.That(SkyEnvironmentController.StarVisibilityFor(-25f, fade, 0f), Is.GreaterThan(0.9f),
            "a clear night is a full star field");
        Assert.That(SkyEnvironmentController.StarVisibilityFor(-25f, fade, 1f), Is.LessThan(0.1f),
            "an overcast night hides the stars");
    }

    [Test]
    public void SkyEnvironment_MoonlightAndSkyboxExposureFollowNightAndCloud()
    {
        Assert.That(SkyEnvironmentController.MoonIntensityFor(false, 0f, 0.14f), Is.EqualTo(0f), "no moonlight by day");
        Assert.That(SkyEnvironmentController.MoonIntensityFor(true, 0f, 0.14f), Is.EqualTo(0.14f).Within(0.001f));
        Assert.That(SkyEnvironmentController.MoonIntensityFor(true, 1f, 0.14f), Is.LessThan(0.05f),
            "thick cloud blocks the moon");

        Assert.That(SkyEnvironmentController.SkyExposureFor(1f, 0f, 1.15f, 0.12f), Is.EqualTo(1.15f).Within(0.001f));
        Assert.That(SkyEnvironmentController.SkyExposureFor(0f, 0f, 1.15f, 0.12f), Is.EqualTo(0.12f).Within(0.001f));
        Assert.That(SkyEnvironmentController.SkyExposureFor(1f, 1f, 1.15f, 0.12f),
            Is.LessThan(SkyEnvironmentController.SkyExposureFor(1f, 0f, 1.15f, 0.12f)),
            "cloud dims the sky by day too");
    }

    [Test]
    public void SkyEnvironment_CloudTintDarkensAtNightAndInStorms()
    {
        Color day = SkyEnvironmentController.CloudTintFor(1f, Color.white, 0f, Color.white, new Color(0.13f, 0.15f, 0.23f), 0.55f);
        Color night = SkyEnvironmentController.CloudTintFor(0f, Color.white, 0f, Color.white, new Color(0.13f, 0.15f, 0.23f), 0.55f);
        Color storm = SkyEnvironmentController.CloudTintFor(1f, Color.white, 0.85f, Color.white, new Color(0.13f, 0.15f, 0.23f), 0.55f);
        Color twilight = SkyEnvironmentController.CloudTintFor(0.12f, new Color(0.94f, 0.42f, 0.18f), 0f,
            Color.white, new Color(0.13f, 0.15f, 0.23f), 0.55f);

        Assert.That(Luminance(night), Is.LessThan(Luminance(day)), "night clouds are dark");
        Assert.That(Luminance(storm), Is.LessThan(Luminance(day)), "storm clouds are dark");
        Assert.That(twilight.r, Is.GreaterThan(twilight.b), "dawn and dusk warm the cloud layer");
    }

    [Test]
    public void SkyEnvironment_BuildsCloudStarAndMoonLayers()
    {
        var go = new GameObject("SkyEnvironmentTest");
        var controller = go.AddComponent<SkyEnvironmentController>();
        var previousSkybox = RenderSettings.skybox;
        RenderSettings.skybox = null;
        try
        {
            // Awake does not run for a component an EditMode test creates, so the
            // construction is invoked the way the runtime bootstrap would.
            InvokePrivate(controller, "Build");

            Assert.IsNotNull(controller.gameObject.transform.Find("SkyEnvironment_Domes"),
                "the sky layers need a root to follow the camera with");
            Transform domes = controller.gameObject.transform.Find("SkyEnvironment_Domes");
            var clouds = domes.Find("SkyEnvironment_Clouds");
            var stars = domes.Find("SkyEnvironment_Stars");
            var moon = domes.Find("SkyEnvironment_Moon");
            Assert.IsNotNull(clouds, "a cloud layer must exist");
            Assert.IsNotNull(stars, "a star layer must exist");
            Assert.IsNotNull(moon, "a moon must exist");
            Assert.IsNotNull(clouds.GetComponent<MeshRenderer>().sharedMaterial.mainTexture,
                "the cloud layer needs its generated texture");
            Assert.IsNotNull(stars.GetComponent<MeshRenderer>().sharedMaterial.mainTexture);
            Assert.IsTrue(clouds.GetComponent<MeshFilter>().sharedMesh.vertexCount > 0);
            Assert.IsNotNull(controller.GetComponentInChildren<Light>(true),
                "the moon carries its own light");
            Assert.IsTrue(RenderSettings.skybox != null && RenderSettings.skybox.name == "SkyEnvironment_Skybox",
                "the environment owns a procedural skybox so the horizon follows the time of day");
        }
        finally
        {
            RenderSettings.skybox = previousSkybox;
            Object.DestroyImmediate(go);
        }
    }

    [Test]
    public void RainController_BuildsRainSystemsAndDrizzleWhenNoneAreAuthored()
    {
        var go = new GameObject("RainControllerTest");
        var controller = go.AddComponent<RainController>();
        try
        {
            Assert.IsNull(controller.rainParticles, "the fixture starts with no authored rain");

            controller.EnsureEffects();

            Assert.IsNotNull(controller.drizzleParticles, "drizzle needs its own system");
            Assert.IsNotNull(controller.rainParticles, "rain must exist before a state can show it");
            Assert.IsNotNull(controller.thunderstormParticles, "a storm needs the heavier system");
            Assert.IsNotNull(controller.rainParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial,
                "rain needs a streak material to be visible");

            controller.SetRain(WeatherState.HeavyRain, 1f);
            Assert.That(controller.rainParticles.emission.rateOverTime.constant, Is.GreaterThan(0f),
                "heavy rain must actually emit");
            controller.SetRain(WeatherState.Clear, 0f);
            Assert.That(controller.rainParticles.isPlaying, Is.False, "rain stops when the weather clears");
        }
        finally { Object.DestroyImmediate(go); }
    }

    [Test]
    public void SnowController_BuildsSnowSystemsWhenNoneAreAuthored()
    {
        var go = new GameObject("SnowControllerTest");
        var controller = go.AddComponent<SnowController>();
        try
        {
            Assert.IsNull(controller.snowParticles, "the fixture starts with no authored snow");

            controller.EnsureEffects();

            Assert.IsNotNull(controller.snowParticles, "snow must exist before a state can show it");
            Assert.IsNotNull(controller.blizzardParticles, "a blizzard needs the heavier system");
            Assert.IsNotNull(controller.snowParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial,
                "snow needs a flake material to be visible");

            controller.SetSnow(WeatherState.Blizzard, 1f);
            Assert.That(controller.blizzardParticles.emission.rateOverTime.constant, Is.GreaterThan(0f),
                "a blizzard must actually emit");
        }
        finally { Object.DestroyImmediate(go); }
    }

    [Test]
    public void FogController_NightFogIsDarkerThanDayFogAndPicksUpTheSkyTint()
    {
        Color weatherFog = new Color(0.78f, 0.84f, 0.92f);
        Color nightTint = new Color(0.03f, 0.04f, 0.09f);
        Color day = FogController.TintForDaylight(weatherFog, Color.white, 1f, nightTint, 0.75f);
        Color night = FogController.TintForDaylight(weatherFog, Color.white, 0f, nightTint, 0.75f);

        Assert.That(Luminance(night), Is.LessThan(Luminance(day) * 0.5f), "the same fog has to go dark at night");

        Color warm = FogController.TintForDaylight(weatherFog, new Color(0.94f, 0.42f, 0.18f), 1f, nightTint, 0.75f);
        Assert.That(warm.r - warm.b, Is.GreaterThan(day.r - day.b), "a sunrise sky warms the fog");
    }

    [Test]
    public void TimeOfDaySystem_DayCycleAdvancesAtTheConfiguredRateAndWraps()
    {
        // A 30 real-minute day is 0.8 game minutes per real second, or 1.25 real
        // seconds per game minute. That is the difference between a visible sunset
        // and a clock that matches the wall clock for the whole session.
        float rate = TimeOfDaySystem.SecondsPerGameMinuteFor(30f, 60f);
        Assert.That(rate, Is.EqualTo(1.25f).Within(0.001f));

        float noonish = TimeOfDaySystem.AdvanceMinutes(480f, 15f * 60f, 30f, 60f);
        Assert.That(noonish, Is.EqualTo(1200f).Within(0.01f),
            "half a compressed day after 08:00 is 20:00");

        float wrapped = TimeOfDaySystem.AdvanceMinutes(1380f, 10f * 60f, 30f, 60f);
        Assert.That(wrapped, Is.EqualTo(420f).Within(0.01f),
            "the clock wraps past midnight instead of running off the end of the day");

        Assert.That(TimeOfDaySystem.SecondsPerGameMinuteFor(0f, 60f), Is.EqualTo(60f),
            "a zero day length keeps the authored legacy rate");
        Assert.That(TimeOfDaySystem.AdvanceMinutes(0f, 60f, 0f, 60f), Is.EqualTo(1f).Within(0.001f),
            "the legacy rate still advances one game minute per real minute");
    }

    [Test]
    public void TimeOfDaySystem_CanPinTheWorldClockWithoutStoppingTheMissionTimetable()
    {
        var go = new GameObject("PinnedClockTest");
        var clock = go.AddComponent<TimeOfDaySystem>();
        try
        {
            clock.followScheduleManager = false;
            clock.SetTimeOfDay(1080f); // 18:00, sunset on the default arc
            Assert.That(clock.IsNight, Is.True, "sunset is the end of the lit day");
            Assert.That(clock.SunElevationDegrees, Is.EqualTo(0f).Within(0.01f));

            clock.SetTimeOfDay(720f);
            Assert.That(clock.IsNight, Is.False);
            Assert.That(clock.SunElevationDegrees, Is.EqualTo(clock.noonSunElevation).Within(0.01f));
            Assert.That(clock.GetSunDirection().y, Is.GreaterThan(0.8f));
        }
        finally { Object.DestroyImmediate(go); }
    }

    [Test]
    public void WeatherSystem_WindyIsABrokenCloudDeckWithStrongerWindThanOvercast()
    {
        float partlyCover = SkyEnvironmentController.CloudCoverageFor(WeatherState.PartlyCloudy, 0.2f);
        float windyCover = SkyEnvironmentController.CloudCoverageFor(WeatherState.Windy, 0.45f);
        float overcastCover = SkyEnvironmentController.CloudCoverageFor(WeatherState.Overcast, 0.4f);

        Assert.That(windyCover, Is.GreaterThan(partlyCover), "a windy sky is more than a few clouds");
        Assert.That(windyCover, Is.LessThan(overcastCover), "but it is not a solid ceiling");

        const float calm = 1.4f, breeze = 4.6f, windy = 9.2f, storm = 17.5f;
        float windySpeed = WindSystem.BaseSpeedFor(WeatherState.Windy, 0.45f, calm, breeze, windy, storm);
        float overcastSpeed = WindSystem.BaseSpeedFor(WeatherState.Overcast, 0.4f, calm, breeze, windy, storm);
        Assert.That(windySpeed, Is.GreaterThan(overcastSpeed),
            "the state is called Windy because it is windier than an overcast");
        Assert.That(windySpeed, Is.LessThan(WindSystem.BaseSpeedFor(WeatherState.Thunderstorm, 1f, calm, breeze, windy, storm)),
            "but it is not a storm");
    }

    [Test]
    public void FogController_WindySitsBetweenClearAirAndAnOvercastCeiling()
    {
        var go = new GameObject("FogWindyTest");
        var fog = go.AddComponent<FogController>();
        try
        {
            fog.SetFog(WeatherState.Clear, 0f);
            float clear = fog.TargetVisibility;
            fog.SetFog(WeatherState.Windy, 0.45f);
            float windy = fog.TargetVisibility;
            fog.SetFog(WeatherState.Overcast, 0.4f);
            float overcast = fog.TargetVisibility;

            Assert.That(windy, Is.LessThan(clear), "a windy day still has distance haze");
            Assert.That(windy, Is.GreaterThan(overcast), "but you can see further than under a solid overcast");
        }
        finally { Object.DestroyImmediate(go); }
    }

    [Test]
    public void WeatherSystem_ProceduralTransitionsTakeTheNeighbouringStateInsteadOfTeleporting()
    {
        Assert.That(WeatherSystem.SoftenTransition(WeatherState.Clear, WeatherState.Thunderstorm),
            Is.EqualTo(WeatherState.HeavyRain), "a storm has to pass through rain");
        Assert.That(WeatherSystem.SoftenTransition(WeatherState.Clear, WeatherState.HeavyRain),
            Is.EqualTo(WeatherState.Overcast), "a downpour cannot appear out of a clear sky");
        Assert.That(WeatherSystem.SoftenTransition(WeatherState.Clear, WeatherState.LightRain),
            Is.EqualTo(WeatherState.Overcast), "rain needs cloud first");
        Assert.That(WeatherSystem.SoftenTransition(WeatherState.Windy, WeatherState.Fog),
            Is.EqualTo(WeatherState.Overcast), "fog forms under a ceiling, not out of broken cloud");
        Assert.That(WeatherSystem.SoftenTransition(WeatherState.Windy, WeatherState.Snow),
            Is.EqualTo(WeatherState.Overcast), "snow needs cloud before it needs snow");
        Assert.That(WeatherSystem.SoftenTransition(WeatherState.HeavyRain, WeatherState.Thunderstorm),
            Is.EqualTo(WeatherState.Thunderstorm), "rain is allowed to become a storm");

        Assert.That(WeatherSystem.AdjacentWeather(WeatherState.Clear, 0f), Is.EqualTo(WeatherState.PartlyCloudy));
        Assert.That(WeatherSystem.AdjacentWeather(WeatherState.Clear, 0.99f), Is.EqualTo(WeatherState.Overcast));

        for (int step = 0; step < 16; step++)
        {
            float roll = step / 16f;
            WeatherState next = WeatherSystem.ChooseNextWeather(WeatherState.Windy, WeatherState.Windy, roll);
            Assert.That(next, Is.Not.EqualTo(WeatherState.Windy),
                "the cycle has to actually change the state, not re-announce it");
        }
    }

    [Test]
    public void WeatherSystem_RealObservationsPromoteDryCloudAndRejectOutOfSeasonSnow()
    {
        Assert.That(WeatherSystem.StateForWmoCode(0), Is.EqualTo(WeatherState.Clear));
        Assert.That(WeatherSystem.StateForWmoCode(3), Is.EqualTo(WeatherState.Overcast));
        Assert.That(WeatherSystem.StateForWmoCode(85), Is.EqualTo(WeatherState.Snow));
        Assert.That(WeatherSystem.StateForWmoCode(95), Is.EqualTo(WeatherState.Thunderstorm));

        Assert.That(WeatherSystem.ApplyObservedConditions(WeatherState.Overcast, 42f, false),
            Is.EqualTo(WeatherState.Windy), "a strong dry wind turns a cloudy sky into the windy state");
        Assert.That(WeatherSystem.ApplyObservedConditions(WeatherState.Overcast, 8f, false),
            Is.EqualTo(WeatherState.Overcast), "a light breeze leaves the cloud deck alone");
        Assert.That(WeatherSystem.ApplyObservedConditions(WeatherState.Snow, 5f, false),
            Is.EqualTo(WeatherState.HeavyRain), "snow outside the snow season falls as rain");
        Assert.That(WeatherSystem.ApplyObservedConditions(WeatherState.Snow, 5f, true),
            Is.EqualTo(WeatherState.Snow));
    }

    [Test]
    public void HUDManager_WindyHasAReadableLabel()
    {
        Assert.That(HUDManager.WeatherLabel(WeatherState.Windy), Is.EqualTo("WINDY"));
    }

    [Test]
    public void TimeOfDaySystem_SunFollowsAContinuousArcAndSetsBelowTheHorizon()
    {
        const float sunrise = 360f;
        const float sunset = 1080f;
        const float noon = 68f;
        const float midnight = -34f;

        Assert.That(TimeOfDaySystem.SunElevationAt(sunrise, sunrise, sunset, noon, midnight), Is.EqualTo(0f).Within(0.01f),
            "the sun sits on the horizon at sunrise");
        Assert.That(TimeOfDaySystem.SunElevationAt(720f, sunrise, sunset, noon, midnight), Is.EqualTo(noon).Within(0.01f),
            "the arc peaks at solar noon");
        Assert.That(TimeOfDaySystem.SunElevationAt(sunset, sunrise, sunset, noon, midnight), Is.EqualTo(0f).Within(0.01f),
            "and comes back to the horizon at sunset");
        Assert.That(TimeOfDaySystem.SunElevationAt(0f, sunrise, sunset, noon, midnight), Is.EqualTo(midnight).Within(1f),
            "the sun keeps travelling below the horizon through the night");
        Assert.That(TimeOfDaySystem.SunElevationAt(540f, sunrise, sunset, noon, midnight),
            Is.GreaterThan(TimeOfDaySystem.SunElevationAt(400f, sunrise, sunset, noon, midnight)),
            "the morning sun only ever climbs");
    }

    [Test]
    public void TimeOfDaySystem_ReportsElevationDaylightAndTheDirectionToTheSun()
    {
        var go = new GameObject("ClockTest");
        var clock = go.AddComponent<TimeOfDaySystem>();
        try
        {
            clock.currentTimeMinutes = 720f;
            InvokePrivate(clock, "ApplyTimeOfDayImmediate");
            Assert.That(clock.IsNight, Is.False);
            Assert.That(clock.SunElevationDegrees, Is.EqualTo(clock.noonSunElevation).Within(0.01f));
            Assert.That(clock.GetDaylightFactor(), Is.EqualTo(1f).Within(0.001f));
            Vector3 noonDirection = clock.GetSunDirection();
            Assert.That(noonDirection.y, Is.GreaterThan(0.8f), "the sun is high and up");
            Assert.That(noonDirection.magnitude, Is.EqualTo(1f).Within(0.001f));

            clock.currentTimeMinutes = 30f;
            InvokePrivate(clock, "ApplyTimeOfDayImmediate");
            Assert.That(clock.IsNight, Is.True);
            Assert.That(clock.SunElevationDegrees, Is.LessThan(0f), "at night the sun is below the horizon");
            Assert.That(clock.GetSunDirection().y, Is.LessThan(0f));
            Assert.That(clock.GetDaylightFactor(), Is.EqualTo(0f));
        }
        finally { Object.DestroyImmediate(go); }
    }

    [Test]
    public void TimeOfDaySystem_SkyIsWarmAtSunriseAndBlueByMidMorning()
    {
        Gradient sky = TimeOfDaySystem.CreateDefaultSkyGradient();
        Assert.That(sky.colorKeys.Length, Is.InRange(3, 8),
            "Unity rejects a gradient with more than eight colour keys and silently keeps the white default");

        // The clock's day phase: 0 at sunrise, 1 at sunset. 385 / 480 / 720 minutes
        // are 06:25, 08:00 and noon on the default 06:00-18:00 arc.
        Color dawn = sky.Evaluate((385f - 360f) / 720f);
        Color morning = sky.Evaluate((480f - 360f) / 720f);
        Color noon = sky.Evaluate((720f - 360f) / 720f);

        Assert.That(dawn.r - dawn.b, Is.GreaterThan(0.3f), "the sunrise sky is warm");
        Assert.That(noon.b, Is.GreaterThan(noon.r), "the noon sky is blue");
        Assert.That(morning.b - morning.r, Is.GreaterThan(0.1f),
            "by mid-morning the daylight has taken over the warm horizon, instead of the sky staying \"dawn\" until lunch");
        Assert.That(morning.r - morning.b, Is.LessThan(dawn.r - dawn.b), "and it is cooler than sunrise");
    }

    [Test]
    public void TimeOfDaySystem_OnlyAGradientWithRealColoursCountsAsAuthored()
    {
        Assert.IsFalse(TimeOfDaySystem.IsAuthoredSkyGradient(null),
            "a missing gradient is not authored");
        Assert.IsFalse(TimeOfDaySystem.IsAuthoredSkyGradient(new Gradient()),
            "Unity's built-in two-key white gradient is what a run-time-added clock starts on, and it must be replaced");
        Assert.IsTrue(TimeOfDaySystem.IsAuthoredSkyGradient(TimeOfDaySystem.CreateDefaultSkyGradient()),
            "the installed default is authored, so it survives a reload");
    }

    [Test]
    public void WeatherSystem_ForwardsAWeatherStateToTheSkyEnvironment()
    {
        var weatherGo = new GameObject("WeatherStub");
        var skyGo = new GameObject("SkyStub");
        try
        {
            var weather = weatherGo.AddComponent<WeatherSystem>();
            var sky = skyGo.AddComponent<SkyEnvironmentController>();
            weather.skyEnvironmentController = sky;

            weather.ApplyWeather(WeatherState.Clear, 0f, 22f);
            float clearCoverage = sky.targetCloudCoverage;

            weather.ApplyWeather(WeatherState.Thunderstorm, 1f, 18f);

            Assert.That(sky.targetCloudCoverage, Is.GreaterThan(clearCoverage),
                "a storm has to cover the sky the weather system reports");
            Assert.That(sky.cloudDarkness, Is.GreaterThan(0.5f), "and darken the cloud layer");
            Assert.That(weather.currentWeather, Is.EqualTo(WeatherState.Thunderstorm));
        }
        finally
        {
            Object.DestroyImmediate(weatherGo);
            Object.DestroyImmediate(skyGo);
        }
    }

    [Test]
    public void AtmosphericHaze_OwnsClearWeatherFogButNeverWeatherFog()
    {
        Assert.That(CinematicAtmosphereController.HazeMayOwnFog(false, WeatherState.Clear), Is.True,
            "clear weather leaves the fog to the city haze");
        Assert.That(CinematicAtmosphereController.HazeMayOwnFog(false, WeatherState.PartlyCloudy), Is.True);
        Assert.That(CinematicAtmosphereController.HazeMayOwnFog(true, WeatherState.Clear), Is.False,
            "weather fog wins while it is running, whatever the state says");
        Assert.That(CinematicAtmosphereController.HazeMayOwnFog(false, WeatherState.HeavyRain), Is.False,
            "rain owns the fog even before its own fog has started");
        Assert.That(CinematicAtmosphereController.HazeMayOwnFog(false, WeatherState.Blizzard), Is.False);
    }

    [Test]
    public void FogController_WeatherFogActiveTracksTheWeatherState()
    {
        var go = new GameObject("FogControllerTest");
        var fog = go.AddComponent<FogController>();
        try
        {
            Assert.That(fog.WeatherFogActive, Is.False, "a fresh controller owns no fog");
            fog.SetFog(WeatherState.HeavyRain, 1f);
            Assert.That(fog.WeatherFogActive, Is.True);
            Assert.That(RenderSettings.fog, Is.True);
            Assert.That(RenderSettings.fogMode, Is.EqualTo(FogMode.Linear));
        }
        finally { Object.DestroyImmediate(go); }
    }

    [Test]
    public void AtmosphericHaze_AppliesInClearWeatherAndStandsDownUnderWeatherFog()
    {
        // A controller left over from another fixture would own the fog, and this
        // test speaks only for the two cases it sets up itself.
        foreach (var leftover in Object.FindObjectsByType<FogController>(FindObjectsInactive.Include))
            Object.DestroyImmediate(leftover.gameObject);

        var hazeGo = new GameObject("HazeTest");
        var weatherFogGo = new GameObject("WeatherFogTest");
        var haze = hazeGo.AddComponent<CinematicAtmosphereController>();
        var weatherFog = weatherFogGo.AddComponent<FogController>();
        bool previousFog = RenderSettings.fog;
        float previousEnd = RenderSettings.fogEndDistance;
        try
        {
            RenderSettings.fogEndDistance = 77f;
            haze.ApplyAtmosphericHaze();
            Assert.That(RenderSettings.fog, Is.True, "clear weather gets the city's distance haze");
            Assert.That(RenderSettings.fogEndDistance, Is.EqualTo(haze.hazeEndDistance).Within(0.01f));

            InvokePrivate(weatherFog, "Awake");
            weatherFog.SetFog(WeatherState.Thunderstorm, 1f);
            RenderSettings.fogEndDistance = 77f;
            haze.ApplyAtmosphericHaze();
            Assert.That(RenderSettings.fogEndDistance, Is.EqualTo(77f).Within(0.01f),
                "the haze must not overwrite the storm's own fog");
        }
        finally
        {
            RenderSettings.fog = previousFog;
            RenderSettings.fogEndDistance = previousEnd;
            Object.DestroyImmediate(weatherFogGo);
            Object.DestroyImmediate(hazeGo);
        }
    }

    [Test]
    public void WindSystem_RisesWithTheWeatherAndNeverDropsBelowStillAir()
    {
        const float calm = 1.4f, breeze = 4.6f, windy = 9.2f, storm = 17.5f;

        float clear = WindSystem.BaseSpeedFor(WeatherState.Clear, 0f, calm, breeze, windy, storm);
        float cloudy = WindSystem.BaseSpeedFor(WeatherState.Overcast, 0.4f, calm, breeze, windy, storm);
        float rain = WindSystem.BaseSpeedFor(WeatherState.HeavyRain, 1f, calm, breeze, windy, storm);
        float gale = WindSystem.BaseSpeedFor(WeatherState.Thunderstorm, 1f, calm, breeze, windy, storm);

        Assert.That(clear, Is.EqualTo(calm).Within(0.01f), "a clear sky is still air");
        Assert.That(cloudy, Is.GreaterThan(clear), "a windy overcast is the whole point of an overcast");
        Assert.That(rain, Is.GreaterThan(cloudy), "rain travels with more wind than cloud does");
        Assert.That(gale, Is.GreaterThan(rain), "a thunderstorm is the windiest state");
        Assert.That(WindSystem.BaseSpeedFor(WeatherState.Fog, 0.7f, calm, breeze, windy, storm), Is.LessThan(cloudy),
            "fog only forms in air that has stopped moving");

        for (int step = 0; step <= 10; step++)
        {
            float intensity = step / 10f;
            float speed = WindSystem.BaseSpeedFor(WeatherState.Drizzle, intensity, calm, breeze, windy, storm);
            Assert.That(speed, Is.GreaterThanOrEqualTo(clear - 0.001f),
                "no state may fall below the still-air floor");
        }
    }

    [Test]
    public void WindSystem_GustsRideOnTopOfTheBaseAndStayBounded()
    {
        Vector2 lull = WindSystem.WindFor(10f, 90f, 0f, 0.5f);
        Vector2 peak = WindSystem.WindFor(10f, 90f, 1f, 0.5f);

        Assert.That(lull.magnitude, Is.EqualTo(10f).Within(0.01f), "a lull is the base speed");
        Assert.That(peak.magnitude, Is.EqualTo(15f).Within(0.01f), "a full gust adds its strength to the base");
        Assert.That(peak.x, Is.EqualTo(15f).Within(0.01f), "bearing 90 blows east");
        Assert.That(peak.y, Is.EqualTo(0f).Within(0.01f));

        float lowest = 1f, highest = 0f;
        for (int step = 0; step < 64; step++)
        {
            float gust = WindSystem.GustAt(step / 64f, 1f);
            Assert.That(gust, Is.InRange(0f, 1f));
            lowest = Mathf.Min(lowest, gust);
            highest = Mathf.Max(highest, gust);
        }
        Assert.That(lowest, Is.LessThan(0.25f), "the gust envelope has to pass through lulls");
        Assert.That(highest, Is.GreaterThan(0.75f), "and through real peaks, not a constant breeze");
    }

    [Test]
    public void RainController_LeansTheShowerWithTheWindInsteadOfFallingAlongTheEmitter()
    {
        var go = new GameObject("RainWindTest");
        var controller = go.AddComponent<RainController>();
        try
        {
            controller.EnsureEffects();
            controller.ApplyWind(new Vector2(12f, -4f));

            var velocity = controller.rainParticles.velocityOverLifetime;
            Assert.That(velocity.enabled, Is.True, "rain needs a world-space velocity to lean with");
            Assert.That(velocity.space, Is.EqualTo(ParticleSystemSimulationSpace.World));
            Assert.That(velocity.y.constant, Is.LessThan(0f), "rain must fall, not travel along the emitter's forward axis");
            Assert.That(velocity.x.constant, Is.EqualTo(12f).Within(0.01f), "east wind pushes the streaks east");
            Assert.That(velocity.z.constant, Is.EqualTo(-4f).Within(0.01f), "north wind pushes them north");
        }
        finally { Object.DestroyImmediate(go); }
    }

    [Test]
    public void Precipitation_FadesWithTheLightInsteadOfGlowingAtNight()
    {
        Color day = new Color(0.72f, 0.78f, 0.86f, 0.55f);
        Color night = new Color(0.28f, 0.33f, 0.44f, 0.42f);

        Color lit = RainController.TintForLight(day, night, 1f);
        Color dark = RainController.TintForLight(day, night, 0f);

        Assert.That(lit, Is.EqualTo(day), "daylight rain keeps the streak colour");
        Assert.That(dark, Is.EqualTo(night), "after dark it drops to the night floor");
        Assert.That(Luminance(dark), Is.LessThan(Luminance(lit) * 0.6f),
            "unlit streaks must not read as white against a midnight sky");
    }

    [Test]
    public void TimeOfDaySystem_SunlightIsWarmAtTheHorizonAndNeutralOverhead()
    {
        Color horizon = new Color(1f, 0.46f, 0.2f);
        Color low = new Color(1f, 0.8f, 0.58f);
        Color noon = new Color(1f, 0.97f, 0.91f);

        Color atHorizon = TimeOfDaySystem.SunTintFor(0f, horizon, low, noon);
        Color atSix = TimeOfDaySystem.SunTintFor(6f, horizon, low, noon);
        Color high = TimeOfDaySystem.SunTintFor(60f, horizon, low, noon);

        Assert.That(atHorizon.r - atHorizon.b, Is.GreaterThan(high.r - high.b),
            "the setting sun has to be warmer than the noon sun");
        Assert.That(atSix.r - atSix.b, Is.LessThan(atHorizon.r - atHorizon.b), "warmth fades as the sun climbs");
        Assert.That(atSix.r - atSix.b, Is.GreaterThan(high.r - high.b), "but six degrees up is still golden");
        Assert.That(high, Is.EqualTo(noon), "noon light is neutral");
        Assert.That(TimeOfDaySystem.SunTintFor(-10f, horizon, low, noon), Is.EqualTo(horizon),
            "below the horizon the sun keeps the last colour it had");
    }

    [Test]
    public void TimeOfDaySystem_DirectSunDisappearsBelowTheHorizon()
    {
        Assert.That(TimeOfDaySystem.SunVisibilityFor(20f), Is.EqualTo(1f).Within(0.001f));
        Assert.That(TimeOfDaySystem.SunVisibilityFor(-20f), Is.EqualTo(0f).Within(0.001f),
            "a set sun must not light the city from underneath");
        Assert.That(TimeOfDaySystem.SunVisibilityFor(0f), Is.InRange(0.05f, 0.95f),
            "the sun fades through the horizon rather than switching off");
    }

    static float Luminance(Color color) => color.r * 0.299f + color.g * 0.587f + color.b * 0.114f;

    static void InvokePrivate(object target, string method)
    {
        var info = target.GetType().GetMethod(method, BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.IsNotNull(info, $"{target.GetType().Name}.{method} must exist");
        info.Invoke(target, null);
    }
}
