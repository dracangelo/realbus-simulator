using UnityEngine;
using System.Collections.Generic;

public class SplineVehicle : MonoBehaviour
{
    static readonly List<SplineVehicle> ActiveSplineVehicles = new List<SplineVehicle>();

    [Header("Graph")]
    public AIRoadGraph roadGraph;
    public int currentNodeIndex = -1;
    public int targetNodeIndex = -1;
    public int previousNodeIndex = -1;
    public int laneIndex = 0;

    [Header("Motion")]
    public float speedKmh = 0f;
    public float accelKmhPerSec = 16f;
    public float brakeKmhPerSec = 25f;
    public float turnRate = 7f;
    public float minHeadway = 10f;
    public float redLightStopDistance = 18f;
    public float laneChangeMetersPerSecond = 3.2f;
    public float overtakeSpeedMarginKmh = 10f;
    public float minimumOvertakeSpeedKmh = 22f;
    public float overtakeScanDistanceMeters = 36f;
    public float maximumOvertakeSeconds = 9f;
    public float overtakeCooldownSeconds = 4f;
    /// <summary>Per-driver cruise variation, written by VehiclePool. Matches the full-tier controller.</summary>
    public float desiredSpeedFactor = 1f;
    /// <summary>Measured body width, written by VehiclePool. The spline tier uses it for the road-edge rail.</summary>
    public float vehicleWidth = 2f;

    [Header("Path Cache")]
    public int lookaheadNodes = 8;
    /// <summary>
    /// Seconds between movement and perception steps. VehiclePool raises this for
    /// vehicles far from the bus; zero keeps the original per-frame update. The
    /// step is accumulated rather than skipped, so average speed is unchanged.
    /// </summary>
    [Tooltip("0 = every frame. The pool sets 0.05-0.1 s for distant vehicles.")]
    public float updateIntervalSeconds = 0f;
    float updateAccumulator;
    /// <summary>Return to the slow/nearside lane after a pass when it is safe.</summary>
    public bool returnToCruiseLane = true;
    /// <summary>Grace period after spawning or a tier change before ordinary lane discipline starts.</summary>
    public float laneDisciplineDelaySeconds = 1.5f;
    float emergencyYieldSeconds;
    float stuckSeconds;
    float laneShift;
    float laneShiftTarget;
    float overtakeTimer;
    float overtakeCooldown;
    int overtakeLaneIndex = -1;
    bool returningToCruiseLane;
    float laneDisciplineTimer;
    TrafficParticipant passTarget;
    Vector3 lastProgressPosition;
    public bool IsOvertaking { get; private set; }
    public static void NotifyEmergency(Vector3 source, float radius, float seconds)
    {
        foreach (var vehicle in ActiveSplineVehicles)
            if (vehicle != null && (vehicle.transform.position - source).sqrMagnitude < radius * radius) vehicle.emergencyYieldSeconds = seconds;
    }
    readonly Queue<int> cachedPath = new Queue<int>();

    void OnEnable()
    {
        if (!ActiveSplineVehicles.Contains(this))
            ActiveSplineVehicles.Add(this);
        lastProgressPosition = transform.position;
    }

    void OnDisable()
    {
        ActiveSplineVehicles.Remove(this);
    }

    public void Init(AIRoadGraph graph, int currentNode, int targetNode, int lane, float initialSpeedKmh, int previousNode = -1)
    {
        roadGraph = graph;
        currentNodeIndex = currentNode;
        targetNodeIndex = targetNode;
        previousNodeIndex = previousNode;
        laneIndex = lane;
        speedKmh = Mathf.Max(0f, initialSpeedKmh);
        ResetLaneChange();
        RebuildPath();
    }

    /// <summary>Clears a pass or yield offset when the body is re-homed or tier-changed.</summary>
    public void ResetLaneChange()
    {
        laneShift = 0f;
        laneShiftTarget = 0f;
        IsOvertaking = false;
        returningToCruiseLane = false;
        passTarget = null;
        overtakeLaneIndex = -1;
        overtakeTimer = 0f;
        overtakeCooldown = 0f;
        emergencyYieldSeconds = 0f;
        laneDisciplineTimer = Mathf.Max(0f, laneDisciplineDelaySeconds);
    }

    public void SetVelocityWorld(Vector3 velocity)
    {
        speedKmh = velocity.magnitude * 3.6f;
    }

    public Vector3 GetVelocityWorld()
    {
        return transform.forward * (speedKmh / 3.6f);
    }

    void Update()
    {
        float dt = ConsumeStep(ref updateAccumulator, Time.deltaTime, updateIntervalSeconds);
        if (dt <= 0f)
            return;
        Simulate(dt);
    }

    /// <summary>
    /// Accumulates frame time and returns the step to simulate with, or zero
    /// while a coarse cadence has not elapsed yet. Pure, so the cadence can be
    /// checked without a frame. A zero interval means every frame, which is what
    /// the near tier keeps.
    /// </summary>
    public static float ConsumeStep(ref float accumulated, float deltaTime, float intervalSeconds)
    {
        if (intervalSeconds <= 0.001f)
        {
            accumulated = 0f;
            return deltaTime;
        }

        accumulated += Mathf.Max(0f, deltaTime);
        if (accumulated < intervalSeconds)
            return 0f;

        float step = accumulated;
        accumulated = 0f;
        return step;
    }

    void Simulate(float dt)
    {
        if (roadGraph == null || !roadGraph.IsValidNode(currentNodeIndex))
            return;

        EnsureTargetNode();
        if (!roadGraph.IsValidNode(targetNodeIndex))
            return;

        // Match the full-physics controller: lanes belong to the directed
        // edge, so clamp after a turn onto a road with fewer lanes.
        laneIndex = Mathf.Clamp(laneIndex, 0, Mathf.Max(1, roadGraph.GetLaneCount(currentNodeIndex, targetNodeIndex)) - 1);

        Vector3 nodeA = roadGraph.GetNodePosition(currentNodeIndex);
        Vector3 nodeB = roadGraph.GetNodePosition(targetNodeIndex);
        Vector3 segment = nodeB - nodeA;
        segment.y = 0f;
        if (segment.sqrMagnitude < .0001f) return;
        segment.Normalize();
        emergencyYieldSeconds = Mathf.Max(0f, emergencyYieldSeconds - dt);

        float distanceToJunction = Vector3.Distance(transform.position, nodeB);
        float desired = roadGraph.GetSpeedLimitKmh(currentNodeIndex) *
                        Mathf.Clamp(desiredSpeedFactor, 0.8f, 1.1f);
        float turnFactor = RealismRules.TurnSpeedFactor(
            roadGraph.GetLikelyTurnAngle(targetNodeIndex, segment, previousNodeIndex));
        if (turnFactor < .999f)
        {
            float approach = Mathf.Clamp(speedKmh / 3.6f * 1.8f + 4f, 10f, 45f);
            desired = RealismRules.ApproachSpeedKmh(desired, desired * turnFactor,
                distanceToJunction, approach);
        }
        UpdateLaneIntent(desired, dt);
        laneShift = AIVehicleController.StepLaneChange(laneShift, laneShiftTarget,
            laneChangeMetersPerSecond, dt);

        // The same lane line the full-physics controller drives: a lane point at each
        // end of this link, and a lookahead on it, so a low-detail car also stays in
        // its lane instead of cutting diagonally towards the next junction.
        float offset = roadGraph.GetLaneOffset(currentNodeIndex, targetNodeIndex, laneIndex) + laneShift;
        Vector3 lateral = Vector3.Cross(Vector3.up, segment) * offset;
        Vector3 laneA = nodeA + lateral;
        Vector3 laneB = nodeB + lateral;
        Vector3 lane = laneB - laneA;
        float laneLength = lane.magnitude;
        Vector3 laneDir = laneLength > .01f ? lane / laneLength : segment;
        float along = laneLength > .01f
            ? Mathf.Clamp01(Vector3.Dot(transform.position - laneA, laneDir) / laneLength)
            : 1f;
        float lookahead = Mathf.Clamp(speedKmh / 3.6f * 1.1f, 4f, 12f);
        Vector3 target = laneA + laneDir * Mathf.Min(laneLength, along * laneLength + lookahead);

        // The same lateral correction the full-physics controller uses, so a car
        // nudged off its line comes back rather than running parallel to it.
        Vector3 right = Vector3.Cross(Vector3.up, segment);
        float bodyHalfWidth = Mathf.Max(.6f, vehicleWidth * .5f);
        float roadHalfWidth = roadGraph.GetRoadHalfWidth(currentNodeIndex, targetNodeIndex);
        if (roadHalfWidth > .1f)
        {
            float maxOffset = Mathf.Max(0f, roadHalfWidth - bodyHalfWidth - .12f);
            float centerOffset = Vector3.Dot(transform.position - nodeA, right);
            if (Mathf.Abs(centerOffset) > maxOffset)
                transform.position -= right * (centerOffset - Mathf.Sign(centerOffset) * maxOffset);
        }
        target -= right * Vector3.Dot(transform.position - laneA, right) * AIVehicleController.LaneCenteringGain;

        Vector3 to = laneB - transform.position;
        to.y = 0f;
        float dist = to.magnitude;
        Vector3 dir = target - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < .0001f) dir = segment;
        else dir.Normalize();

        // Mirror the full-physics controller: a steering line behind or far to the
        // side of the body is a hard turn. Without this the spline tier kept its
        // cruise speed and slid sideways across the road while it swung round.
        desired = AIVehicleController.HardTurnSpeedLimitKmh(desired,
            Vector3.Dot(transform.forward, dir));

        if (emergencyYieldSeconds > 0f) desired = Mathf.Min(desired, 8f);
        if (roadGraph.IsRedSignal(targetNodeIndex))
            desired = Mathf.Min(desired, RealismRules.StoppingSpeedKmh(dist, 5f, brakeKmhPerSec / 3.6f * 0.65f));
        if (IsOvertaking)
            desired = Mathf.Min(desired, AIVehicleController.OvertakeSpeedKmh(desired,
                passTarget != null ? passTarget.SpeedKmh : 0f,
                overtakeSpeedMarginKmh, minimumOvertakeSpeedKmh));

        bool clearOfTarget = passTarget == null ||
            TrafficParticipant.IsLateralClear(transform, passTarget.transform);
        Transform ignore = IsOvertaking && passTarget != null && clearOfTarget
            ? passTarget.transform : null;
        float frontLimited = TrafficParticipant.LimitSpeed(transform, desired, 5f,
            brakeKmhPerSec / 3.6f * 0.65f, ignore);
        float rate = frontLimited < speedKmh ? brakeKmhPerSec : accelKmhPerSec;
        speedKmh = Mathf.MoveTowards(speedKmh, frontLimited, rate * dt);

        float speedMs = speedKmh / 3.6f;
        Vector3 nextPos = Vector3.MoveTowards(transform.position, target, speedMs * dt);
        transform.position = nextPos;
        UpdateStuckRecovery(dt);

        if (dir.sqrMagnitude > 0.001f)
        {
            Quaternion look = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, turnRate * dt);
        }

        if (dist <= 3.5f)
        {
            previousNodeIndex = currentNodeIndex;
            currentNodeIndex = targetNodeIndex;
            EnsureTargetNode(forceAdvance: true);
            // A pass belongs to the old edge. Let the offset ease home rather than
            // snapping, but do not carry it into the next junction as an intent.
            if (!IsOvertaking && emergencyYieldSeconds <= 0f) laneShiftTarget = 0f;
        }
    }

    void UpdateStuckRecovery(float dt)
    {
        float moved = Vector3.Distance(transform.position, lastProgressPosition); lastProgressPosition = transform.position;
        bool legitimateStop = emergencyYieldSeconds > 0f || roadGraph.IsRedSignal(targetNodeIndex) || TrafficParticipant.HasVehicleAhead(transform, minHeadway + 2f);
        if (moved > 0.025f || speedKmh > 1.5f || legitimateStop) { stuckSeconds = 0f; return; }
        stuckSeconds += dt;
        if (stuckSeconds >= 18f)
        {
            cachedPath.Clear();
            int next = roadGraph != null ? roadGraph.GetRandomNextNode(currentNodeIndex, previousNodeIndex) : -1;
            if (roadGraph != null && roadGraph.IsValidNode(next)) targetNodeIndex = next;
            stuckSeconds = 0f;
        }
    }

    /// <summary>
    /// Cheap-tier counterpart of the full controller's intent pass. The spline body
    /// still steers to one lane line, but the line may temporarily sit in the
    /// offside lane so a slow or stopped vehicle can be passed without a physics
    /// body forcing it to a crawl.
    /// </summary>
    void UpdateLaneIntent(float desiredKmh, float dt)
    {
        overtakeCooldown = Mathf.Max(0f, overtakeCooldown - dt);
        laneDisciplineTimer = Mathf.Max(0f, laneDisciplineTimer - dt);
        int laneCount = Mathf.Max(1, roadGraph.GetLaneCount(currentNodeIndex, targetNodeIndex));
        laneIndex = Mathf.Clamp(laneIndex, 0, laneCount - 1);
        float shiftLimit = Mathf.Max(3f, roadGraph.GetLaneWidth(currentNodeIndex, targetNodeIndex));
        float distanceToJunction = Vector3.Distance(transform.position,
            roadGraph.GetNodePosition(targetNodeIndex));

        if (emergencyYieldSeconds > 0f)
        {
            laneShiftTarget = AIVehicleController.KerbShift(roadGraph, currentNodeIndex,
                targetNodeIndex, laneIndex, shiftLimit);
            return;
        }

        if (IsOvertaking)
        {
            overtakeTimer += dt;
            bool targetGone = passTarget == null || !passTarget.isActiveAndEnabled;
            bool passed = !targetGone &&
                Vector3.Dot(transform.forward, passTarget.transform.position - transform.position) < -5f;
            bool tooCloseToJunction = distanceToJunction < 7f;
            bool laneGone = overtakeLaneIndex < 0 || overtakeLaneIndex >= laneCount;
            if (targetGone || passed || tooCloseToJunction ||
                roadGraph.IsRedSignal(targetNodeIndex) || laneGone ||
                overtakeTimer >= Mathf.Max(2f, maximumOvertakeSeconds))
            {
                EndOvertake();
                return;
            }

            // The passing lane is a committed lane now. Keep it until the slow
            // vehicle is behind and the return lane is clear.
            laneShiftTarget = 0f;
            return;
        }

        if (returningToCruiseLane && TryReturnToCruiseLane(distanceToJunction))
            return;

        if (Mathf.Abs(laneShift) > .05f)
        {
            laneShiftTarget = 0f;
            return;
        }

        if (overtakeCooldown <= 0f && !roadGraph.IsRedSignal(targetNodeIndex) &&
            RealismRules.HasOvertakeRunway(distanceToJunction, speedKmh))
        {
            int passLane = AIVehicleController.PassingLaneIndex(roadGraph, currentNodeIndex,
                targetNodeIndex, laneIndex);
            if (passLane >= 0)
            {
                float scan = Mathf.Max(18f, overtakeScanDistanceMeters + speedKmh / 3.6f * .8f);
                float halfLane = Mathf.Max(1.65f,
                    roadGraph.GetLaneWidth(currentNodeIndex, targetNodeIndex) * .5f);
                if (TrafficParticipant.TryFindAhead(transform, scan, halfLane,
                        out TrafficParticipant leader, out float gap) &&
                    AIVehicleController.ShouldStartOvertake(desiredKmh, leader.SpeedKmh, gap,
                        minHeadway, true))
                {
                    float passSide = AIVehicleController.LaneOffsetDelta(roadGraph,
                        currentNodeIndex, targetNodeIndex, laneIndex, passLane);
                    float clearLength = Mathf.Max(25f, speedKmh / 3.6f * 1.4f + 12f);
                    if (Mathf.Abs(passSide) > .05f &&
                        TrafficParticipant.PassingLaneClear(transform, passSide, clearLength))
                    {
                        StartOvertake(leader, passLane);
                        return;
                    }
                }
            }
        }

        if (returnToCruiseLane && laneDisciplineTimer <= 0f && laneCount > 1 &&
            distanceToJunction > 10f)
        {
            int cruiseLane = AIVehicleController.CruiseLaneIndex(roadGraph,
                currentNodeIndex, targetNodeIndex);
            if (cruiseLane != laneIndex)
            {
                float side = AIVehicleController.LaneOffsetDelta(roadGraph,
                    currentNodeIndex, targetNodeIndex, laneIndex, cruiseLane);
                float clearLength = Mathf.Max(18f, speedKmh / 3.6f + 8f);
                if (Mathf.Abs(side) > .05f &&
                    TrafficParticipant.PassingLaneClear(transform, side, clearLength))
                {
                    BeginLaneChange(cruiseLane);
                    return;
                }
            }
        }

        laneShiftTarget = 0f;
    }

    void StartOvertake(TrafficParticipant leader, int passLane)
    {
        IsOvertaking = true;
        returningToCruiseLane = false;
        passTarget = leader;
        overtakeLaneIndex = passLane;
        overtakeTimer = 0f;
        BeginLaneChange(passLane);
        laneShiftTarget = 0f;
    }

    void EndOvertake()
    {
        IsOvertaking = false;
        passTarget = null;
        overtakeLaneIndex = -1;
        laneShiftTarget = 0f;
        overtakeCooldown = Mathf.Max(0f, overtakeCooldownSeconds);
        returningToCruiseLane = roadGraph != null &&
            roadGraph.IsValidNode(currentNodeIndex) && roadGraph.IsValidNode(targetNodeIndex);
    }

    /// <summary>Persisted lane change: move the lane index, keep the pose with a decaying offset.</summary>
    void BeginLaneChange(int targetLane)
    {
        if (roadGraph == null || !roadGraph.IsValidNode(currentNodeIndex) ||
            !roadGraph.IsValidNode(targetNodeIndex))
            return;
        int lanes = Mathf.Max(1, roadGraph.GetLaneCount(currentNodeIndex, targetNodeIndex));
        if (targetLane < 0 || targetLane >= lanes || targetLane == laneIndex)
            return;

        float oldOffset = roadGraph.GetLaneOffset(currentNodeIndex, targetNodeIndex, laneIndex);
        float newOffset = roadGraph.GetLaneOffset(currentNodeIndex, targetNodeIndex, targetLane);
        laneShift += oldOffset - newOffset;
        laneIndex = targetLane;
        laneShiftTarget = 0f;
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
        int cruiseLane = AIVehicleController.CruiseLaneIndex(roadGraph,
            currentNodeIndex, targetNodeIndex);
        if (cruiseLane == laneIndex || lanes <= 1)
        {
            returningToCruiseLane = false;
            return false;
        }

        if (Mathf.Abs(laneShift) > .35f || distanceToJunction < 8f)
            return true;

        float side = AIVehicleController.LaneOffsetDelta(roadGraph, currentNodeIndex,
            targetNodeIndex, laneIndex, cruiseLane);
        float clearLength = Mathf.Max(18f, speedKmh / 3.6f + 8f);
        if (Mathf.Abs(side) > .05f &&
            TrafficParticipant.PassingLaneClear(transform, side, clearLength))
        {
            BeginLaneChange(cruiseLane);
            returningToCruiseLane = false;
            laneShiftTarget = 0f;
        }
        return true;
    }

    void EnsureTargetNode(bool forceAdvance = false)
    {
        if (roadGraph == null) return;

        bool invalid = !roadGraph.IsValidNode(targetNodeIndex) || targetNodeIndex == currentNodeIndex;
        if (forceAdvance || invalid)
        {
            if (cachedPath.Count == 0)
                RebuildPath();
            if (cachedPath.Count > 0)
                targetNodeIndex = cachedPath.Dequeue();
            else
                targetNodeIndex = roadGraph.GetRandomNextNode(currentNodeIndex, previousNodeIndex);

            if (targetNodeIndex == currentNodeIndex)
                targetNodeIndex = roadGraph.GetRandomNextNode(currentNodeIndex, -1);
        }
    }

    void RebuildPath()
    {
        cachedPath.Clear();
        if (roadGraph == null || !roadGraph.IsValidNode(currentNodeIndex))
            return;

        // The queue starts after the current target. Walk the chain with the node
        // just left excluded, so a two-way road does not make the cached path
        // alternate backwards and forwards at every segment.
        int cursor = roadGraph.IsValidNode(targetNodeIndex) && targetNodeIndex != currentNodeIndex
            ? targetNodeIndex
            : currentNodeIndex;
        int previous = cursor == targetNodeIndex ? currentNodeIndex : previousNodeIndex;
        for (int i = 0; i < lookaheadNodes; i++)
        {
            int next = roadGraph.GetRandomNextNode(cursor, previous);
            if (!roadGraph.IsValidNode(next))
                break;
            cachedPath.Enqueue(next);
            previous = cursor;
            cursor = next;
        }
    }
}
