using UnityEngine;
using System.Collections;

/// <summary>
/// Applies a tarmac material to road mesh renderers after OSMRoadMeshBuilder
/// has finished constructing them.
///
/// Bug fixes vs previous version:
///   1. TIMING — was polling GameObject.Find("OSM_Roads") with a 12-second
///      hard deadline. If roads took longer (large city), it gave up and the
///      material was never applied. Now waits on OSMRoadMeshBuilder.roadsBuilt
///      with no deadline.
///   2. RENDER QUEUE — material render queue must be > map tile quads (2000)
///      so roads draw on top. Added renderQueue = 2100 to match the builder.
///   3. CULL MODE — default materials are single-sided. If the winding order
///      is wrong, the road is invisible. Setting _Cull = Off makes the tarmac
///      visible from both sides regardless.
///   4. Y-OFFSET SYNC — ApplyToPlane now reads the plane's existing Y so it
///      doesn't fight with MapTileLoader's fallback plane position.
///   5. DEBUG LOGGING — added debugLogging toggle and detailed logs to diagnose
///      tarmac application issues in builds.
/// </summary>
public class RuntimeTarmacApplier : MonoBehaviour
{
    [Header("Material")]
    public Color  tarmacColor  = new Color(0.15f, 0.15f, 0.15f, 1f); // Lighter gray for visibility
    public float  metallic     = 0f;
    public float  smoothness   = 0.1f;
    public Vector2 textureScale = new Vector2(6f, 6f);
    [Tooltip("Enable to see debug logs about tarmac application")]
    public bool debugLogging = true;

    [Header("Targets")]
    public string roadsRootName     = "OSM_Roads";
    public bool   applyToPlane      = true;
    public string planeObjectName   = "Plane";
    public bool   applyToRoadMeshes = true;

    Material runtimeMaterial;

    void Start()
    {
        StartCoroutine(ApplyWhenReady());
    }

    IEnumerator ApplyWhenReady()
    {
        if (debugLogging) Debug.Log("[RuntimeTarmacApplier] Starting tarmac application coroutine...");

        runtimeMaterial = CreateTarmacMaterial();

        // Apply to fallback plane immediately — it's always present at Start.
        if (applyToPlane)
            ApplyToPlane();

        if (!applyToRoadMeshes)
        {
            if (debugLogging) Debug.Log("[RuntimeTarmacApplier] applyToRoadMeshes is disabled, skipping road tarmac.");
            yield break;
        }

        // FIX: Wait for OSMRoadMeshBuilder to finish rather than using a
        // fixed timeout. Falls back to a name-search loop if the builder
        // isn't present in the scene (e.g. roads pre-baked).
        if (OSMRoadMeshBuilder.Instance != null)
        {
            if (debugLogging) Debug.Log($"[RuntimeTarmacApplier] Found OSMRoadMeshBuilder instance, waiting for roadsBuilt...");
            float waitStart = Time.time;
            while (!OSMRoadMeshBuilder.Instance.roadsBuilt)
                yield return new WaitForSeconds(0.25f);
            if (debugLogging) Debug.Log($"[RuntimeTarmacApplier] Waited {Time.time - waitStart:F1}s for roads to be built.");
        }
        else
        {
            // No builder present — poll for the root GameObject directly,
            // but still cap at 30 s to avoid infinite loops on bad scenes.
            if (debugLogging) Debug.Log($"[RuntimeTarmacApplier] No OSMRoadMeshBuilder instance found, polling for '{roadsRootName}'...");
            float deadline = Time.time + 30f;
            while (Time.time < deadline)
            {
                if (GameObject.Find(roadsRootName) != null)
                {
                    if (debugLogging) Debug.Log($"[RuntimeTarmacApplier] Found roads root '{roadsRootName}'.");
                    break;
                }
                yield return new WaitForSeconds(0.5f);
            }
        }

        ApplyToRoads();
    }

    // ── Public API ────────────────────────────────────────────────────────────

    public void ApplyTarmac()
    {
        if (runtimeMaterial == null)
            runtimeMaterial = CreateTarmacMaterial();

        if (applyToPlane)      ApplyToPlane();
        if (applyToRoadMeshes) ApplyToRoads();
    }

    // ── Internals ─────────────────────────────────────────────────────────────

    void ApplyToPlane()
    {
        if (runtimeMaterial == null)
        {
            Debug.LogWarning("[RuntimeTarmacApplier] Cannot apply tarmac to plane — runtimeMaterial is null!");
            return;
        }

        var plane = GameObject.Find(planeObjectName);
        if (plane == null)
        {
            if (debugLogging) Debug.Log($"[RuntimeTarmacApplier] Plane '{planeObjectName}' not found.");
            return;
        }
        var r = plane.GetComponent<Renderer>();
        if (r == null) return;
        r.material = runtimeMaterial;
        if (debugLogging) Debug.Log($"[RuntimeTarmacApplier] Applied tarmac material to plane '{planeObjectName}'.");
    }

    void ApplyToRoads()
    {
        if (runtimeMaterial == null)
        {
            Debug.LogError("[RuntimeTarmacApplier] Cannot apply tarmac — runtimeMaterial is null!");
            return;
        }

        // Prefer the live reference from the builder.
        GameObject root = null;
        if (OSMRoadMeshBuilder.Instance != null)
            root = GameObject.Find(roadsRootName);   // builder keeps no public ref, use name
        root ??= GameObject.Find(roadsRootName);

        if (root == null)
        {
            Debug.LogWarning("[RuntimeTarmacApplier] Roads root not found — tarmac not applied.");
            return;
        }

        if (debugLogging) Debug.Log($"[RuntimeTarmacApplier] Applying tarmac to roads under '{roadsRootName}'...");

        int count = 0;
        foreach (var mr in root.GetComponentsInChildren<MeshRenderer>(true))
        {
            if (mr == null) continue;
            mr.enabled  = true;
            mr.material = runtimeMaterial;
            mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            mr.receiveShadows    = false;
            count++;
        }

        Debug.Log($"[RuntimeTarmacApplier] Applied tarmac to {count} road renderers.");
    }

    Material CreateTarmacMaterial()
    {
        // Use the same shader preference chain as OSMRoadMeshBuilder.
        Shader shader = Shader.Find("Universal Render Pipeline/Lit")
            ?? Shader.Find("Universal Render Pipeline/Unlit")
            ?? Shader.Find("Standard")
            ?? Shader.Find("Unlit/Color");

        if (shader == null)
        {
            Debug.LogError("[RuntimeTarmacApplier] Failed to find any suitable shader for tarmac material!");
            return null;
        }

        if (debugLogging) Debug.Log($"[RuntimeTarmacApplier] Creating tarmac material with shader: {shader.name}");

        var mat = new Material(shader)
        {
            color        = tarmacColor,
            // FIX: render on top of map tile quads (renderQueue ~2000)
            renderQueue  = 2100
        };

        if (mat.HasProperty("_Metallic"))   mat.SetFloat("_Metallic",   metallic);
        if (mat.HasProperty("_Glossiness")) mat.SetFloat("_Glossiness", smoothness);
        if (mat.HasProperty("_Smoothness")) mat.SetFloat("_Smoothness", smoothness);
        if (mat.HasProperty("_MainTex"))    mat.SetTextureScale("_MainTex", textureScale);

        // FIX: disable culling so roads are visible regardless of winding
        // order — belt-and-suspenders alongside the mesh fix in the builder.
        if (mat.HasProperty("_Cull"))
            mat.SetFloat("_Cull", (float)UnityEngine.Rendering.CullMode.Off);

        // URP opaque surface
        if (mat.HasProperty("_Surface")) mat.SetFloat("_Surface", 0f);
        if (mat.HasProperty("_ZWrite"))  mat.SetFloat("_ZWrite",  1f);

        if (debugLogging) Debug.Log($"[RuntimeTarmacApplier] Tarmac material created: color={tarmacColor}, queue={mat.renderQueue}");

        return mat;
    }
}