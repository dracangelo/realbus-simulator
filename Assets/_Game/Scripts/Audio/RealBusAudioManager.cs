using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class RealBusAudioManager : MonoBehaviour
{
    public static RealBusAudioManager Instance { get; private set; }
    [Header("Optional production clips / FMOD event bridges")]
    public MonoBehaviour fmodEventBridge;
    public AudioClip engineIdle, engineLoad, tyreRoll, tyreSqueal, brakeSqueal, airHiss;
    public AudioClip passengerChatter, airConditioning, cityAmbience, uiClick, dockingChime, missionFanfare, rankJingle, collisionHit;
    public AudioProductionProfile productionProfile;

    AudioSource idleSource, loadSource, tyreSource, interiorSource, ambienceSource, oneShotSource, uiSource, passengerVoiceSource;
    BusController bus;
    XPSystem hookedXP;
    readonly HashSet<Button> hookedButtons = new HashSet<Button>();
    float nextButtonScan;
    float nextTyreSqueal;
    bool braking;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static RealBusAudioManager EnsureExists()
    {
        if (Instance != null) return Instance;
        RealBusAudioManager existing = FindAnyObjectByType<RealBusAudioManager>();
        return existing != null ? existing : new GameObject("RealBusAudioManager").AddComponent<RealBusAudioManager>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); SceneManager.sceneLoaded += OnSceneLoaded;
        if (productionProfile == null) productionProfile = Resources.Load<AudioProductionProfile>("AudioProductionProfile");
        ApplyProductionProfile(); CreateFallbackClips();
        oneShotSource = AddSource(gameObject, false, 0f);
        uiSource = AddSource(gameObject, false, 0f); passengerVoiceSource = AddSource(gameObject, false, 0.35f);
        if (productionProfile != null) { oneShotSource.outputAudioMixerGroup = productionProfile.effectsGroup; uiSource.outputAudioMixerGroup = productionProfile.uiGroup; passengerVoiceSource.outputAudioMixerGroup = productionProfile.cabinGroup; }
    }

    void Start() { BindScene(); }
    void OnDestroy() { if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded; if (hookedXP != null) hookedXP.RankUpTriggered -= OnRankUp; }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { hookedButtons.Clear(); BindScene(); }

    void BindScene()
    {
        bus = FindAnyObjectByType<BusController>();
        if (bus != null)
        {
            idleSource = EnsureLoop(bus.gameObject, engineIdle, "Engine Idle", 0.9f);
            loadSource = EnsureLoop(bus.gameObject, engineLoad, "Engine Load", 0.9f);
            tyreSource = EnsureLoop(bus.gameObject, tyreRoll, "Tyre Roll", 0.75f);
            interiorSource = EnsureLoop(bus.gameObject, passengerChatter != null ? passengerChatter : airConditioning, "Interior", 0.2f);
            if (productionProfile != null) { idleSource.outputAudioMixerGroup = productionProfile.vehicleGroup; loadSource.outputAudioMixerGroup = productionProfile.vehicleGroup; tyreSource.outputAudioMixerGroup = productionProfile.vehicleGroup; interiorSource.outputAudioMixerGroup = productionProfile.cabinGroup; }
            if (bus.hornAudio != null)
            {
                bus.hornAudio.spatialBlend = 1f; bus.hornAudio.maxDistance = 120f;
                if (bus.hornAudio.GetComponent<AudioReverbFilter>() == null)
                {
                    AudioReverbFilter reverb = bus.hornAudio.gameObject.AddComponent<AudioReverbFilter>();
                    reverb.reverbPreset = AudioReverbPreset.City;
                }
            }
        }
        if (ambienceSource == null) ambienceSource = EnsureLoop(gameObject, cityAmbience, "City Ambience", 0.18f, false);
        if (productionProfile != null && ambienceSource != null) ambienceSource.outputAudioMixerGroup = productionProfile.ambienceGroup;
        if (hookedXP != XPSystem.Instance)
        {
            if (hookedXP != null) hookedXP.RankUpTriggered -= OnRankUp;
            hookedXP = XPSystem.Instance;
            if (hookedXP != null) hookedXP.RankUpTriggered += OnRankUp;
        }
    }

    void Update()
    {
        if (Time.unscaledTime >= nextButtonScan) { nextButtonScan = Time.unscaledTime + 2f; HookButtons(); }
        if (bus == null) return;
        float rpm = Mathf.InverseLerp(650f, 2600f, bus.currentRPM);
        float load = Mathf.Clamp01(Mathf.Max(bus.throttleInput, rpm * 0.45f));
        float effects = SettingsManager.EnsureExists().Current.effectsVolume;
        bool cabinView = MobileCameraController.Instance != null && (MobileCameraController.Instance.CurrentPreset == BusCameraPreset.Cockpit || MobileCameraController.Instance.CurrentPreset == BusCameraPreset.DoorView);
        float enginePerspective = cabinView ? 0.62f : 1f;
        Configure(idleSource, Mathf.Lerp(0.35f, 0.08f, load) * effects * enginePerspective, Mathf.Lerp(0.78f, 1.15f, rpm));
        Configure(loadSource, Mathf.Lerp(0.03f, 0.78f, load) * effects * enginePerspective, Mathf.Lerp(0.75f, 1.35f, rpm));
        float wet = WeatherSystem.Instance != null ? WeatherSystem.Instance.weatherIntensity : 0f;
        float surfaceVolume = 1f; RoadSurfaceDetector road = bus.GetComponent<RoadSurfaceDetector>();
        if (road != null && road.perWheelRoadTypes != null && road.perWheelRoadTypes.Length > 0) surfaceVolume = road.perWheelRoadTypes[0] == RoadSurfaceDetector.RoadType.Dirt ? 1.35f : road.perWheelRoadTypes[0] == RoadSurfaceDetector.RoadType.Cobblestone ? 1.18f : 1f;
        Configure(tyreSource, Mathf.InverseLerp(5f, 70f, bus.currentSpeedKmh) * Mathf.Lerp(0.45f, 0.8f, wet) * effects * surfaceVolume, Mathf.Lerp(0.8f, 1.25f, bus.currentSpeedKmh / 100f));
        int pax = PassengerManager.Instance != null ? PassengerManager.Instance.currentPassengers : 0;
        Configure(interiorSource, (pax > 0 ? Mathf.Lerp(0.08f, 0.32f, pax / 70f) : 0.05f) * effects * (cabinView ? 1f : 0.35f), 1f);
        if (ambienceSource != null) ambienceSource.volume = 0.18f * SettingsManager.EnsureExists().Current.musicVolume * (PassengerManager.Instance != null && PassengerManager.Instance.doorsOpen ? 1.55f : 1f);
        if (Mathf.Abs(bus.steerInput) > 0.72f && bus.currentSpeedKmh > 32f && Time.time >= nextTyreSqueal)
        {
            nextTyreSqueal = Time.time + 0.8f; PlayOneShot(tyreSqueal, 0.3f);
        }
        bool nowBraking = bus.brakeInput > 0.55f && bus.currentSpeedKmh > 5f;
        if (nowBraking && !braking) PlayOneShot(brakeSqueal, 0.45f);
        if (!nowBraking && braking) PlayOneShot(airHiss, 0.55f);
        braking = nowBraking;
    }

    void HookButtons()
    {
        Button[] buttons = FindObjectsByType<Button>(FindObjectsInactive.Include);
        for (int i = 0; i < buttons.Length; i++)
            if (hookedButtons.Add(buttons[i])) buttons[i].onClick.AddListener(PlayUIClick);
    }

    public void PlayUIClick() { PlayOneShotOn(uiSource, uiClick, 0.45f); ForwardEvent("ui.click"); }
    public void PlayDocking() { PlayOneShot(dockingChime, 0.7f); ForwardEvent("bus.docked"); }
    public void PlayDoorOperated(bool locking)
    {
        if (airHiss != null)
            PlayOneShot(airHiss, locking ? 0.65f : 0.5f);
        else if (dockingChime != null)
            PlayOneShot(dockingChime, 0.5f);
        ForwardEvent(locking ? "bus.doors_locked" : "bus.doors_opened");
    }
    public void PlayCollision(float severity = 1f) { PlayOneShot(collisionHit, Mathf.Clamp01(0.35f + severity * 0.2f)); ForwardEvent("bus.collision"); }
    public void PlayMissionComplete() { PlayOneShot(missionFanfare, 0.9f); ForwardEvent("mission.complete"); }
    public void PlayRankUp() { PlayOneShot(rankJingle, 0.9f); ForwardEvent("rank.up"); }
    public void PlayPassengerVoice(AudioClip clip) { PlayOneShotOn(passengerVoiceSource, clip, 0.85f); ForwardEvent("passenger.voice"); }
    void OnRankUp(RankUpEventData data) { PlayRankUp(); }

    void PlayOneShot(AudioClip clip, float volume)
    {
        PlayOneShotOn(oneShotSource, clip, volume);
    }

    void PlayOneShotOn(AudioSource source, AudioClip clip, float volume)
    {
        if (clip != null && source != null && source.isActiveAndEnabled && source.gameObject.activeInHierarchy)
            source.PlayOneShot(clip, volume * SettingsManager.EnsureExists().Current.effectsVolume);
    }

    void ApplyProductionProfile()
    {
        if (productionProfile == null) return;
        engineIdle = productionProfile.engineIdle != null ? productionProfile.engineIdle : engineIdle;
        engineLoad = productionProfile.engineLoad != null ? productionProfile.engineLoad : engineLoad;
        tyreRoll = productionProfile.tyreRoll != null ? productionProfile.tyreRoll : tyreRoll;
        tyreSqueal = productionProfile.tyreSqueal != null ? productionProfile.tyreSqueal : tyreSqueal;
        brakeSqueal = productionProfile.brakeSqueal != null ? productionProfile.brakeSqueal : brakeSqueal;
        airHiss = productionProfile.airHiss != null ? productionProfile.airHiss : airHiss;
        collisionHit = productionProfile.collisionHit != null ? productionProfile.collisionHit : collisionHit;
        passengerChatter = productionProfile.passengerChatter != null ? productionProfile.passengerChatter : passengerChatter;
        airConditioning = productionProfile.airConditioning != null ? productionProfile.airConditioning : airConditioning;
        cityAmbience = productionProfile.cityAmbience != null ? productionProfile.cityAmbience : cityAmbience;
        uiClick = productionProfile.uiClick != null ? productionProfile.uiClick : uiClick;
        dockingChime = productionProfile.dockingChime != null ? productionProfile.dockingChime : dockingChime;
        missionFanfare = productionProfile.missionFanfare != null ? productionProfile.missionFanfare : missionFanfare;
        rankJingle = productionProfile.rankJingle != null ? productionProfile.rankJingle : rankJingle;
    }

    /// <summary>
    /// Hands an audio event to the optional bridge. The bridge is preferred through
    /// <see cref="IRealBusAudioEventSink"/> because the old name-based
    /// <c>SendMessage</c> silently did nothing when the method was missing or renamed,
    /// which made a broken bridge indistinguishable from an absent one.
    /// </summary>
    void ForwardEvent(string eventName)
    {
        if (fmodEventBridge == null) return;

        if (fmodEventBridge is IRealBusAudioEventSink sink)
        {
            sink.PlayRealBusEvent(eventName);
            return;
        }

        // Legacy hook for a bridge that has not adopted the interface yet.
        fmodEventBridge.SendMessage("PlayRealBusEvent", eventName, SendMessageOptions.DontRequireReceiver);
    }

    AudioSource EnsureLoop(GameObject owner, AudioClip clip, string label, float volume, bool spatial = true)
    {
        GameObject child = new GameObject(label); child.transform.SetParent(owner.transform, false);
        AudioSource source = AddSource(child, true, spatial ? 0.82f : 0f); source.clip = clip; source.volume = volume;
        if (clip != null && source.isActiveAndEnabled && source.gameObject.activeInHierarchy) source.Play(); return source;
    }

    static AudioSource AddSource(GameObject owner, bool loop, float spatialBlend)
    {
        AudioSource source = owner.AddComponent<AudioSource>(); source.playOnAwake = false; source.loop = loop; source.spatialBlend = spatialBlend; source.rolloffMode = AudioRolloffMode.Logarithmic; source.maxDistance = 80f; return source;
    }

    static void Configure(AudioSource source, float volume, float pitch)
    {
        if (source == null || !source.isActiveAndEnabled || !source.gameObject.activeInHierarchy) return;
        source.volume = volume; source.pitch = pitch;
        if (source.clip != null && !source.isPlaying) source.Play();
    }

    /// <summary>
    /// Builds every clip the game needs when no production audio is installed. The
    /// buffers come from <see cref="ProceduralAudio" /> so that the looping engine and
    /// tyre samples are genuinely periodic - the earlier generators faded every clip
    /// in and out, which made a one-second loop pulse once per second.
    /// </summary>
    void CreateFallbackClips()
    {
        ProceduralAudio.ClipSpec[] library = ProceduralAudio.BuildFallbackLibrary();
        for (int i = 0; i < library.Length; i++)
        {
            ProceduralAudio.ClipSpec spec = library[i];
            AudioClip clip = ProceduralAudio.ToClip(spec.name, spec.samples, spec.loop);
            switch (spec.key)
            {
                case "engineIdle":     if (engineIdle == null) engineIdle = clip; break;
                case "engineLoad":     if (engineLoad == null) engineLoad = clip; break;
                case "tyreRoll":       if (tyreRoll == null) tyreRoll = clip; break;
                case "tyreSqueal":     if (tyreSqueal == null) tyreSqueal = clip; break;
                case "brakeSqueal":    if (brakeSqueal == null) brakeSqueal = clip; break;
                case "airHiss":        if (airHiss == null) airHiss = clip; break;
                case "uiClick":        if (uiClick == null) uiClick = clip; break;
                case "dockingChime":   if (dockingChime == null) dockingChime = clip; break;
                case "missionFanfare": if (missionFanfare == null) missionFanfare = clip; break;
                case "rankJingle":     if (rankJingle == null) rankJingle = clip; break;
                case "collisionHit":   if (collisionHit == null) collisionHit = clip; break;
                case "cityAmbience":   if (cityAmbience == null) cityAmbience = clip; break;
                case "airConditioning": if (passengerChatter == null && airConditioning == null) airConditioning = clip; break;
            }
        }
    }
}
