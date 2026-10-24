using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The title card both loading phases share: scene load and world warm-up draw the
/// same letterboxed frame, the same typography and the same hairline progress. That
/// is deliberate. The hand-off between them used to be a flash of two different
/// screens; now the second card is identical to the first, so the swap is invisible
/// and the whole wait reads as one shot.
///
/// The frame is cheap by construction: a background quad, a generated radial glow, a
/// generated vignette, two black bars and seven text fields. Nothing here samples the
/// scene or runs a render pass, so a 60 s warm-up costs the same as a plain panel and
/// the world streaming behind it still gets the frame time.
/// </summary>
public class CinematicLoadingCard : MonoBehaviour
{
    /// <summary>Height of one letterbox bar as a fraction of the screen.</summary>
    public const float LetterboxFraction = 0.085f;

    public const float RevealSeconds = 0.45f;
    public const float DismissSeconds = 0.5f;

    /// <summary>How long one tip stays legible before it is crossfaded for the next.</summary>
    public const float TipCycleSeconds = 7f;
    public const float TipFadeSeconds = 1.1f;

    /// <summary>Reference resolution the layout is authored against.</summary>
    const float ReferenceHeight = 1080f;

    public string Title => titleText != null ? titleText.text : string.Empty;
    public string Status => statusText != null ? statusText.text : string.Empty;
    public string RouteLine => routeText != null ? routeText.text : string.Empty;
    public float Progress { get; private set; }

    /// <summary>True once the card has finished closing and deactivated itself.</summary>
    public bool IsDismissed { get; private set; }

    /// <summary>True while the card is fully open, i.e. the bars are closed.</summary>
    public bool IsOpen => open >= 1f;

    /// <summary>Raised at the dark point of the tip crossfade, so the owner can swap the text.</summary>
    public event System.Action TipFadedOut;

    RectTransform root, content;
    CanvasGroup group;
    Image topBar, bottomBar, glow, vignette, accentRule, progressFill, progressHead;
    TextMeshProUGUI eyebrowText, titleText, routeText, statusText, percentText, tipText;

    float open;
    float time;
    float animateTimer;
    bool dismissing;
    bool tipFaded;

    public static CinematicLoadingCard Create(Transform parent)
    {
        var go = new GameObject("Cinematic Loading Card", typeof(RectTransform));
        go.transform.SetParent(parent, false);
        var card = go.AddComponent<CinematicLoadingCard>();
        card.EnsureBuilt();
        return card;
    }

    // ── Rules worth pinning in a test ───────────────────────────────────

    /// <summary>
    /// Bar height for an opening amount, 0 to 1. The bars are the card's frame: they
    /// slide in as it opens and retract as it closes, so the reveal is a cut rather
    /// than a panel appearing.
    /// </summary>
    public static float BarFraction(float open01) => Mathf.Clamp01(open01) * LetterboxFraction;

    /// <summary>
    /// Alpha of a rotating tip. Full through the body of the cycle, fading to zero at
    /// both ends so the text can be swapped at the dark point instead of snapping.
    /// </summary>
    public static float TipAlpha(float elapsed, float cycleSeconds, float fadeSeconds)
    {
        if (cycleSeconds <= 0f) return 1f;
        float t = Mathf.Repeat(elapsed, cycleSeconds);
        if (fadeSeconds <= 0f) return 1f;
        float rise = Mathf.Clamp01(t / fadeSeconds);
        float fall = Mathf.Clamp01((cycleSeconds - t) / fadeSeconds);
        return Mathf.SmoothStep(0f, 1f, Mathf.Min(rise, fall));
    }

    public static string FormatProgress(float progress01)
        => Mathf.RoundToInt(Mathf.Clamp01(progress01) * 100f) + "%";

    // ── Public API ─────────────────────────────────────────────────────

    public void Reveal(bool instant = false)
    {
        EnsureBuilt();
        gameObject.SetActive(true);
        IsDismissed = false;
        dismissing = false;
        open = instant ? 1f : 0f;
        Apply();
    }

    public void Dismiss(bool instant = false)
    {
        EnsureBuilt();
        if (instant)
        {
            open = 0f;
            IsDismissed = true;
            Apply();
            gameObject.SetActive(false);
            return;
        }
        dismissing = true;
    }

    public void SetTitle(string cityName)
    {
        EnsureBuilt();
        if (titleText != null)
            titleText.text = string.IsNullOrWhiteSpace(cityName) ? "REALBUS" : cityName.ToUpperInvariant();
    }

    public void SetEyebrow(string text)
    {
        EnsureBuilt();
        if (eyebrowText != null) eyebrowText.text = text;
    }

    public void SetStatus(string message)
    {
        EnsureBuilt();
        if (statusText != null) statusText.text = message;
    }

    public void SetProgress(float value)
    {
        EnsureBuilt();
        Progress = Mathf.Clamp01(value);
        if (progressFill != null) progressFill.fillAmount = Progress;
        if (percentText != null) percentText.text = FormatProgress(Progress);
        UpdateProgressHead();
    }

    public void SetTip(string text)
    {
        EnsureBuilt();
        if (tipText != null) tipText.text = text;
    }

    /// <summary>
    /// The route lower-third: the service the player is about to drive, under the
    /// city title. An empty route hides the line rather than leaving a gap.
    /// </summary>
    public void SetRoute(string code, string routeName)
    {
        EnsureBuilt();
        if (routeText == null) return;

        string destination = routeName ?? string.Empty;
        if (destination.StartsWith(RouteNaming.CityPrefix + " ", System.StringComparison.OrdinalIgnoreCase))
            destination = destination.Substring(RouteNaming.CityPrefix.Length + 1);

        code = string.IsNullOrWhiteSpace(code) ? string.Empty : code.Trim();
        destination = destination.Trim().ToUpperInvariant();
        if (string.IsNullOrEmpty(code) && string.IsNullOrEmpty(destination))
        {
            routeText.text = string.Empty;
            routeText.gameObject.SetActive(false);
            return;
        }

        routeText.gameObject.SetActive(true);
        if (string.IsNullOrEmpty(code)) routeText.text = destination;
        else if (string.IsNullOrEmpty(destination)) routeText.text = code;
        else routeText.text = code + "   ·   " + destination;
    }

    /// <summary>
    /// Uses the city name and the selected route, so the card says what the player
    /// is about to drive rather than just where. The route is read live: the city
    /// loading screen often appears before the world card, and the selection is
    /// already in GameState by then.
    /// </summary>
    public void SetSubject()
    {
        CityDefinition city = GameState.Instance != null ? GameState.Instance.selectedCity : null;
        SetTitle(city != null ? city.cityName : "REALBUS");

        BusRoute route = GameState.Instance != null ? GameState.Instance.selectedRoute : null;
        SetRoute(route != null ? RouteNaming.DisplayCode(route) : null, route != null ? route.routeName : null);
    }

    // ── Frame loop ─────────────────────────────────────────────────────

    void Update()
    {
        if (root == null) return;

        float dt = Time.unscaledDeltaTime;
        time += dt;

        float previousOpen = open;
        if (dismissing)
        {
            open = Mathf.Max(0f, open - dt / DismissSeconds);
            if (open <= 0f)
            {
                dismissing = false;
                IsDismissed = true;
                Apply();
                gameObject.SetActive(false);
                return;
            }
        }
        else if (open < 1f)
        {
            open = Mathf.Min(1f, open + dt / RevealSeconds);
        }

        // The frame is only rewritten while it is moving. A full-screen canvas that
        // is dirtied every frame for a static title card is exactly the kind of cost
        // a loading screen cannot afford to spend on the world streaming behind it.
        if (!Mathf.Approximately(previousOpen, open)) Apply();

        animateTimer -= dt;
        if (animateTimer > 0f) return;
        animateTimer = 0.1f;
        AnimateTip();
        AnimateAmbient();
    }

    void UpdateProgressHead()
    {
        if (progressHead == null) return;
        // The track is authored at 660 px, centred. The head is a small warm glint
        // riding the leading edge of the fill, so the bar has a direction rather
        // than a static two-colour rule.
        const float trackWidth = 660f;
        progressHead.rectTransform.anchoredPosition = new Vector2(-trackWidth * 0.5f + trackWidth * Progress, 0f);
        progressHead.enabled = Progress > 0.001f && Progress < 0.999f;
    }

    void AnimateTip()
    {
        if (tipText == null) return;
        float alpha = TipAlpha(time, TipCycleSeconds, TipFadeSeconds);

        // Swap the copy at the dark point, once per cycle: the visible text never
        // changes under the player's eyes, which is what a crossfade has to hide.
        if (alpha < 0.02f && !tipFaded)
        {
            tipFaded = true;
            TipFadedOut?.Invoke();
        }
        else if (alpha > 0.4f)
        {
            tipFaded = false;
        }

        tipText.color = UITheme.WithAlpha(UITheme.TextSecondary, alpha * 0.85f);
    }

    void AnimateAmbient()
    {
        // Two very slow, very cheap motions: the glow breathes and the accent rule
        // pulses. A loading screen that is completely static reads as a frozen game.
        if (glow != null)
            glow.rectTransform.localScale = Vector3.one * (1f + 0.025f * Mathf.Sin(time * 0.7f));

        if (accentRule != null)
        {
            float breathe = 0.5f + 0.5f * Mathf.Sin(time * 1.6f);
            accentRule.color = UITheme.WithAlpha(UITheme.Accent, 0.28f + 0.34f * breathe);
        }

        if (progressHead != null && progressHead.enabled)
        {
            float pulse = 0.72f + 0.28f * Mathf.Sin(time * 4.2f);
            progressHead.rectTransform.localScale = Vector3.one * pulse;
            progressHead.color = UITheme.WithAlpha(UITheme.Accent, 0.55f + 0.35f * pulse);
        }
    }

    void Apply()
    {
        if (root == null) return;

        float barPixels = BarFraction(open) * ReferenceHeight;
        if (topBar != null) topBar.rectTransform.sizeDelta = new Vector2(0f, barPixels);
        if (bottomBar != null) bottomBar.rectTransform.sizeDelta = new Vector2(0f, barPixels);

        // The copy drifts up a few pixels as the card opens, so the frame settles
        // around it instead of switching on.
        if (content != null)
            content.anchoredPosition = new Vector2(0f, (1f - open) * 28f);

        // The title settles from very slightly oversized rather than switching on
        // at its final size, which is the cheapest way to make the reveal read as
        // a camera move instead of a panel becoming visible.
        if (titleText != null)
            titleText.rectTransform.localScale = Vector3.one * Mathf.Lerp(1.04f, 1f, Mathf.SmoothStep(0f, 1f, open));

        if (group != null) group.alpha = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(open * 1.4f));
    }

    // ── Construction ───────────────────────────────────────────────────

    void Awake() => EnsureBuilt();

    void EnsureBuilt()
    {
        if (root != null) return;

        root = GetComponent<RectTransform>();
        root.anchorMin = Vector2.zero;
        root.anchorMax = Vector2.one;
        root.offsetMin = root.offsetMax = Vector2.zero;

        group = GetComponent<CanvasGroup>();
        if (group == null) group = gameObject.AddComponent<CanvasGroup>();
        group.alpha = 0f;

        Image background = MakeImage("Background", transform, UITheme.Background);
        Stretch(background.rectTransform);
        // Every other image in the card is decorative, but this one is the shield:
        // it must swallow touches so the pedal, steering and action clusters behind
        // the card cannot be pressed while the world is still loading.
        background.raycastTarget = true;

        // The glow sits behind everything: a warm pool the title can sit in.
        glow = MakeImage("Glow", background.transform, UITheme.WithAlpha(UITheme.AccentFixed, 0.10f));
        glow.sprite = RuntimeUiShapes.Glow();
        glow.type = Image.Type.Simple;
        glow.rectTransform.anchorMin = glow.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        glow.rectTransform.sizeDelta = new Vector2(1500f, 900f);
        glow.rectTransform.anchoredPosition = new Vector2(0f, 40f);

        vignette = MakeImage("Vignette", background.transform, new Color(0f, 0f, 0f, 0.75f));
        vignette.sprite = RuntimeUiShapes.Vignette();
        vignette.type = Image.Type.Simple;
        Stretch(vignette.rectTransform);

        content = new GameObject("Content", typeof(RectTransform)).GetComponent<RectTransform>();
        content.SetParent(background.transform, false);
        Stretch(content);

        eyebrowText = MakeText("Eyebrow", content, "PREPARING YOUR WORLD", 22f, new Vector2(0f, 192f),
            new Vector2(1200f, 34f), UITheme.FontWeight.Medium);
        eyebrowText.characterSpacing = 14f;
        eyebrowText.color = UITheme.WithAlpha(UITheme.Accent, 0.9f);

        titleText = MakeText("Title", content, "REALBUS", 104f, new Vector2(0f, 72f),
            new Vector2(1500f, 128f), UITheme.FontWeight.Bold);

        routeText = MakeText("Route", content, "", 18f, new Vector2(0f, 152f),
            new Vector2(1300f, 24f), UITheme.FontWeight.Medium);
        routeText.characterSpacing = 6f;
        routeText.color = UITheme.WithAlpha(UITheme.TextSecondary, 0.92f);
        routeText.gameObject.SetActive(false);

        accentRule = MakeImage("Accent Rule", content, UITheme.WithAlpha(UITheme.Accent, 0.55f));
        accentRule.rectTransform.anchorMin = accentRule.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        accentRule.rectTransform.sizeDelta = new Vector2(180f, 2f);
        accentRule.rectTransform.anchoredPosition = new Vector2(0f, 6f);

        statusText = MakeText("Status", content, "INITIALISING...", 22f, new Vector2(0f, -46f),
            new Vector2(1200f, 34f), UITheme.FontWeight.Medium);
        statusText.color = UITheme.TextSecondary;

        Image track = MakeImage("Progress Track", content, UITheme.SurfaceBright);
        track.rectTransform.anchorMin = track.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        track.rectTransform.sizeDelta = new Vector2(660f, 3f);
        track.rectTransform.anchoredPosition = new Vector2(0f, -112f);

        progressFill = MakeImage("Progress Fill", track.transform, UITheme.Accent);
        Stretch(progressFill.rectTransform);
        progressFill.type = Image.Type.Filled;
        progressFill.fillMethod = Image.FillMethod.Horizontal;
        progressFill.fillOrigin = 0;
        progressFill.fillAmount = 0f;

        progressHead = MakeImage("Progress Head", track.transform, UITheme.WithAlpha(UITheme.Accent, 0.8f));
        progressHead.sprite = RuntimeUiShapes.Glow();
        progressHead.type = Image.Type.Simple;
        progressHead.rectTransform.anchorMin = progressHead.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        progressHead.rectTransform.sizeDelta = new Vector2(42f, 42f);
        progressHead.enabled = false;

        percentText = MakeText("Percent", content, "0%", 20f, new Vector2(0f, -146f),
            new Vector2(220f, 30f), UITheme.FontWeight.Medium);
        percentText.color = UITheme.WithAlpha(UITheme.TextSecondary, 0.9f);

        tipText = MakeText("Tip", content, "", 22f, new Vector2(0f, -236f),
            new Vector2(1180f, 90f), UITheme.FontWeight.Regular);
        tipText.color = UITheme.WithAlpha(UITheme.TextSecondary, 0.85f);

        // Bars last so they cover the glow and the copy as the frame closes.
        topBar = MakeBar("Top Bar", true);
        bottomBar = MakeBar("Bottom Bar", false);
    }

    Image MakeBar(string name, bool top)
    {
        Image bar = MakeImage(name, transform, Color.black);
        RectTransform rect = bar.rectTransform;
        rect.anchorMin = top ? new Vector2(0f, 1f) : new Vector2(0f, 0f);
        rect.anchorMax = top ? new Vector2(1f, 1f) : new Vector2(1f, 0f);
        rect.pivot = top ? new Vector2(0.5f, 1f) : new Vector2(0.5f, 0f);
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        rect.sizeDelta = new Vector2(0f, 0f);
        return bar;
    }

    static Image MakeImage(string name, Transform parent, Color color)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(Image));
        go.transform.SetParent(parent, false);
        var image = go.GetComponent<Image>();
        image.color = color;
        image.raycastTarget = false;
        return image;
    }

    static TextMeshProUGUI MakeText(string name, Transform parent, string value, float size,
        Vector2 position, Vector2 dimensions, UITheme.FontWeight weight)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI));
        go.transform.SetParent(parent, false);
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

    static void Stretch(RectTransform rect)
    {
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = rect.offsetMax = Vector2.zero;
    }
}
