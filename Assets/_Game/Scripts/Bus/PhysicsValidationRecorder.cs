using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>Records raw measurements; thresholds remain acceptance tests, never assumed passes.</summary>
[RequireComponent(typeof(BusController))]
public class PhysicsValidationRecorder : MonoBehaviour
{
    BusController bus;
    float elapsed, time50 = -1f, time80 = -1f, topSpeed, stopDistance;
    bool running, stopping;
    Vector3 previous;
    StreamWriter writer;
    string result = "T: start recording; Y: finish/export. Load: Inspector testPassengerCount.";

    void Start() { bus = GetComponent<BusController>(); }
    void Update()
    {
        if (Keyboard.current == null) return;
        if (Keyboard.current.tKey.wasPressedThisFrame) BeginRecording();
        if (Keyboard.current.yKey.wasPressedThisFrame) EndRecording();
    }
    public void BeginRecording()
    {
        EndRecording();
        elapsed = topSpeed = stopDistance = 0f;
        time50 = time80 = -1f;
        stopping = false;
        string path = Path.Combine(Application.persistentDataPath, "phase2-" + System.DateTime.UtcNow.ToString("yyyyMMdd-HHmmss-fff") + ".csv");
        try
        {
            writer = new StreamWriter(path);
            writer.WriteLine("seconds,speed_kmh,rpm,gear,x,y,z,roll_deg,pitch_deg,mass_kg,brake,parking");
            running = true;
            result = "Recording: " + path;
        }
        catch (IOException error) { result = error.Message; }
    }
    void FixedUpdate()
    {
        if (!running || bus.Rigidbody == null) return;
        elapsed += Time.fixedDeltaTime;
        float speed = bus.currentSpeedKmh;
        if (time50 < 0f && speed >= 50f) time50 = elapsed;
        if (time80 < 0f && speed >= 80f) time80 = elapsed;
        topSpeed = Mathf.Max(topSpeed, speed);
        if (!stopping && bus.brakeInput > 0.9f && speed > 45f && speed < 55f)
        { stopping = true; stopDistance = 0f; previous = transform.position; }
        if (stopping)
        {
            stopDistance += Vector3.Distance(previous, transform.position);
            previous = transform.position;
            if (speed < 0.1f) stopping = false;
        }
        Vector3 p = transform.position;
        writer.WriteLine(string.Format(CultureInfo.InvariantCulture,
            "{0:F3},{1:F3},{2:F1},{3},{4:F3},{5:F3},{6:F3},{7:F3},{8:F3},{9:F1},{10:F2},{11}",
            elapsed, speed, bus.currentRPM, bus.transmissionData.currentGear, p.x, p.y, p.z,
            Mathf.DeltaAngle(0f, transform.eulerAngles.z), Mathf.DeltaAngle(0f, transform.eulerAngles.x),
            bus.Rigidbody.mass, bus.brakeInput, bus.ParkingBrakeActive));
    }
    public void EndRecording()
    {
        writer?.Dispose(); writer = null;
        if (running)
        {
            result = $"0–50: {time50:F2}s; 0–80: {time80:F2}s; peak: {topSpeed:F2}km/h; stop: {stopDistance:F2}m";
            Debug.Log(result);
        }
        running = false;
    }
    void OnDisable() { EndRecording(); }
    void OnApplicationPause(bool paused) { if (paused) EndRecording(); }
    void OnGUI()
    {
        GUI.Label(new Rect(20, 15, Screen.width - 40, 60), result);
        if (bus != null) GUI.Label(new Rect(20, 70, 800, 30), $"{bus.currentSpeedKmh:F1} km/h | {bus.currentRPM:F0} RPM | gear {bus.transmissionData.currentGear + 1} | parking {bus.ParkingBrakeActive}");
        if (GUI.Button(new Rect(20, 105, 150, 40), "Start recording")) BeginRecording();
        if (GUI.Button(new Rect(180, 105, 150, 40), "Finish recording")) EndRecording();
    }
}
