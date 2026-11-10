#!/usr/bin/env python3
"""Download every city's map data in one run.

Cities are discovered from the project's CityDefinition assets, so the list stays
in step with the game. Each city gets its own folder:

  Assets/StreamingAssets/Cities/<CODE>/roads.json      road network (game reads this)
  Assets/StreamingAssets/Cities/<CODE>/stops.json      bus stops
  Assets/StreamingAssets/Cities/<CODE>/buildings.json  building footprints
  Assets/StreamingAssets/Cities/<CODE>/poi.json        amenities/shops/tourism
  Assets/StreamingAssets/Cities/<CODE>/fuel.json       fuel + charging stations
  MapData/<CODE>/overture-buildings.geojson            optional Overture download

Import the Overture files with Tools > RealBus > OSM > Import Overture Buildings
(button "Import all files in MapData").

Examples:
  scripts/download_all_maps.py                     # every city, every OSM layer
  scripts/download_all_maps.py --only NBO,MEL --force
  scripts/download_all_maps.py --layers buildings,poi
  scripts/download_all_maps.py --overture          # also fetch Overture buildings
  scripts/download_all_maps.py --list
"""

from __future__ import annotations

import argparse
import json
import re
import shutil
import subprocess
import sys
import tempfile
import time
from dataclasses import dataclass
from pathlib import Path

PROJECT_ROOT = Path(__file__).resolve().parents[1]
CITY_ASSETS = PROJECT_ROOT / "Assets" / "_Game" / "ScriptableObjects" / "Cities"
CITIES_ROOT = PROJECT_ROOT / "Assets" / "StreamingAssets" / "Cities"
RAW_ROOT = PROJECT_ROOT / "MapData"

DEFAULT_ENDPOINTS = [
    "https://overpass-api.de/api/interpreter",
    "https://overpass.kumi.systems/api/interpreter",
    "https://lz4.overpass-api.de/api/interpreter",
]

OVERTURE_FILE = "overture-buildings.geojson"


@dataclass(frozen=True)
class Layer:
    file_name: str
    label: str
    tiles: int
    query: str


LAYERS = {
    "roads": Layer(
        "roads.json",
        "road network",
        3,
        '[out:json][timeout:__TIMEOUT__];'
        'way[highway~"motorway|trunk|primary|secondary|tertiary|residential|unclassified|service"]'
        "(__BBOX__);out geom;",
    ),
    "stops": Layer(
        "stops.json",
        "bus stops",
        1,
        "[out:json][timeout:__TIMEOUT__];"
        "(node[highway=bus_stop](__BBOX__);node[public_transport=stop_position](__BBOX__););"
        "out body;",
    ),
    "buildings": Layer(
        "buildings.json",
        "buildings",
        3,
        '[out:json][timeout:__TIMEOUT__];way["building"](__BBOX__);out geom;',
    ),
    "poi": Layer(
        "poi.json",
        "places of interest",
        2,
        "[out:json][timeout:__TIMEOUT__];"
        '(nwr["amenity"](__BBOX__);nwr["shop"](__BBOX__);'
        'nwr["tourism"](__BBOX__);nwr["historic"](__BBOX__););'
        "out geom;",
    ),
    "fuel": Layer(
        "fuel.json",
        "fuel and charging stations",
        1,
        "[out:json][timeout:__TIMEOUT__];"
        "(node[amenity=fuel](__BBOX__);node[amenity=charging_station](__BBOX__););"
        "out body;",
    ),
}
DEFAULT_LAYERS = "roads,stops,buildings,poi,fuel"


@dataclass
class City:
    code: str
    name: str
    min_lat: float
    min_lon: float
    max_lat: float
    max_lon: float

    @property
    def bbox(self) -> str:
        return f"{self.min_lat:.7f},{self.min_lon:.7f},{self.max_lat:.7f},{self.max_lon:.7f}"

    @property
    def overture_bbox(self) -> str:
        # Overture expects west,south,east,north (lon/lat), not Overpass order.
        return f"{self.min_lon:.6f},{self.min_lat:.6f},{self.max_lon:.6f},{self.max_lat:.6f}"


class DownloadError(RuntimeError):
    pass


def read_city_assets() -> list[City]:
    cities: list[City] = []
    seen: set[str] = set()
    for path in sorted(CITY_ASSETS.glob("*.asset")):
        text = path.read_text(encoding="utf-8", errors="ignore")
        fields: dict[str, str] = {}
        for key in ("cityCode", "cityName", "minLat", "minLon", "maxLat", "maxLon"):
            match = re.search(rf"^\s*{key}:\s*(\S+)\s*$", text, re.MULTILINE)
            if match:
                fields[key] = match.group(1)
        try:
            city = City(
                code=fields["cityCode"],
                name=fields.get("cityName", fields["cityCode"]),
                min_lat=float(fields["minLat"]),
                min_lon=float(fields["minLon"]),
                max_lat=float(fields["maxLat"]),
                max_lon=float(fields["maxLon"]),
            )
        except (KeyError, ValueError):
            print(f"skip {path.name}: no usable city code/bounds", file=sys.stderr)
            continue
        if city.min_lat >= city.max_lat or city.min_lon >= city.max_lon or city.code in seen:
            print(f"skip {path.name}: invalid or duplicate bounds", file=sys.stderr)
            continue
        seen.add(city.code)
        cities.append(city)
    cities.sort(key=lambda c: c.code)
    return cities


def split_tiles(city: City, tiles: int) -> list[str]:
    lat_step = (city.max_lat - city.min_lat) / tiles
    lon_step = (city.max_lon - city.min_lon) / tiles
    boxes = []
    for y in range(tiles):
        south = city.min_lat + y * lat_step
        north = city.max_lat if y == tiles - 1 else city.min_lat + (y + 1) * lat_step
        for x in range(tiles):
            west = city.min_lon + x * lon_step
            east = city.max_lon if x == tiles - 1 else city.min_lon + (x + 1) * lon_step
            boxes.append(f"{south:.6f},{west:.6f},{north:.6f},{east:.6f}")
    return boxes


def fetch_tile(query: str, destination: Path, endpoints: list[str], delay: float) -> None:
    last_error = ""
    for endpoint in endpoints:
        for attempt in (1, 2):
            result = subprocess.run(
                [
                    "curl", "--fail", "--silent", "--show-error", "--max-time", "300",
                    "-A", "RealBusMapDownloader/1.0",
                    "-X", "POST", endpoint,
                    "--data-urlencode", f"data={query}",
                    "-o", str(destination),
                ],
                capture_output=True,
                text=True,
            )
            if result.returncode == 0:
                time.sleep(delay)
                return
            last_error = (result.stderr or "").strip()
            time.sleep(delay * (attempt + 1))
    raise DownloadError(last_error or "all Overpass endpoints failed")


def merge_tiles(tiles: list[Path], destination: Path) -> int:
    merged = {"version": 0.6, "generator": "download_all_maps.py", "elements": []}
    seen: set[tuple] = set()
    for tile in sorted(tiles):
        payload = json.loads(tile.read_text(encoding="utf-8"))
        for element in payload.get("elements", []):
            key = (element.get("type"), element.get("id"))
            if key in seen:
                continue
            seen.add(key)
            merged["elements"].append(element)
    destination.parent.mkdir(parents=True, exist_ok=True)
    temporary = destination.with_suffix(destination.suffix + ".tmp")
    temporary.write_text(json.dumps(merged, ensure_ascii=True, separators=(",", ":")), encoding="utf-8")
    temporary.replace(destination)
    return len(merged["elements"])


def download_layer(city: City, layer: Layer, tiles: int, endpoints: list[str],
                   timeout: int, delay: float, keep_tiles: bool, force: bool) -> str:
    city_dir = CITIES_ROOT / city.code
    city_dir.mkdir(parents=True, exist_ok=True)
    output = city_dir / layer.file_name
    if output.exists() and output.stat().st_size > 32 and not force:
        return f"{city.code} {layer.label}: already present ({output.stat().st_size // 1024} KB)"

    query = layer.query.replace("__TIMEOUT__", str(timeout))
    boxes = split_tiles(city, max(1, tiles))
    work_dir = Path(tempfile.mkdtemp(prefix=f"realbus-{city.code}-{layer.file_name}-"))
    try:
        for index, box in enumerate(boxes):
            print(f"  {city.code} {layer.label}: tile {index + 1}/{len(boxes)}", flush=True)
            fetch_tile(query.replace("__BBOX__", box), work_dir / f"tile_{index:02d}.json", endpoints, delay)
        count = merge_tiles(sorted(work_dir.glob("tile_*.json")), output)
        if keep_tiles:
            raw_dir = RAW_ROOT / city.code / "tiles" / layer.file_name.replace(".json", "")
            raw_dir.mkdir(parents=True, exist_ok=True)
            for tile in width_sorted(work_dir):
                shutil.copy2(tile, raw_dir / tile.name)
        return f"{city.code} {layer.label}: {count} elements ({output.stat().st_size // 1024} KB)"
    finally:
        shutil.rmtree(work_dir, ignore_errors=True)


def width_sorted(directory: Path) -> list[Path]:
    return sorted(directory.glob("tile_*.json"))


def download_overture(city: City, cli: str, force: bool) -> str:
    raw_dir = RAW_ROOT / city.code
    raw_dir.mkdir(parents=True, exist_ok=True)
    output = raw_dir / OVERTURE_FILE
    if output.exists() and output.stat().st_size > 32 and not force:
        return f"{city.code} overture: already present ({output.stat().st_size // 1024} KB)"
    print(f"  {city.code} overture buildings…", flush=True)
    result = subprocess.run(
        [cli, "download", f"--bbox={city.overture_bbox}", "-f", "geojson",
         "--type=building", "-o", str(output)],
        capture_output=True,
        text=True,
    )
    if result.returncode != 0:
        raise DownloadError((result.stderr or result.stdout or "overturemaps failed").strip())
    return f"{city.code} overture: {output.stat().st_size // 1024} KB → {output.relative_to(PROJECT_ROOT)}"


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(
        description="Download OSM (and optional Overture) map data for every city into per-city folders.",
        formatter_class=argparse.RawDescriptionHelpFormatter,
        epilog=__doc__,
    )
    parser.add_argument("--only", help="comma-separated city codes (default: every city)")
    parser.add_argument("--layers", default=DEFAULT_LAYERS, help=f"layers to download (default: {DEFAULT_LAYERS})")
    parser.add_argument("--tiles", type=int, help="override the per-axis tile count for every layer")
    parser.add_argument("--force", action="store_true", help="re-download files that already exist")
    parser.add_argument("--overture", action="store_true", help="also download Overture building GeoJSON")
    parser.add_argument("--cli", default="overturemaps", help="path to the overturemaps executable")
    parser.add_argument("--delay", type=float, default=1.0, help="seconds to wait between Overpass requests (default 1)")
    parser.add_argument("--timeout", type=int, default=180, help="Overpass query timeout in seconds (default 180)")
    parser.add_argument("--keep-tiles", action="store_true", help="keep raw tile responses under MapData/<CODE>/tiles")
    parser.add_argument("--endpoints", help="comma-separated Overpass endpoints, tried in order")
    parser.add_argument("--list", action="store_true", help="print the discovered cities and exit")
    return parser.parse_args()


def main() -> int:
    args = parse_args()
    cities = read_city_assets()
    if not cities:
        print(f"No CityDefinition assets with bounds found in {CITY_ASSETS}", file=sys.stderr)
        return 1

    if args.list:
        for city in cities:
            print(f"{city.code}  {city.name}  bbox={city.bbox}")
        return 0

    if args.only:
        wanted = {code.strip().upper() for code in args.only.split(",") if code.strip()}
        cities = [city for city in cities if city.code.upper() in wanted]
        missing = wanted - {city.code.upper() for city in cities}
        if missing:
            print(f"unknown city code(s): {', '.join(sorted(missing))}", file=sys.stderr)
            return 1

    layer_names = [name.strip() for name in args.layers.split(",") if name.strip()]
    unknown = [name for name in layer_names if name not in LAYERS]
    if unknown:
        print(f"unknown layer(s): {', '.join(unknown)}. Available: {', '.join(LAYERS)}", file=sys.stderr)
        return 1

    endpoints = [url.strip() for url in (args.endpoints or "").split(",") if url.strip()] or DEFAULT_ENDPOINTS

    if args.overture and not (Path(args.cli).exists() or shutil.which(args.cli)):
        print(f"'{args.cli}' was not found. Install it with: python3 -m venv .venv && .venv/bin/pip install overturemaps",
              file=sys.stderr)
        return 1

    print(f"Downloading {len(layer_names)} layer(s) for {len(cities)} cit{'y' if len(cities) == 1 else 'ies'}"
          f"{' plus Overture buildings' if args.overture else ''}.")
    print(f"OSM output: {CITIES_ROOT.relative_to(PROJECT_ROOT)}/<CODE>/")
    if args.overture:
        print(f"Overture output: {RAW_ROOT.relative_to(PROJECT_ROOT)}/<CODE>/{OVERTURE_FILE}")
    print()

    failures: list[str] = []
    produced = 0
    for position, city in enumerate(cities, start=1):
        print(f"[{position}/{len(cities)}] {city.code} {city.name}", flush=True)
        (RAW_ROOT / city.code).mkdir(parents=True, exist_ok=True)
        (CITIES_ROOT / city.code).mkdir(parents=True, exist_ok=True)
        for name in layer_names:
            layer = LAYERS[name]
            try:
                print("  " + download_layer(
                    city, layer, args.tiles or layer.tiles, endpoints, args.timeout,
                    args.delay, args.keep_tiles, args.force), flush=True)
                produced += 1
            except (DownloadError, json.JSONDecodeError, OSError) as error:
                failures.append(f"{city.code} {name}: {error}")
                print(f"  {city.code} {name}: FAILED — {error}", file=sys.stderr, flush=True)
        if args.overture:
            try:
                print("  " + download_overture(city, args.cli, args.force), flush=True)
                produced += 1
            except (DownloadError, OSError) as error:
                failures.append(f"{city.code} overture: {error}")
                print(f"  {city.code} overture: FAILED — {error}", file=sys.stderr, flush=True)

    print(f"\n{produced} file(s) ready for {len(cities)} cities.")
    if failures:
        print(f"{len(failures)} failed:", file=sys.stderr)
        for failure in failures:
            print("  " + failure, file=sys.stderr)
        return 1
    return 0


if __name__ == "__main__":
    sys.exit(main())
