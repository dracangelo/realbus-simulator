using UnityEngine;

/// <summary>
/// Orders streaming work by what the player can actually see. Distance alone
/// spends the first frames on the road behind the bus, which is why a district
/// could still be bare through the windscreen while its back lots were already
/// built. The score is only an ordering hint: every candidate still loads, it
/// just loads sooner when it is inside the camera cone.
/// </summary>
public static class ViewPriority
{
    public const float DefaultFovDegrees = 72f;

    /// <summary>Uses the driving camera when it is close to the focus, otherwise the vehicle.</summary>
    public static Vector3 ResolveForward(Transform viewer)
    {
        if (viewer == null)
            return Vector3.forward;

        Camera camera = Camera.main;
        if (camera != null)
        {
            Vector3 offset = camera.transform.position - viewer.position;
            offset.y = 0f;
            if (offset.sqrMagnitude <= 30f * 30f)
            {
                Vector3 forward = camera.transform.forward;
                forward.y = 0f;
                if (forward.sqrMagnitude > 0.0001f)
                    return forward.normalized;
            }
        }

        Vector3 vehicleForward = viewer.forward;
        vehicleForward.y = 0f;
        return vehicleForward.sqrMagnitude > 0.0001f ? vehicleForward.normalized : Vector3.forward;
    }

    public static bool IsInsideView(Vector3 viewer, Vector3 forward, float fovDegrees, float maxDistance, Vector3 point)
    {
        Vector3 offset = point - viewer;
        offset.y = 0f;
        float distanceSqr = offset.sqrMagnitude;
        if (distanceSqr > maxDistance * maxDistance)
            return false;
        if (distanceSqr < 0.0001f)
            return true;

        forward.y = 0f;
        if (forward.sqrMagnitude < 0.0001f)
            return false;

        float halfAngle = Mathf.Clamp(fovDegrees, 10f, 300f) * 0.5f;
        float dot = Vector3.Dot(forward.normalized, offset / Mathf.Sqrt(distanceSqr));
        return dot >= Mathf.Cos(halfAngle * Mathf.Deg2Rad);
    }

    /// <summary>
    /// Lower is built first. Objects in the cone get a strong boost; objects
    /// behind the viewer are still streamed, just after the visible ones.
    /// </summary>
    public static float Score(Vector3 viewer, Vector3 forward, float fovDegrees, Vector3 point)
    {
        Vector3 offset = point - viewer;
        offset.y = 0f;
        float distance = offset.magnitude;
        if (distance < 0.001f)
            return 0f;

        forward.y = 0f;
        float dot = forward.sqrMagnitude > 0.0001f
            ? Vector3.Dot(forward.normalized, offset / distance)
            : 0f;
        bool inView = dot >= Mathf.Cos(Mathf.Clamp(fovDegrees, 10f, 300f) * 0.5f * Mathf.Deg2Rad);

        // 0.55x in view, 1.0x at the edge, 1.35x behind. The multipliers are
        // deliberately gentle: a close wall behind the bus is still more
        // important than a hilltop two kilometres ahead.
        float weight = inView ? 0.55f : Mathf.Lerp(1.35f, 1f, Mathf.InverseLerp(-1f, 0f, dot));
        return distance * weight;
    }
}
