using UnityEngine;

/// <summary>
/// The two surfaces that decide whether the city reads as a place: the asphalt the
/// bus drives on and the walls of the buildings beside it.
///
/// Both used to be flat Standard materials with a near-black or single-colour
/// albedo, which is why the road looked like a black ribbon and the walls looked
/// unlit. This factory builds one shared material per surface, with tiling textures
/// generated at run time (so nothing has to ship as a large imported asset) and a
/// shader that tiles them in world space:
///
/// * asphalt: mid-grey tone with grain, a tangent-space normal map, and a
///   low-frequency wear mask for patches and polished wheel paths. World-space UVs
///   mean a 500 m ribbon tiles the same as a 5 m one.
/// * facade: the Mapbox facade atlas, one UV tile per floor, with a per-building
///   tint and a contact-shadow gradient at the base supplied by a property block.
///
/// Textures carry mipmaps and anisotropic filtering, because a road seen at a
/// grazing angle without either shimmers as the bus moves.
/// </summary>
public static class UrbanMaterials
{
    /// <summary>Mid-dark asphalt, linear space (~sRGB 100 when lit).</summary>
    public static readonly Color AsphaltTone = new Color(.33f, .335f, .345f, 1f);

    static Shader asphaltShader;
    static Shader facadeShader;
    static Shader roofShader;
    static Texture2D asphaltAlbedo;
    static Texture2D asphaltNormal;
    static Texture2D asphaltWear;
    static Texture2D facadeWall;
    static Texture2D facadeWallNormal;
    static Material sharedRoad;
    static Material sharedFacade;
    static Material sharedRoof;
    static Material sharedRoofProp;

    /// <summary>
    /// True once the shared road/facade materials and their generated maps exist.
    /// Generating a 512 px albedo, normal and wear atlas is cheap but not free, and
    /// doing it on the first bend of the route was a visible hitch.
    /// </summary>
    public static bool WarmupComplete { get; private set; }

    /// <summary>
    /// Builds the shared city materials and generated textures once. Called by
    /// <see cref="NearFieldAssetWarmup"/> while the loading card covers the scene.
    /// </summary>
    public static void Warmup()
    {
        if (WarmupComplete) return;

        _ = Road;
        _ = Facade;
        _ = Roof;
        _ = RoofProp;
        _ = AsphaltAlbedo;
        _ = AsphaltNormalTexture;
        _ = AsphaltWearTexture;
        _ = FacadeWallTexture;
        _ = FacadeWallNormalTexture;
        WarmupComplete = true;
    }

    public static Shader AsphaltShader => Resolve(ref asphaltShader, "RealBus/Asphalt");
    public static Shader FacadeShader => Resolve(ref facadeShader, "RealBus/Facade");
    public static Shader RoofShader => Resolve(ref roofShader, "RealBus/Roof");

    /// <summary>One asphalt material for every road ribbon in the city.</summary>
    public static Material Road
    {
        get
        {
            if (sharedRoad == null)
            {
                sharedRoad = CreateAsphalt("Asphalt (shared)");
                sharedRoad.hideFlags = HideFlags.DontSave;
                // The road mesh winding is not guaranteed across every OSM way; the
                // original single-sided material could make a road disappear from
                // one direction. Culling both sides is cheap for a flat ribbon.
                if (sharedRoad.HasProperty("_Cull")) sharedRoad.SetFloat("_Cull", (float)UnityEngine.Rendering.CullMode.Off);
            }
            return sharedRoad;
        }
    }

    /// <summary>Shared wall material; per-building tint and variant live in a property block.</summary>
    public static Material Facade
    {
        get
        {
            if (sharedFacade == null)
            {
                sharedFacade = new Material(FacadeShader) { name = "Facade (shared)", hideFlags = HideFlags.DontSave };
                // The facade's own tint property is deliberately not called _Color:
                // the renderer carries a property block, and _Color would also repaint
                // the Standard-shader roof props that share that renderer.
                if (sharedFacade.HasProperty("_FacadeTint")) sharedFacade.SetColor("_FacadeTint", new Color(.74f, .72f, .68f));
                else if (sharedFacade.HasProperty("_Color")) sharedFacade.SetColor("_Color", new Color(.74f, .72f, .68f));
                sharedFacade.SetColor("_GlassColor", new Color(.09f, .115f, .15f));
                sharedFacade.SetColor("_SkyColor", new Color(.42f, .52f, .65f));
                sharedFacade.SetColor("_TrimColor", new Color(.46f, .45f, .42f));
                sharedFacade.SetFloat("_Smoothness", .06f);
                sharedFacade.SetFloat("_AoHeight", 3.5f);
                sharedFacade.SetFloat("_AoStrength", .5f);
                sharedFacade.SetFloat("_TopShade", .15f);
                // A four-way wall atlas (brick, concrete panels, stucco, stone)
                // selected per building from the same id hash as the window variant,
                // plus a relief normal map. This is what stops two neighbouring
                // blocks with the same window rhythm from reading as one extrusion.
                sharedFacade.SetTexture("_WallTex", FacadeWallTexture);
                sharedFacade.SetTexture("_WallNormal", FacadeWallNormalTexture);
                sharedFacade.SetFloat("_WallTile", 2f);
                sharedFacade.SetFloat("_WallDetail", .38f);
                sharedFacade.SetFloat("_WallNormalStrength", .65f);
            }
            return sharedFacade;
        }
    }

    /// <summary>Shared roof material; the tint arrives per building via a property block.</summary>
    public static Material Roof
    {
        get
        {
            if (sharedRoof == null)
            {
                sharedRoof = new Material(RoofShader) { name = "Roof (shared)", hideFlags = HideFlags.DontSave };
                sharedRoof.SetColor("_RoofTint", new Color(.11f, .11f, .12f));
                sharedRoof.SetColor("_SeamColor", new Color(.19f, .19f, .20f));
                sharedRoof.SetFloat("_GravelStrength", .55f);
                sharedRoof.SetFloat("_SeamSpacing", 1.6f);
                sharedRoof.SetFloat("_Smoothness", .08f);
            }
            return sharedRoof;
        }
    }

    /// <summary>
    /// Shared material for the roof clutter (air handling units, vents, water tanks)
    /// appended to a building mesh. A flat galvanised grey, deliberately separate
    /// from the roof shader so props read as metal against the gravel sheet.
    /// </summary>
    public static Material RoofProp
    {
        get
        {
            if (sharedRoofProp == null)
            {
                sharedRoofProp = new Material(BuiltInShaders.Lit) { name = "Roof Props (shared)", hideFlags = HideFlags.DontSave };
                if (sharedRoofProp.HasProperty("_Color")) sharedRoofProp.SetColor("_Color", new Color(.36f, .37f, .39f));
                if (sharedRoofProp.HasProperty("_Glossiness")) sharedRoofProp.SetFloat("_Glossiness", .26f);
                if (sharedRoofProp.HasProperty("_Smoothness")) sharedRoofProp.SetFloat("_Smoothness", .26f);
                if (sharedRoofProp.HasProperty("_Metallic")) sharedRoofProp.SetFloat("_Metallic", .28f);
            }
            return sharedRoofProp;
        }
    }

    /// <summary>
    /// A stable 0-1 facade variant for a building id: punched windows, ribbon
    /// curtain wall, or twin narrow windows. Same id always gives the same facade,
    /// so a block of flats does not change its face when it streams back in.
    /// </summary>
    public static float VariantFor(long id)
    {
        unchecked
        {
            uint h = (uint)(id * 2654435761L + 1013904223L);
            h ^= h >> 13;
            h *= 1274126177u;
            h ^= h >> 16;
            return (h & 0xFFFF) / 65535f;
        }
    }

    /// <summary>Shops only on buildings with a street-level frontage, never on sheds.</summary>
    public static bool ShopfrontFor(long id, int floors)
    {
        if (floors < 2) return false;
        unchecked
        {
            uint h = (uint)(id * 2246822519L + 374761393L);
            h ^= h >> 15;
            return (h & 3) != 0;
        }
    }

    /// <summary>Detail texture behind the asphalt tone. Shared, generated once.</summary>
    public static Texture2D AsphaltAlbedo
    {
        get
        {
            if (asphaltAlbedo == null) asphaltAlbedo = BuildAlbedo();
            return asphaltAlbedo;
        }
    }

    public static Texture2D AsphaltNormalTexture
    {
        get
        {
            if (asphaltNormal == null) asphaltNormal = BuildNormal();
            return asphaltNormal;
        }
    }

    public static Texture2D AsphaltWearTexture
    {
        get
        {
            if (asphaltWear == null) asphaltWear = BuildWear();
            return asphaltWear;
        }
    }

    /// <summary>Albedo for the four-tile wall detail atlas sampled by the facade shader.</summary>
    public static Texture2D FacadeWallTexture
    {
        get
        {
            if (facadeWall == null) facadeWall = BuildFacadeWall(false);
            return facadeWall;
        }
    }

    /// <summary>Tangent-space relief for the wall detail atlas.</summary>
    public static Texture2D FacadeWallNormalTexture
    {
        get
        {
            if (facadeWallNormal == null) facadeWallNormal = BuildFacadeWall(true);
            return facadeWallNormal;
        }
    }

    /// <summary>
    /// A fresh asphalt material for a road, kerb plane or tarmac patch. Callers own
    /// the instance (Unity destroys materials with their renderer).
    /// </summary>
    public static Material CreateAsphalt(string name = "Asphalt")
    {
        Shader shader = AsphaltShader;
        var material = new Material(shader)
        {
            name = name,
            renderQueue = 2100
        };

        if (shader != null && shader.name == "RealBus/Asphalt")
        {
            material.SetColor("_Color", AsphaltTone);
            material.SetTexture("_MainTex", AsphaltAlbedo);
            material.SetTexture("_NormalMap", AsphaltNormalTexture);
            material.SetTexture("_WearMap", AsphaltWearTexture);
            material.SetFloat("_WorldTile", 7f);
            material.SetFloat("_NormalStrength", 1.1f);
            // A little more sheen than before so the sun and the bus's local
            // reflection probe catch the crown and the polished wheel paths. It is
            // still a dark, mostly diffuse surface - .26 is not a wet road; the rain
            // controller takes it to .85 when the asphalt actually is wet.
            material.SetFloat("_Smoothness", .26f);
            material.SetFloat("_WearStrength", .45f);
            material.SetFloat("_WearTile", 38f);
            // Roads are one-sided ribbons; OSM way direction is not reliable enough
            // to assume the winding faces up from every approach.
            if (material.HasProperty("_Cull")) material.SetFloat("_Cull", (float)UnityEngine.Rendering.CullMode.Off);
            return material;
        }

        // Shader not found (stripped from a player build): keep the improved tone
        // and grain rather than dropping back to a flat black Standard material.
        material.color = AsphaltTone;
        if (material.HasProperty("_MainTex")) material.SetTexture("_MainTex", AsphaltAlbedo);
        if (material.HasProperty("_Glossiness")) material.SetFloat("_Glossiness", .18f);
        if (material.HasProperty("_Smoothness")) material.SetFloat("_Smoothness", .18f);
        if (material.HasProperty("_Metallic")) material.SetFloat("_Metallic", 0f);
        return material;
    }

    // ── Generated textures ────────────────────────────────────────────────
    // Deterministic value noise, so the road looks the same every launch and the
    // generated asset never needs to be serialised.

    static Texture2D BuildAlbedo()
    {
        const int size = 512;
        var texture = new Texture2D(size, size, TextureFormat.RGBA32, true)
        {
            name = "Generated Asphalt Grain",
            wrapMode = TextureWrapMode.Repeat,
            filterMode = FilterMode.Trilinear,
            anisoLevel = 8
        };

        var pixels = new Color32[size * size];
        for (int y = 0; y < size; y++)
        for (int x = 0; x < size; x++)
        {
            float u = x / (float)size;
            float v = y / (float)size;

            // Fine aggregate plus a broad mottling, both tileable because the noise
            // is sampled on a period.
            float fine = TileNoise(u, v, 24) * .55f + TileNoise(u, v, 61) * .3f + TileNoise(u, v, 127) * .15f;
            float broad = TileNoise(u, v, 4) * .6f + TileNoise(u, v, 9) * .4f;
            float grain = .72f + fine * .42f + (broad - .5f) * .26f;

            // Repair patches: broad, slightly darker areas of newer or older mix. A
            // road with no patches is the flat sheet that reads as a texture swatch.
            float patch = Mathf.SmoothStep(.58f, .84f, TileNoise(u, v, 3));
            grain = Mathf.Lerp(grain, grain * .74f, patch * .5f);

            // Hairline cracks: the contour of a low-frequency noise field is a cheap
            // connected network that reads as cracking without shipping a decal.
            float vein = Mathf.Abs(TileNoise(u, v, 6) - .5f);
            if (vein < .022f) grain *= Mathf.Lerp(.48f, .9f, vein / .022f);
            float vein2 = Mathf.Abs(TileNoise(u + .37f, v + .11f, 11) - .5f);
            if (vein2 < .012f) grain *= Mathf.Lerp(.58f, .94f, vein2 / .012f);

            // A few darker chips and a lighter aggregate speckle, sparse enough not
            // to read as a pattern.
            float speck = Hash01(x, y, 7);
            if (speck < .015f) grain *= .72f;
            else if (speck > .986f) grain = Mathf.Min(1f, grain * 1.18f);

            byte value = (byte)Mathf.Clamp(Mathf.RoundToInt(grain * 255f), 0, 255);
            pixels[y * size + x] = new Color32(value, value, (byte)Mathf.Clamp(value - 2, 0, 255), 255);
        }

        texture.SetPixels32(pixels);
        texture.Apply(true, false);
        return texture;
    }

    static Texture2D BuildNormal()
    {
        const int size = 512;
        var texture = new Texture2D(size, size, TextureFormat.RGBA32, true, true)
        {
            name = "Generated Asphalt Normal",
            wrapMode = TextureWrapMode.Repeat,
            filterMode = FilterMode.Trilinear,
            anisoLevel = 8
        };

        var heights = new float[size * size];
        for (int y = 0; y < size; y++)
        for (int x = 0; x < size; x++)
        {
            float u = x / (float)size;
            float v = y / (float)size;
            float height = TileNoise(u, v, 20) * .55f + TileNoise(u, v, 55) * .3f + TileNoise(u, v, 110) * .15f;

            // The same crack network the albedo darkens with, cut as grooves into the
            // height field so a grazing highlight catches the edges of a crack.
            float vein = Mathf.Abs(TileNoise(u, v, 6) - .5f);
            if (vein < .022f) height -= .45f * (1f - vein / .022f);
            float vein2 = Mathf.Abs(TileNoise(u + .37f, v + .11f, 11) - .5f);
            if (vein2 < .012f) height -= .32f * (1f - vein2 / .012f);

            heights[y * size + x] = height;
        }

        var pixels = new Color32[size * size];
        const float strength = 2.4f;
        for (int y = 0; y < size; y++)
        for (int x = 0; x < size; x++)
        {
            int left = ((x - 1 + size) % size) + y * size;
            int right = ((x + 1) % size) + y * size;
            int down = x + ((y - 1 + size) % size) * size;
            int up = x + ((y + 1) % size) * size;

            float dx = (heights[right] - heights[left]) * strength;
            float dz = (heights[up] - heights[down]) * strength;
            Vector3 normal = new Vector3(-dx, 1f, -dz).normalized;

            pixels[y * size + x] = new Color32(
                (byte)Mathf.RoundToInt((normal.x * .5f + .5f) * 255f),
                (byte)Mathf.RoundToInt((normal.y * .5f + .5f) * 255f),
                (byte)Mathf.RoundToInt((normal.z * .5f + .5f) * 255f),
                255);
        }

        texture.SetPixels32(pixels);
        texture.Apply(true, false);
        return texture;
    }

    static Texture2D BuildWear()
    {
        const int size = 256;
        var texture = new Texture2D(size, size, TextureFormat.RGBA32, true)
        {
            name = "Generated Asphalt Wear",
            wrapMode = TextureWrapMode.Repeat,
            filterMode = FilterMode.Trilinear,
            anisoLevel = 4
        };

        var pixels = new Color32[size * size];
        for (int y = 0; y < size; y++)
        for (int x = 0; x < size; x++)
        {
            float u = x / (float)size;
            float v = y / (float)size;
            float patches = TileNoise(u, v, 3) * .62f + TileNoise(u, v, 7) * .28f + TileNoise(u, v, 13) * .1f;
            byte value = (byte)Mathf.Clamp(Mathf.RoundToInt(patches * 255f), 0, 255);
            pixels[y * size + x] = new Color32(value, value, value, 255);
        }

        texture.SetPixels32(pixels);
        texture.Apply(true, false);
        return texture;
    }

    /// <summary>
    /// Four wall surfaces packed into one atlas, tileable within each of the four
    /// cells: brick, concrete panels, stucco and stone ashlar. The facade shader
    /// picks a cell per building and repeats it once per storey, so a street shows a
    /// mix of materials rather than one wall stretched across every block.
    /// </summary>
    static Texture2D BuildFacadeWall(bool normalMap)
    {
        const int size = 512;
        const int tileSize = size / 2;
        var texture = new Texture2D(size, size, TextureFormat.RGBA32, true, normalMap)
        {
            name = normalMap ? "Generated Facade Wall Normal" : "Generated Facade Wall",
            wrapMode = TextureWrapMode.Repeat,
            filterMode = FilterMode.Trilinear,
            anisoLevel = 8
        };

        var heights = normalMap ? new float[size * size] : null;
        var pixels = new Color32[size * size];

        for (int y = 0; y < size; y++)
        for (int x = 0; x < size; x++)
        {
            int tile = (y / tileSize) * 2 + (x / tileSize);
            float lu = (x % tileSize) / (float)tileSize;
            float lv = (y % tileSize) / (float)tileSize;
            FacadePattern(tile, lu, lv, out float height, out float shade);

            int index = y * size + x;
            if (normalMap)
            {
                heights[index] = height;
            }
            else
            {
                byte value = (byte)Mathf.Clamp(Mathf.RoundToInt(shade * 255f), 0, 255);
                pixels[index] = new Color32(value, value, (byte)Mathf.Clamp(value - 3, 0, 255), 255);
            }
        }

        if (!normalMap)
        {
            texture.SetPixels32(pixels);
            texture.Apply(true, false);
            return texture;
        }

        // Height to normal, sampling across each tile's own wrap so the cells stay
        // seam-free when the shader repeats them.
        const float strength = 2.2f;
        for (int y = 0; y < size; y++)
        for (int x = 0; x < size; x++)
        {
            int tileX = x / tileSize, tileY = y / tileSize;
            int lx = x % tileSize, ly = y % tileSize;
            int left = tileX * tileSize + Wrap(lx - 1, tileSize) + y * size;
            int right = tileX * tileSize + Wrap(lx + 1, tileSize) + y * size;
            int down = x + (tileY * tileSize + Wrap(ly - 1, tileSize)) * size;
            int up = x + (tileY * tileSize + Wrap(ly + 1, tileSize)) * size;

            float dx = (heights[right] - heights[left]) * strength;
            float dz = (heights[up] - heights[down]) * strength;
            Vector3 normal = new Vector3(-dx, 1f, -dz).normalized;

            pixels[y * size + x] = new Color32(
                (byte)Mathf.RoundToInt((normal.x * .5f + .5f) * 255f),
                (byte)Mathf.RoundToInt((normal.y * .5f + .5f) * 255f),
                (byte)Mathf.RoundToInt((normal.z * .5f + .5f) * 255f),
                255);
        }

        texture.SetPixels32(pixels);
        texture.Apply(true, false);
        return texture;
    }

    /// <summary>Procedural surface for one wall-atlas cell; height drives the normal map.</summary>
    static void FacadePattern(int tile, float u, float v, out float height, out float shade)
    {
        switch (tile)
        {
            case 0: // Running-bond brick.
            {
                const int rows = 8, cols = 4;
                float row = v * rows;
                int rowIndex = Mathf.FloorToInt(row);
                float rowFrac = row - rowIndex;
                float offset = (rowIndex & 1) == 0 ? 0f : .5f;
                float col = u * cols + offset;
                int colIndex = Mathf.FloorToInt(col);
                float colFrac = col - colIndex;
                bool mortar = rowFrac < .07f || colFrac < .045f;
                height = mortar ? .2f : 1f;
                shade = mortar
                    ? .66f
                    : .80f + Hash01(colIndex, rowIndex, 11) * .18f + (TileNoise(u, v, 32) - .5f) * .10f;
                break;
            }
            case 1: // Large concrete panels with recessed joints and form-tie dots.
            {
                float seam = Mathf.Min(
                    Mathf.Min(u, 1f - u),
                    Mathf.Min(v, 1f - v));
                bool joint = seam < .014f;
                float dx = Mathf.Min(Mathf.Abs(u - .25f), Mathf.Abs(u - .75f));
                float dy = Mathf.Min(Mathf.Abs(v - .5f), Mathf.Abs(v - .25f));
                bool tie = Mathf.Max(dx, dy) < .012f;
                height = joint ? .18f : (tie ? .55f : 1f);
                shade = joint ? .70f : (tie ? .74f : .86f + (TileNoise(u, v, 48) - .5f) * .10f);
                break;
            }
            case 2: // Stucco / render: soft trowelled noise, almost no relief.
            {
                float n = TileNoise(u, v, 40) * .6f + TileNoise(u, v, 90) * .4f;
                height = .55f + n * .45f;
                shade = .80f + (n - .5f) * .20f;
                break;
            }
            default: // Stone ashlar: courses of uneven block widths.
            {
                const int rows = 5;
                float row = v * rows;
                int rowIndex = Mathf.FloorToInt(row);
                float rowFrac = row - rowIndex;
                float offset = (rowIndex % 3) * .33f;
                float col = u * 3f + offset;
                int colIndex = Mathf.FloorToInt(col);
                float colFrac = col - colIndex;
                bool joint = rowFrac < .06f || colFrac < .05f;
                height = joint ? .22f : 1f;
                shade = joint
                    ? .68f
                    : .78f + Hash01(colIndex, rowIndex, 23) * .16f;
                break;
            }
        }
    }

    static float TileNoise(float u, float v, int cells)
    {
        float x = u * cells;
        float y = v * cells;
        int x0 = Mathf.FloorToInt(x), y0 = Mathf.FloorToInt(y);
        float fx = x - x0, fy = y - y0;
        fx = fx * fx * (3f - 2f * fx);
        fy = fy * fy * (3f - 2f * fy);

        // Wrapping the lattice indices keeps the noise tileable: the seam sample at
        // u = 1 hashes the same lattice cell as u = 0.
        int xa = Wrap(x0, cells), xb = Wrap(x0 + 1, cells);
        int ya = Wrap(y0, cells), yb = Wrap(y0 + 1, cells);
        float a = Hash01(xa, ya, cells);
        float b = Hash01(xb, ya, cells);
        float c = Hash01(xa, yb, cells);
        float d = Hash01(xb, yb, cells);
        return Mathf.Lerp(Mathf.Lerp(a, b, fx), Mathf.Lerp(c, d, fx), fy);
    }

    static int Wrap(int value, int period)
    {
        int wrapped = value % period;
        return wrapped < 0 ? wrapped + period : wrapped;
    }

    static float Hash01(int x, int y, int salt)
    {
        unchecked
        {
            uint h = (uint)(x * 374761393 + y * 668265263 + salt * 2246822519);
            h = (h ^ (h >> 13)) * 1274126177u;
            h ^= h >> 16;
            return (h & 0xFFFFFF) / 16777215f;
        }
    }

    static Shader Resolve(ref Shader cache, string shaderName)
    {
        if (cache != null) return cache;
        cache = Shader.Find(shaderName);
        // A player build that stripped the shader still has to draw a lit surface,
        // so the Standard shader is the documented fallback.
        if (cache == null) cache = BuiltInShaders.Lit;
        return cache;
    }
}
