using UnityEngine;

public enum MobileThermalState { Nominal, Warm, Hot, Critical }

public class MobilePerformanceManager : MonoBehaviour
{
    public static MobilePerformanceManager Instance { get; private set; }
    public float CurrentResolutionScale { get; private set; } = 1f;
    public float AverageFrameMilliseconds { get; private set; }
    public MobileThermalState ThermalState { get; private set; }

    float smoothedDelta = 1f / 45f;
    float nextEvaluation;
    float nextThermalPoll;
    float goodPerformanceSeconds;
    float lowMemoryModeUntil;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static MobilePerformanceManager EnsureExists()
    {
        if (Instance != null) return Instance;
        MobilePerformanceManager existing = FindFirstObjectByType<MobilePerformanceManager>();
        return existing != null ? existing : new GameObject("MobilePerformanceManager").AddComponent<MobilePerformanceManager>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject);
        SettingsManager settings = SettingsManager.EnsureExists(); settings.SettingsChanged += ApplySettings; ApplySettings();
    }

    void OnDestroy() { if (Instance == this && SettingsManager.Instance != null) SettingsManager.Instance.SettingsChanged -= ApplySettings; }

    void Update()
    {
        float delta = Mathf.Clamp(Time.unscaledDeltaTime, 0.001f, 0.2f);
        smoothedDelta = Mathf.Lerp(smoothedDelta, delta, 0.04f);
        AverageFrameMilliseconds = smoothedDelta * 1000f;
        if (Time.unscaledTime >= nextThermalPoll) { nextThermalPoll = Time.unscaledTime + 10f; ThermalState = ReadThermalState(); }
        if (Time.unscaledTime < nextEvaluation) return;
        nextEvaluation = Time.unscaledTime + 3f;
        EvaluateAdaptiveScale();
    }

    public void EnterLowMemoryMode(float seconds = 45f)
    {
        lowMemoryModeUntil = Time.unscaledTime + Mathf.Max(10f, seconds);
        Application.targetFrameRate = 30; SetResolutionScale(0.65f);
    }

    public void ApplySettings()
    {
        RealBusSettings settings = SettingsManager.EnsureExists().Current;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = settings.targetFrameRate;
        if (!settings.adaptivePerformance) SetResolutionScale(1f);
    }

    void EvaluateAdaptiveScale()
    {
        RealBusSettings settings = SettingsManager.EnsureExists().Current;
        if (!settings.adaptivePerformance) return;
        if (Time.unscaledTime < lowMemoryModeUntil) { Application.targetFrameRate = 30; SetResolutionScale(0.65f); return; }
        float budget = 1000f / Mathf.Max(30, settings.targetFrameRate);
        float next = RecommendScale(CurrentResolutionScale, AverageFrameMilliseconds, budget, ThermalState);
        if (AverageFrameMilliseconds < budget * 0.82f && ThermalState <= MobileThermalState.Warm) goodPerformanceSeconds += 3f;
        else goodPerformanceSeconds = 0f;
        if (next > CurrentResolutionScale && goodPerformanceSeconds < 12f) next = CurrentResolutionScale;
        if (!Mathf.Approximately(next, CurrentResolutionScale)) { SetResolutionScale(next); goodPerformanceSeconds = 0f; }
        if (ThermalState >= MobileThermalState.Hot && Application.targetFrameRate > 30) Application.targetFrameRate = 30;
        else if (ThermalState <= MobileThermalState.Warm && Application.targetFrameRate != settings.targetFrameRate) Application.targetFrameRate = settings.targetFrameRate;
    }

    public static float RecommendScale(float current, float frameMilliseconds, float budgetMilliseconds, MobileThermalState thermal)
    {
        float minimum = thermal >= MobileThermalState.Critical ? 0.65f : 0.72f;
        if (thermal >= MobileThermalState.Hot || frameMilliseconds > budgetMilliseconds * 1.12f)
            return Mathf.Max(minimum, current - (thermal >= MobileThermalState.Critical ? 0.1f : 0.05f));
        if (thermal == MobileThermalState.Nominal && frameMilliseconds < budgetMilliseconds * 0.82f)
            return Mathf.Min(1f, current + 0.05f);
        return current;
    }

    void SetResolutionScale(float value)
    {
        CurrentResolutionScale = Mathf.Clamp(value, 0.65f, 1f);
        ScalableBufferManager.ResizeBuffers(CurrentResolutionScale, CurrentResolutionScale);
    }

    MobileThermalState ReadThermalState()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            using (var power = activity.Call<AndroidJavaObject>("getSystemService", "power"))
            {
                int status = power.Call<int>("getCurrentThermalStatus");
                if (status >= 5) return MobileThermalState.Critical;
                if (status >= 3) return MobileThermalState.Hot;
                if (status >= 1) return MobileThermalState.Warm;
            }
        }
        catch (System.Exception) { }
#endif
        return MobileThermalState.Nominal;
    }
}
