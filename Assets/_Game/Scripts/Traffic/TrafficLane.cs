using UnityEngine;

/// <summary>
/// One directed lane of the road graph, resolved into world space.
///
/// Every consumer that has to know where a vehicle, a stop, a queue or a signal
/// belongs builds this frame and reads it: the two simulation tiers, the pool's
/// spawn placement, the stop prop spawner, waiting passengers and the signal
/// spawner. Before this existed each of them re-derived the lane line with its own
/// cross product and its own left/right branch, which is how a mirrored sign could
/// put a shelter on the oncoming kerb while every individual file looked correct.
/// </summary>
public readonly struct LaneFrame
{
    /// <summary>Lane-line point level with the link's source node.</summary>
    public readonly Vector3 Start;
    /// <summary>Lane-line point level with the link's target node.</summary>
    public readonly Vector3 End;
    /// <summary>Unit planar travel direction, <see cref="Start"/> to <see cref="End"/>.</summary>
    public readonly Vector3 Direction;
    /// <summary>Planar length of the link.</summary>
    public readonly float Length;
    /// <summary>Signed metres from the OSM centreline. Positive is right of travel.</summary>
    public readonly float Offset;
    /// <summary>Lane spacing this link was resolved with, including the model-fit floor.</summary>
    public readonly float Width;

    public LaneFrame(Vector3 start, Vector3 end, Vector3 direction, float length, float offset,
        float width)
    {
        Start = start;
        End = end;
        Direction = direction;
        Length = length;
        Offset = offset;
        Width = width;
    }

    public bool IsValid => Length > .01f && Direction.sqrMagnitude > .25f;

    /// <summary>Unit vector to the right of travel. A positive <see cref="Offset"/> lies along it.</summary>
    public Vector3 Right => Vector3.Cross(Vector3.up, Direction);

    /// <summary>
    /// A point <paramref name="t"/> of the way along the lane line, clamped to the
    /// link. Height is interpolated between the two node ends rather than held flat,
    /// so a body spawned or measured on a ramp sits on the ramp.
    /// </summary>
    public Vector3 PointAt(float t) => Vector3.Lerp(Start, End, Mathf.Clamp01(t));

    /// <summary>Signed lateral distance of a world point from this lane line.</summary>
    public float LateralOf(Vector3 worldPoint)
    {
        Vector3 flat = worldPoint - Start;
        flat.y = 0f;
        return Vector3.Dot(flat, Right);
    }

    /// <summary>How far along the link a world point projects, clamped to [0,1].</summary>
    public float AlongOf(Vector3 worldPoint)
    {
        Vector3 flat = worldPoint - Start;
        flat.y = 0f;
        return Length > .01f ? Mathf.Clamp01(Vector3.Dot(flat, Direction) / Length) : 0f;
    }
}

/// <summary>
/// The single source of truth for lane ordering, lane geometry and the side of the
/// road traffic keeps to.
///
/// Lane indices are graph indices, not screen directions. Their order depends on the
/// link: on a two-way link the lanes of one direction are laid out from the centre
/// line outwards, so a higher index is further from the centre line; on a one-way
/// link they are laid out across the whole carriageway, so the order follows the
/// driving side instead. That asymmetry is real and cannot be removed without moving
/// every car, but it only needs to be expressed ONCE. <see cref="KerbwardStep"/>
/// is that expression, and cruise, pass and kerb-yield lanes are all derived from
/// it rather than each re-deriving `DrivesOnLeft ? current + 1 : current - 1`.
/// </summary>
public static class TrafficLane
{
    /// <summary>
    /// Direction of the kerb relative to the travel direction: -1 when traffic keeps
    /// left (the kerb is on the left), +1 when it keeps right. Every lateral sign in
    /// the traffic stack comes from here, so a stop, a signal head and a spawned car
    /// can never disagree about which side of the road they belong on.
    /// </summary>
    public static float SideSign(bool drivesOnLeft) => drivesOnLeft ? -1f : 1f;

    /// <summary>Unit vector from the carriageway centreline toward the nearside kerb.</summary>
    public static Vector3 KerbDirection(Vector3 travel, bool drivesOnLeft)
    {
        Vector3 forward = Vector3.ProjectOnPlane(travel, Vector3.up);
        if (forward.sqrMagnitude < 1e-4f) return Vector3.zero;
        return Vector3.Cross(Vector3.up, forward.normalized) * SideSign(drivesOnLeft);
    }

    /// <summary>
    /// The lane-index step that moves toward the nearside kerb on this directed link.
    /// Two-way directions are laid out from the centre line outwards, so the kerb is
    /// always the higher index. A one-way carriageway is laid out across the road in
    /// world order, so the step follows the driving side.
    /// </summary>
    public static int KerbwardStep(AIRoadGraph graph, int fromNode, int toNode)
    {
        if (graph == null) return 1;
        if (!graph.IsOneWay(fromNode, toNode)) return 1;
        return graph.DrivesOnLeft ? -1 : 1;
    }

    /// <summary>Moves one lane toward the kerb, or -1 when already in the kerb lane.</summary>
    public static int KerbwardLane(AIRoadGraph graph, int fromNode, int toNode, int laneIndex)
    {
        return Step(graph, fromNode, toNode, laneIndex, KerbwardStep(graph, fromNode, toNode));
    }

    /// <summary>Moves one lane toward the centre line, or -1 when there is none.</summary>
    public static int CentrewardLane(AIRoadGraph graph, int fromNode, int toNode, int laneIndex)
    {
        return Step(graph, fromNode, toNode, laneIndex, -KerbwardStep(graph, fromNode, toNode));
    }

    static int Step(AIRoadGraph graph, int fromNode, int toNode, int laneIndex, int step)
    {
        if (graph == null || !graph.IsValidNode(fromNode) || !graph.IsValidNode(toNode)) return -1;
        if (graph.IsSharedLane(fromNode, toNode)) return -1;
        int lanes = Mathf.Max(1, graph.GetLaneCount(fromNode, toNode));
        if (lanes <= 1 || step == 0) return -1;
        int current = Mathf.Clamp(laneIndex, 0, lanes - 1);
        int target = current + step;
        return target >= 0 && target < lanes && target != current ? target : -1;
    }

    /// <summary>
    /// The normal driving lane: the slow lane nearest the kerb. Faster traffic uses
    /// the lanes nearer the centre line to pass.
    /// </summary>
    public static int CruiseLane(AIRoadGraph graph, int fromNode, int toNode)
    {
        if (graph == null || !graph.IsValidNode(fromNode) || !graph.IsValidNode(toNode)) return 0;
        int lanes = Mathf.Max(1, graph.GetLaneCount(fromNode, toNode));
        if (lanes <= 1) return 0;
        return KerbwardStep(graph, fromNode, toNode) > 0 ? lanes - 1 : 0;
    }

    /// <summary>
    /// The lane a pass moves into. It is always one step toward the centre line, never
    /// toward the kerb: pulling outboard to pass would be undertaking, and on a
    /// two-way link the outboard side is the stopped traffic beside the kerb.
    /// </summary>
    public static int PassingLane(AIRoadGraph graph, int fromNode, int toNode, int laneIndex)
    {
        int centreward = CentrewardLane(graph, fromNode, toNode, laneIndex);
        if (centreward >= 0) return centreward;
        // A one-way carriageway is all one direction, so the lane on its far side is
        // still a legal pass. A two-way link has no such lane: outboard of the kerb
        // lane is the pavement.
        if (graph != null && graph.IsOneWay(fromNode, toNode))
            return KerbwardLane(graph, fromNode, toNode, laneIndex);
        return -1;
    }

    /// <summary>The lane a kerbside yield pulls into, one step toward the kerb.</summary>
    public static int KerbYieldLane(AIRoadGraph graph, int fromNode, int toNode, int laneIndex)
    {
        return KerbwardLane(graph, fromNode, toNode, laneIndex);
    }

    /// <summary>The lane on this directed link whose line is nearest a lateral offset.</summary>
    public static int NearestLane(AIRoadGraph graph, int fromNode, int toNode, float lateralOffset)
    {
        if (graph == null || !graph.IsValidNode(fromNode) || !graph.IsValidNode(toNode)) return 0;
        int lanes = Mathf.Max(1, graph.GetLaneCount(fromNode, toNode));
        int best = 0;
        float bestDistance = float.MaxValue;
        for (int lane = 0; lane < lanes; lane++)
        {
            float distance = Mathf.Abs(lateralOffset - Offset(graph, fromNode, toNode, lane));
            if (distance >= bestDistance) continue;
            bestDistance = distance;
            best = lane;
        }
        return best;
    }

    /// <summary>
    /// Signed lateral offset of one lane line from the OSM centreline, in metres.
    /// This is the only implementation of the lane-offset geometry in the project.
    /// </summary>
    public static float Offset(AIRoadGraph graph, int fromNode, int toNode, int laneIndex)
    {
        if (graph == null || !graph.IsValidNode(fromNode) || !graph.IsValidNode(toNode)) return 0f;

        int lanes = Mathf.Max(1, graph.GetLaneCount(fromNode, toNode));
        float width = graph.GetLaneWidth(fromNode, toNode);
        laneIndex = Mathf.Clamp(laneIndex, 0, lanes - 1);

        // A single-lane road used in both directions has no half to belong to: both
        // directions keep to the middle of the one lane they share.
        if (graph.IsSharedLane(fromNode, toNode)) return 0f;

        // A one-way carriageway spans both sides of the OSM line, so its lanes are
        // centred on it. A two-way direction owns one half only, so every lane sits
        // on the driving side of the centre line.
        if (graph.IsOneWay(fromNode, toNode))
            return (laneIndex - (lanes - 1) * .5f) * width;

        return SideSign(graph.DrivesOnLeft) * (laneIndex + .5f) * width;
    }

    /// <summary>Signed lateral distance from one lane line to another on the same link.</summary>
    public static float OffsetDelta(AIRoadGraph graph, int fromNode, int toNode, int fromLane,
        int toLane)
    {
        return Offset(graph, fromNode, toNode, toLane) - Offset(graph, fromNode, toNode, fromLane);
    }

    /// <summary>
    /// Resolves a directed link's lane into world space. <paramref name="extraOffset"/>
    /// carries a transient pass, yield or recovery shift on top of the committed lane.
    /// </summary>
    public static LaneFrame Frame(AIRoadGraph graph, int fromNode, int toNode, int laneIndex,
        float extraOffset = 0f)
    {
        if (graph == null || !graph.IsValidNode(fromNode) || !graph.IsValidNode(toNode))
            return default;

        Vector3 a = graph.GetNodePosition(fromNode);
        Vector3 b = graph.GetNodePosition(toNode);
        Vector3 segment = b - a;
        segment.y = 0f;
        float length = segment.magnitude;
        if (length < .01f) return default;

        Vector3 direction = segment / length;
        float offset = Offset(graph, fromNode, toNode, laneIndex) + extraOffset;
        Vector3 lateral = Vector3.Cross(Vector3.up, direction) * offset;
        return new LaneFrame(a + lateral, b + lateral, direction, length, offset,
            graph.GetLaneWidth(fromNode, toNode));
    }

    /// <summary>
    /// True when the body is on the correct side of the centre line for its heading.
    /// Placement and the headless audit both call this, so a flipped side cannot be
    /// hidden by the probe agreeing with the placement it is meant to check.
    /// </summary>
    public static bool IsOnKerbSide(Vector3 lateralFromCentreline, Vector3 travel, bool drivesOnLeft)
    {
        Vector3 kerb = KerbDirection(travel, drivesOnLeft);
        if (kerb.sqrMagnitude < .5f) return false;
        return Vector3.Dot(lateralFromCentreline, kerb) >= -0.5f;
    }

    /// <summary>
    /// Lateral distance from the centreline to the kerb a stopped bus docks against,
    /// using the rendered road half-width and reserving the body's own half-width.
    /// The stop prop, the docking trigger and the waiting queue all use it, so the
    /// shelter, the door and the queue stand in the same place on the same kerb.
    /// </summary>
    public static float KerbOffsetFromCentreline(AIRoadGraph graph, int fromNode, int toNode,
        float bodyWidth)
    {
        if (graph == null) return 0f;
        float half = graph.GetRoadHalfWidth(fromNode, toNode);
        if (half <= .1f) return 0f;
        float clearance = Mathf.Max(0f, bodyWidth) * .5f + .35f;
        return Mathf.Max(0f, half - clearance);
    }
}
