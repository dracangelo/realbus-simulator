using NUnit.Framework;
using UnityEngine;

public class SceneryLodTests
{
    [Test]
    public void TinyStreetPropsLeaveTheDrawPassBeforeVegetation()
    {
        Assert.That(SceneryLodPolicy.PropRenderDistance(LiveCityCatalog.Kind.Bench),
            Is.EqualTo(SceneryLodPolicy.TinyPropRenderDistance));
        Assert.That(SceneryLodPolicy.PropRenderDistance(LiveCityCatalog.Kind.Billboard),
            Is.EqualTo(SceneryLodPolicy.TinyPropRenderDistance));
        Assert.That(SceneryLodPolicy.PropRenderDistance(LiveCityCatalog.Kind.Rock),
            Is.EqualTo(SceneryLodPolicy.TinyPropRenderDistance));
        Assert.That(SceneryLodPolicy.PropRenderDistance(LiveCityCatalog.Kind.Tree),
            Is.EqualTo(SceneryLodPolicy.VegetationRenderDistance));
        Assert.That(SceneryLodPolicy.PropRenderDistance(LiveCityCatalog.Kind.Pedestrian),
            Is.EqualTo(SceneryLodPolicy.PedestrianRenderDistance));
        Assert.That(SceneryLodPolicy.TinyPropRenderDistance, Is.LessThan(SceneryLodPolicy.VegetationRenderDistance),
            "a bench must not be drawn as far as a tree");
    }

    [Test]
    public void FarClipIsClampedIntoTheStreamedEnvelope()
    {
        Assert.That(SceneryLodPolicy.ClampFarClip(4000f), Is.EqualTo(SceneryLodPolicy.MaxFarClip));
        Assert.That(SceneryLodPolicy.ClampFarClip(0f), Is.EqualTo(SceneryLodPolicy.GameplayFarClip));
        Assert.That(SceneryLodPolicy.ClampFarClip(800f), Is.EqualTo(800f));
        Assert.That(SceneryLodPolicy.MaxFarClip, Is.EqualTo(SceneryLodPolicy.GameplayFarClip),
            "the clamp ceiling is the gameplay far plane, so no scene can widen it past the streamed city");
        Assert.That(SceneryLodPolicy.GameplayFarClip, Is.EqualTo(1000f),
            "clear haze is opaque by 950 m; a wider plane only draws the empty ring beyond it");
    }

    [Test]
    public void OnlyDetailedMeshesGetAThreeLevelGroup()
    {
        Assert.That(SceneryLodPolicy.NeedsLod(SceneryLodPolicy.BuildingLodTriangleThreshold - 1,
            SceneryLodPolicy.BuildingLodTriangleThreshold), Is.False);
        Assert.That(SceneryLodPolicy.NeedsLod(SceneryLodPolicy.BuildingLodTriangleThreshold,
            SceneryLodPolicy.BuildingLodTriangleThreshold), Is.True);
        Assert.That(SceneryLodPolicy.PropLodTriangleThreshold, Is.GreaterThan(0));
    }

    [Test]
    public void AttachBuildsThreeLevelsWithASharedProxy()
    {
        GameObject root = GameObject.CreatePrimitive(PrimitiveType.Cube);
        try
        {
            Object.DestroyImmediate(root.GetComponent<Collider>());
            var bounds = new Bounds(Vector3.zero, Vector3.one * 4f);

            Assert.That(SceneryLodBuilder.Attach(root, bounds,
                SceneryLodPolicy.BuildingLod1ScreenHeight,
                SceneryLodPolicy.BuildingLod2ScreenHeight, Color.grey), Is.True);

            var group = root.GetComponent<LODGroup>();
            Assert.That(group, Is.Not.Null);
            LOD[] lods = group.GetLODs();
            Assert.That(lods.Length, Is.EqualTo(3));
            Assert.That(lods[0].renderers.Length, Is.EqualTo(1), "LOD0 keeps the real renderer");
            Assert.That(lods[1].renderers.Length, Is.EqualTo(1), "LOD1 is the lit box");
            Assert.That(lods[2].renderers.Length, Is.EqualTo(1), "LOD2 is the unlit box");
            Assert.That(lods[0].screenRelativeTransitionHeight,
                Is.GreaterThan(lods[1].screenRelativeTransitionHeight));
            Assert.That(lods[1].screenRelativeTransitionHeight,
                Is.GreaterThan(lods[2].screenRelativeTransitionHeight));

            Transform proxy = root.transform.Find("LOD_Proxy");
            Assert.That(proxy, Is.Not.Null);
            Assert.That(proxy.Find("LOD1").GetComponent<MeshFilter>(), Is.Not.Null);
            Assert.That(proxy.Find("LOD2").GetComponent<MeshFilter>(), Is.Not.Null);
            Assert.That(proxy.GetComponentsInChildren<MeshRenderer>().Length, Is.EqualTo(2),
                "both proxy levels share the cube mesh and the two shared materials");
        }
        finally
        {
            Object.DestroyImmediate(root);
        }
    }

    [Test]
    public void AttachLeavesAnArtistLodGroupAlone()
    {
        GameObject root = GameObject.CreatePrimitive(PrimitiveType.Cube);
        try
        {
            Object.DestroyImmediate(root.GetComponent<Collider>());
            var artist = root.AddComponent<LODGroup>();
            artist.SetLODs(new[] { new LOD(.5f, root.GetComponentsInChildren<Renderer>()) });

            Assert.That(SceneryLodBuilder.Attach(root, new Bounds(Vector3.zero, Vector3.one),
                SceneryLodPolicy.PropLod1ScreenHeight,
                SceneryLodPolicy.PropLod2ScreenHeight, Color.white), Is.False);
            Assert.That(root.GetComponentsInChildren<LODGroup>(true).Length, Is.EqualTo(1));
        }
        finally
        {
            Object.DestroyImmediate(root);
        }
    }
}
