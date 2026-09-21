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

    public static string ResolveReadPath(CityDefinition city, string fileName)
    {
        string downloaded = WritablePath(city, fileName);
        if (IsUsefulFile(downloaded)) return downloaded;
        return Path.Combine(Application.streamingAssetsPath, "Cities", city != null ? city.cityCode : "NBO", Path.GetFileName(fileName));
    }

    public static bool IsDownloaded(CityDefinition city, string fileName) => IsUsefulFile(WritablePath(city, fileName));

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
        try { return File.Exists(path) && new FileInfo(path).Length > 24; }
        catch (IOException) { return false; }
    }
}
