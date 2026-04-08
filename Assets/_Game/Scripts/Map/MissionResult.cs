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
    public float totalDistanceKm;
    public float totalTimeMinutes;

    public int xpEarned;
    public int collisions;
    public int redLightViolations;

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

        result.totalDistanceKm = FreeDriveSession.Instance?.distanceDrivenKm ?? 0f;
        result.totalTimeMinutes = FreeDriveSession.Instance?.sessionTimeSeconds / 60f ?? 0f;
        
        // XP formula: base 100 + score bonus + star bonus
        result.xpEarned = Mathf.RoundToInt(
            100f + (result.totalScore * 2f) + (result.starRating * 50f));

        return result;
    }
}