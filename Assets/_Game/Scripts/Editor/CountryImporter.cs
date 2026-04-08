#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Editor tool — creates CountryDefinition assets from existing CityDefinition assets.
/// Run from Tools → RealBus → Generate Countries
/// </summary>
public class CountryImporter : EditorWindow
{
    [MenuItem("Tools/RealBus/Generate Countries from Cities")]
    public static void ShowWindow()
    {
        GetWindow<CountryImporter>("Country Generator");
    }

    void OnGUI()
    {
        GUILayout.Label("RealBus Country Generator", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        GUILayout.Label("This tool reads all CityDefinition assets and\n" +
                        "auto-creates CountryDefinition assets grouped by country.",
                        EditorStyles.helpBox);
        EditorGUILayout.Space();

        if (GUILayout.Button("Generate Countries", GUILayout.Height(40)))
            GenerateCountries();
    }

    static void GenerateCountries()
    {
        // Find all CityDefinition assets
        string[] guids = AssetDatabase.FindAssets("t:CityDefinition");
        var cityMap = new Dictionary<string, List<CityDefinition>>();
        var continentMap = new Dictionary<string, string>();
        var codeMap = new Dictionary<string, string>();

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var city = AssetDatabase.LoadAssetAtPath<CityDefinition>(path);
            if (city == null) continue;

            string country = city.country;
            if (!cityMap.ContainsKey(country))
            {
                cityMap[country] = new List<CityDefinition>();
                continentMap[country] = GetContinent(country);
                codeMap[country] = GetCountryCode(country);
            }
            cityMap[country].Add(city);
        }

        // Create output folder
        string folderPath = "Assets/_Game/ScriptableObjects/Countries";
        if (!AssetDatabase.IsValidFolder(folderPath))
            AssetDatabase.CreateFolder(
                "Assets/_Game/ScriptableObjects", "Countries");

        int created = 0;
        int updated = 0;

        foreach (var kvp in cityMap)
        {
            string country = kvp.Key;
            string assetName = $"Country_{country.Replace(" ", "")}";
            string assetPath = $"{folderPath}/{assetName}.asset";

            // Check if already exists
            var existing = AssetDatabase.LoadAssetAtPath<CountryDefinition>(assetPath);

            if (existing == null)
            {
                var asset = ScriptableObject.CreateInstance<CountryDefinition>();
                asset.countryName = country;
                asset.continent = continentMap[country];
                asset.countryCode = codeMap[country];
                asset.cities = kvp.Value.ToArray();

                AssetDatabase.CreateAsset(asset, assetPath);
                created++;
                Debug.Log($"Created: {assetName} ({kvp.Value.Count} cities)");
            }
            else
            {
                // Update existing
                existing.countryName = country;
                existing.continent = continentMap[country];
                existing.countryCode = codeMap[country];
                existing.cities = kvp.Value.ToArray();
                EditorUtility.SetDirty(existing);
                updated++;
                Debug.Log($"Updated: {assetName} ({kvp.Value.Count} cities)");
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Done!",
            $"Generated {created} new countries, updated {updated} existing.\n" +
            $"Total: {cityMap.Count} countries from {guids.Length} cities.",
            "OK");
    }

    static string GetContinent(string country)
    {
        var africa = new HashSet<string> {
            "Kenya", "Uganda", "Tanzania", "Rwanda", "Egypt",
            "South Africa", "Ethiopia", "Ghana", "Nigeria",
            "Senegal", "Cameroon", "Mozambique", "Zambia",
            "Zimbabwe", "Angola", "Mali", "Burkina Faso",
            "Niger", "Chad", "Sudan", "Somalia", "DRC",
            "Ivory Coast", "Guinea", "Benin", "Togo"
        };
        var europe = new HashSet<string> {
            "United Kingdom", "France", "Germany", "Italy",
            "Spain", "Netherlands", "Sweden", "Norway",
            "Denmark", "Finland", "Poland", "Portugal",
            "Belgium", "Switzerland", "Austria", "Greece",
            "Czech Republic", "Hungary", "Romania", "Ukraine"
        };
        var asia = new HashSet<string> {
            "Japan", "India", "Bangladesh", "China", "Thailand",
            "Vietnam", "Indonesia", "Malaysia", "Philippines",
            "South Korea", "Pakistan", "Sri Lanka", "Nepal",
            "Myanmar", "Cambodia", "Singapore", "Turkey",
            "Iran", "Iraq", "Saudi Arabia", "UAE", "Israel"
        };
        var americas = new HashSet<string> {
            "Canada", "United States", "Brazil", "Mexico",
            "Argentina", "Colombia", "Chile", "Peru",
            "Venezuela", "Ecuador", "Bolivia", "Uruguay",
            "Paraguay", "Cuba", "Jamaica", "Haiti"
        };
        var oceania = new HashSet<string> {
            "Australia", "New Zealand", "Papua New Guinea",
            "Fiji", "Solomon Islands"
        };

        if (africa.Contains(country))   return "Africa";
        if (europe.Contains(country))   return "Europe";
        if (asia.Contains(country))     return "Asia";
        if (americas.Contains(country)) return "Americas";
        if (oceania.Contains(country))  return "Oceania";
        return "Other";
    }

    static string GetCountryCode(string country)
    {
        var codes = new Dictionary<string, string>
        {
            {"Kenya", "KE"},
            {"Uganda", "UG"},
            {"Tanzania", "TZ"},
            {"Rwanda", "RW"},
            {"Egypt", "EG"},
            {"South Africa", "ZA"},
            {"Ethiopia", "ET"},
            {"Ghana", "GH"},
            {"Nigeria", "NG"},
            {"Senegal", "SN"},
            {"Cameroon", "CM"},
            {"Mozambique", "MZ"},
            {"Zambia", "ZM"},
            {"Zimbabwe", "ZW"},
            {"Angola", "AO"},
            {"Mali", "ML"},
            {"Burkina Faso", "BF"},
            {"Niger", "NE"},
            {"Chad", "TD"},
            {"Sudan", "SD"},
            {"Somalia", "SO"},
            {"DRC", "CD"},
            {"Ivory Coast", "CI"},
            {"Guinea", "GN"},
            {"Benin", "BJ"},
            {"Togo", "TG"},
            {"Morocco", "MA"},
            {"Algeria", "DZ"},
            {"Tunisia", "TN"},
            {"United Kingdom", "GB"},
            {"France", "FR"},
            {"Germany", "DE"},
            {"Italy", "IT"},
            {"Spain", "ES"},
            {"Netherlands", "NL"},
            {"Sweden", "SE"},
            {"Norway", "NO"},
            {"Denmark", "DK"},
            {"Finland", "FI"},
            {"Poland", "PL"},
            {"Portugal", "PT"},
            {"Belgium", "BE"},
            {"Switzerland", "CH"},
            {"Austria", "AT"},
            {"Greece", "GR"},
            {"Czech Republic", "CZ"},
            {"Hungary", "HU"},
            {"Romania", "RO"},
            {"Ukraine", "UA"},
            {"Russia", "RU"},
            {"Ireland", "IE"},
            {"Bulgaria", "BG"},
            {"Belarus", "BY"},
            {"Serbia", "RS"},
            {"Croatia", "HR"},
            {"Bosnia and Herzegovina", "BA"},
            {"Japan", "JP"},
            {"India", "IN"},
            {"Bangladesh", "BD"},
            {"China", "CN"},
            {"Thailand", "TH"},
            {"Vietnam", "VN"},
            {"Indonesia", "ID"},
            {"Malaysia", "MY"},
            {"Philippines", "PH"},
            {"South Korea", "KR"},
            {"Pakistan", "PK"},
            {"Sri Lanka", "LK"},
            {"Nepal", "NP"},
            {"Myanmar", "MM"},
            {"Cambodia", "KH"},
            {"Singapore", "SG"},
            {"Turkey", "TR"},
            {"Iran", "IR"},
            {"Iraq", "IQ"},
            {"Saudi Arabia", "SA"},
            {"UAE", "AE"},
            {"Israel", "IL"},
            {"Canada", "CA"},
            {"United States", "US"},
            {"Brazil", "BR"},
            {"Mexico", "MX"},
            {"Argentina", "AR"},
            {"Colombia", "CO"},
            {"Chile", "CL"},
            {"Peru", "PE"},
            {"Venezuela", "VE"},
            {"Ecuador", "EC"},
            {"Bolivia", "BO"},
            {"Uruguay", "UY"},
            {"Paraguay", "PY"},
            {"Cuba", "CU"},
            {"Jamaica", "JM"},
            {"Haiti", "HT"},
            {"Guatemala", "GT"},
            {"El Salvador", "SV"},
            {"Honduras", "HN"},
            {"Nicaragua", "NI"},
            {"Panama", "PA"},
            {"Costa Rica", "CR"},
            {"Australia", "AU"},
            {"New Zealand", "NZ"},
            {"Papua New Guinea", "PG"},
            {"Fiji", "FJ"},
            {"Solomon Islands", "SB"}
        };

        return codes.TryGetValue(country, out string code) ? code : "XX";
    }
}
#endif
