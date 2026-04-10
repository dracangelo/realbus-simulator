using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class AIVehicleController : MonoBehaviour
{
    public enum VehicleType { Car, Truck, Motorcycle, Bus, Emergency }

    static readonly List<AIVehicleController> ActiveVehicles = new List<AIVehicleController>();

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
    float emergencyYieldTimer;

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
            rb.linearVelocity = transform.forward * (currentSpeedKmh / 3.6f);
    }

    public Vector3 GetVelocityWorld()
    {
        return rb != null ? rb.linearVelocity : Vector3.zero;
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
        Vector3 travelDir = (target - current);
        travelDir.y = 0f;
        float distance = travelDir.magnitude;
        if (distance > 0.001f) travelDir /= distance;

        float desiredSpeed = ResolveDesiredSpeedKmh();
        float safeSpeed = ApplyTrafficAndFollowingRules(desiredSpeed);
        MoveVehicle(travelDir, safeSpeed);

        if (distance <= Mathf.Max(stopDistance, vehicleLength))
        {
            currentNodeIndex = targetNodeIndex;
            targetNodeIndex = roadGraph.GetRandomNextNode(currentNodeIndex);
            isOvertaking = false;
            laneShift = 0f;
        }
    }

    float ResolveDesiredSpeedKmh()
    {
        float speedLimit = obeySpeedLimits ? roadGraph.GetSpeedLimitKmh(currentNodeIndex) : 45f;
        switch (vehicleType)
        {
            case VehicleType.Truck: return speedLimit * 0.8f;
            case VehicleType.Motorcycle: return speedLimit * 1.05f;
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
                if (distToSignal < 18f)
                    result = 0f;
            }
        }

        AIVehicleController front = FindFrontVehicle();
        if (front != null)
        {
            float gap = Vector3.Distance(transform.position, front.transform.position);
            if (gap < safeFollowingDistance)
                result = Mathf.Min(result, front.currentSpeedKmh * 0.9f);

            // Overtake very slow/stopped bus when safe.
            if (!isOvertaking && front.vehicleType == VehicleType.Bus && front.currentSpeedKmh < 2f && gap < safeFollowingDistance * 1.4f)
            {
                isOvertaking = true;
                laneShift = overtakingLaneShift;
            }
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

        return Mathf.Max(0f, result);
    }

    void MoveVehicle(Vector3 forwardDir, float targetSpeedKmh)
    {
        float targetMs = targetSpeedKmh / 3.6f;
        float currentMs = rb.linearVelocity.magnitude;
        float delta = targetMs - currentMs;
        float accel = delta >= 0f ? maxAccel : maxBrake;
        float newMs = Mathf.MoveTowards(currentMs, targetMs, accel * Time.fixedDeltaTime);

        Vector3 side = Vector3.Cross(Vector3.up, forwardDir).normalized;
        float laneOffset = roadGraph.GetLaneOffset(currentNodeIndex, laneIndex) + laneShift;
        Vector3 desiredPos = transform.position + forwardDir * newMs * Time.fixedDeltaTime + side * laneOffset * 0.05f;
        Vector3 move = (desiredPos - transform.position);
        rb.MovePosition(transform.position + move);

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
