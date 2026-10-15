using System;
using UnityEngine;

/// <summary>
/// How a downloaded vehicle model becomes AI traffic: which class its name implies,
/// how long it has to be in metres, how much of the pool it may take, and how a
/// model that is longer across than along has to be turned.
///
/// These rules are pure and shared, so the editor importer that builds the traffic
/// prefabs and the runtime pool that spawns them cannot disagree, and so the rules
/// can be tested without importing anything. They exist because traffic used to
/// fall back to four procedural boxes whenever a pack was not pre-wired: the
/// downloaded models were sitting in .zip files nothing ever opened.
/// </summary>
public static class TrafficModelRules
{
    /// <summary>Triangles at which a model still takes its full share of the pool.</summary>
    public const int FullWeightTriangleBudget = 9000;

    /// <summary>Length in metres a class of traffic vehicle is scaled to.</summary>
    public static float TargetLengthMeters(AIVehicleController.VehicleType type)
    {
        switch (type)
        {
            case AIVehicleController.VehicleType.Motorcycle: return 2.1f;
            case AIVehicleController.VehicleType.Truck:      return 6.4f;
            case AIVehicleController.VehicleType.Bus:        return 8.5f;
            case AIVehicleController.VehicleType.Emergency:  return 6.2f;
            default:                                         return 4.5f;
        }
    }

    /// <summary>
    /// The class a model name implies, matched on the words vehicle packs actually
    /// use in their file names.
    /// </summary>
    public static AIVehicleController.VehicleType ClassifyModel(string modelName)
    {
        string name = string.IsNullOrWhiteSpace(modelName) ? string.Empty : modelName.ToLowerInvariant();
        if (Contains(name, "motorcycle", "motorbike", "bike", "scooter", "moped", "boda")) return AIVehicleController.VehicleType.Motorcycle;
        if (Contains(name, "ambulance", "fire", "police", "rescue", "paramedic")) return AIVehicleController.VehicleType.Emergency;
        if (Contains(name, "microbus", "minibus", "matatu", "coach", "bus", "shuttle")) return AIVehicleController.VehicleType.Bus;
        if (Contains(name, "van", "pickup", "truck", "lorry", "scania", "cargo", "lorry", "transit")) return AIVehicleController.VehicleType.Truck;
        return AIVehicleController.VehicleType.Car;
    }

    /// <summary>
    /// Share of the traffic pool a model asks for. Dense downloads are down-weighted
    /// rather than excluded, so a heavy model still appears in the city without
    /// deciding the frame budget on its own.
    /// </summary>
    public static float PoolWeight(AIVehicleController.VehicleType type, int triangles)
    {
        float classWeight;
        switch (type)
        {
            case AIVehicleController.VehicleType.Truck:      classWeight = 0.55f; break;
            case AIVehicleController.VehicleType.Bus:        classWeight = 0.30f; break;
            case AIVehicleController.VehicleType.Motorcycle: classWeight = 0.30f; break;
            case AIVehicleController.VehicleType.Emergency:  classWeight = 0.07f; break;
            default:                                        classWeight = 1.00f; break;
        }

        float detail = Mathf.Clamp(FullWeightTriangleBudget / (float)Mathf.Max(1, triangles), 0.05f, 1f);
        return Mathf.Clamp(classWeight * detail, 0.02f, 1f);
    }

    /// <summary>
    /// True for parts that are inside a downloaded model and must never reach
    /// traffic: seats, dashboards, steering and interior trim. The Van download
    /// spends most of its 696 000 triangles on an interior nobody sees from the
    /// street, and sixty of those at once would decide the frame budget.
    /// </summary>
    public static bool IsInteriorPart(string partName)
    {
        if (string.IsNullOrWhiteSpace(partName)) return false;
        string name = partName.ToLowerInvariant();
        return Contains(name, "interior", "interier", "interijer", "seat", "dashboard", "torpedo",
            "steering", "salon", "headrest", "armrest", "glovebox", "cabin interior");
    }

    /// <summary>True when the model's long axis runs across X, so it has to be turned onto Z.</summary>
    public static bool NeedsQuarterTurn(Vector3 size) => Mathf.Abs(size.x) > Mathf.Abs(size.z);

    /// <summary>
    /// Which way a model faces along Z, judged from its named front and rear parts:
    /// +1 already forward, -1 reversed, 0 unknown. Nothing in an FBX records which end
    /// is the nose, so a model imported as authored can drive backwards through the
    /// city; the downloaded packs do name their front and rear pieces, and their
    /// positions along Z answer it. Pure and shared so this can be tested without
    /// importing a vehicle.
    /// </summary>
    public static int FacingSign(bool hasFront, float frontZ, bool hasRear, float rearZ, float centerZ)
    {
        if (hasFront && hasRear)
        {
            if (Mathf.Abs(frontZ - rearZ) < .01f) return 0;
            return frontZ > rearZ ? 1 : -1;
        }
        // Only one named end: it still says which side of the body the nose is on.
        if (hasFront) return frontZ > centerZ + .01f ? 1 : frontZ < centerZ - .01f ? -1 : 0;
        if (hasRear) return rearZ < centerZ - .01f ? 1 : rearZ > centerZ + .01f ? -1 : 0;
        return 0;
    }

    /// <summary>
    /// Signed evidence that a part name says "this belongs at the nose", weighted by how
    /// certain the name is: 1 for a part that is only ever at the front (a named end, a
    /// headlight), .5 for one that is at the front of every real vehicle (windscreen,
    /// bonnet, the steering wheel), -.5 and -1 for their counterparts at the tail, and
    /// .25 for the weak tokens a downloaded pack puts on its wheels and doors - 'FL' and
    /// 'door_fr' really do sit in the front half, but a part named 'BL' only says which
    /// half it is in. Nothing in an FBX says which end is the nose, so this evidence is
    /// what lets the importer face a model correctly instead of leaving it to chance.
    /// </summary>
    public static float NoseEvidence(string partName)
    {
        string name = Normalise(partName);
        if (name.Length == 0) return 0f;
        if (IsFrontPart(partName)) return 1f;
        if (IsRearPart(partName)) return -1f;

        if (Contains(name, "windscreen", "windshield", "bonnet", "hood", "grille", "grill",
                "radiator", "steering", "dashboard", "dash", "cockpit", "numberplate", "licenseplate")) return .5f;
        if (Contains(name, "trunk", "boot", "tailgate", "exhaust", "muffler", "backwindow", "rearwindow")) return -.5f;

        // Wheel and door tokens: 'FL', 'wheel_fr', 'door_fl' ahead, 'BL', 'door_br' behind.
        if (HasToken(name, "fl") || HasToken(name, "fr") || HasToken(name, "front")) return .25f;
        if (HasToken(name, "bl") || HasToken(name, "br") || HasToken(name, "back") || HasToken(name, "rear")) return -.25f;
        return 0f;
    }

    /// <summary>
    /// How a part's position along Z votes on the model's facing: a nose part ahead of the
    /// model's centre votes +1 (the nose is at +Z), the same part behind the centre votes
    /// -1, and a part sitting on the centre line - inside the dead-band - does not vote at
    /// all, so a part that spans the whole model cannot decide anything on its own.
    /// </summary>
    public static float FacingVote(float noseEvidence, float partZ, float centerZ, float deadband)
        => Mathf.Abs(partZ - centerZ) < Mathf.Max(0f, deadband)
            ? 0f
            : Mathf.Sign(partZ - centerZ) * noseEvidence;

    /// <summary>
    /// Turns the summed part votes into a facing: +1 already faces forward, -1 has to be
    /// turned, 0 means the parts did not say. One strong part is enough to decide, while a
    /// single weak token is not, so an odd name cannot spin a whole model around.
    /// </summary>
    public static int FacingFromVotes(float votes, float threshold = .5f)
        => votes >= threshold ? 1 : votes <= -threshold ? -1 : 0;

    /// <summary>
    /// True when a name contains the token on its own boundaries, so 'FR' matches 'wheel_fr'
    /// and 'door_fl', but 'front' does not match the bone named 'FrontAxisSignS'.
    /// </summary>
    public static bool HasToken(string value, string token)
    {
        string name = Normalise(value);
        if (name.Length == 0 || string.IsNullOrEmpty(token)) return false;
        for (int at = name.IndexOf(token, StringComparison.Ordinal); at >= 0; at = name.IndexOf(token, at + 1, StringComparison.Ordinal))
        {
            bool startsClean = at == 0 || !char.IsLetterOrDigit(name[at - 1]);
            int end = at + token.Length;
            bool endsClean = end >= name.Length || !char.IsLetterOrDigit(name[end]);
            if (startsClean && endsClean) return true;
        }
        return false;
    }

    /// <summary>True for a part that sits at the nose of a vehicle.</summary>
    public static bool IsFrontPart(string partName)
    {
        string name = Normalise(partName);
        return name == "front" || name.StartsWith("front ") || name.StartsWith("front_") || name.StartsWith("front-") ||
            name.Contains("headlight") || name.Contains("head light") || name.Contains("headlamp");
    }

    /// <summary>True for a part that sits at the tail of a vehicle.</summary>
    public static bool IsRearPart(string partName)
    {
        string name = Normalise(partName);
        return name == "rear" || name == "back" || name.StartsWith("rear ") || name.StartsWith("rear_") || name.StartsWith("rear-") ||
            name.Contains("taillight") || name.Contains("tail light") || name.Contains("brakelight");
    }

    static string Normalise(string value) => string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim().ToLowerInvariant();

    /// <summary>Length along the long horizontal axis, whichever way the model was authored.</summary>
    public static float LengthOf(Vector3 size) => Mathf.Max(Mathf.Abs(size.x), Mathf.Abs(size.z));

    static bool Contains(string name, params string[] tokens)
    {
        for (int i = 0; i < tokens.Length; i++)
            if (name.Contains(tokens[i])) return true;
        return false;
    }
}
