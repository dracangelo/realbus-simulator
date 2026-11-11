using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// The gameplay HUD, built entirely at runtime so it survives scene reloads and
/// follows the driver through every city.
///
/// The look is deliberately "transit cockpit": a dark slate glass surface, an
/// amber LED destination sign, a route roundel and an instrument-cluster speed
/// dial, with a live route minimap whose line is the road still to drive. It is
/// meant to read like the cab of a real bus rather than a generic racing overlay.
/// </summary>
public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    public bool IsBound => bus != null;
    public bool IsVisible => canvas != null && canvas.gameObject.activeSelf;

    /// <summary>
    /// True when the turn banner is on screen with a live instruction. The smoke run
    /// asserts this for the whole route, not just at the start.
    /// </summary>
    public bool NavigatorVisible => IsVisible && guidanceText != null &&
        guidanceText.gameObject.activeInHierarchy && !string.IsNullOrWhiteSpace(guidanceText.text);

    Canvas canvas;
    GameObject safeRoot;
    TextMeshProUGUI speedText, gearText, nextStopText, etaText, guidanceText, scheduleText;
    TextMeshProUGUI passengerText, fuelText, temperatureText, gradeText;
    TextMeshProUGUI destinationText, routeBadgeText, routeNameText, clockText, weatherText;
    Image speedNeedle, fuelNeedle, temperatureNeedle, progressFill, schedulePill, fuelBarFill;
    Image parkLamp, retarderLamp, doorLamp, alertLamp;
    BusController bus;
    EngineTemperatureSystem temperature;
    FuelSystem fuel;
    BatterySystem battery;
    MissionManager mission;
    PassengerManager passengers;
    int renderedStopCount = -1;
    // TextMeshPro text is a native rebuild, and every one of these values used to be
    // interpolated into a fresh string on every frame whether or not it had changed.
    // That handed the GC a steady stream of small strings and rebuilt the HUD layout
    // 60 times a second for a speed value that changes a few times a s
    int shownSpeed = int.MinValue, shownGear = int.MinValue, shownEnergy = int.MinValue;
    int shownTemperature = int.MinValue, shownPax = int.MinValue, shownCapacity = int.MinValue;
    int shownClockMinute = int.MinValue;
    float routeTextTick;
    bool binding;

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
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopAllCoroutines();
        binding = false;
        StartCoroutine(BindWhenReady());
    }
    void OnDestroy() { if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded; }

    IEnumerator BindWhenReady()
    {
        if (binding) yield break;
        binding = true;

        // The bus is created by SceneBootstrap, which can finish after the HUD's
        // AfterSceneLoad callback. Giving up after one frame left the whole HUD —
        // including the route minimap — permanently hidden. Wait for the bus and
        // let Update retry if it is replaced by a scene reload.
        float deadline = Time.realtimeSinceStartup + 30f;
        while (bus == null && Time.realtimeSinceStartup < deadline)
        {
            bus = FindAnyObjectByType<BusController>();
            if (bus == null) yield return null;
        }

        if (bus == null)
        {
            SetVisible(false);
            binding = false;
            yield break;
        }

        mission = MissionManager.Instance;
        passengers = PassengerManager.Instance != null ? PassengerManager.Instance : FindAnyObjectByType<PassengerManager>();
        fuel = bus.GetComponent<FuelSystem>();
        battery = bus.GetComponent<BatterySystem>();
        temperature = bus.GetComponent<EngineTemperatureSystem>();
        if (temperature == null) temperature = bus.gameObject.AddComponent<EngineTemperatureSystem>();
        if (canvas == null) BuildHUD();
        renderedStopCount = -1;
        SetVisible(true);
        binding = false;
    }

    void Update()
    {
        if (bus == null)
        {
            if (canvas != null) canvas.gameObject.SetActive(false);
            if (!binding) StartCoroutine(BindWhenReady());
            return;
        }
        if (canvas == null) return;
        mission = MissionManager.Instance;
        bool drivingState = mission == null || (mission.missionState != MissionState.Completed && mission.missionState != MissionState.Failed);
        if (canvas.gameObject.activeSelf != drivingState) canvas.gameObject.SetActive(drivingState);
        if (!drivingState) return;
        passengers = PassengerManager.Instance;
        float speed = Mathf.Clamp(bus.currentSpeedKmh, 0f, 120f);
        int speedValue = Mathf.RoundToInt(speed);
        if (speedValue != shownSpeed)
        {
            shownSpeed = speedValue;
            speedText.text = $"{speed:0}\n<size=18>KM/H</size>";
        }
        speedNeedle.rectTransform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(130f, -130f, speed / 120f));
        int gearValue = bus.transmissionData != null ? bus.transmissionData.currentGear : 0;
        if (gearValue != shownGear)
        {
            shownGear = gearValue;
            gearText.text = FormatGear(gearValue);
        }

        float energy = battery != null ? battery.CurrentChargePercent : fuel != null ? fuel.CurrentFuelPercent : FreeDriveSession.Instance != null ? FreeDriveSession.Instance.fuelLevel : 100f;
        int energyValue = Mathf.RoundToInt(energy);
        if (energyValue != shownEnergy)
        {
            shownEnergy = energyValue;
            fuelText.text = battery != null ? $"BATTERY  {energy:0}%" : $"FUEL  {energy:0}%";
        }
        if (fuelBarFill != null) fuelBarFill.fillAmount = Mathf.Clamp01(energy / 100f);
        fuelNeedle.rectTransform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(130f, -130f, energy / 100f));
        float temp = temperature != null ? temperature.currentTemperatureC : 80f;
        if (temperature != null && WeatherSystem.Instance != null) temperature.ambientTemperatureC = WeatherSystem.Instance.temperature;
        int tempValue = Mathf.RoundToInt(temp);
        if (tempValue != shownTemperature)
        {
            shownTemperature = tempValue;
            temperatureText.text = $"ENGINE  {temp:0}°C";
            temperatureText.color = temperature != null && temperature.WarningActive ? UITheme.Error : UITheme.TextSecondary;
        }
        temperatureNeedle.rectTransform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(130f, -130f, Mathf.InverseLerp(60f, 120f, temp)));

        int pax = passengers != null ? passengers.currentPassengers : 0;
        int capacity = passengers != null ? passengers.GetMaxCapacity() : 0;
        if (pax != shownPax || capacity != shownCapacity)
        {
            shownPax = pax;
            shownCapacity = capacity;
            passengerText.text = $"{PassengerFeedbackSystem.MoodIcon(passengers != null ? passengers.averageSatisfaction : 1f)}  PASSENGERS  {pax}/{capacity}";
        }

        // Section 3.4: gradient indicator on the HUD.
        if (gradeText != null)
        {
            float gradePercent = bus.CurrentRoadGrade * 100f;
            int gradeValue = Mathf.RoundToInt(gradePercent);
            string sign = gradeValue >= 0 ? "+" : "";
            string gradeLabel = $"GRADE {sign}{gradeValue}%";
            gradeText.text = gradeLabel;
            gradeText.color = Mathf.Abs(gradeValue) >= 6 ? UITheme.Error : UITheme.TextSecondary;
        }

        UpdateIndicatorLamps();

        // The route labels change at a human pace, so they do not need to rebuild
        // every frame; 10 Hz keeps the ETA live without the layout churn.
        routeTextTick -= Time.unscaledDeltaTime;
        if (routeTextTick <= 0f)
        {
            routeTextTick = 0.1f;
            UpdateRouteReadout(speed);
            UpdateAmbientReadout();
        }

        if (UnityEngine.InputSystem.Keyboard.current != null && UnityEngine.InputSystem.Keyboard.current.mKey.wasPressedThisFrame)
            ToggleMinimap();
    }

    void UpdateIndicatorLamps()
    {
        if (parkLamp != null) parkLamp.color = bus.ParkingBrakeActive ? UITheme.BusStopRed : UITheme.SurfaceBright;
        if (retarderLamp != null) retarderLamp.color = bus.RetarderActive ? UITheme.Accent : UITheme.SurfaceBright;
        if (doorLamp != null) doorLamp.color = passengers != null && passengers.doorsOpen ? UITheme.Secondary : UITheme.SurfaceBright;
        if (alertLamp != null) alertLamp.color = temperature != null && temperature.WarningActive ? UITheme.Error : UITheme.SurfaceBright;
    }

    void UpdateAmbientReadout()
    {
        if (clockText != null)
        {
            TimeOfDaySystem clock = TimeOfDaySystem.Instance;
            int minute = clock != null ? Mathf.FloorToInt(clock.currentTimeMinutes) : -1;
            if (minute != shownClockMinute)
            {
                shownClockMinute = minute;
                clockText.text = clock != null ? FormatClock(clock.currentTimeMinutes) : "--:--";
            }
        }
        if (weatherText != null)
        {
            WeatherSystem weather = WeatherSystem.Instance;
            // Wind is only worth a readout once it is doing something: a still sky
            // should not print "4 km/h" next to the temperature.
            WindSystem wind = WindSystem.Instance;
            string windReadout = wind != null && wind.SpeedKmh >= 8f ? $"  ·  {wind.SpeedKmh:0} km/h" : string.Empty;
            weatherText.text = weather != null ? $"{WeatherLabel(weather.currentWeather)}  {weather.temperature:0}°{windReadout}" : "";
        }
    }

    void UpdateRouteReadout(float speed)
    {
        BusRoute activeRoute = mission != null && mission.currentRoute != null ? mission.currentRoute : GameState.Instance != null ? GameState.Instance.selectedRoute : null;
        string destination = activeRoute != null ? DisplayDestination(activeRoute.routeName) : Localization.Get("hud.freeDrive");
        if (routeBadgeText != null) routeBadgeText.text = activeRoute != null ? ShortRouteNumber(activeRoute.routeNumber) : "FF";
        if (routeNameText != null) routeNameText.text = destination;
        if (destinationText != null) destinationText.text = destination;

        bool selectedMissionStarting = mission != null &&
            (mission.routeActive || mission.CurrentCountdownValue > 0 || mission.missionState == MissionState.Briefing) &&
            GameState.Instance?.selectedRoute != null;
        if (mission == null || (!mission.routeActive && !selectedMissionStarting) || mission.currentRoute == null || mission.currentRoute.stops == null)
        {
            nextStopText.text = Localization.Get("hud.explore");
            etaText.text = Localization.Get("hud.freeDrive");
            guidanceText.text = Localization.Get("hud.noSchedule");
            scheduleText.text = Localization.Get("hud.freeDrive");
            schedulePill.color = UITheme.WithAlpha(UITheme.Secondary, 0.85f);
            progressFill.fillAmount = 0f;
            return;
        }

        int count = mission.currentRoute.stops.Length;
        int index = Mathf.Clamp(mission.currentStopIndex, 0, Mathf.Max(0, count - 1));
        string navigation = GetGuidanceInstruction();
        if (string.IsNullOrWhiteSpace(navigation))
        {
            // Keep the banner up with an explicit fallback instead of blanking the
            // navigator when the guidance line is temporarily unavailable.
            navigation = "ROUTE DATA UNAVAILABLE • CONTINUE";
        }
        guidanceText.text = navigation;
        nextStopText.text = $"{mission.currentRoute.stops[index].stopName}  •  {mission.distanceToNextStop:0} m";
        float etaSeconds = mission.distanceToNextStop / Mathf.Max(4.2f, speed / 3.6f);
        etaText.text = $"{Localization.Get("hud.routeNav")}  •  ETA {FormatEta(etaSeconds)}";
        progressFill.fillAmount = count > 1 ? Mathf.Clamp01(index / (float)(count - 1)) : 1f;

        PunctualityStatus status = ScheduleManager.Instance != null ? ScheduleManager.Instance.GetLiveStatus(index) : PunctualityStatus.OnTime;
        scheduleText.text = StatusIcon(status) + " " + StatusLabel(status).ToUpperInvariant();
        schedulePill.color = UITheme.WithAlpha(StatusColor(status), 0.9f);
        if (renderedStopCount != count) { BuildStopMarkers(count); renderedStopCount = count; }
    }

    public void ToggleMinimap()
    {
        // Minimap removed; route is shown via world-space RouteArrowOverlay instead.
    }

    public static string FormatGear(int gear) { return gear < 0 ? "R" : gear == 0 ? "D" : (gear + 1).ToString(); }
    public static string FormatEta(float seconds) { return seconds < 60f ? "<1 min" : Mathf.CeilToInt(seconds / 60f) + " min"; }

    public static string FormatClock(float minutes)
    {
        int total = Mathf.RoundToInt(Mathf.Repeat(minutes, 1440f));
        int hours = (total / 60) % 24;
        return $"{hours:00}:{total % 60:00}";
    }

    public static string WeatherLabel(WeatherState state)
    {
        switch (state)
        {
            case WeatherState.Clear: return "CLEAR";
            case WeatherState.PartlyCloudy: return "PARTLY CLD";
            case WeatherState.Overcast: return "OVERCAST";
            case WeatherState.Windy: return "WINDY";
            case WeatherState.Fog: return "FOG";
            case WeatherState.Drizzle: return "DRIZZLE";
            case WeatherState.LightRain: return "RAIN";
            case WeatherState.HeavyRain: return "HEAVY RAIN";
            case WeatherState.Thunderstorm: return "STORM";
            case WeatherState.Snow: return "SNOW";
            case WeatherState.Blizzard: return "BLIZZARD";
            default: return "—";
        }
    }

    public static string ShortRouteNumber(string routeNumber)
    {
        if (string.IsNullOrWhiteSpace(routeNumber)) return "•";
        string trimmed = routeNumber.Trim();
        // Every route now starts with the city namespace. The roundel needs the
        // service number, not three letters of "NBO" on every bus in the city.
        if (trimmed.StartsWith(RouteNaming.CityPrefix + "-", System.StringComparison.OrdinalIgnoreCase))
        {
            trimmed = trimmed.Substring(RouteNaming.CityPrefix.Length + 1).TrimStart('-', '_', ' ');
            if (trimmed.Length == 0) return RouteNaming.CityPrefix;
        }
        return trimmed.Length <= 3 ? trimmed.ToUpperInvariant() : trimmed.Substring(0, 3).ToUpperInvariant();
    }

    /// <summary>
    /// Reduces a generated route name — which can read like
    /// "NBO STOP 928938241 • MINIBUSES TO WESTLANDS VIA STOP 1284303773 (RETURN)" —
    /// to the part a destination sign actually shows: the destination itself.
    /// </summary>
    public static string DisplayDestination(string routeName)
    {
        if (string.IsNullOrWhiteSpace(routeName)) return "";

        string value = routeName.Trim();
        int to = value.LastIndexOf(" TO ", System.StringComparison.OrdinalIgnoreCase);
        if (to >= 0 && value.Length - to > 4) value = value.Substring(to + 4);

        int via = value.IndexOf(" VIA ", System.StringComparison.OrdinalIgnoreCase);
        if (via > 0) value = value.Substring(0, via);

        int paren = value.LastIndexOf(" (", System.StringComparison.Ordinal);
        if (paren > 0) value = value.Substring(0, paren);

        value = value.Trim(' ', '-', '•', '|');
        return value.Length == 0 ? routeName.Trim().ToUpperInvariant() : value.ToUpperInvariant();
    }

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

        BuildNavigationCard();
        BuildDestinationSign();
        BuildNavigationBanner();
        BuildSpeedCluster();
        BuildVitalsPanel();
    }

    // ── Left: compact route card (no minimap — navigation uses world arrows) ──
    void BuildNavigationCard()
    {
        RectTransform mapPanel = Panel("NavigationCard", safeRoot.transform, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(470f, 96f), new Vector2(20f, -20f));
        Image navBorder = Box("NavBorder", mapPanel, Vector2.zero, new Vector2(0f, 4f), UITheme.BusAmber);
        navBorder.rectTransform.anchorMin = new Vector2(0f, 1f); navBorder.rectTransform.anchorMax = new Vector2(1f, 1f); navBorder.rectTransform.pivot = new Vector2(0.5f, 1f); navBorder.rectTransform.sizeDelta = new Vector2(0f, 4f);

        Image roundel = Box("RouteRoundel", mapPanel, new Vector2(20f, -24f), new Vector2(48f, 48f), UITheme.BusAmber);
        roundel.rectTransform.anchorMin = roundel.rectTransform.anchorMax = new Vector2(0f, 1f); roundel.rectTransform.pivot = new Vector2(0f, 1f);
        RuntimeUiShapes.Circle(roundel);
        routeBadgeText = Text("RouteNumber", roundel.transform, "1", 22, TextAlignmentOptions.Center, Vector2.zero, new Vector2(48f, 48f));
        routeBadgeText.color = UITheme.Background;
        routeBadgeText.rectTransform.anchorMin = Vector2.zero; routeBadgeText.rectTransform.anchorMax = Vector2.one; routeBadgeText.rectTransform.offsetMin = routeBadgeText.rectTransform.offsetMax = Vector2.zero;
        routeNameText = Text("RouteName", mapPanel, "FREE DRIVE", 15, TextAlignmentOptions.Left, new Vector2(78f, -20f), new Vector2(374f, 26f));
        routeNameText.rectTransform.anchorMin = routeNameText.rectTransform.anchorMax = new Vector2(0f, 1f); routeNameText.rectTransform.pivot = new Vector2(0f, 0.5f);
        routeNameText.color = UITheme.TextSecondary;
        FitSingleLine(routeNameText);

        Image schedulePillBg = Box("SchedulePill", mapPanel, new Vector2(14f, 12f), new Vector2(150f, 24f), UITheme.Secondary);
        schedulePill = schedulePillBg;
        schedulePill.rectTransform.anchorMin = schedulePill.rectTransform.anchorMax = new Vector2(0f, 0f); schedulePill.rectTransform.pivot = new Vector2(0f, 0f);
        RuntimeUiShapes.Rounded(schedulePill);
        scheduleText = Text("ScheduleText", schedulePill.rectTransform, "NO SCHEDULE", 12, TextAlignmentOptions.Center, Vector2.zero, schedulePill.rectTransform.sizeDelta);
        scheduleText.rectTransform.anchorMin = Vector2.zero; scheduleText.rectTransform.anchorMax = Vector2.one; scheduleText.rectTransform.offsetMin = scheduleText.rectTransform.offsetMax = Vector2.zero;
        etaText = Text("Eta", mapPanel, "ROUTE", 13, TextAlignmentOptions.Right, new Vector2(-14f, 14f), new Vector2(290f, 24f));
        etaText.rectTransform.anchorMin = etaText.rectTransform.anchorMax = new Vector2(1f, 0f); etaText.rectTransform.pivot = new Vector2(1f, 0f);
        etaText.color = UITheme.TextSecondary;
    }

    // ── Top-centre: amber LED destination sign ────────────────────────
    void BuildDestinationSign()
    {
        RectTransform sign = Panel("DestinationSign", safeRoot.transform, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(600f, 72f), new Vector2(0f, -18f));
        Image ledStripe = Box("LedStripe", sign, new Vector2(10f, 0f), new Vector2(6f, 44f), UITheme.BusAmberDim);
        ledStripe.rectTransform.anchorMin = ledStripe.rectTransform.anchorMax = new Vector2(0f, 0.5f);
        ledStripe.rectTransform.pivot = new Vector2(0f, 0.5f);
        destinationText = Text("Destination", sign, "ROUTE", 24, TextAlignmentOptions.Left, new Vector2(26f, 0f), new Vector2(380f, 34f));
        destinationText.rectTransform.anchorMin = destinationText.rectTransform.anchorMax = new Vector2(0f, 0.5f);
        destinationText.rectTransform.pivot = new Vector2(0f, 0.5f);
        destinationText.color = UITheme.BusAmber;
        destinationText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        FitSingleLine(destinationText);

        clockText = Text("Clock", sign, "08:00", 22, TextAlignmentOptions.Right, new Vector2(-18f, 10f), new Vector2(150f, 30f));
        clockText.rectTransform.anchorMin = clockText.rectTransform.anchorMax = new Vector2(1f, 0.5f); clockText.rectTransform.pivot = new Vector2(1f, 0.5f);
        // Wide enough for the wind readout below the temperature; the right pivot lets
        // it grow leftwards instead of running into the clock above it.
        weatherText = Text("Weather", sign, "CLEAR 20°", 12, TextAlignmentOptions.Right, new Vector2(-18f, -14f), new Vector2(210f, 22f));
        weatherText.rectTransform.anchorMin = weatherText.rectTransform.anchorMax = new Vector2(1f, 0.5f); weatherText.rectTransform.pivot = new Vector2(1f, 0.5f);
        weatherText.color = UITheme.TextMuted;
    }

    // ── Top-centre: turn instruction, next stop and route progress ────
    void BuildNavigationBanner()
    {
        RectTransform routePanel = Panel("NextStopBanner", safeRoot.transform, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(660f, 126f), new Vector2(0f, -98f));
        Image bannerAccent = Box("BannerAccent", routePanel, Vector2.zero, new Vector2(0f, 3f), UITheme.Accent);
        bannerAccent.rectTransform.anchorMin = new Vector2(0f, 1f); bannerAccent.rectTransform.anchorMax = new Vector2(1f, 1f);
        bannerAccent.rectTransform.pivot = new Vector2(0.5f, 1f); bannerAccent.rectTransform.sizeDelta = new Vector2(0f, 3f);
        guidanceText = Text("Guidance", routePanel, "CONTINUE STRAIGHT", 27, TextAlignmentOptions.Center, new Vector2(0f, 30f), new Vector2(620f, 38f));
        guidanceText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        FitSingleLine(guidanceText);
        nextStopText = Text("NextStop", routePanel, "NEXT STOP", 15, TextAlignmentOptions.Center, new Vector2(0f, 2f), new Vector2(600f, 24f));
        nextStopText.color = UITheme.TextSecondary;
        FitSingleLine(nextStopText);

        Image track = Box("ProgressTrack", routePanel, new Vector2(0f, -46f), new Vector2(600f, 7f), UITheme.SurfaceBright);
        RuntimeUiShapes.Rounded(track);
        progressFill = Box("ProgressFill", track.rectTransform, Vector2.zero, Vector2.zero, UITheme.Accent);
        progressFill.type = Image.Type.Filled; progressFill.fillMethod = Image.FillMethod.Horizontal; progressFill.fillOrigin = 0;
        RuntimeUiShapes.Rounded(progressFill);
        Stretch(progressFill.rectTransform);
    }

    // ── Right: instrument-cluster speed dial ──────────────────────────
    void BuildSpeedCluster()
    {
        RectTransform speedPanel = CirclePanel("SpeedDial", safeRoot.transform, new Vector2(1f, 1f), new Vector2(1f, 1f), 200f, new Vector2(-16f, -16f), UITheme.BusAmber);
        Image speedFace = Box("SpeedFace", speedPanel, Vector2.zero, new Vector2(172f, 172f), new Color(.03f, .038f, .045f, .98f));
        RuntimeUiShapes.Circle(speedFace);
        AddDialTicks(speedFace.transform, 74f, new Color(1f, 1f, 1f, .32f));
        speedText = Text("Speed", speedPanel, "0\n<size=18>KM/H</size>", 52, TextAlignmentOptions.Center, new Vector2(0f, 12f), new Vector2(150f, 104f));
        speedNeedle = Needle("SpeedNeedle", speedPanel, 74f, UITheme.Accent);
        gearText = Text("Gear", speedPanel, "D", 26, TextAlignmentOptions.Center, new Vector2(0f, -64f), new Vector2(64f, 34f));
        gearText.color = UITheme.Accent;

        // Section 3.4: road grade indicator showing the baked grade at the bus.
        gradeText = Text("Grade", speedPanel, "GRADE 0.0%", 18, TextAlignmentOptions.Center, new Vector2(0f, -100f), new Vector2(80f, 24f));
        gradeText.color = UITheme.TextSecondary;

        BuildIndicatorStrip();
    }

    void BuildIndicatorStrip()
    {
        RectTransform strip = Panel("IndicatorStrip", safeRoot.transform, new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(260f, 40f), new Vector2(-16f, -228f));
        parkLamp = Lamp("ParkLamp", strip, new Vector2(38f, 0f), "P");
        retarderLamp = Lamp("RetarderLamp", strip, new Vector2(100f, 0f), "R");
        doorLamp = Lamp("DoorLamp", strip, new Vector2(162f, 0f), "D");
        alertLamp = Lamp("AlertLamp", strip, new Vector2(224f, 0f), "!");
    }

    Image Lamp(string name, RectTransform parent, Vector2 position, string label)
    {
        Image lamp = Box(name, parent, position, new Vector2(30f, 30f), UITheme.SurfaceBright);
        lamp.rectTransform.anchorMin = lamp.rectTransform.anchorMax = new Vector2(0f, 0.5f);
        lamp.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        RuntimeUiShapes.Circle(lamp);
        TextMeshProUGUI text = Text(name + "Label", lamp.rectTransform, label, 14, TextAlignmentOptions.Center, Vector2.zero, new Vector2(30f, 30f));
        text.color = UITheme.TextPrimary;
        text.rectTransform.anchorMin = Vector2.zero; text.rectTransform.anchorMax = Vector2.one; text.rectTransform.offsetMin = text.rectTransform.offsetMax = Vector2.zero;
        return lamp;
    }

    void AddDialTicks(Transform parent, float radius, Color tint)
    {
        const int count = 13;
        for (int i = 0; i < count; i++)
        {
            float angle = Mathf.Lerp(130f, -130f, i / (float)(count - 1));
            Vector2 direction = new Vector2(-Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
            Image tick = Box("Tick_" + i, parent, direction * radius, new Vector2(2f, i % 3 == 0 ? 12f : 7f), tint);
            // A plain quad reads as a crisp tick; the shared sliced sprite is too
            // small to resolve at this size.
            tick.sprite = null; tick.type = Image.Type.Simple;
            tick.rectTransform.anchorMin = tick.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            tick.rectTransform.pivot = new Vector2(0.5f, 0f);
            tick.rectTransform.localEulerAngles = new Vector3(0f, 0f, angle);
        }
    }

    // ── Right, below the dial: vehicle vitals ─────────────────────────
    void BuildVitalsPanel()
    {
        RectTransform panel = Panel("VehicleVitals", safeRoot.transform, new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(260f, 146f), new Vector2(-16f, -282f));
        fuelNeedle = Needle("FuelNeedle", panel, 34f, UITheme.TertiaryDim); fuelNeedle.gameObject.SetActive(false);
        temperatureNeedle = Needle("TemperatureNeedle", panel, 34f, UITheme.Success); temperatureNeedle.gameObject.SetActive(false);

        TextMeshProUGUI vitalsHeader = Text("VitalsHeader", panel, "VEHICLE STATUS", 11, TextAlignmentOptions.Left, new Vector2(14f, -12f), new Vector2(232f, 18f));
        vitalsHeader.color = UITheme.TextMuted;
        vitalsHeader.rectTransform.anchorMin = vitalsHeader.rectTransform.anchorMax = new Vector2(0f, 1f);
        vitalsHeader.rectTransform.pivot = new Vector2(0f, 0.5f);

        fuelText = Text("Fuel", panel, "FUEL  100%", 13, TextAlignmentOptions.Left, new Vector2(14f, -32f), new Vector2(232f, 20f));
        fuelText.rectTransform.anchorMin = fuelText.rectTransform.anchorMax = new Vector2(0f, 1f); fuelText.rectTransform.pivot = new Vector2(0f, 0.5f);

        Image fuelTrack = Box("FuelTrack", panel, new Vector2(14f, -52f), new Vector2(232f, 8f), UITheme.SurfaceBright);
        fuelTrack.rectTransform.anchorMin = fuelTrack.rectTransform.anchorMax = new Vector2(0f, 1f); fuelTrack.rectTransform.pivot = new Vector2(0f, 0.5f);
        RuntimeUiShapes.Rounded(fuelTrack);
        fuelBarFill = Box("FuelFill", fuelTrack.rectTransform, Vector2.zero, Vector2.zero, UITheme.Success);
        fuelBarFill.type = Image.Type.Filled; fuelBarFill.fillMethod = Image.FillMethod.Horizontal; fuelBarFill.fillOrigin = 0;
        RuntimeUiShapes.Rounded(fuelBarFill);
        Stretch(fuelBarFill.rectTransform);

        passengerText = Text("Passengers", panel, "●  PASSENGERS  0/0", 13, TextAlignmentOptions.Left, new Vector2(14f, -76f), new Vector2(232f, 20f));
        passengerText.rectTransform.anchorMin = passengerText.rectTransform.anchorMax = new Vector2(0f, 1f); passengerText.rectTransform.pivot = new Vector2(0f, 0.5f);
        temperatureText = Text("Temperature", panel, "ENGINE  80°C", 13, TextAlignmentOptions.Left, new Vector2(14f, -104f), new Vector2(232f, 20f));
        temperatureText.rectTransform.anchorMin = temperatureText.rectTransform.anchorMax = new Vector2(0f, 1f); temperatureText.rectTransform.pivot = new Vector2(0f, 0.5f);
        temperatureText.color = UITheme.TextSecondary;
    }


    void BuildStopMarkers(int count)
    {
        Transform track = progressFill.transform.parent;
        for (int i = track.childCount - 1; i >= 0; i--) if (track.GetChild(i) != progressFill.transform) Destroy(track.GetChild(i).gameObject);
        for (int i = 0; i < count; i++)
        {
            Image marker = Box("Stop_" + i, track, Vector2.zero, new Vector2(12f, 12f), UITheme.TextPrimary);
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
        string instruction = angle > 28f ? "TURN RIGHT" : angle < -28f ? "TURN LEFT" : angle > 9f ? "KEEP RIGHT" : angle < -9f ? "KEEP LEFT" : "CONTINUE STRAIGHT";
        if (mission.HasActiveDiversion) instruction = mission.CurrentDiversionLabel.ToUpperInvariant() + " • " + instruction;
        return instruction;
    }

    RectTransform Panel(string name, Transform parent, Vector2 anchor, Vector2 pivot, Vector2 size, Vector2 position)
    {
        Image image = Box(name, parent, position, size, new Color(0.04f, 0.06f, 0.075f, 0.9f));
        image.rectTransform.anchorMin = image.rectTransform.anchorMax = anchor; image.rectTransform.pivot = pivot;
        Outline outline = image.gameObject.AddComponent<Outline>(); outline.effectColor = new Color(1f, 1f, 1f, .16f); outline.effectDistance = new Vector2(2f, -2f);
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
        TextMeshProUGUI t = o.GetComponent<TextMeshProUGUI>(); t.text = value; t.fontSize = Mathf.Max(11f, size); t.alignment = align; t.color = UITheme.TextPrimary; t.font = UITheme.GetFont(size >= 26 ? UITheme.FontWeight.Bold : UITheme.FontWeight.Medium); t.raycastTarget = false;
        return t;
    }

    static void Stretch(RectTransform r) { r.anchorMin = Vector2.zero; r.anchorMax = Vector2.one; r.offsetMin = r.offsetMax = Vector2.zero; }

    static void FitSingleLine(TextMeshProUGUI text)
    {
        text.textWrappingMode = TextWrappingModes.NoWrap;
        text.overflowMode = TextOverflowModes.Ellipsis;
        text.maxVisibleLines = 1;
    }

    void SetVisible(bool visible) { if (canvas != null) canvas.gameObject.SetActive(visible); }
}
