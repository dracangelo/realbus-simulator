using UnityEngine;
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