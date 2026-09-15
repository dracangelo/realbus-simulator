using System;

/// <summary>Deterministic Phase 4 rules; independent of Unity frame timing.</summary>
public static class GameplayRules
{
    public static float LatePenaltyPercent(float deltaSeconds) =>
        Math.Min(100f, (float)Math.Floor(Math.Max(0f, deltaSeconds - 60f) / 30f) * 2f);

    public static float WeightedScore(float punctuality, float satisfaction, float safety, float efficiency) =>
        Clamp(punctuality) * 0.30f + Clamp(satisfaction) * 0.30f + Clamp(safety) * 0.25f + Clamp(efficiency) * 0.15f;

    public static int BaseXP(int difficulty) => Math.Max(1, Math.Min(5, difficulty)) * 200;
    public static int EarnedXP(int baseXp, float score, float multiplier) =>
        (int)Math.Round(Math.Max(0, baseXp) * Clamp(score) / 100f * Math.Max(0f, multiplier), MidpointRounding.AwayFromZero);

    public static bool IsDocked(float kerbOffset, float signOffset, float headingError, float speed,
        float kerbTolerance = 0.5f, float signTolerance = 3f, float headingTolerance = 25f, float maxSpeed = 1.5f) =>
        Math.Abs(kerbOffset) <= kerbTolerance && Math.Abs(signOffset) <= signTolerance &&
        Math.Abs(headingError) <= headingTolerance && Math.Abs(speed) <= maxSpeed;

    public static bool CrossedStopLine(float previousX, float previousZ, float currentX, float currentZ, float width)
    {
        if (previousZ >= 0f || currentZ < 0f) return false;
        float t = -previousZ / (currentZ - previousZ);
        return Math.Abs(previousX + (currentX - previousX) * t) <= width * 0.5f;
    }
    static float Clamp(float value) => float.IsNaN(value) ? 0f : Math.Max(0f, Math.Min(100f, value));
}
