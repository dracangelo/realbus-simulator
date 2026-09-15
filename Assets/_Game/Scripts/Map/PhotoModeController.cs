using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhotoModeController : MonoBehaviour
{
    public static PhotoModeController Instance { get; private set; }

    [Header("References")]
    public Camera targetCamera;
    public Transform busTransform;
    public Transform cameraPivot;

    [Header("Input")]
    public Key toggleKey = Key.F10;
    public Key screenshotKey = Key.F12;
    public bool requireRightMouseButtonToLook = true;

    [Header("Movement")]
    public float moveSpeed = 10f;
    public float fastMoveMultiplier = 3f;
    public float lookSensitivity = 0.12f;

    [Header("State")]
    [SerializeField] bool isPhotoModeActive;
    [SerializeField] string lastScreenshotPath;

    float previousTimeScale;
    bool previousAudioPause;
    Transform originalParent;
    Vector3 originalLocalPosition;
    Quaternion originalLocalRotation;
    float pitchDegrees;
    float yawDegrees;

    public bool IsPhotoModeActive => isPhotoModeActive;
    public string LastScreenshotPath => lastScreenshotPath;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        if (targetCamera == null)
            targetCamera = Camera.main != null ? Camera.main : FindFirstObjectByType<Camera>();
        if (busTransform == null)
        {
            var bus = FindFirstObjectByType<BusController>();
            if (bus != null)
                busTransform = bus.transform;
        }
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
            return;

        if (keyboard[toggleKey].wasPressedThisFrame)
        {
            if (isPhotoModeActive)
                ExitPhotoMode();
            else
                EnterPhotoMode();
        }

        if (keyboard[screenshotKey].wasPressedThisFrame)
            CaptureScreenshot();

        if (!isPhotoModeActive || targetCamera == null)
            return;

        UpdatePhotoCameraInput();
    }

    public void EnterPhotoMode()
    {
        if (isPhotoModeActive)
            return;

        if (targetCamera == null)
            targetCamera = Camera.main != null ? Camera.main : FindFirstObjectByType<Camera>();
        if (targetCamera == null)
            return;

        originalParent = targetCamera.transform.parent;
        originalLocalPosition = targetCamera.transform.localPosition;
        originalLocalRotation = targetCamera.transform.localRotation;

        if (cameraPivot != null)
        {
            targetCamera.transform.SetParent(cameraPivot, true);
        }
        else
        {
            targetCamera.transform.SetParent(null, true);
        }

        Vector3 euler = targetCamera.transform.rotation.eulerAngles;
        pitchDegrees = NormalizePitch(euler.x);
        yawDegrees = euler.y;

        previousTimeScale = Time.timeScale;
        previousAudioPause = AudioListener.pause;
        Time.timeScale = 0f;
        AudioListener.pause = true;
        isPhotoModeActive = true;
    }

    public void ExitPhotoMode()
    {
        if (!isPhotoModeActive)
            return;

        if (targetCamera != null)
        {
            if (originalParent != null)
            {
                targetCamera.transform.SetParent(originalParent, false);
                targetCamera.transform.localPosition = originalLocalPosition;
                targetCamera.transform.localRotation = originalLocalRotation;
            }
            else
            {
                targetCamera.transform.SetParent(null, false);
                targetCamera.transform.localPosition = originalLocalPosition;
                targetCamera.transform.localRotation = originalLocalRotation;
            }
        }

        AudioListener.pause = previousAudioPause;
        Time.timeScale = previousTimeScale;
        isPhotoModeActive = false;
    }

    void OnDisable() { ExitPhotoMode(); }

    public void CaptureScreenshot()
    {
        string directory = Path.Combine(Application.persistentDataPath, "Screenshots");
        Directory.CreateDirectory(directory);

        string filename = $"photo_{System.DateTime.Now:yyyyMMdd_HHmmss}.png";
        lastScreenshotPath = Path.Combine(directory, filename);
        ScreenCapture.CaptureScreenshot(lastScreenshotPath);
        Debug.Log($"PhotoMode: Screenshot saved to {lastScreenshotPath}");
    }

    public void MovePhotoCamera(Vector3 localDirection)
    {
        if (isPhotoModeActive && targetCamera != null)
            targetCamera.transform.position += targetCamera.transform.TransformDirection(localDirection) * moveSpeed * Time.unscaledDeltaTime;
    }

    void UpdatePhotoCameraInput()
    {
        float deltaTime = Time.unscaledDeltaTime;
        var keyboard = Keyboard.current;
        var mouse = Mouse.current;
        if (keyboard == null)
            return;

        Vector3 move = Vector3.zero;
        if (keyboard.wKey.isPressed) move += targetCamera.transform.forward;
        if (keyboard.sKey.isPressed) move -= targetCamera.transform.forward;
        if (keyboard.dKey.isPressed) move += targetCamera.transform.right;
        if (keyboard.aKey.isPressed) move -= targetCamera.transform.right;
        if (keyboard.eKey.isPressed) move += Vector3.up;
        if (keyboard.qKey.isPressed) move -= Vector3.up;

        if (move.sqrMagnitude > 0.001f)
        {
            float speed = moveSpeed;
            if (keyboard.leftShiftKey.isPressed || keyboard.rightShiftKey.isPressed)
                speed *= fastMoveMultiplier;

            targetCamera.transform.position += move.normalized * speed * deltaTime;
        }

        bool isLooking = mouse != null && (!requireRightMouseButtonToLook || mouse.rightButton.isPressed);
        if (isLooking)
        {
            Vector2 delta = mouse.delta.ReadValue();
            yawDegrees += delta.x * lookSensitivity;
            pitchDegrees = Mathf.Clamp(pitchDegrees - delta.y * lookSensitivity, -89f, 89f);
            targetCamera.transform.rotation = Quaternion.Euler(pitchDegrees, yawDegrees, 0f);
        }
    }

    static float NormalizePitch(float rawPitch)
    {
        if (rawPitch > 180f)
            rawPitch -= 360f;
        return rawPitch;
    }
}
