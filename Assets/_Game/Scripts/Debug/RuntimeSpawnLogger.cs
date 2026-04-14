using System.Collections;
using UnityEngine;

public class RuntimeSpawnLogger : MonoBehaviour
{
    [Header("Timing")]
    public float initialDelaySeconds = 2f;
    public float repeatIntervalSeconds = 8f;

    [Header("Logging")]
    public bool logContinuously = false;

    void Start()
    {
        StartCoroutine(LogRoutine());
    }

    IEnumerator LogRoutine()
    {
        if (initialDelaySeconds > 0f)
            yield return new WaitForSeconds(initialDelaySeconds);

        do
        {
            LogSnapshot();

            if (!logContinuously)
                yield break;

            if (repeatIntervalSeconds <= 0f)
                yield break;

            yield return new WaitForSeconds(repeatIntervalSeconds);
        }
        while (true);
    }

    void LogSnapshot()
    {
        var mission = FindFirstObjectByType<MissionManager>();
        var route = mission != null ? mission.currentRoute : null;
        int routeStops = route != null && route.stops != null ? route.stops.Length : 0;

        var passengerManager = FindFirstObjectByType<PassengerManager>();
        var passengerSpawner = FindFirstObjectByType<PassengerSpawner>();
        var roadGraph = FindFirstObjectByType<AIRoadGraph>();
        var vehiclePool = FindFirstObjectByType<VehiclePool>();
        var pedestrianSpawner = FindFirstObjectByType<PedestrianSpawner>();

        string trafficSummary = "n/a";
        if (vehiclePool != null)
        {
            vehiclePool.GetTierCounts(out int full, out int spline, out int dormant, out int densityEnabled);
            trafficSummary = $"full={full}/{vehiclePool.maxFullAiVehicles}, spline={spline}, dormant={dormant}, density={densityEnabled}/{vehiclePool.GetPoolCount()}";
        }

        Debug.Log(
            "[RuntimeSpawnLogger] " +
            $"routeStops={routeStops}, " +
            $"passengerManager={(passengerManager != null ? "ok" : "missing")}, " +
            $"passengerSpawner={(passengerSpawner != null ? "ok" : "missing")}, " +
            $"pedestrianSpawner={(pedestrianSpawner != null ? "ok" : "missing")}, " +
            $"roadGraphNodes={(roadGraph != null && roadGraph.nodes != null ? roadGraph.nodes.Length : 0)}, " +
            $"traffic=({trafficSummary})");
    }
}
