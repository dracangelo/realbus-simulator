#!/usr/bin/env bash
# Runs the whole route headlessly and reports a verdict a CI job can gate on.
#
# GameplaySmokeCheck.RunRoute writes the verdict itself (defaults to
# TestResults/route-smoke.json, override with REALBUS_SMOKE_RESULT) and exits
# with it. This script is the outer gate: it fails closed when that file is
# missing or unreadable, so a crashed or unlicensed editor cannot look like a
# passing route.
#
# Exit status:
#   0  route check passed    (result "passed": true)
#   1  the check could not run (missing Unity, compile error, no JSON reader)
#   2  the route check failed (stop progression, traffic, navigator)
#   3  the check produced no readable result (crash, licence, early exit)
#   4  the run exceeded ROUTE_TIMEOUT_SECONDS and was killed
set -uo pipefail

PROJECT_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
RESULTS_DIR="${PROJECT_ROOT}/TestResults"
LOG_FILE="${RESULTS_DIR}/route-smoke.log"
RESULT_FILE="${REALBUS_SMOKE_RESULT:-${RESULTS_DIR}/route-smoke.json}"
UNITY_BIN="${UNITY_BIN:-${HOME}/Unity/Hub/Editor/6000.6.0f1/Editor/Unity}"
ROUTE_TIMEOUT_SECONDS="${ROUTE_TIMEOUT_SECONDS:-2700}"

mkdir -p "${RESULTS_DIR}"
# Unity only appends to its log, and an old JSON is indistinguishable from a new
# one, so both are removed first: no artifact from a previous run can be read as
# this run's verdict.
rm -f "${LOG_FILE}" "${RESULT_FILE}"
export REALBUS_SMOKE_RESULT="${RESULT_FILE}"

# A runner-level failure still has to leave a JSON file, otherwise the CI job has
# to distinguish "no result because it crashed" from "no result because it never
# started" by exit status alone.
write_runner_result() {
  local code="$1" message="$2" escaped
  escaped="${message//\\/\\\\}"
  escaped="${escaped//\"/\\\"}"
  {
    printf '{\n'
    printf '  "schemaVersion": 1,\n'
    printf '  "check": "route-smoke",\n'
    printf '  "source": "runner",\n'
    printf '  "passed": false,\n'
    printf '  "exitCode": %s,\n' "${code}"
    printf '  "failures": ["%s"],\n' "${escaped}"
    printf '  "logPath": "%s",\n' "${LOG_FILE//\"/\\\"}"
    printf '  "resultPath": "%s"\n' "${RESULT_FILE//\"/\\\"}"
    printf '}\n'
  } > "${RESULT_FILE}"
}

fail() {
  local code="$1" message="$2"
  # Keep the verdict the editor wrote, even when it is the thing being rejected:
  # the contradiction is the evidence, and the primary file must agree with the
  # exit status the job gates on.
  if [[ -s "${RESULT_FILE}" ]]; then
    mv -f "${RESULT_FILE}" "${RESULT_FILE%.json}.previous.json" 2>/dev/null || true
  fi
  write_runner_result "${code}" "${message}"
  echo >&2
  echo >&2 "${message}"
  echo >&2 "Result: ${RESULT_FILE} (exit ${code})"
  exit "${code}"
}

if [[ ! -x "${UNITY_BIN}" ]]; then
  echo "Unity executable not found at: ${UNITY_BIN}" >&2
  echo "Set UNITY_BIN to your editor path and rerun." >&2
  exit 1
fi

# This host ships libxml2 2.16, whose soname (libxml2.so.16) the editor cannot
# load. The NDK copy Unity already installs provides the 2 ABI it expects.
UNITY_XML_COMPAT="$(dirname "${UNITY_BIN}")/Data/PlaybackEngines/AndroidPlayer/NDK/toolchains/llvm/prebuilt/linux-x86_64/lib/libxml2.so.2"
if [[ -f "${UNITY_XML_COMPAT}" ]]; then
  export LD_PRELOAD="${UNITY_XML_COMPAT}${LD_PRELOAD:+:${LD_PRELOAD}}"
fi

RUNNER_PREFIX=()
if command -v xvfb-run >/dev/null 2>&1; then
  RUNNER_PREFIX=(xvfb-run -a)
fi

# A bounded runtime: a hung editor would otherwise hold the CI job forever. The
# route's own budget is 1800 game seconds at 3x, roughly 10 minutes of wall clock
# including the world build, so the default leaves ample headroom.
TIMEOUT_PREFIX=()
if command -v timeout >/dev/null 2>&1; then
  TIMEOUT_PREFIX=(timeout --foreground --kill-after=60 "${ROUTE_TIMEOUT_SECONDS}")
fi

echo "Project: ${PROJECT_ROOT}"
echo "Unity:   ${UNITY_BIN}"
echo "Log:     ${LOG_FILE}"
echo "Result:  ${RESULT_FILE}"
echo "Timeout: ${ROUTE_TIMEOUT_SECONDS}s"
echo

"${RUNNER_PREFIX[@]}" "${TIMEOUT_PREFIX[@]}" "${UNITY_BIN}" \
  -batchmode \
  -projectPath "${PROJECT_ROOT}" \
  -executeMethod GameplaySmokeCheck.RunRoute \
  -logFile "${LOG_FILE}"
UNITY_STATUS=$?

if grep -q "error CS" "${LOG_FILE}" 2>/dev/null; then
  echo >&2
  echo "Scripts did not compile. First compiler errors:" >&2
  grep -m 10 "error CS" "${LOG_FILE}" >&2 || true
  fail 1 "the route check could not run: the editor assembly did not compile"
fi

if [[ "${UNITY_STATUS}" -eq 124 || "${UNITY_STATUS}" -eq 137 ]]; then
  fail 4 "the route check exceeded ${ROUTE_TIMEOUT_SECONDS}s and was killed"
fi

if [[ "${UNITY_STATUS}" -eq 198 ]]; then
  fail 3 "the editor exited 198: no valid Unity Editor licence was found"
fi

if [[ ! -s "${RESULT_FILE}" ]]; then
  fail 3 "Unity exited ${UNITY_STATUS} without writing ${RESULT_FILE}; the route verdict is unknown"
fi

if command -v jq >/dev/null 2>&1; then
  JSON_TOOL="jq"
elif command -v python3 >/dev/null 2>&1; then
  JSON_TOOL="python3"
else
  fail 1 "neither jq nor python3 is installed, so ${RESULT_FILE} cannot be read"
fi

result_passed() {
  if [[ "${JSON_TOOL}" == "jq" ]]; then
    jq -r 'if (.passed | type) == "boolean" then (.passed | tostring) else empty end' \
      "${RESULT_FILE}" 2>/dev/null
  else
    python3 -c 'import json,sys
d = json.load(open(sys.argv[1]))
p = d.get("passed")
print(str(p).lower() if isinstance(p, bool) else "")' "${RESULT_FILE}" 2>/dev/null
  fi
}

print_result_summary() {
  if [[ "${JSON_TOOL}" == "jq" ]]; then
    jq -r '
      "route: \(.stops.advanced // 0)/\(.stops.total // 0) stops advanced (serviced \(.stops.serviced // 0), docked \(.stops.docked // 0)) completed=\(.completed) shortcuts=\(.shortcuts // 0)",
      "traffic: moved \(.traffic.moved // 0)/\(.traffic.active // 0) lanesChecked=\(.traffic.lanesChecked) lanesPassed=\(.traffic.lanesPassed)",
      "navigator: \(.navigator.checks // 0) samples, \(.navigator.failures // 0) failures",
      "duration: \(.durationSeconds // 0)s, perf p50=\(.performance.p50Ms // 0)ms p95=\(.performance.p95Ms // 0)ms",
      "failures:",
      ((.failures // [])[] | "  - " + .)
    ' "${RESULT_FILE}" 2>/dev/null
  else
    python3 - "${RESULT_FILE}" <<'PY'
import json, sys

d = json.load(open(sys.argv[1]))
s, t, n, p = d.get("stops", {}), d.get("traffic", {}), d.get("navigator", {}), d.get("performance", {})
print(f"route: {s.get('advanced', 0)}/{s.get('total', 0)} stops advanced "
      f"(serviced {s.get('serviced', 0)}, docked {s.get('docked', 0)}) "
      f"completed={d.get('completed')} shortcuts={d.get('shortcuts', 0)}")
print(f"traffic: moved {t.get('moved', 0)}/{t.get('active', 0)} "
      f"lanesChecked={t.get('lanesChecked')} lanesPassed={t.get('lanesPassed')}")
print(f"navigator: {n.get('checks', 0)} samples, {n.get('failures', 0)} failures")
print(f"duration: {d.get('durationSeconds', 0)}s, "
      f"perf p50={p.get('p50Ms', 0)}ms p95={p.get('p95Ms', 0)}ms")
print("failures:")
for failure in d.get("failures", []):
    print(f"  - {failure}")
PY
  fi
}

echo
echo "Result summary:"
print_result_summary
echo
echo "Machine-readable result: ${RESULT_FILE}"
echo "Unity log:               ${LOG_FILE}"

RESULT_PASSED="$(result_passed)"

if [[ "${RESULT_PASSED}" == "true" ]]; then
  if [[ "${UNITY_STATUS}" -ne 0 ]]; then
    fail 3 "the result says passed but Unity exited ${UNITY_STATUS}; the two must agree"
  fi
  if [[ "${JSON_TOOL}" == "jq" ]] && ! jq -e '.failures | length == 0' "${RESULT_FILE}" >/dev/null 2>&1; then
    fail 3 "the result says passed but lists failures; refusing to gate on a contradictory result"
  fi
  echo
  echo "Route check PASSED."
  exit 0
fi

if [[ "${RESULT_PASSED}" != "false" ]]; then
  fail 3 "${RESULT_FILE} has no readable \"passed\" field"
fi

echo
echo "Last failure lines from the log:"
grep -aE "SMOKE (routeFailure|route failed|routeIndexDrift|routeJson|blockedStop|stopWrongSide|wrongSide)" \
  "${LOG_FILE}" 2>/dev/null | tail -n 20 || true
echo >&2
echo "Route check FAILED (exit 2). Verdict in ${RESULT_FILE}, log in ${LOG_FILE}." >&2
exit 2
