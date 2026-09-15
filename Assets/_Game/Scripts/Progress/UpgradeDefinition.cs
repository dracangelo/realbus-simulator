using System;
using UnityEngine;

public enum BusUpgradeTarget
{
    None = -1,
    EngineTorque = 0,
    BrakeTorque = 1,
    EnergyEfficiency = 2,
    SteeringResponse = 3,
    EnergyCapacity = 4
}

[CreateAssetMenu(fileName = "BusUpgrade", menuName = "RealBus/Progression/Bus Upgrade")]
public class UpgradeDefinition : ScriptableObject
{
    public string upgradeId = "upgrade.engine_tune";
    public string displayName = "Engine Tune";
    [TextArea(2, 4)] public string description = "Improves usable power across the rev range.";
    public BusUpgradeTarget target = BusUpgradeTarget.EngineTorque;
    [Min(0.01f)] public float multiplier = 1.1f;
    public BusUpgradeTarget secondaryTarget = BusUpgradeTarget.None;
    [Min(0.01f)] public float secondaryMultiplier = 1f;
    [Min(0)] public int priceKES = 35000;
    [Range(1, 10)] public int requiredRank = 1;
}

[Serializable]
public struct BusUpgradeModifiers
{
    public float engineTorque;
    public float brakeTorque;
    public float energyConsumption;
    public float steeringResponse;
    public float energyCapacity;

    public static BusUpgradeModifiers Identity => new BusUpgradeModifiers
    {
        engineTorque = 1f,
        brakeTorque = 1f,
        energyConsumption = 1f,
        steeringResponse = 1f,
        energyCapacity = 1f
    };

    public void Apply(UpgradeDefinition definition)
    {
        if (definition == null)
            return;

        float value = Mathf.Max(0.01f, definition.multiplier);
        switch (definition.target)
        {
            case BusUpgradeTarget.EngineTorque: engineTorque *= value; break;
            case BusUpgradeTarget.BrakeTorque: brakeTorque *= value; break;
            case BusUpgradeTarget.EnergyEfficiency: energyConsumption *= value; break;
            case BusUpgradeTarget.SteeringResponse: steeringResponse *= value; break;
            case BusUpgradeTarget.EnergyCapacity: energyCapacity *= value; break;
        }
        ApplyTarget(definition.secondaryTarget, Mathf.Max(0.01f, definition.secondaryMultiplier));
    }

    void ApplyTarget(BusUpgradeTarget target, float value)
    {
        switch (target)
        {
            case BusUpgradeTarget.EngineTorque: engineTorque *= value; break;
            case BusUpgradeTarget.BrakeTorque: brakeTorque *= value; break;
            case BusUpgradeTarget.EnergyEfficiency: energyConsumption *= value; break;
            case BusUpgradeTarget.SteeringResponse: steeringResponse *= value; break;
            case BusUpgradeTarget.EnergyCapacity: energyCapacity *= value; break;
        }
    }
}
