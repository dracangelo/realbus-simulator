using UnityEngine;

public class PassengerSpawner : MonoBehaviour
{
    [Header("Density")]
    public int baselineSpawnMin = 2;
    public int baselineSpawnMax = 6;
    public float rushHourMultiplier = 5f;

    [Header("Rush Peaks (24h clock)")]
    public float morningPeakMinutes = 7.5f * 60f;   // 07:30
    public float eveningPeakMinutes = 17.5f * 60f;  // 17:30
    public float peakWidthMinutes = 90f;

    [Header("Stop Multipliers")]
    public float terminalMultiplier = 1.6f;
    public float normalStopMultiplier = 1f;

    public int GetSpawnCountForStop(BusStopData stop)
    {
        float density = GetDensityMultiplierForCurrentTime();
        float stopMult = IsLikelyTerminal(stop) ? terminalMultiplier : normalStopMultiplier;
        float combined = Mathf.Max(0.1f, density * stopMult);

        int min = Mathf.Max(0, Mathf.RoundToInt(baselineSpawnMin * combined));
        int max = Mathf.Max(min + 1, Mathf.RoundToInt(baselineSpawnMax * combined));
        return Random.Range(min, max + 1);
    }

    public float GetDensityMultiplierForCurrentTime()
    {
        float now = ScheduleManager.Instance != null
            ? ScheduleManager.Instance.currentTimeMinutes
            : 12f * 60f;

        float morning = Gaussian(now, morningPeakMinutes, peakWidthMinutes);
        float evening = Gaussian(now, eveningPeakMinutes, peakWidthMinutes);
        float rushSignal = Mathf.Clamp01(Mathf.Max(morning, evening));

        // Requirement: rush hour peaks to ~5x baseline.
        return Mathf.Lerp(1f, rushHourMultiplier, rushSignal);
    }

    static float Gaussian(float x, float mean, float sigma)
    {
        if (sigma <= 0.001f) return 0f;
        float d = (x - mean) / sigma;
        return Mathf.Exp(-0.5f * d * d);
    }

    static bool IsLikelyTerminal(BusStopData stop)
    {
        if (stop == null || string.IsNullOrEmpty(stop.stopName)) return false;
        string s = stop.stopName.ToLowerInvariant();
        return s.Contains("terminal") || s.Contains("depot") || s.Contains("station");
    }
}
