using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager Instance { get; private set; }
    TextMeshProUGUI label;
    Coroutine activeMessage;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static SubtitleManager EnsureExists()
    {
        if (Instance != null) return Instance;
        SubtitleManager existing = FindFirstObjectByType<SubtitleManager>();
        return existing != null ? existing : new GameObject("SubtitleManager").AddComponent<SubtitleManager>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); Build();
    }

    public void Show(string speaker, string message, float duration = 3f)
    {
        if (!SettingsManager.EnsureExists().Current.subtitles || string.IsNullOrWhiteSpace(message)) return;
        if (activeMessage != null) StopCoroutine(activeMessage);
        activeMessage = StartCoroutine(Display(speaker, message, duration));
    }

    IEnumerator Display(string speaker, string message, float duration)
    {
        label.text = string.IsNullOrWhiteSpace(speaker) ? message : $"<b>{speaker}:</b> {message}";
        label.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(Mathf.Max(0.5f, duration));
        label.transform.parent.gameObject.SetActive(false); activeMessage = null;
    }

    void Build()
    {
        GameObject canvasObject = new GameObject("SubtitleCanvas", typeof(Canvas), typeof(CanvasScaler)); canvasObject.transform.SetParent(transform, false);
        Canvas canvas = canvasObject.GetComponent<Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceOverlay; canvas.sortingOrder = 110;
        CanvasScaler scaler = canvasObject.GetComponent<CanvasScaler>(); scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize; scaler.referenceResolution = new Vector2(1920f, 1080f);
        GameObject panel = new GameObject("SubtitlePanel", typeof(RectTransform), typeof(Image)); panel.transform.SetParent(canvasObject.transform, false);
        RectTransform p = panel.GetComponent<RectTransform>(); p.anchorMin = p.anchorMax = new Vector2(0.5f, 0f); p.pivot = new Vector2(0.5f, 0f); p.sizeDelta = new Vector2(1050f, 90f); p.anchoredPosition = new Vector2(0f, 72f);
        panel.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.82f);
        GameObject text = new GameObject("Text", typeof(RectTransform), typeof(TextMeshProUGUI)); text.transform.SetParent(panel.transform, false);
        RectTransform r = text.GetComponent<RectTransform>(); r.anchorMin = Vector2.zero; r.anchorMax = Vector2.one; r.offsetMin = new Vector2(24f, 10f); r.offsetMax = new Vector2(-24f, -10f);
        label = text.GetComponent<TextMeshProUGUI>(); label.fontSize = 30f; label.alignment = TextAlignmentOptions.Center; label.color = Color.white; label.font = UITheme.GetFont(UITheme.FontWeight.Medium);
        panel.SetActive(false);
    }
}
