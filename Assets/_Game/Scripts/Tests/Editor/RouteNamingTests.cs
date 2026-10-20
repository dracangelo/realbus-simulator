using NUnit.Framework;
using UnityEngine;

/// <summary>
/// The same service must have one name across the importer, the route picker,
/// downloaded assets and the HUD. These tests pin the idempotent NBO rules and
/// the HUD roundel that has to show the number rather than the namespace.
/// </summary>
public class RouteNamingTests
{
    [TestCase("8", "NBO-8")]
    [TestCase("NBO-8", "NBO-8")]
    [TestCase("NBO-NBO-8", "NBO-8")]
    [TestCase("nbo_8", "NBO-8")]
    [TestCase("R0", "NBO-R0")]
    [TestCase("NBO-R0R", "NBO-R0R")]
    [TestCase("", "NBO-R")]
    [TestCase("-", "NBO-R")]
    public void NormalizeCode_AddsExactlyOneCityPrefix(string raw, string expected)
    {
        Assert.That(RouteNaming.NormalizeCode(raw), Is.EqualTo(expected));
    }

    [TestCase("Nairobi CBD - Westlands", "NBO Nairobi CBD - Westlands")]
    [TestCase("NBO Nairobi CBD", "NBO Nairobi CBD")]
    [TestCase("NBO-Strand Loop", "NBO-Strand Loop")]
    [TestCase("", "NBO Route")]
    public void NormalizeName_StartsWithNbo(string raw, string expected)
    {
        Assert.That(RouteNaming.NormalizeName(raw), Is.EqualTo(expected));
    }

    [Test]
    public void BusRoute_NormalisesLegacyAssetOnceAndStaysIdempotent()
    {
        BusRoute route = ScriptableObject.CreateInstance<BusRoute>();
        try
        {
            route.routeNumber = "8";
            route.routeName = "CBD - Westlands";
            route.osmRelationId = 12345;

            route.EnsureRuntimeData();
            Assert.That(route.routeNumber, Is.EqualTo("NBO-8"));
            Assert.That(route.routeName, Is.EqualTo("NBO CBD - Westlands"));
            Assert.That(route.DisplayCode, Is.EqualTo("NBO-8"));

            route.EnsureRuntimeData();
            Assert.That(route.routeNumber, Is.EqualTo("NBO-8"));
            Assert.That(route.routeName, Is.EqualTo("NBO CBD - Westlands"));
        }
        finally
        {
            Object.DestroyImmediate(route);
        }
    }

    [Test]
    public void ReturnRouteNumber_KeepsSingleNamespace()
    {
        Assert.That(RouteNaming.NormalizeCode("NBO-8-R"), Is.EqualTo("NBO-8-R"));
        Assert.That(RouteNaming.NormalizeCode("8-R"), Is.EqualTo("NBO-8-R"));
    }
}
