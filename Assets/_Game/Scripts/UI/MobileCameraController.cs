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

    void Start() { StartCoroutine(BindWhenReady()); }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { ReleaseMirror(); StartCoroutine(BindWhenReady()); }
    void OnDestroy() { if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded; ReleaseMirror(); }

    IEnumerator BindWhenReady()
    {
        for (int frame = 0; frame < 120 && (bus == null || targetCamera == null); frame++)
        {
            bus = FindFirstObjectByType<BusController>();
            targetCamera = Camera.main != null ? Camera.main : FindFirstObjectByType<Camera>();
            if (bus == null || targetCamera == null) yield return null;
        }
        if (bus == null || targetCamera == null) { SetMirrorVisible(false); yield break; }
        baseFieldOfView = targetCamera.fieldOfView; CurrentPreset = (BusCameraPreset)Mathf.Clamp(PlayerPrefs.GetInt("RealBus.CameraPreset", 0), 0, 3);
        ApplyPreset(true); EnsureMirror();
    }

    void Update()
    {
        if (bus == null || targetCamera == null) return;
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

    void ApplyPreset(bool immediate)
    {
        if (bus == null || targetCamera == null) return;
        targetCamera.transform.SetParent(bus.transform, false);
        if (CurrentPreset == BusCameraPreset.Cockpit) { targetLocalPosition = new Vector3(-0.42f, 2.55f, 1.75f); targetLocalRotation = Quaternion.Euler(3f, 0f, 0f); }
        else if (CurrentPreset == BusCameraPreset.WideChase) { targetLocalPosition = new Vector3(0f, 4.2f, -11.5f); targetLocalRotation = Quaternion.Euler(13f, 0f, 0f); }
        else if (CurrentPreset == BusCameraPreset.DoorView) { targetLocalPosition = new Vector3(0.9f, 2.35f, -1.2f); targetLocalRotation = Quaternion.Euler(5f, 35f, 0f); }
        else { targetLocalPosition = new Vector3(0f, 3.2f, -7.5f); targetLocalRotation = Quaternion.Euler(14f, 0f, 0f); }
        if (immediate) { targetCamera.transform.localPosition = targetLocalPosition; targetCamera.transform.localRotation = targetLocalRotation; }
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
