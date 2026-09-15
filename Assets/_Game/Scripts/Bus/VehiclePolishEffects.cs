using UnityEngine;

[DisallowMultipleComponent]
public class VehiclePolishEffects : MonoBehaviour
{
    public BusController bus;
    public ParticleSystem exhaust, roadDust, rainSpray;

    void Awake()
    {
        if (bus == null) bus = GetComponent<BusController>();
        if (exhaust == null) exhaust = CreateEffect("Exhaust", new Vector3(0f, 0.65f, -5.5f), new Color(0.25f, 0.25f, 0.25f, 0.35f));
        if (roadDust == null) roadDust = CreateEffect("Road Dust", new Vector3(0f, 0.35f, -3.2f), new Color(0.55f, 0.46f, 0.34f, 0.35f));
        if (rainSpray == null) rainSpray = CreateEffect("Rain Spray", new Vector3(0f, 0.25f, -3.2f), new Color(0.65f, 0.78f, 0.86f, 0.32f));
    }

    void Update()
    {
        if (bus == null) return;
        SetRate(exhaust, Mathf.Lerp(2f, 22f, Mathf.Clamp01(bus.throttleInput)));
        RoadSurfaceDetector detector = bus.GetComponent<RoadSurfaceDetector>();
        bool dirt = detector != null && detector.perWheelRoadTypes != null && detector.perWheelRoadTypes.Length > 0 && detector.perWheelRoadTypes[0] == RoadSurfaceDetector.RoadType.Dirt;
        float movement = Mathf.InverseLerp(4f, 45f, bus.currentSpeedKmh);
        float rain = WeatherSystem.Instance != null ? WeatherSystem.Instance.weatherIntensity : 0f;
        SetRate(roadDust, dirt ? movement * 35f * (1f - rain) : 0f);
        SetRate(rainSpray, movement * rain * 48f);
    }

    ParticleSystem CreateEffect(string label, Vector3 localPosition, Color tint)
    {
        GameObject o = new GameObject(label); o.transform.SetParent(transform, false); o.transform.localPosition = localPosition;
        ParticleSystem p = o.AddComponent<ParticleSystem>();
        var main = p.main; main.startLifetime = 0.8f; main.startSpeed = 1.5f; main.startSize = 0.18f; main.startColor = tint; main.maxParticles = 80; main.simulationSpace = ParticleSystemSimulationSpace.World;
        var emission = p.emission; emission.rateOverTime = 0f;
        var shape = p.shape; shape.shapeType = ParticleSystemShapeType.Cone; shape.angle = 18f; shape.radius = 0.3f;
        return p;
    }

    static void SetRate(ParticleSystem p, float rate) { if (p == null) return; var emission = p.emission; emission.rateOverTime = rate; }
}
