using UnityEngine;
using System.Collections.Generic;

public class PedestrianSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ZebraCrossing
    {
        public string crossingId = "X0";
        public Transform spawnA;
        public Transform spawnB;
        public TrafficLight controllingTrafficLight;
    }

    [Header("Crossings")]
    public ZebraCrossing[] crossings;

    [Header("Pedestrians")]
    public GameObject pedestrianPrefab;
    public int maxActivePedestrians = 40;
    public float spawnCheckInterval = 1.25f;
    public float walkSpeed = 1.4f;

    [Header("Density")]
    public float baselineSpawnChance = 0.2f;
    public float rushMultiplier = 2.0f;

    readonly List<SimplePedestrian> active = new List<SimplePedestrian>();
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnCheckInterval)
        {
            timer = 0f;
            TrySpawnPedestrians();
        }

        for (int i = active.Count - 1; i >= 0; i--)
        {
            var p = active[i];
            if (p == null) { active.RemoveAt(i); continue; }
            p.Tick(Time.deltaTime, walkSpeed);
            if (p.IsDone)
            {
                Destroy(p.gameObject);
                active.RemoveAt(i);
            }
        }
    }

    void TrySpawnPedestrians()
    {
        if (pedestrianPrefab == null || crossings == null || crossings.Length == 0) return;
        if (active.Count >= maxActivePedestrians) return;

        float density = GetTimeOfDayDensity();
        float spawnChance = Mathf.Clamp01(baselineSpawnChance * density);

        for (int i = 0; i < crossings.Length; i++)
        {
            var x = crossings[i];
            if (x == null || x.spawnA == null || x.spawnB == null) continue;

            // Only cross when road traffic is red (pedestrian green assumed).
            if (x.controllingTrafficLight != null && !x.controllingTrafficLight.IsRed())
                continue;

            if (Random.value > spawnChance)
                continue;

            bool dir = Random.value > 0.5f;
            Transform from = dir ? x.spawnA : x.spawnB;
            Transform to = dir ? x.spawnB : x.spawnA;

            GameObject go = Instantiate(pedestrianPrefab, from.position, Quaternion.identity, transform);
            var ped = go.GetComponent<SimplePedestrian>();
            if (ped == null) ped = go.AddComponent<SimplePedestrian>();
            ped.Init(to);
            active.Add(ped);

            if (active.Count >= maxActivePedestrians)
                break;
        }
    }

    float GetTimeOfDayDensity()
    {
        float now = ScheduleManager.Instance != null ? ScheduleManager.Instance.currentTimeMinutes : 12f * 60f;
        float morning = Gaussian(now, 7.5f * 60f, 90f);
        float evening = Gaussian(now, 17.5f * 60f, 90f);
        float rushSignal = Mathf.Clamp01(Mathf.Max(morning, evening));
        return Mathf.Lerp(1f, rushMultiplier, rushSignal);
    }

    static float Gaussian(float x, float mean, float sigma)
    {
        if (sigma <= 0.001f) return 0f;
        float d = (x - mean) / sigma;
        return Mathf.Exp(-0.5f * d * d);
    }
}

public class SimplePedestrian : MonoBehaviour
{
    Transform target;
    public bool IsDone { get; private set; }

    public void Init(Transform destination)
    {
        target = destination;
        IsDone = false;
    }

    public void Tick(float dt, float speed)
    {
        if (target == null) { IsDone = true; return; }
        Vector3 to = target.position - transform.position;
        to.y = 0f;
        float dist = to.magnitude;
        if (dist < 0.25f) { IsDone = true; return; }
        Vector3 dir = to / Mathf.Max(0.001f, dist);
        transform.position += dir * speed * dt;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), dt * 8f);
    }
}
