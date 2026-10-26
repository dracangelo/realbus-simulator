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
        /// <summary>The local save profile has been applied (upgrades, liveries, progress).</summary>
        public bool saveLoaded;
        /// <summary>The audio manager has attached its loops to the player's bus.</summary>
        public bool audioReady;
        public bool busSpawned;
        /// <summary>The player's bus has its engine/transmission data, i.e. it is the drivable prefab.</summary>
        public bool vehicleConfigured;
        public bool mapInitialised;
        public bool gpsReady;
        public bool roadsBuilt;
        public bool buildingsRingReady;
        public bool tilesLoaded;
        /// <summary>The map loader exhausted its retries; the ground has no imagery.</summary>
        public bool mapFailed;
        /// <summary>Near/FOV assets are still being warmed (tiles, buildings, traffic, props).</summary>
        public bool preloadRequired;
        public bool preloadReady;
        public float preloadProgress;
        public string preloadStatus;
        /// <summary>The cockpit HUD is built and bound to the bus.</summary>
        public bool hudReady;
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
    /// and names the work still in flight. The weights are cumulative thresholds, so
    /// the value never regresses while a milestone stays true: restore save (5%),
    /// audio (9%), bus (17%), vehicle configuration (24%), map (30%), GPS (35%),
    /// roads (48%), buildings (62%), tiles (74%), near-field warm-up (80–95%),
    /// cockpit HUD (97%), depot (98.5%), ready (100%).
    ///
    /// Absent systems count as satisfied (see <see cref="Capture"/>), so a test scene
    /// or a stripped build without, say, a depot is never held open by this ladder.
    /// </summary>
    public static Snapshot Evaluate(Milestones milestones)
    {
        if (!milestones.saveLoaded)
            return new Snapshot { progress = 0.05f, status = "RESTORING SAVE DATA...", ready = false };

        if (!milestones.audioReady)
            return new Snapshot { progress = 0.09f, status = "INITIALISING AUDIO...", ready = false };

        if (!milestones.busSpawned)
            return new Snapshot { progress = 0.17f, status = "SPAWNING BUS...", ready = false };

        if (!milestones.vehicleConfigured)
            return new Snapshot { progress = 0.24f, status = "CONFIGURING VEHICLE...", ready = false };

        if (!milestones.mapInitialised)
            return new Snapshot { progress = 0.30f, status = "INITIALISING MAP...", ready = false };

        if (!milestones.gpsReady)
            return new Snapshot { progress = 0.35f, status = "CALIBRATING GPS...", ready = false };

        if (!milestones.roadsBuilt)
            return new Snapshot { progress = 0.48f, status = "BUILDING ROAD NETWORK...", ready = false };

        if (!milestones.buildingsRingReady)
            return new Snapshot { progress = 0.62f, status = "LOADING BUILDINGS...", ready = false };

        if (!milestones.tilesLoaded)
        {
            // A failed map is not a reason to keep saying "downloading": the bar
            // stops at the honest value and the loading screen puts the real error
            // and its retry action on the card.
            return new Snapshot
            {
                progress = 0.74f,
                status = milestones.mapFailed ? "MAP UNAVAILABLE" : "DOWNLOADING MAP TILES...",
                ready = false
            };
        }

        if (milestones.preloadRequired && !milestones.preloadReady)
        {
            string status = string.IsNullOrWhiteSpace(milestones.preloadStatus)
                ? "PREPARING NEARBY ASSETS..."
                : milestones.preloadStatus;
            // Keep the bar moving through the near-field warm-up instead of
            // parking it for the whole streaming pass.
            float progress = Mathf.Lerp(0.80f, 0.95f, Mathf.Clamp01(milestones.preloadProgress));
            return new Snapshot { progress = progress, status = status, ready = false };
        }

        if (!milestones.hudReady)
            return new Snapshot { progress = 0.97f, status = "PREPARING COCKPIT...", ready = false };

        if (milestones.depotRequired && !milestones.depotBuilt)
            return new Snapshot { progress = 0.985f, status = "PLACING BUS DEPOT...", ready = false };

        return new Snapshot { progress = 1f, status = "WORLD READY", ready = true };
    }

    /// <summary>
    /// Reads the current scene state into <see cref="Milestones"/>. A manager that is
    /// genuinely absent (a test scene, a stripped build) counts as satisfied: this
    /// function observes readiness, it must not create side effects or deadlock on a
    /// system the scene never had.
    /// </summary>
    public static Milestones Capture()
    {
        var milestones = new Milestones();

        var save = SaveManager.Instance;
        milestones.saveLoaded = save == null || save.IsInitialized;

        var audio = RealBusAudioManager.Instance;
        milestones.audioReady = audio == null || audio.IsBound;

        var mission = MissionManager.Instance;
        var bus = mission != null ? mission.busController : null;
        if (bus == null) bus = Object.FindAnyObjectByType<BusController>();
        milestones.busSpawned = bus != null;
        milestones.vehicleConfigured = bus != null && bus.engineData != null && bus.transmissionData != null;

        milestones.mapInitialised = MapTileLoader.Instance != null;
        milestones.gpsReady = GPSManager.Instance != null;

        var roads = OSMRoadMeshBuilder.Instance;
        milestones.roadsBuilt = roads == null || roads.roadsBuilt;

        var buildings = OSMBuildingMeshBuilder.Instance;
        // Treat an absent builder as satisfied: a scene without the building
        // system should never be held back by this gate.
        milestones.buildingsRingReady = buildings == null || buildings.InitialRingReady;

        var map = MapTileLoader.Instance;
        milestones.tilesLoaded = map == null || map.CenterTileLoaded;
        milestones.mapFailed = map != null && map.MapFailed;

        var preload = WorldPreloadCoordinator.Instance;
        milestones.preloadRequired = preload != null && !preload.IsReady;
        milestones.preloadReady = !milestones.preloadRequired;
        milestones.preloadProgress = preload != null ? preload.Progress : 0f;
        milestones.preloadStatus = preload != null ? preload.Status : null;

        var hud = HUDManager.Instance;
        milestones.hudReady = hud == null || hud.IsBound;

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

        // A scene that starts on the street, a checkpoint resume or a failed
        // placement has no yard to wait for. Without this the overlay would sit on
        // "PLACING BUS DEPOT..." for its whole ceiling.
        if (!mission.repositionBusOnStart || !mission.startAtDepot) return false;
        if (mission.DepotPlacementFailed) return false;

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
        if (!milestones.saveLoaded) pending.Add("save");
        if (!milestones.audioReady) pending.Add("audio");
        if (!milestones.busSpawned) pending.Add("bus");
        if (!milestones.vehicleConfigured) pending.Add("vehicle");
        if (!milestones.mapInitialised) pending.Add("map");
        if (!milestones.gpsReady) pending.Add("gps");
        if (!milestones.roadsBuilt) pending.Add("roads");
        if (!milestones.buildingsRingReady) pending.Add("buildings");
        if (!milestones.tilesLoaded) pending.Add(milestones.mapFailed ? "tiles (map download failed)" : "tiles");
        if (milestones.preloadRequired && !milestones.preloadReady) pending.Add("nearby assets");
        if (!milestones.hudReady) pending.Add("hud");
        if (milestones.depotRequired && !milestones.depotBuilt) pending.Add("depot");
        return pending.Count == 0 ? "none" : string.Join(", ", pending);
    }
}
