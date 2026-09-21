using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

// Submit touch state before BusController combines it with keyboard input.
[DefaultExecutionOrder(-100)]
public class MobileControlsUI : MonoBehaviour
{
    public enum ControlScheme { Buttons, SteeringWheel, Tilt }
    public BusController busController;
    public Button accelerateButton, brakeButton, steerLeftButton, steerRightButton;
    public Button retarderButton, hornButton, shiftUpButton, shiftDownButton;
    public Button doorButton;
    public SteeringWheelControl steeringWheel;
    public ControlScheme controlScheme;
    public float tiltSensitivity = 2f;
    [Range(0.25f, 1.5f)] public float steeringSensitivity = 1f;
    [Range(0f, 0.3f)] public float steeringDeadZone = 0.06f;
    bool accelerating, braking, left, right;

    void Start()
    {
        RebindBusController();
        ApplyCinematicLayout();
        SetupHoldButton(accelerateButton, () => accelerating = true, () => accelerating = false);
        SetupHoldButton(brakeButton, () => braking = true, () => braking = false);
        SetupHoldButton(steerLeftButton, () => left = true, () => left = false);
        SetupHoldButton(steerRightButton, () => right = true, () => right = false);
        if (shiftUpButton != null) shiftUpButton.onClick.AddListener(SelectDriveOrShiftUp);
        if (shiftDownButton != null) shiftDownButton.onClick.AddListener(ShiftDownOrSelectReverse);
        if (retarderButton != null) retarderButton.onClick.AddListener(() => { if (busController != null) busController.ToggleRetarder(); });
        if (hornButton != null) hornButton.onClick.AddListener(() => { if (busController != null) busController.HonkHorn(); });
        if (doorButton != null) doorButton.onClick.AddListener(() => PassengerManager.Instance?.RequestDoors());
        SelectControlScheme(PlayerPrefs.GetInt("RealBus.ControlScheme", (int)controlScheme));
        ApplyControlPreferences(SettingsManager.EnsureExists().Current);
    }

    void ApplyCinematicLayout()
    {
        StyleControl(accelerateButton, "ACCEL", new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(-28f, 28f), new Vector2(105f, 230f), 18f);
        StyleControl(brakeButton, "BRAKE", new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(-150f, 28f), new Vector2(112f, 190f), 17f);
        StyleControl(steerLeftButton, "‹", Vector2.zero, Vector2.zero, new Vector2(34f, 34f), new Vector2(170f, 170f), 72f);
        StyleControl(steerRightButton, "›", Vector2.zero, Vector2.zero, new Vector2(220f, 34f), new Vector2(170f, 170f), 72f);
        StyleControl(shiftDownButton, "▼", new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(-36f, 286f), new Vector2(88f, 72f), 34f);
        StyleControl(shiftUpButton, "▲", new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(-36f, 370f), new Vector2(88f, 72f), 34f);
        StyleControl(retarderButton, "R", new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(-36f, 458f), new Vector2(88f, 78f), 28f, UITheme.Accent);
        StyleControl(hornButton, "HORN", new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(-36f, 550f), new Vector2(88f, 78f), 15f);
        doorButton = EnsureActionButton(doorButton, "DoorButton", "DOOR");
        StyleControl(doorButton, "DOOR", new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(-36f, 642f), new Vector2(88f, 78f), 14f);
        AddPedalGrooves(accelerateButton, 5);
        AddPedalGrooves(brakeButton, 4);
        EnsureSteeringWheel();
    }

    Button EnsureActionButton(Button button, string objectName, string label)
    {
        if (button != null) return button;
        GameObject root = new GameObject(objectName, typeof(RectTransform), typeof(Image), typeof(Button));
        root.transform.SetParent(transform, false);
        TextMeshProUGUI text = new GameObject("Label", typeof(RectTransform), typeof(TextMeshProUGUI)).GetComponent<TextMeshProUGUI>();
        text.transform.SetParent(root.transform, false);
        RectTransform labelRect = text.rectTransform;
        labelRect.anchorMin = Vector2.zero; labelRect.anchorMax = Vector2.one;
        labelRect.offsetMin = labelRect.offsetMax = Vector2.zero;
        text.text = label; text.raycastTarget = false;
        return root.GetComponent<Button>();
    }

    void EnsureSteeringWheel()
    {
        if (steeringWheel != null) return;
        GameObject wheel = new GameObject("SteeringWheel", typeof(RectTransform), typeof(Image), typeof(SteeringWheelControl));
        wheel.transform.SetParent(transform, false);
        RectTransform rect = wheel.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = rect.pivot = Vector2.zero;
        rect.anchoredPosition = new Vector2(34f, 30f);
        rect.sizeDelta = new Vector2(270f, 270f);
        Image image = wheel.GetComponent<Image>(); image.color = UITheme.Outline;
        RuntimeUiShapes.Circle(image); RuntimeUiShapes.SoftShadow(image, .5f, 10f);
        Outline outline = wheel.AddComponent<Outline>(); outline.effectColor = new Color(1f, 1f, 1f, .55f); outline.effectDistance = new Vector2(3f, -3f);
        steeringWheel = wheel.GetComponent<SteeringWheelControl>();
        Image wheelFace = new GameObject("WheelFace", typeof(RectTransform), typeof(Image)).GetComponent<Image>();
        wheelFace.transform.SetParent(wheel.transform, false); wheelFace.color = new Color(.025f, .03f, .032f, 1f); wheelFace.raycastTarget = false; RuntimeUiShapes.Circle(wheelFace);
        RectTransform faceRect = wheelFace.rectTransform; faceRect.anchorMin = faceRect.anchorMax = new Vector2(.5f, .5f); faceRect.sizeDelta = new Vector2(226f, 226f);
        CreateWheelSpoke(wheel.transform, new Vector2(12f, 155f), 58f);
        CreateWheelSpoke(wheel.transform, new Vector2(208f, 155f), -58f);
        CreateWheelSpoke(wheel.transform, new Vector2(110f, 38f), 0f);
        Image hub = new GameObject("Hub", typeof(RectTransform), typeof(Image)).GetComponent<Image>();
        hub.transform.SetParent(wheel.transform, false); hub.color = UITheme.SurfaceBright; hub.raycastTarget = false;
        RectTransform hubRect = hub.rectTransform; hubRect.anchorMin = hubRect.anchorMax = new Vector2(.5f, .5f); hubRect.sizeDelta = new Vector2(74f, 74f);
    }

    static void CreateWheelSpoke(Transform parent, Vector2 position, float angle)
    {
        Image spoke = new GameObject("Spoke", typeof(RectTransform), typeof(Image)).GetComponent<Image>();
        spoke.transform.SetParent(parent, false); spoke.color = UITheme.SurfaceBright; spoke.raycastTarget = false;
        RectTransform rect = spoke.rectTransform; rect.anchorMin = rect.anchorMax = Vector2.zero; rect.pivot = new Vector2(0f, .5f); rect.anchoredPosition = position; rect.sizeDelta = new Vector2(105f, 22f); rect.localEulerAngles = new Vector3(0f, 0f, angle);
    }

    static void StyleControl(Button button, string label, Vector2 anchor, Vector2 pivot, Vector2 position, Vector2 size, float fontSize, Color? accent = null)
    {
        if (!button) return;
        RectTransform rect = button.transform as RectTransform;
        rect.anchorMin = rect.anchorMax = anchor; rect.pivot = pivot; rect.anchoredPosition = position; rect.sizeDelta = size; rect.localScale = Vector3.one;
        Image image = button.GetComponent<Image>();
        if (image)
        {
            image.color = new Color(.035f, .04f, .042f, .92f);
            bool circular = Mathf.Abs(size.x - size.y) < 18f && Mathf.Max(size.x, size.y) <= 190f;
            if (circular) RuntimeUiShapes.Circle(image); else RuntimeUiShapes.Rounded(image);
            RuntimeUiShapes.SoftShadow(image, .42f, 7f);
        }
        Outline outline = button.GetComponent<Outline>(); if (!outline) outline = button.gameObject.AddComponent<Outline>();
        outline.effectColor = accent ?? new Color(1f, 1f, 1f, .48f); outline.effectDistance = new Vector2(3f, -3f);
        TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>(true);
        if (text)
        {
            text.text = label; text.fontSize = fontSize; text.font = UITheme.GetFont(UITheme.FontWeight.Bold);
            text.color = accent ?? UITheme.TextPrimary; text.alignment = TextAlignmentOptions.Center;
        }
    }

    static void AddPedalGrooves(Button button, int count)
    {
        if (button == null || button.transform.Find("PedalGrooves") != null) return;
        GameObject grooves = new GameObject("PedalGrooves", typeof(RectTransform)); grooves.transform.SetParent(button.transform, false);
        RectTransform root = grooves.GetComponent<RectTransform>(); root.anchorMin = new Vector2(.18f, .14f); root.anchorMax = new Vector2(.82f, .72f); root.offsetMin = root.offsetMax = Vector2.zero;
        for (int i = 0; i < count; i++)
        {
            Image groove = new GameObject("Groove", typeof(RectTransform), typeof(Image)).GetComponent<Image>(); groove.transform.SetParent(root, false);
            groove.color = new Color(0f, 0f, 0f, .42f); groove.raycastTarget = false; RuntimeUiShapes.Rounded(groove);
            RectTransform r = groove.rectTransform; r.anchorMin = new Vector2(0f, (i + .2f) / count); r.anchorMax = new Vector2(1f, (i + .2f) / count); r.sizeDelta = new Vector2(0f, 10f);
        }
        grooves.transform.SetAsFirstSibling();
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
        if (busController == null)
        {
            RebindBusController();
            if (busController == null) return;
        }
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

    void RebindBusController()
    {
        if (busController != null && busController.isActiveAndEnabled) return;
        busController = FindFirstObjectByType<BusController>();
    }

    void SelectDriveOrShiftUp()
    {
        if (busController == null || busController.transmissionData == null) return;
        if (busController.transmissionData.currentGear < 0) busController.SelectReverse(false);
        else busController.transmissionData.ShiftUp();
    }

    void ShiftDownOrSelectReverse()
    {
        if (busController == null || busController.transmissionData == null || busController.currentSpeedKmh >= 1f) return;
        if (busController.transmissionData.currentGear == 0) busController.SelectReverse(true);
        else busController.transmissionData.ShiftDown();
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
        Add(doorButton, list);
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
