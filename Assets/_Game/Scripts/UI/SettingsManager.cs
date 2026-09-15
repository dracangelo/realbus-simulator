using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum RealBusLanguage { English, Swahili, French, German, Spanish }

[Serializable]
public class RealBusSettings
{
    public int controlScheme;
    public int qualityLevel;
    public float masterVolume = 1f;
    public float musicVolume = 0.8f;
    public float effectsVolume = 1f;
    public RealBusLanguage language;
    public bool largeText;
    public bool colourBlindIcons = true;
    public bool highContrast;
    public bool haptics = true;
    public bool subtitles = true;
    public bool leftHandedControls;
    public bool drivingAssists = true;
    public bool adaptivePerformance = true;
    public bool reducedMotion;
    [Range(30, 60)] public int targetFrameRate = 45;
    [Range(0.5f, 1.5f)] public float touchControlScale = 1f;
    [Range(0.25f, 1.5f)] public float steeringSensitivity = 1f;
    [Range(0f, 0.3f)] public float steeringDeadZone = 0.06f;
}

public class SettingsManager : MonoBehaviour
{
    const string PrefsKey = "RealBus.Settings.v1";
    public static SettingsManager Instance { get; private set; }
    public RealBusSettings Current { get; private set; } = new RealBusSettings();
    public event Action SettingsChanged;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static SettingsManager EnsureExists()
    {
        if (Instance != null) return Instance;
        SettingsManager existing = FindFirstObjectByType<SettingsManager>();
        return existing != null ? existing : new GameObject("SettingsManager").AddComponent<SettingsManager>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); Load(); SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    void OnDestroy() { if (Instance == this) SceneManager.sceneLoaded -= HandleSceneLoaded; }
    void HandleSceneLoaded(Scene scene, LoadSceneMode mode) { Apply(); }

    public void Load()
    {
        string json = PlayerPrefs.GetString(PrefsKey, string.Empty);
        if (!string.IsNullOrWhiteSpace(json))
        {
            try { JsonUtility.FromJsonOverwrite(json, Current); }
            catch (Exception ex) { Debug.LogWarning("SettingsManager: Ignoring invalid settings: " + ex.Message); }
        }
        Current.controlScheme = Mathf.Clamp(Current.controlScheme, 0, 2);
        Current.qualityLevel = Mathf.Clamp(Current.qualityLevel, 0, Mathf.Max(0, QualitySettings.names.Length - 1));
        Current.masterVolume = Mathf.Clamp01(Current.masterVolume);
        Current.musicVolume = Mathf.Clamp01(Current.musicVolume);
        Current.effectsVolume = Mathf.Clamp01(Current.effectsVolume);
        Current.targetFrameRate = NormalizeFrameRate(Current.targetFrameRate);
        Current.touchControlScale = Mathf.Clamp(Current.touchControlScale, 0.5f, 1.5f);
        Current.steeringSensitivity = Mathf.Clamp(Current.steeringSensitivity, 0.25f, 1.5f);
        Current.steeringDeadZone = Mathf.Clamp(Current.steeringDeadZone, 0f, 0.3f);
        Apply();
    }

    public void SaveAndApply()
    {
        PlayerPrefs.SetString(PrefsKey, JsonUtility.ToJson(Current));
        PlayerPrefs.Save();
        Apply(); SettingsChanged?.Invoke();
    }

    public void Apply()
    {
        AudioListener.volume = Current.masterVolume;
        if (QualitySettings.names.Length > 0 && QualitySettings.GetQualityLevel() != Current.qualityLevel)
            QualitySettings.SetQualityLevel(Current.qualityLevel, true);
        MobileControlsUI[] controls = FindObjectsByType<MobileControlsUI>(FindObjectsSortMode.None);
        for (int i = 0; i < controls.Length; i++) controls[i].ApplyControlPreferences(Current);
        if (VisualQualityController.Instance != null) VisualQualityController.Instance.ApplyScenePolish();
    }

    public void CycleControl() { Current.controlScheme = (Current.controlScheme + 1) % 3; SaveAndApply(); }
    public void CycleQuality() { Current.qualityLevel = QualitySettings.names.Length == 0 ? 0 : (Current.qualityLevel + 1) % QualitySettings.names.Length; SaveAndApply(); }
    public void CycleLanguage() { Current.language = (RealBusLanguage)(((int)Current.language + 1) % Enum.GetValues(typeof(RealBusLanguage)).Length); SaveAndApply(); }
    public void CycleFrameRate() { Current.targetFrameRate = Current.targetFrameRate == 30 ? 45 : Current.targetFrameRate == 45 ? 60 : 30; SaveAndApply(); }
    public static string ControlLabel(int value) { return value == 1 ? "Steering wheel" : value == 2 ? "Tilt" : "Buttons"; }
    public static int NormalizeFrameRate(int value) { return value <= 37 ? 30 : value <= 52 ? 45 : 60; }
}
