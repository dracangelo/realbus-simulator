using UnityEngine;
using System.Collections.Generic;

public class ScheduleManager : MonoBehaviour
{
    public static ScheduleManager Instance { get; private set; }

    [Header("Schedule Settings")]
    public float gameStartTimeMinutes = 480f; // 08:00
    public float secondsPerGameMinute = 60f;   // 1 real minute = 1 game minute

    [Header("State")]
    public float currentTimeMinutes = 480f;
    public string currentTimeString = "08:00";

    // Expected arrival time at each stop (in minutes from midnight)
    private Dictionary<int, float> scheduledArrivals = new Dictionary<int, float>();
    private Dictionary<int, float> actualArrivals = new Dictionary<int, float>();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        currentTimeMinutes = gameStartTimeMinutes;
    }

    void Update()
    {
        // Advance game clock
        currentTimeMinutes += (Time.deltaTime / secondsPerGameMinute);

        // Format time string
        int hours24 = Mathf.FloorToInt(currentTimeMinutes / 60f) % 24;
        int mins = Mathf.FloorToInt(currentTimeMinutes % 60f);
        string ampm = hours24 >= 12 ? "PM" : "AM";
        int hours12 = hours24 % 12;
        if (hours12 == 0) hours12 = 12;
        currentTimeString = $"{hours12:00}:{mins:00} {ampm}";
    }

    /// <summary>
    /// Set up schedule for a route — evenly space stops over target duration.
    /// </summary>
    public void SetupSchedule(BusRoute route, float departureMins, float durationMins)
    {
        scheduledArrivals.Clear();
        actualArrivals.Clear();

        if (route == null || route.stops == null || route.stops.Length == 0)
            return;

        currentTimeMinutes = departureMins;
        float interval = route.stops.Length > 1
            ? durationMins / (route.stops.Length - 1)
            : 0f;
        for (int i = 0; i < route.stops.Length; i++)
            scheduledArrivals[i] = departureMins + (i * interval);

        Debug.Log($"Schedule set: {route.stops.Length} stops over {durationMins} mins");
    }

    /// <summary>
    /// Call when bus arrives at a stop. Returns punctuality status.
    /// </summary>
    public PunctualityStatus RecordArrival(int stopIndex)
    {
        actualArrivals[stopIndex] = currentTimeMinutes;

        if (!scheduledArrivals.ContainsKey(stopIndex))
            return PunctualityStatus.OnTime;

        float diff = actualArrivals[stopIndex] - scheduledArrivals[stopIndex];

        PunctualityStatus status;
        if (diff < -1f) status = PunctualityStatus.Early;
        else if (diff <= 1f) status = PunctualityStatus.OnTime;
        else if (diff <= 3f) status = PunctualityStatus.Late;
        else status = PunctualityStatus.SeverelyLate;

        Debug.Log($"Stop {stopIndex}: {status} (diff: {diff:+0.0;-0.0} min)");
        return status;
    }

    public float GetScheduledArrival(int stopIndex)
    {
        return scheduledArrivals.ContainsKey(stopIndex) ? scheduledArrivals[stopIndex] : 0f;
    }

    public float GetActualArrival(int stopIndex)
    {
        return actualArrivals.ContainsKey(stopIndex) ? actualArrivals[stopIndex] : 0f;
    }

    public bool HasActualArrival(int stopIndex)
    {
        return actualArrivals.ContainsKey(stopIndex);
    }

    public float GetArrivalDeltaMinutes(int stopIndex)
    {
        if (!scheduledArrivals.ContainsKey(stopIndex) || !actualArrivals.ContainsKey(stopIndex))
            return 0f;

        return actualArrivals[stopIndex] - scheduledArrivals[stopIndex];
    }

    public float GetArrivalDeltaSeconds(int stopIndex)
    {
        return GetArrivalDeltaMinutes(stopIndex) * 60f;
    }

    public float GetLatePenaltyPercent(int stopIndex)
    {
        float lateSeconds = Mathf.Max(0f, GetArrivalDeltaSeconds(stopIndex));
        return Mathf.Floor(lateSeconds / 30f) * 2f;
    }

    public float GetEarlyWaitSecondsRequired(int stopIndex)
    {
        if (!scheduledArrivals.ContainsKey(stopIndex))
            return 0f;

        float minutesEarly = scheduledArrivals[stopIndex] - currentTimeMinutes;
        return Mathf.Max(0f, minutesEarly * secondsPerGameMinute);
    }

    public string GetStatusLabel(int stopIndex)
    {
        if (!HasActualArrival(stopIndex))
            return "Pending";

        switch (RecordArrivalPreview(stopIndex))
        {
            case PunctualityStatus.Early: return "Early";
            case PunctualityStatus.Late: return "Late";
            case PunctualityStatus.SeverelyLate: return "Severely Late";
            default: return "On Time";
        }
    }

    PunctualityStatus RecordArrivalPreview(int stopIndex)
    {
        if (!scheduledArrivals.ContainsKey(stopIndex) || !actualArrivals.ContainsKey(stopIndex))
            return PunctualityStatus.OnTime;

        float diff = actualArrivals[stopIndex] - scheduledArrivals[stopIndex];
        if (diff < -1f) return PunctualityStatus.Early;
        if (diff <= 1f) return PunctualityStatus.OnTime;
        if (diff <= 3f) return PunctualityStatus.Late;
        return PunctualityStatus.SeverelyLate;
    }
}

public enum PunctualityStatus
{
    OnTime,
    Early,
    Late,
    SeverelyLate
}
