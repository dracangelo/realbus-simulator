# Testing

## EditMode Tests With Coverage

Run the bundled script from the project root:

```bash
bash tools/run_editmode_tests.sh
```

If Unity is installed somewhere else, override the executable path:

```bash
UNITY_BIN="/path/to/Unity" bash tools/run_editmode_tests.sh
```

By default the script runs only the project's EditMode suite in `GameScriptsTests.Editor`. Override that if you want a different assembly list:

```bash
TEST_ASSEMBLIES="GameScriptsTests.Editor" bash tools/run_editmode_tests.sh
```

## What You Get

The script writes all artifacts into `TestResults/`:

- `TestResults/editmode-results.xml`
  Contains the pass/fail/skipped totals and individual test results.
- `TestResults/editmode-unity.log`
  Contains the Unity batchmode log so you can inspect failures in detail.
- `TestResults/Coverage/`
  Contains code coverage artifacts, including the HTML report when Unity generates it.

At the end of the run, the script also prints a quick XML summary for:

- `total`
- `passed`
- `failed`
- `inconclusive`
- `skipped`

## Useful Follow-Ups

Open the XML summary quickly:

```bash
rg "<test-run" TestResults/editmode-results.xml
```

Find failed tests:

```bash
rg 'result="Failed"' TestResults/editmode-results.xml
```

Open the Unity log:

```bash
less TestResults/editmode-unity.log
```

If the coverage HTML report is generated, open the main report file from `TestResults/Coverage/`.

## Headless scene check

The EditMode suite never runs `Awake`/`Start`, so the assembled world is checked by booting the real scene and driving it headlessly:

```bash
XML=/home/drac/Unity/Hub/Editor/6000.6.0f1/Editor/Data/PlaybackEngines/AndroidPlayer/NDK/toolchains/llvm/prebuilt/linux-x86_64/lib/libxml2.so.2
LD_PRELOAD="$XML" xvfb-run -a /home/drac/Unity/Hub/Editor/6000.6.0f1/Editor/Unity \
  -batchmode -projectPath "$PWD" \
  -executeMethod GameplaySmokeCheck.Run -logFile /tmp/smoke.log
```

`RunExtended` drives for 20 seconds instead of 6, `RunHandling` isolates the drivetrain on a closed course, and `RunLiveCity` checks the streamed-city props. The run exits itself: status `0` passed, `2` failed, and every measurement is written to the log as `SMOKE ...` lines.

The environment can be pinned for a run, which is how day, night and storm are checked without waiting for the clock or the weather:

```bash
REALBUS_SMOKE_TIME=780 REALBUS_SMOKE_WEATHER=Thunderstorm LD_PRELOAD="$XML" xvfb-run -a "$UNITY" \
  -batchmode -projectPath "$PWD" -executeMethod GameplaySmokeCheck.RunExtended -logFile /tmp/smoke-storm.log
grep "SMOKE environment" /tmp/smoke-storm.log
```

`REALBUS_SMOKE_TIME` is minutes past midnight (0 = midnight, 780 = 13:00) and `REALBUS_SMOKE_WEATHER` is any `WeatherState` name (`Clear`, `PartlyCloudy`, `Windy`, `Overcast`, `Fog`, `Drizzle`, `LightRain`, `HeavyRain`, `Thunderstorm`, `Snow`, `Blizzard`). The `SMOKE environment` line reports the skybox, cloud coverage, star visibility, moonlight, sun elevation, fog distances and colour, the precipitation emission rates, the sun intensity/shadow strength, the ambient/reflection intensities, and the cinematic grade state (`grade`, `gradeExposure`, `gradeContrast`, `gradeSaturation`, `gradeTint`). The check fails the run when the environment is missing, when rain and weather disagree, when a heavy overcast still has a near-full shadow/reflection (or a clear sky has none), or when the grade failed to attach on a quality tier above Very Low.

Pinned examples used for the cinematic pass:

```bash
# Clear noon
REALBUS_SMOKE_SKIP_CAPTURE=1 REALBUS_SMOKE_TIME=720 REALBUS_SMOKE_WEATHER=Clear \
  LD_PRELOAD="$XML" xvfb-run -a "$UNITY" -batchmode -projectPath "$PWD" \
  -executeMethod GameplaySmokeCheck.Run -logFile /tmp/smoke-noon.log
grep "SMOKE environment" /tmp/smoke-noon.log

# Clear night
REALBUS_SMOKE_SKIP_CAPTURE=1 REALBUS_SMOKE_TIME=60 REALBUS_SMOKE_WEATHER=Clear \
  LD_PRELOAD="$XML" xvfb-run -a "$UNITY" -batchmode -projectPath "$PWD" \
  -executeMethod GameplaySmokeCheck.Run -logFile /tmp/smoke-night.log
grep "SMOKE environment" /tmp/smoke-night.log

# Broken cloud and wind at 08:00
REALBUS_SMOKE_SKIP_CAPTURE=1 REALBUS_SMOKE_TIME=480 REALBUS_SMOKE_WEATHER=Windy \
  LD_PRELOAD="$XML" xvfb-run -a "$UNITY" -batchmode -projectPath "$PWD" \
  -executeMethod GameplaySmokeCheck.Run -logFile /tmp/smoke-windy.log
grep "SMOKE environment" /tmp/smoke-windy.log
```

The assembled scene runs a compressed world clock: `TimeOfDaySystem` starts at the timetable's opening hour and covers a full 24 game hours in 30 real minutes, while `ScheduleManager` keeps its own one-game-minute-per-real-minute timetable for stop punctuality. A pinned `REALBUS_SMOKE_TIME` disables that advance so the sky holds still for the duration of a smoke run. Traffic density reads the same clock (`VehiclePool.GetTrafficDensityVolume`), so a pinned run also pins how many cars the pool keeps awake. The weather cycle starts after the real WeatherService lookup, or after a short 45-second hold when the offline simulated weather is used, then holds each state for 90–240 real seconds and cross-fades over 45–90 seconds.

### Full route and stop progression

The route bug this covers is not a crash: a stop service that never finished left the
served stop as the navigation target, so however far the player drove the mission still
thought they were parked at a stop they had already left. A fixed-duration smoke cannot
catch that, because the failure is "nothing happens for a long time". The route runner
drives the whole route in the real scene and asserts progression stop by stop:

```bash
bash tools/run_route_smoke.sh
```

It launches `GameplaySmokeCheck.RunRoute` in batchmode (same licensing, `LD_PRELOAD`
and `xvfb-run` handling as the other scripts) and writes `TestResults/route-smoke.log`.
For every stop it walks the mission's own guidance line in paced hops, seats the bus on
the docking bay through `BusController.Teleport`, waits for the bay's own gate to dock,
opens the doors through `MissionManager.TryOpenDoorsAtCurrentStop` (the same entry point
the on-screen button uses) and then requires, without pressing anything else:

- `MissionManager.currentStopIndex` reaches `stopIndex + 1`;
- the guidance path was rebuilt for the new target (`GuidancePathVersion` increases);
- the navigator stayed live for the whole route: every five seconds the run samples
  `RouteArrowOverlay.HasVisibleArrows` / `BeaconVisible` and `HUDManager.NavigatorVisible`,
  and a missing sample fails the run (`SMOKE routeNavigator checks=… failures=…`). At the
  final stop the beacon and arrival banner are accepted as the navigator, because no road
  remains to draw chevrons on;
- the mission is `Completed` and `routeActive` is false after the last stop.

The log prints one `SMOKE routeStop` line per stop and a `SMOKE routeResult` summary.
A stop that does not release the route fails the run with the state attached
(`SMOKE route failed: stop 3 did not advance after 88.0s (processing=True
state=AtStop … animations=True dwell=52.0 budget=87.0)`), which names the culprit
instead of waiting out a timeout. The runner also fails on the failure modes that made
the bug look like a hang: an index that moved on its own (`SMOKE routeIndexDrift`), a leg
that cannot be closed (60 hops without getting closer, or 240 s per stop), and a route
that never finishes (1800 s overall). A leg the runner cannot walk is not quietly skipped:
it logs `SMOKE routeShortcut`, seats the bus on the bay, and the stop still has to dock
and release `currentStopIndex` on its own, and the summary counts `shortcuts=`. Between stops the same run checks the city
traffic: bodies in the pool actually move, cars hold their lane once settled, bodies on
two-way links sit on the country's driving side, imported models are textured and
liveries vary (`SMOKE traffic`, `SMOKE trafficSide`, `SMOKE liveries`). The traffic
verdict is taken independently of stop progression, so a route failure still reports
whether the road was alive.

The run is served in game time with `Time.timeScale = 3` (Unity keeps the authored
`fixedDeltaTime`, so the same number of physics steps happen per game second), which is
what keeps a 17-stop service to roughly 12 minutes of wall clock instead of 35.

**Latest route run (2026-10-26, Nairobi, 17 stops):** `Route check PASSED`, exit `0`.
`SMOKE routeResult stops=17 serviced=17 docked=17 advanced=17 failures=0 shortcuts=0
serviceAvg=45.0s serviceLongest=86.6s seconds=809 completed=True trafficMoved=6/12
trafficLaneChecked=True trafficLanes=True navigator=True passed=True`, with every stop
reporting `guidanceVersion=n->n+1 rebuilt=True` and the final one `indexAfter=17
state=Completed`. The navigator assertion ran **158** samples with **0 failures**
(`SMOKE routeNavigator checks=158 failures=0 arrows=False beacon=True banner=True
passed=True`; `arrows=False` is the final destination sample, where the beacon and
arrival banner are the navigator). The traffic sample from the same run: `SMOKE
trafficSide samples=19 wrongSide=0 reversed=2 … countryLeft=True passed=True`,
`SMOKE liveries painted=114 distinct=16 passed=True`, `SMOKE traffic pool=120
spawned=120 atOrigin=0 busDistance=47-400m models=12` and `SMOKE routeTraffic
window=6.1s active=12 moved=6 passed=True`.

Two runner bugs were found and fixed by writing this down rather than by a timeout. The
index-drift guard used to fire while the stop was still being served (advancing is the
assertion there, not drift), and the walker stepped straight at the bay, which drove the
bus through the buildings between it and the stop and let the collision response put it
back; it now follows the mission's guidance and only pulls in at the kerb once the line
has passed the stop.

### Loading screen gate

The loading card is not decorative. One `CinematicLoadingCard` is owned by `LoadingScreenUI` and survives the scene swap; `SceneLoader` fills it to 44 % during `LoadSceneAsync`, then `WorldReadyLoadingScreen` continues from exactly that value while the world builds. 100 % is written only after `WorldReadiness.Evaluate` reports `ready`, and drive input is held at zero until then. A headless run proves the gate from the log:

```bash
grep -aE "WorldPreload|world ready|Mission starting|Route started|world not ready" /tmp/smoke.log
```

Expected order on a healthy run: `[WorldPreload] Near-field ready in …`, `WorldReadyLoadingScreen: world ready in …`, then `Mission starting...` and `Route started: …`. The loader logs every ladder step with its elapsed time (`WorldReadyLoadingScreen: LOADING BUILDINGS... at 35.3s.`), so a slow load names the milestone it is sitting on. A `world not ready after 60s` warning means the 60 s ceiling expired; it prints the outstanding milestone names from `WorldReadiness.DescribePending`, so the missing system is named rather than guessed. The ceiling is a safety valve, not a pass: the bar keeps its last honest value and never shows 100 % for a world that is not playable.

The 39.6 MB Overture footprint pack is parsed on the thread pool (`OSMParser.ParseInBackground`), because parsing 63 748 footprints synchronously froze the card for tens of seconds. The log prints `OSM: parsed … bytes off-thread in …ms`; the last Nairobi run reports 5.3 s for the 0.6 MB road pack and 12.1 s for the footprint pack, both behind an animating bar. The remaining scene-construction cost (`SceneBootstrap` wiring, bus material repair, depot props) is still one synchronous first frame, which is why the world phase measures ~45 s in the Editor.

| Band | Milestone |
| --- | --- |
| 4 % | preparation beat, then `LoadSceneAsync` starts |
| 44 % | scene activated; world warm-up takes the same bar |
| 5 / 9 / 17 / 24 % | save restored, audio bound, bus spawned, vehicle configured |
| 30 / 35 / 48 % | map, GPS, road mesh |
| 62 / 74 % | building ring, centre tile |
| 80–95 % | `WorldPreloadCoordinator` near-field pass (per-stage ceilings 8–18 s, overall 32 s) |
| 97 / 98.5 % | cockpit HUD bound, depot placed (mission starts only) |
| 100 % | every milestone true, then a 0.9 s minimum + 0.6 s hold before input unlocks |

The EditMode cases pin the contract without a scene: `WorldSystemsTests.WorldReadiness_NeverReportsFullUntilTheWorldIsPlayable`, `WorldReadiness_ProgressAdvancesThroughTheLadderAndNeverRegresses`, `LoadingBar_HoldsOneMonotonicRunAcrossTheSceneHandOff` and `BusInputLock_HoldsEveryPedalAtZeroUntilTheWorldIsPlayable`.

### Menu-to-gameplay regression

`GameplaySmokeCheck` opens `GameScene` before entering play mode, so it does not exercise the path the player takes. `MenuFlowSmokeCheck` starts in `MainMenu`, selects a route, calls `SceneLoader.LoadGame()`, and fails unless the card is handed to `WorldReadyLoadingScreen` and then released:

```bash
LD_PRELOAD="$XML" xvfb-run -a "$UNITY" -batchmode -projectPath "$PWD" \
  -executeMethod MenuFlowSmokeCheck.Run -logFile /tmp/menu-flow.log
grep -a "MENU FLOW" /tmp/menu-flow.log
```

Expected: `MENU FLOW: WorldReadyLoadingScreen bootstrapped … card visible=True`, then `MENU FLOW PASS: card released … passed=True`, and exit status `0`. This is the regression for the menu transition never bootstrapping the world phase: a menu→`GameScene` load is a scene load inside an already-running play session, so the old `AfterSceneLoad` bootstrap never ran and the loading card stayed up for the whole drive. `WorldReadyLoadingScreen` and `WorldPreloadCoordinator` now subscribe to `SceneManager.sceneLoaded` from `BeforeSceneLoad` and bootstrap whenever a scene containing a `MissionManager` activates, which covers both the menu flow and a direct `GameScene` play.


### Road environment LOD and far clip

The road environment now has one policy for how much of it the camera draws. `SceneryLodPolicy` holds the far-plane budget and the per-kind street-prop distances; `SceneryLodBuilder` adds the missing three-level `LODGroup` to imported buildings and detailed street props. The camera far plane is clamped to **600–1000 m** (1000 m gameplay) instead of the old 1.5–2.5 km clamp and 4 km authored scene. Clear haze is opaque by 950 m, so the far plane sits inside the fog rather than showing the empty ring beyond the 600–800 m building stream.

LOD groups use three levels:

| Level | Building | Street prop |
| --- | --- | --- |
| LOD0 | full imported/procedural mesh | full imported prop |
| LOD1 | shared lit box, shadows on | shared lit box, shadows on |
| LOD2 | shared unlit box, no shadows | shared unlit box, no shadows |

The proxies share one built-in cube mesh and two materials, so the added memory is a transform and two renderers per object. A model that already ships an artist `LODGroup` (the four-level Coconut/Banana, the imported `10_f_building`, the LOD-named tree packs) is left untouched. Meshes below the triangle threshold (procedural infill houses) are already cheaper than a proxy and keep their full mesh.

Street props are culled by kind: benches, billboards and rocks at **130 m**, vegetation at **240 m**, pedestrians at **120 m**. The object stays alive, so walking back toward it is a re-activation rather than a re-spawn.

The EditMode cases pin the policy and the builder: `SceneryLodTests.TinyStreetPropsLeaveTheDrawPassBeforeVegetation`, `FarClipIsClampedIntoTheStreamedEnvelope`, `OnlyDetailedMeshesGetAThreeLevelGroup`, `AttachBuildsThreeLevelsWithASharedProxy` and `AttachLeavesAnArtistLodGroupAlone`.

**Latest run (2026-10-24):** EditMode **598 tests — 589 passed / 9 failed**, the same nine pre-existing failures. The Nairobi clear-noon smoke exited 0 with no LOD, material or null-reference errors: `SMOKE environment … fogStart=140 fogEnd=950 hazeOwnsFog=True`, `SMOKE perf samples=117 avgFps=8.4 p50Ms=92.9 p95Ms=251.0 … renderers=7097 farClip=1000 lodGroups=163`, and the world still reported `roads=True/True buildings=True/True mission=InProgress`. The software-GL frame time is noisy, so this run proves correctness, not a measured GPU win; a real-display pass is still needed to judge the LOD transitions and the 1 km far plane.

### Traffic and bus cost

`TrafficPerformancePolicy` owns the traffic budget and is read by `VehiclePool`, `SceneBootstrap`, `AIBusScheduleSpawner` and the spline controller, so the pool, the bootstrap and the ambient buses cannot drift apart:

| Knob | Value | Old value |
| --- | --- | --- |
| Pool bodies (catalog / fallback) | 120 / 64 | 160 / 120 |
| Active fraction (min–max) | 0.24–0.62 | 0.32–0.92 |
| Active floor (catalog / fallback) | 12 / 8 | 26 / 10 |
| Full-physics bodies | 12, radius 200 m | 20, radius 280 m |
| Spline band | 400 m | 520 m |
| Hard despawn / visible despawn | 600 / 280 m | 760 / 320 m |

The pool is not re-created while driving: bodies are instantiated once during the loading card (and ambient buses once into a spare stack) and then activated, re-homed and retired. `TrafficBodyOptimizer` runs once per spawned body and removes wheel colliders (player-only physics), non-trigger `MeshCollider`s (leaving one box where nothing solid remains) and driver `Animator`/per-car `AudioSource` components; a body with a lamp rig gets `TrafficPresentationCull`, which drops its lights past 160 m. `SplineVehicle.updateIntervalSeconds` is set from the policy by the tier pass, so a spline car updates every frame near the bus, at 20 Hz past 180 m and at 10 Hz past 320 m, with the elapsed time accumulated so its average speed is unchanged.

The EditMode cases pin the budget and the optimiser: `TrafficPerformanceTests.ActiveFleetStaysWellBelowThePoolAtEveryHour`, `PhysicsRingStaysInsideTheSplineRingAndTheDespawnBand`, `DensityFillRehomesAnythingOutsideTheSplineBand`, `SplineCadenceOnlyCoarsensDistantVehicles`, `ConsumeStepWaitsForTheIntervalThenKeepsTheWholeStep`, `PrepareStripsWheelCollidersAndMeshCollidersButKeepsAnAuthoredBox`, `PrepareAddsOneBoxColliderWhenOnlyAMeshColliderExisted`, `PrepareDisablesAnimatorAndAudioButKeepsTriggerVolumes`, `PrepareAddsThePresentationCullOnlyWhenTheBodyHasLights`, `PrepareLeavesThePlayerVehicleAlone` and `PrepareIsIdempotent`.

**Traffic run (2026-10-24, 08:00 clear, Nairobi):** EditMode **609 tests — 600 passed / 9 failed**, the same nine pre-existing failures. The smoke reported `SMOKE traffic pool=120 spawned=120 … busDistance=69-396m … density=69 densityTarget=72 fullAi=12 spline=57 dormant=51 renderers=527 poolRenderers=1161 wheelColliders=0 meshColliders=0 … offLane=0 … passed=True` against the previous `density=133 fullAi=20 spline=113`, and `SMOKE trafficSide … wrongSide=0 … passed=True`. Two consecutive runs landed at 69–71 active against the 72 target. The first pass of this change stalled at 45 because the density fill re-homed cars into a 520 m band while the tier pass retired anything past the 400 m spline band; `TrafficPerformancePolicy.DensitySpawnBandMeters` now makes both use the same band. Frame time in the same harness moved from `avgFps=8.4 p50Ms=92.9` to `avgFps=14.4 p50Ms=47.7`, but software GL is too noisy and the streamed building count differs between runs, so that is evidence of less work, not a measured GPU win.

**After the loading-screen bootstrap fix (same day):** EditMode stayed at **609 tests — 600 passed / 9 failed** (the same nine), and the 08:00 clear smoke exited `0` with `WorldReadyLoadingScreen: world ready in 41.2s`, `SMOKE traffic … density=72 densityTarget=72 fullAi=12 spline=60 dormant=48 wheelColliders=0 meshColliders=0` and `avgFps=22.9 p50Ms=30.9`. The lane audit reported one transient `offLane=1` (`Sedan1 … laneErr 3.74`, `skipOffLane=1`, 62 km/h) where the previous run had `offLane=0`; it was a full-physics body mid-link, not a rehome or a pool change, so it is recorded here as the known noisy sample rather than a regression. The menu transition itself is covered by `MenuFlowSmokeCheck` (see "Menu-to-gameplay regression" above).

## Imported model wiring

The bus, stop and environment packs under `Assets/unity models` are not wired by hand. Regenerate them in batch after replacing or adding a source FBX:

```bash
XML=/home/drac/Unity/Hub/Editor/6000.6.0f1/Editor/Data/PlaybackEngines/AndroidPlayer/NDK/toolchains/llvm/prebuilt/linux-x86_64/lib/libxml2.so.2
UNITY=/home/drac/Unity/Hub/Editor/6000.6.0f1/Editor/Unity

# Buses, bus stops, gas stations, the road pack and depot props
LD_PRELOAD="$XML" xvfb-run -a "$UNITY" -batchmode -quit -projectPath "$PWD" \
  -executeMethod ImportedModelWiringGenerator.GenerateFromBatch \
  -logFile /tmp/imported-models.log

# Imported buildings: PBR surface profiles, normal/AO maps and prefab colliders
LD_PRELOAD="$XML" xvfb-run -a "$UNITY" -batchmode -quit -projectPath "$PWD" \
  -executeMethod BuildingModelImporter.Build \
  -logFile /tmp/building-models.log

# Streamed environment catalog, prefabs and materials (including LODGroups)
LD_PRELOAD="$XML" xvfb-run -a "$UNITY" -batchmode -quit -projectPath "$PWD" \
  -executeMethod LiveCityModelImporter.Build \
  -logFile /tmp/live-city-models.log

# Both passes in one editor launch
LD_PRELOAD="$XML" xvfb-run -a "$UNITY" -batchmode -quit -projectPath "$PWD" \
  -executeMethod ImportedModelWiringGenerator.GenerateContentFromBatch \
  -logFile /tmp/imported-content.log
```

`ImportedModelWiringGenerator` scans `Assets/unity models/bus`, `busstop` and the `parking lot` stop folders recursively, dedupes nested copies by normalised model stem, prefers the candidate with the most nearby textures, and keeps the top-level path as the stable prefab/spec identity. The pass generates **14 drivable buses, 9 bus stops, 2 gas stations, 1 road pack and 3 depot props**. `LiveCityModelImporter` dedupes the environment packs the same way, removes `UCX_`, camera-path and helper-ground renderers, and writes the **16-entry** `Assets/_Game/Resources/LiveCityCatalog.asset`. For trees and plants it rebuilds a real `LODGroup` from the source `LOD0`–`LOD3` meshes; catalog bounds and the triangle budget use only `LOD0`, so the higher levels do not inflate the streamed budget. `ForestPack` and the dense sources above the 65k-triangle / 22 m thresholds are skipped.

`ImportedModelImportSettings` forces the game-ready FBXs through Unity's Standard material importer. `ImportedModelTextureBinder` then binds maps by material/model filename, safe per-model texture folders, glass-slot exclusion, a livery fallback for `bodi`, and embedded `aiStandardSurface` base-colour pairs. Generated material overrides live in `Assets/_Game/Generated/Materials/Buses`, `Assets/_Game/Generated/Materials/Stops`, `Assets/_Game/Resources/LiveCityGenerated` and `Assets/_Game/Resources/BuildingGenerated`; unreferenced generated materials should be removed after a regeneration.

The realism pass keeps the project on the **Built-in Render Pipeline** (there is no URP package or pipeline asset), so `Standard` is the equivalent of URP/Lit. `BuildingModelImporter` classifies masonry/concrete/plaster, glass, metal and wood and sets metallic/smoothness per surface (concrete ~0.22, glass ~0.86, metal 0.65 metallic / 0.55 smooth, wood ~0.34), binds normal/AO maps, raises occlusion strength, keeps instancing on, and applies a stable near-neutral per-instance tint at placement. `UrbanMaterials`/`RealBus/Facade` raise the wall smoothness from 0.06 to 0.18, add ground grime, and drive `o.Occlusion` from the contact shadow. `ImportedFoliageVariation` gives each spawned tree/plant one of four shared green material variants and keeps GPU instancing enabled; trunks, bark and branches are excluded.

The generated content is exercised by the headless checks above. `GameplaySmokeCheck.Run` reports `SMOKE roadClearance stopModels=... blockedStops=0 crossingKerbs=0`, `SMOKE stopSides anchors=17 wrongSide=0 backwards=0 unmeasured=0` (against the driven `pathPoints`, not the shelter's own rotation) and `SMOKE depotStreet props=0`, and `GameplaySmokeCheck.RunLiveCity` checks the streamed props and the unload path. Both need `REALBUS_SMOKE_SKIP_CAPTURE=1` under software rendering.

**Latest content verification (2026-10-24):** both importers exited 0 after the realism pass (15 building prefabs, 16 scenery entries; Banana and Coconut now carry four-level LODGroups). `dotnet build GameScripts.csproj` and `GameScripts.Editor.csproj` report 0 errors. EditMode is **593 tests — 584 passed / 9 failed**, the same nine pre-existing failures (eight in `GameScriptsTests`, one in `Phase6ProgressionTests`). The traffic density clock now follows `TimeOfDaySystem`, so pinned runs move the daily curve: Nairobi at 01:00 / 08:00 / 17:00 reports `densityVolume=0.12 / 0.96 / 0.95` with `density=62 / 117 / 139` non-dormant cars out of the 160-vehicle pool. The lane probe now leaves an 8 m (or 1.5 bus lengths) margin at each junction and judges the settled middle of a link: all four runs pass with `offLane=0`, `worstLaneDeviation` 0.03–3.14 m and `worstSteadyLaneDeviation` 0.01–0.65 m. A separate side audit answers "some cars drive on the wrong side": `SMOKE trafficSide samples=39 wrongSide=0 reversed=1 pivotOffsets=0 worstPivot=0.05` in Nairobi (down from `reversed=5`, one at `dot=-1.00` and 20 km/h). It judges each settled two-way body against the country's driving side rather than the lane its own graph assigned, so a stale `drivesOnLeft` default or a mirrored lane cannot pass, and it prints `skipChanging/skipInvalid/skipOneWay/skipMargin/skipOffLane` so coverage is visible. The cause was the edge picker: `GetRandomNextNode` now refuses an edge that points back down the road (`dot < -0.2`) while a forward edge exists, and `AIVehicleController.HardTurnSpeedLimitKmh` (shared with `SplineVehicle`) makes a misaligned body creep instead of sliding across at cruise. Four EditMode cases cover it: the backward edge is never picked in 200 draws, a dead end still reverses, a reversed link mirrors its lane offset across the centre line (the reason the offset must not be "fixed"), and the hard-turn cap. Stop shelters are on the docking-side kerb in both driving directions — `stopSides anchors=17 wrongSide=0 backwards=0 leftTraffic=True` in Nairobi and `anchors=18 wrongSide=0 leftTraffic=False` in Berlin. That probe follows `BusRoute.pathPoints`: 93 of 120 route assets list stops out of path order, and the old stop→next-stop chord pointed backwards along the path for 747 of 2014 stops (37.1 %), so folding onto that chord was what mirrored shelters onto the far kerb. The depot audit reports `depotStreet props=0`: with the driveways now stopping on the kerb line and a `PropReachesStreet` guard on imported structures, the workshop and parked fleet buses, no yard prop reaches the carriageway. `roadClearance stopModels=31–36 blockedStops=0 crossingKerbs=0 passed=True` on the same runs, and `GameplaySmokeCheck.Run` exits 0. The docking pass gate is 2 m lateral, 7 m fore/aft, 40° and 3 km/h (the strict `GameplayRules.IsDocked` defaults remain the gold reference); `GameplayRulesTests.DockingZoneGateAllowsARealKerbGapButStillRequiresTheKerb` covers it. `RunLiveCity` was not re-run in this pass; its last verified result stands (`props=104 walkers=1 triangles=1184528 roadOverlaps=0`, then `props=0 triangles=0`). A real-display review of grime, glass reflections, green variation, LOD transitions and the new minimap roads is still pending.

## Cinematic exposure and colour grade

The grade is not a scene component and not a package. `CinematicAtmosphereController` attaches `CinematicColorGrade` to `Camera.main` on scene load and after a quality change; the component runs one `OnRenderImage` pass with `Assets/_Game/Resources/Shaders/RealBusCinematicGrade.shader` (also in `GraphicsSettings`' Always Included Shaders). It is disabled on the Very Low tier and reattached after scene reloads because the atmosphere controller is `DontDestroyOnLoad`.

The pass reads `TimeOfDaySystem.SunElevationDegrees` and `SkyEnvironmentController.cloudCoverage`; it does not own the sun, ambient, fog or reflections. That means a smoke run can assert both the lighting and the grade in the same `SMOKE environment` line. A normal clear-noon run should show `grade=True`, exposure near `1.00`, contrast near `1.06`, saturation near `1.08`, and a near-white tint; a clear-night run should show exposure above `1.0`, saturation below `1.0`, and a blue-biased tint.

Manual screenshot capture (`/tmp/realbus-exterior.png`, `/tmp/realbus-cockpit.png`) still stalls under software rendering, including with `REALBUS_SMOKE_MSAA=1`, so batch runs should use `REALBUS_SMOKE_SKIP_CAPTURE=1`. The final look, especially vignette strength and exposure at golden hour, needs a real display/device pass.

## Checks that need no Unity license

The licence is currently valid, so the normal EditMode and smoke commands above run. If an offline
entitlement expires, the editor stops before any test and exits `198` with `No valid Unity Editor license
found`; `~/.config/unity3d/Unity/Unity.Licensing.Client.log` names the reason, and
`LicenseGroupOfflineValidityPeriodIsExpired` means Unity Hub has to sign in once so the licence can be
renewed online. Everything below still works while that is outstanding.

- `dotnet build GameScripts.csproj` (and `GameScripts.Editor.csproj`, `GameScriptsTests.Editor.csproj`)
  compiles the game, editor and test assemblies against the editor's own managed assemblies, so a
  compile error is caught without starting Unity.
- `python3 tools/check_vehicle_facing.py` replays the traffic import's facing rule over the downloaded
  vehicle FBXs and prints which way each model faces, so traffic orientation can be checked without the
  editor.

## Notes

- On Linux CI or headless machines, `xvfb-run` is used automatically when available.
- `libxml2.so.2` only exists in Unity's bundled Android NDK on this machine (the system has `.so.16`), so the editor will not start without the `LD_PRELOAD` above; `tools/open_unity_6000_6.sh` does the same for the interactive editor.
- Unity batchmode still requires a valid Unity license.
- The runner filters to `GameScriptsTests.Editor` so unrelated third-party test assemblies do not fail your production check.
- If the run fails before tests start, inspect `TestResults/editmode-unity.log` first.
