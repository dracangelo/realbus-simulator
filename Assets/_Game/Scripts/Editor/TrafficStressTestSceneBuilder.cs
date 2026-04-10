#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class TrafficStressTestSceneBuilder
{
    const string ScenePath = "Assets/_Game/Scenes/TrafficStressTest_A54.unity";

    [MenuItem("Tools/RealBus/Build Traffic Stress Test Scene (A54)")]
    public static void Build()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        scene.name = "TrafficStressTest_A54";

        var schedule = new GameObject("ScheduleManager").AddComponent<ScheduleManager>();
        schedule.gameStartTimeMinutes = 8f * 60f;
        schedule.secondsPerGameMinute = 1f;

        var graphGo = new GameObject("AIRoadGraph");
        var graph = graphGo.AddComponent<AIRoadGraph>();
        graph.nodes = new AIRoadGraph.RoadNode[0]; // Fill from your A54 node transforms.

        var poolGo = new GameObject("VehiclePool");
        var pool = poolGo.AddComponent<VehiclePool>();
        pool.roadGraph = graph;
        pool.poolSizePerScene = 200; // stress upper bound
        pool.maxFullAiVehicles = 18; // strict budget
        pool.fullAiRadiusMeters = 200f;
        pool.splineRadiusMeters = 400f;

        var testerGo = new GameObject("TrafficStressTest");
        var tester = testerGo.AddComponent<TrafficStressTest>();
        tester.vehiclePool = pool;
        tester.startVehicles = 10;
        tester.stepVehicles = 10;
        tester.maxVehicles = 200;
        tester.settleSecondsPerStep = 12f;
        tester.sampleFrames = 300;

        var hudGo = new GameObject("TrafficDebugHUD");
        var hud = hudGo.AddComponent<TrafficDebugHUD>();
        hud.vehiclePool = pool;

        EnsureFolder("Assets/_Game/Scenes");
        EditorSceneManager.SaveScene(scene, ScenePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Traffic stress test scene created at " + ScenePath + ". Assign A54 road nodes and vehicle prefabs before running.");
    }

    static void EnsureFolder(string path)
    {
        if (AssetDatabase.IsValidFolder(path))
            return;

        var parts = path.Split('/');
        string current = parts[0];
        for (int i = 1; i < parts.Length; i++)
        {
            string next = current + "/" + parts[i];
            if (!AssetDatabase.IsValidFolder(next))
                AssetDatabase.CreateFolder(current, parts[i]);
            current = next;
        }
    }
}
#endif
