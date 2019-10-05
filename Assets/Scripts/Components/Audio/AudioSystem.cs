using System.Collections.Generic;
using UnityEngine;


public class AudioSystem : IComponentSystem
{
    //List of matching entities
    private List<Entity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(AudioComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            Entity entity = entities[i];
            int entityID = entity.GetEntityID();

            AudioComponent audioComponent = ComponentManager.GetComponent<AudioComponent>(entityID);

            if (!audioComponent.init)
            {
                audioComponent.init = true;
                audioComponent.audioSource = entity.entityGameObject.GetComponentInChildren<AudioSource>();
            }

            if (audioComponent.init && audioComponent.audioSource != null && audioComponent.play != audioComponent.lastPlay)
            {
                audioComponent.lastPlay = audioComponent.play;
                SetAudioStatus(audioComponent, audioComponent.play);
            }
        }
    }


    /// <summary>
    /// Toggles the audio.
    /// </summary>
    /// <param name="_healthComponent"></param>
    /// <param name="_status"></param>
    private void SetAudioStatus(AudioComponent _audioComponent, bool _status)
    {
        if (_status) _audioComponent.audioSource.Play();
        else _audioComponent.audioSource.Stop();
    }
}
