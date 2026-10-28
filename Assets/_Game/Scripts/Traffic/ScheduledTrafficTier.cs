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

    void Awake() { ResolveComponents(); }

    void OnEnable()
    {
        ResolveComponents();
        if (full != null) full.enabled = true;
        if (spline != null) spline.enabled = false;
        if (body != null)
        {
            body.isKinematic = false;
            // Physics interpolation is what keeps a body driven by fixed steps from
            // visibly stuttering between frames. It must be off while the body is
            // kinematic and back on the moment it is promoted.
            body.interpolation = RigidbodyInterpolation.Interpolate;
            body.detectCollisions = true;
            body.constraints |= RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }
        nextUpdate = 0f;
    }

    void ResolveComponents()
    {
        if (full == null) full = GetComponent<AIVehicleController>();
        if (spline == null) spline = GetComponent<SplineVehicle>();
        if (body == null) body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ResolveComponents();
        if (full == null || spline == null) return;
        if (Time.time < nextUpdate) return;
        nextUpdate = Time.time + 0.2f;
        if (player == null)
        {
            // Prefer the mission's bus: it is the body the player actually drives,
            // and it stays valid after a runtime prefab swap. FindAnyObjectByType
            // can return a stale scene bus that SceneBootstrap disabled.
            var mission = MissionManager.Instance;
            var bus = mission != null ? mission.busController : null;
            if (bus == null) bus = FindAnyObjectByType<BusController>();
            if (bus != null) player = bus.transform;
        }
        if (player == null) return;
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance > 400f) { GetComponent<AIBusRouteRunner>()?.Despawn(); return; }
        bool wantFull = distance <= (full.enabled ? 225f : 200f) && (full.enabled || AIVehicleController.ActiveCount < 20);
        if (wantFull == full.enabled) return;
        if (wantFull)
        {
            if (body != null)
            {
                body.isKinematic = false;
                body.interpolation = RigidbodyInterpolation.Interpolate;
                body.detectCollisions = true;
                body.angularVelocity = Vector3.zero;
            }
            full.SetRuntimeState(spline.roadGraph, spline.currentNodeIndex, spline.targetNodeIndex,
                spline.laneIndex, full.vehicleType, Mathf.Max(0f, spline.speedKmh), spline.previousNodeIndex);
            if (body != null) body.linearVelocity = full.GetVelocityWorld();
            spline.enabled = false;
            full.enabled = true;
        }
        else
        {
            spline.Init(full.roadGraph, full.currentNodeIndex, full.targetNodeIndex, full.laneIndex,
                Mathf.Max(0f, full.currentSpeedKmh), full.previousNodeIndex);
            full.enabled = false;
            if (body != null)
            {
                if (!body.isKinematic) { body.linearVelocity = Vector3.zero; body.angularVelocity = Vector3.zero; }
                body.isKinematic = true;
                body.interpolation = RigidbodyInterpolation.None;
                body.detectCollisions = false;
            }
            spline.enabled = true;
        }
    }
}
