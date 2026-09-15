using UnityEngine;
using System.Collections.Generic;

public class SplineVehicle : MonoBehaviour
{
    static readonly List<SplineVehicle> ActiveSplineVehicles = new List<SplineVehicle>();

    [Header("Graph")]
    public AIRoadGraph roadGraph;
    public int currentNodeIndex = -1;
    public int targetNodeIndex = -1;
    public int laneIndex = 0;

    [Header("Motion")]
    public float speedKmh = 0f;
    public float accelKmhPerSec = 16f;
    public float brakeKmhPerSec = 25f;
    public float turnRate = 7f;
    public float minHeadway = 10f;
    public float redLightStopDistance = 18f;

    [Header("Path Cache")]
    public int lookaheadNodes = 8;
    float emergencyYieldSeconds;
    float stuckSeconds;
    Vector3 lastProgressPosition;
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

    public void Init(AIRoadGraph graph, int currentNode, int targetNode, int lane, float initialSpeedKmh)
    {
        roadGraph = graph;
        currentNodeIndex = currentNode;
        targetNodeIndex = targetNode;
        laneIndex = lane;
        speedKmh = Mathf.Max(0f, initialSpeedKmh);
        RebuildPath();
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
        if (roadGraph == null || !roadGraph.IsValidNode(currentNodeIndex))
            return;

        EnsureTargetNode();
        if (!roadGraph.IsValidNode(targetNodeIndex))
            return;

        Vector3 target = roadGraph.GetNodePosition(targetNodeIndex);
        Vector3 segment = (target - roadGraph.GetNodePosition(currentNodeIndex)).normalized;
        emergencyYieldSeconds = Mathf.Max(0f, emergencyYieldSeconds - Time.deltaTime);
        target += Vector3.Cross(Vector3.up, segment) * (roadGraph.GetLaneOffset(currentNodeIndex, laneIndex) - (emergencyYieldSeconds > 0f ? 3f : 0f));
        Vector3 to = target - transform.position;
        to.y = 0f;
        float dist = to.magnitude;
        Vector3 dir = dist > 0.001f ? to / dist : transform.forward;

        float desired = roadGraph.GetSpeedLimitKmh(currentNodeIndex);
        if (emergencyYieldSeconds > 0f) desired = Mathf.Min(desired, 8f);
        if (roadGraph.IsRedSignal(targetNodeIndex))
            desired = Mathf.Min(desired, RealismRules.StoppingSpeedKmh(dist, 5f, brakeKmhPerSec / 3.6f * 0.65f));

        float frontLimited = TrafficParticipant.LimitSpeed(transform, ApplyHeadwayLimit(desired), 5f, brakeKmhPerSec / 3.6f * 0.65f);
        float rate = frontLimited < speedKmh ? brakeKmhPerSec : accelKmhPerSec;
        speedKmh = Mathf.MoveTowards(speedKmh, frontLimited, rate * Time.deltaTime);

        float speedMs = speedKmh / 3.6f;
        Vector3 nextPos = Vector3.MoveTowards(transform.position, target, speedMs * Time.deltaTime);
        transform.position = nextPos;
        UpdateStuckRecovery();

        if (dir.sqrMagnitude > 0.001f)
        {
            Quaternion look = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, turnRate * Time.deltaTime);
        }

        if (dist <= 3.5f)
        {
            currentNodeIndex = targetNodeIndex;
            EnsureTargetNode(forceAdvance: true);
        }
    }

    void UpdateStuckRecovery()
    {
        float moved = Vector3.Distance(transform.position, lastProgressPosition); lastProgressPosition = transform.position;
        bool legitimateStop = emergencyYieldSeconds > 0f || roadGraph.IsRedSignal(targetNodeIndex) || TrafficParticipant.HasVehicleAhead(transform, minHeadway + 2f);
        if (moved > 0.025f || speedKmh > 1.5f || legitimateStop) { stuckSeconds = 0f; return; }
        stuckSeconds += Time.deltaTime;
        if (stuckSeconds >= 18f) { cachedPath.Clear(); EnsureTargetNode(forceAdvance: true); stuckSeconds = 0f; }
    }

    float ApplyHeadwayLimit(float desiredKmh)
    {
        SplineVehicle front = FindFrontSplineVehicle();
        if (front == null) return desiredKmh;

        float gap = Vector3.Distance(transform.position, front.transform.position);
        if (gap >= minHeadway) return desiredKmh;
        return Mathf.Min(desiredKmh, front.speedKmh * Mathf.Clamp01(gap / Mathf.Max(0.5f, minHeadway)));
    }

    SplineVehicle FindFrontSplineVehicle()
    {
        Vector3 fwd = transform.forward;
        SplineVehicle best = null;
        float bestDist = float.MaxValue;
        for (int i = 0; i < ActiveSplineVehicles.Count; i++)
        {
            var other = ActiveSplineVehicles[i];
            if (other == null || other == this) continue;
            Vector3 to = other.transform.position - transform.position;
            float dist = to.magnitude;
            if (dist < 0.001f) continue;
            float ahead = Vector3.Dot(fwd, to / dist);
            if (ahead < 0.65f) continue;
            if (dist < bestDist)
            {
                bestDist = dist;
                best = other;
            }
        }
        return best;
    }

    void EnsureTargetNode(bool forceAdvance = false)
    {
        if (roadGraph == null) return;

        bool invalid = !roadGraph.IsValidNode(targetNodeIndex);
        if (forceAdvance || invalid)
        {
            if (cachedPath.Count == 0)
                RebuildPath();
            targetNodeIndex = cachedPath.Count > 0 ? cachedPath.Dequeue() : roadGraph.GetRandomNextNode(currentNodeIndex);
        }
    }

    void RebuildPath()
    {
        cachedPath.Clear();
        if (roadGraph == null || !roadGraph.IsValidNode(currentNodeIndex))
            return;

        int cursor = roadGraph.IsValidNode(targetNodeIndex) ? targetNodeIndex : currentNodeIndex;
        for (int i = 0; i < lookaheadNodes; i++)
        {
            int next = roadGraph.GetRandomNextNode(cursor);
            if (!roadGraph.IsValidNode(next))
                break;
            cachedPath.Enqueue(next);
            cursor = next;
        }
    }
}
