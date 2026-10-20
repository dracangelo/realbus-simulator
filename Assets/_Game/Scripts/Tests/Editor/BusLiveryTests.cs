using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class BusLiveryTests
{
    [Test]
    public void LiveryPresets_OfferMultipleCinematicSchemes()
    {
        var presets = LiveryEditor.Presets;
        Assert.That(presets.Count, Is.GreaterThanOrEqualTo(10), "the garage should offer more than the old single green shell");

        var ids = new HashSet<string>();
        var bodies = new HashSet<Color>();
        for (int i = 0; i < presets.Count; i++)
        {
            LiveryPreset preset = presets[i];
            Assert.That(preset, Is.Not.Null);
            Assert.That(preset.data, Is.Not.Null, preset.presetId);
            Assert.That(preset.data.zoneColors, Is.Not.Null, preset.presetId);
            Assert.That(preset.data.zoneColors.Length, Is.EqualTo(6), preset.presetId);
            Assert.That(ids.Add(preset.presetId), Is.True, "duplicate preset id " + preset.presetId);
            bodies.Add(preset.data.zoneColors[2]);
        }

        Assert.That(ids.Count, Is.EqualTo(presets.Count));
        Assert.That(bodies.Count, Is.GreaterThanOrEqualTo(8), "the presets should not collapse to a handful of body colours");
    }

    [Test]
    public void DefaultLivery_IsStableAndActuallyPainted()
    {
        const string busId = "fleet.bis_tua";
        LiveryData first = LiveryEditor.DefaultLiveryFor(busId);
        LiveryData second = LiveryEditor.DefaultLiveryFor(busId);
        Assert.That(first, Is.Not.Null);
        Assert.That(second, Is.Not.Null);

        bool painted = false;
        for (int i = 0; i < 6; i++)
        {
            Assert.That(second.zoneColors[i], Is.EqualTo(first.zoneColors[i]), "default livery must be deterministic");
            if (ColorDistance(first.zoneColors[i], Color.white) > 0.05f) painted = true;
        }
        Assert.That(painted, Is.True, "a new bus must not start as an all-white shell");
    }

    [Test]
    public void DefaultLivery_VariesBetweenBusModels()
    {
        var bodies = new HashSet<Color>();
        string[] ids =
        {
            "fleet.bis_tua", "fleet.ecoline", "fleet.jetset", "fleet.cito",
            "fleet.coach", "fleet.laksana", "fleet.toy_bus", "fleet.zedone"
        };
        for (int i = 0; i < ids.Length; i++)
            bodies.Add(LiveryEditor.DefaultLiveryFor(ids[i]).zoneColors[2]);

        Assert.That(bodies.Count, Is.GreaterThan(1), "different buses should not all default to the same paint");
    }

    [Test]
    public void BusPaintShader_IsAvailable()
    {
        Shader shader = Shader.Find("RealBus/BusPaint");
        Assert.That(shader, Is.Not.Null, "RealBus/BusPaint must be imported so the livery is not a flat fallback");
        Assert.That(shader.name, Is.EqualTo("RealBus/BusPaint"));
    }

    [Test]
    public void BusLiveryPaint_BindsRepairedBodySlots()
    {
        var root = new GameObject("PaintTestBus");
        var visual = new GameObject("Visual");
        visual.transform.SetParent(root.transform, false);

        var body = GameObject.CreatePrimitive(PrimitiveType.Cube);
        body.name = "Bodi";
        body.transform.SetParent(visual.transform, false);
        body.transform.localScale = new Vector3(2.5f, 3.2f, 12f);
        var renderer = body.GetComponent<Renderer>();
        var sourceMaterial = new Material(Shader.Find("Standard")) { name = "BodiMat", color = Color.white };
        renderer.sharedMaterial = sourceMaterial;

        var repair = root.AddComponent<ImportedVehicleVisualRepair>();
        repair.RepairNow(visual.transform);
        Assert.That(repair.BodyPaintSlots.Count, Is.GreaterThan(0), "the repair pass must remember which slots are bodywork");

        var paint = root.AddComponent<BusLiveryPaint>();
        LiveryData data = LiveryEditor.DefaultLiveryFor("test.bus");
        paint.Bind(visual.transform, data, "test.bus");

        Assert.That(paint.HasPaint, Is.True);
        Assert.That(paint.PaintedSlotCount, Is.GreaterThan(0));
        Assert.That(renderer.sharedMaterial, Is.SameAs(paint.PaintMaterial));
        Assert.That(paint.PaintMaterial.shader.name, Is.EqualTo("RealBus/BusPaint"));
        Assert.That(paint.PaintMaterial.GetColor("_Zone2"), Is.Not.EqualTo(Color.white));

        Object.DestroyImmediate(root);
        Object.DestroyImmediate(sourceMaterial);
    }

    [Test]
    public void ImportedBusBodySlots_AreRecordedForTheRealPlayerModel()
    {
        BusSpec spec = Resources.Load<BusSpec>("BusSpecs/Imported/BIS_TUA_b809d991");
        Assert.That(spec, Is.Not.Null, "the default model-backed bus spec should be in Resources");
        Assert.That(spec.drivablePrefab, Is.Not.Null);

        GameObject instance = Object.Instantiate(spec.drivablePrefab);
        try
        {
            var bus = instance.GetComponent<BusController>();
            var repair = instance.GetComponent<ImportedVehicleVisualRepair>();
            Assert.That(bus, Is.Not.Null);
            // The drivable prefabs are generated without the repair component;
            // SceneBootstrap adds it at runtime, so mirror that here.
            if (repair == null) repair = instance.AddComponent<ImportedVehicleVisualRepair>();

            Transform visual = bus.modelRoot != null ? bus.modelRoot : instance.transform;
            repair.RepairNow(visual);
            Assert.That(repair.BodyPaintSlots.Count, Is.GreaterThan(0),
                "every untextured body panel should be available to the livery shader");

            for (int i = 0; i < repair.BodyPaintSlots.Count; i++)
            {
                ImportedVehicleVisualRepair.BodyPaintSlot slot = repair.BodyPaintSlots[i];
                string label = (slot.renderer != null ? slot.renderer.name : "") + " " +
                               (slot.renderer != null && slot.renderer.sharedMaterial != null
                                   ? slot.renderer.sharedMaterial.name
                                   : "");
                label = label.ToLowerInvariant();
                Assert.That(label, Does.Not.Contain("glass"), "glass must keep its transparency");
                Assert.That(label, Does.Not.Contain("kursi"), "seats are cabin trim, not bodywork");
                Assert.That(label, Does.Not.Contain("dashboard"), "dashboard is cabin trim, not bodywork");
                Assert.That(label, Does.Not.Contain("wheel"), "wheels are not bodywork");
                Assert.That(label, Does.Not.Contain("tyre"), "tyres are not bodywork");
            }

            var paint = instance.AddComponent<BusLiveryPaint>();
            paint.Bind(visual, LiveryEditor.DefaultLiveryFor(spec.busId), spec.busId);
            Assert.That(paint.HasPaint, Is.True);
            Assert.That(paint.PaintedSlotCount, Is.GreaterThan(0));
        }
        finally
        {
            Object.DestroyImmediate(instance);
        }
    }

    [Test]
    public void Overtaking_StaysInsideTheDirectedCarriageway()
    {
        var root = new GameObject("OvertakeGraph");
        var graph = root.AddComponent<AIRoadGraph>();
        graph.drivesOnLeft = true;
        var a = new GameObject("A").transform;
        var b = new GameObject("B").transform;
        b.position = Vector3.forward * 20f;
        graph.nodes = new[]
        {
            new AIRoadGraph.RoadNode
            {
                point = a, nextNodeIndices = new[] { 1 },
                outgoingLaneCounts = new[] { 2 }, outgoingLaneWidths = new[] { 3.3f },
                outgoingOneWays = new[] { false }
            },
            new AIRoadGraph.RoadNode
            {
                point = b, nextNodeIndices = new[] { 0 },
                outgoingLaneCounts = new[] { 2 }, outgoingLaneWidths = new[] { 3.3f },
                outgoingOneWays = new[] { false }
            }
        };

        // In the inner lane there is no legal pass on a two-way road: moving
        // outboard would be undertaking along the kerb. Wait behind.
        Assert.That(AIVehicleController.OvertakeShift(graph, 0, 1, 0, 3f), Is.EqualTo(0f));

        // The outer (kerb) lane passes on the offside by moving inward.
        float outer = graph.GetLaneOffset(0, 1, 1);
        float inwardShift = AIVehicleController.OvertakeShift(graph, 0, 1, 1, 3f);
        Assert.That(inwardShift, Is.GreaterThan(0f), "left-hand traffic passes on the right (inward)");
        Assert.That(outer + inwardShift, Is.LessThan(0f), "keep the pass on the left carriageway");

        // The outer lane is already at the kerb, so there is nowhere to yield.
        Assert.That(AIVehicleController.KerbShift(graph, 0, 1, 1, 3f), Is.EqualTo(0f));
        // The inner lane yields outward toward the kerb.
        Assert.That(AIVehicleController.KerbShift(graph, 0, 1, 0, 3f), Is.LessThan(0f));

        // On a one-way carriageway the pass uses the offside (right in
        // left-hand traffic), falling back to the other side at the edge.
        graph.nodes[0].outgoingOneWays = new[] { true };
        Assert.That(AIVehicleController.OvertakeShift(graph, 0, 1, 0, 3f), Is.GreaterThan(0f));
        Assert.That(AIVehicleController.OvertakeShift(graph, 0, 1, 1, 3f), Is.LessThan(0f));

        // A single-lane carriageway has nowhere to pass or yield.
        graph.nodes[0].outgoingOneWays = new[] { false };
        graph.nodes[0].outgoingLaneCounts = new[] { 1 };
        Assert.That(AIVehicleController.OvertakeShift(graph, 0, 1, 0, 3f), Is.EqualTo(0f));
        Assert.That(AIVehicleController.KerbShift(graph, 0, 1, 0, 3f), Is.EqualTo(0f));
        Assert.That(AIVehicleController.CanOvertakeOn(graph, 0, 1), Is.False);

        Object.DestroyImmediate(root);
        Object.DestroyImmediate(a.gameObject);
        Object.DestroyImmediate(b.gameObject);
    }

    [Test]
    public void LaneCenteringGain_IsGentle()
    {
        Assert.That(AIVehicleController.LaneCenteringGain, Is.InRange(0.3f, 1.2f),
            "a high pure-pursuit gain makes traffic weave across its lane");
    }

    static float ColorDistance(Color a, Color b)
    {
        return Mathf.Abs(a.r - b.r) + Mathf.Abs(a.g - b.g) + Mathf.Abs(a.b - b.b);
    }
}
