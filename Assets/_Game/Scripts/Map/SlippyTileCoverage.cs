using System;
using System.Collections.Generic;

/// <summary>Web Mercator tile coverage using real geographic bounds, independent of mesh settings.</summary>
public static class SlippyTileCoverage
{
    const double EarthRadius = 6378137d;
    const double MaxLatitude = 85.05112878d;

    public static List<(int x, int y, float dist)> InRadius(double latitude, double longitude, int zoom, float radius)
    {
        if (zoom < 0 || zoom > 22 || double.IsNaN(latitude) || double.IsInfinity(latitude)
            || double.IsNaN(longitude) || double.IsInfinity(longitude) || float.IsNaN(radius) || float.IsInfinity(radius))
            throw new ArgumentOutOfRangeException("Invalid tile coverage parameters.");
        latitude = Math.Max(-MaxLatitude, Math.Min(MaxLatitude, latitude));
        longitude = ((longitude + 180d) % 360d + 360d) % 360d - 180d;
        int count = 1 << zoom;
        double cx = (longitude + 180d) / 360d * count;
        double radians = latitude * Math.PI / 180d;
        double cy = (1d - Math.Log(Math.Tan(radians) + 1d / Math.Cos(radians)) / Math.PI) / 2d * count;
        double tileMeters = 2d * Math.PI * EarthRadius * Math.Cos(radians) / count;
        double extent = Math.Max(0d, radius) / tileMeters;
        // Guard accidental planet-sized requests before allocating or downloading.
        if (extent > 256d) throw new ArgumentOutOfRangeException(nameof(radius), "Select a smaller area or lower zoom.");
        var result = new List<(int x, int y, float dist)>();
        var seen = new HashSet<(int, int)>();
        for (int x = (int)Math.Floor(cx - extent); x <= (int)Math.Floor(cx + extent); x++)
        for (int y = Math.Max(0, (int)Math.Floor(cy - extent)); y <= Math.Min(count - 1, (int)Math.Floor(cy + extent)); y++)
        {
            double dx = Math.Max(0d, Math.Abs(x + 0.5d - cx) - 0.5d) * tileMeters;
            double dy = Math.Max(0d, Math.Abs(y + 0.5d - cy) - 0.5d) * tileMeters;
            if (dx * dx + dy * dy > radius * (double)radius) continue;
            int wrappedX = (x % count + count) % count;
            if (!seen.Add((wrappedX, y))) continue;
            double centerDx = (x + 0.5d - cx) * tileMeters;
            double centerDy = (y + 0.5d - cy) * tileMeters;
            result.Add((wrappedX, y, (float)Math.Sqrt(centerDx * centerDx + centerDy * centerDy)));
        }
        result.Sort((a, b) => a.dist.CompareTo(b.dist));
        return result;
    }
}
