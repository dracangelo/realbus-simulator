using UnityEngine;

[CreateAssetMenu(fileName = "TransmissionSystem", menuName = "RealBus/TransmissionSystem")]
public class TransmissionSystem : ScriptableObject
{
    public float[] gearRatios = { 3.5f, 2.0f, 1.35f, 1.0f, 0.82f, 0.68f };
    public float reverseGearRatio = -4.5f;
    public float differentialRatio = 4.1f;

    // Max speed (km/h) allowed in each gear — matches a real bus gearbox
    // Gear:       1      2      3      4      5      6
    public float[] gearMaxSpeeds = { 12f, 25f, 40f, 58f, 75f, 95f };

    public int currentGear = 0;

    public float GetCurrentRatio()
    {
        if (currentGear < 0) return reverseGearRatio;
        return gearRatios[currentGear];
    }

    /// <summary>
    /// Returns the max speed allowed in the current gear.
    /// BusController uses this to cut throttle when exceeded.
    /// </summary>
    public float GetCurrentMaxSpeed()
    {
        if (currentGear < 0) return 10f; // reverse max 10 km/h
        if (currentGear < gearMaxSpeeds.Length)
            return gearMaxSpeeds[currentGear];
        return 95f;
    }

    public void ShiftUp()
    {
        if (currentGear < gearRatios.Length - 1) currentGear++;
    }

    public void ShiftDown()
    {
        if (currentGear > 0) currentGear--;
    }
}