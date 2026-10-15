using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;

/// <summary>
/// Covers the Mapbox request the game issues. The bug this grew out of was reported as
/// <c>MapTileLoader: Mapbox authorization failed (HTTP 401)</c>, which states a status
/// but not which token was sent or why it was refused — the two things needed to tell a
/// rotated token from an empty one from a missing scope.
///
/// The deterministic tests below pin the URL shape and the four refusal readings. The
/// live test at the end is <see cref="ExplicitAttribute"/>: it needs the network and the
/// configured credential, so it must never run as part of the ordinary suite. Select it
/// with <c>-testFilter MapboxTokenRequestTests.LiveTileRequestIsAuthorised</c> when the
/// question is "does this token actually authorise a tile right now?".
/// </summary>
public class MapboxTokenRequestTests
{
    const string ConfigResourcePath = "Mapbox/MapboxConfiguration";

    /// <summary>A test that publishes a configuration must not leak it into the next one.</summary>
    [TearDown]
    public void ForgetThePublishedConfiguration() => MapboxMapSettings.Reset();

    // A public token whose claims are known, so leakage assertions can be exact.
    const string SampleUser = "realbussim";
    const string SampleAuthorization = "cmmdeuage02b62qqujbqxlneu";
    static string SampleToken => $"pk.{Base64Url($"{{\"u\":\"{SampleUser}\",\"a\":\"{SampleAuthorization}\"}}")}.SIGNATUREPART";

    static string Base64Url(string value) =>
        Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value))
            .Replace('+', '-').Replace('/', '_').TrimEnd('=');

    // ── The request the game sends ────────────────────────────────────────────

    [Test]
    public void Mapbox_TheTileUrlKeepsTheShippedShape()
    {
        string url = MapboxTileRequest.BuildTileUrl("mapbox/satellite-v9", 15, 9949, 13520, SampleToken);

        Assert.AreEqual(
            $"https://api.mapbox.com/styles/v1/mapbox/satellite-v9/tiles/512/15/9949/13520@2x?access_token={SampleToken}",
            url,
            "The loader and the offline cache both route through this builder, so its shape is the shape " +
            "every tile request has.");
    }

    [Test]
    public void Mapbox_TheStyleIdIsNotEscaped()
    {
        // A style id carries its owner as a path segment ('mapbox/satellite-v9'); escaping
        // the slash would ask for a style that does not exist and turn a working request
        // into a 404.
        string url = MapboxTileRequest.BuildTileUrl("realbussim/custom-123", 16, 1, 2, SampleToken);

        Assert.IsTrue(url.Contains("/styles/v1/realbussim/custom-123/tiles/512/16/1/2@2x?"), url);
    }

    // ── Reading the token back out of a log ───────────────────────────────────

    [Test]
    public void Mapbox_DescribingATokenReportsClaimsAndNeverTheSignature()
    {
        string described = MapboxTileRequest.DescribeToken(SampleToken);

        StringAssert.Contains($"user={SampleUser}", described);
        StringAssert.Contains($"authorization={SampleAuthorization}", described);
        StringAssert.DoesNotContain(SampleToken, described, "A description is meant to be quotable in a bug report.");
        StringAssert.DoesNotContain("SIGNATUREPART", described, "The signature must never be printed.");
        StringAssert.Contains("pk.", described);
    }

    [Test]
    public void Mapbox_DescribingAnAbsentTokenSaysWhyThatFails()
    {
        StringAssert.Contains("empty", MapboxTileRequest.DescribeToken(""));
        StringAssert.Contains("empty", MapboxTileRequest.DescribeToken(null));
        // Mapbox answers an empty access_token with 401 "Direct access not allowed", so the
        // description should name that trap rather than merely saying "no token".
        StringAssert.Contains("Direct access not allowed", MapboxTileRequest.DescribeToken(""));
    }

    [Test]
    public void Mapbox_DescribingAValueThatIsNotAPublicTokenFlagsIt()
    {
        StringAssert.Contains("SECRET", MapboxTileRequest.DescribeToken("sk." + Base64Url("{\"u\":\"x\"}") + ".s"));
        StringAssert.Contains("not a Mapbox token", MapboxTileRequest.DescribeToken("hello-world"));
    }

    [Test]
    public void Mapbox_PublicTokenRecognitionMatchesThePrefix()
    {
        Assert.IsTrue(MapboxTileRequest.IsPublicToken(SampleToken));
        Assert.IsTrue(MapboxTileRequest.IsPublicToken(ConfiguredToken()));
        Assert.IsFalse(MapboxTileRequest.IsPublicToken(""));
        Assert.IsFalse(MapboxTileRequest.IsPublicToken("sk.eyJ1IjoieCJ9.sig"));
        Assert.IsFalse(MapboxTileRequest.IsPublicToken("pk-not-really"));
    }

    [Test]
    public void Mapbox_ClaimReadingRefusesWhatItCannotParse()
    {
        Assert.IsFalse(MapboxTileRequest.TryReadClaims("pk.!!!not-base64!!!.sig", out _, out _));
        Assert.IsFalse(MapboxTileRequest.TryReadClaims("sk.eyJ1IjoieCJ9.sig", out _, out _));
        Assert.IsFalse(MapboxTileRequest.TryReadClaims("", out _, out _));

        Assert.IsTrue(MapboxTileRequest.TryReadClaims(SampleToken, out string user, out string authorization));
        Assert.AreEqual(SampleUser, user);
        Assert.AreEqual(SampleAuthorization, authorization);
    }

    [Test]
    public void Mapbox_RedactingAUrlRemovesTheCredential()
    {
        string url = MapboxTileRequest.BuildTileUrl("mapbox/satellite-v9", 15, 9949, 13520, SampleToken);
        string redacted = MapboxTileRequest.RedactToken(url, SampleToken);

        StringAssert.DoesNotContain(SampleToken, redacted);
        StringAssert.DoesNotContain("SIGNATUREPART", redacted);
        StringAssert.Contains($"user={SampleUser}", redacted);
    }

    // ── Reading Mapbox's refusal ──────────────────────────────────────────────

    [Test]
    public void Mapbox_AnInvalidTokenRefusalBlamesTheToken()
    {
        string diagnosis = MapboxTileRequest.ClassifyAuthFailure(401, "{\"message\":\"Not Authorized - Invalid Token\"}");

        StringAssert.Contains("token itself", diagnosis);
        StringAssert.Contains("rotated", diagnosis);
    }

    [Test]
    public void Mapbox_AnEmptyTokenRefusalBlamesTheMissingToken()
    {
        string diagnosis = MapboxTileRequest.ClassifyAuthFailure(
            401, "{\"message\": \"Not Authorized\",  \"error_detail\": \"Direct access not allowed\"}");

        StringAssert.Contains("no usable token", diagnosis);
        StringAssert.DoesNotContain("rotated", diagnosis,
            "An empty token and a wrong token need different fixes, so they must not share wording.");
    }

    [Test]
    public void Mapbox_AForbiddenRefusalBlamesTheScopeNotTheToken()
    {
        string diagnosis = MapboxTileRequest.ClassifyAuthFailure(403, "");

        StringAssert.Contains("styles:tiles", diagnosis);
        StringAssert.DoesNotContain("invalid", diagnosis);
    }

    [Test]
    public void Mapbox_OtherStatusesAreNotReportedAsAuthorizationFailures()
    {
        StringAssert.Contains("not an authorization failure", MapboxTileRequest.ClassifyAuthFailure(200, ""));
        StringAssert.Contains("404", MapboxTileRequest.ClassifyAuthFailure(404, "{\"message\":\"Style not found\"}"));
        StringAssert.Contains("no diagnostic body", MapboxTileRequest.ClassifyAuthFailure(401, ""));
    }

    // ── Reading back what a request actually carried ──────────────────────────

    [Test]
    public void Mapbox_ARequestIsReadBackAsTheStyleAndTokenItCarried()
    {
        string url = MapboxTileRequest.BuildTileUrl("mapbox/satellite-v9", 15, 9949, 13520, SampleToken);

        Assert.IsTrue(MapboxTileRequest.TryReadRequest(url, out string style, out string token));
        Assert.AreEqual("mapbox/satellite-v9", style);
        Assert.AreEqual(SampleToken, token);
    }

    [Test]
    public void Mapbox_ARecoveredTokenStopsAtTheNextQueryParameter()
    {
        string url = MapboxTileRequest.BuildTileUrl("mapbox/satellite-v9", 15, 1, 2, SampleToken) + "&another=1";

        Assert.IsTrue(MapboxTileRequest.TryReadRequest(url, out _, out string token));
        Assert.AreEqual(SampleToken, token, "A trailing parameter must not be read as part of the credential.");
    }

    [Test]
    public void Mapbox_ARequestThatCarriedNoTokenIsReadBackAsEmpty()
    {
        // Exactly the shape the loader sent before its token was resolved. Reading the request
        // back is what lets the refusal report state what travelled, instead of describing
        // whatever the fields happened to hold by the time the response came back.
        string url = MapboxTileRequest.BuildTileUrl("mapbox/satellite-v9", 15, 9949, 13520, "");

        Assert.IsTrue(MapboxTileRequest.TryReadRequest(url, out string style, out string token));
        Assert.AreEqual("mapbox/satellite-v9", style);
        Assert.AreEqual("", token);
        StringAssert.Contains("Direct access not allowed", MapboxTileRequest.DescribeToken(token));
    }

    [Test]
    public void Mapbox_AUrlThatIsNotATileRequestIsNotReadBack()
    {
        Assert.IsFalse(MapboxTileRequest.TryReadRequest("https://api.mapbox.com/tokens/v2/realbussim?access_token=x", out _, out _));
        Assert.IsFalse(MapboxTileRequest.TryReadRequest("https://api.mapbox.com/styles/v1/mapbox/satellite-v9", out _, out _));
        Assert.IsFalse(MapboxTileRequest.TryReadRequest("", out _, out _));
        Assert.IsFalse(MapboxTileRequest.TryReadRequest(null, out _, out _));
    }

    // ── The credential, resolved without a scene ──────────────────────────────

    [Test]
    public void Mapbox_AnInspectorTokenWinsOverTheShippedConfiguration()
    {
        string resolved = MapboxTileRequest.ResolveConfiguredToken($"  {SampleToken}\n", out string source);

        Assert.AreEqual(SampleToken, resolved, "The inspector value is trimmed and used as-is.");
        StringAssert.Contains("inspector", source);
    }

    [Test]
    public void Mapbox_WithNoInspectorTokenTheShippedConfigurationIsUsed()
    {
        string configured = ConfiguredToken();
        if (string.IsNullOrEmpty(configured))
            Assert.Ignore($"No {ConfigResourcePath}.txt in Resources; nothing to fall back to.");

        string resolved = MapboxTileRequest.ResolveConfiguredToken("", out string source);

        Assert.AreEqual(configured, resolved);
        StringAssert.Contains(ConfigResourcePath, source,
            "The log line has to name where the credential came from to be actionable.");
    }

    [Test]
    public void Mapbox_WhitespaceIsNotACredential()
    {
        string configured = ConfiguredToken();
        if (string.IsNullOrEmpty(configured))
            Assert.Ignore($"No {ConfigResourcePath}.txt in Resources; nothing to fall back to.");

        // Whitespace on its own would go onto the wire as 'access_token=   ', which Mapbox
        // refuses as an empty token; it must fall through to the configuration resource.
        Assert.AreEqual(configured, MapboxTileRequest.ResolveConfiguredToken("   ", out _));
    }

    // ── The one resolved configuration every consumer shares ──────────────────

    [Test]
    public void Mapbox_ResolvedSettingsCarryTheInspectorTokenAndTheLabelFreeStyle()
    {
        var settings = MapboxMapSettings.Resolve(
            SampleToken, MapboxMapSettings.LabeledSatelliteStyle, preferLabelFree: true);

        Assert.AreEqual(SampleToken, settings.Token);
        Assert.AreEqual(MapboxMapSettings.LabelFreeSatelliteStyle, settings.Style,
            "The label-free preference belongs to the resolved configuration, not to a silent edit of the scene's field.");
        StringAssert.Contains("inspector", settings.TokenSource);
        Assert.IsTrue(settings.HasToken);
    }

    [Test]
    public void Mapbox_ResolvedSettingsLeaveACustomStyleAlone()
    {
        var settings = MapboxMapSettings.Resolve(SampleToken, "realbussim/custom-123", preferLabelFree: true);

        Assert.AreEqual("realbussim/custom-123", settings.Style);
    }

    [Test]
    public void Mapbox_PublishingReplacesTheConfigurationEveryConsumerReads()
    {
        // The offline cache and the predownloader both read Current, so this is the single place
        // their style and token come from.
        var published = MapboxMapSettings.Resolve(SampleToken, "mapbox/streets-v12", preferLabelFree: true);
        MapboxMapSettings.Publish(published);

        Assert.AreSame(published, MapboxMapSettings.Current);
        Assert.AreEqual("mapbox/streets-v12", MapboxMapSettings.Current.Style);
        Assert.AreEqual(SampleToken, MapboxMapSettings.Current.Token);
    }

    [Test]
    public void Mapbox_AnUnpublishedConfigurationResolvesFromTheShippedResource()
    {
        MapboxMapSettings.Reset();

        string configured = ConfiguredToken();
        if (string.IsNullOrEmpty(configured))
            Assert.Ignore($"No {ConfigResourcePath}.txt in Resources; nothing to resolve.");

        MapboxMapSettings settings = MapboxMapSettings.Current;

        Assert.AreEqual(configured, settings.Token, "A consumer that runs before any loader still gets the shipped credential.");
        Assert.AreEqual(MapboxMapSettings.LabelFreeSatelliteStyle, settings.Style);
        StringAssert.Contains(ConfigResourcePath, settings.TokenSource);
    }

    [Test]
    public void Mapbox_ResettingForgetsWhatWasPublished()
    {
        MapboxMapSettings.Publish(MapboxMapSettings.Resolve(SampleToken, "mapbox/streets-v12", preferLabelFree: true));
        MapboxMapSettings.Reset();

        Assert.AreNotEqual(SampleToken, MapboxMapSettings.Current.Token,
            "Reset must drop the published configuration so the next read resolves again.");
    }

    [Test]
    public void Mapbox_TheSharedConfigurationDescribesItselfWithoutTheSignature()
    {
        string described = MapboxMapSettings.Resolve(SampleToken, "mapbox/satellite-v9", preferLabelFree: true).ToString();

        StringAssert.Contains("mapbox/satellite-v9", described);
        StringAssert.DoesNotContain(SampleToken, described);
        StringAssert.DoesNotContain("SIGNATUREPART", described);
    }

    // ── Which refusals may hold back the downloads ────────────────────────────

    [Test]
    public void Mapbox_OnlyARefusalThatCarriedATokenHoldsBackTheDownloads()
    {
        // A 401 without a token is the loader's own ordering bug. Counting it as an
        // authorization failure used to trip haltDownloadsAfterAuthFailure and leave the map
        // blank for the rest of the session, even though the real token arrived moments later.
        Assert.IsFalse(MapTileLoader.CountsAsAuthFailure(401, ""));
        Assert.IsFalse(MapTileLoader.CountsAsAuthFailure(403, ""));
        Assert.IsFalse(MapTileLoader.CountsAsAuthFailure(200, SampleToken));
        Assert.IsFalse(MapTileLoader.CountsAsAuthFailure(404, SampleToken));
        Assert.IsFalse(MapTileLoader.CountsAsAuthFailure(500, SampleToken));
        Assert.IsTrue(MapTileLoader.CountsAsAuthFailure(401, SampleToken));
        Assert.IsTrue(MapTileLoader.CountsAsAuthFailure(403, SampleToken));
    }

    // ── The configured credential, as the game resolves it ────────────────────

    [Test]
    public void Mapbox_TheConfiguredTokenIsAPublicTokenTheGameCanUse()
    {
        string token = ConfiguredToken();
        if (string.IsNullOrEmpty(token))
            Assert.Ignore($"No {ConfigResourcePath}.txt in Resources; nothing to inspect.");

        Assert.IsTrue(MapboxTileRequest.IsPublicToken(token),
            $"{ConfigResourcePath}.txt must hold a public (pk.) token; it holds {MapboxTileRequest.DescribeToken(token)}.");
        Assert.IsTrue(MapboxTileRequest.TryReadClaims(token, out _, out _),
            $"The configured token does not parse as a Mapbox token: {MapboxTileRequest.DescribeToken(token)}.");
    }

    // ── The live request (opt-in) ─────────────────────────────────────────────

    /// <summary>
    /// Asks Mapbox for the same tile the game asks for, through Unity's own HTTP stack,
    /// and reports Mapbox's answer in full. Excluded from the ordinary suite because it
    /// depends on the network and on a credential that may legitimately be rotated.
    /// </summary>
    [UnityTest]
    [Explicit]
    public IEnumerator LiveTileRequestIsAuthorised()
    {
        string token = ConfiguredToken();
        Assert.IsNotEmpty(token, $"No token in Resources/{ConfigResourcePath}.txt.");

        const string style = "mapbox/satellite-v9";
        const int zoom = 15, tileX = 9949, tileY = 13520; // central Nairobi, the default centre
        string url = MapboxTileRequest.BuildTileUrl(style, zoom, tileX, tileY, token);

        Debug.Log($"[MapboxLive] GET {MapboxTileRequest.RedactToken(url, token)}");

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.timeout = 30;
            request.SetRequestHeader("Accept", "image/png,image/jpeg;q=0.9,*/*;q=0.1");
            yield return request.SendWebRequest();

            long status = request.responseCode;
            string contentType = request.GetResponseHeader("Content-Type") ?? "(none)";
            byte[] bytes = request.downloadHandler != null ? request.downloadHandler.data : null;
            string body = request.downloadHandler != null ? request.downloadHandler.text : "";

            Debug.Log($"[MapboxLive] HTTP {status} | {contentType} | {bytes?.Length ?? 0} bytes | " +
                      $"result={request.result} | token {MapboxTileRequest.DescribeToken(token)}");

            if (status != 200)
                Assert.Fail($"[MapboxLive] HTTP {status} for style '{style}'. " +
                            $"Mapbox said: {MapboxTileRequest.ClassifyAuthFailure(status, body)}. " +
                            $"Body: {body}");

            var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            bool decoded = bytes != null && bytes.Length >= 16 && texture.LoadImage(bytes, false);
            string detail = decoded ? $"{texture.width}x{texture.height}" : "undecodable";
            UnityEngine.Object.DestroyImmediate(texture);

            Assert.IsTrue(decoded, $"[MapboxLive] HTTP 200 but the body was not a PNG/JPEG image ({detail}).");
            Debug.Log($"[MapboxLive] the configured token authorises this tile ({detail}).");
        }
    }

    /// <summary>The shipped credential, read through the same resolver the game uses.</summary>
    static string ConfiguredToken() => MapboxTileRequest.ResolveConfiguredToken("", out _);
}
