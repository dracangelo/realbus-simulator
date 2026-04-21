import re

with open('/home/vng370/Documents/coding/python/My-project/Assets/_Game/Scripts/Map/TileStreamManager.cs', 'r') as f:
    content = f.read()

create_tile_quad_new = """    GameObject CreateTileQuad(Texture2D tex, int offsetX, int offsetY, TileKey key)
    {
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.name = $"StreamTile_{key.zoom}_{key.x}_{key.y}";
        quad.transform.SetParent(transform, false);

        double centerLon = MapTileLoader.TileXToLon(key.x + 0.5, key.zoom);
        double centerLat = MapTileLoader.TileYToLat(key.y + 0.5, key.zoom);
        
        double leftLon = MapTileLoader.TileXToLon(key.x, key.zoom);
        double rightLon = MapTileLoader.TileXToLon(key.x + 1.0, key.zoom);
        double topLat = MapTileLoader.TileYToLat(key.y, key.zoom);
        double bottomLat = MapTileLoader.TileYToLat(key.y + 1.0, key.zoom);

        // Assume MapTileLoader is available or use CoordinateConverter mapTileLoader/geoToWorld
        Vector3 centerPos = converter.GeoToWorldPosition(centerLat, centerLon);
        centerPos.y = 0.01f;

        Vector3 leftPos = converter.GeoToWorldPosition(centerLat, leftLon);
        Vector3 rightPos = converter.GeoToWorldPosition(centerLat, rightLon);
        Vector3 topPos = converter.GeoToWorldPosition(topLat, centerLon);
        Vector3 bottomPos = converter.GeoToWorldPosition(bottomLat, centerLon);

        float width = Vector3.Distance(leftPos, rightPos);
        float height = Vector3.Distance(topPos, bottomPos);

        quad.transform.position = centerPos;
        quad.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        quad.transform.localScale = new Vector3(width, height, 1f);

        var renderer = quad.GetComponent<Renderer>();
"""

content = re.sub(r'    GameObject CreateTileQuad\(Texture2D tex, int offsetX, int offsetY, TileKey key\).*?var renderer = quad\.GetComponent<Renderer>\(\);', create_tile_quad_new, content, flags=re.DOTALL)

with open('/home/vng370/Documents/coding/python/My-project/Assets/_Game/Scripts/Map/TileStreamManager.cs', 'w') as f:
    f.write(content)
