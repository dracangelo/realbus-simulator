using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Pins the parts of the intro shot that are pure maths: the orbit geometry, the
/// letterbox curve and the gate that decides whether the shot plays at all. The
/// camera animation itself needs a live scene, but every rule that could produce a
/// broken frame - a camera under the road, a bar taller than the screen, a shot that
/// replays on every scene load - is decidable here.
/// </summary>
public class CinematicIntroTests
{
    [Test]
    public void OrbitPosition_SitsAtTheRequestedRadiusAndHeightAboveTheFocus()
    {
        var focus = new Vector3(120f, 2f, -40f);
        Vector3 position = CinematicIntroSequence.OrbitPosition(focus, 15f, 8.5f, 138f);

        Vector3 flat = new Vector3(position.x - focus.x, 0f, position.z - focus.z);
        Assert.That(flat.magnitude, Is.EqualTo(15f).Within(0.01f),
            "the shot has to keep the same distance from the bus as it swings around it");
        Assert.That(position.y - focus.y, Is.EqualTo(8.5f).Within(0.01f),
            "the height is measured from the focus, so uneven ground cannot put the camera in the road");
        Assert.That(position.y, Is.GreaterThan(0f), "an orbit camera below ground sees the city from underneath");
    }

    [Test]
    public void OrbitPosition_AnglesAroundTheWorldYAxis()
    {
        var focus = Vector3.zero;
        Vector3 behind = CinematicIntroSequence.OrbitPosition(focus, 10f, 0f, 0f);
        Vector3 side = CinematicIntroSequence.OrbitPosition(focus, 10f, 0f, 90f);

        Assert.That(Vector3.Distance(behind, new Vector3(0f, 0f, 10f)), Is.LessThan(0.01f),
            "0 degrees is the reference direction; the shot adds the bus heading to it");
        Assert.That(Vector3.Distance(side, new Vector3(10f, 0f, 0f)), Is.LessThan(0.01f),
            "a quarter turn of the orbit has to move the camera a quarter turn around the bus");
    }

    [Test]
    public void OrbitRotation_FacesTheFocusWithoutRolling()
    {
        var focus = new Vector3(0f, 1.6f, 0f);
        Vector3 position = CinematicIntroSequence.OrbitPosition(focus, 15f, 8.5f, 138f);
        Quaternion rotation = CinematicIntroSequence.OrbitRotation(position, focus);

        Vector3 toFocus = (focus - position).normalized;
        Assert.That(Vector3.Dot(rotation * Vector3.forward, toFocus), Is.GreaterThan(0.999f),
            "the bus is the subject of the shot and must stay centred");
        Assert.That(Mathf.Abs(rotation.eulerAngles.z), Is.LessThan(0.01f),
            "an upright look-at keeps the horizon level; roll comes from the driving rig, not the cut");
    }

    [Test]
    public void OrbitRotation_HandlesACameraSittingOnTheFocus()
    {
        Quaternion rotation = CinematicIntroSequence.OrbitRotation(Vector3.one, Vector3.one);
        Assert.That(rotation, Is.EqualTo(Quaternion.identity),
            "a zero-length look-at must not produce an undefined rotation, or the frame goes black");
    }

    [Test]
    public void BarAmount_OpensHoldsAndClosesWithinTheShot()
    {
        const float duration = 4f, ramp = 0.7f;

        Assert.That(CinematicIntroSequence.BarAmount(0f, duration, ramp), Is.EqualTo(0f).Within(0.001f));
        Assert.That(CinematicIntroSequence.BarAmount(duration, duration, ramp), Is.EqualTo(0f).Within(0.001f));
        Assert.That(CinematicIntroSequence.BarAmount(duration * 0.5f, duration, ramp), Is.EqualTo(1f).Within(0.001f),
            "the bars must be fully closed for the body of the shot");
        Assert.That(CinematicIntroSequence.BarAmount(ramp * 0.5f, duration, ramp),
            Is.LessThan(CinematicIntroSequence.BarAmount(ramp, duration, ramp)),
            "the frame opens rather than snapping shut");
        Assert.That(CinematicIntroSequence.BarAmount(duration - ramp * 0.5f, duration, ramp),
            Is.LessThan(CinematicIntroSequence.BarAmount(duration - ramp, duration, ramp)),
            "the frame closes again before control returns");

        for (float t = -1f; t <= duration + 1f; t += 0.05f)
        {
            float amount = CinematicIntroSequence.BarAmount(t, duration, ramp);
            Assert.That(amount, Is.InRange(0f, 1f), $"letterbox must stay a fraction at t={t}");
        }
    }

    [Test]
    public void BarAmount_OfAZeroLengthShotIsEmpty()
    {
        Assert.That(CinematicIntroSequence.BarAmount(0f, 0f, 0.7f), Is.EqualTo(0f),
            "a shot with no duration must not leave black bars on screen");
    }

    [Test]
    public void ShouldPlay_OnlyOnceAndOnlyWithARealWorld()
    {
        Assert.That(CinematicIntroSequence.ShouldPlay(false, true, true, true), Is.True);
        Assert.That(CinematicIntroSequence.ShouldPlay(true, true, true, true), Is.False,
            "reloading the scene must not replay the intro");
        Assert.That(CinematicIntroSequence.ShouldPlay(false, false, true, true), Is.False,
            "players who turned the shot off keep control");
        Assert.That(CinematicIntroSequence.ShouldPlay(false, true, false, true), Is.False,
            "no bus means nothing to orbit");
        Assert.That(CinematicIntroSequence.ShouldPlay(false, true, true, false), Is.False,
            "no camera means nowhere to render the shot");
    }

    [Test]
    public void BeatIndexAt_PutsABoundaryOnTheLaterBeat()
    {
        float[] ends = CinematicIntroSequence.BeatEnds(0.36f, 0.52f);

        Assert.That(CinematicIntroSequence.BeatIndexAt(0f, ends), Is.EqualTo(0));
        Assert.That(CinematicIntroSequence.BeatIndexAt(0.35f, ends), Is.EqualTo(0));
        Assert.That(CinematicIntroSequence.BeatIndexAt(0.36f, ends), Is.EqualTo(1),
            "a time landing on the boundary must resolve to the new shot, or the cut reads as a smear");
        Assert.That(CinematicIntroSequence.BeatIndexAt(0.52f, ends), Is.EqualTo(2));
        Assert.That(CinematicIntroSequence.BeatIndexAt(1f, ends), Is.EqualTo(2));
        Assert.That(CinematicIntroSequence.BeatIndexAt(2f, ends), Is.EqualTo(2),
            "overshooting the sequence must not index past the shot list");
    }

    [Test]
    public void BeatEnds_AreOrderedAndClamped()
    {
        float[] ends = CinematicIntroSequence.BeatEnds(1.4f, -3f);
        Assert.That(ends[0], Is.InRange(0f, 1f));
        Assert.That(ends[1], Is.InRange(0f, 1f));
        Assert.That(ends[2], Is.EqualTo(1f), "the last beat always ends with the sequence");
        Assert.That(ends[1], Is.GreaterThanOrEqualTo(ends[0]),
            "an inverted cut would leave the hero pass with no time on screen");
    }

    [Test]
    public void BeatProgress_RunsZeroToOneInsideTheBeatAndNeverLeaks()
    {
        float[] ends = CinematicIntroSequence.BeatEnds(0.4f, 0.6f);

        Assert.That(CinematicIntroSequence.BeatProgress(0f, ends, 0), Is.EqualTo(0f));
        Assert.That(CinematicIntroSequence.BeatProgress(0.4f, ends, 0), Is.EqualTo(1f).Within(0.001f));
        Assert.That(CinematicIntroSequence.BeatProgress(0.4f, ends, 1), Is.EqualTo(0f).Within(0.001f),
            "the hero pass starts at its own first frame, not halfway through it");
        Assert.That(CinematicIntroSequence.BeatProgress(0.5f, ends, 1), Is.EqualTo(0.5f).Within(0.001f));

        for (float t = -1f; t <= 2f; t += 0.05f)
        for (int beat = 0; beat < 3; beat++)
            Assert.That(CinematicIntroSequence.BeatProgress(t, ends, beat), Is.InRange(0f, 1f),
                $"beat {beat} must stay a fraction at t={t}");
    }

    [Test]
    public void Blend_EndsExactlyOnBothPoses()
    {
        var from = new CinematicIntroSequence.ShotPose
        {
            position = new Vector3(1f, 10f, -3f),
            rotation = Quaternion.Euler(12f, 40f, 0f),
            fov = 38f
        };
        var to = new CinematicIntroSequence.ShotPose
        {
            position = new Vector3(4f, 2f, 6f),
            rotation = Quaternion.Euler(-4f, 100f, 0f),
            fov = 60f
        };

        var first = CinematicIntroSequence.Blend(from, to, 2.2f, 0f);
        var last = CinematicIntroSequence.Blend(from, to, 2.2f, 1f);

        Assert.That(Vector3.Distance(first.position, from.position), Is.LessThan(0.001f),
            "the first frame of a beat has to be its start pose or the shot jumps on entry");
        Assert.That(Vector3.Distance(last.position, to.position), Is.LessThan(0.001f),
            "the last frame has to be the next pose, otherwise the cut never resolves");
        Assert.That(first.fov, Is.EqualTo(from.fov).Within(0.001f));
        Assert.That(last.fov, Is.EqualTo(to.fov).Within(0.001f));
    }

    [Test]
    public void Blend_ArcBowsThePathWithoutLiftingTheEndpoints()
    {
        var from = new CinematicIntroSequence.ShotPose { position = Vector3.zero };
        var to = new CinematicIntroSequence.ShotPose { position = new Vector3(0f, 4f, 0f) };

        var middle = CinematicIntroSequence.Blend(from, to, 2f, 0.5f);
        Assert.That(middle.position.y, Is.GreaterThan(4f * 0.5f + 0.5f),
            "the melt should arc over the direct line, not ride it");

        var flat = CinematicIntroSequence.Blend(from, to, 0f, 0.5f);
        Assert.That(flat.position.y, Is.EqualTo(4f * 0.5f).Within(0.01f),
            "with no arc the blend is a plain interpolation");
    }

    [Test]
    public void CopyAmount_IsGoneBeforeTheCut()
    {
        const float duration = 5f, fadeStart = 0.21f, fade = 0.5f;

        Assert.That(CinematicIntroSequence.CopyAmount(0f, duration, fadeStart, fade), Is.EqualTo(1f),
            "the title is up while the bars open");
        Assert.That(CinematicIntroSequence.CopyAmount(fadeStart * duration, duration, fadeStart, fade), Is.EqualTo(1f));
        Assert.That(CinematicIntroSequence.CopyAmount(duration * 0.36f, duration, fadeStart, fade), Is.EqualTo(0f).Within(0.001f),
            "the hero pass must play without a caption over the bus");
        Assert.That(CinematicIntroSequence.CopyAmount(duration, duration, fadeStart, fade), Is.EqualTo(0f));

        float previous = 2f;
        for (float t = 0f; t <= duration; t += 0.05f)
        {
            float amount = CinematicIntroSequence.CopyAmount(t, duration, fadeStart, fade);
            Assert.That(amount, Is.InRange(0f, 1f), $"copy alpha must stay a fraction at t={t}");
            Assert.That(amount, Is.LessThanOrEqualTo(previous), "the caption must only ever fade out");
            previous = amount;
        }
    }

    [Test]
    public void EaseInOut_IsBoundedAndMonotonic()
    {
        Assert.That(CinematicIntroSequence.EaseInOut(-3f), Is.EqualTo(0f));
        Assert.That(CinematicIntroSequence.EaseInOut(4f), Is.EqualTo(1f));

        float previous = -1f;
        for (float t = 0f; t <= 1f; t += 0.02f)
        {
            float value = CinematicIntroSequence.EaseInOut(t);
            Assert.That(value, Is.GreaterThanOrEqualTo(previous), "the blend must never run backwards");
            previous = value;
        }
    }
}
