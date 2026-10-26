using UnityEngine;
using UnityEngine.Networking;

/// <summary>Why a map tile request did not produce an image.</summary>
public enum MapTileFailureKind
{
    None = 0,
    /// <summary>No network route to Mapbox; a cached tile may still be usable.</summary>
    Offline,
    /// <summary>No public token was resolved, so nothing was sent.</summary>
    NoToken,
    /// <summary>Mapbox refused the credential (401/403). Retrying cannot help.</summary>
    Auth,
    /// <summary>HTTP 429. Retry only after the advertised delay.</summary>
    RateLimited,
    /// <summary>HTTP 5xx. Usually transient.</summary>
    ServerError,
    /// <summary>The request timed out before a response arrived.</summary>
    Timeout,
    /// <summary>A connection error that was not a timeout.</summary>
    Network,
    /// <summary>HTTP 4xx other than 401/403/429 (a bad style id, a 404 tile).</summary>
    ProtocolError,
    /// <summary>A 2xx response whose body was not a supported image.</summary>
    InvalidResponse,
    /// <summary>The bytes arrived but could not be written to the tile cache.</summary>
    CacheWrite
}

/// <summary>
/// Pure rules for the map tile download path: what a failure means, whether it is
/// worth retrying, how long to wait, how many requests may run at once, and how
/// large a tile texture should be on this device.
///
/// These live outside <see cref="OfflineCacheManager"/> so they can be tested
/// without a network or a scene, and so the live loader, the pre-downloader and
/// the cache all classify a failure the same way.
/// </summary>
public static class MapDownloadPolicy
{
    public const int DefaultMaxAttempts = 3;
    public const float BaseRetryDelaySeconds = 0.75f;
    public const float MaxRetryDelaySeconds = 8f;
    public const float MaxRetryAfterSeconds = 30f;

    public const int DesktopConcurrentDownloads = 4;
    public const int MobileConcurrentDownloads = 3;

    /// <summary>Mapbox raster tiles are 512 px; @2x doubles that to 1024 px.</summary>
    public const int TilePixelsAt1x = 512;

    /// <summary>Mapbox's caching guidance for raster tiles; configurable on the cache.</summary>
    public const int DefaultCacheMaxAgeDays = 30;

    /// <summary>
    /// Reads a UnityWebRequest outcome as one of the cases the UI and the retry
    /// loop need to distinguish. <paramref name="tokenSent"/> keeps a tokenless
    /// 401 (a bug in the caller) separate from a genuine credential refusal.
    /// </summary>
    public static MapTileFailureKind Classify(UnityWebRequest.Result result, long status,
        string error, bool tokenSent)
    {
        if (result == UnityWebRequest.Result.Success)
            return MapTileFailureKind.None;

        if (status == 401 || status == 403)
            return tokenSent ? MapTileFailureKind.Auth : MapTileFailureKind.NoToken;

        if (status == 429)
            return MapTileFailureKind.RateLimited;

        if (status >= 500)
            return MapTileFailureKind.ServerError;

        if (status >= 400)
            return MapTileFailureKind.ProtocolError;

        if (result == UnityWebRequest.Result.DataProcessingError)
            return MapTileFailureKind.InvalidResponse;

        // A timeout is reported as a connection error with no HTTP status. The
        // error string is the only signal Unity gives us.
        if (!string.IsNullOrEmpty(error) &&
            (error.IndexOf("timed out", System.StringComparison.OrdinalIgnoreCase) >= 0 ||
             error.IndexOf("timeout", System.StringComparison.OrdinalIgnoreCase) >= 0))
            return MapTileFailureKind.Timeout;

        return MapTileFailureKind.Network;
    }

    /// <summary>True when another attempt can reasonably succeed.</summary>
    public static bool IsRetryable(MapTileFailureKind kind)
    {
        switch (kind)
        {
            case MapTileFailureKind.Timeout:
            case MapTileFailureKind.Network:
            case MapTileFailureKind.RateLimited:
            case MapTileFailureKind.ServerError:
            case MapTileFailureKind.InvalidResponse:
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// Exponential backoff after <paramref name="failedAttempts"/> failures
    /// (1 after the first failure). Deterministic by default; the runtime caller
    /// can pass a jitter value so a fleet of tiles does not retry in lockstep.
    /// </summary>
    public static float RetryDelaySeconds(int failedAttempts, float jitter01 = 0.5f)
    {
        int attempt = Mathf.Clamp(failedAttempts, 1, 8);
        float delay = BaseRetryDelaySeconds * Mathf.Pow(2f, attempt - 1);
        delay = Mathf.Min(MaxRetryDelaySeconds, delay);
        // Jitter is symmetric around the nominal delay: 0 is 15% early, 0.5 is
        // exactly nominal, 1 is 15% late. Callers pass Random.value so two tiles
        // failing together do not retry on the same frame.
        delay *= Mathf.Lerp(0.85f, 1.15f, Mathf.Clamp01(jitter01));
        return delay;
    }

    /// <summary>
    /// Delay for a retry, honouring a server's <c>Retry-After</c> header when it is
    /// longer than the local backoff. Accepts both the seconds and HTTP-date forms.
    /// </summary>
    public static float RetryDelaySeconds(int failedAttempts, string retryAfterHeader,
        float jitter01 = 0.5f)
    {
        float delay = RetryDelaySeconds(failedAttempts, jitter01);
        if (TryParseRetryAfter(retryAfterHeader, out float serverDelay))
            delay = Mathf.Max(delay, serverDelay);
        return Mathf.Min(MaxRetryAfterSeconds, delay);
    }

    public static bool TryParseRetryAfter(string value, out float seconds)
    {
        seconds = 0f;
        if (string.IsNullOrWhiteSpace(value))
            return false;

        value = value.Trim();
        if (float.TryParse(value, System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture, out float numeric))
        {
            seconds = Mathf.Clamp(numeric, 0f, MaxRetryAfterSeconds);
            return true;
        }

        if (System.DateTimeOffset.TryParse(value,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.AssumeUniversal, out System.DateTimeOffset when))
        {
            seconds = Mathf.Clamp((float)(when - System.DateTimeOffset.UtcNow).TotalSeconds,
                0f, MaxRetryAfterSeconds);
            return true;
        }

        return false;
    }

    /// <summary>Number of attempts to make for a tile, including the first one.</summary>
    public static int ResolveMaxAttempts(int authored) =>
        Mathf.Clamp(authored <= 0 ? DefaultMaxAttempts : authored, 1, 6);

    /// <summary>How many tile requests may be in flight at once.</summary>
    public static int ResolveConcurrentDownloads(bool mobile, int authored)
    {
        int fallback = mobile ? MobileConcurrentDownloads : DesktopConcurrentDownloads;
        return Mathf.Clamp(authored <= 0 ? fallback : authored, 1, 8);
    }

    /// <summary>
    /// Raster resolution for this device. 1x is a quarter of the texture memory of
    /// @2x. Mobile Low/Medium keeps 1x; High and desktop keep @2x. An authored
    /// value of 1 or 2 always wins.
    /// </summary>
    public static int ResolvePixelRatio(int authored, bool mobile, int qualityLevel)
    {
        if (authored == 1 || authored == 2)
            return authored;

        if (mobile)
            return qualityLevel <= 2 ? 1 : 2;

        return qualityLevel <= 1 ? 1 : 2;
    }

    /// <summary>
    /// Tile window edge length. A 7x7 window at zoom 17 is 49 tiles; 5x5 is 25,
    /// and still covers more than the 1 km camera far clip. Mobile Low/Medium
    /// gets 5, everything else keeps the authored size (capped at 7 for memory).
    /// </summary>
    public static int RecommendedWindowSize(bool mobile, int qualityLevel, int authored)
    {
        int target = mobile && qualityLevel <= 2 ? 5 : 7;
        if (authored <= 0) return target;
        if (mobile && authored > target) return target;
        return Mathf.Min(authored, 7);
    }

    /// <summary>Approximate GPU bytes for one tile texture, including mipmaps.</summary>
    public static long EstimateTileTextureBytes(int pixelRatio, bool mipmaps = true)
    {
        long edge = (long)TilePixelsAt1x * Mathf.Clamp(pixelRatio, 1, 2);
        long bytes = edge * edge * 4; // RGBA32
        return mipmaps ? bytes + bytes / 3 : bytes;
    }

    /// <summary>Approximate GPU megabytes for a full square tile window.</summary>
    public static float EstimateWindowTextureMB(int windowSize, int pixelRatio, bool mipmaps = true)
    {
        int edge = Mathf.Clamp(windowSize, 1, 9);
        long tiles = (long)edge * edge;
        return tiles * EstimateTileTextureBytes(pixelRatio, mipmaps) / (1024f * 1024f);
    }

    /// <summary>
    /// Cheap image sniffing, so a 200 response carrying an HTML error page is
    /// rejected before a Texture2D is allocated. Mapbox serves PNG and JPEG
    /// raster tiles.
    /// </summary>
    public static bool LooksLikeImage(byte[] bytes)
    {
        if (bytes == null || bytes.Length < 16)
            return false;

        // PNG: 89 50 4E 47 0D 0A 1A 0A
        if (bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47)
            return true;

        // JPEG: FF D8 FF
        if (bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF)
            return true;

        // WebP: RIFF....WEBP
        if (bytes.Length >= 12 && bytes[0] == (byte)'R' && bytes[1] == (byte)'I' &&
            bytes[2] == (byte)'F' && bytes[3] == (byte)'F' &&
            bytes[8] == (byte)'W' && bytes[9] == (byte)'E' &&
            bytes[10] == (byte)'B' && bytes[11] == (byte)'P')
            return true;

        return false;
    }

    /// <summary>Short player-facing description of a failure.</summary>
    public static string Describe(MapTileFailureKind kind)
    {
        switch (kind)
        {
            case MapTileFailureKind.Offline: return "No network connection";
            case MapTileFailureKind.NoToken: return "No Mapbox access token is configured";
            case MapTileFailureKind.Auth: return "Mapbox refused the access token";
            case MapTileFailureKind.RateLimited: return "Mapbox is rate-limiting this device";
            case MapTileFailureKind.ServerError: return "Mapbox is temporarily unavailable";
            case MapTileFailureKind.Timeout: return "The map server timed out";
            case MapTileFailureKind.Network: return "The map server could not be reached";
            case MapTileFailureKind.ProtocolError: return "The map style or tile was rejected";
            case MapTileFailureKind.InvalidResponse: return "Mapbox returned an invalid image";
            case MapTileFailureKind.CacheWrite: return "The tile could not be saved";
            default: return "Map tiles are unavailable";
        }
    }
}
