using UnityEngine;

public class FreeDriveSession : MonoBehaviour
{
    public static FreeDriveSession Instance { get; private set; }

    [Header("Session State")]
    public bool sessionActive = false;
    public float distanceDrivenKm = 0f;
    public float sessionTimeSeconds = 0f;
    public float fuelLevel = 100f; // percentage

    [Header("Fuel Settings")]
    public float fuelConsumptionPer100km = 35f; // litres per 100km (diesel bus)
    public float fuelCapacityLitres = 200f;
    private float fuelLitres;

    private BusController busController;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager
            .GetActiveScene().name != "GameScene")
            return;
        busController = FindFirstObjectByType<BusController>();
        fuelLitres = fuelCapacityLitres;
        StartSession();
    }

    void Update()
    {
        if (!sessionActive) return;

        sessionTimeSeconds += Time.deltaTime;

        // Track distance
        if (busController != null)
        {
            float speedKmh = busController.currentSpeedKmh;
            float distanceDelta = (speedKmh / 3600f) * Time.deltaTime;
            distanceDrivenKm += distanceDelta;

            // Consume fuel
            float fuelUsed = (fuelConsumptionPer100km / 100f) * distanceDelta;
            fuelLitres = Mathf.Max(0f, fuelLitres - fuelUsed);
            fuelLevel = (fuelLitres / fuelCapacityLitres) * 100f;

            // Out of fuel
            if (fuelLitres <= 0f)
            {
                busController.throttleInput = 0f;
                Debug.Log("Out of fuel!");
            }
        }
    }

    public void StartSession()
    {
        sessionActive = true;
        distanceDrivenKm = 0f;
        sessionTimeSeconds = 0f;
        fuelLitres = fuelCapacityLitres;
        fuelLevel = 100f;
        Debug.Log("Free drive session started!");
    }

    public void EndSession()
    {
        sessionActive = false;
        Debug.Log($"Session ended — Distance: {distanceDrivenKm:F2} km, Time: {sessionTimeSeconds:F0}s");
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(sessionTimeSeconds / 60f);
        int seconds = Mathf.FloorToInt(sessionTimeSeconds % 60f);
        return $"{minutes:00}:{seconds:00}";
    }

    public string GetCurrentGPSString()
    {
        if (busController == null || GPSManager.Instance == null) return "0.0000, 0.0000";
        var (lat, lon) = GPSManager.Instance.WorldToGps(busController.transform.position);
        return $"{lat:F4}, {lon:F4}";
    }
}