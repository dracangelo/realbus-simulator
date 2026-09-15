using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Phase 4 integration fixture: generated training service on actual Nairobi roads.</summary>
public class GameplayTestBootstrap : MonoBehaviour
{
    public MapTestBootstrap map;
    public bool IsReady => route != null && mission != null && freeDrive != null && freeDrive.sessionActive;
    MissionManager mission;
    FreeDriveSession freeDrive;
    PassengerManager passengers;
    PhotoModeController photo;
    BusController bus;
    BusRoute route;
    MissionData data;
    string status = "Loading Nairobi roads...";
    bool collapsed;
    Vector3 startPosition;
    Quaternion startRotation;
    float resultReveal;
    bool briefingOpen;
    bool showSchedule;
    RouteMapGraphic routeMap;
    LineRenderer routeLine;
    Material routeMaterial;
    FuelSystem fuel;
    AudioClip bellClip;

    void Update()
    {
        if (mission == null || freeDrive == null) return;
        bool showRoute = briefingOpen || mission.routeActive;
        if (routeMap != null) routeMap.gameObject.SetActive(showRoute);
        if (routeLine != null) routeLine.enabled = mission.routeActive;
        if (mission.LastResult != null) resultReveal = Mathf.Min(1f, resultReveal + Time.unscaledDeltaTime);
    }

    IEnumerator Start()
    {
        if (map == null) map = GetComponent<MapTestBootstrap>();
        while (map != null && !map.IsReady) yield return null;
        if (map == null) yield break;
        bus = map.Bus;
        if (GPSManager.Instance == null) new GameObject("GPS service").AddComponent<GPSManager>();
        var schedule = new GameObject("Schedule").AddComponent<ScheduleManager>();
        var score = bus.gameObject.AddComponent<ScoreTracker>();
        bus.gameObject.AddComponent<MissionDrivingMonitor>().roadGraph = map.Graph;
        passengers = new GameObject("Passengers").AddComponent<PassengerManager>();
        passengers.useAdvancedPassengerSimulation = false;
        passengers.bellAudioSource = passengers.gameObject.AddComponent<AudioSource>();
        passengers.bellAudioSource.playOnAwake = false;
        const int sampleRate = 22050;
        var samples = new float[sampleRate / 3];
        for (int i = 0; i < samples.Length; i++)
        {
            float t = (float)i / sampleRate;
            samples[i] = Mathf.Sin(2f * Mathf.PI * 880f * t) * Mathf.Exp(-12f * t) * 0.35f;
        }
        bellClip = AudioClip.Create("Training stop bell", samples.Length, 1, sampleRate, false);
        bellClip.SetData(samples, 0);
        passengers.bellClip = bellClip;
        freeDrive = new GameObject("Free drive").AddComponent<FreeDriveSession>();
        freeDrive.autoStart = false;
        fuel = bus.gameObject.AddComponent<FuelSystem>();
        fuel.busController = bus;
        fuel.passengerManager = passengers;
        fuel.freeDriveSession = freeDrive;
        photo = new GameObject("Photo mode").AddComponent<PhotoModeController>();
        photo.targetCamera = Camera.main;
        photo.busTransform = bus.transform;
        mission = new GameObject("Mission").AddComponent<MissionManager>();
        mission.autoInitialize = false;
        mission.busController = bus;
        mission.repositionBusOnStart = false;
        mission.OnMissionCompleted += _ => resultReveal = 0f;
        var load = bus.GetComponent<PassengerLoadDynamics>();
        if (load != null) load.passengerManager = passengers;
        var door = new GameObject("Front door docking reference");
        door.transform.SetParent(bus.transform, false);
        door.transform.localPosition = new Vector3(1.275f, 0f, 3.5f);
        bus.dockingReference = door.transform;
        if (!BuildTrainingRoute(map.Graph, map.Converter))
        { status = "No suitable connected training path in this city pack."; yield break; }
        status = "Generated Nairobi training service — three stops. Use O / Open doors while aligned at each marker.";
        yield return null;
        freeDrive.StartSession();
    }

    bool BuildTrainingRoute(RoadGraph graph, CoordinateConverter converter)
    {
        int start = graph.FindNearestNodeIndex(bus.transform.position);
        if (start < 0) return false;
        List<int> path = null;
        for (int i = 0; i < graph.nodes.Count; i++)
        {
            float distance = Vector3.Distance(graph.nodes[start].world, graph.nodes[i].world);
            if (distance < 500f || distance > 900f) continue;
            var candidate = graph.FindPathAStar(start, i);
            if (candidate.Count < 8) continue;
            float length = 0f;
            for (int j = 1; j < candidate.Count; j++) length += Vector3.Distance(graph.nodes[candidate[j - 1]].world, graph.nodes[candidate[j]].world);
            if (length > 1500f) continue;
            path = candidate; break;
        }
        if (path == null) return false;
        route = ScriptableObject.CreateInstance<BusRoute>();
        route.name = route.routeName = "Nairobi training service (generated)";
        route.routeNumber = "TRAINING";
        route.isRoadPathValidated = true;
        route.pathPoints = new Vector3[path.Count];
        route.geometryLatLonFlat = new double[path.Count * 2];
        for (int i = 0; i < path.Count; i++)
        {
            var node = graph.nodes[path[i]];
            route.pathPoints[i] = node.world;
            route.geometryLatLonFlat[2 * i] = node.lat;
            route.geometryLatLonFlat[2 * i + 1] = node.lon;
            if (i > 0) route.distanceKm += Vector3.Distance(route.pathPoints[i - 1], node.world) / 1000f;
        }
        route.busStops = new BusStop[3];
        int[] stops = { 0, path.Count / 2, path.Count - 1 };
        for (int i = 0; i < 3; i++)
        {
            int index = stops[i];
            Vector3 point = route.pathPoints[index];
            Vector3 forward = index + 1 < path.Count ? route.pathPoints[index + 1] - point : point - route.pathPoints[index - 1];
            Quaternion rotation = Quaternion.LookRotation(forward);
            var gps = converter.WorldToGeoPosition(point);
            route.busStops[i] = new BusStop { stopId = "training-" + i, stopName = i == 0 ? "Training departure" : i == 2 ? "Training terminus" : "Training stop",
                latitude = gps.lat, longitude = gps.lon, headingDegrees = rotation.eulerAngles.y };
            var zone = new GameObject(route.busStops[i].stopName);
            zone.transform.position = point + rotation * bus.dockingReference.localPosition;
            zone.transform.rotation = rotation;
            var docking = zone.AddComponent<DockingZone>(); docking.stopIndex = i; docking.stopName = zone.name;
            var approach = zone.AddComponent<StopTrigger>(); approach.stopIndex = i; approach.stopName = zone.name;
            var marker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            Destroy(marker.GetComponent<Collider>());
            marker.transform.SetParent(zone.transform, false);
            marker.transform.localPosition = Vector3.up * 1.5f;
            marker.transform.localScale = new Vector3(0.3f, 1.5f, 0.3f);
            if (i == 0) { startPosition = point + Vector3.up * 0.4f; startRotation = rotation; }
        }
        route.SyncLegacyStopsFromBusStops();
        var canvas = FindFirstObjectByType<UnityEngine.Canvas>();
        if (canvas != null)
        {
            var preview = new GameObject("Briefing route map", typeof(RectTransform), typeof(RouteMapGraphic));
            preview.transform.SetParent(canvas.transform, false);
            var rect = preview.GetComponent<RectTransform>();
            rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(1f, 1f);
            rect.anchoredPosition = new Vector2(-20f, -20f); rect.sizeDelta = new Vector2(330f, 130f);
            routeMap = preview.GetComponent<RouteMapGraphic>(); routeMap.color = Color.cyan;
            routeMap.raycastTarget = false; routeMap.SetRoute(route);
        }
        routeLine = new GameObject("Training route guidance").AddComponent<LineRenderer>();
        routeLine.transform.SetParent(transform, false);
        routeMaterial = new Material(Shader.Find("Universal Render Pipeline/Unlit") ?? Shader.Find("Sprites/Default"));
        routeMaterial.color = Color.cyan; routeLine.sharedMaterial = routeMaterial;
        routeLine.startWidth = routeLine.endWidth = 0.35f;
        var guidance = (Vector3[])route.pathPoints.Clone();
        for (int i = 0; i < guidance.Length; i++) guidance[i].y = 0.23f;
        routeLine.positionCount = guidance.Length; routeLine.SetPositions(guidance);
        routeLine.enabled = false;
        data = ScriptableObject.CreateInstance<MissionData>();
        data.route = route; data.missionName = route.routeName;
        data.targetDurationMinutes = route.distanceKm / 20f * 60f + 1f;
        data.minPassengersTarget = 0; data.stretchPassengersTarget = 0;
        data.baseXP = 200; data.timeBonusXP = 0;
        mission.missionData = data; mission.currentRoute = route;
        // Explicit training signal. Live OSM signal placement is available through OsmTrafficSignalSpawner.
        int signalIndex = Mathf.Max(1, path.Count / 3);
        OsmTrafficSignalSpawner.CreateSignal(transform, route.pathPoints[signalIndex],
            Quaternion.LookRotation(route.pathPoints[signalIndex + 1] - route.pathPoints[signalIndex]), 20f);
        var signals = gameObject.AddComponent<OsmTrafficSignalSpawner>();
        signals.converter = converter; signals.graph = graph;
        return true;
    }

    public void BeginTrainingMission()
    {
        briefingOpen = false;
        photo.ExitPhotoMode();
        var body = bus.GetComponent<Rigidbody>();
        body.position = startPosition; body.rotation = startRotation;
        body.linearVelocity = body.angularVelocity = Vector3.zero;
        bus.SetParkingBrake(false);
        mission.StartRoute(route);
    }

    void OnGUI()
    {
        if (mission == null || route == null) { GUI.Label(new Rect(20, 170, 700, 40), status); return; }
        GUI.Box(new Rect(10, 160, 650, collapsed ? 80 : 330), "Phase 4 — Gameplay");
        GUI.Label(new Rect(20, 185, 620, 40), status);
        if (GUI.Button(new Rect(560, 165, 90, 25), collapsed ? "Expand" : "Collapse")) collapsed = !collapsed;
        if (collapsed) return;
        if (!mission.routeActive && mission.CurrentCountdownValue == 0 && GUI.Button(new Rect(20, 230, 190, 30), "Review training mission")) briefingOpen = true;
        if (GUI.Button(new Rect(220, 230, 150, 30), "Free drive")) { briefingOpen = false; photo.ExitPhotoMode(); freeDrive.StartSession(); }
        if (GUI.Button(new Rect(380, 230, 140, 30), "Open doors")) passengers.RequestDoors();
        if (GUI.Button(new Rect(530, 230, 110, 30), "Photo mode")) { if (photo.IsPhotoModeActive) photo.ExitPhotoMode(); else photo.EnterPhotoMode(); }
        if (photo.IsPhotoModeActive && GUI.Button(new Rect(20, 270, 190, 30), "Screenshot (F12)")) photo.CaptureScreenshot();
        if (photo.IsPhotoModeActive)
        {
            if (GUI.RepeatButton(new Rect(220, 270, 65, 30), "Forward")) photo.MovePhotoCamera(Vector3.forward);
            if (GUI.RepeatButton(new Rect(290, 270, 65, 30), "Back")) photo.MovePhotoCamera(Vector3.back);
            if (GUI.RepeatButton(new Rect(360, 270, 65, 30), "Left")) photo.MovePhotoCamera(Vector3.left);
            if (GUI.RepeatButton(new Rect(430, 270, 65, 30), "Right")) photo.MovePhotoCamera(Vector3.right);
            if (GUI.RepeatButton(new Rect(500, 270, 65, 30), "Up")) photo.MovePhotoCamera(Vector3.up);
            if (GUI.RepeatButton(new Rect(570, 270, 65, 30), "Down")) photo.MovePhotoCamera(Vector3.down);
        }
        GUI.Label(new Rect(20, 310, 620, 25), $"{bus.currentSpeedKmh:F1} km/h | Fuel {fuel.CurrentFuelPercent:F1}% {fuel.GetFuelWarningLabel()} | GPS {freeDrive.GetCurrentGPSString()}");
        GUI.Label(new Rect(20, 340, 620, 25), freeDrive.sessionActive
            ? $"Free drive | {freeDrive.distanceDrivenKm:F2} km"
            : $"{mission.missionState} | Countdown {mission.CurrentCountdownValue} | Stop {Mathf.Min(3, mission.currentStopIndex + 1)}/3 | {mission.distanceToNextStop:F0} m | {ScheduleManager.Instance.currentTimeString}");
        if (!freeDrive.sessionActive) GUI.Label(new Rect(20, 370, 620, 25), ScoreTracker.Instance.GetScoreSummary());
        GUI.Label(new Rect(20, 400, 620, 25), $"Doors {(passengers.doorsOpen ? "open" : "closed")} | Passengers {passengers.currentPassengers} | Route {route.distanceKm:F2} km, {data.targetDurationMinutes:F1} minutes, difficulty 1");
        if (mission.routeActive)
        {
            if (GUI.Button(new Rect(20, 435, 150, 30), "Schedule debug")) showSchedule = !showSchedule;
            if (mission.distanceToNextStop <= 50f)
                GUI.Label(new Rect(185, 440, 455, 25), "Approaching stop: align front door with the marker.");
            if (showSchedule)
            {
                var schedule = ScheduleManager.Instance;
                GUI.Box(new Rect(10, 500, 650, 135), "Schedule — expected / actual (minutes since midnight)");
                for (int i = 0; i < route.busStops.Length; i++)
                {
                    string actual = schedule.HasActualArrival(i) ? $"{schedule.GetActualArrival(i):F2} | {schedule.GetStatusLabel(i)} | {schedule.GetLatePenaltyPercent(i):F0}% penalty" : "Pending";
                    GUI.Label(new Rect(20, 530 + i * 30, 620, 25), $"Stop {i + 1} | Expected {schedule.GetScheduledArrival(i):F2} | Actual {actual}");
                }
            }
        }
        if (briefingOpen)
        {
            GUI.Box(new Rect(680, 160, 450, 210), "Mission briefing");
            GUI.Label(new Rect(700, 190, 410, 130), $"{route.routeName}\nDeparture {data.GetDepartureLabel()} | {data.targetDurationMinutes:F1} min | Difficulty 1\n1. Training departure\n2. Training stop\n3. Training terminus\nFollow the cyan road line; align the front door at each marker.");
            if (GUI.Button(new Rect(700, 330, 190, 30), "Depart")) BeginTrainingMission();
            if (GUI.Button(new Rect(900, 330, 190, 30), "Cancel")) briefingOpen = false;
        }
        if (mission.LastResult != null && !freeDrive.sessionActive && !briefingOpen)
        {
            var result = mission.LastResult;
            GUI.Box(new Rect(680, 160, 450, 240), "Mission complete");
            GUI.Label(new Rect(700, 200, 410, 180), $"{result.totalScore * resultReveal:F0}% | {new string('*', Mathf.FloorToInt(result.starRating * resultReveal))}\n+{result.xpEarned} XP | {result.coinsEarned} coins\n{result.totalDistanceKm:F2} km in {result.totalTimeMinutes:F1} minutes\nPunctuality {result.punctualityScore:F0} | Satisfaction {result.satisfactionScore:F0}\nSafety {result.safetyScore:F0} | Efficiency {result.efficiencyScore:F0}\nRed lights: {result.redLightViolations} (-{result.pointDeductions} points)\nRefuel {result.fuelRefuelCostKES} | Service {result.maintenanceCostKES} | Net {result.netEarningsKES} KES");
        }
    }
    void OnDestroy() { if (bellClip != null) Destroy(bellClip); if (routeMaterial != null) Destroy(routeMaterial); if (route != null) Destroy(route); if (data != null) Destroy(data); }
}
