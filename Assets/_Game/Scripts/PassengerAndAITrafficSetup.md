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

Follow these steps for each AI vehicle prefab.

### Target hierarchy

Create the prefab with this hierarchy:

```text
AI_Small_Car
├── Visual
└── Collider
```

Use this component layout:

- `AI_Small_Car`
  - `Transform`
  - `Rigidbody`
  - `AIVehicleController`
  - `SplineVehicle`
- `Visual`
  - `Transform`
  - `MeshFilter`
  - `MeshRenderer`
- `Collider`
  - `Transform`
  - `BoxCollider`

Important:
- `AI_Small_Car` is an empty GameObject
- `Collider` is an empty GameObject
- `Visual` must be visible in the Scene view
- if `Visual` is only an empty GameObject, you will not see anything

### Step 1: Create the root object

Create an empty GameObject.
Name it something clear:
- `AI_Small_Car`
- `AI_Truck_01`
- `AI_Bus_01`

Reset the root transform:
- Position = `0, 0, 0`
- Rotation = `0, 0, 0`
- Scale = `1, 1, 1`

### Step 2: Create the child objects

Under the root object, create exactly these two children:
- `Visual`
- `Collider`

Your hierarchy should now look like this:

```text
AI_Small_Car
├── Visual
└── Collider
```

Right after creating them:
- leave `Collider` as an empty GameObject
- do not leave `Visual` empty
- make `Visual` visible in the next step

### Step 3: Set up the `Visual` child

Select `Visual`.

Make `Visual` visible now.

Use this method for the current stage:

1. Delete the empty `Visual` GameObject.
2. Create `GameObject > 3D Object > Cube`.
3. Rename the Cube to `Visual`.
4. Drag `Visual` under `AI_Small_Car`.
5. Reset `Visual` transform:
   - Position = `0, 0, 0`
   - Rotation = `0, 0, 0`
   - Scale = `1, 1, 2`

`Visual` should contain:
- `Transform`
- `MeshFilter`
- `MeshRenderer`

This works because a Cube already has:
- a visible mesh
- a `MeshFilter`
- a `MeshRenderer`

Set it up so the front of the vehicle points in local positive Z.

Check this carefully:
- the front bumper must face the blue arrow
- the car must not face left or right
- the mesh must sit above the ground

For this placeholder Cube:
- use the long side as the front-to-back length of the vehicle
- make the Cube wide enough to look like a car body
- make the Cube tall enough to be easy to see

Example starting scale for `Visual`:
- small car: `1.2, 1.0, 2.4`
- truck: `1.4, 1.5, 4.5`
- bus: `1.5, 2.0, 7.0`

If the mesh points the wrong way, the AI vehicle will drive sideways.

If you are using a custom placeholder mesh instead of a Cube:
- keep `Visual` as a GameObject
- add `MeshFilter`
- add `MeshRenderer`
- assign a mesh in `MeshFilter`
- assign a visible material in `MeshRenderer`

### Step 4: Set up the `Collider` child

Select `Collider`.

Add:
- `BoxCollider`

Resize the `BoxCollider` so it matches the body of the vehicle.

Match these parts:
- front of collider to front bumper
- rear of collider to rear bumper
- width of collider to the body width
- height of collider to the visible body

Do not make the collider much larger than the mesh.

For `BoxCollider > Material`, leave it as:
- `None (Physics Material)`

### Step 5: Add components to the root object

Select `AI_Small_Car`.

Add these components to the root:
- `Rigidbody`
- `AI Vehicle Controller`
- `Spline Vehicle`

The root object should not use wheel colliders.

### Step 6: Configure the `Rigidbody`

On the root `Rigidbody`, set:
- `Mass = 1200` for a small car
- `Linear Damping = 0`
- `Angular Damping = 0.05`
- `Use Gravity = On`
- `Is Kinematic = Off`
- `Interpolate = Interpolate`
- `Collision Detection = Continuous Dynamic`

If the vehicle tips over during testing, enable:
- `Constraints > Freeze Rotation X`
- `Constraints > Freeze Rotation Z`

Starting mass values:
- small car = `1200`
- truck = `5000`
- bus = `9000`
- motorcycle = `250`
- ambulance = `1800`

### Step 7: Configure `AIVehicleController`

On the root `AIVehicleController`, set `vehicleType` correctly:
- car prefab = `Car`
- truck prefab = `Truck`
- motorcycle prefab = `Motorcycle`
- bus prefab = `Bus`
- ambulance prefab = `Emergency`

Start with these values for a small car:
- `maxAccel = 6`
- `maxBrake = 9`
- `turnRate = 4`
- `stopDistance = 4`
- `safeFollowingDistance = 9`
- `overtakingLaneShift = 3`
- `vehicleLength = 4.2`
- `obeyTrafficSignals = On`
- `obeySpeedLimits = On`

Use these changes for larger vehicles:
- truck: increase `safeFollowingDistance` and `vehicleLength`
- bus: increase `safeFollowingDistance` and `vehicleLength`
- motorcycle: reduce `vehicleLength`

Important:
- use a longer `vehicleLength` for buses and trucks
- use a shorter `vehicleLength` for motorcycles and small cars

### Step 8: Configure `SplineVehicle`

On the root `SplineVehicle`, use these starting values:
- `speedKmh = 0`
- `accelKmhPerSec = 16`
- `brakeKmhPerSec = 25`
- `turnRate = 7`
- `minHeadway = 10`
- `redLightStopDistance = 18`
- `lookaheadNodes = 8`

Leave `roadGraph`, `currentNodeIndex`, `targetNodeIndex`, and `laneIndex` for runtime.
`VehiclePool` will assign those values.

### Step 9: Check the final prefab before saving

Before making the prefab, confirm:
- the root is centered on the vehicle
- the mesh faces positive Z
- the collider matches the body
- the root has `Rigidbody`
- the root has `AIVehicleController`
- the root has `SplineVehicle`
- the child `Collider` has `BoxCollider`

### Step 10: Save as a prefab

Drag the root object from the Hierarchy into your prefab folder.

Save it as:
- `AI_Small_Car.prefab`
- `AI_Truck_01.prefab`
- `AI_Bus_01.prefab`

### Step 11: Test one prefab in the scene

Place one AI prefab in the scene and check:
- it stands upright
- it is not floating
- it is not buried in the road
- the front faces forward
- there are no missing script errors in the Console

### Step 12: Final rule for all AI traffic prefabs

Every AI traffic prefab must use the same setup pattern:

```text
Prefab Root
├── Visual
└── Collider
```

Root components:
- `Rigidbody`
- `AIVehicleController`
- `SplineVehicle`

Child components:
- `Visual` -> mesh components
- `Collider` -> `BoxCollider`

---

## 14) `VehiclePool` setup

Follow these steps to add and configure the traffic pool.

### What `VehiclePool` does

`VehiclePool` creates AI traffic vehicles from your prefabs and switches them between these three runtime states:
- `FullAI`
- `Spline`
- `Dormant`

It also:
- uses the player bus distance to decide which vehicles need full simulation
- uses time of day to decide how many pooled vehicles should be active

### Step 1: Create the `VehiclePool` object

Create an empty GameObject in the scene.
Name it:
- `VehiclePool`

Your traffic-related hierarchy should now look like this:

```text
Pysicstesttrack
├── Map
├── AIRoadGraph
├── VehiclePool
└── AI_Small_Car
```

If your AI prefabs are already saved in the Project window, they do not need to stay in the scene.
Only `VehiclePool` must stay in the scene.

### Step 2: Add the script

Select `VehiclePool`.

Add:
- `VehiclePool`

### Step 3: Assign the road graph

In the `VehiclePool` inspector, assign:
- `roadGraph` -> `AIRoadGraph`

This must point to the road graph object that contains your AI traffic nodes.

If `roadGraph` is empty:
- the pool will log `VehiclePool: Missing AIRoadGraph.`
- no AI traffic will be created

### Step 4: Set the pool size

In the `Pool` section, set:
- `poolSizePerScene = 50`

This means the pool will prepare 50 vehicles in total.

### Step 5: Fill the prefab list

In the `Pool` section, open `prefabs`.

Add one element for each traffic prefab you want the pool to spawn.

Example:

- `Element 0`
  - `type = Car`
  - `prefab = AI_Small_Car`
  - `weight = 1`
- `Element 1`
  - `type = Truck`
  - `prefab = AI_Truck_01`
  - `weight = 0.4`
- `Element 2`
  - `type = Bus`
  - `prefab = AI_Bus_01`
  - `weight = 0.2`

Rules for the prefab list:
- use the same `type` here as the prefab's `AIVehicleController.vehicleType`
- every prefab must already contain `Rigidbody`, `AIVehicleController`, and `SplineVehicle`
- do not leave `prefab` empty
- do not leave the `prefabs` list empty

How `weight` works:
- larger weight = that prefab appears more often
- smaller weight = that prefab appears less often

Simple starting setup:
- car weight = `1`
- truck weight = `0.4`
- bus weight = `0.2`
- motorcycle weight = `0.3`

### Step 6: Configure distance-based simulation

In `Tier Split (distance from player bus)`, set:
- `fullAiRadiusMeters = 200`
- `splineRadiusMeters = 400`
- `transitionHysteresisMeters = 25`
- `maxFullAiVehicles = 18`
- `tierUpdateIntervalSeconds = 0.2`

What these values mean:
- vehicles near the player bus use `FullAI`
- vehicles farther away use `Spline`
- vehicles very far away use `Dormant`

Use this mental model:
- `0 to 200` meters = `FullAI`
- `200 to 400` meters = `Spline`
- beyond that = `Dormant`

Keep `maxFullAiVehicles` between `15` and `20`.
The script clamps this range anyway, and `18` is a good default.

### Step 7: Configure traffic density

In `Density`, set:
- `minActiveFraction = 0.25`
- `maxActiveFraction = 0.9`
- `updateDensityEverySeconds = 4`

What these values mean:
- low traffic times activate about 25% of the pool
- busy traffic times activate up to 90% of the pool

With a pool size of `50`:
- 25% means about `12` active vehicles
- 90% means about `45` active vehicles

### Step 8: Configure rush hour

In `Rush Hour`, set:
- `morningPeakMinutes = 450`
- `eveningPeakMinutes = 1050`
- `peakWidthMinutes = 80`
- `rushMultiplier = 3.5`

These values mean:
- `450` minutes = `7:30 AM`
- `1050` minutes = `5:30 PM`

`VehiclePool` reads time from `ScheduleManager.Instance.currentTimeMinutes`.
If `ScheduleManager` is missing, the pool falls back to midday behavior.

### Step 9: Turn on debug drawing

In `Debug Gizmos`, enable:
- `drawTierGizmos`
- `drawTierRings`

Keep these values:
- `vehicleGizmoRadius = 1.2`

Expected colors:
- green = `FullAI`
- cyan = `Spline`
- gray = `Dormant`

### Step 10: Confirm the player bus exists

`VehiclePool` automatically searches for `BusController` in the scene.

Make sure the gameplay scene contains:
- one active player bus
- `BusController` on that bus

If there is no player bus:
- the pool still exists
- tier distance logic becomes meaningless
- vehicles will not switch around the player correctly

### Step 11: Enter Play Mode and verify the pool

Press Play and check these things in order:

1. No warning appears saying `VehiclePool: Missing AIRoadGraph.`
2. AI vehicles appear in the scene.
3. Vehicles near the player bus move with full simulation.
4. Vehicles farther away stay active with cheaper simulation.
5. Some pooled vehicles stay inactive when outside the active range.

### Step 12: If traffic does not appear

Check these fields first:
- `roadGraph` is assigned
- `prefabs` list has entries
- each prefab slot has a real prefab assigned
- each prefab root has `Rigidbody`
- each prefab root has `AIVehicleController`
- each prefab root has `SplineVehicle`
- `AIRoadGraph` contains valid nodes
- the scene contains a player bus with `BusController`

### Step 13: Good starting inspector values

Use these exact values to begin:

- `roadGraph = AIRoadGraph`
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
- `drawTierGizmos = On`
- `drawTierRings = On`

---

## 15) Pedestrians, ambulance, and AI buses

### `PedestrianSpawner`

Add:
- `PedestrianSpawner`

Assign:
- `crossings`
  - for each crossing, assign:
    - `crossingId`
    - `spawnA`
    - `spawnB`
    - optional `controllingTrafficLight`
- `pedestrianPrefab`

Good starting values:
- `maxActivePedestrians = 40`
- `spawnCheckInterval = 1.25`
- `walkSpeed = 1.4`
- `baselineSpawnChance = 0.2`
- `rushMultiplier = 2.0`

Behavior to expect:
- pedestrians spawn from either side of the crossing
- pedestrians only spawn when:
  - a prefab is assigned
  - both crossing endpoints are assigned
  - the crossing is currently free
  - the controlling traffic light is red for road traffic, if one is assigned
- spawn density increases around morning and evening rush periods using `ScheduleManager.currentTimeMinutes`

Verify:
- pedestrians only cross when vehicle traffic should yield
- one crossing does not keep stacking multiple pedestrians at the same time
- if a pedestrian object is destroyed early, that crossing becomes available again

### `AmbulanceBehaviour`

Emergency prefab should have:
- `AIVehicleController` with `vehicleType = Emergency`
- `AmbulanceBehaviour`

Optional:
- siren audio source
- flashing light references

Good starting values:
- `sirenActive = On`
- `influenceRadius = 45`
- `notifyInterval = 0.4`
- `pullOverDuration = 2.2`

Behavior to expect:
- on `Awake`, the script forces the AI vehicle type to `Emergency`
- while siren is active, nearby AI traffic is notified repeatedly and asked to yield
- if emergency lights are assigned, they flash while active
- when the object is disabled, the notify routine stops and audio/lights are turned off cleanly

Verify:
- nearby AI traffic yields when ambulance behavior is active
- the ambulance does not keep notifying traffic after it is disabled
- siren and lights stop correctly when the prefab is disabled or despawned

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
- node sequences where each node links to the next node in the graph

Good starting values:
- `headwayMinutes = 12`
- `maxConcurrentBuses = 3`

Behavior to expect:
- each configured route gets its own active-bus counter and next spawn time
- newly spawned AI buses start at `nodeSequence[0]` and immediately target `nodeSequence[1]`
- invalid node sequences are skipped and log a warning instead of failing silently
- when an AI bus finishes its route, or is destroyed before completion, the active count is released so another bus can spawn later

Verify:
- buses spawn only on valid routes
- buses follow the configured node sequence in order
- active buses do not exceed `maxConcurrentBuses`
- destroying a spawned AI bus does not permanently block future spawns for that route

---

## 16) First playable validation checklist

Use this checklist in order.

### Core scene

- exactly one active `PassengerManager` singleton
- exactly one active `ScheduleManager` singleton
- exactly one active `MissionManager` singleton
- `MissionManager.busController` points to `Bus_Root`
- `MissionManager.currentRoute` is assigned
- `MissionManager.missionData` is assigned
- `GPSManager` finds `MapTileLoader`
- `PassengerManager.passengerSpawner` points to the intended `PassengerSpawner`
- `ScoreTracker` finds the player bus rigidbody in Play Mode

### Bus

- bus moves, steers, brakes
- wheel meshes follow colliders
- no missing engine/transmission references
- `BusController.RequestKneelingSuspension(bool)` can be triggered without errors
- the bus is moved to the first stop when `MissionManager` initializes

### Mobile UI

- `MobileControlsUI.busController` points to `Bus_Root`
- every canvas button is assigned into `MobileControlsUI`
- hold buttons behave continuously
- tap buttons fire once
- pressing accelerate/brake does not leave throttle or brake stuck on after release

### Passengers

- first stop can board passengers
- later stops unload then reload passengers
- capacity limits are respected
- doors stay closed for boarding when bus capacity policy blocks opening
- dwell time changes with boarding/alighting volume
- wheelchair boarding can trigger kneeling
- `waitingAtCurrentStop`, `lastBoardingCount`, and `lastAlightingCount` update sensibly in the Inspector
- `totalFareCollected` and `sessionIncome` increase when boarding occurs

### Traffic

- `AIRoadGraph` nodes are valid
- each used node has valid `nextNodeIndices`
- `VehiclePool` has prefabs
- `VehiclePool.roadGraph` points to `AIRoadGraph`
- full AI count does not exceed budget
- spline tier activates outside the near-player radius
- no warning appears saying `VehiclePool: Missing AIRoadGraph.`
- if using random graph features:
  - `AIBusScheduleSpawner` route entries generate valid connected node sequences
  - `PedestrianSpawner` can generate crossings or fall back to manual crossings without errors

### Mission flow

- route countdown starts and completes
- the first stop is processed once the route begins
- each later stop advances `currentStopIndex`
- `distanceToNextStop` decreases as the bus approaches the next stop
- route completion shows mission results instead of stalling on the final stop

### Logging

- no `NullReferenceException` appears from:
  - `MissionManager`
  - `PassengerManager`
  - `MobileControlsUI`
  - `VehiclePool`
  - `AIBusScheduleSpawner`
  - `PedestrianSpawner`

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
