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

    public void Init(AIRoadGraph graph, int currentNode, int targetNode, int lane, float initialSpeedKmh, int previousNode = -1)
    {
        roadGraph = graph;
        currentNodeIndex = currentNode;
        targetNodeIndex = targetNode;
        previousNodeIndex = previousNode;
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

        // Match the full-physics controller: lanes belong to the directed
        // edge, so clamp after a turn onto a road with fewer lanes.
        laneIndex = Mathf.Clamp(laneIndex, 0, Mathf.Max(1, roadGraph.GetLaneCount(currentNodeIndex, targetNodeIndex)) - 1);

        Vector3 nodeA = roadGraph.GetNodePosition(currentNodeIndex);
        Vector3 nodeB = roadGraph.GetNodePosition(targetNodeIndex);
        Vector3 segment = nodeB - nodeA;
        segment.y = 0f;
        if (segment.sqrMagnitude < .0001f) return;
        segment.Normalize();
        emergencyYieldSeconds = Mathf.Max(0f, emergencyYieldSeconds - Time.deltaTime);

        // The same lane line the full-physics controller drives: a lane point at each
        // end of this link, and a lookahead on it, so a low-detail car also stays in
        // its lane instead of cutting diagonally towards the next junction.
        float yieldOffset = emergencyYieldSeconds > 0f
            ? AIVehicleController.KerbShift(roadGraph, currentNodeIndex, targetNodeIndex, laneIndex, 3f)
            : 0f;
        float offset = roadGraph.GetLaneOffset(currentNodeIndex, targetNodeIndex, laneIndex) + yieldOffset;
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
        target -= right * Vector3.Dot(transform.position - laneA, right) * AIVehicleController.LaneCenteringGain;

        Vector3 to = laneB - transform.position;
        to.y = 0f;
        float dist = to.magnitude;
        Vector3 dir = target - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < .0001f) dir = segment;
        else dir.Normalize();

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
            previousNodeIndex = currentNodeIndex;
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
        if (stuckSeconds >= 18f)
        {
            cachedPath.Clear();
            int next = roadGraph != null ? roadGraph.GetRandomNextNode(currentNodeIndex, previousNodeIndex) : -1;
            if (roadGraph != null && roadGraph.IsValidNode(next)) targetNodeIndex = next;
            stuckSeconds = 0f;
        }
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
