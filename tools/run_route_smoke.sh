#!/usr/bin/env bash
# Runs the whole route headlessly and fails unless every stop service moves
# MissionManager.currentStopIndex on, the route reaches Completed, and the
# city traffic is actually moving. Batch-only; see GameplaySmokeCheck.RunRoute.
set -uo pipefail

PROJECT_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
RESULTS_DIR="${PROJECT_ROOT}/TestResults"
LOG_FILE="${RESULTS_DIR}/route-smoke.log"
UNITY_BIN="${UNITY_BIN:-${HOME}/Unity/Hub/Editor/6000.6.0f1/Editor/Unity}"

mkdir -p "${RESULTS_DIR}"
# Unity only appends, so a previous run's pass could be read as this one's.
rm -f "${LOG_FILE}"

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

echo "Project: ${PROJECT_ROOT}"
echo "Unity:   ${UNITY_BIN}"
echo "Log:     ${LOG_FILE}"
echo

UNITY_STATUS=0
"${RUNNER_PREFIX[@]}" "${UNITY_BIN}" \
  -batchmode \
  -projectPath "${PROJECT_ROOT}" \
  -executeMethod GameplaySmokeCheck.RunRoute \
  -logFile "${LOG_FILE}"
UNITY_STATUS=$?

if grep -q "error CS" "${LOG_FILE}" 2>/dev/null; then
  echo >&2
  echo "Scripts did not compile. First compiler errors:" >&2
  grep -m 10 "error CS" "${LOG_FILE}" >&2 || true
  exit 1
fi

echo "Route summary:"
grep -aE "SMOKE (route start|routeStop|routeShortcut|routeDock|routeResult|routeNavigator|route failed|routeFailure|routeIndexDrift|routeTraffic|traffic |trafficSide|liveries)" \
  "${LOG_FILE}" || true

if [[ "${UNITY_STATUS}" -ne 0 ]]; then
  echo >&2
  echo "Route check FAILED (Unity exit ${UNITY_STATUS}). Full log: ${LOG_FILE}" >&2
else
  echo
  echo "Route check PASSED."
fi

exit "${UNITY_STATUS}"
