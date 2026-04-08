using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileControlsUI : MonoBehaviour
{
    [Header("Bus Reference")]
    public BusController busController;

    [Header("Buttons")]
    public Button accelerateButton;
    public Button brakeButton;
    public Button steerLeftButton;
    public Button steerRightButton;
    public Button retarderButton;
    public Button hornButton;
    public Button shiftUpButton;
    public Button shiftDownButton;

    private bool pressingAccelerate = false;
    private bool pressingBrake = false;
    private float mobileSteer = 0f;

    void Start()
    {
        SetupHoldButton(accelerateButton,
            () => pressingAccelerate = true,
            () => pressingAccelerate = false);

        SetupHoldButton(brakeButton,
            () => pressingBrake = true,
            () => pressingBrake = false);

        SetupHoldButton(steerLeftButton,
            () => mobileSteer = -1f,
            () => mobileSteer = 0f);

        SetupHoldButton(steerRightButton,
            () => mobileSteer = 1f,
            () => mobileSteer = 0f);

        shiftUpButton.onClick.AddListener(() => busController.transmissionData.ShiftUp());
        shiftDownButton.onClick.AddListener(() => busController.transmissionData.ShiftDown());
        retarderButton.onClick.AddListener(() => busController.ToggleRetarder());
        hornButton.onClick.AddListener(() => busController.HonkHorn());
    }

    void Update()
    {
        // Combine mobile button input with keyboard
        if (pressingAccelerate) busController.throttleInput = 1f;
        if (pressingBrake) busController.brakeInput = 1f;
        if (mobileSteer != 0f) busController.SetSteerInput(mobileSteer);
    }

    void SetupHoldButton(Button btn, System.Action onPress, System.Action onRelease)
    {
        var trigger = btn.gameObject.AddComponent<EventTrigger>();

        var pressEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        pressEntry.callback.AddListener((_) => onPress());
        trigger.triggers.Add(pressEntry);

        var releaseEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        releaseEntry.callback.AddListener((_) => onRelease());
        trigger.triggers.Add(releaseEntry);
    }
}