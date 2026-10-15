using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Reports the configuration the game resolves and Mapbox's own answer to a real tile
/// request, without needing a play session or reading a raw HTTP status.
///
/// Requirement: the editor's update loop must be running, which it is when this is invoked
/// from the menu. A headless equivalent that drives its own pumping lives in the test
/// assembly as the explicit test <c>MapboxTokenRequestTests.LiveTileRequestIsAuthorised</c>,
/// which is run through the Unity test runner rather than through <c>-executeMethod</c>
/// (a batch-mode editor does not pump <c>EditorApplication.update</c>, so the menu command
/// cannot be reused there).
/// </summary>
public static class MapboxTokenDiagnosticsTool
{
    const int DefaultZoom = 15;
    // Central Nairobi at zoom 15 — the project's default map centre.
    const int DefaultTileX = 9949;
    const int DefaultTileY = 13520;

    static UnityWebRequest activeRequest;
    static double deadline;
    static int exitCode = 1;

    [MenuItem("RealBus/Diagnostics/Verify Mapbox Token")]
    public static void Verify()
    {
        exitCode = 1;

        // The one resolved configuration the game itself uses, rather than a second copy of the
        // resolution rules: outside play mode this reads the shipped resource.
        MapboxMapSettings settings = MapboxMapSettings.Current;
        Debug.Log($"[MapboxProbe] configuration: {settings}; token from {settings.TokenSource}");
        Debug.Log($"[MapboxProbe] token fingerprint: {MapboxTileRequest.DescribeToken(settings.Token)}");

        if (!settings.HasToken)
        {
            Debug.LogError($"[MapboxProbe] No token could be resolved ({settings.TokenSource}), so the " +
                           "tile request is skipped.");
            Finish();
            return;
        }

        string url = MapboxTileRequest.BuildTileUrl(settings.Style, DefaultZoom, DefaultTileX, DefaultTileY, settings.Token);
        Debug.Log($"[MapboxProbe] GET {MapboxTileRequest.RedactToken(url, settings.Token)}");

        activeRequest = UnityWebRequest.Get(url);
        activeRequest.timeout = 20;
        activeRequest.SetRequestHeader("Accept", "image/png,image/jpeg;q=0.9,*/*;q=0.1");
        activeRequest.SendWebRequest();

        deadline = EditorApplication.timeSinceStartup + 30.0;
        EditorApplication.update -= Tick;
        EditorApplication.update += Tick;
    }

    static void Tick()
    {
        if (activeRequest != null && !activeRequest.isDone)
        {
            if (EditorApplication.timeSinceStartup <= deadline)
                return;
            Debug.LogError("[MapboxProbe] Timed out waiting for the tile request.");
            Finish();
            return;
        }

        if (activeRequest != null)
        {
            Report(activeRequest);
            activeRequest.Dispose();
            activeRequest = null;
        }

        Finish();
    }

    static void Report(UnityWebRequest request)
    {
        long status = request.responseCode;
        string contentType = request.GetResponseHeader("Content-Type") ?? "(none)";
        byte[] bytes = request.downloadHandler != null ? request.downloadHandler.data : null;
        int length = bytes != null ? bytes.Length : 0;

        Debug.Log($"[MapboxProbe] tile response: HTTP {status} | Content-Type: {contentType} | {length} bytes | " +
                  $"result={request.result} | error={(string.IsNullOrEmpty(request.error) ? "(none)" : request.error)}");

        bool decoded = false;
        if (request.result == UnityWebRequest.Result.Success && bytes != null && bytes.Length >= 16)
        {
            var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            decoded = texture.LoadImage(bytes, false);
            int width = texture.width, height = texture.height;
            UnityEngine.Object.DestroyImmediate(texture);
            if (decoded)
                Debug.Log($"[MapboxProbe] tile decoded as {width}x{height}.");
        }

        if (decoded)
        {
            exitCode = 0;
            Debug.Log("[MapboxProbe] RESULT: the resolved token authorises this tile, and the game builds the " +
                      "same URL, so a 401 during play means a different token travelled with the request.");
            return;
        }

        string body = request.downloadHandler != null ? request.downloadHandler.text : "";
        Debug.LogError($"[MapboxProbe] RESULT: tile request failed. HTTP {status}. " +
                       $"Mapbox said: {MapboxTileRequest.ClassifyAuthFailure(status, body)}");
        if (!string.IsNullOrEmpty(body))
            Debug.LogError($"[MapboxProbe] response body: {Truncate(body, 300)}");
    }

    static void Finish()
    {
        EditorApplication.update -= Tick;
        EditorUtility.DisplayDialog(
            "Mapbox token probe",
            exitCode == 0
                ? "The resolved token authorises a real Mapbox tile request."
                : "The tile request was refused. See the Console for Mapbox's own wording.",
            "Close");
    }

    static string Truncate(string value, int max)
        => string.IsNullOrEmpty(value) || value.Length <= max ? value : value.Substring(0, max) + "…";
}
