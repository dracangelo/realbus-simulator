using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public enum LightState { Red, Green, Amber }

    [Header("Timing")]
    public float redDuration = 45f;
    public float greenDuration = 35f;
    public float amberDuration = 5f;
    public float phaseOffset = 0f; // stagger intersections

    [Header("State")]
    public LightState currentState = LightState.Red;
    public float timeInState = 0f;

    [Header("Visuals")]
    public Renderer redLight;
    public Renderer greenLight;
    public Renderer amberLight;

    public static readonly Color LitRed = new Color(1f, 0.1f, 0.1f);
    public static readonly Color LitGreen = new Color(0.1f, 1f, 0.1f);
    public static readonly Color LitAmber = new Color(1f, 0.6f, 0f);
    public static readonly Color Unlit = new Color(0.15f, 0.15f, 0.15f);

    void Start()
    {
        ApplyPhaseOffset();
        UpdateVisuals();
    }

    void Update()
    {
        timeInState += Time.deltaTime;

        if (timeInState >= GetStateDuration(currentState))
        {
            timeInState = 0f;
            AdvanceState();
        }

        UpdateVisuals();
    }

    void AdvanceState()
    {
        switch (currentState)
        {
            case LightState.Red: currentState = LightState.Green; break;
            case LightState.Green: currentState = LightState.Amber; break;
            case LightState.Amber: currentState = LightState.Red; break;
        }
    }

    float GetStateDuration(LightState state)
    {
        switch (state)
        {
            case LightState.Red: return redDuration;
            case LightState.Green: return greenDuration;
            case LightState.Amber: return amberDuration;
            default: return redDuration;
        }
    }

    void UpdateVisuals()
    {
        if (redLight) redLight.material.color = 
            currentState == LightState.Red ? LitRed : Unlit;
        if (greenLight) greenLight.material.color = 
            currentState == LightState.Green ? LitGreen : Unlit;
        if (amberLight) amberLight.material.color = 
            currentState == LightState.Amber ? LitAmber : Unlit;
    }

    public bool IsRed() => currentState == LightState.Red;
    public bool IsGreen() => currentState == LightState.Green;

    void ApplyPhaseOffset()
    {
        float remainingOffset = Mathf.Max(0f, phaseOffset);
        currentState = LightState.Red;
        timeInState = 0f;

        while (remainingOffset > 0f)
        {
            float stateDuration = GetStateDuration(currentState);
            if (remainingOffset < stateDuration)
            {
                timeInState = remainingOffset;
                break;
            }

            remainingOffset -= stateDuration;
            AdvanceState();
        }
    }
}
