using TMPro;
using UnityEngine;

public class ScheduleDebugOverlay : MonoBehaviour
{
    public TextMeshProUGUI overlayText;
    public KeyCode toggleKey = KeyCode.F3;
    public bool visible;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
            visible = !visible;

        if (overlayText == null)
            return;

        overlayText.gameObject.SetActive(visible);
        if (!visible)
            return;

        var mission = MissionManager.Instance;
        var schedule = ScheduleManager.Instance;
        if (mission == null || schedule == null || mission.currentRoute == null || mission.currentRoute.stops == null)
        {
            overlayText.text = "Schedule Debug: no active route";
            return;
        }

        string text = $"Schedule Debug  {schedule.currentTimeString}\n";
        for (int i = 0; i < mission.currentRoute.stops.Length; i++)
        {
            float scheduled = schedule.GetScheduledArrival(i);
            string scheduledLabel = FormatMinutes(scheduled);
            string actualLabel = schedule.HasActualArrival(i) ? FormatMinutes(schedule.GetActualArrival(i)) : "--:--";
            string status = schedule.HasActualArrival(i) ? schedule.GetStatusLabel(i) : "Pending";
            string delta = schedule.HasActualArrival(i) ? $"{schedule.GetArrivalDeltaSeconds(i):+0;-0;0}s" : "--";
            string penalty = schedule.HasActualArrival(i) ? $"{schedule.GetLatePenaltyPercent(i):0}%" : "--";
            text += $"{i + 1}. {mission.currentRoute.stops[i].stopName} | ETA {scheduledLabel} | ACT {actualLabel} | Δ {delta} | {status} | Pen {penalty}\n";
        }

        overlayText.text = text.TrimEnd();
    }

    string FormatMinutes(float minutes)
    {
        int hours24 = Mathf.FloorToInt(minutes / 60f) % 24;
        int mins = Mathf.FloorToInt(minutes % 60f);
        return $"{hours24:00}:{mins:00}";
    }
}
