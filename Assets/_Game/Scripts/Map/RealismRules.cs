using System;

/// <summary>Deterministic realism rules shared by simulation and acceptance tests.</summary>
public static class RealismRules
{
    public static float DailyDensity(float minutes, float morning = 450f, float evening = 1050f, float width = 90f, float peak = 5f)
    {
        minutes = (minutes % 1440f + 1440f) % 1440f;
        double a = CircularDistance(minutes, morning) / Math.Max(1f, width);
        double b = CircularDistance(minutes, evening) / Math.Max(1f, width);
        return 1f + (Math.Max(1f, peak) - 1f) * (float)Math.Max(Math.Exp(-0.5 * a * a), Math.Exp(-0.5 * b * b));
    }
    static float CircularDistance(float a, float b) { float d = Math.Abs(a - b) % 1440f; return Math.Min(d, 1440f - d); }
    public static float LoadFactor(int passengers, int capacity) => 1f + 0.18f * Clamp01((float)passengers / Math.Max(1, capacity));
    public static float TyreGrip(float wear) => 1f - 0.28f * Clamp01((wear - 0.5f) / 0.5f);
    public static float Daylight(float minutes, float sunrise = 360f, float sunset = 1080f)
    {
        float phase = ((minutes % 1440f + 1440f) % 1440f - sunrise) / Math.Max(1f, sunset - sunrise);
        return phase <= 0f || phase >= 1f ? 0f : (float)Math.Sin(phase * Math.PI);
    }
    public static float StoppingSpeedKmh(float distance, float clearance, float deceleration) =>
        (float)Math.Sqrt(2f * Math.Max(0.1f, deceleration) * Math.Max(0f, distance - clearance)) * 3.6f;
    public static int TileTier(float distance, float near = 200f, float medium = 500f) => distance <= near ? 0 : distance <= medium ? 1 : 2;
    static float Clamp01(float value) => Math.Max(0f, Math.Min(1f, value));
}
