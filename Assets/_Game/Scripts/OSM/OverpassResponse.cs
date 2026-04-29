using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data model and deserialiser for Overpass API JSON responses.
///
/// Handles two common response shapes:
///   • <c>out geom</c>  — geometry embedded directly on way/relation elements.
///   • <c>out body;>;out skel qt;</c> — nodes returned as separate elements;
///     ways and relations carry only ref lists.  <see cref="Resolve"/> stitches
///     them together.
///
/// All fields use simple types (no UnityEngine dependencies) so the parser can
/// be used in Editor tools and tests without a running scene.
/// </summary>
public class OverpassResponse
{
    // ── Data model ─────────────────────────────────────────────────────

    public class GeoPoint
    {
        public double lat;
        public double lon;
    }

    public class Element
    {
        public string type;                             // node | way | relation
        public long   id;

        // Node fields
        public double lat;
        public double lon;

        // Shared
        public Dictionary<string, string> tags     = new();
        public List<GeoPoint>             geometry = new();   // populated by out geom
        public List<long>                 nodeRefs = new();   // populated by out body (ways)
        public List<Member>               members  = new();   // populated for relations
    }

    public class Member
    {
        public string        type;
        public long          @ref;
        public string        role  = "";
        // Positions: set either by inline geometry or by Resolve()
        public double        lat;
        public double        lon;
        public List<GeoPoint> geometry = new();
        public Dictionary<string, string> tags = new();
    }

    // ── Root ───────────────────────────────────────────────────────────

    public List<Element> elements = new();

    /// <summary>All nodes keyed by OSM id — populated by <see cref="Resolve"/>.</summary>
    public Dictionary<long, Element> nodeIndex { get; private set; } = new();

    // ── Deserialise ────────────────────────────────────────────────────

    /// <summary>
    /// Parse raw Overpass JSON string into an <see cref="OverpassResponse"/>.
    /// Call <see cref="Resolve"/> afterwards if you used <c>out body;>;out skel</c>.
    /// </summary>
    public static OverpassResponse Deserialize(string json)
    {
        var result = new OverpassResponse();
        if (string.IsNullOrWhiteSpace(json)) return result;

        var root = Json.Deserialize(json) as Dictionary<string, object>;
        if (root == null || !root.TryGetValue("elements", out var elemsObj)) return result;

        var elems = elemsObj as List<object>;
        if (elems == null) return result;

        foreach (var e0 in elems)
        {
            var e = e0 as Dictionary<string, object>;
            if (e == null) continue;

            var elem = ParseElement(e);
            if (elem != null)
                result.elements.Add(elem);
        }

        return result;
    }

    // ── Post-process ───────────────────────────────────────────────────

    /// <summary>
    /// Resolves node positions in way geometry and relation members using the
    /// node elements present in the response (the <c>></c> recurse step from
    /// <c>out body;>;out skel qt;</c>).
    /// Call once after <see cref="Deserialize"/>.
    /// </summary>
    public void Resolve()
    {
        // Build node index.
        nodeIndex = new Dictionary<long, Element>(elements.Count);
        foreach (var elem in elements)
            if (elem.type == "node")
                nodeIndex[elem.id] = elem;

        // Stitch way geometry from nodeRefs.
        foreach (var elem in elements)
        {
            if (elem.type != "way") continue;
            if (elem.geometry.Count > 0) continue;      // already has inline geometry

            foreach (long nid in elem.nodeRefs)
            {
                if (nodeIndex.TryGetValue(nid, out var node))
                    elem.geometry.Add(new GeoPoint { lat = node.lat, lon = node.lon });
            }
        }

        // Stitch relation member positions.
        foreach (var elem in elements)
        {
            if (elem.type != "relation") continue;
            foreach (var mem in elem.members)
            {
                if (mem.type == "node" && mem.lat == 0d && mem.lon == 0d)
                {
                    if (nodeIndex.TryGetValue(mem.@ref, out var node))
                    {
                        mem.lat = node.lat;
                        mem.lon = node.lon;
                        if (node.tags != null)
                            foreach (var kv in node.tags)
                                mem.tags.TryAdd(kv.Key, kv.Value);
                    }
                }
            }
        }
    }

    // ── Private parsing helpers ────────────────────────────────────────

    static Element ParseElement(Dictionary<string, object> e)
    {
        var elem = new Element
        {
            type = GetStr(e, "type"),
            id   = GetLong(e, "id"),
            lat  = GetDouble(e, "lat"),
            lon  = GetDouble(e, "lon"),
        };

        if (e.TryGetValue("tags", out var tagsObj))
            elem.tags = ParseStringDict(tagsObj as Dictionary<string, object>);

        if (e.TryGetValue("geometry", out var geomObj))
            elem.geometry = ParseGeoPointList(geomObj as List<object>);

        if (e.TryGetValue("nodes", out var nodesObj))
            elem.nodeRefs = ParseLongList(nodesObj as List<object>);

        if (e.TryGetValue("members", out var membersObj))
            elem.members = ParseMembers(membersObj as List<object>);

        return elem;
    }

    static List<Member> ParseMembers(List<object> raw)
    {
        var list = new List<Member>();
        if (raw == null) return list;

        foreach (var m0 in raw)
        {
            var md = m0 as Dictionary<string, object>;
            if (md == null) continue;

            var mem = new Member
            {
                type = GetStr(md, "type"),
                @ref = GetLong(md, "ref"),
                role = GetStr(md, "role"),
                lat  = GetDouble(md, "lat"),
                lon  = GetDouble(md, "lon"),
            };

            if (md.TryGetValue("geometry", out var mg))
                mem.geometry = ParseGeoPointList(mg as List<object>);

            if (md.TryGetValue("tags", out var mt))
                mem.tags = ParseStringDict(mt as Dictionary<string, object>);

            list.Add(mem);
        }
        return list;
    }

    static List<GeoPoint> ParseGeoPointList(List<object> raw)
    {
        var list = new List<GeoPoint>();
        if (raw == null) return list;
        foreach (var p0 in raw)
        {
            var pd = p0 as Dictionary<string, object>;
            if (pd == null) continue;
            if (!pd.ContainsKey("lat") || !pd.ContainsKey("lon")) continue;
            list.Add(new GeoPoint
            {
                lat = System.Convert.ToDouble(pd["lat"]),
                lon = System.Convert.ToDouble(pd["lon"])
            });
        }
        return list;
    }

    static List<long> ParseLongList(List<object> raw)
    {
        var list = new List<long>();
        if (raw == null) return list;
        foreach (var v in raw)
        {
            try { list.Add(System.Convert.ToInt64(v)); } catch { }
        }
        return list;
    }

    static Dictionary<string, string> ParseStringDict(Dictionary<string, object> raw)
    {
        var d = new Dictionary<string, string>();
        if (raw == null) return d;
        foreach (var kv in raw)
            d[kv.Key] = kv.Value?.ToString() ?? "";
        return d;
    }

    // ── Micro-helpers ──────────────────────────────────────────────────

    static string GetStr(Dictionary<string, object> d, string key) =>
        d.TryGetValue(key, out var v) ? (v as string ?? v?.ToString() ?? "") : "";

    static long GetLong(Dictionary<string, object> d, string key)
    {
        if (!d.TryGetValue(key, out var v)) return 0L;
        try { return System.Convert.ToInt64(v); } catch { return 0L; }
    }

    static double GetDouble(Dictionary<string, object> d, string key)
    {
        if (!d.TryGetValue(key, out var v)) return 0d;
        try { return System.Convert.ToDouble(v); } catch { return 0d; }
    }
}
