using UnityEngine;

/// <summary>
/// Makes a spawned traffic body cheap before it ever reaches the road.
///
/// Ambient traffic is not a drivable vehicle. It follows a spline or a kinematic
/// rigidbody, so a <see cref="WheelCollider"/> on it is pure solver cost, a
/// <see cref="MeshCollider"/> is a cooked triangle soup where a box would do,
/// and a driver <see cref="Animator"/> or engine <see cref="AudioSource"/> is
/// work nobody asked for. The player's bus is never passed through here: it
/// keeps its wheel colliders and its full visual rig.
///
/// The method is idempotent, so a prefab prepared once can be prepared again by
/// a different spawner without losing anything.
/// </summary>
public static class TrafficBodyOptimizer
{
    public struct Report
    {
        public int wheelCollidersRemoved;
        public int meshCollidersRemoved;
        public int animatorsDisabled;
        public int audioSourcesDisabled;
        public int lightsFound;
        public bool boxColliderAdded;

        public bool Changed => wheelCollidersRemoved > 0 || meshCollidersRemoved > 0 ||
                               animatorsDisabled > 0 || audioSourcesDisabled > 0 ||
                               boxColliderAdded;
    }

    /// <summary>
    /// Strips player-only physics and expensive presentation from a traffic body.
    /// <paramref name="isPlayerVehicle"/> exists so a caller can share this code
    /// path without ever touching the bus the player drives.
    /// </summary>
    public static Report Prepare(GameObject root, bool isPlayerVehicle = false)
    {
        var report = new Report();
        if (root == null || isPlayerVehicle)
            return report;

        // A wheel collider is the player's steering model. Traffic is driven by
        // the spline or a kinematic body, so the solver would only ever sleep on
        // these; removing them also stops a wheel from becoming the contact that
        // decides the car's collision response.
        foreach (WheelCollider wheel in root.GetComponentsInChildren<WheelCollider>(true))
        {
            if (wheel == null)
                continue;
            wheel.enabled = false;
            DestroySafe(wheel);
            report.wheelCollidersRemoved++;
        }

        // A mesh collider on a traffic body costs cooking and broadphase work
        // for a shape the player only ever brushes past. Triggers are kept: a
        // stop, depot or mission volume may live on the same prefab.
        bool hasSolidCollider = false;
        foreach (Collider collider in root.GetComponentsInChildren<Collider>(true))
        {
            if (collider == null || collider.isTrigger)
                continue;
            if (!collider.enabled)
                continue;
            if (collider is MeshCollider mesh)
            {
                mesh.enabled = false;
                DestroySafe(mesh);
                report.meshCollidersRemoved++;
                continue;
            }
            hasSolidCollider = true;
        }

        // Only a body that had nothing left gets a new box, and only when its
        // renderers give us a real size. Existing authored boxes are left alone:
        // a bus's split collider may be deliberate.
        if (!hasSolidCollider && TryLocalBounds(root, out Bounds bounds))
        {
            var box = root.AddComponent<BoxCollider>();
            box.center = bounds.center;
            box.size = bounds.size;
            report.boxColliderAdded = true;
        }

        // Ambient traffic has no driver and no engine sound of its own. An
        // Animator here would keep a skeleton posed every frame for a model the
        // player never sees up close, and a per-car AudioSource would be a
        // hundred voices fighting the bus mix.
        foreach (Animator animator in root.GetComponentsInChildren<Animator>(true))
        {
            if (animator == null || !animator.enabled)
                continue;
            animator.enabled = false;
            report.animatorsDisabled++;
        }
        foreach (AudioSource audioSource in root.GetComponentsInChildren<AudioSource>(true))
        {
            if (audioSource == null || !audioSource.enabled)
                continue;
            audioSource.enabled = false;
            report.audioSourcesDisabled++;
        }

        // Headlights are the one presentation cost worth keeping near the bus
        // and dropping far from it, so they get a small distance gate instead of
        // being deleted. Current traffic prefabs ship none; the check is free
        // when they do not.
        Light[] lights = root.GetComponentsInChildren<Light>(true);
        report.lightsFound = lights.Length;
        if (lights.Length > 0 && root.GetComponent<TrafficPresentationCull>() == null)
            root.AddComponent<TrafficPresentationCull>();

        return report;
    }

    /// <summary>
    /// Combined renderer bounds in the root's local space, so a fallback box
    /// collider lines up with the visual no matter how the prefab is nested.
    /// </summary>
    public static bool TryLocalBounds(GameObject root, out Bounds bounds)
    {
        bounds = new Bounds(Vector3.zero, Vector3.one);
        if (root == null)
            return false;

        Renderer[] renderers = root.GetComponentsInChildren<Renderer>(true);
        bool any = false;
        Transform rootTransform = root.transform;
        foreach (Renderer renderer in renderers)
        {
            if (renderer == null || !renderer.enabled)
                continue;
            Bounds world = renderer.bounds;
            Vector3 center = world.center;
            Vector3 extents = world.extents;
            for (int corner = 0; corner < 8; corner++)
            {
                var offset = new Vector3(
                    (corner & 1) == 0 ? -extents.x : extents.x,
                    (corner & 2) == 0 ? -extents.y : extents.y,
                    (corner & 4) == 0 ? -extents.z : extents.z);
                Vector3 local = rootTransform.InverseTransformPoint(center + offset);
                if (!any)
                {
                    bounds = new Bounds(local, Vector3.zero);
                    any = true;
                }
                else
                {
                    bounds.Encapsulate(local);
                }
            }
        }

        if (!any)
            return false;

        // A degenerate renderer (a single quad) must still leave a usable box.
        Vector3 size = bounds.size;
        bounds.size = new Vector3(Mathf.Max(.2f, size.x), Mathf.Max(.2f, size.y), Mathf.Max(.2f, size.z));
        return true;
    }

    static void DestroySafe(Object obj)
    {
        if (obj == null)
            return;
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            Object.DestroyImmediate(obj);
            return;
        }
#endif
        Object.Destroy(obj);
    }
}
