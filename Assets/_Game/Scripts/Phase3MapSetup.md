# Phase 3: real map integration

Implements and repairs the code for RealBus_MasterTimeline.docx sections 3.1–3.8. The Phase 3 milestone is not certified: Unity compilation, physical-device driving, airplane mode, and streaming at 80 km/h still require execution in Unity.

## Run the integration scene

1. Open `Assets/_Game/Scenes/MapTest.unity` on its own, then press Play. The fixture builds the Phase 2 bus and generates real road meshes with MeshColliders from the included Nairobi OSM files. It adds no buildings or mission systems. The bus stays kinematic while roads build, then spawns on a road with the parking brake engaged. Press P to release.
2. Use W/S/A/D or the touch controls; the existing button, steering-wheel, and tilt schemes remain available. Stop markers use GPS coordinates. All geometry appears at runtime.
3. Optional Mapbox background: set `REALBUS_MAPBOX_TOKEN` in the environment used to launch Unity, or enter the token in the running test scene and click **Enable map tiles**. The token is kept in memory; it is not written to an asset. Default style is `mapbox/streets-v12`, zoom 16. The project already includes Mapbox SDK sources; this scene uses the project's existing raster streaming adapter and the CoordinateConverter abstraction. It does not instantiate an AbstractMap alongside the raster adapter.
4. Click **Download 2 km area**, wait for all tiles, then select **Offline test mode** and enable tiles again to reload from cache. Test airplane mode on an actual device separately. Cache limit is 500 MiB with LRU eviction; previously downloaded areas can be evicted. A completed request is not sufficient: download completion checks whether files remain cached.
5. **Import real OSM routes** runs the runtime importer. It keeps only road-connected routes and, by default, applies the timeline's 6–40 stops / 3–25 km filter. Missing paths cause rejection, never straight-line substitutes.

Without a token, the bundled OSM roads, stops, and route previews can load offline; the Mapbox imagery cannot appear until downloaded. Background tiles have no colliders in MapTest: the separate OSM road meshes support the bus. Roads are flattened to one elevation; this fixture does not reconstruct bridges or terrain grades.

## Included data and route workflow

The existing Nairobi pack has 870 road ways and 39 stop nodes. `Cities/NBO/routes.json` adds a public Overpass snapshot of three actual Matatu relations, retrieved 2026-09-10 (OSM base timestamp 14:37:51 UTC):

| Route | OSM relation | Stops in snapshot | Status |
| --- | --- | --- | --- |
| 1 | 3225698 | 4 | OSM preview; below minimum stop count; disconnected directed path |
| 2 | 3225904 | 6 | OSM preview; disconnected directed path |
| 3 | 3225965 | 5 | OSM preview; below minimum stop count; disconnected directed path |

These historical OSM relation names/members are imported data, not a claim about current operating schedules. All three parse and retain their multi-way geometry and stop locations. Their member ordering/coverage is insufficient for validated end-to-end routes. Preview polylines may contain discontinuities and their raw length is not a trusted driving distance. MapTest labels these previews and excludes unvalidated routes from the runtime city's availableRoutes. Included member road geometry supplements the central road pack so outer stops can be inspected.

Use **Tools → RealBus → Phase 3 → Import included Nairobi OSM previews** to save those three relations as BusRoute assets under `Assets/_Game/Routes/OSMPreviews`. Preview assets preserve relation IDs and both stop representations; they are explicitly unvalidated.

Use **Tools → RealBus → OSM → Auto-Generate Bus Routes (Overpass)** for corridor-generated drafts from road/stop data. The existing editor supports clustering, filtering, split/merge, and return variants. In Route Editor, assign **Roads JSON for validation** and a CoordinateConverter, then click **Rebuild road-following path and validate** after moving, adding, deleting, reversing or reordering stops. This regenerates directed paths, GPS geometry, metrics, headings, and legacy stop data. Failed validation preserves the existing asset. For a larger route, supply the road network covering the entire route, not only a CBD bounding box. Draft generation and preview import do not certify real-world service routes.

Runtime OSMRouteImporter supports optional `localRoadsJson` and `localRoutesJson` TextAssets for reproducible offline imports. Return routes deep-copy stops and rerun directed pathfinding. Repeated imports replace matching route IDs in city registration instead of appending duplicates.

## Integration changes

- Tile coverage uses latitude and zoom rather than the old 200 m approximation. It wraps longitude, clamps polar indices, prioritizes nearest tiles, and includes both the 2 km bus-centered disk and forward lookahead. Unload radius remains 3 km. Failed downloads retry after 15 seconds.
- Initial LOD mesh work is retried if worker capacity was full; unloaded/recreated tile results use distinct build versions. Downloaded textures transfer ownership without leaking a second full-resolution source texture. Cache images remain readable for LOD generation. Collider ownership is configurable independently from visual LOD.
- Cache corruption falls through to re-download, HTTP errors do not globally declare the device offline, LRU jobs are serialized, and predownload cancellation drains workers before a new download can start. Predownload textures are destroyed and byte estimates use cached file size. OfflineMapsUI reports failed tiles/cancellation rather than presenting every 100% response count as offline-ready.
- Queries format decimals invariantly and union stop/fuel selections. Relation resolution joins ways and node tags. Route parsing includes every member way, honors reverse member direction, and recognizes platform roles.
- RoadGraph handles `oneway=-1`, roundabouts, incoming-only snap segments, OSM node identity at grade-separated crossings, and speeds above the default heuristic ceiling. Directed path construction rejects unreachable legs and updates metrics after routing.
- RoadSurfaceType carries OSM surface metadata without requiring new project tags. The wheel surface detector recognizes asphalt, cobblestone/sett, and dirt/gravel. Wet-road grip remains the existing 0.7 multiplier.
- Phase3City1 is Nairobi. Phase3City2–5 are explicitly disabled placeholders, with no additional city downloads. City selection clears stale streamed tiles when moving the origin. WGS84 validation accepts (0,0) and rejects NaN/out-of-range coordinates.

## Validation performed here

- C# 9 syntax parsing: all 144 game scripts, zero syntax errors (including editor code).
- Executed the actual SlippyTileCoverage source: latitude, zoom, dateline, poles, nearest-first sorting, and duplicate-key checks passed.
- Compiled and exercised the actual MiniJSON / OverpassResponse / BusRouteParser / RoadGraph / RoadRoutePathBuilder source in an isolated .NET harness using minimal test-only Unity math replacements. Three real route relations parsed; all had multi-way geometry. Reverse one-way and incoming-segment snapping checks passed. Full-route connectivity rejection was confirmed for all three snapshots.
- Added Unity EditMode regression tests in Phase3MapTests.cs. Existing coordinate round-trip tests remain available. These Unity tests have not executed here because no Unity editor/assemblies are installed. The isolated math harness is not a substitute for Unity compilation or physics tests.
- Scene/profile GUID checks and `git diff --check` passed.

Remaining acceptance work: compile in Unity 6.3, run EditMode tests, inspect generated road/collider alignment and the three previews, repair/import at least three valid gameplay routes, profile tile transitions at 80 km/h, verify cached-area airplane-mode driving, and profile on S20/A54. Phase 2 physics calibration remains necessary independently of the map integration.

References: [Mapbox Static Tiles API](https://docs.mapbox.com/api/maps/static-tiles/), [Mapbox attribution](https://docs.mapbox.com/help/dive-deeper/attribution/), [OSM one-way semantics](https://wiki.openstreetmap.org/wiki/Key:oneway), [OSM contributors and license](https://www.openstreetmap.org/copyright).
