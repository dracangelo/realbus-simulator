using UnityEngine;

/// <summary>
/// Procedural advertising and street-furniture surfaces for the imported LiveCity
/// props. The generated billboard models in <c>Resources/LiveCityGenerated</c> were
/// built from source packs whose materials had no albedo at all: every banner,
/// billboard, sign and bench rendered as the same flat grey. Importing a texture for
/// each generated material would make every instance of a prefab identical and would
/// still not cover the downloaded cities.
///
/// This library generates a small, cached set of designed advertisements in both
/// landscape and portrait layouts plus a handful of bench surfaces. A spawned
/// billboard receives a stable random variant from the placement seed; flat poster
/// faces get a thin textured quad while the original mesh becomes a dark steel frame,
/// and benches receive a matching shared wood/stone material. Nothing is written to
/// the source FBX files or the generated material assets, so a rebuilt catalog works
/// with the same runtime path.
/// </summary>
public static class StreetTextureLibrary
{
    public const int BillboardVariantCount = 10;
    public const int BenchVariantCount = 4;

    const int LandscapeWidth = 512;
    const int LandscapeHeight = 256;
    const int PortraitWidth = 320;
    const int PortraitHeight = 480;
    const int BenchSize = 256;

    static Texture2D[] landscapeTextures;
    static Texture2D[] portraitTextures;
    static Texture2D[] benchTextures;
    static Material[] landscapeMaterials;
    static Material[] portraitMaterials;
    static Material[] benchMaterials;
    static Material billboardFrameMaterial;

    /// <summary>True once every advertisement and bench surface has been generated.</summary>
    public static bool IsPrewarmed { get; private set; }

    /// <summary>Build the cached textures under the loading card, before the first prop streams in.</summary>
    public static void Prewarm()
    {
        if (IsPrewarmed) return;
        EnsureBillboardMaterials();
        EnsureBenchMaterials();
        _ = BillboardFrame;
        IsPrewarmed = true;
    }

    public static Material GetBillboardMaterial(int index, bool portrait)
    {
        EnsureBillboardMaterials();
        Material[] set = portrait ? portraitMaterials : landscapeMaterials;
        return set[Mathf.Clamp(index, 0, set.Length - 1)];
    }

    public static Texture2D GetBillboardTexture(int index, bool portrait)
    {
        EnsureBillboardMaterials();
        Texture2D[] set = portrait ? portraitTextures : landscapeTextures;
        return set[Mathf.Clamp(index, 0, set.Length - 1)];
    }

    public static Material GetBenchMaterial(int index)
    {
        EnsureBenchMaterials();
        return benchMaterials[Mathf.Clamp(index, 0, benchMaterials.Length - 1)];
    }

    public static Texture2D GetBenchTexture(int index)
    {
        EnsureBenchMaterials();
        return benchTextures[Mathf.Clamp(index, 0, benchTextures.Length - 1)];
    }

    /// <summary>A painted-steel surface for billboard posts and frames.</summary>
    public static Material BillboardFrame
    {
        get
        {
            if (billboardFrameMaterial == null)
            {
                billboardFrameMaterial = new Material(BuiltInShaders.Lit)
                {
                    name = "Billboard Frame (shared)",
                    hideFlags = HideFlags.DontSave
                };
                if (billboardFrameMaterial.HasProperty("_Color"))
                    billboardFrameMaterial.SetColor("_Color", new Color(.105f, .115f, .125f));
                if (billboardFrameMaterial.HasProperty("_Metallic"))
                    billboardFrameMaterial.SetFloat("_Metallic", .55f);
                if (billboardFrameMaterial.HasProperty("_Glossiness"))
                    billboardFrameMaterial.SetFloat("_Glossiness", .32f);
                if (billboardFrameMaterial.HasProperty("_Smoothness"))
                    billboardFrameMaterial.SetFloat("_Smoothness", .32f);
            }
            return billboardFrameMaterial;
        }
    }

    /// <summary>
    /// Stable per-instance variant selection. The same placement seed always picks
    /// the same advertisement, so a prop that streams out and back does not change
    /// its face, while neighbouring billboards almost always differ.
    /// </summary>
    public static int PickVariant(uint seed, int count)
    {
        if (count <= 1) return 0;
        unchecked
        {
            seed ^= seed >> 16;
            seed *= 0x7feb352d;
            seed ^= seed >> 15;
            seed *= 0x846ca68b;
            seed ^= seed >> 16;
            return (int)(seed % (uint)count);
        }
    }

    /// <summary>
    /// Applies the right kind of surface to a freshly spawned LiveCity prop.
    /// Returns the number of poster faces that received an advertisement.
    /// </summary>
    public static int Decorate(GameObject root, LiveCityCatalog.Kind kind, uint seed)
    {
        if (root == null) return 0;
        switch (kind)
        {
            case LiveCityCatalog.Kind.Billboard:
                return DecorateBillboard(root, seed);
            case LiveCityCatalog.Kind.Bench:
                ApplyBench(root, seed);
                return 1;
            default:
                return 0;
        }
    }

    /// <summary>
    /// Gives a billboard a random advertisement. Large flat panels are covered by a
    /// pair of outward-facing textured quads, leaving the original body as a frame;
    /// a banner whose surface is not planar gets the advertisement directly on its
    /// largest renderer instead.
    /// </summary>
    public static int DecorateBillboard(GameObject root, uint seed)
    {
        if (root == null) return 0;
        EnsureBillboardMaterials();

        Renderer[] renderers = root.GetComponentsInChildren<Renderer>(true);
        if (renderers == null || renderers.Length == 0) return 0;

        // The source packs supplied either no albedo or a flat placeholder colour
        // (a huge orange panel is the most obvious one). A poster face gets our
        // advertisement even when the source material technically has a texture:
        // a flat placeholder is not an advertisement, and covering it is the whole
        // point of this library. Non-face body parts are only repainted when they
        // have no texture at all, so a real grille/bracket texture survives.
        int decorated = 0;
        int faceIndex = 0;
        bool anyTextured = false;
        Renderer fallback = null;
        float fallbackScore = 0f;
        for (int i = 0; i < renderers.Length; i++)
        {
            Renderer renderer = renderers[i];
            if (renderer == null || !IsSurfaceRenderer(renderer)) continue;

            if (TryResolvePosterFace(renderer, out Vector3 center, out Vector3 normal,
                    out Vector3 up, out float halfThickness, out Vector2 size, out bool portrait))
            {
                // The face is about to be covered, so its original material (even a
                // flat orange placeholder) becomes the frame. Leaving it textured
                // made the uncovered border read as an untextured billboard.
                PaintFrame(renderer);
                AddPosterFaces(root.transform, center, normal, up, halfThickness, size,
                    portrait, seed, faceIndex++);
                decorated++;
                continue;
            }

            if (HasAnyTexture(renderer))
            {
                anyTextured = true;
                continue;
            }

            PaintFrame(renderer);

            Bounds bounds = renderer.bounds;
            float score = bounds.size.x * bounds.size.y + bounds.size.y * bounds.size.z +
                          bounds.size.z * bounds.size.x;
            if (score > fallbackScore)
            {
                fallbackScore = score;
                fallback = renderer;
            }
        }

        if (decorated == 0 && fallback != null && !anyTextured)
        {
            Bounds bounds = fallback.bounds;
            bool portrait = bounds.size.y > Mathf.Min(bounds.size.x, bounds.size.z) * 1.15f;
            Material material = GetBillboardMaterial(
                PickVariant(seed + 0x6d2b79f5u, BillboardVariantCount), portrait);
            Material[] materials = fallback.sharedMaterials;
            if (materials == null || materials.Length == 0)
                materials = new[] { material };
            else
            {
                for (int m = 0; m < materials.Length; m++)
                    materials[m] = material;
            }
            fallback.sharedMaterials = materials;
            decorated = 1;
        }

        return decorated;
    }

    static void PaintFrame(Renderer renderer)
    {
        if (renderer == null) return;
        Material[] materials = renderer.sharedMaterials;
        if (materials == null || materials.Length == 0) return;
        for (int m = 0; m < materials.Length; m++)
            materials[m] = BillboardFrame;
        renderer.sharedMaterials = materials;
    }

    /// <summary>Replaces a bench's untextured material with one of four generated surfaces.</summary>
    public static void ApplyBench(GameObject root, uint seed)
    {
        if (root == null) return;
        EnsureBenchMaterials();

        Material material = GetBenchMaterial(PickVariant(seed, BenchVariantCount));
        Renderer[] renderers = root.GetComponentsInChildren<Renderer>(true);
        if (renderers == null) return;
        for (int i = 0; i < renderers.Length; i++)
        {
            Renderer renderer = renderers[i];
            if (renderer == null || !IsSurfaceRenderer(renderer)) continue;
            Material[] materials = renderer.sharedMaterials;
            if (materials == null || materials.Length == 0)
            {
                materials = new[] { material };
            }
            else
            {
                bool changed = false;
                for (int m = 0; m < materials.Length; m++)
                {
                    if (materials[m] != null && materials[m].mainTexture != null) continue;
                    materials[m] = material;
                    changed = true;
                }
                if (!changed) continue;
            }
            renderer.sharedMaterials = materials;
        }
    }

    static bool IsSurfaceRenderer(Renderer renderer)
    {
        return !(renderer is ParticleSystemRenderer) &&
               !(renderer is TrailRenderer) &&
               !(renderer is LineRenderer);
    }

    static bool HasAnyTexture(Renderer renderer)
    {
        if (renderer == null) return false;
        Material[] materials = renderer.sharedMaterials;
        if (materials == null) return false;
        for (int i = 0; i < materials.Length; i++)
            if (materials[i] != null && materials[i].mainTexture != null)
                return true;
        return false;
    }

    /// <summary>
    /// Finds a flat poster face in the renderer's own space. Using the world AABB
    /// failed on billboards the streamer had yawed to face a diagonal road: a 45°
    /// rotation made the flat panel look thick on both horizontal axes and pushed
    /// it into the whole-mesh fallback. Local bounds stay axis-aligned with the
    /// source mesh, and transforming the axes gives the real world thickness,
    /// height direction and face centre for any road angle.
    /// </summary>
    static bool TryResolvePosterFace(Renderer renderer, out Vector3 center, out Vector3 normal,
        out Vector3 up, out float halfThickness, out Vector2 size, out bool portrait)
    {
        center = Vector3.zero;
        normal = Vector3.forward;
        up = Vector3.up;
        halfThickness = 0f;
        size = Vector2.zero;
        portrait = false;
        if (renderer == null) return false;

        Bounds local = renderer.localBounds;
        if (local.size.x <= 0f && local.size.y <= 0f && local.size.z <= 0f)
            return false;

        Vector3[] axes = { Vector3.right, Vector3.up, Vector3.forward };
        Vector3[] directions = new Vector3[3];
        float[] lengths = new float[3];
        for (int i = 0; i < 3; i++)
        {
            directions[i] = renderer.transform.TransformDirection(axes[i]).normalized;
            lengths[i] = renderer.transform.TransformVector(axes[i] * local.size[i]).magnitude;
        }

        int thinAxis = 0;
        for (int i = 1; i < 3; i++)
            if (lengths[i] < lengths[thinAxis]) thinAxis = i;

        float thin = lengths[thinAxis];
        if (thin > .9f) return false;

        int heightAxis = -1;
        float bestVertical = 0f;
        for (int i = 0; i < 3; i++)
        {
            if (i == thinAxis) continue;
            float vertical = Mathf.Abs(Vector3.Dot(directions[i], Vector3.up));
            if (vertical > bestVertical)
            {
                bestVertical = vertical;
                heightAxis = i;
            }
        }
        if (heightAxis < 0 || bestVertical < .35f) return false;

        int widthAxis = 3 - thinAxis - heightAxis;
        float width = lengths[widthAxis];
        float height = lengths[heightAxis];
        if (width < .65f || height < .6f || width * height < .9f) return false;

        normal = directions[thinAxis];
        up = directions[heightAxis];
        if (Vector3.Dot(up, Vector3.up) < 0f) up = -up;
        center = renderer.transform.TransformPoint(local.center);
        halfThickness = thin * .5f;
        size = new Vector2(width * .965f, height * .965f);
        portrait = height > width * 1.15f;
        return true;
    }

    static void AddPosterFaces(Transform parent, Vector3 center, Vector3 normal, Vector3 up,
        float halfThickness, Vector2 size, bool portrait, uint seed, int faceIndex)
    {
        uint baseSeed = seed + (uint)(faceIndex * 2654435761u);
        float offset = halfThickness + .018f;
        AddPosterQuad(parent, center + normal * offset, normal, up, size, portrait, baseSeed);
        AddPosterQuad(parent, center - normal * offset, -normal, up, size, portrait,
            baseSeed + 0x9e3779b9u);
    }

    static void AddPosterQuad(Transform parent, Vector3 position, Vector3 normal, Vector3 up,
        Vector2 size, bool portrait, uint seed)
    {
        var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.name = "Billboard_Advertisement";
        Collider collider = quad.GetComponent<Collider>();
        if (collider != null)
        {
            if (Application.isPlaying) Object.Destroy(collider);
            else Object.DestroyImmediate(collider);
        }
        quad.transform.SetParent(parent, true);
        quad.transform.position = position;
        quad.transform.rotation = Quaternion.LookRotation(normal, up);
        quad.transform.localScale = new Vector3(size.x, size.y, 1f);

        var renderer = quad.GetComponent<Renderer>();
        int variant = PickVariant(seed, BillboardVariantCount);
        renderer.sharedMaterial = GetBillboardMaterial(variant, portrait);
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = true;
    }

    static void EnsureBillboardMaterials()
    {
        if (landscapeMaterials != null && landscapeMaterials.Length == BillboardVariantCount &&
            portraitMaterials != null && portraitMaterials.Length == BillboardVariantCount &&
            landscapeTextures != null && landscapeTextures.Length == BillboardVariantCount &&
            portraitTextures != null && portraitTextures.Length == BillboardVariantCount &&
            landscapeTextures[0] != null && portraitTextures[0] != null &&
            landscapeMaterials[0] != null && portraitMaterials[0] != null)
            return;

        landscapeTextures = new Texture2D[BillboardVariantCount];
        portraitTextures = new Texture2D[BillboardVariantCount];
        landscapeMaterials = new Material[BillboardVariantCount];
        portraitMaterials = new Material[BillboardVariantCount];

        for (int i = 0; i < BillboardVariantCount; i++)
        {
            landscapeTextures[i] = BuildAdvertisement(i, false);
            portraitTextures[i] = BuildAdvertisement(i, true);
            landscapeMaterials[i] = MakeAdvertisementMaterial(landscapeTextures[i], i, false);
            portraitMaterials[i] = MakeAdvertisementMaterial(portraitTextures[i], i, true);
        }
    }

    static void EnsureBenchMaterials()
    {
        if (benchMaterials != null && benchMaterials.Length == BenchVariantCount &&
            benchTextures != null && benchTextures.Length == BenchVariantCount &&
            benchTextures[0] != null && benchMaterials[0] != null)
            return;

        benchTextures = new Texture2D[BenchVariantCount];
        benchMaterials = new Material[BenchVariantCount];
        for (int i = 0; i < BenchVariantCount; i++)
        {
            benchTextures[i] = BuildBenchTexture(i);
            var material = new Material(BuiltInShaders.Lit)
            {
                name = $"Bench Surface {i}",
                hideFlags = HideFlags.DontSave
            };
            SetMainTexture(material, benchTextures[i]);
            if (material.HasProperty("_Color")) material.SetColor("_Color", Color.white);
            if (material.HasProperty("_Glossiness")) material.SetFloat("_Glossiness", .22f);
            if (material.HasProperty("_Smoothness")) material.SetFloat("_Smoothness", .22f);
            if (material.HasProperty("_Metallic")) material.SetFloat("_Metallic", .05f);
            material.enableInstancing = true;
            benchMaterials[i] = material;
        }
    }

    static Material MakeAdvertisementMaterial(Texture2D texture, int index, bool portrait)
    {
        var material = new Material(BuiltInShaders.Lit)
        {
            name = $"Billboard Ad {(portrait ? "P" : "L")}{index}",
            hideFlags = HideFlags.DontSave
        };
        SetMainTexture(material, texture);
        if (material.HasProperty("_Color")) material.SetColor("_Color", Color.white);
        if (material.HasProperty("_Glossiness")) material.SetFloat("_Glossiness", .18f);
        if (material.HasProperty("_Smoothness")) material.SetFloat("_Smoothness", .18f);
        if (material.HasProperty("_Metallic")) material.SetFloat("_Metallic", 0f);
        if (material.HasProperty("_EmissionColor"))
        {
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", new Color(.16f, .16f, .16f));
            if (material.HasProperty("_EmissionMap")) material.SetTexture("_EmissionMap", texture);
        }
        material.enableInstancing = true;
        return material;
    }

    static void SetMainTexture(Material material, Texture2D texture)
    {
        if (material == null || texture == null) return;
        material.mainTexture = texture;
        if (material.HasProperty("_MainTex")) material.SetTexture("_MainTex", texture);
        if (material.HasProperty("_BaseMap")) material.SetTexture("_BaseMap", texture);
    }

    struct AdStyle
    {
        public string title;
        public string subtitle;
        public string body;
        public string footer;
        public Color32 bgTop;
        public Color32 bgBottom;
        public Color32 accent;
        public Color32 accent2;
        public Color32 ink;
        public int motif;
        public bool qr;
    }

    static readonly AdStyle[] Styles =
    {
        new AdStyle
        {
            title = "NBO TRANSIT", subtitle = "MOVE THE CITY", body = "EVERY 6 MINUTES", footer = "TAP - RIDE - ARRIVE",
            bgTop = new Color32(12, 30, 58, 255), bgBottom = new Color32(6, 14, 30, 255),
            accent = new Color32(0, 196, 235, 255), accent2 = new Color32(0, 110, 160, 255),
            ink = new Color32(240, 252, 255, 255), motif = 0, qr = true
        },
        new AdStyle
        {
            title = "SAVANNA SOLAR", subtitle = "POWER THE FUTURE", body = "POWER YOUR HOME", footer = "CLEAN ENERGY FOR NBO",
            bgTop = new Color32(16, 72, 58, 255), bgBottom = new Color32(5, 27, 27, 255),
            accent = new Color32(255, 205, 36, 255), accent2 = new Color32(240, 145, 30, 255),
            ink = new Color32(20, 26, 22, 255), motif = 1, qr = false
        },
        new AdStyle
        {
            title = "COASTLINE AIR", subtitle = "FLY FROM NBO", body = "FLY DAILY", footer = "DIRECT TO THE COAST",
            bgTop = new Color32(10, 70, 110, 255), bgBottom = new Color32(4, 24, 52, 255),
            accent = new Color32(255, 158, 52, 255), accent2 = new Color32(40, 205, 220, 255),
            ink = new Color32(255, 250, 240, 255), motif = 2, qr = true
        },
        new AdStyle
        {
            title = "MZIZI COFFEE", subtitle = "ROASTED IN KENYA", body = "SINGLE ORIGIN", footer = "OPEN EVERY MORNING",
            bgTop = new Color32(84, 45, 25, 255), bgBottom = new Color32(28, 15, 10, 255),
            accent = new Color32(234, 197, 145, 255), accent2 = new Color32(175, 112, 58, 255),
            ink = new Color32(32, 18, 10, 255), motif = 3, qr = false
        },
        new AdStyle
        {
            title = "TUSK 5G", subtitle = "THE CITY CONNECTED", body = "5G EVERYWHERE", footer = "UNLIMITED DATA",
            bgTop = new Color32(54, 18, 92, 255), bgBottom = new Color32(12, 5, 28, 255),
            accent = new Color32(236, 64, 230, 255), accent2 = new Color32(92, 194, 255, 255),
            ink = new Color32(255, 246, 255, 255), motif = 4, qr = true
        },
        new AdStyle
        {
            title = "KILI WATER", subtitle = "PURE AND REFRESHING", body = "PURE AT SOURCE", footer = "BOTTLED AT THE SOURCE",
            bgTop = new Color32(8, 92, 132, 255), bgBottom = new Color32(4, 28, 54, 255),
            accent = new Color32(152, 236, 255, 255), accent2 = new Color32(28, 162, 208, 255),
            ink = new Color32(8, 28, 42, 255), motif = 5, qr = false
        },
        new AdStyle
        {
            title = "JENGA HOMES", subtitle = "LIVE ABOVE THE CITY", body = "MOVE IN TODAY", footer = "VIEWINGS THIS WEEK",
            bgTop = new Color32(44, 20, 72, 255), bgBottom = new Color32(12, 8, 24, 255),
            accent = new Color32(245, 198, 88, 255), accent2 = new Color32(184, 96, 238, 255),
            ink = new Color32(28, 18, 10, 255), motif = 6, qr = true
        },
        new AdStyle
        {
            title = "PULSE FITNESS", subtitle = "TRAIN ANYWHERE", body = "JOIN THE PULSE", footer = "FIRST CLASS FREE",
            bgTop = new Color32(110, 18, 26, 255), bgBottom = new Color32(22, 10, 14, 255),
            accent = new Color32(186, 238, 52, 255), accent2 = new Color32(255, 84, 76, 255),
            ink = new Color32(18, 22, 10, 255), motif = 7, qr = false
        },
        new AdStyle
        {
            title = "SAFARI BANK", subtitle = "BANK ON THE MOVE", body = "NO HIDDEN FEES", footer = "OPEN AN ACCOUNT TODAY",
            bgTop = new Color32(12, 62, 46, 255), bgBottom = new Color32(5, 22, 18, 255),
            accent = new Color32(255, 206, 72, 255), accent2 = new Color32(56, 192, 128, 255),
            ink = new Color32(18, 30, 20, 255), motif = 8, qr = true
        },
        new AdStyle
        {
            title = "NBO FESTIVAL", subtitle = "MUSIC UNDER THE SUN", body = "LIVE AT NBO", footer = "TICKETS ON SALE NOW",
            bgTop = new Color32(196, 68, 24, 255), bgBottom = new Color32(80, 14, 78, 255),
            accent = new Color32(255, 220, 56, 255), accent2 = new Color32(255, 116, 76, 255),
            ink = new Color32(48, 14, 20, 255), motif = 9, qr = false
        }
    };

    static Texture2D BuildAdvertisement(int index, bool portrait)
    {
        int width = portrait ? PortraitWidth : LandscapeWidth;
        int height = portrait ? PortraitHeight : LandscapeHeight;
        var texture = new Texture2D(width, height, TextureFormat.RGBA32, true)
        {
            wrapMode = TextureWrapMode.Clamp,
            filterMode = FilterMode.Bilinear,
            anisoLevel = 2,
            name = $"StreetAd_{index}_{(portrait ? "Portrait" : "Landscape")}"
        };

        var pixels = new Color32[width * height];
        var canvas = new BusStopPosterBuilder.CanvasContext(pixels, width, height);
        AdStyle style = Styles[Mathf.Clamp(index, 0, Styles.Length - 1)];

        DrawAdvertisement(canvas, style, portrait, index);
        texture.SetPixels32(pixels);
        texture.Apply(false, false);
        return texture;
    }

    static void DrawAdvertisement(BusStopPosterBuilder.CanvasContext c, AdStyle s, bool portrait, int index)
    {
        int w = c.Width;
        int h = c.Height;
        c.FillGradient(s.bgTop, s.bgBottom);

        // Vertical edge rules read as a printed frame without crossing any of the
        // headline, footer or barcode pixel text.
        c.FillRect(5, 5, 2, h - 10, s.accent2);
        c.FillRect(w - 7, 5, 2, h - 10, s.accent2);

        if (portrait) DrawPortraitLayout(c, s, index);
        else DrawLandscapeLayout(c, s, index);
    }

    static void DrawLandscapeLayout(BusStopPosterBuilder.CanvasContext c, AdStyle s, int index)
    {
        int w = c.Width, h = c.Height;
        int header = 82;
        int footer = 54;
        c.FillRect(0, h - header, w, header, s.accent);
        c.FillRect(0, 0, w, footer, s.accent2);

        int titleScale = c.FitScale(s.title, w - 44, 5);
        c.DrawText(s.title, 22, h - 54, titleScale, s.ink);
        c.DrawTextFit(s.subtitle, 24, h - 78, w - 48, 3, s.ink);

        int bodyScale = c.FitScale(s.body, Mathf.RoundToInt(w * .53f), 5);
        c.DrawText(s.body, 24, Mathf.Max(footer + 26, (h - header) / 2 - 4), bodyScale, s.accent);
        c.FillRect(24, footer + 18, Mathf.Min(160, w / 3), 3, s.accent2);

        int motifW = Mathf.RoundToInt(w * .34f);
        int motifH = Mathf.Max(70, h - header - footer - 34);
        DrawMotif(c, s.motif, w - motifW - 26, footer + 18, motifW, motifH, s.accent, s.accent2, s.ink);

        if (s.qr)
        {
            c.DrawTextFit(s.footer, 24, 18, w - 190, 2, s.ink);
            c.DrawQrCode(w - 58, 10, 34, s.ink);
        }
        else
        {
            c.DrawTextFit(s.footer, 24, 18, w - 230, 2, s.ink);
            c.DrawBarcode(w - 170, 14, 140, 26, s.ink);
        }

        // Variant-specific corner detail stops two ads with the same layout from
        // reading as identical at a distance.
        c.FillCircle(w - 42, h - 44, 9 + index % 3, s.accent2);
    }

    static void DrawPortraitLayout(BusStopPosterBuilder.CanvasContext c, AdStyle s, int index)
    {
        int w = c.Width, h = c.Height;
        int header = 112;
        int footer = 58;
        c.FillRect(0, h - header, w, header, s.accent);
        c.FillRect(0, 0, w, footer, s.accent2);

        int titleScale = c.FitScale(s.title, w - 32, 4);
        c.DrawTextCentered(s.title, w / 2, h - 44, titleScale, s.ink);
        c.DrawTextCenteredFit(s.subtitle, w / 2, h - 86, w - 38, 2, s.ink);

        int motifTop = footer + 30;
        int motifH = Mathf.Max(120, h - header - footer - 58);
        DrawMotif(c, s.motif, 26, motifTop, w - 52, motifH, s.accent, s.accent2, s.ink);

        if (s.qr)
        {
            c.DrawTextCenteredFit(s.footer, w / 2, 18, w - 94, 2, s.ink);
            c.DrawQrCode(w - 52, 9, 38, s.ink);
        }
        else
        {
            // Portrait space is narrow: stack the line above a slimmer barcode
            // instead of printing both through the same pixels.
            c.DrawTextCenteredFit(s.footer, w / 2, 35, w - 44, 2, s.ink);
            c.DrawBarcode(24, 8, w - 48, 20, s.ink);
        }

        c.FillCircle(w - 36, h - header - 20, 19 + index % 4, s.accent2);
    }

    static void DrawMotif(BusStopPosterBuilder.CanvasContext c, int motif, int x, int y, int w, int h,
        Color32 accent, Color32 accent2, Color32 ink)
    {
        switch (motif)
        {
            case 0: DrawSkyline(c, x, y, w, h, accent, accent2, ink); break;
            case 1: DrawSunPower(c, x, y, w, h, accent, accent2); break;
            case 2: DrawWaves(c, x, y, w, h, accent, accent2, ink); break;
            case 3: DrawCoffee(c, x, y, w, h, accent, ink); break;
            case 4: DrawSignal(c, x, y, w, h, accent, accent2); break;
            case 5: DrawBottle(c, x, y, w, h, accent, accent2, ink); break;
            case 6: DrawBuildings(c, x, y, w, h, accent, accent2, ink); break;
            case 7: DrawEqualizer(c, x, y, w, h, accent, accent2); break;
            case 8: DrawShield(c, x, y, w, h, accent, accent2, ink); break;
            default: DrawFestival(c, x, y, w, h, accent, accent2, ink); break;
        }
    }

    static void DrawSkyline(BusStopPosterBuilder.CanvasContext c, int x, int y, int w, int h,
        Color32 accent, Color32 accent2, Color32 ink)
    {
        int baseY = y + 4;
        c.FillCircle(x + w * 3 / 4, y + h * 3 / 4, Mathf.Max(14, Mathf.Min(w, h) / 8), accent2);
        int[] heights = { 42, 70, 55, 84, 48 };
        int count = 5;
        int step = Mathf.Max(10, w / count);
        for (int i = 0; i < count; i++)
        {
            int bw = Mathf.Max(10, step - 6);
            int bh = Mathf.RoundToInt(h * heights[i] / 100f);
            int bx = x + i * step;
            c.FillRect(bx, baseY, bw, bh, accent);
            for (int wy = baseY + 10; wy < baseY + bh - 8; wy += 14)
                c.FillRect(bx + 4, wy, Mathf.Max(3, bw / 4), 4, ink);
        }
    }

    static void DrawSunPower(BusStopPosterBuilder.CanvasContext c, int x, int y, int w, int h,
        Color32 accent, Color32 accent2)
    {
        int cx = x + w / 2, cy = y + h * 2 / 3;
        int radius = Mathf.Max(18, Mathf.Min(w, h) / 4);
        c.FillCircle(cx, cy, radius, accent);
        for (int i = 0; i < 8; i++)
        {
            float angle = i * Mathf.PI / 4f;
            int x0 = cx + Mathf.RoundToInt(Mathf.Cos(angle) * (radius + 8));
            int y0 = cy + Mathf.RoundToInt(Mathf.Sin(angle) * (radius + 8));
            int x1 = cx + Mathf.RoundToInt(Mathf.Cos(angle) * (radius + 20));
            int y1 = cy + Mathf.RoundToInt(Mathf.Sin(angle) * (radius + 20));
            c.DrawLine(x0, y0, x1, y1, accent, 4);
        }
        int panelY = y + 6;
        for (int i = 0; i < 3; i++)
        {
            int py = panelY + i * Mathf.Max(10, h / 8);
            c.FillRect(x + 4, py, w - 8, Mathf.Max(6, h / 18), accent2);
            c.FillRect(x + 4, py + Mathf.Max(6, h / 18) / 2, w - 8, 1, accent);
        }
    }

    static void DrawWaves(BusStopPosterBuilder.CanvasContext c, int x, int y, int w, int h,
        Color32 accent, Color32 accent2, Color32 ink)
    {
        c.FillCircle(x + w / 4, y + h * 3 / 4, Mathf.Max(14, Mathf.Min(w, h) / 7), accent);
        for (int row = 0; row < 5; row++)
        {
            int baseY = y + 14 + row * Mathf.Max(9, h / 7);
            Color32 colour = row % 2 == 0 ? accent : accent2;
            for (int px = 0; px < w - 1; px += 4)
            {
                float phase = (px / (float)Mathf.Max(1, w)) * Mathf.PI * 3f + row * .7f;
                int wy = baseY + Mathf.RoundToInt(Mathf.Sin(phase) * 5f);
                c.FillRect(x + px, wy, 4, 3, colour);
            }
        }
        c.FillRect(x + 4, y + h - 8, w - 8, 3, ink);
    }

    static void DrawCoffee(BusStopPosterBuilder.CanvasContext c, int x, int y, int w, int h,
        Color32 accent, Color32 ink)
    {
        int cupW = Mathf.Max(34, w / 2);
        int cupH = Mathf.Max(28, h / 3);
        int cx = x + w / 2 - cupW / 2;
        int cy = y + h / 3;
        c.FillRect(cx, cy, cupW, cupH, accent);
        c.FillRect(cx - 5, cy + cupH, cupW + 10, 6, ink);
        c.DrawCircleRing(cx + cupW + 7, cy + cupH / 2, cupH / 2, Mathf.Max(3, cupH / 8), accent);
        for (int i = 0; i < 3; i++)
        {
            int sx = cx + 10 + i * Mathf.Max(9, cupW / 4);
            c.DrawLine(sx, cy + cupH + 10, sx + 5, cy + cupH + 28, ink, 3);
        }
        c.FillRect(cx + 5, cy + 8, cupW - 10, Mathf.Max(3, cupH / 6), ink);
    }

    static void DrawSignal(BusStopPosterBuilder.CanvasContext c, int x, int y, int w, int h,
        Color32 accent, Color32 accent2)
    {
        int cx = x + w / 2, cy = y + h / 2;
        int r = Mathf.Max(16, Mathf.Min(w, h) / 5);
        c.FillCircle(cx, cy, r, accent2);
        c.DrawCircleRing(cx, cy, r + 10, 4, accent);
        c.DrawCircleRing(cx, cy, r + 24, 3, accent);
        c.FillRect(cx - 10, cy - 20, 20, 40, accent2);
        c.FillRect(cx - 6, cy - 16, 12, 22, accent);
        c.FillCircle(cx, cy - 10, 3, accent2);
    }

    static void DrawBottle(BusStopPosterBuilder.CanvasContext c, int x, int y, int w, int h,
        Color32 accent, Color32 accent2, Color32 ink)
    {
        int bw = Mathf.Max(30, w / 3);
        int bh = Mathf.Max(70, h * 3 / 5);
        int bx = x + w / 2 - bw / 2;
        int by = y + 8;
        c.FillRect(bx, by, bw, bh, accent);
        c.FillRect(bx + bw / 3, by + bh, bw / 3, Mathf.Max(10, h / 12), accent);
        c.FillRect(bx + bw / 3 - 3, by + bh + Mathf.Max(10, h / 12), bw / 3 + 6, 6, ink);
        c.FillRect(bx + 4, by + bh / 3, bw - 8, Mathf.Max(12, bh / 6), accent2);
        c.DrawCircleRing(x + w / 4, y + h * 3 / 4, Mathf.Max(9, w / 8), 3, accent2);
        c.DrawCircleRing(x + w * 3 / 4, y + h * 2 / 3, Mathf.Max(7, w / 10), 3, accent2);
    }

    static void DrawBuildings(BusStopPosterBuilder.CanvasContext c, int x, int y, int w, int h,
        Color32 accent, Color32 accent2, Color32 ink)
    {
        int baseY = y + 4;
        int step = Mathf.Max(18, w / 4);
        int[] heights = { 55, 82, 68, 94 };
        for (int i = 0; i < 4; i++)
        {
            int bw = Mathf.Max(14, step - 8);
            int bh = Mathf.RoundToInt(h * heights[i] / 100f);
            int bx = x + 4 + i * step;
            c.FillRect(bx, baseY, bw, bh, i % 2 == 0 ? accent : accent2);
            for (int wy = baseY + 10; wy < baseY + bh - 6; wy += 16)
                c.FillRect(bx + 4, wy, Mathf.Max(3, bw - 8), 5, ink);
        }
        c.FillRect(x, baseY, w, 4, ink);
    }

    static void DrawEqualizer(BusStopPosterBuilder.CanvasContext c, int x, int y, int w, int h,
        Color32 accent, Color32 accent2)
    {
        int bars = 9;
        int gap = Mathf.Max(4, w / (bars * 3));
        int barW = Mathf.Max(5, (w - gap * (bars + 1)) / bars);
        int[] levels = { 28, 52, 74, 94, 68, 84, 56, 38, 64 };
        int baseY = y + 6;
        for (int i = 0; i < bars; i++)
        {
            int bh = Mathf.Max(8, h * levels[i] / 100);
            int bx = x + gap + i * (barW + gap);
            c.FillRect(bx, baseY, barW, bh, i % 2 == 0 ? accent : accent2);
        }
        c.FillCircle(x + w / 2, y + h - 16, Mathf.Max(10, h / 8), accent);
    }

    static void DrawShield(BusStopPosterBuilder.CanvasContext c, int x, int y, int w, int h,
        Color32 accent, Color32 accent2, Color32 ink)
    {
        int cx = x + w / 2, cy = y + h / 2;
        int r = Mathf.Max(22, Mathf.Min(w, h) / 3);
        c.FillCircle(cx, cy, r, accent);
        c.FillCircle(cx, cy, Mathf.Max(8, r - 10), accent2);
        c.DrawTextCenteredFit("NBO", cx, cy - 8, r * 2 - 24, Mathf.Max(2, r / 10), ink);
        c.FillCircle(cx, y + h - 12, Mathf.Max(5, r / 5), accent2);
    }

    static void DrawFestival(BusStopPosterBuilder.CanvasContext c, int x, int y, int w, int h,
        Color32 accent, Color32 accent2, Color32 ink)
    {
        int cx = x + w / 2;
        int cy = y + h * 2 / 3;
        c.FillCircle(cx, cy, Mathf.Max(18, Mathf.Min(w, h) / 4), accent);
        for (int i = 0; i < 7; i++)
        {
            int bx = x + Mathf.Max(4, w / 12) + i * Mathf.Max(8, w / 9);
            int bh = Mathf.Max(8, h / 4 + (i % 3) * h / 14);
            c.FillRect(bx, y + 6, Mathf.Max(4, w / 24), bh, accent2);
        }
        c.DrawLine(x + 8, y + h - 12, x + w - 8, y + h - 12, ink, 3);
    }

    static Texture2D BuildBenchTexture(int index)
    {
        var texture = new Texture2D(BenchSize, BenchSize, TextureFormat.RGBA32, true)
        {
            wrapMode = TextureWrapMode.Repeat,
            filterMode = FilterMode.Bilinear,
            anisoLevel = 2,
            name = $"BenchSurface_{index}"
        };
        var pixels = new Color32[BenchSize * BenchSize];
        var c = new BusStopPosterBuilder.CanvasContext(pixels, BenchSize, BenchSize);

        Color32 metal = new Color32(42, 47, 49, 255);
        Color32[] woods =
        {
            new Color32(126, 82, 44, 255),
            new Color32(88, 112, 66, 255),
            new Color32(108, 102, 92, 255),
            new Color32(84, 54, 30, 255)
        };
        Color32 wood = woods[Mathf.Clamp(index, 0, woods.Length - 1)];
        c.FillRect(0, 0, BenchSize, BenchSize, metal);

        const int planks = 6;
        int plankH = BenchSize / planks;
        for (int p = 0; p < planks; p++)
        {
            int py = p * plankH + 3;
            int shade = (p % 2 == 0) ? 0 : -14;
            Color32 face = new Color32(
                (byte)Mathf.Clamp(wood.r + shade + index * 4, 20, 245),
                (byte)Mathf.Clamp(wood.g + shade + index * 2, 20, 245),
                (byte)Mathf.Clamp(wood.b + shade, 20, 245), 255);
            c.FillRect(4, py, BenchSize - 8, plankH - 6, face);
            c.FillRect(4, py + 2, BenchSize - 8, 2, new Color32(220, 210, 188, 90));
            // Grain: frequent fine lines, deterministic so the texture is stable.
            for (int g = 0; g < 7; g++)
            {
                int gy = py + 6 + ((p * 17 + g * 13) % Mathf.Max(1, plankH - 12));
                int gx = (p * 31 + g * 47 + index * 19) % Mathf.Max(1, BenchSize - 40);
                c.FillRect(gx, gy, Mathf.Min(BenchSize - gx - 4, 30 + (g * 9) % 60), 1,
                    new Color32(48, 28, 16, 150));
            }
            c.FillCircle(14, py + plankH / 2, 4, metal);
            c.FillCircle(BenchSize - 14, py + plankH / 2, 4, metal);
        }

        if (index == 1)
        {
            c.FillRect(0, BenchSize / 2 - 5, BenchSize, 10, new Color32(220, 196, 96, 220));
        }
        else if (index == 2)
        {
            for (int i = 0; i < 14; i++)
                c.FillCircle(28 + i * 16, 30 + (i % 3) * 7, 2, new Color32(210, 210, 202, 120));
        }

        texture.SetPixels32(pixels);
        texture.Apply(false, false);
        return texture;
    }
}
