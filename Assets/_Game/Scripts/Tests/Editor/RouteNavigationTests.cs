using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class RouteNavigationTests
{
    static List<Vector3> StraightPath(float length = 200f)
    {
        var path = new List<Vector3>(2);
        path.Add(Vector3.zero);
        path.Add(new Vector3(0f, 0f, length));
        return path;
    }

    [Test]
    public void AgainstPath_IsFalseWhenDrivingAlongTheGuidanceLine()
    {
        var path = StraightPath();
        Assert.That(RouteRecoverySystem.IsDrivingAgainstPath(path, new Vector3(0f, 0f, 40f), Vector3.forward), Is.False,
            "a bus following the line must never be told to turn back");
    }

    [Test]
    public void AgainstPath_IsTrueWhenDrivingOppositeTheGuidanceLine()
    {
        var path = StraightPath();
        Assert.That(RouteRecoverySystem.IsDrivingAgainstPath(path, new Vector3(0f, 0f, 40f), Vector3.back), Is.True,
            "a bus heading away down its own route is the case the warning exists for");
    }

    [Test]
    public void AgainstPath_IgnoresABendTheRouteItselfMakes()
    {
        // A stop that sits behind the bus on a bend is a normal route shape, not a
        // missed stop: only travel against the drawn line is a fault.
        var path = StraightPath();
        Assert.That(RouteRecoverySystem.IsDrivingAgainstPath(path, new Vector3(0f, 0f, 40f), Vector3.right), Is.False,
            "turning a corner is not driving against the route");
    }

    [Test]
    public void AgainstPath_HandlesADegeneratePath()
    {
        Assert.That(RouteRecoverySystem.IsDrivingAgainstPath(null, Vector3.zero, Vector3.forward), Is.True,
            "with no guidance there is nothing to follow, so the recovery path stays open");
        Assert.That(RouteRecoverySystem.IsDrivingAgainstPath(new List<Vector3>(), Vector3.zero, Vector3.forward), Is.True);
        Assert.That(RouteRecoverySystem.IsDrivingAgainstPath(StraightPath(), new Vector3(0f, 0f, 10f), Vector3.zero), Is.False,
            "a parked bus has no heading error to report");
    }

    [Test]
    public void StopServiceWaitIsBounded()
    {
        Assert.That(MissionManager.MaxServiceAnimationWaitSeconds, Is.GreaterThan(0f),
            "a stop must give its animations some time to finish");
        Assert.That(MissionManager.MaxServiceAnimationWaitSeconds, Is.LessThanOrEqualTo(15f),
            "a stuck animation must not hold the route at a stop for long");
    }

    [Test]
    public void TheRouteDoesNotSendTheBusBackToTheStopItJustServed()
    {
        // Imported routes list both platforms of a stop pair. One pair sits at the
        // same coordinates, the other a few metres away under the same name.
        var stops = new[]
        {
            new BusStopData { stopName = "Stop 1", latitude = -1.2900, longitude = 36.8200 },
            new BusStopData { stopName = "Stop 2", latitude = -1.2890, longitude = 36.8200 },
            new BusStopData { stopName = "Stop 3", latitude = -1.2890, longitude = 36.8200 },
            new BusStopData { stopName = "Stima Plaza", latitude = -1.2870, longitude = 36.8220 },
            new BusStopData { stopName = "stima plaza", latitude = -1.28697, longitude = 36.82204 },
            new BusStopData { stopName = "Aga Khan", latitude = -1.2850, longitude = 36.8230 }
        };

        Assert.That(BusRoute.NextDistinctStopIndex(stops, 0), Is.EqualTo(1),
            "the next stop along the road is a stop, not a duplicate");
        Assert.That(BusRoute.NextDistinctStopIndex(stops, 1), Is.EqualTo(3),
            "a platform recorded at the same coordinates is the stop just served");
        Assert.That(BusRoute.NextDistinctStopIndex(stops, 3), Is.EqualTo(5),
            "the opposite-direction platform of the same named stop is the same station");
        Assert.That(BusRoute.NextDistinctStopIndex(stops, 5), Is.EqualTo(6),
            "the end of the route is still reported as the end");
    }

    [Test]
    public void DistinctStopsOnTheSameStreetAreAllServed()
    {
        // Nairobi's Go Bus terminal lists three platforms about 9 m apart, each its
        // own stop: skipping them would silently drop stops from the route.
        var stops = new[]
        {
            new BusStopData { stopName = "Stop 11431881637", latitude = 30.0493149, longitude = 31.2318838 },
            new BusStopData { stopName = "Stop 11431881636", latitude = 30.0493239, longitude = 31.2319736 },
            new BusStopData { stopName = "Stop 11431881635", latitude = 30.0493284, longitude = 31.2320746 }
        };

        Assert.That(BusRoute.NextDistinctStopIndex(stops, 0), Is.EqualTo(1));
        Assert.That(BusRoute.NextDistinctStopIndex(stops, 1), Is.EqualTo(2));
    }
}
