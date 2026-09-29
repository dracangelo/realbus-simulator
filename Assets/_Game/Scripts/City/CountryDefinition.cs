using UnityEngine;

public enum DrivingSide { Automatic, Left, Right }

[CreateAssetMenu(fileName = "CountryDefinition",
    menuName = "RealBus/Country Definition")]
public class CountryDefinition : ScriptableObject
{
    [Header("Country Info")]
    public string countryName;
    public string continent;
    public string countryCode; // 2-letter ISO e.g. KE, GB, JP
    public DrivingSide drivingSide = DrivingSide.Automatic;
    public bool DrivesOnLeft => drivingSide == DrivingSide.Left ||
        (drivingSide == DrivingSide.Automatic && DefaultDrivesOnLeft(countryCode));

    public static bool DefaultDrivesOnLeft(string code)
    {
        switch ((code ?? "").Trim().ToUpperInvariant())
        {
            case "KE": case "TZ": case "UG": case "GB": case "UK": case "IE":
            case "ZA": case "JP": case "AU": case "NZ": case "IN": case "BD":
            case "PK": case "LK": case "TH": case "MY": case "SG": case "ID":
            case "ZW": case "ZM": case "BW": case "NA": case "MW": case "MZ":
            case "LS": case "SZ": case "MU": case "MT": case "CY": return true;
            default: return false;
        }
    }

    [Header("Cities")]
    public CityDefinition[] cities;
}
