using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Small local index of cities the player chose to keep available.</summary>
public static class PlayerCityLibrary
{
    const string Key = "RealBus.FavouriteCities.v1";
    [Serializable] class Data { public List<string> cityCodes = new List<string>(); }
    static Data cached;
    static Data State => cached ??= JsonUtility.FromJson<Data>(PlayerPrefs.GetString(Key, "{}")) ?? new Data();

    public static bool IsFavourite(CityDefinition city) => city != null && State.cityCodes.Contains(Normalize(city.cityCode));
    public static void SetFavourite(CityDefinition city, bool favourite)
    {
        if (city == null) return;
        string code = Normalize(city.cityCode);
        if (favourite) { if (!State.cityCodes.Contains(code)) State.cityCodes.Add(code); }
        else State.cityCodes.Remove(code);
        PlayerPrefs.SetString(Key, JsonUtility.ToJson(State)); PlayerPrefs.Save();
    }
    public static string NormalizedCode(CityDefinition city) => Normalize(city != null ? city.cityCode : null);
    static string Normalize(string value) => string.IsNullOrWhiteSpace(value) ? "UNKNOWN" : value.Trim().ToUpperInvariant();
}
