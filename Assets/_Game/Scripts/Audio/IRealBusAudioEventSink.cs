/// <summary>
/// Typed sink for the optional audio middleware bridge (FMOD, Wwise, ...).
///
/// <see cref="RealBusAudioManager"/> previously forwarded events by calling
/// <c>SendMessage("PlayRealBusEvent", ...)</c> by name, which fails silently: a
/// missing or renamed method on the bridge looked exactly like a bridge that works
/// and simply ignores the event. Implementing this interface makes the contract
/// explicit and compile-checked.
///
/// No audio middleware package is installed in this project, so nothing implements
/// the interface today - it exists so the hook is a real contract rather than a
/// string that may or may not resolve.
/// </summary>
public interface IRealBusAudioEventSink
{
    /// <summary>Called for each gameplay audio event, e.g. "bus.doors_opened".</summary>
    void PlayRealBusEvent(string eventName);
}
