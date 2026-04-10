using UnityEngine;

public class GpsTracker : MonoBehaviour
{
    public static GpsTracker Instance { get; private set; }

    [Header("References")]
    public Transform busTransform;
    public CoordinateConverter converter;

    [Header("State (read-only)")]
    public double currentLat;
    public double currentLon;
    public float updateEverySeconds = 0.2f;

    float t;

    public System.Action<double, double> OnGpsUpdated;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        if (converter == null)
            converter = CoordinateConverter.Instance != null ? CoordinateConverter.Instance : FindFirstObjectByType<CoordinateConverter>();
        if (busTransform == null)
        {
            var bus = FindFirstObjectByType<BusController>();
            if (bus != null) busTransform = bus.transform;
        }
    }

    void Update()
    {
        if (converter == null || busTransform == null)
            return;

        t += Time.deltaTime;
        if (t < updateEverySeconds)
            return;
        t = 0f;

        var gps = converter.WorldToGeoPosition(busTransform.position);
        currentLat = gps.lat;
        currentLon = gps.lon;
        OnGpsUpdated?.Invoke(currentLat, currentLon);
    }
}

