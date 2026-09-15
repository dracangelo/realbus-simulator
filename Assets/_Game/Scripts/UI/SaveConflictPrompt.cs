using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveConflictPrompt : MonoBehaviour
{
    GameObject panel;
    TextMeshProUGUI detailText;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap()
    {
        if (FindFirstObjectByType<SaveConflictPrompt>() == null)
            new GameObject("SaveConflictPrompt").AddComponent<SaveConflictPrompt>();
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += HandleSceneLoaded;
        SaveManager.EnsureExists().SaveConflictDetected += Show;
    }

    void Start()
    {
        if (SaveManager.Instance != null && SaveManager.Instance.HasPendingConflict)
            Show(SaveManager.Instance.PendingConflict);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
        if (SaveManager.Instance != null)
            SaveManager.Instance.SaveConflictDetected -= Show;
    }

    void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        panel = null;
        if (SaveManager.Instance != null && SaveManager.Instance.HasPendingConflict)
            Show(SaveManager.Instance.PendingConflict);
    }

    void Show(SaveConflictInfo conflict)
    {
        EnsureUi();
        if (panel == null)
        {
            Debug.LogWarning("Save conflict requires a choice, but no screen-space Canvas is available in this scene.");
            return;
        }

        detailText.text = conflict != null && !string.IsNullOrWhiteSpace(conflict.reason)
            ? conflict.reason + " Choose which progress to keep."
            : "Cloud and local progress differ. Choose which progress to keep.";
        panel.SetActive(true);
        panel.transform.SetAsLastSibling();
    }

    void EnsureUi()
    {
        if (panel != null)
            return;

        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null)
            return;

        panel = new GameObject("SaveConflictPanel", typeof(RectTransform), typeof(Image));
        panel.transform.SetParent(canvas.transform, false);
        RectTransform root = panel.GetComponent<RectTransform>();
        root.anchorMin = new Vector2(0.18f, 0.28f);
        root.anchorMax = new Vector2(0.82f, 0.72f);
        root.offsetMin = Vector2.zero;
        root.offsetMax = Vector2.zero;
        panel.GetComponent<Image>().color = UITheme.SurfaceContainer;

        TextMeshProUGUI title = CreateText("Title", panel.transform, "SAVE CONFLICT", 28f, UITheme.TextPrimary, UITheme.FontWeight.Bold);
        Anchor(title.rectTransform, new Vector2(0f, 0.72f), new Vector2(1f, 1f), new Vector2(28f, 0f), new Vector2(-28f, -12f));

        detailText = CreateText("Detail", panel.transform, string.Empty, 17f, UITheme.TextSecondary, UITheme.FontWeight.Regular);
        Anchor(detailText.rectTransform, new Vector2(0f, 0.38f), new Vector2(1f, 0.74f), new Vector2(28f, 4f), new Vector2(-28f, -4f));

        Button cloud = CreateButton("Cloud", panel.transform, "USE CLOUD SAVE", UITheme.Accent);
        Anchor(cloud.GetComponent<RectTransform>(), new Vector2(0.06f, 0.08f), new Vector2(0.47f, 0.31f), Vector2.zero, Vector2.zero);
        cloud.onClick.AddListener(() =>
        {
            SaveManager.Instance?.ResolvePendingConflictUsingCloud();
            panel.SetActive(false);
        });

        Button local = CreateButton("Local", panel.transform, "USE THIS DEVICE", UITheme.SurfaceHigh);
        Anchor(local.GetComponent<RectTransform>(), new Vector2(0.53f, 0.08f), new Vector2(0.94f, 0.31f), Vector2.zero, Vector2.zero);
        local.onClick.AddListener(() =>
        {
            SaveManager.Instance?.ResolvePendingConflictUsingLocal();
            panel.SetActive(false);
        });
    }

    static Button CreateButton(string name, Transform parent, string label, Color color)
    {
        GameObject root = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
        root.transform.SetParent(parent, false);
        root.GetComponent<Image>().color = color;
        Button button = root.GetComponent<Button>();
        TextMeshProUGUI text = CreateText("Label", root.transform, label, 15f, UITheme.TextPrimary, UITheme.FontWeight.Bold);
        Anchor(text.rectTransform, Vector2.zero, Vector2.one, new Vector2(8f, 5f), new Vector2(-8f, -5f));
        text.alignment = TextAlignmentOptions.Center;
        return button;
    }

    static TextMeshProUGUI CreateText(string name, Transform parent, string value, float size, Color color, UITheme.FontWeight weight)
    {
        TextMeshProUGUI text = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI)).GetComponent<TextMeshProUGUI>();
        text.transform.SetParent(parent, false);
        text.text = value;
        text.fontSize = size;
        text.color = color;
        text.font = UITheme.GetFont(weight);
        text.textWrappingMode = TextWrappingModes.Normal;
        return text;
    }

    static void Anchor(RectTransform rect, Vector2 min, Vector2 max, Vector2 offsetMin, Vector2 offsetMax)
    {
        rect.anchorMin = min;
        rect.anchorMax = max;
        rect.offsetMin = offsetMin;
        rect.offsetMax = offsetMax;
    }
}
