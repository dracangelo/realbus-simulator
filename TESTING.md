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

The generated content is exercised by the headless checks above. `GameplaySmokeCheck.Run` reports `SMOKE roadClearance stopModels=... blockedStops=0 crossingKerbs=0`, `SMOKE stopSides anchors=17 wrongSide=0` and `SMOKE depotStreet props=0`, and `GameplaySmokeCheck.RunLiveCity` checks the streamed props and the unload path. Both need `REALBUS_SMOKE_SKIP_CAPTURE=1` under software rendering.

**Latest content verification (2026-10-24):** both importers exited 0 after the realism pass (15 building prefabs, 16 scenery entries; Banana and Coconut now carry four-level LODGroups). `dotnet build GameScripts.csproj` and `GameScripts.Editor.csproj` report 0 errors. EditMode is **582 tests — 573 passed / 9 failed**, the same nine pre-existing failures (eight in `GameScriptsTests`, one in `Phase6ProgressionTests`). The traffic density clock now follows `TimeOfDaySystem`, so pinned runs move the daily curve: Nairobi at 01:00 / 08:00 / 17:00 reports `densityVolume=0.12 / 0.96 / 0.95` with `density=62 / 117 / 139` non-dormant cars out of the 160-vehicle pool. The lane probe now leaves an 8 m (or 1.5 bus lengths) margin at each junction and judges the settled middle of a link: all four runs pass with `offLane=0`, `worstLaneDeviation` 0.03–3.14 m and `worstSteadyLaneDeviation` 0.01–0.49 m. Stop shelters are on the docking-side kerb in both driving directions — `stopSides anchors=17 wrongSide=0 leftTraffic=True` in Nairobi and `anchors=18 wrongSide=0 leftTraffic=False` in Berlin. The depot audit reports `depotStreet props=0`: with the driveways now stopping on the kerb line and a `PropReachesStreet` guard on imported structures, the workshop and parked fleet buses, no yard prop reaches the carriageway. `roadClearance stopModels=32–36 blockedStops=0 crossingKerbs=0 passed=True` on the same runs, and `GameplaySmokeCheck.Run` exits 0. `RunLiveCity` was not re-run in this pass; its last verified result stands (`props=104 walkers=1 triangles=1184528 roadOverlaps=0`, then `props=0 triangles=0`). A real-display review of grime, glass reflections, green variation, LOD transitions and the new minimap roads is still pending.

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
