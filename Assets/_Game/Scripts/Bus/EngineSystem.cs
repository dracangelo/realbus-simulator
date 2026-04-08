using UnityEngine;

[CreateAssetMenu(fileName = "EngineSystem", menuName = "RealBus/EngineSystem")]
public class EngineSystem : ScriptableObject
{
    public AnimationCurve torqueCurve;
    public float maxRPM = 2500f;
    public float idleRPM = 500f;

    public float GetTorque(float rpm)
    {
        return torqueCurve.Evaluate(rpm);
    }
}