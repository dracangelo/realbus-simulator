# RealBus Simulator Features

## Bus simulation

- Realistic acceleration, braking, steering, gears, retarder, and parking brake.
- Diesel fuel and electric battery consumption systems.
- Passenger weight, aerodynamics, brake fade, tyre wear, and maintenance.
- Wet, paved, cobblestone, gravel, and dirt surface handling.
- Cinematic mobile driving controls with pedals, steering buttons or wheel, gear, horn, and retarder.
- Model-backed playable buses with generated physics, wheels, cameras, and docking points.
- Optional speed and stop-approach driving assists.

## Maps and routes

- OpenStreetMap road, building, stop, signal, and route importing.
- Imported routes automatically link to their geographic city with repaired metrics and duplicate filtering.
- Road-following route generation with one-way-road support.
- Mapbox tile streaming with distance-based detail levels.
- Resilient Mapbox PNG/JPEG decoding and accurate authorization diagnostics.
- Offline city downloads, cache limits, and LRU cleanup.
- In-game optional downloads for city world data, all routes, or one OSM route.
- GPS tracking, route previews, and runtime navigation lines.
- Cinematic country, city, and route selectors with swipeable cards.

## Gameplay

- Free-drive and scheduled mission modes.
- Guided, accuracy-graded docking against the real bus door and physical stop prefab.
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
- Pooled cars, trucks, motorcycles, emergency vehicles, and scheduled buses.
- Full-physics, spline, billboard, and distance-culling traffic tiers.
- Traffic lights, pedestrian crossings, passing, and emergency sirens.

## World realism

- Clear, rain, fog, snow, overcast, and night conditions.
- Gradual weather transitions, wet-road grip, wipers, and lighting changes.
- Time-of-day simulation with street, cabin, and headlight control.
- Vehicle, road, map-tile, and traffic level-of-detail systems.
- Runtime stress testing with CSV performance reports.
- Real gas-station models spawn at OpenStreetMap fuel locations.
- Varied-height OSM buildings with façade and roof materials.
- Vehicle-proximity building streaming and physical road-edge boundaries with junction openings.
- Imported road-surface models with automatic procedural-road fallback.
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
- Route, city, traffic, and OSM import editor tools.
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
