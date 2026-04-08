using UnityEngine;

public class CityManager : MonoBehaviour
{
    public static CityManager Instance { get; private set; }

    [Header("Countries")]
    public CountryDefinition[] allCountries;

    [Header("Cities")]
    public CityDefinition[] allCities;

    [Header("Active Selections")]
    public CityDefinition activeCity;
    public CountryDefinition activeCountry;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (activeCity == null && allCities != null && allCities.Length > 0)
            activeCity = allCities[0];

        if (activeCountry == null && allCountries != null && allCountries.Length > 0)
            activeCountry = allCountries[0];

        if (activeCity != null)
            Debug.Log($"CityManager: Active city — {activeCity.cityName}, {activeCity.country}");
        else
            Debug.LogWarning("CityManager: No active city set!");
    }

    public void SetActiveCity(CityDefinition city)
    {
        activeCity = city;
        Debug.Log($"CityManager: Switched to {city.cityName}");
    }

    public void SetActiveCountry(CountryDefinition country)
    {
        activeCountry = country;
        Debug.Log($"CityManager: Country set to {country.countryName}");
    }

    public CityDefinition[] GetCitiesForCountry(CountryDefinition country)
    {
        return country?.cities ?? new CityDefinition[0];
    }

    public CountryDefinition[] GetCountriesByContinent(string continent)
    {
        if (allCountries == null) return new CountryDefinition[0];
        var result = new System.Collections.Generic.List<CountryDefinition>();
        foreach (var c in allCountries)
            if (c != null && c.continent == continent)
                result.Add(c);
        return result.ToArray();
    }
}