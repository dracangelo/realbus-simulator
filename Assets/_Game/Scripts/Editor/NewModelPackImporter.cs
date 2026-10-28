#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

/// <summary>
/// Imports the city, road-sign, traffic-light and road packs that arrived after
/// the original model wiring. The city pack is a whole street scene, so only the
/// named buildings, palms, street lights and trees are extracted; ground slabs and
/// camera/light helpers are never saved. Road signs become kerb props, the traffic
/// light becomes the signal housing, and the low-poly road pieces become a small
/// module catalog used for drive-in aprons.
/// </summary>
public static class NewModelPackImporter
{
    const string CityBuildingsSource = "Assets/unity models/citymodel/city buildings/CITY+BUILDINGS.fbx";
    const string CityModelsSource = "Assets/unity models/citymodel/city model/city+models+byothers.fbx";
    const string RoadSignsSource = "Assets/unity models/road signs/street+prop+texture2/roadsigns.fbx";
    const string TrafficLightSource = "Assets/unity models/traffic lights/lights/Cube.002.fbx";
    const string RoadLowPolySource = "Assets/unity models/road/low poly roads/low poly roads.fbx";

    const string CityOutput = "Assets/_Game/Resources/CityPackGenerated";
    const string BuildingOutput = CityOutput + "/Buildings";
    const string BuildingMaterialOutput = BuildingOutput + "/Materials";
    const string BuildingMeshOutput = BuildingOutput + "/Meshes";
    const string PropOutput = CityOutput + "/Props";
    const string PropMaterialOutput = PropOutput + "/Materials";
    const string PropMeshOutput = PropOutput + "/Meshes";

    const string RoadSignOutput = "Assets/_Game/Resources/RoadSignsGenerated";
    const string RoadSignMaterialOutput = RoadSignOutput + "/Materials";
    const string RoadSignMeshOutput = RoadSignOutput + "/Meshes";

    const string TrafficLightOutput = "Assets/_Game/Resources/TrafficLightsGenerated";
    const string TrafficLightMaterialOutput = TrafficLightOutput + "/Materials";
    const string TrafficLightMeshOutput = TrafficLightOutput + "/Meshes";
    const string TrafficLightPrefab = TrafficLightOutput + "/TrafficLightModel.prefab";

    const string RoadModuleOutput = "Assets/_Game/Resources/RoadModulesGenerated";
    const string RoadModuleMaterialOutput = RoadModuleOutput + "/Materials";
    const string RoadModuleMeshOutput = RoadModuleOutput + "/Meshes";

    const string BuildingCatalogPath = "Assets/_Game/Resources/BuildingCatalog.asset";
    const string LiveCityCatalogPath = "Assets/_Game/Resources/LiveCityCatalog.asset";
    const string RoadModuleCatalogPath = "Assets/_Game/Resources/RoadModuleCatalog.asset";
    const string ReportPath = "Assets/_Game/Generated/NEW_MODEL_PACK_WIRING.md";

    static readonly string[] CityBuildingNames = { "Group3", "Group24" };

    // Single buildings in the city-model scene. Variants such as .001 are kept
    // only when they are plausible buildings; most of them are ground slabs or
    // roof boxes and fail the height/triangle tests below.
    static readonly string[] CityModelBuildingBases =
    {
        "45building", "AIT01W2W1", "AIT01W2W2", "AIT01W2W6", "AIT01W2W7", "AIT01W2W8",
        "hirise1", "hirise7", "hirise8", "hirise12", "hirise13",
        "hithepeak", "hivizcayne", "LOWRISEMODERNCONDO2", "LOWRISEMODERNCONDO3",
        "nationalarchives", "Plane001"
    };

    static readonly string[] CityModelPropBases = { "Tree_03_1K", "Tree_05_1K", "Tree_07_1K" };

    const string PalmPrefix = "Component_4_";
    const string StreetLightPrefix = "Component_3_";

    const string RoadTextureFolder = "Assets/unity models/road/low poly roads";
    // The pack keeps each texture set in its own subfolder rather than beside the
    // FBX, so these paths name the set explicitly.
    // A plain concrete set rather than a lane-marked road: these modules pave a
    // forecourt, and a lane line repeated every tile reads as a painted grid.
    const string RoadAlbedo = RoadTextureFolder + "/Concrete020_2K-PNG/Concrete020_2K_Color.png";
    const string RoadNormal = RoadTextureFolder + "/Concrete020_2K-PNG/Concrete020_2K_Normal.png";
    const string SidewalkAlbedo = RoadTextureFolder + "/Concrete Sidewalk 02 1k/TexturesCom_ConcreteSidewalk02_1K_albedo.tif";
    const string SidewalkNormal = RoadTextureFolder + "/Concrete Sidewalk 02 1k/TexturesCom_ConcreteSidewalk02_1K_normal.tif";

    [InitializeOnLoadMethod]
    static void QueueInitialImport()
    {
        EditorApplication.delayCall += () =>
        {
            if (Application.isPlaying || EditorApplication.isPlayingOrWillChangePlaymode) return;
            if (NeedsBuild()) Build();
        };
    }

    [MenuItem("Tools/RealBus/Setup/Import New Model Packs")]
    public static void BuildFromMenu()
    {
        Build();
        ImportedModelWiringGenerator.GenerateGasStationsFromBatch();
        LiveCityModelImporter.Build();
    }

    public static bool NeedsBuild()
    {
        if (!File.Exists(ReportPath)) return true;
        if (!Directory.Exists(BuildingOutput) || !Directory.Exists(PropOutput)) return true;
        if (!Directory.Exists(RoadSignOutput) || !Directory.Exists(RoadModuleOutput)) return true;
        if (!File.Exists(TrafficLightPrefab)) return true;
        return AssetDatabase.LoadAssetAtPath<RoadModuleCatalog>(RoadModuleCatalogPath) == null;
    }

    public static void Build()
    {
        EnsureFolders();
        AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
        ImportedModelImportSettings.EnsureStandard(new[]
        {
            CityBuildingsSource, CityModelsSource, RoadSignsSource, TrafficLightSource, RoadLowPolySource
        });
        AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
        EnsureNormalMap(RoadNormal);
        EnsureNormalMap(SidewalkNormal);

        var report = new StringBuilder("# New model pack wiring\n\n");
        report.AppendLine("Generated from the city, road-sign, traffic-light and low-poly road packs under `Assets/unity models`.\n");
        report.AppendLine("Re-run `Tools > RealBus > Setup > Import New Model Packs` after replacing a source FBX.\n");

        var liveEntries = new List<LiveCityCatalog.Entry>();
        var buildingEntries = new List<BuildingCatalog.Entry>();
        var roadModules = new List<RoadModuleCatalog.Entry>();
        var usedPrefabs = new HashSet<string>(StringComparer.Ordinal);

        BuildCityPack(liveEntries, buildingEntries, usedPrefabs, report);
        BuildRoadSigns(liveEntries, usedPrefabs, report);
        BuildTrafficLight(usedPrefabs, report);
        BuildRoadModules(roadModules, usedPrefabs, report);

        MergeLiveCityCatalog(liveEntries);
        MergeBuildingCatalog(buildingEntries);
        MergeRoadModuleCatalog(roadModules);

        DeleteStalePrefabs(BuildingOutput, usedPrefabs);
        DeleteStalePrefabs(PropOutput, usedPrefabs);
        DeleteStalePrefabs(RoadSignOutput, usedPrefabs);
        DeleteStalePrefabs(RoadModuleOutput, usedPrefabs);
        DeleteStalePrefabs(TrafficLightOutput, usedPrefabs);

        File.WriteAllText(ReportPath, report.ToString());
        AssetDatabase.ImportAsset(ReportPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"[NewModelPack] Wired {buildingEntries.Count} city buildings, {liveEntries.Count} city/sign props, " +
                  $"1 traffic-light housing and {roadModules.Count} road modules. See {ReportPath}.");
    }

    // ── City pack ─────────────────────────────────────────────────────────────

    static void BuildCityPack(List<LiveCityCatalog.Entry> liveEntries, List<BuildingCatalog.Entry> buildingEntries,
        HashSet<string> usedPrefabs, StringBuilder report)
    {
        report.AppendLine("## City buildings\n");
        var buildingsScene = LoadSource(CityBuildingsSource);
        if (buildingsScene != null)
        {
            try
            {
                foreach (string name in CityBuildingNames)
                {
                    Transform child = FindDeepChild(buildingsScene.transform, name);
                    if (child == null)
                    {
                        report.AppendLine($"- `{name}` — not found");
                        continue;
                    }
                    var result = Extract(child, $"{BuildingOutput}/{SafeStem(name)}.prefab",
                        BuildingMaterialOutput, BuildingMeshOutput, 0f, addCollider: true);
                    if (result.prefab == null)
                    {
                        report.AppendLine($"- `{name}` — skipped: no renderable mesh");
                        continue;
                    }
                    usedPrefabs.Add(AssetDatabase.GetAssetPath(result.prefab));
                    buildingEntries.Add(new BuildingCatalog.Entry
                    {
                        prefab = result.prefab,
                        bounds = result.bounds,
                        triangles = result.triangles,
                        sourceName = name
                    });
                    report.AppendLine($"- **{name}** — `{AssetDatabase.GetAssetPath(result.prefab)}`; " +
                                      $"{result.bounds.size.x:F1}×{result.bounds.size.y:F1}×{result.bounds.size.z:F1} m, " +
                                      $"{result.triangles} tris, {result.renderersBefore}→{result.renderersAfter} renderers");
                }
            }
            finally { ReleaseSource(buildingsScene); }
        }
        else
        {
            report.AppendLine($"- source missing: `{CityBuildingsSource}`");
        }

        report.AppendLine("\n## City palms, trees and street lights\n");
        BuildPropsFromScene(CityBuildingsSource, PalmPrefix, LiveCityCatalog.Kind.Tree, "palm", liveEntries, usedPrefabs, report);
        BuildPropsFromScene(CityBuildingsSource, StreetLightPrefix, LiveCityCatalog.Kind.StreetLight, "street light", liveEntries, usedPrefabs, report);

        var modelsScene = LoadSource(CityModelsSource);
        if (modelsScene != null)
        {
            try
            {
                var seen = new HashSet<string>(StringComparer.Ordinal);
                foreach (Transform child in modelsScene.transform)
                {
                    string baseName = StripNumericSuffix(child.name);
                    if (!CityModelBuildingBases.Contains(baseName)) continue;
                    if (child.GetComponentsInChildren<Renderer>(true).Length == 0) continue;

                    Bounds world = WorldBounds(child.gameObject);
                    int triangles = TriangleCount(child.gameObject);
                    if (!IsPlausibleBuilding(world, triangles)) continue;
                    string fingerprint = Fingerprint(world, triangles, child.gameObject);
                    if (!seen.Add(fingerprint)) continue;

                    var result = Extract(child, $"{BuildingOutput}/{SafeStem(child.name)}.prefab",
                        BuildingMaterialOutput, BuildingMeshOutput, 0f, addCollider: true);
                    if (result.prefab == null) continue;
                    usedPrefabs.Add(AssetDatabase.GetAssetPath(result.prefab));
                    buildingEntries.Add(new BuildingCatalog.Entry
                    {
                        prefab = result.prefab,
                        bounds = result.bounds,
                        triangles = result.triangles,
                        sourceName = child.name
                    });
                    report.AppendLine($"- **{child.name}** — `{AssetDatabase.GetAssetPath(result.prefab)}`; " +
                                      $"{result.bounds.size.x:F1}×{result.bounds.size.y:F1}×{result.bounds.size.z:F1} m, " +
                                      $"{result.triangles} tris, {result.renderersBefore}→{result.renderersAfter} renderers");
                }

                foreach (string baseName in CityModelPropBases)
                {
                    var candidates = modelsScene.transform.Cast<Transform>()
                        .Where(child => StripNumericSuffix(child.name) == baseName)
                        .Where(child => child.GetComponentsInChildren<Renderer>(true).Length > 0)
                        .OrderBy(child => child.name, StringComparer.Ordinal)
                        .ToList();
                    if (candidates.Count == 0) continue;
                    var result = Extract(candidates[0], $"{PropOutput}/{SafeStem(baseName)}.prefab",
                        PropMaterialOutput, PropMeshOutput, 0f, addCollider: false,
                        tweak: BindFoliageMaterial);
                    if (result.prefab == null) continue;
                    usedPrefabs.Add(AssetDatabase.GetAssetPath(result.prefab));
                    liveEntries.Add(new LiveCityCatalog.Entry
                    {
                        prefab = result.prefab,
                        kind = LiveCityCatalog.Kind.Tree,
                        bounds = result.bounds,
                        triangles = result.triangles
                    });
                    report.AppendLine($"- **{baseName}** — `{AssetDatabase.GetAssetPath(result.prefab)}`; " +
                                      $"{result.triangles} tris ({candidates.Count} identical variants, one imported)");
                }
            }
            finally { ReleaseSource(modelsScene); }
        }
        else
        {
            report.AppendLine($"- source missing: `{CityModelsSource}`");
        }
    }

    static void BuildPropsFromScene(string sourcePath, string prefix, LiveCityCatalog.Kind kind, string label,
        List<LiveCityCatalog.Entry> liveEntries, HashSet<string> usedPrefabs, StringBuilder report)
    {
        var scene = LoadSource(sourcePath);
        if (scene == null) return;
        try
        {
            var seen = new HashSet<string>(StringComparer.Ordinal);
            int found = 0;
            foreach (Transform child in scene.transform)
            {
                if (!child.name.StartsWith(prefix, StringComparison.Ordinal)) continue;
                if (child.GetComponentsInChildren<Renderer>(true).Length == 0) continue;
                found++;
                Bounds world = WorldBounds(child.gameObject);
                int triangles = TriangleCount(child.gameObject);
                if (!seen.Add(Fingerprint(world, triangles, child.gameObject))) continue;

                var result = Extract(child, $"{PropOutput}/{SafeStem(child.name)}.prefab",
                    PropMaterialOutput, PropMeshOutput, 0f, addCollider: false,
                    tweak: MaterialTweak(kind));
                if (result.prefab == null) continue;
                usedPrefabs.Add(AssetDatabase.GetAssetPath(result.prefab));
                liveEntries.Add(new LiveCityCatalog.Entry
                {
                    prefab = result.prefab,
                    kind = kind,
                    bounds = result.bounds,
                    triangles = result.triangles
                });
                report.AppendLine($"- **{child.name}** — `{AssetDatabase.GetAssetPath(result.prefab)}`; " +
                                  $"{result.bounds.size.y:F1} m tall, {result.triangles} tris, " +
                                  $"{result.renderersBefore}→{result.renderersAfter} renderers");
            }
            if (found == 0) report.AppendLine($"- no {label} found in `{Path.GetFileName(sourcePath)}`");
        }
        finally { ReleaseSource(scene); }
    }

    // ── Road signs ────────────────────────────────────────────────────────────

    static void BuildRoadSigns(List<LiveCityCatalog.Entry> liveEntries, HashSet<string> usedPrefabs, StringBuilder report)
    {
        report.AppendLine("\n## Road signs\n");
        var scene = LoadSource(RoadSignsSource);
        if (scene == null)
        {
            report.AppendLine($"- source missing: `{RoadSignsSource}`");
            return;
        }
        try
        {
            foreach (Transform child in scene.transform)
            {
                if (!child.name.StartsWith("traffic_sign", StringComparison.Ordinal)) continue;
                if (child.GetComponentsInChildren<Renderer>(true).Length == 0) continue;
                var result = Extract(child, $"{RoadSignOutput}/{SafeStem(child.name)}.prefab",
                    RoadSignMaterialOutput, RoadSignMeshOutput, 0f, addCollider: false);
                if (result.prefab == null) continue;
                usedPrefabs.Add(AssetDatabase.GetAssetPath(result.prefab));
                liveEntries.Add(new LiveCityCatalog.Entry
                {
                    prefab = result.prefab,
                    kind = LiveCityCatalog.Kind.RoadSign,
                    bounds = result.bounds,
                    triangles = result.triangles
                });
                report.AppendLine($"- **{child.name}** — `{AssetDatabase.GetAssetPath(result.prefab)}`; " +
                                  $"{result.bounds.size.y:F2} m tall, {result.triangles} tris");
            }
        }
        finally { ReleaseSource(scene); }
    }

    // ── Traffic light ─────────────────────────────────────────────────────────

    static void BuildTrafficLight(HashSet<string> usedPrefabs, StringBuilder report)
    {
        report.AppendLine("\n## Traffic-light housing\n");
        var source = LoadSource(TrafficLightSource);
        if (source == null)
        {
            report.AppendLine($"- source missing: `{TrafficLightSource}`");
            return;
        }

        var root = new GameObject("TrafficLightModel");
        try
        {
            var model = (GameObject)PrefabUtility.InstantiatePrefab(source);
            if (model == null)
            {
                report.AppendLine("- skipped: the FBX did not instantiate");
                return;
            }
            model.name = "Model";
            model.transform.SetParent(root.transform, false);
            StripNonVisualComponents(model);
            var counts = ImportedModelMeshCombiner.CombineByMaterial(model, TrafficLightMeshOutput, "TrafficLightModel");
            EnsureMaterials(model, TrafficLightMaterialOutput, "TrafficLightModel", null);
            Bounds bounds = LocalBounds(root);
            model.transform.localPosition -= new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);
            bounds = LocalBounds(root);
            int triangles = TriangleCount(root);
            if (SceneryLodPolicy.NeedsLod(triangles, SceneryLodPolicy.PropLodTriangleThreshold))
            {
                SceneryLodBuilder.Attach(root, bounds, SceneryLodPolicy.PropLod1ScreenHeight,
                    SceneryLodPolicy.PropLod2ScreenHeight, new Color(.25f, .26f, .27f), castShadows: false);
            }
            var prefab = PrefabUtility.SaveAsPrefabAsset(root, TrafficLightPrefab);
            if (prefab != null)
            {
                usedPrefabs.Add(TrafficLightPrefab);
                report.AppendLine($"- **TrafficLightModel** — `{TrafficLightPrefab}`; " +
                                  $"{bounds.size.x:F2}×{bounds.size.y:F2}×{bounds.size.z:F2} m, " +
                                  $"{triangles} tris, {counts.before}→{counts.after} renderers, LOD group added");
            }
        }
        finally { Object.DestroyImmediate(root); }
    }

    // ── Road modules ──────────────────────────────────────────────────────────

    static void BuildRoadModules(List<RoadModuleCatalog.Entry> roadModules, HashSet<string> usedPrefabs, StringBuilder report)
    {
        report.AppendLine("\n## Road modules\n");
        var scene = LoadSource(RoadLowPolySource);
        if (scene == null)
        {
            report.AppendLine($"- source missing: `{RoadLowPolySource}`");
            return;
        }
        try
        {
            foreach (Transform child in scene.transform)
            {
                if (child.GetComponentsInChildren<Renderer>(true).Length == 0) continue;
                var result = Extract(child, $"{RoadModuleOutput}/{SafeStem(child.name)}.prefab",
                    RoadModuleMaterialOutput, RoadModuleMeshOutput, 0f, addCollider: false,
                    tweak: BindRoadModuleMaterial);
                if (result.prefab == null) continue;
                usedPrefabs.Add(AssetDatabase.GetAssetPath(result.prefab));
                roadModules.Add(new RoadModuleCatalog.Entry
                {
                    prefab = result.prefab,
                    kind = ClassifyRoadModule(child.name),
                    bounds = result.bounds,
                    triangles = result.triangles,
                    sourceName = child.name
                });
                report.AppendLine($"- **{child.name}** — `{AssetDatabase.GetAssetPath(result.prefab)}`; " +
                                  $"{result.bounds.size.x:F2}×{result.bounds.size.z:F2} m, {result.triangles} tris, " +
                                  $"kind `{ClassifyRoadModule(child.name)}`");
            }
        }
        finally { ReleaseSource(scene); }
    }

    static RoadModuleCatalog.Kind ClassifyRoadModule(string name)
    {
        if (name == "Plane" || name == "Plane.008" || name == "Plane.015")
            return name == "Plane" ? RoadModuleCatalog.Kind.RoadTile : RoadModuleCatalog.Kind.RoadTileLong;
        if (name == "Plane.009" || name == "Plane.010" || name == "Plane.011" ||
            name == "Plane.012" || name == "Plane.013" || name == "Plane.014")
            return RoadModuleCatalog.Kind.Sidewalk;
        return RoadModuleCatalog.Kind.Kerb;
    }

    static Action<Material> MaterialTweak(LiveCityCatalog.Kind kind)
        => kind == LiveCityCatalog.Kind.Tree || kind == LiveCityCatalog.Kind.Plant
            ? BindFoliageMaterial
            : null;

    /// <summary>
    /// Leaf cards arrive as opaque quads whose texture is transparent between the
    /// fronds. Without a cutout the transparent pixels draw as black shards, which
    /// is how the first pass rendered the palms. This mirrors the settings the
    /// original city importer applies to imported trees.
    /// </summary>
    static void BindFoliageMaterial(Material material)
    {
        if (material == null) return;
        if (material.HasProperty("_Mode")) material.SetFloat("_Mode", 1f);
        if (material.HasProperty("_Cutoff")) material.SetFloat("_Cutoff", .42f);
        if (material.HasProperty("_Glossiness")) material.SetFloat("_Glossiness", .22f);
        if (material.HasProperty("_Metallic")) material.SetFloat("_Metallic", 0f);
        material.EnableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.SetOverrideTag("RenderType", "TransparentCutout");
        material.renderQueue = (int)RenderQueue.AlphaTest;
    }

    static void BindRoadModuleMaterial(Material material)
    {
        if (material == null) return;
        string name = (material.name ?? string.Empty).ToLowerInvariant();
        if (name.Contains("road"))
        {
            SetMap(material, RoadAlbedo, RoadNormal, Vector2.one);
            SetSurface(material, .18f, 0f);
        }
        else if (name.Contains("pavement") || name.Contains("edge"))
        {
            SetMap(material, SidewalkAlbedo, SidewalkNormal, Vector2.one);
            SetSurface(material, .22f, 0f);
        }
    }

    // ── Extraction ────────────────────────────────────────────────────────────

    struct Extracted
    {
        public GameObject prefab;
        public Bounds bounds;
        public int triangles;
        public int renderersBefore;
        public int renderersAfter;
    }

    static Extracted Extract(Transform child, string prefabPath, string materialFolder, string meshFolder,
        float targetHeight, bool addCollider, Action<Material> tweak = null)
    {
        var result = new Extracted();
        var root = new GameObject(child.name);
        try
        {
            var clone = Object.Instantiate(child.gameObject);
            clone.name = "Model";
            clone.SetActive(true);
            clone.transform.SetParent(root.transform, false);
            StripNonVisualComponents(clone);

            string stem = Path.GetFileNameWithoutExtension(prefabPath);
            var counts = ImportedModelMeshCombiner.CombineByMaterial(clone, meshFolder, stem);
            EnsureMaterials(clone, materialFolder, stem, tweak);

            Bounds bounds = LocalBounds(root);
            // Road tiles are deliberately flat, so only reject a model with no
            // extent at all rather than requiring height.
            if (bounds.size.sqrMagnitude < .0001f ||
                float.IsNaN(bounds.size.x) || float.IsNaN(bounds.size.y) || float.IsNaN(bounds.size.z) ||
                float.IsInfinity(bounds.size.x) || float.IsInfinity(bounds.size.y) || float.IsInfinity(bounds.size.z))
            {
                result.prefab = null;
                return result;
            }

            if (targetHeight > 0f)
                clone.transform.localScale *= targetHeight / bounds.size.y;

            bounds = LocalBounds(root);
            clone.transform.localPosition -= new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);
            bounds = LocalBounds(root);

            int triangles = TriangleCount(root);
            if (addCollider)
            {
                var collider = root.AddComponent<BoxCollider>();
                collider.center = bounds.center;
                collider.size = bounds.size;
                collider.isTrigger = false;
            }

            var prefab = PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
            result.prefab = prefab;
            result.bounds = bounds;
            result.triangles = triangles;
            result.renderersBefore = counts.before;
            result.renderersAfter = counts.after;
            return result;
        }
        finally
        {
            Object.DestroyImmediate(root);
        }
    }

    static void StripNonVisualComponents(GameObject visual)
    {
        foreach (var component in visual.GetComponentsInChildren<Camera>(true)) Object.DestroyImmediate(component);
        foreach (var component in visual.GetComponentsInChildren<AudioSource>(true)) Object.DestroyImmediate(component);
        foreach (var component in visual.GetComponentsInChildren<AudioListener>(true)) Object.DestroyImmediate(component);
        foreach (var component in visual.GetComponentsInChildren<Collider>(true)) Object.DestroyImmediate(component);
        foreach (var component in visual.GetComponentsInChildren<Rigidbody>(true)) Object.DestroyImmediate(component);
        foreach (var component in visual.GetComponentsInChildren<Light>(true)) Object.DestroyImmediate(component);
        foreach (var component in visual.GetComponentsInChildren<Animator>(true)) Object.DestroyImmediate(component);
        foreach (var component in visual.GetComponentsInChildren<Animation>(true)) Object.DestroyImmediate(component);
        foreach (var component in visual.GetComponentsInChildren<ParticleSystem>(true)) Object.DestroyImmediate(component);
        foreach (var component in visual.GetComponentsInChildren<Cloth>(true)) Object.DestroyImmediate(component);
    }

    static void EnsureMaterials(GameObject root, string materialFolder, string stem, Action<Material> tweak)
    {
        EnsureFolder(materialFolder);
        var converted = new Dictionary<Material, Material>();
        int index = 0;
        foreach (var renderer in root.GetComponentsInChildren<Renderer>(true))
        {
            Material[] materials = renderer.sharedMaterials;
            bool changed = false;
            for (int i = 0; i < materials.Length; i++)
            {
                Material original = materials[i];
                if (original == null) continue;
                if (converted.TryGetValue(original, out Material cached))
                {
                    materials[i] = cached;
                    changed = true;
                    continue;
                }

                var copy = new Material(original) { name = original.name };
                if (copy.shader == null || copy.shader.name.IndexOf("Autodesk", StringComparison.OrdinalIgnoreCase) >= 0)
                    copy.shader = BuiltInShaders.Lit;
                copy.enableInstancing = true;
                tweak?.Invoke(copy);

                string path = $"{materialFolder}/{stem}_{index++}.mat";
                if (AssetDatabase.LoadAssetAtPath<Material>(path) != null)
                    AssetDatabase.DeleteAsset(path);
                AssetDatabase.CreateAsset(copy, path);
                converted[original] = copy;
                materials[i] = copy;
                changed = true;
            }
            if (changed) renderer.sharedMaterials = materials;
        }
    }

    static void SetMap(Material material, string albedoPath, string normalPath, Vector2 scale)
    {
        var albedo = AssetDatabase.LoadAssetAtPath<Texture2D>(albedoPath);
        if (albedo != null)
        {
            material.SetTexture("_MainTex", albedo);
            material.SetTextureScale("_MainTex", scale);
            if (material.HasProperty("_Color")) material.SetColor("_Color", Color.white);
        }
        var normal = AssetDatabase.LoadAssetAtPath<Texture2D>(normalPath);
        if (normal != null)
        {
            material.SetTexture("_BumpMap", normal);
            material.EnableKeyword("_NORMALMAP");
        }
    }

    static void SetSurface(Material material, float smoothness, float metallic)
    {
        if (material.HasProperty("_Glossiness")) material.SetFloat("_Glossiness", smoothness);
        if (material.HasProperty("_Metallic")) material.SetFloat("_Metallic", metallic);
    }

    // ── Catalog merge ─────────────────────────────────────────────────────────

    static void MergeLiveCityCatalog(List<LiveCityCatalog.Entry> newEntries)
    {
        var catalog = AssetDatabase.LoadAssetAtPath<LiveCityCatalog>(LiveCityCatalogPath);
        if (catalog == null)
        {
            catalog = ScriptableObject.CreateInstance<LiveCityCatalog>();
            AssetDatabase.CreateAsset(catalog, LiveCityCatalogPath);
        }

        var entries = new List<LiveCityCatalog.Entry>();
        if (catalog.entries != null)
        {
            foreach (var entry in catalog.entries)
            {
                if (entry == null || entry.prefab == null) continue;
                string path = AssetDatabase.GetAssetPath(entry.prefab);
                if (IsUnder(path, CityOutput) || IsUnder(path, RoadSignOutput)) continue;
                entries.Add(entry);
            }
        }
        entries.AddRange(newEntries);
        entries.Sort((a, b) =>
        {
            int kind = a.kind.CompareTo(b.kind);
            return kind != 0 ? kind : string.CompareOrdinal(a.prefab.name, b.prefab.name);
        });
        catalog.entries = entries.ToArray();
        EditorUtility.SetDirty(catalog);
    }

    static void MergeBuildingCatalog(List<BuildingCatalog.Entry> newEntries)
    {
        var catalog = AssetDatabase.LoadAssetAtPath<BuildingCatalog>(BuildingCatalogPath);
        if (catalog == null)
        {
            catalog = ScriptableObject.CreateInstance<BuildingCatalog>();
            AssetDatabase.CreateAsset(catalog, BuildingCatalogPath);
        }

        var entries = new List<BuildingCatalog.Entry>();
        if (catalog.entries != null)
        {
            foreach (var entry in catalog.entries)
            {
                if (entry == null || entry.prefab == null) continue;
                if (IsUnder(AssetDatabase.GetAssetPath(entry.prefab), CityOutput)) continue;
                entries.Add(entry);
            }
        }
        entries.AddRange(newEntries);
        entries.Sort((a, b) => string.CompareOrdinal(a.sourceName, b.sourceName));
        catalog.entries = entries.ToArray();
        EditorUtility.SetDirty(catalog);
    }

    static void MergeRoadModuleCatalog(List<RoadModuleCatalog.Entry> newEntries)
    {
        var catalog = AssetDatabase.LoadAssetAtPath<RoadModuleCatalog>(RoadModuleCatalogPath);
        if (catalog == null)
        {
            catalog = ScriptableObject.CreateInstance<RoadModuleCatalog>();
            AssetDatabase.CreateAsset(catalog, RoadModuleCatalogPath);
        }
        newEntries.Sort((a, b) => string.CompareOrdinal(a.sourceName, b.sourceName));
        catalog.entries = newEntries.ToArray();
        EditorUtility.SetDirty(catalog);
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    /// <summary>
    /// Loaded FBX assets belong to the AssetDatabase and must never be destroyed;
    /// only objects this importer instantiated may be. Scene roots read here are
    /// assets, so this is a no-op for them.
    /// </summary>
    static void ReleaseSource(GameObject source)
    {
        if (source == null || AssetDatabase.Contains(source)) return;
        Object.DestroyImmediate(source);
    }

    static GameObject LoadSource(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning($"[NewModelPack] Missing source: {path}");
            return null;
        }
        var source = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (source == null)
            Debug.LogWarning($"[NewModelPack] Unity did not import '{path}' as a GameObject.");
        return source;
    }

    static Transform FindDeepChild(Transform root, string name)
    {
        foreach (Transform child in root.GetComponentsInChildren<Transform>(true))
            if (child.name == name) return child;
        return null;
    }

    static string StripNumericSuffix(string name)
    {
        int dot = name.LastIndexOf('.');
        if (dot <= 0 || dot >= name.Length - 1) return name;
        for (int i = dot + 1; i < name.Length; i++)
            if (!char.IsDigit(name[i])) return name;
        return name.Substring(0, dot);
    }

    static bool IsPlausibleBuilding(Bounds bounds, int triangles)
    {
        Vector3 size = bounds.size;
        if (size.y < 5f || size.y > 260f) return false;
        if (Mathf.Min(size.x, size.z) < 3f) return false;
        if (Mathf.Max(size.x, size.z) > 130f) return false;
        return triangles >= 300 && triangles <= 500000;
    }

    static string Fingerprint(Bounds bounds, int triangles, GameObject root)
    {
        var names = new List<string>();
        foreach (var renderer in root.GetComponentsInChildren<Renderer>(true))
            foreach (var material in renderer.sharedMaterials)
                names.Add(material != null ? material.name : "NULL");
        names.Sort(StringComparer.Ordinal);
        return $"{bounds.size.x:F1}|{bounds.size.y:F1}|{bounds.size.z:F1}|{triangles}|{string.Join(",", names)}";
    }

    static Bounds WorldBounds(GameObject root)
    {
        var renderers = root.GetComponentsInChildren<Renderer>(true);
        if (renderers.Length == 0) return new Bounds();
        Bounds bounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++) bounds.Encapsulate(renderers[i].bounds);
        return bounds;
    }

    static Bounds LocalBounds(GameObject root)
    {
        var renderers = root.GetComponentsInChildren<Renderer>(true);
        Bounds bounds = new Bounds();
        bool has = false;
        foreach (var renderer in renderers)
        {
            if (renderer == null) continue;
            Bounds world = renderer.bounds;
            Vector3 center = world.center;
            Vector3 extents = world.extents;
            for (int corner = 0; corner < 8; corner++)
            {
                Vector3 point = center + new Vector3(
                    (corner & 1) == 0 ? -extents.x : extents.x,
                    (corner & 2) == 0 ? -extents.y : extents.y,
                    (corner & 4) == 0 ? -extents.z : extents.z);
                Vector3 local = root.transform.InverseTransformPoint(point);
                if (!has) { bounds = new Bounds(local, Vector3.zero); has = true; }
                else bounds.Encapsulate(local);
            }
        }
        return bounds;
    }

    static int TriangleCount(GameObject root)
    {
        int total = 0;
        foreach (var filter in root.GetComponentsInChildren<MeshFilter>(true))
        {
            Mesh mesh = filter.sharedMesh;
            if (mesh == null) continue;
            for (int i = 0; i < mesh.subMeshCount; i++)
                total += (int)mesh.GetIndexCount(i) / 3;
        }
        return total;
    }

    static string SafeStem(string name)
    {
        string safe = string.Concat((name ?? string.Empty).Select(c => char.IsLetterOrDigit(c) ? c : '_')).Trim('_');
        return string.IsNullOrEmpty(safe) ? "Model" : safe;
    }

    static bool IsUnder(string path, string folder)
        => !string.IsNullOrEmpty(path) && path.StartsWith(folder + "/", StringComparison.OrdinalIgnoreCase);

    static void EnsureNormalMap(string path)
    {
        var importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if (importer == null || importer.textureType == TextureImporterType.NormalMap) return;
        importer.textureType = TextureImporterType.NormalMap;
        importer.SaveAndReimport();
    }

    static void EnsureFolders()
    {
        EnsureFolder(BuildingOutput);
        EnsureFolder(BuildingMaterialOutput);
        EnsureFolder(BuildingMeshOutput);
        EnsureFolder(PropOutput);
        EnsureFolder(PropMaterialOutput);
        EnsureFolder(PropMeshOutput);
        EnsureFolder(RoadSignOutput);
        EnsureFolder(RoadSignMaterialOutput);
        EnsureFolder(RoadSignMeshOutput);
        EnsureFolder(TrafficLightOutput);
        EnsureFolder(TrafficLightMaterialOutput);
        EnsureFolder(TrafficLightMeshOutput);
        EnsureFolder(RoadModuleOutput);
        EnsureFolder(RoadModuleMaterialOutput);
        EnsureFolder(RoadModuleMeshOutput);
    }

    static void EnsureFolder(string folder)
    {
        folder = folder.Replace('\\', '/');
        if (AssetDatabase.IsValidFolder(folder)) return;
        string parent = Path.GetDirectoryName(folder)?.Replace('\\', '/');
        if (!string.IsNullOrEmpty(parent)) EnsureFolder(parent);
        AssetDatabase.CreateFolder(parent, Path.GetFileName(folder));
    }

    static void DeleteStalePrefabs(string folder, HashSet<string> used)
    {
        if (!Directory.Exists(folder)) return;
        foreach (string file in Directory.GetFiles(folder, "*.prefab"))
        {
            string path = file.Replace('\\', '/');
            if (!used.Contains(path)) AssetDatabase.DeleteAsset(path);
        }
    }
}
#endif
