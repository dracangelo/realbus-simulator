using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    [Header("Player Selections")]
    public CountryDefinition selectedCountry;
    public CityDefinition selectedCity;
    public BusRoute selectedRoute;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SelectCountry(CountryDefinition country)
    {
        selectedCountry = country;
        selectedCity = null; // reset city when country changes
        selectedRoute = null;
        Debug.Log($"GameState: Country selected — {country.countryName}");
    }

    public void SelectCity(CityDefinition city)
    {
        selectedCity = city;
        selectedRoute = null;
        Debug.Log($"GameState: City selected — {city.cityName}");
    }

    public void SelectRoute(BusRoute route)
    {
        selectedRoute = route;
        Debug.Log($"GameState: Route selected — {route.routeName}");
    }
}