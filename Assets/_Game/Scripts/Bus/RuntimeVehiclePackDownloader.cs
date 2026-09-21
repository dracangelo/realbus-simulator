using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>Installs optional, platform-specific bus AssetBundles outside the base application.</summary>
public class RuntimeVehiclePackDownloader : MonoBehaviour
{
    public static RuntimeVehiclePackDownloader Instance { get; private set; }
    public bool IsDownloading { get; private set; }
    public string Status { get; private set; } = "Ready";
    public float Progress { get; private set; }
    public event Action<string, float> Changed;

    readonly List<AssetBundle> loadedBundles = new List<AssetBundle>();
    UnityWebRequest activeRequest;
    bool cancelRequested;

    public static string PackDirectory => Path.Combine(Application.persistentDataPath, "VehiclePacks");

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap() => EnsureExists();

    public static RuntimeVehiclePackDownloader EnsureExists()
    {
        if (Instance != null) return Instance;
        RuntimeVehiclePackDownloader existing = FindAnyObjectByType<RuntimeVehiclePackDownloader>();
        return existing != null ? existing : new GameObject("RuntimeVehiclePackDownloader").AddComponent<RuntimeVehiclePackDownloader>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Directory.CreateDirectory(PackDirectory);
        StartCoroutine(LoadInstalledPacks());
    }

    public void Download(string url)
    {
        if (IsDownloading) return;
        if (!Uri.TryCreate(url?.Trim(), UriKind.Absolute, out Uri uri) ||
            (!string.Equals(uri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase) &&
             !(Application.isEditor && string.Equals(uri.Scheme, Uri.UriSchemeFile, StringComparison.OrdinalIgnoreCase))))
        {
            SetStatus("Enter a valid HTTPS vehicle-pack URL.", 0f);
            return;
        }
        StartCoroutine(DownloadRoutine(uri));
    }

    public void Cancel()
    {
        cancelRequested = true;
        activeRequest?.Abort();
    }

    public void RemoveDownloadedPacks()
    {
        if (IsDownloading) return;
        foreach (AssetBundle bundle in loadedBundles)
            if (bundle != null) bundle.Unload(false);
        loadedBundles.Clear();

        if (Directory.Exists(PackDirectory))
            foreach (string file in Directory.GetFiles(PackDirectory, "*.bundle"))
                File.Delete(file);
        SetStatus("Vehicle packs removed. Restart to clear their buses from this session.", 0f);
    }

    IEnumerator DownloadRoutine(Uri uri)
    {
        IsDownloading = true;
        cancelRequested = false;
        Directory.CreateDirectory(PackDirectory);
        string fileName = BuildFileName(uri);
        string finalPath = Path.Combine(PackDirectory, fileName);
        string temporaryPath = finalPath + ".download";
        if (File.Exists(temporaryPath)) File.Delete(temporaryPath);

        SetStatus("Downloading vehicle pack…", 0f);
        activeRequest = UnityWebRequest.Get(uri.AbsoluteUri);
        activeRequest.downloadHandler = new DownloadHandlerFile(temporaryPath) { removeFileOnAbort = true };
        activeRequest.timeout = 300;
        UnityWebRequestAsyncOperation operation = activeRequest.SendWebRequest();
        while (!operation.isDone)
        {
            SetStatus("Downloading vehicle pack…", activeRequest.downloadProgress);
            yield return null;
        }

        bool downloaded = !cancelRequested && activeRequest.result == UnityWebRequest.Result.Success;
        string error = activeRequest.error;
        activeRequest.Dispose();
        activeRequest = null;

        if (!downloaded)
        {
            if (File.Exists(temporaryPath)) File.Delete(temporaryPath);
            SetStatus(cancelRequested ? "Vehicle-pack download cancelled." : "Vehicle-pack download failed: " + error, 0f);
            IsDownloading = false;
            yield break;
        }

        AssetBundle bundle = AssetBundle.LoadFromFile(temporaryPath);
        if (bundle == null)
        {
            File.Delete(temporaryPath);
            SetStatus("Pack is invalid or was built for a different platform/Unity version.", 0f);
            IsDownloading = false;
            yield break;
        }

        int imported = RegisterBundle(bundle);
        if (imported == 0)
        {
            bundle.Unload(true);
            File.Delete(temporaryPath);
            SetStatus("Pack contains no valid BusSpec with a drivable prefab.", 0f);
            IsDownloading = false;
            yield break;
        }

        if (File.Exists(finalPath)) File.Delete(finalPath);
        File.Move(temporaryPath, finalPath);
        loadedBundles.Add(bundle);
        IsDownloading = false;
        SetStatus($"Installed {imported} vehicle{(imported == 1 ? string.Empty : "s")} • {RuntimeCityContentDownloader.FormatBytes(GetDownloadedBytes())}", 1f);
    }

    IEnumerator LoadInstalledPacks()
    {
        yield return null;
        if (!Directory.Exists(PackDirectory)) yield break;
        int imported = 0;
        foreach (string path in Directory.GetFiles(PackDirectory, "*.bundle"))
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(path);
            if (bundle == null)
            {
                Debug.LogWarning("Vehicle pack could not be loaded: " + path);
                continue;
            }
            int count = RegisterBundle(bundle);
            if (count > 0) { imported += count; loadedBundles.Add(bundle); }
            else bundle.Unload(true);
            yield return null;
        }
        if (imported > 0) SetStatus($"Loaded {imported} downloaded vehicle{(imported == 1 ? string.Empty : "s")}.", 1f);
    }

    int RegisterBundle(AssetBundle bundle)
    {
        int imported = 0;
        BusSpec[] specs;
        try { specs = bundle.LoadAllAssets<BusSpec>(); }
        catch (Exception exception)
        {
            Debug.LogWarning("Vehicle pack assets could not be read: " + exception.Message);
            return 0;
        }
        BusFleetManager fleet = BusFleetManager.EnsureExists();
        foreach (BusSpec spec in specs)
            if (fleet.RegisterRuntimeBusSpec(spec, true)) imported++;
        return imported;
    }

    public long GetDownloadedBytes()
    {
        long total = 0;
        if (!Directory.Exists(PackDirectory)) return total;
        foreach (string path in Directory.GetFiles(PackDirectory, "*.bundle"))
            total += new FileInfo(path).Length;
        return total;
    }

    static string BuildFileName(Uri uri)
    {
        string stem = Path.GetFileNameWithoutExtension(uri.LocalPath);
        if (string.IsNullOrWhiteSpace(stem)) stem = "vehicle-pack";
        foreach (char invalid in Path.GetInvalidFileNameChars()) stem = stem.Replace(invalid, '_');
        string hash = Hash128.Compute(uri.AbsoluteUri).ToString().Substring(0, 12);
        return stem + "-" + hash + ".bundle";
    }

    void SetStatus(string status, float progress)
    {
        Status = status;
        Progress = Mathf.Clamp01(progress);
        Changed?.Invoke(Status, Progress);
        Debug.Log("[VehiclePacks] " + status);
    }
}
