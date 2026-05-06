using System;
using System.Collections.Generic;
using UnityEngine;

#if PLAYFAB_SDK
using PlayFab;
using PlayFab.ClientModels;
#endif

[Serializable]
public struct RankDefinition
{
    public int rank;
    public string title;
    public int requiredTotalXP;
    public string unlockReveal;
}

[Serializable]
public struct RankProgressSnapshot
{
    public int totalXP;
    public int currentRank;
    public string currentRankTitle;
    public int currentRankStartXP;
    public int nextRankXP;
    public string nextRankTitle;
    public string nextRankUnlockReveal;
    public int xpIntoCurrentRank;
    public int xpRequiredForNextRank;
    public int xpToNextRank;
    public float progress01;
    public bool isMaxRank;
}

[Serializable]
public class RankUpEventData
{
    public int newRank;
    public string rankTitle;
    public string unlockReveal;
}

[Serializable]
public class XPAwardResult
{
    public int awardedXP;
    public int previousTotalXP;
    public int newTotalXP;
    public int previousRank;
    public int newRank;
    public string source;
    public List<RankUpEventData> rankUps = new List<RankUpEventData>();

    public bool RankedUp => rankUps != null && rankUps.Count > 0;
}

public interface IXPCloudSync
{
    bool IsAvailable { get; }
    void Pull(string customId, Action<int?> onSuccess, Action<string> onError);
    void Push(string customId, int totalXP, Action onSuccess, Action<string> onError);
}

public class XPSystem : MonoBehaviour
{
    public const string TotalXpPlayerPrefsKey = "progress.total_xp";
    public const string PlayFabCustomIdPrefsKey = "progress.playfab_custom_id";

    public static XPSystem Instance { get; private set; }

    public event Action<RankProgressSnapshot> XPChanged;
    public event Action<RankUpEventData> RankUpTriggered;

    public int TotalXP => totalXP;
    public int CurrentRank => GetProgressSnapshot().currentRank;
    public int XPToNextRank => GetProgressSnapshot().xpToNextRank;
    public IReadOnlyList<RankDefinition> RankDefinitions => rankDefinitions;

    [SerializeField] List<RankDefinition> rankDefinitions = new List<RankDefinition>();

    int totalXP;
    bool initialized;
    bool cloudPullAttempted;
    readonly IXPCloudSync cloudSync = new PlayFabXPCloudSync();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap()
    {
        EnsureExists();
    }

    public static XPSystem EnsureExists()
    {
        if (Instance != null)
            return Instance;

        var existing = FindFirstObjectByType<XPSystem>();
        if (existing != null)
            return existing;

        var root = new GameObject("XPSystem");
        return root.AddComponent<XPSystem>();
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

        EnsureInitialized();
    }

    void OnApplicationPause(bool paused)
    {
        if (!paused)
            return;

        SaveLocalBackup();
        PushToCloud();
    }

    void OnApplicationQuit()
    {
        SaveLocalBackup();
        PushToCloud();
    }

    public void EnsureInitialized()
    {
        if (initialized)
            return;

        if (rankDefinitions == null || rankDefinitions.Count == 0)
            rankDefinitions = BuildDefaultRankDefinitions();

        totalXP = Mathf.Max(0, PlayerPrefs.GetInt(TotalXpPlayerPrefsKey, 0));
        initialized = true;

        XPChanged?.Invoke(GetProgressSnapshot());
        TryPullFromCloud();
    }

    public RankProgressSnapshot GetProgressSnapshot()
    {
        return EvaluateProgress(totalXP);
    }

    public RankProgressSnapshot EvaluateProgress(int xp)
    {
        EnsureInitialized();

        int clampedXp = Mathf.Max(0, xp);
        RankDefinition current = GetRankDefinitionForXP(clampedXp);
        int currentIndex = Mathf.Clamp(current.rank - 1, 0, rankDefinitions.Count - 1);
        bool isMaxRank = currentIndex >= rankDefinitions.Count - 1;

        RankDefinition next = isMaxRank ? current : rankDefinitions[currentIndex + 1];
        int currentStart = current.requiredTotalXP;
        int nextTarget = isMaxRank ? current.requiredTotalXP : next.requiredTotalXP;
        int xpSpan = Mathf.Max(1, nextTarget - currentStart);
        int xpIntoCurrent = isMaxRank ? xpSpan : Mathf.Clamp(clampedXp - currentStart, 0, xpSpan);
        int xpToNext = isMaxRank ? 0 : Mathf.Max(0, nextTarget - clampedXp);
        float progress = isMaxRank ? 1f : Mathf.Clamp01((float)xpIntoCurrent / xpSpan);

        return new RankProgressSnapshot
        {
            totalXP = clampedXp,
            currentRank = current.rank,
            currentRankTitle = current.title,
            currentRankStartXP = currentStart,
            nextRankXP = nextTarget,
            nextRankTitle = isMaxRank ? current.title : next.title,
            nextRankUnlockReveal = isMaxRank ? current.unlockReveal : next.unlockReveal,
            xpIntoCurrentRank = xpIntoCurrent,
            xpRequiredForNextRank = isMaxRank ? 0 : xpSpan,
            xpToNextRank = xpToNext,
            progress01 = progress,
            isMaxRank = isMaxRank
        };
    }

    public XPAwardResult AwardXP(int amount, string source = null)
    {
        EnsureInitialized();

        int award = Mathf.Max(0, amount);
        var before = GetProgressSnapshot();
        totalXP = Mathf.Max(0, totalXP + award);
        var after = GetProgressSnapshot();

        SaveLocalBackup();
        PushToCloud();

        var result = new XPAwardResult
        {
            awardedXP = award,
            previousTotalXP = before.totalXP,
            newTotalXP = after.totalXP,
            previousRank = before.currentRank,
            newRank = after.currentRank,
            source = source ?? string.Empty
        };

        for (int rank = before.currentRank + 1; rank <= after.currentRank; rank++)
        {
            RankDefinition definition = GetRankDefinition(rank);
            var rankUp = new RankUpEventData
            {
                newRank = definition.rank,
                rankTitle = definition.title,
                unlockReveal = definition.unlockReveal
            };

            result.rankUps.Add(rankUp);
            RankUpTriggered?.Invoke(rankUp);
        }

        XPChanged?.Invoke(after);
        return result;
    }

    public void SetTotalXP(int value, bool saveLocalBackup = true, bool pushCloud = false)
    {
        EnsureInitialized();

        int clampedValue = Mathf.Max(0, value);
        if (clampedValue == totalXP)
            return;

        totalXP = clampedValue;

        if (saveLocalBackup)
            SaveLocalBackup();

        if (pushCloud)
            PushToCloud();

        XPChanged?.Invoke(GetProgressSnapshot());
    }

    public RankDefinition GetRankDefinition(int rank)
    {
        EnsureInitialized();

        int index = Mathf.Clamp(rank - 1, 0, rankDefinitions.Count - 1);
        return rankDefinitions[index];
    }

    void SaveLocalBackup()
    {
        PlayerPrefs.SetInt(TotalXpPlayerPrefsKey, totalXP);
        PlayerPrefs.Save();
    }

    void TryPullFromCloud()
    {
        if (cloudPullAttempted || !cloudSync.IsAvailable)
            return;

        cloudPullAttempted = true;
        cloudSync.Pull(GetOrCreatePlayFabCustomId(), OnCloudPullSucceeded, OnCloudPullFailed);
    }

    void OnCloudPullSucceeded(int? cloudXP)
    {
        if (!cloudXP.HasValue)
        {
            PushToCloud();
            return;
        }

        if (cloudXP.Value > totalXP)
        {
            totalXP = Mathf.Max(0, cloudXP.Value);
            SaveLocalBackup();
            XPChanged?.Invoke(GetProgressSnapshot());
            return;
        }

        if (cloudXP.Value < totalXP)
            PushToCloud();
    }

    void OnCloudPullFailed(string errorMessage)
    {
        Debug.Log($"XPSystem: PlayFab sync unavailable. Using local XP backup. {errorMessage}");
    }

    void PushToCloud()
    {
        if (!initialized || !cloudSync.IsAvailable)
            return;

        cloudSync.Push(GetOrCreatePlayFabCustomId(), totalXP, null, error =>
        {
            Debug.Log($"XPSystem: Failed to push XP to PlayFab. {error}");
        });
    }

    string GetOrCreatePlayFabCustomId()
    {
        string customId = PlayerPrefs.GetString(PlayFabCustomIdPrefsKey, string.Empty);
        if (!string.IsNullOrEmpty(customId))
            return customId;

        customId = Guid.NewGuid().ToString("N");
        PlayerPrefs.SetString(PlayFabCustomIdPrefsKey, customId);
        PlayerPrefs.Save();
        return customId;
    }

    RankDefinition GetRankDefinitionForXP(int xp)
    {
        for (int i = rankDefinitions.Count - 1; i >= 0; i--)
        {
            if (xp >= rankDefinitions[i].requiredTotalXP)
                return rankDefinitions[i];
        }

        return rankDefinitions[0];
    }

    static List<RankDefinition> BuildDefaultRankDefinitions()
    {
        return new List<RankDefinition>
        {
            new RankDefinition { rank = 1, title = "Depot Rookie", requiredTotalXP = 0, unlockReveal = "Nairobi depot license active" },
            new RankDefinition { rank = 2, title = "Route Scout", requiredTotalXP = 500, unlockReveal = "Longer shift chains unlocked" },
            new RankDefinition { rank = 3, title = "City Driver", requiredTotalXP = 1200, unlockReveal = "Mombasa network unlocked" },
            new RankDefinition { rank = 4, title = "Rush Hour Pro", requiredTotalXP = 2200, unlockReveal = "Priority dispatch contracts unlocked" },
            new RankDefinition { rank = 5, title = "Fleet Specialist", requiredTotalXP = 3600, unlockReveal = "Kisumu network unlocked" },
            new RankDefinition { rank = 6, title = "Express Pilot", requiredTotalXP = 5400, unlockReveal = "Nakuru network unlocked" },
            new RankDefinition { rank = 7, title = "Safety Marshal", requiredTotalXP = 7600, unlockReveal = "Elite route endorsements unlocked" },
            new RankDefinition { rank = 8, title = "Network Captain", requiredTotalXP = 10400, unlockReveal = "Kampala network unlocked" },
            new RankDefinition { rank = 9, title = "Transit Veteran", requiredTotalXP = 13800, unlockReveal = "Grand Tour showcases unlocked" },
            new RankDefinition { rank = 10, title = "City Legend", requiredTotalXP = 18000, unlockReveal = "Master operator status reached" }
        };
    }
}

public class PlayFabXPCloudSync : IXPCloudSync
{
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

    public void Pull(string customId, Action<int?> onSuccess, Action<string> onError)
    {
#if PLAYFAB_SDK
        EnsureLogin(customId, () =>
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
            {
                if (result.Data != null &&
                    result.Data.TryGetValue(XPSystem.TotalXpPlayerPrefsKey, out var record) &&
                    int.TryParse(record.Value, out int cloudXP))
                {
                    onSuccess?.Invoke(cloudXP);
                    return;
                }

                onSuccess?.Invoke(null);
            }, error => onError?.Invoke(error.GenerateErrorReport()));
        }, onError);
#else
        onError?.Invoke("PlayFab SDK define not enabled.");
#endif
    }

    public void Push(string customId, int totalXP, Action onSuccess, Action<string> onError)
    {
#if PLAYFAB_SDK
        EnsureLogin(customId, () =>
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { XPSystem.TotalXpPlayerPrefsKey, totalXP.ToString() }
                }
            };

            PlayFabClientAPI.UpdateUserData(request, _ => onSuccess?.Invoke(), error => onError?.Invoke(error.GenerateErrorReport()));
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
#endif
}
