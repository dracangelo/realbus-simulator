using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Drives every signal at one junction from a single two-phase clock.
///
/// Each approach is grouped onto one of two axes by the direction traffic
/// arrives from, which is what makes opposite approaches (both directions of
/// the same street) share a green while the crossing street is held at red.
/// The lamps on a junction never advance their own cycle, so the phase read by
/// the traffic graph and the lens the player sees cannot disagree.
/// </summary>
public class TrafficSignalJunction : MonoBehaviour
{
    public const float DefaultGreenSeconds = 16f;
    public const float DefaultAmberSeconds = 4f;
    public const float DefaultAllRedSeconds = 2f;

    public enum Phase { AxisAGreen, AxisAAmber, AllRedAfterA, AxisBGreen, AxisBAmber, AllRedAfterB }

    [Header("Timing")]
    public float greenSeconds = DefaultGreenSeconds;
    public float amberSeconds = DefaultAmberSeconds;
    [Tooltip("Held red on both axes between phases, so a vehicle that entered on amber clears the box before the crossing street is released.")]
    public float allRedSeconds = DefaultAllRedSeconds;
    [Tooltip("Staggers neighbouring junctions so a route does not stop at every one of them at once.")]
    public float startOffsetSeconds;

    [Header("Approaches")]
    [Tooltip("Approaches whose traffic arrives along the junction's first axis.")]
    public List<TrafficLight> axisA = new List<TrafficLight>();
    [Tooltip("Approaches crossing the first axis.")]
    public List<TrafficLight> axisB = new List<TrafficLight>();

    float clock;

    /// <summary>Phase shown by the junction right now. Exposed for diagnostics and tests.</summary>
    public Phase CurrentPhase { get; private set; }
    public int SignalCount => (axisA != null ? axisA.Count : 0) + (axisB != null ? axisB.Count : 0);

    void Start()
    {
        clock = Mathf.Max(0f, startOffsetSeconds);
        Apply(PhaseAt(clock, greenSeconds, amberSeconds, allRedSeconds));
    }

    void Update()
    {
        clock += Time.deltaTime;
        var phase = PhaseAt(clock, greenSeconds, amberSeconds, allRedSeconds);
        // Writing every lamp every frame would allocate a property block per lens
        // for a state that only changes a few times a minute.
        if (phase != CurrentPhase) Apply(phase);
    }

    public void Configure(IList<TrafficLight> firstAxis, IList<TrafficLight> secondAxis, float offsetSeconds = 0f)
    {
        axisA = new List<TrafficLight>();
        axisB = new List<TrafficLight>();
        if (firstAxis != null)
            foreach (var light in firstAxis)
                if (light != null) axisA.Add(light);
        if (secondAxis != null)
            foreach (var light in secondAxis)
                if (light != null) axisB.Add(light);

        startOffsetSeconds = Mathf.Max(0f, offsetSeconds);
        clock = startOffsetSeconds;
        Apply(PhaseAt(clock, greenSeconds, amberSeconds, allRedSeconds));
    }

    /// <summary>Applies a phase immediately, without waiting for the clock. Used when restoring a junction's state.</summary>
    public void SetPhase(Phase phase) => Apply(phase);

    void Apply(Phase phase)
    {
        CurrentPhase = phase;
        ApplyAxis(axisA, phase, 0);
        ApplyAxis(axisB, phase, 1);
    }

    static void ApplyAxis(List<TrafficLight> lights, Phase phase, int axis)
    {
        if (lights == null) return;
        var state = ShowsGreen(phase, axis) ? TrafficLight.LightState.Green
            : ShowsAmber(phase, axis) ? TrafficLight.LightState.Amber
            : TrafficLight.LightState.Red;

        for (int i = 0; i < lights.Count; i++)
        {
            var light = lights[i];
            if (light == null) continue;
            light.externallyControlled = true;
            light.SetState(state);
        }
    }

    /// <summary>Length of a full both-axes cycle. Pure, so the phase plan is testable.</summary>
    public static float CycleLength(float green, float amber, float allRed)
    {
        green = Mathf.Max(.01f, green);
        amber = Mathf.Max(.01f, amber);
        allRed = Mathf.Max(0f, allRed);
        return 2f * (green + amber + allRed);
    }

    /// <summary>
    /// The phase at <paramref name="seconds"/> into a cycle: axis A green, axis A
    /// amber, all red, axis B green, axis B amber, all red. Pure.
    /// </summary>
    public static Phase PhaseAt(float seconds, float green, float amber, float allRed)
    {
        green = Mathf.Max(.01f, green);
        amber = Mathf.Max(.01f, amber);
        allRed = Mathf.Max(0f, allRed);

        float t = Mathf.Repeat(seconds, CycleLength(green, amber, allRed));
        if (t < green) return Phase.AxisAGreen;
        t -= green;
        if (t < amber) return Phase.AxisAAmber;
        t -= amber;
        if (t < allRed) return Phase.AllRedAfterA;
        t -= allRed;
        if (t < green) return Phase.AxisBGreen;
        t -= green;
        if (t < amber) return Phase.AxisBAmber;
        return Phase.AllRedAfterB;
    }

    /// <summary>True while the axis may proceed (green or the amber clearance).</summary>
    public static bool AllowsAxis(Phase phase, int axis)
        => axis == 0
            ? phase == Phase.AxisAGreen || phase == Phase.AxisAAmber
            : phase == Phase.AxisBGreen || phase == Phase.AxisBAmber;

    public static bool ShowsGreen(Phase phase, int axis)
        => axis == 0 ? phase == Phase.AxisAGreen : phase == Phase.AxisBGreen;

    public static bool ShowsAmber(Phase phase, int axis)
        => axis == 0 ? phase == Phase.AxisAAmber : phase == Phase.AxisBAmber;

    /// <summary>
    /// Groups an arrival direction onto one of two axes. Directions more than 45°
    /// from the first axis belong to the second, so opposite directions (180°
    /// apart) always land on the same axis and cross traffic never does.
    /// </summary>
    public static int AxisForHeading(Vector3 travelDirection)
    {
        travelDirection.y = 0f;
        if (travelDirection.sqrMagnitude < .0001f) return 0;
        float heading = Mathf.Repeat(Mathf.Atan2(travelDirection.x, travelDirection.z) * Mathf.Rad2Deg, 180f);
        return heading < 45f || heading >= 135f ? 0 : 1;
    }
}
