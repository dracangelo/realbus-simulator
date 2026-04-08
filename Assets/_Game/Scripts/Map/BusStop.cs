using UnityEngine;

public class BusStop : MonoBehaviour
{
    [Header("Stop Info")]
    public string stopName = "Bus Stop";
    public double latitude;
    public double longitude;

    [Header("Visuals")]
    public GameObject stopMarker; // assign a cylinder or pole prefab later

    void Start()
    {
        // Place this stop at its real GPS position
        if (GPSManager.Instance != null)
        {
            Vector3 worldPos = GPSManager.Instance.GpsToWorld(latitude, longitude);
            worldPos.y = 0.5f; // slightly above ground
            transform.position = worldPos;
        }
    }

    void OnDrawGizmos()
    {
        // Show stop as yellow sphere in Scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 2f);
        Gizmos.color = Color.white;
    }
}