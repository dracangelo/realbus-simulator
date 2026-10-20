using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Covers the content rules behind the city's look: how a downloaded vehicle model
/// is classified, normalised and weighted in the traffic pool; how varied a
/// building's facade and colour are; and how deep the roadside infill blocks are
/// supposed to reach.
/// </summary>
public class CityContentTests
{
    [TestCase("Sedan1", AIVehicleController.VehicleType.Car)]
    [TestCase("Car2", AIVehicleController.VehicleType.Car)]
    [TestCase("SportCar2", AIVehicleController.VehicleType.Car)]
    [TestCase("Jeep2", AIVehicleController.VehicleType.Car)]
    [TestCase("MicroBus4", AIVehicleController.VehicleType.Bus)]
    [TestCase("Van", AIVehicleController.VehicleType.Truck)]
    [TestCase("toyota hilux 002", AIVehicleController.VehicleType.Car)]
    [TestCase("jeep grand cherokee 001", AIVehicleController.VehicleType.Car)]
    [TestCase("Land rover defender 001", AIVehicleController.VehicleType.Car)]
    [TestCase("Lamborghini+Revuelto", AIVehicleController.VehicleType.Car)]
    [TestCase("bugatti", AIVehicleController.VehicleType.Car)]
    [TestCase("Nissan_GTR_nismo_2022", AIVehicleController.VehicleType.Car)]
    [TestCase("dodge_HP", AIVehicleController.VehicleType.Car)]
    [TestCase("uploads_files_5533688_pickup test2", AIVehicleController.VehicleType.Truck)]
    [TestCase("Matatu 14", AIVehicleController.VehicleType.Bus)]
    [TestCase("uploads_files_4560086_FireEngine", AIVehicleController.VehicleType.Emergency)]
    [TestCase("uploads_files_6375847_ambulance", AIVehicleController.VehicleType.Emergency)]
    [TestCase("uploads_files_901612_bike", AIVehicleController.VehicleType.Motorcycle)]
    [TestCase("", AIVehicleController.VehicleType.Car)]
    public void TrafficModels_NamesClassifyIntoTheRightTrafficType(string name, AIVehicleController.VehicleType expected)
        => Assert.That(TrafficModelRules.ClassifyModel(name), Is.EqualTo(expected));

    [Test]
    public void TrafficModels_EachClassHasAUsableRoadLength()
    {
        foreach (AIVehicleController.VehicleType type in System.Enum.GetValues(typeof(AIVehicleController.VehicleType)))
        {
            float length = TrafficModelRules.TargetLengthMeters(type);
            Assert.That(length, Is.InRange(2f, 9f), $"{type} needs a believable length");
        }

        Assert.That(TrafficModelRules.TargetLengthMeters(AIVehicleController.VehicleType.Motorcycle),
            Is.LessThan(TrafficModelRules.TargetLengthMeters(AIVehicleController.VehicleType.Car)));
        Assert.That(TrafficModelRules.TargetLengthMeters(AIVehicleController.VehicleType.Car),
            Is.LessThan(TrafficModelRules.TargetLengthMeters(AIVehicleController.VehicleType.Bus)));
    }

    [Test]
    public void TrafficModels_HeavyDownloadsLosePoolShareWithoutBeingExcluded()
    {
        float light = TrafficModelRules.PoolWeight(AIVehicleController.VehicleType.Car, 3000);
        float heavy = TrafficModelRules.PoolWeight(AIVehicleController.VehicleType.Car, 90000);

        Assert.That(light, Is.GreaterThan(heavy), "a dense model must not take the same share as a cheap one");
        Assert.That(heavy, Is.GreaterThan(0f), "a dense model still appears, just rarely");
        Assert.That(TrafficModelRules.PoolWeight(AIVehicleController.VehicleType.Emergency, 3000),
            Is.LessThan(light), "emergency vehicles stay rare whatever they cost");
        Assert.That(TrafficModelRules.PoolWeight(AIVehicleController.VehicleType.Car, 0),
            Is.LessThanOrEqualTo(1f), "no weight may exceed the pool's range");
    }

    [Test]
    public void TrafficModels_LongAxisIsTurnedOntoForward()
    {
        Assert.That(TrafficModelRules.NeedsQuarterTurn(new Vector3(4.6f, 1.6f, 1.9f)), Is.True,
            "a model longer across X would drive sideways");
        Assert.That(TrafficModelRules.NeedsQuarterTurn(new Vector3(1.9f, 1.6f, 4.6f)), Is.False);
        Assert.That(TrafficModelRules.LengthOf(new Vector3(4.6f, 1.6f, 1.9f)), Is.EqualTo(4.6f).Within(0.001f));
        Assert.That(TrafficModelRules.LengthOf(new Vector3(1.9f, 1.6f, 4.6f)), Is.EqualTo(4.6f).Within(0.001f));
    }

    [Test]
    public void TrafficModels_FacingIsReadFromTheNamedFrontAndRearParts()
    {
        Assert.That(TrafficModelRules.FacingSign(true, 2.1f, true, -2.1f, 0f), Is.EqualTo(1),
            "headlights in front of the tail means the model already faces forward");
        Assert.That(TrafficModelRules.FacingSign(true, -2.1f, true, 2.1f, 0f), Is.EqualTo(-1),
            "a model authored nose-first along -Z would drive backwards");
        Assert.That(TrafficModelRules.FacingSign(true, 1.8f, false, 0f, 0f), Is.EqualTo(1),
            "a single named end still says which side the nose is on");
        Assert.That(TrafficModelRules.FacingSign(false, 0f, true, -1.8f, 0f), Is.EqualTo(1));
        Assert.That(TrafficModelRules.FacingSign(false, 0f, false, 0f, 0f), Is.Zero,
            "with no named parts the model is left exactly as authored");
        Assert.That(TrafficModelRules.FacingSign(true, 0f, true, 0f, 0f), Is.Zero);
    }

    [TestCase("Headlights", true)]
    [TestCase("Front ", true)]
    [TestCase("front_bumper", true)]
    [TestCase("head light 1", true)]
    [TestCase("Rear", false)]
    [TestCase("TailLight", false)]
    [TestCase("FrontAxisSignS", false, Description = "an armature bone is not a nose")]
    [TestCase("door_fl", false)]
    [TestCase("Glass", false)]
    public void TrafficModels_OnlyVehicleEndsCountAsFront(string partName, bool expected)
        => Assert.That(TrafficModelRules.IsFrontPart(partName), Is.EqualTo(expected));

    [TestCase("Rear ", true)]
    [TestCase("back", true)]
    [TestCase("TailLight", true)]
    [TestCase("Front", false)]
    [TestCase("door_br", false)]
    public void TrafficModels_OnlyVehicleEndsCountAsRear(string partName, bool expected)
        => Assert.That(TrafficModelRules.IsRearPart(partName), Is.EqualTo(expected));

    [TestCase("Headlights", 1f)]
    [TestCase("TailLight", -1f)]
    [TestCase("steering_wheel", .5f)]
    [TestCase("windscreen", .5f)]
    [TestCase("Boot", -.5f)]
    [TestCase("FL", .25f, Description = "front-left wheel of the downloaded pack")]
    [TestCase("door_br", -.25f)]
    [TestCase("Glass", 0f)]
    [TestCase("FrontAxisSignS", 0f, Description = "an armature bone is not a nose")]
    [TestCase("", 0f)]
    public void TrafficModels_PartNamesCarryWeightedEvidenceOfWhichEndTheyBelongTo(string partName, float expected)
        => Assert.That(TrafficModelRules.NoseEvidence(partName), Is.EqualTo(expected));

    [Test]
    public void TrafficModels_TokensMatchWholeWordsOnly()
    {
        Assert.That(TrafficModelRules.HasToken("door_fl", "fl"), Is.True);
        Assert.That(TrafficModelRules.HasToken("FL", "fl"), Is.True, "a name is matched case-insensitively");
        Assert.That(TrafficModelRules.HasToken("FrontAxisSignS", "front"), Is.False, "'front' inside a longer word is not an end");
        Assert.That(TrafficModelRules.HasToken("front_bumper", "front"), Is.True);
    }

    [Test]
    public void TrafficModels_ADownloadedNosesPartsDecideTheDirectionItDrives()
    {
        // Sedan1 as the pack authors it: front wheels, front doors and the steering wheel
        // all sit ahead of the model's centre, rear wheels and rear doors behind it.
        float votes = TrafficModelRules.FacingVote(TrafficModelRules.NoseEvidence("FL"), 1.59f, 0f, .18f)
            + TrafficModelRules.FacingVote(TrafficModelRules.NoseEvidence("door_fr"), .79f, 0f, .18f)
            + TrafficModelRules.FacingVote(TrafficModelRules.NoseEvidence("steering_wheel"), .22f, 0f, .18f)
            + TrafficModelRules.FacingVote(TrafficModelRules.NoseEvidence("BL"), -1.95f, 0f, .18f)
            + TrafficModelRules.FacingVote(TrafficModelRules.NoseEvidence("door_br"), -.66f, 0f, .18f);
        Assert.That(TrafficModelRules.FacingFromVotes(votes), Is.EqualTo(1), "that model already faces forward");

        // The same model authored nose along -Z has to be turned.
        Assert.That(TrafficModelRules.FacingFromVotes(-votes), Is.EqualTo(-1));
    }

    [Test]
    public void TrafficModels_AWeakOrCentredNameCannotDecideOnItsOwn()
    {
        Assert.That(TrafficModelRules.FacingFromVotes(TrafficModelRules.NoseEvidence("FL")), Is.Zero,
            "one wheel token is not enough to spin a whole model around");
        Assert.That(TrafficModelRules.FacingFromVotes(TrafficModelRules.NoseEvidence("Headlights")), Is.EqualTo(1),
            "a headlight is");
        Assert.That(TrafficModelRules.FacingFromVotes(0f), Is.Zero);
        Assert.That(TrafficModelRules.FacingVote(1f, .05f, 0f, .18f), Is.Zero,
            "a part on the model's centre line says nothing about which way it faces");
        Assert.That(TrafficModelRules.FacingVote(1f, .5f, 0f, .18f), Is.EqualTo(1f));
    }

    [Test]
    public void TrafficCatalog_CountsOnlyEntriesThatCanBeSpawned()
    {
        var catalog = ScriptableObject.CreateInstance<TrafficCatalog>();
        try
        {
            var empty = new GameObject("EmptyTrafficModel");
            try
            {
                catalog.entries = new[]
                {
                    new TrafficCatalog.Entry { displayName = "Sedan1", vehicleType = AIVehicleController.VehicleType.Car },
                    new TrafficCatalog.Entry { displayName = "MicroBus4", vehicleType = AIVehicleController.VehicleType.Bus, prefab = empty },
                    null
                };
                Assert.That(catalog.UsableCount, Is.EqualTo(1), "an entry without a prefab cannot be spawned");
            }
            finally { Object.DestroyImmediate(empty); }
        }
        finally { Object.DestroyImmediate(catalog); }
    }

    [Test]
    public void Buildings_CarryOneFacadeTilePerFloorAndStopStretching()
    {
        Assert.That(OSMBuildingMeshBuilder.FacadeTilesFor(6f, 3f), Is.EqualTo(2));
        Assert.That(OSMBuildingMeshBuilder.FacadeTilesFor(30f, 3f), Is.EqualTo(10));
        Assert.That(OSMBuildingMeshBuilder.FacadeTilesFor(0.4f, 3f), Is.EqualTo(1), "no building is tiled less than once");
        Assert.That(OSMBuildingMeshBuilder.FacadeTilesFor(300f, 3f), Is.EqualTo(16), "tiling is capped so the pattern does not alias at distance");
    }

    [Test]
    public void Buildings_AreTintedPerBuildingWithoutLeavingTheirPalette()
    {
        Color baseColour = new Color(0.72f, 0.68f, 0.62f, 1f);
        Color first = OSMBuildingMeshBuilder.TintFor(baseColour, 1234);
        Color again = OSMBuildingMeshBuilder.TintFor(baseColour, 1234);
        Color other = OSMBuildingMeshBuilder.TintFor(baseColour, 987654321);

        Assert.That(first, Is.EqualTo(again), "a building's colour must be stable between loads");
        Assert.That(Vector4.Distance(first, other), Is.GreaterThan(0.02f), "different buildings have to look different");
        Assert.That(first.a, Is.EqualTo(1f));
        foreach (var tint in new[] { first, other })
        {
            Assert.That(tint.r, Is.InRange(0.4f, 1f), "a tint must stay in the material's own range");
            Assert.That(tint.g, Is.InRange(0.4f, 1f));
            Assert.That(tint.b, Is.InRange(0.4f, 1f));
            Assert.That(Mathf.Abs(tint.r - tint.b), Is.LessThan(0.25f), "tinting must not recolour a building outright");
        }
    }

    [Test]
    public void InfillBuildings_HaveVariedSizesHeightsAndPalettes()
    {
        var kinds = new System.Collections.Generic.HashSet<OSMBuildingMeshBuilder.BuildingKind>();
        var heights = new System.Collections.Generic.HashSet<int>();
        float minWidth = float.MaxValue, maxWidth = 0f, minDepth = float.MaxValue, maxDepth = 0f;

        for (uint hash = 1; hash < 400; hash++)
        {
            float width = ProceduralBuildingFill.InfillWidth(hash);
            float depth = ProceduralBuildingFill.InfillDepth(hash);
            var kind = ProceduralBuildingFill.InfillKind(hash);
            float height = ProceduralBuildingFill.InfillHeight(hash, kind);

            Assert.That(width, Is.InRange(9f, 17f));
            Assert.That(depth, Is.InRange(9f, 21f));
            Assert.That(height, Is.GreaterThanOrEqualTo(6f), "no infill building is a shed");
            Assert.That(height, Is.LessThanOrEqualTo(31f));

            kinds.Add(kind);
            heights.Add(Mathf.RoundToInt(height));
            minWidth = Mathf.Min(minWidth, width); maxWidth = Mathf.Max(maxWidth, width);
            minDepth = Mathf.Min(minDepth, depth); maxDepth = Mathf.Max(maxDepth, depth);
        }

        Assert.That(kinds, Does.Contain(OSMBuildingMeshBuilder.BuildingKind.House));
        Assert.That(kinds, Does.Contain(OSMBuildingMeshBuilder.BuildingKind.Commercial));
        Assert.That(kinds, Does.Contain(OSMBuildingMeshBuilder.BuildingKind.Generic));
        Assert.That(heights.Count, Is.GreaterThan(6), "storey counts have to vary, not repeat one value");
        Assert.That(maxWidth - minWidth, Is.GreaterThan(4f));
        Assert.That(maxDepth - minDepth, Is.GreaterThan(8f));
    }

    [Test]
    public void InfillBuildings_HugTheRoadAndReachIntoTheBlock()
    {
        var fill = new GameObject("InfillShapeTest").AddComponent<ProceduralBuildingFill>();
        try
        {
            Assert.That(fill.kerbGap, Is.LessThanOrEqualTo(3f), "the first row belongs beside the road, not behind a yard");
            Assert.That(fill.rowsPerSide, Is.GreaterThanOrEqualTo(2), "one row leaves the middle of every block empty");
            Assert.That(fill.BlockCoverageMeters(), Is.GreaterThanOrEqualTo(30f),
                "the rows have to reach far enough back to fill the block rather than line the kerb");
            Assert.That(fill.minBuildingSpacing, Is.LessThanOrEqualTo(12f), "spacing wider than the buildings leaves gaps");
            // A gap wider than the narrowest building is a hole in the frontage.
            Assert.That(fill.frontGap, Is.LessThan(ProceduralBuildingFill.InfillWidth(1)),
                "neighbours along a street have to nearly touch, not stand apart");
            Assert.That(fill.rowsPerSide, Is.GreaterThanOrEqualTo(4),
                "blocks are deeper than two rows, and filling stops at the next street");
            Assert.That(fill.maxBuildingsPerFrame, Is.GreaterThan(1),
                "raising one building per frame leaves a district looking half-built for seconds");
            Assert.That(fill.millisecondsPerFrame, Is.GreaterThan(0f), "the fill needs a per-frame budget");
            Assert.That(fill.maxBuildings, Is.GreaterThanOrEqualTo(800),
                "a budget that runs out mid-block is what leaves a district half empty");
        }
        finally { Object.DestroyImmediate(fill.gameObject); }
    }

    [Test]
    public void InfillBuildings_ACrossStreetEndsTheBlockButAParallelOneOnlySkipsTheSpot()
    {
        Assert.That(ProceduralBuildingFill.EndsBlock(Vector3.forward, new Vector3(-30f, 0f, 0f), new Vector3(30f, 0f, 0f)),
            Is.True, "a street crossing the row ends the block behind it");
        Assert.That(ProceduralBuildingFill.EndsBlock(Vector3.forward, new Vector3(0f, 0f, -30f), new Vector3(0f, 0f, 30f)),
            Is.False, "the row's own street only rules out the one spot being tested");
        Assert.That(ProceduralBuildingFill.EndsBlock(Vector3.forward, Vector3.zero, Vector3.zero),
            Is.True, "a degenerate segment has no direction to be parallel to");
    }

    [Test]
    public void InfillBuildings_ClearanceIsMeasuredAgainstTheFootprintNotTheCentre()
    {
        // A building sitting exactly on the kerb line of its own street must be accepted:
        // measuring from the centre to the road threw every candidate away, which is why
        // the blocks used to stay empty.
        Vector3 along = Vector3.forward;
        float width = 12f, depth = 15f;
        // Put the footprint's near edge 3.5 m from the road. The centre is then
        // 11 m away, which is the mistake the old centre-based test made.
        Vector3 center = new Vector3(15f, 0f, 0f);
        Vector3 roadA = new Vector3(4f, 0f, -60f), roadB = new Vector3(4f, 0f, 60f);
        float clear = ProceduralBuildingFill.RectangleDistanceToSegment(center, along, width * .5f, depth * .5f, roadA, roadB);
        Assert.That(clear, Is.EqualTo(center.x - depth * .5f - roadA.x).Within(0.01f),
            "clearance is measured from the footprint edge, not the centre");

        // The same footprint straddling the road is zero, however far its centre is.
        Vector3 straddling = new Vector3(0f, 0f, 0f);
        Assert.That(ProceduralBuildingFill.RectangleDistanceToSegment(straddling, along, width * .5f, depth * .5f, roadA, roadB), Is.Zero);

        // And a segment passing through a long footprint is caught although both its ends
        // are well outside it.
        Vector3 longBuilding = new Vector3(40f, 0f, 0f);
        Assert.That(ProceduralBuildingFill.RectangleDistanceToSegment(longBuilding, Vector3.forward, 6f, 20f,
            new Vector3(40f, 0f, -12f), new Vector3(40f, 0f, 12f)), Is.Zero);
    }

    [Test]
    public void InfillBuildings_EveryKindCarriesARoofAndAPaletteOfItsOwn()
    {
        var shapes = new System.Collections.Generic.HashSet<OSMBuildingMeshBuilder.RoofShape>();
        var colours = new System.Collections.Generic.HashSet<Color>();
        int hips = 0, total = 0;
        for (long id = 1; id < 300; id++)
        {
            shapes.Add(OSMBuildingMeshBuilder.RoofShapeFor(OSMBuildingMeshBuilder.BuildingKind.House, id));
            hips += OSMBuildingMeshBuilder.RoofShapeFor(OSMBuildingMeshBuilder.BuildingKind.House, id) == OSMBuildingMeshBuilder.RoofShape.Hip ? 1 : 0;
            colours.Add(OSMBuildingMeshBuilder.PaletteFor(OSMBuildingMeshBuilder.BuildingKind.House, id));
            total++;
        }

        Assert.That(shapes.Count, Is.EqualTo(2), "a street needs both flat-capped and pitched roofs");
        Assert.That(hips, Is.GreaterThan(total / 3), "most houses should have a pitched roof");
        Assert.That(colours.Count, Is.GreaterThan(1), "one colour per kind is what made a district look like one building");
        Assert.That(OSMBuildingMeshBuilder.PaletteFor(OSMBuildingMeshBuilder.BuildingKind.School, 7),
            Is.EqualTo(OSMBuildingMeshBuilder.PaletteFor(OSMBuildingMeshBuilder.BuildingKind.School, 8)),
            "a one-colour kind stays that colour");
    }

    [Test]
    public void LiveGreenery_IsLargerOrSmallerPerInstanceButNeverAbsurd()
    {
        var sizes = new System.Collections.Generic.HashSet<float>();
        for (uint seed = 0; seed < 200; seed++)
        {
            float tree = LiveCityStreamer.ScaleFor(seed, LiveCityCatalog.Kind.Tree);
            sizes.Add(tree);
            Assert.That(tree, Is.InRange(0.7f, 1.4f), "a tree outside this range stops looking like the model");
            Assert.That(LiveCityStreamer.ScaleFor(seed, LiveCityCatalog.Kind.Pedestrian), Is.EqualTo(1f),
                "people are not scaled: a short walker is a child, a tall one is a bug");
        }
        Assert.That(sizes.Count, Is.GreaterThan(10), "greenery has to vary in size or the pack's few models repeat");
    }

    [Test]
    public void LiveGreenery_CheapModelsCarryThePlantingAndDenseOnesStillAppear()
    {
        var model = new GameObject("GreeneryWeightTest");
        try
        {
            var entries = new System.Collections.Generic.List<LiveCityCatalog.Entry>
            {
                new LiveCityCatalog.Entry { kind = LiveCityCatalog.Kind.Tree, prefab = model, triangles = 5000 },
                new LiveCityCatalog.Entry { kind = LiveCityCatalog.Kind.Tree, prefab = model, triangles = 55000 }
            };

            int cheap = 0, dense = 0;
            for (uint seed = 0; seed < 4000; seed++)
            {
                var picked = LiveCityStreamer.PickWeighed(entries, LiveCityCatalog.Kind.Tree, seed);
                Assert.That(picked, Is.Not.Null, "a kind that has models must always resolve");
                if (picked.triangles == 5000) cheap++; else dense++;
            }

            Assert.That(cheap, Is.GreaterThan(dense * 4),
                "planting three 40 000-triangle trees as often as a 5 000-triangle one spends the whole budget");
            Assert.That(dense, Is.GreaterThan(0), "a dense model still appears, it just does not decide the budget");
            Assert.That(LiveCityStreamer.ModelWeight(5000), Is.GreaterThan(LiveCityStreamer.ModelWeight(55000)));
            Assert.That(LiveCityStreamer.ModelWeight(55000), Is.GreaterThan(0f));
            Assert.That(LiveCityStreamer.PickWeighed(entries, LiveCityCatalog.Kind.Bench, 3), Is.Null,
                "a kind with no models plants nothing");
        }
        finally { Object.DestroyImmediate(model); }
    }

    [Test]
    public void LiveGreenery_OpenGroundSpotsAreStableAndSpreadOut()
    {
        var seen = new System.Collections.Generic.HashSet<string>();
        int planted = 0;
        for (uint slot = 0; slot < 500; slot++)
        {
            Vector2 offset = LiveCityStreamer.ScatterOffset(slot);
            Assert.That(offset.x, Is.InRange(0f, 1f));
            Assert.That(offset.y, Is.InRange(0f, 1f));
            Assert.That(LiveCityStreamer.ScatterOffset(slot), Is.EqualTo(offset), "a spot must not move between refreshes");
            seen.Add($"{offset.x:0.00},{offset.y:0.00}");
            if (LiveCityStreamer.ScatterPlants(slot, .5f)) planted++;
        }

        Assert.That(seen.Count, Is.GreaterThan(400), "every spot landing on the same point would plant one clump");
        Assert.That(planted, Is.InRange(120, 380), "about half the offered spots are planted");
    }
}
