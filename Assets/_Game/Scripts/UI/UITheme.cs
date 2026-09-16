using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Central design system for RealBus UI — 60/30/10 rule.
/// Colors extracted from the RealBus HTML reference design.
/// </summary>
public static class UITheme
{
    // ── 60% — Background & Neutrals ─────────────────────────────────
    public static readonly Color Background          = HEX("#0C0F10");
    public static readonly Color BackgroundAlt       = HEX("#101416");
    public static readonly Color Surface             = HEX("#161A1C");
    public static readonly Color SurfaceContainer    = HEX("#1C2023");
    public static readonly Color SurfaceHigh         = HEX("#222629");
    public static readonly Color SurfaceBright       = HEX("#282D30");

    // ── 30% — Secondary Surfaces ────────────────────────────────────
    public static readonly Color OutlineVariant      = HEX("#45484A");
    public static readonly Color Outline             = HEX("#737678");
    public static readonly Color OnSurfaceVariant    = HEX("#A9ABAE");

    // ── 10% — Accent (Primary Orange) ───────────────────────────────
    public static readonly Color Accent              = HEX("#FF9159"); // primary
    public static readonly Color AccentDim           = HEX("#FF7524"); // primary-dim
    public static readonly Color AccentFixed         = HEX("#FF7A2F"); // primary-fixed

    // ── Secondary (Blue) ────────────────────────────────────────────
    public static readonly Color Secondary           = HEX("#15A4FF");
    public static readonly Color SecondaryContainer  = HEX("#00629D");

    // ── Tertiary (Yellow) ───────────────────────────────────────────
    public static readonly Color Tertiary            = HEX("#FFE483");
    public static readonly Color TertiaryDim         = HEX("#EDC600");

    // ── Text ─────────────────────────────────────────────────────────
    public static readonly Color TextPrimary         = HEX("#F8F9FC");
    public static readonly Color TextSecondary       = HEX("#A9ABAE");
    public static readonly Color TextMuted           = HEX("#737678");

    // ── Status ──────────────────────────────────────────────────────
    public static readonly Color Success             = HEX("#34D399");
    public static readonly Color Error               = HEX("#FF7351");
    public static readonly Color ErrorContainer      = HEX("#B92902");

    // ── Continent colors ────────────────────────────────────────────
    public static readonly Color Africa              = HEX("#FF9159"); // orange
    public static readonly Color Europe              = HEX("#15A4FF"); // blue
    public static readonly Color Asia                = HEX("#A78BFA"); // violet
    public static readonly Color Americas            = HEX("#34D399"); // emerald
    public static readonly Color Oceania             = HEX("#22D3EE"); // cyan

    // ── Helpers ─────────────────────────────────────────────────────
    public static Color HEX(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color c);
        return c;
    }

    public static Color WithAlpha(Color c, float a)
    {
        c.a = a;
        return c;
    }

    public static Color GetContinentColor(string country)
    {
        string[] africa   = { "Kenya", "Uganda", "Tanzania", "Rwanda",
                              "Egypt", "South Africa", "Ethiopia",
                              "Ghana", "Nigeria", "Senegal" };
        string[] europe   = { "United Kingdom", "France", "Germany",
                              "Italy", "Spain", "Netherlands", "Sweden" };
        string[] asia     = { "Japan", "India", "Bangladesh", "China",
                              "Thailand", "Vietnam", "Indonesia" };
        string[] americas = { "Canada", "United States", "Brazil",
                              "Mexico", "Argentina", "Colombia" };

        foreach (var c in africa)   if (country == c) return Africa;
        foreach (var c in europe)   if (country == c) return Europe;
        foreach (var c in asia)     if (country == c) return Asia;
        foreach (var c in americas) if (country == c) return Americas;

        return Accent;
    }

    // ── Font helper ─────────────────────────────────────────────────
    public static TMP_FontAsset GetFont(FontWeight weight = FontWeight.Regular)
    {
        if (UIFonts.Instance == null) return null;
        switch (weight)
        {
            case FontWeight.Bold:    return UIFonts.Instance.bold;
            case FontWeight.Medium:  return UIFonts.Instance.medium;
            case FontWeight.Light:   return UIFonts.Instance.lightFont;
            default:                 return UIFonts.Instance.regular;
        }
    }

    public enum FontWeight { Light, Regular, Medium, Bold }
}

/// <summary>Generated sprites used by runtime-built mobile UI.</summary>
public static class RuntimeUiShapes
{
    static Sprite roundedRectangle;
    static Sprite circle;

    public static void Rounded(Image image)
    {
        if (image == null) return;
        image.sprite = roundedRectangle != null ? roundedRectangle : roundedRectangle = BuildRoundedRectangle();
        image.type = Image.Type.Sliced;
    }

    public static void Circle(Image image)
    {
        if (image == null) return;
        image.sprite = circle != null ? circle : circle = BuildCircle();
        image.type = Image.Type.Simple;
        image.preserveAspect = true;
    }

    public static void SoftShadow(Graphic graphic, float alpha = 0.45f, float distance = 8f)
    {
        if (graphic == null) return;
        Shadow shadow = graphic.GetComponent<Shadow>();
        if (shadow == null) shadow = graphic.gameObject.AddComponent<Shadow>();
        shadow.effectColor = new Color(0f, 0f, 0f, alpha);
        shadow.effectDistance = new Vector2(0f, -distance);
        shadow.useGraphicAlpha = true;
    }

    static Sprite BuildRoundedRectangle()
    {
        const int size = 64;
        const int radius = 16;
        Texture2D texture = NewTexture("Runtime Rounded Rectangle", size);
        Color32[] pixels = new Color32[size * size];
        for (int y = 0; y < size; y++)
        for (int x = 0; x < size; x++)
        {
            float cx = Mathf.Clamp(x + .5f, radius, size - radius);
            float cy = Mathf.Clamp(y + .5f, radius, size - radius);
            float distance = Vector2.Distance(new Vector2(x + .5f, y + .5f), new Vector2(cx, cy));
            byte alpha = (byte)Mathf.RoundToInt(Mathf.Clamp01(radius + .5f - distance) * 255f);
            pixels[y * size + x] = new Color32(255, 255, 255, alpha);
        }
        texture.SetPixels32(pixels); texture.Apply(false, true);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(.5f, .5f), 100f, 0,
            SpriteMeshType.FullRect, new Vector4(radius, radius, radius, radius));
        sprite.name = texture.name; sprite.hideFlags = HideFlags.HideAndDontSave;
        return sprite;
    }

    static Sprite BuildCircle()
    {
        const int size = 128;
        float radius = size * .5f - 1f;
        Vector2 center = new Vector2(size * .5f, size * .5f);
        Texture2D texture = NewTexture("Runtime Circle", size);
        Color32[] pixels = new Color32[size * size];
        for (int y = 0; y < size; y++)
        for (int x = 0; x < size; x++)
        {
            float distance = Vector2.Distance(new Vector2(x + .5f, y + .5f), center);
            byte alpha = (byte)Mathf.RoundToInt(Mathf.Clamp01(radius + .75f - distance) * 255f);
            pixels[y * size + x] = new Color32(255, 255, 255, alpha);
        }
        texture.SetPixels32(pixels); texture.Apply(false, true);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(.5f, .5f), 100f);
        sprite.name = texture.name; sprite.hideFlags = HideFlags.HideAndDontSave;
        return sprite;
    }

    static Texture2D NewTexture(string name, int size)
    {
        return new Texture2D(size, size, TextureFormat.RGBA32, false, true)
        {
            name = name,
            filterMode = FilterMode.Bilinear,
            wrapMode = TextureWrapMode.Clamp,
            hideFlags = HideFlags.HideAndDontSave
        };
    }
}
