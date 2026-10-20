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
    public float vehicleLength = 4.2f;

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

    [Header("State")]
    public float currentSpeedKmh;
    public bool isYieldingToEmergency;
    public bool isOvertaking;
    public bool obeyTrafficSignals = true;
    public bool obeySpeedLimits = true;

    Rigidbody rb;
    float laneShift;
    Transform passingBus;
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
        targetNodeIndex = graph != null ? graph.GetRandomNextNode(startNode) : -1;
        PlaceOnLane(startNode, targetNodeIndex, lane);
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
        float offset = roadGraph.GetLaneOffset(currentNodeIndex, targetNodeIndex, laneIndex) + laneShift;
        Vector3 lateral = Vector3.Cross(Vector3.up, segment) * offset;
        Vector3 laneA = nodeA + lateral;
        Vector3 laneB = nodeB + lateral;
        Vector3 lane = laneB - laneA;
        float laneLength = lane.magnitude;
        Vector3 laneDir = laneLength > .01f ? lane / laneLength : segment;
        float along = laneLength > .01f
            ? Mathf.Clamp01(Vector3.Dot(current - laneA, laneDir) / laneLength)
            : 1f;
        float speedMs = currentSpeedKmh / 3.6f;
        float lookahead = Mathf.Clamp(speedMs * 1.1f, 4f, 12f) + vehicleLength * .75f;
        Vector3 aim = laneA + laneDir * Mathf.Min(laneLength, along * laneLength + lookahead);

        // Pure-pursuit correction: shift the aim point across the lane by the current
        // lateral error, so a body nudged off its line (a collision, or a lane count
        // that changes at a junction) steers back instead of running parallel to it.
        Vector3 right = Vector3.Cross(Vector3.up, segment);
        aim -= right * Vector3.Dot(current - laneA, right) * LaneCenteringGain;

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
        if (!isOvertaking) laneShift = 0f;
    }

    void UpdateStuckRecovery()
    {
        float moved = Vector3.Distance(transform.position, lastProgressPosition);
        lastProgressPosition = transform.position;
        recoveryLaneTimer = Mathf.Max(0f, recoveryLaneTimer - Time.fixedDeltaTime);
        if (recoveryLaneTimer <= 0f && !isOvertaking && !isYieldingToEmergency) laneShift = 0f;
        bool legitimateStop = isYieldingToEmergency || roadGraph.IsRedSignal(targetNodeIndex) || TrafficParticipant.HasVehicleAhead(transform, safeFollowingDistance + 2f);
        if (moved > 0.04f || currentSpeedKmh > 2f || legitimateStop) { stuckSeconds = 0f; return; }
        stuckSeconds += Time.fixedDeltaTime;
        float recoveryShift = OvertakeShift(roadGraph, currentNodeIndex, targetNodeIndex, laneIndex, overtakingLaneShift);
        if (stuckSeconds >= 12f && recoveryLaneTimer <= 0f && Mathf.Abs(recoveryShift) > 0.05f)
        {
            if (TrafficParticipant.PassingLaneClear(transform, recoveryShift, 25f))
            {
                laneShift = recoveryShift;
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
        switch (vehicleType)
        {
            case VehicleType.Truck: return speedLimit * 0.8f;
            case VehicleType.Motorcycle: return speedLimit;
            case VehicleType.Bus: return speedLimit * 0.75f;
            case VehicleType.Emergency: return speedLimit * 1.25f;
            default: return speedLimit;
        }
    }

    float ApplyTrafficAndFollowingRules(float desiredSpeedKmh)
    {
        float result = desiredSpeedKmh;

        if (obeyTrafficSignals && vehicleType != VehicleType.Emergency)
        {
            if (roadGraph.IsRedSignal(targetNodeIndex))
            {
                float distToSignal = Vector3.Distance(transform.position, roadGraph.GetNodePosition(targetNodeIndex));
                result = Mathf.Min(result, RealismRules.StoppingSpeedKmh(distToSignal, vehicleLength * 0.5f + 3f, maxBrake * 0.65f));
            }
        }

        // Overtaking uses the adjacent lane of the same carriageway, chosen
        // from the graph's lane offsets. On a two-way link a pass moves inward
        // (the offside), never outboard along the kerb where it would be an
        // undertake; on a single-lane link the shift is zero and traffic queues
        // behind the stopped bus.
        float passSide = OvertakeShift(roadGraph, currentNodeIndex, targetNodeIndex, laneIndex, overtakingLaneShift);
        if (!isOvertaking && Mathf.Abs(passSide) > 0.05f &&
            !roadGraph.IsRedSignal(targetNodeIndex))
        {
            var stoppedBus = TrafficParticipant.StoppedBusAhead(transform, safeFollowingDistance * 2.5f);
            if (stoppedBus != null && TrafficParticipant.PassingLaneClear(transform, passSide))
            { passingBus = stoppedBus; isOvertaking = true; laneShift = passSide; }
        }
        if (isOvertaking)
        {
            result = Mathf.Min(result, 12f);
            if (passingBus == null || Vector3.Dot(transform.forward, passingBus.position - transform.position) < -12f)
            { isOvertaking = false; passingBus = null; laneShift = 0f; }
        }

        if (isYieldingToEmergency)
        {
            result = Mathf.Min(result, 8f);
            // Pull toward the kerb, but never past the outermost lane. On a
            // single-lane street the shift is zero and the car simply slows.
            laneShift = KerbShift(roadGraph, currentNodeIndex, targetNodeIndex, laneIndex, overtakingLaneShift);
            emergencyYieldTimer -= Time.fixedDeltaTime;
            if (emergencyYieldTimer <= 0f)
            {
                isYieldingToEmergency = false;
                laneShift = 0f;
            }
        }

        return TrafficParticipant.LimitSpeed(transform, Mathf.Max(0f, result), vehicleLength * 0.5f + 3f, maxBrake * 0.65f);
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
        if (isOvertaking && passingBus != null)
        {
            // Push toward the target lane in the traffic body's own frame. The
            // old target was always the passing bus's right side, which is the
            // wrong direction when the selected lane is the other side.
            Vector3 side = transform.right;
            float offset = Vector3.Dot(side, transform.position - passingBus.position);
            velocity += side * Mathf.Clamp((laneShift - offset) * 1.5f, -1.5f, 1.5f);
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
