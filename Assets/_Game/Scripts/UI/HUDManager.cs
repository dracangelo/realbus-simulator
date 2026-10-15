using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    Canvas canvas;
    GameObject safeRoot;
    TextMeshProUGUI speedText, gearText, nextStopText, etaText, guidanceText, scheduleText, passengerText, fuelText, temperatureText;
    Image speedNeedle, fuelNeedle, temperatureNeedle, progressFill, schedulePill;
    RuntimeMinimapGraphic minimap;
    BusController bus;
    EngineTemperatureSystem temperature;
    FuelSystem fuel;
    BatterySystem battery;
    MissionManager mission;
    PassengerManager passengers;
    int renderedStopCount = -1;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static HUDManager EnsureExists()
    {
        if (Instance != null) return Instance;
        HUDManager existing = FindAnyObjectByType<HUDManager>();
        if (existing != null) return existing;
        return new GameObject("HUDManager").AddComponent<HUDManager>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start() { StartCoroutine(BindWhenReady()); }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { StartCoroutine(BindWhenReady()); }
    void OnDestroy() { if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded; }

    IEnumerator BindWhenReady()
    {
        yield return null;
        bus = FindAnyObjectByType<BusController>();
        if (bus == null) { SetVisible(false); yield break; }
        mission = MissionManager.Instance;
        passengers = PassengerManager.Instance != null ? PassengerManager.Instance : FindAnyObjectByType<PassengerManager>();
        fuel = bus.GetComponent<FuelSystem>();
        battery = bus.GetComponent<BatterySystem>();
        temperature = bus.GetComponent<EngineTemperatureSystem>();
        if (temperature == null) temperature = bus.gameObject.AddComponent<EngineTemperatureSystem>();
        if (canvas == null) BuildHUD();
        if (minimap != null) { minimap.bus = bus; minimap.mission = mission; }
        renderedStopCount = -1;
        SetVisible(true);
    }

    void Update()
    {
        if (canvas == null || bus == null) return;
        mission = MissionManager.Instance;
        bool drivingState = mission == null || (mission.missionState != MissionState.Completed && mission.missionState != MissionState.Failed);
        if (canvas.gameObject.activeSelf != drivingState) canvas.gameObject.SetActive(drivingState);
        if (!drivingState) return;
        passengers = PassengerManager.Instance;
        float speed = Mathf.Clamp(bus.currentSpeedKmh, 0f, 120f);
        speedText.text = $"{speed:0}\n<size=20>KM/H</size>";
        speedNeedle.rectTransform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(130f, -130f, speed / 120f));
        gearText.text = FormatGear(bus.transmissionData != null ? bus.transmissionData.currentGear : 0);

        float energy = battery != null ? battery.CurrentChargePercent : fuel != null ? fuel.CurrentFuelPercent : FreeDriveSession.Instance != null ? FreeDriveSession.Instance.fuelLevel : 100f;
        fuelText.text = battery != null ? $"BAT {energy:0}%" : $"FUEL {energy:0}%";
        fuelNeedle.rectTransform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(130f, -130f, energy / 100f));
        float temp = temperature != null ? temperature.currentTemperatureC : 80f;
        if (temperature != null && WeatherSystem.Instance != null) temperature.ambientTemperatureC = WeatherSystem.Instance.temperature;
        temperatureText.text = $"TEMP {temp:0}°C";
        temperatureText.color = temperature != null && temperature.WarningActive ? UITheme.Error : UITheme.TextPrimary;
        temperatureNeedle.color = temperatureText.color;
        temperatureNeedle.rectTransform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(130f, -130f, Mathf.InverseLerp(60f, 120f, temp)));

        int pax = passengers != null ? passengers.currentPassengers : 0;
        int capacity = passengers != null ? passengers.GetMaxCapacity() : 0;
        passengerText.text = $"{PassengerFeedbackSystem.MoodIcon(passengers != null ? passengers.averageSatisfaction : 1f)} PAX {pax}/{capacity}";
        UpdateRouteReadout(speed);

        if (UnityEngine.InputSystem.Keyboard.current != null && UnityEngine.InputSystem.Keyboard.current.mKey.wasPressedThisFrame)
            ToggleMinimap();
    }

    void UpdateRouteReadout(float speed)
    {
        bool selectedMissionStarting = mission != null &&
            (mission.routeActive || mission.CurrentCountdownValue > 0 || mission.missionState == MissionState.Briefing) &&
            GameState.Instance?.selectedRoute != null;
        if (mission == null || (!mission.routeActive && !selectedMissionStarting) || mission.currentRoute == null || mission.currentRoute.stops == null)
        {
            nextStopText.text = Localization.Get("hud.freeDrive");
            etaText.text = Localization.Get("hud.explore");
            guidanceText.text = "";
            scheduleText.text = Localization.Get("hud.noSchedule");
            schedulePill.color = UITheme.WithAlpha(UITheme.Secondary, 0.85f);
            progressFill.fillAmount = 0f;
            return;
        }

        int count = mission.currentRoute.stops.Length;
        int index = Mathf.Clamp(mission.currentStopIndex, 0, Mathf.Max(0, count - 1));
        string navigation = GetGuidanceInstruction();
        if (string.IsNullOrWhiteSpace(navigation)) navigation = "↑  CONTINUE STRAIGHT";
        nextStopText.text = $"{navigation}  •  {mission.distanceToNextStop:0} m";
        float etaSeconds = mission.distanceToNextStop / Mathf.Max(4.2f, speed / 3.6f);
        etaText.text = $"{Localization.Get("hud.routeNav")}  •  ETA {FormatEta(etaSeconds)}";
        guidanceText.text = $"{navigation}\n<size=14>{mission.currentRoute.stops[index].stopName}</size>";
        progressFill.fillAmount = count > 1 ? Mathf.Clamp01(index / (float)(count - 1)) : 1f;

        PunctualityStatus status = ScheduleManager.Instance != null ? ScheduleManager.Instance.GetLiveStatus(index) : PunctualityStatus.OnTime;
        scheduleText.text = StatusIcon(status) + " " + StatusLabel(status).ToUpperInvariant();
        schedulePill.color = UITheme.WithAlpha(StatusColor(status), 0.9f);
        if (renderedStopCount != count) { BuildStopMarkers(count); renderedStopCount = count; }
    }

    public void ToggleMinimap()
    {
        if (minimap != null) minimap.gameObject.SetActive(!minimap.gameObject.activeSelf);
    }

    public static string FormatGear(int gear) { return gear < 0 ? "R" : gear == 0 ? "D" : (gear + 1).ToString(); }
    public static string FormatEta(float seconds) { return seconds < 60f ? "<1 min" : Mathf.CeilToInt(seconds / 60f) + " min"; }
    public static string StatusIcon(PunctualityStatus status) { return status == PunctualityStatus.OnTime ? "✓" : status == PunctualityStatus.Early ? "↑" : status == PunctualityStatus.Late ? "!" : "!!"; }
    static string StatusLabel(PunctualityStatus status) { return status == PunctualityStatus.OnTime ? "On time" : status == PunctualityStatus.Early ? "Early" : status == PunctualityStatus.Late ? "Late" : "Severely late"; }
    static Color StatusColor(PunctualityStatus status) { return status == PunctualityStatus.OnTime ? UITheme.Success : status == PunctualityStatus.Early ? UITheme.Secondary : status == PunctualityStatus.Late ? UITheme.TertiaryDim : UITheme.Error; }

    void BuildHUD()
    {
        GameObject c = new GameObject("Phase7HUD", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        c.transform.SetParent(transform, false);
        canvas = c.GetComponent<Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceOverlay; canvas.sortingOrder = 80;
        CanvasScaler scaler = c.GetComponent<CanvasScaler>(); scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize; scaler.referenceResolution = new Vector2(1920f, 1080f); scaler.matchWidthOrHeight = 0.5f;
        safeRoot = new GameObject("SafeArea", typeof(RectTransform), typeof(SafeAreaFitter)); safeRoot.transform.SetParent(c.transform, false);

        RectTransform mapPanel = Panel("NavigationCard", safeRoot.transform, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(460f, 230f), new Vector2(20f, -20f));
        // Amber accent top-border strip on the navigation card
        Image navBorder = Box("NavBorder", mapPanel, Vector2.zero, new Vector2(0f, 4f), UITheme.BusAmber);
        navBorder.rectTransform.anchorMin = new Vector2(0f, 1f); navBorder.rectTransform.anchorMax = new Vector2(1f, 1f); navBorder.rectTransform.pivot = new Vector2(0.5f, 1f); navBorder.rectTransform.sizeDelta = new Vector2(0f, 4f);
        GameObject mini = new GameObject("Minimap", typeof(RectTransform), typeof(RuntimeMinimapGraphic)); mini.transform.SetParent(mapPanel, false);
        RectTransform miniRt = mini.GetComponent<RectTransform>(); Stretch(miniRt); miniRt.offsetMin = new Vector2(8f, 8f); miniRt.offsetMax = new Vector2(-8f, -8f);
        minimap = mini.GetComponent<RuntimeMinimapGraphic>(); minimap.color = new Color(0.03f, 0.048f, 0.055f, 0.95f); minimap.raycastTarget = false;
        Image instructionPill = Box("InstructionPill", mapPanel, new Vector2(-10f, -150f), new Vector2(240f, 60f), new Color(.015f, .02f, .022f, .9f));
        instructionPill.rectTransform.anchorMin = instructionPill.rectTransform.anchorMax = new Vector2(1f, 1f); instructionPill.rectTransform.pivot = new Vector2(1f, 1f);
        RuntimeUiShapes.Rounded(instructionPill);
        guidanceText = Text("Guidance", instructionPill.transform, "↑  CONTINUE", 18, TextAlignmentOptions.Center, Vector2.zero, new Vector2(224f, 50f));

        RectTransform routePanel = Panel("NextStopBanner", safeRoot.transform, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(620f, 118f), new Vector2(0f, -20f));
        nextStopText = Text("Navigation", routePanel, "↑  ROUTE NAVIGATION", 24, TextAlignmentOptions.Center, new Vector2(0f, 20f), new Vector2(580f, 36f));
        etaText = Text("ETA", routePanel, "Follow the highlighted road", 15, TextAlignmentOptions.Center, new Vector2(0f, -8f), new Vector2(560f, 26f));
        etaText.color = UITheme.TextSecondary;
        schedulePill = Box("SchedulePill", routePanel, new Vector2(0f, -44f), new Vector2(172f, 26f), UITheme.Secondary);
        RuntimeUiShapes.Rounded(schedulePill);
        scheduleText = Text("Status", schedulePill.rectTransform, "NO SCHEDULE", 12, TextAlignmentOptions.Center, Vector2.zero, schedulePill.rectTransform.sizeDelta);
        Image track = Box("ProgressTrack", routePanel, new Vector2(0f, -57f), new Vector2(540f, 7f), UITheme.SurfaceBright);
        RuntimeUiShapes.Rounded(track);
        progressFill = Box("ProgressFill", track.rectTransform, Vector2.zero, Vector2.zero, UITheme.Accent);
        progressFill.type = Image.Type.Filled; progressFill.fillMethod = Image.FillMethod.Horizontal; progressFill.fillOrigin = 0;
        RuntimeUiShapes.Rounded(progressFill);
        Stretch(progressFill.rectTransform);

        // Speed dial — shifted left so it doesn't overlap the ACCEL button (anchor 1,0 at -28px)
        RectTransform speedPanel = CirclePanel("SpeedDial", safeRoot.transform, new Vector2(1f, 1f), new Vector2(1f, 1f), 184f, new Vector2(-316f, -24f), UITheme.TertiaryDim);
        Image speedFace = Box("SpeedFace", speedPanel, Vector2.zero, new Vector2(160f, 160f), new Color(.03f, .038f, .04f, .98f));
        RuntimeUiShapes.Circle(speedFace);
        speedText = Text("Speed", speedPanel, "0\n<size=18>km/h</size>", 54, TextAlignmentOptions.Center, new Vector2(0f, 10f), new Vector2(152f, 108f));
        speedNeedle = Needle("SpeedNeedle", speedPanel, 70f, UITheme.TertiaryDim);
        gearText = Text("Gear", speedPanel, "D", 26, TextAlignmentOptions.Center, new Vector2(0f, -60f), new Vector2(68f, 34f));
        gearText.color = UITheme.Accent;

        // Vehicle Vitals — positioned below speed dial, also shifted left to stay clear of buttons
        RectTransform energyPanel = Panel("VehicleVitals", safeRoot.transform, new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(248f, 132f), new Vector2(-316f, -220f));
        fuelNeedle = Needle("FuelNeedle", energyPanel, 34f, UITheme.TertiaryDim); fuelNeedle.gameObject.SetActive(false);
        temperatureNeedle = Needle("TemperatureNeedle", energyPanel, 34f, UITheme.Success); temperatureNeedle.gameObject.SetActive(false);
        fuelText = Text("Fuel", energyPanel, "▰  FUEL 100%", 17, TextAlignmentOptions.Left, new Vector2(14f, 38f), new Vector2(216f, 30f));
        fuelText.rectTransform.anchorMin = fuelText.rectTransform.anchorMax = new Vector2(0f, .5f); fuelText.rectTransform.pivot = new Vector2(0f, .5f);
        passengerText = Text("Passengers", energyPanel, "●  PAX 0/0", 17, TextAlignmentOptions.Left, new Vector2(14f, 2f), new Vector2(216f, 30f));
        passengerText.rectTransform.anchorMin = passengerText.rectTransform.anchorMax = new Vector2(0f, .5f); passengerText.rectTransform.pivot = new Vector2(0f, .5f);
        temperatureText = Text("Temperature", energyPanel, "TEMP 80°C", 13, TextAlignmentOptions.Left, new Vector2(14f, -34f), new Vector2(216f, 26f));
        temperatureText.rectTransform.anchorMin = temperatureText.rectTransform.anchorMax = new Vector2(0f, .5f); temperatureText.rectTransform.pivot = new Vector2(0f, .5f);
    }

    void BuildStopMarkers(int count)
    {
        Transform track = progressFill.transform.parent;
        for (int i = track.childCount - 1; i >= 0; i--) if (track.GetChild(i) != progressFill.transform) Destroy(track.GetChild(i).gameObject);
        for (int i = 0; i < count; i++)
        {
            Image marker = Box("Stop_" + i, track, Vector2.zero, new Vector2(16f, 16f), UITheme.TextPrimary);
            marker.rectTransform.anchorMin = marker.rectTransform.anchorMax = new Vector2(count <= 1 ? 0.5f : i / (float)(count - 1), 0.5f);
        }
    }

    string GetGuidanceInstruction()
    {
        if (mission == null || bus == null || mission.ActiveGuidancePathPoints == null || mission.ActiveGuidancePathPoints.Count < 2) return "";
        var path = mission.ActiveGuidancePathPoints; int nearest = 0; float best = float.MaxValue;
        for (int i = 0; i < path.Count; i++) { float sqr = (path[i] - bus.transform.position).sqrMagnitude; if (sqr < best) { best = sqr; nearest = i; } }
        int ahead = Mathf.Min(path.Count - 1, nearest + 2); Vector3 direction = path[ahead] - bus.transform.position; direction.y = 0f;
        if (direction.sqrMagnitude < 1f) return "CONTINUE";
        float angle = Vector3.SignedAngle(bus.transform.forward, direction.normalized, Vector3.up);
        string instruction = angle > 28f ? "↱  TURN RIGHT" : angle < -28f ? "↰  TURN LEFT" : angle > 9f ? "↗  KEEP RIGHT" : angle < -9f ? "↖  KEEP LEFT" : "↑  CONTINUE STRAIGHT";
        if (mission.HasActiveDiversion) instruction = mission.CurrentDiversionLabel.ToUpperInvariant() + " • " + instruction;
        return instruction;
    }

    RectTransform Panel(string name, Transform parent, Vector2 anchor, Vector2 pivot, Vector2 size, Vector2 position)
    {
        Image image = Box(name, parent, position, size, new Color(0.04f, 0.06f, 0.07f, 0.88f));
        image.rectTransform.anchorMin = image.rectTransform.anchorMax = anchor; image.rectTransform.pivot = pivot;
        Outline outline = image.gameObject.AddComponent<Outline>(); outline.effectColor = new Color(1f, 1f, 1f, .20f); outline.effectDistance = new Vector2(2f, -2f);
        RuntimeUiShapes.SoftShadow(image);
        return image.rectTransform;
    }

    RectTransform CirclePanel(string name, Transform parent, Vector2 anchor, Vector2 pivot, float diameter, Vector2 position, Color tint)
    {
        Image image = Box(name, parent, position, new Vector2(diameter, diameter), tint);
        image.rectTransform.anchorMin = image.rectTransform.anchorMax = anchor; image.rectTransform.pivot = pivot;
        RuntimeUiShapes.Circle(image); RuntimeUiShapes.SoftShadow(image, .5f, 9f);
        return image.rectTransform;
    }

    Image Box(string name, Transform parent, Vector2 position, Vector2 size, Color tint)
    {
        GameObject o = new GameObject(name, typeof(RectTransform), typeof(Image)); o.transform.SetParent(parent, false);
        RectTransform r = o.GetComponent<RectTransform>(); r.sizeDelta = size; r.anchoredPosition = position;
        Image image = o.GetComponent<Image>(); image.color = tint; image.raycastTarget = false; RuntimeUiShapes.Rounded(image); return image;
    }

    Image Needle(string name, Transform parent, float length, Color tint)
    {
        Image needle = Box(name, parent, new Vector2(0f, 4f), new Vector2(5f, length), tint);
        needle.rectTransform.pivot = new Vector2(0.5f, 0.08f); return needle;
    }

    TextMeshProUGUI Text(string name, Transform parent, string value, float size, TextAlignmentOptions align, Vector2 position, Vector2 dimensions)
    {
        GameObject o = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI)); o.transform.SetParent(parent, false);
        RectTransform r = o.GetComponent<RectTransform>(); r.sizeDelta = dimensions; r.anchoredPosition = position;
        TextMeshProUGUI t = o.GetComponent<TextMeshProUGUI>(); t.text = value; t.fontSize = Mathf.Max(12f, size); t.alignment = align; t.color = UITheme.TextPrimary; t.font = UITheme.GetFont(size >= 26 ? UITheme.FontWeight.Bold : UITheme.FontWeight.Medium); t.raycastTarget = false;
        return t;
    }

    static void Stretch(RectTransform r) { r.anchorMin = Vector2.zero; r.anchorMax = Vector2.one; r.offsetMin = r.offsetMax = Vector2.zero; }
    void SetVisible(bool visible) { if (canvas != null) canvas.gameObject.SetActive(visible); }
}
