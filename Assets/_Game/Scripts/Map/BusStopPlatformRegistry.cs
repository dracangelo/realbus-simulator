using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The pavement anchor for each runtime stop, chosen when the stop prop is
/// placed. Passenger visuals read it so waiting people stand on the kerb the bus
/// actually serves instead of at the road-centre GPS point.
///
/// The registry is static because the stop spawner and the passenger manager are
/// separate runtime objects that never hold a reference to each other. It is
/// cleared at the start of every stop-spawn pass, so a route change cannot leave
/// a passenger queue on the previous route's pavement.
/// </summary>
public static class BusStopPlatformRegistry
{
    public struct Anchor
    {
        /// <summary>Centre of the waiting area, already on the pavement.</summary>
        public Vector3 position;
        /// <summary>Unit vector from the carriageway toward the pavement.</summary>
        public Vector3 outward;
        /// <summary>Unit vector along the kerb, in the direction the bus travels.</summary>
        public Vector3 along;
    }

    static readonly Dictionary<int, Anchor> anchors = new Dictionary<int, Anchor>();

    public static int Count => anchors.Count;

    public static void Clear()
    {
        anchors.Clear();
    }

    public static void Set(int stopIndex, Vector3 position, Vector3 outward, Vector3 along)
    {
        Vector3 flatOutward = Vector3.ProjectOnPlane(outward, Vector3.up);
        Vector3 flatAlong = Vector3.ProjectOnPlane(along, Vector3.up);
        if (flatOutward.sqrMagnitude < 0.0001f)
            flatOutward = Vector3.Cross(Vector3.up, flatAlong.sqrMagnitude > 0.0001f ? flatAlong : Vector3.forward);
        if (flatAlong.sqrMagnitude < 0.0001f)
            flatAlong = Vector3.Cross(flatOutward, Vector3.up);

        anchors[stopIndex] = new Anchor
        {
            position = position,
            outward = flatOutward.normalized,
            along = flatAlong.normalized
        };
    }

    public static bool TryGet(int stopIndex, out Anchor anchor)
    {
        return anchors.TryGetValue(stopIndex, out anchor);
    }

    /// <summary>
    /// Where the <paramref name="index"/>-th waiting passenger stands. Everyone is
    /// placed on the pavement side of the anchor and spread along the kerb, so a
    /// queue can never spill into a traffic lane even when the stop's road runs at
    /// an arbitrary angle to the world axes.
    /// </summary>
    public static Vector3 LayoutSlot(Vector3 anchor, Vector3 outward, Vector3 along, int index)
    {
        if (outward.sqrMagnitude < 0.0001f)
            outward = Vector3.Cross(Vector3.up, along.sqrMagnitude > 0.0001f ? along : Vector3.forward);
        if (along.sqrMagnitude < 0.0001f)
            along = Vector3.Cross(outward, Vector3.up);

        outward = outward.normalized;
        along = along.normalized;

        // Five abreast along the kerb, then rows deeper onto the pavement. The
        // anchor itself is already ~1.4 m beyond the kerb; three rows keep the
        // deepest passenger ~2.6 m from the road edge, still under the shelter.
        int row = Mathf.Clamp(index / 5, 0, 2);
        int column = index % 5;
        return anchor + along * ((column - 2) * 0.55f) + outward * (row * 0.6f);
    }
}
