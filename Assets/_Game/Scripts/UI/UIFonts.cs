using UnityEngine;
using TMPro;

/// <summary>
/// Central font registry. Assign in Inspector on a persistent GameObject.
/// </summary>
public class UIFonts : MonoBehaviour
{
    public static UIFonts Instance { get; private set; }

    [Header("Space Grotesk")]
    public TMP_FontAsset bold;
    public TMP_FontAsset medium;
    public TMP_FontAsset regular;
    public TMP_FontAsset lightFont;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        if (transform.parent != null)
            transform.SetParent(null, true);
        DontDestroyOnLoad(gameObject);
    }
}
