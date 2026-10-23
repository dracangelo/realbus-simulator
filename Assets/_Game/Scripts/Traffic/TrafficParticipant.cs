using System.Collections.Generic;
using UnityEngine;

/// <summary>Shared perception across physics traffic, spline traffic, and the player bus.</summary>
public class TrafficParticipant : MonoBehaviour
{
    static readonly List<TrafficParticipant> active = new List<TrafficParticipant>();
    AIVehicleController ai;
    SplineVehicle spline;
    BusController bus;
    public float length = 4.2f;
    /// <summary>Body width in metres. VehiclePool writes the measured prefab width so following and passing checks use the real model, not a car-sized assumption.</summary>
    public float width = 2f;
    public float SpeedKmh => bus != null ? bus.currentSpeedKmh : ai != null && ai.enabled ? ai.currentSpeedKmh : spline != null ? spline.speedKmh : 0f;
    void Awake() { ai = GetComponent<AIVehicleController>(); spline = GetComponent<SplineVehicle>(); bus = GetComponent<BusController>(); if (bus != null) { length = 12f; width = 2.5f; } }
    void OnEnable() { if (!active.Contains(this)) active.Add(this); }
    void OnDisable() { active.Remove(this); }
    static float WidthOf(TrafficParticipant participant)
    {
        return participant != null ? Mathf.Max(1.2f, participant.width) : 2f;
    }

    static float LateralClearance(TrafficParticipant self, TrafficParticipant other)
    {
        // Half of each body plus a small mirror gap. This is what keeps a
        // following car behind a wide bus instead of trying to share its lane,
        // while still ignoring a vehicle in the next lane when the lane
        // spacing has been expanded for these exact models.
        return (WidthOf(self) + WidthOf(other)) * 0.5f + 0.35f;
    }

    public static float LimitSpeed(Transform vehicle, float desired, float clearance, float brake,
        Transform ignore = null)
    {
        TrafficParticipant self = vehicle != null ? vehicle.GetComponent<TrafficParticipant>() : null;
        float selfHalfLength = self != null ? Mathf.Max(1f, self.length) * .5f : 2.1f;
        foreach (var other in active)
        {
            if (other == null || other.transform == vehicle) continue;
            if (ignore != null && other.transform == ignore) continue;
            Vector3 offset = other.transform.position - vehicle.position;
            float ahead = Vector3.Dot(vehicle.forward, offset);
            if (ahead <= 0f || Mathf.Abs(offset.y) > 3f) continue;
            if (Mathf.Abs(Vector3.Dot(vehicle.right, offset)) > LateralClearance(self, other)) continue;
            // Keep a real bumper gap. Subtracting both half lengths stops a long
            // bus from being followed as if it were a 4 m car and is what makes
            // overtaking a bus start far enough back to complete in the lane.
            // The leader's speed is part of the limit now: a car behind a bus
            // doing 50 km/h should be able to keep up, not brake as though the
            // bus were parked.
            float gap = ahead - selfHalfLength - other.length * .5f;
            bool oncoming = Vector3.Dot(vehicle.forward, other.transform.forward) < -0.25f;
            desired = Mathf.Min(desired, oncoming
                ? RealismRules.StoppingSpeedKmh(gap, clearance, brake)
                : RealismRules.SafeFollowingSpeedKmh(desired, other.SpeedKmh, gap, clearance, brake));
        }
        return desired;
    }

    /// <summary>
    /// Finds the nearest traffic body in this vehicle's own lane within
    /// <paramref name="maxDistance"/>, with the bumper-to-bumper gap. Overtaking
    /// decisions need both the leader and its speed; <see cref="LimitSpeed"/> only
    /// needs to clamp speed.
    /// </summary>
    public static bool TryFindAhead(Transform vehicle, float maxDistance, float halfLaneWidth,
        out TrafficParticipant leader, out float gap)
    {
        leader = null;
        gap = 0f;
        if (vehicle == null) return false;

        TrafficParticipant self = vehicle.GetComponent<TrafficParticipant>();
        float selfHalfLength = self != null ? Mathf.Max(1f, self.length) * .5f : 2.1f;
        float best = float.MaxValue;
        float distance = Mathf.Max(1f, maxDistance);
        float halfWidth = Mathf.Max(.6f, halfLaneWidth);
        foreach (var other in active)
        {
            if (other == null || other.transform == vehicle) continue;
            Vector3 offset = other.transform.position - vehicle.position;
            float ahead = Vector3.Dot(vehicle.forward, offset);
            if (ahead <= 0f || ahead > distance || Mathf.Abs(offset.y) > 3f) continue;
            if (Mathf.Abs(Vector3.Dot(vehicle.right, offset)) > halfWidth) continue;
            if (ahead < best)
            {
                best = ahead;
                leader = other;
                gap = Mathf.Max(0f, ahead - selfHalfLength - other.length * .5f);
            }
        }
        return leader != null;
    }
    public static Transform StoppedBusAhead(Transform vehicle, float distance)
    {
        TrafficParticipant self = vehicle != null ? vehicle.GetComponent<TrafficParticipant>() : null;
        foreach (var other in active)
        {
            if (other == null || other.transform == vehicle || other.SpeedKmh >= 2f) continue;
            if (other.bus == null && (other.ai == null || other.ai.vehicleType != AIVehicleController.VehicleType.Bus)) continue;
            Vector3 offset = other.transform.position - vehicle.position;
            float ahead = Vector3.Dot(vehicle.forward, offset);
            if (ahead > 0f && ahead < distance &&
                Mathf.Abs(Vector3.Dot(vehicle.right, offset)) < LateralClearance(self, other)) return other.transform;
        }
        return null;
    }

    /// <summary>
    /// True once a passing body is far enough across that the vehicle it is passing
    /// no longer blocks its own lane. The full and spline controllers use this to
    /// keep following the target during the first part of the lane change, then
    /// release it for the pass. Ignoring the target immediately let a car accelerate
    /// into a stopped leader before its body had actually cleared the lane.
    /// </summary>
    public static bool IsLateralClear(Transform vehicle, Transform other, float extra = .35f)
    {
        if (vehicle == null || other == null) return true;
        var self = vehicle.GetComponent<TrafficParticipant>();
        var target = other.GetComponent<TrafficParticipant>();
        float tolerance = Mathf.Max(2.8f,
            (WidthOf(self) + WidthOf(target)) * .5f + Mathf.Max(0f, extra));
        Vector3 offset = other.position - vehicle.position;
        return Mathf.Abs(Vector3.Dot(vehicle.right, offset)) >= tolerance;
    }

    public static bool PassingLaneClear(Transform vehicle, float sideOffset, float length = 35f)
    {
        TrafficParticipant self = vehicle != null ? vehicle.GetComponent<TrafficParticipant>() : null;
        foreach (var other in active)
        {
            if (other == null || other.transform == vehicle) continue;
            float tolerance = Mathf.Max(2.3f, (WidthOf(self) + WidthOf(other)) * 0.5f + .35f);
            Vector3 offset = other.transform.position - vehicle.position;
            if (Mathf.Abs(Vector3.Dot(vehicle.forward, offset)) < length &&
                Mathf.Abs(Vector3.Dot(vehicle.right, offset) - sideOffset) < tolerance) return false;
        }
        return true;
    }

    public static bool HasVehicleAhead(Transform vehicle, float distance, float halfLaneWidth = 2.3f)
    {
        foreach (var other in active)
        {
            if (other == null || other.transform == vehicle) continue;
            Vector3 offset = other.transform.position - vehicle.position;
            float ahead = Vector3.Dot(vehicle.forward, offset);
            if (ahead > 0f && ahead < distance && Mathf.Abs(Vector3.Dot(vehicle.right, offset)) < halfLaneWidth) return true;
        }
        return false;
    }

    /// <summary>
    /// True when no active traffic body is within <paramref name="radius"/> of a point.
    /// Used before a spawn so a new vehicle cannot materialise on top of an existing
    /// one; the check is planar because road bodies sit at slightly different heights.
    /// </summary>
    public static bool IsSpawnAreaClear(Vector3 point, float radius)
    {
        float sqr = Mathf.Max(0f, radius) * Mathf.Max(0f, radius);
        foreach (var other in active)
        {
            if (other == null) continue;
            Vector3 offset = other.transform.position - point;
            offset.y = 0f;
            if (offset.sqrMagnitude < sqr) return false;
        }
        return true;
    }
}
