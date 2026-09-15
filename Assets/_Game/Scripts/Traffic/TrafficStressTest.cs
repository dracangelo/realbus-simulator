using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

/// <summary>On-device benchmark. Measures actual tiers, frame-time percentiles, and exports CSV.</summary>
public class TrafficStressTest : MonoBehaviour
{
    public VehiclePool vehiclePool;
    public bool autoStart = true;
    public int startVehicles = 10, stepVehicles = 5, maxVehicles = 50;
    public float settleSecondsPerStep = 12f;
    public int sampleFrames = 300;
    public List<int> vehicleCounts = new List<int>();
    public List<float> avgFps = new List<float>();
    public int detectedCliffVehicleCount = -1;
    public float cliffThresholdPercentDrop = 20f;
    public bool Running { get; private set; }
    public string LastReportPath { get; private set; }
    void Start() { if (autoStart) Begin(); }
    public void Begin() { if (!Running && vehiclePool != null) StartCoroutine(RunBenchmark()); }
    IEnumerator RunBenchmark()
    {
        Running = true;
        while (vehiclePool.GetPoolCount() == 0) yield return null;
        vehicleCounts.Clear(); avgFps.Clear(); detectedCliffVehicleCount = -1;
        var csv = new System.Text.StringBuilder("device,requested,full,spline,dormant,avg_fps,p95_ms,passengers,weather\n");
        float previousFps = 0f;
        int limit = Mathf.Min(maxVehicles, vehiclePool.GetPoolCount());
        for (int count = Mathf.Clamp(startVehicles, 1, limit); count <= limit; count += Mathf.Max(1, stepVehicles))
        {
            vehiclePool.SetForcedActiveCount(count);
            yield return new WaitForSeconds(Mathf.Max(0f, settleSecondsPerStep));
            var samples = new List<float>(); float seconds = 0f;
            for (int i = 0; i < Mathf.Max(1, sampleFrames); i++)
            {
                yield return null;
                if (Time.timeScale <= 0f) { i--; continue; }
                seconds += Time.unscaledDeltaTime; samples.Add(Time.unscaledDeltaTime);
            }
            samples.Sort();
            float fps = samples.Count / Mathf.Max(0.001f, seconds);
            float p95 = samples[Mathf.Min(samples.Count - 1, Mathf.FloorToInt(samples.Count * 0.95f))] * 1000f;
            vehiclePool.GetTierCounts(out int full, out int spline, out int dormant, out _);
            vehicleCounts.Add(full + spline); avgFps.Add(fps);
            csv.AppendLine(string.Format(CultureInfo.InvariantCulture, "\"{0}\",{1},{2},{3},{4},{5:F2},{6:F2},{7},{8}", SystemInfo.deviceModel.Replace("\"", "\"\""), count, full, spline, dormant, fps, p95, PassengerManager.Instance != null ? PassengerManager.Instance.currentPassengers : 0, WeatherSystem.Instance != null ? WeatherSystem.Instance.CurrentCompositeWeatherLabel : "None"));
            if (detectedCliffVehicleCount < 0 && (fps < 30f || previousFps > 0f && fps < previousFps * (1f - cliffThresholdPercentDrop / 100f)))
                detectedCliffVehicleCount = full + spline;
            previousFps = fps;
        }
        LastReportPath = Path.Combine(Application.persistentDataPath, "traffic-stress-" + System.DateTime.UtcNow.ToString("yyyyMMdd-HHmmss") + ".csv");
        File.WriteAllText(LastReportPath, csv.ToString());
        vehiclePool.SetForcedActiveCount(-1); Running = false;
        Debug.Log("Traffic benchmark saved: " + LastReportPath);
    }
    void OnDisable() { StopAllCoroutines(); if (Running && vehiclePool != null) vehiclePool.SetForcedActiveCount(-1); Running = false; }
}
