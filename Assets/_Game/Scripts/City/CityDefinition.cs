using UnityEngine;

public enum ClimateZone
{
    Tropical,
    Desert,
    Temperate,
    Continental,
    Mediterranean,
    Arid,
    Subtropical
}

[CreateAssetMenu(fileName = "CityDefinition", menuName = "RealBus/City Definition")]
public class CityDefinition : ScriptableObject
{
    [Header("City Info")]
    public string cityName = "Nairobi";
    public string country = "Kenya";
    public string cityCode = "NBO"; // 3-letter code like airport codes
    public ClimateZone climateZone = ClimateZone.Tropical;
    public float avgTemperatureCelsius = 22f;
    public bool hasMonsoon = false;

    [Header("Map Centre — GPS Origin")]
    public double centreLat = -1.2864;
    public double centreLon = 36.8172;

    [Header("OSM Bounding Box")]
    public double minLat = -1.2900;
    public double maxLat = -1.2630;
    public double minLon = 36.8000;
    public double maxLon = 36.8280;

    [Header("OSM Data Files — in StreamingAssets/Cities/CityCode/")]
    public string roadsFileName = "roads.xml";
    public string buildingsFileName = "buildings.xml";

    [Header("Routes")]
    public BusRoute[] availableRoutes;

    [Header("Spawn Point")]
    public double spawnLat = -1.2864;
    public double spawnLon = 36.8172;

    [Header("Settings")]
    public bool isDownloaded = true; // false = needs download
    public string previewImagePath = "";

    [Header("Seasonality")]
    public int[] rainySeasonMonths = { 3, 4, 5, 10, 11, 12 };
    public int[] snowSeasonMonths = { 12, 1, 2 };

    public string GetRoadsPath()
    {
        return System.IO.Path.Combine(
            Application.streamingAssetsPath,
            "Cities", cityCode, roadsFileName);
    }

    public string GetBuildingsPath()
    {
        return System.IO.Path.Combine(
            Application.streamingAssetsPath,
            "Cities", cityCode, buildingsFileName);
    }

    public bool IsRainySeason()
    {
        return IsMonthInSeason(System.DateTime.Now.Month, rainySeasonMonths);
    }

    public bool IsSnowSeason()
    {
        return IsMonthInSeason(System.DateTime.Now.Month, snowSeasonMonths);
    }

    private static bool IsMonthInSeason(int month, int[] seasonMonths)
    {
        if (seasonMonths == null || seasonMonths.Length == 0)
            return false;

        for (int i = 0; i < seasonMonths.Length; i++)
        {
            if (seasonMonths[i] == month)
                return true;
        }

        return false;
    }
}
