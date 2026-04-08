using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Downloads and displays Mapbox raster tiles as flat mesh quads in the scene.
/// Reads centre coordinates from CityManager.activeCity.
/// </summary>
public class MapTileLoader : MonoBehaviour
{
    [Header("Mapbox Settings")]
    public string mapboxToken = "";
    public string mapStyle = "mapbox/satellite-streets-v12";

    [Header("Map Centre — set automatically from CityDefinition")]
    public double centreLat = -1.2864;
    public double centreLon = 36.8172;
    public int zoomLevel = 15;

    [Header("Tile Grid")]
    public int tilesX = 5;
    public int tilesY = 5;
    public float tileWorldSize = 200f;

    [Header("Tile Layer")]
    public int tileLayer = 0;

    private List<GameObject> activeTiles = new List<GameObject>();
    private int centerTileX, centerTileY;

    void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager
            .GetActiveScene().name != "GameScene")
            return;
        StartCoroutine(InitNextFrame());
    }

    IEnumerator InitNextFrame()
    {
        while (CityManager.Instance == null || CityManager.Instance.activeCity == null)
            yield return null;
        
        var city = CityManager.Instance.activeCity;
        centreLat = city.centreLat;
        centreLon = city.centreLon;
        Debug.Log($"MapTileLoader: Loading map for {city.cityName}");
        
        if (string.IsNullOrEmpty(mapboxToken))
        {
            Debug.LogError("MapTileLoader: No Mapbox token set!");
            yield break;
        }
        LoadMap(centreLat, centreLon, zoomLevel);
    }

    public void LoadMap(double lat, double lon, int zoom)
    {
        ClearTiles();

        centreLat = lat;
        centreLon = lon;
        zoomLevel = zoom;

        centerTileX = LonToTileX(lon, zoom);
        centerTileY = LatToTileY(lat, zoom);

        int halfX = tilesX / 2;
        int halfY = tilesY / 2;

        for (int x = -halfX; x <= halfX; x++)
            for (int y = -halfY; y <= halfY; y++)
                StartCoroutine(DownloadTile(
                    centerTileX + x, centerTileY + y, x, y, zoom));
    }

    IEnumerator DownloadTile(int tileX, int tileY, int offsetX, int offsetY, int zoom)
    {
        string url = $"https://api.mapbox.com/styles/v1/{mapStyle}/tiles/512/" +
                     $"{zoom}/{tileX}/{tileY}@2x?access_token={mapboxToken}";

        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D tex = DownloadHandlerTexture.GetContent(request);
                CreateTileQuad(tex, offsetX, offsetY, tileX, tileY);
            }
            else
            {
                Debug.LogWarning($"MapTileLoader: Failed tile {tileX},{tileY} — {request.error}");
            }
        }
    }

    void CreateTileQuad(Texture2D tex, int offsetX, int offsetY, int tileX, int tileY)
    {
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.name = $"Tile_{tileX}_{tileY}";
        quad.transform.parent = transform;
        quad.transform.position = new Vector3(
            offsetX * tileWorldSize, 0.01f, -offsetY * tileWorldSize);
        quad.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        quad.transform.localScale = new Vector3(tileWorldSize, tileWorldSize, 1f);

        var renderer = quad.GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        renderer.material.mainTexture = tex;

        Destroy(quad.GetComponent<Collider>());
        if (tileLayer > 0) quad.layer = tileLayer;
        activeTiles.Add(quad);
    }

    void ClearTiles()
    {
        foreach (var tile in activeTiles)
            if (tile != null) Destroy(tile);
        activeTiles.Clear();
    }

    public static int LonToTileX(double lon, int zoom)
        => (int)System.Math.Floor((lon + 180.0) / 360.0 * (1 << zoom));

    public static int LatToTileY(double lat, int zoom)
    {
        double latRad = lat * System.Math.PI / 180.0;
        return (int)System.Math.Floor(
            (1.0 - System.Math.Log(
                System.Math.Tan(latRad) + 1.0 / System.Math.Cos(latRad))
            / System.Math.PI) / 2.0 * (1 << zoom));
    }

    public Vector3 GpsToWorldPosition(double lat, double lon)
    {
        double metersPerDegreeLat = 111320.0;
        double metersPerDegreeLon = 111320.0 *
            System.Math.Cos(centreLat * System.Math.PI / 180.0);

        float worldX = (float)((lon - centreLon) * metersPerDegreeLon);
        float worldZ = (float)((lat - centreLat) * metersPerDegreeLat);

        return new Vector3(worldX, 0f, worldZ);
    }

    public (double lat, double lon) WorldPositionToGps(Vector3 worldPos)
    {
        double metersPerDegreeLat = 111320.0;
        double metersPerDegreeLon = 111320.0 *
            System.Math.Cos(centreLat * System.Math.PI / 180.0);

        double lat = centreLat + (worldPos.z / metersPerDegreeLat);
        double lon = centreLon + (worldPos.x / metersPerDegreeLon);

        return (lat, lon);
    }
}