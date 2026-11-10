#if UNITY_EDITOR
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// Imports Overture Maps building footprints as the alternate building layer.
///
/// One city or all of them: raw GeoJSON downloads are kept in
/// <c>MapData/&lt;CODE&gt;/overture-buildings.geojson</c> (outside Assets, so the huge
/// exports are not imported by Unity), and the converted pack is written to
/// <c>Assets/StreamingAssets/Cities/&lt;CODE&gt;/overture-buildings.json</c>.
/// </summary>
public class OvertureBuildingImporter : EditorWindow
{
    const string CityAssetFolder = "Assets/_Game/ScriptableObjects/Cities";
    const string RawFolderName = "MapData";
    const string RawFileName = "overture-buildings.geojson";
    const string PackFileName = "overture-buildings.json";
    const long MaxGeoJsonBytes = 128L * 1024 * 1024;
    const int ReportLineLimit = 400;

    [MenuItem("Tools/RealBus/OSM/Import Overture Buildings")]
    static void Open() => GetWindow<OvertureBuildingImporter>("Overture Buildings");

    CityDefinition city;
    string cliPath = "overturemaps";
    bool redownloadExisting;
    string status = "Select a city, download its GeoJSON, then import it here.";
    Vector2 reportScroll;
    readonly List<string> report = new List<string>();
    BatchState batch;

    void OnEnable() { city = Selection.activeObject as CityDefinition; }

    void OnDisable()
    {
        EditorApplication.update -= PollBatch;
        BatchState state = batch;
        if (state != null) state.CancelRequested = true;
    }

    void OnGUI()
    {
        bool busy = batch != null;
        city = (CityDefinition)EditorGUILayout.ObjectField("City", city, typeof(CityDefinition), false);
        EditorGUILayout.HelpBox("Optional alternate building source. OSM files are retained. Import a complete city-area export; Overture replaces the OSM building layer for this city, not roads or POIs.", MessageType.Info);

        using (new EditorGUI.DisabledScope(busy || EditorApplication.isPlaying))
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Copy city download command"))
                {
                    GUIUtility.systemCopyBuffer = string.Format(CultureInfo.InvariantCulture,
                        "overturemaps download --bbox={0},{1},{2},{3} -f geojson --type=building -o {4}",
                        city != null ? city.minLon : 0d, city != null ? city.minLat : 0d,
                        city != null ? city.maxLon : 0d, city != null ? city.maxLat : 0d,
                        RawFileFor(city != null ? city.cityCode : "CITY"));
                    status = $"Command copied. Saves to {RawFolderName}/<CODE>/{RawFileName}. Requires the official overturemaps CLI. No download has been started.";
                }
                if (GUILayout.Button("Import downloaded GeoJSON…")) ImportSingle();
            }

            EditorGUILayout.Space(6f);
            EditorGUILayout.LabelField("All cities", EditorStyles.boldLabel);
            cliPath = EditorGUILayout.TextField("Overture CLI", cliPath);
            redownloadExisting = EditorGUILayout.Toggle("Redownload existing files", redownloadExisting);
            EditorGUILayout.HelpBox(
                $"Downloads write to {RawFolderName}/<CODE>/{RawFileName}; converted packs go to " +
                $"StreamingAssets/Cities/<CODE>/{PackFileName}. Every CityDefinition in {CityAssetFolder} is included.",
                MessageType.None);

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Download + import ALL cities", GUILayout.Height(32f)))
                    StartBatch(download: true);
                if (GUILayout.Button($"Import all files in {RawFolderName}", GUILayout.Height(32f)))
                    StartBatch(download: false);
            }
        }

        if (busy)
        {
            EditorGUILayout.Space(6f);
            float progress = batch.Total > 0 ? batch.Done / (float)batch.Total : 0f;
            Rect rect = GUILayoutUtility.GetRect(18f, 18f, "TextField");
            EditorGUI.ProgressBar(rect, progress, $"{batch.Done}/{batch.Total}  {batch.Step}");
            if (GUILayout.Button("Cancel after current city"))
                CancelBatch();
        }

        EditorGUILayout.Space(6f);
        EditorGUILayout.HelpBox(status, MessageType.None);
        if (report.Count > 0)
        {
            reportScroll = EditorGUILayout.BeginScrollView(reportScroll, GUILayout.Height(170f));
            for (int i = report.Count - 1; i >= 0; i--)
                EditorGUILayout.LabelField(report[i], EditorStyles.miniLabel);
            EditorGUILayout.EndScrollView();
        }
        if (GUILayout.Button("Download instructions / attribution"))
            Application.OpenURL("https://docs.overturemaps.org/getting-data/overturemaps-py/");
    }

    // ── Single city ────────────────────────────────────────────────────

    void ImportSingle()
    {
        string startFolder = Path.Combine(ProjectRoot, RawFolderName, city != null ? city.cityCode ?? "" : "");
        if (!Directory.Exists(startFolder)) startFolder = Path.Combine(ProjectRoot, RawFolderName);
        string source = EditorUtility.OpenFilePanel("Overture building GeoJSON", startFolder, "geojson,json");
        if (string.IsNullOrEmpty(source)) return;
        try
        {
            if (city == null) throw new InvalidOperationException("Assign the city this export belongs to.");
            if (!IsValidCityCode(city.cityCode))
                throw new InvalidOperationException("City code must contain only letters, numbers, _ or -.");
            long length = new FileInfo(source).Length;
            if (length > MaxGeoJsonBytes)
                throw new InvalidOperationException($"Export exceeds {MaxGeoJsonBytes / (1024 * 1024)} MB. Download a smaller city area.");
            string target = Path.Combine(Application.streamingAssetsPath, "Cities", city.cityCode, PackFileName);
            if (File.Exists(target) && !EditorUtility.DisplayDialog("Replace Overture pack?",
                    "The previous Overture pack will be kept as a .bak file. OSM files are unchanged.", "Replace", "Cancel"))
                return;
            string converted = OvertureBuildingConverter.ConvertGeoJson(File.ReadAllText(source), city, out int imported, out int skipped);
            WritePack(target, converted);
            AssetDatabase.Refresh();
            status = $"Imported {imported} polygons; skipped {skipped} invalid, holed, duplicate or out-of-bounds polygons. Restart Play Mode to load.";
            if (RuntimeCityContentStore.IsDownloaded(city, PackFileName))
                status += " A player-downloaded Overture pack overrides this bundled pack; remove that city's downloaded copy to use this import.";
            Debug.Log(status);
        }
        catch (Exception ex) { status = "Import failed: " + ex.Message; Debug.LogWarning(status); }
    }

    // ── Batch ──────────────────────────────────────────────────────────

    void StartBatch(bool download)
    {
        if (batch != null) return;
        List<CityTarget> targets = LoadCities();
        if (targets.Count == 0)
        {
            status = $"No usable CityDefinition assets found in {CityAssetFolder}.";
            return;
        }

        string cli = string.IsNullOrWhiteSpace(cliPath) ? "overturemaps" : cliPath.Trim();
        if (download && !TryProbeCli(cli, out string probe))
        {
            status = $"'{cli}' could not run ({probe}). Install it in a virtual environment " +
                     $"(python3 -m venv .venv && .venv/bin/pip install overturemaps) and set the CLI path to the venv binary.";
            return;
        }

        report.Clear();
        var state = new BatchState { Total = targets.Count };
        state.Report.Enqueue(download
            ? $"Downloading Overture buildings for {targets.Count} cities with '{cli}'."
            : $"Importing {targets.Count} city folders from {RawFolderName}.");
        batch = state;

        var thread = new Thread(() => RunBatch(state, targets, cli, download, redownloadExisting))
        {
            IsBackground = true,
            Name = "Overture city batch"
        };
        thread.Start();
        EditorApplication.update -= PollBatch;
        EditorApplication.update += PollBatch;
        Repaint();
    }

    void CancelBatch()
    {
        BatchState state = batch;
        if (state == null) return;
        state.CancelRequested = true;
        try { state.CurrentProcess?.Kill(); }
        catch (Exception) { /* It may have exited between the check and the kill. */ }
        status = "Cancelling after the current city…";
    }

    void PollBatch()
    {
        BatchState state = batch;
        if (state == null) return;
        DrainReport(state);
        Repaint();
        if (state.Running) return;

        EditorApplication.update -= PollBatch;
        DrainReport(state);
        batch = null;
        status = state.CancelRequested
            ? $"Cancelled at {state.Done}/{state.Total} cities. {state.Imported} pack(s) written."
            : $"{state.Imported} pack(s) written, {state.Failed} failed, {state.Skipped} skipped ({state.Done}/{state.Total} cities).";
        if (state.Imported > 0) AssetDatabase.Refresh();
        Debug.Log("[Overture] " + status);
        Repaint();
    }

    void DrainReport(BatchState state)
    {
        while (state.Report.TryDequeue(out string line))
        {
            report.Add(line);
            if (report.Count > ReportLineLimit) report.RemoveAt(0);
        }
    }

    static void RunBatch(BatchState state, List<CityTarget> targets, string cli, bool download, bool redownload)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            foreach (CityTarget target in targets)
            {
                if (state.CancelRequested) break;
                state.Step = target.Code;
                try
                {
                    Directory.CreateDirectory(target.RawDirectory);
                    if (download && (redownload || !File.Exists(target.RawPath)))
                    {
                        if (File.Exists(target.RawPath)) File.Delete(target.RawPath);
                        state.Step = $"{target.Code} downloading";
                        RunCli(state, target, cli);
                    }

                    if (!File.Exists(target.RawPath))
                    {
                        state.Skipped++;
                        state.Report.Enqueue($"{target.Code}: no {RawFileName} in {RawFolderName}/{target.Code}; skipped.");
                        continue;
                    }

                    long length = new FileInfo(target.RawPath).Length;
                    if (length == 0) throw new InvalidOperationException("the downloaded file is empty");
                    if (length > MaxGeoJsonBytes)
                        throw new InvalidOperationException($"{length / (1024 * 1024)} MB exceeds the {MaxGeoJsonBytes / (1024 * 1024)} MB cap; download a smaller city area");

                    state.Step = $"{target.Code} converting";
                    string converted = OvertureBuildingConverter.ConvertGeoJson(
                        File.ReadAllText(target.RawPath), target.Code,
                        target.MinLat, target.MinLon, target.MaxLat, target.MaxLon,
                        out int polygons, out int rejected);
                    WritePack(target.PackPath, converted);
                    state.Imported++;
                    state.Report.Enqueue($"{target.Code}: {polygons} footprints ({rejected} skipped) → overture-buildings.json ({converted.Length / 1024} KB).");
                }
                catch (Exception ex)
                {
                    state.Failed++;
                    state.Report.Enqueue($"{target.Code}: FAILED — {ex.Message}");
                }
                finally { state.Done++; }
            }
        }
        finally
        {
            stopwatch.Stop();
            state.Running = false;
            state.Report.Enqueue($"Batch finished in {stopwatch.Elapsed.TotalMinutes:0.0} min.");
        }
    }

    static void RunCli(BatchState state, CityTarget target, string cli)
    {
        var start = new ProcessStartInfo
        {
            FileName = cli,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };
        // Overture's bbox is west,south,east,north — lon/lat first, unlike Overpass.
        string bbox = string.Format(CultureInfo.InvariantCulture, "{0:F6},{1:F6},{2:F6},{3:F6}",
            target.MinLon, target.MinLat, target.MaxLon, target.MaxLat);
        start.ArgumentList.Add("download");
        start.ArgumentList.Add("--bbox=" + bbox);
        start.ArgumentList.Add("-f");
        start.ArgumentList.Add("geojson");
        start.ArgumentList.Add("--type=building");
        start.ArgumentList.Add("-o");
        start.ArgumentList.Add(target.RawPath);

        Process process;
        try { process = Process.Start(start); }
        catch (Exception ex) { throw new InvalidOperationException($"could not start '{cli}': {ex.Message}"); }
        if (process == null) throw new InvalidOperationException($"could not start '{cli}'");

        using (process)
        {
            state.CurrentProcess = process;
            var stdout = process.StandardOutput.ReadToEndAsync();
            var stderr = process.StandardError.ReadToEndAsync();
            process.WaitForExit();
            state.CurrentProcess = null;
            if (state.CancelRequested) throw new OperationCanceledException();
            if (process.ExitCode != 0)
                throw new InvalidOperationException($"overturemaps exited with {process.ExitCode}: {Trim(stderr.Result, 300)}");
        }
    }

    static bool TryProbeCli(string cli, out string message)
    {
        try
        {
            var start = new ProcessStartInfo
            {
                FileName = cli,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            start.ArgumentList.Add("--help");
            using (Process process = Process.Start(start))
            {
                if (process == null) { message = "no process"; return false; }
                var stdout = process.StandardOutput.ReadToEndAsync();
                var stderr = process.StandardError.ReadToEndAsync();
                process.WaitForExit();
                message = Trim(stdout.Result + " " + stderr.Result, 160);
                return process.ExitCode == 0;
            }
        }
        catch (Exception ex) { message = ex.Message; return false; }
    }

    // ── Shared helpers ─────────────────────────────────────────────────

    static List<CityTarget> LoadCities()
    {
        string projectRoot = ProjectRoot;
        string streamingRoot = Application.streamingAssetsPath;
        var targets = new List<CityTarget>();
        var seen = new HashSet<string>();
        foreach (string guid in AssetDatabase.FindAssets("t:CityDefinition", new[] { CityAssetFolder }))
        {
            var definition = AssetDatabase.LoadAssetAtPath<CityDefinition>(AssetDatabase.GUIDToAssetPath(guid));
            if (definition == null || !IsValidCityCode(definition.cityCode)) continue;
            if (definition.minLat >= definition.maxLat || definition.minLon >= definition.maxLon) continue;
            if (!seen.Add(definition.cityCode)) continue;
            targets.Add(new CityTarget(definition, projectRoot, streamingRoot));
        }
        targets.Sort((a, b) => string.CompareOrdinal(a.Code, b.Code));
        return targets;
    }

    static bool IsValidCityCode(string code) =>
        !string.IsNullOrWhiteSpace(code) && System.Text.RegularExpressions.Regex.IsMatch(code, @"^[A-Za-z0-9_-]+$");

    static void WritePack(string target, string converted)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(target));
        string temporary = target + ".import";
        File.WriteAllText(temporary, converted);
        if (File.Exists(target)) File.Replace(temporary, target, target + ".bak");
        else File.Move(temporary, target);
    }

    static string RawFileFor(string cityCode) =>
        Path.Combine(RawFolderName, string.IsNullOrWhiteSpace(cityCode) ? "CITY" : cityCode, RawFileName);

    static string ProjectRoot => Path.GetDirectoryName(Application.dataPath);

    static string Trim(string value, int limit)
    {
        if (string.IsNullOrWhiteSpace(value)) return "";
        value = value.Replace('\n', ' ').Replace('\r', ' ').Trim();
        while (value.Contains("  ")) value = value.Replace("  ", " ");
        return value.Length <= limit ? value : value.Substring(0, limit) + "…";
    }

    sealed class CityTarget
    {
        public CityTarget(CityDefinition definition, string projectRoot, string streamingRoot)
        {
            Code = definition.cityCode;
            MinLat = definition.minLat;
            MinLon = definition.minLon;
            MaxLat = definition.maxLat;
            MaxLon = definition.maxLon;
            RawDirectory = Path.Combine(projectRoot, RawFolderName, Code);
            PackPath = Path.Combine(streamingRoot, "Cities", Code, PackFileName);
        }

        public readonly string Code, RawDirectory, PackPath;
        public readonly double MinLat, MinLon, MaxLat, MaxLon;
        public string RawPath => Path.Combine(RawDirectory, RawFileName);
    }

    sealed class BatchState
    {
        public readonly ConcurrentQueue<string> Report = new ConcurrentQueue<string>();
        public volatile bool Running = true;
        public volatile bool CancelRequested;
        public volatile Process CurrentProcess;
        public volatile string Step = "";
        public int Done;
        public int Total;
        public int Imported;
        public int Failed;
        public int Skipped;
    }
}
#endif
