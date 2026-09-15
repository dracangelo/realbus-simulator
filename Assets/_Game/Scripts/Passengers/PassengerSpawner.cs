using UnityEngine;
using System.Collections.Generic;

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

    [Header("Optional Random Stop Generation")]
    public AIRoadGraph roadGraph;
    public CoordinateConverter coordinateConverter;
    public int generatedStopCount = 8;
    public float generatedStopWaitTimeSeconds = 10f;
    public string generatedStopNamePrefix = "Generated Stop";

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

        return RealismRules.DailyDensity(now, morningPeakMinutes, eveningPeakMinutes, peakWidthMinutes, rushHourMultiplier);
    }

    public BusStopData[] BuildRandomStopsFromRoadGraph(int desiredCount = -1, int startNodeIndex = -1)
    {
        var graph = roadGraph != null ? roadGraph : FindFirstObjectByType<AIRoadGraph>();
        if (graph == null || graph.NodeCount == 0)
        {
            Debug.LogWarning("[PassengerSpawner] Cannot generate stops because AIRoadGraph is missing or empty.");
            return System.Array.Empty<BusStopData>();
        }

        var converter = coordinateConverter != null ? coordinateConverter : CoordinateConverter.Instance;
        if (converter == null)
            converter = FindFirstObjectByType<CoordinateConverter>();

        if (converter == null)
        {
            Debug.LogWarning("[PassengerSpawner] Cannot generate stops because CoordinateConverter is missing.");
            return System.Array.Empty<BusStopData>();
        }

        int count = Mathf.Max(2, desiredCount > 0 ? desiredCount : generatedStopCount);
        int[] nodeSequence = graph.BuildRandomNodeSequence(count, startNodeIndex);
        if (nodeSequence == null || nodeSequence.Length < 2)
        {
            Debug.LogWarning("[PassengerSpawner] AIRoadGraph could not provide a valid random node sequence for passenger stops.");
            return System.Array.Empty<BusStopData>();
        }

        var result = new List<BusStopData>(nodeSequence.Length);
        for (int i = 0; i < nodeSequence.Length; i++)
        {
            if (!graph.IsValidNode(nodeSequence[i]))
                continue;

            var world = graph.GetNodePosition(nodeSequence[i]);
            var gps = converter.WorldToGeoPosition(world);

            result.Add(new BusStopData
            {
                stopName = $"{generatedStopNamePrefix} {nodeSequence[i]:0000}",
                latitude = gps.lat,
                longitude = gps.lon,
                waitTimeSeconds = generatedStopWaitTimeSeconds
            });
        }

        return result.ToArray();
    }

    static bool IsLikelyTerminal(BusStopData stop)
    {
        if (stop == null || string.IsNullOrEmpty(stop.stopName)) return false;
        string s = stop.stopName.ToLowerInvariant();
        return s.Contains("terminal") || s.Contains("depot") || s.Contains("station");
    }
}
