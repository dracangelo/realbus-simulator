# Passenger Agent System + AI Traffic System Setup (Unity)

This guide is the cleaned-up, scene-accurate version of the setup for your gameplay scene.

It is written around the hierarchy shown in your screenshots:
- `Bus_Root`
- `Map`
- `Canvas`
- `PassengerManager`
- `PassengerSpawner`

The goal is to make the scene work without skipped inspector wiring, hidden assumptions, or vague "add the script" steps.

---

## 1) What scene should use this?

Use this setup in your driving/gameplay scene:
- `Pysicstesttrack`

Do not place the passenger runtime or AI traffic runtime in menu-only scenes such as:
- `MainMenu`
- `CountrySelect`
- `CitySelect`
- `RouteSelect`

---

## 2) Scripts that should already exist

Passenger system:
- `PassengerManager.cs`
- `PassengerAgent.cs`
- `PassengerSpawner.cs`
- `PassengerCountUI.cs`

Bus + mission integration:
- `BusController.cs`
- `MissionManager.cs`
- `ScheduleManager.cs`
- `GPSManager.cs`
- `ScoreTracker.cs`

Traffic system:
- `AIRoadGraph.cs`
- `AIVehicleController.cs`
- `SplineVehicle.cs`
- `VehiclePool.cs`
- `PedestrianSpawner.cs`
- `AmbulanceBehaviour.cs`
- `AIBusScheduleSpawner.cs`
- `TrafficDebugHUD.cs`
- `TrafficStressTest.cs`

Map/world loading:
- `MapTileLoader.cs`
- `OSMLoader.cs`
- `OSMBuildingLoader.cs`
- `OSMRoadMeshBuilder.cs`
- `OSMBuildingMeshBuilder.cs`
- `OSMRouteImporter.cs`

Recommended:
- `SceneBootstrap.cs`

---

## 3) Recommended gameplay hierarchy

Your current scene is already close. A good runtime hierarchy for this project is:

```text
Pysicstesttrack
├── Main Camera
├── Directional Light
├── Bus_Root
│   ├── WheelCollider_FL
│   ├── WheelCollider_FR
│   ├── WheelCollider_ML
│   ├── WheelCollider_MR
│   ├── WheelCollider_RL
│   ├── WheelCollider_RR
│   ├── WheelMesh_FL
│   ├── WheelMesh_FR
│   ├── WheelMesh_ML
│   ├── WheelMesh_MR
│   ├── WheelMesh_RL
│   └── WheelMesh_RR
├── Plane
├── EventSystem
├── Map
├── Canvas
│   ├── Btn_Accelerate
│   ├── Btn_Brake
│   ├── Btn_SteerLeft
│   ├── Btn_SteerRight
│   ├── Btn_Retarder
│   ├── Btn_Horn
│   ├── Btn_ShiftUp
│   └── Btn_ShiftDown
├── PassengerManager
└── PassengerSpawner
```

Important note:
- In your current scene, `Map` is acting as the main manager host.
- That is fine.
- You do not need to create a separate `Managers` parent unless you want cleaner organization.

---

## 4) Exact setup for `Bus_Root`

Select `Bus_Root`.

It should have:
- `Transform`
- `Rigidbody`
- `BusController`

### `Rigidbody`

Recommended checks:
- `Use Gravity` enabled
- not `Is Kinematic`
- mass appropriate for a bus
- center of mass handled by `BusController`

### `BusController` inspector wiring

Assign all wheel arrays properly. This is one of the easiest places to make a silent mistake.

- `steerWheels`
  - `WheelCollider_FL`
  - `WheelCollider_FR`
- `driveWheels`
  - `WheelCollider_ML`
  - `WheelCollider_MR`
  - `WheelCollider_RL`
  - `WheelCollider_RR`
- `allWheels`
  - all six wheel colliders
- `rearWheels`
  - `WheelCollider_ML`
  - `WheelCollider_MR`
  - `WheelCollider_RL`
  - `WheelCollider_RR`
- `engineData`
  - assign your `EngineSystem` asset/component
- `transmissionData`
  - assign your `TransmissionSystem` asset/component

### Wheel collider child objects

Each wheel collider object should have:
- `Transform`
- `WheelCollider`
- `WheelVisualSync`

For every `WheelVisualSync`, assign:
- `wheelCollider` -> the wheel collider on that same wheel object
- `wheelMesh` -> the matching visible mesh transform

Examples:
- `WheelCollider_FL` -> `wheelMesh = WheelMesh_FL`
- `WheelCollider_FR` -> `wheelMesh = WheelMesh_FR`
- `WheelCollider_ML` -> `wheelMesh = WheelMesh_ML`
- `WheelCollider_MR` -> `wheelMesh = WheelMesh_MR`
- `WheelCollider_RL` -> `wheelMesh = WheelMesh_RL`
- `WheelCollider_RR` -> `wheelMesh = WheelMesh_RR`

### Bus verification

Before touching passengers or traffic, confirm:
- the bus moves with throttle/brake
- steering affects only the front axle
- wheel meshes visually follow the colliders
- no `NullReferenceException` appears from `BusController` or `WheelVisualSync`

### Passenger-specific bus requirement

`PassengerManager` will call:
- `BusController.RequestKneelingSuspension(bool active)`

That already exists in your `BusController`, so no extra code is required. The important part is making sure `PassengerManager` can find the player bus at runtime.

---

## 5) Exact setup for `Map`

In your screenshots, `Map` is carrying the main runtime managers. That is a good approach for now.

Select `Map`.

It should contain these components:
- `MapTileLoader`
- `GPSManager`
- `PassengerManager`
- `MissionManager`
- `FreeDriveSession`
- `ScheduleManager`
- `ScoreTracker`
- `OSMLoader`
- `OSMBuildingLoader`
- `OSMRoadMeshBuilder`
- `OSMBuildingMeshBuilder`
- `OSMRouteImporter`

### Why this matters

Several systems auto-find each other:
- `GPSManager` searches for `MapTileLoader`
- `MissionManager` depends on `GPSManager`, `BusController`, and `BusRoute`
- `PassengerSpawner` depends on `ScheduleManager`
- `PassengerManager` tries to find `BusController`
- traffic systems often depend on the player bus and road graph

If one of these core components is missing, the scene may still enter Play Mode but large parts of gameplay will quietly fail.

### Minimum `Map` verification

Before moving on:
- `GPSManager.Instance` exists
- `ScheduleManager.Instance` exists
- `PassengerManager.Instance` exists
- `MissionManager.Instance` exists
- no startup error says a required reference is missing

Important:
- `MissionManager` logs `Missing references — check Inspector!` if `currentRoute`, `GPSManager`, or `busController` are not assigned/found

### `MissionManager` required inspector fields

On `MissionManager`, assign:
- `currentRoute`
- `busController` -> `Bus_Root`
- `missionData`

Behavior to expect:
- it moves the bus to the first stop using `GPSManager`
- it starts route timing and schedule setup
- it calls `PassengerManager.HandleStopArrival(...)` at stops

### `ScheduleManager` important fields

Useful defaults:
- `gameStartTimeMinutes = 480`
- `secondsPerGameMinute = 1`

This drives:
- rush-hour passenger density
- AI bus departures
- route punctuality tracking

### `ScoreTracker`

`ScoreTracker` auto-finds the bus rigidbody, but you should still verify it is updating in Play Mode.

It tracks:
- punctuality
- passenger comfort
- safety
- efficiency

---

## 6) Exact setup for `PassengerManager`

You currently have both:
- a `PassengerManager` component on `Map`
- a separate `PassengerManager` GameObject in the hierarchy

Pick one runtime owner and keep only one active `PassengerManager` instance.

Reason:
- `PassengerManager` is a singleton
- if two copies exist, one destroys itself in `Awake()`
- that can create confusing setup bugs

Recommended approach:
- keep the `PassengerManager` component on `Map`
- use the separate `PassengerManager` GameObject only if you want to move the component there and remove it from `Map`

### `PassengerManager` inspector fields

Important fields to review:

- `passengerData`
  - assign if you have a `PassengerData` asset
  - otherwise fallback timings are used
- `maxBusCapacity`
  - default example: `80`
- `doorOpenCapacityLimit`
  - usually same as max capacity unless you want stricter boarding rules
- `fuelCostPerKm`
  - economy tuning
- `passengerSpawner`
  - assign your `PassengerSpawner` object here
- `useAdvancedPassengerSimulation`
  - keep enabled
- `wheelchairChance`
  - default around `0.05`
- `extraWheelchairDwellSeconds`
  - default around `10`
- `seatedCapacity`
  - default around `40`

### What `PassengerManager` actually does

At each stop it:
- unloads passengers first
- decides whether doors may open
- spawns waiting passengers through `PassengerSpawner`
- boards up to available capacity
- adds fare income
- recalculates satisfaction
- recalculates required dwell time
- requests kneeling suspension if wheelchair boarding happens

### PassengerManager verification

In Play Mode, after arriving at a stop, confirm:
- `lastAlightingCount` changes
- `lastBoardingCount` changes
- `waitingAtCurrentStop` changes
- `currentPassengers` updates
- `latestRequiredDwellSeconds` increases when many people board
- `hadWheelchairBoarding` occasionally becomes true

If all values stay at zero after a stop arrival, the likely issue is:
- `MissionManager` is not reaching stops
- `currentRoute` is not valid
- `PassengerManager` is not the active singleton

---

## 7) Exact setup for `PassengerSpawner`

Select the `PassengerSpawner` GameObject.

It should have:
- `Transform`
- `PassengerSpawner`

### Recommended values

- `baselineSpawnMin = 2`
- `baselineSpawnMax = 6`
- `rushHourMultiplier = 5`
- `morningPeakMinutes = 450`
- `eveningPeakMinutes = 1050`
- `peakWidthMinutes = 90`
- `terminalMultiplier = 1.6`
- `normalStopMultiplier = 1`

### How it works

`PassengerSpawner` does not place visible crowd prefabs by itself.

It currently acts as a simulation density calculator:
- reads current time from `ScheduleManager.Instance`
- boosts demand near morning/evening peaks
- boosts likely terminal stops based on stop names such as:
  - `terminal`
  - `depot`
  - `station`

### Required wiring

Make sure the active `PassengerManager.passengerSpawner` field points to this object.

### Verification

Check in Play Mode:
- off-peak stops produce fewer passengers
- peak-hour stops produce more passengers
- terminal-style stops tend to spawn more passengers than normal stops

---

## 8) Bus stop and route requirements

The passenger system only works properly if stop data is valid.

### Required route data

Your active `BusRoute` should have:
- a non-empty `stops` array
- valid latitude/longitude for each stop
- stops in correct travel order
- a usable `baseFare`

### Stop arrival integration

Your current integration already exists in `MissionManager`:
- first stop boarding happens in `MissionStartSequence()`
- later boarding/alighting happens in `ProcessStopArrival()`

The core call is:
- `PassengerManager.Instance.HandleStopArrival(stop, currentRoute.baseFare, currentStopIndex, currentRoute.stops.Length)`

### Verification

If the bus reaches a stop and nothing happens:
- check `distanceToNextStop`
- check that `currentStopIndex` is valid
- check that GPS conversion places the stop where expected
- confirm the bus gets within `15f` meters of the target stop

---

## 9) Exact setup for `Canvas` and mobile controls

Select `Canvas`.

It should have:
- `RectTransform`
- `Canvas`
- `CanvasScaler`
- `GraphicRaycaster`
- `MobileControlsUI`

### `MobileControlsUI` required assignments

Assign:
- `busController` -> `Bus_Root`
- `accelerateButton` -> `Btn_Accelerate`
- `brakeButton` -> `Btn_Brake`
- `steerLeftButton` -> `Btn_SteerLeft`
- `steerRightButton` -> `Btn_SteerRight`
- `retarderButton` -> `Btn_Retarder`
- `hornButton` -> `Btn_Horn`
- `shiftUpButton` -> `Btn_ShiftUp`
- `shiftDownButton` -> `Btn_ShiftDown`

### Button requirements

Each button should:
- use a `Button` component
- be interactable
- be inside the active canvas
- not be blocked by another full-screen UI element

`MobileControlsUI` adds `EventTrigger` handlers at runtime for hold behavior, so the buttons do not need manual pointer event setup in the inspector.

### Verification

In Simulator or on device:
- holding accelerate keeps throttle applied
- holding brake keeps brake applied
- holding left/right steers while pressed
- tap `RET` toggles retarder
- tap `HORN` calls horn
- tap shift buttons changes gear

---

## 10) Passenger UI setup

If you want passenger feedback on-screen, create a small HUD panel under `Canvas`.

Recommended hierarchy:

```text
Canvas
└── PassengerPanel
    ├── CountText
    ├── EmojiText
    └── MoodTint
```

Add:
- `PassengerCountUI` to `PassengerPanel`

Assign:
- `countText` -> a `TextMeshProUGUI`
- `emojiText` -> a `TextMeshProUGUI`
- `moodTint` -> a `UnityEngine.UI.Image`

### What it displays

`PassengerCountUI` reads from `PassengerManager.Instance` and shows:
- current passengers / bus capacity
- mood emoji
- satisfaction color tint

### Suggested thresholds

Defaults already in the script:
- `happyThreshold = 0.8`
- `neutralThreshold = 0.55`
- `unhappyThreshold = 0.35`

### Verification

When stops are processed:
- count text updates
- emoji changes with satisfaction
- tint color shifts from green to yellow/orange/red

---

## 11) AI traffic system setup

This project uses a tiered traffic approach:
- `FullAI` near the player bus
- `Spline` in the mid-distance
- `Dormant` far away

The performance-critical rule is:
- keep `VehiclePool.maxFullAiVehicles` in the `15-20` range
- recommended default is `18`

---

## 12) `AIRoadGraph` setup

Create a GameObject named `AIRoadGraph` if you do not already have one.

Add:
- `AIRoadGraph`

Populate `nodes` with waypoint data.

Fastest workflow:
- generate your local `roads.json`
- open `Tools -> RealBus -> Traffic -> Generate AIRoadGraph From Roads JSON`
- select `Assets/StreamingAssets/Cities/<CITYCODE>/roads.json`
- click `Generate / Replace AIRoadGraph`

This creates:
- an `AIRoadGraph` GameObject
- child waypoint transforms under `GeneratedNodes`
- populated `nextNodeIndices`
- default lane/speed data inferred from OSM where possible

Each node should define:
- `id`
- `point`
- `laneCount`
- `laneWidth`
- `speedLimitKmh`
- `trafficLight` if applicable
- `nextNodeIndices`

### Critical detail often missed

`nextNodeIndices` must point to valid node indices.

If this is incomplete:
- AI vehicles spawn
- but cannot route correctly
- or freeze immediately

### Verification

Confirm:
- every used node has a `point` transform
- no required node has an empty `nextNodeIndices`
- intersections using signals have a `TrafficLight` assigned

---

## 13) AI vehicle prefab setup

Each AI vehicle prefab should contain:
- `Rigidbody`
- collider(s)
- `AIVehicleController`
- `SplineVehicle`

Recommended prefab structure:

```text
AI_Car_01
├── MeshRoot
├── BodyCollider
└── optional visual children
```

You do not need wheel colliders for this traffic system.

`AIVehicleController` handles the near-player, full simulation tier.
`SplineVehicle` handles the cheaper far-distance traffic tier.
Both should live on the same prefab so `VehiclePool` can switch behavior cleanly.

Recommended checks:
- forward direction is positive Z
- collider bounds match the mesh reasonably well
- mass is sensible for the vehicle type

### Root object and transform

The prefab root should represent the whole vehicle.
Place the mesh so that:
- the vehicle faces forward on local positive Z
- the pivot is near the center of the vehicle footprint
- the body sits at the correct ride height above the road

If the mesh faces +X instead of +Z, the traffic car will appear to drive sideways because both traffic scripts rotate the object to face its travel direction.

### `Rigidbody` setup

`AIVehicleController` has a `RequireComponent(typeof(Rigidbody))` attribute, so the prefab must have a real `Rigidbody` on the root.

Recommended checks:
- `Use Gravity` enabled
- not `Is Kinematic`
- `Interpolation = Interpolate`
- freeze X and Z rotation if vehicles wobble or tip on uneven map geometry

Suggested starting mass ranges:
- car: `1200-1800`
- truck: `4000-9000`
- motorcycle: `180-350`
- bus: `7000-14000`
- emergency vehicle: similar to a car or van, depending on model size

### Collider setup

Start simple:
- use `BoxCollider` for most cars, vans, trucks, and buses
- use multiple simple colliders only if the body shape needs it
- avoid detailed mesh colliders unless there is a proven need

Collider goals:
- front and rear bounds roughly match the visible body
- width is close to the actual vehicle width
- collider is centered on the lane
- collider does not extend too far below the wheels and scrape the road

If the collider is much larger than the visible mesh, AI spacing will look wrong even if the logic is working correctly.

### `AIVehicleController` details

`AIVehicleController` is the script that follows the road graph, obeys red lights, follows other traffic, and handles emergency yielding.

Fields worth reviewing per prefab:
- `vehicleType`
- `maxAccel`
- `maxBrake`
- `turnRate`
- `stopDistance`
- `safeFollowingDistance`
- `overtakingLaneShift`
- `vehicleLength`
- `obeyTrafficSignals`
- `obeySpeedLimits`

Useful tuning guidance:
- cars should use balanced acceleration and medium following distance
- trucks and buses should use lower acceleration, longer `vehicleLength`, and more following distance
- motorcycles can use shorter `vehicleLength` and quicker acceleration
- emergency vehicles still need the correct `vehicleType` so nearby AI can react properly

Important:
- `vehicleLength` affects when the vehicle considers itself close enough to advance to the next graph node
- values that are too small can make long vehicles cut corners or transition too early
- values that are too large can make vehicles stop or retarget too soon

Set `AIVehicleController.vehicleType` correctly:
- `Car`
- `Truck`
- `Motorcycle`
- `Bus`
- `Emergency`

### `SplineVehicle` details

`SplineVehicle` is used when the prefab is outside the expensive full-AI radius.

Useful fields:
- `accelKmhPerSec`
- `brakeKmhPerSec`
- `turnRate`
- `minHeadway`
- `redLightStopDistance`
- `lookaheadNodes`

In most cases, the script defaults are good enough.
The main setup requirement is simply that the component exists on every pooled traffic prefab.

### Play Mode verification

Before adding the prefab to `VehiclePool`, drag one instance into the scene and confirm:
- it sits correctly on the road surface
- it points in the expected forward direction
- its scale fits the lane width and nearby vehicles
- no missing-component errors appear in the Console

Then test it through the pool and confirm:
- it follows graph directions instead of drifting sideways
- it stops for red lights when applicable
- it slows behind other vehicles instead of clipping through them
- buses and trucks do not pivot unrealistically through corners

### Common mistakes

- mesh forward axis is wrong, so the prefab drives sideways
- `vehicleType` left as `Car` on bus, truck, or ambulance prefabs
- prefab has `AIVehicleController` but is missing `SplineVehicle`
- pivot is placed at the bumper instead of near the center
- collider is far larger or smaller than the visible body

---

## 14) `VehiclePool` setup

Create `VehiclePool` and assign:
- `roadGraph` -> your `AIRoadGraph`
- `prefabs` -> weighted vehicle prefab list

Recommended values:
- `poolSizePerScene = 50`
- `fullAiRadiusMeters = 200`
- `splineRadiusMeters = 400`
- `transitionHysteresisMeters = 25`
- `maxFullAiVehicles = 18`
- `tierUpdateIntervalSeconds = 0.2`
- `minActiveFraction = 0.25`
- `maxActiveFraction = 0.9`
- `updateDensityEverySeconds = 4`
- `morningPeakMinutes = 450`
- `eveningPeakMinutes = 1050`
- `peakWidthMinutes = 80`
- `rushMultiplier = 3.5`

Important:
- the pool auto-finds the player `BusController`
- if there is no player bus in scene, distance-based tiering becomes meaningless

### Debug recommendations

Enable:
- `drawTierGizmos`
- `drawTierRings`

Expected colors:
- green = `FullAI`
- cyan = `Spline`
- gray = `Dormant`

---

## 15) Pedestrians, ambulance, and AI buses

### `PedestrianSpawner`

Add:
- `PedestrianSpawner`

Assign:
- zebra crossing endpoints
- optional controlling traffic light
- pedestrian prefab

Verify:
- pedestrians only cross when vehicle traffic should yield

### `AmbulanceBehaviour`

Emergency prefab should have:
- `AIVehicleController` with `vehicleType = Emergency`
- `AmbulanceBehaviour`

Optional:
- siren audio source
- flashing light references

Verify:
- nearby AI traffic yields when ambulance behavior is active

### `AIBusScheduleSpawner`

Add:
- `AIBusScheduleSpawner`

Assign:
- `aiBusPrefab`
- route entries with:
  - `graph`
  - `nodeSequence`
  - `headwayMinutes`
  - `maxConcurrentBuses`

It depends on:
- valid `ScheduleManager` time
- valid graph nodes
- node sequences with at least 2 entries

---

## 16) First playable validation checklist

Use this checklist in order.

### Core scene

- exactly one active `PassengerManager` singleton
- exactly one active `ScheduleManager` singleton
- `MissionManager.busController` points to `Bus_Root`
- `MissionManager.currentRoute` is assigned
- `GPSManager` finds `MapTileLoader`

### Bus

- bus moves, steers, brakes
- wheel meshes follow colliders
- no missing engine/transmission references

### Mobile UI

- every canvas button is assigned into `MobileControlsUI`
- hold buttons behave continuously
- tap buttons fire once

### Passengers

- first stop can board passengers
- later stops unload then reload passengers
- capacity limits are respected
- dwell time changes with boarding/alighting volume
- wheelchair boarding can trigger kneeling

### Traffic

- `AIRoadGraph` nodes are valid
- `VehiclePool` has prefabs
- full AI count does not exceed budget
- spline tier activates outside the near-player radius

---

## 17) Common mistakes in this specific scene

- Keeping two active `PassengerManager` components in the scene
- Forgetting to assign `PassengerManager.passengerSpawner`
- Leaving `MissionManager.busController` empty
- Leaving `MissionManager.currentRoute` empty
- Missing `engineData` or `transmissionData` on `BusController`
- Forgetting to wire one or more wheel colliders into the bus arrays
- Forgetting to assign one of the `MobileControlsUI` buttons
- Having an `AIRoadGraph` with nodes but no valid `nextNodeIndices`
- Setting `maxFullAiVehicles` above `20`

---

## 18) Good default values to start with

Passenger side:
- `PassengerSpawner.baselineSpawnMin = 2`
- `PassengerSpawner.baselineSpawnMax = 6`
- `PassengerSpawner.rushHourMultiplier = 5`
- `PassengerManager.maxBusCapacity = 80`
- `PassengerManager.seatedCapacity = 40`
- `PassengerManager.wheelchairChance = 0.05`
- `PassengerManager.extraWheelchairDwellSeconds = 10`

Traffic side:
- `VehiclePool.poolSizePerScene = 50`
- `VehiclePool.maxFullAiVehicles = 18`
- `VehiclePool.fullAiRadiusMeters = 200`
- `VehiclePool.splineRadiusMeters = 400`
- `VehiclePool.minActiveFraction = 0.25`
- `VehiclePool.maxActiveFraction = 0.9`

---

## 19) If you want the cleanest final organization

Your current scene is functional, but the cleanest version would be:

- keep `Bus_Root` as the player vehicle only
- keep `Canvas` for UI only
- keep `Map` as world/map/route runtime host
- keep only one `PassengerManager`
- keep only one `PassengerSpawner`
- add a dedicated `TrafficSystem` root later if traffic grows

That keeps the scene easy to debug without forcing a full restructure right now.
