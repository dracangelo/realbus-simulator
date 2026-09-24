using System;
using System.Collections.Generic;
using UnityEngine;

public enum LiveryZone
{
    Roof,
    Front,
    SidePanelA,
    SidePanelB,
    Rear,
    WheelArches
}

[Serializable]
public class LiveryData
{
    public Color[] zoneColors = new Color[6];

    public LiveryData()
    {
        for (int i = 0; i < zoneColors.Length; i++)
            zoneColors[i] = Color.white;
    }

    public LiveryData Clone()
    {
        var copy = new LiveryData();
        Array.Copy(zoneColors, copy.zoneColors, Mathf.Min(zoneColors.Length, copy.zoneColors.Length));
        return copy;
    }
}

[Serializable]
public class LiveryPreset
{
    public string presetId;
    public string displayName;
    public int requiredRank;
    public LiveryData data;
}

public static class LiveryCodeCodec
{
    // Twelve Base32 characters carry exactly six 10-bit RGBA3322 zone values.
    const string Alphabet = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";

    public static string Encode(LiveryData data)
    {
        ulong payload = 0UL;
        for (int i = 0; i < 6; i++)
        {
            Color color = data != null && data.zoneColors != null && i < data.zoneColors.Length
                ? data.zoneColors[i]
                : Color.white;
            payload |= (ulong)Pack(color) << (i * 10);
        }

        char[] code = new char[12];
        for (int i = 11; i >= 0; i--)
        {
            code[i] = Alphabet[(int)(payload & 31UL)];
            payload >>= 5;
        }
        return new string(code);
    }

    public static bool TryDecode(string code, out LiveryData data)
    {
        data = null;
        string normalized = Normalize(code);
        if (normalized.Length != 12)
            return false;

        ulong payload = 0UL;
        for (int i = 0; i < normalized.Length; i++)
        {
            int value = Alphabet.IndexOf(normalized[i]);
            if (value < 0)
                return false;
            payload = (payload << 5) | (uint)value;
        }

        data = new LiveryData();
        for (int i = 0; i < 6; i++)
            data.zoneColors[i] = Unpack((int)((payload >> (i * 10)) & 1023UL));
        return true;
    }

    public static string Normalize(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return string.Empty;
        return code.Trim().ToUpperInvariant().Replace("O", "0").Replace("I", "1").Replace("L", "1");
    }

    static int Pack(Color color)
    {
        int r = Mathf.RoundToInt(Mathf.Clamp01(color.r) * 7f);
        int g = Mathf.RoundToInt(Mathf.Clamp01(color.g) * 7f);
        int b = Mathf.RoundToInt(Mathf.Clamp01(color.b) * 3f);
        int a = Mathf.RoundToInt(Mathf.Clamp01(color.a) * 3f);
        return r | (g << 3) | (b << 6) | (a << 8);
    }

    static Color Unpack(int packed)
    {
        return new Color((packed & 7) / 7f, ((packed >> 3) & 7) / 7f, ((packed >> 6) & 3) / 3f, ((packed >> 8) & 3) / 3f);
    }
}

public class LiveryEditor : MonoBehaviour
{
    [SerializeField] Renderer[] targetRenderers = Array.Empty<Renderer>();
    [SerializeField] Texture2D zoneMap;
    [SerializeField] int textureSize = 512;
    [SerializeField] string textureProperty = "_BaseMap";
    [SerializeField] LiveryData currentLivery = new LiveryData();

    RenderTexture liveryTexture;
    Texture2D compositionTexture;
    string activeBusId;

    public RenderTexture LiveryTexture => liveryTexture;
    public LiveryData CurrentLivery => currentLivery;
    public static IReadOnlyList<LiveryPreset> Presets => BuildPresets();

    void OnDestroy()
    {
        if (liveryTexture != null) liveryTexture.Release();
        if (compositionTexture != null) Destroy(compositionTexture);
    }

    public void Initialize(string busId, Renderer[] renderers = null, Texture2D mask = null)
    {
        activeBusId = string.IsNullOrWhiteSpace(busId) ? "fleet.standard_single_decker" : busId;
        if (renderers != null) targetRenderers = renderers;
        if (mask != null) zoneMap = mask;

        if (SaveManager.Instance != null && SaveManager.Instance.TryGetLiveryCode(activeBusId, out string savedCode))
            LiveryCodeCodec.TryDecode(savedCode, out currentLivery);

        currentLivery = currentLivery ?? new LiveryData();
        // Imported meshes have their own UV layouts and material textures.
        // Painting an unmasked white atlas over them erases the source artwork.
        // Only models explicitly authored with a livery mask support repainting.
        if (zoneMap != null) Repaint();
    }

    public void SetHSV(LiveryZone zone, float hue, float saturation, float value, float opacity)
    {
        Color color = Color.HSVToRGB(Mathf.Repeat(hue, 1f), Mathf.Clamp01(saturation), Mathf.Clamp01(value));
        color.a = Mathf.Clamp01(opacity);
        FillZone(zone, color);
    }

    public void FillZone(LiveryZone zone, Color color)
    {
        EnsureData();
        currentLivery.zoneColors[Mathf.Clamp((int)zone, 0, 5)] = color;
        Repaint();
    }

    public bool ApplyPreset(int presetIndex)
    {
        var presets = BuildPresets();
        if (presetIndex < 0 || presetIndex >= presets.Count)
            return false;
        int rank = XPSystem.Instance != null ? XPSystem.Instance.CurrentRank : 1;
        if (rank < presets[presetIndex].requiredRank)
            return false;
        currentLivery = presets[presetIndex].data.Clone();
        Repaint();
        return true;
    }

    public string SaveAndGetCode()
    {
        string code = LiveryCodeCodec.Encode(currentLivery);
        SaveManager.EnsureExists().RegisterLivery(activeBusId, code);
        return code;
    }

    public bool ApplyCode(string code, bool save = true)
    {
        if (!LiveryCodeCodec.TryDecode(code, out LiveryData decoded))
            return false;
        currentLivery = decoded;
        Repaint();
        if (save) SaveManager.EnsureExists().RegisterLivery(activeBusId, LiveryCodeCodec.Encode(currentLivery));
        return true;
    }

    public void Repaint()
    {
        EnsureData();
        EnsureTexture();
        Color32[] pixels = new Color32[textureSize * textureSize];
        bool readableMask = zoneMap != null && zoneMap.isReadable;
        for (int y = 0; y < textureSize; y++)
        {
            for (int x = 0; x < textureSize; x++)
            {
                int zoneIndex = readableMask
                    ? Mathf.Clamp(Mathf.RoundToInt(zoneMap.GetPixelBilinear((x + 0.5f) / textureSize, (y + 0.5f) / textureSize).r * 5f), 0, 5)
                    : Mathf.Clamp((x * 6) / textureSize, 0, 5);
                pixels[y * textureSize + x] = currentLivery.zoneColors[zoneIndex];
            }
        }
        compositionTexture.SetPixels32(pixels);
        compositionTexture.Apply(false, false);
        Graphics.Blit(compositionTexture, liveryTexture);
        ApplyTextureToRenderers();
    }

    void EnsureData()
    {
        if (currentLivery == null) currentLivery = new LiveryData();
        if (currentLivery.zoneColors == null || currentLivery.zoneColors.Length != 6)
            currentLivery = new LiveryData();
    }

    void EnsureTexture()
    {
        int size = Mathf.Clamp(textureSize, 64, 2048);
        textureSize = size;
        if (liveryTexture == null || liveryTexture.width != size)
        {
            if (liveryTexture != null) liveryTexture.Release();
            liveryTexture = new RenderTexture(size, size, 0, RenderTextureFormat.ARGB32)
            {
                name = $"Livery_{activeBusId}",
                wrapMode = TextureWrapMode.Repeat,
                filterMode = FilterMode.Bilinear
            };
            liveryTexture.Create();
        }
        if (compositionTexture == null || compositionTexture.width != size)
        {
            if (compositionTexture != null) Destroy(compositionTexture);
            compositionTexture = new Texture2D(size, size, TextureFormat.RGBA32, false) { name = "LiveryComposition" };
        }
    }

    void ApplyTextureToRenderers()
    {
        if (targetRenderers == null || zoneMap == null)
            return;
        for (int i = 0; i < targetRenderers.Length; i++)
        {
            Renderer target = targetRenderers[i];
            if (target == null) continue;
            Material material = target.material;
            if (material.HasProperty(textureProperty)) material.SetTexture(textureProperty, liveryTexture);
            else if (material.HasProperty("_MainTex")) material.SetTexture("_MainTex", liveryTexture);
        }
    }

    static List<LiveryPreset> BuildPresets()
    {
        return new List<LiveryPreset>
        {
            Preset("preset.city_red", "City Red", 1, "C62828", "FFFFFF", "B71C1C"),
            Preset("preset.savanna", "Savanna Gold", 2, "F9A825", "263238", "FFF8E1"),
            Preset("preset.coast", "Coast Blue", 3, "0277BD", "E1F5FE", "01579B"),
            Preset("preset.forest", "Forest Line", 4, "2E7D32", "F1F8E9", "1B5E20"),
            Preset("preset.night", "Night Express", 5, "111827", "38BDF8", "E5E7EB"),
            Preset("preset.heritage", "Heritage Cream", 6, "FFF3E0", "8D6E63", "4E342E"),
            Preset("preset.metro", "Metro Silver", 7, "B0BEC5", "263238", "FF6F00"),
            Preset("preset.legend", "Legend Purple", 9, "6A1B9A", "F3E5F5", "FFD600")
        };
    }

    static LiveryPreset Preset(string id, string title, int rank, string primary, string secondary, string accent)
    {
        ColorUtility.TryParseHtmlString("#" + primary, out Color a);
        ColorUtility.TryParseHtmlString("#" + secondary, out Color b);
        ColorUtility.TryParseHtmlString("#" + accent, out Color c);
        var data = new LiveryData
        {
            zoneColors = new[] { b, a, a, b, c, c }
        };
        return new LiveryPreset { presetId = id, displayName = title, requiredRank = rank, data = data };
    }
}
