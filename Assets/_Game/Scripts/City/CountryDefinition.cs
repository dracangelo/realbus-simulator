using UnityEngine;

[CreateAssetMenu(fileName = "CountryDefinition",
    menuName = "RealBus/Country Definition")]
public class CountryDefinition : ScriptableObject
{
    [Header("Country Info")]
    public string countryName;
    public string continent;
    public string countryCode; // 2-letter ISO e.g. KE, GB, JP

    [Header("Cities")]
    public CityDefinition[] cities;
}