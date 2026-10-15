using UnityEngine;

/// <summary>
/// Keeps a centred modal panel readable on any screen shape.
///
/// Every menu modal in the game is authored for a 1920x1080 landscape canvas
/// (the settings panel alone is 960x880), but the canvas the player actually
/// gets is not: a 1080x2340 portrait phone yields a ~978-unit-wide canvas and a
/// notch or camera cut-out eats into that further. A fixed-size centred panel
/// therefore overflows the screen edges and its own close button slides off the
/// display.
///
/// This component insets the panel inside the device safe area, re-centres it in
/// the area that is actually visible, and uniformly scales it down when the
/// visible area is smaller than the authored size. Uniform scaling is used on
/// purpose so the panel's internal layout (tab bar, cards, footer buttons) keeps
/// its proportions instead of reflowing into an unreadable state.
/// </summary>
[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
public class ResponsiveModalFitter : MonoBehaviour
{
    [Tooltip("Panel size as authored. Captured from the RectTransform when left at zero.")]
    public Vector2 designSize = Vector2.zero;

    [Tooltip("Gap kept between the panel and the edge of the safe area, in canvas units.")]
    public float margin = 24f;

    [Tooltip("Smallest uniform scale allowed, so a tiny screen still shows a usable panel.")]
    [Range(0.2f, 1f)] public float minScale = 0.5f;

    RectTransform panel;
    Vector2 lastCanvasSize;
    Rect lastSafeArea;
    Vector2Int lastScreenSize;

    public struct Fit
    {
        /// <summary>Uniform scale to apply to the panel.</summary>
        public float scale;

        /// <summary>Anchored position for a centre-anchored panel.</summary>
        public Vector2 offset;
    }

    /// <summary>
    /// Resolves how a centred panel of <paramref name="designSize"/> sits inside a
    /// canvas of <paramref name="canvasSize"/> once the safe-area insets and the
    /// edge margin are removed.
    ///
    /// <paramref name="safeInsets"/> is (left, bottom, right, top) in canvas units,
    /// measured inward from each canvas edge. Kept as a pure function so the rule
    /// can be unit tested without a live canvas or a device.
    /// </summary>
    public static Fit Resolve(Vector2 canvasSize, Vector2 designSize, Vector4 safeInsets, float margin, float minScale)
    {
        Vector2 design = new Vector2(Mathf.Max(1f, designSize.x), Mathf.Max(1f, designSize.y));
        float marginTotal = Mathf.Max(0f, margin) * 2f;

        float availableWidth = Mathf.Max(1f, canvasSize.x - safeInsets.x - safeInsets.z - marginTotal);
        float availableHeight = Mathf.Max(1f, canvasSize.y - safeInsets.y - safeInsets.w - marginTotal);

        // Never scale up: a panel that already fits stays at its authored size.
        float scale = Mathf.Min(1f, availableWidth / design.x, availableHeight / design.y);
        scale = Mathf.Clamp(scale, Mathf.Clamp01(minScale), 1f);

        // Re-centre inside the safe area rather than the raw canvas, so a notch on
        // one side does not push the panel's controls under the cut-out.
        return new Fit
        {
            scale = scale,
            offset = new Vector2((safeInsets.x - safeInsets.z) * 0.5f, (safeInsets.y - safeInsets.w) * 0.5f)
        };
    }

    RectTransform Panel
    {
        get
        {
            if (panel == null) panel = GetComponent<RectTransform>();
            return panel;
        }
    }

    void Awake()
    {
        panel = GetComponent<RectTransform>();
        if (panel != null && (designSize.x <= 0f || designSize.y <= 0f))
            designSize = panel.sizeDelta;
    }

    void OnEnable() => Apply();

    void Update()
    {
        Canvas root = ResolveCanvas();
        if (root == null) return;

        Rect canvasRect = CanvasRect(root);
        if (canvasRect.width <= 1f || canvasRect.height <= 1f) return;
        if (canvasRect.size == lastCanvasSize && Screen.safeArea == lastSafeArea &&
            Screen.width == lastScreenSize.x && Screen.height == lastScreenSize.y)
            return;

        Apply();
    }

    /// <summary>Recomputes and applies the panel's fit for the current screen.</summary>
    public void Apply()
    {
        RectTransform rect = Panel;
        if (rect == null) return;

        Canvas root = ResolveCanvas();
        if (root == null) return;

        Rect canvasRect = CanvasRect(root);
        if (canvasRect.width <= 1f || canvasRect.height <= 1f) return;

        Vector2 design = designSize.x > 0f && designSize.y > 0f ? designSize : rect.sizeDelta;
        if (design.x <= 0f || design.y <= 0f) return;

        Fit fit = Resolve(canvasRect.size, design, SafeAreaInsets(canvasRect), margin, minScale);

        rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(0.5f, 0.5f);
        rect.sizeDelta = design;
        rect.anchoredPosition = fit.offset;
        rect.localScale = new Vector3(fit.scale, fit.scale, 1f);

        lastCanvasSize = canvasRect.size;
        lastSafeArea = Screen.safeArea;
        lastScreenSize = new Vector2Int(Screen.width, Screen.height);
    }

    Canvas ResolveCanvas()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas == null) return null;
        return canvas.rootCanvas != null ? canvas.rootCanvas : canvas;
    }

    /// <summary>
    /// Size of the canvas in canvas units. <see cref="Canvas"/> itself has no rect;
    /// the dimensions live on its RectTransform.
    /// </summary>
    static Rect CanvasRect(Canvas canvas)
    {
        RectTransform rect = canvas != null ? canvas.transform as RectTransform : null;
        return rect != null ? rect.rect : default;
    }

    /// <summary>
    /// Converts <see cref="Screen.safeArea"/> (screen pixels) into canvas units so
    /// it can be subtracted from the panel's available space.
    /// </summary>
    Vector4 SafeAreaInsets(Rect canvasRect)
    {
        if (Screen.width <= 0 || Screen.height <= 0) return Vector4.zero;

        Rect safe = Screen.safeArea;
        float left = Mathf.Max(0f, safe.xMin / Screen.width * canvasRect.width);
        float right = Mathf.Max(0f, (Screen.width - safe.xMax) / Screen.width * canvasRect.width);
        float bottom = Mathf.Max(0f, safe.yMin / Screen.height * canvasRect.height);
        float top = Mathf.Max(0f, (Screen.height - safe.yMax) / Screen.height * canvasRect.height);
        return new Vector4(left, bottom, right, top);
    }
}
