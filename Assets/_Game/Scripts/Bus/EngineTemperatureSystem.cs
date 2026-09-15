using UnityEngine;

[DisallowMultipleComponent]
public class EngineTemperatureSystem : MonoBehaviour
{
    public const float WarningTemperatureC = 108f;
    public BusController bus;
    [Range(60f, 125f)] public float currentTemperatureC = 78f;
    public float ambientTemperatureC = 24f;
    public float heatingRate = 7f;
    public float coolingRate = 3f;

    public float NormalizedTemperature => Mathf.InverseLerp(60f, 120f, currentTemperatureC);
    public bool WarningActive => currentTemperatureC >= WarningTemperatureC;

    void Awake()
    {
        if (bus == null) bus = GetComponent<BusController>();
    }

    void Update()
    {
        if (bus == null) return;
        float rpmLoad = Mathf.InverseLerp(700f, 2600f, bus.currentRPM);
        float cooling = Mathf.InverseLerp(0f, 65f, bus.currentSpeedKmh);
        float target = Mathf.Lerp(76f, 116f, rpmLoad) + Mathf.Max(0f, ambientTemperatureC - 24f) * 0.3f;
        float rate = target > currentTemperatureC ? heatingRate : coolingRate * Mathf.Lerp(0.7f, 1.8f, cooling);
        currentTemperatureC = Mathf.MoveTowards(currentTemperatureC, target, rate * Time.deltaTime);
    }
}
