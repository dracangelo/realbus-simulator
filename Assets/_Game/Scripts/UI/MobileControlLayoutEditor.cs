using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class MobileControlLayoutEditor : MonoBehaviour
{
    const string KeyPrefix = "RealBus.ControlLayout.v1.";

    struct LayoutSnapshot
    {
        public Vector2 anchorMin, anchorMax, pivot, position;
        public Vector3 scale;
    }

    MobileControlsUI controlsUI;
    RectTransform[] controls = new RectTransform[0];
    LayoutSnapshot[] defaults = new LayoutSnapshot[0];
    readonly Dictionary<RectTransform, int> indices = new Dictionary<RectTransform, int>();
    GameObject editorOverlay;
    TextMeshProUGUI selectionLabel;
    int selectedIndex = -1;
    float profileScale = 1f;
    bool leftHanded;
    bool editing;

    public bool IsEditing => editing;

    public void Bind(MobileControlsUI owner)
    {
        if (owner == null) return;
        controlsUI = owner;
        RectTransform[] next = owner.GetEditableControls();
        if (SameControls(next)) return;
        controls = next; defaults = new LayoutSnapshot[controls.Length]; indices.Clear();
        for (int i = 0; i < controls.Length; i++)
        {
            RectTransform r = controls[i];
            defaults[i] = Capture(r); indices[r] = i; AddEditingEvents(r);
        }
        BuildOverlay();
    }

    public void ApplyProfile(float scale, bool useLeftHanded)
    {
        profileScale = Mathf.Clamp(scale, 0.5f, 1.5f); leftHanded = useLeftHanded;
        for (int i = 0; i < controls.Length; i++) ApplyControl(i);
    }

    public void SetEditing(bool value)
    {
        editing = value;
        if (editorOverlay != null) editorOverlay.SetActive(value);
        for (int i = 0; i < controls.Length; i++)
        {
            Button button = controls[i] != null ? controls[i].GetComponent<Button>() : null;
            if (button != null) button.interactable = !value;
        }
        if (!value) { selectedIndex = -1; PlayerPrefs.Save(); Phase7MenuController.Instance?.ReturnFromControlLayout(); }
        RefreshSelection();
    }

    public void ToggleEditing() { SetEditing(!editing); }

    public void ResetCurrentProfile()
    {
        for (int i = 0; i < controls.Length; i++)
        {
            PlayerPrefs.DeleteKey(Key(i, "x")); PlayerPrefs.DeleteKey(Key(i, "y")); PlayerPrefs.DeleteKey(Key(i, "s"));
            ApplyControl(i);
        }
        PlayerPrefs.Save();
    }

    void ApplyControl(int index)
    {
        if (index < 0 || index >= controls.Length || controls[index] == null) return;
        RectTransform r = controls[index]; LayoutSnapshot d = defaults[index];
        if (leftHanded)
        {
            r.anchorMin = new Vector2(1f - d.anchorMax.x, d.anchorMin.y);
            r.anchorMax = new Vector2(1f - d.anchorMin.x, d.anchorMax.y);
            r.pivot = new Vector2(1f - d.pivot.x, d.pivot.y);
            r.anchoredPosition = new Vector2(-d.position.x, d.position.y);
        }
        else
        {
            r.anchorMin = d.anchorMin; r.anchorMax = d.anchorMax; r.pivot = d.pivot; r.anchoredPosition = d.position;
        }
        if (PlayerPrefs.HasKey(Key(index, "x")))
            r.anchoredPosition = new Vector2(PlayerPrefs.GetFloat(Key(index, "x")), PlayerPrefs.GetFloat(Key(index, "y")));
        float individualScale = PlayerPrefs.GetFloat(Key(index, "s"), 1f);
        r.localScale = d.scale * profileScale * Mathf.Clamp(individualScale, 0.65f, 1.6f);
    }

    void AddEditingEvents(RectTransform control)
    {
        EventTrigger trigger = control.GetComponent<EventTrigger>();
        if (trigger == null) trigger = control.gameObject.AddComponent<EventTrigger>();
        AddEvent(trigger, EventTriggerType.PointerDown, data => Select(control));
        AddEvent(trigger, EventTriggerType.Drag, data => Drag(control, (PointerEventData)data));
        AddEvent(trigger, EventTriggerType.EndDrag, data => Save(control));
    }

    void Select(RectTransform control)
    {
        if (!editing || !indices.TryGetValue(control, out selectedIndex)) return;
        RefreshSelection();
    }

    void Drag(RectTransform control, PointerEventData data)
    {
        if (!editing || control == null || data == null) return;
        Canvas canvas = control.GetComponentInParent<Canvas>();
        control.anchoredPosition += data.delta / Mathf.Max(0.01f, canvas != null ? canvas.scaleFactor : 1f);
        ClampToParent(control); Save(control);
    }

    void ResizeSelected(float delta)
    {
        if (selectedIndex < 0 || selectedIndex >= controls.Length) return;
        float current = PlayerPrefs.GetFloat(Key(selectedIndex, "s"), 1f);
        PlayerPrefs.SetFloat(Key(selectedIndex, "s"), Mathf.Clamp(current + delta, 0.65f, 1.6f));
        ApplyControl(selectedIndex); Save(controls[selectedIndex]); RefreshSelection();
    }

    void Save(RectTransform control)
    {
        if (!editing || control == null || !indices.TryGetValue(control, out int index)) return;
        PlayerPrefs.SetFloat(Key(index, "x"), control.anchoredPosition.x);
        PlayerPrefs.SetFloat(Key(index, "y"), control.anchoredPosition.y);
    }

    void ClampToParent(RectTransform control)
    {
        RectTransform parent = control.parent as RectTransform;
        if (parent == null) return;
        Vector3[] corners = new Vector3[4]; control.GetWorldCorners(corners);
        Vector3 min = parent.TransformPoint(parent.rect.min); Vector3 max = parent.TransformPoint(parent.rect.max);
        Vector3 correction = Vector3.zero;
        if (corners[0].x < min.x) correction.x += min.x - corners[0].x;
        if (corners[2].x > max.x) correction.x -= corners[2].x - max.x;
        if (corners[0].y < min.y) correction.y += min.y - corners[0].y;
        if (corners[2].y > max.y) correction.y -= corners[2].y - max.y;
        control.position += correction;
    }

    void BuildOverlay()
    {
        if (editorOverlay != null) Destroy(editorOverlay);
        editorOverlay = new GameObject("ControlLayoutEditor", typeof(RectTransform), typeof(Image));
        RectTransform root = editorOverlay.GetComponent<RectTransform>(); root.SetParent(transform, false); root.anchorMin = new Vector2(0.5f, 1f); root.anchorMax = new Vector2(0.5f, 1f); root.pivot = new Vector2(0.5f, 1f); root.sizeDelta = new Vector2(1040f, 74f); root.anchoredPosition = new Vector2(0f, -24f);
        editorOverlay.GetComponent<Image>().color = new Color(0.04f, 0.06f, 0.07f, 0.94f);
        selectionLabel = CreateText(root, "Tap a control, then drag or resize", new Vector2(-110f, 0f), new Vector2(430f, 64f));
        CreateButton(root, "−", new Vector2(150f, 0f), () => ResizeSelected(-0.1f));
        CreateButton(root, "+", new Vector2(218f, 0f), () => ResizeSelected(0.1f));
        CreateButton(root, "RESET", new Vector2(300f, 0f), ResetCurrentProfile, 120f);
        CreateButton(root, "DONE", new Vector2(430f, 0f), () => SetEditing(false), 120f);
        editorOverlay.SetActive(false);
    }

    Button CreateButton(Transform parent, string label, Vector2 position, UnityEngine.Events.UnityAction action, float width = 58f)
    {
        GameObject o = new GameObject("Button_" + label, typeof(RectTransform), typeof(Image), typeof(Button)); o.transform.SetParent(parent, false);
        RectTransform r = o.GetComponent<RectTransform>(); r.sizeDelta = new Vector2(width, 52f); r.anchoredPosition = position;
        o.GetComponent<Image>().color = UITheme.Accent; Button b = o.GetComponent<Button>(); b.onClick.AddListener(action);
        TextMeshProUGUI text = CreateText(r, label, Vector2.zero, r.sizeDelta); text.color = UITheme.Background; text.alignment = TextAlignmentOptions.Center; return b;
    }

    TextMeshProUGUI CreateText(Transform parent, string value, Vector2 position, Vector2 size)
    {
        GameObject o = new GameObject("Text", typeof(RectTransform), typeof(TextMeshProUGUI)); o.transform.SetParent(parent, false);
        RectTransform r = o.GetComponent<RectTransform>(); r.sizeDelta = size; r.anchoredPosition = position;
        TextMeshProUGUI text = o.GetComponent<TextMeshProUGUI>(); text.text = value; text.fontSize = 28f; text.color = UITheme.TextPrimary; text.alignment = TextAlignmentOptions.Left; text.font = UITheme.GetFont(UITheme.FontWeight.Medium); return text;
    }

    void RefreshSelection()
    {
        if (selectionLabel == null) return;
        selectionLabel.text = selectedIndex >= 0 && selectedIndex < controls.Length ? "Selected: " + controls[selectedIndex].name : "Tap a control, then drag or resize";
    }

    bool SameControls(RectTransform[] next)
    {
        if (next == null || next.Length != controls.Length) return false;
        for (int i = 0; i < next.Length; i++) if (next[i] != controls[i]) return false;
        return true;
    }

    string Key(int index, string suffix) { return KeyPrefix + (leftHanded ? "L." : "R.") + index + "." + suffix; }
    static LayoutSnapshot Capture(RectTransform r) { return new LayoutSnapshot { anchorMin = r.anchorMin, anchorMax = r.anchorMax, pivot = r.pivot, position = r.anchoredPosition, scale = r.localScale }; }
    static void AddEvent(EventTrigger trigger, EventTriggerType type, UnityEngine.Events.UnityAction<BaseEventData> action) { var entry = new EventTrigger.Entry { eventID = type }; entry.callback.AddListener(action); trigger.triggers.Add(entry); }
}
