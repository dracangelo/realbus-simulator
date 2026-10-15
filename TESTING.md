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

`REALBUS_SMOKE_TIME` is minutes past midnight (0 = midnight, 780 = 13:00) and `REALBUS_SMOKE_WEATHER` is any `WeatherState` name (`Clear`, `PartlyCloudy`, `Overcast`, `Fog`, `Drizzle`, `LightRain`, `HeavyRain`, `Thunderstorm`, `Snow`, `Blizzard`). The `SMOKE environment` line reports the skybox, cloud coverage, star visibility, moonlight, sun elevation, fog distances and colour, and the precipitation emission rates, and the check fails the run when the environment is missing or when rain and weather disagree.

## Checks that need no Unity license

An expired offline entitlement stops the editor before any test runs, and the run then exits `198` with
`No valid Unity Editor license found`. `~/.config/unity3d/Unity/Unity.Licensing.Client.log` names the
reason; `LicenseGroupOfflineValidityPeriodIsExpired` means Unity Hub has to sign in once so the licence
can be renewed online. Everything below still works while that is outstanding.

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
