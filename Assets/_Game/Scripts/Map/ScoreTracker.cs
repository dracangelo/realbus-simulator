using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker Instance { get; private set; }

    [Header("Score Weights")]
    [Range(0, 1)] public float punctualityWeight = 0.30f;
    [Range(0, 1)] public float satisfactionWeight = 0.30f;
    [Range(0, 1)] public float safetyWeight = 0.25f;
    [Range(0, 1)] public float efficiencyWeight = 0.15f;

    [Header("Punctuality (30%)")]
    public float punctualityScore = 100f;

    [Header("Passenger Satisfaction (30%)")]
    public float satisfactionScore = 100f;
    private float hardBrakingPenalty = 0f;
    private float sharpCornerPenalty = 0f;
    private float harshAccelerationPenalty = 0f;
    private float satisfactionBaseline = 100f;

    [Header("Safety (25%)")]
    public float safetyScore = 100f;
    private int collisionCount = 0;
    private int redLightViolations = 0;

    [Header("Fuel Efficiency (15%)")]
    public float efficiencyScore = 100f;

    [Header("Live Score")]
    public float totalScore = 100f;
    public int stopsCompleted = 0;

    private Rigidbody busRigidbody;
    private Vector3 lastVelocity;

    bool recording;
    float missionFuelStart;
    float missionDistanceStart;
    FuelSystem fuel;
    public int pointDeductions;
    float extraPointPenaltyPercent;
    float passengerServicePenalty;
    public const int MaximumPoints = 10000;
    public bool IsRecording => recording;

    public void BeginMission(BusController bus)
    {
        busRigidbody = bus != null ? bus.GetComponent<Rigidbody>() : null;
        lastVelocity = busRigidbody != null ? busRigidbody.linearVelocity : Vector3.zero;
        punctualityScore = satisfactionScore = safetyScore = efficiencyScore = totalScore = 100f;
        satisfactionBaseline = 100f;
        hardBrakingPenalty = sharpCornerPenalty = harshAccelerationPenalty = 0f;
        collisionCount = redLightViolations = stopsCompleted = pointDeductions = 0;
        extraPointPenaltyPercent = passengerServicePenalty = 0f;
        fuel = bus != null ? bus.GetComponent<FuelSystem>() : null;
        missionFuelStart = fuel != null ? fuel.TotalConsumedLitres : 0f;
        missionDistanceStart = MissionManager.Instance != null ? MissionManager.Instance.DistanceDrivenKm : 0f;
        if (bus != null && bus.GetComponent<MissionDrivingMonitor>() == null) bus.gameObject.AddComponent<MissionDrivingMonitor>();
        recording = true;
    }
    public void EndMission() { UpdateTotalScore(); recording = false; }

    public int CollisionCount => collisionCount;
    public int RedLightViolationCount => redLightViolations;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        var bus = FindFirstObjectByType<BusController>();
        if (bus != null) busRigidbody = bus.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!recording || busRigidbody == null) return;

        CheckPassengerSatisfaction();
        var passengers = PassengerManager.Instance;
        if (passengers != null && passengers.totalPassengersServed > 0)
            satisfactionScore = Mathf.Min(satisfactionScore, passengers.averageSatisfaction * 100f);
        var mission = MissionManager.Instance;
        float scoredDistance = mission != null ? mission.DistanceDrivenKm - missionDistanceStart : 0f;
        if (fuel != null && scoredDistance > 0.1f)
            UpdateEfficiencyScore((fuel.TotalConsumedLitres - missionFuelStart) / scoredDistance * 100f, fuel.GetBaseConsumptionLPer100Km());
        UpdateTotalScore();

        lastVelocity = busRigidbody.linearVelocity;
    }

    void CheckPassengerSatisfaction()
    {
        // Hard braking — deceleration > 0.4g
        Vector3 acceleration = (busRigidbody.linearVelocity - lastVelocity) / Time.fixedDeltaTime;
        float decelerationG = -Vector3.Dot(acceleration, busRigidbody.transform.forward) / 9.81f;

        if (decelerationG > 0.4f)
        {
            hardBrakingPenalty += decelerationG * Time.fixedDeltaTime * 2f;
            satisfactionScore = Mathf.Max(0f, satisfactionBaseline - hardBrakingPenalty - sharpCornerPenalty - harshAccelerationPenalty - passengerServicePenalty);
        }

        // Sharp cornering — lateral G > 0.3g
        float lateralG = Vector3.Dot(acceleration, busRigidbody.transform.right) / 9.81f;
        if (Mathf.Abs(lateralG) > 0.3f)
        {
            sharpCornerPenalty += Mathf.Abs(lateralG) * Time.fixedDeltaTime * 1.5f;
            satisfactionScore = Mathf.Max(0f, satisfactionBaseline - hardBrakingPenalty - sharpCornerPenalty - harshAccelerationPenalty - passengerServicePenalty);
        }

        float accelerationG = Vector3.Dot(acceleration, busRigidbody.transform.forward) / 9.81f;
        if (accelerationG > 0.35f)
        {
            harshAccelerationPenalty += accelerationG * Time.fixedDeltaTime * 1.25f;
            satisfactionScore = Mathf.Max(0f, satisfactionBaseline - hardBrakingPenalty - sharpCornerPenalty - harshAccelerationPenalty - passengerServicePenalty);
        }
    }

    /// <summary>
    /// Call when bus arrives at a stop — update punctuality score.
    /// </summary>
    public void RecordStopArrival(PunctualityStatus status)
    {
        RecordStopArrival(status, 0f);
    }

    public void RecordStopArrival(PunctualityStatus status, float arrivalDeltaSeconds)
    {
        stopsCompleted++;

        punctualityScore = Mathf.Max(0f, punctualityScore - GameplayRules.LatePenaltyPercent(arrivalDeltaSeconds));
        UpdateTotalScore();
    }

    public void RecordCollision()
    {
        if (!recording) return;
        collisionCount++;
        safetyScore = Mathf.Max(0f, safetyScore - 15f);
        Debug.Log($"Collision! Safety score: {safetyScore:F0}");
    }

    public void RecordRedLight()
    {
        if (!recording) return;
        redLightViolations++;
        pointDeductions += 500;
        float categoryDeduction = safetyWeight > 0f ? Mathf.Min(safetyScore, 5f / safetyWeight) : 0f;
        safetyScore -= categoryDeduction;
        extraPointPenaltyPercent += Mathf.Max(0f, 5f - categoryDeduction * safetyWeight);
        UpdateTotalScore();
        Debug.Log($"Red light! Safety score: {safetyScore:F0}");
    }

    public void UpdateEfficiencyScore(float actualL100km, float targetL100km)
    {
        float ratio = targetL100km / Mathf.Max(actualL100km, 0.1f);
        efficiencyScore = Mathf.Clamp(ratio * 100f, 0f, 100f);
    }

    public void ApplyShiftBreakPenalty(float penalty = 8f)
    {
        punctualityScore = Mathf.Max(0f, punctualityScore - penalty);
        UpdateTotalScore();
    }

    public void ApplyTrafficPenalty(float safetyPenalty, float efficiencyPenalty = 0f, float punctualityPenalty = 0f)
    {
        safetyScore = Mathf.Max(0f, safetyScore - Mathf.Max(0f, safetyPenalty));
        efficiencyScore = Mathf.Max(0f, efficiencyScore - Mathf.Max(0f, efficiencyPenalty));
        punctualityScore = Mathf.Max(0f, punctualityScore - Mathf.Max(0f, punctualityPenalty));
        UpdateTotalScore();
    }

    public void ApplyPassengerServicePenalty(float satisfactionPenalty, float punctualityPenalty = 0f)
    {
        if (!recording) return;
        passengerServicePenalty += Mathf.Max(0f, satisfactionPenalty);
        satisfactionScore = Mathf.Max(0f, satisfactionScore - Mathf.Max(0f, satisfactionPenalty));
        punctualityScore = Mathf.Max(0f, punctualityScore - Mathf.Max(0f, punctualityPenalty));
        UpdateTotalScore();
    }

    public void RestoreCheckpoint(float punctuality, float satisfaction, float safety, float efficiency,
        int completedStops, int deductions, int collisions, int redLights)
    {
        punctualityScore = Mathf.Clamp(punctuality, 0f, 100f);
        satisfactionScore = satisfactionBaseline = Mathf.Clamp(satisfaction, 0f, 100f);
        safetyScore = Mathf.Clamp(safety, 0f, 100f);
        efficiencyScore = Mathf.Clamp(efficiency, 0f, 100f);
        stopsCompleted = Mathf.Max(0, completedStops);
        pointDeductions = Mathf.Max(0, deductions);
        collisionCount = Mathf.Max(0, collisions);
        redLightViolations = Mathf.Max(0, redLights);
        hardBrakingPenalty = sharpCornerPenalty = harshAccelerationPenalty = 0f;
        extraPointPenaltyPercent = passengerServicePenalty = 0f;
        missionFuelStart = fuel != null ? fuel.TotalConsumedLitres : 0f;
        missionDistanceStart = MissionManager.Instance != null ? MissionManager.Instance.DistanceDrivenKm : 0f;
        UpdateTotalScore();
    }

    public void ApplyDynamicEventImpact(float safetyPenalty, float punctualityPenalty, string sourceLabel, bool complianceFailure, float efficiencyPenalty = 0f)
    {
        safetyScore = Mathf.Max(0f, safetyScore - Mathf.Max(0f, safetyPenalty));
        punctualityScore = Mathf.Max(0f, punctualityScore - Mathf.Max(0f, punctualityPenalty));
        efficiencyScore = Mathf.Max(0f, efficiencyScore - Mathf.Max(0f, efficiencyPenalty));
        UpdateTotalScore();

        if (complianceFailure)
            Debug.Log($"Dynamic event non-compliance: {sourceLabel} | safety {safetyPenalty:F0} | punctuality {punctualityPenalty:F0}");
        else
            Debug.Log($"Dynamic event impact: {sourceLabel} | punctuality {punctualityPenalty:F0}");
    }

    void UpdateTotalScore()
    {
        totalScore = (punctualityScore * punctualityWeight)
                   + (satisfactionScore * satisfactionWeight)
                   + (safetyScore * safetyWeight)
                   + (efficiencyScore * efficiencyWeight);
        totalScore = Mathf.Clamp(totalScore - extraPointPenaltyPercent, 0f, 100f);
    }

    public int GetStarRating()
    {
        if (totalScore >= 90f) return 5;
        if (totalScore >= 75f) return 4;
        if (totalScore >= 60f) return 3;
        if (totalScore >= 40f) return 2;
        return 1;
    }

    public string GetScoreSummary()
    {
        return $"Total: {totalScore:F0}% | " +
               $"Punctuality: {punctualityScore:F0}% | " +
               $"Satisfaction: {satisfactionScore:F0}% | " +
               $"Safety: {safetyScore:F0}% | " +
               $"Efficiency: {efficiencyScore:F0}%";
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision == null || collision.relativeVelocity.sqrMagnitude < 4f)
            return;

        RecordCollision();
    }
}
