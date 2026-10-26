using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Covers the startup-experience systems: the loading overlay's readiness gate,
/// the HUD fitter that keeps panels clear of the driving controls, and the
/// fallback that repaints imported models whose source textures are missing.
/// </summary>
public class WorldSystemsTests
{
    // ── Loading overlay readiness gate ───────────────────────────────────────

    [Test]
    public void WorldReadiness_ReportsEachMilestoneUntilTheWorldIsPlayable()
    {
        var milestones = new WorldReadiness.Milestones();

        Assert.IsFalse(WorldReadiness.Evaluate(milestones).ready);
        Assert.AreEqual("RESTORING SAVE DATA...", WorldReadiness.Evaluate(milestones).status);

        milestones.saveLoaded = true;
        Assert.AreEqual("INITIALISING AUDIO...", WorldReadiness.Evaluate(milestones).status);

        milestones.audioReady = true;
        Assert.AreEqual("SPAWNING BUS...", WorldReadiness.Evaluate(milestones).status);

        milestones.busSpawned = true;
        Assert.AreEqual("CONFIGURING VEHICLE...", WorldReadiness.Evaluate(milestones).status);

        milestones.vehicleConfigured = true;
        Assert.AreEqual("INITIALISING MAP...", WorldReadiness.Evaluate(milestones).status);

        milestones.mapInitialised = true;
        Assert.AreEqual("CALIBRATING GPS...", WorldReadiness.Evaluate(milestones).status);

        milestones.gpsReady = true;
        Assert.AreEqual("BUILDING ROAD NETWORK...", WorldReadiness.Evaluate(milestones).status);

        milestones.roadsBuilt = true;
        Assert.AreEqual("LOADING BUILDINGS...", WorldReadiness.Evaluate(milestones).status);

        milestones.buildingsRingReady = true;
        Assert.AreEqual("DOWNLOADING MAP TILES...", WorldReadiness.Evaluate(milestones).status);

        milestones.tilesLoaded = true;
        Assert.AreEqual("PREPARING COCKPIT...", WorldReadiness.Evaluate(milestones).status);

        milestones.hudReady = true;
        var ready = WorldReadiness.Evaluate(milestones);
        Assert.IsTrue(ready.ready);
        Assert.AreEqual("WORLD READY", ready.status);
        Assert.AreEqual(1f, ready.progress, 0.0001f);
    }

    [Test]
    public void WorldReadiness_WaitsForNearbyAndFovAssets()
    {
        var milestones = new WorldReadiness.Milestones
        {
            saveLoaded = true,
            audioReady = true,
            busSpawned = true,
            vehicleConfigured = true,
            hudReady = true,
            mapInitialised = true,
            gpsReady = true,
            roadsBuilt = true,
            buildingsRingReady = true,
            tilesLoaded = true,
            preloadRequired = true,
            preloadReady = false,
            preloadStatus = "PLACING BUILDINGS IN VIEW..."
        };

        var waiting = WorldReadiness.Evaluate(milestones);
        Assert.IsFalse(waiting.ready);
        Assert.AreEqual("PLACING BUILDINGS IN VIEW...", waiting.status);
        StringAssert.Contains("nearby assets", WorldReadiness.DescribePending(milestones));

        milestones.preloadReady = true;
        var ready = WorldReadiness.Evaluate(milestones);
        Assert.IsTrue(ready.ready);
        Assert.AreEqual("WORLD READY", ready.status);
    }

    [Test]
    public void WorldReadiness_ProgressAdvancesThroughTheLadderAndNeverRegresses()
    {
        var milestones = new WorldReadiness.Milestones();
        float previous = WorldReadiness.Evaluate(milestones).progress;
        // Once every milestone is cleared the bar legitimately sits at full.
        bool saturated = previous >= 1f;

        for (int step = 1; step <= 10; step++)
        {
            ApplyStep(ref milestones, step);
            float progress = WorldReadiness.Evaluate(milestones).progress;

            Assert.GreaterOrEqual(progress, previous, $"progress regressed at step {step}");
            if (!saturated)
                Assert.Greater(progress, previous, $"progress stalled before the world was ready at step {step}");
            if (progress >= 1f)
                saturated = true;

            previous = progress;
        }

        Assert.IsTrue(saturated, "the ladder never reached a ready world");
        Assert.AreEqual(1f, previous, 0.0001f);
    }

    [Test]
    public void WorldReadiness_AFailedMapSaysSoInsteadOfDownloadingForever()
    {
        var milestones = new WorldReadiness.Milestones
        {
            saveLoaded = true,
            audioReady = true,
            busSpawned = true,
            vehicleConfigured = true,
            mapInitialised = true,
            gpsReady = true,
            roadsBuilt = true,
            buildingsRingReady = true,
            tilesLoaded = false,
            mapFailed = true
        };

        var snapshot = WorldReadiness.Evaluate(milestones);
        Assert.IsFalse(snapshot.ready, "a missing map must not report the world ready");
        Assert.AreEqual("MAP UNAVAILABLE", snapshot.status,
            "the player is told the download failed, not shown an endless download step");
        Assert.AreEqual(0.74f, snapshot.progress, 0.0001f,
            "the bar keeps the last honest milestone; it does not invent progress");
        StringAssert.Contains("map download failed", WorldReadiness.DescribePending(milestones));
    }

    static void ApplyStep(ref WorldReadiness.Milestones milestones, int step)
    {
        if (step >= 1) milestones.saveLoaded = true;
        if (step >= 2) milestones.audioReady = true;
        if (step >= 3) milestones.busSpawned = true;
        if (step >= 4) milestones.vehicleConfigured = true;
        if (step >= 5) milestones.mapInitialised = true;
        if (step >= 6) milestones.gpsReady = true;
        if (step >= 7) milestones.roadsBuilt = true;
        if (step >= 8) milestones.buildingsRingReady = true;
        if (step >= 9) milestones.tilesLoaded = true;
        if (step >= 10) milestones.hudReady = true;
    }

    [Test]
    public void WorldReadiness_OnlyWaitsForTheDepotWhileAMissionIsStarting()
    {
        var milestones = new WorldReadiness.Milestones
        {
            saveLoaded = true,
            audioReady = true,
            busSpawned = true,
            vehicleConfigured = true,
            hudReady = true,
            mapInitialised = true,
            gpsReady = true,
            roadsBuilt = true,
            buildingsRingReady = true,
            tilesLoaded = true,
            depotRequired = false,
            depotBuilt = false
        };

        // Free drive never places a depot, so it must not hold the overlay open.
        Assert.IsTrue(WorldReadiness.Evaluate(milestones).ready);

        milestones.depotRequired = true;
        var waiting = WorldReadiness.Evaluate(milestones);
        Assert.IsFalse(waiting.ready);
        Assert.AreEqual("PLACING BUS DEPOT...", waiting.status);
        StringAssert.Contains("depot", WorldReadiness.DescribePending(milestones));

        milestones.depotBuilt = true;
        Assert.IsTrue(WorldReadiness.Evaluate(milestones).ready);
        Assert.AreEqual("none", WorldReadiness.DescribePending(milestones));
    }

    [Test]
    public void WorldReadiness_NeverReportsFullUntilTheWorldIsPlayable()
    {
        // Everything except the near-field warm-up is done, and the warm-up itself
        // reports a full internal progress. The card must still not show 100%,
        // because the scene is not playable yet.
        var milestones = new WorldReadiness.Milestones
        {
            saveLoaded = true,
            audioReady = true,
            busSpawned = true,
            vehicleConfigured = true,
            mapInitialised = true,
            gpsReady = true,
            roadsBuilt = true,
            buildingsRingReady = true,
            tilesLoaded = true,
            preloadRequired = true,
            preloadReady = false,
            preloadProgress = 1f
        };

        var waiting = WorldReadiness.Evaluate(milestones);
        Assert.IsFalse(waiting.ready);
        Assert.Less(waiting.progress, 1f, "100% has to mean playable, not merely nearly there");

        milestones.depotRequired = true;
        milestones.depotBuilt = false;
        var depotWaiting = WorldReadiness.Evaluate(milestones);
        Assert.IsFalse(depotWaiting.ready);
        Assert.Less(depotWaiting.progress, 1f);
    }

    [Test]
    public void LoadingBar_HoldsOneMonotonicRunAcrossTheSceneHandOff()
    {
        // The scene load fills the first band and the world warm-up continues from
        // exactly that value: the player must never see 100% and then a new bar.
        Assert.Greater(LoadingScreenUI.SceneLoadStart, 0f);
        Assert.Greater(LoadingScreenUI.SceneLoadEnd, LoadingScreenUI.SceneLoadStart);
        Assert.AreEqual(LoadingScreenUI.SceneLoadEnd, LoadingScreenUI.WorldProgressToScreen(0f), 0.0001f);
        Assert.AreEqual(1f, LoadingScreenUI.WorldProgressToScreen(1f), 0.0001f);

        float previous = 0f;
        for (int i = 0; i <= 20; i++)
        {
            float screen = LoadingScreenUI.WorldProgressToScreen(i / 20f);
            Assert.GreaterOrEqual(screen, previous, "the hand-off must not step the bar backwards");
            previous = screen;
        }
    }

    [Test]
    public void BusInputLock_HoldsEveryPedalAtZeroUntilTheWorldIsPlayable()
    {
        var go = new GameObject("InputLockedBus");
        try
        {
            var bus = go.AddComponent<BusController>();
            bus.inputLocked = true;

            // A mobile control writes after the bus's own Update; the lock has to
            // refuse it at the source, not only in the next frame's Update.
            bus.SetMobileInput(1f, 1f, 1f);
            Assert.AreEqual(0f, bus.throttleInput);
            Assert.AreEqual(0f, bus.brakeInput);
            Assert.AreEqual(0f, bus.steerInput);

            bus.SetSteerInput(-1f);
            Assert.AreEqual(0f, bus.steerInput);

            bus.inputLocked = false;
            bus.SetMobileInput(0.6f, 0.2f, -0.5f);
            Assert.AreEqual(0.6f, bus.throttleInput, 0.0001f);
            Assert.AreEqual(0.2f, bus.brakeInput, 0.0001f);
            Assert.AreEqual(-0.5f, bus.steerInput, 0.0001f);
        }
        finally
        {
            Object.DestroyImmediate(go);
        }
    }

    // ── HUD fit around the driving controls ──────────────────────────────────

    [Test]
    public void HudFitter_CentresThePanelInTheBandLeftFreeByTheControls()
    {
        var placement = HudSafeZoneFitter.Resolve(1920f, 1060f, 780f, 420f, 420f, 24f, 360f, 150f);

        Assert.IsFalse(placement.lifted);
        Assert.AreEqual(24f, placement.offsetY, 0.001f);
        Assert.AreEqual(0f, placement.offsetX, 0.001f);
        Assert.GreaterOrEqual(placement.width, 780f);
        Assert.LessOrEqual(placement.width, 1060f);

        // The scaled panel must stay clear of both control clusters.
        float half = placement.width * 0.5f;
        Assert.GreaterOrEqual(960f - half, 420f, "panel reaches into the steering cluster");
        Assert.LessOrEqual(960f + half, 1920f - 420f, "panel reaches into the pedal cluster");
    }

    [Test]
    public void HudFitter_OffsetsTowardTheWiderControlClusterWhenReservesDiffer()
    {
        var placement = HudSafeZoneFitter.Resolve(1920f, 1060f, 700f, 500f, 300f, 24f, 360f, 150f);
        Assert.AreEqual(100f, placement.offsetX, 0.001f);
    }

    [Test]
    public void HudFitter_LiftsAboveTheControlsWhenTheFreeBandIsTooNarrow()
    {
        var narrow = HudSafeZoneFitter.Resolve(1500f, 1060f, 780f, 420f, 420f, 24f, 360f, 150f);

        Assert.IsTrue(narrow.lifted, "panel should lift when it cannot sit between the clusters");
        Assert.AreEqual(360f, narrow.offsetY, 0.001f);
        Assert.AreEqual(0f, narrow.offsetX, 0.001f);
        Assert.LessOrEqual(narrow.width, 1500f - 300f, "lifted panel must respect the edge margin");

        var veryNarrow = HudSafeZoneFitter.Resolve(1200f, 1060f, 780f, 420f, 420f, 24f, 360f, 150f);
        Assert.IsTrue(veryNarrow.lifted);
        Assert.Less(veryNarrow.width, narrow.width);
        Assert.Less(veryNarrow.scale, 1f, "children must be scaled down with the panel");
    }

    // ── Door lock interlock ─────────────────────────────────────────────────────

    [Test]
    public void DoorLockInterlock_WaitsForTheDoorwayButNeverWaitsForever()
    {
        // Doorway clear: lock straight away.
        Assert.IsFalse(PassengerManager.ShouldWaitForDoorwayClear(false, 0f, 8f));

        // Someone is still stepping through: hold the lock.
        Assert.IsTrue(PassengerManager.ShouldWaitForDoorwayClear(true, 0.5f, 8f));
        Assert.IsTrue(PassengerManager.ShouldWaitForDoorwayClear(true, 7.9f, 8f));

        // Stuck animation must never leave the player unable to depart.
        Assert.IsFalse(PassengerManager.ShouldWaitForDoorwayClear(true, 8f, 8f));
        Assert.IsFalse(PassengerManager.ShouldWaitForDoorwayClear(true, 12f, 8f));

        // A zero timeout disables the wait entirely.
        Assert.IsFalse(PassengerManager.ShouldWaitForDoorwayClear(true, 0f, 0f));
    }

    // ── Imported models with missing textures ────────────────────────────────

    [Test]
    public void ImportedFallback_RepaintsUntexturedWhiteOnlyAndRegistersTheHierarchy()
    {
        Shader shader = BuiltInShaders.Lit;
        Assert.IsNotNull(shader, "No lit shader is available for the fallback material.");

        var root = new GameObject("ImportedProp");
        var artworkTexture = new Texture2D(4, 4);
        try
        {
            var prop = GameObject.CreatePrimitive(PrimitiveType.Cube);
            prop.transform.SetParent(root.transform, false);
            var renderer = prop.GetComponent<Renderer>();

            // How the FBX importer stores a material whose texture file is
            // missing: white albedo with zero alpha, which an opaque shader
            // renders as flat white.
            var missingTexture = new Material(shader) { color = new Color(1f, 1f, 1f, 0f) };
            renderer.sharedMaterials = new[] { missingTexture };
            Assert.IsTrue(ImportedMaterialPalette.IsUntexturedWhite(missingTexture));

            // Real artwork must never be overridden.
            var painted = new Material(shader) { color = Color.white };
            painted.SetTexture("_MainTex", artworkTexture);
            Assert.IsFalse(ImportedMaterialPalette.IsUntexturedWhite(painted));

            // Deliberate transparency (glass) is not a white shell.
            var glass = new Material(shader) { color = new Color(1f, 1f, 1f, 0.35f) };
            Assert.IsFalse(ImportedMaterialPalette.IsUntexturedWhite(glass));

            ImportedMaterialPalette.ClearRegisteredRoots();
            ImportedMaterialPalette.RegisterImportedRoot(root.transform, ImportedMaterialPalette.DefaultPropColor);
            Assert.AreEqual(1, ImportedMaterialPalette.RegisteredRootCount);

            int repainted = ImportedMaterialPalette.RepairRegisteredRoots();
            Assert.AreEqual(1, repainted);

            Material applied = renderer.sharedMaterial;
            Assert.IsFalse(ImportedMaterialPalette.IsUntexturedWhite(applied));
            Assert.AreEqual(1f, applied.color.a, 0.001f, "fallback material must be opaque");
            Assert.AreNotSame(missingTexture, applied);
        }
        finally
        {
            ImportedMaterialPalette.ClearRegisteredRoots();
            Object.DestroyImmediate(artworkTexture);
            Object.DestroyImmediate(root);
        }
    }

    [Test]
    public void ImportedFallback_LeavesASceneWithoutWhiteMaterialsUntouched()
    {
        Shader shader = BuiltInShaders.Lit;
        Assert.IsNotNull(shader, "No lit shader is available for the fallback material.");

        var root = new GameObject("AuthoredProp");
        var texture = new Texture2D(4, 4);
        try
        {
            var prop = GameObject.CreatePrimitive(PrimitiveType.Cube);
            prop.transform.SetParent(root.transform, false);
            var renderer = prop.GetComponent<Renderer>();

            var textured = new Material(shader) { color = new Color(0.2f, 0.4f, 0.6f, 1f) };
            textured.SetTexture("_MainTex", texture);
            renderer.sharedMaterials = new[] { textured };

            int repainted = ImportedMaterialPalette.ApplyFallback(root, ImportedMaterialPalette.DefaultPropColor);

            Assert.AreEqual(0, repainted);
            Assert.AreSame(textured, renderer.sharedMaterial);
        }
        finally
        {
            Object.DestroyImmediate(texture);
            Object.DestroyImmediate(root);
        }
    }

    // ── Responsive modal fit (safe area + screen shape) ──────────────────────

    [Test]
    public void ResponsiveModal_LeavesAPanelThatAlreadyFitsAtItsAuthoredSize()
    {
        var fit = ResponsiveModalFitter.Resolve(new Vector2(1920f, 1080f), new Vector2(960f, 880f),
            Vector4.zero, 24f, 0.5f);

        // A roomy landscape display must never scale the panel up.
        Assert.AreEqual(1f, fit.scale, 0.0001f);
        Assert.AreEqual(0f, fit.offset.x, 0.0001f);
        Assert.AreEqual(0f, fit.offset.y, 0.0001f);
    }

    [Test]
    public void ResponsiveModal_ScalesDownToFitAPortraitPhoneCanvas()
    {
        // A 1080x2340 phone produces a ~978-unit-wide canvas, narrower than the
        // 960-unit settings panel once the edge margin is taken off.
        var canvas = new Vector2(978f, 2119f);
        var design = new Vector2(960f, 880f);
        var fit = ResponsiveModalFitter.Resolve(canvas, design, Vector4.zero, 24f, 0.5f);

        Assert.Less(fit.scale, 1f, "a portrait canvas is narrower than the authored panel");
        Assert.Greater(fit.scale, 0.5f, "this common shape must stay above the shrink floor");
        Assert.LessOrEqual(design.x * fit.scale, canvas.x - 48f + 0.01f, "panel must clear the edge margin");
        Assert.AreEqual(0f, fit.offset.x, 0.0001f);
    }

    [Test]
    public void ResponsiveModal_RecentresThePanelInsideANotchedSafeArea()
    {
        // A left camera cut-out of 60 units and an 80-unit status bar at the top.
        var fit = ResponsiveModalFitter.Resolve(new Vector2(1920f, 1080f), new Vector2(960f, 880f),
            new Vector4(60f, 0f, 0f, 80f), 24f, 0.5f);

        Assert.AreEqual(1f, fit.scale, 0.0001f);
        // Shift right of the left cut-out and below the top bar.
        Assert.AreEqual(30f, fit.offset.x, 0.0001f);
        Assert.AreEqual(-40f, fit.offset.y, 0.0001f);
    }

    [Test]
    public void ResponsiveModal_NeverShrinksBelowTheUsabilityFloor()
    {
        var fit = ResponsiveModalFitter.Resolve(new Vector2(400f, 400f), new Vector2(960f, 880f),
            Vector4.zero, 24f, 0.5f);

        Assert.AreEqual(0.5f, fit.scale, 0.0001f, "the shrink floor should hold on a tiny canvas");
    }

    [Test]
    public void ResponsiveModal_FitsEveryRepresentativeScreenShape()
    {
        var design = new Vector2(960f, 880f);
        var insets = new Vector4(36f, 24f, 12f, 48f);
        const float margin = 24f;
        const float floor = 0.5f;

        // Landscape 16:9, landscape 4:3, square, portrait 9:19.5, and a small
        // tablet-shaped viewport.
        var shapes = new[]
        {
            new Vector2(1920f, 1080f),
            new Vector2(1663f, 1247f),
            new Vector2(1080f, 1080f),
            new Vector2(978f, 2119f),
            new Vector2(1200f, 900f)
        };

        foreach (var canvas in shapes)
        {
            var fit = ResponsiveModalFitter.Resolve(canvas, design, insets, margin, floor);

            Assert.LessOrEqual(fit.scale, 1f, $"panel scaled up on {canvas}");
            Assert.GreaterOrEqual(fit.scale, floor - 0.0001f, $"panel shrank past the floor on {canvas}");

            // Above the floor the scaled panel must actually fit the visible area.
            float availableWidth = canvas.x - insets.x - insets.z - margin * 2f;
            float availableHeight = canvas.y - insets.y - insets.w - margin * 2f;
            if (fit.scale > floor + 0.0001f)
            {
                Assert.LessOrEqual(design.x * fit.scale, availableWidth + 0.01f, $"panel overflows width on {canvas}");
                Assert.LessOrEqual(design.y * fit.scale, availableHeight + 0.01f, $"panel overflows height on {canvas}");
            }
        }
    }
}
