using NUnit.Framework;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// The map download path decides what a failure means, whether to retry, how long
/// to wait and how large a tile is. Those decisions used to be scattered across
/// the loader, the cache and the pre-downloader; these tests pin the shared rules
/// so the three call sites cannot drift apart again.
/// </summary>
public class MapDownloadPolicyTests
{
    // ── Failure classification ────────────────────────────────────────────

    [Test]
    public void Classify_SuccessIsNotAFailure()
        => Assert.AreEqual(MapTileFailureKind.None,
            MapDownloadPolicy.Classify(UnityWebRequest.Result.Success, 200, null, true));

    [Test]
    public void Classify_RefusedCredentialIsAuthFailure()
        => Assert.AreEqual(MapTileFailureKind.Auth,
            MapDownloadPolicy.Classify(UnityWebRequest.Result.ProtocolError, 401, "Unauthorized", true),
            "a 401 on a request that carried a token is a credential refusal, and retrying cannot fix it");

    [Test]
    public void Classify_RefusedRequestWithoutTokenIsNotBlamedOnTheToken()
        => Assert.AreEqual(MapTileFailureKind.NoToken,
            MapDownloadPolicy.Classify(UnityWebRequest.Result.ProtocolError, 401, "Unauthorized", false),
            "the caller sent no credential at all; the player-facing advice is different");

    [Test]
    public void Classify_ForbiddenIsAuthFailure()
        => Assert.AreEqual(MapTileFailureKind.Auth,
            MapDownloadPolicy.Classify(UnityWebRequest.Result.ProtocolError, 403, "Forbidden", true));

    [Test]
    public void Classify_RateLimitAndServerErrorStayDistinct()
    {
        Assert.AreEqual(MapTileFailureKind.RateLimited,
            MapDownloadPolicy.Classify(UnityWebRequest.Result.ProtocolError, 429, "Too Many Requests", true));
        Assert.AreEqual(MapTileFailureKind.ServerError,
            MapDownloadPolicy.Classify(UnityWebRequest.Result.ProtocolError, 503, "Service Unavailable", true));
    }

    [Test]
    public void Classify_OtherClientErrorIsProtocolError()
        => Assert.AreEqual(MapTileFailureKind.ProtocolError,
            MapDownloadPolicy.Classify(UnityWebRequest.Result.ProtocolError, 404, "Not Found", true));

    [Test]
    public void Classify_DataProcessingErrorIsAnInvalidResponse()
        => Assert.AreEqual(MapTileFailureKind.InvalidResponse,
            MapDownloadPolicy.Classify(UnityWebRequest.Result.DataProcessingError, 200, "Cannot parse", true));

    [Test]
    public void Classify_TimeoutIsRecognisedFromTheErrorText()
        => Assert.AreEqual(MapTileFailureKind.Timeout,
            MapDownloadPolicy.Classify(UnityWebRequest.Result.ConnectionError, 0,
                "Request timed out after 12 seconds", true),
            "Unity reports a timeout as a connection error with no status; the text is the only signal");

    [Test]
    public void Classify_PlainConnectionErrorIsNetwork()
        => Assert.AreEqual(MapTileFailureKind.Network,
            MapDownloadPolicy.Classify(UnityWebRequest.Result.ConnectionError, 0,
                "Cannot resolve destination host", true));

    // ── Retryability ──────────────────────────────────────────────────────

    [TestCase(MapTileFailureKind.Timeout, true)]
    [TestCase(MapTileFailureKind.Network, true)]
    [TestCase(MapTileFailureKind.RateLimited, true)]
    [TestCase(MapTileFailureKind.ServerError, true)]
    [TestCase(MapTileFailureKind.InvalidResponse, true)]
    [TestCase(MapTileFailureKind.Auth, false)]
    [TestCase(MapTileFailureKind.NoToken, false)]
    [TestCase(MapTileFailureKind.Offline, false)]
    [TestCase(MapTileFailureKind.ProtocolError, false)]
    [TestCase(MapTileFailureKind.CacheWrite, false)]
    public void IsRetryable_OnlyTransientFailuresAreWorthAnotherAttempt(MapTileFailureKind kind, bool expected)
        => Assert.AreEqual(expected, MapDownloadPolicy.IsRetryable(kind));

    // ── Backoff ───────────────────────────────────────────────────────────

    [Test]
    public void RetryDelay_DoublesAndCaps()
    {
        Assert.AreEqual(0.75f, MapDownloadPolicy.RetryDelaySeconds(1), 0.001f);
        Assert.AreEqual(1.5f, MapDownloadPolicy.RetryDelaySeconds(2), 0.001f);
        Assert.AreEqual(3f, MapDownloadPolicy.RetryDelaySeconds(3), 0.001f);
        Assert.AreEqual(6f, MapDownloadPolicy.RetryDelaySeconds(4), 0.001f);
        Assert.AreEqual(MapDownloadPolicy.MaxRetryDelaySeconds, MapDownloadPolicy.RetryDelaySeconds(8), 0.001f,
            "an eight-attempt retry must not schedule a minute of waiting");
    }

    [Test]
    public void RetryDelay_JitterStaysWithinFifteenPercent()
    {
        float low = MapDownloadPolicy.RetryDelaySeconds(2, 0f);
        float high = MapDownloadPolicy.RetryDelaySeconds(2, 1f);
        Assert.AreEqual(1.5f * 0.85f, low, 0.001f);
        Assert.AreEqual(1.5f * 1.15f, high, 0.001f);
    }

    [Test]
    public void RetryDelay_HonoursRetryAfterWhenItIsLonger()
    {
        float delay = MapDownloadPolicy.RetryDelaySeconds(1, "5");
        Assert.AreEqual(5f, delay, 0.001f, "the server asked for five seconds; a 0.75 s retry would be rude");
    }

    [Test]
    public void RetryDelay_KeepsLocalBackoffWhenRetryAfterIsShorter()
    {
        float delay = MapDownloadPolicy.RetryDelaySeconds(4, "1");
        Assert.AreEqual(6f, delay, 0.001f);
    }

    [Test]
    public void RetryDelay_CapsRetryAfter()
    {
        float delay = MapDownloadPolicy.RetryDelaySeconds(1, "3600");
        Assert.AreEqual(MapDownloadPolicy.MaxRetryAfterSeconds, delay, 0.001f,
            "a hostile or broken header must not freeze tile loading for an hour");
    }

    [Test]
    public void RetryAfter_ParsesSecondsAndHttpDates()
    {
        Assert.IsTrue(MapDownloadPolicy.TryParseRetryAfter("2.5", out float seconds));
        Assert.AreEqual(2.5f, seconds, 0.001f);

        string date = System.DateTimeOffset.UtcNow.AddSeconds(6)
            .ToString("R", System.Globalization.CultureInfo.InvariantCulture);
        Assert.IsTrue(MapDownloadPolicy.TryParseRetryAfter(date, out float fromDate));
        Assert.That(fromDate, Is.InRange(4f, 7f));
    }

    [Test]
    public void RetryAfter_RejectsGarbage()
    {
        Assert.IsFalse(MapDownloadPolicy.TryParseRetryAfter(null, out _));
        Assert.IsFalse(MapDownloadPolicy.TryParseRetryAfter("   ", out _));
        Assert.IsFalse(MapDownloadPolicy.TryParseRetryAfter("soon", out _));
    }

    // ── Attempt and concurrency budgets ───────────────────────────────────

    [Test]
    public void MaxAttempts_DefaultsAndClamps()
    {
        Assert.AreEqual(MapDownloadPolicy.DefaultMaxAttempts, MapDownloadPolicy.ResolveMaxAttempts(0));
        Assert.AreEqual(MapDownloadPolicy.DefaultMaxAttempts, MapDownloadPolicy.ResolveMaxAttempts(-4),
            "a negative authored value is an unset field, not a request for zero attempts");
        Assert.AreEqual(1, MapDownloadPolicy.ResolveMaxAttempts(1));
        Assert.AreEqual(6, MapDownloadPolicy.ResolveMaxAttempts(99));
        Assert.AreEqual(4, MapDownloadPolicy.ResolveMaxAttempts(4));
    }

    [Test]
    public void ConcurrentDownloads_DefaultByPlatformAndClamp()
    {
        Assert.AreEqual(MapDownloadPolicy.MobileConcurrentDownloads, MapDownloadPolicy.ResolveConcurrentDownloads(true, 0));
        Assert.AreEqual(MapDownloadPolicy.DesktopConcurrentDownloads, MapDownloadPolicy.ResolveConcurrentDownloads(false, 0));
        Assert.AreEqual(MapDownloadPolicy.DesktopConcurrentDownloads, MapDownloadPolicy.ResolveConcurrentDownloads(false, -1),
            "an unset negative value falls back to the platform default");
        Assert.AreEqual(6, MapDownloadPolicy.ResolveConcurrentDownloads(true, 6));
        Assert.AreEqual(8, MapDownloadPolicy.ResolveConcurrentDownloads(false, 50));
        Assert.AreEqual(1, MapDownloadPolicy.ResolveConcurrentDownloads(false, 1));
    }

    // ── Quality and memory ────────────────────────────────────────────────

    [Test]
    public void PixelRatio_LowTierGetsOneXAndHighTierKeepsTwoX()
    {
        Assert.AreEqual(1, MapDownloadPolicy.ResolvePixelRatio(0, true, 0));
        Assert.AreEqual(1, MapDownloadPolicy.ResolvePixelRatio(0, true, 2));
        Assert.AreEqual(2, MapDownloadPolicy.ResolvePixelRatio(0, true, 3));
        Assert.AreEqual(1, MapDownloadPolicy.ResolvePixelRatio(0, false, 1));
        Assert.AreEqual(2, MapDownloadPolicy.ResolvePixelRatio(0, false, 2));
    }

    [Test]
    public void PixelRatio_AuthoredValueWins()
    {
        Assert.AreEqual(1, MapDownloadPolicy.ResolvePixelRatio(1, false, 5));
        Assert.AreEqual(2, MapDownloadPolicy.ResolvePixelRatio(2, true, 0));
    }

    [Test]
    public void WindowSize_ShrinksOnLowTierPhonesOnly()
    {
        Assert.AreEqual(5, MapDownloadPolicy.RecommendedWindowSize(true, 0, 7));
        Assert.AreEqual(5, MapDownloadPolicy.RecommendedWindowSize(true, 2, 7));
        Assert.AreEqual(7, MapDownloadPolicy.RecommendedWindowSize(true, 3, 7));
        Assert.AreEqual(7, MapDownloadPolicy.RecommendedWindowSize(false, 0, 7));
        Assert.AreEqual(5, MapDownloadPolicy.RecommendedWindowSize(false, 0, 5),
            "an explicitly smaller window must not be enlarged");
        Assert.AreEqual(7, MapDownloadPolicy.RecommendedWindowSize(false, 0, 9),
            "the window is capped at seven so one setting cannot blow the texture budget");
    }

    [Test]
    public void TileTextureEstimate_MatchesTheRealAllocation()
    {
        long oneX = MapDownloadPolicy.EstimateTileTextureBytes(1, mipmaps: false);
        long twoX = MapDownloadPolicy.EstimateTileTextureBytes(2, mipmaps: false);
        Assert.AreEqual(512L * 512L * 4L, oneX);
        Assert.AreEqual(4 * oneX, twoX, "doubling the edge quadruples the bytes");

        float window = MapDownloadPolicy.EstimateWindowTextureMB(7, 2);
        Assert.That(window, Is.InRange(240f, 280f),
            "a 7x7 @2x window is roughly a quarter of a gigabyte; this is the number the loading screen budget is about");
        Assert.That(MapDownloadPolicy.EstimateWindowTextureMB(5, 1), Is.LessThan(window / 4f),
            "the low-tier budget is dramatically smaller, which is why it is the default there");
    }

    // ── Response sniffing ─────────────────────────────────────────────────

    [Test]
    public void LooksLikeImage_AcceptsPngJpegAndWebp()
    {
        byte[] png = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] jpeg = { 0xFF, 0xD8, 0xFF, 0xE0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        byte[] webp = { (byte)'R', (byte)'I', (byte)'F', (byte)'F', 4, 0, 0, 0,
                        (byte)'W', (byte)'E', (byte)'B', (byte)'P', 0, 0, 0, 0 };
        Assert.IsTrue(MapDownloadPolicy.LooksLikeImage(png));
        Assert.IsTrue(MapDownloadPolicy.LooksLikeImage(jpeg));
        Assert.IsTrue(MapDownloadPolicy.LooksLikeImage(webp));
    }

    [Test]
    public void LooksLikeImage_RejectsHtmlAndTruncatedBodies()
    {
        byte[] html = System.Text.Encoding.UTF8.GetBytes("<!DOCTYPE html><html><body>denied</body></html>");
        Assert.IsFalse(MapDownloadPolicy.LooksLikeImage(html),
            "a 200 carrying an error page must be rejected before a texture is allocated");
        Assert.IsFalse(MapDownloadPolicy.LooksLikeImage(new byte[8]));
        Assert.IsFalse(MapDownloadPolicy.LooksLikeImage(null));
    }

    [Test]
    public void Describe_GivesEveryFailureAPlayerFacingSentence()
    {
        foreach (MapTileFailureKind kind in System.Enum.GetValues(typeof(MapTileFailureKind)))
        {
            if (kind == MapTileFailureKind.None) continue;
            string text = MapDownloadPolicy.Describe(kind);
            Assert.IsFalse(string.IsNullOrWhiteSpace(text), $"{kind} needs wording for the loading card");
        }
    }

    // ── Request and cache layout ─────────────────────────────────────────

    [Test]
    public void TileUrl_OneXOmitsTheAt2xSuffix()
    {
        string one = MapboxTileRequest.BuildTileUrl("mapbox/satellite-v9", 17, 5, 6, "pk.test", 1);
        string two = MapboxTileRequest.BuildTileUrl("mapbox/satellite-v9", 17, 5, 6, "pk.test", 2);

        Assert.IsTrue(one.Contains("/17/5/6?access_token="), one);
        Assert.IsTrue(two.Contains("/17/5/6@2x?access_token="), two);
        Assert.AreEqual(two, MapboxTileRequest.BuildTileUrl("mapbox/satellite-v9", 17, 5, 6, "pk.test"),
            "the four-argument form keeps the shipped @2x request");
    }

    [Test]
    public void CachePaths_SeparateOneXFromTwoXWithoutMovingTwoX()
    {
        OfflineCacheManager existing = OfflineCacheManager.Instance;
        OfflineCacheManager cache = null;
        try
        {
            cache = OfflineCacheManager.EnsureInstance();
            string twoX = cache.GetTileCachePath("mapbox/satellite-v9", 17, 10, 20);
            string oneX = cache.GetTileCachePath("mapbox/satellite-v9", 17, 10, 20, 1);
            string twoXExplicit = cache.GetTileCachePath("mapbox/satellite-v9", 17, 10, 20, 2);

            Assert.AreEqual(twoX, twoXExplicit, "the @2x cache layout must not move; existing downloads rely on it");
            Assert.AreNotEqual(twoX, oneX, "a 512 px tile must never be served to a loader that asked for 1024 px");
            Assert.IsTrue(oneX.Contains("@1x"), oneX);
            Assert.IsTrue(oneX.StartsWith(cache.CacheRoot), "both ratios stay inside the cache root");
            Assert.IsTrue(twoX.EndsWith($"{System.IO.Path.DirectorySeparatorChar}20.png"));
        }
        finally
        {
            if (cache != null && cache != existing) Object.DestroyImmediate(cache.gameObject);
        }
    }
}
