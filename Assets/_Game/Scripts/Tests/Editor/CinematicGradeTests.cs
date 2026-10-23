using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Covers the camera-side half of the cinematic brief: exposure, contrast,
/// saturation, warm/cool tint, shadow lift and vignette. The rules are pure so a
/// grade that only looks right at one hour of the day is caught before it reaches
/// a frame.
/// </summary>
public class CinematicGradeTests
{
    [Test]
    public void Grade_ExposureHoldsAtNoonAndLiftsGoldenHourAndNight()
    {
        float noon = CinematicColorGrade.ExposureFor(68f, 0f, 1f, 0.055f, 0.075f);
        float golden = CinematicColorGrade.ExposureFor(0f, 0f, 1f, 0.055f, 0.075f);
        float night = CinematicColorGrade.ExposureFor(-20f, 0f, 1f, 0.055f, 0.075f);
        float overcastNoon = CinematicColorGrade.ExposureFor(68f, 1f, 1f, 0.055f, 0.075f);

        Assert.That(noon, Is.EqualTo(1f).Within(0.001f), "a clear noon is the neutral exposure");
        Assert.That(golden, Is.GreaterThan(noon), "golden hour gets a little more light, not a dimmer one");
        Assert.That(night, Is.GreaterThan(noon), "night needs a small lift to keep the cab readable");
        Assert.That(overcastNoon, Is.LessThan(noon), "cloud is already in the lighting; the grade only trims it");
        Assert.That(overcastNoon, Is.GreaterThan(0.9f), "the trim must not turn an overcast day into night");

        for (float elevation = -30f; elevation <= 80f; elevation += 5f)
        {
            float exposure = CinematicColorGrade.ExposureFor(elevation, 0.5f, 1f, 0.055f, 0.075f);
            Assert.That(exposure, Is.InRange(0.85f, 1.30f));
        }
    }

    [Test]
    public void Grade_WarmthFadesAsTheSunClimbsAndCloudNeutralisesIt()
    {
        Color golden = CinematicColorGrade.TintFor(1f, new Color(0.95f, 0.55f, 0.22f), 0f, 0.10f);
        Color noon = CinematicColorGrade.TintFor(68f, Color.white, 0f, 0.10f);
        Color goldenCloudy = CinematicColorGrade.TintFor(1f, new Color(0.95f, 0.55f, 0.22f), 1f, 0.10f);

        Assert.That(golden.r - golden.b, Is.GreaterThan(0.02f),
            "a low sun has to warm the grade, or the golden hour only exists in the sky");
        Assert.That(golden.r - golden.b, Is.GreaterThan(goldenCloudy.r - goldenCloudy.b),
            "thick cloud has to neutralise the warm grade, not leave a sepia cast");
        Assert.That(noon.r, Is.EqualTo(noon.g).Within(0.02f));
        Assert.That(noon.g, Is.EqualTo(noon.b).Within(0.02f));
    }

    [Test]
    public void Grade_TintIsCoolAtNightAndNeutralAtNoon()
    {
        Color night = CinematicColorGrade.TintFor(-20f, new Color(0.05f, 0.07f, 0.16f), 0f, 0.10f);
        Color noon = CinematicColorGrade.TintFor(68f, Color.white, 0f, 0.10f);

        Assert.That(night.b, Is.GreaterThan(night.r), "a night scene should be moonlit blue, not brown");
        Assert.That(noon.r, Is.EqualTo(1f).Within(0.02f));
        Assert.That(noon.b, Is.EqualTo(1f).Within(0.02f));
    }

    [Test]
    public void Grade_OvercastSoftensContrastAndSaturation()
    {
        float clearContrast = CinematicColorGrade.ContrastFor(45f, 0f, 1.06f);
        float overcastContrast = CinematicColorGrade.ContrastFor(45f, 1f, 1.06f);
        float clearSaturation = CinematicColorGrade.SaturationFor(45f, 0f, 1.08f);
        float overcastSaturation = CinematicColorGrade.SaturationFor(45f, 1f, 1.08f);

        Assert.That(overcastContrast, Is.LessThan(clearContrast));
        Assert.That(overcastSaturation, Is.LessThan(clearSaturation));
        Assert.That(overcastContrast, Is.GreaterThan(0.9f), "the grade still has to hold the scene together");
        Assert.That(overcastSaturation, Is.GreaterThan(0.8f), "cloud is not a black-and-white filter");
    }

    [Test]
    public void Grade_VignetteAndShadowLiftStayBoundedAndFollowTheLight()
    {
        float dayLift = CinematicColorGrade.ShadowLiftFor(45f, 0f, 0.01f);
        float cloudyLift = CinematicColorGrade.ShadowLiftFor(45f, 1f, 0.01f);
        float nightLift = CinematicColorGrade.ShadowLiftFor(-20f, 0f, 0.01f);
        float dayVignette = CinematicColorGrade.VignetteFor(45f, 0f, 0.16f);
        float nightVignette = CinematicColorGrade.VignetteFor(-20f, 0f, 0.16f);

        Assert.That(dayLift, Is.GreaterThan(0f));
        Assert.That(cloudyLift, Is.GreaterThan(dayLift), "cloud needs a little more lift to keep shaded walls readable");
        Assert.That(nightLift, Is.LessThan(dayLift), "night should not be lifted into dusk");
        Assert.That(dayVignette, Is.InRange(0f, 0.45f));
        Assert.That(nightVignette, Is.GreaterThan(dayVignette));
        Assert.That(nightVignette, Is.LessThanOrEqualTo(0.45f));
    }
}
