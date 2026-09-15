using UnityEngine;

[System.Serializable]
public class MissionResult
{
    const string LastMissionPrefsKey = "mission.last_result.json";

    public string routeName;
    public float totalScore;
    public int starRating;

    public float punctualityScore;
    public float satisfactionScore;
    public float safetyScore;
    public float efficiencyScore;

    public int totalPassengers;
    public int totalFareKES;
    public int fuelRefuelCostKES;
    public int maintenanceCostKES;
    public int netEarningsKES;
    public int coinsEarned;
    public float totalDistanceKm;
    public float totalTimeMinutes;

    public int xpEarned;
    public int pointDeductions;
    public int collisions;
    public int redLightViolations;
    public int totalViolations;
    public string violationSummary;
    public string missionTypeLabel;
    public string missionObjectiveSummary;
    public bool scenarioObjectivePassed;
    public string scenarioObjectiveStatus;
    public int dynamicEventsTriggered;
    public string dynamicEventSummary;
    public float driverReputationRating;
    public bool isShiftSummary;
    public int shiftRoutesCompleted;
    public int shiftPlannedRoutes;
    public float shiftFuelConsumedLitres;
    public int shiftIncidentCount;
    public bool returnedToCorrectBay;

    public static MissionResult Generate(BusRoute route)
    {
        var result = new MissionResult();
        var missionData = MissionManager.Instance != null ? MissionManager.Instance.missionData : null;

        result.routeName = route != null ? route.routeName : "Unknown";
        result.totalScore = ScoreTracker.Instance != null ? ScoreTracker.Instance.totalScore : 0f;
        result.starRating = ScoreTracker.Instance != null ? ScoreTracker.Instance.GetStarRating() : 1;

        result.punctualityScore = ScoreTracker.Instance?.punctualityScore ?? 0f;
        result.satisfactionScore = ScoreTracker.Instance?.satisfactionScore ?? 0f;
        result.safetyScore = ScoreTracker.Instance?.safetyScore ?? 0f;
        result.efficiencyScore = ScoreTracker.Instance?.efficiencyScore ?? 0f;

        result.totalPassengers = PassengerManager.Instance?.totalPassengersServed ?? 0;
        result.totalFareKES = PassengerManager.Instance != null ?
            Mathf.RoundToInt(PassengerManager.Instance.totalFareCollected) : 0;

        if (GameState.Instance != null && GameState.Instance.lastMissionSettlement != null)
        {
            var settlement = GameState.Instance.lastMissionSettlement;
            result.fuelRefuelCostKES = Mathf.RoundToInt(settlement.fuelRefuelCostKES);
            result.maintenanceCostKES = Mathf.RoundToInt(settlement.maintenanceCostKES);
            result.netEarningsKES = Mathf.RoundToInt(settlement.netEarningsKES);
        }
        else
        {
            result.netEarningsKES = result.totalFareKES;
        }
        result.coinsEarned = Mathf.Max(0, result.netEarningsKES);

        result.totalDistanceKm = MissionManager.Instance?.DistanceDrivenKm ?? 0f;
        result.totalTimeMinutes = MissionManager.Instance?.ElapsedSeconds / 60f ?? 0f;

        result.pointDeductions = ScoreTracker.Instance != null ? ScoreTracker.Instance.pointDeductions : 0;
        result.collisions = ScoreTracker.Instance?.CollisionCount ?? 0;
        result.redLightViolations = ScoreTracker.Instance?.RedLightViolationCount ?? 0;

        var violationSystem = ExtendedTrafficViolationSystem.Instance;
        result.totalViolations = violationSystem != null ? violationSystem.TotalViolationCount : result.redLightViolations;
        result.violationSummary = violationSystem != null ? violationSystem.GetViolationSummary() : $"SIG {result.redLightViolations}";
        result.missionTypeLabel = missionData != null ? missionData.GetMissionTypeLabel() : "Scheduled Route";
        result.missionObjectiveSummary = missionData != null ? missionData.GetScenarioObjectiveText() : "Complete the assigned service.";
        result.dynamicEventsTriggered = DynamicEventSystem.Instance != null ? DynamicEventSystem.Instance.TriggeredEventCount : 0;
        result.dynamicEventSummary = DynamicEventSystem.Instance != null ? DynamicEventSystem.Instance.GetMissionEventSummary() : "No dynamic events";
        
        if (result.satisfactionScore < 60f)
            result.starRating = Mathf.Min(result.starRating, 2);

        int routeStars = missionData != null
            ? Mathf.Clamp(missionData.starRating, 1, 5)
            : Mathf.Clamp(route != null ? route.difficulty : result.starRating, 1, 5);
        float difficultyMultiplier = missionData != null
            ? Mathf.Max(0.1f, missionData.difficultyMultiplier)
            : 1f;
        int baseXp = missionData != null && missionData.baseXP > 0
            ? missionData.baseXP
            : routeStars * 200;

        result.xpEarned = GameplayRules.EarnedXP(baseXp, result.totalScore, difficultyMultiplier);

        if (missionData != null && result.punctualityScore >= Mathf.Clamp01(missionData.punctualityTarget) * 100f)
            result.xpEarned += Mathf.Max(0, missionData.timeBonusXP);

        ApplyScenarioOutcome(result, missionData);

        result.xpEarned = Mathf.Max(0, result.xpEarned);

        if (GameState.Instance != null && GameState.Instance.economy != null)
        {
            float reputation = Mathf.Clamp(
                GameState.Instance.economy.driverReputationRating - (result.totalViolations * 1.5f) + ((result.satisfactionScore - 60f) * 0.05f),
                0f, 100f);
            GameState.Instance.economy.driverReputationRating = reputation;
            result.driverReputationRating = reputation;
        }

        DriverShiftSystem.Instance?.TryApplyShiftSummary(result);

        return result;
    }

    static void ApplyScenarioOutcome(MissionResult result, MissionData missionData)
    {
        if (result == null)
            return;

        if (missionData == null)
        {
            result.scenarioObjectivePassed = true;
            result.scenarioObjectiveStatus = "Route completed";
            return;
        }

        bool passed;
        string status;

        switch (missionData.missionType)
        {
            case MissionArchetype.RushHourChaos:
            {
                bool peakWindow = missionData.IsPeakDepartureWindow();
                passed = peakWindow &&
                         result.punctualityScore >= Mathf.Clamp01(missionData.punctualityTarget) * 100f &&
                         result.totalPassengers >= missionData.minPassengersTarget &&
                         result.satisfactionScore >= 65f;
                status = passed
                    ? "Peak-hour pressure handled cleanly"
                    : peakWindow
                        ? "Rush-hour load broke schedule or service quality"
                        : "Mission is outside a peak traffic departure window";
                break;
            }
            case MissionArchetype.WeatherChallenge:
            {
                float safetyThreshold = missionData.GetEffectiveSafetyThreshold01() * 100f;
                bool severeWeather = IsAdverseWeather(missionData.requiredWeather) ||
                                     (WeatherSystem.Instance != null && IsAdverseWeather(WeatherSystem.Instance.currentWeather));
                passed = severeWeather && result.collisions == 0 && result.safetyScore >= safetyThreshold;
                status = passed
                    ? $"Weather run cleared in {missionData.GetRequiredWeatherLabel()}"
                    : $"Weather objective failed: keep safety above {Mathf.RoundToInt(safetyThreshold)}% with no crashes";
                break;
            }
            case MissionArchetype.UnexpectedEvents:
            {
                int targetEvents = missionData.GetEffectiveMinimumDynamicEvents();
                passed = result.dynamicEventsTriggered >= targetEvents && result.safetyScore >= 70f;
                status = passed
                    ? $"Handled {result.dynamicEventsTriggered} live disruptions"
                    : $"Disruption target missed: need {targetEvents}+ live events managed safely";
                break;
            }
            case MissionArchetype.RuleEnforcement:
            {
                int limit = missionData.GetEffectiveViolationLimit();
                bool inspectionClear = DriverShiftSystem.Instance == null ||
                                       DriverShiftSystem.Instance.lastInspection == null ||
                                       DriverShiftSystem.Instance.lastInspection.DefectCount == 0;
                passed = result.totalViolations <= limit && result.redLightViolations == 0 && inspectionClear;
                status = passed
                    ? "Inspection and compliance standards met"
                    : $"Compliance failed: keep violations at {limit} or fewer and clear inspection defects";
                if (!passed)
                    result.starRating = Mathf.Min(result.starRating, 2);
                break;
            }
            default:
                passed = result.totalPassengers >= missionData.minPassengersTarget &&
                         result.punctualityScore >= Mathf.Clamp01(missionData.punctualityTarget) * 100f;
                status = passed
                    ? "Scheduled service completed on target"
                    : "Missed the route schedule or passenger target";
                break;
        }

        result.scenarioObjectivePassed = passed;
        result.scenarioObjectiveStatus = status;

        if (passed)
            result.xpEarned += Mathf.Max(0, missionData.scenarioBonusXP);
    }

    static bool IsAdverseWeather(WeatherState state)
    {
        switch (state)
        {
            case WeatherState.Fog:
            case WeatherState.Drizzle:
            case WeatherState.LightRain:
            case WeatherState.HeavyRain:
            case WeatherState.Thunderstorm:
            case WeatherState.Snow:
            case WeatherState.Blizzard:
                return true;
            default:
                return false;
        }
    }

    public void SaveToPlayerPrefs()
    {
        string payload = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(LastMissionPrefsKey, payload);
        PlayerPrefs.Save();
    }

    public static MissionResult LoadLastFromPlayerPrefs()
    {
        string payload = PlayerPrefs.GetString(LastMissionPrefsKey, string.Empty);
        return string.IsNullOrWhiteSpace(payload)
            ? null
            : JsonUtility.FromJson<MissionResult>(payload);
    }
}
