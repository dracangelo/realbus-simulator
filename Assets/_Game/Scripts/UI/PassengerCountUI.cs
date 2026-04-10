using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PassengerCountUI : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI countText;
    public TextMeshProUGUI emojiText;
    public Image moodTint;

    [Header("Emoji Thresholds")]
    [Range(0f, 1f)] public float happyThreshold = 0.8f;
    [Range(0f, 1f)] public float neutralThreshold = 0.55f;
    [Range(0f, 1f)] public float unhappyThreshold = 0.35f;

    void Update()
    {
        var pm = PassengerManager.Instance;
        if (pm == null) return;

        int cap = pm.GetMaxCapacity();
        if (countText != null)
            countText.text = $"{pm.currentPassengers}/{cap} PAX";

        float sat = pm.averageSatisfaction;
        if (emojiText != null)
            emojiText.text = SatisfactionToEmoji(sat);

        if (moodTint != null)
            moodTint.color = SatisfactionToColor(sat);
    }

    string SatisfactionToEmoji(float sat)
    {
        if (sat >= happyThreshold) return "😊";
        if (sat >= neutralThreshold) return "😐";
        if (sat >= unhappyThreshold) return "😕";
        return "😠";
    }

    Color SatisfactionToColor(float sat)
    {
        if (sat >= happyThreshold) return new Color(0.2f, 0.8f, 0.35f, 0.85f);
        if (sat >= neutralThreshold) return new Color(0.95f, 0.8f, 0.2f, 0.85f);
        if (sat >= unhappyThreshold) return new Color(0.95f, 0.55f, 0.2f, 0.85f);
        return new Color(0.9f, 0.25f, 0.25f, 0.9f);
    }
}
