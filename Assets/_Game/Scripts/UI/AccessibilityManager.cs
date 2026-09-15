using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum HapticCue { Docking, Collision, MissionComplete }

public class AccessibilityManager : MonoBehaviour
{
    public static AccessibilityManager Instance { get; private set; }
    float nextTextScan;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static AccessibilityManager EnsureExists()
    {
        if (Instance != null) return Instance;
        AccessibilityManager existing = FindFirstObjectByType<AccessibilityManager>();
        return existing != null ? existing : new GameObject("AccessibilityManager").AddComponent<AccessibilityManager>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy() { if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded; }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { nextTextScan = 0f; }

    void Update()
    {
        if (Time.unscaledTime < nextTextScan) return;
        nextTextScan = Time.unscaledTime + 2f;
        ApplyReadableText();
    }

    public void ApplyReadableText()
    {
        RealBusSettings settings = SettingsManager.EnsureExists().Current;
        TMP_Text[] labels = FindObjectsByType<TMP_Text>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        float minimum = settings.largeText ? 34f : 28f;
        for (int i = 0; i < labels.Length; i++)
        {
            if (labels[i].fontSize < minimum) labels[i].fontSize = minimum;
            if (settings.highContrast && labels[i].color.a > 0.5f) labels[i].color = Color.white;
        }
    }

    public void Pulse(HapticCue cue)
    {
        if (!SettingsManager.EnsureExists().Current.haptics) return;
#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
#endif
    }
}
