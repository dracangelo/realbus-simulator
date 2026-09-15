using System.Collections.Generic;
using UnityEngine;

/// <summary>Reusable low-cost passenger proxies; production prefabs can replace the capsules.</summary>
public class PassengerVisualPool : MonoBehaviour
{
    public PassengerManager passengers;
    public BusController bus;
    public GameObject passengerPrefab;
    public int visualBudget = 120;
    readonly Dictionary<PassengerAgent, Transform> visible = new Dictionary<PassengerAgent, Transform>();
    readonly Stack<Transform> spare = new Stack<Transform>();
    readonly HashSet<PassengerAgent> used = new HashSet<PassengerAgent>();
    readonly List<PassengerAgent> expired = new List<PassengerAgent>();
    Material material;

    void Start()
    {
        if (passengers == null) passengers = PassengerManager.Instance;
        if (bus == null) bus = FindFirstObjectByType<BusController>();
        material = new Material(Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard"));
        material.color = new Color(0.15f, 0.55f, 0.75f);
        for (int i = 0; i < visualBudget; i++)
        {
            GameObject go = passengerPrefab != null ? Instantiate(passengerPrefab, transform) : GameObject.CreatePrimitive(PrimitiveType.Capsule);
            go.transform.SetParent(transform, false);
            if (passengerPrefab == null)
            {
                Destroy(go.GetComponent<Collider>());
                go.GetComponent<Renderer>().sharedMaterial = material;
            }
            go.SetActive(false); spare.Push(go.transform);
        }
    }

    void LateUpdate()
    {
        if (passengers == null || bus == null) return;
        used.Clear();
        Draw(passengers.OnboardPassengers, false);
        Draw(passengers.AlightingPassengers, false);
        Draw(passengers.WaitingPassengers, true);
        expired.Clear();
        foreach (var entry in visible) if (!used.Contains(entry.Key)) expired.Add(entry.Key);
        foreach (var agent in expired)
        {
            var proxy = visible[agent]; proxy.gameObject.SetActive(false); spare.Push(proxy); visible.Remove(agent);
        }
    }

    void Draw(IReadOnlyList<PassengerAgent> agents, bool waiting)
    {
        for (int i = 0; i < agents.Count; i++)
        {
            var agent = agents[i]; used.Add(agent);
            if (!visible.TryGetValue(agent, out var proxy))
            {
                if (spare.Count == 0) continue;
                proxy = spare.Pop(); visible.Add(agent, proxy); proxy.gameObject.SetActive(true);
            }
            proxy.position = waiting
                ? passengers.WaitingPlatformWorld + new Vector3(3f + (i % 3) * 0.6f, 0.85f, -(i / 3) * 0.6f)
                : bus.transform.TransformPoint(agent.busLocalPosition);
            proxy.rotation = bus.transform.rotation * Quaternion.Euler(agent.isSeated ? 0f : agent.standingSway * 25f, 0f, 0f);
            if (passengerPrefab == null) proxy.localScale = new Vector3(agent.isWheelchairPassenger ? 0.65f : 0.4f, agent.isSeated || agent.isWheelchairPassenger ? 0.5f : 0.8f, 0.4f);
        }
    }
    void OnDestroy() { if (material != null) Destroy(material); }

    public void TriggerReaction(string triggerName)
    {
        if (string.IsNullOrWhiteSpace(triggerName)) return;
        foreach (Transform proxy in visible.Values)
        {
            if (proxy == null || !proxy.gameObject.activeInHierarchy) continue;
            Animator animator = proxy.GetComponentInChildren<Animator>();
            if (animator == null || !HasTrigger(animator, triggerName)) continue;
            animator.SetTrigger(triggerName); return;
        }
    }

    static bool HasTrigger(Animator animator, string triggerName)
    {
        AnimatorControllerParameter[] parameters = animator.parameters;
        for (int i = 0; i < parameters.Length; i++)
            if (parameters[i].type == AnimatorControllerParameterType.Trigger && parameters[i].name == triggerName) return true;
        return false;
    }
}
