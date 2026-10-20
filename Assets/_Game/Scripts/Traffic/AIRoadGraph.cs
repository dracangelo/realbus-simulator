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
        public int[] outgoingLaneCounts;
        public float[] outgoingLaneWidths;
        public bool[] outgoingOneWays;
        /// <summary>True for a two-way link that is only one lane wide in total, so
        /// both directions share the same centred lane instead of each taking half
        /// a lane they do not have.</summary>
        public bool[] outgoingSharedLanes;
    }

    [Header("Road Graph")]
    public RoadNode[] nodes;
    [Tooltip("True for a graph that serves one private circuit (a depot yard). Its own vehicles drive it, so the city traffic pool and the scene bootstrap must never bind it as the city graph - it is nearest the player exactly when the player is at that depot.")]
    public bool privateLaneCircuit;
    [Tooltip("Which side of a two-way road this graph's traffic keeps to. Synced from the active city's country when the graph is bound; defaulted to left because the shipped scene opens in Kenya.")]
    public bool drivesOnLeft = true;

    public int NodeCount => nodes != null ? nodes.Length : 0;

    /// <summary>
    /// The side of the road traffic should use, resolved from the active city rather
    /// than only CityManager.activeCountry: the selected city and the selected country
    /// can disagree (the menu stores both), and ResolveCountry prefers the city.
    /// </summary>
    public static bool ResolveDrivesOnLeft()
    {
        var country = RoadSpawnResolver.ResolveCountry();
        if (country != null) return country.DrivesOnLeft;
        var manager = CityManager.Instance;
        if (manager != null && manager.activeCountry != null) return manager.activeCountry.DrivesOnLeft;
        return true;
    }

    /// <summary>
    /// Call when this graph is bound to a city, so the side is fixed before a
    /// vehicle is placed. Returns true when the side actually changed, which
    /// lets a live traffic pool re-seat its vehicles instead of leaving them
    /// on the opposite carriageway after a late city selection.
    /// </summary>
    public bool SyncDrivingSide()
    {
        bool resolved = ResolveDrivesOnLeft();
        if (resolved == drivesOnLeft) return false;
        drivesOnLeft = resolved;
        return true;
    }

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
        return GetRandomNextNode(currentIndex, -1);
    }

    /// <summary>
    /// Picks the next node of a directed edge, refusing an immediate reversal while
    /// the junction offers any other way out.
    ///
    /// A two-way OSM road is two opposite directed edges between the same pair of
    /// nodes. A uniform pick among a chain node's two outgoing edges therefore turned
    /// half the traffic around at every node: cars crossed the centre line, drove back
    /// into the player head-on, and lurched as the controller tried to face a target
    /// behind them. The node just left is excluded whenever there is an alternative;
    /// a genuine dead end still reverses, because there is no other edge.
    /// </summary>
    public int GetRandomNextNode(int currentIndex, int previousNodeIndex)
    {
        if (!IsValidNode(currentIndex)) return -1;
        var next = nodes[currentIndex].nextNodeIndices;
        if (next == null || next.Length == 0) return -1;

        int reverse = -1;
        int chosen = -1;
        int candidateCount = 0;
        float totalWeight = 0f;
        bool haveIncoming = IsValidNode(previousNodeIndex);
        Vector3 currentPosition = GetNodePosition(currentIndex);
        Vector3 incoming = haveIncoming
            ? currentPosition - GetNodePosition(previousNodeIndex)
            : Vector3.zero;
        incoming.y = 0f;
        if (incoming.sqrMagnitude > .0001f) incoming.Normalize();
        else haveIncoming = false;

        for (int i = 0; i < next.Length; i++)
        {
            int candidate = next[i];
            if (!IsValidNode(candidate) || candidate == currentIndex) continue;
            if (candidate == previousNodeIndex) { reverse = candidate; continue; }

            candidateCount++;
            float weight = 1f;
            if (haveIncoming)
            {
                Vector3 outgoing = GetNodePosition(candidate) - currentPosition;
                outgoing.y = 0f;
                if (outgoing.sqrMagnitude > .0001f)
                {
                    float dot = Mathf.Clamp(Vector3.Dot(incoming, outgoing.normalized), -1f, 1f);
                    // Prefer carrying on over turning, but keep every legal turn possible.
                    weight = Mathf.Lerp(0.08f, 1f, (dot + 1f) * .5f);
                }
            }

            totalWeight += weight;
            chosen = candidate;
        }

        if (candidateCount == 0)
            return reverse;

        if (candidateCount == 1)
            return chosen;

        float roll = Random.Range(0f, totalWeight);
        float accumulated = 0f;
        for (int i = 0; i < next.Length; i++)
        {
            int candidate = next[i];
            if (!IsValidNode(candidate) || candidate == currentIndex || candidate == previousNodeIndex) continue;

            float weight = 1f;
            if (haveIncoming)
            {
                Vector3 outgoing = GetNodePosition(candidate) - currentPosition;
                outgoing.y = 0f;
                if (outgoing.sqrMagnitude > .0001f)
                {
                    float dot = Mathf.Clamp(Vector3.Dot(incoming, outgoing.normalized), -1f, 1f);
                    weight = Mathf.Lerp(0.08f, 1f, (dot + 1f) * .5f);
                }
            }

            accumulated += weight;
            if (roll <= accumulated) return candidate;
        }

        return chosen;
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
        return GetRandomNextNode(currentIndex, avoidNode);
    }

    public float GetLaneOffset(int nodeIndex, int laneIndex)
    {
        if (!IsValidNode(nodeIndex)) return 0f;
        int lanes = Mathf.Max(1, nodes[nodeIndex].laneCount);
        laneIndex = Mathf.Clamp(laneIndex, 0, lanes - 1);
        float width = Mathf.Max(MinLaneWidth, nodes[nodeIndex].laneWidth);

        // Center lanes around road middle: e.g. 2 lanes -> -0.5,+0.5 multipliers.
        float centered = laneIndex - (lanes - 1) * 0.5f;
        return centered * width;
    }

    public int GetLaneCount(int fromNode, int toNode)
    {
        if (!IsValidNode(fromNode)) return 1;
        RoadNode node = nodes[fromNode];
        for (int i = 0; node.nextNodeIndices != null && i < node.nextNodeIndices.Length; i++)
            if (node.nextNodeIndices[i] == toNode && node.outgoingLaneCounts != null && i < node.outgoingLaneCounts.Length)
                return Mathf.Max(1, node.outgoingLaneCounts[i]);
        return Mathf.Max(1, node.laneCount);
    }

    /// <summary>
    /// True when this directed link is a one-way carriageway. Lane changes and
    /// overtakes are only safe there; on a two-way link the next lateral lane
    /// would be the oncoming carriageway, which is how traffic ended up
    /// straddling the centre line whenever it tried to pass a stopped bus.
    /// </summary>
    public bool IsOneWay(int fromNode, int toNode)
    {
        if (!IsValidNode(fromNode)) return false;
        RoadNode node = nodes[fromNode];
        for (int i = 0; node.nextNodeIndices != null && i < node.nextNodeIndices.Length; i++)
            if (node.nextNodeIndices[i] == toNode && node.outgoingOneWays != null && i < node.outgoingOneWays.Length)
                return node.outgoingOneWays[i];
        return false;
    }

    public bool DrivesOnLeft => drivesOnLeft;

    /// <summary>
    /// Smallest lane the traffic geometry will place a vehicle in. OSM lane tags are
    /// advisory and a narrow residential street is still driven down its middle by a
    /// 2 m wide car, so the offset never collapses below this.
    /// </summary>
    public const float MinLaneWidth = 2.9f;

    public float GetLaneOffset(int fromNode, int toNode, int laneIndex)
    {
        if (!IsValidNode(fromNode) || !IsValidNode(toNode)) return 0f;
        RoadNode node = nodes[fromNode];
        int link = -1;
        for (int i = 0; node.nextNodeIndices != null && i < node.nextNodeIndices.Length; i++) if (node.nextNodeIndices[i] == toNode) { link = i; break; }
        int lanes = GetLaneCount(fromNode, toNode);
        float width = link >= 0 && node.outgoingLaneWidths != null && link < node.outgoingLaneWidths.Length
            ? node.outgoingLaneWidths[link] : node.laneWidth;
        width = Mathf.Max(MinLaneWidth, width);
        bool oneWay = link >= 0 && node.outgoingOneWays != null && link < node.outgoingOneWays.Length && node.outgoingOneWays[link];
        bool shared = link >= 0 && node.outgoingSharedLanes != null && link < node.outgoingSharedLanes.Length && node.outgoingSharedLanes[link];
        laneIndex = Mathf.Clamp(laneIndex, 0, lanes - 1);

        // A single-lane road used in both directions has no half to belong to: both
        // directions keep to the middle of the same lane.
        if (shared) return 0f;

        if (oneWay)
            return (laneIndex - (lanes - 1) * .5f) * width;

        // A directed link uses only its real direction of travel, and lanes are laid
        // out across that carriageway: lane 0 is the one nearest the centre line and
        // every further lane is one full width outboard. The old formula measured from
        // the middle of a carriageway that did not exist, putting every vehicle metres
        // off the road it was meant to drive on.
        bool left = drivesOnLeft;
        float side = left ? -1f : 1f;
        return side * (laneIndex + .5f) * width;
    }

    /// <summary>
    /// Appends every road node within <paramref name="radius"/> of a world point.
    /// The traffic pool uses this to spawn into the streets around the bus instead
    /// of sampling the whole city and rejecting candidates outside the band.
    /// </summary>
    public void CollectNodesWithin(Vector3 center, float radius, List<int> results, bool requireOutgoingConnection = true)
    {
        if (results == null) return;
        if (nodes == null || nodes.Length == 0) return;
        float radiusSqr = Mathf.Max(0f, radius) * Mathf.Max(0f, radius);
        for (int i = 0; i < nodes.Length; i++)
        {
            if (!IsValidNode(i)) continue;
            if (requireOutgoingConnection && !HasOutgoingConnection(i)) continue;
            Vector3 offset = nodes[i].point.position - center;
            offset.y = 0f;
            if (offset.sqrMagnitude <= radiusSqr) results.Add(i);
        }
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
