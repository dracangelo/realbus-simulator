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

    /// <summary>
    /// Automatic shift decision. A gear is wound out by road speed as well as by
    /// revs: leaving the shift to RPM alone meant a gear could be held past its
    /// rated top speed, so the gearbox's own limits never applied in play.
    /// <paramref name="speedKmh"/> is optional so existing callers that only have
    /// revs keep working.
    /// </summary>
    public bool UpdateAutomatic(float rpm, float maxRpm, float speedKmh = 0f)
    {
        if (!automatic || currentGear < 0 || gearRatios == null || gearRatios.Length == 0) return false;
        int previous = currentGear;
        float upshift = maxRpm * .8f, downshift = maxRpm * .35f;
        bool atGearSpeed = HasHigherGear() && speedKmh >= GetCurrentMaxSpeed() * .98f;
        // Do not drop a gear while the bus is still running near this gear's limit:
        // that would shift down into a gear it is already going too fast for.
        bool slowEnoughToDrop = speedKmh <= 0f || speedKmh < GetCurrentMaxSpeed() * .45f;
        if (atGearSpeed || rpm >= upshift || Mathf.Approximately(rpm, upshift)) ShiftUp();
        else if (slowEnoughToDrop && (rpm <= downshift || Mathf.Approximately(rpm, downshift))) ShiftDown();
        return previous != currentGear;
    }

    /// <summary>True while a higher forward gear exists to shift into.</summary>
    public bool HasHigherGear()
    {
        return currentGear >= 0 && gearRatios != null && currentGear < gearRatios.Length - 1;
    }

    public float GetCurrentRatio()
    {
        if (currentGear < 0) return reverseGearRatio;
        return gearRatios == null || gearRatios.Length == 0 ? 0f : gearRatios[Mathf.Clamp(currentGear, 0, gearRatios.Length - 1)];
    }

    /// <summary>
    /// Returns the max speed allowed in the current gear.
    /// </summary>
    public float GetCurrentMaxSpeed()
    {
        if (currentGear < 0) return 10f; // reverse max 10 km/h
        if (gearMaxSpeeds != null && currentGear < gearMaxSpeeds.Length)
            return gearMaxSpeeds[currentGear];
        return 95f;
    }

    /// <summary>
    /// How much of the driver's throttle this gear can still use at a road speed,
    /// in 0..1. The gear's rated top speed is a real limit, so a bus in first gear
    /// or in reverse cannot be wound out to the engine's rev limit. Below top gear an
    /// automatic gearbox shifts up instead of simply cutting power, and a manual one
    /// has to be shifted - so the cut only applies when no higher gear is available.
    /// </summary>
    public float ThrottleFactorAtSpeed(float speedKmh)
    {
        if (automatic && HasHigherGear()) return 1f;

        float limit = GetCurrentMaxSpeed();
        if (limit <= .1f) return 0f;
        // Full throttle up to ~92% of the gear's limit, then off over the last few
        // percent so the bus settles at the limit instead of bouncing against it.
        float fade = Mathf.Max(1f, limit * .08f);
        return Mathf.Clamp01((limit - Mathf.Abs(speedKmh)) / fade);
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
