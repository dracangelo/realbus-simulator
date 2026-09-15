using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankUpSequence : MonoBehaviour
{
    CanvasGroup overlayGroup;
    RectTransform cardRect;
    TextMeshProUGUI rankText;
    TextMeshProUGUI titleText;
    TextMeshProUGUI unlockText;

    public void EnsureUI(Transform parent)
    {
        if (overlayGroup != null)
            return;

        var overlay = new GameObject("RankUpOverlay", typeof(RectTransform), typeof(Image), typeof(CanvasGroup));
        overlay.transform.SetParent(parent, false);

        var overlayRect = overlay.GetComponent<RectTransform>();
        overlayRect.anchorMin = Vector2.zero;
        overlayRect.anchorMax = Vector2.one;
        overlayRect.offsetMin = Vector2.zero;
        overlayRect.offsetMax = Vector2.zero;

        var overlayImage = overlay.GetComponent<Image>();
        overlayImage.color = UITheme.WithAlpha(UITheme.Background, 0.94f);

        overlayGroup = overlay.GetComponent<CanvasGroup>();
        overlayGroup.alpha = 0f;
        overlayGroup.blocksRaycasts = false;
        overlayGroup.interactable = false;

        var card = new GameObject("RankUpCard", typeof(RectTransform), typeof(Image), typeof(VerticalLayoutGroup));
        card.transform.SetParent(overlay.transform, false);
        cardRect = card.GetComponent<RectTransform>();
        cardRect.anchorMin = new Vector2(0.5f, 0.5f);
        cardRect.anchorMax = new Vector2(0.5f, 0.5f);
        cardRect.sizeDelta = new Vector2(720f, 280f);
        cardRect.anchoredPosition = Vector2.zero;

        var cardImage = card.GetComponent<Image>();
        cardImage.color = UITheme.SurfaceContainer;

        var layout = card.GetComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(36, 36, 32, 32);
        layout.spacing = 10f;
        layout.childAlignment = TextAnchor.MiddleCenter;
        layout.childControlHeight = true;
        layout.childControlWidth = true;
        layout.childForceExpandHeight = false;
        layout.childForceExpandWidth = true;

        rankText = CreateText(card.transform, "RankText", 26f, UITheme.TertiaryDim, UITheme.FontWeight.Bold);
        titleText = CreateText(card.transform, "TitleText", 44f, UITheme.TextPrimary, UITheme.FontWeight.Bold);
        unlockText = CreateText(card.transform, "UnlockText", 22f, UITheme.TextSecondary, UITheme.FontWeight.Medium);

        overlay.SetActive(false);
    }

    public IEnumerator PlaySequence(IReadOnlyList<RankUpEventData> rankUps)
    {
        if (rankUps == null || rankUps.Count == 0)
            yield break;

        EnsureUI(transform);

        for (int i = 0; i < rankUps.Count; i++)
        {
            RankUpEventData rankUp = rankUps[i];
            overlayGroup.gameObject.SetActive(true);

            rankText.text = $"RANK {rankUp.newRank}";
            titleText.text = rankUp.rankTitle.ToUpper();
            unlockText.text = rankUp.unlockReveal.ToUpper();

            overlayGroup.alpha = 0f;
            cardRect.localScale = new Vector3(0.92f, 0.92f, 1f);

            float fadeInDuration = 0.28f;
            float elapsed = 0f;
            while (elapsed < fadeInDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / fadeInDuration);
                overlayGroup.alpha = t;
                cardRect.localScale = Vector3.Lerp(new Vector3(0.92f, 0.92f, 1f), Vector3.one, t);
                yield return null;
            }

            overlayGroup.alpha = 1f;
            cardRect.localScale = Vector3.one;

            yield return new WaitForSeconds(1.25f);

            float fadeOutDuration = 0.22f;
            elapsed = 0f;
            while (elapsed < fadeOutDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / fadeOutDuration);
                overlayGroup.alpha = 1f - t;
                yield return null;
            }

            overlayGroup.alpha = 0f;
            overlayGroup.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.08f);
        }
    }

    static TextMeshProUGUI CreateText(Transform parent, string objectName, float fontSize, Color color, UITheme.FontWeight weight)
    {
        var textObject = new GameObject(objectName, typeof(RectTransform), typeof(TextMeshProUGUI), typeof(LayoutElement));
        textObject.transform.SetParent(parent, false);

        var layoutElement = textObject.GetComponent<LayoutElement>();
        layoutElement.preferredHeight = fontSize + 18f;

        var text = textObject.GetComponent<TextMeshProUGUI>();
        text.alignment = TextAlignmentOptions.Center;
        text.fontSize = fontSize;
        text.color = color;
        text.font = UITheme.GetFont(weight);
        text.textWrappingMode = TextWrappingModes.Normal;
        return text;
    }
}
