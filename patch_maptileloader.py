import re

with open('/home/vng370/Documents/coding/python/My-project/Assets/_Game/Scripts/Map/MapTileLoader.cs', 'r') as f:
    content = f.read()

helpers = """
    public static double TileXToLon(double x, int zoom)
    {
        return x / (double)(1 << zoom) * 360.0 - 180.0;
    }

    public static double TileYToLat(double y, int zoom)
    {
        double n = System.Math.PI - 2.0 * System.Math.PI * y / (double)(1 << zoom);
        return 180.0 / System.Math.PI * System.Math.Atan(0.5 * (System.Math.Exp(n) - System.Math.Exp(-n)));
    }
"""

# Insert helpers before GpsToWorldPosition
content = content.replace("public Vector3 GpsToWorldPosition(double lat, double lon)", helpers + "\n    public Vector3 GpsToWorldPosition(double lat, double lon)")

create_tile_quad_new = """    void CreateTileQuad(Texture2D tex, int offsetX, int offsetY, int tileX, int tileY)
    {
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.name = $"Tile_{tileX}_{tileY}";
        quad.transform.SetParent(transform, false);

        double centerLon = TileXToLon(tileX + 0.5, zoomLevel);
        double centerLat = TileYToLat(tileY + 0.5, zoomLevel);
        
        double leftLon = TileXToLon(tileX, zoomLevel);
        double rightLon = TileXToLon(tileX + 1.0, zoomLevel);
        double topLat = TileYToLat(tileY, zoomLevel);
        double bottomLat = TileYToLat(tileY + 1.0, zoomLevel);

        Vector3 centerPos = GpsToWorldPosition(centerLat, centerLon);
        centerPos.y = tileSurfaceY;

        Vector3 leftPos = GpsToWorldPosition(centerLat, leftLon);
        Vector3 rightPos = GpsToWorldPosition(centerLat, rightLon);
        Vector3 topPos = GpsToWorldPosition(topLat, centerLon);
        Vector3 bottomPos = GpsToWorldPosition(bottomLat, centerLon);

        float width = Vector3.Distance(leftPos, rightPos);
        float height = Vector3.Distance(topPos, bottomPos);

        quad.transform.localPosition = centerPos;
        quad.transform.localRotation = Quaternion.Euler(90f, 0f, 0f); // Changed from -90f to 90f to make it face UP
        quad.transform.localScale = new Vector3(width, height, 1f);

        var renderer = quad.GetComponent<Renderer>();
"""

# Replace CreateTileQuad logic
content = re.sub(r'    void CreateTileQuad\(Texture2D tex, int offsetX, int offsetY, int tileX, int tileY\).*?var renderer = quad\.GetComponent<Renderer>\(\);', create_tile_quad_new, content, flags=re.DOTALL)

with open('/home/vng370/Documents/coding/python/My-project/Assets/_Game/Scripts/Map/MapTileLoader.cs', 'w') as f:
    f.write(content)
