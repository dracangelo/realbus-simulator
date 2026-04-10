#!/usr/bin/env bash
set -euo pipefail

if [ "$#" -ne 5 ]; then
  cat <<'EOF'
Usage:
  scripts/fetch_osm_city_json.sh <city_code> <min_lat> <min_lon> <max_lat> <max_lon>

Example:
  scripts/fetch_osm_city_json.sh NBO -1.2900 36.8000 -1.2630 36.8280

Outputs:
  Assets/StreamingAssets/Cities/<city_code>/roads.json
  Assets/StreamingAssets/Cities/<city_code>/stops.json
EOF
  exit 1
fi

city_code="$1"
min_lat="$2"
min_lon="$3"
max_lat="$4"
max_lon="$5"

if ! command -v python3 >/dev/null 2>&1; then
  echo "python3 is required for merging tiled Overpass JSON responses." >&2
  exit 1
fi

out_dir="Assets/StreamingAssets/Cities/${city_code}"
roads_file="${out_dir}/roads.json"
stops_file="${out_dir}/stops.json"
bbox="${min_lat},${min_lon},${max_lat},${max_lon}"
overpass_url="https://overpass-api.de/api/interpreter"
roads_tiles=3
stops_tiles=2
retry_delay_seconds=2

mkdir -p "${out_dir}"
tmp_dir="$(mktemp -d)"
trap 'rm -rf "${tmp_dir}"' EXIT

post_query() {
  local query="$1"
  local output_file="$2"

  curl --fail --silent --show-error \
    -X POST "${overpass_url}" \
    --data-urlencode "data=${query}" \
    -o "${output_file}"
}

fetch_tiled_query() {
  local kind="$1"
  local tiles_per_axis="$2"
  local query_template="$3"
  local output_file="$4"

  local tile_dir="${tmp_dir}/${kind}"
  mkdir -p "${tile_dir}"

  python3 - "$min_lat" "$min_lon" "$max_lat" "$max_lon" "$tiles_per_axis" "$query_template" "$tile_dir" "$overpass_url" "$retry_delay_seconds" <<'PY'
import json
import math
import pathlib
import subprocess
import sys
import time

min_lat = float(sys.argv[1])
min_lon = float(sys.argv[2])
max_lat = float(sys.argv[3])
max_lon = float(sys.argv[4])
tiles = int(sys.argv[5])
query_template = sys.argv[6]
tile_dir = pathlib.Path(sys.argv[7])
overpass_url = sys.argv[8]
retry_delay_seconds = float(sys.argv[9])

lat_step = (max_lat - min_lat) / tiles
lon_step = (max_lon - min_lon) / tiles

for y in range(tiles):
    tile_min_lat = min_lat + y * lat_step
    tile_max_lat = max_lat if y == tiles - 1 else min_lat + (y + 1) * lat_step
    for x in range(tiles):
        tile_min_lon = min_lon + x * lon_step
        tile_max_lon = max_lon if x == tiles - 1 else min_lon + (x + 1) * lon_step
        bbox = f"{tile_min_lat:.6f},{tile_min_lon:.6f},{tile_max_lat:.6f},{tile_max_lon:.6f}"
        query = query_template.replace("__BBOX__", bbox)
        output_path = tile_dir / f"tile_{y}_{x}.json"

        print(f"Fetching {output_path.name} bbox={bbox}", flush=True)
        attempts = 3
        for attempt in range(1, attempts + 1):
            result = subprocess.run(
                [
                    "curl",
                    "--fail",
                    "--silent",
                    "--show-error",
                    "-X",
                    "POST",
                    overpass_url,
                    "--data-urlencode",
                    f"data={query}",
                    "-o",
                    str(output_path),
                ],
                capture_output=True,
                text=True,
            )

            if result.returncode == 0:
                break

            if attempt == attempts:
                sys.stderr.write(result.stderr)
                sys.exit(result.returncode)

            wait_seconds = retry_delay_seconds * attempt
            print(f"Retrying {output_path.name} in {wait_seconds:.0f}s...", flush=True)
            time.sleep(wait_seconds)
PY

  python3 - "$tile_dir" "$output_file" <<'PY'
import json
import pathlib
import sys

tile_dir = pathlib.Path(sys.argv[1])
output_file = pathlib.Path(sys.argv[2])
merged = {"version": 0.6, "generator": "fetch_osm_city_json.sh", "elements": []}
seen = set()

for path in sorted(tile_dir.glob("tile_*.json")):
    with path.open("r", encoding="utf-8") as handle:
        payload = json.load(handle)
    for element in payload.get("elements", []):
        key = (element.get("type"), element.get("id"))
        if key in seen:
            continue
        seen.add(key)
        merged["elements"].append(element)

output_file.parent.mkdir(parents=True, exist_ok=True)
with output_file.open("w", encoding="utf-8") as handle:
    json.dump(merged, handle, ensure_ascii=True)

print(f"Merged {len(merged['elements'])} elements into {output_file}")
PY
}

roads_query_template=$(cat <<'EOF'
[out:json][timeout:120];
way[highway~"motorway|trunk|primary|secondary|tertiary|residential|unclassified"](__BBOX__);
out geom;
EOF
)

stops_query_template=$(cat <<'EOF'
[out:json][timeout:120];
(
  node[highway=bus_stop](__BBOX__);
  node[public_transport=platform](__BBOX__);
);
out body;
EOF
)

echo "Downloading roads to ${roads_file} using ${roads_tiles}x${roads_tiles} tiles"
fetch_tiled_query "roads" "${roads_tiles}" "${roads_query_template}" "${roads_file}"

echo "Downloading stops to ${stops_file} using ${stops_tiles}x${stops_tiles} tiles"
fetch_tiled_query "stops" "${stops_tiles}" "${stops_query_template}" "${stops_file}"

echo "Done."
echo "Roads JSON: ${roads_file}"
echo "Stops JSON: ${stops_file}"
