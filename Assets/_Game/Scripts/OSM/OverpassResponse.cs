using System.Collections.Generic;
using UnityEngine;

public class OverpassResponse
{
    public class GeometryPoint
    {
        public double lat;
        public double lon;
    }

    public class Element
    {
        public string type; // node/way/relation
        public long id;
        public double lat;
        public double lon;
        public Dictionary<string, string> tags = new Dictionary<string, string>();
        public List<GeometryPoint> geometry = new List<GeometryPoint>(); // for ways/relations when using out geom
        public List<Member> members = new List<Member>(); // for relations
    }

    public class Member
    {
        public string type;
        public long @ref;
        public string role;
        public double lat;
        public double lon;
        public List<GeometryPoint> geometry = new List<GeometryPoint>();
        public Dictionary<string, string> tags = new Dictionary<string, string>();
    }

    public List<Element> elements = new List<Element>();

    public static OverpassResponse Deserialize(string json)
    {
        var result = new OverpassResponse();
        var root = Json.Deserialize(json) as Dictionary<string, object>;
        if (root == null || !root.ContainsKey("elements")) return result;

        var elems = root["elements"] as List<object>;
        if (elems == null) return result;

        foreach (var e0 in elems)
        {
            var e = e0 as Dictionary<string, object>;
            if (e == null) continue;

            var elem = new Element();
            elem.type = e.TryGetValue("type", out var typeObj) ? (typeObj as string) : null;
            elem.id = e.TryGetValue("id", out var idObj) ? System.Convert.ToInt64(idObj) : 0L;
            elem.lat = e.TryGetValue("lat", out var latObj) ? System.Convert.ToDouble(latObj) : 0.0;
            elem.lon = e.TryGetValue("lon", out var lonObj) ? System.Convert.ToDouble(lonObj) : 0.0;

            if (e.TryGetValue("tags", out var tagsObj))
            {
                var tags = tagsObj as Dictionary<string, object>;
                if (tags != null)
                    foreach (var kvp in tags)
                        elem.tags[kvp.Key] = kvp.Value as string ?? kvp.Value?.ToString() ?? "";
            }

            if (e.TryGetValue("geometry", out var geomObj))
                elem.geometry = ParseGeometryList(geomObj);

            if (e.TryGetValue("members", out var membersObj))
            {
                var members = membersObj as List<object>;
                if (members != null)
                {
                    foreach (var m0 in members)
                    {
                        var md = m0 as Dictionary<string, object>;
                        if (md == null) continue;
                        var mem = new Member();
                        mem.type = md.TryGetValue("type", out var mt) ? (mt as string) : null;
                        mem.@ref = md.TryGetValue("ref", out var mr) ? System.Convert.ToInt64(mr) : 0L;
                        mem.role = md.TryGetValue("role", out var roleObj) ? (roleObj as string ?? "") : "";
                        mem.lat = md.TryGetValue("lat", out var mlatObj) ? System.Convert.ToDouble(mlatObj) : 0.0;
                        mem.lon = md.TryGetValue("lon", out var mlonObj) ? System.Convert.ToDouble(mlonObj) : 0.0;
                        if (md.TryGetValue("geometry", out var mg))
                            mem.geometry = ParseGeometryList(mg);
                        if (md.TryGetValue("tags", out var mtagsObj))
                        {
                            var mtags = mtagsObj as Dictionary<string, object>;
                            if (mtags != null)
                                foreach (var kvp in mtags)
                                    mem.tags[kvp.Key] = kvp.Value as string ?? kvp.Value?.ToString() ?? "";
                        }
                        elem.members.Add(mem);
                    }
                }
            }

            result.elements.Add(elem);
        }

        return result;
    }

    static List<GeometryPoint> ParseGeometryList(object geomObj)
    {
        var list = new List<GeometryPoint>();
        var arr = geomObj as List<object>;
        if (arr == null) return list;
        foreach (var p0 in arr)
        {
            var pd = p0 as Dictionary<string, object>;
            if (pd == null) continue;
            if (!pd.ContainsKey("lat") || !pd.ContainsKey("lon")) continue;
            list.Add(new GeometryPoint
            {
                lat = System.Convert.ToDouble(pd["lat"]),
                lon = System.Convert.ToDouble(pd["lon"])
            });
        }
        return list;
    }
}

