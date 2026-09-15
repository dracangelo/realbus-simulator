using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class UIAnimator
{
    /// <summary>Fade a CanvasGroup in</summary>
    public static IEnumerator FadeIn(CanvasGroup cg, float duration = 0.3f)
    {
        if (ReducedMotion()) { cg.alpha = 1f; yield break; }
        cg.alpha = 0f;
        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            cg.alpha = Mathf.Clamp01(t / duration);
            yield return null;
        }
        cg.alpha = 1f;
    }

    /// <summary>Fade a CanvasGroup out</summary>
    public static IEnumerator FadeOut(CanvasGroup cg, float duration = 0.3f)
    {
        if (ReducedMotion()) { cg.alpha = 0f; yield break; }
        cg.alpha = 1f;
        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            cg.alpha = Mathf.Clamp01(1f - (t / duration));
            yield return null;
        }
        cg.alpha = 0f;
    }

    /// <summary>Slide panel in from bottom</summary>
    public static IEnumerator SlideInFromBottom(RectTransform rect,
        float duration = 0.35f, float offset = 60f)
    {
        Vector2 target = rect.anchoredPosition;
        if (ReducedMotion()) { rect.anchoredPosition = target; yield break; }
        Vector2 start = target + new Vector2(0, -offset);
        float t = 0f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float ease = 1f - Mathf.Pow(1f - Mathf.Clamp01(t / duration), 3f);
            rect.anchoredPosition = Vector2.Lerp(start, target, ease);
            yield return null;
        }

        rect.anchoredPosition = target;
    }

    /// <summary>Scale button on press</summary>
    public static IEnumerator PunchScale(Transform t, float intensity = 0.05f)
    {
        Vector3 original = t.localScale;
        if (ReducedMotion()) { t.localScale = original; yield break; }
        Vector3 small = original * (1f - intensity);

        float half = 0.08f;
        float elapsed = 0f;

        while (elapsed < half)
        {
            elapsed += Time.unscaledDeltaTime;
            t.localScale = Vector3.Lerp(original, small,
                Mathf.Clamp01(elapsed / half));
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < half)
        {
            elapsed += Time.unscaledDeltaTime;
            t.localScale = Vector3.Lerp(small, original,
                Mathf.Clamp01(elapsed / half));
            yield return null;
        }

        t.localScale = original;
    }

    static bool ReducedMotion() { return SettingsManager.Instance != null && SettingsManager.Instance.Current.reducedMotion; }
}
