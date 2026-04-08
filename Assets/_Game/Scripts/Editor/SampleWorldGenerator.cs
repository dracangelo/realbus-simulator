#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class SampleWorldGenerator
{
    const string CitiesFolder = "Assets/_Game/ScriptableObjects/Cities";
    const string CountriesFolder = "Assets/_Game/ScriptableObjects/Countries";
    const string MainMenuScenePath = "Assets/_Game/Scenes/MainMenu.unity";

    [MenuItem("Tools/Real Bus Sim/Generate Sample World (5 Countries, 20 Cities)")]
    public static void GenerateSampleWorld()
    {
        EnsureFolder("Assets/_Game", "ScriptableObjects");
        EnsureFolder("Assets/_Game/ScriptableObjects", "Cities");
        EnsureFolder("Assets/_Game/ScriptableObjects", "Countries");

        var countryAssets = new List<CountryDefinition>();
        var cityAssets = new List<CityDefinition>();

        foreach (CountrySeed country in GetSeeds())
        {
            var citiesForCountry = new List<CityDefinition>();

            foreach (CitySeed city in country.cities)
            {
                CityDefinition cityAsset = CreateOrUpdateCity(country, city);
                citiesForCountry.Add(cityAsset);
                cityAssets.Add(cityAsset);
            }

            CountryDefinition countryAsset = CreateOrUpdateCountry(country, citiesForCountry.ToArray());
            countryAssets.Add(countryAsset);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        WireMainMenu(countryAssets.ToArray(), cityAssets.ToArray());

        EditorUtility.DisplayDialog(
            "Sample World Generated",
            $"Created or updated {countryAssets.Count} countries and {cityAssets.Count} cities, then assigned them to MainMenu's CityManager.",
            "OK");
    }

    static CityDefinition CreateOrUpdateCity(CountrySeed country, CitySeed city)
    {
        string assetPath = $"{CitiesFolder}/City_{Sanitize(country.countryName)}_{Sanitize(city.cityName)}.asset";
        CityDefinition asset = AssetDatabase.LoadAssetAtPath<CityDefinition>(assetPath);

        if (asset == null)
        {
            asset = ScriptableObject.CreateInstance<CityDefinition>();
            AssetDatabase.CreateAsset(asset, assetPath);
        }

        asset.cityName = city.cityName;
        asset.country = country.countryName;
        asset.cityCode = city.cityCode;
        asset.climateZone = city.climateZone;
        asset.avgTemperatureCelsius = city.avgTemperatureCelsius;
        asset.hasMonsoon = city.hasMonsoon;
        asset.centreLat = city.latitude;
        asset.centreLon = city.longitude;
        asset.minLat = city.latitude - 0.03;
        asset.maxLat = city.latitude + 0.03;
        asset.minLon = city.longitude - 0.03;
        asset.maxLon = city.longitude + 0.03;
        asset.roadsFileName = "roads.xml";
        asset.buildingsFileName = "buildings.xml";
        asset.availableRoutes = new BusRoute[0];
        asset.spawnLat = city.latitude;
        asset.spawnLon = city.longitude;
        asset.isDownloaded = true;
        asset.previewImagePath = "";
        asset.rainySeasonMonths = city.rainySeasonMonths;
        asset.snowSeasonMonths = city.snowSeasonMonths;

        EditorUtility.SetDirty(asset);
        return asset;
    }

    static CountryDefinition CreateOrUpdateCountry(CountrySeed country, CityDefinition[] cities)
    {
        string assetPath = $"{CountriesFolder}/Country_{Sanitize(country.countryName)}.asset";
        CountryDefinition asset = AssetDatabase.LoadAssetAtPath<CountryDefinition>(assetPath);

        if (asset == null)
        {
            asset = ScriptableObject.CreateInstance<CountryDefinition>();
            AssetDatabase.CreateAsset(asset, assetPath);
        }

        asset.countryName = country.countryName;
        asset.continent = country.continent;
        asset.countryCode = country.countryCode;
        asset.cities = cities;

        EditorUtility.SetDirty(asset);
        return asset;
    }

    static void WireMainMenu(CountryDefinition[] countries, CityDefinition[] cities)
    {
        var scene = EditorSceneManager.OpenScene(MainMenuScenePath, OpenSceneMode.Single);
        CityManager cityManager = Object.FindObjectOfType<CityManager>();

        if (cityManager == null)
        {
            Debug.LogError("SampleWorldGenerator: Could not find CityManager in MainMenu scene.");
            return;
        }

        cityManager.allCountries = countries;
        cityManager.allCities = cities;
        cityManager.activeCountry = countries.Length > 0 ? countries[0] : null;
        cityManager.activeCity = cities.Length > 0 ? cities[0] : null;
        EditorUtility.SetDirty(cityManager);

        GameState gameState = Object.FindObjectOfType<GameState>();
        if (gameState != null)
        {
            gameState.selectedCountry = cityManager.activeCountry;
            gameState.selectedCity = cityManager.activeCity;
            gameState.selectedRoute = null;
            EditorUtility.SetDirty(gameState);
        }

        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
        AssetDatabase.SaveAssets();
    }

    static void EnsureFolder(string parent, string child)
    {
        string path = $"{parent}/{child}";
        if (!AssetDatabase.IsValidFolder(path))
            AssetDatabase.CreateFolder(parent, child);
    }

    static string Sanitize(string value)
    {
        string sanitized = value.Replace(" ", "").Replace("-", "").Replace("'", "");
        sanitized = sanitized.Replace(".", "").Replace(",", "");
        return sanitized;
    }

    static CountrySeed[] GetSeeds()
    {
        return new[]
        {
            new CountrySeed(
                "Kenya", "Africa", "KE",
                new[]
                {
                    new CitySeed("Nairobi", "NBO", ClimateZone.Tropical, 22f, true, -1.2864, 36.8172, new[] { 3, 4, 5, 10, 11, 12 }, new int[0]),
                    new CitySeed("Mombasa", "MBA", ClimateZone.Tropical, 27f, true, -4.0435, 39.6682, new[] { 4, 5, 6, 10, 11 }, new int[0]),
                    new CitySeed("Kisumu", "KSM", ClimateZone.Tropical, 24f, true, -0.0917, 34.7680, new[] { 3, 4, 5, 9, 10, 11 }, new int[0]),
                    new CitySeed("Nakuru", "NKR", ClimateZone.Temperate, 19f, true, -0.3031, 36.0800, new[] { 3, 4, 5, 10, 11 }, new int[0])
                }),
            new CountrySeed(
                "Japan", "Asia", "JP",
                new[]
                {
                    new CitySeed("Tokyo", "TYO", ClimateZone.Temperate, 16f, true, 35.6762, 139.6503, new[] { 6, 7, 9, 10 }, new[] { 12, 1, 2 }),
                    new CitySeed("Osaka", "OSA", ClimateZone.Temperate, 17f, true, 34.6937, 135.5023, new[] { 6, 7, 9 }, new[] { 12, 1, 2 }),
                    new CitySeed("Nagoya", "NGY", ClimateZone.Temperate, 16f, true, 35.1815, 136.9066, new[] { 6, 7, 9 }, new[] { 12, 1, 2 }),
                    new CitySeed("Sapporo", "SPK", ClimateZone.Continental, 9f, false, 43.0618, 141.3545, new[] { 7, 8, 9 }, new[] { 11, 12, 1, 2, 3 })
                }),
            new CountrySeed(
                "Germany", "Europe", "DE",
                new[]
                {
                    new CitySeed("Berlin", "BER", ClimateZone.Continental, 11f, false, 52.5200, 13.4050, new[] { 6, 7, 8 }, new[] { 12, 1, 2 }),
                    new CitySeed("Hamburg", "HAM", ClimateZone.Continental, 10f, false, 53.5511, 9.9937, new[] { 6, 7, 8 }, new[] { 12, 1, 2 }),
                    new CitySeed("Munich", "MUC", ClimateZone.Continental, 10f, false, 48.1351, 11.5820, new[] { 5, 6, 7, 8 }, new[] { 12, 1, 2, 3 }),
                    new CitySeed("Frankfurt", "FRA", ClimateZone.Continental, 11f, false, 50.1109, 8.6821, new[] { 6, 7, 8 }, new[] { 12, 1, 2 })
                }),
            new CountrySeed(
                "United States", "Americas", "US",
                new[]
                {
                    new CitySeed("New York", "NYC", ClimateZone.Continental, 13f, false, 40.7128, -74.0060, new[] { 4, 5, 6, 7, 8 }, new[] { 12, 1, 2 }),
                    new CitySeed("Chicago", "CHI", ClimateZone.Continental, 11f, false, 41.8781, -87.6298, new[] { 4, 5, 6, 7, 8 }, new[] { 11, 12, 1, 2, 3 }),
                    new CitySeed("Seattle", "SEA", ClimateZone.Temperate, 12f, false, 47.6062, -122.3321, new[] { 1, 2, 3, 10, 11, 12 }, new[] { 12, 1 }),
                    new CitySeed("Dallas", "DAL", ClimateZone.Subtropical, 20f, false, 32.7767, -96.7970, new[] { 4, 5, 6 }, new int[0])
                }),
            new CountrySeed(
                "Australia", "Oceania", "AU",
                new[]
                {
                    new CitySeed("Sydney", "SYD", ClimateZone.Subtropical, 19f, false, -33.8688, 151.2093, new[] { 2, 3, 4, 6 }, new int[0]),
                    new CitySeed("Melbourne", "MEL", ClimateZone.Temperate, 15f, false, -37.8136, 144.9631, new[] { 3, 4, 5, 10 }, new int[0]),
                    new CitySeed("Brisbane", "BNE", ClimateZone.Subtropical, 21f, true, -27.4698, 153.0251, new[] { 1, 2, 12 }, new int[0]),
                    new CitySeed("Perth", "PER", ClimateZone.Mediterranean, 18f, false, -31.9505, 115.8605, new[] { 5, 6, 7 }, new int[0])
                })
        };
    }

    readonly struct CountrySeed
    {
        public readonly string countryName;
        public readonly string continent;
        public readonly string countryCode;
        public readonly CitySeed[] cities;

        public CountrySeed(string countryName, string continent, string countryCode, CitySeed[] cities)
        {
            this.countryName = countryName;
            this.continent = continent;
            this.countryCode = countryCode;
            this.cities = cities;
        }
    }

    readonly struct CitySeed
    {
        public readonly string cityName;
        public readonly string cityCode;
        public readonly ClimateZone climateZone;
        public readonly float avgTemperatureCelsius;
        public readonly bool hasMonsoon;
        public readonly double latitude;
        public readonly double longitude;
        public readonly int[] rainySeasonMonths;
        public readonly int[] snowSeasonMonths;

        public CitySeed(
            string cityName,
            string cityCode,
            ClimateZone climateZone,
            float avgTemperatureCelsius,
            bool hasMonsoon,
            double latitude,
            double longitude,
            int[] rainySeasonMonths,
            int[] snowSeasonMonths)
        {
            this.cityName = cityName;
            this.cityCode = cityCode;
            this.climateZone = climateZone;
            this.avgTemperatureCelsius = avgTemperatureCelsius;
            this.hasMonsoon = hasMonsoon;
            this.latitude = latitude;
            this.longitude = longitude;
            this.rainySeasonMonths = rainySeasonMonths;
            this.snowSeasonMonths = snowSeasonMonths;
        }
    }
}
#endif
