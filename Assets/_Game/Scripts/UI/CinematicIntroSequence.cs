using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// A three-beat directed sequence before the player drives: a high, wide establishing
/// orbit beside the bus, a hard cut to a low pass across its front quarter, then a melt
/// down into the exact chase pose the driving rig would have used. Letterbox bars and a
/// route title sell it as a cut, and any tap, click or key press skips to the hand-off.
///
/// This is the cheapest kind of cinematic there is. The camera work is a lerp per frame,
/// the letterbox is two UI quads and the title is one text field, so it costs nothing the
/// world streaming or traffic budgets can feel - unlike bloom, depth of field or motion
/// blur, which on a phone cost a full-resolution pass every frame. Extra shots are free
/// for the same reason: a cut is just a discontinuity between two poses.
///
/// It plays once per session, is skippable, and can be turned off outright.
/// </summary>
[DefaultExecutionOrder(-130)]
public class CinematicIntroSequence : MonoBehaviour
{
    public static CinematicIntroSequence Instance { get; private set; }

    const string EnabledKey = "RealBus.CinematicIntro";

    [Header("Shot")]
    [SerializeField] float durationSeconds = 5f;
    [SerializeField] float orbitRadius = 22f;
    [SerializeField] float orbitHeight = 10.5f;
    [SerializeField] float orbitStartFov = 38f;
    [Tooltip("Arc lift in metres at the midpoint of the melt, so the camera falls into the chase pose instead of sliding.")]
    [SerializeField] float arcLift = 2.2f;

    [Header("Cut")]
    [Tooltip("Where the establishing shot ends and the low hero pass cuts in, as a fraction of the sequence.")]
    [Range(0.2f, 0.65f)] [SerializeField] float cutFraction = 0.36f;
    [Tooltip("Length of the low hero pass, as a fraction of the sequence.")]
    [Range(0.06f, 0.3f)] [SerializeField] float heroFraction = 0.16f;
    [Tooltip("Small dutch angle on the hero pass. Zero keeps the horizon dead level.")]
    [Range(0f, 6f)] [SerializeField] float heroDutchDegrees = 2.2f;

    [Header("Frame")]
    [Range(0.04f, 0.2f)] [SerializeField] float letterboxFraction = 0.105f;
    [SerializeField] float rampSeconds = 0.7f;
    [SerializeField] float inputGraceSeconds = 0.4f;

    static bool playedThisSession;

    /// <summary>Players who would rather drive than watch can turn the shot off.</summary>
    public static bool Enabled
    {
        get => PlayerPrefs.GetInt(EnabledKey, 1) != 0;
        set { PlayerPrefs.SetInt(EnabledKey, value ? 1 : 0); PlayerPrefs.Save(); }
    }

    /// <summary>True once the shot has played, so a scene reload does not replay it.</summary>
    public static bool HasPlayedThisSession => playedThisSession;

    public bool IsPlaying { get; private set; }

    /// <summary>How far through the sequence the camera is, 0 to 1. Used by visual checks.</summary>
    public float Progress { get; private set; }

    Canvas canvas;
    Image topBar, bottomBar, vignette;
    TextMeshProUGUI titleText, subtitleText, skipText;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap()
    {
        if (FindAnyObjectByType<MissionManager>() == null) return;
        if (Instance != null) return;
        new GameObject("CinematicIntroSequence").AddComponent<CinematicIntroSequence>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    void Start()
    {
        StartCoroutine(WaitForTheWorldThenPlay());
    }

    // ── Rules that are worth pinning in a test ─────────────────────────

    /// <summary>
    /// The shot belongs after the loading overlay is gone and only once per session:
    /// playing it while the world is still streaming would show an empty city to a
    /// camera that is deliberately high up.
    /// </summary>
    public static bool ShouldPlay(bool alreadyPlayed, bool enabled, bool hasSubject, bool hasCamera)
        => !alreadyPlayed && enabled && hasSubject && hasCamera;

    /// <summary>Camera position for an orbit angle measured around the world Y axis.</summary>
    public static Vector3 OrbitPosition(Vector3 focus, float radius, float height, float angleDegrees)
    {
        Quaternion yaw = Quaternion.Euler(0f, angleDegrees, 0f);
        return focus + yaw * new Vector3(0f, 0f, radius) + Vector3.up * height;
    }

    /// <summary>Upright look-at rotation, so the shot never inherits the bus's roll.</summary>
    public static Quaternion OrbitRotation(Vector3 position, Vector3 focus)
    {
        Vector3 toFocus = focus - position;
        if (toFocus.sqrMagnitude < 0.0001f) return Quaternion.identity;
        return Quaternion.LookRotation(toFocus.normalized, Vector3.up);
    }

    /// <summary>
    /// Letterbox size from 0 to 1: rises over the first ramp, holds through the shot and
    /// falls over the last one, so the frame opens and closes with the cut.
    /// </summary>
    public static float BarAmount(float elapsed, float duration, float ramp)
    {
        if (duration <= 0f) return 0f;
        float t = Mathf.Clamp(elapsed, 0f, duration);
        if (ramp <= 0f) return 1f;
        float rise = Mathf.Clamp01(t / ramp);
        float fall = Mathf.Clamp01((duration - t) / ramp);
        return Mathf.SmoothStep(0f, 1f, Mathf.Min(rise, fall));
    }

    public static float EaseInOut(float t) => Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(t));

    /// <summary>
    /// The title settles rather than appears: it starts a few percent oversized and
    /// comes to rest. A caption that is simply switched on reads as a subtitle; one
    /// that settles reads as a title.
    /// </summary>
    public static float TitleScale(float elapsed, float settleSeconds)
        => Mathf.Lerp(1.05f, 1f, settleSeconds <= 0f ? 1f : EaseInOut(elapsed / settleSeconds));

    /// <summary>A camera pose, so one beat of the shot list can be blended without a live scene.</summary>
    public struct ShotPose
    {
        public Vector3 position;
        public Quaternion rotation;
        public float fov;
    }

    /// <summary>
    /// Shot boundaries in order: end of the establishing move, end of the hero pass, end of
    /// the sequence. The second boundary is clamped after the first, because a hero pass
    /// that ends before it starts would leave the shot list unsorted.
    /// </summary>
    public static float[] BeatEnds(float cut, float heroEnd)
    {
        float first = Mathf.Clamp01(cut);
        return new[] { first, Mathf.Clamp(heroEnd, first, 1f), 1f };
    }

    /// <summary>
    /// Index of the beat covering a normalised time. A point exactly on a boundary belongs
    /// to the later beat - that is precisely what turns shot two into a cut rather than a
    /// second move, and an off-by-one here is what would soften it.
    /// </summary>
    public static int BeatIndexAt(float t, float[] ends)
    {
        if (ends == null || ends.Length == 0) return 0;
        for (int i = 0; i < ends.Length - 1; i++)
            if (t < ends[i]) return i;
        return ends.Length - 1;
    }

    /// <summary>Progress from 0 to 1 inside one beat.</summary>
    public static float BeatProgress(float t, float[] ends, int index)
    {
        if (ends == null || ends.Length == 0) return 1f;
        int i = Mathf.Clamp(index, 0, ends.Length - 1);
        float start = i == 0 ? 0f : ends[i - 1];
        float end = ends[i];
        return end <= start ? 1f : Mathf.Clamp01((t - start) / (end - start));
    }

    /// <summary>
    /// Blends two poses for one beat. The arc lift bows the path upward at the midpoint, so
    /// the melt falls into the chase pose instead of sliding along a straight line.
    /// </summary>
    public static ShotPose Blend(ShotPose from, ShotPose to, float arcLiftMetres, float u)
    {
        float k = EaseInOut(u);
        Vector3 position = Vector3.Lerp(from.position, to.position, k);
        position.y += Mathf.Sin(Mathf.PI * k) * arcLiftMetres;
        return new ShotPose
        {
            position = position,
            rotation = Quaternion.Slerp(from.rotation, to.rotation, k),
            fov = Mathf.Lerp(from.fov, to.fov, k)
        };
    }

    /// <summary>
    /// The title card belongs to the establishing shot. It fades out before the cut so the
    /// low pass plays clean, and it opens with the letterbox rather than snapping on.
    /// </summary>
    public static float CopyAmount(float elapsed, float duration, float fadeStartFraction, float fadeSeconds)
    {
        if (duration <= 0f) return 0f;
        float start = Mathf.Clamp01(fadeStartFraction) * duration;
        if (elapsed <= start) return 1f;
        if (fadeSeconds <= 0f) return 0f;
        return 1f - EaseInOut((elapsed - start) / fadeSeconds);
    }

    // ── Runtime ────────────────────────────────────────────────────────

    IEnumerator WaitForTheWorldThenPlay()
    {
        // WorldReadyLoadingScreen destroys itself the moment the near-field world is up.
        // Waiting on its absence, rather than on a timer, keeps this shot behind the
        // same gate the player already sits through.
        float waited = 0f;
        while (waited < 40f && WorldReadyLoadingScreen.Instance != null)
        {
            waited += Time.unscaledDeltaTime;
            yield return null;
        }

        yield return null;
        yield return null;

        BusController bus = null;
        Camera camera = null;
        for (int frame = 0; frame < 180 && (bus == null || camera == null); frame++)
        {
            bus = MissionManager.Instance != null ? MissionManager.Instance.busController : null;
            if (bus == null) bus = FindAnyObjectByType<BusController>();
            camera = Camera.main != null ? Camera.main : FindAnyObjectByType<Camera>();
            if (bus == null || camera == null) yield return null;
        }

        if (!AllowedInThisRun) yield break;
        if (!ShouldPlay(playedThisSession, Enabled, bus != null, camera != null))
            yield break;

        // A skippable shot must never be able to take the camera hostage: if the world
        // is not playable yet, leave the rig alone.
        if (!bus.isActiveAndEnabled) yield break;

        yield return PlayShot(bus, camera);
    }

    IEnumerator PlayShot(BusController bus, Camera camera)
    {
        IsPlaying = true;
        playedThisSession = true;

        MobileCameraController rig = MobileCameraController.Instance;

        // The pose the driving rig is holding right now is the pose the shot lands on,
        // so the hand-back is a no-op instead of a visible snap.
        var handoff = new ShotPose
        {
            position = camera.transform.position,
            rotation = camera.transform.rotation,
            fov = camera.fieldOfView
        };

        Vector3 focus = bus.transform.position + Vector3.up * 1.4f;
        Quaternion heading = Quaternion.Euler(0f, bus.transform.eulerAngles.y, 0f);
        Vector3 origin = bus.transform.position;
        Vector3 forward = heading * Vector3.forward;
        Vector3 right = heading * Vector3.right;

        // Beat one - the establishing orbit. High, wide and slow, so the depot reads as a
        // place with depth before the player is asked to drive out of it. A 22 m orbit in
        // a dense yard can start inside a wall, so it is pulled in along its own sight line.
        Vector3 establishA = PullInFrontOfObstacles(focus,
            OrbitPosition(focus, orbitRadius, orbitHeight, bus.transform.eulerAngles.y + 138f), bus.transform);
        Vector3 establishB = PullInFrontOfObstacles(focus,
            OrbitPosition(focus, orbitRadius * 0.92f, orbitHeight * 0.86f, bus.transform.eulerAngles.y + 158f), bus.transform);
        var establishFrom = new ShotPose
        {
            position = establishA,
            rotation = OrbitRotation(establishA, focus),
            fov = orbitStartFov
        };
        var establishTo = new ShotPose
        {
            position = establishB,
            rotation = OrbitRotation(establishB, focus),
            fov = orbitStartFov - 4f
        };

        // Beat two - the hero pass. Cut in hard on the end of beat one and shot from just
        // above the kerb, because the wheels are the point of the frame.
        Quaternion heroTilt = Quaternion.AngleAxis(heroDutchDegrees, Vector3.forward);
        Vector3 heroLook = origin + Vector3.up * 2.1f;
        // Far enough out that the bus reads as a bus. An 8 m pass put the lens against
        // the flank of a 12 m vehicle and filled the frame with one black panel.
        Vector3 heroA = origin + forward * 12.5f + right * 7f;
        Vector3 heroB = origin + forward * 14.5f + right * 9.5f;
        // A depot is exactly where the next parked bus is. Pull both ends in against
        // walls, but never so close that the subject stops fitting in the frame.
        heroA = PullInFrontOfObstacles(heroLook, heroA, bus.transform, 10f);
        heroB = PullInFrontOfObstacles(heroLook, heroB, bus.transform, 10f);
        heroA.y = GroundHeightAt(heroA, bus.transform) + 1.25f;
        heroB.y = GroundHeightAt(heroB, bus.transform) + 1.7f;
        var heroFrom = new ShotPose { position = heroA, rotation = heroTilt * OrbitRotation(heroA, heroLook), fov = 38f };
        var heroTo = new ShotPose { position = heroB, rotation = heroTilt * OrbitRotation(heroB, heroLook), fov = 33f };

        float cut = Mathf.Clamp(cutFraction, 0.2f, 0.65f);
        float[] ends = BeatEnds(cut, Mathf.Clamp(cut + heroFraction, cut + 0.06f, 0.9f));

        camera.transform.SetPositionAndRotation(establishFrom.position, establishFrom.rotation);
        camera.fieldOfView = establishFrom.fov;

        if (rig != null) rig.BeginCinematicOverride();

        BuildOverlay();
        SetFrame(0f, 0f);
        float duration = QualitySettings.GetQualityLevel() <= 0 ? durationSeconds * 0.8f : durationSeconds;
        float copyFadeStart = ends[0] - 0.15f;
        float elapsed = 0f;
        bool skipped = false;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            Progress = t;
            int beat = BeatIndexAt(t, ends);
            float u = BeatProgress(t, ends, beat);

            ShotPose pose = beat == 0 ? Blend(establishFrom, establishTo, 0f, u)
                : beat == 1 ? Blend(heroFrom, heroTo, 0f, u)
                : Blend(heroTo, handoff, arcLift, u);

            camera.transform.SetPositionAndRotation(pose.position, pose.rotation);
            camera.fieldOfView = pose.fov;

            float bars = BarAmount(elapsed, duration, rampSeconds);
            SetFrame(bars, Mathf.Min(bars, CopyAmount(elapsed, duration, copyFadeStart, 0.5f)));
            if (titleText != null)
                titleText.rectTransform.localScale = Vector3.one * TitleScale(elapsed, 0.9f);

            if (elapsed > inputGraceSeconds && SkipRequested())
            {
                skipped = true;
                break;
            }

            yield return null;
        }

        // Land exactly on the rig's pose whether the shot finished or was cut short.
        camera.transform.SetPositionAndRotation(handoff.position, handoff.rotation);
        camera.fieldOfView = handoff.fov;

        float fade = 0f;
        float fadeSeconds = skipped ? 0.18f : 0.35f;
        float from = CurrentFrameAmount;
        while (fade < fadeSeconds)
        {
            fade += Time.unscaledDeltaTime;
            SetFrame(Mathf.Lerp(from, 0f, fade / fadeSeconds), 0f);
            yield return null;
        }

        SetFrame(0f, 0f);
        if (rig != null) rig.EndCinematicOverride();
        TearDownOverlay();
        Progress = 1f;
        IsPlaying = false;
    }

    /// <summary>
    /// Headless runs have no audience, and a multi-second letterboxed shot would land in
    /// every automated screenshot and frame-time sample. A visual check can opt back in.
    /// </summary>
    static bool AllowedInThisRun
    {
        get
        {
            if (!Application.isBatchMode) return true;
            return System.Environment.GetEnvironmentVariable("REALBUS_SMOKE_CAPTURE_INTRO") == "1";
        }
    }

    /// <summary>Shortens the orbit radius until the camera has a clear line to the bus.</summary>
    static Vector3 PullInFrontOfObstacles(Vector3 focus, Vector3 desired, Transform subject, float minDistance = 5f)
    {
        Vector3 direction = desired - focus;
        float distance = direction.magnitude;
        if (distance < 0.01f) return desired;

        RaycastHit[] hits = Physics.RaycastAll(focus, direction / distance, distance,
            ~0, QueryTriggerInteraction.Ignore);
        float nearest = distance;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider == null) continue;
            if (subject != null && hits[i].transform.IsChildOf(subject)) continue;
            nearest = Mathf.Min(nearest, hits[i].distance);
        }

        return nearest < distance
            ? focus + direction / distance * Mathf.Max(minDistance, nearest - 1f)
            : desired;
    }

    /// <summary>
    /// Ground height below a shot position, so the low pass clears kerbs, ramps and depot
    /// aprons instead of dipping under the road surface it is meant to skim.
    /// </summary>
    static float GroundHeightAt(Vector3 position, Transform subject)
    {
        if (!Physics.Raycast(position + Vector3.up * 4f, Vector3.down, out RaycastHit hit,
                14f, ~0, QueryTriggerInteraction.Ignore))
            return position.y;
        if (subject != null && hit.transform.IsChildOf(subject)) return position.y;
        // A roof or a parked bus over the shot position is not the ground: keep the camera
        // at its authored height instead of perching it on whatever the ray found.
        if (hit.point.y > position.y + 0.2f) return position.y;
        return hit.point.y;
    }

    static bool SkipRequested()
    {
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) return true;
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) return true;
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame) return true;
        return false;
    }

    void BuildOverlay()
    {
        var canvasObject = new GameObject("Cinematic Intro Canvas", typeof(Canvas), typeof(CanvasScaler));
        canvasObject.transform.SetParent(transform, false);
        canvas = canvasObject.GetComponent<Canvas>();
        // Under the loading overlay (490) and above the HUD, so the frame reads as a cut
        // rather than as another panel.
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 480;

        var scaler = canvasObject.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;

        topBar = MakeBar("Top Bar", true);
        bottomBar = MakeBar("Bottom Bar", false);

        // The same edge falloff the loading card uses, so the shot the player is
        // dropped into is framed the way the wait that preceded it was framed.
        vignette = new GameObject("Vignette", typeof(RectTransform), typeof(Image)).GetComponent<Image>();
        vignette.transform.SetParent(canvas.transform, false);
        vignette.sprite = RuntimeUiShapes.Vignette();
        vignette.type = Image.Type.Simple;
        vignette.raycastTarget = false;
        vignette.color = new Color(0f, 0f, 0f, 0f);
        RectTransform vignetteRect = vignette.rectTransform;
        vignetteRect.anchorMin = Vector2.zero;
        vignetteRect.anchorMax = Vector2.one;
        vignetteRect.offsetMin = vignetteRect.offsetMax = Vector2.zero;

        titleText = MakeText("Title", "NBO", 74f, new Vector2(0f, 6f), new Vector2(1500f, 92f),
            UITheme.FontWeight.Bold);
        subtitleText = MakeText("Subtitle", "DEPOT DEPARTURE", 24f, new Vector2(0f, -52f),
            new Vector2(1500f, 34f), UITheme.FontWeight.Medium);
        skipText = MakeText("Skip", "TAP TO SKIP", 20f, new Vector2(0f, 64f), new Vector2(500f, 30f),
            UITheme.FontWeight.Regular);

        RouteText(out string title, out string subtitle);
        titleText.text = title;
        subtitleText.text = subtitle;
        subtitleText.color = UITheme.WithAlpha(UITheme.Accent, 0.9f);
        skipText.color = UITheme.WithAlpha(UITheme.TextMuted, 0.75f);
    }

    static void RouteText(out string title, out string subtitle)
    {
        MissionManager mission = MissionManager.Instance;
        BusRoute route = mission != null ? mission.currentRoute : null;
        bool onService = mission != null && mission.routeActive && route != null;

        if (onService)
        {
            title = RouteNaming.DisplayCode(route);
            string name = RouteNaming.NormalizeName(route.routeName);
            // The normaliser adds the city prefix; the title already carries the code,
            // so drop the repeated prefix instead of printing "NBO-8  NBO CBD - Westlands".
            if (name.StartsWith(RouteNaming.CityPrefix + " ", System.StringComparison.Ordinal))
                name = name.Substring(RouteNaming.CityPrefix.Length + 1);
            subtitle = string.IsNullOrWhiteSpace(name) ? "DEPOT DEPARTURE" : name.ToUpperInvariant();
            return;
        }

        CityDefinition city = GameState.Instance != null ? GameState.Instance.selectedCity : null;
        title = city != null ? city.cityName.ToUpperInvariant() : "REALBUS";
        subtitle = "FREE DRIVE";
    }

    float CurrentFrameAmount { get; set; }

    /// <summary>
    /// Writes the frame for one instant. Bars and copy are separate curves: the bars hold
    /// through the cut, while the copy is gone before it, so the hero pass has no captions.
    /// </summary>
    void SetFrame(float amount01, float copy01)
    {
        CurrentFrameAmount = Mathf.Clamp01(amount01);
        float height = CurrentFrameAmount * letterboxFraction * 1080f;
        if (topBar != null) topBar.rectTransform.sizeDelta = new Vector2(0f, height);
        if (bottomBar != null) bottomBar.rectTransform.sizeDelta = new Vector2(0f, height);

        if (vignette != null) vignette.color = new Color(0f, 0f, 0f, 0.55f * CurrentFrameAmount);

        float copy = Mathf.Clamp01(copy01);
        if (titleText != null) titleText.color = UITheme.WithAlpha(UITheme.TextPrimary, copy);
        if (subtitleText != null) subtitleText.color = UITheme.WithAlpha(UITheme.Accent, copy * 0.9f);
        if (skipText != null) skipText.color = UITheme.WithAlpha(UITheme.TextMuted, copy * 0.75f);
    }

    void TearDownOverlay()
    {
        if (canvas != null) Destroy(canvas.gameObject);
        canvas = null; topBar = null; bottomBar = null; vignette = null;
        titleText = null; subtitleText = null; skipText = null;
    }

    Image MakeBar(string name, bool top)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(Image));
        go.transform.SetParent(canvas.transform, false);
        var image = go.GetComponent<Image>();
        image.color = Color.black;
        image.raycastTarget = false; // driving controls keep working underneath the frame

        RectTransform rect = image.rectTransform;
        rect.anchorMin = top ? new Vector2(0f, 1f) : new Vector2(0f, 0f);
        rect.anchorMax = top ? new Vector2(1f, 1f) : new Vector2(1f, 0f);
        rect.pivot = top ? new Vector2(0.5f, 1f) : new Vector2(0.5f, 0f);
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        rect.sizeDelta = new Vector2(0f, 0f);
        return image;
    }

    TextMeshProUGUI MakeText(string name, string value, float size, Vector2 position,
        Vector2 dimensions, UITheme.FontWeight weight)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI));
        go.transform.SetParent(canvas.transform, false);
        var rect = go.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.sizeDelta = dimensions;
        rect.anchoredPosition = position;

        var text = go.GetComponent<TextMeshProUGUI>();
        text.text = value;
        text.fontSize = size;
        text.alignment = TextAlignmentOptions.Center;
        text.color = UITheme.TextPrimary;
        text.font = UITheme.GetFont(weight);
        text.raycastTarget = false;
        return text;
    }
}
