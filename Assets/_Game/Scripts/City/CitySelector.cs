using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Translates a <see cref="CityConfig"/> asset into a live game session
/// (section 3.8).
///
/// Load sequence:
///   1. Create / retrieve a transient <see cref="CityDefinition"/> from config.
///   2. Register with <see cref="CityManager"/> and set as active.
///   3. Update <see cref="CoordinateConverter"/> origin.
///   4. Tell <see cref="MapTileLoader"/> to centre on the new city.
///   5. Trigger <see cref="OSMRouteImporter"/> if routes are needed (demand load).
///   6. Update <see cref="GameState"/> if the city is unlocked.
///
/// Improvements vs original:
///   • Load sequence runs as a coroutine — map and routes don't block each other.
///   • <see cref="IsCityUnlocked"/> check prevents loading locked cities silently.
///   • References resolved once in <see cref="Start"/>; never called per-frame.
/// </summary>
public class CitySelector : MonoBehaviour
{
    // ── Inspector ──────────────────────────────────────────────────────

    [Header("City Packs")]
    public CityConfig[] cityConfigs = System.Array.Empty<CityConfig>();
    public bool autoLoadFirstCity   = false;
    public bool importRoutesOnLoad  = true;

    [Header("Runtime References (auto-resolved if blank)")]
    public CoordinateConverter coordinateConverter;
    public MapTileLoader       mapTileLoader;
    public OSMRouteImporter    routeImporter;

    // ── Events ─────────────────────────────────────────────────────────

    /// <summary>Fired when a city load sequence begins.</summary>
    public event System.Action<CityConfig>     OnCityLoadStarted;
    /// <summary>Fired when the load sequence is complete.</summary>
    public event System.Action<CityDefinition> OnCityLoadComplete;

    // ── Private ────────────────────────────────────────────────────────

    readonly Dictionary<CityConfig, CityDefinition> _runtimeCache = new();
    bool _loading;

    // ── Lifecycle ──────────────────────────────────────────────────────

    void Start()
    {
        ResolveReferences();

        if (autoLoadFirstCity && cityConfigs.Length > 0 && cityConfigs[0] != null)
            LoadCity(cityConfigs[0]);
    }

    // ── Public API ─────────────────────────────────────────────────────

    public void LoadCityByIndex(int index)
    {
        if (index < 0 || index >= cityConfigs.Length)
        {
            Debug.LogWarning($"[CitySelector] Invalid index {index}.");
            return;
        }
        LoadCity(cityConfigs[index]);
    }

    public void LoadCityByCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code)) return;
        foreach (var cfg in cityConfigs)
        {
            if (cfg != null && string.Equals(cfg.cityCode, code,
                    System.StringComparison.OrdinalIgnoreCase))
            {
                LoadCity(cfg);
                return;
            }
        }
        Debug.LogWarning($"[CitySelector] No CityConfig with code '{code}'.");
    }

    public void LoadCity(CityConfig config)
    {
        if (config == null)
        {
            Debug.LogWarning("[CitySelector] LoadCity called with null config.");
            return;
        }

        if (_loading)
        {
            Debug.LogWarning("[CitySelector] Already loading — request queued or dropped.");
            return;
        }

        // Validate config before starting.
        if (!config.IsValid(out string reason))
        {
            Debug.LogError($"[CitySelector] Invalid CityConfig '{config.name}': {reason}");
            return;
        }

        StartCoroutine(LoadCityRoutine(config));
    }

    // ── Load coroutine ─────────────────────────────────────────────────

    IEnumerator LoadCityRoutine(CityConfig config)
    {
        _loading = true;
        OnCityLoadStarted?.Invoke(config);

        ResolveReferences();

        // ── Step 1: City definition ────────────────────────────────────
        var runtimeCity = GetOrCreateRuntimeCity(config);

        var unlock = UnlockManager.Instance;
        if (unlock != null && !unlock.IsCityUnlocked(runtimeCity))
        {
            _loading = false;
            Debug.LogWarning("City is locked: " + runtimeCity.cityName);
            yield break;
        }
        var streamer = FindFirstObjectByType<TileStreamManager>();
        if (streamer != null) streamer.ResetStreaming();

        // ── Step 2: Register & activate ───────────────────────────────
        var cityMgr = CityManager.Instance;
        cityMgr?.RegisterCity(runtimeCity);
        cityMgr?.SetActiveCity(runtimeCity);

        // ── Step 3: Unlock check ───────────────────────────────────────
        var unlockMgr = UnlockManager.Instance;
        bool unlocked = unlockMgr == null || unlockMgr.IsCityUnlocked(runtimeCity);

        var gameState = GameState.Instance;
        if (gameState != null && unlocked)
            gameState.SelectCity(runtimeCity);

        // ── Step 4: Coordinate origin ──────────────────────────────────
        if (coordinateConverter != null)
            coordinateConverter.SetCityOrigin(runtimeCity);

        if (streamer != null) streamer.zoomLevel = config.defaultZoom;

        yield return null; // allow systems to react to origin change

        // ── Step 5: Map tile loader ────────────────────────────────────
        if (mapTileLoader != null)
            mapTileLoader.LoadMap(runtimeCity.centreLat, runtimeCity.centreLon,
                                  Mathf.Max(10, config.defaultZoom));

        yield return null;

        // ── Step 6: Route import (demand) ─────────────────────────────
        bool needsImport = config.loadRoutesOnDemand
            && importRoutesOnLoad
            && routeImporter != null
            && !routeImporter.importInProgress
            && (runtimeCity.availableRoutes == null || runtimeCity.availableRoutes.Length == 0);

        if (needsImport)
        {
            routeImporter.TriggerImport(runtimeCity);
            // Don't yield-wait here — import runs in the background so the player
            // can start driving while routes stream in.
        }

        _loading = false;
        OnCityLoadComplete?.Invoke(runtimeCity);
        Debug.Log($"[CitySelector] City loaded: {runtimeCity.cityName}");
    }

    // ── Helpers ────────────────────────────────────────────────────────

    CityDefinition GetOrCreateRuntimeCity(CityConfig config)
    {
        if (_runtimeCache.TryGetValue(config, out var city) && city != null)
        {
            // Re-apply in case the config was modified in the Editor.
            config.ApplyTo(city, preserveExistingRoutes: true);
            return city;
        }

        city = config.CreateRuntimeCityDefinition();
        _runtimeCache[config] = city;
        return city;
    }

    void ResolveReferences()
    {
        if (coordinateConverter == null)
            coordinateConverter = CoordinateConverter.Instance
                ?? FindFirstObjectByType<CoordinateConverter>();

        if (mapTileLoader == null)
            mapTileLoader = FindFirstObjectByType<MapTileLoader>();

        if (routeImporter == null)
            routeImporter = OSMRouteImporter.Instance
                ?? FindFirstObjectByType<OSMRouteImporter>();
    }
}
