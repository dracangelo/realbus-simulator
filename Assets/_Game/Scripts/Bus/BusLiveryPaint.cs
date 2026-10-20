using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gives the player bus a painted transit livery instead of the flat green
/// shell the imported-model repair pass used to leave on every untextured body
/// panel. The six <see cref="LiveryData"/> zones are pushed into
/// <c>RealBus/BusPaint</c>, which paints them in the bus's own local frame, so
/// the same livery works on every imported mesh regardless of its UV atlas.
///
/// The component lives on the bus root (next to <see cref="LiveryEditor"/>) and
/// owns one material instance. Only the body slots are swapped; glass, lamps,
/// wheels and the interior keep the materials the repair pass finished.
/// </summary>
[DefaultExecutionOrder(200)]
public class BusLiveryPaint : MonoBehaviour
{
    static readonly int Zone0 = Shader.PropertyToID("_Zone0");
    static readonly int Zone1 = Shader.PropertyToID("_Zone1");
    static readonly int Zone2 = Shader.PropertyToID("_Zone2");
    static readonly int Zone3 = Shader.PropertyToID("_Zone3");
    static readonly int Zone4 = Shader.PropertyToID("_Zone4");
    static readonly int Zone5 = Shader.PropertyToID("_Zone5");
    static readonly int Smoothness = Shader.PropertyToID("_Smoothness");
    static readonly int Metallic = Shader.PropertyToID("_Metallic");
    static readonly int LiveryOrigin = Shader.PropertyToID("_LiveryOrigin");
    static readonly int LiveryRight = Shader.PropertyToID("_LiveryRight");
    static readonly int LiveryForward = Shader.PropertyToID("_LiveryForward");
    static readonly int LiveryUp = Shader.PropertyToID("_LiveryUp");
    static readonly int BusWidth = Shader.PropertyToID("_BusWidth");
    static readonly int BusLength = Shader.PropertyToID("_BusLength");
    static readonly int BusHeight = Shader.PropertyToID("_BusHeight");

    struct Slot
    {
        public Renderer renderer;
        public int materialIndex;
    }

    readonly List<Slot> slots = new List<Slot>();
    Material paintMaterial;
    Shader paintShader;
    Vector3 localOrigin;
    float localHeight = 3.4f;
    float localLength = 12f;
    float localWidth = 2.55f;
    bool bound;

    public bool HasPaint => bound && paintMaterial != null;
    public Material PaintMaterial => paintMaterial;
    public int PaintedSlotCount => slots.Count;

    /// <summary>
    /// Swaps the body slots for the livery material and remembers the body's
    /// local bounds. Safe to call again when the selected bus changes.
    /// </summary>
    public void Bind(Transform visualRoot, LiveryData data, string busId)
    {
        // Hold on to the old material until the new slots are known. Releasing
        // it first would leave a textured model (which records no fallback body
        // slots) with destroyed materials the moment the garage re-binds.
        Material previous = paintMaterial;
        paintMaterial = null;
        slots.Clear();
        bound = false;

        if (visualRoot == null)
            visualRoot = transform;

        var repair = GetComponent<ImportedVehicleVisualRepair>();
        if (repair != null)
            repair.RepairNow(visualRoot);

        CollectSlots(visualRoot, repair);
        if (slots.Count == 0)
        {
            // Nothing could be identified as bodywork. Leaving the repaired
            // bus untouched is better than painting the glass and wheels.
            paintMaterial = previous;
            bound = previous != null;
            return;
        }

        paintShader = Shader.Find("RealBus/BusPaint");
        if (paintShader == null)
            paintShader = BuiltInShaders.Lit;

        paintMaterial = new Material(paintShader)
        {
            name = string.IsNullOrWhiteSpace(busId) ? "BusPaint" : "BusPaint_" + busId
        };
        if (paintMaterial.HasProperty(Smoothness)) paintMaterial.SetFloat(Smoothness, 0.62f);
        if (paintMaterial.HasProperty(Metallic)) paintMaterial.SetFloat(Metallic, 0.16f);

        for (int i = 0; i < slots.Count; i++)
        {
            Slot slot = slots[i];
            if (slot.renderer == null) continue;
            Material[] materials = slot.renderer.sharedMaterials;
            if (slot.materialIndex < 0 || slot.materialIndex >= materials.Length) continue;
            materials[slot.materialIndex] = paintMaterial;
            slot.renderer.sharedMaterials = materials;
        }

        // The renderers now point at the new material, so the previous instance
        // is no longer referenced by the bus.
        DestroyMaterial(previous);

        MeasureBody();
        Apply(data);
        RefreshFrame();
        bound = true;
    }

    /// <summary>Pushes a new palette into the live material.</summary>
    public void Apply(LiveryData data)
    {
        if (paintMaterial == null) return;

        Color roof = ZoneColor(data, 0, new Color(0.10f, 0.13f, 0.18f));
        Color front = ZoneColor(data, 1, new Color(0.72f, 0.12f, 0.10f));
        Color sideA = ZoneColor(data, 2, front);
        Color sideB = ZoneColor(data, 3, sideA);
        Color rear = ZoneColor(data, 4, roof);
        Color accent = ZoneColor(data, 5, new Color(0.92f, 0.62f, 0.06f));

        paintMaterial.SetColor(Zone0, ToShader(roof));
        paintMaterial.SetColor(Zone1, ToShader(front));
        paintMaterial.SetColor(Zone2, ToShader(sideA));
        paintMaterial.SetColor(Zone3, ToShader(sideB));
        paintMaterial.SetColor(Zone4, ToShader(rear));
        paintMaterial.SetColor(Zone5, ToShader(accent));

        // Keep the conventional albedo slot coherent for tools and for the
        // Standard fallback, which has no zones.
        if (paintMaterial.HasProperty("_Color"))
            paintMaterial.SetColor("_Color", ToShader(sideA));

        if (paintShader == null || paintShader.name != "RealBus/BusPaint")
        {
            paintMaterial.color = ToShader(sideA);
            if (paintMaterial.HasProperty("_Glossiness")) paintMaterial.SetFloat("_Glossiness", 0.55f);
        }
    }

    /// <summary>
    /// The pose-dependent uniforms are refreshed after physics and after the
    /// renderer has interpolated, so the bands do not lag behind the body on a
    /// bump. One material write per frame is cheap.
    /// </summary>
    public void RefreshFrame()
    {
        if (paintMaterial == null) return;

        Transform frame = transform;
        paintMaterial.SetVector(LiveryOrigin, frame.TransformPoint(localOrigin));
        paintMaterial.SetVector(LiveryRight, frame.right);
        paintMaterial.SetVector(LiveryForward, frame.forward);
        paintMaterial.SetVector(LiveryUp, frame.up);
        if (paintMaterial.HasProperty(BusWidth)) paintMaterial.SetFloat(BusWidth, localWidth);
        if (paintMaterial.HasProperty(BusLength)) paintMaterial.SetFloat(BusLength, localLength);
        if (paintMaterial.HasProperty(BusHeight)) paintMaterial.SetFloat(BusHeight, localHeight);
    }

    void LateUpdate()
    {
        RefreshFrame();
    }

    void OnDestroy()
    {
        // Destroying a material from inside OnDestroy is not allowed in the
        // editor. The material is a runtime object with no asset backing, so
        // letting the editor clean it up is safe; in play mode release it.
        if (paintMaterial != null && Application.isPlaying)
            Destroy(paintMaterial);
        paintMaterial = null;
        slots.Clear();
        bound = false;
    }

    static void DestroyMaterial(Material material)
    {
        if (material == null) return;
        if (Application.isPlaying) Destroy(material);
        else DestroyImmediate(material);
    }

    void CollectSlots(Transform visualRoot, ImportedVehicleVisualRepair repair)
    {
        if (repair != null && repair.BodyPaintSlots.Count > 0)
        {
            for (int i = 0; i < repair.BodyPaintSlots.Count; i++)
                AddSlot(repair.BodyPaintSlots[i].renderer, repair.BodyPaintSlots[i].materialIndex);
            return;
        }

        // A model with no recorded fallback slots may still expose body panels
        // by name (textured packs, or the primitive fallback bus). Match those
        // before falling back to the largest opaque renderer.
        Renderer[] renderers = visualRoot.GetComponentsInChildren<Renderer>(true);
        for (int r = 0; r < renderers.Length; r++)
        {
            Renderer renderer = renderers[r];
            if (renderer == null || IsWheel(renderer.name)) continue;
            Material[] materials = renderer.sharedMaterials;
            for (int m = 0; m < materials.Length; m++)
            {
                Material material = materials[m];
                if (material == null) continue;
                string materialName = material.name.ToLowerInvariant();
                if (IsNonBody(materialName)) continue;

                // Never replace a material that carries real artwork: the
                // livery shader is UV-independent and would erase the source
                // texture. Only untextured panels, panels we already painted,
                // and explicitly named untextured body panels are safe.
                bool alreadyPainted = material.shader != null && material.shader.name == "RealBus/BusPaint";
                bool untextured = material.mainTexture == null;
                bool namedBody = untextured &&
                    (ContainsBodyWord(renderer.name) || ContainsBodyWord(materialName));
                bool neutralBody = untextured && TrafficLivery.IsBodyPaint(material);
                if (alreadyPainted || namedBody || neutralBody)
                    AddSlot(renderer, m);
            }
        }
    }

    void AddSlot(Renderer renderer, int materialIndex)
    {
        if (renderer == null || materialIndex < 0) return;
        for (int i = 0; i < slots.Count; i++)
            if (slots[i].renderer == renderer && slots[i].materialIndex == materialIndex) return;
        slots.Add(new Slot { renderer = renderer, materialIndex = materialIndex });
    }

    void MeasureBody()
    {
        bool has = false;
        Bounds local = new Bounds(Vector3.zero, Vector3.zero);
        Matrix4x4 busToLocal = transform.worldToLocalMatrix;

        for (int i = 0; i < slots.Count; i++)
        {
            Renderer renderer = slots[i].renderer;
            if (renderer == null) continue;
            Matrix4x4 toBus = busToLocal * renderer.transform.localToWorldMatrix;

            MeshFilter filter = renderer.GetComponent<MeshFilter>();
            if (filter == null || filter.sharedMesh == null) continue;
            Bounds mesh = filter.sharedMesh.bounds;
            Vector3 centre = mesh.center;
            Vector3 ext = mesh.extents;
            for (int corner = 0; corner < 8; corner++)
            {
                Vector3 p = centre + new Vector3(
                    (corner & 1) == 0 ? -ext.x : ext.x,
                    (corner & 2) == 0 ? -ext.y : ext.y,
                    (corner & 4) == 0 ? -ext.z : ext.z);
                Vector3 localPoint = toBus.MultiplyPoint3x4(p);
                if (!has) { local = new Bounds(localPoint, Vector3.zero); has = true; }
                else local.Encapsulate(localPoint);
            }
        }

        if (!has)
        {
            localOrigin = Vector3.zero;
            localHeight = 3.4f;
            localLength = 12f;
            localWidth = 2.55f;
            return;
        }

        localOrigin = new Vector3(local.center.x, local.min.y, local.center.z);
        localHeight = Mathf.Max(0.6f, local.size.y);
        localLength = Mathf.Max(1.5f, local.size.z);
        localWidth = Mathf.Max(0.6f, local.size.x);
    }

    static Color ZoneColor(LiveryData data, int index, Color fallback)
    {
        if (data == null || data.zoneColors == null || index >= data.zoneColors.Length)
            return fallback;
        return data.zoneColors[index];
    }

    static Color ToShader(Color color)
    {
        return QualitySettings.activeColorSpace == ColorSpace.Linear ? color.linear : color;
    }

    static bool IsWheel(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        string lower = name.ToLowerInvariant();
        if (lower == "fl" || lower == "fr" || lower == "bl" || lower == "br") return true;
        return lower.Contains("wheel") || lower.Contains("tyre") || lower.Contains("tire") || lower.Contains("rim");
    }

    static bool IsNonBody(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        return name.Contains("glass") || name.Contains("kaca") || name.Contains("window") ||
               name.Contains("light") || name.Contains("lamp") || name.Contains("chrome") ||
               name.Contains("tyre") || name.Contains("tire") || name.Contains("wheel") ||
               name.Contains("interior") || name.Contains("dasbor") || name.Contains("dashboard") ||
               name.Contains("kursi") || name.Contains("seat") || name.Contains("lantai") ||
               name.Contains("floor") || name.Contains("plafon") || name.Contains("curtain") ||
               name.Contains("gorden") || name.Contains("rim") || name.Contains("ban");
    }

    static bool ContainsBodyWord(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        string lower = name.ToLowerInvariant();
        return lower.Contains("body") || lower.Contains("bodi") || lower.Contains("karoseri") ||
               lower.Contains("bodyshell") || lower.Contains("shell");
    }
}
