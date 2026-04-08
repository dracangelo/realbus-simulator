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

## Notes

- On Linux CI or headless machines, `xvfb-run` is used automatically when available.
- Unity batchmode still requires a valid Unity license.
- The runner filters to `GameScriptsTests.Editor` so unrelated third-party test assemblies do not fail your production check.
- If the run fails before tests start, inspect `TestResults/editmode-unity.log` first.
