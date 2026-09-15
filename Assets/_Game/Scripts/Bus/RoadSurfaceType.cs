using UnityEngine;

/// <summary>OSM surface metadata, independent of project-specific tags and layers.</summary>
public class RoadSurfaceType : MonoBehaviour
{
    public RoadSurfaceDetector.RoadType roadType;

    public static RoadSurfaceDetector.RoadType FromOsm(string surface)
    {
        switch (surface)
        {
            case "cobblestone": case "sett": case "unhewn_cobblestone":
                return RoadSurfaceDetector.RoadType.Cobblestone;
            case "dirt": case "earth": case "ground": case "gravel": case "unpaved": case "sand":
                return RoadSurfaceDetector.RoadType.Dirt;
            default: return RoadSurfaceDetector.RoadType.Asphalt;
        }
    }
}
