using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum HapticCue { Docking, Collision, MissionComplete }

public class AccessibilityManager : MonoBehaviour
{
    public static AccessibilityManager Instance { get; private set; }
    float nextTextScan;
    readonly Dictionary<TMP_Text, TextBaseline> textBaselines = new Dictionary<TMP_Text, TextBaseline>();

    sealed class TextBaseline
    {
        public float fontSize;
        public Color color;
    }

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
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        textBaselines.Clear();
        nextTextScan = 0f;
    }

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
        for (int i = 0; i < labels.Length; i++)
        {
            TMP_Text label = labels[i];
            if (!label) continue;

            if (!textBaselines.TryGetValue(label, out TextBaseline baseline))
            {
                baseline = new TextBaseline { fontSize = label.fontSize, color = label.color };
                textBaselines[label] = baseline;
            }

            // Preserve the UI's visual hierarchy. Large Text scales each role
            // proportionally instead of forcing captions, badges and titles to
            // one size (which caused clipping throughout the selection screens).
            label.fontSize = settings.largeText ? baseline.fontSize * 1.18f : baseline.fontSize;
            label.color = settings.highContrast && baseline.color.a > 0.5f
                ? Color.white
                : baseline.color;
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
