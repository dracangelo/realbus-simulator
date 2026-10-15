using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Covers the procedural audio the game actually plays, since the repository ships
/// no audio files at all. The interesting assertions are about <i>audio quality</i>
/// rather than output shape: a looping sample must hold its level across the wrap
/// and must not click, a band must stay inside the frequency range it was asked for,
/// and nothing may clip.
///
/// The thresholds below are not arbitrary. <see cref="EdgeWindow"/> is deliberately
/// 3% of the buffer rather than a fixed number of milliseconds: a window shorter than
/// one period of the clip's lowest partial measures the waveform's phase rather than
/// its level, which produced a bogus 0.18 reading on the 140 Hz cabin-air loop. At 3%
/// the window spans at least two periods of every partial in the library, and on that
/// metric the generator here measures 0.964 to 1.05 while the previous fade-in
/// fade-out generator measured 0.588. The 0.8 threshold sits 0.16 clear of both.
/// </summary>
public class AudioSynthesisTests
{
    /// <summary>
    /// Window used to read the level at each end of a loop: 3% of the buffer, which
    /// spans at least two periods of the lowest partial in every clip here.
    /// </summary>
    static int EdgeWindow(int length) => Mathf.Max(64, length * 3 / 100);

    static IEnumerable<ProceduralAudio.ClipSpec> Library() => ProceduralAudio.BuildFallbackLibrary();

    // ── The fallback set as a whole ──────────────────────────────────────────

    [Test]
    public void Audio_EveryFallbackClipIsAudibleAndUnclipped()
    {
        foreach (var spec in Library())
        {
            Assert.IsNotNull(spec.samples, $"{spec.key} produced no samples");
            Assert.Greater(spec.samples.Length, 128, $"{spec.key} is too short to be heard");

            float peak = ProceduralAudio.Peak(spec.samples);
            Assert.LessOrEqual(peak, 1f, $"{spec.key} clips");
            Assert.GreaterOrEqual(peak, 0.02f, $"{spec.key} is effectively silent");
            Assert.Greater(ProceduralAudio.Rms(spec.samples, 0, spec.samples.Length), 0.005f,
                $"{spec.key} carries no energy");
        }
    }

    [Test]
    public void Audio_FallbackLibraryCoversEveryClipTheAudioManagerAsksFor()
    {
        // The manager assigns clips by key in a switch. A key that does not exist in
        // the library would silently leave that sound null with no error anywhere.
        var required = new HashSet<string>
        {
            "engineIdle", "engineLoad", "tyreRoll", "tyreSqueal", "brakeSqueal",
            "airHiss", "uiClick", "dockingChime", "missionFanfare", "rankJingle",
            "collisionHit", "cityAmbience", "airConditioning"
        };

        var produced = new HashSet<string>();
        foreach (var spec in Library())
        {
            Assert.IsTrue(produced.Add(spec.key), $"duplicate library key '{spec.key}'");
            Assert.IsNotEmpty(spec.name);
        }

        foreach (string key in required)
            Assert.IsTrue(produced.Contains(key), $"the audio manager asks for missing clip '{key}'");
    }

    // ── Looping clips ────────────────────────────────────────────────────────

    [Test]
    public void Audio_LoopingClipsHoldTheirLevelRightAtTheLoopPoint()
    {
        // This is the defect the old generator had: a fade-in and fade-out on a clip
        // that is played on loop, so the engine dropped to near silence once per loop.
        foreach (var spec in Library())
        {
            if (!spec.loop) continue;

            float[] data = spec.samples;
            int window = EdgeWindow(data.Length);
            float overall = ProceduralAudio.Rms(data, 0, data.Length);
            Assert.Greater(overall, 0f, $"{spec.key} is silent");

            float leading = ProceduralAudio.Rms(data, 0, window) / overall;
            float trailing = ProceduralAudio.Rms(data, data.Length - window, window) / overall;

            Assert.GreaterOrEqual(leading, 0.8f,
                $"{spec.key} fades in at the loop start (level ratio {leading:0.###}) - it will pump");
            Assert.GreaterOrEqual(trailing, 0.8f,
                $"{spec.key} fades out at the loop end (level ratio {trailing:0.###}) - it will pump");
        }
    }

    [Test]
    public void Audio_LoopingClipsHaveNoDiscontinuityAtTheWrap()
    {
        foreach (var spec in Library())
        {
            if (!spec.loop) continue;

            float[] data = spec.samples;
            float seam = ProceduralAudio.LoopSeamStep(data);
            float maxStep = ProceduralAudio.MaxStep(data);

            // The step across the wrap must be an ordinary step, not an outlier spike.
            Assert.LessOrEqual(seam, maxStep * 1.5f,
                $"{spec.key} jumps {seam:0.#####} at the loop point but never more than {maxStep:0.#####} elsewhere - that is a click");
        }
    }

    [Test]
    public void Audio_BandNoiseKeepsItsEnergyInsideTheRequestedBand()
    {
        var bands = new[]
        {
            (low: 100f, high: 200f),
            (low: 55f,  high: 240f),
            (low: 90f,  high: 900f)
        };

        foreach (var band in bands)
        {
            float[] data = ProceduralAudio.BandNoise(1f, band.low, band.high, 0.5f, 7);
            float rate = ProceduralAudio.ZeroCrossingRate(data);

            // Zero crossings track roughly twice the dominant frequency, so a correct
            // band lands inside twice the band limits.
            Assert.GreaterOrEqual(rate, band.low * 2f * 0.75f,
                $"band {band.low}-{band.high} Hz has its energy far below the band ({rate:0} crossings/s)");
            Assert.LessOrEqual(rate, band.high * 2f * 1.25f,
                $"band {band.low}-{band.high} Hz has its energy far above the band ({rate:0} crossings/s)");
        }
    }

    [Test]
    public void Audio_EnginePitchRisesBetweenIdleAndLoad()
    {
        float[] idle = ProceduralAudio.DieselEngine(1f, 72f, 0.30f, 11);
        float[] load = ProceduralAudio.DieselEngine(1f, 126f, 0.26f, 23);

        float idleRate = ProceduralAudio.ZeroCrossingRate(idle);
        float loadRate = ProceduralAudio.ZeroCrossingRate(load);

        Assert.Greater(loadRate, idleRate * 1.3f,
            $"the load sample is not pitched above the idle sample ({idleRate:0} vs {loadRate:0} crossings/s)");
    }

    [Test]
    public void Audio_LoopFrequenciesSnapToAWholeNumberOfCycles()
    {
        // This is the property that lets a loop repeat without any fade: after
        // snapping, the buffer holds an exact number of cycles, so the waveform that
        // the last sample was drawing carries straight on into the first sample.
        int length = ProceduralAudio.LengthFor(1f);
        const float requested = 72.4f;

        float snapped = ProceduralAudio.SnapToLoop(requested, length);
        float cycles = snapped * length / ProceduralAudio.SampleRate;

        Assert.AreEqual(Mathf.Round(cycles), cycles, 1e-3f, "the loop is not a whole number of cycles");
        Assert.AreEqual(requested, snapped, 0.6f, "snapping moved the pitch too far");

        float[] engine = ProceduralAudio.DieselEngine(1f, requested, 0.30f, 11);
        Assert.AreEqual(length, engine.Length);
        Assert.LessOrEqual(ProceduralAudio.LoopSeamStep(engine), ProceduralAudio.MaxStep(engine) * 1.5f,
            "the engine loop is discontinuous at the wrap");
    }

    // ── One-shots ────────────────────────────────────────────────────────────

    [Test]
    public void Audio_ImpactDecaysInsteadOfSustaining()
    {
        float[] thump = ProceduralAudio.Thump(0.35f, 90f, 0.30f, 43);
        int window = thump.Length / 10;

        float head = ProceduralAudio.Rms(thump, 0, window);
        float tail = ProceduralAudio.Rms(thump, thump.Length - window, window);

        Assert.Greater(head, tail * 3f, $"the impact does not decay ({head:0.####} -> {tail:0.####})");
        Assert.Greater(head, 0f);
    }

    [Test]
    public void Audio_NoteSequencePlaysItsNotesInOrderAndLength()
    {
        float[] notes = { 784f, 1175f };
        const float noteSeconds = 0.22f;

        float[] data = ProceduralAudio.NoteSequence(notes, noteSeconds, 0.2f, 0.16f);
        Assert.AreEqual(ProceduralAudio.LengthFor(noteSeconds) * notes.Length, data.Length,
            "the sequence length does not match its notes");

        int half = data.Length / notes.Length;
        float first = ProceduralAudio.ZeroCrossingRate(Slice(data, 0, half));
        float second = ProceduralAudio.ZeroCrossingRate(Slice(data, half, half));

        Assert.Greater(second, first * 1.2f,
            $"the higher note does not sound higher ({first:0} vs {second:0} crossings/s)");
    }

    [Test]
    public void Audio_SynthesisIsDeterministicForAGivenSeed()
    {
        float[] a = ProceduralAudio.DieselEngine(0.2f, 90f, 0.3f, 5);
        float[] b = ProceduralAudio.DieselEngine(0.2f, 90f, 0.3f, 5);
        float[] c = ProceduralAudio.DieselEngine(0.2f, 90f, 0.3f, 6);

        Assert.AreEqual(a.Length, b.Length);
        for (int i = 0; i < a.Length; i++)
            Assert.AreEqual(a[i], b[i], 1e-6f, $"sample {i} differed for the same seed");

        bool differs = false;
        for (int i = 0; i < a.Length && !differs; i++)
            differs = Mathf.Abs(a[i] - c[i]) > 1e-6f;
        Assert.IsTrue(differs, "a different seed produced identical audio");
    }

    static float[] Slice(float[] data, int start, int count)
    {
        var slice = new float[count];
        System.Array.Copy(data, start, slice, 0, count);
        return slice;
    }
}
