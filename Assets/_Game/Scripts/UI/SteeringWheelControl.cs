using UnityEngine;
using UnityEngine.EventSystems;

public class SteeringWheelControl : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float maximumRotation = 135f;
    public float Value { get; private set; }
    int pointerId = int.MinValue;
    public void OnPointerDown(PointerEventData data)
    {
        if (pointerId != int.MinValue) return;
        pointerId = data.pointerId;
        OnDrag(data);
    }
    public void OnDrag(PointerEventData data)
    {
        if (pointerId != data.pointerId) return;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform,
            data.position, data.pressEventCamera, out Vector2 point))
            Value = Mathf.Clamp(Mathf.Atan2(point.x, point.y) * Mathf.Rad2Deg / Mathf.Max(1f, maximumRotation), -1f, 1f);
    }
    public void OnPointerUp(PointerEventData data) { if (pointerId == data.pointerId) ResetInput(); }
    public void ResetInput() { Value = 0f; pointerId = int.MinValue; }
    void OnDisable() { ResetInput(); }
}
