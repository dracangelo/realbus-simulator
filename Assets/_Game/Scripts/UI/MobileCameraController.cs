using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BusCameraPreset { Exterior, Cockpit, WideChase, DoorView }

/// <summary>
/// Cinematic camera controller for RealBus. Supports multiple driving presets,
/// dynamic speed FOV expansion, centrifugal chassis roll sway, acceleration/braking pitch,
/// subtle road vibration, and interactive 360° free orbit swipe.
/// </summary>
public class MobileCameraController : MonoBehaviour
{
    public static MobileCameraController Instance { get; private set; }
    public BusCameraPreset CurrentPreset { get; private set; }

    /// <summary>
    /// True while a directed shot owns the camera. The driving rig stops writing the
    /// pose entirely rather than fighting the shot frame by frame, and the mirror stays
    /// off because it is parented to the bus, not to the camera.
    /// </summary>
    public bool CinematicOverride { get; private set; }
    [Tooltip("Optional rear-view inset. Off by default to keep cockpit a single viewport.")]
    public bool showRearViewInset = false;

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

    // ── Cinematic Camera Dynamics ────────────────────────────────────
    float lastBusYaw;
    float lastSpeedKmh;
    float currentChassisRoll;
    float currentPitchTilt;

    // ── Free-Look Orbit ─────────────────────────────────────────────
    float orbitYaw;
    float orbitPitch;
    float lastOrbitTime;
    bool isDraggingOrbit;
    Vector2 lastPointerPos;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static MobileCameraController EnsureExists()
    {
        if (Instance != null) return Instance;
        MobileCameraController existing = FindAnyObjectByType<MobileCameraController>();
        return existing != null ? existing : new GameObject("MobileCameraController").AddComponent<MobileCameraController>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start() { Rebind(); }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { Rebind(); }

    void Rebind()
    {
        if (binding != null) StopCoroutine(binding);
        ReleaseMirror();
        bus = null;
        targetCamera = null;
        orbitYaw = 0f;
        orbitPitch = 0f;
        currentChassisRoll = 0f;
        currentPitchTilt = 0f;
        binding = StartCoroutine(BindWhenReady());
    }

    void OnDestroy()
    {
        if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded;
        ReleaseMirror();
    }

    IEnumerator BindWhenReady()
    {
        yield return null;
        for (int frame = 0; frame < 120 && (bus == null || targetCamera == null); frame++)
        {
            bus = MissionManager.Instance != null ? MissionManager.Instance.busController : null;
            if (bus == null || !bus.isActiveAndEnabled) bus = FindAnyObjectByType<BusController>();
            targetCamera = Camera.main != null ? Camera.main : FindAnyObjectByType<Camera>();
            if (bus == null || targetCamera == null) yield return null;
        }
        binding = null;
        if (bus == null || targetCamera == null) { SetMirrorVisible(false); yield break; }
        busLocalBounds = CalculateLocalVisualBounds(bus.transform);
        baseFieldOfView = targetCamera.fieldOfView > 30f ? targetCamera.fieldOfView : 60f;
        CurrentPreset = (BusCameraPreset)Mathf.Clamp(PlayerPrefs.GetInt("RealBus.CameraPreset", 0), 0, 3);
        lastBusYaw = bus.transform.eulerAngles.y;
        lastSpeedKmh = bus.currentSpeedKmh;
        ApplyPreset(true);
    }

    void LateUpdate()
    {
        if (bus == null || !bus.isActiveAndEnabled || targetCamera == null)
        {
            if (binding == null && FindAnyObjectByType<BusController>() != null) Rebind();
            return;
        }
        if (PhotoModeController.Instance != null && PhotoModeController.Instance.IsPhotoModeActive)
        {
            SetMirrorVisible(false);
            return;
        }
        if (CinematicOverride)
        {
            SetMirrorVisible(false);
            return;
        }

        var keyboard = Keyboard.current;
        if (keyboard != null && keyboard.cKey.wasPressedThisFrame) CycleCamera();

        HandleFreeLookInput();
        UpdateCinematicDynamics();
        UpdateCameraPose(false);
        UpdateDynamicFieldOfView();

        bool mirrorVisible = showRearViewInset && CurrentPreset == BusCameraPreset.Cockpit && QualitySettings.GetQualityLevel() > 0;
        if (mirrorVisible) EnsureMirror();
        SetMirrorVisible(mirrorVisible);
    }

    void HandleFreeLookInput()
    {
        // Only exterior cameras allow free 360° orbit
        bool allowsOrbit = CurrentPreset == BusCameraPreset.Exterior || CurrentPreset == BusCameraPreset.WideChase;
        if (!allowsOrbit)
        {
            orbitYaw = 0f;
            orbitPitch = 0f;
            isDraggingOrbit = false;
            return;
        }

        Vector2 pointerDelta = Vector2.zero;
        bool pointerDown = false;
        Vector2 pointerPos = Vector2.zero;

        // Check Touchscreen
        var touch = Touchscreen.current;
        if (touch != null && touch.primaryTouch.press.isPressed)
        {
            pointerDown = true;
            pointerPos = touch.primaryTouch.position.ReadValue();
        }
        else
        {
            // Check Mouse
            var mouse = Mouse.current;
            if (mouse != null)
            {
                if (mouse.rightButton.isPressed || mouse.leftButton.isPressed)
                {
                    pointerDown = true;
                    pointerPos = mouse.position.ReadValue();
                }
            }
        }

        if (pointerDown)
        {
            bool pointerOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
            if (!isDraggingOrbit)
            {
                if (!pointerOverUI)
                {
                    isDraggingOrbit = true;
                    lastPointerPos = pointerPos;
                    lastOrbitTime = Time.unscaledTime;
                }
            }
            else
            {
                pointerDelta = pointerPos - lastPointerPos;
                lastPointerPos = pointerPos;
                if (pointerDelta.sqrMagnitude > 0.04f)
                {
                    float sensitivity = 0.22f;
                    orbitYaw += pointerDelta.x * sensitivity;
                    orbitPitch = Mathf.Clamp(orbitPitch - pointerDelta.y * sensitivity, -18f, 48f);
                    lastOrbitTime = Time.unscaledTime;
                }
            }
        }
        else
        {
            isDraggingOrbit = false;
        }

        // Auto-realign behind the vehicle when moving or after idle timeout
        float dt = Mathf.Max(0.0001f, Time.unscaledDeltaTime);
        if (!isDraggingOrbit)
        {
            bool autoReturn = (bus.currentSpeedKmh > 5f) || (Time.unscaledTime - lastOrbitTime > 3.0f);
            if (autoReturn)
            {
                orbitYaw = Mathf.Lerp(orbitYaw, 0f, 1f - Mathf.Exp(-3.2f * dt));
                orbitPitch = Mathf.Lerp(orbitPitch, 0f, 1f - Mathf.Exp(-4.0f * dt));
            }
        }
    }

    void UpdateCinematicDynamics()
    {
        float dt = Mathf.Max(0.0001f, Time.unscaledDeltaTime);
        float currentYaw = bus.transform.eulerAngles.y;
        float yawDelta = Mathf.DeltaAngle(lastBusYaw, currentYaw) / dt;
        lastBusYaw = currentYaw;

        // Centrifugal chassis roll sway into turns
        float targetRoll = Mathf.Clamp(-yawDelta * 0.045f, -3.0f, 3.0f);
        currentChassisRoll = Mathf.Lerp(currentChassisRoll, targetRoll, 1f - Mathf.Exp(-5.5f * dt));

        // Pitch tilt under acceleration and heavy braking
        float currentSpeed = bus.currentSpeedKmh;
        float accel = (currentSpeed - lastSpeedKmh) / dt;
        lastSpeedKmh = currentSpeed;

        float targetPitch = Mathf.Clamp(accel * -0.06f, -1.8f, 1.8f);
        currentPitchTilt = Mathf.Lerp(currentPitchTilt, targetPitch, 1f - Mathf.Exp(-5.0f * dt));
    }

    void UpdateDynamicFieldOfView()
    {
        float dt = Mathf.Max(0.0001f, Time.unscaledDeltaTime);
        float speedRatio = Mathf.Clamp01(bus.currentSpeedKmh / 85f);
        float dynamicFov = baseFieldOfView;

        if (CurrentPreset == BusCameraPreset.Exterior || CurrentPreset == BusCameraPreset.WideChase)
            dynamicFov += speedRatio * 11f;
        else if (CurrentPreset == BusCameraPreset.Cockpit)
            dynamicFov += speedRatio * 7f;

        targetCamera.fieldOfView = Mathf.Lerp(targetCamera.fieldOfView, dynamicFov, 1f - Mathf.Exp(-3.5f * dt));
    }

    /// <summary>Hands the camera to a directed shot. The pose is left exactly where it is.</summary>
    public void BeginCinematicOverride()
    {
        CinematicOverride = true;
        SetMirrorVisible(false);
    }

    /// <summary>
    /// Takes the camera back. The shot is expected to have landed on the rig's own pose,
    /// so re-applying the preset is a no-op rather than a jump.
    /// </summary>
    public void EndCinematicOverride()
    {
        CinematicOverride = false;
        if (bus != null && targetCamera != null && bus.isActiveAndEnabled) ApplyPreset(true);
    }

    public void CycleCamera()
    {
        CurrentPreset = (BusCameraPreset)(((int)CurrentPreset + 1) % 4);
        PlayerPrefs.SetInt("RealBus.CameraPreset", (int)CurrentPreset);
        orbitYaw = 0f;
        orbitPitch = 0f;
        ApplyPreset(false);
        SubtitleManager.EnsureExists().Show("Camera", PresetLabel(CurrentPreset), 1.4f);
    }

    public void SetPreset(BusCameraPreset preset)
    {
        CurrentPreset = preset;
        orbitYaw = 0f;
        orbitPitch = 0f;
        ApplyPreset(true);
    }

    void ApplyPreset(bool immediate)
    {
        if (bus == null || targetCamera == null) return;
        targetCamera.transform.SetParent(null, true);
        float width = Mathf.Max(2.2f, busLocalBounds.size.x);
        float height = Mathf.Max(2.8f, busLocalBounds.size.y);
        float length = Mathf.Max(7f, busLocalBounds.size.z);
        float rear = busLocalBounds.min.z;
        float front = busLocalBounds.max.z;

        if (CurrentPreset == BusCameraPreset.Cockpit)
        {
            targetLocalPosition = new Vector3(-width * .22f, busLocalBounds.min.y + height * .53f, front - length * .115f);
            targetLocalRotation = Quaternion.Euler(8f, 0f, 0f);
        }
        else if (CurrentPreset == BusCameraPreset.WideChase)
        {
            targetLocalPosition = new Vector3(0f, busLocalBounds.max.y + height * .45f, rear - length * .72f);
            targetLocalRotation = Quaternion.Euler(12f, 0f, 0f);
        }
        else if (CurrentPreset == BusCameraPreset.DoorView)
        {
            targetLocalPosition = new Vector3(busLocalBounds.max.x + .35f, busLocalBounds.min.y + height * .65f, busLocalBounds.center.z - length * .12f);
            targetLocalRotation = Quaternion.Euler(5f, 35f, 0f);
        }
        else
        {
            targetLocalPosition = new Vector3(0f, busLocalBounds.max.y + height * .2f, rear - length * .42f);
            targetLocalRotation = Quaternion.Euler(12f, 0f, 0f);
        }

        if (immediate) UpdateCameraPose(true);
    }

    public static Quaternion HorizonRotation(Quaternion vehicleRotation)
    {
        Vector3 forward = Vector3.ProjectOnPlane(vehicleRotation * Vector3.forward, Vector3.up);
        if (forward.sqrMagnitude < .001f) forward = Vector3.forward;
        return Quaternion.LookRotation(forward.normalized, Vector3.up);
    }

    void UpdateCameraPose(bool immediate)
    {
        Quaternion heading = HorizonRotation(bus.transform.rotation);
        bool onboard = CurrentPreset == BusCameraPreset.Cockpit || CurrentPreset == BusCameraPreset.DoorView;

        // Apply interactive free orbit
        Quaternion orbitRotation = Quaternion.Euler(orbitPitch, orbitYaw, 0f);
        Quaternion effectiveHeading = onboard ? heading : heading * orbitRotation;

        Vector3 desired = onboard
            ? bus.transform.TransformPoint(targetLocalPosition)
            : bus.transform.position + effectiveHeading * targetLocalPosition;

        if (!onboard) desired = ResolveCollisionSafePosition(desired);

        // Subtle road vibration and engine rumble
        float dt = Mathf.Max(0.0001f, Time.unscaledDeltaTime);
        float speedRatio = Mathf.Clamp01(bus.currentSpeedKmh / 75f);
        float rumbleMagnitude = (bus.currentRPM > 400f ? 0.003f : 0f) + speedRatio * 0.015f;
        float rumbleX = (Mathf.PerlinNoise(Time.unscaledTime * 14f, 0.2f) - 0.5f) * rumbleMagnitude;
        float rumbleY = (Mathf.PerlinNoise(0.5f, Time.unscaledTime * 17f) - 0.5f) * rumbleMagnitude;
        Vector3 rumbleOffset = targetCamera.transform.right * rumbleX + targetCamera.transform.up * rumbleY;

        bool snap = immediate || (desired - targetCamera.transform.position).sqrMagnitude > 1600f;
        targetCamera.transform.position = snap ? desired : Vector3.Lerp(targetCamera.transform.position, desired, 1f - Mathf.Exp(-8f * dt)) + rumbleOffset;

        // Centrifugal chassis roll and acceleration pitch dynamics
        Quaternion cinematicDynamics = Quaternion.Euler(currentPitchTilt, 0f, currentChassisRoll);
        Quaternion targetRotation = effectiveHeading * targetLocalRotation * cinematicDynamics;

        targetCamera.transform.rotation = immediate
            ? targetRotation
            : Quaternion.Slerp(targetCamera.transform.rotation, targetRotation, 1f - Mathf.Exp(-9f * dt));
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

    Vector3 ResolveCollisionSafePosition(Vector3 desired)
    {
        Vector3 pivot = bus.transform.position + Vector3.up * 2.5f;
        Vector3 direction = desired - pivot;
        float distance = direction.magnitude;
        int hitCount = Physics.RaycastNonAlloc(pivot, direction.normalized, cameraHits, distance, ~0, QueryTriggerInteraction.Ignore);
        float nearest = distance;
        for (int i = 0; i < hitCount; i++)
        {
            if (!cameraHits[i].transform.IsChildOf(bus.transform))
                nearest = Mathf.Min(nearest, cameraHits[i].distance);
        }
        return pivot + direction.normalized * Mathf.Max(1.2f, nearest - 0.3f);
    }

    void EnsureMirror()
    {
        if (mirrorCamera != null || bus == null) return;
        GameObject cameraObject = new GameObject("Digital Rear View Camera", typeof(Camera));
        cameraObject.transform.SetParent(bus.transform, false);
        cameraObject.transform.localPosition = new Vector3(0f, 3f, -2.5f);
        cameraObject.transform.localRotation = Quaternion.Euler(4f, 180f, 0f);
        mirrorCamera = cameraObject.GetComponent<Camera>();
        mirrorCamera.fieldOfView = 55f;
        mirrorCamera.depth = -20f;
        mirrorTexture = new RenderTexture(320, 112, 16, RenderTextureFormat.ARGB32);
        mirrorTexture.name = "RealBus Rear View";
        mirrorCamera.targetTexture = mirrorTexture;

        GameObject canvasObject = new GameObject("RearViewCanvas", typeof(Canvas), typeof(CanvasScaler));
        canvasObject.transform.SetParent(transform, false);
        mirrorCanvas = canvasObject.GetComponent<Canvas>();
        mirrorCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        mirrorCanvas.sortingOrder = 75;
        CanvasScaler scaler = canvasObject.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);

        GameObject image = new GameObject("RearView", typeof(RectTransform), typeof(RawImage));
        image.transform.SetParent(canvasObject.transform, false);
        RectTransform r = image.GetComponent<RectTransform>();
        r.anchorMin = r.anchorMax = new Vector2(0.5f, 1f);
        r.pivot = new Vector2(0.5f, 1f);
        r.sizeDelta = new Vector2(420f, 145f);
        r.anchoredPosition = new Vector2(0f, -190f);
        image.GetComponent<RawImage>().texture = mirrorTexture;
    }

    void SetMirrorVisible(bool visible)
    {
        if (mirrorCamera != null) mirrorCamera.enabled = visible;
        if (mirrorCanvas != null) mirrorCanvas.gameObject.SetActive(visible);
    }

    void ReleaseMirror()
    {
        if (mirrorCamera != null) Destroy(mirrorCamera.gameObject);
        if (mirrorCanvas != null) Destroy(mirrorCanvas.gameObject);
        if (mirrorTexture != null) { mirrorTexture.Release(); Destroy(mirrorTexture); }
        mirrorCamera = null;
        mirrorCanvas = null;
        mirrorTexture = null;
    }

    static string PresetLabel(BusCameraPreset preset)
    {
        return preset == BusCameraPreset.Cockpit ? "Cockpit view"
             : preset == BusCameraPreset.WideChase ? "Wide chase view"
             : preset == BusCameraPreset.DoorView ? "Door and kerb view"
             : "Exterior view";
    }
}
