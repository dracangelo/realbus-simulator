using UnityEngine;

[CreateAssetMenu(fileName = "MissionData", menuName = "RealBus/MissionData")]
public class MissionData : ScriptableObject
{
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
    public int starRating = 1; // 1-5 difficulty
}
