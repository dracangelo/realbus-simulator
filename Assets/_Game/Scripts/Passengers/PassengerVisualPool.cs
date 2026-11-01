using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pools the visible passengers and hands each one a <see cref="PassengerVisual"/>.
///
/// The default proxy is a small articulated figure with procedural idle, walk,
/// board, seated and exit poses, so a fresh checkout shows animated passengers
/// without importing a rig. A prefab can be assigned instead; if it carries an
/// Animator Controller the controller is driven through the passenger parameter
/// contract, otherwise the visual falls back to a body bob.
///
/// The pool is deliberately not a second passenger simulation: it only reads the
/// lists owned by <see cref="PassengerManager"/> and never changes their state.
/// </summary>
public class PassengerVisualPool : MonoBehaviour
{
    public PassengerManager passengers;
    public BusController bus;
    public GameObject passengerPrefab;
    public int visualBudget = CrowdPerformancePolicy.MaxPassengerProxies;
    [Tooltip("The procedural proxy uses several primitive renderers. Keep the " +
             "animated budget below the authored prefab budget so a phone does not " +
             "draw a full bus of articulated figures.")]
    public int maxProceduralVisuals = 32;

    readonly Dictionary<PassengerAgent, PassengerVisual> visible =
        new Dictionary<PassengerAgent, PassengerVisual>();
    readonly Stack<PassengerVisual> spare = new Stack<PassengerVisual>();
    readonly HashSet<PassengerAgent> used = new HashSet<PassengerAgent>();
    readonly List<PassengerAgent> expired = new List<PassengerAgent>();

    Material bodyMaterial;
    Material skinMaterial;
    Material legMaterial;
    Material wheelchairMaterial;
    Camera viewer;
    float viewerRefresh;
    bool loggedFirstDraw;

    void Start()
    {
        if (passengers == null) passengers = PassengerManager.Instance;
        if (bus == null) bus = FindAnyObjectByType<BusController>();
        if (passengers == null || bus == null)
        {
            enabled = false;
            return;
        }

        int requested = visualBudget <= 0 ? CrowdPerformancePolicy.MaxPassengerProxies : visualBudget;
        int tierBudget = CrowdPerformancePolicy.BudgetForQuality(
            QualitySettings.GetQualityLevel(), Application.isMobilePlatform, requested);
        if (passengerPrefab == null)
            tierBudget = Mathf.Min(tierBudget, Mathf.Max(1, maxProceduralVisuals));
        visualBudget = Mathf.Clamp(tierBudget, 1, CrowdPerformancePolicy.MaxPassengerProxies);

        bodyMaterial = CreateMaterial(Color.white);
        skinMaterial = CreateMaterial(new Color(0.72f, 0.54f, 0.42f));
        legMaterial = CreateMaterial(new Color(0.16f, 0.18f, 0.22f));
        wheelchairMaterial = CreateMaterial(new Color(0.1f, 0.1f, 0.12f));

        viewer = Camera.main;
        for (int i = 0; i < visualBudget; i++)
            spare.Push(BuildProxy());

        // One line at startup so a device log proves which visual path is live:
        // "procedural" means no humanoid clips were available and the articulated
        // proxy is driving the poses.
        Debug.Log($"[PassengerVisualPool] budget={visualBudget}, mode=" +
                  $"{(passengerPrefab != null ? (spare.Count > 0 && spare.Peek().UsesAnimator ? "animator" : "prefab-bob") : "procedural")}, " +
                  $"quality={QualitySettings.GetQualityLevel()}, mobile={Application.isMobilePlatform}");
    }

    void LateUpdate()
    {
        if (passengers == null || bus == null)
            return;

        if (viewer == null || !viewer.isActiveAndEnabled)
        {
            viewerRefresh -= Time.unscaledDeltaTime;
            if (viewerRefresh <= 0f)
            {
                viewer = Camera.main;
                viewerRefresh = 1f;
            }
        }

        Vector3 viewerPosition = viewer != null ? viewer.transform.position : bus.transform.position;
        used.Clear();
        Draw(passengers.OnboardPassengers, false, viewerPosition);
        Draw(passengers.AlightingPassengers, false, viewerPosition);
        Draw(passengers.WaitingPassengers, true, viewerPosition);

        expired.Clear();
        foreach (var entry in visible)
        {
            if (!used.Contains(entry.Key))
                expired.Add(entry.Key);
        }

        for (int i = 0; i < expired.Count; i++)
        {
            PassengerAgent agent = expired[i];
            if (!visible.TryGetValue(agent, out PassengerVisual visual))
                continue;

            visual.Release();
            visual.gameObject.SetActive(false);
            spare.Push(visual);
            visible.Remove(agent);
        }
    }

    void Draw(IReadOnlyList<PassengerAgent> agents, bool waiting, Vector3 viewerPosition)
    {
        for (int i = 0; i < agents.Count; i++)
        {
            PassengerAgent agent = agents[i];
            if (agent == null)
                continue;

            used.Add(agent);
            if (!visible.TryGetValue(agent, out PassengerVisual visual))
            {
                if (spare.Count == 0)
                    continue;

                visual = spare.Pop();
                visible.Add(agent, visual);
                visual.gameObject.SetActive(true);
            }

            PassengerVisualState state = PassengerVisual.ResolveState(agent, waiting);
            visual.Bind(agent, state, waiting);

            Vector3 position = waiting ? WaitingPosition(i) : OnboardPosition(agent);
            visual.transform.position = position;
            visual.transform.rotation = waiting
                ? FacingBus(position)
                : bus.transform.rotation;

            float distance = Vector3.Distance(viewerPosition, position);
            visual.Tick(Time.deltaTime, distance, CrowdPerformancePolicy.UsesFullAnimation(distance));

            if (!loggedFirstDraw)
            {
                loggedFirstDraw = true;
                Debug.Log($"[PassengerVisualPool] first draw: state={state}, waiting={waiting}, " +
                          $"distance={distance:F1}m, rig={(visual.HasProceduralRig ? "procedural" : visual.UsesAnimator ? "animator" : "fallback")}");
            }
        }
    }

    Vector3 WaitingPosition(int index)
    {
        // The queue is laid out in the stop's own kerb frame. The old version
        // offset the road-centre GPS point along world +X, which put the queue in
        // a traffic lane whenever the street did not happen to run north-south.
        return BusStopPlatformRegistry.LayoutSlot(passengers.WaitingPlatformWorld,
            passengers.WaitingPlatformOutward, passengers.WaitingPlatformAlong, index);
    }

    Vector3 OnboardPosition(PassengerAgent agent)
    {
        Vector3 local = agent.busLocalPosition;
        float footOffset = agent.isSeated ? 0.55f : 0.9f;
        if (agent.isBoarding)
            footOffset *= Mathf.Clamp01(agent.boardingLerp);
        // The door point is roughly 1.05 m above the floor in bus space; keep the
        // exiting proxy's feet near the floor instead of floating at hip height.
        if (agent.isAlighting)
            footOffset = 0.9f;
        local.y -= footOffset;
        return bus.transform.TransformPoint(local);
    }

    Quaternion FacingBus(Vector3 position)
    {
        Vector3 toBus = bus.transform.position - position;
        toBus.y = 0f;
        return toBus.sqrMagnitude > 0.001f
            ? Quaternion.LookRotation(toBus.normalized, Vector3.up)
            : bus.transform.rotation;
    }

    PassengerVisual BuildProxy()
    {
        PassengerVisual visual;
        if (passengerPrefab != null)
        {
            GameObject go = Instantiate(passengerPrefab, transform);
            visual = go.GetComponent<PassengerVisual>();
            if (visual == null)
                visual = go.AddComponent<PassengerVisual>();
            visual.PrepareExisting();
        }
        else
        {
            visual = PassengerVisual.CreateProcedural(transform, bodyMaterial, skinMaterial,
                legMaterial, wheelchairMaterial);
        }

        foreach (var animator in visual.GetComponentsInChildren<Animator>(true))
            animator.cullingMode = AnimatorCullingMode.CullCompletely;

        visual.gameObject.SetActive(false);
        return visual;
    }

    static Material CreateMaterial(Color color)
    {
        var material = new Material(BuiltInShaders.Lit);
        material.color = color;
        return material;
    }

    void OnDestroy()
    {
        if (bodyMaterial != null) Destroy(bodyMaterial);
        if (skinMaterial != null) Destroy(skinMaterial);
        if (legMaterial != null) Destroy(legMaterial);
        if (wheelchairMaterial != null) Destroy(wheelchairMaterial);
    }

    /// <summary>Plays a reaction on a few nearby passengers. Limiting the fan-out keeps
    /// a full bus from retriggering every proxy for one subtitle line.</summary>
    public void TriggerReaction(string triggerName)
    {
        if (string.IsNullOrWhiteSpace(triggerName))
            return;

        int triggered = 0;
        foreach (PassengerVisual visual in visible.Values)
        {
            if (visual == null || !visual.gameObject.activeInHierarchy)
                continue;

            visual.PlayReaction(triggerName);
            triggered++;
            if (triggered >= 4)
                break;
        }
    }
}
