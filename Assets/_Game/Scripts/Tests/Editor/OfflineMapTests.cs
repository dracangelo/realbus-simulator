using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Covers the offline map stack's two failure modes that were invisible until a player tried
/// to download a city: there was no tile cache to write to, and a download that stored nothing
/// was reported as ready.
/// </summary>
public class OfflineMapTests
{
    [Test]
    public void Cache_IsCreatedOnDemandAndSharedByEveryConsumer()
    {
        OfflineCacheManager existing = OfflineCacheManager.Instance;
        OfflineCacheManager created = null;
        try
        {
            created = OfflineCacheManager.EnsureInstance();
            Assert.That(created, Is.Not.Null,
                "the map stack has to be able to obtain a cache; no scene in the project contained one");
            Assert.That(OfflineCacheManager.EnsureInstance(), Is.SameAs(created),
                "a second ask must not build a second cache, or the loader and the download would write to different folders");
            Assert.That(OfflineCacheManager.Instance, Is.SameAs(created),
                "the cache must publish itself: Awake does not run for a component an EditMode test or an edit-mode caller adds");
            Assert.That(created.CacheRoot, Is.Not.Null.And.Not.Empty, "a cache without a root would throw on the first tile");
        }
        finally
        {
            if (created != null && created != existing)
            {
                Object.DestroyImmediate(created.gameObject);
                Assert.That(OfflineCacheManager.Instance, Is.Null, "destroying the cache must release the static reference");
            }
        }
    }

    [Test]
    public void Cache_TilePathsAreStructuredAndCannotLeaveTheCacheRoot()
    {
        // Coordinates Mapbox never serves, so the "nothing is cached yet" assertion below holds
        // on a machine that has actually played the game (the Nairobi centre tile at zoom 15 is
        // the first tile the game downloads, and it is on the developer's disk afterwards).
        const int zoom = 29, x = 1234567, y = 7654321;
        OfflineCacheManager cache = OfflineCacheManager.EnsureInstance();
        try
        {
            string path = cache.GetTileCachePath("mapbox/satellite-v9", zoom, x, y);
            Assert.That(path, Does.EndWith($"{zoom}{System.IO.Path.DirectorySeparatorChar}{x}{System.IO.Path.DirectorySeparatorChar}{y}.png"),
                "the loader and the predownloader both address a tile by style, zoom, x and y");
            Assert.That(cache.GetTileCachePath("mapbox/satellite-v9", zoom, x, y), Is.EqualTo(path),
                "the same tile has to resolve to the same file");
            Assert.That(cache.GetTileCachePath("mapbox/streets-v12", zoom, x, y), Is.Not.EqualTo(path),
                "two styles must not share one tile file");
            Assert.That(path.StartsWith(cache.CacheRoot), Is.True, "tiles live under the cache root");

            // A style is data from a configuration file, and a slash in it must not become a
            // directory hop out of the cache.
            string style = cache.GetTileCachePath("../../escape", 1, 2, 3);
            Assert.That(style.StartsWith(cache.CacheRoot), Is.True, "a style like '../../escape' must stay inside the cache root");
            char separator = System.IO.Path.DirectorySeparatorChar;
            string relative = style.Substring(cache.CacheRoot.Length).TrimStart(separator);
            Assert.That(relative.Split(separator).Length, Is.EqualTo(4),
                "a style is one folder however it is written; it must not add directory levels to the path");
            Assert.That(cache.IsTileCached("mapbox/satellite-v9", zoom, x, y), Is.False,
                "a tile no Mapbox style serves is never on disk, so no tile may report as cached");
        }
        finally
        {
            if (cache != null) Object.DestroyImmediate(cache.gameObject);
        }
    }

    [TestCase(64, 0, 64, true, Description = "every tile is on disk")]
    [TestCase(0, 64, 64, false, Description = "a run where every tile failed is not an offline map")]
    [TestCase(63, 1, 64, false, Description = "one missing tile still leaves a hole in the map")]
    [TestCase(0, 0, 0, false, Description = "nothing was asked for, so nothing was downloaded")]
    [TestCase(1, 1, 1, false, Description = "the counters have to agree, not just add up")]
    public void Download_OnlyCountsAsReadyWhenEveryTileIsOnDisk(int completed, int failed, int total, bool expected)
        => Assert.That(CityPredownloader.DownloadCoveredEveryTile(completed, failed, total), Is.EqualTo(expected));
}
