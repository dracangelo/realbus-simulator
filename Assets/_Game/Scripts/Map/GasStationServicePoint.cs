using UnityEngine;

/// <summary>
/// Drive-in service trigger for a fuel station. The bus does not have to stop on
/// a pad or open a menu: while its collider stays inside the trigger, fuel or
/// charge is added progressively and the cost is taken from the player balance.
/// Service stops when the tank is full or the balance runs out.
/// </summary>
[RequireComponent(typeof(Collider))]
public class GasStationServicePoint : MonoBehaviour
{
    [Header("Service")]
    [Tooltip("Litres per second while a diesel bus is inside the trigger.")]
    public float refuelLitresPerSecond = 22f;
    [Tooltip("kWh per second while an electric bus is inside the trigger.")]
    public float rechargeKWhPerSecond = 55f;
    [Tooltip("When the bus has a BatterySystem, charge it instead of refuelling.")]
    public bool serviceBattery = true;
    [Tooltip("Show the 'tank full' and visit-summary subtitles.")]
    public bool logService = true;

    FuelSystem fuel;
    BatterySystem battery;
    BusController bus;
    float nextAnnouncementTime;

    /// <summary>True while the last service tick actually added energy.</summary>
    public bool IsServicing { get; private set; }
    /// <summary>Litres or kWh added during the current visit.</summary>
    public float SessionAmount { get; private set; }
    /// <summary>KES deducted during the current visit.</summary>
    public float SessionCostKES { get; private set; }
    public BusController ServicingBus => bus;

    void Reset()
    {
        var collider = GetComponent<Collider>();
        if (collider != null) collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other) => BindBus(other);

    void OnTriggerStay(Collider other)
    {
        if (!BindBus(other)) return;
        Service(Time.deltaTime);
    }

    void OnTriggerExit(Collider other)
    {
        if (bus == null) return;
        if (other.GetComponentInParent<BusController>() != bus) return;
        AnnounceVisitSummary();
        Unbind();
    }

    void OnDisable() => Unbind();

    /// <summary>Amount a rate delivers in a time step. Pure, so it can be tested.</summary>
    public static float AmountFor(float ratePerSecond, float seconds)
        => Mathf.Max(0f, ratePerSecond) * Mathf.Max(0f, seconds);

    /// <summary>Cost of an amount at a unit price. Pure, so it can be tested.</summary>
    public static float PriceFor(float amount, float unitPrice)
        => Mathf.Max(0f, amount) * Mathf.Max(0f, unitPrice);

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
        if (bus == controller) return true;
        bus = controller;
        fuel = controller.GetComponent<FuelSystem>();
        battery = controller.GetComponent<BatterySystem>();
        return true;
    }

    /// <summary>One service step. Returns true when energy was actually added.</summary>
    public bool Service(float seconds)
    {
        IsServicing = false;
        if (bus == null || seconds <= 0f) return false;

        if (serviceBattery && battery != null)
            return ServiceBattery(seconds);
        if (fuel != null)
            return ServiceFuel(seconds);
        return false;
    }

    bool ServiceFuel(float seconds)
    {
        float missing = fuel.GetMissingFuelLitres();
        if (missing <= .01f)
        {
            AnnounceFull("Tank full");
            return false;
        }

        float unitPrice = fuel.dieselPricePerLitreKES;
        float affordable = AffordableAmount(unitPrice);
        if (affordable <= .0001f)
        {
            AnnounceNoFunds();
            return false;
        }

        float requested = Mathf.Min(AmountFor(refuelLitresPerSecond, seconds), missing);
        float added = fuel.Refuel(Mathf.Min(requested, affordable));
        if (added <= 0f) return false;

        float price = fuel.PriceForLitres(added);
        Deduct(price);
        SessionAmount += added;
        SessionCostKES += price;
        IsServicing = true;

        if (fuel.GetMissingFuelLitres() <= .01f)
            AnnounceFull($"Tank full · KES {SessionCostKES:N0}");
        return true;
    }

    bool ServiceBattery(float seconds)
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
            return false;
        }

        float requested = Mathf.Min(AmountFor(rechargeKWhPerSecond, seconds), missing);
        float added = battery.Recharge(Mathf.Min(requested, affordable));
        if (added <= 0f) return false;

        float price = battery.PriceForKWh(added);
        Deduct(price);
        SessionAmount += added;
        SessionCostKES += price;
        IsServicing = true;

        if (battery.GetMissingChargeKWh() <= .01f)
            AnnounceFull($"Battery full · KES {SessionCostKES:N0}");
        return true;
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
        SubtitleManager.EnsureExists().Show("Fuel stop", "Not enough balance to buy more fuel.", 2.5f);
    }

    void AnnounceVisitSummary()
    {
        if (!logService || SessionAmount <= .01f) return;
        string unit = serviceBattery && battery != null ? "kWh" : "L";
        SubtitleManager.EnsureExists().Show("Fuel stop",
            $"Serviced {SessionAmount:N1} {unit} · KES {SessionCostKES:N0}", 3f);
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
    }
}
