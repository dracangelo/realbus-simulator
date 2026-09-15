using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaFitter : MonoBehaviour
{
    RectTransform target;
    Rect lastSafeArea;
    Vector2Int lastScreenSize;

    void Awake()
    {
        target = GetComponent<RectTransform>();
        Apply();
    }

    void Update()
    {
        if (lastSafeArea != Screen.safeArea || lastScreenSize.x != Screen.width || lastScreenSize.y != Screen.height)
            Apply();
    }

    public void Apply()
    {
        if (target == null || Screen.width <= 0 || Screen.height <= 0) return;
        Rect area = Screen.safeArea;
        target.anchorMin = new Vector2(area.xMin / Screen.width, area.yMin / Screen.height);
        target.anchorMax = new Vector2(area.xMax / Screen.width, area.yMax / Screen.height);
        target.offsetMin = target.offsetMax = Vector2.zero;
        lastSafeArea = area;
        lastScreenSize = new Vector2Int(Screen.width, Screen.height);
    }
}
