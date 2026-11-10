# Building coverage

No scene rebuild is needed. Restart Play Mode after importing data.

## Player downloads and traffic lanes

In **Settings > Downloads**, choose a city and mark it **Favourite**. Players can
download only an **Offline Map**, **World Data**, **All Routes**, or one bus route
by its numeric OpenStreetMap relation ID. Downloads are stored in the app's private
`DownloadedCities` folder, so they do not increase the base install size. **Remove
City** removes its downloaded OSM files and the map tiles within its saved area.

Offline maps use the same Mapbox token, style, and zoom as gameplay. Map downloads
need a valid token with style access; they do not include 3D buildings or routes.
World data includes buildings/POIs/fuel; route data includes roads, stops, and bus
relations. The one-route action intentionally still downloads the local road network
and stops needed to drive that route. Content is checked before replacing the
built-in equivalent and partial downloads are retained to resume later.

Road meshes and AI traffic now use OSM `lanes`, `width`, `oneway`, and `maxspeed`
metadata. Two-way traffic stays on the country-correct half of the road; one-way
traffic uses all tagged lanes. Missing lane data uses conservative road-type defaults.
This is road-level lane fidelity, not lane-connection data: OSM does not reliably
provide turn arrows, every intersection lane assignment, or traffic restrictions in
the current download query.

## Live-city models

Use **Tools > RealBus > Setup > Build Live City Models** after adding or replacing
models. The initial catalog is generated automatically when missing. Source FBXs,
bus prefabs, mission stops, roads, and existing traffic are not replaced.

The importer prepares trees, plants, rocks, billboards/signs, the bench, and the
available pedestrian model. It normalizes sizes, preserves supplied base textures
and normal maps, and supplies simple category colours where scenery has no colour
or texture. Missing textures are not reconstructed. Scenery over 65,000 triangles
or 22 m wide is excluded from repeated placement; check `[LiveCity]` Console logs.
The existing 84,272-triangle character is allowed under a separate 100,000-triangle
limit, with only two ambient instances within 75 m of the bus.

`LiveCityStreamer` is automatically attached by the gameplay bootstrap when its
catalog exists. It replaces the old whole-city scenery scatter, not the building
or traffic systems. Defaults: 180 m placement radius, 250 m unload radius, 64 props,
360,000 prop triangles, and two ambient pedestrians. One instance is created per
frame. These limits cover this layer only; profile the complete game on phones.

Placement uses actual road widths and building footprints, including procedural
infill, to leave roads and existing colliders clear. Vegetation sways gently;
pedestrians move along short, checked roadside paths. This is ambient activity,
not a crowd/navigation or character-animation system. Supplied animation
controllers are retained; unanimated models still need authored walk cycles.
The map is currently flat and does not classify water or protected land.

Generated assets live in `Assets/_Game/Resources/LiveCityGenerated`, with references
in `LiveCityCatalog.asset`. Nearby **instances** stream in and out; referenced
meshes remain available in memory. This is not an asset-download/Addressables layer.

## Existing maps and procedural fill

The game uses Mapbox **raster tiles** for ground imagery and separate footprint
meshes for buildings. Mapbox Standard and Standard Satellite are not supported by
the [Static Tiles API](https://docs.mapbox.com/api/maps/static-tiles/); switching the
style does not supply Unity building, vegetation, or terrain meshes. Keep the
existing compatible style.

`OSMBuildingMeshBuilder.enableProceduralFill` adds fictional buildings near
residential, living-street, tertiary and secondary roads where mapped footprints
are absent. It requires loaded roads, not a building download. Existing mapped
footprints, road surfaces and nearby colliders exclude candidates.

The default cap is 160 synthetic buildings, generated at most one per frame.
100 m cells stream around the bus (cell centres within 280 m; unload beyond
520 m including the cell margin). Real buildings retain their separate streaming
budget. Disable the option for strictly real-world coverage. This is visual infill,
not a land-use simulation: unmapped water, parks and undeveloped lots are not
identified, and motorway/trunk roads are deliberately excluded.

Mapped and procedural building footprints are checked against the full rendered
road width. Conflicting buildings are omitted at runtime, not removed from the
downloaded data. The Console reports the omitted count. This favours playable
roads where imported map sources or estimated road widths disagree.

Stop shelters use the local road direction and country-side kerb. Their model and
collider bounds are moved beyond the asphalt; the docking target remains inside
the kerb. Junction openings use road geometry rather than shared node IDs.
Imported road tiles are decorative only, and generated mesh takes over at bends
and crossings to avoid stacked rectangular tiles and blocking prefab colliders.
The current map remains flat: this is not a grade-separated bridge/tunnel system.

## Bulk map download

`scripts/download_all_maps.py` downloads every city's OSM layers in one run. It
reads the city list and bounding boxes from the `CityDefinition` assets, so no
city list is duplicated in the script, and writes each city's files into its own
folder under `Assets/StreamingAssets/Cities/<CODE>/` (`roads.json`, `stops.json`,
`buildings.json`, `poi.json`, `fuel.json`). Files that already exist are skipped,
so an interrupted run resumes where it stopped.

```sh
scripts/download_all_maps.py --list                 # what would be downloaded
scripts/download_all_maps.py                        # all cities, all OSM layers
scripts/download_all_maps.py --only NBO,MEL --force # selected cities, fresh copy
scripts/download_all_maps.py --layers buildings,poi  # one or two layers only
scripts/download_all_maps.py --overture              # also fetch Overture buildings
```

Large layers are fetched in tiles and merged, retried on the configured Overpass
mirrors, and written atomically; a failed city does not stop the run, and the
exit status is non-zero if anything failed. `--tiles` overrides the tile count,
`--delay` spaces requests, and `--keep-tiles` keeps the raw responses under
`MapData/<CODE>/tiles/`. `--overture` requires the `overturemaps` CLI (see below)
and only downloads; convert those files in Unity. `MapData/` is git-ignored
because the exports are large.

## Optional Overture buildings

1. Open **Tools > RealBus > OSM > Import Overture Buildings**.
2. Assign the intended `CityDefinition` asset. Check its bounding box covers the
   playable routes, not just a tiny downtown area.
3. Install the official CLI in a virtual environment, following the
   [Overture instructions](https://docs.overturemaps.org/getting-data/overturemaps-py/):

   ```sh
   python3 -m venv /tmp/realbus-overture
   /tmp/realbus-overture/bin/pip install overturemaps
   ```

4. In the window, set **Overture CLI** to the venv binary
   (`/tmp/realbus-overture/bin/overturemaps`).
5. Click **Download + import ALL cities** to run the CLI once per
   `CityDefinition` and convert each result, or **Import all files in MapData** to
   convert exports you downloaded yourself. Raw downloads are written to
   `MapData/<CODE>/overture-buildings.geojson`, one folder per city, and reused on
the next run unless **Redownload existing files** is enabled; converted packs go
   to `Assets/StreamingAssets/Cities/<CODE>/overture-buildings.json`. The batch
   runs off the main thread with progress, a per-city log and a cancel button, so
   the editor stays responsive. Restart Play Mode afterwards and look for
   `Buildings: Using Overture pack` in the Console.

For a single city, **Copy city download command** writes the same command to the
clipboard, and **Import downloaded GeoJSON…** converts one file (its file picker
opens the city's `MapData` folder).

The importer accepts GeoJSON FeatureCollections with Polygon/MultiPolygon geometry,
preserves supplied heights and per-building source metadata, and filters to the
selected city's bounds. It skips malformed, degenerate, oversized, duplicate and
holed polygons (the current mesher cannot preserve courtyards). File size is capped
at 128 MB; inspect the reported imported/skipped counts.

The result is `Assets/StreamingAssets/Cities/<code>/overture-buildings.json`.
Original `buildings.xml/json` files are retained. Overture is an **alternate whole
building layer**, not an overlay or automatic merge with OSM. A small export will
therefore leave other areas without real footprints. Download the complete needed
city area before enabling it. Remove the optional pack to return to OSM.

Player-downloaded packs in `Application.persistentDataPath/DownloadedCities/<code>`
override bundled packs of the same name. Empty optional packs fall back to OSM.
Re-importing asks before replacement and keeps the previous pack as `.bak`.

No Overture download runs automatically, and no real Overture dataset is bundled
by this code change. Imported packs are extruded footprints, not textured landmark
models. Keep provider notices and review
[Overture attribution requirements](https://docs.overturemaps.org/attribution/)
for the sources present in your export before distributing it.

## Driving and spawn checks

Country assets have an Automatic/Left/Right driving-side override. Kenya, Uganda
and Tanzania default to left; Rwanda defaults to right. Spawn offsets are relative
to the bus heading and respect one-way direction, road width and obstacle clearance.
If no clear spawn is found, the Console warns and keeps the requested position.

At rest, the up/Drive button selects first forward gear instead of repeatedly
shifting into a high gear. Hold ACCEL to accelerate; release the parking brake and
finish boarding when the service interlock is active. Start through country, city
and route selection to test the intended country and route.
