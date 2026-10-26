using NUnit.Framework;
using UnityEngine.Networking;

/// <summary>
/// The route importer and the city downloader share one Overpass policy. These
/// tests pin the parts that decide whether a response is real and whether another
/// mirror is worth trying, without making a network request.
/// </summary>
public class OverpassDownloadPolicyTests
{
    const string OneNode =
        "{\"version\":0.6,\"elements\":[{\"type\":\"node\",\"id\":1,\"lat\":-1.28,\"lon\":36.81}]}";

    // ── Endpoint list ─────────────────────────────────────────────────────

    [Test]
    public void EndpointList_PutsTheConfiguredEndpointFirstAndRemovesDuplicates()
    {
        var list = OverpassDownloadPolicy.BuildEndpointList(
            "https://overpass-api.de/api/interpreter");

        Assert.AreEqual("https://overpass-api.de/api/interpreter", list[0]);
        Assert.AreEqual(1, CountOf(list, "https://overpass-api.de/api/interpreter"),
            "the default endpoint must not be tried twice");
        Assert.AreEqual(OverpassDownloadPolicy.PublicEndpoints.Length, list.Count,
            "a preferred endpoint that is already a mirror adds no entry");
    }

    [Test]
    public void EndpointList_KeepsACustomEndpointAndStillFallsBack()
    {
        var list = OverpassDownloadPolicy.BuildEndpointList("https://example.test/interpreter");
        Assert.AreEqual("https://example.test/interpreter", list[0]);
        Assert.AreEqual(OverpassDownloadPolicy.PublicEndpoints.Length + 1, list.Count);
    }

    [Test]
    public void EndpointList_BlankPreferenceStartsWithThePublicMirrors()
    {
        var list = OverpassDownloadPolicy.BuildEndpointList("   ");
        CollectionAssert.AreEqual(OverpassDownloadPolicy.PublicEndpoints, list);
    }

    static int CountOf(System.Collections.Generic.List<string> list, string value)
    {
        int count = 0;
        foreach (string item in list)
            if (item == value) count++;
        return count;
    }

    // ── Retryability ──────────────────────────────────────────────────────

    [TestCase(0, (long)0, true, Description = "a connection that never reached the host")]
    [TestCase(0, (long)429, true, Description = "rate limiting clears")]
    [TestCase(0, (long)500, true, Description = "a mirror having a bad minute")]
    [TestCase(0, (long)503, true, Description = "gateway unavailable")]
    [TestCase(0, (long)400, false, Description = "a malformed query fails the same everywhere")]
    [TestCase(0, (long)401, false, Description = "a refused credential will stay refused")]
    [TestCase(0, (long)403, false, Description = "forbidden is terminal")]
    [TestCase(0, (long)404, false, Description = "a missing endpoint is not transient")]
    public void IsRetryable_TransientFailuresOnly(int result, long status, bool expected)
        => Assert.AreEqual(expected,
            OverpassDownloadPolicy.IsRetryable((UnityWebRequest.Result)result, status, "error"));

    [Test]
    public void IsRetryable_DataProcessingErrorIsWorthAnotherMirror()
        => Assert.IsTrue(OverpassDownloadPolicy.IsRetryable(
            UnityWebRequest.Result.DataProcessingError, 200, "cannot parse"));

    [Test]
    public void IsRetryable_SuccessIsNotARetry()
        => Assert.IsFalse(OverpassDownloadPolicy.IsRetryable(
            UnityWebRequest.Result.Success, 200, null));

    [Test]
    public void RetryDelay_IsBoundedAndNeverShorterThanTheBase()
    {
        Assert.That(OverpassDownloadPolicy.RetryDelaySeconds(1, null), Is.EqualTo(OverpassDownloadPolicy.BaseRetryDelaySeconds).Within(0.001f));
        Assert.That(OverpassDownloadPolicy.RetryDelaySeconds(9, null), Is.LessThanOrEqualTo(OverpassDownloadPolicy.MaxRetryDelaySeconds));
        Assert.That(OverpassDownloadPolicy.RetryDelaySeconds(1, "0"), Is.GreaterThanOrEqualTo(OverpassDownloadPolicy.BaseRetryDelaySeconds));
    }

    // ── Response validation ───────────────────────────────────────────────

    [Test]
    public void LooksLikeJson_AcceptsARealOverpassDocument()
        => Assert.IsTrue(OverpassDownloadPolicy.LooksLikeOverpassJson(OneNode));

    [Test]
    public void LooksLikeJson_RejectsEmptyExtracts()
        => Assert.IsFalse(OverpassDownloadPolicy.LooksLikeOverpassJson("{\"version\":0.6,\"elements\":[]}"),
            "a regional mirror's empty extract must not replace a real city pack");

    [Test]
    public void LooksLikeJson_RejectsHtmlAndPlainText()
    {
        Assert.IsFalse(OverpassDownloadPolicy.LooksLikeOverpassJson(
            "<!DOCTYPE html><html><body>Overpass is down</body></html>"));
        Assert.IsFalse(OverpassDownloadPolicy.LooksLikeOverpassJson("Service Unavailable"));
        Assert.IsFalse(OverpassDownloadPolicy.LooksLikeOverpassJson(null));
        Assert.IsFalse(OverpassDownloadPolicy.LooksLikeOverpassJson("{}"));
    }

    [Test]
    public void LooksLikeJson_RejectsATruncatedDocument()
        => Assert.IsFalse(OverpassDownloadPolicy.LooksLikeOverpassJson("{\"elements\":[{\"type\":"));

    [Test]
    public void Describe_NamesTheFailureForThePlayer()
    {
        StringAssert.Contains("rate-limiting", OverpassDownloadPolicy.Describe(
            UnityWebRequest.Result.ProtocolError, 429, "Too Many Requests"));
        StringAssert.Contains("unavailable", OverpassDownloadPolicy.Describe(
            UnityWebRequest.Result.ProtocolError, 503, "Service Unavailable"));
        StringAssert.Contains("timed out", OverpassDownloadPolicy.Describe(
            UnityWebRequest.Result.ConnectionError, 0, "Request timed out"));
        StringAssert.Contains("could not be reached", OverpassDownloadPolicy.Describe(
            UnityWebRequest.Result.ConnectionError, 0, "Cannot resolve host"));
    }
}
