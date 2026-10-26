using System;
using UnityEngine;

/// <summary>
/// Single source of truth for the Mapbox tile request the game issues, plus the wording
/// needed to explain a refusal.
///
/// Three call sites used to build this URL independently (the live loader, the offline
/// cache and the streaming manager) and a fourth would have been needed for diagnostics.
/// They now share this builder, so a probe cannot drift from what the game actually sends.
/// </summary>
public static class MapboxTileRequest
{
    [Serializable]
    class TokenClaims
    {
        public string u;   // Mapbox user
        public string a;   // authorization id
    }

    [Serializable]
    class TokenConfiguration { public string AccessToken; }

    public const string TileEndpoint = "https://api.mapbox.com/styles/v1";
    public const string TokenValidationEndpoint = "https://api.mapbox.com/tokens/v2";

    /// <summary>Resources path holding the shipped <c>{"AccessToken":"pk.…"}</c> value.</summary>
    public const string ConfigResourcePath = "Mapbox/MapboxConfiguration";

    /// <summary>
    /// Resolves the credential a request will carry: the inspector value when it has one,
    /// otherwise the shipped configuration resource. This is deliberately synchronous and
    /// free of scene state — the loader used to wait for an active city before reading the
    /// resource, so a route started before that sent <c>access_token=</c> and collected
    /// 401 "Direct access not allowed" from Mapbox.
    /// </summary>
    public static string ResolveConfiguredToken(string inspectorToken, out string source)
    {
        if (!string.IsNullOrWhiteSpace(inspectorToken))
        {
            source = "inspector field";
            return inspectorToken.Trim();
        }

        var config = Resources.Load<TextAsset>(ConfigResourcePath);
        if (config != null)
        {
            try
            {
                string token = JsonUtility.FromJson<TokenConfiguration>(config.text)?.AccessToken?.Trim() ?? "";
                if (!string.IsNullOrEmpty(token))
                {
                    source = $"Resources/{ConfigResourcePath}.txt";
                    return token;
                }
            }
            catch (ArgumentException)
            {
                Debug.LogWarning($"MapboxTileRequest: Resources/{ConfigResourcePath}.txt is not valid JSON.");
            }
        }

        source = $"nowhere — set the inspector field or add Resources/{ConfigResourcePath}.txt";
        return "";
    }

    /// <summary>The raster tile URL the game requests. <paramref name="style"/> is a
    /// style id such as <c>mapbox/satellite-v9</c>.</summary>
    public static string BuildTileUrl(string style, int zoom, int tileX, int tileY, string token) =>
        BuildTileUrl(style, zoom, tileX, tileY, token, 2);

    /// <summary>
    /// The raster tile URL at an explicit pixel ratio. Mapbox's Styles raster
    /// endpoint serves the 512 px tile without a suffix and the 1024 px tile with
    /// <c>@2x</c>. The 1x form is a quarter of the texture memory and is what a
    /// low-tier phone uses; the 2x form keeps the authored visual quality.
    /// </summary>
    public static string BuildTileUrl(string style, int zoom, int tileX, int tileY,
        string token, int pixelRatio)
    {
        string suffix = pixelRatio >= 2 ? "@2x" : string.Empty;
        return $"{TileEndpoint}/{style}/tiles/512/{zoom}/{tileX}/{tileY}{suffix}?access_token={token}";
    }

    /// <summary>
    /// Describes a token without exposing it: Mapbox tokens are three dot-separated parts
    /// (prefix, base64url claims, signature), and only the public claims are reported.
    /// </summary>
    public static string DescribeToken(string token)
    {
        if (string.IsNullOrEmpty(token))
            return "(empty — no token was sent, which Mapbox refuses as 'Direct access not allowed')";

        if (token.StartsWith("sk.", StringComparison.Ordinal))
            return "(SECRET sk. token — must not ship in a client build)";

        if (!token.StartsWith("pk.", StringComparison.Ordinal))
            return $"(not a Mapbox token: {token.Length} chars, no 'pk.' prefix)";

        if (!TryReadClaims(token, out string user, out string authorization))
            return $"(pk. token, {token.Length} chars, claims unreadable)";

        return $"(pk. user={user} authorization={authorization}, {token.Length} chars)";
    }

    /// <summary>Replaces the live token inside a URL with its description so a log line or
    /// a probe report can be shared without leaking the credential.</summary>
    public static string RedactToken(string url, string token)
        => string.IsNullOrEmpty(token) || string.IsNullOrEmpty(url)
            ? url
            : url.Replace(token, DescribeToken(token));

    /// <summary>
    /// Turns Mapbox's own response wording into a statement of the cause. The three cases
    /// are genuinely different problems and were previously collapsed into one hint.
    /// </summary>
    public static string ClassifyAuthFailure(long status, string body)
    {
        if (status == 403)
            return "the token authenticated but is not allowed this style — it needs the styles:tiles " +
                   "(and styles:read) scope, access to the style, and no URL restriction that excludes Unity";

        if (status != 401 && status != 403)
            return $"HTTP {status} is not an authorization failure";

        if (body != null && body.IndexOf("Invalid Token", StringComparison.OrdinalIgnoreCase) >= 0)
            return "the token itself was rejected as invalid — it is wrong, was rotated, or belongs to " +
                   "another account";

        if (body != null && body.IndexOf("Direct access not allowed", StringComparison.OrdinalIgnoreCase) >= 0)
            return "no usable token travelled with the request — the empty string reached the URL";

        if (string.IsNullOrWhiteSpace(body))
            return "HTTP 401 with no diagnostic body — the token was missing or the host was reached " +
                   "without it";

        return "the token was refused (Mapbox returned 401 without a recognisable reason)";
    }

    /// <summary>
    /// Reads a request URL back into the two things a refusal report needs: the style it
    /// asked for and the token that actually travelled in its query string. Reporting from
    /// the loader's mutable fields instead produced self-contradicting errors — a single
    /// line could claim "the empty string reached the URL" and then describe a 93-character
    /// token, because the token was resolved between the request being sent and the
    /// failure being logged.
    /// </summary>
    public static bool TryReadRequest(string url, out string style, out string token)
    {
        style = null;
        token = null;

        if (string.IsNullOrEmpty(url))
            return false;

        int styleStart = url.IndexOf("/styles/v1/", StringComparison.Ordinal);
        if (styleStart < 0)
            return false;
        styleStart += "/styles/v1/".Length;

        int styleEnd = url.IndexOf("/tiles/", styleStart, StringComparison.Ordinal);
        if (styleEnd <= styleStart)
            return false;
        style = url.Substring(styleStart, styleEnd - styleStart);

        const string tokenKey = "access_token=";
        int tokenStart = url.IndexOf(tokenKey, styleEnd, StringComparison.Ordinal);
        if (tokenStart < 0)
        {
            // The parameter is absent: the request asked for the tile anonymously.
            token = "";
            return true;
        }

        tokenStart += tokenKey.Length;
        int tokenEnd = url.IndexOf('&', tokenStart);
        token = tokenEnd < 0 ? url.Substring(tokenStart) : url.Substring(tokenStart, tokenEnd - tokenStart);
        return true;
    }

    /// <summary>True when the value looks like a public Mapbox token.</summary>
    public static bool IsPublicToken(string token)
        => !string.IsNullOrEmpty(token) && token.StartsWith("pk.ey", StringComparison.Ordinal);

    /// <summary>Reads the public claims of a public token. Secrets never leave the token.</summary>
    public static bool TryReadClaims(string token, out string user, out string authorization)
    {
        user = null;
        authorization = null;

        if (string.IsNullOrEmpty(token) || !token.StartsWith("pk.", StringComparison.Ordinal))
            return false;

        string[] parts = token.Split('.');
        if (parts.Length < 2 || parts[1].Length == 0)
            return false;

        string payload = parts[1].Replace('-', '+').Replace('_', '/');
        payload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');

        try
        {
            string json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(payload));
            TokenClaims claims = JsonUtility.FromJson<TokenClaims>(json);
            if (claims == null || string.IsNullOrEmpty(claims.u))
                return false;

            user = claims.u;
            authorization = claims.a;
            return true;
        }
        catch (Exception exception) when (exception is FormatException || exception is ArgumentException)
        {
            return false;
        }
    }
}
