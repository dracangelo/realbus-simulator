using UnityEngine;

[System.Serializable]
public enum MissionArchetype
{
    ScheduledRoute,
    RushHourChaos,
    WeatherChallenge,
    UnexpectedEvents,
    RuleEnforcement
}

[CreateAssetMenu(fileName = "MissionData", menuName = "RealBus/MissionData")]
public class MissionData : ScriptableObject
{
    [Header("Mode")]
    public MissionArchetype missionType = MissionArchetype.ScheduledRoute;
    public WeatherState requiredWeather = WeatherState.HeavyRain;
    [Range(0f, 1f)] public float requiredWeatherIntensity = 0.85f;
    [Min(0)] public int minimumDynamicEvents = 0;
    [Min(0)] public int maximumAllowedViolations = 2;
    [Range(0f, 1f)] public float minimumSafetyScore = 0.75f;
    public int scenarioBonusXP = 0;

    [Header("Route")]
    public BusRoute route;

    [Header("Schedule")]
    public float scheduledDepartureTime = 480f; // 08:00 in minutes
    public float targetDurationMinutes = 25f;

    [Header("Targets")]
    public int minPassengersTarget = 30;
    public int stretchPassengersTarget = 45;
    public float punctualityTarget = 0.8f; // 80% on time

    [Header("Rewards")]
    public int baseXP = 200;
    public float difficultyMultiplier = 1.0f;
    public int timeBonusXP = 50;

    [Header("Info")]
    public string missionName = "CBD to Westlands";
    public string description = "Complete the full route on time.";
    [TextArea(2, 4)] public string specialInstruction = string.Empty;
    public int starRating = 1; // 1-5 difficulty

    public int PassengerCountTarget => Mathf.Max(minPassengersTarget, stretchPassengersTarget);
    public bool IsRushHourMission => missionType == MissionArchetype.RushHourChaos;
    public bool IsWeatherChallengeMission => missionType == MissionArchetype.WeatherChallenge;
    public bool IsUnexpectedEventMission => missionType == MissionArchetype.UnexpectedEvents;
    public bool IsRuleEnforcementMission => missionType == MissionArchetype.RuleEnforcement;

    public string GetDepartureLabel()
    {
        int totalMinutes = Mathf.FloorToInt(scheduledDepartureTime);
        int hours24 = (totalMinutes / 60) % 24;
        int minutes = totalMinutes % 60;
        string ampm = hours24 >= 12 ? "PM" : "AM";
        int hours12 = hours24 % 12;
        if (hours12 == 0) hours12 = 12;
        return $"{hours12:00}:{minutes:00} {ampm}";
    }

    public string GetMissionTypeLabel()
    {
        switch (missionType)
        {
            case MissionArchetype.RushHourChaos:
                return "Rush Hour Chaos";
            case MissionArchetype.WeatherChallenge:
                return "Weather Challenge";
            case MissionArchetype.UnexpectedEvents:
                return "Unexpected Events";
            case MissionArchetype.RuleEnforcement:
                return "Rule Enforcement";
            default:
                return "Scheduled Route";
        }
    }

    public string GetRequiredWeatherLabel()
    {
        switch (requiredWeather)
        {
            case WeatherState.LightRain:
                return "Light Rain";
            case WeatherState.HeavyRain:
                return "Heavy Rain";
            case WeatherState.PartlyCloudy:
                return "Partly Cloudy";
            default:
                return requiredWeather.ToString();
        }
    }

    public bool IsPeakDepartureWindow()
    {
        return IsPeakHourTime(scheduledDepartureTime);
    }

    public int GetEffectiveMinimumDynamicEvents()
    {
        int baseline = IsUnexpectedEventMission ? 2 : IsRushHourMission ? 1 : 0;
        return Mathf.Max(minimumDynamicEvents, baseline);
    }

    public int GetEffectiveViolationLimit()
    {
        if (IsRuleEnforcementMission)
            return Mathf.Clamp(maximumAllowedViolations, 0, 3);

        return Mathf.Max(0, maximumAllowedViolations);
    }

    public float GetEffectiveSafetyThreshold01()
    {
        float baseline = IsWeatherChallengeMission ? 0.85f : 0.75f;
        return Mathf.Clamp01(Mathf.Max(minimumSafetyScore, baseline));
    }

    public string GetScenarioObjectiveText()
    {
        switch (missionType)
        {
            case MissionArchetype.RushHourChaos:
                return $"Hold schedule through peak traffic and carry {PassengerCountTarget}+ passengers.";
            case MissionArchetype.WeatherChallenge:
                return $"Finish in {GetRequiredWeatherLabel()} with no crashes and safety above {Mathf.RoundToInt(GetEffectiveSafetyThreshold01() * 100f)}%.";
            case MissionArchetype.UnexpectedEvents:
                return $"Handle at least {GetEffectiveMinimumDynamicEvents()} disruptions while keeping the route moving.";
            case MissionArchetype.RuleEnforcement:
                return $"Complete the run with at most {GetEffectiveViolationLimit()} traffic violations.";
            default:
                return $"Run the full route on schedule and carry {PassengerCountTarget}+ passengers.";
        }
    }

    public string GetOperationalPressureText()
    {
        string custom = string.IsNullOrWhiteSpace(specialInstruction) ? string.Empty : specialInstruction.Trim();
        switch (missionType)
        {
            case MissionArchetype.RushHourChaos:
                return !string.IsNullOrEmpty(custom)
                    ? custom
                    : "Peak loads, tight headways, and stop-vs-schedule tradeoffs.";
            case MissionArchetype.WeatherChallenge:
                return !string.IsNullOrEmpty(custom)
                    ? custom
                    : $"Reduced grip and visibility expected in {GetRequiredWeatherLabel()}.";
            case MissionArchetype.UnexpectedEvents:
                return !string.IsNullOrEmpty(custom)
                    ? custom
                    : "Expect closures, accidents, or detours that force rerouting.";
            case MissionArchetype.RuleEnforcement:
                return !string.IsNullOrEmpty(custom)
                    ? custom
                    : "Inspections, fines, and legal driving discipline all count.";
            default:
                return !string.IsNullOrEmpty(custom)
                    ? custom
                    : "Reliable timetable driving with passenger satisfaction pressure.";
        }
    }

    public static bool IsPeakHourTime(float timeMinutes)
    {
        return (timeMinutes >= 420f && timeMinutes <= 570f) ||
               (timeMinutes >= 960f && timeMinutes <= 1140f);
    }
}
