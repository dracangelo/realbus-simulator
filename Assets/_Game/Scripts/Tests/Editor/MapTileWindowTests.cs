using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class MapTileWindowTests
{
    [Test]
    public void SurroundingWindowIsUniqueAndComplete()
    {
        List<Vector2Int> tiles = MapTileLoader.SurroundingTiles(100, 200, 5, 5);
        Assert.AreEqual(25, tiles.Count);

        var unique = new HashSet<Vector2Int>(tiles);
        Assert.AreEqual(25, unique.Count, "a tile must not be requested twice in one window");
        Assert.IsTrue(unique.Contains(new Vector2Int(100, 200)));
        Assert.IsTrue(unique.Contains(new Vector2Int(98, 198)));
        Assert.IsTrue(unique.Contains(new Vector2Int(102, 202)));
    }

    [Test]
    public void SurroundingWindowLoadsTheCentreFirst()
    {
        List<Vector2Int> tiles = MapTileLoader.SurroundingTiles(-7, 12, 5, 5);
        Assert.AreEqual(new Vector2Int(-7, 12), tiles[0], "the tile under the bus has to be first");
    }

    /// <summary>
    /// The whole point of the sliding window: when the bus crosses a tile boundary,
    /// most of the previous window is still wanted, so only the new edge is fetched
    /// and only the old edge is destroyed. The old code cleared all 25 every time.
    /// </summary>
    [Test]
    public void MovingOneTileKeepsMostOfTheWindow()
    {
        var before = new HashSet<Vector2Int>(MapTileLoader.SurroundingTiles(100, 200, 5, 5));
        var after = new HashSet<Vector2Int>(MapTileLoader.SurroundingTiles(101, 200, 5, 5));
        before.IntersectWith(after);
        Assert.AreEqual(20, before.Count);
    }
}
