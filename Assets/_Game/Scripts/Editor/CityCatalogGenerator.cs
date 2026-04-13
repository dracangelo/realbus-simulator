#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CityCatalogGenerator : EditorWindow
{
    const string CitiesFolder = "Assets/_Game/ScriptableObjects/Cities";
    const string CountriesFolder = "Assets/_Game/ScriptableObjects/Countries";
    const string MainMenuScenePath = "Assets/_Game/Scenes/MainMenu.unity";

    string searchQuery = "";
    bool wireMainMenu = true;
    Vector2 scroll;

    [MenuItem("Tools/RealBus/Cities/City Catalog Generator")]
    public static void Open()
    {
        GetWindow<CityCatalogGenerator>("City Catalog");
    }

    void OnGUI()
    {
        GUILayout.Label("City Catalog Generator", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "Search by city or country, then create CityDefinition assets in bulk. " +
            "This uses a built-in catalog so it works offline.",
            MessageType.Info);

        searchQuery = EditorGUILayout.TextField("Search", searchQuery);
        wireMainMenu = EditorGUILayout.Toggle("Wire MainMenu", wireMainMenu);

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Add Matching Cities"))
                CreateCities(GetFilteredCatalog());
            if (GUILayout.Button("Add Matching Country Groups"))
                CreateCountriesFromFilteredCatalog();
            if (GUILayout.Button("Add Full Catalog"))
                CreateCities(GetCatalog());
        }

        EditorGUILayout.Space();
        GUILayout.Label($"Matches: {GetFilteredCatalog().Count}", EditorStyles.boldLabel);

        scroll = EditorGUILayout.BeginScrollView(scroll);
        foreach (var seed in GetFilteredCatalog())
            EditorGUILayout.LabelField($"{seed.countryName} / {seed.cityName} ({seed.cityCode})");
        EditorGUILayout.EndScrollView();
    }

    void CreateCountriesFromFilteredCatalog()
    {
        var filtered = GetFilteredCatalog();
        var countries = filtered.Select(s => s.countryName).Distinct().ToList();
        var seeds = GetCatalog().Where(s => countries.Contains(s.countryName)).ToList();
        CreateCities(seeds);
    }

    void CreateCities(List<CityCatalogSeed> seeds)
    {
        if (seeds == null || seeds.Count == 0)
        {
            EditorUtility.DisplayDialog("No Matches", "No cities matched the current search.", "OK");
            return;
        }

        EnsureFolder("Assets/_Game", "ScriptableObjects");
        EnsureFolder("Assets/_Game/ScriptableObjects", "Cities");
        EnsureFolder("Assets/_Game/ScriptableObjects", "Countries");

        int createdOrUpdated = 0;
        foreach (var seed in seeds)
        {
            CreateOrUpdateCity(seed);
            createdOrUpdated++;
        }

        RefreshCountryAssets();
        if (wireMainMenu)
            WireMainMenu();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog(
            "City Catalog Applied",
            $"Created or updated {createdOrUpdated} city assets.\nCountries were regenerated from all existing cities.",
            "OK");
    }

    static void CreateOrUpdateCity(CityCatalogSeed seed)
    {
        string assetPath = $"{CitiesFolder}/City_{Sanitize(seed.countryName)}_{Sanitize(seed.cityName)}.asset";
        CityDefinition asset = AssetDatabase.LoadAssetAtPath<CityDefinition>(assetPath);
        if (asset == null)
        {
            asset = ScriptableObject.CreateInstance<CityDefinition>();
            AssetDatabase.CreateAsset(asset, assetPath);
        }

        asset.cityName = seed.cityName;
        asset.country = seed.countryName;
        asset.cityCode = seed.cityCode;
        asset.climateZone = seed.climateZone;
        asset.avgTemperatureCelsius = seed.avgTemperatureCelsius;
        asset.hasMonsoon = seed.hasMonsoon;
        asset.centreLat = seed.latitude;
        asset.centreLon = seed.longitude;
        asset.minLat = seed.latitude - seed.latExtent;
        asset.maxLat = seed.latitude + seed.latExtent;
        asset.minLon = seed.longitude - seed.lonExtent;
        asset.maxLon = seed.longitude + seed.lonExtent;
        asset.roadsFileName = "roads.xml";
        asset.buildingsFileName = "buildings.xml";
        asset.availableRoutes = new BusRoute[0];
        asset.spawnLat = seed.latitude;
        asset.spawnLon = seed.longitude;
        asset.isDownloaded = true;
        asset.previewImagePath = "";
        asset.rainySeasonMonths = seed.rainySeasonMonths;
        asset.snowSeasonMonths = seed.snowSeasonMonths;

        EditorUtility.SetDirty(asset);
    }

    static void RefreshCountryAssets()
    {
        string[] guids = AssetDatabase.FindAssets("t:CityDefinition");
        var grouped = new Dictionary<string, List<CityDefinition>>();
        var countryMeta = new Dictionary<string, (string continent, string countryCode)>();

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var city = AssetDatabase.LoadAssetAtPath<CityDefinition>(path);
            if (city == null)
                continue;

            if (!grouped.ContainsKey(city.country))
                grouped[city.country] = new List<CityDefinition>();

            grouped[city.country].Add(city);

            var seed = GetCatalog().FirstOrDefault(s => s.countryName == city.country);
            if (!countryMeta.ContainsKey(city.country))
                countryMeta[city.country] = (seed.continent ?? "Other", seed.countryCode ?? "");
        }

        foreach (var pair in grouped)
        {
            string assetPath = $"{CountriesFolder}/Country_{Sanitize(pair.Key)}.asset";
            CountryDefinition asset = AssetDatabase.LoadAssetAtPath<CountryDefinition>(assetPath);
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<CountryDefinition>();
                AssetDatabase.CreateAsset(asset, assetPath);
            }

            asset.countryName = pair.Key;
            asset.continent = countryMeta[pair.Key].continent;
            asset.countryCode = countryMeta[pair.Key].countryCode;
            asset.cities = pair.Value.OrderBy(c => c.cityName).ToArray();
            EditorUtility.SetDirty(asset);
        }
    }

    static void WireMainMenu()
    {
        var scene = EditorSceneManager.OpenScene(MainMenuScenePath, OpenSceneMode.Single);
        CityManager cityManager = Object.FindObjectOfType<CityManager>();
        if (cityManager == null)
            return;

        var countries = LoadAllAssets<CountryDefinition>("t:CountryDefinition");
        var cities = LoadAllAssets<CityDefinition>("t:CityDefinition");

        cityManager.allCountries = countries.OrderBy(c => c.countryName).ToArray();
        cityManager.allCities = cities.OrderBy(c => c.country).ThenBy(c => c.cityName).ToArray();
        if (cityManager.activeCountry == null && cityManager.allCountries.Length > 0)
            cityManager.activeCountry = cityManager.allCountries[0];
        if (cityManager.activeCity == null && cityManager.allCities.Length > 0)
            cityManager.activeCity = cityManager.allCities[0];
        EditorUtility.SetDirty(cityManager);

        GameState gameState = Object.FindObjectOfType<GameState>();
        if (gameState != null)
        {
            if (gameState.selectedCountry == null)
                gameState.selectedCountry = cityManager.activeCountry;
            if (gameState.selectedCity == null)
                gameState.selectedCity = cityManager.activeCity;
            EditorUtility.SetDirty(gameState);
        }

        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
    }

    static List<T> LoadAllAssets<T>(string filter) where T : Object
    {
        var result = new List<T>();
        string[] guids = AssetDatabase.FindAssets(filter);
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset != null)
                result.Add(asset);
        }
        return result;
    }

    List<CityCatalogSeed> GetFilteredCatalog()
    {
        var catalog = GetCatalog();
        if (string.IsNullOrWhiteSpace(searchQuery))
            return catalog.OrderBy(c => c.countryName).ThenBy(c => c.cityName).ToList();

        string q = searchQuery.Trim().ToLowerInvariant();
        return catalog
            .Where(c =>
                c.cityName.ToLowerInvariant().Contains(q) ||
                c.countryName.ToLowerInvariant().Contains(q) ||
                c.cityCode.ToLowerInvariant().Contains(q) ||
                c.countryCode.ToLowerInvariant().Contains(q))
            .OrderBy(c => c.countryName)
            .ThenBy(c => c.cityName)
            .ToList();
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
        sanitized = sanitized.Replace(".", "").Replace(",", "").Replace("&", "And");
        return sanitized;
    }

    static List<CityCatalogSeed> GetCatalog()
    {
        return new List<CityCatalogSeed>
        {
            new CityCatalogSeed("Kenya", "Africa", "KE", "Nairobi", "NBO", ClimateZone.Tropical, 22f, true, -1.2864, 36.8172, 0.03, 0.03, new[] { 3, 4, 5, 10, 11, 12 }, new int[0]),
            new CityCatalogSeed("Kenya", "Africa", "KE", "Mombasa", "MBA", ClimateZone.Tropical, 27f, true, -4.0435, 39.6682, 0.03, 0.03, new[] { 4, 5, 6, 10, 11 }, new int[0]),
            new CityCatalogSeed("Kenya", "Africa", "KE", "Kisumu", "KSM", ClimateZone.Tropical, 24f, true, -0.0917, 34.7680, 0.03, 0.03, new[] { 3, 4, 5, 9, 10, 11 }, new int[0]),
            new CityCatalogSeed("Kenya", "Africa", "KE", "Nakuru", "NKR", ClimateZone.Temperate, 19f, true, -0.3031, 36.0800, 0.03, 0.03, new[] { 3, 4, 5, 10, 11 }, new int[0]),
            new CityCatalogSeed("Uganda", "Africa", "UG", "Kampala", "KLA", ClimateZone.Tropical, 23f, true, 0.3476, 32.5825, 0.03, 0.03, new[] { 3, 4, 5, 9, 10, 11 }, new int[0]),
            new CityCatalogSeed("Tanzania", "Africa", "TZ", "Dar es Salaam", "DAR", ClimateZone.Tropical, 26f, true, -6.7924, 39.2083, 0.03, 0.03, new[] { 3, 4, 5, 11, 12 }, new int[0]),
            new CityCatalogSeed("Rwanda", "Africa", "RW", "Kigali", "KGL", ClimateZone.Tropical, 20f, true, -1.9441, 30.0619, 0.025, 0.025, new[] { 3, 4, 10, 11 }, new int[0]),
            new CityCatalogSeed("Egypt", "Africa", "EG", "Cairo", "CAI", ClimateZone.Desert, 23f, false, 30.0444, 31.2357, 0.04, 0.04, new int[0], new int[0]),
            new CityCatalogSeed("South Africa", "Africa", "ZA", "Cape Town", "CPT", ClimateZone.Mediterranean, 17f, false, -33.9249, 18.4241, 0.03, 0.03, new[] { 5, 6, 7, 8 }, new int[0]),
            new CityCatalogSeed("Japan", "Asia", "JP", "Tokyo", "TYO", ClimateZone.Temperate, 16f, true, 35.6762, 139.6503, 0.03, 0.03, new[] { 6, 7, 9, 10 }, new[] { 12, 1, 2 }),
            new CityCatalogSeed("Japan", "Asia", "JP", "Osaka", "OSA", ClimateZone.Temperate, 17f, true, 34.6937, 135.5023, 0.03, 0.03, new[] { 6, 7, 9 }, new[] { 12, 1, 2 }),
            new CityCatalogSeed("Japan", "Asia", "JP", "Nagoya", "NGY", ClimateZone.Temperate, 16f, true, 35.1815, 136.9066, 0.03, 0.03, new[] { 6, 7, 9 }, new[] { 12, 1, 2 }),
            new CityCatalogSeed("Japan", "Asia", "JP", "Sapporo", "SPK", ClimateZone.Continental, 9f, false, 43.0618, 141.3545, 0.03, 0.03, new[] { 7, 8, 9 }, new[] { 11, 12, 1, 2, 3 }),
            new CityCatalogSeed("India", "Asia", "IN", "Delhi", "DEL", ClimateZone.Arid, 25f, true, 28.6139, 77.2090, 0.04, 0.04, new[] { 7, 8 }, new int[0]),
            new CityCatalogSeed("India", "Asia", "IN", "Mumbai", "BOM", ClimateZone.Tropical, 27f, true, 19.0760, 72.8777, 0.035, 0.035, new[] { 6, 7, 8, 9 }, new int[0]),
            new CityCatalogSeed("Bangladesh", "Asia", "BD", "Dhaka", "DAC", ClimateZone.Subtropical, 26f, true, 23.8103, 90.4125, 0.03, 0.03, new[] { 5, 6, 7, 8, 9 }, new int[0]),
            new CityCatalogSeed("Germany", "Europe", "DE", "Berlin", "BER", ClimateZone.Continental, 11f, false, 52.5200, 13.4050, 0.03, 0.03, new[] { 6, 7, 8 }, new[] { 12, 1, 2 }),
            new CityCatalogSeed("Germany", "Europe", "DE", "Hamburg", "HAM", ClimateZone.Continental, 10f, false, 53.5511, 9.9937, 0.03, 0.03, new[] { 6, 7, 8 }, new[] { 12, 1, 2 }),
            new CityCatalogSeed("Germany", "Europe", "DE", "Munich", "MUC", ClimateZone.Continental, 10f, false, 48.1351, 11.5820, 0.03, 0.03, new[] { 5, 6, 7, 8 }, new[] { 12, 1, 2, 3 }),
            new CityCatalogSeed("Germany", "Europe", "DE", "Frankfurt", "FRA", ClimateZone.Continental, 11f, false, 50.1109, 8.6821, 0.03, 0.03, new[] { 6, 7, 8 }, new[] { 12, 1, 2 }),
            new CityCatalogSeed("United Kingdom", "Europe", "GB", "London", "LON", ClimateZone.Temperate, 12f, false, 51.5074, -0.1278, 0.04, 0.04, new[] { 10, 11, 12, 1 }, new int[0]),
            new CityCatalogSeed("France", "Europe", "FR", "Paris", "PAR", ClimateZone.Temperate, 12f, false, 48.8566, 2.3522, 0.035, 0.035, new[] { 10, 11, 12, 1 }, new int[0]),
            new CityCatalogSeed("Netherlands", "Europe", "NL", "Amsterdam", "AMS", ClimateZone.Temperate, 11f, false, 52.3676, 4.9041, 0.03, 0.03, new[] { 10, 11, 12, 1 }, new int[0]),
            new CityCatalogSeed("Spain", "Europe", "ES", "Madrid", "MAD", ClimateZone.Mediterranean, 15f, false, 40.4168, -3.7038, 0.035, 0.035, new[] { 10, 11, 12 }, new int[0]),
            new CityCatalogSeed("United States", "Americas", "US", "New York", "NYC", ClimateZone.Continental, 13f, false, 40.7128, -74.0060, 0.04, 0.04, new[] { 4, 5, 6, 7, 8 }, new[] { 12, 1, 2 }),
            new CityCatalogSeed("United States", "Americas", "US", "Chicago", "CHI", ClimateZone.Continental, 11f, false, 41.8781, -87.6298, 0.035, 0.035, new[] { 4, 5, 6, 7, 8 }, new[] { 11, 12, 1, 2, 3 }),
            new CityCatalogSeed("United States", "Americas", "US", "Seattle", "SEA", ClimateZone.Temperate, 12f, false, 47.6062, -122.3321, 0.03, 0.03, new[] { 1, 2, 3, 10, 11, 12 }, new[] { 12, 1 }),
            new CityCatalogSeed("United States", "Americas", "US", "Dallas", "DAL", ClimateZone.Subtropical, 20f, false, 32.7767, -96.7970, 0.03, 0.03, new[] { 4, 5, 6 }, new int[0]),
            new CityCatalogSeed("Canada", "Americas", "CA", "Toronto", "YTO", ClimateZone.Continental, 9f, false, 43.6532, -79.3832, 0.03, 0.03, new[] { 4, 5, 6, 7, 8 }, new[] { 12, 1, 2, 3 }),
            new CityCatalogSeed("Brazil", "Americas", "BR", "Sao Paulo", "SAO", ClimateZone.Subtropical, 20f, true, -23.5505, -46.6333, 0.04, 0.04, new[] { 11, 12, 1, 2, 3 }, new int[0]),
            new CityCatalogSeed("Mexico", "Americas", "MX", "Mexico City", "MEX", ClimateZone.Subtropical, 17f, true, 19.4326, -99.1332, 0.04, 0.04, new[] { 6, 7, 8, 9 }, new int[0]),
            new CityCatalogSeed("Australia", "Oceania", "AU", "Sydney", "SYD", ClimateZone.Subtropical, 19f, false, -33.8688, 151.2093, 0.03, 0.03, new[] { 2, 3, 4, 6 }, new int[0]),
            new CityCatalogSeed("Australia", "Oceania", "AU", "Melbourne", "MEL", ClimateZone.Temperate, 15f, false, -37.8136, 144.9631, 0.03, 0.03, new[] { 3, 4, 5, 10 }, new int[0]),
            new CityCatalogSeed("Australia", "Oceania", "AU", "Brisbane", "BNE", ClimateZone.Subtropical, 21f, true, -27.4698, 153.0251, 0.03, 0.03, new[] { 1, 2, 12 }, new int[0]),
            new CityCatalogSeed("Australia", "Oceania", "AU", "Perth", "PER", ClimateZone.Mediterranean, 18f, false, -31.9505, 115.8605, 0.03, 0.03, new[] { 5, 6, 7 }, new int[0]),
            new CityCatalogSeed("New Zealand", "Oceania", "NZ", "Auckland", "AKL", ClimateZone.Temperate, 15f, false, -36.8485, 174.7633, 0.03, 0.03, new[] { 6, 7, 8 }, new int[0]),
        };
    }

    readonly struct CityCatalogSeed
    {
        public readonly string countryName;
        public readonly string continent;
        public readonly string countryCode;
        public readonly string cityName;
        public readonly string cityCode;
        public readonly ClimateZone climateZone;
        public readonly float avgTemperatureCelsius;
        public readonly bool hasMonsoon;
        public readonly double latitude;
        public readonly double longitude;
        public readonly double latExtent;
        public readonly double lonExtent;
        public readonly int[] rainySeasonMonths;
        public readonly int[] snowSeasonMonths;

        public CityCatalogSeed(
            string countryName,
            string continent,
            string countryCode,
            string cityName,
            string cityCode,
            ClimateZone climateZone,
            float avgTemperatureCelsius,
            bool hasMonsoon,
            double latitude,
            double longitude,
            double latExtent,
            double lonExtent,
            int[] rainySeasonMonths,
            int[] snowSeasonMonths)
        {
            this.countryName = countryName;
            this.continent = continent;
            this.countryCode = countryCode;
            this.cityName = cityName;
            this.cityCode = cityCode;
            this.climateZone = climateZone;
            this.avgTemperatureCelsius = avgTemperatureCelsius;
            this.hasMonsoon = hasMonsoon;
            this.latitude = latitude;
            this.longitude = longitude;
            this.latExtent = latExtent;
            this.lonExtent = lonExtent;
            this.rainySeasonMonths = rainySeasonMonths;
            this.snowSeasonMonths = snowSeasonMonths;
        }
    }
}
#endif
