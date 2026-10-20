using UnityEngine;

/// <summary>
/// Evaluates how far the gameplay scene is from being playable.
///
/// The Unity scene load finishes long before the world does: map tiles are still
/// downloading, OSM roads and buildings are still being built, the depot is
/// placed and the player bus is spawned on the mission's first frame. Without an
/// explicit readiness gate the loading overlay dismissed itself while all of that
/// was still running, so the game looked frozen.
///
/// The evaluation is a pure function of <see cref="Milestones"/> so it can be
/// unit tested without a live scene; <see cref="Capture"/> reads the live
/// singletons into that struct.
/// </summary>
public static class WorldReadiness
{
    /// <summary>Live state of the milestones that gate the loading overlay.</summary>
    public struct Milestones
    {
        public bool busSpawned;
        public bool mapInitialised;
        public bool gpsReady;
        public bool roadsBuilt;
        public bool buildingsRingReady;
        public bool tilesLoaded;
        /// <summary>Near/FOV assets are still being warmed (tiles, buildings, traffic, props).</summary>
        public bool preloadRequired;
        public bool preloadReady;
        public float preloadProgress;
        public string preloadStatus;
        /// <summary>Depot must be placed before control is handed over (mission starts only).</summary>
        public bool depotRequired;
        public bool depotBuilt;
    }

    public struct Snapshot
    {
        public float progress;
        public string status;
        public bool ready;
    }

    /// <summary>
    /// Weighted progress for the loading bar: each cleared milestone moves the bar
    /// and names the work still in flight.
    /// </summary>
    public static Snapshot Evaluate(Milestones milestones)
    {
        if (!milestones.busSpawned)
            return new Snapshot { progress = 0.10f, status = "SPAWNING BUS...", ready = false };

        if (!milestones.mapInitialised)
            return new Snapshot { progress = 0.22f, status = "INITIALISING MAP...", ready = false };

        if (!milestones.gpsReady)
            return new Snapshot { progress = 0.32f, status = "CALIBRATING GPS...", ready = false };

        if (!milestones.roadsBuilt)
            return new Snapshot { progress = 0.52f, status = "BUILDING ROAD NETWORK...", ready = false };

        if (!milestones.buildingsRingReady)
            return new Snapshot { progress = 0.72f, status = "LOADING BUILDINGS...", ready = false };

        if (!milestones.tilesLoaded)
            return new Snapshot { progress = 0.86f, status = "DOWNLOADING MAP TILES...", ready = false };

        if (milestones.preloadRequired && !milestones.preloadReady)
        {
            string status = string.IsNullOrWhiteSpace(milestones.preloadStatus)
                ? "PREPARING NEARBY ASSETS..."
                : milestones.preloadStatus;
            // Keep the bar moving through the near-field warm-up instead of
            // parking it at 90% for the whole streaming pass.
            float progress = Mathf.Lerp(0.88f, 0.99f, Mathf.Clamp01(milestones.preloadProgress));
            return new Snapshot { progress = progress, status = status, ready = false };
        }

        if (milestones.depotRequired && !milestones.depotBuilt)
            return new Snapshot { progress = 0.94f, status = "PLACING BUS DEPOT...", ready = false };

        return new Snapshot { progress = 1f, status = "WORLD READY", ready = true };
    }

    /// <summary>Reads the current scene state into <see cref="Milestones"/>.</summary>
    public static Milestones Capture()
    {
        var milestones = new Milestones();

        var mission = MissionManager.Instance;
        var bus = mission != null ? mission.busController : null;
        if (bus == null) bus = Object.FindAnyObjectByType<BusController>();
        milestones.busSpawned = bus != null;

        milestones.mapInitialised = MapTileLoader.Instance != null;
        milestones.gpsReady = GPSManager.Instance != null;

        var roads = OSMRoadMeshBuilder.Instance;
        milestones.roadsBuilt = roads == null || roads.roadsBuilt;

        var buildings = OSMBuildingMeshBuilder.Instance;
        // Treat an absent builder as satisfied: a scene without the building
        // system should never be held back by this gate.
        milestones.buildingsRingReady = buildings == null || buildings.InitialRingReady;

        milestones.tilesLoaded = MapTileLoader.Instance == null || MapTileLoader.Instance.CenterTileLoaded;

        var preload = WorldPreloadCoordinator.Instance;
        milestones.preloadRequired = preload != null && !preload.IsReady;
        milestones.preloadReady = !milestones.preloadRequired;
        milestones.preloadProgress = preload != null ? preload.Progress : 0f;
        milestones.preloadStatus = preload != null ? preload.Status : null;

        var depot = BusDepotManager.Instance;
        milestones.depotRequired = RequiresDepot();
        milestones.depotBuilt = depot != null && depot.currentDepot != null;

        return milestones;
    }

    /// <summary>
    /// The depot is only part of the startup path when a route is actually being
    /// started; free drive never places one, so it must not gate the overlay.
    /// </summary>
    public static bool RequiresDepot()
    {
        var mission = MissionManager.Instance;
        if (mission == null) return false;

        // StartRoute flips routeActive before the countdown, and the depot is
        // placed by the same mission-start sequence that follows it.
        if (mission.routeActive) return true;
        if (mission.CurrentCountdownValue > 0) return true;

        switch (mission.missionState)
        {
            case MissionState.Briefing:
            case MissionState.InProgress:
            case MissionState.AtStop:
                return true;
            default:
                return false;
        }
    }

    /// <summary>Human-readable list of the milestones still outstanding.</summary>
    public static string DescribePending(Milestones milestones)
    {
        var pending = new System.Collections.Generic.List<string>(8);
        if (!milestones.busSpawned) pending.Add("bus");
        if (!milestones.mapInitialised) pending.Add("map");
        if (!milestones.gpsReady) pending.Add("gps");
        if (!milestones.roadsBuilt) pending.Add("roads");
        if (!milestones.buildingsRingReady) pending.Add("buildings");
        if (!milestones.tilesLoaded) pending.Add("tiles");
        if (milestones.preloadRequired && !milestones.preloadReady) pending.Add("nearby assets");
        if (milestones.depotRequired && !milestones.depotBuilt) pending.Add("depot");
        return pending.Count == 0 ? "none" : string.Join(", ", pending);
    }
}
