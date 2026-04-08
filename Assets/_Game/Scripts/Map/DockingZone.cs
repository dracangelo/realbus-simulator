using UnityEngine;
using System.Collections;

public class DockingZone : MonoBehaviour
{
    [Header("Settings")]
    public float dockingRadius = 12f;
    public string stopName;

    private BusController busController;
    public bool isDocked = false;

    void Start()
    {
        StartCoroutine(InitNextFrame());
    }

    IEnumerator InitNextFrame()
    {
        yield return null; // wait for BusStop.cs to position this GameObject
        busController = FindFirstObjectByType<BusController>();
        Debug.Log($"DockingZone '{stopName}' ready at position: {transform.position}");
    }

    void Update()
    {
        if (busController == null) return;

        float distance = Vector3.Distance(
            transform.position, busController.transform.position);

        bool wasDockedBefore = isDocked;
        isDocked = distance < dockingRadius
                && busController.currentSpeedKmh < 3f;

        if (isDocked && !wasDockedBefore) OnDocked();
        if (!isDocked && wasDockedBefore) OnUndocked();
    }

    void OnDocked()
    {
        Debug.Log($"Docked at: {stopName}");
        if (StopApproachUI.Instance != null)
            StopApproachUI.Instance.ShowDocked(stopName);
    }

    void OnUndocked()
    {
        Debug.Log($"Departed: {stopName}");
        if (StopApproachUI.Instance != null)
            StopApproachUI.Instance.HideApproach();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.4f);
        Gizmos.DrawWireSphere(transform.position, dockingRadius);
    }
}