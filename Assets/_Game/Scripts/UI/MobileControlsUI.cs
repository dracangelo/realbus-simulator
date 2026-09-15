using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// Submit touch state before BusController combines it with keyboard input.
[DefaultExecutionOrder(-100)]
public class MobileControlsUI : MonoBehaviour
{
    public enum ControlScheme { Buttons, SteeringWheel, Tilt }
    public BusController busController;
    public Button accelerateButton, brakeButton, steerLeftButton, steerRightButton;
    public Button retarderButton, hornButton, shiftUpButton, shiftDownButton;
    public SteeringWheelControl steeringWheel;
    public ControlScheme controlScheme;
    public float tiltSensitivity = 2f;
    [Range(0.25f, 1.5f)] public float steeringSensitivity = 1f;
    [Range(0f, 0.3f)] public float steeringDeadZone = 0.06f;
    bool accelerating, braking, left, right;

    void Start()
    {
        SetupHoldButton(accelerateButton, () => accelerating = true, () => accelerating = false);
        SetupHoldButton(brakeButton, () => braking = true, () => braking = false);
        SetupHoldButton(steerLeftButton, () => left = true, () => left = false);
        SetupHoldButton(steerRightButton, () => right = true, () => right = false);
        if (shiftUpButton != null) shiftUpButton.onClick.AddListener(() => { if (busController != null && busController.transmissionData != null) busController.transmissionData.ShiftUp(); });
        if (shiftDownButton != null) shiftDownButton.onClick.AddListener(() => { if (busController != null && busController.transmissionData != null) busController.transmissionData.ShiftDown(); });
        if (retarderButton != null) retarderButton.onClick.AddListener(() => { if (busController != null) busController.ToggleRetarder(); });
        if (hornButton != null) hornButton.onClick.AddListener(() => { if (busController != null) busController.HonkHorn(); });
        SelectControlScheme(PlayerPrefs.GetInt("RealBus.ControlScheme", (int)controlScheme));
        ApplyControlPreferences(SettingsManager.EnsureExists().Current);
    }

    public void SelectControlScheme(int scheme)
    {
        ResetInput();
        controlScheme = (ControlScheme)Mathf.Clamp(scheme, 0, 2);
        PlayerPrefs.SetInt("RealBus.ControlScheme", (int)controlScheme);
        if (steerLeftButton != null) steerLeftButton.gameObject.SetActive(controlScheme == ControlScheme.Buttons);
        if (steerRightButton != null) steerRightButton.gameObject.SetActive(controlScheme == ControlScheme.Buttons);
        if (steeringWheel != null) steeringWheel.gameObject.SetActive(controlScheme == ControlScheme.SteeringWheel);
        if (controlScheme == ControlScheme.Tilt && Accelerometer.current != null)
            InputSystem.EnableDevice(Accelerometer.current);
    }

    void Update()
    {
        if (busController == null) return;
        float steering = (right ? 1f : 0f) - (left ? 1f : 0f);
        if (controlScheme == ControlScheme.SteeringWheel) steering = steeringWheel != null ? steeringWheel.Value : 0f;
        if (controlScheme == ControlScheme.Tilt)
        {
            Vector3 gravity = Accelerometer.current != null ? Accelerometer.current.acceleration.ReadValue() : Vector3.zero;
            float axis = Screen.orientation == ScreenOrientation.LandscapeLeft ? -gravity.y :
                Screen.orientation == ScreenOrientation.LandscapeRight ? gravity.y : gravity.x;
            steering = Mathf.Clamp(axis * tiltSensitivity, -1f, 1f);
        }
        steering = ApplySteeringCurve(steering, steeringDeadZone, steeringSensitivity);
        busController.SetMobileInput(accelerating ? 1f : 0f, braking ? 1f : 0f, steering);
    }

    public void ApplyControlPreferences(RealBusSettings settings)
    {
        if (settings == null) return;
        steeringSensitivity = settings.steeringSensitivity;
        steeringDeadZone = settings.steeringDeadZone;
        tiltSensitivity = Mathf.Lerp(1f, 3f, Mathf.InverseLerp(0.25f, 1.5f, steeringSensitivity));
        SelectControlScheme(settings.controlScheme);
        MobileControlLayoutEditor editor = GetComponent<MobileControlLayoutEditor>();
        if (editor == null) editor = gameObject.AddComponent<MobileControlLayoutEditor>();
        editor.Bind(this);
        editor.ApplyProfile(settings.touchControlScale, settings.leftHandedControls);
    }

    public static float ApplySteeringCurve(float value, float deadZone, float sensitivity)
    {
        float magnitude = Mathf.Abs(value);
        if (magnitude <= Mathf.Clamp01(deadZone)) return 0f;
        float normalized = Mathf.InverseLerp(Mathf.Clamp01(deadZone), 1f, magnitude);
        float curved = Mathf.Pow(normalized, Mathf.Lerp(1.8f, 0.65f, Mathf.InverseLerp(0.25f, 1.5f, sensitivity)));
        return Mathf.Sign(value) * Mathf.Clamp01(curved * Mathf.Clamp(sensitivity, 0.25f, 1.5f));
    }

    public RectTransform[] GetEditableControls()
    {
        var list = new System.Collections.Generic.List<RectTransform>();
        Add(accelerateButton, list); Add(brakeButton, list); Add(steerLeftButton, list); Add(steerRightButton, list);
        Add(retarderButton, list); Add(hornButton, list); Add(shiftUpButton, list); Add(shiftDownButton, list);
        if (steeringWheel != null) list.Add(steeringWheel.GetComponent<RectTransform>());
        return list.ToArray();
    }

    static void Add(Button button, System.Collections.Generic.List<RectTransform> list)
    {
        if (button != null && button.transform is RectTransform) list.Add((RectTransform)button.transform);
    }

    void ResetInput()
    {
        accelerating = braking = left = right = false;
        if (steeringWheel != null) steeringWheel.ResetInput();
        if (busController != null) busController.SetMobileInput(0f, 0f, 0f);
    }
    void OnDisable() { ResetInput(); }
    void OnApplicationFocus(bool focused) { if (!focused) ResetInput(); }
    void OnApplicationPause(bool paused) { if (paused) ResetInput(); }

    void SetupHoldButton(Button button, System.Action press, System.Action release)
    {
        if (button == null) return;
        var trigger = button.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();
        AddEvent(trigger, EventTriggerType.PointerDown, press);
        AddEvent(trigger, EventTriggerType.PointerUp, release);
        AddEvent(trigger, EventTriggerType.PointerExit, release);
    }
    void AddEvent(EventTrigger trigger, EventTriggerType type, System.Action action)
    {
        var entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener(_ => action());
        trigger.triggers.Add(entry);
    }
}
