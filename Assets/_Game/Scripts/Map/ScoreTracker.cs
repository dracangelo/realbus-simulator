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

    void Update()
    {
        if (busRigidbody == null) return;

        CheckPassengerSatisfaction();
        UpdateTotalScore();

        lastVelocity = busRigidbody.linearVelocity;
    }

    void CheckPassengerSatisfaction()
    {
        // Hard braking — deceleration > 0.4g
        Vector3 acceleration = (busRigidbody.linearVelocity - lastVelocity) / Time.deltaTime;
        float decelerationG = -Vector3.Dot(acceleration, busRigidbody.transform.forward) / 9.81f;

        if (decelerationG > 0.4f)
        {
            hardBrakingPenalty += decelerationG * Time.deltaTime * 2f;
            satisfactionScore = Mathf.Max(0f, 100f - hardBrakingPenalty - sharpCornerPenalty);
        }

        // Sharp cornering — lateral G > 0.3g
        float lateralG = Vector3.Dot(acceleration, busRigidbody.transform.right) / 9.81f;
        if (Mathf.Abs(lateralG) > 0.3f)
        {
            sharpCornerPenalty += Mathf.Abs(lateralG) * Time.deltaTime * 1.5f;
            satisfactionScore = Mathf.Max(0f, 100f - hardBrakingPenalty - sharpCornerPenalty);
        }
    }

    /// <summary>
    /// Call when bus arrives at a stop — update punctuality score.
    /// </summary>
    public void RecordStopArrival(PunctualityStatus status)
    {
        stopsCompleted++;

        switch (status)
        {
            case PunctualityStatus.OnTime:
                // No penalty
                break;
            case PunctualityStatus.Early:
                punctualityScore = Mathf.Max(0f, punctualityScore - 5f);
                break;
            case PunctualityStatus.Late:
                punctualityScore = Mathf.Max(0f, punctualityScore - 10f);
                break;
            case PunctualityStatus.SeverelyLate:
                punctualityScore = Mathf.Max(0f, punctualityScore - 20f);
                break;
        }
    }

    public void RecordCollision()
    {
        collisionCount++;
        safetyScore = Mathf.Max(0f, safetyScore - 15f);
        Debug.Log($"Collision! Safety score: {safetyScore:F0}");
    }

    public void RecordRedLight()
    {
        redLightViolations++;
        safetyScore = Mathf.Max(0f, safetyScore - 10f);
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
        satisfactionScore = Mathf.Max(0f, satisfactionScore - Mathf.Max(0f, satisfactionPenalty));
        punctualityScore = Mathf.Max(0f, punctualityScore - Mathf.Max(0f, punctualityPenalty));
        UpdateTotalScore();
    }

    void UpdateTotalScore()
    {
        totalScore = (punctualityScore * punctualityWeight)
                   + (satisfactionScore * satisfactionWeight)
                   + (safetyScore * safetyWeight)
                   + (efficiencyScore * efficiencyWeight);
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
}
