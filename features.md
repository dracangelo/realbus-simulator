# RealBus Simulator Features

## Bus simulation

- Realistic acceleration, braking, steering, gears, retarder, and parking brake.
- Diesel fuel and electric battery consumption systems.
- Passenger weight, aerodynamics, brake fade, tyre wear, and maintenance.
- Wet, paved, cobblestone, gravel, and dirt surface handling.
- Cinematic mobile driving controls with pedals, steering buttons or wheel, gear, horn, and retarder.
- Model-backed playable buses with generated physics, wheels, cameras, and docking points.
- Imported camera isolation and preservation of original model materials.
- Fallback body paint and transparent glass for untextured bus imports.
- Optional speed and stop-approach driving assists.
- Single-viewport cockpit and horizon-stabilized chase cameras.
- Speed-sensitive steering and chassis-mass suspension damping.

## Maps and routes

- OpenStreetMap road, building, stop, signal, and route importing.
- Imported routes automatically link to their geographic city with repaired metrics and duplicate filtering.
- Road-following route generation with one-way-road support.
- Junction-aware kerbs, clipped road overlays, and continuous bend joins.
- Mapbox tile streaming with distance-based detail levels.
- Resilient Mapbox PNG/JPEG decoding and accurate authorization diagnostics.
- Offline city downloads, cache limits, and LRU cleanup.
- Player favourites plus selective offline-map, world-data, all-route, or one-route downloads.
- OSM lane-aware road dividers and AI traffic on the correct side of each carriageway.
- In-game optional downloads for city world data, all routes, or one OSM route.
- GPS tracking, route previews, and runtime navigation lines.
- Cinematic country, city, and route selectors with swipeable cards.

## Gameplay

- Free-drive and scheduled mission modes.
- Route-direction, country-side lane spawning with barrier clearance checks.
- Guided, accuracy-graded docking against the real bus door and physical stop prefab.
- Road-width-aware stop placement with off-road shelters and reachable kerb anchors.
- Door controls, kneeling, boarding, and alighting.
- Timetables with early, on-time, late, and severely late arrivals.
- Punctuality, satisfaction, safety, and efficiency scoring.
- Speeding, collision, harsh-driving, and red-light penalties.
- Mission briefings, countdowns, results, earnings, XP, and star ratings.
- Daily real-route challenges with local conditions and completion streaks.
- Route mastery medals for smoothness, punctuality, safety, and efficiency.
- Local best-run route recording and driver ghosts.
- Photo mode with screenshot capture.

## Passengers and traffic

- Rush-hour passenger demand and destination-based journeys.
- Capacity limits, wheelchair passengers, and reusable passenger visuals.
- Pooled cars, trucks, buses, and vans built from the downloaded vehicle models, normalised, faced, weighted by cost at import, and textured from the pack's own assets (materials are saved as real assets, so no prefab ships with an empty slot).
- Traffic that keeps its own lane on the correct side of the road, including shared single-lane streets, with lane-following steering instead of corner cutting.
- Traffic that does not U-turn at ordinary two-way junctions (only at dead ends), and spawns that stay clear of the player and other vehicles.
- Traffic volume that follows the daily demand curve, with a floor so the road is never empty, a tier split by distance, and a pool that re-homes dormant vehicles around the bus as it drives.
- Full-physics, spline, billboard, and distance-culling traffic tiers.
- Traffic lights, pedestrian crossings, passing, and emergency sirens.

## World realism

- Clear, rain, fog, snow, overcast, and night conditions.
- Gradual weather transitions, wet-road grip, wipers, and lighting changes.
- A generated sky: weather-driven cloud layer, star field and moon, with a procedural skybox that follows the clock.
- Rain and snow that follow the bus, built at run time when the scene authors none.
- Fog that is lit by the sky: daylight haze, sunrise and sunset warmth, and near-black fog at night.
- Time-of-day simulation with street, cabin, and headlight control, and a sun that follows a continuous arc through dawn and dusk.
- Vehicle, road, map-tile, and traffic level-of-detail systems.
- Runtime stress testing with CSV performance reports.
- Real gas-station models spawn at OpenStreetMap fuel locations.
- Varied-height OSM buildings with façade and roof materials.
- Optional city-scoped Overture building import (Tools > RealBus > OSM).
- Streamed fictional infill that hugs the kerb and fills each block row by row until the next street, depot, or mapped footprint, with per-frame budgets and half-built cells finished on the next pass.
- Downloaded building packs imported as bottom-centred, textured prefabs and mixed into the infill, fitted to each lot and capped by instance and triangle budgets; mapped OSM buildings are never replaced.
- Road-clearance filtering for mapped and procedural building footprints.
- Vehicle-proximity building streaming and physical road-edge boundaries with junction openings.
- Imported road-surface models with automatic procedural-road fallback.
- Nearby imported trees, plants, rocks, billboards, benches, and ambient pedestrians, planted both along the kerb and across open ground between blocks.
- Road/building-safe scenery streaming with prop and triangle budgets, and planting that favours the cheap models so the packs' few dense trees do not spend the whole budget.
- Adaptive 30/45/60 FPS targets, resolution scaling, and thermal response.

## Progression and customization

- Ten driver ranks with XP thresholds and animated rank-up reveals.
- Rank-based city, route, bus, livery, and upgrade unlocks.
- Five bus types with distinct capacity, physics, and energy profiles.
- Grand Tour routes unlocked by completing every city route.
- Garage fleet comparison and bus selection.
- Downloadable platform-specific vehicle packs stored outside the base game.
- Five affordable per-bus upgrades with clear benefits and trade-offs.
- Six-zone livery editor with HSV color and opacity controls.
- Eight preset liveries and shareable 12-character livery codes.

## Saving and interface

- Local PlayerPrefs saves with optional PlayFab cloud synchronization.
- Mission history, XP, unlocks, upgrades, liveries, economy, and vehicle-state persistence.
- Automatic saves after missions, rank-ups, purchases, customization, and app backgrounding.
- Local/cloud conflict detection with player choice.
- Main menu, garage, settings, credits, pause, resilient loading, and mobile UI.
- Rounded cinematic HUD with navigation, next stop, speed dial, fuel, passengers, gear, and schedule.
- North-up route minimap with stop markers and a visibility toggle.
- First-drive tutorial, missed-stop recovery, and turn guidance.
- Exterior, cockpit, chase, and kerb cameras with a digital mirror.

## Audio, visuals, and accessibility

- RPM/load engine layers, tyre, brake, cabin, city, horn, UI, and mission audio hooks.
- Spatial audio with production clip and FMOD bridge slots.
- Glass reflections, cosmetic scuffs, exhaust, dust, and rain-spray effects.
- Quality-aware visuals with motion blur disabled on Low.
- Scalable text, status icons, high contrast, haptics, and subtitles.
- Passenger mood cues and context-sensitive reactions.
- Recurring passenger stories that evolve with trust and driver reputation.
- Resumable offline downloads with cache, size, and storage details.
- Periodic, suspend, and low-memory saves with mission checkpoints.

## Development tools

- Physics, map, gameplay, realism, and progression test scenes.
- Unity EditMode regression tests and coverage reporting.
- Unity 6.6-compatible object searches and explicit Mapbox serialization.
- Route, city, traffic, and OSM import editor tools.
- A vehicle-facing checker that reads the downloaded FBXs straight from the shell, so traffic orientation can be checked without opening the editor.
- Platform-specific downloadable vehicle-pack builder.
- Runtime validation, logging, profiling, and diagnostic overlays.

## Remaining polish priorities

- Import final voices and animation controllers into the passenger reaction library.
- Populate the production audio profile with licensed clips and mixer groups.
- Bind final bus materials and models to cockpit and localized-damage hooks.
- Gather playtest telemetry, then rebalance mission income and upgrades.

## Release validation remaining

- Playtest tutorials, assists, touch layouts, and accessibility on varied screens.
- Profile adaptive scaling, mirrors, and thermal behavior on target phones.
- Validate docking grades across every bus length.
- Stress-test rerouting, traffic recovery, and offline downloads.
- Test suspend, process death, cloud conflicts, and low-memory handling.

## Nice to have

- Online city league standings and ghost exchange.
- Regional bus-culture packs with authentic vehicles, voices, stops, and operations.
- A dispatcher mode for assigning buses, drivers, fares, and timetables.
- Community route and livery sharing with reporting and moderation.
- Convoy co-op after the single-player experience and networking are stable.
- Dynamic roadworks, diversions, breakdowns, events, and service disruptions.
- Shareable replay clips, photo challenges, and cinematic route recaps.
- Custom destination boards, announcements, horn packs, and cabin accessories.
- Optional radio with licensed-safe streams or player-owned local audio.

> Some visual, performance, and device acceptance checks still require testing in Unity and on the target phones.
