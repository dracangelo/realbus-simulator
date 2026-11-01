using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Places signal heads at the junctions of a city road graph and wires them to
/// the right of way the traffic system reads.
///
/// The graph rebuild that runs when a city loads used to clear every node's
/// <c>trafficLight</c> and nothing replaced it, so imported signal models could
/// exist in the project while no live junction ever showed a phase. This service
/// is the step that closes that gap: it walks the rebuilt graph, finds the nodes
/// where three or more roads meet, builds one head per approach at the stop line,
/// binds each head to its own directed link, and gives the junction a single
/// controller so crossing approaches can never be released together.
///
/// The heads are children of the graph, so the next rebuild removes them with the
/// rest of the graph and cannot leave duplicates behind.
/// </summary>
public static class CityTrafficSignals
{
    public const string RootName = "CityTrafficSignals";

    /// <summary>Enough signalled junctions to read as a city without paying for the whole network on a phone.</summary>
    public const int DefaultMaxJunctions = 10;

    /// <summary>How close a junction must be to the driven route to be signalled.</summary>
    public const float DefaultFocusRadiusMeters = 120f;

    /// <summary>Keeps two signalled junctions from landing on the same block.</summary>
    public const float DefaultMinSpacingMeters = 90f;

    /// <summary>Distance from the junction centre to the stop line, where the head stands.</summary>
    public const float StopLineMeters = 6f;

    /// <summary>Sideways offset of the pole from the stop line: the driver's left by default.</summary>
    public const float KerbOffsetMeters = 4f;

    public const int MinApproachesForSignal = 3;

    /// <summary>
    /// Replaces this graph's signals with a fresh set near <paramref name="route"/>.
    /// Returns the number of signal heads created. Safe to call on every rebuild.
    /// </summary>
    public static int Rebuild(AIRoadGraph graph, BusRoute route, bool importedHousing = true,
        int maxJunctions = DefaultMaxJunctions, float focusRadiusMeters = DefaultFocusRadiusMeters,
        float minSpacingMeters = DefaultMinSpacingMeters, int maxImportedHousingJunctions = 1)
    {
        if (graph == null) return 0;

        Clear(graph);
        var focus = CollectRouteSamples(route);
        var junctions = FindJunctions(graph, focus, maxJunctions, focusRadiusMeters, minSpacingMeters);
        return BuildAt(graph, junctions, importedHousing, maxImportedHousingJunctions);
    }

    /// <summary>Removes every signal this service built for <paramref name="graph"/>.</summary>
    public static void Clear(AIRoadGraph graph)
    {
        if (graph == null) return;
        for (int i = graph.transform.childCount - 1; i >= 0; i--)
        {
            var child = graph.transform.GetChild(i);
            if (child == null || child.name != RootName) continue;
            // Destroy is deferred in play mode, so the old root would still be a
            // child when the new set is built and GetOrCreateRoot would adopt it -
            // the fresh heads would then be destroyed with the old hierarchy.
            // Detaching first keeps the rebuild independent of destroy timing.
            child.SetParent(null, false);
            DestroyNow(child.gameObject);
        }
    }

    /// <summary>
    /// Builds a signal set for explicit junction node indices. The junction must
    /// have approaches on both axes; a junction with traffic only along one axis
    /// gets no controller, because there is no conflicting movement to hold.
    /// </summary>
    public static int BuildAt(AIRoadGraph graph, IList<int> junctions, bool importedHousing = true,
        int maxImportedHousingJunctions = 1)
    {
        if (graph == null || junctions == null || junctions.Count == 0) return 0;

        Transform root = GetOrCreateRoot(graph);
        int created = 0;
        int importedJunctions = 0;

        for (int j = 0; j < junctions.Count; j++)
        {
            int junction = junctions[j];
            if (!graph.IsValidNode(junction)) continue;

            // The imported retro housing is a 174k-triangle hero prop. Using it on
            // every head would put millions of triangles into a phone frame, so it
            // is reserved for the first junction; the rest use the cheap
            // procedural head that carries the same three-lens signal state.
            bool useImportedHousing = importedHousing && importedJunctions < Mathf.Max(0, maxImportedHousingJunctions);
            Vector3 junctionPosition = graph.GetNodePosition(junction);
            var approaches = CollectApproaches(graph, junction);
            if (approaches.Count < MinApproachesForSignal) continue;

            // A junction that already carries a signal on any approach is left
            // alone: half a set driven by a new controller would show a phase to
            // traffic the controller does not know about.
            bool alreadySignalled = false;
            for (int i = 0; i < approaches.Count; i++)
            {
                if (graph.GetTrafficLight(approaches[i].source, junction) != null)
                {
                    alreadySignalled = true;
                    break;
                }
            }
            if (alreadySignalled) continue;

            bool hasFirstAxis = false, hasSecondAxis = false;
            for (int i = 0; i < approaches.Count; i++)
            {
                if (approaches[i].axis == 0) hasFirstAxis = true;
                else hasSecondAxis = true;
            }
            if (!hasFirstAxis || !hasSecondAxis) continue;

            var firstAxis = new List<TrafficLight>();
            var secondAxis = new List<TrafficLight>();
            float lateral = graph.drivesOnLeft ? KerbOffsetMeters : -KerbOffsetMeters;

            for (int i = 0; i < approaches.Count; i++)
            {
                var approach = approaches[i];
                Vector3 stopLine = junctionPosition - approach.travel * StopLineMeters;
                stopLine.y = Mathf.Max(.1f, junctionPosition.y);

                // The head faces the traffic arriving at the junction, so its
                // forward axis points back down the approach.
                var rotation = Quaternion.LookRotation(-approach.travel, Vector3.up);
                var light = OsmTrafficSignalSpawner.CreateSignal(root, stopLine, rotation, 0f, lateral, useImportedHousing);
                if (light == null) continue;

                // Set before Start runs so the lamp never advances a private cycle
                // that the controller would then overwrite.
                light.externallyControlled = true;
                if (!graph.BindTrafficSignalToNode(light, junction))
                {
                    DestroyNow(light.gameObject);
                    continue;
                }

                if (approach.axis == 0) firstAxis.Add(light);
                else secondAxis.Add(light);
                created++;
            }

            if (firstAxis.Count == 0 || secondAxis.Count == 0)
            {
                // Without a lamp on both axes there is no phase to apply. Binding
                // one side anyway would leave it held at red by a lamp no
                // controller advances, so the whole set is withdrawn.
                for (int i = 0; i < firstAxis.Count; i++) Withdraw(graph, firstAxis[i]);
                for (int i = 0; i < secondAxis.Count; i++) Withdraw(graph, secondAxis[i]);
                created -= firstAxis.Count + secondAxis.Count;
                continue;
            }

            var junctionObject = new GameObject($"Junction {junction:0000}");
            junctionObject.transform.SetParent(root, false);
            junctionObject.transform.position = junctionPosition;

            var controller = junctionObject.AddComponent<TrafficSignalJunction>();
            // Staggered per junction so a straight run of signals does not show
            // the same phase at once, which reads as broken lights.
            controller.Configure(firstAxis, secondAxis, (junction % 4) * 3f);
            if (useImportedHousing) importedJunctions++;
        }

        return created;
    }

    /// <summary>
    /// Nodes where at least three roads meet, near the route where a route is
    /// known. Results are deterministic: nearest to the route first, then node
    /// index, with a minimum spacing so one block is not covered in signals.
    /// </summary>
    public static List<int> FindJunctions(AIRoadGraph graph, IList<Vector3> focusPoints, int maxJunctions,
        float focusRadiusMeters = DefaultFocusRadiusMeters, float minSpacingMeters = DefaultMinSpacingMeters)
    {
        var result = new List<int>();
        if (graph == null || graph.nodes == null || maxJunctions <= 0) return result;

        var candidates = new List<KeyValuePair<float, int>>();
        for (int i = 0; i < graph.nodes.Length; i++)
        {
            if (!graph.IsValidNode(i)) continue;
            if (CountNeighbours(graph.nodes[i]) < MinApproachesForSignal) continue;

            float distance;
            if (focusPoints != null && focusPoints.Count > 0)
            {
                distance = NearestDistance(graph.GetNodePosition(i), focusPoints, focusRadiusMeters, out bool inRange);
                if (!inRange) continue;
            }
            else
            {
                distance = 0f;
            }

            candidates.Add(new KeyValuePair<float, int>(distance, i));
        }

        candidates.Sort((a, b) =>
        {
            int byDistance = a.Key.CompareTo(b.Key);
            return byDistance != 0 ? byDistance : a.Value.CompareTo(b.Value);
        });

        float spacing = Mathf.Max(0f, minSpacingMeters);
        for (int c = 0; c < candidates.Count && result.Count < maxJunctions; c++)
        {
            int index = candidates[c].Value;
            Vector3 position = graph.GetNodePosition(index);

            bool tooClose = false;
            for (int r = 0; r < result.Count; r++)
            {
                if (Vector3.Distance(position, graph.GetNodePosition(result[r])) < spacing)
                {
                    tooClose = true;
                    break;
                }
            }
            if (!tooClose) result.Add(index);
        }

        return result;
    }

    /// <summary>Route points sampled roughly every <paramref name="spacingMeters"/>.</summary>
    public static List<Vector3> CollectRouteSamples(BusRoute route, float spacingMeters = 60f)
    {
        var samples = new List<Vector3>();
        if (route == null) return samples;

        route.EnsureWorldPathPoints();
        var path = route.pathPoints;
        if (path == null || path.Length == 0) return samples;

        float spacing = Mathf.Max(1f, spacingMeters);
        Vector3 last = path[0];
        samples.Add(last);
        for (int i = 1; i < path.Length; i++)
        {
            if (Vector3.Distance(last, path[i]) < spacing && i != path.Length - 1) continue;
            last = path[i];
            samples.Add(last);
        }
        return samples;
    }

    struct Approach
    {
        public int source;
        public int axis;
        public Vector3 travel;
    }

    static List<Approach> CollectApproaches(AIRoadGraph graph, int junction)
    {
        var approaches = new List<Approach>();
        if (graph == null || graph.nodes == null) return approaches;

        Vector3 junctionPosition = graph.GetNodePosition(junction);
        for (int source = 0; source < graph.nodes.Length; source++)
        {
            if (source == junction || !graph.IsValidNode(source)) continue;
            var links = graph.nodes[source].nextNodeIndices;
            if (links == null) continue;

            bool leadsHere = false;
            for (int link = 0; link < links.Length; link++)
                if (links[link] == junction) { leadsHere = true; break; }
            if (!leadsHere) continue;

            Vector3 travel = junctionPosition - graph.GetNodePosition(source);
            travel.y = 0f;
            if (travel.sqrMagnitude < .01f) continue;
            travel.Normalize();

            approaches.Add(new Approach
            {
                source = source,
                axis = TrafficSignalJunction.AxisForHeading(travel),
                travel = travel
            });
        }

        // Two approaches from the same direction (a duplicated directed link)
        // would put two heads on one stop line.
        approaches.Sort((a, b) =>
        {
            int byAxis = a.axis.CompareTo(b.axis);
            return byAxis != 0 ? byAxis : a.source.CompareTo(b.source);
        });
        return approaches;
    }

    static int CountNeighbours(AIRoadGraph.RoadNode node)
    {
        if (node == null || node.nextNodeIndices == null) return 0;
        var seen = new HashSet<int>();
        for (int i = 0; i < node.nextNodeIndices.Length; i++)
        {
            int next = node.nextNodeIndices[i];
            if (next >= 0) seen.Add(next);
        }
        return seen.Count;
    }

    static float NearestDistance(Vector3 point, IList<Vector3> focusPoints, float radius, out bool inRange)
    {
        float best = float.MaxValue;
        for (int i = 0; i < focusPoints.Count; i++)
        {
            float distance = Vector3.Distance(point, focusPoints[i]);
            if (distance < best) best = distance;
        }
        inRange = best <= Mathf.Max(0f, radius);
        return best;
    }

    static void Withdraw(AIRoadGraph graph, TrafficLight light)
    {
        if (light == null) return;
        graph.UnbindTrafficSignal(light);
        DestroyNow(light.gameObject);
    }

    static Transform GetOrCreateRoot(AIRoadGraph graph)
    {
        for (int i = 0; i < graph.transform.childCount; i++)
        {
            var child = graph.transform.GetChild(i);
            if (child != null && child.name == RootName) return child;
        }

        var root = new GameObject(RootName);
        root.transform.SetParent(graph.transform, false);
        return root.transform;
    }

    static void DestroyNow(Object target)
    {
        if (target == null) return;
        if (Application.isPlaying) Object.Destroy(target);
        else Object.DestroyImmediate(target);
    }
}
