using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Shared rules for talking to Overpass: which mirrors to try, what counts as a
/// real answer, and when another attempt is worth the wait.
///
/// The city downloader already knew about mirrors and rejected an empty element
/// list from a regional extract. The route importer did neither: it posted to one
/// host, accepted any 200 body, and gave up on the first timeout. Both now use
/// this policy so a route import cannot be less reliable than a city download.
/// </summary>
public static class OverpassDownloadPolicy
{
    /// <summary>
    /// Mirrors in the order they are tried. The regional Swiss instance is last
    /// because it only covers its own extract; an empty answer from it is rejected
    /// by <see cref="LooksLikeOverpassJson"/> rather than treated as "no routes".
    /// </summary>
    public static readonly string[] PublicEndpoints =
    {
        "https://overpass-api.de/api/interpreter",
        "https://overpass.kumi.systems/api/interpreter",
        "https://lz4.overpass-api.de/api/interpreter",
        "https://overpass.osm.ch/api/interpreter"
    };

    public const int DefaultAttemptsPerEndpoint = 2;
    public const float BaseRetryDelaySeconds = 1.5f;
    public const float MaxRetryDelaySeconds = 8f;

    /// <summary>
    /// The configured endpoint first, then the public mirrors, with duplicates
    /// removed. An empty or blank preference simply starts with the mirror list.
    /// </summary>
    public static List<string> BuildEndpointList(string preferred)
    {
        var list = new List<string>(PublicEndpoints.Length + 1);
        if (!string.IsNullOrWhiteSpace(preferred))
            list.Add(preferred.Trim());

        for (int i = 0; i < PublicEndpoints.Length; i++)
        {
            string candidate = PublicEndpoints[i];
            bool duplicate = false;
            for (int j = 0; j < list.Count; j++)
            {
                if (string.Equals(list[j], candidate, System.StringComparison.OrdinalIgnoreCase))
                {
                    duplicate = true;
                    break;
                }
            }
            if (!duplicate) list.Add(candidate);
        }
        return list;
    }

    /// <summary>
    /// True when the HTTP outcome is transient. A malformed query (400), a refused
    /// credential (401/403) or a missing endpoint (404) will fail the same way on
    /// every mirror, so the caller should move on rather than burn its budget.
    /// </summary>
    public static bool IsRetryable(UnityWebRequest.Result result, long status, string error)
    {
        if (result == UnityWebRequest.Result.Success)
            return false;

        if (status == 400 || status == 401 || status == 403 || status == 404)
            return false;

        if (status == 429) return true;
        if (status >= 500) return true;
        if (result == UnityWebRequest.Result.DataProcessingError) return true;

        // No status means the request never reached a response: timeout, DNS,
        // TLS or a dropped connection. All are worth one more attempt.
        if (status == 0 && result == UnityWebRequest.Result.ConnectionError)
            return true;

        return !string.IsNullOrEmpty(error);
    }

    /// <summary>
    /// Backoff for the next attempt, honouring a server's Retry-After when present.
    /// Reuses the map download policy's tested curve so the two network paths do
    /// not grow different ideas of "exponential".
    /// </summary>
    public static float RetryDelaySeconds(int failedAttempt, string retryAfter, float jitter01 = 0f)
    {
        float delay = MapDownloadPolicy.RetryDelaySeconds(
            Mathf.Max(1, failedAttempt), retryAfter, jitter01);
        return Mathf.Min(MaxRetryDelaySeconds, Mathf.Max(BaseRetryDelaySeconds, delay));
    }

    /// <summary>
    /// A usable Overpass answer is a JSON object with at least one element. An HTML
    /// error page, a proxy notice, a truncated body and a mirror's empty extract all
    /// fail here, so they cannot overwrite a real city pack or look like "zero routes".
    /// </summary>
    public static bool LooksLikeOverpassJson(string json)
    {
        if (string.IsNullOrWhiteSpace(json) || json.Length < 20)
            return false;

        // Cheap reject before the JSON parser allocates: Overpass returns an object,
        // while error pages are HTML and gateway errors are often plain text.
        int i = 0;
        while (i < json.Length && char.IsWhiteSpace(json[i])) i++;
        if (i >= json.Length || json[i] != '{')
            return false;

        try
        {
            OverpassResponse response = OverpassResponse.Deserialize(json);
            return response?.elements != null && response.elements.Count > 0;
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    /// <summary>Short, player-facing reason for a failed Overpass request.</summary>
    public static string Describe(UnityWebRequest.Result result, long status, string error)
    {
        if (status == 429) return "Overpass is rate-limiting this device";
        if (status >= 500) return "Overpass is temporarily unavailable";
        if (status == 400) return "Overpass rejected the query";
        if (status == 401 || status == 403) return "Overpass refused the request";
        if (status == 404) return "The Overpass endpoint was not found";
        if (!string.IsNullOrEmpty(error) &&
            (error.IndexOf("timed out", System.StringComparison.OrdinalIgnoreCase) >= 0 ||
             error.IndexOf("timeout", System.StringComparison.OrdinalIgnoreCase) >= 0))
            return "Overpass timed out";
        if (result == UnityWebRequest.Result.DataProcessingError)
            return "Overpass returned a malformed response";
        return "Overpass could not be reached";
    }
}
