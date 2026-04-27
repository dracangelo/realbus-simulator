using UnityEngine;
using System.Collections.Generic;

public class AIRoadGraph : MonoBehaviour
{
    [System.Serializable]
    public class RoadNode
    {
        public string id = "N0";
        public Transform point;
        public int laneCount = 2;
        public float laneWidth = 3.3f;
        public float speedLimitKmh = 40f;
        public TrafficLight trafficLight;
        public int[] nextNodeIndices;
    }

    [Header("Road Graph")]
    public RoadNode[] nodes;

    public int NodeCount => nodes != null ? nodes.Length : 0;

    public bool IsValidNode(int index)
    {
        return nodes != null && index >= 0 && index < nodes.Length && nodes[index] != null && nodes[index].point != null;
    }

    public Vector3 GetNodePosition(int index)
    {
        return IsValidNode(index) ? nodes[index].point.position : Vector3.zero;
    }

    public float GetSpeedLimitKmh(int index)
    {
        if (!IsValidNode(index)) return 30f;
        return Mathf.Max(10f, nodes[index].speedLimitKmh);
    }

    public bool IsRedSignal(int index)
    {
        if (!IsValidNode(index)) return false;
        var tl = nodes[index].trafficLight;
        return tl != null && tl.IsRed();
    }

    public int GetRandomNextNode(int currentIndex)
    {
        if (!IsValidNode(currentIndex)) return -1;
        var next = nodes[currentIndex].nextNodeIndices;
        if (next == null || next.Length == 0) return -1;

        int candidate = next[Random.Range(0, next.Length)];
        return IsValidNode(candidate) ? candidate : -1;
    }

    public int GetRandomNodeIndex(bool requireOutgoingConnection = true, int maxAttempts = 64)
    {
        if (nodes == null || nodes.Length == 0)
            return -1;

        int attempts = Mathf.Max(1, maxAttempts);
        for (int i = 0; i < attempts; i++)
        {
            int candidate = Random.Range(0, nodes.Length);
            if (!IsValidNode(candidate))
                continue;
            if (requireOutgoingConnection && !HasOutgoingConnection(candidate))
                continue;
            return candidate;
        }

        for (int i = 0; i < nodes.Length; i++)
        {
            if (!IsValidNode(i))
                continue;
            if (requireOutgoingConnection && !HasOutgoingConnection(i))
                continue;
            return i;
        }

        return -1;
    }

    public bool HasOutgoingConnection(int index)
    {
        if (!IsValidNode(index))
            return false;

        var next = nodes[index].nextNodeIndices;
        if (next == null || next.Length == 0)
            return false;

        for (int i = 0; i < next.Length; i++)
            if (IsValidNode(next[i]))
                return true;

        return false;
    }

    public int[] BuildRandomNodeSequence(int desiredLength, int startNodeIndex = -1)
    {
        if (nodes == null || nodes.Length == 0)
            return System.Array.Empty<int>();

        int length = Mathf.Max(2, desiredLength);
        int start = IsValidNode(startNodeIndex) && HasOutgoingConnection(startNodeIndex)
            ? startNodeIndex
            : GetRandomNodeIndex(requireOutgoingConnection: true);

        if (!IsValidNode(start))
            return System.Array.Empty<int>();

        var sequence = new List<int>(length) { start };
        int cursor = start;

        for (int i = 1; i < length; i++)
        {
            int next = GetRandomConnectedNode(cursor, sequence.Count > 1 ? sequence[sequence.Count - 2] : -1);
            if (!IsValidNode(next))
                break;

            sequence.Add(next);
            cursor = next;
        }

        return sequence.Count >= 2 ? sequence.ToArray() : System.Array.Empty<int>();
    }

    int GetRandomConnectedNode(int currentIndex, int avoidNode = -1)
    {
        if (!IsValidNode(currentIndex))
            return -1;

        var next = nodes[currentIndex].nextNodeIndices;
        if (next == null || next.Length == 0)
            return -1;

        var valid = new List<int>(next.Length);
        for (int i = 0; i < next.Length; i++)
        {
            int candidate = next[i];
            if (!IsValidNode(candidate))
                continue;
            if (candidate == avoidNode && next.Length > 1)
                continue;
            valid.Add(candidate);
        }

        if (valid.Count == 0)
            return IsValidNode(avoidNode) ? avoidNode : -1;

        return valid[Random.Range(0, valid.Count)];
    }

    public float GetLaneOffset(int nodeIndex, int laneIndex)
    {
        if (!IsValidNode(nodeIndex)) return 0f;
        int lanes = Mathf.Max(1, nodes[nodeIndex].laneCount);
        laneIndex = Mathf.Clamp(laneIndex, 0, lanes - 1);
        float width = Mathf.Max(2.5f, nodes[nodeIndex].laneWidth);

        // Center lanes around road middle: e.g. 2 lanes -> -0.5,+0.5 multipliers.
        float centered = laneIndex - (lanes - 1) * 0.5f;
        return centered * width;
    }

    public int GetNearestNodeIndex(Vector3 worldPos)
    {
        if (nodes == null || nodes.Length == 0) return -1;
        int best = -1;
        float bestSqr = float.MaxValue;
        for (int i = 0; i < nodes.Length; i++)
        {
            if (!IsValidNode(i)) continue;
            float sqr = (nodes[i].point.position - worldPos).sqrMagnitude;
            if (sqr < bestSqr)
            {
                bestSqr = sqr;
                best = i;
            }
        }
        return best;
    }

    public List<int> GetNodesWithinRadius(Vector3 worldPos, float radiusMeters)
    {
        var results = new List<int>();
        if (nodes == null || nodes.Length == 0)
            return results;

        float radiusSqr = Mathf.Max(0.1f, radiusMeters) * Mathf.Max(0.1f, radiusMeters);
        for (int i = 0; i < nodes.Length; i++)
        {
            if (!IsValidNode(i))
                continue;

            if ((nodes[i].point.position - worldPos).sqrMagnitude <= radiusSqr)
                results.Add(i);
        }

        return results;
    }

    public bool TryFindPathWorld(Vector3 startWorld, Vector3 endWorld, out List<Vector3> worldPath, ISet<int> blockedNodes = null)
    {
        worldPath = null;

        int startIndex = GetNearestReachableNodeIndex(startWorld, blockedNodes);
        int endIndex = GetNearestReachableNodeIndex(endWorld, blockedNodes);
        if (!IsValidNode(startIndex) || !IsValidNode(endIndex))
            return false;

        if (!TryFindNodePath(startIndex, endIndex, out var nodePath, blockedNodes))
            return false;

        worldPath = new List<Vector3>(nodePath.Count + 2) { startWorld };
        for (int i = 0; i < nodePath.Count; i++)
        {
            Vector3 point = nodes[nodePath[i]].point.position;
            if ((point - worldPath[worldPath.Count - 1]).sqrMagnitude > 1f)
                worldPath.Add(point);
        }

        if ((endWorld - worldPath[worldPath.Count - 1]).sqrMagnitude > 1f)
            worldPath.Add(endWorld);

        return worldPath.Count >= 2;
    }

    public bool TryFindNodePath(int startIndex, int endIndex, out List<int> nodePath, ISet<int> blockedNodes = null)
    {
        nodePath = null;
        if (!IsValidNode(startIndex) || !IsValidNode(endIndex))
            return false;
        if (IsBlocked(startIndex, blockedNodes) || IsBlocked(endIndex, blockedNodes))
            return false;

        var openSet = new List<int> { startIndex };
        var cameFrom = new Dictionary<int, int>();
        var gScore = new Dictionary<int, float> { [startIndex] = 0f };
        var fScore = new Dictionary<int, float> { [startIndex] = Heuristic(startIndex, endIndex) };
        var closedSet = new HashSet<int>();

        while (openSet.Count > 0)
        {
            int current = GetBestNode(openSet, fScore);
            if (current == endIndex)
            {
                nodePath = ReconstructPath(cameFrom, current);
                return nodePath.Count > 0;
            }

            openSet.Remove(current);
            closedSet.Add(current);

            var nextNodes = nodes[current].nextNodeIndices;
            if (nextNodes == null)
                continue;

            for (int i = 0; i < nextNodes.Length; i++)
            {
                int neighbor = nextNodes[i];
                if (!IsValidNode(neighbor) || IsBlocked(neighbor, blockedNodes) || closedSet.Contains(neighbor))
                    continue;

                float tentativeG = gScore[current] + Vector3.Distance(nodes[current].point.position, nodes[neighbor].point.position);
                if (!gScore.TryGetValue(neighbor, out float existingG) || tentativeG < existingG)
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeG;
                    fScore[neighbor] = tentativeG + Heuristic(neighbor, endIndex);
                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return false;
    }

    int GetNearestReachableNodeIndex(Vector3 worldPos, ISet<int> blockedNodes)
    {
        if (nodes == null || nodes.Length == 0)
            return -1;

        int best = -1;
        float bestSqr = float.MaxValue;
        for (int i = 0; i < nodes.Length; i++)
        {
            if (!IsValidNode(i) || IsBlocked(i, blockedNodes))
                continue;

            float sqr = (nodes[i].point.position - worldPos).sqrMagnitude;
            if (sqr < bestSqr)
            {
                bestSqr = sqr;
                best = i;
            }
        }

        return best;
    }

    static int GetBestNode(List<int> openSet, Dictionary<int, float> fScore)
    {
        int bestIndex = openSet[0];
        float bestValue = fScore.TryGetValue(bestIndex, out float initial) ? initial : float.MaxValue;
        for (int i = 1; i < openSet.Count; i++)
        {
            int candidate = openSet[i];
            float score = fScore.TryGetValue(candidate, out float value) ? value : float.MaxValue;
            if (score < bestValue)
            {
                bestValue = score;
                bestIndex = candidate;
            }
        }

        return bestIndex;
    }

    float Heuristic(int fromIndex, int toIndex)
    {
        return Vector3.Distance(nodes[fromIndex].point.position, nodes[toIndex].point.position);
    }

    static List<int> ReconstructPath(Dictionary<int, int> cameFrom, int current)
    {
        var path = new List<int> { current };
        while (cameFrom.TryGetValue(current, out int previous))
        {
            current = previous;
            path.Add(current);
        }

        path.Reverse();
        return path;
    }

    static bool IsBlocked(int nodeIndex, ISet<int> blockedNodes)
    {
        return blockedNodes != null && blockedNodes.Contains(nodeIndex);
    }
}
