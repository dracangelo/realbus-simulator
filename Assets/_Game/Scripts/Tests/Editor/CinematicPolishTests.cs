using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Covers the polish pass around the intro: the loading card's timing, the ambient
/// and reflection rules that follow the clock, and the title settle. Everything here
/// is decidable without a frame, which is the point - a lighting change that only
/// looks right at one hour of the day is a bug that cannot be caught on screen.
/// </summary>
public class CinematicPolishTests
{
    // ── Loading card ───────────────────────────────────────────────────

    [Test]
    public void LoadingCard_BarsOpenWithTheCardAndNeverExceedTheFrame()
    {
        Assert.That(CinematicLoadingCard.BarFraction(0f), Is.EqualTo(0f),
            "a closed card has no bars on screen");
        Assert.That(CinematicLoadingCard.BarFraction(1f), Is.EqualTo(CinematicLoadingCard.LetterboxFraction).Within(0.0001f),
            "an open card frames the screen at the authored letterbox height");
        Assert.That(CinematicLoadingCard.BarFraction(0.5f), Is.LessThan(CinematicLoadingCard.BarFraction(1f)));

        for (float t = -2f; t <= 2f; t += 0.1f)
            Assert.That(CinematicLoadingCard.BarFraction(t), Is.InRange(0f, CinematicLoadingCard.LetterboxFraction),
                $"a bar must stay a fraction of the screen at t={t}");

        Assert.That(CinematicLoadingCard.LetterboxFraction * 2f, Is.LessThan(0.5f),
            "two bars must never eat the whole frame");
    }

    [Test]
    public void LoadingCard_TipCrossfadesAtTheWraponlyAndIsSolidInBetween()
    {
        const float cycle = 7f, fade = 1.1f;

        Assert.That(CinematicLoadingCard.TipAlpha(0f, cycle, fade), Is.EqualTo(0f).Within(0.001f),
            "a tip starts dark and fades up");
        Assert.That(CinematicLoadingCard.TipAlpha(cycle * 0.5f, cycle, fade), Is.EqualTo(1f),
            "the middle of the cycle is fully legible");
        Assert.That(CinematicLoadingCard.TipAlpha(cycle + 0.001f, cycle, fade), Is.EqualTo(0f).Within(0.01f),
            "the wrap point is where the text is swapped, so it has to be dark there");

        float previous = -1f;
        for (float t = 0f; t <= fade; t += 0.05f)
        {
            float alpha = CinematicLoadingCard.TipAlpha(t, cycle, fade);
            Assert.That(alpha, Is.GreaterThanOrEqualTo(previous), "the fade-in never runs backwards");
            previous = alpha;
        }

        for (float t = 0f; t <= cycle * 2f; t += 0.1f)
            Assert.That(CinematicLoadingCard.TipAlpha(t, cycle, fade), Is.InRange(0f, 1f));

        Assert.That(CinematicLoadingCard.TipAlpha(3f, 0f, fade), Is.EqualTo(1f),
            "a card with no cycle must not fade its only tip out");
    }

    [TestCase(0f, "0%")]
    [TestCase(0.5f, "50%")]
    [TestCase(1f, "100%")]
    public void LoadingCard_ProgressReadsAsAPercentage(float value, string expected)
    {
        Assert.That(CinematicLoadingCard.FormatProgress(value), Is.EqualTo(expected));
    }

    [Test]
    public void LoadingCard_ProgressIsClampedRatherThanPrintingNonsense()
    {
        Assert.That(CinematicLoadingCard.FormatProgress(-4f), Is.EqualTo("0%"));
        Assert.That(CinematicLoadingCard.FormatProgress(9f), Is.EqualTo("100%"));
    }

    [Test]
    public void LoadingCard_BuildsItsFrameAndNamesTheCity()
    {
        var canvasGo = new GameObject("LoadingCardTest Canvas", typeof(Canvas));
        try
        {
            CinematicLoadingCard card = CinematicLoadingCard.Create(canvasGo.transform);

            Assert.That(card.transform.Find("Top Bar"), Is.Not.Null, "the frame needs its top bar");
            Assert.That(card.transform.Find("Bottom Bar"), Is.Not.Null, "and its bottom bar");
            Assert.That(card.transform.Find("Background/Vignette"), Is.Not.Null,
                "the edge falloff is what makes the card read as a frame instead of a panel");
            Assert.That(card.transform.Find("Background/Content/Title"), Is.Not.Null);
            Assert.That(card.transform.Find("Background/Content/Progress Track/Progress Fill"), Is.Not.Null);
            Assert.That(card.transform.Find("Background/Content/Progress Track/Progress Head"), Is.Not.Null,
                "the progress bar needs a moving glint so it reads as a direction, not a static rule");
            Assert.That(card.transform.Find("Background/Content/Route"), Is.Not.Null,
                "the route lower-third identifies the service being loaded");

            card.SetTitle("nairobi");
            Assert.That(card.Title, Is.EqualTo("NAIROBI"), "the card titles the city it is about to show");
            card.SetTitle(null);
            Assert.That(card.Title, Is.EqualTo("REALBUS"), "a missing city name must not leave an empty card");

            card.SetRoute("NBO-8", "NBO CBD - Westlands");
            Assert.That(card.RouteLine, Does.Contain("NBO-8"));
            Assert.That(card.RouteLine, Does.Contain("CBD - WESTLANDS"),
                "the route lower-third drops the duplicated NBO prefix and uppercases the destination");
            card.SetRoute("NBO-8", null);
            Assert.That(card.RouteLine, Is.EqualTo("NBO-8"),
                "a route code without a destination must not leave a dangling separator");
            card.SetRoute(null, null);
            Assert.That(card.RouteLine, Is.Empty, "no selected route hides the lower-third instead of leaving a placeholder");

            card.SetProgress(0.42f);
            Assert.That(card.Progress, Is.EqualTo(0.42f).Within(0.001f));

            card.Reveal(instant: true);
            Assert.That(card.IsOpen, Is.True);
            Assert.That(card.gameObject.activeSelf, Is.True);

            card.Dismiss(instant: true);
            Assert.That(card.IsDismissed, Is.True);
            Assert.That(card.gameObject.activeSelf, Is.False, "a dismissed card stops drawing entirely");
        }
        finally
        {
            Object.DestroyImmediate(canvasGo);
        }
    }

    [Test]
    public void NearFieldWarmup_PrewarmsProceduralSurfacesAndBanners()
    {
        UrbanMaterials.Warmup();
        BusStopPosterBuilder.Prewarm();
        StreetTextureLibrary.Prewarm();

        Assert.That(UrbanMaterials.WarmupComplete, Is.True);
        Assert.That(UrbanMaterials.Road, Is.Not.Null, "the shared asphalt material must exist before roads are built");
        Assert.That(UrbanMaterials.Road.mainTexture, Is.Not.Null, "the generated asphalt albedo must be ready");
        Assert.That(BusStopPosterBuilder.IsPrewarmed, Is.True,
            "poster textures should be generated under the loading card, not at the first stop");
        Assert.That(StreetTextureLibrary.IsPrewarmed, Is.True,
            "billboard and bench surfaces must be ready before the first street prop streams in");
    }

    [Test]
    public void StreetTextures_ProvideMultipleStableVariantsForEveryProp()
    {
        StreetTextureLibrary.Prewarm();

        var landscape = new HashSet<int>();
        var portrait = new HashSet<int>();
        var benches = new HashSet<int>();
        for (uint seed = 0; seed < 64; seed++)
        {
            landscape.Add(StreetTextureLibrary.PickVariant(seed, StreetTextureLibrary.BillboardVariantCount));
            portrait.Add(StreetTextureLibrary.PickVariant(seed + 900u, StreetTextureLibrary.BillboardVariantCount));
            benches.Add(StreetTextureLibrary.PickVariant(seed + 1800u, StreetTextureLibrary.BenchVariantCount));
        }
        Assert.That(landscape.Count, Is.GreaterThan(3), "neighbouring billboards need different ad designs");
        Assert.That(portrait.Count, Is.GreaterThan(3));
        Assert.That(benches.Count, Is.EqualTo(StreetTextureLibrary.BenchVariantCount),
            "every generated bench surface should be reachable by the seed chooser");
        Assert.That(StreetTextureLibrary.PickVariant(42u, 10),
            Is.EqualTo(StreetTextureLibrary.PickVariant(42u, 10)),
            "a prop that streams out and back must keep the same design");

        for (int i = 0; i < StreetTextureLibrary.BillboardVariantCount; i++)
        {
            Texture2D landscapeTexture = StreetTextureLibrary.GetBillboardTexture(i, false);
            Texture2D portraitTexture = StreetTextureLibrary.GetBillboardTexture(i, true);
            Assert.That(landscapeTexture, Is.Not.Null);
            Assert.That(portraitTexture, Is.Not.Null);
            Assert.That(landscapeTexture.width, Is.GreaterThan(landscapeTexture.height));
            Assert.That(portraitTexture.height, Is.GreaterThan(portraitTexture.width));
            Assert.That(StreetTextureLibrary.GetBillboardMaterial(i, false).mainTexture,
                Is.SameAs(landscapeTexture));
            Assert.That(StreetTextureLibrary.GetBillboardMaterial(i, true).mainTexture,
                Is.SameAs(portraitTexture));
        }

        for (int i = 0; i < StreetTextureLibrary.BenchVariantCount; i++)
            Assert.That(StreetTextureLibrary.GetBenchMaterial(i).mainTexture, Is.Not.Null);
    }

    [Test]
    public void StreetTextures_DecorateABillboardWithTexturedFacesAndNoColliders()
    {
        var root = new GameObject("Billboard decoration test");
        var panel = GameObject.CreatePrimitive(PrimitiveType.Quad);
        panel.name = "SourcePanel";
        panel.transform.SetParent(root.transform, false);
        panel.transform.localScale = new Vector3(3.5f, 2.4f, 1f);
        // The streamer yaws every billboard to face the road. A 45°-ish road must
        // still resolve as a flat panel, not as a thick box.
        panel.transform.rotation = Quaternion.Euler(0f, 37f, 0f);

        try
        {
            int faces = StreetTextureLibrary.DecorateBillboard(root, 12345u);
            Assert.That(faces, Is.EqualTo(1), "one flat source panel is one poster face");

            int ads = 0;
            foreach (Renderer renderer in root.GetComponentsInChildren<Renderer>(true))
            {
                if (!renderer.name.StartsWith("Billboard_Advertisement")) continue;
                ads++;
                Assert.That(renderer.sharedMaterial, Is.Not.Null);
                Assert.That(renderer.sharedMaterial.mainTexture, Is.Not.Null,
                    "the overlay must be a real textured advertisement");
                Assert.That(renderer.GetComponent<Collider>(), Is.Null,
                    "decorative quads must not add physics colliders");
            }
            Assert.That(ads, Is.EqualTo(2), "a two-sided panel needs a readable face on each side");
            Assert.That(panel.GetComponent<Renderer>().sharedMaterial.mainTexture, Is.Null,
                "the source body becomes a frame; the ad lives on the overlay");
        }
        finally
        {
            Object.DestroyImmediate(root);
        }
    }

    [Test]
    public void StreetTextures_FlatTexturedBillboardFaceIsCoveredNotLeftOrange()
    {
        var root = new GameObject("Textured billboard decoration test");
        var panel = GameObject.CreatePrimitive(PrimitiveType.Quad);
        panel.name = "SourcePanel";
        panel.transform.SetParent(root.transform, false);
        panel.transform.localScale = new Vector3(4f, 2.5f, 1f);

        var sourceMaterial = new Material(panel.GetComponent<Renderer>().sharedMaterial)
        {
            hideFlags = HideFlags.DontSave
        };
        var flatOrange = new Texture2D(8, 8, TextureFormat.RGBA32, false)
        {
            hideFlags = HideFlags.DontSave
        };
        var orangePixels = new Color32[flatOrange.width * flatOrange.height];
        for (int i = 0; i < orangePixels.Length; i++) orangePixels[i] = new Color32(214, 92, 20, 255);
        flatOrange.SetPixels32(orangePixels);
        flatOrange.Apply(false, false);
        sourceMaterial.mainTexture = flatOrange;
        panel.GetComponent<Renderer>().sharedMaterial = sourceMaterial;

        try
        {
            int faces = StreetTextureLibrary.DecorateBillboard(root, 99u);
            Assert.That(faces, Is.EqualTo(1), "the textured panel is still a poster face");

            Renderer source = panel.GetComponent<Renderer>();
            Assert.That(source.sharedMaterial, Is.SameAs(StreetTextureLibrary.BillboardFrame),
                "a flat placeholder texture must become the steel frame, not show around the ad");
            Assert.That(source.sharedMaterial.mainTexture, Is.Null);
        }
        finally
        {
            Object.DestroyImmediate(root);
            Object.DestroyImmediate(sourceMaterial);
            Object.DestroyImmediate(flatOrange);
        }
    }

    // ── Reflections ────────────────────────────────────────────────────

    [Test]
    public void Reflection_IntensityDimsAtNightButDoesNotDisappear()
    {
        float day = SkyReflectionEnvironment.IntensityFor(1f, 0.55f, 0.2f);
        float night = SkyReflectionEnvironment.IntensityFor(0f, 0.55f, 0.2f);

        Assert.That(day, Is.EqualTo(0.55f).Within(0.001f));
        Assert.That(night, Is.EqualTo(0.2f).Within(0.001f));
        Assert.That(night, Is.GreaterThan(0f), "a night street still mirrors the moon and the lamps");
        Assert.That(SkyReflectionEnvironment.IntensityFor(0.5f, 0.55f, 0.2f), Is.InRange(0.2f, 0.55f));
        Assert.That(SkyReflectionEnvironment.IntensityFor(-3f, 0.55f, 0.2f), Is.EqualTo(0.2f));
        Assert.That(SkyReflectionEnvironment.IntensityFor(4f, 0.55f, 0.2f), Is.EqualTo(0.55f));
    }

    [Test]
    public void Reflection_TintGoesColdAtNightAndWarmAtGoldenHour()
    {
        Color goldenSky = new Color(0.95f, 0.55f, 0.22f);
        Color noonSky = new Color(0.52f, 0.74f, 0.96f);

        Color golden = SkyReflectionEnvironment.ReflectionTint(goldenSky, 1f);
        Color noon = SkyReflectionEnvironment.ReflectionTint(noonSky, 1f);
        Color night = SkyReflectionEnvironment.ReflectionTint(noonSky, 0f);

        Assert.That(golden.r - golden.b, Is.GreaterThan(noon.r - noon.b),
            "a golden hour has to reach the reflections, or the paint and glass stay daylight grey");
        Assert.That(Luminance(night), Is.LessThan(Luminance(noon) * 0.5f), "night reflections are dim");
        Assert.That(night.b, Is.GreaterThan(night.r), "and cold, because a dimmed orange sky reads as brown");
    }

    [Test]
    public void Reflection_AmbientBlendsToANightFloorAndKeepsTheSkyTint()
    {
        Color dayAmbient = new Color(0.52f, 0.48f, 0.43f);
        Color nightFloor = new Color(0.04f, 0.05f, 0.09f);

        Assert.That(SkyReflectionEnvironment.AmbientFor(dayAmbient, nightFloor, Color.white, 1f).r,
            Is.EqualTo(dayAmbient.r).Within(0.001f), "a white sky leaves the authored daytime colour alone");

        Color night = SkyReflectionEnvironment.AmbientFor(dayAmbient, nightFloor, Color.white, 0f);
        Assert.That(Luminance(night), Is.EqualTo(Luminance(nightFloor)).Within(0.001f),
            "after dark the floor is a colour, not an absence of light");

        Color warm = SkyReflectionEnvironment.AmbientFor(dayAmbient, nightFloor, new Color(1f, 0.6f, 0.3f), 1f);
        Assert.That(warm.r - warm.b, Is.GreaterThan(dayAmbient.r - dayAmbient.b),
            "a sunrise sky has to warm the bounce light with it");
    }

    [Test]
    public void Reflection_CubemapIsBrightAboveAndDarkBelow()
    {
        Cubemap cube = SkyReflectionEnvironment.Build(Color.white, 1f, Vector3.up, 8);
        try
        {
            Assert.That(cube.width, Is.EqualTo(8));
            Assert.That(cube.height, Is.EqualTo(8));

            float up = AverageLuminance(cube, CubemapFace.PositiveY);
            float down = AverageLuminance(cube, CubemapFace.NegativeY);

            Assert.That(up, Is.GreaterThan(Luminance(Color.white) * 0.5f),
                "the sky face is the colour of the sky");
            Assert.That(down, Is.LessThan(up * 0.5f),
                "the ground half of the cube stays dark, so a reflection never lights a surface from below");
        }
        finally
        {
            Object.DestroyImmediate(cube);
        }
    }

    [Test]
    public void Reflection_CubemapPutsASunLobeWhereTheSunIs()
    {
        Cubemap cube = SkyReflectionEnvironment.Build(Color.white, 1f, new Vector3(1f, 0f, 0f), 12);
        try
        {
            float towardSun = AverageLuminance(cube, CubemapFace.PositiveX);
            float away = AverageLuminance(cube, CubemapFace.NegativeX);

            Assert.That(towardSun, Is.GreaterThan(away),
                "the face pointing at the sun has to be brighter, or wet asphalt never catches a glint");
        }
        finally
        {
            Object.DestroyImmediate(cube);
        }
    }

    [Test]
    public void Reflection_RebuildsWhenTheSkyMovesButNotOnEveryFrame()
    {
        Color tint = SkyReflectionEnvironment.ReflectionTint(Color.white, 1f);
        Cubemap cube = SkyReflectionEnvironment.Build(tint, 1f, Vector3.up, 4);
        try
        {
            SkyReflectionEnvironment.Adopt(cube);

            Assert.That(SkyReflectionEnvironment.ShouldRebuild(tint, 1f, tint, 1f), Is.False,
                "an unchanged sky must not rebuild a 16-pixel cubemap several times a second");
            Assert.That(SkyReflectionEnvironment.ShouldRebuild(tint, 1f, tint, 0.4f), Is.True,
                "a moved sun has to rebuild it, or dusk keeps a noon reflection");
            Assert.That(SkyReflectionEnvironment.ShouldRebuild(tint, 1f, new Color(0.9f, 0.5f, 0.2f), 1f), Is.True,
                "a golden-hour tint has to rebuild it too");
        }
        finally
        {
            SkyReflectionEnvironment.Release();
        }
    }

    // ── Ambient lighting and the intro ─────────────────────────────────

    [Test]
    public void Atmosphere_GoldenHourKeepsItsLightAndNightStillFalls()
    {
        Assert.That(CinematicAtmosphereController.AmbientLightLevel(45f), Is.EqualTo(1f).Within(0.001f),
            "a high sun is full ambient");
        Assert.That(CinematicAtmosphereController.AmbientLightLevel(18f), Is.EqualTo(1f).Within(0.001f),
            "the sun at 18 degrees is a late afternoon, not a dusk: this is the hour the depot shot is framed in");
        Assert.That(CinematicAtmosphereController.AmbientLightLevel(0f), Is.InRange(0.2f, 0.6f),
            "the horizon keeps a third of the light, which is what makes golden hour golden");
        Assert.That(CinematicAtmosphereController.AmbientLightLevel(-20f), Is.EqualTo(0f).Within(0.001f),
            "well below the horizon is night");

        float evening = CinematicAtmosphereController.AmbientIntensityFor(
            CinematicAtmosphereController.AmbientLightLevel(17.6f), 1.05f, 0.28f);
        Assert.That(evening, Is.GreaterThan(1f),
            "an evening shot must not be lit like a night shot: the sine-based daylight fraction put it at 0.48");

        float previous = 2f;
        for (float elevation = 20f; elevation >= -20f; elevation -= 1f)
        {
            float level = CinematicAtmosphereController.AmbientLightLevel(elevation);
            Assert.That(level, Is.InRange(0f, 1f));
            Assert.That(level, Is.LessThanOrEqualTo(previous), "light only falls as the sun goes down");
            previous = level;
        }
    }

    [Test]
    public void Atmosphere_AmbientIntensityFollowsDaylightWithANightFloor()
    {
        Assert.That(CinematicAtmosphereController.AmbientIntensityFor(1f, 1.05f, 0.28f),
            Is.EqualTo(1.05f).Within(0.001f));
        Assert.That(CinematicAtmosphereController.AmbientIntensityFor(0f, 1.05f, 0.28f),
            Is.EqualTo(0.28f).Within(0.001f), "a night scene must not be lit like noon");
        Assert.That(CinematicAtmosphereController.AmbientIntensityFor(0.5f, 1.05f, 0.28f), Is.InRange(0.28f, 1.05f));
        Assert.That(CinematicAtmosphereController.AmbientIntensityFor(-1f, 1.05f, 0.28f), Is.EqualTo(0.28f));
        Assert.That(CinematicAtmosphereController.AmbientIntensityFor(2f, 1.05f, 0.28f), Is.EqualTo(1.05f));
        Assert.That(CinematicAtmosphereController.AmbientIntensityFor(0f, 1.05f, 0.28f), Is.GreaterThan(0f),
            "zero ambient makes a shaded wall unreadable, not moody");
    }

    [Test]
    public void Sky_ExposureHoldsThroughTheAfternoonAndOnlyFallsBelowTheHorizon()
    {
        Assert.That(SkyEnvironmentController.SkyLightLevelFor(60f), Is.EqualTo(1f).Within(0.001f));
        Assert.That(SkyEnvironmentController.SkyLightLevelFor(17.6f), Is.EqualTo(1f).Within(0.001f),
            "a sun 18 degrees up still lights the sky: this is the hour the depot shot is framed in");
        Assert.That(SkyEnvironmentController.SkyLightLevelFor(0f), Is.InRange(0.2f, 0.7f),
            "the horizon is dimmer than the afternoon but nowhere near night");
        Assert.That(SkyEnvironmentController.SkyLightLevelFor(-15f), Is.EqualTo(0f).Within(0.001f));

        float duskExposure = SkyEnvironmentController.SkyExposureFor(
            SkyEnvironmentController.SkyLightLevelFor(17.6f), 0f, 1.15f, 0.12f);
        Assert.That(duskExposure, Is.EqualTo(1.15f).Within(0.001f),
            "afternoon sky exposure must not be a quarter of noon's");

        float previous = 2f;
        for (float elevation = 20f; elevation >= -20f; elevation -= 1f)
        {
            float level = SkyEnvironmentController.SkyLightLevelFor(elevation);
            Assert.That(level, Is.InRange(0f, 1f));
            Assert.That(level, Is.LessThanOrEqualTo(previous));
            previous = level;
        }
    }

    [Test]
    public void Intro_TitleSettlesInsteadOfSnapping()
    {
        Assert.That(CinematicIntroSequence.TitleScale(0f, 0.9f), Is.EqualTo(1.05f).Within(0.001f),
            "the title starts slightly oversized");
        Assert.That(CinematicIntroSequence.TitleScale(0.9f, 0.9f), Is.EqualTo(1f).Within(0.001f));
        Assert.That(CinematicIntroSequence.TitleScale(2f, 0.9f), Is.EqualTo(1f).Within(0.001f),
            "and comes to rest at full size and stays there");
        Assert.That(CinematicIntroSequence.TitleScale(0f, 0f), Is.EqualTo(1f).Within(0.001f),
            "no settle time means no animation");

        float previous = 2f;
        for (float t = 0f; t <= 1f; t += 0.05f)
        {
            float scale = CinematicIntroSequence.TitleScale(t, 0.9f);
            Assert.That(scale, Is.LessThanOrEqualTo(previous), "the title only ever shrinks toward rest");
            Assert.That(scale, Is.InRange(1f, 1.05f));
            previous = scale;
        }
    }

    // ── Weather matching the light ─────────────────────────────────────

    [Test]
    public void WeatherLighting_CloudSoftensShadowsAndDimsAmbientAndReflections()
    {
        Assert.That(WeatherLighting.ShadowStrengthScale(0f), Is.EqualTo(1f).Within(0.001f),
            "a clear sky casts the full shadow");
        Assert.That(WeatherLighting.ShadowStrengthScale(0.9f), Is.LessThan(0.5f),
            "an overcast sky cannot cast a crisp shadow");
        Assert.That(WeatherLighting.ShadowStrengthScale(1f), Is.InRange(0.05f, 0.2f),
            "a storm keeps a trace of contact shadow so nothing looks like it is floating");

        Assert.That(WeatherLighting.AmbientScale(0f), Is.EqualTo(1f).Within(0.001f));
        Assert.That(WeatherLighting.AmbientScale(1f), Is.GreaterThanOrEqualTo(0.5f),
            "cloud scatters light rather than removing it, so a storm's ambient is dim but not black");

        Assert.That(WeatherLighting.ReflectionScale(0f), Is.EqualTo(1f).Within(0.001f));
        Assert.That(WeatherLighting.ReflectionScale(1f), Is.InRange(0.3f, 0.5f),
            "a grey ceiling reflects far less than a blue sky");

        Assert.That(WeatherLighting.SunScale(0f), Is.EqualTo(1f).Within(0.001f));
        Assert.That(WeatherLighting.SunScale(1f), Is.LessThan(0.25f), "and lets very little direct sun through");
    }

    [Test]
    public void WeatherLighting_MovesMonotonicallyWithCloudCover()
    {
        float previousShadow = 2f, previousAmbient = 2f, previousReflection = 2f, previousSun = 2f;
        for (float coverage = 0f; coverage <= 1f; coverage += 0.05f)
        {
            float shadow = WeatherLighting.ShadowStrengthScale(coverage);
            float ambient = WeatherLighting.AmbientScale(coverage);
            float reflection = WeatherLighting.ReflectionScale(coverage);
            float sun = WeatherLighting.SunScale(coverage);

            Assert.That(shadow, Is.InRange(0f, 1f));
            Assert.That(ambient, Is.InRange(0.5f, 1f));
            Assert.That(reflection, Is.InRange(0.3f, 1f));
            Assert.That(sun, Is.InRange(0.1f, 1f));

            Assert.That(shadow, Is.LessThanOrEqualTo(previousShadow), "more cloud never brightens a shadow");
            Assert.That(ambient, Is.LessThanOrEqualTo(previousAmbient));
            Assert.That(reflection, Is.LessThanOrEqualTo(previousReflection));
            Assert.That(sun, Is.LessThanOrEqualTo(previousSun));

            previousShadow = shadow; previousAmbient = ambient;
            previousReflection = reflection; previousSun = sun;
        }
    }

    [Test]
    public void WeatherLighting_DesaturatesTheReflectedSkyWithoutDarkeningIt()
    {
        Color blueSky = new Color(0.52f, 0.74f, 0.96f);

        Color clear = WeatherLighting.Desaturate(blueSky, 0f);
        Color overcast = WeatherLighting.Desaturate(blueSky, 1f);

        Assert.That(clear, Is.EqualTo(blueSky), "a clear sky keeps its colour in the reflection");
        Assert.That(ChannelSpread(overcast), Is.LessThan(ChannelSpread(clear) * 0.6f),
            "cloud pulls the reflection toward grey");
        Assert.That(Luminance(overcast), Is.EqualTo(Luminance(clear)).Within(0.02f),
            "desaturation is not dimming; the intensity carries that");
        Assert.That(overcast.b, Is.GreaterThan(overcast.r), "the residual tint stays cool, not brown");
    }

    [Test]
    public void Fog_OvercastDistanceIsGreyAndRainIsDarkerStill()
    {
        var go = new GameObject("FogLightingTest");
        var fog = go.AddComponent<FogController>();
        try
        {
            fog.SetFog(WeatherState.Overcast, 0.4f);
            Color overcast = fog.TargetFogColor;
            Assert.That(fog.WeatherFogActive, Is.True, "an overcast day has its own distance fog");
            Assert.That(Mathf.Abs(overcast.r - overcast.b), Is.LessThan(0.1f),
                "an overcast distance must lose the clear day's blue");

            fog.SetFog(WeatherState.HeavyRain, 1f);
            Color rain = fog.TargetFogColor;
            Assert.That(Luminance(rain), Is.LessThan(Luminance(overcast)),
                "rain has to read darker than cloud, or the two states look interchangeable");
            Assert.That(fog.TargetVisibility, Is.LessThan(fog.overcastVisibility),
                "and it has to close the distance in as well as darken it");
        }
        finally { Object.DestroyImmediate(go); }
    }

    static float ChannelSpread(Color color)
    {
        float max = Mathf.Max(color.r, Mathf.Max(color.g, color.b));
        float min = Mathf.Min(color.r, Mathf.Min(color.g, color.b));
        return max - min;
    }

    static float AverageLuminance(Cubemap cube, CubemapFace face)
    {
        Color[] pixels = cube.GetPixels(face);
        float total = 0f;
        for (int i = 0; i < pixels.Length; i++) total += Luminance(pixels[i]);
        return total / Mathf.Max(1, pixels.Length);
    }

    static float Luminance(Color color) => color.r * 0.299f + color.g * 0.587f + color.b * 0.114f;
}
