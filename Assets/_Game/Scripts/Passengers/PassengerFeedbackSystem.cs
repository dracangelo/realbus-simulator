using UnityEngine;
using UnityEngine.SceneManagement;

public class PassengerFeedbackSystem : MonoBehaviour
{
    public static PassengerFeedbackSystem Instance { get; private set; }
    PassengerManager passengers;
    BusController bus;
    int lastPassengerCount;
    int lastMoodBand = -1;
    WeatherState lastWeather;
    float lastSpeed;
    float reactionCooldown;
    PassengerReactionLibrary reactionLibrary;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Bootstrap() { EnsureExists(); }

    public static PassengerFeedbackSystem EnsureExists()
    {
        if (Instance != null) return Instance;
        PassengerFeedbackSystem existing = FindFirstObjectByType<PassengerFeedbackSystem>();
        return existing != null ? existing : new GameObject("PassengerFeedbackSystem").AddComponent<PassengerFeedbackSystem>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start() { reactionLibrary = Resources.Load<PassengerReactionLibrary>("PassengerReactionLibrary"); Bind(); }
    void OnDestroy() { if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded; }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { Bind(); }

    void Bind()
    {
        passengers = PassengerManager.Instance != null ? PassengerManager.Instance : FindFirstObjectByType<PassengerManager>();
        bus = FindFirstObjectByType<BusController>();
        lastPassengerCount = passengers != null ? passengers.currentPassengers : 0;
        lastMoodBand = passengers != null ? MoodBand(passengers.averageSatisfaction) : -1;
        lastWeather = WeatherSystem.Instance != null ? WeatherSystem.Instance.currentWeather : WeatherState.Clear;
        lastSpeed = bus != null ? bus.currentSpeedKmh : 0f;
    }

    void Update()
    {
        if (passengers == null || bus == null) { Bind(); return; }
        reactionCooldown = Mathf.Max(0f, reactionCooldown - Time.unscaledDeltaTime);
        if (passengers.currentPassengers > lastPassengerCount) React("boarding", BoardingLine(passengers.lastBoardingCount));
        else if (passengers.currentPassengers < lastPassengerCount) React("alighting", "Thank you, driver.");
        lastPassengerCount = passengers.currentPassengers;

        int mood = MoodBand(passengers.averageSatisfaction);
        if (mood < lastMoodBand && mood == 0) React("comfortable", "That was a comfortable ride.");
        else if (mood > lastMoodBand && mood >= 2) React(mood == 3 ? "upset" : "rough_ride", mood == 3 ? "Please drive more carefully!" : "Could we have a smoother ride?");
        lastMoodBand = mood;

        float deceleration = (lastSpeed - bus.currentSpeedKmh) / Mathf.Max(0.01f, Time.deltaTime) / 3.6f;
        if (deceleration > 5.2f && passengers.currentPassengers > 0) React("harsh_brake", "Easy on the brakes!");
        lastSpeed = bus.currentSpeedKmh;

        if (WeatherSystem.Instance != null && WeatherSystem.Instance.currentWeather != lastWeather)
        {
            lastWeather = WeatherSystem.Instance.currentWeather;
            if (passengers.currentPassengers > 0 && IsSevereWeather(lastWeather)) React("severe_weather", "Glad to be out of this weather.");
        }
    }

    void React(string reactionId, string fallbackMessage)
    {
        if (reactionCooldown > 0f) return;
        PassengerReactionDefinition reaction = reactionLibrary != null ? reactionLibrary.Find(reactionId) : null;
        string message = reaction != null && !string.IsNullOrWhiteSpace(reaction.subtitle) ? reaction.subtitle : fallbackMessage;
        if (string.IsNullOrWhiteSpace(message)) return;
        reactionCooldown = 7f; SubtitleManager.EnsureExists().Show("Passenger", message, 2.6f);
        if (reaction != null)
        {
            if (reaction.voiceClips != null && reaction.voiceClips.Length > 0)
                RealBusAudioManager.EnsureExists().PlayPassengerVoice(reaction.voiceClips[Random.Range(0, reaction.voiceClips.Length)]);
            PassengerVisualPool visuals = FindFirstObjectByType<PassengerVisualPool>();
            if (visuals != null) visuals.TriggerReaction(reaction.animatorTrigger);
        }
    }

    static string BoardingLine(int count) { return count > 5 ? "Busy stop today." : "Good day, driver."; }
    static bool IsSevereWeather(WeatherState weather) { return weather == WeatherState.HeavyRain || weather == WeatherState.Thunderstorm || weather == WeatherState.Snow || weather == WeatherState.Blizzard; }
    public static int MoodBand(float satisfaction) { return satisfaction >= 0.8f ? 0 : satisfaction >= 0.55f ? 1 : satisfaction >= 0.35f ? 2 : 3; }
    public static string MoodIcon(float satisfaction) { int band = MoodBand(satisfaction); return band == 0 ? "☺" : band == 1 ? "●" : band == 2 ? "△" : "!"; }
}
