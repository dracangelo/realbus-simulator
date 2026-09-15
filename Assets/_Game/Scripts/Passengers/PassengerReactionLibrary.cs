using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PassengerReactionDefinition
{
    public string reactionId;
    [TextArea] public string subtitle;
    public AudioClip[] voiceClips;
    public string animatorTrigger;
}

[CreateAssetMenu(fileName = "PassengerReactionLibrary", menuName = "RealBus/Passenger Reaction Library")]
public class PassengerReactionLibrary : ScriptableObject
{
    public PassengerReactionDefinition[] reactions;

    public PassengerReactionDefinition Find(string reactionId)
    {
        if (reactions == null || string.IsNullOrWhiteSpace(reactionId)) return null;
        for (int i = 0; i < reactions.Length; i++)
            if (reactions[i] != null && string.Equals(reactions[i].reactionId, reactionId, System.StringComparison.OrdinalIgnoreCase))
                return reactions[i];
        return null;
    }
}
