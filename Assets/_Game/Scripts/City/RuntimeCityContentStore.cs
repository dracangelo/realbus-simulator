using System.IO;
using UnityEngine;

/// <summary>Read/write location for optional city content downloaded by players.</summary>
public static class RuntimeCityContentStore
{
    public static string Root => Path.Combine(Application.persistentDataPath, "DownloadedCities");

    public static string CityDirectory(string cityCode)
    {
        string safe = string.IsNullOrWhiteSpace(cityCode) ? "UNKNOWN" : cityCode.Trim().ToUpperInvariant();
        return Path.Combine(Root, safe);
    }

    public static string WritablePath(CityDefinition city, string fileName)
    {
        return Path.Combine(CityDirectory(city != null ? city.cityCode : null), Path.GetFileName(fileName));
    }

    public static string BundledPath(CityDefinition city, string fileName)
    {
        return Path.Combine(Application.streamingAssetsPath, "Cities",
            city != null && !string.IsNullOrWhiteSpace(city.cityCode) ? city.cityCode : "NBO",
            Path.GetFileName(fileName));
    }

    public static string ResolveReadPath(CityDefinition city, string fileName)
    {
        string downloaded = WritablePath(city, fileName);
        if (IsUsefulFile(downloaded)) return downloaded;
        return BundledPath(city, fileName);
    }

    public static bool IsDownloaded(CityDefinition city, string fileName) => IsUsefulFile(WritablePath(city, fileName));

    /// <summary>True when the shipped city pack already contains this file.</summary>
    public static bool HasBundledFile(CityDefinition city, string fileName) => IsUsefulFile(BundledPath(city, fileName));

    /// <summary>True when the game can read this file, downloaded or shipped.</summary>
    public static bool IsAvailable(CityDefinition city, string fileName)
        => IsDownloaded(city, fileName) || HasBundledFile(city, fileName);

    /// <summary>
    /// Routes are split across two storage models: an imported routes.json, or the
    /// route assets referenced by the city definition. Both are playable without a
    /// fresh download, so the menu should not imply the city has no routes.
    /// </summary>
    public static bool HasRouteData(CityDefinition city)
        => IsAvailable(city, "routes.json") || (city != null && city.availableRoutes != null && city.availableRoutes.Length > 0);

    public static string SourceLabel(CityDefinition city, string fileName)
    {
        if (IsDownloaded(city, fileName)) return "downloaded";
        if (HasBundledFile(city, fileName)) return "built-in";
        return "missing";
    }

    public static long GetDownloadedBytes(CityDefinition city)
    {
        string directory = CityDirectory(city != null ? city.cityCode : null);
        if (!Directory.Exists(directory)) return 0;
        long total = 0;
        foreach (string file in Directory.GetFiles(directory))
            try { total += new FileInfo(file).Length; } catch (IOException) { }
        return total;
    }

    public static void Write(CityDefinition city, string fileName, string contents)
    {
        string path = WritablePath(city, fileName);
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        string temporary = path + ".download";
        File.WriteAllText(temporary, contents ?? string.Empty);
        if (File.Exists(path)) File.Delete(path);
        File.Move(temporary, path);
    }

    public static void DeleteCity(CityDefinition city)
    {
        string directory = CityDirectory(city != null ? city.cityCode : null);
        if (Directory.Exists(directory)) Directory.Delete(directory, true);
    }

    static bool IsUsefulFile(string path)
    {
        // Placeholder files in older packs were 26 bytes (an empty <osm/> root).
        // Treating those as real content made the downloader say the built-in
        // pack was complete and then the building loader found no footprints.
        // Anything smaller than a practical minimal data file is not usable.
        try { return File.Exists(path) && new FileInfo(path).Length > 128; }
        catch (IOException) { return false; }
    }
}
