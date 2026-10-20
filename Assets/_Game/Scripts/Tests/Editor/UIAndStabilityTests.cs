using NUnit.Framework;
using UnityEngine;

public class UIAndStabilityTests
{
    // ── Minimap projection ────────────────────────────────────────────

    [Test]
    public void RotateToHeading_PointsTheDirectionOfTravelUp()
    {
        // Facing +Z (heading 0) already points up.
        Assert.That(RuntimeMinimapGraphic.RotateToHeading(new Vector2(0f, 1f), 0f), Is.EqualTo(new Vector2(0f, 1f)));
        // Facing +X (heading 90) must rotate to up as well.
        Vector2 east = RuntimeMinimapGraphic.RotateToHeading(new Vector2(1f, 0f), 90f);
        Assert.That(east.x, Is.EqualTo(0f).Within(0.001f));
        Assert.That(east.y, Is.EqualTo(1f).Within(0.001f));
        // Facing +X, the world +Z axis now points to the driver's right.
        Vector2 north = RuntimeMinimapGraphic.RotateToHeading(new Vector2(0f, 1f), 90f);
        Assert.That(north.x, Is.EqualTo(-1f).Within(0.001f));
        Assert.That(north.y, Is.EqualTo(0f).Within(0.001f));
    }

    [Test]
    public void ProjectWorld_ScalesMetresToPixels()
    {
        Vector2 projected = RuntimeMinimapGraphic.ProjectWorld(new Vector3(0f, 0f, 100f), Vector3.zero, 0f, 100f, 200f);
        Assert.That(projected.x, Is.EqualTo(0f).Within(0.001f));
        Assert.That(projected.y, Is.EqualTo(50f).Within(0.001f));
    }

    [Test]
    public void ClipSegmentToCircle_KeepsOnlyTheVisibleRoad()
    {
        Assert.That(RuntimeMinimapGraphic.ClipSegmentToCircle(new Vector2(-50f, 0f), new Vector2(50f, 0f), 100f, out Vector2 a, out Vector2 b), Is.True);
        Assert.That(a, Is.EqualTo(new Vector2(-50f, 0f)));
        Assert.That(b, Is.EqualTo(new Vector2(50f, 0f)));

        Assert.That(RuntimeMinimapGraphic.ClipSegmentToCircle(new Vector2(-200f, 0f), new Vector2(200f, 0f), 100f, out a, out b), Is.True);
        Assert.That(a.x, Is.EqualTo(-100f).Within(0.001f));
        Assert.That(b.x, Is.EqualTo(100f).Within(0.001f));

        Assert.That(RuntimeMinimapGraphic.ClipSegmentToCircle(new Vector2(200f, 0f), new Vector2(300f, 0f), 100f, out _, out _), Is.False);
    }

    // ── HUD readouts ──────────────────────────────────────────────────

    [TestCase(0f, "00:00")]
    [TestCase(510f, "08:30")]
    [TestCase(1439f, "23:59")]
    [TestCase(1500f, "01:00")]
    public void FormatClock_WrapsAcrossMidnight(float minutes, string expected)
    {
        Assert.That(HUDManager.FormatClock(minutes), Is.EqualTo(expected));
    }

    [Test]
    public void WeatherLabel_IsReadableWithoutIconGlyphs()
    {
        Assert.That(HUDManager.WeatherLabel(WeatherState.Clear), Is.EqualTo("CLEAR"));
        Assert.That(HUDManager.WeatherLabel(WeatherState.HeavyRain), Is.EqualTo("HEAVY RAIN"));
        Assert.That(HUDManager.WeatherLabel(WeatherState.Thunderstorm), Is.Not.Empty);
    }

    [TestCase("42", "42")]
    [TestCase("1234", "123")]
    [TestCase("NBO-8", "8")]
    [TestCase("NBO-123", "123")]
    [TestCase("NBO-R0", "R0")]
    [TestCase("", "•")]
    public void ShortRouteNumber_FitsTheRoundel(string number, string expected)
    {
        Assert.That(HUDManager.ShortRouteNumber(number), Is.EqualTo(expected));
    }

    [TestCase("NBO STOP 928938241 • MINIBUSES TO WESTLANDS VIA STOP 1284303773 (RETURN)", "WESTLANDS")]
    [TestCase("Route 46 TO Kibera", "KIBERA")]
    [TestCase("CBD - Westlands", "CBD - WESTLANDS")]
    public void DisplayDestination_ReducesGeneratedRouteNamesToTheDestination(string routeName, string expected)
    {
        Assert.That(HUDManager.DisplayDestination(routeName), Is.EqualTo(expected));
    }

    [Test]
    public void DisplayDestination_FallsBackWhenNothingReadableRemains()
    {
        Assert.That(HUDManager.DisplayDestination(null), Is.EqualTo(""));
        Assert.That(HUDManager.DisplayDestination("   "), Is.EqualTo(""));
    }

    // ── Bus stability assist ──────────────────────────────────────────

    [Test]
    public void SpeedDampingFactor_RampsInWithSpeed()
    {
        Assert.That(BusStabilityAssist.SpeedDampingFactor(0f, 25f), Is.EqualTo(0f));
        Assert.That(BusStabilityAssist.SpeedDampingFactor(12.5f, 25f), Is.EqualTo(0.5f).Within(0.001f));
        Assert.That(BusStabilityAssist.SpeedDampingFactor(25f, 25f), Is.EqualTo(1f));
        Assert.That(BusStabilityAssist.SpeedDampingFactor(80f, 25f), Is.EqualTo(1f));
    }

    [Test]
    public void DampingTorque_OpposesRotationAndSaturates()
    {
        Assert.That(BusStabilityAssist.AngularDamperTorque(0f, 1000f, 2f, 100000f), Is.EqualTo(0f));

        float gentle = BusStabilityAssist.AngularDamperTorque(90f, 1000f, 2f, 100000f);
        Assert.That(gentle, Is.LessThan(0f));
        Assert.That(gentle, Is.EqualTo(-Mathf.PI * 1000f).Within(1f));

        // Opposite spins produce mirrored torques.
        Assert.That(BusStabilityAssist.AngularDamperTorque(-90f, 1000f, 2f, 100000f), Is.EqualTo(-gentle).Within(0.001f));

        // A rate far past the cap is limited rather than amplified.
        Assert.That(BusStabilityAssist.AngularDamperTorque(5000f, 1000f, 10f, 500f), Is.EqualTo(-500f).Within(0.001f));
    }

    [Test]
    public void MaxCorrectionTorque_ScalesWithVehicleMass()
    {
        float light = BusStabilityAssist.MaxCorrectionTorque(6000f, 9.81f, 0.5f);
        float heavy = BusStabilityAssist.MaxCorrectionTorque(12000f, 9.81f, 0.5f);
        Assert.That(heavy, Is.EqualTo(light * 2f).Within(0.01f));
        Assert.That(BusStabilityAssist.MaxCorrectionTorque(12000f, 9.81f, 0.5f), Is.EqualTo(12000f * 9.81f * 0.5f).Within(0.01f));
    }

    [Test]
    public void BusStartup_InstallsStabilityAssist()
    {
        var root = new GameObject("Stability test");
        try
        {
            var bus = root.AddComponent<BusController>();
            typeof(BusController).GetMethod("Start", System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic).Invoke(bus, null);
            Assert.That(root.GetComponent<BusStabilityAssist>(), Is.Not.Null);
            Assert.That(root.GetComponent<BusStabilityAssist>().bus, Is.EqualTo(bus));
        }
        finally { Object.DestroyImmediate(root); }
    }
}
