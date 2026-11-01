using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum PassengerVisualState
{
    Idle,
    Walk,
    Board,
    Seated,
    Exit
}

/// <summary>
/// The parameter contract an authored passenger Animator Controller can use.
/// A controller that exposes these parameters works without any code changes;
/// a controller that does not is left alone and the visual falls back to the
/// procedural path. The contract is intentionally small so a mobile controller
/// needs one float, three bools, one int and one trigger at most.
/// </summary>
public static class PassengerAnimatorParameters
{
    public const string Speed = "Speed";
    public const string Seated = "Seated";
    public const string Boarding = "Boarding";
    public const string Alighting = "Alighting";
    public const string State = "State";
    public const string Reaction = "Reaction";

    public static int StateIndex(PassengerVisualState state) => (int)state;

    public static bool HasParameter(Animator animator, string parameterName, AnimatorControllerParameterType type)
    {
        if (animator == null || string.IsNullOrEmpty(parameterName))
            return false;

        AnimatorControllerParameter[] parameters = animator.parameters;
        for (int i = 0; i < parameters.Length; i++)
        {
            if (parameters[i].name == parameterName && parameters[i].type == type)
                return true;
        }
        return false;
    }
}

/// <summary>
/// Drives one pooled passenger visual.
///
/// The default visual is a small articulated proxy built from primitives: it has
/// a body, head, two arms and two legs, all posed procedurally. That is the path
/// used when the project has no humanoid animation clips (the current state of
/// the repository), so passengers visibly walk, board, sit and exit instead of
/// sliding around as capsules.
///
/// If a prefab with an Animator Controller is supplied, the controller is driven
/// through <see cref="PassengerAnimatorParameters"/> and the procedural rig is
/// skipped. A prefab with no controller gets a simple body bob, so a missing rig
/// never becomes a frozen prop.
/// </summary>
[DisallowMultipleComponent]
public class PassengerVisual : MonoBehaviour
{
    public const float ReactionSeconds = 1.35f;

    const float PoseSmoothing = 14f;
    const float WalkPhaseSpeed = 8.5f;
    const float BoardPhaseSpeed = 5.5f;
    const float ExitPhaseSpeed = 9.5f;
    const float IdlePhaseSpeed = 1.4f;

    static readonly int SpeedId = Animator.StringToHash(PassengerAnimatorParameters.Speed);
    static readonly int SeatedId = Animator.StringToHash(PassengerAnimatorParameters.Seated);
    static readonly int BoardingId = Animator.StringToHash(PassengerAnimatorParameters.Boarding);
    static readonly int AlightingId = Animator.StringToHash(PassengerAnimatorParameters.Alighting);
    static readonly int StateId = Animator.StringToHash(PassengerAnimatorParameters.State);
    static readonly int ReactionId = Animator.StringToHash(PassengerAnimatorParameters.Reaction);

    public Animator animator;

    Transform body;
    Transform head;
    Transform armLPivot;
    Transform armRPivot;
    Transform legLPivot;
    Transform legRPivot;
    Transform wheelchairRoot;
    Transform fallbackRoot;
    Renderer bodyRenderer;
    readonly List<Renderer> renderers = new List<Renderer>();
    MaterialPropertyBlock block;

    PassengerAgent agent;
    int boundVersion = -1;
    PassengerVisualState state = PassengerVisualState.Idle;
    Vector3 bodyBasePosition;
    Vector3 headBasePosition;
    float animationTime;
    float updateAccumulator;
    float reactionTimer;
    float reactionAge;
    string reactionTrigger;
    bool fullAnimation = true;
    bool built;
    bool visible = true;
    bool waitingOnPlatform;

    bool hasSpeed;
    bool hasSeated;
    bool hasBoarding;
    bool hasAlighting;
    bool hasState;
    bool hasReaction;

    public PassengerAgent Agent => agent;
    public PassengerVisualState CurrentState => state;
    public bool ReactionActive => reactionTimer > 0f;
    public bool HasProceduralRig => built;
    public bool WheelchairVisible => wheelchairRoot != null && wheelchairRoot.gameObject.activeSelf;
    public bool UsesAnimator => animator != null && animator.runtimeAnimatorController != null;
    public int RendererCount => renderers.Count;
    public bool RenderersVisible => visible;

    void Awake()
    {
        CacheAnimator();
    }

    /// <summary>Builds the default articulated proxy under <paramref name="parent"/>.</summary>
    public static PassengerVisual CreateProcedural(Transform parent, Material bodyMaterial, Material skinMaterial,
        Material legMaterial, Material wheelchairMaterial)
    {
        var go = new GameObject("PassengerVisual");
        go.transform.SetParent(parent, false);
        var visual = go.AddComponent<PassengerVisual>();
        visual.BuildProcedural(bodyMaterial, skinMaterial, legMaterial, wheelchairMaterial);
        return visual;
    }

    /// <summary>Prepares an authored prefab. Finds its Animator and, when it has no
    /// controller, wraps its children so the fallback bob can move them.</summary>
    public void PrepareExisting()
    {
        CacheAnimator();
        CacheRenderers();
        if (!UsesAnimator)
            EnsureFallbackRoot();
    }

    /// <summary>Maps the simulation flags to the animation state the visual should show.</summary>
    public static PassengerVisualState ResolveState(PassengerAgent agent, bool waitingOnPlatform)
    {
        if (agent == null)
            return PassengerVisualState.Idle;
        if (agent.isAlighting)
            return PassengerVisualState.Exit;
        if (agent.isBoarding)
            return PassengerVisualState.Board;
        if (agent.isWheelchairPassenger || agent.isSeated)
            return PassengerVisualState.Seated;
        // Waiting agents stand until BeginBoarding(), so both the platform and
        // the bus interior resolve to Idle here; Walk stays available to authored
        // controllers that drive State themselves.
        return PassengerVisualState.Idle;
    }

    /// <summary>Binds this visual to an agent. Reuse of a proxy always resets the pose first.</summary>
    public void Bind(PassengerAgent newAgent, PassengerVisualState newState, bool waiting)
    {
        if (newAgent == null)
        {
            Release();
            return;
        }

        // Reference equality is not enough: PassengerManager recycles the same
        // agent object, so the version catches a same-frame reuse that would
        // otherwise keep the previous passenger's pose and archetype.
        if (agent != newAgent || boundVersion != newAgent.reuseVersion)
        {
            agent = newAgent;
            boundVersion = newAgent.reuseVersion;
            ResetPoseImmediate();
            ApplyArchetype();
        }

        state = newState;
        waitingOnPlatform = waiting;
        // Visibility is owned by Tick, which knows the distance. Forcing the
        // renderers on here would toggle every far proxy once per frame.
        ApplyAnimatorState();
    }

    /// <summary>
    /// Advances the visual. Distance uses the same bands as the pedestrian crowd,
    /// so a passenger at the far end of the platform updates at 4 Hz while one
    /// beside the camera keeps the full cycle every frame.
    /// </summary>
    public void Tick(float deltaTime, float distanceMeters, bool full)
    {
        if (agent == null)
            return;

        if (!CrowdPerformancePolicy.AnimatesAt(distanceMeters))
        {
            SetRenderersVisible(false);
            return;
        }

        SetRenderersVisible(true);
        fullAnimation = full;

        float interval = CrowdPerformancePolicy.AnimationInterval(distanceMeters);
        updateAccumulator += Mathf.Max(0f, deltaTime);
        if (interval > 0f && updateAccumulator < interval)
            return;

        float step = interval > 0f ? updateAccumulator : Mathf.Max(0.0001f, deltaTime);
        updateAccumulator = 0f;

        animationTime += step * (full ? 1f : 0.65f);
        if (reactionTimer > 0f)
        {
            reactionTimer = Mathf.Max(0f, reactionTimer - step);
            reactionAge += step;
        }

        if (UsesAnimator)
        {
            ApplyAnimatorState();
            return;
        }

        if (built)
            ApplyProceduralPose(step);
        else
            ApplyFallbackPose(step);
    }

    /// <summary>Plays a reaction trigger on the Animator or as a short procedural gesture.</summary>
    public void PlayReaction(string trigger)
    {
        if (string.IsNullOrWhiteSpace(trigger))
            return;

        reactionTrigger = trigger;
        reactionTimer = ReactionSeconds;
        reactionAge = 0f;

        if (UsesAnimator && hasReaction)
            animator.SetTrigger(ReactionId);
    }

    /// <summary>Returns the proxy to a neutral pose. Called before it enters the spare pool.</summary>
    public void Release()
    {
        agent = null;
        boundVersion = -1;
        state = PassengerVisualState.Idle;
        waitingOnPlatform = false;
        reactionTimer = 0f;
        reactionAge = 0f;
        reactionTrigger = null;
        updateAccumulator = 0f;
        animationTime = 0f;
        ResetPoseImmediate();
        SetRenderersVisible(false);

        if (animator != null)
        {
            animator.Rebind();
            animator.Update(0f);
        }
    }

    void BuildProcedural(Material bodyMaterial, Material skinMaterial, Material legMaterial,
        Material wheelchairMaterial)
    {
        built = true;

        body = Part(PrimitiveType.Capsule, "Body", new Vector3(0f, 0.95f, 0f),
            new Vector3(0.38f, 0.525f, 0.32f), bodyMaterial);
        bodyRenderer = body.GetComponent<Renderer>();

        head = Part(PrimitiveType.Sphere, "Head", new Vector3(0f, 1.62f, 0f),
            new Vector3(0.24f, 0.28f, 0.24f), skinMaterial);

        legLPivot = Pivot("Leg L Pivot", new Vector3(-0.13f, 0.86f, 0f));
        legRPivot = Pivot("Leg R Pivot", new Vector3(0.13f, 0.86f, 0f));
        Limb(PrimitiveType.Capsule, "Leg L", legLPivot, 0.84f, 0.07f, legMaterial);
        Limb(PrimitiveType.Capsule, "Leg R", legRPivot, 0.84f, 0.07f, legMaterial);

        armLPivot = Pivot("Arm L Pivot", new Vector3(-0.29f, 1.38f, 0f));
        armRPivot = Pivot("Arm R Pivot", new Vector3(0.29f, 1.38f, 0f));
        Limb(PrimitiveType.Capsule, "Arm L", armLPivot, 0.62f, 0.045f, skinMaterial);
        Limb(PrimitiveType.Capsule, "Arm R", armRPivot, 0.62f, 0.045f, skinMaterial);

        BuildWheelchair(wheelchairMaterial);

        bodyBasePosition = body.localPosition;
        headBasePosition = head.localPosition;
        CacheRenderers();
        ResetPoseImmediate();
    }

    void BuildWheelchair(Material material)
    {
        wheelchairRoot = new GameObject("Wheelchair").transform;
        wheelchairRoot.SetParent(transform, false);

        Part(PrimitiveType.Cube, "Wheelchair seat", new Vector3(0f, 0.56f, 0f),
            new Vector3(0.46f, 0.06f, 0.46f), material, wheelchairRoot);
        Transform wheelL = Part(PrimitiveType.Cylinder, "Wheel L", new Vector3(-0.3f, 0.32f, 0f),
            new Vector3(0.62f, 0.035f, 0.62f), material, wheelchairRoot);
        wheelL.localRotation = Quaternion.Euler(0f, 0f, 90f);
        Transform wheelR = Part(PrimitiveType.Cylinder, "Wheel R", new Vector3(0.3f, 0.32f, 0f),
            new Vector3(0.62f, 0.035f, 0.62f), material, wheelchairRoot);
        wheelR.localRotation = Quaternion.Euler(0f, 0f, 90f);

        wheelchairRoot.gameObject.SetActive(false);
    }

    Transform Pivot(string name, Vector3 localPosition)
    {
        var go = new GameObject(name);
        go.transform.SetParent(transform, false);
        go.transform.localPosition = localPosition;
        return go.transform;
    }

    Transform Limb(PrimitiveType type, string name, Transform pivot, float height, float radius, Material material)
    {
        return Part(type, name, new Vector3(0f, -height * 0.5f, 0f),
            new Vector3(radius * 2f, height * 0.5f, radius * 2f), material, pivot);
    }

    Transform Part(PrimitiveType type, string name, Vector3 localPosition, Vector3 localScale,
        Material material, Transform parent = null)
    {
        var go = GameObject.CreatePrimitive(type);
        go.name = name;
        Transform t = go.transform;
        t.SetParent(parent != null ? parent : transform, false);
        t.localPosition = localPosition;
        t.localScale = localScale;

        Collider collider = go.GetComponent<Collider>();
        if (collider != null)
        {
            // Disable immediately: a primitive created during Start would otherwise
            // spend one frame as a live collider inside the bus.
            collider.enabled = false;
            if (Application.isPlaying) Destroy(collider);
            else DestroyImmediate(collider);
        }

        Renderer renderer = go.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.sharedMaterial = material;
            renderer.shadowCastingMode = ShadowCastingMode.Off;
        }
        return t;
    }

    void ApplyArchetype()
    {
        if (agent == null)
            return;

        float scale = 1f;
        Color clothes;
        switch (agent.archetype)
        {
            case PassengerArchetype.Tourist:
                clothes = new Color(0.92f, 0.52f, 0.18f);
                scale = 1.02f;
                break;
            case PassengerArchetype.Student:
                clothes = new Color(0.2f, 0.65f, 0.4f);
                scale = 0.92f;
                break;
            case PassengerArchetype.Elderly:
                clothes = new Color(0.55f, 0.4f, 0.7f);
                scale = 0.96f;
                break;
            default:
                clothes = new Color(0.2f, 0.48f, 0.82f);
                break;
        }

        transform.localScale = Vector3.one * scale;

        if (bodyRenderer != null)
        {
            if (block == null) block = new MaterialPropertyBlock();
            bodyRenderer.GetPropertyBlock(block);
            block.SetColor("_Color", clothes);
            bodyRenderer.SetPropertyBlock(block);
        }

        bool wheelchair = agent.isWheelchairPassenger;
        if (wheelchairRoot != null)
            wheelchairRoot.gameObject.SetActive(wheelchair);
        if (legLPivot != null)
            legLPivot.gameObject.SetActive(!wheelchair);
        if (legRPivot != null)
            legRPivot.gameObject.SetActive(!wheelchair);
    }

    void ApplyProceduralPose(float step)
    {
        float alpha = 1f - Mathf.Exp(-PoseSmoothing * Mathf.Max(0.0001f, step));
        float phase = animationTime * PhaseSpeedFor(state);

        float legSwing = 0f;
        float armSwing = 0f;
        float bodyLift = 0f;
        float bodyLean = 0f;
        float headYaw = Mathf.Sin(phase * 0.5f) * 4f;

        switch (state)
        {
            case PassengerVisualState.Walk:
                legSwing = Mathf.Sin(phase) * 32f;
                armSwing = -legSwing * 0.8f;
                bodyLift = Mathf.Abs(Mathf.Cos(phase)) * 0.025f;
                break;
            case PassengerVisualState.Board:
                legSwing = Mathf.Sin(phase) * 24f;
                armSwing = -legSwing * 0.6f;
                bodyLift = Mathf.Abs(Mathf.Cos(phase)) * 0.02f;
                bodyLean = 8f;
                break;
            case PassengerVisualState.Exit:
                legSwing = Mathf.Sin(phase) * 34f;
                armSwing = -legSwing * 0.8f;
                bodyLift = Mathf.Abs(Mathf.Cos(phase)) * 0.02f;
                break;
            case PassengerVisualState.Seated:
                bodyLift = -0.28f;
                break;
            default:
                armSwing = Mathf.Sin(phase) * 3f;
                bodyLift = Mathf.Sin(phase * 0.7f) * 0.01f;
                break;
        }

        if (state == PassengerVisualState.Seated || (agent != null && agent.isWheelchairPassenger))
        {
            Slerp(legLPivot, Quaternion.Euler(-78f, 0f, 0f), alpha);
            Slerp(legRPivot, Quaternion.Euler(-78f, 0f, 0f), alpha);
            Slerp(armLPivot, Quaternion.Euler(-8f, 0f, -4f), alpha);
            Slerp(armRPivot, Quaternion.Euler(-8f, 0f, 4f), alpha);
        }
        else
        {
            Slerp(legLPivot, Quaternion.Euler(legSwing, 0f, 0f), alpha);
            Slerp(legRPivot, Quaternion.Euler(-legSwing, 0f, 0f), alpha);
            Slerp(armLPivot, Quaternion.Euler(armSwing, 0f, -4f), alpha);
            Slerp(armRPivot, Quaternion.Euler(-armSwing, 0f, 4f), alpha);
        }

        float sway = fullAnimation && agent != null ? agent.standingSway * 22f : 0f;
        if (state == PassengerVisualState.Seated)
            sway = 0f;

        Slerp(body, Quaternion.Euler(bodyLean, 0f, sway), alpha);
        Vector3 bodyTarget = bodyBasePosition + Vector3.up * bodyLift;
        body.localPosition = Vector3.Lerp(body.localPosition, bodyTarget, alpha);

        Slerp(head, Quaternion.Euler(0f, headYaw, -sway * 0.35f), alpha);
        head.localPosition = Vector3.Lerp(head.localPosition,
            headBasePosition + Vector3.up * bodyLift, alpha);

        ApplyReactionPose(alpha, phase);
    }

    void ApplyReactionPose(float alpha, float phase)
    {
        if (reactionTimer <= 0f || string.IsNullOrEmpty(reactionTrigger))
            return;

        string trigger = reactionTrigger.ToLowerInvariant();
        if (trigger.Contains("wave") || trigger.Contains("greet") || trigger.Contains("approve"))
        {
            float wave = -125f + Mathf.Sin(reactionAge * 22f) * 14f;
            Slerp(armRPivot, Quaternion.Euler(wave, 0f, 18f), alpha);
            Slerp(head, Quaternion.Euler(0f, Mathf.Sin(reactionAge * 15f) * 9f, 0f), alpha);
        }
        else if (trigger.Contains("brace") || trigger.Contains("rough") || trigger.Contains("complain"))
        {
            Slerp(armLPivot, Quaternion.Euler(-55f, 0f, -12f), alpha);
            Slerp(armRPivot, Quaternion.Euler(-55f, 0f, 12f), alpha);
            Slerp(body, Quaternion.Euler(-7f, 0f, 0f), alpha);
            Slerp(head, Quaternion.Euler(9f, 0f, 0f), alpha);
        }
        else if (trigger.Contains("weather"))
        {
            Slerp(head, Quaternion.Euler(-12f, 0f, 0f), alpha);
            Slerp(armLPivot, Quaternion.Euler(0f, 0f, -10f), alpha);
            Slerp(armRPivot, Quaternion.Euler(0f, 0f, 10f), alpha);
        }
        else
        {
            Slerp(head, Quaternion.Euler(0f, Mathf.Sin(phase) * 6f, 0f), alpha);
        }
    }

    void ApplyFallbackPose(float step)
    {
        if (fallbackRoot == null)
            return;

        float alpha = 1f - Mathf.Exp(-PoseSmoothing * Mathf.Max(0.0001f, step));
        float phase = animationTime * PhaseSpeedFor(state);
        float bob = state == PassengerVisualState.Walk || state == PassengerVisualState.Board ||
                    state == PassengerVisualState.Exit
            ? Mathf.Abs(Mathf.Sin(phase)) * 0.035f
            : Mathf.Sin(phase * 0.7f) * 0.012f;
        float lean = state == PassengerVisualState.Board ? 7f : 0f;

        fallbackRoot.localPosition = Vector3.Lerp(fallbackRoot.localPosition, Vector3.up * bob, alpha);
        fallbackRoot.localRotation = Quaternion.Slerp(fallbackRoot.localRotation,
            Quaternion.Euler(lean, 0f, 0f), alpha);
    }

    static float PhaseSpeedFor(PassengerVisualState state)
    {
        switch (state)
        {
            case PassengerVisualState.Walk: return WalkPhaseSpeed;
            case PassengerVisualState.Board: return BoardPhaseSpeed;
            case PassengerVisualState.Exit: return ExitPhaseSpeed;
            default: return IdlePhaseSpeed;
        }
    }

    void ResetPoseImmediate()
    {
        if (legLPivot != null) legLPivot.localRotation = Quaternion.identity;
        if (legRPivot != null) legRPivot.localRotation = Quaternion.identity;
        if (armLPivot != null) armLPivot.localRotation = Quaternion.identity;
        if (armRPivot != null) armRPivot.localRotation = Quaternion.identity;
        if (body != null)
        {
            body.localRotation = Quaternion.identity;
            body.localPosition = bodyBasePosition;
        }
        if (head != null)
        {
            head.localRotation = Quaternion.identity;
            head.localPosition = headBasePosition;
        }
        if (fallbackRoot != null)
        {
            fallbackRoot.localPosition = Vector3.zero;
            fallbackRoot.localRotation = Quaternion.identity;
        }

        bool wheelchair = agent != null && agent.isWheelchairPassenger;
        if (wheelchairRoot != null) wheelchairRoot.gameObject.SetActive(wheelchair);
        if (legLPivot != null) legLPivot.gameObject.SetActive(!wheelchair);
        if (legRPivot != null) legRPivot.gameObject.SetActive(!wheelchair);
    }

    void CacheAnimator()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>(true);

        hasSpeed = PassengerAnimatorParameters.HasParameter(animator, PassengerAnimatorParameters.Speed,
            AnimatorControllerParameterType.Float);
        hasSeated = PassengerAnimatorParameters.HasParameter(animator, PassengerAnimatorParameters.Seated,
            AnimatorControllerParameterType.Bool);
        hasBoarding = PassengerAnimatorParameters.HasParameter(animator, PassengerAnimatorParameters.Boarding,
            AnimatorControllerParameterType.Bool);
        hasAlighting = PassengerAnimatorParameters.HasParameter(animator, PassengerAnimatorParameters.Alighting,
            AnimatorControllerParameterType.Bool);
        hasState = PassengerAnimatorParameters.HasParameter(animator, PassengerAnimatorParameters.State,
            AnimatorControllerParameterType.Int);
        hasReaction = PassengerAnimatorParameters.HasParameter(animator, PassengerAnimatorParameters.Reaction,
            AnimatorControllerParameterType.Trigger);
    }

    void ApplyAnimatorState()
    {
        if (!UsesAnimator)
            return;

        if (hasSpeed)
            animator.SetFloat(SpeedId, state == PassengerVisualState.Idle || state == PassengerVisualState.Seated ? 0f : 1f);
        if (hasSeated)
            animator.SetBool(SeatedId, state == PassengerVisualState.Seated);
        if (hasBoarding)
            animator.SetBool(BoardingId, state == PassengerVisualState.Board);
        if (hasAlighting)
            animator.SetBool(AlightingId, state == PassengerVisualState.Exit);
        if (hasState)
            animator.SetInteger(StateId, PassengerAnimatorParameters.StateIndex(state));
    }

    void CacheRenderers()
    {
        renderers.Clear();
        Renderer[] found = GetComponentsInChildren<Renderer>(true);
        for (int i = 0; i < found.Length; i++)
            renderers.Add(found[i]);

        if (bodyRenderer == null)
            bodyRenderer = GetComponentInChildren<Renderer>();
    }

    void EnsureFallbackRoot()
    {
        if (fallbackRoot != null)
            return;

        var go = new GameObject("Passenger Model");
        fallbackRoot = go.transform;
        fallbackRoot.SetParent(transform, false);

        var children = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child != fallbackRoot)
                children.Add(child);
        }

        for (int i = 0; i < children.Count; i++)
            children[i].SetParent(fallbackRoot, true);
    }

    void SetRenderersVisible(bool value)
    {
        if (visible == value)
            return;

        visible = value;
        for (int i = 0; i < renderers.Count; i++)
        {
            if (renderers[i] != null)
                renderers[i].enabled = value;
        }
    }

    static void Slerp(Transform target, Quaternion rotation, float alpha)
    {
        if (target == null)
            return;
        target.localRotation = Quaternion.Slerp(target.localRotation, rotation, alpha);
    }
}
