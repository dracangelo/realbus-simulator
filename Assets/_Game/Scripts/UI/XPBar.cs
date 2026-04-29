using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    [SerializeField] Image fillImage;
    [SerializeField] TextMeshProUGUI rankValueText;
    [SerializeField] TextMeshProUGUI rankTitleText;
    [SerializeField] TextMeshProUGUI progressText;
    [SerializeField] TextMeshProUGUI nextUnlockText;
    [SerializeField] bool subscribeToXPSystem = true;

    Coroutine animationRoutine;

    void OnEnable()
    {
        if (subscribeToXPSystem && XPSystem.Instance != null)
            XPSystem.Instance.XPChanged += HandleXPChanged;
    }

    void Start()
    {
        RefreshCurrent();
    }

    void OnDisable()
    {
        if (XPSystem.Instance != null)
            XPSystem.Instance.XPChanged -= HandleXPChanged;
    }

    public void Configure(
        Image fill,
        TextMeshProUGUI rankValue,
        TextMeshProUGUI rankTitle,
        TextMeshProUGUI progress,
        TextMeshProUGUI nextUnlock,
        bool autoSubscribe = true)
    {
        fillImage = fill;
        rankValueText = rankValue;
        rankTitleText = rankTitle;
        progressText = progress;
        nextUnlockText = nextUnlock;
        subscribeToXPSystem = autoSubscribe;
    }

    public void RefreshCurrent()
    {
        if (XPSystem.Instance == null)
            return;

        Refresh(XPSystem.Instance.GetProgressSnapshot());
    }

    public void Refresh(RankProgressSnapshot snapshot)
    {
        if (fillImage)
            fillImage.fillAmount = snapshot.progress01;

        if (rankValueText)
        {
            rankValueText.text = $"RANK {snapshot.currentRank}";
            rankValueText.color = UITheme.TextPrimary;
            rankValueText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }

        if (rankTitleText)
        {
            rankTitleText.text = snapshot.currentRankTitle.ToUpper();
            rankTitleText.color = UITheme.Accent;
            rankTitleText.font = UITheme.GetFont(UITheme.FontWeight.Bold);
        }

        if (progressText)
        {
            progressText.text = snapshot.isMaxRank
                ? $"{snapshot.totalXP:N0} XP  •  MAX RANK"
                : $"{snapshot.xpIntoCurrentRank:N0} / {snapshot.xpRequiredForNextRank:N0} XP  •  {snapshot.xpToNextRank:N0} TO NEXT";
            progressText.color = UITheme.TextSecondary;
            progressText.font = UITheme.GetFont(UITheme.FontWeight.Medium);
        }

        if (nextUnlockText)
        {
            nextUnlockText.text = snapshot.isMaxRank
                ? snapshot.nextRankUnlockReveal.ToUpper()
                : $"NEXT: {snapshot.nextRankTitle.ToUpper()}  •  {snapshot.nextRankUnlockReveal.ToUpper()}";
            nextUnlockText.color = UITheme.TextMuted;
            nextUnlockText.font = UITheme.GetFont(UITheme.FontWeight.Regular);
        }
    }

    public Coroutine AnimateAward(XPAwardResult awardResult, float duration = 1.15f)
    {
        if (animationRoutine != null)
            StopCoroutine(animationRoutine);

        animationRoutine = StartCoroutine(AnimateAwardRoutine(awardResult, duration));
        return animationRoutine;
    }

    public static XPBar CreateRuntimeBar(Transform parent, string objectName = "XPBarRuntime")
    {
        var root = new GameObject(objectName, typeof(RectTransform), typeof(VerticalLayoutGroup), typeof(XPBar));
        root.transform.SetParent(parent, false);

        var rootRect = root.GetComponent<RectTransform>();
        rootRect.anchorMin = new Vector2(0f, 0.5f);
        rootRect.anchorMax = new Vector2(1f, 0.5f);
        rootRect.sizeDelta = new Vector2(0f, 160f);

        var layout = root.GetComponent<VerticalLayoutGroup>();
        layout.spacing = 8f;
        layout.padding = new RectOffset(0, 0, 0, 0);
        layout.childAlignment = TextAnchor.UpperLeft;
        layout.childControlHeight = true;
        layout.childControlWidth = true;
        layout.childForceExpandHeight = false;
        layout.childForceExpandWidth = true;

        TextMeshProUGUI rankValue = CreateText(root.transform, "RankValue", 28f, UITheme.TextPrimary, UITheme.FontWeight.Bold);
        TextMeshProUGUI rankTitle = CreateText(root.transform, "RankTitle", 18f, UITheme.Accent, UITheme.FontWeight.Bold);

        var barBgObject = new GameObject("BarBackground", typeof(RectTransform), typeof(Image), typeof(LayoutElement));
        barBgObject.transform.SetParent(root.transform, false);
        var barBgRect = barBgObject.GetComponent<RectTransform>();
        barBgRect.sizeDelta = new Vector2(0f, 18f);

        var layoutElement = barBgObject.GetComponent<LayoutElement>();
        layoutElement.preferredHeight = 18f;

        var barBg = barBgObject.GetComponent<Image>();
        barBg.color = UITheme.WithAlpha(UITheme.SurfaceHigh, 0.95f);
        barBg.type = Image.Type.Sliced;

        var fillObject = new GameObject("Fill", typeof(RectTransform), typeof(Image));
        fillObject.transform.SetParent(barBgObject.transform, false);
        var fillRect = fillObject.GetComponent<RectTransform>();
        fillRect.anchorMin = Vector2.zero;
        fillRect.anchorMax = Vector2.one;
        fillRect.offsetMin = Vector2.zero;
        fillRect.offsetMax = Vector2.zero;

        var fill = fillObject.GetComponent<Image>();
        fill.color = UITheme.Accent;
        fill.type = Image.Type.Filled;
        fill.fillMethod = Image.FillMethod.Horizontal;
        fill.fillOrigin = (int)Image.OriginHorizontal.Left;

        TextMeshProUGUI progress = CreateText(root.transform, "ProgressText", 16f, UITheme.TextSecondary, UITheme.FontWeight.Medium);
        TextMeshProUGUI nextUnlock = CreateText(root.transform, "NextUnlockText", 14f, UITheme.TextMuted, UITheme.FontWeight.Regular);

        var component = root.GetComponent<XPBar>();
        component.Configure(fill, rankValue, rankTitle, progress, nextUnlock, autoSubscribe: false);
        component.RefreshCurrent();
        return component;
    }

    IEnumerator AnimateAwardRoutine(XPAwardResult awardResult, float duration)
    {
        if (XPSystem.Instance == null || awardResult == null)
        {
            RefreshCurrent();
            yield break;
        }

        float elapsed = 0f;
        int fromXp = awardResult.previousTotalXP;
        int toXp = awardResult.newTotalXP;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            t = 1f - Mathf.Pow(1f - t, 3f);

            int animatedXp = Mathf.RoundToInt(Mathf.Lerp(fromXp, toXp, t));
            Refresh(XPSystem.Instance.EvaluateProgress(animatedXp));
            yield return null;
        }

        Refresh(XPSystem.Instance.EvaluateProgress(toXp));
        animationRoutine = null;
    }

    static TextMeshProUGUI CreateText(Transform parent, string objectName, float fontSize, Color color, UITheme.FontWeight weight)
    {
        var textObject = new GameObject(objectName, typeof(RectTransform), typeof(TextMeshProUGUI), typeof(LayoutElement));
        textObject.transform.SetParent(parent, false);

        var layoutElement = textObject.GetComponent<LayoutElement>();
        layoutElement.preferredHeight = fontSize + 10f;

        var text = textObject.GetComponent<TextMeshProUGUI>();
        text.text = string.Empty;
        text.fontSize = fontSize;
        text.color = color;
        text.font = UITheme.GetFont(weight);
        text.enableWordWrapping = true;
        return text;
    }

    void HandleXPChanged(RankProgressSnapshot snapshot)
    {
        if (animationRoutine == null)
            Refresh(snapshot);
    }
}
