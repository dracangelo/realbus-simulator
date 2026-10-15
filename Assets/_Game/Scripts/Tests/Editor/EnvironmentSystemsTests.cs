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

    static float Luminance(Color color) => color.r * 0.299f + color.g * 0.587f + color.b * 0.114f;

    static void InvokePrivate(object target, string method)
    {
        var info = target.GetType().GetMethod(method, BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.IsNotNull(info, $"{target.GetType().Name}.{method} must exist");
        info.Invoke(target, null);
    }
}
