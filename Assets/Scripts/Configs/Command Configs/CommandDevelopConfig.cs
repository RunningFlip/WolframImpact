using UnityEngine;
using System;


[CreateAssetMenu(fileName = "Command_Develop_Config", menuName = "ScriptableObjects/Command Configs/Command Develop Config")]
public class CommandDevelopConfig : CommandConfig
{
    [Header("Development Config")]
    public DevelopmentConfig config;


    //Development
    [NonSerialized] private int maxObjectsToBuild;

    //Components
    [NonSerialized] private DevelopmentComponent developComponent;
    [NonSerialized] private Transform movementTransform;

    //Other components
    [NonSerialized] private ResourceComponent resourceComponent;


    //Audio
    //--> Serializable, cause this sound settings are used in every of this configs
    private float volume;
    private AudioClip developmentFailedClip;


    public override void CommandSetup(int _userIndex, Entity _entity, CommandTargetComponent _commandTargetComponent)
    {
        base.CommandSetup(_userIndex, _entity, _commandTargetComponent);

        //Components
        FactionComponent factionComponent = ComponentManager.GetComponent<FactionComponent>(_entity.GetEntityID());
        int userEntityId = GameController.Instance.GetUserEntitId(factionComponent.userIndex);
        resourceComponent = ComponentManager.GetComponent<ResourceComponent>(userEntityId);
        developComponent = ComponentManager.GetComponent<DevelopmentComponent>(_entity.GetEntityID());
        movementTransform = _entity.entityGameObject.transform;

        //Parameters
        GameParameter gameParameter = GameController.Instance.GameParameter;
        maxObjectsToBuild = gameParameter.maxObjectsToBuild;

        //Sound
        volume = gameParameter.developmentFailedVolume;
        developmentFailedClip = gameParameter.developmentFailedClip;
    }


    public override void StartCommand(GameObjectEntityHolder _targetEntityHolder, Vector3 _targetPos)
    {
        if (developComponent.objectsToDevelop.Count < maxObjectsToBuild)
        {
            bool enoughResources = CheckResources();

            if (enoughResources)
            {
                developComponent.objectsToDevelop.Add(config);
            }
            else
            {
                TryPlayFailSound();
            }
        }
        else
        {
            TryPlayFailSound();
        }
        isActive = false;
    }


    public override void UpdateCommand() { }


    public override void AfterCommand()
    {
        base.AfterCommand();
    }


    private bool CheckResources()
    {
        bool enoughResources = false;

        if (resourceComponent.currentEnergy >= config.requiredEnergy && resourceComponent.currentMass >= config.requiredMass)
        {
            resourceComponent.currentEnergy -= config.requiredEnergy;
            resourceComponent.currentMass -= config.requiredMass;

            enoughResources = true;
        }
        return enoughResources;
    }


    private void TryPlayFailSound()
    {
        if (developmentFailedClip != null)
        {
            AudioSource.PlayClipAtPoint(developmentFailedClip, movementTransform.position, volume);
        }
    }
}
