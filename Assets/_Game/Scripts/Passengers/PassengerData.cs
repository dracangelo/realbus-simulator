using UnityEngine;

[CreateAssetMenu(fileName = "PassengerData", menuName = "RealBus/PassengerData")]
public class PassengerData : ScriptableObject
{
    [Header("Capacity")]
    public int seatedCapacity = 49;
    public int standingCapacity = 20;
    public int totalCapacity => seatedCapacity + standingCapacity;

    [Header("Boarding")]
    public float boardingTimePerPassenger = 1.5f; // seconds
    public float alightingTimePerPassenger = 1.2f; // seconds

    [Header("Fares (KES)")]
    public float baseFare = 50f;
    public float peakMultiplier = 1.5f; // morning/evening rush
}