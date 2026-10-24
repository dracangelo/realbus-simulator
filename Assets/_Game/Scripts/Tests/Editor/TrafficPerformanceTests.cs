using NUnit.Framework;
using UnityEngine;

public class TrafficPerformanceTests
{
    [Test]
    public void ActiveFleetStaysWellBelowThePoolAtEveryHour()
    {
        Assert.That(TrafficPerformancePolicy.MinActiveFraction, Is.LessThan(TrafficPerformancePolicy.MaxActiveFraction));
        Assert.That(TrafficPerformancePolicy.MaxActiveFraction, Is.LessThanOrEqualTo(0.62f),
            "the old 0.92 of a 160-car pool put 147 bodies on the road");
        Assert.That(TrafficPerformancePolicy.PoolSize(true), Is.LessThanOrEqualTo(120));
        Assert.That(TrafficPerformancePolicy.PoolSize(false), Is.LessThan(TrafficPerformancePolicy.PoolSize(true)));
        Assert.That(TrafficPerformancePolicy.MinActiveVehicles(true), Is.LessThanOrEqualTo(12));
        Assert.That(TrafficPerformancePolicy.MinActiveVehicles(false), Is.LessThan(TrafficPerformancePolicy.MinActiveVehicles(true)));

        int rushActive = Mathf.RoundToInt(TrafficPerformancePolicy.MaxActiveFraction * TrafficPerformancePolicy.PoolSize(true));
        Assert.That(rushActive, Is.LessThanOrEqualTo(80),
            "a rush-hour street must stay near half the old active fleet, not 147 cars");
    }

    [Test]
    public void PhysicsRingStaysInsideTheSplineRingAndTheDespawnBand()
    {
        Assert.That(TrafficPerformancePolicy.FullAiRadiusMeters,
            Is.LessThan(TrafficPerformancePolicy.SplineRadiusMeters));
        Assert.That(TrafficPerformancePolicy.SplineRadiusMeters,
            Is.LessThan(TrafficPerformancePolicy.HardDespawnDistanceMeters));
        Assert.That(TrafficPerformancePolicy.VisibleDespawnDistanceMeters,
            Is.LessThan(TrafficPerformancePolicy.HardDespawnDistanceMeters));
        Assert.That(TrafficPerformancePolicy.MaxFullAiVehicles, Is.LessThan(20),
            "full physics is the expensive tier and must be a minority of the fleet");
        Assert.That(TrafficPerformancePolicy.FullAiCeiling, Is.GreaterThanOrEqualTo(
            TrafficPerformancePolicy.MaxFullAiVehicles));
    }

    [Test]
    public void SplineCadenceOnlyCoarsensDistantVehicles()
    {
        Assert.That(TrafficPerformancePolicy.SplineUpdateInterval(40f), Is.EqualTo(0f),
            "near traffic still updates every frame");
        Assert.That(TrafficPerformancePolicy.SplineUpdateInterval(
            TrafficPerformancePolicy.CoarseSimulationDistanceMeters),
            Is.EqualTo(TrafficPerformancePolicy.CoarseUpdateIntervalSeconds));
        Assert.That(TrafficPerformancePolicy.SplineUpdateInterval(
            TrafficPerformancePolicy.FarSimulationDistanceMeters),
            Is.EqualTo(TrafficPerformancePolicy.FarUpdateIntervalSeconds));

        float previous = 0f;
        for (float distance = 0f; distance <= 800f; distance += 40f)
        {
            float interval = TrafficPerformancePolicy.SplineUpdateInterval(distance);
            Assert.That(interval, Is.GreaterThanOrEqualTo(previous),
                "the update interval must never get cheaper as a car gets closer");
            previous = interval;
        }
    }

    [Test]
    public void ConsumeStepWaitsForTheIntervalThenKeepsTheWholeStep()
    {
        float accumulated = 0f;
        Assert.That(SplineVehicle.ConsumeStep(ref accumulated, 0.02f, 0.05f), Is.EqualTo(0f));
        Assert.That(SplineVehicle.ConsumeStep(ref accumulated, 0.02f, 0.05f), Is.EqualTo(0f));
        Assert.That(SplineVehicle.ConsumeStep(ref accumulated, 0.02f, 0.05f),
            Is.EqualTo(0.06f).Within(1e-4f), "the accumulated time is simulated, not dropped");
        Assert.That(accumulated, Is.EqualTo(0f));

        float everyFrame = 0f;
        Assert.That(SplineVehicle.ConsumeStep(ref everyFrame, 0.016f, 0f), Is.EqualTo(0.016f),
            "a zero interval keeps the original per-frame update");
    }

    [Test]
    public void PrepareStripsWheelCollidersAndMeshCollidersButKeepsAnAuthoredBox()
    {
        var root = new GameObject("TrafficBody");
        try
        {
            root.AddComponent<Rigidbody>();
            var visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
            visual.name = "Visual";
            visual.transform.SetParent(root.transform, false);
            Object.DestroyImmediate(visual.GetComponent<Collider>());

            var wheel = visual.AddComponent<WheelCollider>();
            var mesh = visual.AddComponent<MeshCollider>();
            mesh.sharedMesh = visual.GetComponent<MeshFilter>().sharedMesh;
            var authoredBox = root.AddComponent<BoxCollider>();
            authoredBox.size = new Vector3(1.8f, 1.4f, 4.2f);

            TrafficBodyOptimizer.Report report = TrafficBodyOptimizer.Prepare(root);

            Assert.That(report.wheelCollidersRemoved, Is.EqualTo(1));
            Assert.That(report.meshCollidersRemoved, Is.EqualTo(1));
            Assert.That(root.GetComponentsInChildren<WheelCollider>(true), Is.Empty);
            Assert.That(root.GetComponentsInChildren<MeshCollider>(true), Is.Empty);
            Assert.That(root.GetComponentsInChildren<BoxCollider>(true).Length, Is.EqualTo(1),
                "an authored box is already the simplified shape");
            Assert.That(report.boxColliderAdded, Is.False);
            Assert.That(authoredBox.size, Is.EqualTo(new Vector3(1.8f, 1.4f, 4.2f)));
        }
        finally
        {
            Object.DestroyImmediate(root);
        }
    }

    [Test]
    public void PrepareAddsOneBoxColliderWhenOnlyAMeshColliderExisted()
    {
        var root = GameObject.CreatePrimitive(PrimitiveType.Cube);
        try
        {
            root.name = "TrafficBody";
            Object.DestroyImmediate(root.GetComponent<BoxCollider>());
            var mesh = root.AddComponent<MeshCollider>();
            mesh.sharedMesh = root.GetComponent<MeshFilter>().sharedMesh;

            TrafficBodyOptimizer.Report report = TrafficBodyOptimizer.Prepare(root);

            Assert.That(report.meshCollidersRemoved, Is.EqualTo(1));
            Assert.That(report.boxColliderAdded, Is.True);
            var box = root.GetComponent<BoxCollider>();
            Assert.That(box, Is.Not.Null);
            Assert.That(box.size.x, Is.EqualTo(1f).Within(0.01f));
            Assert.That(box.size.y, Is.EqualTo(1f).Within(0.01f));
            Assert.That(box.size.z, Is.EqualTo(1f).Within(0.01f));
        }
        finally
        {
            Object.DestroyImmediate(root);
        }
    }

    [Test]
    public void PrepareDisablesAnimatorAndAudioButKeepsTriggerVolumes()
    {
        var root = new GameObject("TrafficBody");
        try
        {
            var animator = root.AddComponent<Animator>();
            var audioSource = root.AddComponent<AudioSource>();
            var triggerGo = GameObject.CreatePrimitive(PrimitiveType.Cube);
            triggerGo.name = "StopTrigger";
            triggerGo.transform.SetParent(root.transform, false);
            Object.DestroyImmediate(triggerGo.GetComponent<BoxCollider>());
            var trigger = triggerGo.AddComponent<MeshCollider>();
            trigger.sharedMesh = triggerGo.GetComponent<MeshFilter>().sharedMesh;
            trigger.convex = true;
            trigger.isTrigger = true;

            TrafficBodyOptimizer.Report report = TrafficBodyOptimizer.Prepare(root);

            Assert.That(report.animatorsDisabled, Is.EqualTo(1));
            Assert.That(report.audioSourcesDisabled, Is.EqualTo(1));
            Assert.That(animator.enabled, Is.False);
            Assert.That(audioSource.enabled, Is.False);
            Assert.That(report.meshCollidersRemoved, Is.EqualTo(0),
                "a trigger volume is gameplay, not a collision mesh");
            Assert.That(triggerGo.GetComponent<MeshCollider>(), Is.Not.Null);
        }
        finally
        {
            Object.DestroyImmediate(root);
        }
    }

    [Test]
    public void PrepareAddsThePresentationCullOnlyWhenTheBodyHasLights()
    {
        var plain = new GameObject("PlainTraffic");
        var lit = new GameObject("LitTraffic");
        try
        {
            Assert.That(TrafficBodyOptimizer.Prepare(plain).lightsFound, Is.EqualTo(0));
            Assert.That(plain.GetComponent<TrafficPresentationCull>(), Is.Null,
                "a car with no lamp rig pays nothing");

            var light = lit.AddComponent<Light>();
            light.type = LightType.Point;
            TrafficBodyOptimizer.Report report = TrafficBodyOptimizer.Prepare(lit);
            Assert.That(report.lightsFound, Is.EqualTo(1));
            Assert.That(lit.GetComponent<TrafficPresentationCull>(), Is.Not.Null);

            TrafficBodyOptimizer.Prepare(lit);
            Assert.That(lit.GetComponents<TrafficPresentationCull>().Length, Is.EqualTo(1),
                "preparing the same body twice must not stack cullers");
        }
        finally
        {
            Object.DestroyImmediate(plain);
            Object.DestroyImmediate(lit);
        }
    }

    [Test]
    public void PrepareLeavesThePlayerVehicleAlone()
    {
        var root = new GameObject("PlayerBus");
        try
        {
            root.AddComponent<Rigidbody>();
            var wheel = root.AddComponent<WheelCollider>();
            var audioSource = root.AddComponent<AudioSource>();

            TrafficBodyOptimizer.Report report = TrafficBodyOptimizer.Prepare(root, isPlayerVehicle: true);

            Assert.That(report.Changed, Is.False);
            Assert.That(root.GetComponent<WheelCollider>(), Is.SameAs(wheel),
                "the bus the player drives keeps its wheel colliders");
            Assert.That(audioSource.enabled, Is.True);
        }
        finally
        {
            Object.DestroyImmediate(root);
        }
    }

    [Test]
    public void PrepareIsIdempotent()
    {
        var root = GameObject.CreatePrimitive(PrimitiveType.Cube);
        try
        {
            root.name = "TrafficBody";
            Object.DestroyImmediate(root.GetComponent<BoxCollider>());
            var mesh = root.AddComponent<MeshCollider>();
            mesh.sharedMesh = root.GetComponent<MeshFilter>().sharedMesh;

            TrafficBodyOptimizer.Report first = TrafficBodyOptimizer.Prepare(root);
            TrafficBodyOptimizer.Report second = TrafficBodyOptimizer.Prepare(root);

            Assert.That(first.Changed, Is.True);
            Assert.That(second.Changed, Is.False);
            Assert.That(root.GetComponents<BoxCollider>().Length, Is.EqualTo(1));
        }
        finally
        {
            Object.DestroyImmediate(root);
        }
    }
}
