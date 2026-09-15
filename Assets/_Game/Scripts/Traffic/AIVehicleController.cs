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
    public int laneIndex = 0;

    [Header("Motion")]
    public float maxAccel = 6f;
    public float maxBrake = 9f;
    public float turnRate = 4f;
    public float stopDistance = 4f;
    public float safeFollowingDistance = 9f;
    public float overtakingLaneShift = 3.0f;
    public float vehicleLength = 4.2f;

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
        lastProgressPosition = transform.position;
    }

    public void Init(AIRoadGraph graph, int startNode, int lane, VehicleType type)
    {
        roadGraph = graph;
        currentNodeIndex = startNode;
        laneIndex = lane;
        vehicleType = type;
        targetNodeIndex = graph != null ? graph.GetRandomNextNode(startNode) : -1;
        if (graph != null && graph.IsValidNode(startNode))
            transform.position = graph.GetNodePosition(startNode);
    }

    public void SetRuntimeState(AIRoadGraph graph, int currentNode, int targetNode, int lane, VehicleType type, float speedKmh)
    {
        roadGraph = graph;
        currentNodeIndex = currentNode;
        targetNodeIndex = targetNode;
        laneIndex = lane;
        vehicleType = type;
        currentSpeedKmh = Mathf.Max(0f, speedKmh);
        if (rb != null)
            if (!rb.isKinematic) rb.linearVelocity = transform.forward * (currentSpeedKmh / 3.6f);
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

        if (!roadGraph.IsValidNode(targetNodeIndex))
            targetNodeIndex = roadGraph.GetRandomNextNode(currentNodeIndex);
        if (!roadGraph.IsValidNode(targetNodeIndex))
            return;

        Vector3 current = transform.position;
        Vector3 target = roadGraph.GetNodePosition(targetNodeIndex);
        Vector3 segment = (target - roadGraph.GetNodePosition(currentNodeIndex)).normalized;
        target += Vector3.Cross(Vector3.up, segment) * (roadGraph.GetLaneOffset(currentNodeIndex, laneIndex) + laneShift);
        Vector3 travelDir = (target - current);
        travelDir.y = 0f;
        float distance = travelDir.magnitude;
        if (distance > 0.001f) travelDir /= distance;

        float desiredSpeed = ResolveDesiredSpeedKmh();
        float safeSpeed = ApplyTrafficAndFollowingRules(desiredSpeed);
        MoveVehicle(travelDir, safeSpeed);
        UpdateStuckRecovery();

        if (distance <= Mathf.Min(2f, stopDistance))
        {
            currentNodeIndex = targetNodeIndex;
            targetNodeIndex = roadGraph.GetRandomNextNode(currentNodeIndex);
            if (!isOvertaking) laneShift = 0f;
        }
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
        if (stuckSeconds >= 12f && recoveryLaneTimer <= 0f && roadGraph.nodes[currentNodeIndex].laneCount > 1)
        {
            float side = laneIndex == 0 ? Mathf.Abs(overtakingLaneShift) : -Mathf.Abs(overtakingLaneShift);
            if (TrafficParticipant.PassingLaneClear(transform, side, 25f)) { laneShift = side; recoveryLaneTimer = 5f; stuckSeconds = 0f; return; }
        }
        if (stuckSeconds >= 20f)
        {
            int next = roadGraph.GetRandomNextNode(currentNodeIndex);
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

        if (!isOvertaking && roadGraph.nodes[currentNodeIndex].laneCount > 1 && !roadGraph.IsRedSignal(targetNodeIndex))
        {
            var stoppedBus = TrafficParticipant.StoppedBusAhead(transform, safeFollowingDistance * 2.5f);
            if (stoppedBus != null && TrafficParticipant.PassingLaneClear(transform, overtakingLaneShift))
            { passingBus = stoppedBus; isOvertaking = true; laneShift = overtakingLaneShift; }
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
            laneShift = -Mathf.Abs(overtakingLaneShift);
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
        float targetMs = targetSpeedKmh / 3.6f;
        float currentMs = currentSpeedKmh / 3.6f;
        float accel = targetMs >= currentMs ? maxAccel : maxBrake;
        float newMs = Mathf.MoveTowards(currentMs, targetMs, accel * Time.fixedDeltaTime);
        // Physics owns position; the lane target is an absolute offset, never a per-frame drift.
        Vector3 velocity = forwardDir * newMs;
        if (isOvertaking && passingBus != null)
        {
            Vector3 side = passingBus.right;
            float offset = Vector3.Dot(side, transform.position - passingBus.position);
            velocity += side * Mathf.Clamp((overtakingLaneShift - offset) * 1.5f, -1.5f, 1.5f);
        }
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;

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
