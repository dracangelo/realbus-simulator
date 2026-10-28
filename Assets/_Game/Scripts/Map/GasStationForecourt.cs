using UnityEngine;

/// <summary>
/// Paves a drive-in apron around a spawned fuel station with the imported low-poly
/// road modules. The apron is visual only: the generated OSM ribbon keeps owning
/// the carriageway and its physics, and the station's own trigger colliders keep
/// the bus free to drive in. A missing catalog simply leaves the station as it was.
/// </summary>
public static class GasStationForecourt
{
    public const string CatalogResourcePath = "RoadModuleCatalog";

    /// <summary>Above the map tiles (0.15 m) and just below the generated roads (0.25 m).</summary>
    public const float SurfaceY = 0.22f;

    public static bool IsAvailable
    {
        get
        {
            var catalog = Resources.Load<RoadModuleCatalog>(CatalogResourcePath);
            return catalog != null && catalog.PickLongestRoadTile() != null;
        }
    }

    /// <summary>
    /// Builds the apron under <paramref name="station"/>. <paramref name="localBounds"/>
    /// must be the station model's bounds in the station root's local space.
    /// </summary>
    public static GameObject Build(Transform station, Bounds localBounds, int maxTiles = 24)
    {
        if (station == null || maxTiles <= 0) return null;
        var catalog = Resources.Load<RoadModuleCatalog>(CatalogResourcePath);
        var tile = catalog != null ? catalog.PickLongestRoadTile() : null;
        if (tile == null || tile.prefab == null) return null;

        float tileWidth = Mathf.Max(1f, tile.bounds.size.x);
        float tileDepth = Mathf.Max(1f, tile.bounds.size.z);

        // Cover the station plus a two-car approach on every side. The clamp keeps
        // a very large prefab from tiling the whole block.
        float width = Mathf.Max(8f, localBounds.size.x + 8f);
        float depth = Mathf.Max(12f, localBounds.size.z + 8f);
        int columns = Mathf.Clamp(Mathf.CeilToInt(width / tileWidth), 1, 6);
        int rows = Mathf.Clamp(Mathf.CeilToInt(depth / tileDepth), 1, 8);
        while (columns * rows > maxTiles && columns > 1) columns--;
        while (columns * rows > maxTiles && rows > 1) rows--;

        var root = new GameObject("Forecourt");
        root.transform.SetParent(station, false);
        root.transform.localPosition = new Vector3(localBounds.center.x, 0f, localBounds.center.z);
        // The apron is a world-space surface: it sits just above the map tiles
        // whatever height the station was spawned at, and follows the station's
        // heading without inheriting its vertical offset.
        Vector3 apronPosition = root.transform.position;
        apronPosition.y = SurfaceY;
        root.transform.position = apronPosition;
        root.transform.localRotation = Quaternion.identity;
        root.transform.localScale = Vector3.one;

        float originX = -(columns - 1) * tileWidth * .5f;
        float originZ = -(rows - 1) * tileDepth * .5f;
        for (int column = 0; column < columns; column++)
        for (int row = 0; row < rows; row++)
        {
            var instance = Object.Instantiate(tile.prefab, root.transform, false);
            instance.name = $"Forecourt_{column}_{row}";
            instance.transform.localPosition = new Vector3(originX + column * tileWidth, 0f, originZ + row * tileDepth);
            instance.transform.localRotation = Quaternion.identity;
            instance.transform.localScale = Vector3.one;
            foreach (var renderer in instance.GetComponentsInChildren<Renderer>(true))
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }

        return root;
    }
}
