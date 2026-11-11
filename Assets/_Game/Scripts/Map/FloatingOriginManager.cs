using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Floating origin (section 3.3).
///
/// When the tracked bus is more than <see cref="shiftThresholdMeters"/> from the
/// world origin, the whole world is recentred so the bus stays near (0, 0, 0),
/// avoiding single-precision jitter far from the origin. <see cref="MapOrigin"/>
/// is made shiftable so GPS conversions stay consistent after a shift.
///
/// Registrations: callers (bus, tile loader, road parent) register transforms
/// via <see cref="Register"/>; on a shift every registered object moves by the
/// negated shift and MapOrigin shifts by the equivalent GPS delta.
/// </summary>
public class FloatingOriginManager : MonoBehaviour
{
    public static FloatingOriginManager Instance { get; private set; }

    [Header("Tracked object")]
    [Tooltip("Bus to track. Resolved from MissionManager / BusController when unassigned.")]
    public Transform trackedObject;

    [Header("Settings")]
    [Tooltip("Recentre when the tracked object is farther than this from the origin (2–3 km spec).")]
    [Min(0f)] public float shiftThresholdMeters = 2500f;
    [Tooltip("Minimum seconds between origin shifts.")]
    [Min(0f)] public float minSecondsBetweenShifts = 5f;

    readonly List<(Transform transform, Rigidbody rigidbody)> _registrations = new();
    double _lastShiftTime;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void EnsureInstance()
    {
        if (Instance != null) return;
        var existing = UnityEngine.Object.FindAnyObjectByType<FloatingOriginManager>();
        if (existing != null)
        {
            Instance = existing;
            return;
        }
        var go = new GameObject("FloatingOriginManager");
        Instance = go.AddComponent<FloatingOriginManager>();
        DontDestroyOnLoad(go);
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (trackedObject == null)
        {
            var bus = MissionManager.Instance != null ? MissionManager.Instance.busController : null;
            if (bus == null) bus = FindAnyObjectByType<BusController>();
            trackedObject = bus != null ? bus.transform : null;
        }

        if (MapTileLoader.Instance != null) Register(MapTileLoader.Instance.transform);
        if (OSMRoadMeshBuilder.Instance != null && OSMRoadMeshBuilder.Instance.RoadsParent != null)
            Register(OSMRoadMeshBuilder.Instance.RoadsParent.transform);
    }

    void Update()
    {
        if (trackedObject == null)
        {
            var bus = MissionManager.Instance != null ? MissionManager.Instance.busController : null;
            if (bus == null) bus = FindAnyObjectByType<BusController>();
            trackedObject = bus != null ? bus.transform : null;
            if (trackedObject == null) return;
        }

        if (Time.unscaledTime - _lastShiftTime < minSecondsBetweenShifts) return;
        if (trackedObject.position.magnitude > shiftThresholdMeters) PerformShift();
    }

    /// <summary>Registers an object to be moved when the origin shifts.</summary>
    public static void Register(Transform transform, Rigidbody rigidbody = null)
    {
        if (transform == null) return;
        EnsureInstanceCreated();
        if (Instance._registrations.Exists(r => r.transform == transform)) return;
        Instance._registrations.Add((transform, rigidbody));
    }

    /// <summary>Removes a previously registered object.</summary>
    public static void Unregister(Transform transform)
    {
        if (transform == null || Instance == null) return;
        Instance._registrations.RemoveAll(r => r.transform == transform);
    }

    static void EnsureInstanceCreated()
    {
        if (Instance != null) return;
        var existing = UnityEngine.Object.FindAnyObjectByType<FloatingOriginManager>();
        Instance = existing ?? new GameObject("FloatingOriginManager").AddComponent<FloatingOriginManager>();
        if (Instance.gameObject.scene.IsValid() && Instance.gameObject.scene.isLoaded)
            DontDestroyOnLoad(Instance.gameObject);
    }

    void PerformShift()
    {
        if (trackedObject == null) return;

        Vector3 shift = trackedObject.position;
        shift.y = 0f; // recentre horizontally; keep altitude
        if (shift.sqrMagnitude < 1f) return;

        foreach (var registration in _registrations)
        {
            if (registration.transform == null) continue;
            Vector3 newPosition = registration.transform.position - shift;
            if (registration.rigidbody != null)
                registration.rigidbody.position = newPosition; // teleport the physics body
            else
                registration.transform.position = newPosition;
        }

        var origin = ResolveOrigin();
        if (origin != null)
        {
            origin.ShiftOrigin(shift);
            Debug.Log($"[FloatingOrigin] Recentred world by {shift.magnitude:F1} m. " +
                      $"New origin: {origin.originLat:F6}, {origin.originLon:F6} ({origin.originLabel}).");
        }

        _lastShiftTime = Time.unscaledTime;
    }

    static MapOrigin ResolveOrigin()
    {
        var converter = CoordinateConverter.Instance;
        MapOrigin origin = converter != null ? converter.mapOrigin : null;
        if (origin == null)
        {
            var all = Resources.FindObjectsOfTypeAll<MapOrigin>();
            if (all.Length > 0) origin = all[0];
        }

        // Work on a runtime copy so the shipped asset is not modified in the Editor.
        if (origin != null)
        {
            var runtimeCopy = UnityEngine.Object.Instantiate(origin);
            if (converter != null) converter.mapOrigin = runtimeCopy;
            origin = runtimeCopy;
        }
        return origin;
    }
}