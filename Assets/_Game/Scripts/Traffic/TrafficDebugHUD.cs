using UnityEngine;

public class TrafficDebugHUD : MonoBehaviour
{
    public VehiclePool vehiclePool;
    public bool visible = true;
    public KeyCode toggleKey = KeyCode.F8;
    public Rect panelRect = new Rect(12f, 12f, 360f, 140f);
    public bool showLegend = true;
    public Rect legendRect = new Rect(12f, 158f, 360f, 86f);

    float smoothedFps = 60f;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
            visible = !visible;

        float fps = Time.unscaledDeltaTime > 0f ? (1f / Time.unscaledDeltaTime) : smoothedFps;
        smoothedFps = Mathf.Lerp(smoothedFps, fps, 0.08f);
    }

    void OnGUI()
    {
        if (!visible) return;

        GUI.color = new Color(0f, 0f, 0f, 0.7f);
        GUI.Box(panelRect, GUIContent.none);
        GUI.color = Color.white;

        GUILayout.BeginArea(panelRect);
        GUILayout.Space(8f);
        GUILayout.Label("Traffic Debug HUD");
        GUILayout.Label($"FPS: {smoothedFps:0.0}");

        if (vehiclePool == null)
            vehiclePool = FindFirstObjectByType<VehiclePool>();

        if (vehiclePool != null)
        {
            vehiclePool.GetTierCounts(out int full, out int spline, out int dormant, out int densityEnabled);
            GUILayout.Label($"Full AI: {full} / {vehiclePool.maxFullAiVehicles}");
            GUILayout.Label($"Spline: {spline}");
            GUILayout.Label($"Dormant: {dormant}");
            GUILayout.Label($"Density Active: {densityEnabled} / {vehiclePool.GetPoolCount()}");
        }
        else
        {
            GUILayout.Label("VehiclePool: missing");
        }

        GUILayout.EndArea();

        if (showLegend && vehiclePool != null)
            DrawLegend(vehiclePool);
    }

    void DrawLegend(VehiclePool pool)
    {
        GUI.color = new Color(0f, 0f, 0f, 0.65f);
        GUI.Box(legendRect, GUIContent.none);
        GUI.color = Color.white;

        float x = legendRect.x + 12f;
        float y = legendRect.y + 14f;
        DrawLegendRow(new Rect(x, y, legendRect.width - 24f, 20f), pool.fullAiGizmoColor, "Full AI (physics)");
        DrawLegendRow(new Rect(x, y + 22f, legendRect.width - 24f, 20f), pool.splineGizmoColor, "Spline (cheap)");
        DrawLegendRow(new Rect(x, y + 44f, legendRect.width - 24f, 20f), pool.dormantGizmoColor, "Dormant");
    }

    void DrawLegendRow(Rect rowRect, Color color, string label)
    {
        var prev = GUI.color;
        GUI.color = color;
        GUI.DrawTexture(new Rect(rowRect.x, rowRect.y + 3f, 14f, 14f), Texture2D.whiteTexture);
        GUI.color = prev;
        GUI.Label(new Rect(rowRect.x + 22f, rowRect.y, rowRect.width - 22f, rowRect.height), label);
    }
}
