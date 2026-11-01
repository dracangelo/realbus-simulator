using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// The player-facing refuelling and charging panel.
///
/// It appears only while the bus is inside a station trigger, stays out of the
/// bottom control corners, and asks the station to open a paid session. The
/// station owns the money and energy transaction; this panel only chooses a
/// target and shows progress, so closing the UI can never leave a second
/// transaction running.
/// </summary>
public class StationInteractionPanel : MonoBehaviour
{
    public static StationInteractionPanel Instance { get; private set; }

    static readonly float[] TargetFractions = { .25f, .5f, .75f, 1f };

    Canvas canvas;
    BusController bus;
    GasStationServicePoint station;
    float scanTimer;
    float refreshTimer;
    bool dismissed;
    bool wasActive;
    float noticeUntil;
    string noticeKey;
    int targetIndex = TargetFractions.Length - 1;

    TMP_Text titleLabel, modeLabel, levelLabel, progressLabel, estimateLabel, statusLabel, targetHeading;
    TMP_Text startLabel, stopLabel;
    Button startButton, stopButton;
    Image fill;
    readonly Button[] targetButtons = new Button[TargetFractions.Length];
    readonly TMP_Text[] targetLabels = new TMP_Text[TargetFractions.Length];

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static StationInteractionPanel EnsureExists()
    {
        if (Instance != null) return Instance;
        StationInteractionPanel existing = FindAnyObjectByType<StationInteractionPanel>();
        return existing != null ? existing : new GameObject("StationInteractionPanel").AddComponent<StationInteractionPanel>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start() { StartCoroutine(BindWhenReady()); }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (canvas != null) Destroy(canvas.gameObject);
        canvas = null;
        station = null;
        dismissed = false;
        wasActive = false;
        noticeUntil = 0f;
        noticeKey = null;
        StartCoroutine(BindWhenReady());
    }

    void OnDestroy()
    {
        if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    IEnumerator BindWhenReady()
    {
        for (int frame = 0; frame < 180; frame++)
        {
            bus = FindAnyObjectByType<BusController>();
            if (bus != null) { Build(); yield break; }
            yield return null;
        }
    }

    void Update()
    {
        if (bus == null)
        {
            bus = FindAnyObjectByType<BusController>();
            if (bus == null) return;
            if (canvas == null) Build();
        }

        scanTimer -= Time.unscaledDeltaTime;
        if (scanTimer <= 0f || station == null)
        {
            scanTimer = .25f;
            var found = FindNearbyStation();
            if (found != station)
            {
                station = found;
                dismissed = false;
                wasActive = false;
                noticeUntil = 0f;
                noticeKey = null;
                targetIndex = TargetFractions.Length - 1;
            }
        }

        bool visible = station != null && !dismissed && station.IsBusInside(bus);
        if (canvas != null && canvas.gameObject.activeSelf != visible)
            canvas.gameObject.SetActive(visible);
        if (!visible) return;

        refreshTimer -= Time.unscaledDeltaTime;
        if (refreshTimer > 0f) return;
        refreshTimer = .1f;
        Refresh();
    }

    GasStationServicePoint FindNearbyStation()
    {
        if (bus == null) return null;
        var points = FindObjectsByType<GasStationServicePoint>(FindObjectsInactive.Exclude);
        GasStationServicePoint best = null;
        float bestDistance = float.MaxValue;
        for (int i = 0; i < points.Length; i++)
        {
            var point = points[i];
            if (point == null || !point.IsBusInside(bus)) continue;
            float distance = Vector3.SqrMagnitude(point.transform.position - bus.transform.position);
            if (distance < bestDistance)
            {
                best = point;
                bestDistance = distance;
            }
        }
        return best;
    }

    void Build()
    {
        if (canvas != null) return;

        var root = new GameObject("StationInteractionCanvas", typeof(Canvas), typeof(CanvasScaler),
            typeof(GraphicRaycaster), typeof(SafeAreaFitter));
        root.transform.SetParent(transform, false);
        canvas = root.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 84;

        var scaler = root.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);

        var panelObject = new GameObject("StationPanel", typeof(RectTransform), typeof(Image));
        panelObject.transform.SetParent(root.transform, false);
        var panel = panelObject.GetComponent<RectTransform>();
        // Right-hand side, lifted clear of the pedal column. The pedals occupy the
        // bottom-right corner up to roughly 250 px of the 1080 reference height, so
        // a centre-anchored panel would sit on top of the accelerator on wide
        // landscape phones.
        panel.anchorMin = panel.anchorMax = panel.pivot = new Vector2(1f, .5f);
        panel.anchoredPosition = new Vector2(-20f, 104f);
        panel.sizeDelta = new Vector2(384f, 470f);

        var background = panelObject.GetComponent<Image>();
        background.color = new Color(UITheme.Surface.r, UITheme.Surface.g, UITheme.Surface.b, .96f);
        background.raycastTarget = true;
        RuntimeUiShapes.Rounded(background);
        RuntimeUiShapes.SoftShadow(background, .6f, 10f);
        var outline = panelObject.AddComponent<Outline>();
        outline.effectColor = new Color(1f, 1f, 1f, .16f);
        outline.effectDistance = new Vector2(2f, -2f);

        titleLabel = CreateText(panel, "Title", "", 23f, UITheme.Accent,
            new Vector2(18f, -12f), new Vector2(252f, 32f), TextAlignmentOptions.Left);
        titleLabel.fontStyle = FontStyles.Bold;

        var close = CreateButton(panel, "Close", "✕", new Vector2(326f, -10f), new Vector2(40f, 40f),
            ClosePressed);
        SetButtonFont(close, 21f, UITheme.TextSecondary);

        modeLabel = CreateText(panel, "Mode", "", 21f, UITheme.BusAmber,
            new Vector2(18f, -50f), new Vector2(348f, 26f), TextAlignmentOptions.Left);
        modeLabel.fontStyle = FontStyles.Bold;

        levelLabel = CreateText(panel, "Level", "", 20f, UITheme.TextPrimary,
            new Vector2(18f, -80f), new Vector2(348f, 28f), TextAlignmentOptions.Left);

        var barObject = new GameObject("ProgressBar", typeof(RectTransform), typeof(Image));
        barObject.transform.SetParent(panel, false);
        var bar = barObject.GetComponent<RectTransform>();
        bar.anchorMin = bar.anchorMax = new Vector2(0f, 1f);
        bar.pivot = new Vector2(0f, 1f);
        bar.anchoredPosition = new Vector2(18f, -116f);
        bar.sizeDelta = new Vector2(348f, 14f);
        var barImage = barObject.GetComponent<Image>();
        barImage.color = UITheme.SurfaceHigh;
        barImage.raycastTarget = false;
        RuntimeUiShapes.Rounded(barImage);

        var fillObject = new GameObject("Fill", typeof(RectTransform), typeof(Image));
        fillObject.transform.SetParent(bar, false);
        var fillRect = fillObject.GetComponent<RectTransform>();
        fillRect.anchorMin = Vector2.zero;
        fillRect.anchorMax = new Vector2(0f, 1f);
        fillRect.offsetMin = fillRect.offsetMax = Vector2.zero;
        fill = fillObject.GetComponent<Image>();
        fill.color = UITheme.Accent;
        fill.raycastTarget = false;
        RuntimeUiShapes.Rounded(fill);

        progressLabel = CreateText(panel, "Progress", "", 17f, UITheme.TextSecondary,
            new Vector2(18f, -136f), new Vector2(348f, 22f), TextAlignmentOptions.Left);

        targetHeading = CreateText(panel, "TargetHeading", "", 17f, UITheme.TextSecondary,
            new Vector2(18f, -166f), new Vector2(348f, 22f), TextAlignmentOptions.Left);
        targetHeading.fontStyle = FontStyles.Bold;

        for (int i = 0; i < TargetFractions.Length; i++)
        {
            int index = i;
            var button = CreateButton(panel, "Target" + i, TargetLabel(i),
                new Vector2(18f + i * 87f, -192f), new Vector2(78f, 48f), () => SelectTarget(index));
            targetButtons[i] = button;
            targetLabels[i] = button.GetComponentInChildren<TMP_Text>(true);
        }

        estimateLabel = CreateText(panel, "Estimate", "", 18f, UITheme.TextPrimary,
            new Vector2(18f, -248f), new Vector2(348f, 44f), TextAlignmentOptions.Left);

        statusLabel = CreateText(panel, "Status", "", 18f, UITheme.TextSecondary,
            new Vector2(18f, -294f), new Vector2(348f, 54f), TextAlignmentOptions.Left);
        statusLabel.fontStyle = FontStyles.Bold;

        startButton = CreateButton(panel, "Start", "", new Vector2(18f, -356f), new Vector2(348f, 56f),
            StartPressed);
        startLabel = startButton.GetComponentInChildren<TMP_Text>(true);
        SetButtonFont(startButton, 22f, UITheme.Background);

        stopButton = CreateButton(panel, "Stop", "", new Vector2(18f, -356f), new Vector2(348f, 56f),
            StopPressed);
        stopLabel = stopButton.GetComponentInChildren<TMP_Text>(true);
        SetButtonFont(stopButton, 22f, UITheme.TextPrimary);
        stopButton.GetComponent<Image>().color = UITheme.ErrorContainer;

        canvas.gameObject.SetActive(false);
    }

    void SelectTarget(int index)
    {
        if (station != null && station.SessionActive) return;
        targetIndex = Mathf.Clamp(index, 0, TargetFractions.Length - 1);
        Refresh();
    }

    void StartPressed()
    {
        if (station == null) return;
        float target = GasStationServicePoint.TargetAmountFor(TargetFractions[targetIndex], station.MissingAmount);
        if (!station.BeginSession(target, out string failureKey))
        {
            if (statusLabel != null) statusLabel.text = Localization.Get(failureKey);
            return;
        }
        Refresh();
    }

    void StopPressed()
    {
        if (station == null) return;
        station.StopSession(cancelled: true);
        Refresh();
    }

    void ClosePressed()
    {
        // Closing the panel must not leave a paid session running with no way to
        // see or stop it.
        if (station != null && station.SessionActive) station.StopSession(cancelled: true);
        dismissed = true;
        if (canvas != null) canvas.gameObject.SetActive(false);
    }

    void Refresh()
    {
        if (station == null || canvas == null) return;

        bool charging = station.Mode == GasStationServicePoint.ServiceMode.Charge;
        bool active = station.SessionActive;
        string unit = station.UnitLabel;

        // Keep a short completion/stop notice visible after the station closes a
        // session, so the player is told why the pump stopped.
        if (wasActive && !active)
        {
            noticeUntil = Time.unscaledTime + 4f;
            noticeKey = station.LastSessionCompleted ? "station.complete"
                : station.LastSessionAmount > .01f ? "station.stopped"
                : null;
        }
        wasActive = active;

        titleLabel.text = Localization.Get("station.title");
        modeLabel.text = Localization.Get(charging ? "station.charge" : "station.fuel");
        modeLabel.color = charging ? UITheme.Secondary : UITheme.BusAmber;

        levelLabel.text = $"{Localization.Get("station.level")}  {station.CurrentPercent:0}%   " +
                          $"{station.CurrentAmount:0.#} / {station.CapacityAmount:0.#} {unit}";

        float target = active
            ? station.TargetAmount
            : GasStationServicePoint.TargetAmountFor(TargetFractions[targetIndex], station.MissingAmount);
        float cost = station.EstimatedCostFor(target);

        float progress = active ? station.Progress01 : 0f;
        var fillRect = fill != null ? fill.rectTransform : null;
        if (fillRect != null)
            fillRect.anchorMax = new Vector2(progress, 1f);

        progressLabel.text = active
            ? $"{Localization.Get("station.progress")}  {progress * 100f:0}%   " +
              $"({station.SessionAmount:0.#} / {station.TargetAmount:0.#} {unit})"
            : $"{Localization.Get("station.target")}  {target:0.#} {unit}";

        targetHeading.text = Localization.Get("station.target");
        for (int i = 0; i < targetButtons.Length; i++)
        {
            if (targetButtons[i] == null) continue;
            targetButtons[i].interactable = !active;
            bool selected = i == targetIndex;
            var image = targetButtons[i].GetComponent<Image>();
            if (image != null)
                image.color = selected ? UITheme.Accent : UITheme.SurfaceHigh;
            if (targetLabels[i] != null)
            {
                targetLabels[i].text = TargetLabel(i);
                targetLabels[i].color = selected ? UITheme.Background : UITheme.TextPrimary;
            }
        }

        estimateLabel.text = active
            ? $"{Localization.Get("station.estimated")}  KES {station.SessionCostKES:N0}"
            : $"{Localization.Get("station.add")} {target:0.#} {unit}\n" +
              $"{Localization.Get("station.estimated")}  KES {cost:N0}";

        string block = station.GetStartBlockReasonKey();
        string statusKey = active ? "station.progress" : block ?? "station.ready";
        if (!active && station.IsFull) statusKey = "station.fullTank";
        if (!active && noticeKey != null && Time.unscaledTime < noticeUntil)
            statusKey = noticeKey;
        statusLabel.text = Localization.Get(statusKey);
        statusLabel.color = active ? UITheme.BusAmber
            : noticeKey != null && Time.unscaledTime < noticeUntil
                ? (station.LastSessionCompleted ? UITheme.Success : UITheme.Error)
            : block == null ? UITheme.Success
            : UITheme.Error;

        bool canStart = !active && block == null;
        if (startButton != null)
        {
            startButton.gameObject.SetActive(!active);
            startButton.interactable = canStart;
            var image = startButton.GetComponent<Image>();
            if (image != null)
                image.color = canStart ? UITheme.Accent : UITheme.SurfaceHigh;
            if (startLabel != null)
            {
                startLabel.text = Localization.Get(charging ? "station.startCharge" : "station.startFuel");
                startLabel.color = canStart ? UITheme.Background : UITheme.TextMuted;
            }
        }

        if (stopButton != null)
        {
            stopButton.gameObject.SetActive(active);
            if (stopLabel != null) stopLabel.text = Localization.Get("station.stop");
        }
    }

    static string TargetLabel(int index)
        => index == TargetFractions.Length - 1 ? Localization.Get("station.full")
            : $"{Mathf.RoundToInt(TargetFractions[index] * 100f)}%";

    static TMP_Text CreateText(Transform parent, string name, string value, float size, Color color,
        Vector2 position, Vector2 dimensions, TextAlignmentOptions alignment)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI));
        go.transform.SetParent(parent, false);
        var rect = go.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = new Vector2(0f, 1f);
        rect.pivot = new Vector2(0f, 1f);
        rect.anchoredPosition = position;
        rect.sizeDelta = dimensions;

        var text = go.GetComponent<TextMeshProUGUI>();
        text.text = value;
        text.fontSize = size;
        text.color = color;
        text.alignment = alignment;
        text.textWrappingMode = TextWrappingModes.Normal;
        text.overflowMode = TextOverflowModes.Ellipsis;
        text.raycastTarget = false;
        var font = UITheme.GetFont(UITheme.FontWeight.Medium);
        if (font != null) text.font = font;
        return text;
    }

    static Button CreateButton(Transform parent, string name, string label, Vector2 position,
        Vector2 dimensions, UnityAction action)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
        go.transform.SetParent(parent, false);
        var rect = go.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = new Vector2(0f, 1f);
        rect.pivot = new Vector2(0f, 1f);
        rect.anchoredPosition = position;
        rect.sizeDelta = dimensions;

        var image = go.GetComponent<Image>();
        image.color = UITheme.SurfaceHigh;
        RuntimeUiShapes.Rounded(image);

        var button = go.GetComponent<Button>();
        button.onClick.AddListener(action);
        var colors = button.colors;
        colors.normalColor = Color.white;
        colors.highlightedColor = new Color(1.08f, 1.08f, 1.08f, 1f);
        colors.pressedColor = new Color(.85f, .85f, .85f, 1f);
        colors.disabledColor = new Color(.55f, .55f, .55f, .6f);
        colors.fadeDuration = .08f;
        button.colors = colors;

        var text = CreateText(go.transform, "Label", label, 20f, UITheme.TextPrimary,
            Vector2.zero, dimensions, TextAlignmentOptions.Center);
        text.rectTransform.anchorMin = Vector2.zero;
        text.rectTransform.anchorMax = Vector2.one;
        text.rectTransform.offsetMin = text.rectTransform.offsetMax = Vector2.zero;
        text.fontStyle = FontStyles.Bold;
        return button;
    }

    static void SetButtonFont(Button button, float size, Color color)
    {
        if (button == null) return;
        var text = button.GetComponentInChildren<TMP_Text>(true);
        if (text == null) return;
        text.fontSize = size;
        text.color = color;
    }
}
