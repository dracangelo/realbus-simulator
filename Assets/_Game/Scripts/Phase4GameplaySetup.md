# Phase 4 — Core gameplay loop

Implements the code for RealBus_MasterTimeline.docx sections 4.1–4.7. Phase 4's device and end-to-end acceptance milestone remains unverified until tested in Unity.

## Run

Open `Assets/_Game/Scenes/GameplayTest.unity` on its own and press Play. It uses the Phase 3 Nairobi road fixture and the Phase 2 bus. No token is needed for the bundled road geometry. The scene selects a connected 500–900 m displacement path (up to 1.5 km driving length) and creates three training stops along it. This is a generated training service on actual OSM roads, not an official Nairobi bus route. The three unvalidated OSM previews from Phase 3 are not used as gameplay routes.

The scene starts in free drive. Release parking brake (P) if needed. W/S accelerate/brake, A/D steer; touch controls remain selectable. Free drive has no passenger service or schedule objectives. Fuel depletes; odometer and GPS are shown. Photo mode pauses simulation and provides keyboard movement, touch movement buttons, and screenshot capture (F12). F10 toggles photo mode. Screenshots go to `Application.persistentDataPath/Screenshots`.

Click **Review training mission** to see the route preview, stops, departure time, duration, and difficulty, then **Depart**. Follow the cyan road line between stops. The bus is placed at the departure stop; countdown holds the service brake, then releases it. Press O or **Open doors** to service the first stop. Drive to the next two markers, align the bus with the road direction and bring the front-door reference to the sign. Door opening requires lateral alignment within 0.5 m, longitudinal alignment within 3 m, heading within 25 degrees, and speed at most 1.5 km/h. Invalid requests are rejected; they do not queue automatic door opening later.

The bus remains braked while it waits for an early schedule and while passengers board/alight. Kneeling changes the WheelCollider suspension targets toward an 80 mm drop; it restores on departure. Actual body displacement needs verification under load. Stop index advances only after dwell finishes, and the mission result appears only after the final dwell. The result displays category scores, stars, XP, coins, actual mission distance/time, and red-light deductions. It is saved through the existing MissionResult PlayerPrefs persistence. **Free drive** cancels any countdown/service coroutine and clears passengers. Repeat the mission to verify clean score and telemetry resets. **Schedule debug** shows expected and actual arrivals, status, and penalties. Door requests play a generated bell tone in this fixture; production buses can assign their own bell recording.

## Integrating production scenes

- Existing MissionManager, MissionBriefingUI, MissionResultUI, FreeDriveUI, ScoreDisplay, and ScheduleDebugOverlay remain the production components. The test scene uses a compact runtime control panel so it can be exercised without wiring TMP scene assets. Assign MissionData to MissionManager and use StartRoute; routes need at least two stops. Both legacy stop arrays and BusStop arrays are supported.
- ScoreTracker.BeginMission/EndMission are owned by MissionManager. Passenger acceleration is sampled in FixedUpdate only while recording. MissionDrivingMonitor forwards collisions when ScoreTracker is on a separate services object and supplies basic speeding penalties if ExtendedTrafficViolationSystem is absent. Assign its RoadGraph for OSM speed limits; fallback is 50 km/h.
- Place each DockingZone at the intended front-door/sign alignment and orient its forward axis in the direction of travel. Assign BusController.dockingReference to the door reference. Legacy prefabs with no reference still use the root position and therefore require root-aligned docking zones. The old distance-only fallback has been removed.
- FuelSystem.TotalConsumedLitres provides cumulative consumption across refills. Efficiency compares mission consumption per distance with the bus's configured consumption target. Existing electric buses use their BatterySystem; both energy systems now set a drivetrain depletion gate. FreeDriveSession supplies a basic fuel fallback only if neither energy component exists.
- OsmTrafficSignalSpawner accepts local OSM signal JSON or requests traffic_signals through the existing Overpass query builder. Assign CoordinateConverter and its runtime RoadGraph, then call LoadSignals. It projects signal nodes onto roads, applies deterministic phase offsets, and creates red/amber/green visuals plus stop-line detectors. OSM does not supply live phases or precise stop-line/approach orientation: inspect generated placement and add other intersection approaches manually. The gameplay fixture's single signal is explicitly a training signal.
- SignalViolationDetector's forward axis defines permitted travel across its local z=0 plane. It sweeps the bus front reference between physics steps, detecting crossings even when a fast bus skips the zone or a slow bus creeps through red. Reverse and lateral passes do not count. Adjust frontBumperOffsetMeters for different bus lengths.

## Rules and persistence

Punctuality retains the ±60-second on-time band. This implementation applies the 2% penalty for each full 30 seconds **beyond** that band; for example, 90 seconds late costs 2%, 180 seconds costs 8%. Early arrivals wait until their scheduled time rather than receiving an additional score deduction. Arrival records are idempotent.

Scores use the existing 30% punctuality / 30% satisfaction / 25% safety / 15% efficiency weights. Red lights log a 500-point deduction on a 10,000-point scale, represented as a 20-point safety-category loss (5 overall percentage points) with any amount exceeding the safety category’s zero floor deducted from the overall score. Every crossing costs five overall percentage points until the overall score reaches zero. Hard braking above 0.4g, lateral acceleration above 0.3g, collisions, and speeding affect the relevant categories. Scores reset at each mission and do not accumulate collision/satisfaction penalties outside it.

MissionData.baseXP=0 derives 200–1000 XP from difficulty 1–5. An explicitly configured baseXP overrides that default. Core XP uses baseXP × score/100 × difficultyMultiplier; configured time/scenario bonuses remain available. The training mission disables those bonuses. Coins use the existing fare/settlement calculation. Result distance and elapsed time now come from the mission itself, not an inactive free-drive session. Existing later-phase XP/save services are used when present; result JSON is still persisted locally without them.

## Validation

Executed in this workspace:

- Parsed all 150 game C# files as C# 9, including editor code: zero syntax errors.
- Compiled and ran the actual Unity-independent GameplayRules source: punctuality boundaries, weighted scoring, XP calculation, all docking tolerance boundaries, and high-/low-speed/directional stop-line crossing checks passed.
- Re-ran the Phase 3 isolated coverage/parser/graph checks: passed.
- Scene/script GUID and whitespace checks: passed.

Added Unity EditMode tests in GameplayRulesTests.cs for the rules above, signal cycle overshoot/zero durations, and score reset/inactive collision behavior. They have not executed here. Unity compilation and physics cannot run because no Unity editor/assemblies are installed; syntax and isolated-rule checks are not a Unity build.

In Unity, run all EditMode tests, then verify: free drive consumes fuel with zero passengers; photo mode restores pause/audio/camera state; countdown prevents acceleration; each docking boundary rejects correctly; an early stop holds until departure time; final dwell precedes results; abort/restart cannot resume old coroutines or award twice; all four score categories change; red-line crossing logs once; results persist after restart. Finally complete the training route and a repaired production route on S20 and profile the required 60 FPS. Phase 2 physics calibration and Phase 3 production-route repairs remain separate outstanding acceptance work.
