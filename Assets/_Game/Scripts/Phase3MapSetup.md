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

## Depot yards

A depot is placed beside the route's first stop (`BusDepotManager.TrySpawnDepotAndResolveBay`), and it now owns the ground it stands on:

- **Yard zone.** Placement claims a rotated rectangle over the apron (`RegisterZone`, apron + 2 m margin); `ReleaseCurrentDepot` withdraws it. Mapped OSM buildings (`OSMBuildingMeshBuilder.TryProcessBuildingWay`), fictional infill (`ProceduralBuildingFill.Clear`) and synthetic buildings all refuse that ground, and whatever already stands in it is evicted when the yard is placed (`ClearDepotOverlaps`, which also drops the records and buckets so streaming cannot rebuild them).
- **Yard roads.** The yard lays its own asphalt through the shared road builder (`BuildRoadSegmentMesh`, `BuildLaneMarkings`): a one-way entry aisle, a U-turn and a one-way exit aisle, 5 m wide, with MeshColliders and the road friction material, all registered in `RoadSurfaceIndex` under one synthetic negative way id so building infill, street furniture and spawn clearing treat them as road. `RoadSurfaceIndex.Remove(wayId)` withdraws them on release. Both driveways cross the yard edge and stop 1 m past the kerb, so they meet the street as junctions; without a road builder the same two driveways are laid as plain tarmac slabs.
- **Layout.** The bay row sits behind the driving band, entry and exit aisles never overlap, the departure gate is the exit driveway meeting the road, floodlights are nudged (or dropped) until they clear every aisle, the dispatch sign hangs above the bays, and imported depot buildings are kept only inside a 20 m per-axis budget — the shipped `Parking+Garage` prop measures 200 m (525 m as placed) and `parking` a 52 m lot, so the yard refuses both and builds its own workshop. Before that rule the imported garage contributed a ~300 m invisible BoxCollider and a 200 m slab over the depot, the road and the surrounding block.

- **Props are judged by the lanes, not only by their size.** `PropStandsInYardLane` measures a placed prop's plan-view footprint from its renderers and colliders and refuses it against the real lane corridors (pure `FootprintOverlapsLanes`: corner distance, lane-endpoint-in-footprint, segment crossing). Imported structures, the procedural workshop, floodlights and parked fleet buses all go through it; `PropsStandingInYardLanes()` returns the names of any offender, so "nothing stands in the lanes" is a one-call assertion. The 20 m budget stays as well, because a prop can fit any budget and still swallow the block.
- **Departure.** A route start puts the bus in Bay 1 with the parking brake set, and throttle is gated on that brake, so the bus could not be driven out until the player found the park-brake control. `BusController` now releases the brake when the driver applies throttle from a standstill (`ShouldReleaseParkingBrake`; never while the foot brake, service interlock, empty tank or motion applies), and the dispatch subtitle points at the exit lane instead of a key.
- **Yard traffic.** `DepotTrafficSpawner`, created as a child of the depot, builds an 8-node private `AIRoadGraph` from the yard's own lane points - street approach, entry driveway, entry aisle, U-turn, exit aisle, exit driveway, street departure - and drives spline vehicles along it. It is one-directional: a vehicle that reaches the far end waits out of sight and re-enters from the top of the street, since the depot knows only one street lane at its kerb. `privateLaneCircuit` keeps `VehiclePool` and `SceneBootstrap` from binding the yard loop as the city graph. Street traffic is held whenever the player's bus is inside the yard, so it can never block the way out.

Verified by `RealismSystemsTests.BusDepotManager_*` (zone rejection and withdrawal, entry/exit separation and index registration, bay/road separation, pole clearance, building eviction, lane-footprint refusal, yard traffic and its hold gate), `BusController_ThrottleReleasesTheParkingBrakeSoADepotBusCanDriveOut`, and the headless `GameScene` smoke run (`Constructed depot … Roads: 3`, `Refused imported depot prop …`, no depot entry in the bus collider overlap probe, and the depot's traffic/`propsInLanes` report).

## Validation performed here

- C# 9 syntax parsing: all 144 game scripts, zero syntax errors (including editor code).
- Executed the actual SlippyTileCoverage source: latitude, zoom, dateline, poles, nearest-first sorting, and duplicate-key checks passed.
- Compiled and exercised the actual MiniJSON / OverpassResponse / BusRouteParser / RoadGraph / RoadRoutePathBuilder source in an isolated .NET harness using minimal test-only Unity math replacements. Three real route relations parsed; all had multi-way geometry. Reverse one-way and incoming-segment snapping checks passed. Full-route connectivity rejection was confirmed for all three snapshots.
- Added Unity EditMode regression tests in Phase3MapTests.cs. Existing coordinate round-trip tests remain available. These Unity tests have not executed here because no Unity editor/assemblies are installed. The isolated math harness is not a substitute for Unity compilation or physics tests.
- Scene/profile GUID checks and `git diff --check` passed.

Remaining acceptance work: compile in Unity 6.3, run EditMode tests, inspect generated road/collider alignment and the three previews, repair/import at least three valid gameplay routes, profile tile transitions at 80 km/h, verify cached-area airplane-mode driving, and profile on S20/A54. Phase 2 physics calibration remains necessary independently of the map integration.

References: [Mapbox Static Tiles API](https://docs.mapbox.com/api/maps/static-tiles/), [Mapbox attribution](https://docs.mapbox.com/help/dive-deeper/attribution/), [OSM one-way semantics](https://wiki.openstreetmap.org/wiki/Key:oneway), [OSM contributors and license](https://www.openstreetmap.org/copyright).
