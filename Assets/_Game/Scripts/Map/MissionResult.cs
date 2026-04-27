using UnityEngine;

[System.Serializable]
public class MissionResult
{
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
    public float totalDistanceKm;
    public float totalTimeMinutes;

    public int xpEarned;
    public int collisions;
    public int redLightViolations;
    public int totalViolations;
    public string violationSummary;
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

        result.totalDistanceKm = FreeDriveSession.Instance?.distanceDrivenKm ?? 0f;
        result.totalTimeMinutes = FreeDriveSession.Instance?.sessionTimeSeconds / 60f ?? 0f;

        result.collisions = ScoreTracker.Instance?.CollisionCount ?? 0;
        result.redLightViolations = ScoreTracker.Instance?.RedLightViolationCount ?? 0;

        var violationSystem = ExtendedTrafficViolationSystem.Instance;
        result.totalViolations = violationSystem != null ? violationSystem.TotalViolationCount : result.redLightViolations;
        result.violationSummary = violationSystem != null ? violationSystem.GetViolationSummary() : $"SIG {result.redLightViolations}";
        result.dynamicEventsTriggered = DynamicEventSystem.Instance != null ? DynamicEventSystem.Instance.TriggeredEventCount : 0;
        result.dynamicEventSummary = DynamicEventSystem.Instance != null ? DynamicEventSystem.Instance.GetMissionEventSummary() : "No dynamic events";
        
        if (result.satisfactionScore < 60f)
            result.starRating = Mathf.Min(result.starRating, 2);

        var missionData = MissionManager.Instance != null ? MissionManager.Instance.missionData : null;
        int routeStars = missionData != null
            ? Mathf.Clamp(missionData.starRating, 1, 5)
            : Mathf.Clamp(route != null ? route.difficulty : result.starRating, 1, 5);
        float difficultyMultiplier = missionData != null
            ? Mathf.Max(0.1f, missionData.difficultyMultiplier)
            : 1f;
        int baseXp = missionData != null && missionData.baseXP > 0
            ? missionData.baseXP
            : routeStars * 200;

        result.xpEarned = Mathf.RoundToInt(baseXp * (result.totalScore / 100f) * difficultyMultiplier);

        if (missionData != null && result.punctualityScore >= Mathf.Clamp01(missionData.punctualityTarget) * 100f)
            result.xpEarned += Mathf.Max(0, missionData.timeBonusXP);

        result.xpEarned = Mathf.Max(0, result.xpEarned - (result.totalViolations * 15));

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
}
