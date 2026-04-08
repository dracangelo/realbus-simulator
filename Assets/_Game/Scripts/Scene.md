# RealBus Simulator — Scene Hierarchy & Component Reference

> **Last updated:** Phase 6.5 complete  
> **Unity version:** 6000.3.10f1 (Unity 6.3 LTS)  
> **Platform:** Android (Samsung Galaxy S20 / A54)
> **Orientation target:** Landscape-only on Android phones and tablets

---

## Table of Contents
1. [Project Structure](#project-structure)
2. [Build Settings — Scene Order](#build-settings--scene-order)
3. [MainMenu Scene](#mainmenu-scene)
4. [CountrySelect Scene](#countryselect-scene)
5. [CitySelect Scene](#cityselect-scene)
6. [RouteSelect Scene](#routeselect-scene)
7. [GameScene](#gamescene)
8. [ScriptableObjects Reference](#scriptableobjects-reference)
9. [StreamingAssets Structure](#streamingassets-structure)
10. [Key Inspector Wiring Rules](#key-inspector-wiring-rules)

---

## Project Structure

```
Assets/
├── _Game/
│   ├── Fonts/
│   │   ├── SpaceGrotesk-Bold.ttf + SDF asset
│   │   ├── SpaceGrotesk-Medium.ttf + SDF asset
│   │   ├── SpaceGrotesk-Regular.ttf + SDF asset
│   │   └── SpaceGrotesk-Light.ttf + SDF asset
│   ├── Materials/
│   │   ├── RoadMaterial          (URP/Lit, dark grey #282828)
│   │   ├── PavementMaterial      (URP/Lit, light grey #B4B4B4)
│   │   └── BuildingMaterial      (URP/Lit, warm concrete #C8BEB0)
│   ├── ScriptableObjects/
│   │   ├── Cities/
│   │   │   └── City_Nairobi, City_London, City_Tokyo ... (16 total)
│   │   ├── Countries/
│   │   │   └── Country_Kenya, Country_UK, Country_Japan ... (11 total)
│   │   ├── EngineData
│   │   ├── TransmissionData
│   │   ├── PassengerData
│   │   ├── Route_01_CBD_Westlands
│   │   └── Mission_01_CBD_Westlands
│   ├── Scripts/
│   │   ├── Bus/
│   │   │   ├── BusController.cs
│   │   │   ├── EngineSystem.cs
│   │   │   ├── TransmissionSystem.cs
│   │   │   └── WheelVisualSync.cs
│   │   ├── City/
│   │   │   ├── CityDefinition.cs      (ScriptableObject)
│   │   │   ├── CityManager.cs
│   │   │   ├── CountryDefinition.cs   (ScriptableObject)
│   │   │   └── GameState.cs
│   │   ├── Editor/
│   │   │   └── CountryImporter.cs     (Editor tool only)
│   │   ├── Environment/
│   │   │   ├── WeatherSystem.cs       (Phase 5.3 — added after 6.5)
│   │   │   ├── RainController.cs
│   │   │   ├── FogController.cs
│   │   │   ├── SnowController.cs
│   │   │   └── SkyController.cs
│   │   ├── Map/
│   │   │   ├── MapTileLoader.cs
│   │   │   ├── GPSManager.cs
│   │   │   ├── BusStop.cs
│   │   │   ├── BusRoute.cs            (ScriptableObject)
│   │   │   ├── RouteVisualizer.cs
│   │   │   ├── FreeDriveSession.cs
│   │   │   ├── MissionManager.cs
│   │   │   ├── MissionState.cs        (enum)
│   │   │   ├── MissionData.cs         (ScriptableObject)
│   │   │   ├── MissionResult.cs
│   │   │   └── ScheduleManager.cs
│   │   ├── OSM/
│   │   │   ├── OSMTypes.cs
│   │   │   ├── OSMParser.cs
│   │   │   ├── OSMLoader.cs
│   │   │   ├── OSMBuildingLoader.cs
│   │   │   ├── OSMRoadMeshBuilder.cs
│   │   │   ├── OSMBuildingMeshBuilder.cs
│   │   │   └── OSMRouteImporter.cs
│   │   ├── Passengers/
│   │   │   ├── PassengerData.cs       (ScriptableObject)
│   │   │   └── PassengerManager.cs
│   │   ├── Traffic/
│   │   │   ├── TrafficLight.cs
│   │   │   └── SignalViolationDetector.cs
│   │   └── UI/
│   │       ├── UITheme.cs             (static class — colors & fonts)
│   │       ├── UIFonts.cs             (MonoBehaviour — font registry)
│   │       ├── UIAnimator.cs          (static class — animations)
│   │       ├── SceneLoader.cs
│   │       ├── SceneBootstrap.cs
│   │       ├── MainMenuUI.cs
│   │       ├── CountrySelectUI.cs
│   │       ├── CitySelectUI.cs
│   │       ├── RouteSelectUI.cs
│   │       ├── MissionBriefingUI.cs
│   │       ├── MissionResultUI.cs
│   │       ├── FreeDriveUI.cs
│   │       ├── StopApproachUI.cs
│   │       └── MobileControlsUI.cs
│   └── Scenes/
│       ├── MainMenu.unity
│       ├── CountrySelect.unity
│       ├── CitySelect.unity
│       ├── RouteSelect.unity
│       └── GameScene.unity
├── StreamingAssets/
│   └── Cities/
│       └── NBO/
│           ├── roads.xml
│           └── buildings.xml
└── Settings/
    ├── URP_Low.asset
    ├── URP_Medium.asset
    └── URP_High.asset
```

---

## Build Settings — Scene Order

| Index | Scene Name | Purpose |
|-------|-----------|---------|
| 0 | MainMenu | Entry point, persistent managers live here |
| 1 | CountrySelect | Browse countries grouped by continent |
| 2 | CitySelect | Browse cities in selected country |
| 3 | RouteSelect | Browse routes in selected city |
| 4 | GameScene | Driving, mission, HUD |

> ⚠️ **IMPORTANT:** `SceneLoader`, `GameState`, `CityManager`, `UIFonts`  
> only exist in **MainMenu**. They persist via `DontDestroyOnLoad`.  
> Never add these to other scenes.

---

## MainMenu Scene

### Hierarchy
```
MainMenu
├── GameManagers                    ← Persistent managers (DontDestroyOnLoad)
│   ├── SceneLoader
│   ├── GameState
│   ├── CityManager
│   └── UIFonts
├── Canvas (Canvas)
│   ├── Canvas Scaler               ← UI Scale Mode: Scale With Screen Size, Reference Resolution: 1920×1080, Match: 0.5
│   ├── MainMenuPanel (Panel)       ← Image + CanvasGroup, anchors stretch, Left:0 Right:0 Top:0 Bottom:0, color #0C0F10
│   │   └── RootPanel (Panel)       ← RectTransform only, anchors stretch, Left:56 Right:-56 Top:-32 Bottom:32
│   │       ├── TopBar (Panel)      ← RectTransform only, anchor min(0,1) max(1,1), pivot(0.5,1), Pos X:0 Y:0, Width:0 Height:88
│   │       │   ├── BrandGroup (Panel)      ← RectTransform only, anchor min(0,0.5) max(0,0.5), pivot(0,0.5), Pos X:0 Y:-44, W:460 H:64
│   │       │   │   └── Text_Title (TMP)    ← TextMeshProUGUI, Pos X:0 Y:0, W:460 H:64, text "REAL BUS SIM", size 32, bold, #FF9159
│   │       │   ├── NavLinks (Panel)        ← RectTransform + Horizontal Layout Group, anchor center, pivot(0.5,0.5), Pos X:0 Y:-44, W:760 H:56, spacing 38
│   │       │   │   ├── Text_Drive (TMP)    ← TextMeshProUGUI, preferred W:130 H:56, text "DRIVE", size 18, bold, #FF9159
│   │       │   │   ├── Text_Garage (TMP)   ← TextMeshProUGUI, preferred W:160 H:56, text "GARAGE", size 18, bold, #F8F9FC @ 60%
│   │       │   │   ├── Text_Routes (TMP)   ← TextMeshProUGUI, preferred W:160 H:56, text "ROUTES", size 18, bold, #F8F9FC @ 60%
│   │       │   │   └── Text_Market (TMP)   ← TextMeshProUGUI, preferred W:160 H:56, text "MARKET", size 18, bold, #F8F9FC @ 60%
│   │       │   └── CurrencyBadge (Panel)   ← Image, anchor min(1,0.5) max(1,0.5), pivot(1,0.5), Pos X:0 Y:-44, W:240 H:54, color #161A1C
│   │       │       └── Text_Cash (TMP)     ← TextMeshProUGUI, Pos X:0 Y:0, W:240 H:54, text "$254,000", size 22, bold, #FF9159
│   │       ├── HeroCard (Panel)            ← Image, anchor min(0,1) max(1,1), pivot(0.5,1), Pos X:0 Y:-112, W:0 H:430, color #1C2023
│   │       │   ├── HeroBusImage (Image)    ← Image, anchors stretch, Left:0 Right:0 Top:0 Bottom:0, assign bus artwork
│   │       │   ├── HeroOverlay (Image)     ← Image, anchors stretch, Left:0 Right:0 Top:0 Bottom:0, color #0C0F10 @ 58%
│   │       │   └── HeroContent (Panel)     ← RectTransform only, anchors stretch, Left:44 Right:-44 Top:-40 Bottom:40
│   │       │       ├── StatusChip (Panel)  ← Image, anchor min(0,1) max(0,1), pivot(0,1), Pos X:0 Y:0, W:280 H:40, color #FF9159 @ 18%
│   │       │       │   └── Text_Status (TMP)    ← TextMeshProUGUI, Pos X:0 Y:0, W:280 H:40, text "CURRENTLY EQUIPPED", size 13, bold, #FF9159
│   │       │       ├── Text_HeroTitle (TMP)     ← TextMeshProUGUI, anchor min(0,0) max(0,0), pivot(0,0), Pos X:0 Y:122, W:760 H:90, text "VOLTA S-SERIES", size 64, bold italic, #F8F9FC
│   │       │       ├── Text_HeroDesc (TMP)      ← TextMeshProUGUI, anchor min(0,0) max(0,0), pivot(0,0), Pos X:0 Y:66, W:700 H:52, size 21, #A9ABAE
│   │       │       ├── Btn_Play (Button)        ← Image + Button, anchor min(0,0) max(0,0), pivot(0,0), Pos X:0 Y:0, W:340 H:72, color #FF9159
│   │       │       │   └── Text (TMP)           ← TextMeshProUGUI, Pos X:0 Y:0, W:340 H:72, text "START DRIVING", size 22, bold, #0C0F10
│   │       │       └── Btn_Customize (Button)   ← Image + Button, anchor min(0,0) max(0,0), pivot(0,0), Pos X:360 Y:0, W:270 H:72, color #222629 @ 88%
│   │       │           └── Text (TMP)           ← TextMeshProUGUI, Pos X:0 Y:0, W:270 H:72, text "CUSTOMIZE", size 22, bold, #F8F9FC
│   │       ├── StatsGrid (Panel)                ← RectTransform only, anchors stretch, Left:0 Right:0 Top:-566 Bottom:0
│   │       │   ├── GaragePanel (Panel)          ← Image, anchor min(0,0) max(0,1), pivot(0,0.5), Pos X:0 Y:0, W:1180 H:0, color #1C2023
│   │       │   │   ├── Text_GarageTitle (TMP)       ← anchor top-left, pivot(0,1), Pos X:32 Y:-28, W:340 H:36, text "MY GARAGE", size 26, bold
│   │       │   │   ├── Text_GarageSubtitle (TMP)    ← anchor top-left, pivot(0,1), Pos X:32 Y:-64, W:460 H:26, text "Technical Performance Analysis", size 17
│   │       │   │   ├── Stat_Battery (Panel)         ← Image, anchor min(0,1) max(0,1), pivot(0,1), Pos X:32 Y:-116, W:532 H:120
│   │       │   │   ├── Stat_Capacity (Panel)        ← Image, anchor min(1,1) max(1,1), pivot(1,1), Pos X:-32 Y:-116, W:532 H:120
│   │       │   │   ├── Stat_Wear (Panel)            ← Image, anchor min(0,1) max(0,1), pivot(0,1), Pos X:32 Y:-252, W:532 H:120
│   │       │   │   └── Stat_Speed (Panel)           ← Image, anchor min(1,1) max(1,1), pivot(1,1), Pos X:-32 Y:-252, W:532 H:120
│   │       │   └── CareerPanel (Panel)          ← Image, anchor min(1,0) max(1,1), pivot(1,0.5), Pos X:0 Y:0, W:572 H:0, color #1C2023
│   │       │       ├── Text_CareerTitle (TMP)       ← anchor top-left, pivot(0,1), Pos X:32 Y:-28, W:220 H:36, text "CAREER", size 26, bold
│   │       │       ├── Text_CareerSubtitle (TMP)    ← anchor top-left, pivot(0,1), Pos X:32 Y:-64, W:300 H:48, text "Driver Level & Experience", size 17
│   │       │       ├── Text_LevelValue (TMP)        ← anchor center, pivot(0.5,0.5), Pos X:0 Y:8, W:180 H:82, text "24", size 68, bold
│   │       │       ├── XPBar_BG (Image)             ← anchor center, pivot(0.5,0.5), Pos X:0 Y:-62, W:300 H:10, color #282D30
│   │       │       │   └── XPBar_Fill (Image)       ← anchors min(0,0) max(0.84,1), Left:0 Right:0 Top:0 Bottom:0, color #FF9159
│   │       │       └── Text_XPValue (TMP)           ← anchor center, pivot(0.5,0.5), Pos X:0 Y:-92, W:300 H:28, text "8,420 / 10,000 XP", size 18
│   │       └── Text_Version (TMP)               ← anchor min(1,0) max(1,0), pivot(1,0), Pos X:0 Y:0, W:280 H:24, text "v0.1.0 — Early Access", size 16, #737678
│   └── EventSystem
└── (no SceneBootstrap — MainMenu is the origin scene)
```

### Components — GameManagers

| Component | Key Settings |
|-----------|-------------|
| `SceneLoader` | DontDestroyOnLoad — loads all scenes, forces Android auto-rotation to landscape left/right only |
| `GameState` | DontDestroyOnLoad — holds selectedCountry, selectedCity, selectedRoute |
| `CityManager` | DontDestroyOnLoad — allCities (16), allCountries (11), activeCity = City_Nairobi |
| `UIFonts` | DontDestroyOnLoad — bold, medium, regular, light → Space Grotesk SDF assets |

### Components — Canvas
| Component | Key Settings |
|-----------|-------------|
| `MainMenuUI` | canvasGroup→MainMenuPanel, rootPanel→RootPanel, topBar→TopBar, heroCard→HeroCard, statsGrid→StatsGrid, backgroundPanel→MainMenuPanel |
| `MainMenuUI` | titleText→Text_Title, navDriveText→Text_Drive, navGarageText→Text_Garage, navRoutesText→Text_Routes, navMarketText→Text_Market, currencyBadge→CurrencyBadge, currencyText→Text_Cash |
| `MainMenuUI` | heroCardBackground→HeroCard, heroGradientOverlay→HeroOverlay, statusChipBackground→StatusChip, statusChipText→Text_Status, heroTitleText→Text_HeroTitle, heroDescriptionText→Text_HeroDesc |
| `MainMenuUI` | playButton→Btn_Play, playButtonText→Btn_Play/Text, customizeButton→Btn_Customize, customizeButtonText→Btn_Customize/Text, versionText→Text_Version |
| `MainMenuUI` | garagePanel→GaragePanel, garageTitleText→Text_GarageTitle, garageSubtitleText→Text_GarageSubtitle, batteryValueText→Stat_Battery/Text_Value, batteryFill→Stat_Battery/Bar_BG/Bar_Fill |
| `MainMenuUI` | capacityValueText→Stat_Capacity/Text_Value, capacityFill→Stat_Capacity/Bar_BG/Bar_Fill, wearValueText→Stat_Wear/Text_Value, wearFill→Stat_Wear/Bar_BG/Bar_Fill |
| `MainMenuUI` | speedValueText→Stat_Speed/Text_Value, speedFill→Stat_Speed/Bar_BG/Bar_Fill, careerPanel→CareerPanel, careerTitleText→Text_CareerTitle, careerSubtitleText→Text_CareerSubtitle, levelValueText→Text_LevelValue, xpValueText→Text_XPValue, xpFill→XPBar_BG/XPBar_Fill |
| `MainMenuUI` | Runtime layout: preserves tshe PC-style landscape composition on Android, applies safe-area padding, compresses hero/text sizing on shorter landscape phones, and converts the garage/career region into a vertical scroll area on the tightest landscape screens |

### Editor Build Recipe — Recreate The Landing Screen
1. Select `Canvas Scaler` and keep `Reference Resolution = 1920 × 1080`, `Screen Match Mode = Match Width Or Height`, `Match = 0.5`.
2. Android target is landscape-only. Portrait is not supported; the runtime locks rotation to `LandscapeLeft` and `LandscapeRight`.
3. Stretch `MainMenuPanel` to the full canvas and add `CanvasGroup` plus `Image` with color `#0C0F10`.
4. Create `RootPanel` as a child of `MainMenuPanel`, set anchors to full stretch, then offsets `Left 56`, `Right -56`, `Top -32`, `Bottom 32`.
5. Create `TopBar` under `RootPanel`, anchors `min(0,1)` `max(1,1)`, pivot `(0.5,1)`, height `88`, anchored Y `0`.
6. Add `BrandGroup` inside `TopBar`, anchor left center, size `460 × 64`, pos `X 0 Y -44`. Add `Text_Title` with TMP alignment `Midline Left`, font `SpaceGrotesk Bold`, size `32`, character spacing `14`.
7. Add `NavLinks` inside `TopBar`, anchor center, size `760 × 56`, position `Y -44`, then add `Horizontal Layout Group` with spacing `38`, child alignment `Middle Center`, child force expand off.
8. Inside `NavLinks`, create `Text_Drive`, `Text_Garage`, `Text_Routes`, `Text_Market`. Give each TMP size `18`, uppercase text, font `SpaceGrotesk Bold`, alignment centered, character spacing `6`.
9. Add `CurrencyBadge` in `TopBar`, anchor right center, pivot `(1,0.5)`, size `240 × 54`, pos `X 0 Y -44`. Add `Image` color `#161A1C` and set sprite type to `Sliced` if you have a rounded sprite. Inside it, add `Text_Cash` with centered-left padding look and size `22`.
10. Create `HeroCard` under `RootPanel`, anchor stretch top, pivot `(0.5,1)`, height `430`, offsets `Left 0 Right 0 Top -112`. Add `Image` color `#1C2023`.
11. Add `HeroBusImage` to fill `HeroCard`. Use your bus promo artwork or a cropped bus screenshot from gameplay. Set `Image Type = Simple`, `Preserve Aspect = false`, and crop for a cinematic wide composition.
12. Add `HeroOverlay` above the image, full stretch, color `#0C0F10` with alpha around `148`. If you have a vertical gradient sprite, use that instead of a flat alpha image.
13. Add `HeroContent` full stretch inside `HeroCard` with offsets `44, 40, -44, 40`.
14. Place `StatusChip` at top-left of `HeroContent`, anchor `(0,1)`, pivot `(0,1)`, size `280 × 40`, position `X 0 Y 0`. Add `Image` color `#FF9159` at `18%` alpha and child TMP `Text_Status` size `13`, centered, bold, character spacing `6`.
15. Place `Text_HeroTitle` near the lower-left of the hero, anchor `(0,0)`, pivot `(0,0)`, position `X 0 Y 122`, size `760 × 90`, font `SpaceGrotesk Bold`, size `64`, italic, alignment `Bottom Left`.
16. Place `Text_HeroDesc` below the title, anchor `(0,0)`, pivot `(0,0)`, position `X 0 Y 66`, size `700 × 52`, font `SpaceGrotesk Medium`, size `21`, line spacing around `6`.
17. Create `Btn_Play` and `Btn_Customize` at the lower-left of `HeroContent`. Use anchors `(0,0)`, sizes `340 × 72` and `270 × 72`, positions `Y 0`, with `Btn_Customize` shifted right to `X 360`.
18. Set `Btn_Play` image color `#FF9159`. Set `Btn_Customize` image color `#222629` with alpha `225`. Give both buttons `Navigation = None` if this is mobile-only.
19. Add TMP children for both buttons, font `SpaceGrotesk Bold`, size `22`, character spacing `4`, centered. `Btn_Play` text color is `#0C0F10`. `Btn_Customize` text color is `#F8F9FC`.
20. Create `StatsGrid` under `RootPanel`, stretch horizontally, anchor from top `0` to bottom `0`, offsets `Top -566`, `Bottom 0`, `Left 0`, `Right 0`.
21. Inside `StatsGrid`, add `GaragePanel` anchored left stretch with width `1180` and `CareerPanel` anchored right stretch with width `572`. Keep a gap of `24`.
22. Give both panels `Image` color `#1C2023`. If you have a rounded sprite, use the same sliced sprite as the hero. Padding inside each panel should be `32`.
23. In `GaragePanel`, add `Text_GarageTitle` and `Text_GarageSubtitle` at the top. Then create a 2×2 grid of stat cards: `Stat_Battery`, `Stat_Capacity`, `Stat_Wear`, `Stat_Speed`.
24. Runtime behavior: on the tightest landscape phones, `MainMenuUI` wraps `StatsGrid` in a generated masked `ScrollRect` and stacks `GaragePanel` above `CareerPanel` so the lower section scrolls vertically instead of getting cramped.
25. Each stat card should be `532 × 120`, color `#222629`, and contain `Text_Label` size `12`, `Text_Value` size `34`, `Bar_BG` size `420 × 10`, and `Bar_Fill` anchored from the left.
26. Use these values exactly: `Battery Range = 420 KM`, `Max Capacity = 85 PAX`, `Wear Level = 12%`, `Top Speed = 115 KPH`.
27. Use these bar fill widths: battery `85%` blue, capacity `70%` yellow, wear `12%` red, speed `92%` orange.
28. In `CareerPanel`, add `Text_CareerTitle`, `Text_CareerSubtitle`, a large centered `Text_LevelValue` using size `68`, then `XPBar_BG` with width `300` and `XPBar_Fill` at `84%` width, and `Text_XPValue` under it.
29. Add `Text_Version` under `RootPanel`, anchor bottom-right, pivot `(1,0)`, position `X 0 Y 0`, size `280 × 24`, alignment `Bottom Right`.
30. Add the `MainMenuUI` component to `Canvas` or `MainMenuPanel` and wire every field using the mapping table above.
31. Keep your current scene flow unchanged: `Btn_Play` still calls `SceneLoader.Instance?.LoadCountrySelect()`.
32. For the hero artwork, use a high-resolution bus image with lots of empty space on the left-bottom third so the text stays readable. If your image is busy, darken `HeroOverlay` more.

### Notes
- The reference HTML uses a frosted-glass secondary button. In Unity mobile UI, a tinted dark panel looks close enough and is much cheaper than real blur.
- The entire look depends on spacing and scale. If it feels wrong, first check padding, font sizes, and panel heights before changing colors.

---

## CountrySelect Scene

### Hierarchy
```
CountrySelect
├── Canvas
│   ├── Canvas Scaler               ← 1920×1080, Match 0.5
│   ├── MainPanel                   ← Image #0C0F10, CanvasGroup
│   │   ├── TopGlow                 ← subtle orange glow accent
│   │   └── ContentPanel            ← stretch, Left 72 Right -72 Top -36 Bottom 36
│   │       ├── TopBar              ← dark glass strip with brand + status
│   │       │   ├── BrandGroup      ← TMP "REAL BUS SIM"
│   │       │   └── RightCluster    ← RankChip + CurrencyBadge
│   │       ├── HeroBand            ← Image #1C2023, premium header card
│   │       │   └── HeroContent
│   │       │       ├── ModeBadge   ← "REGION NETWORK"
│   │       │       ├── Text_Title  ← TMP "SELECT COUNTRY", size 42, bold
│   │       │       ├── AccentLine  ← Image #FF9159, 84×4
│   │       │       ├── Text_Subtitle ← TMP "Choose a region and discover its city network."
│   │       │       └── InfoRail    ← 3 stat tiles
│   │       ├── ListShell           ← main list surface
│   │       │   ├── ListHeader      ← "AVAILABLE REGIONS" + helper text
│   │       │   └── CountryScrollView
│   │       │       └── Viewport    ← Mask, transparent Image
│   │       │           └── Content ← Vertical Layout Group, Content Size Fitter
│   │       └── FooterBar
│   │           ├── Btn_Back        ← Button #161A1C, 260×54
│   │           │   └── Text (TMP)  ← "BACK", #A9ABAE
│   │           └── Text_FooterHint ← helper note
│   └── EventSystem
└── Canvas → SceneBootstrap (redirects to MainMenu if no SceneLoader)
```

### Components — Canvas
| Component | Key Settings |
|-----------|-------------|
| `CountrySelectUI` | canvasGroup→MainPanel, contentPanel→ContentPanel, titleText→Text_Title, subtitleText→Text_Subtitle, accentLine→AccentLine, countryListContainer→Content, backButton→Btn_Back, backButtonText→Btn_Back/Text, backgroundPanel→MainPanel |
| `SceneBootstrap` | Redirects to MainMenu if SceneLoader.Instance is null |

> **Note:** Country cards are generated at runtime by `CountrySelectUI`.  
> The editor builder creates the premium landscape shell only.  
> `CountrySelectUI` still spawns the actual country cards into `Content` at runtime.

---

## CitySelect Scene

### Hierarchy
```
CitySelect
├── Canvas
│   ├── Canvas Scaler               ← 1920×1080, Match 0.5
│   ├── MainPanel                   ← Image #0C0F10, CanvasGroup, has CitySelectUI
│   │   └── ContentPanel            ← stretch, Left 72 Right -72 Top -36 Bottom 36
│   │       ├── TopBar              ← brand, nav labels, currency badge
│   │       ├── HeaderSection       ← large title/subtitle with metrics cards
│   │       │   ├── Text_Title      ← TMP "SELECT CITY"
│   │       │   ├── Text_TitleAccent ← TMP "ZONE"
│   │       │   ├── AccentLine      ← Image #FF9159
│   │       │   ├── Text_Subtitle   ← TMP (shows "CountryName — X cities" at runtime)
│   │       │   └── MetricsRow      ← 3 compact stat cards
│   │       ├── CityShell           ← large surface container
│   │       │   ├── CityHeader      ← "DEPLOYMENT CITIES" + helper copy
│   │       │   └── CityScrollView  ← ScrollRect
│   │       │       └── Viewport    ← Mask, transparent Image
│   │       │           └── CityGrid ← Grid Layout Group, 3 columns, Content Size Fitter
│   │       ├── IntelSection        ← "LATEST INTEL" + 3 info cards
│   │       └── FooterBar
│   │           └── Btn_Back        ← Button #161A1C, 260×48
│   │               └── Text (TMP)  ← "BACK"
│   └── EventSystem
└── Canvas → SceneBootstrap
```

### Components — Canvas
| Component | Key Settings |
|-----------|-------------|
| `CitySelectUI` | canvasGroup→MainPanel, contentPanel→ContentPanel, titleText→Text_Title, subtitleText→Text_Subtitle, cityListContainer→CityGrid, backButton→Btn_Back, backButtonText→Btn_Back/Text, headerAccentLine→AccentLine, scrollRect→CityScrollView |
| `SceneBootstrap` | — |

> **Note:** The editor builder creates the desktop-style shell only.  
> `CitySelectUI` still generates the actual city cards into `CityGrid` at runtime  
> using `CityManager.activeCountry.cities`.

---

## RouteSelect Scene

### Hierarchy
```
RouteSelect
├── Canvas
│   ├── Canvas Scaler (1080×1920)
│   ├── MainPanel                   ← Image #0C0F10, CanvasGroup
│   │   └── ContentPanel            ← anchor center, 900×1600
│   │       ├── Text_Title          ← TMP "SELECT ROUTE", size 42, bold
│   │       ├── AccentLine          ← Image #F59E0B, 60×3
│   │       ├── Text_CityName       ← TMP (shows "CityName, Country")
│   │       ├── RouteContainer      ← Vertical Layout Group, Content Size Fitter
│   │       │                          Width 820, anchor center
│   │       └── Btn_Back            ← Button #1C2023, 300×56
│   │           └── Text (TMP)      ← "BACK"
│   └── EventSystem
└── Canvas → SceneBootstrap
```

### Components — Canvas
| Component | Key Settings |
|-----------|-------------|
| `RouteSelectUI` | canvasGroup→MainPanel, contentPanel→ContentPanel, titleText→Text_Title, cityNameText→Text_CityName, routeListContainer→RouteContainer, backButton→Btn_Back, backButtonText→Btn_Back/Text, accentLine→AccentLine, backgroundPanel→MainPanel |
| `SceneBootstrap` | — |

> **Note:** Route cards generated at runtime. Shows route name, stop count,  
> base fare, green left accent bar. Routes from GameState.selectedCity.availableRoutes.

---

## GameScene

### Hierarchy
```
GameScene
├── MapSystem                       ← Position 0,0,0
│   ├── MapTileLoader
│   ├── GPSManager
│   ├── PassengerManager
│   ├── MissionManager
│   ├── FreeDriveSession
│   ├── ScheduleManager
│   ├── ScoreTracker
│   ├── OSMLoader
│   ├── OSMBuildingLoader
│   ├── OSMRoadMeshBuilder
│   ├── OSMBuildingMeshBuilder
│   └── OSMRouteImporter
│
├── Bus_Root                        ← Rigidbody (mass 12000, drag 0.3, angDrag 0.5)
│   ├── BusController
│   ├── EngineSystem (ScriptableObject ref → EngineData)
│   ├── TransmissionSystem (ScriptableObject ref → TransmissionData)
│   ├── WheelVisualSync
│   ├── Wheel_FL                    ← WheelCollider
│   ├── Wheel_FR                    ← WheelCollider
│   ├── Wheel_ML                    ← WheelCollider
│   ├── Wheel_MR                    ← WheelCollider
│   ├── Wheel_RL                    ← WheelCollider
│   └── Wheel_RR                    ← WheelCollider
│
├── Plane                           ← Scale 140×1×140
│   ├── Mesh Renderer (DISABLED — invisible but keeps collider)
│   └── Mesh Collider
│
├── BusStops                        ← Parent container, position 0,0,0
│   ├── Stop_GPO                    ← BusStop (lat -1.2864, lon 36.8172)
│   │                                  StopTrigger, DockingZone
│   ├── Stop_Koja                   ← BusStop (lat -1.2833, lon 36.8160)
│   │                                  StopTrigger, DockingZone
│   ├── Stop_Ambassadeur            ← BusStop (lat -1.2820, lon 36.8140)
│   │                                  StopTrigger, DockingZone
│   ├── Stop_UniversityWay          ← BusStop (lat -1.2800, lon 36.8110)
│   │                                  StopTrigger, DockingZone
│   ├── Stop_KenyattaAve            ← BusStop (lat -1.2770, lon 36.8090)
│   │                                  StopTrigger, DockingZone
│   ├── Stop_MuseumHill             ← BusStop (lat -1.2723, lon 36.8060)
│   │                                  StopTrigger, DockingZone
│   └── Stop_Westlands              ← BusStop (lat -1.2647, lon 36.8020)
│                                      StopTrigger, DockingZone
│
├── TrafficLights                   ← Parent container
│   ├── TL_Koja                     ← TrafficLight (phase 0), SignalViolationDetector
│   │   ├── Pole                    ← Cylinder, Scale 1×15×1, PosY 7
│   │   ├── Light_Red               ← Sphere, Scale 2×2×2, PosY 16
│   │   ├── Light_Amber             ← Sphere, Scale 2×2×2, PosY 13.5
│   │   └── Light_Green             ← Sphere, Scale 2×2×2, PosY 11
│   ├── TL_Ambassadeur              ← phase offset 15
│   ├── TL_UniversityWay            ← phase offset 30
│   ├── TL_Kenyatta                 ← phase offset 45
│   └── TL_MuseumHill               ← phase offset 20
│
├── RouteVisualizer                 ← Position 0,0,0
│   ├── RouteVisualizer script
│   │   ├── Route → Route_01_CBD_Westlands (fallback if no GameState)
│   │   ├── Route Color → orange #FF8000
│   │   ├── Line Width → 3
│   │   └── Line Height Y → 0.5
│   └── LineRenderer (added at runtime)
│
├── Directional Light               ← Sun light for SkyController
│
├── Camera                          ← Child of Bus_Root
│   ├── Position Y:6, Z:-12
│   └── Rotation X:12
│
└── Canvas
    ├── Canvas Scaler (1080×1920)
    ├── HUD_Panel                   ← Semi-transparent, anchor Top Left
    │   ├── Text_Speed              ← TMP, size 64, bold, #F8F9FC
    │   ├── Text_SpeedLabel         ← TMP "km/h", size 18, #A9ABAE
    │   ├── Text_Gear               ← TMP "G1", size 24, #FF9159
    │   ├── Text_Fuel               ← TMP "100%", size 18
    │   ├── FuelBar                 ← Image, Fill Horizontal, green
    │   ├── Text_Distance           ← TMP "0.00 km"
    │   ├── Text_Time               ← TMP "00:00"
    │   ├── Text_GPS                ← TMP coordinates, size 14
    │   ├── Text_Passengers         ← TMP "0 PAX", #15A4FF
    │   ├── Text_Clock              ← TMP "08:00 AM", bold
    │   └── Text_Score              ← TMP "100%", #34D399
    │
    ├── ApproachPanel               ← Hidden by default, StopApproachUI
    │   ├── Text_StopName
    │   ├── Text_Distance
    │   └── Text_Status
    │
    ├── BriefingPanel               ← Hidden by default, MissionBriefingUI
    │   └── ContentCard             ← #161A1C panel
    │       ├── Text_City
    │       ├── Text_RouteName
    │       ├── AccentLine
    │       ├── StatsRow            ← Horizontal Layout Group
    │       │   ├── Stat_Stops
    │       │   ├── Stat_Fare
    │       │   ├── Stat_Departure
    │       │   └── Stat_Duration
    │       ├── Text_StopsList
    │       ├── Btn_Start           ← #F59E0B
    │       └── Btn_Cancel          ← #161A1C
    │
    ├── ResultPanel                 ← Hidden by default, MissionResultUI
    │   └── ContentCard             ← #161A1C panel
    │       ├── Text_CityRoute
    │       ├── Text_TotalScore
    │       ├── Text_Stars
    │       ├── AccentLine
    │       ├── BreakdownRow        ← Horizontal Layout Group
    │       │   ├── Text_Punctuality
    │       │   ├── Text_Satisfaction
    │       │   ├── Text_Safety
    │       │   └── Text_Efficiency
    │       ├── SummaryRow          ← Horizontal Layout Group
    │       │   ├── Block_Passengers
    │       │   ├── Block_Fare
    │       │   ├── Block_Distance
    │       │   └── Block_XP
    │       ├── Btn_PlayAgain       ← #F59E0B
    │       └── Btn_MainMenu        ← #161A1C
    │
    ├── MobileControlsPanel         ← MobileControlsUI
    │   ├── Btn_Accelerate
    │   ├── Btn_Brake
    │   ├── Btn_SteerLeft
    │   ├── Btn_SteerRight
    │   ├── Btn_Retarder
    │   ├── Btn_Horn
    │   ├── Btn_ShiftUp
    │   └── Btn_ShiftDown
    │
    └── EventSystem
```

### Components — MapSystem

| Component | Key Inspector Settings |
|-----------|----------------------|
| `MapTileLoader` | mapboxToken (your token), mapStyle "mapbox/satellite-streets-v12", zoomLevel 15, tilesX/Y 13, tileWorldSize 300. **Reads centreLat/Lon from CityManager** |
| `GPSManager` | Singleton. Wraps MapTileLoader. Use `GPSManager.Instance.GpsToWorld(lat,lon)` |
| `PassengerManager` | passengerData→PassengerData ScriptableObject |
| `MissionManager` | currentRoute→Route_01_CBD_Westlands, busController→Bus_Root, countdownSeconds 5, missionData→Mission_01_CBD_Westlands |
| `FreeDriveSession` | fuelConsumptionPer100km 35, fuelCapacityLitres 200. **Only runs in GameScene** |
| `ScheduleManager` | gameStartTimeMinutes 480 (08:00), secondsPerGameMinute 1 |
| `ScoreTracker` | punctualityWeight 0.30, satisfactionWeight 0.30, safetyWeight 0.25, efficiencyWeight 0.15 |
| `OSMLoader` | **Only runs in GameScene.** Loads from StreamingAssets/Cities/NBO/roads.xml |
| `OSMBuildingLoader` | **Only runs in GameScene.** Loads from StreamingAssets/Cities/NBO/buildings.xml |
| `OSMRoadMeshBuilder` | roadMaterial→RoadMaterial, roadYOffset 0.02 |
| `OSMBuildingMeshBuilder` | buildingMaterial→BuildingMaterial, defaultBuildingHeight 10, maxBuildingsPerFrame 50 |
| `OSMRouteImporter` | autoImportOnStart ✅, fetches from Overpass API. Adds routes to activeCity |

### Components — Bus_Root

| Component | Key Settings |
|-----------|-------------|
| `Rigidbody` | Mass 12000, Linear Damping 0.3, Angular Damping 0.5, Interpolate, Continuous Dynamic |
| `BusController` | engineData→EngineData, transmissionData→TransmissionData, all 6 WheelColliders assigned |
| `TransmissionSystem` | gearRatios [3.5,2.0,1.35,1.0,0.82,0.68], reverseRatio -4.5, differential 4.1, gearMaxSpeeds [12,25,40,58,75,95] |

### Components — Bus Stops

Each stop has:

| Component | Key Settings |
|-----------|-------------|
| `BusStop` | stopName, latitude, longitude (see GPS table below) |
| `StopTrigger` | stopName (text), approachDistance 50 |
| `DockingZone` | stopName (text), dockingRadius 12 |

**GPS Coordinates:**

| Stop | Latitude | Longitude |
|------|----------|-----------|
| GPO | -1.2864 | 36.8172 |
| Koja | -1.2833 | 36.8160 |
| Ambassadeur | -1.2820 | 36.8140 |
| University Way | -1.2800 | 36.8110 |
| Kenyatta Avenue | -1.2770 | 36.8090 |
| Museum Hill | -1.2723 | 36.8060 |
| Westlands | -1.2647 | 36.8020 |

### Components — Traffic Lights

| Component | Key Settings |
|-----------|-------------|
| `TrafficLight` | redDuration 45, greenDuration 35, amberDuration 5, phaseOffset (varies per intersection) |
| `SignalViolationDetector` | trafficLight→parent TL GameObject, detectionWidth 8, detectionDepth 3 |

**Traffic Light Positions & Phase Offsets:**

| Name | World Position | Phase Offset |
|------|---------------|-------------|
| TL_Koja | (-133, 0, 345) | 0 |
| TL_Ambassadeur | (-356, 0, 489) | 15 |
| TL_UniversityWay | (-690, 0, 712) | 30 |
| TL_Kenyatta | (-912, 0, 1046) | 45 |
| TL_MuseumHill | (-1246, 0, 1569) | 20 |

### Components — Canvas (GameScene)

| Component | Key Settings |
|-----------|-------------|
| `FreeDriveUI` | session→MapSystem/FreeDriveSession, busController→Bus_Root, transmissionData→MapSystem/TransmissionSystem, all text/image slots wired |
| `StopApproachUI` | approachPanel→ApproachPanel, stopNameText, distanceText, statusText, panelBackground |
| `MissionBriefingUI` | briefingCanvasGroup→BriefingPanel, all slots wired, missionData→Mission_01_CBD_Westlands |
| `MissionResultUI` | resultCanvasGroup→ResultPanel, all slots wired |
| `MobileControlsUI` | All 8 button slots wired to Bus_Root/BusController |
| `SceneBootstrap` | Does NOT redirect from GameScene (GameScene is exempt) |

---

## ScriptableObjects Reference

### CityDefinition (16 cities)
Located: `Assets/_Game/ScriptableObjects/Cities/`

| Asset | City | Code | Climate | Snow |
|-------|------|------|---------|------|
| City_Nairobi | Nairobi | NBO | Tropical | ❌ |
| City_Nakuru | Nakuru | NKR | Tropical | ❌ |
| City_Mombasa | Mombasa | MBA | Tropical | ❌ |
| City_Kisumu | Kisumu | KSM | Tropical | ❌ |
| City_Kampala | Kampala | KLA | Tropical | ❌ |
| City_Cairo | Cairo | CAI | Desert | ❌ |
| City_CapeTown | Cape Town | CPT | Mediterranean | ❌ |
| City_DarEsSalaam | Dar es Salaam | DAR | Tropical | ❌ |
| City_Kigali | Kigali | KGL | Tropical | ❌ |
| City_London | London | LON | Temperate | ❌ |
| City_Delhi | Delhi | DEL | Arid | ❌ |
| City_Tokyo | Tokyo | TKY | Temperate | ✅ |
| City_Toronto | Toronto | TOR | Continental | ✅ |
| City_Ontario | Ontario | ONT | Continental | ✅ |
| City_Dhaka | Dhaka | DAC | Subtropical | ❌ |
| City_Sylhet | Sylhet | ZYL | Subtropical | ❌ |

### CountryDefinition (11 countries)
Located: `Assets/_Game/ScriptableObjects/Countries/`  
Generated by **Tools → RealBus → Generate Countries from Cities**

### BusRoute
| Asset | Route Name | Stops | Base Fare |
|-------|-----------|-------|-----------|
| Route_01_CBD_Westlands | CBD - Westlands | 7 | KES 50 |

### MissionData
| Asset | Mission | Route | Departure | Duration |
|-------|---------|-------|-----------|----------|
| Mission_01_CBD_Westlands | CBD to Westlands | Route_01 | 480 min (08:00) | 45 min |

---

## StreamingAssets Structure

```
Assets/StreamingAssets/
└── Cities/
    └── NBO/
        ├── roads.xml       ← 371KB, 2912 nodes, 458 road segments
        └── buildings.xml   ← 3.8MB, 36431 nodes, 5722 buildings
```

**To refresh OSM data:**
```bash
# Roads
curl -X POST "https://overpass-api.de/api/interpreter" \
  --data 'data=[out:xml][timeout:60];(way["highway"~"trunk|primary|secondary|tertiary"](-1.2900,36.8000,-1.2630,36.8280);>;);out body;' \
  -o Assets/StreamingAssets/Cities/NBO/roads.xml

# Buildings
curl -X POST "https://overpass-api.de/api/interpreter" \
  --data 'data=[out:xml][timeout:60];(way["building"](-1.2900,36.8100,-1.2800,36.8250);>;);out body;' \
  -o Assets/StreamingAssets/Cities/NBO/buildings.xml
```

**To add a new city:**
1. Create `CityDefinition` ScriptableObject with correct GPS bounds
2. Create `StreamingAssets/Cities/CITYCODE/` folder
3. Run curl commands with that city's bounding box
4. Run **Tools → RealBus → Generate Countries from Cities**
5. Add city to `CityManager.allCities` in MainMenu scene

---

## Key Inspector Wiring Rules

> These rules prevent the most common bugs:

1. **`GameManagers` only in MainMenu.** Never add SceneLoader, GameState,  
   CityManager, UIFonts to CountrySelect, CitySelect, RouteSelect or GameScene.

2. **`CityManager` only on GameManagers, never on MapSystem.**  
   MapSystem had a CityManager causing GameObjects to self-destruct.

3. **`SceneBootstrap` on every scene except MainMenu and GameScene.**  
   Both are exempt — MainMenu is origin, GameScene loads directly for testing.

4. **All OSM scripts check for GameScene before running:**
   ```csharp
   if (SceneManager.GetActiveScene().name != "GameScene") return;
   ```

5. **`MissionManager.currentRoute` must be assigned** in Inspector  
   (Route_01_CBD_Westlands) AND busController must point to Bus_Root.

6. **Bus Stop GPS coordinates must all be filled** in BusStop component.  
   Empty coordinates = stop at 0,0,0 = mission never progresses.

7. **MapTileLoader reads centreLat/Lon from CityManager** automatically.  
   Do not hardcode coordinates in MapTileLoader Inspector.

8. **Space Grotesk fonts** must be assigned in UIFonts component on  
   GameManagers (MainMenu scene). All UI scripts read fonts via  
   `UITheme.GetFont(UITheme.FontWeight.Bold)`.

---

## Design System

Colors (`UITheme.cs`):
```
Background:     #0C0F10  (60% — deep navy)
Surface:        #161A1C  (30% — card background)
SurfaceHigh:    #222629  (30% — elevated surface)
Accent:         #FF9159  (10% — primary orange)
Success:        #34D399  (green)
Secondary:      #15A4FF  (blue)
Tertiary:       #FFE483  (yellow)
Error:          #FF7351  (red)
TextPrimary:    #F8F9FC
TextSecondary:  #A9ABAE
TextMuted:      #737678
```

Continent colors:
```
Africa:   #FF9159 (orange)
Europe:   #15A4FF (blue)
Asia:     #A78BFA (violet)
Americas: #34D399 (emerald)
Oceania:  #22D3EE (cyan)
```

Font: **Space Grotesk** (Bold, Medium, Regular, Light)  
Reference: `Assets/_Game/Fonts/`

---

*Generated from RealBus Simulator development session — Phase 6.5*
