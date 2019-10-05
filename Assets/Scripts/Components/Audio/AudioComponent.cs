using UnityEngine;


[CreateAssetMenu(fileName = "AudioComponent", menuName = "ScriptableObjects/Components Configs/AudioComponent")]
public class AudioComponent : EntityComponent
{
    [Header("Status")]
    public bool play;
    public bool lastPlay;

    [Header("Specs")]
    public AudioSource audioSource;
}
