using System.Collections.Generic;
using UnityEngine;

public enum DynamicEventType
{
    TrafficAccident,
    PlannedRoadClosure,
    PoliceCheckpoint,
    ConstructionZone,
    SignalFailure,
    SuddenWeatherTransition
}

[System.Serializable]
public class DynamicEventRecord
{
    public DynamicEventType type;
    public string title;
    public string description;
    public float startProgress01;
    public float durationSeconds;
    public bool triggered;
    public bool resolved;
    public bool complianceObserved;
    public bool penaltyApplied;
}

public class DynamicEventSystem : MonoBehaviour
{
    struct EventWeight
    {
        public DynamicEventType type;
        public float weight;

        public EventWeight(DynamicEventType type, float weight)
        {
            this.type = type;
            this.weight = weight;
        }
    }

    public static DynamicEventSystem Instance { get; private set; }

    [Header("References")]
    public BusController busController;

    [Header("Mission Events")]
    [Range(0, 4)] public int minEventsPerRoute = 1;
    [Range(0, 4)] public int maxEventsPerRoute = 3;
    public float checkpointSpeedLimitKmh = 20f;
    public float constructionSpeedLimitKmh = 25f;
    public float signalFailureYieldSpeedKmh = 15f;

    readonly List<DynamicEventRecord> plannedEvents = new List<DynamicEventRecord>();
    readonly List<DynamicEventRecord> completedEvents = new List<DynamicEventRecord>();

    DynamicEventRecord activeEvent;
    BusRoute activeRoute;
    float activeEventStartTime;
    float lastRouteStartTime;

    public IReadOnlyList<DynamicEventRecord> PlannedEvents => plannedEvents;
    public IReadOnlyList<DynamicEventRecord> CompletedEvents => completedEvents;
    public DynamicEventRecord ActiveEvent => activeEvent;
    public int TriggeredEventCount => completedEvents.Count + (activeEvent != null && activeEvent.triggered ? 1 : 0);
    public string CurrentEventLabel => activeEvent != null && activeEvent.triggered
        ? $"{activeEvent.title}: {activeEvent.description}"
        : "No active event";

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this); return; }
        Instance = this;
    }

    void Start()
    {
        if (busController == null)
            busController = FindFirstObjectByType<BusController>();
    }

    void Update()
    {
        var mission = MissionManager.Instance;
        if (mission == null || !mission.routeActive || activeRoute == null)
            return;

        TryActivateNextEvent(mission);
        UpdateActiveEvent();
    }

    public void StartRoute(BusRoute route)
    {
        ResetRouteEvents();
        activeRoute = route;
        lastRouteStartTime = Time.time;
        BuildPlannedEvents(route);
    }

    public void CompleteRoute()
    {
        if (activeEvent != null)
            ResolveActiveEvent();
    }

    public void ResetRouteEvents()
    {
        plannedEvents.Clear();
        completedEvents.Clear();
        activeEvent = null;
        activeRoute = null;
        activeEventStartTime = 0f;
        lastRouteStartTime = 0f;
    }

    public string GetMissionEventSummary()
    {
        if (TriggeredEventCount == 0)
            return "No dynamic events";

        var parts = new List<string>();
        for (int i = 0; i < completedEvents.Count; i++)
        {
            var ev = completedEvents[i];
            string compliance = ev.complianceObserved ? "OK" : ev.penaltyApplied ? "PEN" : "INFO";
            parts.Add($"{ev.title} {compliance}");
        }

        if (activeEvent != null && activeEvent.triggered)
        {
            string compliance = activeEvent.complianceObserved ? "OK" : activeEvent.penaltyApplied ? "PEN" : "LIVE";
            parts.Add($"{activeEvent.title} {compliance}");
        }

        return string.Join(" | ", parts);
    }

    void BuildPlannedEvents(BusRoute route)
    {
        if (route == null)
            return;

        int stopCount = route.stops != null ? route.stops.Length : route.GetStopCount();
        var missionData = MissionManager.Instance != null ? MissionManager.Instance.missionData : null;
        int missionMinimum = missionData != null ? missionData.GetEffectiveMinimumDynamicEvents() : 0;
        int targetCount = Mathf.Clamp(
            Mathf.Max(Mathf.RoundToInt((stopCount - 1) / 3f), missionMinimum),
            minEventsPerRoute,
            Mathf.Max(maxEventsPerRoute, missionMinimum));
        var usedTypes = new HashSet<DynamicEventType>();

        for (int i = 0; i < targetCount; i++)
        {
            DynamicEventType type = PickWeightedEventType(usedTypes);
            usedTypes.Add(type);

            float start = Mathf.Clamp01(0.18f + (i * 0.24f) + Random.Range(-0.04f, 0.08f));
            plannedEvents.Add(CreateRecord(type, start));
        }
    }

    DynamicEventType PickWeightedEventType(HashSet<DynamicEventType> usedTypes)
    {
        var city = CityManager.Instance != null ? CityManager.Instance.activeCity : null;
        var missionData = MissionManager.Instance != null ? MissionManager.Instance.missionData : null;
        WeatherState currentWeather = WeatherSystem.Instance != null ? WeatherSystem.Instance.currentWeather : WeatherState.Clear;
        float timeMinutes = ScheduleManager.Instance != null ? ScheduleManager.Instance.currentTimeMinutes : 720f;
        bool isPeakHour = (timeMinutes >= 420f && timeMinutes <= 570f) || (timeMinutes >= 960f && timeMinutes <= 1140f);
        bool isNight = timeMinutes >= 1200f || timeMinutes <= 360f;
        bool isWet = currentWeather == WeatherState.Drizzle || currentWeather == WeatherState.LightRain ||
                     currentWeather == WeatherState.HeavyRain || currentWeather == WeatherState.Thunderstorm;

        float rushWeight = missionData != null && missionData.IsRushHourMission ? 1.1f : 0f;
        float eventWeight = missionData != null && missionData.IsUnexpectedEventMission ? 1.2f : 0f;
        float enforcementWeight = missionData != null && missionData.IsRuleEnforcementMission ? 1.1f : 0f;
        float weatherWeight = missionData != null && missionData.IsWeatherChallengeMission ? 1.0f : 0f;

        var weights = new List<EventWeight>
        {
            new EventWeight(DynamicEventType.TrafficAccident, 1f + (isPeakHour ? 1.1f : 0f) + (isWet ? 0.8f : 0f) + rushWeight + eventWeight),
            new EventWeight(DynamicEventType.PlannedRoadClosure, 0.9f + (city != null && city.cityCode == "NBO" ? 0.2f : 0f) + eventWeight),
            new EventWeight(DynamicEventType.PoliceCheckpoint, 0.8f + (isNight ? 0.9f : 0.25f) + enforcementWeight),
            new EventWeight(DynamicEventType.ConstructionZone, 1.0f + (!isNight ? 0.8f : 0f) + eventWeight),
            new EventWeight(DynamicEventType.SignalFailure, 0.55f + (isWet ? 1.0f : 0f) + enforcementWeight),
            new EventWeight(DynamicEventType.SuddenWeatherTransition,
                (WeatherSystem.Instance != null && !WeatherSystem.Instance.forceWeather ? 0.8f : 0.1f) + weatherWeight)
        };

        float total = 0f;
        for (int i = 0; i < weights.Count; i++)
        {
            if (usedTypes.Contains(weights[i].type))
                continue;
            total += Mathf.Max(0.01f, weights[i].weight);
        }

        float roll = Random.value * total;
        for (int i = 0; i < weights.Count; i++)
        {
            if (usedTypes.Contains(weights[i].type))
                continue;

            roll -= Mathf.Max(0.01f, weights[i].weight);
            if (roll <= 0f)
                return weights[i].type;
        }

        return DynamicEventType.TrafficAccident;
    }

    DynamicEventRecord CreateRecord(DynamicEventType type, float startProgress01)
    {
        switch (type)
        {
            case DynamicEventType.TrafficAccident:
                return new DynamicEventRecord
                {
                    type = type,
                    title = "ACCIDENT",
                    description = "Road ahead blocked. Follow detour advisory.",
                    startProgress01 = startProgress01,
                    durationSeconds = 50f
                };
            case DynamicEventType.PlannedRoadClosure:
                return new DynamicEventRecord
                {
                    type = type,
                    title = "ROAD CLOSED",
                    description = "Signed diversion in effect. Expect delay.",
                    startProgress01 = startProgress01,
                    durationSeconds = 60f
                };
            case DynamicEventType.PoliceCheckpoint:
                return new DynamicEventRecord
                {
                    type = type,
                    title = "CHECKPOINT",
                    description = $"Mandatory slow zone {checkpointSpeedLimitKmh:F0} km/h.",
                    startProgress01 = startProgress01,
                    durationSeconds = 25f
                };
            case DynamicEventType.ConstructionZone:
                return new DynamicEventRecord
                {
                    type = type,
                    title = "CONSTRUCTION",
                    description = $"Temporary {constructionSpeedLimitKmh:F0} km/h single-lane passage.",
                    startProgress01 = startProgress01,
                    durationSeconds = 40f
                };
            case DynamicEventType.SignalFailure:
                return new DynamicEventRecord
                {
                    type = type,
                    title = "SIGNAL OUT",
                    description = $"Treat junction as give-way. Slow below {signalFailureYieldSpeedKmh:F0} km/h.",
                    startProgress01 = startProgress01,
                    durationSeconds = 20f
                };
            default:
                return new DynamicEventRecord
                {
                    type = type,
                    title = "WEATHER SHIFT",
                    description = "Conditions changing mid-route. Grip update applied.",
                    startProgress01 = startProgress01,
                    durationSeconds = 12f
                };
        }
    }

    void TryActivateNextEvent(MissionManager mission)
    {
        if (activeEvent != null)
            return;

        for (int i = 0; i < plannedEvents.Count; i++)
        {
            var candidate = plannedEvents[i];
            if (candidate == null || candidate.triggered)
                continue;

            if (GetRouteProgress01(mission) >= candidate.startProgress01)
            {
                ActivateEvent(candidate);
                return;
            }
        }
    }

    void ActivateEvent(DynamicEventRecord ev)
    {
        activeEvent = ev;
        activeEvent.triggered = true;
        activeEventStartTime = Time.time;

        if (ev.type == DynamicEventType.SuddenWeatherTransition)
            ApplySuddenWeatherTransition();
        else if (ev.type == DynamicEventType.TrafficAccident || ev.type == DynamicEventType.PlannedRoadClosure)
        {
            TryActivateDetour(ev);
            ScoreTracker.Instance?.ApplyDynamicEventImpact(0f, 6f, ev.title, false);
        }

        Debug.Log($"DynamicEventSystem: Activated {ev.title} — {ev.description}");
    }

    void UpdateActiveEvent()
    {
        if (activeEvent == null || busController == null)
            return;

        switch (activeEvent.type)
        {
            case DynamicEventType.PoliceCheckpoint:
                TrackSlowZoneCompliance(activeEvent, checkpointSpeedLimitKmh, 7f, 1f, 0f, 10f);
                break;
            case DynamicEventType.ConstructionZone:
                TrackSlowZoneCompliance(activeEvent, constructionSpeedLimitKmh, 5f, 3f, 1f, 8f);
                break;
            case DynamicEventType.SignalFailure:
                TrackSlowZoneCompliance(activeEvent, signalFailureYieldSpeedKmh, 6f, 0f, 2f, 6f);
                break;
        }

        if (Time.time - activeEventStartTime >= activeEvent.durationSeconds)
            ResolveActiveEvent();
    }

    void TrackSlowZoneCompliance(DynamicEventRecord ev, float targetSpeedKmh, float safetyPenalty, float punctualityPenalty, float efficiencyPenalty, float complianceWindowSeconds)
    {
        if (busController.currentSpeedKmh <= targetSpeedKmh)
            ev.complianceObserved = true;

        if (!ev.penaltyApplied && Time.time - activeEventStartTime >= complianceWindowSeconds && busController.currentSpeedKmh > targetSpeedKmh + 3f)
        {
            ev.penaltyApplied = true;
            ScoreTracker.Instance?.ApplyDynamicEventImpact(safetyPenalty, punctualityPenalty, ev.title, true, efficiencyPenalty);
        }
    }

    void ResolveActiveEvent()
    {
        if (activeEvent == null)
            return;

        activeEvent.resolved = true;
        completedEvents.Add(activeEvent);
        activeEvent = null;
    }

    void ApplySuddenWeatherTransition()
    {
        if (WeatherSystem.Instance == null)
            return;

        WeatherState state = PickTransitionWeatherState();
        float intensity = GetWeatherIntensity(state);
        float temp = WeatherSystem.Instance.temperature;
        WeatherSystem.Instance.ApplyWeather(state, intensity, temp);
    }

    WeatherState PickTransitionWeatherState()
    {
        WeatherState current = WeatherSystem.Instance != null ? WeatherSystem.Instance.currentWeather : WeatherState.Clear;
        if (current == WeatherState.Clear || current == WeatherState.PartlyCloudy)
            return Random.value < 0.5f ? WeatherState.Overcast : WeatherState.LightRain;
        if (current == WeatherState.Overcast)
            return Random.value < 0.5f ? WeatherState.LightRain : WeatherState.HeavyRain;
        if (current == WeatherState.LightRain)
            return Random.value < 0.6f ? WeatherState.HeavyRain : WeatherState.Fog;
        if (current == WeatherState.HeavyRain)
            return Random.value < 0.5f ? WeatherState.Thunderstorm : WeatherState.Fog;
        return WeatherState.Overcast;
    }

    float GetWeatherIntensity(WeatherState state)
    {
        switch (state)
        {
            case WeatherState.PartlyCloudy: return 0.2f;
            case WeatherState.Overcast: return 0.4f;
            case WeatherState.Fog: return 0.7f;
            case WeatherState.Drizzle: return 0.3f;
            case WeatherState.LightRain: return 0.5f;
            case WeatherState.HeavyRain: return 0.85f;
            case WeatherState.Thunderstorm: return 1f;
            case WeatherState.Snow: return 0.6f;
            case WeatherState.Blizzard: return 1f;
            default: return 0f;
        }
    }

    float GetRouteProgress01(MissionManager mission)
    {
        if (activeRoute == null || activeRoute.stops == null || activeRoute.stops.Length <= 1)
            return 0f;

        int segments = Mathf.Max(1, activeRoute.stops.Length - 1);
        float stopProgress = Mathf.Clamp01((mission.currentStopIndex - 1) / (float)segments);
        float elapsed = Mathf.Max(0f, Time.time - lastRouteStartTime);
        float timeProgress = mission.missionData != null && mission.missionData.targetDurationMinutes > 0.01f
            ? Mathf.Clamp01(elapsed / (mission.missionData.targetDurationMinutes * 60f))
            : stopProgress;

        return Mathf.Max(stopProgress, timeProgress * 0.9f);
    }

    void TryActivateDetour(DynamicEventRecord ev)
    {
        var mission = MissionManager.Instance;
        if (mission == null)
            return;

        if (!mission.TryGetGuidancePointAhead(ev.type == DynamicEventType.TrafficAccident ? 180f : 240f, out var blockedPoint))
            return;

        bool rerouted = mission.TryApplyTemporaryDiversion(
            ev.title,
            ev.description,
            blockedPoint,
            ev.type == DynamicEventType.TrafficAccident ? 35f : 55f);

        ev.description = rerouted
            ? $"{ev.description} Diversion path loaded."
            : $"{ev.description} No valid diversion found.";
    }
}
