using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneBootstrap : MonoBehaviour
{
    [SerializeField] bool redirectIfNoManagers = true;
    [SerializeField] SceneCatalog sceneCatalog;

    void Awake()
    {
        if (redirectIfNoManagers && SceneLoader.Instance == null)
        {
            Debug.LogWarning("SceneBootstrap: No SceneLoader — creating one for direct scene testing.");
            var loader = new GameObject("SceneLoader");
            var sl = loader.AddComponent<SceneLoader>();
            if (sceneCatalog != null)
                sl.sceneCatalog = sceneCatalog;
        }

        EnsureCoreManagersForDirectPlay();
    }

    void EnsureCoreManagersForDirectPlay()
    {
        // Directly playing Country/City/Route scenes often skips MainMenu bootstrap.
        // Create essential managers so UI scenes can run standalone.
        if (GameState.Instance == null)
        {
            var go = new GameObject("GameState");
            go.AddComponent<GameState>();
        }

        if (CityManager.Instance == null)
        {
            var go = new GameObject("CityManager");
            go.AddComponent<CityManager>();
        }

#if UNITY_EDITOR
        SeedCityManagerFromAssetsInEditor();
#endif
    }

#if UNITY_EDITOR
    void SeedCityManagerFromAssetsInEditor()
    {
        var cityManager = CityManager.Instance;
        if (cityManager == null) return;
        if (cityManager.allCountries != null && cityManager.allCountries.Length > 0) return;

        string[] countryGuids = AssetDatabase.FindAssets("t:CountryDefinition");
        if (countryGuids == null || countryGuids.Length == 0) return;

        var countries = new List<CountryDefinition>();
        var allCities = new List<CityDefinition>();

        foreach (string guid in countryGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var country = AssetDatabase.LoadAssetAtPath<CountryDefinition>(path);
            if (!country) continue;
            countries.Add(country);

            if (country.cities == null) continue;
            foreach (var city in country.cities)
            {
                if (city != null)
                    allCities.Add(city);
            }
        }

        if (countries.Count == 0) return;

        cityManager.allCountries = countries.ToArray();
        cityManager.allCities = allCities.ToArray();
        cityManager.activeCountry = cityManager.allCountries[0];
        cityManager.activeCity = (cityManager.activeCountry.cities != null && cityManager.activeCountry.cities.Length > 0)
            ? cityManager.activeCountry.cities[0]
            : null;

        Debug.Log($"SceneBootstrap: Seeded CityManager with {cityManager.allCountries.Length} countries for direct scene play.");
    }
#endif
}