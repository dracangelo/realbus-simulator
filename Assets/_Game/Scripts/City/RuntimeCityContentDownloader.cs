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
    public event Action<string, float> Changed;
    public event Action<CityDefinition, bool> Completed;

    readonly string[] endpoints =
    {
        "https://overpass-api.de/api/interpreter",
        "https://overpass.kumi.systems/api/interpreter",
        "https://lz4.overpass-api.de/api/interpreter"
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
        if (IsDownloading || city == null) return;
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
            SetStatus(succeeded
                ? $"{city.cityName} content ready • {FormatBytes(RuntimeCityContentStore.GetDownloadedBytes(city))}"
                : "Download cancelled. Completed files were kept for resume.", succeeded ? 1f : Progress);
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
        try { return OverpassResponse.Deserialize(json)?.elements != null; }
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
