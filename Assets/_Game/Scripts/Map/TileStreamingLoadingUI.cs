using UnityEngine;
using UnityEngine.UI;

public class TileStreamingLoadingUI : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup canvasGroup;
    public RectTransform spinner;
    public float spinSpeedDegPerSec = 240f;

    bool loading;

    void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    void Update()
    {
        if (spinner != null && loading)
            spinner.Rotate(0f, 0f, -spinSpeedDegPerSec * Time.unscaledDeltaTime);
    }

    public void SetLoading(bool isLoading)
    {
        loading = isLoading;
        if (canvasGroup == null) return;
        canvasGroup.alpha = loading ? 1f : 0f;
        canvasGroup.blocksRaycasts = loading;
        canvasGroup.interactable = loading;
    }
}

