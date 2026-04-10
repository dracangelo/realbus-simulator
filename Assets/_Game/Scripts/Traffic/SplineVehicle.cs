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
    readonly Queue<int> cachedPath = new Queue<int>();

    void OnEnable()
    {
        if (!ActiveSplineVehicles.Contains(this))
            ActiveSplineVehicles.Add(this);
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
        Vector3 to = target - transform.position;
        to.y = 0f;
        float dist = to.magnitude;
        Vector3 dir = dist > 0.001f ? to / dist : transform.forward;

        float desired = roadGraph.GetSpeedLimitKmh(currentNodeIndex);
        if (roadGraph.IsRedSignal(targetNodeIndex) && dist <= redLightStopDistance)
            desired = 0f;

        float frontLimited = ApplyHeadwayLimit(desired);
        float rate = frontLimited < speedKmh ? brakeKmhPerSec : accelKmhPerSec;
        speedKmh = Mathf.MoveTowards(speedKmh, frontLimited, rate * Time.deltaTime);

        float speedMs = speedKmh / 3.6f;
        float laneOffset = roadGraph.GetLaneOffset(currentNodeIndex, laneIndex);
        Vector3 side = Vector3.Cross(Vector3.up, dir).normalized;
        Vector3 nextPos = transform.position + dir * speedMs * Time.deltaTime + side * laneOffset * 0.035f;
        transform.position = nextPos;

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

        int cursor = currentNodeIndex;
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
