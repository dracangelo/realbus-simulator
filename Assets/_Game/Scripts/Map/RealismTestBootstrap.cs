using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Phase 5 fixture. Uses generated training service and bundled OSM roads.</summary>
public class RealismTestBootstrap : MonoBehaviour
{
    public GameplayTestBootstrap gameplay;
    public bool autoRunBenchmark;
    BusController bus;
    PassengerManager passengers;
    MaintenanceSystem maintenance;
    WeatherSystem weather;
    TimeOfDaySystem clock;
    VehiclePool traffic;
    TrafficStressTest benchmark;
    ParticleSystem rainParticles;
    Transform wiper;
    Material trafficMaterial;
    readonly List<GameObject> templates = new List<GameObject>();
    AudioClip siren;
    bool ready, showCabin = true;
    string status = "Preparing Phase 5 realism...";

    IEnumerator Start()
    {
        if (GameState.Instance == null) new GameObject("Simulation state").AddComponent<GameState>();
        if (gameplay == null) gameplay = GetComponent<GameplayTestBootstrap>();
        while (gameplay != null && !gameplay.IsReady) yield return null;
        if (gameplay == null) yield break;
        bus = gameplay.map.Bus;
        if (bus.GetComponent<TrafficParticipant>() == null) bus.gameObject.AddComponent<TrafficParticipant>();
        maintenance = bus.gameObject.AddComponent<MaintenanceSystem>();
        passengers = PassengerManager.Instance;
        passengers.useAdvancedPassengerSimulation = true;
        passengers.useCapacityOverride = true; passengers.maxBusCapacity = passengers.doorOpenCapacityLimit = 80;
        passengers.seatedCapacity = 40;
        passengers.doorLocalPosition = new Vector3(1.3f, 1.1f, 3.5f);
        passengers.platformLocalPosition = new Vector3(2.2f, 0.85f, 3.5f);
        passengers.passengerSpawner = new GameObject("Passenger density").AddComponent<PassengerSpawner>();
        var visuals = new GameObject("Passenger visual pool").AddComponent<PassengerVisualPool>();
        visuals.passengers = passengers; visuals.bus = bus;
        // An open-top fixture exposes the agents; the body collider remains in place.
        if (bus.modelRoot != null) bus.modelRoot.GetComponent<Renderer>().enabled = false;
        BuildWeather();
        var bridge = new GameObject("Directed traffic roads").AddComponent<RoadTrafficBridge>();
        yield return bridge.Build(gameplay.map.Graph);
        BuildTraffic(bridge.Graph);
        BuildCrossing();
        var route = MissionManager.Instance.currentRoute;
        route.busStops[route.busStops.Length - 1].stopName = "Training depot";
        route.SyncLegacyStopsFromBusStops();
        // Let controller Start methods initialize before applying the requested state.
        yield return null;
        weather.ApplyWeather(WeatherState.LightRain, 0.5f, 22f);
        ready = true; status = "Phase 5 — passengers, traffic, weather, maintenance";
        if (autoRunBenchmark)
        {
            passengers.passengerSpawner.baselineSpawnMin = passengers.passengerSpawner.baselineSpawnMax = 20;
            gameplay.BeginTrainingMission();
            float deadline = Time.time + 15f;
            while (!MissionManager.Instance.routeActive && Time.time < deadline) yield return null;
            if (MissionManager.Instance.routeActive)
            {
                // Docking references need an Update after countdown departure.
                yield return null; passengers.RequestDoors();
                yield return new WaitForSeconds(1f);
                benchmark.Begin();
            }
        }
    }

    void BuildWeather()
    {
        var root = new GameObject("Realism weather");
        var sky = root.AddComponent<SkyController>();
        foreach (var light in FindObjectsByType<Light>(FindObjectsSortMode.None)) if (light.type == LightType.Directional) { sky.sunLight = light; break; }
        var rain = root.AddComponent<RainController>();
        var fog = root.AddComponent<FogController>();
        weather = root.AddComponent<WeatherSystem>();
        weather.forceWeather = true; weather.forcedWeatherState = WeatherState.LightRain;
        weather.rainController = rain; weather.fogController = fog; weather.skyController = sky;
        clock = root.AddComponent<TimeOfDaySystem>(); clock.skyController = sky; clock.directionalSun = sky.sunLight;
        var particles = new GameObject("Local rain");
        rainParticles = particles.AddComponent<ParticleSystem>(); rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        var main = rainParticles.main; main.startLifetime = 1.4f; main.startSpeed = 16f; main.startSize = 0.04f;
        main.maxParticles = QualitySettings.GetQualityLevel() == 0 ? 300 : 1000; main.simulationSpace = ParticleSystemSimulationSpace.World;
        var shape = rainParticles.shape; shape.shapeType = ParticleSystemShapeType.Box; shape.scale = new Vector3(35f, 35f, 1f);
        particles.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        rain.rainParticles = rain.drizzleParticles = rain.thunderstormParticles = rainParticles;
        var particleMaterial = new Material(Shader.Find("Universal Render Pipeline/Particles/Unlit") ?? Shader.Find("Sprites/Default"));
        particleMaterial.color = new Color(0.65f, 0.8f, 0.9f, 0.55f);
        rainParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial = particleMaterial;
        var roads = FindFirstObjectByType<RoadNetworkSurface>();
        if (roads != null) { rain.wetRoadRenderers = roads.GetComponentsInChildren<Renderer>(); roads.gameObject.AddComponent<RoadGeometryLod>().viewer = bus.transform; }
        wiper = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        wiper.name = "Wiper"; Destroy(wiper.GetComponent<Collider>()); wiper.SetParent(bus.transform, false);
        wiper.localPosition = new Vector3(0f, 2f, 6.05f); wiper.localScale = new Vector3(0.04f, 1.1f, 0.04f);
        clock.busHeadLights = new[] { Lamp("Bus headlight", bus.transform, new Vector3(0f, 1f, 6f), LightType.Spot) };
        clock.busInteriorLights = new[] { Lamp("Bus interior light", bus.transform, new Vector3(0f, 2.8f, 0f), LightType.Point) };
        var street = new List<Light>();
        foreach (var point in MissionManager.Instance.currentRoute.pathPoints)
        {
            if (street.Count > 0 && Vector3.Distance(street[street.Count - 1].transform.position, point) < 80f) continue;
            street.Add(Lamp("Street light", transform, point + new Vector3(4f, 5f, 0f), LightType.Point));
        }
        clock.streetLights = street.ToArray();
    }

    Light Lamp(string label, Transform parent, Vector3 local, LightType type)
    {
        var light = new GameObject(label).AddComponent<Light>(); light.transform.SetParent(parent, false);
        light.transform.localPosition = local; light.type = type; light.range = 22f; light.spotAngle = 65f;
        light.shadows = LightShadows.None; return light;
    }

    void BuildTraffic(AIRoadGraph graph)
    {
        trafficMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard")); trafficMaterial.color = new Color(0.8f, 0.35f, 0.1f);
        var car = VehicleTemplate("Car", new Vector3(1.8f, 1.4f, 4.2f));
        var truck = VehicleTemplate("Truck", new Vector3(2.2f, 2.3f, 7f));
        var motorcycle = VehicleTemplate("Motorcycle", new Vector3(0.6f, 1.2f, 2f));
        var ambulance = VehicleTemplate("Ambulance", new Vector3(2f, 2f, 5f));
        var emergency = ambulance.AddComponent<AmbulanceBehaviour>();
        emergency.emergencyLights = new[] { Lamp("Emergency beacon", ambulance.transform, new Vector3(0f, 2.3f, 0f), LightType.Point) };
        emergency.emergencyLights[0].color = Color.blue; emergency.emergencyLights[0].range = 8f;
        emergency.sirenSource = ambulance.AddComponent<AudioSource>(); emergency.sirenSource.loop = true; emergency.sirenSource.spatialBlend = 1f; emergency.sirenSource.maxDistance = 100f;
        var samples = new float[22050];
        for (int i = 0; i < samples.Length; i++) samples[i] = Mathf.Sin(2f * Mathf.PI * (i < 11025 ? 660f : 880f) * i / 22050f) * 0.12f;
        siren = AudioClip.Create("Training siren", samples.Length, 1, 22050, false); siren.SetData(samples, 0); emergency.sirenSource.clip = siren;
        traffic = new GameObject("Traffic pool 50").AddComponent<VehiclePool>(); traffic.roadGraph = graph;
        traffic.maxFullAiVehicles = 18; traffic.logSpawnEvents = traffic.logBusDistance = false;
        traffic.minActiveFraction = 0.4f; traffic.maxActiveFraction = 1f;
        traffic.prefabs = new[] { Entry(car, AIVehicleController.VehicleType.Car, 0.55f), Entry(truck, AIVehicleController.VehicleType.Truck, 0.2f), Entry(motorcycle, AIVehicleController.VehicleType.Motorcycle, 0.2f), Entry(ambulance, AIVehicleController.VehicleType.Emergency, 0.05f) };
        var buses = new GameObject("Scheduled AI bus").AddComponent<AIBusScheduleSpawner>();
        buses.aiBusPrefab = VehicleTemplate("Scheduled bus", new Vector3(2.55f, 2.5f, 10f));
        var route = MissionManager.Instance.currentRoute;
        var sequence = new int[route.pathPoints.Length];
        for (int i = 0; i < sequence.Length; i++) sequence[i] = graph.GetNearestNodeIndex(route.pathPoints[i]);
        buses.routes = new[] { new AIBusScheduleSpawner.ScheduledAIBusRoute { graph = graph, nodeSequence = sequence, headwayMinutes = 5f, maxConcurrentBuses = 1 } };
        benchmark = gameObject.AddComponent<TrafficStressTest>(); benchmark.autoStart = false; benchmark.vehiclePool = traffic;
    }

    GameObject VehicleTemplate(string label, Vector3 size)
    {
        var root = new GameObject(label + " template"); root.SetActive(false); root.transform.SetParent(transform, false);
        var body = GameObject.CreatePrimitive(PrimitiveType.Cube); body.transform.SetParent(root.transform, false); body.transform.localPosition = Vector3.up * (size.y * 0.5f); body.transform.localScale = size;
        body.GetComponent<Renderer>().sharedMaterial = trafficMaterial;
        var rb = root.AddComponent<Rigidbody>(); rb.mass = 1500f; rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        root.AddComponent<AIVehicleController>().vehicleLength = size.z; root.AddComponent<SplineVehicle>().enabled = false;
        root.AddComponent<TrafficParticipant>().length = size.z;
        var detailed = new List<Renderer> { body.GetComponent<Renderer>() };
        for (int i = 0; i < 4; i++)
        {
            var wheel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            Destroy(wheel.GetComponent<Collider>()); wheel.transform.SetParent(root.transform, false);
            wheel.transform.localPosition = new Vector3((i % 2 == 0 ? -1f : 1f) * size.x * 0.5f, 0.45f, (i < 2 ? 1f : -1f) * size.z * 0.3f);
            wheel.transform.localRotation = Quaternion.Euler(0f, 0f, 90f); wheel.transform.localScale = new Vector3(0.8f, 0.12f, 0.8f);
            wheel.GetComponent<Renderer>().sharedMaterial = trafficMaterial; detailed.Add(wheel.GetComponent<Renderer>());
        }
        var reduced = GameObject.CreatePrimitive(PrimitiveType.Cube); Destroy(reduced.GetComponent<Collider>());
        reduced.transform.SetParent(root.transform, false); reduced.transform.localPosition = body.transform.localPosition; reduced.transform.localScale = size;
        reduced.GetComponent<Renderer>().sharedMaterial = trafficMaterial;
        var billboard = GameObject.CreatePrimitive(PrimitiveType.Quad); Destroy(billboard.GetComponent<Collider>());
        billboard.transform.SetParent(root.transform, false); billboard.transform.localPosition = body.transform.localPosition; billboard.transform.localScale = new Vector3(size.z, size.y, 1f);
        billboard.GetComponent<Renderer>().sharedMaterial = trafficMaterial;
        var lod = root.AddComponent<VehicleDistanceLod>(); lod.detailed = detailed.ToArray(); lod.reduced = new[] { reduced.GetComponent<Renderer>() }; lod.billboard = new[] { billboard.GetComponent<Renderer>() };
        lod.reduced[0].enabled = lod.billboard[0].enabled = false;
        templates.Add(root); return root;
    }
    VehiclePool.VehiclePrefabEntry Entry(GameObject prefab, AIVehicleController.VehicleType type, float weight) => new VehiclePool.VehiclePrefabEntry { prefab = prefab, type = type, weight = weight };

    void BuildCrossing()
    {
        var signal = FindFirstObjectByType<TrafficLight>(); if (signal == null) return;
        var a = new GameObject("Crossing A").transform; var b = new GameObject("Crossing B").transform;
        a.SetParent(transform, false); b.SetParent(transform, false);
        a.position = signal.transform.position + signal.transform.right * 5f + signal.transform.forward * 5f + Vector3.up * 0.9f;
        b.position = a.position - signal.transform.right * 10f;
        var crossing = new GameObject("Pooled crossing pedestrians").AddComponent<PedestrianSpawner>();
        crossing.maxActivePedestrians = 8; crossing.logSpawnEvents = false;
        crossing.crossings = new[] { new PedestrianSpawner.ZebraCrossing { crossingId = "Training crossing", spawnA = a, spawnB = b, controllingTrafficLight = signal } };
    }

    void Update()
    {
        if (!ready) return;
        rainParticles.transform.position = bus.transform.position + Vector3.up * 14f;
        if (wiper != null) { wiper.gameObject.SetActive(weather.IsRaining()); wiper.localRotation = Quaternion.Euler(0f, 0f, Mathf.Sin(Time.time * 5f) * 55f); }
    }
    void OnGUI()
    {
        GUI.Box(new Rect(680, 410, 550, 235), status);
        if (!ready) return;
        traffic.GetTierCounts(out int full, out int spline, out int dormant, out _);
        GUI.Label(new Rect(695, 438, 520, 25), $"AI {full} physics / {spline} lightweight / {dormant} dormant | {weather.CurrentCompositeWeatherLabel}");
        GUI.Label(new Rect(695, 462, 520, 25), $"Passengers {passengers.currentPassengers}/80 | Satisfaction {passengers.averageSatisfaction:P0} | Brake {maintenance.BrakeConditionPercent:F0}%");
        GUI.Label(new Rect(695, 486, 520, 25), $"Tyres {maintenance.GetAxleConditionPercent(0):F0}/{maintenance.GetAxleConditionPercent(1):F0}/{maintenance.GetAxleConditionPercent(2):F0}% | Engine {maintenance.EngineHours:F2}h");
        WeatherState[] states = { WeatherState.Clear, WeatherState.Overcast, WeatherState.LightRain, WeatherState.HeavyRain, WeatherState.Fog };
        for (int i = 0; i < states.Length; i++) if (GUI.Button(new Rect(695 + i * 103, 515, 100, 28), states[i].ToString())) weather.TransitionWeather(states[i]);
        if (GUI.Button(new Rect(695, 550, 120, 28), "Day / night")) ScheduleManager.Instance.currentTimeMinutes = clock.IsNight ? 480f : 1200f;
        if (GUI.Button(new Rect(825, 550, 120, 28), "Cabin view")) { showCabin = !showCabin; if (bus.modelRoot != null) bus.modelRoot.GetComponent<Renderer>().enabled = !showCabin; }
        if (GUI.Button(new Rect(955, 550, 120, 28), "Benchmark")) benchmark.Begin();
        GUI.Label(new Rect(695, 588, 520, 45), benchmark.Running ? "Benchmark running: remain on the road; CSV records actual active tiers." : benchmark.LastReportPath ?? "Weather transitions take 3–5 minutes. Start the mission in the Phase 4 panel.");
    }
    void OnDestroy()
    {
        if (rainParticles != null) Destroy(rainParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial);
        if (trafficMaterial != null) Destroy(trafficMaterial);
        if (siren != null) Destroy(siren);
    }
}
