using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class AIVehicleController : MonoBehaviour
{
    public enum VehicleType { Car, Truck, Motorcycle, Bus, Emergency }

    static readonly List<AIVehicleController> ActiveVehicles = new List<AIVehicleController>();

    public static int ActiveCount => ActiveVehicles.Count;

    [Header("Identity")]
    public VehicleType vehicleType = VehicleType.Car;

    [Header("Graph")]
    public AIRoadGraph roadGraph;
    public int currentNodeIndex = -1;
    public int targetNodeIndex = -1;
    public int previousNodeIndex = -1;
    public int laneIndex = 0;
    /// <summary>Preferred cruising lane on multi-lane roads (2 lanes, 4 lanes, etc.).</summary>
    public int preferredLaneIndex = -1;
    int plannedNextNodeIndex = -1;
    float junctionTurnGraceTimer;

    [Header("Motion")]
    public float maxAccel = 6f;
    public float maxBrake = 9f;
    public float turnRate = 4f;
    public float stopDistance = 4f;
    public float safeFollowingDistance = 9f;
    public float overtakingLaneShift = 3.0f;
    public float laneChangeMetersPerSecond = 3.0f;
    public float overtakeSpeedMarginKmh = 12f;
    public float minimumOvertakeSpeedKmh = 24f;
    public float overtakeScanDistanceMeters = 38f;
    public float maximumOvertakeSeconds = 9f;
    public float overtakeCooldownSeconds = 4f;
    /// <summary>A committed lane change that is not making lateral progress (held by traffic or a kerb) is abandoned after roughly this many seconds.</summary>
    public float laneChangeSettleSeconds = 2f;
    /// <summary>Hold-off before a lane change that could not complete is attempted again.</summary>
    public float laneChangeRetrySeconds = 3f;
    public float vehicleLength = 4.2f;
    /// <summary>Measured prefab width, written by VehiclePool. Following and passing use it alongside TrafficParticipant.width.</summary>
    public float vehicleWidth = 2f;
    /// <summary>Per-driver cruise variation, written by VehiclePool. Range is roughly 0.92–1.06.</summary>
    public float desiredSpeedFactor = 1f;

    /// <summary>
    /// Lateral pure-pursuit gain. The old 2.5 pushed the aim point 3.5x the
    /// current error across the lane, which made a nudged car correct hard,
    /// overshoot and weave. The geometric pursuit already converges; a small
    /// nudge is only needed when a collision or a lane-count change leaves the
    /// body wide of its line.
    /// </summary>
    public const float LaneCenteringGain = 0.9f;

    /// <summary>
    /// Caps speed when the steering target sits behind or far to the side of the
    /// body. A hard turn is crept through instead of being taken at cruise, where
    /// the body would keep its speed and slide sideways across the road. Both
    /// simulation tiers call this, so a change to the rule cannot diverge between
    /// the physics cars and the spline cars again.
    /// </summary>
    public static float HardTurnSpeedLimitKmh(float desiredKmh, float alignment)
    {
        if (alignment >= .35f) return desiredKmh;
        return Mathf.Min(desiredKmh, Mathf.Lerp(0f, 14f, Mathf.Clamp01((alignment + 1f) / 1.35f)));
    }

    /// <summary>
    /// Lateral offset from the current lane to a passing lane on the same
    /// carriageway. On a two-way link lane 0 is the inner lane next to the
    /// centre line on both driving sides, so a pass moves inward towards lane
    /// 0 and never outboard along the kerb (which would be undertaking). On a
    /// one-way link the pass uses the offside of the driving convention, with
    /// the other side as a fallback when that lane does not exist.
    /// </summary>
    public static float OvertakeShift(AIRoadGraph graph, int fromNode, int toNode, int laneIndex, float maxShift)
    {
        int target = PassingLaneIndex(graph, fromNode, toNode, laneIndex);
        if (target < 0) return 0f;
        return Mathf.Clamp(LaneOffsetDelta(graph, fromNode, toNode, laneIndex, target),
            -Mathf.Abs(maxShift), Mathf.Abs(maxShift));
    }

    /// <summary>
    /// Lateral offset toward the near-side kerb, clamped to the outermost
    /// lane. On a two-way link the kerb is outboard (a higher lane index on
    /// both driving sides); on a one-way link it follows the driving side.
    /// A yield on the kerb lane, or on a single-lane street, is zero.
    /// </summary>
    public static float KerbShift(AIRoadGraph graph, int fromNode, int toNode, int laneIndex, float maxShift)
    {
        int target = KerbLaneIndex(graph, fromNode, toNode, laneIndex);
        if (target < 0) return 0f;
        return Mathf.Clamp(LaneOffsetDelta(graph, fromNode, toNode, laneIndex, target),
            -Mathf.Abs(maxShift), Mathf.Abs(maxShift));
    }

    /// <summary>
    /// Persisted lane a pass should move into. This is the lane-index form of
    /// <see cref="OvertakeShift"/>: the car actually changes lane and stays
    /// there until the pass finishes, instead of borrowing a temporary offset
    /// and snapping back to the lane it started in.
    /// </summary>
    public static int PassingLaneIndex(AIRoadGraph graph, int fromNode, int toNode, int laneIndex)
    {
        return TrafficLane.PassingLane(graph, fromNode, toNode, laneIndex);
    }

    /// <summary>Lane to pull into for a kerbside yield, as a persistent lane index.</summary>
    public static int KerbLaneIndex(AIRoadGraph graph, int fromNode, int toNode, int laneIndex)
    {
        return TrafficLane.KerbYieldLane(graph, fromNode, toNode, laneIndex);
    }

    /// <summary>
    /// The normal driving lane: the kerbside/slow lane on a two-way road, and
    /// the near side of a one-way carriageway. Faster traffic uses the lanes
    /// nearer the centre line to pass. Lane indices are graph link indices, not
    /// screen directions, so this follows the graph's driving side.
    /// </summary>
    public static int CruiseLaneIndex(AIRoadGraph graph, int fromNode, int toNode)
    {
        return TrafficLane.CruiseLane(graph, fromNode, toNode);
    }

    /// <summary>Signed lateral distance from one lane line to another on the same directed link.</summary>
    public static float LaneOffsetDelta(AIRoadGraph graph, int fromNode, int toNode, int fromLane, int toLane)
    {
        return TrafficLane.OffsetDelta(graph, fromNode, toNode, fromLane, toLane);
    }

    public static bool CanOvertakeOn(AIRoadGraph graph, int fromNode, int toNode, int laneIndex = 0,
        float maxShift = 3f)
    {
        return Mathf.Abs(OvertakeShift(graph, fromNode, toNode, laneIndex, maxShift)) > 0.05f;
    }

    /// <summary>
    /// Pure overtake decision shared by the physics and spline tiers. A lane must
    /// exist, the leader must actually be slower, and the gap must be close enough
    /// that this is a pass rather than a speculative drift across the road.
    /// </summary>
    public static bool ShouldStartOvertake(float desiredKmh, float leaderKmh, float gapMeters,
        float followingDistance, bool laneAvailable, float metersToJunction = float.MaxValue)
    {
        if (!laneAvailable) return false;
        if (leaderKmh >= Mathf.Max(0f, desiredKmh) - 6f) return false;
        float trigger = Mathf.Max(12f, Mathf.Max(0f, followingDistance) * 2.8f);
        if (gapMeters <= .05f || gapMeters > trigger) return false;
        // The pass has to fit before the next junction. A lane change that has to be
        // abandoned inside the junction leaves the car straddling two lanes exactly
        // where the crossing traffic is, so a late pass is not worth starting.
        return RealismRules.HasOvertakeRunway(metersToJunction, desiredKmh, 2.6f, 26f);
    }

    /// <summary>
    /// Speed held while alongside a slower vehicle. It is deliberately not the old
    /// 12 km/h crawl: a pass has to be quick enough to clear the target lane before
    /// the next junction. The speed limit remains the hard cap.
    /// </summary>
    public static float OvertakeSpeedKmh(float desiredKmh, float leaderKmh,
        float speedMarginKmh = 12f, float minimumKmh = 24f)
    {
        float desired = Mathf.Max(0f, desiredKmh);
        float pass = Mathf.Max(Mathf.Max(0f, minimumKmh), Mathf.Max(0f, leaderKmh) + Mathf.Max(0f, speedMarginKmh));
        return Mathf.Min(desired, pass);
    }

    /// <summary>
    /// Smooth but decisive lane movement. The old system assigned the lateral
    /// offset in a single frame; this keeps the body planted while still crossing
    /// a 4.3 m widened lane in under two seconds.
    /// </summary>
    public static float StepLaneChange(float current, float target, float metersPerSecond, float deltaTime)
    {
        return Mathf.MoveTowards(current, target,
            Mathf.Max(.1f, metersPerSecond) * Mathf.Max(0f, deltaTime));
    }

    [Header("State")]
    public float currentSpeedKmh;
    public bool isYieldingToEmergency;
    public bool isOvertaking;
    /// <summary>Return to the slow/nearside lane after a pass when it is safe. The pass itself is a real lane change, not a temporary sideways offset.</summary>
    public bool returnToCruiseLane = true;
    /// <summary>Grace period after spawning or a tier change before ordinary lane discipline starts.</summary>
    public float laneDisciplineDelaySeconds = 1.5f;
    /// <summary>True while the body is deliberately between lanes (pass, return, kerb yield or stuck recovery).</summary>
    public bool IsChangingLane => isOvertaking || isYieldingToEmergency ||
                                  recoveryLaneTimer > 0f || Mathf.Abs(laneShift) > .05f ||
                                  laneChangeActive;
    public float LateralLaneError { get; private set; }
    public bool obeyTrafficSignals = true;
    public bool obeySpeedLimits = true;

    Rigidbody rb;
    float laneShift;
    float laneShiftTarget;
    // Set when a lane index is committed, cleared only once the body has reached
    // the new lane (or the change is abandoned). Keeps the settled-lane check from
    // reading the tail of a lane change as a lane-keeping fault.
    bool laneChangeActive;
    float laneChangeErrorBaseline = float.MaxValue;
    float laneChangeStallTimer;
    TrafficParticipant passTarget;
    int overtakeLaneIndex = -1;
    bool returningToCruiseLane;
    float laneDisciplineTimer;
    float overtakeTimer;
    float overtakeCooldown;
    float emergencyYieldTimer;
    float stuckSeconds;
    float recoveryLaneTimer;
    Vector3 lastProgressPosition;

    void OnEnable()
    {
        if (!ActiveVehicles.Contains(this))
            ActiveVehicles.Add(this);
    }

    void OnDisable()
    {
        ActiveVehicles.Remove(this);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        // Traffic is steered, not thrown: keep bodies upright so a kerb strike or a
        // player touch cannot tip a car onto its side and leave it juddering.
        rb.constraints |= RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        lastProgressPosition = transform.position;
    }

    public void Init(AIRoadGraph graph, int startNode, int lane, VehicleType type)
    {
        roadGraph = graph;
        currentNodeIndex = startNode;
        previousNodeIndex = -1;
        laneIndex = lane;
        preferredLaneIndex = lane;
        vehicleType = type;
        ResetLaneChange();
        targetNodeIndex = graph != null ? graph.GetRandomNextNode(startNode) : -1;
        PlaceOnLane(startNode, targetNodeIndex, lane);
    }

    /// <summary>Drops a transient pass/yield offset when a body is re-homed or promoted.</summary>
    public void ResetLaneChange()
    {
        laneShift = 0f;
        laneShiftTarget = 0f;
        isOvertaking = false;
        returningToCruiseLane = false;
        passTarget = null;
        overtakeLaneIndex = -1;
        overtakeTimer = 0f;
        overtakeCooldown = 0f;
        emergencyYieldTimer = 0f;
        recoveryLaneTimer = 0f;
        laneDisciplineTimer = Mathf.Max(0f, laneDisciplineDelaySeconds);
        plannedNextNodeIndex = -1;
        junctionTurnGraceTimer = 0f;
        laneChangeActive = false;
        laneChangeErrorBaseline = float.MaxValue;
        laneChangeStallTimer = 0f;
    }

    /// <summary>Flags an unresolved lane change from the body's current pose.</summary>
    void RefreshLaneChangeActive()
    {
        laneChangeErrorBaseline = float.MaxValue;
        laneChangeStallTimer = 0f;
        laneChangeActive = false;
        if (roadGraph == null || !roadGraph.IsValidNode(currentNodeIndex) ||
            !roadGraph.IsValidNode(targetNodeIndex))
            return;
        Vector3 segment = roadGraph.GetNodePosition(targetNodeIndex) -
                          roadGraph.GetNodePosition(currentNodeIndex);
        segment.y = 0f;
        if (segment.sqrMagnitude <= .0001f) return;
        Vector3 right = Vector3.Cross(Vector3.up, segment.normalized);
        float bodyOffset = Vector3.Dot(transform.position -
            roadGraph.GetNodePosition(currentNodeIndex), right);
        float committed = roadGraph.GetLaneOffset(currentNodeIndex, targetNodeIndex, laneIndex);
        float tolerance = Mathf.Max(1f,
            roadGraph.GetLaneWidth(currentNodeIndex, targetNodeIndex) * .35f);
        laneChangeActive = Mathf.Abs(bodyOffset - committed) > tolerance;
    }

    /// <summary>
    /// Supervises a committed lane change. The offset easing finishes on a timer, so
    /// a body held by traffic or a kerb can be left a whole lane short of the lane
    /// its index names. While that is unresolved the car still counts as changing
    /// lanes; if it stops making lateral progress the change is abandoned, which
    /// re-syncs the index to the lane the body occupies, and it is retried later.
    /// </summary>
    void UpdateLaneChangeSettlement(float lateralError, Vector3 planarFromNodeA, Vector3 right)
    {
        if (!laneChangeActive)
        {
            laneChangeErrorBaseline = float.MaxValue;
            laneChangeStallTimer = 0f;
            return;
        }
        if (roadGraph == null || !roadGraph.IsValidNode(currentNodeIndex) ||
            !roadGraph.IsValidNode(targetNodeIndex))
            return;

        float laneWidth = roadGraph.GetLaneWidth(currentNodeIndex, targetNodeIndex);
        float tolerance = Mathf.Max(1f, laneWidth * .35f);
        // lateralError is measured against GetLaneOffset(laneIndex) + laneShift, so
        // adding the residual back measures the body against the committed lane.
        float committedError = Mathf.Abs(lateralError + laneShift);

        if (committedError <= tolerance)
        {
            // The body reached the lane it committed to.
            laneChangeActive = false;
            laneChangeErrorBaseline = float.MaxValue;
            laneChangeStallTimer = 0f;
            return;
        }

        // A crawling body cannot make lateral progress; wait until it is moving
        // before judging it, or a red light would abandon every lane change.
        if (currentSpeedKmh < 6f)
        {
            laneChangeErrorBaseline = committedError;
            laneChangeStallTimer = 0f;
            return;
        }

        if (laneChangeErrorBaseline == float.MaxValue ||
            committedError < laneChangeErrorBaseline - .3f)
        {
            laneChangeErrorBaseline = committedError;
            laneChangeStallTimer = 0f;
            return;
        }

        laneChangeStallTimer += Time.fixedDeltaTime;
        if (laneChangeStallTimer < Mathf.Max(.5f, laneChangeSettleSeconds))
            return;

        AbortLaneChange(planarFromNodeA, right);
    }

    /// <summary>Gives up on the committed lane and re-syncs the index to the lane the body occupies.</summary>
    void AbortLaneChange(Vector3 planarFromNodeA, Vector3 right)
    {
        float bodyOffset = Vector3.Dot(planarFromNodeA, right);
        laneIndex = NearestLaneIndex(roadGraph, currentNodeIndex, targetNodeIndex, bodyOffset);
        laneShift = 0f;
        laneShiftTarget = 0f;
        isOvertaking = false;
        returningToCruiseLane = false;
        passTarget = null;
        overtakeLaneIndex = -1;
        laneChangeActive = false;
        laneChangeErrorBaseline = float.MaxValue;
        laneChangeStallTimer = 0f;
        laneDisciplineTimer = Mathf.Max(laneDisciplineTimer, laneChangeRetrySeconds);
        overtakeCooldown = Mathf.Max(overtakeCooldown, laneChangeRetrySeconds);
    }

    /// <summary>The lane on this directed link whose line is nearest a lateral body offset.</summary>
    public static int NearestLaneIndex(AIRoadGraph graph, int fromNode, int toNode, float bodyOffset)
    {
        return TrafficLane.NearestLane(graph, fromNode, toNode, bodyOffset);
    }

    /// <summary>
    /// Places the body on its own lane line of a directed link and faces it along the
    /// link. Spawning on the bare node put every vehicle on the centre line, so the
    /// first thing a driver saw was a car cutting across the road to reach its lane.
    /// </summary>
    public void PlaceOnLane(int nodeIndex, int targetIndex, int lane, int previousIndex = -1, float along01 = 0f)
    {
        if (roadGraph == null || !roadGraph.IsValidNode(nodeIndex)) return;
        currentNodeIndex = nodeIndex;
        targetNodeIndex = targetIndex;
        previousNodeIndex = previousIndex;
        laneIndex = lane;
        Vector3 point = roadGraph.GetNodePosition(nodeIndex);
        Quaternion facing = transform.rotation;
        // Placing on the shared lane frame puts the body exactly on the line its
        // controller will steer down, instead of on the centreline offset by a
        // separately computed lateral vector.
        LaneFrame frame = TrafficLane.Frame(roadGraph, nodeIndex, targetIndex, lane);
        if (frame.IsValid)
        {
            point = frame.PointAt(Mathf.Clamp(along01, 0.02f, 0.98f));
            facing = Quaternion.LookRotation(frame.Direction, Vector3.up);
        }
        else if (roadGraph.IsValidNode(targetIndex))
        {
            Vector3 segment = roadGraph.GetNodePosition(targetIndex) - point;
            segment.y = 0f;
            if (segment.sqrMagnitude > .0001f)
                facing = Quaternion.LookRotation(segment.normalized, Vector3.up);
        }
        transform.SetPositionAndRotation(point, facing);
        if (rb != null)
        {
            rb.position = point;
            rb.rotation = facing;
            if (!rb.isKinematic)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    public void SetRuntimeState(AIRoadGraph graph, int currentNode, int targetNode, int lane, VehicleType type, float speedKmh, int previousNode = -1)
    {
        roadGraph = graph;
        currentNodeIndex = currentNode;
        targetNodeIndex = targetNode;
        previousNodeIndex = previousNode;
        laneIndex = lane;
        preferredLaneIndex = lane;
        vehicleType = type;
        currentSpeedKmh = Mathf.Max(0f, speedKmh);
        ResetLaneChange();

        // A spline can advance its target on the frame it is promoted, leaving
        // current == target. Without a fresh target the fixed step returns every
        // frame and the body coasts straight with no steering, which reads as a
        // vehicle that suddenly jumps out of its lane.
        if (roadGraph != null && (!roadGraph.IsValidNode(targetNodeIndex) || targetNodeIndex == currentNodeIndex))
            targetNodeIndex = roadGraph.GetRandomNextNode(currentNodeIndex, previousNodeIndex);

        if (rb != null)
        {
            if (!rb.isKinematic) rb.linearVelocity = transform.forward * (currentSpeedKmh / 3.6f);
            rb.angularVelocity = Vector3.zero;
        }

        // A tier hand-off can pass on a lane index the body has not reached (the
        // outgoing tier was mid-change). Keep it flagged so the crossing is not read
        // as a car driving a lane off its own line.
        RefreshLaneChangeActive();
    }

    public Vector3 GetVelocityWorld()
    {
        return transform.forward * (currentSpeedKmh / 3.6f);
    }

    public void SetVelocityWorld(Vector3 velocity)
    {
        if (rb != null)
            rb.linearVelocity = velocity;
    }

    float surfaceRailTimer;

    /// <summary>
    /// Puts a body that has wandered off the drawn road back on the nearest
    /// carriageway, at a cadence rather than per frame. Returns the position the rest
    /// of the step must work from, so steering and lane error use the corrected spot.
    /// </summary>
    Vector3 RailToDrawnAsphalt(Vector3 position, float bodyHalfWidth)
    {
        surfaceRailTimer -= Time.fixedDeltaTime;
        if (surfaceRailTimer > 0f) return position;
        surfaceRailTimer = TrafficLane.SurfaceRailIntervalSeconds;

        Vector3 corrected = TrafficLane.ClampToSurface(TrafficLane.RuntimeSurface, position, bodyHalfWidth);
        if ((corrected - position).sqrMagnitude <= .0001f) return position;

        transform.position = corrected;
        if (rb != null) rb.position = corrected;
        return corrected;
    }

    void FixedUpdate()
    {
        if (roadGraph == null || !roadGraph.IsValidNode(currentNodeIndex))
            return;

        if (!roadGraph.IsValidNode(targetNodeIndex) || targetNodeIndex == currentNodeIndex)
            targetNodeIndex = roadGraph.GetRandomNextNode(currentNodeIndex, previousNodeIndex);
        if (!roadGraph.IsValidNode(targetNodeIndex))
            return;

        // A turn can enter a narrower road. Keep the chosen lane valid for
        // this directed edge instead of carrying a two-lane index into a
        // single-lane street.
        laneIndex = Mathf.Clamp(laneIndex, 0, Mathf.Max(1, roadGraph.GetLaneCount(currentNodeIndex, targetNodeIndex)) - 1);

        // Decide the pass/yield offset before the steering line is built, then
        // move toward it at a finite rate. The old code assigned a lane offset in
        // one frame, which made a car snap sideways instead of changing lanes.
        UpdateDrivingIntent();
        laneShift = StepLaneChange(laneShift, laneShiftTarget,
            laneChangeMetersPerSecond, Time.fixedDeltaTime);

        Vector3 current = transform.position;
        Vector3 nodeA = roadGraph.GetNodePosition(currentNodeIndex);
        Vector3 nodeB = roadGraph.GetNodePosition(targetNodeIndex);
        Vector3 segment = nodeB - nodeA;
        segment.y = 0f;
        if (segment.sqrMagnitude < .0001f)
        {
            AdvanceToNextNode();
            return;
        }
        segment.Normalize();

        // Both ends of the lane line use this link's own lane normal. Steering at a
        // point on that line, a lookahead ahead of the vehicle, is what keeps it inside
        // its lane through a junction: aiming straight at the next node cut the corner
        // across the opposite carriageway and the pavement.
        Vector3 right = Vector3.Cross(Vector3.up, segment);

        // Hard road-edge rail. Pure pursuit is allowed to be a little wide at a
        // junction, but the body must never end up with its wheels over the kerb.
        // RoadWidthPolicy reserves the model width in the rendered ribbon, so this
        // clamp normally has clearance to spare and only catches collisions,
        // sharp turns and stale lane offsets.
        junctionTurnGraceTimer = Mathf.Max(0f, junctionTurnGraceTimer - Time.fixedDeltaTime);
        float bodyHalfWidth = Mathf.Max(.6f, vehicleWidth * .5f);
        // The road-edge rail is never switched off. A turn used to open a two second
        // hole in it, and a corner is exactly where a body cuts across the kerb. The
        // clamp is measured to the nearest point of this link's asphalt - and of the
        // link the car is turning into once it is in the junction mouth - so the car
        // still swings through the corner, just never onto the pavement.
        Vector3 railed = TrafficLane.ClampToRoad(roadGraph, currentNodeIndex, targetNodeIndex,
            plannedNextNodeIndex, current, bodyHalfWidth);
        if ((railed - current).sqrMagnitude > .0001f)
        {
            current = railed;
            transform.position = current;
            if (rb != null) rb.position = current;
        }

        // The graph is a model of the road; the drawn asphalt is the road. Where the
        // two disagree - a node welded across a carriageway, a link whose centreline
        // runs down a median - the body is put back on the carriageway the player can
        // actually see instead of continuing across the verge.
        current = RailToDrawnAsphalt(current, bodyHalfWidth);

        // The lane line comes from the shared frame, which is the same geometry the
        // spline tier, the pool's spawn placement and the headless audit read. The
        // old local cross product could not disagree with itself, but it could - and
        // did - disagree with the other three.
        LaneFrame frame = TrafficLane.Frame(roadGraph, currentNodeIndex, targetNodeIndex,
            laneIndex, laneShift);
        Vector3 laneA = frame.IsValid ? frame.Start : nodeA;
        Vector3 laneB = frame.IsValid ? frame.End : nodeB;
        float laneLength = frame.IsValid ? frame.Length : 0f;
        Vector3 laneDir = frame.IsValid ? frame.Direction : segment;
        float along = frame.IsValid ? frame.AlongOf(current) : 1f;

        float lateralError = Vector3.Dot(current - laneA, right);
        LateralLaneError = Mathf.Abs(lateralError);

        // A collision or a stale carriageway side can leave a body several
        // metres off its own lane. Pure pursuit corrects that slowly and, at
        // 13 m, reads as a car driving on the pavement. Snap only the extreme
        // case and never while a deliberate lane change is in progress; the
        // normal compliance correction below handles everything smaller.
        float laneWidth = roadGraph.GetLaneWidth(currentNodeIndex, targetNodeIndex);
        if (!IsChangingLane && junctionTurnGraceTimer <= 0f && LateralLaneError > Mathf.Max(3.2f, laneWidth * 1.35f))
        {
            Vector3 corrected = current - right * lateralError;
            corrected.y = current.y;
            transform.position = corrected;
            if (rb != null) rb.position = corrected;
            current = corrected;
            lateralError = 0f;
            LateralLaneError = 0f;
        }

        UpdateLaneChangeSettlement(lateralError, current - nodeA, right);

        float speedMs = currentSpeedKmh / 3.6f;
        float lookahead = Mathf.Clamp(speedMs * 1.1f, 4f, 12f) + vehicleLength * .75f;
        Vector3 aim = laneA + laneDir * Mathf.Min(laneLength, along * laneLength + lookahead);

        // Pure-pursuit correction: shift the aim point across the lane by the current
        // lateral error, so a body nudged off its line (a collision, or a lane count
        // that changes at a junction) steers back instead of running parallel to it.
        aim -= right * lateralError * LaneCenteringGain;

        Vector3 travelDir = aim - current;
        travelDir.y = 0f;
        if (travelDir.sqrMagnitude > .0001f) travelDir.Normalize();
        else travelDir = segment;

        float desiredSpeed = ResolveDesiredSpeedKmh();
        float safeSpeed = ApplyTrafficAndFollowingRules(desiredSpeed);
        MoveVehicle(travelDir, safeSpeed);
        UpdateStuckRecovery();

        // The link ends at its lane point, not at the bare node: a vehicle offset to its
        // own lane never gets within a couple of metres of the node itself.
        if (Vector3.Distance(current, laneB) <= Mathf.Max(2.5f, stopDistance))
            AdvanceToNextNode();
    }

    /// <summary>
    /// Chooses whether this frame's line should sit in its own lane, a passing lane
    /// or the kerb. It only sets <see cref="laneShiftTarget"/>; FixedUpdate eases
    /// the body toward it, so a pass has a visible lane-change phase rather than a
    /// one-frame teleport.
    /// </summary>
    void UpdateDrivingIntent()
    {
        if (roadGraph == null || !roadGraph.IsValidNode(currentNodeIndex) ||
            !roadGraph.IsValidNode(targetNodeIndex))
            return;

        float dt = Time.fixedDeltaTime;
        overtakeCooldown = Mathf.Max(0f, overtakeCooldown - dt);
        laneDisciplineTimer = Mathf.Max(0f, laneDisciplineTimer - dt);
        int laneCount = Mathf.Max(1, roadGraph.GetLaneCount(currentNodeIndex, targetNodeIndex));
        int clampedLane = Mathf.Clamp(laneIndex, 0, laneCount - 1);
        if (clampedLane != laneIndex)
        {
            // The road narrowed under the car, so the body has to merge inward. That
            // is a real lane change and needs the same settle supervision.
            laneIndex = clampedLane;
            laneChangeActive = true;
            laneChangeErrorBaseline = float.MaxValue;
            laneChangeStallTimer = 0f;
        }

        // Use the graph's actual lane spacing as the shift limit. Vehicles can be
        // wider than the old 3 m assumption, and a pass that only moves 3 m in a
        // 4.3 m lane leaves the body straddling both lanes.
        float shiftLimit = Mathf.Max(overtakingLaneShift,
            roadGraph.GetLaneWidth(currentNodeIndex, targetNodeIndex));
        float distanceToJunction = Vector3.Distance(transform.position,
            roadGraph.GetNodePosition(targetNodeIndex));

        if (isYieldingToEmergency)
        {
            laneShiftTarget = KerbShift(roadGraph, currentNodeIndex, targetNodeIndex,
                laneIndex, shiftLimit);
            return;
        }

        if (isOvertaking)
        {
            overtakeTimer += dt;
            bool targetGone = passTarget == null || !passTarget.isActiveAndEnabled;
            bool passed = !targetGone &&
                Vector3.Dot(transform.forward, passTarget.transform.position - transform.position) <
                -Mathf.Max(2f, vehicleLength * .7f);
            bool tooCloseToJunction = distanceToJunction < Mathf.Max(7f, vehicleLength * 1.2f);
            bool laneGone = overtakeLaneIndex < 0 || overtakeLaneIndex >= laneCount;
            // A leader that has picked its speed back up leaves nothing to pass.
            bool targetOutpaced = !targetGone &&
                passTarget.SpeedKmh >= ResolveDesiredSpeedKmh() - 4f;
            if (targetGone || passed || tooCloseToJunction || targetOutpaced ||
                roadGraph.IsRedSignal(currentNodeIndex, targetNodeIndex) || laneGone ||
                overtakeTimer >= Mathf.Max(2f, maximumOvertakeSeconds))
            {
                EndOvertake();
                return;
            }

            // The lane index was committed when the pass began. Keep it until the
            // pass is over; the smooth lane-change offset is already easing to the
            // new lane line.
            laneShiftTarget = 0f;
            return;
        }

        // Stuck recovery already owns the lane offset until its timer expires.
        if (recoveryLaneTimer > 0f) return;

        if (returningToCruiseLane && TryReturnToCruiseLane(distanceToJunction))
            return;

        // Do not stack a second lane change on top of one still in progress.
        if (Mathf.Abs(laneShift) > .05f)
        {
            laneShiftTarget = 0f;
            return;
        }

        // A queue waiting at a red light stays in the lane it arrived in. Changing
        // lane inside the stop zone is what leaves cars straddling two lanes - and
        // the kerb - while they wait, and it never helps anyone leave sooner.
        if (roadGraph.IsRedSignal(currentNodeIndex, targetNodeIndex) &&
            distanceToJunction < Mathf.Max(18f, vehicleLength * 2f + currentSpeedKmh / 3.6f))
        {
            laneShiftTarget = 0f;
            return;
        }

        // Approach intersection turn preparation on multi-lane roads
        if (roadGraph != null && distanceToJunction < 55f)
        {
            if (plannedNextNodeIndex < 0 || !roadGraph.IsValidNode(plannedNextNodeIndex))
                plannedNextNodeIndex = roadGraph.GetRandomNextNode(targetNodeIndex, currentNodeIndex);

            if (plannedNextNodeIndex >= 0 && laneDisciplineTimer <= 0f && !isOvertaking && Mathf.Abs(laneShift) < 0.05f)
            {
                int turnLane = TrafficLane.ResolveTurnLane(roadGraph, currentNodeIndex, targetNodeIndex, plannedNextNodeIndex);
                if (turnLane >= 0 && turnLane < laneCount && turnLane != laneIndex)
                {
                    int step = turnLane > laneIndex ? 1 : -1;
                    int nextLane = laneIndex + step;
                    float side = LaneOffsetDelta(roadGraph, currentNodeIndex, targetNodeIndex, laneIndex, nextLane);
                    float clearLength = Mathf.Max(16f, currentSpeedKmh / 3.6f + 6f);
                    if (Mathf.Abs(side) > 0.05f && TrafficParticipant.PassingLaneClear(transform, side, clearLength))
                    {
                        BeginLaneChange(nextLane);
                        return;
                    }
                }
            }
        }

        if (overtakeCooldown <= 0f && !roadGraph.IsRedSignal(currentNodeIndex, targetNodeIndex) &&
            RealismRules.HasOvertakeRunway(distanceToJunction, currentSpeedKmh))
        {
            int passLane = PassingLaneIndex(roadGraph, currentNodeIndex, targetNodeIndex, laneIndex);
            if (passLane < 0 && (roadGraph.IsOneWay(currentNodeIndex, targetNodeIndex) || laneCount >= 3))
                passLane = KerbLaneIndex(roadGraph, currentNodeIndex, targetNodeIndex, laneIndex);

            if (passLane >= 0 && passLane < laneCount && passLane != laneIndex)
            {
                float desired = ResolveDesiredSpeedKmh();
                float scan = Mathf.Max(18f, overtakeScanDistanceMeters + currentSpeedKmh / 3.6f * .8f);
                float halfLane = Mathf.Max(1.5f, vehicleWidth * .5f + .65f);
                if (TrafficParticipant.TryFindAhead(transform, scan, halfLane,
                        out TrafficParticipant leader, out float gap) &&
                    ShouldStartOvertake(desired, leader.SpeedKmh, gap, safeFollowingDistance, true,
                        distanceToJunction))
                {
                    float passSide = LaneOffsetDelta(roadGraph, currentNodeIndex, targetNodeIndex,
                        laneIndex, passLane);
                    float clearLength = Mathf.Max(26f, currentSpeedKmh / 3.6f * 1.4f + 12f);
                    if (Mathf.Abs(passSide) > .05f &&
                        TrafficParticipant.PassingLaneClear(transform, passSide, clearLength))
                    {
                        StartOvertake(leader, passLane);
                        return;
                    }
                }
            }
        }

        // Multi-lane cruising and lane balance: cars cruise in their preferred lane
        // across 2-lane, 3-lane and 4-lane roads rather than all cramping into one lane.
        int targetPreferred = GetEffectivePreferredLane(laneCount);
        if (laneDisciplineTimer <= 0f && laneCount > 1 &&
            distanceToJunction > Mathf.Max(12f, vehicleLength * 1.8f))
        {
            if (targetPreferred != laneIndex)
            {
                int step = targetPreferred > laneIndex ? 1 : -1;
                int stepLane = laneIndex + step;
                float side = LaneOffsetDelta(roadGraph, currentNodeIndex, targetNodeIndex,
                    laneIndex, stepLane);
                float clearLength = Mathf.Max(18f, currentSpeedKmh / 3.6f + 8f);
                if (Mathf.Abs(side) > .05f &&
                    TrafficParticipant.PassingLaneClear(transform, side, clearLength))
                {
                    BeginLaneChange(stepLane);
                    return;
                }
            }
        }

        laneShiftTarget = 0f;
    }

    int GetEffectivePreferredLane(int laneCount)
    {
        if (preferredLaneIndex >= 0 && preferredLaneIndex < laneCount)
            return preferredLaneIndex;
        preferredLaneIndex = TrafficLane.PickLaneForVehicle(roadGraph, currentNodeIndex, targetNodeIndex,
            vehicleType, desiredSpeedFactor, transform.GetHashCode());
        return preferredLaneIndex;
    }

    void StartOvertake(TrafficParticipant leader, int passLane)
    {
        isOvertaking = true;
        returningToCruiseLane = false;
        passTarget = leader;
        overtakeLaneIndex = passLane;
        overtakeTimer = 0f;
        BeginLaneChange(passLane);
        laneShiftTarget = 0f;
    }

    void EndOvertake()
    {
        isOvertaking = false;
        passTarget = null;
        overtakeLaneIndex = -1;
        laneShiftTarget = 0f;
        overtakeCooldown = Mathf.Max(0f, overtakeCooldownSeconds);
        returningToCruiseLane = roadGraph != null &&
            roadGraph.IsValidNode(currentNodeIndex) && roadGraph.IsValidNode(targetNodeIndex);
    }

    /// <summary>
    /// Commits to a new lane while preserving the rendered pose. The current
    /// lane line becomes newLaneOffset + laneShift, so the body starts exactly
    /// where it was and eases across over the normal lane-change time.
    /// </summary>
    void BeginLaneChange(int targetLane)
    {
        if (roadGraph == null || !roadGraph.IsValidNode(currentNodeIndex) ||
            !roadGraph.IsValidNode(targetNodeIndex))
            return;
        int lanes = Mathf.Max(1, roadGraph.GetLaneCount(currentNodeIndex, targetNodeIndex));
        if (targetLane < 0 || targetLane >= lanes || targetLane == laneIndex)
            return;

        // Ease across from the old lane line to the new one, using the shared
        // offset geometry for both instead of a second local computation.
        laneShift -= TrafficLane.OffsetDelta(roadGraph, currentNodeIndex, targetNodeIndex,
            laneIndex, targetLane);
        laneIndex = targetLane;
        laneShiftTarget = 0f;
        laneChangeActive = true;
        laneChangeErrorBaseline = float.MaxValue;
        laneChangeStallTimer = 0f;
    }

    bool TryReturnToCruiseLane(float distanceToJunction)
    {
        if (roadGraph == null || !roadGraph.IsValidNode(currentNodeIndex) ||
            !roadGraph.IsValidNode(targetNodeIndex))
        {
            returningToCruiseLane = false;
            return false;
        }

        int lanes = Mathf.Max(1, roadGraph.GetLaneCount(currentNodeIndex, targetNodeIndex));
        int targetLane = (returnToCruiseLane || vehicleType == VehicleType.Bus || vehicleType == VehicleType.Truck)
            ? CruiseLaneIndex(roadGraph, currentNodeIndex, targetNodeIndex)
            : GetEffectivePreferredLane(lanes);
        if (targetLane == laneIndex || lanes <= 1)
        {
            returningToCruiseLane = false;
            return false;
        }

        // Let the passing lane change settle first; two offsets at once read as a
        // car sliding diagonally through the middle of the road.
        if (Mathf.Abs(laneShift) > .35f || distanceToJunction < Mathf.Max(8f, vehicleLength))
            return true;

        float side = LaneOffsetDelta(roadGraph, currentNodeIndex, targetNodeIndex,
            laneIndex, targetLane);
        float clearLength = Mathf.Max(18f, currentSpeedKmh / 3.6f + 8f);
        if (Mathf.Abs(side) > .05f &&
            TrafficParticipant.PassingLaneClear(transform, side, clearLength))
        {
            BeginLaneChange(targetLane);
            returningToCruiseLane = false;
            laneShiftTarget = 0f;
        }
        return true;
    }

    /// <summary>
    /// Leaves the current link and picks a continuation that does not double back,
    /// so ordinary two-way chain nodes do not turn every car around.
    /// Maps lanes across intersections (2 lanes to 4 lanes, turns, etc.) and smooths the turn.
    /// </summary>
    void AdvanceToNextNode()
    {
        int prev = currentNodeIndex;
        int curr = targetNodeIndex;
        int next = (plannedNextNodeIndex >= 0 && roadGraph != null && roadGraph.IsValidNode(plannedNextNodeIndex))
            ? plannedNextNodeIndex
            : (roadGraph != null ? roadGraph.GetRandomNextNode(curr, prev) : -1);

        int oldLane = laneIndex;
        previousNodeIndex = prev;
        currentNodeIndex = curr;
        targetNodeIndex = next;
        plannedNextNodeIndex = -1;

        if (roadGraph != null && roadGraph.IsValidNode(curr) && roadGraph.IsValidNode(next))
        {
            laneIndex = TrafficLane.ResolveExitLane(roadGraph, prev, curr, next, oldLane);
            preferredLaneIndex = GetEffectivePreferredLane(roadGraph.GetLaneCount(curr, next));
        }

        junctionTurnGraceTimer = 2.0f;
        if (!isOvertaking && !isYieldingToEmergency && recoveryLaneTimer <= 0f)
            laneShiftTarget = 0f;
    }

    void UpdateStuckRecovery()
    {
        float moved = Vector3.Distance(transform.position, lastProgressPosition);
        lastProgressPosition = transform.position;
        recoveryLaneTimer = Mathf.Max(0f, recoveryLaneTimer - Time.fixedDeltaTime);
        if (recoveryLaneTimer <= 0f && !isOvertaking && !isYieldingToEmergency) laneShiftTarget = 0f;
        bool legitimateStop = isYieldingToEmergency || roadGraph.IsRedSignal(currentNodeIndex, targetNodeIndex) || TrafficParticipant.HasVehicleAhead(transform, safeFollowingDistance + 2f);
        if (moved > 0.04f || currentSpeedKmh > 2f || legitimateStop) { stuckSeconds = 0f; return; }
        stuckSeconds += Time.fixedDeltaTime;
        float shiftLimit = Mathf.Max(overtakingLaneShift,
            roadGraph.GetLaneWidth(currentNodeIndex, targetNodeIndex));
        float recoveryShift = OvertakeShift(roadGraph, currentNodeIndex, targetNodeIndex, laneIndex, shiftLimit);
        if (stuckSeconds >= 12f && recoveryLaneTimer <= 0f && Mathf.Abs(recoveryShift) > 0.05f)
        {
            if (TrafficParticipant.PassingLaneClear(transform, recoveryShift, 25f))
            {
                laneShiftTarget = recoveryShift;
                recoveryLaneTimer = 5f;
                stuckSeconds = 0f;
                return;
            }
        }
        if (stuckSeconds >= 20f)
        {
            int next = roadGraph.GetRandomNextNode(currentNodeIndex, previousNodeIndex);
            if (roadGraph.IsValidNode(next)) targetNodeIndex = next;
            stuckSeconds = 0f;
        }
    }

    float ResolveDesiredSpeedKmh()
    {
        float speedLimit = obeySpeedLimits ? roadGraph.GetSpeedLimitKmh(currentNodeIndex, targetNodeIndex) : 45f;
        float typeFactor;
        switch (vehicleType)
        {
            case VehicleType.Truck: typeFactor = 0.8f; break;
            case VehicleType.Motorcycle: typeFactor = 1f; break;
            case VehicleType.Bus: typeFactor = 0.75f; break;
            case VehicleType.Emergency: typeFactor = 1.25f; break;
            default: typeFactor = 1f; break;
        }

        // Section 3.4: grade-adjusted AI speed. Vehicles slow on uphill grades and
        // may pick up a little on downhills, bounded so they never exceed the limit.
        float grade = RoadGradeLookup.GetGrade(transform.position);
        float gradeFactor = 1f + Mathf.Clamp(grade, -0.15f, 0.05f);
        if (vehicleType == VehicleType.Truck || vehicleType == VehicleType.Bus)
            gradeFactor = 1f + Mathf.Clamp(grade, -0.25f, 0.02f); // heavy vehicles lose more uphill

        return speedLimit * typeFactor * Mathf.Clamp(desiredSpeedFactor, 0.8f, 1.1f) * gradeFactor;
    }

    float ApplyTrafficAndFollowingRules(float desiredSpeedKmh)
    {
        float result = desiredSpeedKmh;

        // Ease off for the turn that is actually likely at the next junction.
        // ApproachSpeedKmh restores full speed when the junction is still far
        // away, so ordinary straight running is unaffected.
        if (roadGraph.IsValidNode(currentNodeIndex) && roadGraph.IsValidNode(targetNodeIndex))
        {
            Vector3 junction = roadGraph.GetNodePosition(targetNodeIndex);
            Vector3 incoming = junction - roadGraph.GetNodePosition(currentNodeIndex);
            incoming.y = 0f;
            if (incoming.sqrMagnitude > .0001f)
            {
                incoming.Normalize();
                // Slow for the exit this car has actually planned: the shallowest
                // exit at the junction can be a side street it is not going to take,
                // which let a right-angle turn be taken at cruise speed.
                float angle = roadGraph.IsValidNode(plannedNextNodeIndex)
                    ? roadGraph.GetTurnAngle(currentNodeIndex, targetNodeIndex, plannedNextNodeIndex)
                    : roadGraph.GetLikelyTurnAngle(targetNodeIndex, incoming, previousNodeIndex);
                float factor = RealismRules.TurnSpeedFactor(angle);
                if (factor < .999f)
                {
                    float distance = Vector3.Distance(transform.position, junction);
                    float approach = Mathf.Clamp(currentSpeedKmh / 3.6f * 1.8f + vehicleLength, 10f, 45f);
                    result = Mathf.Min(result, RealismRules.ApproachSpeedKmh(
                        desiredSpeedKmh, desiredSpeedKmh * factor, distance, approach));
                }
            }
        }

        if (obeyTrafficSignals && vehicleType != VehicleType.Emergency)
        {
            if (roadGraph.IsRedSignal(currentNodeIndex, targetNodeIndex))
            {
                float distToSignal = Vector3.Distance(transform.position, roadGraph.GetNodePosition(targetNodeIndex));
                result = Mathf.Min(result, RealismRules.StoppingSpeedKmh(distToSignal, vehicleLength * 0.5f + 3f, maxBrake * 0.65f));
            }
        }

        // Never enter a junction whose far side has no room for this body. Stopping
        // across the junction blocks the cross street, and a queue that spills round
        // the corner is what reads as traffic parked on the pavement.
        result = Mathf.Min(result, JunctionEntrySpeedKmh(result));

        // A pass keeps its speed up rather than crawling at 12 km/h. The lane is
        // clear when the pass starts, and the target vehicle is ignored by the
        // following rule so the pass can actually complete instead of being
        // braked by the car alongside it. Everything else remains a hard limit.
        if (isOvertaking)
        {
            float leaderSpeed = passTarget != null ? passTarget.SpeedKmh : 0f;
            result = Mathf.Min(result, OvertakeSpeedKmh(result, leaderSpeed,
                overtakeSpeedMarginKmh, minimumOvertakeSpeedKmh));
        }

        if (isYieldingToEmergency)
        {
            result = Mathf.Min(result, 8f);
            emergencyYieldTimer -= Time.fixedDeltaTime;
            if (emergencyYieldTimer <= 0f)
            {
                isYieldingToEmergency = false;
                laneShiftTarget = 0f;
            }
        }

        bool clearOfTarget = passTarget == null ||
            TrafficParticipant.IsLateralClear(transform, passTarget.transform);
        Transform ignore = isOvertaking && passTarget != null && clearOfTarget
            ? passTarget.transform : null;
        return TrafficParticipant.LimitSpeed(transform, Mathf.Max(0f, result),
            vehicleLength * 0.5f + 3f, maxBrake * 0.65f, ignore);
    }

    /// <summary>
    /// Speed cap for entering the junction ahead. Zero while the far side of it is
    /// occupied, so the car waits at the stop line instead of parking across the
    /// crossing; full speed again once the exit has room. Only applies when the exit
    /// link is already planned, so it can never brake a car for a junction it is
    /// not about to cross.
    /// </summary>
    float JunctionEntrySpeedKmh(float desiredKmh)
    {
        if (!roadGraph.IsValidNode(currentNodeIndex) || !roadGraph.IsValidNode(targetNodeIndex) ||
            !roadGraph.IsValidNode(plannedNextNodeIndex))
            return desiredKmh;

        float clearance = vehicleLength * .5f + 2f;
        float distance = Vector3.Distance(transform.position, roadGraph.GetNodePosition(targetNodeIndex));
        if (distance > clearance + safeFollowingDistance + 6f) return desiredKmh;

        Vector3 exitPoint = JunctionExitPoint(plannedNextNodeIndex);
        if (exitPoint == Vector3.zero) return desiredKmh;

        float radius = vehicleLength * .5f + 3.5f;
        if (TrafficParticipant.IsSpawnAreaClear(exitPoint, radius, transform)) return desiredKmh;
        return Mathf.Min(desiredKmh, RealismRules.StoppingSpeedKmh(distance, clearance, maxBrake * 0.65f));
    }

    /// <summary>A point just inside the link this car is about to enter.</summary>
    Vector3 JunctionExitPoint(int nextNode)
    {
        int exitLane = Mathf.Clamp(laneIndex, 0, Mathf.Max(1, roadGraph.GetLaneCount(targetNodeIndex, nextNode)) - 1);
        LaneFrame exit = TrafficLane.Frame(roadGraph, targetNodeIndex, nextNode, exitLane);
        if (!exit.IsValid) return Vector3.zero;
        return exit.Start + exit.Direction * Mathf.Min(8f, exit.Length * .5f);
    }

    void MoveVehicle(Vector3 forwardDir, float targetSpeedKmh)
    {
        // A target behind or far to the side of the body means a hard turn. Creep
        // through it instead of keeping full speed and sliding sideways.
        targetSpeedKmh = HardTurnSpeedLimitKmh(targetSpeedKmh,
            Vector3.Dot(transform.forward, forwardDir));

        float targetMs = targetSpeedKmh / 3.6f;
        float currentMs = currentSpeedKmh / 3.6f;
        float accel = targetMs >= currentMs ? maxAccel : maxBrake;
        float newMs = Mathf.MoveTowards(currentMs, targetMs, accel * Time.fixedDeltaTime);
        // Physics owns position; the lane target is an absolute offset, never a per-frame drift.
        Vector3 velocity = forwardDir * newMs;
        // While a committed lane change is still easing, give the body a small
        // lateral component towards the new line. Pure pursuit alone converges,
        // but this keeps a widened 4.3 m lane change decisive instead of making
        // the car look like it is drifting across the road.
        if (Mathf.Abs(laneShift) > .02f && forwardDir.sqrMagnitude > .0001f)
        {
            Vector3 side = Vector3.Cross(Vector3.up, forwardDir).normalized;
            velocity += side * Mathf.Clamp(-laneShift * .9f, -1.6f, 1.6f);
        }
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;
        rb.angularVelocity = Vector3.zero;

        if (forwardDir.sqrMagnitude > 0.001f)
        {
            Quaternion look = Quaternion.LookRotation(forwardDir, Vector3.up);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, look, turnRate * Time.fixedDeltaTime));
        }

        currentSpeedKmh = newMs * 3.6f;
    }

    AIVehicleController FindFrontVehicle()
    {
        Vector3 fwd = transform.forward;
        AIVehicleController best = null;
        float bestDist = float.MaxValue;
        for (int i = 0; i < ActiveVehicles.Count; i++)
        {
            var other = ActiveVehicles[i];
            if (other == null || other == this) continue;
            Vector3 to = other.transform.position - transform.position;
            float forward = Vector3.Dot(fwd, to.normalized);
            if (forward < 0.65f) continue;
            float dist = to.magnitude;
            if (dist < bestDist)
            {
                bestDist = dist;
                best = other;
            }
        }
        return best;
    }

    public void PullOverForEmergency(float duration = 4f)
    {
        isYieldingToEmergency = true;
        emergencyYieldTimer = Mathf.Max(emergencyYieldTimer, duration);
    }

    public static void NotifyEmergencyVehicle(Vector3 source, float radius, float yieldDuration)
    {
        SplineVehicle.NotifyEmergency(source, radius, yieldDuration);
        float sqr = radius * radius;
        for (int i = 0; i < ActiveVehicles.Count; i++)
        {
            var v = ActiveVehicles[i];
            if (v == null || v.vehicleType == VehicleType.Emergency) continue;
            if ((v.transform.position - source).sqrMagnitude <= sqr)
                v.PullOverForEmergency(yieldDuration);
        }
    }
}
