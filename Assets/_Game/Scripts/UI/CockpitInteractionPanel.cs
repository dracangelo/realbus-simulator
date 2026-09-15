using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CockpitInteractionPanel : MonoBehaviour
{
    public static CockpitInteractionPanel Instance { get; private set; }
    Canvas canvas;
    BusController bus;
    TextMeshProUGUI doorsLabel, kneelLabel, parkLabel, retarderLabel, damageLabel;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static CockpitInteractionPanel EnsureExists()
    {
        if (Instance != null) return Instance;
        CockpitInteractionPanel existing = FindFirstObjectByType<CockpitInteractionPanel>();
        return existing != null ? existing : new GameObject("CockpitInteractionPanel").AddComponent<CockpitInteractionPanel>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start() { StartCoroutine(BindWhenReady()); }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { if (canvas != null) Destroy(canvas.gameObject); canvas = null; StartCoroutine(BindWhenReady()); }
    void OnDestroy() { if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded; }

    IEnumerator BindWhenReady()
    {
        for (int frame = 0; frame < 120; frame++)
        {
            bus = FindFirstObjectByType<BusController>();
            if (bus != null) { Build(); yield break; }
            yield return null;
        }
    }

    void Update()
    {
        bool visible = bus != null && MobileCameraController.Instance != null && MobileCameraController.Instance.CurrentPreset == BusCameraPreset.Cockpit;
        if (canvas != null && canvas.gameObject.activeSelf != visible) canvas.gameObject.SetActive(visible);
        if (!visible) return;
        PassengerManager passengers = PassengerManager.Instance;
        Set(doorsLabel, "DOORS", passengers != null && passengers.doorsOpen);
        Set(kneelLabel, "KNEEL", bus.kneelingSuspensionActive);
        Set(parkLabel, "PARK", bus.ParkingBrakeActive);
        Set(retarderLabel, "RETARDER", bus.RetarderActive);
        CosmeticDamageSystem damage = bus.GetComponent<CosmeticDamageSystem>();
        if (damageLabel != null) damageLabel.text = damage != null && damage.damageAmount > 0.01f ? $"BODY {damage.damageAmount * 100f:0}%" : "BODY OK";
    }

    void Build()
    {
        GameObject root = new GameObject("CockpitControlsCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster), typeof(SafeAreaFitter)); root.transform.SetParent(transform, false);
        canvas = root.GetComponent<Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceOverlay; canvas.sortingOrder = 82;
        CanvasScaler scaler = root.GetComponent<CanvasScaler>(); scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize; scaler.referenceResolution = new Vector2(1920f, 1080f);
        GameObject panel = new GameObject("Controls", typeof(RectTransform), typeof(Image)); panel.transform.SetParent(root.transform, false); RectTransform p = panel.GetComponent<RectTransform>(); p.anchorMin = p.anchorMax = p.pivot = new Vector2(0.5f, 0f); p.sizeDelta = new Vector2(1060f, 86f); p.anchoredPosition = new Vector2(0f, 24f); panel.GetComponent<Image>().color = new Color(0.03f, 0.05f, 0.06f, 0.9f);
        doorsLabel = Button(panel.transform, "DOORS", -405f, () => PassengerManager.Instance?.RequestDoors());
        kneelLabel = Button(panel.transform, "KNEEL", -205f, () => { if (bus.currentSpeedKmh < 1f) bus.RequestKneelingSuspension(!bus.kneelingSuspensionActive); });
        parkLabel = Button(panel.transform, "PARK", -5f, () => bus.SetParkingBrake(!bus.ParkingBrakeActive));
        retarderLabel = Button(panel.transform, "RETARDER", 195f, bus.ToggleRetarder);
        damageLabel = Label(panel.transform, "BODY OK", 410f, 170f);
        canvas.gameObject.SetActive(false);
    }

    TextMeshProUGUI Button(Transform parent, string label, float x, UnityEngine.Events.UnityAction action)
    {
        GameObject o = new GameObject(label, typeof(RectTransform), typeof(Image), typeof(Button)); o.transform.SetParent(parent, false); RectTransform r = o.GetComponent<RectTransform>(); r.sizeDelta = new Vector2(180f, 60f); r.anchoredPosition = new Vector2(x, 0f); o.GetComponent<Image>().color = UITheme.SurfaceHigh; o.GetComponent<Button>().onClick.AddListener(action); return Label(o.transform, label, 0f, 170f);
    }

    TextMeshProUGUI Label(Transform parent, string value, float x, float width)
    {
        GameObject o = new GameObject("Label", typeof(RectTransform), typeof(TextMeshProUGUI)); o.transform.SetParent(parent, false); RectTransform r = o.GetComponent<RectTransform>(); r.sizeDelta = new Vector2(width, 60f); r.anchoredPosition = new Vector2(x, 0f); TextMeshProUGUI text = o.GetComponent<TextMeshProUGUI>(); text.text = value; text.fontSize = 26f; text.alignment = TextAlignmentOptions.Center; text.color = UITheme.TextPrimary; text.font = UITheme.GetFont(UITheme.FontWeight.Bold); return text;
    }

    static void Set(TextMeshProUGUI label, string text, bool active)
    {
        if (label == null) return; label.text = active ? text + " ON" : text; label.color = active ? UITheme.Accent : UITheme.TextPrimary;
    }
}
