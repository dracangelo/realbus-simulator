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
    public float SpeedKmh => bus != null ? bus.currentSpeedKmh : ai != null && ai.enabled ? ai.currentSpeedKmh : spline != null ? spline.speedKmh : 0f;
    void Awake() { ai = GetComponent<AIVehicleController>(); spline = GetComponent<SplineVehicle>(); bus = GetComponent<BusController>(); if (bus != null) length = 12f; }
    void OnEnable() { if (!active.Contains(this)) active.Add(this); }
    void OnDisable() { active.Remove(this); }
    public static float LimitSpeed(Transform vehicle, float desired, float clearance, float brake)
    {
        foreach (var other in active)
        {
            if (other == null || other.transform == vehicle) continue;
            Vector3 offset = other.transform.position - vehicle.position;
            float ahead = Vector3.Dot(vehicle.forward, offset);
            if (ahead <= 0f || Mathf.Abs(offset.y) > 3f || Mathf.Abs(Vector3.Dot(vehicle.right, offset)) > 2.1f) continue;
            float gap = ahead - other.length * 0.5f;
            desired = Mathf.Min(desired, RealismRules.StoppingSpeedKmh(gap, clearance, brake));
        }
        return desired;
    }
    public static Transform StoppedBusAhead(Transform vehicle, float distance)
    {
        foreach (var other in active)
        {
            if (other == null || other.transform == vehicle || other.SpeedKmh >= 2f) continue;
            if (other.bus == null && (other.ai == null || other.ai.vehicleType != AIVehicleController.VehicleType.Bus)) continue;
            Vector3 offset = other.transform.position - vehicle.position;
            float ahead = Vector3.Dot(vehicle.forward, offset);
            if (ahead > 0f && ahead < distance && Mathf.Abs(Vector3.Dot(vehicle.right, offset)) < 2f) return other.transform;
        }
        return null;
    }

    public static bool PassingLaneClear(Transform vehicle, float sideOffset, float length = 35f)
    {
        foreach (var other in active)
        {
            if (other == null || other.transform == vehicle) continue;
            Vector3 offset = other.transform.position - vehicle.position;
            if (Mathf.Abs(Vector3.Dot(vehicle.forward, offset)) < length &&
                Mathf.Abs(Vector3.Dot(vehicle.right, offset) - sideOffset) < 2.3f) return false;
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
}
