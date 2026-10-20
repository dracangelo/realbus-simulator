using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class TrafficLiveryTests
{
    [Test]
    public void PaintIsStableForTheSameModelAndSlot()
    {
        Assert.AreEqual(TrafficLivery.PaintFor("Sedan1", 3), TrafficLivery.PaintFor("Sedan1", 3));
    }

    [Test]
    public void PaintedTrafficActuallyVaries()
    {
        var seen = new HashSet<Color>();
        for (int slot = 0; slot < 40; slot++)
            seen.Add(TrafficLivery.PaintFor("Sedan1", slot));

        Assert.GreaterOrEqual(seen.Count, 8, "a pool of 40 cars should not repeat one colour");
    }

    [Test]
    public void OnlyNeutralOpaqueSlotsCountAsBodyPaint()
    {
        Material body = MakeMaterial("Sedan1_0", Color.white, 1f);
        Material glass = MakeMaterial("Sedan1_1", new Color(0.2f, 0.2f, 0.2f, 0.5f), 0.5f);
        Material lamp = MakeMaterial("Sedan1_2", new Color(1f, 0.85f, 0f), 1f);
        Material tyre = MakeMaterial("Tyre_0", Color.white, 1f);

        try
        {
            Assert.IsTrue(TrafficLivery.IsBodyPaint(body));
            Assert.IsFalse(TrafficLivery.IsBodyPaint(glass), "glass must keep its authored alpha");
            Assert.IsFalse(TrafficLivery.IsBodyPaint(lamp), "a lamp is not bodywork");
            Assert.IsFalse(TrafficLivery.IsBodyPaint(tyre), "rubber is not bodywork");
        }
        finally
        {
            Object.DestroyImmediate(body);
            Object.DestroyImmediate(glass);
            Object.DestroyImmediate(lamp);
            Object.DestroyImmediate(tyre);
        }
    }

    [Test]
    public void ApplyPaintsTheBodyAndLeavesGlassAlone()
    {
        // Real traffic renderers always carry a mesh, so the fixture uses primitives.
        // The root itself is empty: the paint belongs to the body parts, not the parent.
        var vehicle = new GameObject("LiveryTestVehicle");
        GameObject body = GameObject.CreatePrimitive(PrimitiveType.Cube);
        body.name = "Body";
        GameObject windows = GameObject.CreatePrimitive(PrimitiveType.Cube);
        windows.name = "Windows";
        body.transform.SetParent(vehicle.transform);
        windows.transform.SetParent(vehicle.transform);

        Material bodyMaterial = MakeMaterial("Sedan1_0", Color.white, 1f);
        Material glassMaterial = MakeMaterial("Sedan1_1", new Color(0.2f, 0.2f, 0.2f, 0.5f), 0.5f);
        body.GetComponent<Renderer>().sharedMaterial = bodyMaterial;
        windows.GetComponent<Renderer>().sharedMaterial = glassMaterial;

        try
        {
            Assert.IsTrue(TrafficLivery.Apply(vehicle, "Sedan1", 2), "the body slot should be painted");

            // The paint is written per material slot, so read the same way.
            var block = new MaterialPropertyBlock();
            body.GetComponent<Renderer>().GetPropertyBlock(block, 0);
            AssertColor(TrafficLivery.PaintFor("Sedan1", 2), block.GetColor("_Color"));

            var glassBlock = new MaterialPropertyBlock();
            windows.GetComponent<Renderer>().GetPropertyBlock(glassBlock, 0);
            Assert.AreEqual(Color.clear, glassBlock.GetColor("_Color"), "glass must not receive a paint block");
        }
        finally
        {
            Object.DestroyImmediate(vehicle);
            Object.DestroyImmediate(bodyMaterial);
            Object.DestroyImmediate(glassMaterial);
        }
    }

    /// <summary>The property block round-trips through Vector4, so compare with a tolerance.</summary>
    static void AssertColor(Color expected, Color actual)
    {
        Assert.AreEqual(expected.r, actual.r, 1e-4f, "red");
        Assert.AreEqual(expected.g, actual.g, 1e-4f, "green");
        Assert.AreEqual(expected.b, actual.b, 1e-4f, "blue");
        Assert.AreEqual(expected.a, actual.a, 1e-4f, "alpha");
    }

    static Material MakeMaterial(string name, Color color, float alpha)
    {
        Shader shader = Shader.Find("Standard");
        Assert.IsNotNull(shader, "the built-in Standard shader is required by the traffic import");
        var material = new Material(shader) { name = name };
        color.a = alpha;
        material.color = color;
        return material;
    }
}
