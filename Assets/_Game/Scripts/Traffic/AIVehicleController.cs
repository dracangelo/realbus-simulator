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
    /// Lateral offset from the current lane to a passing lane on the same
    /// carriageway. On a two-way link lane 0 is the inner lane next to the
    /// centre line on both driving sides, so a pass moves inward towards lane
    /// 0 and never outboard along the kerb (which would be undertaking). On a
    /// one-way link the pass uses the offside of the driving convention, with
    /// the other side as a fallback when that lane does not exist.
    /// </summary>
    public static float OvertakeShift(AIRoadGraph graph, int fromNode, int toNode, int laneIndex, float maxShift)
    {
        if (graph == null) return 0f;
        int lanes = graph.GetLaneCount(fromNode, toNode);
        if (lanes <= 1) return 0f;
        int current = Mathf.Clamp(laneIndex, 0, lanes - 1);
        int target;
        if (graph.IsOneWay(fromNode, toNode))
        {
            target = graph.DrivesOnLeft ? current + 1 : current - 1;
            if (target < 0 || target >= lanes)
                target = graph.DrivesOnLeft ? current - 1 : current + 1;
        }
        else
        {
            // Moving outboard on a two-way road means pulling into the kerb
            // lane beside the vehicle being passed. Wait behind instead when
            // already in the inner lane.
            target = current - 1;
        }
        if (target < 0 || target >= lanes) return 0f;

        float shift = graph.GetLaneOffset(fromNode, toNode, target) -
                      graph.GetLaneOffset(fromNode, toNode, current);
        float limit = Mathf.Abs(maxShift);
        return Mathf.Clamp(shift, -limit, limit);
    }

    /// <summary>
    /// Lateral offset toward the near-side kerb, clamped to the outermost
    /// lane. On a two-way link the kerb is outboard (a higher lane index on
    /// both driving sides); on a one-way link it follows the driving side.
    /// A yield on the kerb lane, or on a single-lane street, is zero.
    /// </summary>
    public static float KerbShift(AIRoadGraph graph, int fromNode, int toNode, int laneIndex, float maxShift)
    {
        if (graph == null) return 0f;
        int lanes = graph.GetLaneCount(fromNode, toNode);
        if (lanes <= 1) return 0f;
        int current = Mathf.Clamp(laneIndex, 0, lanes - 1);
        int target = graph.IsOneWay(fromNode, toNode)
            ? (graph.DrivesOnLeft ? current - 1 : current + 1)
            : current + 1;
        if (target < 0 || target >= lanes) return 0f;

        float shift = graph.GetLaneOffset(fromNode, toNode, target) -
                      graph.GetLaneOffset(fromNode, toNode, current);
        float limit = Mathf.Abs(maxShift);
        return Mathf.Clamp(shift, -limit, limit);
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
        float followingDistance, bool laneAvailable)
    {
        if (!laneAvailable) return false;
        if (leaderKmh >= Mathf.Max(0f, desiredKmh) - 6f) return false;
        float trigger = Mathf.Max(12f, Mathf.Max(0f, followingDistance) * 2.8f);
        return gapMeters > .05f && gapMeters <= trigger;
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
    /// <summary>True while the body is deliberately between lanes (pass, kerb yield or stuck recovery).</summary>
    public bool IsChangingLane => isOvertaking || isYieldingToEmergency || recoveryLaneTimer > 0f ||
                                  Mathf.Abs(laneShift) > .05f;
    public float LateralLaneError { get; private set; }
    public bool obeyTrafficSignals = true;
    public bool obeySpeedLimits = true;

    Rigidbody rb;
    float laneShift;
    float laneShiftTarget;
    TrafficParticipant passTarget;
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
        passTarget = null;
        overtakeTimer = 0f;
        overtakeCooldown = 0f;
        emergencyYieldTimer = 0f;
        recoveryLaneTimer = 0f;
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
        Vector3 start = roadGraph.GetNodePosition(nodeIndex);
        Vector3 point = start;
        Quaternion facing = transform.rotation;
        if (roadGraph.IsValidNode(targetIndex))
        {
            Vector3 target = roadGraph.GetNodePosition(targetIndex);
            Vector3 segment = target - start;
            segment.y = 0f;
            point = Vector3.Lerp(start, target, Mathf.Clamp(along01, 0.02f, 0.98f));
            if (segment.sqrMagnitude > .0001f)
            {
                segment.Normalize();
                point += Vector3.Cross(Vector3.up, segment) * roadGraph.GetLaneOffset(nodeIndex, targetIndex, lane);
                facing = Quaternion.LookRotation(segment, Vector3.up);
            }
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
        float bodyHalfWidth = Mathf.Max(.6f, vehicleWidth * .5f);
        float roadHalfWidth = roadGraph.GetRoadHalfWidth(currentNodeIndex, targetNodeIndex);
        if (roadHalfWidth > .1f)
        {
            float maxOffset = Mathf.Max(0f, roadHalfWidth - bodyHalfWidth - .12f);
            float centerOffset = Vector3.Dot(current - nodeA, right);
            if (Mathf.Abs(centerOffset) > maxOffset)
            {
                Vector3 corrected = current - right *
                    (centerOffset - Mathf.Sign(centerOffset) * maxOffset);
                corrected.y = current.y;
                transform.position = corrected;
                if (rb != null) rb.position = corrected;
                current = corrected;
            }
        }

        float offset = roadGraph.GetLaneOffset(currentNodeIndex, targetNodeIndex, laneIndex) + laneShift;
        Vector3 lateral = right * offset;
        Vector3 laneA = nodeA + lateral;
        Vector3 laneB = nodeB + lateral;
        Vector3 lane = laneB - laneA;
        float laneLength = lane.magnitude;
        Vector3 laneDir = laneLength > .01f ? lane / laneLength : segment;
        float along = laneLength > .01f
            ? Mathf.Clamp01(Vector3.Dot(current - laneA, laneDir) / laneLength)
            : 1f;

        float lateralError = Vector3.Dot(current - laneA, right);
        LateralLaneError = Mathf.Abs(lateralError);

        // A collision or a stale carriageway side can leave a body several
        // metres off its own lane. Pure pursuit corrects that slowly and, at
        // 13 m, reads as a car driving on the pavement. Snap only the extreme
        // case and never while a deliberate lane change is in progress; the
        // normal compliance correction below handles everything smaller.
        float laneWidth = roadGraph.GetLaneWidth(currentNodeIndex, targetNodeIndex);
        if (!IsChangingLane && LateralLaneError > Mathf.Max(3.2f, laneWidth * 1.35f))
        {
            Vector3 corrected = current - right * lateralError;
            corrected.y = current.y;
            transform.position = corrected;
            if (rb != null) rb.position = corrected;
            current = corrected;
            lateralError = 0f;
            LateralLaneError = 0f;
        }

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
        float dt = Time.fixedDeltaTime;
        overtakeCooldown = Mathf.Max(0f, overtakeCooldown - dt);
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

        float passSide = OvertakeShift(roadGraph, currentNodeIndex, targetNodeIndex,
            laneIndex, shiftLimit);

        if (isOvertaking)
        {
            overtakeTimer += dt;
            bool targetGone = passTarget == null || !passTarget.isActiveAndEnabled;
            bool passed = !targetGone &&
                Vector3.Dot(transform.forward, passTarget.transform.position - transform.position) <
                -Mathf.Max(2f, vehicleLength * .7f);
            bool tooCloseToJunction = distanceToJunction < Mathf.Max(7f, vehicleLength * 1.2f);
            if (targetGone || passed || tooCloseToJunction ||
                roadGraph.IsRedSignal(targetNodeIndex) ||
                Mathf.Abs(passSide) < .05f ||
                overtakeTimer >= Mathf.Max(2f, maximumOvertakeSeconds))
            {
                EndOvertake();
                return;
            }

            laneShiftTarget = passSide;
            return;
        }

        // Stuck recovery already owns the lane offset until its timer expires.
        if (recoveryLaneTimer > 0f) return;

        if (overtakeCooldown <= 0f && Mathf.Abs(passSide) > .05f &&
            !roadGraph.IsRedSignal(targetNodeIndex) &&
            RealismRules.HasOvertakeRunway(distanceToJunction, currentSpeedKmh))
        {
            float desired = ResolveDesiredSpeedKmh();
            float scan = Mathf.Max(18f, overtakeScanDistanceMeters + currentSpeedKmh / 3.6f * .8f);
            float halfLane = Mathf.Max(1.5f, vehicleWidth * .5f + .65f);
            if (TrafficParticipant.TryFindAhead(transform, scan, halfLane,
                    out TrafficParticipant leader, out float gap) &&
                ShouldStartOvertake(desired, leader.SpeedKmh, gap, safeFollowingDistance, true))
            {
                float clearLength = Mathf.Max(26f, currentSpeedKmh / 3.6f * 1.4f + 12f);
                if (TrafficParticipant.PassingLaneClear(transform, passSide, clearLength))
                {
                    isOvertaking = true;
                    passTarget = leader;
                    overtakeTimer = 0f;
                    laneShiftTarget = passSide;
                    return;
                }
            }
        }

        laneShiftTarget = 0f;
    }

    void EndOvertake()
    {
        isOvertaking = false;
        passTarget = null;
        laneShiftTarget = 0f;
        overtakeCooldown = Mathf.Max(0f, overtakeCooldownSeconds);
    }

    /// <summary>
    /// Leaves the current link and picks a continuation that does not double back,
    /// so ordinary two-way chain nodes do not turn every car around.
    /// </summary>
    void AdvanceToNextNode()
    {
        previousNodeIndex = currentNodeIndex;
        currentNodeIndex = targetNodeIndex;
        targetNodeIndex = roadGraph != null
            ? roadGraph.GetRandomNextNode(currentNodeIndex, previousNodeIndex)
            : -1;
        if (!isOvertaking && !isYieldingToEmergency && recoveryLaneTimer <= 0f)
            laneShiftTarget = 0f;
    }

    void UpdateStuckRecovery()
    {
        float moved = Vector3.Distance(transform.position, lastProgressPosition);
        lastProgressPosition = transform.position;
        recoveryLaneTimer = Mathf.Max(0f, recoveryLaneTimer - Time.fixedDeltaTime);
        if (recoveryLaneTimer <= 0f && !isOvertaking && !isYieldingToEmergency) laneShiftTarget = 0f;
        bool legitimateStop = isYieldingToEmergency || roadGraph.IsRedSignal(targetNodeIndex) || TrafficParticipant.HasVehicleAhead(transform, safeFollowingDistance + 2f);
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
        float speedLimit = obeySpeedLimits ? roadGraph.GetSpeedLimitKmh(currentNodeIndex) : 45f;
        float typeFactor;
        switch (vehicleType)
        {
            case VehicleType.Truck: typeFactor = 0.8f; break;
            case VehicleType.Motorcycle: typeFactor = 1f; break;
            case VehicleType.Bus: typeFactor = 0.75f; break;
            case VehicleType.Emergency: typeFactor = 1.25f; break;
            default: typeFactor = 1f; break;
        }
        return speedLimit * typeFactor * Mathf.Clamp(desiredSpeedFactor, 0.8f, 1.1f);
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
                float angle = roadGraph.GetLikelyTurnAngle(targetNodeIndex, incoming, previousNodeIndex);
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
            if (roadGraph.IsRedSignal(targetNodeIndex))
            {
                float distToSignal = Vector3.Distance(transform.position, roadGraph.GetNodePosition(targetNodeIndex));
                result = Mathf.Min(result, RealismRules.StoppingSpeedKmh(distToSignal, vehicleLength * 0.5f + 3f, maxBrake * 0.65f));
            }
        }

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

    void MoveVehicle(Vector3 forwardDir, float targetSpeedKmh)
    {
        // A target behind or far to the side of the body means a hard turn. Creep
        // through it instead of keeping full speed and sliding sideways.
        float alignment = Vector3.Dot(transform.forward, forwardDir);
        if (alignment < .35f)
            targetSpeedKmh = Mathf.Min(targetSpeedKmh, Mathf.Lerp(0f, 14f, Mathf.Clamp01((alignment + 1f) / 1.35f)));

        float targetMs = targetSpeedKmh / 3.6f;
        float currentMs = currentSpeedKmh / 3.6f;
        float accel = targetMs >= currentMs ? maxAccel : maxBrake;
        float newMs = Mathf.MoveTowards(currentMs, targetMs, accel * Time.fixedDeltaTime);
        // Physics owns position; the lane target is an absolute offset, never a per-frame drift.
        Vector3 velocity = forwardDir * newMs;
        if (isOvertaking && passTarget != null)
        {
            // Push toward the target lane in the traffic body's own frame. The
            // target is still in the original lane, so the signed distance from
            // it is also the lateral progress of the lane change.
            Vector3 side = transform.right;
            float offset = Vector3.Dot(side, transform.position - passTarget.transform.position);
            velocity += side * Mathf.Clamp((laneShift - offset) * 2f, -1.8f, 1.8f);
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
