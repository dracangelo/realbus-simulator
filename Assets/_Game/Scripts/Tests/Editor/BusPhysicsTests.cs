using NUnit.Framework;
using UnityEngine;

public class BusPhysicsTests
{
    [Test]
    public void ParkingBrakePreservesFrontServiceBrakesAndReleasesRearBrakes()
    {
        var root = new GameObject("Brake test");
        try
        {
            var bus = root.AddComponent<BusController>();
            bus.allWheels = new WheelCollider[6];
            for (int index = 0; index < 6; index++)
            {
                var wheel = new GameObject("Wheel");
                wheel.transform.SetParent(root.transform);
                bus.allWheels[index] = wheel.AddComponent<WheelCollider>();
            }
            bus.rearWheels = new[] { bus.allWheels[2], bus.allWheels[3], bus.allWheels[4], bus.allWheels[5] };
            bus.brakeInput = 1f;
            bus.SetParkingBrake(true);
            Invoke(bus, "ApplyBrakes");
            Invoke(bus, "ApplyParkingBrake");
            Assert.AreEqual(2000f, bus.allWheels[0].brakeTorque, 0.01f);
            Assert.AreEqual(12000f, bus.allWheels[2].brakeTorque, 0.01f);
            bus.SetParkingBrake(false);
            bus.brakeInput = 0f;
            Invoke(bus, "ApplyBrakes");
            Invoke(bus, "ApplyParkingBrake");
            Assert.AreEqual(0f, bus.allWheels[2].brakeTorque);
        }
        finally { Object.DestroyImmediate(root); }
    }

    [Test]
    public void MobileReleaseClearsAllInputs()
    {
        var root = new GameObject("Input test");
        try
        {
            var bus = root.AddComponent<BusController>();
            bus.SetMobileInput(1f, 1f, -1f);
            bus.SetMobileInput(0f, 0f, 0f);
            Assert.AreEqual(0f, bus.throttleInput);
            Assert.AreEqual(0f, bus.brakeInput);
            Assert.AreEqual(0f, bus.steerInput);
        }
        finally { Object.DestroyImmediate(root); }
    }

    static void Invoke(BusController bus, string name)
    {
        typeof(BusController).GetMethod(name, System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.NonPublic).Invoke(bus, null);
    }

    [Test]
    public void SteeringMirrorsAndInnerWheelTurnsFurther()
    {
        Vector2 right = BusController.AckermannAngles(30f, 6.2f, 2.1f);
        Vector2 left = BusController.AckermannAngles(-30f, 6.2f, 2.1f);
        Assert.Greater(right.y, right.x);
        Assert.AreEqual(-right.y, left.x, 0.001f);
        Assert.AreEqual(-right.x, left.y, 0.001f);
        Assert.AreEqual(Vector2.zero, BusController.AckermannAngles(0f, 6.2f, 2.1f));
    }

    [Test]
    public void AutomaticTransmissionHonorsThresholdsAndReverse()
    {
        var transmission = ScriptableObject.CreateInstance<TransmissionSystem>();
        try
        {
            Assert.IsFalse(transmission.UpdateAutomatic(1999f, 2500f));
            Assert.IsTrue(transmission.UpdateAutomatic(2000f, 2500f));
            Assert.AreEqual(1, transmission.currentGear);
            Assert.IsTrue(transmission.UpdateAutomatic(875f, 2500f));
            Assert.AreEqual(0, transmission.currentGear);
            transmission.currentGear = -1;
            Assert.IsFalse(transmission.UpdateAutomatic(2500f, 2500f));
            Assert.AreEqual(-4.5f, transmission.GetCurrentRatio());
        }
        finally { Object.DestroyImmediate(transmission); }
    }

    [Test]
    public void DragOpposesVelocityAndScalesWithSpeedSquared()
    {
        Vector3 first = AerodynamicsSystem.DragForce(Vector3.forward * 10f, 0.65f, 8.5f, 1.225f);
        Vector3 second = AerodynamicsSystem.DragForce(Vector3.forward * 20f, 0.65f, 8.5f, 1.225f);
        Assert.Less(first.z, 0f);
        Assert.AreEqual(first.z * 4f, second.z, 0.001f);
        Assert.AreEqual(Vector3.zero, AerodynamicsSystem.DragForce(Vector3.zero, 1f, 1f, 1f));
    }

    [Test]
    public void PassengerCenterIsMassWeighted()
    {
        Vector3 center = PassengerLoadDynamics.CombinedCenter(12000f, new Vector3(0f, 1.6f, 0f),
            6000f, new Vector3(0.3f, 2.2f, 0f));
        Assert.AreEqual(0.1f, center.x, 0.0001f);
        Assert.AreEqual(1.8f, center.y, 0.0001f);
    }

    [Test]
    public void EngineHasTimelineTorqueCurveByDefault()
    {
        var engine = ScriptableObject.CreateInstance<EngineSystem>();
        try { Assert.AreEqual(1200f, engine.GetTorque(1200f), 0.001f); }
        finally { Object.DestroyImmediate(engine); }
    }
}
