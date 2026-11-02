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
        /// <summary>Per-link speed limit. A node can join a motorway to a residential
        /// street; storing one node-wide value let a car carry the motorway limit
        /// onto the slow road after the turn.</summary>
        public float[] outgoingSpeedLimits;
        /// <summary>Signal controlling this directed link as it approaches its end.
        /// Signals are bound per approach where the scene provides them; the
        /// node-level <see cref="trafficLight"/> remains the fallback for scenes
        /// that only author one signal per junction.</summary>
        public TrafficLight[] outgoingTrafficLights;
    }

    [Header("Road Graph")]
    public RoadNode[] nodes;
    [Tooltip("True for a graph that serves one private circuit (a depot yard). Its own vehicles drive it, so the city traffic pool and the scene bootstrap must never bind it as the city graph - it is nearest the player exactly when the player is at that depot.")]
    public bool privateLaneCircuit;
    [Tooltip("Which side of a two-way road this graph's traffic keeps to. Synced from the active city's country when the graph is bound; defaulted to left because the shipped scene opens in Kenya.")]
    public bool drivesOnLeft = true;

    [Tooltip("Runtime floor for lane spacing. VehiclePool raises this to the widest traffic model it actually spawns, so a bus or wide car never hangs over the centre line or the pavement.")]
    [SerializeField] float minimumLaneWidth = MinLaneWidth;

    public int NodeCount => nodes != null ? nodes.Length : 0;

    /// <summary>The lane spacing currently enforced, including the model-fit floor.</summary>
    public float MinimumLaneWidth => Mathf.Max(MinLaneWidth, minimumLaneWidth);

    /// <summary>
    /// Raises lane spacing so the widest spawned vehicle fits inside one lane.
    /// Existing graph widths are only ever increased: narrowing a road again after
    /// the player has driven it would make the traffic jump sideways.
    /// </summary>
    public void SetMinimumLaneWidth(float width)
    {
        float clamped = Mathf.Clamp(width, MinLaneWidth, 6f);
        if (clamped <= MinimumLaneWidth)
            return;

        minimumLaneWidth = clamped;
        if (nodes == null)
            return;

        for (int i = 0; i < nodes.Length; i++)
        {
            RoadNode node = nodes[i];
            if (node == null) continue;
            if (node.laneWidth < clamped)
                node.laneWidth = clamped;

            // Per-link widths are what the graph actually reads for this directed
            // edge. Raising only the node fallback left narrow links narrow, so a
            // wide imported car still hung over the centre line on those roads.
            if (node.outgoingLaneWidths == null) continue;
            for (int link = 0; link < node.outgoingLaneWidths.Length; link++)
            {
                if (node.outgoingLaneWidths[link] < clamped)
                    node.outgoingLaneWidths[link] = clamped;
            }
        }
    }

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

    /// <summary>
    /// The largest public city graph in the scene. Depot yards create their own
    /// tiny <see cref="privateLaneCircuit"/> graph, so an arbitrary
    /// FindAnyObjectByType&lt;AIRoadGraph&gt; can return an eight-node yard loop while
    /// the player is standing at the depot. Every consumer that needs the city
    /// network should start here instead.
    /// </summary>
    public static AIRoadGraph FindCityGraph()
    {
        AIRoadGraph best = null;
        int bestNodes = 0;
        var graphs = Object.FindObjectsByType<AIRoadGraph>(FindObjectsInactive.Exclude);
        for (int i = 0; i < graphs.Length; i++)
        {
            AIRoadGraph graph = graphs[i];
            if (graph == null || graph.privateLaneCircuit || graph.NodeCount <= bestNodes)
                continue;
            best = graph;
            bestNodes = graph.NodeCount;
        }
        return best;
    }

    /// <summary>
    /// The public city graph whose road network is nearest to <paramref name="worldPoint"/>.
    /// Returns null when every public graph is farther away than
    /// <paramref name="maxDistanceMeters"/>, which lets callers decide whether a
    /// city spawn or route start is actually on a known road.
    /// </summary>
    public static AIRoadGraph FindCityGraphNear(Vector3 worldPoint, float maxDistanceMeters = 2500f)
    {
        AIRoadGraph best = null;
        float bestDistance = Mathf.Max(0f, maxDistanceMeters);
        var graphs = Object.FindObjectsByType<AIRoadGraph>(FindObjectsInactive.Exclude);
        for (int i = 0; i < graphs.Length; i++)
        {
            AIRoadGraph graph = graphs[i];
            if (graph == null || graph.privateLaneCircuit || graph.NodeCount == 0)
                continue;

            int nearest = graph.GetNearestNodeIndex(worldPoint);
            if (!graph.IsValidNode(nearest))
                continue;

            float distance = Vector3.Distance(worldPoint, graph.GetNodePosition(nearest));
            if (distance <= bestDistance)
            {
                best = graph;
                bestDistance = distance;
            }
        }
        return best;
    }

    /// <summary>Nearest public city graph, falling back to the largest city graph.</summary>
    public static AIRoadGraph ResolveCityGraph(Vector3 worldPoint, float maxDistanceMeters = 2500f)
    {
        return FindCityGraphNear(worldPoint, maxDistanceMeters) ?? FindCityGraph();
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

    /// <summary>
    /// Speed limit of one directed link. Falls back to the source node's value
    /// when the graph was authored before per-link limits existed.
    /// </summary>
    public float GetSpeedLimitKmh(int fromNode, int toNode)
    {
        if (!IsValidNode(fromNode)) return 30f;
        RoadNode node = nodes[fromNode];
        int link = FindLinkIndex(node, toNode);
        if (link >= 0 && node.outgoingSpeedLimits != null && link < node.outgoingSpeedLimits.Length)
            return Mathf.Max(10f, node.outgoingSpeedLimits[link]);
        return GetSpeedLimitKmh(fromNode);
    }

    public bool IsRedSignal(int index)
    {
        if (!IsValidNode(index)) return false;
        var tl = nodes[index].trafficLight;
        return tl != null && tl.IsRed();
    }

    /// <summary>
    /// The signal a vehicle on this directed link must obey as it reaches the
    /// junction. Per-approach binding wins; the destination node's signal is the
    /// fallback for a single-signal junction.
    /// </summary>
    public TrafficLight GetTrafficLight(int fromNode, int toNode)
    {
        if (IsValidNode(fromNode))
        {
            RoadNode node = nodes[fromNode];
            int link = FindLinkIndex(node, toNode);
            if (link >= 0 && node.outgoingTrafficLights != null &&
                link < node.outgoingTrafficLights.Length && node.outgoingTrafficLights[link] != null)
                return node.outgoingTrafficLights[link];
        }
        return IsValidNode(toNode) ? nodes[toNode].trafficLight : null;
    }

    public bool IsRedSignal(int fromNode, int toNode)
    {
        var tl = GetTrafficLight(fromNode, toNode);
        return tl != null && tl.IsRed();
    }

    /// <summary>
    /// Binds a scene signal to the approach whose direction it faces. A junction
    /// with one signal keeps the node-wide fallback so every approach still stops;
    /// a junction with several signals gets an independent phase per approach.
    /// </summary>
    public bool TryBindTrafficSignal(TrafficLight signal, float maxDistanceMeters = 25f)
    {
        if (signal == null || nodes == null || nodes.Length == 0) return false;

        int nodeIndex = GetNearestNodeIndex(signal.transform.position);
        if (!IsValidNode(nodeIndex)) return false;
        if (Vector3.Distance(GetNodePosition(nodeIndex), signal.transform.position) >
            Mathf.Max(1f, maxDistanceMeters)) return false;

        return BindTrafficSignalToNode(signal, nodeIndex);
    }

    public bool BindTrafficSignalToNode(TrafficLight signal, int nodeIndex)
    {
        if (signal == null || !IsValidNode(nodeIndex)) return false;

        Vector3 nodePosition = GetNodePosition(nodeIndex);
        Vector3 toSignal = signal.transform.position - nodePosition;
        toSignal.y = 0f;

        // Find every directed link that enters this junction. The signal is on
        // the stop-line side of the junction, so the approach it belongs to has
        // the strongest dot product with the vector from the junction to the signal.
        int bestSource = -1;
        int bestLink = -1;
        float bestDot = -2f;
        for (int source = 0; source < nodes.Length; source++)
        {
            if (source == nodeIndex || !IsValidNode(source)) continue;
            RoadNode candidate = nodes[source];
            int link = FindLinkIndex(candidate, nodeIndex);
            if (link < 0) continue;
            if (candidate.outgoingTrafficLights != null &&
                link < candidate.outgoingTrafficLights.Length &&
                candidate.outgoingTrafficLights[link] != null) continue;

            Vector3 approach = GetNodePosition(source) - nodePosition;
            approach.y = 0f;
            float dot = toSignal.sqrMagnitude > .0001f && approach.sqrMagnitude > .0001f
                ? Vector3.Dot(toSignal.normalized, approach.normalized)
                : 1f;
            if (dot > bestDot)
            {
                bestDot = dot;
                bestSource = source;
                bestLink = link;
            }
        }

        if (bestSource >= 0)
        {
            RoadNode sourceNode = nodes[bestSource];
            EnsureSignalArray(sourceNode);
            sourceNode.outgoingTrafficLights[bestLink] = signal;
        }

        // Keep a junction-wide fallback for any approach without its own signal,
        // and for graphs that only ever author one signal per junction.
        if (nodes[nodeIndex].trafficLight == null)
            nodes[nodeIndex].trafficLight = signal;

        return bestSource >= 0 || nodes[nodeIndex].trafficLight != null;
    }

    /// <summary>
    /// Removes every binding to <paramref name="signal"/>. Used when a junction
    /// turns out not to have a usable phase for both axes: a lamp left bound with
    /// no controller driving it would hold that approach at red forever.
    /// </summary>
    public void UnbindTrafficSignal(TrafficLight signal)
    {
        if (signal == null || nodes == null) return;
        for (int i = 0; i < nodes.Length; i++)
        {
            RoadNode node = nodes[i];
            if (node == null) continue;
            if (node.trafficLight == signal) node.trafficLight = null;
            if (node.outgoingTrafficLights == null) continue;
            for (int slot = 0; slot < node.outgoingTrafficLights.Length; slot++)
                if (node.outgoingTrafficLights[slot] == signal) node.outgoingTrafficLights[slot] = null;
        }
    }

    static void EnsureSignalArray(RoadNode node)
    {
        if (node.nextNodeIndices == null) return;
        int required = node.nextNodeIndices.Length;
        if (node.outgoingTrafficLights != null && node.outgoingTrafficLights.Length >= required) return;

        var expanded = new TrafficLight[required];
        if (node.outgoingTrafficLights != null)
            System.Array.Copy(node.outgoingTrafficLights, expanded, node.outgoingTrafficLights.Length);
        node.outgoingTrafficLights = expanded;
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
        for (int i = 0; i < next.Length; i++)
            if (next[i] == previousNodeIndex && IsValidNode(next[i])) reverse = next[i];

        bool haveIncoming = IsValidNode(previousNodeIndex);
        Vector3 currentPosition = GetNodePosition(currentIndex);
        Vector3 incoming = haveIncoming
            ? currentPosition - GetNodePosition(previousNodeIndex)
            : Vector3.zero;
        incoming.y = 0f;
        if (incoming.sqrMagnitude > .0001f) incoming.Normalize();
        else haveIncoming = false;

        // Dot of an eligible edge with the incoming direction; -2 marks an edge
        // that must not be picked (the node just left or an invalid node).
        float Dot(int candidate)
        {
            if (!IsValidNode(candidate) || candidate == currentIndex || candidate == previousNodeIndex) return -2f;
            if (!haveIncoming) return 1f;
            Vector3 outgoing = GetNodePosition(candidate) - currentPosition;
            outgoing.y = 0f;
            if (outgoing.sqrMagnitude <= .0001f) return 1f;
            return Mathf.Clamp(Vector3.Dot(incoming, outgoing.normalized), -1f, 1f);
        }

        float Weight(float dot) => Mathf.Lerp(.08f, 1f, (dot + 1f) * .5f);

        int Pick(float minDot)
        {
            float totalWeight = 0f;
            for (int i = 0; i < next.Length; i++)
            {
                float dot = Dot(next[i]);
                if (dot < minDot) continue;
                totalWeight += Weight(dot);
            }
            if (totalWeight <= 0f) return -1;

            float roll = Random.Range(0f, totalWeight);
            float accumulated = 0f;
            for (int i = 0; i < next.Length; i++)
            {
                float dot = Dot(next[i]);
                if (dot < minDot) continue;
                accumulated += Weight(dot);
                if (roll <= accumulated) return next[i];
            }
            return -1;
        }

        // Prefer edges that carry the car forward. An edge pointing back down the
        // road reverses the directed link, and the lane offset is laid out relative
        // to that link: the offset mirrors onto the opposite carriageway while the
        // body still faces its old way, so the car slides across the centre line.
        // A reversal is kept only when the junction genuinely offers nothing else
        // (a cul-de-sac), where `reverse` is the way out.
        bool hasForward = false;
        for (int i = 0; i < next.Length; i++)
        {
            float dot = Dot(next[i]);
            if (dot >= -.2f) { hasForward = true; break; }
        }

        int chosen = hasForward ? Pick(-.2f) : Pick(-1.5f);
        if (chosen < 0) chosen = reverse;
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

    /// <summary>
    /// Node-level fallback used only where no directed link is known. Directed
    /// placement always goes through <see cref="TrafficLane"/>; this keeps the
    /// single-carriageway centring convention for callers that have one node.
    /// </summary>
    public float GetLaneOffset(int nodeIndex, int laneIndex)
    {
        if (!IsValidNode(nodeIndex)) return 0f;
        int lanes = Mathf.Max(1, nodes[nodeIndex].laneCount);
        laneIndex = Mathf.Clamp(laneIndex, 0, lanes - 1);
        float width = Mathf.Max(MinimumLaneWidth, nodes[nodeIndex].laneWidth);

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
    /// Angle of the most likely legal continuation at a junction, in degrees.
    /// The next-node picker weights straight-ahead exits above turns, so this
    /// gives the controller a useful pre-junction speed signal without slowing
    /// every car for a side street it is not going to take. A dead end returns
    /// 180 degrees so the approach is treated as a slow turnaround.
    /// </summary>
    public float GetLikelyTurnAngle(int nodeIndex, Vector3 incomingDirection, int previousNodeIndex = -1)
    {
        if (!IsValidNode(nodeIndex)) return 0f;
        incomingDirection.y = 0f;
        if (incomingDirection.sqrMagnitude < .0001f) return 0f;

        var node = nodes[nodeIndex];
        if (node.nextNodeIndices == null || node.nextNodeIndices.Length == 0)
            return 180f;

        Vector3 current = GetNodePosition(nodeIndex);
        float shallowest = float.MaxValue;
        for (int i = 0; i < node.nextNodeIndices.Length; i++)
        {
            int candidate = node.nextNodeIndices[i];
            if (!IsValidNode(candidate) || candidate == nodeIndex || candidate == previousNodeIndex)
                continue;

            Vector3 outgoing = GetNodePosition(candidate) - current;
            outgoing.y = 0f;
            if (outgoing.sqrMagnitude < .0001f) continue;
            float angle = Vector3.Angle(incomingDirection, outgoing);
            if (angle < shallowest) shallowest = angle;
        }

        return shallowest < float.MaxValue ? shallowest : 180f;
    }

    /// <summary>
    /// Smallest lane the traffic geometry will place a vehicle in. OSM lane tags are
    /// advisory and a narrow residential street is still driven down its middle by a
    /// 2 m wide car, so the offset never collapses below this.
    /// </summary>
    public const float MinLaneWidth = 2.9f;

    /// <summary>The resolved spacing of one directed link, including the model-fit floor.</summary>
    public float GetLaneWidth(int fromNode, int toNode)
    {
        if (!IsValidNode(fromNode) || !IsValidNode(toNode)) return MinimumLaneWidth;
        RoadNode node = nodes[fromNode];
        int link = FindLinkIndex(node, toNode);
        float width = link >= 0 && node.outgoingLaneWidths != null && link < node.outgoingLaneWidths.Length
            ? node.outgoingLaneWidths[link] : node.laneWidth;
        return Mathf.Max(MinimumLaneWidth, width);
    }

    /// <summary>
    /// Half-width of the asphalt available to this directed link. OSM's
    /// asymmetric lane counts are rendered with the wider side's envelope on
    /// both sides, so the opposite direction's count is part of the fit.
    /// Controllers use this as a hard rail when a collision or a sharp turn
    /// leaves a body wide of its lane line.
    /// </summary>
    public float GetRoadHalfWidth(int fromNode, int toNode)
    {
        if (!IsValidNode(fromNode) || !IsValidNode(toNode)) return 0f;
        float laneWidth = GetLaneWidth(fromNode, toNode);
        int ownLanes = GetLaneCount(fromNode, toNode);
        if (IsSharedLane(fromNode, toNode))
            return laneWidth * .5f;
        if (IsOneWay(fromNode, toNode))
            return ownLanes * laneWidth * .5f;

        int oppositeLanes = IsValidNode(toNode) ? GetLaneCount(toNode, fromNode) : ownLanes;
        int sideLanes = Mathf.Max(1, Mathf.Max(ownLanes, oppositeLanes));
        return sideLanes * laneWidth;
    }

    public bool IsSharedLane(int fromNode, int toNode)
    {
        if (!IsValidNode(fromNode)) return false;
        var node = nodes[fromNode];
        int link = FindLinkIndex(node, toNode);
        return link >= 0 && node.outgoingSharedLanes != null &&
               link < node.outgoingSharedLanes.Length && node.outgoingSharedLanes[link];
    }

    static int FindLinkIndex(RoadNode node, int toNode)
    {
        if (node == null || node.nextNodeIndices == null) return -1;
        for (int i = 0; i < node.nextNodeIndices.Length; i++)
            if (node.nextNodeIndices[i] == toNode) return i;
        return -1;
    }

    /// <summary>Signed lateral offset of one lane line from the OSM centreline.</summary>
    public float GetLaneOffset(int fromNode, int toNode, int laneIndex)
    {
        // Delegated so there is exactly one lane-geometry implementation in the
        // project. Controllers, spawn placement, stops, queues and probes all read
        // the same numbers from the same function.
        return TrafficLane.Offset(this, fromNode, toNode, laneIndex);
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
