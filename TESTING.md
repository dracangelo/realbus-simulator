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

The assembled scene runs a compressed world clock: `TimeOfDaySystem` starts at the timetable's opening hour and covers a full 24 game hours in 30 real minutes, while `ScheduleManager` keeps its own one-game-minute-per-real-minute timetable for stop punctuality. A pinned `REALBUS_SMOKE_TIME` disables that advance so the sky holds still for the duration of a smoke run. The weather cycle starts after the real WeatherService lookup, or after a short 45-second hold when the offline simulated weather is used, then holds each state for 90–240 real seconds and cross-fades over 45–90 seconds.

## Imported model wiring

The bus, stop and environment packs under `Assets/unity models` are not wired by hand. Regenerate them in batch after replacing or adding a source FBX:

```bash
XML=/home/drac/Unity/Hub/Editor/6000.6.0f1/Editor/Data/PlaybackEngines/AndroidPlayer/NDK/toolchains/llvm/prebuilt/linux-x86_64/lib/libxml2.so.2
UNITY=/home/drac/Unity/Hub/Editor/6000.6.0f1/Editor/Unity

# Buses, bus stops, gas stations, the road pack and depot props
LD_PRELOAD="$XML" xvfb-run -a "$UNITY" -batchmode -quit -projectPath "$PWD" \
  -executeMethod ImportedModelWiringGenerator.GenerateFromBatch \
  -logFile /tmp/imported-models.log

# Streamed environment catalog, prefabs and materials
LD_PRELOAD="$XML" xvfb-run -a "$UNITY" -batchmode -quit -projectPath "$PWD" \
  -executeMethod LiveCityModelImporter.Build \
  -logFile /tmp/live-city-models.log

# Both passes in one editor launch
LD_PRELOAD="$XML" xvfb-run -a "$UNITY" -batchmode -quit -projectPath "$PWD" \
  -executeMethod ImportedModelWiringGenerator.GenerateContentFromBatch \
  -logFile /tmp/imported-content.log
```

`ImportedModelWiringGenerator` scans `Assets/unity models/bus`, `busstop` and the `parking lot` stop folders recursively, dedupes nested copies by normalised model stem, prefers the candidate with the most nearby textures, and keeps the top-level path as the stable prefab/spec identity. The pass generates **14 drivable buses, 9 bus stops, 2 gas stations, 1 road pack and 3 depot props**. `LiveCityModelImporter` dedupes the environment packs the same way, keeps LOD0, removes `UCX_`, camera-path and helper-ground renderers, and writes the **16-entry** `Assets/_Game/Resources/LiveCityCatalog.asset`. `ForestPack` and the dense sources above the 65k-triangle / 22 m thresholds are skipped.

`ImportedModelImportSettings` forces the game-ready FBXs through Unity's Standard material importer. `ImportedModelTextureBinder` then binds maps by material/model filename, safe per-model texture folders, glass-slot exclusion, a livery fallback for `bodi`, and embedded `aiStandardSurface` base-colour pairs. Generated material overrides live in `Assets/_Game/Generated/Materials/Buses`, `Assets/_Game/Generated/Materials/Stops` and `Assets/_Game/Resources/LiveCityGenerated`; unreferenced generated materials should be removed after a regeneration.

The generated content is exercised by the headless checks above. `GameplaySmokeCheck.Run` reports `SMOKE roadClearance stopModels=... blockedStops=0 crossingKerbs=0`, and `GameplaySmokeCheck.RunLiveCity` checks the streamed props and the unload path. Both need `REALBUS_SMOKE_SKIP_CAPTURE=1` under software rendering.

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
