# Mobile polish implementation

The first polish pass is runtime-bootstrapped and preserves existing scenes and prefabs.

## Included

- A skippable first-drive tutorial and optional speed/stop-approach assists.
- Per-handedness touch layouts with drag, individual resize, global scale, sensitivity, and dead-zone controls.
- 30/45/60 FPS targets, adaptive resolution, Android thermal response, and low-memory fallback.
- Live docking bay feedback with kerb, stop-mark, heading, speed, score, and grade guidance.
- Passenger mood reactions, turn guidance, missed-stop rerouting, and traffic deadlock recovery.
- Exterior, cockpit, wide chase, and kerb cameras with a quality-gated digital mirror.
- Camera-, cabin-, surface-, load-, and weather-aware audio mixing.
- Resumable offline map status with cached tile count, size estimate, and available storage.
- Periodic, suspend, focus-loss, quit, and low-memory saves with mission checkpoints.
- A Resume/Start Over prompt that restores route, bus, schedule, passengers, fares, and scores.
- Lower upgrade prices with visible performance trade-offs.
- Left-handed controls and reduced-motion UI animation support.
- Cockpit touch controls and localized cosmetic-damage shader properties.
- Local balance telemetry at `Application.persistentDataPath/realbus_balance_telemetry.csv`.

## Final asset handoff

Create `Resources/PassengerReactionLibrary.asset` for voice clips and animator triggers. Create `Resources/AudioProductionProfile.asset` for licensed clips and mixer groups. Runtime fallbacks remain active for empty slots.

## Device validation

Test 16:9, 19.5:9, and tablet layouts. On the Galaxy A54, run 30, 45, and 60 FPS sessions for at least 20 minutes each and verify resolution recovery, thermal fallback, touch-layout persistence, camera cost, mirror cost, interrupted downloads, suspend/resume, and low-memory recovery. Tune thresholds from captured frame-time and thermal logs before release.

## Nice-to-have systems added

- Daily real-route challenges selected from the local date, city, routes, weather, and departure windows.
- Persistent challenge completion streaks and scenario bonus XP.
- Route mastery medals across smoothness, punctuality, safety, and efficiency.
- Compact local best-run recordings with an in-world driver ghost.
- Recurring passenger profiles with trust-based story stages.
