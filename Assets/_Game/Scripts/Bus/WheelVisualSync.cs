using UnityEngine;

public class WheelVisualSync : MonoBehaviour
{
    public WheelCollider wheelCollider;
    public Transform wheelMesh;

    void Update()
    {
        if (wheelCollider == null || wheelMesh == null) return;
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelMesh.position = pos;
        wheelMesh.rotation = rot;
    }
}