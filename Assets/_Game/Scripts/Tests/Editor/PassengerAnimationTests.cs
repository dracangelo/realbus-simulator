using NUnit.Framework;
using UnityEngine;

public class PassengerAnimationTests
{
    GameObject root;
    Material bodyMaterial;
    Material skinMaterial;
    Material legMaterial;
    Material wheelchairMaterial;

    [SetUp]
    public void SetUp()
    {
        root = new GameObject("PassengerAnimationTest");
        bodyMaterial = NewMaterial();
        skinMaterial = NewMaterial();
        legMaterial = NewMaterial();
        wheelchairMaterial = NewMaterial();
    }

    [TearDown]
    public void TearDown()
    {
        if (root != null) Object.DestroyImmediate(root);
        if (bodyMaterial != null) Object.DestroyImmediate(bodyMaterial);
        if (skinMaterial != null) Object.DestroyImmediate(skinMaterial);
        if (legMaterial != null) Object.DestroyImmediate(legMaterial);
        if (wheelchairMaterial != null) Object.DestroyImmediate(wheelchairMaterial);
    }

    static Material NewMaterial()
    {
        return new Material(BuiltInShaders.Lit);
    }

    [Test]
    public void ResolveState_MapsSimulationFlagsToPoses()
    {
        var agent = new PassengerAgent(0, 2, 60f, false, PassengerArchetype.Commuter);

        Assert.That(PassengerVisual.ResolveState(null, true), Is.EqualTo(PassengerVisualState.Idle));
        Assert.That(PassengerVisual.ResolveState(agent, true), Is.EqualTo(PassengerVisualState.Idle));

        agent.isBoarding = true;
        Assert.That(PassengerVisual.ResolveState(agent, false), Is.EqualTo(PassengerVisualState.Board));

        agent.isBoarding = false;
        agent.isSeated = true;
        Assert.That(PassengerVisual.ResolveState(agent, false), Is.EqualTo(PassengerVisualState.Seated));

        agent.isSeated = false;
        agent.isAlighting = true;
        Assert.That(PassengerVisual.ResolveState(agent, false), Is.EqualTo(PassengerVisualState.Exit));

        agent.isAlighting = false;
        agent.isWheelchairPassenger = true;
        Assert.That(PassengerVisual.ResolveState(agent, true), Is.EqualTo(PassengerVisualState.Seated));
    }

    [Test]
    public void ProceduralVisual_BuildsArticulatedProxyAndResetsOnRelease()
    {
        PassengerVisual visual = PassengerVisual.CreateProcedural(root.transform, bodyMaterial,
            skinMaterial, legMaterial, wheelchairMaterial);

        Assert.That(visual.HasProceduralRig, Is.True);
        Assert.That(visual.RendererCount, Is.GreaterThanOrEqualTo(6),
            "the proxy needs a body, head, two arms and two legs");

        var agent = new PassengerAgent(0, 3, 60f, false, PassengerArchetype.Student);
        visual.Bind(agent, PassengerVisual.ResolveState(agent, false), false);

        Assert.That(visual.Agent, Is.SameAs(agent));
        Assert.That(visual.CurrentState, Is.EqualTo(PassengerVisualState.Idle));

        visual.PlayReaction("Wave");
        Assert.That(visual.ReactionActive, Is.True);
        for (int i = 0; i < 20; i++)
            visual.Tick(0.1f, 5f, true);

        Assert.That(visual.ReactionActive, Is.False,
            "the reaction must end instead of sticking to the proxy");

        visual.Release();

        Assert.That(visual.Agent, Is.Null);
        Assert.That(visual.CurrentState, Is.EqualTo(PassengerVisualState.Idle));
        Assert.That(visual.ReactionActive, Is.False);
    }

    [Test]
    public void ProceduralVisual_WheelchairUsesSeatedPoseAndWheelchair()
    {
        PassengerVisual visual = PassengerVisual.CreateProcedural(root.transform, bodyMaterial,
            skinMaterial, legMaterial, wheelchairMaterial);
        var agent = new PassengerAgent(0, 2, 60f, true, PassengerArchetype.Elderly);

        visual.Bind(agent, PassengerVisual.ResolveState(agent, false), false);
        visual.Tick(0.1f, 4f, true);

        Assert.That(visual.CurrentState, Is.EqualTo(PassengerVisualState.Seated));
        Assert.That(visual.WheelchairVisible, Is.True);

        visual.Release();
        Assert.That(visual.WheelchairVisible, Is.False,
            "a reused proxy must not keep the previous passenger's wheelchair");
    }

    [Test]
    public void WalkStateAnimatesLegs_AndFarLodHidesTheProxy()
    {
        PassengerVisual visual = PassengerVisual.CreateProcedural(root.transform, bodyMaterial,
            skinMaterial, legMaterial, wheelchairMaterial);
        var agent = new PassengerAgent(0, 2, 60f, false, PassengerArchetype.Commuter);
        visual.Bind(agent, PassengerVisualState.Walk, false);

        for (int i = 0; i < 30; i++)
            visual.Tick(1f / 60f, 5f, true);

        Transform leg = visual.transform.Find("Leg L Pivot");
        Assert.That(leg, Is.Not.Null);
        Assert.That(Quaternion.Angle(leg.localRotation, Quaternion.identity), Is.GreaterThan(1f),
            "walking must swing the leg instead of leaving the proxy frozen");
        Assert.That(visual.RenderersVisible, Is.True);

        // Past the crowd cutoff the pose is frozen and the renderers are hidden.
        visual.Tick(0.1f, CrowdPerformancePolicy.AnimationCutoffMeters + 50f, false);
        Assert.That(visual.RenderersVisible, Is.False);

        visual.Tick(0.1f, 5f, true);
        Assert.That(visual.RenderersVisible, Is.True, "coming back into range must show the proxy again");
    }

    [Test]
    public void ReusedAgentInstance_ResetsVisualStateOnRebind()
    {
        PassengerVisual visual = PassengerVisual.CreateProcedural(root.transform, bodyMaterial,
            skinMaterial, legMaterial, wheelchairMaterial);

        var agent = new PassengerAgent(0, 2, 60f, true, PassengerArchetype.Elderly);
        visual.Bind(agent, PassengerVisual.ResolveState(agent, false), false);
        visual.Tick(0.1f, 4f, true);
        Assert.That(visual.WheelchairVisible, Is.True);

        // The manager recycles the very same object; the visual must still treat
        // it as a new passenger instead of keeping the wheelchair and seated pose.
        agent.ResetForReuse(1, 3, 45f, false, PassengerArchetype.Student);
        visual.Bind(agent, PassengerVisual.ResolveState(agent, true), true);

        Assert.That(visual.Agent, Is.SameAs(agent));
        Assert.That(visual.CurrentState, Is.EqualTo(PassengerVisualState.Idle));
        Assert.That(visual.WheelchairVisible, Is.False,
            "same-instance reuse must reset the previous passenger's wheelchair");
    }

    [Test]
    public void AnimatorParameterContractNamesAndStateIndicesAreStable()
    {
        Assert.That(PassengerAnimatorParameters.Speed, Is.EqualTo("Speed"));
        Assert.That(PassengerAnimatorParameters.Seated, Is.EqualTo("Seated"));
        Assert.That(PassengerAnimatorParameters.Boarding, Is.EqualTo("Boarding"));
        Assert.That(PassengerAnimatorParameters.Alighting, Is.EqualTo("Alighting"));
        Assert.That(PassengerAnimatorParameters.State, Is.EqualTo("State"));
        Assert.That(PassengerAnimatorParameters.Reaction, Is.EqualTo("Reaction"));

        Assert.That(PassengerAnimatorParameters.StateIndex(PassengerVisualState.Idle), Is.EqualTo(0));
        Assert.That(PassengerAnimatorParameters.StateIndex(PassengerVisualState.Walk), Is.EqualTo(1));
        Assert.That(PassengerAnimatorParameters.StateIndex(PassengerVisualState.Board), Is.EqualTo(2));
        Assert.That(PassengerAnimatorParameters.StateIndex(PassengerVisualState.Seated), Is.EqualTo(3));
        Assert.That(PassengerAnimatorParameters.StateIndex(PassengerVisualState.Exit), Is.EqualTo(4));
        Assert.That(PassengerAnimatorParameters.HasParameter(null, "Speed",
            AnimatorControllerParameterType.Float), Is.False);
    }
}
