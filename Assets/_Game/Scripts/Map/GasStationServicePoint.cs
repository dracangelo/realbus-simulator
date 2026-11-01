using UnityEngine;

/// <summary>
/// Drive-in service trigger for a fuel station or EV charger.
///
/// The station still meters energy progressively while the bus stands in the
/// trigger, but a player session is now explicit: the interaction panel calls
/// <see cref="BeginSession"/>, the station refuses to start while the bus is
/// moving or the tank is full, and only one station can own a session at a time.
/// That is what keeps a second tick, a second station or a second bus from
/// charging the player twice for the same litre.
///
/// <see cref="Service"/> remains the low-level metering step used by the
/// automatic/AI path and by the tests. It never starts a second session.
/// </summary>
[RequireComponent(typeof(Collider))]
public class GasStationServicePoint : MonoBehaviour
{
    public enum ServiceMode { None, Fuel, Charge }

    [Header("Service")]
    [Tooltip("Litres per second while a diesel bus is inside the trigger.")]
    public float refuelLitresPerSecond = 22f;
    [Tooltip("kWh per second while an electric bus is inside the trigger.")]
    public float rechargeKWhPerSecond = 55f;
    [Tooltip("When the bus has an enabled BatterySystem, charge it instead of refuelling.")]
    public bool serviceBattery = true;
    [Tooltip("Show the 'tank full' and visit-summary subtitles.")]
    public bool logService = true;
    [Tooltip("Service automatically as soon as a bus enters, without waiting for the interaction panel. Off by default so the player explicitly starts a paid session.")]
    public bool autoServiceWhenUnattended = false;
    [Tooltip("The bus must be at or below this speed before a player session can start, and while it runs.")]
    public float maxServiceSpeedKmh = 1.5f;

    [Header("Compatibility")]
    [Tooltip("This station can sell diesel.")]
    public bool supportsFuel = true;
    [Tooltip("This station can sell electric charge. Turn off on a fuel-only site.")]
    public bool supportsCharging = true;

    FuelSystem fuel;
    BatterySystem battery;
    BusController bus;
    float nextAnnouncementTime;

    /// <summary>The station that currently owns the paid session, if any.</summary>
    public static GasStationServicePoint ActiveSession { get; private set; }

    /// <summary>True while the last service tick actually added energy.</summary>
    public bool IsServicing { get; private set; }
    /// <summary>Litres or kWh added during the current visit/session.</summary>
    public float SessionAmount { get; private set; }
    /// <summary>KES deducted during the current visit/session.</summary>
    public float SessionCostKES { get; private set; }
    /// <summary>True while a player-started session is running.</summary>
    public bool SessionActive { get; private set; }
    /// <summary>Energy the player asked to buy for this session, in litres or kWh.</summary>
    public float TargetAmount { get; private set; }
    /// <summary>Amount added by the most recently finished session.</summary>
    public float LastSessionAmount { get; private set; }
    /// <summary>Cost of the most recently finished session.</summary>
    public float LastSessionCostKES { get; private set; }
    public ServiceMode LastSessionMode { get; private set; }
    /// <summary>True when the last session ended because it reached its target or a full tank, not because it was cancelled.</summary>
    public bool LastSessionCompleted { get; private set; }
    public BusController ServicingBus => bus;

    /// <summary>Fuel or charge the station is currently set up to deliver.</summary>
    public ServiceMode Mode => ResolveMode();

    public bool IsFull => Mode == ServiceMode.Charge
        ? battery != null && battery.GetMissingChargeKWh() <= .01f
        : Mode == ServiceMode.Fuel && fuel != null && fuel.GetMissingFuelLitres() <= .01f;

    public float CapacityAmount => Mode == ServiceMode.Charge
        ? battery != null ? battery.batteryCapacityKWh : 0f
        : fuel != null ? fuel.tankCapacityLitres : 0f;

    public float CurrentAmount => Mode == ServiceMode.Charge
        ? battery != null ? battery.CurrentChargeKWh : 0f
        : fuel != null ? fuel.CurrentFuelLitres : 0f;

    public float MissingAmount => Mode == ServiceMode.Charge
        ? battery != null ? battery.GetMissingChargeKWh() : 0f
        : fuel != null ? fuel.GetMissingFuelLitres() : 0f;

    public float CurrentPercent => Mode == ServiceMode.Charge
        ? battery != null ? battery.CurrentChargePercent : 0f
        : fuel != null ? fuel.CurrentFuelPercent : 0f;

    public float UnitPrice => Mode == ServiceMode.Charge
        ? battery != null ? battery.electricityPricePerKWhKES : 0f
        : fuel != null ? fuel.dieselPricePerLitreKES : 0f;

    public string UnitLabel => Mode == ServiceMode.Charge
        ? "kWh"
        : fuel != null && !string.IsNullOrWhiteSpace(fuel.energyUnitLabel) ? fuel.energyUnitLabel : "L";

    /// <summary>0–1 progress of the active session, or 0 when idle.</summary>
    public float Progress01 => SessionActive && TargetAmount > .0001f
        ? Mathf.Clamp01(SessionAmount / TargetAmount)
        : 0f;

    void Reset()
    {
        var collider = GetComponent<Collider>();
        if (collider != null) collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other) => BindBus(other);

    void OnTriggerStay(Collider other)
    {
        if (!BindBus(other)) return;

        // A player session only advances while it is running. The automatic path
        // is opt-in for depots and test scenes; a normal station waits for the
        // interaction panel so the player sees the price before paying.
        if (SessionActive)
            TickSession(Time.deltaTime);
        else if (autoServiceWhenUnattended)
        {
            BeginSession(MissingAmount);
            if (SessionActive) TickSession(Time.deltaTime);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (bus == null) return;
        if (other.GetComponentInParent<BusController>() != bus) return;
        if (SessionActive) StopSession(cancelled: true);
        else AnnounceVisitSummary();
        Unbind();
    }

    void OnDisable()
    {
        if (SessionActive) StopSession(cancelled: true);
        Unbind();
    }

    /// <summary>Amount a rate delivers in a time step. Pure, so it can be tested.</summary>
    public static float AmountFor(float ratePerSecond, float seconds)
        => Mathf.Max(0f, ratePerSecond) * Mathf.Max(0f, seconds);

    /// <summary>Cost of an amount at a unit price. Pure, so it can be tested.</summary>
    public static float PriceFor(float amount, float unitPrice)
        => Mathf.Max(0f, amount) * Mathf.Max(0f, unitPrice);

    /// <summary>Target energy for a fraction (0–1) of the tank's missing capacity. Pure.</summary>
    public static float TargetAmountFor(float fraction, float missingAmount)
        => Mathf.Clamp01(fraction) * Mathf.Max(0f, missingAmount);

    /// <summary>
    /// How much energy the current balance can buy at <paramref name="unitPrice"/>.
    /// Returns <see cref="float.MaxValue"/> when there is no economy to charge.
    /// </summary>
    public static float AffordableAmount(float unitPrice)
    {
        var state = GameState.Instance;
        if (state == null || state.economy == null) return float.MaxValue;
        if (unitPrice <= 0f) return float.MaxValue;
        return Mathf.Max(0f, state.economy.balanceKES) / unitPrice;
    }

    public bool BindBus(Collider other)
    {
        if (other == null) return false;
        return BindBus(other.GetComponentInParent<BusController>());
    }

    public bool BindBus(BusController controller)
    {
        if (controller == null) return false;
        // Never let a second bus take over a session that is already being paid
        // for. The first bus has to leave (or the session must end) first.
        if (SessionActive && bus != null && bus != controller) return false;
        if (bus == controller) return true;

        bus = controller;
        fuel = controller.GetComponent<FuelSystem>();
        battery = controller.GetComponent<BatterySystem>();
        return true;
    }

    /// <summary>
    /// Starts a paid session. <paramref name="targetAmount"/> is the energy to add
    /// in litres or kWh; pass a value at or below zero for a full tank.
    /// </summary>
    public bool BeginSession(float targetAmount = -1f) => BeginSession(targetAmount, out _);

    public bool BeginSession(float targetAmount, out string failureKey)
    {
        failureKey = GetStartBlockReasonKey();
        if (failureKey != null) return false;

        float missing = MissingAmount;
        float target = targetAmount <= 0f ? missing : Mathf.Min(Mathf.Max(0f, targetAmount), missing);
        if (target <= .01f)
        {
            failureKey = "station.full";
            return false;
        }

        SessionActive = true;
        ActiveSession = this;
        TargetAmount = target;
        SessionAmount = 0f;
        SessionCostKES = 0f;
        IsServicing = false;
        LastSessionAmount = 0f;
        LastSessionCostKES = 0f;
        LastSessionMode = Mode;
        return true;
    }

    /// <summary>
    /// Why a session cannot start right now, as a localisation key. Null means
    /// the station is ready. Kept separate from <see cref="BeginSession"/> so the
    /// panel can explain the refusal instead of silently doing nothing.
    /// </summary>
    public string GetStartBlockReasonKey()
    {
        if (bus == null) return "station.unsupported";
        if (SessionActive) return "station.active";
        if (ActiveSession != null && ActiveSession != this) return "station.busy";
        if (ResolveMode() == ServiceMode.None) return "station.unsupported";
        if (!IsStopped()) return "station.tooFast";
        if (IsFull) return "station.full";
        if (AffordableAmount(UnitPrice) <= .0001f) return "station.noFunds";
        return null;
    }

    /// <summary>True when the bound bus is slow enough to be serviced.</summary>
    public bool IsStopped()
        => bus != null && bus.currentSpeedKmh <= Mathf.Max(.1f, maxServiceSpeedKmh);

    /// <summary>True when the supplied bus is physically inside this station's trigger.</summary>
    public bool IsBusInside(BusController controller)
    {
        if (controller == null) return false;
        var collider = GetComponent<Collider>();
        if (collider != null && collider.enabled)
        {
            Vector3 point = controller.transform.position;
            if (collider.bounds.SqrDistance(point) <= .01f) return true;
        }
        return Vector3.Distance(controller.transform.position, transform.position) <= 2f;
    }

    /// <summary>One service step. Returns true when energy was actually added.</summary>
    public bool Service(float seconds)
    {
        if (bus == null || seconds <= 0f) return false;
        if (SessionActive) return TickSession(seconds);
        // Legacy/direct metering: a caller that already owns the trigger can tick
        // the station without opening a panel. Gameplay entry points use
        // BeginSession so this path cannot silently spend money.
        return ServiceStep(seconds, float.MaxValue);
    }

    /// <summary>Price of buying <paramref name="amount"/> at this station's unit price.</summary>
    public float EstimatedCostFor(float amount) => PriceFor(amount, UnitPrice);

    bool TickSession(float seconds)
    {
        if (!SessionActive) return false;

        if (!IsStopped())
        {
            StopSession(cancelled: true);
            return false;
        }

        float remaining = Mathf.Max(0f, TargetAmount - SessionAmount);
        if (remaining <= .0001f)
        {
            StopSession(cancelled: false);
            return false;
        }

        bool serviced = ServiceStep(seconds, remaining);
        if (SessionActive && (SessionAmount >= TargetAmount - .0001f || IsFull))
            StopSession(cancelled: false);
        return serviced;
    }

    bool ServiceStep(float seconds, float limit)
    {
        IsServicing = false;
        switch (ResolveMode())
        {
            case ServiceMode.Charge: return ServiceBattery(seconds, limit);
            case ServiceMode.Fuel: return ServiceFuel(seconds, limit);
            default: return false;
        }
    }

    bool ServiceFuel(float seconds, float limit)
    {
        float missing = fuel.GetMissingFuelLitres();
        if (missing <= .01f)
        {
            AnnounceFull(Localization.Get("station.fullTank"));
            return false;
        }

        float unitPrice = fuel.dieselPricePerLitreKES;
        float affordable = AffordableAmount(unitPrice);
        if (affordable <= .0001f)
        {
            AnnounceNoFunds();
            if (SessionActive) StopSession(cancelled: true);
            return false;
        }

        float requested = Mathf.Min(AmountFor(refuelLitresPerSecond, seconds), missing);
        requested = Mathf.Min(requested, Mathf.Max(0f, limit));
        requested = Mathf.Min(requested, affordable);
        float added = fuel.Refuel(requested);
        if (added <= 0f) return false;

        float price = fuel.PriceForLitres(added);
        Deduct(price);
        SessionAmount += added;
        SessionCostKES += price;
        IsServicing = true;

        if (fuel.GetMissingFuelLitres() <= .01f)
        {
            string full = Localization.Get("station.fullTank");
            AnnounceFull($"{full} · KES {SessionCostKES:N0}");
        }
        return true;
    }

    bool ServiceBattery(float seconds, float limit)
    {
        float missing = battery.GetMissingChargeKWh();
        if (missing <= .01f)
        {
            AnnounceFull("Battery full");
            return false;
        }

        float unitPrice = battery.electricityPricePerKWhKES;
        float affordable = AffordableAmount(unitPrice);
        if (affordable <= .0001f)
        {
            AnnounceNoFunds();
            if (SessionActive) StopSession(cancelled: true);
            return false;
        }

        float requested = Mathf.Min(AmountFor(rechargeKWhPerSecond, seconds), missing);
        requested = Mathf.Min(requested, Mathf.Max(0f, limit));
        requested = Mathf.Min(requested, affordable);
        float added = battery.Recharge(requested);
        if (added <= 0f) return false;

        float price = battery.PriceForKWh(added);
        Deduct(price);
        SessionAmount += added;
        SessionCostKES += price;
        IsServicing = true;

        if (battery.GetMissingChargeKWh() <= .01f)
        {
            string full = Localization.Get("station.fullTank");
            AnnounceFull($"{full} · KES {SessionCostKES:N0}");
        }
        return true;
    }

    /// <summary>Ends the active session. Cancelling keeps what was already delivered.</summary>
    public void StopSession(bool cancelled = false)
    {
        if (!SessionActive) return;

        LastSessionAmount = SessionAmount;
        LastSessionCostKES = SessionCostKES;
        LastSessionMode = Mode;
        LastSessionCompleted = !cancelled &&
            (SessionAmount >= TargetAmount - .01f || IsFull);
        SessionActive = false;
        IsServicing = false;
        TargetAmount = 0f;
        if (ActiveSession == this) ActiveSession = null;

        if (SessionAmount > .01f)
        {
            AnnounceVisitSummary(Localization.Get(cancelled ? "station.stopped" : "station.complete"));
            // The purchase is already in GameState; commit it to the existing save
            // now so a crash before the next background/quit save cannot hand the
            // player free fuel. EditMode tests never reach this branch.
            if (Application.isPlaying)
                SaveManager.EnsureExists().SaveNow("station_service", pushCloud: false);
        }
    }

    ServiceMode ResolveMode()
    {
        bool canCharge = supportsCharging && serviceBattery && battery != null;
        bool canFuel = supportsFuel && fuel != null;
        if (canCharge && battery.enabled) return ServiceMode.Charge;
        if (canFuel && fuel.enabled) return ServiceMode.Fuel;
        if (canCharge) return ServiceMode.Charge;
        if (canFuel) return ServiceMode.Fuel;
        return ServiceMode.None;
    }

    void Deduct(float price)
    {
        if (price <= 0f) return;
        var state = GameState.Instance;
        if (state == null || state.economy == null) return;
        state.economy.balanceKES = Mathf.Max(0f, state.economy.balanceKES - price);
    }

    void AnnounceFull(string message)
    {
        if (!logService || Time.unscaledTime < nextAnnouncementTime) return;
        nextAnnouncementTime = Time.unscaledTime + 4f;
        SubtitleManager.EnsureExists().Show("Fuel stop", message, 2.5f);
    }

    void AnnounceNoFunds()
    {
        if (!logService || Time.unscaledTime < nextAnnouncementTime) return;
        nextAnnouncementTime = Time.unscaledTime + 5f;
        SubtitleManager.EnsureExists().Show("Fuel stop", Localization.Get("station.noFunds"), 2.5f);
    }

    void AnnounceVisitSummary(string title = "Fuel stop")
    {
        if (!logService || SessionAmount <= .01f) return;
        bool charged = LastSessionMode == ServiceMode.Charge ||
            (LastSessionMode == ServiceMode.None && serviceBattery && battery != null);
        string unit = charged ? "kWh" : "L";
        SubtitleManager.EnsureExists().Show(title,
            $"{SessionAmount:N1} {unit} · KES {SessionCostKES:N0}", 3f);
    }

    void Unbind()
    {
        IsServicing = false;
        bus = null;
        fuel = null;
        battery = null;
        // The visit ends with the trigger, whether or not logging is enabled.
        // Leaving the totals behind would fold the next bus's purchase into the
        // previous visit's summary.
        SessionAmount = 0f;
        SessionCostKES = 0f;
        TargetAmount = 0f;
    }
}
