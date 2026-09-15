#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

public static class Phase3MapTools
{
    [MenuItem("Tools/RealBus/Phase 3/Import included Nairobi OSM previews")]
    public static void ImportPreviews()
    {
        const string path = "Assets/StreamingAssets/Cities/NBO/routes.json";
        if (!File.Exists(path)) { Debug.LogError("Missing included Nairobi routes.json"); return; }
        var data = OverpassResponse.Deserialize(File.ReadAllText(path));
        data.Resolve();
        string folder = "Assets/_Game/Routes";
        if (!AssetDatabase.IsValidFolder(folder)) AssetDatabase.CreateFolder("Assets/_Game", "Routes");
        folder += "/OSMPreviews";
        if (!AssetDatabase.IsValidFolder(folder)) AssetDatabase.CreateFolder("Assets/_Game/Routes", "OSMPreviews");
        int count = 0;
        foreach (var parsed in BusRouteParser.ParseRoutes(data))
        {
            var route = OSMRouteImporter.CreateRouteAsset(parsed, null);
            route.sourceCityCode = "NBO";
            route.routeName += " [OSM preview - requires validation]";
            for (int i = 0; i < parsed.geometry.Count; i++)
                route.pathPoints[i] = CoordinateConverter.LocalOriginGeoToWorld(parsed.geometry[i].lat,
                    parsed.geometry[i].lon, -1.286389, 36.817223);
            string assetPath = AssetDatabase.GenerateUniqueAssetPath(folder + "/OSM_" + parsed.osmRelationId + ".asset");
            AssetDatabase.CreateAsset(route, assetPath);
            count++;
        }
        AssetDatabase.SaveAssets();
        Debug.Log($"Imported {count} real OSM previews. Inspect and rebuild using Route Editor before assigning gameplay routes.");
    }
}
#endif
