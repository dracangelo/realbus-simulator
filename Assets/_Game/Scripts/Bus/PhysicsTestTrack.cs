using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

/// <summary>Standalone Phase 2 fixture. No map, mission, or save services required.</summary>
public class PhysicsTestTrack : MonoBehaviour
{
    public EngineSystem engineProfile;
    public TransmissionSystem transmissionProfile;
    public BusSpec busSpec;
    public bool createTestSurface = true;
    public BusController Bus => bus;
    BusController bus;
    EngineSystem ownedEngine;
    TransmissionSystem ownedTransmission;
    PhysicsMaterial roadFriction;

    void Start()
    {
        roadFriction = new PhysicsMaterial("Asphalt") { staticFriction = 0.7f, dynamicFriction = 0.7f };
        if (createTestSurface)
        {
        var road = Cube("500m asphalt test pad", new Vector3(0f, -0.25f, 0f), new Vector3(500f, 0.5f, 500f));
        road.GetComponent<Collider>().sharedMaterial = roadFriction;
        var hill = Cube("10 percent grade", new Vector3(50f, 2.5f, 0f), new Vector3(12f, 0.5f, 50f));
        hill.transform.rotation = Quaternion.Euler(-Mathf.Atan(0.1f) * Mathf.Rad2Deg, 0f, 0f);
        hill.GetComponent<Collider>().sharedMaterial = roadFriction;
        }
        var light = new GameObject("Sun").AddComponent<Light>();
        light.type = LightType.Directional;
        light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        light.intensity = 1.2f;
        var root = new GameObject("Bus_Root");
        root.transform.position = new Vector3(0f, 0.15f, -180f);
        var body = root.AddComponent<Rigidbody>();
        body.mass = 12000f;
        body.linearDamping = 0.3f;
        body.angularDamping = 0.5f;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        bus = root.AddComponent<BusController>();
        if (engineProfile == null) engineProfile = ownedEngine = ScriptableObject.CreateInstance<EngineSystem>();
        if (transmissionProfile == null) transmissionProfile = ownedTransmission = ScriptableObject.CreateInstance<TransmissionSystem>();
        bus.engineData = engineProfile;
        bus.transmissionData = transmissionProfile;
        if (busSpec != null) bus.ApplyBusSpec(busSpec);
        var shell = Cube("Bus body", Vector3.zero, new Vector3(2.55f, 2.5f, 12f));
        shell.transform.SetParent(root.transform, false);
        shell.transform.localPosition = new Vector3(0f, 2f, 0f);
        bus.modelRoot = shell.transform;
        var wheels = new WheelCollider[6];
        string[] names = { "FL", "FR", "ML", "MR", "RL", "RR" };
        for (int i = 0; i < 6; i++)
        {
            var wheel = new GameObject("WheelCollider_" + names[i]);
            wheel.transform.SetParent(root.transform, false);
            wheel.transform.localPosition = new Vector3(i % 2 == 0 ? -1.05f : 1.05f, 0.6f, i < 2 ? 3.1f : i < 4 ? -1.7f : -3.1f);
            var collider = wheels[i] = wheel.AddComponent<WheelCollider>();
            collider.radius = 0.5f;
            collider.mass = 150f;
            collider.suspensionDistance = 0.2f;
            collider.suspensionSpring = new JointSpring { spring = i < 2 ? 120000f : 160000f, damper = i < 2 ? 6000f : 8000f, targetPosition = 0.5f };
            collider.forwardFriction = new WheelFrictionCurve { extremumSlip = 0.4f, extremumValue = 1f, asymptoteSlip = 0.8f, asymptoteValue = 0.6f, stiffness = 1.2f };
            collider.sidewaysFriction = new WheelFrictionCurve { extremumSlip = 0.25f, extremumValue = 1f, asymptoteSlip = 0.5f, asymptoteValue = 0.75f, stiffness = 1f };
            collider.ConfigureVehicleSubsteps(5f, 12, 15);
            var visual = new GameObject("WheelVisual_" + names[i]);
            visual.transform.SetParent(root.transform, false);
            var mesh = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            Destroy(mesh.GetComponent<Collider>());
            mesh.transform.SetParent(visual.transform, false);
            mesh.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            mesh.transform.localScale = new Vector3(1f, 0.15f, 1f);
            var sync = visual.AddComponent<WheelVisualSync>();
            sync.wheelCollider = collider;
            sync.wheelMesh = visual.transform;
        }
        bus.allWheels = wheels;
        bus.steerWheels = new[] { wheels[0], wheels[1] };
        bus.driveWheels = bus.rearWheels = new[] { wheels[2], wheels[3], wheels[4], wheels[5] };
        root.AddComponent<AerodynamicsSystem>();
        root.AddComponent<PassengerLoadDynamics>();
        root.AddComponent<PhysicsValidationRecorder>();
        bus.engineAudio = root.AddComponent<AudioSource>();
        bus.engineAudio.loop = true; // Assign an engine loop for RPM-driven pitch.
        var camera = new GameObject("Test Camera").AddComponent<Camera>();
        camera.tag = "MainCamera";
        camera.gameObject.AddComponent<AudioListener>();
        camera.transform.SetParent(root.transform, false);
        camera.transform.localPosition = new Vector3(0f, 6f, -16f);
        camera.transform.localRotation = Quaternion.Euler(15f, 0f, 0f);
        BuildControls();
    }

    void BuildControls()
    {
        var canvas = new GameObject("Mobile Controls", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvas.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1280f, 720f);
        var controls = canvas.AddComponent<MobileControlsUI>();
        controls.busController = bus;
        controls.accelerateButton = Button(canvas.transform, "Accelerate", 1050f, 30f);
        controls.brakeButton = Button(canvas.transform, "Brake", 850f, 30f);
        controls.steerLeftButton = Button(canvas.transform, "Left", 20f, 30f);
        controls.steerRightButton = Button(canvas.transform, "Right", 220f, 30f);
        controls.retarderButton = Button(canvas.transform, "Retarder", 1050f, 120f);
        controls.hornButton = Button(canvas.transform, "Horn", 850f, 120f);
        var wheel = Button(canvas.transform, "Drag wheel", 100f, 130f);
        controls.steeringWheel = wheel.gameObject.AddComponent<SteeringWheelControl>();
        for (int i = 0; i < 3; i++)
        {
            int index = i;
            Button(canvas.transform, ((MobileControlsUI.ControlScheme)i).ToString(), 20f + i * 200f, 250f)
                .onClick.AddListener(() => controls.SelectControlScheme(index));
        }
        Button(canvas.transform, "Parking brake", 650f, 120f).onClick.AddListener(() => bus.SetParkingBrake(!bus.ParkingBrakeActive));
        Button(canvas.transform, "Forward/Reverse", 650f, 30f).onClick.AddListener(() => bus.SelectReverse(bus.transmissionData.currentGear >= 0));
        new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
    }

    static Button Button(Transform parent, string label, float x, float y)
    {
        var go = new GameObject(label, typeof(RectTransform), typeof(Image), typeof(Button));
        go.transform.SetParent(parent, false);
        var rect = (RectTransform)go.transform;
        rect.anchorMin = rect.anchorMax = rect.pivot = Vector2.zero;
        rect.anchoredPosition = new Vector2(x, y);
        rect.sizeDelta = new Vector2(180f, 70f);
        var text = new GameObject("Label", typeof(RectTransform), typeof(Text)).GetComponent<Text>();
        text.transform.SetParent(go.transform, false);
        text.rectTransform.anchorMin = Vector2.zero;
        text.rectTransform.anchorMax = Vector2.one;
        text.rectTransform.offsetMin = text.rectTransform.offsetMax = Vector2.zero;
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.fontSize = 22;
        text.color = Color.black;
        text.alignment = TextAnchor.MiddleCenter;
        text.text = label;
        text.raycastTarget = false;
        return go.GetComponent<Button>();
    }

    static GameObject Cube(string name, Vector3 position, Vector3 scale)
    {
        var result = GameObject.CreatePrimitive(PrimitiveType.Cube);
        result.name = name;
        result.transform.position = position;
        result.transform.localScale = scale;
        return result;
    }

    void OnDestroy()
    {
        if (ownedEngine != null) Destroy(ownedEngine);
        if (ownedTransmission != null) Destroy(ownedTransmission);
        if (roadFriction != null) Destroy(roadFriction);
    }
}
