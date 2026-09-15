using UnityEngine;

[CreateAssetMenu(fileName = "EngineSystem", menuName = "RealBus/EngineSystem")]
public class EngineSystem : ScriptableObject
{
    public AnimationCurve torqueCurve = new AnimationCurve(
        new Keyframe(500f, 600f), new Keyframe(1200f, 1200f),
        new Keyframe(2000f, 900f), new Keyframe(2500f, 600f));
    public float maxRPM = 2500f;
    public float idleRPM = 500f;

    public float GetTorque(float rpm)
    {
        return torqueCurve == null ? 0f : Mathf.Max(0f, torqueCurve.Evaluate(rpm));
    }
}