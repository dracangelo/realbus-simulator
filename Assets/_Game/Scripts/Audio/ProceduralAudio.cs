using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Procedural audio synthesis for RealBus.
///
/// The repository ships no audio files at all - there is not a single .wav, .ogg or
/// .mp3 anywhere under Assets - so everything the player hears is generated at
/// runtime and synthesis quality *is* the audio production. The previous generators
/// produced a bare sine plus one harmonic and applied a fade-in **and** fade-out
/// envelope to every clip, including the looping ones, so a looping one-second
/// engine sample dropped to silence at both ends and pumped once per second.
///
/// The generators here are built around two rules:
///
///   1. <b>Looping clips are periodic by construction.</b> Every partial frequency is
///      snapped so an integer number of cycles fits the buffer (<see cref="SnapToLoop"/>),
///      and noise is built as a sum of snapped sinusoids rather than by filtering a
///      random stream. The result repeats exactly, so the loop point needs no fade at
///      all and the level stays flat across the whole buffer.
///   2. <b>One-shots get an envelope, loops never do.</b>
///
/// Everything is deterministic for a given seed so audio is reproducible and the
/// buffers can be asserted directly in tests.
/// </summary>
public static class ProceduralAudio
{
    public const int SampleRate = 22050;

    /// <summary>One generated clip and the manager field it belongs in.</summary>
    public struct ClipSpec
    {
        public string key;
        public string name;
        public float[] samples;
        public bool loop;
    }

    // ── Building blocks ──────────────────────────────────────────────────────

    /// <summary>
    /// A looping band of "noise" built from snapped sinusoids. Because each partial
    /// completes a whole number of cycles per buffer the result is exactly periodic -
    /// continuous at the loop point and perfectly flat in level.
    /// </summary>
    public static float[] BandNoise(float seconds, float lowHz, float highHz, float amplitude, int seed)
    {
        int length = LengthFor(seconds);
        float[] data = new float[length];

        float low = Mathf.Clamp(Mathf.Min(lowHz, highHz), 1f, SampleRate * 0.45f);
        float high = Mathf.Clamp(Mathf.Max(lowHz, highHz), low + 1f, SampleRate * 0.45f);

        const int partials = 26;
        uint state = SeedOf(seed);
        float gainScale = 1f / Mathf.Sqrt(partials);

        for (int p = 0; p < partials; p++)
        {
            float t = partials == 1 ? 0f : p / (float)(partials - 1);
            float hz = SnapToLoop(Mathf.Lerp(low, high, t), length);
            float phase = NextFloat(ref state) * 2f * Mathf.PI;
            float gain = (0.55f + 0.45f * NextFloat(ref state)) * gainScale;
            float w = 2f * Mathf.PI * hz / SampleRate;

            for (int i = 0; i < length; i++)
                data[i] += Mathf.Sin(w * i + phase) * gain;
        }

        Normalize(data, amplitude);
        return data;
    }

    /// <summary>
    /// A looping diesel engine: a harmonic stack whose fundamental is snapped to the
    /// buffer, an amplitude modulation at the firing order (one power stroke per two
    /// revolutions) and a band of combustion roughness.
    /// </summary>
    public static float[] DieselEngine(float seconds, float fundamentalHz, float amplitude, int seed)
    {
        int length = LengthFor(seconds);
        float[] data = new float[length];

        float fundamental = SnapToLoop(Mathf.Max(20f, fundamentalHz), length);
        float[] harmonics = { 1f, 2f, 3f, 4f, 6f, 8f, 11f };
        float[] gains = { 1f, 0.66f, 0.44f, 0.30f, 0.19f, 0.12f, 0.07f };

        for (int h = 0; h < harmonics.Length; h++)
        {
            float hz = SnapToLoop(fundamental * harmonics[h], length);
            if (hz >= SampleRate * 0.45f) continue;

            float w = 2f * Mathf.PI * hz / SampleRate;
            float gain = gains[h];
            for (int i = 0; i < length; i++) data[i] += Mathf.Sin(w * i) * gain;
        }

        // Firing-order roughness: a lumpy idle rather than a pure organ tone.
        float amHz = SnapToLoop(fundamental * 0.5f, length);
        float amW = 2f * Mathf.PI * amHz / SampleRate;
        for (int i = 0; i < length; i++)
            data[i] *= 0.82f + 0.18f * Mathf.Sin(amW * i);

        float[] roughness = BandNoise(seconds, fundamental * 6f, fundamental * 26f, 1f, seed);
        for (int i = 0; i < length; i++) data[i] += roughness[i] * 0.30f;

        Normalize(data, amplitude);
        return data;
    }

    /// <summary>A non-looping squeal with vibrato, used for tyres and brakes.</summary>
    public static float[] Squeal(float seconds, float hz, float vibratoHz, float amplitude)
    {
        int length = LengthFor(seconds);
        float[] data = new float[length];
        float phase = 0f;

        for (int i = 0; i < length; i++)
        {
            float t = i / (float)SampleRate;
            float wobble = 1f + 0.045f * Mathf.Sin(2f * Mathf.PI * vibratoHz * t);
            phase += 2f * Mathf.PI * hz * wobble / SampleRate;

            float attack = Mathf.Clamp01(t / 0.012f);
            float release = Mathf.Clamp01((seconds - t) / 0.30f);
            float envelope = attack * release;

            data[i] = (Mathf.Sin(phase) * 0.80f + Mathf.Sin(phase * 2f) * 0.18f) * envelope;
        }

        Normalize(data, amplitude);
        return data;
    }

    /// <summary>A non-looping noise burst (air brakes, releases).</summary>
    public static float[] Hiss(float seconds, float lowHz, float highHz, float amplitude, int seed)
    {
        float[] data = BandNoise(seconds, lowHz, highHz, 1f, seed);
        int length = data.Length;

        for (int i = 0; i < length; i++)
        {
            float t = i / (float)SampleRate;
            float attack = Mathf.Clamp01(t / 0.008f);
            float release = Mathf.Clamp01((seconds - t) / 0.25f);
            data[i] *= attack * release;
        }

        Normalize(data, amplitude);
        return data;
    }

    /// <summary>
    /// A non-looping impact: a decaying low body plus a short bright crack.
    /// </summary>
    public static float[] Thump(float seconds, float hz, float amplitude, int seed)
    {
        int length = LengthFor(seconds);
        float[] data = new float[length];
        float[] crack = BandNoise(seconds, hz * 3f, hz * 16f, 1f, seed);

        for (int i = 0; i < length; i++)
        {
            float t = i / (float)SampleRate;
            float body = Mathf.Sin(2f * Mathf.PI * hz * t) * Mathf.Exp(-t * 24f);
            float edge = crack[i] * Mathf.Exp(-t * 70f) * 0.6f;
            data[i] = body + edge;
        }

        Normalize(data, amplitude);
        return data;
    }

    /// <summary>
    /// A non-looping melody of equal-length notes, used for the interface click, the
    /// docking chime and the reward jingles.
    /// </summary>
    public static float[] NoteSequence(float[] notesHz, float noteSeconds, float amplitude, float decaySeconds)
    {
        int noteLength = LengthFor(noteSeconds);
        int length = noteLength * Mathf.Max(1, notesHz != null ? notesHz.Length : 1);
        float[] data = new float[length];
        if (notesHz == null || notesHz.Length == 0) return data;

        float decay = Mathf.Max(0.02f, decaySeconds);

        for (int n = 0; n < notesHz.Length; n++)
        {
            for (int i = 0; i < noteLength; i++)
            {
                float t = i / (float)SampleRate;
                float envelope = Mathf.Exp(-t / decay) * (1f - Mathf.Exp(-t / 0.004f));
                float value = Mathf.Sin(2f * Mathf.PI * notesHz[n] * t) * 0.75f
                            + Mathf.Sin(2f * Mathf.PI * notesHz[n] * 2f * t) * 0.18f;
                data[n * noteLength + i] = value * envelope;
            }
        }

        Normalize(data, amplitude);
        return data;
    }

    /// <summary>Wraps a sample buffer into an <see cref="AudioClip"/>.</summary>
    public static AudioClip ToClip(string name, float[] samples, bool loop)
    {
        int length = samples != null ? Mathf.Max(1, samples.Length) : 1;
        float[] data = samples != null && samples.Length > 0 ? samples : new float[1];
        AudioClip clip = AudioClip.Create(name, length, 1, SampleRate, false);
        clip.SetData(data, 0);
        return clip;
    }

    /// <summary>
    /// The complete fallback set the audio manager needs when no production clips are
    /// installed. Shared with the tests so what is asserted is exactly what plays.
    /// </summary>
    public static ClipSpec[] BuildFallbackLibrary()
    {
        var specs = new List<ClipSpec>
        {
            Spec("engineIdle",  "Fallback Diesel Idle",   true,  DieselEngine(1.0f, 72f, 0.30f, 11)),
            Spec("engineLoad",  "Fallback Diesel Load",   true,  DieselEngine(1.0f, 126f, 0.26f, 23)),
            Spec("tyreRoll",    "Fallback Tyre Roll",     true,  BandNoise(1.0f, 55f, 240f, 0.16f, 31)),
            Spec("tyreSqueal",  "Fallback Tyre Squeal",   false, Squeal(0.30f, 780f, 9f, 0.16f)),
            Spec("brakeSqueal", "Fallback Brake Squeal",  false, Squeal(0.40f, 1180f, 6f, 0.18f)),
            Spec("airHiss",     "Fallback Air Hiss",      false, Hiss(0.35f, 900f, 6500f, 0.20f, 37)),
            Spec("uiClick",     "Fallback UI Click",      false, NoteSequence(new[] { 660f }, 0.07f, 0.20f, 0.018f)),
            Spec("dockingChime","Fallback Docking Chime", false, NoteSequence(new[] { 784f, 1175f }, 0.22f, 0.20f, 0.16f)),
            Spec("missionFanfare","Fallback Mission Fanfare", false, NoteSequence(new[] { 523f, 659f, 784f }, 0.28f, 0.22f, 0.20f)),
            Spec("rankJingle",  "Fallback Rank Jingle",   false, NoteSequence(new[] { 659f, 880f, 1044f }, 0.22f, 0.22f, 0.16f)),
            Spec("collisionHit","Fallback Collision",     false, Thump(0.35f, 90f, 0.30f, 43)),
            Spec("cityAmbience","Fallback City Ambience", true,  BandNoise(1.5f, 90f, 900f, 0.05f, 47)),
            Spec("airConditioning","Fallback Cabin Air",  true,  BandNoise(1.5f, 140f, 900f, 0.045f, 53))
        };
        return specs.ToArray();
    }

    static ClipSpec Spec(string key, string name, bool loop, float[] samples)
    {
        return new ClipSpec { key = key, name = name, loop = loop, samples = samples };
    }

    // ── Measurement helpers (used by the tests and for diagnostics) ──────────

    /// <summary>Buffer length for a duration, clamped to at least two samples.</summary>
    public static int LengthFor(float seconds)
    {
        return Mathf.Max(2, Mathf.RoundToInt(SampleRate * Mathf.Max(1f / SampleRate, seconds)));
    }

    /// <summary>
    /// Snaps a frequency so a whole number of cycles fits <paramref name="length"/>
    /// samples. This is what makes a generated clip repeat exactly.
    /// </summary>
    public static float SnapToLoop(float hz, int length)
    {
        if (length <= 0) return hz;
        float cycles = Mathf.Max(1f, Mathf.Round(hz * length / (float)SampleRate));
        return cycles * SampleRate / length;
    }

    /// <summary>Largest absolute sample value.</summary>
    public static float Peak(float[] data)
    {
        float peak = 0f;
        for (int i = 0; i < data.Length; i++)
        {
            float value = Mathf.Abs(data[i]);
            if (value > peak) peak = value;
        }
        return peak;
    }

    /// <summary>Root-mean-square of a window, or 0 for an empty window.</summary>
    public static float Rms(float[] data, int start, int count)
    {
        int begin = Mathf.Clamp(start, 0, data.Length);
        int end = Mathf.Clamp(begin + count, begin, data.Length);
        if (end <= begin) return 0f;

        double sum = 0.0;
        for (int i = begin; i < end; i++) sum += (double)data[i] * data[i];
        return Mathf.Sqrt((float)(sum / (end - begin)));
    }

    /// <summary>
    /// Size of the step the waveform takes across the loop point. For a periodic
    /// buffer this is an ordinary sample-to-sample step; a large value means an
    /// audible click at the wrap.
    /// </summary>
    public static float LoopSeamStep(float[] data)
    {
        if (data == null || data.Length < 2) return 0f;
        return Mathf.Abs(data[0] - data[data.Length - 1]);
    }

    /// <summary>
    /// Largest single-sample jump. A click at the loop point shows up here as an
    /// outlier far bigger than any step inside the buffer.
    /// </summary>
    public static float MaxStep(float[] data)
    {
        if (data == null || data.Length < 2) return 0f;

        float max = 0f;
        for (int i = 1; i < data.Length; i++)
        {
            float step = Mathf.Abs(data[i] - data[i - 1]);
            if (step > max) max = step;
        }
        return max;
    }

    /// <summary>Average absolute difference between neighbouring samples.</summary>
    public static float MeanStep(float[] data)
    {
        if (data == null || data.Length < 2) return 0f;
        double sum = 0.0;
        for (int i = 1; i < data.Length; i++) sum += Mathf.Abs(data[i] - data[i - 1]);
        return (float)(sum / (data.Length - 1));
    }

    /// <summary>
    /// Zero crossings per second, a cheap proxy for the dominant frequency. For a
    /// signal confined to a band this lands near twice the band centre.
    /// </summary>
    public static float ZeroCrossingRate(float[] data)
    {
        if (data == null || data.Length < 2) return 0f;

        int crossings = 0;
        for (int i = 1; i < data.Length; i++)
            if ((data[i - 1] < 0f && data[i] >= 0f) || (data[i - 1] >= 0f && data[i] < 0f))
                crossings++;

        return crossings * (float)SampleRate / data.Length;
    }

    // ── Internals ────────────────────────────────────────────────────────────

    /// <summary>Scales the buffer so its peak equals <paramref name="amplitude"/>.</summary>
    static void Normalize(float[] data, float amplitude)
    {
        float peak = Peak(data);
        if (peak <= 1e-6f) return;

        float scale = amplitude / peak;
        for (int i = 0; i < data.Length; i++) data[i] *= scale;
    }

    static uint SeedOf(int seed)
    {
        // A zero state would make xorshift collapse, so offset it.
        uint state = (uint)seed * 2654435761u + 1013904223u;
        return state == 0u ? 2463534242u : state;
    }

    static float NextFloat(ref uint state)
    {
        state ^= state << 13;
        state ^= state >> 17;
        state ^= state << 5;
        return (state & 0xFFFFFFu) / (float)0xFFFFFF;
    }
}
