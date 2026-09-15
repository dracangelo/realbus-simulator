#!/usr/bin/env bash
set -euo pipefail

PROJECT_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
UNITY_EDITOR="${REALBUS_UNITY_BIN:-/home/drac/Unity/Hub/Editor/6000.6.0f1/Editor/Unity}"
UNITY_XML_COMPAT="$(dirname "${UNITY_EDITOR}")/Data/PlaybackEngines/AndroidPlayer/NDK/toolchains/llvm/prebuilt/linux-x86_64/lib/libxml2.so.2"

if [[ ! -x "${UNITY_EDITOR}" ]]; then
  echo "Unity editor not found: ${UNITY_EDITOR}" >&2
  exit 1
fi

if [[ -f "${UNITY_XML_COMPAT}" ]]; then
  export LD_PRELOAD="${UNITY_XML_COMPAT}${LD_PRELOAD:+:${LD_PRELOAD}}"
fi

exec "${UNITY_EDITOR}" -projectPath "${PROJECT_ROOT}" "$@"
