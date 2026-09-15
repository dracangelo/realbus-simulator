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
        public string tagName = "Road";
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
        new RoadLayerMapping { layerName = "Road", tagName = "Road", roadType = RoadType.Asphalt },
        new RoadLayerMapping { layerName = "Cobblestone", tagName = "Cobblestone", roadType = RoadType.Cobblestone },
        new RoadLayerMapping { layerName = "Dirt", tagName = "Dirt", roadType = RoadType.Dirt },
    };

    [Header("Multipliers")]
    public float asphaltMultiplier = 1.0f;
    public float cobblestoneMultiplier = 0.85f;
    public float dirtMultiplier = 0.65f;

    [Header("Wet (rain) multiplier")]
    public float wetMultiplier = 0.7f;

    public float[] perWheelSurfaceMultiplier;
    public RoadType[] perWheelRoadTypes;

    void Start()
    {
        if (busController == null)
            busController = FindFirstObjectByType<BusController>();

        int wheelCount = busController != null && busController.allWheels != null ? busController.allWheels.Length : 0;
        perWheelSurfaceMultiplier = new float[Mathf.Max(0, wheelCount)];
        perWheelRoadTypes = new RoadType[Mathf.Max(0, wheelCount)];
        for (int i = 0; i < perWheelSurfaceMultiplier.Length; i++)
        {
            perWheelSurfaceMultiplier[i] = asphaltMultiplier;
            perWheelRoadTypes[i] = RoadType.Asphalt;
        }
    }

    void Update()
    {
        if (busController == null || busController.allWheels == null) return;
        var wheels = busController.allWheels;

        if (perWheelSurfaceMultiplier == null || perWheelSurfaceMultiplier.Length != wheels.Length)
            perWheelSurfaceMultiplier = new float[wheels.Length];
        if (perWheelRoadTypes == null || perWheelRoadTypes.Length != wheels.Length)
            perWheelRoadTypes = new RoadType[wheels.Length];

        for (int i = 0; i < wheels.Length; i++)
        {
            var wc = wheels[i];
            if (wc == null)
            {
                perWheelSurfaceMultiplier[i] = asphaltMultiplier;
                perWheelRoadTypes[i] = RoadType.Asphalt;
                continue;
            }

            float mult = asphaltMultiplier;
            RoadType type = RoadType.Asphalt;

            Vector3 origin = wc.transform.position + Vector3.up * 0.1f;
            float rayLen = wc.suspensionDistance + wc.radius + extraRayLength;
            if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, rayLen, surfaceLayers, QueryTriggerInteraction.Ignore))
            {
                type = ResolveRoadType(hit.collider.transform);
                mult = MultiplierFromType(type);
            }

            perWheelSurfaceMultiplier[i] = mult;
            perWheelRoadTypes[i] = type;
        }
    }

    RoadType ResolveRoadType(Transform hitTransform)
    {
        var metadata = hitTransform.GetComponentInParent<RoadSurfaceType>();
        if (metadata != null) return metadata.roadType;
        Transform current = hitTransform;
        while (current != null)
        {
            RoadType type = RoadTypeFromLayerOrTag(current.gameObject.layer, current.tag);
            if (type != RoadType.Asphalt || HasExplicitAsphaltMapping(current.gameObject.layer, current.tag))
                return type;
            current = current.parent;
        }

        return RoadType.Asphalt;
    }

    RoadType RoadTypeFromLayerOrTag(int layer, string tagName)
    {
        if (layerMappings != null)
        {
            for (int i = 0; i < layerMappings.Length; i++)
            {
                var mapping = layerMappings[i];
                int mapped = string.IsNullOrWhiteSpace(mapping.layerName)
                    ? -1
                    : LayerMask.NameToLayer(mapping.layerName);
                bool layerMatches = mapped >= 0 && mapped == layer;
                bool tagMatches = !string.IsNullOrWhiteSpace(mapping.tagName) &&
                                  string.Equals(mapping.tagName, tagName, System.StringComparison.Ordinal);

                if (layerMatches || tagMatches)
                    return mapping.roadType;
            }
        }
        return RoadType.Asphalt;
    }

    bool HasExplicitAsphaltMapping(int layer, string tagName)
    {
        if (layerMappings == null)
            return false;

        for (int i = 0; i < layerMappings.Length; i++)
        {
            var mapping = layerMappings[i];
            if (mapping.roadType != RoadType.Asphalt)
                continue;

            int mapped = string.IsNullOrWhiteSpace(mapping.layerName)
                ? -1
                : LayerMask.NameToLayer(mapping.layerName);
            bool layerMatches = mapped >= 0 && mapped == layer;
            bool tagMatches = !string.IsNullOrWhiteSpace(mapping.tagName) &&
                              string.Equals(mapping.tagName, tagName, System.StringComparison.Ordinal);

            if (layerMatches || tagMatches)
                return true;
        }

        return false;
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
