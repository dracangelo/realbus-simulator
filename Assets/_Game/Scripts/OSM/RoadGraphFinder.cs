using UnityEngine;

/// <summary>
/// Runtime accessor for the baked <see cref="RoadGraph"/> used by route ETA
/// computation (section 4.4). The graph is built at import time and saved to
/// the city bundle; this component exposes it to the route estimator without
/// requiring a scene reference.
/// </summary>
public class RoadGraphFinder : MonoBehaviour
{
    public static RoadGraphFinder Instance { get; private set; }
    public RoadGraph graph;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (graph == null) graph = FindAnyObjectByType<RoadGraphRuntimeHost>()?.graph;
    }
}

/// <summary>
/// Hosts a RoadGraph instance in the scene so RoadGraphFinder can locate it.
/// Added automatically when a RoadGraph is built from OSM data at runtime.
/// </summary>
public class RoadGraphRuntimeHost : MonoBehaviour
{
    public RoadGraph graph;
}