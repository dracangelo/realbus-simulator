using NUnit.Framework;
using TMPro;
using UnityEngine;

/// <summary>
/// Covers the interface localisation: the string table's completeness, the lookup
/// and fallback rules, the change notification, and the label binding that makes a
/// language change actually repaint the menus.
/// </summary>
public class LocalizationTests
{
    // ── Table completeness ───────────────────────────────────────────────────

    [Test]
    public void Localization_EveryKeyHasAStringInEverySupportedLanguage()
    {
        var missing = Localization.ValidateTable();

        Assert.IsEmpty(missing,
            "these keys are not fully translated: " + string.Join(", ", missing));
        Assert.AreEqual(5, Localization.SupportedLanguages.Length);
        Assert.GreaterOrEqual(Localization.KeyCount, 95, "the table lost entries");
    }

    [Test]
    public void Localization_EveryKeyUsedByTheMenusAndHudResolves()
    {
        // A key typo would render the raw key in game rather than failing loudly,
        // so assert that every key the UI actually asks for is present.
        string[] usedKeys =
        {
            // tabs
            "tab.controls", "tab.audio", "tab.display", "tab.accessibility",

            // setting cards
            "set.steeringScheme", "set.steeringScheme.desc",
            "set.drivingAssists", "set.drivingAssists.desc",
            "set.leftHanded", "set.leftHanded.desc",
            "set.touchSize", "set.touchSize.desc",
            "set.steeringResponse", "set.steeringResponse.desc",
            "set.steeringDeadZone", "set.steeringDeadZone.desc",
            "set.customLayout", "set.customLayout.desc", "set.customLayout.action",
            "set.masterVolume", "set.masterVolume.desc",
            "set.sfx", "set.sfx.desc",
            "set.music", "set.music.desc",
            "set.subtitles", "set.subtitles.desc",
            "set.haptics", "set.haptics.desc",
            "set.quality", "set.quality.desc",
            "set.framerate", "set.framerate.desc",
            "set.adaptive", "set.adaptive.desc",
            "set.reducedMotion", "set.reducedMotion.desc",
            "set.language", "set.language.desc",
            "set.largeText", "set.largeText.desc",
            "set.highContrast", "set.highContrast.desc",
            "set.colourBlind", "set.colourBlind.desc",
            "set.depot", "set.depot.desc", "set.depot.action",
            "set.credits", "set.credits.desc", "set.credits.action",

            // modal headers
            "hdr.settings.title", "hdr.settings.subtitle", "badge.depotOnline",
            "hdr.mobile.title", "hdr.mobile.subtitle", "badge.optimized",
            "hdr.pause.title", "hdr.pause.subtitle", "badge.standby",
            "hdr.downloads.title", "hdr.downloads.subtitle", "hdr.downloads.badge",
            "hdr.fleet.title", "hdr.fleet.subtitle", "hdr.fleet.badge",
            "hdr.credits.title", "hdr.credits.subtitle", "hdr.credits.badge",
            "hdr.daily.title", "hdr.daily.subtitle", "hdr.daily.badge",

            // buttons
            "btn.settings", "btn.stopClose", "btn.stopBack",
            "btn.depotDownloads", "btn.mobileOptimize", "btn.resumeService",
            "btn.operatorSettings", "btn.editControls", "btn.depotMainMenu",
            "btn.offlineMap", "btn.worldData", "btn.allRoutes", "btn.downloadRoute",
            "btn.cancel", "btn.removeCity", "btn.vehiclePacks", "btn.downloadInstall",
            "btn.removePacks", "btn.favourite", "btn.completeCity",
            "btn.driveChallenge", "btn.overtureDocs",

            // values, status copy and HUD
            "value.on", "value.off",
            "status.ready", "status.noCities", "status.loadingDaily",
            "hud.freeDrive", "hud.noSchedule", "hud.explore", "hud.routeNav"
        };

        foreach (string key in usedKeys)
            Assert.IsTrue(Localization.HasKey(key), $"the UI asks for a missing key: {key}");
    }

    // ── Lookup rules ─────────────────────────────────────────────────────────

    [Test]
    public void Localization_ReturnsTheStringForTheRequestedLanguage()
    {
        Assert.AreEqual("COMMANDES", Localization.Get(RealBusLanguage.French, "tab.controls"));
        Assert.AreEqual("VIDHIBITI", Localization.Get(RealBusLanguage.Swahili, "tab.controls"));
        Assert.AreEqual("STEUERUNG", Localization.Get(RealBusLanguage.German, "tab.controls"));
        Assert.AreEqual("CONTROLES", Localization.Get(RealBusLanguage.Spanish, "tab.controls"));

        // Every language must actually differ from English somewhere, otherwise the
        // selector would look like it does nothing for that language.
        foreach (var language in Localization.SupportedLanguages)
        {
            if (language == RealBusLanguage.English) continue;
            Assert.AreNotEqual(
                Localization.Get(RealBusLanguage.English, "tab.accessibility"),
                Localization.Get(language, "tab.accessibility"),
                $"no translated string for {language}");
        }
    }

    [Test]
    public void Localization_ShowsAnUnknownKeyInsteadOfRenderingBlank()
    {
        Assert.AreEqual("no.such.key", Localization.Get("no.such.key"));
        Assert.IsFalse(Localization.HasKey("no.such.key"));
        Assert.AreEqual(string.Empty, Localization.Get(string.Empty));
    }

    [Test]
    public void Localization_GetUsesTheActiveLanguage()
    {
        Localization.SetLanguage(RealBusLanguage.French);
        Assert.AreEqual("COMMANDES", Localization.Get("tab.controls"));

        Localization.SetLanguage(RealBusLanguage.English);
        Assert.AreEqual("CONTROLS", Localization.Get("tab.controls"));
    }

    // ── Change notification ──────────────────────────────────────────────────

    [Test]
    public void Localization_NotifiesListenersOnlyWhenTheLanguageActuallyChanges()
    {
        Localization.SetLanguage(RealBusLanguage.English);

        int notifications = 0;
        System.Action listener = () => notifications++;
        Localization.LanguageChanged += listener;
        try
        {
            Assert.IsTrue(Localization.SetLanguage(RealBusLanguage.German), "a real change must report true");
            Assert.AreEqual(1, notifications);
            Assert.AreEqual(RealBusLanguage.German, Localization.Current);

            // Re-applying the same language must not churn the UI.
            Assert.IsFalse(Localization.SetLanguage(RealBusLanguage.German), "no change must report false");
            Assert.AreEqual(1, notifications);
        }
        finally
        {
            Localization.LanguageChanged -= listener;
            Localization.SetLanguage(RealBusLanguage.English);
        }
    }

    // ── Label binding ────────────────────────────────────────────────────────

    [Test]
    public void LocalizedText_RepaintsItsLabelWhenTheLanguageChanges()
    {
        Localization.SetLanguage(RealBusLanguage.English);

        var canvas = new GameObject("LocalizationTestCanvas", typeof(RectTransform), typeof(Canvas));
        var labelObject = new GameObject("Label", typeof(RectTransform), typeof(TextMeshProUGUI));
        labelObject.transform.SetParent(canvas.transform, false);

        try
        {
            TextMeshProUGUI label = labelObject.GetComponent<TextMeshProUGUI>();
            LocalizedText binding = LocalizedText.Bind(labelObject, "hud.freeDrive");

            Assert.IsNotNull(binding);
            Assert.AreEqual("FREE DRIVE", label.text, "the binding must resolve the active language on attach");

            Localization.SetLanguage(RealBusLanguage.German);
            Assert.AreEqual("FREIE FAHRT", label.text, "the label did not follow the language change");

            Localization.SetLanguage(RealBusLanguage.Spanish);
            Assert.AreEqual("CONDUCCIÓN LIBRE", label.text, "the label did not follow a second change");

            Localization.SetLanguage(RealBusLanguage.English);
            Assert.AreEqual("FREE DRIVE", label.text, "the label did not return to English");
        }
        finally
        {
            Localization.SetLanguage(RealBusLanguage.English);
            Object.DestroyImmediate(labelObject);
            Object.DestroyImmediate(canvas);
        }
    }

    [Test]
    public void LocalizedText_StopsListeningOnceItIsDestroyed()
    {
        Localization.SetLanguage(RealBusLanguage.English);

        var canvas = new GameObject("LocalizationTestCanvas", typeof(RectTransform), typeof(Canvas));
        var labelObject = new GameObject("Label", typeof(RectTransform), typeof(TextMeshProUGUI));
        labelObject.transform.SetParent(canvas.transform, false);

        try
        {
            LocalizedText.Bind(labelObject, "hud.freeDrive");
            Object.DestroyImmediate(labelObject);

            // A leaked subscription would throw here, because the label is gone.
            Assert.DoesNotThrow(() => Localization.SetLanguage(RealBusLanguage.French));
        }
        finally
        {
            Localization.SetLanguage(RealBusLanguage.English);
            Object.DestroyImmediate(canvas);
        }
    }

    [Test]
    public void LocalizedText_BindsEverySettingCardKeyWithoutLeavingTheRawKeyOnScreen()
    {
        Localization.SetLanguage(RealBusLanguage.Swahili);

        var canvas = new GameObject("LocalizationTestCanvas", typeof(RectTransform), typeof(Canvas));
        var created = new System.Collections.Generic.List<GameObject>();
        try
        {
            foreach (string key in new[] { "set.language", "set.quality", "btn.stopClose", "tab.audio" })
            {
                var labelObject = new GameObject("Label", typeof(RectTransform), typeof(TextMeshProUGUI));
                labelObject.transform.SetParent(canvas.transform, false);
                created.Add(labelObject);

                LocalizedText.Bind(labelObject, key);
                string rendered = labelObject.GetComponent<TextMeshProUGUI>().text;

                Assert.AreNotEqual(key, rendered, $"the raw key '{key}' reached the screen");
                Assert.IsNotEmpty(rendered);
                Assert.AreEqual(Localization.Get(key), rendered);
            }
        }
        finally
        {
            Localization.SetLanguage(RealBusLanguage.English);
            foreach (var go in created) Object.DestroyImmediate(go);
            Object.DestroyImmediate(canvas);
        }
    }
}
