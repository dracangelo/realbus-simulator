using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Repairs the materials inside imported bus and vehicle prefabs on the
/// Built-in Render Pipeline: missing source textures leave FBX panels flat
/// white, glass imports opaque, and a material can arrive with a broken shader.
/// It is deliberately runtime-safe so newly generated bus prefabs work without
/// manual material extraction.
/// </summary>
public class ImportedVehicleVisualRepair : MonoBehaviour
{
    /// <summary>
    /// One body slot the repair pass painted with the fallback colour. The
    /// player-bus livery system uses this instead of guessing which of the
    /// imported meshes are bodywork, so the glass, lamps, wheels and interior
    /// keep the finishes assigned here.
    /// </summary>
    public struct BodyPaintSlot
    {
        public Renderer renderer;
        public int materialIndex;

        public BodyPaintSlot(Renderer renderer, int materialIndex)
        {
            this.renderer = renderer;
            this.materialIndex = materialIndex;
        }
    }

    [Tooltip("Tint untextured, neutral body panels when the FBX contains no body artwork.")]
    public bool useFallbackBodyColor = true;
    public Color fallbackBodyColor = new Color(0.08f, 0.28f, 0.22f, 1f);
    readonly List<Material> runtimeMaterials = new List<Material>();
    readonly List<BodyPaintSlot> bodyPaintSlots = new List<BodyPaintSlot>();
    public IReadOnlyList<BodyPaintSlot> BodyPaintSlots => bodyPaintSlots;
    Transform lastRepairedRoot;
    bool repairApplied;

    void Start()
    {
        // This component used to be added to generated buses without ever
        // invoking RepairNow, leaving imported material problems unrepaired.
        RepairNow();
    }

    public int RepairNow(Transform visualRoot = null)
    {
        if (repairApplied && visualRoot == null)
            return GetComponentsInChildren<Renderer>(true).Length;

        Transform root = visualRoot != null ? visualRoot : transform;
        // A replacement model means the old slot list belongs to a destroyed
        // hierarchy. modelRoot and the bus root are the same tree, so a repair
        // through either entry point must not discard the slots recorded by the
        // other - that is exactly what happens when Start() runs after the
        // livery has already bound to modelRoot.
        bool sameHierarchy = lastRepairedRoot != null &&
            (root == lastRepairedRoot ||
             root.IsChildOf(lastRepairedRoot) ||
             lastRepairedRoot.IsChildOf(root));
        if (!sameHierarchy)
        {
            bodyPaintSlots.Clear();
        }
        lastRepairedRoot = root;
        var bus = GetComponent<BusController>();
        DisableEmbeddedCameras(bus != null ? bus.modelRoot : root);
        Renderer[] renderers = root.GetComponentsInChildren<Renderer>(true);
        if (renderers.Length == 0)
        {
            Debug.LogError($"ImportedVehicleVisualRepair: '{root.name}' has no renderers. Rerun Tools > RealBus > Wire Imported Models.");
            return 0;
        }

        Shader targetShader = BuiltInShaders.Lit;
        var replacements = new Dictionary<Material, Material>();
        int repaired = 0;

        foreach (Renderer renderer in renderers)
        {
            if (renderer == null) continue;
            renderer.enabled = true;
            renderer.forceRenderingOff = false;

            Material[] materials = renderer.sharedMaterials;
            bool changed = false;
            for (int i = 0; i < materials.Length; i++)
            {
                Material source = materials[i];
                bool invalid = source == null || source.shader == null || !source.shader.isSupported ||
                    source.shader.name.Contains("InternalErrorShader");
                string materialName = source != null ? source.name.ToLowerInvariant() : "";
                // Interior slots are already finished and must not be cloned again.
                if (materialName.Contains("(interior)")) continue;
                // Runtime materials have already been through this pass. Cloning
                // them again on a repeat call would stack copies and lose the
                // livery slot bookkeeping.
                if (materialName.Contains("(runtime)")) continue;
                if (source != null && source.shader != null &&
                    source.shader.name == "RealBus/BusPaint") continue;
                bool glass = materialName.Contains("glass") || materialName.Contains("kaca");
                Color sourceColor = ReadColor(source);
                bool bodyPanel = materialName == "bodi" || materialName == "body" || materialName == "karoseri";
                // Missing source textures leave many imported panels untextured and
                // near-white. Paint those (plus named body panels) with the fallback
                // colour so the bus is not a flat white shell. Glass is excluded by
                // the guard below and keeps its transparency.
                bool untextured = ReadTexture(source) == null;
                // The shared helper also catches the FBX importer's
                // (1,1,1,0) white for missing textures, which an opaque shader
                // renders as flat white.
                bool whiteShell = ImportedMaterialPalette.IsUntexturedWhite(source);
                bool fallbackPaint = useFallbackBodyColor && untextured && (bodyPanel || whiteShell);
                // The dashboard, seats, floor, ceiling and screens are untextured in
                // most of these model packs, so they import as flat white or light
                // grey panels. Give each one a plausible cockpit finish instead of
                // leaving the driver in a white plastic shell.
                Color interiorColor = Color.white;
                float interiorSmoothness = .3f;
                float interiorMetallic = 0f;
                Color interiorEmission = Color.black;
                bool interior = !glass && TryComponentFinish(renderer.name.ToLowerInvariant(), materialName, out interiorColor,
                    out interiorSmoothness, out interiorMetallic, out interiorEmission);
                // Interior and detail finishes win over the generic body
                // fallback: otherwise an untextured white dashboard, seat or
                // number plate would also be recorded as a body slot and the
                // player livery would paint the cabin.
                if (interior) fallbackPaint = false;
                if ((!invalid && !glass && !fallbackPaint && !interior) || targetShader == null) continue;

                if (source != null && replacements.TryGetValue(source, out Material cached))
                {
                    materials[i] = cached;
                    changed = true;
                    if (fallbackPaint) AddBodySlot(renderer, i);
                    continue;
                }

                Material replacement = !invalid ? new Material(source) : new Material(targetShader);
                replacement.name = (source != null ? source.name : renderer.name) + (interior ? " (Interior)" : " (Runtime)");
                Color color = interior ? interiorColor : fallbackPaint ? fallbackBodyColor : sourceColor;
                color.a = glass ? Mathf.Clamp(color.a, 0.22f, 0.55f) : Mathf.Max(color.a, 0.9f);
                // Write the Built-in albedo slot, and set .color as well so a shader
                // that only exposes the property still gets the intended colour.
                replacement.color = color;
                if (replacement.HasProperty("_Color")) replacement.SetColor("_Color", color);
                // A non-glass panel keeps whatever opaque mode it was imported with;
                // glass is switched properly in ConfigureGlass.
                Texture texture = ReadTexture(source);
                if (texture != null && replacement.HasProperty("_MainTex"))
                {
                    replacement.SetTexture("_MainTex", texture);
                    replacement.SetTextureScale("_MainTex", source.GetTextureScale("_MainTex"));
                    replacement.SetTextureOffset("_MainTex", source.GetTextureOffset("_MainTex"));
                }
                if (replacement.HasProperty("_Glossiness")) replacement.SetFloat("_Glossiness", glass ? 0.82f : interior ? interiorSmoothness : 0.34f);
                if (replacement.HasProperty("_Smoothness")) replacement.SetFloat("_Smoothness", glass ? 0.82f : interior ? interiorSmoothness : 0.34f);
                if (replacement.HasProperty("_Metallic")) replacement.SetFloat("_Metallic", interior ? interiorMetallic : 0f);
                if (interior && interiorEmission.maxColorComponent > 0.001f)
                {
                    replacement.EnableKeyword("_EMISSION");
                    if (replacement.HasProperty("_EmissionColor")) replacement.SetColor("_EmissionColor", interiorEmission);
                    replacement.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
                }
                if (glass) ConfigureGlass(replacement);

                runtimeMaterials.Add(replacement);
                if (source != null) replacements[source] = replacement;
                materials[i] = replacement;
                changed = true;
                if (fallbackPaint) AddBodySlot(renderer, i);
                repaired++;
            }
            if (changed) renderer.sharedMaterials = materials;
        }

        if (visualRoot == null)
            repairApplied = true;
        // Keep the hierarchy on the palette's late-repair list so the world can
        // be swept again once every model has finished spawning.
        if (repaired > 0)
            ImportedMaterialPalette.RegisterImportedRoot(root, fallbackBodyColor);
        Debug.Log($"ImportedVehicleVisualRepair: '{root.name}' has {renderers.Length} renderers; repaired {repaired} material slots for the Built-in Render Pipeline.");
        return renderers.Length;
    }

    /// <summary>
    /// A plausible finish for an interior or detail material, matched by the names
    /// used across the imported bus packs (Indonesian, English and a few
    /// misspellings): dasbor, kursi, lantai, plafon, gorden, krum, outih, plus the
    /// renderer names for lights, rims, plates and screens. Without this pass the
    /// generic white fallback paints every untextured white panel body-green,
    /// including wheel rims, number plates and the tablet screen. Returns false for
    /// body and glazing materials so exterior paint and glass are untouched.
    /// </summary>
    static bool TryComponentFinish(string rendererName, string name, out Color color, out float smoothness, out float metallic, out Color emission)
    {
        color = Color.white;
        smoothness = .3f;
        metallic = 0f;
        emission = Color.black;
        if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(rendererName)) return false;

        // Interior display and its bezel.
        if (rendererName.Contains("tv_layar") || name.Contains("layar") || name.Contains("screen"))
        {
            color = new Color(.012f, .014f, .02f); smoothness = .7f;
            emission = new Color(.06f, .10f, .15f); return true;
        }
        if (rendererName == "tv" || rendererName.Contains("tv") || name.Contains("monitor") || name.Contains("display"))
        {
            color = new Color(.02f, .02f, .022f); smoothness = .55f; return true;
        }
        // Lamps: the white lens colours are not body paint.
        if (rendererName.Contains("lampu_dalem") || name.Contains("lampu dalem") || name.Contains("interior light"))
        {
            color = new Color(.9f, .82f, .66f); smoothness = .35f;
            emission = new Color(.35f, .29f, .2f); return true;
        }
        if (rendererName.Contains("lampu_belakang") || name.Contains("lampu blakang"))
        {
            color = name.Contains("atret") ? new Color(.62f, .62f, .60f) : new Color(.36f, .03f, .025f);
            smoothness = .72f; return true;
        }
        if (rendererName.Contains("lampu_sein") || name.Contains("sein"))
        {
            color = new Color(.5f, .24f, .035f); smoothness = .7f; return true;
        }
        if (rendererName.Contains("lampu") || name.Contains("headlight") || name.Contains("lampu depan"))
        {
            color = new Color(.7f, .71f, .68f); smoothness = .82f; return true;
        }
        // Number plate, rims and tyres.
        if (rendererName.Contains("plat") || name.Contains("license"))
        {
            color = new Color(.76f, .76f, .72f); smoothness = .4f; metallic = .1f; return true;
        }
        if (rendererName == "rim" || rendererName.Contains("rim"))
        {
            color = new Color(.55f, .56f, .58f); smoothness = .8f; metallic = 1f; return true;
        }
        if (rendererName == "ban" || rendererName == "bsn" || name.Contains("tyre") || name.Contains("tire"))
        {
            color = new Color(.032f, .032f, .035f); smoothness = .12f; return true;
        }
        if (name == "ac" || name.Contains("lampu ac"))
        {
            color = new Color(.72f, .72f, .70f); smoothness = .3f; return true;
        }

        if (name.Contains("dasbor") || name.Contains("dashboard") || name.Contains("dash board") || name.Contains("cockpit"))
        {
            color = new Color(.052f, .055f, .06f); smoothness = .42f; metallic = .04f; return true;
        }
        if (name.Contains("kursi") || name.Contains("seat"))
        {
            color = new Color(.07f, .09f, .13f); smoothness = .16f; return true;
        }
        if (name.Contains("lantai") || name.Contains("floor") || rendererName.Contains("lantai") || rendererName.Contains("floor"))
        {
            color = new Color(.075f, .075f, .08f); smoothness = .32f; return true;
        }
        if (name.Contains("plafon") || name.Contains("ceiling") || name.Contains("headliner"))
        {
            color = new Color(.58f, .56f, .52f); smoothness = .12f; return true;
        }
        if (name.Contains("krum") || name.Contains("chrome") || name.Contains("cermin") || name.Contains("mirror"))
        {
            color = new Color(.66f, .68f, .70f); smoothness = .92f; metallic = 1f; return true;
        }
        if (name.Contains("gorden") || name.Contains("curtain"))
        {
            color = new Color(.08f, .17f, .19f); smoothness = .06f; return true;
        }
        if (name.Contains("outih") || name.Contains("interior panel") || name.Contains("trim panel"))
        {
            color = new Color(.4f, .4f, .38f); smoothness = .3f; return true;
        }
        return false;
    }

    void AddBodySlot(Renderer renderer, int materialIndex)
    {
        if (renderer == null || materialIndex < 0) return;
        for (int i = 0; i < bodyPaintSlots.Count; i++)
            if (bodyPaintSlots[i].renderer == renderer && bodyPaintSlots[i].materialIndex == materialIndex) return;
        bodyPaintSlots.Add(new BodyPaintSlot(renderer, materialIndex));
    }

    static Color ReadColor(Material source)
    {
        if (source == null) return new Color(0.82f, 0.84f, 0.86f, 1f);
        if (source.HasProperty("_Color")) return source.GetColor("_Color");
        return Color.white;
    }

    /// <summary>
    /// Switches a Built-in material to alpha-blended glass: Fade mode (2), the
    /// SrcAlpha/OneMinusSrcAlpha pair, ZWrite off, the transparent queue and the
    /// keyword the Standard shader keys its blend variants on.
    /// </summary>
    public static void ConfigureGlass(Material material)
    {
        if (material.HasProperty("_Mode")) material.SetFloat("_Mode", 2f);
        if (material.HasProperty("_SrcBlend")) material.SetFloat("_SrcBlend", (float)BlendMode.SrcAlpha);
        if (material.HasProperty("_DstBlend")) material.SetFloat("_DstBlend", (float)BlendMode.OneMinusSrcAlpha);
        if (material.HasProperty("_ZWrite")) material.SetFloat("_ZWrite", 0f);
        material.SetOverrideTag("RenderType", "Transparent");
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.renderQueue = (int)RenderQueue.Transparent;
    }

    static Texture ReadTexture(Material source)
    {
        if (source == null || !source.HasProperty("_MainTex")) return null;
        return source.GetTexture("_MainTex");
    }

    // Only pass an imported visual subtree, never the player root: gameplay
    // cameras and their listener are legitimate siblings of that subtree.
    public static void DisableEmbeddedCameras(Transform visualRoot)
    {
        if (visualRoot == null) return;
        foreach (Camera camera in visualRoot.GetComponentsInChildren<Camera>(true)) camera.enabled = false;
        foreach (AudioListener listener in visualRoot.GetComponentsInChildren<AudioListener>(true)) listener.enabled = false;
    }

    void OnDestroy()
    {
        for (int i = 0; i < runtimeMaterials.Count; i++)
            if (runtimeMaterials[i] != null) Destroy(runtimeMaterials[i]);
    }
}
