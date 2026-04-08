#!/usr/bin/env bash
set -euo pipefail

PROJECT_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
RESULTS_DIR="${PROJECT_ROOT}/TestResults"
UNITY_BIN="${UNITY_BIN:-/home/vng370/Unity/Hub/Editor/6000.3.10f1/Editor/Unity}"
TEST_PLATFORM="${TEST_PLATFORM:-editmode}"
TEST_ASSEMBLIES="${TEST_ASSEMBLIES:-GameScriptsTests.Editor}"
TEST_RESULTS_FILE="${RESULTS_DIR}/${TEST_PLATFORM}-results.xml"
UNITY_LOG_FILE="${RESULTS_DIR}/${TEST_PLATFORM}-unity.log"
COVERAGE_DIR="${RESULTS_DIR}/Coverage"

mkdir -p "${RESULTS_DIR}" "${COVERAGE_DIR}"

if [[ ! -x "${UNITY_BIN}" ]]; then
  echo "Unity executable not found at: ${UNITY_BIN}" >&2
  echo "Set UNITY_BIN to your editor path and rerun." >&2
  exit 1
fi

RUNNER_PREFIX=()
if command -v xvfb-run >/dev/null 2>&1; then
  RUNNER_PREFIX=(xvfb-run -a)
fi

echo "Project: ${PROJECT_ROOT}"
echo "Unity:   ${UNITY_BIN}"
echo "Tests:   ${TEST_ASSEMBLIES}"
echo "Results: ${TEST_RESULTS_FILE}"
echo "Log:     ${UNITY_LOG_FILE}"
echo "Coverage:${COVERAGE_DIR}"
echo

"${RUNNER_PREFIX[@]}" "${UNITY_BIN}" \
  -batchmode \
  -projectPath "${PROJECT_ROOT}" \
  -runTests \
  -testPlatform "${TEST_PLATFORM}" \
  -assemblyNames "${TEST_ASSEMBLIES}" \
  -testResults "${TEST_RESULTS_FILE}" \
  -logFile "${UNITY_LOG_FILE}" \
  -debugCodeOptimization \
  -enableCodeCoverage \
  -coverageResultsPath "${COVERAGE_DIR}" \
  -coverageOptions "generateAdditionalMetrics;generateHtmlReport;generateBadgeReport;assemblyFilters:+GameScripts,+GameScripts.Editor,-GameScriptsTests.Editor"

if [[ ! -f "${TEST_RESULTS_FILE}" ]]; then
  echo >&2
  echo "Unity exited without writing ${TEST_RESULTS_FILE}." >&2
  echo "This usually means the Unity Test Runner did not discover or execute a test assembly." >&2
  echo "Check the Unity log: ${UNITY_LOG_FILE}" >&2
  echo >&2
  tail -n 80 "${UNITY_LOG_FILE}" >&2 || true
  exit 1
fi

echo
echo "Unity test run completed."
echo "XML results: ${TEST_RESULTS_FILE}"
echo "Unity log:   ${UNITY_LOG_FILE}"
echo "Coverage:    ${COVERAGE_DIR}"

if [[ -f "${TEST_RESULTS_FILE}" ]]; then
  echo
  echo "Result summary:"
  grep '<test-run ' "${TEST_RESULTS_FILE}" \
    | grep -Eo 'total="[0-9]+"|passed="[0-9]+"|failed="[0-9]+"|inconclusive="[0-9]+"|skipped="[0-9]+"' \
    || true
fi

if [[ -z "$(find "${COVERAGE_DIR}" -mindepth 1 -print -quit 2>/dev/null)" ]]; then
  echo
  echo "Coverage directory is empty. If tests ran, inspect ${UNITY_LOG_FILE} for coverage-package warnings."
fi
