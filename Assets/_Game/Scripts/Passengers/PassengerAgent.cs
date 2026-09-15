using UnityEngine;

[System.Serializable]
public enum PassengerArchetype
{
    Commuter,
    Tourist,
    Student,
    Elderly
}

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
    public PassengerArchetype archetype;
    public bool stopRequested;
    public bool preparingToAlight;
    public bool hasComplainedAboutBraking;

    // Lightweight animation state placeholders.
    public Vector3 platformPosition;
    public Vector3 busLocalPosition;
    public float boardingLerp;
    public float standingSway;

    public PassengerAgent(int originStop, int destinationStop, float initialPatience, bool wheelchair, PassengerArchetype passengerArchetype = PassengerArchetype.Commuter)
    {
        originStopIndex = originStop;
        destinationStopIndex = destinationStop;
        patienceSeconds = initialPatience;
        isWheelchairPassenger = wheelchair;
        archetype = passengerArchetype;
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
        isAlighting = false;
        boardingLerp = 0f;
        busLocalPosition = platformPosition;
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
        preparingToAlight = false;
    }

    public void AssignBusSlot(int slotIndex)
    {
        assignedSlotIndex = slotIndex;
    }

    public void UpdateStandingSway(float inverseAcceleration, float deltaTime = 0.02f)
    {
        // Standing passengers sway opposite longitudinal acceleration.
        standingSway = Mathf.Clamp(inverseAcceleration * 0.08f, -0.35f, 0.35f);
        satisfaction = Mathf.Clamp01(satisfaction - Mathf.Abs(standingSway) * 0.025f * deltaTime);
    }

    public void TickBoardingAnimation(float deltaTime, Vector3 targetBusLocalPosition, float lerpSpeed = 2.6f)
    {
        if (!isBoarding)
            return;

        boardingLerp = Mathf.Clamp01(boardingLerp + (deltaTime * Mathf.Max(0.1f, lerpSpeed)));
        busLocalPosition = Vector3.Lerp(platformPosition, targetBusLocalPosition, boardingLerp);
        if (boardingLerp >= 0.999f)
            isBoarding = false;
    }

    public void TickAlightingAnimation(float deltaTime, Vector3 doorLocalPosition, float moveSpeed = 2.8f)
    {
        if (!isAlighting)
            return;

        busLocalPosition = Vector3.MoveTowards(
            busLocalPosition,
            doorLocalPosition,
            Mathf.Max(0.1f, moveSpeed) * deltaTime);
    }

    public bool HasReachedExit(Vector3 doorLocalPosition, float threshold = 0.08f)
    {
        return Vector3.Distance(busLocalPosition, doorLocalPosition) <= Mathf.Max(0.01f, threshold);
    }

    public void MarkExited()
    {
        isAlighting = false;
        preparingToAlight = false;
        stopRequested = false;
    }

    public void RegisterHarshAcceleration(float deltaTime = 0.02f)
    {
        satisfaction = Mathf.Clamp01(satisfaction - 0.08f * deltaTime);
    }

    public float GetBoardingTimeMultiplier()
    {
        switch (archetype)
        {
            case PassengerArchetype.Tourist: return 1.45f;
            case PassengerArchetype.Student: return 1.2f;
            case PassengerArchetype.Elderly: return 1.35f;
            default: return 1f;
        }
    }

    public float GetAlightingTimeMultiplier()
    {
        switch (archetype)
        {
            case PassengerArchetype.Student: return 1.15f;
            case PassengerArchetype.Elderly: return 1.4f;
            default: return 1f;
        }
    }

    public bool RequiresKneelingForBoarding()
    {
        return isWheelchairPassenger || archetype == PassengerArchetype.Elderly;
    }

    public void PrepareForUpcomingStop()
    {
        preparingToAlight = true;
    }

    public void PressStopRequest()
    {
        stopRequested = true;
    }

    public void ClearStopRequest()
    {
        stopRequested = false;
        preparingToAlight = false;
    }

    public void RegisterHarshBrakingComplaint()
    {
        if (hasComplainedAboutBraking)
            return;

        hasComplainedAboutBraking = true;
        satisfaction = Mathf.Clamp01(satisfaction - 0.08f);
    }
}
