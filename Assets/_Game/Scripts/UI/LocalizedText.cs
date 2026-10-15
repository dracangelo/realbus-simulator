using TMPro;
using UnityEngine;

/// <summary>
/// Binds a text label to a <see cref="Localization"/> key so it renders in the
/// active language and re-renders when the player changes language.
///
/// The menus are built once at startup and kept alive, so a label cannot just read
/// its string at creation time - it has to react when <c>SettingsManager</c> applies
/// a new language. This is the component that closes that loop.
///
/// The subscription is deliberately made from <see cref="Bind"/> as well as from
/// <c>Awake</c>/<c>OnEnable</c>, and is removed before being re-added so it can never
/// double-register. Waiting on the lifecycle callbacks alone is fragile: they are not
/// invoked for components added while the editor is not playing, and a panel that is
/// closed while the language changes would otherwise reopen showing the old text
/// (which is why <see cref="OnEnable"/> re-applies rather than only re-subscribing).
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
[DisallowMultipleComponent]
public class LocalizedText : MonoBehaviour
{
    [Tooltip("Localization table key, e.g. set.masterVolume.")]
    public string key;

    TMP_Text label;

    /// <summary>
    /// Attaches a binding to an existing text object and applies it immediately.
    /// Returns the created component, or null when no key/object was supplied.
    /// </summary>
    public static LocalizedText Bind(GameObject target, string key)
    {
        if (target == null || string.IsNullOrEmpty(key)) return null;

        LocalizedText binding = target.GetComponent<LocalizedText>();
        if (binding == null) binding = target.AddComponent<LocalizedText>();

        binding.key = key;
        binding.Resolve();
        binding.Subscribe();
        binding.Apply();
        return binding;
    }

    void Awake()
    {
        Resolve();
        Subscribe();
    }

    void OnEnable()
    {
        // Catches up a label that was hidden while the language changed.
        Resolve();
        Subscribe();
        Apply();
    }

    void OnDisable() => Unsubscribe();

    void OnDestroy() => Unsubscribe();

    /// <summary>Caches the text component this binding writes to.</summary>
    void Resolve()
    {
        if (this == null) return;
        if (label == null) label = GetComponent<TMP_Text>();
    }

    void Subscribe()
    {
        if (this == null) return;
        // Removing first keeps a repeated subscribe from registering twice.
        Localization.LanguageChanged -= Apply;
        Localization.LanguageChanged += Apply;
    }

    void Unsubscribe() => Localization.LanguageChanged -= Apply;

    /// <summary>Resolves the key in the active language and writes it to the label.</summary>
    public void Apply()
    {
        if (string.IsNullOrEmpty(key)) return;
        if (this == null) return;

        TMP_Text target = label;
        if (target == null) return;
        target.text = Localization.Get(key);
    }
}
