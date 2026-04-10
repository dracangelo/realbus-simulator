using UnityEngine;

[System.Serializable]
public class PassengerAgent
{
    public int originStopIndex;
    public int destinationStopIndex;
    public float patienceSeconds;
    [Range(0f, 1f)] public float satisfaction = 1f;
    public bool isWheelchairPassenger;
    public bool isSeated;
    public bool isBoarding;
    public bool isAlighting;
    public int assignedSlotIndex = -1;

    // Lightweight animation state placeholders.
    public Vector3 platformPosition;
    public Vector3 busLocalPosition;
    public float boardingLerp;
    public float standingSway;

    public PassengerAgent(int originStop, int destinationStop, float initialPatience, bool wheelchair)
    {
        originStopIndex = originStop;
        destinationStopIndex = destinationStop;
        patienceSeconds = initialPatience;
        isWheelchairPassenger = wheelchair;
    }

    public void TickPatience(float deltaTime)
    {
        patienceSeconds = Mathf.Max(0f, patienceSeconds - deltaTime);
        if (patienceSeconds <= 0f)
            satisfaction = Mathf.Max(0f, satisfaction - deltaTime * 0.2f);
    }

    public void BeginBoarding()
    {
        isBoarding = true;
        boardingLerp = 0f;
    }

    public void MarkBoarded()
    {
        isBoarding = false;
        isAlighting = false;
        boardingLerp = 1f;
    }

    public void MarkAlightingToDoor()
    {
        isAlighting = true;
        isBoarding = false;
    }

    public void AssignBusSlot(int slotIndex)
    {
        assignedSlotIndex = slotIndex;
    }

    public void UpdateStandingSway(float inverseAcceleration)
    {
        // Standing passengers sway opposite longitudinal acceleration.
        standingSway = Mathf.Clamp(inverseAcceleration * 0.08f, -0.35f, 0.35f);
        satisfaction = Mathf.Clamp01(satisfaction - Mathf.Abs(standingSway) * 0.0005f);
    }
}
