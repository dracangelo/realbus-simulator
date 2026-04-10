# Passenger Agent System + AI Traffic System Setup (Unity) — step-by-step

This guide explains exactly how to set up both systems in Unity with **no skipped steps**:
- Passenger Agent System
- AI Traffic System (Full AI + Spline tiers)

Follow the steps in order, and **do not proceed** until each verification step passes.

---

## 1) Prerequisites

Before setup, confirm these scripts exist in your project:

- Passenger:
  - `PassengerManager.cs`
  - `PassengerAgent.cs`
  - `PassengerSpawner.cs`
  - `PassengerCountUI.cs`
- Bus/mission integration:
  - `MissionManager.cs` (calls passenger stop handling)
  - `BusController.cs` (kneeling support)
  - `ScheduleManager.cs`
- Traffic:
  - `AIRoadGraph.cs`
  - `AIVehicleController.cs`
  - `SplineVehicle.cs`
  - `VehiclePool.cs`
  - `PedestrianSpawner.cs`
  - `AmbulanceBehaviour.cs`
  - `AIBusScheduleSpawner.cs`
  - `TrafficDebugHUD.cs`
  - `TrafficStressTest.cs`

Also recommended for gameplay stability:
- `SceneBootstrap.cs` (auto-seeds managers when testing scenes directly)

---

## 2) Which scene do I add this to?

Add these runtime systems to your **gameplay scene**:
- **`Pysicstesttrack`** (your driving scene)

Do **not** add passenger/traffic simulation to these UI scenes:
- `CountrySelect`
- `CitySelect`
- `RouteSelect`
- `MainMenu`

If you ever decide to have simulation persist across scenes, we can convert selected managers to `DontDestroyOnLoad` (but keep it simple for now).

---

## 3) Scene Hierarchy (recommended)

Create this structure in your gameplay scene:

```text
Scene
├── Managers
│   ├── GameState
│   ├── CityManager
│   ├── ScheduleManager
│   ├── MissionManager
│   ├── PassengerManager
│   ├── PassengerSpawner
│   └── GPSManager
├── Player
│   └── PlayerBus (BusController + Rigidbody + colliders)
├── TrafficSystem
│   ├── AIRoadGraph
│   ├── VehiclePool
│   ├── PedestrianSpawner
│   ├── AIBusScheduleSpawner
│   ├── EmergencyAmbulance (optional prefab instance)
│   └── TrafficDebugHUD
├── World
│   ├── BusStops
│   ├── ZebraCrossings
│   └── TrafficLights
└── UI
    ├── PassengerPanel
    │   ├── CountText (TMP)
    │   ├── EmojiText (TMP)
    │   └── MoodTint (Image)
    └── (other UI)
```

---

## 4) Passenger Agent System Setup

### Step 1: Create `PassengerManager`

1. Add empty GameObject: `PassengerManager`.
2. Add component: `PassengerManager`.
3. Assign any required references used by your script (stop points, bus transforms, capacities, etc.).

**Verify**
- In Play Mode, `PassengerManager.Instance` exists (no console errors).

### Step 2: Configure `PassengerSpawner`

1. Add empty GameObject: `PassengerSpawner`.
2. Add component: `PassengerSpawner`.
3. Configure:
   - `baselineSpawnMin` / `baselineSpawnMax`
   - `rushHourMultiplier`
   - `morningPeakMinutes` (default around `450` = 7:30 AM)
   - `eveningPeakMinutes` (default around `1050` = 5:30 PM)
   - `peakWidthMinutes`
4. Ensure `ScheduleManager` exists so density changes by time-of-day.

**Verify**
- In Play Mode, `ScheduleManager.currentTimeMinutes` increases.
- Passenger spawns differ between peak and off-peak times.

### Step 3: Bus stop data sanity check

1. Ensure route stops are valid and in order (`BusRoute.stops`).
2. Ensure each stop has world position mapping (via GPS/route systems already in your project).
3. Make sure stop arrival calls happen in `MissionManager`:
   - `PassengerManager.Instance.HandleStopArrival(...)`

**Verify**
- When you reach a stop, you see boarding/alighting and the dwell time changes.

### Step 4: Player bus integration

1. Select `PlayerBus` object.
2. Ensure `BusController` exists.
3. Confirm kneeling support is available:
   - `RequestKneelingSuspension(bool active)` should be callable by passenger flow.

**Verify**
- No `NullReferenceException` in passenger boarding at stops.

### Step 5: Passenger UI

1. Create UI object (e.g. `PassengerPanel`).
2. Add component: `PassengerCountUI`.
3. Assign fields:
   - `countText` -> TMP text for passenger count
   - `emojiText` -> TMP text for mood emoji
   - `moodTint` -> Image for color mood feedback
4. Enter Play Mode and confirm values update while driving.

**Verify**
- Count changes when passengers board/alight.
- Mood emoji/tint changes as satisfaction changes.

### Step 6: Validation checklist

- Passenger count increases at busy stops.
- Boarding is limited by bus capacity.
- Alighting happens at destination stops.
- Dwell time changes with boarding/alighting volume.
- Satisfaction updates over time and is visible in UI.

---

## 5) AI Traffic System Setup (Tiered)

### Important performance rule (non-negotiable)

- `VehiclePool.maxFullAiVehicles` is the hard budget for the A54 target.
- Keep this **15–20** (default **18**).
- Full AI runs physics + higher-cost logic near the player; spline tier is used beyond that.

### Step 1: Build the road graph (`AIRoadGraph`)

1. Add empty GameObject: `AIRoadGraph`.
2. Add component: `AIRoadGraph`.
3. Create waypoint transforms under `World` (or a dedicated `RoadNodes` object).
4. In `AIRoadGraph.nodes`, add one element per waypoint and assign:
   - `id`
   - `point` (Transform)
   - `laneCount` and `laneWidth`
   - `speedLimitKmh`
   - `trafficLight` (optional but recommended at intersections)
   - `nextNodeIndices` (graph connectivity)

**Verify**
- Select `AIRoadGraph` and confirm there are no missing node `Transform` refs.
- In Play Mode, vehicles move (if they don’t, the graph connectivity is incomplete).

### Step 2: Prepare AI vehicle prefabs

For each prefab (car/truck/motorcycle/bus):

1. Add:
   - `Rigidbody`
   - collider(s)
   - `AIVehicleController`
   - `SplineVehicle`
2. Keep both scripts on prefab; `VehiclePool` enables one tier at runtime.
3. Set `AIVehicleController.vehicleType` correctly.

**Prefab sanity checklist**
- `Rigidbody` mass is reasonable (cars ~1200–1800, trucks higher).
- Vehicle forward axis points in the driving direction (Z+ forward).
- Colliders roughly match vehicle bounds.

### Step 3: Configure `VehiclePool`

1. Add empty GameObject: `VehiclePool`.
2. Add component: `VehiclePool`.
3. Assign:
   - `roadGraph` -> `AIRoadGraph`
   - `prefabs` -> weighted list of car/truck/motorcycle prefabs
4. Recommended values:
   - `poolSizePerScene = 50` (or higher for stress scene)
   - `fullAiRadiusMeters = 200`
   - `splineRadiusMeters = 400`
   - `maxFullAiVehicles = 18` (hard cap in 15-20 window)
   - `transitionHysteresisMeters = 25`
5. Confirm `PlayerBus` has `BusController`; pool uses this as distance center.

**Verify**
- Press Play, open Scene view.
- You see tier rings and gizmos if enabled (green full AI near bus, cyan spline mid-range).
- Full AI count never exceeds `maxFullAiVehicles` (check `TrafficDebugHUD`).

### Step 4: Pedestrians at zebra crossings

1. Add empty GameObject: `PedestrianSpawner`.
2. Add component: `PedestrianSpawner`.
3. Create zebra crossing pairs:
   - `spawnA` transform
   - `spawnB` transform
   - optional `controllingTrafficLight`
4. Assign `pedestrianPrefab`.
5. Configure `maxActivePedestrians`, `spawnCheckInterval`, and spawn chance.

**Verify**
- Pedestrians only cross when the controlling light is red for vehicles.

### Step 5: Emergency vehicle behavior

1. Use ambulance prefab (or create one).
2. Add:
   - `AIVehicleController` (`vehicleType = Emergency`)
   - `AmbulanceBehaviour`
3. Optional:
   - Assign `sirenSource`
   - Assign flashing `Light[] emergencyLights`
4. Tune:
   - `influenceRadius`
   - `notifyInterval`
   - `pullOverDuration`

**Verify**
- When ambulance siren is active, nearby traffic slows and shifts over.

### Step 6: AI buses with schedule

1. Add empty GameObject: `AIBusScheduleSpawner`.
2. Add component: `AIBusScheduleSpawner`.
3. Assign:
   - `aiBusPrefab` (has Rigidbody + AIVehicleController + SplineVehicle)
   - route entries (`graph`, `nodeSequence`, `headwayMinutes`, `maxConcurrentBuses`)
4. Ensure `ScheduleManager` is active for time-based departures.

**Verify**
- Buses spawn on headway and follow node sequences.

### Step 7: Debug + visualization

1. Add `TrafficDebugHUD` to an object in scene (or `TrafficSystem` root).
2. Assign `vehiclePool`.
3. Use `F8` to toggle HUD.
4. In `VehiclePool`, enable:
   - `drawTierGizmos`
   - `drawTierRings`
5. Validate:
   - Green markers: full AI near bus
   - Cyan markers: spline vehicles mid-range
   - Gray markers: dormant far vehicles

---

## 6) Stress Test Scene (A54)

### Step 1: Build scene automatically

Use menu:

- `Tools/RealBus/Build Traffic Stress Test Scene (A54)`

This creates:
- `Assets/_Game/Scenes/TrafficStressTest_A54.unity`

### Step 2: Wire scene contents

1. Open the created scene.
2. Populate `AIRoadGraph.nodes` for A54 path.
3. Assign `VehiclePool.prefabs`.
4. Confirm `TrafficStressTest.vehiclePool` is assigned.

### Step 3: Run benchmark

1. Enter Play Mode.
2. `TrafficStressTest` increments active vehicle count.
3. It logs average FPS per step and detects the cliff point.
4. Use that cliff as your global traffic budget ceiling.

---

## 7) Common mistakes to avoid

- Missing `BusController` on player bus (tier distance center not found).
- Empty `AIRoadGraph.nodes` (vehicles cannot route).
- Prefabs missing `Rigidbody` or missing AI scripts.
- No `ScheduleManager` in scene (density falls back to default midday behavior).
- Setting `maxFullAiVehicles` above 20 (breaks performance target).

---

## 8) Recommended first playable defaults

- `VehiclePool.poolSizePerScene = 50`
- `VehiclePool.maxFullAiVehicles = 18`
- `VehiclePool.fullAiRadiusMeters = 200`
- `VehiclePool.splineRadiusMeters = 400`
- `VehiclePool.minActiveFraction = 0.25`
- `VehiclePool.maxActiveFraction = 0.9`
- `PedestrianSpawner.maxActivePedestrians = 40`

These values are a stable starting point for A54-class performance.

---

## 9) Map/Streaming/Road Physics (next systems you requested)

This project currently uses:
- `MapTileLoader` for raster tiles + GPS/world conversion
- OSM meshes for roads (`OSMRoadMeshBuilder`) with `MeshCollider`

The improvements below add:
- A single **CoordinateConverter** wrapper
- A persistent **MapOrigin** asset
- GPS tracking for the bus
- Tile streaming (2km load / 3km unload) with priority and UI spinner
- Offline cache + city predownload flow
- Road surface friction (asphalt/cobble/dirt + wetness)

These steps are intentionally strict so nothing is missed.

---

## 10) Coordinate Conversion (CoordinateConverter + MapOrigin)

### Goal

All systems use one API:
- `CoordinateConverter.GeoToWorldPosition(lat, lon)`
- `CoordinateConverter.WorldToGeoPosition(worldPos)`

### Step 1: Create the MapOrigin asset (persistent)

1. In Project window, create:
   - **Create → RealBus → Map → Map Origin**
2. Name it: `MapOrigin_ActiveCity` (or similar).
3. Set:
   - `originLat` = your city/session origin
   - `originLon` = your city/session origin
   - `originLabel` = city name

**Verify**
- The values match your chosen city (`CityDefinition.centreLat/centreLon`).

### Step 2: Add CoordinateConverter to gameplay scene

1. In `Pysicstesttrack`, create GameObject: `CoordinateConverter`
2. Add component: `CoordinateConverter`
3. Assign:
   - `mapOrigin` → your `MapOrigin_ActiveCity` asset
   - `mapTileLoader` → optional (auto-finds)

**Verify**
- In Play Mode, no console errors.
- Call sites can use `CoordinateConverter.Instance`.

### Step 3: Unit test verification (round trip)

1. Run EditMode tests.
2. Ensure `CoordinateConverterTests` passes.

**Pass criteria**
- Round-trip accuracy is within **0.1m** for local points.

---

## 11) GPS Tracker (feeds minimap + streaming)

### Step 1: Add GpsTracker

1. Create GameObject: `GpsTracker`
2. Add component: `GpsTracker`
3. Assign:
   - `busTransform` → your player bus transform
   - `converter` → `CoordinateConverter` (optional; auto-finds)

**Verify**
- In Play Mode, `GpsTracker.currentLat/currentLon` changes as bus moves.

---

## 12) Road Colliders + Physics Material + Surface Grip

### Goal

- MeshColliders exist for drivable roads
- PhysicsMaterial applied: **static friction 0.7**, **dynamic friction 0.6**
- WheelCollider stiffness is adjusted by road type and wetness:
  - Asphalt = **1.0×**
  - Cobblestone = **0.85×**
  - Dirt = **0.65×**
  - Wet (rain) = **0.7×** multiplier (applied on top)

### Step 1: Create the road PhysicMaterial asset

1. Project window → Create → Physic Material
2. Name: `PM_Road_Asphalt`
3. Set:
   - `Static Friction = 0.7`
   - `Dynamic Friction = 0.6`
   - `Friction Combine = Average` (recommended)
   - `Bounce Combine = Minimum`

### Step 2: Apply to OSM road meshes

1. Select the GameObject with `OSMRoadMeshBuilder`
2. Assign:
   - `roadPhysicMaterial` → `PM_Road_Asphalt`
   - `roadLayer` → a layer you dedicate to roads (e.g. `Road`)
   - `roadTag` → `Road` (or keep default)

**Verify**
- In Play Mode, generated road segments have:
  - `MeshCollider` enabled
  - correct PhysicMaterial
  - correct layer/tag

### Step 3: Set up road type layers

Create layers (Project Settings → Tags and Layers):
- `Road` (asphalt)
- `Cobblestone`
- `Dirt`

Assign these layers to the corresponding road mesh objects in your world (or generate them per type if you split roads).

### Step 4: Add RoadSurfaceDetector + RoadSurfaceFrictionController

1. On a `RoadPhysics` GameObject (or on the bus), add:
   - `RoadSurfaceDetector`
   - `RoadSurfaceFrictionController`
2. Assign:
   - `busController` references (or let them auto-find)
3. In `RoadSurfaceDetector`, set:
   - `surfaceLayers` to include your road layers
   - verify layer mappings:
     - `Road` → Asphalt
     - `Cobblestone` → Cobblestone
     - `Dirt` → Dirt

**Verify**
- In Play Mode, changing the road layer under a wheel changes grip feel.
- During rain, grip is reduced further (wet multiplier).

---

## 13) Tile Streaming (2km load / 3km unload) + Loading Spinner

### Step 1: Add OfflineCacheManager

1. Create GameObject: `OfflineCacheManager`
2. Add component: `OfflineCacheManager`

**Verify**
- On start, it logs online/offline status.

### Step 2: Add TileStreamManager

1. Create GameObject: `TileStreamManager`
2. Add component: `TileStreamManager`
3. Assign:
   - `mapboxToken` (required)
   - `mapStyle`
   - `zoomLevel`
   - `busTransform` → player bus (optional; auto-finds)
   - `converter` → CoordinateConverter (optional; auto-finds)
   - `cache` → OfflineCacheManager (optional; auto-finds)
4. Set radii:
   - `loadRadiusMeters = 2000`
   - `unloadRadiusMeters = 3000`
5. Set performance:
   - `maxConcurrentDownloads = 6`
   - `refreshEverySeconds = 0.5`

### Step 3: Add loading indicator UI

1. In your gameplay UI canvas, create:
   - `TileStreamingLoadingUI` panel
   - `CanvasGroup` on the panel root
   - an `Image` as spinner (child)
2. Add component: `TileStreamingLoadingUI`
3. Assign:
   - `canvasGroup`
   - `spinner` RectTransform
4. Assign `TileStreamManager.loadingUI` to the `TileStreamingLoadingUI` instance.

**Verify**
- Spinner is visible while tiles are downloading.
- Spinner hides when streaming settles.

---

## 14) Offline Maps: cache + predownload UI flow

### Step 1: Add CityPredownloader

1. Create GameObject: `CityPredownloader`
2. Add component: `CityPredownloader`
3. Assign:
   - `mapboxToken`
   - `mapStyle`
   - `zoomLevel`
   - `radiusMeters` (start with 2000)

### Step 2: Create Settings UI (Offline Maps)

Add a panel in your Settings menu:

- Dropdown (TMP): City selection
- Button: Download
- Slider: Progress bar
- Text (TMP): Progress percent
- Text (TMP): Estimated size

Add component: `OfflineMapsUI` and assign:
- `cityDropdown`
- `downloadButton`
- `progressBar`
- `progressLabel`
- `sizeLabel`
- `predownloader`

**Verify**
- Clicking Download starts progress updates.
- Cached tiles are written to `Application.persistentDataPath/TileCache/...`
- If device is offline, tile requests come from cache (missing tiles fail gracefully).

---

## 15) Scene navigation (no hardcoded scene names)

This project now uses a **scene build-index catalog** (no scene-name strings in code).

### Step 1: Create the catalog asset

1. Project window → **Create → RealBus → Scenes → Scene Catalog**
2. Name it: `SceneCatalog_Default`
3. Set build indices to match your Build Settings order:
   - `mainMenuBuildIndex`
   - `countrySelectBuildIndex`
   - `citySelectBuildIndex`
   - `routeSelectBuildIndex`
   - `gameplayBuildIndex` (your `Pysicstesttrack`)

### Step 2: Assign it to SceneLoader (once)

1. In your persistent bootstrap scene (or in `MainMenu`), select `SceneLoader`
2. Assign `sceneCatalog` → `SceneCatalog_Default`

**Verify**
- Buttons that call `SceneLoader.Load*()` navigate correctly.

---

## 16) Automatic route generation (Overpass → RoadGraph → BusRoute assets)

### What this gives you

- Overpass download for **roads** + **bus stops**
- Stop snap to road centerlines
- Corridor clustering into draft routes
- A* pathfinding on the road graph between ordered stops
- Filtering into **10–20 usable routes**
- Return routes (reverse stops + re-pathfind)
- Saved `BusRoute` ScriptableObjects in `Assets/_Game/Routes/`

### Step 1: Open the importer tool

Menu:
- `Tools/RealBus/OSM/Auto-Generate Bus Routes (Overpass)`

### Step 2: Set bounding box and run

1. Fill Min/Max lat/lon for your target city area.
2. Click in order:
   - **1) Download roads + stops**
   - **2) Cluster stops → draft routes**
   - **3) Generate paths (A*) + filter**
   - **4) Save BusRoute assets (+ return routes)**

**Verify**
- New assets appear in `Assets/_Game/Routes/`
- Each saved route has:
  - `busStops[]`
  - `pathPoints[]`
  - `distanceKm`, `estimatedTimeMinutes`, `difficulty`

### Step 3: Visualize a route in Scene view (optional)

Option A (gizmo component):
- Add `RouteDebugVisualiser` to a GameObject in scene and assign a `BusRoute`.

Option B (editor window):
- Open `Tools/RealBus/Routes/Route Editor Window`

---

## 17) Route Editor Window (edit stops in-scene)

Open:
- `Tools/RealBus/Routes/Route Editor Window`

### Features

- **Move stop**: drag stop marker in Scene view (updates GPS)
- **Delete stop**
- **Reorder stops**: Up/Down
- **Reverse stops**: quick return-variant helper
- **Add stop (scene click)**:
  - Enter add-stop mode → click in Scene view to place stop
  - Inserts after selected stop (or at end)
- **Split route**:
  - Choose split point → creates 2 new `BusRoute` assets
- **Merge routes**:
  - Assign “Merge with” route → creates merged `BusRoute` asset
  - Optional junction de-duplication

**Verify**
- After edits, the `BusRoute` asset is marked dirty and saves correctly.
