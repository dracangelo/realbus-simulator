using UnityEngine;

/// <summary>
/// Keeps a bottom-centre HUD panel clear of the on-screen driving controls.
///
/// The mobile controls own the bottom-left corner (steering buttons or wheel) and
/// the bottom-right corner (pedals plus the retarder/horn/door column). A panel
/// with a fixed width anchored to the screen centre therefore collides with them
/// as soon as the visible UI area is not exactly 16:9 - which is why the game
/// scene looked like it did not fit the buttons.
///
/// The panel is re-centred inside the band the two clusters leave free, scaled
/// down when that band is narrower than its authored width, and lifted above the
/// clusters when it cannot fit between them at all. Scaling is uniform so the
/// panel's own children keep their relative layout.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class HudSafeZoneFitter : MonoBehaviour
{
    [Tooltip("Panel to fit. Defaults to this object.")]
    public RectTransform target;

    [Tooltip("Panel size as authored; the children are laid out for this width.")]
    public Vector2 designSize = new Vector2(1060f, 86f);

    [Tooltip("Narrowest the panel may be while it still sits between the controls.")]
    public float minWidth = 760f;

    [Tooltip("Bottom offset used when the panel fits between the control clusters.")]
    public float bottomOffset = 24f;

    [Tooltip("Bottom offset used when the panel has to sit above the clusters.")]
    public float liftedBottomOffset = 360f;

    [Tooltip("Horizontal space owned by the steering cluster.")]
    public float leftReserve = 420f;

    [Tooltip("Horizontal space owned by the pedal and function cluster.")]
    public float rightReserve = 420f;

    [Tooltip("Side margin kept when the panel is lifted above the clusters.")]
    public float edgeMargin = 150f;

    float lastCanvasWidth = -1f;

    public struct Placement
    {
        public float width;
        public float offsetX;
        public float offsetY;
        public bool lifted;
        public float scale;
    }

    /// <summary>
    /// Resolves the panel placement for a given visible canvas width. Kept as a
    /// pure function so the rule can be unit tested without a live canvas.
    /// </summary>
    public static Placement Resolve(float canvasWidth, float designWidth, float minWidth,
        float leftReserve, float rightReserve, float bottomOffset, float liftedBottomOffset, float edgeMargin)
    {
        float safeDesignWidth = Mathf.Max(1f, designWidth);
        float free = canvasWidth - leftReserve - rightReserve;

        if (free >= minWidth)
        {
            // Fits between the clusters: centre it in the free band.
            float width = Mathf.Clamp(free * 0.96f, minWidth, safeDesignWidth);
            return new Placement
            {
                width = width,
                offsetX = (leftReserve - rightReserve) * 0.5f,
                offsetY = bottomOffset,
                lifted = false,
                scale = width / safeDesignWidth
            };
        }

        // Too narrow to sit between the controls: lift above them and use the
        // widest panel the screen allows.
        float lifted = Mathf.Clamp(canvasWidth - edgeMargin * 2f, minWidth * 0.6f, safeDesignWidth);
        return new Placement
        {
            width = lifted,
            offsetX = 0f,
            offsetY = liftedBottomOffset,
            lifted = true,
            scale = lifted / safeDesignWidth
        };
    }

    RectTransform Target => target != null ? target : transform as RectTransform;

    void OnEnable() => Apply();

    void Update()
    {
        RectTransform rect = Target;
        if (rect == null) return;

        float canvasWidth = ResolveCanvasWidth(rect);
        if (canvasWidth <= 1f || Mathf.Abs(canvasWidth - lastCanvasWidth) < 1f) return;
        Apply();
    }

    public void Apply()
    {
        RectTransform rect = Target;
        if (rect == null) return;

        float canvasWidth = ResolveCanvasWidth(rect);
        if (canvasWidth <= 1f) return;

        lastCanvasWidth = canvasWidth;
        Placement placement = Resolve(canvasWidth, designSize.x, minWidth,
            leftReserve, rightReserve, bottomOffset, liftedBottomOffset, edgeMargin);

        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0f);
        rect.pivot = new Vector2(0.5f, 0f);
        rect.sizeDelta = designSize;
        rect.anchoredPosition = new Vector2(placement.offsetX, placement.offsetY);
        rect.localScale = new Vector3(placement.scale, placement.scale, 1f);
    }

    /// <summary>
    /// Visible width of the canvas in reference units (1920 at 16:9), which is
    /// what the anchors are measured against.
    /// </summary>
    static float ResolveCanvasWidth(RectTransform rect)
    {
        Canvas canvas = rect.GetComponentInParent<Canvas>();
        if (canvas == null) return 0f;

        Canvas root = canvas.rootCanvas != null ? canvas.rootCanvas : canvas;
        if (root.transform is RectTransform rootRect && rootRect.rect.width > 1f)
            return rootRect.rect.width;

        return 0f;
    }
}
