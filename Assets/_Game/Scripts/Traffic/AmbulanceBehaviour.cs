using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AIVehicleController))]
public class AmbulanceBehaviour : MonoBehaviour
{
    [Header("Emergency")]
    public bool sirenActive = true;
    public float influenceRadius = 45f;
    public float notifyInterval = 0.4f;
    public float pullOverDuration = 2.2f;

    [Header("Audio/Visual")]
    public AudioSource sirenSource;
    public Light[] emergencyLights;

    AIVehicleController ai;
    float t;
    bool flashState;
    Coroutine notifyRoutine;

    void Awake()
    {
        ai = GetComponent<AIVehicleController>();
        ai.vehicleType = AIVehicleController.VehicleType.Emergency;
    }

    void Update()
    {
        if (sirenSource != null)
        {
            if (sirenActive && !sirenSource.isPlaying) sirenSource.Play();
            if (!sirenActive && sirenSource.isPlaying) sirenSource.Stop();
        }

        // Simple flash toggle.
        if (emergencyLights != null && emergencyLights.Length > 0)
        {
            t += Time.deltaTime * 10f;
            if (t >= 1f)
            {
                t = 0f;
                flashState = !flashState;
                for (int i = 0; i < emergencyLights.Length; i++)
                    if (emergencyLights[i] != null)
                        emergencyLights[i].enabled = flashState;
            }
        }
    }

    void OnEnable()
    {
        if (notifyRoutine == null)
            notifyRoutine = StartCoroutine(NotifyRoutine());
    }

    void OnDisable()
    {
        if (notifyRoutine != null)
        {
            StopCoroutine(notifyRoutine);
            notifyRoutine = null;
        }

        if (sirenSource != null && sirenSource.isPlaying)
            sirenSource.Stop();

        SetEmergencyLights(false);
    }

    IEnumerator NotifyRoutine()
    {
        while (enabled)
        {
            if (sirenActive)
                AIVehicleController.NotifyEmergencyVehicle(transform.position, influenceRadius, pullOverDuration);
            yield return new WaitForSeconds(notifyInterval);
        }

        notifyRoutine = null;
    }

    void SetEmergencyLights(bool enabledState)
    {
        if (emergencyLights == null) return;
        for (int i = 0; i < emergencyLights.Length; i++)
            if (emergencyLights[i] != null)
                emergencyLights[i].enabled = enabledState;
    }
}
