using UnityEngine;

/// <summary>Scheduled traffic shares the scene's 20-body cap and 200/400 metre tiers.</summary>
[RequireComponent(typeof(AIVehicleController), typeof(SplineVehicle))]
public class ScheduledTrafficTier : MonoBehaviour
{
    AIVehicleController full;
    SplineVehicle spline;
    Rigidbody body;
    Transform player;
    float nextUpdate;
    void Awake() { full = GetComponent<AIVehicleController>(); spline = GetComponent<SplineVehicle>(); body = GetComponent<Rigidbody>(); }
    void OnEnable()
    {
        full.enabled = true; spline.enabled = false;
        body.isKinematic = false; body.detectCollisions = true;
        body.linearVelocity = Vector3.zero; body.angularVelocity = Vector3.zero;
        nextUpdate = 0f;
    }
    void Update()
    {
        if (Time.time < nextUpdate) return;
        nextUpdate = Time.time + 0.2f;
        if (player == null) { var bus = FindFirstObjectByType<BusController>(); if (bus != null) player = bus.transform; }
        if (player == null) return;
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance > 400f) { GetComponent<AIBusRouteRunner>()?.Despawn(); return; }
        bool wantFull = distance <= (full.enabled ? 225f : 200f) && (full.enabled || AIVehicleController.ActiveCount < 20);
        if (wantFull == full.enabled) return;
        if (wantFull)
        {
            body.isKinematic = false; body.detectCollisions = true;
            full.SetRuntimeState(spline.roadGraph, spline.currentNodeIndex, spline.targetNodeIndex, spline.laneIndex, full.vehicleType, spline.speedKmh);
            spline.enabled = false; full.enabled = true;
        }
        else
        {
            spline.Init(full.roadGraph, full.currentNodeIndex, full.targetNodeIndex, full.laneIndex, full.currentSpeedKmh);
            full.enabled = false; body.linearVelocity = Vector3.zero; body.angularVelocity = Vector3.zero;
            body.isKinematic = true; body.detectCollisions = false; spline.enabled = true;
        }
    }
}
