using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public enum CityContentDownloadKind
{
    World,
    Routes,
    CompleteCity,
    SingleRoute
}

/// <summary>Downloads optional OSM city data into player-managed storage.</summary>
public class RuntimeCityContentDownloader : MonoBehaviour
{
    public static RuntimeCityContentDownloader Instance { get; private set; }
    public bool IsDownloading { get; private set; }
    public string Status { get; private set; } = "Ready";
    public float Progress { get; private set; }

    /// <summary>True when the last run completed from the city pack instead of the network.</summary>
    public bool LastRunUsedFallback { get; private set; }
    public int LastRunFallbackCount { get; private set; }

    public event Action<string, float> Changed;
    public event Action<CityDefinition, bool> Completed;

    readonly string[] endpoints =
    {
        "https://overpass-api.de/api/interpreter",
        "https://overpass.kumi.systems/api/interpreter",
        "https://lz4.overpass-api.de/api/interpreter",
        // Regional Overpass instance. It is used last because it only covers
        // its own extract; an empty successful response is rejected below so
        // it can never overwrite a real city pack with zero elements.
        "https://overpass.osm.ch/api/interpreter"
    };
    bool cancelRequested;
    UnityWebRequest activeRequest;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap() => EnsureExists();

    public static RuntimeCityContentDownloader EnsureExists()
    {
        if (Instance != null) return Instance;
        RuntimeCityContentDownloader existing = FindAnyObjectByType<RuntimeCityContentDownloader>();
        return existing != null ? existing : new GameObject("RuntimeCityContentDownloader").AddComponent<RuntimeCityContentDownloader>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Download(CityDefinition city, CityContentDownloadKind kind, long relationId = 0)
    {
        if (city == null)
        {
            SetStatus("Choose a city first.", 0f);
            return;
        }
        if (IsDownloading)
        {
            SetStatus("A city download is already running. Wait for it to finish or cancel it.", Progress);
            return;
        }
        if (kind == CityContentDownloadKind.SingleRoute && relationId <= 0)
        {
            SetStatus("Enter a valid OpenStreetMap route relation ID.", 0f);
            return;
        }
        StartCoroutine(DownloadRoutine(city, kind, relationId));
    }

    public void Cancel()
    {
        cancelRequested = true;
        activeRequest?.Abort();
    }

    public void Delete(CityDefinition city)
    {
        if (IsDownloading || city == null) return;
        RuntimeCityContentStore.DeleteCity(city);
        SetStatus($"Removed downloaded data for {city.cityName}.", 0f);
    }

    IEnumerator DownloadRoutine(CityDefinition city, CityContentDownloadKind kind, long relationId)
    {
        IsDownloading = true;
        cancelRequested = false;
        LastRunUsedFallback = false;
        LastRunFallbackCount = 0;
        bool succeeded = false;
        var jobs = BuildJobs(city, kind, relationId);
        try
        {
            for (int i = 0; i < jobs.Count; i++)
            {
                if (cancelRequested) break;
                DownloadJob job = jobs[i];
                SetStatus($"{city.cityName}: downloading {job.label} ({i + 1}/{jobs.Count})", i / (float)jobs.Count);
                string response = null;
                yield return PostWithFallback(job.query, json => response = json);
                if (cancelRequested) break;
                if (!IsValidOverpassJson(response))
                {
                    // The shipped city pack is a real fallback, not a hidden
                    // assumption: roads, stops and buildings are read from
                    // StreamingAssets whenever a downloaded copy is absent.
                    if (TryUseBuiltInFallback(city, job, kind, relationId, out string fallbackStatus))
                    {
                        LastRunUsedFallback = true;
                        LastRunFallbackCount++;
                        SetStatus(fallbackStatus, (i + 1f) / jobs.Count);
                        yield return null;
                        continue;
                    }

                    SetStatus($"Download failed while fetching {job.label}.", i / (float)jobs.Count);
                    yield break;
                }
                RuntimeCityContentStore.Write(city, job.fileName, response);
                if (job.fileName == "routes.json")
                    RuntimeCityContentStore.Write(city, "routes.mode", kind == CityContentDownloadKind.SingleRoute ? "single" : "all");
                SetStatus($"Saved {job.label}", (i + 1f) / jobs.Count);
                yield return null;
            }

            succeeded = !cancelRequested;
            if (succeeded)
            {
                string source = LastRunFallbackCount > 0
                    ? "using the built-in city pack where the network was unavailable"
                    : FormatBytes(RuntimeCityContentStore.GetDownloadedBytes(city));
                SetStatus($"{city.cityName} content ready • {source}", 1f);
            }
            else
            {
                SetStatus("Download cancelled. Completed files were kept for resume.", Progress);
            }
        }
        finally
        {
            activeRequest?.Dispose();
            activeRequest = null;
            IsDownloading = false;
            Completed?.Invoke(city, succeeded);
        }
    }

    List<DownloadJob> BuildJobs(CityDefinition city, CityContentDownloadKind kind, long relationId)
    {
        string bbox = FormattableString.Invariant($"{city.minLat:F7},{city.minLon:F7},{city.maxLat:F7},{city.maxLon:F7}");
        var jobs = new List<DownloadJob>();
        bool world = kind == CityContentDownloadKind.World || kind == CityContentDownloadKind.CompleteCity;
        bool routes = kind == CityContentDownloadKind.Routes || kind == CityContentDownloadKind.CompleteCity || kind == CityContentDownloadKind.SingleRoute;

        if (routes)
        {
            jobs.Add(new DownloadJob("roads.json", "road network", OverpassQueryBuilder.RoadsQuery(city.minLat, city.minLon, city.maxLat, city.maxLon, 120)));
            string routeQuery = relationId > 0
                ? $"[out:json][timeout:120];relation({relationId.ToString(CultureInfo.InvariantCulture)});out body;>;out body qt;"
                : OverpassQueryBuilder.BusRoutesQuery(city.minLat, city.minLon, city.maxLat, city.maxLon, 120);
            jobs.Add(new DownloadJob("routes.json", relationId > 0 ? $"route {relationId}" : "all bus routes", routeQuery));
            jobs.Add(new DownloadJob("stops.json", "bus stops", OverpassQueryBuilder.BusStopsQuery(city.minLat, city.minLon, city.maxLat, city.maxLon, 90)));
        }

        if (world)
        {
            jobs.Add(new DownloadJob("buildings.json", "buildings", $"[out:json][timeout:120];way[\"building\"]({bbox});out geom;"));
            jobs.Add(new DownloadJob("poi.json", "places of interest",
                $"[out:json][timeout:120];(nwr[\"amenity\"]({bbox});nwr[\"shop\"]({bbox});nwr[\"tourism\"]({bbox});nwr[\"historic\"]({bbox}););out geom;"));
            jobs.Add(new DownloadJob("fuel.json", "fuel and charging stations", OverpassQueryBuilder.FuelAmenitiesQuery(city.minLat, city.minLon, city.maxLat, city.maxLon, 90)));
        }
        return jobs;
    }

    bool TryUseBuiltInFallback(CityDefinition city, DownloadJob job,
        CityContentDownloadKind kind, long relationId, out string status)
    {
        status = null;
        switch (job.fileName)
        {
            case "routes.json":
                if (kind == CityContentDownloadKind.SingleRoute)
                {
                    BusRoute match = FindBuiltInRoute(city, relationId);
                    if (match == null)
                        return false;
                    status = $"{city.cityName}: route {match.DisplayCode} is already in the built-in pack.";
                    return true;
                }
                if (!RuntimeCityContentStore.HasRouteData(city))
                    return false;
                status = $"{city.cityName}: using the built-in route network.";
                return true;

            case "roads.json":
            case "stops.json":
                if (!RuntimeCityContentStore.HasBundledFile(city, job.fileName))
                    return false;
                status = $"{city.cityName}: using the built-in {job.label}.";
                return true;

            case "buildings.json":
                if (!RuntimeCityContentStore.HasBundledFile(city, "buildings.json") &&
                    !RuntimeCityContentStore.HasBundledFile(city, "buildings.xml"))
                    return false;
                status = $"{city.cityName}: using the built-in buildings.";
                return true;

            case "poi.json":
            case "fuel.json":
                // Places and fuel stations are extra detail. Failing the whole
                // city download because one optional Overpass query timed out
                // made the menu look broken while the playable world was fine.
                status = $"{city.cityName}: skipped optional {job.label} (network unavailable).";
                return true;

            default:
                return false;
        }
    }

    static BusRoute FindBuiltInRoute(CityDefinition city, long relationId)
    {
        if (city == null || city.availableRoutes == null || relationId <= 0)
            return null;
        foreach (BusRoute route in city.availableRoutes)
            if (route != null && route.osmRelationId == relationId)
                return route;
        return null;
    }

    IEnumerator PostWithFallback(string query, Action<string> result)
    {
        result?.Invoke(null);
        for (int i = 0; i < endpoints.Length && !cancelRequested; i++)
        {
            byte[] body = Encoding.UTF8.GetBytes("data=" + UnityWebRequest.EscapeURL(query));
            activeRequest = new UnityWebRequest(endpoints[i], "POST")
            {
                uploadHandler = new UploadHandlerRaw(body),
                downloadHandler = new DownloadHandlerBuffer(),
                timeout = 135
            };
            activeRequest.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            activeRequest.SetRequestHeader("Accept", "application/json");
            activeRequest.SetRequestHeader("User-Agent", "RealBusSimulator/1.0");
            yield return activeRequest.SendWebRequest();
            if (activeRequest.result == UnityWebRequest.Result.Success)
            {
                string json = activeRequest.downloadHandler.text;
                activeRequest.Dispose(); activeRequest = null;
                result?.Invoke(json);
                yield break;
            }
            Debug.LogWarning($"City content download failed via {endpoints[i]}: {activeRequest.error}");
            activeRequest.Dispose(); activeRequest = null;
            if (i + 1 < endpoints.Length) yield return new WaitForSecondsRealtime(1f + i);
        }
    }

    void SetStatus(string value, float progress)
    {
        Status = value;
        Progress = Mathf.Clamp01(progress);
        Changed?.Invoke(Status, Progress);
        Debug.Log("[CityContent] " + value);
    }

    static bool IsValidOverpassJson(string json)
    {
        if (string.IsNullOrWhiteSpace(json) || json.Length < 20) return false;
        try
        {
            var response = OverpassResponse.Deserialize(json);
            // An empty elements array is a successful HTTP response, not a
            // successful download. Regional mirrors return it for cities they
            // do not cover; accepting it would replace a full pack with no data.
            return response?.elements != null && response.elements.Count > 0;
        }
        catch (Exception) { return false; }
    }

    public static string FormatBytes(long bytes)
    {
        if (bytes < 1024) return bytes + " B";
        if (bytes < 1024 * 1024) return (bytes / 1024f).ToString("0.0") + " KB";
        return (bytes / (1024f * 1024f)).ToString("0.0") + " MB";
    }

    readonly struct DownloadJob
    {
        public readonly string fileName, label, query;
        public DownloadJob(string fileName, string label, string query) { this.fileName = fileName; this.label = label; this.query = query; }
    }
}
