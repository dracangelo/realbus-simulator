using UnityEngine;

public class RoadSurfaceDetector : MonoBehaviour
{
    public enum RoadType
    {
        Asphalt,
        Cobblestone,
        Dirt
    }

    [System.Serializable]
    public class RoadLayerMapping
    {
        public string layerName = "Road";
        public RoadType roadType = RoadType.Asphalt;
    }

    [Header("References")]
    public BusController busController;

    [Header("Raycast")]
    public float extraRayLength = 0.35f;
    public LayerMask surfaceLayers = ~0;

    [Header("Road Types (by layer)")]
    public RoadLayerMapping[] layerMappings =
    {
        new RoadLayerMapping { layerName = "Road", roadType = RoadType.Asphalt },
        new RoadLayerMapping { layerName = "Cobblestone", roadType = RoadType.Cobblestone },
        new RoadLayerMapping { layerName = "Dirt", roadType = RoadType.Dirt },
    };

    [Header("Multipliers")]
    public float asphaltMultiplier = 1.0f;
    public float cobblestoneMultiplier = 0.85f;
    public float dirtMultiplier = 0.65f;

    [Header("Wet (rain) multiplier")]
    public float wetMultiplier = 0.7f;

    public float[] perWheelSurfaceMultiplier;

    void Start()
    {
        if (busController == null)
            busController = FindFirstObjectByType<BusController>();

        int wheelCount = busController != null && busController.allWheels != null ? busController.allWheels.Length : 0;
        perWheelSurfaceMultiplier = new float[Mathf.Max(0, wheelCount)];
        for (int i = 0; i < perWheelSurfaceMultiplier.Length; i++)
            perWheelSurfaceMultiplier[i] = asphaltMultiplier;
    }

    void Update()
    {
        if (busController == null || busController.allWheels == null) return;
        var wheels = busController.allWheels;

        if (perWheelSurfaceMultiplier == null || perWheelSurfaceMultiplier.Length != wheels.Length)
            perWheelSurfaceMultiplier = new float[wheels.Length];

        bool wet = WeatherSystem.Instance != null && WeatherSystem.Instance.IsRaining();

        for (int i = 0; i < wheels.Length; i++)
        {
            var wc = wheels[i];
            if (wc == null)
            {
                perWheelSurfaceMultiplier[i] = asphaltMultiplier;
                continue;
            }

            float mult = asphaltMultiplier;
            RoadType type = RoadType.Asphalt;

            Vector3 origin = wc.transform.position + Vector3.up * 0.1f;
            float rayLen = wc.suspensionDistance + wc.radius + extraRayLength;
            if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, rayLen, surfaceLayers, QueryTriggerInteraction.Ignore))
            {
                int layer = hit.collider.gameObject.layer;
                type = RoadTypeFromLayer(layer);
                mult = MultiplierFromType(type);

                if (wet)
                    mult *= wetMultiplier;
            }

            perWheelSurfaceMultiplier[i] = mult;
        }
    }

    RoadType RoadTypeFromLayer(int layer)
    {
        if (layerMappings != null)
        {
            for (int i = 0; i < layerMappings.Length; i++)
            {
                int mapped = LayerMask.NameToLayer(layerMappings[i].layerName);
                if (mapped >= 0 && mapped == layer)
                    return layerMappings[i].roadType;
            }
        }
        return RoadType.Asphalt;
    }

    float MultiplierFromType(RoadType type)
    {
        switch (type)
        {
            case RoadType.Cobblestone: return cobblestoneMultiplier;
            case RoadType.Dirt: return dirtMultiplier;
            default: return asphaltMultiplier;
        }
    }
}

