using System.Collections.Generic;
using UnityEngine;


public class AudioSystem : IComponentSystem
{
    //List of matching entities
    private List<IEntity> entities;


    public void OnCreate() { }


    public void OnUpdate()
    {
        entities = EntityManager.Entities.ForEach(typeof(AudioComponent));

        for (int i = 0; i < entities.Count; i++)
        {
            IEntity iEntity = entities[i];
            int entityID = iEntity.GetEntityID();

            AudioComponent audioComponent = ComponentManager.GetComponent<AudioComponent>(entityID);

            if (!audioComponent.init)
            {
                audioComponent.init = true;
                audioComponent.audioSource = ((Entity)iEntity).entityGameObject.GetComponent<AudioSource>();
            }

            if (audioComponent.init && audioComponent.play != audioComponent.lastPlay)
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
