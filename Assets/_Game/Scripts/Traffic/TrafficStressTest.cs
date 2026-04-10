using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrafficStressTest : MonoBehaviour
{
    [Header("References")]
    public VehiclePool vehiclePool;

    [Header("Test")]
    public int startVehicles = 10;
    public int stepVehicles = 10;
    public int maxVehicles = 200;
    public float settleSecondsPerStep = 12f;
    public int sampleFrames = 300;

    [Header("Output")]
    public List<int> vehicleCounts = new List<int>();
    public List<float> avgFps = new List<float>();
    public int detectedCliffVehicleCount = -1;
    public float cliffThresholdPercentDrop = 20f;

    bool running;

    void Start()
    {
        if (!running)
            StartCoroutine(RunBenchmark());
    }

    IEnumerator RunBenchmark()
    {
        running = true;
        vehicleCounts.Clear();
        avgFps.Clear();
        detectedCliffVehicleCount = -1;

        float prevFps = -1f;
        for (int count = startVehicles; count <= maxVehicles; count += stepVehicles)
        {
            ApplyVehicleCount(count);
            yield return new WaitForSeconds(settleSecondsPerStep);

            float fps = 0f;
            yield return StartCoroutine(SampleAverageFps(sampleFrames, v => fps = v));
            vehicleCounts.Add(count);
            avgFps.Add(fps);
            Debug.Log($"TrafficStressTest: vehicles={count}, avgFPS={fps:0.0}");

            if (prevFps > 0f)
            {
                float drop = (prevFps - fps) / Mathf.Max(1f, prevFps);
                if (drop >= cliffThresholdPercentDrop * 0.01f && detectedCliffVehicleCount < 0)
                    detectedCliffVehicleCount = count;
            }
            prevFps = fps;
        }

        int maxBudget = detectedCliffVehicleCount > 0 ? Mathf.Max(startVehicles, detectedCliffVehicleCount - stepVehicles) : maxVehicles;
        Debug.Log($"TrafficStressTest: Estimated max safe AI traffic budget = {maxBudget}");
        running = false;
    }

    IEnumerator SampleAverageFps(int frames, System.Action<float> onDone)
    {
        float acc = 0f;
        int used = 0;
        for (int i = 0; i < frames; i++)
        {
            yield return null;
            if (Time.unscaledDeltaTime <= 0f) continue;
            acc += 1f / Time.unscaledDeltaTime;
            used++;
        }
        float avg = used > 0 ? acc / used : 0f;
        onDone?.Invoke(avg);
    }

    void ApplyVehicleCount(int target)
    {
        if (vehiclePool == null) return;
        int clamped = Mathf.Clamp(target, 1, Mathf.Max(1, vehiclePool.GetPoolCount()));
        vehiclePool.SetForcedActiveCount(clamped);
    }
}
