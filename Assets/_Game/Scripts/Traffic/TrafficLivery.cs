using UnityEngine;

/// <summary>
/// Gives every spawned AI vehicle its own paint. The imported packs share one
/// prefab per model, so without this every sedan on the road is the same colour.
/// The body slot is found rather than guessed from the hierarchy: it is the opaque,
/// near-neutral material slot, which is exactly the slot the importer painted white
/// and left untextured (or textured with the neutral pixel atlas). Glass is skipped
/// by its alpha, lamps by their colour, so a tint cannot turn a windscreen green or
/// a headlight into bodywork.
///
/// Colours go through a <see cref="MaterialPropertyBlock"/>, so the shared prefab
/// material is never instantiated and the drawn mesh stays on the same material.
/// </summary>
public static class TrafficLivery
{
    /// <summary>
    /// Consumer-car colours, then a few vehicles that read as fleet or commercial
    /// rather than private: taxi yellow, bus green and a pair of vans in works
    /// orange and white. Every entry is dark enough to keep the white-ish body
    /// texture readable after the tint is multiplied in.
    /// </summary>
    static readonly Color[] Paints =
    {
        new Color(0.86f, 0.87f, 0.88f), // white
        new Color(0.58f, 0.60f, 0.63f), // silver
        new Color(0.16f, 0.17f, 0.19f), // graphite
        new Color(0.055f, 0.06f, 0.07f), // black
        new Color(0.10f, 0.18f, 0.38f), // deep blue
        new Color(0.20f, 0.26f, 0.44f), // steel blue
        new Color(0.55f, 0.08f, 0.07f), // red
        new Color(0.34f, 0.05f, 0.07f), // maroon
        new Color(0.86f, 0.62f, 0.05f), // taxi yellow
        new Color(0.10f, 0.30f, 0.18f), // bus green
        new Color(0.07f, 0.27f, 0.28f), // teal
        new Color(0.78f, 0.32f, 0.08f), // works orange
        new Color(0.62f, 0.56f, 0.42f), // beach beige
        new Color(0.30f, 0.16f, 0.26f), // plum
        new Color(0.44f, 0.47f, 0.45f), // olive grey
        new Color(0.20f, 0.36f, 0.50f), // fleet blue
    };

    /// <summary>
    /// Applies a paint to every body slot of a spawned vehicle. <paramref name="model"/>
    /// and <paramref name="slot"/> keep a model's cars from all receiving the same
    /// colour while still being stable for a given pool slot.
    /// </summary>
    public static bool Apply(GameObject vehicle, string model, int slot)
    {
        if (vehicle == null) return false;

        Color paint = PaintFor(model, slot);
        float gloss = 0.42f + 0.22f * Hash01(model, slot, 17);
        bool painted = false;

        Renderer[] renderers = vehicle.GetComponentsInChildren<Renderer>(true);
        for (int i = 0; i < renderers.Length; i++)
        {
            Renderer renderer = renderers[i];
            if (renderer == null || IsWheelRenderer(renderer.name)) continue;

            Material[] materials = renderer.sharedMaterials;
            for (int m = 0; m < materials.Length; m++)
            {
                if (!IsBodyPaint(materials[m])) continue;

                // A fresh block per slot: reusing one across renderers leaves the
                // previous renderer's block whichever way the copy semantics fall.
                var block = new MaterialPropertyBlock();
                renderer.GetPropertyBlock(block, m);
                block.SetColor("_Color", paint);
                if (materials[m].HasProperty("_Glossiness")) block.SetFloat("_Glossiness", gloss);
                if (materials[m].HasProperty("_Smoothness")) block.SetFloat("_Smoothness", gloss);
                renderer.SetPropertyBlock(block, m);

                painted = true;
            }
        }

        return painted;
    }

    /// <summary>Stable paint choice for a model and pool slot; also used by the tests.</summary>
    public static Color PaintFor(string model, int slot)
    {
        int index = Mathf.Abs(Hash(model, slot, 0)) % Paints.Length;
        return Paints[index];
    }

    /// <summary>
    /// The bodywork slot: opaque, and light/neutral enough that tinting it reads as
    /// paint rather than as a recolour of the pack's authored detail. A texture does
    /// not disqualify a slot - this pack maps its bodies to a neutral pixel atlas -
    /// but the tint is only applied when the slot's own colour is near white, so a
    /// deliberately coloured or photographed surface is left alone.
    /// </summary>
    public static bool IsBodyPaint(Material material)
    {
        if (material == null) return false;

        string name = material.name.ToLowerInvariant();
        if (name.Contains("glass") || name.Contains("kaca") || name.Contains("window") ||
            name.Contains("light") || name.Contains("lamp") || name.Contains("chrome") ||
            name.Contains("tyre") || name.Contains("tire") || name.Contains("wheel"))
            return false;

        Color color = material.color;
        if (color.a < 0.95f) return false;                       // glass and transparent trim
        if (material.HasProperty("_EmissionColor") &&
            material.GetColor("_EmissionColor").maxColorComponent > 0.2f) return false; // lamps

        // Opaque white/grey body panels. A saturated authored material keeps its look.
        return color.r > 0.62f && color.g > 0.62f && color.b > 0.62f &&
               Mathf.Abs(color.r - color.g) < 0.08f && Mathf.Abs(color.g - color.b) < 0.08f;
    }

    /// <summary>
    /// These packs map the wheels through the same neutral pixel atlas as the body,
    /// so the material test alone would tint the tyres body-colour. The wheel parts
    /// are named FL/FR/BL/BR - or plainly wheel/tyre/rim on other packs - and are
    /// skipped by name.
    /// </summary>
    static bool IsWheelRenderer(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        string lower = name.ToLowerInvariant();
        if (lower == "fl" || lower == "fr" || lower == "bl" || lower == "br") return true;
        return lower.Contains("wheel") || lower.Contains("tyre") || lower.Contains("tire") || lower.Contains("rim");
    }

    static int Hash(string model, int slot, int salt)
    {
        unchecked
        {
            uint h = 2166136261u;
            if (!string.IsNullOrEmpty(model))
                for (int i = 0; i < model.Length; i++) h = (h ^ model[i]) * 16777619u;
            h = (h ^ (uint)slot) * 16777619u;
            h = (h ^ (uint)salt) * 16777619u;
            h ^= h >> 13;
            h *= 1274126177u;
            h ^= h >> 16;
            return (int)(h & 0x7FFFFFFF);
        }
    }

    static float Hash01(string model, int slot, int salt)
        => (Hash(model, slot, salt) & 0xFFFF) / 65535f;
}
