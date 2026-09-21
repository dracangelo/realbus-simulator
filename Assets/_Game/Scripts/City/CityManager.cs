using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Central registry and switcher for cities and countries (section 3.8).
///
/// Improvements vs original:
///   • Event-driven — <see cref="OnCityChanged"/> / <see cref="OnCountryChanged"/>
///     so UI, CoordinateConverter, TileStreamManager etc. don't need to poll.
///   • Internal List&lt;T&gt; instead of arrays for O(1) Add without copy-expand.
///   • Array properties (<see cref="allCities"/> / <see cref="allCountries"/>) 
///     are kept for Inspector serialisation; they stay in sync automatically.
///   • <see cref="RegisterCity"/> is idempotent by city code.
/// </summary>
public class CityManager : MonoBehaviour
{
    public static CityManager Instance { get; private set; }

    // ── Inspector ──────────────────────────────────────────────────────

    [Header("Countries")]
    public CountryDefinition[] allCountries = System.Array.Empty<CountryDefinition>();

    [Header("Cities")]
    public CityDefinition[] allCities = System.Array.Empty<CityDefinition>();

    [Header("Active")]
    public CityDefinition  activeCity;
    public CountryDefinition activeCountry;

    // ── Events ─────────────────────────────────────────────────────────

    /// <summary>Fired whenever <see cref="activeCity"/> changes.</summary>
    public static event System.Action<CityDefinition> OnCityChanged;

    /// <summary>Fired whenever <see cref="activeCountry"/> changes.</summary>
    public static event System.Action<CountryDefinition> OnCountryChanged;

    // ── Private ────────────────────────────────────────────────────────

    readonly List<CityDefinition>    _cities    = new();
    readonly List<CountryDefinition> _countries = new();

    // ── Lifecycle ──────────────────────────────────────────────────────

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Seed internal lists from Inspector arrays.
        _cities.AddRange(allCities);
        _countries.AddRange(allCountries);
    }

    void Start()
    {
        // Default selections.
        if (activeCity == null && _cities.Count > 0)
            SetActiveCity(_cities[0]);

        if (activeCountry == null && _countries.Count > 0)
            SetActiveCountry(_countries[0]);

        if (activeCity != null)
            Debug.Log($"[CityManager] Active city: {activeCity.cityName}, {activeCity.country}");
        else
            Debug.LogWarning("[CityManager] No active city set.");
    }

    // ── Public API ─────────────────────────────────────────────────────

    public void SetActiveCity(CityDefinition city)
    {
        if (city == null)
        {
            Debug.LogWarning("[CityManager] Cannot set a null active city.");
            return;
        }

        if (activeCity == city) return; // already active — no event spam

        activeCity = city;
        Debug.Log($"[CityManager] Switched to {city.cityName}");
        OnCityChanged?.Invoke(city);
    }

    public void SetActiveCountry(CountryDefinition country)
    {
        if (country == null) return;
        if (activeCountry == country) return;

        activeCountry = country;
        if (!CountryContainsCity(country, activeCity))
            activeCity = null;
        Debug.Log($"[CityManager] Country: {country.countryName}");
        OnCountryChanged?.Invoke(country);
    }

    static bool CountryContainsCity(CountryDefinition country, CityDefinition city)
    {
        if (country == null || city == null || country.cities == null) return false;
        for (int i = 0; i < country.cities.Length; i++)
            if (country.cities[i] == city) return true;
        return false;
    }

    /// <summary>
    /// Adds a city to the registry (or replaces by city code).
    /// Idempotent — safe to call multiple times with the same city.
    /// </summary>
    public void RegisterCity(CityDefinition city)
    {
        if (city == null) return;

        for (int i = 0; i < _cities.Count; i++)
        {
            if (_cities[i] == city) return; // already registered by reference

            if (_cities[i] != null
                && !string.IsNullOrWhiteSpace(city.cityCode)
                && string.Equals(_cities[i].cityCode, city.cityCode,
                                 System.StringComparison.OrdinalIgnoreCase))
            {
                _cities[i] = city;   // replace stale entry with same code
                SyncInspectorArrays();
                return;
            }
        }

        _cities.Add(city);
        SyncInspectorArrays();
    }

    public void RegisterCountry(CountryDefinition country)
    {
        if (country == null) return;
        if (_countries.Contains(country)) return;
        _countries.Add(country);
        SyncInspectorArrays();
    }

    // ── Queries ────────────────────────────────────────────────────────

    public CityDefinition GetCityByCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code)) return null;
        foreach (var c in _cities)
            if (c != null && string.Equals(c.cityCode, code, System.StringComparison.OrdinalIgnoreCase))
                return c;
        return null;
    }

    public CityDefinition[] GetCitiesForCountry(CountryDefinition country)
    {
        if (country?.cities == null) return System.Array.Empty<CityDefinition>();
        return country.cities;
    }

    public CountryDefinition[] GetCountriesByContinent(string continent)
    {
        if (string.IsNullOrWhiteSpace(continent)) return System.Array.Empty<CountryDefinition>();
        var result = new List<CountryDefinition>();
        foreach (var c in _countries)
            if (c != null && c.continent == continent)
                result.Add(c);
        return result.ToArray();
    }

    /// <summary>All registered cities as a read-only list.</summary>
    public IReadOnlyList<CityDefinition> Cities => _cities;

    /// <summary>All registered countries as a read-only list.</summary>
    public IReadOnlyList<CountryDefinition> Countries => _countries;

    // ── Helpers ────────────────────────────────────────────────────────

    /// <summary>
    /// Keeps the Inspector-visible arrays in sync with the internal lists so
    /// values show correctly in the Inspector at runtime.
    /// </summary>
    void SyncInspectorArrays()
    {
        allCities    = _cities.ToArray();
        allCountries = _countries.ToArray();
    }
}
