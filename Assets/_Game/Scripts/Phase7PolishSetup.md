# Phase 7 - Polish, Audio and UX

Phase 7 UI systems bootstrap at runtime. Gameplay scenes now receive a safe-area HUD, pause flow, subtitles, accessibility handling, spatial vehicle audio, cosmetic effects, and quality-aware visuals. The main menu receives Settings and Credits actions, while scene transitions use an asynchronous city loading screen.

## Production asset wiring

- Assign production clips to `RealBusAudioManager`; an optional bridge receives named FMOD events through `PlayRealBusEvent`. The Unity audio layer remains the fallback when FMOD is unavailable.
- Name glass renderers with `glass` so transparent surface and reflection setup is applied automatically.
- Replace generated exhaust, dust, and rain-spray particles with authored prefabs when final effects are available.
- URP colour grading, bloom, headlight flare assets, detailed exterior/interior meshes, UVs, and scuff shaders remain art-pipeline inputs.

## Accessibility

Runtime text is raised to at least 28sp. Schedule states pair colour with symbols. Settings expose large text, high contrast, controls, quality, volume, language, haptics, and subtitles. Docking, collision, and mission completion trigger matching feedback.

## Validation before beta

Run `bash tools/run_editmode_tests.sh`, then complete a 45-minute Samsung Galaxy A54 route session. Capture frame pacing, memory trend, thermal state, battery usage, loading transitions, every menu, and each audio layer. The beta milestone requires no sustained thermal throttling, no memory growth, no recurring stutter, and under 15% battery use for the session.
