# Phase 6 - Progression and Meta Systems

Phase 6 is bootstrapped at runtime. Open the main menu and choose **Garage** to access fleet comparison, per-bus upgrades, and the livery studio.

## Implemented loop

- Missions award XP through `MissionManager`, update the ten-rank `XPSystem`, and display progress in the main menu and mission result UI.
- Featured cities unlock at ranks 1, 3, 5, 6, and 8. Completing every base route in a city adds its generated Grand Tour.
- The default fleet contains Standard Single-Decker, Minibus, Articulated 18m, Electric, and Double-Decker buses. Electric buses use `BatterySystem`; diesel buses use `FuelSystem`.
- The garage upgrade shop sells five per-bus modifier assets: engine torque, brake torque, energy efficiency, steering response, and energy capacity. Purchases immediately reapply the selected bus configuration.
- The livery studio paints six zones into one runtime `RenderTexture`, includes eight rank-gated presets, and imports/exports 12-character alphanumeric codes.
- `SaveManager` stores XP, rank, unlocks, selected bus, upgrades, liveries, mission history, economy, and vehicle condition in PlayerPrefs and the PlayFab payload. Mission completion, rank-up, purchases, livery saves, app background, and app quit trigger saves.

## PlayFab deployment

1. Enable the PlayFab SDK and `PLAYFAB_SDK` scripting define.
2. Set the PlayFab Title ID.
3. Deploy `PlayFabCloudScript/RealBusSaveHandlers.js` as legacy CloudScript.
4. Publish a revision containing `GetPlayerSaveData` and `SetPlayerSaveData`.

Without PlayFab configuration the complete progression loop remains functional using the local PlayerPrefs backup.

## Livery model setup

The runtime editor works immediately with a six-stripe fallback preview. For production bus art, provide a readable zone-map texture whose red channel maps 0 through 1 across Roof, Front, Side A, Side B, Rear, and Wheel Arches. Assign the bus renderers and zone map to `LiveryEditor`; the generated RenderTexture is applied to `_BaseMap`, falling back to `_MainTex`.

## Validation

Run `bash tools/run_editmode_tests.sh` after Unity regenerates project files. The EditMode suite covers rank thresholds, fleet unlocks, Grand Tour generation, save restoration, livery-code round trips, per-bus upgrade persistence, currency deductions, and measurable fuel-consumption modifiers.
