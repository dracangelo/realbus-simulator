#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class RouteEditorWindow : EditorWindow
{
    BusRoute route;
    BusRoute mergeWith;
    CoordinateConverter converter;
    Vector2 scroll;
    Color routeColor = new Color(1f, 0.6f, 0.2f, 0.9f);
    int selectedStopIndex = -1;

    // Add-stop mode
    bool addStopMode;
    string newStopName = "New Stop";
    string newStopId = "";
    bool insertAfterSelected = true;

    // Split/Merge options
    int splitAfterIndex = 3;
    bool mergeDeduplicateJunction = true;

    [MenuItem("Tools/RealBus/Routes/Route Editor Window")]
    public static void Open()
    {
        GetWindow<RouteEditorWindow>("Route Editor");
    }

    void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    void OnGUI()
    {
        route = (BusRoute)EditorGUILayout.ObjectField("BusRoute", route, typeof(BusRoute), false);
        converter = (CoordinateConverter)EditorGUILayout.ObjectField("CoordinateConverter (scene)", converter, typeof(CoordinateConverter), true);
        routeColor = EditorGUILayout.ColorField("Route color", routeColor);
        mergeWith = (BusRoute)EditorGUILayout.ObjectField("Merge with", mergeWith, typeof(BusRoute), false);

        if (route == null)
        {
            EditorGUILayout.HelpBox("Assign a BusRoute asset (generated in Assets/_Game/Routes).", MessageType.Info);
            return;
        }

        if (GUILayout.Button("Focus SceneView on route"))
            FocusOnRoute();

        GUILayout.Space(6);
        scroll = EditorGUILayout.BeginScrollView(scroll);
        EditorGUILayout.LabelField("Stops", EditorStyles.boldLabel);

        if (route.busStops == null || route.busStops.Length == 0)
        {
            EditorGUILayout.HelpBox("This route has no BusStop[] data.", MessageType.Warning);
        }
        else
        {
            for (int i = 0; i < route.busStops.Length; i++)
            {
                var s = route.busStops[i];
                EditorGUILayout.BeginHorizontal();
                bool isSelected = i == selectedStopIndex;
                var labelStyle = new GUIStyle(EditorStyles.label);
                if (isSelected) labelStyle.fontStyle = FontStyle.Bold;
                if (GUILayout.Button(isSelected ? "●" : "○", GUILayout.Width(22)))
                    selectedStopIndex = i;
                EditorGUILayout.LabelField($"{i + 1}. {s.stopName}", labelStyle, GUILayout.Width(220));
                if (GUILayout.Button("Delete", GUILayout.Width(60)))
                {
                    DeleteStop(i);
                    break;
                }
                if (GUILayout.Button("Up", GUILayout.Width(40)) && i > 0)
                    SwapStops(i, i - 1);
                if (GUILayout.Button("Down", GUILayout.Width(50)) && i < route.busStops.Length - 1)
                    SwapStops(i, i + 1);
                EditorGUILayout.EndHorizontal();
            }
        }

        GUILayout.Space(10);
        EditorGUILayout.LabelField("Operations", EditorStyles.boldLabel);

        // Add stop
        EditorGUILayout.LabelField("Add Stop", EditorStyles.boldLabel);
        newStopName = EditorGUILayout.TextField("Stop name", newStopName);
        newStopId = EditorGUILayout.TextField("Stop id (optional)", newStopId);
        insertAfterSelected = EditorGUILayout.ToggleLeft("Insert after selected stop", insertAfterSelected);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(addStopMode ? "Cancel add-stop mode" : "Enter add-stop mode (click in Scene)"))
            addStopMode = !addStopMode;
        if (GUILayout.Button("Add stop at bus/scene origin"))
            AddStopAtWorld(Vector3.zero);
        EditorGUILayout.EndHorizontal();
        if (addStopMode)
            EditorGUILayout.HelpBox("Add-stop mode: click in Scene view to place a stop. (Uses CoordinateConverter.WorldToGeoPosition).", MessageType.Info);

        GUILayout.Space(8);
        // Split route
        EditorGUILayout.LabelField("Split Route", EditorStyles.boldLabel);
        splitAfterIndex = EditorGUILayout.IntSlider("Split after stop #", splitAfterIndex, 1, Mathf.Max(1, (route.busStops?.Length ?? 2) - 2));
        if (GUILayout.Button("Split into 2 new route assets"))
            SplitRouteAsset(splitAfterIndex - 1);

        GUILayout.Space(8);
        // Merge routes
        EditorGUILayout.LabelField("Merge Routes", EditorStyles.boldLabel);
        mergeDeduplicateJunction = EditorGUILayout.ToggleLeft("De-duplicate junction stop (if very close)", mergeDeduplicateJunction);
        if (GUILayout.Button("Merge into new route asset"))
            MergeRoutesAsset();

        GUILayout.Space(8);
        if (GUILayout.Button("Reverse stops (return variant helper)"))
        {
            ReverseStops();
        }
        EditorGUILayout.EndScrollView();
    }

    void OnSceneGUI(SceneView sv)
    {
        if (route == null) return;
        var conv = converter != null ? converter : FindFirstObjectByType<CoordinateConverter>();
        if (conv == null) return;

        Handles.color = routeColor;

        // Draw path points if present.
        if (route.pathPoints != null && route.pathPoints.Length >= 2)
        {
            Handles.DrawAAPolyLine(4f, route.pathPoints);
        }

        if (route.busStops == null) return;

        // Add stop mode: click-to-place.
        if (addStopMode)
        {
            HandleAddStopClick(conv);
            sv.Repaint();
        }

        for (int i = 0; i < route.busStops.Length; i++)
        {
            var stop = route.busStops[i];
            Vector3 p = conv.GeoToWorldPosition(stop.latitude, stop.longitude);
            float size = HandleUtility.GetHandleSize(p) * 0.1f;

            EditorGUI.BeginChangeCheck();
            Vector3 moved = Handles.FreeMoveHandle(p, size, Vector3.zero, Handles.SphereHandleCap);
            if (EditorGUI.EndChangeCheck())
            {
                var gps = conv.WorldToGeoPosition(moved);
                stop.latitude = gps.lat;
                stop.longitude = gps.lon;
                route.busStops[i] = stop;
                EditorUtility.SetDirty(route);
            }

            if (i == selectedStopIndex)
            {
                Handles.color = Color.white;
                Handles.DrawWireDisc(p + Vector3.up * 0.2f, Vector3.up, size * 3f);
                Handles.color = routeColor;
            }
            Handles.Label(p + Vector3.up * (size * 2f), $"{i + 1}: {stop.stopName}");
        }
    }

    void FocusOnRoute()
    {
        if (route.pathPoints == null || route.pathPoints.Length == 0) return;
        var bounds = new Bounds(route.pathPoints[0], Vector3.zero);
        for (int i = 1; i < route.pathPoints.Length; i++)
            bounds.Encapsulate(route.pathPoints[i]);

        SceneView.lastActiveSceneView.Frame(bounds, false);
    }

    void DeleteStop(int index)
    {
        var list = new List<BusStop>(route.busStops);
        list.RemoveAt(index);
        route.busStops = list.ToArray();
        if (selectedStopIndex == index) selectedStopIndex = -1;
        if (selectedStopIndex > index) selectedStopIndex--;
        EditorUtility.SetDirty(route);
    }

    void SwapStops(int a, int b)
    {
        var arr = route.busStops;
        var tmp = arr[a];
        arr[a] = arr[b];
        arr[b] = tmp;
        route.busStops = arr;
        EditorUtility.SetDirty(route);
    }

    void ReverseStops()
    {
        var arr = route.busStops;
        System.Array.Reverse(arr);
        route.busStops = arr;
        selectedStopIndex = -1;
        EditorUtility.SetDirty(route);
    }

    void HandleAddStopClick(CoordinateConverter conv)
    {
        Event e = Event.current;
        if (e == null) return;

        if (e.type != EventType.MouseDown || e.button != 0 || e.alt)
            return;

        Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
        // Prefer colliders (roads/ground). If none, drop onto XZ plane at y=0.
        Vector3 world;
        if (Physics.Raycast(ray, out RaycastHit hit, 50000f))
            world = hit.point;
        else
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            if (!plane.Raycast(ray, out float enter)) return;
            world = ray.GetPoint(enter);
        }

        AddStopAtWorld(world, conv);
        e.Use();
    }

    void AddStopAtWorld(Vector3 world)
    {
        var conv = converter != null ? converter : FindFirstObjectByType<CoordinateConverter>();
        if (conv == null) return;
        AddStopAtWorld(world, conv);
    }

    void AddStopAtWorld(Vector3 world, CoordinateConverter conv)
    {
        if (route.busStops == null)
            route.busStops = new BusStop[0];

        var gps = conv.WorldToGeoPosition(world);
        var stop = new BusStop
        {
            stopId = string.IsNullOrEmpty(newStopId) ? System.Guid.NewGuid().ToString("N").Substring(0, 10) : newStopId,
            stopName = string.IsNullOrEmpty(newStopName) ? "New Stop" : newStopName,
            latitude = gps.lat,
            longitude = gps.lon,
            headingDegrees = 0f
        };

        var list = new List<BusStop>(route.busStops);
        int insertIndex = list.Count;
        if (insertAfterSelected && selectedStopIndex >= 0 && selectedStopIndex < list.Count)
            insertIndex = selectedStopIndex + 1;
        list.Insert(insertIndex, stop);
        route.busStops = list.ToArray();
        selectedStopIndex = insertIndex;
        EditorUtility.SetDirty(route);
    }

    void SplitRouteAsset(int afterIndex)
    {
        if (route.busStops == null || route.busStops.Length < 2) return;
        if (afterIndex < 0 || afterIndex >= route.busStops.Length - 1) return;

        var aStops = new List<BusStop>();
        var bStops = new List<BusStop>();
        for (int i = 0; i <= afterIndex; i++) aStops.Add(route.busStops[i]);
        for (int i = afterIndex + 1; i < route.busStops.Length; i++) bStops.Add(route.busStops[i]);

        string basePath = AssetDatabase.GetAssetPath(route);
        string folder = string.IsNullOrEmpty(basePath) ? "Assets/_Game/Routes" : System.IO.Path.GetDirectoryName(basePath);

        var a = ScriptableObject.CreateInstance<BusRoute>();
        a.routeName = $"{route.routeName} (Part A)";
        a.routeNumber = route.routeNumber;
        a.baseFare = route.baseFare;
        a.busStops = aStops.ToArray();
        RecomputeMetrics(a);

        var b = ScriptableObject.CreateInstance<BusRoute>();
        b.routeName = $"{route.routeName} (Part B)";
        b.routeNumber = route.routeNumber;
        b.baseFare = route.baseFare;
        b.busStops = bStops.ToArray();
        RecomputeMetrics(b);

        EnsureFolder(folder);
        AssetDatabase.CreateAsset(a, AssetDatabase.GenerateUniqueAssetPath($"{folder}/{MakeSafe(a.routeName)}.asset"));
        AssetDatabase.CreateAsset(b, AssetDatabase.GenerateUniqueAssetPath($"{folder}/{MakeSafe(b.routeName)}.asset"));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    void MergeRoutesAsset()
    {
        if (route == null || mergeWith == null) return;
        if (route.busStops == null || mergeWith.busStops == null) return;
        if (route.busStops.Length == 0 || mergeWith.busStops.Length == 0) return;

        var mergedStops = new List<BusStop>();
        mergedStops.AddRange(route.busStops);

        int startIndex = 0;
        if (mergeDeduplicateJunction)
        {
            var conv = converter != null ? converter : FindFirstObjectByType<CoordinateConverter>();
            if (conv != null)
            {
                Vector3 aLast = conv.GeoToWorldPosition(route.busStops[^1].latitude, route.busStops[^1].longitude);
                Vector3 bFirst = conv.GeoToWorldPosition(mergeWith.busStops[0].latitude, mergeWith.busStops[0].longitude);
                if ((aLast - bFirst).sqrMagnitude <= (25f * 25f))
                    startIndex = 1;
            }
        }

        for (int i = startIndex; i < mergeWith.busStops.Length; i++)
            mergedStops.Add(mergeWith.busStops[i]);

        var asset = ScriptableObject.CreateInstance<BusRoute>();
        asset.routeName = $"{route.routeName} + {mergeWith.routeName}";
        asset.routeNumber = string.IsNullOrEmpty(route.routeNumber) ? mergeWith.routeNumber : route.routeNumber;
        asset.baseFare = Mathf.Max(route.baseFare, mergeWith.baseFare);
        asset.busStops = mergedStops.ToArray();

        // Best-effort pathPoints merge (if available).
        var pts = new List<Vector3>();
        if (route.pathPoints != null && route.pathPoints.Length > 0) pts.AddRange(route.pathPoints);
        if (mergeWith.pathPoints != null && mergeWith.pathPoints.Length > 0)
        {
            if (pts.Count > 0 && (pts[^1] - mergeWith.pathPoints[0]).sqrMagnitude < 0.25f)
                pts.AddRange(mergeWith.pathPoints[1..]);
            else
                pts.AddRange(mergeWith.pathPoints);
        }
        asset.pathPoints = pts.Count >= 2 ? pts.ToArray() : null;

        RecomputeMetrics(asset);

        string basePath = AssetDatabase.GetAssetPath(route);
        string folder = string.IsNullOrEmpty(basePath) ? "Assets/_Game/Routes" : System.IO.Path.GetDirectoryName(basePath);
        EnsureFolder(folder);
        AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath($"{folder}/{MakeSafe(asset.routeName)}.asset"));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    void RecomputeMetrics(BusRoute r)
    {
        var conv = converter != null ? converter : FindFirstObjectByType<CoordinateConverter>();
        if (conv == null)
        {
            r.distanceKm = 0f;
            r.estimatedTimeMinutes = 0f;
            return;
        }

        float meters = 0f;
        if (r.pathPoints != null && r.pathPoints.Length >= 2)
        {
            for (int i = 0; i < r.pathPoints.Length - 1; i++)
                meters += Vector3.Distance(r.pathPoints[i], r.pathPoints[i + 1]);
        }
        else if (r.busStops != null && r.busStops.Length >= 2)
        {
            for (int i = 0; i < r.busStops.Length - 1; i++)
            {
                Vector3 a = conv.GeoToWorldPosition(r.busStops[i].latitude, r.busStops[i].longitude);
                Vector3 b = conv.GeoToWorldPosition(r.busStops[i + 1].latitude, r.busStops[i + 1].longitude);
                meters += Vector3.Distance(a, b);
            }
        }

        r.distanceKm = meters / 1000f;
        float avgKmh = r.difficulty >= 4 ? 22f : r.difficulty == 3 ? 28f : 40f;
        float driveMin = (r.distanceKm / Mathf.Max(5f, avgKmh)) * 60f;
        float dwellMin = (r.busStops != null ? r.busStops.Length : 0) * 0.35f;
        r.estimatedTimeMinutes = driveMin + dwellMin;
    }

    static string MakeSafe(string s)
    {
        foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            s = s.Replace(c, '_');
        return s.Replace(" ", "_");
    }

    static void EnsureFolder(string folderPath)
    {
        if (string.IsNullOrEmpty(folderPath)) return;
        if (AssetDatabase.IsValidFolder(folderPath)) return;

        // Create nested folders if needed.
        string[] parts = folderPath.Split('/');
        if (parts.Length == 0) return;
        string cur = parts[0];
        for (int i = 1; i < parts.Length; i++)
        {
            string next = cur + "/" + parts[i];
            if (!AssetDatabase.IsValidFolder(next))
                AssetDatabase.CreateFolder(cur, parts[i]);
            cur = next;
        }
    }
}
#endif

