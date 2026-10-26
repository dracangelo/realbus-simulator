using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Places glowing chevron arrows in world-space along the active route path
/// so the driver always has a clear visual guide without needing a minimap.
///
/// Design goals:
///   - One arrow every <see cref="arrowSpacingMeters"/> along the path ahead.
///   - Arrows fade in from 0 alpha past <see cref="fadeInMeters"/> and fade
///     out toward <see cref="fadeOutMeters"/> ahead.
///   - A larger pulsing green beacon marks the upcoming stop position.
///   - All geometry is built once and reused; only positions/rotations update.
/// </summary>
[DefaultExecutionOrder(100)]
public class RouteArrowOverlay : MonoBehaviour
{
    // ── Configuration ───────────────────────────────────────────────────

    [Header("Arrow Strip")]
    public float arrowSpacingMeters = 18f;
    public int   maxArrows          = 14;
    public float fadeInMeters       = 5f;
    public float fadeOutMeters      = 240f;
    public float hoverHeight        = 0.28f;
    public float arrowScale         = 1.8f;

    [Header("Next-Stop Beacon")]
    public float beaconScale       = 2.8f;
    public float beaconHoverHeight = 2.2f;

    [Header("Palette")]
    public Color routeArrowColor  = new Color(1f,    0.64f, 0.08f, 1f);
    public Color beaconArrowColor = new Color(0.18f, 1f,    0.55f, 1f);

    // ── Runtime ─────────────────────────────────────────────────────────

    struct Arrow
    {
        public GameObject     root;
        public MeshRenderer   rend;
        public MaterialPropertyBlock mpb;
    }

    Arrow[] pool;
    Arrow   beacon;

    BusController  bus;
    MissionManager mission;

    readonly List<Vector3> cachedPath = new List<Vector3>(256);
    int  lastPathVersion = -1;
    bool overlayReady;

    static readonly int PropColor    = Shader.PropertyToID("_Color");
    static readonly int PropEmission = Shader.PropertyToID("_EmissionColor");

    // ── Singleton ───────────────────────────────────────────────────────

    public static RouteArrowOverlay Instance { get; private set; }

    /// <summary>True when at least one route chevron is on screen.</summary>
    public bool HasVisibleArrows
    {
        get
        {
            if (pool == null) return false;
            foreach (var arrow in pool)
                if (arrow.root != null && arrow.root.activeSelf) return true;
            return false;
        }
    }

    /// <summary>True when the next-stop beacon is on screen.</summary>
    public bool BeaconVisible => beacon.root != null && beacon.root.activeSelf;

    /// <summary>
    /// True when a route is active and the overlay is actually drawing part of it.
    /// The full-route smoke run samples this for the whole journey: the old defect
    /// was not a crash but markers that silently stopped being drawn after the first
    /// route or the first served stop.
    /// </summary>
    public bool IsGuiding => overlayReady && mission != null && mission.routeActive &&
        mission.currentRoute != null && (HasVisibleArrows || BeaconVisible);

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static RouteArrowOverlay EnsureExists()
    {
        if (Instance != null) return Instance;
        var ex = FindAnyObjectByType<RouteArrowOverlay>();
        if (ex != null) return ex;
        return new GameObject("RouteArrowOverlay").AddComponent<RouteArrowOverlay>();
    }

    // ── Lifecycle ───────────────────────────────────────────────────────

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    float bindRetryTimer;

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
        DestroyPool();
    }

    /// <summary>
    /// Binds on demand rather than once at startup. The overlay is created before
    /// the gameplay scene exists and survives scene loads, so a single 45-second
    /// startup coroutine meant that any route started after that window - or any
    /// route whose bus was replaced - got no markers at all.
    /// </summary>
    bool TryBind()
    {
        if (bus == null) bus = FindAnyObjectByType<BusController>();
        if (mission == null) mission = MissionManager.Instance;
        if (bus == null) return false;
        if (pool == null || beacon.root == null) BuildPool();
        overlayReady = true;
        return true;
    }

    void Update()
    {
        if (!overlayReady)
        {
            // Cheap retry: the bus does not exist until a route is loaded.
            bindRetryTimer += Time.unscaledDeltaTime;
            if (bindRetryTimer < 0.5f) return;
            bindRetryTimer = 0f;
            if (!TryBind()) return;
        }

        // The overlay survives scene loads (DontDestroyOnLoad) but the mission,
        // the bus and the route behind them do not. Holding the references from
        // the first route is why the markers worked for the opening route and
        // then silently disappeared - or kept guiding the previous route - once
        // the player loaded another one. Re-resolve whenever the bound objects
        // are gone.
        if (bus == null) bus = FindAnyObjectByType<BusController>();
        if (mission == null || mission.currentRoute == null)
        {
            var resolved = MissionManager.Instance;
            if (resolved != mission)
            {
                mission = resolved;
                lastPathVersion = -1;
                cachedPath.Clear();
            }
        }
        if (bus == null || mission == null) { HideAll(); if (beacon.root != null) beacon.root.SetActive(false); return; }

        // No active route (menu, free drive, finished route): the markers belong to
        // the navigator and must not linger over an old leg.
        if (!mission.routeActive || mission.currentRoute == null)
        {
            HideAll();
            if (beacon.root != null) beacon.root.SetActive(false);
            return;
        }

        RefreshPath();
        PlaceArrows();
        PlaceBeacon();
    }

    // ── Pool ────────────────────────────────────────────────────────────

    void BuildPool()
    {
        DestroyPool();
        pool   = new Arrow[maxArrows];
        beacon = BuildArrow("RouteBeacon", beaconScale, beaconArrowColor);
        for (int i = 0; i < maxArrows; i++)
            pool[i] = BuildArrow("RouteArrow_" + i, arrowScale, routeArrowColor);
    }

    void DestroyPool()
    {
        if (pool != null)
        {
            foreach (var a in pool)
                if (a.root != null) Destroy(a.root);
            pool = null;
        }
        if (beacon.root != null) { Destroy(beacon.root); beacon = default; }
    }

    Arrow BuildArrow(string arrowName, float scale, Color color)
    {
        var root = new GameObject(arrowName);
        root.transform.SetParent(transform, false);

        var mf = root.AddComponent<MeshFilter>();
        mf.mesh = BuildChevronMesh();

        var rend = root.AddComponent<MeshRenderer>();
        var mat  = new Material(Shader.Find("Standard"))
        {
            name        = "RouteArrowMat",
            renderQueue = 3000
        };
        // Alpha-blend so arrows fade smoothly.
        mat.SetFloat("_Mode",     3f);
        mat.SetInt("_SrcBlend",  (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend",  (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite",    0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.EnableKeyword("_EMISSION");
        // Make albedo dark so the visual is purely emissive.
        mat.color = new Color(0.05f, 0.05f, 0.05f, 1f);
        mat.SetColor("_EmissionColor", color * 2.4f);
        rend.material = mat;
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        rend.receiveShadows    = false;

        var mpb = new MaterialPropertyBlock();
        mpb.SetColor(PropColor,    color);
        mpb.SetColor(PropEmission, color * 2.4f);
        rend.SetPropertyBlock(mpb);

        root.transform.localScale = Vector3.one * scale;
        root.SetActive(false);
        return new Arrow { root = root, rend = rend, mpb = mpb };
    }

    // ── Chevron mesh ────────────────────────────────────────────────────

    static Mesh BuildChevronMesh()
    {
        // Flat chevron pointing in +Z, lying in XZ plane (Y=0).
        //
        //          tip (0, 0, +ft)
        //         / \
        //  L-outer   R-outer
        //     |   notch  |
        //  L-inner     R-inner
        //
        float ft = 0.45f;   // tip forward
        float bk = -0.35f;  // back edge Z
        float ow = 0.50f;   // outer half-width
        float iw = 0.12f;   // inner half-width (notch)

        var verts = new Vector3[]
        {
            new Vector3(  0f,  0f,  ft),   // 0  tip
            new Vector3(-ow,   0f,  bk),   // 1  outer back-left
            new Vector3(-iw,   0f,  bk),   // 2  inner back-left
            new Vector3( iw,   0f,  bk),   // 3  inner back-right
            new Vector3( ow,   0f,  bk),   // 4  outer back-right
        };

        var tris = new int[]
        {
            0, 1, 2,   // left wing
            0, 2, 3,   // centre fill
            0, 3, 4,   // right wing
        };

        var normals = new Vector3[verts.Length];
        for (int i = 0; i < normals.Length; i++) normals[i] = Vector3.up;

        var mesh = new Mesh { name = "RouteChevron" };
        mesh.vertices  = verts;
        mesh.triangles = tris;
        mesh.normals   = normals;
        mesh.RecalculateBounds();
        return mesh;
    }

    // ── Path cache ──────────────────────────────────────────────────────

    void RefreshPath()
    {
        if (mission == null) return;
        var src = mission.ActiveGuidancePathPoints;
        if (src == null || src.Count < 2)
        {
            // A rebuild can briefly hand back no points (GPS or tile data not
            // ready yet). Keep the last valid line and its version instead of
            // blanking the navigator: the next rebuild replaces it. A route
            // change is handled above by re-binding the mission and clearing.
            return;
        }

        // GuidancePathVersion changes on every rebuild, including the rebuild that
        // happens when a stop is served or a diversion is cleared. Caching by the
        // point count treated the next leg as unchanged whenever it happened to
        // have the same number of points, so the chevrons kept leading the player
        // back down the leg they had just driven.
        int version = mission.GuidancePathVersion;
        if (version == lastPathVersion) return;
        lastPathVersion = version;

        cachedPath.Clear();
        for (int i = 0; i < src.Count; i++) cachedPath.Add(src[i]);
    }

    // ── Placement ───────────────────────────────────────────────────────

    void PlaceArrows()
    {
        if (cachedPath.Count < 2 || pool == null) { HideAll(); return; }

        Vector3 busPos = bus.transform.position;
        int nearest = FindNearestSegment(busPos, out float nearestT);
        if (nearest < 0) { HideAll(); return; }

        int   arrowIdx  = 0;
        float accum     = 0f;
        float nextDist  = arrowSpacingMeters * 0.5f;

        // Start on the nearest segment itself. Starting one segment earlier (the
        // old behaviour) applied the projection t from the nearest segment to the
        // segment before it, so near the end of a leg the first "ahead" arrow was
        // actually behind the bus and the remaining arrows ran out before the
        // stop. That read as the navigator disappearing on the final approach.
        for (int i = Mathf.Max(0, nearest);
             i < cachedPath.Count - 1 && arrowIdx < pool.Length; i++)
        {
            Vector3 segA   = cachedPath[i];
            Vector3 segB   = cachedPath[i + 1];
            float   segLen = Vector3.Distance(segA, segB);
            if (segLen < 0.01f) continue;

            float t0        = (i == nearest) ? nearestT : 0f;
            float available = segLen * (1f - t0);

            while (available >= nextDist - accum && arrowIdx < pool.Length)
            {
                float   frac     = t0 + (nextDist - accum) / segLen;
                Vector3 wPos     = Vector3.Lerp(segA, segB, Mathf.Clamp01(frac));
                Vector3 dir      = (segB - segA).normalized;
                float   dist     = Vector3.Distance(wPos, busPos);
                float   alpha    = ComputeAlpha(dist);

                PlaceArrowAt(ref pool[arrowIdx], wPos, dir, routeArrowColor, alpha, arrowScale);
                arrowIdx++;
                nextDist += arrowSpacingMeters;
            }
            accum += available;
        }

        for (int i = arrowIdx; i < pool.Length; i++)
            pool[i].root.SetActive(false);
    }

    void PlaceBeacon()
    {
        if (beacon.root == null) return;
        if (!TryGetNextStopWorld(out Vector3 stopPos)) { beacon.root.SetActive(false); return; }

        float pulse = 0.5f + 0.5f * Mathf.Sin(Time.time * 3.2f);
        Color glow  = beaconArrowColor * (2f + pulse * 1.6f);

        Vector3 dir = bus != null ? bus.transform.forward : Vector3.forward;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.01f) dir = Vector3.forward;

        stopPos.y = hoverHeight + beaconHoverHeight;

        beacon.root.transform.SetPositionAndRotation(
            stopPos,
            Quaternion.LookRotation(dir, Vector3.up) * Quaternion.Euler(90f, 0f, 0f));
        beacon.root.transform.localScale = Vector3.one * beaconScale;

        beacon.mpb.SetColor(PropColor,    beaconArrowColor);
        beacon.mpb.SetColor(PropEmission, glow);
        beacon.rend.SetPropertyBlock(beacon.mpb);
        beacon.root.SetActive(true);
    }

    void PlaceArrowAt(ref Arrow a, Vector3 pos, Vector3 dir, Color baseColor, float alpha, float scale)
    {
        if (alpha <= 0.005f) { a.root.SetActive(false); return; }

        pos.y = hoverHeight;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.01f) dir = Vector3.forward;

        a.root.transform.SetPositionAndRotation(
            pos,
            Quaternion.LookRotation(dir, Vector3.up) * Quaternion.Euler(90f, 0f, 0f));
        a.root.transform.localScale = Vector3.one * scale;

        float shimmer = 0.35f * Mathf.Sin(Time.time * 2f + pos.x * 0.5f);
        Color c       = baseColor; c.a = alpha;
        Color glow    = baseColor * (1.6f + shimmer);
        a.mpb.SetColor(PropColor,    c);
        a.mpb.SetColor(PropEmission, glow * alpha);
        a.rend.SetPropertyBlock(a.mpb);
        a.root.SetActive(true);
    }

    void HideAll()
    {
        if (pool == null) return;
        foreach (var a in pool)
            if (a.root != null) a.root.SetActive(false);
    }

    float ComputeAlpha(float dist)
    {
        if (dist < fadeInMeters)  return dist / fadeInMeters;
        if (dist > fadeOutMeters) return Mathf.Max(0f, 1f - (dist - fadeOutMeters) / 40f);
        return 1f;
    }

    int FindNearestSegment(Vector3 pos, out float t)
    {
        t = 0f;
        int   best    = -1;
        float bestSqr = float.MaxValue;

        for (int i = 0; i < cachedPath.Count - 1; i++)
        {
            Vector3 a  = cachedPath[i]; a.y = pos.y;
            Vector3 b  = cachedPath[i + 1]; b.y = pos.y;
            Vector3 ab = b - a;
            float  len2 = ab.sqrMagnitude;
            float  proj = len2 < 0.0001f ? 0f
                        : Mathf.Clamp01(Vector3.Dot(pos - a, ab) / len2);
            float  sqr  = (a + ab * proj - pos).sqrMagnitude;
            if (sqr < bestSqr) { bestSqr = sqr; best = i; t = proj; }
        }
        return best;
    }

    bool TryGetNextStopWorld(out Vector3 world)
    {
        world = Vector3.zero;
        if (mission == null || mission.currentRoute == null) return false;
        var route = mission.currentRoute;
        if (route.stops == null || route.stops.Length == 0) return false;

        int idx  = Mathf.Clamp(mission.currentStopIndex, 0, route.stops.Length - 1);
        var stop = route.stops[idx];
        var gps  = GPSManager.Instance;
        if (gps == null) return false;

        world = gps.GpsToWorld(stop.latitude, stop.longitude);
        return true;
    }
}
