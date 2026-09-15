using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

#if PLAYFAB_SDK
using PlayFab;
using PlayFab.ClientModels;
#endif

[Serializable]
public class SaveKeyValueEntry
{
    public string key;
    public string value;
}

[Serializable]
public class MissionHistoryEntry
{
    public string routeId;
    public string routeName;
    public string cityCode;
    public int xpEarned;
    public int stars;
    public int netEarningsKES;
    public float totalScore;
    public long completedAtUnixSeconds;
}

[Serializable]
public class SaveData
{
    public int saveVersion = 1;
    public long updatedAtUnixSeconds;
    public int playerXP;
    public int rank;
    public string[] unlockedCities = Array.Empty<string>();
    public string[] unlockedBuses = Array.Empty<string>();
    public SaveKeyValueEntry[] activeUpgrades = Array.Empty<SaveKeyValueEntry>();
    public string[] liveries = Array.Empty<string>();
    public MissionHistoryEntry[] missionHistory = Array.Empty<MissionHistoryEntry>();
    public string[] completedRouteKeys = Array.Empty<string>();
    public string selectedCountry = string.Empty;
    public string selectedCityCode = string.Empty;
    public string selectedRouteId = string.Empty;
    public string selectedBusId = string.Empty;
    public PlayerEconomyData economy = new PlayerEconomyData();
    public VehiclePersistentState vehicleState = new VehiclePersistentState();
    public MissionSettlementData lastMissionSettlement = new MissionSettlementData();
}

[Serializable]
public class SaveConflictInfo
{
    public SaveData localSave;
    public SaveData cloudSave;
    public string reason;
}

public interface ISaveCloudSync
{
    bool IsAvailable { get; }
    void Pull(string customId, Action<SaveData> onSuccess, Action<string> onError);
    void Push(string customId, SaveData saveData, Action onSuccess, Action<string> onError);
}

public class SaveManager : MonoBehaviour
{
    public const string LocalSavePrefsKey = "save.player_data.json";
    public const string PendingUpgradePrefsKey = "save.active_upgrades.json";
    public const string KnownLiveriesPrefsKey = "save.liveries";

    public static SaveManager Instance { get; private set; }

    public event Action<SaveConflictInfo> SaveConflictDetected;
    public event Action<SaveData> SaveApplied;

    [SerializeField] int missionHistoryLimit = 24;
    [SerializeField] int conflictAmbiguityWindowSeconds = 120;

    readonly Dictionary<string, string> activeUpgrades = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    readonly HashSet<string> liveries = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    readonly List<MissionHistoryEntry> missionHistory = new List<MissionHistoryEntry>();
    readonly ISaveCloudSync cloudSync = new PlayFabSaveCloudSync();
    static readonly Regex UpdatedAtJsonPattern = new Regex(@"""updatedAtUnixSeconds"":-?[0-9]+,?", RegexOptions.CultureInvariant);

    SaveConflictInfo pendingConflict;
    SaveData lastAppliedSave;
    bool initialized;
    bool cloudPullAttempted;

    public bool HasPendingConflict => pendingConflict != null;
    public SaveConflictInfo PendingConflict => pendingConflict;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap()
    {
        EnsureExists();
    }

    public static SaveManager EnsureExists()
    {
        if (Instance != null)
            return Instance;

        var existing = FindFirstObjectByType<SaveManager>();
        if (existing != null)
            return existing;

        var root = new GameObject("SaveManager");
        return root.AddComponent<SaveManager>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        XPSystem.EnsureExists();
        UnlockManager.EnsureExists();
        BusFleetManager.EnsureExists();
        EnsureGameStateExists();

        SceneManager.sceneLoaded += HandleSceneLoaded;
        SubscribeToRuntimeEvents();
        EnsureInitialized();
    }

    void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= HandleSceneLoaded;

        UnsubscribeFromRuntimeEvents();
    }

    void OnApplicationPause(bool paused)
    {
        if (!paused)
            return;

        SaveNow("app_background");
    }

    void OnApplicationQuit()
    {
        SaveNow("app_quit");
    }

    public void EnsureInitialized()
    {
        if (initialized)
            return;

        initialized = true;
        LoadUpgradeStateFromPrefs();
        LoadLiveryStateFromPrefs();

        SaveData localSave = LoadLocalBackup();
        if (localSave != null)
            ApplySaveData(localSave, saveLocalBackup: false, pushCloud: false);

        TryPullFromCloud();
    }

    public SaveData CaptureCurrentData()
    {
        var xpSystem = XPSystem.EnsureExists();
        var unlockManager = UnlockManager.EnsureExists();
        var fleetManager = BusFleetManager.EnsureExists();
        var gameState = EnsureGameStateExists();

        if (UpgradeManager.Instance != null)
        {
            string[] purchasedUpgradeKeys = UpgradeManager.Instance.GetPurchasedKeys();
            for (int i = 0; i < purchasedUpgradeKeys.Length; i++)
            {
                string key = purchasedUpgradeKeys[i];
                if (!activeUpgrades.ContainsKey(key))
                    activeUpgrades[key] = "owned";
            }
        }

        string selectedCountry = string.Empty;
        if (gameState.selectedCountry != null)
            selectedCountry = !string.IsNullOrWhiteSpace(gameState.selectedCountry.countryCode)
                ? gameState.selectedCountry.countryCode
                : gameState.selectedCountry.countryName;

        return new SaveData
        {
            saveVersion = 1,
            updatedAtUnixSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            playerXP = xpSystem.TotalXP,
            rank = xpSystem.CurrentRank,
            unlockedCities = unlockManager.GetUnlockedCityCodes(),
            unlockedBuses = fleetManager.GetOwnedBusIds(),
            activeUpgrades = SerializeActiveUpgrades(),
            liveries = SerializeLiveries(),
            missionHistory = missionHistory.ToArray(),
            completedRouteKeys = unlockManager.GetCompletedRouteKeys(),
            selectedCountry = selectedCountry ?? string.Empty,
            selectedCityCode = gameState.selectedCity != null ? gameState.selectedCity.cityCode : string.Empty,
            selectedRouteId = gameState.selectedRoute != null ? gameState.selectedRoute.GetProgressionId(gameState.selectedCity != null ? gameState.selectedCity.cityCode : null) : string.Empty,
            selectedBusId = fleetManager.GetSelectedBusId(),
            economy = CloneEconomy(gameState.economy),
            vehicleState = CloneVehicleState(gameState.vehicleState),
            lastMissionSettlement = CloneSettlement(gameState.lastMissionSettlement)
        };
    }

    public void ApplySaveData(SaveData saveData, bool saveLocalBackup = true, bool pushCloud = false)
    {
        if (saveData == null)
            return;

        var xpSystem = XPSystem.EnsureExists();
        var unlockManager = UnlockManager.EnsureExists();
        var fleetManager = BusFleetManager.EnsureExists();
        var gameState = EnsureGameStateExists();

        xpSystem.SetTotalXP(saveData.playerXP, saveLocalBackup: false, pushCloud: false);
        unlockManager.ApplyCompletedRouteKeys(saveData.completedRouteKeys);
        fleetManager.ApplySaveState(saveData.unlockedBuses, saveData.selectedBusId);

        gameState.economy = CloneEconomy(saveData.economy);
        gameState.vehicleState = CloneVehicleState(saveData.vehicleState);
        gameState.lastMissionSettlement = CloneSettlement(saveData.lastMissionSettlement);

        activeUpgrades.Clear();
        if (saveData.activeUpgrades != null)
        {
            for (int i = 0; i < saveData.activeUpgrades.Length; i++)
            {
                var entry = saveData.activeUpgrades[i];
                if (entry != null && !string.IsNullOrWhiteSpace(entry.key))
                    activeUpgrades[entry.key] = entry.value ?? string.Empty;
            }
        }

        UpgradeManager.EnsureExists().ApplySaveEntries(saveData.activeUpgrades);

        liveries.Clear();
        if (saveData.liveries != null)
        {
            for (int i = 0; i < saveData.liveries.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(saveData.liveries[i]))
                    liveries.Add(saveData.liveries[i]);
            }
        }

        missionHistory.Clear();
        if (saveData.missionHistory != null)
            missionHistory.AddRange(saveData.missionHistory);

        lastAppliedSave = saveData;
        ApplySelectionState(saveData);
        PersistAuxiliaryCollections();

        if (saveLocalBackup)
            WriteLocalBackup(saveData);

        if (pushCloud)
            PushToCloud(saveData);

        SaveApplied?.Invoke(saveData);
    }

    public void SaveNow(string reason, bool pushCloud = true)
    {
        if (!initialized || HasPendingConflict)
            return;

        SaveData data = CaptureCurrentData();
        WriteLocalBackup(data);

        if (pushCloud)
            PushToCloud(data);
    }

    public void RecordMissionResult(MissionResult result, BusRoute route, CityDefinition city)
    {
        if (result == null)
            return;

        missionHistory.Insert(0, new MissionHistoryEntry
        {
            routeId = route != null ? route.GetProgressionId(city != null ? city.cityCode : null) : string.Empty,
            routeName = result.routeName,
            cityCode = city != null ? city.cityCode : string.Empty,
            xpEarned = result.xpEarned,
            stars = result.starRating,
            netEarningsKES = result.netEarningsKES,
            totalScore = result.totalScore,
            completedAtUnixSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        });

        while (missionHistory.Count > Mathf.Max(1, missionHistoryLimit))
            missionHistory.RemoveAt(missionHistory.Count - 1);

        SaveNow("mission_complete");
    }

    public IReadOnlyList<MissionHistoryEntry> GetMissionHistory()
    {
        return missionHistory;
    }

    public bool TryGetRouteHistory(string routeId, out int bestStars, out float bestScore, out long lastPlayedUnixSeconds)
    {
        bestStars = 0; bestScore = 0f; lastPlayedUnixSeconds = 0;
        if (string.IsNullOrWhiteSpace(routeId)) return false;
        bool found = false;
        for (int i = 0; i < missionHistory.Count; i++)
        {
            MissionHistoryEntry entry = missionHistory[i];
            if (entry == null || !string.Equals(entry.routeId, routeId, StringComparison.OrdinalIgnoreCase)) continue;
            found = true; bestStars = Mathf.Max(bestStars, entry.stars); bestScore = Mathf.Max(bestScore, entry.totalScore); lastPlayedUnixSeconds = Math.Max(lastPlayedUnixSeconds, entry.completedAtUnixSeconds);
        }
        return found;
    }

    public void RegisterUpgradePurchase(string upgradeId, string value = "owned")
    {
        if (string.IsNullOrWhiteSpace(upgradeId))
            return;

        activeUpgrades[upgradeId] = value ?? string.Empty;
        PersistAuxiliaryCollections();
        SaveNow("upgrade_purchase");
    }

    public void RegisterLivery(string liveryId)
    {
        if (string.IsNullOrWhiteSpace(liveryId))
            return;

        if (liveries.Add(liveryId))
        {
            PersistAuxiliaryCollections();
            SaveNow("livery_unlock");
        }
    }

    public void RegisterLivery(string busId, string liveryCode)
    {
        if (string.IsNullOrWhiteSpace(busId) || string.IsNullOrWhiteSpace(liveryCode))
            return;

        if (!LiveryCodeCodec.TryDecode(liveryCode, out _))
            return;

        string prefix = busId.Trim() + "=";
        liveries.RemoveWhere(value => value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
        liveries.Add(prefix + LiveryCodeCodec.Normalize(liveryCode));
        PersistAuxiliaryCollections();
        SaveNow("livery_save");
    }

    public bool TryGetLiveryCode(string busId, out string liveryCode)
    {
        liveryCode = string.Empty;
        if (string.IsNullOrWhiteSpace(busId))
            return false;

        string prefix = busId.Trim() + "=";
        foreach (string entry in liveries)
        {
            if (!entry.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                continue;

            string candidate = entry.Substring(prefix.Length);
            if (LiveryCodeCodec.TryDecode(candidate, out _))
            {
                liveryCode = LiveryCodeCodec.Normalize(candidate);
                return true;
            }
        }

        return false;
    }

    public void ResolvePendingConflictUsingCloud()
    {
        if (!HasPendingConflict)
            return;

        SaveConflictInfo conflict = pendingConflict;
        pendingConflict = null;
        ApplySaveData(conflict.cloudSave, saveLocalBackup: true, pushCloud: false);
    }

    public void ResolvePendingConflictUsingLocal()
    {
        if (!HasPendingConflict)
            return;

        SaveConflictInfo conflict = pendingConflict;
        pendingConflict = null;
        ApplySaveData(conflict.localSave, saveLocalBackup: true, pushCloud: true);
    }

    void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (lastAppliedSave != null)
            ApplySelectionState(lastAppliedSave);
    }

    void SubscribeToRuntimeEvents()
    {
        if (XPSystem.Instance != null)
            XPSystem.Instance.RankUpTriggered += HandleRankUpTriggered;
    }

    void UnsubscribeFromRuntimeEvents()
    {
        if (XPSystem.Instance != null)
            XPSystem.Instance.RankUpTriggered -= HandleRankUpTriggered;
    }

    void HandleRankUpTriggered(RankUpEventData rankUp)
    {
        SaveNow("rank_up");
    }

    void TryPullFromCloud()
    {
        if (cloudPullAttempted || !cloudSync.IsAvailable)
            return;

        cloudPullAttempted = true;
        cloudSync.Pull(GetOrCreatePlayFabCustomId(), ResolveCloudSave, error =>
        {
            Debug.Log($"SaveManager: Cloud save unavailable. Using local backup. {error}");
        });
    }

    void ResolveCloudSave(SaveData cloudSave)
    {
        SaveData localSave = LoadLocalBackup();
        if (cloudSave == null)
        {
            if (localSave != null)
                PushToCloud(localSave);
            return;
        }

        if (localSave == null)
        {
            ApplySaveData(cloudSave, saveLocalBackup: true, pushCloud: false);
            return;
        }

        if (AreEquivalent(localSave, cloudSave))
        {
            ApplySaveData(localSave, saveLocalBackup: false, pushCloud: false);
            return;
        }

        long deltaSeconds = Mathf.Abs((int)(cloudSave.updatedAtUnixSeconds - localSave.updatedAtUnixSeconds));
        if (deltaSeconds <= conflictAmbiguityWindowSeconds)
        {
            pendingConflict = new SaveConflictInfo
            {
                localSave = localSave,
                cloudSave = cloudSave,
                reason = "Local and cloud saves differ but were updated at nearly the same time."
            };
            SaveConflictDetected?.Invoke(pendingConflict);
            return;
        }

        if (cloudSave.updatedAtUnixSeconds > localSave.updatedAtUnixSeconds)
            ApplySaveData(cloudSave, saveLocalBackup: true, pushCloud: false);
        else
            ApplySaveData(localSave, saveLocalBackup: true, pushCloud: true);
    }

    void PushToCloud(SaveData data)
    {
        if (data == null || !cloudSync.IsAvailable || HasPendingConflict)
            return;

        cloudSync.Push(GetOrCreatePlayFabCustomId(), data, null, error =>
        {
            Debug.Log($"SaveManager: Failed to push cloud save. {error}");
        });
    }

    void WriteLocalBackup(SaveData data)
    {
        if (data == null)
            return;

        PlayerPrefs.SetString(LocalSavePrefsKey, JsonUtility.ToJson(data));
        PersistAuxiliaryCollections();
        PlayerPrefs.Save();
        lastAppliedSave = data;
    }

    SaveData LoadLocalBackup()
    {
        string payload = PlayerPrefs.GetString(LocalSavePrefsKey, string.Empty);
        if (string.IsNullOrWhiteSpace(payload))
            return null;

        try
        {
            return JsonUtility.FromJson<SaveData>(payload);
        }
        catch (Exception exception)
        {
            Debug.LogWarning($"SaveManager: Failed to parse local save. {exception.Message}");
            return null;
        }
    }

    void LoadUpgradeStateFromPrefs()
    {
        activeUpgrades.Clear();
        string payload = PlayerPrefs.GetString(PendingUpgradePrefsKey, string.Empty);
        if (string.IsNullOrWhiteSpace(payload))
            return;

        var container = JsonUtility.FromJson<SaveKeyValueEntryArrayWrapper>(payload);
        if (container?.entries == null)
            return;

        for (int i = 0; i < container.entries.Length; i++)
        {
            var entry = container.entries[i];
            if (entry != null && !string.IsNullOrWhiteSpace(entry.key))
                activeUpgrades[entry.key] = entry.value ?? string.Empty;
        }
    }

    void LoadLiveryStateFromPrefs()
    {
        liveries.Clear();
        string raw = PlayerPrefs.GetString(KnownLiveriesPrefsKey, string.Empty);
        if (string.IsNullOrWhiteSpace(raw))
            return;

        string[] values = raw.Split('|');
        for (int i = 0; i < values.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(values[i]))
                liveries.Add(values[i]);
        }
    }

    void PersistAuxiliaryCollections()
    {
        var wrapper = new SaveKeyValueEntryArrayWrapper
        {
            entries = SerializeActiveUpgrades()
        };

        PlayerPrefs.SetString(PendingUpgradePrefsKey, JsonUtility.ToJson(wrapper));
        PlayerPrefs.SetString(KnownLiveriesPrefsKey, string.Join("|", SerializeLiveries()));
    }

    SaveKeyValueEntry[] SerializeActiveUpgrades()
    {
        var entries = new List<SaveKeyValueEntry>(activeUpgrades.Count);
        foreach (var pair in activeUpgrades)
        {
            entries.Add(new SaveKeyValueEntry
            {
                key = pair.Key,
                value = pair.Value
            });
        }

        entries.Sort((left, right) => string.Compare(left.key, right.key, StringComparison.OrdinalIgnoreCase));
        return entries.ToArray();
    }

    string[] SerializeLiveries()
    {
        var values = new List<string>(liveries);
        values.Sort(StringComparer.OrdinalIgnoreCase);
        return values.ToArray();
    }

    void ApplySelectionState(SaveData saveData)
    {
        if (saveData == null)
            return;

        var cityManager = CityManager.Instance;
        var gameState = GameState.Instance;
        if (gameState == null)
            return;

        if (cityManager?.allCountries != null && !string.IsNullOrWhiteSpace(saveData.selectedCountry))
        {
            CountryDefinition country = FindCountry(saveData.selectedCountry, cityManager.allCountries);
            if (country != null)
            {
                gameState.selectedCountry = country;
                cityManager.activeCountry = country;
            }
        }

        if (cityManager?.allCities != null && !string.IsNullOrWhiteSpace(saveData.selectedCityCode))
        {
            var city = cityManager.GetCityByCode(saveData.selectedCityCode);
            if (city != null)
            {
                gameState.selectedCity = city;
                cityManager.activeCity = city;
            }
        }

        if (gameState.selectedCity != null &&
            gameState.selectedCity.availableRoutes != null &&
            !string.IsNullOrWhiteSpace(saveData.selectedRouteId))
        {
            for (int i = 0; i < gameState.selectedCity.availableRoutes.Length; i++)
            {
                var route = gameState.selectedCity.availableRoutes[i];
                if (route != null &&
                    string.Equals(route.GetProgressionId(gameState.selectedCity.cityCode), saveData.selectedRouteId, StringComparison.OrdinalIgnoreCase))
                {
                    gameState.selectedRoute = route;
                    break;
                }
            }
        }
    }

    static CountryDefinition FindCountry(string identifier, CountryDefinition[] countries)
    {
        for (int i = 0; i < countries.Length; i++)
        {
            var country = countries[i];
            if (country == null)
                continue;

            if (string.Equals(country.countryCode, identifier, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(country.countryName, identifier, StringComparison.OrdinalIgnoreCase))
            {
                return country;
            }
        }

        return null;
    }

    static bool AreEquivalent(SaveData left, SaveData right)
    {
        if (left == null || right == null)
            return left == right;

        string leftJson = UpdatedAtJsonPattern.Replace(JsonUtility.ToJson(left), string.Empty);
        string rightJson = UpdatedAtJsonPattern.Replace(JsonUtility.ToJson(right), string.Empty);
        return leftJson == rightJson;
    }

    static PlayerEconomyData CloneEconomy(PlayerEconomyData source)
    {
        return source == null
            ? new PlayerEconomyData()
            : new PlayerEconomyData
            {
                balanceKES = source.balanceKES,
                driverReputationRating = source.driverReputationRating
            };
    }

    static VehiclePersistentState CloneVehicleState(VehiclePersistentState source)
    {
        var state = new VehiclePersistentState();
        if (source == null)
            return state;

        state.activeBusId = source.activeBusId;
        state.isElectricBus = source.isElectricBus;
        state.energyUnitLabel = source.energyUnitLabel;
        state.passengerCapacity = source.passengerCapacity;
        state.seatedCapacity = source.seatedCapacity;
        state.fuelCapacityLitres = source.fuelCapacityLitres;
        state.fuelLitres = source.fuelLitres;
        state.totalFuelConsumedLitres = source.totalFuelConsumedLitres;
        state.lowFuelWarningTriggered = source.lowFuelWarningTriggered;
        state.criticalFuelWarningTriggered = source.criticalFuelWarningTriggered;
        state.brakeWearNormalized = source.brakeWearNormalized;
        state.engineHours = source.engineHours;
        state.doorFunctionOperational = source.doorFunctionOperational;
        state.exteriorLightsOperational = source.exteriorLightsOperational;
        state.axleTyreWearNormalized = source.axleTyreWearNormalized != null
            ? (float[])source.axleTyreWearNormalized.Clone()
            : new float[] { 0.06f, 0.08f, 0.08f };
        return state;
    }

    static MissionSettlementData CloneSettlement(MissionSettlementData source)
    {
        return source == null
            ? new MissionSettlementData()
            : new MissionSettlementData
            {
                grossEarningsKES = source.grossEarningsKES,
                fuelRefuelCostKES = source.fuelRefuelCostKES,
                maintenanceCostKES = source.maintenanceCostKES,
                netEarningsKES = source.netEarningsKES,
                autoRefuelApplied = source.autoRefuelApplied
            };
    }

    static GameState EnsureGameStateExists()
    {
        if (GameState.Instance != null)
            return GameState.Instance;

        var existing = FindFirstObjectByType<GameState>();
        if (existing != null)
            return existing;

        var root = new GameObject("GameState");
        return root.AddComponent<GameState>();
    }

    string GetOrCreatePlayFabCustomId()
    {
        string customId = PlayerPrefs.GetString(XPSystem.PlayFabCustomIdPrefsKey, string.Empty);
        if (!string.IsNullOrEmpty(customId))
            return customId;

        customId = Guid.NewGuid().ToString("N");
        PlayerPrefs.SetString(XPSystem.PlayFabCustomIdPrefsKey, customId);
        PlayerPrefs.Save();
        return customId;
    }

    [Serializable]
    class SaveKeyValueEntryArrayWrapper
    {
        public SaveKeyValueEntry[] entries = Array.Empty<SaveKeyValueEntry>();
    }
}

public class PlayFabSaveCloudSync : ISaveCloudSync
{
    const string CloudSaveKey = "save.player_data.json";

    public bool IsAvailable
    {
        get
        {
#if PLAYFAB_SDK
            return PlayFabSettings.staticSettings != null &&
                   !string.IsNullOrWhiteSpace(PlayFabSettings.staticSettings.TitleId);
#else
            return false;
#endif
        }
    }

    public void Pull(string customId, Action<SaveData> onSuccess, Action<string> onError)
    {
#if PLAYFAB_SDK
        EnsureLogin(customId, () =>
        {
            var cloudRequest = new ExecuteCloudScriptRequest
            {
                FunctionName = "GetPlayerSaveData",
                GeneratePlayStreamEvent = false
            };

            PlayFabClientAPI.ExecuteCloudScript(cloudRequest, result =>
            {
                if (TryExtractSavePayload(result.FunctionResult, out string payload))
                {
                    onSuccess?.Invoke(JsonUtility.FromJson<SaveData>(payload));
                    return;
                }

                FallbackPull(onSuccess, onError);
            }, _ => FallbackPull(onSuccess, onError));
        }, onError);
#else
        onError?.Invoke("PlayFab SDK define not enabled.");
#endif
    }

    public void Push(string customId, SaveData saveData, Action onSuccess, Action<string> onError)
    {
#if PLAYFAB_SDK
        EnsureLogin(customId, () =>
        {
            string payload = JsonUtility.ToJson(saveData);
            var request = new ExecuteCloudScriptRequest
            {
                FunctionName = "SetPlayerSaveData",
                FunctionParameter = new Dictionary<string, object>
                {
                    { "payload", payload }
                },
                GeneratePlayStreamEvent = false
            };

            PlayFabClientAPI.ExecuteCloudScript(request, _ => onSuccess?.Invoke(), _ => FallbackPush(payload, onSuccess, onError));
        }, onError);
#else
        onError?.Invoke("PlayFab SDK define not enabled.");
#endif
    }

#if PLAYFAB_SDK
    static bool loggedIn;

    static void EnsureLogin(string customId, Action onSuccess, Action<string> onError)
    {
        if (loggedIn)
        {
            onSuccess?.Invoke();
            return;
        }

        var request = new LoginWithCustomIDRequest
        {
            CustomId = customId,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, _ =>
        {
            loggedIn = true;
            onSuccess?.Invoke();
        }, error => onError?.Invoke(error.GenerateErrorReport()));
    }

    static void FallbackPull(Action<SaveData> onSuccess, Action<string> onError)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            if (result.Data != null &&
                result.Data.TryGetValue(CloudSaveKey, out var record) &&
                !string.IsNullOrWhiteSpace(record.Value))
            {
                onSuccess?.Invoke(JsonUtility.FromJson<SaveData>(record.Value));
                return;
            }

            onSuccess?.Invoke(null);
        }, error => onError?.Invoke(error.GenerateErrorReport()));
    }

    static void FallbackPush(string payload, Action onSuccess, Action<string> onError)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { CloudSaveKey, payload }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, _ => onSuccess?.Invoke(), error => onError?.Invoke(error.GenerateErrorReport()));
    }

    static bool TryExtractSavePayload(object functionResult, out string payload)
    {
        payload = null;
        if (functionResult == null)
            return false;

        if (functionResult is string text && !string.IsNullOrWhiteSpace(text))
        {
            payload = text;
            return true;
        }

        if (functionResult is Dictionary<string, object> dictionary)
        {
            if (dictionary.TryGetValue("payload", out object payloadValue) &&
                payloadValue is string payloadText &&
                !string.IsNullOrWhiteSpace(payloadText))
            {
                payload = payloadText;
                return true;
            }

            if (dictionary.TryGetValue("saveData", out object saveValue) &&
                saveValue is string saveText &&
                !string.IsNullOrWhiteSpace(saveText))
            {
                payload = saveText;
                return true;
            }
        }

        string serialized = Json.Serialize(functionResult);
        if (!string.IsNullOrWhiteSpace(serialized) && serialized.StartsWith("{", StringComparison.Ordinal))
        {
            payload = serialized;
            return true;
        }

        return false;
    }
#endif
}
