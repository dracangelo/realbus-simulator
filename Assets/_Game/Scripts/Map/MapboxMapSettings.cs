using UnityEngine;

/// <summary>
/// The one resolved Mapbox configuration the map stack shares: a single token, a single
/// style and the source the token came from.
///
/// Three components used to hold their own copy of these two values — the tile loader, the
/// city predownloader, and the offline cache (as parameters on every call) — and two of them
/// re-read the same configuration resource. Copies that disagree are invisible until a request
/// is refused, which is exactly how a tokenless request went out while another component held
/// a perfectly good token.
///
/// <see cref="Current"/> resolves from the shipped resource the first time anything needs it,
/// and <see cref="Publish"/> adopts the authoritative values once the scene's loader has
/// resolved them.
/// </summary>
public sealed class MapboxMapSettings
{
    /// <summary>Satellite imagery with street labels — the authored default in the scene.</summary>
    public const string LabeledSatelliteStyle = "mapbox/satellite-streets-v12";

    /// <summary>Label-free satellite imagery, preferred when the loader asks for it.</summary>
    public const string LabelFreeSatelliteStyle = "mapbox/satellite-v9";

    public string Token { get; }
    public string Style { get; }

    /// <summary>Where <see cref="Token"/> came from, so a log line can say which one was used.</summary>
    public string TokenSource { get; }

    public bool HasToken => !string.IsNullOrEmpty(Token);

    MapboxMapSettings(string token, string style, string tokenSource)
    {
        Token = token;
        Style = style;
        TokenSource = tokenSource;
    }

    /// <summary>
    /// Builds a configuration from authored values: the inspector token when it has one,
    /// otherwise the shipped resource; and the label-free satellite style when the caller
    /// prefers it over the labelled one. Synchronous and free of scene state, so it can be
    /// resolved from anywhere — including before a city, a route or a loader exists.
    /// </summary>
    public static MapboxMapSettings Resolve(string inspectorToken, string inspectorStyle, bool preferLabelFree)
    {
        string style = string.IsNullOrWhiteSpace(inspectorStyle) ? LabeledSatelliteStyle : inspectorStyle.Trim();
        if (preferLabelFree && style == LabeledSatelliteStyle)
            style = LabelFreeSatelliteStyle;

        return new MapboxMapSettings(
            MapboxTileRequest.ResolveConfiguredToken(inspectorToken, out string source),
            style,
            source);
    }

    /// <summary>The configuration in force for this run.</summary>
    public static MapboxMapSettings Current =>
        current ?? (current = Resolve(null, LabeledSatelliteStyle, preferLabelFree: true));

    /// <summary>
    /// Adopts an authoritative configuration. The scene's loader calls this as soon as it has
    /// resolved its own fields, which is before any consumer can start downloading.
    /// </summary>
    public static void Publish(MapboxMapSettings settings)
    {
        if (settings != null)
            current = settings;
    }

    /// <summary>Forgets the published configuration so the next read resolves again.</summary>
    public static void Reset() => current = null;

    static MapboxMapSettings current;

    /// <summary>Safe for logs: names the style, and describes the token without its signature.</summary>
    public override string ToString() =>
        $"style '{Style}' with {(HasToken ? MapboxTileRequest.DescribeToken(Token) : "no token")}";
}
