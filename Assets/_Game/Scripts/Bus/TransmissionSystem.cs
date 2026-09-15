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
    public bool automatic = true;

    public bool UpdateAutomatic(float rpm, float maxRpm)
    {
        if (!automatic || currentGear < 0 || gearRatios == null || gearRatios.Length == 0) return false;
        int previous = currentGear;
        if (rpm >= maxRpm * 0.8f) ShiftUp();
        else if (rpm <= maxRpm * 0.35f) ShiftDown();
        return previous != currentGear;
    }

    public float GetCurrentRatio()
    {
        if (currentGear < 0) return reverseGearRatio;
        return gearRatios == null || gearRatios.Length == 0 ? 0f : gearRatios[Mathf.Clamp(currentGear, 0, gearRatios.Length - 1)];
    }

    /// <summary>
    /// Returns the max speed allowed in the current gear.
    /// BusController uses this to cut throttle when exceeded.
    /// </summary>
    public float GetCurrentMaxSpeed()
    {
        if (currentGear < 0) return 10f; // reverse max 10 km/h
        if (gearMaxSpeeds != null && currentGear < gearMaxSpeeds.Length)
            return gearMaxSpeeds[currentGear];
        return 95f;
    }

    public void ShiftUp()
    {
        if (gearRatios != null && currentGear < gearRatios.Length - 1) currentGear++;
    }

    public void ShiftDown()
    {
        if (currentGear > 0) currentGear--;
    }
}