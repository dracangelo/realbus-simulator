using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioProductionProfile", menuName = "RealBus/Audio Production Profile")]
public class AudioProductionProfile : ScriptableObject
{
    [Header("Mixer routing")]
    public AudioMixerGroup vehicleGroup;
    public AudioMixerGroup cabinGroup;
    public AudioMixerGroup ambienceGroup;
    public AudioMixerGroup effectsGroup;
    public AudioMixerGroup uiGroup;

    [Header("Vehicle")]
    public AudioClip engineIdle, engineLoad, tyreRoll, tyreSqueal, brakeSqueal, airHiss, collisionHit;
    [Header("Cabin and world")]
    public AudioClip passengerChatter, airConditioning, cityAmbience;
    [Header("Interface and rewards")]
    public AudioClip uiClick, dockingChime, missionFanfare, rankJingle;
}
