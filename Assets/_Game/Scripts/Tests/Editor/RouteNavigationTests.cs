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
}
