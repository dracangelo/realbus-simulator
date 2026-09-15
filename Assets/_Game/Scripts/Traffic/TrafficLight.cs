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
        AdvanceTime(Time.deltaTime);
    }

    public void AdvanceTime(float seconds)
    {
        var previous = currentState;
        float cycle = GetStateDuration(LightState.Red) + GetStateDuration(LightState.Green) + GetStateDuration(LightState.Amber);
        timeInState += Mathf.Max(0f, seconds) % cycle;
        while (timeInState >= GetStateDuration(currentState))
        {
            timeInState -= GetStateDuration(currentState);
            AdvanceState();
        }
        if (previous != currentState) UpdateVisuals();
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
            case LightState.Red: return Mathf.Max(0.01f, redDuration);
            case LightState.Green: return Mathf.Max(0.01f, greenDuration);
            case LightState.Amber: return Mathf.Max(0.01f, amberDuration);
            default: return Mathf.Max(0.01f, redDuration);
        }
    }

    void UpdateVisuals()
    {
        ApplyColor(redLight, currentState == LightState.Red ? LitRed : Unlit);
        ApplyColor(greenLight, currentState == LightState.Green ? LitGreen : Unlit);
        ApplyColor(amberLight, currentState == LightState.Amber ? LitAmber : Unlit);
    }
    MaterialPropertyBlock block;
    void ApplyColor(Renderer target, Color color)
    {
        if (target == null) return;
        if (block == null) block = new MaterialPropertyBlock();
        target.GetPropertyBlock(block);
        block.SetColor("_BaseColor", color);
        block.SetColor("_Color", color);
        target.SetPropertyBlock(block);
    }

    public bool IsRed() => currentState == LightState.Red;
    public bool IsGreen() => currentState == LightState.Green;

    void ApplyPhaseOffset()
    {
        currentState = LightState.Red;
        timeInState = 0f;
        AdvanceTime(phaseOffset);
    }
}
