using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BusCameraPreset { Exterior, Cockpit, WideChase, DoorView }

public class MobileCameraController : MonoBehaviour
{
    public static MobileCameraController Instance { get; private set; }
    public BusCameraPreset CurrentPreset { get; private set; }
    Camera targetCamera, mirrorCamera;
    BusController bus;
    RenderTexture mirrorTexture;
    Canvas mirrorCanvas;
    Vector3 targetLocalPosition;
    Quaternion targetLocalRotation;
    float baseFieldOfView;
    Bounds busLocalBounds;
    Coroutine binding;
    readonly RaycastHit[] cameraHits = new RaycastHit[12];

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static MobileCameraController EnsureExists()
    {
        if (Instance != null) return Instance;
        MobileCameraController existing = FindFirstObjectByType<MobileCameraController>();
        return existing != null ? existing : new GameObject("MobileCameraController").AddComponent<MobileCameraController>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start() { Rebind(); }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { Rebind(); }
    void Rebind()
    {
        if (binding != null) StopCoroutine(binding);
        ReleaseMirror();
        bus = null;
        targetCamera = null;
        binding = StartCoroutine(BindWhenReady());
    }
    void OnDestroy() { if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded; ReleaseMirror(); }

    IEnumerator BindWhenReady()
    {
        // SceneBootstrap replaces the serialized bus during Start. Binding in
        // sceneLoaded otherwise retains the disabled bus scheduled for removal.
        yield return null;
        for (int frame = 0; frame < 120 && (bus == null || targetCamera == null); frame++)
        {
            bus = MissionManager.Instance != null ? MissionManager.Instance.busController : null;
            if (bus == null || !bus.isActiveAndEnabled) bus = FindAnyObjectByType<BusController>();
            targetCamera = Camera.main != null ? Camera.main : FindFirstObjectByType<Camera>();
            if (bus == null || targetCamera == null) yield return null;
        }
        binding = null;
        if (bus == null || targetCamera == null) { SetMirrorVisible(false); yield break; }
        busLocalBounds = CalculateLocalVisualBounds(bus.transform);
        baseFieldOfView = targetCamera.fieldOfView; CurrentPreset = (BusCameraPreset)Mathf.Clamp(PlayerPrefs.GetInt("RealBus.CameraPreset", 0), 0, 3);
        ApplyPreset(true); EnsureMirror();
    }

    void LateUpdate()
    {
        if (bus == null || !bus.isActiveAndEnabled || targetCamera == null)
        {
            if (binding == null && FindAnyObjectByType<BusController>() != null) Rebind();
            return;
        }
        if (PhotoModeController.Instance != null && PhotoModeController.Instance.IsPhotoModeActive) { SetMirrorVisible(false); return; }
        var keyboard = UnityEngine.InputSystem.Keyboard.current;
        if (keyboard != null && keyboard.cKey.wasPressedThisFrame) CycleCamera();
        Vector3 desired = ResolveCollisionSafePosition(targetLocalPosition);
        targetCamera.transform.localPosition = Vector3.Lerp(targetCamera.transform.localPosition, desired, 1f - Mathf.Exp(-8f * Time.unscaledDeltaTime));
        targetCamera.transform.localRotation = Quaternion.Slerp(targetCamera.transform.localRotation, targetLocalRotation, 1f - Mathf.Exp(-9f * Time.unscaledDeltaTime));
        targetCamera.fieldOfView = Mathf.Lerp(targetCamera.fieldOfView, baseFieldOfView + Mathf.InverseLerp(20f, 90f, bus.currentSpeedKmh) * 5f, Time.unscaledDeltaTime * 3f);
        SetMirrorVisible(CurrentPreset == BusCameraPreset.Cockpit && QualitySettings.GetQualityLevel() > 0);
    }

    public void CycleCamera()
    {
        CurrentPreset = (BusCameraPreset)(((int)CurrentPreset + 1) % 4); PlayerPrefs.SetInt("RealBus.CameraPreset", (int)CurrentPreset); ApplyPreset(false);
        SubtitleManager.EnsureExists().Show("Camera", PresetLabel(CurrentPreset), 1.4f);
    }

    public void SetPreset(BusCameraPreset preset)
    {
        CurrentPreset = preset;
        ApplyPreset(true);
    }

    void ApplyPreset(bool immediate)
    {
        if (bus == null || targetCamera == null) return;
        targetCamera.transform.SetParent(bus.transform, false);
        float width = Mathf.Max(2.2f, busLocalBounds.size.x);
        float height = Mathf.Max(2.8f, busLocalBounds.size.y);
        float length = Mathf.Max(7f, busLocalBounds.size.z);
        float rear = busLocalBounds.min.z;
        float front = busLocalBounds.max.z;
        if (CurrentPreset == BusCameraPreset.Cockpit) { targetLocalPosition = new Vector3(-width * .22f, busLocalBounds.min.y + height * .72f, front - length * .18f); targetLocalRotation = Quaternion.Euler(3f, 0f, 0f); }
        else if (CurrentPreset == BusCameraPreset.WideChase) { targetLocalPosition = new Vector3(0f, busLocalBounds.max.y + height * .45f, rear - length * .72f); targetLocalRotation = Quaternion.Euler(12f, 0f, 0f); }
        else if (CurrentPreset == BusCameraPreset.DoorView) { targetLocalPosition = new Vector3(busLocalBounds.max.x + .35f, busLocalBounds.min.y + height * .65f, busLocalBounds.center.z - length * .12f); targetLocalRotation = Quaternion.Euler(5f, 35f, 0f); }
        else { targetLocalPosition = new Vector3(0f, busLocalBounds.max.y + height * .2f, rear - length * .42f); targetLocalRotation = Quaternion.Euler(12f, 0f, 0f); }
        if (immediate) { targetCamera.transform.localPosition = targetLocalPosition; targetCamera.transform.localRotation = targetLocalRotation; }
    }

    static Bounds CalculateLocalVisualBounds(Transform root)
    {
        var controller = root.GetComponent<BusController>();
        Transform visual = controller != null && controller.modelRoot != null ? controller.modelRoot : root;
        Renderer[] renderers = visual.GetComponentsInChildren<Renderer>(true);
        if (renderers.Length == 0) return new Bounds(new Vector3(0f, 1.5f, 0f), new Vector3(2.5f, 3f, 10f));
        Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        foreach (Renderer renderer in renderers)
        {
            // Transform mesh-local corners, not a rotated world AABB. The
            // latter inflates the bus dimensions after a route spawn rotation.
            Bounds b = renderer.localBounds;
            for (int x = -1; x <= 1; x += 2)
            for (int y = -1; y <= 1; y += 2)
            for (int z = -1; z <= 1; z += 2)
            {
                Vector3 corner = root.InverseTransformPoint(renderer.transform.TransformPoint(b.center + Vector3.Scale(b.extents, new Vector3(x, y, z))));
                min = Vector3.Min(min, corner); max = Vector3.Max(max, corner);
            }
        }
        return new Bounds((min + max) * .5f, max - min);
    }

    Vector3 ResolveCollisionSafePosition(Vector3 desiredLocal)
    {
        if (CurrentPreset == BusCameraPreset.Cockpit || CurrentPreset == BusCameraPreset.DoorView) return desiredLocal;
        Vector3 pivot = bus.transform.TransformPoint(new Vector3(0f, 2.5f, 0f)); Vector3 desired = bus.transform.TransformPoint(desiredLocal); Vector3 direction = desired - pivot; float distance = direction.magnitude;
        int hitCount = Physics.RaycastNonAlloc(pivot, direction.normalized, cameraHits, distance, ~0, QueryTriggerInteraction.Ignore); float nearest = distance;
        for (int i = 0; i < hitCount; i++) if (!cameraHits[i].transform.IsChildOf(bus.transform)) nearest = Mathf.Min(nearest, cameraHits[i].distance);
        return bus.transform.InverseTransformPoint(pivot + direction.normalized * Mathf.Max(1.2f, nearest - 0.3f));
    }

    void EnsureMirror()
    {
        if (mirrorCamera != null || bus == null) return;
        GameObject cameraObject = new GameObject("Digital Rear View Camera", typeof(Camera)); cameraObject.transform.SetParent(bus.transform, false); cameraObject.transform.localPosition = new Vector3(0f, 3f, -2.5f); cameraObject.transform.localRotation = Quaternion.Euler(4f, 180f, 0f);
        mirrorCamera = cameraObject.GetComponent<Camera>(); mirrorCamera.fieldOfView = 55f; mirrorCamera.depth = -20f; mirrorTexture = new RenderTexture(320, 112, 16, RenderTextureFormat.ARGB32); mirrorTexture.name = "RealBus Rear View"; mirrorCamera.targetTexture = mirrorTexture;
        GameObject canvasObject = new GameObject("RearViewCanvas", typeof(Canvas), typeof(CanvasScaler)); canvasObject.transform.SetParent(transform, false); mirrorCanvas = canvasObject.GetComponent<Canvas>(); mirrorCanvas.renderMode = RenderMode.ScreenSpaceOverlay; mirrorCanvas.sortingOrder = 75;
        CanvasScaler scaler = canvasObject.GetComponent<CanvasScaler>(); scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize; scaler.referenceResolution = new Vector2(1920f, 1080f);
        GameObject image = new GameObject("RearView", typeof(RectTransform), typeof(RawImage)); image.transform.SetParent(canvasObject.transform, false); RectTransform r = image.GetComponent<RectTransform>(); r.anchorMin = r.anchorMax = new Vector2(0.5f, 1f); r.pivot = new Vector2(0.5f, 1f); r.sizeDelta = new Vector2(420f, 145f); r.anchoredPosition = new Vector2(0f, -190f); image.GetComponent<RawImage>().texture = mirrorTexture;
    }

    void SetMirrorVisible(bool visible) { if (mirrorCamera != null) mirrorCamera.enabled = visible; if (mirrorCanvas != null) mirrorCanvas.gameObject.SetActive(visible); }
    void ReleaseMirror() { if (mirrorCamera != null) Destroy(mirrorCamera.gameObject); if (mirrorCanvas != null) Destroy(mirrorCanvas.gameObject); if (mirrorTexture != null) { mirrorTexture.Release(); Destroy(mirrorTexture); } mirrorCamera = null; mirrorCanvas = null; mirrorTexture = null; }
    static string PresetLabel(BusCameraPreset preset) { return preset == BusCameraPreset.Cockpit ? "Cockpit view" : preset == BusCameraPreset.WideChase ? "Wide chase view" : preset == BusCameraPreset.DoorView ? "Door and kerb view" : "Exterior view"; }
}
