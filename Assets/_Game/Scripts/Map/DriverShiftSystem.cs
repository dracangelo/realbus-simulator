using UnityEngine;

[System.Serializable]
public class ShiftInspectionChecklist
{
    public bool tyreConditionPassed = true;
    public bool fuelLevelPassed = true;
    public bool doorFunctionPassed = true;
    public bool exteriorLightsPassed = true;

    public int DefectCount
    {
        get
        {
            int defects = 0;
            if (!tyreConditionPassed) defects++;
            if (!fuelLevelPassed) defects++;
            if (!doorFunctionPassed) defects++;
            if (!exteriorLightsPassed) defects++;
            return defects;
        }
    }
}

public class DriverShiftSystem : MonoBehaviour
{
    public static DriverShiftSystem Instance { get; private set; }

    [Header("Shift State")]
    public bool shiftActive = false;
    public bool shiftCompleted = false;
    public bool shiftSummaryPending = false;
    public bool inspectionCompleted = false;
    public bool rotaSignedIn = false;
    public bool scheduledBreakDue = false;
    public float shiftDurationMinutes = 60f;
    public float elapsedShiftMinutes = 0f;
    public int plannedRouteCount = 3;
    public int routesCompleted = 0;
    public int breaksTaken = 0;
    public int missedBreaks = 0;
    public string assignedDepotBayId = "BAY-3";
    public bool returnedToCorrectBay = true;

    [Header("Inspection")]
    public ShiftInspectionChecklist lastInspection = new ShiftInspectionChecklist();

    [Header("Shift Summary")]
    public int totalPassengersServed = 0;
    public float totalGrossEarningsKES = 0f;
    public float totalNetEarningsKES = 0f;
    public float totalDistanceKm = 0f;
    public float totalFuelConsumedLitres = 0f;
    public int totalIncidents = 0;
    public float averagePunctualityScore = 100f;
    public float averageSatisfactionScore = 100f;
    public float averageSafetyScore = 100f;
    public float averageEfficiencyScore = 100f;

    float routeFuelCounterAtStart = 0f;
    int routeCollisionCountAtStart = 0;
    int routeRedLightCountAtStart = 0;
    float punctualityScoreAccumulator = 0f;
    float satisfactionScoreAccumulator = 0f;
    float safetyScoreAccumulator = 0f;
    float efficiencyScoreAccumulator = 0f;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this); return; }
        Instance = this;
    }

    public void PrepareRoute(BusRoute route, MissionData missionData)
    {
        if (shiftCompleted)
            ResetShiftState();

        if (!shiftActive)
            StartShift(route, missionData);

        if (!inspectionCompleted)
            RunPreDriveInspection();

        if (!rotaSignedIn)
            SignInToRota();

        if (scheduledBreakDue)
        {
            missedBreaks++;
            scheduledBreakDue = false;
            ScoreTracker.Instance?.ApplyShiftBreakPenalty(8f);
        }

        CaptureRouteBaseline();
    }

    public void RunPreDriveInspection()
    {
        var vehicleState = GameState.Instance != null ? GameState.Instance.vehicleState : null;
        var maintenanceSystem = FindFirstObjectByType<MaintenanceSystem>();
        var fuelSystem = FindFirstObjectByType<FuelSystem>();

        float lowestTyreCondition = 100f;
        if (maintenanceSystem != null)
        {
            for (int i = 0; i < maintenanceSystem.AxleCount; i++)
                lowestTyreCondition = Mathf.Min(lowestTyreCondition, maintenanceSystem.GetAxleConditionPercent(i));
        }

        lastInspection.tyreConditionPassed = lowestTyreCondition >= 55f;
        lastInspection.fuelLevelPassed = fuelSystem == null || fuelSystem.CurrentFuelPercent >= FuelSystem.FuelWarningPercent;
        lastInspection.doorFunctionPassed = vehicleState == null || vehicleState.doorFunctionOperational;
        lastInspection.exteriorLightsPassed = vehicleState == null || vehicleState.exteriorLightsOperational;
        inspectionCompleted = true;
    }

    public void SignInToRota()
    {
        rotaSignedIn = true;
    }

    public void TakeScheduledBreak(float breakMinutes = 15f)
    {
        if (!scheduledBreakDue)
            return;

        scheduledBreakDue = false;
        breaksTaken++;
        elapsedShiftMinutes += breakMinutes;
    }

    public void CompleteRoute(BusRoute route, MissionResult result, bool endedAtDepot)
    {
        if (!shiftActive || result == null)
            return;

        routesCompleted++;
        elapsedShiftMinutes += Mathf.Max(10f, result.totalTimeMinutes);
        totalPassengersServed += result.totalPassengers;
        totalGrossEarningsKES += result.totalFareKES;
        totalNetEarningsKES += result.netEarningsKES;
        totalDistanceKm += result.totalDistanceKm;

        float fuelCounter = GameState.Instance != null ? GameState.Instance.vehicleState.totalFuelConsumedLitres : 0f;
        totalFuelConsumedLitres += Mathf.Max(0f, fuelCounter - routeFuelCounterAtStart);

        int routeIncidents = 0;
        if (ScoreTracker.Instance != null)
        {
            routeIncidents += Mathf.Max(0, ScoreTracker.Instance.CollisionCount - routeCollisionCountAtStart);
            routeIncidents += Mathf.Max(0, ScoreTracker.Instance.RedLightViolationCount - routeRedLightCountAtStart);
        }
        totalIncidents += routeIncidents;

        punctualityScoreAccumulator += result.punctualityScore;
        satisfactionScoreAccumulator += result.satisfactionScore;
        safetyScoreAccumulator += result.safetyScore;
        efficiencyScoreAccumulator += result.efficiencyScore;

        averagePunctualityScore = punctualityScoreAccumulator / routesCompleted;
        averageSatisfactionScore = satisfactionScoreAccumulator / routesCompleted;
        averageSafetyScore = safetyScoreAccumulator / routesCompleted;
        averageEfficiencyScore = efficiencyScoreAccumulator / routesCompleted;

        if (routesCompleted < plannedRouteCount)
        {
            scheduledBreakDue = true;
            if (endedAtDepot)
                TakeScheduledBreak();
        }

        if (routesCompleted >= plannedRouteCount || elapsedShiftMinutes >= shiftDurationMinutes)
        {
            shiftActive = false;
            shiftCompleted = true;
            shiftSummaryPending = true;
            returnedToCorrectBay = endedAtDepot;
            if (!endedAtDepot)
                ScoreTracker.Instance?.ApplyShiftBreakPenalty(5f);
        }
    }

    public bool TryApplyShiftSummary(MissionResult result)
    {
        if (!shiftSummaryPending || result == null)
            return false;

        result.isShiftSummary = true;
        result.routeName = $"SHIFT {routesCompleted}/{plannedRouteCount}";
        result.punctualityScore = averagePunctualityScore;
        result.satisfactionScore = averageSatisfactionScore;
        result.safetyScore = averageSafetyScore;
        result.efficiencyScore = averageEfficiencyScore;
        result.totalScore = (averagePunctualityScore + averageSatisfactionScore + averageSafetyScore + averageEfficiencyScore) / 4f;
        result.totalPassengers = totalPassengersServed;
        result.totalFareKES = Mathf.RoundToInt(totalGrossEarningsKES);
        result.netEarningsKES = Mathf.RoundToInt(totalNetEarningsKES);
        result.totalDistanceKm = totalDistanceKm;
        result.shiftRoutesCompleted = routesCompleted;
        result.shiftPlannedRoutes = plannedRouteCount;
        result.shiftFuelConsumedLitres = totalFuelConsumedLitres;
        result.shiftIncidentCount = totalIncidents;
        result.returnedToCorrectBay = returnedToCorrectBay;

        shiftSummaryPending = false;
        return true;
    }

    void StartShift(BusRoute route, MissionData missionData)
    {
        ResetShiftState();
        shiftActive = true;

        float routeMinutes = Mathf.Max(15f,
            missionData != null ? missionData.targetDurationMinutes :
            route != null && route.estimatedTimeMinutes > 0f ? route.estimatedTimeMinutes :
            25f);

        plannedRouteCount = Mathf.Clamp(Mathf.RoundToInt(75f / routeMinutes), 2, 4);
        shiftDurationMinutes = Mathf.Clamp((routeMinutes * plannedRouteCount) + ((plannedRouteCount - 1) * 10f), 30f, 90f);
    }

    void ResetShiftState()
    {
        shiftActive = false;
        shiftCompleted = false;
        shiftSummaryPending = false;
        inspectionCompleted = false;
        rotaSignedIn = false;
        scheduledBreakDue = false;
        elapsedShiftMinutes = 0f;
        routesCompleted = 0;
        breaksTaken = 0;
        missedBreaks = 0;
        returnedToCorrectBay = true;
        lastInspection = new ShiftInspectionChecklist();
        totalPassengersServed = 0;
        totalGrossEarningsKES = 0f;
        totalNetEarningsKES = 0f;
        totalDistanceKm = 0f;
        totalFuelConsumedLitres = 0f;
        totalIncidents = 0;
        averagePunctualityScore = 100f;
        averageSatisfactionScore = 100f;
        averageSafetyScore = 100f;
        averageEfficiencyScore = 100f;
        punctualityScoreAccumulator = 0f;
        satisfactionScoreAccumulator = 0f;
        safetyScoreAccumulator = 0f;
        efficiencyScoreAccumulator = 0f;
    }

    void CaptureRouteBaseline()
    {
        routeFuelCounterAtStart = GameState.Instance != null ? GameState.Instance.vehicleState.totalFuelConsumedLitres : 0f;
        routeCollisionCountAtStart = ScoreTracker.Instance != null ? ScoreTracker.Instance.CollisionCount : 0;
        routeRedLightCountAtStart = ScoreTracker.Instance != null ? ScoreTracker.Instance.RedLightViolationCount : 0;
    }
}
