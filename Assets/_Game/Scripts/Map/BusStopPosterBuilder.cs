using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates authentic illuminated fake advertising posters, City-Light-Poster totems,
/// and ambient waiting commuters at bus stops to make the transit network feel alive.
/// </summary>
public static class BusStopPosterBuilder
{
    const int PosterWidth = 256;
    const int PosterHeight = 384;
    const int PosterCount = 8;

    static Texture2D[] cachedTextures;
    static Material[] cachedPosterMaterials;
    static Material cachedFrameMaterial;

    /// <summary>
    /// True once the eight poster textures and their materials exist. The loading
    /// card calls <see cref="Prewarm"/> so the first stop does not generate them
    /// while the player is pulling out of the depot.
    /// </summary>
    public static bool IsPrewarmed { get; private set; }

    public static void Prewarm()
    {
        if (IsPrewarmed) return;
        EnsurePostersBuilt();
        GetFrameMaterial();
        IsPrewarmed = true;
    }

    // 5x7 font: ASCII 32 (' ') to 90 ('Z')
    // Each character is represented by 7 rows of 5 bits (0..31)
    static readonly byte[,] Font5x7 = new byte[59, 7]
    {
        // 32 ' '
        { 0, 0, 0, 0, 0, 0, 0 },
        // 33 '!'
        { 4, 4, 4, 4, 4, 0, 4 },
        // 34 '"'
        { 10, 10, 0, 0, 0, 0, 0 },
        // 35 '#'
        { 10, 10, 31, 10, 31, 10, 10 },
        // 36 '$'
        { 4, 15, 20, 14, 5, 30, 4 },
        // 37 '%'
        { 25, 26, 4, 8, 11, 19, 0 },
        // 38 '&'
        { 12, 18, 20, 8, 21, 18, 13 },
        // 39 '\''
        { 4, 4, 0, 0, 0, 0, 0 },
        // 40 '('
        { 2, 4, 8, 8, 8, 4, 2 },
        // 41 ')'
        { 8, 4, 2, 2, 2, 4, 8 },
        // 42 '*'
        { 21, 14, 31, 14, 21, 0, 0 },
        // 43 '+'
        { 0, 4, 4, 31, 4, 4, 0 },
        // 44 ','
        { 0, 0, 0, 0, 6, 4, 8 },
        // 45 '-'
        { 0, 0, 0, 31, 0, 0, 0 },
        // 46 '.'
        { 0, 0, 0, 0, 0, 12, 12 },
        // 47 '/'
        { 1, 2, 4, 8, 16, 0, 0 },
        // 48 '0'
        { 14, 19, 21, 21, 25, 17, 14 },
        // 49 '1'
        { 4, 12, 4, 4, 4, 4, 14 },
        // 50 '2'
        { 14, 17, 1, 6, 8, 16, 31 },
        // 51 '3'
        { 30, 1, 1, 14, 1, 1, 30 },
        // 52 '4'
        { 2, 6, 10, 18, 31, 2, 2 },
        // 53 '5'
        { 31, 16, 30, 1, 1, 17, 14 },
        // 54 '6'
        { 14, 16, 30, 17, 17, 17, 14 },
        // 55 '7'
        { 31, 1, 2, 4, 4, 4, 4 },
        // 56 '8'
        { 14, 17, 17, 14, 17, 17, 14 },
        // 57 '9'
        { 14, 17, 17, 15, 1, 1, 14 },
        // 58 ':'
        { 0, 12, 12, 0, 12, 12, 0 },
        // 59 ';'
        { 0, 12, 12, 0, 6, 4, 8 },
        // 60 '<'
        { 2, 4, 8, 16, 8, 4, 2 },
        // 61 '='
        { 0, 31, 0, 31, 0, 0, 0 },
        // 62 '>'
        { 8, 4, 2, 1, 2, 4, 8 },
        // 63 '?'
        { 14, 17, 1, 2, 4, 0, 4 },
        // 64 '@'
        { 14, 17, 21, 21, 29, 16, 14 },
        // 65 'A'
        { 14, 17, 17, 31, 17, 17, 17 },
        // 66 'B'
        { 30, 17, 17, 30, 17, 17, 30 },
        // 67 'C'
        { 15, 16, 16, 16, 16, 16, 15 },
        // 68 'D'
        { 30, 17, 17, 17, 17, 17, 30 },
        // 69 'E'
        { 31, 16, 16, 30, 16, 16, 31 },
        // 70 'F'
        { 31, 16, 16, 30, 16, 16, 16 },
        // 71 'G'
        { 15, 16, 16, 23, 17, 17, 15 },
        // 72 'H'
        { 17, 17, 17, 31, 17, 17, 17 },
        // 73 'I'
        { 14, 4, 4, 4, 4, 4, 14 },
        // 74 'J'
        { 1, 1, 1, 1, 17, 17, 14 },
        // 75 'K'
        { 17, 18, 20, 24, 20, 18, 17 },
        // 76 'L'
        { 16, 16, 16, 16, 16, 16, 31 },
        // 77 'M'
        { 17, 27, 21, 21, 17, 17, 17 },
        // 78 'N'
        { 17, 25, 21, 19, 17, 17, 17 },
        // 79 'O'
        { 14, 17, 17, 17, 17, 17, 14 },
        // 80 'P'
        { 30, 17, 17, 30, 16, 16, 16 },
        // 81 'Q'
        { 14, 17, 17, 17, 21, 18, 13 },
        // 82 'R'
        { 30, 17, 17, 30, 20, 18, 17 },
        // 83 'S'
        { 15, 16, 16, 14, 1, 1, 30 },
        // 84 'T'
        { 31, 4, 4, 4, 4, 4, 4 },
        // 85 'U'
        { 17, 17, 17, 17, 17, 17, 14 },
        // 86 'V'
        { 17, 17, 17, 17, 17, 10, 4 },
        // 87 'W'
        { 17, 17, 17, 21, 21, 27, 17 },
        // 88 'X'
        { 17, 17, 10, 4, 10, 17, 17 },
        // 89 'Y'
        { 17, 17, 10, 4, 4, 4, 4 },
        // 90 'Z'
        { 31, 1, 2, 4, 8, 16, 31 }
    };

    /// <summary>Decorates a bus stop root with poster totems, shelter wall ads, and ambient commuters.</summary>
    public static void DecorateBusStop(GameObject stopRoot, GameObject shelterModel, int stopIndex, bool leftTraffic)
    {
        if (stopRoot == null) return;

        Bounds bounds = new Bounds(Vector3.zero, new Vector3(2.5f, 2.5f, 4f));
        if (shelterModel != null)
        {
            var renderers = shelterModel.GetComponentsInChildren<Renderer>();
            if (renderers.Length > 0)
            {
                bounds = renderers[0].bounds;
                for (int i = 1; i < renderers.Length; i++) bounds.Encapsulate(renderers[i].bounds);
            }
        }

        int frontPoster = stopIndex % PosterCount;
        int backPoster = (stopIndex + 1) % PosterCount;

        // Position City-Light Totem on sidewalk beside the shelter along the road
        float sideSign = leftTraffic ? -1f : 1f;
        Vector3 totemLocalPos = shelterModel != null
            ? stopRoot.transform.InverseTransformPoint(bounds.center)
            : Vector3.zero;

        // Offset totem to the roadside edge of the pavement, slightly ahead of the shelter
        totemLocalPos.x += sideSign * Mathf.Min(1.8f, bounds.extents.x + 0.6f);
        totemLocalPos.z += bounds.extents.z + 0.7f;
        totemLocalPos.y = 0f;

        GameObject totem = CreatePosterTotem(stopRoot.transform, totemLocalPos, Quaternion.Euler(0, 90f, 0), frontPoster, backPoster);
        KeepTotemClearOfRoad(totem, stopRoot.transform, sideSign);

        // Attach an additional route map / timetable poster directly to the shelter's side or rear wall
        if (shelterModel != null)
        {
            AttachShelterWallPoster(shelterModel, (stopIndex + 3) % PosterCount, leftTraffic ? -1f : 1f);
        }

        // Spawn ambient waiting commuters
        SpawnAmbientCommuters(stopRoot.transform, totemLocalPos, stopIndex);
    }

    /// <summary>Spawns a freestanding poster totem when shelter could not be fitted.</summary>
    public static void SpawnStandalonePosterTotem(GameObject stopRoot, int stopIndex, bool leftTraffic)
    {
        if (stopRoot == null) return;
        float sideSign = leftTraffic ? -1f : 1f;
        // The docking root is deliberately inset toward the kerb so the bus can
        // reach the door trigger. The totem therefore has to sit further out on
        // the pavement: at 1.5 m its 1.15 m-wide base still clipped a narrow
        // lane on downloaded streets. 2.5 m clears the largest scaled footprint
        // while keeping the poster inside the stop's visual footprint.
        Vector3 totemPos = new Vector3(sideSign * 2.5f, 0f, 0.5f);
        GameObject totem = CreatePosterTotem(stopRoot.transform, totemPos, Quaternion.Euler(0, 90f, 0), stopIndex % PosterCount, (stopIndex + 2) % PosterCount);
        KeepTotemClearOfRoad(totem, stopRoot.transform, sideSign);
        SpawnAmbientCommuters(stopRoot.transform, totemPos, stopIndex);
    }

    /// <summary>
    /// Pushes a poster totem outward from the kerb until its whole base clears the
    /// asphalt. The stop root is deliberately inset so the bus door can reach the
    /// trigger; on a narrow downloaded street that puts a 1.15 m-wide totem base
    /// partly in the running lane. Moving only the furniture keeps docking intact.
    /// </summary>
    static void KeepTotemClearOfRoad(GameObject totem, Transform stopRoot, float sideSign)
    {
        if (totem == null || stopRoot == null) return;
        var roads = OSMRoadMeshBuilder.Instance != null ? OSMRoadMeshBuilder.Instance.SurfaceIndex : null;
        if (roads == null) return;

        Vector3 outward = stopRoot.rotation * Vector3.right * sideSign;
        for (int attempt = 0; attempt < 20; attempt++)
        {
            // The totem is built from primitives, so its renderer and collider
            // bounds can lag the transforms set immediately above. Sync before
            // measuring or the first iteration tests the previous position.
            Physics.SyncTransforms();
            var renderers = totem.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0) return;
            Bounds bounds = renderers[0].bounds;
            for (int i = 1; i < renderers.Length; i++) bounds.Encapsulate(renderers[i].bounds);
            foreach (var collider in totem.GetComponentsInChildren<Collider>())
                if (collider.enabled && !collider.isTrigger) bounds.Encapsulate(collider.bounds);

            var footprint = new[]
            {
                new Vector3(bounds.min.x, 0f, bounds.min.z),
                new Vector3(bounds.max.x, 0f, bounds.min.z),
                new Vector3(bounds.max.x, 0f, bounds.max.z),
                new Vector3(bounds.min.x, 0f, bounds.max.z)
            };
            // Leave a shoulder gap, not just zero overlap: a totem flush against
            // the asphalt still reads as an obstacle and can catch a wing mirror.
            if (!roads.OverlapsFootprint(footprint, .65f)) return;
            totem.transform.position += outward * .5f;
            Physics.SyncTransforms();
        }

        // A totem that still cannot find pavement would stand in a lane. The
        // shelter or stop marker remains; losing one poster is better than
        // blocking the running lane on a narrow downloaded street.
        Debug.LogWarning($"[BusStopPosterBuilder] Poster totem at '{stopRoot.name}' has no clear pavement; removing it instead of blocking the road.");
        Object.Destroy(totem);
    }

    /// <summary>Builds a modern illuminated City-Light-Poster totem (double-sided).</summary>
    public static GameObject CreatePosterTotem(Transform parent, Vector3 localPos, Quaternion localRot, int frontIndex, int backIndex)
    {
        var totem = new GameObject("BusStop_PosterTotem");
        totem.transform.SetParent(parent, false);
        totem.transform.localPosition = localPos;
        totem.transform.localRotation = localRot;

        Material frameMat = GetFrameMaterial();
        Material frontPosterMat = GetPosterMaterial(frontIndex);
        Material backPosterMat = GetPosterMaterial(backIndex);

        // Base foot
        var baseFoot = GameObject.CreatePrimitive(PrimitiveType.Cube);
        baseFoot.name = "BaseFoot";
        baseFoot.transform.SetParent(totem.transform, false);
        baseFoot.transform.localPosition = new Vector3(0f, 0.04f, 0f);
        baseFoot.transform.localScale = new Vector3(1.15f, 0.08f, 0.32f);
        baseFoot.GetComponent<Renderer>().sharedMaterial = frameMat;

        // Main totem casing
        var casing = GameObject.CreatePrimitive(PrimitiveType.Cube);
        casing.name = "FrameCasing";
        casing.transform.SetParent(totem.transform, false);
        casing.transform.localPosition = new Vector3(0f, 1.05f, 0f);
        casing.transform.localScale = new Vector3(1.02f, 1.95f, 0.10f);
        casing.GetComponent<Renderer>().sharedMaterial = frameMat;

        // Top LED hood / accent bar
        var topHood = GameObject.CreatePrimitive(PrimitiveType.Cube);
        topHood.name = "TopHood";
        topHood.transform.SetParent(totem.transform, false);
        topHood.transform.localPosition = new Vector3(0f, 2.05f, 0f);
        topHood.transform.localScale = new Vector3(1.06f, 0.06f, 0.14f);
        topHood.GetComponent<Renderer>().sharedMaterial = frameMat;

        // Front poster face
        var frontQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        frontQuad.name = "Poster_Front";
        frontQuad.transform.SetParent(totem.transform, false);
        frontQuad.transform.localPosition = new Vector3(0f, 1.05f, -0.052f);
        frontQuad.transform.localScale = new Vector3(0.92f, 1.68f, 1f);
        frontQuad.transform.localRotation = Quaternion.identity;
        frontQuad.GetComponent<Renderer>().sharedMaterial = frontPosterMat;
        Object.Destroy(frontQuad.GetComponent<Collider>());

        // Back poster face
        var backQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        backQuad.name = "Poster_Back";
        backQuad.transform.SetParent(totem.transform, false);
        backQuad.transform.localPosition = new Vector3(0f, 1.05f, 0.052f);
        backQuad.transform.localScale = new Vector3(0.92f, 1.68f, 1f);
        backQuad.transform.localRotation = Quaternion.Euler(0, 180f, 0);
        backQuad.GetComponent<Renderer>().sharedMaterial = backPosterMat;
        Object.Destroy(backQuad.GetComponent<Collider>());

        return totem;
    }

    /// <summary>
    /// Attaches a poster display pane to the pavement-facing wall of an existing
    /// shelter model. <paramref name="sideSign"/> is negative when the pavement is
    /// on local -X (left-hand traffic) and positive when it is on local +X; the
    /// old fixed -X position put the pane toward the carriageway on right-hand
    /// routes, where its corners could overhang the kerb.
    /// </summary>
    public static void AttachShelterWallPoster(GameObject shelterModel, int posterIndex, float sideSign = -1f)
    {
        if (shelterModel == null) return;

        var wallPoster = GameObject.CreatePrimitive(PrimitiveType.Quad);
        wallPoster.name = "Shelter_WallPoster";
        wallPoster.transform.SetParent(shelterModel.transform, false);
        wallPoster.transform.localPosition = new Vector3(Mathf.Sign(sideSign) * 0.55f, 1.25f, 0.1f);
        wallPoster.transform.localRotation = Quaternion.Euler(0, 90f, 0);
        wallPoster.transform.localScale = new Vector3(0.85f, 1.35f, 1f);
        wallPoster.GetComponent<Renderer>().sharedMaterial = GetPosterMaterial(posterIndex);
        Object.Destroy(wallPoster.GetComponent<Collider>());
    }

    /// <summary>Spawns 1-3 waiting civilian passengers at the bus stop.</summary>
    public static void SpawnAmbientCommuters(Transform stopRoot, Vector3 totemLocalPos, int stopSeed)
    {
        GameObject civilianPrefab = GetCivilianPrefab();
        if (civilianPrefab == null) return;

        int commuterCount = 1 + (stopSeed % 3); // 1 to 3 commuters
        for (int c = 0; c < commuterCount; c++)
        {
            var commuter = Object.Instantiate(civilianPrefab, stopRoot);
            commuter.name = $"AmbientCommuter_{c + 1}";

            // Position civilian near the totem or waiting under shelter
            Vector3 offset = (c == 0)
                ? totemLocalPos + new Vector3(0.8f, 0f, -0.4f) // looking toward poster
                : new Vector3(totemLocalPos.x * 0.4f, 0f, -0.8f * c);

            offset.y = 0.02f;
            commuter.transform.localPosition = offset;

            // Facing angle: looking toward oncoming traffic or poster
            float yaw = (c == 0) ? -90f : 10f + c * 25f;
            commuter.transform.localRotation = Quaternion.Euler(0f, yaw, 0f);

            // Add subtle alive breathing / head motion
            var motion = commuter.AddComponent<AmbientCommuterMotion>();
            motion.phase = stopSeed * 1.7f + c * 2.3f;
        }
    }

    static GameObject GetCivilianPrefab()
    {
        var catalog = Resources.Load<LiveCityCatalog>("LiveCityCatalog");
        if (catalog != null)
        {
            foreach (var e in catalog.entries)
                if (e != null && e.kind == LiveCityCatalog.Kind.Pedestrian && e.prefab != null)
                    return e.prefab;
        }
        return Resources.Load<GameObject>("LiveCityGenerated/ab834887507b6c0fba84796be9f2c3ea");
    }

    public static Material GetPosterMaterial(int index)
    {
        EnsurePostersBuilt();
        index = Mathf.Clamp(index, 0, PosterCount - 1);
        return cachedPosterMaterials[index];
    }

    public static Material GetFrameMaterial()
    {
        if (cachedFrameMaterial == null)
        {
            cachedFrameMaterial = new Material(BuiltInShaders.Lit) { name = "BusStop_Poster_Frame" };
            Color frameColor = new Color(0.12f, 0.13f, 0.14f);
            cachedFrameMaterial.color = frameColor;
            if (cachedFrameMaterial.HasProperty("_Metallic")) cachedFrameMaterial.SetFloat("_Metallic", 0.85f);
            if (cachedFrameMaterial.HasProperty("_Glossiness")) cachedFrameMaterial.SetFloat("_Glossiness", 0.65f);
            if (cachedFrameMaterial.HasProperty("_Smoothness")) cachedFrameMaterial.SetFloat("_Smoothness", 0.65f);
        }
        return cachedFrameMaterial;
    }

    static void EnsurePostersBuilt()
    {
        if (cachedPosterMaterials != null && cachedPosterMaterials.Length == PosterCount)
            return;

        cachedTextures = new Texture2D[PosterCount];
        cachedPosterMaterials = new Material[PosterCount];

        Shader shader = BuiltInShaders.Lit;

        for (int i = 0; i < PosterCount; i++)
        {
            var tex = BuildPosterTexture(i);
            cachedTextures[i] = tex;

            var mat = new Material(shader) { name = $"BusStop_PosterMat_{i}" };
            mat.mainTexture = tex;
            if (mat.HasProperty("_MainTex")) mat.SetTexture("_MainTex", tex);
            if (mat.HasProperty("_EmissionColor"))
            {
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", new Color(0.18f, 0.18f, 0.18f));
                if (mat.HasProperty("_EmissionMap")) mat.SetTexture("_EmissionMap", tex);
            }
            cachedPosterMaterials[i] = mat;
        }
    }

    static Texture2D BuildPosterTexture(int index)
    {
        var tex = new Texture2D(PosterWidth, PosterHeight, TextureFormat.RGBA32, false)
        {
            wrapMode = TextureWrapMode.Clamp,
            filterMode = FilterMode.Bilinear,
            name = $"Poster_{index}"
        };

        var pixels = new Color32[PosterWidth * PosterHeight];
        var ctx = new CanvasContext(pixels, PosterWidth, PosterHeight);

        switch (index)
        {
            case 0: BuildPoster_MetroRapid(ctx); break;
            case 1: BuildPoster_SparkCola(ctx); break;
            case 2: BuildPoster_CafeAurora(ctx); break;
            case 3: BuildPoster_SkylineSuites(ctx); break;
            case 4: BuildPoster_RealbusCare(ctx); break;
            case 5: BuildPoster_SunsetFestival(ctx); break;
            case 6: BuildPoster_BurgerCrunch(ctx); break;
            default: BuildPoster_Pulse5G(ctx); break;
        }

        tex.SetPixels32(pixels);
        tex.Apply(false, false);
        return tex;
    }

    // Poster 0: Metro Rapid Transit Network Map & Timetable
    static void BuildPoster_MetroRapid(CanvasContext c)
    {
        c.FillGradient(new Color32(12, 24, 45, 255), new Color32(20, 48, 88, 255));
        c.FillRect(0, 320, 256, 64, new Color32(0, 168, 232, 255));
        c.DrawText("METRO RAPID", 20, 345, 3, new Color32(255, 255, 255, 255));
        c.DrawText("CITY NETWORK", 20, 326, 2, new Color32(220, 245, 255, 255));

        // Transit map lines
        c.DrawLine(24, 180, 232, 180, new Color32(235, 60, 60, 255), 6);  // Red Line
        c.DrawLine(40, 260, 210, 120, new Color32(0, 190, 240, 255), 6);  // Blue Line
        c.DrawLine(30, 110, 220, 240, new Color32(255, 195, 0, 255), 6);  // Yellow Line
        c.DrawLine(128, 90, 128, 270, new Color32(40, 190, 90, 255), 6);  // Green Line

        // Station transfer hubs
        int[] hubsX = { 60, 95, 128, 160, 195 };
        int[] hubsY = { 180, 210, 180, 150, 180 };
        for (int i = 0; i < hubsX.Length; i++)
        {
            c.FillCircle(hubsX[i], hubsY[i], 9, new Color32(255, 255, 255, 255));
            c.FillCircle(hubsX[i], hubsY[i], 5, new Color32(15, 30, 60, 255));
        }

        // Timetable info
        c.FillRect(14, 25, 228, 55, new Color32(10, 20, 38, 220));
        c.DrawText("TIMETABLE", 24, 62, 2, new Color32(0, 200, 255, 255));
        c.DrawText("EVERY 5 MINS", 24, 45, 2, new Color32(255, 255, 255, 255));
        c.DrawText("ALL PASSES ACCEPTED", 24, 31, 1, new Color32(180, 200, 220, 255));
        c.DrawQrCode(190, 30, 42, new Color32(255, 255, 255, 255));
    }

    // Poster 1: Spark Cola Refreshment
    static void BuildPoster_SparkCola(CanvasContext c)
    {
        c.FillGradient(new Color32(185, 20, 30, 255), new Color32(110, 8, 16, 255));
        c.FillRect(0, 315, 256, 69, new Color32(255, 210, 0, 255));
        c.DrawText("SPARK COLA", 24, 345, 3, new Color32(180, 15, 25, 255));
        c.DrawText("MAXIMUM TASTE", 24, 324, 2, new Color32(20, 20, 20, 255));

        // Bottle silhouette
        c.FillRect(112, 100, 32, 140, new Color32(255, 255, 255, 255));
        c.FillRect(120, 240, 16, 35, new Color32(255, 255, 255, 255));
        c.FillCircle(128, 170, 38, new Color32(255, 210, 0, 255));
        c.FillCircle(128, 170, 32, new Color32(180, 15, 25, 255));
        c.DrawText("ICE", 112, 162, 2, new Color32(255, 255, 255, 255));

        // Floating bubbles
        c.DrawCircleRing(65, 140, 12, 2, new Color32(255, 255, 255, 180));
        c.DrawCircleRing(190, 175, 16, 2, new Color32(255, 255, 255, 180));
        c.DrawCircleRing(75, 225, 8, 2, new Color32(255, 255, 255, 180));
        c.DrawCircleRing(180, 110, 10, 2, new Color32(255, 255, 255, 180));

        // Footer discount stamp
        c.FillRect(14, 20, 228, 55, new Color32(20, 20, 20, 220));
        c.DrawText("REFRESH NOW", 24, 55, 2, new Color32(255, 210, 0, 255));
        c.DrawText("ONLY $1.99 AT KIOSKS", 24, 35, 2, new Color32(255, 255, 255, 255));
        c.DrawBarcode(180, 26, 50, 42, new Color32(255, 255, 255, 255));
    }

    // Poster 2: Cafe Aurora Artisan Coffee
    static void BuildPoster_CafeAurora(CanvasContext c)
    {
        c.FillGradient(new Color32(48, 26, 16, 255), new Color32(28, 14, 8, 255));
        c.FillRect(0, 310, 256, 74, new Color32(230, 200, 160, 255));
        c.DrawText("CAFE AURORA", 20, 345, 3, new Color32(45, 22, 12, 255));
        c.DrawText("NEXT STOP ESPRESSO", 20, 322, 2, new Color32(80, 45, 25, 255));

        // Steaming Coffee Cup Icon
        c.FillRect(90, 130, 65, 55, new Color32(240, 220, 190, 255));
        c.FillCircle(155, 155, 18, new Color32(240, 220, 190, 255));
        c.FillCircle(155, 155, 10, new Color32(48, 26, 16, 255));
        c.FillRect(80, 120, 85, 10, new Color32(240, 220, 190, 255));

        // Steam waves
        c.DrawLine(100, 195, 105, 240, new Color32(220, 180, 140, 180), 3);
        c.DrawLine(122, 198, 120, 248, new Color32(220, 180, 140, 180), 3);
        c.DrawLine(144, 195, 140, 240, new Color32(220, 180, 140, 180), 3);

        c.DrawText("FRESH ROAST DAILY", 28, 90, 2, new Color32(230, 200, 160, 255));
        c.FillRect(14, 20, 228, 50, new Color32(20, 10, 6, 240));
        c.DrawText("SHOW TRANSIT PASS", 24, 48, 2, new Color32(255, 215, 0, 255));
        c.DrawText("GET 20% OFF COFFEE", 24, 30, 2, new Color32(255, 255, 255, 255));
    }

    // Poster 3: Skyline Suites Luxury Apartments
    static void BuildPoster_SkylineSuites(CanvasContext c)
    {
        c.FillGradient(new Color32(32, 12, 58, 255), new Color32(10, 4, 20, 255));
        c.FillRect(0, 315, 256, 69, new Color32(180, 80, 240, 255));
        c.DrawText("SKYLINE SUITES", 18, 345, 3, new Color32(255, 255, 255, 255));
        c.DrawText("CITY LIVING", 18, 324, 2, new Color32(240, 210, 255, 255));

        // Skyscraper silhouettes
        c.FillRect(30, 60, 45, 170, new Color32(65, 30, 100, 255));
        c.FillRect(85, 60, 55, 230, new Color32(85, 40, 130, 255));
        c.FillRect(150, 60, 40, 190, new Color32(65, 30, 100, 255));
        c.FillRect(198, 60, 35, 140, new Color32(45, 20, 75, 255));

        // Lit windows
        for (int y = 90; y < 270; y += 22)
        {
            c.FillRect(98, y, 12, 10, new Color32(255, 230, 120, 255));
            c.FillRect(118, y, 12, 10, new Color32(255, 230, 120, 255));
        }

        c.FillRect(14, 18, 228, 50, new Color32(20, 8, 36, 230));
        c.DrawText("NOW LEASING", 24, 46, 2, new Color32(240, 120, 255, 255));
        c.DrawText("2 & 3 BEDROOM SUITES", 24, 28, 2, new Color32(255, 255, 255, 255));
    }

    // Poster 4: RealBus Care - Commuter Insurance
    static void BuildPoster_RealbusCare(CanvasContext c)
    {
        c.FillGradient(new Color32(10, 80, 50, 255), new Color32(6, 42, 26, 255));
        c.FillRect(0, 320, 256, 64, new Color32(0, 205, 120, 255));
        c.DrawText("REALBUS CARE", 18, 348, 3, new Color32(10, 40, 25, 255));
        c.DrawText("COMMUTE WITH PEACE", 18, 327, 2, new Color32(230, 255, 240, 255));

        // Shield emblem
        c.FillCircle(128, 190, 55, new Color32(255, 255, 255, 255));
        c.FillCircle(128, 190, 47, new Color32(10, 80, 50, 255));
        c.DrawText("SAFE", 98, 182, 3, new Color32(0, 220, 130, 255));

        // 5 gold stars
        for (int i = 0; i < 5; i++)
        {
            c.FillCircle(68 + i * 30, 115, 8, new Color32(255, 215, 0, 255));
        }

        c.FillRect(14, 20, 228, 55, new Color32(5, 30, 18, 240));
        c.DrawText("24/7 ROADSIDE COVER", 24, 52, 2, new Color32(0, 220, 130, 255));
        c.DrawText("FREE PASS PROTECTION", 24, 32, 2, new Color32(255, 255, 255, 255));
    }

    // Poster 5: Sunset Beat Music Festival
    static void BuildPoster_SunsetFestival(CanvasContext c)
    {
        c.FillGradient(new Color32(230, 70, 25, 255), new Color32(130, 20, 120, 255));
        c.FillRect(0, 315, 256, 69, new Color32(255, 225, 40, 255));
        c.DrawText("SUNSET BEAT", 22, 345, 3, new Color32(180, 20, 20, 255));
        c.DrawText("MUSIC FESTIVAL 2026", 22, 324, 2, new Color32(40, 10, 40, 255));

        // Giant sun circle
        c.FillCircle(128, 180, 60, new Color32(255, 200, 30, 220));

        // Equalizer waves
        int[] eqH = { 20, 45, 75, 95, 110, 130, 115, 90, 65, 35 };
        for (int i = 0; i < eqH.Length; i++)
        {
            c.FillRect(38 + i * 18, 100, 12, eqH[i], new Color32(255, 255, 255, 190));
        }

        c.FillRect(14, 20, 228, 55, new Color32(30, 5, 35, 230));
        c.DrawText("AUG 15-17 CENTRAL PARK", 24, 52, 2, new Color32(255, 220, 50, 255));
        c.DrawText("30+ ARTISTS • BUY PASS", 24, 32, 2, new Color32(255, 255, 255, 255));
    }

    // Poster 6: Burger Crunch Diner
    static void BuildPoster_BurgerCrunch(CanvasContext c)
    {
        c.FillGradient(new Color32(210, 110, 15, 255), new Color32(120, 25, 15, 255));
        c.FillRect(0, 315, 256, 69, new Color32(255, 220, 40, 255));
        c.DrawText("BURGER CRUNCH", 14, 345, 3, new Color32(110, 20, 10, 255));
        c.DrawText("100% REAL FLAVOR", 14, 324, 2, new Color32(40, 20, 10, 255));

        // Burger layers
        c.FillRect(70, 210, 116, 25, new Color32(235, 180, 95, 255)); // top bun
        c.FillRect(75, 190, 106, 12, new Color32(70, 180, 40, 255));   // lettuce
        c.FillRect(70, 170, 116, 15, new Color32(240, 45, 35, 255));   // tomato
        c.FillRect(70, 145, 116, 20, new Color32(95, 45, 20, 255));    // patty
        c.FillRect(70, 120, 116, 20, new Color32(235, 180, 95, 255)); // bottom bun

        c.FillCircle(195, 205, 30, new Color32(255, 215, 0, 255));
        c.DrawText("$4.99", 175, 198, 2, new Color32(120, 20, 10, 255));

        c.FillRect(14, 20, 228, 55, new Color32(25, 10, 5, 230));
        c.DrawText("OPEN UNTIL 2 AM", 24, 52, 2, new Color32(255, 215, 0, 255));
        c.DrawText("JUST 1 STOP AWAY", 24, 32, 2, new Color32(255, 255, 255, 255));
    }

    // Poster 7: Pulse 5G Mobile
    static void BuildPoster_Pulse5G(CanvasContext c)
    {
        c.FillGradient(new Color32(10, 16, 38, 255), new Color32(4, 8, 20, 255));
        c.FillRect(0, 315, 256, 69, new Color32(0, 225, 255, 255));
        c.DrawText("PULSE 5G", 26, 345, 3, new Color32(5, 15, 40, 255));
        c.DrawText("ULTRA RAPID METRO", 26, 324, 2, new Color32(20, 40, 80, 255));

        // Concentric network wave arcs
        c.DrawCircleRing(128, 165, 30, 4, new Color32(0, 230, 255, 255));
        c.DrawCircleRing(128, 165, 55, 4, new Color32(0, 200, 255, 200));
        c.DrawCircleRing(128, 165, 80, 4, new Color32(0, 170, 255, 140));

        c.DrawText("1000 MBPS", 75, 158, 3, new Color32(255, 255, 255, 255));

        c.FillRect(14, 20, 228, 55, new Color32(8, 14, 30, 240));
        c.DrawText("99.9% UNDERGROUND", 24, 52, 2, new Color32(0, 230, 255, 255));
        c.DrawText("UNLIMITED COMMUTER DATA", 24, 32, 2, new Color32(255, 255, 255, 255));
    }

    // Drawing context helper class. Internal rather than private so the street
    // advertisement library can use the same pixel font and drawing primitives
    // without carrying a second copy of the glyph table.
    internal class CanvasContext
    {
        readonly Color32[] pixels;
        readonly int width;
        readonly int height;

        public int Width => width;
        public int Height => height;

        public CanvasContext(Color32[] pixels, int width, int height)
        {
            this.pixels = pixels;
            this.width = width;
            this.height = height;
        }

        public void FillRect(int x, int y, int w, int h, Color32 c)
        {
            int x1 = Mathf.Max(0, x), x2 = Mathf.Min(width, x + w);
            int y1 = Mathf.Max(0, y), y2 = Mathf.Min(height, y + h);
            for (int cy = y1; cy < y2; cy++)
            {
                int row = cy * width;
                for (int cx = x1; cx < x2; cx++)
                    pixels[row + cx] = c;
            }
        }

        public void FillGradient(Color32 top, Color32 bottom)
        {
            for (int y = 0; y < height; y++)
            {
                float t = (float)y / Mathf.Max(1, height - 1);
                Color32 c = new Color32(
                    (byte)Mathf.Lerp(bottom.r, top.r, t),
                    (byte)Mathf.Lerp(bottom.g, top.g, t),
                    (byte)Mathf.Lerp(bottom.b, top.b, t),
                    255);
                int row = y * width;
                for (int x = 0; x < width; x++) pixels[row + x] = c;
            }
        }

        public void FillCircle(int cx, int cy, int r, Color32 c)
        {
            int r2 = r * r;
            int x1 = Mathf.Max(0, cx - r), x2 = Mathf.Min(width, cx + r + 1);
            int y1 = Mathf.Max(0, cy - r), y2 = Mathf.Min(height, cy + r + 1);
            for (int y = y1; y < y2; y++)
            {
                int dy2 = (y - cy) * (y - cy);
                int row = y * width;
                for (int x = x1; x < x2; x++)
                {
                    if ((x - cx) * (x - cx) + dy2 <= r2)
                        pixels[row + x] = c;
                }
            }
        }

        public void DrawCircleRing(int cx, int cy, int r, int thickness, Color32 c)
        {
            int outer2 = r * r;
            int inner2 = (r - thickness) * (r - thickness);
            int x1 = Mathf.Max(0, cx - r), x2 = Mathf.Min(width, cx + r + 1);
            int y1 = Mathf.Max(0, cy - r), y2 = Mathf.Min(height, cy + r + 1);
            for (int y = y1; y < y2; y++)
            {
                int dy2 = (y - cy) * (y - cy);
                int row = y * width;
                for (int x = x1; x < x2; x++)
                {
                    int d2 = (x - cx) * (x - cx) + dy2;
                    if (d2 <= outer2 && d2 >= inner2)
                        pixels[row + x] = c;
                }
            }
        }

        public void DrawLine(int x0, int y0, int x1, int y1, Color32 c, int thickness)
        {
            int half = thickness / 2;
            int dx = Mathf.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = -Mathf.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = dx + dy;
            while (true)
            {
                FillRect(x0 - half, y0 - half, thickness, thickness, c);
                if (x0 == x1 && y0 == y1) break;
                int e2 = 2 * err;
                if (e2 >= dy) { err += dy; x0 += sx; }
                if (e2 <= dx) { err += dx; y0 += sy; }
            }
        }

        public void DrawText(string text, int x, int y, int scale, Color32 c)
        {
            if (string.IsNullOrEmpty(text)) return;
            int curX = x;
            for (int i = 0; i < text.Length; i++)
            {
                char ch = char.ToUpperInvariant(text[i]);
                int idx = ch - 32;
                if (idx >= 0 && idx < 59)
                {
                    for (int row = 0; row < 7; row++)
                    {
                        byte b = Font5x7[idx, row];
                        int py = y + (6 - row) * scale;
                        for (int col = 0; col < 5; col++)
                        {
                            if ((b & (16 >> col)) != 0)
                            {
                                int px = curX + col * scale;
                                FillRect(px, py, scale, scale, c);
                            }
                        }
                    }
                }
                curX += 6 * scale;
            }
        }

        /// <summary>Width of a line in pixels at a given 5x7 font scale.</summary>
        public int TextWidth(string text, int scale)
        {
            if (string.IsNullOrEmpty(text) || scale <= 0) return 0;
            return Mathf.Max(0, text.Length * 6 * scale - scale);
        }

        /// <summary>Largest integer font scale that keeps a line inside a width.</summary>
        public int FitScale(string text, int maxWidth, int maxScale)
        {
            int scale = Mathf.Max(1, maxScale);
            while (scale > 1 && TextWidth(text, scale) > maxWidth)
                scale--;
            return scale;
        }

        public void DrawTextCentered(string text, int centerX, int y, int scale, Color32 c)
        {
            DrawText(text, centerX - TextWidth(text, scale) / 2, y, scale, c);
        }

        public void DrawTextFit(string text, int x, int y, int maxWidth, int maxScale, Color32 c)
        {
            DrawText(text, x, y, FitScale(text, maxWidth, maxScale), c);
        }

        public void DrawTextCenteredFit(string text, int centerX, int y, int maxWidth, int maxScale, Color32 c)
        {
            int scale = FitScale(text, maxWidth, maxScale);
            DrawText(text, centerX - TextWidth(text, scale) / 2, y, scale, c);
        }

        public void DrawQrCode(int x, int y, int size, Color32 c)
        {
            FillRect(x, y, size, size, c);
            FillRect(x + 3, y + 3, size - 6, size - 6, new Color32(10, 10, 10, 255));
            int sub = size / 3;
            FillRect(x + 4, y + size - sub - 4, sub, sub, c);
            FillRect(x + size - sub - 4, y + size - sub - 4, sub, sub, c);
            FillRect(x + 4, y + 4, sub, sub, c);
        }

        public void DrawBarcode(int x, int y, int w, int h, Color32 c)
        {
            int cx = x;
            uint pattern = 0b101100110101100101101011;
            while (cx < x + w)
            {
                bool bar = (pattern & 1) != 0;
                pattern = (pattern >> 1) | ((pattern & 1) << 23);
                if (bar) FillRect(cx, y, 2, h, c);
                cx += 3;
            }
        }
    }
}

/// <summary>Subtle breathing and head turn motion for ambient bus stop commuters.</summary>
public class AmbientCommuterMotion : MonoBehaviour
{
    public float phase;
    Quaternion initialRot;

    void Start()
    {
        initialRot = transform.localRotation;
    }

    void Update()
    {
        float t = Time.time + phase;
        float sway = Mathf.Sin(t * 0.7f) * 2.5f;
        transform.localRotation = initialRot * Quaternion.Euler(0f, sway, 0f);
    }
}
