using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Places street lights along every built road and switches them on at night.
///
/// Street lights are baked into the road mesh build: after each OSM way is turned
/// into a road ribbon, <see cref="PlaceLightsAlongRoad"/> stamps light poles at a
/// fixed interval along the centreline. Each pole carries a <see cref="Light"/>
/// whose <see cref="Light.enabled"/> is driven by <see cref="TimeOfDaySystem.IsNight"/>.
///
/// At night the fog is already tinted dark by <see cref="FogController"/> and
/// <see cref="CinematicAtmosphereController"/>; street lights cut through it so the
/// road stays readable after sunset.
/// </summary>
public class StreetLightManager : MonoBehaviour
{
    public static StreetLightManager Instance { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void EnsureInstance()
    {
        if (Instance != null) return;
        var existing = UnityEngine.Object.FindAnyObjectByType<StreetLightManager>();
        if (existing != null) { Instance = existing; return; }
        var go = new GameObject("StreetLightManager");
        Instance = go.AddComponent<StreetLightManager>();
        DontDestroyOnLoad(go);
    }
    [Header("Street Light Prefab")]
    [Tooltip("FBX/prefab for the street light pole. A Light component is added at runtime if missing.")]
    public GameObject streetLightPrefab;

    [Header("Placement")]
    [Tooltip("Metres between street lights along each road.")]
    [Min(5f)] public float spacingMeters = 25f;
    [Tooltip("Metres above the road surface the light sits.")]
    public float lightHeightMeters = 7f;
    [Tooltip("Only place lights on road types that carry traffic at night.")]
    public bool onlyMajorRoads = false;

    [Header("Night Behaviour")]
    [Tooltip("Light intensity after dark (TimeOfDaySystem.nightStreetLightIntensity is the fallback).")]
    public float nightIntensity = 1.15f;
    [Tooltip("Light colour after dark - warm sodium-style street lighting.")]
    public Color nightLightColor = new Color(1f, 0.92f, 0.78f, 1f);
    [Tooltip("Light colour in daylight (lights are off, but the emissive texture may still read).")]
    public Color dayLightColor = Color.white;

    [Header("References")]
    public TimeOfDaySystem timeOfDay;

    // Every placed street light, so they can all be toggled together at dusk/dawn.
    readonly List<Light> _streetLights = new();
    bool _timeOfDaySubscribed;

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

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
        UnsubscribeTimeOfDay();
    }

    void Update()
    {
        if (timeOfDay == null) timeOfDay = TimeOfDaySystem.Instance;
        if (timeOfDay == null) return;

        if (!_timeOfDaySubscribed)
        {
            _timeOfDaySubscribed = true;
            // TimeOfDaySystem doesn't expose an event, so poll once per frame.
            // The cost is a single boolean read and a Light.enabled write only when it changes.
        }

        bool isNight = timeOfDay.IsNight;
        foreach (var light in _streetLights)
        {
            if (light != null) light.enabled = isNight;
        }
    }

    /// <summary>
    /// Call from <see cref="OSMRoadMeshBuilder"/> after each road way is built.
    /// Places street lights along the baked (grade-aware) centreline points.
    /// </summary>
    public void PlaceLightsAlongRoad(List<Vector3> points, OSMWay way, float roadWidth)
    {
        if (points == null || points.Count < 2) return;
        if (streetLightPrefab == null)
        {
            Debug.LogWarning("[StreetLightManager] No street light prefab assigned - skipping street lights.");
            return;
        }

        if (onlyMajorRoads && !IsMajorRoad(way)) return;

        float spacing = Mathf.Max(1f, spacingMeters);
        float halfWidth = roadWidth * 0.5f;

        for (int i = 1; i < points.Count; i++)
        {
            Vector3 from = points[i - 1];
            Vector3 to = points[i];
            Vector3 segment = to - from;
            float segmentLength = segment.magnitude;
            if (segmentLength < 1f) continue;

            Vector3 direction = segment / segmentLength;
            int count = Mathf.Max(1, Mathf.FloorToInt(segmentLength / spacing));

            // Offset lights to the left of the driving direction so they stand
            // clear of the bus lane on left-traffic countries (and vice versa).
            bool leftTraffic = RoadSpawnResolver.ResolveCountry()?.DrivesOnLeft == true;
            float sideSign = leftTraffic ? -1f : 1f;
            Vector3 lateral = Vector3.Cross(Vector3.up, direction).normalized * sideSign * (halfWidth + 1.2f);

            for (int j = 0; j <= count; j++)
            {
                float t = (float)j / count;
                Vector3 position = Vector3.Lerp(from, to, t) + lateral;
                // Follow the baked road elevation: points already carry the grade.
                position.y = Mathf.Lerp(from.y, to.y, t) + lightHeightMeters;

                var pole = Instantiate(streetLightPrefab, position, Quaternion.LookRotation(direction, Vector3.up));
                pole.name = $"StreetLight_{way.id}_{i}_{j}";
                pole.transform.localScale = Vector3.one;

                var light = pole.GetComponentInChildren<Light>();
                if (light == null)
                {
                    light = pole.AddComponent<Light>();
                    light.type = LightType.Point;
                    light.range = 22f;
                }

                light.color = nightLightColor;
                light.intensity = nightIntensity;
                light.enabled = timeOfDay != null && timeOfDay.IsNight;

                _streetLights.Add(light);
            }
        }
    }

    static bool IsMajorRoad(OSMWay way)
    {
        string type = way.GetRoadType();
        return type == "motorway" || type == "trunk" || type == "primary" ||
               type == "secondary" || type == "tertiary";
    }

    void UnsubscribeTimeOfDay()
    {
        _timeOfDaySubscribed = false;
    }
}